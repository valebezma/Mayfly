namespace Mayfly.Mathematics.Statistics
{
    partial class RegressionSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegressionSelection));
            this.comboBoxArgument = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFunction = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.listViewGroupers = new System.Windows.Forms.ListView();
            this.ColumnFactors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxCompare = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxArgument
            // 
            resources.ApplyResources(this.comboBoxArgument, "comboBoxArgument");
            this.comboBoxArgument.DisplayMember = "HeaderText";
            this.comboBoxArgument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArgument.FormattingEnabled = true;
            this.comboBoxArgument.Name = "comboBoxArgument";
            this.comboBoxArgument.SelectedIndexChanged += new System.EventHandler(this.comboBoxArgument_SelectedIndexChanged);
            this.comboBoxArgument.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
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
            this.comboBoxFunction.SelectedIndexChanged += new System.EventHandler(this.comboBoxArgument_SelectedIndexChanged);
            this.comboBoxFunction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            this.listViewGroupers.Name = "listViewGroupers";
            this.listViewGroupers.ShowGroups = false;
            this.listViewGroupers.TileSize = new System.Drawing.Size(180, 25);
            this.listViewGroupers.UseCompatibleStateImageBehavior = false;
            this.listViewGroupers.View = System.Windows.Forms.View.Details;
            this.listViewGroupers.SelectedIndexChanged += new System.EventHandler(this.listViewGroupers_SelectedIndexChanged);
            // 
            // ColumnFactors
            // 
            resources.ApplyResources(this.ColumnFactors, "ColumnFactors");
            // 
            // checkBoxCompare
            // 
            resources.ApplyResources(this.checkBoxCompare, "checkBoxCompare");
            this.checkBoxCompare.Name = "checkBoxCompare";
            this.checkBoxCompare.UseVisualStyleBackColor = true;
            // 
            // RegressionSelection
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxCompare);
            this.Controls.Add(this.listViewGroupers);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxArgument);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFunction);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegressionSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxArgument;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFunction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.ListView listViewGroupers;
        private System.Windows.Forms.ColumnHeader ColumnFactors;
        private System.Windows.Forms.CheckBox checkBoxCompare;
    }
}