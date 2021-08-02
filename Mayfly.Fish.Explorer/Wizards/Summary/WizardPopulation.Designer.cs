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
            this.pageLength = new AeroWizard.WizardPage();
            this.labelLength = new System.Windows.Forms.Label();
            this.plotL = new Mayfly.Mathematics.Charts.Plot();
            this.pageAge = new AeroWizard.WizardPage();
            this.plotT = new Mayfly.Mathematics.Charts.Plot();
            this.label1 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxAge = new System.Windows.Forms.CheckBox();
            this.checkBoxLength = new System.Windows.Forms.CheckBox();
            this.checkBoxAppKeys = new System.Windows.Forms.CheckBox();
            this.checkBoxAppT = new System.Windows.Forms.CheckBox();
            this.checkBoxAppL = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.structureCalculator = new System.ComponentModel.BackgroundWorker();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageLength.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotL)).BeginInit();
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
            this.wizardExplorer.Pages.Add(this.pageLength);
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
            // pageLength
            // 
            this.pageLength.Controls.Add(this.labelLength);
            this.pageLength.Controls.Add(this.plotL);
            this.pageLength.Name = "pageLength";
            resources.ApplyResources(this.pageLength, "pageLength");
            this.pageLength.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageLength_Commit);
            this.pageLength.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageLength_Rollback);
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
            // 
            // plotL
            // 
            resources.ApplyResources(this.plotL, "plotL");
            this.plotL.AxisXAutoInterval = false;
            this.plotL.AxisXAutoMaximum = false;
            this.plotL.AxisXAutoMinimum = false;
            this.plotL.AxisYAutoMaximum = false;
            this.plotL.AxisYAutoMinimum = false;
            this.plotL.Name = "plotL";
            this.plotL.ShowLegend = false;
            this.plotL.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
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
            this.pageReport.Controls.Add(this.checkBoxLength);
            this.pageReport.Controls.Add(this.checkBoxAppKeys);
            this.pageReport.Controls.Add(this.checkBoxAppT);
            this.pageReport.Controls.Add(this.checkBoxAppL);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
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
            // checkBoxLength
            // 
            resources.ApplyResources(this.checkBoxLength, "checkBoxLength");
            this.checkBoxLength.Checked = true;
            this.checkBoxLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLength.Name = "checkBoxLength";
            this.checkBoxLength.UseVisualStyleBackColor = true;
            this.checkBoxLength.CheckedChanged += new System.EventHandler(this.checkBoxLength_CheckedChanged);
            this.checkBoxLength.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
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
            // checkBoxAppL
            // 
            resources.ApplyResources(this.checkBoxAppL, "checkBoxAppL");
            this.checkBoxAppL.Name = "checkBoxAppL";
            this.checkBoxAppL.UseVisualStyleBackColor = true;
            this.checkBoxAppL.EnabledChanged += new System.EventHandler(this.checkBox_EnabledChanged);
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
            // WizardStockComposition
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardStockComposition";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageLength.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotL)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxAppL;
        private System.Windows.Forms.CheckBox checkBoxLength;
        private System.ComponentModel.BackgroundWorker reporter;
        private AeroWizard.WizardPage pageLength;
        private AeroWizard.WizardPage pageAge;
        private Mathematics.Charts.Plot plotL;
        private System.Windows.Forms.Label labelLength;
        private Mathematics.Charts.Plot plotT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAge;
        private System.Windows.Forms.CheckBox checkBoxAppT;
        private System.Windows.Forms.CheckBox checkBoxAppKeys;
    }
}