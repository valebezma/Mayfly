namespace Mayfly.Fish.Explorer
{
    partial class AgeKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgeKeyForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextHideEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageKey = new System.Windows.Forms.TabPage();
            this.spreadSheet = new Mayfly.Controls.SpreadSheet();
            this.columnSizeClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageChart = new System.Windows.Forms.TabPage();
            this.plotT = new Mayfly.Mathematics.Charts.Plot();
            this.contextMenuStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pageKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheet)).BeginInit();
            this.pageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotT)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextHideEmpty});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // contextHideEmpty
            // 
            this.contextHideEmpty.CheckOnClick = true;
            this.contextHideEmpty.Name = "contextHideEmpty";
            resources.ApplyResources(this.contextHideEmpty, "contextHideEmpty");
            this.contextHideEmpty.CheckedChanged += new System.EventHandler(this.ToolStripMenuItemHideEmpty_CheckedChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.pageKey);
            this.tabControl1.Controls.Add(this.pageChart);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // pageKey
            // 
            this.pageKey.Controls.Add(this.spreadSheet);
            resources.ApplyResources(this.pageKey, "pageKey");
            this.pageKey.Name = "pageKey";
            this.pageKey.UseVisualStyleBackColor = true;
            // 
            // spreadSheet
            // 
            this.spreadSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSizeClass,
            this.columnAge,
            this.columnLength});
            this.spreadSheet.DefaultDecimalPlaces = 0;
            resources.ApplyResources(this.spreadSheet, "spreadSheet");
            this.spreadSheet.Name = "spreadSheet";
            this.spreadSheet.ReadOnly = true;
            this.spreadSheet.RowMenu = this.contextMenuStrip;
            // 
            // columnSizeClass
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.columnSizeClass.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnSizeClass.FillWeight = 200F;
            this.columnSizeClass.Frozen = true;
            resources.ApplyResources(this.columnSizeClass, "columnSizeClass");
            this.columnSizeClass.Name = "columnSizeClass";
            this.columnSizeClass.ReadOnly = true;
            this.columnSizeClass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnAge
            // 
            resources.ApplyResources(this.columnAge, "columnAge");
            this.columnAge.Name = "columnAge";
            this.columnAge.ReadOnly = true;
            this.columnAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnLength
            // 
            resources.ApplyResources(this.columnLength, "columnLength");
            this.columnLength.Name = "columnLength";
            this.columnLength.ReadOnly = true;
            this.columnLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pageChart
            // 
            this.pageChart.Controls.Add(this.plotT);
            resources.ApplyResources(this.pageChart, "pageChart");
            this.pageChart.Name = "pageChart";
            this.pageChart.UseVisualStyleBackColor = true;
            // 
            // plotT
            // 
            this.plotT.AxisXAutoMaximum = false;
            this.plotT.AxisXAutoMinimum = false;
            resources.ApplyResources(this.plotT, "plotT");
            this.plotT.AxisYAutoMaximum = false;
            this.plotT.AxisYAutoMinimum = false;
            this.plotT.Name = "plotT";
            this.plotT.ShowLegend = true;
            this.plotT.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // AgeKeyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "AgeKeyForm";
            this.ShowIcon = false;
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.pageKey.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheet)).EndInit();
            this.pageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem contextHideEmpty;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageKey;
        private Controls.SpreadSheet spreadSheet;
        private System.Windows.Forms.TabPage pageChart;
        private Mathematics.Charts.Plot plotT;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSizeClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLength;

    }
}