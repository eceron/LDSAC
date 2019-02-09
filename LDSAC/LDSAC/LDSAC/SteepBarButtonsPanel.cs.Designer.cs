namespace LDSAC
{
    partial class SteepBarButtonsPanel
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
            //components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            components = new System.ComponentModel.Container();
            this.dataUpdateManager = new OpenSystems.CustomerCare.DataUpdate.BL.DataUpdateManager(this.components);
            //Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            //appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(239)))));
            //appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(223)))), ((int)(((byte)(214)))));
            //this.cancelButton.Appearance = appearance1;
            this.cancelButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cancelButton.CausesValidation = false;
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelButton.Location = new System.Drawing.Point(721, 2);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 25);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancelar";
            //this.cancelButton.MouseLeave += new System.EventHandler(this.cancelButton_MouseLeave);
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            //this.cancelButton.MouseEnter += new System.EventHandler(this.cancelButton_MouseEnter);
            // 
            // nextButton
            // 
            //this.nextButton.Appearance = appearance1;
            this.nextButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.nextButton.Location = new System.Drawing.Point(631, 2);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(90, 25);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "&Siguiente";
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            //this.previousButton.Appearance = appearance1;
            this.previousButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.previousButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.previousButton.Location = new System.Drawing.Point(541, 2);
            this.previousButton.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(90, 25);
            this.previousButton.TabIndex = 2;
            this.previousButton.Text = "&Atrás";
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // OpenSteepBarButtonsPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(150)))), ((int)(((byte)(223)))));
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.cancelButton);
            this.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.Name = "OpenSteepBarButtonsPanel";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(813, 29);
            this.Load += new System.EventHandler(this.BarButtonsPanel_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button cancelButton;
        private OpenSystems.CustomerCare.DataUpdate.BL.DataUpdateManager dataUpdateManager;
    }
}
