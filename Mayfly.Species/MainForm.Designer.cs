namespace Mayfly.Species
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusSpecies = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTaxon = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKey = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddStep = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPictures = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPictureLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTaxon = new System.Windows.Forms.TabPage();
            this.listViewMinor = new System.Windows.Forms.ListView();
            this.colSynSpecies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSynRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSynName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewRepresence = new System.Windows.Forms.ListView();
            this.colSpcSpecies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSpcRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSpcName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewDerivates = new System.Windows.Forms.TreeView();
            this.contextTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextTreeNewTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTreeNewSpc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.contextTreeExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTreeCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageKey = new System.Windows.Forms.TabPage();
            this.buttonTry = new System.Windows.Forms.Button();
            this.labelEngagedCount = new System.Windows.Forms.Label();
            this.listViewEngagement = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewStep = new System.Windows.Forms.TreeView();
            this.labelStpCount = new System.Windows.Forms.Label();
            this.tabPagePictures = new System.Windows.Forms.TabPage();
            this.labelPicCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listViewImages = new System.Windows.Forms.ListView();
            this.contextTaxon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextTaxonEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonDepart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonAddSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextTaxonExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpecies = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextSpeciesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpeciesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpeciesDepart = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListKeys = new System.Windows.Forms.ImageList(this.components);
            this.contextStep = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextStepDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.contextStepNewFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFeature = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextFeatureEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFeatureDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextFeatureNewFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSynonym = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextRemoveSynonym = new System.Windows.Forms.ToolStripMenuItem();
            this.backSpcLoader = new System.ComponentModel.BackgroundWorker();
            this.tdSave = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.status = new Mayfly.Controls.Status();
            this.backTreeLoader = new System.ComponentModel.BackgroundWorker();
            this.processDisplay = new Mayfly.Controls.ProcessDisplay(this.components);
            this.tdDeleteTaxon = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbDeleteConfirm = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteParentize = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteOrphanize = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageTaxon.SuspendLayout();
            this.contextTree.SuspendLayout();
            this.tabPageKey.SuspendLayout();
            this.tabPagePictures.SuspendLayout();
            this.contextTaxon.SuspendLayout();
            this.contextSpecies.SuspendLayout();
            this.contextStep.SuspendLayout();
            this.contextFeature.SuspendLayout();
            this.contextSynonym.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusSpecies,
            this.statusTaxon,
            this.statusProcess,
            this.statusLoading});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusSpecies
            // 
            resources.ApplyResources(this.statusSpecies, "statusSpecies");
            this.statusSpecies.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusSpecies.Name = "statusSpecies";
            // 
            // statusTaxon
            // 
            resources.ApplyResources(this.statusTaxon, "statusTaxon");
            this.statusTaxon.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.statusTaxon.Name = "statusTaxon";
            // 
            // statusProcess
            // 
            this.statusProcess.Name = "statusProcess";
            resources.ApplyResources(this.statusProcess, "statusProcess");
            this.statusProcess.Spring = true;
            // 
            // statusLoading
            // 
            this.statusLoading.Margin = new System.Windows.Forms.Padding(1, 6, 1, 6);
            this.statusLoading.MarqueeAnimationSpeed = 50;
            this.statusLoading.Name = "statusLoading";
            resources.ApplyResources(this.statusLoading, "statusLoading");
            this.statusLoading.Value = 50;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTaxon,
            this.menuKey,
            this.menuPictures,
            this.menuService});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator1,
            this.menuItemPreview,
            this.menuItemPrint,
            this.toolStripSeparator8,
            this.menuItemClose});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            // 
            // menuItemNew
            // 
            resources.ApplyResources(this.menuItemNew, "menuItemNew");
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            resources.ApplyResources(this.menuItemOpen, "menuItemOpen");
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            resources.ApplyResources(this.menuItemSave, "menuItemSave");
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            resources.ApplyResources(this.menuItemSaveAs, "menuItemSaveAs");
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuItemPreview
            // 
            resources.ApplyResources(this.menuItemPreview, "menuItemPreview");
            this.menuItemPreview.Name = "menuItemPreview";
            this.menuItemPreview.Click += new System.EventHandler(this.menuItemPreview_Click);
            // 
            // menuItemPrint
            // 
            resources.ApplyResources(this.menuItemPrint, "menuItemPrint");
            this.menuItemPrint.Name = "menuItemPrint";
            this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuTaxon
            // 
            this.menuTaxon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddTaxon,
            this.menuItemAddSpecies});
            this.menuTaxon.Name = "menuTaxon";
            resources.ApplyResources(this.menuTaxon, "menuTaxon");
            // 
            // menuItemAddTaxon
            // 
            this.menuItemAddTaxon.Name = "menuItemAddTaxon";
            resources.ApplyResources(this.menuItemAddTaxon, "menuItemAddTaxon");
            this.menuItemAddTaxon.Click += new System.EventHandler(this.menuItemAddTaxon_Click);
            // 
            // menuItemAddSpecies
            // 
            this.menuItemAddSpecies.Name = "menuItemAddSpecies";
            resources.ApplyResources(this.menuItemAddSpecies, "menuItemAddSpecies");
            this.menuItemAddSpecies.Click += new System.EventHandler(this.menuItemAddSpecies_Click);
            // 
            // menuKey
            // 
            this.menuKey.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddStep,
            this.menuItemAddFeature});
            this.menuKey.Name = "menuKey";
            resources.ApplyResources(this.menuKey, "menuKey");
            // 
            // menuItemAddStep
            // 
            this.menuItemAddStep.Name = "menuItemAddStep";
            resources.ApplyResources(this.menuItemAddStep, "menuItemAddStep");
            // 
            // menuItemAddFeature
            // 
            this.menuItemAddFeature.Name = "menuItemAddFeature";
            resources.ApplyResources(this.menuItemAddFeature, "menuItemAddFeature");
            // 
            // menuPictures
            // 
            this.menuPictures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPictureLoad});
            this.menuPictures.Name = "menuPictures";
            resources.ApplyResources(this.menuPictures, "menuPictures");
            // 
            // menuItemPictureLoad
            // 
            this.menuItemPictureLoad.Name = "menuItemPictureLoad";
            resources.ApplyResources(this.menuItemPictureLoad, "menuItemPictureLoad");
            // 
            // menuService
            // 
            this.menuService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.menuItemAbout});
            this.menuService.Name = "menuService";
            resources.ApplyResources(this.menuService, "menuService");
            // 
            // menuItemSettings
            // 
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Name = "menuItemSettings";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageTaxon);
            this.tabControl.Controls.Add(this.tabPageKey);
            this.tabControl.Controls.Add(this.tabPagePictures);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tab_Changed);
            // 
            // tabPageTaxon
            // 
            this.tabPageTaxon.Controls.Add(this.listViewMinor);
            this.tabPageTaxon.Controls.Add(this.listViewRepresence);
            this.tabPageTaxon.Controls.Add(this.treeViewDerivates);
            resources.ApplyResources(this.tabPageTaxon, "tabPageTaxon");
            this.tabPageTaxon.Name = "tabPageTaxon";
            this.tabPageTaxon.UseVisualStyleBackColor = true;
            // 
            // listViewMinor
            // 
            resources.ApplyResources(this.listViewMinor, "listViewMinor");
            this.listViewMinor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewMinor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSynSpecies,
            this.colSynRef,
            this.colSynName});
            this.listViewMinor.FullRowSelect = true;
            this.listViewMinor.HideSelection = false;
            this.listViewMinor.Name = "listViewMinor";
            this.listViewMinor.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewMinor.UseCompatibleStateImageBehavior = false;
            this.listViewMinor.View = System.Windows.Forms.View.Details;
            this.listViewMinor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewMinor_MouseDown);
            // 
            // colSynSpecies
            // 
            resources.ApplyResources(this.colSynSpecies, "colSynSpecies");
            // 
            // colSynRef
            // 
            resources.ApplyResources(this.colSynRef, "colSynRef");
            // 
            // colSynName
            // 
            resources.ApplyResources(this.colSynName, "colSynName");
            // 
            // listViewRepresence
            // 
            this.listViewRepresence.AllowColumnReorder = true;
            this.listViewRepresence.AllowDrop = true;
            resources.ApplyResources(this.listViewRepresence, "listViewRepresence");
            this.listViewRepresence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRepresence.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSpcSpecies,
            this.colSpcRef,
            this.colSpcName});
            this.listViewRepresence.FullRowSelect = true;
            this.listViewRepresence.HideSelection = false;
            this.listViewRepresence.Name = "listViewRepresence";
            this.listViewRepresence.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewRepresence.UseCompatibleStateImageBehavior = false;
            this.listViewRepresence.View = System.Windows.Forms.View.Details;
            this.listViewRepresence.ItemActivate += new System.EventHandler(this.contextSpeciesEdit_Click);
            this.listViewRepresence.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewRepresence_ItemDrag);
            this.listViewRepresence.SelectedIndexChanged += new System.EventHandler(this.listViewRepresence_SelectedIndexChanged);
            this.listViewRepresence.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewRepresence_DragDrop);
            this.listViewRepresence.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewRepresence_DragOver);
            this.listViewRepresence.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewRepresence_MouseDown);
            // 
            // colSpcSpecies
            // 
            resources.ApplyResources(this.colSpcSpecies, "colSpcSpecies");
            // 
            // colSpcRef
            // 
            resources.ApplyResources(this.colSpcRef, "colSpcRef");
            // 
            // colSpcName
            // 
            resources.ApplyResources(this.colSpcName, "colSpcName");
            // 
            // treeViewDerivates
            // 
            this.treeViewDerivates.AllowDrop = true;
            resources.ApplyResources(this.treeViewDerivates, "treeViewDerivates");
            this.treeViewDerivates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewDerivates.ContextMenuStrip = this.contextTree;
            this.treeViewDerivates.FullRowSelect = true;
            this.treeViewDerivates.HideSelection = false;
            this.treeViewDerivates.HotTracking = true;
            this.treeViewDerivates.ItemHeight = 23;
            this.treeViewDerivates.Name = "treeViewDerivates";
            this.treeViewDerivates.ShowLines = false;
            this.treeViewDerivates.ShowNodeToolTips = true;
            this.treeViewDerivates.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeViewDerivates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeViewDerivates.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeViewDerivates.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeViewDerivates.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeViewDerivates.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.treeViewDerivates.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            // 
            // contextTree
            // 
            this.contextTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextTreeNewTaxon,
            this.contextTreeNewSpc,
            this.toolStripSeparator4,
            this.contextTreeExpand,
            this.contextTreeCollapse});
            this.contextTree.Name = "contextTree";
            resources.ApplyResources(this.contextTree, "contextTree");
            // 
            // contextTreeNewTaxon
            // 
            this.contextTreeNewTaxon.Name = "contextTreeNewTaxon";
            resources.ApplyResources(this.contextTreeNewTaxon, "contextTreeNewTaxon");
            this.contextTreeNewTaxon.Click += new System.EventHandler(this.menuItemAddTaxon_Click);
            // 
            // contextTreeNewSpc
            // 
            this.contextTreeNewSpc.Name = "contextTreeNewSpc";
            resources.ApplyResources(this.contextTreeNewSpc, "contextTreeNewSpc");
            this.contextTreeNewSpc.Click += new System.EventHandler(this.menuItemAddSpecies_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // contextTreeExpand
            // 
            this.contextTreeExpand.Name = "contextTreeExpand";
            resources.ApplyResources(this.contextTreeExpand, "contextTreeExpand");
            this.contextTreeExpand.Click += new System.EventHandler(this.contextTreeExpand_Click);
            // 
            // contextTreeCollapse
            // 
            this.contextTreeCollapse.Name = "contextTreeCollapse";
            resources.ApplyResources(this.contextTreeCollapse, "contextTreeCollapse");
            this.contextTreeCollapse.Click += new System.EventHandler(this.contextTreeCollapse_Click);
            // 
            // tabPageKey
            // 
            this.tabPageKey.Controls.Add(this.buttonTry);
            this.tabPageKey.Controls.Add(this.labelEngagedCount);
            this.tabPageKey.Controls.Add(this.listViewEngagement);
            this.tabPageKey.Controls.Add(this.treeViewStep);
            this.tabPageKey.Controls.Add(this.labelStpCount);
            resources.ApplyResources(this.tabPageKey, "tabPageKey");
            this.tabPageKey.Name = "tabPageKey";
            this.tabPageKey.UseVisualStyleBackColor = true;
            // 
            // buttonTry
            // 
            resources.ApplyResources(this.buttonTry, "buttonTry");
            this.buttonTry.Name = "buttonTry";
            this.buttonTry.UseVisualStyleBackColor = true;
            this.buttonTry.Click += new System.EventHandler(this.buttonTry_Click);
            // 
            // labelEngagedCount
            // 
            resources.ApplyResources(this.labelEngagedCount, "labelEngagedCount");
            this.labelEngagedCount.Name = "labelEngagedCount";
            // 
            // listViewEngagement
            // 
            this.listViewEngagement.AllowColumnReorder = true;
            this.listViewEngagement.AllowDrop = true;
            resources.ApplyResources(this.listViewEngagement, "listViewEngagement");
            this.listViewEngagement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewEngagement.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewEngagement.FullRowSelect = true;
            this.listViewEngagement.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewEngagement.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewEngagement.Groups1")))});
            this.listViewEngagement.HideSelection = false;
            this.listViewEngagement.Name = "listViewEngagement";
            this.listViewEngagement.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewEngagement.UseCompatibleStateImageBehavior = false;
            this.listViewEngagement.View = System.Windows.Forms.View.Details;
            this.listViewEngagement.ItemActivate += new System.EventHandler(this.listViewEngagement_ItemActivate);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // treeViewStep
            // 
            resources.ApplyResources(this.treeViewStep, "treeViewStep");
            this.treeViewStep.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewStep.FullRowSelect = true;
            this.treeViewStep.HotTracking = true;
            this.treeViewStep.ItemHeight = 23;
            this.treeViewStep.Name = "treeViewStep";
            this.treeViewStep.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeViewStep.Nodes")))});
            this.treeViewStep.ShowLines = false;
            this.treeViewStep.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewStep_AfterSelect);
            this.treeViewStep.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewStep_NodeMouseClick);
            this.treeViewStep.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewStep_NodeMouseDoubleClick);
            // 
            // labelStpCount
            // 
            resources.ApplyResources(this.labelStpCount, "labelStpCount");
            this.labelStpCount.Name = "labelStpCount";
            // 
            // tabPagePictures
            // 
            this.tabPagePictures.Controls.Add(this.labelPicCount);
            this.tabPagePictures.Controls.Add(this.button1);
            this.tabPagePictures.Controls.Add(this.listViewImages);
            resources.ApplyResources(this.tabPagePictures, "tabPagePictures");
            this.tabPagePictures.Name = "tabPagePictures";
            this.tabPagePictures.UseVisualStyleBackColor = true;
            // 
            // labelPicCount
            // 
            resources.ApplyResources(this.labelPicCount, "labelPicCount");
            this.labelPicCount.Name = "labelPicCount";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listViewImages
            // 
            resources.ApplyResources(this.listViewImages, "listViewImages");
            this.listViewImages.HideSelection = false;
            this.listViewImages.Name = "listViewImages";
            this.listViewImages.UseCompatibleStateImageBehavior = false;
            // 
            // contextTaxon
            // 
            this.contextTaxon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextTaxonEdit,
            this.contextTaxonDelete,
            this.contextTaxonDepart,
            this.contextTaxonAddSpecies,
            this.toolStripSeparator3,
            this.contextTaxonExpandAll});
            this.contextTaxon.Name = "contextMenuStripTaxon";
            resources.ApplyResources(this.contextTaxon, "contextTaxon");
            this.contextTaxon.Opening += new System.ComponentModel.CancelEventHandler(this.contextTaxon_Opening);
            // 
            // contextTaxonEdit
            // 
            resources.ApplyResources(this.contextTaxonEdit, "contextTaxonEdit");
            this.contextTaxonEdit.Name = "contextTaxonEdit";
            this.contextTaxonEdit.Click += new System.EventHandler(this.contextTaxonEdit_Click);
            // 
            // contextTaxonDelete
            // 
            this.contextTaxonDelete.Name = "contextTaxonDelete";
            resources.ApplyResources(this.contextTaxonDelete, "contextTaxonDelete");
            this.contextTaxonDelete.Click += new System.EventHandler(this.contextTaxonDelete_Click);
            // 
            // contextTaxonDepart
            // 
            this.contextTaxonDepart.Name = "contextTaxonDepart";
            resources.ApplyResources(this.contextTaxonDepart, "contextTaxonDepart");
            this.contextTaxonDepart.Click += new System.EventHandler(this.contextTaxonDepart_Click);
            // 
            // contextTaxonAddSpecies
            // 
            this.contextTaxonAddSpecies.Name = "contextTaxonAddSpecies";
            resources.ApplyResources(this.contextTaxonAddSpecies, "contextTaxonAddSpecies");
            this.contextTaxonAddSpecies.Click += new System.EventHandler(this.menuItemAddSpecies_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // contextTaxonExpandAll
            // 
            this.contextTaxonExpandAll.Name = "contextTaxonExpandAll";
            resources.ApplyResources(this.contextTaxonExpandAll, "contextTaxonExpandAll");
            this.contextTaxonExpandAll.Click += new System.EventHandler(this.contextTaxonExpandAll_Click);
            // 
            // contextSpecies
            // 
            this.contextSpecies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextSpeciesEdit,
            this.contextSpeciesDelete,
            this.contextSpeciesDepart});
            this.contextSpecies.Name = "contextMenuStripWater";
            resources.ApplyResources(this.contextSpecies, "contextSpecies");
            this.contextSpecies.Opening += new System.ComponentModel.CancelEventHandler(this.contextSpecies_Opening);
            // 
            // contextSpeciesEdit
            // 
            resources.ApplyResources(this.contextSpeciesEdit, "contextSpeciesEdit");
            this.contextSpeciesEdit.Name = "contextSpeciesEdit";
            this.contextSpeciesEdit.Click += new System.EventHandler(this.contextSpeciesEdit_Click);
            // 
            // contextSpeciesDelete
            // 
            this.contextSpeciesDelete.Name = "contextSpeciesDelete";
            resources.ApplyResources(this.contextSpeciesDelete, "contextSpeciesDelete");
            this.contextSpeciesDelete.Click += new System.EventHandler(this.contextSpeciesDelete_Click);
            // 
            // contextSpeciesDepart
            // 
            this.contextSpeciesDepart.Name = "contextSpeciesDepart";
            resources.ApplyResources(this.contextSpeciesDepart, "contextSpeciesDepart");
            this.contextSpeciesDepart.Click += new System.EventHandler(this.contextSpeciesDepart_Click);
            // 
            // imageListKeys
            // 
            this.imageListKeys.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListKeys.ImageStream")));
            this.imageListKeys.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListKeys.Images.SetKeyName(0, "star.png");
            this.imageListKeys.Images.SetKeyName(1, "starfilled.png");
            // 
            // contextStep
            // 
            this.contextStep.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextStepDelete,
            this.toolStripSeparator5,
            this.contextStepNewFeature});
            this.contextStep.Name = "contextStep";
            resources.ApplyResources(this.contextStep, "contextStep");
            // 
            // contextStepDelete
            // 
            this.contextStepDelete.Name = "contextStepDelete";
            resources.ApplyResources(this.contextStepDelete, "contextStepDelete");
            this.contextStepDelete.Click += new System.EventHandler(this.contextStepDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // contextStepNewFeature
            // 
            this.contextStepNewFeature.Name = "contextStepNewFeature";
            resources.ApplyResources(this.contextStepNewFeature, "contextStepNewFeature");
            this.contextStepNewFeature.Click += new System.EventHandler(this.contextStepNewFeature_Click);
            // 
            // contextFeature
            // 
            this.contextFeature.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextFeatureEdit,
            this.contextFeatureDelete,
            this.toolStripSeparator2,
            this.contextFeatureNewFeature});
            this.contextFeature.Name = "contextFeature";
            resources.ApplyResources(this.contextFeature, "contextFeature");
            // 
            // contextFeatureEdit
            // 
            resources.ApplyResources(this.contextFeatureEdit, "contextFeatureEdit");
            this.contextFeatureEdit.Name = "contextFeatureEdit";
            this.contextFeatureEdit.Click += new System.EventHandler(this.contextFeatureEdit_Click);
            // 
            // contextFeatureDelete
            // 
            this.contextFeatureDelete.Name = "contextFeatureDelete";
            resources.ApplyResources(this.contextFeatureDelete, "contextFeatureDelete");
            this.contextFeatureDelete.Click += new System.EventHandler(this.contextFeatureDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // contextFeatureNewFeature
            // 
            this.contextFeatureNewFeature.Name = "contextFeatureNewFeature";
            resources.ApplyResources(this.contextFeatureNewFeature, "contextFeatureNewFeature");
            this.contextFeatureNewFeature.Click += new System.EventHandler(this.contextStepNewFeature_Click);
            // 
            // contextSynonym
            // 
            this.contextSynonym.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextRemoveSynonym});
            this.contextSynonym.Name = "contextSynonym";
            resources.ApplyResources(this.contextSynonym, "contextSynonym");
            // 
            // contextRemoveSynonym
            // 
            this.contextRemoveSynonym.Name = "contextRemoveSynonym";
            resources.ApplyResources(this.contextRemoveSynonym, "contextRemoveSynonym");
            this.contextRemoveSynonym.Click += new System.EventHandler(this.contextRemoveSynonym_Click);
            // 
            // backSpcLoader
            // 
            this.backSpcLoader.WorkerSupportsCancellation = true;
            this.backSpcLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backSpcLoader_DoWork);
            this.backSpcLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backSpcLoader_RunWorkerCompleted);
            // 
            // tdSave
            // 
            this.tdSave.AllowDialogCancellation = true;
            this.tdSave.Buttons.Add(this.tdbSave);
            this.tdSave.Buttons.Add(this.tdbDiscard);
            this.tdSave.Buttons.Add(this.tdbCancelClose);
            this.tdSave.CenterParent = true;
            resources.ApplyResources(this.tdSave, "tdSave");
            // 
            // tdbSave
            // 
            resources.ApplyResources(this.tdbSave, "tdbSave");
            // 
            // tdbDiscard
            // 
            resources.ApplyResources(this.tdbDiscard, "tdbDiscard");
            // 
            // tdbCancelClose
            // 
            this.tdbCancelClose.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // status
            // 
            this.status.Default = null;
            this.status.MaximalInterval = 2000;
            this.status.StatusLog = this.statusProcess;
            // 
            // backTreeLoader
            // 
            this.backTreeLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backTreeLoader_DoWork);
            this.backTreeLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backTreeLoader_RunWorkerCompleted);
            // 
            // processDisplay
            // 
            this.processDisplay.Default = null;
            this.processDisplay.Look = null;
            this.processDisplay.MaximalInterval = 2000;
            this.processDisplay.ProgressBar = this.statusLoading;
            this.processDisplay.StatusLog = this.statusProcess;
            // 
            // tdDeleteTaxon
            // 
            this.tdDeleteTaxon.Buttons.Add(this.tdbDeleteConfirm);
            this.tdDeleteTaxon.Buttons.Add(this.tdbDeleteParentize);
            this.tdDeleteTaxon.Buttons.Add(this.tdbDeleteOrphanize);
            this.tdDeleteTaxon.Buttons.Add(this.tdbDeleteCancel);
            this.tdDeleteTaxon.CenterParent = true;
            resources.ApplyResources(this.tdDeleteTaxon, "tdDeleteTaxon");
            // 
            // tdbDeleteConfirm
            // 
            resources.ApplyResources(this.tdbDeleteConfirm, "tdbDeleteConfirm");
            // 
            // tdbDeleteParentize
            // 
            resources.ApplyResources(this.tdbDeleteParentize, "tdbDeleteParentize");
            // 
            // tdbDeleteOrphanize
            // 
            resources.ApplyResources(this.tdbDeleteOrphanize, "tdbDeleteOrphanize");
            // 
            // tdbDeleteCancel
            // 
            this.tdbDeleteCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageTaxon.ResumeLayout(false);
            this.contextTree.ResumeLayout(false);
            this.tabPageKey.ResumeLayout(false);
            this.tabPageKey.PerformLayout();
            this.tabPagePictures.ResumeLayout(false);
            this.tabPagePictures.PerformLayout();
            this.contextTaxon.ResumeLayout(false);
            this.contextSpecies.ResumeLayout(false);
            this.contextStep.ResumeLayout(false);
            this.contextFeature.ResumeLayout(false);
            this.contextSynonym.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemPreview;
        private System.Windows.Forms.ToolStripMenuItem menuItemPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.ToolStripMenuItem menuTaxon;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddTaxon;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddSpecies;
        private System.Windows.Forms.ToolStripMenuItem menuService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageTaxon;
        private System.Windows.Forms.ListView listViewRepresence;
        private System.Windows.Forms.ColumnHeader colSpcSpecies;
        private System.Windows.Forms.ColumnHeader colSpcRef;
        private System.Windows.Forms.TabPage tabPageKey;
        private System.Windows.Forms.TabPage tabPagePictures;
        private System.Windows.Forms.ToolStripMenuItem menuPictures;
        private System.Windows.Forms.ToolStripMenuItem menuItemPictureLoad;
        private System.Windows.Forms.ListView listViewImages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelStpCount;
        private System.Windows.Forms.Label labelPicCount;
        private System.Windows.Forms.ContextMenuStrip contextTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonEdit;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonAddSpecies;
        private System.Windows.Forms.ContextMenuStrip contextSpecies;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesEdit;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesDelete;
        private Mayfly.Controls.Status status;
        private TaskDialogs.TaskDialog tdSave;
        private TaskDialogs.TaskDialogButton tdbSave;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.Label labelEngagedCount;
        private System.Windows.Forms.ListView listViewEngagement;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TreeView treeViewStep;
        private System.Windows.Forms.Button buttonTry;
        private System.Windows.Forms.ImageList imageListKeys;
        private System.Windows.Forms.ToolStripMenuItem menuKey;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddFeature;
        private System.Windows.Forms.ContextMenuStrip contextStep;
        private System.Windows.Forms.ContextMenuStrip contextFeature;
        private System.Windows.Forms.ToolStripMenuItem contextFeatureEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem contextFeatureNewFeature;
        private System.Windows.Forms.ToolStripMenuItem contextFeatureDelete;
        private System.Windows.Forms.ToolStripMenuItem contextStepDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem contextStepNewFeature;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddStep;
        private System.Windows.Forms.ListView listViewMinor;
        private System.Windows.Forms.ColumnHeader colSynSpecies;
        private System.Windows.Forms.ColumnHeader colSynRef;
        private System.Windows.Forms.ContextMenuStrip contextSynonym;
        private System.Windows.Forms.ToolStripMenuItem contextRemoveSynonym;
        private System.ComponentModel.BackgroundWorker backSpcLoader;
        private System.ComponentModel.BackgroundWorker backTreeLoader;
        public System.Windows.Forms.TreeView treeViewDerivates;
        private System.Windows.Forms.ToolStripStatusLabel statusTaxon;
        private System.Windows.Forms.ToolStripStatusLabel statusProcess;
        private Mayfly.Controls.ProcessDisplay processDisplay;
        private System.Windows.Forms.ColumnHeader colSpcName;
        private System.Windows.Forms.ColumnHeader colSynName;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonDepart;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesDepart;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonExpandAll;
        private System.Windows.Forms.ToolStripProgressBar statusLoading;
        private System.Windows.Forms.ContextMenuStrip contextTree;
        private System.Windows.Forms.ToolStripMenuItem contextTreeNewTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextTreeNewSpc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem contextTreeExpand;
        private System.Windows.Forms.ToolStripMenuItem contextTreeCollapse;
        private TaskDialogs.TaskDialog tdDeleteTaxon;
        private TaskDialogs.TaskDialogButton tdbDeleteConfirm;
        private TaskDialogs.TaskDialogButton tdbDeleteParentize;
        private TaskDialogs.TaskDialogButton tdbDeleteOrphanize;
        private TaskDialogs.TaskDialogButton tdbDeleteCancel;
        private System.Windows.Forms.ToolStripStatusLabel statusSpecies;
    }
}