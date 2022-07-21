using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlExplorer : SettingsControl, ISettingsControl
    {
        public SettingsControlExplorer() {

            InitializeComponent();

            comboBoxDiversity.DataSource = Service.GetDiversityIndices();
        }



        public void LoadSettings() {

            comboBoxDominance.SelectedIndex = UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)UserSettings.Diversity;
            comboBoxDominance.SelectedIndex = UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)UserSettings.Diversity;

            checkBoxKeepWizards.Checked = SettingsExplorer.KeepWizard;
            comboBoxReportCriticality.SelectedIndex = (int)SettingsExplorer.ReportCriticality;
            checkBoxConsistency.Checked = SettingsExplorer.CheckConsistency;
        }

        public void SaveSettings() {

            SettingsExplorer.KeepWizard = checkBoxKeepWizards.Checked;
            SettingsExplorer.CheckConsistency = checkBoxConsistency.Checked;
            SettingsExplorer.ReportCriticality = (ArtifactCriticality)comboBoxReportCriticality.SelectedIndex;

            UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            UserSettings.Dominance = comboBoxDominance.SelectedIndex;
            UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            UserSettings.Dominance = comboBoxDominance.SelectedIndex;
        }
    }
}
