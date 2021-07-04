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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPagePrint = new System.Windows.Forms.TabPage();
            this.textBoxCoupletChar = new System.Windows.Forms.TextBox();
            this.labelCoupletChar = new System.Windows.Forms.Label();
            this.radioButtonModern = new System.Windows.Forms.RadioButton();
            this.radioButtonClassic = new System.Windows.Forms.RadioButton();
            this.labelKeyReportView = new System.Windows.Forms.Label();
            this.labelPrintCaption = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonApply = new System.Windows.Forms.Button();
            this.colorDialogSelectedSeries = new System.Windows.Forms.ColorDialog();
            this.colorDialogUnselectedSeries = new System.Windows.Forms.ColorDialog();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPagePrint);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
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
            // colorDialogSelectedSeries
            // 
            this.colorDialogSelectedSeries.Color = System.Drawing.Color.Coral;
            // 
            // colorDialogUnselectedSeries
            // 
            this.colorDialogUnselectedSeries.Color = System.Drawing.Color.Coral;
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
        private System.Windows.Forms.ColorDialog colorDialogSelectedSeries;
        private System.Windows.Forms.ColorDialog colorDialogUnselectedSeries;
        private System.Windows.Forms.RadioButton radioButtonModern;
        private System.Windows.Forms.RadioButton radioButtonClassic;
        private System.Windows.Forms.Label labelKeyReportView;
        private System.Windows.Forms.TextBox textBoxCoupletChar;
        private System.Windows.Forms.Label labelCoupletChar;
    }
}