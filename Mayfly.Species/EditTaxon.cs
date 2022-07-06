using System;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species
{
    public partial class EditTaxon : Form
    {
        public bool IsChanged = false;

        public TaxonomicIndex.TaxonRow TaxonRow;



        public EditTaxon(TaxonomicIndex.TaxonRow taxonRow)
        {
            InitializeComponent();

            textBoxName.AutoCompleteCustomSource.AddRange(((TaxonomicIndex)taxonRow.Table.DataSet).Genera);
            textBoxReference.AutoCompleteCustomSource.AddRange(((TaxonomicIndex)taxonRow.Table.DataSet).References);

            TaxonRow = taxonRow;

            textBoxName.Text = TaxonRow.Name;
            if (!TaxonRow.IsReferenceNull()) textBoxReference.Text = TaxonRow.Reference;
            if (!TaxonRow.IsLocalNull()) textBoxLocal.Text = TaxonRow.Local;
            if (!TaxonRow.IsDescriptionNull()) textBoxDescription.Text = TaxonRow.Description;
            taxonSelector.Data = (TaxonomicIndex)taxonRow.Table.DataSet;
            comboBoxRank.DataSource = taxonRow.IsHigher ? TaxonomicRank.HigherRanks : TaxonomicRank.AllRanks;
            comboBoxRank.SelectedIndexChanged += new System.EventHandler(this.comboBoxRank_SelectedIndexChanged);
            comboBoxRank.SelectedItem = taxonRow.TaxonomicRank;

            IsChanged = false;
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            TaxonRow.TaxonomicRank = (TaxonomicRank)comboBoxRank.SelectedItem;
            TaxonRow.Name = textBoxName.Text;

            if (!textBoxReference.Text.IsAcceptable()) TaxonRow.SetReferenceNull();
            else TaxonRow.Reference = textBoxReference.Text;

            if (textBoxLocal.Text.IsAcceptable()) { TaxonRow.Local = textBoxLocal.Text; }
            else { TaxonRow.SetLocalNull(); }
            
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
            buttonOK.Enabled = textBoxName.Text.IsAcceptable() &&
                (TaxonRow.IsHigher || taxonSelector.Taxon != null);
        }

        private void comboBoxRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            taxonSelector.DeepestRank =
                comboBoxRank.SelectedIndex > 0 ? (TaxonomicRank)comboBoxRank.Items[comboBoxRank.SelectedIndex - 1] : null;
        }

        private void taxonSelector_OnTreeLoaded(object sender, EventArgs e)
        {
            taxonSelector.Taxon = TaxonRow.IsTaxIDNull() ? null : TaxonRow.TaxonRowParent;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            this.ResetText(string.Format("{0} — {1}", textBoxName.Text, "Edit taxon"), false);
            valueChanged(sender, e);
        }
    }
}
