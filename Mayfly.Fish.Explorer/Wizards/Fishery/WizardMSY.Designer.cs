namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardMSY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardMSY));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageReference = new AeroWizard.WizardPage();
            this.spreadSheetReference = new Mayfly.Controls.SpreadSheet();
            this.ColumnRefAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRefW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRefF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRefZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonGrowth = new System.Windows.Forms.Button();
            this.buttonVpa = new System.Windows.Forms.Button();
            this.numericUpDownM = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pageResults = new AeroWizard.WizardPage();
            this.textBoxYield = new System.Windows.Forms.TextBox();
            this.labelY = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.labelX = new System.Windows.Forms.Label();
            this.spreadSheetMSY = new Mayfly.Controls.SpreadSheet();
            this.ColumnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.labelN = new System.Windows.Forms.Label();
            this.numericUpDownN = new System.Windows.Forms.NumericUpDown();
            this.pageChart = new AeroWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.plotMSY = new Mayfly.Mathematics.Charts.Plot();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxGrowth = new System.Windows.Forms.CheckBox();
            this.checkBoxVPA = new System.Windows.Forms.CheckBox();
            this.checkBoxMSY = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageReference.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).BeginInit();
            this.pageResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMSY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).BeginInit();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotMSY)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageReference);
            this.wizardExplorer.Pages.Add(this.pageResults);
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
            // pageReference
            // 
            this.pageReference.AllowNext = false;
            this.pageReference.Controls.Add(this.spreadSheetReference);
            this.pageReference.Controls.Add(this.buttonGrowth);
            this.pageReference.Controls.Add(this.buttonVpa);
            this.pageReference.Controls.Add(this.numericUpDownM);
            this.pageReference.Controls.Add(this.label3);
            this.pageReference.Controls.Add(this.label2);
            this.pageReference.Name = "pageReference";
            resources.ApplyResources(this.pageReference, "pageReference");
            this.pageReference.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReference_Commit);
            this.pageReference.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReference_Rollback);
            // 
            // spreadSheetReference
            // 
            this.spreadSheetReference.AllowUserToAddRows = true;
            resources.ApplyResources(this.spreadSheetReference, "spreadSheetReference");
            this.spreadSheetReference.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnRefAge,
            this.ColumnRefW,
            this.ColumnRefF,
            this.ColumnRefZ});
            this.spreadSheetReference.Name = "spreadSheetReference";
            this.spreadSheetReference.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetReference_CellEndEdit);
            this.spreadSheetReference.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetReference_RowValidated);
            // 
            // ColumnRefAge
            // 
            dataGridViewCellStyle1.Format = "g";
            this.ColumnRefAge.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnRefAge, "ColumnRefAge");
            this.ColumnRefAge.Name = "ColumnRefAge";
            this.ColumnRefAge.ReadOnly = true;
            // 
            // ColumnRefW
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.ColumnRefW.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnRefW, "ColumnRefW");
            this.ColumnRefW.Name = "ColumnRefW";
            // 
            // ColumnRefF
            // 
            dataGridViewCellStyle3.Format = "N3";
            this.ColumnRefF.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnRefF, "ColumnRefF");
            this.ColumnRefF.Name = "ColumnRefF";
            // 
            // ColumnRefZ
            // 
            dataGridViewCellStyle4.Format = "N3";
            this.ColumnRefZ.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnRefZ, "ColumnRefZ");
            this.ColumnRefZ.Name = "ColumnRefZ";
            this.ColumnRefZ.ReadOnly = true;
            // 
            // buttonGrowth
            // 
            resources.ApplyResources(this.buttonGrowth, "buttonGrowth");
            this.buttonGrowth.Name = "buttonGrowth";
            this.buttonGrowth.UseVisualStyleBackColor = true;
            this.buttonGrowth.Click += new System.EventHandler(this.buttonGrowth_Click);
            // 
            // buttonVpa
            // 
            resources.ApplyResources(this.buttonVpa, "buttonVpa");
            this.buttonVpa.Name = "buttonVpa";
            this.buttonVpa.UseVisualStyleBackColor = true;
            this.buttonVpa.Click += new System.EventHandler(this.buttonVpa_Click);
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
            2,
            0,
            0,
            65536});
            this.numericUpDownM.ValueChanged += new System.EventHandler(this.numericUpDownM_ValueChanged);
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
            // pageResults
            // 
            this.pageResults.Controls.Add(this.textBoxYield);
            this.pageResults.Controls.Add(this.labelY);
            this.pageResults.Controls.Add(this.numericUpDownX);
            this.pageResults.Controls.Add(this.labelX);
            this.pageResults.Controls.Add(this.spreadSheetMSY);
            this.pageResults.Controls.Add(this.label5);
            this.pageResults.Controls.Add(this.labelN);
            this.pageResults.Controls.Add(this.numericUpDownN);
            this.pageResults.Name = "pageResults";
            resources.ApplyResources(this.pageResults, "pageResults");
            this.pageResults.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageResults_Commit);
            // 
            // textBoxYield
            // 
            resources.ApplyResources(this.textBoxYield, "textBoxYield");
            this.textBoxYield.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxYield.Name = "textBoxYield";
            this.textBoxYield.ReadOnly = true;
            // 
            // labelY
            // 
            resources.ApplyResources(this.labelY, "labelY");
            this.labelY.Name = "labelY";
            // 
            // numericUpDownX
            // 
            resources.ApplyResources(this.numericUpDownX, "numericUpDownX");
            this.numericUpDownX.DecimalPlaces = 3;
            this.numericUpDownX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.msy_Changed);
            // 
            // labelX
            // 
            resources.ApplyResources(this.labelX, "labelX");
            this.labelX.Name = "labelX";
            // 
            // spreadSheetMSY
            // 
            resources.ApplyResources(this.spreadSheetMSY, "spreadSheetMSY");
            this.spreadSheetMSY.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAge,
            this.ColumnF,
            this.ColumnZ,
            this.ColumnN,
            this.ColumnB,
            this.ColumnC,
            this.ColumnY});
            this.spreadSheetMSY.Name = "spreadSheetMSY";
            this.spreadSheetMSY.ReadOnly = true;
            // 
            // ColumnAge
            // 
            dataGridViewCellStyle5.Format = "g";
            this.ColumnAge.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnAge, "ColumnAge");
            this.ColumnAge.Name = "ColumnAge";
            this.ColumnAge.ReadOnly = true;
            // 
            // ColumnF
            // 
            dataGridViewCellStyle6.Format = "N3";
            this.ColumnF.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnF, "ColumnF");
            this.ColumnF.Name = "ColumnF";
            this.ColumnF.ReadOnly = true;
            // 
            // ColumnZ
            // 
            dataGridViewCellStyle7.Format = "N3";
            this.ColumnZ.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnZ, "ColumnZ");
            this.ColumnZ.Name = "ColumnZ";
            this.ColumnZ.ReadOnly = true;
            // 
            // ColumnN
            // 
            dataGridViewCellStyle8.Format = "N2";
            this.ColumnN.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.ColumnN, "ColumnN");
            this.ColumnN.Name = "ColumnN";
            this.ColumnN.ReadOnly = true;
            // 
            // ColumnB
            // 
            dataGridViewCellStyle9.Format = "N2";
            this.ColumnB.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.ColumnB, "ColumnB");
            this.ColumnB.Name = "ColumnB";
            this.ColumnB.ReadOnly = true;
            // 
            // ColumnC
            // 
            dataGridViewCellStyle10.Format = "N2";
            this.ColumnC.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.ColumnC, "ColumnC");
            this.ColumnC.Name = "ColumnC";
            this.ColumnC.ReadOnly = true;
            // 
            // ColumnY
            // 
            dataGridViewCellStyle11.Format = "N2";
            this.ColumnY.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnY, "ColumnY");
            this.ColumnY.Name = "ColumnY";
            this.ColumnY.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // labelN
            // 
            resources.ApplyResources(this.labelN, "labelN");
            this.labelN.Name = "labelN";
            // 
            // numericUpDownN
            // 
            resources.ApplyResources(this.numericUpDownN, "numericUpDownN");
            this.numericUpDownN.DecimalPlaces = 1;
            this.numericUpDownN.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownN.Name = "numericUpDownN";
            this.numericUpDownN.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownN.ValueChanged += new System.EventHandler(this.numericUpDownN_ValueChanged);
            // 
            // pageChart
            // 
            this.pageChart.Controls.Add(this.label4);
            this.pageChart.Controls.Add(this.plotMSY);
            this.pageChart.Name = "pageChart";
            resources.ApplyResources(this.pageChart, "pageChart");
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // plotMSY
            // 
            resources.ApplyResources(this.plotMSY, "plotMSY");
            this.plotMSY.AxisXAutoMaximum = false;
            this.plotMSY.AxisXAutoMinimum = false;
            this.plotMSY.AxisXMax = 3D;
            this.plotMSY.AxisY2AutoMaximum = false;
            this.plotMSY.AxisYAutoMaximum = false;
            this.plotMSY.AxisYAutoMinimum = false;
            this.plotMSY.AxisYInterval = 0.1D;
            this.plotMSY.AxisYMax = 1D;
            this.plotMSY.Name = "plotMSY";
            this.plotMSY.ShowLegend = true;
            this.plotMSY.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxGrowth);
            this.pageReport.Controls.Add(this.checkBoxVPA);
            this.pageReport.Controls.Add(this.checkBoxMSY);
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
            // checkBoxVPA
            // 
            resources.ApplyResources(this.checkBoxVPA, "checkBoxVPA");
            this.checkBoxVPA.Checked = true;
            this.checkBoxVPA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVPA.Name = "checkBoxVPA";
            this.checkBoxVPA.UseVisualStyleBackColor = true;
            // 
            // checkBoxMSY
            // 
            resources.ApplyResources(this.checkBoxMSY, "checkBoxMSY");
            this.checkBoxMSY.Checked = true;
            this.checkBoxMSY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMSY.Name = "checkBoxMSY";
            this.checkBoxMSY.UseVisualStyleBackColor = true;
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
            // WizardMSY
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardMSY";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageReference.ResumeLayout(false);
            this.pageReference.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).EndInit();
            this.pageResults.ResumeLayout(false);
            this.pageResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMSY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).EndInit();
            this.pageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotMSY)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxMSY;
        private System.Windows.Forms.CheckBox checkBoxGrowth;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageChart;
        private System.Windows.Forms.Label label4;
        private Mathematics.Charts.Plot plotMSY;
        private AeroWizard.WizardPage pageReference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxVPA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownM;
        private System.Windows.Forms.Button buttonVpa;
        private AeroWizard.WizardPage pageResults;
        private System.Windows.Forms.TextBox textBoxYield;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label labelX;
        private Controls.SpreadSheet spreadSheetMSY;
        private System.Windows.Forms.Label label5;
        private Controls.SpreadSheet spreadSheetReference;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.NumericUpDown numericUpDownN;
        private System.Windows.Forms.Button buttonGrowth;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRefAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRefW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRefF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRefZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnY;
    }
}