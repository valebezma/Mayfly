namespace Mayfly.Wild
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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageReferences = new System.Windows.Forms.TabPage();
            this.labelWaters = new System.Windows.Forms.Label();
            this.buttonBrowseWaters = new System.Windows.Forms.Button();
            this.textBoxWaters = new System.Windows.Forms.TextBox();
            this.buttonOpenWaters = new System.Windows.Forms.Button();
            this.labelRef = new System.Windows.Forms.Label();
            this.buttonBrowseSpecies = new System.Windows.Forms.Button();
            this.textBoxSpecies = new System.Windows.Forms.TextBox();
            this.labelSpecies = new System.Windows.Forms.Label();
            this.buttonOpenSpecies = new System.Windows.Forms.Button();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSpecies = new System.Windows.Forms.TabPage();
            this.checkBoxSpeciesExpand = new System.Windows.Forms.CheckBox();
            this.checkBoxSpeciesExpandVisualControl = new System.Windows.Forms.CheckBox();
            this.buttonClearRecent = new System.Windows.Forms.Button();
            this.labelRecent = new System.Windows.Forms.Label();
            this.numericUpDownRecentCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoLog = new System.Windows.Forms.CheckBox();
            this.labelInputFish = new System.Windows.Forms.Label();
            this.tabPageIndividuals = new System.Windows.Forms.TabPage();
            this.listViewAddtVars = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonRemoveVar = new System.Windows.Forms.Button();
            this.buttonNewVar = new System.Windows.Forms.Button();
            this.checkBoxAutoDecreaseBio = new System.Windows.Forms.CheckBox();
            this.checkBoxFixTotals = new System.Windows.Forms.CheckBox();
            this.labelAddtsVars = new System.Windows.Forms.Label();
            this.labelCommonVars = new System.Windows.Forms.Label();
            this.checkBoxAutoIncreaseBio = new System.Windows.Forms.CheckBox();
            this.tabPageFactors = new System.Windows.Forms.TabPage();
            this.listViewAddtFctr = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddtFctrsDelete = new System.Windows.Forms.Button();
            this.buttonAddtFctrsAdd = new System.Windows.Forms.Button();
            this.labelAddtFctrs = new System.Windows.Forms.Label();
            this.tabPagePrint = new System.Windows.Forms.TabPage();
            this.comboBoxLogOrder = new System.Windows.Forms.ComboBox();
            this.checkBoxBreakBetweenSpecies = new System.Windows.Forms.CheckBox();
            this.checkBoxOrderLog = new System.Windows.Forms.CheckBox();
            this.checkBoxCardOdd = new System.Windows.Forms.CheckBox();
            this.checkBoxBreakBeforeIndividuals = new System.Windows.Forms.CheckBox();
            this.labelPrintCaption = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tdClearRecent = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbRecentClear = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbRecentCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.buttonBasicSettings = new System.Windows.Forms.Button();
            this.tabControlSettings.SuspendLayout();
            this.tabPageReferences.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSpecies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).BeginInit();
            this.tabPageIndividuals.SuspendLayout();
            this.tabPageFactors.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageReferences);
            this.tabControlSettings.Controls.Add(this.tabPageInput);
            this.tabControlSettings.Controls.Add(this.tabPagePrint);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageReferences
            // 
            resources.ApplyResources(this.tabPageReferences, "tabPageReferences");
            this.tabPageReferences.Controls.Add(this.labelWaters);
            this.tabPageReferences.Controls.Add(this.buttonBrowseWaters);
            this.tabPageReferences.Controls.Add(this.textBoxWaters);
            this.tabPageReferences.Controls.Add(this.buttonOpenWaters);
            this.tabPageReferences.Controls.Add(this.labelRef);
            this.tabPageReferences.Controls.Add(this.buttonBrowseSpecies);
            this.tabPageReferences.Controls.Add(this.textBoxSpecies);
            this.tabPageReferences.Controls.Add(this.labelSpecies);
            this.tabPageReferences.Controls.Add(this.buttonOpenSpecies);
            this.tabPageReferences.Name = "tabPageReferences";
            this.tabPageReferences.UseVisualStyleBackColor = true;
            // 
            // labelWaters
            // 
            resources.ApplyResources(this.labelWaters, "labelWaters");
            this.labelWaters.Name = "labelWaters";
            // 
            // buttonBrowseWaters
            // 
            resources.ApplyResources(this.buttonBrowseWaters, "buttonBrowseWaters");
            this.buttonBrowseWaters.Name = "buttonBrowseWaters";
            this.buttonBrowseWaters.UseVisualStyleBackColor = true;
            this.buttonBrowseWaters.Click += new System.EventHandler(this.buttonBrowseWaters_Click);
            // 
            // textBoxWaters
            // 
            resources.ApplyResources(this.textBoxWaters, "textBoxWaters");
            this.textBoxWaters.Name = "textBoxWaters";
            this.textBoxWaters.ReadOnly = true;
            // 
            // buttonOpenWaters
            // 
            resources.ApplyResources(this.buttonOpenWaters, "buttonOpenWaters");
            this.buttonOpenWaters.Name = "buttonOpenWaters";
            this.buttonOpenWaters.UseVisualStyleBackColor = true;
            this.buttonOpenWaters.Click += new System.EventHandler(this.buttonOpenWaters_Click);
            // 
            // labelRef
            // 
            resources.ApplyResources(this.labelRef, "labelRef");
            this.labelRef.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelRef.Name = "labelRef";
            // 
            // buttonBrowseSpecies
            // 
            resources.ApplyResources(this.buttonBrowseSpecies, "buttonBrowseSpecies");
            this.buttonBrowseSpecies.Name = "buttonBrowseSpecies";
            this.buttonBrowseSpecies.UseVisualStyleBackColor = true;
            this.buttonBrowseSpecies.Click += new System.EventHandler(this.buttonBrowseSpecies_Click);
            // 
            // textBoxSpecies
            // 
            resources.ApplyResources(this.textBoxSpecies, "textBoxSpecies");
            this.textBoxSpecies.Name = "textBoxSpecies";
            this.textBoxSpecies.ReadOnly = true;
            // 
            // labelSpecies
            // 
            resources.ApplyResources(this.labelSpecies, "labelSpecies");
            this.labelSpecies.Name = "labelSpecies";
            // 
            // buttonOpenSpecies
            // 
            resources.ApplyResources(this.buttonOpenSpecies, "buttonOpenSpecies");
            this.buttonOpenSpecies.Name = "buttonOpenSpecies";
            this.buttonOpenSpecies.UseVisualStyleBackColor = true;
            this.buttonOpenSpecies.Click += new System.EventHandler(this.buttonOpenSpecies_Click);
            // 
            // tabPageInput
            // 
            resources.ApplyResources(this.tabPageInput, "tabPageInput");
            this.tabPageInput.Controls.Add(this.tabControl1);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageSpecies);
            this.tabControl1.Controls.Add(this.tabPageIndividuals);
            this.tabControl1.Controls.Add(this.tabPageFactors);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageSpecies
            // 
            resources.ApplyResources(this.tabPageSpecies, "tabPageSpecies");
            this.tabPageSpecies.Controls.Add(this.checkBoxSpeciesExpand);
            this.tabPageSpecies.Controls.Add(this.checkBoxSpeciesExpandVisualControl);
            this.tabPageSpecies.Controls.Add(this.buttonClearRecent);
            this.tabPageSpecies.Controls.Add(this.labelRecent);
            this.tabPageSpecies.Controls.Add(this.numericUpDownRecentCount);
            this.tabPageSpecies.Controls.Add(this.checkBoxAutoLog);
            this.tabPageSpecies.Controls.Add(this.labelInputFish);
            this.tabPageSpecies.Name = "tabPageSpecies";
            this.tabPageSpecies.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpeciesExpand
            // 
            resources.ApplyResources(this.checkBoxSpeciesExpand, "checkBoxSpeciesExpand");
            this.checkBoxSpeciesExpand.Name = "checkBoxSpeciesExpand";
            this.checkBoxSpeciesExpand.UseVisualStyleBackColor = true;
            this.checkBoxSpeciesExpand.CheckedChanged += new System.EventHandler(this.checkBoxSpeciesExpand_CheckedChanged);
            // 
            // checkBoxSpeciesExpandVisualControl
            // 
            resources.ApplyResources(this.checkBoxSpeciesExpandVisualControl, "checkBoxSpeciesExpandVisualControl");
            this.checkBoxSpeciesExpandVisualControl.Name = "checkBoxSpeciesExpandVisualControl";
            this.checkBoxSpeciesExpandVisualControl.UseVisualStyleBackColor = true;
            // 
            // buttonClearRecent
            // 
            resources.ApplyResources(this.buttonClearRecent, "buttonClearRecent");
            this.buttonClearRecent.Name = "buttonClearRecent";
            this.buttonClearRecent.UseVisualStyleBackColor = true;
            this.buttonClearRecent.Click += new System.EventHandler(this.buttonClearRecent_Click);
            // 
            // labelRecent
            // 
            resources.ApplyResources(this.labelRecent, "labelRecent");
            this.labelRecent.Name = "labelRecent";
            // 
            // numericUpDownRecentCount
            // 
            resources.ApplyResources(this.numericUpDownRecentCount, "numericUpDownRecentCount");
            this.numericUpDownRecentCount.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownRecentCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownRecentCount.Name = "numericUpDownRecentCount";
            this.numericUpDownRecentCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxAutoLog
            // 
            resources.ApplyResources(this.checkBoxAutoLog, "checkBoxAutoLog");
            this.checkBoxAutoLog.Name = "checkBoxAutoLog";
            this.checkBoxAutoLog.UseVisualStyleBackColor = true;
            // 
            // labelInputFish
            // 
            resources.ApplyResources(this.labelInputFish, "labelInputFish");
            this.labelInputFish.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelInputFish.Name = "labelInputFish";
            // 
            // tabPageIndividuals
            // 
            resources.ApplyResources(this.tabPageIndividuals, "tabPageIndividuals");
            this.tabPageIndividuals.Controls.Add(this.listViewAddtVars);
            this.tabPageIndividuals.Controls.Add(this.buttonRemoveVar);
            this.tabPageIndividuals.Controls.Add(this.buttonNewVar);
            this.tabPageIndividuals.Controls.Add(this.checkBoxAutoDecreaseBio);
            this.tabPageIndividuals.Controls.Add(this.checkBoxFixTotals);
            this.tabPageIndividuals.Controls.Add(this.labelAddtsVars);
            this.tabPageIndividuals.Controls.Add(this.labelCommonVars);
            this.tabPageIndividuals.Controls.Add(this.checkBoxAutoIncreaseBio);
            this.tabPageIndividuals.Name = "tabPageIndividuals";
            this.tabPageIndividuals.UseVisualStyleBackColor = true;
            // 
            // listViewAddtVars
            // 
            resources.ApplyResources(this.listViewAddtVars, "listViewAddtVars");
            this.listViewAddtVars.CheckBoxes = true;
            this.listViewAddtVars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewAddtVars.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAddtVars.HideSelection = false;
            this.listViewAddtVars.LabelEdit = true;
            this.listViewAddtVars.Name = "listViewAddtVars";
            this.listViewAddtVars.ShowGroups = false;
            this.listViewAddtVars.UseCompatibleStateImageBehavior = false;
            this.listViewAddtVars.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // buttonRemoveVar
            // 
            resources.ApplyResources(this.buttonRemoveVar, "buttonRemoveVar");
            this.buttonRemoveVar.Name = "buttonRemoveVar";
            this.buttonRemoveVar.UseVisualStyleBackColor = true;
            // 
            // buttonNewVar
            // 
            resources.ApplyResources(this.buttonNewVar, "buttonNewVar");
            this.buttonNewVar.Name = "buttonNewVar";
            this.buttonNewVar.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoDecreaseBio
            // 
            resources.ApplyResources(this.checkBoxAutoDecreaseBio, "checkBoxAutoDecreaseBio");
            this.checkBoxAutoDecreaseBio.Name = "checkBoxAutoDecreaseBio";
            this.checkBoxAutoDecreaseBio.UseVisualStyleBackColor = true;
            // 
            // checkBoxFixTotals
            // 
            resources.ApplyResources(this.checkBoxFixTotals, "checkBoxFixTotals");
            this.checkBoxFixTotals.Name = "checkBoxFixTotals";
            this.checkBoxFixTotals.UseVisualStyleBackColor = true;
            // 
            // labelAddtsVars
            // 
            resources.ApplyResources(this.labelAddtsVars, "labelAddtsVars");
            this.labelAddtsVars.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelAddtsVars.Name = "labelAddtsVars";
            // 
            // labelCommonVars
            // 
            resources.ApplyResources(this.labelCommonVars, "labelCommonVars");
            this.labelCommonVars.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelCommonVars.Name = "labelCommonVars";
            // 
            // checkBoxAutoIncreaseBio
            // 
            resources.ApplyResources(this.checkBoxAutoIncreaseBio, "checkBoxAutoIncreaseBio");
            this.checkBoxAutoIncreaseBio.Name = "checkBoxAutoIncreaseBio";
            this.checkBoxAutoIncreaseBio.UseVisualStyleBackColor = true;
            // 
            // tabPageFactors
            // 
            resources.ApplyResources(this.tabPageFactors, "tabPageFactors");
            this.tabPageFactors.Controls.Add(this.listViewAddtFctr);
            this.tabPageFactors.Controls.Add(this.buttonAddtFctrsDelete);
            this.tabPageFactors.Controls.Add(this.buttonAddtFctrsAdd);
            this.tabPageFactors.Controls.Add(this.labelAddtFctrs);
            this.tabPageFactors.Name = "tabPageFactors";
            this.tabPageFactors.UseVisualStyleBackColor = true;
            // 
            // listViewAddtFctr
            // 
            resources.ApplyResources(this.listViewAddtFctr, "listViewAddtFctr");
            this.listViewAddtFctr.CheckBoxes = true;
            this.listViewAddtFctr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewAddtFctr.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAddtFctr.HideSelection = false;
            this.listViewAddtFctr.LabelEdit = true;
            this.listViewAddtFctr.Name = "listViewAddtFctr";
            this.listViewAddtFctr.ShowGroups = false;
            this.listViewAddtFctr.UseCompatibleStateImageBehavior = false;
            this.listViewAddtFctr.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // buttonAddtFctrsDelete
            // 
            resources.ApplyResources(this.buttonAddtFctrsDelete, "buttonAddtFctrsDelete");
            this.buttonAddtFctrsDelete.Name = "buttonAddtFctrsDelete";
            this.buttonAddtFctrsDelete.UseVisualStyleBackColor = true;
            // 
            // buttonAddtFctrsAdd
            // 
            resources.ApplyResources(this.buttonAddtFctrsAdd, "buttonAddtFctrsAdd");
            this.buttonAddtFctrsAdd.Name = "buttonAddtFctrsAdd";
            this.buttonAddtFctrsAdd.UseVisualStyleBackColor = true;
            // 
            // labelAddtFctrs
            // 
            resources.ApplyResources(this.labelAddtFctrs, "labelAddtFctrs");
            this.labelAddtFctrs.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelAddtFctrs.Name = "labelAddtFctrs";
            // 
            // tabPagePrint
            // 
            resources.ApplyResources(this.tabPagePrint, "tabPagePrint");
            this.tabPagePrint.Controls.Add(this.comboBoxLogOrder);
            this.tabPagePrint.Controls.Add(this.checkBoxBreakBetweenSpecies);
            this.tabPagePrint.Controls.Add(this.checkBoxOrderLog);
            this.tabPagePrint.Controls.Add(this.checkBoxCardOdd);
            this.tabPagePrint.Controls.Add(this.checkBoxBreakBeforeIndividuals);
            this.tabPagePrint.Controls.Add(this.labelPrintCaption);
            this.tabPagePrint.Name = "tabPagePrint";
            this.tabPagePrint.UseVisualStyleBackColor = true;
            // 
            // comboBoxLogOrder
            // 
            resources.ApplyResources(this.comboBoxLogOrder, "comboBoxLogOrder");
            this.comboBoxLogOrder.DisplayMember = "DisplayName";
            this.comboBoxLogOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogOrder.FormattingEnabled = true;
            this.comboBoxLogOrder.Items.AddRange(new object[] {
            resources.GetString("comboBoxLogOrder.Items"),
            resources.GetString("comboBoxLogOrder.Items1"),
            resources.GetString("comboBoxLogOrder.Items2")});
            this.comboBoxLogOrder.Name = "comboBoxLogOrder";
            // 
            // checkBoxBreakBetweenSpecies
            // 
            resources.ApplyResources(this.checkBoxBreakBetweenSpecies, "checkBoxBreakBetweenSpecies");
            this.checkBoxBreakBetweenSpecies.Name = "checkBoxBreakBetweenSpecies";
            this.checkBoxBreakBetweenSpecies.UseVisualStyleBackColor = true;
            // 
            // checkBoxOrderLog
            // 
            resources.ApplyResources(this.checkBoxOrderLog, "checkBoxOrderLog");
            this.checkBoxOrderLog.Name = "checkBoxOrderLog";
            this.checkBoxOrderLog.UseVisualStyleBackColor = true;
            this.checkBoxOrderLog.CheckedChanged += new System.EventHandler(this.checkBoxOrderLog_CheckedChanged);
            // 
            // checkBoxCardOdd
            // 
            resources.ApplyResources(this.checkBoxCardOdd, "checkBoxCardOdd");
            this.checkBoxCardOdd.Name = "checkBoxCardOdd";
            this.checkBoxCardOdd.UseVisualStyleBackColor = true;
            this.checkBoxCardOdd.CheckedChanged += new System.EventHandler(this.checkBoxBreakBeforeIndividuals_CheckedChanged);
            // 
            // checkBoxBreakBeforeIndividuals
            // 
            resources.ApplyResources(this.checkBoxBreakBeforeIndividuals, "checkBoxBreakBeforeIndividuals");
            this.checkBoxBreakBeforeIndividuals.Name = "checkBoxBreakBeforeIndividuals";
            this.checkBoxBreakBeforeIndividuals.UseVisualStyleBackColor = true;
            this.checkBoxBreakBeforeIndividuals.CheckedChanged += new System.EventHandler(this.checkBoxBreakBeforeIndividuals_CheckedChanged);
            // 
            // labelPrintCaption
            // 
            resources.ApplyResources(this.labelPrintCaption, "labelPrintCaption");
            this.labelPrintCaption.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelPrintCaption.Name = "labelPrintCaption";
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
            // tdClearRecent
            // 
            this.tdClearRecent.Buttons.Add(this.tdbRecentClear);
            this.tdClearRecent.Buttons.Add(this.tdbRecentCancel);
            resources.ApplyResources(this.tdClearRecent, "tdClearRecent");
            // 
            // tdbRecentClear
            // 
            resources.ApplyResources(this.tdbRecentClear, "tdbRecentClear");
            // 
            // tdbRecentCancel
            // 
            this.tdbRecentCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            resources.ApplyResources(this.tdbRecentCancel, "tdbRecentCancel");
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
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonBasicSettings);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageReferences.ResumeLayout(false);
            this.tabPageReferences.PerformLayout();
            this.tabPageInput.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSpecies.ResumeLayout(false);
            this.tabPageSpecies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).EndInit();
            this.tabPageIndividuals.ResumeLayout(false);
            this.tabPageIndividuals.PerformLayout();
            this.tabPageFactors.ResumeLayout(false);
            this.tabPageFactors.PerformLayout();
            this.tabPagePrint.ResumeLayout(false);
            this.tabPagePrint.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TextBox textBoxSpecies;
        protected System.Windows.Forms.TextBox textBoxWaters;
        protected System.Windows.Forms.CheckBox checkBoxBreakBetweenSpecies;
        protected System.Windows.Forms.CheckBox checkBoxBreakBeforeIndividuals;
        protected System.Windows.Forms.CheckBox checkBoxCardOdd;
        protected System.Windows.Forms.NumericUpDown numericUpDownRecentCount;
        protected System.Windows.Forms.CheckBox checkBoxAutoLog;
        protected System.Windows.Forms.ListView listViewAddtVars;
        protected System.Windows.Forms.CheckBox checkBoxAutoDecreaseBio;
        protected System.Windows.Forms.CheckBox checkBoxFixTotals;
        protected System.Windows.Forms.CheckBox checkBoxAutoIncreaseBio;
        protected TaskDialogs.TaskDialog tdClearRecent;
        protected TaskDialogs.TaskDialogButton tdbRecentClear;
        protected System.Windows.Forms.ListView listViewAddtFctr;
        protected System.Windows.Forms.TabPage tabPageReferences;
        protected System.Windows.Forms.TabPage tabPageIndividuals;
        protected System.Windows.Forms.TabPage tabPageFactors;
        protected System.Windows.Forms.Button buttonCancel;
        protected System.Windows.Forms.Label labelRef;
        protected System.Windows.Forms.Label labelSpecies;
        protected System.Windows.Forms.Button buttonOpenSpecies;
        protected System.Windows.Forms.Label labelWaters;
        protected System.Windows.Forms.Button buttonOpenWaters;
        protected System.Windows.Forms.TabControl tabControlSettings;
        protected System.Windows.Forms.Button buttonOK;
        protected System.Windows.Forms.Button buttonBrowseSpecies;
        protected System.Windows.Forms.Button buttonBrowseWaters;
        protected System.Windows.Forms.Button buttonApply;
        protected System.Windows.Forms.TabPage tabPagePrint;
        protected System.Windows.Forms.Label labelPrintCaption;
        protected System.Windows.Forms.TabPage tabPageInput;
        protected System.Windows.Forms.TabControl tabControl1;
        protected System.Windows.Forms.TabPage tabPageSpecies;
        protected System.Windows.Forms.Label labelRecent;
        protected System.Windows.Forms.Label labelInputFish;
        protected System.Windows.Forms.ColumnHeader columnHeader1;
        protected System.Windows.Forms.Button buttonRemoveVar;
        protected System.Windows.Forms.Button buttonNewVar;
        protected System.Windows.Forms.Label labelCommonVars;
        protected System.Windows.Forms.Button buttonClearRecent;
        protected System.Windows.Forms.Label labelAddtsVars;
        protected TaskDialogs.TaskDialogButton tdbRecentCancel;
        protected System.Windows.Forms.ColumnHeader columnHeader2;
        protected System.Windows.Forms.Button buttonAddtFctrsDelete;
        protected System.Windows.Forms.Button buttonAddtFctrsAdd;
        protected System.Windows.Forms.Label labelAddtFctrs;
        protected System.Windows.Forms.CheckBox checkBoxSpeciesExpand;
        protected System.Windows.Forms.CheckBox checkBoxSpeciesExpandVisualControl;
        protected System.Windows.Forms.CheckBox checkBoxOrderLog;
        protected System.Windows.Forms.ComboBox comboBoxLogOrder;
        protected System.Windows.Forms.Button buttonBasicSettings;
    }
}