using Mayfly.Species;
using Mayfly.Wild;
using System.Collections.Generic;
using System;
using Mayfly.Waters;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Fish
{
    public static partial class DataExtensions
    {
        public static string GetSuggestedName(this Data data)
        {
            return data.GetSuggestedName(
                UserSettings.Interface.Extension,
                data.Solitary.GetSamplerSign(false));
        }

        public static SpeciesKey GetSpeciesKey(this Data data)
        {
            SpeciesKey speciesKey = new SpeciesKey();

            foreach (Data.SpeciesRow dataSpcRow in data.Species)
            {
                SpeciesKey.SpeciesRow speciesRow = speciesKey.Species.NewSpeciesRow();

                speciesRow.Species = dataSpcRow.Species;

                SpeciesKey.SpeciesRow equivalentRow = dataSpcRow.KeyRecord;

                if (equivalentRow != null)
                {
                    if (!equivalentRow.IsReferenceNull()) speciesRow.Reference = equivalentRow.Reference;
                    if (!equivalentRow.IsNameNull()) speciesRow.Name = equivalentRow.Name;
                }

                var logRows = dataSpcRow.GetLogRows();

                if (logRows.Length > 0)
                {
                    speciesRow.Description = "Species collected: ";

                    foreach (Data.LogRow logRow in logRows)
                    {
                        if (!logRow.CardRow.IsWaterIDNull())
                        {
                            speciesRow.Description += string.Format("at {0} ", logRow.CardRow.WaterRow.Presentation);
                        }

                        if (!logRow.CardRow.IsWhenNull())
                        {
                            speciesRow.Description += logRow.CardRow.When.ToString("d");
                        }

                        //Sample lengths = data.Individual.LengthColumn.GetSample(logRow.GetIndividualRows());
                        //Sample masses = data.Individual.MassColumn.GetSample(logRow.GetIndividualRows());
                        //List<object> sexes = data.Individual.SexColumn.GetValues(logRow.GetIndividualRows(), true);

                        //if (lengths.Count + masses.Count + sexes.Count > 0)
                        //{
                        //    speciesRow.Description += ": ";

                        //    if (lengths.Count > 0)
                        //    {
                        //        speciesRow.Description += string.Format("L = {0} mm", lengths.ToString("E"));
                        //    }

                        //    if (masses.Count > 0)
                        //    {
                        //        speciesRow.Description += ", ";
                        //        speciesRow.Description += string.Format("W = {0} mg", masses.ToString("E"));
                        //    }

                        //    if (sexes.Count > 0)
                        //    {
                        //        speciesRow.Description += ", ";
                        //        speciesRow.Description += "sexes are ";
                        //        foreach (object sex in sexes)
                        //        {
                        //            speciesRow.Description += new Sex((int)sex).ToString("C") + ", ";
                        //        }
                        //    }
                        //}

                        speciesRow.Description += "; ";
                    }

                    speciesRow.Description = speciesRow.Description.TrimEnd("; ".ToCharArray()) + ".";
                }

                speciesKey.Species.AddSpeciesRow(speciesRow);
            }

            return speciesKey;
        }



        public static Samplers.SamplerRow GetSamplerRow(this Data.CardRow cardRow)
        {
            return cardRow.GetSamplerRow(Fish.UserSettings.SamplersIndex);
        }

        public static FishSamplerType GetGearType(this Data.CardRow cardRow)
        {
            return cardRow.IsSamplerNull() ? FishSamplerType.None : cardRow.GetSamplerRow().GetSamplerType();
        }

        public static string GetSamplerSign(this Data.CardRow cardRow) => cardRow.GetSamplerSign(true);

        public static string GetSamplerSign(this Data.CardRow cardRow, bool full)
        {
            Samplers.SamplerRow samplerRow = cardRow.GetSamplerRow();
            string result = full ? samplerRow.Sampler : samplerRow.ShortName;
            result += " " + cardRow.GetGearClass();
            return result;
        }

        public static double GetEffort(this Data.CardRow cardRow)
        {
            if (cardRow.IsSamplerNull()) return double.NaN;
            return GetEffort(cardRow, cardRow.GetGearType().GetDefaultExpression());
        }

        public static double GetEffort(this Data.CardRow cardRow, ExpressionVariant expression)
        {
            if (cardRow.IsSamplerNull()) return double.NaN;
            else
                switch (expression)
                {
                    case ExpressionVariant.Square:
                        return cardRow.GetSquare() / UnitEffort.SquareUnitCost;
                    case ExpressionVariant.Volume:
                        return cardRow.GetVolume() / UnitEffort.VolumeUnitCost;
                    case ExpressionVariant.Efforts:
                        return cardRow.GetEffortScore() / cardRow.GetGearType().GetEffortStdScore();
                }

            return double.NaN;
        }

        public static double GetExposure(this Data.CardRow cardRow)
        {
            if (cardRow.IsVelocityNull()) return double.NaN;
            if (cardRow.IsSpanNull()) return double.NaN;

            return cardRow.Velocity * cardRow.Duration.TotalHours * 1000;
        }

        public static double GetSquare(this Data.CardRow cardRow)
        {
            if (!cardRow.IsExactAreaNull()) return cardRow.ExactArea;

            if (cardRow.IsSamplerNull()) return double.NaN;

            if (cardRow.GetSamplerRow().IsEffortFormulaNull()) return double.NaN;

            switch (cardRow.GetSamplerRow().EffortFormula)
            {
                case "EL":
                    if (!cardRow.IsLengthNull() && !cardRow.IsExposureNull())
                    {
                        return cardRow.Length * cardRow.Exposure;
                    }
                    break;
                case "MLH":
                    if (!cardRow.IsLengthNull())
                    {
                        return Math.Pow(cardRow.Length, 2) / (4 * Math.PI);
                    }
                    break;
                case "MTLH":
                    if (cardRow.Sampler == 730)
                    {
                        // Encircling gillnet

                        //if (!_this.IsLengthNull())
                        //{
                        //    Square = Math.Pow(_this.Length, 2) / (4 * Math.PI);
                        //}

                    }
                    else
                    {
                        // Except 730
                        if (!cardRow.IsLengthNull() && !cardRow.IsSpanNull())
                        {
                            return Math.PI * 0.25 * Math.Pow(cardRow.Length, 2) * cardRow.Duration.TotalDays;
                        }
                    }
                    break;

                case "MTVELOH":
                    if (!cardRow.IsOpeningNull() && !cardRow.IsVelocityNull() &&
                        !cardRow.IsSpanNull())
                    {
                        return cardRow.Duration.TotalHours * cardRow.Velocity * 1000 * cardRow.Opening;
                    }
                    break;

                case "MTVELH":
                    if (!cardRow.IsLengthNull() && !cardRow.IsVelocityNull() &&
                        !cardRow.IsSpanNull())
                    {
                        return cardRow.Duration.TotalHours * cardRow.Velocity * 1000 * cardRow.Length;
                    }
                    break;

                case "MS":
                    if (!cardRow.IsSquareNull())
                    {
                        return cardRow.Square;
                    }
                    break;

                case "MELOH":
                case "MELO":
                case "ELO":
                    switch (cardRow.Sampler)
                    {
                        case 720: // driftnet
                            if (!cardRow.IsOpeningNull() && !cardRow.IsLengthNull() && !cardRow.IsExposureNull())
                            {
                                // First segment. Net opening
                                // With Huygens formulae
                                double z = (3 * cardRow.Length + cardRow.Opening) / 8;
                                double x = Math.Sqrt((z - cardRow.Opening / 2) * (z + cardRow.Opening / 2));
                                double s = x * (cardRow.Length - cardRow.Opening);

                                // Second segment. Net exposure
                                s += cardRow.Opening * cardRow.Exposure;

                                return s;
                            }
                            else if (!cardRow.IsOpeningNull() && !cardRow.IsExposureNull())
                            {
                                // Only segment
                                return cardRow.Exposure * cardRow.Opening;
                            }
                            break;

                        default: // all sein nets
                            if (cardRow.IsWaterIDNull())
                            {
                                if (cardRow.IsExposureNull()) return double.NaN;

                                if (cardRow.IsOpeningNull()) { return cardRow.IsLengthNull() ? double.NaN : cardRow.Length * cardRow.Exposure; }
                                else { return cardRow.Opening * cardRow.Exposure; }
                            }
                            else
                            {
                                switch ((WaterType)cardRow.WaterRow.Type)
                                {
                                    case WaterType.Stream:
                                        if (!cardRow.IsLengthNull() && !cardRow.IsExposureNull())
                                        {
                                            if (cardRow.IsOpeningNull())
                                            {
                                                if (cardRow.Exposure < 2 * cardRow.Length / Math.PI)
                                                {
                                                    return double.NaN;
                                                }
                                                else
                                                {
                                                    // Automatic effective opening of sein
                                                    double r = 2 * cardRow.Length / Math.PI;

                                                    // First and third segments. Sein opening and closing
                                                    double s1 = (Math.Pow(r, 2) * Math.PI) / 4;

                                                    // Second segment. Sein exposure
                                                    double s2 = r * (cardRow.Exposure - 2 * r);

                                                    return 2 * s1 + s2;
                                                }
                                            }
                                            else
                                            {
                                                if (cardRow.Exposure < 2 * cardRow.Opening / Math.PI)
                                                {
                                                    return double.NaN;
                                                }
                                                else
                                                {
                                                    // First and last segment - opening and closure
                                                    double s1 = Math.Pow(cardRow.Opening, 2) * Math.PI / 4;

                                                    // Seconde segment - exposure
                                                    double s2 = cardRow.Opening * (cardRow.Exposure - 2 * cardRow.Opening);

                                                    return 2 * s1 + s2;


                                                    //// First segment. Sein opening
                                                    //// With Huygens formulae
                                                    //double z = (6 * cardRow.Length + 2 * cardRow.Opening) / 8;
                                                    //double r = Math.Sqrt((z - cardRow.Opening) * (z + cardRow.Opening));
                                                    //double s1 = (r * cardRow.Opening * Math.PI) / 4;

                                                    //// Second segment. Sein exposure
                                                    //double s2 = cardRow.Opening * (cardRow.Exposure - cardRow.Opening - r);

                                                    //// Third segment. Sein closing
                                                    //double s3 = Math.Pow(cardRow.Opening, 2) * Math.PI / 4;

                                                    //return s1 + s2 + s3;
                                                }
                                            }
                                        }
                                        break;
                                    case WaterType.Lake:
                                    case WaterType.Tank:
                                        if (!cardRow.IsLengthNull() && !cardRow.IsExposureNull())
                                        {
                                            if (cardRow.Exposure < cardRow.Length)
                                            {
                                                return cardRow.Length * cardRow.Exposure / 2;
                                            }
                                            else
                                            {
                                                double s = 0;
                                                if (cardRow.IsOpeningNull())
                                                {
                                                    // First segment. Sein exposure
                                                    s += cardRow.Length * (cardRow.Exposure - cardRow.Length);

                                                    // Second segment. Sein closing
                                                    s += Math.Pow(cardRow.Length, 2) / 2;
                                                }
                                                else
                                                {
                                                    // First segment. Sein opening
                                                    // With Huygens formulae
                                                    double z = (3 * cardRow.Length + cardRow.Opening) / 8;
                                                    double x = Math.Sqrt((z - cardRow.Opening / 2) * (z + cardRow.Opening / 2));
                                                    s += x * (cardRow.Length - cardRow.Opening);

                                                    // Second segment. Sein exposure
                                                    s += cardRow.Opening * (cardRow.Exposure - cardRow.Opening);

                                                    // Third segment. Sein closing
                                                    s += Math.Pow(cardRow.Opening, 2) / 2;
                                                }

                                                return s;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                    break;

                case "MELH":
                    if (!cardRow.IsLengthNull() && !cardRow.IsExposureNull())
                    {
                        return cardRow.Exposure * cardRow.Length;
                    }
                    break;
            }

            return double.NaN;
        }

        public static double GetVolume(this Data.CardRow cardRow)
        {
            if (cardRow.IsDepthNull())
            {
                if (cardRow.IsHeightNull())
                {
                    return double.NaN;
                }
                else
                {
                    return cardRow.GetSquare() * cardRow.Height;
                }
            }
            else
            {
                if (cardRow.IsHeightNull())
                {
                    return cardRow.GetSquare() * cardRow.Depth;
                }
                else
                {
                    return cardRow.GetSquare() * Math.Min(cardRow.Depth, cardRow.Height);
                }
            }
        }

        public static double GetEffortScore(this Data.CardRow cardRow)
        {
            double result = double.NaN;
            if (cardRow.IsSamplerNull()) return result;
            if (cardRow.GetSamplerRow().IsEffortFormulaNull()) return result;

            switch (cardRow.GetSamplerRow().EffortFormula)
            {
                case "MTLH":
                    if (!cardRow.IsLengthNull() && !cardRow.IsHeightNull() && !cardRow.IsSpanNull())
                    {
                        result = (cardRow.Length * cardRow.Height * cardRow.Duration.TotalHours);
                    }
                    break;

                case "TJ":
                case "T":
                    if (!cardRow.IsSpanNull())
                    {
                        result = cardRow.Duration.TotalHours;
                    }
                    break;
            }

            return result;
        }

        public static string GetGearClass(this Data.CardRow cardRow)
        {
            if (cardRow.GetSamplerRow().EffortFormula.Contains("M")) {
                if (cardRow.IsMeshNull()) return string.Empty;
                return cardRow.Mesh.ToString("◊ 0");
            } else if (cardRow.GetSamplerRow().EffortFormula.Contains("J")) {
                if (cardRow.IsHookNull()) return string.Empty;
                return cardRow.Hook.ToString("ʔ 0");
            } else {
                return cardRow.When.ToString("yyyy MMMM");
            }

            //return string.Empty;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Data.LogRow logRow)
        {
            if (logRow.IsQuantityNull()) return double.NaN;
            return (double)logRow.Quantity / logRow.CardRow.GetEffort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Quantity per cubic meter in individuals</returns>
        public static double GetAbundance(this Data.LogRow logRow, ExpressionVariant variant)
        {
            if (logRow.IsQuantityNull()) return double.NaN;
            return (double)logRow.Quantity / logRow.CardRow.GetEffort(variant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Data.LogRow logRow)
        {
            if (logRow.IsMassNull()) return double.NaN;

            return logRow.Mass / logRow.CardRow.GetEffort();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns>Mass per cubic meter in grams</returns>
        public static double GetBiomass(this Data.LogRow logRow, ExpressionVariant variant)
        {
            if (logRow.IsMassNull()) return double.NaN;

            return logRow.Mass / logRow.CardRow.GetEffort(variant);
        }

        //public static SpeciesKey GetSpeciesKey(this Data.SpeciesRow[] dataSpcRows)
        //{
        //    SpeciesKey speciesKey = new SpeciesKey();

        //    if (dataSpcRows.Length == 0) return speciesKey;

        //    Data data = (Data)dataSpcRows[0].Table.DataSet;

        //    foreach (Data.SpeciesRow dataSpcRow in dataSpcRows)
        //    {
        //        SpeciesKey.SpeciesRow speciesRow = speciesKey.Species.NewSpeciesRow();

        //        speciesRow.Species = dataSpcRow.Species;

        //        SpeciesKey.SpeciesRow equivalentRow = Benthos.UserSettings.SpeciesIndex.Species.FindBySpecies(
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

        //        //        //foreach (SpeciesKey.TaxaRow taxaRow in editSpecies.SelectedTaxa)
        //        //        //{
        //        //        //    speciesKey.Species.AddSpeciesRow(speciesRow);
        //        //        //    dest.Rep.AddRepRow(taxaRow, destRow);
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



        public static string GetDescription(this Data.IndividualRow indRow)
        {
            string result = string.Empty;
            result += indRow.Species;
            if (!indRow.IsRegIDNull()) result += string.Format(" #{0}: ", indRow.RegID);
            if (!indRow.IsLengthNull()) result += string.Format(" L={0};", indRow.Length);
            if (!indRow.IsMassNull()) result += string.Format(" W={0};", indRow.Mass);
            if (!indRow.IsAgeNull()) result += string.Format(" A={0}", (Age)indRow.Age);
            return result;
        }


        public static double GetTotalLength(this Data.IndividualRow indRow)
        {
            return indRow.GetAddtValue("TL");
        }

        public static double GetCondition(this Data.IndividualRow indRow)
        {
            //if (indRow.IsLengthNull()) return double.NaN;
            //if (indRow.IsMassNull()) return double.NaN;
            //return (100.0 * indRow.Mass) / Math.Pow(indRow.Length / 10.0, 3.0);
            if (indRow.IsMassNull()) return double.NaN;
            Data data = (Data)indRow.Table.DataSet;
            if (data.MassModels == null) return double.NaN;
            double wm = data.FindMassModel(indRow.Species).GetValue(indRow.Length);
            return indRow.Mass / wm;
        }

        public static double GetConditionSomatic(this Data.IndividualRow indRow)
        {
            //if (indRow.IsLengthNull()) return double.NaN;
            //if (indRow.IsSomaticMassNull()) return double.NaN;
            //return (100.0 * indRow.SomaticMass) / Math.Pow(indRow.Length / 10.0, 3.0);

            return double.NaN;
        }


        public static double GetRelativeFecundity(this Data.IndividualRow indRow)
        {
            if (indRow.IsMassNull()) return double.NaN;
            return indRow.GetAbsoluteFecundity() / indRow.Mass;
        }

        public static double GetRelativeFecunditySomatic(this Data.IndividualRow indRow)
        {
            if (indRow.IsSomaticMassNull()) return double.NaN;
            return indRow.GetAbsoluteFecundity() / indRow.SomaticMass;
        }

        public static double GetAbsoluteFecundity(this Data.IndividualRow indRow)
        {
            if (indRow.IsGonadMassNull()) return double.NaN;
            if (indRow.IsGonadSampleNull()) return double.NaN;
            if (indRow.IsGonadSampleMassNull()) return double.NaN;
            return indRow.GonadMass * (indRow.GonadSample / indRow.GonadSampleMass);
        }

        public static double GetGonadIndex(this Data.IndividualRow indRow)
        {
            if (indRow.IsGonadMassNull()) return double.NaN;
            if (indRow.IsMassNull()) return double.NaN;
            return indRow.GonadMass / indRow.Mass;
        }

        public static double GetGonadIndexSomatic(this Data.IndividualRow indRow)
        {
            if (indRow.IsGonadMassNull()) return double.NaN;
            if (indRow.IsSomaticMassNull()) return double.NaN;
            return indRow.GonadMass / indRow.SomaticMass;
        }

        public static double GetAveEggMass(this Data.IndividualRow indRow)
        {
            if (indRow.IsGonadSampleMassNull()) return double.NaN;
            if (indRow.IsGonadSampleNull()) return double.NaN;
            return indRow.GonadSampleMass / indRow.GonadSample;
        }


        public static double GetConsumptionIndex(this Data.IndividualRow indRow)
        {
            if (indRow.IsConsumedMassNull()) return double.NaN;
            if (indRow.IsMassNull()) return double.NaN;
            return indRow.ConsumedMass / indRow.Mass * 10;
        }

        public static int GetDietItemCount(this Data.IndividualRow indRow)
        {
            if (indRow.IsDietPresented()) {
                return indRow.GetConsumed().Species.Count;
            }

            return -1;
        }

        public static bool IsDietPresented(this Data.IndividualRow indRow)
        {
            return (indRow.GetIntestineRows().Length > 0);
        }

        public static Data GetConsumed(this Data.IndividualRow indRow)
        {
            return indRow.GetConsumed(false);
        }

        public static Data GetConsumed(this Data.IndividualRow indRow, bool pool)
        {
            Data result = new Data();

            foreach (Data.IntestineRow intRow in indRow.GetIntestineRows())
            {
                Data intData = intRow.GetConsumed();

                if (pool)
                {
                    if (result.Card.Count == 0) { result = intData; }
                    else { intData.Solitary.CopyLogTo(result.Solitary); }
                }
                else
                {
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
        public static Data GetConsumed(this Data.IntestineRow intRow)
        {
            Data.IndividualRow indRow = intRow.IndividualRow;

            Data result = new Data();

            if (!intRow.IsConsumedNull()) { result.ReadXml(new StringReader(intRow.Consumed)); }

            result.Solitary.Sampler = 0;

            if (indRow.IsRegIDNull()) { result.Solitary.SetLabelNull(); }
            else { result.Solitary.Label = indRow.RegID; }

            if (indRow.IsMassNull()) { result.Solitary.SetSquareNull(); }
            else { result.Solitary.Square = indRow.Mass; }

            if (indRow.LogRow.CardRow.IsWhenNull()) { result.Solitary.SetWhenNull(); }
            else { result.Solitary.When = indRow.LogRow.CardRow.When; }

            if (indRow.LogRow.CardRow.IsWaterIDNull() || indRow.LogRow.CardRow.WaterRow.IsWaterNull())
            {
                result.Solitary.SetWaterIDNull();
            }
            else
            {
                Data.WaterRow waterRow = result.Water.NewWaterRow();
                waterRow.Type = indRow.LogRow.CardRow.WaterRow.Type;
                waterRow.Water = indRow.LogRow.CardRow.WaterRow.Water;
                result.Water.AddWaterRow(waterRow);
                result.Solitary.WaterRow = waterRow;
            }

            result.Solitary.Comments = indRow.GetDescription();

            if (!indRow.IsLengthNull())
            {
                Data.FactorRow lengthFactor = result.Factor.FindByFactor(Wild.Resources.Reports.Caption.Length);
                if (lengthFactor == null) lengthFactor = result.Factor.AddFactorRow(Wild.Resources.Reports.Caption.Length);

                Data.FactorValueRow factorValueRow = result.FactorValue.FindByCardIDFactorID(result.Solitary.ID, lengthFactor.ID);
                if (factorValueRow == null) factorValueRow = result.FactorValue.AddFactorValueRow(result.Solitary, lengthFactor, indRow.Length);
                factorValueRow.Value = indRow.Length;
            }

            if (!indRow.IsAgeNull())
            {
                Data.FactorRow ageFactor = result.Factor.FindByFactor(Wild.Resources.Reports.Caption.Age);
                if (ageFactor == null) ageFactor = result.Factor.AddFactorRow(Wild.Resources.Reports.Caption.Age);

                Data.FactorValueRow factorValueRow = result.FactorValue.FindByCardIDFactorID(result.Solitary.ID, ageFactor.ID);
                if (factorValueRow == null) factorValueRow = result.FactorValue.AddFactorValueRow(result.Solitary, ageFactor, indRow.Age);
                factorValueRow.Value = indRow.Age;
            }

            result.Solitary.ID = intRow.ID;
            result.ClearUseless();
            return result;
        }



        public static Data GetInfection(this Data.OrganRow organRow)
        {
            if (organRow.IsInfectionNull()) return null;

            Data result = new Data();
            result.ReadXml(new StringReader(organRow.Infection));

            if (!organRow.IndividualRow.IsRegIDNull())
            {
                result.Solitary.Label = organRow.IndividualRow.RegID;
            }

            result.Solitary.Comments = organRow.IndividualRow.GetDescription();

            return result;
        }
    }
}

