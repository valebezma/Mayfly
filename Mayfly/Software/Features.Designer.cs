namespace Mayfly.Software
{
    partial class Features
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Features));
            this.labelInstruction = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonGet = new System.Windows.Forms.Button();
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.columnHeaderFeature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpire = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInstruction
            // 
            resources.ApplyResources(this.labelInstruction, "labelInstruction");
            this.labelInstruction.Name = "labelInstruction";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonGet
            // 
            resources.ApplyResources(this.buttonGet, "buttonGet");
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // listViewLicenses
            // 
            resources.ApplyResources(this.listViewLicenses, "listViewLicenses");
            this.listViewLicenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLicenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFeature,
            this.columnHeaderExpire});
            this.listViewLicenses.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLicenses.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewLicenses.Groups1")))});
            this.listViewLicenses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLicenses.HideSelection = false;
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.ShowGroups = false;
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Details;
            this.listViewLicenses.ItemActivate += new System.EventHandler(this.listViewLicenses_ItemActivate);
            // 
            // columnHeaderFeature
            // 
            resources.ApplyResources(this.columnHeaderFeature, "columnHeaderFeature");
            // 
            // columnHeaderExpire
            // 
            resources.ApplyResources(this.columnHeaderExpire, "columnHeaderExpire");
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // Features
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.listViewLicenses);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelInstruction);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Features";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Features_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Button buttonOK;
        protected System.Windows.Forms.Button buttonGet;
        protected System.Windows.Forms.ListView listViewLicenses;
        protected System.Windows.Forms.ColumnHeader columnHeaderFeature;
        private System.Windows.Forms.ColumnHeader columnHeaderExpire;
        private System.Windows.Forms.Button buttonAdd;
    }
}