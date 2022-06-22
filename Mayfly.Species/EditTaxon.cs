using System;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species
{
    public partial class EditTaxon : Form
    {
        public bool IsChanged = false;

        public SpeciesKey.TaxonRow TaxonRow;



        public EditTaxon(SpeciesKey.TaxonRow taxonRow)
        {
            InitializeComponent();

            comboBoxRank.DataSource = TaxonomicRank.SuperGenericRanks;
            taxonSelector.Data = (SpeciesKey)taxonRow.Table.DataSet;

            TaxonRow = taxonRow;

            comboBoxRank.SelectedValue = taxonRow.Rank;
            textBoxTaxon.Text = TaxonRow.Taxon;
            if (!TaxonRow.IsNameNull()) textBoxName.Text = TaxonRow.Name;
            if (!TaxonRow.IsDescriptionNull()) textBoxDescription.Text = TaxonRow.Description;
            taxonSelector.Taxon = TaxonRow.IsTaxIDNull() ? null : TaxonRow.TaxonRowParent;
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            TaxonRow.Rank = (int)comboBoxRank.SelectedValue;
            TaxonRow.Taxon = textBoxTaxon.Text;
            
            if (textBoxName.Text.IsAcceptable()) { TaxonRow.Name = textBoxName.Text; }
            else { TaxonRow.SetNameNull(); }
            
            if (textBoxDescription.Text.IsAcceptable()) { TaxonRow.Description = textBoxDescription.Text; }
            else { TaxonRow.SetDescriptionNull(); }

            if (taxonSelector.Taxon == null) TaxonRow.SetTaxIDNull();
            else TaxonRow.TaxID = taxonSelector.Taxon.ID;

            Close();
        }

        private void taxonSelector_BeforeTaxonSelected(object sender, TaxonEventArgs e)
        {
            if (e.TaxonRow != null)
            {
                if (e.TaxonRow.Rank >= (int)comboBoxRank.SelectedValue)
                {
                    taxonSelector.SelectionAllowed = false;
                    ToolTip toolTip = new ToolTip();
                    toolTip.ToolTipTitle = "Wrong selection";
                    string instruction = String.Format("{0} has lower or equal rank", e.TaxonRow.FullName);
                    toolTip.Show(instruction, taxonSelector, taxonSelector.Width / 2, taxonSelector.Height, 1500);
                    Service.PlaySound(Mayfly.Resources.Sounds.Wrong);
                }
            }
        }

        private void valueChanged(object sender, EventArgs e)
        {
            IsChanged = true;
            buttonOK.Enabled = textBoxTaxon.Text.IsAcceptable();
        }
    }
}
