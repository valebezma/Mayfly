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
    public partial class ColorBox : UserControl
    {
        private Color color;

        public event EventHandler ValueChanged;

        [Browsable(true), DefaultValue(typeof(Color), "CornflowerBlue")]
        public Color Color {

            get { return color; }

            set {

                color = value;
                dialog.Color = value;
                BackColor = Enabled ? color : Color.Transparent;
            }
        }

        public ColorBox() {

            InitializeComponent();
        }

        private void ColorPicker_Click(object sender, EventArgs e) {

            if (dialog.ShowDialog(this) == DialogResult.OK) {

                if (!color.Equals(dialog.Color)) {

                color =
                    BackColor = 
                    dialog.Color;

                if (ValueChanged != null) {
                        ValueChanged.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void ColorBox_EnabledChanged(object sender, EventArgs e) {

            BackColor = Enabled ? color : Color.Transparent;
        }
    }
}
