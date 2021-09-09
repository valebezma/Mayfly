using Mayfly.Extensions;
using Mayfly.Geographics;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Mayfly.TaskDialogs;
using System.Net;

namespace Mayfly.Software
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            listViewLicenses.Shine();

            comboBoxCulture.Items.Add(CultureInfo.GetCultureInfo("en"));

            foreach (string dir in Directory.GetDirectories(Application.StartupPath))
            {
                try
                {
                    CultureInfo cult = CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(dir));
                    comboBoxCulture.Items.Add(cult);
                }
                catch { }
            }

            comboBoxCulture.SelectedItem = UserSettings.Language.Equals(CultureInfo.InvariantCulture) ?
                comboBoxCulture.Items[0] : UserSettings.Language;

            textBoxUsername.Text = UserSettings.Username;

            UpdateCredentials(UserSettings.Credential);

            comboBoxUpdatePolicy.Enabled =
                UserSettings.Username != Resources.Interface.UserUnknown;
            comboBoxUpdatePolicy.SelectedIndex = comboBoxUpdatePolicy.Enabled ?
                (int)UserSettings.UpdatePolicy : 0;

            checkBoxUseUnsafeConnection.Checked = UserSettings.UseUnsafeConnection;

            checkBoxShareDiagnostics.Checked = UserSettings.ShareDiagnostics;
        }


        public void OpenFeatures()
        {
            tabControlSettings.SelectedTab = tabPage1;
        }

        private void UpdateCredentials(NetworkCredential cred)
        {
            checkBoxCredentials.Checked = cred != null;

            if (cred == null)
            {
                pictureBoxLogin.Image = null;
                textBoxEmail.ReadOnly =
                    maskedPass.ReadOnly = false;
            }
            else
            {
                textBoxEmail.Text = cred.UserName;
                maskedPass.Text = cred.Password;
                pictureBoxLogin.Image = Pictogram.Check;
                textBoxEmail.ReadOnly =
                    maskedPass.ReadOnly = true;
                buttonLogin.Enabled = false;

                UpdateLicenses();
            }
        }

        private void UpdateLicenses()
        {
            listViewLicenses.Items.Clear();

            foreach (License lic in License.InstalledLicenses)
            {
                ListViewItem li = new ListViewItem(lic.Feature);
                li.Name = lic.Feature.ToString();
                li.SubItems.Add(((int)lic.ExpiresIn.TotalDays).ToCorrectString(Resources.Interface.ExpirationMask));
                listViewLicenses.Items.Add(li);
            }
        }


        private void buttonApply_Click(object sender, EventArgs e)
        {
            UserSettings.Language = comboBoxCulture.SelectedIndex == 0 ? CultureInfo.InvariantCulture : (CultureInfo)comboBoxCulture.SelectedItem;
            UserSettings.UpdatePolicy = (UpdatePolicy)comboBoxUpdatePolicy.SelectedIndex;
            UserSettings.UseUnsafeConnection = checkBoxUseUnsafeConnection.Checked;
            UserSettings.ShareDiagnostics = checkBoxShareDiagnostics.Checked;

            if (!checkBoxCredentials.Checked)
            {
                License.SendUninstall(UserSettings.Credential);
                UserSettings.Credential = null;
                foreach (License lic in License.InstalledLicenses)
                {
                    lic.Uninstall();
                }
            }
            Service.ResetUICulture();
            Log.Write(EventType.Maintenance, "Mayfly settings changed");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }



        private void buttonLogin_Click(object sender, EventArgs e)
        {
            NetworkCredential cred = new NetworkCredential(textBoxEmail.Text, maskedPass.Text);

            License[] lics = License.GetLicenses(cred);

            if (lics == null) // Licenses can not be received
            {
                pictureBoxLogin.Image = Pictogram.NoneRed;
                listViewLicenses.Items.Clear();
                return;
            }
            else // Licenses are received
            {
                if (lics.Length > 0 && lics[0].Licensee != UserSettings.Username) // Username  in Settings and at Server does not match
                {
                    TaskDialogButton tdbMismatch = taskDialogNameMismatch.ShowDialog(this);

                    if (tdbMismatch == tdbMismatchReplace)
                    {
                        UserSettings.Username = lics[0].Licensee;
                        textBoxUsername.Text = UserSettings.Username;
                    }
                    else if (tdbMismatch == tdbMismatchSupport)
                    {
                        Server.SendEmail(Server.GetEmail("feedback"),
                            string.Format(Resources.Interface.FeedbackSubject, EntryAssemblyInfo.Title, UserSettings.Username),
                            string.Format(Resources.Interface.FeedbackBody, UserSettings.Username, EntryAssemblyInfo.Title));
                        return;
                    }
                    else if (tdbMismatch == tdbMismatchCancel)
                    {
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
                UpdateLicenses();
            }
        }

        private void comboBoxUpdatePolicy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxCredentials_Click(object sender, EventArgs e)
        { }

        private void checkBoxCredentials_CheckedChanged(object sender, EventArgs e)
        {
            if (listViewLicenses.Items.Count > 0 && !checkBoxCredentials.Checked)
            {
                TaskDialogButton tdbLogout = taskDialogLogout.ShowDialog(this);
                if (tdbLogout == tdbSignoutCancel)
                {
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
                buttonLogin.Enabled =
                labelFeaturesInstruction.Enabled =
                listViewLicenses.Enabled = checkBoxCredentials.Checked;

            if (checkBoxCredentials.Checked)
            {
                textBoxEmail.ReadOnly =
                    maskedPass.ReadOnly = false;
            }
            else
            {
                textBoxEmail.Text =
                    maskedPass.Text = string.Empty;
                pictureBoxLogin.Image = null;
                listViewLicenses.Items.Clear();
            }
        }

        private void textBoxEmail_DoubleClick(object sender, EventArgs e)
        {
            textBoxEmail.ReadOnly = 
            maskedPass.ReadOnly = false;
            buttonLogin.Enabled = true;
        }
    }
}
