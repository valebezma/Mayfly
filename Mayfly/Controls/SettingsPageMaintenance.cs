namespace Mayfly.Controls
{
    public partial class SettingsPageMaintenance : SettingsPage, ISettingsPage
    {
        public SettingsPageMaintenance() {

            InitializeComponent();
        }



        public void LoadSettings() {

            comboBoxUpdatePolicy.Enabled =
                UserSettings.Username != Resources.Interface.UserUnknown;
            comboBoxUpdatePolicy.SelectedIndex = comboBoxUpdatePolicy.Enabled ?
                (int)UserSettings.UpdatePolicy : 0;

            checkBoxUseUnsafeConnection.Checked = UserSettings.UseUnsafeConnection;

            checkBoxShareDiagnostics.Checked = UserSettings.ShareDiagnostics;
        }

        public void SaveSettings() {

            UserSettings.UpdatePolicy = (UpdatePolicy)comboBoxUpdatePolicy.SelectedIndex;
            UserSettings.UseUnsafeConnection = checkBoxUseUnsafeConnection.Checked;

            UserSettings.ShareDiagnostics = checkBoxShareDiagnostics.Checked;
        }
    }
}
