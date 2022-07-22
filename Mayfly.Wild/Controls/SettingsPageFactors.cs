using Mayfly.Extensions;
using System.Collections.Generic;
using System.Windows.Forms;
using static Mayfly.Wild.UserSettings;
using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsPageFactors : SettingsPage, ISettingsPage
    {
        public SettingsPageFactors() {

            InitializeComponent();
            listView.Shine();
        }



        public void LoadSettings() {

            foreach (string item in AddtFactors) {
                ListViewItem li = new ListViewItem(item);
                listView.Items.Add(li);
            }
        }

        public void SaveSettings() {

            List<string> currvars = new List<string>();
            foreach (ListViewItem item in listView.CheckedItems)
                currvars.Add(item.Text);
            AddtFactors = currvars.ToArray();
        }
    }
}
