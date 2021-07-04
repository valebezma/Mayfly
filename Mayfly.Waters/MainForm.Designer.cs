namespace Mayfly.Waters
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWater = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddStream = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddLake = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddTank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.searchBox1 = new Mayfly.Controls.SearchBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listViewWaters = new System.Windows.Forms.ListView();
            this.columnHeaderWaterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOutflow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMouthToMouth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripWater = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.loadInflowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextStripMenuItemAddStream = new System.Windows.Forms.ToolStripMenuItem();
            this.contextStripMenuItemAddLake = new System.Windows.Forms.ToolStripMenuItem();
            this.contextStripMenuItemAddTank = new System.Windows.Forms.ToolStripMenuItem();
            this.labelWaterName = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.waterTree1 = new Mayfly.Waters.Controls.WaterTree();
            this.labelListCount = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelTreeCount = new System.Windows.Forms.Label();
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogDelete = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbDeleteOne = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteInflows = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDeleteCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.status1 = new Mayfly.Controls.Status();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.backFiller = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripWater.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuWater,
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
            this.toolStripSeparator5,
            this.menuItemPreview,
            this.menuItemPrint,
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
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
            // menuWater
            // 
            this.menuWater.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddStream,
            this.menuItemAddLake,
            this.menuItemAddTank,
            this.toolStripSeparator2,
            this.menuItemEdit,
            this.menuItemDelete,
            this.toolStripSeparator4,
            this.menuItemFilter});
            this.menuWater.Name = "menuWater";
            resources.ApplyResources(this.menuWater, "menuWater");
            // 
            // menuItemAddStream
            // 
            this.menuItemAddStream.Name = "menuItemAddStream";
            resources.ApplyResources(this.menuItemAddStream, "menuItemAddStream");
            this.menuItemAddStream.Click += new System.EventHandler(this.menuItemAddStream_Click);
            // 
            // menuItemAddLake
            // 
            this.menuItemAddLake.Name = "menuItemAddLake";
            resources.ApplyResources(this.menuItemAddLake, "menuItemAddLake");
            this.menuItemAddLake.Click += new System.EventHandler(this.menuItemAddLake_Click);
            // 
            // menuItemAddTank
            // 
            this.menuItemAddTank.Name = "menuItemAddTank";
            resources.ApplyResources(this.menuItemAddTank, "menuItemAddTank");
            this.menuItemAddTank.Click += new System.EventHandler(this.menuItemAddTank_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Name = "menuItemEdit";
            resources.ApplyResources(this.menuItemEdit, "menuItemEdit");
            this.menuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Name = "menuItemDelete";
            resources.ApplyResources(this.menuItemDelete, "menuItemDelete");
            this.menuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // menuItemFilter
            // 
            this.menuItemFilter.Name = "menuItemFilter";
            resources.ApplyResources(this.menuItemFilter, "menuItemFilter");
            this.menuItemFilter.Click += new System.EventHandler(this.menuItemFilter_Click);
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
            // searchBox1
            // 
            resources.ApplyResources(this.searchBox1, "searchBox1");
            this.searchBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.searchBox1.InstantSearch = false;
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.TextChanged += new System.EventHandler(this.searchBox1_TextChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empty.bmp");
            this.imageList1.Images.SetKeyName(1, "terminal_river.bmp");
            this.imageList1.Images.SetKeyName(2, "river.bmp");
            this.imageList1.Images.SetKeyName(3, "inflow_left.bmp");
            this.imageList1.Images.SetKeyName(4, "inflow_right.bmp");
            this.imageList1.Images.SetKeyName(5, "tank.bmp");
            this.imageList1.Images.SetKeyName(6, "lake_flood_left.bmp");
            this.imageList1.Images.SetKeyName(7, "lake_flood_right.bmp");
            this.imageList1.Images.SetKeyName(8, "lake.bmp");
            // 
            // listViewWaters
            // 
            this.listViewWaters.AllowColumnReorder = true;
            this.listViewWaters.AllowDrop = true;
            resources.ApplyResources(this.listViewWaters, "listViewWaters");
            this.listViewWaters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewWaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderWaterName,
            this.columnHeaderOutflow,
            this.columnHeaderMouthToMouth,
            this.columnHeaderLength});
            this.listViewWaters.FullRowSelect = true;
            this.listViewWaters.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewWaters.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewWaters.Groups1"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewWaters.Groups2")))});
            this.listViewWaters.HideSelection = false;
            this.listViewWaters.Name = "listViewWaters";
            this.listViewWaters.UseCompatibleStateImageBehavior = false;
            this.listViewWaters.View = System.Windows.Forms.View.Details;
            this.listViewWaters.ItemActivate += new System.EventHandler(this.menuItemEdit_Click);
            this.listViewWaters.SelectedIndexChanged += new System.EventHandler(this.listViewWaters_SelectedIndexChanged);
            this.listViewWaters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewWaters_MouseDown);
            // 
            // columnHeaderWaterName
            // 
            resources.ApplyResources(this.columnHeaderWaterName, "columnHeaderWaterName");
            // 
            // columnHeaderOutflow
            // 
            resources.ApplyResources(this.columnHeaderOutflow, "columnHeaderOutflow");
            // 
            // columnHeaderMouthToMouth
            // 
            resources.ApplyResources(this.columnHeaderMouthToMouth, "columnHeaderMouthToMouth");
            // 
            // columnHeaderLength
            // 
            resources.ApplyResources(this.columnHeaderLength, "columnHeaderLength");
            // 
            // contextMenuStripWater
            // 
            this.contextMenuStripWater.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextStripMenuItemEdit,
            this.contextStripMenuItemDelete,
            this.toolStripSeparator10,
            this.loadInflowsToolStripMenuItem,
            this.toolStripSeparator3,
            this.contextStripMenuItemAddStream,
            this.contextStripMenuItemAddLake,
            this.contextStripMenuItemAddTank});
            this.contextMenuStripWater.Name = "contextMenuStripWater";
            resources.ApplyResources(this.contextMenuStripWater, "contextMenuStripWater");
            // 
            // contextStripMenuItemEdit
            // 
            resources.ApplyResources(this.contextStripMenuItemEdit, "contextStripMenuItemEdit");
            this.contextStripMenuItemEdit.Name = "contextStripMenuItemEdit";
            this.contextStripMenuItemEdit.Click += new System.EventHandler(this.menuItemEdit_Click);
            // 
            // contextStripMenuItemDelete
            // 
            resources.ApplyResources(this.contextStripMenuItemDelete, "contextStripMenuItemDelete");
            this.contextStripMenuItemDelete.Name = "contextStripMenuItemDelete";
            this.contextStripMenuItemDelete.Click += new System.EventHandler(this.menuItemDelete_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            // 
            // loadInflowsToolStripMenuItem
            // 
            this.loadInflowsToolStripMenuItem.Name = "loadInflowsToolStripMenuItem";
            resources.ApplyResources(this.loadInflowsToolStripMenuItem, "loadInflowsToolStripMenuItem");
            this.loadInflowsToolStripMenuItem.Click += new System.EventHandler(this.loadInflowsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // contextStripMenuItemAddStream
            // 
            this.contextStripMenuItemAddStream.Name = "contextStripMenuItemAddStream";
            resources.ApplyResources(this.contextStripMenuItemAddStream, "contextStripMenuItemAddStream");
            this.contextStripMenuItemAddStream.Click += new System.EventHandler(this.contextToolStripMenuItemAddStream_Click);
            // 
            // contextStripMenuItemAddLake
            // 
            this.contextStripMenuItemAddLake.Name = "contextStripMenuItemAddLake";
            resources.ApplyResources(this.contextStripMenuItemAddLake, "contextStripMenuItemAddLake");
            this.contextStripMenuItemAddLake.Click += new System.EventHandler(this.contextToolStripMenuItemAddLake_Click);
            // 
            // contextStripMenuItemAddTank
            // 
            this.contextStripMenuItemAddTank.Name = "contextStripMenuItemAddTank";
            resources.ApplyResources(this.contextStripMenuItemAddTank, "contextStripMenuItemAddTank");
            this.contextStripMenuItemAddTank.Click += new System.EventHandler(this.contextToolStripMenuItemAddTank_Click);
            // 
            // labelWaterName
            // 
            resources.ApplyResources(this.labelWaterName, "labelWaterName");
            this.labelWaterName.Name = "labelWaterName";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.waterTree1);
            this.tabPage1.Controls.Add(this.searchBox1);
            this.tabPage1.Controls.Add(this.labelListCount);
            this.tabPage1.Controls.Add(this.textBoxDescription);
            this.tabPage1.Controls.Add(this.labelTreeCount);
            this.tabPage1.Controls.Add(this.labelWaterName);
            this.tabPage1.Controls.Add(this.listViewWaters);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // waterTree1
            // 
            this.waterTree1.AllowDrop = true;
            this.waterTree1.AllowReplacement = true;
            resources.ApplyResources(this.waterTree1, "waterTree1");
            this.waterTree1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTree1.FullRowSelect = true;
            this.waterTree1.HotTracking = true;
            this.waterTree1.ItemHeight = 23;
            this.waterTree1.Name = "waterTree1";
            this.waterTree1.ShowLines = false;
            this.waterTree1.ShowNodeToolTips = true;
            this.waterTree1.Sorted = true;
            this.waterTree1.WaterMenuStrip = this.contextMenuStripWater;
            this.waterTree1.WaterObject = null;
            this.waterTree1.WaterObjects = null;
            this.waterTree1.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.treeViewWaters_WaterSelected);
            this.waterTree1.WaterUpdated += new Mayfly.Waters.Controls.WaterEventHandler(this.treeViewWaters_WaterUpdated);
            // 
            // labelListCount
            // 
            resources.ApplyResources(this.labelListCount, "labelListCount");
            this.labelListCount.Name = "labelListCount";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            // 
            // labelTreeCount
            // 
            resources.ApplyResources(this.labelTreeCount, "labelTreeCount");
            this.labelTreeCount.Name = "labelTreeCount";
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
            this.tdbSave.Default = true;
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
            // taskDialogDelete
            // 
            this.taskDialogDelete.AllowDialogCancellation = true;
            this.taskDialogDelete.Buttons.Add(this.tdbDeleteOne);
            this.taskDialogDelete.Buttons.Add(this.tdbDeleteInflows);
            this.taskDialogDelete.Buttons.Add(this.tdbDeleteCancel);
            this.taskDialogDelete.CenterParent = true;
            resources.ApplyResources(this.taskDialogDelete, "taskDialogDelete");
            // 
            // tdbDeleteOne
            // 
            this.tdbDeleteOne.Default = true;
            resources.ApplyResources(this.tdbDeleteOne, "tdbDeleteOne");
            // 
            // tdbDeleteInflows
            // 
            resources.ApplyResources(this.tdbDeleteInflows, "tdbDeleteInflows");
            // 
            // tdbDeleteCancel
            // 
            this.tdbDeleteCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // status1
            // 
            this.status1.Default = null;
            this.status1.MaximalInterval = 2000;
            this.status1.StatusLog = this.toolStripStatusLabel1;
            // 
            // backFiller
            // 
            this.backFiller.WorkerSupportsCancellation = true;
            this.backFiller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backFiller_DoWork);
            this.backFiller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backFiller_RunWorkerCompleted);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripWater.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private Mayfly.Controls.Status status1;
        private Mayfly.Controls.SearchBox searchBox1;
        private TaskDialogs.TaskDialog taskDialogSaveChanges;
        private Mayfly.TaskDialogs.TaskDialogButton tdbSave;
        private Mayfly.TaskDialogs.TaskDialogButton tdbDiscard;
        private Mayfly.TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ListView listViewWaters;
        private System.Windows.Forms.ColumnHeader columnHeaderWaterName;
        private System.Windows.Forms.ColumnHeader columnHeaderOutflow;
        private System.Windows.Forms.ColumnHeader columnHeaderMouthToMouth;
        private System.Windows.Forms.ColumnHeader columnHeaderLength;
        private System.Windows.Forms.Label labelWaterName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuWater;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddStream;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddLake;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddTank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private TaskDialogs.TaskDialog taskDialogDelete;
        private TaskDialogs.TaskDialogButton tdbDeleteOne;
        private TaskDialogs.TaskDialogButton tdbDeleteInflows;
        private TaskDialogs.TaskDialogButton tdbDeleteCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripWater;
        private System.Windows.Forms.ToolStripMenuItem contextStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem contextStripMenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem contextStripMenuItemAddStream;
        private System.Windows.Forms.ToolStripMenuItem contextStripMenuItemAddLake;
        private System.Windows.Forms.ToolStripMenuItem contextStripMenuItemAddTank;
        private System.Windows.Forms.ToolStripMenuItem loadInflowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuItemFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuItemPreview;
        private System.Windows.Forms.ToolStripMenuItem menuItemPrint;
        private System.Windows.Forms.ToolStripMenuItem menuService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label labelTreeCount;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelListCount;
        private Controls.WaterTree waterTree1;
        private System.ComponentModel.BackgroundWorker backFiller;
    }
}