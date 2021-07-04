namespace Mayfly.Software.Management
{
    partial class WizardRoll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardRoll));
            this.wizardControl = new AeroWizard.WizardControl();
            this.wizardPageUpload = new AeroWizard.WizardPage();
            this.textBoxFiles = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.wizardPageRoll = new AeroWizard.WizardPage();
            this.labelUpStatus = new System.Windows.Forms.Label();
            this.labelUploading = new System.Windows.Forms.Label();
            this.progressUpload = new System.Windows.Forms.ProgressBar();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.wizardPageDone = new AeroWizard.WizardPage();
            this.labelDone = new System.Windows.Forms.Label();
            this.uploader = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardPageUpload.SuspendLayout();
            this.wizardPageRoll.SuspendLayout();
            this.wizardPageDone.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            resources.ApplyResources(this.wizardControl, "wizardControl");
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.Add(this.wizardPageUpload);
            this.wizardControl.Pages.Add(this.wizardPageRoll);
            this.wizardControl.Pages.Add(this.wizardPageDone);
            this.wizardControl.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl_Cancelling);
            this.wizardControl.Finished += new System.EventHandler(this.wizardControl_Finished);
            // 
            // wizardPageUpload
            // 
            this.wizardPageUpload.Controls.Add(this.textBoxFiles);
            this.wizardPageUpload.Controls.Add(this.label6);
            this.wizardPageUpload.Controls.Add(this.buttonDetails);
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
            this.wizardPageDone.Controls.Add(this.labelDone);
            this.wizardPageDone.Name = "wizardPageDone";
            resources.ApplyResources(this.wizardPageDone, "wizardPageDone");
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
            // WizardRoll
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardControl);
            this.Name = "WizardRoll";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardPageUpload.ResumeLayout(false);
            this.wizardPageUpload.PerformLayout();
            this.wizardPageRoll.ResumeLayout(false);
            this.wizardPageRoll.PerformLayout();
            this.wizardPageDone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl;
        private AeroWizard.WizardPage wizardPageUpload;
        private System.Windows.Forms.Label label6;
        private AeroWizard.WizardPage wizardPageRoll;
        private System.Windows.Forms.TextBox textBoxFiles;
        private System.Windows.Forms.Label labelUploading;
        private System.Windows.Forms.ProgressBar progressUpload;
        private System.Windows.Forms.Button buttonDetails;
        private System.ComponentModel.BackgroundWorker uploader;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Label labelUpStatus;
        private AeroWizard.WizardPage wizardPageDone;
        private System.Windows.Forms.Label labelDone;
    }
}