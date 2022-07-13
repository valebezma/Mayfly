using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Wild;
using Mayfly.Geographics;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Waters.Controls;
using static Mayfly.Wild.UserSettings;
using static Mayfly.Wild.ReaderSettings;

namespace Mayfly.Benthos
{
    public class BenthosCard : Wild.Card
    {
        private ComboBox comboBoxBank;
        private ComboBox comboBoxCrossSection;
        private Label labelCrossSection;
        private TextBox textBoxDepth;
        private NumericUpDown numericUpDownReps;
        private Label labelDepth;
        private Label labelMesh;
        private Label labelRepeats;
        private Label labelSquare;
        private TextBox textBoxMesh;
        private TextBox textBoxSquare;
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
        private NumericUpDown numericUpDownDebris;
        private CheckBox checkBoxClay;
        private NumericUpDown numericUpDownSapropel;
        private NumericUpDown numericUpDownCobble;
        private CheckBox checkBoxSilt;
        private NumericUpDown numericUpDownCPOM;
        private CheckBox checkBoxPhytal;
        private NumericUpDown numericUpDownFPOM;
        private CheckBox checkBoxSand;
        private NumericUpDown numericUpDownGravel;
        private NumericUpDown numericUpDownLiving;
        private NumericUpDown numericUpDownBoulder;
        private CheckBox checkBoxWood;
        private NumericUpDown numericUpDownWood;
        private CheckBox checkBoxGravel;
        private NumericUpDown numericUpDownSand;
        private CheckBox checkBoxDebris;
        private CheckBox checkBoxCobble;
        private CheckBox checkBoxLiving;
        private NumericUpDown numericUpDownPhytal;
        private CheckBox checkBoxBoulder;
        private NumericUpDown numericUpDownSilt;
        private CheckBox checkBoxSapropel;
        private Label labelOrganics;
        private CheckBox checkBoxFPOM;
        private CheckBox checkBoxCPOM;
        private Label labelMinerals;
        private NumericUpDown numericUpDownClay;
        private Label labelBank;

        private double Square // In square meters
        {
            get {
                if (textBoxSquare.Text.IsDoubleConvertible()) {
                    return Convert.ToDouble(textBoxSquare.Text);
                } else {
                    return double.NaN;
                }
            }

            set {
                if (double.IsNaN(value)) {
                    textBoxSquare.Text = string.Empty;
                } else {
                    textBoxSquare.Text = value.ToString();//Textual.Mask(4));

                    if (SelectedSampler != null && !SelectedSampler.IsEffortValueNull()) {

                        switch (SelectedSampler.GetSamplerType()) {
                            case BenthosSamplerType.Grabber:
                                // Replies count equals to 
                                // square divided by [standard effort] of grabber
                                numericUpDownReps.Value = (decimal)Math.Round(value / SelectedSampler.EffortValue, 0);
                                break;
                            case BenthosSamplerType.Scraper:
                                // Exposure length in centimeters equals to 
                                // square divided by knife length (standard effort) of creeper multiplied by 100
                                numericUpDownReps.Value = 100 * (decimal)Math.Round(value / SelectedSampler.EffortValue, 3);
                                break;
                            default:
                                numericUpDownReps.Value = (decimal)Math.Round(value / SelectedSampler.EffortValue, 0);
                                break;
                        }
                    } else {
                        statusCard.Message(Resources.Interface.Messages.SamplerUnable);
                    }
                }
            }
        }
        private NumericUpDown[] MineralControls => new NumericUpDown[] { numericUpDownBoulder, numericUpDownCobble, numericUpDownGravel, numericUpDownSand, numericUpDownSilt, numericUpDownClay };


        public BenthosCard() : base() {
            InitializeComponent();
            Initiate();
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
            this.numericUpDownReps = new System.Windows.Forms.NumericUpDown();
            this.textBoxSquare = new System.Windows.Forms.TextBox();
            this.textBoxMesh = new System.Windows.Forms.TextBox();
            this.labelSquare = new System.Windows.Forms.Label();
            this.labelRepeats = new System.Windows.Forms.Label();
            this.labelMesh = new System.Windows.Forms.Label();
            this.labelDepth = new System.Windows.Forms.Label();
            this.textBoxDepth = new System.Windows.Forms.TextBox();
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
            this.numericUpDownDebris = new System.Windows.Forms.NumericUpDown();
            this.checkBoxClay = new System.Windows.Forms.CheckBox();
            this.numericUpDownSapropel = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCobble = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSilt = new System.Windows.Forms.CheckBox();
            this.numericUpDownCPOM = new System.Windows.Forms.NumericUpDown();
            this.checkBoxPhytal = new System.Windows.Forms.CheckBox();
            this.numericUpDownFPOM = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSand = new System.Windows.Forms.CheckBox();
            this.numericUpDownGravel = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLiving = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBoulder = new System.Windows.Forms.NumericUpDown();
            this.checkBoxWood = new System.Windows.Forms.CheckBox();
            this.numericUpDownWood = new System.Windows.Forms.NumericUpDown();
            this.checkBoxGravel = new System.Windows.Forms.CheckBox();
            this.numericUpDownSand = new System.Windows.Forms.NumericUpDown();
            this.checkBoxDebris = new System.Windows.Forms.CheckBox();
            this.checkBoxCobble = new System.Windows.Forms.CheckBox();
            this.checkBoxLiving = new System.Windows.Forms.CheckBox();
            this.numericUpDownPhytal = new System.Windows.Forms.NumericUpDown();
            this.checkBoxBoulder = new System.Windows.Forms.CheckBox();
            this.numericUpDownSilt = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSapropel = new System.Windows.Forms.CheckBox();
            this.labelOrganics = new System.Windows.Forms.Label();
            this.checkBoxFPOM = new System.Windows.Forms.CheckBox();
            this.checkBoxCPOM = new System.Windows.Forms.CheckBox();
            this.labelMinerals = new System.Windows.Forms.Label();
            this.numericUpDownClay = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReps)).BeginInit();
            this.contextMenuStripSubstrate.SuspendLayout();
            this.tabPageSubstrate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDebris)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSapropel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCobble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCPOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGravel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBoulder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhytal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSilt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClay)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.comboBoxBank);
            this.tabPageCollect.Controls.Add(this.comboBoxCrossSection);
            this.tabPageCollect.Controls.Add(this.labelCrossSection);
            this.tabPageCollect.Controls.Add(this.labelBank);
            this.tabPageCollect.Controls.SetChildIndex(this.labelComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelBank, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelWater, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelCrossSection, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBoxCrossSection, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waterSelector, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBoxBank, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waypointControl1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelTag, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelPosition, 0);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSubstrate);
            this.tabControl.Controls.SetChildIndex(this.tabPageFactors, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageLog, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageEnvironment, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageSampler, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageSubstrate, 0);
            this.tabControl.Controls.SetChildIndex(this.tabPageCollect, 0);
            // 
            // waterSelector
            // 
            this.waterSelector.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.waterSelector_WaterSelected);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.textBoxDepth);
            this.tabPageSampler.Controls.Add(this.numericUpDownReps);
            this.tabPageSampler.Controls.Add(this.labelDepth);
            this.tabPageSampler.Controls.Add(this.labelMesh);
            this.tabPageSampler.Controls.Add(this.labelRepeats);
            this.tabPageSampler.Controls.Add(this.labelSquare);
            this.tabPageSampler.Controls.Add(this.textBoxMesh);
            this.tabPageSampler.Controls.Add(this.textBoxSquare);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelRepeats, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelDepth, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownReps, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxDepth, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonEquipment, 0);
            // 
            // comboBoxSampler
            // 
            this.comboBoxSampler.TabIndex = 3;
            this.comboBoxSampler.SelectedIndexChanged += new System.EventHandler(this.sampler_Changed);
            // 
            // buttonGear
            // 
            this.buttonEquipment.TabIndex = 2;
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
            // numericUpDownReps
            // 
            this.numericUpDownReps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownReps.Location = new System.Drawing.Point(434, 89);
            this.numericUpDownReps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownReps.Name = "numericUpDownReps";
            this.numericUpDownReps.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownReps.TabIndex = 6;
            this.numericUpDownReps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownReps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownReps.Visible = false;
            this.numericUpDownReps.ValueChanged += new System.EventHandler(this.numericUpDownReps_ValueChanged);
            // 
            // textBoxSquare
            // 
            this.textBoxSquare.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxSquare.Location = new System.Drawing.Point(214, 88);
            this.textBoxSquare.Name = "textBoxSquare";
            this.textBoxSquare.ReadOnly = true;
            this.textBoxSquare.Size = new System.Drawing.Size(50, 20);
            this.textBoxSquare.TabIndex = 5;
            this.textBoxSquare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMesh
            // 
            this.textBoxMesh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxMesh.Location = new System.Drawing.Point(214, 114);
            this.textBoxMesh.Name = "textBoxMesh";
            this.textBoxMesh.Size = new System.Drawing.Size(50, 20);
            this.textBoxMesh.TabIndex = 8;
            this.textBoxMesh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelSquare
            // 
            this.labelSquare.AutoSize = true;
            this.labelSquare.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSquare.Location = new System.Drawing.Point(45, 91);
            this.labelSquare.Name = "labelSquare";
            this.labelSquare.Size = new System.Drawing.Size(102, 13);
            this.labelSquare.TabIndex = 4;
            this.labelSquare.Text = "Sampling square, m²";
            // 
            // labelRepeats
            // 
            this.labelRepeats.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelRepeats.AutoSize = true;
            this.labelRepeats.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRepeats.Location = new System.Drawing.Point(270, 91);
            this.labelRepeats.Name = "labelRepeats";
            this.labelRepeats.Size = new System.Drawing.Size(47, 13);
            this.labelRepeats.TabIndex = 5;
            this.labelRepeats.Text = "Repeats";
            this.labelRepeats.Visible = false;
            // 
            // labelMesh
            // 
            this.labelMesh.AutoSize = true;
            this.labelMesh.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMesh.Location = new System.Drawing.Point(45, 117);
            this.labelMesh.Name = "labelMesh";
            this.labelMesh.Size = new System.Drawing.Size(95, 13);
            this.labelMesh.TabIndex = 7;
            this.labelMesh.Text = "Sieve opening, µm";
            // 
            // labelDepth
            // 
            this.labelDepth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDepth.AutoSize = true;
            this.labelDepth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDepth.Location = new System.Drawing.Point(270, 117);
            this.labelDepth.Name = "labelDepth";
            this.labelDepth.Size = new System.Drawing.Size(50, 13);
            this.labelDepth.TabIndex = 9;
            this.labelDepth.Text = "Depth, m";
            // 
            // textBoxDepth
            // 
            this.textBoxDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDepth.Location = new System.Drawing.Point(434, 114);
            this.textBoxDepth.Name = "textBoxDepth";
            this.textBoxDepth.Size = new System.Drawing.Size(50, 20);
            this.textBoxDepth.TabIndex = 10;
            this.textBoxDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.ItemSubSand.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubLoamySand
            // 
            this.ItemSubLoamySand.Name = "ItemSubLoamySand";
            this.ItemSubLoamySand.Size = new System.Drawing.Size(138, 22);
            this.ItemSubLoamySand.Tag = "";
            this.ItemSubLoamySand.Text = "Loamy sand";
            this.ItemSubLoamySand.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
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
            this.ItemSubSilt.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
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
            this.ItemSubClay.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubSandyClay
            // 
            this.ItemSubSandyClay.Name = "ItemSubSandyClay";
            this.ItemSubSandyClay.Size = new System.Drawing.Size(130, 22);
            this.ItemSubSandyClay.Text = "Sandy clay";
            this.ItemSubSandyClay.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubSiltyClay
            // 
            this.ItemSubSiltyClay.Name = "ItemSubSiltyClay";
            this.ItemSubSiltyClay.Size = new System.Drawing.Size(130, 22);
            this.ItemSubSiltyClay.Text = "Silty clay";
            this.ItemSubSiltyClay.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
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
            this.ItemSubLoam.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubSandyLoam
            // 
            this.ItemSubSandyLoam.Name = "ItemSubSandyLoam";
            this.ItemSubSandyLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSandyLoam.Text = "Sandy loam";
            this.ItemSubSandyLoam.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubSandyClayLoam
            // 
            this.ItemSubSandyClayLoam.Name = "ItemSubSandyClayLoam";
            this.ItemSubSandyClayLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSandyClayLoam.Text = "Sandy clay loam";
            this.ItemSubSandyClayLoam.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubClayLoam
            // 
            this.ItemSubClayLoam.Name = "ItemSubClayLoam";
            this.ItemSubClayLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubClayLoam.Text = "Clay loam";
            this.ItemSubClayLoam.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubSiltyClayLoam
            // 
            this.ItemSubSiltyClayLoam.Name = "ItemSubSiltyClayLoam";
            this.ItemSubSiltyClayLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSiltyClayLoam.Text = "Silty clay loam";
            this.ItemSubSiltyClayLoam.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // ItemSubSiltLoam
            // 
            this.ItemSubSiltLoam.Name = "ItemSubSiltLoam";
            this.ItemSubSiltLoam.Size = new System.Drawing.Size(160, 22);
            this.ItemSubSiltLoam.Text = "Silt loam";
            this.ItemSubSiltLoam.Click += new System.EventHandler(this.ToolStripMenuItemTexture_Click);
            // 
            // tabPageSubstrate
            // 
            this.tabPageSubstrate.Controls.Add(this.labelTexture);
            this.tabPageSubstrate.Controls.Add(this.buttonSubTexture);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownDebris);
            this.tabPageSubstrate.Controls.Add(this.checkBoxClay);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownSapropel);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownCobble);
            this.tabPageSubstrate.Controls.Add(this.checkBoxSilt);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownCPOM);
            this.tabPageSubstrate.Controls.Add(this.checkBoxPhytal);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownFPOM);
            this.tabPageSubstrate.Controls.Add(this.checkBoxSand);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownGravel);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownLiving);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownBoulder);
            this.tabPageSubstrate.Controls.Add(this.checkBoxWood);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownWood);
            this.tabPageSubstrate.Controls.Add(this.checkBoxGravel);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownSand);
            this.tabPageSubstrate.Controls.Add(this.checkBoxDebris);
            this.tabPageSubstrate.Controls.Add(this.checkBoxCobble);
            this.tabPageSubstrate.Controls.Add(this.checkBoxLiving);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownPhytal);
            this.tabPageSubstrate.Controls.Add(this.checkBoxBoulder);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownSilt);
            this.tabPageSubstrate.Controls.Add(this.checkBoxSapropel);
            this.tabPageSubstrate.Controls.Add(this.labelOrganics);
            this.tabPageSubstrate.Controls.Add(this.checkBoxFPOM);
            this.tabPageSubstrate.Controls.Add(this.checkBoxCPOM);
            this.tabPageSubstrate.Controls.Add(this.labelMinerals);
            this.tabPageSubstrate.Controls.Add(this.numericUpDownClay);
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
            // 
            // numericUpDownDebris
            // 
            this.numericUpDownDebris.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDebris.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownDebris.Location = new System.Drawing.Point(433, 284);
            this.numericUpDownDebris.Name = "numericUpDownDebris";
            this.numericUpDownDebris.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownDebris.TabIndex = 70;
            this.numericUpDownDebris.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownDebris.Visible = false;
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
            // numericUpDownSapropel
            // 
            this.numericUpDownSapropel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSapropel.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownSapropel.Location = new System.Drawing.Point(433, 258);
            this.numericUpDownSapropel.Name = "numericUpDownSapropel";
            this.numericUpDownSapropel.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSapropel.TabIndex = 68;
            this.numericUpDownSapropel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSapropel.Visible = false;
            // 
            // numericUpDownCobble
            // 
            this.numericUpDownCobble.Location = new System.Drawing.Point(135, 93);
            this.numericUpDownCobble.Name = "numericUpDownCobble";
            this.numericUpDownCobble.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownCobble.TabIndex = 45;
            this.numericUpDownCobble.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCobble.Visible = false;
            this.numericUpDownCobble.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
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
            // numericUpDownCPOM
            // 
            this.numericUpDownCPOM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownCPOM.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownCPOM.Location = new System.Drawing.Point(214, 310);
            this.numericUpDownCPOM.Name = "numericUpDownCPOM";
            this.numericUpDownCPOM.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownCPOM.TabIndex = 64;
            this.numericUpDownCPOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCPOM.Visible = false;
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
            // 
            // numericUpDownFPOM
            // 
            this.numericUpDownFPOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFPOM.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownFPOM.Location = new System.Drawing.Point(433, 232);
            this.numericUpDownFPOM.Name = "numericUpDownFPOM";
            this.numericUpDownFPOM.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownFPOM.TabIndex = 66;
            this.numericUpDownFPOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFPOM.Visible = false;
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
            // numericUpDownGravel
            // 
            this.numericUpDownGravel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownGravel.Location = new System.Drawing.Point(285, 67);
            this.numericUpDownGravel.Name = "numericUpDownGravel";
            this.numericUpDownGravel.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownGravel.TabIndex = 47;
            this.numericUpDownGravel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownGravel.Visible = false;
            this.numericUpDownGravel.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // numericUpDownLiving
            // 
            this.numericUpDownLiving.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownLiving.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownLiving.Location = new System.Drawing.Point(214, 258);
            this.numericUpDownLiving.Name = "numericUpDownLiving";
            this.numericUpDownLiving.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownLiving.TabIndex = 60;
            this.numericUpDownLiving.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownLiving.Visible = false;
            // 
            // numericUpDownBoulder
            // 
            this.numericUpDownBoulder.Location = new System.Drawing.Point(135, 67);
            this.numericUpDownBoulder.Name = "numericUpDownBoulder";
            this.numericUpDownBoulder.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownBoulder.TabIndex = 43;
            this.numericUpDownBoulder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBoulder.Visible = false;
            this.numericUpDownBoulder.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
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
            // 
            // numericUpDownWood
            // 
            this.numericUpDownWood.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownWood.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownWood.Location = new System.Drawing.Point(214, 284);
            this.numericUpDownWood.Name = "numericUpDownWood";
            this.numericUpDownWood.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownWood.TabIndex = 62;
            this.numericUpDownWood.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownWood.Visible = false;
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
            // numericUpDownSand
            // 
            this.numericUpDownSand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownSand.Location = new System.Drawing.Point(285, 93);
            this.numericUpDownSand.Name = "numericUpDownSand";
            this.numericUpDownSand.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSand.TabIndex = 49;
            this.numericUpDownSand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSand.Visible = false;
            this.numericUpDownSand.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
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
            // 
            // numericUpDownPhytal
            // 
            this.numericUpDownPhytal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownPhytal.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPhytal.Location = new System.Drawing.Point(214, 232);
            this.numericUpDownPhytal.Name = "numericUpDownPhytal";
            this.numericUpDownPhytal.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownPhytal.TabIndex = 58;
            this.numericUpDownPhytal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownPhytal.Visible = false;
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
            // numericUpDownSilt
            // 
            this.numericUpDownSilt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSilt.Location = new System.Drawing.Point(433, 67);
            this.numericUpDownSilt.Name = "numericUpDownSilt";
            this.numericUpDownSilt.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSilt.TabIndex = 51;
            this.numericUpDownSilt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSilt.Visible = false;
            this.numericUpDownSilt.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
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
            // numericUpDownClay
            // 
            this.numericUpDownClay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownClay.Location = new System.Drawing.Point(433, 93);
            this.numericUpDownClay.Name = "numericUpDownClay";
            this.numericUpDownClay.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownClay.TabIndex = 53;
            this.numericUpDownClay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownClay.Visible = false;
            this.numericUpDownClay.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // BenthosCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(564, 631);
            this.Name = "BenthosCard";
            this.OnSaved += new System.EventHandler(this.BenthosCard_OnSaved);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReps)).EndInit();
            this.contextMenuStripSubstrate.ResumeLayout(false);
            this.tabPageSubstrate.ResumeLayout(false);
            this.tabPageSubstrate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDebris)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSapropel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCobble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCPOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGravel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBoulder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhytal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSilt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetSquare() {
            if (SelectedSampler != null && !SelectedSampler.IsEffortValueNull()) {
                switch (SelectedSampler.GetSamplerType()) {
                    case BenthosSamplerType.Grabber:
                        Square = (double)numericUpDownReps.Value * SelectedSampler.EffortValue;
                        break;
                    case BenthosSamplerType.Scraper:
                        Square = (double)numericUpDownReps.Value * SelectedSampler.EffortValue / 100;
                        break;
                }
            } else {
                //Square = double.NaN;
            }
        }

        private void SetCombos(WaterType type) {
            comboBoxCrossSection.Items.Clear();
            comboBoxCrossSection.Items.AddRange(Wild.Service.CrossSection(type));
            comboBoxBank.Enabled = type != WaterType.Lake;
        }

        private void DropSubs() {
            numericUpDownBoulder.Value = 0;
            numericUpDownCobble.Value = 0;
            numericUpDownGravel.Value = 0;
            numericUpDownSand.Value = 0;
            numericUpDownSilt.Value = 0;
            numericUpDownClay.Value = 0;

            numericUpDownPhytal.Value = 0;
            numericUpDownLiving.Value = 0;
            numericUpDownWood.Value = 0;
            numericUpDownCPOM.Value = 0;
            numericUpDownFPOM.Value = 0;
            numericUpDownSapropel.Value = 0;
            numericUpDownDebris.Value = 0;
        }

        private void ResetSubs() {
            checkBoxBoulder.Checked = (numericUpDownBoulder.Value > 0);
            checkBoxCobble.Checked = (numericUpDownCobble.Value > 0);
            checkBoxGravel.Checked = (numericUpDownGravel.Value > 0);
            checkBoxSand.Checked = (numericUpDownSand.Value > 0);
            checkBoxSilt.Checked = (numericUpDownSilt.Value > 0);
            checkBoxClay.Checked = (numericUpDownClay.Value > 0);

            checkBoxPhytal.Checked = (numericUpDownPhytal.Value > 0);
            checkBoxLiving.Checked = (numericUpDownLiving.Value > 0);
            checkBoxWood.Checked = (numericUpDownWood.Value > 0);
            checkBoxCPOM.Checked = (numericUpDownCPOM.Value > 0);
            checkBoxFPOM.Checked = (numericUpDownFPOM.Value > 0);
            checkBoxSapropel.Checked = (numericUpDownSapropel.Value > 0);
            checkBoxDebris.Checked = (numericUpDownDebris.Value > 0);
        }



        private void BenthosCard_OnSaved(object sender, EventArgs e) {


            #region Save substrate values

            SubstrateSample substrate = new SubstrateSample(0, 0, 0);

            #region Mineral Substrates

            if (checkBoxBoulder.Checked) {
                substrate.Boulder = (int)numericUpDownBoulder.Value;
            }

            if (checkBoxCobble.Checked) {
                substrate.Cobble = (int)numericUpDownCobble.Value;
            }

            if (checkBoxGravel.Checked) {
                substrate.Gravel = (int)numericUpDownGravel.Value;
            }

            if (checkBoxSand.Checked) {
                substrate.Sand = (int)numericUpDownSand.Value;
            }

            if (checkBoxSilt.Checked) {
                substrate.Silt = (int)numericUpDownSilt.Value;
            }

            if (checkBoxClay.Checked) {
                substrate.Clay = (int)numericUpDownClay.Value;
            }

            #endregion

            #region Organic Substrates

            if (checkBoxPhytal.Checked) {
                substrate.Phytal = (int)numericUpDownPhytal.Value;
            }

            if (checkBoxLiving.Checked) {
                substrate.Living = (int)numericUpDownLiving.Value;
            }

            if (checkBoxWood.Checked) {
                substrate.Wood = (int)numericUpDownWood.Value;
            }

            if (checkBoxCPOM.Checked) {
                substrate.CPOM = (int)numericUpDownCPOM.Value;
            }

            if (checkBoxFPOM.Checked) {
                substrate.FPOM = (int)numericUpDownFPOM.Value;
            }

            if (checkBoxSapropel.Checked) {
                substrate.Sapropel = (int)numericUpDownSapropel.Value;
            }

            if (checkBoxDebris.Checked) {
                substrate.Debris = (int)numericUpDownDebris.Value;
            }

            #endregion

            if (substrate.IsAvailable) {
                data.Solitary.Substrate = substrate.Protocol;
            } else {
                data.Solitary.SetSubstrateNull();
            }

            #endregion
        }

        private void waterSelector_WaterSelected(object sender, WaterEventArgs e) {
            SetCombos(waterSelector.IsWaterSelected ? waterSelector.WaterObject.WaterType : WaterType.None);
            statusCard.Message(Wild.Resources.Interface.Messages.WaterSet);
        }

        private void sampler_Changed(object sender, EventArgs e) {
            BenthosSamplerType kind = SelectedSampler.GetSamplerType();

            textBoxSquare.ReadOnly = labelRepeats.Visible =
                numericUpDownReps.Visible = kind != BenthosSamplerType.Manual;

            switch (kind) {
                case BenthosSamplerType.Grabber:
                    labelRepeats.Text = Resources.Interface.Interface.Repeats;
                    break;
                case BenthosSamplerType.Scraper:
                    labelRepeats.Text = Resources.Interface.Interface.Expanse;
                    break;
            }

            SetSquare();
            isChanged = true;
        }


        private void numericUpDownReps_ValueChanged(object sender, EventArgs e) {
            if (numericUpDownReps.ContainsFocus) {
                SetSquare();
            }
        }

        private void numericUpDownReps_VisibleChanged(object sender, EventArgs e) {
            if (!numericUpDownReps.Visible) {
                //numericUpDownReps.Value = 0;
                textBoxSquare.Text = string.Empty;
            }
        }

        private void textBoxSquare_TextChanged(object sender, EventArgs e) {
            value_Changed(sender, e);
            Logger.UpdateStatus();
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

        private void pictureBoxWarnOpening_MouseHover(object sender, EventArgs e) {
            if (comboBoxCrossSection.SelectedIndex == -1 ||
                comboBoxCrossSection.SelectedIndex == comboBoxCrossSection.Items.Count - 1) {
                labelBank.Enabled = comboBoxBank.Enabled = false;
            } else {
                labelBank.Enabled = comboBoxBank.Enabled = true;
            }

            value_Changed(sender, e);
        }



        private void substrate_ValueChanged(object sender, EventArgs e) {
            value_Changed(sender, e);

            if (sender is NumericUpDown down && down.ContainsFocus) {
                int selectedValue = (int)down.Value;

                int rest = 100 - selectedValue;

                int currentRest = 0;

                foreach (NumericUpDown numericUpDown in MineralControls) {
                    if (numericUpDown == down) continue;
                    currentRest += (int)numericUpDown.Value;
                }

                if (currentRest > rest) {
                    foreach (NumericUpDown numericUpDown in MineralControls) {
                        if (numericUpDown == down) continue;
                        if (numericUpDown.Value == 0) continue;
                        numericUpDown.Value = (int)(numericUpDown.Value * (decimal)rest / (decimal)currentRest);
                    }
                }
            }

            // TODO: get substrate from sand+silt+clay IF checks are checked

            SubstrateSample substrate = new SubstrateSample(
                (double)numericUpDownSand.Value,
                (double)numericUpDownSilt.Value,
                (double)numericUpDownClay.Value);

            labelTexture.Text = substrate.TypeName;
        }

        private void checkBoxGranulae_CheckedChanged(object sender, EventArgs e) {
            Control control = Controls.Find("numericUpDown" +
                ((CheckBox)sender).Name.Substring(8), true)[0];

            if (control is NumericUpDown) {
                control.Visible = ((CheckBox)sender).Checked;
            }

            substrate_ValueChanged(sender, e);
        }

        private void buttonSubTexture_Click(object sender, EventArgs e) {
            contextMenuStripSubstrate.Show(buttonSubTexture, new Point(buttonSubTexture.Width, 0), ToolStripDropDownDirection.Right);
        }

        private void ToolStripMenuItemTexture_Click(object sender, EventArgs e) {

            DropSubs();
            string texture = ((ToolStripMenuItem)sender).Name.Substring(7);
            SubstrateSample substrate = new SubstrateSample(SubstrateSample.FromName(texture));
            numericUpDownSand.Value = 100 * (decimal)substrate.Sand;
            numericUpDownSilt.Value = 100 * (decimal)substrate.Silt;
            numericUpDownClay.Value = 100 * (decimal)substrate.Clay;
            ResetSubs();
        }



        private void logger_IndividualsRequired(object sender, EventArgs e) {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                if (gridRow.Cells[ColumnSpecies.Name].Value != null) {

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
                        individuals.SetColumns(ColumnSpecies, ColumnQuantity, ColumnMass);
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
    }
}
