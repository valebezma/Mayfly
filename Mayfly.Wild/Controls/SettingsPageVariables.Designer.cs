namespace Mayfly.Wild.Controls
{
    partial class SettingsPageVariables
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
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddtFctrsDelete = new System.Windows.Forms.Button();
            this.buttonAddtFctrsAdd = new System.Windows.Forms.Button();
            this.labelAddtFctrs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.CheckBoxes = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView.HideSelection = false;
            this.listView.LabelEdit = true;
            this.listView.Location = new System.Drawing.Point(48, 43);
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(251, 154);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Variable";
            this.columnHeader2.Width = 215;
            // 
            // buttonAddtFctrsDelete
            // 
            this.buttonAddtFctrsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddtFctrsDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddtFctrsDelete.Location = new System.Drawing.Point(305, 43);
            this.buttonAddtFctrsDelete.Name = "buttonAddtFctrsDelete";
            this.buttonAddtFctrsDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonAddtFctrsDelete.TabIndex = 2;
            this.buttonAddtFctrsDelete.Text = "Delete";
            this.buttonAddtFctrsDelete.UseVisualStyleBackColor = true;
            this.buttonAddtFctrsDelete.Click += new System.EventHandler(this.buttonRemoveVar_Click);
            // 
            // buttonAddtFctrsAdd
            // 
            this.buttonAddtFctrsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddtFctrsAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddtFctrsAdd.Location = new System.Drawing.Point(305, 174);
            this.buttonAddtFctrsAdd.Name = "buttonAddtFctrsAdd";
            this.buttonAddtFctrsAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAddtFctrsAdd.TabIndex = 3;
            this.buttonAddtFctrsAdd.Text = "Add";
            this.buttonAddtFctrsAdd.UseVisualStyleBackColor = true;
            this.buttonAddtFctrsAdd.Click += new System.EventHandler(this.buttonNewVar_Click);
            // 
            // labelAddtFctrs
            // 
            this.labelAddtFctrs.AutoSize = true;
            this.labelAddtFctrs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelAddtFctrs.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelAddtFctrs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAddtFctrs.Location = new System.Drawing.Point(28, 0);
            this.labelAddtFctrs.Name = "labelAddtFctrs";
            this.labelAddtFctrs.Size = new System.Drawing.Size(111, 15);
            this.labelAddtFctrs.TabIndex = 0;
            this.labelAddtFctrs.Text = "Additional Variables";
            // 
            // SettingsControlVariables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.buttonAddtFctrsDelete);
            this.Controls.Add(this.buttonAddtFctrsAdd);
            this.Controls.Add(this.labelAddtFctrs);
            this.Group = "Reader";
            this.Name = "SettingsControlVariables";
            this.Section = "Variables";
            this.Size = new System.Drawing.Size(400, 200);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonAddtFctrsDelete;
        private System.Windows.Forms.Button buttonAddtFctrsAdd;
        private System.Windows.Forms.Label labelAddtFctrs;
    }
}
