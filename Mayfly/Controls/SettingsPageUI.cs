using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace Mayfly.Controls
{
    public partial class SettingsPageUI : SettingsPage, ISettingsPage
    {
        public SettingsPageUI() {

            InitializeComponent();

            comboBoxCulture.Items.Add(CultureInfo.GetCultureInfo("en"));

            foreach (string dir in Directory.GetDirectories(Application.StartupPath)) {
                try {
                    CultureInfo cult = CultureInfo.GetCultureInfo(Path.GetFileNameWithoutExtension(dir));
                    comboBoxCulture.Items.Add(cult);
                } catch { }
            }
        }



        public void LoadSettings() {

            comboBoxCulture.SelectedItem = UI.Language.Equals(CultureInfo.InvariantCulture) ?
                comboBoxCulture.Items[0] : UI.Language;
        }

        public void SaveSettings() {

            UI.Language = comboBoxCulture.SelectedIndex == 0 ? CultureInfo.InvariantCulture : (CultureInfo)comboBoxCulture.SelectedItem;
            Service.ResetUICulture();
        }
    }
}
