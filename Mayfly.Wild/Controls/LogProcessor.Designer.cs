namespace Mayfly.Wild.Controls
{
    partial class LogProcessor
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogProcessor));
            this.contextLog = new System.Windows.Forms.ContextMenuStrip();
            this.ToolStripMenuItemIndividuals = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSpecies = new System.Windows.Forms.ContextMenuStrip();
            this.toolStripMenuItemKey = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorKey = new System.Windows.Forms.ToolStripSeparator();
            this.Provider = new Mayfly.Species.TaxonProvider();
            this.toolTipAttention = new System.Windows.Forms.ToolTip();
            this.contextLog.SuspendLayout();
            this.contextMenuStripSpecies.SuspendLayout();
            // 
            // contextLog
            // 
            this.contextLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemIndividuals,
            this.ToolStripMenuItemIndex,
            this.toolStripSeparator5,
            this.ToolStripMenuItemCut,
            this.ToolStripMenuItemCopy,
            this.ToolStripMenuItemPaste,
            this.ToolStripMenuItemDelete});
            this.contextLog.Name = "contextMenuStripLog";
            this.contextLog.Size = new System.Drawing.Size(154, 142);
            this.contextLog.Opening += new System.ComponentModel.CancelEventHandler(this.contextLog_Opening);
            // 
            // ToolStripMenuItemIndividuals
            // 
            this.ToolStripMenuItemIndividuals.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ToolStripMenuItemIndividuals.Name = "ToolStripMenuItemIndividuals";
            this.ToolStripMenuItemIndividuals.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItemIndividuals.Text = "Individuals log";
            // 
            // ToolStripMenuItemIndex
            // 
            this.ToolStripMenuItemIndex.Name = "ToolStripMenuItemIndex";
            this.ToolStripMenuItemIndex.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItemIndex.Text = "Open key";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(150, 6);
            // 
            // ToolStripMenuItemCut
            // 
            this.ToolStripMenuItemCut.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemCut.Image")));
            this.ToolStripMenuItemCut.Name = "ToolStripMenuItemCut";
            this.ToolStripMenuItemCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ToolStripMenuItemCut.ShowShortcutKeys = false;
            this.ToolStripMenuItemCut.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItemCut.Text = "Cut";
            // 
            // ToolStripMenuItemCopy
            // 
            this.ToolStripMenuItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemCopy.Image")));
            this.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            this.ToolStripMenuItemCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ToolStripMenuItemCopy.ShowShortcutKeys = false;
            this.ToolStripMenuItemCopy.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItemCopy.Text = "Copy";
            // 
            // ToolStripMenuItemPaste
            // 
            this.ToolStripMenuItemPaste.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemPaste.Image")));
            this.ToolStripMenuItemPaste.Name = "ToolStripMenuItemPaste";
            this.ToolStripMenuItemPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.ToolStripMenuItemPaste.ShowShortcutKeys = false;
            this.ToolStripMenuItemPaste.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItemPaste.Text = "Paste";
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemDelete.Image")));
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItemDelete.Text = "Delete";
            // 
            // contextMenuStripSpecies
            // 
            this.contextMenuStripSpecies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemKey,
            this.toolStripMenuItemRecent,
            this.toolStripMenuItemAll,
            this.toolStripSeparatorKey});
            this.contextMenuStripSpecies.Name = "contextMenuStrip_species";
            this.contextMenuStripSpecies.Size = new System.Drawing.Size(164, 76);
            // 
            // toolStripMenuItemKey
            // 
            this.toolStripMenuItemKey.Name = "toolStripMenuItemKey";
            this.toolStripMenuItemKey.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItemKey.Text = "Species from key";
            // 
            // toolStripMenuItemRecent
            // 
            this.toolStripMenuItemRecent.Name = "toolStripMenuItemRecent";
            this.toolStripMenuItemRecent.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItemRecent.Text = "Recent species";
            // 
            // toolStripMenuItemAll
            // 
            this.toolStripMenuItemAll.Name = "toolStripMenuItemAll";
            this.toolStripMenuItemAll.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItemAll.Text = "All species";
            // 
            // toolStripSeparatorKey
            // 
            this.toolStripSeparatorKey.Name = "toolStripSeparatorKey";
            this.toolStripSeparatorKey.Size = new System.Drawing.Size(160, 6);
            // 
            // Provider
            // 
            this.Provider.CheckDuplicates = false;
            this.Provider.ColumnName = "ColumnSpecies";
            this.Provider.RecentListCount = 0;
            this.Provider.SpeciesSelected += new Mayfly.Species.SpeciesSelectEventHandler(this.provider_SpeciesSelected);
            this.Provider.DuplicateFound += new Mayfly.Species.DuplicateFoundEventHandler(this.provider_DuplicateDetected);
            this.Provider.IndexChanged += new System.EventHandler(this.provider_IndexChanged);
            this.contextLog.ResumeLayout(false);
            this.contextMenuStripSpecies.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextLog;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndividuals;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndex;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCut;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        public Species.TaxonProvider Provider;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSpecies;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKey;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRecent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorKey;
        private System.Windows.Forms.ToolTip toolTipAttention;
    }
}
