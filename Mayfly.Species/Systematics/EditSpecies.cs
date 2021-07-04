using System;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using Mayfly.Extensions;

namespace Mayfly.Species.Systematics
{
    public partial class EditSpecies : Form
    {
        public bool IsChanged = false;
        public SpeciesKey.SpeciesRow SpeciesRow;


        public EditSpecies(SpeciesKey.SpeciesRow speciesRow)
        {
            InitializeComponent();

            textBoxScientific.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Species.Genera);
            textBoxReference.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Species.References);

            SpeciesRow = speciesRow;

            if (!SpeciesRow.IsSpeciesNull()) textBoxScientific.Text = SpeciesRow.Species;
            if (!SpeciesRow.IsLocalNull()) textBoxLocal.Text = SpeciesRow.Local;
            if (!SpeciesRow.IsReferenceNull()) textBoxReference.Text = SpeciesRow.Reference;

            IsChanged = false;
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            SpeciesRow.Species = textBoxScientific.Text;

            if (!textBoxLocal.Text.IsAcceptable()) SpeciesRow.SetLocalNull();
            else SpeciesRow.Local = textBoxLocal.Text;

            if (!textBoxReference.Text.IsAcceptable()) SpeciesRow.SetReferenceNull();
            else SpeciesRow.Reference = textBoxReference.Text;

            if (!textBoxDescription.Text.IsAcceptable()) SpeciesRow.SetDescriptionNull();
            else SpeciesRow.Description = textBoxDescription.Text;

            Close();
        }

        private void textBoxLocal_Enter(object sender, EventArgs e)
        { 
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage; 
        }

        private void textBoxReference_Enter(object sender, EventArgs e)
        { 
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en"));
        }

        private void ValueChanged(object sender, EventArgs e)
        { 
            IsChanged = true;

            buttonOK.Enabled = textBoxScientific.Text.IsAcceptable();
        }
    }
}
