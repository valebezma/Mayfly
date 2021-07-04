using Mayfly.Extensions;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Software
{
    public partial class Features : Form
    {
        public Features()
        {
            InitializeComponent();
            listViewLicenses.Shine();

            UpdateLicenses();
        }




        private void UpdateLicenses()
        {
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FeatureAdd add = new FeatureAdd();
            add.SetFriendlyDesktopLocation(buttonAdd, FormLocation.NextToHost);
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

        private void buttonGet_Click(object sender, EventArgs e)
        {

        }

        private void Features_Load(object sender, EventArgs e)
        {

        }
    }
}
