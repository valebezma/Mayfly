namespace Mayfly.Software.Locals
{
    partial class Locals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Locals));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBoxCulture = new System.Windows.Forms.ComboBox();
            this.labelCulture = new System.Windows.Forms.Label();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.labelProj = new System.Windows.Forms.Label();
            this.comboBoxFile = new System.Windows.Forms.ComboBox();
            this.labelFile = new System.Windows.Forms.Label();
            this.textBoxRoot = new System.Windows.Forms.TextBox();
            this.labelRoot = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.numericFrom = new System.Windows.Forms.NumericUpDown();
            this.labelLength = new System.Windows.Forms.Label();
            this.spreadSheetValues = new Mayfly.Controls.SpreadSheet();
            this.ColumnFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.numericEnd = new System.Windows.Forms.NumericUpDown();
            this.buttonLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxCulture
            // 
            this.comboBoxCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCulture.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxCulture, "comboBoxCulture");
            this.comboBoxCulture.Name = "comboBoxCulture";
            this.comboBoxCulture.SelectedIndexChanged += new System.EventHandler(this.comboBoxCulture_SelectedIndexChanged);
            this.comboBoxCulture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxProject_KeyPress);
            // 
            // labelCulture
            // 
            resources.ApplyResources(this.labelCulture, "labelCulture");
            this.labelCulture.Name = "labelCulture";
            // 
            // comboBoxProject
            // 
            resources.ApplyResources(this.comboBoxProject, "comboBoxProject");
            this.comboBoxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.SelectedIndexChanged += new System.EventHandler(this.comboBoxProject_SelectedIndexChanged);
            this.comboBoxProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxProject_KeyPress);
            // 
            // labelProj
            // 
            resources.ApplyResources(this.labelProj, "labelProj");
            this.labelProj.Name = "labelProj";
            // 
            // comboBoxFile
            // 
            resources.ApplyResources(this.comboBoxFile, "comboBoxFile");
            this.comboBoxFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFile.FormattingEnabled = true;
            this.comboBoxFile.Name = "comboBoxFile";
            this.comboBoxFile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxProject_KeyPress);
            // 
            // labelFile
            // 
            resources.ApplyResources(this.labelFile, "labelFile");
            this.labelFile.Name = "labelFile";
            // 
            // textBoxRoot
            // 
            resources.ApplyResources(this.textBoxRoot, "textBoxRoot");
            this.textBoxRoot.Name = "textBoxRoot";
            this.textBoxRoot.TextChanged += new System.EventHandler(this.textBoxRoot_TextChanged);
            // 
            // labelRoot
            // 
            resources.ApplyResources(this.labelRoot, "labelRoot");
            this.labelRoot.Name = "labelRoot";
            // 
            // buttonPrint
            // 
            resources.ApplyResources(this.buttonPrint, "buttonPrint");
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // numericFrom
            // 
            resources.ApplyResources(this.numericFrom, "numericFrom");
            this.numericFrom.Name = "numericFrom";
            this.numericFrom.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
            // 
            // spreadSheetValues
            // 
            resources.ApplyResources(this.spreadSheetValues, "spreadSheetValues");
            this.spreadSheetValues.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.spreadSheetValues.CellPadding = new System.Windows.Forms.Padding(15);
            this.spreadSheetValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFile,
            this.ColumnRes,
            this.ColumnText,
            this.ColumnLoc});
            this.spreadSheetValues.Name = "spreadSheetValues";
            // 
            // ColumnFile
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.ColumnFile.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnFile, "ColumnFile");
            this.ColumnFile.Name = "ColumnFile";
            this.ColumnFile.ReadOnly = true;
            // 
            // ColumnRes
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.ColumnRes.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnRes, "ColumnRes");
            this.ColumnRes.Name = "ColumnRes";
            this.ColumnRes.ReadOnly = true;
            // 
            // ColumnText
            // 
            this.ColumnText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnText.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnText, "ColumnText");
            this.ColumnText.Name = "ColumnText";
            // 
            // ColumnLoc
            // 
            this.ColumnLoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnLoc.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.ColumnLoc, "ColumnLoc");
            this.ColumnLoc.Name = "ColumnLoc";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericEnd
            // 
            resources.ApplyResources(this.numericEnd, "numericEnd");
            this.numericEnd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericEnd.Name = "numericEnd";
            this.numericEnd.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // buttonLoad
            // 
            resources.ApplyResources(this.buttonLoad, "buttonLoad");
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // ResourceTyper
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.numericEnd);
            this.Controls.Add(this.numericFrom);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.textBoxRoot);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelProj);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelCulture);
            this.Controls.Add(this.comboBoxFile);
            this.Controls.Add(this.comboBoxProject);
            this.Controls.Add(this.comboBoxCulture);
            this.Controls.Add(this.spreadSheetValues);
            this.Name = "ResourceTyper";
            ((System.ComponentModel.ISupportInitialize)(this.numericFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SpreadSheet spreadSheetValues;
        private System.Windows.Forms.ComboBox comboBoxCulture;
        private System.Windows.Forms.Label labelCulture;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.Label labelProj;
        private System.Windows.Forms.ComboBox comboBoxFile;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnText;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLoc;
        private System.Windows.Forms.TextBox textBoxRoot;
        private System.Windows.Forms.Label labelRoot;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.NumericUpDown numericFrom;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericEnd;
        private System.Windows.Forms.Button buttonLoad;
    }
}