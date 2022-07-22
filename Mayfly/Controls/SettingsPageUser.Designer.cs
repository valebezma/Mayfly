namespace Mayfly.Controls
{
    partial class SettingsPageUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPageUser));
            this.taskDialogNameMismatch = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbMismatchSupport = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbMismatchCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbMismatchReplace = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogLogout = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSignoutCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSignoutConfirm = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.pictureBoxLogin = new System.Windows.Forms.PictureBox();
            this.maskedPass = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxCredentials = new System.Windows.Forms.CheckBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelPass = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPersonal = new System.Windows.Forms.Label();
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
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
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
            // SettingsControlUser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxLogin);
            this.Controls.Add(this.maskedPass);
            this.Controls.Add(this.checkBoxCredentials);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelPersonal);
            this.Name = "SettingsControlUser";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TaskDialogs.TaskDialog taskDialogNameMismatch;
        private TaskDialogs.TaskDialog taskDialogLogout;
        private System.Windows.Forms.PictureBox pictureBoxLogin;
        private System.Windows.Forms.MaskedTextBox maskedPass;
        private System.Windows.Forms.CheckBox checkBoxCredentials;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPersonal;
        private TaskDialogs.TaskDialogButton tdbMismatchCancel;
        private TaskDialogs.TaskDialogButton tdbMismatchReplace;
        private TaskDialogs.TaskDialogButton tdbMismatchSupport;
        private TaskDialogs.TaskDialogButton tdbSignoutCancel;
        private TaskDialogs.TaskDialogButton tdbSignoutConfirm;
    }
}
