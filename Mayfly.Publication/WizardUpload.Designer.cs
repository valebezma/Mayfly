namespace Mayfly.Publication
{
    partial class WizardUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardUpload));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardControl = new AeroWizard.WizardControl();
            this.wizardPageStart = new AeroWizard.WizardPage();
            this.buttonForce = new System.Windows.Forms.Button();
            this.labelNoChanges = new System.Windows.Forms.Label();
            this.labelStart = new System.Windows.Forms.Label();
            this.wizardPageBinaries = new AeroWizard.WizardPage();
            this.spreadSheetBinaries = new Mayfly.Controls.SpreadSheet();
            this.ColumnBinary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVersionCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVersionPublished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelUpdate = new System.Windows.Forms.Label();
            this.wizardPageNotes = new AeroWizard.WizardPage();
            this.spreadSheetReleaseNotes = new Mayfly.Controls.SpreadSheet();
            this.ColumnCulture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCur = new System.Windows.Forms.TextBox();
            this.textBoxPub = new System.Windows.Forms.TextBox();
            this.textBoxBin = new System.Windows.Forms.TextBox();
            this.wizardPageUpload = new AeroWizard.WizardPage();
            this.textBoxFiles = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.wizardPageDone = new AeroWizard.WizardPage();
            this.labelUpStatus = new System.Windows.Forms.Label();
            this.labelUploading = new System.Windows.Forms.Label();
            this.progressUpload = new System.Windows.Forms.ProgressBar();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.buttonFtp = new System.Windows.Forms.Button();
            this.buttonWebsite = new System.Windows.Forms.Button();
            this.labelDone = new System.Windows.Forms.Label();
            this.uploadInspector = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.uploader = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardPageStart.SuspendLayout();
            this.wizardPageBinaries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetBinaries)).BeginInit();
            this.wizardPageNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetReleaseNotes)).BeginInit();
            this.wizardPageUpload.SuspendLayout();
            this.wizardPageDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            resources.ApplyResources(this.wizardControl, "wizardControl");
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.Add(this.wizardPageStart);
            this.wizardControl.Pages.Add(this.wizardPageBinaries);
            this.wizardControl.Pages.Add(this.wizardPageNotes);
            this.wizardControl.Pages.Add(this.wizardPageUpload);
            this.wizardControl.Pages.Add(this.wizardPageDone);
            this.wizardControl.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl_Cancelling);
            this.wizardControl.Finished += new System.EventHandler(this.wizardControl_Finished);
            // 
            // wizardPageStart
            // 
            this.wizardPageStart.Controls.Add(this.buttonForce);
            this.wizardPageStart.Controls.Add(this.labelNoChanges);
            this.wizardPageStart.Controls.Add(this.labelStart);
            this.wizardPageStart.Name = "wizardPageStart";
            resources.ApplyResources(this.wizardPageStart, "wizardPageStart");
            // 
            // buttonForce
            // 
            resources.ApplyResources(this.buttonForce, "buttonForce");
            this.buttonForce.Name = "buttonForce";
            this.buttonForce.UseVisualStyleBackColor = true;
            this.buttonForce.Click += new System.EventHandler(this.buttonForce_Click);
            // 
            // labelNoChanges
            // 
            resources.ApplyResources(this.labelNoChanges, "labelNoChanges");
            this.labelNoChanges.Name = "labelNoChanges";
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // wizardPageBinaries
            // 
            this.wizardPageBinaries.Controls.Add(this.spreadSheetBinaries);
            this.wizardPageBinaries.Controls.Add(this.labelUpdate);
            this.wizardPageBinaries.Name = "wizardPageBinaries";
            resources.ApplyResources(this.wizardPageBinaries, "wizardPageBinaries");
            this.wizardPageBinaries.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageBinaries_Commit);
            // 
            // spreadSheetBinaries
            // 
            resources.ApplyResources(this.spreadSheetBinaries, "spreadSheetBinaries");
            this.spreadSheetBinaries.CellPadding = new System.Windows.Forms.Padding(0);
            this.spreadSheetBinaries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnBinary,
            this.ColumnVersionCurrent,
            this.ColumnVersionPublished});
            this.spreadSheetBinaries.DefaultDecimalPlaces = 0;
            this.spreadSheetBinaries.Name = "spreadSheetBinaries";
            this.spreadSheetBinaries.ReadOnly = true;
            this.spreadSheetBinaries.RowHeadersVisible = false;
            this.spreadSheetBinaries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ColumnBinary
            // 
            this.ColumnBinary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnBinary.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnBinary, "ColumnBinary");
            this.ColumnBinary.Name = "ColumnBinary";
            this.ColumnBinary.ReadOnly = true;
            // 
            // ColumnVersionCurrent
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnVersionCurrent.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnVersionCurrent, "ColumnVersionCurrent");
            this.ColumnVersionCurrent.Name = "ColumnVersionCurrent";
            this.ColumnVersionCurrent.ReadOnly = true;
            // 
            // ColumnVersionPublished
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnVersionPublished.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnVersionPublished, "ColumnVersionPublished");
            this.ColumnVersionPublished.Name = "ColumnVersionPublished";
            this.ColumnVersionPublished.ReadOnly = true;
            // 
            // labelUpdate
            // 
            resources.ApplyResources(this.labelUpdate, "labelUpdate");
            this.labelUpdate.Name = "labelUpdate";
            // 
            // wizardPageNotes
            // 
            this.wizardPageNotes.Controls.Add(this.spreadSheetReleaseNotes);
            this.wizardPageNotes.Controls.Add(this.label5);
            this.wizardPageNotes.Controls.Add(this.label4);
            this.wizardPageNotes.Controls.Add(this.label3);
            this.wizardPageNotes.Controls.Add(this.label1);
            this.wizardPageNotes.Controls.Add(this.label2);
            this.wizardPageNotes.Controls.Add(this.textBoxCur);
            this.wizardPageNotes.Controls.Add(this.textBoxPub);
            this.wizardPageNotes.Controls.Add(this.textBoxBin);
            this.wizardPageNotes.Name = "wizardPageNotes";
            resources.ApplyResources(this.wizardPageNotes, "wizardPageNotes");
            this.wizardPageNotes.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNotes_Commit);
            this.wizardPageNotes.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNotes_Rollback);
            // 
            // spreadSheetReleaseNotes
            // 
            this.spreadSheetReleaseNotes.CellPadding = new System.Windows.Forms.Padding(0);
            this.spreadSheetReleaseNotes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCulture,
            this.ColumnNote});
            resources.ApplyResources(this.spreadSheetReleaseNotes, "spreadSheetReleaseNotes");
            this.spreadSheetReleaseNotes.Name = "spreadSheetReleaseNotes";
            this.spreadSheetReleaseNotes.RowTemplate.Height = 60;
            this.spreadSheetReleaseNotes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetReleaseNotes_CellValueChanged);
            // 
            // ColumnCulture
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCulture.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnCulture, "ColumnCulture");
            this.ColumnCulture.Name = "ColumnCulture";
            this.ColumnCulture.ReadOnly = true;
            // 
            // ColumnNote
            // 
            this.ColumnNote.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnNote.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnNote, "ColumnNote");
            this.ColumnNote.Name = "ColumnNote";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // textBoxCur
            // 
            resources.ApplyResources(this.textBoxCur, "textBoxCur");
            this.textBoxCur.Name = "textBoxCur";
            this.textBoxCur.ReadOnly = true;
            // 
            // textBoxPub
            // 
            resources.ApplyResources(this.textBoxPub, "textBoxPub");
            this.textBoxPub.Name = "textBoxPub";
            this.textBoxPub.ReadOnly = true;
            // 
            // textBoxBin
            // 
            resources.ApplyResources(this.textBoxBin, "textBoxBin");
            this.textBoxBin.Name = "textBoxBin";
            this.textBoxBin.ReadOnly = true;
            // 
            // wizardPageUpload
            // 
            this.wizardPageUpload.Controls.Add(this.textBoxFiles);
            this.wizardPageUpload.Controls.Add(this.label6);
            this.wizardPageUpload.Controls.Add(this.buttonDetails);
            this.wizardPageUpload.Controls.Add(this.buttonSave);
            this.wizardPageUpload.Name = "wizardPageUpload";
            resources.ApplyResources(this.wizardPageUpload, "wizardPageUpload");
            this.wizardPageUpload.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageUpload_Commit);
            this.wizardPageUpload.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageUpload_Initialize);
            // 
            // textBoxFiles
            // 
            resources.ApplyResources(this.textBoxFiles, "textBoxFiles");
            this.textBoxFiles.Name = "textBoxFiles";
            this.textBoxFiles.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // buttonDetails
            // 
            resources.ApplyResources(this.buttonDetails, "buttonDetails");
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // wizardPageDone
            // 
            this.wizardPageDone.AllowBack = false;
            this.wizardPageDone.Controls.Add(this.labelUpStatus);
            this.wizardPageDone.Controls.Add(this.labelUploading);
            this.wizardPageDone.Controls.Add(this.progressUpload);
            this.wizardPageDone.Controls.Add(this.buttonRetry);
            this.wizardPageDone.Controls.Add(this.buttonFtp);
            this.wizardPageDone.Controls.Add(this.buttonWebsite);
            this.wizardPageDone.Controls.Add(this.labelDone);
            this.wizardPageDone.Name = "wizardPageDone";
            this.wizardPageDone.ShowCancel = false;
            resources.ApplyResources(this.wizardPageDone, "wizardPageDone");
            // 
            // labelUpStatus
            // 
            resources.ApplyResources(this.labelUpStatus, "labelUpStatus");
            this.labelUpStatus.Name = "labelUpStatus";
            // 
            // labelUploading
            // 
            resources.ApplyResources(this.labelUploading, "labelUploading");
            this.labelUploading.Name = "labelUploading";
            // 
            // progressUpload
            // 
            resources.ApplyResources(this.progressUpload, "progressUpload");
            this.progressUpload.Name = "progressUpload";
            this.progressUpload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // buttonRetry
            // 
            resources.ApplyResources(this.buttonRetry, "buttonRetry");
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.UseVisualStyleBackColor = true;
            this.buttonRetry.Click += new System.EventHandler(this.buttonRetry_Click);
            // 
            // buttonFtp
            // 
            resources.ApplyResources(this.buttonFtp, "buttonFtp");
            this.buttonFtp.Name = "buttonFtp";
            this.buttonFtp.UseVisualStyleBackColor = true;
            this.buttonFtp.Click += new System.EventHandler(this.buttonFtp_Click);
            // 
            // buttonWebsite
            // 
            resources.ApplyResources(this.buttonWebsite, "buttonWebsite");
            this.buttonWebsite.Name = "buttonWebsite";
            this.buttonWebsite.UseVisualStyleBackColor = true;
            this.buttonWebsite.Click += new System.EventHandler(this.buttonWebsite_Click);
            // 
            // labelDone
            // 
            resources.ApplyResources(this.labelDone, "labelDone");
            this.labelDone.Name = "labelDone";
            // 
            // uploadInspector
            // 
            this.uploadInspector.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploadInspector_DoWork);
            this.uploadInspector.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uploadInspector_RunWorkerCompleted);
            // 
            // uploader
            // 
            this.uploader.WorkerReportsProgress = true;
            this.uploader.WorkerSupportsCancellation = true;
            this.uploader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploader_DoWork);
            this.uploader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.uploader_ProgressChanged);
            this.uploader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uploader_RunWorkerCompleted);
            // 
            // WizardUpload
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardControl);
            this.Name = "WizardUpload";
            this.Load += new System.EventHandler(this.WizardUpload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardPageStart.ResumeLayout(false);
            this.wizardPageBinaries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetBinaries)).EndInit();
            this.wizardPageNotes.ResumeLayout(false);
            this.wizardPageNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetReleaseNotes)).EndInit();
            this.wizardPageUpload.ResumeLayout(false);
            this.wizardPageUpload.PerformLayout();
            this.wizardPageDone.ResumeLayout(false);
            this.wizardPageDone.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl;
        private AeroWizard.WizardPage wizardPageBinaries;
        private System.Windows.Forms.Label labelUpdate;
        private AeroWizard.WizardPage wizardPageUpload;
        private System.ComponentModel.BackgroundWorker uploadInspector;
        private AeroWizard.WizardPage wizardPageNotes;
        private System.Windows.Forms.TextBox textBoxCur;
        private System.Windows.Forms.TextBox textBoxPub;
        private System.Windows.Forms.TextBox textBoxBin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Controls.SpreadSheet spreadSheetReleaseNotes;
        private Controls.SpreadSheet spreadSheetBinaries;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBinary;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVersionCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVersionPublished;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private AeroWizard.WizardPage wizardPageDone;
        private System.Windows.Forms.Label labelDone;
        private System.Windows.Forms.Button buttonWebsite;
        private System.Windows.Forms.TextBox textBoxFiles;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label labelUploading;
        private System.Windows.Forms.ProgressBar progressUpload;
        private System.Windows.Forms.Button buttonDetails;
        private System.ComponentModel.BackgroundWorker uploader;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Label labelUpStatus;
        private AeroWizard.WizardPage wizardPageStart;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelNoChanges;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCulture;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNote;
        private System.Windows.Forms.Button buttonFtp;
        private System.Windows.Forms.Button buttonForce;
    }
}