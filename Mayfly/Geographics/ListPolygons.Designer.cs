namespace Mayfly.Geographics
{
    partial class ListPolygons
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPolygons));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridWpt = new System.Windows.Forms.DataGridView();
            this.ColumnPolygon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPerimeter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridWpt)).BeginInit();
            this.SuspendLayout();
            // 
            // GridWpt
            // 
            this.GridWpt.AllowDrop = true;
            this.GridWpt.AllowUserToAddRows = false;
            this.GridWpt.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GridWpt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.GridWpt, "GridWpt");
            this.GridWpt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridWpt.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GridWpt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridWpt.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridWpt.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridWpt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridWpt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GridWpt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPolygon,
            this.ColumnName,
            this.ColumnPerimeter,
            this.ColumnArea,
            this.ColumnWidth});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridWpt.DefaultCellStyle = dataGridViewCellStyle7;
            this.GridWpt.Name = "GridWpt";
            this.GridWpt.ReadOnly = true;
            this.GridWpt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GridWpt.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridWpt.RowTemplate.Height = 20;
            this.GridWpt.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridWpt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridWpt.SelectionChanged += new System.EventHandler(this.GridWpt_SelectionChanged);
            // 
            // ColumnPolygon
            // 
            resources.ApplyResources(this.ColumnPolygon, "ColumnPolygon");
            this.ColumnPolygon.Name = "ColumnPolygon";
            this.ColumnPolygon.ReadOnly = true;
            // 
            // ColumnName
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnName.FillWeight = 150F;
            resources.ApplyResources(this.ColumnName, "ColumnName");
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            // 
            // ColumnPerimeter
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N1";
            this.ColumnPerimeter.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnPerimeter, "ColumnPerimeter");
            this.ColumnPerimeter.Name = "ColumnPerimeter";
            this.ColumnPerimeter.ReadOnly = true;
            // 
            // ColumnArea
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N4";
            this.ColumnArea.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnArea, "ColumnArea");
            this.ColumnArea.Name = "ColumnArea";
            this.ColumnArea.ReadOnly = true;
            // 
            // ColumnWidth
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N1";
            this.ColumnWidth.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnWidth.FillWeight = 120F;
            resources.ApplyResources(this.ColumnWidth, "ColumnWidth");
            this.ColumnWidth.Name = "ColumnWidth";
            this.ColumnWidth.ReadOnly = true;
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonOpen
            // 
            resources.ApplyResources(this.buttonOpen, "buttonOpen");
            this.buttonOpen.FlatAppearance.BorderSize = 0;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // ListPolygons
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.GridWpt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListPolygons";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.GridWpt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView GridWpt;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPolygon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPerimeter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWidth;
    }
}