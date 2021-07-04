namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardMSYR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardMSYR));
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageGrowth = new AeroWizard.WizardPage();
            this.buttonGrowth = new System.Windows.Forms.Button();
            this.textBoxT0 = new Mayfly.Controls.NumberBox();
            this.labelK = new System.Windows.Forms.Label();
            this.textBoxK = new Mayfly.Controls.NumberBox();
            this.labelW = new System.Windows.Forms.Label();
            this.textBoxW = new Mayfly.Controls.NumberBox();
            this.labelT0 = new System.Windows.Forms.Label();
            this.labelCategoryInstruction = new System.Windows.Forms.Label();
            this.pageAges = new AeroWizard.WizardPage();
            this.numericUpDownM = new System.Windows.Forms.NumericUpDown();
            this.labelFi = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonMortality = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTc = new Mayfly.Controls.NumberBox();
            this.textBoxTr = new Mayfly.Controls.NumberBox();
            this.pageChart = new AeroWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.plotYR = new Mayfly.Mathematics.Charts.Plot();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxGrowth = new System.Windows.Forms.CheckBox();
            this.checkBoxYR = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.pictureBoxWarn = new System.Windows.Forms.PictureBox();
            this.labelWarn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageGrowth.SuspendLayout();
            this.pageAges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).BeginInit();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotYR)).BeginInit();
            this.pageReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarn)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageGrowth);
            this.wizardExplorer.Pages.Add(this.pageAges);
            this.wizardExplorer.Pages.Add(this.pageChart);
            this.wizardExplorer.Pages.Add(this.pageReport);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // pageStart
            // 
            this.pageStart.Controls.Add(this.labelStart);
            this.pageStart.Name = "pageStart";
            resources.ApplyResources(this.pageStart, "pageStart");
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // pageGrowth
            // 
            this.pageGrowth.AllowNext = false;
            this.pageGrowth.Controls.Add(this.buttonGrowth);
            this.pageGrowth.Controls.Add(this.textBoxT0);
            this.pageGrowth.Controls.Add(this.labelK);
            this.pageGrowth.Controls.Add(this.textBoxK);
            this.pageGrowth.Controls.Add(this.labelW);
            this.pageGrowth.Controls.Add(this.textBoxW);
            this.pageGrowth.Controls.Add(this.labelT0);
            this.pageGrowth.Controls.Add(this.labelCategoryInstruction);
            this.pageGrowth.Name = "pageGrowth";
            resources.ApplyResources(this.pageGrowth, "pageGrowth");
            // 
            // buttonGrowth
            // 
            resources.ApplyResources(this.buttonGrowth, "buttonGrowth");
            this.buttonGrowth.Name = "buttonGrowth";
            this.buttonGrowth.UseVisualStyleBackColor = true;
            this.buttonGrowth.Click += new System.EventHandler(this.buttonGrowth_Click);
            // 
            // textBoxT0
            // 
            resources.ApplyResources(this.textBoxT0, "textBoxT0");
            this.textBoxT0.Name = "textBoxT0";
            this.textBoxT0.ReadOnly = true;
            this.textBoxT0.Value = double.NaN;
            this.textBoxT0.TextChanged += new System.EventHandler(this.growth_Changed);
            // 
            // labelK
            // 
            resources.ApplyResources(this.labelK, "labelK");
            this.labelK.Name = "labelK";
            // 
            // textBoxK
            // 
            resources.ApplyResources(this.textBoxK, "textBoxK");
            this.textBoxK.Name = "textBoxK";
            this.textBoxK.ReadOnly = true;
            this.textBoxK.Value = double.NaN;
            this.textBoxK.TextChanged += new System.EventHandler(this.growth_Changed);
            // 
            // labelW
            // 
            resources.ApplyResources(this.labelW, "labelW");
            this.labelW.Name = "labelW";
            // 
            // textBoxW
            // 
            resources.ApplyResources(this.textBoxW, "textBoxW");
            this.textBoxW.Name = "textBoxW";
            this.textBoxW.ReadOnly = true;
            this.textBoxW.Value = double.NaN;
            this.textBoxW.TextChanged += new System.EventHandler(this.growth_Changed);
            // 
            // labelT0
            // 
            resources.ApplyResources(this.labelT0, "labelT0");
            this.labelT0.Name = "labelT0";
            // 
            // labelCategoryInstruction
            // 
            resources.ApplyResources(this.labelCategoryInstruction, "labelCategoryInstruction");
            this.labelCategoryInstruction.Name = "labelCategoryInstruction";
            // 
            // pageAges
            // 
            this.pageAges.AllowNext = false;
            this.pageAges.Controls.Add(this.numericUpDownM);
            this.pageAges.Controls.Add(this.labelFi);
            this.pageAges.Controls.Add(this.button1);
            this.pageAges.Controls.Add(this.buttonMortality);
            this.pageAges.Controls.Add(this.label3);
            this.pageAges.Controls.Add(this.label2);
            this.pageAges.Controls.Add(this.label1);
            this.pageAges.Controls.Add(this.textBoxTc);
            this.pageAges.Controls.Add(this.textBoxTr);
            this.pageAges.Name = "pageAges";
            resources.ApplyResources(this.pageAges, "pageAges");
            this.pageAges.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAges_Commit);
            // 
            // numericUpDownM
            // 
            resources.ApplyResources(this.numericUpDownM, "numericUpDownM");
            this.numericUpDownM.DecimalPlaces = 3;
            this.numericUpDownM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownM.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownM.Name = "numericUpDownM";
            this.numericUpDownM.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownM.ValueChanged += new System.EventHandler(this.ages_Changed);
            // 
            // labelFi
            // 
            resources.ApplyResources(this.labelFi, "labelFi");
            this.labelFi.Name = "labelFi";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonMortality_Click);
            // 
            // buttonMortality
            // 
            resources.ApplyResources(this.buttonMortality, "buttonMortality");
            this.buttonMortality.Name = "buttonMortality";
            this.buttonMortality.UseVisualStyleBackColor = true;
            this.buttonMortality.Click += new System.EventHandler(this.buttonMortality_Click);
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxTc
            // 
            resources.ApplyResources(this.textBoxTc, "textBoxTc");
            this.textBoxTc.Name = "textBoxTc";
            this.textBoxTc.Value = double.NaN;
            this.textBoxTc.TextChanged += new System.EventHandler(this.ages_Changed);
            // 
            // textBoxTr
            // 
            resources.ApplyResources(this.textBoxTr, "textBoxTr");
            this.textBoxTr.Name = "textBoxTr";
            this.textBoxTr.Value = double.NaN;
            this.textBoxTr.TextChanged += new System.EventHandler(this.ages_Changed);
            // 
            // pageChart
            // 
            this.pageChart.AllowNext = false;
            this.pageChart.Controls.Add(this.label4);
            this.pageChart.Controls.Add(this.plotYR);
            this.pageChart.Name = "pageChart";
            resources.ApplyResources(this.pageChart, "pageChart");
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // plotYR
            // 
            resources.ApplyResources(this.plotYR, "plotYR");
            this.plotYR.AxisXAutoMaximum = false;
            this.plotYR.AxisXAutoMinimum = false;
            this.plotYR.AxisXMax = 6D;
            this.plotYR.AxisY2AutoMaximum = false;
            this.plotYR.AxisYAutoMaximum = false;
            this.plotYR.AxisYAutoMinimum = false;
            this.plotYR.AxisYInterval = 0.1D;
            this.plotYR.AxisYMax = 1D;
            this.plotYR.Name = "plotYR";
            this.plotYR.ShowLegend = true;
            this.plotYR.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxGrowth);
            this.pageReport.Controls.Add(this.checkBoxYR);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxGrowth
            // 
            resources.ApplyResources(this.checkBoxGrowth, "checkBoxGrowth");
            this.checkBoxGrowth.Checked = true;
            this.checkBoxGrowth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGrowth.Name = "checkBoxGrowth";
            this.checkBoxGrowth.UseVisualStyleBackColor = true;
            // 
            // checkBoxYR
            // 
            resources.ApplyResources(this.checkBoxYR, "checkBoxYR");
            this.checkBoxYR.Checked = true;
            this.checkBoxYR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxYR.Name = "checkBoxYR";
            this.checkBoxYR.UseVisualStyleBackColor = true;
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // pictureBoxWarn
            // 
            resources.ApplyResources(this.pictureBoxWarn, "pictureBoxWarn");
            this.pictureBoxWarn.Image = global::Mayfly.Resources.Icons.Attention;
            this.pictureBoxWarn.Name = "pictureBoxWarn";
            this.pictureBoxWarn.TabStop = false;
            // 
            // labelWarn
            // 
            resources.ApplyResources(this.labelWarn, "labelWarn");
            this.labelWarn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelWarn.Name = "labelWarn";
            // 
            // WizardMSYR
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelWarn);
            this.Controls.Add(this.pictureBoxWarn);
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardMSYR";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageGrowth.ResumeLayout(false);
            this.pageGrowth.PerformLayout();
            this.pageAges.ResumeLayout(false);
            this.pageAges.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).EndInit();
            this.pageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotYR)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageGrowth;
        private System.Windows.Forms.Label labelCategoryInstruction;
        private AeroWizard.WizardPage pageStart;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxYR;
        private System.Windows.Forms.CheckBox checkBoxGrowth;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageAges;
        private System.Windows.Forms.Label label1;
        private Mayfly.Controls.NumberBox textBoxT0;
        private System.Windows.Forms.Label labelK;
        private Mayfly.Controls.NumberBox textBoxK;
        private System.Windows.Forms.Label labelW;
        private Mayfly.Controls.NumberBox textBoxW;
        private System.Windows.Forms.Label labelT0;
        private System.Windows.Forms.Button buttonGrowth;
        private System.Windows.Forms.Button buttonMortality;
        private Mayfly.Controls.NumberBox textBoxTc;
        private System.Windows.Forms.Label label3;
        private Mayfly.Controls.NumberBox textBoxTr;
        private System.Windows.Forms.Label label2;
        private AeroWizard.WizardPage pageChart;
        private System.Windows.Forms.Label label4;
        private Mathematics.Charts.Plot plotYR;
        private System.Windows.Forms.PictureBox pictureBoxWarn;
        private System.Windows.Forms.Label labelWarn;
        private System.Windows.Forms.NumericUpDown numericUpDownM;
        private System.Windows.Forms.Label labelFi;
        private System.Windows.Forms.Button button1;
    }
}