namespace Mayfly.Mathematics
{
    partial class Pivot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pivot));
            this.labelColumns = new System.Windows.Forms.Label();
            this.labelRows = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.calculator = new System.ComponentModel.BackgroundWorker();
            this.listViewValue = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sortableColumnGroups = new Mayfly.Controls.SortableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sortableRowGroups = new Mayfly.Controls.SortableListView();
            this.ColumnFactors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxGeneralized = new System.Windows.Forms.CheckBox();
            this.checkBoxAveraged = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelColumns
            // 
            resources.ApplyResources(this.labelColumns, "labelColumns");
            this.labelColumns.Name = "labelColumns";
            // 
            // labelRows
            // 
            resources.ApplyResources(this.labelRows, "labelRows");
            this.labelRows.Name = "labelRows";
            // 
            // labelValue
            // 
            resources.ApplyResources(this.labelValue, "labelValue");
            this.labelValue.Name = "labelValue";
            // 
            // calculator
            // 
            this.calculator.WorkerReportsProgress = true;
            this.calculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calculator_DoWork);
            this.calculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calculator_RunWorkerCompleted);
            // 
            // listViewValue
            // 
            resources.ApplyResources(this.listViewValue, "listViewValue");
            this.listViewValue.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listViewValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewValue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewValue.FullRowSelect = true;
            this.listViewValue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewValue.HideSelection = false;
            this.listViewValue.MultiSelect = false;
            this.listViewValue.Name = "listViewValue";
            this.listViewValue.TileSize = new System.Drawing.Size(135, 20);
            this.listViewValue.UseCompatibleStateImageBehavior = false;
            this.listViewValue.View = System.Windows.Forms.View.Tile;
            this.listViewValue.SelectedIndexChanged += new System.EventHandler(this.pivot_Changed);
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // comboBoxFormat
            // 
            resources.ApplyResources(this.comboBoxFormat, "comboBoxFormat");
            this.comboBoxFormat.DisplayMember = "Text";
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.ValueMember = "Value";
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            this.comboBoxFormat.TextChanged += new System.EventHandler(this.comboBoxFormat_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // sortableColumnGroups
            // 
            resources.ApplyResources(this.sortableColumnGroups, "sortableColumnGroups");
            this.sortableColumnGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sortableColumnGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.sortableColumnGroups.FullRowSelect = true;
            this.sortableColumnGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.sortableColumnGroups.HideSelection = false;
            this.sortableColumnGroups.LineAfter = -1;
            this.sortableColumnGroups.LineBefore = -1;
            this.sortableColumnGroups.MultiSelect = false;
            this.sortableColumnGroups.Name = "sortableColumnGroups";
            this.sortableColumnGroups.ShowGroups = false;
            this.sortableColumnGroups.TileSize = new System.Drawing.Size(135, 20);
            this.sortableColumnGroups.UseCompatibleStateImageBehavior = false;
            this.sortableColumnGroups.View = System.Windows.Forms.View.Tile;
            this.sortableColumnGroups.SelectedIndexChanged += new System.EventHandler(this.sortableColumnGroups_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // sortableRowGroups
            // 
            resources.ApplyResources(this.sortableRowGroups, "sortableRowGroups");
            this.sortableRowGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sortableRowGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnFactors});
            this.sortableRowGroups.FullRowSelect = true;
            this.sortableRowGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.sortableRowGroups.HideSelection = false;
            this.sortableRowGroups.LineAfter = -1;
            this.sortableRowGroups.LineBefore = -1;
            this.sortableRowGroups.MultiSelect = false;
            this.sortableRowGroups.Name = "sortableRowGroups";
            this.sortableRowGroups.ShowGroups = false;
            this.sortableRowGroups.TileSize = new System.Drawing.Size(135, 20);
            this.sortableRowGroups.UseCompatibleStateImageBehavior = false;
            this.sortableRowGroups.View = System.Windows.Forms.View.Tile;
            this.sortableRowGroups.SelectedIndexChanged += new System.EventHandler(this.sortableRowGroups_SelectedIndexChanged);
            // 
            // ColumnFactors
            // 
            resources.ApplyResources(this.ColumnFactors, "ColumnFactors");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBoxGeneralized
            // 
            resources.ApplyResources(this.checkBoxGeneralized, "checkBoxGeneralized");
            this.checkBoxGeneralized.Name = "checkBoxGeneralized";
            this.checkBoxGeneralized.UseVisualStyleBackColor = true;
            this.checkBoxGeneralized.CheckedChanged += new System.EventHandler(this.pivot_Changed);
            // 
            // checkBoxAveraged
            // 
            resources.ApplyResources(this.checkBoxAveraged, "checkBoxAveraged");
            this.checkBoxAveraged.Checked = true;
            this.checkBoxAveraged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAveraged.Name = "checkBoxAveraged";
            this.checkBoxAveraged.UseVisualStyleBackColor = true;
            this.checkBoxAveraged.CheckedChanged += new System.EventHandler(this.pivot_Changed);
            // 
            // Pivot
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.checkBoxAveraged);
            this.Controls.Add(this.checkBoxGeneralized);
            this.Controls.Add(this.comboBoxFormat);
            this.Controls.Add(this.sortableColumnGroups);
            this.Controls.Add(this.sortableRowGroups);
            this.Controls.Add(this.listViewValue);
            this.Controls.Add(this.labelColumns);
            this.Controls.Add(this.labelRows);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelValue);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pivot";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pivot_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelColumns;
        private System.Windows.Forms.Label labelRows;
        private System.Windows.Forms.Label labelValue;
        private System.ComponentModel.BackgroundWorker calculator;
        private System.Windows.Forms.ListView listViewValue;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        public Controls.SortableListView sortableRowGroups;
        private System.Windows.Forms.ColumnHeader ColumnFactors;
        public Controls.SortableListView sortableColumnGroups;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxGeneralized;
        private System.Windows.Forms.CheckBox checkBoxAveraged;
    }
}