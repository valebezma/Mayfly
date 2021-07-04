namespace Mayfly.Calculus
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLoading = new System.Windows.Forms.ToolStripProgressBar();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTable = new System.Windows.Forms.TabPage();
            this.labelEntriesCount = new System.Windows.Forms.Label();
            this.spreadSheetData = new Mayfly.Controls.SpreadSheet();
            this.processDisplay = new Mayfly.Controls.ProcessDisplay(this.components);
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.status1 = new Mayfly.Controls.Status();
            this.adapter = new Mayfly.Mathematics.MathAdapter(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetData)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusProcess,
            this.statusLoading});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // statusProcess
            // 
            this.statusProcess.Name = "statusProcess";
            resources.ApplyResources(this.statusProcess, "statusProcess");
            this.statusProcess.Spring = true;
            // 
            // statusLoading
            // 
            this.statusLoading.Margin = new System.Windows.Forms.Padding(1, 6, 1, 6);
            this.statusLoading.Name = "statusLoading";
            resources.ApplyResources(this.statusLoading, "statusLoading");
            this.statusLoading.Value = 50;
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.menuItemPrint,
            this.toolStripSeparator1,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            resources.ApplyResources(this.menuItemFile, "menuItemFile");
            // 
            // menuItemNew
            // 
            resources.ApplyResources(this.menuItemNew, "menuItemNew");
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            resources.ApplyResources(this.menuItemOpen, "menuItemOpen");
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            resources.ApplyResources(this.menuItemSave, "menuItemSave");
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            resources.ApplyResources(this.menuItemSaveAs, "menuItemSaveAs");
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItemPrint
            // 
            resources.ApplyResources(this.menuItemPrint, "menuItemPrint");
            this.menuItemPrint.Name = "menuItemPrint";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            resources.ApplyResources(this.menuItemExit, "menuItemExit");
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.serviceToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSettings,
            this.menuItemAbout});
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            resources.ApplyResources(this.serviceToolStripMenuItem, "serviceToolStripMenuItem");
            // 
            // menuItemSettings
            // 
            resources.ApplyResources(this.menuItemSettings, "menuItemSettings");
            this.menuItemSettings.Name = "menuItemSettings";
            this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageTable);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageTable
            // 
            this.tabPageTable.Controls.Add(this.labelEntriesCount);
            this.tabPageTable.Controls.Add(this.spreadSheetData);
            resources.ApplyResources(this.tabPageTable, "tabPageTable");
            this.tabPageTable.Name = "tabPageTable";
            this.tabPageTable.UseVisualStyleBackColor = true;
            // 
            // labelEntriesCount
            // 
            resources.ApplyResources(this.labelEntriesCount, "labelEntriesCount");
            this.labelEntriesCount.Name = "labelEntriesCount";
            // 
            // spreadSheetData
            // 
            this.spreadSheetData.AllowColumnRenaming = true;
            this.spreadSheetData.AllowStringSuggection = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.spreadSheetData.AllowUserToAddRows = true;
            this.spreadSheetData.AllowUserToDeleteColumns = true;
            this.spreadSheetData.AllowUserToDeleteRows = true;
            resources.ApplyResources(this.spreadSheetData, "spreadSheetData");
            this.spreadSheetData.IsLog = true;
            this.spreadSheetData.Name = "spreadSheetData";
            this.spreadSheetData.Filtered += new System.EventHandler(this.spreadSheetData_Filtered);
            this.spreadSheetData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetData_CellValueChanged);
            // 
            // processDisplay
            // 
            this.processDisplay.Default = null;
            this.processDisplay.Look = null;
            this.processDisplay.MaximalInterval = 2000;
            this.processDisplay.ProgressBar = this.statusLoading;
            this.processDisplay.StatusLog = this.statusProcess;
            // 
            // taskDialogSaveChanges
            // 
            this.taskDialogSaveChanges.AllowDialogCancellation = true;
            this.taskDialogSaveChanges.Buttons.Add(this.tdbSave);
            this.taskDialogSaveChanges.Buttons.Add(this.tdbDiscard);
            this.taskDialogSaveChanges.Buttons.Add(this.tdbCancelClose);
            this.taskDialogSaveChanges.CenterParent = true;
            resources.ApplyResources(this.taskDialogSaveChanges, "taskDialogSaveChanges");
            // 
            // tdbSave
            // 
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
            // status1
            // 
            this.status1.Default = null;
            this.status1.MaximalInterval = 2000;
            this.status1.StatusLog = this.toolStripStatusLabel1;
            // 
            // adapter
            // 
            this.adapter.Sheet = this.spreadSheetData;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTable.ResumeLayout(false);
            this.tabPageTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Controls.Status status1;
        private TaskDialogs.TaskDialog taskDialogSaveChanges;
        private TaskDialogs.TaskDialogButton tdbSave;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTable;
        private Mayfly.Controls.SpreadSheet spreadSheetData;
        private System.Windows.Forms.ToolStripMenuItem menuItemPrint;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ToolStripStatusLabel statusProcess;
        private System.Windows.Forms.ToolStripProgressBar statusLoading;
        private Controls.ProcessDisplay processDisplay;
        private System.Windows.Forms.Label labelEntriesCount;
        private Mayfly.Mathematics.MathAdapter adapter;
    }
}