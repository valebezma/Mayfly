using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        Data.SpeciesRow individualSpecies;

        private void LoadIndLog()
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            foreach (Data.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }
            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync();
        }

        private void LoadIndLog(Data.SpeciesRow speciesRow)
        {
            individualSpecies = speciesRow;

            IsBusy = true;
            spreadSheetInd.StartProcessing(AllowedStack.QuantityIndividual(speciesRow),
                Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            foreach (Data.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync(speciesRow);
        }

        private Data.IndividualRow[] GetIndividuals(IList rows)
        {
            spreadSheetInd.EndEdit();
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (DataGridViewRow gridRow in rows) {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.IndividualRow individualRow = IndividualRow(gridRow);
                if (individualRow == null) continue;

                result.Add(individualRow);
            }

            return result.ToArray();
        }


        private void SimulateStratifiedSamples()
        {
            IsBusy = true;

            spreadSheetInd.StartProcessing(individualSpecies == null ?
                data.Stratified.Quantity : AllowedStack.QuantityStratified(individualSpecies),
                Wild.Resources.Interface.Process.StratifiedProcessing);

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderIndSimulated.RunWorkerAsync(individualSpecies);
        }

        private void ClearSimulated()
        {
            for (int i = 0; i < spreadSheetInd.Rows.Count; i++)
            {
                if (spreadSheetInd[columnIndID.Index, i].Value == null)
                {
                    spreadSheetInd.Rows.RemoveAt(i);
                    i--;
                }
            }

            spreadSheetInd.UpdateStatus();
        }


        private DataGridViewRow GetLine(Data.IndividualRow individualRow)
        {
            DataGridViewRow result = new DataGridViewRow();
            result.CreateCells(spreadSheetInd);
            result.Cells[columnIndID.Index].Value = individualRow.ID;
            UpdateIndividualRow(result, individualRow);

            if (Licensing.Verify("Fishery Scientist"))
            {
                if (UserSettings.AgeSuggest) SetIndividualAgeTip(result, individualRow);
                if (UserSettings.MassSuggest) SetIndividualMassTip(result, individualRow);
            }

            setCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());
            return result;
        }


        private DataGridViewRow FindIndividualRow(Data.IndividualRow individualRow)
        {
            return columnIndID.GetRow(individualRow.ID, true, true);
        }

        private Data.IndividualRow IndividualRow(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[columnIndID.Name].Value == null) { return null; }
            else
            {
                int ID = (int)gridRow.Cells[columnIndID.Name].Value;
                return data.Individual.FindByID(ID);
            }
        }

        private Data.IndividualRow SaveIndRow(DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow = IndividualRow(gridRow);

            if (individualRow == null) return null;

            object length = gridRow.Cells[columnIndLength.Name].Value;
            if (length == null) individualRow.SetLengthNull();
            else individualRow.Length = (double)length;

            object mass = gridRow.Cells[columnIndMass.Name].Value;
            if (mass == null) individualRow.SetMassNull();
            else individualRow.Mass = (double)mass;

            object gmass = gridRow.Cells[columnIndSomaticMass.Name].Value;
            if (gmass == null) individualRow.SetSomaticMassNull();
            else individualRow.SomaticMass = (double)gmass;

            object regID = gridRow.Cells[columnIndRegID.Name].Value;
            if (regID == null) individualRow.SetRegIDNull();
            else individualRow.RegID = (string)regID;

            Age age = (Age)gridRow.Cells[columnIndAge.Name].Value;
            if (age == null) individualRow.SetAgeNull();
            else individualRow.Age = age.Value;

            if ((tabPageSpcStats.Parent != null) && // If stats are loaded
                (selectedStatSpc.Species == individualRow.Species)) // and selected species is currently editing
            {
                strates_Changed(this, new EventArgs());
            }

            Sex sex = (Sex)gridRow.Cells[columnIndSex.Name].Value;
            if (sex == null) individualRow.SetSexNull();
            else individualRow.Sex = sex.Value;

            object maturity = gridRow.Cells[columnIndMaturity.Name].Value;
            if (maturity == null)
            {
                individualRow.SetMaturityNull();
                individualRow.SetIntermatureNull();
            }
            else
            {
                individualRow.Maturity = ((Maturity)maturity).Value;
                individualRow.Intermature = ((Maturity)maturity).IsIntermediate;
            }

            #region Fecundity

            object gonadMass = gridRow.Cells[columnIndGonadMass.Name].Value;
            if (gonadMass == null) individualRow.SetGonadMassNull();
            else individualRow.GonadMass = (double)gonadMass;

            object gonadSampleMass = gridRow.Cells[columnIndGonadSampleMass.Name].Value;
            if (gonadSampleMass == null) individualRow.SetGonadSampleMassNull();
            else individualRow.GonadSampleMass = (double)gonadSampleMass;

            object gonadSample = gridRow.Cells[columnIndGonadSample.Name].Value;
            if (gonadSample == null) individualRow.SetGonadSampleNull();
            else individualRow.GonadSample = (int)gonadSample;

            object eggSize = gridRow.Cells[columnIndEggSize.Name].Value;
            if (eggSize == null) individualRow.SetEggSizeNull();
            else individualRow.EggSize = (double)eggSize;

            #endregion

            #region Diet

            object fat = gridRow.Cells[columnIndFat.Name].Value;
            if (fat == null) individualRow.SetFatnessNull();
            else individualRow.Fatness = (int)fat;

            #endregion

            object comments = gridRow.Cells[columnIndComments.Name].Value;
            if (comments == null) individualRow.SetCommentsNull();
            else individualRow.Comments = (string)comments;

            #region Additional variables

            foreach (DataGridViewColumn gridColumn in spreadSheetInd.GetColumns("Var_"))
            {
                Data.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                object varValue = gridRow.Cells[gridColumn.Name].Value;

                if (varValue == null)
                {
                    if (variableRow == null) continue;

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) continue;

                    valueRow.Delete();
                }
                else
                {
                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null)
                    {
                        data.Value.AddValueRow(individualRow, variableRow, (double)varValue);
                    }
                    else
                    {
                        valueRow.Value = (double)varValue;
                    }
                }
            }

            #endregion

            rememberChanged(individualRow.LogRow.CardRow);

            return individualRow;
        }

        private void UpdateIndividualRow(Data.IndividualRow individualRow)
        {
            UpdateIndividualRow(FindIndividualRow(individualRow), individualRow);
        }

        private void UpdateIndividualRow(DataGridViewRow result, Data.IndividualRow individualRow) 
        {
            result.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.IsSpcIDNull() ? null : individualRow.Species;
            result.Cells[columnIndLength.Index].Value = individualRow.IsLengthNull() ? null : (object)individualRow.Length;
            result.Cells[columnIndMass.Index].Value = individualRow.IsMassNull() ? null : (object)individualRow.Mass;
            result.Cells[columnIndSomaticMass.Index].Value = individualRow.IsSomaticMassNull() ? null : (object)individualRow.SomaticMass;
            result.Cells[columnIndCondition.Index].Value = double.IsNaN(individualRow.GetCondition()) ? null : (object)individualRow.GetCondition();
            result.Cells[columnIndConditionSoma.Index].Value = double.IsNaN(individualRow.GetConditionSomatic()) ? null : (object)individualRow.GetConditionSomatic();
            result.Cells[columnIndRegID.Index].Value = individualRow.IsRegIDNull() ? null : individualRow.RegID;

            if (individualRow.IsAgeNull())
            {
                result.Cells[columnIndAge.Index].Value = null;
                result.Cells[columnIndGeneration.Index].Value = null;
            }
            else
            {
                Age age = (Age)individualRow.Age;
                result.Cells[columnIndAge.Index].Value = age;
                Wild.Service.HandleAgeInput(result.Cells[columnIndAge.Index], columnIndAge.DefaultCellStyle);
                if (!individualRow.IsAgeNull()) result.Cells[columnIndGeneration.Index].Value = individualRow.Generation;
            }

            result.Cells[columnIndSex.Index].Value = individualRow.IsSexNull() ? null : (Sex)individualRow.Sex;
            result.Cells[columnIndMaturity.Index].Value = individualRow.IsMaturityNull()
                ? null
                : individualRow.IsIntermatureNull()
                    ? new Maturity(individualRow.Maturity)
                    : new Maturity(individualRow.Maturity, individualRow.Intermature);


            result.Cells[columnIndGonadMass.Index].Value = individualRow.IsGonadMassNull() ? null : (object)individualRow.GonadMass;
            result.Cells[columnIndGonadSampleMass.Index].Value = individualRow.IsGonadSampleMassNull() ? null : (object)individualRow.GonadSampleMass;
            result.Cells[columnIndGonadSample.Index].Value = individualRow.IsGonadSampleNull() ? null : (object)individualRow.GonadSample;
            result.Cells[columnIndEggSize.Index].Value = individualRow.IsEggSizeNull() ? null : (object)individualRow.EggSize;
            result.Cells[columnIndFecundityAbs.Index].Value = double.IsNaN(individualRow.GetAbsoluteFecundity()) ? null : (object)(individualRow.GetAbsoluteFecundity() / 1000.0);
            result.Cells[columnIndFecundityRelative.Index].Value = double.IsNaN(individualRow.GetRelativeFecundity()) ? null : (object)individualRow.GetRelativeFecundity();
            result.Cells[columnIndFecundityRelativeSoma.Index].Value = double.IsNaN(individualRow.GetRelativeFecunditySomatic()) ? null : (object)individualRow.GetRelativeFecunditySomatic();
            result.Cells[columnIndGonadIndex.Index].Value = double.IsNaN(individualRow.GetGonadIndex()) ? null : (object)individualRow.GetGonadIndex();
            result.Cells[columnIndGonadIndexSoma.Index].Value = double.IsNaN(individualRow.GetGonadIndexSomatic()) ? null : (object)individualRow.GetGonadIndexSomatic();

            
            result.Cells[columnIndFat.Index].Value = individualRow.IsFatnessNull() ? null : (object)individualRow.Fatness;

            if (individualRow.IsConsumedMassNull())
            {
                result.Cells[columnIndConsumed.Index].Value = null;
                result.Cells[columnIndConsumptionIndex.Index].Value = null;
            }
            else
            {
                result.Cells[columnIndConsumed.Index].Value = individualRow.ConsumedMass;
                result.Cells[columnIndConsumptionIndex.Index].Value = individualRow.GetConsumptionIndex();
            }

            int dietCount = individualRow.GetDietItemCount();
            result.Cells[columnIndDietItems.Index].Value = dietCount == -1 ? null : (object)dietCount;

            result.Cells[columnIndComments.Index].Value = individualRow.IsCommentsNull() ? null : individualRow.Comments;

            foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
            {
                result.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.IsValueNull() ? null : (object)valueRow.Value;
            }
        }

        private DataGridViewRow[] IndividualRows(Data.StratifiedRow stratifiedRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            for (int i = 0; i < stratifiedRow.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetInd);
                gridRow.ReadOnly = true;
                gridRow.DefaultCellStyle.ForeColor = Constants.InfantColor; // Color.DarkGray;

                //gridRow.Cells[columnIndID.Index].Value;

                if (stratifiedRow.LogRow.IsSpcIDNull()) { }
                else
                {
                    gridRow.Cells[columnIndSpecies.Index].Value = stratifiedRow.LogRow.SpeciesRow.Species;
                }

                gridRow.Cells[columnIndLength.Index].Value = stratifiedRow.SizeClass.Midpoint;

                gridRow.Cells[columnIndMass.Index].Value = data.MassModels.GetValue(
                    stratifiedRow.LogRow.SpeciesRow.Species, stratifiedRow.SizeClass.Midpoint);

                gridRow.Cells[columnIndComments.Index].Value = Resources.Interface.Interface.SimulatedIndividual;

                Age age = (Age)data.GrowthModels.GetValue(
                    stratifiedRow.LogRow.SpeciesRow.Species, stratifiedRow.SizeClass.Midpoint, true);

                gridRow.Cells[columnIndAge.Index].Value = age;
                Wild.Service.HandleAgeInput(gridRow.Cells[columnIndAge.Index], columnIndAge.DefaultCellStyle);

                setCardValue(stratifiedRow.LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns());

                result.Add(gridRow);
            }

            return result.ToArray();
        }

        private DataGridViewRow[] IndividualRows(Data.LogRow logRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
            {
                result.AddRange(IndividualRows(stratifiedRow));
            }

            return result.ToArray();
        }

        private DataGridViewRow[] IndividualRows(Data.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                int id = (int)gridRow.Cells[columnIndID.Name].Value;
                Data.IndividualRow individualRow = data.Individual.FindByID(id);

                if (individualRow.LogRow.CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }



        private void SetIndividualAgeTip(DataGridViewRow gridRow)
        {
            SetIndividualAgeTip(gridRow, IndividualRow(gridRow));
        }

        private void SetIndividualAgeTip(DataGridViewRow gridRow, Data.IndividualRow individualRow)
        {
            if (individualRow.IsLengthNull())
            {
                return;
            }

            if (gridRow.Cells[columnIndAge.Index].Value != null)
            {
                return;
            }

            double ageValue = data.GrowthModels.GetValue(
                individualRow.Species, individualRow.Length, true);

            if (double.IsNaN(ageValue))
            {
                gridRow.Cells[columnIndAge.Index].SetNullValue(Wild.Resources.Interface.Interface.SuggestionUnavailable);
            }
            else
            {
                Age age = new Age(ageValue);

                gridRow.Cells[columnIndAge.Index].SetNullValue(
                    double.IsNaN(ageValue) ?
                    Wild.Resources.Interface.Interface.SuggestionUnavailable : " " + age.ToString() + " ");
                Wild.Service.HandleAgeInput(gridRow.Cells[columnIndAge.Index], columnIndAge.DefaultCellStyle);

                gridRow.Cells[columnIndGeneration.Index].SetNullValue(
                    double.IsNaN(ageValue) ?
                    Wild.Resources.Interface.Interface.SuggestionUnavailable :
                    individualRow.LogRow.CardRow.When.AddYears(-age.Years).Year.ToString());
            }

        }

        private void SetIndividualMassTip(DataGridViewRow gridRow)
        {
            SetIndividualMassTip(gridRow, IndividualRow(gridRow));
        }

        private void SetIndividualMassTip(DataGridViewRow gridRow, Data.IndividualRow individualRow)
        {
            if (individualRow.IsLengthNull())
            {
                return;
            }

            if (gridRow.Cells[columnIndMass.Index].Value != null)
            {
                return;
            }

            double mass = data.MassModels.GetValue(
                individualRow.Species,
                individualRow.Length);

            gridRow.Cells[columnIndMass.Index].SetNullValue(
                double.IsNaN(mass) ?
                Wild.Resources.Interface.Interface.SuggestionUnavailable :
                //string.Format(Resources.Interface.SuggestionFormat,
                //mass.ToString(columnIndMass.DefaultCellStyle.Format)));
                mass.ToString(columnIndMass.DefaultCellStyle.Format));
        }
    }
}
