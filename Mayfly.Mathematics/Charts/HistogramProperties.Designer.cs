namespace Mayfly.Mathematics.Charts
{
    partial class HistogramProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistogramProperties));
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonUpper = new System.Windows.Forms.RadioButton();
            this.radioButtonLower = new System.Windows.Forms.RadioButton();
            this.pictureBoxUpper = new System.Windows.Forms.PictureBox();
            this.pictureBoxLower = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPageFitDistribution = new System.Windows.Forms.TabPage();
            this.labelEquation = new System.Windows.Forms.Label();
            this.checkBoxShowFitResult = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCount = new System.Windows.Forms.CheckBox();
            this.checkBoxShowFit = new System.Windows.Forms.CheckBox();
            this.comboBoxFit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelFitColor = new System.Windows.Forms.Panel();
            this.trackBarFitWidth = new System.Windows.Forms.TrackBar();
            this.labelFitWidth = new System.Windows.Forms.Label();
            this.labelFitColor = new System.Windows.Forms.Label();
            this.colorDialogColumn = new System.Windows.Forms.ColorDialog();
            this.colorDialogFit = new System.Windows.Forms.ColorDialog();
            this.tabControl.SuspendLayout();
            this.tabPageAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            this.tabPageValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLower)).BeginInit();
            this.tabPageFitDistribution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFitWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageAppearance);
            this.tabControl.Controls.Add(this.tabPageValues);
            this.tabControl.Controls.Add(this.tabPageFitDistribution);
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
            this.panelMarkerColor.BackColor = System.Drawing.Color.OliveDrab;
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
            this.tabPageValues.Controls.Add(this.label1);
            this.tabPageValues.Controls.Add(this.radioButtonUpper);
            this.tabPageValues.Controls.Add(this.radioButtonLower);
            this.tabPageValues.Controls.Add(this.pictureBoxUpper);
            this.tabPageValues.Controls.Add(this.pictureBoxLower);
            this.tabPageValues.Controls.Add(this.label14);
            resources.ApplyResources(this.tabPageValues, "tabPageValues");
            this.tabPageValues.Name = "tabPageValues";
            this.tabPageValues.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // radioButtonUpper
            // 
            resources.ApplyResources(this.radioButtonUpper, "radioButtonUpper");
            this.radioButtonUpper.Name = "radioButtonUpper";
            this.radioButtonUpper.UseVisualStyleBackColor = true;
            this.radioButtonUpper.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // radioButtonLower
            // 
            resources.ApplyResources(this.radioButtonLower, "radioButtonLower");
            this.radioButtonLower.Checked = true;
            this.radioButtonLower.Name = "radioButtonLower";
            this.radioButtonLower.TabStop = true;
            this.radioButtonLower.UseVisualStyleBackColor = true;
            this.radioButtonLower.CheckedChanged += new System.EventHandler(this.structureChanged);
            // 
            // pictureBoxUpper
            // 
            resources.ApplyResources(this.pictureBoxUpper, "pictureBoxUpper");
            this.pictureBoxUpper.Image = global::Mayfly.Mathematics.Properties.Resources.Upper;
            this.pictureBoxUpper.Name = "pictureBoxUpper";
            this.pictureBoxUpper.TabStop = false;
            // 
            // pictureBoxLower
            // 
            this.pictureBoxLower.Image = global::Mayfly.Mathematics.Properties.Resources.Lower;
            resources.ApplyResources(this.pictureBoxLower, "pictureBoxLower");
            this.pictureBoxLower.Name = "pictureBoxLower";
            this.pictureBoxLower.TabStop = false;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // tabPageFitDistribution
            // 
            this.tabPageFitDistribution.Controls.Add(this.labelEquation);
            this.tabPageFitDistribution.Controls.Add(this.checkBoxShowFitResult);
            this.tabPageFitDistribution.Controls.Add(this.checkBoxShowCount);
            this.tabPageFitDistribution.Controls.Add(this.checkBoxShowFit);
            this.tabPageFitDistribution.Controls.Add(this.comboBoxFit);
            this.tabPageFitDistribution.Controls.Add(this.label2);
            this.tabPageFitDistribution.Controls.Add(this.label4);
            this.tabPageFitDistribution.Controls.Add(this.panelFitColor);
            this.tabPageFitDistribution.Controls.Add(this.trackBarFitWidth);
            this.tabPageFitDistribution.Controls.Add(this.labelFitWidth);
            this.tabPageFitDistribution.Controls.Add(this.labelFitColor);
            resources.ApplyResources(this.tabPageFitDistribution, "tabPageFitDistribution");
            this.tabPageFitDistribution.Name = "tabPageFitDistribution";
            this.tabPageFitDistribution.UseVisualStyleBackColor = true;
            // 
            // labelEquation
            // 
            resources.ApplyResources(this.labelEquation, "labelEquation");
            this.labelEquation.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelEquation.Name = "labelEquation";
            // 
            // checkBoxShowFitResult
            // 
            resources.ApplyResources(this.checkBoxShowFitResult, "checkBoxShowFitResult");
            this.checkBoxShowFitResult.Name = "checkBoxShowFitResult";
            this.checkBoxShowFitResult.UseVisualStyleBackColor = true;
            this.checkBoxShowFitResult.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // checkBoxShowCount
            // 
            resources.ApplyResources(this.checkBoxShowCount, "checkBoxShowCount");
            this.checkBoxShowCount.Name = "checkBoxShowCount";
            this.checkBoxShowCount.UseVisualStyleBackColor = true;
            this.checkBoxShowCount.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // checkBoxShowFit
            // 
            resources.ApplyResources(this.checkBoxShowFit, "checkBoxShowFit");
            this.checkBoxShowFit.Name = "checkBoxShowFit";
            this.checkBoxShowFit.UseVisualStyleBackColor = true;
            this.checkBoxShowFit.CheckedChanged += new System.EventHandler(this.checkBoxShowFit_CheckedChanged);
            // 
            // comboBoxFit
            // 
            resources.ApplyResources(this.comboBoxFit, "comboBoxFit");
            this.comboBoxFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFit.FormattingEnabled = true;
            this.comboBoxFit.Items.AddRange(new object[] {
            resources.GetString("comboBoxFit.Items"),
            resources.GetString("comboBoxFit.Items1"),
            resources.GetString("comboBoxFit.Items2"),
            resources.GetString("comboBoxFit.Items3"),
            resources.GetString("comboBoxFit.Items4")});
            this.comboBoxFit.Name = "comboBoxFit";
            this.comboBoxFit.SelectedIndexChanged += new System.EventHandler(this.checkBoxShowFit_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Name = "label4";
            // 
            // panelFitColor
            // 
            this.panelFitColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panelFitColor, "panelFitColor");
            this.panelFitColor.Name = "panelFitColor";
            this.panelFitColor.Click += new System.EventHandler(this.panelFitColor_Click);
            // 
            // trackBarFitWidth
            // 
            resources.ApplyResources(this.trackBarFitWidth, "trackBarFitWidth");
            this.trackBarFitWidth.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarFitWidth.LargeChange = 1;
            this.trackBarFitWidth.Maximum = 5;
            this.trackBarFitWidth.Minimum = 1;
            this.trackBarFitWidth.Name = "trackBarFitWidth";
            this.trackBarFitWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarFitWidth.Value = 2;
            this.trackBarFitWidth.Scroll += new System.EventHandler(this.valueChanged);
            // 
            // labelFitWidth
            // 
            resources.ApplyResources(this.labelFitWidth, "labelFitWidth");
            this.labelFitWidth.Name = "labelFitWidth";
            // 
            // labelFitColor
            // 
            resources.ApplyResources(this.labelFitColor, "labelFitColor");
            this.labelFitColor.Name = "labelFitColor";
            // 
            // colorDialogColumn
            // 
            this.colorDialogColumn.Color = System.Drawing.Color.Coral;
            // 
            // colorDialogFit
            // 
            this.colorDialogFit.Color = System.Drawing.Color.Tomato;
            // 
            // HistogramProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "HistogramProperties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_Closing);
            this.tabControl.ResumeLayout(false);
            this.tabPageAppearance.ResumeLayout(false);
            this.tabPageAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            this.tabPageValues.ResumeLayout(false);
            this.tabPageValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLower)).EndInit();
            this.tabPageFitDistribution.ResumeLayout(false);
            this.tabPageFitDistribution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFitWidth)).EndInit();
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
        private System.Windows.Forms.TabPage tabPageFitDistribution;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxBorder;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelFitColor;
        private System.Windows.Forms.TrackBar trackBarFitWidth;
        private System.Windows.Forms.Label labelFitWidth;
        private System.Windows.Forms.Label labelFitColor;
        private System.Windows.Forms.ColorDialog colorDialogFit;
        private System.Windows.Forms.TabPage tabPageValues;
        private System.Windows.Forms.RadioButton radioButtonUpper;
        private System.Windows.Forms.RadioButton radioButtonLower;
        private System.Windows.Forms.PictureBox pictureBoxUpper;
        private System.Windows.Forms.PictureBox pictureBoxLower;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFit;
        private System.Windows.Forms.CheckBox checkBoxShowFit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label labelEquation;
        private System.Windows.Forms.CheckBox checkBoxShowCount;
        private System.Windows.Forms.CheckBox checkBoxShowFitResult;
    }
}