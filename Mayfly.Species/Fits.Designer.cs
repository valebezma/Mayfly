namespace Mayfly.Species
{
    partial class Fits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fits));
            this.listViewFits = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEntry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewFits
            // 
            resources.ApplyResources(this.listViewFits, "listViewFits");
            this.listViewFits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewFits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderEntry});
            this.listViewFits.FullRowSelect = true;
            this.listViewFits.Name = "listViewFits";
            this.listViewFits.ShowGroups = false;
            this.listViewFits.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewFits.UseCompatibleStateImageBehavior = false;
            this.listViewFits.View = System.Windows.Forms.View.Details;
            this.listViewFits.ItemActivate += new System.EventHandler(this.listViewFits_ItemActivate);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderEntry
            // 
            resources.ApplyResources(this.columnHeaderEntry, "columnHeaderEntry");
            // 
            // DefinableSpecies
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ControlBox = false;
            this.Controls.Add(this.listViewFits);
            this.Name = "DefinableSpecies";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewFits;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderEntry;
    }
}