﻿namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardComposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardComposition));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.ColumnCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNPUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNPUEF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBPUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBPUEF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSexRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calculatorStructure = new System.ComponentModel.BackgroundWorker();
            this.pageComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).BeginInit();
            this.contextAgeReconstruction.SuspendLayout();
            this.contextComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageCatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            this.SuspendLayout();
            // 
            // pageComposition
            // 
            resources.ApplyResources(this.pageComposition, "pageComposition");
            this.pageComposition.Controls.Add(this.buttonEqChart);
            this.pageComposition.Controls.Add(this.checkBoxFractions);
            this.pageComposition.Controls.Add(this.checkBoxPUE);
            this.pageComposition.Controls.Add(this.labelClasses);
            this.pageComposition.Controls.Add(this.comboBoxParameter);
            this.pageComposition.Controls.Add(this.labelClassesInstruction);
            this.pageComposition.Controls.Add(this.spreadSheetComposition);
            this.pageComposition.Name = "pageComposition";
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
            resources.ApplyResources(this.contextAgeReconstruction, "contextAgeReconstruction");
            this.contextAgeReconstruction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextShowCalculation});
            this.contextAgeReconstruction.Name = "contextMenuStripAgeReconstruction";
            // 
            // contextShowCalculation
            // 
            resources.ApplyResources(this.contextShowCalculation, "contextShowCalculation");
            this.contextShowCalculation.Name = "contextShowCalculation";
            this.contextShowCalculation.Click += new System.EventHandler(this.contextShowCalculation_Click);
            // 
            // columnComposition
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnComposition.DefaultCellStyle = dataGridViewCellStyle12;
            this.columnComposition.Frozen = true;
            resources.ApplyResources(this.columnComposition, "columnComposition");
            this.columnComposition.Name = "columnComposition";
            this.columnComposition.ReadOnly = true;
            // 
            // contextComposition
            // 
            resources.ApplyResources(this.contextComposition, "contextComposition");
            this.contextComposition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCompositionSplit});
            this.contextComposition.Name = "contextMenuStripSplit";
            this.contextComposition.Opening += new System.ComponentModel.CancelEventHandler(this.contextComposition_Opening);
            // 
            // contextCompositionSplit
            // 
            resources.ApplyResources(this.contextCompositionSplit, "contextCompositionSplit");
            this.contextCompositionSplit.Name = "contextCompositionSplit";
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
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPage1_Initialize);
            // 
            // pageCatches
            // 
            resources.ApplyResources(this.pageCatches, "pageCatches");
            this.pageCatches.Controls.Add(this.label1);
            this.pageCatches.Controls.Add(this.spreadSheetCatches);
            this.pageCatches.Name = "pageCatches";
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
            this.ColumnCategory,
            this.ColumnL,
            this.ColumnW,
            this.ColumnN,
            this.ColumnNPUE,
            this.ColumnNPUEF,
            this.ColumnB,
            this.ColumnBPUE,
            this.ColumnBPUEF,
            this.ColumnSexRatio});
            this.spreadSheetCatches.DefaultDecimalPlaces = 2;
            this.spreadSheetCatches.Name = "spreadSheetCatches";
            this.spreadSheetCatches.ReadOnly = true;
            this.spreadSheetCatches.RowHeadersVisible = false;
            this.spreadSheetCatches.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetCatches.RowTemplate.Height = 35;
            // 
            // ColumnCategory
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnCategory.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColumnCategory.Frozen = true;
            resources.ApplyResources(this.ColumnCategory, "ColumnCategory");
            this.ColumnCategory.Name = "ColumnCategory";
            this.ColumnCategory.ReadOnly = true;
            // 
            // ColumnL
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Format = "g1";
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnL.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.ColumnL, "ColumnL");
            this.ColumnL.Name = "ColumnL";
            this.ColumnL.ReadOnly = true;
            // 
            // ColumnW
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Format = "g1";
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnW.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.ColumnW, "ColumnW");
            this.ColumnW.Name = "ColumnW";
            this.ColumnW.ReadOnly = true;
            // 
            // ColumnN
            // 
            dataGridViewCellStyle16.Format = "N0";
            this.ColumnN.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.ColumnN, "ColumnN");
            this.ColumnN.Name = "ColumnN";
            this.ColumnN.ReadOnly = true;
            // 
            // ColumnNPUE
            // 
            dataGridViewCellStyle17.Format = "N3";
            this.ColumnNPUE.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.ColumnNPUE, "ColumnNPUE");
            this.ColumnNPUE.Name = "ColumnNPUE";
            this.ColumnNPUE.ReadOnly = true;
            // 
            // ColumnNPUEF
            // 
            dataGridViewCellStyle18.Format = "P1";
            this.ColumnNPUEF.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.ColumnNPUEF, "ColumnNPUEF");
            this.ColumnNPUEF.Name = "ColumnNPUEF";
            this.ColumnNPUEF.ReadOnly = true;
            // 
            // ColumnB
            // 
            dataGridViewCellStyle19.Format = "N3";
            this.ColumnB.DefaultCellStyle = dataGridViewCellStyle19;
            resources.ApplyResources(this.ColumnB, "ColumnB");
            this.ColumnB.Name = "ColumnB";
            this.ColumnB.ReadOnly = true;
            // 
            // ColumnBPUE
            // 
            dataGridViewCellStyle20.Format = "N3";
            this.ColumnBPUE.DefaultCellStyle = dataGridViewCellStyle20;
            resources.ApplyResources(this.ColumnBPUE, "ColumnBPUE");
            this.ColumnBPUE.Name = "ColumnBPUE";
            this.ColumnBPUE.ReadOnly = true;
            // 
            // ColumnBPUEF
            // 
            dataGridViewCellStyle21.Format = "P1";
            this.ColumnBPUEF.DefaultCellStyle = dataGridViewCellStyle21;
            resources.ApplyResources(this.ColumnBPUEF, "ColumnBPUEF");
            this.ColumnBPUEF.Name = "ColumnBPUEF";
            this.ColumnBPUEF.ReadOnly = true;
            // 
            // ColumnSexRatio
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnSexRatio.DefaultCellStyle = dataGridViewCellStyle22;
            resources.ApplyResources(this.ColumnSexRatio, "ColumnSexRatio");
            this.ColumnSexRatio.Name = "ColumnSexRatio";
            this.ColumnSexRatio.ReadOnly = true;
            // 
            // calculatorStructure
            // 
            this.calculatorStructure.WorkerReportsProgress = true;
            this.calculatorStructure.WorkerSupportsCancellation = true;
            this.calculatorStructure.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calculatorStructure_DoWork);
            this.calculatorStructure.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.calculatorStructure_ProgressChanged);
            this.calculatorStructure.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calculatorStructure_RunWorkerCompleted);
            // 
            // WizardComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardComposition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WizardCatchesComposition_FormClosing);
            this.pageComposition.ResumeLayout(false);
            this.pageComposition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).EndInit();
            this.contextAgeReconstruction.ResumeLayout(false);
            this.contextComposition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
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
        private System.Windows.Forms.Button buttonEqChart;
        private AeroWizard.WizardPage wizardPage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComposition;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNPUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNPUEF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBPUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBPUEF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSexRatio;
    }
}