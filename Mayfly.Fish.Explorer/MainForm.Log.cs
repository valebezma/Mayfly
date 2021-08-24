using Mayfly.Extensions;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        SpeciesKey.BaseRow baseLog;

        SpeciesKey.TaxaRow[] taxaLog;



        private void LoadLog()
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();

            columnLogDiversityA.Visible = (baseSpc != null);
            columnLogDiversityB.Visible = (baseSpc != null);

            loaderLog.RunWorkerAsync();
        }



        private DataGridViewRow GetLine(Data.CardRow cardRow, SpeciesKey.TaxaRow taxaRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID;

            int Q = 0;
            double W = 0.0;
            double A = 0.0;
            double B = 0.0;
            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (double.IsNaN(cardRow.GetEffort())) continue;

                if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

                if (!logRow.IsQuantityNull())
                {
                    Q += logRow.Quantity;
                    A += logRow.GetAbundance();
                    abundances.Add(logRow.GetAbundance());
                }

                if (!logRow.IsMassNull())
                {
                    W += logRow.Mass;
                    B += logRow.GetBiomass();
                    biomasses.Add(logRow.GetBiomass());
                }
            }

            setCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = taxaRow.TaxonName;

            result.Cells[columnLogQuantity.Index].Value = Q;
            result.Cells[columnLogMass.Index].Value = W;
            result.Cells[columnLogAbundance.Index].Value = A;
            result.Cells[columnLogBiomass.Index].Value = B;
            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }

        private DataGridViewRow GetLine(Data.CardRow cardRow, Data.SpeciesRow speciesRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogSpc.Index].Value = speciesRow;
            result.Cells[columnLogID.Index].Value = cardRow.ID;

            Data.LogRow logRow = data.Log.FindByCardIDSpcID(cardRow.ID, speciesRow.ID);

            if (logRow == null)
            {
                result.Cells[columnLogQuantity.Index].Value = 0;
                result.Cells[columnLogMass.Index].Value = 0D;
                result.Cells[columnLogAbundance.Index].Value = 0D;
                result.Cells[columnLogBiomass.Index].Value = 0D;

                result.Cells[columnLogAbundance.Index].Style.Format =
                result.Cells[columnLogBiomass.Index].Style.Format = string.Empty;
            }
            else
            {
                if (!logRow.IsQuantityNull())
                //{
                //    result.Cells[columnLogQuantity.Index].Value = double.NaN;
                //    result.Cells[columnLogAbundance.Index].Value = double.NaN;
                //}
                //else
                {
                    result.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
                    result.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
                    result.Cells[columnLogAbundance.Index].Style.Format = columnLogAbundance.ExtendFormat(logRow.CardRow.GetAbundanceUnits());
                }

                if (!logRow.IsMassNull())
                {
                    result.Cells[columnLogMass.Index].Value = logRow.Mass;
                    result.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();
                    result.Cells[columnLogBiomass.Index].Style.Format = columnLogBiomass.ExtendFormat(logRow.CardRow.GetBiomassUnits());
                }
            }

            setCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            return result;
        }



        private Data.LogRow LogRow(DataGridViewRow gridRow)
        {
            int id = (int)gridRow.Cells[columnLogID.Index].Value;

            if (baseSpc == null) // Every row is log row with species and cardID
            {
                string species = (string)gridRow.Cells[columnLogSpc.Index].Value;
                return data.Log.FindByCardIDSpcID(id, data.Species.FindBySpecies(species).ID);
            }
            else // Every row is taxa+card combination with multiple log rows
            {
                return null;
            }
        }

        private Data.LogRow LogRowStratified(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnStratifiedID.Index].Value;
            return data.Log.FindByID(ID);
        }
    }
}
