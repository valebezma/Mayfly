namespace Mayfly.Prospect
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
            this.tabPageReferences = new System.Windows.Forms.TabPage();
            this.numericUpDownUpdateFrequency = new System.Windows.Forms.NumericUpDown();
            this.labelUpdatedTip = new System.Windows.Forms.Label();
            this.labelFreq = new System.Windows.Forms.Label();
            this.labelWaters = new System.Windows.Forms.Label();
            this.buttonBrowseWaters = new System.Windows.Forms.Button();
            this.textBoxWaters = new System.Windows.Forms.TextBox();
            this.buttonOpenWaters = new System.Windows.Forms.Button();
            this.labelRef = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonBrowseCards = new System.Windows.Forms.Button();
            this.textBoxCards = new System.Windows.Forms.TextBox();
            this.labelData = new System.Windows.Forms.Label();
            this.tabPageSpecies = new System.Windows.Forms.TabPage();
            this.labelSpeciesPaths = new System.Windows.Forms.Label();
            this.buttonBrowseFsh = new System.Windows.Forms.Button();
            this.buttonBrowseBen = new System.Windows.Forms.Button();
            this.buttonBrowsePlk = new System.Windows.Forms.Button();
            this.textBoxFsh = new System.Windows.Forms.TextBox();
            this.textBoxBen = new System.Windows.Forms.TextBox();
            this.textBoxPlk = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpenFsh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOpenBen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenPlk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControlSettings.SuspendLayout();
            this.tabPageReferences.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateFrequency)).BeginInit();
            this.tabPageSpecies.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageReferences);
            this.tabControlSettings.Controls.Add(this.tabPageSpecies);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageReferences
            // 
            this.tabPageReferences.Controls.Add(this.numericUpDownUpdateFrequency);
            this.tabPageReferences.Controls.Add(this.labelUpdatedTip);
            this.tabPageReferences.Controls.Add(this.labelFreq);
            this.tabPageReferences.Controls.Add(this.labelWaters);
            this.tabPageReferences.Controls.Add(this.buttonBrowseWaters);
            this.tabPageReferences.Controls.Add(this.textBoxWaters);
            this.tabPageReferences.Controls.Add(this.buttonOpenWaters);
            this.tabPageReferences.Controls.Add(this.labelRef);
            this.tabPageReferences.Controls.Add(this.buttonUpdate);
            this.tabPageReferences.Controls.Add(this.buttonBrowseCards);
            this.tabPageReferences.Controls.Add(this.textBoxCards);
            this.tabPageReferences.Controls.Add(this.labelData);
            resources.ApplyResources(this.tabPageReferences, "tabPageReferences");
            this.tabPageReferences.Name = "tabPageReferences";
            this.tabPageReferences.UseVisualStyleBackColor = true;
            // 
            // numericUpDownUpdateFrequency
            // 
            resources.ApplyResources(this.numericUpDownUpdateFrequency, "numericUpDownUpdateFrequency");
            this.numericUpDownUpdateFrequency.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownUpdateFrequency.Name = "numericUpDownUpdateFrequency";
            this.numericUpDownUpdateFrequency.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // labelUpdatedTip
            // 
            resources.ApplyResources(this.labelUpdatedTip, "labelUpdatedTip");
            this.labelUpdatedTip.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelUpdatedTip.Name = "labelUpdatedTip";
            // 
            // labelFreq
            // 
            resources.ApplyResources(this.labelFreq, "labelFreq");
            this.labelFreq.Name = "labelFreq";
            // 
            // labelWaters
            // 
            resources.ApplyResources(this.labelWaters, "labelWaters");
            this.labelWaters.Name = "labelWaters";
            // 
            // buttonBrowseWaters
            // 
            resources.ApplyResources(this.buttonBrowseWaters, "buttonBrowseWaters");
            this.buttonBrowseWaters.Name = "buttonBrowseWaters";
            this.buttonBrowseWaters.UseVisualStyleBackColor = true;
            this.buttonBrowseWaters.Click += new System.EventHandler(this.buttonBrowseWaters_Click);
            // 
            // textBoxWaters
            // 
            resources.ApplyResources(this.textBoxWaters, "textBoxWaters");
            this.textBoxWaters.Name = "textBoxWaters";
            this.textBoxWaters.ReadOnly = true;
            // 
            // buttonOpenWaters
            // 
            resources.ApplyResources(this.buttonOpenWaters, "buttonOpenWaters");
            this.buttonOpenWaters.Name = "buttonOpenWaters";
            this.buttonOpenWaters.UseVisualStyleBackColor = true;
            this.buttonOpenWaters.Click += new System.EventHandler(this.buttonOpenWaters_Click);
            // 
            // labelRef
            // 
            resources.ApplyResources(this.labelRef, "labelRef");
            this.labelRef.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelRef.Name = "labelRef";
            // 
            // buttonUpdate
            // 
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonBrowseCards
            // 
            resources.ApplyResources(this.buttonBrowseCards, "buttonBrowseCards");
            this.buttonBrowseCards.Name = "buttonBrowseCards";
            this.buttonBrowseCards.UseVisualStyleBackColor = true;
            this.buttonBrowseCards.Click += new System.EventHandler(this.buttonBrowseCards_Click);
            // 
            // textBoxCards
            // 
            resources.ApplyResources(this.textBoxCards, "textBoxCards");
            this.textBoxCards.Name = "textBoxCards";
            this.textBoxCards.ReadOnly = true;
            // 
            // labelData
            // 
            resources.ApplyResources(this.labelData, "labelData");
            this.labelData.Name = "labelData";
            // 
            // tabPageSpecies
            // 
            this.tabPageSpecies.Controls.Add(this.labelSpeciesPaths);
            this.tabPageSpecies.Controls.Add(this.buttonBrowseFsh);
            this.tabPageSpecies.Controls.Add(this.buttonBrowseBen);
            this.tabPageSpecies.Controls.Add(this.buttonBrowsePlk);
            this.tabPageSpecies.Controls.Add(this.textBoxFsh);
            this.tabPageSpecies.Controls.Add(this.textBoxBen);
            this.tabPageSpecies.Controls.Add(this.textBoxPlk);
            this.tabPageSpecies.Controls.Add(this.label3);
            this.tabPageSpecies.Controls.Add(this.buttonOpenFsh);
            this.tabPageSpecies.Controls.Add(this.label2);
            this.tabPageSpecies.Controls.Add(this.buttonOpenBen);
            this.tabPageSpecies.Controls.Add(this.label1);
            this.tabPageSpecies.Controls.Add(this.buttonOpenPlk);
            resources.ApplyResources(this.tabPageSpecies, "tabPageSpecies");
            this.tabPageSpecies.Name = "tabPageSpecies";
            this.tabPageSpecies.UseVisualStyleBackColor = true;
            // 
            // labelSpeciesPaths
            // 
            resources.ApplyResources(this.labelSpeciesPaths, "labelSpeciesPaths");
            this.labelSpeciesPaths.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelSpeciesPaths.Name = "labelSpeciesPaths";
            // 
            // buttonBrowseFsh
            // 
            resources.ApplyResources(this.buttonBrowseFsh, "buttonBrowseFsh");
            this.buttonBrowseFsh.Name = "buttonBrowseFsh";
            this.buttonBrowseFsh.UseVisualStyleBackColor = true;
            this.buttonBrowseFsh.Click += new System.EventHandler(this.buttonBrowseFsh_Click);
            // 
            // buttonBrowseBen
            // 
            resources.ApplyResources(this.buttonBrowseBen, "buttonBrowseBen");
            this.buttonBrowseBen.Name = "buttonBrowseBen";
            this.buttonBrowseBen.UseVisualStyleBackColor = true;
            this.buttonBrowseBen.Click += new System.EventHandler(this.buttonBrowseBen_Click);
            // 
            // buttonBrowsePlk
            // 
            resources.ApplyResources(this.buttonBrowsePlk, "buttonBrowsePlk");
            this.buttonBrowsePlk.Name = "buttonBrowsePlk";
            this.buttonBrowsePlk.UseVisualStyleBackColor = true;
            this.buttonBrowsePlk.Click += new System.EventHandler(this.buttonBrowsePlk_Click);
            // 
            // textBoxFsh
            // 
            resources.ApplyResources(this.textBoxFsh, "textBoxFsh");
            this.textBoxFsh.Name = "textBoxFsh";
            this.textBoxFsh.ReadOnly = true;
            // 
            // textBoxBen
            // 
            resources.ApplyResources(this.textBoxBen, "textBoxBen");
            this.textBoxBen.Name = "textBoxBen";
            this.textBoxBen.ReadOnly = true;
            // 
            // textBoxPlk
            // 
            resources.ApplyResources(this.textBoxPlk, "textBoxPlk");
            this.textBoxPlk.Name = "textBoxPlk";
            this.textBoxPlk.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // buttonOpenFsh
            // 
            resources.ApplyResources(this.buttonOpenFsh, "buttonOpenFsh");
            this.buttonOpenFsh.Name = "buttonOpenFsh";
            this.buttonOpenFsh.UseVisualStyleBackColor = true;
            this.buttonOpenFsh.Click += new System.EventHandler(this.buttonOpenFsh_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonOpenBen
            // 
            resources.ApplyResources(this.buttonOpenBen, "buttonOpenBen");
            this.buttonOpenBen.Name = "buttonOpenBen";
            this.buttonOpenBen.UseVisualStyleBackColor = true;
            this.buttonOpenBen.Click += new System.EventHandler(this.buttonOpenBen_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // buttonOpenPlk
            // 
            resources.ApplyResources(this.buttonOpenPlk, "buttonOpenPlk");
            this.buttonOpenPlk.Name = "buttonOpenPlk";
            this.buttonOpenPlk.UseVisualStyleBackColor = true;
            this.buttonOpenPlk.Click += new System.EventHandler(this.buttonOpenPlk_Click);
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
            this.tabPageReferences.ResumeLayout(false);
            this.tabPageReferences.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUpdateFrequency)).EndInit();
            this.tabPageSpecies.ResumeLayout(false);
            this.tabPageSpecies.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBoxCards;
        private System.Windows.Forms.TextBox textBoxWaters;
        private System.Windows.Forms.TabPage tabPageReferences;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelRef;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label labelWaters;
        private System.Windows.Forms.Button buttonOpenWaters;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonBrowseCards;
        private System.Windows.Forms.Button buttonBrowseWaters;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateFrequency;
        private System.Windows.Forms.Label labelFreq;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label labelUpdatedTip;
        private System.Windows.Forms.TabPage tabPageSpecies;
        private System.Windows.Forms.Label labelSpeciesPaths;
        private System.Windows.Forms.Button buttonBrowsePlk;
        private System.Windows.Forms.TextBox textBoxPlk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpenPlk;
        private System.Windows.Forms.Button buttonBrowseFsh;
        private System.Windows.Forms.Button buttonBrowseBen;
        private System.Windows.Forms.TextBox textBoxFsh;
        private System.Windows.Forms.TextBox textBoxBen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOpenFsh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOpenBen;
    }
}