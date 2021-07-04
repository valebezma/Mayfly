namespace Mayfly.Fish.Explorer.Survey
{
    partial class WizardCommunityComposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardCommunityComposition));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pageCommunity = new AeroWizard.WizardPage();
            this.textBoxDiversity = new System.Windows.Forms.TextBox();
            this.labelDiversity = new System.Windows.Forms.Label();
            this.labelSpeciesCompositionInstruction = new System.Windows.Forms.Label();
            this.spreadSheetCommunity = new Mayfly.Controls.SpreadSheet();
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
            this.calculatorCommunity = new System.ComponentModel.BackgroundWorker();
            this.mathComposition = new Mayfly.Mathematics.MathAdapter(this.components);
            this.ColumnCatchesSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesAbundance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesAbundanceP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesBiomass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesBiomassP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatchesSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommAbundance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommAbundanceP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommBiomass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommBiomassP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommOccurrence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCommDominance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageCommunity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCommunity)).BeginInit();
            this.pageComposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.pageCatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            this.SuspendLayout();
            // 
            // pageCommunity
            // 
            this.pageCommunity.Controls.Add(this.textBoxDiversity);
            this.pageCommunity.Controls.Add(this.labelDiversity);
            this.pageCommunity.Controls.Add(this.labelSpeciesCompositionInstruction);
            this.pageCommunity.Controls.Add(this.spreadSheetCommunity);
            this.pageCommunity.Name = "pageCommunity";
            resources.ApplyResources(this.pageCommunity, "pageCommunity");
            this.pageCommunity.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCommunity_Commit);
            // 
            // textBoxDiversity
            // 
            resources.ApplyResources(this.textBoxDiversity, "textBoxDiversity");
            this.textBoxDiversity.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxDiversity.Name = "textBoxDiversity";
            this.textBoxDiversity.ReadOnly = true;
            // 
            // labelDiversity
            // 
            resources.ApplyResources(this.labelDiversity, "labelDiversity");
            this.labelDiversity.Name = "labelDiversity";
            // 
            // labelSpeciesCompositionInstruction
            // 
            resources.ApplyResources(this.labelSpeciesCompositionInstruction, "labelSpeciesCompositionInstruction");
            this.labelSpeciesCompositionInstruction.Name = "labelSpeciesCompositionInstruction";
            // 
            // spreadSheetCommunity
            // 
            resources.ApplyResources(this.spreadSheetCommunity, "spreadSheetCommunity");
            this.spreadSheetCommunity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCommSpecies,
            this.ColumnCommQ,
            this.ColumnCommAbundance,
            this.ColumnCommAbundanceP,
            this.ColumnCommBiomass,
            this.ColumnCommBiomassP,
            this.ColumnCommOccurrence,
            this.ColumnCommDominance});
            this.spreadSheetCommunity.DefaultDecimalPlaces = 2;
            this.spreadSheetCommunity.Name = "spreadSheetCommunity";
            this.spreadSheetCommunity.RowHeadersVisible = false;
            this.spreadSheetCommunity.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCommunity_CellValueChanged);
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnComposition.DefaultCellStyle = dataGridViewCellStyle9;
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
            this.wizardExplorer.Pages.Add(this.pageCommunity);
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
            // calculatorCommunity
            // 
            this.calculatorCommunity.WorkerReportsProgress = true;
            this.calculatorCommunity.WorkerSupportsCancellation = true;
            this.calculatorCommunity.DoWork += new System.ComponentModel.DoWorkEventHandler(this.communityCalculator_DoWork);
            this.calculatorCommunity.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.communityCalculator_ProgressChanged);
            this.calculatorCommunity.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.communityCalculator_RunWorkerCompleted);
            // 
            // mathComposition
            // 
            this.mathComposition.Sheet = this.spreadSheetCommunity;
            // 
            // ColumnCatchesSpecies
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnCatchesSpecies.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnCatchesSpecies.Frozen = true;
            resources.ApplyResources(this.ColumnCatchesSpecies, "ColumnCatchesSpecies");
            this.ColumnCatchesSpecies.Name = "ColumnCatchesSpecies";
            this.ColumnCatchesSpecies.ReadOnly = true;
            // 
            // ColumnCatchesAbundance
            // 
            dataGridViewCellStyle11.Format = "N0";
            this.ColumnCatchesAbundance.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.ColumnCatchesAbundance, "ColumnCatchesAbundance");
            this.ColumnCatchesAbundance.Name = "ColumnCatchesAbundance";
            this.ColumnCatchesAbundance.ReadOnly = true;
            // 
            // ColumnCatchesAbundanceP
            // 
            dataGridViewCellStyle12.Format = "P1";
            this.ColumnCatchesAbundanceP.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.ColumnCatchesAbundanceP, "ColumnCatchesAbundanceP");
            this.ColumnCatchesAbundanceP.Name = "ColumnCatchesAbundanceP";
            this.ColumnCatchesAbundanceP.ReadOnly = true;
            // 
            // ColumnCatchesBiomass
            // 
            dataGridViewCellStyle13.Format = "N3";
            this.ColumnCatchesBiomass.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.ColumnCatchesBiomass, "ColumnCatchesBiomass");
            this.ColumnCatchesBiomass.Name = "ColumnCatchesBiomass";
            this.ColumnCatchesBiomass.ReadOnly = true;
            // 
            // ColumnCatchesBiomassP
            // 
            dataGridViewCellStyle14.Format = "P1";
            this.ColumnCatchesBiomassP.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.ColumnCatchesBiomassP, "ColumnCatchesBiomassP");
            this.ColumnCatchesBiomassP.Name = "ColumnCatchesBiomassP";
            this.ColumnCatchesBiomassP.ReadOnly = true;
            // 
            // ColumnCatchesSex
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnCatchesSex.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.ColumnCatchesSex, "ColumnCatchesSex");
            this.ColumnCatchesSex.Name = "ColumnCatchesSex";
            this.ColumnCatchesSex.ReadOnly = true;
            // 
            // ColumnCommSpecies
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnCommSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnCommSpecies.Frozen = true;
            resources.ApplyResources(this.ColumnCommSpecies, "ColumnCommSpecies");
            this.ColumnCommSpecies.Name = "ColumnCommSpecies";
            this.ColumnCommSpecies.ReadOnly = true;
            // 
            // ColumnCommQ
            // 
            dataGridViewCellStyle2.Format = "N2";
            this.ColumnCommQ.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnCommQ, "ColumnCommQ");
            this.ColumnCommQ.Name = "ColumnCommQ";
            // 
            // ColumnCommAbundance
            // 
            dataGridViewCellStyle3.Format = "N0";
            this.ColumnCommAbundance.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnCommAbundance, "ColumnCommAbundance");
            this.ColumnCommAbundance.Name = "ColumnCommAbundance";
            this.ColumnCommAbundance.ReadOnly = true;
            // 
            // ColumnCommAbundanceP
            // 
            dataGridViewCellStyle4.Format = "P1";
            this.ColumnCommAbundanceP.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnCommAbundanceP, "ColumnCommAbundanceP");
            this.ColumnCommAbundanceP.Name = "ColumnCommAbundanceP";
            this.ColumnCommAbundanceP.ReadOnly = true;
            // 
            // ColumnCommBiomass
            // 
            dataGridViewCellStyle5.Format = "N3";
            this.ColumnCommBiomass.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnCommBiomass, "ColumnCommBiomass");
            this.ColumnCommBiomass.Name = "ColumnCommBiomass";
            this.ColumnCommBiomass.ReadOnly = true;
            // 
            // ColumnCommBiomassP
            // 
            dataGridViewCellStyle6.Format = "P1";
            this.ColumnCommBiomassP.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnCommBiomassP, "ColumnCommBiomassP");
            this.ColumnCommBiomassP.Name = "ColumnCommBiomassP";
            this.ColumnCommBiomassP.ReadOnly = true;
            // 
            // ColumnCommOccurrence
            // 
            dataGridViewCellStyle7.Format = "P1";
            this.ColumnCommOccurrence.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColumnCommOccurrence, "ColumnCommOccurrence");
            this.ColumnCommOccurrence.Name = "ColumnCommOccurrence";
            // 
            // ColumnCommDominance
            // 
            dataGridViewCellStyle8.Format = "N3";
            this.ColumnCommDominance.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.ColumnCommDominance, "ColumnCommDominance");
            this.ColumnCommDominance.Name = "ColumnCommDominance";
            // 
            // WizardCommunityComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardCommunityComposition";
            this.pageCommunity.ResumeLayout(false);
            this.pageCommunity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCommunity)).EndInit();
            this.pageComposition.ResumeLayout(false);
            this.pageComposition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetComposition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.pageCatches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPage pageCommunity;
        private System.Windows.Forms.Label labelSpeciesCompositionInstruction;
        public Mayfly.Controls.SpreadSheet spreadSheetCommunity;
        private AeroWizard.WizardPage pageComposition;
        private System.Windows.Forms.Label labelClassesInstruction;
        public Mayfly.Controls.SpreadSheet spreadSheetComposition;
        private AeroWizard.WizardControl wizardExplorer;
        private System.ComponentModel.BackgroundWorker calculatorCommunity;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComposition;
        private System.Windows.Forms.CheckBox checkBoxFractions;
        private System.Windows.Forms.CheckBox checkBoxPUE;
        private System.Windows.Forms.Label labelClasses;
        private System.Windows.Forms.ComboBox comboBoxParameter;
        private AeroWizard.WizardPage pageCatches;
        private System.Windows.Forms.Label label1;
        public Controls.SpreadSheet spreadSheetCatches;
        private Mathematics.MathAdapter mathComposition;
        private System.Windows.Forms.TextBox textBoxDiversity;
        private System.Windows.Forms.Label labelDiversity;
        private AeroWizard.WizardPage wizardPage1;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesAbundanceP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesBiomass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesBiomassP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatchesSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommAbundance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommAbundanceP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommBiomass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommBiomassP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommOccurrence;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCommDominance;
    }
}