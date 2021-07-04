namespace Mayfly.Bacterioplankton
{
    partial class ObservedCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObservedCalc));
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownArea = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFields = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFields)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numericUpDownArea
            // 
            resources.ApplyResources(this.numericUpDownArea, "numericUpDownArea");
            this.numericUpDownArea.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownArea.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownArea.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownArea.Name = "numericUpDownArea";
            this.numericUpDownArea.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownArea.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownFields
            // 
            resources.ApplyResources(this.numericUpDownFields, "numericUpDownFields");
            this.numericUpDownFields.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFields.Name = "numericUpDownFields";
            this.numericUpDownFields.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFields.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxValue
            // 
            resources.ApplyResources(this.textBoxValue, "textBoxValue");
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            // 
            // ObservedCalc
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownFields);
            this.Controls.Add(this.numericUpDownArea);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObservedCalc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownArea;
        private System.Windows.Forms.NumericUpDown numericUpDownFields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxValue;
    }
}