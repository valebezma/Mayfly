namespace Mayfly.Fish
{
    partial class SettingsControlStratified
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
            this.labelInterval = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelStratified = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelInterval.Location = new System.Drawing.Point(45, 65);
            this.labelInterval.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(136, 13);
            this.labelInterval.TabIndex = 4;
            this.labelInterval.Text = "Default sample interval, mm";
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownInterval.Location = new System.Drawing.Point(277, 63);
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownInterval.TabIndex = 5;
            this.numericUpDownInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelStratified
            // 
            this.labelStratified.AutoSize = true;
            this.labelStratified.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStratified.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStratified.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelStratified.Location = new System.Drawing.Point(25, 25);
            this.labelStratified.Margin = new System.Windows.Forms.Padding(0, 0, 0, 25);
            this.labelStratified.Name = "labelStratified";
            this.labelStratified.Size = new System.Drawing.Size(163, 15);
            this.labelStratified.TabIndex = 3;
            this.labelStratified.Text = "Stratified sample initial values";
            // 
            // SettingsControlStratified
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.labelStratified);
            this.MinimumSize = new System.Drawing.Size(400, 150);
            this.Name = "SettingsControlStratified";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 150);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelStratified;
    }
}
