namespace Mayfly.Controls
{
    partial class ColumnProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnProperties));
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.labelHeader = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxDateCollapse = new System.Windows.Forms.CheckBox();
            this.comboBoxDateFormatOption = new System.Windows.Forms.ComboBox();
            this.comboBoxDateFormat = new System.Windows.Forms.ComboBox();
            this.labelDecimal = new System.Windows.Forms.Label();
            this.labelDatePart = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.labelLayout = new System.Windows.Forms.Label();
            this.labelVertical = new System.Windows.Forms.Label();
            this.comboBoxVertical = new System.Windows.Forms.ComboBox();
            this.labelHorizontal = new System.Windows.Forms.Label();
            this.comboBoxHorizontal = new System.Windows.Forms.ComboBox();
            this.numericUpDownDecimals = new System.Windows.Forms.NumericUpDown();
            this.checkBoxPercentage = new System.Windows.Forms.CheckBox();
            this.textBoxFormat = new System.Windows.Forms.TextBox();
            this.labelFormat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimals)).BeginInit();
            this.SuspendLayout();
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label15.Name = "label15";
            // 
            // textBoxHeader
            // 
            resources.ApplyResources(this.textBoxHeader, "textBoxHeader");
            this.textBoxHeader.Name = "textBoxHeader";
            // 
            // labelHeader
            // 
            resources.ApplyResources(this.labelHeader, "labelHeader");
            this.labelHeader.Name = "labelHeader";
            // 
            // comboBoxType
            // 
            resources.ApplyResources(this.comboBoxType, "comboBoxType");
            this.comboBoxType.DisplayMember = "Name";
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            resources.GetString("comboBoxType.Items"),
            resources.GetString("comboBoxType.Items1"),
            resources.GetString("comboBoxType.Items2"),
            resources.GetString("comboBoxType.Items3"),
            resources.GetString("comboBoxType.Items4")});
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // labelType
            // 
            resources.ApplyResources(this.labelType, "labelType");
            this.labelType.Name = "labelType";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxDateCollapse
            // 
            resources.ApplyResources(this.checkBoxDateCollapse, "checkBoxDateCollapse");
            this.checkBoxDateCollapse.Name = "checkBoxDateCollapse";
            this.checkBoxDateCollapse.UseVisualStyleBackColor = true;
            this.checkBoxDateCollapse.CheckedChanged += new System.EventHandler(this.comboBoxDateFormat_SelectedIndexChanged);
            // 
            // comboBoxDateFormatOption
            // 
            resources.ApplyResources(this.comboBoxDateFormatOption, "comboBoxDateFormatOption");
            this.comboBoxDateFormatOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDateFormatOption.FormattingEnabled = true;
            this.comboBoxDateFormatOption.Items.AddRange(new object[] {
            resources.GetString("comboBoxDateFormatOption.Items"),
            resources.GetString("comboBoxDateFormatOption.Items1"),
            resources.GetString("comboBoxDateFormatOption.Items2")});
            this.comboBoxDateFormatOption.Name = "comboBoxDateFormatOption";
            this.comboBoxDateFormatOption.SelectedIndexChanged += new System.EventHandler(this.comboBoxDateFormatOption_SelectedIndexChanged);
            this.comboBoxDateFormatOption.VisibleChanged += new System.EventHandler(this.comboBoxDateFormatOption_VisibleChanged);
            // 
            // comboBoxDateFormat
            // 
            resources.ApplyResources(this.comboBoxDateFormat, "comboBoxDateFormat");
            this.comboBoxDateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDateFormat.FormattingEnabled = true;
            this.comboBoxDateFormat.Items.AddRange(new object[] {
            resources.GetString("comboBoxDateFormat.Items"),
            resources.GetString("comboBoxDateFormat.Items1"),
            resources.GetString("comboBoxDateFormat.Items2"),
            resources.GetString("comboBoxDateFormat.Items3"),
            resources.GetString("comboBoxDateFormat.Items4"),
            resources.GetString("comboBoxDateFormat.Items5"),
            resources.GetString("comboBoxDateFormat.Items6"),
            resources.GetString("comboBoxDateFormat.Items7"),
            resources.GetString("comboBoxDateFormat.Items8"),
            resources.GetString("comboBoxDateFormat.Items9"),
            resources.GetString("comboBoxDateFormat.Items10"),
            resources.GetString("comboBoxDateFormat.Items11")});
            this.comboBoxDateFormat.Name = "comboBoxDateFormat";
            this.comboBoxDateFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxDateFormat_SelectedIndexChanged);
            // 
            // labelDecimal
            // 
            resources.ApplyResources(this.labelDecimal, "labelDecimal");
            this.labelDecimal.Name = "labelDecimal";
            // 
            // labelDatePart
            // 
            resources.ApplyResources(this.labelDatePart, "labelDatePart");
            this.labelDatePart.Name = "labelDatePart";
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // labelLayout
            // 
            resources.ApplyResources(this.labelLayout, "labelLayout");
            this.labelLayout.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelLayout.Name = "labelLayout";
            // 
            // labelVertical
            // 
            resources.ApplyResources(this.labelVertical, "labelVertical");
            this.labelVertical.Name = "labelVertical";
            // 
            // comboBoxVertical
            // 
            resources.ApplyResources(this.comboBoxVertical, "comboBoxVertical");
            this.comboBoxVertical.DisplayMember = "Name";
            this.comboBoxVertical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVertical.FormattingEnabled = true;
            this.comboBoxVertical.Items.AddRange(new object[] {
            resources.GetString("comboBoxVertical.Items"),
            resources.GetString("comboBoxVertical.Items1"),
            resources.GetString("comboBoxVertical.Items2")});
            this.comboBoxVertical.Name = "comboBoxVertical";
            this.comboBoxVertical.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // labelHorizontal
            // 
            resources.ApplyResources(this.labelHorizontal, "labelHorizontal");
            this.labelHorizontal.Name = "labelHorizontal";
            // 
            // comboBoxHorizontal
            // 
            resources.ApplyResources(this.comboBoxHorizontal, "comboBoxHorizontal");
            this.comboBoxHorizontal.DisplayMember = "Name";
            this.comboBoxHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHorizontal.FormattingEnabled = true;
            this.comboBoxHorizontal.Items.AddRange(new object[] {
            resources.GetString("comboBoxHorizontal.Items"),
            resources.GetString("comboBoxHorizontal.Items1"),
            resources.GetString("comboBoxHorizontal.Items2")});
            this.comboBoxHorizontal.Name = "comboBoxHorizontal";
            this.comboBoxHorizontal.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // numericUpDownDecimals
            // 
            resources.ApplyResources(this.numericUpDownDecimals, "numericUpDownDecimals");
            this.numericUpDownDecimals.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownDecimals.Name = "numericUpDownDecimals";
            this.numericUpDownDecimals.ValueChanged += new System.EventHandler(this.numericUpDownDecimals_ValueChanged);
            // 
            // checkBoxPercentage
            // 
            resources.ApplyResources(this.checkBoxPercentage, "checkBoxPercentage");
            this.checkBoxPercentage.Name = "checkBoxPercentage";
            this.checkBoxPercentage.UseVisualStyleBackColor = true;
            this.checkBoxPercentage.CheckedChanged += new System.EventHandler(this.comboBoxDateFormat_SelectedIndexChanged);
            // 
            // textBoxFormat
            // 
            resources.ApplyResources(this.textBoxFormat, "textBoxFormat");
            this.textBoxFormat.Name = "textBoxFormat";
            this.textBoxFormat.ReadOnly = true;
            // 
            // labelFormat
            // 
            resources.ApplyResources(this.labelFormat, "labelFormat");
            this.labelFormat.Name = "labelFormat";
            // 
            // ColumnProperties
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.numericUpDownDecimals);
            this.Controls.Add(this.checkBoxPercentage);
            this.Controls.Add(this.checkBoxDateCollapse);
            this.Controls.Add(this.labelDecimal);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.labelDatePart);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxHorizontal);
            this.Controls.Add(this.comboBoxVertical);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.labelLayout);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.labelHorizontal);
            this.Controls.Add(this.textBoxFormat);
            this.Controls.Add(this.textBoxHeader);
            this.Controls.Add(this.labelVertical);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.comboBoxDateFormatOption);
            this.Controls.Add(this.comboBoxDateFormat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnProperties";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxHeader;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkBoxDateCollapse;
        private System.Windows.Forms.ComboBox comboBoxDateFormatOption;
        private System.Windows.Forms.ComboBox comboBoxDateFormat;
        private System.Windows.Forms.Label labelDecimal;
        private System.Windows.Forms.Label labelDatePart;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Label labelLayout;
        private System.Windows.Forms.Label labelVertical;
        private System.Windows.Forms.ComboBox comboBoxVertical;
        private System.Windows.Forms.Label labelHorizontal;
        private System.Windows.Forms.ComboBox comboBoxHorizontal;
        private System.Windows.Forms.NumericUpDown numericUpDownDecimals;
        private System.Windows.Forms.CheckBox checkBoxPercentage;
        private System.Windows.Forms.TextBox textBoxFormat;
        private System.Windows.Forms.Label labelFormat;
    }
}