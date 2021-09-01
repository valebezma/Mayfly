namespace Mayfly.Fish.Explorer
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTreat = new System.Windows.Forms.TabPage();
            this.comboBoxAlk = new System.Windows.Forms.ComboBox();
            this.AlkTypeLabel = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSizeInterval = new System.Windows.Forms.Label();
            this.comboBoxDominance = new System.Windows.Forms.ComboBox();
            this.labelDominance = new System.Windows.Forms.Label();
            this.comboBoxDiversity = new System.Windows.Forms.ComboBox();
            this.labelDiversity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPagePrediction = new System.Windows.Forms.TabPage();
            this.listViewBio = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonBioRemove = new System.Windows.Forms.Button();
            this.buttonBioBrowse = new System.Windows.Forms.Button();
            this.checkBoxBioAutoLoad = new System.Windows.Forms.CheckBox();
            this.checkBoxSuggestAge = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxSuggestMass = new System.Windows.Forms.CheckBox();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.comboBoxReportCriticality = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxConsistency = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepWizards = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPageGamingAge = new System.Windows.Forms.TabPage();
            this.spreadSheetAge = new Mayfly.Controls.SpreadSheet();
            this.ColumnAgeSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAgeValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelAgeInstrcution = new System.Windows.Forms.Label();
            this.labelAgeTitle = new System.Windows.Forms.Label();
            this.tabPageGamingMeasure = new System.Windows.Forms.TabPage();
            this.spreadSheetMeasure = new Mayfly.Controls.SpreadSheet();
            this.ColumnMeasureSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMeasureValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelMeasureInstruction = new System.Windows.Forms.Label();
            this.labelMeasureTitle = new System.Windows.Forms.Label();
            this.tabPageCatchability = new System.Windows.Forms.TabPage();
            this.spreadSheetCatchability = new Mayfly.Controls.SpreadSheet();
            this.columnCatchabilitySpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCatchabilityValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelGear = new System.Windows.Forms.Label();
            this.comboBoxGear = new System.Windows.Forms.ComboBox();
            this.numericUpDownCatchabilityDefault = new System.Windows.Forms.NumericUpDown();
            this.labelCatchabilityDefault = new System.Windows.Forms.Label();
            this.labelCatchabilityTitle = new System.Windows.Forms.Label();
            this.labelCatchabilityInstruction = new System.Windows.Forms.Label();
            this.tabPageOther = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelFish = new System.Windows.Forms.Label();
            this.buttonMath = new System.Windows.Forms.Button();
            this.buttonProductSettings = new System.Windows.Forms.Button();
            this.buttonFish = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.speciesSelectorMeasure = new Mayfly.Species.SpeciesSelector(this.components);
            this.speciesSelectorCatchability = new Mayfly.Species.SpeciesSelector(this.components);
            this.speciesSelectorAge = new Mayfly.Species.SpeciesSelector(this.components);
            this.buttonBasicSettings = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageTreat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.tabPagePrediction.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.tabPageGamingAge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAge)).BeginInit();
            this.tabPageGamingMeasure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMeasure)).BeginInit();
            this.tabPageCatchability.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatchability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCatchabilityDefault)).BeginInit();
            this.tabPageOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageTreat);
            this.tabControl.Controls.Add(this.tabPagePrediction);
            this.tabControl.Controls.Add(this.tabPageAdvanced);
            this.tabControl.Controls.Add(this.tabPageGamingAge);
            this.tabControl.Controls.Add(this.tabPageGamingMeasure);
            this.tabControl.Controls.Add(this.tabPageCatchability);
            this.tabControl.Controls.Add(this.tabPageOther);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            // 
            // tabPageTreat
            // 
            this.tabPageTreat.Controls.Add(this.comboBoxAlk);
            this.tabPageTreat.Controls.Add(this.AlkTypeLabel);
            this.tabPageTreat.Controls.Add(this.numericUpDownInterval);
            this.tabPageTreat.Controls.Add(this.labelSizeInterval);
            this.tabPageTreat.Controls.Add(this.comboBoxDominance);
            this.tabPageTreat.Controls.Add(this.labelDominance);
            this.tabPageTreat.Controls.Add(this.comboBoxDiversity);
            this.tabPageTreat.Controls.Add(this.labelDiversity);
            this.tabPageTreat.Controls.Add(this.label4);
            resources.ApplyResources(this.tabPageTreat, "tabPageTreat");
            this.tabPageTreat.Name = "tabPageTreat";
            this.tabPageTreat.UseVisualStyleBackColor = true;
            // 
            // comboBoxAlk
            // 
            resources.ApplyResources(this.comboBoxAlk, "comboBoxAlk");
            this.comboBoxAlk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlk.FormattingEnabled = true;
            this.comboBoxAlk.Items.AddRange(new object[] {
            resources.GetString("comboBoxAlk.Items"),
            resources.GetString("comboBoxAlk.Items1")});
            this.comboBoxAlk.Name = "comboBoxAlk";
            // 
            // AlkTypeLabel
            // 
            resources.ApplyResources(this.AlkTypeLabel, "AlkTypeLabel");
            this.AlkTypeLabel.Name = "AlkTypeLabel";
            // 
            // numericUpDownInterval
            // 
            resources.ApplyResources(this.numericUpDownInterval, "numericUpDownInterval");
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            // 
            // labelSizeInterval
            // 
            resources.ApplyResources(this.labelSizeInterval, "labelSizeInterval");
            this.labelSizeInterval.Name = "labelSizeInterval";
            // 
            // comboBoxDominance
            // 
            resources.ApplyResources(this.comboBoxDominance, "comboBoxDominance");
            this.comboBoxDominance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDominance.FormattingEnabled = true;
            this.comboBoxDominance.Items.AddRange(new object[] {
            resources.GetString("comboBoxDominance.Items"),
            resources.GetString("comboBoxDominance.Items1"),
            resources.GetString("comboBoxDominance.Items2"),
            resources.GetString("comboBoxDominance.Items3"),
            resources.GetString("comboBoxDominance.Items4"),
            resources.GetString("comboBoxDominance.Items5"),
            resources.GetString("comboBoxDominance.Items6"),
            resources.GetString("comboBoxDominance.Items7"),
            resources.GetString("comboBoxDominance.Items8"),
            resources.GetString("comboBoxDominance.Items9"),
            resources.GetString("comboBoxDominance.Items10"),
            resources.GetString("comboBoxDominance.Items11"),
            resources.GetString("comboBoxDominance.Items12"),
            resources.GetString("comboBoxDominance.Items13"),
            resources.GetString("comboBoxDominance.Items14"),
            resources.GetString("comboBoxDominance.Items15"),
            resources.GetString("comboBoxDominance.Items16")});
            this.comboBoxDominance.Name = "comboBoxDominance";
            // 
            // labelDominance
            // 
            resources.ApplyResources(this.labelDominance, "labelDominance");
            this.labelDominance.Name = "labelDominance";
            // 
            // comboBoxDiversity
            // 
            resources.ApplyResources(this.comboBoxDiversity, "comboBoxDiversity");
            this.comboBoxDiversity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiversity.FormattingEnabled = true;
            this.comboBoxDiversity.Name = "comboBoxDiversity";
            // 
            // labelDiversity
            // 
            resources.ApplyResources(this.labelDiversity, "labelDiversity");
            this.labelDiversity.Name = "labelDiversity";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Name = "label4";
            // 
            // tabPagePrediction
            // 
            this.tabPagePrediction.Controls.Add(this.listViewBio);
            this.tabPagePrediction.Controls.Add(this.buttonBioRemove);
            this.tabPagePrediction.Controls.Add(this.buttonBioBrowse);
            this.tabPagePrediction.Controls.Add(this.checkBoxBioAutoLoad);
            this.tabPagePrediction.Controls.Add(this.checkBoxSuggestAge);
            this.tabPagePrediction.Controls.Add(this.label7);
            this.tabPagePrediction.Controls.Add(this.label2);
            this.tabPagePrediction.Controls.Add(this.checkBoxSuggestMass);
            resources.ApplyResources(this.tabPagePrediction, "tabPagePrediction");
            this.tabPagePrediction.Name = "tabPagePrediction";
            this.tabPagePrediction.UseVisualStyleBackColor = true;
            // 
            // listViewBio
            // 
            resources.ApplyResources(this.listViewBio, "listViewBio");
            this.listViewBio.CheckBoxes = true;
            this.listViewBio.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName});
            this.listViewBio.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewBio.HideSelection = false;
            this.listViewBio.LabelEdit = true;
            this.listViewBio.Name = "listViewBio";
            this.listViewBio.ShowGroups = false;
            this.listViewBio.UseCompatibleStateImageBehavior = false;
            this.listViewBio.View = System.Windows.Forms.View.Details;
            this.listViewBio.SelectedIndexChanged += new System.EventHandler(this.listViewBio_SelectedIndexChanged);
            // 
            // columnFileName
            // 
            resources.ApplyResources(this.columnFileName, "columnFileName");
            // 
            // buttonBioRemove
            // 
            resources.ApplyResources(this.buttonBioRemove, "buttonBioRemove");
            this.buttonBioRemove.Name = "buttonBioRemove";
            this.buttonBioRemove.UseVisualStyleBackColor = true;
            this.buttonBioRemove.Click += new System.EventHandler(this.buttonBioRemove_Click);
            // 
            // buttonBioBrowse
            // 
            resources.ApplyResources(this.buttonBioBrowse, "buttonBioBrowse");
            this.buttonBioBrowse.Name = "buttonBioBrowse";
            this.buttonBioBrowse.UseVisualStyleBackColor = true;
            this.buttonBioBrowse.Click += new System.EventHandler(this.buttonBioBrowse_Click);
            // 
            // checkBoxBioAutoLoad
            // 
            resources.ApplyResources(this.checkBoxBioAutoLoad, "checkBoxBioAutoLoad");
            this.checkBoxBioAutoLoad.Name = "checkBoxBioAutoLoad";
            this.checkBoxBioAutoLoad.UseVisualStyleBackColor = true;
            this.checkBoxBioAutoLoad.CheckedChanged += new System.EventHandler(this.checkBoxBioAutoLoad_CheckedChanged);
            // 
            // checkBoxSuggestAge
            // 
            resources.ApplyResources(this.checkBoxSuggestAge, "checkBoxSuggestAge");
            this.checkBoxSuggestAge.Name = "checkBoxSuggestAge";
            this.checkBoxSuggestAge.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label7.Name = "label7";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // checkBoxSuggestMass
            // 
            resources.ApplyResources(this.checkBoxSuggestMass, "checkBoxSuggestMass");
            this.checkBoxSuggestMass.Name = "checkBoxSuggestMass";
            this.checkBoxSuggestMass.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.comboBoxReportCriticality);
            this.tabPageAdvanced.Controls.Add(this.label3);
            this.tabPageAdvanced.Controls.Add(this.checkBoxConsistency);
            this.tabPageAdvanced.Controls.Add(this.checkBoxKeepWizards);
            this.tabPageAdvanced.Controls.Add(this.label5);
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // comboBoxReportCriticality
            // 
            resources.ApplyResources(this.comboBoxReportCriticality, "comboBoxReportCriticality");
            this.comboBoxReportCriticality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReportCriticality.FormattingEnabled = true;
            this.comboBoxReportCriticality.Items.AddRange(new object[] {
            resources.GetString("comboBoxReportCriticality.Items"),
            resources.GetString("comboBoxReportCriticality.Items1"),
            resources.GetString("comboBoxReportCriticality.Items2"),
            resources.GetString("comboBoxReportCriticality.Items3")});
            this.comboBoxReportCriticality.Name = "comboBoxReportCriticality";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // checkBoxConsistency
            // 
            resources.ApplyResources(this.checkBoxConsistency, "checkBoxConsistency");
            this.checkBoxConsistency.Name = "checkBoxConsistency";
            this.checkBoxConsistency.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeepWizards
            // 
            resources.ApplyResources(this.checkBoxKeepWizards, "checkBoxKeepWizards");
            this.checkBoxKeepWizards.Name = "checkBoxKeepWizards";
            this.checkBoxKeepWizards.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.Name = "label5";
            // 
            // tabPageGamingAge
            // 
            this.tabPageGamingAge.Controls.Add(this.spreadSheetAge);
            this.tabPageGamingAge.Controls.Add(this.labelAgeInstrcution);
            this.tabPageGamingAge.Controls.Add(this.labelAgeTitle);
            resources.ApplyResources(this.tabPageGamingAge, "tabPageGamingAge");
            this.tabPageGamingAge.Name = "tabPageGamingAge";
            this.tabPageGamingAge.UseVisualStyleBackColor = true;
            // 
            // spreadSheetAge
            // 
            this.spreadSheetAge.AllowUserToAddRows = true;
            this.spreadSheetAge.AllowUserToDeleteRows = true;
            this.spreadSheetAge.AllowUserToResizeColumns = false;
            resources.ApplyResources(this.spreadSheetAge, "spreadSheetAge");
            this.spreadSheetAge.AutoClearEmptyRows = true;
            this.spreadSheetAge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnAgeSpecies,
            this.ColumnAgeValue});
            this.spreadSheetAge.DefaultDecimalPlaces = 0;
            this.spreadSheetAge.Name = "spreadSheetAge";
            // 
            // ColumnAgeSpecies
            // 
            this.ColumnAgeSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnAgeSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnAgeSpecies, "ColumnAgeSpecies");
            this.ColumnAgeSpecies.Name = "ColumnAgeSpecies";
            // 
            // ColumnAgeValue
            // 
            this.ColumnAgeValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(this.ColumnAgeValue, "ColumnAgeValue");
            this.ColumnAgeValue.Name = "ColumnAgeValue";
            // 
            // labelAgeInstrcution
            // 
            resources.ApplyResources(this.labelAgeInstrcution, "labelAgeInstrcution");
            this.labelAgeInstrcution.Name = "labelAgeInstrcution";
            // 
            // labelAgeTitle
            // 
            resources.ApplyResources(this.labelAgeTitle, "labelAgeTitle");
            this.labelAgeTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelAgeTitle.Name = "labelAgeTitle";
            // 
            // tabPageGamingMeasure
            // 
            this.tabPageGamingMeasure.Controls.Add(this.spreadSheetMeasure);
            this.tabPageGamingMeasure.Controls.Add(this.labelMeasureInstruction);
            this.tabPageGamingMeasure.Controls.Add(this.labelMeasureTitle);
            resources.ApplyResources(this.tabPageGamingMeasure, "tabPageGamingMeasure");
            this.tabPageGamingMeasure.Name = "tabPageGamingMeasure";
            this.tabPageGamingMeasure.UseVisualStyleBackColor = true;
            // 
            // spreadSheetMeasure
            // 
            this.spreadSheetMeasure.AllowUserToAddRows = true;
            this.spreadSheetMeasure.AllowUserToDeleteRows = true;
            this.spreadSheetMeasure.AllowUserToResizeColumns = false;
            resources.ApplyResources(this.spreadSheetMeasure, "spreadSheetMeasure");
            this.spreadSheetMeasure.AutoClearEmptyRows = true;
            this.spreadSheetMeasure.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnMeasureSpecies,
            this.ColumnMeasureValue});
            this.spreadSheetMeasure.DefaultDecimalPlaces = 0;
            this.spreadSheetMeasure.Name = "spreadSheetMeasure";
            // 
            // ColumnMeasureSpecies
            // 
            this.ColumnMeasureSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnMeasureSpecies.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnMeasureSpecies, "ColumnMeasureSpecies");
            this.ColumnMeasureSpecies.Name = "ColumnMeasureSpecies";
            // 
            // ColumnMeasureValue
            // 
            this.ColumnMeasureValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(this.ColumnMeasureValue, "ColumnMeasureValue");
            this.ColumnMeasureValue.Name = "ColumnMeasureValue";
            // 
            // labelMeasureInstruction
            // 
            resources.ApplyResources(this.labelMeasureInstruction, "labelMeasureInstruction");
            this.labelMeasureInstruction.Name = "labelMeasureInstruction";
            // 
            // labelMeasureTitle
            // 
            resources.ApplyResources(this.labelMeasureTitle, "labelMeasureTitle");
            this.labelMeasureTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelMeasureTitle.Name = "labelMeasureTitle";
            // 
            // tabPageCatchability
            // 
            this.tabPageCatchability.Controls.Add(this.spreadSheetCatchability);
            this.tabPageCatchability.Controls.Add(this.labelGear);
            this.tabPageCatchability.Controls.Add(this.comboBoxGear);
            this.tabPageCatchability.Controls.Add(this.numericUpDownCatchabilityDefault);
            this.tabPageCatchability.Controls.Add(this.labelCatchabilityDefault);
            this.tabPageCatchability.Controls.Add(this.labelCatchabilityTitle);
            this.tabPageCatchability.Controls.Add(this.labelCatchabilityInstruction);
            resources.ApplyResources(this.tabPageCatchability, "tabPageCatchability");
            this.tabPageCatchability.Name = "tabPageCatchability";
            this.tabPageCatchability.UseVisualStyleBackColor = true;
            // 
            // spreadSheetCatchability
            // 
            this.spreadSheetCatchability.AllowUserToAddRows = true;
            this.spreadSheetCatchability.AllowUserToDeleteRows = true;
            this.spreadSheetCatchability.AllowUserToResizeColumns = false;
            resources.ApplyResources(this.spreadSheetCatchability, "spreadSheetCatchability");
            this.spreadSheetCatchability.AutoClearEmptyRows = true;
            this.spreadSheetCatchability.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCatchabilitySpecies,
            this.columnCatchabilityValue});
            this.spreadSheetCatchability.DefaultDecimalPlaces = 3;
            this.spreadSheetCatchability.Name = "spreadSheetCatchability";
            this.spreadSheetCatchability.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCatchability_CellEndEdit);
            // 
            // columnCatchabilitySpecies
            // 
            this.columnCatchabilitySpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = null;
            this.columnCatchabilitySpecies.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnCatchabilitySpecies, "columnCatchabilitySpecies");
            this.columnCatchabilitySpecies.Name = "columnCatchabilitySpecies";
            // 
            // columnCatchabilityValue
            // 
            this.columnCatchabilityValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(this.columnCatchabilityValue, "columnCatchabilityValue");
            this.columnCatchabilityValue.Name = "columnCatchabilityValue";
            // 
            // labelGear
            // 
            resources.ApplyResources(this.labelGear, "labelGear");
            this.labelGear.Name = "labelGear";
            // 
            // comboBoxGear
            // 
            resources.ApplyResources(this.comboBoxGear, "comboBoxGear");
            this.comboBoxGear.DisplayMember = "Sampler";
            this.comboBoxGear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGear.FormattingEnabled = true;
            this.comboBoxGear.Name = "comboBoxGear";
            this.comboBoxGear.Sorted = true;
            this.comboBoxGear.SelectedIndexChanged += new System.EventHandler(this.comboBoxGear_SelectedIndexChanged);
            // 
            // numericUpDownCatchabilityDefault
            // 
            resources.ApplyResources(this.numericUpDownCatchabilityDefault, "numericUpDownCatchabilityDefault");
            this.numericUpDownCatchabilityDefault.DecimalPlaces = 3;
            this.numericUpDownCatchabilityDefault.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCatchabilityDefault.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCatchabilityDefault.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownCatchabilityDefault.Name = "numericUpDownCatchabilityDefault";
            this.numericUpDownCatchabilityDefault.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // labelCatchabilityDefault
            // 
            resources.ApplyResources(this.labelCatchabilityDefault, "labelCatchabilityDefault");
            this.labelCatchabilityDefault.Name = "labelCatchabilityDefault";
            // 
            // labelCatchabilityTitle
            // 
            resources.ApplyResources(this.labelCatchabilityTitle, "labelCatchabilityTitle");
            this.labelCatchabilityTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelCatchabilityTitle.Name = "labelCatchabilityTitle";
            // 
            // labelCatchabilityInstruction
            // 
            resources.ApplyResources(this.labelCatchabilityInstruction, "labelCatchabilityInstruction");
            this.labelCatchabilityInstruction.Name = "labelCatchabilityInstruction";
            // 
            // tabPageOther
            // 
            this.tabPageOther.Controls.Add(this.label1);
            this.tabPageOther.Controls.Add(this.label6);
            this.tabPageOther.Controls.Add(this.labelFish);
            this.tabPageOther.Controls.Add(this.buttonMath);
            this.tabPageOther.Controls.Add(this.buttonProductSettings);
            this.tabPageOther.Controls.Add(this.buttonFish);
            resources.ApplyResources(this.tabPageOther, "tabPageOther");
            this.tabPageOther.Name = "tabPageOther";
            this.tabPageOther.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // labelFish
            // 
            resources.ApplyResources(this.labelFish, "labelFish");
            this.labelFish.Name = "labelFish";
            // 
            // buttonMath
            // 
            resources.ApplyResources(this.buttonMath, "buttonMath");
            this.buttonMath.Name = "buttonMath";
            this.buttonMath.UseVisualStyleBackColor = true;
            this.buttonMath.Click += new System.EventHandler(this.buttonMath_Click);
            // 
            // buttonProductSettings
            // 
            resources.ApplyResources(this.buttonProductSettings, "buttonProductSettings");
            this.buttonProductSettings.Name = "buttonProductSettings";
            this.buttonProductSettings.UseVisualStyleBackColor = true;
            this.buttonProductSettings.Click += new System.EventHandler(this.buttonBasicSettings_Click);
            // 
            // buttonFish
            // 
            resources.ApplyResources(this.buttonFish, "buttonFish");
            this.buttonFish.Name = "buttonFish";
            this.buttonFish.UseVisualStyleBackColor = true;
            this.buttonFish.Click += new System.EventHandler(this.buttonFish_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // speciesSelectorMeasure
            // 
            this.speciesSelectorMeasure.AllowKey = false;
            this.speciesSelectorMeasure.CheckDuplicates = false;
            this.speciesSelectorMeasure.ColumnName = "ColumnMeasureSpecies";
            this.speciesSelectorMeasure.Grid = this.spreadSheetMeasure;
            this.speciesSelectorMeasure.RecentListCount = 0;
            // 
            // speciesSelectorCatchability
            // 
            this.speciesSelectorCatchability.AllowKey = false;
            this.speciesSelectorCatchability.CheckDuplicates = false;
            this.speciesSelectorCatchability.ColumnName = "columnCatchabilitySpecies";
            this.speciesSelectorCatchability.Grid = this.spreadSheetCatchability;
            this.speciesSelectorCatchability.RecentListCount = 0;
            // 
            // speciesSelectorAge
            // 
            this.speciesSelectorAge.AllowKey = false;
            this.speciesSelectorAge.CheckDuplicates = false;
            this.speciesSelectorAge.ColumnName = "ColumnAgeSpecies";
            this.speciesSelectorAge.Grid = this.spreadSheetAge;
            this.speciesSelectorAge.RecentListCount = 0;
            // 
            // buttonBasicSettings
            // 
            resources.ApplyResources(this.buttonBasicSettings, "buttonBasicSettings");
            this.buttonBasicSettings.Name = "buttonBasicSettings";
            this.buttonBasicSettings.UseVisualStyleBackColor = true;
            this.buttonBasicSettings.Click += new System.EventHandler(this.buttonBasicSettings_Click);
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonBasicSettings);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControl.ResumeLayout(false);
            this.tabPageTreat.ResumeLayout(false);
            this.tabPageTreat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.tabPagePrediction.ResumeLayout(false);
            this.tabPagePrediction.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            this.tabPageGamingAge.ResumeLayout(false);
            this.tabPageGamingAge.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAge)).EndInit();
            this.tabPageGamingMeasure.ResumeLayout(false);
            this.tabPageGamingMeasure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMeasure)).EndInit();
            this.tabPageCatchability.ResumeLayout(false);
            this.tabPageCatchability.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatchability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCatchabilityDefault)).EndInit();
            this.tabPageOther.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGamingAge;
        private System.Windows.Forms.Label labelAgeInstrcution;
        private System.Windows.Forms.Label labelAgeTitle;
        private System.Windows.Forms.TabPage tabPageGamingMeasure;
        private System.Windows.Forms.Label labelMeasureInstruction;
        private System.Windows.Forms.Label labelMeasureTitle;
        private System.Windows.Forms.TabPage tabPageCatchability;
        private System.Windows.Forms.NumericUpDown numericUpDownCatchabilityDefault;
        private System.Windows.Forms.Label labelCatchabilityDefault;
        private System.Windows.Forms.Label labelCatchabilityTitle;
        private System.Windows.Forms.Label labelCatchabilityInstruction;
        private System.Windows.Forms.Label labelGear;
        public System.Windows.Forms.ComboBox comboBoxGear;
        private Mayfly.Controls.SpreadSheet spreadSheetAge;
        private Mayfly.Controls.SpreadSheet spreadSheetMeasure;
        private Mayfly.Controls.SpreadSheet spreadSheetCatchability;
        private Species.SpeciesSelector speciesSelectorMeasure;
        private Species.SpeciesSelector speciesSelectorCatchability;
        private Species.SpeciesSelector speciesSelectorAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAgeSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAgeValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasureSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMeasureValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCatchabilitySpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCatchabilityValue;
        private System.Windows.Forms.TabPage tabPageOther;
        private System.Windows.Forms.Button buttonMath;
        private System.Windows.Forms.Button buttonFish;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFish;
        private System.Windows.Forms.TabPage tabPageTreat;
        private System.Windows.Forms.ComboBox comboBoxDominance;
        private System.Windows.Forms.Label labelDominance;
        private System.Windows.Forms.ComboBox comboBoxDiversity;
        private System.Windows.Forms.Label labelDiversity;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelSizeInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPagePrediction;
        private System.Windows.Forms.Button buttonBioBrowse;
        private System.Windows.Forms.CheckBox checkBoxBioAutoLoad;
        private System.Windows.Forms.CheckBox checkBoxSuggestAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxSuggestMass;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.CheckBox checkBoxKeepWizards;
        private System.Windows.Forms.Label label5;
        protected System.Windows.Forms.Button buttonBasicSettings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonProductSettings;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.ListView listViewBio;
        private System.Windows.Forms.Button buttonBioRemove;
        private System.Windows.Forms.ComboBox comboBoxAlk;
        private System.Windows.Forms.Label AlkTypeLabel;
        private System.Windows.Forms.CheckBox checkBoxConsistency;
        private System.Windows.Forms.ComboBox comboBoxReportCriticality;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
    }
}