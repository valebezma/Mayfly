using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Mayfly.Wild.Exchange
{
    public partial class PermissionGrant : Form
    {
        public PermissionGrant()
        {
            InitializeComponent();
            textBoxReference.Text = Mayfly.UserSettings.Username;
            dateTimeExpiration.MinDate = DateTime.Now;
            dateTimeExpiration.Value = DateTime.Today.AddMonths(2);
        }

        private void textBoxRecipient_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = 
                !string.IsNullOrWhiteSpace(textBoxRecipient.Text) &&
                !string.IsNullOrWhiteSpace(textBoxPass.Text) &&
                (textBoxRecipient.Text != textBoxReference.Text);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UserSettings.InterfacePermission.SaveDialog.FileName = textBoxRecipient.Text;

            if (UserSettings.InterfacePermission.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Permission permission = new Permission();
                permission.Grant.AddGrantRow(textBoxReference.Text,
                    textBoxRecipient.Text, dateTimeExpiration.Value);
                string data = permission.GetXml();
                
                string encryptedData = StringCipher.Encrypt(data,
                    textBoxPass.Text);
                File.WriteAllText(UserSettings.InterfacePermission.SaveDialog.FileName, encryptedData);

                Close();
            }
        }
    }
}
