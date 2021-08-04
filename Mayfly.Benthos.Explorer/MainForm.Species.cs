using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseSpc;

        SpeciesKey.TaxaRow[] taxaSpc;

        SpeciesKey.SpeciesRow[] variaSpc;

        
        
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

        private Data.SpeciesRow GetSpcRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnSpcID.Index].Value;
            return data.Species.FindByID(ID);
        }

        private DataGridViewRow GetSpcRow(Data.SpeciesRow speciesRow)
        {
            return columnSpcID.GetRow(speciesRow.ID, true, true);
        }

        private DataGridViewRow UpdateSpeciesRow(Data.SpeciesRow speciesRow)
        {
            DataGridViewRow result = GetSpcRow(speciesRow);
            result.Cells[columnSpcSpc.Index].Value = speciesRow.Species;
            return result;
        }
    }
}
