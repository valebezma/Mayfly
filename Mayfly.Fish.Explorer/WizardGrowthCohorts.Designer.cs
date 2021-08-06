namespace Mayfly.Fish.Explorer
{
    partial class WizardGrowthCohorts
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardGrowthCohorts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageCohorts = new AeroWizard.WizardPage();
            this.labelNoData = new System.Windows.Forms.Label();
            this.spreadSheetCohorts = new Mayfly.Controls.SpreadSheet();
            this.ColumnCohAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.pageAL = new AeroWizard.WizardPage();
            this.buttonAL = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.statChartAL = new Mayfly.Mathematics.Charts.Plot();
            this.label8 = new System.Windows.Forms.Label();
            this.pageWL = new AeroWizard.WizardPage();
            this.buttonLW = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statChartLW = new Mayfly.Mathematics.Charts.Plot();
            this.labelNoticeGears = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxGrowth = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxMass = new System.Windows.Forms.CheckBox();
            this.checkBoxHistory = new System.Windows.Forms.CheckBox();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.cohortsDetector = new System.ComponentModel.BackgroundWorker();
            this.contextGrowth = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGrowthAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMass = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMassAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageCohorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCohorts)).BeginInit();
            this.pageAL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartAL)).BeginInit();
            this.pageWL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartLW)).BeginInit();
            this.pageReport.SuspendLayout();
            this.contextGrowth.SuspendLayout();
            this.contextMass.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageCohorts);
            this.wizardExplorer.Pages.Add(this.pageAL);
            this.wizardExplorer.Pages.Add(this.pageWL);
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
            // pageCohorts
            // 
            this.pageCohorts.Controls.Add(this.labelNoData);
            this.pageCohorts.Controls.Add(this.spreadSheetCohorts);
            this.pageCohorts.Controls.Add(this.label1);
            this.pageCohorts.Name = "pageCohorts";
            resources.ApplyResources(this.pageCohorts, "pageCohorts");
            // 
            // labelNoData
            // 
            resources.ApplyResources(this.labelNoData, "labelNoData");
            this.labelNoData.Name = "labelNoData";
            // 
            // spreadSheetCohorts
            // 
            resources.ApplyResources(this.spreadSheetCohorts, "spreadSheetCohorts");
            this.spreadSheetCohorts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCohAge});
            this.spreadSheetCohorts.DefaultDecimalPlaces = 0;
            this.spreadSheetCohorts.Name = "spreadSheetCohorts";
            this.spreadSheetCohorts.ReadOnly = true;
            this.spreadSheetCohorts.RowHeadersVisible = false;
            this.spreadSheetCohorts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ColumnCohAge
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCohAge.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnCohAge.Frozen = true;
            resources.ApplyResources(this.ColumnCohAge, "ColumnCohAge");
            this.ColumnCohAge.Name = "ColumnCohAge";
            this.ColumnCohAge.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageAL
            // 
            this.pageAL.Controls.Add(this.buttonAL);
            this.pageAL.Controls.Add(this.label3);
            this.pageAL.Controls.Add(this.statChartAL);
            this.pageAL.Controls.Add(this.label8);
            this.pageAL.Name = "pageAL";
            resources.ApplyResources(this.pageAL, "pageAL");
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
            // statChartAL
            // 
            resources.ApplyResources(this.statChartAL, "statChartAL");
            this.statChartAL.AxisXMax = 43197.625287013892D;
            this.statChartAL.AxisXMin = 43197.625287013892D;
            this.statChartAL.AxisYAutoMinimum = false;
            this.statChartAL.IsChronic = true;
            this.statChartAL.Name = "statChartAL";
            this.statChartAL.ShowLegend = true;
            this.statChartAL.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Years;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // pageWL
            // 
            this.pageWL.Controls.Add(this.buttonLW);
            this.pageWL.Controls.Add(this.label2);
            this.pageWL.Controls.Add(this.statChartLW);
            this.pageWL.Controls.Add(this.labelNoticeGears);
            this.pageWL.Name = "pageWL";
            resources.ApplyResources(this.pageWL, "pageWL");
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
            // statChartLW
            // 
            resources.ApplyResources(this.statChartLW, "statChartLW");
            this.statChartLW.AxisXAutoMinimum = false;
            this.statChartLW.AxisYAutoMinimum = false;
            this.statChartLW.Name = "statChartLW";
            this.statChartLW.ShowLegend = false;
            this.statChartLW.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // labelNoticeGears
            // 
            resources.ApplyResources(this.labelNoticeGears, "labelNoticeGears");
            this.labelNoticeGears.Name = "labelNoticeGears";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxGrowth);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxMass);
            this.pageReport.Controls.Add(this.checkBoxHistory);
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
            this.checkBoxGrowth.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
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
            // checkBoxHistory
            // 
            resources.ApplyResources(this.checkBoxHistory, "checkBoxHistory");
            this.checkBoxHistory.Checked = true;
            this.checkBoxHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHistory.Name = "checkBoxHistory";
            this.checkBoxHistory.UseVisualStyleBackColor = true;
            this.checkBoxHistory.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // cohortsDetector
            // 
            this.cohortsDetector.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cohortsDetector_DoWork);
            this.cohortsDetector.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.cohortsDetector_RunWorkerCompleted);
            // 
            // contextGrowth
            // 
            this.contextGrowth.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGrowthAll,
            this.toolStripSeparator1});
            this.contextGrowth.Name = "contextGrowth";
            resources.ApplyResources(this.contextGrowth, "contextGrowth");
            // 
            // contextGrowthAll
            // 
            this.contextGrowthAll.Name = "contextGrowthAll";
            resources.ApplyResources(this.contextGrowthAll, "contextGrowthAll");
            this.contextGrowthAll.Click += new System.EventHandler(this.contextGrowthAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // contextMass
            // 
            this.contextMass.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMassAll,
            this.toolStripSeparator2});
            this.contextMass.Name = "contextMass";
            resources.ApplyResources(this.contextMass, "contextMass");
            // 
            // contextMassAll
            // 
            this.contextMassAll.Name = "contextMassAll";
            resources.ApplyResources(this.contextMassAll, "contextMassAll");
            this.contextMassAll.Click += new System.EventHandler(this.contextMassAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // WizardGrowthCohorts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardGrowthCohorts";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageCohorts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCohorts)).EndInit();
            this.pageAL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statChartAL)).EndInit();
            this.pageWL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statChartLW)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.contextGrowth.ResumeLayout(false);
            this.contextMass.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageWL;
        private AeroWizard.WizardPage pageAL;
        public Mathematics.Charts.Plot statChartLW;
        private System.Windows.Forms.Label label3;
        private AeroWizard.WizardPage pageStart;
        private Mathematics.Charts.Plot statChartAL;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxHistory;
        private System.Windows.Forms.CheckBox checkBoxGrowth;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.Label labelNoticeGears;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private AeroWizard.WizardPage pageCohorts;
        private Controls.SpreadSheet spreadSheetCohorts;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker cohortsDetector;
        private System.Windows.Forms.Button buttonAL;
        private System.Windows.Forms.ContextMenuStrip contextGrowth;
        private System.Windows.Forms.ToolStripMenuItem contextGrowthAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMass;
        private System.Windows.Forms.ToolStripMenuItem contextMassAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button buttonLW;
        private System.Windows.Forms.CheckBox checkBoxMass;
        private System.Windows.Forms.Label labelNoData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCohAge;
    }
}