namespace Mayfly.Plankton
{
    partial class Card
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Card));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            Mayfly.Wild.AquaState aquaState2 = new Mayfly.Wild.AquaState();
            Mayfly.Wild.WeatherState weather2 = new Mayfly.Wild.Weather();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.blankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCardBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSampleLogBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemIndividualsLogBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemData = new System.Windows.Forms.ToolStripMenuItem();
            this.addEnvironmentalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFactorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemService = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWatersRef = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSpeciesRef = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonBlankLog = new System.Windows.Forms.Button();
            this.spreadSheetLog = new Mayfly.Controls.SpreadSheet();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSubsample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemIndividuals = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSpeciesKey = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSubsample = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSubSecond = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSubThird = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSubTenth = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageCollect = new System.Windows.Forms.TabPage();
            this.labelPosition = new System.Windows.Forms.Label();
            this.waypointControl1 = new Mayfly.Geographics.WaypointControl();
            this.waterSelector = new Mayfly.Waters.Controls.WaterSelector();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.comboBoxCrossSection = new System.Windows.Forms.ComboBox();
            this.labelCrossSection = new System.Windows.Forms.Label();
            this.labelWater = new System.Windows.Forms.Label();
            this.labelBank = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.labelTag = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSampler = new System.Windows.Forms.TabPage();
            this.comboBoxSampler = new GroupedComboBox();
            this.textBoxDepth = new System.Windows.Forms.TextBox();
            this.labelDepth = new System.Windows.Forms.Label();
            this.labelMethod = new System.Windows.Forms.Label();
            this.labelSampler = new System.Windows.Forms.Label();
            this.labelMesh = new System.Windows.Forms.Label();
            this.labelPortion = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.textBoxMesh = new System.Windows.Forms.TextBox();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.numericUpDownPortion = new System.Windows.Forms.NumericUpDown();
            this.tabPageEnvironment = new System.Windows.Forms.TabPage();
            this.aquaControl1 = new Mayfly.Wild.Controls.AquaControl();
            this.weatherControl1 = new Mayfly.Wild.Controls.WeatherControl();
            this.labelActWeather = new System.Windows.Forms.Label();
            this.labelWaterConds = new System.Windows.Forms.Label();
            this.tabPageFactors = new System.Windows.Forms.TabPage();
            this.spreadSheetAddt = new Mayfly.Controls.SpreadSheet();
            this.ColumnAddtFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddtValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMass = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipAttention = new System.Windows.Forms.ToolTip(this.components);
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.statusCard = new Mayfly.Controls.Status();
            this.speciesLogger = new Mayfly.Species.SpeciesSelector(this.components);
            this.MenuStrip.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).BeginInit();
            this.contextMenuStripLog.SuspendLayout();
            this.tabPageCollect.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortion)).BeginInit();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageFactors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAddt)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxLabel
            // 
            resources.ApplyResources(this.textBoxLabel, "textBoxLabel");
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelLabel
            // 
            resources.ApplyResources(this.labelLabel, "labelLabel");
            this.labelLabel.Name = "labelLabel";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile,
            this.ToolStripMenuItemData,
            this.ToolStripMenuItemService});
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.Name = "MenuStrip";
            // 
            // ToolStripMenuItemFile
            // 
            this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNew,
            this.ToolStripMenuItemOpen,
            this.ToolStripMenuItemSave,
            this.ToolStripMenuItemSaveAs,
            this.toolStripSeparator3,
            this.ToolStripMenuItemPrintPreview,
            this.ToolStripMenuItemPrint,
            this.blankToolStripMenuItem,
            this.toolStripSeparator6,
            this.ToolStripMenuItemClose});
            this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            resources.ApplyResources(this.ToolStripMenuItemFile, "ToolStripMenuItemFile");
            // 
            // ToolStripMenuItemNew
            // 
            this.ToolStripMenuItemNew.Name = "ToolStripMenuItemNew";
            resources.ApplyResources(this.ToolStripMenuItemNew, "ToolStripMenuItemNew");
            this.ToolStripMenuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // ToolStripMenuItemOpen
            // 
            this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            resources.ApplyResources(this.ToolStripMenuItemOpen, "ToolStripMenuItemOpen");
            this.ToolStripMenuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // ToolStripMenuItemSave
            // 
            this.ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
            resources.ApplyResources(this.ToolStripMenuItemSave, "ToolStripMenuItemSave");
            this.ToolStripMenuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // ToolStripMenuItemSaveAs
            // 
            this.ToolStripMenuItemSaveAs.Name = "ToolStripMenuItemSaveAs";
            resources.ApplyResources(this.ToolStripMenuItemSaveAs, "ToolStripMenuItemSaveAs");
            this.ToolStripMenuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // ToolStripMenuItemPrintPreview
            // 
            this.ToolStripMenuItemPrintPreview.Name = "ToolStripMenuItemPrintPreview";
            resources.ApplyResources(this.ToolStripMenuItemPrintPreview, "ToolStripMenuItemPrintPreview");
            this.ToolStripMenuItemPrintPreview.Click += new System.EventHandler(this.menuItemPrintPreview_Click);
            // 
            // ToolStripMenuItemPrint
            // 
            this.ToolStripMenuItemPrint.Name = "ToolStripMenuItemPrint";
            resources.ApplyResources(this.ToolStripMenuItemPrint, "ToolStripMenuItemPrint");
            this.ToolStripMenuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // blankToolStripMenuItem
            // 
            this.blankToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCardBlank,
            this.ToolStripMenuItemSampleLogBlank,
            this.ToolStripMenuItemIndividualsLogBlank});
            this.blankToolStripMenuItem.Name = "blankToolStripMenuItem";
            resources.ApplyResources(this.blankToolStripMenuItem, "blankToolStripMenuItem");
            // 
            // ToolStripMenuItemCardBlank
            // 
            this.ToolStripMenuItemCardBlank.Name = "ToolStripMenuItemCardBlank";
            resources.ApplyResources(this.ToolStripMenuItemCardBlank, "ToolStripMenuItemCardBlank");
            this.ToolStripMenuItemCardBlank.Click += new System.EventHandler(this.menuItemCardBlank_Click);
            // 
            // ToolStripMenuItemSampleLogBlank
            // 
            this.ToolStripMenuItemSampleLogBlank.Name = "ToolStripMenuItemSampleLogBlank";
            resources.ApplyResources(this.ToolStripMenuItemSampleLogBlank, "ToolStripMenuItemSampleLogBlank");
            this.ToolStripMenuItemSampleLogBlank.Click += new System.EventHandler(this.menuItemLogBlank_Click);
            // 
            // ToolStripMenuItemIndividualsLogBlank
            // 
            this.ToolStripMenuItemIndividualsLogBlank.Name = "ToolStripMenuItemIndividualsLogBlank";
            resources.ApplyResources(this.ToolStripMenuItemIndividualsLogBlank, "ToolStripMenuItemIndividualsLogBlank");
            this.ToolStripMenuItemIndividualsLogBlank.Click += new System.EventHandler(this.menuItemIndividualsLogBlank_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // ToolStripMenuItemClose
            // 
            this.ToolStripMenuItemClose.Name = "ToolStripMenuItemClose";
            resources.ApplyResources(this.ToolStripMenuItemClose, "ToolStripMenuItemClose");
            this.ToolStripMenuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // ToolStripMenuItemData
            // 
            this.ToolStripMenuItemData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEnvironmentalDataToolStripMenuItem,
            this.addFactorsToolStripMenuItem,
            this.toolStripSeparator7,
            this.menuItemLocation});
            this.ToolStripMenuItemData.Name = "ToolStripMenuItemData";
            resources.ApplyResources(this.ToolStripMenuItemData, "ToolStripMenuItemData");
            // 
            // addEnvironmentalDataToolStripMenuItem
            // 
            this.addEnvironmentalDataToolStripMenuItem.Name = "addEnvironmentalDataToolStripMenuItem";
            resources.ApplyResources(this.addEnvironmentalDataToolStripMenuItem, "addEnvironmentalDataToolStripMenuItem");
            this.addEnvironmentalDataToolStripMenuItem.Click += new System.EventHandler(this.addEnvironmentalDataToolStripMenuItem_Click);
            // 
            // addFactorsToolStripMenuItem
            // 
            this.addFactorsToolStripMenuItem.Name = "addFactorsToolStripMenuItem";
            resources.ApplyResources(this.addFactorsToolStripMenuItem, "addFactorsToolStripMenuItem");
            this.addFactorsToolStripMenuItem.Click += new System.EventHandler(this.addFactorsToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // menuItemLocation
            // 
            this.menuItemLocation.Name = "menuItemLocation";
            resources.ApplyResources(this.menuItemLocation, "menuItemLocation");
            this.menuItemLocation.Click += new System.EventHandler(this.menuItemLocation_Click);
            // 
            // ToolStripMenuItemService
            // 
            this.ToolStripMenuItemService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemWatersRef,
            this.ToolStripMenuItemSpeciesRef,
            this.toolStripSeparator2,
            this.ToolStripMenuItemSettings,
            this.ToolStripMenuItemAbout});
            this.ToolStripMenuItemService.Name = "ToolStripMenuItemService";
            resources.ApplyResources(this.ToolStripMenuItemService, "ToolStripMenuItemService");
            // 
            // ToolStripMenuItemWatersRef
            // 
            this.ToolStripMenuItemWatersRef.Name = "ToolStripMenuItemWatersRef";
            resources.ApplyResources(this.ToolStripMenuItemWatersRef, "ToolStripMenuItemWatersRef");
            this.ToolStripMenuItemWatersRef.Click += new System.EventHandler(this.ToolStripMenuItemWatersRef_Click);
            // 
            // ToolStripMenuItemSpeciesRef
            // 
            this.ToolStripMenuItemSpeciesRef.Name = "ToolStripMenuItemSpeciesRef";
            resources.ApplyResources(this.ToolStripMenuItemSpeciesRef, "ToolStripMenuItemSpeciesRef");
            this.ToolStripMenuItemSpeciesRef.Click += new System.EventHandler(this.ToolStripMenuItemSpeciesRef_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // ToolStripMenuItemSettings
            // 
            resources.ApplyResources(this.ToolStripMenuItemSettings, "ToolStripMenuItemSettings");
            this.ToolStripMenuItemSettings.Name = "ToolStripMenuItemSettings";
            this.ToolStripMenuItemSettings.Click += new System.EventHandler(this.ToolStripMenuItemSettings_Click);
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            resources.ApplyResources(this.ToolStripMenuItemAbout, "ToolStripMenuItemAbout");
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // tabPageLog
            // 
            this.tabPageLog.AllowDrop = true;
            this.tabPageLog.Controls.Add(this.buttonAdd);
            this.tabPageLog.Controls.Add(this.buttonBlankLog);
            this.tabPageLog.Controls.Add(this.spreadSheetLog);
            resources.ApplyResources(this.tabPageLog, "tabPageLog");
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // buttonBlankLog
            // 
            resources.ApplyResources(this.buttonBlankLog, "buttonBlankLog");
            this.buttonBlankLog.FlatAppearance.BorderSize = 0;
            this.buttonBlankLog.Name = "buttonBlankLog";
            this.buttonBlankLog.UseVisualStyleBackColor = true;
            this.buttonBlankLog.Click += new System.EventHandler(this.menuItemLogBlank_Click);
            // 
            // spreadSheetLog
            // 
            this.spreadSheetLog.AllowUserToAddRows = true;
            this.spreadSheetLog.AllowUserToDeleteRows = true;
            resources.ApplyResources(this.spreadSheetLog, "spreadSheetLog");
            this.spreadSheetLog.AutoClearEmptyRows = true;
            this.spreadSheetLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.spreadSheetLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnSpecies,
            this.ColumnSubsample,
            this.ColumnQuantity,
            this.ColumnMass});
            this.spreadSheetLog.DefaultDecimalPlaces = 0;
            this.spreadSheetLog.Name = "spreadSheetLog";
            this.spreadSheetLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.spreadSheetLog.RowMenu = this.contextMenuStripLog;
            this.spreadSheetLog.RowMenuLaunchableItemIndex = 0;
            this.spreadSheetLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetLog.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetLog_CellEndEdit);
            this.spreadSheetLog.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetLog_CellValueChanged);
            this.spreadSheetLog.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.spreadSheetLog_RowsAdded);
            this.spreadSheetLog.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.spreadSheetLog_RowsRemoved);
            this.spreadSheetLog.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.spreadSheetLog_UserDeletedRow);
            // 
            // ColumnID
            // 
            resources.ApplyResources(this.ColumnID, "ColumnID");
            this.ColumnID.Name = "ColumnID";
            // 
            // ColumnSpecies
            // 
            this.ColumnSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSpecies.DefaultCellStyle = dataGridViewCellStyle7;
            this.ColumnSpecies.FillWeight = 200F;
            resources.ApplyResources(this.ColumnSpecies, "ColumnSpecies");
            this.ColumnSpecies.Name = "ColumnSpecies";
            // 
            // ColumnSubsample
            // 
            this.ColumnSubsample.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Format = "P1";
            dataGridViewCellStyle8.NullValue = "Неразб.";
            this.ColumnSubsample.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnSubsample.FillWeight = 30F;
            resources.ApplyResources(this.ColumnSubsample, "ColumnSubsample");
            this.ColumnSubsample.Name = "ColumnSubsample";
            // 
            // ColumnQuantity
            // 
            this.ColumnQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnQuantity.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColumnQuantity.FillWeight = 30F;
            resources.ApplyResources(this.ColumnQuantity, "ColumnQuantity");
            this.ColumnQuantity.Name = "ColumnQuantity";
            // 
            // ColumnMass
            // 
            this.ColumnMass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnMass.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnMass.FillWeight = 30F;
            resources.ApplyResources(this.ColumnMass, "ColumnMass");
            this.ColumnMass.Name = "ColumnMass";
            // 
            // contextMenuStripLog
            // 
            this.contextMenuStripLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemIndividuals,
            this.ToolStripMenuItemSpeciesKey,
            this.contextSubsample,
            this.toolStripSeparator1,
            this.ToolStripMenuItemCut,
            this.ToolStripMenuItemCopy,
            this.ToolStripMenuItemPaste,
            this.ToolStripMenuItemDelete});
            this.contextMenuStripLog.Name = "contextMenuStripLog";
            resources.ApplyResources(this.contextMenuStripLog, "contextMenuStripLog");
            this.contextMenuStripLog.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripLog_Opening);
            // 
            // ToolStripMenuItemIndividuals
            // 
            resources.ApplyResources(this.ToolStripMenuItemIndividuals, "ToolStripMenuItemIndividuals");
            this.ToolStripMenuItemIndividuals.Name = "ToolStripMenuItemIndividuals";
            this.ToolStripMenuItemIndividuals.Click += new System.EventHandler(this.ToolStripMenuItemIndividuals_Click);
            // 
            // ToolStripMenuItemSpeciesKey
            // 
            this.ToolStripMenuItemSpeciesKey.Name = "ToolStripMenuItemSpeciesKey";
            resources.ApplyResources(this.ToolStripMenuItemSpeciesKey, "ToolStripMenuItemSpeciesKey");
            this.ToolStripMenuItemSpeciesKey.Click += new System.EventHandler(this.ToolStripMenuItemSpeciesKey_Click);
            // 
            // contextSubsample
            // 
            this.contextSubsample.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextSubSecond,
            this.contextSubThird,
            this.contextSubTenth});
            this.contextSubsample.Name = "contextSubsample";
            resources.ApplyResources(this.contextSubsample, "contextSubsample");
            // 
            // contextSubSecond
            // 
            this.contextSubSecond.Name = "contextSubSecond";
            resources.ApplyResources(this.contextSubSecond, "contextSubSecond");
            this.contextSubSecond.Click += new System.EventHandler(this.contextSubSecond_Click);
            // 
            // contextSubThird
            // 
            this.contextSubThird.Name = "contextSubThird";
            resources.ApplyResources(this.contextSubThird, "contextSubThird");
            this.contextSubThird.Click += new System.EventHandler(this.contextSubThird_Click);
            // 
            // contextSubTenth
            // 
            this.contextSubTenth.Name = "contextSubTenth";
            resources.ApplyResources(this.contextSubTenth, "contextSubTenth");
            this.contextSubTenth.Click += new System.EventHandler(this.contextSubTenth_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // ToolStripMenuItemCut
            // 
            this.ToolStripMenuItemCut.Name = "ToolStripMenuItemCut";
            resources.ApplyResources(this.ToolStripMenuItemCut, "ToolStripMenuItemCut");
            this.ToolStripMenuItemCut.Click += new System.EventHandler(this.ToolStripMenuItemCut_Click);
            // 
            // ToolStripMenuItemCopy
            // 
            this.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            resources.ApplyResources(this.ToolStripMenuItemCopy, "ToolStripMenuItemCopy");
            this.ToolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
            // 
            // ToolStripMenuItemPaste
            // 
            this.ToolStripMenuItemPaste.Name = "ToolStripMenuItemPaste";
            resources.ApplyResources(this.ToolStripMenuItemPaste, "ToolStripMenuItemPaste");
            this.ToolStripMenuItemPaste.Click += new System.EventHandler(this.ToolStripMenuItemPaste_Click);
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            resources.ApplyResources(this.ToolStripMenuItemDelete, "ToolStripMenuItemDelete");
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.labelPosition);
            this.tabPageCollect.Controls.Add(this.waypointControl1);
            this.tabPageCollect.Controls.Add(this.waterSelector);
            this.tabPageCollect.Controls.Add(this.comboBoxBank);
            this.tabPageCollect.Controls.Add(this.comboBoxCrossSection);
            this.tabPageCollect.Controls.Add(this.labelLabel);
            this.tabPageCollect.Controls.Add(this.labelCrossSection);
            this.tabPageCollect.Controls.Add(this.textBoxLabel);
            this.tabPageCollect.Controls.Add(this.labelWater);
            this.tabPageCollect.Controls.Add(this.labelBank);
            this.tabPageCollect.Controls.Add(this.textBoxComments);
            this.tabPageCollect.Controls.Add(this.labelComments);
            this.tabPageCollect.Controls.Add(this.labelTag);
            resources.ApplyResources(this.tabPageCollect, "tabPageCollect");
            this.tabPageCollect.Name = "tabPageCollect";
            this.tabPageCollect.UseVisualStyleBackColor = true;
            // 
            // labelPosition
            // 
            resources.ApplyResources(this.labelPosition, "labelPosition");
            this.labelPosition.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPosition.Name = "labelPosition";
            // 
            // waypointControl1
            // 
            this.waypointControl1.AllowDrop = true;
            resources.ApplyResources(this.waypointControl1, "waypointControl1");
            this.waypointControl1.BackColor = System.Drawing.SystemColors.Window;
            this.waypointControl1.Name = "waypointControl1";
            this.waypointControl1.ReadOnly = false;
            this.waypointControl1.Changed += new System.EventHandler(this.value_Changed);
            // 
            // waterSelector
            // 
            resources.ApplyResources(this.waterSelector, "waterSelector");
            this.waterSelector.Name = "waterSelector";
            this.waterSelector.WaterObject = null;
            this.waterSelector.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.waterSelector_WaterSelected);
            // 
            // comboBoxBank
            // 
            resources.ApplyResources(this.comboBoxBank, "comboBoxBank");
            this.comboBoxBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBank.FormattingEnabled = true;
            this.comboBoxBank.Items.AddRange(new object[] {
            resources.GetString("comboBoxBank.Items"),
            resources.GetString("comboBoxBank.Items1")});
            this.comboBoxBank.Name = "comboBoxBank";
            this.comboBoxBank.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.comboBoxBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBoxCrossSection
            // 
            resources.ApplyResources(this.comboBoxCrossSection, "comboBoxCrossSection");
            this.comboBoxCrossSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrossSection.FormattingEnabled = true;
            this.comboBoxCrossSection.Name = "comboBoxCrossSection";
            this.comboBoxCrossSection.SelectedIndexChanged += new System.EventHandler(this.comboBoxCrossSection_SelectedIndexChanged);
            this.comboBoxCrossSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // labelCrossSection
            // 
            resources.ApplyResources(this.labelCrossSection, "labelCrossSection");
            this.labelCrossSection.Name = "labelCrossSection";
            // 
            // labelWater
            // 
            resources.ApplyResources(this.labelWater, "labelWater");
            this.labelWater.Name = "labelWater";
            // 
            // labelBank
            // 
            resources.ApplyResources(this.labelBank, "labelBank");
            this.labelBank.Name = "labelBank";
            // 
            // textBoxComments
            // 
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelComments
            // 
            resources.ApplyResources(this.labelComments, "labelComments");
            this.labelComments.Name = "labelComments";
            // 
            // labelTag
            // 
            resources.ApplyResources(this.labelTag, "labelTag");
            this.labelTag.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTag.Name = "labelTag";
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageCollect);
            this.tabControl.Controls.Add(this.tabPageSampler);
            this.tabControl.Controls.Add(this.tabPageEnvironment);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Controls.Add(this.tabPageFactors);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.comboBoxSampler);
            this.tabPageSampler.Controls.Add(this.textBoxDepth);
            this.tabPageSampler.Controls.Add(this.labelDepth);
            this.tabPageSampler.Controls.Add(this.labelMethod);
            this.tabPageSampler.Controls.Add(this.labelSampler);
            this.tabPageSampler.Controls.Add(this.labelMesh);
            this.tabPageSampler.Controls.Add(this.labelPortion);
            this.tabPageSampler.Controls.Add(this.labelVolume);
            this.tabPageSampler.Controls.Add(this.textBoxMesh);
            this.tabPageSampler.Controls.Add(this.textBoxVolume);
            this.tabPageSampler.Controls.Add(this.numericUpDownPortion);
            resources.ApplyResources(this.tabPageSampler, "tabPageSampler");
            this.tabPageSampler.Name = "tabPageSampler";
            this.tabPageSampler.UseVisualStyleBackColor = true;
            // 
            // comboBoxSampler
            // 
            resources.ApplyResources(this.comboBoxSampler, "comboBoxSampler");
            this.comboBoxSampler.DataSource = null;
            this.comboBoxSampler.DisplayMember = "Sampler";
            this.comboBoxSampler.DropDownHeight = 350;
            this.comboBoxSampler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampler.FormattingEnabled = true;
            this.comboBoxSampler.GroupMember = "OperationDisplay";
            this.comboBoxSampler.Name = "comboBoxSampler";
            this.comboBoxSampler.SelectedIndexChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // textBoxDepth
            // 
            resources.ApplyResources(this.textBoxDepth, "textBoxDepth");
            this.textBoxDepth.Name = "textBoxDepth";
            this.textBoxDepth.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelDepth
            // 
            resources.ApplyResources(this.labelDepth, "labelDepth");
            this.labelDepth.Name = "labelDepth";
            // 
            // labelMethod
            // 
            resources.ApplyResources(this.labelMethod, "labelMethod");
            this.labelMethod.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelMethod.Name = "labelMethod";
            // 
            // labelSampler
            // 
            resources.ApplyResources(this.labelSampler, "labelSampler");
            this.labelSampler.Name = "labelSampler";
            // 
            // labelMesh
            // 
            resources.ApplyResources(this.labelMesh, "labelMesh");
            this.labelMesh.Name = "labelMesh";
            // 
            // labelPortion
            // 
            resources.ApplyResources(this.labelPortion, "labelPortion");
            this.labelPortion.Name = "labelPortion";
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // textBoxMesh
            // 
            resources.ApplyResources(this.textBoxMesh, "textBoxMesh");
            this.textBoxMesh.Name = "textBoxMesh";
            this.textBoxMesh.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // textBoxVolume
            // 
            resources.ApplyResources(this.textBoxVolume, "textBoxVolume");
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.ReadOnly = true;
            this.textBoxVolume.TextChanged += new System.EventHandler(this.textBoxVolume_TextChanged);
            // 
            // numericUpDownPortion
            // 
            resources.ApplyResources(this.numericUpDownPortion, "numericUpDownPortion");
            this.numericUpDownPortion.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPortion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortion.Name = "numericUpDownPortion";
            this.numericUpDownPortion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortion.ValueChanged += new System.EventHandler(this.numericUpDownReps_ValueChanged);
            this.numericUpDownPortion.VisibleChanged += new System.EventHandler(this.numericUpDownReps_VisibleChanged);
            // 
            // tabPageEnvironment
            // 
            this.tabPageEnvironment.Controls.Add(this.aquaControl1);
            this.tabPageEnvironment.Controls.Add(this.weatherControl1);
            this.tabPageEnvironment.Controls.Add(this.labelActWeather);
            this.tabPageEnvironment.Controls.Add(this.labelWaterConds);
            resources.ApplyResources(this.tabPageEnvironment, "tabPageEnvironment");
            this.tabPageEnvironment.Name = "tabPageEnvironment";
            this.tabPageEnvironment.UseVisualStyleBackColor = true;
            // 
            // aquaControl1
            // 
            resources.ApplyResources(this.aquaControl1, "aquaControl1");
            aquaState2.Colour = -1;
            aquaState2.Conductivity = double.NaN;
            aquaState2.DissolvedOxygen = double.NaN;
            aquaState2.FlowRate = double.NaN;
            aquaState2.Foam = Mayfly.Wild.OrganolepticState.NotInvestigated;
            aquaState2.Limpidity = double.NaN;
            aquaState2.Odor = Mayfly.Wild.OrganolepticState.NotInvestigated;
            aquaState2.OxygenSaturation = double.NaN;
            aquaState2.pH = double.NaN;
            aquaState2.Sewage = Mayfly.Wild.OrganolepticState.NotInvestigated;
            aquaState2.TemperatureBottom = double.NaN;
            aquaState2.TemperatureSurface = double.NaN;
            aquaState2.Turbidity = Mayfly.Wild.OrganolepticState.NotInvestigated;
            this.aquaControl1.AquaState = aquaState2;
            this.aquaControl1.BackColor = System.Drawing.SystemColors.Window;
            this.aquaControl1.Name = "aquaControl1";
            // 
            // weatherControl1
            // 
            resources.ApplyResources(this.weatherControl1, "weatherControl1");
            this.weatherControl1.BackColor = System.Drawing.SystemColors.Window;
            this.weatherControl1.Name = "weatherControl1";
            weather2.AdditionalEvent = 0;
            weather2.Cloudage = double.NaN;
            weather2.Degree = 0;
            weather2.Discretion = 0;
            weather2.Event = 0;
            weather2.Humidity = double.NaN;
            weather2.Pressure = double.NaN;
            weather2.Temperature = double.NaN;
            weather2.WindDirection = double.NaN;
            weather2.WindRate = double.NaN;
            this.weatherControl1.Weather = weather2;
            // 
            // labelActWeather
            // 
            resources.ApplyResources(this.labelActWeather, "labelActWeather");
            this.labelActWeather.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelActWeather.Name = "labelActWeather";
            // 
            // labelWaterConds
            // 
            resources.ApplyResources(this.labelWaterConds, "labelWaterConds");
            this.labelWaterConds.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelWaterConds.Name = "labelWaterConds";
            // 
            // tabPageFactors
            // 
            this.tabPageFactors.Controls.Add(this.spreadSheetAddt);
            resources.ApplyResources(this.tabPageFactors, "tabPageFactors");
            this.tabPageFactors.Name = "tabPageFactors";
            this.tabPageFactors.UseVisualStyleBackColor = true;
            // 
            // spreadSheetAddt
            // 
            this.spreadSheetAddt.AllowStringSuggection = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.spreadSheetAddt.AllowUserToAddRows = true;
            this.spreadSheetAddt.AllowUserToDeleteRows = true;
            this.spreadSheetAddt.AllowUserToResizeColumns = false;
            this.spreadSheetAddt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAddtFactor,
            this.ColumnAddtValue});
            this.spreadSheetAddt.DefaultDecimalPlaces = 0;
            resources.ApplyResources(this.spreadSheetAddt, "spreadSheetAddt");
            this.spreadSheetAddt.Name = "spreadSheetAddt";
            this.spreadSheetAddt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetAddt_CellEndEdit);
            // 
            // ColumnAddtFactor
            // 
            this.ColumnAddtFactor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.NullValue = null;
            this.ColumnAddtFactor.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnAddtFactor, "ColumnAddtFactor");
            this.ColumnAddtFactor.Name = "ColumnAddtFactor";
            // 
            // ColumnAddtValue
            // 
            this.ColumnAddtValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N1";
            this.ColumnAddtValue.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.ColumnAddtValue, "ColumnAddtValue");
            this.ColumnAddtValue.Name = "ColumnAddtValue";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLog,
            this.StatusCount,
            this.StatusMass});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // StatusLog
            // 
            this.StatusLog.Name = "StatusLog";
            resources.ApplyResources(this.StatusLog, "StatusLog");
            this.StatusLog.Spring = true;
            // 
            // StatusCount
            // 
            resources.ApplyResources(this.StatusCount, "StatusCount");
            this.StatusCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.StatusCount.Name = "StatusCount";
            // 
            // StatusMass
            // 
            resources.ApplyResources(this.StatusMass, "StatusMass");
            this.StatusMass.Name = "StatusMass";
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
            // statusCard
            // 
            this.statusCard.Default = "Species count:";
            this.statusCard.MaximalInterval = 2000;
            this.statusCard.StatusLog = this.StatusLog;
            // 
            // speciesLogger
            // 
            this.speciesLogger.Button = this.buttonAdd;
            this.speciesLogger.CheckDuplicates = false;
            this.speciesLogger.ColumnName = "ColumnSpecies";
            this.speciesLogger.Grid = this.spreadSheetLog;
            this.speciesLogger.RecentListCount = 0;
            this.speciesLogger.SpeciesSelected += new Mayfly.Species.SpeciesSelectEventHandler(this.speciesLogger_SpeciesSelected);
            this.speciesLogger.DuplicateFound += new Mayfly.Species.DuplicateFoundEventHandler(this.speciesLogger_DuplicateDetected);
            // 
            // Card
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.tabControl);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "Card";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Card_FormClosing);
            this.Load += new System.EventHandler(this.Card_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).EndInit();
            this.contextMenuStripLog.ResumeLayout(false);
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortion)).EndInit();
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageFactors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAddt)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemService;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWatersRef;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSpeciesRef;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TabPage tabPageCollect;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLog;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripStatusLabel StatusMass;
        private System.Windows.Forms.ToolStripStatusLabel StatusCount;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndividuals;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSpeciesKey;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.Label labelWater;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.Label labelCrossSection;
        private System.Windows.Forms.Label labelBank;
        private System.Windows.Forms.TabPage tabPageEnvironment;
        private System.Windows.Forms.Label labelActWeather;
        private System.Windows.Forms.Label labelWaterConds;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPrint;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPrintPreview;
        private System.Windows.Forms.ToolStripStatusLabel StatusLog;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage tabPageFactors;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemData;
        private System.Windows.Forms.ToolStripMenuItem menuItemLocation;
        private Mayfly.Controls.Status statusCard;
        private Mayfly.TaskDialogs.TaskDialog taskDialogSaveChanges;
        private Mayfly.TaskDialogs.TaskDialogButton tdbSave;
        private Mayfly.TaskDialogs.TaskDialogButton tdbDiscard;
        private Mayfly.TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem addFactorsToolStripMenuItem;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ToolStripMenuItem addEnvironmentalDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCut;
        private System.Windows.Forms.Button buttonBlankLog;
        private System.Windows.Forms.ToolStripMenuItem blankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCardBlank;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSampleLogBlank;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndividualsLogBlank;
        private System.Windows.Forms.ToolTip toolTipAttention;
        private Mayfly.Controls.SpreadSheet spreadSheetAddt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddtFactor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddtValue;
        private Species.SpeciesSelector speciesLogger;
        private Wild.Controls.WeatherControl weatherControl1;
        private Wild.Controls.AquaControl aquaControl1;
        private Geographics.WaypointControl waypointControl1;
        private System.Windows.Forms.ToolStripMenuItem contextSubsample;
        private System.Windows.Forms.ToolStripMenuItem contextSubSecond;
        private System.Windows.Forms.ToolStripMenuItem contextSubThird;
        private System.Windows.Forms.ToolStripMenuItem contextSubTenth;
        private Waters.Controls.WaterSelector waterSelector;
        private System.Windows.Forms.TabPage tabPageSampler;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.Label labelMethod;
        private System.Windows.Forms.Label labelSampler;
        private System.Windows.Forms.Label labelMesh;
        private System.Windows.Forms.Label labelPortion;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.NumericUpDown numericUpDownPortion;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.ComboBox comboBoxCrossSection;
        private System.Windows.Forms.TextBox textBoxMesh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSubsample;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMass;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNew;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpen;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.ComboBox comboBoxBank;
        private Controls.SpreadSheet spreadSheetLog;
        private System.Windows.Forms.TextBox textBoxDepth;
        private GroupedComboBox comboBoxSampler;
    }
}