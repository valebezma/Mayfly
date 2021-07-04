namespace Mayfly.Bacterioplankton.Explorer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelWaters = new System.Windows.Forms.Label();
            this.labelCards = new System.Windows.Forms.Label();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.labelArtefacts = new System.Windows.Forms.Label();
            this.pictureBoxArtefacts = new System.Windows.Forms.PictureBox();
            this.listViewInvestigators = new System.Windows.Forms.ListView();
            this.HeaderInvestigator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewWaters = new System.Windows.Forms.ListView();
            this.HeaderWater = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListWaters = new System.Windows.Forms.ImageList(this.components);
            this.labelDateEnd = new System.Windows.Forms.Label();
            this.labelDateStart = new System.Windows.Forms.Label();
            this.labelCollectors = new System.Windows.Forms.Label();
            this.labelDates = new System.Windows.Forms.Label();
            this.labelCardsNumber = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageArtefacts = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageArtefactSpecies = new System.Windows.Forms.TabPage();
            this.labelArtefactSpecies = new System.Windows.Forms.Label();
            this.pictureBoxArtefactSpecies = new System.Windows.Forms.PictureBox();
            this.labelArtefactSpeciesCount = new System.Windows.Forms.Label();
            this.spreadSheetArtefactSpecies = new Mayfly.Controls.SpreadSheet();
            this.columnArtefactSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnArtefactValidName = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.columnArtefactN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processDisplay = new Mayfly.Controls.ProcessDisplay(this.components);
            this.statusLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.statusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageCard = new System.Windows.Forms.TabPage();
            this.labelCardCount = new System.Windows.Forms.Label();
            this.spreadSheetCard = new Mayfly.Controls.SpreadSheet();
            this.columnCardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardInvestigator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardWater = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardWhen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardDepth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardAbundance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardBiomass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCardComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextCard = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCardOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCardLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.contextCardPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCardPrintNote = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCardPrintFull = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.contextCardRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.labelLogCount = new System.Windows.Forms.Label();
            this.buttonSelectLog = new System.Windows.Forms.Button();
            this.spreadSheetLog = new Mayfly.Controls.SpreadSheet();
            this.columnLogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLogSpc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLogAbundance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLogBiomass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextLogOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLogCard = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLogInds = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.contextLogRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageInd = new System.Windows.Forms.TabPage();
            this.buttonSelectInd = new System.Windows.Forms.Button();
            this.labelIndCount = new System.Windows.Forms.Label();
            this.spreadSheetInd = new Mayfly.Controls.SpreadSheet();
            this.columnIndID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnIndSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnIndLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIndDiameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnIndMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnIndComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextInd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextIndOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.contextIndCard = new System.Windows.Forms.ToolStripMenuItem();
            this.contextIndLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.contextIndRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusQuantity = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMass = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBackupCards = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveSet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSample = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemArtefactsSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLoadCards = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLoadSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLoadSpc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLoadLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLoadIndividuals = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCards = new System.Windows.Forms.ToolStripMenuItem();
            this.additionalFactorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAssignVariants = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCardFindEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCardPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCardPrintFull = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCardPrintNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemComCom = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIndividuals = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIndPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.loaderInd = new System.ComponentModel.BackgroundWorker();
            this.loaderLog = new System.ComponentModel.BackgroundWorker();
            this.loaderCard = new System.ComponentModel.BackgroundWorker();
            this.loaderData = new System.ComponentModel.BackgroundWorker();
            this.dataSaver = new System.ComponentModel.BackgroundWorker();
            this.loaderIndExtended = new System.ComponentModel.BackgroundWorker();
            this.loaderLogExtended = new System.ComponentModel.BackgroundWorker();
            this.taskDialogSave = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSaveAll = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.fbDialogBackup = new System.Windows.Forms.FolderBrowserDialog();
            this.tdLog = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbLogRename = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbLogCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdInd = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbIndRename = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbIndCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.mathLog = new Mayfly.Mathematics.MathAdapter(this.components);
            this.mathCard = new Mayfly.Mathematics.MathAdapter(this.components);
            this.mathInd = new Mayfly.Mathematics.MathAdapter(this.components);
            this.comparerLog = new System.ComponentModel.BackgroundWorker();
            this.specTipper = new System.ComponentModel.BackgroundWorker();
            this.specUpdater = new System.ComponentModel.BackgroundWorker();
            this.tabPageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtefacts)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageArtefacts.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageArtefactSpecies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtefactSpecies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetArtefactSpecies)).BeginInit();
            this.tabPageCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCard)).BeginInit();
            this.contextCard.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).BeginInit();
            this.contextLog.SuspendLayout();
            this.tabPageInd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetInd)).BeginInit();
            this.contextInd.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWaters
            // 
            resources.ApplyResources(this.labelWaters, "labelWaters");
            this.labelWaters.Name = "labelWaters";
            // 
            // labelCards
            // 
            resources.ApplyResources(this.labelCards, "labelCards");
            this.labelCards.Name = "labelCards";
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.AllowDrop = true;
            this.tabPageInfo.Controls.Add(this.labelArtefacts);
            this.tabPageInfo.Controls.Add(this.pictureBoxArtefacts);
            this.tabPageInfo.Controls.Add(this.listViewInvestigators);
            this.tabPageInfo.Controls.Add(this.listViewWaters);
            this.tabPageInfo.Controls.Add(this.labelWaters);
            this.tabPageInfo.Controls.Add(this.labelDateEnd);
            this.tabPageInfo.Controls.Add(this.labelDateStart);
            this.tabPageInfo.Controls.Add(this.labelCards);
            this.tabPageInfo.Controls.Add(this.labelCollectors);
            this.tabPageInfo.Controls.Add(this.labelDates);
            this.tabPageInfo.Controls.Add(this.labelCardsNumber);
            resources.ApplyResources(this.tabPageInfo, "tabPageInfo");
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            this.tabPageInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.cards_DragDrop);
            this.tabPageInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.cards_DragEnter);
            this.tabPageInfo.DragLeave += new System.EventHandler(this.cards_DragLeave);
            // 
            // labelArtefacts
            // 
            resources.ApplyResources(this.labelArtefacts, "labelArtefacts");
            this.labelArtefacts.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelArtefacts.Name = "labelArtefacts";
            this.labelArtefacts.DoubleClick += new System.EventHandler(this.menuItemArtefactsSearch_Click);
            // 
            // pictureBoxArtefacts
            // 
            resources.ApplyResources(this.pictureBoxArtefacts, "pictureBoxArtefacts");
            this.pictureBoxArtefacts.Name = "pictureBoxArtefacts";
            this.pictureBoxArtefacts.TabStop = false;
            this.pictureBoxArtefacts.DoubleClick += new System.EventHandler(this.menuItemArtefactsSearch_Click);
            // 
            // listViewInvestigators
            // 
            resources.ApplyResources(this.listViewInvestigators, "listViewInvestigators");
            this.listViewInvestigators.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewInvestigators.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderInvestigator});
            this.listViewInvestigators.FullRowSelect = true;
            this.listViewInvestigators.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewInvestigators.Name = "listViewInvestigators";
            this.listViewInvestigators.TileSize = new System.Drawing.Size(230, 25);
            this.listViewInvestigators.UseCompatibleStateImageBehavior = false;
            this.listViewInvestigators.View = System.Windows.Forms.View.Tile;
            this.listViewInvestigators.ItemActivate += new System.EventHandler(this.listViewInvestigators_ItemActivate);
            // 
            // HeaderInvestigator
            // 
            resources.ApplyResources(this.HeaderInvestigator, "HeaderInvestigator");
            // 
            // listViewWaters
            // 
            resources.ApplyResources(this.listViewWaters, "listViewWaters");
            this.listViewWaters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewWaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderWater});
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
            // HeaderWater
            // 
            resources.ApplyResources(this.HeaderWater, "HeaderWater");
            // 
            // imageListWaters
            // 
            this.imageListWaters.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListWaters.ImageStream")));
            this.imageListWaters.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListWaters.Images.SetKeyName(0, "stream");
            this.imageListWaters.Images.SetKeyName(1, "lake");
            this.imageListWaters.Images.SetKeyName(2, "tank");
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
            // labelCollectors
            // 
            resources.ApplyResources(this.labelCollectors, "labelCollectors");
            this.labelCollectors.Name = "labelCollectors";
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
            this.labelCardsNumber.DoubleClick += new System.EventHandler(this.menuItemLoadCards_Click);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageInfo);
            this.tabControl.Controls.Add(this.tabPageArtefacts);
            this.tabControl.Controls.Add(this.tabPageCard);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Controls.Add(this.tabPageInd);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tab_Changed);
            // 
            // tabPageArtefacts
            // 
            this.tabPageArtefacts.Controls.Add(this.tabControl1);
            resources.ApplyResources(this.tabPageArtefacts, "tabPageArtefacts");
            this.tabPageArtefacts.Name = "tabPageArtefacts";
            this.tabPageArtefacts.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageArtefactSpecies);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageArtefactSpecies
            // 
            this.tabPageArtefactSpecies.Controls.Add(this.labelArtefactSpecies);
            this.tabPageArtefactSpecies.Controls.Add(this.pictureBoxArtefactSpecies);
            this.tabPageArtefactSpecies.Controls.Add(this.labelArtefactSpeciesCount);
            this.tabPageArtefactSpecies.Controls.Add(this.spreadSheetArtefactSpecies);
            resources.ApplyResources(this.tabPageArtefactSpecies, "tabPageArtefactSpecies");
            this.tabPageArtefactSpecies.Name = "tabPageArtefactSpecies";
            this.tabPageArtefactSpecies.UseVisualStyleBackColor = true;
            // 
            // labelArtefactSpecies
            // 
            resources.ApplyResources(this.labelArtefactSpecies, "labelArtefactSpecies");
            this.labelArtefactSpecies.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelArtefactSpecies.Name = "labelArtefactSpecies";
            // 
            // pictureBoxArtefactSpecies
            // 
            resources.ApplyResources(this.pictureBoxArtefactSpecies, "pictureBoxArtefactSpecies");
            this.pictureBoxArtefactSpecies.Name = "pictureBoxArtefactSpecies";
            this.pictureBoxArtefactSpecies.TabStop = false;
            // 
            // labelArtefactSpeciesCount
            // 
            resources.ApplyResources(this.labelArtefactSpeciesCount, "labelArtefactSpeciesCount");
            this.labelArtefactSpeciesCount.Name = "labelArtefactSpeciesCount";
            // 
            // spreadSheetArtefactSpecies
            // 
            resources.ApplyResources(this.spreadSheetArtefactSpecies, "spreadSheetArtefactSpecies");
            this.spreadSheetArtefactSpecies.CellPadding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.spreadSheetArtefactSpecies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnArtefactSpecies,
            this.columnArtefactValidName,
            this.columnArtefactN});
            this.spreadSheetArtefactSpecies.DefaultDecimalPlaces = 0;
            this.spreadSheetArtefactSpecies.Display = this.processDisplay;
            this.spreadSheetArtefactSpecies.MultiSelect = false;
            this.spreadSheetArtefactSpecies.Name = "spreadSheetArtefactSpecies";
            // 
            // columnArtefactSpecies
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnArtefactSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnArtefactSpecies, "columnArtefactSpecies");
            this.columnArtefactSpecies.Name = "columnArtefactSpecies";
            // 
            // columnArtefactValidName
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.columnArtefactValidName.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnArtefactValidName, "columnArtefactValidName");
            this.columnArtefactValidName.Image = null;
            this.columnArtefactValidName.Name = "columnArtefactValidName";
            this.columnArtefactValidName.ReadOnly = true;
            this.columnArtefactValidName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnArtefactN
            // 
            resources.ApplyResources(this.columnArtefactN, "columnArtefactN");
            this.columnArtefactN.Name = "columnArtefactN";
            this.columnArtefactN.ReadOnly = true;
            // 
            // processDisplay
            // 
            this.processDisplay.Default = null;
            this.processDisplay.Look = null;
            this.processDisplay.MaximalInterval = 2000;
            this.processDisplay.ProgressBar = this.statusLoading;
            this.processDisplay.StatusLog = this.statusProcess;
            // 
            // statusLoading
            // 
            resources.ApplyResources(this.statusLoading, "statusLoading");
            this.statusLoading.Margin = new System.Windows.Forms.Padding(1, 6, 1, 6);
            this.statusLoading.Name = "statusLoading";
            this.statusLoading.Value = 50;
            // 
            // statusProcess
            // 
            this.statusProcess.Name = "statusProcess";
            resources.ApplyResources(this.statusProcess, "statusProcess");
            this.statusProcess.Spring = true;
            // 
            // tabPageCard
            // 
            this.tabPageCard.Controls.Add(this.labelCardCount);
            this.tabPageCard.Controls.Add(this.spreadSheetCard);
            resources.ApplyResources(this.tabPageCard, "tabPageCard");
            this.tabPageCard.Name = "tabPageCard";
            this.tabPageCard.UseVisualStyleBackColor = true;
            // 
            // labelCardCount
            // 
            resources.ApplyResources(this.labelCardCount, "labelCardCount");
            this.labelCardCount.Name = "labelCardCount";
            // 
            // spreadSheetCard
            // 
            resources.ApplyResources(this.spreadSheetCard, "spreadSheetCard");
            this.spreadSheetCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCardID,
            this.columnCardInvestigator,
            this.columnCardWater,
            this.columnCardLabel,
            this.columnCardWhen,
            this.columnCardVolume,
            this.columnCardDepth,
            this.columnCardAbundance,
            this.columnCardBiomass,
            this.columnCardComments});
            this.spreadSheetCard.Display = this.processDisplay;
            this.spreadSheetCard.IsLog = true;
            this.spreadSheetCard.Name = "spreadSheetCard";
            this.spreadSheetCard.RowMenu = this.contextCard;
            this.spreadSheetCard.RowMenuLaunchableItemIndex = 0;
            this.spreadSheetCard.Filtered += new System.EventHandler(this.spreadSheetCard_Filtered);
            this.spreadSheetCard.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCard_CellValueChanged);
            // 
            // columnCardID
            // 
            dataGridViewCellStyle3.NullValue = null;
            this.columnCardID.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnCardID, "columnCardID");
            this.columnCardID.Name = "columnCardID";
            this.columnCardID.ReadOnly = true;
            this.columnCardID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnCardInvestigator
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnCardInvestigator.DefaultCellStyle = dataGridViewCellStyle4;
            this.columnCardInvestigator.FillWeight = 150F;
            resources.ApplyResources(this.columnCardInvestigator, "columnCardInvestigator");
            this.columnCardInvestigator.Name = "columnCardInvestigator";
            this.columnCardInvestigator.ReadOnly = true;
            // 
            // columnCardWater
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnCardWater.DefaultCellStyle = dataGridViewCellStyle5;
            this.columnCardWater.FillWeight = 200F;
            resources.ApplyResources(this.columnCardWater, "columnCardWater");
            this.columnCardWater.Name = "columnCardWater";
            this.columnCardWater.ReadOnly = true;
            // 
            // columnCardLabel
            // 
            resources.ApplyResources(this.columnCardLabel, "columnCardLabel");
            this.columnCardLabel.Name = "columnCardLabel";
            // 
            // columnCardWhen
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.columnCardWhen.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.columnCardWhen, "columnCardWhen");
            this.columnCardWhen.Name = "columnCardWhen";
            this.columnCardWhen.ReadOnly = true;
            // 
            // columnCardVolume
            // 
            dataGridViewCellStyle7.Format = "N4";
            this.columnCardVolume.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.columnCardVolume, "columnCardVolume");
            this.columnCardVolume.Name = "columnCardVolume";
            // 
            // columnCardDepth
            // 
            dataGridViewCellStyle8.Format = "0.0";
            this.columnCardDepth.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.columnCardDepth, "columnCardDepth");
            this.columnCardDepth.Name = "columnCardDepth";
            // 
            // columnCardAbundance
            // 
            dataGridViewCellStyle9.Format = "N3";
            this.columnCardAbundance.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.columnCardAbundance, "columnCardAbundance");
            this.columnCardAbundance.Name = "columnCardAbundance";
            // 
            // columnCardBiomass
            // 
            dataGridViewCellStyle10.Format = "N3";
            this.columnCardBiomass.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.columnCardBiomass, "columnCardBiomass");
            this.columnCardBiomass.Name = "columnCardBiomass";
            // 
            // columnCardComments
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnCardComments.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.columnCardComments, "columnCardComments");
            this.columnCardComments.Name = "columnCardComments";
            // 
            // contextCard
            // 
            this.contextCard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCardOpen,
            this.contextCardLog,
            this.toolStripSeparator5,
            this.contextCardPrint,
            this.contextCardPrintNote,
            this.contextCardPrintFull,
            this.toolStripSeparator8,
            this.contextCardRemove});
            this.contextCard.Name = "contextMenuStrip_header";
            resources.ApplyResources(this.contextCard, "contextCard");
            this.contextCard.Opening += new System.ComponentModel.CancelEventHandler(this.contextCard_Opening);
            // 
            // contextCardOpen
            // 
            resources.ApplyResources(this.contextCardOpen, "contextCardOpen");
            this.contextCardOpen.Name = "contextCardOpen";
            this.contextCardOpen.Click += new System.EventHandler(this.contextCardOpen_Click);
            // 
            // contextCardLog
            // 
            resources.ApplyResources(this.contextCardLog, "contextCardLog");
            this.contextCardLog.Name = "contextCardLog";
            this.contextCardLog.Click += new System.EventHandler(this.contextCardLog_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // contextCardPrint
            // 
            this.contextCardPrint.Name = "contextCardPrint";
            resources.ApplyResources(this.contextCardPrint, "contextCardPrint");
            this.contextCardPrint.Click += new System.EventHandler(this.contextCardPrint_Click);
            // 
            // contextCardPrintNote
            // 
            this.contextCardPrintNote.Name = "contextCardPrintNote";
            resources.ApplyResources(this.contextCardPrintNote, "contextCardPrintNote");
            this.contextCardPrintNote.Click += new System.EventHandler(this.contextCardPrintNotes_Click);
            // 
            // contextCardPrintFull
            // 
            resources.ApplyResources(this.contextCardPrintFull, "contextCardPrintFull");
            this.contextCardPrintFull.Name = "contextCardPrintFull";
            this.contextCardPrintFull.Click += new System.EventHandler(this.contextCardPrintFull_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // contextCardRemove
            // 
            resources.ApplyResources(this.contextCardRemove, "contextCardRemove");
            this.contextCardRemove.Name = "contextCardRemove";
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.labelLogCount);
            this.tabPageLog.Controls.Add(this.buttonSelectLog);
            this.tabPageLog.Controls.Add(this.spreadSheetLog);
            resources.ApplyResources(this.tabPageLog, "tabPageLog");
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // labelLogCount
            // 
            resources.ApplyResources(this.labelLogCount, "labelLogCount");
            this.labelLogCount.Name = "labelLogCount";
            // 
            // buttonSelectLog
            // 
            resources.ApplyResources(this.buttonSelectLog, "buttonSelectLog");
            this.buttonSelectLog.Name = "buttonSelectLog";
            this.buttonSelectLog.UseVisualStyleBackColor = true;
            // 
            // spreadSheetLog
            // 
            this.spreadSheetLog.AllowUserToHideRows = true;
            resources.ApplyResources(this.spreadSheetLog, "spreadSheetLog");
            this.spreadSheetLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnLogID,
            this.columnLogSpc,
            this.columnLogAbundance,
            this.columnLogBiomass});
            this.spreadSheetLog.Display = this.processDisplay;
            this.spreadSheetLog.IsLog = true;
            this.spreadSheetLog.Name = "spreadSheetLog";
            this.spreadSheetLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.spreadSheetLog.RowMenu = this.contextLog;
            this.spreadSheetLog.RowMenuLaunchableItemIndex = 0;
            this.spreadSheetLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // columnLogID
            // 
            resources.ApplyResources(this.columnLogID, "columnLogID");
            this.columnLogID.Name = "columnLogID";
            this.columnLogID.ReadOnly = true;
            // 
            // columnLogSpc
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnLogSpc.DefaultCellStyle = dataGridViewCellStyle12;
            this.columnLogSpc.FillWeight = 200F;
            resources.ApplyResources(this.columnLogSpc, "columnLogSpc");
            this.columnLogSpc.Name = "columnLogSpc";
            // 
            // columnLogAbundance
            // 
            dataGridViewCellStyle13.Format = "N0";
            this.columnLogAbundance.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.columnLogAbundance, "columnLogAbundance");
            this.columnLogAbundance.Name = "columnLogAbundance";
            this.columnLogAbundance.ReadOnly = true;
            // 
            // columnLogBiomass
            // 
            dataGridViewCellStyle14.Format = "N5";
            this.columnLogBiomass.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.columnLogBiomass, "columnLogBiomass");
            this.columnLogBiomass.Name = "columnLogBiomass";
            this.columnLogBiomass.ReadOnly = true;
            // 
            // contextLog
            // 
            this.contextLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextLogOpen,
            this.contextLogCard,
            this.contextLogInds,
            this.toolStripSeparator7,
            this.contextLogRemove});
            this.contextLog.Name = "contextMenuStripLog";
            resources.ApplyResources(this.contextLog, "contextLog");
            // 
            // contextLogOpen
            // 
            resources.ApplyResources(this.contextLogOpen, "contextLogOpen");
            this.contextLogOpen.Name = "contextLogOpen";
            this.contextLogOpen.Click += new System.EventHandler(this.contextLogOpen_Click);
            // 
            // contextLogCard
            // 
            resources.ApplyResources(this.contextLogCard, "contextLogCard");
            this.contextLogCard.Name = "contextLogCard";
            // 
            // contextLogInds
            // 
            resources.ApplyResources(this.contextLogInds, "contextLogInds");
            this.contextLogInds.Name = "contextLogInds";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // contextLogRemove
            // 
            resources.ApplyResources(this.contextLogRemove, "contextLogRemove");
            this.contextLogRemove.Name = "contextLogRemove";
            // 
            // tabPageInd
            // 
            this.tabPageInd.Controls.Add(this.buttonSelectInd);
            this.tabPageInd.Controls.Add(this.labelIndCount);
            this.tabPageInd.Controls.Add(this.spreadSheetInd);
            resources.ApplyResources(this.tabPageInd, "tabPageInd");
            this.tabPageInd.Name = "tabPageInd";
            this.tabPageInd.UseVisualStyleBackColor = true;
            // 
            // buttonSelectInd
            // 
            resources.ApplyResources(this.buttonSelectInd, "buttonSelectInd");
            this.buttonSelectInd.Name = "buttonSelectInd";
            this.buttonSelectInd.UseVisualStyleBackColor = true;
            this.buttonSelectInd.Click += new System.EventHandler(this.buttonSelectInd_Click);
            // 
            // labelIndCount
            // 
            resources.ApplyResources(this.labelIndCount, "labelIndCount");
            this.labelIndCount.Name = "labelIndCount";
            // 
            // spreadSheetInd
            // 
            this.spreadSheetInd.AllowUserToDeleteRows = true;
            resources.ApplyResources(this.spreadSheetInd, "spreadSheetInd");
            this.spreadSheetInd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndID,
            this.columnIndSpecies,
            this.columnIndLength,
            this.ColumnIndDiameter,
            this.columnIndMass,
            this.columnIndComments});
            this.spreadSheetInd.DefaultDecimalPlaces = 0;
            this.spreadSheetInd.Display = this.processDisplay;
            this.spreadSheetInd.IsLog = true;
            this.spreadSheetInd.Name = "spreadSheetInd";
            this.spreadSheetInd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.spreadSheetInd.RowMenu = this.contextInd;
            this.spreadSheetInd.RowMenuLaunchableItemIndex = 0;
            this.spreadSheetInd.RowTemplate.Height = 20;
            this.spreadSheetInd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetInd.Filtered += new System.EventHandler(this.spreadSheetInd_Filtered);
            this.spreadSheetInd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetInd_CellValueChanged);
            this.spreadSheetInd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.spreadSheetInd_UserDeletingRow);
            // 
            // columnIndID
            // 
            resources.ApplyResources(this.columnIndID, "columnIndID");
            this.columnIndID.Name = "columnIndID";
            this.columnIndID.ReadOnly = true;
            // 
            // columnIndSpecies
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnIndSpecies.DefaultCellStyle = dataGridViewCellStyle15;
            this.columnIndSpecies.FillWeight = 200F;
            resources.ApplyResources(this.columnIndSpecies, "columnIndSpecies");
            this.columnIndSpecies.Name = "columnIndSpecies";
            // 
            // columnIndLength
            // 
            dataGridViewCellStyle16.Format = "N1";
            this.columnIndLength.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.columnIndLength, "columnIndLength");
            this.columnIndLength.Name = "columnIndLength";
            // 
            // ColumnIndDiameter
            // 
            resources.ApplyResources(this.ColumnIndDiameter, "ColumnIndDiameter");
            this.ColumnIndDiameter.Name = "ColumnIndDiameter";
            // 
            // columnIndMass
            // 
            dataGridViewCellStyle17.Format = "N5";
            dataGridViewCellStyle17.NullValue = null;
            this.columnIndMass.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.columnIndMass, "columnIndMass");
            this.columnIndMass.Name = "columnIndMass";
            this.columnIndMass.ReadOnly = true;
            // 
            // columnIndComments
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnIndComments.DefaultCellStyle = dataGridViewCellStyle18;
            this.columnIndComments.FillWeight = 250F;
            resources.ApplyResources(this.columnIndComments, "columnIndComments");
            this.columnIndComments.Name = "columnIndComments";
            // 
            // contextInd
            // 
            this.contextInd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextIndOpen,
            this.contextIndCard,
            this.contextIndLog,
            this.toolStripSeparator6,
            this.contextIndRemove});
            this.contextInd.Name = "contextMenuStrip_header";
            resources.ApplyResources(this.contextInd, "contextInd");
            // 
            // contextIndOpen
            // 
            resources.ApplyResources(this.contextIndOpen, "contextIndOpen");
            this.contextIndOpen.Name = "contextIndOpen";
            this.contextIndOpen.Click += new System.EventHandler(this.contextIndOpen_Click);
            // 
            // contextIndCard
            // 
            resources.ApplyResources(this.contextIndCard, "contextIndCard");
            this.contextIndCard.Name = "contextIndCard";
            this.contextIndCard.Click += new System.EventHandler(this.contextIndCard_Click);
            // 
            // contextIndLog
            // 
            this.contextIndLog.Name = "contextIndLog";
            resources.ApplyResources(this.contextIndLog, "contextIndLog");
            this.contextIndLog.Click += new System.EventHandler(this.contextIndLog_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // contextIndRemove
            // 
            resources.ApplyResources(this.contextIndRemove, "contextIndRemove");
            this.contextIndRemove.Name = "contextIndRemove";
            this.contextIndRemove.Click += new System.EventHandler(this.contextIndDelete_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusQuantity,
            this.statusMass,
            this.statusProcess,
            this.statusLoading});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // statusQuantity
            // 
            resources.ApplyResources(this.statusQuantity, "statusQuantity");
            this.statusQuantity.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusQuantity.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.statusQuantity.Name = "statusQuantity";
            // 
            // statusMass
            // 
            resources.ApplyResources(this.statusMass, "statusMass");
            this.statusMass.Name = "statusMass";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemSample,
            this.menuItemCards,
            this.menuItemLog,
            this.menuItemIndividuals,
            this.menuItemService});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddData,
            this.toolStripSeparator10,
            this.menuItemSave,
            this.menuItemBackupCards,
            this.menuItemSaveSet,
            this.toolStripSeparator9,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            resources.ApplyResources(this.menuItemFile, "menuItemFile");
            // 
            // menuItemAddData
            // 
            resources.ApplyResources(this.menuItemAddData, "menuItemAddData");
            this.menuItemAddData.Name = "menuItemAddData";
            this.menuItemAddData.Click += new System.EventHandler(this.menuItemAddData_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            // 
            // menuItemSave
            // 
            resources.ApplyResources(this.menuItemSave, "menuItemSave");
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemBackupCards
            // 
            resources.ApplyResources(this.menuItemBackupCards, "menuItemBackupCards");
            this.menuItemBackupCards.Name = "menuItemBackupCards";
            this.menuItemBackupCards.Click += new System.EventHandler(this.menuItemBackup_Click);
            // 
            // menuItemSaveSet
            // 
            resources.ApplyResources(this.menuItemSaveSet, "menuItemSaveSet");
            this.menuItemSaveSet.Name = "menuItemSaveSet";
            this.menuItemSaveSet.Click += new System.EventHandler(this.menuItemSaveSet_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            resources.ApplyResources(this.menuItemExit, "menuItemExit");
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemSample
            // 
            this.menuItemSample.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemArtefactsSearch,
            this.toolStripSeparator4,
            this.menuItemLoadCards,
            this.menuItemLoadSpecies,
            this.menuItemLoadIndividuals});
            this.menuItemSample.Name = "menuItemSample";
            resources.ApplyResources(this.menuItemSample, "menuItemSample");
            this.menuItemSample.DropDownOpening += new System.EventHandler(this.menuItemSample_DropDownOpening);
            // 
            // menuItemArtefactsSearch
            // 
            this.menuItemArtefactsSearch.Name = "menuItemArtefactsSearch";
            resources.ApplyResources(this.menuItemArtefactsSearch, "menuItemArtefactsSearch");
            this.menuItemArtefactsSearch.Click += new System.EventHandler(this.menuItemArtefactsSearch_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // menuItemLoadCards
            // 
            this.menuItemLoadCards.Name = "menuItemLoadCards";
            resources.ApplyResources(this.menuItemLoadCards, "menuItemLoadCards");
            this.menuItemLoadCards.Click += new System.EventHandler(this.menuItemLoadCards_Click);
            // 
            // menuItemLoadSpecies
            // 
            this.menuItemLoadSpecies.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLoadSpc,
            this.menuItemLoadLog});
            this.menuItemLoadSpecies.Name = "menuItemLoadSpecies";
            resources.ApplyResources(this.menuItemLoadSpecies, "menuItemLoadSpecies");
            // 
            // menuItemLoadSpc
            // 
            this.menuItemLoadSpc.Name = "menuItemLoadSpc";
            resources.ApplyResources(this.menuItemLoadSpc, "menuItemLoadSpc");
            // 
            // menuItemLoadLog
            // 
            this.menuItemLoadLog.Name = "menuItemLoadLog";
            resources.ApplyResources(this.menuItemLoadLog, "menuItemLoadLog");
            this.menuItemLoadLog.Click += new System.EventHandler(this.menuItemLoadLog_Click);
            // 
            // menuItemLoadIndividuals
            // 
            this.menuItemLoadIndividuals.Name = "menuItemLoadIndividuals";
            resources.ApplyResources(this.menuItemLoadIndividuals, "menuItemLoadIndividuals");
            this.menuItemLoadIndividuals.Click += new System.EventHandler(this.menuItemLoadInd_Click);
            // 
            // menuItemCards
            // 
            this.menuItemCards.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.additionalFactorsToolStripMenuItem,
            this.menuItemCardFindEmpty,
            this.toolStripSeparator1,
            this.menuItemCardPrint,
            this.menuItemCardPrintFull,
            this.menuItemCardPrintNotes});
            this.menuItemCards.Name = "menuItemCards";
            resources.ApplyResources(this.menuItemCards, "menuItemCards");
            // 
            // additionalFactorsToolStripMenuItem
            // 
            this.additionalFactorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAssignVariants});
            this.additionalFactorsToolStripMenuItem.Name = "additionalFactorsToolStripMenuItem";
            resources.ApplyResources(this.additionalFactorsToolStripMenuItem, "additionalFactorsToolStripMenuItem");
            // 
            // menuItemAssignVariants
            // 
            this.menuItemAssignVariants.Name = "menuItemAssignVariants";
            resources.ApplyResources(this.menuItemAssignVariants, "menuItemAssignVariants");
            this.menuItemAssignVariants.Click += new System.EventHandler(this.menuItemAssignVariants_Click);
            // 
            // menuItemCardFindEmpty
            // 
            this.menuItemCardFindEmpty.Name = "menuItemCardFindEmpty";
            resources.ApplyResources(this.menuItemCardFindEmpty, "menuItemCardFindEmpty");
            this.menuItemCardFindEmpty.Click += new System.EventHandler(this.menuItemCardFindEmpty_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuItemCardPrint
            // 
            resources.ApplyResources(this.menuItemCardPrint, "menuItemCardPrint");
            this.menuItemCardPrint.Name = "menuItemCardPrint";
            this.menuItemCardPrint.Click += new System.EventHandler(this.menuItemCardPrint_Click);
            // 
            // menuItemCardPrintFull
            // 
            this.menuItemCardPrintFull.Name = "menuItemCardPrintFull";
            resources.ApplyResources(this.menuItemCardPrintFull, "menuItemCardPrintFull");
            this.menuItemCardPrintFull.Click += new System.EventHandler(this.menuItemCardPrint_Click);
            // 
            // menuItemCardPrintNotes
            // 
            this.menuItemCardPrintNotes.Name = "menuItemCardPrintNotes";
            resources.ApplyResources(this.menuItemCardPrintNotes, "menuItemCardPrintNotes");
            this.menuItemCardPrintNotes.Click += new System.EventHandler(this.menuItemCardPrintNotes_Click);
            // 
            // menuItemLog
            // 
            this.menuItemLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemComCom});
            this.menuItemLog.Name = "menuItemLog";
            resources.ApplyResources(this.menuItemLog, "menuItemLog");
            // 
            // menuItemComCom
            // 
            this.menuItemComCom.Name = "menuItemComCom";
            resources.ApplyResources(this.menuItemComCom, "menuItemComCom");
            this.menuItemComCom.Click += new System.EventHandler(this.menuItemComCom_Click);
            // 
            // menuItemIndividuals
            // 
            this.menuItemIndividuals.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemIndPrint});
            this.menuItemIndividuals.Name = "menuItemIndividuals";
            resources.ApplyResources(this.menuItemIndividuals, "menuItemIndividuals");
            // 
            // menuItemIndPrint
            // 
            resources.ApplyResources(this.menuItemIndPrint, "menuItemIndPrint");
            this.menuItemIndPrint.Name = "menuItemIndPrint";
            this.menuItemIndPrint.Click += new System.EventHandler(this.menuItemIndPrint_Click);
            // 
            // menuItemService
            // 
            this.menuItemService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.menuItemAbout});
            this.menuItemService.Name = "menuItemService";
            resources.ApplyResources(this.menuItemService, "menuItemService");
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
            // loaderInd
            // 
            this.loaderInd.WorkerReportsProgress = true;
            this.loaderInd.DoWork += new System.ComponentModel.DoWorkEventHandler(this.indLoader_DoWork);
            this.loaderInd.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.loaderInd.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.indLoader_RunWorkerCompleted);
            // 
            // loaderLog
            // 
            this.loaderLog.WorkerReportsProgress = true;
            this.loaderLog.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LogLoader_DoWork);
            this.loaderLog.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.loaderLog.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LogLoader_RunWorkerCompleted);
            // 
            // loaderCard
            // 
            this.loaderCard.WorkerReportsProgress = true;
            this.loaderCard.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cardLoader_DoWork);
            this.loaderCard.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.loaderCard.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.cardLoader_RunWorkerCompleted);
            // 
            // loaderData
            // 
            this.loaderData.WorkerReportsProgress = true;
            this.loaderData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataLoader_DoWork);
            this.loaderData.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.loaderData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dataLoader_RunWorkerCompleted);
            // 
            // dataSaver
            // 
            this.dataSaver.WorkerReportsProgress = true;
            this.dataSaver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataSaver_DoWork);
            this.dataSaver.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.dataSaver.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dataSaver_RunWorkerCompleted);
            // 
            // loaderIndExtended
            // 
            this.loaderIndExtended.WorkerReportsProgress = true;
            this.loaderIndExtended.DoWork += new System.ComponentModel.DoWorkEventHandler(this.indExtender_DoWork);
            this.loaderIndExtended.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.loaderIndExtended.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.extender_RunWorkerCompleted);
            // 
            // loaderLogExtended
            // 
            this.loaderLogExtended.WorkerReportsProgress = true;
            this.loaderLogExtended.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LogExtender_DoWork);
            this.loaderLogExtended.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.loaderLogExtended.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.extender_RunWorkerCompleted);
            // 
            // taskDialogSave
            // 
            this.taskDialogSave.Buttons.Add(this.tdbSaveAll);
            this.taskDialogSave.Buttons.Add(this.tdbDiscard);
            this.taskDialogSave.Buttons.Add(this.tdbCancelClose);
            this.taskDialogSave.CenterParent = true;
            resources.ApplyResources(this.taskDialogSave, "taskDialogSave");
            // 
            // tdbSaveAll
            // 
            resources.ApplyResources(this.tdbSaveAll, "tdbSaveAll");
            // 
            // tdbDiscard
            // 
            resources.ApplyResources(this.tdbDiscard, "tdbDiscard");
            // 
            // tdbCancelClose
            // 
            this.tdbCancelClose.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // fbDialogBackup
            // 
            resources.ApplyResources(this.fbDialogBackup, "fbDialogBackup");
            this.fbDialogBackup.RootFolder = System.Environment.SpecialFolder.UserProfile;
            // 
            // tdLog
            // 
            this.tdLog.Buttons.Add(this.tdbLogRename);
            this.tdLog.Buttons.Add(this.tdbLogCancel);
            this.tdLog.CenterParent = true;
            resources.ApplyResources(this.tdLog, "tdLog");
            // 
            // tdbLogRename
            // 
            resources.ApplyResources(this.tdbLogRename, "tdbLogRename");
            // 
            // tdbLogCancel
            // 
            this.tdbLogCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // tdInd
            // 
            this.tdInd.Buttons.Add(this.tdbIndRename);
            this.tdInd.Buttons.Add(this.tdbIndCancel);
            this.tdInd.CenterParent = true;
            resources.ApplyResources(this.tdInd, "tdInd");
            // 
            // tdbIndRename
            // 
            resources.ApplyResources(this.tdbIndRename, "tdbIndRename");
            // 
            // tdbIndCancel
            // 
            this.tdbIndCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // mathLog
            // 
            this.mathLog.Sheet = this.spreadSheetLog;
            // 
            // mathCard
            // 
            this.mathCard.Sheet = this.spreadSheetCard;
            // 
            // mathInd
            // 
            this.mathInd.Sheet = this.spreadSheetInd;
            // 
            // comparerLog
            // 
            this.comparerLog.WorkerReportsProgress = true;
            this.comparerLog.DoWork += new System.ComponentModel.DoWorkEventHandler(this.comparerLog_DoWork);
            this.comparerLog.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.comparerLog.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.comparerLog_RunWorkerCompleted);
            // 
            // specTipper
            // 
            this.specTipper.WorkerReportsProgress = true;
            this.specTipper.DoWork += new System.ComponentModel.DoWorkEventHandler(this.specTipper_DoWork);
            this.specTipper.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.specTipper.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.specTipper_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtefacts)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageArtefacts.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageArtefactSpecies.ResumeLayout(false);
            this.tabPageArtefactSpecies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxArtefactSpecies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetArtefactSpecies)).EndInit();
            this.tabPageCard.ResumeLayout(false);
            this.tabPageCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCard)).EndInit();
            this.contextCard.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).EndInit();
            this.contextLog.ResumeLayout(false);
            this.tabPageInd.ResumeLayout(false);
            this.tabPageInd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetInd)).EndInit();
            this.contextInd.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWaters;
        private System.Windows.Forms.Label labelCards;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Label labelCollectors;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripProgressBar statusLoading;
        private System.Windows.Forms.ToolStripStatusLabel statusProcess;
        public System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusQuantity;
        private System.Windows.Forms.ToolStripStatusLabel statusMass;
        private System.Windows.Forms.TabPage tabPageInd;
        private Mayfly.Controls.SpreadSheet spreadSheetInd;
        private System.Windows.Forms.ListView listViewWaters;
        private System.Windows.Forms.ColumnHeader HeaderWater;
        private System.Windows.Forms.ImageList imageListWaters;
        private System.Windows.Forms.ContextMenuStrip contextLog;
        private System.Windows.Forms.ToolStripMenuItem contextLogOpen;
        private System.Windows.Forms.Label labelCardsNumber;
        private System.Windows.Forms.ListView listViewInvestigators;
        private System.Windows.Forms.ColumnHeader HeaderInvestigator;
        private System.Windows.Forms.Label labelDateEnd;
        private System.Windows.Forms.Label labelDateStart;
        private System.Windows.Forms.Label labelDates;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemSample;
        private System.Windows.Forms.ToolStripMenuItem menuItemLoadSpecies;
        private System.Windows.Forms.ToolStripMenuItem menuItemLoadIndividuals;
        private System.Windows.Forms.ToolStripMenuItem menuItemService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.Label labelIndCount;
        private System.Windows.Forms.TabPage tabPageCard;
        private System.Windows.Forms.ToolStripMenuItem menuItemLoadCards;
        private System.Windows.Forms.Label labelCardCount;
        private Mayfly.Controls.SpreadSheet spreadSheetCard;
        private System.Windows.Forms.ContextMenuStrip contextCard;
        private System.Windows.Forms.ToolStripMenuItem contextCardOpen;
        private System.Windows.Forms.ContextMenuStrip contextInd;
        private System.Windows.Forms.ToolStripMenuItem contextIndOpen;
        private TaskDialogs.TaskDialog taskDialogSave;
        private TaskDialogs.TaskDialogButton tdbSaveAll;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemLoadLog;
        private System.Windows.Forms.Button buttonSelectInd;
        private System.Windows.Forms.ToolStripMenuItem contextCardPrintFull;
        private System.Windows.Forms.ToolStripMenuItem contextCardPrintNote;
        private System.Windows.Forms.ToolStripMenuItem menuItemCards;
        private System.Windows.Forms.ToolStripMenuItem menuItemCardPrint;
        private System.Windows.Forms.ToolStripMenuItem menuItemCardPrintNotes;
        private System.Windows.Forms.ToolStripMenuItem contextCardPrint;
        private System.Windows.Forms.ToolStripMenuItem menuItemCardPrintFull;
        private Controls.ProcessDisplay processDisplay;
        private System.ComponentModel.BackgroundWorker loaderInd;
        private System.ComponentModel.BackgroundWorker loaderLog;
        private System.ComponentModel.BackgroundWorker loaderCard;
        private System.ComponentModel.BackgroundWorker loaderData;
        private System.ComponentModel.BackgroundWorker dataSaver;
        private System.ComponentModel.BackgroundWorker loaderIndExtended;
        private System.ComponentModel.BackgroundWorker loaderLogExtended;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddData;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemCardFindEmpty;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem additionalFactorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemAssignVariants;
        private System.Windows.Forms.ToolStripMenuItem menuItemLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemLoadSpc;
        private System.Windows.Forms.TabPage tabPageArtefacts;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageArtefactSpecies;
        private System.Windows.Forms.Label labelArtefactSpecies;
        private System.Windows.Forms.PictureBox pictureBoxArtefactSpecies;
        private System.Windows.Forms.Label labelArtefactSpeciesCount;
        private Controls.SpreadSheet spreadSheetArtefactSpecies;
        private System.Windows.Forms.Label labelArtefacts;
        private System.Windows.Forms.PictureBox pictureBoxArtefacts;
        private System.Windows.Forms.ToolStripMenuItem menuItemArtefactsSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnArtefactSpecies;
        private Controls.SpreadSheetIconTextBoxColumn columnArtefactValidName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnArtefactN;
        private System.Windows.Forms.ToolStripMenuItem contextIndLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemIndividuals;
        private System.Windows.Forms.ToolStripMenuItem menuItemIndPrint;
        private System.Windows.Forms.ToolStripMenuItem menuItemBackupCards;
        private System.Windows.Forms.FolderBrowserDialog fbDialogBackup;
        private System.Windows.Forms.ToolStripMenuItem contextIndRemove;
        private TaskDialogs.TaskDialog tdLog;
        private TaskDialogs.TaskDialogButton tdbLogRename;
        private TaskDialogs.TaskDialogButton tdbLogCancel;
        private TaskDialogs.TaskDialog tdInd;
        private TaskDialogs.TaskDialogButton tdbIndRename;
        private TaskDialogs.TaskDialogButton tdbIndCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem contextCardLog;
        private System.Windows.Forms.ToolStripMenuItem contextLogCard;
        private System.Windows.Forms.ToolStripMenuItem contextLogInds;
        private System.Windows.Forms.ToolStripMenuItem contextIndCard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem contextLogRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem contextCardRemove;
        private Mathematics.MathAdapter mathLog;
        private Mathematics.MathAdapter mathCard;
        private Mathematics.MathAdapter mathInd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem menuItemComCom;
        private System.ComponentModel.BackgroundWorker comparerLog;
        private System.ComponentModel.BackgroundWorker specTipper;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.Label labelLogCount;
        private System.Windows.Forms.Button buttonSelectLog;
        private Controls.SpreadSheet spreadSheetLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIndDiameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIndComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardInvestigator;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardWater;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardWhen;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardDepth;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardBiomass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCardComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLogSpc;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLogAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnLogBiomass;
        private System.ComponentModel.BackgroundWorker specUpdater;
    }
}

