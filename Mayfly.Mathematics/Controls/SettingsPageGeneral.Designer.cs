namespace Mayfly.Mathematics.Controls
{
    partial class SettingsPageGeneral
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
            this.numericUpDownSL = new System.Windows.Forms.NumericUpDown();
            this.labelSL = new System.Windows.Forms.Label();
            this.numericUpDownStrongSize = new System.Windows.Forms.NumericUpDown();
            this.labelExpect = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelStrongSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrongSize)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownSL
            // 
            this.numericUpDownSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSL.DecimalPlaces = 3;
            this.numericUpDownSL.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownSL.Location = new System.Drawing.Point(277, 38);
            this.numericUpDownSL.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            196608});
            this.numericUpDownSL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownSL.Name = "numericUpDownSL";
            this.numericUpDownSL.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownSL.TabIndex = 2;
            this.numericUpDownSL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownSL.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // labelSL
            // 
            this.labelSL.AutoSize = true;
            this.labelSL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSL.Location = new System.Drawing.Point(42, 40);
            this.labelSL.Name = "labelSL";
            this.labelSL.Size = new System.Drawing.Size(125, 13);
            this.labelSL.TabIndex = 1;
            this.labelSL.Text = "Default significance level";
            // 
            // numericUpDownStrongSize
            // 
            this.numericUpDownStrongSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownStrongSize.Location = new System.Drawing.Point(277, 116);
            this.numericUpDownStrongSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStrongSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownStrongSize.Name = "numericUpDownStrongSize";
            this.numericUpDownStrongSize.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownStrongSize.TabIndex = 5;
            this.numericUpDownStrongSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownStrongSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelExpect
            // 
            this.labelExpect.AutoSize = true;
            this.labelExpect.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelExpect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelExpect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelExpect.Location = new System.Drawing.Point(25, 0);
            this.labelExpect.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.labelExpect.Name = "labelExpect";
            this.labelExpect.Size = new System.Drawing.Size(169, 15);
            this.labelExpect.TabIndex = 0;
            this.labelExpect.Text = "General statistical expectations";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSize.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSize.Location = new System.Drawing.Point(25, 78);
            this.labelSize.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(94, 15);
            this.labelSize.TabIndex = 3;
            this.labelSize.Text = "Common values";
            // 
            // labelStrongSize
            // 
            this.labelStrongSize.AutoSize = true;
            this.labelStrongSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelStrongSize.Location = new System.Drawing.Point(42, 118);
            this.labelStrongSize.Name = "labelStrongSize";
            this.labelStrongSize.Size = new System.Drawing.Size(99, 13);
            this.labelStrongSize.TabIndex = 4;
            this.labelStrongSize.Text = "Minimal sample size";
            // 
            // SettingsControlGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownSL);
            this.Controls.Add(this.labelSL);
            this.Controls.Add(this.numericUpDownStrongSize);
            this.Controls.Add(this.labelExpect);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelStrongSize);
            this.Group = "Statistics";
            this.Name = "SettingsControlGeneral";
            this.Section = "General";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrongSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownSL;
        private System.Windows.Forms.Label labelSL;
        private System.Windows.Forms.NumericUpDown numericUpDownStrongSize;
        private System.Windows.Forms.Label labelExpect;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelStrongSize;
    }
}
