namespace Mayfly.Wild
{
    partial class StratifiedSampleProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StratifiedSampleProperties));
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.domainUpDownInterval = new System.Windows.Forms.DomainUpDown();
            this.numericUpDownStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEnd = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.domainUpDownInterval);
            this.panel1.Controls.Add(this.numericUpDownStart);
            this.panel1.Controls.Add(this.numericUpDownEnd);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.labelInstruction);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Name = "panel1";
            // 
            // domainUpDownInterval
            // 
            resources.ApplyResources(this.domainUpDownInterval, "domainUpDownInterval");
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items1"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items2"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items3"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items4"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items5"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items6"));
            this.domainUpDownInterval.Items.Add(resources.GetString("domainUpDownInterval.Items7"));
            this.domainUpDownInterval.Name = "domainUpDownInterval";
            this.domainUpDownInterval.SelectedItemChanged += new System.EventHandler(this.domainUpDownInterval_SelectedItemChanged);
            this.domainUpDownInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.domainUpDownInterval_KeyPress);
            // 
            // numericUpDownStart
            // 
            resources.ApplyResources(this.numericUpDownStart, "numericUpDownStart");
            this.numericUpDownStart.DecimalPlaces = 1;
            this.numericUpDownStart.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStart.Name = "numericUpDownStart";
            this.numericUpDownStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStart.ValueChanged += new System.EventHandler(this.numeric_ValueChanged);
            // 
            // numericUpDownEnd
            // 
            resources.ApplyResources(this.numericUpDownEnd, "numericUpDownEnd");
            this.numericUpDownEnd.DecimalPlaces = 1;
            this.numericUpDownEnd.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownEnd.Name = "numericUpDownEnd";
            this.numericUpDownEnd.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownEnd.ValueChanged += new System.EventHandler(this.numeric_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelInstruction
            // 
            resources.ApplyResources(this.labelInstruction, "labelInstruction");
            this.labelInstruction.Name = "labelInstruction";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // StratifiedSampleProperties
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StratifiedSampleProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.StratifiedSampleProperties_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownStart;
        private System.Windows.Forms.NumericUpDown numericUpDownEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DomainUpDown domainUpDownInterval;
    }
}