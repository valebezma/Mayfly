namespace Mayfly.Fish.Explorer
{
    partial class SettingsPageReproduction
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
            this.spreadSheetAge = new Mayfly.Controls.SpreadSheet();
            this.ColumnAgeSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAgeValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelAgeInstrcution = new System.Windows.Forms.Label();
            this.labelAgeTitle = new System.Windows.Forms.Label();
            this.speciesSelectorAge = new Mayfly.Species.TaxonProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAge)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadSheetAge
            // 
            this.spreadSheetAge.AllowUserToAddRows = true;
            this.spreadSheetAge.AllowUserToDeleteRows = true;
            this.spreadSheetAge.AllowUserToResizeColumns = false;
            this.spreadSheetAge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetAge.AutoClearEmptyRows = true;
            this.spreadSheetAge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAgeSpecies,
            this.ColumnAgeValue});
            this.spreadSheetAge.DefaultDecimalPlaces = 0;
            this.spreadSheetAge.Location = new System.Drawing.Point(51, 109);
            this.spreadSheetAge.Name = "spreadSheetAge";
            this.spreadSheetAge.RowHeadersWidth = 25;
            this.spreadSheetAge.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.spreadSheetAge.Size = new System.Drawing.Size(301, 143);
            this.spreadSheetAge.StatusFormat = null;
            this.spreadSheetAge.TabIndex = 2;
            // 
            // ColumnAgeSpecies
            // 
            this.ColumnAgeSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnAgeSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnAgeSpecies.HeaderText = "Species";
            this.ColumnAgeSpecies.Name = "ColumnAgeSpecies";
            // 
            // ColumnAgeValue
            // 
            this.ColumnAgeValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAgeValue.HeaderText = "Age";
            this.ColumnAgeValue.Name = "ColumnAgeValue";
            this.ColumnAgeValue.Width = 75;
            // 
            // labelAgeInstrcution
            // 
            this.labelAgeInstrcution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAgeInstrcution.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAgeInstrcution.Location = new System.Drawing.Point(48, 65);
            this.labelAgeInstrcution.Margin = new System.Windows.Forms.Padding(45, 0, 45, 0);
            this.labelAgeInstrcution.Name = "labelAgeInstrcution";
            this.labelAgeInstrcution.Size = new System.Drawing.Size(310, 26);
            this.labelAgeInstrcution.TabIndex = 1;
            this.labelAgeInstrcution.Text = "Reproductive age of a fish is an age when most of all fishes (50%+) became mature" +
    ". It is used it total allowable catch calculation.";
            // 
            // labelAgeTitle
            // 
            this.labelAgeTitle.AutoSize = true;
            this.labelAgeTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelAgeTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelAgeTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAgeTitle.Location = new System.Drawing.Point(28, 25);
            this.labelAgeTitle.Name = "labelAgeTitle";
            this.labelAgeTitle.Size = new System.Drawing.Size(148, 15);
            this.labelAgeTitle.TabIndex = 0;
            this.labelAgeTitle.Text = "Species Reproductive Ages";
            // 
            // speciesSelectorAge
            // 
            this.speciesSelectorAge.CheckDuplicates = false;
            this.speciesSelectorAge.ColumnName = "ColumnAgeSpecies";
            this.speciesSelectorAge.Grid = this.spreadSheetAge;
            this.speciesSelectorAge.RecentListCount = 0;
            // 
            // SettingsControlReproduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spreadSheetAge);
            this.Controls.Add(this.labelAgeInstrcution);
            this.Controls.Add(this.labelAgeTitle);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "SettingsControlReproduction";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SpreadSheet spreadSheetAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAgeSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAgeValue;
        private System.Windows.Forms.Label labelAgeInstrcution;
        private System.Windows.Forms.Label labelAgeTitle;
        private Species.TaxonProvider speciesSelectorAge;
    }
}
