namespace Mayfly.Software
{
    partial class WizardUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardUpdate));
            this.wizardControlUpdate = new AeroWizard.WizardControl();
            this.wizardPageStart = new AeroWizard.WizardPage();
            this.labelUpdate = new System.Windows.Forms.Label();
            this.wizardPageNotes = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxReleaseNotes = new System.Windows.Forms.TextBox();
            this.wizardPageGet = new AeroWizard.WizardPage();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelInformation = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.wizardPageError = new AeroWizard.WizardPage();
            this.labelError = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.wizardPageDone = new AeroWizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundDownloader = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlUpdate)).BeginInit();
            this.wizardPageStart.SuspendLayout();
            this.wizardPageNotes.SuspendLayout();
            this.wizardPageGet.SuspendLayout();
            this.wizardPageError.SuspendLayout();
            this.wizardPageDone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControlUpdate
            // 
            this.wizardControlUpdate.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardControlUpdate, "wizardControlUpdate");
            this.wizardControlUpdate.Name = "wizardControlUpdate";
            this.wizardControlUpdate.Pages.Add(this.wizardPageStart);
            this.wizardControlUpdate.Pages.Add(this.wizardPageNotes);
            this.wizardControlUpdate.Pages.Add(this.wizardPageGet);
            this.wizardControlUpdate.Pages.Add(this.wizardPageError);
            this.wizardControlUpdate.Pages.Add(this.wizardPageDone);
            this.wizardControlUpdate.Finished += new System.EventHandler(this.wizardControlUpdate_Finished);
            // 
            // wizardPageStart
            // 
            this.wizardPageStart.AllowNext = false;
            this.wizardPageStart.Controls.Add(this.labelUpdate);
            this.wizardPageStart.Name = "wizardPageStart";
            resources.ApplyResources(this.wizardPageStart, "wizardPageStart");
            // 
            // labelUpdate
            // 
            resources.ApplyResources(this.labelUpdate, "labelUpdate");
            this.labelUpdate.Name = "labelUpdate";
            // 
            // wizardPageNotes
            // 
            this.wizardPageNotes.Controls.Add(this.label1);
            this.wizardPageNotes.Controls.Add(this.textBoxReleaseNotes);
            this.wizardPageNotes.Name = "wizardPageNotes";
            resources.ApplyResources(this.wizardPageNotes, "wizardPageNotes");
            this.wizardPageNotes.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNotes_Commit);
            this.wizardPageNotes.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageNotes_Initialize);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxReleaseNotes
            // 
            resources.ApplyResources(this.textBoxReleaseNotes, "textBoxReleaseNotes");
            this.textBoxReleaseNotes.Name = "textBoxReleaseNotes";
            this.textBoxReleaseNotes.ReadOnly = true;
            // 
            // wizardPageGet
            // 
            this.wizardPageGet.AllowBack = false;
            this.wizardPageGet.AllowNext = false;
            this.wizardPageGet.Controls.Add(this.labelStatus);
            this.wizardPageGet.Controls.Add(this.labelInformation);
            this.wizardPageGet.Controls.Add(this.progressBar);
            this.wizardPageGet.Name = "wizardPageGet";
            resources.ApplyResources(this.wizardPageGet, "wizardPageGet");
            // 
            // labelStatus
            // 
            resources.ApplyResources(this.labelStatus, "labelStatus");
            this.labelStatus.AutoEllipsis = true;
            this.labelStatus.Name = "labelStatus";
            // 
            // labelInformation
            // 
            resources.ApplyResources(this.labelInformation, "labelInformation");
            this.labelInformation.Name = "labelInformation";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // wizardPageError
            // 
            this.wizardPageError.AllowBack = false;
            this.wizardPageError.Controls.Add(this.labelError);
            this.wizardPageError.Controls.Add(this.label3);
            this.wizardPageError.Name = "wizardPageError";
            resources.ApplyResources(this.wizardPageError, "wizardPageError");
            // 
            // labelError
            // 
            resources.ApplyResources(this.labelError, "labelError");
            this.labelError.Name = "labelError";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // wizardPageDone
            // 
            this.wizardPageDone.AllowBack = false;
            this.wizardPageDone.Controls.Add(this.label2);
            this.wizardPageDone.Name = "wizardPageDone";
            resources.ApplyResources(this.wizardPageDone, "wizardPageDone");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // backgroundDownloader
            // 
            this.backgroundDownloader.WorkerReportsProgress = true;
            this.backgroundDownloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundDownloader_DoWork);
            this.backgroundDownloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundDownloader_ProgressChanged);
            this.backgroundDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundDownloader_RunWorkerCompleted);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // WizardUpdate
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.wizardControlUpdate);
            this.Name = "WizardUpdate";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlUpdate)).EndInit();
            this.wizardPageStart.ResumeLayout(false);
            this.wizardPageNotes.ResumeLayout(false);
            this.wizardPageNotes.PerformLayout();
            this.wizardPageGet.ResumeLayout(false);
            this.wizardPageGet.PerformLayout();
            this.wizardPageError.ResumeLayout(false);
            this.wizardPageDone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControlUpdate;
        private AeroWizard.WizardPage wizardPageStart;
        private System.Windows.Forms.Label labelUpdate;
        private AeroWizard.WizardPage wizardPageNotes;
        private System.Windows.Forms.TextBox textBoxReleaseNotes;
        private System.Windows.Forms.Label label1;
        private AeroWizard.WizardPage wizardPageGet;
        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.ProgressBar progressBar;
        private AeroWizard.WizardPage wizardPageDone;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundDownloader;
        private System.Windows.Forms.Label labelStatus;
        private AeroWizard.WizardPage wizardPageError;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}