namespace Mayfly.Bacterioplankton
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStripValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemClearValue = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPasteValue = new System.Windows.Forms.ToolStripMenuItem();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.contextMenuStripAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemNewVar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemLength = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddQ = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripVar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemClearColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSaveVars = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxWarningQuantity = new System.Windows.Forms.PictureBox();
            this.contextMenuStripInd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemInd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonBlank = new System.Windows.Forms.Button();
            this.spreadSheetLog = new Mayfly.Controls.SpreadSheet();
            this.dataGridViewColumn1 = new System.Windows.Forms.DataGridViewColumn();
            this.taskDialogSave = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSaveAllIndividuals = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.inputDialogDupl = new Mayfly.TaskDialogs.InputDialog(this.components);
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
            this.contextMenuStripAdd.SuspendLayout();
            this.contextMenuStripVar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningQuantity)).BeginInit();
            this.contextMenuStripInd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripValue
            // 
            resources.ApplyResources(this.contextMenuStripValue, "contextMenuStripValue");
            this.contextMenuStripValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemClearValue,
            this.ToolStripMenuItemCopyValue,
            this.ToolStripMenuItemPasteValue});
            this.contextMenuStripValue.Name = "contextMenuStripTranslate";
            this.contextMenuStripValue.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripValue_Opening);
            // 
            // ToolStripMenuItemClearValue
            // 
            resources.ApplyResources(this.ToolStripMenuItemClearValue, "ToolStripMenuItemClearValue");
            this.ToolStripMenuItemClearValue.Name = "ToolStripMenuItemClearValue";
            this.ToolStripMenuItemClearValue.Click += new System.EventHandler(this.ToolStripMenuItemClearValue_Click);
            // 
            // ToolStripMenuItemCopyValue
            // 
            resources.ApplyResources(this.ToolStripMenuItemCopyValue, "ToolStripMenuItemCopyValue");
            this.ToolStripMenuItemCopyValue.Name = "ToolStripMenuItemCopyValue";
            this.ToolStripMenuItemCopyValue.Click += new System.EventHandler(this.ToolStripMenuItemCopyValue_Click);
            // 
            // ToolStripMenuItemPasteValue
            // 
            resources.ApplyResources(this.ToolStripMenuItemPasteValue, "ToolStripMenuItemPasteValue");
            this.ToolStripMenuItemPasteValue.Name = "ToolStripMenuItemPasteValue";
            this.ToolStripMenuItemPasteValue.Click += new System.EventHandler(this.ToolStripMenuItemPasteValue_Click);
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
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // contextMenuStripAdd
            // 
            resources.ApplyResources(this.contextMenuStripAdd, "contextMenuStripAdd");
            this.contextMenuStripAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNewVar,
            this.toolStripSeparator1,
            this.ToolStripMenuItemLength,
            this.menuItemAddQ});
            this.contextMenuStripAdd.Name = "contextMenuStripParameters";
            // 
            // ToolStripMenuItemNewVar
            // 
            resources.ApplyResources(this.ToolStripMenuItemNewVar, "ToolStripMenuItemNewVar");
            this.ToolStripMenuItemNewVar.Name = "ToolStripMenuItemNewVar";
            this.ToolStripMenuItemNewVar.Click += new System.EventHandler(this.ToolStripMenuItemNewVar_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // ToolStripMenuItemLength
            // 
            resources.ApplyResources(this.ToolStripMenuItemLength, "ToolStripMenuItemLength");
            this.ToolStripMenuItemLength.Name = "ToolStripMenuItemLength";
            this.ToolStripMenuItemLength.Click += new System.EventHandler(this.ToolStripMenuItemLength_Click);
            // 
            // menuItemAddQ
            // 
            resources.ApplyResources(this.menuItemAddQ, "menuItemAddQ");
            this.menuItemAddQ.Name = "menuItemAddQ";
            this.menuItemAddQ.Click += new System.EventHandler(this.menuItemAddQ_Click);
            // 
            // contextMenuStripVar
            // 
            resources.ApplyResources(this.contextMenuStripVar, "contextMenuStripVar");
            this.contextMenuStripVar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemRemove,
            this.ToolStripMenuItemClearColumn,
            this.toolStripSeparator2,
            this.ToolStripMenuItemSaveVars});
            this.contextMenuStripVar.Name = "contextMenuStripVar";
            this.contextMenuStripVar.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripVar_Opening);
            // 
            // ToolStripMenuItemRemove
            // 
            resources.ApplyResources(this.ToolStripMenuItemRemove, "ToolStripMenuItemRemove");
            this.ToolStripMenuItemRemove.Name = "ToolStripMenuItemRemove";
            this.ToolStripMenuItemRemove.Click += new System.EventHandler(this.ToolStripMenuItemRemove_Click);
            // 
            // ToolStripMenuItemClearColumn
            // 
            resources.ApplyResources(this.ToolStripMenuItemClearColumn, "ToolStripMenuItemClearColumn");
            this.ToolStripMenuItemClearColumn.Name = "ToolStripMenuItemClearColumn";
            this.ToolStripMenuItemClearColumn.Click += new System.EventHandler(this.ToolStripMenuItemClearColumn_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // ToolStripMenuItemSaveVars
            // 
            resources.ApplyResources(this.ToolStripMenuItemSaveVars, "ToolStripMenuItemSaveVars");
            this.ToolStripMenuItemSaveVars.Name = "ToolStripMenuItemSaveVars";
            this.ToolStripMenuItemSaveVars.Click += new System.EventHandler(this.ToolStripMenuItemSaveVars_Click);
            // 
            // pictureBoxWarningQuantity
            // 
            resources.ApplyResources(this.pictureBoxWarningQuantity, "pictureBoxWarningQuantity");
            this.pictureBoxWarningQuantity.BackColor = System.Drawing.Color.White;
            this.pictureBoxWarningQuantity.Name = "pictureBoxWarningQuantity";
            this.pictureBoxWarningQuantity.TabStop = false;
            this.pictureBoxWarningQuantity.DoubleClick += new System.EventHandler(this.pictureBoxWarningQuantity_DoubleClick);
            // 
            // contextMenuStripInd
            // 
            resources.ApplyResources(this.contextMenuStripInd, "contextMenuStripInd");
            this.contextMenuStripInd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemInd,
            this.ToolStripMenuItemDuplicate,
            this.ToolStripMenuItemDelete,
            this.toolStripSeparator5,
            this.ToolStripMenuItemCut,
            this.ToolStripMenuItemCopy,
            this.ToolStripMenuItemPaste});
            this.contextMenuStripInd.Name = "contextMenuStripInd";
            // 
            // ToolStripMenuItemInd
            // 
            resources.ApplyResources(this.ToolStripMenuItemInd, "ToolStripMenuItemInd");
            this.ToolStripMenuItemInd.Name = "ToolStripMenuItemInd";
            this.ToolStripMenuItemInd.Click += new System.EventHandler(this.ToolStripMenuItemInd_Click);
            // 
            // ToolStripMenuItemDuplicate
            // 
            resources.ApplyResources(this.ToolStripMenuItemDuplicate, "ToolStripMenuItemDuplicate");
            this.ToolStripMenuItemDuplicate.Name = "ToolStripMenuItemDuplicate";
            this.ToolStripMenuItemDuplicate.Click += new System.EventHandler(this.ToolStripMenuItemDuplicate_Click);
            // 
            // ToolStripMenuItemDelete
            // 
            resources.ApplyResources(this.ToolStripMenuItemDelete, "ToolStripMenuItemDelete");
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
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
            // buttonReport
            // 
            resources.ApplyResources(this.buttonReport, "buttonReport");
            this.buttonReport.FlatAppearance.BorderSize = 0;
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonBlank
            // 
            resources.ApplyResources(this.buttonBlank, "buttonBlank");
            this.buttonBlank.FlatAppearance.BorderSize = 0;
            this.buttonBlank.Name = "buttonBlank";
            this.buttonBlank.UseVisualStyleBackColor = true;
            this.buttonBlank.Click += new System.EventHandler(this.buttonBlank_Click);
            // 
            // spreadSheetLog
            // 
            resources.ApplyResources(this.spreadSheetLog, "spreadSheetLog");
            this.spreadSheetLog.AllowColumnRenaming = true;
            this.spreadSheetLog.AllowUserToAddRows = true;
            this.spreadSheetLog.AllowUserToDeleteColumns = true;
            this.spreadSheetLog.AllowUserToDeleteRows = true;
            this.spreadSheetLog.AllowUserToResizeColumns = false;
            this.spreadSheetLog.AutoClearEmptyRows = true;
            this.spreadSheetLog.CellMenu = this.contextMenuStripValue;
            this.spreadSheetLog.ColumnMenu = this.contextMenuStripVar;
            this.spreadSheetLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnLength,
            this.ColumnWidth,
            this.ColumnFrequency,
            this.ColumnComments});
            this.spreadSheetLog.DefaultDecimalPlaces = 0;
            this.spreadSheetLog.Name = "spreadSheetLog";
            this.spreadSheetLog.RowMenu = this.contextMenuStripInd;
            this.spreadSheetLog.ColumnRenamed += new Mayfly.Controls.GridColumnRenameEventHandler(this.spreadSheetLog_ColumnRenamed);
            this.spreadSheetLog.RowRemoving += new System.Windows.Forms.DataGridViewRowEventHandler(this.spreadSheetLog_RowRemoving);
            this.spreadSheetLog.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetLog_CellEnter);
            this.spreadSheetLog.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetLog_CellValueChanged);
            this.spreadSheetLog.ColumnRemoved += new System.Windows.Forms.DataGridViewColumnEventHandler(this.spreadSheetLog_ColumnRemoved);
            this.spreadSheetLog.CurrentCellChanged += new System.EventHandler(this.spreadSheetLog_CurrentCellChanged);
            this.spreadSheetLog.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.spreadSheetLog_UserDeletingRow);
            this.spreadSheetLog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.spreadSheetLog_MouseClick);
            // 
            // dataGridViewColumn1
            // 
            resources.ApplyResources(this.dataGridViewColumn1, "dataGridViewColumn1");
            this.dataGridViewColumn1.Name = "dataGridViewColumn1";
            // 
            // taskDialogSave
            // 
            this.taskDialogSave.Buttons.Add(this.tdbSaveAllIndividuals);
            this.taskDialogSave.Buttons.Add(this.tdbCancelClose);
            this.taskDialogSave.Buttons.Add(this.tdbDiscard);
            this.taskDialogSave.CenterParent = true;
            resources.ApplyResources(this.taskDialogSave, "taskDialogSave");
            // 
            // tdbSaveAllIndividuals
            // 
            resources.ApplyResources(this.tdbSaveAllIndividuals, "tdbSaveAllIndividuals");
            // 
            // tdbCancelClose
            // 
            this.tdbCancelClose.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            resources.ApplyResources(this.tdbCancelClose, "tdbCancelClose");
            // 
            // tdbDiscard
            // 
            resources.ApplyResources(this.tdbDiscard, "tdbDiscard");
            // 
            // inputDialogDupl
            // 
            resources.ApplyResources(this.inputDialogDupl, "inputDialogDupl");
            this.inputDialogDupl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Value_KeyPress);
            // 
            // ColumnID
            // 
            resources.ApplyResources(this.ColumnID, "ColumnID");
            this.ColumnID.Name = "ColumnID";
            // 
            // ColumnLength
            // 
            dataGridViewCellStyle5.Format = "N1";
            this.ColumnLength.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnLength, "ColumnLength");
            this.ColumnLength.Name = "ColumnLength";
            // 
            // ColumnWidth
            // 
            dataGridViewCellStyle6.Format = "N1";
            this.ColumnWidth.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnWidth, "ColumnWidth");
            this.ColumnWidth.Name = "ColumnWidth";
            // 
            // ColumnFrequency
            // 
            dataGridViewCellStyle7.Format = "N0";
            this.ColumnFrequency.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnFrequency, "ColumnFrequency");
            this.ColumnFrequency.Name = "ColumnFrequency";
            // 
            // ColumnComments
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnComments.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnComments.FillWeight = 153.1616F;
            resources.ApplyResources(this.ColumnComments, "ColumnComments");
            this.ColumnComments.Name = "ColumnComments";
            // 
            // Individuals
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonBlank);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.spreadSheetLog);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.numericUpDownQuantity);
            this.Controls.Add(this.pictureBoxWarningQuantity);
            this.Name = "Individuals";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Individuals_FormClosing);
            this.contextMenuStripValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
            this.contextMenuStripAdd.ResumeLayout(false);
            this.contextMenuStripVar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarningQuantity)).EndInit();
            this.contextMenuStripInd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mayfly.Controls.SpreadSheet spreadSheetLog;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripValue;
        private System.Windows.Forms.PictureBox pictureBoxWarningQuantity;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAdd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNewVar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVar;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRemove;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveVars;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClearValue;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemInd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCut;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClearColumn;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLength;
        public System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Label labelComments;
        private TaskDialogs.TaskDialog taskDialogSave;
        private TaskDialogs.TaskDialogButton tdbSaveAllIndividuals;
        private TaskDialogs.TaskDialogButton tdbCancelClose;
        private TaskDialogs.TaskDialogButton tdbDiscard;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonBlank;
        private TaskDialogs.InputDialog inputDialogDupl;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPasteValue;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopyValue;
        private System.Windows.Forms.DataGridViewColumn dataGridViewColumn1;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddQ;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDuplicate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFrequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComments;
    }
}