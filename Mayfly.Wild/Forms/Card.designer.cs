namespace Mayfly.Wild
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            Mayfly.Wild.AquaState aquaState1 = new Mayfly.Wild.AquaState();
            Mayfly.Wild.WeatherState weatherState1 = new Mayfly.Wild.WeatherState();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCardBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemIndividualsLogBlank = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemData = new System.Windows.Forms.ToolStripMenuItem();
            this.addEnvironmentalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFactorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemService = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWatersRef = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSpeciesRef = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAboutCard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.spreadSheetLog = new Mayfly.Controls.SpreadSheet();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDefinition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQtyExam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMassExam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCollect = new System.Windows.Forms.TabPage();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelTag = new System.Windows.Forms.Label();
            this.waypointControl1 = new Mayfly.Geographics.WaypointControl();
            this.waterSelector = new Mayfly.Waters.Controls.WaterSelector();
            this.labelWater = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSampler = new System.Windows.Forms.TabPage();
            this.buttonEquipment = new System.Windows.Forms.Button();
            this.comboBoxSampler = new GroupedComboBox();
            this.labelMethod = new System.Windows.Forms.Label();
            this.labelSampler = new System.Windows.Forms.Label();
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
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.statusCard = new Mayfly.Controls.Status();
            this.contextEquipment = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Logger = new Mayfly.Wild.Controls.LogProcessor(this.components);
            this.MenuStrip.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
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
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator3,
            this.menuItemPreview,
            this.menuItemPrint,
            this.menuItemBlank,
            this.toolStripSeparator6,
            this.menuItemClose});
            this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            resources.ApplyResources(this.ToolStripMenuItemFile, "ToolStripMenuItemFile");
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            resources.ApplyResources(this.menuItemNew, "menuItemNew");
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            resources.ApplyResources(this.menuItemOpen, "menuItemOpen");
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // menuItemPreview
            // 
            this.menuItemPreview.Name = "menuItemPreview";
            resources.ApplyResources(this.menuItemPreview, "menuItemPreview");
            this.menuItemPreview.Click += new System.EventHandler(this.menuItemPrintPreview_Click);
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Name = "menuItemPrint";
            resources.ApplyResources(this.menuItemPrint, "menuItemPrint");
            this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // menuItemBlank
            // 
            this.menuItemBlank.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCardBlank,
            this.ToolStripMenuItemIndividualsLogBlank});
            this.menuItemBlank.Name = "menuItemBlank";
            resources.ApplyResources(this.menuItemBlank, "menuItemBlank");
            // 
            // ToolStripMenuItemCardBlank
            // 
            this.ToolStripMenuItemCardBlank.Name = "ToolStripMenuItemCardBlank";
            resources.ApplyResources(this.ToolStripMenuItemCardBlank, "ToolStripMenuItemCardBlank");
            this.ToolStripMenuItemCardBlank.Click += new System.EventHandler(this.menuItemCardBlank_Click);
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
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
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
            this.menuItemSettings,
            this.menuItemAboutCard,
            this.menuItemAbout});
            this.ToolStripMenuItemService.Name = "ToolStripMenuItemService";
            resources.ApplyResources(this.ToolStripMenuItemService, "ToolStripMenuItemService");
            // 
            // ToolStripMenuItemWatersRef
            // 
            this.ToolStripMenuItemWatersRef.Name = "ToolStripMenuItemWatersRef";
            resources.ApplyResources(this.ToolStripMenuItemWatersRef, "ToolStripMenuItemWatersRef");
            this.ToolStripMenuItemWatersRef.Click += new System.EventHandler(this.menuItemWaters_Click);
            // 
            // ToolStripMenuItemSpeciesRef
            // 
            this.ToolStripMenuItemSpeciesRef.Name = "ToolStripMenuItemSpeciesRef";
            resources.ApplyResources(this.ToolStripMenuItemSpeciesRef, "ToolStripMenuItemSpeciesRef");
            this.ToolStripMenuItemSpeciesRef.Click += new System.EventHandler(this.menuItemSpecies_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemAboutCard
            // 
            this.menuItemAboutCard.Name = "menuItemAboutCard";
            resources.ApplyResources(this.menuItemAboutCard, "menuItemAboutCard");
            this.menuItemAboutCard.Click += new System.EventHandler(this.itemAboutCard_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // tabPageLog
            // 
            this.tabPageLog.AllowDrop = true;
            this.tabPageLog.Controls.Add(this.buttonAdd);
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
            // spreadSheetLog
            // 
            this.spreadSheetLog.AllowUserToAddRows = true;
            this.spreadSheetLog.AllowUserToDeleteRows = true;
            resources.ApplyResources(this.spreadSheetLog, "spreadSheetLog");
            this.spreadSheetLog.AutoClearEmptyRows = true;
            this.spreadSheetLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.spreadSheetLog.CellPadding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.spreadSheetLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnDefinition,
            this.ColumnQtyExam,
            this.ColumnQty,
            this.ColumnMassExam,
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
            // ColumnDefinition
            // 
            this.ColumnDefinition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Format = "s";
            this.ColumnDefinition.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnDefinition.FillWeight = 200F;
            resources.ApplyResources(this.ColumnDefinition, "ColumnDefinition");
            this.ColumnDefinition.Name = "ColumnDefinition";
            // 
            // ColumnQtyExam
            // 
            this.ColumnQtyExam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnQtyExam.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnQtyExam.FillWeight = 30F;
            resources.ApplyResources(this.ColumnQtyExam, "ColumnQtyExam");
            this.ColumnQtyExam.Name = "ColumnQtyExam";
            // 
            // ColumnQty
            // 
            this.ColumnQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnQty.FillWeight = 30F;
            resources.ApplyResources(this.ColumnQty, "ColumnQty");
            this.ColumnQty.Name = "ColumnQty";
            // 
            // ColumnMassExam
            // 
            this.ColumnMassExam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Format = "N1";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnMassExam.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnMassExam.FillWeight = 30F;
            resources.ApplyResources(this.ColumnMassExam, "ColumnMassExam");
            this.ColumnMassExam.Name = "ColumnMassExam";
            // 
            // ColumnMass
            // 
            this.ColumnMass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Format = "N1";
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.ColumnMass.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnMass.FillWeight = 30F;
            resources.ApplyResources(this.ColumnMass, "ColumnMass");
            this.ColumnMass.Name = "ColumnMass";
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.labelPosition);
            this.tabPageCollect.Controls.Add(this.labelTag);
            this.tabPageCollect.Controls.Add(this.waypointControl1);
            this.tabPageCollect.Controls.Add(this.waterSelector);
            this.tabPageCollect.Controls.Add(this.labelLabel);
            this.tabPageCollect.Controls.Add(this.textBoxLabel);
            this.tabPageCollect.Controls.Add(this.labelWater);
            this.tabPageCollect.Controls.Add(this.textBoxComments);
            this.tabPageCollect.Controls.Add(this.labelComments);
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
            // labelTag
            // 
            resources.ApplyResources(this.labelTag, "labelTag");
            this.labelTag.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelTag.Name = "labelTag";
            // 
            // waypointControl1
            // 
            this.waypointControl1.AllowDrop = true;
            resources.ApplyResources(this.waypointControl1, "waypointControl1");
            this.waypointControl1.BackColor = System.Drawing.Color.Transparent;
            this.waypointControl1.CoordinateFormat = "d";
            this.waypointControl1.DateTimeFormat = "dd.MM.yyyy (ddd) HH:mm";
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
            // labelWater
            // 
            resources.ApplyResources(this.labelWater, "labelWater");
            this.labelWater.Name = "labelWater";
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
            this.tabPageSampler.Controls.Add(this.buttonEquipment);
            this.tabPageSampler.Controls.Add(this.comboBoxSampler);
            this.tabPageSampler.Controls.Add(this.labelMethod);
            this.tabPageSampler.Controls.Add(this.labelSampler);
            resources.ApplyResources(this.tabPageSampler, "tabPageSampler");
            this.tabPageSampler.Name = "tabPageSampler";
            this.tabPageSampler.UseVisualStyleBackColor = true;
            // 
            // buttonEquipment
            // 
            resources.ApplyResources(this.buttonEquipment, "buttonEquipment");
            this.buttonEquipment.Name = "buttonEquipment";
            this.buttonEquipment.Click += new System.EventHandler(this.buttonEquipment_Click);
            // 
            // comboBoxSampler
            // 
            resources.ApplyResources(this.comboBoxSampler, "comboBoxSampler");
            this.comboBoxSampler.DataSource = null;
            this.comboBoxSampler.DropDownHeight = 350;
            this.comboBoxSampler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampler.FormattingEnabled = true;
            this.comboBoxSampler.GroupMember = "TypeName";
            this.comboBoxSampler.Name = "comboBoxSampler";
            this.comboBoxSampler.SelectedIndexChanged += new System.EventHandler(this.comboBoxSampler_SelectedIndexChanged);
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
            aquaState1.Colour = -1;
            aquaState1.Conductivity = double.NaN;
            aquaState1.DissolvedOxygen = double.NaN;
            aquaState1.FlowRate = double.NaN;
            aquaState1.Foam = Mayfly.Wild.OrganolepticState.Absent;
            aquaState1.Limpidity = double.NaN;
            aquaState1.Odor = Mayfly.Wild.OrganolepticState.Absent;
            aquaState1.OxygenSaturation = double.NaN;
            aquaState1.pH = double.NaN;
            aquaState1.Sewage = Mayfly.Wild.OrganolepticState.Absent;
            aquaState1.TemperatureBottom = double.NaN;
            aquaState1.TemperatureSurface = double.NaN;
            aquaState1.Turbidity = Mayfly.Wild.OrganolepticState.Absent;
            this.aquaControl1.AquaState = aquaState1;
            this.aquaControl1.BackColor = System.Drawing.Color.Transparent;
            this.aquaControl1.Name = "aquaControl1";
            this.aquaControl1.Changed += new System.EventHandler(this.value_Changed);
            // 
            // weatherControl1
            // 
            resources.ApplyResources(this.weatherControl1, "weatherControl1");
            this.weatherControl1.BackColor = System.Drawing.Color.Transparent;
            this.weatherControl1.Name = "weatherControl1";
            weatherState1.AdditionalEvent = 0;
            weatherState1.Cloudage = double.NaN;
            weatherState1.Degree = 0;
            weatherState1.Discretion = 0;
            weatherState1.Event = 0;
            weatherState1.Humidity = double.NaN;
            weatherState1.Pressure = double.NaN;
            weatherState1.Temperature = double.NaN;
            weatherState1.WindDirection = double.NaN;
            weatherState1.WindRate = double.NaN;
            this.weatherControl1.Weather = weatherState1;
            this.weatherControl1.Changed += new System.EventHandler(this.value_Changed);
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.NullValue = null;
            this.ColumnAddtFactor.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnAddtFactor, "ColumnAddtFactor");
            this.ColumnAddtFactor.Name = "ColumnAddtFactor";
            // 
            // ColumnAddtValue
            // 
            this.ColumnAddtValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N1";
            this.ColumnAddtValue.DefaultCellStyle = dataGridViewCellStyle7;
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
            this.statusCard.Default = "© 2022 Валентин Безматерных";
            this.statusCard.MaximalInterval = 2000;
            this.statusCard.StatusLog = this.StatusLog;
            // 
            // contextEquipment
            // 
            this.contextEquipment.Name = "contextGear";
            resources.ApplyResources(this.contextEquipment, "contextEquipment");
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Format = "N1";
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.FillWeight = 30F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.card_FormClosing);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).EndInit();
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
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
        protected System.Windows.Forms.TabPage tabPageCollect;
        protected System.Windows.Forms.Label labelLabel;
        protected System.Windows.Forms.MenuStrip MenuStrip;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemService;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWatersRef;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSpeciesRef;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected System.Windows.Forms.TabPage tabPageLog;
        protected System.Windows.Forms.TabControl tabControl;
        protected System.Windows.Forms.StatusStrip statusStrip;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        protected System.Windows.Forms.ToolStripMenuItem menuItemSave;
        protected System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        protected System.Windows.Forms.ToolStripMenuItem menuItemClose;
        protected System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        protected System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        protected System.Windows.Forms.Label labelWater;
        protected System.Windows.Forms.Label labelComments;
        protected System.Windows.Forms.TabPage tabPageEnvironment;
        protected System.Windows.Forms.Label labelActWeather;
        protected System.Windows.Forms.Label labelWaterConds;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        protected System.Windows.Forms.ToolStripMenuItem menuItemPrint;
        protected System.Windows.Forms.ToolStripMenuItem menuItemPreview;
        protected System.Windows.Forms.TabPage tabPageFactors;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemData;
        protected System.Windows.Forms.ToolStripMenuItem menuItemLocation;
        protected global::Mayfly.Controls.Status statusCard;
        protected TaskDialogs.TaskDialog taskDialogSaveChanges;
        protected TaskDialogs.TaskDialogButton tdbSave;
        protected TaskDialogs.TaskDialogButton tdbDiscard;
        protected TaskDialogs.TaskDialogButton tdbCancelClose;
        protected System.Windows.Forms.ToolStripMenuItem addFactorsToolStripMenuItem;
        protected System.Windows.Forms.Button buttonAdd;
        protected System.Windows.Forms.ToolStripMenuItem addEnvironmentalDataToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        protected System.Windows.Forms.ToolStripMenuItem menuItemBlank;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCardBlank;
        protected System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemIndividualsLogBlank;
        protected global::Mayfly.Controls.SpreadSheet spreadSheetAddt;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddtFactor;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnAddtValue;
        protected Controls.WeatherControl weatherControl1;
        protected Controls.AquaControl aquaControl1;
        protected Geographics.WaypointControl waypointControl1;
        protected Waters.Controls.WaterSelector waterSelector;
        protected System.Windows.Forms.ToolStripMenuItem menuItemAboutCard;
        protected global::Mayfly.Controls.SpreadSheet spreadSheetLog;
        protected System.Windows.Forms.TabPage tabPageSampler;
        protected System.Windows.Forms.Label labelMethod;
        protected System.Windows.Forms.Label labelSampler;
        protected System.Windows.Forms.Label labelTag;
        protected System.Windows.Forms.Label labelPosition;
        protected System.Windows.Forms.TextBox textBoxLabel;
        protected System.Windows.Forms.TextBox textBoxComments;
        protected System.Windows.Forms.ToolStripMenuItem menuItemNew;
        protected System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        protected GroupedComboBox comboBoxSampler;
        protected System.Windows.Forms.Button buttonEquipment;
        private System.Windows.Forms.ContextMenuStrip contextEquipment;
        public Controls.LogProcessor Logger;
        private System.Windows.Forms.ToolStripStatusLabel StatusMass;
        private System.Windows.Forms.ToolStripStatusLabel StatusCount;
        private System.Windows.Forms.ToolStripStatusLabel StatusLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnQtyExam;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnMassExam;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnDefinition;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnQty;
        protected System.Windows.Forms.DataGridViewTextBoxColumn ColumnMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}