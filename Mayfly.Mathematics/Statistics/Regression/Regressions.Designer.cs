namespace Mayfly.Mathematics.Statistics
{
    partial class RegressionComparison
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
            this.listViewRegressions = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnR2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuRegression = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextRegressionProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnPlotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyParametersTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCommon = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelCoincidence = new System.Windows.Forms.Label();
            this.pictureBoxCoincidence = new System.Windows.Forms.PictureBox();
            this.textBoxCoincidenceP = new System.Windows.Forms.TextBox();
            this.buttonPairwise = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.backCalc = new System.ComponentModel.BackgroundWorker();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.contextMenuRegression.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoincidence)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewRegressions
            // 
            this.listViewRegressions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRegressions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewRegressions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnN,
            this.columnR2});
            this.listViewRegressions.ContextMenuStrip = this.contextMenuRegression;
            this.listViewRegressions.FullRowSelect = true;
            this.listViewRegressions.Location = new System.Drawing.Point(48, 73);
            this.listViewRegressions.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.listViewRegressions.Name = "listViewRegressions";
            this.listViewRegressions.ShowGroups = false;
            this.listViewRegressions.Size = new System.Drawing.Size(388, 316);
            this.listViewRegressions.TabIndex = 1;
            this.listViewRegressions.TileSize = new System.Drawing.Size(180, 25);
            this.listViewRegressions.UseCompatibleStateImageBehavior = false;
            this.listViewRegressions.View = System.Windows.Forms.View.Details;
            this.listViewRegressions.ItemActivate += new System.EventHandler(this.listViewRegressions_ItemActivate);
            this.listViewRegressions.SelectedIndexChanged += new System.EventHandler(this.listViewRegressions_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 250;
            // 
            // columnN
            // 
            this.columnN.Text = "Count";
            this.columnN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnR2
            // 
            this.columnR2.Text = "R2";
            this.columnR2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // contextMenuRegression
            // 
            this.contextMenuRegression.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextRegressionProperties,
            this.showOnPlotToolStripMenuItem,
            this.copyParametersTableToolStripMenuItem});
            this.contextMenuRegression.Name = "contextMenuRegression";
            this.contextMenuRegression.Size = new System.Drawing.Size(194, 70);
            // 
            // contextRegressionProperties
            // 
            this.contextRegressionProperties.Name = "contextRegressionProperties";
            this.contextRegressionProperties.Size = new System.Drawing.Size(193, 22);
            this.contextRegressionProperties.Text = "Show properties";
            this.contextRegressionProperties.Click += new System.EventHandler(this.listViewRegressions_ItemActivate);
            // 
            // showOnPlotToolStripMenuItem
            // 
            this.showOnPlotToolStripMenuItem.Name = "showOnPlotToolStripMenuItem";
            this.showOnPlotToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.showOnPlotToolStripMenuItem.Text = "Show on plot";
            this.showOnPlotToolStripMenuItem.Click += new System.EventHandler(this.showOnPlotToolStripMenuItem_Click);
            // 
            // copyParametersTableToolStripMenuItem
            // 
            this.copyParametersTableToolStripMenuItem.Name = "copyParametersTableToolStripMenuItem";
            this.copyParametersTableToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copyParametersTableToolStripMenuItem.Text = "Copy parameters table";
            this.copyParametersTableToolStripMenuItem.Click += new System.EventHandler(this.copyParametersTableToolStripMenuItem_Click);
            // 
            // buttonCommon
            // 
            this.buttonCommon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCommon.Enabled = false;
            this.buttonCommon.Location = new System.Drawing.Point(336, 490);
            this.buttonCommon.Name = "buttonCommon";
            this.buttonCommon.Size = new System.Drawing.Size(100, 23);
            this.buttonCommon.TabIndex = 5;
            this.buttonCommon.Text = "Merge";
            this.buttonCommon.UseVisualStyleBackColor = true;
            this.buttonCommon.Click += new System.EventHandler(this.buttonCommon_Click);
            // 
            // labelCoincidence
            // 
            this.labelCoincidence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCoincidence.AutoSize = true;
            this.labelCoincidence.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCoincidence.Location = new System.Drawing.Point(45, 410);
            this.labelCoincidence.Name = "labelCoincidence";
            this.labelCoincidence.Size = new System.Drawing.Size(66, 13);
            this.labelCoincidence.TabIndex = 2;
            this.labelCoincidence.Text = "Coincidence";
            // 
            // pictureBoxCoincidence
            // 
            this.pictureBoxCoincidence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCoincidence.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxCoincidence.Location = new System.Drawing.Point(342, 410);
            this.pictureBoxCoincidence.Name = "pictureBoxCoincidence";
            this.pictureBoxCoincidence.Size = new System.Drawing.Size(14, 14);
            this.pictureBoxCoincidence.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxCoincidence.TabIndex = 29;
            this.pictureBoxCoincidence.TabStop = false;
            this.pictureBoxCoincidence.DoubleClick += new System.EventHandler(this.pictureBoxCoincidence_DoubleClick);
            // 
            // textBoxCoincidenceP
            // 
            this.textBoxCoincidenceP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCoincidenceP.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCoincidenceP.Location = new System.Drawing.Point(362, 407);
            this.textBoxCoincidenceP.Name = "textBoxCoincidenceP";
            this.textBoxCoincidenceP.ReadOnly = true;
            this.textBoxCoincidenceP.Size = new System.Drawing.Size(74, 20);
            this.textBoxCoincidenceP.TabIndex = 3;
            this.textBoxCoincidenceP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonPairwise
            // 
            this.buttonPairwise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPairwise.Enabled = false;
            this.buttonPairwise.Location = new System.Drawing.Point(336, 461);
            this.buttonPairwise.Name = "buttonPairwise";
            this.buttonPairwise.Size = new System.Drawing.Size(100, 23);
            this.buttonPairwise.TabIndex = 4;
            this.buttonPairwise.Text = "Pairwise";
            this.buttonPairwise.UseVisualStyleBackColor = true;
            this.buttonPairwise.Click += new System.EventHandler(this.buttonPairwise_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReport.FlatAppearance.BorderSize = 0;
            this.buttonReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReport.Image = global::Mayfly.Pictogram.Report;
            this.buttonReport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonReport.Location = new System.Drawing.Point(48, 490);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(23, 23);
            this.buttonReport.TabIndex = 6;
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // backCalc
            // 
            this.backCalc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backCalc_DoWork);
            this.backCalc.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backCalc_RunWorkerCompleted);
            // 
            // labelInstruction
            // 
            this.labelInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelInstruction.Location = new System.Drawing.Point(48, 45);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(388, 13);
            this.labelInstruction.TabIndex = 0;
            this.labelInstruction.Text = "Select two or more regression models to test their coincidence";
            // 
            // RegressionComparison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.pictureBoxCoincidence);
            this.Controls.Add(this.textBoxCoincidenceP);
            this.Controls.Add(this.labelCoincidence);
            this.Controls.Add(this.buttonPairwise);
            this.Controls.Add(this.buttonCommon);
            this.Controls.Add(this.listViewRegressions);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 450);
            this.Name = "RegressionComparison";
            this.Padding = new System.Windows.Forms.Padding(45);
            this.ShowIcon = false;
            this.Text = "Separate regression comparison";
            this.contextMenuRegression.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoincidence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRegressions;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.Button buttonCommon;
        private System.Windows.Forms.ContextMenuStrip contextMenuRegression;
        private System.Windows.Forms.ToolStripMenuItem contextRegressionProperties;
        private System.Windows.Forms.ColumnHeader columnR2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelCoincidence;
        private System.Windows.Forms.PictureBox pictureBoxCoincidence;
        private System.Windows.Forms.TextBox textBoxCoincidenceP;
        private System.Windows.Forms.ColumnHeader columnN;
        private System.Windows.Forms.Button buttonPairwise;
        private System.Windows.Forms.ToolStripMenuItem showOnPlotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyParametersTableToolStripMenuItem;
        private System.Windows.Forms.Button buttonReport;
        private System.ComponentModel.BackgroundWorker backCalc;
        private System.Windows.Forms.Label labelInstruction;
    }
}