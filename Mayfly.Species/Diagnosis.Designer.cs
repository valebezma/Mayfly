namespace Mayfly.Species
{
    partial class Diagnosis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Diagnosis));
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.columnHeaderFeature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewHistory
            // 
            this.listViewHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFeature,
            this.columnHeaderState});
            resources.ApplyResources(this.listViewHistory, "listViewHistory");
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.Details;
            this.listViewHistory.ItemActivate += new System.EventHandler(this.listViewHistory_ItemActivate);
            // 
            // columnHeaderFeature
            // 
            resources.ApplyResources(this.columnHeaderFeature, "columnHeaderFeature");
            // 
            // columnHeaderState
            // 
            resources.ApplyResources(this.columnHeaderState, "columnHeaderState");
            // 
            // Diagnosis
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ControlBox = false;
            this.Controls.Add(this.listViewHistory);
            this.Name = "Diagnosis";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ColumnHeader columnHeaderFeature;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
    }
}