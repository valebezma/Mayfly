namespace Mayfly.Wild.Controls
{
    partial class SettingsControlReader
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
            this.checkBoxSpeciesExpand = new System.Windows.Forms.CheckBox();
            this.checkBoxSpeciesExpandVisualControl = new System.Windows.Forms.CheckBox();
            this.buttonClearRecent = new System.Windows.Forms.Button();
            this.labelRecent = new System.Windows.Forms.Label();
            this.numericUpDownRecentCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoLog = new System.Windows.Forms.CheckBox();
            this.labelInputFish = new System.Windows.Forms.Label();
            this.checkBoxAutoDecreaseBio = new System.Windows.Forms.CheckBox();
            this.checkBoxFixTotals = new System.Windows.Forms.CheckBox();
            this.labelCommonVars = new System.Windows.Forms.Label();
            this.checkBoxAutoIncreaseBio = new System.Windows.Forms.CheckBox();
            this.labelWaters = new System.Windows.Forms.Label();
            this.buttonBrowseWaters = new System.Windows.Forms.Button();
            this.textBoxWaters = new System.Windows.Forms.TextBox();
            this.buttonOpenWaters = new System.Windows.Forms.Button();
            this.labelRef = new System.Windows.Forms.Label();
            this.buttonBrowseSpecies = new System.Windows.Forms.Button();
            this.textBoxSpecies = new System.Windows.Forms.TextBox();
            this.labelSpecies = new System.Windows.Forms.Label();
            this.buttonOpenSpecies = new System.Windows.Forms.Button();
            this.tdClearRecent = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbRecentClear = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbRecentCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxSpeciesExpand
            // 
            this.checkBoxSpeciesExpand.AutoSize = true;
            this.checkBoxSpeciesExpand.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxSpeciesExpand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSpeciesExpand.Location = new System.Drawing.Point(48, 289);
            this.checkBoxSpeciesExpand.Name = "checkBoxSpeciesExpand";
            this.checkBoxSpeciesExpand.Size = new System.Drawing.Size(205, 17);
            this.checkBoxSpeciesExpand.TabIndex = 14;
            this.checkBoxSpeciesExpand.Text = "Automatically expand taxonomic index";
            this.checkBoxSpeciesExpand.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxSpeciesExpand.UseVisualStyleBackColor = true;
            this.checkBoxSpeciesExpand.CheckedChanged += new System.EventHandler(this.checkBoxSpeciesExpand_CheckedChanged);
            // 
            // checkBoxSpeciesExpandVisualControl
            // 
            this.checkBoxSpeciesExpandVisualControl.AutoSize = true;
            this.checkBoxSpeciesExpandVisualControl.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxSpeciesExpandVisualControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSpeciesExpandVisualControl.Location = new System.Drawing.Point(73, 312);
            this.checkBoxSpeciesExpandVisualControl.Name = "checkBoxSpeciesExpandVisualControl";
            this.checkBoxSpeciesExpandVisualControl.Size = new System.Drawing.Size(195, 17);
            this.checkBoxSpeciesExpandVisualControl.TabIndex = 15;
            this.checkBoxSpeciesExpandVisualControl.Text = "Visually confirm and edit new record";
            this.checkBoxSpeciesExpandVisualControl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxSpeciesExpandVisualControl.UseVisualStyleBackColor = true;
            // 
            // buttonClearRecent
            // 
            this.buttonClearRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearRecent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClearRecent.Location = new System.Drawing.Point(179, 260);
            this.buttonClearRecent.Name = "buttonClearRecent";
            this.buttonClearRecent.Size = new System.Drawing.Size(75, 23);
            this.buttonClearRecent.TabIndex = 12;
            this.buttonClearRecent.Text = "Clear list";
            this.buttonClearRecent.UseVisualStyleBackColor = true;
            this.buttonClearRecent.Click += new System.EventHandler(this.buttonClearRecent_Click);
            // 
            // labelRecent
            // 
            this.labelRecent.AutoSize = true;
            this.labelRecent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRecent.Location = new System.Drawing.Point(45, 264);
            this.labelRecent.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.labelRecent.Name = "labelRecent";
            this.labelRecent.Size = new System.Drawing.Size(128, 13);
            this.labelRecent.TabIndex = 11;
            this.labelRecent.Text = "Recent species list length";
            // 
            // numericUpDownRecentCount
            // 
            this.numericUpDownRecentCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRecentCount.Location = new System.Drawing.Point(260, 262);
            this.numericUpDownRecentCount.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownRecentCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownRecentCount.Name = "numericUpDownRecentCount";
            this.numericUpDownRecentCount.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownRecentCount.TabIndex = 13;
            this.numericUpDownRecentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRecentCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxAutoLog
            // 
            this.checkBoxAutoLog.AutoSize = true;
            this.checkBoxAutoLog.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxAutoLog.Location = new System.Drawing.Point(48, 239);
            this.checkBoxAutoLog.Name = "checkBoxAutoLog";
            this.checkBoxAutoLog.Size = new System.Drawing.Size(253, 17);
            this.checkBoxAutoLog.TabIndex = 10;
            this.checkBoxAutoLog.Text = "Open individuals log when adding new definition";
            this.checkBoxAutoLog.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoLog.UseVisualStyleBackColor = true;
            // 
            // labelInputFish
            // 
            this.labelInputFish.AutoSize = true;
            this.labelInputFish.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelInputFish.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelInputFish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelInputFish.Location = new System.Drawing.Point(28, 199);
            this.labelInputFish.Name = "labelInputFish";
            this.labelInputFish.Size = new System.Drawing.Size(101, 15);
            this.labelInputFish.TabIndex = 9;
            this.labelInputFish.Text = "Default Behaviour";
            // 
            // checkBoxAutoDecreaseBio
            // 
            this.checkBoxAutoDecreaseBio.AutoSize = true;
            this.checkBoxAutoDecreaseBio.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoDecreaseBio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxAutoDecreaseBio.Location = new System.Drawing.Point(68, 443);
            this.checkBoxAutoDecreaseBio.Name = "checkBoxAutoDecreaseBio";
            this.checkBoxAutoDecreaseBio.Size = new System.Drawing.Size(276, 17);
            this.checkBoxAutoDecreaseBio.TabIndex = 19;
            this.checkBoxAutoDecreaseBio.Text = "Decrease total when individual values being reduced";
            this.checkBoxAutoDecreaseBio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoDecreaseBio.UseVisualStyleBackColor = true;
            // 
            // checkBoxFixTotals
            // 
            this.checkBoxFixTotals.AutoSize = true;
            this.checkBoxFixTotals.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxFixTotals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxFixTotals.Location = new System.Drawing.Point(48, 397);
            this.checkBoxFixTotals.Name = "checkBoxFixTotals";
            this.checkBoxFixTotals.Size = new System.Drawing.Size(299, 17);
            this.checkBoxFixTotals.TabIndex = 17;
            this.checkBoxFixTotals.Text = "Calculate quantity and mass totals with data on individuals";
            this.checkBoxFixTotals.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxFixTotals.UseVisualStyleBackColor = true;
            this.checkBoxFixTotals.CheckedChanged += new System.EventHandler(this.checkBoxFixTotals_CheckedChanged);
            // 
            // labelCommonVars
            // 
            this.labelCommonVars.AutoSize = true;
            this.labelCommonVars.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCommonVars.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelCommonVars.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommonVars.Location = new System.Drawing.Point(28, 357);
            this.labelCommonVars.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelCommonVars.Name = "labelCommonVars";
            this.labelCommonVars.Size = new System.Drawing.Size(107, 15);
            this.labelCommonVars.TabIndex = 16;
            this.labelCommonVars.Text = "Common Variables";
            // 
            // checkBoxAutoIncreaseBio
            // 
            this.checkBoxAutoIncreaseBio.AutoSize = true;
            this.checkBoxAutoIncreaseBio.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoIncreaseBio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxAutoIncreaseBio.Location = new System.Drawing.Point(68, 420);
            this.checkBoxAutoIncreaseBio.Name = "checkBoxAutoIncreaseBio";
            this.checkBoxAutoIncreaseBio.Size = new System.Drawing.Size(273, 17);
            this.checkBoxAutoIncreaseBio.TabIndex = 18;
            this.checkBoxAutoIncreaseBio.Text = "Increase total when individual values sum overlaps it";
            this.checkBoxAutoIncreaseBio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoIncreaseBio.UseVisualStyleBackColor = true;
            this.checkBoxAutoIncreaseBio.CheckedChanged += new System.EventHandler(this.checkBoxAutoIncreaseBio_CheckedChanged);
            // 
            // labelWaters
            // 
            this.labelWaters.AutoSize = true;
            this.labelWaters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelWaters.Location = new System.Drawing.Point(48, 133);
            this.labelWaters.Name = "labelWaters";
            this.labelWaters.Size = new System.Drawing.Size(69, 13);
            this.labelWaters.TabIndex = 5;
            this.labelWaters.Text = "Waters index";
            // 
            // buttonBrowseWaters
            // 
            this.buttonBrowseWaters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseWaters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBrowseWaters.Location = new System.Drawing.Point(196, 151);
            this.buttonBrowseWaters.Name = "buttonBrowseWaters";
            this.buttonBrowseWaters.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseWaters.TabIndex = 7;
            this.buttonBrowseWaters.Text = "Browse...";
            this.buttonBrowseWaters.UseVisualStyleBackColor = true;
            this.buttonBrowseWaters.Click += new System.EventHandler(this.buttonBrowseWaters_Click);
            // 
            // textBoxWaters
            // 
            this.textBoxWaters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaters.Location = new System.Drawing.Point(51, 154);
            this.textBoxWaters.Name = "textBoxWaters";
            this.textBoxWaters.ReadOnly = true;
            this.textBoxWaters.Size = new System.Drawing.Size(139, 20);
            this.textBoxWaters.TabIndex = 6;
            // 
            // buttonOpenWaters
            // 
            this.buttonOpenWaters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenWaters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenWaters.Location = new System.Drawing.Point(277, 151);
            this.buttonOpenWaters.Name = "buttonOpenWaters";
            this.buttonOpenWaters.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenWaters.TabIndex = 8;
            this.buttonOpenWaters.Text = "Open";
            this.buttonOpenWaters.UseVisualStyleBackColor = true;
            this.buttonOpenWaters.Click += new System.EventHandler(this.buttonOpenWaters_Click);
            // 
            // labelRef
            // 
            this.labelRef.AutoSize = true;
            this.labelRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelRef.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelRef.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRef.Location = new System.Drawing.Point(28, 25);
            this.labelRef.Name = "labelRef";
            this.labelRef.Size = new System.Drawing.Size(64, 15);
            this.labelRef.TabIndex = 0;
            this.labelRef.Text = "References";
            // 
            // buttonBrowseSpecies
            // 
            this.buttonBrowseSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBrowseSpecies.Location = new System.Drawing.Point(196, 89);
            this.buttonBrowseSpecies.Name = "buttonBrowseSpecies";
            this.buttonBrowseSpecies.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSpecies.TabIndex = 3;
            this.buttonBrowseSpecies.Text = "Browse...";
            this.buttonBrowseSpecies.UseVisualStyleBackColor = true;
            this.buttonBrowseSpecies.Click += new System.EventHandler(this.buttonBrowseSpecies_Click);
            // 
            // textBoxSpecies
            // 
            this.textBoxSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSpecies.Location = new System.Drawing.Point(51, 92);
            this.textBoxSpecies.Name = "textBoxSpecies";
            this.textBoxSpecies.ReadOnly = true;
            this.textBoxSpecies.Size = new System.Drawing.Size(139, 20);
            this.textBoxSpecies.TabIndex = 2;
            // 
            // labelSpecies
            // 
            this.labelSpecies.AutoSize = true;
            this.labelSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSpecies.Location = new System.Drawing.Point(48, 71);
            this.labelSpecies.Name = "labelSpecies";
            this.labelSpecies.Size = new System.Drawing.Size(73, 13);
            this.labelSpecies.TabIndex = 1;
            this.labelSpecies.Text = "Species index";
            // 
            // buttonOpenSpecies
            // 
            this.buttonOpenSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenSpecies.Location = new System.Drawing.Point(277, 89);
            this.buttonOpenSpecies.Name = "buttonOpenSpecies";
            this.buttonOpenSpecies.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenSpecies.TabIndex = 4;
            this.buttonOpenSpecies.Text = "Open";
            this.buttonOpenSpecies.UseVisualStyleBackColor = true;
            this.buttonOpenSpecies.Click += new System.EventHandler(this.buttonOpenSpecies_Click);
            // 
            // tdClearRecent
            // 
            this.tdClearRecent.Buttons.Add(this.tdbRecentClear);
            this.tdClearRecent.Buttons.Add(this.tdbRecentCancel);
            this.tdClearRecent.MainInstruction = "Clear recent species?";
            // 
            // tdbRecentClear
            // 
            this.tdbRecentClear.Text = "Clear";
            // 
            // tdbRecentCancel
            // 
            this.tdbRecentCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // SettingsControlReader
            // 
            this.Controls.Add(this.labelWaters);
            this.Controls.Add(this.buttonBrowseWaters);
            this.Controls.Add(this.textBoxWaters);
            this.Controls.Add(this.buttonOpenWaters);
            this.Controls.Add(this.labelRef);
            this.Controls.Add(this.buttonBrowseSpecies);
            this.Controls.Add(this.textBoxSpecies);
            this.Controls.Add(this.labelSpecies);
            this.Controls.Add(this.buttonOpenSpecies);
            this.Controls.Add(this.checkBoxAutoDecreaseBio);
            this.Controls.Add(this.checkBoxFixTotals);
            this.Controls.Add(this.labelCommonVars);
            this.Controls.Add(this.checkBoxAutoIncreaseBio);
            this.Controls.Add(this.checkBoxSpeciesExpand);
            this.Controls.Add(this.checkBoxSpeciesExpandVisualControl);
            this.Controls.Add(this.buttonClearRecent);
            this.Controls.Add(this.labelRecent);
            this.Controls.Add(this.numericUpDownRecentCount);
            this.Controls.Add(this.checkBoxAutoLog);
            this.Controls.Add(this.labelInputFish);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "SettingsControlReader";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxSpeciesExpand;
        private System.Windows.Forms.CheckBox checkBoxSpeciesExpandVisualControl;
        private System.Windows.Forms.Button buttonClearRecent;
        private System.Windows.Forms.Label labelRecent;
        private System.Windows.Forms.NumericUpDown numericUpDownRecentCount;
        private System.Windows.Forms.CheckBox checkBoxAutoLog;
        private System.Windows.Forms.Label labelInputFish;
        private System.Windows.Forms.CheckBox checkBoxAutoDecreaseBio;
        private System.Windows.Forms.CheckBox checkBoxFixTotals;
        private System.Windows.Forms.Label labelCommonVars;
        private System.Windows.Forms.CheckBox checkBoxAutoIncreaseBio;
        private System.Windows.Forms.Label labelWaters;
        private System.Windows.Forms.Button buttonBrowseWaters;
        private System.Windows.Forms.TextBox textBoxWaters;
        private System.Windows.Forms.Button buttonOpenWaters;
        private System.Windows.Forms.Label labelRef;
        private System.Windows.Forms.Button buttonBrowseSpecies;
        private System.Windows.Forms.TextBox textBoxSpecies;
        private System.Windows.Forms.Label labelSpecies;
        private System.Windows.Forms.Button buttonOpenSpecies;
        protected TaskDialogs.TaskDialog tdClearRecent;
        private TaskDialogs.TaskDialogButton tdbRecentClear;
        private TaskDialogs.TaskDialogButton tdbRecentCancel;
    }
}
