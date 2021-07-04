namespace Mayfly.Software.Management
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
            this.wizardControl = new AeroWizard.WizardControl();
            this.wizardPageStart = new AeroWizard.WizardPage();
            this.buttonScheme = new System.Windows.Forms.Button();
            this.buttonForce = new System.Windows.Forms.Button();
            this.labelNoChanges = new System.Windows.Forms.Label();
            this.labelStart = new System.Windows.Forms.Label();
            this.wizardPageMissing = new AeroWizard.WizardPage();
            this.textBoxMissingFiles = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.wizardPageNews = new AeroWizard.WizardPage();
            this.spreadSheetNews = new Mayfly.Controls.SpreadSheet();
            this.ColumnBinary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVersionCurrent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVersionPublished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChanges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelUpdate = new System.Windows.Forms.Label();
            this.wizardPageNotes = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.labelNote = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCur = new System.Windows.Forms.TextBox();
            this.textBoxPub = new System.Windows.Forms.TextBox();
            this.textBoxBin = new System.Windows.Forms.TextBox();
            this.textBoxNoteLast = new System.Windows.Forms.TextBox();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.wizardPageUpload = new AeroWizard.WizardPage();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.textBoxUpFiles = new System.Windows.Forms.TextBox();
            this.wizardPageRoll = new AeroWizard.WizardPage();
            this.labelUpStatus = new System.Windows.Forms.Label();
            this.labelUploading = new System.Windows.Forms.Label();
            this.progressUpload = new System.Windows.Forms.ProgressBar();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.wizardPageDone = new AeroWizard.WizardPage();
            this.buttonFtp = new System.Windows.Forms.Button();
            this.buttonWebsite = new System.Windows.Forms.Button();
            this.labelDone = new System.Windows.Forms.Label();
            this.uploader = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardPageStart.SuspendLayout();
            this.wizardPageMissing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.wizardPageNews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetNews)).BeginInit();
            this.wizardPageNotes.SuspendLayout();
            this.wizardPageUpload.SuspendLayout();
            this.wizardPageRoll.SuspendLayout();
            this.wizardPageDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            this.wizardControl.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardControl, "wizardControl");
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.Add(this.wizardPageStart);
            this.wizardControl.Pages.Add(this.wizardPageMissing);
            this.wizardControl.Pages.Add(this.wizardPageNews);
            this.wizardControl.Pages.Add(this.wizardPageNotes);
            this.wizardControl.Pages.Add(this.wizardPageUpload);
            this.wizardControl.Pages.Add(this.wizardPageRoll);
            this.wizardControl.Pages.Add(this.wizardPageDone);
            this.wizardControl.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl_Cancelling);
            this.wizardControl.Finished += new System.EventHandler(this.wizardControl_Finished);
            // 
            // wizardPageStart
            // 
            this.wizardPageStart.Controls.Add(this.buttonScheme);
            this.wizardPageStart.Controls.Add(this.buttonForce);
            this.wizardPageStart.Controls.Add(this.labelNoChanges);
            this.wizardPageStart.Controls.Add(this.labelStart);
            this.wizardPageStart.Name = "wizardPageStart";
            resources.ApplyResources(this.wizardPageStart, "wizardPageStart");
            // 
            // buttonScheme
            // 
            resources.ApplyResources(this.buttonScheme, "buttonScheme");
            this.buttonScheme.Name = "buttonScheme";
            this.buttonScheme.UseVisualStyleBackColor = true;
            this.buttonScheme.Click += new System.EventHandler(this.buttonScheme_Click);
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
            // wizardPageMissing
            // 
            this.wizardPageMissing.AllowNext = false;
            this.wizardPageMissing.Controls.Add(this.textBoxMissingFiles);
            this.wizardPageMissing.Controls.Add(this.label7);
            this.wizardPageMissing.Controls.Add(this.pictureBox1);
            this.wizardPageMissing.Name = "wizardPageMissing";
            resources.ApplyResources(this.wizardPageMissing, "wizardPageMissing");
            // 
            // textBoxMissingFiles
            // 
            resources.ApplyResources(this.textBoxMissingFiles, "textBoxMissingFiles");
            this.textBoxMissingFiles.Name = "textBoxMissingFiles";
            this.textBoxMissingFiles.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Mayfly.Software.Management.Properties.Resources.Warning;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // wizardPageNews
            // 
            this.wizardPageNews.Controls.Add(this.spreadSheetNews);
            this.wizardPageNews.Controls.Add(this.labelUpdate);
            this.wizardPageNews.Name = "wizardPageNews";
            resources.ApplyResources(this.wizardPageNews, "wizardPageNews");
            this.wizardPageNews.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNews_Commit);
            // 
            // spreadSheetNews
            // 
            resources.ApplyResources(this.spreadSheetNews, "spreadSheetNews");
            this.spreadSheetNews.CellPadding = new System.Windows.Forms.Padding(0);
            this.spreadSheetNews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnBinary,
            this.ColumnVersionCurrent,
            this.ColumnVersionPublished,
            this.ColumnChanges});
            this.spreadSheetNews.DefaultDecimalPlaces = 0;
            this.spreadSheetNews.Name = "spreadSheetNews";
            this.spreadSheetNews.ReadOnly = true;
            this.spreadSheetNews.RowHeadersVisible = false;
            this.spreadSheetNews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
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
            // ColumnChanges
            // 
            resources.ApplyResources(this.ColumnChanges, "ColumnChanges");
            this.ColumnChanges.Name = "ColumnChanges";
            this.ColumnChanges.ReadOnly = true;
            // 
            // labelUpdate
            // 
            resources.ApplyResources(this.labelUpdate, "labelUpdate");
            this.labelUpdate.Name = "labelUpdate";
            // 
            // wizardPageNotes
            // 
            this.wizardPageNotes.Controls.Add(this.label1);
            this.wizardPageNotes.Controls.Add(this.labelNote);
            this.wizardPageNotes.Controls.Add(this.label5);
            this.wizardPageNotes.Controls.Add(this.label4);
            this.wizardPageNotes.Controls.Add(this.label3);
            this.wizardPageNotes.Controls.Add(this.label2);
            this.wizardPageNotes.Controls.Add(this.textBoxCur);
            this.wizardPageNotes.Controls.Add(this.textBoxPub);
            this.wizardPageNotes.Controls.Add(this.textBoxBin);
            this.wizardPageNotes.Controls.Add(this.textBoxNoteLast);
            this.wizardPageNotes.Controls.Add(this.textBoxNote);
            this.wizardPageNotes.Name = "wizardPageNotes";
            resources.ApplyResources(this.wizardPageNotes, "wizardPageNotes");
            this.wizardPageNotes.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNotes_Commit);
            this.wizardPageNotes.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNotes_Rollback);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelNote
            // 
            resources.ApplyResources(this.labelNote, "labelNote");
            this.labelNote.Name = "labelNote";
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
            // textBoxNoteLast
            // 
            resources.ApplyResources(this.textBoxNoteLast, "textBoxNoteLast");
            this.textBoxNoteLast.Name = "textBoxNoteLast";
            this.textBoxNoteLast.ReadOnly = true;
            // 
            // textBoxNote
            // 
            this.textBoxNote.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxNote, "textBoxNote");
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.ReadOnly = true;
            // 
            // wizardPageUpload
            // 
            this.wizardPageUpload.Controls.Add(this.checkBoxBackup);
            this.wizardPageUpload.Controls.Add(this.label6);
            this.wizardPageUpload.Controls.Add(this.buttonDetails);
            this.wizardPageUpload.Controls.Add(this.textBoxUpFiles);
            this.wizardPageUpload.Name = "wizardPageUpload";
            resources.ApplyResources(this.wizardPageUpload, "wizardPageUpload");
            this.wizardPageUpload.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageUpload_Commit);
            this.wizardPageUpload.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageUpload_Initialize);
            // 
            // checkBoxBackup
            // 
            resources.ApplyResources(this.checkBoxBackup, "checkBoxBackup");
            this.checkBoxBackup.Checked = true;
            this.checkBoxBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
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
            // textBoxUpFiles
            // 
            resources.ApplyResources(this.textBoxUpFiles, "textBoxUpFiles");
            this.textBoxUpFiles.Name = "textBoxUpFiles";
            this.textBoxUpFiles.ReadOnly = true;
            // 
            // wizardPageRoll
            // 
            this.wizardPageRoll.AllowBack = false;
            this.wizardPageRoll.Controls.Add(this.labelUpStatus);
            this.wizardPageRoll.Controls.Add(this.labelUploading);
            this.wizardPageRoll.Controls.Add(this.progressUpload);
            this.wizardPageRoll.Controls.Add(this.buttonRetry);
            this.wizardPageRoll.Name = "wizardPageRoll";
            this.wizardPageRoll.ShowCancel = false;
            resources.ApplyResources(this.wizardPageRoll, "wizardPageRoll");
            // 
            // labelUpStatus
            // 
            resources.ApplyResources(this.labelUpStatus, "labelUpStatus");
            this.labelUpStatus.AutoEllipsis = true;
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
            // wizardPageDone
            // 
            this.wizardPageDone.Controls.Add(this.buttonFtp);
            this.wizardPageDone.Controls.Add(this.buttonWebsite);
            this.wizardPageDone.Controls.Add(this.labelDone);
            this.wizardPageDone.Name = "wizardPageDone";
            resources.ApplyResources(this.wizardPageDone, "wizardPageDone");
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
            this.wizardPageMissing.ResumeLayout(false);
            this.wizardPageMissing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.wizardPageNews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetNews)).EndInit();
            this.wizardPageNotes.ResumeLayout(false);
            this.wizardPageNotes.PerformLayout();
            this.wizardPageUpload.ResumeLayout(false);
            this.wizardPageUpload.PerformLayout();
            this.wizardPageRoll.ResumeLayout(false);
            this.wizardPageRoll.PerformLayout();
            this.wizardPageDone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl;
        private AeroWizard.WizardPage wizardPageNews;
        private System.Windows.Forms.Label labelUpdate;
        private AeroWizard.WizardPage wizardPageUpload;
        private AeroWizard.WizardPage wizardPageNotes;
        private System.Windows.Forms.TextBox textBoxCur;
        private System.Windows.Forms.TextBox textBoxPub;
        private System.Windows.Forms.TextBox textBoxBin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Controls.SpreadSheet spreadSheetNews;
        private System.Windows.Forms.Label label6;
        private AeroWizard.WizardPage wizardPageRoll;
        private System.Windows.Forms.TextBox textBoxUpFiles;
        private System.Windows.Forms.Label labelUploading;
        private System.Windows.Forms.ProgressBar progressUpload;
        private System.Windows.Forms.Button buttonDetails;
        private System.ComponentModel.BackgroundWorker uploader;
        private System.Windows.Forms.Button buttonRetry;
        private AeroWizard.WizardPage wizardPageStart;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelNoChanges;
        private AeroWizard.WizardPage wizardPageDone;
        private AeroWizard.WizardPage wizardPageMissing;
        private System.Windows.Forms.TextBox textBoxMissingFiles;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonFtp;
        private System.Windows.Forms.Button buttonWebsite;
        private System.Windows.Forms.Label labelDone;
        private System.Windows.Forms.Button buttonForce;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNoteLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBinary;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVersionCurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVersionPublished;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnChanges;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.Button buttonScheme;
        private System.Windows.Forms.Label labelUpStatus;
    }
}