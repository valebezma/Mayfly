namespace Mayfly.Wild.Controls
{
    partial class SettingsControlIndices
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
            this.checkBoxSpeciesExpand = new System.Windows.Forms.CheckBox();
            this.checkBoxSpeciesExpandVisualControl = new System.Windows.Forms.CheckBox();
            this.buttonClearRecent = new System.Windows.Forms.Button();
            this.labelRecent = new System.Windows.Forms.Label();
            this.numericUpDownRecentCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).BeginInit();
            this.SuspendLayout();
            // 
            // labelWaters
            // 
            this.labelWaters.AutoSize = true;
            this.labelWaters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelWaters.Location = new System.Drawing.Point(48, 40);
            this.labelWaters.Name = "labelWaters";
            this.labelWaters.Size = new System.Drawing.Size(69, 13);
            this.labelWaters.TabIndex = 1;
            this.labelWaters.Text = "Waters index";
            // 
            // buttonBrowseWaters
            // 
            this.buttonBrowseWaters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseWaters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBrowseWaters.Location = new System.Drawing.Point(221, 59);
            this.buttonBrowseWaters.Name = "buttonBrowseWaters";
            this.buttonBrowseWaters.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseWaters.TabIndex = 3;
            this.buttonBrowseWaters.Text = "Browse...";
            this.buttonBrowseWaters.UseVisualStyleBackColor = true;
            this.buttonBrowseWaters.Click += new System.EventHandler(this.buttonBrowseWaters_Click);
            // 
            // textBoxWaters
            // 
            this.textBoxWaters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaters.Location = new System.Drawing.Point(51, 61);
            this.textBoxWaters.Name = "textBoxWaters";
            this.textBoxWaters.ReadOnly = true;
            this.textBoxWaters.Size = new System.Drawing.Size(164, 20);
            this.textBoxWaters.TabIndex = 2;
            // 
            // buttonOpenWaters
            // 
            this.buttonOpenWaters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenWaters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenWaters.Location = new System.Drawing.Point(302, 59);
            this.buttonOpenWaters.Name = "buttonOpenWaters";
            this.buttonOpenWaters.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenWaters.TabIndex = 4;
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
            this.labelRef.Location = new System.Drawing.Point(28, 0);
            this.labelRef.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.labelRef.Name = "labelRef";
            this.labelRef.Size = new System.Drawing.Size(44, 15);
            this.labelRef.TabIndex = 0;
            this.labelRef.Text = "Indices";
            // 
            // buttonBrowseSpecies
            // 
            this.buttonBrowseSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBrowseSpecies.Location = new System.Drawing.Point(221, 129);
            this.buttonBrowseSpecies.Name = "buttonBrowseSpecies";
            this.buttonBrowseSpecies.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseSpecies.TabIndex = 7;
            this.buttonBrowseSpecies.Text = "Browse...";
            this.buttonBrowseSpecies.UseVisualStyleBackColor = true;
            this.buttonBrowseSpecies.Click += new System.EventHandler(this.buttonBrowseSpecies_Click);
            // 
            // textBoxSpecies
            // 
            this.textBoxSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSpecies.Location = new System.Drawing.Point(51, 131);
            this.textBoxSpecies.Name = "textBoxSpecies";
            this.textBoxSpecies.ReadOnly = true;
            this.textBoxSpecies.Size = new System.Drawing.Size(164, 20);
            this.textBoxSpecies.TabIndex = 6;
            // 
            // labelSpecies
            // 
            this.labelSpecies.AutoSize = true;
            this.labelSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSpecies.Location = new System.Drawing.Point(48, 110);
            this.labelSpecies.Name = "labelSpecies";
            this.labelSpecies.Size = new System.Drawing.Size(87, 13);
            this.labelSpecies.TabIndex = 5;
            this.labelSpecies.Text = "Taxonomic index";
            // 
            // buttonOpenSpecies
            // 
            this.buttonOpenSpecies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOpenSpecies.Location = new System.Drawing.Point(302, 129);
            this.buttonOpenSpecies.Name = "buttonOpenSpecies";
            this.buttonOpenSpecies.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenSpecies.TabIndex = 8;
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
            this.tdbRecentClear.Default = true;
            this.tdbRecentClear.Text = "Clear";
            // 
            // tdbRecentCancel
            // 
            this.tdbRecentCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // checkBoxSpeciesExpand
            // 
            this.checkBoxSpeciesExpand.AutoSize = true;
            this.checkBoxSpeciesExpand.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxSpeciesExpand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSpeciesExpand.Location = new System.Drawing.Point(56, 184);
            this.checkBoxSpeciesExpand.Name = "checkBoxSpeciesExpand";
            this.checkBoxSpeciesExpand.Size = new System.Drawing.Size(205, 17);
            this.checkBoxSpeciesExpand.TabIndex = 12;
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
            this.checkBoxSpeciesExpandVisualControl.Location = new System.Drawing.Point(81, 207);
            this.checkBoxSpeciesExpandVisualControl.Name = "checkBoxSpeciesExpandVisualControl";
            this.checkBoxSpeciesExpandVisualControl.Size = new System.Drawing.Size(195, 17);
            this.checkBoxSpeciesExpandVisualControl.TabIndex = 13;
            this.checkBoxSpeciesExpandVisualControl.Text = "Visually confirm and edit new record";
            this.checkBoxSpeciesExpandVisualControl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxSpeciesExpandVisualControl.UseVisualStyleBackColor = true;
            // 
            // buttonClearRecent
            // 
            this.buttonClearRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearRecent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClearRecent.Location = new System.Drawing.Point(221, 155);
            this.buttonClearRecent.Name = "buttonClearRecent";
            this.buttonClearRecent.Size = new System.Drawing.Size(75, 23);
            this.buttonClearRecent.TabIndex = 10;
            this.buttonClearRecent.Text = "Clear list";
            this.buttonClearRecent.UseVisualStyleBackColor = true;
            this.buttonClearRecent.Click += new System.EventHandler(this.buttonClearRecent_Click);
            // 
            // labelRecent
            // 
            this.labelRecent.AutoSize = true;
            this.labelRecent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRecent.Location = new System.Drawing.Point(53, 160);
            this.labelRecent.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.labelRecent.Name = "labelRecent";
            this.labelRecent.Size = new System.Drawing.Size(128, 13);
            this.labelRecent.TabIndex = 9;
            this.labelRecent.Text = "Recent species list length";
            // 
            // numericUpDownRecentCount
            // 
            this.numericUpDownRecentCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRecentCount.Location = new System.Drawing.Point(302, 158);
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
            this.numericUpDownRecentCount.TabIndex = 11;
            this.numericUpDownRecentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownRecentCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // SettingsControlIndices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxSpeciesExpand);
            this.Controls.Add(this.checkBoxSpeciesExpandVisualControl);
            this.Controls.Add(this.buttonClearRecent);
            this.Controls.Add(this.labelRecent);
            this.Controls.Add(this.numericUpDownRecentCount);
            this.Controls.Add(this.labelWaters);
            this.Controls.Add(this.buttonBrowseWaters);
            this.Controls.Add(this.textBoxWaters);
            this.Controls.Add(this.buttonOpenWaters);
            this.Controls.Add(this.labelRef);
            this.Controls.Add(this.buttonBrowseSpecies);
            this.Controls.Add(this.textBoxSpecies);
            this.Controls.Add(this.labelSpecies);
            this.Controls.Add(this.buttonOpenSpecies);
            this.Group = "Reader";
            this.Name = "SettingsControlIndices";
            this.Section = "Indices";
            this.Size = new System.Drawing.Size(400, 241);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.CheckBox checkBoxSpeciesExpand;
        private System.Windows.Forms.CheckBox checkBoxSpeciesExpandVisualControl;
        private System.Windows.Forms.Button buttonClearRecent;
        private System.Windows.Forms.Label labelRecent;
        private System.Windows.Forms.NumericUpDown numericUpDownRecentCount;
        private TaskDialogs.TaskDialogButton tdbRecentClear;
        private TaskDialogs.TaskDialogButton tdbRecentCancel;
    }
}
