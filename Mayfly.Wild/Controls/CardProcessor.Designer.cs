namespace Mayfly.Wild.Controls
{
    partial class CardProcessor
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog();
            // 
            // taskDialogSaveChanges
            // 
            this.taskDialogSaveChanges.CenterParent = true;
            this.taskDialogSaveChanges.Content = "There were some changes since last saving. Would you like to save them?";
            this.taskDialogSaveChanges.MainInstruction = "Save changes?";
            this.taskDialogSaveChanges.WindowTitle = "Fish Reader";

        }

        #endregion

        private TaskDialogs.TaskDialog taskDialogSaveChanges;
    }
}
