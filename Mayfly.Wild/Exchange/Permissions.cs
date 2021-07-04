using Mayfly.Extensions;
using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Mayfly.Wild.Exchange
{
    public partial class Permissions : Form
    {
        public Permissions()
        {
            InitializeComponent();
            listViewPermissions.Shine();

            UpdatePermissions();
        }

        public Permissions(string filename): this()
        {
            InstallPermission(filename);
        }



        private static void InstallPermission(string filename, string password)
        {
            Permission.GrantRow given = Permission.GetPermission(filename, password);

            string name = Path.GetFileNameWithoutExtension(filename);
           
            UserSetting.SetValue(Mayfly.UserSettingPaths.KeyGeneral, new string[] { "Permissions", name },
                "Donor", StringCipher.Encrypt(given.Donor, name));
            UserSetting.SetValue(Mayfly.UserSettingPaths.KeyGeneral, new string[] { "Permissions", name },
                "Expire", StringCipher.Encrypt(given.Expire.ToString("s", CultureInfo.InvariantCulture), name));
        }

        private void InstallPermission(string filename)
        {
            if (inputDialogPassword.ShowDialog() == DialogResult.OK)
            {
                string failure = string.Empty;

                try
                {
                    Permission.GrantRow given = Permission.GetPermission(filename, inputDialogPassword.Input);

                    if (given.Donor.ToLower() == Mayfly.UserSettings.Username.ToLower())
                    {
                        failure = Resources.Permissions.PermGrantedByU;
                    }
                    else if (given.Recipient.ToLower() != Mayfly.UserSettings.Username.ToLower())
                    {
                        failure = Resources.Permissions.PermNot4U;
                    }
                    else if (given.Expire <= DateTime.Now)
                    {
                        failure = Resources.Permissions.PermExpired;
                    }
                    else
                    {
                        InstallPermission(filename, inputDialogPassword.Input);
                        UpdatePermissions();
                    }
                }
                catch //(Exception e)
                {
                    failure = Resources.Permissions.PermIncorrectPass;
                }

                inputDialogPassword.Input = string.Empty;

                if (failure != string.Empty)
                {
                    taskDialogPermitError.Content = string.Format(
                        new ResourceManager(typeof(Permissions)).GetString(
                        "taskDialogPermitError.Content"), failure);
                    taskDialogPermitError.ShowDialog(this);
                }
            }
        }

        private void UpdatePermissions()
        {
            UserSettings.installedPermissions = null;
            listViewPermissions.Items.Clear();

            foreach (Permission.GrantRow grantRow in UserSettings.InstalledPermissions.Grant)
            {
                ListViewItem li = new ListViewItem(grantRow.Donor);
                li.SubItems.Add(grantRow.Expire.ToLongDateString());
                listViewPermissions.Items.Add(li);

                li.Group = listViewPermissions.Groups[grantRow.IsExpired ? "groupExpired" : "groupActive"];
            }
        }



        private void buttonInstall_Click(object sender, EventArgs e)
        {
            if (UserSettings.InterfacePermission.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                InstallPermission(UserSettings.InterfacePermission.OpenDialog.FileName);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGrant_Click(object sender, EventArgs e)
        {
            PermissionGrant grant = new PermissionGrant();
            grant.ShowDialog(this);
        }

        private void listViewPermissions_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (FileSystem.MaskedNames((string[])e.Data.GetData(DataFormats.FileDrop),
                    UserSettings.InterfacePermission.Extension).Length > 0)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void listViewPermissions_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = FileSystem.MaskedNames((string[])e.Data.GetData(DataFormats.FileDrop),
                    UserSettings.InterfacePermission.Extension);

                if (fileNames.Length > 0)
                {
                    e.Effect = DragDropEffects.None;

                    foreach (string filename in fileNames)
                    {
                        InstallPermission(filename);
                    }
                }
            }
        }
    }
}
