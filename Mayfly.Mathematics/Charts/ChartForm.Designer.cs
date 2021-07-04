namespace Mayfly.Mathematics.Charts
{
    partial class ChartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartForm));
            this.StatChart = new Mayfly.Mathematics.Charts.Plot();
            ((System.ComponentModel.ISupportInitialize)(this.StatChart)).BeginInit();
            this.SuspendLayout();
            // 
            // StatChart
            // 
            resources.ApplyResources(this.StatChart, "StatChart");
            this.StatChart.AxisYInterval = 5D;
            this.StatChart.Name = "StatChart";
            this.StatChart.ShowLegend = false;
            this.StatChart.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // ChartForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.StatChart);
            this.Name = "ChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.StatChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Plot StatChart;

    }
}