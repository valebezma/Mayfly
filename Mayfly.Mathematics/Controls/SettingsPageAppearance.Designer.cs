namespace Mayfly.Mathematics.Controls
{
    partial class SettingsPageAppearance
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.labelDefaultTrendColor = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.colorPickerSelected = new Mayfly.Controls.ColorBox();
            this.colorPickerTrend = new Mayfly.Controls.ColorBox();
            this.SuspendLayout();
            // 
            // labelDefaultTrendColor
            // 
            this.labelDefaultTrendColor.AutoSize = true;
            this.labelDefaultTrendColor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDefaultTrendColor.Location = new System.Drawing.Point(45, 66);
            this.labelDefaultTrendColor.Name = "labelDefaultTrendColor";
            this.labelDefaultTrendColor.Size = new System.Drawing.Size(94, 13);
            this.labelDefaultTrendColor.TabIndex = 3;
            this.labelDefaultTrendColor.Text = "Default trend color";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(45, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Selected series color";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label26.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(25, 0);
            this.label26.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(78, 15);
            this.label26.TabIndex = 0;
            this.label26.Text = "Data coloring";
            // 
            // colorPickerSelected
            // 
            this.colorPickerSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPickerSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPickerSelected.Color = System.Drawing.Color.Empty;
            this.colorPickerSelected.Location = new System.Drawing.Point(327, 33);
            this.colorPickerSelected.Name = "colorPickerSelected";
            this.colorPickerSelected.Size = new System.Drawing.Size(50, 20);
            this.colorPickerSelected.TabIndex = 5;
            // 
            // colorPickerTrend
            // 
            this.colorPickerTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPickerTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPickerTrend.Color = System.Drawing.Color.Empty;
            this.colorPickerTrend.Location = new System.Drawing.Point(327, 59);
            this.colorPickerTrend.Name = "colorPickerTrend";
            this.colorPickerTrend.Size = new System.Drawing.Size(50, 20);
            this.colorPickerTrend.TabIndex = 5;
            // 
            // SettingsPageAppearance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorPickerTrend);
            this.Controls.Add(this.colorPickerSelected);
            this.Controls.Add(this.labelDefaultTrendColor);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label26);
            this.Group = "Statistics";
            this.Name = "SettingsPageAppearance";
            this.Section = "Appearance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDefaultTrendColor;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label26;
        private Mayfly.Controls.ColorBox colorPickerSelected;
        private Mayfly.Controls.ColorBox colorPickerTrend;
    }
}
