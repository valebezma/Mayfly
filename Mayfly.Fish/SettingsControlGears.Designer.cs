namespace Mayfly.Fish
{
    partial class SettingsControlGears
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spreadSheetOpening = new Mayfly.Controls.SpreadSheet();
            this.columnOpeningGear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOpeningValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelOpeningDefault = new System.Windows.Forms.Label();
            this.numericUpDownOpeningDefault = new System.Windows.Forms.NumericUpDown();
            this.labelActive = new System.Windows.Forms.Label();
            this.labelActiveInstruction = new System.Windows.Forms.Label();
            this.labelStationary = new System.Windows.Forms.Label();
            this.labelExposure = new System.Windows.Forms.Label();
            this.labelStationaryInstruction = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.numericUpDownStdSoak = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStdHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStdLength = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetOpening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpeningDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdSoak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdLength)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadSheetOpening
            // 
            this.spreadSheetOpening.AllowStringSuggection = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.spreadSheetOpening.AllowUserToAddRows = true;
            this.spreadSheetOpening.AllowUserToDeleteRows = true;
            this.spreadSheetOpening.AllowUserToResizeColumns = false;
            this.spreadSheetOpening.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetOpening.AutoClearEmptyRows = true;
            this.spreadSheetOpening.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnOpeningGear,
            this.columnOpeningValue});
            this.spreadSheetOpening.DefaultDecimalPlaces = 3;
            this.spreadSheetOpening.Location = new System.Drawing.Point(45, 312);
            this.spreadSheetOpening.Margin = new System.Windows.Forms.Padding(45, 10, 45, 0);
            this.spreadSheetOpening.Name = "spreadSheetOpening";
            this.spreadSheetOpening.RowHeadersWidth = 25;
            this.spreadSheetOpening.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.spreadSheetOpening.Size = new System.Drawing.Size(310, 143);
            this.spreadSheetOpening.StatusFormat = null;
            this.spreadSheetOpening.TabIndex = 36;
            // 
            // columnOpeningGear
            // 
            this.columnOpeningGear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnOpeningGear.DefaultCellStyle = dataGridViewCellStyle3;
            this.columnOpeningGear.HeaderText = "Gear";
            this.columnOpeningGear.Name = "columnOpeningGear";
            this.columnOpeningGear.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnOpeningValue
            // 
            this.columnOpeningValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Format = "P0";
            this.columnOpeningValue.DefaultCellStyle = dataGridViewCellStyle4;
            this.columnOpeningValue.HeaderText = "Opening";
            this.columnOpeningValue.Name = "columnOpeningValue";
            this.columnOpeningValue.Width = 75;
            // 
            // labelOpeningDefault
            // 
            this.labelOpeningDefault.AutoSize = true;
            this.labelOpeningDefault.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelOpeningDefault.Location = new System.Drawing.Point(45, 281);
            this.labelOpeningDefault.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.labelOpeningDefault.Name = "labelOpeningDefault";
            this.labelOpeningDefault.Size = new System.Drawing.Size(125, 13);
            this.labelOpeningDefault.TabIndex = 34;
            this.labelOpeningDefault.Text = "Default opening value, %";
            // 
            // numericUpDownOpeningDefault
            // 
            this.numericUpDownOpeningDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownOpeningDefault.Location = new System.Drawing.Point(280, 279);
            this.numericUpDownOpeningDefault.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownOpeningDefault.Name = "numericUpDownOpeningDefault";
            this.numericUpDownOpeningDefault.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownOpeningDefault.TabIndex = 35;
            this.numericUpDownOpeningDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownOpeningDefault.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelActive
            // 
            this.labelActive.AutoSize = true;
            this.labelActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelActive.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelActive.Location = new System.Drawing.Point(25, 198);
            this.labelActive.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.labelActive.Name = "labelActive";
            this.labelActive.Size = new System.Drawing.Size(134, 15);
            this.labelActive.TabIndex = 32;
            this.labelActive.Text = "Active Gears Parameters";
            // 
            // labelActiveInstruction
            // 
            this.labelActiveInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelActiveInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelActiveInstruction.Location = new System.Drawing.Point(45, 238);
            this.labelActiveInstruction.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
            this.labelActiveInstruction.Name = "labelActiveInstruction";
            this.labelActiveInstruction.Size = new System.Drawing.Size(310, 26);
            this.labelActiveInstruction.TabIndex = 33;
            this.labelActiveInstruction.Text = "Default opening is percentage of gear length which is actual measure between its " +
    "ends.";
            // 
            // labelStationary
            // 
            this.labelStationary.AutoSize = true;
            this.labelStationary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStationary.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStationary.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelStationary.Location = new System.Drawing.Point(28, 25);
            this.labelStationary.Name = "labelStationary";
            this.labelStationary.Size = new System.Drawing.Size(154, 15);
            this.labelStationary.TabIndex = 37;
            this.labelStationary.Text = "Stationary Gears Parameters";
            // 
            // labelExposure
            // 
            this.labelExposure.AutoSize = true;
            this.labelExposure.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelExposure.Location = new System.Drawing.Point(48, 160);
            this.labelExposure.Name = "labelExposure";
            this.labelExposure.Size = new System.Drawing.Size(162, 13);
            this.labelExposure.TabIndex = 43;
            this.labelExposure.Text = "Gillnet standard exposition, hours";
            // 
            // labelStationaryInstruction
            // 
            this.labelStationaryInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStationaryInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelStationaryInstruction.Location = new System.Drawing.Point(48, 65);
            this.labelStationaryInstruction.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
            this.labelStationaryInstruction.Name = "labelStationaryInstruction";
            this.labelStationaryInstruction.Size = new System.Drawing.Size(310, 26);
            this.labelStationaryInstruction.TabIndex = 38;
            this.labelStationaryInstruction.Text = "Parameters of standard gillnet effort is applied to calculate gillnet efforts for" +
    " report, stock estimation or composition estimation.";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelHeight.Location = new System.Drawing.Point(48, 134);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(126, 13);
            this.labelHeight.TabIndex = 41;
            this.labelHeight.Text = "Gillnet standard height. m";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelLength.Location = new System.Drawing.Point(48, 108);
            this.labelLength.Margin = new System.Windows.Forms.Padding(45, 15, 0, 0);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(126, 13);
            this.labelLength.TabIndex = 39;
            this.labelLength.Text = "Gillnet standard length, m";
            // 
            // numericUpDownStdSoak
            // 
            this.numericUpDownStdSoak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownStdSoak.Location = new System.Drawing.Point(283, 158);
            this.numericUpDownStdSoak.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.numericUpDownStdSoak.Name = "numericUpDownStdSoak";
            this.numericUpDownStdSoak.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownStdSoak.TabIndex = 44;
            this.numericUpDownStdSoak.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownStdHeight
            // 
            this.numericUpDownStdHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownStdHeight.DecimalPlaces = 2;
            this.numericUpDownStdHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownStdHeight.Location = new System.Drawing.Point(283, 132);
            this.numericUpDownStdHeight.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStdHeight.Name = "numericUpDownStdHeight";
            this.numericUpDownStdHeight.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownStdHeight.TabIndex = 42;
            this.numericUpDownStdHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownStdLength
            // 
            this.numericUpDownStdLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownStdLength.DecimalPlaces = 2;
            this.numericUpDownStdLength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownStdLength.Location = new System.Drawing.Point(283, 106);
            this.numericUpDownStdLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStdLength.Name = "numericUpDownStdLength";
            this.numericUpDownStdLength.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownStdLength.TabIndex = 40;
            this.numericUpDownStdLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SettingsControlGears
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelStationary);
            this.Controls.Add(this.labelExposure);
            this.Controls.Add(this.labelStationaryInstruction);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.numericUpDownStdSoak);
            this.Controls.Add(this.numericUpDownStdHeight);
            this.Controls.Add(this.numericUpDownStdLength);
            this.Controls.Add(this.spreadSheetOpening);
            this.Controls.Add(this.labelOpeningDefault);
            this.Controls.Add(this.numericUpDownOpeningDefault);
            this.Controls.Add(this.labelActive);
            this.Controls.Add(this.labelActiveInstruction);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "SettingsControlGears";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetOpening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpeningDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdSoak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SpreadSheet spreadSheetOpening;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOpeningGear;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOpeningValue;
        private System.Windows.Forms.Label labelOpeningDefault;
        private System.Windows.Forms.NumericUpDown numericUpDownOpeningDefault;
        private System.Windows.Forms.Label labelActive;
        private System.Windows.Forms.Label labelActiveInstruction;
        private System.Windows.Forms.Label labelStationary;
        private System.Windows.Forms.Label labelExposure;
        private System.Windows.Forms.Label labelStationaryInstruction;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.NumericUpDown numericUpDownStdSoak;
        private System.Windows.Forms.NumericUpDown numericUpDownStdHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownStdLength;
    }
}
