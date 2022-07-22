using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Plankton.Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SpeciesIndex = Plankton.UserSettings.SpeciesIndex;
            speciesValidator.IndexPath = 
            speciesLog.IndexPath =
            speciesInd.IndexPath = Plankton.UserSettings.TaxonomicIndexPath;

            listViewWaters.Shine();
            listViewSamplers.Shine();
            listViewInvestigators.Shine();

            labelCardCount.UpdateStatus(0);
            labelSpcCount.UpdateStatus(0);
            labelLogCount.UpdateStatus(0);
            labelIndCount.UpdateStatus(0);

            selectedLogRows = new List<Wild.Survey.LogRow>();

            columnCardWater.ValueType = typeof(string);
            columnCardLabel.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardSampler.ValueType = typeof(string);
            columnCardVolume.ValueType = typeof(double);
            columnCardDepth.ValueType = typeof(double);
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

            tabPageLog.Parent = null;

            LoadTaxonList();

            columnIndID.ValueType = typeof(int);
            columnIndSpecies.ValueType = typeof(string);
            columnIndLength.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndSex.ValueType = typeof(Sex);
            columnIndGrade.ValueType = typeof(Grade);
            columnIndComments.ValueType = typeof(string);

            tabPageInd.Parent = null;

            tabPageArtifacts.Parent = null;

            menuStrip.SetMenuIcons();

            if (UserSettings.AutoLoadBio)
            {
                processDisplay.StartProcessing(Wild.Resources.Interface.Process.BioLoading);
                backSpecSheetLoader.RunWorkerAsync(UserSettings.Bios);
            }

            IsEmpty = true;
        }

        public MainForm(Wild.Survey _data) : this()
        {
            data = _data;
        }

        public MainForm(CardStack stack)
            : this()
        {
            foreach (Wild.Survey.CardRow cardRow in stack)
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

                Wild.Survey _data = new Wild.Survey();

                if (_data.Read(filenames[i]))
                {
                    if (_data.Card.Count == 0)
                        Log.Write(string.Format("File is empty: {0}.", filenames[i]));
                    else
                    {
                        Wild.Survey.CardRow[] cardRows = _data.CopyTo(data);
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
            menuItemCards.Visible = tabControl.SelectedTab == tabPageCard;
            menuItemSpc.Visible = tabControl.SelectedTab == tabPageSpc;
            menuItemLog.Visible = tabControl.SelectedTab == tabPageLog;
            menuItemIndividuals.Visible = tabControl.SelectedTab == tabPageInd;
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        #region Menus

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            SaveCards();
        }

        private void menuItemAddData_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog(this) == DialogResult.OK)
            {
                LoadCards(UserSettings.Interface.OpenDialog.FileNames);
            }
        }

        private void menuItemBackupCards_Click(object sender, EventArgs e)
        {
            if (fbDialogBackup.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Wild.Survey.CardRow cardRow in data.Card)
                {
                    Wild.Survey _data = cardRow.SingleCardDataset();
                    string filename = IO.SuggestName(fbDialogBackup.SelectedPath, _data.Solitary.GetSuggestedName());
                    _data.WriteToFile(Path.Combine(fbDialogBackup.SelectedPath, filename));
                }
            }
        }

        private void menuItemExportSpec_Click(object sender, EventArgs e)
        {
            if (Wild.UserSettings.InterfaceBio.SaveDialog.ShowDialog(this) == DialogResult.OK)
            {
                data.ExportBio(Wild.UserSettings.InterfaceBio.SaveDialog.FileName);
            }
        }

        private void menuItemImportSpec_Click(object sender, EventArgs e)
        {
            if (Wild.UserSettings.InterfaceBio.OpenDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (data.IsBioLoaded)
                {
                    TaskDialogButton tdb = taskDialogSpec.ShowDialog();

                    if (tdb != tdbSpecCancel)
                    {
                        processDisplay.StartProcessing(Wild.Resources.Interface.Process.BioLoading);
                        backSpecSheetLoader.RunWorkerAsync(Wild.UserSettings.InterfaceBio.OpenDialog.FileName);
                    }
                }
                else
                {
                    processDisplay.StartProcessing(Wild.Resources.Interface.Process.BioLoading);
                    backSpecSheetLoader.RunWorkerAsync(Wild.UserSettings.InterfaceBio.OpenDialog.FileName);
                }
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Sample menu

        private void menuItemSample_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void menuItemArtifactsSearch_Click(object sender, EventArgs e)
        {
            tabPageArtifacts.Parent = tabControl;
            tabControl.SelectedTab = tabPageArtifacts;
            FindArtifacts();
            LoadArtifacts();
        }

        //private void menuItemRecoverWizard_Click(object sender, EventArgs e)
        //{
        //    WizardRecoverer recoverer = new WizardRecoverer(Data);
        //    recoverer.SetFriendlyDesktopLocation(this, FormLocation.Centered);
        //    recoverer.DataRecovered += recoverer_DataRecovered;
        //    recoverer.Show();
        //}

        //private void recoverer_DataRecovered(object sender, EventArgs e)
        //{
        //    WizardRecoverer recoverer = (WizardRecoverer)sender;

        //    foreach (Data.CardRow cardRow in recoverer.BadData.Card)
        //    {
        //        RememberChanged(cardRow);
        //        UpdateCardRow(cardRow);
        //    }

        //    foreach (Data.LogRow logRow in recoverer.BadData.Log)
        //    {
        //        UpdateLogRow(logRow);
        //    }

        //    foreach (Data.IndividualRow individualRow in recoverer.BadData.Individual)
        //    {
        //        UpdateIndividualRow(individualRow);
        //    }

        //    UpdateSummary();
            
        //    recoverer.Protocol.Run();
        //}

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

            tabPageSpc.Parent = tabControl;

            tab_Changed(sender, e);

            LoadSpc();
        }

        private void menuItemLoadLog_Click(object sender, EventArgs e)
        {
            tabPageSpc.Parent = tabControl;
            tabControl.SelectedTab = tabPageSpc;

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

        #region Cards menu

        private void menuItemAssignVariants_Click(object sender, EventArgs e)
        {
            Wild.Service.AssignAsFactors(spreadSheetCard.GetInsertedColumns(), true);
        }

        private void menuItemCardFindEmpty_Click(object sender, EventArgs e)
        {
            spreadSheetCard.ClearSelection();
            foreach (DataGridViewRow gridRow in spreadSheetCard.Rows)
            {
                Wild.Survey.CardRow cardRow = GetCardRow(gridRow);
                gridRow.Selected = cardRow.GetLogRows().Length == 0;
            }
        }

        private void menuItemCardPrint_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.Rows, CardReportLevel.Note | CardReportLevel.Log);
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.Rows, CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Individuals);
        }

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.Rows, CardReportLevel.Note);
        }

        #endregion

        #region General species log

        private void menuItemSpcSave_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                TaxonomicIndex speciesKey = data.GetSpeciesKey();
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

        private void BaseItem_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.BaseRow baseRow = ((ToolStripMenuItem)sender).Tag as TaxonomicIndex.BaseRow;
            DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(baseRow.BaseName,
                baseRow.BaseName, typeof(string), 0);

            foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
            {
                if (gridRow.Cells[columnSpcSpc.Index].Value == null)
                {
                    continue;
                }

                if (gridRow.Cells[columnSpcSpc.Index].Value as string ==
                    Species.Resources.Interface.UnidentifiedTitle)
                {
                    continue;
                }

                TaxonomicIndex.TaxonRow speciesRow = SpeciesIndex.Definition.FindByName(
                    gridRow.Cells[columnSpcSpc.Index].Value as string);

                if (speciesRow == null)
                {
                    gridRow.Cells[gridColumn.Index].Value = null;
                }
                else
                {
                    TaxonomicIndex.TaxonRow taxonRow = speciesRow.GetTaxon(baseRow);
                    if (taxonRow != null) gridRow.Cells[gridColumn.Index].Value = taxonRow.TaxonName;
                }
            }
        }

        #endregion

        #region Full species log

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

                Composition composition = stack.GetBasicCenosisComposition();
                composition.Name = variant.ToString();
                result.Add(composition);
            }

            e.Result = result;
        }

        private void comparerLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CompositionComparison comcom = new CompositionComparison(
                data.GetStack().GetBasicCenosisComposition(),
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

        private void menuItemLicenses_Click(object sender, EventArgs e)
        {
            //Features lics = new Features();
            //lics.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            //lics.ShowDialog(this);
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            about.ShowDialog();
        }

        #endregion

        #endregion

        #region Background Workers

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }



        private void backSpecSheetLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            data.ImportBio((string)e.Argument);//, tdb == tdbSpecClear);
        }

        private void backSpecSheetLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statusBio.Visible = data.IsBioLoaded;

            processDisplay.StopProcessing();

            if (tabPageInd.Parent == null) return;

            processDisplay.StartProcessing(spreadSheetInd.RowCount, Wild.Resources.Interface.Process.SpecApply);
            specTipper.RunWorkerAsync();
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

        #endregion

        #region General

        private void cards_DragDrop(object sender, DragEventArgs e)
        {
            cards_DragLeave(sender, e);
            LoadCards(e.GetOperableFilenames(Plankton.UserSettings.Interface.Extension));
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

        private void listViewSamplers_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardSampler, listViewSamplers.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardInvestigator, listViewInvestigators.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        #endregion

        private void speciesValidator_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            //if (string.Equals(e.OriginalValue, e.SpeciesName)) return;

            tdSpecies.Content = string.Format(
                Wild.Resources.Interface.Messages.SpeciesRename,
                e.OriginalValue, e.SelectedTaxonName);

            if (tdSpecies.ShowDialog() == tdbSpcRename)
            {
                // TODO: If already exist?

                Wild.Survey.DefinitionRow spcRow = data.Definition.FindByName(e.OriginalValue);
                Wild.Survey.DefinitionRow spcRow1 = data.Definition.FindByName(e.SelectedTaxonName);

                if (spcRow1 == null) // If there is no new species in index
                {
                    spcRow.Species = e.SelectedTaxonName;

                    foreach (Wild.Survey.LogRow logRow in spcRow.GetLogRows())
                    {
                        RememberChanged(logRow.CardRow);
                    }
                }
                else // If there is new species already in index
                {
                    foreach (Wild.Survey.LogRow logRow in spcRow.GetLogRows())
                    {
                        logRow.DefinitionRow = spcRow1;
                        RememberChanged(logRow.CardRow);
                    }

                    spcRow.Delete();
                }

                GetSpeciesFullName(e.Row, e.Column, columnArtifactValidName);
            }
            else
            {
                 e.GetCell().Value = e.OriginalValue;
            }
        }

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

        private void contextCard_Opening(object sender, CancelEventArgs e)
        {
            contextCardOpen.Enabled = false;

            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Wild.Survey.CardRow cardRow = GetCardRow(gridRow);
                if (cardRow.Path == null) continue;
                contextCardOpen.Enabled = true;
                break;
            }
        }

        private void contextCardOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Wild.Survey.CardRow cardRow = GetCardRow(gridRow);
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

        private void contextCardLog_Click(object sender, EventArgs e)
        {

        }

        private void contextCardPrint_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.SelectedRows, CardReportLevel.Note | CardReportLevel.Log);
        }

        private void contextCardPrintNotes_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.SelectedRows, CardReportLevel.Note);
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e)
        {
            PrintCards((IList)spreadSheetCard.SelectedRows, CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Individuals);
        }

        #endregion        

        #region Species

        private void comboBoxSpc_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseSpc = comboBoxSpc.SelectedItem as TaxonomicIndex.BaseRow;

            menuItemSpcTaxon.Enabled = baseSpc == null;

            if (baseSpc != null)
            {
                taxonSpc = data.Species.Taxon(baseSpc);
                variaSpc = baseLog.Varia;
            }

            if (tabPageSpc.Parent == tabControl) LoadSpc();
        }

        private void spreadSheetSpc_Filtered(object sender, EventArgs e)
        {
            UpdateSpcTotals();
        }

        private void spreadSheetSpc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonSpcSave_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Species.TaxonomicIndex speciesKey = new TaxonomicIndex();
                foreach (Wild.Survey.DefinitionRow speciesRow in data.Species)
                {
                    Species.TaxonomicIndex.TaxonRow newSpeciesRow = speciesKey.Species.NewSpeciesRow();
                    newSpeciesRow.Species = speciesRow.Species;
                    speciesKey.Species.AddSpeciesRow(newSpeciesRow);
                }

                speciesKey.SaveToFile(Species.UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void contextSpcDelete_Click(object sender, EventArgs e)
        {
            int rowsToDelete = spreadSheetSpc.SelectedRows.Count;
            while (rowsToDelete > 0)
            {
                Wild.Survey.DefinitionRow spcRow = GetSpcRow(spreadSheetSpc.SelectedRows[0]);
                data.Species.RemoveSpeciesRow(spcRow);

                spreadSheetSpc.Rows.Remove(spreadSheetSpc.SelectedRows[0]);
                rowsToDelete--;
            }
        }

        #endregion

        #region Log

        private void comboBoxLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseLog = comboBoxLog.SelectedItem as TaxonomicIndex.BaseRow;

            menuItemSpcTaxon.Enabled = baseLog == null;

            if (baseLog != null)
            {
                taxonLog = data.Species.Taxon(baseLog);
                variaLog = baseLog.Varia;
            }
            
            if (tabPageLog.Parent == tabControl) LoadLog();
        }

        private void LogLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (baseLog == null)
            {
                for (int i = 0; i < data.Card.Count; i++)
                {
                    foreach (Wild.Survey.DefinitionRow speciesRow in data.Species)
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
                    foreach (TaxonomicIndex.TaxonRow taxonRow in taxonLog)
                    {
                        DataGridViewRow gridRow = LogRow(data.Card[i], taxonRow);

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
                    new object[] { baseLog == null ? GetLogRow(gridRow).CardRow : GetCardRow(gridRow, columnLogID),
                        gridRow, spreadSheetLog.GetInsertedColumns() });
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
                e.OriginalValue, e.SelectedTaxonName);

            if (tdLog.ShowDialog() == tdbLogRename)
            {
                Wild.Survey.DefinitionRow spcRow = data.Definition.FindByName(e.SelectedTaxonName);

                if (spcRow == null)
                {
                    spcRow = data.Species.AddSpeciesRow(e.SelectedTaxonName);
                }

                Wild.Survey.LogRow logRow = GetLogRow(e.Row);
                logRow.DefinitionRow = spcRow;

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
                Wild.Survey.LogRow logRow = GetLogRow(gridRow);
                Mayfly.IO.RunFile(logRow.CardRow.Path, logRow.DefinitionRow.Taxon);
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
                columnIndLength, columnIndSex, columnIndGrade }, true);
            //spreadSheetInd.InitiateBackgroundTable();
            UpdateIndTotals();
            IsBusy = false;
            spreadSheetInd.StopProcessing();
        }

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
                e.OriginalValue, e.SelectedTaxonName);

            if (tdInd.ShowDialog() == tdbIndRename)
            {
                Wild.Survey.IndividualRow individualRow = GetIndividualRow(e.Row);

                Wild.Survey.DefinitionRow spcRow = data.Definition.FindByName(e.SelectedTaxonName);

                if (spcRow == null)
                {
                    spcRow = data.Species.AddSpeciesRow(e.SelectedTaxonName);
                }

                if (individualRow.LogRow.Quantity == 1)
                {
                    // If individual is single - just replace log.species
                    individualRow.LogRow.SpeciesRow = spcRow;
                }
                else
                {
                    // If there are more individual(-s) - create new log and replace individual to it                    
                    Wild.Survey.LogRow logRow = data.Log.FindByCardIDSpcID(individualRow.LogRow.CardRow.ID, spcRow.ID);

                    if (logRow == null)
                    {
                        logRow = data.Log.NewLogRow();
                        logRow.DefinitionRow = spcRow;
                        logRow.CardRow = individualRow.LogRow.CardRow;
                        logRow.Quantity = 1;
                        data.Log.AddLogRow(logRow);
                    }

                    individualRow.LogRow.Quantity--;
                    if (!individualRow.LogRow.IsMassNull()) individualRow.LogRow.Mass -= individualRow.LogRow.Mass;

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

                Wild.Survey.IndividualRow individualRow = GetIndividualRow(gridRow);
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
                Wild.Survey.IndividualRow individualRow = GetIndividualRow(gridRow);

                Mayfly.IO.RunFile(individualRow.LogRow.CardRow.Path,
                    individualRow.LogRow.SpeciesRow.Species);

                // TODO: select row in a log
            }
        }

        private void contextIndDelete_Click(object sender, EventArgs e)
        {
            while (spreadSheetInd.SelectedRows.Count > 0)
            {
                Wild.Survey.IndividualRow individualRow = GetIndividualRow(spreadSheetInd.SelectedRows[0]);
                RememberChanged(individualRow.LogRow.CardRow);
                data.Individual.Rows.Remove(individualRow);
                spreadSheetInd.Rows.Remove(spreadSheetInd.SelectedRows[0]);
            }
        }

        private void spreadSheetInd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Wild.Survey.IndividualRow individualRow = GetIndividualRow(e.Row);
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