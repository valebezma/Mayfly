namespace Mayfly.Software
{
    partial class GetIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetIndex));
            this.labelGetInstruction = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureIcon = new System.Windows.Forms.PictureBox();
            this.taskDialogMissingReference = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbBrowse = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDownload = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCreate = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelGetInstruction
            // 
            resources.ApplyResources(this.labelGetInstruction, "labelGetInstruction");
            this.labelGetInstruction.Name = "labelGetInstruction";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // pictureIcon
            // 
            resources.ApplyResources(this.pictureIcon, "pictureIcon");
            this.pictureIcon.Name = "pictureIcon";
            this.pictureIcon.TabStop = false;
            // 
            // taskDialogMissingReference
            // 
            this.taskDialogMissingReference.Buttons.Add(this.tdbBrowse);
            this.taskDialogMissingReference.Buttons.Add(this.tdbDownload);
            this.taskDialogMissingReference.Buttons.Add(this.tdbCreate);
            this.taskDialogMissingReference.ButtonStyle = Mayfly.TaskDialogs.TaskDialogButtonStyle.CommandLinks;
            this.taskDialogMissingReference.CenterParent = true;
            resources.ApplyResources(this.taskDialogMissingReference, "taskDialogMissingReference");
            this.taskDialogMissingReference.ExpandFooterArea = true;
            // 
            // tdbBrowse
            // 
            resources.ApplyResources(this.tdbBrowse, "tdbBrowse");
            // 
            // tdbDownload
            // 
            resources.ApplyResources(this.tdbDownload, "tdbDownload");
            this.tdbDownload.Default = true;
            // 
            // tdbCreate
            // 
            resources.ApplyResources(this.tdbCreate, "tdbCreate");
            // 
            // DialogReference
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pictureIcon);
            this.Controls.Add(this.labelGetInstruction);
            this.Controls.Add(this.progressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogReference";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.dialogReference_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TaskDialogs.TaskDialog taskDialogMissingReference;
        private TaskDialogs.TaskDialogButton tdbBrowse;
        private TaskDialogs.TaskDialogButton tdbDownload;
        private TaskDialogs.TaskDialogButton tdbCreate;
        private System.Windows.Forms.Label labelGetInstruction;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureIcon;
    }
}