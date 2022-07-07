using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Benthos
{
    public class BenthosCard : Wild.Card
    {
        private ComboBox comboBoxBank;
        private ComboBox comboBoxCrossSection;
        private Label labelCrossSection;
        private Label labelTexture;
        private Button buttonSubTexture;
        private TextBox textBoxDepth;
        private NumericUpDown numericUpDownDebris;
        private NumericUpDown numericUpDownReps;
        private Label labelDepth;
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
        private Label labelMesh;
        private CheckBox checkBoxWood;
        private NumericUpDown numericUpDownWood;
        private CheckBox checkBoxGravel;
        private Label labelRepeats;
        private NumericUpDown numericUpDownSand;
        private CheckBox checkBoxDebris;
        private CheckBox checkBoxCobble;
        private Label labelSquare;
        private CheckBox checkBoxLiving;
        private NumericUpDown numericUpDownPhytal;
        private CheckBox checkBoxBoulder;
        private TextBox textBoxMesh;
        private NumericUpDown numericUpDownSilt;
        private CheckBox checkBoxSapropel;
        private Label labelOrganics;
        private TextBox textBoxSquare;
        private CheckBox checkBoxFPOM;
        private CheckBox checkBoxCPOM;
        private Label labelMinerals;
        private NumericUpDown numericUpDownClay;
        private Label labelBank;

        public BenthosCard() : base() {
            Initiate();
        }

        private void InitializeComponent() {
            this.labelBank = new System.Windows.Forms.Label();
            this.labelCrossSection = new System.Windows.Forms.Label();
            this.comboBoxCrossSection = new System.Windows.Forms.ComboBox();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.checkBoxClay = new System.Windows.Forms.CheckBox();
            this.numericUpDownCobble = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSilt = new System.Windows.Forms.CheckBox();
            this.checkBoxPhytal = new System.Windows.Forms.CheckBox();
            this.checkBoxSand = new System.Windows.Forms.CheckBox();
            this.numericUpDownGravel = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBoulder = new System.Windows.Forms.NumericUpDown();
            this.checkBoxWood = new System.Windows.Forms.CheckBox();
            this.checkBoxGravel = new System.Windows.Forms.CheckBox();
            this.numericUpDownSand = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCobble = new System.Windows.Forms.CheckBox();
            this.checkBoxLiving = new System.Windows.Forms.CheckBox();
            this.checkBoxBoulder = new System.Windows.Forms.CheckBox();
            this.numericUpDownSilt = new System.Windows.Forms.NumericUpDown();
            this.labelOrganics = new System.Windows.Forms.Label();
            this.checkBoxFPOM = new System.Windows.Forms.CheckBox();
            this.labelMinerals = new System.Windows.Forms.Label();
            this.numericUpDownClay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownReps = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCPOM = new System.Windows.Forms.CheckBox();
            this.textBoxSquare = new System.Windows.Forms.TextBox();
            this.checkBoxSapropel = new System.Windows.Forms.CheckBox();
            this.textBoxMesh = new System.Windows.Forms.TextBox();
            this.numericUpDownPhytal = new System.Windows.Forms.NumericUpDown();
            this.labelSquare = new System.Windows.Forms.Label();
            this.checkBoxDebris = new System.Windows.Forms.CheckBox();
            this.labelRepeats = new System.Windows.Forms.Label();
            this.numericUpDownWood = new System.Windows.Forms.NumericUpDown();
            this.labelMesh = new System.Windows.Forms.Label();
            this.numericUpDownLiving = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFPOM = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCPOM = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSapropel = new System.Windows.Forms.NumericUpDown();
            this.labelDepth = new System.Windows.Forms.Label();
            this.numericUpDownDebris = new System.Windows.Forms.NumericUpDown();
            this.textBoxDepth = new System.Windows.Forms.TextBox();
            this.buttonSubTexture = new System.Windows.Forms.Button();
            this.labelTexture = new System.Windows.Forms.Label();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCobble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGravel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBoulder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSilt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhytal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCPOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSapropel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDebris)).BeginInit();
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
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.labelTexture);
            this.tabPageSampler.Controls.Add(this.buttonSubTexture);
            this.tabPageSampler.Controls.Add(this.textBoxDepth);
            this.tabPageSampler.Controls.Add(this.numericUpDownDebris);
            this.tabPageSampler.Controls.Add(this.numericUpDownReps);
            this.tabPageSampler.Controls.Add(this.labelDepth);
            this.tabPageSampler.Controls.Add(this.checkBoxClay);
            this.tabPageSampler.Controls.Add(this.numericUpDownSapropel);
            this.tabPageSampler.Controls.Add(this.numericUpDownCobble);
            this.tabPageSampler.Controls.Add(this.checkBoxSilt);
            this.tabPageSampler.Controls.Add(this.numericUpDownCPOM);
            this.tabPageSampler.Controls.Add(this.checkBoxPhytal);
            this.tabPageSampler.Controls.Add(this.numericUpDownFPOM);
            this.tabPageSampler.Controls.Add(this.checkBoxSand);
            this.tabPageSampler.Controls.Add(this.numericUpDownGravel);
            this.tabPageSampler.Controls.Add(this.numericUpDownLiving);
            this.tabPageSampler.Controls.Add(this.numericUpDownBoulder);
            this.tabPageSampler.Controls.Add(this.labelMesh);
            this.tabPageSampler.Controls.Add(this.checkBoxWood);
            this.tabPageSampler.Controls.Add(this.numericUpDownWood);
            this.tabPageSampler.Controls.Add(this.checkBoxGravel);
            this.tabPageSampler.Controls.Add(this.labelRepeats);
            this.tabPageSampler.Controls.Add(this.numericUpDownSand);
            this.tabPageSampler.Controls.Add(this.checkBoxDebris);
            this.tabPageSampler.Controls.Add(this.checkBoxCobble);
            this.tabPageSampler.Controls.Add(this.labelSquare);
            this.tabPageSampler.Controls.Add(this.checkBoxLiving);
            this.tabPageSampler.Controls.Add(this.numericUpDownPhytal);
            this.tabPageSampler.Controls.Add(this.checkBoxBoulder);
            this.tabPageSampler.Controls.Add(this.textBoxMesh);
            this.tabPageSampler.Controls.Add(this.numericUpDownSilt);
            this.tabPageSampler.Controls.Add(this.checkBoxSapropel);
            this.tabPageSampler.Controls.Add(this.labelOrganics);
            this.tabPageSampler.Controls.Add(this.textBoxSquare);
            this.tabPageSampler.Controls.Add(this.checkBoxFPOM);
            this.tabPageSampler.Controls.Add(this.checkBoxCPOM);
            this.tabPageSampler.Controls.Add(this.labelMinerals);
            this.tabPageSampler.Controls.Add(this.numericUpDownClay);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownClay, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMinerals, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxCPOM, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxFPOM, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelOrganics, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxSapropel, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownSilt, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxBoulder, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownPhytal, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxLiving, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSquare, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxCobble, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxDebris, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownSand, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelRepeats, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxGravel, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownWood, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxWood, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMesh, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownBoulder, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownLiving, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownGravel, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxSand, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownFPOM, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxPhytal, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownCPOM, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxSilt, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownCobble, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownSapropel, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.checkBoxClay, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelDepth, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownReps, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownDebris, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBoxDepth, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonSubTexture, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelTexture, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonGear, 0);
            // 
            // comboBoxSampler
            // 
            this.comboBoxSampler.TabIndex = 3;
            // 
            // buttonGear
            // 
            this.buttonGear.TabIndex = 2;
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
            this.comboBoxCrossSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
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
            this.comboBoxBank.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.comboBoxBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // checkBoxClay
            // 
            this.checkBoxClay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxClay.AutoSize = true;
            this.checkBoxClay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxClay.Location = new System.Drawing.Point(348, 224);
            this.checkBoxClay.Name = "checkBoxClay";
            this.checkBoxClay.Size = new System.Drawing.Size(46, 17);
            this.checkBoxClay.TabIndex = 22;
            this.checkBoxClay.Text = "Clay";
            this.checkBoxClay.UseVisualStyleBackColor = true;
            this.checkBoxClay.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericUpDownCobble
            // 
            this.numericUpDownCobble.Location = new System.Drawing.Point(135, 223);
            this.numericUpDownCobble.Name = "numericUpDownCobble";
            this.numericUpDownCobble.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownCobble.TabIndex = 15;
            this.numericUpDownCobble.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCobble.Visible = false;
            this.numericUpDownCobble.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxSilt
            // 
            this.checkBoxSilt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSilt.AutoSize = true;
            this.checkBoxSilt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSilt.Location = new System.Drawing.Point(348, 198);
            this.checkBoxSilt.Name = "checkBoxSilt";
            this.checkBoxSilt.Size = new System.Drawing.Size(40, 17);
            this.checkBoxSilt.TabIndex = 20;
            this.checkBoxSilt.Text = "Silt";
            this.checkBoxSilt.UseVisualStyleBackColor = true;
            this.checkBoxSilt.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxPhytal
            // 
            this.checkBoxPhytal.AutoSize = true;
            this.checkBoxPhytal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxPhytal.Location = new System.Drawing.Point(48, 363);
            this.checkBoxPhytal.Name = "checkBoxPhytal";
            this.checkBoxPhytal.Size = new System.Drawing.Size(145, 17);
            this.checkBoxPhytal.TabIndex = 27;
            this.checkBoxPhytal.Text = "Phytal (macrophytes etc.)";
            this.checkBoxPhytal.UseVisualStyleBackColor = true;
            this.checkBoxPhytal.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxSand
            // 
            this.checkBoxSand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxSand.AutoSize = true;
            this.checkBoxSand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSand.Location = new System.Drawing.Point(198, 224);
            this.checkBoxSand.Name = "checkBoxSand";
            this.checkBoxSand.Size = new System.Drawing.Size(51, 17);
            this.checkBoxSand.TabIndex = 18;
            this.checkBoxSand.Text = "Sand";
            this.checkBoxSand.UseVisualStyleBackColor = true;
            this.checkBoxSand.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericUpDownGravel
            // 
            this.numericUpDownGravel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownGravel.Location = new System.Drawing.Point(285, 197);
            this.numericUpDownGravel.Name = "numericUpDownGravel";
            this.numericUpDownGravel.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownGravel.TabIndex = 17;
            this.numericUpDownGravel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownGravel.Visible = false;
            this.numericUpDownGravel.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // numericUpDownBoulder
            // 
            this.numericUpDownBoulder.Location = new System.Drawing.Point(135, 197);
            this.numericUpDownBoulder.Name = "numericUpDownBoulder";
            this.numericUpDownBoulder.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownBoulder.TabIndex = 13;
            this.numericUpDownBoulder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBoulder.Visible = false;
            this.numericUpDownBoulder.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxWood
            // 
            this.checkBoxWood.AutoSize = true;
            this.checkBoxWood.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxWood.Location = new System.Drawing.Point(48, 415);
            this.checkBoxWood.Name = "checkBoxWood";
            this.checkBoxWood.Size = new System.Drawing.Size(110, 17);
            this.checkBoxWood.TabIndex = 31;
            this.checkBoxWood.Text = "Xylal (dead wood)";
            this.checkBoxWood.UseVisualStyleBackColor = true;
            this.checkBoxWood.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxGravel
            // 
            this.checkBoxGravel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxGravel.AutoSize = true;
            this.checkBoxGravel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxGravel.Location = new System.Drawing.Point(198, 198);
            this.checkBoxGravel.Name = "checkBoxGravel";
            this.checkBoxGravel.Size = new System.Drawing.Size(57, 17);
            this.checkBoxGravel.TabIndex = 16;
            this.checkBoxGravel.Text = "Gravel";
            this.checkBoxGravel.UseVisualStyleBackColor = true;
            this.checkBoxGravel.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericUpDownSand
            // 
            this.numericUpDownSand.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownSand.Location = new System.Drawing.Point(285, 223);
            this.numericUpDownSand.Name = "numericUpDownSand";
            this.numericUpDownSand.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSand.TabIndex = 19;
            this.numericUpDownSand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSand.Visible = false;
            this.numericUpDownSand.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // checkBoxCobble
            // 
            this.checkBoxCobble.AutoSize = true;
            this.checkBoxCobble.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxCobble.Location = new System.Drawing.Point(48, 224);
            this.checkBoxCobble.Name = "checkBoxCobble";
            this.checkBoxCobble.Size = new System.Drawing.Size(64, 17);
            this.checkBoxCobble.TabIndex = 14;
            this.checkBoxCobble.Text = "Cobbles";
            this.checkBoxCobble.UseVisualStyleBackColor = true;
            this.checkBoxCobble.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxLiving
            // 
            this.checkBoxLiving.AutoSize = true;
            this.checkBoxLiving.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxLiving.Location = new System.Drawing.Point(48, 389);
            this.checkBoxLiving.Name = "checkBoxLiving";
            this.checkBoxLiving.Size = new System.Drawing.Size(103, 17);
            this.checkBoxLiving.TabIndex = 29;
            this.checkBoxLiving.Text = "Terrestrial plants";
            this.checkBoxLiving.UseVisualStyleBackColor = true;
            this.checkBoxLiving.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // checkBoxBoulder
            // 
            this.checkBoxBoulder.AutoSize = true;
            this.checkBoxBoulder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxBoulder.Location = new System.Drawing.Point(48, 198);
            this.checkBoxBoulder.Name = "checkBoxBoulder";
            this.checkBoxBoulder.Size = new System.Drawing.Size(67, 17);
            this.checkBoxBoulder.TabIndex = 12;
            this.checkBoxBoulder.Text = "Boulders";
            this.checkBoxBoulder.UseVisualStyleBackColor = true;
            this.checkBoxBoulder.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // numericUpDownSilt
            // 
            this.numericUpDownSilt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSilt.Location = new System.Drawing.Point(433, 197);
            this.numericUpDownSilt.Name = "numericUpDownSilt";
            this.numericUpDownSilt.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSilt.TabIndex = 21;
            this.numericUpDownSilt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSilt.Visible = false;
            this.numericUpDownSilt.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
            // 
            // labelOrganics
            // 
            this.labelOrganics.AutoSize = true;
            this.labelOrganics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelOrganics.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelOrganics.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelOrganics.Location = new System.Drawing.Point(28, 320);
            this.labelOrganics.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelOrganics.Name = "labelOrganics";
            this.labelOrganics.Size = new System.Drawing.Size(113, 15);
            this.labelOrganics.TabIndex = 26;
            this.labelOrganics.Text = "Biotic Microhabitats";
            // 
            // checkBoxFPOM
            // 
            this.checkBoxFPOM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxFPOM.AutoSize = true;
            this.checkBoxFPOM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxFPOM.Location = new System.Drawing.Point(273, 363);
            this.checkBoxFPOM.Name = "checkBoxFPOM";
            this.checkBoxFPOM.Size = new System.Drawing.Size(56, 17);
            this.checkBoxFPOM.TabIndex = 35;
            this.checkBoxFPOM.Text = "FPOM";
            this.checkBoxFPOM.UseVisualStyleBackColor = true;
            this.checkBoxFPOM.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
            // 
            // labelMinerals
            // 
            this.labelMinerals.AutoSize = true;
            this.labelMinerals.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelMinerals.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelMinerals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMinerals.Location = new System.Drawing.Point(28, 155);
            this.labelMinerals.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelMinerals.Name = "labelMinerals";
            this.labelMinerals.Size = new System.Drawing.Size(119, 15);
            this.labelMinerals.TabIndex = 11;
            this.labelMinerals.Text = "Mineral Composition";
            // 
            // numericUpDownClay
            // 
            this.numericUpDownClay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownClay.Location = new System.Drawing.Point(433, 223);
            this.numericUpDownClay.Name = "numericUpDownClay";
            this.numericUpDownClay.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownClay.TabIndex = 23;
            this.numericUpDownClay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownClay.Visible = false;
            this.numericUpDownClay.ValueChanged += new System.EventHandler(this.substrate_ValueChanged);
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
            // checkBoxCPOM
            // 
            this.checkBoxCPOM.AutoSize = true;
            this.checkBoxCPOM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxCPOM.Location = new System.Drawing.Point(48, 441);
            this.checkBoxCPOM.Name = "checkBoxCPOM";
            this.checkBoxCPOM.Size = new System.Drawing.Size(149, 17);
            this.checkBoxCPOM.TabIndex = 33;
            this.checkBoxCPOM.Text = "CPOM (e. g. fallen leaves)";
            this.checkBoxCPOM.UseVisualStyleBackColor = true;
            this.checkBoxCPOM.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
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
            // checkBoxSapropel
            // 
            this.checkBoxSapropel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxSapropel.AutoSize = true;
            this.checkBoxSapropel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSapropel.Location = new System.Drawing.Point(273, 389);
            this.checkBoxSapropel.Name = "checkBoxSapropel";
            this.checkBoxSapropel.Size = new System.Drawing.Size(132, 17);
            this.checkBoxSapropel.TabIndex = 37;
            this.checkBoxSapropel.Text = "Organic mud, sapropel";
            this.checkBoxSapropel.UseVisualStyleBackColor = true;
            this.checkBoxSapropel.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
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
            // numericUpDownPhytal
            // 
            this.numericUpDownPhytal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownPhytal.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPhytal.Location = new System.Drawing.Point(214, 362);
            this.numericUpDownPhytal.Name = "numericUpDownPhytal";
            this.numericUpDownPhytal.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownPhytal.TabIndex = 28;
            this.numericUpDownPhytal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownPhytal.Visible = false;
            this.numericUpDownPhytal.ValueChanged += new System.EventHandler(this.value_Changed);
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
            // checkBoxDebris
            // 
            this.checkBoxDebris.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBoxDebris.AutoSize = true;
            this.checkBoxDebris.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxDebris.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDebris.Location = new System.Drawing.Point(273, 415);
            this.checkBoxDebris.Name = "checkBoxDebris";
            this.checkBoxDebris.Size = new System.Drawing.Size(56, 17);
            this.checkBoxDebris.TabIndex = 39;
            this.checkBoxDebris.Text = "Debris";
            this.checkBoxDebris.UseVisualStyleBackColor = true;
            this.checkBoxDebris.CheckedChanged += new System.EventHandler(this.checkBoxGranulae_CheckedChanged);
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
            // numericUpDownWood
            // 
            this.numericUpDownWood.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownWood.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownWood.Location = new System.Drawing.Point(214, 414);
            this.numericUpDownWood.Name = "numericUpDownWood";
            this.numericUpDownWood.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownWood.TabIndex = 32;
            this.numericUpDownWood.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownWood.Visible = false;
            this.numericUpDownWood.ValueChanged += new System.EventHandler(this.value_Changed);
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
            // numericUpDownLiving
            // 
            this.numericUpDownLiving.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownLiving.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownLiving.Location = new System.Drawing.Point(214, 388);
            this.numericUpDownLiving.Name = "numericUpDownLiving";
            this.numericUpDownLiving.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownLiving.TabIndex = 30;
            this.numericUpDownLiving.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownLiving.Visible = false;
            this.numericUpDownLiving.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericUpDownFPOM
            // 
            this.numericUpDownFPOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFPOM.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownFPOM.Location = new System.Drawing.Point(433, 362);
            this.numericUpDownFPOM.Name = "numericUpDownFPOM";
            this.numericUpDownFPOM.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownFPOM.TabIndex = 36;
            this.numericUpDownFPOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFPOM.Visible = false;
            this.numericUpDownFPOM.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericUpDownCPOM
            // 
            this.numericUpDownCPOM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownCPOM.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownCPOM.Location = new System.Drawing.Point(214, 440);
            this.numericUpDownCPOM.Name = "numericUpDownCPOM";
            this.numericUpDownCPOM.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownCPOM.TabIndex = 34;
            this.numericUpDownCPOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCPOM.Visible = false;
            this.numericUpDownCPOM.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericUpDownSapropel
            // 
            this.numericUpDownSapropel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSapropel.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownSapropel.Location = new System.Drawing.Point(433, 388);
            this.numericUpDownSapropel.Name = "numericUpDownSapropel";
            this.numericUpDownSapropel.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSapropel.TabIndex = 38;
            this.numericUpDownSapropel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSapropel.Visible = false;
            this.numericUpDownSapropel.ValueChanged += new System.EventHandler(this.value_Changed);
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
            // numericUpDownDebris
            // 
            this.numericUpDownDebris.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDebris.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownDebris.Location = new System.Drawing.Point(433, 414);
            this.numericUpDownDebris.Name = "numericUpDownDebris";
            this.numericUpDownDebris.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownDebris.TabIndex = 40;
            this.numericUpDownDebris.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownDebris.Visible = false;
            this.numericUpDownDebris.ValueChanged += new System.EventHandler(this.value_Changed);
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
            // buttonSubTexture
            // 
            this.buttonSubTexture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSubTexture.Location = new System.Drawing.Point(48, 269);
            this.buttonSubTexture.Margin = new System.Windows.Forms.Padding(3, 25, 3, 3);
            this.buttonSubTexture.Name = "buttonSubTexture";
            this.buttonSubTexture.Size = new System.Drawing.Size(100, 23);
            this.buttonSubTexture.TabIndex = 24;
            this.buttonSubTexture.Text = "Select texture";
            this.buttonSubTexture.UseVisualStyleBackColor = true;
            this.buttonSubTexture.Click += new System.EventHandler(this.buttonSubTexture_Click);
            // 
            // labelTexture
            // 
            this.labelTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTexture.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelTexture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTexture.Location = new System.Drawing.Point(185, 273);
            this.labelTexture.Name = "labelTexture";
            this.labelTexture.Size = new System.Drawing.Size(300, 15);
            this.labelTexture.TabIndex = 25;
            this.labelTexture.Text = "Change substrate mineral composition";
            this.labelTexture.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // BenthosCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(564, 631);
            this.Name = "BenthosCard";
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCobble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGravel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBoulder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSilt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPhytal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLiving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCPOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSapropel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDebris)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void comboBoxCrossSection_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e) {

        }

        private void value_Changed(object sender, EventArgs e) {

        }

        private void checkBoxGranulae_CheckedChanged(object sender, EventArgs e) {

        }

        private void substrate_ValueChanged(object sender, EventArgs e) {

        }

        private void numericUpDownReps_ValueChanged(object sender, EventArgs e) {

        }

        private void buttonSubTexture_Click(object sender, EventArgs e) {

        }
    }
}
