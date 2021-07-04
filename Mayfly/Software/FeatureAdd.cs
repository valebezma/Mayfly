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
        public License.UserLicenseRow License;



        public FeatureAdd()
        {
            InitializeComponent();
        }

        public FeatureAdd(License.UserLicenseRow installedLicense)
            : this()
        {
            License = installedLicense;

            Text = License.Feature;
            labelInstruction.Visible = false;
            maskedSn.ReadOnly = true;
            pictureBoxSn.Visible = false;


            maskedSn.Text = License.Serial;
            UpdateLicense();
            buttonAdd.Visible = false;
        }

        private void UpdateLicense()
        {
            textBoxFeature.Text = License.Feature;
            textBoxLicensee.Text = License.Licensee;
            textBoxExpires.Text = License.Expires.ToLongDateString();
        }

        private void ClearLicense()
        {
            textBoxFeature.Text = string.Empty;
            textBoxLicensee.Text = string.Empty;
            textBoxExpires.Text = string.Empty;
            maskedSn.Enabled = true;

            buttonAdd.Enabled = false;
        }



        private void maskedSn_TextChanged(object sender, EventArgs e)
        {
            if (!maskedSn.ReadOnly && maskedSn.Text.Length == 25)
            {
                pictureBoxSn.Image = null;
                Application.UseWaitCursor = true;
                backgroundSerial.RunWorkerAsync(maskedSn.Text.ToLowerInvariant());
            }
            else if (maskedSn.Text.Length == 0)
            {
                pictureBoxSn.Image = null;
                ClearLicense();
            }
            else
            {
                pictureBoxSn.Image = Resources.Icons.NoneRed;
                ClearLicense();
            }
        }

        private void backgroundSerial_DoWork(object sender, DoWorkEventArgs e)
        {
            string serial = (string)e.Argument;

            throw new Exception("Activation server is offline");

            //License result = new Software.License();
            //result.UserLicense.AddUserLicenseRow("Безматерных Валентин", "Bios", new DateTime(2025, 12, 31), maskedSn.Text);
            //e.Result = result.UserLicense[0];
        }

        private void backgroundSerial_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                License = (License.UserLicenseRow)e.Result;
                buttonAdd.Enabled = false;

                if (e.Result == null)
                {
                    pictureBoxSn.Image = Resources.Icons.NoneRed;
                    ClearLicense();
                    Mayfly.Service.PlaySound(Resources.Sounds.Wrong);
                }
                else
                {
                    pictureBoxSn.Image = Resources.Icons.Check;
                    UpdateLicense();
                    buttonAdd.Enabled = true;
                }
            }

            Application.UseWaitCursor = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (License == null) return;

            UserSetting.SetValue(UserSettingPaths.KeyLicenses, 
                StringCipher.Encrypt(License.Serial, UserSettings.Username), 
                StringCipher.Encrypt(License.Table.DataSet.GetXml(), License.Serial));

            Log.Write(EventType.Maintenance, "Feature\"{0}\" installed with serial number {1}.", License.Feature, License.Serial);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
