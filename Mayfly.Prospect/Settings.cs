using Mayfly.Extensions;
using Mayfly.Geographics;
using System;
using System.Windows.Forms;
using System.IO;
using System.Resources;

using System.Globalization;
using Mayfly.Software;

namespace Mayfly.Prospect
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            DateTime last = File.GetCreationTime(UserSettingPaths.LocalFishCopyPath);
            labelUpdatedTip.ResetFormatted(last, (DateTime.Now - last).TotalDays);

            textBoxWaters.Text = Wild.UserSettings.WatersIndexPath;
            textBoxCards.Text = UserSettings.CardsPath;

            textBoxPlk.Text = Plankton.UserSettings.SpeciesIndexPath;
            textBoxBen.Text = Benthos.UserSettings.SpeciesIndexPath;
            textBoxFsh.Text = Fish.UserSettings.SpeciesIndexPath;

            numericUpDownUpdateFrequency.Value = UserSettings.UpdateFrequency;
        }



        private void SaveSettings()
        {
            Wild.UserSettings.WatersIndexPath = textBoxWaters.Text;

            Plankton.UserSettings.SpeciesIndexPath = textBoxPlk.Text;
            Benthos.UserSettings.SpeciesIndexPath = textBoxBen.Text;
            Fish.UserSettings.SpeciesIndexPath = textBoxFsh.Text;

            UserSettings.CardsPath = textBoxCards.Text;
            UserSettings.UpdateFrequency = (int)numericUpDownUpdateFrequency.Value;
        }



        private void buttonApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
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


        public void buttonBrowseWaters_Click(object sender, EventArgs e)
        {
            if (Waters.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            { 
                textBoxWaters.Text = Waters.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonOpenWaters_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(textBoxWaters.Text); 
        }


        private void buttonBrowseCards_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxCards.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Service.UpdateLocalData();
            DateTime last = File.GetCreationTime(UserSettingPaths.LocalFishCopyPath);
            labelUpdatedTip.ResetFormatted(last, (DateTime.Now - last).TotalDays);
        }


        private void buttonBrowsePlk_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            { 
                textBoxPlk.Text = Species.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonBrowseBen_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            { 
                textBoxBen.Text = Species.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonBrowseFsh_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            { 
                textBoxFsh.Text = Species.UserSettings.Interface.OpenDialog.FileName;
            }
        }

        private void buttonOpenPlk_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(textBoxPlk.Text); 
        }

        private void buttonOpenBen_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(textBoxBen.Text);
        }

        private void buttonOpenFsh_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(textBoxFsh.Text);
        }
    }
}
