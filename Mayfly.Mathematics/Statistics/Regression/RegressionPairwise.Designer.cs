namespace Mayfly.Mathematics.Statistics
{
    partial class RegressionPairwise
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
            this.spreadSheetMatrix = new Mayfly.Controls.SpreadSheet();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelPar = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadSheetMatrix
            // 
            this.spreadSheetMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetMatrix.Location = new System.Drawing.Point(28, 107);
            this.spreadSheetMatrix.Name = "spreadSheetMatrix";
            this.spreadSheetMatrix.RowHeadersWidth = 250;
            this.spreadSheetMatrix.Size = new System.Drawing.Size(578, 276);
            this.spreadSheetMatrix.TabIndex = 0;
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Slope (b)",
            "Elevation (a)"});
            this.comboBoxType.Location = new System.Drawing.Point(217, 69);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxType.TabIndex = 4;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // labelPar
            // 
            this.labelPar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPar.AutoSize = true;
            this.labelPar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPar.Location = new System.Drawing.Point(140, 72);
            this.labelPar.Name = "labelPar";
            this.labelPar.Size = new System.Drawing.Size(55, 13);
            this.labelPar.TabIndex = 3;
            this.labelPar.Text = "Parameter";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(100, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(435, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "Select parameter of linear regressions to be compared pairwise. \r\nValues of q sta" +
    "tistic will be shown in the matrix table below.";
            // 
            // RegressionPairwise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.labelPar);
            this.Controls.Add(this.spreadSheetMatrix);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegressionPairwise";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.ShowIcon = false;
            this.Text = "Regression pairwise comparison";
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMatrix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SpreadSheet spreadSheetMatrix;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelPar;
        private System.Windows.Forms.Label label4;
    }
}