namespace LDSAC
{
    partial class LDSAC
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.chCondButtonsPanel = new OpenSystems.Financing.Windows.Controls.OpenSteepBarButtonsPanel();
            this.ChConditionsHeadPanel = new System.Windows.Forms.Panel();
            this.chCondWorkPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(660, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Debug";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChConditionsHeadPanel
            // 
            this.ChConditionsHeadPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ChConditionsHeadPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChConditionsHeadPanel.Location = new System.Drawing.Point(0, 23);
            this.ChConditionsHeadPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ChConditionsHeadPanel.MinimumSize = new System.Drawing.Size(300, 70);
            this.ChConditionsHeadPanel.Name = "ChConditionsHeadPanel";
            this.ChConditionsHeadPanel.Size = new System.Drawing.Size(660, 70);
            this.ChConditionsHeadPanel.TabIndex = 0;
            // 
            // chCondWorkPanel
            // 
            this.chCondWorkPanel.AutoSize = true;
            this.chCondWorkPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chCondWorkPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.chCondWorkPanel.Location = new System.Drawing.Point(0, 70);
            this.chCondWorkPanel.Margin = new System.Windows.Forms.Padding(0);
            this.chCondWorkPanel.Name = "chCondWorkPanel";
            this.chCondWorkPanel.Size = new System.Drawing.Size(0, 0);
            this.chCondWorkPanel.TabIndex = 3;
            // 
            // chCondButtonsPanel
            // 
            this.chCondButtonsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(150)))), ((int)(((byte)(223)))));
            this.chCondButtonsPanel.CurrentPanel = null;
            this.chCondButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chCondButtonsPanel.LastButtonPressed = OpenSystems.Financing.Windows.Controls.OpenSteepBarButtonsPanel.BarButtonType.NextButton;
            this.chCondButtonsPanel.Location = new System.Drawing.Point(0, 123);
            this.chCondButtonsPanel.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.chCondButtonsPanel.Name = "chCondButtonsPanel";
            this.chCondButtonsPanel.Padding = new System.Windows.Forms.Padding(2);
            this.chCondButtonsPanel.PreviousPanel = null;
            this.chCondButtonsPanel.Size = new System.Drawing.Size(792, 30);
            this.chCondButtonsPanel.TabIndex = 1;
            this.chCondButtonsPanel.WorkPanel = null;
            // 
            // LDSAC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(660, 273);
            this.Controls.Add(this.chCondWorkPanel);
            this.Controls.Add(this.ChConditionsHeadPanel);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 180);
            this.Name = "LDSAC";
            this.Text = "LDSAC - Abono a capital";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenSystems.Financing.Windows.Controls.OpenSteepBarButtonsPanel chCondButtonsPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel ChConditionsHeadPanel;
        private System.Windows.Forms.Panel chCondWorkPanel;
    }
}