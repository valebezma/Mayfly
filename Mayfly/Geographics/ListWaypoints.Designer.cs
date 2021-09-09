namespace Mayfly.Geographics
{
    partial class ListWaypoints
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListWaypoints));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridWpt = new System.Windows.Forms.DataGridView();
            this.ColumnWPT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLatT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLngT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAlt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextWaypoints = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextItemGetTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.contextItemGetPoly = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridWpt)).BeginInit();
            this.contextWaypoints.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridWpt
            // 
            resources.ApplyResources(this.GridWpt, "GridWpt");
            this.GridWpt.AllowDrop = true;
            this.GridWpt.AllowUserToAddRows = false;
            this.GridWpt.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GridWpt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.ColumnWPT,
            this.ColumnName,
            this.ColumnLatT,
            this.ColumnLngT,
            this.ColumnAlt,
            this.ColumnTime});
            this.GridWpt.ContextMenuStrip = this.contextWaypoints;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridWpt.DefaultCellStyle = dataGridViewCellStyle4;
            this.GridWpt.Name = "GridWpt";
            this.GridWpt.ReadOnly = true;
            this.GridWpt.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.GridWpt.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridWpt.RowTemplate.Height = 20;
            this.GridWpt.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridWpt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridWpt.SelectionChanged += new System.EventHandler(this.GridWpt_SelectionChanged);
            // 
            // ColumnWPT
            // 
            resources.ApplyResources(this.ColumnWPT, "ColumnWPT");
            this.ColumnWPT.Name = "ColumnWPT";
            this.ColumnWPT.ReadOnly = true;
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
            // ColumnLatT
            // 
            this.ColumnLatT.FillWeight = 120F;
            resources.ApplyResources(this.ColumnLatT, "ColumnLatT");
            this.ColumnLatT.Name = "ColumnLatT";
            this.ColumnLatT.ReadOnly = true;
            // 
            // ColumnLngT
            // 
            this.ColumnLngT.FillWeight = 120F;
            resources.ApplyResources(this.ColumnLngT, "ColumnLngT");
            this.ColumnLngT.Name = "ColumnLngT";
            this.ColumnLngT.ReadOnly = true;
            // 
            // ColumnAlt
            // 
            this.ColumnAlt.FillWeight = 75F;
            resources.ApplyResources(this.ColumnAlt, "ColumnAlt");
            this.ColumnAlt.Name = "ColumnAlt";
            this.ColumnAlt.ReadOnly = true;
            // 
            // ColumnTime
            // 
            this.ColumnTime.FillWeight = 120F;
            resources.ApplyResources(this.ColumnTime, "ColumnTime");
            this.ColumnTime.Name = "ColumnTime";
            this.ColumnTime.ReadOnly = true;
            // 
            // contextWaypoints
            // 
            resources.ApplyResources(this.contextWaypoints, "contextWaypoints");
            this.contextWaypoints.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextItemGetTrack,
            this.contextItemGetPoly});
            this.contextWaypoints.Name = "contextWaypoints";
            this.contextWaypoints.Opening += new System.ComponentModel.CancelEventHandler(this.contextWaypoints_Opening);
            // 
            // contextItemGetTrack
            // 
            resources.ApplyResources(this.contextItemGetTrack, "contextItemGetTrack");
            this.contextItemGetTrack.Name = "contextItemGetTrack";
            this.contextItemGetTrack.Click += new System.EventHandler(this.contextItemGetTrack_Click);
            // 
            // contextItemGetPoly
            // 
            resources.ApplyResources(this.contextItemGetPoly, "contextItemGetPoly");
            this.contextItemGetPoly.Name = "contextItemGetPoly";
            this.contextItemGetPoly.Click += new System.EventHandler(this.contextItemGetPoly_Click);
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
            // buttonSettings
            // 
            resources.ApplyResources(this.buttonSettings, "buttonSettings");
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // ListWaypoints
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.GridWpt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListWaypoints";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.GridWpt)).EndInit();
            this.contextWaypoints.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView GridWpt;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWPT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLatT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLngT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAlt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.ContextMenuStrip contextWaypoints;
        private System.Windows.Forms.ToolStripMenuItem contextItemGetTrack;
        private System.Windows.Forms.ToolStripMenuItem contextItemGetPoly;
    }
}