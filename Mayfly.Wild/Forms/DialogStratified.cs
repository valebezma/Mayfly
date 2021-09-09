using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Wild.Controls;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class StratifiedSampleProperties : Form
    {
        StratifiedSample Sample { get; set; }

        public StratifiedSampleProperties(StratifiedSample sample)
        {
            InitializeComponent();
            Sample = sample;
            UpdateValues();
        }

        public void UpdateValues()
        {
            numericUpDownStart.Value = (decimal)Sample.Start;
            numericUpDownEnd.Value = (decimal)Sample.End;
            domainUpDownInterval.Text = Sample.Interval.ToString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Sample.Interval = Convert.ToDouble(domainUpDownInterval.Text);
            Sample.Reset(Convert.ToDouble(numericUpDownStart.Value),
                Convert.ToDouble(numericUpDownEnd.Value));

            DialogResult = DialogResult.OK;
            Close();
        }

        private void numeric_ValueChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = 
                (numericUpDownStart.Value <= numericUpDownEnd.Value &&
                domainUpDownInterval.Text != null);
        }

        private void domainUpDownInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            domainUpDownInterval.HandleInput(e, typeof(double));
        }

        private void domainUpDownInterval_SelectedItemChanged(object sender, EventArgs e)
        {
            numericUpDownEnd.Increment = numericUpDownStart.Increment =
                Convert.ToDecimal(domainUpDownInterval.Text);
            numeric_ValueChanged(sender, e);
        }

        private void StratifiedSampleProperties_Load(object sender, EventArgs e)
        {        }
    }
}
