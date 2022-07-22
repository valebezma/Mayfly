using Mayfly.Extensions;
using System;
using System.Windows.Forms;
using Mayfly.Controls;
using System.Collections.Generic;

namespace Mayfly.Software
{
    public partial class Settings : Form
    {
        UserControl currentControl;



        public Settings(List<Type> set) {

            InitializeComponent();

            foreach (Type t in set) {

                currentControl = (UserControl)Activator.CreateInstance(t);
                ((ISettingsPage)currentControl).LoadSettings();
                currentControl.Dock = DockStyle.Fill;
                panelContent.Controls.Add(currentControl);
                currentControl.Hide();

                string group = ((SettingsPage)currentControl).Group;

                TreeNode[] tc = treeViewCatogories.Nodes.Find(group, false);
                TreeNode tn = tc.Length > 0 ? tc[0] : null;

                if (tn == null) {
                    tn = new TreeNode(group) { Name = group };
                    treeViewCatogories.Nodes.Add(tn);
                }

                string section = ((SettingsPage)currentControl).Section;

                tc = tn.Nodes.Find(section, false);
                TreeNode tn1 = tc.Length > 0 ? tc[0] : null;

                if (tn1 == null) {
                    tn1 = new TreeNode(section) {
                        Name = section,
                        Tag = currentControl
                    };
                    tn.Nodes.Add(tn1);
                }
            }

            treeViewCatogories.ExpandAll();
        }



        public void Show(string group, string section) {

            TreeNode[] tc = treeViewCatogories.Nodes.Find(group, false);
            TreeNode tn = tc.Length > 0 ? tc[0] : null;

            if (tn == null) {
                return;
            }

            tc = tn.Nodes.Find(section, false);
            TreeNode tn1 = tc.Length > 0 ? tc[0] : null;

            if (tn1 == null) {
                return;
            }

            treeViewCatogories.SelectedNode = tn1;
        }



        private void settings_Load(object sender, EventArgs e) {

            treeViewCatogories.Shine();
            treeViewCatogories.SelectedNode = treeViewCatogories.Nodes[0];
        }

        private void treeViewCatogories_AfterSelect(object sender, TreeViewEventArgs e) {

            if (currentControl != null) {

                currentControl.Hide();

                //((ISettingControl)currentControl).SaveSettings();
                //panelContent.Controls.Remove(currentControl);
                //currentControl.Dispose();
            }

            if (e.Node.Tag == null) {
                treeViewCatogories.SelectedNode = e.Node.Nodes[0];
                return;
            }

            currentControl = (UserControl)e.Node.Tag;
            currentControl.Show();

            //currentControl = (UserControl)Activator.CreateInstance((Type)e.Node.Tag);
            //((ISettingControl)currentControl).LoadSettings();
            //currentControl.Dock = DockStyle.Fill;
            //panelContent.Controls.Add(currentControl);
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e) {
            foreach (UserControl c in panelContent.Controls) {
                ((ISettingsPage)c).SaveSettings();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            buttonApply_Click(sender, e);
            Close();
        }
    }
}
