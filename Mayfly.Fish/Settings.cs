using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Wild;
using System.Windows.Forms;
using System.IO;
using static Mayfly.Fish.UserSettings;

namespace Mayfly.Fish
{
    public class Settings : Mayfly.Wild.Settings
    {
        private Button buttonBrowseParasites;
        private TextBox textBoxParasites;
        private Label label7;
        private Button buttonOpenParasites;
        private TabPage tabPageGears;
        private TabControl tabControl2;
        private TabPage tabPageActive;
        private Controls.SpreadSheet spreadSheetOpening;
        private Label labelOpeningDefault;
        private NumericUpDown numericUpDownOpeningDefault;
        private Label labelActive;
        private Label labelActiveInstruction;
        private TabPage tabPageStationary;
        private Label labelStationary;
        private Label labelExposure;
        private Label labelStationaryInstruction;
        private Label labelHeight;
        private Label labelLength;
        private NumericUpDown numericUpDownStdSoak;
        private NumericUpDown numericUpDownStdHeight;
        private NumericUpDown numericUpDownStdLength;
        private DataGridViewTextBoxColumn columnOpeningGear;
        private TabPage tabPageEquipment;
        private Controls.SpreadSheet spreadSheetGears;
        private TabPage tabPageStratified;
        private Label labelInterval;
        private NumericUpDown numericUpDownInterval;
        private Label labelStratified;
        private Button buttonBrowseDiet;
        private TextBox textBoxDiet;
        private Label label3;
        private Button buttonOpenDiet;
        private DataGridViewComboBoxColumn columnSampler;
        private DataGridViewTextBoxColumn columnMesh;
        private DataGridViewTextBoxColumn columnLength;
        private DataGridViewTextBoxColumn columnHeight;
        private Button buttonGearsClear;
        private DataGridViewTextBoxColumn columnOpeningValue;



        public Settings()
            : base(UserSettings.ReaderSettings)
        {
            InitializeComponent();

            textBoxDiet.Text = UserSettings.DietIndexPath;
            textBoxParasites.Text = UserSettings.ParasitesIndexPath;

            numericUpDownInterval.Value = (decimal)UserSettings.DefaultStratifiedInterval;

            spreadSheetOpening.StringVariants = ReaderSettings.SamplersIndex.GetSamplerNames('O');
            columnOpeningGear.ValueType = typeof(string);
            columnOpeningValue.ValueType = typeof(double);

            columnMesh.ValueType = typeof(int);
            columnLength.ValueType = typeof(double);
            columnHeight.ValueType = typeof(double);

            numericUpDownOpeningDefault.Value = 100 * (decimal)UserSettings.DefaultOpening;
            numericUpDownStdLength.Value = (decimal)UserSettings.GillnetStdLength;
            numericUpDownStdHeight.Value = (decimal)UserSettings.GillnetStdHeight;
            numericUpDownStdSoak.Value = UserSettings.GillnetStdExposure;

            spreadSheetOpening.Rows.Clear();

            foreach (Samplers.SamplerRow samplerRow in ReaderSettings.SamplersIndex.Sampler)
            {
                LoadOpening(samplerRow);
            }

            //columnSampler.DataSource = ReaderSettings.SamplersIndex.GetPassives();
            columnSampler.DataSource = ReaderSettings.SamplersIndex.Sampler.Select();
            columnSampler.DisplayMember = "Sampler";
            columnSampler.ValueMember = "ShortName";

            LoadGears();
        }



        protected override void SaveSettings()
        {
            if (!tabPageReferences.IsDisposed)
            {
                UserSettings.DietIndexPath = textBoxDiet.Text;
                UserSettings.ParasitesIndexPath = textBoxParasites.Text;
            }

            if (!tabPageGears.IsDisposed)
            {
                UserSettings.DefaultOpening = .01 * (double)numericUpDownOpeningDefault.Value;

                UserSetting.ClearFolder(UserSettings.ReaderSettings.Path, nameof(Service.Opening));
                foreach (DataGridViewRow gridRow in spreadSheetOpening.Rows)
                {
                    if (gridRow.IsNewRow) continue;
                    if (gridRow.Cells[columnOpeningGear.Index].Value == null) continue;
                    if (gridRow.Cells[columnOpeningValue.Index].Value == null) continue;
                    if ((double)gridRow.Cells[columnOpeningValue.Index].Value == UserSettings.DefaultOpening) continue;
                    Service.SaveOpening(((Samplers.SamplerRow)gridRow.Tag).ID, (double)gridRow.Cells[columnOpeningValue.Index].Value);
                }

                UserSettings.GillnetStdLength = (double)numericUpDownStdLength.Value;
                UserSettings.GillnetStdHeight = (double)numericUpDownStdHeight.Value;
                UserSettings.GillnetStdExposure = (int)numericUpDownStdSoak.Value;

                UserSettings.Equipment = new Equipment();
                foreach (DataGridViewRow gridRow in spreadSheetGears.Rows)
                {
                    if (gridRow.IsNewRow) continue;
                    if (gridRow.Cells[columnSampler.Index].Value == null) continue;

                    Equipment.UnitsRow unitsRow = UserSettings.Equipment.Units.NewUnitsRow();
                    unitsRow.SamplerID = ReaderSettings.SamplersIndex.Sampler.FindByCode((string)gridRow.Cells[columnSampler.Index].Value).ID;
                    if (gridRow.Cells[columnMesh.Index].Value != null) unitsRow.Mesh = (int)gridRow.Cells[columnMesh.Index].Value;
                    if (gridRow.Cells[columnLength.Index].Value != null) unitsRow.Length = (double)gridRow.Cells[columnLength.Index].Value;
                    if (gridRow.Cells[columnHeight.Index].Value != null) unitsRow.Height = (double)gridRow.Cells[columnHeight.Index].Value;
                    UserSettings.Equipment.Units.AddUnitsRow(unitsRow);
                }
                Service.SaveEquipment();
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonOpenParasites = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxParasites = new System.Windows.Forms.TextBox();
            this.buttonBrowseParasites = new System.Windows.Forms.Button();
            this.tabPageGears = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageEquipment = new System.Windows.Forms.TabPage();
            this.buttonGearsClear = new System.Windows.Forms.Button();
            this.spreadSheetGears = new Mayfly.Controls.SpreadSheet();
            this.columnSampler = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnMesh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageActive = new System.Windows.Forms.TabPage();
            this.spreadSheetOpening = new Mayfly.Controls.SpreadSheet();
            this.columnOpeningGear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOpeningValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelOpeningDefault = new System.Windows.Forms.Label();
            this.numericUpDownOpeningDefault = new System.Windows.Forms.NumericUpDown();
            this.labelActive = new System.Windows.Forms.Label();
            this.labelActiveInstruction = new System.Windows.Forms.Label();
            this.tabPageStationary = new System.Windows.Forms.TabPage();
            this.labelStationary = new System.Windows.Forms.Label();
            this.labelExposure = new System.Windows.Forms.Label();
            this.labelStationaryInstruction = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.numericUpDownStdSoak = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStdHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStdLength = new System.Windows.Forms.NumericUpDown();
            this.tabPageStratified = new System.Windows.Forms.TabPage();
            this.labelInterval = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelStratified = new System.Windows.Forms.Label();
            this.buttonOpenDiet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDiet = new System.Windows.Forms.TextBox();
            this.buttonBrowseDiet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).BeginInit();
            this.tabPageReferences.SuspendLayout();
            this.tabPageIndividuals.SuspendLayout();
            this.tabPageFactors.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSpecies.SuspendLayout();
            this.tabPageGears.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageEquipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetGears)).BeginInit();
            this.tabPageActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetOpening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpeningDefault)).BeginInit();
            this.tabPageStationary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdSoak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdLength)).BeginInit();
            this.tabPageStratified.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSpecies
            // 
            resources.ApplyResources(this.textBoxSpecies, "textBoxSpecies");
            // 
            // textBoxWaters
            // 
            resources.ApplyResources(this.textBoxWaters, "textBoxWaters");
            // 
            // tabPageReferences
            // 
            this.tabPageReferences.Controls.Add(this.buttonBrowseDiet);
            this.tabPageReferences.Controls.Add(this.buttonBrowseParasites);
            this.tabPageReferences.Controls.Add(this.textBoxDiet);
            this.tabPageReferences.Controls.Add(this.textBoxParasites);
            this.tabPageReferences.Controls.Add(this.label3);
            this.tabPageReferences.Controls.Add(this.buttonOpenDiet);
            this.tabPageReferences.Controls.Add(this.label7);
            this.tabPageReferences.Controls.Add(this.buttonOpenParasites);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonOpenSpecies, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.labelSpecies, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonBrowseSpecies, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.labelRef, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonOpenWaters, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonBrowseWaters, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.labelWaters, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonOpenParasites, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.label7, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonOpenDiet, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.label3, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.textBoxParasites, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.textBoxDiet, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.textBoxSpecies, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonBrowseParasites, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.buttonBrowseDiet, 0);
            this.tabPageReferences.Controls.SetChildIndex(this.textBoxWaters, 0);
            // 
            // labelSpecies
            // 
            resources.ApplyResources(this.labelSpecies, "labelSpecies");
            // 
            // buttonOpenSpecies
            // 
            resources.ApplyResources(this.buttonOpenSpecies, "buttonOpenSpecies");
            // 
            // labelWaters
            // 
            resources.ApplyResources(this.labelWaters, "labelWaters");
            // 
            // buttonOpenWaters
            // 
            resources.ApplyResources(this.buttonOpenWaters, "buttonOpenWaters");
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageGears);
            this.tabControlSettings.Controls.SetChildIndex(this.tabPagePrint, 0);
            this.tabControlSettings.Controls.SetChildIndex(this.tabPageGears, 0);
            this.tabControlSettings.Controls.SetChildIndex(this.tabPageInput, 0);
            this.tabControlSettings.Controls.SetChildIndex(this.tabPageReferences, 0);
            // 
            // buttonBrowseSpecies
            // 
            resources.ApplyResources(this.buttonBrowseSpecies, "buttonBrowseSpecies");
            // 
            // buttonBrowseWaters
            // 
            resources.ApplyResources(this.buttonBrowseWaters, "buttonBrowseWaters");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageStratified);
            this.tabControl1.Controls.SetChildIndex(this.tabPageStratified, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPageIndividuals, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPageSpecies, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPageFactors, 0);
            // 
            // checkBoxSpeciesExpandVisualControl
            // 
            resources.ApplyResources(this.checkBoxSpeciesExpandVisualControl, "checkBoxSpeciesExpandVisualControl");
            // 
            // buttonOpenParasites
            // 
            resources.ApplyResources(this.buttonOpenParasites, "buttonOpenParasites");
            this.buttonOpenParasites.Name = "buttonOpenParasites";
            this.buttonOpenParasites.UseVisualStyleBackColor = true;
            this.buttonOpenParasites.Click += new System.EventHandler(this.buttonOpenParasites_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxParasites
            // 
            resources.ApplyResources(this.textBoxParasites, "textBoxParasites");
            this.textBoxParasites.Name = "textBoxParasites";
            this.textBoxParasites.ReadOnly = true;
            // 
            // buttonBrowseParasites
            // 
            resources.ApplyResources(this.buttonBrowseParasites, "buttonBrowseParasites");
            this.buttonBrowseParasites.Name = "buttonBrowseParasites";
            this.buttonBrowseParasites.UseVisualStyleBackColor = true;
            this.buttonBrowseParasites.Click += new System.EventHandler(this.buttonBrowseParasites_Click);
            // 
            // tabPageGears
            // 
            this.tabPageGears.Controls.Add(this.tabControl2);
            resources.ApplyResources(this.tabPageGears, "tabPageGears");
            this.tabPageGears.Name = "tabPageGears";
            this.tabPageGears.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            resources.ApplyResources(this.tabControl2, "tabControl2");
            this.tabControl2.Controls.Add(this.tabPageEquipment);
            this.tabControl2.Controls.Add(this.tabPageActive);
            this.tabControl2.Controls.Add(this.tabPageStationary);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            // 
            // tabPageEquipment
            // 
            this.tabPageEquipment.Controls.Add(this.buttonGearsClear);
            this.tabPageEquipment.Controls.Add(this.spreadSheetGears);
            resources.ApplyResources(this.tabPageEquipment, "tabPageEquipment");
            this.tabPageEquipment.Name = "tabPageEquipment";
            this.tabPageEquipment.UseVisualStyleBackColor = true;
            // 
            // buttonGearsClear
            // 
            resources.ApplyResources(this.buttonGearsClear, "buttonGearsClear");
            this.buttonGearsClear.Name = "buttonGearsClear";
            this.buttonGearsClear.UseVisualStyleBackColor = true;
            this.buttonGearsClear.Click += new System.EventHandler(this.buttonGearsClear_Click);
            // 
            // spreadSheetGears
            // 
            this.spreadSheetGears.AllowUserToAddRows = true;
            this.spreadSheetGears.AllowUserToDeleteRows = true;
            resources.ApplyResources(this.spreadSheetGears, "spreadSheetGears");
            this.spreadSheetGears.AutoClearEmptyRows = true;
            this.spreadSheetGears.CellPadding = new System.Windows.Forms.Padding(0);
            this.spreadSheetGears.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSampler,
            this.columnMesh,
            this.columnLength,
            this.columnHeight});
            this.spreadSheetGears.Name = "spreadSheetGears";
            this.spreadSheetGears.RowTemplate.Height = 24;
            // 
            // columnSampler
            // 
            this.columnSampler.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSampler.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnSampler.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            resources.ApplyResources(this.columnSampler, "columnSampler");
            this.columnSampler.Name = "columnSampler";
            this.columnSampler.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnSampler.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // columnMesh
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.columnMesh.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnMesh, "columnMesh");
            this.columnMesh.Name = "columnMesh";
            // 
            // columnLength
            // 
            resources.ApplyResources(this.columnLength, "columnLength");
            this.columnLength.Name = "columnLength";
            // 
            // columnHeight
            // 
            resources.ApplyResources(this.columnHeight, "columnHeight");
            this.columnHeight.Name = "columnHeight";
            // 
            // tabPageActive
            // 
            this.tabPageActive.Controls.Add(this.spreadSheetOpening);
            this.tabPageActive.Controls.Add(this.labelOpeningDefault);
            this.tabPageActive.Controls.Add(this.numericUpDownOpeningDefault);
            this.tabPageActive.Controls.Add(this.labelActive);
            this.tabPageActive.Controls.Add(this.labelActiveInstruction);
            resources.ApplyResources(this.tabPageActive, "tabPageActive");
            this.tabPageActive.Name = "tabPageActive";
            this.tabPageActive.UseVisualStyleBackColor = true;
            // 
            // spreadSheetOpening
            // 
            this.spreadSheetOpening.AllowStringSuggection = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.spreadSheetOpening.AllowUserToAddRows = true;
            this.spreadSheetOpening.AllowUserToDeleteRows = true;
            this.spreadSheetOpening.AllowUserToResizeColumns = false;
            resources.ApplyResources(this.spreadSheetOpening, "spreadSheetOpening");
            this.spreadSheetOpening.AutoClearEmptyRows = true;
            this.spreadSheetOpening.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnOpeningGear,
            this.columnOpeningValue});
            this.spreadSheetOpening.DefaultDecimalPlaces = 3;
            this.spreadSheetOpening.Name = "spreadSheetOpening";
            this.spreadSheetOpening.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetOpening_CellEndEdit);
            // 
            // columnOpeningGear
            // 
            this.columnOpeningGear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnOpeningGear.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnOpeningGear, "columnOpeningGear");
            this.columnOpeningGear.Name = "columnOpeningGear";
            this.columnOpeningGear.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnOpeningValue
            // 
            this.columnOpeningValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Format = "P0";
            this.columnOpeningValue.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnOpeningValue, "columnOpeningValue");
            this.columnOpeningValue.Name = "columnOpeningValue";
            // 
            // labelOpeningDefault
            // 
            resources.ApplyResources(this.labelOpeningDefault, "labelOpeningDefault");
            this.labelOpeningDefault.Name = "labelOpeningDefault";
            // 
            // numericUpDownOpeningDefault
            // 
            resources.ApplyResources(this.numericUpDownOpeningDefault, "numericUpDownOpeningDefault");
            this.numericUpDownOpeningDefault.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownOpeningDefault.Name = "numericUpDownOpeningDefault";
            this.numericUpDownOpeningDefault.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelActive
            // 
            resources.ApplyResources(this.labelActive, "labelActive");
            this.labelActive.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelActive.Name = "labelActive";
            // 
            // labelActiveInstruction
            // 
            resources.ApplyResources(this.labelActiveInstruction, "labelActiveInstruction");
            this.labelActiveInstruction.Name = "labelActiveInstruction";
            // 
            // tabPageStationary
            // 
            this.tabPageStationary.Controls.Add(this.labelStationary);
            this.tabPageStationary.Controls.Add(this.labelExposure);
            this.tabPageStationary.Controls.Add(this.labelStationaryInstruction);
            this.tabPageStationary.Controls.Add(this.labelHeight);
            this.tabPageStationary.Controls.Add(this.labelLength);
            this.tabPageStationary.Controls.Add(this.numericUpDownStdSoak);
            this.tabPageStationary.Controls.Add(this.numericUpDownStdHeight);
            this.tabPageStationary.Controls.Add(this.numericUpDownStdLength);
            resources.ApplyResources(this.tabPageStationary, "tabPageStationary");
            this.tabPageStationary.Name = "tabPageStationary";
            this.tabPageStationary.UseVisualStyleBackColor = true;
            // 
            // labelStationary
            // 
            resources.ApplyResources(this.labelStationary, "labelStationary");
            this.labelStationary.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStationary.Name = "labelStationary";
            // 
            // labelExposure
            // 
            resources.ApplyResources(this.labelExposure, "labelExposure");
            this.labelExposure.Name = "labelExposure";
            // 
            // labelStationaryInstruction
            // 
            resources.ApplyResources(this.labelStationaryInstruction, "labelStationaryInstruction");
            this.labelStationaryInstruction.Name = "labelStationaryInstruction";
            // 
            // labelHeight
            // 
            resources.ApplyResources(this.labelHeight, "labelHeight");
            this.labelHeight.Name = "labelHeight";
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
            // 
            // numericUpDownStdSoak
            // 
            resources.ApplyResources(this.numericUpDownStdSoak, "numericUpDownStdSoak");
            this.numericUpDownStdSoak.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.numericUpDownStdSoak.Name = "numericUpDownStdSoak";
            // 
            // numericUpDownStdHeight
            // 
            resources.ApplyResources(this.numericUpDownStdHeight, "numericUpDownStdHeight");
            this.numericUpDownStdHeight.DecimalPlaces = 2;
            this.numericUpDownStdHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownStdHeight.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStdHeight.Name = "numericUpDownStdHeight";
            // 
            // numericUpDownStdLength
            // 
            resources.ApplyResources(this.numericUpDownStdLength, "numericUpDownStdLength");
            this.numericUpDownStdLength.DecimalPlaces = 2;
            this.numericUpDownStdLength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownStdLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownStdLength.Name = "numericUpDownStdLength";
            // 
            // tabPageStratified
            // 
            this.tabPageStratified.Controls.Add(this.labelInterval);
            this.tabPageStratified.Controls.Add(this.numericUpDownInterval);
            this.tabPageStratified.Controls.Add(this.labelStratified);
            resources.ApplyResources(this.tabPageStratified, "tabPageStratified");
            this.tabPageStratified.Name = "tabPageStratified";
            this.tabPageStratified.UseVisualStyleBackColor = true;
            // 
            // labelInterval
            // 
            resources.ApplyResources(this.labelInterval, "labelInterval");
            this.labelInterval.Name = "labelInterval";
            // 
            // numericUpDownInterval
            // 
            resources.ApplyResources(this.numericUpDownInterval, "numericUpDownInterval");
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelStratified
            // 
            resources.ApplyResources(this.labelStratified, "labelStratified");
            this.labelStratified.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStratified.Name = "labelStratified";
            // 
            // buttonOpenDiet
            // 
            resources.ApplyResources(this.buttonOpenDiet, "buttonOpenDiet");
            this.buttonOpenDiet.Name = "buttonOpenDiet";
            this.buttonOpenDiet.UseVisualStyleBackColor = true;
            this.buttonOpenDiet.Click += new System.EventHandler(this.buttonOpenDiet_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxDiet
            // 
            resources.ApplyResources(this.textBoxDiet, "textBoxDiet");
            this.textBoxDiet.Name = "textBoxDiet";
            this.textBoxDiet.ReadOnly = true;
            // 
            // buttonBrowseDiet
            // 
            resources.ApplyResources(this.buttonBrowseDiet, "buttonBrowseDiet");
            this.buttonBrowseDiet.Name = "buttonBrowseDiet";
            this.buttonBrowseDiet.UseVisualStyleBackColor = true;
            this.buttonBrowseDiet.Click += new System.EventHandler(this.buttonBrowseDiet_Click);
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).EndInit();
            this.tabPageReferences.ResumeLayout(false);
            this.tabPageReferences.PerformLayout();
            this.tabPageIndividuals.ResumeLayout(false);
            this.tabPageIndividuals.PerformLayout();
            this.tabPageFactors.ResumeLayout(false);
            this.tabPageFactors.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabPagePrint.ResumeLayout(false);
            this.tabPagePrint.PerformLayout();
            this.tabPageInput.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSpecies.ResumeLayout(false);
            this.tabPageSpecies.PerformLayout();
            this.tabPageGears.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageEquipment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetGears)).EndInit();
            this.tabPageActive.ResumeLayout(false);
            this.tabPageActive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetOpening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpeningDefault)).EndInit();
            this.tabPageStationary.ResumeLayout(false);
            this.tabPageStationary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdSoak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStdLength)).EndInit();
            this.tabPageStratified.ResumeLayout(false);
            this.tabPageStratified.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.ResumeLayout(false);

        }



        private void LoadOpening(Samplers.SamplerRow samplerRow)
        {
            double open = Service.DefaultOpening(samplerRow.ID);
            if (double.IsNaN(open)) return;
            if (open == Service.DefaultOpening()) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetOpening);
            gridRow.Cells[columnOpeningGear.Index].Value = samplerRow.Sampler;
            gridRow.Cells[columnOpeningValue.Index].Value = open;
            gridRow.Tag = samplerRow;
            spreadSheetOpening.Rows.Add(gridRow);
            
            LoadGears();
        }

        private void LoadGears()
        {
            spreadSheetGears.Rows.Clear();

            foreach (Equipment.UnitsRow unitRow in UserSettings.Equipment.Units)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGears);
                if (!unitRow.IsSamplerIDNull()) gridRow.Cells[columnSampler.Index].Value = ReaderSettings.SamplersIndex.Sampler.FindByID(unitRow.SamplerID).ShortName;
                if (!unitRow.IsMeshNull()) gridRow.Cells[columnMesh.Index].Value = unitRow.Mesh;
                if (!unitRow.IsLengthNull()) gridRow.Cells[columnLength.Index].Value = unitRow.Length;
                if (!unitRow.IsHeightNull()) gridRow.Cells[columnHeight.Index].Value = unitRow.Height;
                spreadSheetGears.Rows.Add(gridRow);
            }
        }



        private void buttonBrowseDiet_Click(object sender, EventArgs e)
        {
            if (Mayfly.Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                UserSettings.DietIndexPath = Species.UserSettings.Interface.OpenDialog.FileName;
                textBoxDiet.Text = UserSettings.DietIndexPath;
            }
        }

        private void buttonOpenDiet_Click(object sender, EventArgs e)
        {
            IO.RunFile(textBoxDiet.Text);
        }

        private void buttonBrowseParasites_Click(object sender, EventArgs e)
        {
            if (Mayfly.Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                Fish.UserSettings.ParasitesIndexPath = Mayfly.Species.UserSettings.Interface.OpenDialog.FileName;
                textBoxParasites.Text = Fish.UserSettings.ParasitesIndexPath;
            }
        }

        private void buttonOpenParasites_Click(object sender, EventArgs e)
        {
            IO.RunFile(textBoxParasites.Text);
        }

        private void buttonGearsClear_Click(object sender, EventArgs e)
        {
            spreadSheetGears.Rows.Clear();
        }

        private void spreadSheetOpening_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object value = spreadSheetOpening[e.ColumnIndex, e.RowIndex].Value;

            if (value == null)
            {
                spreadSheetOpening.Rows[e.RowIndex].Tag = null;
                return;
            }

            if (e.ColumnIndex == columnOpeningGear.Index)
            {
                Samplers.SamplerRow samplerRow = ReaderSettings.SamplersIndex.Sampler.FindBySampler((string)value);
                spreadSheetOpening.Rows[e.RowIndex].Tag = samplerRow;
            }
        }
    }
}
