using System;
using System.Windows.Forms;
using Mayfly;
using Mayfly.Species;
using Mayfly.Wild;
using System.ComponentModel;
using System.Resources;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Fish.Legal
{
    public partial class Settings : Form
    {
        public event EventHandler SettingsSaved;



        public Settings()
        {
            InitializeComponent();

            //openStamp.Filter = FileSystem.FilterFromExt(".png");

            LoadSettings();
        }



        private void LoadSettings()
        {
            checkBoxHandWrite.Checked = UserSettings.HandWrite;
            checkBoxUseStamp.Checked = UserSettings.UseStamp;
            checkBoxUseFaximile.Checked = UserSettings.UseFaximile;
            textBoxStamp.Text = UserSettings.Stamp;
            textBoxFaximile.Text = UserSettings.Faximile;
            checkBoxPreventOvercatch.Checked = UserSettings.PreventOvercatch;
            checkBoxRoundCatch.Checked = UserSettings.RoundCatch != 0;
            if (UserSettings.RoundCatch != 0)
            {
                domainUpDownRoundCatch.SelectedIndex = (int)Math.Log10(UserSettings.RoundCatch) - 1;
            }

            checkBoxBindCatch.Checked = UserSettings.BindCatch;
        }

        private void SaveSettings()
        {
            UserSettings.HandWrite = checkBoxHandWrite.Checked;
            UserSettings.UseStamp = checkBoxUseStamp.Checked && File.Exists(textBoxStamp.Text);

            if (UserSettings.UseStamp)
            {
                if (!textBoxStamp.Text.Contains(Wild.UserSettings.FieldDataFolder))
                {
                    string stamp = Path.Combine(Wild.UserSettings.FieldDataFolder, "stamp" + Path.GetExtension(textBoxStamp.Text));
                    File.Copy(textBoxStamp.Text, stamp);
                    textBoxStamp.Text = stamp;
                }

                UserSettings.Stamp = textBoxStamp.Text;
            }

            UserSettings.UseFaximile = checkBoxUseFaximile.Checked && File.Exists(textBoxFaximile.Text);

            if (UserSettings.UseFaximile)
            {
                if (!textBoxFaximile.Text.Contains(Wild.UserSettings.FieldDataFolder))
                {
                    string faximile = Path.Combine(Wild.UserSettings.FieldDataFolder, "faximile" + Path.GetExtension(textBoxFaximile.Text));
                    File.Copy(textBoxFaximile.Text, faximile);
                    textBoxFaximile.Text = faximile;
                }

                UserSettings.Faximile = textBoxFaximile.Text;
            }

            UserSettings.PreventOvercatch = checkBoxPreventOvercatch.Checked;

            UserSettings.RoundCatch = checkBoxRoundCatch.Checked ?
                (int)Math.Pow((double)10, (double)(domainUpDownRoundCatch.SelectedIndex + 1)) : 0;

            UserSettings.BindCatch = checkBoxBindCatch.Checked;

            if (SettingsSaved != null)
            {
                SettingsSaved.Invoke(this, new EventArgs());
            }
        }


        private void checkBoxUseStamp_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStamp.Enabled = buttonStamp.Enabled = checkBoxUseStamp.Checked;
        }

        private void buttonStamp_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfacePictures.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxStamp.Text = FileSystem.InterfacePictures.OpenDialog.FileName;
            }
        }

        private void checkBoxUseFaximile_CheckedChanged(object sender, EventArgs e)
        {
            textBoxFaximile.Enabled = buttonFaximile.Enabled = checkBoxUseFaximile.Checked;
        }

        private void buttonFaximile_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfacePictures.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFaximile.Text = FileSystem.InterfacePictures.OpenDialog.FileName;
            }
        }

        private void checkBoxRoundCatch_CheckedChanged(object sender, EventArgs e)
        {
            domainUpDownRoundCatch.Enabled = checkBoxRoundCatch.Checked;
        }



        private void buttonBasicSettings_Click(object sender, EventArgs e)
        {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSettings();

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
