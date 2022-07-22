namespace Mayfly.Fish.Explorer
{
    partial class SettingsPageMeasure
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
            this.spreadSheetMeasure = new Mayfly.Controls.SpreadSheet();
            this.ColumnMeasureSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMeasureValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelMeasureInstruction = new System.Windows.Forms.Label();
            this.labelMeasureTitle = new System.Windows.Forms.Label();
            this.speciesSelectorMeasure = new Mayfly.Species.TaxonProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMeasure)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadSheetMeasure
            // 
            this.spreadSheetMeasure.AllowUserToAddRows = true;
            this.spreadSheetMeasure.AllowUserToDeleteRows = true;
            this.spreadSheetMeasure.AllowUserToResizeColumns = false;
            this.spreadSheetMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetMeasure.AutoClearEmptyRows = true;
            this.spreadSheetMeasure.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMeasureSpecies,
            this.ColumnMeasureValue});
            this.spreadSheetMeasure.DefaultDecimalPlaces = 0;
            this.spreadSheetMeasure.Location = new System.Drawing.Point(51, 109);
            this.spreadSheetMeasure.Name = "spreadSheetMeasure";
            this.spreadSheetMeasure.RowHeadersWidth = 25;
            this.spreadSheetMeasure.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.spreadSheetMeasure.Size = new System.Drawing.Size(304, 343);
            this.spreadSheetMeasure.StatusFormat = null;
            this.spreadSheetMeasure.TabIndex = 2;
            // 
            // ColumnMeasureSpecies
            // 
            this.ColumnMeasureSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnMeasureSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnMeasureSpecies.HeaderText = "Species";
            this.ColumnMeasureSpecies.Name = "ColumnMeasureSpecies";
            // 
            // ColumnMeasureValue
            // 
            this.ColumnMeasureValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnMeasureValue.HeaderText = "Length, cm";
            this.ColumnMeasureValue.Name = "ColumnMeasureValue";
            // 
            // labelMeasureInstruction
            // 
            this.labelMeasureInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMeasureInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMeasureInstruction.Location = new System.Drawing.Point(48, 65);
            this.labelMeasureInstruction.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
            this.labelMeasureInstruction.Name = "labelMeasureInstruction";
            this.labelMeasureInstruction.Size = new System.Drawing.Size(310, 26);
            this.labelMeasureInstruction.TabIndex = 1;
            this.labelMeasureInstruction.Text = "Minimum allowable length is a measure of fish that can be taken by fisheries. It " +
    "is used in total allowable catch calculation.";
            // 
            // labelMeasureTitle
            // 
            this.labelMeasureTitle.AutoSize = true;
            this.labelMeasureTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelMeasureTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelMeasureTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMeasureTitle.Location = new System.Drawing.Point(28, 25);
            this.labelMeasureTitle.Name = "labelMeasureTitle";
            this.labelMeasureTitle.Size = new System.Drawing.Size(184, 15);
            this.labelMeasureTitle.TabIndex = 0;
            this.labelMeasureTitle.Text = "Species Minimal Allowed Lengths";
            // 
            // speciesSelectorMeasure
            // 
            this.speciesSelectorMeasure.CheckDuplicates = false;
            this.speciesSelectorMeasure.ColumnName = "ColumnMeasureSpecies";
            this.speciesSelectorMeasure.Grid = this.spreadSheetMeasure;
            this.speciesSelectorMeasure.RecentListCount = 0;
            // 
            // SettingsControlMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spreadSheetMeasure);
            this.Controls.Add(this.labelMeasureInstruction);
            this.Controls.Add(this.labelMeasureTitle);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "SettingsControlMeasure";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMeasure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SpreadSheet spreadSheetMeasure;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasureSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasureValue;
        private System.Windows.Forms.Label labelMeasureInstruction;
        private System.Windows.Forms.Label labelMeasureTitle;
        private Species.TaxonProvider speciesSelectorMeasure;
    }
}
