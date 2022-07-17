using Mayfly.Extensions;
using Mayfly.Waters;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mayfly.Fish
{
    public static partial class DataExtensions
    {
        public static double GetSquare(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Square");
        }

        public static double GetHeight(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Height");
        }

        public static double GetLength(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Length");
        }

        public static double GetOpening(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Opening");
        }

        public static double GetVelocity(this Survey.EquipmentRow eqpRow) {
            return eqpRow.GetVirtue("Velocity");
        }

        public static double GetCoveredArea(this Survey.EquipmentRow eqpRow, int replications) {

            if (replications == -1) return double.NaN;

            return replications * eqpRow.GetSquare();
        }

        public static double GetExposureArea(this Survey.EquipmentRow eqpRow, double e) {

            if (double.IsNaN(e)) return double.NaN;

            double l = eqpRow.GetLength();
            double o = eqpRow.GetOpening();

            return double.IsNaN(o) ? (double.IsNaN(l) ? double.NaN : l * e) : o * e;
        }

        public static double GetOutscribedCircleArea(this Survey.EquipmentRow eqpRow) {

            double d = eqpRow.GetLength();
            return double.IsNaN(d) ? double.NaN : d * d * .25 * Math.PI;
        }

        public static double GetTowedArea(this Survey.EquipmentRow eqpRow, TimeSpan duration) {

            if (duration == TimeSpan.Zero) return double.NaN;

            double e = eqpRow.GetVelocity() * 1000 * duration.TotalHours;

            return eqpRow.GetExposureArea(e);
        }

        public static double GetSeinArea(this Survey.EquipmentRow eqpRow, WaterType waterType, double e) {

            if (double.IsNaN(e)) return double.NaN;

            if (waterType == WaterType.None) return eqpRow.GetExposureArea(e);

            double l = eqpRow.GetLength();
            if (double.IsNaN(l)) return double.NaN;

            double o = eqpRow.GetOpening();

            switch (waterType) {

                case WaterType.Stream:
                    if (double.IsNaN(o)) {
                        if (e < 2 * l / Math.PI) {
                            return double.NaN;
                        } else {
                            // Automatic effective opening of sein
                            double r = 2 * l / Math.PI;

                            // First and third segments. Sein opening and closing
                            double s1 = (Math.Pow(r, 2) * Math.PI) / 4;

                            // Second segment. Sein exposure
                            double s2 = r * (e - 2 * r);

                            return 2 * s1 + s2;
                        }
                    } else {
                        if (e < 2 * o / Math.PI) {
                            return double.NaN;
                        } else {
                            // First and last segment - opening and closure
                            double s1 = Math.Pow(o, 2) * Math.PI / 4;

                            // Seconde segment - exposure
                            double s2 = o * (e - 2 * o);

                            return 2 * s1 + s2;


                            //// First segment. Sein opening
                            //// With Huygens formulae
                            //double z = (6 * eqpRow.GetLength() + 2 * eqpRow.GetOpening()) / 8;
                            //double r = Math.Sqrt((z - eqpRow.GetOpening()) * (z + eqpRow.GetOpening()));
                            //double s1 = (r * eqpRow.GetOpening() * Math.PI) / 4;

                            //// Second segment. Sein exposure
                            //double s2 = eqpRow.GetOpening() * (exposure - eqpRow.GetOpening() - r);

                            //// Third segment. Sein closing
                            //double s3 = Math.Pow(eqpRow.GetOpening(), 2) * Math.PI / 4;

                            //return s1 + s2 + s3;
                        }
                    }

                case WaterType.Lake:
                case WaterType.Tank:
                    if (e < l) {
                        return l * e / 2;
                    } else {
                        double s = 0;
                        if (double.IsNaN(o)) {
                            // First segment. Sein exposure
                            s += l * (e - l);

                            // Second segment. Sein closing
                            s += Math.Pow(l, 2) / 2;
                        } else {
                            // First segment. Sein opening
                            // With Huygens formulae
                            double z = (3 * l + o) / 8;
                            double x = Math.Sqrt((z - o / 2) * (z + o / 2));
                            s += x * (l - o);

                            // Second segment. Sein exposure
                            s += o * (e - o);

                            // Third segment. Sein closing
                            s += o * o * .5;
                        }

                        return s;
                    }
            }

            return double.NaN;
        }

        public static double GetDriftArea(this Survey.EquipmentRow eqpRow, double e) {

            if (double.IsNaN(e)) return double.NaN;

            double l = eqpRow.GetLength();
            double o = eqpRow.GetOpening();

            if (!double.IsNaN(o)) {

                if (!double.IsNaN(l)) {
                    // First segment. Net opening
                    // With Huygens formulae
                    double z = (3 * l + o) / 8;
                    double x = Math.Sqrt((z - o / 2) * (z + o / 2));
                    double s = x * (l - o);

                    // Second segment. Net exposure
                    s += o * e;

                    return s;
                } else {
                    // Only segment
                    return e * o;
                }
            }

            return double.NaN;
        }

        public static double GetArea(this Survey.CardRow cardRow) {

            if (!cardRow.IsEffortNull() && cardRow.Effort < 0) return -cardRow.Effort;

            if (cardRow.IsEqpIDNull()) return double.NaN;

            switch (cardRow.SamplerRow.GetSamplerType()) {

                case FishSamplerType.Driftnet:
                    return cardRow.EquipmentRow.GetDriftArea(cardRow.Exposure);

                case FishSamplerType.Gillnet:
                case FishSamplerType.Trap:
                case FishSamplerType.Hook:
                    return cardRow.EquipmentRow.GetOutscribedCircleArea() * cardRow.Duration.TotalDays;

                case FishSamplerType.LiftNet:
                case FishSamplerType.FallingGear:
                    return cardRow.EquipmentRow.GetCoveredArea(cardRow.Portions);

                case FishSamplerType.Sein:
                    return cardRow.EquipmentRow.GetSeinArea(cardRow.IsWaterIDNull() ? WaterType.None : (WaterType)cardRow.WaterRow.Type, cardRow.Exposure);

                case FishSamplerType.Dredge:
                case FishSamplerType.Trawl:
                    return cardRow.EquipmentRow.GetTowedArea(cardRow.Duration);

                case FishSamplerType.Electrofishing:
                    return cardRow.EquipmentRow.GetExposureArea(cardRow.Exposure);

                case FishSamplerType.SurroundingNet:
                    return cardRow.EquipmentRow.GetOutscribedCircleArea();
            }

            return double.NaN;
        }


        public static double GetVolume(this Survey.CardRow cardRow) {

            double s = cardRow.GetArea();
            if (double.IsNaN(s)) return double.NaN;
            double h = cardRow.IsDepthNull() ? cardRow.EquipmentRow.GetHeight() : Math.Min(cardRow.Depth, cardRow.EquipmentRow.GetHeight());
            return h * s;
        }


        public static double GetStandards(this Survey.CardRow cardRow) {

            if (cardRow.IsEqpIDNull()) return double.NaN;

            switch (cardRow.SamplerRow.GetSamplerType()) {

                case FishSamplerType.Gillnet:
                    return cardRow.EquipmentRow.GetLength() * cardRow.EquipmentRow.GetHeight() * cardRow.Duration.TotalHours;

                case FishSamplerType.Hook:
                    return cardRow.Duration.TotalHours;
            }

            return double.NaN;
        }


        public static double GetEffort(this Survey.CardRow cardRow, EffortExpression ev) {

            if (cardRow.IsEqpIDNull()) {
                return double.NaN;
            } else {
                switch (ev) {
                    case EffortExpression.Area:
                        return cardRow.GetArea() / UnitEffort.SquareUnitCost;

                    case EffortExpression.Volume:
                        return cardRow.GetVolume() / UnitEffort.VolumeUnitCost;

                    case EffortExpression.Standards:
                        return cardRow.GetStandards() / cardRow.SamplerRow.GetSamplerType().GetEffortStdScore();
                }
            }

            return double.NaN;
        }

        public static double GetEffort(this Survey.CardRow cardRow) {

            if (cardRow.IsEqpIDNull()) return double.NaN;
            return GetEffort(cardRow, cardRow.SamplerRow.GetSamplerType().GetDefaultExpression());
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Survey.LogRow logRow) {

            if (logRow.IsQuantityNull()) return double.NaN;
            return logRow.Quantity / logRow.CardRow.GetEffort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Survey.LogRow logRow, EffortExpression variant) {

            if (logRow.IsQuantityNull()) return double.NaN;
            return logRow.Quantity / logRow.CardRow.GetEffort(variant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Survey.LogRow logRow) {

            if (logRow.IsMassNull()) return double.NaN;
            return logRow.Mass / logRow.CardRow.GetEffort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Survey.LogRow logRow, EffortExpression variant) {

            if (logRow.IsMassNull()) return double.NaN;
            return logRow.Mass / logRow.CardRow.GetEffort(variant);
        }

        //public static SpeciesKey GetSpeciesKey(this Data.DefinitionRow[] dataSpcRows)
        //{
        //    SpeciesKey speciesKey = new SpeciesKey();

        //    if (dataSpcRows.Length == 0) return speciesKey;

        //    Data data = (Data)dataSpcRows[0].Table.DataSet;

        //    foreach (Data.DefinitionRow dataSpcRow in dataSpcRows)
        //    {
        //        SpeciesKey.TaxonRow speciesRow = speciesKey.Species.NewSpeciesRow();

        //        speciesRow.Species = dataSpcRow.Species;

        //        SpeciesKey.TaxonRow equivalentRow = Benthos.UserSettings.SpeciesIndex.Definition.FindByName(
        //            dataSpcRow.Species);

        //        if (equivalentRow != null)
        //        {
        //            if (!equivalentRow.IsReferenceNull()) speciesRow.Reference = equivalentRow.Reference;
        //            if (!equivalentRow.IsNameNull()) speciesRow.Name = equivalentRow.Name;
        //        }

        //        var logRows = dataSpcRow.GetLogRows();

        //        if (logRows.Length > 0)
        //        {
        //            speciesRow.Description = "Species collected: ";

        //            foreach (Data.LogRow logRow in logRows)
        //            {
        //                if (!logRow.CardRow.IsWaterIDNull())
        //                {
        //                    speciesRow.Description += string.Format("at {0} ", logRow.CardRow.WaterRow.Presentation);
        //                }

        //                if (!logRow.CardRow.IsWhenNull())
        //                {
        //                    speciesRow.Description += logRow.CardRow.When.ToString("d");
        //                }

        //                Sample lengths = data.Individual.LengthColumn.GetSample(logRow.GetIndividualRows());
        //                Sample masses = data.Individual.MassColumn.GetSample(logRow.GetIndividualRows());
        //                List<object> sexes = data.Individual.SexColumn.GetValues(logRow.GetIndividualRows(), true);

        //                if (lengths.Count + masses.Count + sexes.Count > 0)
        //                {
        //                    speciesRow.Description += ": ";

        //                    if (lengths.Count > 0)
        //                    {
        //                        speciesRow.Description += string.Format("L = {0} mm", lengths.ToString("E"));
        //                    }

        //                    if (masses.Count > 0)
        //                    {
        //                        speciesRow.Description += ", ";
        //                        speciesRow.Description += string.Format("W = {0} mg", masses.ToString("E"));
        //                    }

        //                    if (sexes.Count > 0)
        //                    {
        //                        speciesRow.Description += ", ";
        //                        speciesRow.Description += "sexes are ";
        //                        foreach (object sex in sexes)
        //                        {
        //                            speciesRow.Description += new Sex((int)sex).ToString("C") + ", ";
        //                        }
        //                    }
        //                }

        //                speciesRow.Description += "; ";
        //            }

        //            speciesRow.Description = speciesRow.Description.TrimEnd("; ".ToCharArray()) + ".";
        //        }

        //        speciesKey.Species.AddSpeciesRow(speciesRow);

        //        //if (Species.UserSettings.VisualConfirmationOnAutoUpdate)
        //        //{
        //        //    AddAutoSpecies editSpecies = new AddAutoSpecies(speciesRow);

        //        //    if (editSpecies.ShowDialog() == DialogResult.OK)
        //        //    {
        //        //        speciesKey.Species.AddSpeciesRow(speciesRow);

        //        //        //foreach (SpeciesKey.TaxonRow taxonRow in editSpecies.SelectedTaxon)
        //        //        //{
        //        //        //    speciesKey.Species.AddSpeciesRow(speciesRow);
        //        //        //    dest.Rep.AddRepRow(taxonRow, destRow);
        //        //        //}
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    speciesKey.Species.AddSpeciesRow(speciesRow);
        //        //}
        //    }

        //    return speciesKey;
        //}



        public static string GetDescription(this Survey.IndividualRow indRow) {

            List<string> result = new List<string>();
            result.Add(indRow.LogRow.DefinitionRow.KeyRecord.CommonName);
            if (!indRow.IsTallyNull()) result.Add(string.Format("#{0}", indRow.Tally));
            if (!indRow.IsLengthNull()) result.Add(string.Format("L = {0}", indRow.Length));
            if (!indRow.IsMassNull()) result.Add(string.Format("W = {0}", indRow.Mass));
            if (!indRow.IsAgeNull()) result.Add(string.Format("A = {0}", (Age)indRow.Age));
            return result.Merge();
        }

        public static string GetDescription(this Survey.LogRow logRow) {

            return string.Format(Wild.Resources.Interface.Interface.LogMask, logRow.DefinitionRow.KeyRecord, logRow.CardRow);
        }


        public static double GetTotalLength(this Survey.IndividualRow indRow) {
            return indRow.GetAddtValue("TL");
        }

        public static double GetCondition(this Survey.IndividualRow indRow) {
            //if (indRow.IsLengthNull()) return double.NaN;
            //if (indRow.IsMassNull()) return double.NaN;
            //return (100.0 * indRow.Mass) / Math.Pow(indRow.Length / 10.0, 3.0);
            if (indRow.IsMassNull()) return double.NaN;
            Survey data = (Survey)indRow.Table.DataSet;
            ContinuousBio cb = data.FindMassModel(indRow.Species);
            if (cb == null) return double.NaN;
            double wm = cb.GetValue(indRow.Length);
            return indRow.Mass / wm;
        }

        public static double GetConditionSomatic(this Survey.IndividualRow indRow) {
            //if (indRow.IsLengthNull()) return double.NaN;
            //if (indRow.IsSomaticMassNull()) return double.NaN;
            //return (100.0 * indRow.SomaticMass) / Math.Pow(indRow.Length / 10.0, 3.0);

            return double.NaN;
        }


        public static double GetRelativeFecundity(this Survey.IndividualRow indRow) {
            if (indRow.IsMassNull()) return double.NaN;
            return indRow.GetAbsoluteFecundity() / indRow.Mass;
        }

        public static double GetRelativeFecunditySomatic(this Survey.IndividualRow indRow) {
            if (indRow.IsSomaticMassNull()) return double.NaN;
            return indRow.GetAbsoluteFecundity() / indRow.SomaticMass;
        }

        public static double GetAbsoluteFecundity(this Survey.IndividualRow indRow) {
            if (indRow.IsGonadMassNull()) return double.NaN;
            if (indRow.IsGonadSampleNull()) return double.NaN;
            if (indRow.IsGonadSampleMassNull()) return double.NaN;
            return indRow.GonadMass * (indRow.GonadSample / indRow.GonadSampleMass);
        }

        public static double GetGonadIndex(this Survey.IndividualRow indRow) {
            if (indRow.IsGonadMassNull()) return double.NaN;
            if (indRow.IsMassNull()) return double.NaN;
            return indRow.GonadMass / indRow.Mass;
        }

        public static double GetGonadIndexSomatic(this Survey.IndividualRow indRow) {
            if (indRow.IsGonadMassNull()) return double.NaN;
            if (indRow.IsSomaticMassNull()) return double.NaN;
            return indRow.GonadMass / indRow.SomaticMass;
        }

        public static double GetAveEggMass(this Survey.IndividualRow indRow) {
            if (indRow.IsGonadSampleMassNull()) return double.NaN;
            if (indRow.IsGonadSampleNull()) return double.NaN;
            return indRow.GonadSampleMass / indRow.GonadSample;
        }


        public static double GetConsumptionIndex(this Survey.IndividualRow indRow) {
            if (indRow.IsConsumedMassNull()) return double.NaN;
            if (indRow.IsMassNull()) return double.NaN;
            return indRow.ConsumedMass / indRow.Mass * 10;
        }

        public static int GetDietItemCount(this Survey.IndividualRow indRow) {
            if (indRow.IsDietPresented()) {
                return indRow.GetConsumed().Definition.Count;
            }

            return -1;
        }

        public static bool IsDietPresented(this Survey.IndividualRow indRow) {
            return (indRow.GetIntestineRows().Length > 0);
        }

        public static Survey GetConsumed(this Survey.IndividualRow indRow) {
            return indRow.GetConsumed(false);
        }

        public static Survey GetConsumed(this Survey.IndividualRow indRow, bool pool) {
            Survey result = new Survey();

            foreach (Survey.IntestineRow intRow in indRow.GetIntestineRows()) {
                Survey intData = intRow.GetConsumed();

                if (pool) {
                    if (result.Card.Count == 0) { result = intData; } else { intData.Solitary.CopyLogTo(result.Solitary); }
                } else {
                    intData.CopyTo(result);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns intestine content as a new Data unit 
        /// </summary>
        /// <param name="intRow"></param>
        /// <returns></returns>
        public static Survey GetConsumed(this Survey.IntestineRow intRow) {
            Survey.IndividualRow indRow = intRow.IndividualRow;

            Survey result = new Survey();

            if (!intRow.IsConsumedNull()) { result.ReadXml(new StringReader(intRow.Consumed)); }

            //result.Solitary.Sampler = 0;

            if (indRow.IsTallyNull()) { result.Solitary.SetLabelNull(); } else { result.Solitary.Label = indRow.Tally; }

            //if (indRow.IsMassNull()) { result.Solitary.SetSquareNull(); } else { result.Solitary.Square = indRow.Mass; }

            if (indRow.LogRow.CardRow.IsWhenNull()) { result.Solitary.SetWhenNull(); } else { result.Solitary.When = indRow.LogRow.CardRow.When; }

            if (indRow.LogRow.CardRow.IsWaterIDNull() || indRow.LogRow.CardRow.WaterRow.IsWaterNull()) {
                result.Solitary.SetWaterIDNull();
            } else {
                Survey.WaterRow waterRow = result.Water.NewWaterRow();
                waterRow.Type = indRow.LogRow.CardRow.WaterRow.Type;
                waterRow.Water = indRow.LogRow.CardRow.WaterRow.Water;
                result.Water.AddWaterRow(waterRow);
                result.Solitary.WaterRow = waterRow;
            }

            result.Solitary.Comments = indRow.GetDescription();

            if (!indRow.IsLengthNull()) {
                Survey.FactorRow lengthFactor = result.Factor.FindByFactor(Wild.Resources.Reports.Caption.LengthUnit);
                if (lengthFactor == null) lengthFactor = result.Factor.AddFactorRow(Wild.Resources.Reports.Caption.LengthUnit);

                Survey.FactorValueRow factorValueRow = result.FactorValue.FindByCardIDFactorID(result.Solitary.ID, lengthFactor.ID);
                if (factorValueRow == null) factorValueRow = result.FactorValue.AddFactorValueRow(result.Solitary, lengthFactor, indRow.Length);
                factorValueRow.Value = indRow.Length;
            }

            if (!indRow.IsAgeNull()) {
                Survey.FactorRow ageFactor = result.Factor.FindByFactor(Wild.Resources.Reports.Caption.AgeUnit);
                if (ageFactor == null) ageFactor = result.Factor.AddFactorRow(Wild.Resources.Reports.Caption.AgeUnit);

                Survey.FactorValueRow factorValueRow = result.FactorValue.FindByCardIDFactorID(result.Solitary.ID, ageFactor.ID);
                if (factorValueRow == null) factorValueRow = result.FactorValue.AddFactorValueRow(result.Solitary, ageFactor, indRow.Age);
                factorValueRow.Value = indRow.Age;
            }

            result.Solitary.ID = intRow.ID;
            result.ClearUseless();
            return result;
        }



        public static Survey GetInfection(this Survey.OrganRow organRow) {
            if (organRow.IsInfectionNull()) return null;

            Survey result = new Survey();
            result.ReadXml(new StringReader(organRow.Infection));

            if (!organRow.IndividualRow.IsTallyNull()) {
                result.Solitary.Label = organRow.IndividualRow.Tally;
            }

            result.Solitary.Comments = organRow.IndividualRow.GetDescription();

            return result;
        }
    }
}