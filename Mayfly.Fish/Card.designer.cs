namespace Mayfly.Fish
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
            Mayfly.Wild.AquaState aquaState3 = new Mayfly.Wild.AquaState();
            Mayfly.Wild.WeatherState weatherState3 = new Mayfly.Wild.WeatherState();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelWater = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSpot = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCardBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemIndividualsLogBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemIndividualProfileBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemData = new System.Windows.Forms.ToolStripMenuItem();
            this.meniItemEnvironment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFactors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWatersRef = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSpeciesRef = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAboutCard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAddInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLog = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMass = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCollect = new System.Windows.Forms.TabPage();
            this.labelTag = new System.Windows.Forms.Label();
            this.textBoxDepth = new System.Windows.Forms.TextBox();
            this.waterSelector = new Mayfly.Waters.Controls.WaterSelector();
            this.labelDepth = new System.Windows.Forms.Label();
            this.labelLabel = new System.Windows.Forms.Label();
            this.labelComments = new System.Windows.Forms.Label();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelPosition = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.waypointControl1 = new Mayfly.Geographics.WaypointControl();
            this.tabPageSampler = new System.Windows.Forms.TabPage();
            this.buttonGear = new System.Windows.Forms.Button();
            this.labelMethod = new System.Windows.Forms.Label();
            this.comboBoxSampler = new GroupedComboBox();
            this.tabPageEnvironment = new System.Windows.Forms.TabPage();
            this.aquaControl1 = new Mayfly.Wild.Controls.AquaControl();
            this.weatherControl1 = new Mayfly.Wild.Controls.WeatherControl();
            this.labelActWeather = new System.Windows.Forms.Label();
            this.labelWaterConds = new System.Windows.Forms.Label();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.spreadSheetLog = new Mayfly.Controls.SpreadSheet();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.tabPageFactors = new System.Windows.Forms.TabPage();
            this.spreadSheetAddt = new Mayfly.Controls.SpreadSheet();
            this.ColumnAddtFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddtValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextGear = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelTrack = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbSinglepoint = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbAsPoly = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbExposure = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.taskDialogTrackHandle = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.taskDialogLocationHandle = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSinking = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbRemoval = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.statusCard = new Mayfly.Controls.Status();
            this.Logger = new Mayfly.Wild.Controls.LogProcessor(this.components);
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.labelEffort = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.textBoxEfforts = new System.Windows.Forms.TextBox();
            this.labelEfforts = new System.Windows.Forms.Label();
            this.textBoxArea = new System.Windows.Forms.TextBox();
            this.labelArea = new System.Windows.Forms.Label();
            this.labelOpening = new System.Windows.Forms.Label();
            this.labelSampler = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelMesh = new System.Windows.Forms.Label();
            this.textBoxSquare = new System.Windows.Forms.TextBox();
            this.textBoxMesh = new System.Windows.Forms.TextBox();
            this.labelSquare = new System.Windows.Forms.Label();
            this.labelHook = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.textBoxOpening = new System.Windows.Forms.TextBox();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.pictureBoxWarnOpening = new System.Windows.Forms.PictureBox();
            this.labelLength = new System.Windows.Forms.Label();
            this.panelLS = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelOperation = new System.Windows.Forms.Label();
            this.labelOperationEnd = new System.Windows.Forms.Label();
            this.labelVelocity = new System.Windows.Forms.Label();
            this.textBoxVelocity = new System.Windows.Forms.TextBox();
            this.textBoxExposure = new System.Windows.Forms.TextBox();
            this.labelExposure = new System.Windows.Forms.Label();
            this.pictureBoxWarnExposure = new System.Windows.Forms.PictureBox();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.panelGeoData = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelExactArea = new System.Windows.Forms.Label();
            this.textBoxExactArea = new System.Windows.Forms.TextBox();
            this.textBoxHook = new System.Windows.Forms.TextBox();
            this.MenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageCollect.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).BeginInit();
            this.tabPageFactors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAddt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnOpening)).BeginInit();
            this.panelLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnExposure)).BeginInit();
            this.panelGeoData.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWater
            // 
            resources.ApplyResources(this.labelWater, "labelWater");
            this.labelWater.Name = "labelWater";
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
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator8,
            this.menuItemPreview,
            this.menuItemPrint,
            this.menuItemBlank,
            this.toolStripSeparator9,
            this.menuItemClose});
            this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            resources.ApplyResources(this.ToolStripMenuItemFile, "ToolStripMenuItemFile");
            // 
            // menuItemNew
            // 
            this.menuItemNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEmpty,
            this.menuItemSpot,
            this.menuItemGear});
            this.menuItemNew.Name = "menuItemNew";
            resources.ApplyResources(this.menuItemNew, "menuItemNew");
            // 
            // menuItemEmpty
            // 
            resources.ApplyResources(this.menuItemEmpty, "menuItemEmpty");
            this.menuItemEmpty.Name = "menuItemEmpty";
            this.menuItemEmpty.Click += new System.EventHandler(this.menuItemEmpty_Click);
            // 
            // menuItemSpot
            // 
            this.menuItemSpot.Name = "menuItemSpot";
            resources.ApplyResources(this.menuItemSpot, "menuItemSpot");
            this.menuItemSpot.Click += new System.EventHandler(this.menuItemSpot_Click);
            // 
            // menuItemGear
            // 
            this.menuItemGear.Name = "menuItemGear";
            resources.ApplyResources(this.menuItemGear, "menuItemGear");
            this.menuItemGear.Click += new System.EventHandler(this.menuItemGear_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            resources.ApplyResources(this.menuItemOpen, "menuItemOpen");
            this.menuItemOpen.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            resources.ApplyResources(this.menuItemSave, "menuItemSave");
            this.menuItemSave.Click += new System.EventHandler(this.ToolStripMenuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            resources.ApplyResources(this.menuItemSaveAs, "menuItemSaveAs");
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // menuItemPreview
            // 
            this.menuItemPreview.Name = "menuItemPreview";
            resources.ApplyResources(this.menuItemPreview, "menuItemPreview");
            this.menuItemPreview.Click += new System.EventHandler(this.ToolStripMenuItemPrintPreview_Click);
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Name = "menuItemPrint";
            resources.ApplyResources(this.menuItemPrint, "menuItemPrint");
            this.menuItemPrint.Click += new System.EventHandler(this.ToolStripMenuItemPrint_Click);
            // 
            // menuItemBlank
            // 
            this.menuItemBlank.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCardBlank,
            this.ToolStripMenuItemIndividualsLogBlank,
            this.ToolStripMenuItemIndividualProfileBlank});
            this.menuItemBlank.Name = "menuItemBlank";
            resources.ApplyResources(this.menuItemBlank, "menuItemBlank");
            // 
            // ToolStripMenuItemCardBlank
            // 
            this.ToolStripMenuItemCardBlank.Name = "ToolStripMenuItemCardBlank";
            resources.ApplyResources(this.ToolStripMenuItemCardBlank, "ToolStripMenuItemCardBlank");
            this.ToolStripMenuItemCardBlank.Click += new System.EventHandler(this.ToolStripMenuItemCardBlank_Click);
            // 
            // ToolStripMenuItemIndividualsLogBlank
            // 
            this.ToolStripMenuItemIndividualsLogBlank.Name = "ToolStripMenuItemIndividualsLogBlank";
            resources.ApplyResources(this.ToolStripMenuItemIndividualsLogBlank, "ToolStripMenuItemIndividualsLogBlank");
            this.ToolStripMenuItemIndividualsLogBlank.Click += new System.EventHandler(this.ToolStripMenuItemIndividualsLogBlank_Click);
            // 
            // ToolStripMenuItemIndividualProfileBlank
            // 
            this.ToolStripMenuItemIndividualProfileBlank.Name = "ToolStripMenuItemIndividualProfileBlank";
            resources.ApplyResources(this.ToolStripMenuItemIndividualProfileBlank, "ToolStripMenuItemIndividualProfileBlank");
            this.ToolStripMenuItemIndividualProfileBlank.Click += new System.EventHandler(this.ToolStripMenuItemIndividualProfileBlank_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.ToolStripMenuItemClose_Click);
            // 
            // ToolStripMenuItemData
            // 
            this.ToolStripMenuItemData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meniItemEnvironment,
            this.menuItemFactors,
            this.toolStripSeparator7,
            this.menuItemLocation});
            this.ToolStripMenuItemData.Name = "ToolStripMenuItemData";
            resources.ApplyResources(this.ToolStripMenuItemData, "ToolStripMenuItemData");
            // 
            // meniItemEnvironment
            // 
            this.meniItemEnvironment.Name = "meniItemEnvironment";
            resources.ApplyResources(this.meniItemEnvironment, "meniItemEnvironment");
            this.meniItemEnvironment.Click += new System.EventHandler(this.menuItemEnvironment_Click);
            // 
            // menuItemFactors
            // 
            this.menuItemFactors.Name = "menuItemFactors";
            resources.ApplyResources(this.menuItemFactors, "menuItemFactors");
            this.menuItemFactors.Click += new System.EventHandler(this.menuItemFactors_Click);
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
            this.menuItemWatersRef,
            this.menuItemSpeciesRef,
            this.toolStripSeparator3,
            this.menuItemSettings,
            this.itemAboutCard,
            this.menuItemAbout});
            this.ToolStripMenuItemService.Name = "ToolStripMenuItemService";
            resources.ApplyResources(this.ToolStripMenuItemService, "ToolStripMenuItemService");
            // 
            // menuItemWatersRef
            // 
            this.menuItemWatersRef.Name = "menuItemWatersRef";
            resources.ApplyResources(this.menuItemWatersRef, "menuItemWatersRef");
            this.menuItemWatersRef.Click += new System.EventHandler(this.ToolStripMenuItemWatersRef_Click);
            // 
            // menuItemSpeciesRef
            // 
            this.menuItemSpeciesRef.Name = "menuItemSpeciesRef";
            resources.ApplyResources(this.menuItemSpeciesRef, "menuItemSpeciesRef");
            this.menuItemSpeciesRef.Click += new System.EventHandler(this.ToolStripMenuItemSpeciesRef_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Click += new System.EventHandler(this.ToolStripMenuItemSettings_Click);
            // 
            // itemAboutCard
            // 
            this.itemAboutCard.Name = "itemAboutCard";
            resources.ApplyResources(this.itemAboutCard, "itemAboutCard");
            this.itemAboutCard.Click += new System.EventHandler(this.itemAboutCard_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuItemAddInfo
            // 
            resources.ApplyResources(this.menuItemAddInfo, "menuItemAddInfo");
            this.menuItemAddInfo.Name = "menuItemAddInfo";
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
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageCollect);
            this.tabControl.Controls.Add(this.tabPageEnvironment);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Controls.Add(this.tabPageFactors);
            this.tabControl.Controls.Add(this.tabPageSampler);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.AllowDrop = true;
            this.tabPageCollect.Controls.Add(this.labelTag);
            this.tabPageCollect.Controls.Add(this.textBoxDepth);
            this.tabPageCollect.Controls.Add(this.waterSelector);
            this.tabPageCollect.Controls.Add(this.labelDepth);
            this.tabPageCollect.Controls.Add(this.labelLabel);
            this.tabPageCollect.Controls.Add(this.labelComments);
            this.tabPageCollect.Controls.Add(this.labelWater);
            this.tabPageCollect.Controls.Add(this.textBoxLabel);
            this.tabPageCollect.Controls.Add(this.labelPosition);
            this.tabPageCollect.Controls.Add(this.textBoxComments);
            this.tabPageCollect.Controls.Add(this.waypointControl1);
            resources.ApplyResources(this.tabPageCollect, "tabPageCollect");
            this.tabPageCollect.Name = "tabPageCollect";
            this.tabPageCollect.UseVisualStyleBackColor = true;
            this.tabPageCollect.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            // 
            // labelTag
            // 
            resources.ApplyResources(this.labelTag, "labelTag");
            this.labelTag.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTag.Name = "labelTag";
            // 
            // textBoxDepth
            // 
            resources.ApplyResources(this.textBoxDepth, "textBoxDepth");
            this.textBoxDepth.Name = "textBoxDepth";
            this.textBoxDepth.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxDepth.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxDepth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // waterSelector
            // 
            resources.ApplyResources(this.waterSelector, "waterSelector");
            this.waterSelector.Name = "waterSelector";
            this.waterSelector.WaterObject = null;
            this.waterSelector.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.waterSelector_WaterSelected);
            // 
            // labelDepth
            // 
            resources.ApplyResources(this.labelDepth, "labelDepth");
            this.labelDepth.Name = "labelDepth";
            // 
            // labelLabel
            // 
            resources.ApplyResources(this.labelLabel, "labelLabel");
            this.labelLabel.Name = "labelLabel";
            // 
            // labelComments
            // 
            resources.ApplyResources(this.labelComments, "labelComments");
            this.labelComments.Name = "labelComments";
            // 
            // textBoxLabel
            // 
            resources.ApplyResources(this.textBoxLabel, "textBoxLabel");
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelPosition
            // 
            resources.ApplyResources(this.labelPosition, "labelPosition");
            this.labelPosition.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPosition.Name = "labelPosition";
            // 
            // textBoxComments
            // 
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // waypointControl1
            // 
            this.waypointControl1.AllowDrop = true;
            this.waypointControl1.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.waypointControl1, "waypointControl1");
            this.waypointControl1.Name = "waypointControl1";
            this.waypointControl1.ReadOnly = false;
            this.waypointControl1.Changed += new System.EventHandler(this.waypointControl1_Changed);
            this.waypointControl1.LocationImported += new Mayfly.Geographics.LocationDataEventHandler(this.waypointControl1_LocationImported);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.textBoxVolume);
            this.tabPageSampler.Controls.Add(this.labelEffort);
            this.tabPageSampler.Controls.Add(this.labelVolume);
            this.tabPageSampler.Controls.Add(this.buttonGear);
            this.tabPageSampler.Controls.Add(this.textBoxEfforts);
            this.tabPageSampler.Controls.Add(this.labelEfforts);
            this.tabPageSampler.Controls.Add(this.textBoxArea);
            this.tabPageSampler.Controls.Add(this.labelArea);
            this.tabPageSampler.Controls.Add(this.labelMethod);
            this.tabPageSampler.Controls.Add(this.labelOpening);
            this.tabPageSampler.Controls.Add(this.labelSampler);
            this.tabPageSampler.Controls.Add(this.textBoxHeight);
            this.tabPageSampler.Controls.Add(this.labelMesh);
            this.tabPageSampler.Controls.Add(this.textBoxSquare);
            this.tabPageSampler.Controls.Add(this.textBoxMesh);
            this.tabPageSampler.Controls.Add(this.labelSquare);
            this.tabPageSampler.Controls.Add(this.labelHook);
            this.tabPageSampler.Controls.Add(this.labelHeight);
            this.tabPageSampler.Controls.Add(this.textBoxHook);
            this.tabPageSampler.Controls.Add(this.textBoxOpening);
            this.tabPageSampler.Controls.Add(this.textBoxLength);
            this.tabPageSampler.Controls.Add(this.pictureBoxWarnOpening);
            this.tabPageSampler.Controls.Add(this.labelLength);
            this.tabPageSampler.Controls.Add(this.panelGeoData);
            this.tabPageSampler.Controls.Add(this.comboBoxSampler);
            this.tabPageSampler.Controls.Add(this.panelLS);
            resources.ApplyResources(this.tabPageSampler, "tabPageSampler");
            this.tabPageSampler.Name = "tabPageSampler";
            this.tabPageSampler.UseVisualStyleBackColor = true;
            // 
            // buttonGear
            // 
            resources.ApplyResources(this.buttonGear, "buttonGear");
            this.buttonGear.Name = "buttonGear";
            this.buttonGear.Click += new System.EventHandler(this.buttonGear_Click);
            // 
            // labelMethod
            // 
            resources.ApplyResources(this.labelMethod, "labelMethod");
            this.labelMethod.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelMethod.Name = "labelMethod";
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
            aquaState3.Colour = -1;
            aquaState3.Conductivity = double.NaN;
            aquaState3.DissolvedOxygen = double.NaN;
            aquaState3.FlowRate = double.NaN;
            aquaState3.Foam = Mayfly.Wild.OrganolepticState.Absent;
            aquaState3.Limpidity = double.NaN;
            aquaState3.Odor = Mayfly.Wild.OrganolepticState.Absent;
            aquaState3.OxygenSaturation = double.NaN;
            aquaState3.pH = double.NaN;
            aquaState3.Sewage = Mayfly.Wild.OrganolepticState.Absent;
            aquaState3.TemperatureBottom = double.NaN;
            aquaState3.TemperatureSurface = double.NaN;
            aquaState3.Turbidity = Mayfly.Wild.OrganolepticState.Absent;
            this.aquaControl1.AquaState = aquaState3;
            this.aquaControl1.BackColor = System.Drawing.SystemColors.Window;
            this.aquaControl1.Name = "aquaControl1";
            // 
            // weatherControl1
            // 
            resources.ApplyResources(this.weatherControl1, "weatherControl1");
            this.weatherControl1.BackColor = System.Drawing.SystemColors.Window;
            this.weatherControl1.Name = "weatherControl1";
            weatherState3.AdditionalEvent = 0;
            weatherState3.Cloudage = double.NaN;
            weatherState3.Degree = 0;
            weatherState3.Discretion = 0;
            weatherState3.Event = 0;
            weatherState3.Humidity = double.NaN;
            weatherState3.Pressure = double.NaN;
            weatherState3.Temperature = double.NaN;
            weatherState3.WindDirection = double.NaN;
            weatherState3.WindRate = double.NaN;
            this.weatherControl1.Weather = weatherState3;
            this.weatherControl1.Changed += new System.EventHandler(this.weatherControl1_Changed);
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
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.spreadSheetLog);
            this.tabPageLog.Controls.Add(this.buttonAdd);
            resources.ApplyResources(this.tabPageLog, "tabPageLog");
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.UseVisualStyleBackColor = true;
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
            this.ColumnQuantity,
            this.ColumnMass});
            this.spreadSheetLog.DefaultDecimalPlaces = 0;
            this.spreadSheetLog.Name = "spreadSheetLog";
            this.spreadSheetLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.spreadSheetLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnID
            // 
            resources.ApplyResources(this.ColumnID, "ColumnID");
            this.ColumnID.Name = "ColumnID";
            // 
            // ColumnSpecies
            // 
            this.ColumnSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSpecies.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnSpecies.FillWeight = 200F;
            resources.ApplyResources(this.ColumnSpecies, "ColumnSpecies");
            this.ColumnSpecies.Name = "ColumnSpecies";
            // 
            // ColumnQuantity
            // 
            this.ColumnQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnQuantity.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnQuantity.FillWeight = 30F;
            resources.ApplyResources(this.ColumnQuantity, "ColumnQuantity");
            this.ColumnQuantity.Name = "ColumnQuantity";
            // 
            // ColumnMass
            // 
            this.ColumnMass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnMass.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColumnMass.FillWeight = 30F;
            resources.ApplyResources(this.ColumnMass, "ColumnMass");
            this.ColumnMass.Name = "ColumnMass";
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
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
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.NullValue = null;
            this.ColumnAddtFactor.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.ColumnAddtFactor, "ColumnAddtFactor");
            this.ColumnAddtFactor.Name = "ColumnAddtFactor";
            // 
            // ColumnAddtValue
            // 
            this.ColumnAddtValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N1";
            this.ColumnAddtValue.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.ColumnAddtValue, "ColumnAddtValue");
            this.ColumnAddtValue.Name = "ColumnAddtValue";
            // 
            // contextGear
            // 
            this.contextGear.Name = "contextGear";
            resources.ApplyResources(this.contextGear, "contextGear");
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
            // tdbCancelTrack
            // 
            this.tdbCancelTrack.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // tdbSinglepoint
            // 
            resources.ApplyResources(this.tdbSinglepoint, "tdbSinglepoint");
            // 
            // tdbAsPoly
            // 
            resources.ApplyResources(this.tdbAsPoly, "tdbAsPoly");
            // 
            // tdbExposure
            // 
            resources.ApplyResources(this.tdbExposure, "tdbExposure");
            // 
            // taskDialogTrackHandle
            // 
            this.taskDialogTrackHandle.Buttons.Add(this.tdbExposure);
            this.taskDialogTrackHandle.Buttons.Add(this.tdbAsPoly);
            this.taskDialogTrackHandle.Buttons.Add(this.tdbSinglepoint);
            this.taskDialogTrackHandle.Buttons.Add(this.tdbCancelTrack);
            resources.ApplyResources(this.taskDialogTrackHandle, "taskDialogTrackHandle");
            // 
            // taskDialogLocationHandle
            // 
            this.taskDialogLocationHandle.Buttons.Add(this.tdbSinking);
            this.taskDialogLocationHandle.Buttons.Add(this.tdbRemoval);
            resources.ApplyResources(this.taskDialogLocationHandle, "taskDialogLocationHandle");
            // 
            // tdbSinking
            // 
            this.tdbSinking.Default = true;
            resources.ApplyResources(this.tdbSinking, "tdbSinking");
            // 
            // tdbRemoval
            // 
            resources.ApplyResources(this.tdbRemoval, "tdbRemoval");
            // 
            // statusCard
            // 
            this.statusCard.Default = null;
            this.statusCard.MaximalInterval = 2000;
            this.statusCard.StatusLog = this.StatusLog;
            // 
            // Logger
            // 
            this.Logger.AllowKey = false;
            this.Logger.AutoLogOpen = false;
            this.Logger.Button = this.buttonAdd;
            this.Logger.Grid = this.spreadSheetLog;
            this.Logger.LabelMass = this.StatusMass;
            this.Logger.LabelQty = this.StatusCount;
            this.Logger.Status = this.statusCard;
            this.Logger.Changed += new System.EventHandler(this.value_Changed);
            this.Logger.IndividualsRequired += new System.EventHandler(this.logger_IndividualsRequired);
            // 
            // textBoxVolume
            // 
            resources.ApplyResources(this.textBoxVolume, "textBoxVolume");
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.ReadOnly = true;
            this.textBoxVolume.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelEffort
            // 
            resources.ApplyResources(this.labelEffort, "labelEffort");
            this.labelEffort.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelEffort.Name = "labelEffort";
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // textBoxEfforts
            // 
            resources.ApplyResources(this.textBoxEfforts, "textBoxEfforts");
            this.textBoxEfforts.Name = "textBoxEfforts";
            this.textBoxEfforts.ReadOnly = true;
            this.textBoxEfforts.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxEfforts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelEfforts
            // 
            resources.ApplyResources(this.labelEfforts, "labelEfforts");
            this.labelEfforts.Name = "labelEfforts";
            // 
            // textBoxArea
            // 
            resources.ApplyResources(this.textBoxArea, "textBoxArea");
            this.textBoxArea.Name = "textBoxArea";
            this.textBoxArea.ReadOnly = true;
            this.textBoxArea.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelArea
            // 
            resources.ApplyResources(this.labelArea, "labelArea");
            this.labelArea.Name = "labelArea";
            // 
            // labelOpening
            // 
            resources.ApplyResources(this.labelOpening, "labelOpening");
            this.labelOpening.Name = "labelOpening";
            // 
            // labelSampler
            // 
            resources.ApplyResources(this.labelSampler, "labelSampler");
            this.labelSampler.Name = "labelSampler";
            // 
            // textBoxHeight
            // 
            resources.ApplyResources(this.textBoxHeight, "textBoxHeight");
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxHeight.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelMesh
            // 
            resources.ApplyResources(this.labelMesh, "labelMesh");
            this.labelMesh.Name = "labelMesh";
            // 
            // textBoxSquare
            // 
            resources.ApplyResources(this.textBoxSquare, "textBoxSquare");
            this.textBoxSquare.Name = "textBoxSquare";
            this.textBoxSquare.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxSquare.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxSquare.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxMesh
            // 
            resources.ApplyResources(this.textBoxMesh, "textBoxMesh");
            this.textBoxMesh.Name = "textBoxMesh";
            this.textBoxMesh.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxMesh.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxMesh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelSquare
            // 
            resources.ApplyResources(this.labelSquare, "labelSquare");
            this.labelSquare.Name = "labelSquare";
            // 
            // labelHook
            // 
            resources.ApplyResources(this.labelHook, "labelHook");
            this.labelHook.Name = "labelHook";
            // 
            // labelHeight
            // 
            resources.ApplyResources(this.labelHeight, "labelHeight");
            this.labelHeight.Name = "labelHeight";
            // 
            // textBoxOpening
            // 
            resources.ApplyResources(this.textBoxOpening, "textBoxOpening");
            this.textBoxOpening.Name = "textBoxOpening";
            this.textBoxOpening.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxOpening.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxOpening.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxLength
            // 
            resources.ApplyResources(this.textBoxLength, "textBoxLength");
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxLength.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // pictureBoxWarnOpening
            // 
            resources.ApplyResources(this.pictureBoxWarnOpening, "pictureBoxWarnOpening");
            this.pictureBoxWarnOpening.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarnOpening.Name = "pictureBoxWarnOpening";
            this.pictureBoxWarnOpening.TabStop = false;
            this.pictureBoxWarnOpening.DoubleClick += new System.EventHandler(this.pictureBoxWarnOpening_DoubleClick);
            this.pictureBoxWarnOpening.MouseLeave += new System.EventHandler(this.pictureBoxWarnOpening_MouseLeave);
            this.pictureBoxWarnOpening.MouseHover += new System.EventHandler(this.pictureBoxWarnOpening_MouseHover);
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
            // 
            // panelLS
            // 
            resources.ApplyResources(this.panelLS, "panelLS");
            this.panelLS.Controls.Add(this.dateTimePickerEnd);
            this.panelLS.Controls.Add(this.dateTimePickerStart);
            this.panelLS.Controls.Add(this.pictureBoxWarnExposure);
            this.panelLS.Controls.Add(this.labelExposure);
            this.panelLS.Controls.Add(this.textBoxExposure);
            this.panelLS.Controls.Add(this.textBoxVelocity);
            this.panelLS.Controls.Add(this.labelVelocity);
            this.panelLS.Controls.Add(this.labelOperationEnd);
            this.panelLS.Controls.Add(this.labelOperation);
            this.panelLS.Controls.Add(this.labelDuration);
            this.panelLS.Name = "panelLS";
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Name = "dateTimePicker1";
            // 
            // dateTimePicker2
            // 
            resources.ApplyResources(this.dateTimePicker2, "dateTimePicker2");
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Name = "dateTimePicker2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // textBox4
            // 
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Name = "label6";
            // 
            // labelDuration
            // 
            resources.ApplyResources(this.labelDuration, "labelDuration");
            this.labelDuration.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelDuration.Name = "labelDuration";
            // 
            // labelOperation
            // 
            resources.ApplyResources(this.labelOperation, "labelOperation");
            this.labelOperation.Name = "labelOperation";
            // 
            // labelOperationEnd
            // 
            resources.ApplyResources(this.labelOperationEnd, "labelOperationEnd");
            this.labelOperationEnd.Name = "labelOperationEnd";
            // 
            // labelVelocity
            // 
            resources.ApplyResources(this.labelVelocity, "labelVelocity");
            this.labelVelocity.Name = "labelVelocity";
            // 
            // textBoxVelocity
            // 
            resources.ApplyResources(this.textBoxVelocity, "textBoxVelocity");
            this.textBoxVelocity.Name = "textBoxVelocity";
            this.textBoxVelocity.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxVelocity.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxVelocity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxExposure
            // 
            resources.ApplyResources(this.textBoxExposure, "textBoxExposure");
            this.textBoxExposure.Name = "textBoxExposure";
            this.textBoxExposure.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxExposure.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxExposure.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelExposure
            // 
            resources.ApplyResources(this.labelExposure, "labelExposure");
            this.labelExposure.Name = "labelExposure";
            // 
            // pictureBoxWarnExposure
            // 
            resources.ApplyResources(this.pictureBoxWarnExposure, "pictureBoxWarnExposure");
            this.pictureBoxWarnExposure.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarnExposure.Name = "pictureBoxWarnExposure";
            this.pictureBoxWarnExposure.TabStop = false;
            this.pictureBoxWarnExposure.DoubleClick += new System.EventHandler(this.pictureBoxWarnExposure_DoubleClick);
            this.pictureBoxWarnExposure.MouseLeave += new System.EventHandler(this.pictureBoxWarnExposure_MouseLeave);
            this.pictureBoxWarnExposure.MouseHover += new System.EventHandler(this.pictureBoxWarnExposure_MouseHover);
            // 
            // dateTimePickerStart
            // 
            resources.ApplyResources(this.dateTimePickerStart, "dateTimePickerStart");
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.ValueChanged += new System.EventHandler(this.dateTimePickerStart_ValueChanged);
            this.dateTimePickerStart.EnabledChanged += new System.EventHandler(this.dateTimePickerStart_EnabledChanged);
            // 
            // dateTimePickerEnd
            // 
            resources.ApplyResources(this.dateTimePickerEnd, "dateTimePickerEnd");
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.ValueChanged += new System.EventHandler(this.dateTimePickerEnd_ValueChanged);
            this.dateTimePickerEnd.EnabledChanged += new System.EventHandler(this.dateTimePickerStart_EnabledChanged);
            // 
            // panelGeoData
            // 
            resources.ApplyResources(this.panelGeoData, "panelGeoData");
            this.panelGeoData.Controls.Add(this.textBoxExactArea);
            this.panelGeoData.Controls.Add(this.labelExactArea);
            this.panelGeoData.Name = "panelGeoData";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelExactArea
            // 
            resources.ApplyResources(this.labelExactArea, "labelExactArea");
            this.labelExactArea.Name = "labelExactArea";
            // 
            // textBoxExactArea
            // 
            resources.ApplyResources(this.textBoxExactArea, "textBoxExactArea");
            this.textBoxExactArea.Name = "textBoxExactArea";
            this.textBoxExactArea.ReadOnly = true;
            this.textBoxExactArea.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxExactArea.TextChanged += new System.EventHandler(this.sampler_ValueChanged);
            this.textBoxExactArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxHook
            // 
            resources.ApplyResources(this.textBoxHook, "textBoxHook");
            this.textBoxHook.Name = "textBoxHook";
            this.textBoxHook.EnabledChanged += new System.EventHandler(this.samplerValue_EnabledChanged);
            this.textBoxHook.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxHook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // Card
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "Card";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Card_FormClosing);
            this.Load += new System.EventHandler(this.Card_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).EndInit();
            this.tabPageFactors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAddt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnOpening)).EndInit();
            this.panelLS.ResumeLayout(false);
            this.panelLS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarnExposure)).EndInit();
            this.panelGeoData.ResumeLayout(false);
            this.panelGeoData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWater;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemService;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddInfo;
        private System.Windows.Forms.TabPage tabPageCollect;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemWatersRef;
        private System.Windows.Forms.ToolStripMenuItem menuItemSpeciesRef;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelMethod;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuItemPrint;
        private System.Windows.Forms.ToolStripMenuItem menuItemPreview;
        private Mayfly.Controls.Status statusCard;
        private Mayfly.TaskDialogs.TaskDialog taskDialogSaveChanges;
        private Mayfly.TaskDialogs.TaskDialogButton tdbSave;
        private Mayfly.TaskDialogs.TaskDialogButton tdbDiscard;
        private Mayfly.TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemData;
        private System.Windows.Forms.ToolStripMenuItem menuItemLocation;
        private System.Windows.Forms.TabPage tabPageEnvironment;
        private System.Windows.Forms.Label labelActWeather;
        private System.Windows.Forms.Label labelWaterConds;
        private System.Windows.Forms.ToolStripMenuItem menuItemFactors;
        private System.Windows.Forms.TabPage tabPageFactors;
        private Mayfly.Controls.SpreadSheet spreadSheetLog;
        private System.Windows.Forms.ToolStripMenuItem meniItemEnvironment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripStatusLabel StatusLog;
        private System.Windows.Forms.ToolStripStatusLabel StatusCount;
        private System.Windows.Forms.ToolStripStatusLabel StatusMass;
        private System.Windows.Forms.ToolStripMenuItem menuItemBlank;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCardBlank;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndividualsLogBlank;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndividualProfileBlank;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private Mayfly.Controls.SpreadSheet spreadSheetAddt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddtFactor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddtValue;
        private System.Windows.Forms.Label labelComments;
        private Wild.Controls.WeatherControl weatherControl1;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.TextBox textBoxDepth;
        private System.Windows.Forms.Label labelLabel;
        private Wild.Controls.AquaControl aquaControl1;
        private Geographics.WaypointControl waypointControl1;
        private Waters.Controls.WaterSelector waterSelector;
        private System.Windows.Forms.ToolStripMenuItem itemAboutCard;
        private System.Windows.Forms.ToolStripMenuItem menuItemEmpty;
        private System.Windows.Forms.ToolStripMenuItem menuItemSpot;
        private System.Windows.Forms.ToolStripMenuItem menuItemGear;
        private TaskDialogs.TaskDialogButton tdbCancelTrack;
        private TaskDialogs.TaskDialogButton tdbSinglepoint;
        private TaskDialogs.TaskDialogButton tdbAsPoly;
        private TaskDialogs.TaskDialogButton tdbExposure;
        private TaskDialogs.TaskDialog taskDialogTrackHandle;
        private GroupedComboBox comboBoxSampler;
        private TaskDialogs.TaskDialog taskDialogLocationHandle;
        private TaskDialogs.TaskDialogButton tdbSinking;
        private TaskDialogs.TaskDialogButton tdbRemoval;
        private System.Windows.Forms.Button buttonGear;
        private System.Windows.Forms.ContextMenuStrip contextGear;
        private System.Windows.Forms.TabPage tabPageSampler;
        private System.Windows.Forms.Label labelTag;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMass;
        internal Wild.Controls.LogProcessor Logger;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.Label labelEffort;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.TextBox textBoxEfforts;
        private System.Windows.Forms.Label labelEfforts;
        private System.Windows.Forms.TextBox textBoxArea;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.Label labelOpening;
        private System.Windows.Forms.Label labelSampler;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelMesh;
        private System.Windows.Forms.TextBox textBoxSquare;
        private System.Windows.Forms.TextBox textBoxMesh;
        private System.Windows.Forms.Label labelSquare;
        private System.Windows.Forms.Label labelHook;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.TextBox textBoxHook;
        private System.Windows.Forms.TextBox textBoxOpening;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.PictureBox pictureBoxWarnOpening;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Panel panelLS;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.PictureBox pictureBoxWarnExposure;
        private System.Windows.Forms.Label labelExposure;
        private System.Windows.Forms.TextBox textBoxExposure;
        private System.Windows.Forms.TextBox textBoxVelocity;
        private System.Windows.Forms.Label labelVelocity;
        private System.Windows.Forms.Label labelOperationEnd;
        private System.Windows.Forms.Label labelOperation;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Panel panelGeoData;
        private System.Windows.Forms.TextBox textBoxExactArea;
        private System.Windows.Forms.Label labelExactArea;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
    }
}