namespace Mayfly.Wild.Controls
{
    partial class SettingsPageLog
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
            this.components = new System.ComponentModel.Container();
            this.checkBoxAutoLog = new System.Windows.Forms.CheckBox();
            this.labelInputFish = new System.Windows.Forms.Label();
            this.checkBoxAutoDecreaseBio = new System.Windows.Forms.CheckBox();
            this.checkBoxFixTotals = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoIncreaseBio = new System.Windows.Forms.CheckBox();
            this.tdClearRecent = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbRecentClear = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbRecentCancel = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.SuspendLayout();
            // 
            // checkBoxAutoLog
            // 
            this.checkBoxAutoLog.AutoSize = true;
            this.checkBoxAutoLog.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoLog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxAutoLog.Location = new System.Drawing.Point(48, 40);
            this.checkBoxAutoLog.Name = "checkBoxAutoLog";
            this.checkBoxAutoLog.Size = new System.Drawing.Size(253, 17);
            this.checkBoxAutoLog.TabIndex = 1;
            this.checkBoxAutoLog.Text = "Open individuals log when adding new definition";
            this.checkBoxAutoLog.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoLog.UseVisualStyleBackColor = true;
            // 
            // labelInputFish
            // 
            this.labelInputFish.AutoSize = true;
            this.labelInputFish.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelInputFish.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelInputFish.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelInputFish.Location = new System.Drawing.Point(28, 0);
            this.labelInputFish.Name = "labelInputFish";
            this.labelInputFish.Size = new System.Drawing.Size(101, 15);
            this.labelInputFish.TabIndex = 0;
            this.labelInputFish.Text = "Default Behaviour";
            // 
            // checkBoxAutoDecreaseBio
            // 
            this.checkBoxAutoDecreaseBio.AutoSize = true;
            this.checkBoxAutoDecreaseBio.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoDecreaseBio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxAutoDecreaseBio.Location = new System.Drawing.Point(68, 109);
            this.checkBoxAutoDecreaseBio.Name = "checkBoxAutoDecreaseBio";
            this.checkBoxAutoDecreaseBio.Size = new System.Drawing.Size(276, 17);
            this.checkBoxAutoDecreaseBio.TabIndex = 4;
            this.checkBoxAutoDecreaseBio.Text = "Decrease total when individual values being reduced";
            this.checkBoxAutoDecreaseBio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoDecreaseBio.UseVisualStyleBackColor = true;
            // 
            // checkBoxFixTotals
            // 
            this.checkBoxFixTotals.AutoSize = true;
            this.checkBoxFixTotals.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxFixTotals.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxFixTotals.Location = new System.Drawing.Point(48, 63);
            this.checkBoxFixTotals.Name = "checkBoxFixTotals";
            this.checkBoxFixTotals.Size = new System.Drawing.Size(299, 17);
            this.checkBoxFixTotals.TabIndex = 2;
            this.checkBoxFixTotals.Text = "Calculate quantity and mass totals with data on individuals";
            this.checkBoxFixTotals.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxFixTotals.UseVisualStyleBackColor = true;
            this.checkBoxFixTotals.CheckedChanged += new System.EventHandler(this.checkBoxFixTotals_CheckedChanged);
            // 
            // checkBoxAutoIncreaseBio
            // 
            this.checkBoxAutoIncreaseBio.AutoSize = true;
            this.checkBoxAutoIncreaseBio.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoIncreaseBio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxAutoIncreaseBio.Location = new System.Drawing.Point(68, 86);
            this.checkBoxAutoIncreaseBio.Name = "checkBoxAutoIncreaseBio";
            this.checkBoxAutoIncreaseBio.Size = new System.Drawing.Size(273, 17);
            this.checkBoxAutoIncreaseBio.TabIndex = 3;
            this.checkBoxAutoIncreaseBio.Text = "Increase total when individual values sum overlaps it";
            this.checkBoxAutoIncreaseBio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxAutoIncreaseBio.UseVisualStyleBackColor = true;
            this.checkBoxAutoIncreaseBio.CheckedChanged += new System.EventHandler(this.checkBoxAutoIncreaseBio_CheckedChanged);
            // 
            // tdClearRecent
            // 
            this.tdClearRecent.Buttons.Add(this.tdbRecentClear);
            this.tdClearRecent.Buttons.Add(this.tdbRecentCancel);
            this.tdClearRecent.MainInstruction = "Clear recent species?";
            // 
            // tdbRecentClear
            // 
            this.tdbRecentClear.Text = "Clear";
            // 
            // tdbRecentCancel
            // 
            this.tdbRecentCancel.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // SettingsControlLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.checkBoxAutoDecreaseBio);
            this.Controls.Add(this.checkBoxFixTotals);
            this.Controls.Add(this.checkBoxAutoIncreaseBio);
            this.Controls.Add(this.checkBoxAutoLog);
            this.Controls.Add(this.labelInputFish);
            this.Group = "Reader";
            this.Name = "SettingsControlLog";
            this.Section = "Definition log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxAutoLog;
        private System.Windows.Forms.Label labelInputFish;
        private System.Windows.Forms.CheckBox checkBoxAutoDecreaseBio;
        private System.Windows.Forms.CheckBox checkBoxFixTotals;
        private System.Windows.Forms.CheckBox checkBoxAutoIncreaseBio;
        protected TaskDialogs.TaskDialog tdClearRecent;
        private TaskDialogs.TaskDialogButton tdbRecentClear;
        private TaskDialogs.TaskDialogButton tdbRecentCancel;
    }
}
