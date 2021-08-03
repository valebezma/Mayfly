using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Plankton.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseSpc;

        SpeciesKey.TaxaRow[] taxaSpc;

        SpeciesKey.SpeciesRow[] variaSpc;





        private void UpdateSpcTotals()
        {
            labelSpcCount.UpdateStatus(spreadSheetSpc.VisibleRowCount);
        }

        private void baseItem_Click(object sender, EventArgs e)
        {
            SpeciesKey.BaseRow baseRow = ((ToolStripMenuItem)sender).Tag as SpeciesKey.BaseRow;

            DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(baseRow.BaseName,
                baseRow.BaseName, typeof(string), 0);

            foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
            {
                if (gridRow.Cells[columnSpcSpc.Index].Value == null)
                {
                    continue;
                }

                if (gridRow.Cells[columnSpcSpc.Index].Value as string ==
                    Species.Resources.Interface.UnidentifiedTitle)
                {
                    continue;
                }

                string species = gridRow.Cells[columnSpcSpc.Index].Value as string;

                SpeciesKey.TaxaRow taxaRow = SpeciesIndex.GetTaxon(species, baseRow);

                gridRow.Cells[gridColumn.Index].Value = (taxaRow == null) ?
                    Species.Resources.Interface.Varia : taxaRow.TaxonName;
            }
        }

        private void LoadSpc()
        {
            IsBusy = true;
            spreadSheetSpc.StartProcessing(
                baseSpc == null ? data.Species.Count : (taxaSpc.Length + 1),
                Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetSpc.Rows.Clear();

            columnSpcDiversityA.Visible = (baseSpc != null);
            columnSpcDiversityB.Visible = (baseSpc != null);

            loaderSpc.RunWorkerAsync();
        }


        private DataGridViewRow GetSpcRow(Data.SpeciesRow speciesRow)
        {
            return columnSpcID.GetRow(speciesRow.ID, true, true);
        }

        private Data.SpeciesRow GetSpcRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnSpcID.Index].Value;
            return data.Species.FindByID(ID);
        }

        private DataGridViewRow UpdateSpeciesRow(Data.SpeciesRow speciesRow)
        {
            DataGridViewRow result = GetSpcRow(speciesRow);

            if (speciesRow.IsSpeciesNull())
            {
                result.Cells[columnSpcSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }
            else
            {
                result.Cells[columnSpcSpc.Index].Value = speciesRow.Species;
            }

            return result;
        }

        private void LoadTaxaList()
        {
            // Clear list
            comboBoxSpc.Items.Clear();

            if (SpeciesIndex == null) return;

            // Fill list
            foreach (SpeciesKey.BaseRow baseRow in SpeciesIndex.Base)
            {
                comboBoxSpc.Items.Add(baseRow);

                ToolStripMenuItem item = new ToolStripMenuItem(baseRow.BaseName);
                item.Click += BaseItem_Click;
                item.Tag = baseRow;
                menuItemSpcTaxa.DropDownItems.Add(item);
            }

            menuItemSpcTaxa.Enabled = menuItemSpcTaxa.DropDownItems.Count > 0;
        }

        private void SpcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (baseSpc == null)
            {
                Composition speciesComposition = data.GetCenosisComposition();

                for (int i = 0; i < speciesComposition.Count; i++)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();

                    gridRow.CreateCells(spreadSheetSpc);
                    gridRow.Cells[columnSpcSpc.Index].Value = speciesComposition[i].Name;
                    gridRow.Cells[columnSpcQuantity.Index].Value = speciesComposition[i].Quantity;
                    gridRow.Cells[columnSpcMass.Index].Value = speciesComposition[i].Mass;
                    gridRow.Cells[columnSpcAbundance.Index].Value = speciesComposition[i].Abundance;
                    gridRow.Cells[columnSpcBiomass.Index].Value = speciesComposition[i].Biomass * (DietExplorer ? 10000d : 1d);
                    gridRow.Cells[columnSpcOccurrence.Index].Value = speciesComposition[i].Occurrence;
                    gridRow.Cells[columnSpcDominance.Index].Value = speciesComposition[i].Dominance;
                    result.Add(gridRow);

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }
            else
            {
                for (int i = 0; i < taxaSpc.Length; i++)
                {
                    result.Add(LogRow(taxaSpc[i]));
                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
                result.Add(LogRowVaria());
            }

            e.Result = result.ToArray();
        }

        private void SpcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSpc.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
            UpdateSpcTotals();
        }
    }
}
