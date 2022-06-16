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

        Button clickedButton;



        public AddSpecies(SpeciesKey.SpeciesRow speciesRow)
        {
            InitializeComponent();
            SelectedTaxa = new List<SpeciesKey.TaxaRow>();

            textBoxScientific.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Species.Genera);
            textBoxReference.AutoCompleteCustomSource.AddRange(((SpeciesKey)speciesRow.Table.DataSet).Species.References);

            SpeciesRow = speciesRow;

            try { textBoxScientific.Text = SpeciesRow.Species; } catch { }
            if (!SpeciesRow.IsNameNull()) textBoxLocal.Text = SpeciesRow.Name;
            if (!SpeciesRow.IsReferenceNull()) textBoxReference.Text = SpeciesRow.Reference;
            if (!SpeciesRow.IsDescriptionNull()) textBoxDescription.Text = SpeciesRow.Description;

            AddTaxaSelectors();

            IsChanged = false;
        }



        SpeciesKey.BaseRow selectedBase;

        public void AddTaxaSelectors()
        {
            SpeciesKey key = (SpeciesKey)SpeciesRow.Table.DataSet;
            flowTaxa.Controls.Clear();

            foreach (SpeciesKey.BaseRow baseRow in key.Base.Select(null, "Index DESC"))
            {
                selectedBase = baseRow;
                Button baseButton = new Button
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Name = baseRow.ID.ToString(),
                    Text = string.Format(Resources.Interface.TaxaSelect, baseRow.BaseName),
                    FlatStyle = FlatStyle.System
                };
                clickedButton = baseButton;
                ContextMenuStrip baseMenu = getBaseMenu(baseRow);
                baseMenu.Opening += baseMenu_Opening;
                baseButton.Click += (o, e) =>
                {
                    clickedButton = baseButton;
                    selectedBase = key.Base.FindByID(int.Parse(baseButton.Name));
                    baseMenu.Show(baseButton, new Point(0, baseButton.Height));
                };

                flowTaxa.Controls.Add(baseButton);
            }

            labelNoTaxa.Visible = flowTaxa.Controls.Count == 0;
        }

        private void baseMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //for (int i = 2; i < ((ContextMenuStrip)sender).Items.Count; i++)
            //{
            //    ToolStripItem item = ((ContextMenuStrip)sender).Items[i];
            //    item.Visible = true;
            //}
            //selectedBase
        }

        private ContextMenuStrip getBaseMenu(SpeciesKey.BaseRow baseRow)
        {
            ContextMenuStrip baseMenu = new ContextMenuStrip();

            ToolStripMenuItem otheritem = new ToolStripMenuItem(Resources.Interface.Varia)
            {
                CheckOnClick = true,
                Name = "Varia"
            };
            otheritem.CheckedChanged += (o, e) =>
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

                    clickedButton.Text = string.Format(Resources.Interface.TaxaSelect, baseRow.BaseName);
                }
            };
            baseMenu.Items.Add(otheritem);
            otheritem.Checked = baseRow.Varia.Contains(SpeciesRow);

            baseMenu.Items.Add(new ToolStripSeparator());

            List<SpeciesKey.TaxaRow> taxaRows = new List<SpeciesKey.TaxaRow>();
            taxaRows.AddRange(baseRow.GetTaxaRows());
            taxaRows.Sort();

            foreach (SpeciesKey.TaxaRow taxaRow in taxaRows)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(taxaRow.TaxonName)
                {
                    CheckOnClick = true
                };
                item.Name = taxaRow.ID.ToString();
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

                        clickedButton.Text = string.Format("{0} {1}", baseRow.BaseName, taxaRow.TaxonName);

                        //baseRow
                        //taxaRow                       



                        // Recalculate other button menus!!!
                    }
                });
                baseMenu.Items.Add(item);
                item.Checked = taxaRow.Includes(SpeciesRow, true);
            }

            return baseMenu;
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
