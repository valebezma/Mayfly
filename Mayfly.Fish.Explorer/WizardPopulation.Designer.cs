namespace Mayfly.Fish.Explorer
{
    partial class WizardPopulation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardPopulation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.checkBoxAge = new System.Windows.Forms.CheckBox();
            this.checkBoxLength = new System.Windows.Forms.CheckBox();
            this.checkBoxLengthAdjusted = new System.Windows.Forms.CheckBox();
            this.checkBoxAgeAdjusted = new System.Windows.Forms.CheckBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageBasic = new AeroWizard.WizardPage();
            this.buttonL = new System.Windows.Forms.Button();
            this.buttonW = new System.Windows.Forms.Button();
            this.labelBasicInstruction = new System.Windows.Forms.Label();
            this.pageModelLW = new AeroWizard.WizardPage();
            this.buttonLW = new System.Windows.Forms.Button();
            this.labelWLInstruction = new System.Windows.Forms.Label();
            this.plotLW = new Mayfly.Mathematics.Charts.Plot();
            this.pageModelAL = new AeroWizard.WizardPage();
            this.buttonAL = new System.Windows.Forms.Button();
            this.labelALInstruction = new System.Windows.Forms.Label();
            this.plotAL = new Mayfly.Mathematics.Charts.Plot();
            this.pageModelAW = new AeroWizard.WizardPage();
            this.plotAW = new Mayfly.Mathematics.Charts.Plot();
            this.labelAWInstruction = new System.Windows.Forms.Label();
            this.pageCpue = new AeroWizard.WizardPage();
            this.labelCpueInstruction = new System.Windows.Forms.Label();
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
            this.pageLength = new AeroWizard.WizardPage();
            this.plotLength = new Mayfly.Mathematics.Charts.Plot();
            this.comboBoxLengthSource = new System.Windows.Forms.ComboBox();
            this.labelLengthInstruction = new System.Windows.Forms.Label();
            this.labelLengthSource = new System.Windows.Forms.Label();
            this.pageSelectionSource = new AeroWizard.WizardPage();
            this.pictureBoxSelectionSourceWarn = new System.Windows.Forms.PictureBox();
            this.labelSelectionSourceWarn = new System.Windows.Forms.Label();
            this.labelSelectionSourceInstruction = new System.Windows.Forms.Label();
            this.plotSelectionSource = new Mayfly.Mathematics.Charts.Plot();
            this.pageSelection = new AeroWizard.WizardPage();
            this.textBoxSD = new System.Windows.Forms.TextBox();
            this.labelSD = new System.Windows.Forms.Label();
            this.textBoxSF = new System.Windows.Forms.TextBox();
            this.labelSF = new System.Windows.Forms.Label();
            this.labelSelectionInstruction = new System.Windows.Forms.Label();
            this.plotSelection = new Mayfly.Mathematics.Charts.Plot();
            this.pageLengthAdjusted = new AeroWizard.WizardPage();
            this.labelLengthAdjustedInstruction = new System.Windows.Forms.Label();
            this.plotLengthAdjusted = new Mayfly.Mathematics.Charts.Plot();
            this.pageAge = new AeroWizard.WizardPage();
            this.plotAge = new Mayfly.Mathematics.Charts.Plot();
            this.comboBoxAgeSource = new System.Windows.Forms.ComboBox();
            this.labelAgeInstruction = new System.Windows.Forms.Label();
            this.labelAgeSource = new System.Windows.Forms.Label();
            this.pageMortality = new AeroWizard.WizardPage();
            this.pictureMortalityWarn = new System.Windows.Forms.PictureBox();
            this.labelMortalityWarn = new System.Windows.Forms.Label();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.labelSLabel = new System.Windows.Forms.Label();
            this.labelZLabel = new System.Windows.Forms.Label();
            this.textBoxFi = new System.Windows.Forms.TextBox();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.labelFiLabel = new System.Windows.Forms.Label();
            this.comboBoxMortalityAge = new System.Windows.Forms.ComboBox();
            this.labelMortalityAge = new System.Windows.Forms.Label();
            this.buttonMortality = new System.Windows.Forms.Button();
            this.labelMortalityInstruction = new System.Windows.Forms.Label();
            this.plotMortality = new Mayfly.Mathematics.Charts.Plot();
            this.pageAgeAdjusted = new AeroWizard.WizardPage();
            this.plotAgeAdjusted = new Mayfly.Mathematics.Charts.Plot();
            this.labelAgeAdjustedInstruction = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxReportAge = new System.Windows.Forms.CheckBox();
            this.checkBoxReportLength = new System.Windows.Forms.CheckBox();
            this.checkBoxReportLengthAdjusted = new System.Windows.Forms.CheckBox();
            this.checkBoxReportAgeAdjusted = new System.Windows.Forms.CheckBox();
            this.checkBoxReportAgeKeys = new System.Windows.Forms.CheckBox();
            this.checkBoxReportBasic = new System.Windows.Forms.CheckBox();
            this.checkBoxReportAgeCPUE = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxReportGears = new System.Windows.Forms.CheckBox();
            this.structureCalculator = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.classesCalculator = new System.ComponentModel.BackgroundWorker();
            this.selectionCalculator = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageBasic.SuspendLayout();
            this.pageModelLW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotLW)).BeginInit();
            this.pageModelAL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotAL)).BeginInit();
            this.pageModelAW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotAW)).BeginInit();
            this.pageCpue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).BeginInit();
            this.pageLength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotLength)).BeginInit();
            this.pageSelectionSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectionSourceWarn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotSelectionSource)).BeginInit();
            this.pageSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotSelection)).BeginInit();
            this.pageLengthAdjusted.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotLengthAdjusted)).BeginInit();
            this.pageAge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotAge)).BeginInit();
            this.pageMortality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMortalityWarn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotMortality)).BeginInit();
            this.pageAgeAdjusted.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotAgeAdjusted)).BeginInit();
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
            this.wizardExplorer.Pages.Add(this.pageModelLW);
            this.wizardExplorer.Pages.Add(this.pageModelAL);
            this.wizardExplorer.Pages.Add(this.pageModelAW);
            this.wizardExplorer.Pages.Add(this.pageCpue);
            this.wizardExplorer.Pages.Add(this.pageLength);
            this.wizardExplorer.Pages.Add(this.pageSelectionSource);
            this.wizardExplorer.Pages.Add(this.pageSelection);
            this.wizardExplorer.Pages.Add(this.pageLengthAdjusted);
            this.wizardExplorer.Pages.Add(this.pageAge);
            this.wizardExplorer.Pages.Add(this.pageMortality);
            this.wizardExplorer.Pages.Add(this.pageAgeAdjusted);
            this.wizardExplorer.Pages.Add(this.pageReport);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // pageStart
            // 
            this.pageStart.Controls.Add(this.checkBoxAge);
            this.pageStart.Controls.Add(this.checkBoxLength);
            this.pageStart.Controls.Add(this.checkBoxLengthAdjusted);
            this.pageStart.Controls.Add(this.checkBoxAgeAdjusted);
            this.pageStart.Controls.Add(this.labelStart);
            this.pageStart.Name = "pageStart";
            resources.ApplyResources(this.pageStart, "pageStart");
            this.pageStart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageStart_Commit);
            // 
            // checkBoxAge
            // 
            resources.ApplyResources(this.checkBoxAge, "checkBoxAge");
            this.checkBoxAge.Checked = true;
            this.checkBoxAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAge.Name = "checkBoxAge";
            this.checkBoxAge.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
            // 
            // checkBoxLength
            // 
            resources.ApplyResources(this.checkBoxLength, "checkBoxLength");
            this.checkBoxLength.Checked = true;
            this.checkBoxLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLength.Name = "checkBoxLength";
            this.checkBoxLength.CheckedChanged += new System.EventHandler(this.checkBoxLength_CheckedChanged);
            // 
            // checkBoxLengthAdjusted
            // 
            resources.ApplyResources(this.checkBoxLengthAdjusted, "checkBoxLengthAdjusted");
            this.checkBoxLengthAdjusted.Name = "checkBoxLengthAdjusted";
            this.checkBoxLengthAdjusted.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAgeAdjusted
            // 
            resources.ApplyResources(this.checkBoxAgeAdjusted, "checkBoxAgeAdjusted");
            this.checkBoxAgeAdjusted.Name = "checkBoxAgeAdjusted";
            this.checkBoxAgeAdjusted.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
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
            this.pageBasic.Controls.Add(this.labelBasicInstruction);
            this.pageBasic.Name = "pageBasic";
            resources.ApplyResources(this.pageBasic, "pageBasic");
            this.pageBasic.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageBasic_Commit);
            // 
            // buttonL
            // 
            resources.ApplyResources(this.buttonL, "buttonL");
            this.buttonL.Name = "buttonL";
            this.buttonL.UseVisualStyleBackColor = true;
            this.buttonL.Click += new System.EventHandler(this.buttonL_Click);
            // 
            // buttonW
            // 
            resources.ApplyResources(this.buttonW, "buttonW");
            this.buttonW.Name = "buttonW";
            this.buttonW.UseVisualStyleBackColor = true;
            this.buttonW.Click += new System.EventHandler(this.buttonW_Click);
            // 
            // labelBasicInstruction
            // 
            resources.ApplyResources(this.labelBasicInstruction, "labelBasicInstruction");
            this.labelBasicInstruction.Name = "labelBasicInstruction";
            // 
            // pageModelLW
            // 
            this.pageModelLW.Controls.Add(this.buttonLW);
            this.pageModelLW.Controls.Add(this.labelWLInstruction);
            this.pageModelLW.Controls.Add(this.plotLW);
            this.pageModelLW.Name = "pageModelLW";
            resources.ApplyResources(this.pageModelLW, "pageModelLW");
            this.pageModelLW.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageLW_Rollback);
            // 
            // buttonLW
            // 
            resources.ApplyResources(this.buttonLW, "buttonLW");
            this.buttonLW.Name = "buttonLW";
            this.buttonLW.UseVisualStyleBackColor = true;
            this.buttonLW.Click += new System.EventHandler(this.buttonLW_Click);
            // 
            // labelWLInstruction
            // 
            resources.ApplyResources(this.labelWLInstruction, "labelWLInstruction");
            this.labelWLInstruction.Name = "labelWLInstruction";
            // 
            // plotLW
            // 
            resources.ApplyResources(this.plotLW, "plotLW");
            this.plotLW.AxisXAutoMinimum = false;
            this.plotLW.AxisYAutoMinimum = false;
            this.plotLW.Name = "plotLW";
            this.plotLW.ShowLegend = false;
            this.plotLW.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageModelAL
            // 
            this.pageModelAL.Controls.Add(this.buttonAL);
            this.pageModelAL.Controls.Add(this.labelALInstruction);
            this.pageModelAL.Controls.Add(this.plotAL);
            this.pageModelAL.Name = "pageModelAL";
            resources.ApplyResources(this.pageModelAL, "pageModelAL");
            // 
            // buttonAL
            // 
            resources.ApplyResources(this.buttonAL, "buttonAL");
            this.buttonAL.Name = "buttonAL";
            this.buttonAL.UseVisualStyleBackColor = true;
            this.buttonAL.Click += new System.EventHandler(this.buttonAL_Click);
            // 
            // labelALInstruction
            // 
            resources.ApplyResources(this.labelALInstruction, "labelALInstruction");
            this.labelALInstruction.Name = "labelALInstruction";
            // 
            // plotAL
            // 
            resources.ApplyResources(this.plotAL, "plotAL");
            this.plotAL.AxisXAutoMinimum = false;
            this.plotAL.AxisYAutoMinimum = false;
            this.plotAL.Name = "plotAL";
            this.plotAL.ShowLegend = false;
            this.plotAL.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageModelAW
            // 
            this.pageModelAW.Controls.Add(this.plotAW);
            this.pageModelAW.Controls.Add(this.labelAWInstruction);
            this.pageModelAW.Name = "pageModelAW";
            resources.ApplyResources(this.pageModelAW, "pageModelAW");
            this.pageModelAW.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAW_Commit);
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
            // labelAWInstruction
            // 
            resources.ApplyResources(this.labelAWInstruction, "labelAWInstruction");
            this.labelAWInstruction.Name = "labelAWInstruction";
            // 
            // pageCpue
            // 
            this.pageCpue.Controls.Add(this.labelCpueInstruction);
            this.pageCpue.Controls.Add(this.spreadSheetSelectivity);
            this.pageCpue.Controls.Add(this.label2);
            this.pageCpue.Controls.Add(this.textBoxBpue);
            this.pageCpue.Controls.Add(this.labelNpueUnit);
            this.pageCpue.Controls.Add(this.textBoxNpue);
            this.pageCpue.Controls.Add(this.labelBpueUnit);
            this.pageCpue.Name = "pageCpue";
            resources.ApplyResources(this.pageCpue, "pageCpue");
            this.pageCpue.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCpue_Commit);
            this.pageCpue.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCpue_Rollback);
            // 
            // labelCpueInstruction
            // 
            resources.ApplyResources(this.labelCpueInstruction, "labelCpueInstruction");
            this.labelCpueInstruction.Name = "labelCpueInstruction";
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
            // pageLength
            // 
            this.pageLength.Controls.Add(this.plotLength);
            this.pageLength.Controls.Add(this.comboBoxLengthSource);
            this.pageLength.Controls.Add(this.labelLengthInstruction);
            this.pageLength.Controls.Add(this.labelLengthSource);
            this.pageLength.Name = "pageLength";
            resources.ApplyResources(this.pageLength, "pageLength");
            this.pageLength.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageLength_Commit);
            this.pageLength.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageLength_Rollback);
            // 
            // plotLength
            // 
            resources.ApplyResources(this.plotLength, "plotLength");
            this.plotLength.AxisXAutoMaximum = false;
            this.plotLength.AxisXAutoMinimum = false;
            this.plotLength.AxisYAutoMaximum = false;
            this.plotLength.AxisYAutoMinimum = false;
            this.plotLength.Name = "plotLength";
            this.plotLength.ShowLegend = false;
            this.plotLength.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // comboBoxLengthSource
            // 
            resources.ApplyResources(this.comboBoxLengthSource, "comboBoxLengthSource");
            this.comboBoxLengthSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLengthSource.FormattingEnabled = true;
            this.comboBoxLengthSource.Items.AddRange(new object[] {
            resources.GetString("comboBoxLengthSource.Items"),
            resources.GetString("comboBoxLengthSource.Items1")});
            this.comboBoxLengthSource.Name = "comboBoxLengthSource";
            this.comboBoxLengthSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxLengthSource_SelectedIndexChanged);
            // 
            // labelLengthInstruction
            // 
            resources.ApplyResources(this.labelLengthInstruction, "labelLengthInstruction");
            this.labelLengthInstruction.Name = "labelLengthInstruction";
            // 
            // labelLengthSource
            // 
            resources.ApplyResources(this.labelLengthSource, "labelLengthSource");
            this.labelLengthSource.Name = "labelLengthSource";
            // 
            // pageSelectionSource
            // 
            this.pageSelectionSource.Controls.Add(this.pictureBoxSelectionSourceWarn);
            this.pageSelectionSource.Controls.Add(this.labelSelectionSourceWarn);
            this.pageSelectionSource.Controls.Add(this.labelSelectionSourceInstruction);
            this.pageSelectionSource.Controls.Add(this.plotSelectionSource);
            this.pageSelectionSource.Name = "pageSelectionSource";
            resources.ApplyResources(this.pageSelectionSource, "pageSelectionSource");
            this.pageSelectionSource.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSelectionSource_Commit);
            this.pageSelectionSource.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.pageSelectionSource_Initialize);
            // 
            // pictureBoxSelectionSourceWarn
            // 
            resources.ApplyResources(this.pictureBoxSelectionSourceWarn, "pictureBoxSelectionSourceWarn");
            this.pictureBoxSelectionSourceWarn.Name = "pictureBoxSelectionSourceWarn";
            this.pictureBoxSelectionSourceWarn.TabStop = false;
            // 
            // labelSelectionSourceWarn
            // 
            resources.ApplyResources(this.labelSelectionSourceWarn, "labelSelectionSourceWarn");
            this.labelSelectionSourceWarn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelSelectionSourceWarn.Name = "labelSelectionSourceWarn";
            // 
            // labelSelectionSourceInstruction
            // 
            resources.ApplyResources(this.labelSelectionSourceInstruction, "labelSelectionSourceInstruction");
            this.labelSelectionSourceInstruction.Name = "labelSelectionSourceInstruction";
            // 
            // plotSelectionSource
            // 
            resources.ApplyResources(this.plotSelectionSource, "plotSelectionSource");
            this.plotSelectionSource.AxisXAutoInterval = false;
            this.plotSelectionSource.Name = "plotSelectionSource";
            this.plotSelectionSource.ShowLegend = true;
            this.plotSelectionSource.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageSelection
            // 
            this.pageSelection.Controls.Add(this.textBoxSD);
            this.pageSelection.Controls.Add(this.labelSD);
            this.pageSelection.Controls.Add(this.textBoxSF);
            this.pageSelection.Controls.Add(this.labelSF);
            this.pageSelection.Controls.Add(this.labelSelectionInstruction);
            this.pageSelection.Controls.Add(this.plotSelection);
            this.pageSelection.Name = "pageSelection";
            resources.ApplyResources(this.pageSelection, "pageSelection");
            // 
            // textBoxSD
            // 
            resources.ApplyResources(this.textBoxSD, "textBoxSD");
            this.textBoxSD.Name = "textBoxSD";
            this.textBoxSD.ReadOnly = true;
            // 
            // labelSD
            // 
            resources.ApplyResources(this.labelSD, "labelSD");
            this.labelSD.Name = "labelSD";
            // 
            // textBoxSF
            // 
            resources.ApplyResources(this.textBoxSF, "textBoxSF");
            this.textBoxSF.Name = "textBoxSF";
            this.textBoxSF.ReadOnly = true;
            // 
            // labelSF
            // 
            resources.ApplyResources(this.labelSF, "labelSF");
            this.labelSF.Name = "labelSF";
            // 
            // labelSelectionInstruction
            // 
            resources.ApplyResources(this.labelSelectionInstruction, "labelSelectionInstruction");
            this.labelSelectionInstruction.Name = "labelSelectionInstruction";
            // 
            // plotSelection
            // 
            resources.ApplyResources(this.plotSelection, "plotSelection");
            this.plotSelection.AxisXAutoInterval = false;
            this.plotSelection.AxisXAutoMaximum = false;
            this.plotSelection.AxisXAutoMinimum = false;
            this.plotSelection.AxisYAutoMaximum = false;
            this.plotSelection.AxisYAutoMinimum = false;
            this.plotSelection.AxisYInterval = 0.1D;
            this.plotSelection.AxisYMax = 1D;
            this.plotSelection.Name = "plotSelection";
            this.plotSelection.ShowLegend = true;
            this.plotSelection.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageLengthAdjusted
            // 
            this.pageLengthAdjusted.Controls.Add(this.labelLengthAdjustedInstruction);
            this.pageLengthAdjusted.Controls.Add(this.plotLengthAdjusted);
            this.pageLengthAdjusted.Name = "pageLengthAdjusted";
            resources.ApplyResources(this.pageLengthAdjusted, "pageLengthAdjusted");
            this.pageLengthAdjusted.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageLengthAdjusted_Commit);
            // 
            // labelLengthAdjustedInstruction
            // 
            resources.ApplyResources(this.labelLengthAdjustedInstruction, "labelLengthAdjustedInstruction");
            this.labelLengthAdjustedInstruction.Name = "labelLengthAdjustedInstruction";
            // 
            // plotLengthAdjusted
            // 
            resources.ApplyResources(this.plotLengthAdjusted, "plotLengthAdjusted");
            this.plotLengthAdjusted.AxisXAutoInterval = false;
            this.plotLengthAdjusted.AxisXAutoMaximum = false;
            this.plotLengthAdjusted.AxisXAutoMinimum = false;
            this.plotLengthAdjusted.AxisYAutoMaximum = false;
            this.plotLengthAdjusted.AxisYAutoMinimum = false;
            this.plotLengthAdjusted.AxisYMax = 1D;
            this.plotLengthAdjusted.Name = "plotLengthAdjusted";
            this.plotLengthAdjusted.ShowLegend = true;
            this.plotLengthAdjusted.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageAge
            // 
            this.pageAge.Controls.Add(this.plotAge);
            this.pageAge.Controls.Add(this.comboBoxAgeSource);
            this.pageAge.Controls.Add(this.labelAgeInstruction);
            this.pageAge.Controls.Add(this.labelAgeSource);
            this.pageAge.Name = "pageAge";
            resources.ApplyResources(this.pageAge, "pageAge");
            this.pageAge.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAge_Commit);
            this.pageAge.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAge_Rollback);
            // 
            // plotAge
            // 
            resources.ApplyResources(this.plotAge, "plotAge");
            this.plotAge.AxisXAutoMaximum = false;
            this.plotAge.AxisXAutoMinimum = false;
            this.plotAge.AxisYAutoMaximum = false;
            this.plotAge.AxisYAutoMinimum = false;
            this.plotAge.Name = "plotAge";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series1.Name = "juv";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series2.Name = "m";
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series2.Points.Add(dataPoint6);
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series3.Name = "f";
            series3.Points.Add(dataPoint7);
            series3.Points.Add(dataPoint8);
            series3.Points.Add(dataPoint9);
            this.plotAge.Series.Add(series1);
            this.plotAge.Series.Add(series2);
            this.plotAge.Series.Add(series3);
            this.plotAge.ShowLegend = false;
            this.plotAge.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // comboBoxAgeSource
            // 
            resources.ApplyResources(this.comboBoxAgeSource, "comboBoxAgeSource");
            this.comboBoxAgeSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAgeSource.FormattingEnabled = true;
            this.comboBoxAgeSource.Items.AddRange(new object[] {
            resources.GetString("comboBoxAgeSource.Items"),
            resources.GetString("comboBoxAgeSource.Items1")});
            this.comboBoxAgeSource.Name = "comboBoxAgeSource";
            this.comboBoxAgeSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxAgeSource_SelectedIndexChanged);
            // 
            // labelAgeInstruction
            // 
            resources.ApplyResources(this.labelAgeInstruction, "labelAgeInstruction");
            this.labelAgeInstruction.Name = "labelAgeInstruction";
            // 
            // labelAgeSource
            // 
            resources.ApplyResources(this.labelAgeSource, "labelAgeSource");
            this.labelAgeSource.Name = "labelAgeSource";
            // 
            // pageMortality
            // 
            this.pageMortality.Controls.Add(this.pictureMortalityWarn);
            this.pageMortality.Controls.Add(this.labelMortalityWarn);
            this.pageMortality.Controls.Add(this.textBoxS);
            this.pageMortality.Controls.Add(this.labelSLabel);
            this.pageMortality.Controls.Add(this.labelZLabel);
            this.pageMortality.Controls.Add(this.textBoxFi);
            this.pageMortality.Controls.Add(this.textBoxZ);
            this.pageMortality.Controls.Add(this.labelFiLabel);
            this.pageMortality.Controls.Add(this.comboBoxMortalityAge);
            this.pageMortality.Controls.Add(this.labelMortalityAge);
            this.pageMortality.Controls.Add(this.buttonMortality);
            this.pageMortality.Controls.Add(this.labelMortalityInstruction);
            this.pageMortality.Controls.Add(this.plotMortality);
            this.pageMortality.Name = "pageMortality";
            resources.ApplyResources(this.pageMortality, "pageMortality");
            this.pageMortality.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageMortality_Commit);
            this.pageMortality.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.pageMortality_Initialize);
            this.pageMortality.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageMortality_Rollback);
            // 
            // pictureMortalityWarn
            // 
            resources.ApplyResources(this.pictureMortalityWarn, "pictureMortalityWarn");
            this.pictureMortalityWarn.Name = "pictureMortalityWarn";
            this.pictureMortalityWarn.TabStop = false;
            // 
            // labelMortalityWarn
            // 
            resources.ApplyResources(this.labelMortalityWarn, "labelMortalityWarn");
            this.labelMortalityWarn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelMortalityWarn.Name = "labelMortalityWarn";
            // 
            // textBoxS
            // 
            resources.ApplyResources(this.textBoxS, "textBoxS");
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.ReadOnly = true;
            // 
            // labelSLabel
            // 
            resources.ApplyResources(this.labelSLabel, "labelSLabel");
            this.labelSLabel.Name = "labelSLabel";
            // 
            // labelZLabel
            // 
            resources.ApplyResources(this.labelZLabel, "labelZLabel");
            this.labelZLabel.Name = "labelZLabel";
            // 
            // textBoxFi
            // 
            resources.ApplyResources(this.textBoxFi, "textBoxFi");
            this.textBoxFi.Name = "textBoxFi";
            this.textBoxFi.ReadOnly = true;
            // 
            // textBoxZ
            // 
            resources.ApplyResources(this.textBoxZ, "textBoxZ");
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.ReadOnly = true;
            // 
            // labelFiLabel
            // 
            resources.ApplyResources(this.labelFiLabel, "labelFiLabel");
            this.labelFiLabel.Name = "labelFiLabel";
            // 
            // comboBoxMortalityAge
            // 
            resources.ApplyResources(this.comboBoxMortalityAge, "comboBoxMortalityAge");
            this.comboBoxMortalityAge.DisplayMember = "Name";
            this.comboBoxMortalityAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMortalityAge.FormattingEnabled = true;
            this.comboBoxMortalityAge.Name = "comboBoxMortalityAge";
            this.comboBoxMortalityAge.SelectedIndexChanged += new System.EventHandler(this.comboBoxMortalityAge_SelectedIndexChanged);
            // 
            // labelMortalityAge
            // 
            resources.ApplyResources(this.labelMortalityAge, "labelMortalityAge");
            this.labelMortalityAge.Name = "labelMortalityAge";
            // 
            // buttonMortality
            // 
            resources.ApplyResources(this.buttonMortality, "buttonMortality");
            this.buttonMortality.Name = "buttonMortality";
            this.buttonMortality.UseVisualStyleBackColor = true;
            this.buttonMortality.Click += new System.EventHandler(this.buttonMortality_Click);
            // 
            // labelMortalityInstruction
            // 
            resources.ApplyResources(this.labelMortalityInstruction, "labelMortalityInstruction");
            this.labelMortalityInstruction.Name = "labelMortalityInstruction";
            // 
            // plotMortality
            // 
            resources.ApplyResources(this.plotMortality, "plotMortality");
            this.plotMortality.AxisXAutoMinimum = false;
            this.plotMortality.AxisYAutoMinimum = false;
            this.plotMortality.Name = "plotMortality";
            this.plotMortality.ShowLegend = false;
            this.plotMortality.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageAgeAdjusted
            // 
            this.pageAgeAdjusted.Controls.Add(this.plotAgeAdjusted);
            this.pageAgeAdjusted.Controls.Add(this.labelAgeAdjustedInstruction);
            this.pageAgeAdjusted.Name = "pageAgeAdjusted";
            resources.ApplyResources(this.pageAgeAdjusted, "pageAgeAdjusted");
            this.pageAgeAdjusted.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageMortality_Commit);
            // 
            // plotAgeAdjusted
            // 
            resources.ApplyResources(this.plotAgeAdjusted, "plotAgeAdjusted");
            this.plotAgeAdjusted.AxisXAutoInterval = false;
            this.plotAgeAdjusted.AxisXAutoMaximum = false;
            this.plotAgeAdjusted.AxisXAutoMinimum = false;
            this.plotAgeAdjusted.AxisYAutoMaximum = false;
            this.plotAgeAdjusted.AxisYAutoMinimum = false;
            this.plotAgeAdjusted.AxisYMax = 1D;
            this.plotAgeAdjusted.Name = "plotAgeAdjusted";
            this.plotAgeAdjusted.ShowLegend = true;
            this.plotAgeAdjusted.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // labelAgeAdjustedInstruction
            // 
            resources.ApplyResources(this.labelAgeAdjustedInstruction, "labelAgeAdjustedInstruction");
            this.labelAgeAdjustedInstruction.Name = "labelAgeAdjustedInstruction";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxReportAge);
            this.pageReport.Controls.Add(this.checkBoxReportLength);
            this.pageReport.Controls.Add(this.checkBoxReportLengthAdjusted);
            this.pageReport.Controls.Add(this.checkBoxReportAgeAdjusted);
            this.pageReport.Controls.Add(this.checkBoxReportAgeKeys);
            this.pageReport.Controls.Add(this.checkBoxReportBasic);
            this.pageReport.Controls.Add(this.checkBoxReportAgeCPUE);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxReportGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            this.pageReport.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Rollback);
            // 
            // checkBoxReportAge
            // 
            resources.ApplyResources(this.checkBoxReportAge, "checkBoxReportAge");
            this.checkBoxReportAge.Checked = true;
            this.checkBoxReportAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportAge.Name = "checkBoxReportAge";
            this.checkBoxReportAge.CheckedChanged += new System.EventHandler(this.checkBoxReportAge_CheckedChanged);
            this.checkBoxReportAge.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxReportLength
            // 
            resources.ApplyResources(this.checkBoxReportLength, "checkBoxReportLength");
            this.checkBoxReportLength.Checked = true;
            this.checkBoxReportLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportLength.Name = "checkBoxReportLength";
            this.checkBoxReportLength.CheckedChanged += new System.EventHandler(this.checkBoxReportLength_CheckedChanged);
            this.checkBoxReportLength.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxReportLengthAdjusted
            // 
            resources.ApplyResources(this.checkBoxReportLengthAdjusted, "checkBoxReportLengthAdjusted");
            this.checkBoxReportLengthAdjusted.Checked = true;
            this.checkBoxReportLengthAdjusted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportLengthAdjusted.Name = "checkBoxReportLengthAdjusted";
            this.checkBoxReportLengthAdjusted.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxReportAgeAdjusted
            // 
            resources.ApplyResources(this.checkBoxReportAgeAdjusted, "checkBoxReportAgeAdjusted");
            this.checkBoxReportAgeAdjusted.Checked = true;
            this.checkBoxReportAgeAdjusted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportAgeAdjusted.Name = "checkBoxReportAgeAdjusted";
            this.checkBoxReportAgeAdjusted.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxReportAgeKeys
            // 
            resources.ApplyResources(this.checkBoxReportAgeKeys, "checkBoxReportAgeKeys");
            this.checkBoxReportAgeKeys.Checked = true;
            this.checkBoxReportAgeKeys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportAgeKeys.Name = "checkBoxReportAgeKeys";
            this.checkBoxReportAgeKeys.UseVisualStyleBackColor = true;
            this.checkBoxReportAgeKeys.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxReportBasic
            // 
            resources.ApplyResources(this.checkBoxReportBasic, "checkBoxReportBasic");
            this.checkBoxReportBasic.Checked = true;
            this.checkBoxReportBasic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportBasic.Name = "checkBoxReportBasic";
            this.checkBoxReportBasic.UseVisualStyleBackColor = true;
            // 
            // checkBoxReportAgeCPUE
            // 
            resources.ApplyResources(this.checkBoxReportAgeCPUE, "checkBoxReportAgeCPUE");
            this.checkBoxReportAgeCPUE.Checked = true;
            this.checkBoxReportAgeCPUE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportAgeCPUE.Name = "checkBoxReportAgeCPUE";
            this.checkBoxReportAgeCPUE.UseVisualStyleBackColor = true;
            this.checkBoxReportAgeCPUE.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxReportGears
            // 
            resources.ApplyResources(this.checkBoxReportGears, "checkBoxReportGears");
            this.checkBoxReportGears.Checked = true;
            this.checkBoxReportGears.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReportGears.Name = "checkBoxReportGears";
            this.checkBoxReportGears.UseVisualStyleBackColor = true;
            this.checkBoxReportGears.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
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
            // classesCalculator
            // 
            this.classesCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.classesCalculator_DoWork);
            this.classesCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.classesCalculator_RunWorkerCompleted);
            // 
            // selectionCalculator
            // 
            this.selectionCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.selectionCalculator_DoWork);
            this.selectionCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.selectionCalculator_RunWorkerCompleted);
            // 
            // WizardPopulation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardPopulation";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageStart.PerformLayout();
            this.pageBasic.ResumeLayout(false);
            this.pageModelLW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotLW)).EndInit();
            this.pageModelAL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotAL)).EndInit();
            this.pageModelAW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotAW)).EndInit();
            this.pageCpue.ResumeLayout(false);
            this.pageCpue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).EndInit();
            this.pageLength.ResumeLayout(false);
            this.pageLength.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotLength)).EndInit();
            this.pageSelectionSource.ResumeLayout(false);
            this.pageSelectionSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectionSourceWarn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotSelectionSource)).EndInit();
            this.pageSelection.ResumeLayout(false);
            this.pageSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotSelection)).EndInit();
            this.pageLengthAdjusted.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotLengthAdjusted)).EndInit();
            this.pageAge.ResumeLayout(false);
            this.pageAge.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotAge)).EndInit();
            this.pageMortality.ResumeLayout(false);
            this.pageMortality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMortalityWarn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotMortality)).EndInit();
            this.pageAgeAdjusted.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotAgeAdjusted)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxReportGears;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageAge;
        private Mathematics.Charts.Plot plotAge;
        private System.Windows.Forms.Label labelAgeInstruction;
        private System.Windows.Forms.CheckBox checkBoxReportAgeCPUE;
        private System.Windows.Forms.CheckBox checkBoxReportAgeKeys;
        private AeroWizard.WizardPage pageBasic;
        private System.Windows.Forms.Label labelBasicInstruction;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonW;
        private AeroWizard.WizardPage pageCpue;
        private Controls.SpreadSheet spreadSheetSelectivity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBpue;
        private System.Windows.Forms.Label labelNpueUnit;
        private System.Windows.Forms.TextBox textBoxNpue;
        private System.Windows.Forms.Label labelBpueUnit;
        private System.ComponentModel.BackgroundWorker classesCalculator;
        private System.Windows.Forms.Label labelCpueInstruction;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityW;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityL;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityB;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivitySex;
        private System.Windows.Forms.Label labelAgeSource;
        private System.Windows.Forms.ComboBox comboBoxAgeSource;
        private AeroWizard.WizardPage pageSelection;
        private System.Windows.Forms.TextBox textBoxSD;
        private System.Windows.Forms.Label labelSD;
        private System.Windows.Forms.TextBox textBoxSF;
        private System.Windows.Forms.Label labelSF;
        private System.Windows.Forms.Label labelSelectionInstruction;
        private Mathematics.Charts.Plot plotSelection;
        private AeroWizard.WizardPage pageLengthAdjusted;
        private System.Windows.Forms.Label labelLengthAdjustedInstruction;
        private Mathematics.Charts.Plot plotLengthAdjusted;
        private AeroWizard.WizardPage pageSelectionSource;
        private System.Windows.Forms.PictureBox pictureBoxSelectionSourceWarn;
        private System.Windows.Forms.Label labelSelectionSourceWarn;
        private System.Windows.Forms.Label labelSelectionSourceInstruction;
        private Mathematics.Charts.Plot plotSelectionSource;
        private System.ComponentModel.BackgroundWorker selectionCalculator;
        private AeroWizard.WizardPage pageModelAL;
        private AeroWizard.WizardPage pageModelLW;
        private AeroWizard.WizardPage pageModelAW;
        private System.Windows.Forms.Button buttonLW;
        private System.Windows.Forms.Label labelWLInstruction;
        private Mathematics.Charts.Plot plotAL;
        private System.Windows.Forms.Button buttonAL;
        private System.Windows.Forms.Label labelALInstruction;
        private Mathematics.Charts.Plot plotAW;
        private System.Windows.Forms.Label labelAWInstruction;
        private System.Windows.Forms.CheckBox checkBoxReportBasic;
        private AeroWizard.WizardPage pageMortality;
        private AeroWizard.WizardPage pageAgeAdjusted;
        private System.Windows.Forms.ComboBox comboBoxMortalityAge;
        private System.Windows.Forms.Label labelMortalityAge;
        private System.Windows.Forms.Button buttonMortality;
        private Mathematics.Charts.Plot plotMortality;
        private System.Windows.Forms.Label labelMortalityInstruction;
        private System.Windows.Forms.Label labelAgeAdjustedInstruction;
        private Mathematics.Charts.Plot plotLW;
        private System.Windows.Forms.CheckBox checkBoxAge;
        private System.Windows.Forms.CheckBox checkBoxLength;
        private System.Windows.Forms.CheckBox checkBoxAgeAdjusted;
        private System.Windows.Forms.CheckBox checkBoxLengthAdjusted;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.Label labelSLabel;
        private System.Windows.Forms.Label labelZLabel;
        private System.Windows.Forms.TextBox textBoxFi;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.Label labelFiLabel;
        private AeroWizard.WizardPage pageLength;
        private Mathematics.Charts.Plot plotLength;
        private System.Windows.Forms.ComboBox comboBoxLengthSource;
        private System.Windows.Forms.Label labelLengthInstruction;
        private System.Windows.Forms.Label labelLengthSource;
        private Mathematics.Charts.Plot plotAgeAdjusted;
        private System.Windows.Forms.CheckBox checkBoxReportAge;
        private System.Windows.Forms.CheckBox checkBoxReportLength;
        private System.Windows.Forms.CheckBox checkBoxReportLengthAdjusted;
        private System.Windows.Forms.CheckBox checkBoxReportAgeAdjusted;
        private System.Windows.Forms.PictureBox pictureMortalityWarn;
        private System.Windows.Forms.Label labelMortalityWarn;
    }
}