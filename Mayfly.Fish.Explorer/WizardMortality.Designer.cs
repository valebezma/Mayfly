namespace Mayfly.Fish.Explorer
{
    partial class WizardMortality
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardMortality));
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelNoData = new System.Windows.Forms.Label();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageChart = new AeroWizard.WizardPage();
            this.comboBoxAge = new System.Windows.Forms.ComboBox();
            this.labelT = new System.Windows.Forms.Label();
            this.buttonAL = new System.Windows.Forms.Button();
            this.statChartMortality = new Mayfly.Mathematics.Charts.Plot();
            this.labelCategoryInstruction = new System.Windows.Forms.Label();
            this.pageValues = new AeroWizard.WizardPage();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.labelS = new System.Windows.Forms.Label();
            this.textBoxFi = new System.Windows.Forms.TextBox();
            this.labelFi = new System.Windows.Forms.Label();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.labelZ = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.checkBoxMortality = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxAge = new System.Windows.Forms.CheckBox();
            this.checkBoxAppT = new System.Windows.Forms.CheckBox();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.categoryCalculator = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartMortality)).BeginInit();
            this.pageValues.SuspendLayout();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageChart);
            this.wizardExplorer.Pages.Add(this.pageValues);
            this.wizardExplorer.Pages.Add(this.pageReport);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // pageStart
            // 
            this.pageStart.Controls.Add(this.labelNoData);
            this.pageStart.Controls.Add(this.labelStart);
            this.pageStart.Name = "pageStart";
            resources.ApplyResources(this.pageStart, "pageStart");
            this.pageStart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageStart_Commit);
            // 
            // labelNoData
            // 
            resources.ApplyResources(this.labelNoData, "labelNoData");
            this.labelNoData.Name = "labelNoData";
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // pageChart
            // 
            this.pageChart.Controls.Add(this.comboBoxAge);
            this.pageChart.Controls.Add(this.labelT);
            this.pageChart.Controls.Add(this.buttonAL);
            this.pageChart.Controls.Add(this.statChartMortality);
            this.pageChart.Controls.Add(this.labelCategoryInstruction);
            this.pageChart.Name = "pageChart";
            resources.ApplyResources(this.pageChart, "pageChart");
            this.pageChart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageChart_Commit);
            this.pageChart.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageChart_Rollback);
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
            // buttonAL
            // 
            resources.ApplyResources(this.buttonAL, "buttonAL");
            this.buttonAL.Name = "buttonAL";
            this.buttonAL.UseVisualStyleBackColor = true;
            this.buttonAL.Click += new System.EventHandler(this.buttonAL_Click);
            // 
            // statChartMortality
            // 
            resources.ApplyResources(this.statChartMortality, "statChartMortality");
            this.statChartMortality.AxisXAutoMinimum = false;
            this.statChartMortality.AxisYAutoMinimum = false;
            this.statChartMortality.Name = "statChartMortality";
            this.statChartMortality.ShowLegend = false;
            this.statChartMortality.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // labelCategoryInstruction
            // 
            resources.ApplyResources(this.labelCategoryInstruction, "labelCategoryInstruction");
            this.labelCategoryInstruction.Name = "labelCategoryInstruction";
            // 
            // pageValues
            // 
            this.pageValues.Controls.Add(this.textBoxS);
            this.pageValues.Controls.Add(this.labelS);
            this.pageValues.Controls.Add(this.textBoxFi);
            this.pageValues.Controls.Add(this.labelFi);
            this.pageValues.Controls.Add(this.textBoxZ);
            this.pageValues.Controls.Add(this.labelZ);
            this.pageValues.Controls.Add(this.label1);
            this.pageValues.Name = "pageValues";
            resources.ApplyResources(this.pageValues, "pageValues");
            // 
            // textBoxS
            // 
            resources.ApplyResources(this.textBoxS, "textBoxS");
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.ReadOnly = true;
            // 
            // labelS
            // 
            resources.ApplyResources(this.labelS, "labelS");
            this.labelS.Name = "labelS";
            // 
            // textBoxFi
            // 
            resources.ApplyResources(this.textBoxFi, "textBoxFi");
            this.textBoxFi.Name = "textBoxFi";
            this.textBoxFi.ReadOnly = true;
            // 
            // labelFi
            // 
            resources.ApplyResources(this.labelFi, "labelFi");
            this.labelFi.Name = "labelFi";
            // 
            // textBoxZ
            // 
            resources.ApplyResources(this.textBoxZ, "textBoxZ");
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.ReadOnly = true;
            // 
            // labelZ
            // 
            resources.ApplyResources(this.labelZ, "labelZ");
            this.labelZ.Name = "labelZ";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Controls.Add(this.checkBoxMortality);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxAge);
            this.pageReport.Controls.Add(this.checkBoxAppT);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxGears
            // 
            resources.ApplyResources(this.checkBoxGears, "checkBoxGears");
            this.checkBoxGears.Name = "checkBoxGears";
            this.checkBoxGears.UseVisualStyleBackColor = true;
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
            // checkBoxAge
            // 
            resources.ApplyResources(this.checkBoxAge, "checkBoxAge");
            this.checkBoxAge.Checked = true;
            this.checkBoxAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAge.Name = "checkBoxAge";
            this.checkBoxAge.UseVisualStyleBackColor = true;
            this.checkBoxAge.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
            // 
            // checkBoxAppT
            // 
            resources.ApplyResources(this.checkBoxAppT, "checkBoxAppT");
            this.checkBoxAppT.Name = "checkBoxAppT";
            this.checkBoxAppT.UseVisualStyleBackColor = true;
            this.checkBoxAppT.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // categoryCalculator
            // 
            this.categoryCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.categoryCalculator_DoWork);
            this.categoryCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.categoryCalculator_RunWorkerCompleted);
            // 
            // WizardMortality
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardMortality";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageChart.ResumeLayout(false);
            this.pageChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartMortality)).EndInit();
            this.pageValues.ResumeLayout(false);
            this.pageValues.PerformLayout();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxAge;
        private System.Windows.Forms.CheckBox checkBoxAppT;
        private System.Windows.Forms.CheckBox checkBoxMortality;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private System.ComponentModel.BackgroundWorker reporter;
        private Mathematics.Charts.Plot statChartMortality;
        private AeroWizard.WizardPage pageValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.Label labelS;
        private System.Windows.Forms.TextBox textBoxFi;
        private System.Windows.Forms.Label labelFi;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.Button buttonAL;
        private System.ComponentModel.BackgroundWorker categoryCalculator;
        private System.Windows.Forms.Label labelNoData;
        private System.Windows.Forms.Label labelT;
        private System.Windows.Forms.ComboBox comboBoxAge;
    }
}