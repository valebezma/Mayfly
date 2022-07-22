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

        [Browsable(true), DefaultValue(typeof(Color), "CornflowerBlue")]
        public Color Color {

            get { return color; }

            set {
                color = value;
                dialog.Color = value;
                this.BackColor = value;
            }
        }

        public ColorBox() {
            InitializeComponent();
        }

        private void ColorPicker_Click(object sender, EventArgs e) {

            if (dialog.ShowDialog(this) == DialogResult.OK) {

                color = 
                    dialog.Color = 
                    dialog.Color;
            }
        }
    }
}
