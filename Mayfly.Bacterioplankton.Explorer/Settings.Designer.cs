namespace Mayfly.Bacterioplankton.Explorer
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageOther = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.labelFish = new System.Windows.Forms.Label();
            this.labelOther = new System.Windows.Forms.Label();
            this.buttonMath = new System.Windows.Forms.Button();
            this.buttonPlankton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabControlSettings.SuspendLayout();
            this.tabPageOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.Controls.Add(this.tabPageOther);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            // 
            // tabPageOther
            // 
            this.tabPageOther.Controls.Add(this.label1);
            this.tabPageOther.Controls.Add(this.labelFish);
            this.tabPageOther.Controls.Add(this.labelOther);
            this.tabPageOther.Controls.Add(this.buttonMath);
            this.tabPageOther.Controls.Add(this.buttonPlankton);
            resources.ApplyResources(this.tabPageOther, "tabPageOther");
            this.tabPageOther.Name = "tabPageOther";
            this.tabPageOther.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelFish
            // 
            resources.ApplyResources(this.labelFish, "labelFish");
            this.labelFish.Name = "labelFish";
            // 
            // labelOther
            // 
            resources.ApplyResources(this.labelOther, "labelOther");
            this.labelOther.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelOther.Name = "labelOther";
            // 
            // buttonMath
            // 
            resources.ApplyResources(this.buttonMath, "buttonMath");
            this.buttonMath.Name = "buttonMath";
            this.buttonMath.UseVisualStyleBackColor = true;
            this.buttonMath.Click += new System.EventHandler(this.buttonMath_Click);
            // 
            // buttonPlankton
            // 
            resources.ApplyResources(this.buttonPlankton, "buttonPlankton");
            this.buttonPlankton.Name = "buttonPlankton";
            this.buttonPlankton.UseVisualStyleBackColor = true;
            this.buttonPlankton.Click += new System.EventHandler(this.buttonPlankton_Click);
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
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControlSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageOther.ResumeLayout(false);
            this.tabPageOther.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabPage tabPageOther;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFish;
        private System.Windows.Forms.Label labelOther;
        private System.Windows.Forms.Button buttonMath;
        private System.Windows.Forms.Button buttonPlankton;
    }
}