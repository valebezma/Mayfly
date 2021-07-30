namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardCenosis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardCenosis));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageSelectivity = new AeroWizard.WizardPage();
            this.labelNoticeGearsSelectivity = new System.Windows.Forms.Label();
            this.labelSelectivityGroupInstruction = new System.Windows.Forms.Label();
            this.comboBoxDataset = new System.Windows.Forms.ComboBox();
            this.labelSelectivityGroupSelect = new System.Windows.Forms.Label();
            this.spreadSheetSelectivity = new Mayfly.Controls.SpreadSheet();
            this.columnSelectivitySpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityNpue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityNPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityBpue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSelectivityBPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageBiology = new AeroWizard.WizardPage();
            this.labelBioInstruction = new System.Windows.Forms.Label();
            this.spreadSheetBiology = new Mayfly.Controls.SpreadSheet();
            this.columnBioSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxAppCatches = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxAppAB = new System.Windows.Forms.CheckBox();
            this.checkBoxAppCPUE = new System.Windows.Forms.CheckBox();
            this.checkBoxCenosis = new System.Windows.Forms.CheckBox();
            this.checkBoxCatches = new System.Windows.Forms.CheckBox();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.calculatorCenosis = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.calculatorSelectivity = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageSelectivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).BeginInit();
            this.pageBiology.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetBiology)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageSelectivity);
            this.wizardExplorer.Pages.Add(this.pageBiology);
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
            // pageSelectivity
            // 
            this.pageSelectivity.Controls.Add(this.labelNoticeGearsSelectivity);
            this.pageSelectivity.Controls.Add(this.labelSelectivityGroupInstruction);
            this.pageSelectivity.Controls.Add(this.comboBoxDataset);
            this.pageSelectivity.Controls.Add(this.labelSelectivityGroupSelect);
            this.pageSelectivity.Controls.Add(this.spreadSheetSelectivity);
            this.pageSelectivity.Name = "pageSelectivity";
            resources.ApplyResources(this.pageSelectivity, "pageSelectivity");
            this.pageSelectivity.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSelectivity_Commit);
            this.pageSelectivity.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSelectivity_Rollback);
            // 
            // labelNoticeGearsSelectivity
            // 
            resources.ApplyResources(this.labelNoticeGearsSelectivity, "labelNoticeGearsSelectivity");
            this.labelNoticeGearsSelectivity.Name = "labelNoticeGearsSelectivity";
            // 
            // labelSelectivityGroupInstruction
            // 
            resources.ApplyResources(this.labelSelectivityGroupInstruction, "labelSelectivityGroupInstruction");
            this.labelSelectivityGroupInstruction.Name = "labelSelectivityGroupInstruction";
            // 
            // comboBoxDataset
            // 
            resources.ApplyResources(this.comboBoxDataset, "comboBoxDataset");
            this.comboBoxDataset.DisplayMember = "Name";
            this.comboBoxDataset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataset.FormattingEnabled = true;
            this.comboBoxDataset.Name = "comboBoxDataset";
            this.comboBoxDataset.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataset_SelectedIndexChanged);
            // 
            // labelSelectivityGroupSelect
            // 
            resources.ApplyResources(this.labelSelectivityGroupSelect, "labelSelectivityGroupSelect");
            this.labelSelectivityGroupSelect.Name = "labelSelectivityGroupSelect";
            // 
            // spreadSheetSelectivity
            // 
            resources.ApplyResources(this.spreadSheetSelectivity, "spreadSheetSelectivity");
            this.spreadSheetSelectivity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSelectivitySpecies,
            this.columnSelectivityLength,
            this.columnSelectivityMass,
            this.columnSelectivityN,
            this.columnSelectivityNpue,
            this.columnSelectivityNPer,
            this.columnSelectivityB,
            this.columnSelectivityBpue,
            this.columnSelectivityBPer});
            this.spreadSheetSelectivity.DefaultDecimalPlaces = 0;
            this.spreadSheetSelectivity.Name = "spreadSheetSelectivity";
            this.spreadSheetSelectivity.ReadOnly = true;
            this.spreadSheetSelectivity.RowHeadersVisible = false;
            this.spreadSheetSelectivity.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetSelectivity.RowTemplate.Height = 35;
            this.spreadSheetSelectivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // columnSelectivitySpecies
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSelectivitySpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnSelectivitySpecies.FillWeight = 150F;
            this.columnSelectivitySpecies.Frozen = true;
            resources.ApplyResources(this.columnSelectivitySpecies, "columnSelectivitySpecies");
            this.columnSelectivitySpecies.Name = "columnSelectivitySpecies";
            this.columnSelectivitySpecies.ReadOnly = true;
            // 
            // columnSelectivityLength
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSelectivityLength.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnSelectivityLength, "columnSelectivityLength");
            this.columnSelectivityLength.Name = "columnSelectivityLength";
            this.columnSelectivityLength.ReadOnly = true;
            // 
            // columnSelectivityMass
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "G";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSelectivityMass.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnSelectivityMass, "columnSelectivityMass");
            this.columnSelectivityMass.Name = "columnSelectivityMass";
            this.columnSelectivityMass.ReadOnly = true;
            // 
            // columnSelectivityN
            // 
            dataGridViewCellStyle4.Format = "N0";
            this.columnSelectivityN.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnSelectivityN, "columnSelectivityN");
            this.columnSelectivityN.Name = "columnSelectivityN";
            this.columnSelectivityN.ReadOnly = true;
            // 
            // columnSelectivityNpue
            // 
            dataGridViewCellStyle5.Format = "N3";
            this.columnSelectivityNpue.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.columnSelectivityNpue, "columnSelectivityNpue");
            this.columnSelectivityNpue.Name = "columnSelectivityNpue";
            this.columnSelectivityNpue.ReadOnly = true;
            // 
            // columnSelectivityNPer
            // 
            dataGridViewCellStyle6.Format = "P1";
            this.columnSelectivityNPer.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.columnSelectivityNPer, "columnSelectivityNPer");
            this.columnSelectivityNPer.Name = "columnSelectivityNPer";
            this.columnSelectivityNPer.ReadOnly = true;
            // 
            // columnSelectivityB
            // 
            dataGridViewCellStyle7.Format = "N3";
            this.columnSelectivityB.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.columnSelectivityB, "columnSelectivityB");
            this.columnSelectivityB.Name = "columnSelectivityB";
            this.columnSelectivityB.ReadOnly = true;
            // 
            // columnSelectivityBpue
            // 
            dataGridViewCellStyle8.Format = "N3";
            this.columnSelectivityBpue.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.columnSelectivityBpue, "columnSelectivityBpue");
            this.columnSelectivityBpue.Name = "columnSelectivityBpue";
            this.columnSelectivityBpue.ReadOnly = true;
            // 
            // columnSelectivityBPer
            // 
            dataGridViewCellStyle9.Format = "P1";
            this.columnSelectivityBPer.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.columnSelectivityBPer, "columnSelectivityBPer");
            this.columnSelectivityBPer.Name = "columnSelectivityBPer";
            this.columnSelectivityBPer.ReadOnly = true;
            // 
            // pageBiology
            // 
            this.pageBiology.Controls.Add(this.labelBioInstruction);
            this.pageBiology.Controls.Add(this.spreadSheetBiology);
            this.pageBiology.Name = "pageBiology";
            resources.ApplyResources(this.pageBiology, "pageBiology");
            this.pageBiology.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageBiology_Commit);
            // 
            // labelBioInstruction
            // 
            resources.ApplyResources(this.labelBioInstruction, "labelBioInstruction");
            this.labelBioInstruction.Name = "labelBioInstruction";
            // 
            // spreadSheetBiology
            // 
            this.spreadSheetBiology.AllowUserToHideRows = true;
            resources.ApplyResources(this.spreadSheetBiology, "spreadSheetBiology");
            this.spreadSheetBiology.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnBioSpecies,
            this.columnBioLength,
            this.columnBioMass,
            this.columnBioN,
            this.columnBioB});
            this.spreadSheetBiology.DefaultDecimalPlaces = 0;
            this.spreadSheetBiology.Name = "spreadSheetBiology";
            this.spreadSheetBiology.RowHeadersVisible = false;
            this.spreadSheetBiology.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetBiology.RowTemplate.Height = 35;
            this.spreadSheetBiology.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // columnBioSpecies
            // 
            this.columnBioSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnBioSpecies.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.columnBioSpecies, "columnBioSpecies");
            this.columnBioSpecies.Name = "columnBioSpecies";
            this.columnBioSpecies.ReadOnly = true;
            // 
            // columnBioLength
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "G";
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnBioLength.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.columnBioLength, "columnBioLength");
            this.columnBioLength.Name = "columnBioLength";
            this.columnBioLength.ReadOnly = true;
            // 
            // columnBioMass
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "G";
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnBioMass.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.columnBioMass, "columnBioMass");
            this.columnBioMass.Name = "columnBioMass";
            this.columnBioMass.ReadOnly = true;
            // 
            // columnBioN
            // 
            dataGridViewCellStyle13.Format = "N0";
            this.columnBioN.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.columnBioN, "columnBioN");
            this.columnBioN.Name = "columnBioN";
            this.columnBioN.ReadOnly = true;
            // 
            // columnBioB
            // 
            dataGridViewCellStyle14.Format = "N3";
            this.columnBioB.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.columnBioB, "columnBioB");
            this.columnBioB.Name = "columnBioB";
            this.columnBioB.ReadOnly = true;
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxAppCatches);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxAppAB);
            this.pageReport.Controls.Add(this.checkBoxAppCPUE);
            this.pageReport.Controls.Add(this.checkBoxCenosis);
            this.pageReport.Controls.Add(this.checkBoxCatches);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            this.pageReport.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Rollback);
            // 
            // checkBoxAppCatches
            // 
            resources.ApplyResources(this.checkBoxAppCatches, "checkBoxAppCatches");
            this.checkBoxAppCatches.Checked = true;
            this.checkBoxAppCatches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppCatches.Name = "checkBoxAppCatches";
            this.checkBoxAppCatches.UseVisualStyleBackColor = true;
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxAppAB
            // 
            resources.ApplyResources(this.checkBoxAppAB, "checkBoxAppAB");
            this.checkBoxAppAB.Checked = true;
            this.checkBoxAppAB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppAB.Name = "checkBoxAppAB";
            this.checkBoxAppAB.UseVisualStyleBackColor = true;
            this.checkBoxAppAB.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAppCPUE
            // 
            resources.ApplyResources(this.checkBoxAppCPUE, "checkBoxAppCPUE");
            this.checkBoxAppCPUE.Checked = true;
            this.checkBoxAppCPUE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppCPUE.Name = "checkBoxAppCPUE";
            this.checkBoxAppCPUE.UseVisualStyleBackColor = true;
            this.checkBoxAppCPUE.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxCenosis
            // 
            resources.ApplyResources(this.checkBoxCenosis, "checkBoxCenosis");
            this.checkBoxCenosis.Checked = true;
            this.checkBoxCenosis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCenosis.Name = "checkBoxCenosis";
            this.checkBoxCenosis.UseVisualStyleBackColor = true;
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
            // checkBoxGears
            // 
            resources.ApplyResources(this.checkBoxGears, "checkBoxGears");
            this.checkBoxGears.Checked = true;
            this.checkBoxGears.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGears.Name = "checkBoxGears";
            this.checkBoxGears.UseVisualStyleBackColor = true;
            // 
            // calculatorCenosis
            // 
            this.calculatorCenosis.WorkerReportsProgress = true;
            this.calculatorCenosis.WorkerSupportsCancellation = true;
            this.calculatorCenosis.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cenosisCalculator_DoWork);
            this.calculatorCenosis.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.cenosisCalculator_ProgressChanged);
            this.calculatorCenosis.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.cenosisCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // calculatorSelectivity
            // 
            this.calculatorSelectivity.WorkerReportsProgress = true;
            this.calculatorSelectivity.DoWork += new System.ComponentModel.DoWorkEventHandler(this.selectivityCalculator_DoWork);
            this.calculatorSelectivity.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.selectivityCalculator_RunWorkerCompleted);
            // 
            // WizardCenosis
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardCenosis";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageSelectivity.ResumeLayout(false);
            this.pageSelectivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).EndInit();
            this.pageBiology.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetBiology)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private System.ComponentModel.BackgroundWorker calculatorCenosis;
        private AeroWizard.WizardPage pageBiology;
        public Controls.SpreadSheet spreadSheetBiology;
        private System.Windows.Forms.Label labelBioInstruction;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxAppCPUE;
        private System.Windows.Forms.CheckBox checkBoxCenosis;
        private System.Windows.Forms.CheckBox checkBoxCatches;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.CheckBox checkBoxAppAB;
        private AeroWizard.WizardPage pageSelectivity;
        private System.Windows.Forms.Label labelNoticeGearsSelectivity;
        private System.Windows.Forms.Label labelSelectivityGroupInstruction;
        public System.Windows.Forms.ComboBox comboBoxDataset;
        private System.Windows.Forms.Label labelSelectivityGroupSelect;
        public Controls.SpreadSheet spreadSheetSelectivity;
        private System.Windows.Forms.CheckBox checkBoxAppCatches;
        private System.ComponentModel.BackgroundWorker calculatorSelectivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivitySpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityB;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioB;
    }
}