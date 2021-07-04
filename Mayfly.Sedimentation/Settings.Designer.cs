namespace Mayfly.Sedimentation
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
            this.tabPageGGI = new System.Windows.Forms.TabPage();
            this.comboBoxGrainSize = new System.Windows.Forms.ComboBox();
            this.labelGrainSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageNGAVT = new System.Windows.Forms.TabPage();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxPart = new System.Windows.Forms.TextBox();
            this.textBoxD = new System.Windows.Forms.TextBox();
            this.labelD = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.textBoxGravity = new System.Windows.Forms.TextBox();
            this.labelGravity = new System.Windows.Forms.Label();
            this.textBoxSolidShape = new System.Windows.Forms.TextBox();
            this.labelSolidShape = new System.Windows.Forms.Label();
            this.textBoxSolidDensity = new System.Windows.Forms.TextBox();
            this.labelSolidDensity = new System.Windows.Forms.Label();
            this.textBoxWaterDensity = new System.Windows.Forms.TextBox();
            this.labelWaterDensity = new System.Windows.Forms.Label();
            this.labelGeneral = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCriticalLoad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCriticalSediment = new System.Windows.Forms.TextBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageGGI.SuspendLayout();
            this.tabPageNGAVT.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageGGI);
            this.tabControlSettings.Controls.Add(this.tabPageNGAVT);
            this.tabControlSettings.Controls.Add(this.tabPageGeneral);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageGGI
            // 
            this.tabPageGGI.Controls.Add(this.comboBoxGrainSize);
            this.tabPageGGI.Controls.Add(this.labelGrainSize);
            this.tabPageGGI.Controls.Add(this.label2);
            resources.ApplyResources(this.tabPageGGI, "tabPageGGI");
            this.tabPageGGI.Name = "tabPageGGI";
            this.tabPageGGI.UseVisualStyleBackColor = true;
            // 
            // comboBoxGrainSize
            // 
            resources.ApplyResources(this.comboBoxGrainSize, "comboBoxGrainSize");
            this.comboBoxGrainSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGrainSize.FormattingEnabled = true;
            this.comboBoxGrainSize.Items.AddRange(new object[] {
            resources.GetString("comboBoxGrainSize.Items"),
            resources.GetString("comboBoxGrainSize.Items1"),
            resources.GetString("comboBoxGrainSize.Items2")});
            this.comboBoxGrainSize.Name = "comboBoxGrainSize";
            // 
            // labelGrainSize
            // 
            resources.ApplyResources(this.labelGrainSize, "labelGrainSize");
            this.labelGrainSize.Name = "labelGrainSize";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // tabPageNGAVT
            // 
            this.tabPageNGAVT.Controls.Add(this.label27);
            this.tabPageNGAVT.Controls.Add(this.textBoxPart);
            this.tabPageNGAVT.Controls.Add(this.textBoxD);
            this.tabPageNGAVT.Controls.Add(this.labelD);
            this.tabPageNGAVT.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPageNGAVT, "tabPageNGAVT");
            this.tabPageNGAVT.Name = "tabPageNGAVT";
            this.tabPageNGAVT.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // textBoxPart
            // 
            this.textBoxPart.AllowDrop = true;
            resources.ApplyResources(this.textBoxPart, "textBoxPart");
            this.textBoxPart.Name = "textBoxPart";
            this.textBoxPart.TextChanged += new System.EventHandler(this.textBoxPart_TextChanged);
            this.textBoxPart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxD
            // 
            resources.ApplyResources(this.textBoxD, "textBoxD");
            this.textBoxD.Name = "textBoxD";
            this.textBoxD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelD
            // 
            resources.ApplyResources(this.labelD, "labelD");
            this.labelD.Name = "labelD";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.textBoxGravity);
            this.tabPageGeneral.Controls.Add(this.labelGravity);
            this.tabPageGeneral.Controls.Add(this.textBoxSolidShape);
            this.tabPageGeneral.Controls.Add(this.labelSolidShape);
            this.tabPageGeneral.Controls.Add(this.textBoxSolidDensity);
            this.tabPageGeneral.Controls.Add(this.labelSolidDensity);
            this.tabPageGeneral.Controls.Add(this.textBoxCriticalSediment);
            this.tabPageGeneral.Controls.Add(this.textBoxCriticalLoad);
            this.tabPageGeneral.Controls.Add(this.label5);
            this.tabPageGeneral.Controls.Add(this.textBoxWaterDensity);
            this.tabPageGeneral.Controls.Add(this.label4);
            this.tabPageGeneral.Controls.Add(this.labelWaterDensity);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.labelGeneral);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // textBoxGravity
            // 
            resources.ApplyResources(this.textBoxGravity, "textBoxGravity");
            this.textBoxGravity.Name = "textBoxGravity";
            this.textBoxGravity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelGravity
            // 
            resources.ApplyResources(this.labelGravity, "labelGravity");
            this.labelGravity.Name = "labelGravity";
            // 
            // textBoxSolidShape
            // 
            resources.ApplyResources(this.textBoxSolidShape, "textBoxSolidShape");
            this.textBoxSolidShape.Name = "textBoxSolidShape";
            this.textBoxSolidShape.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelSolidShape
            // 
            resources.ApplyResources(this.labelSolidShape, "labelSolidShape");
            this.labelSolidShape.Name = "labelSolidShape";
            // 
            // textBoxSolidDensity
            // 
            resources.ApplyResources(this.textBoxSolidDensity, "textBoxSolidDensity");
            this.textBoxSolidDensity.Name = "textBoxSolidDensity";
            this.textBoxSolidDensity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelSolidDensity
            // 
            resources.ApplyResources(this.labelSolidDensity, "labelSolidDensity");
            this.labelSolidDensity.Name = "labelSolidDensity";
            // 
            // textBoxWaterDensity
            // 
            resources.ApplyResources(this.textBoxWaterDensity, "textBoxWaterDensity");
            this.textBoxWaterDensity.Name = "textBoxWaterDensity";
            this.textBoxWaterDensity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelWaterDensity
            // 
            resources.ApplyResources(this.labelWaterDensity, "labelWaterDensity");
            this.labelWaterDensity.Name = "labelWaterDensity";
            // 
            // labelGeneral
            // 
            resources.ApplyResources(this.labelGeneral, "labelGeneral");
            this.labelGeneral.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelGeneral.Name = "labelGeneral";
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxCriticalLoad
            // 
            resources.ApplyResources(this.textBoxCriticalLoad, "textBoxCriticalLoad");
            this.textBoxCriticalLoad.Name = "textBoxCriticalLoad";
            this.textBoxCriticalLoad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxCriticalSediment
            // 
            resources.ApplyResources(this.textBoxCriticalSediment, "textBoxCriticalSediment");
            this.textBoxCriticalSediment.Name = "textBoxCriticalSediment";
            this.textBoxCriticalSediment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
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
            this.tabPageGGI.ResumeLayout(false);
            this.tabPageGGI.PerformLayout();
            this.tabPageNGAVT.ResumeLayout(false);
            this.tabPageNGAVT.PerformLayout();
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label labelGeneral;
        private System.Windows.Forms.TextBox textBoxSolidDensity;
        private System.Windows.Forms.Label labelSolidDensity;
        private System.Windows.Forms.TextBox textBoxWaterDensity;
        private System.Windows.Forms.Label labelWaterDensity;
        private System.Windows.Forms.TextBox textBoxGravity;
        private System.Windows.Forms.Label labelGravity;
        private System.Windows.Forms.TextBox textBoxSolidShape;
        private System.Windows.Forms.Label labelSolidShape;
        private System.Windows.Forms.TabPage tabPageGGI;
        private System.Windows.Forms.ComboBox comboBoxGrainSize;
        private System.Windows.Forms.Label labelGrainSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageNGAVT;
        private System.Windows.Forms.TextBox textBoxD;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBoxPart;
        private System.Windows.Forms.TextBox textBoxCriticalSediment;
        private System.Windows.Forms.TextBox textBoxCriticalLoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}