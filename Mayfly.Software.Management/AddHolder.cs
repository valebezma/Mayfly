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
//using Mayfly.Installer;
using System.Net;
using Mayfly.Software;
using Mayfly.Extensions;

namespace Mayfly.Management
{
    public partial class AddHolder : Form
    {
        Development Database;

        public Development.LicenseRow LicenseRow;

        public AddHolder(Development database)
        {
            InitializeComponent();

            Database = database;

            listViewConstraints.Shine();
            dateTimeExpire.MinDate = DateTime.Now;
            dateTimeExpire.Value = DateTime.Today.AddMonths(3);

            comboBoxProduct.Items.Clear();

            foreach (Development.ProductRow productRow in Database.Product)
            {
                comboBoxProduct.Items.Add(productRow);
            }

            foreach (Development.AccountRow accountRow in Database.Account)
            {
                comboBoxAccount.Items.Add(accountRow);
            }

            comboBoxProduct.SelectedIndex = 0;
        }

        private void condition_Changed(object sender, EventArgs e)
        {
            textBoxSn.Text = string.Empty;

            buttonGenerate.Enabled =
                !string.IsNullOrWhiteSpace(comboBoxAccount.Text) &&
                comboBoxProduct.SelectedIndex != -1;

        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Development.ProductRow productRow = (Development.ProductRow)comboBoxProduct.SelectedItem;

            listViewConstraints.Items.Clear();
            foreach (Development.FileRow fileRow in productRow.GetFileRows())
            {
                ListViewItem li = listViewConstraints.CreateItem(fileRow.ID.ToString(), fileRow.File);
                li.UpdateItem(new object[] { Constants.Null, productRow.GetAvailableFeatures(fileRow).Merge() });
            }

            condition_Changed(sender, e);
        }

        private void checkBoxExpiry_CheckedChanged(object sender, EventArgs e)
        {
            dateTimeExpire.Enabled = checkBoxExpiry.Checked;
            listViewConstraints.Enabled = !checkBoxExpiry.Checked;
        }

        private void listViewConstraints_ItemCheck(object sender, ItemCheckEventArgs e)
        {            
            ListViewItem li = listViewConstraints.Items[e.Index];
            
            Development.ProductRow selectedProductRow = (Development.ProductRow)comboBoxProduct.SelectedItem;
            Development.FileRow fileRow = Database.File.FindByID(li.GetID());

            if (e.CurrentValue != CheckState.Checked)
            {
                li.UpdateItem(new object[] { fileRow.GetLatestVersion().Major, 
                    selectedProductRow.GetAvailableFeatures(fileRow).Merge() });
            }

            if (e.NewValue == CheckState.Unchecked)
            {
                li.UpdateItem(new object[] { Constants.Null, 
                    selectedProductRow.GetAvailableFeatures(fileRow).Merge() });
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            textBoxSn.Text = Service.RandomString(textBoxSn.MaxLength);
            buttonGenerate.Enabled = false;
        }

        private void textBoxSn_TextChanged(object sender, EventArgs e)
        {
            buttonCreate.Enabled = !string.IsNullOrWhiteSpace(textBoxSn.Text);
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            LicenseRow = Database.License.NewLicenseRow();

            if (comboBoxAccount.SelectedItem == null)
            {
                Development.AccountRow accRow = Database.Account.NewAccountRow();
                accRow.Name = comboBoxAccount.Text;
                Database.Account.AddAccountRow(accRow);
                LicenseRow.AccountRow = accRow;
            }
            else
            {
                LicenseRow.AccountRow = (Development.AccountRow)comboBoxAccount.SelectedItem;
            }

            LicenseRow.ProductRow = (Development.ProductRow)comboBoxProduct.SelectedItem;

            if (checkBoxExpiry.Checked)
                LicenseRow.Expire = dateTimeExpire.Value;

            LicenseRow.Key = textBoxSn.Text.ToLower();
            LicenseRow.Acquired = DateTime.UtcNow;

            Database.License.AddLicenseRow(LicenseRow);

            if (listViewConstraints.Enabled)
                foreach (ListViewItem li in listViewConstraints.Items)
                {
                    if (li.Checked)
                    {
                        if (li.SubItems[0].Text != Constants.Null)
                        {
                            string version = li.SubItems[0].Text;

                            Database.Available.AddAvailableRow(LicenseRow,
                                Database.File.FindByID(li.GetID()),
                                version);
                        }

                        if (li.SubItems[1].Text != Constants.Null)
                        {
                            string[] features = li.SubItems[1].Text.Split(new char[] { ' ', ',' });

                            // TODO
                        }
                    }
                }

            Close();
        }
    }
}
