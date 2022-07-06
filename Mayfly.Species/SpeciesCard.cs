using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species
{
    public partial class SpeciesCard : Form
    {
        public TaxonomicIndex.TaxonRow SpeciesRow;

        public SpeciesCard(TaxonomicIndex.TaxonRow speciesRow)
        {
            InitializeComponent();

            SpeciesRow = speciesRow;

            this.Text = speciesRow.FullName;

            labelSpecies.Text = speciesRow.Name;

            labelReference.Text = speciesRow.IsReferenceNull() ? 
                Constants.Null : speciesRow.Reference;

            labelLocal.Text = speciesRow.IsLocalNull() ? 
                Constants.Null : speciesRow.Local.GetLocalizedValue();

            labelTaxon.Text = string.Empty;
            foreach (TaxonomicIndex.TaxonRow taxonRow in speciesRow.Hierarchy) {
                labelTaxon.Text += taxonRow.FullName + Constants.Break;
            }

            labelDescription.Text = speciesRow.IsDescriptionNull() ?
                Resources.Interface.DescriptionNull : speciesRow.Description;

            labelSynonyms.Text = string.Empty;
            foreach (TaxonomicIndex.TaxonRow syn in speciesRow.Synonyms) {
                labelSynonyms.Text += syn.FullName + Constants.Break;
            }
        }

        public SpeciesCard(TaxonomicIndex.TaxonRow speciesRow, bool allowSelect) : 
            this(speciesRow)
        {
            buttonSelect.Visible = allowSelect;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
