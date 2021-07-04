using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public partial class TaxonChange : Form
    {
        public string NewSpeciesName
        {
            set
            {
                textBox1.Text = value;
            }

            get
            {
                return textBox1.Text;
            }
        }

        public TaxonChange()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }
    }
}
