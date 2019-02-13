using System;
using System.Data.Common;
using OpenSystems.Common.Data;
using System.Data;
using OpenSystems.Financing.Common;
using OpenSystems.Common.ExceptionHandler;
using System.Collections.Generic;
using OpenSystems.Financing.Entities;
using OpenSystems.Common.Util;

namespace LDSAC.DAL
{
    public class DataAccessLDSAC
    {
        private const String _GET_PROD_FOR_CHANGE_COND = "LDC_BOABONOCAPITAL.GetDefByProd";
        private const String _GET_SUSC_PROD = "LDC_BOABONOCAPITAL.PRGETSUSCPRODUCT";
        private const String _EXECUTE_ABONO_CAPITAL = "LDC_BOABONOCAPITAL.PREXECUTEABONOCAPITAL";

        public static String GET_PROD_FOR_CHANGE_COND
        {
            get { return _GET_PROD_FOR_CHANGE_COND; }
        }

        public static String GET_SUSC_PROD
        {
            get { return _GET_SUSC_PROD; }
        }

        public static String EXECUTE_ABONO_CAPITAL
        {
            get { return _EXECUTE_ABONO_CAPITAL; }
        }        

        /// <summary>
        /// Inicializa estructuras de memoria para el proceso de financiación de deuda
        /// </summary>
        /// <param name="subscriptionId">Identificador de la suscripción</param>
        public static void InitializeFinancing(Int64 subscriptionId)
        {
            using (DbCommand command = OpenDataBase.db.GetStoredProcCommand("CC_BOFinancing.InitFinancingDebt"))
            {
                OpenDataBase.db.AddInParameter(command, "inuSubscription", DbType.Int64, subscriptionId);
                OpenDataBase.db.ExecuteNonQuery(command);
            }
        }      

        /// <summary>
        /// Inicializa colecciones y estructuras de memoria, excepto las cuotas extras.
        /// </summary>
        public static void ClearMemoryFinancing()
        {
            try
            {
                String CommandSQL = "cc_bofinancing.ClearMemoryFinancing";

                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception ex)
            {
                //FinancingExceptionHandler.HandleError(ex, FinancingExceptionHandler.RaiseParameterException.RaiseON, true);
                return;
            }

        }

        /// <summary>
        /// Inicializa las cuotas extras en memoria del server
        /// </summary>
        public static void ClearExtraPayment()
        {
            try
            {
                String CommandSQL = "cc_bofinancing.InitializeExtraPayment";

                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception ex)
            {
                //FinancingExceptionHandler.HandleError(ex, FinancingExceptionHandler.RaiseParameterException.RaiseON, true);
                return;
            }

        }

        /*Obtener productos y diferidos*/

        public static void GetDebtToChangeCond(Int64 subscriptionId, Int64? productId, Int64? financingId, Int64? deferredId,
            out Dictionary<Int64, Product> products, out List<Deferred> deferreds)
        {
            deferreds = new List<Deferred>();

            using (DbCommand cmd = OpenDataBase.db.GetStoredProcCommand(GET_PROD_FOR_CHANGE_COND))
            {
                //OpenDataBase.db.AddInParameter(cmd, "inuSubscriptionId", DbType.Int64, subscriptionId);
                OpenDataBase.db.AddInParameter(cmd, "inuProductId", DbType.Int64, productId);
                //OpenDataBase.db.AddInParameter(cmd, "inuFinancingId", DbType.Int64, financingId);
                //OpenDataBase.db.AddInParameter(cmd, "inuDeferredId", DbType.Int64, deferredId);
                OpenDataBase.db.AddParameterRefCursor(cmd, "orfProducts");
                OpenDataBase.db.AddParameterRefCursor(cmd, "orfDefWithPendBal");                

                /* Ejecuta el comando a través de un DataReader */
                using (IDataReader reader = OpenDataBase.db.ExecuteReader(cmd, CommandBehavior.SequentialAccess))
                {
                    GetProducts(reader, out products);

                    if (reader.NextResult())
                    {
                        GetDeferreds(reader, out deferreds);
                    }
                }
            }
        }

        private static void GetProducts(IDataReader reader, out Dictionary<Int64, Product> products)
        {
            products = new Dictionary<Int64, Product>();
            BaseProduct baseProduct;
            DependingProduct dependingProd;
            Int64? baseProductId;

            /* Se obtienen las posiciones numéricas de las columnas del conjunto de registros obtenido */
            int colProductId = reader.GetOrdinal("PRODUCT_ID");
            int colProdTypeDesc = reader.GetOrdinal("SERVDESC");
            int colProdTypeId = reader.GetOrdinal("PRODUCT_TYPE_ID");
            int colStatusId = reader.GetOrdinal("STATUS_ID");
            int colStatusDesc = reader.GetOrdinal("ESCODESC");
            int colPendingBalance = reader.GetOrdinal("PENDING_BALANCE");
            int colDefPendBalance = reader.GetOrdinal("DEFERRED_PENDING_BAL");
            int colBaseProdId = reader.GetOrdinal("BASE_PRODUCT_ID");
            int colSelected = reader.GetOrdinal("SELECTED");

            /* Procesa cada uno de los registros obtenidos */
            while (reader.Read())
            {
                baseProductId = OpenConvert.ToLongNullable(reader[colBaseProdId]);

                /* Se verifica si el producto es base o dependiente */
                if (!baseProductId.HasValue)
                {
                    baseProduct = new BaseProduct();
                    baseProduct.ProductId = OpenConvert.ToLong(reader[colProductId]);
                    baseProduct.Description = OpenConvert.ToString(reader[colProdTypeDesc]);
                    baseProduct.ProductTypeId = OpenConvert.ToLong(reader[colProdTypeId]);
                    baseProduct.StatusId = OpenConvert.ToLong(reader[colStatusId]);
                    baseProduct.StatusDescription = OpenConvert.ToString(reader[colStatusDesc]);
                    baseProduct.PendingBalance = OpenConvert.ToDecimal(reader[colPendingBalance]);
                    baseProduct.DeferredPendingBalance = OpenConvert.ToDecimal(reader[colDefPendBalance]);
                    baseProduct.Selected = OpenConvert.StringOpenToBool(OpenConvert.ToString(reader[colSelected]));
                    baseProduct.Enabled = true;

                    products.Add(baseProduct.ProductId, baseProduct);
                }
                else
                {
                    dependingProd = new DependingProduct();
                    dependingProd.BaseProductId = baseProductId.Value;
                    dependingProd.ProductId = OpenConvert.ToLong(reader[colProductId]);
                    dependingProd.Description = OpenConvert.ToString(reader[colProdTypeDesc]);
                    dependingProd.ProductTypeId = OpenConvert.ToLong(reader[colProdTypeId]);
                    dependingProd.StatusId = OpenConvert.ToLong(reader[colStatusId]);
                    dependingProd.StatusDescription = OpenConvert.ToString(reader[colStatusDesc]);
                    dependingProd.PendingBalance = OpenConvert.ToDecimal(reader[colPendingBalance]);
                    dependingProd.DeferredPendingBalance = OpenConvert.ToDecimal(reader[colDefPendBalance]);
                    dependingProd.Selected = OpenConvert.StringOpenToBool(OpenConvert.ToString(reader[colSelected]));
                    dependingProd.Enabled = true;

                    products.Add(dependingProd.ProductId, dependingProd);
                }
            }
        }

        private static void GetDeferreds(IDataReader reader, out List<Deferred> deferreds)
        {
            deferreds = new List<Deferred>();
            Deferred deferred;

            /* Se obtienen las posiciones numéricas de las columnas del conjunto de registros obtenido */
            int colRowNumber = reader.GetOrdinal("ROW_NUMBER_");
            int colDeferredId = reader.GetOrdinal("DEFERRED_ID");
            int colFinancingId = reader.GetOrdinal("FINANCING_ID");
            int colProductId = reader.GetOrdinal("PRODUCT_ID");
            int colConceptId = reader.GetOrdinal("CONCEPT_ID");
            int colConceptDesc = reader.GetOrdinal("CONCEPT_DESC");
            int colIntConceptId = reader.GetOrdinal("INTEREST_CONCEPT_ID");
            int colIntConceptDesc = reader.GetOrdinal("INTEREST_CONCEPT_DESC");
            int colQuotaValue = reader.GetOrdinal("QUOTA_VALUE");
            int colPendingBalance = reader.GetOrdinal("PENDING_BALANCE");
            int colLastMovDate = reader.GetOrdinal("LAST_MOVEMENT_DATE");

            /* Procesa cada uno de los registros obtenidos */
            while (reader.Read())
            {
                deferred = new Deferred();
                deferred.Position = OpenConvert.ToLong(reader[colRowNumber]);
                deferred.Id = OpenConvert.ToLong(reader[colDeferredId]);
                deferred.FinancingId = OpenConvert.ToLong(reader[colFinancingId]);
                deferred.ProductId = OpenConvert.ToLong(reader[colProductId]);
                deferred.ConceptId = OpenConvert.ToInt32(reader[colConceptId]);
                deferred.ConceptDescription = OpenConvert.ToString(reader[colConceptDesc]);
                deferred.InterestConcId = OpenConvert.ToInt32(reader[colIntConceptId]);
                deferred.InterestConcDescription = OpenConvert.ToString(reader[colIntConceptDesc]);
                deferred.QuoteValue = OpenConvert.ToDecimal(reader[colQuotaValue]);
                deferred.PendingBalance = OpenConvert.ToDecimal(reader[colPendingBalance]);
                deferred.LastMovementDate = OpenConvert.ToDateTimeNullable(reader[colLastMovDate]).Value;

                deferreds.Add(deferred);
            }
        }
        public static void GetSuscProd(Int64 productId, out Int64 subscriptionId)
        {
            using (DbCommand cmd = OpenDataBase.db.GetStoredProcCommand(GET_SUSC_PROD))
            {                
                OpenDataBase.db.AddInParameter(cmd, "inuProduct", DbType.Int64, productId);
                OpenDataBase.db.AddOutParameter(cmd, "outSusc", DbType.Int64, 8);                
                OpenDataBase.db.ExecuteNonQuery(cmd);
                subscriptionId = Convert.ToInt64(OpenDataBase.db.GetParameterValue(cmd, "outSusc"));
            }
        }

        public static void ExecuteAbonoCapital(String diferidos, Decimal valor_total)
        {

            using (DbCommand cmd = OpenDataBase.db.GetStoredProcCommand(EXECUTE_ABONO_CAPITAL))
            {                
                OpenDataBase.db.AddInParameter(cmd, "inuDiferidos", DbType.String, diferidos);
                OpenDataBase.db.AddInParameter(cmd, "inuValorTotal", DbType.Decimal, valor_total);
                OpenDataBase.db.ExecuteNonQuery(cmd);                
            }
        }
    }
}