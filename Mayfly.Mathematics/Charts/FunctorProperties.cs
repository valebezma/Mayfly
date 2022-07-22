using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mayfly.Mathematics.Charts
{
    public partial class FunctorProperties : Form
    {
        public event EventHandler ValueChanged;

        public Functor Functor { get; set; }

        public string FunctionName {
            get { return textBoxName.Text; }
            set {
                textBoxName.Text = value;
                ResetTitle();
            }
        }

        public bool AllowCursors {
            get { return checkBoxAllowCursors.Checked; }
            set { checkBoxAllowCursors.Checked = value; }
        }

        public Color TrendColor {
            get { return colorBoxTrend.Color; }
            set { colorBoxTrend.Color = value; }
        }

        public int TrendWidth {
            get { return trackBarTrendWidth.Value; }
            set { trackBarTrendWidth.Value = value; }
        }



        public FunctorProperties(Functor functor) {

            InitializeComponent();
            Functor = functor;

            textBoxName.Text = functor.Name;
        }



        public void ResetTitle() {
            Text = string.Format(Resources.Interface.SeriesProperties, FunctionName);
        }



        private void valueChanged(object sender, EventArgs e) {
            if (ValueChanged != null && ActiveControl == (Control)sender) {
                ValueChanged.Invoke(sender, new EventArgs());
            }

            Functor.Update();
        }

        private void Properties_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }
    }
}
