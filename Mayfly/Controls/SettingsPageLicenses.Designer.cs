namespace Mayfly.Controls
{
    partial class SettingsPageLicenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPageLicenses));
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.columnHeaderLicense = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpire = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelFeaturesInstruction = new System.Windows.Forms.Label();
            this.labelPersonal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewLicenses
            // 
            resources.ApplyResources(this.listViewLicenses, "listViewLicenses");
            this.listViewLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLicenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLicense,
            this.columnHeaderExpire});
            this.listViewLicenses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLicenses.HideSelection = false;
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderLicense
            // 
            resources.ApplyResources(this.columnHeaderLicense, "columnHeaderLicense");
            // 
            // columnHeaderExpire
            // 
            resources.ApplyResources(this.columnHeaderExpire, "columnHeaderExpire");
            // 
            // labelFeaturesInstruction
            // 
            resources.ApplyResources(this.labelFeaturesInstruction, "labelFeaturesInstruction");
            this.labelFeaturesInstruction.Name = "labelFeaturesInstruction";
            // 
            // labelPersonal
            // 
            resources.ApplyResources(this.labelPersonal, "labelPersonal");
            this.labelPersonal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPersonal.Name = "labelPersonal";
            // 
            // SettingsControlLicenses
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewLicenses);
            this.Controls.Add(this.labelFeaturesInstruction);
            this.Controls.Add(this.labelPersonal);
            this.Name = "SettingsControlLicenses";
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
