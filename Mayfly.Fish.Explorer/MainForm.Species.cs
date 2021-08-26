using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseSpc;

        SpeciesKey.TaxaRow[] taxaSpc;


        private void LoadTaxaList()
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

        private void LoadSpc()
        {
            IsBusy = true;
            spreadSheetSpc.StartProcessing(baseSpc == null ? data.Species.Count : taxaSpc.Length,
                Wild.Resources.Interface.Process.SpeciesProcessing);

            spreadSheetSpc.Rows.Clear();

            loaderSpc.RunWorkerAsync();
        }

        private DataGridViewRow GetLine(SpeciesKey.TaxaRow taxaRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetSpc);

            result.Cells[columnSpcID.Index].Value = taxaRow.ID;

            double Q = 0.0;
            double W = 0.0;

            foreach (Data.LogRow logRow in data.Log)
            {
                if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

                if (!logRow.IsQuantityNull())
                {
                    Q += logRow.Quantity;
                }

                if (!logRow.IsMassNull())
                {
                    W += logRow.Mass;
                }
            }

            //Q /= Data.GetStack().Card.Count;
            //W /= Data.GetStack().Card.Count;

            result.Cells[columnSpcSpc.Index].Value = taxaRow.TaxonName;
            if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
            if (W > 0) result.Cells[columnSpcMass.Index].Value = W;
            result.Cells[columnSpcOccurrence.Index].Value = taxaRow.Occurrence(data);

            return result;
        }
    }
}
