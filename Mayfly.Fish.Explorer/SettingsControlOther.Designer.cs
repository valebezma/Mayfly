namespace Mayfly.Fish.Explorer
{
    partial class SettingsControlOther
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
            this.comboBoxAlk = new System.Windows.Forms.ComboBox();
            this.AlkTypeLabel = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSizeInterval = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxAlk
            // 
            this.comboBoxAlk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAlk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlk.FormattingEnabled = true;
            this.comboBoxAlk.Items.AddRange(new object[] {
            "Raw (more collections needed)",
            "Smooth (bio or sufficient data needed)"});
            this.comboBoxAlk.Location = new System.Drawing.Point(202, 89);
            this.comboBoxAlk.Name = "comboBoxAlk";
            this.comboBoxAlk.Size = new System.Drawing.Size(150, 21);
            this.comboBoxAlk.TabIndex = 10;
            // 
            // AlkTypeLabel
            // 
            this.AlkTypeLabel.AutoSize = true;
            this.AlkTypeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AlkTypeLabel.Location = new System.Drawing.Point(48, 92);
            this.AlkTypeLabel.Name = "AlkTypeLabel";
            this.AlkTypeLabel.Size = new System.Drawing.Size(116, 13);
            this.AlkTypeLabel.TabIndex = 8;
            this.AlkTypeLabel.Text = "Length-to-age key type";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownInterval.Location = new System.Drawing.Point(277, 63);
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownInterval.TabIndex = 12;
            this.numericUpDownInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSizeInterval
            // 
            this.labelSizeInterval.AutoSize = true;
            this.labelSizeInterval.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSizeInterval.Location = new System.Drawing.Point(48, 65);
            this.labelSizeInterval.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelSizeInterval.Name = "labelSizeInterval";
            this.labelSizeInterval.Size = new System.Drawing.Size(192, 13);
            this.labelSizeInterval.TabIndex = 11;
            this.labelSizeInterval.Text = "Size interval for length composition, mm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(28, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Assessment parameters";
            // 
            // SettingsControlOther
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxAlk);
            this.Controls.Add(this.AlkTypeLabel);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.labelSizeInterval);
            this.Controls.Add(this.label4);
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "SettingsControlOther";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 200);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAlk;
        private System.Windows.Forms.Label AlkTypeLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelSizeInterval;
        private System.Windows.Forms.Label label4;
    }
}
