namespace Mayfly.Mathematics.Controls
{
    partial class SettingsPageTests
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
            this.comboBoxNormality = new System.Windows.Forms.ComboBox();
            this.labelNormality = new System.Windows.Forms.Label();
            this.comboBoxLSD = new System.Windows.Forms.ComboBox();
            this.labelLSD = new System.Windows.Forms.Label();
            this.comboBoxHomogeneity = new System.Windows.Forms.ComboBox();
            this.labelHomo = new System.Windows.Forms.Label();
            this.labelTest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxNormality
            // 
            this.comboBoxNormality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNormality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNormality.FormattingEnabled = true;
            this.comboBoxNormality.Items.AddRange(new object[] {
            "Kolmogorov-Smirnov test"});
            this.comboBoxNormality.Location = new System.Drawing.Point(177, 37);
            this.comboBoxNormality.Name = "comboBoxNormality";
            this.comboBoxNormality.Size = new System.Drawing.Size(200, 21);
            this.comboBoxNormality.TabIndex = 2;
            // 
            // labelNormality
            // 
            this.labelNormality.AutoSize = true;
            this.labelNormality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNormality.Location = new System.Drawing.Point(42, 40);
            this.labelNormality.Name = "labelNormality";
            this.labelNormality.Size = new System.Drawing.Size(50, 13);
            this.labelNormality.TabIndex = 1;
            this.labelNormality.Text = "Normality";
            // 
            // comboBoxLSD
            // 
            this.comboBoxLSD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLSD.FormattingEnabled = true;
            this.comboBoxLSD.Items.AddRange(new object[] {
            "Least significant difference (LSD) test",
            "Tukey\'s honest significant difference (HSD) test"});
            this.comboBoxLSD.Location = new System.Drawing.Point(177, 91);
            this.comboBoxLSD.Name = "comboBoxLSD";
            this.comboBoxLSD.Size = new System.Drawing.Size(200, 21);
            this.comboBoxLSD.TabIndex = 6;
            // 
            // labelLSD
            // 
            this.labelLSD.AutoSize = true;
            this.labelLSD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelLSD.Location = new System.Drawing.Point(42, 94);
            this.labelLSD.Name = "labelLSD";
            this.labelLSD.Size = new System.Drawing.Size(133, 13);
            this.labelLSD.TabIndex = 5;
            this.labelLSD.Text = "Least significant difference";
            // 
            // comboBoxHomogeneity
            // 
            this.comboBoxHomogeneity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxHomogeneity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHomogeneity.FormattingEnabled = true;
            this.comboBoxHomogeneity.Items.AddRange(new object[] {
            "Bartlett\'s test (Bartlett, 1937)",
            "F(max) test (Hartley, 1950)",
            "Levene\'s test (Levene and Howard, 1960)",
            "Brown–Forsythe test (Brown and Forsythe, 1974)"});
            this.comboBoxHomogeneity.Location = new System.Drawing.Point(177, 64);
            this.comboBoxHomogeneity.Name = "comboBoxHomogeneity";
            this.comboBoxHomogeneity.Size = new System.Drawing.Size(200, 21);
            this.comboBoxHomogeneity.TabIndex = 4;
            // 
            // labelHomo
            // 
            this.labelHomo.AutoSize = true;
            this.labelHomo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelHomo.Location = new System.Drawing.Point(42, 67);
            this.labelHomo.Name = "labelHomo";
            this.labelHomo.Size = new System.Drawing.Size(69, 13);
            this.labelHomo.TabIndex = 3;
            this.labelHomo.Text = "Homogeneity";
            // 
            // labelTest
            // 
            this.labelTest.AutoSize = true;
            this.labelTest.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelTest.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTest.Location = new System.Drawing.Point(25, 0);
            this.labelTest.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.labelTest.Name = "labelTest";
            this.labelTest.Size = new System.Drawing.Size(129, 15);
            this.labelTest.TabIndex = 0;
            this.labelTest.Text = "Default test procedures";
            // 
            // SettingsControlTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxNormality);
            this.Controls.Add(this.labelNormality);
            this.Controls.Add(this.comboBoxLSD);
            this.Controls.Add(this.labelLSD);
            this.Controls.Add(this.comboBoxHomogeneity);
            this.Controls.Add(this.labelHomo);
            this.Controls.Add(this.labelTest);
            this.Group = "Statistics";
            this.Name = "SettingsControlTests";
            this.Section = "Tests";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxNormality;
        private System.Windows.Forms.Label labelNormality;
        private System.Windows.Forms.ComboBox comboBoxLSD;
        private System.Windows.Forms.Label labelLSD;
        private System.Windows.Forms.ComboBox comboBoxHomogeneity;
        private System.Windows.Forms.Label labelHomo;
        private System.Windows.Forms.Label labelTest;
    }
}
