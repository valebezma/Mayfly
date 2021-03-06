namespace Mayfly.Software
{
    partial class WizardUninstall
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardUninstall));
            this.wizardControl = new AeroWizard.WizardControl();
            this.wizardPageStart = new AeroWizard.WizardPage();
            this.checkBoxOptions = new System.Windows.Forms.CheckBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.wizardPageOptions = new AeroWizard.WizardPage();
            this.checkBoxFeatures = new System.Windows.Forms.CheckBox();
            this.checkBoxLicenses = new System.Windows.Forms.CheckBox();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.wizardPageRun = new AeroWizard.WizardPage();
            this.listViewRun = new System.Windows.Forms.ListView();
            this.buttonRunCheck = new System.Windows.Forms.Button();
            this.labelCheckRun = new System.Windows.Forms.Label();
            this.wizardPageClean = new AeroWizard.WizardPage();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.labelGetInstruction = new System.Windows.Forms.Label();
            this.wizardPageDone = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.backCleaner = new System.ComponentModel.BackgroundWorker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardPageStart.SuspendLayout();
            this.wizardPageOptions.SuspendLayout();
            this.wizardPageRun.SuspendLayout();
            this.wizardPageClean.SuspendLayout();
            this.wizardPageDone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            this.wizardControl.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardControl, "wizardControl");
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.Add(this.wizardPageStart);
            this.wizardControl.Pages.Add(this.wizardPageOptions);
            this.wizardControl.Pages.Add(this.wizardPageRun);
            this.wizardControl.Pages.Add(this.wizardPageClean);
            this.wizardControl.Pages.Add(this.wizardPageDone);
            this.wizardControl.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl_Cancelled);
            this.wizardControl.Finished += new System.EventHandler(this.wizardControl_Finished);
            // 
            // wizardPageStart
            // 
            this.wizardPageStart.Controls.Add(this.checkBoxOptions);
            this.wizardPageStart.Controls.Add(this.labelStart);
            this.wizardPageStart.Name = "wizardPageStart";
            resources.ApplyResources(this.wizardPageStart, "wizardPageStart");
            // 
            // checkBoxOptions
            // 
            resources.ApplyResources(this.checkBoxOptions, "checkBoxOptions");
            this.checkBoxOptions.Name = "checkBoxOptions";
            this.checkBoxOptions.UseVisualStyleBackColor = true;
            this.checkBoxOptions.CheckedChanged += new System.EventHandler(this.checkBoxOptions_CheckedChanged);
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // wizardPageOptions
            // 
            this.wizardPageOptions.Controls.Add(this.checkBoxFeatures);
            this.wizardPageOptions.Controls.Add(this.checkBoxLicenses);
            this.wizardPageOptions.Controls.Add(this.labelInstruction);
            this.wizardPageOptions.Name = "wizardPageOptions";
            resources.ApplyResources(this.wizardPageOptions, "wizardPageOptions");
            this.wizardPageOptions.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageOptions_Commit);
            // 
            // checkBoxFeatures
            // 
            resources.ApplyResources(this.checkBoxFeatures, "checkBoxFeatures");
            this.checkBoxFeatures.Name = "checkBoxFeatures";
            this.checkBoxFeatures.UseVisualStyleBackColor = true;
            // 
            // checkBoxLicenses
            // 
            resources.ApplyResources(this.checkBoxLicenses, "checkBoxLicenses");
            this.checkBoxLicenses.Checked = true;
            this.checkBoxLicenses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLicenses.Name = "checkBoxLicenses";
            this.checkBoxLicenses.UseVisualStyleBackColor = true;
            // 
            // labelInstruction
            // 
            resources.ApplyResources(this.labelInstruction, "labelInstruction");
            this.labelInstruction.Name = "labelInstruction";
            // 
            // wizardPageRun
            // 
            this.wizardPageRun.Controls.Add(this.listViewRun);
            this.wizardPageRun.Controls.Add(this.buttonRunCheck);
            this.wizardPageRun.Controls.Add(this.labelCheckRun);
            this.wizardPageRun.Name = "wizardPageRun";
            resources.ApplyResources(this.wizardPageRun, "wizardPageRun");
            this.wizardPageRun.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageRun_Initialize);
            // 
            // listViewRun
            // 
            resources.ApplyResources(this.listViewRun, "listViewRun");
            this.listViewRun.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRun.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewRun.HideSelection = false;
            this.listViewRun.Name = "listViewRun";
            this.listViewRun.TileSize = new System.Drawing.Size(430, 25);
            this.listViewRun.UseCompatibleStateImageBehavior = false;
            this.listViewRun.View = System.Windows.Forms.View.Tile;
            // 
            // buttonRunCheck
            // 
            resources.ApplyResources(this.buttonRunCheck, "buttonRunCheck");
            this.buttonRunCheck.Name = "buttonRunCheck";
            this.buttonRunCheck.UseVisualStyleBackColor = true;
            this.buttonRunCheck.Click += new System.EventHandler(this.buttonRunCheck_Click);
            // 
            // labelCheckRun
            // 
            resources.ApplyResources(this.labelCheckRun, "labelCheckRun");
            this.labelCheckRun.Name = "labelCheckRun";
            // 
            // wizardPageClean
            // 
            this.wizardPageClean.AllowCancel = false;
            this.wizardPageClean.AllowNext = false;
            this.wizardPageClean.Controls.Add(this.textBoxStatus);
            this.wizardPageClean.Controls.Add(this.labelGetInstruction);
            this.wizardPageClean.Name = "wizardPageClean";
            this.wizardPageClean.ShowCancel = false;
            resources.ApplyResources(this.wizardPageClean, "wizardPageClean");
            this.wizardPageClean.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageClean_Initialize);
            // 
            // textBoxStatus
            // 
            resources.ApplyResources(this.textBoxStatus, "textBoxStatus");
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            // 
            // labelGetInstruction
            // 
            resources.ApplyResources(this.labelGetInstruction, "labelGetInstruction");
            this.labelGetInstruction.Name = "labelGetInstruction";
            // 
            // wizardPageDone
            // 
            this.wizardPageDone.AllowBack = false;
            this.wizardPageDone.Controls.Add(this.label1);
            this.wizardPageDone.Name = "wizardPageDone";
            this.wizardPageDone.ShowCancel = false;
            resources.ApplyResources(this.wizardPageDone, "wizardPageDone");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // backCleaner
            // 
            this.backCleaner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backCleaner_DoWork);
            this.backCleaner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backCleaner_RunWorkerCompleted);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // WizardUninstall
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.wizardControl);
            this.Name = "WizardUninstall";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardPageStart.ResumeLayout(false);
            this.wizardPageStart.PerformLayout();
            this.wizardPageOptions.ResumeLayout(false);
            this.wizardPageOptions.PerformLayout();
            this.wizardPageRun.ResumeLayout(false);
            this.wizardPageClean.ResumeLayout(false);
            this.wizardPageClean.PerformLayout();
            this.wizardPageDone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        private void WizardControl_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private AeroWizard.WizardControl wizardControl;
        private AeroWizard.WizardPage wizardPageStart;
        private AeroWizard.WizardPage wizardPageClean;
        private AeroWizard.WizardPage wizardPageDone;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGetInstruction;
        private System.ComponentModel.BackgroundWorker backCleaner;
        private AeroWizard.WizardPage wizardPageOptions;
        private System.Windows.Forms.CheckBox checkBoxOptions;
        private System.Windows.Forms.CheckBox checkBoxFeatures;
        private System.Windows.Forms.CheckBox checkBoxLicenses;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.PictureBox pictureBox2;
        private AeroWizard.WizardPage wizardPageRun;
        private System.Windows.Forms.ListView listViewRun;
        private System.Windows.Forms.Button buttonRunCheck;
        private System.Windows.Forms.Label labelCheckRun;
    }
}

