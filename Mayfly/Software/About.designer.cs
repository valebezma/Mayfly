namespace Mayfly.Software
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.labelCopyright = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPowered = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.picturePowered = new System.Windows.Forms.PictureBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFeedback = new System.Windows.Forms.Button();
            this.pictureAppIcon = new System.Windows.Forms.PictureBox();
            this.buttonLic = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.buttonUpdates = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePowered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCopyright
            // 
            resources.ApplyResources(this.labelCopyright, "labelCopyright");
            this.labelCopyright.Name = "labelCopyright";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.Controls.Add(this.labelPowered);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.picturePowered);
            this.panel1.Name = "panel1";
            // 
            // labelPowered
            // 
            resources.ApplyResources(this.labelPowered, "labelPowered");
            this.labelPowered.BackColor = System.Drawing.Color.Transparent;
            this.labelPowered.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelPowered.Name = "labelPowered";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // picturePowered
            // 
            resources.ApplyResources(this.picturePowered, "picturePowered");
            this.picturePowered.BackColor = System.Drawing.Color.Transparent;
            this.picturePowered.Name = "picturePowered";
            this.picturePowered.TabStop = false;
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.Name = "labelVersion";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // buttonFeedback
            // 
            resources.ApplyResources(this.buttonFeedback, "buttonFeedback");
            this.buttonFeedback.Name = "buttonFeedback";
            this.buttonFeedback.UseVisualStyleBackColor = true;
            this.buttonFeedback.Click += new System.EventHandler(this.buttonFeedback_Click);
            // 
            // pictureAppIcon
            // 
            resources.ApplyResources(this.pictureAppIcon, "pictureAppIcon");
            this.pictureAppIcon.Name = "pictureAppIcon";
            this.pictureAppIcon.TabStop = false;
            // 
            // buttonLic
            // 
            resources.ApplyResources(this.buttonLic, "buttonLic");
            this.buttonLic.Name = "buttonLic";
            this.buttonLic.UseVisualStyleBackColor = true;
            this.buttonLic.Click += new System.EventHandler(this.buttonLic_Click);
            // 
            // pictureLogo
            // 
            resources.ApplyResources(this.pictureLogo, "pictureLogo");
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.TabStop = false;
            // 
            // buttonUpdates
            // 
            resources.ApplyResources(this.buttonUpdates, "buttonUpdates");
            this.buttonUpdates.Name = "buttonUpdates";
            this.buttonUpdates.UseVisualStyleBackColor = true;
            this.buttonUpdates.Click += new System.EventHandler(this.buttonUpdates_Click);
            // 
            // About
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.pictureAppIcon);
            this.Controls.Add(this.buttonLic);
            this.Controls.Add(this.buttonUpdates);
            this.Controls.Add(this.buttonFeedback);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picturePowered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.PictureBox pictureAppIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox picturePowered;
        private System.Windows.Forms.Label labelPowered;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonFeedback;
        private System.Windows.Forms.Button buttonLic;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.Button buttonUpdates;
    }
}