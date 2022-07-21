namespace Mayfly.Species.Controls
{
    partial class SettingsControlPrint
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBoxCoupletChar = new System.Windows.Forms.TextBox();
            this.labelCoupletChar = new System.Windows.Forms.Label();
            this.radioButtonModern = new System.Windows.Forms.RadioButton();
            this.radioButtonClassic = new System.Windows.Forms.RadioButton();
            this.labelKeyReportView = new System.Windows.Forms.Label();
            this.labelPrintCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxCoupletChar
            // 
            this.textBoxCoupletChar.Location = new System.Drawing.Point(216, 59);
            this.textBoxCoupletChar.Name = "textBoxCoupletChar";
            this.textBoxCoupletChar.Size = new System.Drawing.Size(100, 20);
            this.textBoxCoupletChar.TabIndex = 21;
            this.textBoxCoupletChar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCoupletChar
            // 
            this.labelCoupletChar.AutoSize = true;
            this.labelCoupletChar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCoupletChar.Location = new System.Drawing.Point(45, 62);
            this.labelCoupletChar.Name = "labelCoupletChar";
            this.labelCoupletChar.Size = new System.Drawing.Size(78, 13);
            this.labelCoupletChar.TabIndex = 19;
            this.labelCoupletChar.Text = "Couplet symbol";
            // 
            // radioButtonModern
            // 
            this.radioButtonModern.AutoSize = true;
            this.radioButtonModern.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonModern.Location = new System.Drawing.Point(316, 38);
            this.radioButtonModern.Name = "radioButtonModern";
            this.radioButtonModern.Size = new System.Drawing.Size(61, 17);
            this.radioButtonModern.TabIndex = 22;
            this.radioButtonModern.Text = "Modern";
            this.radioButtonModern.UseVisualStyleBackColor = true;
            this.radioButtonModern.CheckedChanged += new System.EventHandler(this.radioButtonModern_CheckedChanged);
            // 
            // radioButtonClassic
            // 
            this.radioButtonClassic.AutoSize = true;
            this.radioButtonClassic.Checked = true;
            this.radioButtonClassic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonClassic.Location = new System.Drawing.Point(216, 38);
            this.radioButtonClassic.Name = "radioButtonClassic";
            this.radioButtonClassic.Size = new System.Drawing.Size(58, 17);
            this.radioButtonClassic.TabIndex = 20;
            this.radioButtonClassic.TabStop = true;
            this.radioButtonClassic.Text = "Classic";
            this.radioButtonClassic.UseVisualStyleBackColor = true;
            // 
            // labelKeyReportView
            // 
            this.labelKeyReportView.AutoSize = true;
            this.labelKeyReportView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelKeyReportView.Location = new System.Drawing.Point(45, 40);
            this.labelKeyReportView.Name = "labelKeyReportView";
            this.labelKeyReportView.Size = new System.Drawing.Size(84, 13);
            this.labelKeyReportView.TabIndex = 18;
            this.labelKeyReportView.Text = "Report key view";
            // 
            // labelPrintCaption
            // 
            this.labelPrintCaption.AutoSize = true;
            this.labelPrintCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPrintCaption.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPrintCaption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPrintCaption.Location = new System.Drawing.Point(28, 0);
            this.labelPrintCaption.Name = "labelPrintCaption";
            this.labelPrintCaption.Size = new System.Drawing.Size(144, 15);
            this.labelPrintCaption.TabIndex = 17;
            this.labelPrintCaption.Text = "Print and Preview Settings";
            // 
            // SettingsControlPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxCoupletChar);
            this.Controls.Add(this.labelCoupletChar);
            this.Controls.Add(this.radioButtonModern);
            this.Controls.Add(this.radioButtonClassic);
            this.Controls.Add(this.labelKeyReportView);
            this.Controls.Add(this.labelPrintCaption);
            this.Name = "SettingsControlPrint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCoupletChar;
        private System.Windows.Forms.Label labelCoupletChar;
        private System.Windows.Forms.RadioButton radioButtonModern;
        private System.Windows.Forms.RadioButton radioButtonClassic;
        private System.Windows.Forms.Label labelKeyReportView;
        private System.Windows.Forms.Label labelPrintCaption;
    }
}
