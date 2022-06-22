using System;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using Mayfly.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mayfly.Species
{
    public partial class EditSpecies : Form
    {
        public bool IsChanged = false;

        public SpeciesKey.SpeciesRow SpeciesRow;



        public EditSpecies(SpeciesKey.SpeciesRow speciesRow)
        {
            InitializeComponent();

            taxonSelector.Data = (SpeciesKey)speciesRow.Table.DataSet;

            textBoxScientific.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Genera);
            textBoxReference.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).References);

            SpeciesRow = speciesRow;

            try { textBoxScientific.Text = SpeciesRow.Species; } catch { }
            if (!SpeciesRow.IsNameNull()) textBoxLocal.Text = SpeciesRow.Name;
            if (!SpeciesRow.IsReferenceNull()) textBoxReference.Text = SpeciesRow.Reference;
            if (!SpeciesRow.IsDescriptionNull()) textBoxDescription.Text = SpeciesRow.Description;
            taxonSelector.Taxon = SpeciesRow.IsTaxIDNull() ? null : SpeciesRow.TaxonRow;

            IsChanged = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SpeciesRow.Species = textBoxScientific.Text;

            if (!textBoxLocal.Text.IsAcceptable()) SpeciesRow.SetNameNull();
            else SpeciesRow.Name = textBoxLocal.Text;

            if (!textBoxReference.Text.IsAcceptable()) SpeciesRow.SetReferenceNull();
            else SpeciesRow.Reference = textBoxReference.Text;

            if (!textBoxDescription.Text.IsAcceptable()) SpeciesRow.SetDescriptionNull();
            else SpeciesRow.Description = textBoxDescription.Text;

            if (taxonSelector.Taxon == null) SpeciesRow.SetTaxIDNull();
            else SpeciesRow.TaxID = taxonSelector.Taxon.ID;

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

        private void comboBoxTaxon_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void valueChanged(object sender, EventArgs e)
        { 
            IsChanged = true;
            buttonOK.Enabled = textBoxScientific.Text.IsAcceptable();
        }
    }
}
