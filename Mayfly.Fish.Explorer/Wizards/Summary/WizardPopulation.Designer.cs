namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardStockComposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardStockComposition));
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageBasic = new AeroWizard.WizardPage();
            this.labelBasic = new System.Windows.Forms.Label();
            this.pageAge = new AeroWizard.WizardPage();
            this.plotT = new Mayfly.Mathematics.Charts.Plot();
            this.label1 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxAge = new System.Windows.Forms.CheckBox();
            this.checkBoxAppKeys = new System.Windows.Forms.CheckBox();
            this.checkBoxAppT = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.structureCalculator = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            this.labelBiomass = new System.Windows.Forms.Label();
            this.labelAbundance = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.buttonW = new System.Windows.Forms.Button();
            this.buttonL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageBasic.SuspendLayout();
            this.pageAge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotT)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageBasic);
            this.wizardExplorer.Pages.Add(this.pageAge);
            this.wizardExplorer.Pages.Add(this.pageReport);
            this.wizardExplorer.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardExplorer_Cancelling);
            // 
            // pageStart
            // 
            this.pageStart.Controls.Add(this.labelStart);
            this.pageStart.Name = "pageStart";
            resources.ApplyResources(this.pageStart, "pageStart");
            this.pageStart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageStart_Commit);
            // 
            // labelStart
            // 
            resources.ApplyResources(this.labelStart, "labelStart");
            this.labelStart.Name = "labelStart";
            // 
            // pageBasic
            // 
            this.pageBasic.Controls.Add(this.buttonL);
            this.pageBasic.Controls.Add(this.buttonW);
            this.pageBasic.Controls.Add(this.labelBiomass);
            this.pageBasic.Controls.Add(this.labelAbundance);
            this.pageBasic.Controls.Add(this.labelLength);
            this.pageBasic.Controls.Add(this.labelBasic);
            this.pageBasic.Name = "pageBasic";
            resources.ApplyResources(this.pageBasic, "pageBasic");
            this.pageBasic.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageBasic_Commit);
            // 
            // labelBasic
            // 
            resources.ApplyResources(this.labelBasic, "labelBasic");
            this.labelBasic.Name = "labelBasic";
            // 
            // pageAge
            // 
            this.pageAge.Controls.Add(this.plotT);
            this.pageAge.Controls.Add(this.label1);
            this.pageAge.Name = "pageAge";
            resources.ApplyResources(this.pageAge, "pageAge");
            this.pageAge.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageAge_Rollback);
            // 
            // plotT
            // 
            resources.ApplyResources(this.plotT, "plotT");
            this.plotT.AxisXAutoMaximum = false;
            this.plotT.AxisXAutoMinimum = false;
            this.plotT.AxisYAutoMaximum = false;
            this.plotT.AxisYAutoMinimum = false;
            this.plotT.Name = "plotT";
            this.plotT.ShowLegend = false;
            this.plotT.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxAge);
            this.pageReport.Controls.Add(this.checkBoxAppKeys);
            this.pageReport.Controls.Add(this.checkBoxAppT);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            this.pageReport.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageReport_Rollback);
            // 
            // checkBoxAge
            // 
            resources.ApplyResources(this.checkBoxAge, "checkBoxAge");
            this.checkBoxAge.Checked = true;
            this.checkBoxAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAge.Name = "checkBoxAge";
            this.checkBoxAge.UseVisualStyleBackColor = true;
            this.checkBoxAge.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
            this.checkBoxAge.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAppKeys
            // 
            resources.ApplyResources(this.checkBoxAppKeys, "checkBoxAppKeys");
            this.checkBoxAppKeys.Name = "checkBoxAppKeys";
            this.checkBoxAppKeys.UseVisualStyleBackColor = true;
            this.checkBoxAppKeys.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // checkBoxAppT
            // 
            resources.ApplyResources(this.checkBoxAppT, "checkBoxAppT");
            this.checkBoxAppT.Name = "checkBoxAppT";
            this.checkBoxAppT.UseVisualStyleBackColor = true;
            this.checkBoxAppT.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // checkBoxGears
            // 
            resources.ApplyResources(this.checkBoxGears, "checkBoxGears");
            this.checkBoxGears.Name = "checkBoxGears";
            this.checkBoxGears.UseVisualStyleBackColor = true;
            this.checkBoxGears.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
            // 
            // structureCalculator
            // 
            this.structureCalculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.structureCalculator_DoWork);
            this.structureCalculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.structureCalculator_RunWorkerCompleted);
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // labelBiomass
            // 
            resources.ApplyResources(this.labelBiomass, "labelBiomass");
            this.labelBiomass.Name = "labelBiomass";
            // 
            // labelAbundance
            // 
            resources.ApplyResources(this.labelAbundance, "labelAbundance");
            this.labelAbundance.Name = "labelAbundance";
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
            // 
            // buttonW
            // 
            resources.ApplyResources(this.buttonW, "buttonW");
            this.buttonW.Name = "buttonW";
            this.buttonW.UseVisualStyleBackColor = true;
            // 
            // buttonL
            // 
            resources.ApplyResources(this.buttonL, "buttonL");
            this.buttonL.Name = "buttonL";
            this.buttonL.UseVisualStyleBackColor = true;
            // 
            // WizardStockComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardStockComposition";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageBasic.ResumeLayout(false);
            this.pageBasic.PerformLayout();
            this.pageAge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotT)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private System.ComponentModel.BackgroundWorker structureCalculator;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageAge;
        private Mathematics.Charts.Plot plotT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAge;
        private System.Windows.Forms.CheckBox checkBoxAppT;
        private System.Windows.Forms.CheckBox checkBoxAppKeys;
        private AeroWizard.WizardPage pageBasic;
        private System.Windows.Forms.Label labelBasic;
        private System.Windows.Forms.Label labelBiomass;
        private System.Windows.Forms.Label labelAbundance;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonW;
    }
}