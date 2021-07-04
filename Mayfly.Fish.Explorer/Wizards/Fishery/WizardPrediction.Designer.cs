namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardPrediction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardPrediction));
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pagePrediction = new AeroWizard.WizardPage();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.labelChangesManage = new System.Windows.Forms.Label();
            this.plotPrediction = new Mayfly.Mathematics.Charts.Plot();
            this.label1 = new System.Windows.Forms.Label();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxGrowth = new System.Windows.Forms.CheckBox();
            this.checkBoxVPA = new System.Windows.Forms.CheckBox();
            this.checkBoxMSY = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pagePrediction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotPrediction)).BeginInit();
            this.pageReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pagePrediction);
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
            // pagePrediction
            // 
            this.pagePrediction.Controls.Add(this.numericUpDownX);
            this.pagePrediction.Controls.Add(this.labelChangesManage);
            this.pagePrediction.Controls.Add(this.plotPrediction);
            this.pagePrediction.Controls.Add(this.label1);
            this.pagePrediction.Name = "pagePrediction";
            resources.ApplyResources(this.pagePrediction, "pagePrediction");
            this.pagePrediction.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pagePrediction_Rollback);
            // 
            // numericUpDownX
            // 
            resources.ApplyResources(this.numericUpDownX, "numericUpDownX");
            this.numericUpDownX.DecimalPlaces = 3;
            this.numericUpDownX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // labelChangesManage
            // 
            resources.ApplyResources(this.labelChangesManage, "labelChangesManage");
            this.labelChangesManage.Name = "labelChangesManage";
            // 
            // plotPrediction
            // 
            resources.ApplyResources(this.plotPrediction, "plotPrediction");
            this.plotPrediction.AxisXAutoMaximum = false;
            this.plotPrediction.AxisXAutoMinimum = false;
            this.plotPrediction.AxisXMax = 43197.629599328706D;
            this.plotPrediction.AxisXMin = 43197.629599328706D;
            this.plotPrediction.AxisYAutoMinimum = false;
            this.plotPrediction.AxisYMax = 10D;
            this.plotPrediction.IsChronic = true;
            this.plotPrediction.Name = "plotPrediction";
            this.plotPrediction.ShowLegend = true;
            this.plotPrediction.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Years;
            this.plotPrediction.StructureValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxGrowth);
            this.pageReport.Controls.Add(this.checkBoxVPA);
            this.pageReport.Controls.Add(this.checkBoxMSY);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxGrowth
            // 
            resources.ApplyResources(this.checkBoxGrowth, "checkBoxGrowth");
            this.checkBoxGrowth.Checked = true;
            this.checkBoxGrowth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGrowth.Name = "checkBoxGrowth";
            this.checkBoxGrowth.UseVisualStyleBackColor = true;
            // 
            // checkBoxVPA
            // 
            resources.ApplyResources(this.checkBoxVPA, "checkBoxVPA");
            this.checkBoxVPA.Checked = true;
            this.checkBoxVPA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVPA.Name = "checkBoxVPA";
            this.checkBoxVPA.UseVisualStyleBackColor = true;
            // 
            // checkBoxMSY
            // 
            resources.ApplyResources(this.checkBoxMSY, "checkBoxMSY");
            this.checkBoxMSY.Checked = true;
            this.checkBoxMSY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMSY.Name = "checkBoxMSY";
            this.checkBoxMSY.UseVisualStyleBackColor = true;
            // 
            // labelReport
            // 
            resources.ApplyResources(this.labelReport, "labelReport");
            this.labelReport.Name = "labelReport";
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // WizardPrediction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardPrediction";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pagePrediction.ResumeLayout(false);
            this.pagePrediction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotPrediction)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private AeroWizard.WizardPage pageReport;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxMSY;
        private System.Windows.Forms.CheckBox checkBoxGrowth;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.CheckBox checkBoxVPA;
        private AeroWizard.WizardPage pagePrediction;
        private System.Windows.Forms.Label label1;
        private Mathematics.Charts.Plot plotPrediction;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label labelChangesManage;
    }
}