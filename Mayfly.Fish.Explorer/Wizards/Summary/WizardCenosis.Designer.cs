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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageGearClass = new AeroWizard.WizardPage();
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
            this.pageComposition = new AeroWizard.WizardPage();
            this.comboBoxDiversity = new System.Windows.Forms.ComboBox();
            this.textBoxDiversity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spreadSheetComposition = new Mayfly.Controls.SpreadSheet();
            this.ColumnCompositionSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionBP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionOccurrance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompositionDominance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageReport = new AeroWizard.WizardPage();
            this.comboBoxExample = new System.Windows.Forms.ComboBox();
            this.checkBoxSpreadsheets = new System.Windows.Forms.CheckBox();
            this.checkBoxByClass = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxAppExample = new System.Windows.Forms.CheckBox();
            this.checkBoxCenosis = new System.Windows.Forms.CheckBox();
            this.checkBoxCatches = new System.Windows.Forms.CheckBox();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.calculatorCenosis = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.calculatorSelectivity = new System.ComponentModel.BackgroundWorker();
            this.calculatorStructure = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageGearClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).BeginInit();
            this.pageComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageGearClass);
            this.wizardExplorer.Pages.Add(this.pageComposition);
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
            // pageGearClass
            // 
            this.pageGearClass.Controls.Add(this.labelNoticeGearsSelectivity);
            this.pageGearClass.Controls.Add(this.labelSelectivityGroupInstruction);
            this.pageGearClass.Controls.Add(this.comboBoxDataset);
            this.pageGearClass.Controls.Add(this.labelSelectivityGroupSelect);
            this.pageGearClass.Controls.Add(this.spreadSheetSelectivity);
            this.pageGearClass.Name = "pageGearClass";
            resources.ApplyResources(this.pageGearClass, "pageGearClass");
            this.pageGearClass.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSelectivity_Commit);
            this.pageGearClass.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSelectivity_Rollback);
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
            // pageComposition
            // 
            this.pageComposition.Controls.Add(this.comboBoxDiversity);
            this.pageComposition.Controls.Add(this.textBoxDiversity);
            this.pageComposition.Controls.Add(this.label1);
            this.pageComposition.Controls.Add(this.label2);
            this.pageComposition.Controls.Add(this.spreadSheetComposition);
            this.pageComposition.Name = "pageComposition";
            resources.ApplyResources(this.pageComposition, "pageComposition");
            this.pageComposition.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageComposition_Commit);
            this.pageComposition.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageComposition_Rollback);
            // 
            // comboBoxDiversity
            // 
            resources.ApplyResources(this.comboBoxDiversity, "comboBoxDiversity");
            this.comboBoxDiversity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiversity.FormattingEnabled = true;
            this.comboBoxDiversity.Name = "comboBoxDiversity";
            this.comboBoxDiversity.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDiversity_SelectedIndexChanged);
            // 
            // textBoxDiversity
            // 
            resources.ApplyResources(this.textBoxDiversity, "textBoxDiversity");
            this.textBoxDiversity.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxDiversity.Name = "textBoxDiversity";
            this.textBoxDiversity.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // spreadSheetComposition
            // 
            resources.ApplyResources(this.spreadSheetComposition, "spreadSheetComposition");
            this.spreadSheetComposition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCompositionSpecies,
            this.ColumnCompositionQ,
            this.ColumnCompositionA,
            this.ColumnCompositionAP,
            this.ColumnCompositionB,
            this.ColumnCompositionBP,
            this.ColumnCompositionOccurrance,
            this.ColumnCompositionDominance});
            this.spreadSheetComposition.DefaultDecimalPlaces = 2;
            this.spreadSheetComposition.Name = "spreadSheetComposition";
            this.spreadSheetComposition.RowHeadersVisible = false;
            this.spreadSheetComposition.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetComposition_CellValueChanged);
            // 
            // ColumnCompositionSpecies
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnCompositionSpecies.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnCompositionSpecies.Frozen = true;
            resources.ApplyResources(this.ColumnCompositionSpecies, "ColumnCompositionSpecies");
            this.ColumnCompositionSpecies.Name = "ColumnCompositionSpecies";
            this.ColumnCompositionSpecies.ReadOnly = true;
            // 
            // ColumnCompositionQ
            // 
            dataGridViewCellStyle11.Format = "N2";
            this.ColumnCompositionQ.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnCompositionQ, "ColumnCompositionQ");
            this.ColumnCompositionQ.Name = "ColumnCompositionQ";
            // 
            // ColumnCompositionA
            // 
            dataGridViewCellStyle12.Format = "N0";
            this.ColumnCompositionA.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.ColumnCompositionA, "ColumnCompositionA");
            this.ColumnCompositionA.Name = "ColumnCompositionA";
            this.ColumnCompositionA.ReadOnly = true;
            // 
            // ColumnCompositionAP
            // 
            dataGridViewCellStyle13.Format = "P1";
            this.ColumnCompositionAP.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.ColumnCompositionAP, "ColumnCompositionAP");
            this.ColumnCompositionAP.Name = "ColumnCompositionAP";
            this.ColumnCompositionAP.ReadOnly = true;
            // 
            // ColumnCompositionB
            // 
            dataGridViewCellStyle14.Format = "N3";
            this.ColumnCompositionB.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.ColumnCompositionB, "ColumnCompositionB");
            this.ColumnCompositionB.Name = "ColumnCompositionB";
            this.ColumnCompositionB.ReadOnly = true;
            // 
            // ColumnCompositionBP
            // 
            dataGridViewCellStyle15.Format = "P1";
            this.ColumnCompositionBP.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.ColumnCompositionBP, "ColumnCompositionBP");
            this.ColumnCompositionBP.Name = "ColumnCompositionBP";
            this.ColumnCompositionBP.ReadOnly = true;
            // 
            // ColumnCompositionOccurrance
            // 
            dataGridViewCellStyle16.Format = "P1";
            this.ColumnCompositionOccurrance.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.ColumnCompositionOccurrance, "ColumnCompositionOccurrance");
            this.ColumnCompositionOccurrance.Name = "ColumnCompositionOccurrance";
            // 
            // ColumnCompositionDominance
            // 
            dataGridViewCellStyle17.Format = "N3";
            this.ColumnCompositionDominance.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.ColumnCompositionDominance, "ColumnCompositionDominance");
            this.ColumnCompositionDominance.Name = "ColumnCompositionDominance";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.comboBoxExample);
            this.pageReport.Controls.Add(this.checkBoxSpreadsheets);
            this.pageReport.Controls.Add(this.checkBoxByClass);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxAppExample);
            this.pageReport.Controls.Add(this.checkBoxCenosis);
            this.pageReport.Controls.Add(this.checkBoxCatches);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // comboBoxExample
            // 
            resources.ApplyResources(this.comboBoxExample, "comboBoxExample");
            this.comboBoxExample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExample.FormattingEnabled = true;
            this.comboBoxExample.Name = "comboBoxExample";
            this.comboBoxExample.SelectedIndexChanged += new System.EventHandler(this.ComboBoxExample_SelectedIndexChanged);
            // 
            // checkBoxSpreadsheets
            // 
            resources.ApplyResources(this.checkBoxSpreadsheets, "checkBoxSpreadsheets");
            this.checkBoxSpreadsheets.Name = "checkBoxSpreadsheets";
            this.checkBoxSpreadsheets.UseVisualStyleBackColor = true;
            // 
            // checkBoxByClass
            // 
            resources.ApplyResources(this.checkBoxByClass, "checkBoxByClass");
            this.checkBoxByClass.Name = "checkBoxByClass";
            this.checkBoxByClass.UseVisualStyleBackColor = true;
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxAppExample
            // 
            resources.ApplyResources(this.checkBoxAppExample, "checkBoxAppExample");
            this.checkBoxAppExample.Name = "checkBoxAppExample";
            this.checkBoxAppExample.UseVisualStyleBackColor = true;
            this.checkBoxAppExample.CheckedChanged += new System.EventHandler(this.CheckBoxAppExample_CheckedChanged);
            this.checkBoxAppExample.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
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
            // calculatorStructure
            // 
            this.calculatorStructure.WorkerReportsProgress = true;
            this.calculatorStructure.WorkerSupportsCancellation = true;
            this.calculatorStructure.DoWork += new System.ComponentModel.DoWorkEventHandler(this.structureCalculator_DoWork);
            this.calculatorStructure.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.structureCalculator_ProgressChanged);
            this.calculatorStructure.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.structureCalculator_RunWorkerCompleted);
            // 
            // WizardCenosis
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardCenosis";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageGearClass.ResumeLayout(false);
            this.pageGearClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSelectivity)).EndInit();
            this.pageComposition.ResumeLayout(false);
            this.pageComposition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private System.ComponentModel.BackgroundWorker calculatorCenosis;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxAppExample;
        private System.Windows.Forms.CheckBox checkBoxCenosis;
        private System.Windows.Forms.CheckBox checkBoxCatches;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private AeroWizard.WizardPage pageGearClass;
        private System.Windows.Forms.Label labelNoticeGearsSelectivity;
        private System.Windows.Forms.Label labelSelectivityGroupInstruction;
        public System.Windows.Forms.ComboBox comboBoxDataset;
        private System.Windows.Forms.Label labelSelectivityGroupSelect;
        public Controls.SpreadSheet spreadSheetSelectivity;
        private System.Windows.Forms.CheckBox checkBoxByClass;
        private System.ComponentModel.BackgroundWorker calculatorSelectivity;
        private AeroWizard.WizardPage pageComposition;
        private System.Windows.Forms.TextBox textBoxDiversity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.SpreadSheet spreadSheetComposition;
        private System.ComponentModel.BackgroundWorker calculatorStructure;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivitySpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityNPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityB;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBpue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSelectivityBPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionBP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionOccurrance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompositionDominance;
        private System.Windows.Forms.ComboBox comboBoxDiversity;
        private System.Windows.Forms.ComboBox comboBoxExample;
        private System.Windows.Forms.CheckBox checkBoxSpreadsheets;
    }
}