namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardMortalityCohorts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardMortalityCohorts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageCohorts = new AeroWizard.WizardPage();
            this.labelNoData = new System.Windows.Forms.Label();
            this.spreadSheetCohorts = new Mayfly.Controls.SpreadSheet();
            this.ColumnCohAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.pageChart = new AeroWizard.WizardPage();
            this.comboBoxAge = new System.Windows.Forms.ComboBox();
            this.labelT = new System.Windows.Forms.Label();
            this.buttonM = new System.Windows.Forms.Button();
            this.statChartMortality = new Mayfly.Mathematics.Charts.Plot();
            this.labelCategoryInstruction = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxMortality = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxHistory = new System.Windows.Forms.CheckBox();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.cohortsDetector = new System.ComponentModel.BackgroundWorker();
            this.contextMortality = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGrowthAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.modelCalculator = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageCohorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCohorts)).BeginInit();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartMortality)).BeginInit();
            this.pageReport.SuspendLayout();
            this.contextMortality.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageCohorts);
            this.wizardExplorer.Pages.Add(this.pageChart);
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
            this.pageCohorts.Controls.Add(this.label2);
            this.pageCohorts.Name = "pageCohorts";
            resources.ApplyResources(this.pageCohorts, "pageCohorts");
            this.pageCohorts.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCohorts_Rollback);
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
            resources.ApplyResources(this.ColumnCohAge, "ColumnCohAge");
            this.ColumnCohAge.Name = "ColumnCohAge";
            this.ColumnCohAge.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pageChart
            // 
            this.pageChart.Controls.Add(this.comboBoxAge);
            this.pageChart.Controls.Add(this.labelT);
            this.pageChart.Controls.Add(this.buttonM);
            this.pageChart.Controls.Add(this.statChartMortality);
            this.pageChart.Controls.Add(this.labelCategoryInstruction);
            this.pageChart.Name = "pageChart";
            resources.ApplyResources(this.pageChart, "pageChart");
            // 
            // comboBoxAge
            // 
            resources.ApplyResources(this.comboBoxAge, "comboBoxAge");
            this.comboBoxAge.DisplayMember = "Name";
            this.comboBoxAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAge.FormattingEnabled = true;
            this.comboBoxAge.Name = "comboBoxAge";
            this.comboBoxAge.SelectedIndexChanged += new System.EventHandler(this.comboBoxAge_SelectedIndexChanged);
            // 
            // labelT
            // 
            resources.ApplyResources(this.labelT, "labelT");
            this.labelT.Name = "labelT";
            // 
            // buttonM
            // 
            resources.ApplyResources(this.buttonM, "buttonM");
            this.buttonM.Name = "buttonM";
            this.buttonM.UseVisualStyleBackColor = true;
            this.buttonM.Click += new System.EventHandler(this.buttonM_Click);
            // 
            // statChartMortality
            // 
            resources.ApplyResources(this.statChartMortality, "statChartMortality");
            this.statChartMortality.AxisXMax = 43197.627167928244D;
            this.statChartMortality.AxisXMin = 43197.627167928244D;
            this.statChartMortality.AxisYAutoMinimum = false;
            this.statChartMortality.IsChronic = true;
            this.statChartMortality.Name = "statChartMortality";
            this.statChartMortality.ShowLegend = true;
            this.statChartMortality.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Years;
            // 
            // labelCategoryInstruction
            // 
            resources.ApplyResources(this.labelCategoryInstruction, "labelCategoryInstruction");
            this.labelCategoryInstruction.Name = "labelCategoryInstruction";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxMortality);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxHistory);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxMortality
            // 
            resources.ApplyResources(this.checkBoxMortality, "checkBoxMortality");
            this.checkBoxMortality.Checked = true;
            this.checkBoxMortality.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMortality.Name = "checkBoxMortality";
            this.checkBoxMortality.UseVisualStyleBackColor = true;
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxHistory
            // 
            resources.ApplyResources(this.checkBoxHistory, "checkBoxHistory");
            this.checkBoxHistory.Checked = true;
            this.checkBoxHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHistory.Name = "checkBoxHistory";
            this.checkBoxHistory.UseVisualStyleBackColor = true;
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
            // contextMortality
            // 
            this.contextMortality.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGrowthAll,
            this.toolStripSeparator1});
            this.contextMortality.Name = "contextGrowth";
            resources.ApplyResources(this.contextMortality, "contextMortality");
            // 
            // contextGrowthAll
            // 
            this.contextGrowthAll.Name = "contextGrowthAll";
            resources.ApplyResources(this.contextGrowthAll, "contextGrowthAll");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // modelCalculator
            // 
            this.modelCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.modelCalculator_DoWork);
            this.modelCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.modelCalculator_RunWorkerCompleted);
            // 
            // WizardMortalityCohorts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardMortalityCohorts";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageCohorts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCohorts)).EndInit();
            this.pageChart.ResumeLayout(false);
            this.pageChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartMortality)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.contextMortality.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageChart;
        private System.Windows.Forms.Label labelCategoryInstruction;
        private AeroWizard.WizardPage pageStart;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxHistory;
        private System.Windows.Forms.CheckBox checkBoxMortality;
        private System.ComponentModel.BackgroundWorker reporter;
        private Mathematics.Charts.Plot statChartMortality;
        private AeroWizard.WizardPage pageCohorts;
        private Controls.SpreadSheet spreadSheetCohorts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonM;
        private System.ComponentModel.BackgroundWorker cohortsDetector;
        private System.Windows.Forms.ContextMenuStrip contextMortality;
        private System.Windows.Forms.ToolStripMenuItem contextGrowthAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker modelCalculator;
        private System.Windows.Forms.Label labelNoData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCohAge;
        private System.Windows.Forms.ComboBox comboBoxAge;
        private System.Windows.Forms.Label labelT;
    }
}