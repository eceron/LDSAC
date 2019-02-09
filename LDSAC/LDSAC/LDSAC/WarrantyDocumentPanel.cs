using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenSystems.Financing.Controls.Resources.WarrantyDocumentPanel.Dao;
using OpenSystems.Financing.Entities;
using OpenSystems.Common.ExceptionHandler;
using OpenSystems.ClientRegister.Executor;
using OpenSystems.Financing.BL;
using LDSAC.DAL;


namespace LDSAC
{
    public partial class WarrantyDocumentPanel : OpenSystems.Financing.Windows.Controls.OpenSteepPanel
    {

        const int COMP_NUMBER = 5;

        //  Lista de codeudores
        private Dictionary<Int64, Int64> cosignersList;

        private Int64 _printingFormatCode;
        private Statement _statement;
        private Int64? _subscriberId;

        /// <summary>
        /// Identificador del cliente titular de la deuda que está siendo financiada
        /// </summary>
        public Int64? SubscriberId
        {
            get { return _subscriberId; }
            set { _subscriberId = value; }
        }

        /// <summary>
        /// Formato de impresión seleccionado en pantalla
        /// </summary>
        public Int64 PrintingFormatCode
        {
            get { return _printingFormatCode; }
            set { _printingFormatCode = value; }
        }       
        
        public WarrantyDocumentPanel()
        {
            InitializeComponent();

            // Crea instancia de la clase statement
            this._statement = new Statement(this.warrantyDocDataSet);
        }

        /// <summary>
        /// Inserta la información del pagaré y de los codeudores
        /// </summary>
        /// <param name="TotalAcumInteres">Interés acumulado</param>
        /// <param name="nuFinanCode">Código de la financiación</param>
        /// <param name="TotalToFinance">Valor a financiar</param>
        /// <param name="QuotaValue">Valor de la cuota</param>
        /// <param name="QuotasNumber">Número de cuotas</param>
        /// <param name="InterestOfFinacing">Interés de la financiación</param>
        /// <param name="Discount">Descuentos</param>
        public Int64? RegisterWarrDoc(
            Double TotalAcumInteres,
            Int64? nuFinanCode,
            Double TotalToFinance,
            Double QuotaValue,
            long QuotasNumber,
            Double InterestOfFinacing
            )
        {
            Int64? warrantyDoc;
            warrantyDoc = DataAccessLDSAC.InsertPagare
            (
                nuFinanCode.Value,
                TotalToFinance,
                TotalAcumInteres,
                QuotasNumber,
                InterestOfFinacing,
                QuotaValue
            );

            if (warrantyDoc == null)
            {
                return warrantyDoc;
            }

            foreach (Int64 consigner in cosignersList.Values)
            {
                //  Registra el codeudor
                DataAccessLDSAC.RegisterCosigner(warrantyDoc, consigner);
            }
            return warrantyDoc;
        }
    }
}
