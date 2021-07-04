namespace Mayfly.Mathematics.Statistics
{
    partial class AnovaPairwise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnovaPairwise));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelAlpha = new System.Windows.Forms.Label();
            this.labelSD = new System.Windows.Forms.Label();
            this.textBoxSD = new System.Windows.Forms.TextBox();
            this.numericUpDownAlpha = new System.Windows.Forms.NumericUpDown();
            this.spreadSheetPairs = new Mayfly.Controls.SpreadSheet();
            this.buttonReport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.columnSample1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSample1Pres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSample2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSample2Pres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDifference = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.statChart1 = new Mayfly.Mathematics.Charts.Plot();
            this.mathAdapter1 = new Mayfly.Mathematics.MathAdapter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetPairs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAlpha
            // 
            resources.ApplyResources(this.labelAlpha, "labelAlpha");
            this.labelAlpha.Name = "labelAlpha";
            // 
            // labelSD
            // 
            resources.ApplyResources(this.labelSD, "labelSD");
            this.labelSD.Name = "labelSD";
            // 
            // textBoxSD
            // 
            resources.ApplyResources(this.textBoxSD, "textBoxSD");
            this.textBoxSD.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSD.Name = "textBoxSD";
            this.textBoxSD.ReadOnly = true;
            // 
            // numericUpDownAlpha
            // 
            resources.ApplyResources(this.numericUpDownAlpha, "numericUpDownAlpha");
            this.numericUpDownAlpha.DecimalPlaces = 3;
            this.numericUpDownAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAlpha.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownAlpha.Name = "numericUpDownAlpha";
            this.numericUpDownAlpha.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownAlpha.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // spreadSheetPairs
            // 
            resources.ApplyResources(this.spreadSheetPairs, "spreadSheetPairs");
            this.spreadSheetPairs.CellPadding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.spreadSheetPairs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSample1,
            this.columnSample1Pres,
            this.columnSample2,
            this.columnSample2Pres,
            this.columnDifference});
            this.spreadSheetPairs.DefaultDecimalPlaces = 3;
            this.spreadSheetPairs.Name = "spreadSheetPairs";
            this.spreadSheetPairs.ReadOnly = true;
            this.spreadSheetPairs.RowTemplate.Height = 35;
            this.spreadSheetPairs.SelectionChanged += new System.EventHandler(this.spreadSheetPairs_SelectionChanged);
            // 
            // buttonReport
            // 
            resources.ApplyResources(this.buttonReport, "buttonReport");
            this.buttonReport.FlatAppearance.BorderSize = 0;
            this.buttonReport.Image = global::Mayfly.Properties.Resources.Report;
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // columnSample1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSample1.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnSample1, "columnSample1");
            this.columnSample1.Name = "columnSample1";
            this.columnSample1.ReadOnly = true;
            this.columnSample1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnSample1Pres
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSample1Pres.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnSample1Pres, "columnSample1Pres");
            this.columnSample1Pres.Name = "columnSample1Pres";
            this.columnSample1Pres.ReadOnly = true;
            // 
            // columnSample2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSample2.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnSample2, "columnSample2");
            this.columnSample2.Name = "columnSample2";
            this.columnSample2.ReadOnly = true;
            // 
            // columnSample2Pres
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSample2Pres.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnSample2Pres, "columnSample2Pres");
            this.columnSample2Pres.Name = "columnSample2Pres";
            this.columnSample2Pres.ReadOnly = true;
            // 
            // columnDifference
            // 
            resources.ApplyResources(this.columnDifference, "columnDifference");
            this.columnDifference.Image = null;
            this.columnDifference.Name = "columnDifference";
            this.columnDifference.ReadOnly = true;
            // 
            // statChart1
            // 
            resources.ApplyResources(this.statChart1, "statChart1");
            this.statChart1.AxisXAutoInterval = false;
            this.statChart1.AxisXAutoMaximum = false;
            this.statChart1.AxisXAutoMinimum = false;
            this.statChart1.AxisXInterval = 1D;
            this.statChart1.AxisXMax = 3D;
            this.statChart1.AxisXTitle = "";
            this.statChart1.AxisYTitle = "";
            this.statChart1.Name = "statChart1";
            this.statChart1.ShowLegend = false;
            this.statChart1.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            this.statChart1.AppearanceValueChanged += new System.EventHandler(this.statChart1_AppearanceValueChanged);
            // 
            // mathAdapter1
            // 
            this.mathAdapter1.Sheet = this.spreadSheetPairs;
            // 
            // AnovaPairwise
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.numericUpDownAlpha);
            this.Controls.Add(this.spreadSheetPairs);
            this.Controls.Add(this.labelAlpha);
            this.Controls.Add(this.labelSD);
            this.Controls.Add(this.textBoxSD);
            this.Controls.Add(this.statChart1);
            this.Name = "AnovaPairwise";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetPairs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAlpha;
        private System.Windows.Forms.Label labelSD;
        public System.Windows.Forms.TextBox textBoxSD;
        private System.Windows.Forms.NumericUpDown numericUpDownAlpha;
        private Controls.SpreadSheet spreadSheetPairs;
        private Charts.Plot statChart1;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSample1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSample1Pres;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSample2;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSample2Pres;
        private Controls.SpreadSheetIconTextBoxColumn columnDifference;
        private MathAdapter mathAdapter1;
    }
}