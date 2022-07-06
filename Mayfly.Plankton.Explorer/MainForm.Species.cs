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
        TaxonomicIndex.BaseRow baseSpc;

        TaxonomicIndex.TaxonRow[] taxonSpc;

        TaxonomicIndex.SpeciesRow[] variaSpc;





        private void UpdateSpcTotals()
        {
            labelSpcCount.UpdateStatus(spreadSheetSpc.VisibleRowCount);
        }

        private void baseItem_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.BaseRow baseRow = ((ToolStripMenuItem)sender).Tag as TaxonomicIndex.BaseRow;

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

                TaxonomicIndex.TaxonRow taxonRow = SpeciesIndex.GetTaxon(species, baseRow);

                gridRow.Cells[gridColumn.Index].Value = (taxonRow == null) ?
                    Species.Resources.Interface.Varia : taxonRow.TaxonName;
            }
        }

        private void LoadSpc()
        {
            IsBusy = true;
            spreadSheetSpc.StartProcessing(
                baseSpc == null ? data.Species.Count : (taxonSpc.Length + 1),
                Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetSpc.Rows.Clear();

            columnSpcDiversityA.Visible = (baseSpc != null);
            columnSpcDiversityB.Visible = (baseSpc != null);

            loaderSpc.RunWorkerAsync();
        }


        private DataGridViewRow GetSpcRow(Data.DefinitionRow speciesRow)
        {
            return columnSpcID.GetRow(speciesRow.ID, true, true);
        }

        private Data.DefinitionRow GetSpcRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnSpcID.Index].Value;
            return data.Species.FindByID(ID);
        }

        private DataGridViewRow UpdateSpeciesRow(Data.DefinitionRow speciesRow)
        {
            DataGridViewRow result = GetSpcRow(speciesRow);
            result.Cells[columnSpcSpc.Index].Value = speciesRow;
            return result;
        }

        private void LoadTaxonList()
        {
            // Clear list
            comboBoxSpc.Items.Clear();

            if (SpeciesIndex == null) return;

            // Fill list
            foreach (TaxonomicIndex.BaseRow baseRow in SpeciesIndex.Base)
            {
                comboBoxSpc.Items.Add(baseRow);

                ToolStripMenuItem item = new ToolStripMenuItem(baseRow.BaseName);
                item.Click += BaseItem_Click;
                item.Tag = baseRow;
                menuItemSpcTaxon.DropDownItems.Add(item);
            }

            menuItemSpcTaxon.Enabled = menuItemSpcTaxon.DropDownItems.Count > 0;
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
                for (int i = 0; i < taxonSpc.Length; i++)
                {
                    result.Add(LogRow(taxonSpc[i]));
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
