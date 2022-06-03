using Mayfly.Benthos;
using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Extensions;

namespace Mayfly.Benthos
{
    public static partial class DataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Suggested name for data card with extension</returns>
        public static string GetSuggestedName(this Data.CardRow cardRow)
        {
            return cardRow.GetSuggestedName(UserSettings.Interface.Extension);            
        }

        public static SpeciesKey GetSpeciesKey(this Data data)
        {
            return ((Data.SpeciesRow[])data.Species.Select()).GetSpeciesKey();
        }

        public static Data.SpeciesRow[] GetUnweightedSpecies(this Data data)
        {
            List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();

            foreach (Data.LogRow logRow in data.Log)
            {
                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.SpeciesRow))
                {
                    result.Add(logRow.SpeciesRow);
                }
            }

            return result.ToArray();
        }

        public static Data.SpeciesRow[] GetSpeciesWithUnweightedIndividuals(this Data data)
        {
            List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();

            foreach (Data.IndividualRow individualRow in data.Individual)
            {
                if (!individualRow.IsMassNull()) continue;
                if (!result.Contains(individualRow.LogRow.SpeciesRow))
                {
                    result.Add(individualRow.LogRow.SpeciesRow);
                }
            }

            return result.ToArray();
        }



        public static SubstrateSample GetSubstrate(this Data.CardRow cardRow)
        {
            return cardRow.IsSubstrateNull() ? null : new SubstrateSample(cardRow.Substrate);
        }



        public static double GetAverageMass(this Data.SpeciesRow spcRow)
        {
            double result = 0;
            int divider = 0;

            foreach (Data.IndividualRow individualRow in spcRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result += individualRow.Mass;
                divider++;
            }

            if (divider > 0) // There are some weighted individuals of given length class
            {
                return result / divider;
            }
            else
            {
                return double.NaN;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Data.LogRow logRow)
        {
            if (logRow.IsQuantityNull()) return double.NaN;
            if (logRow.CardRow.IsSquareNull()) return double.NaN;

            return System.Math.Round((logRow.IsQuantityNull() ? (double)logRow.DetailedQuantity: (double)logRow.Quantity) /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Square, 6);
            //return System.Math.Round((logRow.IsQuantityNull() ? (double)logRow.DetailedQuantity : (double)logRow.Quantity) /
            //    (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
            //    logRow.CardRow.Square, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per square meter in grams</returns>
        public static double GetBiomass(this Data.LogRow logRow)
        {
            if (logRow.CardRow.IsSquareNull()) return double.NaN;

            return 0.001 * (logRow.IsMassNull() ? logRow.DetailedMass: logRow.Mass /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Square);
        }

        public static SpeciesKey GetSpeciesKey(this Data.SpeciesRow[] dataSpcRows)
        {
            SpeciesKey speciesKey = new SpeciesKey();

            if (dataSpcRows.Length == 0) return speciesKey;

            Data data = (Data)dataSpcRows[0].Table.DataSet;

            foreach (Data.SpeciesRow dataSpcRow in dataSpcRows)
            {
                SpeciesKey.SpeciesRow speciesRow = speciesKey.Species.NewSpeciesRow();

                speciesRow.Species = dataSpcRow.Species;

                SpeciesKey.SpeciesRow equivalentRow = dataSpcRow.KeyRecord;

                if (equivalentRow != null)
                {
                    if (!equivalentRow.IsReferenceNull()) speciesRow.Reference = equivalentRow.Reference;
                    if (!equivalentRow.IsNameNull()) speciesRow.Name = equivalentRow.Name;
                }

                //var logRows = dataSpcRow.GetLogRows();

                //if (logRows.Length > 0)
                //{
                //    speciesRow.Description = "Species collected: ";

                //    foreach (Data.LogRow logRow in logRows)
                //    {
                //        if (!logRow.CardRow.IsWaterIDNull())
                //        {
                //            speciesRow.Description += string.Format("at {0} ", logRow.CardRow.WaterRow.Presentation);
                //        }

                //        if (!logRow.CardRow.IsWhenNull())
                //        {
                //            speciesRow.Description += logRow.CardRow.When.ToString("d");
                //        }

                //        Sample lengths = data.Individual.LengthColumn.GetSample(logRow.GetIndividualRows());
                //        Sample masses = data.Individual.MassColumn.GetSample(logRow.GetIndividualRows());
                //        List<object> sexes = data.Individual.SexColumn.GetValues(logRow.GetIndividualRows(), true);

                //        if (lengths.Count + masses.Count + sexes.Count > 0)
                //        {
                //            speciesRow.Description += ": ";

                //            if (lengths.Count > 0)
                //            {
                //                speciesRow.Description += string.Format("L = {0} mm", lengths.ToString("E"));
                //            }

                //            if (masses.Count > 0)
                //            {
                //                speciesRow.Description += ", ";
                //                speciesRow.Description += string.Format("W = {0} mg", masses.ToString("E"));
                //            }

                //            if (sexes.Count > 0)
                //            {
                //                speciesRow.Description += ", ";
                //                speciesRow.Description += "sexes are ";
                //                foreach (object sex in sexes)
                //                {
                //                    speciesRow.Description += new Sex((int)sex).ToString("C") + ", ";
                //                }
                //            }
                //        }

                //        speciesRow.Description += "; ";
                //    }

                //    speciesRow.Description = speciesRow.Description.TrimEnd("; ".ToCharArray()) + ".";
                //}

                speciesKey.Species.AddSpeciesRow(speciesRow);

                //if (Species.UserSettings.VisualConfirmationOnAutoUpdate)
                //{
                //    AddAutoSpecies editSpecies = new AddAutoSpecies(speciesRow);

                //    if (editSpecies.ShowDialog() == DialogResult.OK)
                //    {
                //        speciesKey.Species.AddSpeciesRow(speciesRow);

                //        //foreach (SpeciesKey.TaxaRow taxaRow in editSpecies.SelectedTaxa)
                //        //{
                //        //    speciesKey.Species.AddSpeciesRow(speciesRow);
                //        //    dest.Rep.AddRepRow(taxaRow, destRow);
                //        //}
                //    }
                //}
                //else
                //{
                //    speciesKey.Species.AddSpeciesRow(speciesRow);
                //}
            }

            return speciesKey;
        }
    }
}

