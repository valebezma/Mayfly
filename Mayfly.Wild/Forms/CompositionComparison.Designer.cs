namespace Mayfly.Wild
{
    partial class CompositionComparison
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompositionComparison));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spreadSheetA = new Mayfly.Controls.SpreadSheet();
            this.ColumnSpeciesA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAbundance = new System.Windows.Forms.TabPage();
            this.tabPageBiomasses = new System.Windows.Forms.TabPage();
            this.spreadSheetB = new Mayfly.Controls.SpreadSheet();
            this.columnSpeciesB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageOccurrence = new System.Windows.Forms.TabPage();
            this.spreadSheetO = new Mayfly.Controls.SpreadSheet();
            this.columnSpeciesO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageDominance = new System.Windows.Forms.TabPage();
            this.spreadSheetD = new Mayfly.Controls.SpreadSheet();
            this.columnSpeciesD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageMatrix = new System.Windows.Forms.TabPage();
            this.listViewIndex = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelCombinationsCount = new System.Windows.Forms.Label();
            this.spreadSheetMatrix = new Mayfly.Controls.SpreadSheet();
            this.processDisplay = new Mayfly.Controls.ProcessDisplay(this.components);
            this.statusLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.statusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.listsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSimilarity = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backMatch = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetA)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageAbundance.SuspendLayout();
            this.tabPageBiomasses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetB)).BeginInit();
            this.tabPageOccurrence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetO)).BeginInit();
            this.tabPageDominance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetD)).BeginInit();
            this.tabPageMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMatrix)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadSheetA
            // 
            resources.ApplyResources(this.spreadSheetA, "spreadSheetA");
            this.spreadSheetA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpeciesA});
            this.spreadSheetA.Name = "spreadSheetA";
            this.spreadSheetA.ReadOnly = true;
            // 
            // ColumnSpeciesA
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSpeciesA.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnSpeciesA, "ColumnSpeciesA");
            this.ColumnSpeciesA.Name = "ColumnSpeciesA";
            this.ColumnSpeciesA.ReadOnly = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageAbundance);
            this.tabControl1.Controls.Add(this.tabPageBiomasses);
            this.tabControl1.Controls.Add(this.tabPageOccurrence);
            this.tabControl1.Controls.Add(this.tabPageDominance);
            this.tabControl1.Controls.Add(this.tabPageMatrix);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageAbundance
            // 
            this.tabPageAbundance.Controls.Add(this.spreadSheetA);
            resources.ApplyResources(this.tabPageAbundance, "tabPageAbundance");
            this.tabPageAbundance.Name = "tabPageAbundance";
            this.tabPageAbundance.UseVisualStyleBackColor = true;
            // 
            // tabPageBiomasses
            // 
            this.tabPageBiomasses.Controls.Add(this.spreadSheetB);
            resources.ApplyResources(this.tabPageBiomasses, "tabPageBiomasses");
            this.tabPageBiomasses.Name = "tabPageBiomasses";
            this.tabPageBiomasses.UseVisualStyleBackColor = true;
            // 
            // spreadSheetB
            // 
            resources.ApplyResources(this.spreadSheetB, "spreadSheetB");
            this.spreadSheetB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSpeciesB});
            this.spreadSheetB.DefaultDecimalPlaces = 3;
            this.spreadSheetB.Name = "spreadSheetB";
            this.spreadSheetB.ReadOnly = true;
            // 
            // columnSpeciesB
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSpeciesB.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.columnSpeciesB, "columnSpeciesB");
            this.columnSpeciesB.Name = "columnSpeciesB";
            this.columnSpeciesB.ReadOnly = true;
            // 
            // tabPageOccurrence
            // 
            this.tabPageOccurrence.Controls.Add(this.spreadSheetO);
            resources.ApplyResources(this.tabPageOccurrence, "tabPageOccurrence");
            this.tabPageOccurrence.Name = "tabPageOccurrence";
            this.tabPageOccurrence.UseVisualStyleBackColor = true;
            // 
            // spreadSheetO
            // 
            resources.ApplyResources(this.spreadSheetO, "spreadSheetO");
            this.spreadSheetO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSpeciesO});
            this.spreadSheetO.DefaultDecimalPlaces = 3;
            this.spreadSheetO.Name = "spreadSheetO";
            this.spreadSheetO.ReadOnly = true;
            // 
            // columnSpeciesO
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSpeciesO.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.columnSpeciesO, "columnSpeciesO");
            this.columnSpeciesO.Name = "columnSpeciesO";
            this.columnSpeciesO.ReadOnly = true;
            // 
            // tabPageDominance
            // 
            this.tabPageDominance.Controls.Add(this.spreadSheetD);
            resources.ApplyResources(this.tabPageDominance, "tabPageDominance");
            this.tabPageDominance.Name = "tabPageDominance";
            this.tabPageDominance.UseVisualStyleBackColor = true;
            // 
            // spreadSheetD
            // 
            resources.ApplyResources(this.spreadSheetD, "spreadSheetD");
            this.spreadSheetD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSpeciesD});
            this.spreadSheetD.DefaultDecimalPlaces = 3;
            this.spreadSheetD.Name = "spreadSheetD";
            this.spreadSheetD.ReadOnly = true;
            // 
            // columnSpeciesD
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnSpeciesD.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.columnSpeciesD, "columnSpeciesD");
            this.columnSpeciesD.Name = "columnSpeciesD";
            this.columnSpeciesD.ReadOnly = true;
            // 
            // tabPageMatrix
            // 
            this.tabPageMatrix.Controls.Add(this.listViewIndex);
            this.tabPageMatrix.Controls.Add(this.labelCombinationsCount);
            this.tabPageMatrix.Controls.Add(this.spreadSheetMatrix);
            resources.ApplyResources(this.tabPageMatrix, "tabPageMatrix");
            this.tabPageMatrix.Name = "tabPageMatrix";
            this.tabPageMatrix.UseVisualStyleBackColor = true;
            // 
            // listViewIndex
            // 
            this.listViewIndex.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            resources.ApplyResources(this.listViewIndex, "listViewIndex");
            this.listViewIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.listViewIndex.FullRowSelect = true;
            this.listViewIndex.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewIndex.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewIndex.Groups1"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewIndex.Groups2")))});
            this.listViewIndex.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewIndex.HideSelection = false;
            this.listViewIndex.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items3"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items4"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items5"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items6"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items7"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items8"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items9"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items10"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items11"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items12"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items13"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items14"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listViewIndex.Items15")))});
            this.listViewIndex.MultiSelect = false;
            this.listViewIndex.Name = "listViewIndex";
            this.listViewIndex.TileSize = new System.Drawing.Size(135, 20);
            this.listViewIndex.UseCompatibleStateImageBehavior = false;
            this.listViewIndex.View = System.Windows.Forms.View.Tile;
            this.listViewIndex.SelectedIndexChanged += new System.EventHandler(this.listViewIndex_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // labelCombinationsCount
            // 
            resources.ApplyResources(this.labelCombinationsCount, "labelCombinationsCount");
            this.labelCombinationsCount.Name = "labelCombinationsCount";
            // 
            // spreadSheetMatrix
            // 
            resources.ApplyResources(this.spreadSheetMatrix, "spreadSheetMatrix");
            this.spreadSheetMatrix.DefaultDecimalPlaces = 3;
            this.spreadSheetMatrix.Display = this.processDisplay;
            this.spreadSheetMatrix.Name = "spreadSheetMatrix";
            this.spreadSheetMatrix.ReadOnly = true;
            // 
            // processDisplay
            // 
            this.processDisplay.Default = null;
            this.processDisplay.Look = null;
            this.processDisplay.MaximalInterval = 2000;
            this.processDisplay.ProgressBar = this.statusLoading;
            this.processDisplay.StatusLog = this.statusProcess;
            // 
            // statusLoading
            // 
            resources.ApplyResources(this.statusLoading, "statusLoading");
            this.statusLoading.Margin = new System.Windows.Forms.Padding(1, 6, 1, 6);
            this.statusLoading.Name = "statusLoading";
            this.statusLoading.Value = 50;
            // 
            // statusProcess
            // 
            this.statusProcess.Name = "statusProcess";
            resources.ApplyResources(this.statusProcess, "statusProcess");
            this.statusProcess.Spring = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listsToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // listsToolStripMenuItem
            // 
            this.listsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSimilarity});
            this.listsToolStripMenuItem.Name = "listsToolStripMenuItem";
            resources.ApplyResources(this.listsToolStripMenuItem, "listsToolStripMenuItem");
            // 
            // menuItemSimilarity
            // 
            this.menuItemSimilarity.Name = "menuItemSimilarity";
            resources.ApplyResources(this.menuItemSimilarity, "menuItemSimilarity");
            this.menuItemSimilarity.Click += new System.EventHandler(this.menuItemSimilarity_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProcess,
            this.statusLoading});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // backMatch
            // 
            this.backMatch.WorkerReportsProgress = true;
            this.backMatch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backMatch_DoWork);
            this.backMatch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backMatch_ProgressChanged);
            this.backMatch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backMatch_RunWorkerCompleted);
            // 
            // CompositionComparison
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CompositionComparison";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetA)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageAbundance.ResumeLayout(false);
            this.tabPageBiomasses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetB)).EndInit();
            this.tabPageOccurrence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetO)).EndInit();
            this.tabPageDominance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetD)).EndInit();
            this.tabPageMatrix.ResumeLayout(false);
            this.tabPageMatrix.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetMatrix)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mayfly.Controls.SpreadSheet spreadSheetA;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAbundance;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem listsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusProcess;
        private System.Windows.Forms.ToolStripProgressBar statusLoading;
        private System.Windows.Forms.TabPage tabPageBiomasses;
        private Mayfly.Controls.SpreadSheet spreadSheetB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpeciesA;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSpeciesB;
        private System.Windows.Forms.TabPage tabPageMatrix;
        private System.Windows.Forms.Label labelCombinationsCount;
        private Mayfly.Controls.SpreadSheet spreadSheetMatrix;
        private System.Windows.Forms.ToolStripMenuItem menuItemSimilarity;
        private System.Windows.Forms.ListView listViewIndex;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.ComponentModel.BackgroundWorker backMatch;
        private Mayfly.Controls.ProcessDisplay processDisplay;
        private System.Windows.Forms.TabPage tabPageOccurrence;
        private Mayfly.Controls.SpreadSheet spreadSheetO;
        private System.Windows.Forms.TabPage tabPageDominance;
        private Mayfly.Controls.SpreadSheet spreadSheetD;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSpeciesO;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSpeciesD;
    }
}