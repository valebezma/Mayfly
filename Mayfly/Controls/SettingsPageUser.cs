using Mayfly.TaskDialogs;
using System;
using System.Net;

namespace Mayfly.Controls
{
    public partial class SettingsPageUser : SettingsPage, ISettingsPage
    {
        public SettingsPageUser() {

            InitializeComponent();
        }



        public void LoadSettings() {

            textBoxUsername.Text = UserSettings.Username;

            UpdateCredentials(UserSettings.Credential);
        }

        public void SaveSettings() {

            if (!checkBoxCredentials.Checked) {
                License.SendUninstall(UserSettings.Credential);
                UserSettings.Credential = null;
                foreach (License lic in License.InstalledLicenses) {
                    lic.Uninstall();
                }
            }
        }

        private void UpdateCredentials(NetworkCredential cred) {

            checkBoxCredentials.Checked = cred != null;

            if (cred == null) {
                pictureBoxLogin.Image = null;
                textBoxEmail.ReadOnly =
                    maskedPass.ReadOnly = false;
            } else {
                textBoxEmail.Text = cred.UserName;
                maskedPass.Text = cred.Password;
                pictureBoxLogin.Image = Pictogram.Check;
                textBoxEmail.ReadOnly =
                    maskedPass.ReadOnly = true;
                buttonLogin.Enabled = false;
            }
        }



        private void buttonLogin_Click(object sender, EventArgs e) {

            NetworkCredential cred = new NetworkCredential(textBoxEmail.Text, maskedPass.Text);

            License[] lics = License.GetLicenses(cred);

            if (lics == null) // Licenses can not be received
            {
                pictureBoxLogin.Image = Pictogram.NoneRed;
                return;
            } else { // Licenses are received

                if (lics.Length > 0 && lics[0].Licensee != UserSettings.Username) // Username  in Settings and at Server does not match
                {
                    TaskDialogButton tdbMismatch = taskDialogNameMismatch.ShowDialog(this);

                    if (tdbMismatch == tdbMismatchReplace) {
                        UserSettings.Username = lics[0].Licensee;
                        textBoxUsername.Text = UserSettings.Username;
                    } else if (tdbMismatch == tdbMismatchSupport) {
                        Server.SendEmail(Server.GetEmail("feedback"),
                            string.Format(Resources.Interface.FeedbackSubject, EntryAssemblyInfo.Title, UserSettings.Username),
                            string.Format(Resources.Interface.FeedbackBody, UserSettings.Username, EntryAssemblyInfo.Title));
                        return;
                    } else if (tdbMismatch == tdbMismatchCancel) {
                        checkBoxCredentials.Checked = false;
                        return;
                    }
                }

                // Remember correct credentials
                UserSettings.Credential = cred;

                // Install licenses
                License.InstallLicenses(lics);

                // Update form
                pictureBoxLogin.Image = Pictogram.Check;
                textBoxEmail.ReadOnly = maskedPass.ReadOnly = true;
                buttonLogin.Enabled = false;
            }
        }


        private void checkBoxCredentials_Click(object sender, EventArgs e) { }

        private void checkBoxCredentials_CheckedChanged(object sender, EventArgs e) {

            if (License.InstalledLicenses.Length > 0 && !checkBoxCredentials.Checked) {

                TaskDialogButton tdbLogout = taskDialogLogout.ShowDialog(this);
                if (tdbLogout == tdbSignoutCancel) {
                    checkBoxCredentials.CheckedChanged -= checkBoxCredentials_CheckedChanged;
                    checkBoxCredentials.Checked = true;
                    checkBoxCredentials.CheckedChanged += checkBoxCredentials_CheckedChanged;
                    return;
                }
            }

            labelEmail.Enabled =
                textBoxEmail.Enabled =
                labelPass.Enabled =
                maskedPass.Enabled =
                buttonLogin.Enabled = checkBoxCredentials.Checked;

            if (checkBoxCredentials.Checked) {
                textBoxEmail.ReadOnly =
                    maskedPass.ReadOnly = false;
            } else {
                textBoxEmail.Text =
                    maskedPass.Text = string.Empty;
                pictureBoxLogin.Image = null;
            }
        }

        private void textBoxEmail_DoubleClick(object sender, EventArgs e) {
            textBoxEmail.ReadOnly =
            maskedPass.ReadOnly = false;
            buttonLogin.Enabled = true;
        }
    }
}
