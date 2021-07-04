namespace Mayfly.Mathematics.Charts
{
    partial class EvaluationProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvaluationProperties));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAppearance = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxSeriesName = new System.Windows.Forms.TextBox();
            this.panelMarkerColor = new System.Windows.Forms.Panel();
            this.labelSeriesName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_marker_color = new System.Windows.Forms.Label();
            this.checkBoxBorder = new System.Windows.Forms.CheckBox();
            this.tabPageValues = new System.Windows.Forms.TabPage();
            this.labelCL = new System.Windows.Forms.Label();
            this.numericUpDownConfidenceLevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxValueVariant = new System.Windows.Forms.ComboBox();
            this.labeDisplayValue = new System.Windows.Forms.Label();
            this.colorDialogColumn = new System.Windows.Forms.ColorDialog();
            this.tabControl.SuspendLayout();
            this.tabPageAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            this.tabPageValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConfidenceLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageAppearance);
            this.tabControl.Controls.Add(this.tabPageValues);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageAppearance
            // 
            this.tabPageAppearance.Controls.Add(this.label21);
            this.tabPageAppearance.Controls.Add(this.trackBarWidth);
            this.tabPageAppearance.Controls.Add(this.label16);
            this.tabPageAppearance.Controls.Add(this.label15);
            this.tabPageAppearance.Controls.Add(this.textBoxSeriesName);
            this.tabPageAppearance.Controls.Add(this.panelMarkerColor);
            this.tabPageAppearance.Controls.Add(this.labelSeriesName);
            this.tabPageAppearance.Controls.Add(this.label5);
            this.tabPageAppearance.Controls.Add(this.label3);
            this.tabPageAppearance.Controls.Add(this.label_marker_color);
            this.tabPageAppearance.Controls.Add(this.checkBoxBorder);
            resources.ApplyResources(this.tabPageAppearance, "tabPageAppearance");
            this.tabPageAppearance.Name = "tabPageAppearance";
            this.tabPageAppearance.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // trackBarWidth
            // 
            resources.ApplyResources(this.trackBarWidth, "trackBarWidth");
            this.trackBarWidth.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarWidth.LargeChange = 1;
            this.trackBarWidth.Minimum = 5;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Value = 8;
            this.trackBarWidth.Scroll += new System.EventHandler(this.valueChanged);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label15.Name = "label15";
            // 
            // textBoxSeriesName
            // 
            resources.ApplyResources(this.textBoxSeriesName, "textBoxSeriesName");
            this.textBoxSeriesName.Name = "textBoxSeriesName";
            this.textBoxSeriesName.TextChanged += new System.EventHandler(this.textBoxSeriesName_TextChanged);
            // 
            // panelMarkerColor
            // 
            this.panelMarkerColor.BackColor = System.Drawing.Color.NavajoWhite;
            this.panelMarkerColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelMarkerColor, "panelMarkerColor");
            this.panelMarkerColor.Name = "panelMarkerColor";
            this.panelMarkerColor.Click += new System.EventHandler(this.panelMarkerColor_Click);
            // 
            // labelSeriesName
            // 
            resources.ApplyResources(this.labelSeriesName, "labelSeriesName");
            this.labelSeriesName.Name = "labelSeriesName";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label_marker_color
            // 
            resources.ApplyResources(this.label_marker_color, "label_marker_color");
            this.label_marker_color.Name = "label_marker_color";
            // 
            // checkBoxBorder
            // 
            resources.ApplyResources(this.checkBoxBorder, "checkBoxBorder");
            this.checkBoxBorder.Checked = true;
            this.checkBoxBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBorder.Name = "checkBoxBorder";
            this.checkBoxBorder.UseVisualStyleBackColor = true;
            this.checkBoxBorder.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // tabPageValues
            // 
            this.tabPageValues.Controls.Add(this.labelCL);
            this.tabPageValues.Controls.Add(this.numericUpDownConfidenceLevel);
            this.tabPageValues.Controls.Add(this.label1);
            this.tabPageValues.Controls.Add(this.comboBoxValueVariant);
            this.tabPageValues.Controls.Add(this.labeDisplayValue);
            resources.ApplyResources(this.tabPageValues, "tabPageValues");
            this.tabPageValues.Name = "tabPageValues";
            this.tabPageValues.UseVisualStyleBackColor = true;
            // 
            // labelCL
            // 
            resources.ApplyResources(this.labelCL, "labelCL");
            this.labelCL.Name = "labelCL";
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // comboBoxValueVariant
            // 
            resources.ApplyResources(this.comboBoxValueVariant, "comboBoxValueVariant");
            this.comboBoxValueVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxValueVariant.FormattingEnabled = true;
            this.comboBoxValueVariant.Items.AddRange(new object[] {
            resources.GetString("comboBoxValueVariant.Items"),
            resources.GetString("comboBoxValueVariant.Items1"),
            resources.GetString("comboBoxValueVariant.Items2")});
            this.comboBoxValueVariant.Name = "comboBoxValueVariant";
            this.comboBoxValueVariant.SelectedIndexChanged += new System.EventHandler(this.comboBoxValueVariant_SelectedIndexChanged);
            // 
            // labeDisplayValue
            // 
            resources.ApplyResources(this.labeDisplayValue, "labeDisplayValue");
            this.labeDisplayValue.Name = "labeDisplayValue";
            // 
            // colorDialogColumn
            // 
            this.colorDialogColumn.Color = System.Drawing.Color.NavajoWhite;
            // 
            // EvaluationProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "EvaluationProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HistogramProperties_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPageAppearance.ResumeLayout(false);
            this.tabPageAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            this.tabPageValues.ResumeLayout(false);
            this.tabPageValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConfidenceLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControl;
        public System.Windows.Forms.TabPage tabPageAppearance;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxSeriesName;
        private System.Windows.Forms.Panel panelMarkerColor;
        private System.Windows.Forms.Label labelSeriesName;
        private System.Windows.Forms.Label label_marker_color;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.ColorDialog colorDialogColumn;
        private System.Windows.Forms.CheckBox checkBoxBorder;
        private System.Windows.Forms.TabPage tabPageValues;
        private System.Windows.Forms.Label labeDisplayValue;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxValueVariant;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCL;
        private System.Windows.Forms.NumericUpDown numericUpDownConfidenceLevel;
    }
}