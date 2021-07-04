namespace Mayfly.Mathematics.Statistics
{
    partial class AnovaProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnovaProperties));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelValues = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flowSamples = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxCompatibility = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFactor = new System.Windows.Forms.Label();
            this.textBoxFactor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.contextFactor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextPosthoc = new System.Windows.Forms.ToolStripMenuItem();
            this.contextChart = new System.Windows.Forms.ToolStripMenuItem();
            this.GridResults = new Mayfly.Controls.SpreadSheet();
            this.columnSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnP = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCompatibility)).BeginInit();
            this.contextFactor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridResults)).BeginInit();
            this.SuspendLayout();
            // 
            // labelValues
            // 
            resources.ApplyResources(this.labelValues, "labelValues");
            this.labelValues.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelValues.Name = "labelValues";
            // 
            // flowSamples
            // 
            resources.ApplyResources(this.flowSamples, "flowSamples");
            this.flowSamples.Name = "flowSamples";
            this.toolTip1.SetToolTip(this.flowSamples, resources.GetString("flowSamples.ToolTip"));
            // 
            // pictureBoxCompatibility
            // 
            this.pictureBoxCompatibility.Image = global::Mayfly.Mathematics.Properties.Resources.Check;
            resources.ApplyResources(this.pictureBoxCompatibility, "pictureBoxCompatibility");
            this.pictureBoxCompatibility.Name = "pictureBoxCompatibility";
            this.pictureBoxCompatibility.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxCompatibility, resources.GetString("pictureBoxCompatibility.ToolTip"));
            this.pictureBoxCompatibility.DoubleClick += new System.EventHandler(this.pictureBoxCompatibility_DoubleClick);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // labelFactor
            // 
            resources.ApplyResources(this.labelFactor, "labelFactor");
            this.labelFactor.Name = "labelFactor";
            // 
            // textBoxFactor
            // 
            resources.ApplyResources(this.textBoxFactor, "textBoxFactor");
            this.textBoxFactor.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxFactor.Name = "textBoxFactor";
            this.textBoxFactor.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonReport
            // 
            resources.ApplyResources(this.buttonReport, "buttonReport");
            this.buttonReport.FlatAppearance.BorderSize = 0;
            this.buttonReport.Image = global::Mayfly.Properties.Resources.Report;
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonCopy
            // 
            resources.ApplyResources(this.buttonCopy, "buttonCopy");
            this.buttonCopy.FlatAppearance.BorderSize = 0;
            this.buttonCopy.Image = global::Mayfly.Properties.Resources.Copy;
            this.buttonCopy.Name = "buttonCopy";
            // 
            // contextFactor
            // 
            this.contextFactor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextPosthoc,
            this.contextChart});
            this.contextFactor.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextFactor, "contextFactor");
            // 
            // contextPosthoc
            // 
            this.contextPosthoc.Name = "contextPosthoc";
            resources.ApplyResources(this.contextPosthoc, "contextPosthoc");
            this.contextPosthoc.Click += new System.EventHandler(this.contextPosthoc_Click);
            // 
            // contextChart
            // 
            this.contextChart.Name = "contextChart";
            resources.ApplyResources(this.contextChart, "contextChart");
            this.contextChart.Click += new System.EventHandler(this.contextChart_Click);
            // 
            // GridResults
            // 
            resources.ApplyResources(this.GridResults, "GridResults");
            this.GridResults.CellPadding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.GridResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSource,
            this.columnDF,
            this.columnSS,
            this.columnMS,
            this.columnF,
            this.columnP});
            this.GridResults.DefaultDecimalPlaces = 3;
            this.GridResults.MultiSelect = false;
            this.GridResults.Name = "GridResults";
            this.GridResults.ReadOnly = true;
            this.GridResults.RowHeadersVisible = false;
            this.GridResults.RowMenu = this.contextFactor;
            this.GridResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridResults.SelectionChanged += new System.EventHandler(this.GridResults_SelectionChanged);
            this.GridResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GridResults_MouseDoubleClick);
            // 
            // columnSource
            // 
            this.columnSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSource.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnSource, "columnSource");
            this.columnSource.Name = "columnSource";
            this.columnSource.ReadOnly = true;
            this.columnSource.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnDF
            // 
            this.columnDF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Format = "N0";
            this.columnDF.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnDF, "columnDF");
            this.columnDF.Name = "columnDF";
            this.columnDF.ReadOnly = true;
            this.columnDF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnSS
            // 
            this.columnSS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Format = "N4";
            this.columnSS.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnSS, "columnSS");
            this.columnSS.Name = "columnSS";
            this.columnSS.ReadOnly = true;
            this.columnSS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnMS
            // 
            this.columnMS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(this.columnMS, "columnMS");
            this.columnMS.Name = "columnMS";
            this.columnMS.ReadOnly = true;
            this.columnMS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnF
            // 
            this.columnF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(this.columnF, "columnF");
            this.columnF.Name = "columnF";
            this.columnF.ReadOnly = true;
            this.columnF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnP
            // 
            this.columnP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.columnP.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnP, "columnP");
            this.columnP.Image = null;
            this.columnP.Name = "columnP";
            this.columnP.ReadOnly = true;
            this.columnP.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AnovaProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.GridResults);
            this.Controls.Add(this.flowSamples);
            this.Controls.Add(this.pictureBoxCompatibility);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelValues);
            this.Controls.Add(this.textBoxFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelFactor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnovaProperties";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCompatibility)).EndInit();
            this.contextFactor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelValues;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFactor;
        public System.Windows.Forms.TextBox textBoxFactor;
        private System.Windows.Forms.FlowLayoutPanel flowSamples;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxCompatibility;
        private Mayfly.Controls.SpreadSheet GridResults;
        private System.Windows.Forms.ContextMenuStrip contextFactor;
        private System.Windows.Forms.ToolStripMenuItem contextPosthoc;
        private System.Windows.Forms.ToolStripMenuItem contextChart;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSS;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMS;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnF;
        private Controls.SpreadSheetIconTextBoxColumn columnP;

    }
}