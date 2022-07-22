namespace Mayfly.Fish.Explorer
{
    internal class SettingsPagePrediction : Wild.Controls.SettingsPagePrediction
    {
        private System.Windows.Forms.CheckBox checkBoxSuggestAge;
        private System.Windows.Forms.CheckBox checkBoxSuggestMass;


        public SettingsPagePrediction() : base() {

            InitializeComponent();
        }

        private void InitializeComponent() {

            this.checkBoxSuggestAge = new System.Windows.Forms.CheckBox();
            this.checkBoxSuggestMass = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxSuggestAge
            // 
            this.checkBoxSuggestAge.AutoSize = true;
            this.checkBoxSuggestAge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSuggestAge.Location = new System.Drawing.Point(51, 86);
            this.checkBoxSuggestAge.Name = "checkBoxSuggestAge";
            this.checkBoxSuggestAge.Size = new System.Drawing.Size(115, 17);
            this.checkBoxSuggestAge.TabIndex = 20;
            this.checkBoxSuggestAge.Text = "Suggest age value";
            this.checkBoxSuggestAge.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuggestMass
            // 
            this.checkBoxSuggestMass.AutoSize = true;
            this.checkBoxSuggestMass.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSuggestMass.Location = new System.Drawing.Point(51, 63);
            this.checkBoxSuggestMass.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.checkBoxSuggestMass.Name = "checkBoxSuggestMass";
            this.checkBoxSuggestMass.Size = new System.Drawing.Size(121, 17);
            this.checkBoxSuggestMass.TabIndex = 19;
            this.checkBoxSuggestMass.Text = "Suggest mass value";
            this.checkBoxSuggestMass.UseVisualStyleBackColor = true;
            // 
            // SettingsControlPrediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.checkBoxSuggestAge);
            this.Controls.Add(this.checkBoxSuggestMass);
            this.Name = "SettingsControlPrediction";
            this.Controls.SetChildIndex(this.checkBoxSuggestMass, 0);
            this.Controls.SetChildIndex(this.checkBoxSuggestAge, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public new void LoadSettings() {

            base.LoadSettings();
            checkBoxSuggestAge.Checked = UserSettings.SuggestAge;
            checkBoxSuggestMass.Checked = UserSettings.SuggestMass;
        }

        private new void SaveSettings() {

            base.SaveSettings();
            UserSettings.SuggestAge = checkBoxSuggestAge.Checked;
            UserSettings.SuggestMass = checkBoxSuggestMass.Checked;
        }
    }
}
