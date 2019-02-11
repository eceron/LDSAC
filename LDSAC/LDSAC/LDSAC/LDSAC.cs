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

                    /* Se establece la suscripción en el panel de condiciones de financiación */
                    this.DebtConditionsPanel.SubscriptionId = this._subscriptionId;

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

                    /* Se establece el producto en el panel de condiciones de financiación */
                    this.DebtConditionsPanel.ProductId = this._productId;

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

                    /* Se establece la suscripción en el panel de condiciones de financiación */
                    this.DebtConditionsPanel.SubscriptionId = _subscriptionId;

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

                    /* Se establece el producto en el panel de condiciones de financiación */
                    this.DebtConditionsPanel.ProductId = _productId;

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
        /// Panel de condiciones de financiación
        /// </summary>
        private DebtConditionPanel DebtConditionsPanel;

        /// <summary>
        /// Panel de datos de pagaré
        /// </summary> 
        private WarrantyDocumentPanel WarrantyDocumentPanel;

        /// <summary>
        /// Panel de seleccion de financiaciones para cambio de condiciones
        /// </summary>
        private SelectFinanPanel SelectFinanPanel;


        private OpenHeaderTitles header;

        /// <summary>
        /// Variable para almacenar si se imprime o no el pagare.
        /// </summary>
        String printWarrantyDoc = string.Empty;

        #endregion private variables

        #region Métodos
        private void InitializeFinancingConditions()
        {
            /* Forza a inicializar condiciones de financiación */
            this.DebtConditionsPanel.InitializeFinancingConditions();

            /* Define valores de referencia en el componente de condiciones de financiación */
            this.InitOtherFinancingValues();
        }


        /// <summary>
        /// Define valores de referencia en el componente de condiciones de financiacion
        /// </summary>
        private void InitOtherFinancingValues()
        {
            /* Asigna el valor mínimo a pagar y el valor a financiar */
            this.DebtConditionsPanel.MinimumValueToPay = 0;
            this.DebtConditionsPanel.FinSelectedValue = Convert.ToDouble(SelectFinanPanel.ValToChangeConditions);
            this.DebtConditionsPanel.ValueToFinancing = Convert.ToDouble(SelectFinanPanel.ValToChangeConditions);
            this.DebtConditionsPanel.ValueToPay = 0;
        }


        /// <summary>
        /// Ejecuta cambio de condiciones
        /// </summary>
        private void ExecuteChangeConditions()
        {
            Decimal quoteAccum;
            Decimal pendingBalance;
            Decimal capitalAccum;
            Decimal extraQuoteAccum;
            Decimal interestAccum;
            Int64? financingId = null;

            Decimal totalToFinance;

            Boolean waitBySign;
            Boolean waitByPayment;
            Boolean taxesOneQuote;

            Cursor currentCursor = this.Cursor;

            Int64? nuWarrantyDoc = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                /* Se ejecuta el proceso de cambio de condiciones */
                FinancingController.Instance.ExecDebtFinancing
                (
                    this.DebtConditionsPanel.FinConditions.FinancingPlanId.Value,
                    this.DebtConditionsPanel.FinConditions.ComputeMethod.Value,
                    this.DebtConditionsPanel.FinConditions.FirstDateToPay.Value,
                    this.DebtConditionsPanel.FinConditions.InterestOfFinacing.Value,
                    this.DebtConditionsPanel.FinConditions.Spread.Value,
                    this.DebtConditionsPanel.FinConditions.QuotasNumber.Value,
                    "RF",
                    this.DebtConditionsPanel.FinConditions.PercentToFinancing.Value,
                    this.DebtConditionsPanel.FinConditions.ValueToPay.Value,
                    this.DebtConditionsPanel.FinConditions.TaxOneQuota,
                    this.DebtConditionsPanel.Programname,
                    false,
                    true,
                    ref financingId,
                    out quoteAccum,
                    out pendingBalance,
                    out capitalAccum,
                    out extraQuoteAccum,
                    out interestAccum,
                    out waitBySign,
                    out waitByPayment
                );

                /* Establece el valor del flag que indica si los impuestos serán financiados a una sola cuota */
                taxesOneQuote = (DebtConditionsPanel.FinConditions.TaxOneQuota == FinanBLConstants.SI);

                /* Se calcula el valor total a financiar a partir de los acumulados */
                totalToFinance = capitalAccum + extraQuoteAccum;

                /* Saldo actual : %s1. Condiciones de financiación : Cuota Inicial: %s2, Valor Cuota: %s3. Financiar con estas condiciones? */
                DialogResult drProcessConfirmation = ExceptionHandler.DisplayMessage(117523, //FinancingConstants.AcceptChConditions,
                    new String[] { totalToFinance.ToString(), quoteAccum.ToString() },
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (drProcessConfirmation == DialogResult.Yes)
                {
                    /* Deshace la transaccion para procesar mediante espera pago */
                    FinancingController.Instance.RollbackTransaction();

                    /* Registra los datos de la solicitud */
                    this.MotiveGenericDataPanel.RegisterRequestData(false);

                    foreach (DictionaryEntry de in this.DebtConditionsPanel.SelectedProducts)
                    {
                        //Registra o actualiza el rol del solicitante
                        this.MotiveGenericDataPanel.UpdProdRole((Int64)de.Key);
                    }

                    /* Se almacenan los datos de la solicitud de financiación */
                    FinancingController.Instance.SaveFinancingRequest(
                        this.MotiveGenericDataPanel.RequestInstance.RequestId.Value,
                        RequestType.CHANGE_CONDITIONS,
                        this.MotiveGenericDataPanel.SubscriptionId.Value,
                        financingId.Value,
                        DebtConditionsPanel.FinConditions.FinancingPlanId.Value,
                        DebtConditionsPanel.FinConditions.ComputeMethod.Value,
                        DebtConditionsPanel.FinConditions.InterestRateId.Value,
                        DebtConditionsPanel.FinConditions.FirstDateToPay.Value,
                        DebtConditionsPanel.FinConditions.ValueToPay.Value,
                        DebtConditionsPanel.FinConditions.PercentToFinancing.Value,
                        DebtConditionsPanel.FinConditions.InterestOfFinacing.Value,
                        DebtConditionsPanel.FinConditions.Spread.Value,
                        DebtConditionsPanel.FinConditions.QuotasNumber.Value,
                        (Double)quoteAccum,
                        taxesOneQuote,
                        "RF",
                        waitBySign,
                        waitByPayment,
                        this.MotiveGenericDataPanel.RequestInstance.EmployeeId.Value,
                        DebtConditionsPanel.Programname,
                        FinanBLConstants.NO,
                        this.FinancingId);

                    /* Registra los datos del pagaré de financiación solo si el plan de financiación
                     * requiere pagaré */
                    if (DebtConditionsPanel.MandatoryWarrantyDocument)
                    {
                        nuWarrantyDoc = this.WarrantyDocumentPanel.RegisterWarrDoc(
                             (Double)interestAccum,
                             financingId,
                             (Double)totalToFinance,
                             (Double)quoteAccum,
                             this.DebtConditionsPanel.FinConditions.QuotasNumber.Value,
                             this.DebtConditionsPanel.FinConditions.InterestOfFinacing.Value);
                    }

                    /* Obtiene el valor del parámetro que indica si se imprime o no el pagaré */
                    printWarrantyDoc = Parameter.prm.GetParameterString("FA_IMPRIME_PAGARE"); //FinancingConstants.PrintWarrantyDoc);

                    /* Imprime pagaré si es requerido */
                    /*if (DebtConditionsPanel.MandatoryWarrantyDocument && printWarrantyDoc != null
                                && printWarrantyDoc.Equals("S") && financingId.HasValue)
                    {
                        PromissoryNoteUtil.PrintPromNote(nuWarrantyDoc.Value, this.WarrantyDocumentPanel.PrintingFormatCode);
                    }*/

                    // Indica que la solicitud se registro exitosamente
                    this.successfulOperation = true;

                    /* Se despliega un mensaje indicando que el proceso terminó con éxito */
                    ExceptionHandler.DisplayMessage(ErrorMessages.SUCCESS_OPERATION_MSG, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /* Se realiza persistencia en la base de datos */
                    FinancingController.Instance.CommitTransaction();

                    /* Se cierra la forma */
                    this.Close();
                }
                else
                {
                    /* Se deshacen los cambios realizados en la base de datos */
                    FinancingController.Instance.RollbackTransaction();
                }

                this.Cursor = currentCursor;
            }
            catch (Exception ex)
            {
                /* Se deshacen los cambios realizados en la base de datos */
                FinancingController.Instance.RollbackTransaction();
                this.Cursor = currentCursor;
                throw ex;
            }
        }
        #endregion      
        
        public LDSAC()
        {
            InitializeComponent();

            this.LoadApplication();
            
        }

        private void LoadApplication()
        {
            this.header = new OpenHeaderTitles();

            this.header.HeaderTitle = "Abono a capital";

            this.header.HeaderSubtitle1 = "HeaderSubtitle1 eceron";

            this.header.HeaderSubtitle2 = "HeaderSubtitle2 eceron";

            //this.header.RowInformationHeader = "header.RowInformationHeader";

            this.Text = "LDSAC - Abono a capital";

            MotiveGenericDataPanel = new RequestRegisterControl();
            MotiveGenericDataPanel.TagName = "P_ABONO_A_CAPITAL_100364";//"P_CHANGE_COND_DEBT"; //FinanBLConstants.CHANGE_COND_PACK_TYPE_TAG_NAME;
            MotiveGenericDataPanel.MotiveTagName = "M_ABONO_A_CAPITAL_100346";//"M_DEBT_NEGOTIATION"; //FinanBLConstants.CHANGE_COND_PROD_MOTI_TAG_NAME;
            /*Valores quemados de prueba*/

            
            /*this.MotiveGenericDataPanel.RequestInstance.EmployeeName = "Eduardo Cerón";
            this.MotiveGenericDataPanel.RequestInstance.EmployeeId = 239232;
            //this.MotiveGenericDataPanel.RequestInstance.Comment = "Esto es una prueba";
            this.MotiveGenericDataPanel.RequestInstance.RequestId = 24343;
            this.MotiveGenericDataPanel.RequestInstance.OrganizationalArea = "Area";
            this.MotiveGenericDataPanel.RequestInstance.OrganizationalAreaId = 999;
            this.MotiveGenericDataPanel.RequestInstance.AddressId = 223223;
            //this.MotiveGenericDataPanel.RequestInstance.ReceptionTypeId = 55555;
            this.MotiveGenericDataPanel.RequestInstance.ContactId = 334342;
            this.MotiveGenericDataPanel.RequestInstance.RegisterDate = Convert.ToDateTime("11/02/2019");*/
            this._subscriptionId = 4659;
            //this._productId = 4659;
            //this._financingId = 3634435;
            this.MotiveGenericDataPanel.SubscriptionId = this._subscriptionId;

            SelectFinanPanel = new SelectFinanPanel();
            SelectFinanPanel.ProgramName = "LDSAC"; // FinancingConstants.ChConditionsAppName;  
            SelectFinanPanel.txtSubscriptionId.TextBoxValue = Convert.ToString(this._subscriptionId);
            this.SelectFinanPanel.LoadDebtToChangeCond(
                        this._subscriptionId.Value,
                        _productId,
                        this._financingId,
                        this._deferredId);
            
            DebtConditionsPanel = new DebtConditionPanel();
            /*Debe ir comentado *///DebtConditionsPanel.FinConditions.DocumentSupportRequired = false;
            //DebtConditionsPanel.FinConditions.ShowDocumentSupport = false;
            //DebtConditionsPanel.FinConditions.ShowPercentToFinancing = false;
            //DebtConditionsPanel.FinConditions.ShowchkIVAOneQuote = false;
            //DebtConditionsPanel.FinConditions.ShowMinimunValueToPay = false;/*Hasta aquí*/
            //DebtConditionsPanel.FinConditions.Programa = "LDSAC"; // SelectFinanPanel.ProgramName;
            DebtConditionsPanel.Programname = "LDSAC"; //DebtConditionsPanel.FinConditions.Programa;

            WarrantyDocumentPanel = new WarrantyDocumentPanel();

            this.BaseEntity = "CONTRACT";
            this.NodeLevelValue = "603046"; // nodeLevelValue;

           // this.ProductId = Convert.ToInt64(this.NodeLevelValue);

            /* Asigna el header */

            /*if (this.header.HeaderTitle != null)
            {
                MessageBox.Show("Debe seleccionar un archivo." + this.header.HeaderTitle);
            }*/

            this.ChConditionsHeadPanel.Controls.Add(this.header);

            /* Define el panel donde se mostraran los paneles de navegación */
            this.chCondButtonsPanel.WorkPanel = this.chCondWorkPanel;

            /* Define el primer panel de navegación. Automáticamente se convierte en el panel a visualizar */
            this.chCondButtonsPanel.PanelCollection.Add(this.MotiveGenericDataPanel);

            /* Define segundo panel de navegación */
            this.chCondButtonsPanel.PanelCollection.Add(this.SelectFinanPanel);

            /* Define el tercer panel de navegación */
            //this.chCondButtonsPanel.PanelCollection.Add(this.DebtConditionsPanel);

            /* Define el cuarto panel de navegación */
            //this.chCondButtonsPanel.PanelCollection.Add(this.WarrantyDocumentPanel);

            /* Establece el maximo panel de navegacion por defecto */
            //this.chCondButtonsPanel.SetMaxPanel(this.WarrantyDocumentPanel);
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
                /* Se establece el identificador del cliente titutar de la deuda a financiar en el panel de
                 * pagaré */
                this.WarrantyDocumentPanel.SubscriberId = this.MotiveGenericDataPanel.SubscriberId;

                /* Inicializaciones de financiaciones a nivel de BD */
                DataAccessLDSAC.InitializeFinancing(this._subscriptionId.Value); //this.SubscriptionId.Value);

                /* Se ejecutan validaciones previas al proceso */
                //previousValidations();

                /* Se establecen los manejadores de los eventos asociados a los botones del panel de navegación */
                this.chCondButtonsPanel.preClickedButton += new OpenSteepBarButtonsPanel.OpenBarButtonPanelPreClickHandler(preBarButtonClick);
                this.chCondButtonsPanel.postClickedButton += new OpenSteepBarButtonsPanel.OpenBarButtonPanelPostClickHandler(postBarButtonClick);

                /* Se establece el manejador del evento asociado al cambio en la selección del plan de financiación */
                //this.DebtConditionsPanel.ChangePlanAction += new OpenFinancingConditions.ChangePlanActionDelegate(ChangePlanAction);

                /* Se establece el manejador del evento asociado al cambio del valor a pagar en las condiciones de financiación */
                //this.DebtConditionsPanel.ValueToPayChanged += new EventHandler(DebtConditionsPanel_ValueToPayChanged);

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
                    MessageBox.Show("Finalización trámite preButton", "Mensaje Alerta");
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
                    MessageBox.Show("Finalización trámite PostButton", "Mensaje Alerta");
                    //this.Close();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LoadApplication();

            //this.LDSAC_Load(sender, e);
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
    }
}
