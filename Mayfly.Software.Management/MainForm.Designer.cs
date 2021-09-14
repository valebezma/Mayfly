namespace Mayfly.Software.Management
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.labelFileTip = new System.Windows.Forms.Label();
            this.labelFileFriendly = new System.Windows.Forms.Label();
            this.labelFilename = new System.Windows.Forms.Label();
            this.textBoxFileTip = new System.Windows.Forms.TextBox();
            this.textBoxFileFriendly = new System.Windows.Forms.TextBox();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.labelFileTitle = new System.Windows.Forms.Label();
            this.tabPageVersions = new System.Windows.Forms.TabPage();
            this.spreadSheetVer = new Mayfly.Controls.SpreadSheet();
            this.columnVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnVerPublished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnVerNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.spreadSheetSat = new Mayfly.Controls.SpreadSheet();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxExtDetails = new System.Windows.Forms.CheckBox();
            this.spreadSheetExt = new Mayfly.Controls.SpreadSheet();
            this.columnExt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtFriendly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtProgid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtPreviewTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtPreviewDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtFullDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelFeatureCount = new System.Windows.Forms.Label();
            this.labelProductCount = new System.Windows.Forms.Label();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.columnFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextFileRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewProducts = new System.Windows.Forms.ListView();
            this.columnProduct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextProductRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonProductAdd = new System.Windows.Forms.Button();
            this.inputProduct = new Mayfly.TaskDialogs.InputDialog(this.components);
            this.buttonFileAdd = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.columnSatID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSatPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnSatLocal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageVersions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetVer)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSat)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetExt)).BeginInit();
            this.contextFile.SuspendLayout();
            this.contextProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUpload,
            this.menuItemUpdate,
            this.menuItemClose});
            this.menuFile.Name = "menuFile";
            resources.ApplyResources(this.menuFile, "menuFile");
            this.menuFile.DropDownOpening += new System.EventHandler(this.menuFile_DropDownOpening);
            // 
            // menuItemUpload
            // 
            this.menuItemUpload.Name = "menuItemUpload";
            resources.ApplyResources(this.menuItemUpload, "menuItemUpload");
            this.menuItemUpload.Click += new System.EventHandler(this.menuItemUpload_Click);
            // 
            // menuItemUpdate
            // 
            this.menuItemUpdate.Name = "menuItemUpdate";
            resources.ApplyResources(this.menuItemUpdate, "menuItemUpdate");
            this.menuItemUpdate.Click += new System.EventHandler(this.menuItemUpdate_Click);
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageVersions);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.labelFileTip);
            this.tabPageGeneral.Controls.Add(this.labelFileFriendly);
            this.tabPageGeneral.Controls.Add(this.labelFilename);
            this.tabPageGeneral.Controls.Add(this.textBoxFileTip);
            this.tabPageGeneral.Controls.Add(this.textBoxFileFriendly);
            this.tabPageGeneral.Controls.Add(this.textBoxFilename);
            this.tabPageGeneral.Controls.Add(this.labelFileTitle);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // labelFileTip
            // 
            resources.ApplyResources(this.labelFileTip, "labelFileTip");
            this.labelFileTip.Name = "labelFileTip";
            // 
            // labelFileFriendly
            // 
            resources.ApplyResources(this.labelFileFriendly, "labelFileFriendly");
            this.labelFileFriendly.Name = "labelFileFriendly";
            // 
            // labelFilename
            // 
            resources.ApplyResources(this.labelFilename, "labelFilename");
            this.labelFilename.Name = "labelFilename";
            // 
            // textBoxFileTip
            // 
            this.textBoxFileTip.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxFileTip, "textBoxFileTip");
            this.textBoxFileTip.Name = "textBoxFileTip";
            this.textBoxFileTip.TextChanged += new System.EventHandler(this.textBoxFileTip_TextChanged);
            // 
            // textBoxFileFriendly
            // 
            resources.ApplyResources(this.textBoxFileFriendly, "textBoxFileFriendly");
            this.textBoxFileFriendly.Name = "textBoxFileFriendly";
            this.textBoxFileFriendly.TextChanged += new System.EventHandler(this.textBoxFileFriendly_TextChanged);
            // 
            // textBoxFilename
            // 
            resources.ApplyResources(this.textBoxFilename, "textBoxFilename");
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.TextChanged += new System.EventHandler(this.textBoxFilename_TextChanged);
            // 
            // labelFileTitle
            // 
            resources.ApplyResources(this.labelFileTitle, "labelFileTitle");
            this.labelFileTitle.Name = "labelFileTitle";
            // 
            // tabPageVersions
            // 
            this.tabPageVersions.Controls.Add(this.spreadSheetVer);
            resources.ApplyResources(this.tabPageVersions, "tabPageVersions");
            this.tabPageVersions.Name = "tabPageVersions";
            this.tabPageVersions.UseVisualStyleBackColor = true;
            // 
            // spreadSheetVer
            // 
            this.spreadSheetVer.AllowUserToAddRows = true;
            this.spreadSheetVer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.spreadSheetVer.CellPadding = new System.Windows.Forms.Padding(5);
            this.spreadSheetVer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnVer,
            this.columnVerPublished,
            this.columnVerNotes});
            resources.ApplyResources(this.spreadSheetVer, "spreadSheetVer");
            this.spreadSheetVer.Name = "spreadSheetVer";
            this.spreadSheetVer.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheet_CellEndEdit);
            this.spreadSheetVer.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetVer_CellValueChanged);
            this.spreadSheetVer.CurrentCellDirtyStateChanged += new System.EventHandler(this.spreadSheetVer_CurrentCellDirtyStateChanged);
            this.spreadSheetVer.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetVer_RowEnter);
            // 
            // columnVer
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.columnVer.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnVer, "columnVer");
            this.columnVer.Name = "columnVer";
            this.columnVer.ReadOnly = true;
            // 
            // columnVerPublished
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnVerPublished.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnVerPublished, "columnVerPublished");
            this.columnVerPublished.Name = "columnVerPublished";
            this.columnVerPublished.ReadOnly = true;
            // 
            // columnVerNotes
            // 
            this.columnVerNotes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnVerNotes.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnVerNotes, "columnVerNotes");
            this.columnVerNotes.Name = "columnVerNotes";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.spreadSheetSat);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // spreadSheetSat
            // 
            this.spreadSheetSat.AllowUserToAddRows = true;
            this.spreadSheetSat.AllowUserToDeleteRows = true;
            this.spreadSheetSat.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.spreadSheetSat.CellPadding = new System.Windows.Forms.Padding(5);
            this.spreadSheetSat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSatID,
            this.columnSatPath,
            this.columnSatLocal});
            resources.ApplyResources(this.spreadSheetSat, "spreadSheetSat");
            this.spreadSheetSat.Name = "spreadSheetSat";
            this.spreadSheetSat.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheet_CellEndEdit);
            this.spreadSheetSat.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetSat_CellValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxExtDetails);
            this.tabPage2.Controls.Add(this.spreadSheetExt);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxExtDetails
            // 
            resources.ApplyResources(this.checkBoxExtDetails, "checkBoxExtDetails");
            this.checkBoxExtDetails.Name = "checkBoxExtDetails";
            this.checkBoxExtDetails.UseVisualStyleBackColor = true;
            this.checkBoxExtDetails.CheckedChanged += new System.EventHandler(this.checkBoxExtDetails_CheckedChanged);
            // 
            // spreadSheetExt
            // 
            this.spreadSheetExt.AllowUserToAddRows = true;
            this.spreadSheetExt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.spreadSheetExt.CellPadding = new System.Windows.Forms.Padding(5);
            this.spreadSheetExt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnExt,
            this.columnExtFriendly,
            this.columnExtProgid,
            this.columnExtIndex,
            this.columnExtPreviewTitle,
            this.columnExtPreviewDetails,
            this.columnExtFullDetails});
            resources.ApplyResources(this.spreadSheetExt, "spreadSheetExt");
            this.spreadSheetExt.Name = "spreadSheetExt";
            this.spreadSheetExt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheet_CellEndEdit);
            this.spreadSheetExt.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetExt_CellValueChanged);
            // 
            // columnExt
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.columnExt.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.columnExt, "columnExt");
            this.columnExt.Name = "columnExt";
            // 
            // columnExtFriendly
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.Format = "G";
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.columnExtFriendly.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.columnExtFriendly, "columnExtFriendly");
            this.columnExtFriendly.Name = "columnExtFriendly";
            // 
            // columnExtProgid
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.columnExtProgid.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.columnExtProgid, "columnExtProgid");
            this.columnExtProgid.Name = "columnExtProgid";
            // 
            // columnExtIndex
            // 
            dataGridViewCellStyle8.Format = "N0";
            this.columnExtIndex.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.columnExtIndex, "columnExtIndex");
            this.columnExtIndex.Name = "columnExtIndex";
            // 
            // columnExtPreviewTitle
            // 
            this.columnExtPreviewTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.columnExtPreviewTitle, "columnExtPreviewTitle");
            this.columnExtPreviewTitle.Name = "columnExtPreviewTitle";
            // 
            // columnExtPreviewDetails
            // 
            this.columnExtPreviewDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.columnExtPreviewDetails, "columnExtPreviewDetails");
            this.columnExtPreviewDetails.Name = "columnExtPreviewDetails";
            // 
            // columnExtFullDetails
            // 
            this.columnExtFullDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.columnExtFullDetails, "columnExtFullDetails");
            this.columnExtFullDetails.Name = "columnExtFullDetails";
            // 
            // labelFeatureCount
            // 
            resources.ApplyResources(this.labelFeatureCount, "labelFeatureCount");
            this.labelFeatureCount.Name = "labelFeatureCount";
            // 
            // labelProductCount
            // 
            resources.ApplyResources(this.labelProductCount, "labelProductCount");
            this.labelProductCount.Name = "labelProductCount";
            // 
            // listViewFiles
            // 
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFile});
            this.listViewFiles.ContextMenuStrip = this.contextFile;
            this.listViewFiles.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewFiles.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewFiles.Groups1")))});
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.TileSize = new System.Drawing.Size(150, 25);
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewFiles_ItemChecked);
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // columnFile
            // 
            resources.ApplyResources(this.columnFile, "columnFile");
            // 
            // contextFile
            // 
            this.contextFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextFileRemove});
            this.contextFile.Name = "contextFile";
            resources.ApplyResources(this.contextFile, "contextFile");
            this.contextFile.Opening += new System.ComponentModel.CancelEventHandler(this.contextFile_Opening);
            // 
            // contextFileRemove
            // 
            this.contextFileRemove.Name = "contextFileRemove";
            resources.ApplyResources(this.contextFileRemove, "contextFileRemove");
            this.contextFileRemove.Click += new System.EventHandler(this.contextFileRemove_Click);
            // 
            // listViewProducts
            // 
            this.listViewProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProduct});
            this.listViewProducts.ContextMenuStrip = this.contextProduct;
            this.listViewProducts.FullRowSelect = true;
            this.listViewProducts.HideSelection = false;
            resources.ApplyResources(this.listViewProducts, "listViewProducts");
            this.listViewProducts.MultiSelect = false;
            this.listViewProducts.Name = "listViewProducts";
            this.listViewProducts.ShowGroups = false;
            this.listViewProducts.TileSize = new System.Drawing.Size(150, 25);
            this.listViewProducts.UseCompatibleStateImageBehavior = false;
            this.listViewProducts.View = System.Windows.Forms.View.Details;
            this.listViewProducts.SelectedIndexChanged += new System.EventHandler(this.listViewProducts_SelectedIndexChanged);
            // 
            // columnProduct
            // 
            resources.ApplyResources(this.columnProduct, "columnProduct");
            // 
            // contextProduct
            // 
            this.contextProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextProductRemove});
            this.contextProduct.Name = "contextProduct";
            resources.ApplyResources(this.contextProduct, "contextProduct");
            this.contextProduct.Opening += new System.ComponentModel.CancelEventHandler(this.contextProduct_Opening);
            // 
            // contextProductRemove
            // 
            this.contextProductRemove.Name = "contextProductRemove";
            resources.ApplyResources(this.contextProductRemove, "contextProductRemove");
            this.contextProductRemove.Click += new System.EventHandler(this.contextProductRemove_Click);
            // 
            // buttonProductAdd
            // 
            resources.ApplyResources(this.buttonProductAdd, "buttonProductAdd");
            this.buttonProductAdd.Name = "buttonProductAdd";
            this.buttonProductAdd.UseVisualStyleBackColor = true;
            this.buttonProductAdd.Click += new System.EventHandler(this.buttonProductAdd_Click);
            // 
            // inputProduct
            // 
            resources.ApplyResources(this.inputProduct, "inputProduct");
            // 
            // buttonFileAdd
            // 
            resources.ApplyResources(this.buttonFileAdd, "buttonFileAdd");
            this.buttonFileAdd.Name = "buttonFileAdd";
            this.buttonFileAdd.UseVisualStyleBackColor = true;
            this.buttonFileAdd.Click += new System.EventHandler(this.buttonFileAdd_Click);
            // 
            // openFile
            // 
            resources.ApplyResources(this.openFile, "openFile");
            // 
            // columnSatID
            // 
            resources.ApplyResources(this.columnSatID, "columnSatID");
            this.columnSatID.Name = "columnSatID";
            // 
            // columnSatPath
            // 
            this.columnSatPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.columnSatPath.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnSatPath, "columnSatPath");
            this.columnSatPath.Name = "columnSatPath";
            // 
            // columnSatLocal
            // 
            resources.ApplyResources(this.columnSatLocal, "columnSatLocal");
            this.columnSatLocal.Name = "columnSatLocal";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonFileAdd);
            this.Controls.Add(this.buttonProductAdd);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelFeatureCount);
            this.Controls.Add(this.labelProductCount);
            this.Controls.Add(this.listViewProducts);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageVersions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetVer)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSat)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetExt)).EndInit();
            this.contextFile.ResumeLayout(false);
            this.contextProduct.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageVersions;
        private System.Windows.Forms.ToolStripMenuItem menuItemUpload;
        private System.Windows.Forms.Label labelProductCount;
        private System.Windows.Forms.ListView listViewProducts;
        private System.Windows.Forms.Label labelFeatureCount;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader columnFile;
        private System.Windows.Forms.ContextMenuStrip contextProduct;
        private System.Windows.Forms.ToolStripMenuItem contextProductRemove;
        private Controls.SpreadSheet spreadSheetVer;
        private System.Windows.Forms.Label labelFileTitle;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label labelFileTip;
        private System.Windows.Forms.Label labelFileFriendly;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.TextBox textBoxFileTip;
        private System.Windows.Forms.TextBox textBoxFileFriendly;
        private System.Windows.Forms.TextBox textBoxFilename;
        private Controls.SpreadSheet spreadSheetExt;
        private Controls.SpreadSheet spreadSheetSat;
        private System.Windows.Forms.CheckBox checkBoxExtDetails;
        private System.Windows.Forms.Button buttonProductAdd;
        private TaskDialogs.InputDialog inputProduct;
        private System.Windows.Forms.Button buttonFileAdd;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.ContextMenuStrip contextFile;
        private System.Windows.Forms.ToolStripMenuItem contextFileRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnVerPublished;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnVerNotes;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ColumnHeader columnProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExt;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtFriendly;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtProgid;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtPreviewTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtPreviewDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtFullDetails;
        private System.Windows.Forms.ToolStripMenuItem menuItemUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSatID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSatPath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnSatLocal;
    }
}

