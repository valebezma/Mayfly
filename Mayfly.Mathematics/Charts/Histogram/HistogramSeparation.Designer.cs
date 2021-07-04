namespace Mayfly.Mathematics.Charts
{
    partial class HistogramSeparation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistogramSeparation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridLog = new System.Windows.Forms.DataGridView();
            this.ColumnGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMean = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonAdjust = new System.Windows.Forms.Button();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statChartPlot = new Mayfly.Mathematics.Charts.Plot();
            ((System.ComponentModel.ISupportInitialize)(this.GridLog)).BeginInit();
            this.SuspendLayout();
            // 
            // GridLog
            // 
            this.GridLog.AllowDrop = true;
            this.GridLog.AllowUserToAddRows = false;
            this.GridLog.AllowUserToResizeColumns = false;
            this.GridLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.GridLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.GridLog, "GridLog");
            this.GridLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GridLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridLog.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.GridLog.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnGroup,
            this.ColumnMean,
            this.ColumnSD,
            this.ColumnN,
            this.ColumnSI});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridLog.DefaultCellStyle = dataGridViewCellStyle8;
            this.GridLog.Name = "GridLog";
            this.GridLog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.GridLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridLog.RowTemplate.Height = 20;
            this.GridLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GridLog.ShowEditingIcon = false;
            // 
            // ColumnGroup
            // 
            this.ColumnGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.ColumnGroup.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnGroup, "ColumnGroup");
            this.ColumnGroup.Name = "ColumnGroup";
            // 
            // ColumnMean
            // 
            dataGridViewCellStyle4.Format = ".000";
            this.ColumnMean.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnMean, "ColumnMean");
            this.ColumnMean.Name = "ColumnMean";
            // 
            // ColumnSD
            // 
            dataGridViewCellStyle5.Format = ".000";
            this.ColumnSD.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnSD, "ColumnSD");
            this.ColumnSD.Name = "ColumnSD";
            // 
            // ColumnN
            // 
            dataGridViewCellStyle6.Format = "0";
            this.ColumnN.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnN, "ColumnN");
            this.ColumnN.Name = "ColumnN";
            // 
            // ColumnSI
            // 
            dataGridViewCellStyle7.Format = ".00";
            this.ColumnSI.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnSI, "ColumnSI");
            this.ColumnSI.Name = "ColumnSI";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.toolTip1.SetToolTip(this.buttonNext, resources.GetString("buttonNext.ToolTip"));
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonAdjust
            // 
            resources.ApplyResources(this.buttonAdjust, "buttonAdjust");
            this.buttonAdjust.Name = "buttonAdjust";
            this.toolTip1.SetToolTip(this.buttonAdjust, resources.GetString("buttonAdjust.ToolTip"));
            this.buttonAdjust.UseVisualStyleBackColor = true;
            this.buttonAdjust.Click += new System.EventHandler(this.buttonAdjust_Click);
            // 
            // labelInstruction
            // 
            resources.ApplyResources(this.labelInstruction, "labelInstruction");
            this.labelInstruction.Name = "labelInstruction";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.toolTip1.SetToolTip(this.buttonOK, resources.GetString("buttonOK.ToolTip"));
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonRestart
            // 
            resources.ApplyResources(this.buttonRestart, "buttonRestart");
            this.buttonRestart.Name = "buttonRestart";
            this.toolTip1.SetToolTip(this.buttonRestart, resources.GetString("buttonRestart.ToolTip"));
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // buttonReport
            // 
            resources.ApplyResources(this.buttonReport, "buttonReport");
            this.buttonReport.Name = "buttonReport";
            this.toolTip1.SetToolTip(this.buttonReport, resources.GetString("buttonReport.ToolTip"));
            this.buttonReport.UseVisualStyleBackColor = true;
            // 
            // statChartPlot
            // 
            this.statChartPlot.AxisXTitle = "";
            this.statChartPlot.AxisYTitle = "";
            this.statChartPlot.Text = "";
            resources.ApplyResources(this.statChartPlot, "statChartPlot");
            this.statChartPlot.Name = "statChartPlot";
            this.statChartPlot.ShowLegend = false;
            this.statChartPlot.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            this.statChartPlot.SelectionChanged += new System.EventHandler(this.statChartPlot_SelectionChanged);
            // 
            // Separation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.buttonAdjust);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.GridLog);
            this.Controls.Add(this.statChartPlot);
            this.Name = "Separation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HistogramSeparation_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.GridLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Plot statChartPlot;
        private System.Windows.Forms.DataGridView GridLog;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonAdjust;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMean;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSI;
    }
}