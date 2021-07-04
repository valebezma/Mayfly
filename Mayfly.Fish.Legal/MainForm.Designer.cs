namespace Mayfly.Fish.Legal
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Акты проведения лова", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Акты возвращения ВБР в среду обитания", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Акты уничтожения ВБР", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Акты отбора образцов и живых особей ВБР для транспортировки", System.Windows.Forms.HorizontalAlignment.Left);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonAddNote = new System.Windows.Forms.Button();
            this.listViewNotes = new System.Windows.Forms.ListView();
            this.columnNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLicense = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddLicense = new System.Windows.Forms.Button();
            this.spreadSheetCatches = new Mayfly.Controls.SpreadSheet();
            this.ColumnSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFraction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.Paper = new Mayfly.Fish.Legal.LegalPapers();
            this.contextNote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextNotePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Paper)).BeginInit();
            this.contextNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 590);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonAddNote);
            this.tabPage1.Controls.Add(this.listViewNotes);
            this.tabPage1.Controls.Add(this.buttonAddLicense);
            this.tabPage1.Controls.Add(this.spreadSheetCatches);
            this.tabPage1.Controls.Add(this.listViewLicenses);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(45);
            this.tabPage1.Size = new System.Drawing.Size(752, 564);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Разрешения и квоты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonAddNote
            // 
            this.buttonAddNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddNote.Enabled = false;
            this.buttonAddNote.Location = new System.Drawing.Point(229, 222);
            this.buttonAddNote.Name = "buttonAddNote";
            this.buttonAddNote.Size = new System.Drawing.Size(175, 23);
            this.buttonAddNote.TabIndex = 17;
            this.buttonAddNote.Text = "Составить акты";
            this.buttonAddNote.UseVisualStyleBackColor = true;
            this.buttonAddNote.Click += new System.EventHandler(this.buttonAddNote_Click);
            // 
            // listViewNotes
            // 
            this.listViewNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNo,
            this.columnDate,
            this.columnLicense});
            this.listViewNotes.ContextMenuStrip = this.contextNote;
            this.listViewNotes.FullRowSelect = true;
            listViewGroup1.Header = "Акты проведения лова";
            listViewGroup1.Name = "groupCatch";
            listViewGroup2.Header = "Акты возвращения ВБР в среду обитания";
            listViewGroup2.Name = "groupRelease";
            listViewGroup3.Header = "Акты уничтожения ВБР";
            listViewGroup3.Name = "groupUtilization";
            listViewGroup4.Header = "Акты отбора образцов и живых особей ВБР для транспортировки";
            listViewGroup4.Name = "groupTransport";
            this.listViewNotes.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.listViewNotes.Location = new System.Drawing.Point(229, 48);
            this.listViewNotes.Name = "listViewNotes";
            this.listViewNotes.Size = new System.Drawing.Size(475, 168);
            this.listViewNotes.TabIndex = 16;
            this.listViewNotes.TileSize = new System.Drawing.Size(150, 25);
            this.listViewNotes.UseCompatibleStateImageBehavior = false;
            this.listViewNotes.View = System.Windows.Forms.View.Details;
            this.listViewNotes.SelectedIndexChanged += new System.EventHandler(this.listViewNotes_SelectedIndexChanged);
            // 
            // columnNo
            // 
            this.columnNo.Text = "Номер";
            // 
            // columnDate
            // 
            this.columnDate.Text = "Дата";
            this.columnDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnDate.Width = 100;
            // 
            // columnLicense
            // 
            this.columnLicense.Text = "Разрешение";
            this.columnLicense.Width = 150;
            // 
            // buttonAddLicense
            // 
            this.buttonAddLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddLicense.Location = new System.Drawing.Point(48, 493);
            this.buttonAddLicense.Name = "buttonAddLicense";
            this.buttonAddLicense.Size = new System.Drawing.Size(175, 23);
            this.buttonAddLicense.TabIndex = 14;
            this.buttonAddLicense.Text = "Добавить разрешение";
            this.buttonAddLicense.UseVisualStyleBackColor = true;
            this.buttonAddLicense.Click += new System.EventHandler(this.buttonAddLicense_Click);
            // 
            // spreadSheetCatches
            // 
            this.spreadSheetCatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetCatches.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.spreadSheetCatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpecies,
            this.ColumnQuantity,
            this.ColumnMass,
            this.ColumnFraction});
            this.spreadSheetCatches.Location = new System.Drawing.Point(229, 251);
            this.spreadSheetCatches.Name = "spreadSheetCatches";
            this.spreadSheetCatches.RowTemplate.Height = 45;
            this.spreadSheetCatches.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetCatches.Size = new System.Drawing.Size(475, 265);
            this.spreadSheetCatches.TabIndex = 13;
            // 
            // ColumnSpecies
            // 
            this.ColumnSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSpecies.HeaderText = "Вид";
            this.ColumnSpecies.Name = "ColumnSpecies";
            // 
            // ColumnQuantity
            // 
            this.ColumnQuantity.HeaderText = "Количество, экз.";
            this.ColumnQuantity.Name = "ColumnQuantity";
            // 
            // ColumnMass
            // 
            this.ColumnMass.HeaderText = "Масса, кг";
            this.ColumnMass.Name = "ColumnMass";
            // 
            // ColumnFraction
            // 
            this.ColumnFraction.HeaderText = "Освоение, %";
            this.ColumnFraction.Name = "ColumnFraction";
            // 
            // listViewLicenses
            // 
            this.listViewLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLicenses.FullRowSelect = true;
            this.listViewLicenses.Location = new System.Drawing.Point(48, 48);
            this.listViewLicenses.MultiSelect = false;
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.ShowGroups = false;
            this.listViewLicenses.Size = new System.Drawing.Size(175, 439);
            this.listViewLicenses.TabIndex = 10;
            this.listViewLicenses.TileSize = new System.Drawing.Size(150, 25);
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Tile;
            this.listViewLicenses.ItemActivate += new System.EventHandler(this.listViewLicenses_ItemActivate);
            this.listViewLicenses.SelectedIndexChanged += new System.EventHandler(this.listViewLicenses_SelectedIndexChanged);
            // 
            // Paper
            // 
            this.Paper.DataSetName = "LegalPapers";
            this.Paper.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // contextNote
            // 
            this.contextNote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextNotePrint});
            this.contextNote.Name = "contextNote";
            this.contextNote.Size = new System.Drawing.Size(125, 26);
            // 
            // contextNotePrint
            // 
            this.contextNotePrint.Name = "contextNotePrint";
            this.contextNotePrint.Size = new System.Drawing.Size(124, 22);
            this.contextNotePrint.Text = "Печатать";
            this.contextNotePrint.Click += new System.EventHandler(this.contextNotePrint_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Paper)).EndInit();
            this.contextNote.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public LegalPapers Paper;
        private System.Windows.Forms.ListView listViewLicenses;
        private Controls.SpreadSheet spreadSheetCatches;
        private System.Windows.Forms.Button buttonAddLicense;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFraction;
        private System.Windows.Forms.Button buttonAddNote;
        private System.Windows.Forms.ListView listViewNotes;
        private System.Windows.Forms.ColumnHeader columnNo;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnLicense;
        private System.Windows.Forms.ContextMenuStrip contextNote;
        private System.Windows.Forms.ToolStripMenuItem contextNotePrint;
    }
}