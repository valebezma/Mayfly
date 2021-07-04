namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardCatchesComposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardCatchesComposition));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pageComposition = new AeroWizard.WizardPage();
            this.buttonEqChart = new System.Windows.Forms.Button();
            this.checkBoxFractions = new System.Windows.Forms.CheckBox();
            this.checkBoxPUE = new System.Windows.Forms.CheckBox();
            this.labelClasses = new System.Windows.Forms.Label();
            this.comboBoxParameter = new System.Windows.Forms.ComboBox();
            this.labelClassesInstruction = new System.Windows.Forms.Label();
            this.spreadSheetComposition = new Mayfly.Controls.SpreadSheet();
            this.contextAgeReconstruction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextShowCalculation = new System.Windows.Forms.ToolStripMenuItem();
            this.columnComposition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextComposition = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCompositionSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.pageCatches = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.spreadSheetCatches = new Mayfly.Controls.SpreadSheet();
            this.ColumnCatchesCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesAbundance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesAbundanceP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesBiomass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesBiomassP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calculatorStructure = new System.ComponentModel.BackgroundWorker();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).BeginInit();
            this.contextAgeReconstruction.SuspendLayout();
            this.contextComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.pageCatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            this.SuspendLayout();
            // 
            // pageComposition
            // 
            this.pageComposition.Controls.Add(this.buttonEqChart);
            this.pageComposition.Controls.Add(this.checkBoxFractions);
            this.pageComposition.Controls.Add(this.checkBoxPUE);
            this.pageComposition.Controls.Add(this.labelClasses);
            this.pageComposition.Controls.Add(this.comboBoxParameter);
            this.pageComposition.Controls.Add(this.labelClassesInstruction);
            this.pageComposition.Controls.Add(this.spreadSheetComposition);
            this.pageComposition.Name = "pageComposition";
            resources.ApplyResources(this.pageComposition, "pageComposition");
            this.pageComposition.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageComposition_Rollback);
            // 
            // buttonEqChart
            // 
            resources.ApplyResources(this.buttonEqChart, "buttonEqChart");
            this.buttonEqChart.Name = "buttonEqChart";
            this.buttonEqChart.UseVisualStyleBackColor = true;
            this.buttonEqChart.Click += new System.EventHandler(this.buttonEqChart_Click);
            // 
            // checkBoxFractions
            // 
            resources.ApplyResources(this.checkBoxFractions, "checkBoxFractions");
            this.checkBoxFractions.Name = "checkBoxFractions";
            this.checkBoxFractions.UseVisualStyleBackColor = true;
            this.checkBoxFractions.CheckedChanged += new System.EventHandler(this.checkBoxFractions_CheckedChanged);
            // 
            // checkBoxPUE
            // 
            resources.ApplyResources(this.checkBoxPUE, "checkBoxPUE");
            this.checkBoxPUE.Name = "checkBoxPUE";
            this.checkBoxPUE.UseVisualStyleBackColor = true;
            this.checkBoxPUE.CheckedChanged += new System.EventHandler(this.displayParameter_Changed);
            // 
            // labelClasses
            // 
            resources.ApplyResources(this.labelClasses, "labelClasses");
            this.labelClasses.Name = "labelClasses";
            // 
            // comboBoxParameter
            // 
            resources.ApplyResources(this.comboBoxParameter, "comboBoxParameter");
            this.comboBoxParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParameter.FormattingEnabled = true;
            this.comboBoxParameter.Items.AddRange(new object[] {
            resources.GetString("comboBoxParameter.Items"),
            resources.GetString("comboBoxParameter.Items1")});
            this.comboBoxParameter.Name = "comboBoxParameter";
            this.comboBoxParameter.SelectedIndexChanged += new System.EventHandler(this.displayParameter_Changed);
            // 
            // labelClassesInstruction
            // 
            resources.ApplyResources(this.labelClassesInstruction, "labelClassesInstruction");
            this.labelClassesInstruction.Name = "labelClassesInstruction";
            // 
            // spreadSheetComposition
            // 
            resources.ApplyResources(this.spreadSheetComposition, "spreadSheetComposition");
            this.spreadSheetComposition.ColumnMenu = this.contextAgeReconstruction;
            this.spreadSheetComposition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnComposition});
            this.spreadSheetComposition.DefaultDecimalPlaces = 0;
            this.spreadSheetComposition.Name = "spreadSheetComposition";
            this.spreadSheetComposition.RowMenu = this.contextComposition;
            this.spreadSheetComposition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.spreadSheetComposition.MouseClick += new System.Windows.Forms.MouseEventHandler(this.spreadSheetComposition_MouseClick);
            // 
            // contextAgeReconstruction
            // 
            this.contextAgeReconstruction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextShowCalculation});
            this.contextAgeReconstruction.Name = "contextMenuStripAgeReconstruction";
            resources.ApplyResources(this.contextAgeReconstruction, "contextAgeReconstruction");
            // 
            // contextShowCalculation
            // 
            this.contextShowCalculation.Name = "contextShowCalculation";
            resources.ApplyResources(this.contextShowCalculation, "contextShowCalculation");
            this.contextShowCalculation.Click += new System.EventHandler(this.contextShowCalculation_Click);
            // 
            // columnComposition
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.columnComposition.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnComposition.Frozen = true;
            resources.ApplyResources(this.columnComposition, "columnComposition");
            this.columnComposition.Name = "columnComposition";
            this.columnComposition.ReadOnly = true;
            // 
            // contextComposition
            // 
            this.contextComposition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCompositionSplit});
            this.contextComposition.Name = "contextMenuStripSplit";
            resources.ApplyResources(this.contextComposition, "contextComposition");
            this.contextComposition.Opening += new System.ComponentModel.CancelEventHandler(this.contextComposition_Opening);
            // 
            // contextCompositionSplit
            // 
            this.contextCompositionSplit.Name = "contextCompositionSplit";
            resources.ApplyResources(this.contextCompositionSplit, "contextCompositionSplit");
            this.contextCompositionSplit.Click += new System.EventHandler(this.menuCompositionSplit_Click);
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.wizardPage1);
            this.wizardExplorer.Pages.Add(this.pageComposition);
            this.wizardExplorer.Pages.Add(this.pageCatches);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.labelStart);
            this.wizardPage1.Name = "wizardPage1";
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPage1_Initialize);
            // 
            // pageCatches
            // 
            this.pageCatches.Controls.Add(this.label1);
            this.pageCatches.Controls.Add(this.spreadSheetCatches);
            this.pageCatches.Name = "pageCatches";
            resources.ApplyResources(this.pageCatches, "pageCatches");
            this.pageCatches.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCatches_Commit);
            this.pageCatches.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCatches_Rollback);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // spreadSheetCatches
            // 
            resources.ApplyResources(this.spreadSheetCatches, "spreadSheetCatches");
            this.spreadSheetCatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCatchesCategory,
            this.ColumnCatchesAbundance,
            this.ColumnCatchesAbundanceP,
            this.ColumnCatchesBiomass,
            this.ColumnCatchesBiomassP,
            this.ColumnCatchesSex});
            this.spreadSheetCatches.DefaultDecimalPlaces = 2;
            this.spreadSheetCatches.Name = "spreadSheetCatches";
            this.spreadSheetCatches.ReadOnly = true;
            this.spreadSheetCatches.RowHeadersVisible = false;
            // 
            // ColumnCatchesCategory
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCatchesCategory.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnCatchesCategory.Frozen = true;
            resources.ApplyResources(this.ColumnCatchesCategory, "ColumnCatchesCategory");
            this.ColumnCatchesCategory.Name = "ColumnCatchesCategory";
            this.ColumnCatchesCategory.ReadOnly = true;
            // 
            // ColumnCatchesAbundance
            // 
            dataGridViewCellStyle3.Format = "N3";
            this.ColumnCatchesAbundance.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnCatchesAbundance, "ColumnCatchesAbundance");
            this.ColumnCatchesAbundance.Name = "ColumnCatchesAbundance";
            this.ColumnCatchesAbundance.ReadOnly = true;
            // 
            // ColumnCatchesAbundanceP
            // 
            dataGridViewCellStyle4.Format = "P1";
            this.ColumnCatchesAbundanceP.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnCatchesAbundanceP, "ColumnCatchesAbundanceP");
            this.ColumnCatchesAbundanceP.Name = "ColumnCatchesAbundanceP";
            this.ColumnCatchesAbundanceP.ReadOnly = true;
            // 
            // ColumnCatchesBiomass
            // 
            dataGridViewCellStyle5.Format = "N3";
            this.ColumnCatchesBiomass.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnCatchesBiomass, "ColumnCatchesBiomass");
            this.ColumnCatchesBiomass.Name = "ColumnCatchesBiomass";
            this.ColumnCatchesBiomass.ReadOnly = true;
            // 
            // ColumnCatchesBiomassP
            // 
            dataGridViewCellStyle6.Format = "P1";
            this.ColumnCatchesBiomassP.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnCatchesBiomassP, "ColumnCatchesBiomassP");
            this.ColumnCatchesBiomassP.Name = "ColumnCatchesBiomassP";
            this.ColumnCatchesBiomassP.ReadOnly = true;
            // 
            // ColumnCatchesSex
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCatchesSex.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnCatchesSex, "ColumnCatchesSex");
            this.ColumnCatchesSex.Name = "ColumnCatchesSex";
            this.ColumnCatchesSex.ReadOnly = true;
            // 
            // calculatorStructure
            // 
            this.calculatorStructure.WorkerReportsProgress = true;
            this.calculatorStructure.WorkerSupportsCancellation = true;
            this.calculatorStructure.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calculatorStructure_DoWork);
            this.calculatorStructure.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.calculatorStructure_ProgressChanged);
            this.calculatorStructure.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calculatorStructure_RunWorkerCompleted);
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // WizardCatchesComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardCatchesComposition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WizardCatchesComposition_FormClosing);
            this.pageComposition.ResumeLayout(false);
            this.pageComposition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).EndInit();
            this.contextAgeReconstruction.ResumeLayout(false);
            this.contextComposition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.pageCatches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPage pageComposition;
        private System.Windows.Forms.Label labelClasses;
        private System.Windows.Forms.ComboBox comboBoxParameter;
        private System.Windows.Forms.Label labelClassesInstruction;
        public Mayfly.Controls.SpreadSheet spreadSheetComposition;
        private System.Windows.Forms.ContextMenuStrip contextAgeReconstruction;
        private System.Windows.Forms.ToolStripMenuItem contextShowCalculation;
        private System.ComponentModel.BackgroundWorker calculatorStructure;
        private System.Windows.Forms.ContextMenuStrip contextComposition;
        private System.Windows.Forms.ToolStripMenuItem contextCompositionSplit;
        internal AeroWizard.WizardControl wizardExplorer;
        private System.Windows.Forms.CheckBox checkBoxFractions;
        private System.Windows.Forms.CheckBox checkBoxPUE;
        private AeroWizard.WizardPage pageCatches;
        private System.Windows.Forms.Label label1;
        public Controls.SpreadSheet spreadSheetCatches;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComposition;
        private System.Windows.Forms.Button buttonEqChart;
        private AeroWizard.WizardPage wizardPage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesAbundanceP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesBiomass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesBiomassP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesSex;
        private System.Windows.Forms.Label labelStart;
    }
}