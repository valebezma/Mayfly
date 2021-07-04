namespace Mayfly.Mathematics.Charts
{
    partial class ScatterplotSeparation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScatterplotSeparation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.spreadSheetClusters = new Mayfly.Controls.SpreadSheet();
            this.ColumnSample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSeedX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSeedY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numericUpDownK = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxMeasure = new System.Windows.Forms.ComboBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPerc = new System.Windows.Forms.TextBox();
            this.workerPointAssigner = new System.ComponentModel.BackgroundWorker();
            this.checkBoxRelative = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetClusters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonRestart
            // 
            resources.ApplyResources(this.buttonRestart, "buttonRestart");
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // spreadSheetClusters
            // 
            resources.ApplyResources(this.spreadSheetClusters, "spreadSheetClusters");
            this.spreadSheetClusters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSample,
            this.ColumnN,
            this.ColumnSeedX,
            this.ColumnSeedY});
            this.spreadSheetClusters.Name = "spreadSheetClusters";
            this.spreadSheetClusters.ReadOnly = true;
            this.spreadSheetClusters.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.spreadSheetClusters.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetClusters.RowTemplate.Height = 35;
            this.spreadSheetClusters.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ColumnSample
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSample.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnSample, "ColumnSample");
            this.ColumnSample.Name = "ColumnSample";
            this.ColumnSample.ReadOnly = true;
            // 
            // ColumnN
            // 
            dataGridViewCellStyle2.Format = "0";
            this.ColumnN.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnN, "ColumnN");
            this.ColumnN.Name = "ColumnN";
            this.ColumnN.ReadOnly = true;
            // 
            // ColumnSeedX
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnSeedX.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnSeedX, "ColumnSeedX");
            this.ColumnSeedX.Name = "ColumnSeedX";
            this.ColumnSeedX.ReadOnly = true;
            // 
            // ColumnSeedY
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnSeedY.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnSeedY, "ColumnSeedY");
            this.ColumnSeedY.Name = "ColumnSeedY";
            this.ColumnSeedY.ReadOnly = true;
            // 
            // numericUpDownK
            // 
            resources.ApplyResources(this.numericUpDownK, "numericUpDownK");
            this.numericUpDownK.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownK.Name = "numericUpDownK";
            this.numericUpDownK.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownK.ValueChanged += new System.EventHandler(this.numericUpDownK_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxStat
            // 
            resources.ApplyResources(this.textBoxStat, "textBoxStat");
            this.textBoxStat.Name = "textBoxStat";
            this.textBoxStat.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBoxMethod
            // 
            resources.ApplyResources(this.comboBoxMethod, "comboBoxMethod");
            this.comboBoxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethod.FormattingEnabled = true;
            this.comboBoxMethod.Items.AddRange(new object[] {
            resources.GetString("comboBoxMethod.Items"),
            resources.GetString("comboBoxMethod.Items1"),
            resources.GetString("comboBoxMethod.Items2")});
            this.comboBoxMethod.Name = "comboBoxMethod";
            this.comboBoxMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxMethod_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBoxMeasure
            // 
            resources.ApplyResources(this.comboBoxMeasure, "comboBoxMeasure");
            this.comboBoxMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMeasure.FormattingEnabled = true;
            this.comboBoxMeasure.Items.AddRange(new object[] {
            resources.GetString("comboBoxMeasure.Items"),
            resources.GetString("comboBoxMeasure.Items1"),
            resources.GetString("comboBoxMeasure.Items2"),
            resources.GetString("comboBoxMeasure.Items3"),
            resources.GetString("comboBoxMeasure.Items4")});
            this.comboBoxMeasure.Name = "comboBoxMeasure";
            this.comboBoxMeasure.SelectedIndexChanged += new System.EventHandler(this.comboBoxMeasure_SelectedIndexChanged);
            // 
            // buttonCopy
            // 
            resources.ApplyResources(this.buttonCopy, "buttonCopy");
            this.buttonCopy.FlatAppearance.BorderSize = 0;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxPerc
            // 
            resources.ApplyResources(this.textBoxPerc, "textBoxPerc");
            this.textBoxPerc.Name = "textBoxPerc";
            this.textBoxPerc.ReadOnly = true;
            // 
            // workerPointAssigner
            // 
            this.workerPointAssigner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerPointAssigner_DoWork);
            this.workerPointAssigner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerPointAssigner_RunWorkerCompleted);
            // 
            // checkBoxRelative
            // 
            resources.ApplyResources(this.checkBoxRelative, "checkBoxRelative");
            this.checkBoxRelative.Name = "checkBoxRelative";
            this.checkBoxRelative.UseVisualStyleBackColor = true;
            // 
            // ScatterplotSeparation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.checkBoxRelative);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.comboBoxMeasure);
            this.Controls.Add(this.comboBoxMethod);
            this.Controls.Add(this.textBoxPerc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxStat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.spreadSheetClusters);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScatterplotSeparation";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetClusters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonRestart;
        private Mayfly.Controls.SpreadSheet spreadSheetClusters;
        private System.Windows.Forms.NumericUpDown numericUpDownK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxMeasure;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSample;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSeedX;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSeedY;
        private System.ComponentModel.BackgroundWorker workerPointAssigner;
        private System.Windows.Forms.CheckBox checkBoxRelative;
    }
}