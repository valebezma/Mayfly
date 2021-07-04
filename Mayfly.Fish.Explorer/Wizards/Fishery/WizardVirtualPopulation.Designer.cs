namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardVirtualPopulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardVirtualPopulation));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageYield = new AeroWizard.WizardPage();
            this.plotYield = new Mayfly.Mathematics.Charts.Plot();
            this.label1 = new System.Windows.Forms.Label();
            this.pageCatches = new AeroWizard.WizardPage();
            this.buttonAnnual = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxCohortModel = new System.Windows.Forms.ComboBox();
            this.labelNoData = new System.Windows.Forms.Label();
            this.spreadSheetCatches = new Mayfly.Controls.SpreadSheet();
            this.ColumnCatAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.pageCohorts = new AeroWizard.WizardPage();
            this.spreadSheetCohorts = new Mayfly.Controls.SpreadSheet();
            this.ColumnCohAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.pageVpa = new AeroWizard.WizardPage();
            this.spreadSheetVpa = new Mayfly.Controls.SpreadSheet();
            this.ColumnVpaAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVpaYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVpaCatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVpaF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVpaN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonF = new System.Windows.Forms.Button();
            this.numericUpDownF = new System.Windows.Forms.NumericUpDown();
            this.labelArea = new System.Windows.Forms.Label();
            this.buttonM = new System.Windows.Forms.Button();
            this.numericUpDownM = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pageResults = new AeroWizard.WizardPage();
            this.spreadSheetResults = new Mayfly.Controls.SpreadSheet();
            this.ColumnResultAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResultYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResultC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResultF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnResultN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelGear = new System.Windows.Forms.Label();
            this.comboBoxCohort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pageChart = new AeroWizard.WizardPage();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxCohortChart = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.statChartVpa = new Mayfly.Mathematics.Charts.Plot();
            this.pageStock = new AeroWizard.WizardPage();
            this.spreadSheetStock = new Mayfly.Controls.SpreadSheet();
            this.ColumnStockAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxAppVpa = new System.Windows.Forms.CheckBox();
            this.checkBoxVpa = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxMortality = new System.Windows.Forms.CheckBox();
            this.checkBoxCatchHistory = new System.Windows.Forms.CheckBox();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.calcCatches = new System.ComponentModel.BackgroundWorker();
            this.calcAnnuals = new System.ComponentModel.BackgroundWorker();
            this.contextAnnuals = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextItemAnnualExplore = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxWarn = new System.Windows.Forms.PictureBox();
            this.labelWarn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageYield.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotYield)).BeginInit();
            this.pageCatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            this.pageCohorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCohorts)).BeginInit();
            this.pageVpa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetVpa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).BeginInit();
            this.pageResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetResults)).BeginInit();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartVpa)).BeginInit();
            this.pageStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetStock)).BeginInit();
            this.pageReport.SuspendLayout();
            this.contextAnnuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarn)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageYield);
            this.wizardExplorer.Pages.Add(this.pageCatches);
            this.wizardExplorer.Pages.Add(this.pageCohorts);
            this.wizardExplorer.Pages.Add(this.pageVpa);
            this.wizardExplorer.Pages.Add(this.pageResults);
            this.wizardExplorer.Pages.Add(this.pageChart);
            this.wizardExplorer.Pages.Add(this.pageStock);
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
            // pageYield
            // 
            this.pageYield.AllowNext = false;
            this.pageYield.Controls.Add(this.plotYield);
            this.pageYield.Controls.Add(this.label1);
            this.pageYield.Name = "pageYield";
            resources.ApplyResources(this.pageYield, "pageYield");
            this.pageYield.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageYield_Commit);
            this.pageYield.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageYield_Rollback);
            // 
            // plotYield
            // 
            resources.ApplyResources(this.plotYield, "plotYield");
            this.plotYield.AxisXMax = 43197.632529525465D;
            this.plotYield.AxisXMin = 43197.632529525465D;
            this.plotYield.AxisYAutoMinimum = false;
            this.plotYield.IsChronic = true;
            this.plotYield.Name = "plotYield";
            this.plotYield.ShowLegend = false;
            this.plotYield.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageCatches
            // 
            this.pageCatches.Controls.Add(this.buttonAnnual);
            this.pageCatches.Controls.Add(this.label10);
            this.pageCatches.Controls.Add(this.comboBoxCohortModel);
            this.pageCatches.Controls.Add(this.labelNoData);
            this.pageCatches.Controls.Add(this.spreadSheetCatches);
            this.pageCatches.Controls.Add(this.label2);
            this.pageCatches.Name = "pageCatches";
            resources.ApplyResources(this.pageCatches, "pageCatches");
            this.pageCatches.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCatches_Commit);
            // 
            // buttonAnnual
            // 
            resources.ApplyResources(this.buttonAnnual, "buttonAnnual");
            this.buttonAnnual.Name = "buttonAnnual";
            this.buttonAnnual.UseVisualStyleBackColor = true;
            this.buttonAnnual.Click += new System.EventHandler(this.buttonAnnual_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBoxCohortModel
            // 
            resources.ApplyResources(this.comboBoxCohortModel, "comboBoxCohortModel");
            this.comboBoxCohortModel.DisplayMember = "Name";
            this.comboBoxCohortModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCohortModel.FormattingEnabled = true;
            this.comboBoxCohortModel.Name = "comboBoxCohortModel";
            this.comboBoxCohortModel.Sorted = true;
            this.comboBoxCohortModel.ValueMember = "Birth";
            this.comboBoxCohortModel.SelectedIndexChanged += new System.EventHandler(this.comboBoxCohortModel_SelectedIndexChanged);
            // 
            // labelNoData
            // 
            resources.ApplyResources(this.labelNoData, "labelNoData");
            this.labelNoData.Name = "labelNoData";
            // 
            // spreadSheetCatches
            // 
            resources.ApplyResources(this.spreadSheetCatches, "spreadSheetCatches");
            this.spreadSheetCatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCatAge});
            this.spreadSheetCatches.DefaultDecimalPlaces = 0;
            this.spreadSheetCatches.Name = "spreadSheetCatches";
            this.spreadSheetCatches.RowHeadersVisible = false;
            this.spreadSheetCatches.RowTemplate.Height = 35;
            this.spreadSheetCatches.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetCatches.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCatches_CellValueChanged);
            // 
            // ColumnCatAge
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCatAge.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnCatAge.Frozen = true;
            resources.ApplyResources(this.ColumnCatAge, "ColumnCatAge");
            this.ColumnCatAge.Name = "ColumnCatAge";
            this.ColumnCatAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pageCohorts
            // 
            this.pageCohorts.Controls.Add(this.spreadSheetCohorts);
            this.pageCohorts.Controls.Add(this.label4);
            this.pageCohorts.Name = "pageCohorts";
            resources.ApplyResources(this.pageCohorts, "pageCohorts");
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
            this.spreadSheetCohorts.RowTemplate.Height = 35;
            this.spreadSheetCohorts.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnCohAge
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCohAge.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnCohAge.Frozen = true;
            resources.ApplyResources(this.ColumnCohAge, "ColumnCohAge");
            this.ColumnCohAge.Name = "ColumnCohAge";
            this.ColumnCohAge.ReadOnly = true;
            this.ColumnCohAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pageVpa
            // 
            this.pageVpa.Controls.Add(this.spreadSheetVpa);
            this.pageVpa.Controls.Add(this.buttonF);
            this.pageVpa.Controls.Add(this.numericUpDownF);
            this.pageVpa.Controls.Add(this.labelArea);
            this.pageVpa.Controls.Add(this.buttonM);
            this.pageVpa.Controls.Add(this.numericUpDownM);
            this.pageVpa.Controls.Add(this.label5);
            this.pageVpa.Controls.Add(this.label7);
            this.pageVpa.Name = "pageVpa";
            resources.ApplyResources(this.pageVpa, "pageVpa");
            this.pageVpa.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageVpa_Commit);
            // 
            // spreadSheetVpa
            // 
            resources.ApplyResources(this.spreadSheetVpa, "spreadSheetVpa");
            this.spreadSheetVpa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnVpaAge,
            this.ColumnVpaYear,
            this.ColumnVpaCatch,
            this.ColumnVpaF,
            this.ColumnVpaN});
            this.spreadSheetVpa.DefaultDecimalPlaces = 0;
            this.spreadSheetVpa.Name = "spreadSheetVpa";
            this.spreadSheetVpa.ReadOnly = true;
            this.spreadSheetVpa.RowHeadersVisible = false;
            this.spreadSheetVpa.RowTemplate.Height = 35;
            this.spreadSheetVpa.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnVpaAge
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnVpaAge.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnVpaAge.Frozen = true;
            resources.ApplyResources(this.ColumnVpaAge, "ColumnVpaAge");
            this.ColumnVpaAge.Name = "ColumnVpaAge";
            this.ColumnVpaAge.ReadOnly = true;
            // 
            // ColumnVpaYear
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "0000";
            this.ColumnVpaYear.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnVpaYear, "ColumnVpaYear");
            this.ColumnVpaYear.Name = "ColumnVpaYear";
            this.ColumnVpaYear.ReadOnly = true;
            // 
            // ColumnVpaCatch
            // 
            dataGridViewCellStyle5.Format = "N2";
            this.ColumnVpaCatch.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnVpaCatch, "ColumnVpaCatch");
            this.ColumnVpaCatch.Name = "ColumnVpaCatch";
            this.ColumnVpaCatch.ReadOnly = true;
            // 
            // ColumnVpaF
            // 
            dataGridViewCellStyle6.Format = "N3";
            this.ColumnVpaF.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnVpaF, "ColumnVpaF");
            this.ColumnVpaF.Name = "ColumnVpaF";
            this.ColumnVpaF.ReadOnly = true;
            // 
            // ColumnVpaN
            // 
            dataGridViewCellStyle7.Format = "N2";
            this.ColumnVpaN.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnVpaN, "ColumnVpaN");
            this.ColumnVpaN.Name = "ColumnVpaN";
            this.ColumnVpaN.ReadOnly = true;
            // 
            // buttonF
            // 
            resources.ApplyResources(this.buttonF, "buttonF");
            this.buttonF.Name = "buttonF";
            this.buttonF.UseVisualStyleBackColor = true;
            // 
            // numericUpDownF
            // 
            resources.ApplyResources(this.numericUpDownF, "numericUpDownF");
            this.numericUpDownF.DecimalPlaces = 3;
            this.numericUpDownF.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownF.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownF.Name = "numericUpDownF";
            this.numericUpDownF.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownF.ValueChanged += new System.EventHandler(this.vpa_Changed);
            // 
            // labelArea
            // 
            resources.ApplyResources(this.labelArea, "labelArea");
            this.labelArea.Name = "labelArea";
            // 
            // buttonM
            // 
            resources.ApplyResources(this.buttonM, "buttonM");
            this.buttonM.Name = "buttonM";
            this.buttonM.UseVisualStyleBackColor = true;
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
            this.numericUpDownM.ValueChanged += new System.EventHandler(this.vpa_Changed);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // pageResults
            // 
            this.pageResults.Controls.Add(this.spreadSheetResults);
            this.pageResults.Controls.Add(this.labelGear);
            this.pageResults.Controls.Add(this.comboBoxCohort);
            this.pageResults.Controls.Add(this.label3);
            this.pageResults.Name = "pageResults";
            resources.ApplyResources(this.pageResults, "pageResults");
            // 
            // spreadSheetResults
            // 
            resources.ApplyResources(this.spreadSheetResults, "spreadSheetResults");
            this.spreadSheetResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnResultAge,
            this.ColumnResultYear,
            this.ColumnResultC,
            this.ColumnResultF,
            this.ColumnResultN});
            this.spreadSheetResults.DefaultDecimalPlaces = 0;
            this.spreadSheetResults.Name = "spreadSheetResults";
            this.spreadSheetResults.ReadOnly = true;
            this.spreadSheetResults.RowHeadersVisible = false;
            this.spreadSheetResults.RowTemplate.Height = 35;
            this.spreadSheetResults.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnResultAge
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnResultAge.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnResultAge.Frozen = true;
            resources.ApplyResources(this.ColumnResultAge, "ColumnResultAge");
            this.ColumnResultAge.Name = "ColumnResultAge";
            this.ColumnResultAge.ReadOnly = true;
            // 
            // ColumnResultYear
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "0000";
            this.ColumnResultYear.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.ColumnResultYear, "ColumnResultYear");
            this.ColumnResultYear.Name = "ColumnResultYear";
            this.ColumnResultYear.ReadOnly = true;
            // 
            // ColumnResultC
            // 
            dataGridViewCellStyle10.Format = "N2";
            this.ColumnResultC.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.ColumnResultC, "ColumnResultC");
            this.ColumnResultC.Name = "ColumnResultC";
            this.ColumnResultC.ReadOnly = true;
            // 
            // ColumnResultF
            // 
            dataGridViewCellStyle11.Format = "N3";
            this.ColumnResultF.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnResultF, "ColumnResultF");
            this.ColumnResultF.Name = "ColumnResultF";
            this.ColumnResultF.ReadOnly = true;
            // 
            // ColumnResultN
            // 
            dataGridViewCellStyle12.Format = "N2";
            this.ColumnResultN.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.ColumnResultN, "ColumnResultN");
            this.ColumnResultN.Name = "ColumnResultN";
            this.ColumnResultN.ReadOnly = true;
            // 
            // labelGear
            // 
            resources.ApplyResources(this.labelGear, "labelGear");
            this.labelGear.Name = "labelGear";
            // 
            // comboBoxCohort
            // 
            resources.ApplyResources(this.comboBoxCohort, "comboBoxCohort");
            this.comboBoxCohort.DisplayMember = "Name";
            this.comboBoxCohort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCohort.FormattingEnabled = true;
            this.comboBoxCohort.Name = "comboBoxCohort";
            this.comboBoxCohort.Sorted = true;
            this.comboBoxCohort.SelectedIndexChanged += new System.EventHandler(this.comboBoxCohort_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // pageChart
            // 
            this.pageChart.Controls.Add(this.label9);
            this.pageChart.Controls.Add(this.comboBoxCohortChart);
            this.pageChart.Controls.Add(this.label8);
            this.pageChart.Controls.Add(this.statChartVpa);
            this.pageChart.Name = "pageChart";
            resources.ApplyResources(this.pageChart, "pageChart");
            this.pageChart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageChart_Commit);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // comboBoxCohortChart
            // 
            resources.ApplyResources(this.comboBoxCohortChart, "comboBoxCohortChart");
            this.comboBoxCohortChart.DisplayMember = "Name";
            this.comboBoxCohortChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCohortChart.FormattingEnabled = true;
            this.comboBoxCohortChart.Name = "comboBoxCohortChart";
            this.comboBoxCohortChart.Sorted = true;
            this.comboBoxCohortChart.SelectedIndexChanged += new System.EventHandler(this.comboBoxCohortChart_SelectedIndexChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // statChartVpa
            // 
            resources.ApplyResources(this.statChartVpa, "statChartVpa");
            this.statChartVpa.AxisXAutoMaximum = false;
            this.statChartVpa.AxisXAutoMinimum = false;
            this.statChartVpa.AxisXMax = 43197.63253224537D;
            this.statChartVpa.AxisXMin = 43197.63253224537D;
            this.statChartVpa.AxisYAutoMaximum = false;
            this.statChartVpa.AxisYAutoMinimum = false;
            this.statChartVpa.IsChronic = true;
            this.statChartVpa.Name = "statChartVpa";
            this.statChartVpa.ShowLegend = false;
            this.statChartVpa.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Years;
            // 
            // pageStock
            // 
            this.pageStock.Controls.Add(this.spreadSheetStock);
            this.pageStock.Controls.Add(this.label12);
            this.pageStock.Name = "pageStock";
            resources.ApplyResources(this.pageStock, "pageStock");
            // 
            // spreadSheetStock
            // 
            resources.ApplyResources(this.spreadSheetStock, "spreadSheetStock");
            this.spreadSheetStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStockAge});
            this.spreadSheetStock.DefaultDecimalPlaces = 0;
            this.spreadSheetStock.Name = "spreadSheetStock";
            this.spreadSheetStock.ReadOnly = true;
            this.spreadSheetStock.RowHeadersVisible = false;
            this.spreadSheetStock.RowTemplate.Height = 35;
            this.spreadSheetStock.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnStockAge
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnStockAge.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColumnStockAge.Frozen = true;
            resources.ApplyResources(this.ColumnStockAge, "ColumnStockAge");
            this.ColumnStockAge.Name = "ColumnStockAge";
            this.ColumnStockAge.ReadOnly = true;
            this.ColumnStockAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxAppVpa);
            this.pageReport.Controls.Add(this.checkBoxVpa);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxMortality);
            this.pageReport.Controls.Add(this.checkBoxCatchHistory);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxAppVpa
            // 
            resources.ApplyResources(this.checkBoxAppVpa, "checkBoxAppVpa");
            this.checkBoxAppVpa.Checked = true;
            this.checkBoxAppVpa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppVpa.Name = "checkBoxAppVpa";
            this.checkBoxAppVpa.UseVisualStyleBackColor = true;
            this.checkBoxAppVpa.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxVpa
            // 
            resources.ApplyResources(this.checkBoxVpa, "checkBoxVpa");
            this.checkBoxVpa.Checked = true;
            this.checkBoxVpa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVpa.Name = "checkBoxVpa";
            this.checkBoxVpa.UseVisualStyleBackColor = true;
            this.checkBoxVpa.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxMortality
            // 
            resources.ApplyResources(this.checkBoxMortality, "checkBoxMortality");
            this.checkBoxMortality.Name = "checkBoxMortality";
            this.checkBoxMortality.UseVisualStyleBackColor = true;
            // 
            // checkBoxCatchHistory
            // 
            resources.ApplyResources(this.checkBoxCatchHistory, "checkBoxCatchHistory");
            this.checkBoxCatchHistory.Checked = true;
            this.checkBoxCatchHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCatchHistory.Name = "checkBoxCatchHistory";
            this.checkBoxCatchHistory.UseVisualStyleBackColor = true;
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // calcCatches
            // 
            this.calcCatches.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calcCatches_DoWork);
            this.calcCatches.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calcCatches_RunWorkerCompleted);
            // 
            // calcAnnuals
            // 
            this.calcAnnuals.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calcAnnuals_DoWork);
            this.calcAnnuals.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calcAnnuals_RunWorkerCompleted);
            // 
            // contextAnnuals
            // 
            this.contextAnnuals.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextItemAnnualExplore});
            this.contextAnnuals.Name = "contextAnnuals";
            resources.ApplyResources(this.contextAnnuals, "contextAnnuals");
            // 
            // contextItemAnnualExplore
            // 
            this.contextItemAnnualExplore.Name = "contextItemAnnualExplore";
            resources.ApplyResources(this.contextItemAnnualExplore, "contextItemAnnualExplore");
            this.contextItemAnnualExplore.Click += new System.EventHandler(this.contextItemAnnualExplore_Click);
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
            // WizardVirtualPopulation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxWarn);
            this.Controls.Add(this.labelWarn);
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardVirtualPopulation";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageYield.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotYield)).EndInit();
            this.pageCatches.ResumeLayout(false);
            this.pageCatches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).EndInit();
            this.pageCohorts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCohorts)).EndInit();
            this.pageVpa.ResumeLayout(false);
            this.pageVpa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetVpa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).EndInit();
            this.pageResults.ResumeLayout(false);
            this.pageResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetResults)).EndInit();
            this.pageChart.ResumeLayout(false);
            this.pageChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChartVpa)).EndInit();
            this.pageStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetStock)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.contextAnnuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private AeroWizard.WizardPage pageReport;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxCatchHistory;
        private System.Windows.Forms.CheckBox checkBoxVpa;
        private System.Windows.Forms.CheckBox checkBoxMortality;
        private AeroWizard.WizardPage pageYield;
        private System.Windows.Forms.Label label1;
        private AeroWizard.WizardPage pageCatches;
        private System.Windows.Forms.Label labelNoData;
        private Controls.SpreadSheet spreadSheetCatches;
        private System.Windows.Forms.Label label2;
        private AeroWizard.WizardPage pageCohorts;
        private Controls.SpreadSheet spreadSheetCohorts;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker calcCatches;
        private AeroWizard.WizardPage pageResults;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelGear;
        private System.Windows.Forms.ComboBox comboBoxCohort;
        private System.ComponentModel.BackgroundWorker calcAnnuals;
        private AeroWizard.WizardPage pageVpa;
        private System.Windows.Forms.Button buttonM;
        private System.Windows.Forms.NumericUpDown numericUpDownM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private AeroWizard.WizardPage pageChart;
        private System.Windows.Forms.Label label8;
        private Mathematics.Charts.Plot statChartVpa;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxCohortChart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxCohortModel;
        private Controls.SpreadSheet spreadSheetVpa;
        private System.Windows.Forms.Button buttonF;
        private System.Windows.Forms.NumericUpDown numericUpDownF;
        private System.Windows.Forms.Label labelArea;
        private Controls.SpreadSheet spreadSheetResults;
        private AeroWizard.WizardPage pageStock;
        private Controls.SpreadSheet spreadSheetStock;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxAppVpa;
        private System.Windows.Forms.Button buttonAnnual;
        private System.Windows.Forms.ContextMenuStrip contextAnnuals;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCohAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVpaAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVpaYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVpaCatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVpaF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVpaN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnResultN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockAge;
        private Mathematics.Charts.Plot plotYield;
        private System.Windows.Forms.PictureBox pictureBoxWarn;
        private System.Windows.Forms.Label labelWarn;
        private System.Windows.Forms.ToolStripMenuItem contextItemAnnualExplore;
    }
}