namespace Mayfly.Mathematics.Charts
{
    partial class FunctorProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctorProperties));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAppearance = new System.Windows.Forms.TabPage();
            this.panelTrendColor = new System.Windows.Forms.Panel();
            this.trackBarTrendWidth = new System.Windows.Forms.TrackBar();
            this.label23 = new System.Windows.Forms.Label();
            this.label_trend_color = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPageInteraction = new System.Windows.Forms.TabPage();
            this.panelColorUnselectedSeries = new System.Windows.Forms.Panel();
            this.panelColorSelectedSeries = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxAllowCursors = new System.Windows.Forms.CheckBox();
            this.colorDialogTrend = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPageAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrendWidth)).BeginInit();
            this.tabPageInteraction.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageAppearance);
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
            this.tabPageAppearance.Controls.Add(this.textBoxName);
            this.tabPageAppearance.Controls.Add(this.label22);
            this.tabPageAppearance.Controls.Add(this.label20);
            resources.ApplyResources(this.tabPageAppearance, "tabPageAppearance");
            this.tabPageAppearance.Name = "tabPageAppearance";
            this.tabPageAppearance.UseVisualStyleBackColor = true;
            // 
            // panelTrendColor
            // 
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
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label20.Name = "label20";
            // 
            // tabPageInteraction
            // 
            this.tabPageInteraction.Controls.Add(this.panelColorUnselectedSeries);
            this.tabPageInteraction.Controls.Add(this.panelColorSelectedSeries);
            this.tabPageInteraction.Controls.Add(this.label7);
            this.tabPageInteraction.Controls.Add(this.label8);
            this.tabPageInteraction.Controls.Add(this.label13);
            this.tabPageInteraction.Controls.Add(this.label6);
            this.tabPageInteraction.Controls.Add(this.checkBoxAllowCursors);
            resources.ApplyResources(this.tabPageInteraction, "tabPageInteraction");
            this.tabPageInteraction.Name = "tabPageInteraction";
            this.tabPageInteraction.UseVisualStyleBackColor = true;
            // 
            // panelColorUnselectedSeries
            // 
            this.panelColorUnselectedSeries.BackColor = System.Drawing.Color.Gainsboro;
            this.panelColorUnselectedSeries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelColorUnselectedSeries, "panelColorUnselectedSeries");
            this.panelColorUnselectedSeries.Name = "panelColorUnselectedSeries";
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // checkBoxAllowCursors
            // 
            resources.ApplyResources(this.checkBoxAllowCursors, "checkBoxAllowCursors");
            this.checkBoxAllowCursors.Name = "checkBoxAllowCursors";
            this.checkBoxAllowCursors.UseVisualStyleBackColor = true;
            this.checkBoxAllowCursors.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // FunctorProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FunctorProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Properties_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAppearance.ResumeLayout(false);
            this.tabPageAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTrendWidth)).EndInit();
            this.tabPageInteraction.ResumeLayout(false);
            this.tabPageInteraction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAppearance;
        public System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ColorDialog colorDialogTrend;
        private System.Windows.Forms.TabPage tabPageInteraction;
        private System.Windows.Forms.CheckBox checkBoxAllowCursors;
        private System.Windows.Forms.Panel panelColorUnselectedSeries;
        private System.Windows.Forms.Panel panelColorSelectedSeries;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelTrendColor;
        private System.Windows.Forms.TrackBar trackBarTrendWidth;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label_trend_color;
        public System.Windows.Forms.Label label4;
    }
}