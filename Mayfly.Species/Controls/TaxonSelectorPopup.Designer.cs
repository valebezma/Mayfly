namespace Mayfly.Species.Controls
{
    partial class TaxonSelectorPopup
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
            this.treeViewDerivates = new System.Windows.Forms.TreeView();
            this.backTreeLoader = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // treeViewDerivates
            // 
            this.treeViewDerivates.AllowDrop = true;
            this.treeViewDerivates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewDerivates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDerivates.FullRowSelect = true;
            this.treeViewDerivates.HideSelection = false;
            this.treeViewDerivates.HotTracking = true;
            this.treeViewDerivates.ItemHeight = 23;
            this.treeViewDerivates.Location = new System.Drawing.Point(0, 0);
            this.treeViewDerivates.Name = "treeViewDerivates";
            this.treeViewDerivates.ShowLines = false;
            this.treeViewDerivates.ShowNodeToolTips = true;
            this.treeViewDerivates.Size = new System.Drawing.Size(384, 334);
            this.treeViewDerivates.TabIndex = 2;
            this.treeViewDerivates.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeViewDerivates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeViewDerivates_KeyPress);
            // 
            // backTreeLoader
            // 
            this.backTreeLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backTreeLoader_DoWork);
            this.backTreeLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backTreeLoader_RunWorkerCompleted);
            // 
            // SelectTaxon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 334);
            this.ControlBox = false;
            this.Controls.Add(this.treeViewDerivates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "SelectTaxon";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.SelectTaxon_Deactivate);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDerivates;
        private System.ComponentModel.BackgroundWorker backTreeLoader;
    }
}