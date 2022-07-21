namespace Mayfly.Species.Controls
{
    partial class SettingsControlTree
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
            this.checkBoxFillLower = new System.Windows.Forms.CheckBox();
            this.textBoxHigherFormat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLowerFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLowerTaxon = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.dialogLowerTaxon = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // checkBoxFillLower
            // 
            this.checkBoxFillLower.AutoSize = true;
            this.checkBoxFillLower.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxFillLower.Location = new System.Drawing.Point(47, 120);
            this.checkBoxFillLower.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.checkBoxFillLower.Name = "checkBoxFillLower";
            this.checkBoxFillLower.Size = new System.Drawing.Size(132, 17);
            this.checkBoxFillLower.TabIndex = 15;
            this.checkBoxFillLower.Text = "Fill tree with lower taxa";
            this.checkBoxFillLower.UseVisualStyleBackColor = true;
            // 
            // textBoxHigherFormat
            // 
            this.textBoxHigherFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHigherFormat.Location = new System.Drawing.Point(277, 34);
            this.textBoxHigherFormat.Name = "textBoxHigherFormat";
            this.textBoxHigherFormat.Size = new System.Drawing.Size(100, 20);
            this.textBoxHigherFormat.TabIndex = 10;
            this.textBoxHigherFormat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(48, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Higher taxon name format";
            // 
            // textBoxLowerFormat
            // 
            this.textBoxLowerFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLowerFormat.Location = new System.Drawing.Point(277, 60);
            this.textBoxLowerFormat.Name = "textBoxLowerFormat";
            this.textBoxLowerFormat.Size = new System.Drawing.Size(100, 20);
            this.textBoxLowerFormat.TabIndex = 12;
            this.textBoxLowerFormat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(48, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Lower taxon name format";
            // 
            // panelLowerTaxon
            // 
            this.panelLowerTaxon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLowerTaxon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLowerTaxon.Location = new System.Drawing.Point(327, 86);
            this.panelLowerTaxon.Name = "panelLowerTaxon";
            this.panelLowerTaxon.Size = new System.Drawing.Size(50, 20);
            this.panelLowerTaxon.TabIndex = 14;
            this.panelLowerTaxon.Click += new System.EventHandler(this.panelLowerTaxon_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(48, 92);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 13);
            this.label19.TabIndex = 13;
            this.label19.Text = "Lower taxon name color";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label26.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(28, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(152, 15);
            this.label26.TabIndex = 8;
            this.label26.Text = "Taxonomic tree appearance";
            // 
            // dialogLowerTaxon
            // 
            this.dialogLowerTaxon.AnyColor = true;
            this.dialogLowerTaxon.FullOpen = true;
            // 
            // SettingsControlTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxFillLower);
            this.Controls.Add(this.textBoxHigherFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLowerFormat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelLowerTaxon);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label26);
            this.Name = "SettingsControlTree";
            this.Size = new System.Drawing.Size(400, 193);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxFillLower;
        private System.Windows.Forms.TextBox textBoxHigherFormat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLowerFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLowerTaxon;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ColorDialog dialogLowerTaxon;
    }
}
