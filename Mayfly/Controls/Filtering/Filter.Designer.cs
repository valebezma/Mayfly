namespace Mayfly.Controls
{
    partial class Filter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Filter));
            this.flowFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.pictureBoxChanges = new System.Windows.Forms.PictureBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDrop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Filterator = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChanges)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowFilters
            // 
            resources.ApplyResources(this.flowFilters, "flowFilters");
            this.flowFilters.Name = "flowFilters";
            this.flowFilters.SizeChanged += new System.EventHandler(this.flowFilters_SizeChanged);
            this.flowFilters.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowFilters_ControlAdded);
            this.flowFilters.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flowFilters_ControlRemoved);
            // 
            // buttonFilter
            // 
            resources.ApplyResources(this.buttonFilter, "buttonFilter");
            this.buttonFilter.FlatAppearance.BorderSize = 0;
            this.buttonFilter.Name = "buttonFilter";
            this.toolTip1.SetToolTip(this.buttonFilter, resources.GetString("buttonFilter.ToolTip"));
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // pictureBoxChanges
            // 
            resources.ApplyResources(this.pictureBoxChanges, "pictureBoxChanges");
            this.pictureBoxChanges.Image = global::Mayfly.Resources.Icons.Flag;
            this.pictureBoxChanges.Name = "pictureBoxChanges";
            this.pictureBoxChanges.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxChanges, resources.GetString("pictureBoxChanges.ToolTip"));
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.Image = global::Mayfly.Controls.Filtering.Filtering.FunnelPlus;
            this.buttonAdd.Name = "buttonAdd";
            this.toolTip1.SetToolTip(this.buttonAdd, resources.GetString("buttonAdd.ToolTip"));
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDrop
            // 
            resources.ApplyResources(this.buttonDrop, "buttonDrop");
            this.buttonDrop.FlatAppearance.BorderSize = 0;
            this.buttonDrop.Name = "buttonDrop";
            this.toolTip1.SetToolTip(this.buttonDrop, resources.GetString("buttonDrop.ToolTip"));
            this.buttonDrop.Click += new System.EventHandler(this.buttonDrop_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.flowFilters);
            this.panel1.Name = "panel1";
            // 
            // Filterator
            // 
            this.Filterator.WorkerReportsProgress = true;
            this.Filterator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Filterator_DoWork);
            this.Filterator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Filterator_ProgressChanged);
            this.Filterator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Filterator_RunWorkerCompleted);
            // 
            // Filter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxChanges);
            this.Controls.Add(this.buttonDrop);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.buttonAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Filter";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Filter_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChanges)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowFilters;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.PictureBox pictureBoxChanges;
        private System.ComponentModel.BackgroundWorker Filterator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDrop;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}