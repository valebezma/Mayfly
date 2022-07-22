namespace Mayfly.Wild.Controls
{
    partial class SettingsPagePrint
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
            this.comboBoxLogOrder = new System.Windows.Forms.ComboBox();
            this.checkBoxBreakBetweenSpecies = new System.Windows.Forms.CheckBox();
            this.checkBoxOrderLog = new System.Windows.Forms.CheckBox();
            this.checkBoxCardOdd = new System.Windows.Forms.CheckBox();
            this.checkBoxBreakBeforeIndividuals = new System.Windows.Forms.CheckBox();
            this.labelPrintCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxLogOrder
            // 
            this.comboBoxLogOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLogOrder.DisplayMember = "DisplayName";
            this.comboBoxLogOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogOrder.Enabled = false;
            this.comboBoxLogOrder.FormattingEnabled = true;
            this.comboBoxLogOrder.Items.AddRange(new object[] {
            "Alphabetically",
            "By quantity",
            "By mass"});
            this.comboBoxLogOrder.Location = new System.Drawing.Point(177, 61);
            this.comboBoxLogOrder.Name = "comboBoxLogOrder";
            this.comboBoxLogOrder.Size = new System.Drawing.Size(200, 21);
            this.comboBoxLogOrder.TabIndex = 3;
            // 
            // checkBoxBreakBetweenSpecies
            // 
            this.checkBoxBreakBetweenSpecies.AutoSize = true;
            this.checkBoxBreakBetweenSpecies.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBreakBetweenSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxBreakBetweenSpecies.Location = new System.Drawing.Point(65, 106);
            this.checkBoxBreakBetweenSpecies.Name = "checkBoxBreakBetweenSpecies";
            this.checkBoxBreakBetweenSpecies.Size = new System.Drawing.Size(104, 17);
            this.checkBoxBreakBetweenSpecies.TabIndex = 5;
            this.checkBoxBreakBetweenSpecies.Text = "for each species";
            this.checkBoxBreakBetweenSpecies.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBreakBetweenSpecies.UseVisualStyleBackColor = true;
            // 
            // checkBoxOrderLog
            // 
            this.checkBoxOrderLog.AutoSize = true;
            this.checkBoxOrderLog.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxOrderLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxOrderLog.Location = new System.Drawing.Point(45, 63);
            this.checkBoxOrderLog.Name = "checkBoxOrderLog";
            this.checkBoxOrderLog.Size = new System.Drawing.Size(111, 17);
            this.checkBoxOrderLog.TabIndex = 2;
            this.checkBoxOrderLog.Text = "Sort log as printed";
            this.checkBoxOrderLog.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxOrderLog.UseVisualStyleBackColor = true;
            this.checkBoxOrderLog.CheckedChanged += new System.EventHandler(this.checkBoxOrderLog_CheckedChanged);
            // 
            // checkBoxCardOdd
            // 
            this.checkBoxCardOdd.AutoSize = true;
            this.checkBoxCardOdd.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxCardOdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxCardOdd.Location = new System.Drawing.Point(45, 40);
            this.checkBoxCardOdd.Name = "checkBoxCardOdd";
            this.checkBoxCardOdd.Size = new System.Drawing.Size(142, 17);
            this.checkBoxCardOdd.TabIndex = 1;
            this.checkBoxCardOdd.Text = "Start card with odd page";
            this.checkBoxCardOdd.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxCardOdd.UseVisualStyleBackColor = true;
            // 
            // checkBoxBreakBeforeIndividuals
            // 
            this.checkBoxBreakBeforeIndividuals.AutoSize = true;
            this.checkBoxBreakBeforeIndividuals.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBreakBeforeIndividuals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxBreakBeforeIndividuals.Location = new System.Drawing.Point(45, 86);
            this.checkBoxBreakBeforeIndividuals.Name = "checkBoxBreakBeforeIndividuals";
            this.checkBoxBreakBeforeIndividuals.Size = new System.Drawing.Size(189, 17);
            this.checkBoxBreakBeforeIndividuals.TabIndex = 4;
            this.checkBoxBreakBeforeIndividuals.Text = "Start individuals log with new page";
            this.checkBoxBreakBeforeIndividuals.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBreakBeforeIndividuals.UseVisualStyleBackColor = true;
            this.checkBoxBreakBeforeIndividuals.CheckedChanged += new System.EventHandler(this.checkBoxBreakBeforeIndividuals_CheckedChanged);
            // 
            // labelPrintCaption
            // 
            this.labelPrintCaption.AutoSize = true;
            this.labelPrintCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPrintCaption.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPrintCaption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPrintCaption.Location = new System.Drawing.Point(28, 0);
            this.labelPrintCaption.Name = "labelPrintCaption";
            this.labelPrintCaption.Size = new System.Drawing.Size(144, 15);
            this.labelPrintCaption.TabIndex = 0;
            this.labelPrintCaption.Text = "Print and Preview Settings";
            // 
            // SettingsControlPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxLogOrder);
            this.Controls.Add(this.checkBoxBreakBetweenSpecies);
            this.Controls.Add(this.checkBoxOrderLog);
            this.Controls.Add(this.checkBoxCardOdd);
            this.Controls.Add(this.checkBoxBreakBeforeIndividuals);
            this.Controls.Add(this.labelPrintCaption);
            this.Group = "Reader";
            this.Name = "SettingsControlPrint";
            this.Section = "Printing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLogOrder;
        private System.Windows.Forms.CheckBox checkBoxBreakBetweenSpecies;
        private System.Windows.Forms.CheckBox checkBoxOrderLog;
        private System.Windows.Forms.CheckBox checkBoxCardOdd;
        private System.Windows.Forms.CheckBox checkBoxBreakBeforeIndividuals;
        private System.Windows.Forms.Label labelPrintCaption;
    }
}
