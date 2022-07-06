namespace Mayfly.Species.Controls
{
    partial class TaxonTreeView
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
            this.components = new System.ComponentModel.Container();
            this.loader = new System.ComponentModel.BackgroundWorker();
            this.contextSpecies = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextSpeciesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpeciesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpeciesDepart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextTreeNewTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTreeNewSpc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.contextTreeZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTreeExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTreeCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextTaxonEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonDepart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonAddTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonAddSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextTaxonZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tdDeleteTaxon = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbTaxonDeleteConfirm = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbTaxonDeleteParentize = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbTaxonDeleteOrphanize = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbTaxonDeleteCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.contextSpecies.SuspendLayout();
            this.contextTree.SuspendLayout();
            this.contextTaxon.SuspendLayout();
            this.SuspendLayout();
            // 
            // loader
            // 
            this.loader.WorkerReportsProgress = true;
            this.loader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loader_DoWork);
            this.loader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.loader_ProgressChanged);
            this.loader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loader_RunWorkerCompleted);
            // 
            // contextSpecies
            // 
            this.contextSpecies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextSpeciesEdit,
            this.contextSpeciesDelete,
            this.contextSpeciesDepart});
            this.contextSpecies.Name = "contextMenuStripWater";
            this.contextSpecies.Size = new System.Drawing.Size(156, 70);
            this.contextSpecies.Opening += new System.ComponentModel.CancelEventHandler(this.contextSpecies_Opening);
            // 
            // contextSpeciesEdit
            // 
            this.contextSpeciesEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.contextSpeciesEdit.Name = "contextSpeciesEdit";
            this.contextSpeciesEdit.Size = new System.Drawing.Size(155, 22);
            this.contextSpeciesEdit.Text = "Edit species";
            // 
            // contextSpeciesDelete
            // 
            this.contextSpeciesDelete.Name = "contextSpeciesDelete";
            this.contextSpeciesDelete.Size = new System.Drawing.Size(155, 22);
            this.contextSpeciesDelete.Text = "Delete species";
            // 
            // contextSpeciesDepart
            // 
            this.contextSpeciesDepart.Name = "contextSpeciesDepart";
            this.contextSpeciesDepart.Size = new System.Drawing.Size(155, 22);
            this.contextSpeciesDepart.Text = "Depart from {0}";
            // 
            // contextTree
            // 
            this.contextTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextTreeNewTaxon,
            this.contextTreeNewSpc,
            this.toolStripSeparator4,
            this.contextTreeZoomOut,
            this.contextTreeExpand,
            this.contextTreeCollapse});
            this.contextTree.Name = "contextTree";
            this.contextTree.Size = new System.Drawing.Size(140, 120);
            // 
            // contextTreeNewTaxon
            // 
            this.contextTreeNewTaxon.Name = "contextTreeNewTaxon";
            this.contextTreeNewTaxon.Size = new System.Drawing.Size(139, 22);
            this.contextTreeNewTaxon.Text = "New taxon";
            // 
            // contextTreeNewSpc
            // 
            this.contextTreeNewSpc.Name = "contextTreeNewSpc";
            this.contextTreeNewSpc.Size = new System.Drawing.Size(139, 22);
            this.contextTreeNewSpc.Text = "New species";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(136, 6);
            // 
            // contextTreeZoomOut
            // 
            this.contextTreeZoomOut.Name = "contextTreeZoomOut";
            this.contextTreeZoomOut.Size = new System.Drawing.Size(139, 22);
            this.contextTreeZoomOut.Text = "Zoom out";
            // 
            // contextTreeExpand
            // 
            this.contextTreeExpand.Name = "contextTreeExpand";
            this.contextTreeExpand.Size = new System.Drawing.Size(139, 22);
            this.contextTreeExpand.Text = "Expand all";
            // 
            // contextTreeCollapse
            // 
            this.contextTreeCollapse.Name = "contextTreeCollapse";
            this.contextTreeCollapse.Size = new System.Drawing.Size(139, 22);
            this.contextTreeCollapse.Text = "Collapse all";
            // 
            // contextTaxon
            // 
            this.contextTaxon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextTaxonEdit,
            this.contextTaxonDelete,
            this.contextTaxonDepart,
            this.contextTaxonAddTaxon,
            this.contextTaxonAddSpecies,
            this.toolStripSeparator3,
            this.contextTaxonZoomIn,
            this.contextTaxonExpandAll});
            this.contextTaxon.Name = "contextMenuStripTaxon";
            this.contextTaxon.Size = new System.Drawing.Size(163, 164);
            this.contextTaxon.Opening += new System.ComponentModel.CancelEventHandler(this.contextTaxon_Opening);
            // 
            // contextTaxonEdit
            // 
            this.contextTaxonEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.contextTaxonEdit.Name = "contextTaxonEdit";
            this.contextTaxonEdit.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonEdit.Text = "Edit taxon";
            // 
            // contextTaxonDelete
            // 
            this.contextTaxonDelete.Name = "contextTaxonDelete";
            this.contextTaxonDelete.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonDelete.Text = "Delete taxon";
            // 
            // contextTaxonDepart
            // 
            this.contextTaxonDepart.Name = "contextTaxonDepart";
            this.contextTaxonDepart.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonDepart.Text = "Depart from {0}";
            // 
            // contextTaxonAddTaxon
            // 
            this.contextTaxonAddTaxon.Name = "contextTaxonAddTaxon";
            this.contextTaxonAddTaxon.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonAddTaxon.Text = "Add new taxon";
            // 
            // contextTaxonAddSpecies
            // 
            this.contextTaxonAddSpecies.Name = "contextTaxonAddSpecies";
            this.contextTaxonAddSpecies.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonAddSpecies.Text = "Add new species";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(159, 6);
            // 
            // contextTaxonZoomIn
            // 
            this.contextTaxonZoomIn.Name = "contextTaxonZoomIn";
            this.contextTaxonZoomIn.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonZoomIn.Text = "Zoom in";
            // 
            // contextTaxonExpandAll
            // 
            this.contextTaxonExpandAll.Name = "contextTaxonExpandAll";
            this.contextTaxonExpandAll.Size = new System.Drawing.Size(162, 22);
            this.contextTaxonExpandAll.Text = "Expand all";
            // 
            // tdDeleteTaxon
            // 
            this.tdDeleteTaxon.Buttons.Add(this.tdbTaxonDeleteConfirm);
            this.tdDeleteTaxon.Buttons.Add(this.tdbTaxonDeleteParentize);
            this.tdDeleteTaxon.Buttons.Add(this.tdbTaxonDeleteOrphanize);
            this.tdDeleteTaxon.Buttons.Add(this.tdbTaxonDeleteCancel);
            this.tdDeleteTaxon.CenterParent = true;
            this.tdDeleteTaxon.Content = "Taxon has species included. What to do with them?";
            this.tdDeleteTaxon.MainInstruction = "Taxon has descendants";
            this.tdDeleteTaxon.WindowTitle = "Species Inventory";
            // 
            // tdbTaxonDeleteConfirm
            // 
            this.tdbTaxonDeleteConfirm.Text = "Remove";
            // 
            // tdbTaxonDeleteParentize
            // 
            this.tdbTaxonDeleteParentize.Text = "Assign to parent";
            // 
            // tdbTaxonDeleteOrphanize
            // 
            this.tdbTaxonDeleteOrphanize.Text = "Orphanize";
            // 
            // tdbTaxonDeleteCancel
            // 
            this.tdbTaxonDeleteCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            this.tdbTaxonDeleteCancel.Text = "taskDialogButton1";
            // 
            // TaxaTreeView
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.contextSpecies.ResumeLayout(false);
            this.contextTree.ResumeLayout(false);
            this.contextTaxon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker loader;
        private System.Windows.Forms.ContextMenuStrip contextSpecies;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesEdit;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesDelete;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesDepart;
        private System.Windows.Forms.ContextMenuStrip contextTree;
        private System.Windows.Forms.ToolStripMenuItem contextTreeNewTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextTreeNewSpc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem contextTreeExpand;
        private System.Windows.Forms.ToolStripMenuItem contextTreeCollapse;
        private System.Windows.Forms.ContextMenuStrip contextTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonEdit;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonDelete;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonDepart;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonAddSpecies;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonExpandAll;
        private System.Windows.Forms.ToolTip toolTip;
        private TaskDialogs.TaskDialog tdDeleteTaxon;
        private TaskDialogs.TaskDialogButton tdbTaxonDeleteConfirm;
        private TaskDialogs.TaskDialogButton tdbTaxonDeleteOrphanize;
        private TaskDialogs.TaskDialogButton tdbTaxonDeleteParentize;
        private TaskDialogs.TaskDialogButton tdbTaxonDeleteCancel;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonAddTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonZoomIn;
        private System.Windows.Forms.ToolStripMenuItem contextTreeZoomOut;
    }
}
