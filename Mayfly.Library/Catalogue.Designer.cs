namespace Mayfly.Library
{
    partial class Catalogue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Catalogue));
            this.splitContainerSeries = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSeries = new System.Windows.Forms.Label();
            this.listViewSeries = new System.Windows.Forms.ListView();
            this.columnSeriesTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxSeriesGroup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxIssueGroup = new System.Windows.Forms.ComboBox();
            this.labelIssues = new System.Windows.Forms.Label();
            this.listViewIssues = new System.Windows.Forms.ListView();
            this.columnIssueYear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIssueNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIssueTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLicenses = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBook = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxBookGroup = new System.Windows.Forms.ComboBox();
            this.labelBooks = new System.Windows.Forms.Label();
            this.buttonBookAdd = new System.Windows.Forms.Button();
            this.listViewBooks = new System.Windows.Forms.ListView();
            this.columnBookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBookWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBookWhere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBookPublisher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBookPages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnBookQty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextBook = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextBookEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextBookRead = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSeries = new System.Windows.Forms.TabPage();
            this.tabResearches = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxResearchesGroup = new System.Windows.Forms.ComboBox();
            this.buttonResearchAdd = new System.Windows.Forms.Button();
            this.labelResearches = new System.Windows.Forms.Label();
            this.listViewResearches = new System.Windows.Forms.ListView();
            this.columnResTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnResWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnResExecutive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnResKeywords = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnResAuthors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextResearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextResearchEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextResearchRead = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.Data = new Mayfly.Library.Library();
            this.backFileMover = new System.ComponentModel.BackgroundWorker();
            this.backLoader = new System.ComponentModel.BackgroundWorker();
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.processDisplay = new Mayfly.Controls.ProcessDisplay(this.components);
            this.searchBox1 = new Mayfly.Controls.SearchBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSeries)).BeginInit();
            this.splitContainerSeries.Panel1.SuspendLayout();
            this.splitContainerSeries.Panel2.SuspendLayout();
            this.splitContainerSeries.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBook.SuspendLayout();
            this.contextBook.SuspendLayout();
            this.tabSeries.SuspendLayout();
            this.tabResearches.SuspendLayout();
            this.contextResearch.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Data)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerSeries
            // 
            resources.ApplyResources(this.splitContainerSeries, "splitContainerSeries");
            this.splitContainerSeries.Name = "splitContainerSeries";
            // 
            // splitContainerSeries.Panel1
            // 
            this.splitContainerSeries.Panel1.Controls.Add(this.label4);
            this.splitContainerSeries.Panel1.Controls.Add(this.labelSeries);
            this.splitContainerSeries.Panel1.Controls.Add(this.listViewSeries);
            this.splitContainerSeries.Panel1.Controls.Add(this.comboBoxSeriesGroup);
            // 
            // splitContainerSeries.Panel2
            // 
            this.splitContainerSeries.Panel2.Controls.Add(this.label3);
            this.splitContainerSeries.Panel2.Controls.Add(this.comboBoxIssueGroup);
            this.splitContainerSeries.Panel2.Controls.Add(this.labelIssues);
            this.splitContainerSeries.Panel2.Controls.Add(this.listViewIssues);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // labelSeries
            // 
            resources.ApplyResources(this.labelSeries, "labelSeries");
            this.labelSeries.Name = "labelSeries";
            // 
            // listViewSeries
            // 
            this.listViewSeries.AllowColumnReorder = true;
            this.listViewSeries.AllowDrop = true;
            resources.ApplyResources(this.listViewSeries, "listViewSeries");
            this.listViewSeries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSeriesTitle});
            this.listViewSeries.FullRowSelect = true;
            this.listViewSeries.Name = "listViewSeries";
            this.listViewSeries.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewSeries.UseCompatibleStateImageBehavior = false;
            this.listViewSeries.View = System.Windows.Forms.View.Details;
            this.listViewSeries.SelectedIndexChanged += new System.EventHandler(this.listViewSeries_SelectedIndexChanged);
            // 
            // columnSeriesTitle
            // 
            resources.ApplyResources(this.columnSeriesTitle, "columnSeriesTitle");
            // 
            // comboBoxSeriesGroup
            // 
            resources.ApplyResources(this.comboBoxSeriesGroup, "comboBoxSeriesGroup");
            this.comboBoxSeriesGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeriesGroup.FormattingEnabled = true;
            this.comboBoxSeriesGroup.Items.AddRange(new object[] {
            resources.GetString("comboBoxSeriesGroup.Items")});
            this.comboBoxSeriesGroup.Name = "comboBoxSeriesGroup";
            this.comboBoxSeriesGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeriesGroup_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBoxIssueGroup
            // 
            resources.ApplyResources(this.comboBoxIssueGroup, "comboBoxIssueGroup");
            this.comboBoxIssueGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIssueGroup.FormattingEnabled = true;
            this.comboBoxIssueGroup.Items.AddRange(new object[] {
            resources.GetString("comboBoxIssueGroup.Items"),
            resources.GetString("comboBoxIssueGroup.Items1"),
            resources.GetString("comboBoxIssueGroup.Items2"),
            resources.GetString("comboBoxIssueGroup.Items3")});
            this.comboBoxIssueGroup.Name = "comboBoxIssueGroup";
            this.comboBoxIssueGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxIssueGroup_SelectedIndexChanged);
            // 
            // labelIssues
            // 
            resources.ApplyResources(this.labelIssues, "labelIssues");
            this.labelIssues.Name = "labelIssues";
            // 
            // listViewIssues
            // 
            this.listViewIssues.AllowColumnReorder = true;
            this.listViewIssues.AllowDrop = true;
            resources.ApplyResources(this.listViewIssues, "listViewIssues");
            this.listViewIssues.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewIssues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIssueYear,
            this.columnIssueNo,
            this.columnIssueTitle});
            this.listViewIssues.FullRowSelect = true;
            this.listViewIssues.Name = "listViewIssues";
            this.listViewIssues.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewIssues.UseCompatibleStateImageBehavior = false;
            this.listViewIssues.View = System.Windows.Forms.View.Details;
            // 
            // columnIssueYear
            // 
            resources.ApplyResources(this.columnIssueYear, "columnIssueYear");
            // 
            // columnIssueNo
            // 
            resources.ApplyResources(this.columnIssueNo, "columnIssueNo");
            // 
            // columnIssueTitle
            // 
            resources.ApplyResources(this.columnIssueTitle, "columnIssueTitle");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuService});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator1,
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
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuService
            // 
            this.menuService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.menuItemLicenses,
            this.menuItemAbout});
            this.menuService.Name = "menuService";
            resources.ApplyResources(this.menuService, "menuService");
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemLicenses
            // 
            this.menuItemLicenses.Name = "menuItemLicenses";
            resources.ApplyResources(this.menuItemLicenses, "menuItemLicenses");
            this.menuItemLicenses.Click += new System.EventHandler(this.menuItemLicenses_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabBook);
            this.tabControl1.Controls.Add(this.tabSeries);
            this.tabControl1.Controls.Add(this.tabResearches);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabBook
            // 
            this.tabBook.Controls.Add(this.label2);
            this.tabBook.Controls.Add(this.comboBoxBookGroup);
            this.tabBook.Controls.Add(this.labelBooks);
            this.tabBook.Controls.Add(this.buttonBookAdd);
            this.tabBook.Controls.Add(this.listViewBooks);
            resources.ApplyResources(this.tabBook, "tabBook");
            this.tabBook.Name = "tabBook";
            this.tabBook.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBoxBookGroup
            // 
            resources.ApplyResources(this.comboBoxBookGroup, "comboBoxBookGroup");
            this.comboBoxBookGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBookGroup.FormattingEnabled = true;
            this.comboBoxBookGroup.Items.AddRange(new object[] {
            resources.GetString("comboBoxBookGroup.Items"),
            resources.GetString("comboBoxBookGroup.Items1"),
            resources.GetString("comboBoxBookGroup.Items2"),
            resources.GetString("comboBoxBookGroup.Items3"),
            resources.GetString("comboBoxBookGroup.Items4"),
            resources.GetString("comboBoxBookGroup.Items5")});
            this.comboBoxBookGroup.Name = "comboBoxBookGroup";
            this.comboBoxBookGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxBookGroup_SelectedIndexChanged);
            // 
            // labelBooks
            // 
            resources.ApplyResources(this.labelBooks, "labelBooks");
            this.labelBooks.Name = "labelBooks";
            // 
            // buttonBookAdd
            // 
            resources.ApplyResources(this.buttonBookAdd, "buttonBookAdd");
            this.buttonBookAdd.Name = "buttonBookAdd";
            this.buttonBookAdd.UseVisualStyleBackColor = true;
            this.buttonBookAdd.Click += new System.EventHandler(this.buttonBookAdd_Click);
            // 
            // listViewBooks
            // 
            this.listViewBooks.AllowColumnReorder = true;
            this.listViewBooks.AllowDrop = true;
            resources.ApplyResources(this.listViewBooks, "listViewBooks");
            this.listViewBooks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnBookTitle,
            this.columnBookWhen,
            this.columnBookWhere,
            this.columnBookPublisher,
            this.columnBookPages,
            this.columnBookQty});
            this.listViewBooks.ContextMenuStrip = this.contextBook;
            this.listViewBooks.FullRowSelect = true;
            this.listViewBooks.Name = "listViewBooks";
            this.listViewBooks.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewBooks.UseCompatibleStateImageBehavior = false;
            this.listViewBooks.View = System.Windows.Forms.View.Details;
            this.listViewBooks.ItemActivate += new System.EventHandler(this.listViewBooks_ItemActivate);
            // 
            // columnBookTitle
            // 
            resources.ApplyResources(this.columnBookTitle, "columnBookTitle");
            // 
            // columnBookWhen
            // 
            resources.ApplyResources(this.columnBookWhen, "columnBookWhen");
            // 
            // columnBookWhere
            // 
            resources.ApplyResources(this.columnBookWhere, "columnBookWhere");
            // 
            // columnBookPublisher
            // 
            resources.ApplyResources(this.columnBookPublisher, "columnBookPublisher");
            // 
            // columnBookPages
            // 
            resources.ApplyResources(this.columnBookPages, "columnBookPages");
            // 
            // columnBookQty
            // 
            resources.ApplyResources(this.columnBookQty, "columnBookQty");
            // 
            // contextBook
            // 
            this.contextBook.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextBookEdit,
            this.contextBookRead});
            this.contextBook.Name = "contextBook";
            resources.ApplyResources(this.contextBook, "contextBook");
            this.contextBook.Opening += new System.ComponentModel.CancelEventHandler(this.contextBook_Opening);
            // 
            // contextBookEdit
            // 
            resources.ApplyResources(this.contextBookEdit, "contextBookEdit");
            this.contextBookEdit.Name = "contextBookEdit";
            this.contextBookEdit.Click += new System.EventHandler(this.listViewBooks_ItemActivate);
            // 
            // contextBookRead
            // 
            this.contextBookRead.Name = "contextBookRead";
            resources.ApplyResources(this.contextBookRead, "contextBookRead");
            this.contextBookRead.Click += new System.EventHandler(this.contextBookRead_Click);
            // 
            // tabSeries
            // 
            this.tabSeries.Controls.Add(this.splitContainerSeries);
            resources.ApplyResources(this.tabSeries, "tabSeries");
            this.tabSeries.Name = "tabSeries";
            this.tabSeries.UseVisualStyleBackColor = true;
            // 
            // tabResearches
            // 
            this.tabResearches.Controls.Add(this.label9);
            this.tabResearches.Controls.Add(this.comboBoxResearchesGroup);
            this.tabResearches.Controls.Add(this.buttonResearchAdd);
            this.tabResearches.Controls.Add(this.labelResearches);
            this.tabResearches.Controls.Add(this.listViewResearches);
            resources.ApplyResources(this.tabResearches, "tabResearches");
            this.tabResearches.Name = "tabResearches";
            this.tabResearches.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // comboBoxResearchesGroup
            // 
            resources.ApplyResources(this.comboBoxResearchesGroup, "comboBoxResearchesGroup");
            this.comboBoxResearchesGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResearchesGroup.FormattingEnabled = true;
            this.comboBoxResearchesGroup.Items.AddRange(new object[] {
            resources.GetString("comboBoxResearchesGroup.Items"),
            resources.GetString("comboBoxResearchesGroup.Items1"),
            resources.GetString("comboBoxResearchesGroup.Items2"),
            resources.GetString("comboBoxResearchesGroup.Items3"),
            resources.GetString("comboBoxResearchesGroup.Items4")});
            this.comboBoxResearchesGroup.Name = "comboBoxResearchesGroup";
            this.comboBoxResearchesGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxResearchesGroup_SelectedIndexChanged);
            // 
            // buttonResearchAdd
            // 
            resources.ApplyResources(this.buttonResearchAdd, "buttonResearchAdd");
            this.buttonResearchAdd.Name = "buttonResearchAdd";
            this.buttonResearchAdd.UseVisualStyleBackColor = true;
            this.buttonResearchAdd.Click += new System.EventHandler(this.buttonResearchAdd_Click);
            // 
            // labelResearches
            // 
            resources.ApplyResources(this.labelResearches, "labelResearches");
            this.labelResearches.Name = "labelResearches";
            // 
            // listViewResearches
            // 
            this.listViewResearches.AllowColumnReorder = true;
            this.listViewResearches.AllowDrop = true;
            resources.ApplyResources(this.listViewResearches, "listViewResearches");
            this.listViewResearches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewResearches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnResTitle,
            this.columnResWhen,
            this.columnResExecutive,
            this.columnResKeywords,
            this.columnResAuthors});
            this.listViewResearches.ContextMenuStrip = this.contextResearch;
            this.listViewResearches.FullRowSelect = true;
            this.listViewResearches.Name = "listViewResearches";
            this.listViewResearches.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewResearches.UseCompatibleStateImageBehavior = false;
            this.listViewResearches.View = System.Windows.Forms.View.Details;
            this.listViewResearches.ItemActivate += new System.EventHandler(this.listViewResearches_ItemActivate);
            // 
            // columnResTitle
            // 
            resources.ApplyResources(this.columnResTitle, "columnResTitle");
            // 
            // columnResWhen
            // 
            resources.ApplyResources(this.columnResWhen, "columnResWhen");
            // 
            // columnResExecutive
            // 
            resources.ApplyResources(this.columnResExecutive, "columnResExecutive");
            // 
            // columnResKeywords
            // 
            resources.ApplyResources(this.columnResKeywords, "columnResKeywords");
            // 
            // columnResAuthors
            // 
            resources.ApplyResources(this.columnResAuthors, "columnResAuthors");
            // 
            // contextResearch
            // 
            this.contextResearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextResearchEdit,
            this.contextResearchRead});
            this.contextResearch.Name = "contextBook";
            resources.ApplyResources(this.contextResearch, "contextResearch");
            this.contextResearch.Opening += new System.ComponentModel.CancelEventHandler(this.contextResearch_Opening);
            // 
            // contextResearchEdit
            // 
            resources.ApplyResources(this.contextResearchEdit, "contextResearchEdit");
            this.contextResearchEdit.Name = "contextResearchEdit";
            // 
            // contextResearchRead
            // 
            this.contextResearchRead.Name = "contextResearchRead";
            resources.ApplyResources(this.contextResearchRead, "contextResearchRead");
            this.contextResearchRead.Click += new System.EventHandler(this.contextResearchRead_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelCount,
            this.statusProcess,
            this.statusLoading});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusLabelCount
            // 
            this.statusLabelCount.Name = "statusLabelCount";
            resources.ApplyResources(this.statusLabelCount, "statusLabelCount");
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
            // Data
            // 
            this.Data.DataSetName = "Library";
            this.Data.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // backFileMover
            // 
            this.backFileMover.WorkerReportsProgress = true;
            this.backFileMover.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backFileMover_DoWork);
            this.backFileMover.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backFileMover_ProgressChanged);
            this.backFileMover.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backFileMover_RunWorkerCompleted);
            // 
            // backLoader
            // 
            this.backLoader.WorkerReportsProgress = true;
            this.backLoader.WorkerSupportsCancellation = true;
            this.backLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backLoader_DoWork);
            this.backLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backLoader_ProgressChanged);
            this.backLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backLoader_RunWorkerCompleted);
            // 
            // taskDialogSaveChanges
            // 
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
            // processDisplay
            // 
            this.processDisplay.Default = null;
            this.processDisplay.Look = null;
            this.processDisplay.MaximalInterval = 2000;
            this.processDisplay.ProgressBar = this.statusLoading;
            this.processDisplay.StatusLog = this.statusProcess;
            // 
            // searchBox1
            // 
            resources.ApplyResources(this.searchBox1, "searchBox1");
            this.searchBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.searchBox1.InstantSearch = true;
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.SearchTermChanged += new System.EventHandler(this.searchBox1_SearchTermChanged);
            // 
            // Catalogue
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Catalogue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Catalogue_FormClosing);
            this.splitContainerSeries.Panel1.ResumeLayout(false);
            this.splitContainerSeries.Panel1.PerformLayout();
            this.splitContainerSeries.Panel2.ResumeLayout(false);
            this.splitContainerSeries.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSeries)).EndInit();
            this.splitContainerSeries.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabBook.ResumeLayout(false);
            this.tabBook.PerformLayout();
            this.contextBook.ResumeLayout(false);
            this.tabSeries.ResumeLayout(false);
            this.tabResearches.ResumeLayout(false);
            this.tabResearches.PerformLayout();
            this.contextResearch.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBook;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabSeries;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCount;
        private System.Windows.Forms.ListView listViewBooks;
        private System.Windows.Forms.ColumnHeader columnBookTitle;
        private System.Windows.Forms.ColumnHeader columnBookWhen;
        private System.Windows.Forms.ColumnHeader columnBookWhere;
        private System.Windows.Forms.ColumnHeader columnBookPublisher;
        private System.Windows.Forms.ColumnHeader columnBookPages;
        private System.Windows.Forms.Button buttonBookAdd;
        private System.Windows.Forms.Label labelBooks;
        private System.Windows.Forms.Label labelIssues;
        private System.Windows.Forms.ListView listViewIssues;
        private System.Windows.Forms.ColumnHeader columnIssueYear;
        private System.Windows.Forms.ColumnHeader columnIssueNo;
        private System.Windows.Forms.ListView listViewSeries;
        private System.Windows.Forms.Label labelSeries;
        private Library Data;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private TaskDialogs.TaskDialog taskDialogSaveChanges;
        private TaskDialogs.TaskDialogButton tdbSave;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ContextMenuStrip contextBook;
        private System.Windows.Forms.ToolStripMenuItem contextBookEdit;
        private System.Windows.Forms.ToolStripMenuItem contextBookRead;
        private System.Windows.Forms.ColumnHeader columnBookQty;
        private System.Windows.Forms.TabPage tabResearches;
        private System.Windows.Forms.Label labelResearches;
        private System.Windows.Forms.ListView listViewResearches;
        private System.Windows.Forms.ColumnHeader columnResTitle;
        private System.Windows.Forms.ColumnHeader columnResWhen;
        private System.Windows.Forms.ColumnHeader columnResExecutive;
        private System.Windows.Forms.Button buttonResearchAdd;
        private Controls.ProcessDisplay processDisplay;
        private System.ComponentModel.BackgroundWorker backFileMover;
        private System.Windows.Forms.ContextMenuStrip contextResearch;
        private System.Windows.Forms.ToolStripMenuItem contextResearchEdit;
        private System.Windows.Forms.ToolStripMenuItem contextResearchRead;
        private System.ComponentModel.BackgroundWorker backLoader;
        private System.Windows.Forms.ToolStripStatusLabel statusProcess;
        private System.Windows.Forms.ToolStripProgressBar statusLoading;
        private System.Windows.Forms.ToolStripMenuItem menuService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ComboBox comboBoxResearchesGroup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxBookGroup;
        private System.Windows.Forms.ColumnHeader columnSeriesTitle;
        private System.Windows.Forms.ColumnHeader columnIssueTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxSeriesGroup;
        private System.Windows.Forms.ComboBox comboBoxIssueGroup;
        private System.Windows.Forms.SplitContainer splitContainerSeries;
        private System.Windows.Forms.ColumnHeader columnResKeywords;
        private System.Windows.Forms.ColumnHeader columnResAuthors;
        private Controls.SearchBox searchBox1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLicenses;
    }
}