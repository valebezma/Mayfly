namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardExtrapolations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardExtrapolations));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pageWater = new AeroWizard.WizardPage();
            this.labelWaterInstruction = new System.Windows.Forms.Label();
            this.numericUpDownVolume = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDepth = new System.Windows.Forms.NumericUpDown();
            this.labelVolume = new System.Windows.Forms.Label();
            this.numericUpDownArea = new System.Windows.Forms.NumericUpDown();
            this.labelDepth = new System.Windows.Forms.Label();
            this.labelArea = new System.Windows.Forms.Label();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageStocks = new AeroWizard.WizardPage();
            this.labelStocksInstruction = new System.Windows.Forms.Label();
            this.spreadSheetStocks = new Mayfly.Controls.SpreadSheet();
            this.ColumnSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAbundance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBiomass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGamingLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGamingAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGamingN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGamingB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxAbundance = new System.Windows.Forms.CheckBox();
            this.checkBoxStocks = new System.Windows.Forms.CheckBox();
            this.checkBoxCommunity = new System.Windows.Forms.CheckBox();
            this.checkBoxCPUE = new System.Windows.Forms.CheckBox();
            this.checkBoxCatches = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.pageWater.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageStocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetStocks)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageWater
            // 
            this.pageWater.Controls.Add(this.labelWaterInstruction);
            this.pageWater.Controls.Add(this.numericUpDownVolume);
            this.pageWater.Controls.Add(this.numericUpDownDepth);
            this.pageWater.Controls.Add(this.labelVolume);
            this.pageWater.Controls.Add(this.numericUpDownArea);
            this.pageWater.Controls.Add(this.labelDepth);
            this.pageWater.Controls.Add(this.labelArea);
            this.pageWater.Name = "pageWater";
            resources.ApplyResources(this.pageWater, "pageWater");
            this.pageWater.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageWater_Commit);
            this.pageWater.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.pageWater_Initialize);
            this.pageWater.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageWater_Rollback);
            // 
            // labelWaterInstruction
            // 
            resources.ApplyResources(this.labelWaterInstruction, "labelWaterInstruction");
            this.labelWaterInstruction.Name = "labelWaterInstruction";
            // 
            // numericUpDownVolume
            // 
            resources.ApplyResources(this.numericUpDownVolume, "numericUpDownVolume");
            this.numericUpDownVolume.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownVolume.Name = "numericUpDownVolume";
            this.numericUpDownVolume.ReadOnly = true;
            this.numericUpDownVolume.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownDepth
            // 
            resources.ApplyResources(this.numericUpDownDepth, "numericUpDownDepth");
            this.numericUpDownDepth.DecimalPlaces = 2;
            this.numericUpDownDepth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDepth.Name = "numericUpDownDepth";
            this.numericUpDownDepth.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.numericUpDownDepth.ValueChanged += new System.EventHandler(this.numericUpDownDepth_ValueChanged);
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // numericUpDownArea
            // 
            resources.ApplyResources(this.numericUpDownArea, "numericUpDownArea");
            this.numericUpDownArea.DecimalPlaces = 1;
            this.numericUpDownArea.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownArea.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownArea.Name = "numericUpDownArea";
            this.numericUpDownArea.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownArea.ValueChanged += new System.EventHandler(this.numericUpDownArea_ValueChanged);
            // 
            // labelDepth
            // 
            resources.ApplyResources(this.labelDepth, "labelDepth");
            this.labelDepth.Name = "labelDepth";
            // 
            // labelArea
            // 
            resources.ApplyResources(this.labelArea, "labelArea");
            this.labelArea.Name = "labelArea";
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageWater);
            this.wizardExplorer.Pages.Add(this.pageStocks);
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
            // pageStocks
            // 
            this.pageStocks.Controls.Add(this.labelStocksInstruction);
            this.pageStocks.Controls.Add(this.spreadSheetStocks);
            this.pageStocks.Name = "pageStocks";
            resources.ApplyResources(this.pageStocks, "pageStocks");
            this.pageStocks.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageStocks_Commit);
            // 
            // labelStocksInstruction
            // 
            resources.ApplyResources(this.labelStocksInstruction, "labelStocksInstruction");
            this.labelStocksInstruction.Name = "labelStocksInstruction";
            // 
            // spreadSheetStocks
            // 
            resources.ApplyResources(this.spreadSheetStocks, "spreadSheetStocks");
            this.spreadSheetStocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpecies,
            this.ColumnAbundance,
            this.ColumnBiomass,
            this.ColumnN,
            this.ColumnB,
            this.ColumnGamingLength,
            this.ColumnGamingAge,
            this.ColumnGamingN,
            this.ColumnGamingB});
            this.spreadSheetStocks.DefaultDecimalPlaces = 3;
            this.spreadSheetStocks.Name = "spreadSheetStocks";
            this.spreadSheetStocks.RowHeadersVisible = false;
            this.spreadSheetStocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.spreadSheetStocks.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetStocks_CellValueChanged);
            // 
            // ColumnSpecies
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnSpecies.FillWeight = 150F;
            this.ColumnSpecies.Frozen = true;
            resources.ApplyResources(this.ColumnSpecies, "ColumnSpecies");
            this.ColumnSpecies.Name = "ColumnSpecies";
            this.ColumnSpecies.ReadOnly = true;
            // 
            // ColumnAbundance
            // 
            resources.ApplyResources(this.ColumnAbundance, "ColumnAbundance");
            this.ColumnAbundance.Name = "ColumnAbundance";
            // 
            // ColumnBiomass
            // 
            resources.ApplyResources(this.ColumnBiomass, "ColumnBiomass");
            this.ColumnBiomass.Name = "ColumnBiomass";
            // 
            // ColumnN
            // 
            dataGridViewCellStyle2.Format = "N3";
            this.ColumnN.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnN, "ColumnN");
            this.ColumnN.Name = "ColumnN";
            this.ColumnN.ReadOnly = true;
            // 
            // ColumnB
            // 
            dataGridViewCellStyle3.Format = "N3";
            this.ColumnB.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnB, "ColumnB");
            this.ColumnB.Name = "ColumnB";
            this.ColumnB.ReadOnly = true;
            // 
            // ColumnGamingLength
            // 
            dataGridViewCellStyle4.Format = "0 см";
            this.ColumnGamingLength.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnGamingLength, "ColumnGamingLength");
            this.ColumnGamingLength.Name = "ColumnGamingLength";
            // 
            // ColumnGamingAge
            // 
            dataGridViewCellStyle5.Format = "0+";
            this.ColumnGamingAge.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnGamingAge, "ColumnGamingAge");
            this.ColumnGamingAge.Name = "ColumnGamingAge";
            // 
            // ColumnGamingN
            // 
            dataGridViewCellStyle6.Format = "N3";
            this.ColumnGamingN.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnGamingN, "ColumnGamingN");
            this.ColumnGamingN.Name = "ColumnGamingN";
            this.ColumnGamingN.ReadOnly = true;
            // 
            // ColumnGamingB
            // 
            dataGridViewCellStyle7.Format = "N3";
            this.ColumnGamingB.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnGamingB, "ColumnGamingB");
            this.ColumnGamingB.Name = "ColumnGamingB";
            this.ColumnGamingB.ReadOnly = true;
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxAbundance);
            this.pageReport.Controls.Add(this.checkBoxStocks);
            this.pageReport.Controls.Add(this.checkBoxCommunity);
            this.pageReport.Controls.Add(this.checkBoxCPUE);
            this.pageReport.Controls.Add(this.checkBoxCatches);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxAbundance
            // 
            resources.ApplyResources(this.checkBoxAbundance, "checkBoxAbundance");
            this.checkBoxAbundance.Checked = true;
            this.checkBoxAbundance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAbundance.Name = "checkBoxAbundance";
            this.checkBoxAbundance.UseVisualStyleBackColor = true;
            // 
            // checkBoxStocks
            // 
            resources.ApplyResources(this.checkBoxStocks, "checkBoxStocks");
            this.checkBoxStocks.Checked = true;
            this.checkBoxStocks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStocks.Name = "checkBoxStocks";
            this.checkBoxStocks.UseVisualStyleBackColor = true;
            // 
            // checkBoxCommunity
            // 
            resources.ApplyResources(this.checkBoxCommunity, "checkBoxCommunity");
            this.checkBoxCommunity.Checked = true;
            this.checkBoxCommunity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCommunity.Name = "checkBoxCommunity";
            this.checkBoxCommunity.UseVisualStyleBackColor = true;
            // 
            // checkBoxCPUE
            // 
            resources.ApplyResources(this.checkBoxCPUE, "checkBoxCPUE");
            this.checkBoxCPUE.Checked = true;
            this.checkBoxCPUE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCPUE.Name = "checkBoxCPUE";
            this.checkBoxCPUE.UseVisualStyleBackColor = true;
            this.checkBoxCPUE.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxCatches
            // 
            resources.ApplyResources(this.checkBoxCatches, "checkBoxCatches");
            this.checkBoxCatches.Checked = true;
            this.checkBoxCatches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCatches.Name = "checkBoxCatches";
            this.checkBoxCatches.UseVisualStyleBackColor = true;
            this.checkBoxCatches.CheckedChanged += new System.EventHandler(this.checkBoxCatches_CheckedChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxGears
            // 
            resources.ApplyResources(this.checkBoxGears, "checkBoxGears");
            this.checkBoxGears.Checked = true;
            this.checkBoxGears.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGears.Name = "checkBoxGears";
            this.checkBoxGears.UseVisualStyleBackColor = true;
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // WizardExtrapolations
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardExtrapolations";
            this.pageWater.ResumeLayout(false);
            this.pageWater.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageStocks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetStocks)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPage pageWater;
        private System.Windows.Forms.Label labelWaterInstruction;
        private System.Windows.Forms.NumericUpDown numericUpDownVolume;
        private System.Windows.Forms.NumericUpDown numericUpDownDepth;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.NumericUpDown numericUpDownArea;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.Label labelArea;
        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private AeroWizard.WizardPage pageReport;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.CheckBox checkBoxCommunity;
        private System.Windows.Forms.CheckBox checkBoxCPUE;
        private System.Windows.Forms.CheckBox checkBoxCatches;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private System.Windows.Forms.CheckBox checkBoxStocks;
        private AeroWizard.WizardPage pageStocks;
        private System.Windows.Forms.Label labelStocksInstruction;
        public Controls.SpreadSheet spreadSheetStocks;
        private System.Windows.Forms.CheckBox checkBoxAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBiomass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGamingLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGamingAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGamingN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGamingB;
    }
}