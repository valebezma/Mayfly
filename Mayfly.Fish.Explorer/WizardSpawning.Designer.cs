namespace Mayfly.Fish.Explorer
{
    partial class WizardSpawning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardSpawning));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageTable = new AeroWizard.WizardPage();
            this.spreadSheetEnv = new Mayfly.Controls.SpreadSheet();
            this.label1 = new System.Windows.Forms.Label();
            this.pageChart = new AeroWizard.WizardPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelConditions = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxEnvironment = new System.Windows.Forms.CheckBox();
            this.checkBoxFecundityPattern = new System.Windows.Forms.CheckBox();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.ColumnEnvWhen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnvTempSurface = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnvTempAir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnvLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnvWind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnvPrecips = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnEnvClouds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetEnv)).BeginInit();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageTable);
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
            // pageTable
            // 
            this.pageTable.Controls.Add(this.spreadSheetEnv);
            this.pageTable.Controls.Add(this.label1);
            this.pageTable.Name = "pageTable";
            resources.ApplyResources(this.pageTable, "pageTable");
            this.pageTable.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageTable_Commit);
            // 
            // spreadSheetEnv
            // 
            this.spreadSheetEnv.AllowUserToAddRows = true;
            resources.ApplyResources(this.spreadSheetEnv, "spreadSheetEnv");
            this.spreadSheetEnv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEnvWhen,
            this.ColumnEnvTempSurface,
            this.ColumnEnvTempAir,
            this.ColumnEnvLevel,
            this.ColumnEnvWind,
            this.ColumnEnvPrecips,
            this.ColumnEnvClouds});
            this.spreadSheetEnv.Name = "spreadSheetEnv";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageChart
            // 
            this.pageChart.Controls.Add(this.chart1);
            this.pageChart.Controls.Add(this.labelConditions);
            this.pageChart.Name = "pageChart";
            resources.ApplyResources(this.pageChart, "pageChart");
            // 
            // chart1
            // 
            resources.ApplyResources(this.chart1, "chart1");
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            chartArea1.AxisX.LabelStyle.Format = "m";
            chartArea1.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.Title = "Temperature, °C";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F);
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY2.Title = "Wind rate, m/s; Cloudage, okts";
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F);
            chartArea1.BorderColor = System.Drawing.Color.Gainsboro;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 4;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.SteelBlue;
            series1.Legend = "Legend1";
            series1.LegendText = "Temperature of water";
            series1.MarkerSize = 10;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Water";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.SteelBlue;
            series2.Legend = "Legend1";
            series2.LegendText = "Temperature of air";
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Air";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.LightSteelBlue;
            series3.Legend = "Legend1";
            series3.LegendText = "Wind rate";
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Wind";
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.DodgerBlue;
            series4.Legend = "Legend1";
            series4.LegendText = "Cloudage";
            series4.MarkerSize = 7;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series4.Name = "Clouds";
            series4.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            // 
            // labelConditions
            // 
            resources.ApplyResources(this.labelConditions, "labelConditions");
            this.labelConditions.Name = "labelConditions";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxEnvironment);
            this.pageReport.Controls.Add(this.checkBoxFecundityPattern);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxEnvironment
            // 
            resources.ApplyResources(this.checkBoxEnvironment, "checkBoxEnvironment");
            this.checkBoxEnvironment.Checked = true;
            this.checkBoxEnvironment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnvironment.Name = "checkBoxEnvironment";
            this.checkBoxEnvironment.UseVisualStyleBackColor = true;
            // 
            // checkBoxFecundityPattern
            // 
            resources.ApplyResources(this.checkBoxFecundityPattern, "checkBoxFecundityPattern");
            this.checkBoxFecundityPattern.Checked = true;
            this.checkBoxFecundityPattern.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFecundityPattern.Name = "checkBoxFecundityPattern";
            this.checkBoxFecundityPattern.UseVisualStyleBackColor = true;
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // ColumnEnvWhen
            // 
            this.ColumnEnvWhen.Frozen = true;
            resources.ApplyResources(this.ColumnEnvWhen, "ColumnEnvWhen");
            this.ColumnEnvWhen.Name = "ColumnEnvWhen";
            // 
            // ColumnEnvTempSurface
            // 
            resources.ApplyResources(this.ColumnEnvTempSurface, "ColumnEnvTempSurface");
            this.ColumnEnvTempSurface.Name = "ColumnEnvTempSurface";
            // 
            // ColumnEnvTempAir
            // 
            resources.ApplyResources(this.ColumnEnvTempAir, "ColumnEnvTempAir");
            this.ColumnEnvTempAir.Name = "ColumnEnvTempAir";
            // 
            // ColumnEnvLevel
            // 
            resources.ApplyResources(this.ColumnEnvLevel, "ColumnEnvLevel");
            this.ColumnEnvLevel.Name = "ColumnEnvLevel";
            // 
            // ColumnEnvWind
            // 
            resources.ApplyResources(this.ColumnEnvWind, "ColumnEnvWind");
            this.ColumnEnvWind.Name = "ColumnEnvWind";
            // 
            // ColumnEnvPrecips
            // 
            resources.ApplyResources(this.ColumnEnvPrecips, "ColumnEnvPrecips");
            this.ColumnEnvPrecips.Name = "ColumnEnvPrecips";
            // 
            // ColumnEnvClouds
            // 
            resources.ApplyResources(this.ColumnEnvClouds, "ColumnEnvClouds");
            this.ColumnEnvClouds.Name = "ColumnEnvClouds";
            // 
            // WizardSpawning
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardSpawning";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetEnv)).EndInit();
            this.pageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxEnvironment;
        private System.Windows.Forms.CheckBox checkBoxFecundityPattern;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageChart;
        private System.Windows.Forms.Label labelConditions;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private AeroWizard.WizardPage pageTable;
        private System.Windows.Forms.Label label1;
        private Controls.SpreadSheet spreadSheetEnv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnvWhen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnvTempSurface;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnvTempAir;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnvLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnvWind;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnEnvPrecips;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnvClouds;
    }
}