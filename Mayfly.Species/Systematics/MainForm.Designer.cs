namespace Mayfly.Species.Systematics
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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.menuTaxa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddBase = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabPageTaxa = new System.Windows.Forms.TabPage();
            this.labelMinCount = new System.Windows.Forms.Label();
            this.listViewMinor = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelRepCount = new System.Windows.Forms.Label();
            this.labelTaxCount = new System.Windows.Forms.Label();
            this.listViewRepresence = new System.Windows.Forms.ListView();
            this.columnHeaderSpeciesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderReference = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewTaxa = new System.Windows.Forms.TreeView();
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
            this.contextTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextNewBase = new System.Windows.Forms.ToolStripMenuItem();
            this.contextBase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextBaseRename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextBaseDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.contextBaseAddTaxa = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextTaxonEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonRename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTaxonDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextTaxonAddSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpecies = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextSpeciesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSpeciesDelete = new System.Windows.Forms.ToolStripMenuItem();
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
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogAssociateSpecies = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSetSpecies = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSetSpeciesCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogReassociateSpecies = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbReassSpecies = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbReassSpeciesCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogDeleteSpecies = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbDeleteSpc = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteRep = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.status = new Mayfly.Controls.Status();
            this.backTreeLoader = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageTaxa.SuspendLayout();
            this.tabPageKey.SuspendLayout();
            this.tabPagePictures.SuspendLayout();
            this.contextTree.SuspendLayout();
            this.contextBase.SuspendLayout();
            this.contextTaxon.SuspendLayout();
            this.contextSpecies.SuspendLayout();
            this.contextStep.SuspendLayout();
            this.contextFeature.SuspendLayout();
            this.contextSynonym.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTaxa,
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
            this.menuItemNew.Image = global::Mayfly.Resources.Icons.New;
            this.menuItemNew.Name = "menuItemNew";
            resources.ApplyResources(this.menuItemNew, "menuItemNew");
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = global::Mayfly.Resources.Icons.Open;
            this.menuItemOpen.Name = "menuItemOpen";
            resources.ApplyResources(this.menuItemOpen, "menuItemOpen");
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = global::Mayfly.Resources.Icons.Save;
            this.menuItemSave.Name = "menuItemSave";
            resources.ApplyResources(this.menuItemSave, "menuItemSave");
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
            this.menuItemPreview.Image = global::Mayfly.Resources.Icons.Preview;
            this.menuItemPreview.Name = "menuItemPreview";
            resources.ApplyResources(this.menuItemPreview, "menuItemPreview");
            this.menuItemPreview.Click += new System.EventHandler(this.menuItemPreview_Click);
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Image = global::Mayfly.Resources.Icons.Print;
            this.menuItemPrint.Name = "menuItemPrint";
            resources.ApplyResources(this.menuItemPrint, "menuItemPrint");
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
            // menuTaxa
            // 
            this.menuTaxa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddBase,
            this.menuItemAddTaxon,
            this.menuItemAddSpecies});
            this.menuTaxa.Name = "menuTaxa";
            resources.ApplyResources(this.menuTaxa, "menuTaxa");
            // 
            // menuItemAddBase
            // 
            this.menuItemAddBase.Name = "menuItemAddBase";
            resources.ApplyResources(this.menuItemAddBase, "menuItemAddBase");
            this.menuItemAddBase.Click += new System.EventHandler(this.menuItemAddBase_Click);
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
            this.menuItemSettings.Image = global::Mayfly.Resources.Icons.Settings;
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
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
            this.tabControl.Controls.Add(this.tabPageTaxa);
            this.tabControl.Controls.Add(this.tabPageKey);
            this.tabControl.Controls.Add(this.tabPagePictures);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tab_Changed);
            // 
            // tabPageTaxa
            // 
            this.tabPageTaxa.Controls.Add(this.labelMinCount);
            this.tabPageTaxa.Controls.Add(this.listViewMinor);
            this.tabPageTaxa.Controls.Add(this.labelRepCount);
            this.tabPageTaxa.Controls.Add(this.labelTaxCount);
            this.tabPageTaxa.Controls.Add(this.listViewRepresence);
            this.tabPageTaxa.Controls.Add(this.treeViewTaxa);
            resources.ApplyResources(this.tabPageTaxa, "tabPageTaxa");
            this.tabPageTaxa.Name = "tabPageTaxa";
            this.tabPageTaxa.UseVisualStyleBackColor = true;
            // 
            // labelMinCount
            // 
            resources.ApplyResources(this.labelMinCount, "labelMinCount");
            this.labelMinCount.Name = "labelMinCount";
            // 
            // listViewMinor
            // 
            resources.ApplyResources(this.listViewMinor, "listViewMinor");
            this.listViewMinor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewMinor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.listViewMinor.FullRowSelect = true;
            this.listViewMinor.Name = "listViewMinor";
            this.listViewMinor.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewMinor.UseCompatibleStateImageBehavior = false;
            this.listViewMinor.View = System.Windows.Forms.View.Details;
            this.listViewMinor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewMinor_MouseDown);
            // 
            // columnHeader5
            // 
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            // 
            // columnHeader6
            // 
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            // 
            // labelRepCount
            // 
            resources.ApplyResources(this.labelRepCount, "labelRepCount");
            this.labelRepCount.Name = "labelRepCount";
            // 
            // labelTaxCount
            // 
            resources.ApplyResources(this.labelTaxCount, "labelTaxCount");
            this.labelTaxCount.Name = "labelTaxCount";
            // 
            // listViewRepresence
            // 
            this.listViewRepresence.AllowColumnReorder = true;
            this.listViewRepresence.AllowDrop = true;
            resources.ApplyResources(this.listViewRepresence, "listViewRepresence");
            this.listViewRepresence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRepresence.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSpeciesName,
            this.columnHeaderReference});
            this.listViewRepresence.FullRowSelect = true;
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
            // columnHeaderSpeciesName
            // 
            resources.ApplyResources(this.columnHeaderSpeciesName, "columnHeaderSpeciesName");
            // 
            // columnHeaderReference
            // 
            resources.ApplyResources(this.columnHeaderReference, "columnHeaderReference");
            // 
            // treeViewTaxa
            // 
            this.treeViewTaxa.AllowDrop = true;
            resources.ApplyResources(this.treeViewTaxa, "treeViewTaxa");
            this.treeViewTaxa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewTaxa.FullRowSelect = true;
            this.treeViewTaxa.HotTracking = true;
            this.treeViewTaxa.ItemHeight = 23;
            this.treeViewTaxa.LabelEdit = true;
            this.treeViewTaxa.Name = "treeViewTaxa";
            this.treeViewTaxa.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeViewTaxa.Nodes")))});
            this.treeViewTaxa.ShowLines = false;
            this.treeViewTaxa.ShowNodeToolTips = true;
            this.treeViewTaxa.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewTaxa_AfterLabelEdit);
            this.treeViewTaxa.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewTaxa_ItemDrag);
            this.treeViewTaxa.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTaxa_AfterSelect);
            this.treeViewTaxa.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewTaxa_NodeMouseClick);
            this.treeViewTaxa.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewTaxa_NodeMouseDoubleClick);
            this.treeViewTaxa.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewTaxa_DragDrop);
            this.treeViewTaxa.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewTaxa_DragEnter);
            this.treeViewTaxa.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewTaxa_DragOver);
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
            this.listViewImages.Name = "listViewImages";
            this.listViewImages.UseCompatibleStateImageBehavior = false;
            // 
            // contextTree
            // 
            this.contextTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextNewBase});
            this.contextTree.Name = "contextMenuStripTree";
            resources.ApplyResources(this.contextTree, "contextTree");
            // 
            // contextNewBase
            // 
            this.contextNewBase.Name = "contextNewBase";
            resources.ApplyResources(this.contextNewBase, "contextNewBase");
            this.contextNewBase.Click += new System.EventHandler(this.menuItemAddBase_Click);
            // 
            // contextBase
            // 
            this.contextBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextBaseRename,
            this.contextBaseDelete,
            this.toolStripSeparator4,
            this.contextBaseAddTaxa});
            this.contextBase.Name = "contextMenuStripBase";
            resources.ApplyResources(this.contextBase, "contextBase");
            // 
            // contextBaseRename
            // 
            resources.ApplyResources(this.contextBaseRename, "contextBaseRename");
            this.contextBaseRename.Name = "contextBaseRename";
            this.contextBaseRename.Click += new System.EventHandler(this.contextRename_Click);
            // 
            // contextBaseDelete
            // 
            this.contextBaseDelete.Name = "contextBaseDelete";
            resources.ApplyResources(this.contextBaseDelete, "contextBaseDelete");
            this.contextBaseDelete.Click += new System.EventHandler(this.contextBaseDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // contextBaseAddTaxa
            // 
            this.contextBaseAddTaxa.Name = "contextBaseAddTaxa";
            resources.ApplyResources(this.contextBaseAddTaxa, "contextBaseAddTaxa");
            this.contextBaseAddTaxa.Click += new System.EventHandler(this.menuItemAddTaxon_Click);
            // 
            // contextTaxon
            // 
            this.contextTaxon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextTaxonEdit,
            this.contextTaxonRename,
            this.contextTaxonDelete,
            this.toolStripSeparator3,
            this.contextTaxonAddSpecies});
            this.contextTaxon.Name = "contextMenuStripTaxa";
            resources.ApplyResources(this.contextTaxon, "contextTaxon");
            this.contextTaxon.Opening += new System.ComponentModel.CancelEventHandler(this.contextTaxon_Opening);
            // 
            // contextTaxonEdit
            // 
            resources.ApplyResources(this.contextTaxonEdit, "contextTaxonEdit");
            this.contextTaxonEdit.Name = "contextTaxonEdit";
            this.contextTaxonEdit.Click += new System.EventHandler(this.contextTaxonEdit_Click);
            // 
            // contextTaxonRename
            // 
            this.contextTaxonRename.Name = "contextTaxonRename";
            resources.ApplyResources(this.contextTaxonRename, "contextTaxonRename");
            this.contextTaxonRename.Click += new System.EventHandler(this.contextRename_Click);
            // 
            // contextTaxonDelete
            // 
            this.contextTaxonDelete.Name = "contextTaxonDelete";
            resources.ApplyResources(this.contextTaxonDelete, "contextTaxonDelete");
            this.contextTaxonDelete.Click += new System.EventHandler(this.contextTaxonDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // contextTaxonAddSpecies
            // 
            this.contextTaxonAddSpecies.Name = "contextTaxonAddSpecies";
            resources.ApplyResources(this.contextTaxonAddSpecies, "contextTaxonAddSpecies");
            this.contextTaxonAddSpecies.Click += new System.EventHandler(this.menuItemAddSpecies_Click);
            // 
            // contextSpecies
            // 
            this.contextSpecies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextSpeciesEdit,
            this.contextSpeciesDelete});
            this.contextSpecies.Name = "contextMenuStripWater";
            resources.ApplyResources(this.contextSpecies, "contextSpecies");
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
            this.backSpcLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backSpcLoader_DoWork);
            this.backSpcLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backSpcLoader_RunWorkerCompleted);
            // 
            // taskDialogSaveChanges
            // 
            this.taskDialogSaveChanges.AllowDialogCancellation = true;
            this.taskDialogSaveChanges.Buttons.Add(this.tdbSave);
            this.taskDialogSaveChanges.Buttons.Add(this.tdbDiscard);
            this.taskDialogSaveChanges.Buttons.Add(this.tdbCancelClose);
            this.taskDialogSaveChanges.CenterParent = true;
            resources.ApplyResources(this.taskDialogSaveChanges, "taskDialogSaveChanges");
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
            // taskDialogAssociateSpecies
            // 
            this.taskDialogAssociateSpecies.AllowDialogCancellation = true;
            this.taskDialogAssociateSpecies.Buttons.Add(this.tdbSetSpecies);
            this.taskDialogAssociateSpecies.Buttons.Add(this.tdbSetSpeciesCancel);
            this.taskDialogAssociateSpecies.CenterParent = true;
            resources.ApplyResources(this.taskDialogAssociateSpecies, "taskDialogAssociateSpecies");
            // 
            // tdbSetSpecies
            // 
            resources.ApplyResources(this.tdbSetSpecies, "tdbSetSpecies");
            // 
            // tdbSetSpeciesCancel
            // 
            this.tdbSetSpeciesCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // taskDialogReassociateSpecies
            // 
            this.taskDialogReassociateSpecies.AllowDialogCancellation = true;
            this.taskDialogReassociateSpecies.Buttons.Add(this.tdbReassSpecies);
            this.taskDialogReassociateSpecies.Buttons.Add(this.tdbReassSpeciesCancel);
            this.taskDialogReassociateSpecies.CenterParent = true;
            resources.ApplyResources(this.taskDialogReassociateSpecies, "taskDialogReassociateSpecies");
            // 
            // tdbReassSpecies
            // 
            resources.ApplyResources(this.tdbReassSpecies, "tdbReassSpecies");
            // 
            // tdbReassSpeciesCancel
            // 
            this.tdbReassSpeciesCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // taskDialogDeleteSpecies
            // 
            this.taskDialogDeleteSpecies.AllowDialogCancellation = true;
            this.taskDialogDeleteSpecies.Buttons.Add(this.tdbDeleteSpc);
            this.taskDialogDeleteSpecies.Buttons.Add(this.tdbDeleteRep);
            this.taskDialogDeleteSpecies.Buttons.Add(this.tdbDeleteCancel);
            this.taskDialogDeleteSpecies.CenterParent = true;
            resources.ApplyResources(this.taskDialogDeleteSpecies, "taskDialogDeleteSpecies");
            // 
            // tdbDeleteSpc
            // 
            resources.ApplyResources(this.tdbDeleteSpc, "tdbDeleteSpc");
            // 
            // tdbDeleteRep
            // 
            resources.ApplyResources(this.tdbDeleteRep, "tdbDeleteRep");
            // 
            // tdbDeleteCancel
            // 
            this.tdbDeleteCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // status
            // 
            this.status.Default = null;
            this.status.MaximalInterval = 2000;
            this.status.StatusLog = this.toolStripStatusLabel1;
            // 
            // backTreeLoader
            // 
            this.backTreeLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backTreeLoader_DoWork);
            this.backTreeLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backTreeLoader_RunWorkerCompleted);
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
            this.tabPageTaxa.ResumeLayout(false);
            this.tabPageTaxa.PerformLayout();
            this.tabPageKey.ResumeLayout(false);
            this.tabPageKey.PerformLayout();
            this.tabPagePictures.ResumeLayout(false);
            this.tabPagePictures.PerformLayout();
            this.contextTree.ResumeLayout(false);
            this.contextBase.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
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
        private System.Windows.Forms.ToolStripMenuItem menuTaxa;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddBase;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddTaxon;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddSpecies;
        private System.Windows.Forms.ToolStripMenuItem menuService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageTaxa;
        private System.Windows.Forms.ListView listViewRepresence;
        private System.Windows.Forms.ColumnHeader columnHeaderSpeciesName;
        private System.Windows.Forms.ColumnHeader columnHeaderReference;
        public System.Windows.Forms.TreeView treeViewTaxa;
        private System.Windows.Forms.TabPage tabPageKey;
        private System.Windows.Forms.Label labelTaxCount;
        private System.Windows.Forms.TabPage tabPagePictures;
        private System.Windows.Forms.ToolStripMenuItem menuPictures;
        private System.Windows.Forms.ToolStripMenuItem menuItemPictureLoad;
        private System.Windows.Forms.ListView listViewImages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelRepCount;
        private System.Windows.Forms.Label labelStpCount;
        private System.Windows.Forms.Label labelPicCount;
        private System.Windows.Forms.ContextMenuStrip contextTree;
        private System.Windows.Forms.ToolStripMenuItem contextNewBase;
        private System.Windows.Forms.ContextMenuStrip contextBase;
        private System.Windows.Forms.ToolStripMenuItem contextBaseRename;
        private System.Windows.Forms.ToolStripMenuItem contextBaseDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem contextBaseAddTaxa;
        private System.Windows.Forms.ContextMenuStrip contextTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonEdit;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonRename;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem contextTaxonAddSpecies;
        private System.Windows.Forms.ContextMenuStrip contextSpecies;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesEdit;
        private System.Windows.Forms.ToolStripMenuItem contextSpeciesDelete;
        private Mayfly.Controls.Status status;
        private TaskDialogs.TaskDialog taskDialogSaveChanges;
        private TaskDialogs.TaskDialog taskDialogAssociateSpecies;
        private TaskDialogs.TaskDialog taskDialogReassociateSpecies;
        private TaskDialogs.TaskDialog taskDialogDeleteSpecies;
        private TaskDialogs.TaskDialogButton tdbSave;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private TaskDialogs.TaskDialogButton tdbSetSpecies;
        private TaskDialogs.TaskDialogButton tdbSetSpeciesCancel;
        private TaskDialogs.TaskDialogButton tdbReassSpecies;
        private TaskDialogs.TaskDialogButton tdbReassSpeciesCancel;
        private TaskDialogs.TaskDialogButton tdbDeleteSpc;
        private TaskDialogs.TaskDialogButton tdbDeleteRep;
        private TaskDialogs.TaskDialogButton tdbDeleteCancel;
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
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label labelMinCount;
        private System.Windows.Forms.ContextMenuStrip contextSynonym;
        private System.Windows.Forms.ToolStripMenuItem contextRemoveSynonym;
        private System.ComponentModel.BackgroundWorker backSpcLoader;
        private System.ComponentModel.BackgroundWorker backTreeLoader;
    }
}