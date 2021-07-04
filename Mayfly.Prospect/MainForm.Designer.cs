namespace Mayfly.Prospect
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWaters = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCalendar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.menuService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.listViewWaters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListWaters = new System.Windows.Forms.ImageList(this.components);
            this.labelWaters = new System.Windows.Forms.Label();
            this.buttonBenthos = new System.Windows.Forms.Button();
            this.buttonPlankton = new System.Windows.Forms.Button();
            this.buttonFish = new System.Windows.Forms.Button();
            this.labelDateEnd = new System.Windows.Forms.Label();
            this.labelDateStart = new System.Windows.Forms.Label();
            this.labelCards = new System.Windows.Forms.Label();
            this.labelDates = new System.Windows.Forms.Label();
            this.labelCardsNumber = new System.Windows.Forms.Label();
            this.listViewInvestigators = new System.Windows.Forms.ListView();
            this.columnInvestigator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelCollectors = new System.Windows.Forms.Label();
            this.tabPageWaters = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchBox1 = new Mayfly.Controls.SearchBox();
            this.waterTree1 = new Mayfly.Waters.Controls.WaterTree();
            this.listViewStudiedWaters = new System.Windows.Forms.ListView();
            this.columnHeaderWaterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOutflow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMouthToMouth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuWater = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextWaterBrief = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageLocations = new System.Windows.Forms.TabPage();
            this.buttonLocSave = new System.Windows.Forms.Button();
            this.listViewLocations = new System.Windows.Forms.ListView();
            this.columnPlcWhere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPlcWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelLocations = new System.Windows.Forms.Label();
            this.tabPageCalendar = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.processDisplay1 = new Mayfly.Controls.ProcessDisplay(this.components);
            this.status1 = new Mayfly.Controls.Status();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tabPageWaters.SuspendLayout();
            this.contextMenuWater.SuspendLayout();
            this.tabPageLocations.SuspendLayout();
            this.tabPageCalendar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.dataToolStripMenuItem,
            this.menuService});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            resources.ApplyResources(this.menuItemExit, "menuItemExit");
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemWaters,
            this.menuItemCalendar,
            this.menuItemLocations});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            resources.ApplyResources(this.dataToolStripMenuItem, "dataToolStripMenuItem");
            // 
            // menuItemWaters
            // 
            this.menuItemWaters.Name = "menuItemWaters";
            resources.ApplyResources(this.menuItemWaters, "menuItemWaters");
            this.menuItemWaters.Click += new System.EventHandler(this.menuItemWaters_Click);
            // 
            // menuItemCalendar
            // 
            this.menuItemCalendar.Name = "menuItemCalendar";
            resources.ApplyResources(this.menuItemCalendar, "menuItemCalendar");
            this.menuItemCalendar.Click += new System.EventHandler(this.menuItemCalendar_Click);
            // 
            // menuItemLocations
            // 
            this.menuItemLocations.Name = "menuItemLocations";
            resources.ApplyResources(this.menuItemLocations, "menuItemLocations");
            this.menuItemLocations.Click += new System.EventHandler(this.menuItemLocations_Click);
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
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageInfo);
            this.tabControl1.Controls.Add(this.tabPageWaters);
            this.tabControl1.Controls.Add(this.tabPageLocations);
            this.tabControl1.Controls.Add(this.tabPageCalendar);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.listViewWaters);
            this.tabPageInfo.Controls.Add(this.labelWaters);
            this.tabPageInfo.Controls.Add(this.buttonBenthos);
            this.tabPageInfo.Controls.Add(this.buttonPlankton);
            this.tabPageInfo.Controls.Add(this.buttonFish);
            this.tabPageInfo.Controls.Add(this.labelDateEnd);
            this.tabPageInfo.Controls.Add(this.labelDateStart);
            this.tabPageInfo.Controls.Add(this.labelCards);
            this.tabPageInfo.Controls.Add(this.labelDates);
            this.tabPageInfo.Controls.Add(this.labelCardsNumber);
            this.tabPageInfo.Controls.Add(this.listViewInvestigators);
            this.tabPageInfo.Controls.Add(this.labelCollectors);
            resources.ApplyResources(this.tabPageInfo, "tabPageInfo");
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // listViewWaters
            // 
            resources.ApplyResources(this.listViewWaters, "listViewWaters");
            this.listViewWaters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewWaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewWaters.FullRowSelect = true;
            this.listViewWaters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewWaters.LargeImageList = this.imageListWaters;
            this.listViewWaters.Name = "listViewWaters";
            this.listViewWaters.ShowGroups = false;
            this.listViewWaters.SmallImageList = this.imageListWaters;
            this.listViewWaters.TileSize = new System.Drawing.Size(230, 25);
            this.listViewWaters.UseCompatibleStateImageBehavior = false;
            this.listViewWaters.View = System.Windows.Forms.View.Tile;
            this.listViewWaters.ItemActivate += new System.EventHandler(this.listViewWaters_ItemActivate);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // imageListWaters
            // 
            this.imageListWaters.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListWaters.ImageStream")));
            this.imageListWaters.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListWaters.Images.SetKeyName(0, "stream");
            this.imageListWaters.Images.SetKeyName(1, "lake");
            this.imageListWaters.Images.SetKeyName(2, "tank");
            // 
            // labelWaters
            // 
            resources.ApplyResources(this.labelWaters, "labelWaters");
            this.labelWaters.Name = "labelWaters";
            // 
            // buttonBenthos
            // 
            resources.ApplyResources(this.buttonBenthos, "buttonBenthos");
            this.buttonBenthos.Name = "buttonBenthos";
            this.buttonBenthos.UseVisualStyleBackColor = true;
            this.buttonBenthos.Click += new System.EventHandler(this.buttonBenthos_Click);
            // 
            // buttonPlankton
            // 
            resources.ApplyResources(this.buttonPlankton, "buttonPlankton");
            this.buttonPlankton.Name = "buttonPlankton";
            this.buttonPlankton.UseVisualStyleBackColor = true;
            this.buttonPlankton.Click += new System.EventHandler(this.buttonPlankton_Click);
            // 
            // buttonFish
            // 
            resources.ApplyResources(this.buttonFish, "buttonFish");
            this.buttonFish.Name = "buttonFish";
            this.buttonFish.UseVisualStyleBackColor = true;
            this.buttonFish.Click += new System.EventHandler(this.buttonFish_Click);
            // 
            // labelDateEnd
            // 
            resources.ApplyResources(this.labelDateEnd, "labelDateEnd");
            this.labelDateEnd.Name = "labelDateEnd";
            // 
            // labelDateStart
            // 
            resources.ApplyResources(this.labelDateStart, "labelDateStart");
            this.labelDateStart.Name = "labelDateStart";
            // 
            // labelCards
            // 
            resources.ApplyResources(this.labelCards, "labelCards");
            this.labelCards.Name = "labelCards";
            // 
            // labelDates
            // 
            resources.ApplyResources(this.labelDates, "labelDates");
            this.labelDates.Name = "labelDates";
            // 
            // labelCardsNumber
            // 
            resources.ApplyResources(this.labelCardsNumber, "labelCardsNumber");
            this.labelCardsNumber.Name = "labelCardsNumber";
            // 
            // listViewInvestigators
            // 
            resources.ApplyResources(this.listViewInvestigators, "listViewInvestigators");
            this.listViewInvestigators.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewInvestigators.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnInvestigator});
            this.listViewInvestigators.FullRowSelect = true;
            this.listViewInvestigators.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewInvestigators.Name = "listViewInvestigators";
            this.listViewInvestigators.TileSize = new System.Drawing.Size(230, 25);
            this.listViewInvestigators.UseCompatibleStateImageBehavior = false;
            this.listViewInvestigators.View = System.Windows.Forms.View.Tile;
            // 
            // columnInvestigator
            // 
            resources.ApplyResources(this.columnInvestigator, "columnInvestigator");
            // 
            // labelCollectors
            // 
            resources.ApplyResources(this.labelCollectors, "labelCollectors");
            this.labelCollectors.Name = "labelCollectors";
            // 
            // tabPageWaters
            // 
            this.tabPageWaters.Controls.Add(this.label2);
            this.tabPageWaters.Controls.Add(this.label1);
            this.tabPageWaters.Controls.Add(this.searchBox1);
            this.tabPageWaters.Controls.Add(this.waterTree1);
            this.tabPageWaters.Controls.Add(this.listViewStudiedWaters);
            resources.ApplyResources(this.tabPageWaters, "tabPageWaters");
            this.tabPageWaters.Name = "tabPageWaters";
            this.tabPageWaters.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // searchBox1
            // 
            resources.ApplyResources(this.searchBox1, "searchBox1");
            this.searchBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.TextChanged += new System.EventHandler(this.searchBox1_TextChanged);
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
            this.waterTree1.WaterMenuStrip = null;
            this.waterTree1.WaterObject = null;
            this.waterTree1.WaterObjects = null;
            this.waterTree1.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.waterTree1_WaterSelected);
            this.waterTree1.WaterAdded += new Mayfly.Waters.Controls.WaterTree.TreeNodeEventHandler(this.waterTree1_WaterAdded);
            // 
            // listViewStudiedWaters
            // 
            this.listViewStudiedWaters.AllowColumnReorder = true;
            this.listViewStudiedWaters.AllowDrop = true;
            resources.ApplyResources(this.listViewStudiedWaters, "listViewStudiedWaters");
            this.listViewStudiedWaters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewStudiedWaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderWaterName,
            this.columnHeaderOutflow,
            this.columnHeaderMouthToMouth,
            this.columnHeaderLength});
            this.listViewStudiedWaters.ContextMenuStrip = this.contextMenuWater;
            this.listViewStudiedWaters.FullRowSelect = true;
            this.listViewStudiedWaters.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewStudiedWaters.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewStudiedWaters.Groups1"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewStudiedWaters.Groups2")))});
            this.listViewStudiedWaters.MultiSelect = false;
            this.listViewStudiedWaters.Name = "listViewStudiedWaters";
            this.listViewStudiedWaters.ShowItemToolTips = true;
            this.listViewStudiedWaters.UseCompatibleStateImageBehavior = false;
            this.listViewStudiedWaters.View = System.Windows.Forms.View.Details;
            this.listViewStudiedWaters.ItemActivate += new System.EventHandler(this.listViewStudiedWaters_ItemActivate);
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
            // contextMenuWater
            // 
            this.contextMenuWater.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextWaterBrief});
            this.contextMenuWater.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuWater, "contextMenuWater");
            this.contextMenuWater.Opening += new System.ComponentModel.CancelEventHandler(this.contextWater_Opening);
            // 
            // contextWaterBrief
            // 
            this.contextWaterBrief.Name = "contextWaterBrief";
            resources.ApplyResources(this.contextWaterBrief, "contextWaterBrief");
            this.contextWaterBrief.Click += new System.EventHandler(this.surveyItem_Click);
            // 
            // tabPageLocations
            // 
            this.tabPageLocations.Controls.Add(this.buttonLocSave);
            this.tabPageLocations.Controls.Add(this.listViewLocations);
            this.tabPageLocations.Controls.Add(this.labelLocations);
            resources.ApplyResources(this.tabPageLocations, "tabPageLocations");
            this.tabPageLocations.Name = "tabPageLocations";
            this.tabPageLocations.UseVisualStyleBackColor = true;
            // 
            // buttonLocSave
            // 
            resources.ApplyResources(this.buttonLocSave, "buttonLocSave");
            this.buttonLocSave.Name = "buttonLocSave";
            this.buttonLocSave.UseVisualStyleBackColor = true;
            this.buttonLocSave.Click += new System.EventHandler(this.buttonLocSave_Click);
            // 
            // listViewLocations
            // 
            this.listViewLocations.AllowColumnReorder = true;
            this.listViewLocations.AllowDrop = true;
            resources.ApplyResources(this.listViewLocations, "listViewLocations");
            this.listViewLocations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPlcWhere,
            this.columnPlcWhen});
            this.listViewLocations.FullRowSelect = true;
            this.listViewLocations.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLocations.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLocations.Groups1"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLocations.Groups2")))});
            this.listViewLocations.Name = "listViewLocations";
            this.listViewLocations.UseCompatibleStateImageBehavior = false;
            this.listViewLocations.View = System.Windows.Forms.View.Details;
            this.listViewLocations.ItemActivate += new System.EventHandler(this.listViewLocations_ItemActivate);
            // 
            // columnPlcWhere
            // 
            resources.ApplyResources(this.columnPlcWhere, "columnPlcWhere");
            // 
            // columnPlcWhen
            // 
            resources.ApplyResources(this.columnPlcWhen, "columnPlcWhen");
            // 
            // labelLocations
            // 
            resources.ApplyResources(this.labelLocations, "labelLocations");
            this.labelLocations.Name = "labelLocations";
            // 
            // tabPageCalendar
            // 
            this.tabPageCalendar.Controls.Add(this.label3);
            this.tabPageCalendar.Controls.Add(this.monthCalendar1);
            resources.ApplyResources(this.tabPageCalendar, "tabPageCalendar");
            this.tabPageCalendar.Name = "tabPageCalendar";
            this.tabPageCalendar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // monthCalendar1
            // 
            resources.ApplyResources(this.monthCalendar1, "monthCalendar1");
            this.monthCalendar1.MaxSelectionCount = 365;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowToday = false;
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
            // processDisplay1
            // 
            this.processDisplay1.Default = null;
            this.processDisplay1.Look = null;
            this.processDisplay1.MaximalInterval = 2000;
            this.processDisplay1.ProgressBar = null;
            this.processDisplay1.StatusLog = null;
            // 
            // status1
            // 
            this.status1.Default = null;
            this.status1.MaximalInterval = 2000;
            this.status1.StatusLog = this.toolStripStatusLabel1;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.tabPageWaters.ResumeLayout(false);
            this.tabPageWaters.PerformLayout();
            this.contextMenuWater.ResumeLayout(false);
            this.tabPageLocations.ResumeLayout(false);
            this.tabPageLocations.PerformLayout();
            this.tabPageCalendar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.ToolStripMenuItem menuService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPageWaters;
        private System.Windows.Forms.ListView listViewStudiedWaters;
        private System.Windows.Forms.ColumnHeader columnHeaderWaterName;
        private System.Windows.Forms.ColumnHeader columnHeaderOutflow;
        private System.Windows.Forms.ColumnHeader columnHeaderMouthToMouth;
        private System.Windows.Forms.ColumnHeader columnHeaderLength;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemWaters;
        private Controls.ProcessDisplay processDisplay1;
        private Waters.Controls.WaterTree waterTree1;
        private System.Windows.Forms.ListView listViewInvestigators;
        private System.Windows.Forms.ColumnHeader columnInvestigator;
        private System.Windows.Forms.Label labelCollectors;
        private System.Windows.Forms.Label labelDateEnd;
        private System.Windows.Forms.Label labelDateStart;
        private System.Windows.Forms.Label labelCards;
        private System.Windows.Forms.Label labelDates;
        private System.Windows.Forms.Label labelCardsNumber;
        private System.Windows.Forms.Button buttonBenthos;
        private System.Windows.Forms.Button buttonPlankton;
        private System.Windows.Forms.Button buttonFish;
        private System.Windows.Forms.ListView listViewWaters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label labelWaters;
        private System.Windows.Forms.ImageList imageListWaters;
        private System.Windows.Forms.TabPage tabPageLocations;
        private System.Windows.Forms.Label labelLocations;
        private System.Windows.Forms.ListView listViewLocations;
        private System.Windows.Forms.ColumnHeader columnPlcWhere;
        private System.Windows.Forms.ColumnHeader columnPlcWhen;
        private System.Windows.Forms.ToolStripMenuItem menuItemLocations;
        private System.Windows.Forms.Button buttonLocSave;
        private Controls.SearchBox searchBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Controls.Status status1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuWater;
        private System.Windows.Forms.ToolStripMenuItem contextWaterBrief;
        private System.Windows.Forms.ToolStripMenuItem menuItemCalendar;
        private System.Windows.Forms.TabPage tabPageCalendar;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label3;
    }
}

