namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardGearClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardGearClass));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxAppendices = new System.Windows.Forms.CheckBox();
            this.checkBoxCatchesPerClass = new System.Windows.Forms.CheckBox();
            this.checkBoxCatches = new System.Windows.Forms.CheckBox();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.calculatorSelectivity = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.pageSelectivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
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
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageSelectivity);
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
            // pageReport
            // 
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxAppendices);
            this.pageReport.Controls.Add(this.checkBoxCatchesPerClass);
            this.pageReport.Controls.Add(this.checkBoxCatches);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            this.pageReport.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.PageReport_Initialize);
            this.pageReport.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Rollback);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxAppendices
            // 
            resources.ApplyResources(this.checkBoxAppendices, "checkBoxAppendices");
            this.checkBoxAppendices.Checked = true;
            this.checkBoxAppendices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppendices.Name = "checkBoxAppendices";
            this.checkBoxAppendices.UseVisualStyleBackColor = true;
            // 
            // checkBoxCatchesPerClass
            // 
            resources.ApplyResources(this.checkBoxCatchesPerClass, "checkBoxCatchesPerClass");
            this.checkBoxCatchesPerClass.Checked = true;
            this.checkBoxCatchesPerClass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCatchesPerClass.Name = "checkBoxCatchesPerClass";
            this.checkBoxCatchesPerClass.UseVisualStyleBackColor = true;
            // 
            // checkBoxCatches
            // 
            resources.ApplyResources(this.checkBoxCatches, "checkBoxCatches");
            this.checkBoxCatches.Checked = true;
            this.checkBoxCatches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCatches.Name = "checkBoxCatches";
            this.checkBoxCatches.UseVisualStyleBackColor = true;
            // 
            // checkBoxGears
            // 
            resources.ApplyResources(this.checkBoxGears, "checkBoxGears");
            this.checkBoxGears.Checked = true;
            this.checkBoxGears.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGears.Name = "checkBoxGears";
            this.checkBoxGears.UseVisualStyleBackColor = true;
            // 
            // calculatorSelectivity
            // 
            this.calculatorSelectivity.WorkerReportsProgress = true;
            this.calculatorSelectivity.DoWork += new System.ComponentModel.DoWorkEventHandler(this.selectivityCalculator_DoWork);
            this.calculatorSelectivity.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.selectivityCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // WizardGearClass
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardGearClass";
            this.pageSelectivity.ResumeLayout(false);
            this.pageSelectivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPage pageSelectivity;
        private AeroWizard.WizardControl wizardExplorer;
        private System.Windows.Forms.Label labelSelectivityGroupSelect;
        public System.Windows.Forms.ComboBox comboBoxDataset;
        public Mayfly.Controls.SpreadSheet spreadSheetSelectivity;
        private System.Windows.Forms.Label labelSelectivityGroupInstruction;
        private System.ComponentModel.BackgroundWorker calculatorSelectivity;
        private System.Windows.Forms.Label labelNoticeGearsSelectivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivitySpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityB;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBPer;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private AeroWizard.WizardPage pageReport;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxAppendices;
        private System.Windows.Forms.CheckBox checkBoxCatches;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private System.Windows.Forms.CheckBox checkBoxCatchesPerClass;
    }
}