using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static Mayfly.Wild.SettingsReader;
using static Mayfly.Wild.UserSettings;

namespace Mayfly.Benthos
{
    public class BenthosCard : Wild.Card
    {
        private ComboBox comboBoxBank;
        private ComboBox comboBoxCrossSection;
        private Label labelCrossSection;
        private Mayfly.Controls.NumberBox numericPortions;
        private Label labelSieve;
        private Label labelPortions;
        private Label labelArea;
        private Mayfly.Controls.NumberBox numericSieve;
        private Mayfly.Controls.NumberBox numericArea;
        private ContextMenuStrip contextMenuStripSubstrate;
        private IContainer components;
        private ToolStripMenuItem ToolStripMenuItemSands;
        private ToolStripMenuItem ItemSubSand;
        private ToolStripMenuItem ItemSubLoamySand;
        private ToolStripMenuItem ToolStripMenuItemSilts;
        private ToolStripMenuItem ItemSubSilt;
        private ToolStripMenuItem ToolStripMenuItemClays;
        private ToolStripMenuItem ItemSubClay;
        private ToolStripMenuItem ItemSubSandyClay;
        private ToolStripMenuItem ItemSubSiltyClay;
        private ToolStripMenuItem ToolStripMenuItemLoams;
        private ToolStripMenuItem ItemSubLoam;
        private ToolStripMenuItem ItemSubSandyLoam;
        private ToolStripMenuItem ItemSubSandyClayLoam;
        private ToolStripMenuItem ItemSubClayLoam;
        private ToolStripMenuItem ItemSubSiltyClayLoam;
        private ToolStripMenuItem ItemSubSiltLoam;
        private TabPage tabPageSubstrate;
        private Label labelTexture;
        private Button buttonSubTexture;
        private Mayfly.Controls.NumberBox numericDebris;
        private CheckBox checkBoxClay;
        private Mayfly.Controls.NumberBox numericSapropel;
        private Mayfly.Controls.NumberBox numericCobble;
        private CheckBox checkBoxSilt;
        private Mayfly.Controls.NumberBox numericCPOM;
        private CheckBox checkBoxPhytal;
        private Mayfly.Controls.NumberBox numericFPOM;
        private CheckBox checkBoxSand;
        private Mayfly.Controls.NumberBox numericGravel;
        private Mayfly.Controls.NumberBox numericLiving;
        private Mayfly.Controls.NumberBox numericBoulder;
        private CheckBox checkBoxWood;
        private Mayfly.Controls.NumberBox numericWood;
        private CheckBox checkBoxGravel;
        private Mayfly.Controls.NumberBox numericSand;
        private CheckBox checkBoxDebris;
        private CheckBox checkBoxCobble;
        private CheckBox checkBoxLiving;
        private Mayfly.Controls.NumberBox numericPhytal;
        private CheckBox checkBoxBoulder;
        private Mayfly.Controls.NumberBox numericSilt;
        private CheckBox checkBoxSapropel;
        private Label labelOrganics;
        private CheckBox checkBoxFPOM;
        private CheckBox checkBoxCPOM;
        private Label labelMinerals;
        private Mayfly.Controls.NumberBox numericClay;
        private Label label17;
        private Mayfly.Controls.NumberBox numericDepth;
        private Label labelDepth;
        private Label labelWidth;
        private Mayfly.Controls.NumberBox numericWidth;
        private Label labelExposure;
        private Label labelSquare;
        private Mayfly.Controls.NumberBox numericExposure;
        private Mayfly.Controls.NumberBox numericSquare;
        private Label labelBank;
        private Mayfly.Controls.NumberBox[] MineralControls => new Mayfly.Controls.NumberBox[] { numericBoulder, numericCobble, numericGravel, numericSand, numericSilt, numericClay };


        public BenthosCard() : base() {

            InitializeComponent();
            Initiate();

            tabPageLog.Parent = null;
            tabPageLog.Parent = tabControl;
        }

        public BenthosCard(string filename) : this() {

            load(filename);
        }



        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.labelBank = new System.Windows.Forms.Label();
            this.labelCrossSection = new System.Windows.Forms.Label();
            this.comboBoxCrossSection = new System.Windows.Forms.ComboBox();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.numericPortions = new Mayfly.Controls.NumberBox();
            this.numericArea = new Mayfly.Controls.NumberBox();
            this.numericSieve = new Mayfly.Controls.NumberBox();
            this.labelArea = new System.Windows.Forms.Label();
            this.labelPortions = new System.Windows.Forms.Label();
            this.labelSieve = new System.Windows.Forms.Label();
            this.contextMenuStripSubstrate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemSands = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSand = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubLoamySand = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSilts = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSilt = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemClays = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubClay = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSandyClay = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSiltyClay = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLoams = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubLoam = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSandyLoam = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSandyClayLoam = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubClayLoam = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSiltyClayLoam = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSubSiltLoam = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageSubstrate = new System.Windows.Forms.TabPage();
            this.labelTexture = new System.Windows.Forms.Label();
            this.buttonSubTexture = new System.Windows.Forms.Button();
            this.numericDebris = new Mayfly.Controls.NumberBox();
            this.checkBoxClay = new System.Windows.Forms.CheckBox();
            this.numericSapropel = new Mayfly.Controls.NumberBox();
            this.numericCobble = new Mayfly.Controls.NumberBox();
            this.checkBoxSilt = new System.Windows.Forms.CheckBox();
            this.numericCPOM = new Mayfly.Controls.NumberBox();
            this.checkBoxPhytal = new System.Windows.Forms.CheckBox();
            this.numericFPOM = new Mayfly.Controls.NumberBox();
            this.checkBoxSand = new System.Windows.Forms.CheckBox();
            this.numericGravel = new Mayfly.Controls.NumberBox();
            this.numericLiving = new Mayfly.Controls.NumberBox();
            this.numericBoulder = new Mayfly.Controls.NumberBox();
            this.checkBoxWood = new System.Windows.Forms.CheckBox();
            this.numericWood = new Mayfly.Controls.NumberBox();
            this.checkBoxGravel = new System.Windows.Forms.CheckBox();
            this.numericSand = new Mayfly.Controls.NumberBox();
            this.checkBoxDebris = new System.Windows.Forms.CheckBox();
            this.checkBoxCobble = new System.Windows.Forms.CheckBox();
            this.checkBoxLiving = new System.Windows.Forms.CheckBox();
            this.numericPhytal = new Mayfly.Controls.NumberBox();
            this.checkBoxBoulder = new System.Windows.Forms.CheckBox();
            this.numericSilt = new Mayfly.Controls.NumberBox();
            this.checkBoxSapropel = new System.Windows.Forms.CheckBox();
            this.labelOrganics = new System.Windows.Forms.Label();
            this.checkBoxFPOM = new System.Windows.Forms.CheckBox();
            this.checkBoxCPOM = new System.Windows.Forms.CheckBox();
            this.labelMinerals = new System.Windows.Forms.Label();
            this.numericClay = new Mayfly.Controls.NumberBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numericDepth = new Mayfly.Controls.NumberBox();
            this.labelDepth = new System.Windows.Forms.Label();
            this.numericWidth = new Mayfly.Controls.NumberBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.numericExposure = new Mayfly.Controls.NumberBox();
            this.labelExposure = new System.Windows.Forms.Label();
            this.numericSquare = new Mayfly.Controls.NumberBox();
            this.labelSquare = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            this.contextMenuStripSubstrate.SuspendLayout();
            this.tabPageSubstrate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.numericDepth);
            this.tabPageCollect.Controls.Add(this.labelDepth);
            this.tabPageCollect.Controls.Add(this.comboBoxBank);
            this.tabPageCollect.Controls.Add(this.comboBoxCrossSection);
            this.tabPageCollect.Controls.Add(this.labelCrossSection);
            this.tabPageCollect.Controls.Add(this.labelBank);
            this.tabPageCollect.Controls.SetChildIndex(this.labelBank, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelCrossSection, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBoxCrossSection, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBoxBank, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelDepth, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.numericDepth, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelWater, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waterSelector, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waypointControl1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelTag, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelPosition, 0);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSubstrate);
            this.tabControl.Controls.SetChildIndex(this.tabPageSubstrate, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageFactors, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageLog, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageEnvironment, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageSampler, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageCollect, 0);
            // 
            // labelComments
            // 
            this.labelComments.Location = new System.Drawing.Point(45, 340);
            this.labelComments.TabIndex = 13;
            // 
            // waypointControl1
            // 
            this.waypointControl1.Location = new System.Drawing.Point(45, 225);
            this.waypointControl1.TabIndex = 12;
            // 
            // waterSelector
            // 
            this.waterSelector.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.waterSelector_WaterSelected);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.label17);
            this.tabPageSampler.Controls.Add(this.numericPortions);
            this.tabPageSampler.Controls.Add(this.labelExposure);
            this.tabPageSampler.Controls.Add(this.labelSquare);
            this.tabPageSampler.Controls.Add(this.labelWidth);
            this.tabPageSampler.Controls.Add(this.labelSieve);
            this.tabPageSampler.Controls.Add(this.labelPortions);
            this.tabPageSampler.Controls.Add(this.labelArea);
            this.tabPageSampler.Controls.Add(this.numericExposure);
            this.tabPageSampler.Controls.Add(this.numericSquare);
            this.tabPageSampler.Controls.Add(this.numericWidth);
            this.tabPageSampler.Controls.Add(this.numericSieve);
            this.tabPageSampler.Controls.Add(this.numericArea);
            this.tabPageSampler.Controls.SetChildIndex(this.numericArea, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericSieve, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericWidth, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelArea, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelPortions, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSieve, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelWidth, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericPortions, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonEquipment, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label17, 0);
            // 
            // labelPosition
            // 
            this.labelPosition.Location = new System.Drawing.Point(28, 182);
            this.labelPosition.TabIndex = 11;
            // 
            // textBoxComments
            // 
            this.textBoxComments.Location = new System.Drawing.Point(122, 337);
            this.textBoxComments.Size = new System.Drawing.Size(362, 147);
            this.textBoxComments.TabIndex = 14;
            // 
            // comboBoxSampler
            // 
            this.comboBoxSampler.TabIndex = 3;
            // 
            // buttonEquipment
            // 
            this.buttonEquipment.TabIndex = 2;
            // 
            // Logger
            // 
            this.Logger.IndividualsRequired += new System.EventHandler(this.logger_IndividualsRequired);
            // 
            // labelBank
            // 
            this.labelBank.AutoSize = true;
            this.labelBank.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelBank.Location = new System.Drawing.Point(270, 117);
            this.labelBank.Name = "labelBank";
            this.labelBank.Size = new System.Drawing.Size(32, 13);
            this.labelBank.TabIndex = 7;
            this.labelBank.Text = "Bank";
            // 
            // labelCrossSection
            // 
            this.labelCrossSection.AutoSize = true;
            this.labelCrossSection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCrossSection.Location = new System.Drawing.Point(45, 117);
            this.labelCrossSection.Name = "labelCrossSection";
            this.labelCrossSection.Size = new System.Drawing.Size(43, 13);
            this.labelCrossSection.TabIndex = 5;
            this.labelCrossSection.Text = "Section";
            // 
            // comboBoxCrossSection
            // 
            this.comboBoxCrossSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrossSection.FormattingEnabled = true;
            this.comboBoxCrossSection.Location = new System.Drawing.Point(122, 114);
            this.comboBoxCrossSection.Name = "comboBoxCrossSection";
            this.comboBoxCrossSection.Size = new System.Drawing.Size(142, 21);
            this.comboBoxCrossSection.TabIndex = 6;
            this.comboBoxCrossSection.SelectedIndexChanged += new System.EventHandler(this.comboBoxCrossSection_SelectedIndexChanged);
            // 
            // comboBoxBank
            // 
            this.comboBoxBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBank.Enabled = false;
            this.comboBoxBank.FormattingEnabled = true;
            this.comboBoxBank.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBoxBank.Location = new System.Drawing.Point(348, 114);
            this.comboBoxBank.Name = "comboBoxBank";
            this.comboBoxBank.Size = new System.Drawing.Size(136, 21);
            this.comboBoxBank.TabIndex = 8;
            // 
            // numericPortions
            // 
            this.numericPortions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericPortions.Format = "N0";
            this.numericPortions.Location = new System.Drawing.Point(434, 206);
            this.numericPortions.Maximum = 100D;
            this.numericPortions.Minimum = 0D;
            this.numericPortions.Name = "numericPortions";
            this.numericPortions.Size = new System.Drawing.Size(50, 20);
            this.numericPortions.TabIndex = 14;
            this.numericPortions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericPortions.Value = -1D;
            this.numericPortions.ValueChanged += new System.EventHandler(this.effort_Changed);
            // 
            // numericArea
            // 
            this.numericArea.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericArea.Format = "N0";
            this.numericArea.Location = new System.Drawing.Point(214, 232);
            this.numericArea.Maximum = 100D;
            this.numericArea.Minimum = 0D;
            this.numericArea.Name = "numericArea";
            this.numericArea.ReadOnly = true;
            this.numericArea.Size = new System.Drawing.Size(50, 20);
            this.numericArea.TabIndex = 16;
            this.numericArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericArea.Value = -1D;
            // 
            // numericSieve
            // 
            this.numericSieve.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericSieve.Format = "N0";
            this.numericSieve.Location = new System.Drawing.Point(214, 115);
            this.numericSieve.Maximum = 5000D;
            this.numericSieve.Minimum = 0D;
            this.numericSieve.Name = "numericSieve";
            this.numericSieve.Size = new System.Drawing.Size(50, 20);
            this.numericSieve.TabIndex = 9;
            this.numericSieve.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSieve.Value = -1D;
            this.numericSieve.ValueChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelArea
            // 
            this.labelArea.AutoSize = true;
            this.labelArea.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelArea.Location = new System.Drawing.Point(45, 235);
            this.labelArea.Name = "labelArea";
            this.labelArea.Size = new System.Drawing.Size(46, 13);
            this.labelArea.TabIndex = 15;
            this.labelArea.Text = "Area, m²";
            // 
            // labelPortions
            // 
            this.labelPortions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPortions.AutoSize = true;
            this.labelPortions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPortions.Location = new System.Drawing.Point(270, 208);
            this.labelPortions.Name = "labelPortions";
            this.labelPortions.Size = new System.Drawing.Size(45, 13);
            this.labelPortions.TabIndex = 13;
            this.labelPortions.Text = "Portions";
            // 
            // labelSieve
            // 
            this.labelSieve.AutoSize = true;
            this.labelSieve.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSieve.Location = new System.Drawing.Point(45, 118);
            this.labelSieve.Name = "labelSieve";
            this.labelSieve.Size = new System.Drawing.Size(95, 13);
            this.labelSieve.TabIndex = 8;
            this.labelSieve.Text = "Sieve opening, µm";
            // 
            // contextMenuStripSubstrate
            // 
            this.contextMenuStripSubstrate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSands,
            this.ToolStripMenuItemSilts,
            this.ToolStripMenuItemClays,
            this.ToolStripMenuItemLoams});
            this.contextMenuStripSubstrate.Name = "contextMenuStripSubstrate";
            this.contextMenuStripSubstrate.Size = new System.Drawing.Size(110, 92);
            // 
            // ToolStripMenuItemSands
            // 
            this.ToolStripMenuItemSands.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSubSand,
            this.ItemSubLoamySand});
            this.ToolStripMenuItemSands.Name = "ToolStripMenuItemSands";
            this.ToolStripMenuItemSands.Size = new System.Drawing.Size(109, 22);
            this.ToolStripMenuItemSands.Text = "Sands";
            // 
            // ItemSubSand
            // 
            this.ItemSubSand.Name = "ItemSubSand";
            this.ItemSubSand.Size = new System.Drawing.Size(138, 22);
            this.ItemSubSand.Tag = "";
            this.ItemSubSand.Text = "Sand";
            this.ItemSubSand.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubLoamySand
            // 
            this.ItemSubLoamySand.Name = "ItemSubLoamySand";
            this.ItemSubLoamySand.Size = new System.Drawing.Size(138, 22);
            this.ItemSubLoamySand.Tag = "";
            this.ItemSubLoamySand.Text = "Loamy sand";
            this.ItemSubLoamySand.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ToolStripMenuItemSilts
            // 
            this.ToolStripMenuItemSilts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSubSilt});
            this.ToolStripMenuItemSilts.Name = "ToolStripMenuItemSilts";
            this.ToolStripMenuItemSilts.Size = new System.Drawing.Size(109, 22);
            this.ToolStripMenuItemSilts.Text = "Silts";
            // 
            // ItemSubSilt
            // 
            this.ItemSubSilt.Name = "ItemSubSilt";
            this.ItemSubSilt.Size = new System.Drawing.Size(90, 22);
            this.ItemSubSilt.Tag = "";
            this.ItemSubSilt.Text = "Silt";
            this.ItemSubSilt.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ToolStripMenuItemClays
            // 
            this.ToolStripMenuItemClays.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSubClay,
            this.ItemSubSandyClay,
            this.ItemSubSiltyClay});
            this.ToolStripMenuItemClays.Name = "ToolStripMenuItemClays";
            this.ToolStripMenuItemClays.Size = new System.Drawing.Size(109, 22);
            this.ToolStripMenuItemClays.Text = "Clays";
            // 
            // ItemSubClay
            // 
            this.ItemSubClay.Name = "ItemSubClay";
            this.ItemSubClay.Size = new System.Drawing.Size(130, 22);
            this.ItemSubClay.Tag = "";
            this.ItemSubClay.Text = "Clay";
            this.ItemSubClay.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubSandyClay
            // 
            this.ItemSubSandyClay.Name = "ItemSubSandyClay";
            this.ItemSubSandyClay.Size = new System.Drawing.Size(130, 22);
            this.ItemSubSandyClay.Text = "Sandy clay";
            this.ItemSubSandyClay.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubSiltyClay
            // 
            this.ItemSubSiltyClay.Name = "ItemSubSiltyClay";
            this.ItemSubSiltyClay.Size = new System.Drawing.Size(130, 22);
            this.ItemSubSiltyClay.Text = "Silty clay";
            this.ItemSubSiltyClay.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ToolStripMenuItemLoams
            // 
            this.ToolStripMenuItemLoams.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSubLoam,
            this.ItemSubSandyLoam,
            this.ItemSubSandyClayLoam,
            this.ItemSubClayLoam,
            this.ItemSubSiltyClayLoam,
            this.ItemSubSiltLoam});
            this.ToolStripMenuItemLoams.Name = "ToolStripMenuItemLoams";
            this.ToolStripMenuItemLoams.Size = new System.Drawing.Size(109, 22);
            this.ToolStripMenuItemLoams.Text = "Loams";
            // 
            // ItemSubLoam
            // 
            this.ItemSubLoam.Name = "ItemSubLoam";
            this.ItemSubLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubLoam.Text = "Loam";
            this.ItemSubLoam.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubSandyLoam
            // 
            this.ItemSubSandyLoam.Name = "ItemSubSandyLoam";
            this.ItemSubSandyLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSandyLoam.Text = "Sandy loam";
            this.ItemSubSandyLoam.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubSandyClayLoam
            // 
            this.ItemSubSandyClayLoam.Name = "ItemSubSandyClayLoam";
            this.ItemSubSandyClayLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSandyClayLoam.Text = "Sandy clay loam";
            this.ItemSubSandyClayLoam.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubClayLoam
            // 
            this.ItemSubClayLoam.Name = "ItemSubClayLoam";
            this.ItemSubClayLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubClayLoam.Text = "Clay loam";
            this.ItemSubClayLoam.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubSiltyClayLoam
            // 
            this.ItemSubSiltyClayLoam.Name = "ItemSubSiltyClayLoam";
            this.ItemSubSiltyClayLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSiltyClayLoam.Text = "Silty clay loam";
            this.ItemSubSiltyClayLoam.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // ItemSubSiltLoam
            // 
            this.ItemSubSiltLoam.Name = "ItemSubSiltLoam";
            this.ItemSubSiltLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSiltLoam.Text = "Silt loam";
            this.ItemSubSiltLoam.Click += new System.EventHandler(this.itemTexture_Click);
            // 
            // tabPageSubstrate
            // 
            this.tabPageSubstrate.Controls.Add(this.labelTexture);
            this.tabPageSubstrate.Controls.Add(this.buttonSubTexture);
            this.tabPageSubstrate.Controls.Add(this.numericDebris);
            this.tabPageSubstrate.Controls.Add(this.checkBoxClay);
            this.tabPageSubstrate.Controls.Add(this.numericSapropel);
            this.tabPageSubstrate.Controls.Add(this.numericCobble);
            this.tabPageSubstrate.Controls.Add(this.checkBoxSilt);
            this.tabPageSubstrate.Controls.Add(this.numericCPOM);
            this.tabPageSubstrate.Controls.Add(this.checkBoxPhytal);
            this.tabPageSubstrate.Controls.Add(this.numericFPOM);
            this.tabPageSubstrate.Controls.Add(this.checkBoxSand);
            this.tabPageSubstrate.Controls.Add(this.numericGravel);
            this.tabPageSubstrate.Controls.Add(this.numericLiving);
            this.tabPageSubstrate.Controls.Add(this.numericBoulder);
            this.tabPageSubstrate.Controls.Add(this.checkBoxWood);
            this.tabPageSubstrate.Controls.Add(this.numericWood);
            this.tabPageSubstrate.Controls.Add(this.checkBoxGravel);
            this.tabPageSubstrate.Controls.Add(this.numericSand);
            this.tabPageSubstrate.Controls.Add(this.checkBoxDebris);
            this.tabPageSubstrate.Controls.Add(this.checkBoxCobble);
            this.tabPageSubstrate.Controls.Add(this.checkBoxLiving);
            this.tabPageSubstrate.Controls.Add(this.numericPhytal);
            this.tabPageSubstrate.Controls.Add(this.checkBoxBoulder);
            this.tabPageSubstrate.Controls.Add(this.numericSilt);
            this.tabPageSubstrate.Controls.Add(this.checkBoxSapropel);
            this.tabPageSubstrate.Controls.Add(this.labelOrganics);
            this.tabPageSubstrate.Controls.Add(this.checkBoxFPOM);
            this.tabPageSubstrate.Controls.Add(this.checkBoxCPOM);
            this.tabPageSubstrate.Controls.Add(this.labelMinerals);
            this.tabPageSubstrate.Controls.Add(this.numericClay);
            this.tabPageSubstrate.Location = new System.Drawing.Point(4, 22);
            this.tabPageSubstrate.Name = "tabPageSubstrate";
            this.tabPageSubstrate.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.tabPageSubstrate.Size = new System.Drawing.Size(532, 532);
            this.tabPageSubstrate.TabIndex = 12;
            this.tabPageSubstrate.Text = "Substrate";
            this.tabPageSubstrate.UseVisualStyleBackColor = true;
            // 
            // labelTexture
            // 
            this.labelTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTexture.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelTexture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTexture.Location = new System.Drawing.Point(185, 143);
            this.labelTexture.Name = "labelTexture";
            this.labelTexture.Size = new System.Drawing.Size(300, 15);
            this.labelTexture.TabIndex = 55;
            this.labelTexture.Text = "Change substrate mineral composition";
            this.labelTexture.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // buttonSubTexture
            // 
            this.buttonSubTexture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSubTexture.Location = new System.Drawing.Point(48, 139);
            this.buttonSubTexture.Margin = new System.Windows.Forms.Padding(3, 25, 3, 3);
            this.buttonSubTexture.Name = "buttonSubTexture";
            this.buttonSubTexture.Size = new System.Drawing.Size(100, 23);
            this.buttonSubTexture.TabIndex = 54;
            this.buttonSubTexture.Text = "Select texture";
            this.buttonSubTexture.UseVisualStyleBackColor = true;
            this.buttonSubTexture.Click += new System.EventHandler(this.buttonSubTexture_Click);
            // 
            // numericDebris
            // 
            this.numericDebris.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericDebris.Format = "N0";
            this.numericDebris.Location = new System.Drawing.Point(433, 284);
            this.numericDebris.Maximum = 100D;
            this.numericDebris.Minimum = 0D;
            this.numericDebris.Name = "numericDebris";
            this.numericDebris.Size = new System.Drawing.Size(50, 20);
            this.numericDebris.TabIndex = 70;
            this.numericDebris.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericDebris.Value = -1D;
            this.numericDebris.Visible = false;
            // 
            // checkBoxClay
            // 
            this.checkBoxClay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxClay.AutoSize = true;
            this.checkBoxClay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxClay.Location = new System.Drawing.Point(348, 94);
            this.checkBoxClay.Name = "checkBoxClay";
            this.checkBoxClay.Size = new System.Drawing.Size(46, 17);
            this.checkBoxClay.TabIndex = 52;
            this.checkBoxClay.Text = "Clay";
            this.checkBoxClay.UseVisualStyleBackColor = true;
            this.checkBoxClay.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericSapropel
            // 
            this.numericSapropel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSapropel.Format = "N0";
            this.numericSapropel.Location = new System.Drawing.Point(433, 258);
            this.numericSapropel.Maximum = 100D;
            this.numericSapropel.Minimum = 0D;
            this.numericSapropel.Name = "numericSapropel";
            this.numericSapropel.Size = new System.Drawing.Size(50, 20);
            this.numericSapropel.TabIndex = 68;
            this.numericSapropel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSapropel.Value = -1D;
            this.numericSapropel.Visible = false;
            // 
            // numericCobble
            // 
            this.numericCobble.Format = "N0";
            this.numericCobble.Location = new System.Drawing.Point(135, 93);
            this.numericCobble.Maximum = 100D;
            this.numericCobble.Minimum = 0D;
            this.numericCobble.Name = "numericCobble";
            this.numericCobble.Size = new System.Drawing.Size(50, 20);
            this.numericCobble.TabIndex = 45;
            this.numericCobble.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericCobble.Value = -1D;
            this.numericCobble.Visible = false;
            this.numericCobble.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxSilt
            // 
            this.checkBoxSilt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSilt.AutoSize = true;
            this.checkBoxSilt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSilt.Location = new System.Drawing.Point(348, 68);
            this.checkBoxSilt.Name = "checkBoxSilt";
            this.checkBoxSilt.Size = new System.Drawing.Size(40, 17);
            this.checkBoxSilt.TabIndex = 50;
            this.checkBoxSilt.Text = "Silt";
            this.checkBoxSilt.UseVisualStyleBackColor = true;
            this.checkBoxSilt.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericCPOM
            // 
            this.numericCPOM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericCPOM.Format = "N0";
            this.numericCPOM.Location = new System.Drawing.Point(214, 310);
            this.numericCPOM.Maximum = 100D;
            this.numericCPOM.Minimum = 0D;
            this.numericCPOM.Name = "numericCPOM";
            this.numericCPOM.Size = new System.Drawing.Size(50, 20);
            this.numericCPOM.TabIndex = 64;
            this.numericCPOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericCPOM.Value = -1D;
            this.numericCPOM.Visible = false;
            // 
            // checkBoxPhytal
            // 
            this.checkBoxPhytal.AutoSize = true;
            this.checkBoxPhytal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxPhytal.Location = new System.Drawing.Point(48, 233);
            this.checkBoxPhytal.Name = "checkBoxPhytal";
            this.checkBoxPhytal.Size = new System.Drawing.Size(145, 17);
            this.checkBoxPhytal.TabIndex = 57;
            this.checkBoxPhytal.Text = "Phytal (macrophytes etc.)";
            this.checkBoxPhytal.UseVisualStyleBackColor = true;
            this.checkBoxPhytal.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericFPOM
            // 
            this.numericFPOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericFPOM.Format = "N0";
            this.numericFPOM.Location = new System.Drawing.Point(433, 232);
            this.numericFPOM.Maximum = 100D;
            this.numericFPOM.Minimum = 0D;
            this.numericFPOM.Name = "numericFPOM";
            this.numericFPOM.Size = new System.Drawing.Size(50, 20);
            this.numericFPOM.TabIndex = 66;
            this.numericFPOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericFPOM.Value = -1D;
            this.numericFPOM.Visible = false;
            // 
            // checkBoxSand
            // 
            this.checkBoxSand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxSand.AutoSize = true;
            this.checkBoxSand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSand.Location = new System.Drawing.Point(198, 94);
            this.checkBoxSand.Name = "checkBoxSand";
            this.checkBoxSand.Size = new System.Drawing.Size(51, 17);
            this.checkBoxSand.TabIndex = 48;
            this.checkBoxSand.Text = "Sand";
            this.checkBoxSand.UseVisualStyleBackColor = true;
            this.checkBoxSand.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericGravel
            // 
            this.numericGravel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericGravel.Format = "N0";
            this.numericGravel.Location = new System.Drawing.Point(285, 67);
            this.numericGravel.Maximum = 100D;
            this.numericGravel.Minimum = 0D;
            this.numericGravel.Name = "numericGravel";
            this.numericGravel.Size = new System.Drawing.Size(50, 20);
            this.numericGravel.TabIndex = 47;
            this.numericGravel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericGravel.Value = -1D;
            this.numericGravel.Visible = false;
            this.numericGravel.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // numericLiving
            // 
            this.numericLiving.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericLiving.Format = "N0";
            this.numericLiving.Location = new System.Drawing.Point(214, 258);
            this.numericLiving.Maximum = 100D;
            this.numericLiving.Minimum = 0D;
            this.numericLiving.Name = "numericLiving";
            this.numericLiving.Size = new System.Drawing.Size(50, 20);
            this.numericLiving.TabIndex = 60;
            this.numericLiving.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericLiving.Value = -1D;
            this.numericLiving.Visible = false;
            // 
            // numericBoulder
            // 
            this.numericBoulder.Format = "N0";
            this.numericBoulder.Location = new System.Drawing.Point(135, 67);
            this.numericBoulder.Maximum = 100D;
            this.numericBoulder.Minimum = 0D;
            this.numericBoulder.Name = "numericBoulder";
            this.numericBoulder.Size = new System.Drawing.Size(50, 20);
            this.numericBoulder.TabIndex = 43;
            this.numericBoulder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericBoulder.Value = -1D;
            this.numericBoulder.Visible = false;
            this.numericBoulder.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxWood
            // 
            this.checkBoxWood.AutoSize = true;
            this.checkBoxWood.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxWood.Location = new System.Drawing.Point(48, 285);
            this.checkBoxWood.Name = "checkBoxWood";
            this.checkBoxWood.Size = new System.Drawing.Size(110, 17);
            this.checkBoxWood.TabIndex = 61;
            this.checkBoxWood.Text = "Xylal (dead wood)";
            this.checkBoxWood.UseVisualStyleBackColor = true;
            this.checkBoxWood.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericWood
            // 
            this.numericWood.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericWood.Format = "N0";
            this.numericWood.Location = new System.Drawing.Point(214, 284);
            this.numericWood.Maximum = 100D;
            this.numericWood.Minimum = 0D;
            this.numericWood.Name = "numericWood";
            this.numericWood.Size = new System.Drawing.Size(50, 20);
            this.numericWood.TabIndex = 62;
            this.numericWood.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericWood.Value = -1D;
            this.numericWood.Visible = false;
            // 
            // checkBoxGravel
            // 
            this.checkBoxGravel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxGravel.AutoSize = true;
            this.checkBoxGravel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxGravel.Location = new System.Drawing.Point(198, 68);
            this.checkBoxGravel.Name = "checkBoxGravel";
            this.checkBoxGravel.Size = new System.Drawing.Size(57, 17);
            this.checkBoxGravel.TabIndex = 46;
            this.checkBoxGravel.Text = "Gravel";
            this.checkBoxGravel.UseVisualStyleBackColor = true;
            this.checkBoxGravel.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericSand
            // 
            this.numericSand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericSand.Format = "N0";
            this.numericSand.Location = new System.Drawing.Point(285, 93);
            this.numericSand.Maximum = 100D;
            this.numericSand.Minimum = 0D;
            this.numericSand.Name = "numericSand";
            this.numericSand.Size = new System.Drawing.Size(50, 20);
            this.numericSand.TabIndex = 49;
            this.numericSand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSand.Value = -1D;
            this.numericSand.Visible = false;
            this.numericSand.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxDebris
            // 
            this.checkBoxDebris.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxDebris.AutoSize = true;
            this.checkBoxDebris.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxDebris.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDebris.Location = new System.Drawing.Point(273, 285);
            this.checkBoxDebris.Name = "checkBoxDebris";
            this.checkBoxDebris.Size = new System.Drawing.Size(56, 17);
            this.checkBoxDebris.TabIndex = 69;
            this.checkBoxDebris.Text = "Debris";
            this.checkBoxDebris.UseVisualStyleBackColor = true;
            this.checkBoxDebris.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxCobble
            // 
            this.checkBoxCobble.AutoSize = true;
            this.checkBoxCobble.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxCobble.Location = new System.Drawing.Point(48, 94);
            this.checkBoxCobble.Name = "checkBoxCobble";
            this.checkBoxCobble.Size = new System.Drawing.Size(64, 17);
            this.checkBoxCobble.TabIndex = 44;
            this.checkBoxCobble.Text = "Cobbles";
            this.checkBoxCobble.UseVisualStyleBackColor = true;
            this.checkBoxCobble.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxLiving
            // 
            this.checkBoxLiving.AutoSize = true;
            this.checkBoxLiving.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxLiving.Location = new System.Drawing.Point(48, 259);
            this.checkBoxLiving.Name = "checkBoxLiving";
            this.checkBoxLiving.Size = new System.Drawing.Size(103, 17);
            this.checkBoxLiving.TabIndex = 59;
            this.checkBoxLiving.Text = "Terrestrial plants";
            this.checkBoxLiving.UseVisualStyleBackColor = true;
            this.checkBoxLiving.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericPhytal
            // 
            this.numericPhytal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericPhytal.Format = "N0";
            this.numericPhytal.Location = new System.Drawing.Point(214, 232);
            this.numericPhytal.Maximum = 100D;
            this.numericPhytal.Minimum = 0D;
            this.numericPhytal.Name = "numericPhytal";
            this.numericPhytal.Size = new System.Drawing.Size(50, 20);
            this.numericPhytal.TabIndex = 58;
            this.numericPhytal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericPhytal.Value = -1D;
            this.numericPhytal.Visible = false;
            // 
            // checkBoxBoulder
            // 
            this.checkBoxBoulder.AutoSize = true;
            this.checkBoxBoulder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxBoulder.Location = new System.Drawing.Point(48, 68);
            this.checkBoxBoulder.Name = "checkBoxBoulder";
            this.checkBoxBoulder.Size = new System.Drawing.Size(67, 17);
            this.checkBoxBoulder.TabIndex = 42;
            this.checkBoxBoulder.Text = "Boulders";
            this.checkBoxBoulder.UseVisualStyleBackColor = true;
            this.checkBoxBoulder.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericSilt
            // 
            this.numericSilt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSilt.Format = "N0";
            this.numericSilt.Location = new System.Drawing.Point(433, 67);
            this.numericSilt.Maximum = 100D;
            this.numericSilt.Minimum = 0D;
            this.numericSilt.Name = "numericSilt";
            this.numericSilt.Size = new System.Drawing.Size(50, 20);
            this.numericSilt.TabIndex = 51;
            this.numericSilt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSilt.Value = -1D;
            this.numericSilt.Visible = false;
            this.numericSilt.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxSapropel
            // 
            this.checkBoxSapropel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxSapropel.AutoSize = true;
            this.checkBoxSapropel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSapropel.Location = new System.Drawing.Point(273, 259);
            this.checkBoxSapropel.Name = "checkBoxSapropel";
            this.checkBoxSapropel.Size = new System.Drawing.Size(132, 17);
            this.checkBoxSapropel.TabIndex = 67;
            this.checkBoxSapropel.Text = "Organic mud, sapropel";
            this.checkBoxSapropel.UseVisualStyleBackColor = true;
            this.checkBoxSapropel.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // labelOrganics
            // 
            this.labelOrganics.AutoSize = true;
            this.labelOrganics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelOrganics.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelOrganics.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelOrganics.Location = new System.Drawing.Point(28, 190);
            this.labelOrganics.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelOrganics.Name = "labelOrganics";
            this.labelOrganics.Size = new System.Drawing.Size(113, 15);
            this.labelOrganics.TabIndex = 56;
            this.labelOrganics.Text = "Biotic Microhabitats";
            // 
            // checkBoxFPOM
            // 
            this.checkBoxFPOM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxFPOM.AutoSize = true;
            this.checkBoxFPOM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxFPOM.Location = new System.Drawing.Point(273, 233);
            this.checkBoxFPOM.Name = "checkBoxFPOM";
            this.checkBoxFPOM.Size = new System.Drawing.Size(56, 17);
            this.checkBoxFPOM.TabIndex = 65;
            this.checkBoxFPOM.Text = "FPOM";
            this.checkBoxFPOM.UseVisualStyleBackColor = true;
            this.checkBoxFPOM.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxCPOM
            // 
            this.checkBoxCPOM.AutoSize = true;
            this.checkBoxCPOM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxCPOM.Location = new System.Drawing.Point(48, 311);
            this.checkBoxCPOM.Name = "checkBoxCPOM";
            this.checkBoxCPOM.Size = new System.Drawing.Size(149, 17);
            this.checkBoxCPOM.TabIndex = 63;
            this.checkBoxCPOM.Text = "CPOM (e. g. fallen leaves)";
            this.checkBoxCPOM.UseVisualStyleBackColor = true;
            this.checkBoxCPOM.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // labelMinerals
            // 
            this.labelMinerals.AutoSize = true;
            this.labelMinerals.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelMinerals.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelMinerals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMinerals.Location = new System.Drawing.Point(28, 25);
            this.labelMinerals.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelMinerals.Name = "labelMinerals";
            this.labelMinerals.Size = new System.Drawing.Size(119, 15);
            this.labelMinerals.TabIndex = 41;
            this.labelMinerals.Text = "Mineral Composition";
            // 
            // numericClay
            // 
            this.numericClay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericClay.Format = "N0";
            this.numericClay.Location = new System.Drawing.Point(433, 93);
            this.numericClay.Maximum = 100D;
            this.numericClay.Minimum = 0D;
            this.numericClay.Name = "numericClay";
            this.numericClay.Size = new System.Drawing.Size(50, 20);
            this.numericClay.TabIndex = 53;
            this.numericClay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericClay.Value = -1D;
            this.numericClay.Visible = false;
            this.numericClay.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label17.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(28, 168);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 15);
            this.label17.TabIndex = 10;
            this.label17.Text = "Sampled area";
            // 
            // numericDepth
            // 
            this.numericDepth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericDepth.Format = "N0";
            this.numericDepth.Location = new System.Drawing.Point(214, 141);
            this.numericDepth.Maximum = 100D;
            this.numericDepth.Minimum = 0D;
            this.numericDepth.Name = "numericDepth";
            this.numericDepth.Size = new System.Drawing.Size(50, 20);
            this.numericDepth.TabIndex = 10;
            this.numericDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericDepth.Value = -1D;
            // 
            // labelDepth
            // 
            this.labelDepth.AutoSize = true;
            this.labelDepth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDepth.Location = new System.Drawing.Point(45, 144);
            this.labelDepth.Name = "labelDepth";
            this.labelDepth.Size = new System.Drawing.Size(50, 13);
            this.labelDepth.TabIndex = 9;
            this.labelDepth.Text = "Depth, m";
            // 
            // numericWidth
            // 
            this.numericWidth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericWidth.Format = "N0";
            this.numericWidth.Location = new System.Drawing.Point(214, 89);
            this.numericWidth.Maximum = 100D;
            this.numericWidth.Minimum = 0D;
            this.numericWidth.Name = "numericWidth";
            this.numericWidth.Size = new System.Drawing.Size(50, 20);
            this.numericWidth.TabIndex = 5;
            this.numericWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericWidth.Value = -1D;
            this.numericWidth.ValueChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelWidth.Location = new System.Drawing.Point(45, 92);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(86, 13);
            this.labelWidth.TabIndex = 4;
            this.labelWidth.Text = "Blade length, cm";
            // 
            // numericExposure
            // 
            this.numericExposure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericExposure.Format = "N0";
            this.numericExposure.Location = new System.Drawing.Point(214, 206);
            this.numericExposure.Maximum = 100D;
            this.numericExposure.Minimum = 0D;
            this.numericExposure.Name = "numericExposure";
            this.numericExposure.Size = new System.Drawing.Size(50, 20);
            this.numericExposure.TabIndex = 12;
            this.numericExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericExposure.Value = -1D;
            this.numericExposure.ValueChanged += new System.EventHandler(this.effort_Changed);
            // 
            // labelExposure
            // 
            this.labelExposure.AutoSize = true;
            this.labelExposure.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelExposure.Location = new System.Drawing.Point(45, 209);
            this.labelExposure.Name = "labelExposure";
            this.labelExposure.Size = new System.Drawing.Size(71, 13);
            this.labelExposure.TabIndex = 11;
            this.labelExposure.Text = "Exposure, cm";
            // 
            // numericSquare
            // 
            this.numericSquare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericSquare.Format = "N0";
            this.numericSquare.Location = new System.Drawing.Point(434, 89);
            this.numericSquare.Maximum = 10000D;
            this.numericSquare.Minimum = 0D;
            this.numericSquare.Name = "numericSquare";
            this.numericSquare.Size = new System.Drawing.Size(50, 20);
            this.numericSquare.TabIndex = 7;
            this.numericSquare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSquare.Value = -1D;
            this.numericSquare.ValueChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelSquare
            // 
            this.labelSquare.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSquare.AutoSize = true;
            this.labelSquare.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSquare.Location = new System.Drawing.Point(270, 92);
            this.labelSquare.Name = "labelSquare";
            this.labelSquare.Size = new System.Drawing.Size(103, 13);
            this.labelSquare.TabIndex = 6;
            this.labelSquare.Text = "Grabber square, cm²";
            // 
            // BenthosCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(564, 631);
            this.Name = "BenthosCard";
            this.OnSaved += new System.EventHandler(this.benthosCard_OnSaved);
            this.OnCleared += new System.EventHandler(this.benthosCard_OnCleared);
            this.OnEquipmentSaved += new Mayfly.Wild.EquipmentEventHandler(this.benthosCard_OnEquipmentSaved);
            this.Controls.SetChildIndex(this.tabControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            this.contextMenuStripSubstrate.ResumeLayout(false);
            this.tabPageSubstrate.ResumeLayout(false);
            this.tabPageSubstrate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void resetMinerals() {

            checkBoxBoulder.Checked = (numericBoulder.Value > 0);
            checkBoxCobble.Checked = (numericCobble.Value > 0);
            checkBoxGravel.Checked = (numericGravel.Value > 0);
            checkBoxSand.Checked = (numericSand.Value > 0);
            checkBoxSilt.Checked = (numericSilt.Value > 0);
            checkBoxClay.Checked = (numericClay.Value > 0);
        }

        private void clearMinerals() {

            foreach (NumberBox numeric in MineralControls) {
                numeric.Clear();
            }

            resetMinerals();
        }

        private void resetOrganics() {

            checkBoxPhytal.Checked = (numericPhytal.Value > 0);
            checkBoxLiving.Checked = (numericLiving.Value > 0);
            checkBoxWood.Checked = (numericWood.Value > 0);
            checkBoxCPOM.Checked = (numericCPOM.Value > 0);
            checkBoxFPOM.Checked = (numericFPOM.Value > 0);
            checkBoxSapropel.Checked = (numericSapropel.Value > 0);
            checkBoxDebris.Checked = (numericDebris.Value > 0);
        }

        private void clearOrganics() {

            foreach (NumberBox numeric in new NumberBox[] { numericPhytal, numericLiving, numericWood, numericCPOM, numericFPOM, numericSapropel, numericDebris }) {
                numeric.Clear();
            }

            resetOrganics();
        }

        private void clearSubstrate() {
            clearMinerals();
            clearOrganics();
        }

        private void clearSampler() {

            numericWidth.Clear();
            numericSquare.Clear();
            numericSieve.Clear();
        }

        private void clearEffort() {

            numericExposure.Clear();
            numericPortions.Clear();

        }

        private void saveSubstrate() {

            SubstrateSample substrate = new SubstrateSample(0, 0, 0);

            if (checkBoxBoulder.Checked) {
                substrate.Boulder = numericBoulder.Value;
            }

            if (checkBoxCobble.Checked) {
                substrate.Cobble = numericCobble.Value;
            }

            if (checkBoxGravel.Checked) {
                substrate.Gravel = numericGravel.Value;
            }

            if (checkBoxSand.Checked) {
                substrate.Sand = numericSand.Value;
            }

            if (checkBoxSilt.Checked) {
                substrate.Silt = numericSilt.Value;
            }

            if (checkBoxClay.Checked) {
                substrate.Clay = numericClay.Value;
            }



            if (checkBoxPhytal.Checked) {
                substrate.Phytal = numericPhytal.Value;
            }

            if (checkBoxLiving.Checked) {
                substrate.Living = numericLiving.Value;
            }

            if (checkBoxWood.Checked) {
                substrate.Wood = numericWood.Value;
            }

            if (checkBoxCPOM.Checked) {
                substrate.CPOM = numericCPOM.Value;
            }

            if (checkBoxFPOM.Checked) {
                substrate.FPOM = numericFPOM.Value;
            }

            if (checkBoxSapropel.Checked) {
                substrate.Sapropel = numericSapropel.Value;
            }

            if (checkBoxDebris.Checked) {
                substrate.Debris = numericDebris.Value;
            }


            if (substrate.IsAvailable) {
                data.Solitary.Substrate = substrate.Protocol;
            } else {
                data.Solitary.SetSubstrateNull();
            }
        }

        //private void saveSampler() {

        //    base.saveSampler();
        //    data.Solitary.EquipmentRow.SetVirtue("Width", numericWidth.Value);
        //    data.Solitary.EquipmentRow.SetVirtue("Square", numericSquare.Value);
        //}

        private void saveEffort() {

            if (numericDepth.IsSet) {
                data.Solitary.Depth = numericDepth.Value;
            } else {
                data.Solitary.SetDepthNull();
            }

            if (SelectedSampler.EffortType == (int)EffortType.Portion) {
                if (numericPortions.IsSet) {
                    data.Solitary.Effort = numericPortions.Value;
                } else {
                    data.Solitary.SetEffortNull();
                }
            } else if (SelectedSampler.EffortType == (int)EffortType.Exposure) {
                if (numericExposure.IsSet) {
                    data.Solitary.Effort = numericExposure.Value * .01;
                } else {
                    data.Solitary.SetEffortNull();
                }
            }
        }



        private void benthosCard_OnCleared(object sender, EventArgs e) {

            clearSubstrate();
            clearSampler();
            clearEffort();
        }

        private void benthosCard_OnEquipmentSaved(object sender, EquipmentEventArgs e) {

            effort_Changed(sender, e);
        }

        private void benthosCard_OnSaved(object sender, EventArgs e) {

            saveSubstrate();
        }

        private void waterSelector_WaterSelected(object sender, WaterEventArgs e) {

            WaterType type = waterSelector.IsWaterSelected ? waterSelector.WaterObject.WaterType : WaterType.None;
            comboBoxCrossSection.Items.Clear();
            comboBoxCrossSection.Items.AddRange(Wild.Service.CrossSection(type));
            comboBoxBank.Enabled = type != WaterType.Lake;
        }


        private void logger_IndividualsRequired(object sender, EventArgs e) {

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                if (gridRow.Cells[ColumnDefinition.Name].Value != null) {

                    Wild.Survey.LogRow logRow = Logger.SaveLogRow(gridRow);
                    Individuals individuals = null;

                    foreach (Form form in Application.OpenForms) {
                        if (!(form is Individuals)) continue;
                        if (((Individuals)form).LogRow == logRow) {
                            individuals = (Individuals)form;
                        }
                    }

                    if (individuals == null) {
                        individuals = new Individuals(logRow);
                        individuals.SetColumns(ColumnDefinition, ColumnQty, ColumnMass);
                        individuals.LogLine = gridRow;
                        individuals.SetFriendlyDesktopLocation(spreadSheetLog);
                        individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
                        individuals.Show(this);
                    } else {
                        individuals.BringToFront();
                    }
                }
            }
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e) {
            Individuals individuals = sender as Individuals;
            if (individuals.DialogResult == DialogResult.OK) {
                isChanged |= individuals.ChangesWereMade;
            }
        }


        private void comboBoxCrossSection_SelectedIndexChanged(object sender, EventArgs e) {

            if (comboBoxCrossSection.SelectedIndex == -1 ||
                comboBoxCrossSection.SelectedIndex == comboBoxCrossSection.Items.Count - 1) {
                labelBank.Enabled = comboBoxBank.Enabled = false;
            } else {
                labelBank.Enabled = comboBoxBank.Enabled = true;
            }

            value_Changed(sender, e);
        }


        private void virtue_Changed(object sender, EventArgs e) {

            if (SelectedSampler == null) return;

            saveSampler();
        }

        private void effort_Changed(object sender, EventArgs e) {

            saveEffort();
            numericArea.Value = data.Solitary.GetArea();
            numericArea.Format = "N3";
            isChanged = true;
        }



        private void substrate_ValueChanged(object sender, EventArgs e) {

            value_Changed(sender, e);

            if (sender is NumberBox down && down.ContainsFocus) {
                int selectedValue = (int)down.Value;

                int rest = 100 - selectedValue;

                int currentRest = 0;

                foreach (NumberBox numeric in MineralControls) {
                    if (numeric == down) continue;
                    currentRest += (int)numeric.Value;
                }

                if (currentRest > rest) {
                    foreach (NumberBox numeric in MineralControls) {
                        if (numeric == down) continue;
                        if (numeric.Value == 0) continue;
                        numeric.Value = (int)(numeric.Value * rest / currentRest);
                    }
                }
            }

            // TODO: get substrate from sand+silt+clay IF checks are checked

            SubstrateSample substrate = new SubstrateSample(numericSand.Value, numericSilt.Value, numericClay.Value);

            labelTexture.Text = substrate.TypeName;
        }

        private void checkBoxGranulae_CheckedChanged(object sender, EventArgs e) {

            Control control = Controls.Find("numeric" +
                ((CheckBox)sender).Name.Substring(8), true)[0];

            if (control is NumberBox) {
                control.Visible = ((CheckBox)sender).Checked;
            }

            substrate_ValueChanged(sender, e);
        }

        private void buttonSubTexture_Click(object sender, EventArgs e) {

            contextMenuStripSubstrate.Show(buttonSubTexture, new Point(buttonSubTexture.Width, 0), ToolStripDropDownDirection.Right);
        }

        private void itemTexture_Click(object sender, EventArgs e) {

            clearMinerals();

            string texture = ((ToolStripMenuItem)sender).Name.Substring(7);
            SubstrateSample substrate = new SubstrateSample(SubstrateSample.FromName(texture));
            numericSand.Value = 100 * substrate.Sand;
            numericSilt.Value = 100 * substrate.Silt;
            numericClay.Value = 100 * substrate.Clay;

            resetMinerals();
        }
    }
}
