namespace Mayfly.Fish.Explorer
{
    partial class SettingsControlCatchability
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spreadSheetCatchability = new Mayfly.Controls.SpreadSheet();
            this.columnCatchabilitySpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCatchabilityValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelGear = new System.Windows.Forms.Label();
            this.comboBoxGear = new System.Windows.Forms.ComboBox();
            this.numericUpDownCatchabilityDefault = new System.Windows.Forms.NumericUpDown();
            this.labelCatchabilityDefault = new System.Windows.Forms.Label();
            this.labelCatchabilityTitle = new System.Windows.Forms.Label();
            this.labelCatchabilityInstruction = new System.Windows.Forms.Label();
            this.speciesSelectorCatchability = new Mayfly.Species.TaxonProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatchability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCatchabilityDefault)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadSheetCatchability
            // 
            this.spreadSheetCatchability.AllowUserToAddRows = true;
            this.spreadSheetCatchability.AllowUserToDeleteRows = true;
            this.spreadSheetCatchability.AllowUserToResizeColumns = false;
            this.spreadSheetCatchability.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetCatchability.AutoClearEmptyRows = true;
            this.spreadSheetCatchability.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCatchabilitySpecies,
            this.columnCatchabilityValue});
            this.spreadSheetCatchability.DefaultDecimalPlaces = 3;
            this.spreadSheetCatchability.Location = new System.Drawing.Point(45, 165);
            this.spreadSheetCatchability.Margin = new System.Windows.Forms.Padding(45, 10, 45, 0);
            this.spreadSheetCatchability.Name = "spreadSheetCatchability";
            this.spreadSheetCatchability.RowHeadersWidth = 25;
            this.spreadSheetCatchability.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.spreadSheetCatchability.Size = new System.Drawing.Size(310, 140);
            this.spreadSheetCatchability.StatusFormat = null;
            this.spreadSheetCatchability.TabIndex = 6;
            this.spreadSheetCatchability.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCatchability_CellEndEdit);
            // 
            // columnCatchabilitySpecies
            // 
            this.columnCatchabilitySpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = null;
            this.columnCatchabilitySpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnCatchabilitySpecies.HeaderText = "Species";
            this.columnCatchabilitySpecies.Name = "columnCatchabilitySpecies";
            // 
            // columnCatchabilityValue
            // 
            this.columnCatchabilityValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.columnCatchabilityValue.HeaderText = "Q";
            this.columnCatchabilityValue.Name = "columnCatchabilityValue";
            this.columnCatchabilityValue.Width = 75;
            // 
            // labelGear
            // 
            this.labelGear.AutoSize = true;
            this.labelGear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelGear.Location = new System.Drawing.Point(45, 135);
            this.labelGear.Name = "labelGear";
            this.labelGear.Size = new System.Drawing.Size(53, 13);
            this.labelGear.TabIndex = 4;
            this.labelGear.Text = "Gear type";
            // 
            // comboBoxGear
            // 
            this.comboBoxGear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxGear.DisplayMember = "Sampler";
            this.comboBoxGear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGear.FormattingEnabled = true;
            this.comboBoxGear.Location = new System.Drawing.Point(155, 132);
            this.comboBoxGear.Name = "comboBoxGear";
            this.comboBoxGear.Size = new System.Drawing.Size(200, 21);
            this.comboBoxGear.Sorted = true;
            this.comboBoxGear.TabIndex = 5;
            this.comboBoxGear.SelectedIndexChanged += new System.EventHandler(this.comboBoxGear_SelectedIndexChanged);
            // 
            // numericUpDownCatchabilityDefault
            // 
            this.numericUpDownCatchabilityDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownCatchabilityDefault.DecimalPlaces = 3;
            this.numericUpDownCatchabilityDefault.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCatchabilityDefault.Location = new System.Drawing.Point(280, 106);
            this.numericUpDownCatchabilityDefault.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCatchabilityDefault.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownCatchabilityDefault.Name = "numericUpDownCatchabilityDefault";
            this.numericUpDownCatchabilityDefault.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownCatchabilityDefault.TabIndex = 3;
            this.numericUpDownCatchabilityDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCatchabilityDefault.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // labelCatchabilityDefault
            // 
            this.labelCatchabilityDefault.AutoSize = true;
            this.labelCatchabilityDefault.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCatchabilityDefault.Location = new System.Drawing.Point(45, 108);
            this.labelCatchabilityDefault.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.labelCatchabilityDefault.Name = "labelCatchabilityDefault";
            this.labelCatchabilityDefault.Size = new System.Drawing.Size(97, 13);
            this.labelCatchabilityDefault.TabIndex = 2;
            this.labelCatchabilityDefault.Text = "Default catchability";
            // 
            // labelCatchabilityTitle
            // 
            this.labelCatchabilityTitle.AutoSize = true;
            this.labelCatchabilityTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCatchabilityTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelCatchabilityTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCatchabilityTitle.Location = new System.Drawing.Point(25, 25);
            this.labelCatchabilityTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 25);
            this.labelCatchabilityTitle.Name = "labelCatchabilityTitle";
            this.labelCatchabilityTitle.Size = new System.Drawing.Size(132, 15);
            this.labelCatchabilityTitle.TabIndex = 0;
            this.labelCatchabilityTitle.Text = "Catchability Parameters";
            // 
            // labelCatchabilityInstruction
            // 
            this.labelCatchabilityInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCatchabilityInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCatchabilityInstruction.Location = new System.Drawing.Point(45, 65);
            this.labelCatchabilityInstruction.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
            this.labelCatchabilityInstruction.Name = "labelCatchabilityInstruction";
            this.labelCatchabilityInstruction.Size = new System.Drawing.Size(310, 26);
            this.labelCatchabilityInstruction.TabIndex = 1;
            this.labelCatchabilityInstruction.Text = "Default values of catchability is applied in stock estimation when there is no sp" +
    "ecified value for species or gear.";
            // 
            // speciesSelectorCatchability
            // 
            this.speciesSelectorCatchability.CheckDuplicates = false;
            this.speciesSelectorCatchability.ColumnName = "columnCatchabilitySpecies";
            this.speciesSelectorCatchability.Grid = this.spreadSheetCatchability;
            this.speciesSelectorCatchability.RecentListCount = 0;
            // 
            // SettingsControlCatchability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spreadSheetCatchability);
            this.Controls.Add(this.labelGear);
            this.Controls.Add(this.comboBoxGear);
            this.Controls.Add(this.numericUpDownCatchabilityDefault);
            this.Controls.Add(this.labelCatchabilityDefault);
            this.Controls.Add(this.labelCatchabilityTitle);
            this.Controls.Add(this.labelCatchabilityInstruction);
            this.MinimumSize = new System.Drawing.Size(400, 350);
            this.Name = "SettingsControlCatchability";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 350);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatchability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCatchabilityDefault)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SpreadSheet spreadSheetCatchability;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCatchabilitySpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCatchabilityValue;
        private System.Windows.Forms.Label labelGear;
        private System.Windows.Forms.NumericUpDown numericUpDownCatchabilityDefault;
        private System.Windows.Forms.Label labelCatchabilityDefault;
        private System.Windows.Forms.Label labelCatchabilityTitle;
        private System.Windows.Forms.Label labelCatchabilityInstruction;
        private Species.TaxonProvider speciesSelectorCatchability;
        private System.Windows.Forms.ComboBox comboBoxGear;
    }
}
