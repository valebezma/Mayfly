using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Reader;

namespace Mayfly.Extensions
{
    public static class DataExtensions
    {
        public static SpeciesKey GetSpeciesKey(this Data data)
        {
            SpeciesKey speciesKey = new SpeciesKey();

            foreach (Data.SpeciesRow dataSpcRow in data.Species)
            {
                SpeciesKey.SpeciesRow speciesRow = speciesKey.Species.NewSpeciesRow();

                speciesRow.Species = dataSpcRow.Species;

                //SpeciesKey.SpeciesRow equivalentRow = Benthos.UserSettings.SpeciesIndex.Species.FindBySpecies(
                //    dataSpcRow.Species);

                //if (equivalentRow != null)
                //{
                //    if (!equivalentRow.IsReferenceNull()) speciesRow.Reference = equivalentRow.Reference;
                //    if (!equivalentRow.IsLocalNull()) speciesRow.Local = equivalentRow.Local;
                //}

                //var logRows = dataSpcRow.GetLogRows();

                //if (logRows.Length > 0)
                //{
                //    speciesRow.Description = "Species collected: ";

                //    foreach (Data.LogRow logRow in logRows)
                //    {
                //        if (!logRow.CardRow.IsWaterIDNull())
                //        {
                //            speciesRow.Description += string.Format("at {0} ", logRow.CardRow.WaterRow.GetWaterFullName());
                //        }

                //        if (!logRow.CardRow.IsWhenNull())
                //        {
                //            speciesRow.Description += logRow.CardRow.When.ToString("d");
                //        }

                //        Meta.Numerics.Statistics.Sample lengths = data.Individual.LengthColumn.GetSample(logRow.GetIndividualRows());
                //        Meta.Numerics.Statistics.Sample masses = data.Individual.MassColumn.GetSample(logRow.GetIndividualRows());
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
            }

            return speciesKey;
        }
    }
}

