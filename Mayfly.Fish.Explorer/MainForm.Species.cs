using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseSpc;

        SpeciesKey.TaxaRow[] taxaSpc;



        private void loadTaxaList()
        {
            // Clear list
            comboBoxSpcTaxa.Items.Clear();
            comboBoxLogTaxa.Items.Clear();
            menuItemSpcTaxa.DropDownItems.Clear();

            if (Fish.UserSettings.SpeciesIndex == null) return;

            // Fill list
            foreach (SpeciesKey.BaseRow baseRow in Fish.UserSettings.SpeciesIndex.Base)
            {
                comboBoxSpcTaxa.Items.Add(baseRow);
                comboBoxLogTaxa.Items.Add(baseRow);

                ToolStripMenuItem item = new ToolStripMenuItem(baseRow.BaseName);
                item.Click += baseItem_Click;
                item.Tag = baseRow;
                menuItemSpcTaxa.DropDownItems.Add(item);
            }
            
            comboBoxSpcTaxa.Enabled = comboBoxSpcTaxa.Items.Count > 0;
            comboBoxLogTaxa.Enabled = comboBoxLogTaxa.Items.Count > 0;
            menuItemSpcTaxa.Enabled = menuItemSpcTaxa.DropDownItems.Count > 0;
        }

        private void loadSpc()
        {
            IsBusy = true;
            spreadSheetSpc.StartProcessing(baseSpc == null ? data.Species.Count : taxaSpc.Length,
                Wild.Resources.Interface.Process.SpeciesProcessing);

            spreadSheetSpc.Rows.Clear();

            loaderSpc.RunWorkerAsync();
        }



        private Data.SpeciesRow findSpeciesRow(DataGridViewRow gridRow)
        {
            return baseSpc == null ? data.Species.FindByID((int)gridRow.Cells[columnSpcID.Index].Value) : null;
        }

        private void updateSpeciesArtefact(DataGridViewRow gridRow)
        {
            if (gridRow == null) return;

            SpeciesArtefact artefact = findSpeciesRow(gridRow).GetFacts(FullStack);

            if (artefact.FactsCount > 0)
            {
                ((TextAndImageCell)gridRow.Cells[columnSpcSpc.Index]).Image = Artefact.GetImage(artefact.Criticality);
                gridRow.Cells[columnSpcSpc.Index].ToolTipText = artefact.GetNotices(true).Merge(System.Environment.NewLine);
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnSpcSpc.Index]).Image = null;
                gridRow.Cells[columnSpcSpc.Index].ToolTipText = string.Empty;
            }

        }



        private Data.SpeciesRow[] getSpeciesRows(IList rows)
        {
            spreadSheetLog.EndEdit();
            List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();
            foreach (DataGridViewRow gridRow in rows)
            {
                if (gridRow.IsNewRow) continue;
                result.Add(findSpeciesRow(gridRow));
            }

            return result.ToArray();
        }
    }
}
