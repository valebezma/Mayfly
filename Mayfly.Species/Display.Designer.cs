namespace Mayfly.Species
{
    partial class Display
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Display));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuKeys = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBack = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemGotoTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGotoSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemToTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.menuService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTaxa = new System.Windows.Forms.TabPage();
            this.labelSpcCount = new System.Windows.Forms.Label();
            this.labelTaxCount = new System.Windows.Forms.Label();
            this.listViewRepresence = new System.Windows.Forms.ListView();
            this.columnHeaderSpeciesName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderReference = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewTaxa = new System.Windows.Forms.TreeView();
            this.tabPageKey = new System.Windows.Forms.TabPage();
            this.contextGoto = new System.Windows.Forms.ContextMenuStrip();
            this.contextGotoTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGotoSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.contextToTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.contextToSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxFits = new System.Windows.Forms.CheckBox();
            this.checkBoxDiagnosis = new System.Windows.Forms.CheckBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.definitionPanel = new Mayfly.Species.Controls.DefinitionPanel();
            this.tabPageFit = new System.Windows.Forms.TabPage();
            this.labelFitCount = new System.Windows.Forms.Label();
            this.labelFtrCount = new System.Windows.Forms.Label();
            this.listViewFits = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeViewFeatures = new System.Windows.Forms.TreeView();
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.status1 = new Mayfly.Controls.Status();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageTaxa.SuspendLayout();
            this.tabPageKey.SuspendLayout();
            this.contextGoto.SuspendLayout();
            this.tabPageFit.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuKeys,
            this.menuService});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPreview,
            this.menuItemPrint,
            this.toolStripSeparator8,
            this.menuItemClose});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            // 
            // menuItemPreview
            // 
            this.menuItemPreview.Image = global::Mayfly.Pictogram.Preview;
            this.menuItemPreview.Name = "menuItemPreview";
            resources.ApplyResources(this.menuItemPreview, "menuItemPreview");
            this.menuItemPreview.Click += new System.EventHandler(this.menuItemPreview_Click);
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Image = global::Mayfly.Pictogram.Print;
            this.menuItemPrint.Name = "menuItemPrint";
            resources.ApplyResources(this.menuItemPrint, "menuItemPrint");
            this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuKeys
            // 
            this.menuKeys.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemRestart,
            this.menuItemBack,
            this.toolStripSeparator7,
            this.menuItemGotoTaxon,
            this.menuItemGotoSpecies,
            this.toolStripSeparator1,
            this.menuItemToTaxon,
            this.menuItemToSpecies});
            this.menuKeys.Name = "menuKeys";
            resources.ApplyResources(this.menuKeys, "menuKeys");
            // 
            // menuItemRestart
            // 
            this.menuItemRestart.Name = "menuItemRestart";
            resources.ApplyResources(this.menuItemRestart, "menuItemRestart");
            this.menuItemRestart.Click += new System.EventHandler(this.menuItemRestart_Click);
            // 
            // menuItemBack
            // 
            resources.ApplyResources(this.menuItemBack, "menuItemBack");
            this.menuItemBack.Name = "menuItemBack";
            this.menuItemBack.Click += new System.EventHandler(this.menuItemBack_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // menuItemGotoTaxon
            // 
            this.menuItemGotoTaxon.Name = "menuItemGotoTaxon";
            resources.ApplyResources(this.menuItemGotoTaxon, "menuItemGotoTaxon");
            // 
            // menuItemGotoSpecies
            // 
            this.menuItemGotoSpecies.Name = "menuItemGotoSpecies";
            resources.ApplyResources(this.menuItemGotoSpecies, "menuItemGotoSpecies");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuItemToTaxon
            // 
            this.menuItemToTaxon.Name = "menuItemToTaxon";
            resources.ApplyResources(this.menuItemToTaxon, "menuItemToTaxon");
            // 
            // menuItemToSpecies
            // 
            this.menuItemToSpecies.Name = "menuItemToSpecies";
            resources.ApplyResources(this.menuItemToSpecies, "menuItemToSpecies");
            // 
            // menuService
            // 
            this.menuService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.menuItemAbout});
            this.menuService.Name = "menuService";
            resources.ApplyResources(this.menuService, "menuService");
            // 
            // menuItemSettings
            // 
            this.menuItemSettings.Image = global::Mayfly.Pictogram.Settings;
            this.menuItemSettings.Name = "menuItemSettings";
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            resources.ApplyResources(this.statusLabel, "statusLabel");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageTaxa);
            this.tabControl.Controls.Add(this.tabPageKey);
            this.tabControl.Controls.Add(this.tabPageFit);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageTaxa
            // 
            this.tabPageTaxa.Controls.Add(this.labelSpcCount);
            this.tabPageTaxa.Controls.Add(this.labelTaxCount);
            this.tabPageTaxa.Controls.Add(this.listViewRepresence);
            this.tabPageTaxa.Controls.Add(this.treeViewTaxa);
            resources.ApplyResources(this.tabPageTaxa, "tabPageTaxa");
            this.tabPageTaxa.Name = "tabPageTaxa";
            this.tabPageTaxa.UseVisualStyleBackColor = true;
            // 
            // labelSpcCount
            // 
            resources.ApplyResources(this.labelSpcCount, "labelSpcCount");
            this.labelSpcCount.Name = "labelSpcCount";
            // 
            // labelTaxCount
            // 
            resources.ApplyResources(this.labelTaxCount, "labelTaxCount");
            this.labelTaxCount.Name = "labelTaxCount";
            // 
            // listViewRepresence
            // 
            this.listViewRepresence.AllowColumnReorder = true;
            this.listViewRepresence.AllowDrop = true;
            resources.ApplyResources(this.listViewRepresence, "listViewRepresence");
            this.listViewRepresence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRepresence.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSpeciesName,
            this.columnHeaderReference});
            this.listViewRepresence.FullRowSelect = true;
            this.listViewRepresence.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewRepresence.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewRepresence.Groups1"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewRepresence.Groups2")))});
            this.listViewRepresence.Name = "listViewRepresence";
            this.listViewRepresence.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewRepresence.UseCompatibleStateImageBehavior = false;
            this.listViewRepresence.View = System.Windows.Forms.View.Details;
            this.listViewRepresence.ItemActivate += new System.EventHandler(this.listViewSpecies_ItemActivate);
            // 
            // columnHeaderSpeciesName
            // 
            resources.ApplyResources(this.columnHeaderSpeciesName, "columnHeaderSpeciesName");
            // 
            // columnHeaderReference
            // 
            resources.ApplyResources(this.columnHeaderReference, "columnHeaderReference");
            // 
            // treeViewTaxa
            // 
            resources.ApplyResources(this.treeViewTaxa, "treeViewTaxa");
            this.treeViewTaxa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewTaxa.FullRowSelect = true;
            this.treeViewTaxa.HotTracking = true;
            this.treeViewTaxa.ItemHeight = 23;
            this.treeViewTaxa.Name = "treeViewTaxa";
            this.treeViewTaxa.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeViewTaxa.Nodes")))});
            this.treeViewTaxa.ShowLines = false;
            this.treeViewTaxa.ShowNodeToolTips = true;
            this.treeViewTaxa.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTaxa_AfterSelect);
            // 
            // tabPageKey
            // 
            this.tabPageKey.ContextMenuStrip = this.contextGoto;
            this.tabPageKey.Controls.Add(this.checkBoxFits);
            this.tabPageKey.Controls.Add(this.checkBoxDiagnosis);
            this.tabPageKey.Controls.Add(this.buttonBack);
            this.tabPageKey.Controls.Add(this.definitionPanel);
            resources.ApplyResources(this.tabPageKey, "tabPageKey");
            this.tabPageKey.Name = "tabPageKey";
            this.tabPageKey.UseVisualStyleBackColor = true;
            // 
            // contextGoto
            // 
            this.contextGoto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGotoTaxon,
            this.contextGotoSpecies,
            this.toolStripSeparator9,
            this.contextToTaxon,
            this.contextToSpecies});
            this.contextGoto.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextGoto, "contextGoto");
            // 
            // contextGotoTaxon
            // 
            this.contextGotoTaxon.Name = "contextGotoTaxon";
            resources.ApplyResources(this.contextGotoTaxon, "contextGotoTaxon");
            // 
            // contextGotoSpecies
            // 
            this.contextGotoSpecies.Name = "contextGotoSpecies";
            resources.ApplyResources(this.contextGotoSpecies, "contextGotoSpecies");
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // contextToTaxon
            // 
            this.contextToTaxon.Name = "contextToTaxon";
            resources.ApplyResources(this.contextToTaxon, "contextToTaxon");
            // 
            // contextToSpecies
            // 
            this.contextToSpecies.Name = "contextToSpecies";
            resources.ApplyResources(this.contextToSpecies, "contextToSpecies");
            // 
            // checkBoxFits
            // 
            resources.ApplyResources(this.checkBoxFits, "checkBoxFits");
            this.checkBoxFits.Name = "checkBoxFits";
            this.checkBoxFits.UseVisualStyleBackColor = true;
            this.checkBoxFits.CheckedChanged += new System.EventHandler(this.checkBoxFits_CheckedChanged);
            // 
            // checkBoxDiagnosis
            // 
            resources.ApplyResources(this.checkBoxDiagnosis, "checkBoxDiagnosis");
            this.checkBoxDiagnosis.Name = "checkBoxDiagnosis";
            this.checkBoxDiagnosis.UseVisualStyleBackColor = true;
            this.checkBoxDiagnosis.CheckedChanged += new System.EventHandler(this.checkBoxHistory_CheckedChanged);
            // 
            // buttonBack
            // 
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.Name = "buttonBack";
            this.toolTip.SetToolTip(this.buttonBack, resources.GetString("buttonBack.ToolTip"));
            this.buttonBack.Click += new System.EventHandler(this.menuItemBack_Click);
            // 
            // definitionPanel
            // 
            resources.ApplyResources(this.definitionPanel, "definitionPanel");
            this.definitionPanel.Name = "definitionPanel";
            this.definitionPanel.UserSelectedState += new Mayfly.Species.Controls.StateClickedEventHandler(this.definitionPanel_UserSelectedState);
            this.definitionPanel.SpeciesDefined += new Mayfly.Species.Controls.StateClickedEventHandler(this.definitionPanel_SpeciesDefined);
            this.definitionPanel.StepChanged += new Mayfly.Species.Controls.DefinitionEventHandler(this.definitionPanel_StepChanged);
            // 
            // tabPageFit
            // 
            this.tabPageFit.Controls.Add(this.labelFitCount);
            this.tabPageFit.Controls.Add(this.labelFtrCount);
            this.tabPageFit.Controls.Add(this.listViewFits);
            this.tabPageFit.Controls.Add(this.treeViewFeatures);
            resources.ApplyResources(this.tabPageFit, "tabPageFit");
            this.tabPageFit.Name = "tabPageFit";
            this.tabPageFit.UseVisualStyleBackColor = true;
            // 
            // labelFitCount
            // 
            resources.ApplyResources(this.labelFitCount, "labelFitCount");
            this.labelFitCount.Name = "labelFitCount";
            // 
            // labelFtrCount
            // 
            resources.ApplyResources(this.labelFtrCount, "labelFtrCount");
            this.labelFtrCount.Name = "labelFtrCount";
            // 
            // listViewFits
            // 
            resources.ApplyResources(this.listViewFits, "listViewFits");
            this.listViewFits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewFits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewFits.FullRowSelect = true;
            this.listViewFits.Name = "listViewFits";
            this.listViewFits.ShowGroups = false;
            this.listViewFits.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewFits.UseCompatibleStateImageBehavior = false;
            this.listViewFits.View = System.Windows.Forms.View.Details;
            this.listViewFits.ItemActivate += new System.EventHandler(this.listViewFits_ItemActivate);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // treeViewFeatures
            // 
            this.treeViewFeatures.AllowDrop = true;
            resources.ApplyResources(this.treeViewFeatures, "treeViewFeatures");
            this.treeViewFeatures.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewFeatures.CheckBoxes = true;
            this.treeViewFeatures.FullRowSelect = true;
            this.treeViewFeatures.HotTracking = true;
            this.treeViewFeatures.ItemHeight = 23;
            this.treeViewFeatures.Name = "treeViewFeatures";
            this.treeViewFeatures.ShowLines = false;
            this.treeViewFeatures.ShowNodeToolTips = true;
            this.treeViewFeatures.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFeatures_AfterCheck);
            this.treeViewFeatures.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewFeatures_DrawNode);
            // 
            // status1
            // 
            this.status1.Default = null;
            this.status1.MaximalInterval = 2000;
            this.status1.StatusLog = this.statusLabel;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageTaxa.ResumeLayout(false);
            this.tabPageTaxa.PerformLayout();
            this.tabPageKey.ResumeLayout(false);
            this.contextGoto.ResumeLayout(false);
            this.tabPageFit.ResumeLayout(false);
            this.tabPageFit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private Mayfly.Controls.Status status1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripMenuItem menuKeys;
        private System.Windows.Forms.ToolStripMenuItem menuItemBack;
        private System.Windows.Forms.ToolStripMenuItem menuItemGotoTaxon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem menuItemGotoSpecies;
        private System.Windows.Forms.TabPage tabPageKey;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextGoto;
        private System.Windows.Forms.ToolStripMenuItem contextGotoTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextGotoSpecies;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem contextToTaxon;
        private System.Windows.Forms.ToolStripMenuItem contextToSpecies;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.ToolStripMenuItem menuItemPreview;
        private System.Windows.Forms.ToolStripMenuItem menuItemPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuService;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private Controls.DefinitionPanel definitionPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemToTaxon;
        private System.Windows.Forms.ToolStripMenuItem menuItemToSpecies;
        private System.Windows.Forms.ToolStripMenuItem menuItemRestart;
        private System.Windows.Forms.TabPage tabPageTaxa;
        private System.Windows.Forms.Label labelSpcCount;
        private System.Windows.Forms.Label labelTaxCount;
        private System.Windows.Forms.ListView listViewRepresence;
        private System.Windows.Forms.ColumnHeader columnHeaderSpeciesName;
        private System.Windows.Forms.ColumnHeader columnHeaderReference;
        private System.Windows.Forms.TabPage tabPageFit;
        private System.Windows.Forms.Label labelFitCount;
        private System.Windows.Forms.Label labelFtrCount;
        private System.Windows.Forms.ListView listViewFits;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TreeView treeViewTaxa;
        private System.Windows.Forms.TreeView treeViewFeatures;
        private System.Windows.Forms.CheckBox checkBoxFits;
        private System.Windows.Forms.CheckBox checkBoxDiagnosis;
    }
}