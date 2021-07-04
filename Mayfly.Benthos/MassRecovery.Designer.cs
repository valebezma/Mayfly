namespace Mayfly.Benthos
{
    partial class MassRecovery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MassRecovery));
            this.wizardControlMassRecover = new AeroWizard.WizardControl();
            this.wizardPageData = new AeroWizard.WizardPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBoxCombine = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlyFit = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.listViewVars = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSpecies = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wizardPageChart = new AeroWizard.WizardPage();
            this.statChart1 = new Mayfly.Mathematics.Charts.Plot();
            this.label3 = new System.Windows.Forms.Label();
            this.cardLoader = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlMassRecover)).BeginInit();
            this.wizardPageData.SuspendLayout();
            this.wizardPageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControlMassRecover
            // 
            resources.ApplyResources(this.wizardControlMassRecover, "wizardControlMassRecover");
            this.wizardControlMassRecover.Name = "wizardControlMassRecover";
            this.wizardControlMassRecover.Pages.Add(this.wizardPageData);
            this.wizardControlMassRecover.Pages.Add(this.wizardPageChart);
            // 
            // wizardPageData
            // 
            this.wizardPageData.AllowNext = false;
            this.wizardPageData.Controls.Add(this.progressBar1);
            this.wizardPageData.Controls.Add(this.checkBoxCombine);
            this.wizardPageData.Controls.Add(this.checkBoxOnlyFit);
            this.wizardPageData.Controls.Add(this.label4);
            this.wizardPageData.Controls.Add(this.label2);
            this.wizardPageData.Controls.Add(this.label1);
            this.wizardPageData.Controls.Add(this.label29);
            this.wizardPageData.Controls.Add(this.listViewVars);
            this.wizardPageData.Controls.Add(this.listViewSpecies);
            this.wizardPageData.Name = "wizardPageData";
            resources.ApplyResources(this.wizardPageData, "wizardPageData");
            this.wizardPageData.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageData_Commit);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // checkBoxCombine
            // 
            resources.ApplyResources(this.checkBoxCombine, "checkBoxCombine");
            this.checkBoxCombine.Checked = true;
            this.checkBoxCombine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCombine.Name = "checkBoxCombine";
            this.checkBoxCombine.UseVisualStyleBackColor = true;
            // 
            // checkBoxOnlyFit
            // 
            resources.ApplyResources(this.checkBoxOnlyFit, "checkBoxOnlyFit");
            this.checkBoxOnlyFit.Checked = true;
            this.checkBoxOnlyFit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOnlyFit.Name = "checkBoxOnlyFit";
            this.checkBoxOnlyFit.UseVisualStyleBackColor = true;
            this.checkBoxOnlyFit.CheckedChanged += new System.EventHandler(this.checkBoxOnlyFit_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // listViewVars
            // 
            this.listViewVars.AllowDrop = true;
            resources.ApplyResources(this.listViewVars, "listViewVars");
            this.listViewVars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewVars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewVars.FullRowSelect = true;
            this.listViewVars.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewVars.Name = "listViewVars";
            this.listViewVars.ShowGroups = false;
            this.listViewVars.TileSize = new System.Drawing.Size(180, 25);
            this.listViewVars.UseCompatibleStateImageBehavior = false;
            this.listViewVars.View = System.Windows.Forms.View.Details;
            this.listViewVars.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listViewVars.DragDrop += new System.Windows.Forms.DragEventHandler(this.CardsDragDrop);
            this.listViewVars.DragEnter += new System.Windows.Forms.DragEventHandler(this.CardsDragEnter);
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // listViewSpecies
            // 
            this.listViewSpecies.AllowDrop = true;
            resources.ApplyResources(this.listViewSpecies, "listViewSpecies");
            this.listViewSpecies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSpecies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewSpecies.FullRowSelect = true;
            this.listViewSpecies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewSpecies.Name = "listViewSpecies";
            this.listViewSpecies.ShowGroups = false;
            this.listViewSpecies.TileSize = new System.Drawing.Size(180, 25);
            this.listViewSpecies.UseCompatibleStateImageBehavior = false;
            this.listViewSpecies.View = System.Windows.Forms.View.Details;
            this.listViewSpecies.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listViewSpecies.DragDrop += new System.Windows.Forms.DragEventHandler(this.CardsDragDrop);
            this.listViewSpecies.DragEnter += new System.Windows.Forms.DragEventHandler(this.CardsDragEnter);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // wizardPageChart
            // 
            this.wizardPageChart.Controls.Add(this.statChart1);
            this.wizardPageChart.Controls.Add(this.label3);
            this.wizardPageChart.IsFinishPage = true;
            this.wizardPageChart.Name = "wizardPageChart";
            resources.ApplyResources(this.wizardPageChart, "wizardPageChart");
            this.wizardPageChart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageChart_Commit);
            // 
            // statChart1
            // 
            resources.ApplyResources(this.statChart1, "statChart1");
            this.statChart1.AxisXTitle = "";
            this.statChart1.AxisYTitle = "";
            this.statChart1.Name = "statChart1";
            this.statChart1.ShowLegend = false;
            this.statChart1.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cardLoader
            // 
            this.cardLoader.WorkerReportsProgress = true;
            this.cardLoader.WorkerSupportsCancellation = true;
            this.cardLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cardLoader_DoWork);
            this.cardLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged);
            this.cardLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.cardLoader_RunWorkerCompleted);
            // 
            // MassRecovery
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardControlMassRecover);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MassRecovery";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlMassRecover)).EndInit();
            this.wizardPageData.ResumeLayout(false);
            this.wizardPageData.PerformLayout();
            this.wizardPageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControlMassRecover;
        private AeroWizard.WizardPage wizardPageData;
        private AeroWizard.WizardPage wizardPageChart;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxOnlyFit;
        private Mathematics.Charts.Plot statChart1;
        private System.ComponentModel.BackgroundWorker cardLoader;
        private System.Windows.Forms.ListView listViewVars;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listViewSpecies;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBoxCombine;
    }
}