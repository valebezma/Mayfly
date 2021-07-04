namespace Mayfly.Software
{
    partial class FeatureAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureAdd));
            this.labelInstruction = new System.Windows.Forms.Label();
            this.pictureBoxSn = new System.Windows.Forms.PictureBox();
            this.maskedSn = new System.Windows.Forms.MaskedTextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.backgroundSerial = new System.ComponentModel.BackgroundWorker();
            this.labelExpires = new System.Windows.Forms.Label();
            this.labelLicensee = new System.Windows.Forms.Label();
            this.textBoxLicensee = new System.Windows.Forms.TextBox();
            this.textBoxFeature = new System.Windows.Forms.TextBox();
            this.labelFeature = new System.Windows.Forms.Label();
            this.textBoxExpires = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSn)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInstruction
            // 
            resources.ApplyResources(this.labelInstruction, "labelInstruction");
            this.labelInstruction.Name = "labelInstruction";
            // 
            // pictureBoxSn
            // 
            resources.ApplyResources(this.pictureBoxSn, "pictureBoxSn");
            this.pictureBoxSn.Name = "pictureBoxSn";
            this.pictureBoxSn.TabStop = false;
            // 
            // maskedSn
            // 
            resources.ApplyResources(this.maskedSn, "maskedSn");
            this.maskedSn.Culture = new System.Globalization.CultureInfo("");
            this.maskedSn.Name = "maskedSn";
            this.maskedSn.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.maskedSn.TextChanged += new System.EventHandler(this.maskedSn_TextChanged);
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // backgroundSerial
            // 
            this.backgroundSerial.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundSerial_DoWork);
            this.backgroundSerial.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundSerial_RunWorkerCompleted);
            // 
            // labelExpires
            // 
            resources.ApplyResources(this.labelExpires, "labelExpires");
            this.labelExpires.Name = "labelExpires";
            // 
            // labelLicensee
            // 
            resources.ApplyResources(this.labelLicensee, "labelLicensee");
            this.labelLicensee.Name = "labelLicensee";
            // 
            // textBoxLicensee
            // 
            resources.ApplyResources(this.textBoxLicensee, "textBoxLicensee");
            this.textBoxLicensee.Name = "textBoxLicensee";
            this.textBoxLicensee.ReadOnly = true;
            // 
            // textBoxFeature
            // 
            resources.ApplyResources(this.textBoxFeature, "textBoxFeature");
            this.textBoxFeature.Name = "textBoxFeature";
            this.textBoxFeature.ReadOnly = true;
            // 
            // labelFeature
            // 
            resources.ApplyResources(this.labelFeature, "labelFeature");
            this.labelFeature.Name = "labelFeature";
            // 
            // textBoxExpires
            // 
            resources.ApplyResources(this.textBoxExpires, "textBoxExpires");
            this.textBoxExpires.Name = "textBoxExpires";
            this.textBoxExpires.ReadOnly = true;
            // 
            // FeatureAdd
            // 
            this.AcceptButton = this.buttonAdd;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.labelExpires);
            this.Controls.Add(this.labelFeature);
            this.Controls.Add(this.labelLicensee);
            this.Controls.Add(this.textBoxFeature);
            this.Controls.Add(this.textBoxExpires);
            this.Controls.Add(this.textBoxLicensee);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.pictureBoxSn);
            this.Controls.Add(this.maskedSn);
            this.Controls.Add(this.labelInstruction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FeatureAdd";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.PictureBox pictureBoxSn;
        private System.Windows.Forms.MaskedTextBox maskedSn;
        private System.Windows.Forms.Button buttonAdd;
        private System.ComponentModel.BackgroundWorker backgroundSerial;
        private System.Windows.Forms.Label labelExpires;
        private System.Windows.Forms.Label labelLicensee;
        private System.Windows.Forms.TextBox textBoxLicensee;
        private System.Windows.Forms.TextBox textBoxFeature;
        private System.Windows.Forms.Label labelFeature;
        private System.Windows.Forms.TextBox textBoxExpires;
    }
}