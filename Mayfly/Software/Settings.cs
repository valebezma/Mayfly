using Mayfly.Extensions;
using Mayfly.Geographics;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Mayfly.Software
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            //comboBoxCulture.Items.Add("System UI language");
            comboBoxCulture.Items.Add(CultureInfo.GetCultureInfo("en"));

            foreach (string dir in Directory.GetDirectories(Application.StartupPath))
            {
                try {
                    CultureInfo cult = CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(dir));
                    comboBoxCulture.Items.Add(cult); }
                catch { }
            }

            comboBoxCulture.SelectedItem = UserSettings.Language.Equals(CultureInfo.InvariantCulture) ?
                comboBoxCulture.Items[0] : UserSettings.Language;



            textBoxUsername.Text = UserSettings.Username;
            listViewLicenses.Shine();
            bool hasexp = false;
            List<string> loadedfeatures = new List<string>();
            foreach (License.UserLicenseRow licRow in Licensing.InstalledLicenses.UserLicense.Select(null, "Expires Desc")) {
                if (loadedfeatures.Contains(licRow.Feature) && !licRow.IsValid) continue;
                ListViewItem li = new ListViewItem(licRow.Feature);
                li.Name = licRow.ID.ToString();
                li.SubItems.Add(licRow.Expires.ToLongDateString());
                listViewLicenses.Items.Add(li);
                li.Group = licRow.IsValid ?
                    listViewLicenses.GetGroup("groupValid") :
                    listViewLicenses.GetGroup("groupExpired");
                hasexp |= !licRow.IsValid;
                loadedfeatures.Add(licRow.Feature);
            }
            listViewLicenses.ShowGroups = hasexp;



            comboBoxUpdatePolicy.Enabled = 
                UserSettings.Username != Resources.Interface.UserUnknown;            
            comboBoxUpdatePolicy.SelectedIndex = comboBoxUpdatePolicy.Enabled ? 
                (int)UserSettings.UpdatePolicy : 0;

            checkBoxUseUnsafeConnection.Checked = UserSettings.UseUnsafeConnection;


            checkBoxLog.Checked = UserSettings.Log;
            checkBoxKeepLog.Checked = UserSettings.LogSpan > 0;
            comboBoxKeepLog.SelectedIndex = (UserSettings.LogSpan == 1) ? 0 :
                (UserSettings.LogSpan == 7) ? 1 :
                (UserSettings.LogSpan == 30) ? 2 :
                (UserSettings.LogSpan == 365) ? 3 : -1;
            checkBoxLogSend.Checked = UserSettings.LogSend;

            if (checkBoxLog.Checked)
            {
                DirectoryInfo di = new DirectoryInfo(Log.FolderPath);
                if (di.Exists)
                {
                    long size = di.GetSize();
                    buttonClearLog.Enabled = size > 0;
                    labelLogSize.ResetFormatted(Mayfly.Service.GetFriendlyBytes(size));
                }
                else
                {
                    labelLogSize.Text = string.Empty;
                }

                buttonOpenLog.Enabled = File.Exists(Log.CurrentFile);
            }
            else
            {
                labelLogSize.Text = string.Empty;
                buttonClearLog.Enabled = false;
                buttonOpenLog.Enabled = false;
            }
        }


        public void OpenFeatures()
        {
            tabControlSettings.SelectedTab = tabPage1;
        }


        private void buttonApply_Click(object sender, EventArgs e)
        {
            UserSettings.Language = comboBoxCulture.SelectedIndex == 0 ? CultureInfo.InvariantCulture : (CultureInfo)comboBoxCulture.SelectedItem;

            UserSettings.UpdatePolicy = (UpdatePolicy)comboBoxUpdatePolicy.SelectedIndex;

            UserSettings.Log = checkBoxLog.Checked;
            UserSettings.LogSpan = (!checkBoxKeepLog.Checked || comboBoxKeepLog.SelectedIndex == -1) ? 0 :
                comboBoxKeepLog.SelectedIndex == 0 ? 1 :
                comboBoxKeepLog.SelectedIndex == 1 ? 7 :
                comboBoxKeepLog.SelectedIndex == 2 ? 30 :
                comboBoxKeepLog.SelectedIndex == 3 ? 365 : 0;
            UserSettings.LogSend = checkBoxLogSend.Checked;

            UserSettings.UseUnsafeConnection = checkBoxUseUnsafeConnection.Checked;

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



        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            FeatureAdd add = new FeatureAdd();
            add.SetFriendlyDesktopLocation(buttonUnlock, FormLocation.NextToHost);
            if (add.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem(add.License.Feature);
                li.SubItems.Add(add.License.Expires.ToShortDateString());
                listViewLicenses.Items.Add(li);
            }
        }

        private void listViewLicenses_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewLicenses.SelectedItems)
            {
                FeatureAdd add = new FeatureAdd(Licensing.InstalledLicenses.UserLicense.FindByFeature(li.Text)[0]);
                add.SetFriendlyDesktopLocation(li);
                add.ShowDialog(this);
            }
        }


        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            Directory.Delete(Log.FolderPath, true);
            labelLogSize.Text = string.Empty;
            buttonClearLog.Enabled = false;
            buttonOpenLog.Enabled = false;
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    FileSystem.RunFile(Log.FolderPath);
                }
                else
                {
                    FileSystem.RunFile(Log.FolderPath);
                }
            }
            else
            {
                ActionLogs actionsLog = new ActionLogs();
                actionsLog.SetFriendlyDesktopLocation(buttonOpenLog, FormLocation.NextToHost);
                actionsLog.ShowDialog();
            }
        }

        private void checkBoxKeepLog_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxKeepLog.Enabled =
            checkBoxLogSend.Enabled = 
            buttonClearLog.Enabled = checkBoxKeepLog.Checked;
        }

        private void checkBoxLog_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxKeepLog.Enabled = checkBoxLog.Checked;
        }
    }
}
