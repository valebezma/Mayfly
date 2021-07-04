namespace Mayfly.Fish
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxVars = new System.Windows.Forms.ListBox();
            this.listBoxSpecies = new System.Windows.Forms.ListBox();
            this.label29 = new System.Windows.Forms.Label();
            this.wizardPageChart = new AeroWizard.WizardPage();
            this.label3 = new System.Windows.Forms.Label();
            this.statChart1 = new Mayfly.Statistics.StatChart();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlMassRecover)).BeginInit();
            this.wizardPageData.SuspendLayout();
            this.wizardPageChart.SuspendLayout();
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
            resources.ApplyResources(this.wizardPageData, "wizardPageData");
            this.wizardPageData.AllowNext = false;
            this.wizardPageData.Controls.Add(this.label4);
            this.wizardPageData.Controls.Add(this.label2);
            this.wizardPageData.Controls.Add(this.label1);
            this.wizardPageData.Controls.Add(this.listBoxVars);
            this.wizardPageData.Controls.Add(this.listBoxSpecies);
            this.wizardPageData.Controls.Add(this.label29);
            this.wizardPageData.Name = "wizardPageData";
            this.wizardPageData.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageData_Commit);
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
            // listBoxVars
            // 
            resources.ApplyResources(this.listBoxVars, "listBoxVars");
            this.listBoxVars.AllowDrop = true;
            this.listBoxVars.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxVars.FormattingEnabled = true;
            this.listBoxVars.Name = "listBoxVars";
            this.listBoxVars.SelectedIndexChanged += new System.EventHandler(this.listBoxSpecies_SelectedIndexChanged);
            this.listBoxVars.DragDrop += new System.Windows.Forms.DragEventHandler(this.CardsDragDrop);
            this.listBoxVars.DragEnter += new System.Windows.Forms.DragEventHandler(this.CardsDragEnter);
            // 
            // listBoxSpecies
            // 
            resources.ApplyResources(this.listBoxSpecies, "listBoxSpecies");
            this.listBoxSpecies.AllowDrop = true;
            this.listBoxSpecies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxSpecies.FormattingEnabled = true;
            this.listBoxSpecies.Name = "listBoxSpecies";
            this.listBoxSpecies.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxSpecies.SelectedIndexChanged += new System.EventHandler(this.listBoxSpecies_SelectedIndexChanged);
            this.listBoxSpecies.DragDrop += new System.Windows.Forms.DragEventHandler(this.CardsDragDrop);
            this.listBoxSpecies.DragEnter += new System.Windows.Forms.DragEventHandler(this.CardsDragEnter);
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // wizardPageChart
            // 
            resources.ApplyResources(this.wizardPageChart, "wizardPageChart");
            this.wizardPageChart.Controls.Add(this.label3);
            this.wizardPageChart.Controls.Add(this.statChart1);
            this.wizardPageChart.IsFinishPage = true;
            this.wizardPageChart.Name = "wizardPageChart";
            this.wizardPageChart.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageChart_Commit);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // statChart1
            // 
            resources.ApplyResources(this.statChart1, "statChart1");
            this.statChart1.IsChronic = false;
            this.statChart1.IsCursorDragging = false;
            this.statChart1.Name = "statChart1";
            this.statChart1.SelectedArgumentCursor = null;
            this.statChart1.SelectedFunctionCursor = null;
            this.statChart1.SelectionCursor = null;
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
            this.wizardPageChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControlMassRecover;
        private AeroWizard.WizardPage wizardPageData;
        private AeroWizard.WizardPage wizardPageChart;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxVars;
        private System.Windows.Forms.ListBox listBoxSpecies;
        public Mayfly.Statistics.StatChart statChart1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}