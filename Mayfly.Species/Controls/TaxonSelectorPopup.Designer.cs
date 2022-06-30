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
            this.components = new System.ComponentModel.Container();
            this.taxaTreeView = new Mayfly.Species.Controls.TaxaTreeView(this.components);
            this.SuspendLayout();
            // 
            // taxaTreeView
            // 
            this.taxaTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taxaTreeView.DeepestRank = null;
            this.taxaTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taxaTreeView.FullRowSelect = true;
            this.taxaTreeView.HigherTaxonFormat = "F";
            this.taxaTreeView.HotTracking = true;
            this.taxaTreeView.Location = new System.Drawing.Point(0, 0);
            this.taxaTreeView.LowerTaxonColor = System.Drawing.Color.Empty;
            this.taxaTreeView.LowerTaxonFormat = "F";
            this.taxaTreeView.Name = "taxaTreeView";
            this.taxaTreeView.PickedTaxon = null;
            this.taxaTreeView.RootTaxon = null;
            this.taxaTreeView.Size = new System.Drawing.Size(384, 334);
            this.taxaTreeView.Sorted = true;
            this.taxaTreeView.TabIndex = 0;
            this.taxaTreeView.OnTreeLoaded += new System.EventHandler(this.taxaTreeView_OnTreeLoaded);
            this.taxaTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taxaTreeView_KeyPress);
            // 
            // TaxonSelectorPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 334);
            this.ControlBox = false;
            this.Controls.Add(this.taxaTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "TaxonSelectorPopup";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.SelectTaxon_Deactivate);
            this.ResumeLayout(false);

        }

        #endregion

        private TaxaTreeView taxaTreeView;
    }
}