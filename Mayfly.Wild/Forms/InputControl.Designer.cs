namespace Mayfly.Wild
{
    partial class InputControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputControl));
            this.labelSpeciesSelection = new System.Windows.Forms.Label();
            this.comboBoxSpecies = new System.Windows.Forms.ComboBox();
            this.comboBoxArgument = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFunction = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.backgroundChartBuilder = new System.ComponentModel.BackgroundWorker();
            this.labelLack = new System.Windows.Forms.Label();
            this.StatChart = new Mayfly.Mathematics.Charts.Plot();
            ((System.ComponentModel.ISupportInitialize)(this.StatChart)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSpeciesSelection
            // 
            resources.ApplyResources(this.labelSpeciesSelection, "labelSpeciesSelection");
            this.labelSpeciesSelection.Name = "labelSpeciesSelection";
            // 
            // comboBoxSpecies
            // 
            resources.ApplyResources(this.comboBoxSpecies, "comboBoxSpecies");
            this.comboBoxSpecies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpecies.FormattingEnabled = true;
            this.comboBoxSpecies.Name = "comboBoxSpecies";
            this.comboBoxSpecies.Sorted = true;
            this.comboBoxSpecies.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecies_SelectedIndexChanged);
            // 
            // comboBoxArgument
            // 
            resources.ApplyResources(this.comboBoxArgument, "comboBoxArgument");
            this.comboBoxArgument.DisplayMember = "HeaderText";
            this.comboBoxArgument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArgument.FormattingEnabled = true;
            this.comboBoxArgument.Name = "comboBoxArgument";
            this.comboBoxArgument.Sorted = true;
            this.comboBoxArgument.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBoxFunction
            // 
            resources.ApplyResources(this.comboBoxFunction, "comboBoxFunction");
            this.comboBoxFunction.DisplayMember = "HeaderText";
            this.comboBoxFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFunction.FormattingEnabled = true;
            this.comboBoxFunction.Name = "comboBoxFunction";
            this.comboBoxFunction.Sorted = true;
            this.comboBoxFunction.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // backgroundChartBuilder
            // 
            this.backgroundChartBuilder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundChartBuilder_DoWork);
            this.backgroundChartBuilder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundChartBuilder_RunWorkerCompleted);
            // 
            // labelLack
            // 
            resources.ApplyResources(this.labelLack, "labelLack");
            this.labelLack.Name = "labelLack";
            // 
            // StatChart
            // 
            resources.ApplyResources(this.StatChart, "StatChart");
            this.StatChart.AxisXAutoMinimum = false;
            this.StatChart.AxisYAutoMinimum = false;
            this.StatChart.Name = "StatChart";
            this.StatChart.ShowLegend = false;
            this.StatChart.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // InputControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.labelLack);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.StatChart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelSpeciesSelection);
            this.Controls.Add(this.comboBoxFunction);
            this.Controls.Add(this.comboBoxArgument);
            this.Controls.Add(this.comboBoxSpecies);
            this.Name = "InputControl";
            ((System.ComponentModel.ISupportInitialize)(this.StatChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSpeciesSelection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.ComponentModel.BackgroundWorker backgroundChartBuilder;
        private Mathematics.Charts.Plot StatChart;
        private System.Windows.Forms.Label labelLack;
        private System.Windows.Forms.ComboBox comboBoxSpecies;
        private System.Windows.Forms.ComboBox comboBoxArgument;
        private System.Windows.Forms.ComboBox comboBoxFunction;
    }
}