namespace Mayfly.Fish.Explorer
{
    partial class WizardGearSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardGearSet));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextEffort = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextEffortMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.pageEfforts = new AeroWizard.WizardPage();
            this.checkBoxSpatial = new System.Windows.Forms.CheckBox();
            this.textBoxEffort = new System.Windows.Forms.TextBox();
            this.labelNoticeGears = new System.Windows.Forms.Label();
            this.labelEffortTotal = new System.Windows.Forms.Label();
            this.labelEffortInstruction = new System.Windows.Forms.Label();
            this.spreadSheetEfforts = new Mayfly.Controls.SpreadSheet();
            this.ColumnClass = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.ColumnOperations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEffort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEffortP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSpatialWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageSampler = new AeroWizard.WizardPage();
            this.labelGearInstruction = new System.Windows.Forms.Label();
            this.labelEUInstruction = new System.Windows.Forms.Label();
            this.labelGear = new System.Windows.Forms.Label();
            this.labelEU = new System.Windows.Forms.Label();
            this.comboBoxGearType = new System.Windows.Forms.ComboBox();
            this.comboBoxUE = new System.Windows.Forms.ComboBox();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.calculatorEffort = new System.ComponentModel.BackgroundWorker();
            this.calculatorSelection = new System.ComponentModel.BackgroundWorker();
            this.contextEffort.SuspendLayout();
            this.pageEfforts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetEfforts)).BeginInit();
            this.pageSampler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextEffort
            // 
            this.contextEffort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextEffortMerge});
            this.contextEffort.Name = "contextMenuStripSubData";
            resources.ApplyResources(this.contextEffort, "contextEffort");
            this.contextEffort.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripSubData_Opening);
            // 
            // contextEffortMerge
            // 
            this.contextEffortMerge.Name = "contextEffortMerge";
            resources.ApplyResources(this.contextEffortMerge, "contextEffortMerge");
            this.contextEffortMerge.Click += new System.EventHandler(this.ToolStripMenuItemMergeSelected_Click);
            // 
            // pageEfforts
            // 
            this.pageEfforts.AllowNext = false;
            this.pageEfforts.Controls.Add(this.checkBoxSpatial);
            this.pageEfforts.Controls.Add(this.textBoxEffort);
            this.pageEfforts.Controls.Add(this.labelNoticeGears);
            this.pageEfforts.Controls.Add(this.labelEffortTotal);
            this.pageEfforts.Controls.Add(this.labelEffortInstruction);
            this.pageEfforts.Controls.Add(this.spreadSheetEfforts);
            this.pageEfforts.Name = "pageEfforts";
            resources.ApplyResources(this.pageEfforts, "pageEfforts");
            this.pageEfforts.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageEfforts_Commit);
            // 
            // checkBoxSpatial
            // 
            resources.ApplyResources(this.checkBoxSpatial, "checkBoxSpatial");
            this.checkBoxSpatial.Name = "checkBoxSpatial";
            this.checkBoxSpatial.UseVisualStyleBackColor = true;
            this.checkBoxSpatial.CheckedChanged += new System.EventHandler(this.checkBoxSpatial_CheckedChanged);
            this.checkBoxSpatial.EnabledChanged += new System.EventHandler(this.checkBoxSpatial_EnabledChanged);
            // 
            // textBoxEffort
            // 
            resources.ApplyResources(this.textBoxEffort, "textBoxEffort");
            this.textBoxEffort.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxEffort.Name = "textBoxEffort";
            this.textBoxEffort.ReadOnly = true;
            // 
            // labelNoticeGears
            // 
            resources.ApplyResources(this.labelNoticeGears, "labelNoticeGears");
            this.labelNoticeGears.Name = "labelNoticeGears";
            // 
            // labelEffortTotal
            // 
            resources.ApplyResources(this.labelEffortTotal, "labelEffortTotal");
            this.labelEffortTotal.Name = "labelEffortTotal";
            // 
            // labelEffortInstruction
            // 
            resources.ApplyResources(this.labelEffortInstruction, "labelEffortInstruction");
            this.labelEffortInstruction.Name = "labelEffortInstruction";
            // 
            // spreadSheetEfforts
            // 
            this.spreadSheetEfforts.AllowUserToHideRows = true;
            resources.ApplyResources(this.spreadSheetEfforts, "spreadSheetEfforts");
            this.spreadSheetEfforts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.spreadSheetEfforts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnClass,
            this.ColumnOperations,
            this.ColumnEffort,
            this.ColumnEffortP,
            this.ColumnSpatialWeight});
            this.spreadSheetEfforts.DefaultDecimalPlaces = 3;
            this.spreadSheetEfforts.Name = "spreadSheetEfforts";
            this.spreadSheetEfforts.RowMenu = this.contextEffort;
            this.spreadSheetEfforts.RowMenuLaunchableItemIndex = 0;
            this.spreadSheetEfforts.UserChangedRowVisibility += new System.Windows.Forms.DataGridViewRowEventHandler(this.spreadSheetEfforts_UserChangedRowVisibility);
            this.spreadSheetEfforts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetEfforts_CellEndEdit);
            // 
            // ColumnClass
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.ColumnClass.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.ColumnClass, "ColumnClass");
            this.ColumnClass.Image = null;
            this.ColumnClass.Name = "ColumnClass";
            this.ColumnClass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColumnOperations
            // 
            dataGridViewCellStyle2.Format = "N0";
            this.ColumnOperations.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ColumnOperations, "ColumnOperations");
            this.ColumnOperations.Name = "ColumnOperations";
            this.ColumnOperations.ReadOnly = true;
            // 
            // ColumnEffort
            // 
            dataGridViewCellStyle3.Format = "N3";
            this.ColumnEffort.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.ColumnEffort, "ColumnEffort");
            this.ColumnEffort.Name = "ColumnEffort";
            this.ColumnEffort.ReadOnly = true;
            // 
            // ColumnEffortP
            // 
            dataGridViewCellStyle4.Format = "P1";
            this.ColumnEffortP.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnEffortP, "ColumnEffortP");
            this.ColumnEffortP.Name = "ColumnEffortP";
            this.ColumnEffortP.ReadOnly = true;
            // 
            // ColumnSpatialWeight
            // 
            dataGridViewCellStyle5.Format = "P0";
            this.ColumnSpatialWeight.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnSpatialWeight, "ColumnSpatialWeight");
            this.ColumnSpatialWeight.Name = "ColumnSpatialWeight";
            // 
            // pageSampler
            // 
            this.pageSampler.Controls.Add(this.labelGearInstruction);
            this.pageSampler.Controls.Add(this.labelEUInstruction);
            this.pageSampler.Controls.Add(this.labelGear);
            this.pageSampler.Controls.Add(this.labelEU);
            this.pageSampler.Controls.Add(this.comboBoxGearType);
            this.pageSampler.Controls.Add(this.comboBoxUE);
            this.pageSampler.Name = "pageSampler";
            resources.ApplyResources(this.pageSampler, "pageSampler");
            this.pageSampler.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSampler_Commit);
            this.pageSampler.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSampler_Rollback);
            // 
            // labelGearInstruction
            // 
            resources.ApplyResources(this.labelGearInstruction, "labelGearInstruction");
            this.labelGearInstruction.Name = "labelGearInstruction";
            // 
            // labelEUInstruction
            // 
            resources.ApplyResources(this.labelEUInstruction, "labelEUInstruction");
            this.labelEUInstruction.Name = "labelEUInstruction";
            // 
            // labelGear
            // 
            resources.ApplyResources(this.labelGear, "labelGear");
            this.labelGear.Name = "labelGear";
            // 
            // labelEU
            // 
            resources.ApplyResources(this.labelEU, "labelEU");
            this.labelEU.Name = "labelEU";
            // 
            // comboBoxGearType
            // 
            resources.ApplyResources(this.comboBoxGearType, "comboBoxGearType");
            this.comboBoxGearType.DisplayMember = "Type";
            this.comboBoxGearType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGearType.FormattingEnabled = true;
            this.comboBoxGearType.Name = "comboBoxGearType";
            this.comboBoxGearType.ValueMember = "Type";
            this.comboBoxGearType.SelectedIndexChanged += new System.EventHandler(this.comboBoxGear_SelectedIndexChanged);
            // 
            // comboBoxUE
            // 
            resources.ApplyResources(this.comboBoxUE, "comboBoxUE");
            this.comboBoxUE.DisplayMember = "UnitDescription";
            this.comboBoxUE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUE.FormattingEnabled = true;
            this.comboBoxUE.Name = "comboBoxUE";
            this.comboBoxUE.ValueMember = "Variant";
            this.comboBoxUE.SelectedIndexChanged += new System.EventHandler(this.comboBoxUE_SelectedIndexChanged);
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.wizardPage1);
            this.wizardExplorer.Pages.Add(this.pageSampler);
            this.wizardExplorer.Pages.Add(this.pageEfforts);
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
            // calculatorEffort
            // 
            this.calculatorEffort.WorkerReportsProgress = true;
            this.calculatorEffort.DoWork += new System.ComponentModel.DoWorkEventHandler(this.effortCalculator_DoWork);
            this.calculatorEffort.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.calculatorEffort.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.effortCalculator_RunWorkerCompleted);
            // 
            // calculatorSelection
            // 
            this.calculatorSelection.WorkerReportsProgress = true;
            this.calculatorSelection.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calculatorSelection_DoWork);
            this.calculatorSelection.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.calculatorSelection.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calculatorSelection_RunWorkerCompleted);
            // 
            // WizardGearSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardGearSet";
            this.contextEffort.ResumeLayout(false);
            this.pageEfforts.ResumeLayout(false);
            this.pageEfforts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetEfforts)).EndInit();
            this.pageSampler.ResumeLayout(false);
            this.pageSampler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextEffort;
        private System.Windows.Forms.ToolStripMenuItem contextEffortMerge;
        private AeroWizard.WizardPage pageEfforts;
        private AeroWizard.WizardPage pageSampler;
        private System.Windows.Forms.Label labelGearInstruction;
        private System.Windows.Forms.Label labelEUInstruction;
        private System.Windows.Forms.Label labelGear;
        private System.Windows.Forms.Label labelEU;
        private System.Windows.Forms.Label labelEffortInstruction;
        private System.Windows.Forms.Label labelEffortTotal;
        private System.Windows.Forms.Label labelNoticeGears;
        private System.ComponentModel.BackgroundWorker calculatorEffort;
        private System.Windows.Forms.ComboBox comboBoxGearType;
        private System.Windows.Forms.ComboBox comboBoxUE;
        private System.ComponentModel.BackgroundWorker calculatorSelection;
        private Controls.SpreadSheet spreadSheetEfforts;
        private AeroWizard.WizardControl wizardExplorer;
        private System.Windows.Forms.TextBox textBoxEffort;
        private AeroWizard.WizardPage wizardPage1;
        private System.Windows.Forms.CheckBox checkBoxSpatial;
        private Controls.SpreadSheetIconTextBoxColumn ColumnClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOperations;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEffort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEffortP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpatialWeight;
        private System.Windows.Forms.Label labelStart;
    }
}