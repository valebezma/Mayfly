namespace Mayfly.Mathematics.Statistics
{
    partial class TestProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestProperties));
            this.textBoxStatistic = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownAlpha = new System.Windows.Forms.NumericUpDown();
            this.textBoxFCrit = new System.Windows.Forms.TextBox();
            this.labelAlpha = new System.Windows.Forms.Label();
            this.labelFCrit = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxP = new System.Windows.Forms.TextBox();
            this.statChart1 = new Mayfly.Mathematics.Charts.Plot();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxStatistic
            // 
            resources.ApplyResources(this.textBoxStatistic, "textBoxStatistic");
            this.textBoxStatistic.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxStatistic.Name = "textBoxStatistic";
            this.textBoxStatistic.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDownAlpha
            // 
            resources.ApplyResources(this.numericUpDownAlpha, "numericUpDownAlpha");
            this.numericUpDownAlpha.DecimalPlaces = 3;
            this.numericUpDownAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAlpha.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownAlpha.Name = "numericUpDownAlpha";
            this.numericUpDownAlpha.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownAlpha.ValueChanged += new System.EventHandler(this.numericUpDownAlpha_ValueChanged);
            // 
            // textBoxFCrit
            // 
            resources.ApplyResources(this.textBoxFCrit, "textBoxFCrit");
            this.textBoxFCrit.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxFCrit.Name = "textBoxFCrit";
            this.textBoxFCrit.ReadOnly = true;
            // 
            // labelAlpha
            // 
            resources.ApplyResources(this.labelAlpha, "labelAlpha");
            this.labelAlpha.Name = "labelAlpha";
            // 
            // labelFCrit
            // 
            resources.ApplyResources(this.labelFCrit, "labelFCrit");
            this.labelFCrit.Name = "labelFCrit";
            // 
            // buttonCopy
            // 
            resources.ApplyResources(this.buttonCopy, "buttonCopy");
            this.buttonCopy.FlatAppearance.BorderSize = 0;
            this.buttonCopy.Image = global::Mayfly.Properties.Resources.Copy;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxP
            // 
            resources.ApplyResources(this.textBoxP, "textBoxP");
            this.textBoxP.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxP.Name = "textBoxP";
            this.textBoxP.ReadOnly = true;
            // 
            // statChart1
            // 
            resources.ApplyResources(this.statChart1, "statChart1");
            this.statChart1.AxisXAutoInterval = false;
            this.statChart1.AxisXAutoMaximum = false;
            this.statChart1.AxisXAutoMinimum = false;
            this.statChart1.AxisYAutoInterval = false;
            this.statChart1.AxisYAutoMaximum = false;
            this.statChart1.AxisYAutoMinimum = false;
            this.statChart1.Name = "statChart1";
            this.statChart1.ShowLegend = false;
            this.statChart1.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            this.statChart1.Click += new System.EventHandler(this.statChart1_Click);
            // 
            // TestProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.statChart1);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.numericUpDownAlpha);
            this.Controls.Add(this.textBoxP);
            this.Controls.Add(this.textBoxFCrit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelAlpha);
            this.Controls.Add(this.labelFCrit);
            this.Controls.Add(this.textBoxStatistic);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestProperties";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownAlpha;
        private System.Windows.Forms.Label labelAlpha;
        private System.Windows.Forms.Label labelFCrit;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label label4;
        private Charts.Plot statChart1;
        private System.Windows.Forms.TextBox textBoxStatistic;
        private System.Windows.Forms.TextBox textBoxFCrit;
        private System.Windows.Forms.TextBox textBoxP;
    }
}