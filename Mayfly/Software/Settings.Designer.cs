namespace Mayfly.Software
{
    partial class Settings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPagePrint = new System.Windows.Forms.TabPage();
            this.comboBoxCulture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBoxLogin = new System.Windows.Forms.PictureBox();
            this.maskedPass = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxCredentials = new System.Windows.Forms.CheckBox();
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.columnHeaderLicense = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpire = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelFeaturesInstruction = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPersonal = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxUseUnsafeConnection = new System.Windows.Forms.CheckBox();
            this.labelUpdates = new System.Windows.Forms.Label();
            this.labelUpdatePolicy = new System.Windows.Forms.Label();
            this.comboBoxUpdatePolicy = new System.Windows.Forms.ComboBox();
            this.labelDiagnosics = new System.Windows.Forms.Label();
            this.checkBoxShareDiagnostics = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.taskDialogNameMismatch = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbMismatchSupport = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbMismatchCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbMismatchReplace = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogLogout = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSignoutCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSignoutConfirm = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tabControlSettings.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPagePrint);
            this.tabControlSettings.Controls.Add(this.tabPage1);
            this.tabControlSettings.Controls.Add(this.tabPage2);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPagePrint
            // 
            this.tabPagePrint.Controls.Add(this.comboBoxCulture);
            this.tabPagePrint.Controls.Add(this.label2);
            this.tabPagePrint.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPagePrint, "tabPagePrint");
            this.tabPagePrint.Name = "tabPagePrint";
            this.tabPagePrint.UseVisualStyleBackColor = true;
            // 
            // comboBoxCulture
            // 
            resources.ApplyResources(this.comboBoxCulture, "comboBoxCulture");
            this.comboBoxCulture.DisplayMember = "NativeName";
            this.comboBoxCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulture.FormattingEnabled = true;
            this.comboBoxCulture.Name = "comboBoxCulture";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBoxLogin);
            this.tabPage1.Controls.Add(this.maskedPass);
            this.tabPage1.Controls.Add(this.checkBoxCredentials);
            this.tabPage1.Controls.Add(this.listViewLicenses);
            this.tabPage1.Controls.Add(this.buttonLogin);
            this.tabPage1.Controls.Add(this.labelFeaturesInstruction);
            this.tabPage1.Controls.Add(this.labelPass);
            this.tabPage1.Controls.Add(this.labelEmail);
            this.tabPage1.Controls.Add(this.labelUsername);
            this.tabPage1.Controls.Add(this.textBoxEmail);
            this.tabPage1.Controls.Add(this.textBoxUsername);
            this.tabPage1.Controls.Add(this.labelPersonal);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBoxLogin
            // 
            resources.ApplyResources(this.pictureBoxLogin, "pictureBoxLogin");
            this.pictureBoxLogin.Name = "pictureBoxLogin";
            this.pictureBoxLogin.TabStop = false;
            // 
            // maskedPass
            // 
            resources.ApplyResources(this.maskedPass, "maskedPass");
            this.maskedPass.AsciiOnly = true;
            this.maskedPass.HidePromptOnLeave = true;
            this.maskedPass.Name = "maskedPass";
            this.maskedPass.PasswordChar = '*';
            this.maskedPass.ReadOnly = true;
            this.maskedPass.DoubleClick += new System.EventHandler(this.textBoxEmail_DoubleClick);
            // 
            // checkBoxCredentials
            // 
            resources.ApplyResources(this.checkBoxCredentials, "checkBoxCredentials");
            this.checkBoxCredentials.Name = "checkBoxCredentials";
            this.checkBoxCredentials.UseVisualStyleBackColor = true;
            this.checkBoxCredentials.CheckedChanged += new System.EventHandler(this.checkBoxCredentials_CheckedChanged);
            this.checkBoxCredentials.Click += new System.EventHandler(this.checkBoxCredentials_Click);
            // 
            // listViewLicenses
            // 
            resources.ApplyResources(this.listViewLicenses, "listViewLicenses");
            this.listViewLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLicenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLicense,
            this.columnHeaderExpire});
            this.listViewLicenses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLicenses.HideSelection = false;
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderLicense
            // 
            resources.ApplyResources(this.columnHeaderLicense, "columnHeaderLicense");
            // 
            // columnHeaderExpire
            // 
            resources.ApplyResources(this.columnHeaderExpire, "columnHeaderExpire");
            // 
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelFeaturesInstruction
            // 
            resources.ApplyResources(this.labelFeaturesInstruction, "labelFeaturesInstruction");
            this.labelFeaturesInstruction.Name = "labelFeaturesInstruction";
            // 
            // labelPass
            // 
            resources.ApplyResources(this.labelPass, "labelPass");
            this.labelPass.Name = "labelPass";
            // 
            // labelEmail
            // 
            resources.ApplyResources(this.labelEmail, "labelEmail");
            this.labelEmail.Name = "labelEmail";
            // 
            // labelUsername
            // 
            resources.ApplyResources(this.labelUsername, "labelUsername");
            this.labelUsername.Name = "labelUsername";
            // 
            // textBoxEmail
            // 
            resources.ApplyResources(this.textBoxEmail, "textBoxEmail");
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.ReadOnly = true;
            this.textBoxEmail.DoubleClick += new System.EventHandler(this.textBoxEmail_DoubleClick);
            // 
            // textBoxUsername
            // 
            resources.ApplyResources(this.textBoxUsername, "textBoxUsername");
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.ReadOnly = true;
            // 
            // labelPersonal
            // 
            resources.ApplyResources(this.labelPersonal, "labelPersonal");
            this.labelPersonal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPersonal.Name = "labelPersonal";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxUseUnsafeConnection);
            this.tabPage2.Controls.Add(this.labelUpdates);
            this.tabPage2.Controls.Add(this.labelUpdatePolicy);
            this.tabPage2.Controls.Add(this.comboBoxUpdatePolicy);
            this.tabPage2.Controls.Add(this.labelDiagnosics);
            this.tabPage2.Controls.Add(this.checkBoxShareDiagnostics);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseUnsafeConnection
            // 
            resources.ApplyResources(this.checkBoxUseUnsafeConnection, "checkBoxUseUnsafeConnection");
            this.checkBoxUseUnsafeConnection.Name = "checkBoxUseUnsafeConnection";
            this.checkBoxUseUnsafeConnection.UseVisualStyleBackColor = true;
            // 
            // labelUpdates
            // 
            resources.ApplyResources(this.labelUpdates, "labelUpdates");
            this.labelUpdates.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelUpdates.Name = "labelUpdates";
            // 
            // labelUpdatePolicy
            // 
            resources.ApplyResources(this.labelUpdatePolicy, "labelUpdatePolicy");
            this.labelUpdatePolicy.Name = "labelUpdatePolicy";
            // 
            // comboBoxUpdatePolicy
            // 
            resources.ApplyResources(this.comboBoxUpdatePolicy, "comboBoxUpdatePolicy");
            this.comboBoxUpdatePolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpdatePolicy.FormattingEnabled = true;
            this.comboBoxUpdatePolicy.Items.AddRange(new object[] {
            resources.GetString("comboBoxUpdatePolicy.Items"),
            resources.GetString("comboBoxUpdatePolicy.Items1"),
            resources.GetString("comboBoxUpdatePolicy.Items2")});
            this.comboBoxUpdatePolicy.Name = "comboBoxUpdatePolicy";
            this.comboBoxUpdatePolicy.SelectedIndexChanged += new System.EventHandler(this.comboBoxUpdatePolicy_SelectedIndexChanged);
            // 
            // labelDiagnosics
            // 
            resources.ApplyResources(this.labelDiagnosics, "labelDiagnosics");
            this.labelDiagnosics.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelDiagnosics.Name = "labelDiagnosics";
            // 
            // checkBoxShareDiagnostics
            // 
            resources.ApplyResources(this.checkBoxShareDiagnostics, "checkBoxShareDiagnostics");
            this.checkBoxShareDiagnostics.Name = "checkBoxShareDiagnostics";
            this.checkBoxShareDiagnostics.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // taskDialogNameMismatch
            // 
            this.taskDialogNameMismatch.Buttons.Add(this.tdbMismatchSupport);
            this.taskDialogNameMismatch.Buttons.Add(this.tdbMismatchCancel);
            this.taskDialogNameMismatch.Buttons.Add(this.tdbMismatchReplace);
            this.taskDialogNameMismatch.CenterParent = true;
            resources.ApplyResources(this.taskDialogNameMismatch, "taskDialogNameMismatch");
            this.taskDialogNameMismatch.ExpandFooterArea = true;
            // 
            // tdbMismatchSupport
            // 
            resources.ApplyResources(this.tdbMismatchSupport, "tdbMismatchSupport");
            // 
            // tdbMismatchCancel
            // 
            this.tdbMismatchCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            resources.ApplyResources(this.tdbMismatchCancel, "tdbMismatchCancel");
            // 
            // tdbMismatchReplace
            // 
            this.tdbMismatchReplace.Default = true;
            resources.ApplyResources(this.tdbMismatchReplace, "tdbMismatchReplace");
            // 
            // taskDialogLogout
            // 
            this.taskDialogLogout.Buttons.Add(this.tdbSignoutCancel);
            this.taskDialogLogout.Buttons.Add(this.tdbSignoutConfirm);
            this.taskDialogLogout.CenterParent = true;
            resources.ApplyResources(this.taskDialogLogout, "taskDialogLogout");
            // 
            // tdbSignoutCancel
            // 
            resources.ApplyResources(this.tdbSignoutCancel, "tdbSignoutCancel");
            // 
            // tdbSignoutConfirm
            // 
            resources.ApplyResources(this.tdbSignoutConfirm, "tdbSignoutConfirm");
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabPagePrint.ResumeLayout(false);
            this.tabPagePrint.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Button buttonCancel;
        protected System.Windows.Forms.TabControl tabControlSettings;
        protected System.Windows.Forms.Button buttonOK;
        protected System.Windows.Forms.Button buttonApply;
        protected System.Windows.Forms.TabPage tabPagePrint;
        protected System.Windows.Forms.ComboBox comboBoxCulture;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        protected System.Windows.Forms.Label labelUsername;
        protected System.Windows.Forms.TextBox textBoxUsername;
        protected System.Windows.Forms.Label labelPersonal;
        private System.Windows.Forms.TabPage tabPage2;
        protected System.Windows.Forms.Label labelUpdates;
        protected System.Windows.Forms.Label labelUpdatePolicy;
        protected System.Windows.Forms.ComboBox comboBoxUpdatePolicy;
        protected System.Windows.Forms.Label labelDiagnosics;
        protected System.Windows.Forms.ListView listViewLicenses;
        protected System.Windows.Forms.ColumnHeader columnHeaderLicense;
        private System.Windows.Forms.ColumnHeader columnHeaderExpire;
        protected System.Windows.Forms.CheckBox checkBoxUseUnsafeConnection;
        protected System.Windows.Forms.CheckBox checkBoxShareDiagnostics;
        protected System.Windows.Forms.Button buttonLogin;
        protected System.Windows.Forms.Label labelFeaturesInstruction;
        private TaskDialogs.TaskDialog taskDialogNameMismatch;
        private TaskDialogs.TaskDialogButton tdbMismatchCancel;
        private TaskDialogs.TaskDialogButton tdbMismatchReplace;
        private TaskDialogs.TaskDialogButton tdbMismatchSupport;
        protected System.Windows.Forms.CheckBox checkBoxCredentials;
        protected System.Windows.Forms.Label labelPass;
        protected System.Windows.Forms.Label labelEmail;
        protected System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.MaskedTextBox maskedPass;
        private System.Windows.Forms.PictureBox pictureBoxLogin;
        private TaskDialogs.TaskDialog taskDialogLogout;
        private TaskDialogs.TaskDialogButton tdbSignoutCancel;
        private TaskDialogs.TaskDialogButton tdbSignoutConfirm;
    }
}