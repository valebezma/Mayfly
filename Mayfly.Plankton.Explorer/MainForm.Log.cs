using Mayfly.Extensions;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    partial class MainForm
    {
        TaxonomicIndex.BaseRow baseLog;

        TaxonomicIndex.TaxonRow[] taxonLog;

        TaxonomicIndex.TaxonRow[] variaLog;



        private void UpdateLogTotals()
        {
            labelLogCount.UpdateStatus(spreadSheetLog.VisibleRowCount);
        }

        private void LoadLog()
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();

            columnLogDiversityA.Visible = (baseLog != null);
            columnLogDiversityB.Visible = (baseLog != null);

            loaderLog.RunWorkerAsync();
        }

        private DataGridViewRow GetLogRow(Wild.Survey.LogRow logRow)
        {
            return columnLogID.GetRow(logRow.ID, true, true);
        }

        private Wild.Survey.LogRow GetLogRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnLogID.Index].Value;
            return data.Log.FindByID(ID);
        }

        private DataGridViewRow[] GetCardLogRows(Wild.Survey.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (GetLogRow(gridRow).CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }
        


        private DataGridViewRow LogRow(Wild.Survey.CardRow cardRow, Wild.Survey.DefinitionRow speciesRow)
        {
            Wild.Survey.LogRow logRow = data.Log.FindByCardIDDefID(cardRow.ID, speciesRow.ID);

            if (logRow == null)
            {
                DataGridViewRow result = new DataGridViewRow();
                result.CreateCells(spreadSheetLog);
                result.Cells[columnLogSpc.Index].Value = speciesRow.Species;
                result.Cells[columnLogID.Index].Value = cardRow.ID;
                result.Cells[columnLogAbundance.Index].Value = 0D;
                result.Cells[columnLogBiomass.Index].Value = 0D;
                SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());
                return result;
            }
            else
            {
                return UpdateLogRow(logRow);
            }
        }

        private DataGridViewRow UpdateLogRow(Wild.Survey.LogRow logRow)
        {
            DataGridViewRow result = GetLogRow(logRow);
            if (logRow.IsSpcIDNull())
            {
                result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }
            else
            {
                result.Cells[columnLogSpc.Index].Value = logRow.DefinitionRow.Taxon;
            }

            result.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
            result.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
            result.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass() * (DietExplorer ? 10000d : 1d);
            if (!logRow.IsMassNull()) result.Cells[columnLogMass.Index].Value = logRow.Mass;

            SetCardValue(logRow.CardRow, result, spreadSheetLog.GetInsertedColumns());

            return result;
        }

        private DataGridViewRow LogRow(TaxonomicIndex.TaxonRow taxonRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetSpc);

            result.Cells[columnSpcID.Index].Value = taxonRow.ID;

            double Q = 0.0;
            double A = 0.0;
            double B = 0.0;
            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Wild.Survey.LogRow logRow in data.Log)
            {
                if (logRow.CardRow.IsVolumeNull()) continue;

                if (!taxonRow.Includes(logRow.DefinitionRow.Taxon)) continue;

                if (!logRow.IsQuantityNull())
                {
                    Q += logRow.Quantity;
                    A += data.Abundance(logRow);
                    abundances.Add(data.Abundance(logRow));
                }

                if (!logRow.IsMassNull())
                {
                    double b = logRow.GetBiomass() * (DietExplorer ? 10000d : 1d);
                    B += b;
                    biomasses.Add(b);
                }
            }

            //A /= Data.Card.Count;
            //B /= Data.Card.Count;
            A = Math.Round(A / data.Card.Count, 3);
            B = Math.Round(B / data.Card.Count, 3);

            result.Cells[columnSpcSpc.Index].Value = taxonRow.TaxonName;
            if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
            if (A > 0) result.Cells[columnSpcAbundance.Index].Value = A;
            if (B > 0) result.Cells[columnSpcBiomass.Index].Value = B;
            result.Cells[columnSpcOccurrence.Index].Value = taxonRow.Occurrence(data);
            result.Cells[columnSpcDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnSpcDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }

        private DataGridViewRow LogRowVaria()
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetSpc);

            result.Cells[columnSpcID.Index].Value = 0;

            double Q = 0.0;
            double A = 0.0;
            double B = 0.0;
            List<double> Abundances = new List<double>();
            List<double> Biomasses = new List<double>();

            foreach (Wild.Survey.LogRow logRow in data.Log)
            {
                if (logRow.CardRow.IsVolumeNull()) continue;

                if (!variaLog.Contains(SpeciesIndex.Definition.FindByName(logRow.DefinitionRow.Taxon))) continue;

                if (!logRow.IsQuantityNull())
                {
                    Q += logRow.Quantity;
                    A += data.Abundance(logRow);
                    Abundances.Add(data.Abundance(logRow));
                }

                if (!logRow.IsMassNull())
                {
                    double b = logRow.GetBiomass() * (DietExplorer ? 10000d : 1d);
                    B += b;
                    Biomasses.Add(b);
                }
            }

            A = Math.Round(A / data.Card.Count, 3);
            B = Math.Round(B / data.Card.Count, 3);

            result.Cells[columnSpcSpc.Index].Value = Species.Resources.Interface.Varia;
            if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
            if (A > 0) result.Cells[columnSpcAbundance.Index].Value = A;
            if (B > 0) result.Cells[columnSpcBiomass.Index].Value = B;
            result.Cells[columnSpcDiversityA.Index].Value = new Sample(Abundances).Diversity();
            result.Cells[columnSpcDiversityB.Index].Value = new Sample(Biomasses).Diversity();

            return result;
        }

        private DataGridViewRow LogRow(Wild.Survey.CardRow cardRow, TaxonomicIndex.TaxonRow taxonRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID;

            double A = 0.0;
            double B = 0.0;
            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                if (logRow.CardRow.IsVolumeNull()) continue;

                if (!taxonRow.Includes(logRow.DefinitionRow.Taxon)) continue;

                if (!logRow.IsQuantityNull())
                {
                    A += data.Abundance(logRow);
                    abundances.Add(data.Abundance(logRow));
                }

                if (!logRow.IsMassNull())
                {
                    double b = logRow.GetBiomass() * (DietExplorer ? 10000d : 1d);
                    B += b;
                    biomasses.Add(b);
                }
            }

            SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = taxonRow.TaxonName;

            result.Cells[columnLogAbundance.Index].Value = A;
            result.Cells[columnLogBiomass.Index].Value = B;
            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }

        private DataGridViewRow LogRowVaria(Wild.Survey.CardRow cardRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID;

            double A = 0.0;
            double B = 0.0;
            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                if (logRow.CardRow.IsVolumeNull()) continue;

                if (!variaLog.Contains(SpeciesIndex.Definition.FindByName(logRow.DefinitionRow.Taxon))) continue;

                if (!logRow.IsQuantityNull())
                {
                    A += data.Abundance(logRow);
                    abundances.Add(data.Abundance(logRow));
                }

                if (!logRow.IsMassNull())
                {
                    double b = logRow.GetBiomass() * (DietExplorer ? 10000d : 1d);
                    B += b;
                    biomasses.Add(b);
                }
            }

            SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.Varia;

            result.Cells[columnLogAbundance.Index].Value = A;
            result.Cells[columnLogBiomass.Index].Value = B;
            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }
    }
}
