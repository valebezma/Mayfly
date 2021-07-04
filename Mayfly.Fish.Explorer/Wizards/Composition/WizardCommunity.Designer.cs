namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardCommunity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardCommunity));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageBiology = new AeroWizard.WizardPage();
            this.labelBioInstruction = new System.Windows.Forms.Label();
            this.spreadSheetBiology = new Mayfly.Controls.SpreadSheet();
            this.columnBioSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnBioMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageReport = new AeroWizard.WizardPage();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxAbundance = new System.Windows.Forms.CheckBox();
            this.checkBoxAppendices = new System.Windows.Forms.CheckBox();
            this.checkBoxCommunity = new System.Windows.Forms.CheckBox();
            this.checkBoxCatches = new System.Windows.Forms.CheckBox();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.calculatorCommunity = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
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
            // pageBiology
            // 
            this.pageBiology.Controls.Add(this.labelBioInstruction);
            this.pageBiology.Controls.Add(this.spreadSheetBiology);
            this.pageBiology.Name = "pageBiology";
            resources.ApplyResources(this.pageBiology, "pageBiology");
            this.pageBiology.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageBiology_Commit);
            this.pageBiology.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageBiology_Rollback);
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
            this.columnBioN,
            this.columnBioB,
            this.columnBioLength,
            this.columnBioMass});
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnBioSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnBioSpecies, "columnBioSpecies");
            this.columnBioSpecies.Name = "columnBioSpecies";
            this.columnBioSpecies.ReadOnly = true;
            // 
            // columnBioN
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.columnBioN.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnBioN, "columnBioN");
            this.columnBioN.Name = "columnBioN";
            this.columnBioN.ReadOnly = true;
            // 
            // columnBioB
            // 
            dataGridViewCellStyle3.Format = "N3";
            this.columnBioB.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnBioB, "columnBioB");
            this.columnBioB.Name = "columnBioB";
            this.columnBioB.ReadOnly = true;
            // 
            // columnBioLength
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "G";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnBioLength.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnBioLength, "columnBioLength");
            this.columnBioLength.Name = "columnBioLength";
            this.columnBioLength.ReadOnly = true;
            // 
            // columnBioMass
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "G";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnBioMass.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.columnBioMass, "columnBioMass");
            this.columnBioMass.Name = "columnBioMass";
            this.columnBioMass.ReadOnly = true;
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxAbundance);
            this.pageReport.Controls.Add(this.checkBoxAppendices);
            this.pageReport.Controls.Add(this.checkBoxCommunity);
            this.pageReport.Controls.Add(this.checkBoxCatches);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            this.pageReport.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Rollback);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxAbundance
            // 
            resources.ApplyResources(this.checkBoxAbundance, "checkBoxAbundance");
            this.checkBoxAbundance.Checked = true;
            this.checkBoxAbundance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAbundance.Name = "checkBoxAbundance";
            this.checkBoxAbundance.UseVisualStyleBackColor = true;
            this.checkBoxAbundance.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAppendices
            // 
            resources.ApplyResources(this.checkBoxAppendices, "checkBoxAppendices");
            this.checkBoxAppendices.Checked = true;
            this.checkBoxAppendices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAppendices.Name = "checkBoxAppendices";
            this.checkBoxAppendices.UseVisualStyleBackColor = true;
            this.checkBoxAppendices.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxCommunity
            // 
            resources.ApplyResources(this.checkBoxCommunity, "checkBoxCommunity");
            this.checkBoxCommunity.Checked = true;
            this.checkBoxCommunity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCommunity.Name = "checkBoxCommunity";
            this.checkBoxCommunity.UseVisualStyleBackColor = true;
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
            // calculatorCommunity
            // 
            this.calculatorCommunity.WorkerReportsProgress = true;
            this.calculatorCommunity.WorkerSupportsCancellation = true;
            this.calculatorCommunity.DoWork += new System.ComponentModel.DoWorkEventHandler(this.communityCalculator_DoWork);
            this.calculatorCommunity.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.communityCalculator_ProgressChanged);
            this.calculatorCommunity.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.communityCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // WizardCommunity
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardCommunity";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageBiology.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetBiology)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private System.ComponentModel.BackgroundWorker calculatorCommunity;
        private AeroWizard.WizardPage pageBiology;
        public Controls.SpreadSheet spreadSheetBiology;
        private System.Windows.Forms.Label labelBioInstruction;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxAppendices;
        private System.Windows.Forms.CheckBox checkBoxCommunity;
        private System.Windows.Forms.CheckBox checkBoxCatches;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.CheckBox checkBoxAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioN;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioB;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBioMass;
    }
}