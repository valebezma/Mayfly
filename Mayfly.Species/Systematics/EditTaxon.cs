using System;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species.Systematics
{
    public partial class EditTaxon : Form
    {
        public SpeciesKey.TaxaRow TaxonRow;

        public bool IsChanged = false;

        public EditTaxon(SpeciesKey.TaxaRow taxonRow)
        {
            InitializeComponent();

            TaxonRow = taxonRow;

            textBoxTaxon.Text = TaxonRow.Taxon;
            if (!TaxonRow.IsNameNull()) textBoxName.Text = TaxonRow.Name;
            if (!TaxonRow.IsDescriptionNull()) textBoxDescription.Text = TaxonRow.Description;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            TaxonRow.Taxon = textBoxTaxon.Text;
            
            if (textBoxName.Text.IsAcceptable()) {
                TaxonRow.Name = textBoxName.Text;
            } else {
                TaxonRow.SetNameNull();
            }
            
            if (textBoxDescription.Text.IsAcceptable()) {
                TaxonRow.Description = textBoxDescription.Text;
            } else {
                TaxonRow.SetDescriptionNull();
            }

            Close();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            IsChanged = true;
            buttonOK.Enabled = textBoxTaxon.Text.IsAcceptable();
        }
    }
}
