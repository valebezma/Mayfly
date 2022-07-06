using Mayfly.Plankton;
using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Extensions;

namespace Mayfly.Plankton
{
    public static class DataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Suggested name for data card with extension</returns>
        public static string GetSuggestedName(this Data.CardRow cardRow)
        {
            return cardRow.GetSuggestedName(UserSettings.ReaderSettings.Interface.Extension);
        }

        public static TaxonomicIndex GetSpeciesKey(this Data data)
        {
            return Data.GetSpeciesKey((Data.DefinitionRow[])data.Definition.Select());

            //TaxonomicIndex speciesKey = new TaxonomicIndex();

            //foreach (Data.DefinitionRow dataSpcRow in data.Species)
            //{
            //    TaxonomicIndex.SpeciesRow speciesRow = speciesKey.Species.NewSpeciesRow();

            //    speciesRow.Species = dataSpcRow.Species;

            //    TaxonomicIndex.SpeciesRow equivalentRow = Plankton.UserSettings.SpeciesIndex.Definition.FindByName(
            //        dataSpcRow.Species);

            //    if (equivalentRow != null)
            //    {
            //        if (!equivalentRow.IsReferenceNull()) speciesRow.Reference = equivalentRow.Reference;
            //        if (!equivalentRow.IsNameNull()) speciesRow.Name = equivalentRow.Name;
            //    }

            //    //var logRows = dataSpcRow.GetLogRows();

            //    //if (logRows.Length > 0)
            //    //{
            //    //    speciesRow.Description = "Species collected: ";

            //    //    foreach (Data.LogRow logRow in logRows)
            //    //    {
            //    //        if (!logRow.CardRow.IsWaterIDNull())
            //    //        {
            //    //            speciesRow.Description += string.Format("at {0} ", logRow.CardRow.WaterRow.Presentation);
            //    //        }

            //    //        if (!logRow.CardRow.IsWhenNull())
            //    //        {
            //    //            speciesRow.Description += logRow.CardRow.When.ToString("d");
            //    //        }

            //    //        Sample lengths = data.Individual.LengthColumn.GetSample(logRow.GetIndividualRows());
            //    //        Sample masses = data.Individual.MassColumn.GetSample(logRow.GetIndividualRows());
            //    //        List<object> sexes = data.Individual.SexColumn.GetValues(logRow.GetIndividualRows(), true);

            //    //        if (lengths.Count + masses.Count + sexes.Count > 0)
            //    //        {
            //    //            speciesRow.Description += ": ";

            //    //            if (lengths.Count > 0)
            //    //            {
            //    //                speciesRow.Description += string.Format("L = {0} mm", lengths.ToString("E"));
            //    //            }

            //    //            if (masses.Count > 0)
            //    //            {
            //    //                speciesRow.Description += ", ";
            //    //                speciesRow.Description += string.Format("W = {0} mg", masses.ToString("E"));
            //    //            }

            //    //            if (sexes.Count > 0)
            //    //            {
            //    //                speciesRow.Description += ", ";
            //    //                speciesRow.Description += "sexes are ";
            //    //                foreach (object sex in sexes)
            //    //                {
            //    //                    speciesRow.Description += new Sex((int)sex).ToString("C") + ", ";
            //    //                }
            //    //            }
            //    //        }

            //    //        speciesRow.Description += "; ";
            //    //    }

            //    //    speciesRow.Description = speciesRow.Description.TrimEnd("; ".ToCharArray()) + ".";
            //    //}

            //    speciesKey.Species.AddSpeciesRow(speciesRow);
            //}

            //return speciesKey;
        }

        public static List<string> GetSpeciesList(this Data data)
        {
            List<string> result = new List<string>();

            foreach (Data.LogRow logRow in data.Log)
            {
                if (!result.Contains(logRow.DefinitionRow.Taxon))
                {
                    result.Add(logRow.DefinitionRow.Taxon);
                }
            }

            return result;
        }

        public static Data.DefinitionRow[] GetSpeciesForWeightRecovery(this Data data)
        {
            List<Data.DefinitionRow> result = new List<Data.DefinitionRow>();

            foreach (Data.LogRow logRow in data.Log)
            {
                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow))
                {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public static Data.DefinitionRow[] GetSpeciesWithUnweightedIndividuals(this Data data)
        {
            List<Data.DefinitionRow> result = new List<Data.DefinitionRow>();

            foreach (Data.IndividualRow individualRow in data.Individual)
            {
                if (!individualRow.IsMassNull()) continue;
                if (!result.Contains(individualRow.LogRow.DefinitionRow))
                {
                    result.Add(individualRow.LogRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }





        /// <summary>
        /// Find species log record and returns its abundance
        /// </summary>
        /// <param name="cardRow"></param>
        /// <param name="speciesRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Data.CardRow cardRow, Data.DefinitionRow speciesRow)
        {
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (logRow.DefinitionRow == speciesRow)
                {
                    return logRow.GetAbundance();
                }
            }

            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Data.LogRow logRow)
        {
            if (logRow.IsQuantityNull()) return double.NaN;
            if (logRow.CardRow.IsVolumeNull()) return double.NaN;

            return logRow.IsQuantityNull() ? (double)logRow.DetailedQuantity: (double)logRow.Quantity /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Volume;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Data.LogRow logRow)
        {
            if (logRow.CardRow.IsVolumeNull()) return double.NaN;

            return logRow.IsMassNull() ? logRow.DetailedMass: logRow.Mass /
                (logRow.IsSubsampleNull() ? 1.0 : logRow.Subsample) /
                logRow.CardRow.Volume;
        }
    }
}

