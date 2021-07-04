namespace Mayfly.Geographics
{
    partial class ListLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListLocation));
            this.listViewLocationType = new System.Windows.Forms.ListView();
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonSelect = new System.Windows.Forms.Button();
            this.backgroundLocationLoader = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // listViewLocationType
            // 
            resources.ApplyResources(this.listViewLocationType, "listViewLocationType");
            this.listViewLocationType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLocationType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderType,
            this.columnHeaderCount});
            this.listViewLocationType.FullRowSelect = true;
            this.listViewLocationType.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewLocationType.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewLocationType.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewLocationType.Items2")))});
            this.listViewLocationType.MultiSelect = false;
            this.listViewLocationType.Name = "listViewLocationType";
            this.listViewLocationType.ShowGroups = false;
            this.listViewLocationType.TileSize = new System.Drawing.Size(280, 25);
            this.listViewLocationType.UseCompatibleStateImageBehavior = false;
            this.listViewLocationType.View = System.Windows.Forms.View.Details;
            this.listViewLocationType.ItemActivate += new System.EventHandler(this.listViewLocationType_ItemActivate);
            this.listViewLocationType.SelectedIndexChanged += new System.EventHandler(this.listViewLocationType_SelectedIndexChanged);
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(this.columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderCount
            // 
            resources.ApplyResources(this.columnHeaderCount, "columnHeaderCount");
            // 
            // buttonSelect
            // 
            resources.ApplyResources(this.buttonSelect, "buttonSelect");
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.listViewLocationType_ItemActivate);
            // 
            // backgroundLocationLoader
            // 
            this.backgroundLocationLoader.WorkerSupportsCancellation = true;
            this.backgroundLocationLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundLocationLoader_DoWork);
            this.backgroundLocationLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundLocationLoader_RunWorkerCompleted);
            // 
            // ListLocation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.listViewLocationType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListLocation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListLocation_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewLocationType;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderCount;
        private System.Windows.Forms.Button buttonSelect;
        private System.ComponentModel.BackgroundWorker backgroundLocationLoader;

    }
}