using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenSystems.Financing.Controls;
using System.Collections;
//using OpenSystems.Financing.Common;
using OpenSystems.Common.ExceptionHandler;
//using OpenSystems.Financing.Windows.Controls;
//using OpenSystems.Financing.Controls.Resources.FinancingConditions.Gui;
using OpenSystems.Financing.BL;

namespace LDSAC
{
    public partial class DebtConditionPanel : OpenSystems.Financing.Windows.Controls.OpenSteepPanel
    {
        #region propiedades
        private Int64? _SubscriptionId;
        private Int64? _ProductId;
        private String _programname;
        private Boolean _allowsQuotasFlow = false;
        private Boolean _safeChangeQuotasFlow = true;
        private Double? _AverageQuota = null;

        public Double? AverageQuota
        {
            get { return _AverageQuota; }
            set { _AverageQuota = value; }
        }

        public Boolean AllowsQuotasFlow
        {
            get { return _allowsQuotasFlow; }
            set { _allowsQuotasFlow = value; }
        }

        public String Programname
        {
            get { return _programname; }
            set { _programname = value; }
        }

        public OpenFinancingConditions FinConditions
        {
            get { return this.FinancingConditions; }
        }

        public Double? FinSelectedValue
        {
            get { return this.FinancingConditions.FinSelectedValue; }
            set { this.FinancingConditions.FinSelectedValue = value; }
        }

        public Double? ValueToPay
        {
            get { return this.FinancingConditions.ValueToPay; }
            set { this.FinancingConditions.ValueToPay = value; }
        }

        public Double? ValueToFinancing
        {
            get { return this.FinancingConditions.ValueToFinancing; }
            set { this.FinancingConditions.ValueToFinancing = value; }
        }

        public Double? MinimumValueToPay
        {
            get { return this.FinancingConditions.MinimumValueToPay; }
            set { this.FinancingConditions.MinimumValueToPay = value; }
        }

        public Int64? SubscriptionId
        {
            get
            {
                return _SubscriptionId;
            }
            set
            {
                _SubscriptionId = value;
                this.FinancingConditions.SubscriptionId = _SubscriptionId;
                this.QuotasFlow.CurrentSubscription = _SubscriptionId;
                this.QuotasFlow.RoundFactor = this.FinancingConditions.RoundFactor;
            }
        }

        public Int64? ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
                this.FinancingConditions.ProductId = _ProductId;
                this.QuotasFlow.RoundFactor = this.FinancingConditions.RoundFactor;
            }
        }


        /// <summary>
        /// Indica sobre que productos deben actuar las condiciones de financiacion
        /// </summary>
        public Hashtable SelectedProducts
        {
            get { return this.FinancingConditions.SelectedProducts; }
            set { this.FinancingConditions.SelectedProducts = value; }
        }

        public Boolean MandatoryWarrantyDocument
        {
            get { return FinancingConditions.MandatoryWarrantyDocument; }
        }


        /// <summary>
        /// Dirección para segmentar los planes de financiación
        /// </summary>
        public Int64? AddressId
        {
            get { return this.FinancingConditions.AddressId; }
            set { this.FinancingConditions.AddressId = value; }
        }

        /// <summary>
        /// Categoria para segmentar los planes de financiación
        /// </summary>
        public long? CategoryId
        {
            get { return this.FinancingConditions.CategoryId; }
            set { this.FinancingConditions.CategoryId = value; }
        }

        /// <summary>
        /// Subcategoria para segmentar los planes de financiación
        /// </summary>
        public long? SubCategoryId
        {
            get { return this.FinancingConditions.SubCategoryId; }
            set { this.FinancingConditions.SubCategoryId = value; }
        }


        /// <summary>
        /// Condiciones de financiación
        /// </summary>
        public OpenSystems.Financing.Controls.OpenFinancingConditions FinancingConditions1
        {
            get { return FinancingConditions; }
            set { FinancingConditions = value; }
        }

        #endregion propiedades

        public void InitializeFinancingConditions()
        {
            this.FinancingConditions.InitializaAllFinancingConditions();
        }
        
        public DebtConditionPanel()
        {
            InitializeComponent();
        }
    }
}
