using Mayfly.Mathematics.Statistics;
using Mayfly.Plankton;
using Mayfly.Species;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Mayfly.Plankton.Explorer;

namespace Mayfly.Extensions
{
    public static class DataExtensions
    {
        public static object[] GetVariantsOf(this Data data, string field)
        {
            List<object> variants = new List<object>();

            foreach (Data.CardRow cardRow in data.Card)
            {
                object value = cardRow.Get(field);

                if (value == null) continue;

                if (!variants.Contains(value))
                    variants.Add(value);
            }

            variants.Sort(new OmniSorter());

            return variants.ToArray();
        }
        
        public static string[] GetInvestigators(this Data data)
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in data.Card)
            {
                string investigator = cardRow.Investigator;
                if (result.Contains(investigator)) continue;
                result.Add(investigator);
            }
            return result.ToArray();
        }

        public static string[] GetWaters(this Data data)
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in data.Card)
            {
                if (cardRow.IsWaterIDNull()) continue;
                string waterDescription = cardRow.WaterRow.Presentation;
                if (result.Contains(waterDescription)) continue;
                result.Add(waterDescription);
            }
            return result.ToArray();
        }

        public static DateTime[] GetDates(this Data data)
        {
            List<DateTime> result = new List<DateTime>();
            foreach (Data.CardRow cardRow in data.Card)
            {
                if (cardRow.IsWhenNull()) continue;
                if (result.Contains(cardRow.When.Date)) continue;
                result.Add(cardRow.When.Date);
            }
            result.Sort();
            return result.ToArray();
        }

        public static Samplers.SamplerRow[] GetSamplers(this Data data)
        {
            List<Samplers.SamplerRow> Result = new List<Samplers.SamplerRow>();

            foreach (Data.CardRow cardRow in data.Card)
            {
                if (cardRow.IsSamplerNull()) continue;
                Samplers.SamplerRow samplerRow = Plankton.Service.Sampler(cardRow.Sampler);
                if (samplerRow == null) continue;
                if (Result.Contains(samplerRow)) continue;
                Result.Add(samplerRow);
            }

            return Result.ToArray();
        }

        public static string[] GetSamplersList(this Data data)
        {
            List<string> result = new List<string>();

            foreach (Samplers.SamplerRow samplerRow in data.GetSamplers())
            {
                result.Add(samplerRow.Sampler);
            }

            return result.ToArray();
        }

        public static int GetSpeciesWealth(this Data data)
        {
            return data.Log.SpcIDColumn.SelectDistinctInteger().Length;
        }



        #region Stats

        #region Quantity

        public static double Quantity(this Data data)
        {
            double result = 0;

            foreach (Data.LogRow logRow in data.Log)
            {
                if (logRow.IsQuantityNull()) return double.NaN;

                result += logRow.Quantity;
            }

            return result;
        }

        //public static double Abundance(this Data data)
        //{
        //    double result = 0;

        //    foreach (Data.CardRow cardRow in data.Card)
        //    {
        //        if (cardRow.IsSquareNull()) continue;
        //        result += data.Quantity(cardRow) / cardRow.Square;
        //    }

        //    result /= data.Card.Count;

        //    return Math.Round(result, 2);
        //}

        public static double Abundance(this Data data, Data.CardRow cardRow)
        {
            double result = 0;
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                result += data.Abundance(logRow);
                if (double.IsNaN(result)) { return result; }
            }
            return result;
        }

        public static double Abundance(this Data data, Data.DefinitionRow speciesRow)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return 0;

            double result = 0.0;

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                result += data.Abundance(logRow);
            }

            return Math.Round(result / (double)data.Card.Count, 2);
        }

        public static double Abundance(this Data data, Data.LogRow logRow)
        {
            if (logRow.IsQuantityNull()) return double.NaN;
            if (logRow.CardRow.IsVolumeNull()) return double.NaN;
            return Math.Round((double)logRow.Quantity / (logRow.IsSubsampleNull() ? 1 : logRow.Subsample) / logRow.CardRow.Volume, 3);
        }

        public static double Quantity(this Data data, Data.DefinitionRow speciesRow)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return 0;

            double result = 0.0;

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                if (logRow.IsQuantityNull())
                {
                    logRow.Quantity = logRow.DetailedQuantity;
                }

                result += logRow.Quantity;
            }

            return result;
        }

        //public static int Quantity(this Data data, Data.DefinitionRow speciesRow, double lengthClass)
        //{
        //    return speciesRow.GetQuantity(lengthClass);
        //}

        //public static int Quantity(this Data data, Data.DefinitionRow speciesRow, double lengthClass, Sex G)
        //{
        //    speciesRow = data.Definition.FindByName(speciesRow.Species);
        //    if (speciesRow == null) return 0;

        //    int result = 0;
        //    foreach (Data.LogRow logRow in speciesRow.GetLogRows())
        //    {
        //        foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
        //        {
        //            if (individualRow.IsLengthNull()) continue;
        //            if (individualRow.IsSexNull()) continue;
        //            if (Plankton.Service.GetStrate(individualRow.Length) != lengthClass) continue;
        //            if (individualRow.Sex != G.Value) continue;
        //            result++;
        //        }
        //    }
        //    return result;
        //}

        public static int Quantity(this Data data, Data.DefinitionRow speciesRow, Sex G)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return 0;

            int result = 0;
            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    result++;
                }
            }
            return result;
        }

        //public static int QuantitySexed(this Data data, Data.DefinitionRow speciesRow, double lengthClass)
        //{
        //    speciesRow = data.Definition.FindByName(speciesRow.Species);
        //    if (speciesRow == null) return 0;

        //    int result = 0;
        //    foreach (Data.LogRow logRow in speciesRow.GetLogRows())
        //    {
        //        foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
        //        {
        //            if (individualRow.IsSexNull()) continue;
        //            if (individualRow.IsLengthNull()) continue;
        //            if (Plankton.Service.GetStrate(individualRow.Length) != lengthClass) continue;
        //            result++;
        //        }
        //    }
        //    return result;
        //}

        //public static int Quantity(this Data.IndividualDataTable individual, Data.DefinitionRow speciesRow, Data.VariableRow variableRow)
        //{
        //    return individual.Quantity(speciesRow.Species, variableRow);
        //}

        //public static int Quantity(this Data.IndividualDataTable individual, string species, Data.VariableRow variableRow)
        //{
        //    Data.DefinitionRow speciesRow = ((Data)individual.DataSet).Definition.FindByName(species);

        //    if (speciesRow == null) return 0;

        //    int result = 0;
        //    foreach (Data.LogRow logRow in speciesRow.GetLogRows())
        //    {
        //        result += individual.Quantity(logRow, variableRow);
        //    }
        //    return result;
        //}

        public static int Quantity(this Data.IndividualDataTable individual, Data.LogRow logRow, Data.VariableRow variableRow)
        {
            int result = 0;
            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (((Data)individual.DataSet).Value.FindByIndIDVarID(individualRow.ID, variableRow.ID) == null) continue;
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }
            return result;
        }

        public static int Unweighted(this Data.IndividualDataTable individual, Data.DefinitionRow speciesRow, Data.VariableRow variableRow)
        {
            return individual.Unweighted(speciesRow.Species, variableRow);
        }

        public static int Unweighted(this Data.IndividualDataTable individual, string species, Data.VariableRow variableRow)
        {
            Data.DefinitionRow speciesRow = ((Data)individual.DataSet).Definition.FindByName(species);

            if (speciesRow == null) return 0;

            int result = 0;
            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                result += individual.Unweighted(logRow, variableRow);
            }
            return result;
        }

        public static int Unweighted(this Data.IndividualDataTable individual, Data.LogRow logRow, Data.VariableRow variableRow)
        {
            int result = 0;
            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (((Data)individual.DataSet).Value.FindByIndIDVarID(individualRow.ID, variableRow.ID) == null) continue;
                if (!individualRow.IsMassNull()) continue;
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }
            return result;
        }

        #endregion

        #region Mass

        //public static double Mass(this Data data, Data.DefinitionRow speciesRow)
        //{
        //    //speciesRow = data.Definition.FindByName(speciesRow.Species);
        //    //if (speciesRow == null) return 0;

        //    double result = 0.0;

        //    foreach (Data.LogRow logRow in speciesRow.GetLogRows())
        //    {
        //        double w = data.Mass(logRow);

        //        if (double.IsNaN(w))
        //        {
        //            return double.NaN;
        //        }
        //        else
        //        {
        //            result += w;
        //        }
        //    }

        //    return result;
        //}

        //public static double Mass(this Data data, Data.LogRow logRow)
        //{
        //    if (logRow.IsMassNull() || double.IsNaN(logRow.Mass))
        //    {
        //        return data.Individual.Mass(logRow);
        //    }
        //    else
        //    {
        //        return logRow.Mass;
        //    }
        //}

        //public static double Biomass(this Data data)
        //{
        //    double result = 0;

        //    foreach (Data.CardRow cardRow in data.Card)
        //    {
        //        if (cardRow.IsSquareNull()) continue;
        //        result += data.Mass(cardRow) / 1000 / cardRow.Square;
        //    }

        //    result /= data.Card.Count;

        //    return result;
        //}

        //public static double DetailedMass(this Data data, Data.LogRow logRow)
        //{
        //    double result = 0;

        //    foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
        //    {
        //        if (!individualRow.IsMassNull())
        //        {
        //            result += individualRow.Mass;
        //        }
        //    }

        //    return result;
        //}
        


        //public static double Biomass(this Data data, Data.CardRow cardRow)
        //{
        //    double result = 0;
        //    foreach (Data.LogRow logRow in cardRow.GetLogRows())
        //    {
        //        result += data.Biomass(logRow);
        //        if (double.IsNaN(result)) { return result; }
        //    }
        //    return result;
        ////}

        //public static double Biomass(this Data data, Data.LogRow logRow)
        //{
        //    if (logRow.IsMassNull()) return double.NaN;
        //    if (logRow.CardRow.IsVolumeNull()) return double.NaN;
        //    return logRow.Mass / (logRow.IsSubsampleNull() ? 1 : logRow.Subsample) / 1000 / logRow.CardRow.Volume;
        //}

        //public static double Biomass(this Data data, Data.DefinitionRow speciesRow)
        //{
        //    speciesRow = data.Definition.FindByName(speciesRow.Species);
        //    if (speciesRow == null) return 0;

        //    double result = 0.0;

        //    foreach (Data.LogRow logRow in speciesRow.GetLogRows())
        //    {
        //        result += data.Biomass(logRow);
        //    }

        //    return result / (double)data.Card.Count;
        //}

        public static Sample Masses(this Data data, Data.DefinitionRow speciesRow)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return new Sample();

            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        #endregion

        #region Length

        public static Sample Lengths(this Data data, Data.DefinitionRow speciesRow)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return new Sample();

            List<double> result = new List<double>();

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsLengthNull()) continue;
                    result.Add(individualRow.Length);
                }
            }

            if (result.Count > 0) return new Sample(result.ToArray());
            else return null;
        }

        public static double LengthAverage(this Data data, Data.DefinitionRow speciesRow, Sex G)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return double.NaN;

            double result = 0;
            double divider = 0;

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    result += individualRow.Length;
                    divider++;
                }
            }

            if (divider == 0)
            {
                return double.NaN;
            }
            else
            {
                return result / divider;
            }
        }

        public static Sample Lengths(this Data data, Data.DefinitionRow speciesRow, Sex G)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return new Sample();

            List<double> result = new List<double>();

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    result.Add(individualRow.Length);
                }
            }
            return new Sample(result.ToArray());
        }

        public static double LengthMin(this Data data, Data.DefinitionRow speciesRow, Sex G)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return double.NaN;

            double result = double.MaxValue;
            int i = 0;

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    if (result > individualRow.Length)
                    {
                        result = individualRow.Length;
                        i++;
                    }
                }
            }

            if (i > 0)
            {
                return result;
            }
            else
            {
                return double.NaN;
            }
        }

        public static double LengthMax(this Data data, Data.DefinitionRow speciesRow, Sex G)
        {
            speciesRow = data.Definition.FindByName(speciesRow.Species);
            if (speciesRow == null) return double.NaN;

            double result = double.MinValue;
            int i = 0;

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    if (result < individualRow.Length)
                    {
                        result = individualRow.Length;
                        i++;
                    }
                }
            }

            if (i > 0)
            {
                return result;
            }
            else
            {
                return double.NaN;
            }
        }

        #endregion

        #region Diversity

        public static double Wealth(this Data data, Data.CardRow cardRow)
        {
            return cardRow.GetLogRows().Length;
        }

        public static double DiversityA(this Data data, Data.CardRow cardRow)
        {
            List<double> values = new List<double>();
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                values.Add(data.Abundance(logRow));
            }

            return values.Count == 0 ? double.NaN : new Sample(values).Diversity();
        }

        public static double DiversityB(this Data data, Data.CardRow cardRow)
        {
            List<double> values = new List<double>();

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                values.Add(logRow.GetBiomass());
            }

            return values.Count == 0 ? double.NaN : new Sample(values).Diversity();
        }

        #endregion

        public static int Weighted(this Data data, Data.DefinitionRow speciesRow)
        {
            int result = 0;
            speciesRow = data.Definition.FindByName(speciesRow.Species); if (speciesRow == null) return 0;

            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result++;
            }

            return result;
        }

        public static int Measured(this Data data, Data.DefinitionRow speciesRow)
        {
            int result = 0;
            speciesRow = data.Definition.FindByName(speciesRow.Species); if (speciesRow == null) return 0;

            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }

        #endregion



        public static string[] InCommon(this Data data, Data toCompare)
        {
            List<string> list1 = data.GetSpeciesList();
            List<string> list2 = toCompare.GetSpeciesList();

            List<string> result = new List<string>();

            foreach (string species in list1)
            {
                if (list2.Contains(species))
                {
                    result.Add(species);
                }
            }

            return result.ToArray();
        }



        public static Power SearchMassModel(this Data data, Data.VariableRow variableRow, Data.IndividualRow[] individualRows)
        {
            variableRow = data.Variable.FindByVarName(variableRow.Variable);
            BivariateSample bivariateSample = new BivariateSample();

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsMassNull()) continue;
                Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);
                if (valueRow == null) continue;
                bivariateSample.Add(valueRow.Value, individualRow.Mass);
            }

            if (bivariateSample.Count < Mathematics.UserSettings.RequiredSampleSize)
                return null;

            bivariateSample.X.Name = variableRow.Variable;
            bivariateSample.Y.Name = Wild.Resources.Reports.Caption.Mass;

            return new Power(bivariateSample);
        }
        


        public static int GetCount(this IList<Data.IndividualRow> individualRows)
        {
            int result = 0;

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }

            return result;
        }

        public static double[] GetMass(this IList<Data.IndividualRow> individualRows)
        {
            Data data = (Data)individualRows[0].Table.DataSet;
            return data.Individual.MassColumn.GetDoubles(individualRows).ToArray();

            //List<double> result = new List<double>();

            //foreach (Data.IndividualRow individualRow in individualRows)
            //{
            //    if (individualRow.IsMassNull()) continue;
            //    result.Add(individualRow.Mass);
            //}

            //return result.ToArray();
        }

        public static double GetAverageMass(this IList<Data.IndividualRow> individualRows)
        {
            Sample mass = new Sample(GetMass(individualRows));
            return mass.Count > 0 ? mass.Mean : double.NaN;
            //double result = 0;
            //int count = 0;

            //foreach (Data.IndividualRow individualRow in individualRows)
            //{
            //    if (individualRow.IsMassNull()) continue;
            //    result += individualRow.Mass;
            //    count++;
            //}

            //if (count > 0)
            //{
            //    return result / (double)count;
            //}
            //else
            //{
            //    return double.NaN;
            //}
        }

        public static List<Data.IndividualRow> GetMeasuredRows(this IList<Data.IndividualRow> rows, Data.VariableRow variableRow)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in rows)
            {
                foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
                {
                    if (valueRow.VariableRow.Variable == variableRow.Variable &&
                        !valueRow.IsValueNull() && !result.Contains(individualRow))
                    {
                        result.Add(individualRow);
                        break;
                    }
                }
            }

            return result;
        }

        public static List<Data.IndividualRow> GetMeasuredRows(this IList<Data.IndividualRow> rows, DataColumn dataColumn)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in rows)
            {
                if (individualRow.IsNull(dataColumn)) continue;
                result.Add(individualRow);
            }

            return result;
        }
        
        

        public static Composition GetCenosisComposition(this Data data)
        {
            return data.GetStack().GetBasicCenosisComposition();
        }

        public static void AddSpeciesMenus(this ToolStripMenuItem item, CardStack stack, EventHandler command)
        {
            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i].Tag != null)
                {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator))
            {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (Data.DefinitionRow speciesRow in stack.GetSpeciesRows())
            {
                ToolStripItem _item = new ToolStripMenuItem();
                _item.Tag = speciesRow;
                _item.Text = speciesRow.KeyRecord.FullName;
                _item.Click += command;
                item.DropDownItems.Add(_item);
            }

        }
    }
}

