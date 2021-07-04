using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Bacterioplankton.Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            listViewWaters.Shine();
            listViewInvestigators.Shine();

            labelCardCount.UpdateStatus(0);
            labelLogCount.UpdateStatus(0);
            labelIndCount.UpdateStatus(0);

            selectedLogRows = new List<Data.LogRow>();

            columnCardWater.ValueType = typeof(string);
            columnCardLabel.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardVolume.ValueType = typeof(double);
            columnCardDepth.ValueType = typeof(double);
            columnCardAbundance.ValueType = typeof(double);
            columnCardBiomass.ValueType = typeof(double);
            columnCardComments.ValueType = typeof(string);

            tabPageCard.Parent = null;

            columnLogSpc.ValueType = typeof(string);
            columnLogAbundance.ValueType = typeof(double);
            columnLogBiomass.ValueType = typeof(double);

            tabPageLog.Parent = null;

            columnIndID.ValueType = typeof(int);
            columnIndSpecies.ValueType = typeof(string);
            columnIndLength.ValueType = typeof(double);
            ColumnIndDiameter.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndComments.ValueType = typeof(string);

            tabPageInd.Parent = null;

            tabPageArtefacts.Parent = null;

            FullStack = new CardStack(data);

            IsEmpty = true;
        }

        public MainForm(Data _data) : this()
        {
            data = _data;
        }

        public MainForm(CardStack stack)
            : this()
        {
            foreach (Data.CardRow cardRow in stack)
            {
                cardRow.SingleCardDataset().CopyTo(data);
            }

            UpdateSummary();
        }



        private void dataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;

            for (int i = 0; i < filenames.Length; i++)
            {
                if (data.IsLoaded(filenames[i])) continue;

                Data _data = new Data();

                if (_data.Read(filenames[i]))
                {
                    if (_data.Card.Count == 0)
                        Log.Write(string.Format("File is empty: {0}.", filenames[i]));
                    else
                    {
                        Data.CardRow[] cardRows = _data.CopyTo(data);
                    }
                }

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }
        }

        private void dataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCard.StopProcessing();
            UpdateSummary();
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateSummary();

            tabPageArtefacts.Parent = null;
            tabPageLog.Parent = null;
            tabPageInd.Parent = null;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;

            switch (CheckAndSave())
            {
                case DialogResult.OK:
                    e.Cancel = true;
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }



        private void tab_Changed(object sender, EventArgs e)
        {
            menuItemCards.Visible = tabControl.SelectedTab == tabPageCard;
            menuItemLog.Visible = tabControl.SelectedTab == tabPageLog;
            menuItemIndividuals.Visible = tabControl.SelectedTab == tabPageInd;
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }
        

        private void dataSaver_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveCards(sender as BackgroundWorker, e);
        }

        private void dataSaver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            spreadSheetCard.StopProcessing();

            UpdateSummary();
            if (IsClosing)
            {
                Close();
            }
        }
        

        private void extender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            processDisplay.StopProcessing();
        }
        

        #region Menus

        #region File

        private void menuItemAddData_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog(this) == DialogResult.OK)
            {
                LoadCards(UserSettings.Interface.OpenDialog.FileNames);
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            SaveCards();
        }

        private void menuItemBackup_Click(object sender, EventArgs e)
        {
            if (fbDialogBackup.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Data.CardRow cardRow in data.Card)
                {
                    Data _data = cardRow.SingleCardDataset();
                    string filename = FileSystem.SuggestName(fbDialogBackup.SelectedPath, _data.GetSuggestedName());
                    _data.WriteToFile(Path.Combine(fbDialogBackup.SelectedPath, filename));
                }
            }
        }

        private void menuItemSaveSet_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.SaveDialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllLines(UserSettings.Interface.SaveDialog.FileName, data.GetFilenames());
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Sample

        private void menuItemSample_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void menuItemArtefactsSearch_Click(object sender, EventArgs e)
        {
            tabPageArtefacts.Parent = tabControl;
            tabControl.SelectedTab = tabPageArtefacts;
            FindArtefacts();
            LoadArtefacts();
        }

        private void menuItemLoadCards_Click(object sender, EventArgs e)
        {
            tabPageCard.Parent = tabControl;
            tabControl.SelectedTab = tabPageCard;
            LoadCardLog();
        }

        private void menuItemLoadLog_Click(object sender, EventArgs e)
        {
            tabPageLog.Parent = tabControl;
            tabControl.SelectedTab = tabPageLog;

            tab_Changed(sender, e);

            LoadLog();
        }

        private void menuItemLoadInd_Click(object sender, EventArgs e)
        {
            tabPageInd.Parent = tabControl;
            tabControl.SelectedTab = tabPageInd;
            LoadIndLog();
        }

        #endregion

        #region Cards

        private void menuItemAssignVariants_Click(object sender, EventArgs e)
        {
            Wild.Service.AssignAsFactors(spreadSheetCard.GetInsertedColumns(), true);
        }

        private void menuItemCardFindEmpty_Click(object sender, EventArgs e)
        {
            spreadSheetCard.ClearSelection();
            foreach (DataGridViewRow gridRow in spreadSheetCard.Rows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                gridRow.Selected = cardRow.GetLogRows().Length == 0;
            }
        }

        private void menuItemCardPrint_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.Rows, CardReportLevel.IncludeSpeciesLog);
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.Rows, CardReportLevel.IncludeIndividualsLog);
        }

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.Rows, CardReportLevel.Note);
        }

        #endregion

        #region Log

        private void menuItemComCom_Click(object sender, EventArgs e)
        {
            SelectionValue selectionValue = new SelectionValue(spreadSheetCard);

            if (selectionValue.ShowDialog(this) != DialogResult.OK) return;

            IsBusy = true;
            spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
            {
                string field = gridColumn.Name.TrimStart("columnCard".ToCharArray());

                comparerLog.RunWorkerAsync(field);
            }
        }

        private void comparerLog_DoWork(object sender, DoWorkEventArgs e)
        {
            string field = (string)e.Argument;

            object[] variants = data.GetVariantsOf(field);

            List<Composition> result = new List<Composition>();

            foreach (object variant in variants)
            {
                CardStack stack = data.GetStack().GetStack(field, variant);

                Composition composition = stack.GetCommunityComposition();
                composition.Name = variant.ToString();
                result.Add(composition);
            }

            e.Result = result;
        }

        private void comparerLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CompositionComparison comcom = new CompositionComparison(
                data.GetStack().GetCommunityCompositionFrame(),
                (List<Composition>)e.Result);

            comcom.UpdateCompositionAppearance();
            comcom.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            comcom.Show();

            IsBusy = false;
            spreadSheetLog.StopProcessing();
        }

        #endregion

        #region Individuals log

        private void menuItemIndPrint_Click(object sender, EventArgs e)
        { }

        #endregion

        #region Service Menu

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        #endregion

        #endregion

        #region Main page

        private void cards_DragDrop(object sender, DragEventArgs e)
        {
            cards_DragLeave(sender, e);
            LoadCards(e.GetOperableFilenames(Bacterioplankton.UserSettings.Interface.Extension));
        }

        private void cards_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void cards_DragLeave(object sender, EventArgs e)
        { }


        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardWater, listViewWaters.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardInvestigator, listViewInvestigators.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        #endregion

        #region Cards

        private void spreadSheetCard_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetCard.ContainsFocus)
            {
                SaveCardRow(spreadSheetCard.Rows[e.RowIndex]);
            }

            UpdateCardTotals();
        }

        private void spreadSheetCard_Filtered(object sender, EventArgs e)
        {
            UpdateCardTotals();
        }


        private void cardLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            for (int i = 0; i < data.Card.Count; i++)
            {
                result.Add(UpdateCardRow(data.Card[i]));
                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void cardLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCard.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetCard.StopProcessing();
            UpdateCardTotals();
        }


        private void contextCard_Opening(object sender, CancelEventArgs e)
        {
            contextCardOpen.Enabled = false;

            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                if (cardRow.Path == null) continue;
                contextCardOpen.Enabled = true;
                break;
            }
        }

        private void contextCardOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                if (cardRow.Path == null) continue;

                FileSystem.RunFile(cardRow.Path);
            }
        }

        private void contextCardLog_Click(object sender, EventArgs e)
        {

        }

        private void contextCardPrint_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.SelectedRows, CardReportLevel.IncludeSpeciesLog);
        }

        private void contextCardPrintNotes_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.SelectedRows, CardReportLevel.Note);
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.SelectedRows, CardReportLevel.IncludeIndividualsLog);
        }

        #endregion        

        #region Log

        private void LogLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            for (int i = 0; i < data.Card.Count; i++)
            {
                foreach (Data.SpeciesRow speciesRow in data.Species)
                {
                    DataGridViewRow gridRow = GetLine(data.Card[i], speciesRow);

                    result.Add(gridRow);

                    if ((double)gridRow.Cells[columnLogAbundance.Index].Value == 0)
                    {
                        spreadSheetLog.SetHidden(gridRow);
                    }
                }

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void LogLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetLog.Rows.AddRange(e.Result as DataGridViewRow[]);
            //spreadSheetLog.InitiateBackgroundTable();
            UpdateLogTotals();
            IsBusy = false;
            spreadSheetLog.StopProcessing();
        }
        
        private void adapterLog_OperatingChanged(object sender, EventArgs e)
        {

        }

        private void LogExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(SetCardValue);
                gridRow.DataGridView.Invoke(valueSetter,
                    new object[] { LogRow(gridRow).CardRow, gridRow, spreadSheetLog.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }

        private void spreadSheetLog_Filtered(object sender, EventArgs e)
        {
            UpdateLogTotals();
        }

        private void speciesLog_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            tdLog.Content = string.Format(
                Wild.Resources.Interface.Messages.LogRename,
                e.OriginalValue, e.SpeciesName);

            if (tdLog.ShowDialog() == tdbLogRename)
            {
                Data.SpeciesRow spcRow = data.Species.FindBySpecies(e.SpeciesName);

                if (spcRow == null)
                {
                    spcRow = data.Species.AddSpeciesRow(e.SpeciesName);
                }

                Data.LogRow logRow = LogRow(e.Row);
                logRow.SpeciesRow = spcRow;

                RememberChanged(logRow.CardRow);
            }
            else
            {
                e.GetCell().Value = e.OriginalValue;
            }
        }

        private void contextLogOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                Data.LogRow logRow = LogRow(gridRow);
                Mayfly.FileSystem.RunFile(logRow.CardRow.Path, logRow.SpeciesRow.Species);
            }
        }

        private void buttonSelectLog_Click(object sender, EventArgs e)
        {
            if (LoadCardAddt(spreadSheetLog))
            {
                IsBusy = true;
                processDisplay.StartProcessing(spreadSheetLog.RowCount,
                    Wild.Resources.Interface.Process.ExtSpc);
                loaderLogExtended.RunWorkerAsync();
            }
        }

        #endregion

        #region Individuals

        private void indExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(SetCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { GetIndividualRow(gridRow).LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void specTipper_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                SetIndividualMassTip(gridRow);
            }
        }

        private void specTipper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetInd.StopProcessing();
        }




        private void spreadSheetInd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetInd[e.ColumnIndex, e.RowIndex].Value is Age)
            {
                if (((Age)spreadSheetInd[e.ColumnIndex, e.RowIndex].Value).Gain)
                {
                    spreadSheetInd[e.ColumnIndex, e.RowIndex].Style.Padding = new Padding(0, 0, 2, 0);
                }
                else
                {
                    spreadSheetInd[e.ColumnIndex, e.RowIndex].Style.Padding = new Padding(0, 0, 8, 0);
                }
            }

            if (spreadSheetInd.ContainsFocus)
            {
                SaveIndRow(spreadSheetInd.Rows[e.RowIndex]);
            }

            UpdateIndTotals();
        }

        private void spreadSheetInd_Filtered(object sender, EventArgs e)
        {
            UpdateIndTotals();
        }

        private void speciesInd_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            tdInd.Content = string.Format(
                Wild.Resources.Interface.Messages.IndRename,
                e.OriginalValue, e.SpeciesName);

            if (tdInd.ShowDialog() == tdbIndRename)
            {
                Data.IndividualRow individualRow = GetIndividualRow(e.Row);

                Data.SpeciesRow spcRow = data.Species.FindBySpecies(e.SpeciesName);

                if (spcRow == null)
                {
                    spcRow = data.Species.AddSpeciesRow(e.SpeciesName);
                }

                if (individualRow.LogRow.Quantity == 1)
                {
                    // If individual is single - just replace log.species
                    individualRow.LogRow.SpeciesRow = spcRow;
                }
                else
                {
                    // If there are more individual(-s) - create new log and replace individual to it                    
                    Data.LogRow logRow = data.Log.FindByCardIDSpcID(individualRow.LogRow.CardRow.ID, spcRow.ID);

                    if (logRow == null)
                    {
                        logRow = data.Log.NewLogRow();
                        logRow.SpeciesRow = spcRow;
                        logRow.CardRow = individualRow.LogRow.CardRow;
                        logRow.Quantity = 1;
                        data.Log.AddLogRow(logRow);
                    }

                    individualRow.LogRow.Quantity--;
                    //if (!individualRow.LogRow.IsMassNull()) individualRow.LogRow.Mass -= individualRow.LogRow.Mass;

                    individualRow.LogRow = logRow;
                    logRow.Quantity++;
                    //if (logRow.IsMassNull()) logRow.Mass = individualRow.Mass;
                    //else logRow.Mass += individualRow.Mass;
                }

                RememberChanged(individualRow.LogRow.CardRow);
            }
            else
            {
                e.GetCell().Value = e.OriginalValue;
            }
        }

        private void contextIndLog_Click(object sender, EventArgs e)
        {
            if (spreadSheetLog.RowCount > 0)
            {
                selectCorrespondingLogRows();
                tabControl.SelectedTab = tabPageLog;
            }
            else
            {
                loaderLog.RunWorkerCompleted += selectCorrespondingLogRows;
                menuItemLoadLog_Click(sender, e);
            }
        }

        private void contextIndCard_Click(object sender, EventArgs e)
        {
            //if (spreadSheetCard.RowCount > 0)
            //{
            //    selectCorrespondingCardRow();
            //    tabControlCards.SelectedTab = tabPageCard;
            //}
            //else
            //{
            //    loaderCard.RunWorkerCompleted += selectCorrespondingCardRow;
            //    menuItemLoadCards_Click(sender, e);
            //}
        }

        private void selectCorrespondingLogRows()
        {
            spreadSheetLog.ClearSelection();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                if (!gridRow.Visible) continue;

                Data.IndividualRow individualRow = GetIndividualRow(gridRow);
                DataGridViewRow corrRow = columnLogID.GetRow(individualRow.LogID);

                if (corrRow == null) continue;
                corrRow.Selected = true;
                spreadSheetLog.FirstDisplayedScrollingRowIndex = corrRow.Index;
            }
        }

        private void selectCorrespondingLogRows(object sender, RunWorkerCompletedEventArgs e)
        {
            selectCorrespondingLogRows();
            loaderLog.RunWorkerCompleted -= selectCorrespondingLogRows;
        }





        private void contextIndOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = GetIndividualRow(gridRow);

                Mayfly.FileSystem.RunFile(individualRow.LogRow.CardRow.Path,
                    individualRow.LogRow.SpeciesRow.Species);

                // TODO: select row in a log
            }
        }

        private void contextIndDelete_Click(object sender, EventArgs e)
        {
            while (spreadSheetInd.SelectedRows.Count > 0)
            {
                Data.IndividualRow individualRow = GetIndividualRow(spreadSheetInd.SelectedRows[0]);
                RememberChanged(individualRow.LogRow.CardRow);
                data.Individual.Rows.Remove(individualRow);
                spreadSheetInd.Rows.Remove(spreadSheetInd.SelectedRows[0]);
            }
        }

        private void spreadSheetInd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Data.IndividualRow individualRow = GetIndividualRow(e.Row);
            RememberChanged(individualRow.LogRow.CardRow);
            data.Individual.Rows.Remove(individualRow);
        }

        private void buttonSelectInd_Click(object sender, EventArgs e)
        {
            if (LoadCardAddt(spreadSheetInd))
            {
                IsBusy = true;
                processDisplay.StartProcessing(spreadSheetInd.RowCount,
                    Wild.Resources.Interface.Process.ExtInd);
                loaderIndExtended.RunWorkerAsync();
            }
        }

        #endregion
    }
}