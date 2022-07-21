namespace Mayfly.Controls
{
    partial class SettingsControlMaintenance
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
            this.checkBoxUseUnsafeConnection = new System.Windows.Forms.CheckBox();
            this.labelUpdates = new System.Windows.Forms.Label();
            this.labelUpdatePolicy = new System.Windows.Forms.Label();
            this.comboBoxUpdatePolicy = new System.Windows.Forms.ComboBox();
            this.labelDiagnosics = new System.Windows.Forms.Label();
            this.checkBoxShareDiagnostics = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxUseUnsafeConnection
            // 
            this.checkBoxUseUnsafeConnection.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxUseUnsafeConnection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxUseUnsafeConnection.Location = new System.Drawing.Point(154, 64);
            this.checkBoxUseUnsafeConnection.Name = "checkBoxUseUnsafeConnection";
            this.checkBoxUseUnsafeConnection.Size = new System.Drawing.Size(181, 17);
            this.checkBoxUseUnsafeConnection.TabIndex = 3;
            this.checkBoxUseUnsafeConnection.Text = "Use unsafe connection";
            this.checkBoxUseUnsafeConnection.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxUseUnsafeConnection.UseVisualStyleBackColor = true;
            // 
            // labelUpdates
            // 
            this.labelUpdates.AutoSize = true;
            this.labelUpdates.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelUpdates.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelUpdates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelUpdates.Location = new System.Drawing.Point(28, 0);
            this.labelUpdates.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelUpdates.Name = "labelUpdates";
            this.labelUpdates.Size = new System.Drawing.Size(50, 15);
            this.labelUpdates.TabIndex = 0;
            this.labelUpdates.Text = "Updates";
            // 
            // labelUpdatePolicy
            // 
            this.labelUpdatePolicy.AutoSize = true;
            this.labelUpdatePolicy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelUpdatePolicy.Location = new System.Drawing.Point(45, 40);
            this.labelUpdatePolicy.Name = "labelUpdatePolicy";
            this.labelUpdatePolicy.Size = new System.Drawing.Size(72, 13);
            this.labelUpdatePolicy.TabIndex = 1;
            this.labelUpdatePolicy.Text = "Update policy";
            // 
            // comboBoxUpdatePolicy
            // 
            this.comboBoxUpdatePolicy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxUpdatePolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUpdatePolicy.FormattingEnabled = true;
            this.comboBoxUpdatePolicy.Items.AddRange(new object[] {
            "Do not check updates",
            "Check updates and notice me",
            "Run updates if released"});
            this.comboBoxUpdatePolicy.Location = new System.Drawing.Point(154, 37);
            this.comboBoxUpdatePolicy.Name = "comboBoxUpdatePolicy";
            this.comboBoxUpdatePolicy.Size = new System.Drawing.Size(223, 21);
            this.comboBoxUpdatePolicy.TabIndex = 2;
            // 
            // labelDiagnosics
            // 
            this.labelDiagnosics.AutoSize = true;
            this.labelDiagnosics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDiagnosics.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelDiagnosics.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDiagnosics.Location = new System.Drawing.Point(28, 109);
            this.labelDiagnosics.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelDiagnosics.Name = "labelDiagnosics";
            this.labelDiagnosics.Size = new System.Drawing.Size(129, 15);
            this.labelDiagnosics.TabIndex = 4;
            this.labelDiagnosics.Text = "Diagnostic Information";
            // 
            // checkBoxShareDiagnostics
            // 
            this.checkBoxShareDiagnostics.AutoSize = true;
            this.checkBoxShareDiagnostics.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxShareDiagnostics.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShareDiagnostics.Location = new System.Drawing.Point(48, 152);
            this.checkBoxShareDiagnostics.Name = "checkBoxShareDiagnostics";
            this.checkBoxShareDiagnostics.Size = new System.Drawing.Size(134, 17);
            this.checkBoxShareDiagnostics.TabIndex = 5;
            this.checkBoxShareDiagnostics.Text = "Share diagnostics data";
            this.checkBoxShareDiagnostics.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxShareDiagnostics.UseVisualStyleBackColor = true;
            // 
            // SettingsControlMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDiagnosics);
            this.Controls.Add(this.checkBoxShareDiagnostics);
            this.Controls.Add(this.checkBoxUseUnsafeConnection);
            this.Controls.Add(this.labelUpdates);
            this.Controls.Add(this.labelUpdatePolicy);
            this.Controls.Add(this.comboBoxUpdatePolicy);
            this.Group = "Basic";
            this.Name = "SettingsControlMaintenance";
            this.Section = "Maintenance";
            this.Size = new System.Drawing.Size(400, 189);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxUseUnsafeConnection;
        private System.Windows.Forms.Label labelUpdates;
        private System.Windows.Forms.Label labelUpdatePolicy;
        private System.Windows.Forms.ComboBox comboBoxUpdatePolicy;
        private System.Windows.Forms.Label labelDiagnosics;
        private System.Windows.Forms.CheckBox checkBoxShareDiagnostics;
    }
}
