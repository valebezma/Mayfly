using Mayfly.Bacterioplankton;
using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using System;

namespace Mayfly.Bacterioplankton
{
    public static class DataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Suggested name for data card with extension</returns>
        public static string GetSuggestedName(this Data data)
        {
            return data.GetSuggestedName(UserSettings.Interface.Extension);
        }



        /// <summary>
        /// Find species log record and returns its abundance
        /// </summary>
        /// <param name="cardRow"></param>
        /// <param name="speciesRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Data.CardRow cardRow, Data.SpeciesRow speciesRow)
        {
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (logRow.SpeciesRow == speciesRow)
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
            if (logRow.CardRow.IsSquareNull()) return double.NaN;

            if (logRow.IsSubsampleNull()) return double.NaN;

            return (double)logRow.Quantity * ((logRow.CardRow.Square * 1000000) / logRow.Subsample) / logRow.CardRow.Volume;
            // For example:
            // 46 ind. * (1500 sq. um / (314.16 sq. mm * 1000000)) / 2 ml) = 
            // (46 ind. * 4.77465e-6) / 2 ml =
            // 2.19634e-4 ind. / 2 ml =
            // 1.09817e-4 ind. / ml

            // Which is
            // 109.817 ind. / l =
            // 109817 ind. / cub. m
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Data.LogRow logRow)
        {
            if (logRow.CardRow.IsVolumeNull()) return double.NaN;
            if (logRow.CardRow.IsSquareNull()) return double.NaN;

            if (logRow.IsSubsampleNull()) return double.NaN;

            return logRow.Mass * ((logRow.CardRow.Square) / logRow.Subsample) / logRow.CardRow.Volume;
        }
        
        public static Sample GetVolumes(this Data.LogRow logRow)
        {
                Sample volumes = new Sample();

                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsWidthNull()) continue;

                    volumes.Add(individualRow.GetVolume());
                }

                return volumes;
        }

        public static double GetMass(this Data.LogRow logRow)
        {
                Sample v = logRow.GetVolumes();
                if (v.Count == 0) return double.NaN;
                return logRow.DetailedQuantity* v.Mean * Bacterioplankton.UserSettings.Consistence;
        }



        public static double GetVolume(this Data.IndividualRow indRow)
        {
            if (indRow.IsWidthNull()) return double.NaN;

            double v = 0;

            if (indRow.IsLengthNull())
            {
                v = Math.PI * (4 / 3) * Math.Pow(indRow.Width * 0.5, 3);
            }
            else if (2 * indRow.Width > indRow.Length)
            {
                v = Math.PI * indRow.Length * indRow.Width * indRow.Width / 6.0;
            }
            else
            {
                v = Math.PI * indRow.Length * (indRow.Width * 0.5) * (indRow.Width * 0.5);
            }

            return v;
        }
    }
}

