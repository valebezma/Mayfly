using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Bacterioplankton;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Waters;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;

namespace Mayfly.Bacterioplankton.Explorer
{
    public partial class MainForm
    {
        public Data data = new Data();

        public CardStack FullStack { get; private set; }

        public bool IsBusy
        {
            get
            {
                return busy;
            }

            set
            {
                tabControl.AllowDrop =
                    spreadSheetInd.Enabled = !value;

                foreach (Control control in tabPageInfo.Controls)
                {
                    control.Enabled = !value;
                }

                foreach (Control control in new List<Control>{
                    spreadSheetCard,
                    spreadSheetLog,
                    spreadSheetInd,
                    buttonSelectLog,
                    buttonSelectInd })
                {
                    control.Enabled = !value;
                }

                busy = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return empty;
            }

            set
            {
                empty = value;
                menuItemSample.Enabled = !value;
            }
        }

        public bool IsChanged
        {
            get
            {
                return ChangedCards.Count > 0;
            }
        }



        private List<Data.LogRow> selectedLogRows;

        private bool busy;

        private bool empty;

        private List<string> ChangedCards = new List<string>();

        private bool IsClosing = false;



        private ResourceManager localizer = new ResourceManager(typeof(MainForm));



        private void UpdateSummary()
        {
            FullStack = new CardStack(data);

            IsEmpty = data.Card.Count == 0;

            if (IsEmpty)
            {
                Text = EntryAssemblyInfo.Title;
                labelArtefacts.Visible = pictureBoxArtefacts.Visible = false;

                statusQuantity.ResetFormatted(Constants.Null);
                statusMass.ResetFormatted(Constants.Null);

                IsBusy = false;
            }
            else
            {
                string friendly = System.IO.Path.GetFileName(data.Card.CommonPath);

                if (string.IsNullOrWhiteSpace(friendly)) {
                    friendly = Mayfly.Resources.Interface.VariousSources;
                }

                UserSettings.Interface.SaveDialog.FileName = friendly;
                this.Text = String.Format("{0} - {1}", friendly, EntryAssemblyInfo.Title);

                Log.Write("{0} cards loaded (common path: {1}).",
                    data.Card.Count, data.Card.CommonPath);

                spreadSheetCard.ClearInsertedColumns();

                foreach (Data.FactorRow factorRow in data.Factor)
                {
                    DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                    gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
                }

                #region Update general values

                labelCardsNumber.Text = data.Card.Count.ToString();

                if (FullStack.GetDates().Count() > 1)
                {
                    labelDateStart.Text = FullStack.EarliestEvent.ToLongDateString();
                    labelDateEnd.Text = FullStack.LatestEvent.ToLongDateString();
                }
                else if (FullStack.GetDates().Count() == 1)
                {
                    labelDateStart.Text = FullStack.EarliestEvent.ToLongDateString();
                }
                else
                {
                    labelDateStart.Text = string.Empty;
                    labelDateEnd.Text = string.Empty;
                }

                listViewWaters.Items.Clear();
                foreach (Data.WaterRow waterRow in data.Water)
                {
                    if (waterRow.IsWaterNull())
                    {
                        listViewWaters.CreateItem(waterRow.ID.ToString(), Waters.Resources.Interface.Unnamed, waterRow.Type - 1);
                    }
                    else
                    {
                        listViewWaters.CreateItem(waterRow.ID.ToString(), waterRow.Water, waterRow.Type - 1);
                    }
                }

                #endregion

                #region Authorities

                listViewInvestigators.Items.Clear();

                foreach (string investigator in FullStack.GetInvestigators())
                {
                    listViewInvestigators.CreateItem(investigator, investigator);
                }

                #endregion

                double q = FullStack.Quantity();
                double w = FullStack.Mass();

                statusQuantity.ResetFormatted(Wild.Service.GetFriendlyQuantity((int)q));
                statusMass.ResetFormatted(Wild.Service.GetFriendlyMass(w / 1000));
            }
        }

        private void UpdateIndTotals()
        {
            labelIndCount.UpdateStatus(spreadSheetInd.VisibleRowCount);
        }

        

        private Data.IndividualRow GetIndividualRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnIndID.Name].Value;
            return data.Individual.FindByID(ID);
        }
        
        private DataGridViewRow GetIndividualRow(Data.IndividualRow individualRow)
        {
            return columnIndID.GetRow(individualRow.ID, true, true);
        }



        private DataGridViewRow[] GetCardLogRows(Data.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (LogRow(gridRow).CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private DataGridViewRow[] GetCardIndividualRows(Data.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (GetIndividualRow(gridRow).LogRow.CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private DataGridViewRow[] GetLogIndividualRows(Data.LogRow logRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (GetIndividualRow(gridRow).LogRow == logRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }



        private DialogResult CheckAndSave()
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSave.ShowDialog(this);

                if (b == tdbSaveAll)
                {
                    SaveCards();
                    return DialogResult.OK;
                }
                else if (b == tdbCancelClose)
                {
                    return DialogResult.Cancel;
                }
            }

            return DialogResult.No;
        }

        private void SaveCards()
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsSaving);

            dataSaver.RunWorkerAsync();
        }

        private void SaveCards(BackgroundWorker worker, DoWorkEventArgs e)
        {
            int index = 0;
            foreach (Data.CardRow cardRow in data.Card)
            {
                SaveCard(cardRow);
                index++;
                worker.ReportProgress(index);
            }
        }

        private void SaveCard(Data.CardRow cardRow)
        {
            if (cardRow.Path == null) return;

            if (ChangedCards.Contains(cardRow.Path))
            {
                Data data = cardRow.SingleCardDataset();
                if (data != null)
                {
                    data.WriteToFile(cardRow.Path);
                    ChangedCards.Remove(cardRow.Path);
                }
            }
        }




        private void SaveIndRow(DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow = GetIndividualRow(gridRow);

            if (individualRow == null) return;

            object length = gridRow.Cells[columnIndLength.Name].Value;
            if (length == null) individualRow.SetLengthNull();
            else individualRow.Length = (double)length;

            object diameter = gridRow.Cells[ColumnIndDiameter.Name].Value;
            if (length == null) individualRow.SetWidthNull();
            else individualRow.Width = (double)diameter;

            //object mass = gridRow.Cells[columnIndMass.Name].Value;
            //if (mass == null)
            //{
            //    individualRow.SetMassNull();
            //    individualRow.LogRow.SetMassNull();
            //}
            //else
            //{
            //    if (individualRow.LogRow.IsMassNull())
            //    {
            //        individualRow.LogRow.Mass = individualRow.LogRow.Mass;
            //    }
            //    else
            //    {
            //        if (!individualRow.IsMassNull()) individualRow.LogRow.Mass -= individualRow.Mass;
            //        individualRow.LogRow.Mass += (double)mass;
            //    }

            //    UpdateLogRow(individualRow.LogRow);

            //    individualRow.Mass = (double)mass;
            //}

            object comments = gridRow.Cells[columnIndComments.Name].Value;
            if (comments == null) individualRow.SetCommentsNull();
            else individualRow.Comments = (string)comments;

            // Additional variables
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

            RememberChanged(individualRow.LogRow.CardRow);
        }



        public void LoadCards(string[] entries)
        {
            IsBusy = true;
            string[] filenames = FileSystem.MaskedNames(entries, Bacterioplankton.UserSettings.Interface.Extension);
            spreadSheetCard.StartProcessing(filenames.Length, Wild.Resources.Interface.Process.CardsLoading);
            loaderData.RunWorkerAsync(filenames);
        }


        private DataGridViewRow UpdateLogRow(Data.LogRow logRow)
        {
            DataGridViewRow result = GetLine(logRow.CardRow, logRow.SpeciesRow);

            if (logRow.IsSpcIDNull()) {
                result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            } else {
                result.Cells[columnLogSpc.Index].Value = logRow.SpeciesRow.Species;
            }

            result.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
            result.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();

            SetCardValue(logRow.CardRow, result, spreadSheetLog.GetInsertedColumns());

            return result;
        }

        private DataGridViewRow UpdateIndividualRow(Data.IndividualRow individualRow)
        {
            DataGridViewRow result = GetIndividualRow(individualRow);

            if (individualRow.LogRow.IsSpcIDNull())
            {
                result.Cells[columnIndSpecies.Index].Value = null;
            }
            else
            {
                result.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.SpeciesRow.Species;
            }

            if (individualRow.IsLengthNull()) { }
            else
            {
                result.Cells[columnIndLength.Index].Value = individualRow.Length;
            }

            //if (individualRow.IsMassNull())
            //{
            //    result.Cells[columnIndMass.Index].Style.NullValue =
            //        individualRow.RecoveredMass == 0 ? null :
            //        individualRow.RecoveredMass.ToString(columnIndMass.DefaultCellStyle.Format);
            //}
            //else
            //{
            //    result.Cells[columnIndMass.Index].Value = individualRow.Mass;
            //}

            if (individualRow.IsCommentsNull()) { }
            else
            {
                result.Cells[columnIndComments.Index].Value = individualRow.Comments;
            }

            SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());

            foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (!valueRow.IsValueNull())
                {
                    result.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
                }
            }

            return result;
        }



        private bool LoadCardAddt(SpreadSheet spreadSheet)
        {
            SelectionValue selectionValue = new SelectionValue(spreadSheetCard);
            selectionValue.Picker.UserSelectedColumns = spreadSheet.GetInsertedColumns();

            if (selectionValue.ShowDialog(this) != DialogResult.OK) return false;

            bool newInserted = false;
            int i = spreadSheet.InsertedColumnCount;
            foreach (DataGridViewColumn gridColumn in spreadSheet.GetInsertedColumns())
            {
                if (gridColumn.Name.Contains("Var")) continue;
                if (selectionValue.Picker.IsSelected(gridColumn)) continue;
                spreadSheet.Columns.Remove(gridColumn);
                i--;
            }

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
            {
                if (spreadSheet.GetColumn(gridColumn.Name) == null)
                {
                    spreadSheet.InsertColumn(gridColumn, i, gridColumn.Name.TrimStart("columnCard".ToCharArray()));
                    newInserted = true;
                    i++;
                }
            }

            return newInserted;
        }

        private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns)
        {
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                SetCardValue(cardRow, gridRow, gridColumn);
            }
        }

        private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        {
            SetCardValue(cardRow, gridRow, gridColumn, gridColumn.Name);
        }

        private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn, string field)
        {
            gridRow.Cells[gridColumn.Index].Value = cardRow.Get(field);
        }

        private delegate void ValueSetEventHandler(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);



        private void SetIndividualMassTip(DataGridViewRow gridRow)
        {
            SetIndividualMassTip(gridRow, GetIndividualRow(gridRow));
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

            //double mass = data.WeightModels.GetValue(
            //    individualRow.Species,
            //    individualRow.Length);

            //gridRow.Cells[columnIndMass.Index].SetNullValue(double.IsNaN(mass) ?
            //    Wild.Resources.Interface.Interface.SuggestionUnavailable :
            //    mass.ToString(columnIndMass.DefaultCellStyle.Format));
        }



        private void UpdateSpeciesChart(Chart chart, bool showLegend)
        {
            if (chart.Series.Count != 1)
                throw new ArgumentException("Chart should contain single data series");

            chart.Series[0].Points.Clear();
            chart.Legends.Clear();

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                chart.Series[0].Points.Add(SpeciesDataPoint(speciesRow));
            }

            chart.Series[0].Sort(PointSortOrder.Descending);

            while (chart.Series[0].Points.Count > 6)
            {
                chart.Series[0].Points.RemoveAt(6);
            }

            chart.Series[0].Sort(PointSortOrder.Ascending);

            if (showLegend)
            {
                chart.Legends.Add(new Legend());
            }
        }

        private DataPoint SpeciesDataPoint(Data.SpeciesRow speciesRow)
        {
            DataPoint dataPoint = new DataPoint();
            double quantity = FullStack.Quantity(speciesRow);
            dataPoint.SetCustomProperty("Species", speciesRow.Species);

            dataPoint.YValues[0] = quantity;
            dataPoint.ToolTip = string.Format("{0}\n{1}", speciesRow.Species,
                quantity.ToString((string)localizer.GetObject("StatusQuantity.Text")));

            dataPoint.AxisLabel =
                //dataPoint.Label =
                speciesRow.Species;

            return dataPoint;
        }
        
        #region Individuals

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

        private void indLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            for (int i = 0; i < data.Individual.Count; i++)
            {
                result.Add(UpdateIndividualRow(data.Individual[i]));
                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void indLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetInd.Rows.AddRange(e.Result as DataGridViewRow[]);
            spreadSheetInd.SetColumnsVisibility(new DataGridViewColumn[] {
                columnIndLength, ColumnIndDiameter }, true);
            UpdateIndTotals();
            IsBusy = false;
            spreadSheetInd.StopProcessing();
        }

        #endregion

        #region Artefacts

        private int FindArtefacts()
        {
            int artefactCount = 0;
            artefactCount += FindSpeciesArtefacts();
            return artefactCount;
        }

        private int FindSpeciesArtefacts()
        {
            int artefactCount = 0;

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                double q = FullStack.Quantity(speciesRow);

                #region In key missing

                SpeciesKey.SpeciesRow spcRow = Bacterioplankton.UserSettings.SpeciesIndex.Species.FindBySpecies(
                    speciesRow.Species);

                if (spcRow == null)
                {
                    artefactCount++;
                }

                #endregion
            }

            labelArtefactSpeciesCount.UpdateStatus(artefactCount);
            labelArtefactSpecies.Visible = pictureBoxArtefactSpecies.Visible = artefactCount == 0;
            return artefactCount;
        }

        private void LoadArtefacts()
        {
            spreadSheetArtefactSpecies.Rows.Clear();

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetArtefactSpecies);

                gridRow.Cells[columnArtefactSpecies.Index].Value = speciesRow.Species;
                double q = FullStack.Quantity(speciesRow);
                gridRow.Cells[columnArtefactN.Index].Value = q;

                spreadSheetArtefactSpecies.Rows.Add(gridRow);
            }
        }

        #endregion

        private void RememberChanged(Data.CardRow cardRow)
        {
            if (cardRow.Path == null) return;

            if (!ChangedCards.Contains(cardRow.Path))
            {
                ChangedCards.Add(cardRow.Path);
            }

            menuItemSave.Enabled = IsChanged;
        }
    }
}
