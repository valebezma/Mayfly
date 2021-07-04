namespace Mayfly.Management
{
    partial class AddHolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddHolder));
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimeExpire = new System.Windows.Forms.DateTimePicker();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxSn = new System.Windows.Forms.TextBox();
            this.labelPass = new System.Windows.Forms.Label();
            this.comboBoxProduct = new System.Windows.Forms.ComboBox();
            this.labelProduct = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.checkBoxExpiry = new System.Windows.Forms.CheckBox();
            this.listViewConstraints = new System.Windows.Forms.ListView();
            this.columnBinary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFeatures = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // dateTimeExpire
            // 
            resources.ApplyResources(this.dateTimeExpire, "dateTimeExpire");
            this.dateTimeExpire.Name = "dateTimeExpire";
            this.dateTimeExpire.ValueChanged += new System.EventHandler(this.condition_Changed);
            // 
            // buttonCreate
            // 
            resources.ApplyResources(this.buttonCreate, "buttonCreate");
            this.buttonCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // textBoxSn
            // 
            resources.ApplyResources(this.textBoxSn, "textBoxSn");
            this.textBoxSn.Name = "textBoxSn";
            this.textBoxSn.TextChanged += new System.EventHandler(this.textBoxSn_TextChanged);
            // 
            // labelPass
            // 
            resources.ApplyResources(this.labelPass, "labelPass");
            this.labelPass.Name = "labelPass";
            // 
            // comboBoxProduct
            // 
            this.comboBoxProduct.DisplayMember = "Name";
            this.comboBoxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProduct.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxProduct, "comboBoxProduct");
            this.comboBoxProduct.Name = "comboBoxProduct";
            this.comboBoxProduct.SelectedIndexChanged += new System.EventHandler(this.comboBoxProduct_SelectedIndexChanged);
            // 
            // labelProduct
            // 
            resources.ApplyResources(this.labelProduct, "labelProduct");
            this.labelProduct.Name = "labelProduct";
            // 
            // buttonGenerate
            // 
            resources.ApplyResources(this.buttonGenerate, "buttonGenerate");
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // checkBoxExpiry
            // 
            resources.ApplyResources(this.checkBoxExpiry, "checkBoxExpiry");
            this.checkBoxExpiry.Checked = true;
            this.checkBoxExpiry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExpiry.Name = "checkBoxExpiry";
            this.checkBoxExpiry.UseVisualStyleBackColor = true;
            this.checkBoxExpiry.CheckedChanged += new System.EventHandler(this.checkBoxExpiry_CheckedChanged);
            // 
            // listViewConstraints
            // 
            resources.ApplyResources(this.listViewConstraints, "listViewConstraints");
            this.listViewConstraints.CheckBoxes = true;
            this.listViewConstraints.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnBinary,
            this.columnVersion,
            this.columnFeatures});
            this.listViewConstraints.Name = "listViewConstraints";
            this.listViewConstraints.UseCompatibleStateImageBehavior = false;
            this.listViewConstraints.View = System.Windows.Forms.View.Details;
            this.listViewConstraints.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewConstraints_ItemCheck);
            // 
            // columnBinary
            // 
            resources.ApplyResources(this.columnBinary, "columnBinary");
            // 
            // columnVersion
            // 
            resources.ApplyResources(this.columnVersion, "columnVersion");
            // 
            // columnFeatures
            // 
            resources.ApplyResources(this.columnFeatures, "columnFeatures");
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.DisplayMember = "Name";
            this.comboBoxAccount.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxAccount, "comboBoxAccount");
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.ValueMember = "Name";
            this.comboBoxAccount.TextChanged += new System.EventHandler(this.condition_Changed);
            // 
            // AddHolder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.listViewConstraints);
            this.Controls.Add(this.checkBoxExpiry);
            this.Controls.Add(this.comboBoxAccount);
            this.Controls.Add(this.comboBoxProduct);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.dateTimeExpire);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelProduct);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxSn);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddHolder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeExpire;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ComboBox comboBoxProduct;
        private System.Windows.Forms.Label labelProduct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSn;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckBox checkBoxExpiry;
        private System.Windows.Forms.ListView listViewConstraints;
        private System.Windows.Forms.ColumnHeader columnBinary;
        private System.Windows.Forms.ColumnHeader columnVersion;
        private System.Windows.Forms.ColumnHeader columnFeatures;
        private System.Windows.Forms.ComboBox comboBoxAccount;
    }
}