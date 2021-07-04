namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardGrowth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardGrowth));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageCrossSection = new AeroWizard.WizardPage();
            this.labelNoData = new System.Windows.Forms.Label();
            this.spreadSheetCross = new Mayfly.Controls.SpreadSheet();
            this.columnCrossAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCrossN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCrossLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCrossMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.pageAL = new AeroWizard.WizardPage();
            this.statChartAL = new Mayfly.Mathematics.Charts.Plot();
            this.buttonAL = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNoDataGrowth = new System.Windows.Forms.Label();
            this.pageWL = new AeroWizard.WizardPage();
            this.statChartLW = new Mayfly.Mathematics.Charts.Plot();
            this.buttonLW = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNoDataWeight = new System.Windows.Forms.Label();
            this.pageAW = new AeroWizard.WizardPage();
            this.plotAW = new Mayfly.Mathematics.Charts.Plot();
            this.label1 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxMass = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxGrowth = new System.Windows.Forms.CheckBox();
            this.checkBoxData = new System.Windows.Forms.CheckBox();
            this.categoryCalculator = new System.ComponentModel.BackgroundWorker();
            this.modelCalculator = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageCrossSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCross)).BeginInit();
            this.pageAL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartAL)).BeginInit();
            this.pageWL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartLW)).BeginInit();
            this.pageAW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotAW)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageCrossSection);
            this.wizardExplorer.Pages.Add(this.pageAL);
            this.wizardExplorer.Pages.Add(this.pageWL);
            this.wizardExplorer.Pages.Add(this.pageAW);
            this.wizardExplorer.Pages.Add(this.pageReport);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // pageStart
            // 
            this.pageStart.Controls.Add(this.labelStart);
            this.pageStart.Name = "pageStart";
            resources.ApplyResources(this.pageStart, "pageStart");
            this.pageStart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageStart_Commit);
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // pageCrossSection
            // 
            this.pageCrossSection.AllowNext = false;
            this.pageCrossSection.Controls.Add(this.labelNoData);
            this.pageCrossSection.Controls.Add(this.spreadSheetCross);
            this.pageCrossSection.Controls.Add(this.label9);
            this.pageCrossSection.Name = "pageCrossSection";
            resources.ApplyResources(this.pageCrossSection, "pageCrossSection");
            this.pageCrossSection.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCrossSection_Commit);
            this.pageCrossSection.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCrossSection_Rollback);
            // 
            // labelNoData
            // 
            resources.ApplyResources(this.labelNoData, "labelNoData");
            this.labelNoData.Name = "labelNoData";
            // 
            // spreadSheetCross
            // 
            resources.ApplyResources(this.spreadSheetCross, "spreadSheetCross");
            this.spreadSheetCross.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.spreadSheetCross.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCrossAge,
            this.columnCrossN,
            this.columnCrossLength,
            this.columnCrossMass});
            this.spreadSheetCross.DefaultDecimalPlaces = 0;
            this.spreadSheetCross.Name = "spreadSheetCross";
            this.spreadSheetCross.ReadOnly = true;
            this.spreadSheetCross.RowHeadersVisible = false;
            this.spreadSheetCross.RowTemplate.Height = 35;
            this.spreadSheetCross.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetCross.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // columnCrossAge
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.columnCrossAge.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnCrossAge, "columnCrossAge");
            this.columnCrossAge.Name = "columnCrossAge";
            this.columnCrossAge.ReadOnly = true;
            // 
            // columnCrossN
            // 
            resources.ApplyResources(this.columnCrossN, "columnCrossN");
            this.columnCrossN.Name = "columnCrossN";
            this.columnCrossN.ReadOnly = true;
            // 
            // columnCrossLength
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "S";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnCrossLength.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnCrossLength, "columnCrossLength");
            this.columnCrossLength.Name = "columnCrossLength";
            this.columnCrossLength.ReadOnly = true;
            // 
            // columnCrossMass
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "S";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnCrossMass.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnCrossMass, "columnCrossMass");
            this.columnCrossMass.Name = "columnCrossMass";
            this.columnCrossMass.ReadOnly = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // pageAL
            // 
            this.pageAL.Controls.Add(this.statChartAL);
            this.pageAL.Controls.Add(this.buttonAL);
            this.pageAL.Controls.Add(this.label3);
            this.pageAL.Controls.Add(this.labelNoDataGrowth);
            this.pageAL.Name = "pageAL";
            resources.ApplyResources(this.pageAL, "pageAL");
            // 
            // statChartAL
            // 
            resources.ApplyResources(this.statChartAL, "statChartAL");
            this.statChartAL.AxisXAutoMinimum = false;
            this.statChartAL.AxisYAutoMinimum = false;
            this.statChartAL.Name = "statChartAL";
            this.statChartAL.ShowLegend = false;
            this.statChartAL.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // buttonAL
            // 
            resources.ApplyResources(this.buttonAL, "buttonAL");
            this.buttonAL.Name = "buttonAL";
            this.buttonAL.UseVisualStyleBackColor = true;
            this.buttonAL.Click += new System.EventHandler(this.buttonAL_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labelNoDataGrowth
            // 
            resources.ApplyResources(this.labelNoDataGrowth, "labelNoDataGrowth");
            this.labelNoDataGrowth.Name = "labelNoDataGrowth";
            // 
            // pageWL
            // 
            this.pageWL.Controls.Add(this.statChartLW);
            this.pageWL.Controls.Add(this.buttonLW);
            this.pageWL.Controls.Add(this.label2);
            this.pageWL.Controls.Add(this.labelNoDataWeight);
            this.pageWL.Name = "pageWL";
            resources.ApplyResources(this.pageWL, "pageWL");
            // 
            // statChartLW
            // 
            resources.ApplyResources(this.statChartLW, "statChartLW");
            this.statChartLW.AxisXAutoMinimum = false;
            this.statChartLW.AxisYAutoMinimum = false;
            this.statChartLW.Name = "statChartLW";
            this.statChartLW.ShowLegend = false;
            this.statChartLW.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // buttonLW
            // 
            resources.ApplyResources(this.buttonLW, "buttonLW");
            this.buttonLW.Name = "buttonLW";
            this.buttonLW.UseVisualStyleBackColor = true;
            this.buttonLW.Click += new System.EventHandler(this.buttonLW_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelNoDataWeight
            // 
            resources.ApplyResources(this.labelNoDataWeight, "labelNoDataWeight");
            this.labelNoDataWeight.Name = "labelNoDataWeight";
            // 
            // pageAW
            // 
            this.pageAW.Controls.Add(this.plotAW);
            this.pageAW.Controls.Add(this.label1);
            this.pageAW.Name = "pageAW";
            resources.ApplyResources(this.pageAW, "pageAW");
            this.pageAW.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAW_Commit);
            // 
            // plotAW
            // 
            resources.ApplyResources(this.plotAW, "plotAW");
            this.plotAW.AxisXAutoMinimum = false;
            this.plotAW.AxisYAutoMinimum = false;
            this.plotAW.Name = "plotAW";
            this.plotAW.ShowLegend = false;
            this.plotAW.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxMass);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxGrowth);
            this.pageReport.Controls.Add(this.checkBoxData);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxMass
            // 
            resources.ApplyResources(this.checkBoxMass, "checkBoxMass");
            this.checkBoxMass.Checked = true;
            this.checkBoxMass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMass.Name = "checkBoxMass";
            this.checkBoxMass.UseVisualStyleBackColor = true;
            this.checkBoxMass.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxGrowth
            // 
            resources.ApplyResources(this.checkBoxGrowth, "checkBoxGrowth");
            this.checkBoxGrowth.Checked = true;
            this.checkBoxGrowth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGrowth.Name = "checkBoxGrowth";
            this.checkBoxGrowth.UseVisualStyleBackColor = true;
            this.checkBoxGrowth.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxData
            // 
            resources.ApplyResources(this.checkBoxData, "checkBoxData");
            this.checkBoxData.Checked = true;
            this.checkBoxData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxData.Name = "checkBoxData";
            this.checkBoxData.UseVisualStyleBackColor = true;
            this.checkBoxData.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // categoryCalculator
            // 
            this.categoryCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.categoryCalculator_DoWork);
            this.categoryCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.categoryCalculator_RunWorkerCompleted);
            // 
            // modelCalculator
            // 
            this.modelCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.modelCalculator_DoWork);
            this.modelCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.modelCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // WizardGrowth
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardGrowth";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageCrossSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCross)).EndInit();
            this.pageAL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statChartAL)).EndInit();
            this.pageWL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statChartLW)).EndInit();
            this.pageAW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotAW)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private System.ComponentModel.BackgroundWorker categoryCalculator;
        private AeroWizard.WizardPage pageWL;
        private AeroWizard.WizardPage pageAL;
        public Mathematics.Charts.Plot statChartLW;
        private System.Windows.Forms.Label label3;
        private AeroWizard.WizardPage pageStart;
        private Mathematics.Charts.Plot statChartAL;
        private System.ComponentModel.BackgroundWorker modelCalculator;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxGrowth;
        private System.Windows.Forms.CheckBox checkBoxMass;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.Label labelNoDataWeight;
        private System.Windows.Forms.Label labelNoDataGrowth;
        private AeroWizard.WizardPage pageCrossSection;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private Controls.SpreadSheet spreadSheetCross;
        private System.Windows.Forms.Button buttonAL;
        private System.Windows.Forms.Button buttonLW;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCrossAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCrossN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCrossLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCrossMass;
        private System.Windows.Forms.CheckBox checkBoxData;
        private System.Windows.Forms.Label labelNoData;
        private AeroWizard.WizardPage pageAW;
        private Mathematics.Charts.Plot plotAW;
        private System.Windows.Forms.Label label1;
    }
}