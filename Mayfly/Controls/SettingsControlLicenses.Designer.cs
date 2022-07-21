namespace Mayfly.Controls
{
    partial class SettingsControlLicenses
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
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.columnHeaderLicense = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpire = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelFeaturesInstruction = new System.Windows.Forms.Label();
            this.labelPersonal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewLicenses
            // 
            this.listViewLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLicenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLicense,
            this.columnHeaderExpire});
            this.listViewLicenses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLicenses.HideSelection = false;
            this.listViewLicenses.Location = new System.Drawing.Point(48, 65);
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.Size = new System.Drawing.Size(329, 174);
            this.listViewLicenses.TabIndex = 2;
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderLicense
            // 
            this.columnHeaderLicense.Text = "License";
            this.columnHeaderLicense.Width = 175;
            // 
            // columnHeaderExpire
            // 
            this.columnHeaderExpire.Text = "Expires In";
            this.columnHeaderExpire.Width = 85;
            // 
            // labelFeaturesInstruction
            // 
            this.labelFeaturesInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFeaturesInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFeaturesInstruction.Location = new System.Drawing.Point(45, 39);
            this.labelFeaturesInstruction.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.labelFeaturesInstruction.Name = "labelFeaturesInstruction";
            this.labelFeaturesInstruction.Size = new System.Drawing.Size(324, 13);
            this.labelFeaturesInstruction.TabIndex = 1;
            this.labelFeaturesInstruction.Text = "You currently have following licenses installed:";
            // 
            // labelPersonal
            // 
            this.labelPersonal.AutoSize = true;
            this.labelPersonal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPersonal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPersonal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPersonal.Location = new System.Drawing.Point(28, 0);
            this.labelPersonal.Name = "labelPersonal";
            this.labelPersonal.Size = new System.Drawing.Size(98, 15);
            this.labelPersonal.TabIndex = 0;
            this.labelPersonal.Text = "Licenses Installed";
            // 
            // SettingsControlLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewLicenses);
            this.Controls.Add(this.labelFeaturesInstruction);
            this.Controls.Add(this.labelPersonal);
            this.Group = "Basic";
            this.Name = "SettingsControlLicenses";
            this.Section = "Licenses";
            this.Size = new System.Drawing.Size(400, 239);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewLicenses;
        private System.Windows.Forms.ColumnHeader columnHeaderLicense;
        private System.Windows.Forms.ColumnHeader columnHeaderExpire;
        private System.Windows.Forms.Label labelFeaturesInstruction;
        private System.Windows.Forms.Label labelPersonal;
    }
}
