using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Controls
{
    public partial class SettingsPage : UserControl
    {
        [Localizable(true), DefaultValue("Common")]
        public string Group {
            get;
            set;
        }

        [Localizable(true), DefaultValue("Common")]
        public string Section {
            get;
            set;
        }

        public SettingsPage() {

            InitializeComponent();
        }
    }
}
