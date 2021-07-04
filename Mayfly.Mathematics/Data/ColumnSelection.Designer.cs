namespace Mayfly.Mathematics
{
    partial class ColumnSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnSelection));
            this.comboBoxValues = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.listViewGroupers = new Mayfly.Controls.SortableListView();
            this.ColumnFactors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // comboBoxValues
            // 
            resources.ApplyResources(this.comboBoxValues, "comboBoxValues");
            this.comboBoxValues.DisplayMember = "HeaderText";
            this.comboBoxValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxValues.FormattingEnabled = true;
            this.comboBoxValues.Name = "comboBoxValues";
            this.comboBoxValues.SelectedIndexChanged += new System.EventHandler(this.comboBoxArgument_SelectedIndexChanged);
            this.comboBoxValues.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // listViewGroupers
            // 
            resources.ApplyResources(this.listViewGroupers, "listViewGroupers");
            this.listViewGroupers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewGroupers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnFactors});
            this.listViewGroupers.FullRowSelect = true;
            this.listViewGroupers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewGroupers.HideSelection = false;
            this.listViewGroupers.LineAfter = -1;
            this.listViewGroupers.LineBefore = -1;
            this.listViewGroupers.Name = "listViewGroupers";
            this.listViewGroupers.ShowGroups = false;
            this.listViewGroupers.TileSize = new System.Drawing.Size(180, 25);
            this.listViewGroupers.UseCompatibleStateImageBehavior = false;
            this.listViewGroupers.View = System.Windows.Forms.View.Details;
            this.listViewGroupers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewGroupers_MouseDown);
            this.listViewGroupers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listViewGroupers_MouseMove);
            this.listViewGroupers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewGroupers_MouseUp);
            // 
            // ColumnFactors
            // 
            resources.ApplyResources(this.ColumnFactors, "ColumnFactors");
            // 
            // ColumnSelection
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.listViewGroupers);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxValues);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ColumnHeader ColumnFactors;
        public Mayfly.Controls.SortableListView listViewGroupers;
    }
}