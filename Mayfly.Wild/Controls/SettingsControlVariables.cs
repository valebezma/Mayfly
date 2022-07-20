using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Mayfly.Wild.SettingsReader;
using System;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlVariables : UserControl, ISettingControl
    {
        public SettingsControlVariables() {

            InitializeComponent();
            listView.Shine();
        }

        public void LoadSettings() {

            foreach (string item in AddtVariables) {
                ListViewItem li = new ListViewItem(item);
                listView.Items.Add(li);
            }

            foreach (ListViewItem item in listView.Items) {
                if (Array.IndexOf(CurrentVariables, item.Text) > -1)
                    item.Checked = true;
            }
        }

        public void SaveSettings() {

            List<string> addvars = new List<string>();
            foreach (ListViewItem item in listView.Items)
                addvars.Add(item.Text);
            AddtVariables = addvars.ToArray();
        }

        private void buttonRemoveVar_Click(object sender, EventArgs e) {
            foreach (ListViewItem li in listView.SelectedItems) {
                listView.Items.Remove(li);
            }
        }

        private void buttonNewVar_Click(object sender, EventArgs e) {
            ListViewItem newitem = new ListViewItem();
            listView.Items.Add(newitem);
            newitem.BeginEdit();
        }

        private void listViewAddVars_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            if (!e.Label.IsAcceptable()) listView.Items[e.Item].Remove();
        }

    }
}
