﻿namespace Mayfly.Mathematics
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.comboBoxRegressionType = new System.Windows.Forms.ComboBox();
            this.labelRegressionType = new System.Windows.Forms.Label();
            this.numericUpDownSL = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSoftSize = new System.Windows.Forms.NumericUpDown();
            this.labelSL = new System.Windows.Forms.Label();
            this.numericUpDownStrongSize = new System.Windows.Forms.NumericUpDown();
            this.labelSoftSize = new System.Windows.Forms.Label();
            this.labelRegression = new System.Windows.Forms.Label();
            this.labelExpect = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelStrongSize = new System.Windows.Forms.Label();
            this.tabPageMethods = new System.Windows.Forms.TabPage();
            this.comboBoxNormality = new System.Windows.Forms.ComboBox();
            this.labelNormality = new System.Windows.Forms.Label();
            this.comboBoxLSD = new System.Windows.Forms.ComboBox();
            this.labelLSD = new System.Windows.Forms.Label();
            this.comboBoxHomogeneity = new System.Windows.Forms.ComboBox();
            this.labelHomo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTest = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabControlSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSoftSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrongSize)).BeginInit();
            this.tabPageMethods.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageGeneral);
            this.tabControlSettings.Controls.Add(this.tabPageMethods);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.comboBoxRegressionType);
            this.tabPageGeneral.Controls.Add(this.labelRegressionType);
            this.tabPageGeneral.Controls.Add(this.numericUpDownSL);
            this.tabPageGeneral.Controls.Add(this.numericUpDownSoftSize);
            this.tabPageGeneral.Controls.Add(this.labelSL);
            this.tabPageGeneral.Controls.Add(this.numericUpDownStrongSize);
            this.tabPageGeneral.Controls.Add(this.labelSoftSize);
            this.tabPageGeneral.Controls.Add(this.labelRegression);
            this.tabPageGeneral.Controls.Add(this.labelExpect);
            this.tabPageGeneral.Controls.Add(this.labelSize);
            this.tabPageGeneral.Controls.Add(this.labelStrongSize);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // comboBoxRegressionType
            // 
            resources.ApplyResources(this.comboBoxRegressionType, "comboBoxRegressionType");
            this.comboBoxRegressionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegressionType.Name = "comboBoxRegressionType";
            // 
            // labelRegressionType
            // 
            resources.ApplyResources(this.labelRegressionType, "labelRegressionType");
            this.labelRegressionType.Name = "labelRegressionType";
            // 
            // numericUpDownSL
            // 
            resources.ApplyResources(this.numericUpDownSL, "numericUpDownSL");
            this.numericUpDownSL.DecimalPlaces = 3;
            this.numericUpDownSL.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownSL.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            196608});
            this.numericUpDownSL.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownSL.Name = "numericUpDownSL";
            this.numericUpDownSL.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // numericUpDownSoftSize
            // 
            resources.ApplyResources(this.numericUpDownSoftSize, "numericUpDownSoftSize");
            this.numericUpDownSoftSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSoftSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownSoftSize.Name = "numericUpDownSoftSize";
            this.numericUpDownSoftSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // labelSL
            // 
            resources.ApplyResources(this.labelSL, "labelSL");
            this.labelSL.Name = "labelSL";
            // 
            // numericUpDownStrongSize
            // 
            resources.ApplyResources(this.numericUpDownStrongSize, "numericUpDownStrongSize");
            this.numericUpDownStrongSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStrongSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownStrongSize.Name = "numericUpDownStrongSize";
            this.numericUpDownStrongSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // labelSoftSize
            // 
            resources.ApplyResources(this.labelSoftSize, "labelSoftSize");
            this.labelSoftSize.Name = "labelSoftSize";
            // 
            // labelRegression
            // 
            resources.ApplyResources(this.labelRegression, "labelRegression");
            this.labelRegression.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelRegression.Name = "labelRegression";
            // 
            // labelExpect
            // 
            resources.ApplyResources(this.labelExpect, "labelExpect");
            this.labelExpect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelExpect.Name = "labelExpect";
            // 
            // labelSize
            // 
            resources.ApplyResources(this.labelSize, "labelSize");
            this.labelSize.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelSize.Name = "labelSize";
            // 
            // labelStrongSize
            // 
            resources.ApplyResources(this.labelStrongSize, "labelStrongSize");
            this.labelStrongSize.Name = "labelStrongSize";
            // 
            // tabPageMethods
            // 
            this.tabPageMethods.Controls.Add(this.comboBoxNormality);
            this.tabPageMethods.Controls.Add(this.labelNormality);
            this.tabPageMethods.Controls.Add(this.comboBoxLSD);
            this.tabPageMethods.Controls.Add(this.labelLSD);
            this.tabPageMethods.Controls.Add(this.comboBoxHomogeneity);
            this.tabPageMethods.Controls.Add(this.labelHomo);
            this.tabPageMethods.Controls.Add(this.label1);
            this.tabPageMethods.Controls.Add(this.labelTest);
            resources.ApplyResources(this.tabPageMethods, "tabPageMethods");
            this.tabPageMethods.Name = "tabPageMethods";
            this.tabPageMethods.UseVisualStyleBackColor = true;
            // 
            // comboBoxNormality
            // 
            resources.ApplyResources(this.comboBoxNormality, "comboBoxNormality");
            this.comboBoxNormality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNormality.FormattingEnabled = true;
            this.comboBoxNormality.Items.AddRange(new object[] {
            resources.GetString("comboBoxNormality.Items")});
            this.comboBoxNormality.Name = "comboBoxNormality";
            // 
            // labelNormality
            // 
            resources.ApplyResources(this.labelNormality, "labelNormality");
            this.labelNormality.Name = "labelNormality";
            // 
            // comboBoxLSD
            // 
            resources.ApplyResources(this.comboBoxLSD, "comboBoxLSD");
            this.comboBoxLSD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLSD.FormattingEnabled = true;
            this.comboBoxLSD.Items.AddRange(new object[] {
            resources.GetString("comboBoxLSD.Items"),
            resources.GetString("comboBoxLSD.Items1")});
            this.comboBoxLSD.Name = "comboBoxLSD";
            // 
            // labelLSD
            // 
            resources.ApplyResources(this.labelLSD, "labelLSD");
            this.labelLSD.Name = "labelLSD";
            // 
            // comboBoxHomogeneity
            // 
            resources.ApplyResources(this.comboBoxHomogeneity, "comboBoxHomogeneity");
            this.comboBoxHomogeneity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHomogeneity.FormattingEnabled = true;
            this.comboBoxHomogeneity.Items.AddRange(new object[] {
            resources.GetString("comboBoxHomogeneity.Items"),
            resources.GetString("comboBoxHomogeneity.Items1"),
            resources.GetString("comboBoxHomogeneity.Items2"),
            resources.GetString("comboBoxHomogeneity.Items3")});
            this.comboBoxHomogeneity.Name = "comboBoxHomogeneity";
            // 
            // labelHomo
            // 
            resources.ApplyResources(this.labelHomo, "labelHomo");
            this.labelHomo.Name = "labelHomo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // labelTest
            // 
            resources.ApplyResources(this.labelTest, "labelTest");
            this.labelTest.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTest.Name = "labelTest";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSoftSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrongSize)).EndInit();
            this.tabPageMethods.ResumeLayout(false);
            this.tabPageMethods.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.NumericUpDown numericUpDownSL;
        private System.Windows.Forms.NumericUpDown numericUpDownSoftSize;
        private System.Windows.Forms.Label labelSL;
        private System.Windows.Forms.NumericUpDown numericUpDownStrongSize;
        private System.Windows.Forms.Label labelSoftSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelStrongSize;
        private System.Windows.Forms.TabPage tabPageMethods;
        private System.Windows.Forms.ComboBox comboBoxNormality;
        private System.Windows.Forms.Label labelNormality;
        private System.Windows.Forms.ComboBox comboBoxLSD;
        private System.Windows.Forms.Label labelLSD;
        private System.Windows.Forms.ComboBox comboBoxHomogeneity;
        private System.Windows.Forms.Label labelHomo;
        private System.Windows.Forms.Label labelTest;
        private System.Windows.Forms.Label labelRegression;
        private System.Windows.Forms.Label labelExpect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRegressionType;
        private System.Windows.Forms.ComboBox comboBoxRegressionType;
    }
}