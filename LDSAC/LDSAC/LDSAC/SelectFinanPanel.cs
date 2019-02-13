using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using OpenSystems.Common.ExceptionHandler;
using OpenSystems.Common.Resources;
using OpenSystems.Financing.Common;
using OpenSystems.Financing.BL;
using OpenSystems.Financing.Entities;
using OpenSystems.Common.Util;

namespace LDSAC
{
    public partial class SelectFinanPanel : OpenSystems.Financing.Windows.Controls.OpenSteepPanel
    {
        #region Atributos
        private Int64 _subscriptionId;
        private Int64? _productId;
        private Int64? _financingId;
        private Int64? _deferredId;
        private Boolean _deferredSelChanged;
        private Hashtable _selectedProducts;
        private Decimal _valToChangeConditions;
        private List<String> _listaDiferidos;
        private string[] _finalListDiferidos;
        private Decimal _suma_diferidos;
        #endregion

        #region Propiedades
        public Decimal SumaDiferidos
        {
            get { return _suma_diferidos; }
        }

        public string[] FinalListDiferidos
        {
            get { return _finalListDiferidos; }
        }

        public Decimal ValToChangeConditions
        {
            get { return _valToChangeConditions; }
        }

        public List<String> ListDiferidos
        {
            get { return _listaDiferidos; }
        }

        [Browsable(false)]
        public Boolean DeferredSelChanged
        {
            get { return _deferredSelChanged; }
            set { _deferredSelChanged = value; }
        }


        /// <summary>
        /// Tabla Hash con los códigos de productos seleccionados
        /// </summary>
        public Hashtable SelectedProducts
        {
            get
            {
                /* Se obtiene la lista de productos base a partir del panel de productos */
                List<BaseProduct> baseProducts = this._pnlDebtSelection.BaseProducts;

                /* Se obtiene la colección de productos seleccionados a partir de la lista de productos base */
                _selectedProducts = FinancingController.Instance.GetSelectedProducts(ref baseProducts);

                return _selectedProducts;
            }
        }


        public String ProgramName
        {
            get { return this._pnlDebtSelection.ProgramName; }
            set { this._pnlDebtSelection.ProgramName = value; }
        }
        #endregion
        
        public SelectFinanPanel()
        {
            InitializeComponent();
            this._listaDiferidos = new List<String>();
            this._valToChangeConditions = 10;
            this._suma_diferidos = 0;
        }

        #region Acciones de Eventos
        // <summary>
        /// Manejador del evento asociado al cambio en la selección de un producto del panel de selección de deuda
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void _pnlDebtSelection_ProductSelectionChange(object sender, OpenSystems.Financing.UI.Controls.Events.ProductChangeEventArgs e)
        {
            /* Se verifica si se puede realizar el cambio de condiciones sobre la deuda diferida del producto,
             * si este se encuentra seleccionado */
            if (e.Product.Selected)
            {
                try
                {
                    /* Valida que se pueda realizar el cambio de condiciones sobre la deuda diferida
                     * del producto */
                    ChangeCondController.Instance.ValProdForChangeCond(e.Product, this.ProgramName);

                    /* Se indica que se seleccionen los diferidos al seleccionar el producto */
                    e.ProcessDeferreds = true;
                }
                catch (Exception ex)
                {
                    if (e.DisplayErrorMessages)
                    {
                        /* Se despliega el mensaje de error */
                        GlobalExceptionProcessing.ShowErrorException(ex);
                    }

                    /* Se cancela la selección del producto */
                    e.CancelChange = true;
                }
            }
        }


        /// <summary>
        /// Manejador del evento asociado al cambio en la selección de un diferido del panel de selección de deuda
        /// </summary>
        /// <param name="sender">Objeto que disparó el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void _pnlDebtSelection_DeferredSelectionChange(object sender, OpenSystems.Financing.UI.Controls.Events.DeferredSelectionEventArgs e)
        {
            /* Se verifica si el diferido puede ser incluido en el cambio de condiciones, si este se
             * encuentra seleccionado */
            //MessageBox.Show(Convert.ToString(e.Deferred.Id));
            if (e.Deferred.Selected)
            {
                //MessageBox.Show("Se selecciona"+ Convert.ToString(e.Deferred.Id));
                ListDiferidos.Add(Convert.ToString(e.Deferred.Id));
                this._suma_diferidos = this._suma_diferidos + e.Deferred.PendingBalance;
                try
                {
                    /* Valida que el diferido pueda ser seleccionado */
                    ChangeCondController.Instance.ValDeferredSelection(e.Deferred);
                }
                catch (Exception ex)
                {
                    if (e.DisplayErrorMessages)
                    {
                        /* Se despliega el mensaje de error */
                        GlobalExceptionProcessing.ShowErrorException(ex);
                    }

                    /*  Se cancela la selección del diferido */
                    e.CancelChange = true;
                }
            }
            else
            {
                //MessageBox.Show("Se deselecciona "+Convert.ToString(e.Deferred.Id));
                ListDiferidos.Remove(Convert.ToString(e.Deferred.Id));
                this._suma_diferidos = this._suma_diferidos - e.Deferred.PendingBalance;
            }

            /* Se valida si el cambio en la selección del diferido no será cancelado */
            /*if (!e.CancelChange)
            {
                MessageBox.Show(Convert.ToString(e.Deferred.Id));
                /* Se recalcula el valor total a financiar teniendo en cuenta el flag de selección del diferido */
                /*FinanConditionsController.Instance.ComputeValueToFinance(
                    e.Deferred,
                    ref this._valToChangeConditions);

                /* Se sincroniza el valor total a financiar con el campo del control */
                /*this.SynchronizeTotalToFinance();

                /* Establece el flag de cambio en la selección de diferidos */
                /*this._deferredSelChanged = true;
            }*/
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Obtiene la deuda diferida que puede ser incluida en un proceso de cambio de condiciones de financiación.
        /// </summary>
        /// <param name="subscriptionId">Identificador de la suscripción</param>
        /// <param name="productId">Identificador del producto</param>
        /// <param name="financingId">Código de la financiación</param>
        /// <param name="deferredId">Código del diferido</param>
        public void LoadDebtToChangeCond(Int64 subscriptionId, Int64? productId, Int64? financingId, Int64? deferredId)
        {
            /* Se establece la suscripción, el producto, la financiación y el diferido */
            this._subscriptionId = subscriptionId;
            this._productId = productId;
            this._financingId = financingId;
            this._deferredId = deferredId;

            /* Establece los datos en los campos respectivo del control */
            this.txtSubscriptionId.TextBoxValue = this._subscriptionId.ToString();
            this.txtFinancingId.TextBoxValue = this._financingId.ToString();

            /* Obtiene los productos a incluir en la financiación para la suscripción */
            /*this._pnlDebtSelection.BaseProducts = ChangeCondController.Instance.GetDebtToChangeCond(
                this._subscriptionId,
                this._productId,
                this._financingId,
                this._deferredId);*/
            this._pnlDebtSelection.BaseProducts = GetDebtToChangeCond(
                this._subscriptionId,
                this._productId,
                this._financingId,
                this._deferredId);

            /* Inicializa el indicador de cambio en la selección de diferidos */
            this._deferredSelChanged = true;
        }

        /*Obtiene productos y diferidos*/
        public List<BaseProduct> GetDebtToChangeCond(Int64 subscriptionId, Int64? productId, Int64? financingId, Int64? deferredId)
        {
            Dictionary<Int64, Product> products;
            List<Deferred> deferreds;

            /* Se obtienen los productos y diferidos a incluir en el cambio de condiciones */
            DAL.DataAccessLDSAC.GetDebtToChangeCond(
                subscriptionId,
                productId,
                financingId,
                deferredId,
                out products,
                out deferreds);

            /* Se adicionan los diferidos a los productos según corresponda */
            FinancingController.Instance.AddDeferredsToProducts(ref products, ref deferreds);

            /* Se obtiene la lista de productos base a partir de la colección de productos */
            List<BaseProduct> baseProducts = FinancingController.Instance.GetBaseProducts(ref products);

            return baseProducts;
        }


        /// <summary>
        /// Establece la deuda diferida sobre la cual se realizará el proceso de cambio de condiciones
        /// </summary>
        public void SetDebtToChangeCond()
        {
            List<BaseProduct> baseProducts = this._pnlDebtSelection.BaseProducts;
            Dictionary<Int64, Product> products = FinancingController.Instance.GetProducts(ref baseProducts);

            /* Establece la deuda diferida sobre la cual se realizará el proceso de cambio de condiciones */
            ChangeCondController.Instance.SetDebtToChangeCond(ref products);

            /* Se calcula el valor total a financiar a partir de los diferidos seleccionados correspondientes
             * a los productos de la lista de productos base */
            FinanConditionsController.Instance.ComputeValueToFinance(
                ref products,
                out this._valToChangeConditions);

            /* Se sincroniza el valor total a financiar con el campo del control */
            this.SynchronizeTotalToFinance();
        }


        /// <summary>
        /// Sincroniza el campo de texto correspondiente al valor total a financiar
        /// con el valor calculado
        /// </summary>
        private void SynchronizeTotalToFinance()
        {
            //this.txtBalToChangeCond.TextBoxValue = this._valToChangeConditions.ToString();
            this.txtBalToChangeCond.TextBoxValue = Convert.ToString(0);
        }

        public String Concatenar_Diferidos()
        {
            String cadena_diferidos;
            this._finalListDiferidos = new string[ListDiferidos.Count];
            int i = 0;
            foreach (String diferido in ListDiferidos)
            {
                this._finalListDiferidos[i] = diferido;
                i = i + 1;                
            }

            if (this._finalListDiferidos.Length == 0)
            {
                cadena_diferidos = "NO_SELECT";
            }
            else
            {
                cadena_diferidos = this._finalListDiferidos[0];
                for (int j = 1; j < this._finalListDiferidos.Length; j++)
                {
                    cadena_diferidos = cadena_diferidos + "," + this._finalListDiferidos[j];
                }
            }            

            return cadena_diferidos;            
        }

        public void ExecuteAbonoCapital()
        {
            String diferidos = Concatenar_Diferidos();
            if (Convert.ToDecimal(txtBalToChangeCond.TextBoxValue) > 0)
            {
                if (diferidos == "NO_SELECT")
                {
                    ExceptionHandler.DisplayMessage(ErrorMessages.INVALID_COSIGNER_ERR, "Debe seleccionar al menos un diferido");                      
                }
                else
                {
                    if (Convert.ToDecimal(txtBalToChangeCond.TextBoxValue) < SumaDiferidos)
                    {                        
                        //DAL.DataAccessLDSAC.ExecuteAbonoCapital(diferidos, Convert.ToDecimal(txtBalToChangeCond.TextBoxValue));
                    }
                    else
                    {
                        ExceptionHandler.DisplayMessage(ErrorMessages.INVALID_COSIGNER_ERR, "El valor a abonar debe ser menor a la suma de los diferidos seleccionados, suma de diferidos: $" + Convert.ToString(SumaDiferidos));                        
                    }
                }                
            }
            else
            {
                ExceptionHandler.DisplayMessage(ErrorMessages.INVALID_COSIGNER_ERR,"El valor a abonar debe ser mayor que cero");                                
            }

        }
        #endregion
    }
}
