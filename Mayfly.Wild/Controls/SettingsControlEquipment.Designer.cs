namespace Mayfly.Wild.Controls
{
    partial class SettingsControlEquipment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonClear = new System.Windows.Forms.Button();
            this.spreadSheetGears = new Mayfly.Controls.SpreadSheet();
            this.columnSampler = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnMesh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetGears)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClear.Location = new System.Drawing.Point(297, 449);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // spreadSheetGears
            // 
            this.spreadSheetGears.AllowUserToAddRows = true;
            this.spreadSheetGears.AllowUserToDeleteRows = true;
            this.spreadSheetGears.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetGears.AutoClearEmptyRows = true;
            this.spreadSheetGears.CellPadding = new System.Windows.Forms.Padding(0);
            this.spreadSheetGears.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSampler,
            this.columnMesh,
            this.columnLength,
            this.columnHeight});
            this.spreadSheetGears.Location = new System.Drawing.Point(28, 28);
            this.spreadSheetGears.Name = "spreadSheetGears";
            this.spreadSheetGears.RowHeadersWidth = 35;
            this.spreadSheetGears.RowTemplate.Height = 24;
            this.spreadSheetGears.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.spreadSheetGears.Size = new System.Drawing.Size(344, 415);
            this.spreadSheetGears.StatusFormat = null;
            this.spreadSheetGears.TabIndex = 0;
            // 
            // columnSampler
            // 
            this.columnSampler.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSampler.DefaultCellStyle = dataGridViewCellStyle5;
            this.columnSampler.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.columnSampler.HeaderText = "Sampler";
            this.columnSampler.Name = "columnSampler";
            this.columnSampler.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSampler.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // columnMesh
            // 
            dataGridViewCellStyle6.Format = "N0";
            this.columnMesh.DefaultCellStyle = dataGridViewCellStyle6;
            this.columnMesh.HeaderText = "Mesh";
            this.columnMesh.Name = "columnMesh";
            this.columnMesh.Width = 50;
            // 
            // columnLength
            // 
            this.columnLength.HeaderText = "Length, m";
            this.columnLength.Name = "columnLength";
            this.columnLength.Width = 50;
            // 
            // columnHeight
            // 
            this.columnHeight.HeaderText = "Height, m";
            this.columnHeight.Name = "columnHeight";
            this.columnHeight.Width = 50;
            // 
            // SettingsControlEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.spreadSheetGears);
            this.Name = "SettingsControlEquipment";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.Size = new System.Drawing.Size(400, 500);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetGears)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClear;
        private Mayfly.Controls.SpreadSheet spreadSheetGears;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnSampler;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMesh;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnHeight;
    }
}
