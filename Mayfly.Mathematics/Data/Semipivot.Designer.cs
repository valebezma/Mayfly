namespace Mayfly.Mathematics
{
    partial class Semipivot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Semipivot));
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxRow = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listViewColumn = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.calculator = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBoxRow
            // 
            resources.ApplyResources(this.comboBoxRow, "comboBoxRow");
            this.comboBoxRow.DisplayMember = "HeaderText";
            this.comboBoxRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRow.FormattingEnabled = true;
            this.comboBoxRow.Name = "comboBoxRow";
            this.comboBoxRow.SelectedIndexChanged += new System.EventHandler(this.group_Changed);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // listViewColumn
            // 
            this.listViewColumn.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            resources.ApplyResources(this.listViewColumn, "listViewColumn");
            this.listViewColumn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewColumn.FullRowSelect = true;
            this.listViewColumn.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewColumn.HideSelection = false;
            this.listViewColumn.Name = "listViewColumn";
            this.listViewColumn.TileSize = new System.Drawing.Size(135, 20);
            this.listViewColumn.UseCompatibleStateImageBehavior = false;
            this.listViewColumn.View = System.Windows.Forms.View.Tile;
            this.listViewColumn.SelectedIndexChanged += new System.EventHandler(this.group_Changed);
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // calculator
            // 
            this.calculator.WorkerReportsProgress = true;
            this.calculator.WorkerSupportsCancellation = true;
            this.calculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calculator_DoWork);
            this.calculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calculator_RunWorkerCompleted);
            // 
            // Semipivot
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxRow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listViewColumn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Semipivot";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Semipivot_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Sheet_BoundsChanged);
            this.LocationChanged += new System.EventHandler(this.Sheet_BoundsChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxRow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listViewColumn;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.ComponentModel.BackgroundWorker calculator;
    }
}