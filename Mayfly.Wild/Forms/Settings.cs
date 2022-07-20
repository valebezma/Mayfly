using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static Mayfly.UserSettings;
using static Mayfly.Wild.SettingsReader;
using static Mayfly.Wild.UserSettings;

namespace Mayfly.Wild
{
    public partial class Settings : Form
    {
        protected EventHandler saved;

        [Category("Mayfly Events"), Browsable(true)]
        public event EventHandler OnSaved {
            add { saved += value; }
            remove { saved -= value; }
        }



        public Settings() {

            InitializeComponent();
        }


        protected void Initiate() {
        }



        private void buttonApply_Click(object sender, EventArgs e) {

            if (saved != null) saved.Invoke(this, EventArgs.Empty);

            Log.Write(EventType.Maintenance, "User settings changed");
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            buttonApply_Click(sender, e);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }



        private void buttonBasicSettings_Click(object sender, EventArgs e) {
            Software.Settings settings = new Software.Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            settings.ShowDialog();
        }
    }
}
