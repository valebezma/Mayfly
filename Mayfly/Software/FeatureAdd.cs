using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Software
{
    public partial class FeatureAdd : Form
    {
        public License License;



        public FeatureAdd()
        {
            InitializeComponent();
        }

        //public FeatureAdd(License installedLicense)
        //    : this()
        //{
        //    License = installedLicense;

        //    Text = License.Feature;
        //    labelInstruction.Visible = false;
        //    maskedSn.ReadOnly = true;
        //    pictureBoxSn.Visible = false;


        //    maskedSn.Text = License.Serial;
        //    UpdateLicense();
        //    buttonAdd.Visible = buttonGrant.Visible = false;
        //}

        private void UpdateLicense()
        {
            textBoxFeature.Text = License.Feature;
            textBoxExpires.Text = License.Acquired > DateTime.UtcNow ? Resources.License.LicenseNotActivated : License.Expires.ToLongDateString();
        }

        private void ClearLicense()
        {
            textBoxFeature.Text = string.Empty;
            textBoxExpires.Text = string.Empty;
            maskedSn.Enabled = true;

            buttonAdd.Enabled = false;
        }



        private void maskedSn_TextChanged(object sender, EventArgs e)
        {
            pictureBoxSn.Image = null;

            if (!maskedSn.ReadOnly && maskedSn.Text.Length == 25)
            {
                Application.UseWaitCursor = true;
                backgroundSerial.RunWorkerAsync(maskedSn.Text.ToLowerInvariant());
            }
            else
            {
                ClearLicense();
            }
        }

        private void backgroundSerial_DoWork(object sender, DoWorkEventArgs e)
        {
            string serial = (string)e.Argument;
            e.Result = Licensing.VerifyLicense(serial);
        }

        private void backgroundSerial_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.UseWaitCursor = false;
            ClearLicense();

            if (e.Cancelled)
            {
                return;
            }

            if (e.Result == null)
            {
                pictureBoxSn.Image = Resources.Icons.NoneRed;
                Service.PlaySound(Resources.Sounds.Wrong);
                return;
            }

            License = (License)e.Result;

            foreach (License lic in Licensing.InstalledLicenses)
            {
                if (lic.Serial == License.Serial)
                {
                    Notification.ShowNotification(
                        Resources.License.LicenseWrong,
                        Resources.License.LicenseInstalledInstruction);
                    return;
                }
            }

            if (License.Licensee != UserSettings.Username)
            {
                pictureBoxSn.Image = Resources.Icons.NoneRed;
                Notification.ShowNotification(
                        Resources.License.LicenseWrong,
                        Resources.License.LicenseWrongOwnerInstruction);
                return;
            }

            if (License.Expires < DateTime.Today)
            {
                pictureBoxSn.Image = Resources.Icons.NoneRed;
                Notification.ShowNotification(
                        Resources.License.LicenseWrong,
                        Resources.License.LicenseExpiredInstruction);
                return;
            }

            pictureBoxSn.Image = Resources.Icons.Check;
            UpdateLicense();

            buttonAdd.Enabled = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (License == null) return;

            if (License.Install())
            {
                Log.Write(EventType.Maintenance, "Feature\"{0}\" installed with serial number {1}.", License.Feature, License.Serial);
                DialogResult = DialogResult.OK;
            }

            Close();
        }
    }
}
