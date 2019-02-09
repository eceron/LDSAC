﻿using System;
using System.Data.Common;
using OpenSystems.Common.Data;
using System.Data;
using OpenSystems.Financing.Common;
using OpenSystems.Common.ExceptionHandler;

namespace LDSAC.DAL
{
    public class DataAccessLDSAC
    {
        /// <summary>
        /// Inserta la información del pagaré
        /// </summary>
        /// <param name="inuPagacofi">Código de la financiación</param>
        /// <param name="inuValCapital">Valor del capital</param>
        /// <param name="inuValInteres">Valor del interés</param>
        /// <param name="inuPagaNucu">Número de cuotas</param>
        /// <param name="inuPagaPoin">Porcentaje de interés</param>
        /// <param name="inuPagaVcPa">Valor de la cuota</param>
        /// <param name="inuPagaDeAp">Descuentos</param>
        /// <returns>Código del pagaré</returns>
        public static Int64? InsertPagare
        (
            long inuPagacofi,
            Double inuValCapital,
            Double inuValInteres,
            long inuPagaNucu,
            Double inuPagaPoin,
            Double inuPagaVcPa
        )
        {
            Int64? warrDoc;
            try
            {
                String CommandSQL = "FI_BOWarrantyDoc.InsWarrantyDoc";

                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.AddInParameter(dbCommand, "inuPagacofi", DbType.Int64, inuPagacofi);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuValCapital", DbType.Double, inuValCapital);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuValInteres", DbType.Double, inuValInteres);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuPagaNucu", DbType.Int64, inuPagaNucu);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuPagaPoin", DbType.Double, inuPagaPoin);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuPagaVcPa", DbType.Double, inuPagaVcPa);
                    OpenDataBase.db.AddOutParameter(dbCommand, "onuWarrDocCons", DbType.Int64, 0);
                    OpenDataBase.db.ExecuteNonQuery(dbCommand);

                    warrDoc = Convert.ToInt64(OpenDataBase.db.GetParameterValue(dbCommand, "onuWarrDocCons"));
                }
            }
            catch (Exception ex)
            {
                //ExceptionHandler.DisplayError(ex.GetHashCode);
                warrDoc = null;
            }
            return warrDoc;
        }

        /// <summary>
        /// Registra el codeudor del pagaré
        /// </summary>
        /// <param name="WarrantyDoc">Código del pagaré</param>
        /// <param name="Cosigner">Código del cliente</param>
        public static void RegisterCosigner
        (
            Int64? WarrantyDoc,
            Int64? Cosigner
        )
        {
            try
            {
                String CommandSQL = "FI_BOWarrantyDoc.InsCosigner";

                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.AddInParameter(dbCommand, "inuWarrDocCons", DbType.Int64, WarrantyDoc);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuCosigner", DbType.Int64, Cosigner);
                    OpenDataBase.db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception ex)
            {                
                WarrantyDoc = null;
            }
        }

        /// <summary>
        /// Devuelve la dirección a partir del id
        /// </summary>
        /// <param name="AddressId">Código de la dirección</param>
        /// <returns></returns>
        public static String GetAddress(Int64? AddressId)
        {
            String address = String.Empty;
            try
            {
                String CommandSQL = "AB_BOBasicDataServices.fsbGetDescAddressParsed";

                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.AddInParameter(dbCommand, "inuAddressId", DbType.Int64, AddressId);
                    OpenDataBase.db.AddParameter(dbCommand, "RETURN_VALUE", DbType.String, 0,
                        ParameterDirection.ReturnValue, true, 0, 0, string.Empty,
                        DataRowVersion.Default, null);
                    OpenDataBase.db.ExecuteNonQuery(dbCommand);

                    address = Convert.ToString(OpenDataBase.db.GetParameterValue(dbCommand, "RETURN_VALUE"));
                }
            }
            catch (Exception ex)
            {
                address = null;
            }

            return address;
        }

        /// <summary>
        /// Carga la información del pagaré de la garantia
        /// </summary>
        /// <param name="warrantyDocID">Código de la garantia</param>
        public static void LoadWarrantyDoc(Int64 warrantyDocID)
        {
            String sql = "pkboem_promnote_mem.LoadData";

            using (DbCommand cmd = OpenDataBase.db.GetStoredProcCommand(sql))
            {
                OpenDataBase.db.AddInParameter(cmd, "inuWarrantyDocID", DbType.Decimal, warrantyDocID);
                OpenDataBase.db.ExecuteNonQuery(cmd);
            }
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
        /// Establece las condiciones de financiacion para Simular o Financiar
        /// con los datos del componente de saldos por concepto sincronizado con la tabla
        /// temporal mo_tmp_bal_by_concept
        /// </summary>
        /// <param name="difepldi">Plan de financiacion</param>
        /// <param name="difemeca">Metodo de calculo</param>
        /// <param name="initPayDate">Fecha de pago de la primera cuota</param>
        /// <param name="difeinte">Porcentaje de interes</param>
        /// <param name="difespre">Puntos Adicionales</param>
        /// <param name="difepoci">Porcentaje de Cuota Inicial</param>
        /// <param name="difenucu">Numero de cuotas</param>
        /// <param name="difenudo">Documento de soporte</param>
        /// <param name="porcToFinanc">Porcentaje a financiar</param>
        /// <param name="valueToPay">Valor a pagar</param>
        /// <param name="iva1cuota">Financiar iva a 1 cuota (S|N)</param>
        /// <param name="program">Programa</param>
        /// <param name="AcumCI"></param>
        /// <param name="AcumQuota"></param>
        /// <param name="Balance"></param>        
        /// <param name="simulate">Simular</param>
        public static void SetFinanConditions
        (
            Int64? difepldi,
            Int64? difemeca,
            DateTime? initPayDate,
            Double? difeinte,
            Double? difespre,
            Int64? difenucu,
            String difenudo,
            Double? porcToFinanc,
            Double? valueToPay,
            String iva1cuota,
            String program,
            ref Double? AcumQuota,
            ref Double? Balance,
            Boolean Simulate,
            Boolean ChangeConditions,
            ref Double TotalAcumCapital,
            ref Double TotalAcumCuotExtr,
            ref Double TotalAcumInteres,
            ref Int64? nuFinanCode,
            ref String signRequired
        )
        {
            try
            {
                String CommandSQL = "CC_BOFinancing.ExecDebtFinanc";

                String Simulating;
                String stChangingConditions;

                if (Simulate) Simulating = "S"; else Simulating = "N";
                if (ChangeConditions) stChangingConditions = "S"; else stChangingConditions = "N";

                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.AddInParameter(dbCommand, "inuPlan", DbType.Int64, difepldi);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuMetodoCuota", DbType.Int64, difemeca);
                    OpenDataBase.db.AddInParameter(dbCommand, "idtFechaDiferido", DbType.DateTime, initPayDate);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuPorcInteres", DbType.Double, difeinte);

                    OpenDataBase.db.AddInParameter(dbCommand, "inuSpread", DbType.Double, difespre);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuNumeroCuotas", DbType.Int64, difenucu);
                    OpenDataBase.db.AddInParameter(dbCommand, "isbDocumento", DbType.String, difenudo);

                    OpenDataBase.db.AddInParameter(dbCommand, "inuPorcAFinanciar", DbType.Double, porcToFinanc);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuVlrPagoIni", DbType.Double, valueToPay);
                    OpenDataBase.db.AddInParameter(dbCommand, "ichIVAUnaCuota", DbType.String, iva1cuota);
                    OpenDataBase.db.AddInParameter(dbCommand, "isbPrograma", DbType.String, program);

                    OpenDataBase.db.AddInParameter(dbCommand, "isbSimulate", DbType.String, Simulating);
                    OpenDataBase.db.AddInParameter(dbCommand, "isbChangeConditions", DbType.String, stChangingConditions);

                    if (nuFinanCode.HasValue)
                    {
                        OpenDataBase.db.AddInParameter(dbCommand, "inuFinanCode", DbType.Int64, nuFinanCode);
                    }
                    else
                    {
                        OpenDataBase.db.AddInParameter(dbCommand, "inuFinanCode", DbType.Int64, DBNull.Value);
                    }

                    OpenDataBase.db.AddOutParameter(dbCommand, "onuAcumCuota", DbType.Double, 12);
                    OpenDataBase.db.AddOutParameter(dbCommand, "onuSaldo", DbType.Double, 12);
                    OpenDataBase.db.AddOutParameter(dbCommand, "onuTotalAcumCapital", DbType.Double, 0);

                    OpenDataBase.db.AddOutParameter(dbCommand, "onuTotalAcumCuotExtr", DbType.Double, 0);
                    OpenDataBase.db.AddOutParameter(dbCommand, "onuTotalAcumInteres", DbType.Double, 0);
                    OpenDataBase.db.AddOutParameter(dbCommand, "osbSignRequired", DbType.String, 0);

                    OpenDataBase.db.ExecuteNonQuery(dbCommand);

                    if (OpenDataBase.db.GetParameterValue(dbCommand, "onuAcumCuota") == DBNull.Value) { AcumQuota = null; }
                    else { AcumQuota = Convert.ToDouble(OpenDataBase.db.GetParameterValue(dbCommand, "onuAcumCuota")); }

                    if (OpenDataBase.db.GetParameterValue(dbCommand, "onuSaldo") == DBNull.Value) { Balance = null; }
                    else { Balance = Convert.ToDouble(OpenDataBase.db.GetParameterValue(dbCommand, "onuSaldo")); }

                    if (OpenDataBase.db.GetParameterValue(dbCommand, "onuTotalAcumCapital") == DBNull.Value) { TotalAcumCapital = 0.0; }
                    else { TotalAcumCapital = Convert.ToDouble(OpenDataBase.db.GetParameterValue(dbCommand, "onuTotalAcumCapital")); }

                    if (OpenDataBase.db.GetParameterValue(dbCommand, "onuTotalAcumCuotExtr") == DBNull.Value) { TotalAcumCuotExtr = 0.0; }
                    else { TotalAcumCuotExtr = Convert.ToDouble(OpenDataBase.db.GetParameterValue(dbCommand, "onuTotalAcumCuotExtr")); }

                    if (OpenDataBase.db.GetParameterValue(dbCommand, "onuTotalAcumInteres") == DBNull.Value) { TotalAcumInteres = 0.0; }
                    else { TotalAcumInteres = Convert.ToDouble(OpenDataBase.db.GetParameterValue(dbCommand, "onuTotalAcumInteres")); }

                    if (OpenDataBase.db.GetParameterValue(dbCommand, "osbSignRequired") == DBNull.Value) { signRequired = string.Empty; }
                    else { signRequired = Convert.ToString(OpenDataBase.db.GetParameterValue(dbCommand, "osbSignRequired")); }
                }
            }
            catch (Exception ex)
            {
                //FinancingExceptionHandler.HandleError(ex, FinancingExceptionHandler.RaiseParameterException.RaiseON, true);
                return;
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


        /// <summary>
        /// Ejecuta las validaciones configuradas para el tipo de solicitud 'Financiación de Deuda'
        /// </summary>
        /// <param name="tagLevel">Etiqueta del Nivel</param>
        /// <param name="NodeLevelValue">Identificador del nodo del nivel</param>
        public static void ExecFinanValidations
        (
            String baseEntity,
            String NodeLevelValue
        )
        {
            String CommandSQL = "gi_boinstancedata.ExecFinanValidations";

            try
            {
                using (DbCommand dbCommand = OpenDataBase.db.GetStoredProcCommand(CommandSQL))
                {
                    OpenDataBase.db.AddInParameter(dbCommand, "isbBaseEntity", DbType.String, baseEntity);
                    OpenDataBase.db.AddInParameter(dbCommand, "inuLevelId", DbType.String, NodeLevelValue);

                    OpenDataBase.db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception ex)
            {
                // Se propaga el error levantado por las validaciones 
                throw (ex);
            }
        }
    }
}