namespace Mayfly.Controls
{
    partial class SettingsControlUI
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
            this.comboBoxCulture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPersonal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxCulture
            // 
            this.comboBoxCulture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCulture.DisplayMember = "NativeName";
            this.comboBoxCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulture.FormattingEnabled = true;
            this.comboBoxCulture.Location = new System.Drawing.Point(154, 36);
            this.comboBoxCulture.Name = "comboBoxCulture";
            this.comboBoxCulture.Size = new System.Drawing.Size(223, 21);
            this.comboBoxCulture.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(45, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Language";
            // 
            // labelPersonal
            // 
            this.labelPersonal.AutoSize = true;
            this.labelPersonal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPersonal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPersonal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPersonal.Location = new System.Drawing.Point(28, 0);
            this.labelPersonal.Name = "labelPersonal";
            this.labelPersonal.Size = new System.Drawing.Size(124, 15);
            this.labelPersonal.TabIndex = 0;
            this.labelPersonal.Text = "User Interface Settings";
            // 
            // SettingsControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxCulture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPersonal);
            this.Group = "Basic";
            this.Name = "SettingsControlUI";
            this.Section = "Interface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCulture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPersonal;
    }
}
