namespace Mayfly.Wild
{
    partial class Calculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.numericUpDownTransfocatorMagnification = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTicks = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownTickValue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPrecision = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.domainUpDownUnits = new System.Windows.Forms.DomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTransfocatorMagnification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTickValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrecision)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownTransfocatorMagnification
            // 
            resources.ApplyResources(this.numericUpDownTransfocatorMagnification, "numericUpDownTransfocatorMagnification");
            this.numericUpDownTransfocatorMagnification.DecimalPlaces = 1;
            this.numericUpDownTransfocatorMagnification.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTransfocatorMagnification.Name = "numericUpDownTransfocatorMagnification";
            this.numericUpDownTransfocatorMagnification.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTransfocatorMagnification.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownTransfocatorMagnification.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDownTicks
            // 
            resources.ApplyResources(this.numericUpDownTicks, "numericUpDownTicks");
            this.numericUpDownTicks.Name = "numericUpDownTicks";
            this.numericUpDownTicks.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownTicks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDownTickValue
            // 
            resources.ApplyResources(this.numericUpDownTickValue, "numericUpDownTickValue");
            this.numericUpDownTickValue.DecimalPlaces = 1;
            this.numericUpDownTickValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTickValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTickValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownTickValue.Name = "numericUpDownTickValue";
            this.numericUpDownTickValue.ReadOnly = true;
            this.numericUpDownTickValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTickValue.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownTickValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxValue
            // 
            resources.ApplyResources(this.textBoxValue, "textBoxValue");
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numericUpDownPrecision
            // 
            resources.ApplyResources(this.numericUpDownPrecision, "numericUpDownPrecision");
            this.numericUpDownPrecision.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPrecision.Name = "numericUpDownPrecision";
            this.numericUpDownPrecision.ValueChanged += new System.EventHandler(this.numericUpDownPrecision_ValueChanged);
            this.numericUpDownPrecision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // domainUpDownUnits
            // 
            this.domainUpDownUnits.Items.Add(resources.GetString("domainUpDownUnits.Items"));
            this.domainUpDownUnits.Items.Add(resources.GetString("domainUpDownUnits.Items1"));
            this.domainUpDownUnits.Items.Add(resources.GetString("domainUpDownUnits.Items2"));
            this.domainUpDownUnits.Items.Add(resources.GetString("domainUpDownUnits.Items3"));
            this.domainUpDownUnits.Items.Add(resources.GetString("domainUpDownUnits.Items4"));
            resources.ApplyResources(this.domainUpDownUnits, "domainUpDownUnits");
            this.domainUpDownUnits.Name = "domainUpDownUnits";
            this.domainUpDownUnits.SelectedItemChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.domainUpDownUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // Calculator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTickValue);
            this.Controls.Add(this.numericUpDownTicks);
            this.Controls.Add(this.numericUpDownPrecision);
            this.Controls.Add(this.numericUpDownTransfocatorMagnification);
            this.Controls.Add(this.domainUpDownUnits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Calculator_FormClosing);
            this.Load += new System.EventHandler(this.Calculator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTransfocatorMagnification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTickValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrecision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownTransfocatorMagnification;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDownTicks;
        public System.Windows.Forms.NumericUpDown numericUpDownTickValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownPrecision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.DomainUpDown domainUpDownUnits;
    }
}