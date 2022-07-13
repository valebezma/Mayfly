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
using static Mayfly.Wild.ReaderSettings;
using static Mayfly.Fish.UserSettings;

namespace Mayfly.Fish
{
    public partial class Individuals : Form
    {
        public Survey.LogRow LogRow;

        private Survey Data { get; set; }

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
                List<string> result = new List<string>(AddtVariables);

                foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                {
                    if (result.Contains(gridColumn.HeaderText)) continue;

                    result.Add(gridColumn.HeaderText);
                }

                return result.ToArray();
            }
        }

        private string[] currentVariables
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

        List<Survey.IndividualRow> redefinedSpecimen = new List<Survey.IndividualRow>();



        private Individuals()
        {
            InitializeComponent();
            Log.Write("Open individuals form.");

            stratifiedSample.Interval = DefaultStratifiedInterval;

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
                !FixTotals;
        }

        public Individuals(Survey.LogRow logRow) : this()
        {
            LogRow = logRow;
            Data = (Survey)LogRow.Table.DataSet;

            Text = string.Format(Wild.Resources.Interface.Interface.IndLog,
                    logRow.IsDefIDNull() ? Species.Resources.Interface.UnidentifiedTitle :
                    logRow.DefinitionRow.Taxon);

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

            Survey.IndividualRow[] individualRows = LogRow.GetIndividualRows();

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
                foreach (Survey.VariableRow variableRow in Data.Variable.Rows)
                {
                    spreadSheetLog.InsertColumn(variableRow.Variable, spreadSheetLog.ColumnCount - 1);
                }

                foreach (Survey.IndividualRow individualRow in individualRows)
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

            Survey.StratifiedRow[] stratifiedRows = LogRow.GetStratifiedRows();

            if (stratifiedRows.Length > 0)
            {
                double min = LogRow.MinStrate.LeftEndpoint;
                double max = LogRow.MaxStrate.LeftEndpoint;

                stratifiedSample.Interval = LogRow.Interval;
                stratifiedSample.Reset(min, max, true);

                foreach (Survey.StratifiedRow stratifiedRow in stratifiedRows)
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

            // Clear index species
            while (contextItemRedefine.DropDownItems.Count > 2)
            {
                contextItemRedefine.DropDownItems.RemoveAt(2);
            }


            // Insert domestic
            foreach (Survey.LogRow logRow in LogRow.CardRow.GetLogRows())
            {
                if (logRow == LogRow) continue;

                ToolStripMenuItem item = new ToolStripMenuItem(logRow.DefinitionRow.KeyRecord.CommonName);
                item.Tag = logRow;
                item.Click += redefineDomesticSpecies_Click;
                contextItemRedefine.DropDownItems.Insert(0, item);
            }

            //// Insert index
            //contextRedefineAll.DropDownItems.Clear();
            //contextRedefineAll.Visible = SpeciesIndex.Species.Count <= Species.AllowableSpeciesListLength;
            //if (SpeciesIndex.Species.Count <= Species.AllowableSpeciesListLength)
            //{
            //    foreach (SpeciesKey.TaxonRow speciesRow in SpeciesIndex.Species.Rows)
            //    {
            //        ToolStripItem speciesItem = new ToolStripMenuItem();
            //        speciesItem.Tag = speciesRow;
            //        speciesItem.Text = speciesRow.CommonName;
            //        speciesItem.Click += new EventHandler(redefineReferenceSpecies_Click);
            //        contextRedefineAll.DropDownItems.Add(speciesItem);
            //    }

            //    contextRedefineAll.SortItems();
            //}

            //foreach (SpeciesKey.BaseRow baseRow in SpeciesIndex.Base.Rows)
            //{
            //    ToolStripMenuItem baseItem = new ToolStripMenuItem();
            //    baseItem.Text = baseRow.BaseName;

            //    foreach (SpeciesKey.TaxonRow taxonRow in baseRow.GetTaxonRows())
            //    {
            //        ToolStripMenuItem taxonItem = new ToolStripMenuItem();
            //        taxonItem.Text = taxonRow.TaxonName;

            //        foreach (SpeciesKey.RepRow representativeRow in taxonRow.GetRepRows())
            //        {
            //            ToolStripItem speciesItem = new ToolStripMenuItem();
            //            speciesItem.Tag = representativeRow.SpeciesRow;
            //            speciesItem.Text = representativeRow.SpeciesRow.ShortName;
            //            speciesItem.Click += new EventHandler(redefineReferenceSpecies_Click);

            //            taxonItem.DropDownItems.Add(speciesItem);
            //        }

            //        taxonItem.SortItems();
            //        baseItem.DropDownItems.Add(taxonItem);
            //    }

            //    baseItem.SortItems();
            //    contextItemRedefine.DropDownItems.Add(baseItem);
            //}
        }

        private void RedefineSelected(Survey.LogRow logRow)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                Survey.IndividualRow indRow = IndividualRow(gridRow);
                redefinedSpecimen.Add(indRow);
                indRow.LogRow = logRow;
                if (indRow.IsCommentsNull()) { indRow.Comments = string.Format(Wild.Resources.Interface.Interface.RedefineComment, LogRow.DefinitionRow.Taxon); }
                else { indRow.Comments += string.Format(Environment.NewLine + Wild.Resources.Interface.Interface.RedefineComment, LogRow.DefinitionRow.Taxon); }

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
            card.Logger.UpdateLogRow(logRow);

            //Individuals newlog = card.OpenIndividuals(logRow);
            //newlog.LogLine = card.speciesLogger.FindLine(logRow.DefinitionRow.Taxon);
            //newlog.UpdateLogRow();
            //newlog.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);

        }

        private void redefineDomesticSpecies_Click(object sender, EventArgs e)
        {
            RedefineSelected((Survey.LogRow)((ToolStripMenuItem)sender).Tag);
        }

        private void redefineReferenceSpecies_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;

            //Data.LogRow logRow = Data.Log.NewLogRow();
            //logRow.DefinitionRow = speciesRow

            Card card = (Card)this.Owner;
            RedefineSelected(card.Logger.SaveLogRow(card.Logger.Grid.Rows[card.Logger.InsertSpecies(speciesRow)]));
            UpdateRedefineList();
        }

        private void UpdateTotals()
        { 
            double MassDifference = DetailedMass - PrevDetailedMass;
            int QuantityDifference = DetailedQuantity - PrevDetailedQuantity;

            PrevDetailedMass = DetailedMass;
            PrevDetailedQuantity = DetailedQuantity;

            if (ReaderSettings.FixTotals)
            {
                Mass = DetailedMass / 1000;
                Quantity = DetailedQuantity;
            }
            else
            {
                if (AutoIncreaseBio)
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

                if (AutoDecreaseBio && MassDifference < 0)
                {
                    Mass += (MassDifference / 1000);
                }

                if (AutoDecreaseBio && QuantityDifference < 0)
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
                Survey.IndividualRow individualRow =
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

            if (AddtVariables != null)
            {
                foreach (string addtVar in AddtVariables)
                {
                    ToolStripMenuItem newVarMenu = new ToolStripMenuItem(addtVar);
                    newVarMenu.Click += new EventHandler(CustomVar_Click);
                    contextMenuStripAdd.Items.Insert(7, newVarMenu);
                }
            }
        }

        private void InsertCurrentVariableColumns()
        {
            if (CurrentVariables != null)
            {
                foreach (string currVar in CurrentVariables)
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
                    Survey.StratifiedRow stratifiedRow = Data.Stratified.FindByLogIDClass(LogRow.ID, sizeGroup.LeftEndpoint);
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
                    card.Logger.UpdateStatus();
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

        private Survey.IndividualRow IndividualRow(DataGridViewRow gridRow)
        {
            return IndividualRow(Data, LogRow, gridRow);
        }

        private Survey.IndividualRow IndividualRow(Survey data, Survey.LogRow logRow, DataGridViewRow gridRow)
        {
            Survey.IndividualRow individualRow;

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

        private Survey.IndividualRow SaveIndividualRow(DataGridViewRow gridRow)
        {
            return SaveIndividualRow(Data, LogRow, gridRow);
        }

        private Survey.IndividualRow SaveIndividualRow(Survey data, Survey.LogRow logRow, DataGridViewRow gridRow)
        {
            Survey.IndividualRow individualRow = IndividualRow(data, logRow, gridRow);

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

        private void SaveAddtValues(Survey.IndividualRow individualRow, DataGridViewRow gridRow)
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

        private DataGridViewRow InsertIndividualRow(Survey.IndividualRow individualRow)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLog);
            gridRow.Cells[ColumnID.Index].Value = individualRow.ID;
            spreadSheetLog.Rows.Add(gridRow);
            UpdateIndividualRow(gridRow, individualRow);
            return gridRow;
        }

        private DataGridViewRow FindIndividualRow(Survey.IndividualRow individualRow)
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

        private void UpdateIndividualRow(Survey.IndividualRow individualRow)
        {
            UpdateIndividualRow(FindIndividualRow(individualRow), individualRow);
        }

        private void UpdateIndividualRow(DataGridViewRow gridRow, Survey.IndividualRow individualRow)
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

            foreach (Survey.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (!valueRow.IsValueNull())
                {
                    DataGridViewColumn col = spreadSheetLog.GetColumn(valueRow.VariableRow.Variable);
                    if (col != null) gridRow.Cells[col.Index].Value = valueRow.Value;
                }
            }
        }

        private Survey.IndividualRow[] GetIndividuals(IList rows)
        {
            spreadSheetLog.EndEdit();
            List<Survey.IndividualRow> result = new List<Survey.IndividualRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Survey.IndividualRow individualRow = IndividualRow(gridRow);
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
            Survey.VariableRow variableRow = Data.Variable.FindByVarName(e.PreviousCaption);

            if (variableRow != null)
            {
                variableRow.Variable = e.Column.HeaderText;
            }

            IsChanged = true;
        }

        private void spreadSheetLog_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            Width -= e.Column.Width;

            Survey.VariableRow variableRow = Data.Variable.FindByVarName(e.Column.HeaderText);

            if (variableRow != null)
            {
                for (int i = 0; i < Data.Value.Count; i++)
                {
                    Survey.ValueRow valueRow = Data.Value[i];
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
                Survey.ContainsIndividuals(Clipboard.GetText());
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
            Survey clipData = new Survey();

            Survey.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            Survey.DefinitionRow clipSpeciesRow = clipData.Definition.NewDefinitionRow();
            clipData.Definition.AddDefinitionRow(clipSpeciesRow);

            Survey.LogRow clipLogRow = clipData.Log.NewLogRow();
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
            Survey clipData = Survey.FromClipboard();

            // If clipData contains Deep tables then insert data and rows 
            // otherwise insert just rows

            foreach (Survey.VariableRow clipVariableRow in clipData.Variable.Rows)
            {
                spreadSheetLog.InsertColumn(clipVariableRow.Variable);
            }

            foreach (Survey.IndividualRow clipIndividualRow in clipData.Individual)
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
            AddtVariables = AdditionalVariables;
            AddVariableMenuItems();
            CurrentVariables = currentVariables;
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
                    foreach (Survey.IndividualRow indRow in redefinedSpecimen)
                    {
                        Survey.LogRow prevLog = indRow.LogRow;

                        indRow.LogRow = LogRow;

                        if (prevLog.Quantity == 1)
                        {
                            Card card = (Card)Owner;

                            Individuals indlog = card.GetOpenedIndividuals(prevLog);
                            if (indlog != null) { indlog.Hide(); }

                            card.Logger.Provider.Remove(prevLog.DefinitionRow.Taxon);
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