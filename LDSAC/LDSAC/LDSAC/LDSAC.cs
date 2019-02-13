using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenSystems.Financing.BL;
using OpenSystems.Request.RequestRegisterBasic.UI.Controls;
using OpenSystems.Windows.Controls;
using OpenSystems.Common.ExceptionHandler;
using System.Collections;
using OpenSystems.Common.Resources;
using FinanBLConstants = OpenSystems.Financing.BL.Constants;
using LDSAC.DAL;
using OpenSystems.Financing.Windows.Controls;
using OpenSystems.Windows.Controls.OperatingSectorControl.DAO;
using OpenSystems.Financing.Common;
using OpenSystems.Financing.Entities;


namespace LDSAC
{
    public partial class LDSAC : Form
    {
        #region Atributos
        private String _baseEntity;
        private String _nodeLevelValue;
        private Int64? _financingId;
        private Int64? _deferredId;
        private Int64? _subscriptionId;
        private Int64? _productId;
        private Boolean successfulOperation = false;
        #endregion              
        
        #region Propiedades

        /// <summary>
        /// Flag que indica si el proceso terminó con exito
        /// </summary>
        public Boolean SuccessfulOperation
        {
            get { return successfulOperation; }
        }

        /// <summary>
        /// Tag del Nivel dentro de la aplicacion que ejecuta cambio de condiciones
        /// </summary>
        public String BaseEntity
        {
            get { return _baseEntity; }
            set { _baseEntity = value; }
        }

        /// <summary>
        /// Codigo de la finaciacion para cambio de condiciones
        /// </summary>
        public String NodeLevelValue
        {
            get { return _nodeLevelValue; }
            set { _nodeLevelValue = value; }
        }


        /// <summary>
        /// Financiacion
        /// </summary>
        public Int64? FinancingId
        {
            get { return _financingId; }
            set
            {
                _financingId = value;

                /* Se verifica que la financiación no sea nula */
                if (_financingId.HasValue)
                {
                    /* Se obtienen los datos a partir de la financiación */
                    ChangeCondController.Instance.GetDataFromFinancing(
                        _financingId.Value,
                        out this._subscriptionId);

                    /* Se ejecuta la acción por evento asociada a la ejecución del proceso */
                    FinancingController.Instance.ValProcessExecution(
                        this._subscriptionId.Value,
                        this._productId);

                    /* Se establece la suscripción en el control de datos básicos de la solicitud */
                    this.MotiveGenericDataPanel.SubscriptionId = this._subscriptionId;

                    /* Se obtiene la deuda diferida sobre la cual se realizará el cambio de condiciones */
                    this.SelectFinanPanel.LoadDebtToChangeCond(
                        this._subscriptionId.Value,
                        this._productId,
                        _financingId,
                        this._deferredId);
                }
            }
        }

        /// <summary>
        /// Diferido
        /// </summary>
        public Int64? DeferredId
        {
            get { return _deferredId; }
            set
            {
                _deferredId = value;

                /* Se verifica que el diferido no sea nulo */
                if (_deferredId.HasValue)
                {
                    /* Se obtienen los datos a partir del diferido */
                    ChangeCondController.Instance.GetDataFromDeferred(
                        _deferredId.Value,
                        out this._financingId,
                        out this._productId,
                        out this._subscriptionId);

                    /* Se ejecuta la acción por evento asociada a la ejecución del proceso */
                    FinancingController.Instance.ValProcessExecution(
                        this._subscriptionId.Value,
                        this._productId);

                    /* Se establece el producto en el control de datos básicos de la solicitud */
                    this.MotiveGenericDataPanel.ProductId = this._productId;

                    /* Se obtiene la deuda diferida sobre la cual se realizará el cambio de condiciones */
                    this.SelectFinanPanel.LoadDebtToChangeCond(
                        this._subscriptionId.Value,
                        this._productId,
                        this._financingId,
                        _deferredId);
                }
            }
        }
        public Int64? SubscriptionId
        {
            get { return _subscriptionId; }
            set
            {
                _subscriptionId = value;

                /* Se verifica que la suscripción no sea nula */
                if (_subscriptionId.HasValue)
                {
                    /* Se ejecuta la acción por evento asociada a la ejecución del proceso */
                    FinancingController.Instance.ValProcessExecution(
                        _subscriptionId.Value,
                        this._productId);

                    /* Se establece la suscripción en el control de datos básicos de la solicitud */
                    this.MotiveGenericDataPanel.SubscriptionId = _subscriptionId;

                    /* Se obtiene la deuda diferida sobre la cual se realizará el cambio de condiciones */
                    this.SelectFinanPanel.LoadDebtToChangeCond(
                        _subscriptionId.Value,
                        this._productId,
                        this._financingId,
                        this._deferredId);
                }
            }
        }
        public Int64? ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;

                /* Se verifica que el producto no sea nulo */
                if (_productId.HasValue)
                {
                    /* Establece el producto en el control de datos básicos de la solicitud */
                    this.MotiveGenericDataPanel.ProductId = _productId;

                    /* Establece la suscripción a partir de los datos de la solicitud */
                    this._subscriptionId = this.MotiveGenericDataPanel.SubscriptionId;

                    /* Se ejecuta la acción por evento asociada a la ejecución del proceso */
                    FinancingController.Instance.ValProcessExecution(
                        this._subscriptionId.Value,
                        _productId);

                    this.SelectFinanPanel.LoadDebtToChangeCond(
                        this._subscriptionId.Value,
                        _productId,
                        this._financingId,
                        this._deferredId);
                }
            }
        }
      
        #endregion

        #region private variables
        /// <summary>
        /// Panel de datos genéricos del motivo
        /// </summary>
        public RequestRegisterControl MotiveGenericDataPanel;

        /// <summary>
        /// Panel de seleccion de financiaciones para cambio de condiciones
        /// </summary>
        private SelectFinanPanel SelectFinanPanel;

        private OpenHeaderTitles header;

        #endregion private variables

        #region Métodos
        


        #endregion      
        
        public LDSAC(Int64 productId, OpenHeaderTitles header)
        {
            
            InitializeComponent();
            this._productId = productId;
            this.LoadApplication(header);
            
        }

        private void LoadApplication(OpenHeaderTitles header)
        {
            this.header = new OpenHeaderTitles();            

            /* Configura el encabezado */
            if (header.ParsedHeaderTitle != null)
            {
                this.header.HeaderTitle = header.ParsedHeaderTitle;
            }
            if (header.ParsedHeaderSubtitle1 != null)
            {
                this.header.HeaderSubtitle1 = header.ParsedHeaderSubtitle1;
            }
            if (header.ParsedHeaderSubtitle2 != null)
            {
                this.header.HeaderSubtitle2 = header.ParsedHeaderSubtitle2;
            }
            if (header.RowInformationHeader != null)
            {
                this.header.RowInformationHeader = header.RowInformationHeader;
            }

            this.Text = "LDSAC - Abono a capital";

            MotiveGenericDataPanel = new RequestRegisterControl();
            MotiveGenericDataPanel.TagName = "P_ABONO_A_CAPITAL_100364";//"P_CHANGE_COND_DEBT"; //FinanBLConstants.CHANGE_COND_PACK_TYPE_TAG_NAME;
            MotiveGenericDataPanel.MotiveTagName = "M_ABONO_A_CAPITAL_100346";//"M_DEBT_NEGOTIATION"; //FinanBLConstants.CHANGE_COND_PROD_MOTI_TAG_NAME;
            /*Valores quemados de prueba*/
                        
            this._subscriptionId = this.GetSuscProd(Convert.ToInt64(this._productId));
            this.MotiveGenericDataPanel.SubscriptionId = this._subscriptionId;

            SelectFinanPanel = new SelectFinanPanel();
            SelectFinanPanel.ProgramName = "LDSAC"; 
            SelectFinanPanel.txtSubscriptionId.TextBoxValue = Convert.ToString(this._subscriptionId);
            this.SelectFinanPanel.LoadDebtToChangeCond(
                        this._subscriptionId.Value,
                        _productId,
                        this._financingId,
                        this._deferredId);

            this.BaseEntity = "CONTRACT";
            this.NodeLevelValue = "603046"; // nodeLevelValue;

            this.ChConditionsHeadPanel.Controls.Add(this.header);

            /* Define el panel donde se mostraran los paneles de navegación */
            this.chCondButtonsPanel.WorkPanel = this.chCondWorkPanel;

            /* Define el primer panel de navegación. Automáticamente se convierte en el panel a visualizar */
            this.chCondButtonsPanel.PanelCollection.Add(this.MotiveGenericDataPanel);

            /* Define segundo panel de navegación */
            this.chCondButtonsPanel.PanelCollection.Add(this.SelectFinanPanel);

            this.chCondButtonsPanel.SetMaxPanel(this.SelectFinanPanel);
            

            /* Visualiza el panel de trabajo actual */
            this.chCondButtonsPanel.ShowCurrentPanel();
        }

        private void LDSAC_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            try
            {               
                /* Inicializaciones de financiaciones a nivel de BD */
                DataAccessLDSAC.InitializeFinancing(this._subscriptionId.Value); //this.SubscriptionId.Value);

                /* Se ejecutan validaciones previas al proceso */
                //previousValidations();

                /* Se establecen los manejadores de los eventos asociados a los botones del panel de navegación */
                this.chCondButtonsPanel.preClickedButton += new OpenSteepBarButtonsPanel.OpenBarButtonPanelPreClickHandler(preBarButtonClick);
                this.chCondButtonsPanel.postClickedButton += new OpenSteepBarButtonsPanel.OpenBarButtonPanelPostClickHandler(postBarButtonClick);
                
                /* Se posiciona la forma en el centro de la pantalla */
                this.CenterToScreen();

            }
            catch (Exception ex)
            {
                this.Close();                
                throw ex;
            }

        }

        /// <summary>
        /// Método que se ejecuta justo antes de cambiar el panel cuando ocurre intento de navegación
        /// desde la barra de botones.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preBarButtonClick(object sender, CancelEventArgs e)
        {
            if (this.chCondButtonsPanel.LastButtonPressed == OpenSteepBarButtonsPanel.BarButtonType.NextButton)
            {
                this.ValidationOfRequiredFields();
                if (this.chCondButtonsPanel.CurrentPanel.GetType() == SelectFinanPanel.GetType())
                {
                    if (SelectFinanPanel.SelectedProducts.Count == 0 || this.SelectFinanPanel.ValToChangeConditions == 0)
                    {
                        /* Error: La suma del valor de los diferidos seleccionados debe ser mayor que cero */
                        ExceptionHandler.Raise(FinancingConstants.DeferredNotSelected);                        
                    }
                    if (this.SelectFinanPanel.DeferredSelChanged)
                    {
                        /* Se establece la deuda diferida sobre la cual se realizará el cambio de condiciones */
                        //this.SelectFinanPanel.SetDebtToChangeCond();
                        //this.InitializeFinancingConditions();

                        /* Inicializa el detector de cambio en la selección de diferidos */
                        //this.SelectFinanPanel.DeferredSelChanged = false;
                        
                        //this.Close();
                    }
                    ExecuteAbonoCapital();
                    
                }

            }
        }

        /// <summary>
        /// Método que se ejecuta cuando se presiona alguno de los botones de la barra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void postBarButtonClick(object sender, EventArgs e)
        {
            this.CenterToScreen();

            //Cancel desde cualquier ventana de navegación
            if (this.chCondButtonsPanel.LastButtonPressed == OpenSteepBarButtonsPanel.BarButtonType.CancelButton)
            {
                this.successfulOperation = false;

                /* Cierra la forma */
                this.Close();
            }
            if (this.chCondButtonsPanel.LastButtonPressed == OpenSteepBarButtonsPanel.BarButtonType.NextButton)
            {
                if (this.chCondButtonsPanel.PreviousPanel.GetType() == MotiveGenericDataPanel.GetType()
                    && this.chCondButtonsPanel.CurrentPanel.GetType() == SelectFinanPanel.GetType())
                {
                    this.SelectFinanPanel.txtRequestPersonName.TextBoxValue = this.MotiveGenericDataPanel.RequestInstance.ContactName;
                    
                }
                if (this.chCondButtonsPanel.PreviousPanel.GetType() == SelectFinanPanel.GetType()
                    && this.chCondButtonsPanel.CurrentPanel.GetType() == SelectFinanPanel.GetType())
                {

                    //Ejecuta la financiación.
                    //ExecuteChangeConditions();                   
                    //this.Close();
                }
            }

        }
        /// <summary>
        /// Validaciones requeridas antes de iniciar el proceso de cambio de condiciones
        /// </summary>
        private void previousValidations()
        {
            //Ejecuta las validaciones configuradas en los atributos del tipo de solicitud 'Financiación de Deuda'
            /*Statement.ExecFinanValidations
            (
                this.BaseEntity,
                this.NodeLevelValue
            );*/
        }
        private void LDSACNavigator_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Limpia las tablas de memoria asociadas al proceso de financiación */
            //Statement.ClearMemoryFinancing();
            return;
            /* Limpia las tablas de memoria asociadas a cuotas extras */
            //Statement.ClearExtraPayment();
        }
        private void ValidationOfRequiredFields()
        {
            if (this.chCondButtonsPanel.CurrentPanel is OpenSteepPanel)
            {
                OpenSteepPanel.ValidateRequiredFields((object)this.chCondButtonsPanel.CurrentPanel);
            }
            else if (this.chCondButtonsPanel.CurrentPanel is RequestRegisterControl)
            {
                (this.chCondButtonsPanel.CurrentPanel as RequestRegisterControl).ValidateData();
            }
        }

       

        #region Metodos
        private void ExecuteAbonoCapital()
        {
            this.SelectFinanPanel.ExecuteAbonoCapital();
        }

        private Int64 GetSuscProd(Int64 productId)
        {
            Int64 subscriptionId;
            DAL.DataAccessLDSAC.GetSuscProd(productId, out subscriptionId);

            return subscriptionId;
        }
        #endregion
    }
}
