namespace Mayfly.Software
{
    partial class WizardInstall
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardInstall));
            this.wizardControl = new AeroWizard.WizardControl();
            this.wizardPageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.wizardPageProduct = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPageEula = new AeroWizard.WizardPage();
            this.webBrowserEula = new System.Windows.Forms.WebBrowser();
            this.labelEula = new System.Windows.Forms.Label();
            this.wizardPageLanguage = new AeroWizard.WizardPage();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLangInstruction = new System.Windows.Forms.Label();
            this.checkBoxOptions = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.labelLang = new System.Windows.Forms.Label();
            this.wizardPageShortcuts = new AeroWizard.WizardPage();
            this.checkBoxDesktop = new System.Windows.Forms.CheckBox();
            this.checkBoxStart = new System.Windows.Forms.CheckBox();
            this.labelShortcutInstruction = new System.Windows.Forms.Label();
            this.wizardPageFiletypes = new AeroWizard.WizardPage();
            this.radioFiletypeKeep = new System.Windows.Forms.RadioButton();
            this.radioFileTypeAsk = new System.Windows.Forms.RadioButton();
            this.radioFileTypeReass = new System.Windows.Forms.RadioButton();
            this.checkBoxRegPropHandler = new System.Windows.Forms.CheckBox();
            this.checkBoxRegFileType = new System.Windows.Forms.CheckBox();
            this.labelFileTypeExists = new System.Windows.Forms.Label();
            this.labelFileTypes = new System.Windows.Forms.Label();
            this.wizardPageGet = new AeroWizard.WizardPage();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelGetInstruction = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.wizardPageDone = new AeroWizard.WizardPage();
            this.labelDone = new System.Windows.Forms.Label();
            this.backgroundDownloader = new System.ComponentModel.BackgroundWorker();
            this.backgroundCultures = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.taskDialogReassoc = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbKeep = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbReassoc = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardPageStart.SuspendLayout();
            this.wizardPageProduct.SuspendLayout();
            this.wizardPageEula.SuspendLayout();
            this.wizardPageLanguage.SuspendLayout();
            this.wizardPageShortcuts.SuspendLayout();
            this.wizardPageFiletypes.SuspendLayout();
            this.wizardPageGet.SuspendLayout();
            this.wizardPageDone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            resources.ApplyResources(this.wizardControl, "wizardControl");
            this.wizardControl.BackColor = System.Drawing.Color.White;
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.Add(this.wizardPageStart);
            this.wizardControl.Pages.Add(this.wizardPageProduct);
            this.wizardControl.Pages.Add(this.wizardPageEula);
            this.wizardControl.Pages.Add(this.wizardPageLanguage);
            this.wizardControl.Pages.Add(this.wizardPageShortcuts);
            this.wizardControl.Pages.Add(this.wizardPageFiletypes);
            this.wizardControl.Pages.Add(this.wizardPageGet);
            this.wizardControl.Pages.Add(this.wizardPageDone);
            this.wizardControl.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl_Cancelling);
            this.wizardControl.Finished += new System.EventHandler(this.wizardControl_Finished);
            // 
            // wizardPageStart
            // 
            resources.ApplyResources(this.wizardPageStart, "wizardPageStart");
            this.wizardPageStart.Controls.Add(this.labelStart);
            this.wizardPageStart.Name = "wizardPageStart";
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // wizardPageProduct
            // 
            resources.ApplyResources(this.wizardPageProduct, "wizardPageProduct");
            this.wizardPageProduct.Controls.Add(this.label1);
            this.wizardPageProduct.Controls.Add(this.comboBoxProduct);
            this.wizardPageProduct.Controls.Add(this.label2);
            this.wizardPageProduct.Name = "wizardPageProduct";
            this.wizardPageProduct.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageProduct_Commit);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBoxProduct
            // 
            resources.ApplyResources(this.comboBoxProduct, "comboBoxProduct");
            this.comboBoxProduct.DisplayMember = "Name";
            this.comboBoxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProduct.FormattingEnabled = true;
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.SelectedIndexChanged += new System.EventHandler(this.comboBoxProduct_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // wizardPageEula
            // 
            resources.ApplyResources(this.wizardPageEula, "wizardPageEula");
            this.wizardPageEula.AllowNext = false;
            this.wizardPageEula.Controls.Add(this.webBrowserEula);
            this.wizardPageEula.Controls.Add(this.labelEula);
            this.wizardPageEula.Name = "wizardPageEula";
            this.wizardPageEula.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageEula_Commit);
            this.wizardPageEula.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageEula_Initialize);
            this.wizardPageEula.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageEula_Rollback);
            // 
            // webBrowserEula
            // 
            resources.ApplyResources(this.webBrowserEula, "webBrowserEula");
            this.webBrowserEula.Name = "webBrowserEula";
            // 
            // labelEula
            // 
            resources.ApplyResources(this.labelEula, "labelEula");
            this.labelEula.Name = "labelEula";
            // 
            // wizardPageLanguage
            // 
            resources.ApplyResources(this.wizardPageLanguage, "wizardPageLanguage");
            this.wizardPageLanguage.Controls.Add(this.textBoxUsername);
            this.wizardPageLanguage.Controls.Add(this.label4);
            this.wizardPageLanguage.Controls.Add(this.labelLangInstruction);
            this.wizardPageLanguage.Controls.Add(this.checkBoxOptions);
            this.wizardPageLanguage.Controls.Add(this.label3);
            this.wizardPageLanguage.Controls.Add(this.comboBoxLang);
            this.wizardPageLanguage.Controls.Add(this.labelLang);
            this.wizardPageLanguage.Name = "wizardPageLanguage";
            this.wizardPageLanguage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageLanguage_Commit);
            this.wizardPageLanguage.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageLanguage_Initialize);
            // 
            // textBoxUsername
            // 
            resources.ApplyResources(this.textBoxUsername, "textBoxUsername");
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.TextChanged += new System.EventHandler(this.textBoxUsername_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // labelLangInstruction
            // 
            resources.ApplyResources(this.labelLangInstruction, "labelLangInstruction");
            this.labelLangInstruction.Name = "labelLangInstruction";
            // 
            // checkBoxOptions
            // 
            resources.ApplyResources(this.checkBoxOptions, "checkBoxOptions");
            this.checkBoxOptions.Name = "checkBoxOptions";
            this.checkBoxOptions.UseVisualStyleBackColor = true;
            this.checkBoxOptions.CheckedChanged += new System.EventHandler(this.checkBoxOptions_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBoxLang
            // 
            resources.ApplyResources(this.comboBoxLang, "comboBoxLang");
            this.comboBoxLang.DisplayMember = "DisplayName";
            this.comboBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLang.FormattingEnabled = true;
            this.comboBoxLang.Name = "comboBoxLang";
            this.comboBoxLang.SelectedIndexChanged += new System.EventHandler(this.comboBoxLang_SelectedIndexChanged);
            // 
            // labelLang
            // 
            resources.ApplyResources(this.labelLang, "labelLang");
            this.labelLang.Name = "labelLang";
            // 
            // wizardPageShortcuts
            // 
            resources.ApplyResources(this.wizardPageShortcuts, "wizardPageShortcuts");
            this.wizardPageShortcuts.Controls.Add(this.checkBoxDesktop);
            this.wizardPageShortcuts.Controls.Add(this.checkBoxStart);
            this.wizardPageShortcuts.Controls.Add(this.labelShortcutInstruction);
            this.wizardPageShortcuts.Name = "wizardPageShortcuts";
            // 
            // checkBoxDesktop
            // 
            resources.ApplyResources(this.checkBoxDesktop, "checkBoxDesktop");
            this.checkBoxDesktop.Checked = true;
            this.checkBoxDesktop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDesktop.Name = "checkBoxDesktop";
            this.checkBoxDesktop.UseVisualStyleBackColor = true;
            // 
            // checkBoxStart
            // 
            resources.ApplyResources(this.checkBoxStart, "checkBoxStart");
            this.checkBoxStart.Checked = true;
            this.checkBoxStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            // 
            // labelShortcutInstruction
            // 
            resources.ApplyResources(this.labelShortcutInstruction, "labelShortcutInstruction");
            this.labelShortcutInstruction.Name = "labelShortcutInstruction";
            // 
            // wizardPageFiletypes
            // 
            resources.ApplyResources(this.wizardPageFiletypes, "wizardPageFiletypes");
            this.wizardPageFiletypes.Controls.Add(this.radioFiletypeKeep);
            this.wizardPageFiletypes.Controls.Add(this.radioFileTypeAsk);
            this.wizardPageFiletypes.Controls.Add(this.radioFileTypeReass);
            this.wizardPageFiletypes.Controls.Add(this.checkBoxRegPropHandler);
            this.wizardPageFiletypes.Controls.Add(this.checkBoxRegFileType);
            this.wizardPageFiletypes.Controls.Add(this.labelFileTypeExists);
            this.wizardPageFiletypes.Controls.Add(this.labelFileTypes);
            this.wizardPageFiletypes.Name = "wizardPageFiletypes";
            // 
            // radioFiletypeKeep
            // 
            resources.ApplyResources(this.radioFiletypeKeep, "radioFiletypeKeep");
            this.radioFiletypeKeep.Name = "radioFiletypeKeep";
            this.radioFiletypeKeep.UseVisualStyleBackColor = true;
            // 
            // radioFileTypeAsk
            // 
            resources.ApplyResources(this.radioFileTypeAsk, "radioFileTypeAsk");
            this.radioFileTypeAsk.Checked = true;
            this.radioFileTypeAsk.Name = "radioFileTypeAsk";
            this.radioFileTypeAsk.TabStop = true;
            this.radioFileTypeAsk.UseVisualStyleBackColor = true;
            // 
            // radioFileTypeReass
            // 
            resources.ApplyResources(this.radioFileTypeReass, "radioFileTypeReass");
            this.radioFileTypeReass.Name = "radioFileTypeReass";
            this.radioFileTypeReass.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegPropHandler
            // 
            resources.ApplyResources(this.checkBoxRegPropHandler, "checkBoxRegPropHandler");
            this.checkBoxRegPropHandler.Name = "checkBoxRegPropHandler";
            this.checkBoxRegPropHandler.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegFileType
            // 
            resources.ApplyResources(this.checkBoxRegFileType, "checkBoxRegFileType");
            this.checkBoxRegFileType.Checked = true;
            this.checkBoxRegFileType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRegFileType.Name = "checkBoxRegFileType";
            this.checkBoxRegFileType.UseVisualStyleBackColor = true;
            this.checkBoxRegFileType.CheckedChanged += new System.EventHandler(this.checkBoxRegFileType_CheckedChanged);
            // 
            // labelFileTypeExists
            // 
            resources.ApplyResources(this.labelFileTypeExists, "labelFileTypeExists");
            this.labelFileTypeExists.Name = "labelFileTypeExists";
            // 
            // labelFileTypes
            // 
            resources.ApplyResources(this.labelFileTypes, "labelFileTypes");
            this.labelFileTypes.Name = "labelFileTypes";
            // 
            // wizardPageGet
            // 
            resources.ApplyResources(this.wizardPageGet, "wizardPageGet");
            this.wizardPageGet.AllowBack = false;
            this.wizardPageGet.AllowCancel = false;
            this.wizardPageGet.AllowNext = false;
            this.wizardPageGet.Controls.Add(this.labelStatus);
            this.wizardPageGet.Controls.Add(this.labelGetInstruction);
            this.wizardPageGet.Controls.Add(this.progressBar);
            this.wizardPageGet.Name = "wizardPageGet";
            this.wizardPageGet.ShowCancel = false;
            this.wizardPageGet.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageGet_Initialize);
            // 
            // labelStatus
            // 
            resources.ApplyResources(this.labelStatus, "labelStatus");
            this.labelStatus.AutoEllipsis = true;
            this.labelStatus.Name = "labelStatus";
            // 
            // labelGetInstruction
            // 
            resources.ApplyResources(this.labelGetInstruction, "labelGetInstruction");
            this.labelGetInstruction.Name = "labelGetInstruction";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // wizardPageDone
            // 
            resources.ApplyResources(this.wizardPageDone, "wizardPageDone");
            this.wizardPageDone.AllowBack = false;
            this.wizardPageDone.Controls.Add(this.labelDone);
            this.wizardPageDone.Name = "wizardPageDone";
            this.wizardPageDone.ShowCancel = false;
            // 
            // labelDone
            // 
            resources.ApplyResources(this.labelDone, "labelDone");
            this.labelDone.Name = "labelDone";
            // 
            // backgroundDownloader
            // 
            this.backgroundDownloader.WorkerReportsProgress = true;
            this.backgroundDownloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundDownloader_DoWork);
            this.backgroundDownloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundDownloader_ProgressChanged);
            this.backgroundDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundDownloader_RunWorkerCompleted);
            // 
            // backgroundCultures
            // 
            this.backgroundCultures.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundCultures_DoWork);
            this.backgroundCultures.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundCultures_RunWorkerCompleted);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // taskDialogReassoc
            // 
            this.taskDialogReassoc.Buttons.Add(this.tdbKeep);
            this.taskDialogReassoc.Buttons.Add(this.tdbReassoc);
            resources.ApplyResources(this.taskDialogReassoc, "taskDialogReassoc");
            // 
            // tdbKeep
            // 
            resources.ApplyResources(this.tdbKeep, "tdbKeep");
            // 
            // tdbReassoc
            // 
            resources.ApplyResources(this.tdbReassoc, "tdbReassoc");
            this.tdbReassoc.Default = true;
            // 
            // WizardInstall
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.wizardControl);
            this.Name = "WizardInstall";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardPageStart.ResumeLayout(false);
            this.wizardPageProduct.ResumeLayout(false);
            this.wizardPageProduct.PerformLayout();
            this.wizardPageEula.ResumeLayout(false);
            this.wizardPageLanguage.ResumeLayout(false);
            this.wizardPageLanguage.PerformLayout();
            this.wizardPageShortcuts.ResumeLayout(false);
            this.wizardPageShortcuts.PerformLayout();
            this.wizardPageFiletypes.ResumeLayout(false);
            this.wizardPageFiletypes.PerformLayout();
            this.wizardPageGet.ResumeLayout(false);
            this.wizardPageGet.PerformLayout();
            this.wizardPageDone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl;
        private AeroWizard.WizardPage wizardPageStart;
        private System.Windows.Forms.Label labelStart;
        private AeroWizard.WizardPage wizardPageEula;
        private System.Windows.Forms.Label labelEula;
        private System.Windows.Forms.WebBrowser webBrowserEula;
        private AeroWizard.WizardPage wizardPageShortcuts;
        private System.Windows.Forms.CheckBox checkBoxDesktop;
        private System.Windows.Forms.CheckBox checkBoxStart;
        private System.Windows.Forms.Label labelShortcutInstruction;
        private AeroWizard.WizardPage wizardPageGet;
        private System.Windows.Forms.Label labelGetInstruction;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelStatus;
        private System.ComponentModel.BackgroundWorker backgroundDownloader;
        private AeroWizard.WizardPage wizardPageDone;
        private System.Windows.Forms.Label labelDone;
        private System.ComponentModel.BackgroundWorker backgroundCultures;
        private AeroWizard.WizardPage wizardPageLanguage;
        private System.Windows.Forms.Label labelLangInstruction;
        private System.Windows.Forms.CheckBox checkBoxOptions;
        private System.Windows.Forms.ComboBox comboBoxLang;
        private System.Windows.Forms.Label labelLang;
        private TaskDialogs.TaskDialog taskDialogReassoc;
        private TaskDialogs.TaskDialogButton tdbKeep;
        private TaskDialogs.TaskDialogButton tdbReassoc;
        private AeroWizard.WizardPage wizardPageProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProduct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private AeroWizard.WizardPage wizardPageFiletypes;
        private System.Windows.Forms.CheckBox checkBoxRegPropHandler;
        private System.Windows.Forms.CheckBox checkBoxRegFileType;
        private System.Windows.Forms.Label labelFileTypes;
        private System.Windows.Forms.Label labelFileTypeExists;
        private System.Windows.Forms.RadioButton radioFiletypeKeep;
        private System.Windows.Forms.RadioButton radioFileTypeAsk;
        private System.Windows.Forms.RadioButton radioFileTypeReass;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

