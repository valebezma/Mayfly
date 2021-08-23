using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Charts
{
    public partial class FunctorProperties : Form
    {
        public event EventHandler ValueChanged;

        public Functor Functor { get; set; }

        public string FunctionName
        {
            get;
            internal set;
        }

        public bool AllowCursors
        {
            get { return checkBoxAllowCursors.Checked; }
            set { checkBoxAllowCursors.Checked = value; }
        }

        public Color TrendColor
        {
            get { return colorDialogTrend.Color; }
            set
            {
                colorDialogTrend.Color = value;
                panelTrendColor.BackColor = value;
            }
        }

        public int TrendWidth
        {
            get { return trackBarTrendWidth.Value; }
            set { trackBarTrendWidth.Value = value; }
        }



        public FunctorProperties(Functor functor)
        {
            InitializeComponent();
            Functor = functor;
            textBoxName.Text = functor.Name;
        }



        public void ResetTitle()
        {
            Text = string.Format(Resources.Interface.SeriesProperties, FunctionName);
        }



        private void valueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && ActiveControl == (Control)sender)
            {
                ValueChanged.Invoke(sender, new EventArgs());
            }

            Functor.Update();
        }

        private void panelTrendColor_Click(object sender, EventArgs e)
        {
            colorDialogTrend.ShowDialog(this);
            panelTrendColor.BackColor = colorDialogTrend.Color;
            valueChanged(ActiveControl, e);
        }

        private void Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
