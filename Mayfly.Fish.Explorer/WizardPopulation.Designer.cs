namespace Mayfly.Fish.Explorer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardExplorer = new AeroWizard.WizardControl();
            this.pageStart = new AeroWizard.WizardPage();
            this.labelStart = new System.Windows.Forms.Label();
            this.pageBasic = new AeroWizard.WizardPage();
            this.buttonL = new System.Windows.Forms.Button();
            this.buttonT = new System.Windows.Forms.Button();
            this.buttonW = new System.Windows.Forms.Button();
            this.labelT = new System.Windows.Forms.Label();
            this.labelBiomass = new System.Windows.Forms.Label();
            this.labelW = new System.Windows.Forms.Label();
            this.labelL = new System.Windows.Forms.Label();
            this.labelBasic = new System.Windows.Forms.Label();
            this.pageCpue = new AeroWizard.WizardPage();
            this.spreadSheet1 = new Mayfly.Controls.SpreadSheet();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.wizardExplorer)).BeginInit();
            this.pageStart.SuspendLayout();
            this.pageBasic.SuspendLayout();
            this.pageCpue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheet1)).BeginInit();
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
            this.wizardExplorer.Pages.Add(this.pageCpue);
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
            this.pageBasic.Controls.Add(this.buttonT);
            this.pageBasic.Controls.Add(this.buttonW);
            this.pageBasic.Controls.Add(this.labelT);
            this.pageBasic.Controls.Add(this.labelBiomass);
            this.pageBasic.Controls.Add(this.labelW);
            this.pageBasic.Controls.Add(this.labelL);
            this.pageBasic.Controls.Add(this.labelBasic);
            this.pageBasic.Name = "pageBasic";
            resources.ApplyResources(this.pageBasic, "pageBasic");
            this.pageBasic.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageBasic_Commit);
            // 
            // buttonL
            // 
            resources.ApplyResources(this.buttonL, "buttonL");
            this.buttonL.Name = "buttonL";
            this.buttonL.UseVisualStyleBackColor = true;
            // 
            // buttonT
            // 
            resources.ApplyResources(this.buttonT, "buttonT");
            this.buttonT.Name = "buttonT";
            this.buttonT.UseVisualStyleBackColor = true;
            // 
            // buttonW
            // 
            resources.ApplyResources(this.buttonW, "buttonW");
            this.buttonW.Name = "buttonW";
            this.buttonW.UseVisualStyleBackColor = true;
            // 
            // labelT
            // 
            resources.ApplyResources(this.labelT, "labelT");
            this.labelT.Name = "labelT";
            // 
            // labelBiomass
            // 
            resources.ApplyResources(this.labelBiomass, "labelBiomass");
            this.labelBiomass.Name = "labelBiomass";
            // 
            // labelW
            // 
            resources.ApplyResources(this.labelW, "labelW");
            this.labelW.Name = "labelW";
            // 
            // labelL
            // 
            resources.ApplyResources(this.labelL, "labelL");
            this.labelL.Name = "labelL";
            // 
            // labelBasic
            // 
            resources.ApplyResources(this.labelBasic, "labelBasic");
            this.labelBasic.Name = "labelBasic";
            // 
            // pageCpue
            // 
            this.pageCpue.Controls.Add(this.spreadSheet1);
            this.pageCpue.Controls.Add(this.label2);
            this.pageCpue.Controls.Add(this.textBox2);
            this.pageCpue.Controls.Add(this.label3);
            this.pageCpue.Controls.Add(this.textBox1);
            this.pageCpue.Controls.Add(this.label4);
            this.pageCpue.Name = "pageCpue";
            resources.ApplyResources(this.pageCpue, "pageCpue");
            this.pageCpue.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.PageCpue_Commit);
            // 
            // spreadSheet1
            // 
            resources.ApplyResources(this.spreadSheet1, "spreadSheet1");
            this.spreadSheet1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.spreadSheet1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.spreadSheet1.DefaultDecimalPlaces = 0;
            this.spreadSheet1.Name = "spreadSheet1";
            this.spreadSheet1.ReadOnly = true;
            this.spreadSheet1.RowHeadersVisible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle2.Format = "N3";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle3.Format = "N3";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle4.Format = "N3";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle5.Format = "N3";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.BackColor = System.Drawing.SystemColors.Window;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            this.pageCpue.ResumeLayout(false);
            this.pageCpue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheet1)).EndInit();
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
        private System.Windows.Forms.Label labelW;
        private System.Windows.Forms.Label labelL;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonW;
        private System.Windows.Forms.Button buttonT;
        private System.Windows.Forms.Label labelT;
        private AeroWizard.WizardPage pageCpue;
        private Controls.SpreadSheet spreadSheet1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
    }
}