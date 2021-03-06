using Mayfly.Benthos;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mayfly.Mathematics.Charts;
using System.IO;
using System.Text;
using Mayfly.Wild;
using Mayfly.Species;

namespace Mayfly.Extensions
{
    public static class DataExtensionsBio
    {
        public static void ApplyMassRecoveryModel(this Wild.Survey data, Wild.Survey.DefinitionRow definitionRow, Wild.Survey.VariableRow variableRow, Regression model)
        {
            data.ApplyMassRecoveryModel(definitionRow.GetIndividualRows(), variableRow, model);
        }

        public static void ApplyMassRecoveryModel(this Wild.Survey data, IList<Wild.Survey.IndividualRow> individualRows, Wild.Survey.VariableRow variableRow, Regression model)
        {
            variableRow = data.Variable.FindByVarName(variableRow.Variable);
            foreach (Wild.Survey.IndividualRow individualRow in individualRows)
            {
                if (!individualRow.IsMassNull()) continue;
                Wild.Survey.ValueRow valueRoow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);
                if (valueRoow == null) continue;
                individualRow.Mass = Math.Round(model.Predict(valueRoow.Value), 2);
            }
        }

        public static List<Wild.Survey.IndividualRow> GetIndividualsThatAreMeasuredToo(this DataColumn dataColumn, List<Wild.Survey.IndividualRow> naturalRows, List<Wild.Survey.IndividualRow> foodRows)
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            if (naturalRows.Count == 0) return result;

            if (foodRows.Count == 0) return result;

            List<object> availableValues = ((Wild.Survey)naturalRows[0].Table.DataSet).Individual.Columns[dataColumn.ColumnName]
                .GetValues(naturalRows, true);

            foreach (Wild.Survey.IndividualRow individualRow in foodRows)
            {
                if (individualRow.IsNull(dataColumn)) continue;
                if (availableValues.Contains(individualRow[dataColumn]))
                {
                    result.Add(individualRow);
                }
            }

            return result;
        }


        public static void ApplyMassRecoveryWithModelData(this Wild.Survey data, List<Wild.Survey.IndividualRow> individualRows, DataColumn dataColumn, List<Wild.Survey.IndividualRow> naturalIndividuals)
        {
            Wild.Survey naturalData = (Wild.Survey)naturalIndividuals[0].Table.DataSet;
            DataColumn naturalValues = naturalData.Individual.Columns[dataColumn.ColumnName];
            List<Sample> samples = naturalData.Individual.MassColumn.GetSamples(naturalValues, naturalIndividuals);

            foreach (Wild.Survey.IndividualRow individualRow in individualRows)
            {
                if (!individualRow.IsMassNull()) continue;

                foreach (Sample sample in samples)
                {
                    if (sample.Name == string.Empty) continue;

                    if (individualRow[dataColumn.ColumnName].ToString() == sample.Name)
                    {
                        if (sample.Count > 1)
                        {
                            individualRow[data.Individual.MassColumn] = Math.Round(sample.Mean, 2);
                        }
                        else
                        {
                            individualRow[data.Individual.MassColumn] = Math.Round(sample.Sum(), 2);
                        }
                    }
                }
            }
        }



        public static void RestoreMass(this Wild.Survey data)
        {
            foreach (Wild.Survey.LogRow logRow in data.Log)
            {
                if (!logRow.IsMassNull()) continue;

                logRow.Mass = logRow.DetailedMass;
            }
        }

        public static void RecalcQuantity(this Wild.Survey data)
        {
            foreach (Wild.Survey.LogRow logRow in data.Log)
            {
                if (!logRow.IsQuantityNull()) continue;

                logRow.Quantity = logRow.DetailedQuantity;
            }
        }

        ///// <summary>
        ///// Recover mass with natural or reference data using straight priority and all available associations
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="referenceData"></param>
        //public static void RecoverMasses(this Data data, Data referenceData)
        //{
        //    foreach (Data.DefinitionRow definitionRow in data.Species)
        //    {
        //        #region Associates

        //        List<SpeciesKey.TaxonRow> associates = new List<SpeciesKey.TaxonRow>();

        //        SpeciesKey.TaxonRow conSpecies = referenceData.Definition.FindByName(definitionRow.Species);

        //        if (conSpecies != null)
        //        {
        //            associates.Add(conSpecies);
        //        }

        //        foreach (string associate in Mayfly.Benthos.Explorer.Service.GetAssociates(speciesRow.Species))
        //        {
        //            conSpecies = referenceData.Definition.FindByName(associate);

        //            if (conSpecies != null)
        //            {
        //                associates.Add(conSpecies);
        //            }
        //        }

        //        if (associates.Count == 0)
        //        {
        //            Data.DefinitionRow conGenus = referenceData.Definition.FindByName(
        //                Species.SpeciesKey.Genus(speciesRow.Species) + " sp.");

        //            if (conGenus != null)
        //            {
        //                associates.Add(conGenus);
        //            }
        //        }

        //        #endregion

        //        if (associates.Count == 0) continue;

        //        List<string> assoc = new List<string>();

        //        foreach (Data.DefinitionRow associate in associates)
        //        {
        //            assoc.Add(associate.Species);
        //        }

        //        List<Data.IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

        //        #region Try #1. Recover by length

        //        int measured = 0;

        //        foreach (SpeciesKey.TaxonRow associate in associates)
        //        {
        //            measured += referenceData.GetStack().Measured(associate);
        //        }

        //        List<Data.IndividualRow> recoverableRows = unweightedIndividuals.GetUnweightedAndMeasuredIndividualRows();
        //        int recoverableCount = recoverableRows.GetCount();

        //        if (measured > 0 && recoverableCount > 0)
        //        {
        //            List<Data.IndividualRow> natureRows = new List<Data.IndividualRow>();

        //            foreach (Data.DefinitionRow associate in associates)
        //            {
        //                natureRows.AddRange(associate.GetIndividualRows().GetWeightedAndMeasuredIndividualRows());
        //            }

        //            //Regression model = referenceData.FindMassModel(assoc.ToArray()).Regression;

        //            ////BivariateSample sample = referenceData.GetBivariate(natureRows.ToArray(),
        //            ////    referenceData.Individual.LengthColumn, referenceData.Individual.MassColumn);

        //            ////if (sample.Count >= Mayfly.Mathematics.UserSettings.StrongSampleSize)
        //            ////{
        //            ////    Power model = new Power(sample);

        //            //    if (model != null)
        //            //    {
        //            //        List<Data.IndividualRow> recoveredRows = new List<Data.IndividualRow>();

        //            //        foreach (Data.IndividualRow individualRow in unweightedIndividuals)
        //            //        {
        //            //            if (individualRow.IsLengthNull()) continue;
        //            //            individualRow.Mass = Math.Round(model.Predict(individualRow.Length), 5);
        //            //            recoveredRows.Add(individualRow);
        //            //        }

        //            //        foreach (Data.IndividualRow individualRow in recoveredRows)
        //            //        {
        //            //            unweightedIndividuals.Remove(individualRow);
        //            //        }
        //            //    }
        //            ////}
        //        }

        //        #endregion

        //        #region Try #2. Recover by variable

        //        List<Data.IndividualRow> weightedIndividuals = new List<Data.IndividualRow>();

        //        foreach (Data.DefinitionRow associate in associates)
        //        {
        //            weightedIndividuals.AddRange(associate.GetIndividualRows());
        //        }

        //        foreach (Data.VariableRow variableRow in referenceData.Variable)
        //        {
        //            Power model = referenceData.SearchMassModel(variableRow, weightedIndividuals.ToArray());

        //            if (model != null)
        //            {
        //                recoverableRows = unweightedIndividuals.GetMeasuredRows(data.Variable.FindByVarName(variableRow.Variable));

        //                if (recoverableRows.Count > 0)
        //                {
        //                    List<Data.IndividualRow> recoveredRows = new List<Data.IndividualRow>();

        //                    foreach (Data.IndividualRow individualRow in recoverableRows)
        //                    {
        //                        if (!individualRow.IsMassNull()) continue;
        //                        Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID,
        //                            data.Variable.FindByVarName(variableRow.Variable).ID);
        //                        if (valueRow == null) continue;
        //                        individualRow.Mass = Math.Round(model.Predict(valueRow.Value), 5);
        //                        recoveredRows.Add(individualRow);
        //                    }

        //                    foreach (Data.IndividualRow individualRow in recoveredRows)
        //                    {
        //                        unweightedIndividuals.Remove(individualRow);
        //                    }
        //                }
        //            }
        //        }

        //        #endregion

        //        #region Try #3. Recover by individual category

        //        DataColumn[] categorialVariables = new DataColumn[] { 
        //            data.Individual.GradeColumn 
        //        };

        //        foreach (DataColumn dataColumn in categorialVariables)
        //        {
        //            List<Data.IndividualRow> naturalRows = new List<Data.IndividualRow>();

        //            foreach (Data.DefinitionRow associate in associates)
        //            {
        //                naturalRows.AddRange(associate.GetIndividualRows()
        //                    .GetMeasuredRows(referenceData.Individual.Columns[dataColumn.ColumnName]));
        //            }

        //            recoverableRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows,
        //                unweightedIndividuals.GetMeasuredRows(dataColumn));

        //            if (recoverableRows.Count > 0)
        //            {
        //                List<Sample> samples = referenceData.Individual.MassColumn.GetSamples(
        //                    referenceData.Individual.Columns[dataColumn.ColumnName],
        //                    naturalRows);

        //                foreach (Data.IndividualRow individualRow in recoverableRows)
        //                {
        //                    if (!individualRow.IsMassNull()) continue;

        //                    foreach (Sample sample in samples)
        //                    {
        //                        if (sample.Name == string.Empty) continue;

        //                        if (sample.Name == individualRow[dataColumn.ColumnName].ToString())
        //                        {
        //                            if (sample.Count > 1)
        //                            {
        //                                individualRow.Mass = Math.Round(sample.Mean, 5);
        //                            }
        //                            else
        //                            {
        //                                individualRow.Mass = Math.Round(sample.Sum(), 5);
        //                            }
        //                        }
        //                    }
        //                }

        //                foreach (Data.IndividualRow individualRow in recoverableRows)
        //                {
        //                    unweightedIndividuals.Remove(individualRow);
        //                }
        //            }
        //        }

        //        #endregion

        //        #region Try #4. Recover by raw average individual mass

        //        {
        //            int weighted = 0;

        //            foreach (Data.DefinitionRow associate in associates)
        //            {
        //                weighted += referenceData.GetStack().Weighted(associate);
        //            }

        //            recoverableCount = unweightedIndividuals.GetCount() + speciesRow.AbstractIndividuals();

        //            if (weighted > 0 && recoverableCount > 0)
        //            {
        //                List<Data.IndividualRow> naturalRows = new List<Data.IndividualRow>();

        //                foreach (Data.DefinitionRow associate in associates)
        //                {
        //                    naturalRows.AddRange(associate.GetWeightedIndividualRows());
        //                }

        //                double avgMass = Math.Round(naturalRows.GetAverageMass(), 5);

        //                foreach (Data.IndividualRow individualRow in unweightedIndividuals)
        //                {
        //                    individualRow.Mass = avgMass;
        //                }

        //                unweightedIndividuals.Clear();
        //            }
        //        }

        //        #endregion
        //    }

        //    data.RestoreMass();
        //}

        public static Power SearchMassModel(this Wild.Survey data, Wild.Survey.VariableRow variableRow, Wild.Survey.IndividualRow[] individualRows)
        {
            variableRow = data.Variable.FindByVarName(variableRow.Variable);
            BivariateSample bivariateSample = new BivariateSample();

            foreach (Wild.Survey.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsMassNull()) continue;
                Wild.Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);
                if (valueRow == null) continue;
                bivariateSample.Add(valueRow.Value, individualRow.Mass);
            }

            if (bivariateSample.Count < Mathematics.UserSettings.RequiredSampleSize)
                return null;

            bivariateSample.X.Name = variableRow.Variable;
            bivariateSample.Y.Name = Benthos.Resources.Reports.Caption.Mass;

            return bivariateSample.Count > 10 ? new Power(bivariateSample) : null;
        }





        public static int Quantity(this Wild.Survey.IndividualDataTable individual, Wild.Survey.DefinitionRow definitionRow, Wild.Survey.VariableRow variableRow)
        {
            return individual.Quantity(definitionRow.Taxon, variableRow);
        }

        public static int Quantity(this Wild.Survey.IndividualDataTable individual, string species, Wild.Survey.VariableRow variableRow)
        {
            Wild.Survey.DefinitionRow speciesRow = ((Wild.Survey)individual.DataSet).Definition.FindByName(species);



            int result = 0;
            foreach (Wild.Survey.LogRow logRow in speciesRow.GetLogRows())
            {
                result += individual.Quantity(logRow, variableRow);
            }
            return result;
        }

        public static int Quantity(this Wild.Survey.IndividualDataTable individual, Wild.Survey.LogRow logRow, Wild.Survey.VariableRow variableRow)
        {
            int result = 0;
            foreach (Wild.Survey.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (((Wild.Survey)individual.DataSet).Value.FindByIndIDVarID(individualRow.ID, variableRow.ID) == null) continue;
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }
            return result;
        }

        public static int Unweighted(this Wild.Survey.IndividualDataTable individual, Wild.Survey.DefinitionRow speciesRow, Wild.Survey.VariableRow variableRow)
        {
            return individual.Unweighted(speciesRow.Taxon, variableRow);
        }

        public static int Unweighted(this Wild.Survey.IndividualDataTable individual, string species, Wild.Survey.VariableRow variableRow)
        {
            Wild.Survey.DefinitionRow speciesRow = ((Wild.Survey)individual.DataSet).Definition.FindByName(species);



            int result = 0;
            foreach (Wild.Survey.LogRow logRow in speciesRow.GetLogRows())
            {
                result += individual.Unweighted(logRow, variableRow);
            }
            return result;
        }

        public static int Unweighted(this Wild.Survey.IndividualDataTable individual, Wild.Survey.LogRow logRow, Wild.Survey.VariableRow variableRow)
        {
            int result = 0;
            foreach (Wild.Survey.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (((Wild.Survey)individual.DataSet).Value.FindByIndIDVarID(individualRow.ID, variableRow.ID) == null) continue;
                if (!individualRow.IsMassNull()) continue;
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }
            return result;
        }

        

        public static Wild.Survey GetBenthosBio(this Wild.Survey data1)
        {
            Wild.Survey data = (Wild.Survey)((DataSet)data1).Copy();

            // Remove unsigned cards

            for (int i = 0; i < data.Card.Count; i++)
            {
                if (data.Card[i].IsSignNull())
                {
                    data.Card.Rows.RemoveAt(i);
                    i--;
                }
            }

            if (data.Card.Count == 0)
                throw new ArgumentNullException("Unsigned data", "Cards do not contain signed data.");

            // Remove irrelevant tables and fields
            
            data.FactorValue.Clear();
            data.Factor.Clear();

            for (int i = 0; i < data.Individual.Count; i++)
            {
                if (!data.Individual[i].IsGradeNull() && data.Individual[i].Grade != 1)
                {
                    data.Individual.Rows.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < data.Individual.Count; i++)
            {
                if (data.Individual[i].IsMassNull())
                {
                    data.Individual.Rows.RemoveAt(i);
                    i--;
                }
            }

            TaxonomicIndex.TaxonRow[] spclist = data.GetStack().GetSpecies();

            for (int i = 0; i < spclist.Length; i++)
            {
                TaxonomicIndex.TaxonRow speciesRow = spclist[i];

                //if (speciesRow.Quantity < 5)
                //{
                //    data.Species.RemoveSpeciesRow(speciesRow);
                //    i--;
                //    continue;
                //}

                List<Wild.Survey.IndividualRow> wRows = speciesRow.GetWeightedIndividualRows();

                if (wRows.Count == 0) continue;

                string d = "Ширина головной капсулы, мм";
                string w = "Масса, мг";
                List<Wild.Survey.IndividualRow> dRows = wRows.GetMeasuredRows(d);
                BivariateSample dSample = new BivariateSample(d, w);
                foreach (Wild.Survey.IndividualRow dRow in dRows)
                {
                    dSample.Add(data.GetIndividualValue(dRow, d), dRow.Mass);
                }

                //if (dSample.Count < 5)
                //{
                //    data.Species.RemoveSpeciesRow(speciesRow);
                //    i--;
                //    continue;
                //}

                //try
                //{
                //    Power dw = new Power(dSample);

                //    if (dw.Slope < 2 || dw.Slope > 4)
                //    {
                //        data.Species.RemoveSpeciesRow(speciesRow);
                //        i--;
                //        continue;
                //    }
                //}
                //catch
                //{
                //        data.Species.RemoveSpeciesRow(speciesRow);
                //        i--;
                //        continue;

                //}

                //Benthos.Explorer.HeadSample TotalSample = new Benthos.Explorer.HeadSample(wRows, "Обобщенная выборка", "Общ");

                //if (TotalSample.dw == null)
                //{
                //    data.Species.RemoveSpeciesRow(speciesRow);
                //    i--;
                //    continue;
                //}
            }

            if (data.Definition.Count == 0)
                throw new ArgumentNullException("Regression data", "Data is unsufficient to be used as bio.");

            return data;
        }
    }
}

