namespace Mayfly.Fish.Explorer
{
    partial class WizardExportBio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardExportBio));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.pageSigns = new AeroWizard.WizardPage();
            this.spreadSheetSigns = new Mayfly.Controls.SpreadSheet();
            this.ColumnSignInvestigator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSignN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.pageGrowth = new AeroWizard.WizardPage();
            this.labelNoDataGrowth = new System.Windows.Forms.Label();
            this.spreadSheetGrowth = new Mayfly.Controls.SpreadSheet();
            this.ColumnGrowthSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGrowthN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGrowthR2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGrowthL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGrowthK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGrowthT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextGrowth = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGrowthChart = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.pageWeight = new AeroWizard.WizardPage();
            this.labelNoDataWeight = new System.Windows.Forms.Label();
            this.spreadSheetWeight = new Mayfly.Controls.SpreadSheet();
            this.ColumnWeightSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeightN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeightR2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeightQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWeightB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextWeight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextWeightChart = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxReport = new System.Windows.Forms.CheckBox();
            this.modelCalculator = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.backSpecExporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageSigns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSigns)).BeginInit();
            this.pageGrowth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetGrowth)).BeginInit();
            this.contextGrowth.SuspendLayout();
            this.pageWeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetWeight)).BeginInit();
            this.contextWeight.SuspendLayout();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageSigns);
            this.wizardExplorer.Pages.Add(this.pageGrowth);
            this.wizardExplorer.Pages.Add(this.pageWeight);
            this.wizardExplorer.Pages.Add(this.pageReport);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // pageStart
            // 
            this.pageStart.Controls.Add(this.label4);
            this.pageStart.Name = "pageStart";
            resources.ApplyResources(this.pageStart, "pageStart");
            this.pageStart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageStart_Commit);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pageSigns
            // 
            this.pageSigns.Controls.Add(this.spreadSheetSigns);
            this.pageSigns.Controls.Add(this.label1);
            this.pageSigns.Name = "pageSigns";
            resources.ApplyResources(this.pageSigns, "pageSigns");
            this.pageSigns.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSigns_Commit);
            // 
            // spreadSheetSigns
            // 
            resources.ApplyResources(this.spreadSheetSigns, "spreadSheetSigns");
            this.spreadSheetSigns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSignInvestigator,
            this.ColumnSignN});
            this.spreadSheetSigns.DefaultDecimalPlaces = 4;
            this.spreadSheetSigns.Name = "spreadSheetSigns";
            this.spreadSheetSigns.ReadOnly = true;
            this.spreadSheetSigns.RowHeadersVisible = false;
            this.spreadSheetSigns.RowTemplate.Height = 35;
            this.spreadSheetSigns.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetSigns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ColumnSignInvestigator
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSignInvestigator.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnSignInvestigator.FillWeight = 150F;
            resources.ApplyResources(this.ColumnSignInvestigator, "ColumnSignInvestigator");
            this.ColumnSignInvestigator.Name = "ColumnSignInvestigator";
            this.ColumnSignInvestigator.ReadOnly = true;
            // 
            // ColumnSignN
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.ColumnSignN.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnSignN, "ColumnSignN");
            this.ColumnSignN.Name = "ColumnSignN";
            this.ColumnSignN.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageGrowth
            // 
            this.pageGrowth.Controls.Add(this.labelNoDataGrowth);
            this.pageGrowth.Controls.Add(this.spreadSheetGrowth);
            this.pageGrowth.Controls.Add(this.label9);
            this.pageGrowth.Name = "pageGrowth";
            resources.ApplyResources(this.pageGrowth, "pageGrowth");
            // 
            // labelNoDataGrowth
            // 
            resources.ApplyResources(this.labelNoDataGrowth, "labelNoDataGrowth");
            this.labelNoDataGrowth.Name = "labelNoDataGrowth";
            // 
            // spreadSheetGrowth
            // 
            resources.ApplyResources(this.spreadSheetGrowth, "spreadSheetGrowth");
            this.spreadSheetGrowth.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnGrowthSpecies,
            this.ColumnGrowthN,
            this.ColumnGrowthR2,
            this.ColumnGrowthL,
            this.ColumnGrowthK,
            this.ColumnGrowthT});
            this.spreadSheetGrowth.DefaultDecimalPlaces = 4;
            this.spreadSheetGrowth.Name = "spreadSheetGrowth";
            this.spreadSheetGrowth.ReadOnly = true;
            this.spreadSheetGrowth.RowHeadersVisible = false;
            this.spreadSheetGrowth.RowMenu = this.contextGrowth;
            this.spreadSheetGrowth.RowTemplate.Height = 35;
            this.spreadSheetGrowth.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetGrowth.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ColumnGrowthSpecies
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnGrowthSpecies.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnGrowthSpecies.FillWeight = 150F;
            resources.ApplyResources(this.ColumnGrowthSpecies, "ColumnGrowthSpecies");
            this.ColumnGrowthSpecies.Name = "ColumnGrowthSpecies";
            this.ColumnGrowthSpecies.ReadOnly = true;
            // 
            // ColumnGrowthN
            // 
            dataGridViewCellStyle4.Format = "N0";
            this.ColumnGrowthN.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnGrowthN, "ColumnGrowthN");
            this.ColumnGrowthN.Name = "ColumnGrowthN";
            this.ColumnGrowthN.ReadOnly = true;
            // 
            // ColumnGrowthR2
            // 
            dataGridViewCellStyle5.Format = "P1";
            this.ColumnGrowthR2.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnGrowthR2, "ColumnGrowthR2");
            this.ColumnGrowthR2.Name = "ColumnGrowthR2";
            this.ColumnGrowthR2.ReadOnly = true;
            // 
            // ColumnGrowthL
            // 
            dataGridViewCellStyle6.Format = "N0";
            this.ColumnGrowthL.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnGrowthL, "ColumnGrowthL");
            this.ColumnGrowthL.Name = "ColumnGrowthL";
            this.ColumnGrowthL.ReadOnly = true;
            // 
            // ColumnGrowthK
            // 
            dataGridViewCellStyle7.Format = "N4";
            this.ColumnGrowthK.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnGrowthK, "ColumnGrowthK");
            this.ColumnGrowthK.Name = "ColumnGrowthK";
            this.ColumnGrowthK.ReadOnly = true;
            // 
            // ColumnGrowthT
            // 
            dataGridViewCellStyle8.Format = "N1";
            this.ColumnGrowthT.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.ColumnGrowthT, "ColumnGrowthT");
            this.ColumnGrowthT.Name = "ColumnGrowthT";
            this.ColumnGrowthT.ReadOnly = true;
            // 
            // contextGrowth
            // 
            this.contextGrowth.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGrowthChart});
            this.contextGrowth.Name = "contextGrowth";
            resources.ApplyResources(this.contextGrowth, "contextGrowth");
            // 
            // contextGrowthChart
            // 
            this.contextGrowthChart.Name = "contextGrowthChart";
            resources.ApplyResources(this.contextGrowthChart, "contextGrowthChart");
            this.contextGrowthChart.Click += new System.EventHandler(this.contextGrowthChart_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // pageWeight
            // 
            this.pageWeight.Controls.Add(this.labelNoDataWeight);
            this.pageWeight.Controls.Add(this.spreadSheetWeight);
            this.pageWeight.Controls.Add(this.label2);
            this.pageWeight.Name = "pageWeight";
            resources.ApplyResources(this.pageWeight, "pageWeight");
            this.pageWeight.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageWeight_Commit);
            // 
            // labelNoDataWeight
            // 
            resources.ApplyResources(this.labelNoDataWeight, "labelNoDataWeight");
            this.labelNoDataWeight.Name = "labelNoDataWeight";
            // 
            // spreadSheetWeight
            // 
            resources.ApplyResources(this.spreadSheetWeight, "spreadSheetWeight");
            this.spreadSheetWeight.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnWeightSpecies,
            this.ColumnWeightN,
            this.ColumnWeightR2,
            this.ColumnWeightQ,
            this.ColumnWeightB});
            this.spreadSheetWeight.DefaultDecimalPlaces = 4;
            this.spreadSheetWeight.Name = "spreadSheetWeight";
            this.spreadSheetWeight.ReadOnly = true;
            this.spreadSheetWeight.RowHeadersVisible = false;
            this.spreadSheetWeight.RowMenu = this.contextWeight;
            this.spreadSheetWeight.RowTemplate.Height = 35;
            this.spreadSheetWeight.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.spreadSheetWeight.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ColumnWeightSpecies
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnWeightSpecies.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColumnWeightSpecies.FillWeight = 150F;
            resources.ApplyResources(this.ColumnWeightSpecies, "ColumnWeightSpecies");
            this.ColumnWeightSpecies.Name = "ColumnWeightSpecies";
            this.ColumnWeightSpecies.ReadOnly = true;
            // 
            // ColumnWeightN
            // 
            dataGridViewCellStyle10.Format = "N0";
            this.ColumnWeightN.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.ColumnWeightN, "ColumnWeightN");
            this.ColumnWeightN.Name = "ColumnWeightN";
            this.ColumnWeightN.ReadOnly = true;
            // 
            // ColumnWeightR2
            // 
            dataGridViewCellStyle11.Format = "P1";
            this.ColumnWeightR2.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnWeightR2, "ColumnWeightR2");
            this.ColumnWeightR2.Name = "ColumnWeightR2";
            this.ColumnWeightR2.ReadOnly = true;
            // 
            // ColumnWeightQ
            // 
            dataGridViewCellStyle12.Format = "N4";
            this.ColumnWeightQ.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.ColumnWeightQ, "ColumnWeightQ");
            this.ColumnWeightQ.Name = "ColumnWeightQ";
            this.ColumnWeightQ.ReadOnly = true;
            // 
            // ColumnWeightB
            // 
            dataGridViewCellStyle13.Format = "N4";
            this.ColumnWeightB.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.ColumnWeightB, "ColumnWeightB");
            this.ColumnWeightB.Name = "ColumnWeightB";
            this.ColumnWeightB.ReadOnly = true;
            // 
            // contextWeight
            // 
            this.contextWeight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextWeightChart});
            this.contextWeight.Name = "contextWeight";
            resources.ApplyResources(this.contextWeight, "contextWeight");
            // 
            // contextWeightChart
            // 
            this.contextWeightChart.Name = "contextWeightChart";
            resources.ApplyResources(this.contextWeightChart, "contextWeightChart");
            this.contextWeightChart.Click += new System.EventHandler(this.contextWeightChart_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.label7);
            this.pageReport.Controls.Add(this.checkBoxReport);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // checkBoxReport
            // 
            resources.ApplyResources(this.checkBoxReport, "checkBoxReport");
            this.checkBoxReport.Checked = true;
            this.checkBoxReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReport.Name = "checkBoxReport";
            this.checkBoxReport.UseVisualStyleBackColor = true;
            this.checkBoxReport.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // modelCalculator
            // 
            this.modelCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.modelCalculator_DoWork);
            this.modelCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.modelCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // backSpecExporter
            // 
            this.backSpecExporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backSpecExporter_DoWork);
            this.backSpecExporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backSpecExporter_RunWorkerCompleted);
            // 
            // WizardExport2
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardExport2";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageSigns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetSigns)).EndInit();
            this.pageGrowth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetGrowth)).EndInit();
            this.contextGrowth.ResumeLayout(false);
            this.pageWeight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetWeight)).EndInit();
            this.contextWeight.ResumeLayout(false);
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private System.ComponentModel.BackgroundWorker modelCalculator;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageGrowth;
        private System.Windows.Forms.Label label9;
        private Controls.SpreadSheet spreadSheetGrowth;
        private System.Windows.Forms.CheckBox checkBoxReport;
        private System.Windows.Forms.Label labelNoDataGrowth;
        private System.ComponentModel.BackgroundWorker backSpecExporter;
        private AeroWizard.WizardPage pageWeight;
        private System.Windows.Forms.Label labelNoDataWeight;
        private Controls.SpreadSheet spreadSheetWeight;
        private System.Windows.Forms.Label label2;
        private AeroWizard.WizardPage pageSigns;
        private System.Windows.Forms.Label label1;
        private Controls.SpreadSheet spreadSheetSigns;
        private System.Windows.Forms.ContextMenuStrip contextGrowth;
        private System.Windows.Forms.ToolStripMenuItem contextGrowthChart;
        private System.Windows.Forms.ContextMenuStrip contextWeight;
        private System.Windows.Forms.ToolStripMenuItem contextWeightChart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSignInvestigator;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSignN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGrowthSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGrowthN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGrowthR2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGrowthL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGrowthK;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGrowthT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeightSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeightN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeightR2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeightQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWeightB;
    }
}