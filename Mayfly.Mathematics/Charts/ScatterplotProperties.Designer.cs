namespace Mayfly.Mathematics.Charts
{
    partial class ScatterplotProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScatterplotProperties));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAppearance = new System.Windows.Forms.TabPage();
            this.panelTrendColor = new System.Windows.Forms.Panel();
            this.trackBarTrendWidth = new System.Windows.Forms.TrackBar();
            this.label23 = new System.Windows.Forms.Label();
            this.label_trend_color = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarMarkerWidth = new System.Windows.Forms.TrackBar();
            this.panelMarkerColor = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxRegressionName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.trackBarMarkerSize = new System.Windows.Forms.TrackBar();
            this.tabPageTrendline = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownConfidenceLevel = new System.Windows.Forms.NumericUpDown();
            this.comboBoxTrend = new System.Windows.Forms.ComboBox();
            this.checkBoxShowTrend = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxOutliers = new System.Windows.Forms.CheckBox();
            this.checkBoxPI = new System.Windows.Forms.CheckBox();
            this.checkBoxCI = new System.Windows.Forms.CheckBox();
            this.checkBoxShowExplained = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCount = new System.Windows.Forms.CheckBox();
            this.tabPageInteraction = new System.Windows.Forms.TabPage();
            this.panelColorSelectedSeries = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBoxAllowCursors = new System.Windows.Forms.CheckBox();
            this.colorDialogMarker = new System.Windows.Forms.ColorDialog();
            this.colorDialogTrend = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPageAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrendWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMarkerWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMarkerSize)).BeginInit();
            this.tabPageTrendline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConfidenceLevel)).BeginInit();
            this.tabPageInteraction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageAppearance);
            this.tabControl1.Controls.Add(this.tabPageTrendline);
            this.tabControl1.Controls.Add(this.tabPageInteraction);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageAppearance
            // 
            this.tabPageAppearance.Controls.Add(this.panelTrendColor);
            this.tabPageAppearance.Controls.Add(this.trackBarTrendWidth);
            this.tabPageAppearance.Controls.Add(this.label23);
            this.tabPageAppearance.Controls.Add(this.label_trend_color);
            this.tabPageAppearance.Controls.Add(this.label4);
            this.tabPageAppearance.Controls.Add(this.trackBarMarkerWidth);
            this.tabPageAppearance.Controls.Add(this.panelMarkerColor);
            this.tabPageAppearance.Controls.Add(this.label18);
            this.tabPageAppearance.Controls.Add(this.label21);
            this.tabPageAppearance.Controls.Add(this.label24);
            this.tabPageAppearance.Controls.Add(this.textBoxRegressionName);
            this.tabPageAppearance.Controls.Add(this.label22);
            this.tabPageAppearance.Controls.Add(this.label1);
            this.tabPageAppearance.Controls.Add(this.label20);
            this.tabPageAppearance.Controls.Add(this.trackBarMarkerSize);
            resources.ApplyResources(this.tabPageAppearance, "tabPageAppearance");
            this.tabPageAppearance.Name = "tabPageAppearance";
            this.tabPageAppearance.UseVisualStyleBackColor = true;
            // 
            // panelTrendColor
            // 
            this.panelTrendColor.BackColor = System.Drawing.Color.Maroon;
            this.panelTrendColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelTrendColor, "panelTrendColor");
            this.panelTrendColor.Name = "panelTrendColor";
            this.panelTrendColor.Click += new System.EventHandler(this.panelTrendColor_Click);
            // 
            // trackBarTrendWidth
            // 
            resources.ApplyResources(this.trackBarTrendWidth, "trackBarTrendWidth");
            this.trackBarTrendWidth.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarTrendWidth.LargeChange = 1;
            this.trackBarTrendWidth.Maximum = 5;
            this.trackBarTrendWidth.Minimum = 1;
            this.trackBarTrendWidth.Name = "trackBarTrendWidth";
            this.trackBarTrendWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarTrendWidth.Value = 2;
            this.trackBarTrendWidth.Scroll += new System.EventHandler(this.valueChanged);
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label_trend_color
            // 
            resources.ApplyResources(this.label_trend_color, "label_trend_color");
            this.label_trend_color.Name = "label_trend_color";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Name = "label4";
            // 
            // trackBarMarkerWidth
            // 
            resources.ApplyResources(this.trackBarMarkerWidth, "trackBarMarkerWidth");
            this.trackBarMarkerWidth.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarMarkerWidth.LargeChange = 1;
            this.trackBarMarkerWidth.Maximum = 5;
            this.trackBarMarkerWidth.Minimum = 1;
            this.trackBarMarkerWidth.Name = "trackBarMarkerWidth";
            this.trackBarMarkerWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMarkerWidth.Value = 1;
            this.trackBarMarkerWidth.Scroll += new System.EventHandler(this.valueChanged);
            // 
            // panelMarkerColor
            // 
            this.panelMarkerColor.BackColor = System.Drawing.Color.SeaGreen;
            this.panelMarkerColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelMarkerColor, "panelMarkerColor");
            this.panelMarkerColor.Name = "panelMarkerColor";
            this.panelMarkerColor.BackColorChanged += new System.EventHandler(this.panelMarkerColor_BackColorChanged);
            this.panelMarkerColor.Click += new System.EventHandler(this.panelMarkerColor_Click);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // textBoxRegressionName
            // 
            resources.ApplyResources(this.textBoxRegressionName, "textBoxRegressionName");
            this.textBoxRegressionName.Name = "textBoxRegressionName";
            this.textBoxRegressionName.TextChanged += new System.EventHandler(this.textBoxRegressionName_TextChanged);
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label20.Name = "label20";
            // 
            // trackBarMarkerSize
            // 
            resources.ApplyResources(this.trackBarMarkerSize, "trackBarMarkerSize");
            this.trackBarMarkerSize.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarMarkerSize.LargeChange = 1;
            this.trackBarMarkerSize.Maximum = 12;
            this.trackBarMarkerSize.Minimum = 3;
            this.trackBarMarkerSize.Name = "trackBarMarkerSize";
            this.trackBarMarkerSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMarkerSize.Value = 6;
            this.trackBarMarkerSize.Scroll += new System.EventHandler(this.valueChanged);
            // 
            // tabPageTrendline
            // 
            this.tabPageTrendline.Controls.Add(this.label5);
            this.tabPageTrendline.Controls.Add(this.numericUpDownConfidenceLevel);
            this.tabPageTrendline.Controls.Add(this.comboBoxTrend);
            this.tabPageTrendline.Controls.Add(this.checkBoxShowTrend);
            this.tabPageTrendline.Controls.Add(this.label9);
            this.tabPageTrendline.Controls.Add(this.label2);
            this.tabPageTrendline.Controls.Add(this.checkBoxOutliers);
            this.tabPageTrendline.Controls.Add(this.checkBoxPI);
            this.tabPageTrendline.Controls.Add(this.checkBoxCI);
            this.tabPageTrendline.Controls.Add(this.checkBoxShowExplained);
            this.tabPageTrendline.Controls.Add(this.checkBoxShowCount);
            resources.ApplyResources(this.tabPageTrendline, "tabPageTrendline");
            this.tabPageTrendline.Name = "tabPageTrendline";
            this.tabPageTrendline.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numericUpDownConfidenceLevel
            // 
            resources.ApplyResources(this.numericUpDownConfidenceLevel, "numericUpDownConfidenceLevel");
            this.numericUpDownConfidenceLevel.DecimalPlaces = 3;
            this.numericUpDownConfidenceLevel.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            262144});
            this.numericUpDownConfidenceLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownConfidenceLevel.Name = "numericUpDownConfidenceLevel";
            this.numericUpDownConfidenceLevel.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numericUpDownConfidenceLevel.ValueChanged += new System.EventHandler(this.valueChanged);
            // 
            // comboBoxTrend
            // 
            resources.ApplyResources(this.comboBoxTrend, "comboBoxTrend");
            this.comboBoxTrend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTrend.FormattingEnabled = true;
            this.comboBoxTrend.Name = "comboBoxTrend";
            this.comboBoxTrend.SelectedIndexChanged += new System.EventHandler(this.comboBoxTrend_SelectedIndexChanged);
            this.comboBoxTrend.Click += new System.EventHandler(this.comboBoxTrend_Click);
            // 
            // checkBoxShowTrend
            // 
            resources.ApplyResources(this.checkBoxShowTrend, "checkBoxShowTrend");
            this.checkBoxShowTrend.Name = "checkBoxShowTrend";
            this.checkBoxShowTrend.UseVisualStyleBackColor = true;
            this.checkBoxShowTrend.CheckedChanged += new System.EventHandler(this.checkBoxTrend_CheckedChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label9.Name = "label9";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // checkBoxOutliers
            // 
            resources.ApplyResources(this.checkBoxOutliers, "checkBoxOutliers");
            this.checkBoxOutliers.Name = "checkBoxOutliers";
            this.checkBoxOutliers.UseVisualStyleBackColor = true;
            this.checkBoxOutliers.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // checkBoxPI
            // 
            resources.ApplyResources(this.checkBoxPI, "checkBoxPI");
            this.checkBoxPI.Name = "checkBoxPI";
            this.checkBoxPI.UseVisualStyleBackColor = true;
            this.checkBoxPI.CheckedChanged += new System.EventHandler(this.checkBoxBands_CheckedChanged);
            // 
            // checkBoxCI
            // 
            resources.ApplyResources(this.checkBoxCI, "checkBoxCI");
            this.checkBoxCI.Name = "checkBoxCI";
            this.checkBoxCI.UseVisualStyleBackColor = true;
            this.checkBoxCI.CheckedChanged += new System.EventHandler(this.checkBoxBands_CheckedChanged);
            // 
            // checkBoxShowExplained
            // 
            resources.ApplyResources(this.checkBoxShowExplained, "checkBoxShowExplained");
            this.checkBoxShowExplained.Name = "checkBoxShowExplained";
            this.checkBoxShowExplained.UseVisualStyleBackColor = true;
            this.checkBoxShowExplained.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // checkBoxShowCount
            // 
            resources.ApplyResources(this.checkBoxShowCount, "checkBoxShowCount");
            this.checkBoxShowCount.Name = "checkBoxShowCount";
            this.checkBoxShowCount.UseVisualStyleBackColor = true;
            this.checkBoxShowCount.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // tabPageInteraction
            // 
            this.tabPageInteraction.Controls.Add(this.panelColorSelectedSeries);
            this.tabPageInteraction.Controls.Add(this.label7);
            this.tabPageInteraction.Controls.Add(this.label8);
            this.tabPageInteraction.Controls.Add(this.label13);
            this.tabPageInteraction.Controls.Add(this.checkBoxAllowCursors);
            resources.ApplyResources(this.tabPageInteraction, "tabPageInteraction");
            this.tabPageInteraction.Name = "tabPageInteraction";
            this.tabPageInteraction.UseVisualStyleBackColor = true;
            // 
            // panelColorSelectedSeries
            // 
            this.panelColorSelectedSeries.BackColor = System.Drawing.Color.Red;
            this.panelColorSelectedSeries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelColorSelectedSeries, "panelColorSelectedSeries");
            this.panelColorSelectedSeries.Name = "panelColorSelectedSeries";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label8.Name = "label8";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label13.Name = "label13";
            // 
            // checkBoxAllowCursors
            // 
            resources.ApplyResources(this.checkBoxAllowCursors, "checkBoxAllowCursors");
            this.checkBoxAllowCursors.Name = "checkBoxAllowCursors";
            this.checkBoxAllowCursors.UseVisualStyleBackColor = true;
            this.checkBoxAllowCursors.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // colorDialogMarker
            // 
            this.colorDialogMarker.Color = System.Drawing.Color.SeaGreen;
            // 
            // ScatterplotProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScatterplotProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Properties_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAppearance.ResumeLayout(false);
            this.tabPageAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrendWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMarkerWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMarkerSize)).EndInit();
            this.tabPageTrendline.ResumeLayout(false);
            this.tabPageTrendline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConfidenceLevel)).EndInit();
            this.tabPageInteraction.ResumeLayout(false);
            this.tabPageInteraction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAppearance;
        private System.Windows.Forms.TabPage tabPageTrendline;
        public System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxRegressionName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox checkBoxPI;
        private System.Windows.Forms.CheckBox checkBoxCI;
        private System.Windows.Forms.CheckBox checkBoxShowExplained;
        private System.Windows.Forms.CheckBox checkBoxShowCount;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelMarkerColor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TrackBar trackBarMarkerWidth;
        private System.Windows.Forms.TrackBar trackBarMarkerSize;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTrend;
        private System.Windows.Forms.CheckBox checkBoxShowTrend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownConfidenceLevel;
        private System.Windows.Forms.ColorDialog colorDialogMarker;
        private System.Windows.Forms.ColorDialog colorDialogTrend;
        private System.Windows.Forms.TabPage tabPageInteraction;
        private System.Windows.Forms.CheckBox checkBoxAllowCursors;
        private System.Windows.Forms.Panel panelColorSelectedSeries;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxOutliers;
        private System.Windows.Forms.Panel panelTrendColor;
        private System.Windows.Forms.TrackBar trackBarTrendWidth;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label_trend_color;
        public System.Windows.Forms.Label label4;
    }
}