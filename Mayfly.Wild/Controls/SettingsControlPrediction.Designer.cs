namespace Mayfly.Wild.Controls
{
    partial class SettingsControlPrediction
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listViewBio = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonBioRemove = new System.Windows.Forms.Button();
            this.buttonBioBrowse = new System.Windows.Forms.Button();
            this.checkBoxBioAutoLoad = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewBio
            // 
            this.listViewBio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBio.CheckBoxes = true;
            this.listViewBio.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName});
            this.listViewBio.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewBio.HideSelection = false;
            this.listViewBio.LabelEdit = true;
            this.listViewBio.Location = new System.Drawing.Point(51, 196);
            this.listViewBio.Name = "listViewBio";
            this.listViewBio.ShowGroups = false;
            this.listViewBio.Size = new System.Drawing.Size(247, 201);
            this.listViewBio.TabIndex = 16;
            this.listViewBio.UseCompatibleStateImageBehavior = false;
            this.listViewBio.View = System.Windows.Forms.View.Details;
            this.listViewBio.SelectedIndexChanged += new System.EventHandler(this.listViewBio_SelectedIndexChanged);
            // 
            // columnFileName
            // 
            this.columnFileName.Text = "Bio";
            this.columnFileName.Width = 150;
            // 
            // buttonBioRemove
            // 
            this.buttonBioRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBioRemove.Enabled = false;
            this.buttonBioRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBioRemove.Location = new System.Drawing.Point(308, 304);
            this.buttonBioRemove.Name = "buttonBioRemove";
            this.buttonBioRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonBioRemove.TabIndex = 18;
            this.buttonBioRemove.Text = "Remove";
            this.buttonBioRemove.UseVisualStyleBackColor = true;
            this.buttonBioRemove.Click += new System.EventHandler(this.buttonBioRemove_Click);
            // 
            // buttonBioBrowse
            // 
            this.buttonBioBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBioBrowse.Enabled = false;
            this.buttonBioBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBioBrowse.Location = new System.Drawing.Point(308, 196);
            this.buttonBioBrowse.Name = "buttonBioBrowse";
            this.buttonBioBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBioBrowse.TabIndex = 17;
            this.buttonBioBrowse.Text = "Browse...";
            this.buttonBioBrowse.UseVisualStyleBackColor = true;
            this.buttonBioBrowse.Click += new System.EventHandler(this.buttonBioBrowse_Click);
            // 
            // checkBoxBioAutoLoad
            // 
            this.checkBoxBioAutoLoad.AutoSize = true;
            this.checkBoxBioAutoLoad.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxBioAutoLoad.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxBioAutoLoad.Location = new System.Drawing.Point(51, 173);
            this.checkBoxBioAutoLoad.Name = "checkBoxBioAutoLoad";
            this.checkBoxBioAutoLoad.Size = new System.Drawing.Size(171, 17);
            this.checkBoxBioAutoLoad.TabIndex = 15;
            this.checkBoxBioAutoLoad.Text = "Automatically load bios on start";
            this.checkBoxBioAutoLoad.UseVisualStyleBackColor = true;
            this.checkBoxBioAutoLoad.CheckedChanged += new System.EventHandler(this.checkBoxBioAutoLoad_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(28, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Predition Automation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(28, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Bio Predition";
            // 
            // SettingsControlPrediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewBio);
            this.Controls.Add(this.buttonBioRemove);
            this.Controls.Add(this.buttonBioBrowse);
            this.Controls.Add(this.checkBoxBioAutoLoad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Group = "Explorer";
            this.Name = "SettingsControlPrediction";
            this.Section = "Predictions";
            this.Size = new System.Drawing.Size(400, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewBio;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.Button buttonBioRemove;
        private System.Windows.Forms.Button buttonBioBrowse;
        private System.Windows.Forms.CheckBox checkBoxBioAutoLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
