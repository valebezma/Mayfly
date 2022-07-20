namespace Mayfly.Controls
{
    partial class SettingControlUser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingControlUser));
            this.taskDialogNameMismatch = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbMismatchSupport = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbMismatchCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbMismatchReplace = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogLogout = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSignoutCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSignoutConfirm = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.comboBoxCulture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.checkBoxUseUnsafeConnection = new System.Windows.Forms.CheckBox();
            this.labelUpdates = new System.Windows.Forms.Label();
            this.labelUpdatePolicy = new System.Windows.Forms.Label();
            this.comboBoxUpdatePolicy = new System.Windows.Forms.ComboBox();
            this.labelDiagnosics = new System.Windows.Forms.Label();
            this.checkBoxShareDiagnostics = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).BeginInit();
            this.SuspendLayout();
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
            // SettingControlUser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxUseUnsafeConnection);
            this.Controls.Add(this.labelUpdates);
            this.Controls.Add(this.labelUpdatePolicy);
            this.Controls.Add(this.comboBoxUpdatePolicy);
            this.Controls.Add(this.labelDiagnosics);
            this.Controls.Add(this.checkBoxShareDiagnostics);
            this.Controls.Add(this.comboBoxCulture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBoxLogin);
            this.Controls.Add(this.maskedPass);
            this.Controls.Add(this.checkBoxCredentials);
            this.Controls.Add(this.listViewLicenses);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelFeaturesInstruction);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelPersonal);
            this.Name = "SettingControlUser";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TaskDialogs.TaskDialog taskDialogNameMismatch;
        private TaskDialogs.TaskDialog taskDialogLogout;
        private System.Windows.Forms.PictureBox pictureBoxLogin;
        private System.Windows.Forms.MaskedTextBox maskedPass;
        private System.Windows.Forms.ColumnHeader columnHeaderExpire;
        private System.Windows.Forms.ComboBox comboBoxCulture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxCredentials;
        private System.Windows.Forms.ListView listViewLicenses;
        private System.Windows.Forms.ColumnHeader columnHeaderLicense;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelFeaturesInstruction;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPersonal;
        private System.Windows.Forms.CheckBox checkBoxUseUnsafeConnection;
        private System.Windows.Forms.Label labelUpdates;
        private System.Windows.Forms.Label labelUpdatePolicy;
        private System.Windows.Forms.ComboBox comboBoxUpdatePolicy;
        private System.Windows.Forms.Label labelDiagnosics;
        private System.Windows.Forms.CheckBox checkBoxShareDiagnostics;
        private TaskDialogs.TaskDialogButton tdbMismatchCancel;
        private TaskDialogs.TaskDialogButton tdbMismatchReplace;
        private TaskDialogs.TaskDialogButton tdbMismatchSupport;
        private TaskDialogs.TaskDialogButton tdbSignoutCancel;
        private TaskDialogs.TaskDialogButton tdbSignoutConfirm;
    }
}
