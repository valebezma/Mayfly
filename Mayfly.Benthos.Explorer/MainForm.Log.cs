using Mayfly.Benthos;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Wild;
using Mayfly.Wild.Controls;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseLog;

        SpeciesKey.TaxaRow[] taxaLog;

        SpeciesKey.SpeciesRow[] variaLog;


        
        private void LoadLog()
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();

            columnLogDiversityA.Visible = (baseLog != null);
            columnLogDiversityB.Visible = (baseLog != null);

            loaderLog.RunWorkerAsync();
        }

        private Data.LogRow GetLogRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnLogID.Index].Value;
            return data.Log.FindByID(ID);
        }

        private DataGridViewRow GetLogRow(Data.LogRow logRow)
        {
            return columnLogID.GetRow(logRow.ID, true, true);
        }

        private DataGridViewRow[] GetCardLogRows(Data.CardRow cardRow)
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

        //private DataGridViewRow LogRow(SpeciesKey.TaxaRow taxaRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();

        //    result.CreateCells(spreadSheetSpc);

        //    result.Cells[columnSpcID.Index].Value = taxaRow.ID;

        //    double Q = 0.0;
        //    double M = 0.0;

        //    double A = 0.0;
        //    double B = 0.0;
        //    List<double> Abundances = new List<double>();
        //    List<double> Biomasses = new List<double>();

        //    foreach (Data.LogRow logRow in data.Log)
        //    {
        //        if (logRow.CardRow.IsSquareNull()) continue;

        //        if (!taxaRow.DoesInclude(logRow.SpeciesRow.Species)) continue;

        //        if (!logRow.IsQuantityNull())
        //        {
        //            Q += logRow.Quantity;
        //            A += logRow.Abundance;
        //            Abundances.Add(logRow.Abundance);
        //        }

        //        if (!logRow.IsMassNull())
        //        {
        //            M += logRow.Mass;
        //            double b = logRow.Biomass * (DietExplorer ? 10000d : 1d);
        //            B += b;
        //            Biomasses.Add(b);
        //        }
        //    }

        //    //A /= Data.Card.Count;
        //    //B /= Data.Card.Count;
        //    //A = Math.Round(A / data.Card.Count, 3);
        //    //B = Math.Round(B / data.Card.Count, 3);

        //    result.Cells[columnSpcSpc.Index].Value = taxaRow.TaxonName;

        //    if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
        //    if (Q > 0) result.Cells[columnSpcMass.Index].Value = M;

        //    if (A > 0) result.Cells[columnSpcAbundance.Index].Value = A;
        //    if (B > 0) result.Cells[columnSpcBiomass.Index].Value = B;

        //    result.Cells[columnSpcOccurrence.Index].Value = FullStack.GetOccurrence(taxaRow.GetSpecies());

        //    result.Cells[columnSpcDiversityA.Index].Value = new Sample(Abundances).Diversity();
        //    result.Cells[columnSpcDiversityB.Index].Value = new Sample(Biomasses).Diversity();

        //    return result;
        //}

        //private DataGridViewRow LogRowVaria()
        //{
        //    DataGridViewRow result = new DataGridViewRow();

        //    result.CreateCells(spreadSheetSpc);

        //    result.Cells[columnSpcID.Index].Value = 0;

        //    double Q = 0.0;
        //    double M = 0.0;
        //    double A = 0.0;
        //    double B = 0.0;
        //    List<double> Abundances = new List<double>();
        //    List<double> Biomasses = new List<double>();

        //    foreach (Data.LogRow logRow in data.Log)
        //    {
        //        if (logRow.CardRow.IsSquareNull()) continue;

        //        if (!variaSpc.Contains(SpeciesIndex.Species.FindBySpecies(logRow.SpeciesRow.Species))) continue;

        //        if (!logRow.IsQuantityNull())
        //        {
        //            Q += logRow.Quantity;
        //            A += logRow.Abundance;
        //            Abundances.Add(logRow.Abundance);
        //        }

        //        if (!logRow.IsMassNull())
        //        {
        //            M += logRow.Mass;
        //            double b = logRow.Biomass * (DietExplorer ? 10000d : 1d);
        //            B += b;
        //            Biomasses.Add(b);
        //        }
        //    }

        //    //A = Math.Round(A / data.Card.Count, 3);
        //    //B = Math.Round(B / data.Card.Count, 3);

        //    result.Cells[columnSpcSpc.Index].Value = Species.Resources.Interface.Varia;

        //    if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
        //    if (Q > 0) result.Cells[columnSpcMass.Index].Value = M;

        //    if (A > 0) result.Cells[columnSpcAbundance.Index].Value = A;
        //    if (B > 0) result.Cells[columnSpcBiomass.Index].Value = B;

        //    result.Cells[columnSpcOccurrence.Index].Value = FullStack.GetOccurrence(variaSpc);

        //    result.Cells[columnSpcDiversityA.Index].Value = new Sample(Abundances).Diversity();
        //    result.Cells[columnSpcDiversityB.Index].Value = new Sample(Biomasses).Diversity();

        //    return result;
        //}



        private DataGridViewRow LogRow(Data.CardRow cardRow, SpeciesKey.TaxaRow taxaRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID; // Why?

            double q = 0.0;
            double w = 0.0;

            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (logRow.CardRow.IsSquareNull()) continue;

                if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

                if (!logRow.IsQuantityNull())
                {
                    q += logRow.Quantity;
                    abundances.Add(logRow.GetAbundance() * (DietExplorer ? 1000 : 1));
                }

                if (!logRow.IsMassNull())
                {
                    w += logRow.Mass;
                    biomasses.Add(logRow.GetBiomass() * (DietExplorer ? 10 : 1));
                }
            }

            SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = taxaRow.TaxonName;

            result.Cells[columnLogQuantity.Index].Value = (int)q;
            result.Cells[columnLogMass.Index].Value = w;

            result.Cells[columnLogAbundance.Index].Value = abundances.Sum();
            result.Cells[columnLogBiomass.Index].Value = biomasses.Sum();

            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }

        private DataGridViewRow LogRowVaria(Data.CardRow cardRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID; // Why?

            double q = 0.0;
            double w = 0.0;

            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (logRow.CardRow.IsSquareNull()) continue;

                if (!variaLog.Contains(SpeciesIndex.Species.FindBySpecies(logRow.SpeciesRow.Species))) continue;

                if (!logRow.IsQuantityNull())
                {
                    q += logRow.Quantity;
                    abundances.Add(logRow.GetAbundance() * (DietExplorer ? 1000 : 1));
                }

                if (!logRow.IsMassNull())
                {
                    w += logRow.Mass;
                    biomasses.Add(logRow.GetBiomass() * (DietExplorer ? 10 : 1));
                }
            }

            SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.Varia;

            result.Cells[columnLogQuantity.Index].Value = (int)q;
            result.Cells[columnLogMass.Index].Value = w;

            result.Cells[columnLogAbundance.Index].Value = abundances.Sum();
            result.Cells[columnLogBiomass.Index].Value = biomasses.Sum();

            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }



        private DataGridViewRow LogRow(Data.CardRow cardRow, Data.SpeciesRow speciesRow)
        {
            Data.LogRow logRow = data.Log.FindByCardIDSpcID(cardRow.ID, speciesRow.ID);

            if (logRow == null)
            {
                DataGridViewRow result = new DataGridViewRow();
                result.CreateCells(spreadSheetLog);
                result.Cells[columnLogSpc.Index].Value = speciesRow.Species;
                result.Cells[columnLogID.Index].Value = cardRow.ID; // Why?
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

        private DataGridViewRow UpdateLogRow(Data.LogRow logRow)
        {
            DataGridViewRow result = GetLogRow(logRow);

            if (logRow.IsSpcIDNull())
            {
                result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }
            else
            {
                result.Cells[columnLogSpc.Index].Value = logRow.SpeciesRow.Species;
            }

            result.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
            result.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance() * (DietExplorer ? 1000 : 1);

            if (!logRow.IsMassNull()) result.Cells[columnLogMass.Index].Value = logRow.Mass;
            result.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass() * (DietExplorer ? 10 : 1);

            SetCardValue(logRow.CardRow, result, spreadSheetLog.GetInsertedColumns());

            return result;
        }

        //private DataGridViewRow LogRow(Data.SpeciesRow speciesRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();

        //    result.CreateCells(spreadSheetSpc);

        //    result.Cells[columnSpcID.Index].Value = speciesRow.ID;

        //    if (speciesRow.IsSpeciesNull())
        //    {
        //        result.Cells[columnSpcSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
        //    }
        //    else
        //    {
        //        result.Cells[columnSpcSpc.Index].Value = speciesRow.Species;
        //    }

        //    result.Cells[columnSpcQuantity.Index].Value = Data.Quantity(speciesRow);
        //    result.Cells[columnSpcMass.Index].Value = Data.Mass(speciesRow);
        //    result.Cells[columnSpcAbundance.Index].Value = Data.Abundance(speciesRow);
        //    result.Cells[columnSpcBiomass.Index].Value = Data.Biomass(speciesRow) * (DietExplorer ? 10000d : 1d);
        //    result.Cells[columnSpcOccurrence.Index].Value = speciesRow.Occurrence();
        //    result.Cells[columnSpcDominance.Index].Value = speciesRow.Dominance();

        //    return result;
        //}
    }
}
