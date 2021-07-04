namespace Mayfly.Fish.Explorer.Observations
{
    partial class StratifiedControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StratifiedControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listViewSpecies = new System.Windows.Forms.ListView();
            this.columnHeaderSpecies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSample = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.spreadSheetSample = new Mayfly.Controls.SpreadSheet();
            this.ColumnClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.ColumnRegID = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.buttonReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSample)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewSpecies
            // 
            resources.ApplyResources(this.listViewSpecies, "listViewSpecies");
            this.listViewSpecies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSpecies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSpecies,
            this.columnHeaderSample});
            this.listViewSpecies.FullRowSelect = true;
            this.listViewSpecies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewSpecies.HideSelection = false;
            this.listViewSpecies.MultiSelect = false;
            this.listViewSpecies.Name = "listViewSpecies";
            this.listViewSpecies.ShowGroups = false;
            this.listViewSpecies.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewSpecies.TileSize = new System.Drawing.Size(175, 25);
            this.listViewSpecies.UseCompatibleStateImageBehavior = false;
            this.listViewSpecies.View = System.Windows.Forms.View.Tile;
            this.listViewSpecies.SelectedIndexChanged += new System.EventHandler(this.listViewSpecies_SelectedIndexChanged);
            // 
            // columnHeaderSpecies
            // 
            resources.ApplyResources(this.columnHeaderSpecies, "columnHeaderSpecies");
            // 
            // columnHeaderSample
            // 
            resources.ApplyResources(this.columnHeaderSample, "columnHeaderSample");
            // 
            // spreadSheetSample
            // 
            resources.ApplyResources(this.spreadSheetSample, "spreadSheetSample");
            this.spreadSheetSample.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnClass,
            this.ColumnCount,
            this.ColumnMass,
            this.ColumnRegID});
            this.spreadSheetSample.DefaultDecimalPlaces = 0;
            this.spreadSheetSample.Name = "spreadSheetSample";
            this.spreadSheetSample.ReadOnly = true;
            this.spreadSheetSample.RowHeadersVisible = false;
            // 
            // ColumnClass
            // 
            resources.ApplyResources(this.ColumnClass, "ColumnClass");
            this.ColumnClass.Name = "ColumnClass";
            this.ColumnClass.ReadOnly = true;
            // 
            // ColumnCount
            // 
            resources.ApplyResources(this.ColumnCount, "ColumnCount");
            this.ColumnCount.Name = "ColumnCount";
            this.ColumnCount.ReadOnly = true;
            // 
            // ColumnMass
            // 
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5, 0, 15, 0);
            this.ColumnMass.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnMass, "ColumnMass");
            this.ColumnMass.Image = null;
            this.ColumnMass.Name = "ColumnMass";
            this.ColumnMass.ReadOnly = true;
            this.ColumnMass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnRegID
            // 
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5, 0, 15, 0);
            this.ColumnRegID.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnRegID, "ColumnRegID");
            this.ColumnRegID.Image = null;
            this.ColumnRegID.Name = "ColumnRegID";
            this.ColumnRegID.ReadOnly = true;
            this.ColumnRegID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // buttonReport
            // 
            resources.ApplyResources(this.buttonReport, "buttonReport");
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // StratifiedControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.spreadSheetSample);
            this.Controls.Add(this.listViewSpecies);
            this.Name = "StratifiedControl";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSample)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewSpecies;
        private System.Windows.Forms.ColumnHeader columnHeaderSpecies;
        private System.Windows.Forms.ColumnHeader columnHeaderSample;
        private Controls.SpreadSheet spreadSheetSample;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
        private Controls.SpreadSheetIconTextBoxColumn ColumnMass;
        private Controls.SpreadSheetIconTextBoxColumn ColumnRegID;
        private System.Windows.Forms.Button buttonReport;
    }
}