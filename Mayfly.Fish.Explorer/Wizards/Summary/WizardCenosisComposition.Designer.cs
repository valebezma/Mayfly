namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardCenosisComposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardCenosisComposition));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pageCenosis = new AeroWizard.WizardPage();
            this.pageComposition = new AeroWizard.WizardPage();
            this.checkBoxFractions = new System.Windows.Forms.CheckBox();
            this.checkBoxPUE = new System.Windows.Forms.CheckBox();
            this.labelClasses = new System.Windows.Forms.Label();
            this.comboBoxParameter = new System.Windows.Forms.ComboBox();
            this.labelClassesInstruction = new System.Windows.Forms.Label();
            this.spreadSheetComposition = new Mayfly.Controls.SpreadSheet();
            this.columnComposition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageCatches = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.spreadSheetCatches = new Mayfly.Controls.SpreadSheet();
            this.ColumnCatchesSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesNPUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesNPUEp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesBPUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesBPUEp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calculatorCenosis = new System.ComponentModel.BackgroundWorker();
            this.mathComposition = new Mayfly.Mathematics.MathAdapter(this.components);
            this.spreadSheet1 = new Mayfly.Controls.SpreadSheet();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.pageCatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pageCenosis
            // 
            this.pageCenosis.Name = "pageCenosis";
            resources.ApplyResources(this.pageCenosis, "pageCenosis");
            this.pageCenosis.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCenosis_Commit);
            // 
            // pageComposition
            // 
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
            this.spreadSheetComposition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnComposition});
            this.spreadSheetComposition.DefaultDecimalPlaces = 0;
            this.spreadSheetComposition.Name = "spreadSheetComposition";
            this.spreadSheetComposition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // columnComposition
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnComposition.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnComposition.FillWeight = 150F;
            this.columnComposition.Frozen = true;
            resources.ApplyResources(this.columnComposition, "columnComposition");
            this.columnComposition.Name = "columnComposition";
            this.columnComposition.ReadOnly = true;
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.wizardPage1);
            this.wizardExplorer.Pages.Add(this.pageComposition);
            this.wizardExplorer.Pages.Add(this.pageCatches);
            this.wizardExplorer.Pages.Add(this.pageCenosis);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.labelStart);
            this.wizardPage1.Name = "wizardPage1";
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPage1_Initialize);
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // pageCatches
            // 
            this.pageCatches.Controls.Add(this.label1);
            this.pageCatches.Controls.Add(this.spreadSheetCatches);
            this.pageCatches.Name = "pageCatches";
            resources.ApplyResources(this.pageCatches, "pageCatches");
            this.pageCatches.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCatches_Commit);
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
            this.ColumnCatchesSpecies,
            this.ColumnCatchesL,
            this.ColumnCatchesW,
            this.ColumnCatchesN,
            this.ColumnCatchesNPUE,
            this.ColumnCatchesNPUEp,
            this.ColumnCatchesB,
            this.ColumnCatchesBPUE,
            this.ColumnCatchesBPUEp,
            this.ColumnCatchesSex});
            this.spreadSheetCatches.Name = "spreadSheetCatches";
            this.spreadSheetCatches.ReadOnly = true;
            this.spreadSheetCatches.RowHeadersVisible = false;
            this.spreadSheetCatches.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetCatches.RowTemplate.Height = 35;
            // 
            // ColumnCatchesSpecies
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnCatchesSpecies.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnCatchesSpecies.Frozen = true;
            resources.ApplyResources(this.ColumnCatchesSpecies, "ColumnCatchesSpecies");
            this.ColumnCatchesSpecies.Name = "ColumnCatchesSpecies";
            this.ColumnCatchesSpecies.ReadOnly = true;
            // 
            // ColumnCatchesL
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnCatchesL.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnCatchesL, "ColumnCatchesL");
            this.ColumnCatchesL.Name = "ColumnCatchesL";
            this.ColumnCatchesL.ReadOnly = true;
            // 
            // ColumnCatchesW
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "g";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnCatchesW.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnCatchesW, "ColumnCatchesW");
            this.ColumnCatchesW.Name = "ColumnCatchesW";
            this.ColumnCatchesW.ReadOnly = true;
            // 
            // ColumnCatchesN
            // 
            dataGridViewCellStyle5.Format = "N0";
            this.ColumnCatchesN.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnCatchesN, "ColumnCatchesN");
            this.ColumnCatchesN.Name = "ColumnCatchesN";
            this.ColumnCatchesN.ReadOnly = true;
            // 
            // ColumnCatchesNPUE
            // 
            dataGridViewCellStyle6.Format = "N1";
            this.ColumnCatchesNPUE.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnCatchesNPUE, "ColumnCatchesNPUE");
            this.ColumnCatchesNPUE.Name = "ColumnCatchesNPUE";
            this.ColumnCatchesNPUE.ReadOnly = true;
            // 
            // ColumnCatchesNPUEp
            // 
            dataGridViewCellStyle7.Format = "P1";
            this.ColumnCatchesNPUEp.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnCatchesNPUEp, "ColumnCatchesNPUEp");
            this.ColumnCatchesNPUEp.Name = "ColumnCatchesNPUEp";
            this.ColumnCatchesNPUEp.ReadOnly = true;
            // 
            // ColumnCatchesB
            // 
            dataGridViewCellStyle8.Format = "N2";
            this.ColumnCatchesB.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.ColumnCatchesB, "ColumnCatchesB");
            this.ColumnCatchesB.Name = "ColumnCatchesB";
            this.ColumnCatchesB.ReadOnly = true;
            // 
            // ColumnCatchesBPUE
            // 
            dataGridViewCellStyle9.Format = "N2";
            this.ColumnCatchesBPUE.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.ColumnCatchesBPUE, "ColumnCatchesBPUE");
            this.ColumnCatchesBPUE.Name = "ColumnCatchesBPUE";
            this.ColumnCatchesBPUE.ReadOnly = true;
            // 
            // ColumnCatchesBPUEp
            // 
            dataGridViewCellStyle10.Format = "P1";
            this.ColumnCatchesBPUEp.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.ColumnCatchesBPUEp, "ColumnCatchesBPUEp");
            this.ColumnCatchesBPUEp.Name = "ColumnCatchesBPUEp";
            this.ColumnCatchesBPUEp.ReadOnly = true;
            // 
            // ColumnCatchesSex
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCatchesSex.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnCatchesSex, "ColumnCatchesSex");
            this.ColumnCatchesSex.Name = "ColumnCatchesSex";
            this.ColumnCatchesSex.ReadOnly = true;
            // 
            // calculatorCenosis
            // 
            this.calculatorCenosis.WorkerReportsProgress = true;
            this.calculatorCenosis.WorkerSupportsCancellation = true;
            this.calculatorCenosis.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cenosisCalculator_DoWork);
            this.calculatorCenosis.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.cenosisCalculator_ProgressChanged);
            this.calculatorCenosis.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.cenosisCalculator_RunWorkerCompleted);
            // 
            // mathComposition
            // 
            // 
            // spreadSheet1
            // 
            resources.ApplyResources(this.spreadSheet1, "spreadSheet1");
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle13.Format = "N2";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle14.Format = "N0";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle15.Format = "P1";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle16.Format = "N3";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle17.Format = "P1";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle18.Format = "P1";
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle18;
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle19.Format = "N3";
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle19;
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.spreadSheet1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.spreadSheet1.DefaultDecimalPlaces = 2;
            this.spreadSheet1.Name = "spreadSheet1";
            this.spreadSheet1.RowHeadersVisible = false;
            this.mathComposition.Sheet = this.spreadSheet1;
            // 
            // WizardCenosisComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardCenosisComposition";
            this.pageComposition.ResumeLayout(false);
            this.pageComposition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.pageCatches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPage pageCenosis;
        private AeroWizard.WizardPage pageComposition;
        private System.Windows.Forms.Label labelClassesInstruction;
        public Mayfly.Controls.SpreadSheet spreadSheetComposition;
        private AeroWizard.WizardControl wizardExplorer;
        private System.ComponentModel.BackgroundWorker calculatorCenosis;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComposition;
        private System.Windows.Forms.CheckBox checkBoxFractions;
        private System.Windows.Forms.CheckBox checkBoxPUE;
        private System.Windows.Forms.Label labelClasses;
        private System.Windows.Forms.ComboBox comboBoxParameter;
        private AeroWizard.WizardPage pageCatches;
        private System.Windows.Forms.Label label1;
        public Controls.SpreadSheet spreadSheetCatches;
        private Mathematics.MathAdapter mathComposition;
        private AeroWizard.WizardPage wizardPage1;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesNPUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesNPUEp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesBPUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesBPUEp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesSex;
        private Controls.SpreadSheet spreadSheet1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}