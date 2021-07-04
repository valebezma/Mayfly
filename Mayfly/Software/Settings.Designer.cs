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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPagePrint = new System.Windows.Forms.TabPage();
            this.comboBoxCulture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.columnHeaderFeature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpire = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.buttonGrant = new System.Windows.Forms.Button();
            this.labelFeaturesInstruction = new System.Windows.Forms.Label();
            this.labelFeatures = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPersonal = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUpdatePolicy = new System.Windows.Forms.Label();
            this.comboBoxUpdatePolicy = new System.Windows.Forms.ComboBox();
            this.comboBoxKeepLog = new System.Windows.Forms.ComboBox();
            this.labelLogSize = new System.Windows.Forms.Label();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.labelPerformance = new System.Windows.Forms.Label();
            this.checkBoxLogSend = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepLog = new System.Windows.Forms.CheckBox();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.checkBoxUseUnsafeConnection = new System.Windows.Forms.CheckBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.comboBoxCulture.DisplayMember = "DisplayName";
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
            this.tabPage1.Controls.Add(this.listViewLicenses);
            this.tabPage1.Controls.Add(this.buttonUnlock);
            this.tabPage1.Controls.Add(this.buttonGrant);
            this.tabPage1.Controls.Add(this.labelFeaturesInstruction);
            this.tabPage1.Controls.Add(this.labelFeatures);
            this.tabPage1.Controls.Add(this.labelUsername);
            this.tabPage1.Controls.Add(this.textBoxUsername);
            this.tabPage1.Controls.Add(this.labelPersonal);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listViewLicenses
            // 
            resources.ApplyResources(this.listViewLicenses, "listViewLicenses");
            this.listViewLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLicenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFeature,
            this.columnHeaderExpire});
            this.listViewLicenses.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLicenses.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLicenses.Groups1")))});
            this.listViewLicenses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLicenses.HideSelection = false;
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Details;
            this.listViewLicenses.ItemActivate += new System.EventHandler(this.listViewLicenses_ItemActivate);
            // 
            // columnHeaderFeature
            // 
            resources.ApplyResources(this.columnHeaderFeature, "columnHeaderFeature");
            // 
            // columnHeaderExpire
            // 
            resources.ApplyResources(this.columnHeaderExpire, "columnHeaderExpire");
            // 
            // buttonUnlock
            // 
            resources.ApplyResources(this.buttonUnlock, "buttonUnlock");
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.UseVisualStyleBackColor = true;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // buttonGrant
            // 
            resources.ApplyResources(this.buttonGrant, "buttonGrant");
            this.buttonGrant.Name = "buttonGrant";
            this.buttonGrant.UseVisualStyleBackColor = true;
            // 
            // labelFeaturesInstruction
            // 
            resources.ApplyResources(this.labelFeaturesInstruction, "labelFeaturesInstruction");
            this.labelFeaturesInstruction.Name = "labelFeaturesInstruction";
            // 
            // labelFeatures
            // 
            resources.ApplyResources(this.labelFeatures, "labelFeatures");
            this.labelFeatures.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelFeatures.Name = "labelFeatures";
            // 
            // labelUsername
            // 
            resources.ApplyResources(this.labelUsername, "labelUsername");
            this.labelUsername.Name = "labelUsername";
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
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.labelUpdatePolicy);
            this.tabPage2.Controls.Add(this.comboBoxUpdatePolicy);
            this.tabPage2.Controls.Add(this.comboBoxKeepLog);
            this.tabPage2.Controls.Add(this.labelLogSize);
            this.tabPage2.Controls.Add(this.buttonOpenLog);
            this.tabPage2.Controls.Add(this.buttonClearLog);
            this.tabPage2.Controls.Add(this.labelPerformance);
            this.tabPage2.Controls.Add(this.checkBoxLogSend);
            this.tabPage2.Controls.Add(this.checkBoxKeepLog);
            this.tabPage2.Controls.Add(this.checkBoxLog);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Name = "label3";
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
            // 
            // comboBoxKeepLog
            // 
            resources.ApplyResources(this.comboBoxKeepLog, "comboBoxKeepLog");
            this.comboBoxKeepLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeepLog.FormattingEnabled = true;
            this.comboBoxKeepLog.Items.AddRange(new object[] {
            resources.GetString("comboBoxKeepLog.Items"),
            resources.GetString("comboBoxKeepLog.Items1"),
            resources.GetString("comboBoxKeepLog.Items2"),
            resources.GetString("comboBoxKeepLog.Items3")});
            this.comboBoxKeepLog.Name = "comboBoxKeepLog";
            // 
            // labelLogSize
            // 
            resources.ApplyResources(this.labelLogSize, "labelLogSize");
            this.labelLogSize.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelLogSize.Name = "labelLogSize";
            // 
            // buttonOpenLog
            // 
            resources.ApplyResources(this.buttonOpenLog, "buttonOpenLog");
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // buttonClearLog
            // 
            resources.ApplyResources(this.buttonClearLog, "buttonClearLog");
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // labelPerformance
            // 
            resources.ApplyResources(this.labelPerformance, "labelPerformance");
            this.labelPerformance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPerformance.Name = "labelPerformance";
            // 
            // checkBoxLogSend
            // 
            resources.ApplyResources(this.checkBoxLogSend, "checkBoxLogSend");
            this.checkBoxLogSend.Name = "checkBoxLogSend";
            this.checkBoxLogSend.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeepLog
            // 
            resources.ApplyResources(this.checkBoxKeepLog, "checkBoxKeepLog");
            this.checkBoxKeepLog.Name = "checkBoxKeepLog";
            this.checkBoxKeepLog.UseVisualStyleBackColor = true;
            this.checkBoxKeepLog.CheckedChanged += new System.EventHandler(this.checkBoxKeepLog_CheckedChanged);
            // 
            // checkBoxLog
            // 
            resources.ApplyResources(this.checkBoxLog, "checkBoxLog");
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            this.checkBoxLog.CheckedChanged += new System.EventHandler(this.checkBoxLog_CheckedChanged);
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
            // checkBoxUseUnsafeConnection
            // 
            resources.ApplyResources(this.checkBoxUseUnsafeConnection, "checkBoxUseUnsafeConnection");
            this.checkBoxUseUnsafeConnection.Name = "checkBoxUseUnsafeConnection";
            this.checkBoxUseUnsafeConnection.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AcceptButton = this.buttonOK;
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
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label labelUpdatePolicy;
        protected System.Windows.Forms.ComboBox comboBoxUpdatePolicy;
        private System.Windows.Forms.ComboBox comboBoxKeepLog;
        protected System.Windows.Forms.Label labelLogSize;
        protected System.Windows.Forms.Button buttonOpenLog;
        protected System.Windows.Forms.Button buttonClearLog;
        protected System.Windows.Forms.Label labelPerformance;
        protected System.Windows.Forms.CheckBox checkBoxLogSend;
        protected System.Windows.Forms.CheckBox checkBoxKeepLog;
        protected System.Windows.Forms.CheckBox checkBoxLog;
        protected System.Windows.Forms.Label labelFeatures;
        protected System.Windows.Forms.Label labelFeaturesInstruction;
        protected System.Windows.Forms.Button buttonGrant;
        protected System.Windows.Forms.Button buttonUnlock;
        protected System.Windows.Forms.ListView listViewLicenses;
        protected System.Windows.Forms.ColumnHeader columnHeaderFeature;
        private System.Windows.Forms.ColumnHeader columnHeaderExpire;
        protected System.Windows.Forms.CheckBox checkBoxUseUnsafeConnection;
    }
}