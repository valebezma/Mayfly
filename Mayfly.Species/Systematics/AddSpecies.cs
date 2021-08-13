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
    public partial class AddSpecies : Form
    {
        public bool IsChanged = false;

        public SpeciesKey.SpeciesRow SpeciesRow;

        public List<SpeciesKey.TaxaRow> SelectedTaxa;



        private AddSpecies()
        {
            InitializeComponent();
            SelectedTaxa = new List<SpeciesKey.TaxaRow>();
        }

        public AddSpecies(SpeciesKey.SpeciesRow speciesRow) : this()
        {
            textBoxScientific.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Species.Genera);
            textBoxReference.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Species.References);

            SpeciesRow = speciesRow;

            textBoxScientific.Text = SpeciesRow.Species;
            if (!SpeciesRow.IsNameNull()) textBoxLocal.Text = SpeciesRow.Name;
            if (!SpeciesRow.IsReferenceNull()) textBoxReference.Text = SpeciesRow.Reference;
            if (!SpeciesRow.IsDescriptionNull()) textBoxDescription.Text = SpeciesRow.Description;

            AddTaxaSelectors((SpeciesKey)speciesRow.Table.DataSet);

            IsChanged = false;
        }



        SpeciesKey.BaseRow selectedBase;

        public void AddTaxaSelectors(SpeciesKey key)
        {
            flowTaxa.Controls.Clear();;

            foreach (SpeciesKey.BaseRow baseRow in key.Base)
            {
                selectedBase = baseRow;

                Button baseButton = new Button();
                baseButton.AutoSize = true;
                baseButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                baseButton.Name = baseRow.ID.ToString();
                baseButton.Text = string.Format(Resources.Interface.TaxaSelect, baseRow.BaseName);
                baseButton.FlatStyle = FlatStyle.System;

                ContextMenuStrip baseMenu = new ContextMenuStrip();

                ToolStripMenuItem otheritem = new ToolStripMenuItem(Resources.Interface.Varia);
                otheritem.CheckOnClick = true;
                otheritem.Name = "Others";
                otheritem.CheckedChanged += ((o, e) =>
                {
                    if (otheritem.Checked)
                    {
                        foreach (SpeciesKey.TaxaRow taxRow in selectedBase.GetTaxaRows())
                        {
                            if (SelectedTaxa.Contains(taxRow))
                            {
                                SelectedTaxa.Remove(taxRow);
                            }
                        }

                        for (int i = 2; i < baseMenu.Items.Count; i++)
                        {
                            ((ToolStripMenuItem)baseMenu.Items[i]).Checked = false;
                        }

                        baseButton.Text = string.Format(Resources.Interface.TaxaSelect, baseRow.BaseName);
                    }
                });
                baseMenu.Items.Add(otheritem);
                otheritem.Checked = baseRow.Varia.Contains(SpeciesRow);

                baseMenu.Items.Add(new ToolStripSeparator());

                List<SpeciesKey.TaxaRow> taxaRows = new List<SpeciesKey.TaxaRow>();
                taxaRows.AddRange(baseRow.GetTaxaRows());
                taxaRows.Sort();

                foreach (SpeciesKey.TaxaRow taxaRow in taxaRows)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(taxaRow.TaxonName);
                    item.CheckOnClick = true;
                    item.CheckedChanged += ((o, e) =>
                    {
                        if (item.Checked)
                        {
                            foreach (SpeciesKey.TaxaRow taxRow in selectedBase.GetTaxaRows())
                            {
                                if (SelectedTaxa.Contains(taxRow))
                                {
                                    SelectedTaxa.Remove(taxRow);
                                }
                            }
                            SelectedTaxa.Add(taxaRow);

                            otheritem.Checked = false;
                            for (int i = 2; i < baseMenu.Items.Count; i++)
                            {
                                if (baseMenu.Items[i] == item) continue;
                                ((ToolStripMenuItem)baseMenu.Items[i]).Checked = false;
                            }

                            baseButton.Text = string.Format("{0} {1}", baseRow.BaseName, taxaRow.TaxonName);
                        }
                    });
                    item.Name = taxaRow.ID.ToString();
                    baseMenu.Items.Add(item);

                    item.Checked = taxaRow.Includes(SpeciesRow.Species);
                }

                baseButton.Click += ((o, e) =>
                {
                    selectedBase = key.Base.FindByID(int.Parse(baseButton.Name));
                    baseMenu.Show(baseButton, new Point(0, baseButton.Height));
                });

                flowTaxa.Controls.Add(baseButton);
            }

            labelNoTaxa.Visible = flowTaxa.Controls.Count == 0;
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

        private void valueChanged(object sender, EventArgs e)
        { 
            IsChanged = true;

            buttonOK.Enabled = textBoxScientific.Text.IsAcceptable();
        }
    }
}
