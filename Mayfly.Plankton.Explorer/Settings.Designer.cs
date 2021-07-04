namespace Mayfly.Plankton.Explorer
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
            this.tabPageTreat = new System.Windows.Forms.TabPage();
            this.listViewBio = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonBioRemove = new System.Windows.Forms.Button();
            this.buttonBioBrowse = new System.Windows.Forms.Button();
            this.checkBoxBioAutoLoad = new System.Windows.Forms.CheckBox();
            this.comboBoxDominance = new System.Windows.Forms.ComboBox();
            this.labelDominance = new System.Windows.Forms.Label();
            this.comboBoxDiversity = new System.Windows.Forms.ComboBox();
            this.labelDiversity = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelIndices = new System.Windows.Forms.Label();
            this.tabPageOther = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonProductSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFish = new System.Windows.Forms.Label();
            this.buttonMath = new System.Windows.Forms.Button();
            this.buttonPlankton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonBasicSettings = new System.Windows.Forms.Button();
            this.tabControlSettings.SuspendLayout();
            this.tabPageTreat.SuspendLayout();
            this.tabPageOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageTreat);
            this.tabControlSettings.Controls.Add(this.tabPageOther);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageTreat
            // 
            this.tabPageTreat.Controls.Add(this.listViewBio);
            this.tabPageTreat.Controls.Add(this.buttonBioRemove);
            this.tabPageTreat.Controls.Add(this.buttonBioBrowse);
            this.tabPageTreat.Controls.Add(this.checkBoxBioAutoLoad);
            this.tabPageTreat.Controls.Add(this.comboBoxDominance);
            this.tabPageTreat.Controls.Add(this.labelDominance);
            this.tabPageTreat.Controls.Add(this.comboBoxDiversity);
            this.tabPageTreat.Controls.Add(this.labelDiversity);
            this.tabPageTreat.Controls.Add(this.label2);
            this.tabPageTreat.Controls.Add(this.labelIndices);
            resources.ApplyResources(this.tabPageTreat, "tabPageTreat");
            this.tabPageTreat.Name = "tabPageTreat";
            this.tabPageTreat.UseVisualStyleBackColor = true;
            // 
            // listViewBio
            // 
            resources.ApplyResources(this.listViewBio, "listViewBio");
            this.listViewBio.CheckBoxes = true;
            this.listViewBio.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName});
            this.listViewBio.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewBio.HideSelection = false;
            this.listViewBio.LabelEdit = true;
            this.listViewBio.Name = "listViewBio";
            this.listViewBio.ShowGroups = false;
            this.listViewBio.UseCompatibleStateImageBehavior = false;
            this.listViewBio.View = System.Windows.Forms.View.Details;
            this.listViewBio.SelectedIndexChanged += new System.EventHandler(this.listViewBio_SelectedIndexChanged);
            // 
            // columnFileName
            // 
            resources.ApplyResources(this.columnFileName, "columnFileName");
            // 
            // buttonBioRemove
            // 
            resources.ApplyResources(this.buttonBioRemove, "buttonBioRemove");
            this.buttonBioRemove.Name = "buttonBioRemove";
            this.buttonBioRemove.UseVisualStyleBackColor = true;
            this.buttonBioRemove.Click += new System.EventHandler(this.buttonBioRemove_Click);
            // 
            // buttonBioBrowse
            // 
            resources.ApplyResources(this.buttonBioBrowse, "buttonBioBrowse");
            this.buttonBioBrowse.Name = "buttonBioBrowse";
            this.buttonBioBrowse.UseVisualStyleBackColor = true;
            this.buttonBioBrowse.Click += new System.EventHandler(this.buttonBioBrowse_Click);
            // 
            // checkBoxBioAutoLoad
            // 
            resources.ApplyResources(this.checkBoxBioAutoLoad, "checkBoxBioAutoLoad");
            this.checkBoxBioAutoLoad.Name = "checkBoxBioAutoLoad";
            this.checkBoxBioAutoLoad.UseVisualStyleBackColor = true;
            this.checkBoxBioAutoLoad.CheckedChanged += new System.EventHandler(this.checkBoxBioAutoLoad_CheckedChanged);
            // 
            // comboBoxDominance
            // 
            resources.ApplyResources(this.comboBoxDominance, "comboBoxDominance");
            this.comboBoxDominance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDominance.FormattingEnabled = true;
            this.comboBoxDominance.Items.AddRange(new object[] {
            resources.GetString("comboBoxDominance.Items"),
            resources.GetString("comboBoxDominance.Items1"),
            resources.GetString("comboBoxDominance.Items2"),
            resources.GetString("comboBoxDominance.Items3"),
            resources.GetString("comboBoxDominance.Items4"),
            resources.GetString("comboBoxDominance.Items5"),
            resources.GetString("comboBoxDominance.Items6"),
            resources.GetString("comboBoxDominance.Items7"),
            resources.GetString("comboBoxDominance.Items8"),
            resources.GetString("comboBoxDominance.Items9"),
            resources.GetString("comboBoxDominance.Items10"),
            resources.GetString("comboBoxDominance.Items11"),
            resources.GetString("comboBoxDominance.Items12"),
            resources.GetString("comboBoxDominance.Items13"),
            resources.GetString("comboBoxDominance.Items14"),
            resources.GetString("comboBoxDominance.Items15"),
            resources.GetString("comboBoxDominance.Items16")});
            this.comboBoxDominance.Name = "comboBoxDominance";
            // 
            // labelDominance
            // 
            resources.ApplyResources(this.labelDominance, "labelDominance");
            this.labelDominance.Name = "labelDominance";
            // 
            // comboBoxDiversity
            // 
            resources.ApplyResources(this.comboBoxDiversity, "comboBoxDiversity");
            this.comboBoxDiversity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiversity.FormattingEnabled = true;
            this.comboBoxDiversity.Name = "comboBoxDiversity";
            // 
            // labelDiversity
            // 
            resources.ApplyResources(this.labelDiversity, "labelDiversity");
            this.labelDiversity.Name = "labelDiversity";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // labelIndices
            // 
            resources.ApplyResources(this.labelIndices, "labelIndices");
            this.labelIndices.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelIndices.Name = "labelIndices";
            // 
            // tabPageOther
            // 
            this.tabPageOther.Controls.Add(this.label6);
            this.tabPageOther.Controls.Add(this.buttonProductSettings);
            this.tabPageOther.Controls.Add(this.label1);
            this.tabPageOther.Controls.Add(this.labelFish);
            this.tabPageOther.Controls.Add(this.buttonMath);
            this.tabPageOther.Controls.Add(this.buttonPlankton);
            resources.ApplyResources(this.tabPageOther, "tabPageOther");
            this.tabPageOther.Name = "tabPageOther";
            this.tabPageOther.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // buttonProductSettings
            // 
            resources.ApplyResources(this.buttonProductSettings, "buttonProductSettings");
            this.buttonProductSettings.Name = "buttonProductSettings";
            this.buttonProductSettings.UseVisualStyleBackColor = true;
            this.buttonProductSettings.Click += new System.EventHandler(this.buttonBasicSettings_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelFish
            // 
            resources.ApplyResources(this.labelFish, "labelFish");
            this.labelFish.Name = "labelFish";
            // 
            // buttonMath
            // 
            resources.ApplyResources(this.buttonMath, "buttonMath");
            this.buttonMath.Name = "buttonMath";
            this.buttonMath.UseVisualStyleBackColor = true;
            this.buttonMath.Click += new System.EventHandler(this.buttonMath_Click);
            // 
            // buttonPlankton
            // 
            resources.ApplyResources(this.buttonPlankton, "buttonPlankton");
            this.buttonPlankton.Name = "buttonPlankton";
            this.buttonPlankton.UseVisualStyleBackColor = true;
            this.buttonPlankton.Click += new System.EventHandler(this.buttonPlankton_Click);
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
            // buttonBasicSettings
            // 
            resources.ApplyResources(this.buttonBasicSettings, "buttonBasicSettings");
            this.buttonBasicSettings.Name = "buttonBasicSettings";
            this.buttonBasicSettings.UseVisualStyleBackColor = true;
            this.buttonBasicSettings.Click += new System.EventHandler(this.buttonBasicSettings_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonBasicSettings);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageTreat.ResumeLayout(false);
            this.tabPageTreat.PerformLayout();
            this.tabPageOther.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPageTreat;
        private System.Windows.Forms.Label labelIndices;
        private System.Windows.Forms.ComboBox comboBoxDiversity;
        private System.Windows.Forms.Label labelDiversity;
        private System.Windows.Forms.ComboBox comboBoxDominance;
        private System.Windows.Forms.Label labelDominance;
        private System.Windows.Forms.TabPage tabPageOther;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFish;
        private System.Windows.Forms.Button buttonMath;
        private System.Windows.Forms.Button buttonPlankton;
        private System.Windows.Forms.ListView listViewBio;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.Button buttonBioRemove;
        private System.Windows.Forms.Button buttonBioBrowse;
        private System.Windows.Forms.CheckBox checkBoxBioAutoLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonProductSettings;
        protected System.Windows.Forms.Button buttonBasicSettings;
    }
}