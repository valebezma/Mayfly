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

            labelSpecies.Text = speciesRow.Name;

            labelReference.Text = speciesRow.IsReferenceNull() ? 
                Constants.Null : speciesRow.Reference;

            labelLocal.Text = speciesRow.IsLocalNull() ? 
                Constants.Null : speciesRow.Local;

            labelTaxa.Text = string.Empty;
            SpeciesKey.RepRow[] reps = speciesRow.GetRepRows();
            foreach (SpeciesKey.RepRow repRow in reps)
            {
                labelTaxa.Text += repRow.TaxaRow.FullName+
                    (reps.Length > 6 ? Constants.StdSeparator : Constants.Break);
            }

            labelDescription.Text = speciesRow.IsDescriptionNull() ?
                Resources.Interface.DescriptionNull : speciesRow.Description;

            labelSynonyms.Text = string.Empty;
            SpeciesKey.SpeciesRow[] syns = speciesRow.Synonyms;
            foreach (SpeciesKey.SpeciesRow syn in syns)
            {
                labelSynonyms.Text += syn.FullName+
                    (reps.Length > 6 ? Constants.StdSeparator : Constants.Break);
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
