namespace Mayfly.Fish.Explorer.Fishery
{
    partial class WizardSelectivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardSelectivity));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageCatches = new AeroWizard.WizardPage();
            this.label9 = new System.Windows.Forms.Label();
            this.plotC = new Mayfly.Mathematics.Charts.Plot();
            this.pageSelectivity = new AeroWizard.WizardPage();
            this.textBoxSD = new System.Windows.Forms.TextBox();
            this.labelSD = new System.Windows.Forms.Label();
            this.textBoxSF = new System.Windows.Forms.TextBox();
            this.labelSF = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.plotS = new Mayfly.Mathematics.Charts.Plot();
            this.pagePopulation = new AeroWizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.plotP = new Mayfly.Mathematics.Charts.Plot();
            this.pageReport = new AeroWizard.WizardPage();
            this.checkBoxPopulation = new System.Windows.Forms.CheckBox();
            this.checkBoxSelectivity = new System.Windows.Forms.CheckBox();
            this.checkBoxLength = new System.Windows.Forms.CheckBox();
            this.labelReport = new System.Windows.Forms.Label();
            this.checkBoxGears = new System.Windows.Forms.CheckBox();
            this.backCompose = new System.ComponentModel.BackgroundWorker();
            this.pictureBoxWarn = new System.Windows.Forms.PictureBox();
            this.labelWarn = new System.Windows.Forms.Label();
            this.columnComposition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reporter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageCatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotC)).BeginInit();
            this.pageSelectivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotS)).BeginInit();
            this.pagePopulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotP)).BeginInit();
            this.pageReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarn)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardExplorer
            // 
            resources.ApplyResources(this.wizardExplorer, "wizardExplorer");
            this.wizardExplorer.BackColor = System.Drawing.Color.White;
            this.wizardExplorer.Name = "wizardExplorer";
            this.wizardExplorer.Pages.Add(this.pageStart);
            this.wizardExplorer.Pages.Add(this.pageCatches);
            this.wizardExplorer.Pages.Add(this.pageSelectivity);
            this.wizardExplorer.Pages.Add(this.pagePopulation);
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
            // pageCatches
            // 
            this.pageCatches.Controls.Add(this.label9);
            this.pageCatches.Controls.Add(this.plotC);
            this.pageCatches.Name = "pageCatches";
            resources.ApplyResources(this.pageCatches, "pageCatches");
            this.pageCatches.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCatches_Commit);
            this.pageCatches.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageCatches_Rollback);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // plotC
            // 
            resources.ApplyResources(this.plotC, "plotC");
            this.plotC.AxisXAutoInterval = false;
            this.plotC.Name = "plotC";
            this.plotC.ShowLegend = true;
            this.plotC.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageSelectivity
            // 
            this.pageSelectivity.Controls.Add(this.textBoxSD);
            this.pageSelectivity.Controls.Add(this.labelSD);
            this.pageSelectivity.Controls.Add(this.textBoxSF);
            this.pageSelectivity.Controls.Add(this.labelSF);
            this.pageSelectivity.Controls.Add(this.label1);
            this.pageSelectivity.Controls.Add(this.plotS);
            this.pageSelectivity.Name = "pageSelectivity";
            resources.ApplyResources(this.pageSelectivity, "pageSelectivity");
            this.pageSelectivity.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageSelectivity_Commit);
            // 
            // textBoxSD
            // 
            resources.ApplyResources(this.textBoxSD, "textBoxSD");
            this.textBoxSD.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSD.Name = "textBoxSD";
            this.textBoxSD.ReadOnly = true;
            // 
            // labelSD
            // 
            resources.ApplyResources(this.labelSD, "labelSD");
            this.labelSD.Name = "labelSD";
            // 
            // textBoxSF
            // 
            resources.ApplyResources(this.textBoxSF, "textBoxSF");
            this.textBoxSF.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSF.Name = "textBoxSF";
            this.textBoxSF.ReadOnly = true;
            // 
            // labelSF
            // 
            resources.ApplyResources(this.labelSF, "labelSF");
            this.labelSF.Name = "labelSF";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // plotS
            // 
            resources.ApplyResources(this.plotS, "plotS");
            this.plotS.AxisXAutoInterval = false;
            this.plotS.AxisXAutoMaximum = false;
            this.plotS.AxisXAutoMinimum = false;
            this.plotS.AxisYAutoMaximum = false;
            this.plotS.AxisYAutoMinimum = false;
            this.plotS.AxisYInterval = 0.1D;
            this.plotS.AxisYMax = 1D;
            this.plotS.Name = "plotS";
            this.plotS.ShowLegend = true;
            this.plotS.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pagePopulation
            // 
            this.pagePopulation.Controls.Add(this.label2);
            this.pagePopulation.Controls.Add(this.plotP);
            this.pagePopulation.Name = "pagePopulation";
            resources.ApplyResources(this.pagePopulation, "pagePopulation");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // plotP
            // 
            resources.ApplyResources(this.plotP, "plotP");
            this.plotP.AxisXAutoInterval = false;
            this.plotP.AxisXAutoMaximum = false;
            this.plotP.AxisXAutoMinimum = false;
            this.plotP.AxisYAutoMaximum = false;
            this.plotP.AxisYAutoMinimum = false;
            this.plotP.AxisYMax = 1D;
            this.plotP.Name = "plotP";
            this.plotP.ShowLegend = true;
            this.plotP.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.checkBoxPopulation);
            this.pageReport.Controls.Add(this.checkBoxSelectivity);
            this.pageReport.Controls.Add(this.checkBoxLength);
            this.pageReport.Controls.Add(this.labelReport);
            this.pageReport.Controls.Add(this.checkBoxGears);
            this.pageReport.Name = "pageReport";
            resources.ApplyResources(this.pageReport, "pageReport");
            this.pageReport.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageReport_Commit);
            // 
            // checkBoxPopulation
            // 
            resources.ApplyResources(this.checkBoxPopulation, "checkBoxPopulation");
            this.checkBoxPopulation.Checked = true;
            this.checkBoxPopulation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPopulation.Name = "checkBoxPopulation";
            this.checkBoxPopulation.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectivity
            // 
            resources.ApplyResources(this.checkBoxSelectivity, "checkBoxSelectivity");
            this.checkBoxSelectivity.Checked = true;
            this.checkBoxSelectivity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSelectivity.Name = "checkBoxSelectivity";
            this.checkBoxSelectivity.UseVisualStyleBackColor = true;
            // 
            // checkBoxLength
            // 
            resources.ApplyResources(this.checkBoxLength, "checkBoxLength");
            this.checkBoxLength.Checked = true;
            this.checkBoxLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLength.Name = "checkBoxLength";
            this.checkBoxLength.UseVisualStyleBackColor = true;
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
            // 
            // backCompose
            // 
            this.backCompose.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backCompose_DoWork);
            this.backCompose.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backCompose_RunWorkerCompleted);
            // 
            // pictureBoxWarn
            // 
            resources.ApplyResources(this.pictureBoxWarn, "pictureBoxWarn");
            this.pictureBoxWarn.Image = global::Mayfly.Resources.Icons.Attention;
            this.pictureBoxWarn.Name = "pictureBoxWarn";
            this.pictureBoxWarn.TabStop = false;
            // 
            // labelWarn
            // 
            resources.ApplyResources(this.labelWarn, "labelWarn");
            this.labelWarn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelWarn.Name = "labelWarn";
            // 
            // columnComposition
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.columnComposition.DefaultCellStyle = dataGridViewCellStyle1;
            this.columnComposition.FillWeight = 150F;
            this.columnComposition.Frozen = true;
            resources.ApplyResources(this.columnComposition, "columnComposition");
            this.columnComposition.Name = "columnComposition";
            this.columnComposition.ReadOnly = true;
            // 
            // reporter
            // 
            this.reporter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reporter_DoWork);
            this.reporter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reporter_RunWorkerCompleted);
            // 
            // WizardSelectivity
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxWarn);
            this.Controls.Add(this.labelWarn);
            this.Controls.Add(this.wizardExplorer);
            this.Name = "WizardSelectivity";
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).EndInit();
            this.pageStart.ResumeLayout(false);
            this.pageCatches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotC)).EndInit();
            this.pageSelectivity.ResumeLayout(false);
            this.pageSelectivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotS)).EndInit();
            this.pagePopulation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotP)).EndInit();
            this.pageReport.ResumeLayout(false);
            this.pageReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AeroWizard.WizardControl wizardExplorer;
        private AeroWizard.WizardPage pageStart;
        private System.Windows.Forms.Label labelStart;
        private AeroWizard.WizardPage pageReport;
        private System.ComponentModel.BackgroundWorker backCompose;
        private System.Windows.Forms.PictureBox pictureBoxWarn;
        private System.Windows.Forms.Label labelWarn;
        private AeroWizard.WizardPage pageSelectivity;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnComposition;
        private AeroWizard.WizardPage pageCatches;
        private AeroWizard.WizardPage pagePopulation;
        private System.Windows.Forms.Label label9;
        private Mathematics.Charts.Plot plotC;
        private System.Windows.Forms.Label label1;
        private Mathematics.Charts.Plot plotS;
        private System.Windows.Forms.Label label2;
        private Mathematics.Charts.Plot plotP;
        private System.Windows.Forms.CheckBox checkBoxPopulation;
        private System.Windows.Forms.CheckBox checkBoxLength;
        private System.Windows.Forms.Label labelReport;
        private System.Windows.Forms.CheckBox checkBoxGears;
        private System.ComponentModel.BackgroundWorker reporter;
        private System.Windows.Forms.CheckBox checkBoxSelectivity;
        private System.Windows.Forms.TextBox textBoxSF;
        private System.Windows.Forms.Label labelSF;
        private System.Windows.Forms.TextBox textBoxSD;
        private System.Windows.Forms.Label labelSD;
    }
}