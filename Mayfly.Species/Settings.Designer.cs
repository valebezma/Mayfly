namespace Mayfly.Species
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
            this.tabPageAppearance = new System.Windows.Forms.TabPage();
            this.textBoxHigherFormat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLowerFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLowerTaxon = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tabPagePrint = new System.Windows.Forms.TabPage();
            this.textBoxCoupletChar = new System.Windows.Forms.TextBox();
            this.labelCoupletChar = new System.Windows.Forms.Label();
            this.radioButtonModern = new System.Windows.Forms.RadioButton();
            this.radioButtonClassic = new System.Windows.Forms.RadioButton();
            this.labelKeyReportView = new System.Windows.Forms.Label();
            this.labelPrintCaption = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.buttonApply = new System.Windows.Forms.Button();
            this.dialogLowerTaxon = new System.Windows.Forms.ColorDialog();
            this.checkBoxFillLower = new System.Windows.Forms.CheckBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageAppearance.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageAppearance);
            this.tabControlSettings.Controls.Add(this.tabPagePrint);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageAppearance
            // 
            this.tabPageAppearance.Controls.Add(this.checkBoxFillLower);
            this.tabPageAppearance.Controls.Add(this.textBoxHigherFormat);
            this.tabPageAppearance.Controls.Add(this.label2);
            this.tabPageAppearance.Controls.Add(this.textBoxLowerFormat);
            this.tabPageAppearance.Controls.Add(this.label1);
            this.tabPageAppearance.Controls.Add(this.panelLowerTaxon);
            this.tabPageAppearance.Controls.Add(this.label19);
            this.tabPageAppearance.Controls.Add(this.label26);
            resources.ApplyResources(this.tabPageAppearance, "tabPageAppearance");
            this.tabPageAppearance.Name = "tabPageAppearance";
            this.tabPageAppearance.UseVisualStyleBackColor = true;
            // 
            // textBoxHigherFormat
            // 
            resources.ApplyResources(this.textBoxHigherFormat, "textBoxHigherFormat");
            this.textBoxHigherFormat.Name = "textBoxHigherFormat";
            this.toolTip.SetToolTip(this.textBoxHigherFormat, resources.GetString("textBoxHigherFormat.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxLowerFormat
            // 
            resources.ApplyResources(this.textBoxLowerFormat, "textBoxLowerFormat");
            this.textBoxLowerFormat.Name = "textBoxLowerFormat";
            this.toolTip.SetToolTip(this.textBoxLowerFormat, resources.GetString("textBoxLowerFormat.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panelLowerTaxon
            // 
            this.panelLowerTaxon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelLowerTaxon, "panelLowerTaxon");
            this.panelLowerTaxon.Name = "panelLowerTaxon";
            this.panelLowerTaxon.Click += new System.EventHandler(this.panelLowerTaxon_Click);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label26.Name = "label26";
            // 
            // tabPagePrint
            // 
            this.tabPagePrint.Controls.Add(this.textBoxCoupletChar);
            this.tabPagePrint.Controls.Add(this.labelCoupletChar);
            this.tabPagePrint.Controls.Add(this.radioButtonModern);
            this.tabPagePrint.Controls.Add(this.radioButtonClassic);
            this.tabPagePrint.Controls.Add(this.labelKeyReportView);
            this.tabPagePrint.Controls.Add(this.labelPrintCaption);
            resources.ApplyResources(this.tabPagePrint, "tabPagePrint");
            this.tabPagePrint.Name = "tabPagePrint";
            this.tabPagePrint.UseVisualStyleBackColor = true;
            // 
            // textBoxCoupletChar
            // 
            resources.ApplyResources(this.textBoxCoupletChar, "textBoxCoupletChar");
            this.textBoxCoupletChar.Name = "textBoxCoupletChar";
            this.toolTip.SetToolTip(this.textBoxCoupletChar, resources.GetString("textBoxCoupletChar.ToolTip"));
            // 
            // labelCoupletChar
            // 
            resources.ApplyResources(this.labelCoupletChar, "labelCoupletChar");
            this.labelCoupletChar.Name = "labelCoupletChar";
            // 
            // radioButtonModern
            // 
            resources.ApplyResources(this.radioButtonModern, "radioButtonModern");
            this.radioButtonModern.Name = "radioButtonModern";
            this.radioButtonModern.UseVisualStyleBackColor = true;
            this.radioButtonModern.CheckedChanged += new System.EventHandler(this.radioButtonModern_CheckedChanged);
            // 
            // radioButtonClassic
            // 
            resources.ApplyResources(this.radioButtonClassic, "radioButtonClassic");
            this.radioButtonClassic.Checked = true;
            this.radioButtonClassic.Name = "radioButtonClassic";
            this.radioButtonClassic.TabStop = true;
            this.radioButtonClassic.UseVisualStyleBackColor = true;
            // 
            // labelKeyReportView
            // 
            resources.ApplyResources(this.labelKeyReportView, "labelKeyReportView");
            this.labelKeyReportView.Name = "labelKeyReportView";
            // 
            // labelPrintCaption
            // 
            resources.ApplyResources(this.labelPrintCaption, "labelPrintCaption");
            this.labelPrintCaption.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPrintCaption.Name = "labelPrintCaption";
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
            // dialogLowerTaxon
            // 
            this.dialogLowerTaxon.AnyColor = true;
            this.dialogLowerTaxon.FullOpen = true;
            // 
            // checkBoxFillLower
            // 
            resources.ApplyResources(this.checkBoxFillLower, "checkBoxFillLower");
            this.checkBoxFillLower.Name = "checkBoxFillLower";
            this.checkBoxFillLower.UseVisualStyleBackColor = true;
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
            this.tabPageAppearance.ResumeLayout(false);
            this.tabPageAppearance.PerformLayout();
            this.tabPagePrint.ResumeLayout(false);
            this.tabPagePrint.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPagePrint;
        private System.Windows.Forms.Label labelPrintCaption;
        private System.Windows.Forms.RadioButton radioButtonModern;
        private System.Windows.Forms.RadioButton radioButtonClassic;
        private System.Windows.Forms.Label labelKeyReportView;
        private System.Windows.Forms.TextBox textBoxCoupletChar;
        private System.Windows.Forms.Label labelCoupletChar;
        private System.Windows.Forms.TabPage tabPageAppearance;
        private System.Windows.Forms.Panel panelLowerTaxon;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBoxHigherFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLowerFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog dialogLowerTaxon;
        private System.Windows.Forms.CheckBox checkBoxFillLower;
    }
}