namespace Mayfly.Fish.Explorer
{
    partial class Individuals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Individuals));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMass = new System.Windows.Forms.NumericUpDown();
            this.labelMass = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.contextMenuStripInd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.contextMenuStripAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemNewVar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.somaticWeightGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gonadWeightGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecunditySampleWeightGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemStratifiedSample = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxWarningMass = new System.Windows.Forms.PictureBox();
            this.pictureBoxWarningQuantity = new System.Windows.Forms.PictureBox();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.toolTipAttention = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripVar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemClearColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSaveVars = new System.Windows.Forms.ToolStripMenuItem();
            this.stratifiedSample = new Mayfly.Wild.Controls.StratifiedSample();
            this.spreadSheetLog = new Mayfly.Controls.SpreadSheet();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSomaticMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRegID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaturity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGonadMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGonadSampleMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskDialogSave = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSaveAllIndividuals = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMass)).BeginInit();
            this.contextMenuStripInd.SuspendLayout();
            this.contextMenuStripAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningMass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningQuantity)).BeginInit();
            this.contextMenuStripVar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).BeginInit();
            this.SuspendLayout();
            // 
            // labelQuantity
            // 
            resources.ApplyResources(this.labelQuantity, "labelQuantity");
            this.labelQuantity.Name = "labelQuantity";
            // 
            // numericUpDownQuantity
            // 
            resources.ApplyResources(this.numericUpDownQuantity, "numericUpDownQuantity");
            this.numericUpDownQuantity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownQuantity.Name = "numericUpDownQuantity";
            this.numericUpDownQuantity.ValueChanged += new System.EventHandler(this.numericUpDownQuantity_ValueChanged);
            // 
            // numericUpDownMass
            // 
            resources.ApplyResources(this.numericUpDownMass, "numericUpDownMass");
            this.numericUpDownMass.DecimalPlaces = 3;
            this.numericUpDownMass.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownMass.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownMass.Name = "numericUpDownMass";
            this.numericUpDownMass.ValueChanged += new System.EventHandler(this.numericUpDownMass_ValueChanged);
            // 
            // labelMass
            // 
            resources.ApplyResources(this.labelMass, "labelMass");
            this.labelMass.Name = "labelMass";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // contextMenuStripInd
            // 
            this.contextMenuStripInd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDelete,
            this.toolStripSeparator5,
            this.ToolStripMenuItemCut,
            this.ToolStripMenuItemCopy,
            this.ToolStripMenuItemPaste});
            this.contextMenuStripInd.Name = "contextMenuStripInd";
            resources.ApplyResources(this.contextMenuStripInd, "contextMenuStripInd");
            // 
            // ToolStripMenuItemDelete
            // 
            resources.ApplyResources(this.ToolStripMenuItemDelete, "ToolStripMenuItemDelete");
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // ToolStripMenuItemCut
            // 
            resources.ApplyResources(this.ToolStripMenuItemCut, "ToolStripMenuItemCut");
            this.ToolStripMenuItemCut.Name = "ToolStripMenuItemCut";
            this.ToolStripMenuItemCut.Click += new System.EventHandler(this.ToolStripMenuItemCut_Click);
            // 
            // ToolStripMenuItemCopy
            // 
            resources.ApplyResources(this.ToolStripMenuItemCopy, "ToolStripMenuItemCopy");
            this.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            this.ToolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
            // 
            // ToolStripMenuItemPaste
            // 
            resources.ApplyResources(this.ToolStripMenuItemPaste, "ToolStripMenuItemPaste");
            this.ToolStripMenuItemPaste.Name = "ToolStripMenuItemPaste";
            this.ToolStripMenuItemPaste.Click += new System.EventHandler(this.ToolStripMenuItemPaste_Click);
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // contextMenuStripAdd
            // 
            this.contextMenuStripAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNewVar,
            this.toolStripSeparator1,
            this.somaticWeightGToolStripMenuItem,
            this.gonadWeightGToolStripMenuItem,
            this.fecunditySampleWeightGToolStripMenuItem,
            this.toolStripSeparator4,
            this.ToolStripMenuItemStratifiedSample});
            this.contextMenuStripAdd.Name = "contextMenuStripAdd";
            resources.ApplyResources(this.contextMenuStripAdd, "contextMenuStripAdd");
            // 
            // ToolStripMenuItemNewVar
            // 
            this.ToolStripMenuItemNewVar.Name = "ToolStripMenuItemNewVar";
            resources.ApplyResources(this.ToolStripMenuItemNewVar, "ToolStripMenuItemNewVar");
            this.ToolStripMenuItemNewVar.Click += new System.EventHandler(this.ToolStripMenuItemNewVar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // somaticWeightGToolStripMenuItem
            // 
            this.somaticWeightGToolStripMenuItem.Name = "somaticWeightGToolStripMenuItem";
            resources.ApplyResources(this.somaticWeightGToolStripMenuItem, "somaticWeightGToolStripMenuItem");
            this.somaticWeightGToolStripMenuItem.Click += new System.EventHandler(this.somaticWeightGToolStripMenuItem_Click);
            // 
            // gonadWeightGToolStripMenuItem
            // 
            this.gonadWeightGToolStripMenuItem.Name = "gonadWeightGToolStripMenuItem";
            resources.ApplyResources(this.gonadWeightGToolStripMenuItem, "gonadWeightGToolStripMenuItem");
            this.gonadWeightGToolStripMenuItem.Click += new System.EventHandler(this.gonadWeightGToolStripMenuItem_Click);
            // 
            // fecunditySampleWeightGToolStripMenuItem
            // 
            this.fecunditySampleWeightGToolStripMenuItem.Name = "fecunditySampleWeightGToolStripMenuItem";
            resources.ApplyResources(this.fecunditySampleWeightGToolStripMenuItem, "fecunditySampleWeightGToolStripMenuItem");
            this.fecunditySampleWeightGToolStripMenuItem.Click += new System.EventHandler(this.fecunditySampleWeightToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // ToolStripMenuItemStratifiedSample
            // 
            resources.ApplyResources(this.ToolStripMenuItemStratifiedSample, "ToolStripMenuItemStratifiedSample");
            this.ToolStripMenuItemStratifiedSample.Name = "ToolStripMenuItemStratifiedSample";
            this.ToolStripMenuItemStratifiedSample.Click += new System.EventHandler(this.ToolStripMenuItemStratifiedSample_Click);
            // 
            // pictureBoxWarningMass
            // 
            resources.ApplyResources(this.pictureBoxWarningMass, "pictureBoxWarningMass");
            this.pictureBoxWarningMass.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarningMass.Image = Pictogram.Warning;
            this.pictureBoxWarningMass.Name = "pictureBoxWarningMass";
            this.pictureBoxWarningMass.TabStop = false;
            this.pictureBoxWarningMass.DoubleClick += new System.EventHandler(this.pictureBoxWarningMass_DoubleClick);
            this.pictureBoxWarningMass.MouseLeave += new System.EventHandler(this.pictureBoxWarningM_MouseLeave);
            this.pictureBoxWarningMass.MouseHover += new System.EventHandler(this.pictureBoxWarningM_MouseHover);
            // 
            // pictureBoxWarningQuantity
            // 
            resources.ApplyResources(this.pictureBoxWarningQuantity, "pictureBoxWarningQuantity");
            this.pictureBoxWarningQuantity.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarningQuantity.Image = Pictogram.Warning;
            this.pictureBoxWarningQuantity.Name = "pictureBoxWarningQuantity";
            this.pictureBoxWarningQuantity.TabStop = false;
            this.pictureBoxWarningQuantity.DoubleClick += new System.EventHandler(this.pictureBoxWarningQuantity_DoubleClick);
            this.pictureBoxWarningQuantity.MouseLeave += new System.EventHandler(this.pictureBoxWarningQ_MouseLeave);
            this.pictureBoxWarningQuantity.MouseHover += new System.EventHandler(this.pictureBoxWarningQ_MouseHover);
            // 
            // textBoxComments
            // 
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.TextChanged += new System.EventHandler(this.textBoxComments_TextChanged);
            // 
            // labelComments
            // 
            resources.ApplyResources(this.labelComments, "labelComments");
            this.labelComments.Name = "labelComments";
            // 
            // contextMenuStripVar
            // 
            this.contextMenuStripVar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemClearColumn,
            this.toolStripSeparator2,
            this.ToolStripMenuItemSaveVars});
            this.contextMenuStripVar.Name = "contextMenuStripVar";
            resources.ApplyResources(this.contextMenuStripVar, "contextMenuStripVar");
            this.contextMenuStripVar.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripVar_Opening);
            // 
            // ToolStripMenuItemClearColumn
            // 
            this.ToolStripMenuItemClearColumn.Name = "ToolStripMenuItemClearColumn";
            resources.ApplyResources(this.ToolStripMenuItemClearColumn, "ToolStripMenuItemClearColumn");
            this.ToolStripMenuItemClearColumn.Click += new System.EventHandler(this.ToolStripMenuItemClearColumn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // ToolStripMenuItemSaveVars
            // 
            this.ToolStripMenuItemSaveVars.Name = "ToolStripMenuItemSaveVars";
            resources.ApplyResources(this.ToolStripMenuItemSaveVars, "ToolStripMenuItemSaveVars");
            this.ToolStripMenuItemSaveVars.Click += new System.EventHandler(this.ToolStripMenuItemSaveVars_Click);
            // 
            // stratifiedSample
            // 
            resources.ApplyResources(this.stratifiedSample, "stratifiedSample");
            this.stratifiedSample.End = 250D;
            this.stratifiedSample.Interval = 10D;
            this.stratifiedSample.Name = "stratifiedSample";
            this.stratifiedSample.ParkingLot = 15;
            this.stratifiedSample.Start = 200D;
            this.stratifiedSample.ValueChanged += new System.EventHandler(this.Range_ValueChanged);
            // 
            // spreadSheetLog
            // 
            this.spreadSheetLog.AllowStringSuggection = System.Windows.Forms.AutoCompleteMode.Append;
            this.spreadSheetLog.AllowUserToAddRows = true;
            this.spreadSheetLog.AllowUserToDeleteColumns = true;
            this.spreadSheetLog.AllowUserToDeleteRows = true;
            resources.ApplyResources(this.spreadSheetLog, "spreadSheetLog");
            this.spreadSheetLog.ColumnMenu = this.contextMenuStripVar;
            this.spreadSheetLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnLength,
            this.ColumnMass,
            this.ColumnSomaticMass,
            this.ColumnRegID,
            this.ColumnSex,
            this.ColumnMaturity,
            this.ColumnGonadMass,
            this.ColumnGonadSampleMass,
            this.ColumnComments});
            this.spreadSheetLog.DefaultDecimalPlaces = 0;
            this.spreadSheetLog.Name = "spreadSheetLog";
            this.spreadSheetLog.RowMenu = this.contextMenuStripInd;
            this.spreadSheetLog.RowMenuLaunchableItemIndex = 0;
            this.spreadSheetLog.ColumnRenamed += new Mayfly.Controls.GridColumnRenameEventHandler(this.spreadSheetLog_ColumnRenamed);
            this.spreadSheetLog.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetLog_CellEnter);
            this.spreadSheetLog.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetLog_CellValueChanged);
            this.spreadSheetLog.ColumnRemoved += new System.Windows.Forms.DataGridViewColumnEventHandler(this.spreadSheetLog_ColumnRemoved);
            this.spreadSheetLog.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.spreadSheetLog_UserDeletingRow);
            this.spreadSheetLog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.spreadSheetLog_KeyPress);
            this.spreadSheetLog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.spreadSheetLog_MouseClick);
            // 
            // ColumnID
            // 
            resources.ApplyResources(this.ColumnID, "ColumnID");
            this.ColumnID.Name = "ColumnID";
            // 
            // ColumnLength
            // 
            dataGridViewCellStyle1.NullValue = null;
            this.ColumnLength.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnLength, "ColumnLength");
            this.ColumnLength.Name = "ColumnLength";
            // 
            // ColumnMass
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.ColumnMass.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnMass, "ColumnMass");
            this.ColumnMass.Name = "ColumnMass";
            // 
            // ColumnSomaticMass
            // 
            resources.ApplyResources(this.ColumnSomaticMass, "ColumnSomaticMass");
            this.ColumnSomaticMass.Name = "ColumnSomaticMass";
            // 
            // ColumnRegID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnRegID.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnRegID, "ColumnRegID");
            this.ColumnRegID.Name = "ColumnRegID";
            // 
            // ColumnSex
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnSex.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnSex, "ColumnSex");
            this.ColumnSex.Name = "ColumnSex";
            // 
            // ColumnMaturity
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = null;
            this.ColumnMaturity.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnMaturity, "ColumnMaturity");
            this.ColumnMaturity.Name = "ColumnMaturity";
            // 
            // ColumnGonadMass
            // 
            resources.ApplyResources(this.ColumnGonadMass, "ColumnGonadMass");
            this.ColumnGonadMass.Name = "ColumnGonadMass";
            // 
            // ColumnGonadSampleMass
            // 
            resources.ApplyResources(this.ColumnGonadSampleMass, "ColumnGonadSampleMass");
            this.ColumnGonadSampleMass.Name = "ColumnGonadSampleMass";
            // 
            // ColumnComments
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.ColumnComments.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnComments, "ColumnComments");
            this.ColumnComments.Name = "ColumnComments";
            // 
            // taskDialogSave
            // 
            this.taskDialogSave.Buttons.Add(this.tdbSaveAllIndividuals);
            this.taskDialogSave.Buttons.Add(this.tdbDiscard);
            this.taskDialogSave.Buttons.Add(this.tdbCancelClose);
            this.taskDialogSave.CenterParent = true;
            resources.ApplyResources(this.taskDialogSave, "taskDialogSave");
            // 
            // tdbSaveAllIndividuals
            // 
            resources.ApplyResources(this.tdbSaveAllIndividuals, "tdbSaveAllIndividuals");
            // 
            // tdbDiscard
            // 
            resources.ApplyResources(this.tdbDiscard, "tdbDiscard");
            // 
            // tdbCancelClose
            // 
            this.tdbCancelClose.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // Individuals
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.pictureBoxWarningMass);
            this.Controls.Add(this.pictureBoxWarningQuantity);
            this.Controls.Add(this.stratifiedSample);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDownMass);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelMass);
            this.Controls.Add(this.numericUpDownQuantity);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.spreadSheetLog);
            this.Name = "Individuals";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Individuals_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMass)).EndInit();
            this.contextMenuStripInd.ResumeLayout(false);
            this.contextMenuStripAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningMass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningQuantity)).EndInit();
            this.contextMenuStripVar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
        private System.Windows.Forms.NumericUpDown numericUpDownMass;
        private System.Windows.Forms.Label labelMass;
        private System.Windows.Forms.Button buttonOK;
        private Mayfly.Controls.SpreadSheet spreadSheetLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPaste;
        private Wild.Controls.StratifiedSample stratifiedSample;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAdd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStratifiedSample;
        private System.Windows.Forms.PictureBox pictureBoxWarningMass;
        private System.Windows.Forms.PictureBox pictureBoxWarningQuantity;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCut;
        private TaskDialogs.TaskDialog taskDialogSave;
        private TaskDialogs.TaskDialogButton tdbSaveAllIndividuals;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        public System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.ToolTip toolTipAttention;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNewVar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVar;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClearColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveVars;
        private System.Windows.Forms.ToolStripMenuItem somaticWeightGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gonadWeightGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecunditySampleWeightGToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSomaticMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRegID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaturity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGonadMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGonadSampleMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComments;
    }
}