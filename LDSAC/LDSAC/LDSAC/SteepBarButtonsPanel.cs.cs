using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenSystems.Windows.Controls;
using OpenSystems.CustomerCare.DataUpdate;

namespace LDSAC
{
    public partial class SteepBarButtonsPanel : UserControl
    {
        #region atributos

        /* Atributos para boton de modificación de datos de cliente */
        private Boolean _UpdateDataClient = false;
        private Int32? _suscriptionId = null;
        private Int32? _productId = null;

        /* Atributos de la forma */
        private Boolean _stopChange = false;
        //private FinancingNavigator.FinancingNavigator _fnv = null;
        private BarButtonType _lastButtonPressed;
        private Control _currentPanel;
        private Control _previousPanel;
        private PanelsCollection _panelCollection = new PanelsCollection();
        private Panel _workPanel;

        #endregion

        #region propiedades

        /* Contrato cargado */
        public Int32? SuscriptionId
        {
            get { return _suscriptionId; }
            set { _suscriptionId = value; }
        }

        /* Producto cargado */
        public Int32? ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }


        /// <summary>
        /// Forma Padre
        /// </summary>
        /*public FinancingNavigator.FinancingNavigator Fnv
        {
            get { return _fnv; }
            set { _fnv = value; }
        }*/

        /// <summary>
        /// Verifica si pone el boton de modificación de datos del cliente
        /// </summary>
        public Boolean UpdateDataClient
        {
            get { return _UpdateDataClient; }
            set { _UpdateDataClient = value; }
        }

        /// <summary>
        /// Panel actual seleccionado en el area de trabajo
        /// </summary>
        public Control CurrentPanel
        {
            get { return _currentPanel; }
            set { _currentPanel = value; }
        }

        /// <summary>
        /// Panel anterior seleccionado en el area de trabajo
        /// </summary>
        public Control PreviousPanel
        {
            get { return _previousPanel; }
            set { _previousPanel = value; }
        }


        /// <summary>
        /// Panel contenedor para de controles (paneles) de navegación
        /// </summary>
        public Panel WorkPanel
        {
            get { return _workPanel; }
            set { _workPanel = value; }
        }

        /// <summary>
        /// Coleccion de paneles de navegación
        /// </summary>
        public PanelsCollection PanelCollection
        {
            get { return _panelCollection; }
        }

        /// <summary>
        /// Tipo del último botón presionado en la barra de botones
        /// </summary>
        public BarButtonType LastButtonPressed
        {
            get { return _lastButtonPressed; }
            set { _lastButtonPressed = value; }
        }


        #endregion

        public enum BarButtonType { NextButton, PreviousButton, CancelButton }

        private Control _maxPanel;
        
        public SteepBarButtonsPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Crear boton y adicionarlo
        /// </summary>
        /*private void ConfButtonDataUpdate()
        {
            if (this._UpdateDataClient)
            {
                OpenButton dataUpdateButton = dataUpdateManager.CreateButton(this);

                dataUpdateButton.Height = this.cancelButton.Height;
                dataUpdateButton.Width = this.cancelButton.Width;
                dataUpdateButton.Dock = this.cancelButton.Dock;
                dataUpdateButton.Appearance = this.cancelButton.Appearance;

                OpenSystems.CustomerCare.DataUpdate.BL.Utils.AddControlAt(this, dataUpdateButton, 0);
            }
        }*/

        // Se dispara cuando se presiona alguno de los botones de la barra 
        public delegate void OpenBarButtonPanelPreClickHandler(object sender, CancelEventArgs e);
        public event OpenBarButtonPanelPreClickHandler preClickedButton;

        public delegate void OpenBarButtonPanelPostClickHandler(object sender, EventArgs e);
        public event OpenBarButtonPanelPostClickHandler postClickedButton;

        /// <summary>
        /// Visualiza el panel actual dentro del workpanel
        /// </summary>
        public void ShowCurrentPanel()
        {
            if (this.DesignMode)
                return;

            ValidateWorkPanel();

            ValidatePanelCollection();

            //Define el primer panel de la colección como el current panel en caso de que el current 
            //panel aún no se haya definido
            if (_currentPanel == null)
            {
                _currentPanel = _panelCollection[0];
            }

            //Visualiza el panel current
            if (_workPanel.Controls.Count > 0)
            {
                _workPanel.Controls.RemoveAt(0);
            }

            _workPanel.Controls.Add(_currentPanel);

            //_currentPanel.Location = new System.Drawing.Point(0, 0);
            _currentPanel.TabIndex = 0;
        }

        /// <summary>
        /// Valida que exista al menos un panel de navegación
        /// </summary>
        private void ValidatePanelCollection()
        {
            if (this.DesignMode)
                return;

            if (_panelCollection.Count == 0)
            {
                throw new ArgumentException("Barra de botones de navegación no encontro panel en la colección OpenSteepPanelsCollection");
            }
        }

        /// <summary>
        /// Valida que exista el panel donde se mostraran los controles (paneles) de navegación
        /// </summary>
        private void ValidateWorkPanel()
        {
            if (this.DesignMode)
                return;

            if (_workPanel == null)
            {
                throw new ArgumentException("Barra de botones de navegación no encontro panel WorkPanel");
            }
        }

        private void BarButtonsPanel_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            //RefreshButtons();
            // Se establece el boton de modificación de datos del cliente
            //this.ConfButtonDataUpdate();

            // valida la modificación de datos del cliente
            if (this._UpdateDataClient)
            {
                // Se inicializa el boton de modificaciones de datos del cliente
                if (this._productId.HasValue)
                {
                    this.dataUpdateManager.InitApplicationByProduct(this, this._productId.Value);
                }
                else
                {
                    this.dataUpdateManager.InitApplicationBySubscription(this, this._suscriptionId.Value);
                }
            }
        }

        /// <summary>
        /// Actualiza la apariencia de los botones de navegación
        /// </summary>
        /*private void RefreshButtons()
        {
            if (this.DesignMode)
                return;

            ValidateWorkPanel();
            ValidatePanelCollection();

            //Define el primer panel de la colección como el current panel en caso de que el current 
            //panel aún no se haya definido
            if (_currentPanel == null)
            {
                _currentPanel = _panelCollection[0];
            }

            if (_panelCollection.IndexOf(_currentPanel) == 0)
            {
                this.previousButton.Visible = false;
            }
            else
            {
                this.previousButton.Visible = true;
            }

            //if (_panelCollection.IndexOf(_currentPanel) == (_panelCollection.Count - 1))
            if (_currentPanel == _maxPanel)
            {
                this.nextButton.Text = OpenSystems.Financing.Windows.Controls.Resources.OpenSteepBarButtonsPanel.LAST_BUTTON_CAPTION;
            }
            else
            {
                this.nextButton.Text = OpenSystems.Financing.Windows.Controls.Resources.OpenSteepBarButtonsPanel.NEXT_BUTTON_CAPTION;
            }
        }*/

        private void previousButton_Click(object sender, EventArgs e)
        {
            _lastButtonPressed = BarButtonType.PreviousButton;

            CancelEventArgs ce = new CancelEventArgs(false);
            raisepreClickedButton(ce);

            if (!ce.Cancel)
            {

                if (_panelCollection.IndexOf(_currentPanel) > 0)
                {
                    _previousPanel = _currentPanel;

                    _currentPanel = _panelCollection[_panelCollection.IndexOf(_currentPanel) - 1];

                    ShowCurrentPanel();

                    //RefreshButtons();
                }

                raisepostClickedButton();
            }

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            _lastButtonPressed = BarButtonType.NextButton;

            CancelEventArgs ce = new CancelEventArgs(false);
            raisepreClickedButton(ce);

            if (!ce.Cancel)
            {

                if (_panelCollection.Count > 0)
                {
                    _previousPanel = _currentPanel;

                    if (_panelCollection.IndexOf(_currentPanel) < (_panelCollection.Count - 1))
                    {
                        _currentPanel = _panelCollection[_panelCollection.IndexOf(_currentPanel) + 1];

                        ShowCurrentPanel();

                        //RefreshButtons();
                    }
                }
            }

            raisepostClickedButton();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this._stopChange = true;
            this._lastButtonPressed = BarButtonType.CancelButton;

            CancelEventArgs ce = new CancelEventArgs(false);

            raisepreClickedButton(ce);

            if (!ce.Cancel)
                raisepostClickedButton();
        }

        /// <summary>
        /// Ejecuta el delegado previo a los procesos que ocurren cuando un botón de la barra es presionado
        /// </summary>
        private void raisepreClickedButton(CancelEventArgs ce)
        {
            if (preClickedButton != null)
                preClickedButton(this, ce);
        }

        /// <summary>
        /// Ejecuta el delegado posterior a los procesos que ocurren cuando un botón de la barra es presionado
        /// </summary>
        /// 
        private void raisepostClickedButton()
        {
            if (postClickedButton != null)
                postClickedButton(this, new EventArgs());
        }

        /// <summary>
        /// Establece como máximo panel de avance, el panel actual
        /// </summary>
        public void SetMaxPanel(Control panel)
        {
            _maxPanel = panel;

            //RefreshButtons();
        }

    }
}
