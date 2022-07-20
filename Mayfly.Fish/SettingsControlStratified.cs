using System.Windows.Forms;
using static Mayfly.Fish.UserSettings;


namespace Mayfly.Fish
{
    public partial class SettingsControlStratified : UserControl, ISettingControl
    {
        public SettingsControlStratified() {
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
