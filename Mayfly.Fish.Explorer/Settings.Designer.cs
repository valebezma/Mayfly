namespace Mayfly.Fish.Explorer
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTreat = new System.Windows.Forms.TabPage();
            this.tabPagePrediction = new System.Windows.Forms.TabPage();
            this.checkBoxSuggestAge = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxSuggestMass = new System.Windows.Forms.CheckBox();
            this.tabPageOther = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelFish = new System.Windows.Forms.Label();
            this.buttonMath = new System.Windows.Forms.Button();
            this.buttonProductSettings = new System.Windows.Forms.Button();
            this.buttonFish = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonBasicSettings = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPagePrediction.SuspendLayout();
            this.tabPageOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageTreat);
            this.tabControl.Controls.Add(this.tabPagePrediction);
            this.tabControl.Controls.Add(this.tabPageOther);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            // 
            // tabPageTreat
            // 
            resources.ApplyResources(this.tabPageTreat, "tabPageTreat");
            this.tabPageTreat.Name = "tabPageTreat";
            this.tabPageTreat.UseVisualStyleBackColor = true;
            // 
            // tabPagePrediction
            // 
            this.tabPagePrediction.Controls.Add(this.checkBoxSuggestAge);
            this.tabPagePrediction.Controls.Add(this.label2);
            this.tabPagePrediction.Controls.Add(this.checkBoxSuggestMass);
            resources.ApplyResources(this.tabPagePrediction, "tabPagePrediction");
            this.tabPagePrediction.Name = "tabPagePrediction";
            this.tabPagePrediction.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuggestAge
            // 
            resources.ApplyResources(this.checkBoxSuggestAge, "checkBoxSuggestAge");
            this.checkBoxSuggestAge.Name = "checkBoxSuggestAge";
            this.checkBoxSuggestAge.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // checkBoxSuggestMass
            // 
            resources.ApplyResources(this.checkBoxSuggestMass, "checkBoxSuggestMass");
            this.checkBoxSuggestMass.Name = "checkBoxSuggestMass";
            this.checkBoxSuggestMass.UseVisualStyleBackColor = true;
            // 
            // tabPageOther
            // 
            this.tabPageOther.Controls.Add(this.label1);
            this.tabPageOther.Controls.Add(this.label6);
            this.tabPageOther.Controls.Add(this.labelFish);
            this.tabPageOther.Controls.Add(this.buttonMath);
            this.tabPageOther.Controls.Add(this.buttonProductSettings);
            this.tabPageOther.Controls.Add(this.buttonFish);
            resources.ApplyResources(this.tabPageOther, "tabPageOther");
            this.tabPageOther.Name = "tabPageOther";
            this.tabPageOther.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // labelFish
            // 
            resources.ApplyResources(this.labelFish, "labelFish");
            this.labelFish.Name = "labelFish";
            // 
            // buttonMath
            // 
            resources.ApplyResources(this.buttonMath, "buttonMath");
            this.buttonMath.Name = "buttonMath";
            this.buttonMath.UseVisualStyleBackColor = true;
            this.buttonMath.Click += new System.EventHandler(this.buttonMath_Click);
            // 
            // buttonProductSettings
            // 
            resources.ApplyResources(this.buttonProductSettings, "buttonProductSettings");
            this.buttonProductSettings.Name = "buttonProductSettings";
            this.buttonProductSettings.UseVisualStyleBackColor = true;
            this.buttonProductSettings.Click += new System.EventHandler(this.buttonBasicSettings_Click);
            // 
            // buttonFish
            // 
            resources.ApplyResources(this.buttonFish, "buttonFish");
            this.buttonFish.Name = "buttonFish";
            this.buttonFish.UseVisualStyleBackColor = true;
            this.buttonFish.Click += new System.EventHandler(this.buttonFish_Click);
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
            // buttonBasicSettings
            // 
            resources.ApplyResources(this.buttonBasicSettings, "buttonBasicSettings");
            this.buttonBasicSettings.Name = "buttonBasicSettings";
            this.buttonBasicSettings.UseVisualStyleBackColor = true;
            this.buttonBasicSettings.Click += new System.EventHandler(this.buttonBasicSettings_Click);
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonBasicSettings);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.tabControl.ResumeLayout(false);
            this.tabPagePrediction.ResumeLayout(false);
            this.tabPagePrediction.PerformLayout();
            this.tabPageOther.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageOther;
        private System.Windows.Forms.Button buttonMath;
        private System.Windows.Forms.Button buttonFish;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFish;
        private System.Windows.Forms.TabPage tabPageTreat;
        private System.Windows.Forms.TabPage tabPagePrediction;
        private System.Windows.Forms.CheckBox checkBoxSuggestAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxSuggestMass;
        protected System.Windows.Forms.Button buttonBasicSettings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonProductSettings;
    }
}