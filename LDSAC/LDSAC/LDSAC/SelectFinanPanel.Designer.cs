namespace LDSAC
{
    partial class SelectFinanPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFinanPanel));
            this.txtRequestPersonName = new OpenSystems.Windows.Controls.OpenSimpleTextBox();
            this._pnlHeader = new System.Windows.Forms.Panel();
            this.txtFinancingId = new OpenSystems.Windows.Controls.OpenSimpleTextBox();
            this.txtSubscriptionId = new OpenSystems.Windows.Controls.OpenSimpleTextBox();
            this._pnlContent = new System.Windows.Forms.Panel();
            this._pnlDebtSelection = new OpenSystems.Financing.UI.Controls.OpenDebtSelectionPanel();
            this._pnlNegotBaseTitle = new System.Windows.Forms.Panel();
            this._title = new OpenSystems.Windows.Controls.OpenTitle();
            this._pnlNegotiationBase = new System.Windows.Forms.Panel();
            this.txtBalToChangeCond = new OpenSystems.Windows.Controls.OpenSimpleTextBox();
            this._pnlHeader.SuspendLayout();
            this._pnlContent.SuspendLayout();
            this._pnlNegotBaseTitle.SuspendLayout();
            this._pnlNegotiationBase.SuspendLayout();
            this.SuspendLayout();

            // 
            // txtRequestPersonName
            // 
            this.txtRequestPersonName.Caption = "Solicitante";
            this.txtRequestPersonName.CaptionFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestPersonName.DateTimeFormatMask = OpenSystems.Windows.Controls.DateTimeFormatMasks.ShorDate;
            this.txtRequestPersonName.ReadOnly = true;
            //this.txtRequestPersonName.NumberType = Infragistics.Win.UltraWinEditors.NumericType.Integer;
            this.txtRequestPersonName.Length = null;
            this.txtRequestPersonName.TextBoxValue = "";
            this.txtRequestPersonName.Location = new System.Drawing.Point(546, 6);
            this.txtRequestPersonName.Name = "txtRequestPersonName";
            this.txtRequestPersonName.Size = new System.Drawing.Size(250, 20);
            this.txtRequestPersonName.TabIndex = 5;
            this.txtRequestPersonName.TabStop = false;
            // 
            // _pnlHeader
            // 
            this._pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this._pnlHeader.Controls.Add(this.txtFinancingId);
            this._pnlHeader.Controls.Add(this.txtRequestPersonName);
            this._pnlHeader.Controls.Add(this.txtSubscriptionId);
            this._pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnlHeader.Location = new System.Drawing.Point(3, 3);
            this._pnlHeader.Name = "_pnlHeader";
            this._pnlHeader.Size = new System.Drawing.Size(871, 63);
            this._pnlHeader.TabIndex = 3;
            // 
            // txtFinancingId
            // 
            this.txtFinancingId.Caption = "Financiación";
            this.txtFinancingId.CaptionFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinancingId.DateTimeFormatMask = OpenSystems.Windows.Controls.DateTimeFormatMasks.ShorDate;
            this.txtFinancingId.ReadOnly = true;
            //this.txtFinancingId.NumberType = Infragistics.Win.UltraWinEditors.NumericType.Integer;
            this.txtFinancingId.Length = null;
            this.txtFinancingId.TextBoxValue = "";
            this.txtFinancingId.Location = new System.Drawing.Point(150, 32);
            this.txtFinancingId.Name = "txtFinancingId";
            this.txtFinancingId.Size = new System.Drawing.Size(250, 20);
            this.txtFinancingId.TabIndex = 9;
            this.txtFinancingId.TabStop = false;
            // 
            // txtSubscriptionId
            // 
            this.txtSubscriptionId.Caption = "Suscripción";
            this.txtSubscriptionId.CaptionFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriptionId.DateTimeFormatMask = OpenSystems.Windows.Controls.DateTimeFormatMasks.ShorDate;
            this.txtSubscriptionId.ReadOnly = true;
            //this.txtSubscriptionId.NumberType = Infragistics.Win.UltraWinEditors.NumericType.Integer;
            this.txtSubscriptionId.Length = null;
            this.txtSubscriptionId.TextBoxValue = "";
            this.txtSubscriptionId.Location = new System.Drawing.Point(150, 6);
            this.txtSubscriptionId.Name = "txtSubscriptionId";
            this.txtSubscriptionId.Size = new System.Drawing.Size(250, 20);
            this.txtSubscriptionId.TabIndex = 0;
            this.txtSubscriptionId.TabStop = false;
            // 
            // _pnlContent
            // 
            this._pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this._pnlContent.Controls.Add(this._pnlDebtSelection);
            this._pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlContent.Location = new System.Drawing.Point(3, 66);
            this._pnlContent.Name = "_pnlContent";
            this._pnlContent.Size = new System.Drawing.Size(871, 402);
            this._pnlContent.TabIndex = 7;
            // 
            // _pnlDebtSelection
            // 
            this._pnlDebtSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            //this._pnlDebtSelection.BaseProducts = ((System.Collections.Generic.List<OpenSystems.Financing.Entities.BaseProduct>)(resources.GetObject("_pnlDebtSelection.BaseProducts")));
            this._pnlDebtSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlDebtSelection.Font = new System.Drawing.Font("Verdana", 8F);
            this._pnlDebtSelection.Location = new System.Drawing.Point(0, 0);
            this._pnlDebtSelection.Margin = new System.Windows.Forms.Padding(0);
            this._pnlDebtSelection.Name = "_pnlDebtSelection";
            this._pnlDebtSelection.ProgramName = null;
            this._pnlDebtSelection.Size = new System.Drawing.Size(871, 402);
            this._pnlDebtSelection.TabIndex = 0;
            this._pnlDebtSelection.ProductSelectionChange += new OpenSystems.Financing.UI.Controls.OpenProductsSelectionPanel.ProductEventHandler(this._pnlDebtSelection_ProductSelectionChange);
            this._pnlDebtSelection.DeferredSelectionChange += new OpenSystems.Financing.UI.Controls.OpenDeferredsSelectionPanel.DeferredSelectionHandler(this._pnlDebtSelection_DeferredSelectionChange);
            // 
            // _pnlNegotBaseTitle
            // 
            this._pnlNegotBaseTitle.Controls.Add(this._title);
            this._pnlNegotBaseTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnlNegotBaseTitle.Location = new System.Drawing.Point(0, 0);
            this._pnlNegotBaseTitle.Name = "_pnlNegotBaseTitle";
            this._pnlNegotBaseTitle.Size = new System.Drawing.Size(871, 24);
            this._pnlNegotBaseTitle.TabIndex = 8;
            // 
            // _title
            // 
            this._title.BackColor = System.Drawing.Color.Transparent;
            this._title.Caption = "Base de Negociación";
            this._title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._title.Font = new System.Drawing.Font("Verdana", 8.25F);
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(871, 24);
            this._title.TabIndex = 0;
            // 
            // _pnlNegotiationBase
            // 
            this._pnlNegotiationBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this._pnlNegotiationBase.Controls.Add(this._pnlNegotBaseTitle);
            this._pnlNegotiationBase.Controls.Add(this.txtBalToChangeCond);
            this._pnlNegotiationBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pnlNegotiationBase.Location = new System.Drawing.Point(3, 468);
            this._pnlNegotiationBase.Name = "_pnlNegotiationBase";
            this._pnlNegotiationBase.Size = new System.Drawing.Size(871, 66);
            this._pnlNegotiationBase.TabIndex = 9;
            // 
            // txtBalToChangeCond
            // 
            this.txtBalToChangeCond.TypeBox = OpenSystems.Windows.Controls.TypesBox.Currency;
            this.txtBalToChangeCond.Caption = "A Financiar";
            this.txtBalToChangeCond.CaptionFont = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalToChangeCond.DateTimeFormatMask = OpenSystems.Windows.Controls.DateTimeFormatMasks.ShorDate;
            this.txtBalToChangeCond.ReadOnly = true;
            //this.txtBalToChangeCond.NumberType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.txtBalToChangeCond.NumberComposition = new OpenSystems.Windows.Controls.Number(15, 2);
            this.txtBalToChangeCond.Length = null;
            this.txtBalToChangeCond.TextBoxObjectValue = 0;
            this.txtBalToChangeCond.TextBoxValue = "0";
            this.txtBalToChangeCond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBalToChangeCond.Location = new System.Drawing.Point(662, 30);
            this.txtBalToChangeCond.Name = "txtBalToChangeCond";
            this.txtBalToChangeCond.Size = new System.Drawing.Size(194, 20);
            this.txtBalToChangeCond.TabIndex = 11;
            this.txtBalToChangeCond.TabStop = false;
            // 
            // OpenSelectFinanPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.Controls.Add(this._pnlContent);
            this.Controls.Add(this._pnlHeader);
            this.Controls.Add(this._pnlNegotiationBase);
            this.Name = "OpenSelectFinanPanel";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(877, 537);
            this._pnlHeader.ResumeLayout(false);
            this._pnlHeader.PerformLayout();
            this._pnlContent.ResumeLayout(false);
            this._pnlNegotBaseTitle.ResumeLayout(false);
            this._pnlNegotiationBase.ResumeLayout(false);
            this._pnlNegotiationBase.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel _pnlHeader;
        private OpenSystems.Windows.Controls.OpenSimpleTextBox txtSubscriptionId;
        internal OpenSystems.Windows.Controls.OpenSimpleTextBox txtRequestPersonName;
        private System.Windows.Forms.Panel _pnlContent;
        private OpenSystems.Windows.Controls.OpenSimpleTextBox txtFinancingId;
        private System.Windows.Forms.Panel _pnlNegotBaseTitle;
        private OpenSystems.Windows.Controls.OpenTitle _title;
        private System.Windows.Forms.Panel _pnlNegotiationBase;
        private OpenSystems.Windows.Controls.OpenSimpleTextBox txtBalToChangeCond;
        private OpenSystems.Financing.UI.Controls.OpenDebtSelectionPanel _pnlDebtSelection;
    }
}
