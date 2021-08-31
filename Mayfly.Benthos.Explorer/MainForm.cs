using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Benthos.Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            listViewWaters.Shine();
            listViewSamplers.Shine();
            listViewInvestigators.Shine();

            SetSpeciesIndex(Benthos.UserSettings.SpeciesIndexPath);

            selectedLogRows = new List<Data.LogRow>();

            columnCardWater.ValueType = typeof(string);
            columnCardLabel.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardWhere.ValueType = typeof(Waypoint);
            columnCardSampler.ValueType = typeof(string);
            columnCardSquare.ValueType = typeof(double);
            columnCardSubstrate.ValueType = typeof(string);
            columnCardDepth.ValueType = typeof(double);
            ColumnCardCrossSection.ValueType = typeof(string);
            ColumnCardBank.ValueType = typeof(string);
            columnCardWealth.ValueType = typeof(double);
            columnCardQuantity.ValueType = typeof(int);
            columnCardMass.ValueType = typeof(double);
            columnCardAbundance.ValueType = typeof(double);
            columnCardBiomass.ValueType = typeof(double);
            columnCardDiversityA.ValueType = typeof(double);
            columnCardDiversityB.ValueType = typeof(double);
            columnCardComments.ValueType = typeof(string);

            tabPageCard.Parent = null;

            columnSpcSpc.ValueType = typeof(string);
            columnSpcQuantity.ValueType = typeof(int);
            columnSpcMass.ValueType = typeof(double);
            columnSpcAbundance.ValueType = typeof(double);
            columnSpcBiomass.ValueType = typeof(double);
            columnSpcOccurrence.ValueType = typeof(double);
            columnSpcDominance.ValueType = typeof(double);
            columnSpcDiversityA.ValueType = typeof(double);
            columnSpcDiversityB.ValueType = typeof(double);

            tabPageSpc.Parent = null;

            columnLogSpc.ValueType = typeof(string);
            columnLogQuantity.ValueType = typeof(int);
            columnLogMass.ValueType = typeof(double);
            columnLogAbundance.ValueType = typeof(double);
            columnLogBiomass.ValueType = typeof(double);
            columnLogDiversityA.ValueType = typeof(double);
            columnLogDiversityB.ValueType = typeof(double);

            columnIndID.ValueType = typeof(int);
            columnIndSpecies.ValueType = typeof(string);
            ColumnIndFrequency.ValueType = typeof(int);
            columnIndLength.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndSex.ValueType = typeof(Sex);
            columnIndGrade.ValueType = typeof(Grade);
            columnIndInstar.ValueType = typeof(int);
            columnIndComments.ValueType = typeof(string);

            data.RefreshBios();
            //data.WeightModels.VisualConfirmation =
            //    UserSettings.VisualConfirmation;

            IsEmpty = true;
        }

        public MainForm(string[] args)
            : this()
        {
            if (args.Length == 1)
            {
                switch (args[0])
                {
                    default:
                        this.Load += (o, e) => { LoadCards(args.GetOperableFilenames(Benthos.UserSettings.Interface.Extension)); };
                        break;
                }
            }
            else
            {
                this.Load += (o, e) => { LoadCards(args.GetOperableFilenames(Benthos.UserSettings.Interface.Extension)); };
            }
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

        public MainForm(Data _data) : this()
        {
            data = _data;
            data.RefreshBios();
        }
        


        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateSummary();

            tabPageArtifacts.Parent = null;
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
            menuItemCards.Visible = (tabControl.SelectedTab == tabPageCard);
            menuItemSpc.Visible = (tabControl.SelectedTab == tabPageSpc);
            menuItemLog.Visible = (tabControl.SelectedTab == tabPageLog);
            menuItemIndividuals.Visible = (tabControl.SelectedTab == tabPageInd);
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }



        private void dataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;

            for (int i = 0; i < filenames.Length; i++)
            {
                if (Path.GetExtension(filenames[i]) == Wild.UserSettings.InterfaceBio.Extension)
                {
                    processDisplay.SetStatus(Wild.Resources.Interface.Process.SpecLoading);

                    if (data.IsBioLoaded)
                    {
                        TaskDialogButton tdb = taskDialogBio.ShowDialog();

                        if (tdb != tdbSpecCancel)
                        {
                            data.ImportBio(filenames[i], tdb == tdbSpecClear);
                        }
                    }
                    else
                    {
                        data.ImportBio(filenames[i]);
                    }

                    //data.RefreshBios();

                    processDisplay.ResetStatus();
                }
                else if (Path.GetExtension(filenames[i]) == Benthos.UserSettings.Interface.Extension)
                {
                    if (data.IsLoaded(filenames[i])) continue;

                    Data _data = new Data();

                    if (_data.Read(filenames[i]))
                    {
                        if (_data.Card.Count == 0)
                        {
                            Log.Write(string.Format("File is empty: {0}.", filenames[i]));
                        }
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


        private void modelCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            foreach (Data.SpeciesRow speciesRow in data.Species.GetPhylogeneticallySorted(Benthos.UserSettings.SpeciesIndex))
            {
                ContinuousBio bio = data.FindMassModel(speciesRow.Species);
                if (bio != null) bio.RefreshInternal();
                i++;
                ((BackgroundWorker)sender).ReportProgress(i);
            }
        }

        private void modelCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double q = FullStack.Quantity();
            double w = FullStack.Mass();

            statusQuantity.ResetFormatted(Wild.Service.GetFriendlyQuantity((int)q));
            statusMass.ResetFormatted(Wild.Service.GetFriendlyMass(w / 1000));

            processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.ArtifactsProcessing);
            artefactFinder.RunWorkerAsync();
        }


        private void dataSaver_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;

            while (ChangedCards.Count > 0)
            {
                Data.CardRow cardRow = ChangedCards[0];

                index++;
                dataSaver.ReportProgress(index);

                if (cardRow.Path != null)
                {
                    Data _data = cardRow.SingleCardDataset();
                    _data.WriteToFile(cardRow.Path);
                }

                if (CardRowSaved != null) CardRowSaved.Invoke(this, new CardRowSaveEvent(cardRow));

                ChangedCards.RemoveAt(0);
            }
        }

        private void dataSaver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            spreadSheetCard.StopProcessing();
            UpdateSummary();
            menuItemSave.Enabled = IsChanged;
            if (IsClosing) { Close(); }
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

        private void menuItemBackupCards_Click(object sender, EventArgs e)
        {
            if (fbDialogBackup.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Data.CardRow cardRow in data.Card)
                {
                    string filename = IO.SuggestName(fbDialogBackup.SelectedPath, cardRow.GetSuggestedName());
                    Data _data = cardRow.SingleCardDataset();
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
        {        }

        private void menuItemArtifacts_Click(object sender, EventArgs e)
        {
            showArtifacts = true;
            processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.ArtifactsProcessing);
            artefactFinder.RunWorkerAsync();
        }

        private void menuItemLoadCards_Click(object sender, EventArgs e)
        {
            tabPageCard.Parent = tabControl;
            tabControl.SelectedTab = tabPageCard;
            LoadCardLog();
        }

        private void menuItemLoadSpc_Click(object sender, EventArgs e)
        {
            tabPageSpc.Parent = tabControl;
            tabControl.SelectedTab = tabPageSpc;

            tab_Changed(sender, e);

            LoadSpc();
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

        private void menuItemBriefSpecies_Click(object sender, EventArgs e)
        {
            Report report = new Report(Resources.Reports.Cenosis.Title);
            FullStack.AddBrief(report, null);
            report.Run();
        }

        #region Service

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void menuItemLicenses_Click(object sender, EventArgs e)
        {
            Software.Settings settings = new Software.Settings();
            settings.OpenFeatures();
            settings.ShowDialog();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            about.SetPowered(Benthos.Properties.Resources.ibiw, Wild.Resources.Interface.Powered.IBIW);
            about.ShowDialog();
        }

        #endregion

        #endregion

        #region Main page

        private void cards_DragDrop(object sender, DragEventArgs e)
        {
            cards_DragLeave(sender, e);
            LoadCards(e.GetOperableFilenames(Benthos.UserSettings.Interface.Extension));
        }

        private void cards_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
                labelCards.ForeColor = labelCardsValue.ForeColor = Color.LightGray;
            }
        }

        private void cards_DragLeave(object sender, EventArgs e)
        {
            labelCards.ForeColor = labelCardsValue.ForeColor = SystemColors.ControlText;
        }

        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardWater, listViewWaters.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        private void listViewSamplers_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardSampler, listViewSamplers.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardInvestigator, listViewInvestigators.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        #endregion

        #region Artifacts

        private void contextArtSpecies_Opening(object sender, CancelEventArgs e)
        {
            //contextArtSpeciesAddToRef.Enabled = Benthos.UserSettings.SpeciesAutoExpand;
        }

        private void contextArtSpeciesAddToRef_Click(object sender, EventArgs e)
        {
            List<Data.SpeciesRow> speciesRows = new List<Data.SpeciesRow>();

            foreach (DataGridViewRow gridRow in spreadSheetArtifactSpecies.SelectedRows)
            {
                string species = (string)gridRow.Cells[columnArtifactSpecies.Index].Value;
                Data.SpeciesRow speciesRow = data.Species.FindBySpecies(species);
                speciesRows.Add(speciesRow);
            }

            speciesRows.ToArray().GetSpeciesKey().ImportTo(Benthos.UserSettings.SpeciesIndex, 
                Benthos.UserSettings.SpeciesAutoExpandVisual);
            Benthos.UserSettings.SpeciesIndex.SaveToFile(Benthos.UserSettings.SpeciesIndexPath);

            menuItemArtifacts_Click(sender, e);
        }

        private void contextArtCardOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetArtifactCard.SelectedRows)
            {
                Data.CardRow cardRow = (Data.CardRow)gridRow.Cells[columnArtCardName.Index].Value;
                IO.RunFile(cardRow.Path);
            }
        }

        private void speciesValidator_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            //if (string.Equals(e.OriginalValue, e.SpeciesName)) return;

            tdSpecies.Content = string.Format(
                Wild.Resources.Interface.Messages.SpeciesRename,
                e.OriginalValue, e.SpeciesName);

            if (tdSpecies.ShowDialog() == tdbSpcRename)
            {
                // TODO: If already exist?

                Data.SpeciesRow spcRow = data.Species.FindBySpecies(e.OriginalValue);
                Data.SpeciesRow spcRow1 = data.Species.FindBySpecies(e.SpeciesName);

                if (spcRow1 == null) // If there is no new species in reference
                {
                    spcRow.Species = e.SpeciesName;

                    foreach (Data.LogRow logRow in spcRow.GetLogRows())
                    {
                        RememberChanged(logRow.CardRow);
                    }
                }
                else // If new species is already in reference
                {
                    foreach (Data.LogRow logRow in spcRow.GetLogRows())
                    {
                        logRow.SpeciesRow = spcRow1;
                        RememberChanged(logRow.CardRow);
                    }

                    spcRow.Delete();
                }

                menuItemArtifacts_Click(sender, e);
                //GetSpeciesFullName(e.Row, e.Column, columnArtifactValidName);
            }
            else
            {
                 e.GetCell().Value = e.OriginalValue;
            }
        }

        private void artefactFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = FullStack.CheckConsistency();
        }

        private void artefactFinder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int count = 0;

            foreach (ConsistencyChecker artifact in (ConsistencyChecker[])e.Result)
            {
                count += artifact.ArtifactsCount;
            }

            labelArtifacts.Visible = pictureBoxArtifacts.Visible = count > 0;

            if (count > 0)
            {
                if (showArtifacts)
                {
                    List<CardArtifact> cardArtifacts = new List<CardArtifact>();
                    List<SpeciesArtifact> spcArtifacts = new List<SpeciesArtifact>();

                    foreach (ConsistencyChecker artifact in (ConsistencyChecker[])e.Result)
                    {
                        if (artifact is CardArtifact artefact1)
                        {
                            cardArtifacts.Add(artefact1);
                        }

                        if (artifact is SpeciesArtifact artefact2)
                        {
                            spcArtifacts.Add(artefact2);
                        }
                    }

                    tabPageArtifacts.Parent = tabControl;
                    tabControl.SelectedTab = tabPageArtifacts;

                    if (cardArtifacts.Count == 0)
                    {
                        tabPageArtifactCards.Parent = null;
                    }
                    else
                    {
                        tabPageArtifactCards.Parent = tabControl1;
                        ShowCardArtifacts(cardArtifacts.ToArray());
                    }

                    if (spcArtifacts.Count == 0)
                    {
                        tabPageArtifactSpecies.Parent = null;
                    }
                    else
                    {
                        tabPageArtifactSpecies.Parent = tabControl1;
                        ShowSpeciesArtifacts(spcArtifacts.ToArray());
                    }

                    showArtifacts = false;
                }
                else
                {
                    Notification.ShowNotification(
                        Wild.Resources.Interface.Messages.ArtifactsNotification,
                        Wild.Resources.Interface.Messages.ArtifactsNotificationInstruction,
                        menuItemArtifacts_Click);
                }
            }
            else
            {
                if (showArtifacts)
                {
                    Notification.ShowNotification(Wild.Resources.Interface.Messages.ArtifactsNoneNotification,
                        Wild.Resources.Interface.Messages.ArtifactsNoneNotificationInstruction);
                    showArtifacts = false;
                }
            }

            IsBusy = false;
            processDisplay.StopProcessing();
        }

        #endregion

        #region Cards

        private void menuItemAssignVariants_Click(object sender, EventArgs e)
        {
            Wild.Service.AssignAsFactors(spreadSheetCard.GetInsertedColumns(), true);
        }

        private void menuItemSplitExplorer_Click(object sender, EventArgs e)
        {
            SelectionValue selectionValue = new SelectionValue(spreadSheetCard.GetInsertedColumns());

            if (selectionValue.ShowDialog() != DialogResult.OK) return;

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
            {
                foreach (string value in gridColumn.GetStrings(true))
                {
                    List<string> filenames = new List<string>();

                    foreach (DataGridViewRow gridRow in gridColumn.GetRows(value))
                    {
                        filenames.Add(CardRow(gridRow).Path);
                    }

                    Data data = new Data();

                    foreach (string filename in filenames)
                    {
                        Data d = new Data();
                        d.Read(filename);
                        //d.Solitary.Path = filename;
                        d.CopyTo(data);
                    }

                    MainForm explorer = new MainForm(data);
                    explorer.ResetText(value, EntryAssemblyInfo.Title);
                    explorer.Show();
                }
            }
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

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void menuItemCardPrint_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species).Run();
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Individuals).Run();
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
        }



        private void spreadSheetCard_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetCard.ContainsFocus)
            {
                SaveCardRow(spreadSheetCard.Rows[e.RowIndex]);
            }
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

                if (DietExplorer && !cardRow.IsLabelNull())
                {
                    IO.RunFile(cardRow.Path, new string[] { "diet", cardRow.Label });
                }
                else
                {
                    IO.RunFile(cardRow.Path);
                }
            }
        }

        private void contextCardPrintNotes_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void contextCardPrint_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species).Run();
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Individuals).Run();
        }

        #endregion

        #region Individuals

        private void menuItemIndPrint_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetInd.Rows).GetReport().Run();
        }

        private void printIndividualsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetInd.SelectedRows).GetReport().Run();
        }

        private void RecoverMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WizardRecoverer recoverer = new WizardRecoverer(GetIndividuals(spreadSheetInd.Rows));
            WizardRecoverer recoverer = new WizardRecoverer(data);
            recoverer.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            recoverer.DataRecovered += recoverer_DataRecovered;
            recoverer.Show();
        }

        private void RecoverMassToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //WizardRecoverer recoverer = new WizardRecoverer(GetIndividuals(spreadSheetInd.SelectedRows));
            //recoverer.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            //recoverer.DataRecovered += recoverer_DataRecovered;
            //recoverer.Show();
        }

        private void recoverer_DataRecovered(object sender, EventArgs e)
        {
            WizardRecoverer recoverer = (WizardRecoverer)sender;
            foreach (Data.IndividualRow indRow in recoverer.RecoveredIndividualRows)
            {
                UpdateIndividualRow(indRow);
                RememberChanged(indRow.LogRow.CardRow);
            }
        }



        private void indLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (e.Argument == null)
            {
                for (int i = 0; i < data.Individual.Count; i++)
                {
                    result.Add(GetLine(data.Individual[i]));
                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }
            else
            {
                Data.SpeciesRow speciesRow = (Data.SpeciesRow)e.Argument;
                Data.IndividualRow[] indRows = speciesRow.GetIndividualRows();

                for (int i = 0; i < indRows.Length; i++)
                {
                    result.Add(GetLine(indRows[i]));
                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }

            e.Result = result.ToArray();

        }

        private void indLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tabPageInd.Parent = tabControl;
            tabControl.SelectedTab = tabPageInd;
            spreadSheetInd.Rows.AddRange(e.Result as DataGridViewRow[]);
            //spreadSheetInd.SetColumnsVisibility(new DataGridViewColumn[] {
            //    columnIndLength, columnIndSex, columnIndGrade, columnIndInstar }, true);

            IsBusy = false;
            spreadSheetInd.StopProcessing();
        }


        private void indExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(SetCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { IndividualRow(gridRow).LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }

        private void spreadSheetInd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetInd[e.ColumnIndex, e.RowIndex].Value is Age age)
            {
                if (age.Gain)
                {
                    spreadSheetInd[e.ColumnIndex, e.RowIndex].Style.Padding = new Padding(0, 0, 2, 0);
                }
                else
                {
                    spreadSheetInd[e.ColumnIndex, e.RowIndex].Style.Padding = new Padding(0, 0, 8, 0);
                }
            }

            Benthos.Service.HandleInstarInput(e, columnIndGrade, columnIndInstar);

            if (spreadSheetInd.ContainsFocus)
            {
                SaveIndRow(spreadSheetInd.Rows[e.RowIndex]);
            }
        }

        private void speciesInd_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            tdInd.Content = string.Format(
                Wild.Resources.Interface.Messages.IndRename,
                e.OriginalValue, e.SpeciesName);

            if (tdInd.ShowDialog() == tdbIndRename)
            {
                Data.IndividualRow individualRow = IndividualRow(e.Row);

                Data.SpeciesRow spcRow = data.Species.FindBySpecies(e.SpeciesName);

                if (spcRow == null)
                {
                    spcRow = data.Species.AddSpeciesRow(e.SpeciesName);
                }

                if (individualRow.LogRow.GetIndividualRows().Length == 1)
                {
                    // If individual is single - just replace log.species
                    individualRow.LogRow.SpeciesRow = spcRow;
                }
                else
                {
                    // If there are more individual(-s) - 
                    // create new log                   
                    Data.LogRow logRow = data.Log.FindByCardIDSpcID(individualRow.LogRow.CardRow.ID, spcRow.ID);

                    if (logRow == null)
                    {
                        logRow = data.Log.NewLogRow();
                        logRow.SpeciesRow = spcRow;
                        logRow.CardRow = individualRow.LogRow.CardRow;
                        logRow.Quantity = 1;
                        data.Log.AddLogRow(logRow);
                    }

                    // Correct former log row values
                    individualRow.LogRow.Quantity--;
                    if (!individualRow.LogRow.IsMassNull()) individualRow.LogRow.Mass -= individualRow.LogRow.Mass;

                    // Assign to new log row and correct its values
                    individualRow.LogRow = logRow;
                    logRow.Quantity++;
                    if (logRow.IsMassNull()) logRow.Mass = individualRow.Mass;
                    else logRow.Mass += individualRow.Mass;
                }

                RememberChanged(individualRow.LogRow.CardRow);
            }
            else
            {
                e.GetCell().Value = e.OriginalValue;
            }
        }

        private void contextInd_Opening(object sender, CancelEventArgs e)
        {
            printIndividualsLogToolStripMenuItem.Enabled = spreadSheetInd.SelectedRows.Count > 1;
        }

        private void contextIndLog_Click(object sender, EventArgs e)
        {
            if (spreadSheetLog.RowCount > 0)
            {
                selectCorrespondingLogRows();
                tabControl.SelectedTab = tabPageSpc;
                tabControl1.SelectedTab = tabPageLog;
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

                Data.IndividualRow individualRow = IndividualRow(gridRow);
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
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                Mayfly.IO.RunFile(individualRow.LogRow.CardRow.Path,
                    individualRow.LogRow.SpeciesRow.Species);

                // TODO: select row in a log
            }
        }

        private void contextIndDelete_Click(object sender, EventArgs e)
        {
            while (spreadSheetInd.SelectedRows.Count > 0)
            {
                Data.IndividualRow individualRow = IndividualRow(spreadSheetInd.SelectedRows[0]);
                RememberChanged(individualRow.LogRow.CardRow);
                data.Individual.Rows.Remove(individualRow);
                spreadSheetInd.Rows.Remove(spreadSheetInd.SelectedRows[0]);
            }
        }

        private void spreadSheetInd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Data.IndividualRow individualRow = IndividualRow(e.Row);
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

        #region Species

        private void menuItemSpcSave_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                SpeciesKey speciesKey = data.GetSpeciesKey();
                speciesKey.SaveToFile(Species.UserSettings.Interface.SaveDialog.FileName);
                Mayfly.IO.RunFile(Species.UserSettings.Interface.SaveDialog.FileName);
            }
        }

        //private void menuItemSpcValidate_Click(object sender, EventArgs e)
        //{
        //    if (ModifierKeys.HasFlag(Keys.Control))
        //    {
        //        if (Mayfly.Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            speciesValidator.IndexPath = Mayfly.Species.UserSettings.Interface.OpenDialog.FileName;
        //        }
        //    }

        //    IsBusy = true;
        //    spreadSheetSpc.StartProcessing(Data.Species.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
        //    columnSpcValid.Visible = true;
        //    loaderSpcList.RunWorkerAsync();
        //}

        //private void spcListLoader_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
        //    {
        //        GetSpeciesFullName(gridRow);
        //    }
        //}

        //private void spcListLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    IsBusy = false;
        //    spreadSheetSpc.StopProcessing();
        //}

        private void contextSpcDelete_Click(object sender, EventArgs e)
        {
            int rowsToDelete = spreadSheetSpc.SelectedRows.Count;
            while (rowsToDelete > 0)
            {
                Data.SpeciesRow spcRow = GetSpcRow(spreadSheetSpc.SelectedRows[0]);
                data.Species.RemoveSpeciesRow(spcRow);

                spreadSheetSpc.Rows.Remove(spreadSheetSpc.SelectedRows[0]);
                rowsToDelete--;
            }
        }



        private void SpcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Composition composition;

            if (baseSpc == null)
            {
                composition = FullStack.GetCenosisComposition();
            }
            else
            {
                composition = FullStack.GetTaxonomicComposition(baseSpc);

                //for (int i = 0; i < taxaSpc.Length; i++)
                //{
                //    result.Add(LogRow(taxaSpc[i]));
                //    (sender as BackgroundWorker).ReportProgress(i + 1);
                //}
                //result.Add(LogRowVaria());
            }

            //composition.SetLines(columnSpcSpc);

            for (int i = 0; i < composition.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetSpc);

                if (composition is TaxaComposition tc)
                {
                    gridRow.Cells[columnSpcSpc.Index].Value = tc[i].DataRow;
                }
                else if (composition is SpeciesComposition sc)
                {
                    gridRow.Cells[columnSpcSpc.Index].Value = sc[i].SpeciesRow;
                }

                gridRow.Cells[columnSpcQuantity.Index].Value = composition[i].Quantity;
                gridRow.Cells[columnSpcMass.Index].Value = composition[i].Mass;

                gridRow.Cells[columnSpcAbundance.Index].Value = composition[i].Abundance;
                gridRow.Cells[columnSpcBiomass.Index].Value = composition[i].Biomass * (DietExplorer ? 10000d : 1d);

                gridRow.Cells[columnSpcOccurrence.Index].Value = composition[i].Occurrence;
                gridRow.Cells[columnSpcDominance.Index].Value = composition[i].Dominance;

                if (composition is TaxaComposition composition1)
                {
                    gridRow.Cells[columnSpcDiversityA.Index].Value = composition1[i].DiversityA;
                    gridRow.Cells[columnSpcDiversityB.Index].Value = composition1[i].DiversityB;
                }

                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void SpcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSpc.ClearInsertedColumns();
            spreadSheetSpc.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
        }



        private void comboBoxSpcTaxa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //baseSpc = comboBoxSpcTaxa.SelectedItem == null ?
            //    null : ((DataRowView)comboBoxSpcTaxa.SelectedItem).Row as SpeciesKey.BaseRow;
            baseSpc = comboBoxSpcTaxa.SelectedItem as SpeciesKey.BaseRow;

            menuItemSpcTaxa.Enabled = baseSpc == null;

            if (baseSpc != null)
            {
                taxaSpc = baseSpc.GetTaxaRows();// data.Species.Taxa(baseSpc);
                variaSpc = baseSpc.Varia;
            }
            
            LoadSpc();
        }

        private void spreadSheetSpc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void buttonSpcSave_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Species.SpeciesKey speciesKey = new SpeciesKey();
                foreach (Data.SpeciesRow speciesRow in data.Species)
                {
                    Species.SpeciesKey.SpeciesRow newSpeciesRow = speciesKey.Species.NewSpeciesRow();
                    newSpeciesRow.Species = speciesRow.Species;
                    speciesKey.Species.AddSpeciesRow(newSpeciesRow);
                }

                speciesKey.SaveToFile(Species.UserSettings.Interface.SaveDialog.FileName);
            }
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



        private void contextLogOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                Data.LogRow logRow = GetLogRow(gridRow);
                IO.RunFile(logRow.CardRow.Path, logRow.SpeciesRow.Species);
            }
        }



        private void comparerLog_DoWork(object sender, DoWorkEventArgs e)
        {
            string field = (string)e.Argument;

            string format = spreadSheetLog.Columns[field] == null ? string.Empty :
                spreadSheetLog.Columns[field].DefaultCellStyle.Format;

            string[] variants = data.GetVariantsOf(field, format);

            List<Composition> result = new List<Composition>();

            foreach (string variant in variants)
            {
                CardStack stack = FullStack.GetStack(field, variant, format);

                Composition composition;

                if (baseLog == null) {
                    composition = stack.GetCenosisComposition();
                } else {
                    composition = stack.GetTaxonomicComposition(baseLog);
                }

                composition.Name = variant;
                result.Add(composition);
            }

            e.Result = result;
        }

        private void comparerLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Composition composition;

            if (baseLog == null) {
                composition = FullStack.GetBasicCenosisComposition();
            } else {
                composition = FullStack.GetTaxonomicComposition(baseLog);
            }

            CompositionComparison comcom = new CompositionComparison(
                composition, (List<Composition>)e.Result);

            //comcom.UpdateCompositionAppearance();
            comcom.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            comcom.Show();

            IsBusy = false;
            spreadSheetLog.StopProcessing();
        }
        

        private void adapterLog_OperatingChanged(object sender, EventArgs e)
        { }



        private void LogLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (baseLog == null)
            {
                for (int i = 0; i < data.Card.Count; i++)
                {
                    foreach (Data.SpeciesRow speciesRow in data.Species)
                    {
                        DataGridViewRow gridRow = LogRow(data.Card[i], speciesRow);

                        result.Add(gridRow);

                        if ((double)gridRow.Cells[columnLogAbundance.Index].Value == 0)
                        {
                            spreadSheetLog.SetHidden(gridRow);
                        }
                    }

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }
            else
            {
                for (int i = 0; i < data.Card.Count; i++)
                {
                    foreach (SpeciesKey.TaxaRow taxaRow in taxaLog)
                    {
                        DataGridViewRow gridRow = LogRow(data.Card[i], taxaRow);

                        result.Add(gridRow);

                        if ((double)gridRow.Cells[columnLogAbundance.Index].Value == 0)
                        {
                            spreadSheetLog.SetHidden(gridRow);
                        }
                    }

                    DataGridViewRow variaRow = LogRowVaria(data.Card[i]);
                    result.Add(variaRow);
                    if ((double)variaRow.Cells[columnLogAbundance.Index].Value == 0)
                    {
                        spreadSheetLog.SetHidden(variaRow);
                    }

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }

            e.Result = result.ToArray();
        }

        private void LogLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetLog.Rows.AddRange(e.Result as DataGridViewRow[]);
            //spreadSheetLog.InitiateBackgroundTable();
            IsBusy = false;
            spreadSheetLog.StopProcessing();
        }



        private void LogExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                //if (spreadSheetLog.IsHidden(gridRow)) continue;

                ValueSetEventHandler valueSetter = new ValueSetEventHandler(SetCardValue);

                bool trueLogRow = baseLog == null && gridRow.Cells[columnLogQuantity.Index].Value != null;

                gridRow.DataGridView.Invoke(valueSetter,
                    new object[] { trueLogRow ? GetLogRow(gridRow).CardRow : CardRow(gridRow, columnLogID),
                        gridRow, spreadSheetLog.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void comboBoxLogTaxa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //baseLog = comboBoxLogTaxa.SelectedItem == null ?
            //    null : ((DataRowView)comboBoxLogTaxa.SelectedItem).Row as SpeciesKey.BaseRow;
            baseLog = comboBoxLogTaxa.SelectedItem as SpeciesKey.BaseRow;

            menuItemSpcTaxa.Enabled = baseLog == null;

            if (baseLog != null)
            {
                taxaLog = data.Species.Taxa(baseLog);
                variaLog = baseLog.Varia;
            }

            LoadLog();
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

                Data.LogRow logRow = GetLogRow(e.Row);
                logRow.SpeciesRow = spcRow;

                RememberChanged(logRow.CardRow);
            }
            else
            {
                e.GetCell().Value = e.OriginalValue;
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

        private void contextCardExplore_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();

            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                if (!filenames.Contains("\"" + cardRow.Path + "\"")) filenames.Add("\"" + cardRow.Path + "\"");
            }

            IO.RunFile(Application.ExecutablePath, filenames.ToArray());
            //MainForm newMain = new MainForm(filenames.ToArray());
            //newMain.Show();
        }

        private void menuItemExportBio_Click(object sender, EventArgs e)
        {
            Wild.UserSettings.InterfaceBio.SaveDialog.FileName = IO.SuggestName(
                Wild.UserSettings.InterfaceBio.SaveDialog.InitialDirectory,
                IO.GetFriendlyCommonName(data.GetFilenames())
                );

            if (Wild.UserSettings.InterfaceBio.SaveDialog.ShowDialog(this) == DialogResult.OK)
            {
                data.GetBenthosBio().ExportBio(Wild.UserSettings.InterfaceBio.SaveDialog.FileName);
            }
        }
    }
}