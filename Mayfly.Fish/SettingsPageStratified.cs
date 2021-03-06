using System.Windows.Forms;
using static Mayfly.Fish.UserSettings;
using Mayfly.Controls;


namespace Mayfly.Fish
{
    public partial class SettingsPageStratified : SettingsPage, ISettingsPage
    {
        public SettingsPageStratified() {
            InitializeComponent();
        }



        public void LoadSettings() {

            numericUpDownInterval.Value = (decimal)DefaultStratifiedInterval;
        }

        public void SaveSettings() {

            DefaultStratifiedInterval = (double)numericUpDownInterval.Value;
        }
    }
}
