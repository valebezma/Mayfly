namespace Mayfly.Fish.Explorer
{
    partial class WizardStockComposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardStockComposition));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 15D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 16D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 17D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 25D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 14D);
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 16D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 7D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 5D);
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageBasic = new AeroWizard.WizardPage();
            this.buttonL = new System.Windows.Forms.Button();
            this.buttonW = new System.Windows.Forms.Button();
            this.labelBasic = new System.Windows.Forms.Label();
            this.pageCpue = new AeroWizard.WizardPage();
            this.labelCpueDescription = new System.Windows.Forms.Label();
            this.spreadSheetSelectivity = new Mayfly.Controls.SpreadSheet();
            this.columnSelectivityClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityNpue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityBpue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivitySex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBpue = new System.Windows.Forms.TextBox();
            this.labelNpueUnit = new System.Windows.Forms.Label();
            this.textBoxNpue = new System.Windows.Forms.TextBox();
            this.labelBpueUnit = new System.Windows.Forms.Label();
            this.pageAge = new AeroWizard.WizardPage();
            this.plotT = new Mayfly.Mathematics.Charts.Plot();
            this.label1 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxAge = new System.Windows.Forms.CheckBox();
            this.checkBoxAppKeys = new System.Windows.Forms.CheckBox();
            this.checkBoxAppT = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.structureCalculator = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.selectivityCalculator = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageBasic.SuspendLayout();
            this.pageCpue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).BeginInit();
            this.pageAge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotT)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageBasic);
            this.wizardExplorer.Pages.Add(this.pageCpue);
            this.wizardExplorer.Pages.Add(this.pageAge);
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
            // pageBasic
            // 
            this.pageBasic.Controls.Add(this.buttonL);
            this.pageBasic.Controls.Add(this.buttonW);
            this.pageBasic.Controls.Add(this.labelBasic);
            this.pageBasic.Name = "pageBasic";
            resources.ApplyResources(this.pageBasic, "pageBasic");
            this.pageBasic.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageBasic_Commit);
            // 
            // buttonL
            // 
            resources.ApplyResources(this.buttonL, "buttonL");
            this.buttonL.Name = "buttonL";
            this.buttonL.UseVisualStyleBackColor = true;
            this.buttonL.Click += new System.EventHandler(this.ButtonL_Click);
            // 
            // buttonW
            // 
            resources.ApplyResources(this.buttonW, "buttonW");
            this.buttonW.Name = "buttonW";
            this.buttonW.UseVisualStyleBackColor = true;
            this.buttonW.Click += new System.EventHandler(this.ButtonW_Click);
            // 
            // labelBasic
            // 
            resources.ApplyResources(this.labelBasic, "labelBasic");
            this.labelBasic.Name = "labelBasic";
            // 
            // pageCpue
            // 
            this.pageCpue.Controls.Add(this.labelCpueDescription);
            this.pageCpue.Controls.Add(this.spreadSheetSelectivity);
            this.pageCpue.Controls.Add(this.label2);
            this.pageCpue.Controls.Add(this.textBoxBpue);
            this.pageCpue.Controls.Add(this.labelNpueUnit);
            this.pageCpue.Controls.Add(this.textBoxNpue);
            this.pageCpue.Controls.Add(this.labelBpueUnit);
            this.pageCpue.Name = "pageCpue";
            resources.ApplyResources(this.pageCpue, "pageCpue");
            this.pageCpue.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageCpue_Commit);
            this.pageCpue.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageCpue_Rollback);
            // 
            // labelCpueDescription
            // 
            resources.ApplyResources(this.labelCpueDescription, "labelCpueDescription");
            this.labelCpueDescription.Name = "labelCpueDescription";
            // 
            // spreadSheetSelectivity
            // 
            resources.ApplyResources(this.spreadSheetSelectivity, "spreadSheetSelectivity");
            this.spreadSheetSelectivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSelectivityClass,
            this.columnSelectivityW,
            this.columnSelectivityL,
            this.columnSelectivityN,
            this.columnSelectivityB,
            this.columnSelectivityNpue,
            this.columnSelectivityBpue,
            this.columnSelectivitySex});
            this.spreadSheetSelectivity.DefaultDecimalPlaces = 2;
            this.spreadSheetSelectivity.Name = "spreadSheetSelectivity";
            this.spreadSheetSelectivity.ReadOnly = true;
            this.spreadSheetSelectivity.RowHeadersVisible = false;
            this.spreadSheetSelectivity.RowTemplate.Height = 35;
            // 
            // columnSelectivityClass
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSelectivityClass.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnSelectivityClass, "columnSelectivityClass");
            this.columnSelectivityClass.Name = "columnSelectivityClass";
            this.columnSelectivityClass.ReadOnly = true;
            // 
            // columnSelectivityW
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSelectivityW.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnSelectivityW, "columnSelectivityW");
            this.columnSelectivityW.Name = "columnSelectivityW";
            this.columnSelectivityW.ReadOnly = true;
            // 
            // columnSelectivityL
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSelectivityL.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnSelectivityL, "columnSelectivityL");
            this.columnSelectivityL.Name = "columnSelectivityL";
            this.columnSelectivityL.ReadOnly = true;
            // 
            // columnSelectivityN
            // 
            dataGridViewCellStyle4.Format = "N0";
            this.columnSelectivityN.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnSelectivityN, "columnSelectivityN");
            this.columnSelectivityN.Name = "columnSelectivityN";
            this.columnSelectivityN.ReadOnly = true;
            // 
            // columnSelectivityB
            // 
            resources.ApplyResources(this.columnSelectivityB, "columnSelectivityB");
            this.columnSelectivityB.Name = "columnSelectivityB";
            this.columnSelectivityB.ReadOnly = true;
            // 
            // columnSelectivityNpue
            // 
            resources.ApplyResources(this.columnSelectivityNpue, "columnSelectivityNpue");
            this.columnSelectivityNpue.Name = "columnSelectivityNpue";
            this.columnSelectivityNpue.ReadOnly = true;
            // 
            // columnSelectivityBpue
            // 
            resources.ApplyResources(this.columnSelectivityBpue, "columnSelectivityBpue");
            this.columnSelectivityBpue.Name = "columnSelectivityBpue";
            this.columnSelectivityBpue.ReadOnly = true;
            // 
            // columnSelectivitySex
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "P0";
            this.columnSelectivitySex.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.columnSelectivitySex, "columnSelectivitySex");
            this.columnSelectivitySex.Name = "columnSelectivitySex";
            this.columnSelectivitySex.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxBpue
            // 
            resources.ApplyResources(this.textBoxBpue, "textBoxBpue");
            this.textBoxBpue.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxBpue.Name = "textBoxBpue";
            this.textBoxBpue.ReadOnly = true;
            // 
            // labelNpueUnit
            // 
            resources.ApplyResources(this.labelNpueUnit, "labelNpueUnit");
            this.labelNpueUnit.Name = "labelNpueUnit";
            // 
            // textBoxNpue
            // 
            resources.ApplyResources(this.textBoxNpue, "textBoxNpue");
            this.textBoxNpue.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxNpue.Name = "textBoxNpue";
            this.textBoxNpue.ReadOnly = true;
            // 
            // labelBpueUnit
            // 
            resources.ApplyResources(this.labelBpueUnit, "labelBpueUnit");
            this.labelBpueUnit.Name = "labelBpueUnit";
            // 
            // pageAge
            // 
            this.pageAge.Controls.Add(this.plotT);
            this.pageAge.Controls.Add(this.label1);
            this.pageAge.Name = "pageAge";
            resources.ApplyResources(this.pageAge, "pageAge");
            this.pageAge.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAge_Rollback);
            // 
            // plotT
            // 
            resources.ApplyResources(this.plotT, "plotT");
            this.plotT.AxisXAutoMaximum = false;
            this.plotT.AxisXAutoMinimum = false;
            this.plotT.AxisYAutoMaximum = false;
            this.plotT.AxisYAutoMinimum = false;
            chartArea1.Name = "ChartArea1";
            this.plotT.ChartAreas.Add(chartArea1);
            this.plotT.Name = "plotT";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series1.Name = "juv";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series2.Name = "m";
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series2.Points.Add(dataPoint6);
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series3.Name = "f";
            series3.Points.Add(dataPoint7);
            series3.Points.Add(dataPoint8);
            series3.Points.Add(dataPoint9);
            this.plotT.Series.Add(series1);
            this.plotT.Series.Add(series2);
            this.plotT.Series.Add(series3);
            this.plotT.ShowLegend = false;
            this.plotT.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxAge);
            this.pageReport.Controls.Add(this.checkBoxAppKeys);
            this.pageReport.Controls.Add(this.checkBoxAppT);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            this.pageReport.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageReport_Rollback);
            // 
            // checkBoxAge
            // 
            resources.ApplyResources(this.checkBoxAge, "checkBoxAge");
            this.checkBoxAge.Checked = true;
            this.checkBoxAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAge.Name = "checkBoxAge";
            this.checkBoxAge.UseVisualStyleBackColor = true;
            this.checkBoxAge.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
            this.checkBoxAge.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAppKeys
            // 
            resources.ApplyResources(this.checkBoxAppKeys, "checkBoxAppKeys");
            this.checkBoxAppKeys.Name = "checkBoxAppKeys";
            this.checkBoxAppKeys.UseVisualStyleBackColor = true;
            this.checkBoxAppKeys.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAppT
            // 
            resources.ApplyResources(this.checkBoxAppT, "checkBoxAppT");
            this.checkBoxAppT.Name = "checkBoxAppT";
            this.checkBoxAppT.UseVisualStyleBackColor = true;
            this.checkBoxAppT.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxGears
            // 
            resources.ApplyResources(this.checkBoxGears, "checkBoxGears");
            this.checkBoxGears.Name = "checkBoxGears";
            this.checkBoxGears.UseVisualStyleBackColor = true;
            this.checkBoxGears.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // structureCalculator
            // 
            this.structureCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.structureCalculator_DoWork);
            this.structureCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.structureCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // selectivityCalculator
            // 
            this.selectivityCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SelectivityCalculator_DoWork);
            this.selectivityCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SelectivityCalculator_RunWorkerCompleted);
            // 
            // WizardStockComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardStockComposition";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageBasic.ResumeLayout(false);
            this.pageCpue.ResumeLayout(false);
            this.pageCpue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).EndInit();
            this.pageAge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotT)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private System.ComponentModel.BackgroundWorker structureCalculator;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageAge;
        private Mathematics.Charts.Plot plotT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAge;
        private System.Windows.Forms.CheckBox checkBoxAppT;
        private System.Windows.Forms.CheckBox checkBoxAppKeys;
        private AeroWizard.WizardPage pageBasic;
        private System.Windows.Forms.Label labelBasic;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonW;
        private AeroWizard.WizardPage pageCpue;
        private Controls.SpreadSheet spreadSheetSelectivity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBpue;
        private System.Windows.Forms.Label labelNpueUnit;
        private System.Windows.Forms.TextBox textBoxNpue;
        private System.Windows.Forms.Label labelBpueUnit;
        private System.ComponentModel.BackgroundWorker selectivityCalculator;
        private System.Windows.Forms.Label labelCpueDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityW;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityL;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityB;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivitySex;
    }
}