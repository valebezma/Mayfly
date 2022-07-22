using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsPageExplorer : SettingsPage, ISettingsPage
    {
        public SettingsPageExplorer() {

            InitializeComponent();

            comboBoxDiversity.DataSource = Service.GetDiversityIndices();
        }



        public void LoadSettings() {

            comboBoxDominance.SelectedIndex = UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)UserSettings.Diversity;
            comboBoxDominance.SelectedIndex = UserSettings.Dominance;
            comboBoxDiversity.SelectedIndex = (int)UserSettings.Diversity;

            checkBoxKeepWizards.Checked = ExplorerSettings.KeepWizard;
            comboBoxReportCriticality.SelectedIndex = (int)ExplorerSettings.ReportCriticality;
            checkBoxConsistency.Checked = ExplorerSettings.CheckConsistency;
        }

        public void SaveSettings() {

            ExplorerSettings.KeepWizard = checkBoxKeepWizards.Checked;
            ExplorerSettings.CheckConsistency = checkBoxConsistency.Checked;
            ExplorerSettings.ReportCriticality = (ArtifactCriticality)comboBoxReportCriticality.SelectedIndex;

            UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            UserSettings.Dominance = comboBoxDominance.SelectedIndex;
            UserSettings.Diversity = (DiversityIndex)comboBoxDiversity.SelectedIndex;
            UserSettings.Dominance = comboBoxDominance.SelectedIndex;
        }
    }
}
