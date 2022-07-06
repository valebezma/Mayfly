using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using Mayfly.Wild.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
{
    public partial class Individuals : Form
    {
        public Data.LogRow LogRow;

        private Data Data { get; set; }

        public bool IsChanged;

        public bool ChangesWereMade;

        public DataGridViewRow LogLine;

        public double Mass
        {
            get
            {
                if (numericUpDownMass.Enabled)
                {
                    return (double)numericUpDownMass.Value;
                }
                else
                {
                    return double.NaN;
                }
            }

            set
            {
                if (double.IsNaN(value))
                {
                    numericUpDownMass.Enabled = false;
                    numericUpDownMass.Value = decimal.Zero;
                }
                else
                {
                    numericUpDownMass.Enabled = true;
                    numericUpDownMass.Value = (decimal)value;
                }
            }
        }

        public double DetailedMass
        {
            get
            {
                if (stratifiedSample.TotalCount > 0)
                {
                    return double.NaN;
                }

                double result = 0;

                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    if (gridRow.Cells[ColumnMass.Index].Value == null) return double.NaN;

                    if (gridRow.Cells[ColumnMass.Index].Value is double @double)
                    {
                        result += @double;
                    }
                }

                return result; // Math.Round(result, 3);
            }
        }

        public int Quantity
        {
            get
            {
                return (int)numericUpDownQuantity.Value;
            }

            set
            {
                numericUpDownQuantity.Value = (decimal)value;
            }
        }

        public int DetailedQuantity
        {
            get
            {
                return spreadSheetLog.RowCount - 1 + stratifiedSample.TotalCount;
            }
        }

        public bool IsLenghtRangeShown;

        private bool IsThereUnsavedIndividuals
        {
            get
            {
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    if (gridRow.Visible)
                    {
                        if (gridRow.Cells[ColumnID.Index].Value == null)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        private DataGridViewColumn SelectedColumn;

        public string[] AdditionalVariables
        {
            get
            {
                List<string> result = new List<string>(Fish.UserSettings.AddtVariables);

                foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                {
                    if (result.Contains(gridColumn.HeaderText)) continue;

                    result.Add(gridColumn.HeaderText);
                }

                return result.ToArray();
            }
        }

        public string[] CurrentVariables
        {
            get
            {
                List<string> result = new List<string>();

                foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                {
                    if (!gridColumn.Visible) continue;

                    result.Add(gridColumn.Name);
                }

                return result.ToArray();
            }
        }

        private double PrevDetailedMass;

        private int PrevDetailedQuantity;

        public Surveys Observations { get; set; }

        public Surveys.ActionRow ActionRow { get; set; }





        public Individuals(Data.LogRow logRow)
        {
            InitializeComponent();

            LogRow = logRow;
            Data = (Data)LogRow.Table.DataSet;
            if (logRow.IsDefIDNull())
            {
                Text = string.Format(Wild.Resources.Interface.Interface.IndLog,
                    Species.Resources.Interface.UnidentifiedTitle);
            }
            else
            {
                Text = string.Format(Wild.Resources.Interface.Interface.IndLog,
                    logRow.DefinitionRow.Taxon);
            }

            ColumnLength.ValueType = typeof(double);
            ColumnMass.ValueType = typeof(double);
            ColumnSomaticMass.ValueType = typeof(double);
            //ColumnRegID.ValueType = typeof(string);
            ColumnSex.ValueType = typeof(Sex);
            ColumnMaturity.ValueType = typeof(Maturity);
            ColumnGonadMass.ValueType = typeof(double);
            ColumnGonadSampleMass.ValueType = typeof(double);
            ColumnComments.ValueType = typeof(string);

            if (!LogRow.IsMassNull())
            {
                Mass = LogRow.Mass;
            }

            if (!LogRow.IsQuantityNull())
            {
                Quantity = LogRow.Quantity;
            }

            if (!LogRow.IsCommentsNull())
            {
                textBoxComments.Text = LogRow.Comments;
            }

            AddVariableMenuItems();
            InsertCurrentVariableColumns();

            Data.IndividualRow[] individualRows = LogRow.GetIndividualRows();

            if (individualRows.Length == 0)
            {
                if (!LogRow.IsQuantityNull() && LogRow.Quantity == 1 && !LogRow.IsMassNull())
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetLog);
                    gridRow.Cells[ColumnMass.Index].Value = LogRow.Mass * 1000;
                    spreadSheetLog.Rows.Add(gridRow);
                    ChangesWereMade = true;
                }
            }
            else
            {
                foreach (Data.VariableRow variableRow in Data.Variable.Rows)
                {
                    spreadSheetLog.InsertColumn(variableRow.Variable, spreadSheetLog.ColumnCount - 1);
                }

                foreach (Data.IndividualRow individualRow in individualRows)
                {
                    InsertIndividualRow(individualRow);
                }

                foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                {
                    if (spreadSheetLog.IsEmpty(gridColumn))
                    {
                        Width -= gridColumn.Width;
                        spreadSheetLog.Columns.Remove(gridColumn);
                    }
                }
            }

            Data.StratifiedRow[] stratifiedRows = LogRow.GetStratifiedRows();

            if (stratifiedRows.Length > 0)
            {
                stratifiedSample.Reset(LogRow.MinStrate.LeftEndpoint, LogRow.MaxStrate.LeftEndpoint);

                foreach (Data.StratifiedRow stratifiedRow in stratifiedRows)
                {
                    stratifiedSample.FindCounter(stratifiedRow.Class).Count = stratifiedRow.Count;
                }

                ShowLengthRange();
            }

            numericUpDownMass.Enabled = numericUpDownQuantity.Enabled = !Fish.UserSettings.FixTotals;

            //spreadSheetLog.CurrentCell = spreadSheetLog.FirstClearCell();

            UpdateTotals();
            IsChanged = false;
        }



        #region Methods

        private void UpdateTotals()
        {
            double MassDifference = DetailedMass - PrevDetailedMass;
            int QuantityDifference = DetailedQuantity - PrevDetailedQuantity;

            PrevDetailedMass = DetailedMass;
            PrevDetailedQuantity = DetailedQuantity;

            if (Fish.UserSettings.AutoIncreaseBio)
            {
                if (!double.IsNaN(DetailedMass) && DetailedMass > Mass * 1000 || double.IsNaN(Mass))
                {
                    Mass = DetailedMass / 1000;
                }

                if (DetailedQuantity > Quantity)
                {
                    Quantity = DetailedQuantity;
                }
            }
            else
            {
                pictureBoxWarningQuantity.Visible = (DetailedQuantity > Quantity);
                pictureBoxWarningMass.Visible = (DetailedMass > (1000 * Mass));
            }

            if (Fish.UserSettings.AutoDecreaseBio && MassDifference < 0)
            {
                Mass += (MassDifference / 1000);
            }

            if (Fish.UserSettings.AutoDecreaseBio && QuantityDifference < 0)
            {
                Quantity += QuantityDifference;
            }
        }

        public EventHandler Updater;

        private void ClearEmptyRows()
        {
            foreach (DataGridViewRow Row in spreadSheetLog.Rows)
            {
                if (Row.IsNewRow) continue;

                if (spreadSheetLog.IsEmpty(Row))
                {
                    Clear(Row);
                    spreadSheetLog.Rows.Remove(Row);
                }
            }
        }

        private void Clear(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnID.Index].Value != null)
            {
                Data.IndividualRow individualRow =
                    Data.Individual.FindByID(
                    (int)gridRow.Cells[ColumnID.Index].Value);

                if (individualRow != null)
                {
                    individualRow.Delete();
                }
            }
        }

        private void Clear(DataGridViewColumn gridColumn)
        {
            for (int i = 0; i < spreadSheetLog.RowCount; i++)
            {
                spreadSheetLog[gridColumn.Index, i].Value = null;
            }
        }

        private void ShowLengthRange()
        {
            if (!IsLenghtRangeShown)
            {
                stratifiedSample.Visible = true;
                spreadSheetLog.Height -= stratifiedSample.Height;
                IsLenghtRangeShown = true;
            }
        }

        private void HideLengthRange()
        {
            if (IsLenghtRangeShown)
            {
                stratifiedSample.Visible = false;
                spreadSheetLog.Height += stratifiedSample.Height;
                IsLenghtRangeShown = false;
                stratifiedSample.Reset();
            }
        }

        private void AddVariableMenuItems()
        {
            while (contextMenuStripAdd.Items.Count > 7)
            {
                contextMenuStripAdd.Items.RemoveAt(5);
            }

            if (Fish.UserSettings.AddtVariables != null)
            {
                foreach (string addtVar in Fish.UserSettings.AddtVariables)
                {
                    ToolStripMenuItem newVarMenu = new ToolStripMenuItem(addtVar);
                    newVarMenu.Click += new EventHandler(CustomVar_Click);
                    contextMenuStripAdd.Items.Insert(5, newVarMenu);
                }
            }
        }

        private void InsertCurrentVariableColumns()
        {
            if (Fish.UserSettings.CurrentVariables != null)
            {
                foreach (string currVar in Fish.UserSettings.CurrentVariables)
                {
                    spreadSheetLog.InsertColumn(currVar, spreadSheetLog.ColumnCount - 1);
                }
            }
        }

        private void SaveData()
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (gridRow.IsNewRow)
                {
                    continue;
                }

                SaveIndividualRow(gridRow);
            }

            Data.Individual.AcceptChanges();

            if (stratifiedSample.Visible)
            {
                LogRow.Interval = stratifiedSample.Interval;

                foreach (SizeGroup sizeGroup in stratifiedSample.SizeGroups)
                {
                    Data.StratifiedRow stratifiedRow = Data.Stratified.FindByLogIDClass(LogRow.ID, 
                        sizeGroup.LeftEndpoint);

                    if (stratifiedRow == null)
                    {
                        if (sizeGroup.Count > 0)
                        {
                            Data.Stratified.AddStratifiedRow(LogRow, sizeGroup.LeftEndpoint, sizeGroup.Count);
                        }
                    }
                    else
                    {
                        if (sizeGroup.Count == 0)
                        {
                            stratifiedRow.Delete();
                        }
                        else
                        {
                            stratifiedRow.Count = sizeGroup.Count;
                        }
                    }
                }
            }

            Data.Stratified.AcceptChanges();

            UpdateLogRow();

            IsChanged = false;
        }

        private void UpdateLogRow()
        {
            if (Quantity > 0)
            {
                LogRow.Quantity = Quantity;
                LogLine.Cells["ColumnQuantity"].Value = LogRow.Quantity;
            }
            else
            {
                LogRow.SetQuantityNull();
                LogLine.Cells["ColumnQuantity"].Value = null;
            }

            if (!double.IsNaN(Mass) && Mass > 0)
            {
                LogRow.Mass = Mass;
                LogLine.Cells["ColumnMass"].Value = LogRow.Mass;
            }
            else
            {
                LogRow.SetMassNull();
                LogLine.Cells["ColumnMass"].Value = null;
            }

            if (textBoxComments.Text.IsAcceptable())
            {
                LogRow.Comments = textBoxComments.Text;
                LogLine.Cells["ColumnSpecies"].ToolTipText = LogRow.Comments.InsertBreaks(35);
            }
            else
            {
                LogRow.SetCommentsNull();
                LogLine.Cells["ColumnSpecies"].ToolTipText = string.Empty;
            }


            if (LogLine != null)
            {
                if (LogLine.DataGridView.FindForm() is Card card)
                {
                    card.UpdateStatus();
                }

                if (Updater != null)
                {
                    Updater.Invoke(LogLine, new EventArgs());
                }
            }
        }

        private bool IsAlreadySaved(DataGridViewRow gridRow)
        {
            return gridRow.Cells[ColumnID.Index].Value == null;
        }

        private Data.IndividualRow IndividualRow(DataGridViewRow gridRow)
        {
            return IndividualRow(Data, LogRow, gridRow);
        }

        private Data.IndividualRow IndividualRow(Data data, Data.LogRow logRow, DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow;

            if (gridRow.Cells[ColumnID.Index].Value != null)
            {
                individualRow = data.Individual.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
                if (individualRow != null)
                {
                    goto Saving;
                }
            }

            individualRow = data.Individual.NewIndividualRow();
            individualRow.LogRow = logRow;

        Saving:

            if (gridRow.Cells[ColumnLength.Index].Value == null)
            {
                individualRow.SetLengthNull();
            }
            else
            {
                individualRow.Length = (double)gridRow.Cells[ColumnLength.Index].Value;
            }

            if (gridRow.Cells[ColumnMass.Index].Value == null)
            {
                individualRow.SetMassNull();
            }
            else
            {
                individualRow.Mass = (double)gridRow.Cells[ColumnMass.Index].Value;
            }

            if (gridRow.Cells[ColumnSomaticMass.Index].Value == null)
            {
                individualRow.SetSomaticMassNull();
            }
            else
            {
                individualRow.SomaticMass = (double)gridRow.Cells[ColumnSomaticMass.Index].Value;
            }

            if (gridRow.Cells[ColumnRegID.Index].Value == null)
            {
                individualRow.SetTallyNull();
            }
            else
            {
                individualRow.Tally = (string)gridRow.Cells[ColumnRegID.Index].Value;
            }

            if (gridRow.Cells[ColumnSex.Index].Value == null)
            {
                individualRow.SetSexNull();
            }
            else
            {
                individualRow.Sex = ((Sex)gridRow.Cells[ColumnSex.Index].Value).Value;
            }

            if (gridRow.Cells[ColumnMaturity.Index].Value == null)
            {
                individualRow.SetMaturityNull();
                individualRow.SetIntermatureNull();
            }
            else
            {
                individualRow.Maturity = ((Maturity)gridRow.Cells[ColumnMaturity.Index].Value).Value;
                individualRow.Intermature = ((Maturity)gridRow.Cells[ColumnMaturity.Index].Value).IsIntermediate;
            }

            if (gridRow.Cells[ColumnGonadMass.Index].Value == null)
            {
                individualRow.SetGonadMassNull();
            }
            else
            {
                individualRow.GonadMass = (double)gridRow.Cells[ColumnGonadMass.Index].Value;
            }

            if (gridRow.Cells[ColumnGonadSampleMass.Index].Value == null)
            {
                individualRow.SetGonadSampleMassNull();
            }
            else
            {
                individualRow.GonadSampleMass = (double)gridRow.Cells[ColumnGonadSampleMass.Index].Value;
            }

            if (gridRow.Cells[ColumnComments.Index].Value == null)
            {
                individualRow.SetCommentsNull();
            }
            else
            {
                individualRow.Comments = (string)gridRow.Cells[ColumnComments.Index].Value;
            }

            SaveAddtValues(data, individualRow, gridRow);

            return individualRow;
        }

        private Data.IndividualRow SaveIndividualRow(DataGridViewRow gridRow)
        {
            return SaveIndividualRow(Data, LogRow, gridRow);
        }

        private Data.IndividualRow SaveIndividualRow(Data data, Data.LogRow logRow, DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow = IndividualRow(data, logRow, gridRow);

            if (data.Individual.Rows.IndexOf(individualRow) == -1)
            {
                data.Individual.AddIndividualRow(individualRow);
            }

            if (data == Data)
            {
                gridRow.Cells[ColumnID.Index].Value = individualRow.ID;
                gridRow.HeaderCell.Value = Math.Abs(individualRow.ID);
            }

            return individualRow;
        }

        private void SaveAddtValues(Data data, Data.IndividualRow individualRow, DataGridViewRow gridRow)
        {
            foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
            {
                if (gridRow.Cells[gridColumn.Index].Value == null)
                {
                    Data.VariableRow CurrentVarRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                    if (CurrentVarRow == null) continue;

                    Data.ValueRow ValueRow = data.Value.FindByIndIDVarID(individualRow.ID, CurrentVarRow.ID);

                    if (ValueRow == null) continue;

                    ValueRow.Delete();
                }
                else
                {
                    Data.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null)
                    {
                        data.Value.AddValueRow(individualRow, variableRow, (double)gridRow.Cells[gridColumn.Index].Value);
                    }
                    else
                    {
                        valueRow.Value = (double)gridRow.Cells[gridColumn.Index].Value;
                    }
                }
            }
        }

        private DataGridViewRow InsertIndividualRow(Data.IndividualRow individualRow)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLog);
            gridRow.HeaderCell.Value = Math.Abs(individualRow.ID);

            gridRow.Cells[ColumnID.Index].Value = individualRow.ID;

            if (individualRow.IsLengthNull()) { }
            else
            {
                gridRow.Cells[ColumnLength.Index].Value = individualRow.Length;
            }

            if (individualRow.IsMassNull()) { }
            else
            {
                gridRow.Cells[ColumnMass.Index].Value = individualRow.Mass;
            }

            if (individualRow.IsSomaticMassNull()) { }
            else
            {
                gridRow.Cells[ColumnSomaticMass.Index].Value = individualRow.SomaticMass;
            }

            if (individualRow.IsTallyNull()) { }
            else
            {
                gridRow.Cells[ColumnRegID.Index].Value = individualRow.Tally;
            }

            if (individualRow.IsSexNull()) { }
            else
            {
                gridRow.Cells[ColumnSex.Index].Value = new Sex(individualRow.Sex);
            }

            if (individualRow.IsMaturityNull()) { }
            else
            {
                if (individualRow.IsIntermatureNull())
                {
                    gridRow.Cells[ColumnMaturity.Index].Value = new Maturity(individualRow.Maturity);
                }
                else
                {
                    gridRow.Cells[ColumnMaturity.Index].Value = new Maturity(individualRow.Maturity, 
                        individualRow.Intermature);
                }
            }

            if (individualRow.IsGonadMassNull()) { }
            else
            {
                gridRow.Cells[ColumnGonadMass.Index].Value = individualRow.GonadMass;
            }

            if (individualRow.IsGonadSampleMassNull()) { }
            else
            {
                gridRow.Cells[ColumnGonadSampleMass.Index].Value = individualRow.GonadSampleMass;
            }

            if (individualRow.IsCommentsNull()) { }
            else
            {
                gridRow.Cells[ColumnComments.Index].Value = individualRow.Comments;
            }

            foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (!valueRow.IsValueNull())
                {
                    gridRow.Cells[spreadSheetLog.GetColumn(valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
                }
            }

            spreadSheetLog.Rows.Add(gridRow);

            return gridRow;
        }

        #endregion



        private void Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        #region Add menu

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            contextMenuStripAdd.Show(buttonAdd, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void ToolStripMenuItemStratifiedSample_Click(object sender, EventArgs e)
        {
            double minLengthClass = double.MaxValue;
            double maxLengthClass = double.MinValue;

            for (int i = 0; i < spreadSheetLog.RowCount - 1; i++)
            {
                if (spreadSheetLog[ColumnLength.Name, i].Value.IsDoubleConvertible())
                {
                    double currentLength = (double)spreadSheetLog[ColumnLength.Name, i].Value;

                    if (currentLength < minLengthClass)
                    {
                        minLengthClass = currentLength;
                    }

                    if (currentLength > maxLengthClass)
                    {
                        maxLengthClass = currentLength;
                    }
                }
            }

            if (minLengthClass != double.MaxValue && maxLengthClass != double.MinValue)
            {
                minLengthClass = Math.Floor(minLengthClass / stratifiedSample.Interval) * stratifiedSample.Interval;
                maxLengthClass = Math.Floor(maxLengthClass / stratifiedSample.Interval) * stratifiedSample.Interval + stratifiedSample.Interval;

                stratifiedSample.Reset(minLengthClass, maxLengthClass);
            }

            if (stratifiedSample.Properties.ShowDialog(this) == DialogResult.OK)
            {
                ShowLengthRange();
            }
        }

        private void ToolStripMenuItemNewVar_Click(object sender, EventArgs e)
        {
            spreadSheetLog.InsertColumn(spreadSheetLog.ColumnCount - 1);
        }

        private void CustomVar_Click(object sender, EventArgs e)
        {
            string variable = ((ToolStripMenuItem)sender).Text;
            spreadSheetLog.InsertColumn(variable, spreadSheetLog.ColumnCount - 1);
            spreadSheetLog.CurrentCell = spreadSheetLog[variable, spreadSheetLog.CurrentRow.Index];
            spreadSheetLog.Focus();
        }

        private void somaticWeightGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnSomaticMass);
        }

        private void gonadWeightGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnGonadMass);
        }

        private void fecunditySampleWeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnGonadSampleMass);
        }

        #endregion

        #region Totals logics

        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            pictureBoxWarningQuantity.Visible = (DetailedQuantity > Quantity);
            IsChanged = true;
        }

        private void numericUpDownMass_ValueChanged(object sender, EventArgs e)
        {
            pictureBoxWarningMass.Visible = (DetailedMass > (Mass * 1000));
            IsChanged = true;
        }

        private void pictureBoxWarningQuantity_DoubleClick(object sender, EventArgs e)
        {
            Quantity = DetailedQuantity;
            IsChanged = true;
        }

        private void pictureBoxWarningMass_DoubleClick(object sender, EventArgs e)
        {
            Mass = DetailedMass / 1000;
            IsChanged = true;
        }

        private void pictureBoxWarningM_MouseHover(object sender, EventArgs e)
        {
            toolTipAttention.ToolTipTitle = Wild.Resources.Interface.Messages.MassInequal;
            toolTipAttention.Show(string.Format(Fish.Resources.Interface.Messages.MassSetEqual,
                DetailedMass, 1000 * Mass), numericUpDownMass, 0, numericUpDownMass.Height);
        }

        private void pictureBoxWarningM_MouseLeave(object sender, EventArgs e)
        {
            toolTipAttention.Hide(numericUpDownMass);
        }

        private void pictureBoxWarningQ_MouseHover(object sender, EventArgs e)
        {
            toolTipAttention.ToolTipTitle = Wild.Resources.Interface.Messages.QuantityInequal;
            toolTipAttention.Show(string.Format(Wild.Resources.Interface.Messages.QuantitySetEqual,
                DetailedQuantity, Quantity), numericUpDownQuantity, 0, numericUpDownQuantity.Height);
        }

        private void pictureBoxWarningQ_MouseLeave(object sender, EventArgs e)
        {
            toolTipAttention.Hide(numericUpDownQuantity);
        }

        #endregion

        #region Grid Log logics

        private void spreadSheetLog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 10 & ModifierKeys.HasFlag(Keys.Control))
            {
                buttonOK.PerformClick();
            }
        }

        private void spreadSheetLog_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnSex.Index)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(CultureInfo.InvariantCulture);
            }
            else
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }

        private void spreadSheetLog_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            Data.IndividualRow individualRow = SaveIndividualRow(spreadSheetLog.Rows[e.RowIndex]);
            ActionRow.CatchXML = Data.GetXml();

            if (e.ColumnIndex == ColumnLength.Index)
            {
                if (spreadSheetLog[ColumnMass.Index, e.RowIndex].Value == null)
                {
                    int weighted = Observations.GetCombinedData().GetStack().Weighted(LogRow.DefinitionRow.KeyRecord, Service.GetStrate(individualRow.Length));
                    spreadSheetLog[ColumnMass.Index, e.RowIndex].Style.NullValue = weighted < UserSettings.RequiredClassSize ? string.Empty : Resources.Interface.EnoughStamp;
                }

                if (spreadSheetLog[ColumnRegID.Index, e.RowIndex].Value == null)
                {
                    int reged = Observations.GetCombinedData().GetStack().Tallied(LogRow.DefinitionRow.KeyRecord, Service.GetStrate(individualRow.Length));
                    spreadSheetLog[ColumnRegID.Index, e.RowIndex].Style.NullValue = reged < UserSettings.RequiredClassSize ? string.Empty : Resources.Interface.EnoughStamp;
                }
            }

            if (spreadSheetLog.ContainsFocus)
            {
                ClearEmptyRows();
                IsChanged = true;
            }

            //if (double.IsNaN(DetailedMass) && Fish.UserSettings.AutoDecreaseBio &&
            //    Fish.UserSettings.AutoIncreaseBio)
            //{
            //    Mass = double.NaN;
            //}
            //else
            //{
            //    numericUpDownMass.Enabled = true;
            //}

            UpdateTotals();
        }

        private void spreadSheetLog_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Clear(e.Row);
            IsChanged = true;
        }

        private void spreadSheetLog_RowRemoving(object sender, DataGridViewRowEventArgs e)
        {
            Clear(e.Row);
            spreadSheetLog.Rows.Remove(e.Row);
            IsChanged = true;
        }

        private void spreadSheetLog_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int columnIndex = spreadSheetLog.HitTest(e.X, e.Y).ColumnIndex;

                if (columnIndex == -1)
                {
                    SelectedColumn = null;
                }
                else
                {
                    SelectedColumn = spreadSheetLog.Columns[columnIndex];
                }
            }
        }

        private void spreadSheetLog_ColumnRenamed(object sender, GridColumnRenameEventArgs e)
        {
            Data.VariableRow variableRow = Data.Variable.FindByVarName(e.PreviousCaption);

            if (variableRow != null)
            {
                variableRow.Variable = e.Column.HeaderText;
            }

            IsChanged = true;
        }

        private void spreadSheetLog_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            Width -= e.Column.Width;

            Data.VariableRow variableRow = Data.Variable.FindByVarName(e.Column.HeaderText);

            if (variableRow != null)
            {
                for (int i = 0; i < Data.Value.Count; i++)
                {
                    Data.ValueRow valueRow = Data.Value[i];
                    if (valueRow.VariableRow == variableRow &&
                        valueRow.IndividualRow.LogRow == LogRow)
                    {
                        valueRow.Delete();
                        i--;
                    }
                }
            }

            IsChanged = true;
        }

        #endregion

        #region Individual menu

        private void ToolStripMenuItemCut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemCopy_Click(sender, e);
            ToolStripMenuItemDelete_Click(sender, e);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            Data clipData = new Data();

            Data.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            Data.DefinitionRow clipSpeciesRow = clipData.Definition.NewDefinitionRow();
            clipData.Definition.AddDefinitionRow(clipSpeciesRow);

            Data.LogRow clipLogRow = clipData.Log.NewLogRow();
            clipLogRow.CardRow = clipCardRow;
            clipLogRow.DefinitionRow = clipSpeciesRow;
            clipData.Log.AddLogRow(clipLogRow);

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.IsNewRow) continue;
                SaveIndividualRow(clipData, clipLogRow, gridRow);
            }

            Clipboard.SetText(clipData.GetXml());
        }

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            Data clipData = Data.FromClipboard();

            // If clipData contains Deep tables then insert data and rows 
            // otherwise insert just rows

            foreach (Data.VariableRow clipVariableRow in clipData.Variable.Rows)
            {
                spreadSheetLog.InsertColumn(clipVariableRow.Variable);
            }

            foreach (Data.IndividualRow clipIndividualRow in clipData.Individual)
            {
                InsertIndividualRow(clipIndividualRow);
            }

            IsChanged = true;
            UpdateTotals();
            Clipboard.Clear();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            int rowsToDelete = spreadSheetLog.SelectedRows.Count;
            while (rowsToDelete > 0)
            {
                Clear(spreadSheetLog.SelectedRows[0]);
                spreadSheetLog.Rows.Remove(spreadSheetLog.SelectedRows[0]);
                rowsToDelete--;
            }

            IsChanged = true;
            UpdateTotals();
        }

        #endregion

        #region Stratified logics

        private void Range_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotals();
            IsChanged = true;
        }

        #endregion

        #region Column menu

        private void contextMenuStripVar_Opening(object sender, CancelEventArgs e)
        {
            //ToolStripMenuItemRemove.Enabled = spreadSheetLog.GetInsertedColumns().Contains(SelectedColumn);
        }

        private void ToolStripMenuItemClearColumn_Click(object sender, EventArgs e)
        {
            Clear(SelectedColumn);
            IsChanged = true;
        }

        private void ToolStripMenuItemSaveVars_Click(object sender, EventArgs e)
        {
            Fish.UserSettings.AddtVariables = AdditionalVariables;
            AddVariableMenuItems();
            Fish.UserSettings.CurrentVariables = CurrentVariables;
        }

        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemSaveVars_Click(sender, e);

            ChangesWereMade |= IsChanged;
            SaveData();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxComments_TextChanged(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        private void Individuals_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK &&
                (IsThereUnsavedIndividuals || IsChanged))
            {
                TaskDialogButton b = taskDialogSave.ShowDialog(this);

                if (b == tdbSaveAllIndividuals)
                {
                    SaveData();
                    DialogResult = DialogResult.OK;
                    ChangesWereMade = true;
                }
                else if (b == tdbDiscard)
                {

                }
                else if (b == tdbCancelClose)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}