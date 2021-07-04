using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Mayfly.Software;

namespace Mayfly.Calculus
{
    public partial class MainForm : Form
    {
        private string fileName;

        public string FileName
        {
            set
            {
                this.ResetText(value == null ? FileSystem.GetNewFileCaption(".csv") : value, EntryAssemblyInfo.Title);
                fileName = value;
            }

            get
            {
                return fileName;
            }
        }

        public bool IsChanged
        {
            set;
            get;
        }



        public MainForm()
        {
            InitializeComponent();


            /*listViewGroupColumns.Shine();
            listViewGroupDisplay.Shine();
            listViewPivotDisplay.Shine();
            listViewPivotIcons.Shine();
            tabPageGroup.Parent = null;
            tabPagePivot.Parent = null;*/


            FileName = null;

            for (int i = 1; i < 6; i++)
            {
                spreadSheetData.InsertColumn("Var " + i, typeof(double));
            }
        }

        public MainForm(SpreadSheet spreadSheetSource) : this()
        {
            spreadSheetData.Load(spreadSheetSource);
            labelEntriesCount.UpdateStatus(spreadSheetData.VisibleRowCount);

            // TODO: place to point where grids will match visually
        }

        public MainForm(string fileName) : this()
        {
            LoadData(fileName);
        }



        #region Methods

        public void LoadData(string fileName)
        {
            //ClearPivot();

            spreadSheetData.Columns.Clear();
            spreadSheetData.Load(Mathematics.Service.LoadFromFile(fileName));
            labelEntriesCount.UpdateStatus(spreadSheetData.VisibleRowCount);
            FileName = fileName;
        }

        private DialogResult CheckAndSave()
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSaveChanges.ShowDialog(this);

                if (b == tdbSave)
                {
                    menuItemSave_Click(menuItemSave, new EventArgs());
                    return DialogResult.OK;
                }
                else if (b == tdbDiscard)
                {
                    return DialogResult.No;
                }
                else if (b == tdbCancelClose)
                {
                    return DialogResult.Cancel;
                }
            }

            return DialogResult.No;
        }
        
        private void Save(string fileName)
        {
            adapter.Sheet.SaveToFile(fileName);
            FileName = fileName;
            IsChanged = false;
        }


        /*
        private void ClearGroup()
        {
            spreadSheetGroup.Rows.Clear();
            spreadSheetGroup.ClearInsertedColumns();
            labelGroupCount.UpdateStatus(0);
            tabPageGroup.Parent = null;
            tabControl1.SelectedTab = tabPageTable;
        }

        public void Group()
        {
            comboBoxGroupRows.DataSource = spreadSheetData.GetCategorialColumns();

            groupPicker = new ColumnPicker(listViewGroupColumns);
            groupPicker.ColumnCollection = spreadSheetData.GetNumericalColumns();

            labelGroupCount.UpdateStatus(0);

            tabPageGroup.Parent = tabControl1;
            tabControl1.SelectedTab = tabPageGroup;
            listViewGroupDisplay.SelectedIndices.Clear();
            listViewGroupDisplay.SelectedIndices.Add(1);
            menuItemGroup.Visible = true;

            CalculateGroup();
        }

        private void CalculateGroup()
        {
            if (GroupRowGrouper == null) return;

            GroupForbidden = true;

            columnGroupGroup.HeaderText = GroupRowGrouper.HeaderText;

            foreach (DataGridViewColumn gridColumn in groupPicker.SelectedColumns)
            {
                DataGridViewColumn newGridColumn = spreadSheetGroup.InsertColumn(gridColumn);
                newGridColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                newGridColumn.ValueType = null;
            }

            spreadSheetGroup.Rows.Clear();

            string[] rowGroups = GroupRowGrouper.GetStrings(true).ToArray();
            labelGroupCount.UpdateStatus(rowGroups.Length);
            spreadSheetGroup.StartProcessing(rowGroups.Length, "Grouping data");
            calculatorGroup.RunWorkerAsync(rowGroups);
        }



        private void ClearPivot()
        {
            spreadSheetPivot.Rows.Clear();
            spreadSheetPivot.ClearInsertedColumns();
            labelPivotCount.UpdateStatus(0);
            tabPagePivot.Parent = null;
            tabControl1.SelectedTab = tabPageTable;
        }

        public void Pivot()
        {
            comboBoxPivotRows.DataSource = spreadSheetData.GetCategorialColumns();
            comboBoxPivotColumns.DataSource = spreadSheetData.GetCategorialColumns();
            comboBoxPivotValues.DataSource = spreadSheetData.GetNumericalColumns();

            labelPivotCount.UpdateStatus(0);

            tabPagePivot.Parent = tabControl1;
            tabControl1.SelectedTab = tabPagePivot;
            listViewPivotDisplay.SelectedIndices.Clear();
            listViewPivotDisplay.SelectedIndices.Add(1);
            menuItemPivot.Visible = true;
            CalculatePivot();
        }

        private void CalculatePivot()
        {
            if (PivotRowGrouper == null) return;
            if (PivotColumnGrouper == null) return;
            if (PivotValueColumn == null) return;

            PivotForbidden = true;

            columnPivotGroup.HeaderText = PivotRowGrouper.HeaderText;

            spreadSheetPivot.ClearInsertedColumns();
            spreadSheetPivot.Rows.Clear();

            string[] columnGroups = PivotColumnGrouper.GetStrings(true).ToArray();

            foreach (string value in columnGroups)
            {
                DataGridViewColumn gridColumn = spreadSheetPivot.InsertIconColumn(value, value, null);
            }

            string[] rowGroups = PivotRowGrouper.GetStrings(true).ToArray();
            labelPivotCount.UpdateStatus(columnGroups.Length * rowGroups.Length);
            spreadSheetPivot.StartProcessing(columnGroups.Length * rowGroups.Length, "Pivot processing");
            calculatorPivot.RunWorkerAsync(rowGroups);
        }
        */


        #endregion



        #region Interface logics

        private void MainForm_Load(object sender, EventArgs e)
        {
            labelEntriesCount.UpdateStatus(spreadSheetData.VisibleRowCount);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //menuItemPivot.Visible = tabControl1.SelectedTab == tabPagePivot;
            //menuItemGroup.Visible = tabControl1.SelectedTab == tabPageGroup;
        }

        #region Menu

        private void menuItemNew_Click(object sender, EventArgs e)
        {

        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (CheckAndSave() != DialogResult.Cancel)
                {
                    LoadData(UserSettings.Interface.OpenDialog.FileName);
                }
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (FileName == null)
            {
                menuItemSaveAs_Click(menuItemSaveAs, new EventArgs());
            }
            else
            {
                Save(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            UserSettings.Interface.SaveDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(FileName);

            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Mathematics.Settings settings = new Mathematics.Settings();
            settings.ShowDialog();
        }

        #endregion



        #region Data table

        private void spreadSheetData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
        }

        private void spreadSheetData_Filtered(object sender, EventArgs e)
        {
            labelEntriesCount.UpdateStatus(spreadSheetData.VisibleRowCount);
        }
        /*
        private void buttonGroup_Click(object sender, EventArgs e)
        {
            Group();
        }

        private void buttonPivot_Click(object sender, EventArgs e)
        {
            Pivot();
        }
        */
        #endregion



        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }


        /*
        #region Group table

        private void calculatorGroup_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            int i = 0;

            foreach (string value in (string[])e.Argument)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGroup);
                gridRow.Cells[columnGroupGroup.Index].Value = value;
                gridRow.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                gridRow.MinimumHeight = 35;

                List<DataGridViewRow> valueRows = GroupRowGrouper.GetRows(value);

                foreach (DataGridViewColumn gridColumn in spreadSheetGroup.GetInsertedColumns())
                {
                    Sample sample = spreadSheetData.GetColumn(gridColumn.Name).GetSample(valueRows);

                    if (sample.Count > 0)
                    {
                        sample.Name = string.Format("{0} = {1}", GroupRowGrouper.HeaderText, value);
                        gridRow.Cells[gridColumn.Index].ToolTipText = sample.ToLongString();
                        gridRow.Cells[gridColumn.Index].Value = sample;
                    }
                    else
                    {
                        gridRow.Cells[gridColumn.Index].ToolTipText = string.Empty;
                        gridRow.Cells[gridColumn.Index].Value = null;
                    }
                }

                i++;
                (sender as BackgroundWorker).ReportProgress(i);

                result.Add(gridRow);
            }

            e.Result = result.ToArray();
        }

        private void calculatorGroup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetGroup.Rows.AddRange((DataGridViewRow[])e.Result);
            spreadSheetGroup.Refresh();

            spreadSheetGroup.StopProcessing();
            GroupForbidden = false;
        }

        private void listViewGroupColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDoGroup.Enabled = listViewGroupColumns.SelectedItems.Count > 0;
            //bool newInserted = false;
            //int i = spreadSheetGroup.InsertedColumnCount;
            //foreach (DataGridViewColumn gridColumn in spreadSheetGroup.GetInsertedColumns())
            //{
            //    if (selectionValue.Picker.IsSelected(gridColumn)) continue;
            //    spreadSheetGroup.Columns.Remove(gridColumn);
            //    i--;
            //}

            //foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
            //{
            //    if (spreadSheetGroup.GetColumn(gridColumn.Name) == null)
            //    {
            //        spreadSheetGroup.InsertColumn(gridColumn, i, gridColumn.Name.TrimStart("columnCard".ToCharArray()));
            //        newInserted = true;
            //        i++;
            //    }
            //}
        }

        private void listViewGroupDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            spreadSheetGroup.Refresh();
        }

        private void spreadSheetGroup_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == columnGroupGroup.Index)
            {
                e.Value = e.Value;
            }
            else if (listViewGroupDisplay.SelectedItems.Count > 0)
            {
                Service.Format(e, listViewGroupDisplay.SelectedItems[0].Index);
            }
            else
            {
                e.Value = string.Empty;
            }

            e.FormattingApplied = true;
        }

        private void buttonDoGroup_Click(object sender, EventArgs e)
        {
            GroupRowGrouper = (DataGridViewColumn)comboBoxGroupRows.SelectedItem;
            CalculateGroup();
        }

        private void buttonPivotize_Click(object sender, EventArgs e)
        {
            Pivot();
            comboBoxPivotRows.SelectedValue = comboBoxGroupRows.SelectedValue;
        }

        #endregion



        #region Pivot table

        private void pivot_Changed(object sender, EventArgs e)
        {
            PivotRowGrouper = (DataGridViewColumn)comboBoxPivotRows.SelectedItem;
            PivotColumnGrouper = (DataGridViewColumn)comboBoxPivotColumns.SelectedItem;
            PivotValueColumn = (DataGridViewColumn)comboBoxPivotValues.SelectedItem;
            
            CalculatePivot();
        }

        private void calculatorPivot_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            int i = 0;

            foreach (string value in (string[])e.Argument)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetPivot);
                gridRow.Cells[columnPivotGroup.Index].Value = value;
                gridRow.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                gridRow.MinimumHeight = 35;

                List<DataGridViewRow> valueRows = PivotRowGrouper.GetRows(value);

                foreach (DataGridViewColumn gridColumn in spreadSheetPivot.GetInsertedColumns())
                {
                    i++;
                    (sender as BackgroundWorker).ReportProgress(i);

                    Sample sample = PivotValueColumn.GetSample(PivotColumnGrouper.GetRows(gridColumn.Name, valueRows));

                    if (sample.Count > 0)
                    {
                        sample.Name = string.Format("{0} {1} in {2} is {3}", 
                            PivotRowGrouper.HeaderText, value, PivotColumnGrouper.HeaderText, gridColumn.Name);
                        gridRow.Cells[gridColumn.Index].ToolTipText = sample.ToLongString();
                        gridRow.Cells[gridColumn.Index].Value = sample;
                    }
                    else
                    {
                        gridRow.Cells[gridColumn.Index].ToolTipText = string.Empty;
                        gridRow.Cells[gridColumn.Index].Value = null;
                    }

                }

                result.Add(gridRow);
            }

            e.Result = result.ToArray();
        }

        private void calculatorPivot_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetPivot.Rows.AddRange((DataGridViewRow[])e.Result);
            spreadSheetPivot.Refresh();
            DisplayIcons();

            spreadSheetPivot.StopProcessing();
            PivotForbidden = false;
        }

        private void display_Changed(object sender, EventArgs e)
        {
            spreadSheetPivot.Refresh();
        }

        private void icon_Changed(object sender, EventArgs e)
        {
            DisplayIcons();
        }

        //private void buttonCrossChart_Click(object sender, EventArgs e)
        //{
        //    Charts.StatChart statChart = SpreadSheet.HandleCrossChart((DataGridViewColumn)comboBoxValues.SelectedItem,
        //        new List<DataGridViewColumn>() { (DataGridViewColumn)comboBoxGroupRows.SelectedItem,
        //            (DataGridViewColumn)comboBoxGroupColumns.SelectedItem });
        //    statChart.FindForm().SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
        //}

        private void spreadSheetPivot_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == columnPivotGroup.Index)
            {
                e.Value = e.Value;
            }
            else if (listViewPivotDisplay.SelectedItems.Count > 0)
            {
                Service.Format(e, listViewPivotDisplay.SelectedItems[0].Index, PivotValueColumn.DefaultCellStyle.Format);
            }
            else
            {
                e.Value = string.Empty;
            }

            e.FormattingApplied = true;
        }

        private void contextPivot_Opening(object sender, CancelEventArgs e)
        {
            contextDetails.Enabled = spreadSheetPivot.CurrentCell.Value is Sample;
        }

        private void contextRowChart_Click(object sender, EventArgs e)
        {
            List<Evaluation> evaluations = new List<Evaluation>();

            foreach (DataGridViewRow gridRow in spreadSheetPivot.SelectedRows)
            {
                Evaluation evaluation = new Evaluation((string)gridRow.Cells[columnPivotGroup.Index].Value);

                foreach (DataGridViewColumn gridColumn in spreadSheetPivot.GetInsertedColumns())
                {
                    Sample sample = spreadSheetPivot[gridColumn.Index, gridRow.Index].Value as Sample;
                    if (sample == null) sample = new Sample();
                    sample.Name = gridColumn.HeaderText;
                    evaluation.Add(sample);
                }

                evaluations.Add(evaluation);
            }

            ChartForm result = new ChartForm(PivotColumnGrouper.HeaderText, PivotValueColumn.HeaderText);
            result.Text = PivotRowGrouper.HeaderText;

            foreach (Evaluation evaluation in evaluations)
            {
                evaluation.ValueChanged += new EvaluationEventHandler(result.StatChart.Update);
                evaluation.ValueChanged += new EvaluationEventHandler(result.StatChart.Rebuild);
                result.StatChart.AddSeries(evaluation);
            }

            result.StatChart.SetColorScheme();
            result.StatChart.Update(sender, e);
            result.StatChart.Rebuild(sender, e);

            result.Show();
        }

        private void contextRowAnova_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetPivot.SelectedRows)
            {
                List<Sample> samples = new List<Sample>();

                foreach (DataGridViewColumn gridColumn in spreadSheetPivot.GetInsertedColumns())
                {
                    Sample sample = spreadSheetPivot[gridColumn.Index, gridRow.Index].Value as Sample;
                    if (sample == null) sample = new Sample();

                    sample.Name = gridColumn.HeaderText;
                    samples.Add(sample);
                }

                AnovaProperties properties = new AnovaProperties(PivotValueColumn.HeaderText, PivotColumnGrouper.HeaderText, samples);
                properties.Show();
                properties.ShowGraph();
            }
        }

        private void contextDetails_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell gridCell in spreadSheetPivot.SelectedCells)
            {
                Sample stored = gridCell.Value as Sample;

                if (stored != null)
                {
                    SampleProperties properties = new SampleProperties(stored);
                    properties.SetFriendlyDesktopLocation(gridCell);
                    properties.Show();
                }
            }
        }

        private void contextEfficiencyEstimation_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell gridCell in spreadSheetPivot.SelectedCells)
            {
                Sample stored = gridCell.Value as Sample;

                if (stored != null)
                {
                    EffortEstimation effortEstimation = new EffortEstimation(stored);
                    effortEstimation.SetFriendlyDesktopLocation(gridCell);
                    effortEstimation.Show();
                }
            }
        }

        #endregion

        */

        #endregion
    }
}