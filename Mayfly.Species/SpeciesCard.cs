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

namespace Mayfly.Species
{
    public partial class SpeciesCard : Form
    {
        public SpeciesKey.SpeciesRow SpeciesRow;

        public SpeciesCard(SpeciesKey.SpeciesRow speciesRow)
        {
            InitializeComponent();

            SpeciesRow = speciesRow;

            this.Text = speciesRow.FullName;

            labelSpecies.Text = speciesRow.Species;

            labelReference.Text = speciesRow.IsReferenceNull() ? 
                Constants.Null : speciesRow.Reference;

            labelLocal.Text = speciesRow.IsNameNull() ? 
                Constants.Null : speciesRow.Name;

            labelTaxon.Text = string.Empty;
            foreach (SpeciesKey.TaxonRow taxonRow in speciesRow.GetParents()) {
                labelTaxon.Text += taxonRow.FullName + Constants.Break;
            }

            labelDescription.Text = speciesRow.IsDescriptionNull() ?
                Resources.Interface.DescriptionNull : speciesRow.Description;

            labelSynonyms.Text = string.Empty;
            foreach (SpeciesKey.SpeciesRow syn in speciesRow.Synonyms) {
                labelSynonyms.Text += syn.FullName + Constants.Break;
            }
        }

        public SpeciesCard(SpeciesKey.SpeciesRow speciesRow, bool allowSelect) : 
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
