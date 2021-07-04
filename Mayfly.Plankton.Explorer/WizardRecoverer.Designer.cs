namespace Mayfly.Plankton.Explorer
{
    partial class WizardRecoverer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardRecoverer));
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
            this.wizardControlRecoverer = new AeroWizard.WizardControl();
            this.wizardPageInit = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.spreadSheetInit = new Mayfly.Controls.SpreadSheet();
            this.columnInitSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnInitCount = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.columnInitUnweighted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnInitAbstract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnInitUnweightedTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wizardPageExtraLoading = new AeroWizard.WizardPage();
            this.pictureBoxCheck = new System.Windows.Forms.PictureBox();
            this.buttonAddNatural = new System.Windows.Forms.Button();
            this.labelLoaded = new System.Windows.Forms.Label();
            this.labelLoading = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPageExtraSpecies = new AeroWizard.WizardPage();
            this.checkBoxUseRawMass = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.spreadSheetExtra = new Mayfly.Controls.SpreadSheet();
            this.columnExtraSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtraCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExtraRaw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wizardPageCoordination = new AeroWizard.WizardPage();
            this.buttonSetCoordination = new System.Windows.Forms.Button();
            this.buttonResetCoo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.spreadSheetAssociation = new Mayfly.Controls.SpreadSheet();
            this.columnAsscSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAsscAssociate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wizardPagePriority = new AeroWizard.WizardPage();
            this.textBoxRate = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxTotalRecoverable = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.spreadSheetPriority = new Mayfly.Controls.SpreadSheet();
            this.columnPriSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPriRecoverable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPriRecoverableP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPriQuality = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.columnPriLength = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.columnPriRaw = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.wizardPageTotals = new AeroWizard.WizardPage();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBoxReport = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.loaderCard = new System.ComponentModel.BackgroundWorker();
            this.backgroundRecoverer = new System.ComponentModel.BackgroundWorker();
            this.openNatural = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlRecoverer)).BeginInit();
            this.wizardPageInit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetInit)).BeginInit();
            this.wizardPageExtraLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheck)).BeginInit();
            this.wizardPageExtraSpecies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetExtra)).BeginInit();
            this.wizardPageCoordination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAssociation)).BeginInit();
            this.wizardPagePriority.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetPriority)).BeginInit();
            this.wizardPageTotals.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControlRecoverer
            // 
            resources.ApplyResources(this.wizardControlRecoverer, "wizardControlRecoverer");
            this.wizardControlRecoverer.Name = "wizardControlRecoverer";
            this.wizardControlRecoverer.Pages.Add(this.wizardPageInit);
            this.wizardControlRecoverer.Pages.Add(this.wizardPageExtraLoading);
            this.wizardControlRecoverer.Pages.Add(this.wizardPageExtraSpecies);
            this.wizardControlRecoverer.Pages.Add(this.wizardPageCoordination);
            this.wizardControlRecoverer.Pages.Add(this.wizardPagePriority);
            this.wizardControlRecoverer.Pages.Add(this.wizardPageTotals);
            this.wizardControlRecoverer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControlRecoverer_Cancelling);
            this.wizardControlRecoverer.Finished += new System.EventHandler(this.wizardControlRecoverer_Finished);
            this.wizardControlRecoverer.SelectedPageChanged += new System.EventHandler(this.wizardControlRecoverer_SelectedPageChanged);
            // 
            // wizardPageInit
            // 
            this.wizardPageInit.Controls.Add(this.label1);
            this.wizardPageInit.Controls.Add(this.spreadSheetInit);
            this.wizardPageInit.Name = "wizardPageInit";
            resources.ApplyResources(this.wizardPageInit, "wizardPageInit");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // spreadSheetInit
            // 
            resources.ApplyResources(this.spreadSheetInit, "spreadSheetInit");
            this.spreadSheetInit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnInitSpecies,
            this.columnInitCount,
            this.columnInitUnweighted,
            this.columnInitAbstract,
            this.columnInitUnweightedTotal});
            this.spreadSheetInit.DefaultDecimalPlaces = 0;
            this.spreadSheetInit.Name = "spreadSheetInit";
            this.spreadSheetInit.ReadOnly = true;
            // 
            // columnInitSpecies
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.columnInitSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.columnInitSpecies, "columnInitSpecies");
            this.columnInitSpecies.Name = "columnInitSpecies";
            this.columnInitSpecies.ReadOnly = true;
            // 
            // columnInitCount
            // 
            resources.ApplyResources(this.columnInitCount, "columnInitCount");
            this.columnInitCount.Image = null;
            this.columnInitCount.Name = "columnInitCount";
            this.columnInitCount.ReadOnly = true;
            // 
            // columnInitUnweighted
            // 
            resources.ApplyResources(this.columnInitUnweighted, "columnInitUnweighted");
            this.columnInitUnweighted.Name = "columnInitUnweighted";
            this.columnInitUnweighted.ReadOnly = true;
            // 
            // columnInitAbstract
            // 
            resources.ApplyResources(this.columnInitAbstract, "columnInitAbstract");
            this.columnInitAbstract.Name = "columnInitAbstract";
            this.columnInitAbstract.ReadOnly = true;
            // 
            // columnInitUnweightedTotal
            // 
            resources.ApplyResources(this.columnInitUnweightedTotal, "columnInitUnweightedTotal");
            this.columnInitUnweightedTotal.Name = "columnInitUnweightedTotal";
            this.columnInitUnweightedTotal.ReadOnly = true;
            // 
            // wizardPageExtraLoading
            // 
            this.wizardPageExtraLoading.AllowDrop = true;
            this.wizardPageExtraLoading.AllowNext = false;
            this.wizardPageExtraLoading.Controls.Add(this.pictureBoxCheck);
            this.wizardPageExtraLoading.Controls.Add(this.buttonAddNatural);
            this.wizardPageExtraLoading.Controls.Add(this.labelLoaded);
            this.wizardPageExtraLoading.Controls.Add(this.labelLoading);
            this.wizardPageExtraLoading.Controls.Add(this.progressBar1);
            this.wizardPageExtraLoading.Controls.Add(this.label2);
            this.wizardPageExtraLoading.Name = "wizardPageExtraLoading";
            resources.ApplyResources(this.wizardPageExtraLoading, "wizardPageExtraLoading");
            this.wizardPageExtraLoading.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageExtraLoading_Commit);
            this.wizardPageExtraLoading.DragDrop += new System.Windows.Forms.DragEventHandler(this.cards_DragDrop);
            this.wizardPageExtraLoading.DragEnter += new System.Windows.Forms.DragEventHandler(this.cards_DragEnter);
            // 
            // pictureBoxCheck
            // 
            resources.ApplyResources(this.pictureBoxCheck, "pictureBoxCheck");
            this.pictureBoxCheck.Name = "pictureBoxCheck";
            this.pictureBoxCheck.TabStop = false;
            // 
            // buttonAddNatural
            // 
            resources.ApplyResources(this.buttonAddNatural, "buttonAddNatural");
            this.buttonAddNatural.Name = "buttonAddNatural";
            this.buttonAddNatural.UseVisualStyleBackColor = true;
            this.buttonAddNatural.Click += new System.EventHandler(this.buttonAddNatural_Click);
            // 
            // labelLoaded
            // 
            resources.ApplyResources(this.labelLoaded, "labelLoaded");
            this.labelLoaded.Name = "labelLoaded";
            // 
            // labelLoading
            // 
            resources.ApplyResources(this.labelLoading, "labelLoading");
            this.labelLoading.Name = "labelLoading";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // wizardPageExtraSpecies
            // 
            this.wizardPageExtraSpecies.Controls.Add(this.checkBoxUseRawMass);
            this.wizardPageExtraSpecies.Controls.Add(this.label3);
            this.wizardPageExtraSpecies.Controls.Add(this.spreadSheetExtra);
            this.wizardPageExtraSpecies.Name = "wizardPageExtraSpecies";
            resources.ApplyResources(this.wizardPageExtraSpecies, "wizardPageExtraSpecies");
            this.wizardPageExtraSpecies.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageExtraSpecies_Commit);
            // 
            // checkBoxUseRawMass
            // 
            resources.ApplyResources(this.checkBoxUseRawMass, "checkBoxUseRawMass");
            this.checkBoxUseRawMass.Name = "checkBoxUseRawMass";
            this.checkBoxUseRawMass.UseVisualStyleBackColor = true;
            this.checkBoxUseRawMass.CheckedChanged += new System.EventHandler(this.checkBoxUseRawMass_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // spreadSheetExtra
            // 
            resources.ApplyResources(this.spreadSheetExtra, "spreadSheetExtra");
            this.spreadSheetExtra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnExtraSpecies,
            this.columnExtraCount,
            this.columnExtraRaw});
            this.spreadSheetExtra.DefaultDecimalPlaces = 0;
            this.spreadSheetExtra.Name = "spreadSheetExtra";
            this.spreadSheetExtra.ReadOnly = true;
            this.spreadSheetExtra.DoubleClick += new System.EventHandler(this.spreadSheetExtra_DoubleClick);
            // 
            // columnExtraSpecies
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.columnExtraSpecies.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.columnExtraSpecies, "columnExtraSpecies");
            this.columnExtraSpecies.Name = "columnExtraSpecies";
            this.columnExtraSpecies.ReadOnly = true;
            // 
            // columnExtraCount
            // 
            resources.ApplyResources(this.columnExtraCount, "columnExtraCount");
            this.columnExtraCount.Name = "columnExtraCount";
            this.columnExtraCount.ReadOnly = true;
            // 
            // columnExtraRaw
            // 
            dataGridViewCellStyle3.Format = "N2";
            this.columnExtraRaw.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.columnExtraRaw, "columnExtraRaw");
            this.columnExtraRaw.Name = "columnExtraRaw";
            this.columnExtraRaw.ReadOnly = true;
            // 
            // wizardPageCoordination
            // 
            this.wizardPageCoordination.Controls.Add(this.buttonSetCoordination);
            this.wizardPageCoordination.Controls.Add(this.buttonResetCoo);
            this.wizardPageCoordination.Controls.Add(this.label5);
            this.wizardPageCoordination.Controls.Add(this.spreadSheetAssociation);
            this.wizardPageCoordination.Name = "wizardPageCoordination";
            resources.ApplyResources(this.wizardPageCoordination, "wizardPageCoordination");
            this.wizardPageCoordination.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageAssociation_Commit);
            // 
            // buttonSetCoordination
            // 
            resources.ApplyResources(this.buttonSetCoordination, "buttonSetCoordination");
            this.buttonSetCoordination.Name = "buttonSetCoordination";
            this.buttonSetCoordination.UseVisualStyleBackColor = true;
            this.buttonSetCoordination.Click += new System.EventHandler(this.buttonAssiciate_Click);
            // 
            // buttonResetCoo
            // 
            resources.ApplyResources(this.buttonResetCoo, "buttonResetCoo");
            this.buttonResetCoo.Name = "buttonResetCoo";
            this.buttonResetCoo.UseVisualStyleBackColor = true;
            this.buttonResetCoo.Click += new System.EventHandler(this.buttonResetCoo_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // spreadSheetAssociation
            // 
            resources.ApplyResources(this.spreadSheetAssociation, "spreadSheetAssociation");
            this.spreadSheetAssociation.CellPadding = new System.Windows.Forms.Padding(0);
            this.spreadSheetAssociation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnAsscSpecies,
            this.columnAsscAssociate});
            this.spreadSheetAssociation.DefaultDecimalPlaces = 0;
            this.spreadSheetAssociation.Name = "spreadSheetAssociation";
            this.spreadSheetAssociation.ReadOnly = true;
            this.spreadSheetAssociation.DoubleClick += new System.EventHandler(this.spreadSheetCoordination_DoubleClick);
            // 
            // columnAsscSpecies
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnAsscSpecies.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.columnAsscSpecies, "columnAsscSpecies");
            this.columnAsscSpecies.Name = "columnAsscSpecies";
            this.columnAsscSpecies.ReadOnly = true;
            // 
            // columnAsscAssociate
            // 
            this.columnAsscAssociate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.columnAsscAssociate.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.columnAsscAssociate, "columnAsscAssociate");
            this.columnAsscAssociate.Name = "columnAsscAssociate";
            this.columnAsscAssociate.ReadOnly = true;
            // 
            // wizardPagePriority
            // 
            this.wizardPagePriority.Controls.Add(this.textBoxRate);
            this.wizardPagePriority.Controls.Add(this.label15);
            this.wizardPagePriority.Controls.Add(this.textBoxTotalRecoverable);
            this.wizardPagePriority.Controls.Add(this.label7);
            this.wizardPagePriority.Controls.Add(this.label4);
            this.wizardPagePriority.Controls.Add(this.spreadSheetPriority);
            this.wizardPagePriority.Name = "wizardPagePriority";
            resources.ApplyResources(this.wizardPagePriority, "wizardPagePriority");
            // 
            // textBoxRate
            // 
            resources.ApplyResources(this.textBoxRate, "textBoxRate");
            this.textBoxRate.Name = "textBoxRate";
            this.textBoxRate.ReadOnly = true;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // textBoxTotalRecoverable
            // 
            resources.ApplyResources(this.textBoxTotalRecoverable, "textBoxTotalRecoverable");
            this.textBoxTotalRecoverable.Name = "textBoxTotalRecoverable";
            this.textBoxTotalRecoverable.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // spreadSheetPriority
            // 
            resources.ApplyResources(this.spreadSheetPriority, "spreadSheetPriority");
            this.spreadSheetPriority.CellPadding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.spreadSheetPriority.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnPriSpecies,
            this.columnPriRecoverable,
            this.columnPriRecoverableP,
            this.columnPriQuality,
            this.columnPriLength,
            this.columnPriRaw});
            this.spreadSheetPriority.DefaultDecimalPlaces = 0;
            this.spreadSheetPriority.Name = "spreadSheetPriority";
            this.spreadSheetPriority.ReadOnly = true;
            this.spreadSheetPriority.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.spreadSheetPriority_ColumnDisplayIndexChanged);
            this.spreadSheetPriority.DoubleClick += new System.EventHandler(this.spreadSheetPriority_DoubleClick);
            // 
            // columnPriSpecies
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.columnPriSpecies.DefaultCellStyle = dataGridViewCellStyle6;
            this.columnPriSpecies.Frozen = true;
            resources.ApplyResources(this.columnPriSpecies, "columnPriSpecies");
            this.columnPriSpecies.Name = "columnPriSpecies";
            this.columnPriSpecies.ReadOnly = true;
            // 
            // columnPriRecoverable
            // 
            this.columnPriRecoverable.Frozen = true;
            resources.ApplyResources(this.columnPriRecoverable, "columnPriRecoverable");
            this.columnPriRecoverable.Name = "columnPriRecoverable";
            this.columnPriRecoverable.ReadOnly = true;
            // 
            // columnPriRecoverableP
            // 
            dataGridViewCellStyle7.Format = "P1";
            this.columnPriRecoverableP.DefaultCellStyle = dataGridViewCellStyle7;
            this.columnPriRecoverableP.Frozen = true;
            resources.ApplyResources(this.columnPriRecoverableP, "columnPriRecoverableP");
            this.columnPriRecoverableP.Name = "columnPriRecoverableP";
            this.columnPriRecoverableP.ReadOnly = true;
            // 
            // columnPriQuality
            // 
            dataGridViewCellStyle8.Format = "P1";
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.columnPriQuality.DefaultCellStyle = dataGridViewCellStyle8;
            this.columnPriQuality.Frozen = true;
            resources.ApplyResources(this.columnPriQuality, "columnPriQuality");
            this.columnPriQuality.Image = null;
            this.columnPriQuality.Name = "columnPriQuality";
            this.columnPriQuality.ReadOnly = true;
            this.columnPriQuality.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnPriLength
            // 
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.columnPriLength.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.columnPriLength, "columnPriLength");
            this.columnPriLength.Image = null;
            this.columnPriLength.Name = "columnPriLength";
            this.columnPriLength.ReadOnly = true;
            this.columnPriLength.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // columnPriRaw
            // 
            dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.columnPriRaw.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.columnPriRaw, "columnPriRaw");
            this.columnPriRaw.Image = null;
            this.columnPriRaw.Name = "columnPriRaw";
            this.columnPriRaw.ReadOnly = true;
            this.columnPriRaw.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // wizardPageTotals
            // 
            this.wizardPageTotals.Controls.Add(this.label12);
            this.wizardPageTotals.Controls.Add(this.checkBoxReport);
            this.wizardPageTotals.Controls.Add(this.label8);
            this.wizardPageTotals.IsFinishPage = true;
            this.wizardPageTotals.Name = "wizardPageTotals";
            resources.ApplyResources(this.wizardPageTotals, "wizardPageTotals");
            this.wizardPageTotals.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageTotals_Commit);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // checkBoxReport
            // 
            resources.ApplyResources(this.checkBoxReport, "checkBoxReport");
            this.checkBoxReport.Name = "checkBoxReport";
            this.checkBoxReport.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // loaderCard
            // 
            this.loaderCard.WorkerReportsProgress = true;
            this.loaderCard.WorkerSupportsCancellation = true;
            this.loaderCard.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loaderCard_DoWork);
            this.loaderCard.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.loaderCard_ProgressChanged);
            this.loaderCard.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loaderCard_RunWorkerCompleted);
            // 
            // backgroundRecoverer
            // 
            this.backgroundRecoverer.WorkerReportsProgress = true;
            this.backgroundRecoverer.WorkerSupportsCancellation = true;
            this.backgroundRecoverer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundRecoverer_DoWork);
            this.backgroundRecoverer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundRecoverer_RunWorkerCompleted);
            // 
            // openNatural
            // 
            resources.ApplyResources(this.openNatural, "openNatural");
            // 
            // WizardRecoverer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardControlRecoverer);
            this.Name = "WizardRecoverer";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlRecoverer)).EndInit();
            this.wizardPageInit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetInit)).EndInit();
            this.wizardPageExtraLoading.ResumeLayout(false);
            this.wizardPageExtraLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheck)).EndInit();
            this.wizardPageExtraSpecies.ResumeLayout(false);
            this.wizardPageExtraSpecies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetExtra)).EndInit();
            this.wizardPageCoordination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetAssociation)).EndInit();
            this.wizardPagePriority.ResumeLayout(false);
            this.wizardPagePriority.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetPriority)).EndInit();
            this.wizardPageTotals.ResumeLayout(false);
            this.wizardPageTotals.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControlRecoverer;
        private AeroWizard.WizardPage wizardPageInit;
        private Controls.SpreadSheet spreadSheetInit;
        private AeroWizard.WizardPage wizardPageExtraLoading;
        private AeroWizard.WizardPage wizardPageExtraSpecies;
        private AeroWizard.WizardPage wizardPagePriority;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker loaderCard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAddNatural;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Controls.SpreadSheet spreadSheetPriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Controls.SpreadSheet spreadSheetExtra;
        private System.Windows.Forms.Label labelLoading;
        private System.Windows.Forms.Label labelLoaded;
        private System.Windows.Forms.PictureBox pictureBoxCheck;
        private AeroWizard.WizardPage wizardPageCoordination;
        private System.Windows.Forms.Label label5;
        private Controls.SpreadSheet spreadSheetAssociation;
        private System.Windows.Forms.Button buttonResetCoo;
        private System.Windows.Forms.TextBox textBoxTotalRecoverable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxReport;
        private AeroWizard.WizardPage wizardPageTotals;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnInitSpecies;
        private Controls.SpreadSheetIconTextBoxColumn columnInitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnInitUnweighted;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnInitAbstract;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnInitUnweightedTotal;
        private System.Windows.Forms.Label label12;
        private System.ComponentModel.BackgroundWorker backgroundRecoverer;
        private System.Windows.Forms.Button buttonSetCoordination;
        private System.Windows.Forms.CheckBox checkBoxUseRawMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtraSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtraCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExtraRaw;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAsscSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAsscAssociate;
        private System.Windows.Forms.TextBox textBoxRate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPriSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPriRecoverable;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPriRecoverableP;
        private Controls.SpreadSheetIconTextBoxColumn columnPriQuality;
        private Controls.SpreadSheetIconTextBoxColumn columnPriLength;
        private Controls.SpreadSheetIconTextBoxColumn columnPriRaw;
        private System.Windows.Forms.OpenFileDialog openNatural;
    }
}