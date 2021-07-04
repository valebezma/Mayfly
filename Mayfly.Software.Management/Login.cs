using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Software.Management
{
    public partial class Login : Form
    {
        public string DataBaseConnectionString {
            get {
                return string.Format(@"Server={0};user={1};password={2};database={3}",
                    textBoxServer.Text, textBoxLogin.Text, textBoxPass.Text, textBoxDatabase.Text); }
        }

        public Login()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
