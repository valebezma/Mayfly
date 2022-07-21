namespace Mayfly.Software
{
    partial class Settings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.treeViewCatogories = new System.Windows.Forms.TreeView();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Controls.Add(this.buttonApply);
            this.panelButtons.Controls.Add(this.buttonOK);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // treeViewCatogories
            // 
            resources.ApplyResources(this.treeViewCatogories, "treeViewCatogories");
            this.treeViewCatogories.FullRowSelect = true;
            this.treeViewCatogories.HideSelection = false;
            this.treeViewCatogories.HotTracking = true;
            this.treeViewCatogories.ItemHeight = 25;
            this.treeViewCatogories.Name = "treeViewCatogories";
            this.treeViewCatogories.ShowLines = false;
            this.treeViewCatogories.ShowRootLines = false;
            this.treeViewCatogories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCatogories_AfterSelect);
            // 
            // panelContent
            // 
            resources.ApplyResources(this.panelContent, "panelContent");
            this.panelContent.Name = "panelContent";
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.treeViewCatogories);
            this.Controls.Add(this.panelButtons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Settings_Load);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TreeView treeViewCatogories;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button buttonApply;
    }
}