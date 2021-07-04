namespace Mayfly.Wild.Exchange
{
    partial class Permissions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Permissions));
            this.listViewPermissions = new System.Windows.Forms.ListView();
            this.columnHeaderGranter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpire = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonInstall = new System.Windows.Forms.Button();
            this.labelExchangeInstruction = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.inputDialogPassword = new Mayfly.TaskDialogs.InputDialog(this.components);
            this.taskDialogPermitError = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdInstallCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.buttonGrant = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewPermissions
            // 
            this.listViewPermissions.AllowDrop = true;
            resources.ApplyResources(this.listViewPermissions, "listViewPermissions");
            this.listViewPermissions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPermissions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderGranter,
            this.columnHeaderExpire});
            this.listViewPermissions.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewPermissions.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewPermissions.Groups1")))});
            this.listViewPermissions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPermissions.HideSelection = false;
            this.listViewPermissions.Name = "listViewPermissions";
            this.listViewPermissions.UseCompatibleStateImageBehavior = false;
            this.listViewPermissions.View = System.Windows.Forms.View.Details;
            this.listViewPermissions.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewPermissions_DragDrop);
            this.listViewPermissions.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewPermissions_DragEnter);
            // 
            // columnHeaderGranter
            // 
            resources.ApplyResources(this.columnHeaderGranter, "columnHeaderGranter");
            // 
            // columnHeaderExpire
            // 
            resources.ApplyResources(this.columnHeaderExpire, "columnHeaderExpire");
            // 
            // buttonInstall
            // 
            resources.ApplyResources(this.buttonInstall, "buttonInstall");
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // labelExchangeInstruction
            // 
            resources.ApplyResources(this.labelExchangeInstruction, "labelExchangeInstruction");
            this.labelExchangeInstruction.Name = "labelExchangeInstruction";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // inputDialogPassword
            // 
            resources.ApplyResources(this.inputDialogPassword, "inputDialogPassword");
            // 
            // taskDialogPermitError
            // 
            this.taskDialogPermitError.Buttons.Add(this.tdInstallCancel);
            resources.ApplyResources(this.taskDialogPermitError, "taskDialogPermitError");
            // 
            // tdInstallCancel
            // 
            this.tdInstallCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // buttonGrant
            // 
            resources.ApplyResources(this.buttonGrant, "buttonGrant");
            this.buttonGrant.Name = "buttonGrant";
            this.buttonGrant.UseVisualStyleBackColor = true;
            this.buttonGrant.Click += new System.EventHandler(this.buttonGrant_Click);
            // 
            // Permissions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.listViewPermissions);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonGrant);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.labelExchangeInstruction);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Permissions";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ListView listViewPermissions;
        protected System.Windows.Forms.ColumnHeader columnHeaderGranter;
        private System.Windows.Forms.ColumnHeader columnHeaderExpire;
        protected System.Windows.Forms.Button buttonInstall;
        protected System.Windows.Forms.Label labelExchangeInstruction;
        protected System.Windows.Forms.Button buttonOK;
        private TaskDialogs.InputDialog inputDialogPassword;
        private TaskDialogs.TaskDialog taskDialogPermitError;
        private TaskDialogs.TaskDialogButton tdInstallCancel;
        private System.Windows.Forms.Button buttonGrant;
    }
}