using Mayfly.Wild;
using Mayfly.Wild.Controls;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Controls;
using Mayfly.Species;
using System.Collections;

namespace Mayfly.Fish
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
                List<string> result = new List<string>(UserSettings.AddtVariables);

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

        List<Data.IndividualRow> redefinedSpecimen = new List<Data.IndividualRow> ();



        private Individuals()
        {
            InitializeComponent();
            Log.Write("Open individuals form.");

            stratifiedSample.Interval = UserSettings.DefaultStratifiedInterval;

            ColumnLength.ValueType = typeof(double);
            ColumnMass.ValueType = typeof(double);
            ColumnSomaticMass.ValueType = typeof(double);
            ColumnRegID.ValueType = typeof(string);
            ColumnAge.ValueType = typeof(Age);
            ColumnSex.ValueType = typeof(Sex);
            ColumnMaturity.ValueType = typeof(Maturity);
            ColumnGonadMass.ValueType = typeof(double);
            ColumnGonadSampleMass.ValueType = typeof(double);
            ColumnGonadSample.ValueType = typeof(int);
            ColumnEggSize.ValueType = typeof(double);
            ColumnComments.ValueType = typeof(string);

            AddVariableMenuItems();
            InsertCurrentVariableColumns();

            numericUpDownMass.Enabled =
                numericUpDownQuantity.Enabled = 
                !UserSettings.FixTotals;
        }

        public Individuals(Data.LogRow logRow) : this()
        {
            LogRow = logRow;
            Data = (Data)LogRow.Table.DataSet;

            this.ResetFormatted((logRow.IsSpcIDNull() ? Species.Resources.Interface.UnidentifiedTitle : logRow.SpeciesRow.ToString()));

            UpdateValues();
            UpdateTotals();
            UpdateRedefineList();

            IsChanged = false;
        }



        #region Methods

        public void UpdateValues()
        {
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

            spreadSheetLog.Rows.Clear();

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
                double min = LogRow.MinStrate.LeftEndpoint;
                double max = LogRow.MaxStrate.LeftEndpoint;

                stratifiedSample.Interval = LogRow.Interval;
                stratifiedSample.Reset(min, max, true);

                foreach (Data.StratifiedRow stratifiedRow in stratifiedRows)
                {
                    stratifiedSample.FindCounter(stratifiedRow.Class).Count = stratifiedRow.Count;
                }

                ShowLengthRange();
            }

            foreach (DataGridViewColumn col in new DataGridViewColumn[] { 
                ColumnSomaticMass, ColumnGonadMass, ColumnGonadSample, 
                ColumnGonadSampleMass, ColumnEggSize
            })
            {
                col.Visible = !spreadSheetLog.IsEmpty(col);
            }
        }

        private void UpdateRedefineList()
        {
            // Clear domestic
            while (!(contextItemRedefine.DropDownItems[0] is ToolStripSeparator))
            {
                contextItemRedefine.DropDownItems.RemoveAt(0);
            }

            // Clear reference species
            while (contextItemRedefine.DropDownItems.Count > 2)
            {
                contextItemRedefine.DropDownItems.RemoveAt(2);
            }


            // Insert domestic
            foreach (Data.LogRow logRow in LogRow.CardRow.GetLogRows())
            {
                if (logRow == LogRow) continue;

                ToolStripMenuItem item = new ToolStripMenuItem(logRow.SpeciesRow.KeyRecord.ShortName);
                item.Tag = logRow;
                item.Click += redefineDomesticSpecies_Click;
                contextItemRedefine.DropDownItems.Insert(0, item);
            }

            // Insert reference
            contextRedefineAll.DropDownItems.Clear();
            contextRedefineAll.Visible = UserSettings.SpeciesIndex.Species.Count <= Species.UserSettings.AllowableSpeciesListLength;
            if (UserSettings.SpeciesIndex.Species.Count <= Species.UserSettings.AllowableSpeciesListLength)
            {
                foreach (SpeciesKey.SpeciesRow speciesRow in UserSettings.SpeciesIndex.Species.Rows)
                {
                    ToolStripItem speciesItem = new ToolStripMenuItem();
                    speciesItem.Tag = speciesRow;
                    speciesItem.Text = speciesRow.ShortName;
                    speciesItem.Click += new EventHandler(redefineReferenceSpecies_Click);
                    contextRedefineAll.DropDownItems.Add(speciesItem);
                }

                contextRedefineAll.SortItems();
            }

            foreach (SpeciesKey.BaseRow baseRow in UserSettings.SpeciesIndex.Base.Rows)
            {
                ToolStripMenuItem baseItem = new ToolStripMenuItem();
                baseItem.Text = baseRow.BaseName;

                foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows())
                {
                    ToolStripMenuItem taxaItem = new ToolStripMenuItem();
                    taxaItem.Text = taxaRow.TaxonName;

                    foreach (SpeciesKey.RepRow representativeRow in taxaRow.GetRepRows())
                    {
                        ToolStripItem speciesItem = new ToolStripMenuItem();
                        speciesItem.Tag = representativeRow.SpeciesRow;
                        speciesItem.Text = representativeRow.SpeciesRow.ShortName;
                        speciesItem.Click += new EventHandler(redefineReferenceSpecies_Click);

                        taxaItem.DropDownItems.Add(speciesItem);
                    }

                    taxaItem.SortItems();
                    baseItem.DropDownItems.Add(taxaItem);
                }

                baseItem.SortItems();
                contextItemRedefine.DropDownItems.Add(baseItem);
            }
        }

        private void RedefineSelected(Data.LogRow logRow)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                Data.IndividualRow indRow = IndividualRow(gridRow);
                redefinedSpecimen.Add(indRow);
                indRow.LogRow = logRow;
                if (indRow.IsCommentsNull()) { indRow.Comments = string.Format(Wild.Resources.Interface.Interface.RedefineComment, LogRow.SpeciesRow.Species); }
                else { indRow.Comments += string.Format(Environment.NewLine + Wild.Resources.Interface.Interface.RedefineComment, LogRow.SpeciesRow.Species); }

                if (logRow.IsQuantityNull()) logRow.Quantity = 1; else logRow.Quantity += 1;
                if (logRow.IsMassNull()) logRow.Mass = indRow.Mass / 1000.0; else logRow.Mass += (indRow.Mass / 1000.0);
            }

            while (spreadSheetLog.SelectedRows.Count > 0)
            {
                spreadSheetLog.Rows.Remove(spreadSheetLog.SelectedRows[0]);
            }

            UpdateTotals();
            UpdateLogRow();

            Card card = (Card)this.Owner;
            card.UpdateLogRow(logRow);

            //Individuals newlog = card.OpenIndividuals(logRow);
            //newlog.LogLine = card.speciesLogger.FindLine(logRow.SpeciesRow.Species);
            //newlog.UpdateLogRow();
            //newlog.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);

        }

        private void redefineDomesticSpecies_Click(object sender, EventArgs e)
        {
            RedefineSelected((Data.LogRow)((ToolStripMenuItem)sender).Tag);
        }

        private void redefineReferenceSpecies_Click(object sender, EventArgs e)
        {
            SpeciesKey.SpeciesRow speciesRow = (SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag;

            //Data.LogRow logRow = Data.Log.NewLogRow();
            //logRow.SpeciesRow = speciesRow

            Card card = (Card)this.Owner;
            RedefineSelected(card.SaveLogRow(card.speciesLogger.InsertSpecies(speciesRow)));
            UpdateRedefineList();
        }

        private void UpdateTotals()
        { 
            double MassDifference = DetailedMass - PrevDetailedMass;
            int QuantityDifference = DetailedQuantity - PrevDetailedQuantity;

            PrevDetailedMass = DetailedMass;
            PrevDetailedQuantity = DetailedQuantity;

            if (UserSettings.FixTotals)
            {
                Mass = DetailedMass / 1000;
                Quantity = DetailedQuantity;
            }
            else
            {
                if (UserSettings.AutoIncreaseBio)
                {
                    if (!double.IsNaN(DetailedMass) && DetailedMass > Mass * 1000)
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

                if (UserSettings.AutoDecreaseBio && MassDifference < 0)
                {
                    Mass += (MassDifference / 1000);
                }

                if (UserSettings.AutoDecreaseBio && QuantityDifference < 0)
                {
                    Quantity += QuantityDifference;
                }
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
            while (contextMenuStripAdd.Items.Count > 9)
            {
                contextMenuStripAdd.Items.RemoveAt(7);
            }

            if (UserSettings.AddtVariables != null)
            {
                foreach (string addtVar in UserSettings.AddtVariables)
                {
                    ToolStripMenuItem newVarMenu = new ToolStripMenuItem(addtVar);
                    newVarMenu.Click += new EventHandler(CustomVar_Click);
                    contextMenuStripAdd.Items.Insert(7, newVarMenu);
                }
            }
        }

        private void InsertCurrentVariableColumns()
        {
            if (UserSettings.CurrentVariables != null)
            {
                foreach (string currVar in UserSettings.CurrentVariables)
                {
                    spreadSheetLog.InsertColumn(currVar, spreadSheetLog.ColumnCount - 1);
                }
            }
        }

        public void SaveData()
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
                    Data.StratifiedRow stratifiedRow = Data.Stratified.FindByLogIDClass(LogRow.ID, sizeGroup.LeftEndpoint);
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
            if (LogLine == null) return;
            if (LogLine.Index == -1) return;

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
                individualRow.SetRegIDNull();
            }
            else
            {
                individualRow.RegID = (string)gridRow.Cells[ColumnRegID.Index].Value;
            }

            if (gridRow.Cells[ColumnAge.Index].Value == null)
            {
                individualRow.SetAgeNull();
            }
            else
            {
                individualRow.Age = ((Age)gridRow.Cells[ColumnAge.Index].Value).Value;
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

            if (gridRow.Cells[ColumnGonadSample.Index].Value == null)
            {
                individualRow.SetGonadSampleNull();
            }
            else
            {
                individualRow.GonadSample = (int)gridRow.Cells[ColumnGonadSample.Index].Value;
            }

            if (gridRow.Cells[ColumnEggSize.Index].Value == null)
            {
                individualRow.SetEggSizeNull();
            }
            else
            {
                individualRow.EggSize = (double)gridRow.Cells[ColumnEggSize.Index].Value;
            }

            if (gridRow.Cells[ColumnComments.Index].Value == null)
            {
                individualRow.SetCommentsNull();
            }
            else
            {
                individualRow.Comments = (string)gridRow.Cells[ColumnComments.Index].Value;
            }

            SaveAddtValues(individualRow, gridRow);

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

        private void SaveAddtValues(Data.IndividualRow individualRow, DataGridViewRow gridRow)
        {
            foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
            {
                if (gridRow.Cells[gridColumn.Index].Value == null)
                {
                    individualRow.SetAddtValueNull(gridColumn.HeaderText);
                }
                else
                {
                    individualRow.SetAddtValue(gridColumn.HeaderText, (double)gridRow.Cells[gridColumn.Index].Value);
                }
            }
        }

        private DataGridViewRow InsertIndividualRow(Data.IndividualRow individualRow)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLog);
            gridRow.Cells[ColumnID.Index].Value = individualRow.ID;
            spreadSheetLog.Rows.Add(gridRow);
            UpdateIndividualRow(gridRow, individualRow);
            return gridRow;
        }

        private DataGridViewRow FindIndividualRow(Data.IndividualRow individualRow)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (gridRow.Cells[ColumnID.Name].Value == null) continue;

                if ((int)gridRow.Cells[ColumnID.Name].Value == individualRow.ID)
                {
                    return gridRow;
                }
            }

            return null;
        }

        private void UpdateIndividualRow(Data.IndividualRow individualRow)
        {
            UpdateIndividualRow(FindIndividualRow(individualRow), individualRow);
        }

        private void UpdateIndividualRow(DataGridViewRow gridRow, Data.IndividualRow individualRow)
        {
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

            if (individualRow.IsAgeNull()) { }
            else
            {
                gridRow.Cells[ColumnAge.Index].Value = new Age(individualRow.Age);
                Wild.Service.HandleAgeInput(gridRow.Cells[ColumnAge.Index]);
            }

            if (individualRow.IsRegIDNull()) { }
            else
            {
                gridRow.Cells[ColumnRegID.Index].Value = individualRow.RegID;
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

            if (individualRow.IsGonadSampleNull()) { }
            else
            {
                gridRow.Cells[ColumnGonadSample.Index].Value = individualRow.GonadSample;
            }

            if (individualRow.IsEggSizeNull()) { }
            else
            {
                gridRow.Cells[ColumnEggSize.Index].Value = individualRow.EggSize;
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
                    DataGridViewColumn col = spreadSheetLog.GetColumn(valueRow.VariableRow.Variable);
                    if (col != null) gridRow.Cells[col.Index].Value = valueRow.Value;
                }
            }
        }

        private Data.IndividualRow[] GetIndividuals(IList rows)
        {
            spreadSheetLog.EndEdit();
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.IndividualRow individualRow = IndividualRow(gridRow);
                if (individualRow == null) continue;

                result.Add(individualRow);
            }

            return result.ToArray();
        }

        #endregion



        #region Interface logics

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

        private void contextSomaticMass_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnSomaticMass);
        }

        private void contextGonadMass_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnGonadMass);
        }

        private void contextFecunditySampleMass_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnGonadSampleMass);
        }

        private void contextGametesNumber_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnGonadSample);
        }

        private void contextMeanEggDiameter_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnEggSize);
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
            toolTipAttention.Show(string.Format(Resources.Interface.Messages.MassSetEqual,
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

            if (e.ColumnIndex == ColumnAge.Index)
            {
                Wild.Service.HandleAgeInput(spreadSheetLog[e.ColumnIndex, e.RowIndex]);
            }

            if (spreadSheetLog.ContainsFocus)
            {
                ClearEmptyRows();
                IsChanged = true;
            }

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

        private void contextMenuStripInd_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItemPaste.Enabled = Clipboard.ContainsText() &&
                Data.ContainsIndividuals(Clipboard.GetText());
        }

        private void ToolStripMenuItemInd_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.IsNewRow) continue;

                Individual individual = new Individual(SaveIndividualRow(gridRow));
                individual.SetFriendlyDesktopLocation(gridRow);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.Show();
            }
        }

        private void ToolStripMenuItemTrophics_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.IsNewRow) continue;

                Individual individual = new Individual(SaveIndividualRow(gridRow));
                individual.SetFriendlyDesktopLocation(gridRow);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowTrophics();
                individual.Show();
            }
        }

        void individual_ValueChanged(object sender, EventArgs e)
        {
            UpdateIndividualRow(((Individual)sender).IndividualRow);
        }

        void individual_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsChanged &= ((Individual)sender).IsChanged;
        }

        private void ToolStripMenuItemIndPrint_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetLog.SelectedRows).GetReport(CardReportLevel.Profile).Run();
        }

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

            Data.SpeciesRow clipSpeciesRow = clipData.Species.NewSpeciesRow();
            clipData.Species.AddSpeciesRow(clipSpeciesRow);

            Data.LogRow clipLogRow = clipData.Log.NewLogRow();
            clipLogRow.CardRow = clipCardRow;
            clipLogRow.SpeciesRow = clipSpeciesRow;
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
            UserSettings.AddtVariables = AdditionalVariables;
            AddVariableMenuItems();
            UserSettings.CurrentVariables = CurrentVariables;
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
                    foreach (Data.IndividualRow indRow in redefinedSpecimen)
                    {
                        Data.LogRow prevLog = indRow.LogRow;

                        indRow.LogRow = LogRow;

                        if (prevLog.Quantity == 1)
                        {
                            Card card = (Card)this.Owner;

                            Individuals indlog = card.GetOpenedIndividuals(prevLog);
                            if (indlog != null) { indlog.Hide(); }

                            card.speciesLogger.Remove(prevLog.SpeciesRow.Species);
                            Data.Log.RemoveLogRow(prevLog);
                        }
                    }

                    UpdateValues();
                    UpdateLogRow();
                }
                else if (b == tdbCancelClose)
                {
                    e.Cancel = true;
                }
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            SaveData();
            LogRow.GetReport(CardReportLevel.Individuals | CardReportLevel.Stratified).Run();
        }

        private void buttonBlank_Click(object sender, EventArgs e)
        {
            FishReport.BlankIndividualsLog.Run();
        }

        #endregion
    }
}