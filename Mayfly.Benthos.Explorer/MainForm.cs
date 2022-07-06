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
            listViewDates.Shine();

            spreadSheetCard.UpdateStatus();
            spreadSheetSpc.UpdateStatus();
            spreadSheetLog.UpdateStatus();
            spreadSheetInd.UpdateStatus();

            SetSpeciesIndex(Benthos.UserSettings.TaxonomicIndexPath);

            selectedLogRows = new List<Data.LogRow>();

            columnCardWater.ValueType = typeof(string);
            columnCardLabel.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardWhere.ValueType = typeof(Waypoint);
            columnCardWeather.ValueType = typeof(WeatherState);
            columnCardTempSurface.ValueType = typeof(double);
            columnCardSampler.ValueType = typeof(string);
            columnCardSquare.ValueType = typeof(double);
            columnCardSubstrate.ValueType = typeof(string);
            columnCardDepth.ValueType = typeof(double);
            columnCardCrossSection.ValueType = typeof(string);
            columnCardBank.ValueType = typeof(string);
            columnCardWealth.ValueType = typeof(double);
            columnCardQuantity.ValueType = typeof(int);
            columnCardMass.ValueType = typeof(double);
            columnCardAbundance.ValueType = typeof(double);
            columnCardBiomass.ValueType = typeof(double);
            columnCardDiversityA.ValueType = typeof(double);
            columnCardDiversityB.ValueType = typeof(double);
            columnCardComments.ValueType = typeof(string);

            tabPageCard.Parent = null;

            columnSpcSpc.ValueType = typeof(TaxonomicIndex.TaxonRow);
            columnSpcQuantity.ValueType = typeof(int);
            columnSpcMass.ValueType = typeof(double);
            columnSpcAbundance.ValueType = typeof(double);
            columnSpcBiomass.ValueType = typeof(double);
            columnSpcOccurrence.ValueType = typeof(double);
            columnSpcDominance.ValueType = typeof(double);
            columnSpcDiversityA.ValueType = typeof(double);
            columnSpcDiversityB.ValueType = typeof(double);

            tabPageComposition.Parent = null;

            columnLogSpc.ValueType = typeof(TaxonomicIndex.TaxonRow);
            columnLogQuantity.ValueType = typeof(int);
            columnLogMass.ValueType = typeof(double);
            columnLogAbundance.ValueType = typeof(double);
            columnLogBiomass.ValueType = typeof(double);
            columnLogDiversityA.ValueType = typeof(double);
            columnLogDiversityB.ValueType = typeof(double);

            tabPageLog.Parent = null;

            columnIndID.ValueType = typeof(int);
            columnIndSpecies.ValueType = typeof(string);
            columnIndFrequency.ValueType = typeof(int);
            columnIndLength.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndSex.ValueType = typeof(Sex);
            columnIndGrade.ValueType = typeof(Grade);
            columnIndInstar.ValueType = typeof(int);
            columnIndComments.ValueType = typeof(string);

            tabPageInd.Parent = null;

            menuStrip.SetMenuIcons();

            spreadSheetInd.SetBioAcceptable(LoadCards);
            spreadSheetSpc.SetBioAcceptable(LoadCards);
            spreadSheetLog.SetBioAcceptable(LoadCards);

            data = new Data(Benthos.UserSettings.SpeciesIndex, Benthos.UserSettings.SamplersIndex);
            FullStack = new CardStack();

            loadTaxonList();

            IsEmpty = true;
        }

        public MainForm(string[] args)
            : this()
        {
            if (args.Length == 0)
            {
                return;
            }

            this.Load += (o, e) => { LoadCards(args.GetOperableFilenames(Benthos.UserSettings.Interface.Extension)); };

        }

        public MainForm(CardStack stack)
            : this()
        {
            foreach (Data.CardRow cardRow in stack)
            {
                cardRow.SingleCardDataset().CopyTo(data);
            }

            updateSummary();
        }

        public MainForm(Data _data) : this()
        {
            data = _data;
            data.RefreshBios();
        }
        


        private void mainForm_Load(object sender, EventArgs e)
        {
            updateSummary();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

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
            menuCards.Visible = (tabControl.SelectedTab == tabPageCard);
            menuComposition.Visible = (tabControl.SelectedTab == tabPageComposition);
            menuIndividuals.Visible = (tabControl.SelectedTab == tabPageInd);
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }



        private void dataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;

            processDisplay.SetProgressMaximum(filenames.Length);

            for (int i = 0; i < filenames.Length; i++)
            {
                if (Path.GetExtension(filenames[i]) == Wild.UserSettings.InterfaceBio.Extension)
                {
                    processDisplay.SetStatus(Wild.Resources.Interface.Process.BioLoading);

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
                        else
                        {
                            _data.CopyTo(data);
                        }
                    }
                }

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }
        }

        private void dataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCard.StopProcessing();
            updateSummary();
        }


        private void modelCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            data.RefreshBios();
        }

        private void modelCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                processDisplay.StartProcessing(Wild.Resources.Interface.Process.ArtifactsProcessing);
            }
            else
            {
                processDisplay.StopProcessing();
                IsBusy = false;
            }

            artifactFinder.RunWorkerAsync();

            updateMass(FullStack.Mass());
        }



        private void artifactFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!UserSettings.CheckConsistency)
            {
                e.Cancel = true;
                return;
            }

            e.Result = FullStack.CheckConsistency();
        }

        private void artifactFinder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                ConsistencyChecker[] artifacts = (ConsistencyChecker[])e.Result;

                labelArtifacts.Visible = pictureBoxArtifacts.Visible = artifacts.Length > 0;

                if (artifacts.Length > 0)
                {
                    Notification.ShowNotification(
                        Wild.Resources.Interface.Messages.ArtifactsNotification,
                        Wild.Resources.Interface.Messages.ArtifactsNotificationInstruction);
                }
                else
                {
                    Notification.ShowNotification(Wild.Resources.Interface.Messages.ArtifactsNoneNotification,
                        Wild.Resources.Interface.Messages.ArtifactsNoneNotificationInstruction);
                }

                updateArtifacts();
            }

            IsBusy = false;
            processDisplay.StopProcessing();
        }


        private void dataSaver_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;
            processDisplay.SetProgressMaximum(ChangedCards.Count);

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
            updateSummary();
            menuItemSave.Enabled = IsChanged;
            if (isClosing) { Close(); }
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
            if (fbdBackup.ShowDialog(this) == DialogResult.OK)
            {
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    data.WriteToFile(Path.Combine(fbdBackup.SelectedPath, "backup.xml"));
                }
                else foreach (Data.CardRow cardRow in data.Card)
                    {
                        string filename = IO.SuggestName(fbdBackup.SelectedPath, cardRow.GetSuggestedName());
                        Data _data = cardRow.SingleCardDataset();
                        _data.WriteToFile(Path.Combine(fbdBackup.SelectedPath, filename));
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

        private void menuItemImportBio_Click(object sender, EventArgs e)
        {
            if (Wild.UserSettings.InterfaceBio.OpenDialog.ShowDialog(this) == DialogResult.OK)
            {
                LoadCards(Wild.UserSettings.InterfaceBio.OpenDialog.FileName);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Materials

        private void menuItemCards_Click(object sender, EventArgs e)
        {
            loadCards();
        }

        private void menuItemLog_Click(object sender, EventArgs e)
        {
            loadLog();
        }

        private void menuItemIndAll_Click(object sender, EventArgs e)
        {
            loadIndividuals();
        }

        #endregion

        private void menuItemComposition_Click(object sender, EventArgs e)
        {
            loadSpc();
        }

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
            List<string> ext = new List<string>();
            ext.Add(Benthos.UserSettings.Interface.Extension);
            ext.Add(Wild.UserSettings.InterfaceBio.Extension);
            LoadCards(e.GetOperableFilenames(ext.ToArray()));
        }

        private void cards_DragEnter(object sender, DragEventArgs e)
        {
            List<string> ext = new List<string>();
            ext.Add(Benthos.UserSettings.Interface.Extension);
            ext.Add(Wild.UserSettings.InterfaceBio.Extension);
            if (e.GetOperableFilenames(ext.ToArray()).Length > 0)
            {
                e.Effect = DragDropEffects.All;

                foreach (Control ctrl in tabPageInfo.Controls)
                {
                    ctrl.ForeColor = Color.LightGray;
                }
            }
        }

        private void cards_DragLeave(object sender, EventArgs e)
        {
            foreach (Control ctrl in tabPageInfo.Controls)
            {
                ctrl.ForeColor = SystemColors.ControlText;
            }
        }


        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            loadCards(FullStack.GetStack(data.Water.FindByID(listViewWaters.FocusedItem.GetID())));
        }

        private void listViewSamplers_ItemActivate(object sender, EventArgs e)
        {
            loadCards(FullStack.GetStack("SamplerID", listViewSamplers.FocusedItem.GetID()));
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e)
        {
            loadCards(FullStack.GetStack("Investigator", listViewInvestigators.FocusedItem.Text));
        }

        private void listViewDates_ItemActivate(object sender, EventArgs e)
        {
            DateTime[] dt = (DateTime[])listViewDates.FocusedItem.Tag;
            spreadSheetCard.EnsureFilter(columnCardWhen, dt[0].ToOADate(), dt[dt.Length - 1].ToOADate() + 1, loaderCard, menuItemCards_Click);
        }

        #endregion



        #region Cards

        private void menuItemCardMeteo_CheckedChanged(object sender, EventArgs e)
        {
            columnCardWeather.Visible =
            columnCardTempSurface.Visible =
                menuItemCardMeteo.Checked;
        }

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
                        filenames.Add(findCardRow(gridRow).Path);
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
                Data.CardRow cardRow = findCardRow(gridRow);
                gridRow.Selected = cardRow.GetLogRows().Length == 0;
            }
        }

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void menuItemCardPrint_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Log).Run();
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Individuals).Run();
        }



        private void cardLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            CardStack stack = (CardStack)e.Argument;
            processDisplay.SetProgressMaximum(stack.Count);

            for (int i = 0; i < stack.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetCard);
                setCardValue(stack[i], gridRow, columnCardID, "ID");
                updateCardRow(gridRow);
                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void cardLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCard.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetCard.StopProcessing();
            spreadSheetCard.UpdateStatus();

            tabPageCard.Parent = tabControl;
            tabControl.SelectedTab = tabPageCard;

            updateArtifacts();
        }



        private void spreadSheetCard_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetCard.ContainsFocus)
            {
                saveCardRow(spreadSheetCard.Rows[e.RowIndex]);
            }
        }


        private void contextCard_Opening(object sender, CancelEventArgs e)
        {
            contextCardOpen.Enabled = false;

            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = findCardRow(gridRow);
                if (cardRow.Path == null) continue;
                contextCardOpen.Enabled = true;
                break;
            }
        }

        private void contextCardOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = findCardRow(gridRow);
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
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void contextCardPrint_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Log).Run();
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Individuals).Run();
        }

        private void contextCardExplore_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();

            foreach (string path in getCardStack(spreadSheetCard.SelectedRows).GetFilenames())
            {
                if (!filenames.Contains("\"" + path + "\"")) filenames.Add("\"" + path + "\"");
            }

            IO.RunFile(Application.ExecutablePath, filenames.ToArray());
            //MainForm newMain = new MainForm(filenames.ToArray());
            //newMain.Show();
        }

        #endregion

        #region Species

        private void contextSpcAddToReference_Click(object sender, EventArgs e)
        {

        }

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



        private void spcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Composition composition;

            if (rankSpc == null)
            {
                composition = FullStack.GetCenosisComposition();
            }
            else
            {
                composition = FullStack.GetCenosisComposition(rankSpc);
            }

            processDisplay.SetProgressMaximum(composition.Count);

            for (int i = 0; i < composition.Count; i++)
            {
                if (composition[i].Quantity == 0) continue;

                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetSpc);                

                if (composition is TaxonomicComposition tc)
                {
                    gridRow.Cells[columnSpcSpc.Index].Value = tc[i].DataRow;
                    gridRow.Cells[columnSpcID.Index].Value = tc[i].DataRow?.ID;

                    if (tc[i].DataRow == null) gridRow.Cells[columnSpcSpc.Index].Value = tc[i].Name;
                }
                else if (composition is SpeciesComposition sc)
                {
                    gridRow.Cells[columnSpcSpc.Index].Value = sc[i].SpeciesRow;
                    gridRow.Cells[columnSpcID.Index].Value = sc[i].SpeciesRow?.ID;
                }

                gridRow.Cells[columnSpcQuantity.Index].Value = composition[i].Quantity;
                gridRow.Cells[columnSpcMass.Index].Value = composition[i].Mass;

                gridRow.Cells[columnSpcAbundance.Index].Value = composition[i].Abundance;
                gridRow.Cells[columnSpcBiomass.Index].Value = composition[i].Biomass * (DietExplorer ? 10000d : 1d);

                gridRow.Cells[columnSpcOccurrence.Index].Value = composition[i].Occurrence;
                gridRow.Cells[columnSpcDominance.Index].Value = composition[i].Dominance;

                if (composition is TaxonomicComposition composition1)
                {
                    gridRow.Cells[columnSpcDiversityA.Index].Value = composition1[i].DiversityA;
                    gridRow.Cells[columnSpcDiversityB.Index].Value = composition1[i].DiversityB;
                }

                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void spcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSpc.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
            spreadSheetSpc.UpdateStatus();

            tabPageComposition.Parent = tabControl;
            tabControl.SelectedTab = tabPageComposition;

            updateArtifacts();
            //spreadSheetSpc.ClearInsertedColumns();
        }



        private void comboBoxSpcTaxon_SelectedIndexChanged(object sender, EventArgs e)
        {
            rankSpc = comboBoxSpcTaxon.SelectedItem as TaxonomicRank;

            menuItemSpcTaxon.Enabled =
                rankSpc == null;

            columnSpcDiversityA.Visible =
                columnSpcDiversityB.Visible =
                rankSpc != null;

            if (rankSpc != null)
            {
                spreadSheetSpc.ClearInsertedColumns();
            }

            columnSpcSpc.HeaderText = rankSpc == null ? Wild.Resources.Reports.Caption.Species : rankSpc.ToString();

            loadSpc();
        }

        private void spreadSheetSpc_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void speciesValidator_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            if (string.Equals(e.OriginalValue, e.SelectedTaxon.Name)) return;

            tdSpecies.Content = string.Format(
                Wild.Resources.Interface.Messages.SpeciesRename,
                e.OriginalValue, e.SelectedTaxon.Name);

            if (tdSpecies.ShowDialog() == tdbSpcRename)
            {
                // TODO: If already exist?

                Data.DefinitionRow spcRow = data.Definition.FindByName(e.OriginalValue);
                Data.DefinitionRow spcRow1 = data.Definition.FindByName(e.SelectedTaxon.Name);

                if (spcRow1 == null) // If there is no new species in index
                {
                    spcRow.Taxon = e.SelectedTaxon.Name;
                    spcRow.Rank = e.SelectedTaxon.Rank;

                    foreach (Data.LogRow logRow in spcRow.GetLogRows())
                    {
                        rememberChanged(logRow.CardRow);
                    }
                }
                else // If new species is already in index
                {
                    foreach (Data.LogRow logRow in spcRow.GetLogRows())
                    {
                        logRow.DefinitionRow = spcRow1;
                        rememberChanged(logRow.CardRow);
                    }

                    spcRow.Delete();
                }

                updateSpeciesArtifacts(e.Row);
            }
            else
            {
                e.GetCell().Value = e.OriginalValue;
            }
        }


        private void buttonSpcSave_Click(object sender, EventArgs e)
        {
            if (Species.UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                TaxonomicIndex speciesKey = new TaxonomicIndex();

                foreach (Data.DefinitionRow speciesRow in data.Definition)
                {
                    speciesKey.Taxon.AddTaxonRow(speciesKey.Taxon.NewTaxonRow(speciesRow.Rank, speciesRow.Taxon));
                }

                speciesKey.SaveToFile(Species.UserSettings.Interface.SaveDialog.FileName);
            }
        }

        #endregion

        #region Log

        private void contextLogOpen_Click(object sender, EventArgs e)
        {
            foreach (Data.LogRow logRow in getLogRows(spreadSheetLog.SelectedRows))
            {
                IO.RunFile(logRow.CardRow.Path,
                    new object[] { logRow.DefinitionRow.Taxon });
            }
        }

        private void logLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Data.LogRow[] logRows = (Data.LogRow[])e.Argument;

            if (rankLog == null)
            {
                processDisplay.SetProgressMaximum(logRows.Length);

                for (int i = 0; i < logRows.Length; i++)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetLog);
                    gridRow.Cells[columnLogID.Index].Value = logRows[i].ID;
                    updateLogRow(gridRow);
                    result.Add(gridRow);

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }
            else
            {
                int j = 0;
                processDisplay.SetProgressMaximum(FullStack.Count * SpeciesIndex.GetTaxonRows(rankLog).Length);
                foreach (Data.CardRow cardRow in FullStack)
                {
                    CardStack singleCardStack = new CardStack(new List<Data.CardRow>() { cardRow });

                    TaxonomicComposition composition = singleCardStack.GetCenosisComposition(rankLog);

                    for (int i = 0; i < composition.Count; i++)
                    {
                        (sender as BackgroundWorker).ReportProgress(j);
                        j++;

                        if (composition[i].Quantity == 0) continue;

                        DataGridViewRow gridRow = new DataGridViewRow();

                        gridRow.CreateCells(spreadSheetLog);

                        gridRow.Cells[columnLogSpc.Index].Value = composition[i].DataRow;
                        gridRow.Cells[columnLogID.Index].Value = cardRow.ID;
                        gridRow.Cells[columnLogQuantity.Index].Value = composition[i].Quantity;
                        gridRow.Cells[columnLogMass.Index].Value = composition[i].Mass;
                        gridRow.Cells[columnLogAbundance.Index].Value = composition[i].Abundance;
                        gridRow.Cells[columnLogBiomass.Index].Value = composition[i].Biomass;

                        setCardValue(cardRow, gridRow, spreadSheetLog.GetInsertedColumns());

                        result.Add(gridRow);
                    }

                    //foreach (SpeciesKey.TaxonRow taxonRow in baseLog.GetTaxonRows())
                    //{
                    //    DataGridViewRow gridRow = LogRow(cardRow, taxonRow);

                    //    result.Add(gridRow);

                    //    if (gridRow.Cells[columnLogQuantity.Index].Value == null ||
                    //        (int)gridRow.Cells[columnLogQuantity.Index].Value == 0)
                    //    {
                    //        spreadSheetLog.SetHidden(gridRow);
                    //    }
                    //}

                    //DataGridViewRow variaRow = LogRowVaria(data.Card[i]);
                    //result.Add(variaRow);
                    //if (variaRow.Cells[columnLogQuantity.Index].Value == null ||
                    //        (int)variaRow.Cells[columnLogQuantity.Index].Value == 0)
                    //{
                    //    spreadSheetLog.SetHidden(variaRow);
                    //}

                    //(sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }

            e.Result = result.ToArray();
        }

        private void logLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetLog.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetLog.StopProcessing();
            spreadSheetLog.UpdateStatus();

            tabPageLog.Parent = tabControl;
            tabControl.SelectedTab = tabPageLog;

            columnLogDiversityA.Visible = (rankSpc != null);
            columnLogDiversityB.Visible = (rankSpc != null);

            updateArtifacts();

            //spreadSheetLog.Rows.AddRange(e.Result as DataGridViewRow[]);
            ////spreadSheetLog.InitiateBackgroundTable();
            //IsBusy = false;
            //spreadSheetLog.StopProcessing();
        }



        private void logExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
            processDisplay.SetProgressMaximum(spreadSheetLog.Rows.Count);

            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                gridRow.DataGridView.Invoke(valueSetter, new object[] { 
                    rankLog == null ? findLogRow(gridRow).CardRow : data.Card.FindByID((int)gridRow.Cells[columnLogID.Index].Value), gridRow, spreadSheetLog.GetInsertedColumns()
                });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void comboBoxLogTaxon_SelectedIndexChanged(object sender, EventArgs e)
        {
            rankLog = comboBoxLogTaxon.SelectedItem as TaxonomicRank;
            menuItemSpcTaxon.Enabled = rankLog == null;
            loadLog();
        }

        private void speciesLog_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            tdLog.Content = string.Format(
                Wild.Resources.Interface.Messages.LogRename,
                e.OriginalValue, e.SelectedTaxon.Name);

            if (tdLog.ShowDialog() == tdbLogRename)
            {
                Data.DefinitionRow spcRow = data.Definition.FindByName(e.SelectedTaxon.Name);

                if (spcRow == null)
                {
                    spcRow = data.Definition.AddDefinitionRow(e.SelectedTaxon.Rank, e.SelectedTaxon.Name);
                }

                Data.LogRow logRow = findLogRow(e.Row);
                logRow.DefinitionRow = spcRow;

                rememberChanged(logRow.CardRow);
            }
            else
            {
                e.GetCell().Value = e.OriginalValue;
            }
        }

        private void buttonSelectLog_Click(object sender, EventArgs e)
        {
            if (loadCardAddt(spreadSheetLog))
            {
                IsBusy = true;
                spreadSheetLog.StartProcessing(Wild.Resources.Interface.Process.ExtLog);
                loaderLogExtended.RunWorkerAsync();
            }
        }

        #endregion       

        #region Individuals

        private void menuItemIndPrint_Click(object sender, EventArgs e)
        {
            getIndividuals(spreadSheetInd.Rows).GetReport().Run();
        }

        private void printIndividualsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getIndividuals(spreadSheetInd.SelectedRows).GetReport().Run();
        }

        private void RecoverMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WizardRecoverer recoverer = new WizardRecoverer(GetIndividuals(spreadSheetInd.Rows));
            WizardRecoverer recoverer = new WizardRecoverer(data);
            recoverer.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            recoverer.DataRecovered += recoverer_DataRecovered;
            recoverer.Show();
        }

        private void recoverer_DataRecovered(object sender, EventArgs e)
        {
            WizardRecoverer recoverer = (WizardRecoverer)sender;
            foreach (Data.IndividualRow indRow in recoverer.RecoveredIndividualRows)
            {                
                updateIndividualRow(columnIndID.GetRow(indRow.ID));
                rememberChanged(indRow.LogRow.CardRow);
            }
        }



        private void indLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Data.IndividualRow[] indRows = (Data.IndividualRow[])e.Argument;
            processDisplay.SetProgressMaximum(indRows.Length);

            for (int i = 0; i < indRows.Length; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetInd);
                gridRow.Cells[columnIndID.Index].Value = indRows[i].ID;
                updateIndividualRow(gridRow);

                setCardValue(indRows[i].LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns());
                result.Add(gridRow);
                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();

            //List<DataGridViewRow> result = new List<DataGridViewRow>();

            //if (e.Argument == null)
            //{
            //    for (int i = 0; i < data.Individual.Count; i++)
            //    {
            //        result.Add(GetLine(data.Individual[i]));
            //        (sender as BackgroundWorker).ReportProgress(i + 1);
            //    }
            //}
            //else
            //{
            //    Data.DefinitionRow speciesRow = (Data.DefinitionRow)e.Argument;
            //    Data.IndividualRow[] indRows = speciesRow.GetIndividualRows();

            //    for (int i = 0; i < indRows.Length; i++)
            //    {
            //        result.Add(GetLine(indRows[i]));
            //        (sender as BackgroundWorker).ReportProgress(i + 1);
            //    }
            //}

            //e.Result = result.ToArray();

        }

        private void indLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetInd.Rows.AddRange(e.Result as DataGridViewRow[]);
            spreadSheetInd.UpdateStatus();
            IsBusy = false;
            spreadSheetInd.StopProcessing();

            tabPageInd.Parent = tabControl;
            tabControl.SelectedTab = tabPageInd;

            applyBio();
            updateArtifacts();

            //tabPageInd.Parent = tabControl;
            //tabControl.SelectedTab = tabPageInd;
            //spreadSheetInd.Rows.AddRange(e.Result as DataGridViewRow[]);
            ////spreadSheetInd.SetColumnsVisibility(new DataGridViewColumn[] {
            ////    columnIndLength, columnIndSex, columnIndGrade, columnIndInstar }, true);

            //IsBusy = false;
            //spreadSheetInd.StopProcessing();
        }


        private void indExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            processDisplay.SetProgressMaximum(spreadSheetInd.Rows.Count);
            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { findIndividualRow(gridRow).LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns() });
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
                saveIndividualRow(spreadSheetInd.Rows[e.RowIndex]);
            }
        }

        private void speciesInd_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            tdInd.Content = string.Format(
                Wild.Resources.Interface.Messages.IndRename,
                e.OriginalValue, e.SelectedTaxon.Name);

            if (tdInd.ShowDialog() == tdbIndRename)
            {
                Data.IndividualRow individualRow = findIndividualRow(e.Row);

                Data.DefinitionRow spcRow = data.Definition.FindByName(e.SelectedTaxon.Name);

                if (spcRow == null)
                {
                    spcRow = data.Definition.AddDefinitionRow(e.SelectedTaxon.Rank, e.SelectedTaxon.Name);
                }

                if (individualRow.LogRow.GetIndividualRows().Length == 1)
                {
                    // If individual is single - just replace log.species
                    individualRow.LogRow.DefinitionRow = spcRow;
                }
                else
                {
                    // If there are more individual(-s) - 
                    // create new log                   
                    Data.LogRow logRow = data.Log.FindByCardIDDefID(individualRow.LogRow.CardRow.ID, spcRow.ID);

                    if (logRow == null)
                    {
                        logRow = data.Log.NewLogRow();
                        logRow.DefinitionRow = spcRow;
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

                rememberChanged(individualRow.LogRow.CardRow);
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

        private void contextIndCard_Click(object sender, EventArgs e)
        {
            if (spreadSheetCard.RowCount > 0)
            {
                selectCorrespondingCardRows();
                tabControl.SelectedTab = tabPageCard;
            }
            else
            {
                loaderCard.RunWorkerCompleted += selectCorrespondingCardRows;
                menuItemCards_Click(sender, e);
            }
        }

        private void selectCorrespondingCardRows()
        {
            spreadSheetCard.ClearSelection();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                if (!gridRow.Visible) continue;

                Data.IndividualRow individualRow = findIndividualRow(gridRow);
                DataGridViewRow corrRow = columnCardID.GetRow(individualRow.LogRow.CardID);

                if (corrRow == null) continue;
                corrRow.Selected = true;
                spreadSheetCard.FirstDisplayedScrollingRowIndex = corrRow.Index;
            }
        }

        private void selectCorrespondingCardRows(object sender, RunWorkerCompletedEventArgs e)
        {
            selectCorrespondingCardRows();
            loaderLog.RunWorkerCompleted -= selectCorrespondingLogRows;
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
                menuItemLog_Click(sender, e);
            }
        }

        private void selectCorrespondingLogRows()
        {
            spreadSheetLog.ClearSelection();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                if (!gridRow.Visible) continue;

                Data.IndividualRow individualRow = findIndividualRow(gridRow);
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
                Data.IndividualRow individualRow = findIndividualRow(gridRow);

                Mayfly.IO.RunFile(individualRow.LogRow.CardRow.Path,
                    individualRow.LogRow.DefinitionRow.Taxon);

                // TODO: select row in a log
            }
        }

        private void contextIndDelete_Click(object sender, EventArgs e)
        {
            while (spreadSheetInd.SelectedRows.Count > 0)
            {
                Data.IndividualRow individualRow = findIndividualRow(spreadSheetInd.SelectedRows[0]);
                rememberChanged(individualRow.LogRow.CardRow);
                data.Individual.Rows.Remove(individualRow);
                spreadSheetInd.Rows.Remove(spreadSheetInd.SelectedRows[0]);
            }
        }

        private void spreadSheetInd_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Data.IndividualRow individualRow = findIndividualRow(e.Row);
            rememberChanged(individualRow.LogRow.CardRow);
            data.Individual.Rows.Remove(individualRow);
        }

        private void buttonSelectInd_Click(object sender, EventArgs e)
        {
            if (loadCardAddt(spreadSheetInd))
            {
                IsBusy = true;
                spreadSheetInd.StartProcessing(Wild.Resources.Interface.Process.ExtInd);
                loaderIndExtended.RunWorkerAsync();
            }
        }

        #endregion

        private void menuItemCompareComp_Click(object sender, EventArgs e)
        {
            SelectionValue selectionValue = new SelectionValue(spreadSheetCard);

            if (selectionValue.ShowDialog(this) != DialogResult.OK) return;

            IsBusy = true;
            spreadSheetLog.StartProcessing(Wild.Resources.Interface.Process.LoadLog);

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
            {
                string field = gridColumn.Name.TrimStart("columnCard".ToCharArray());

                BackgroundWorker comparerLog = new BackgroundWorker();
                comparerLog.DoWork += comparerLog_DoWork;
                comparerLog.RunWorkerCompleted += comparerLog_RunWorkerCompleted;
                comparerLog.RunWorkerAsync(field);
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

                if (rankLog == null)
                {
                    composition = stack.GetCenosisComposition();
                }
                else
                {
                    composition = stack.GetCenosisComposition(rankLog);
                }

                composition.Name = variant;
                result.Add(composition);
            }

            e.Result = result;
        }

        private void comparerLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Composition composition;

            if (rankLog == null)
            {
                composition = FullStack.GetCenosisComposition();
            }
            else
            {
                composition = FullStack.GetCenosisComposition(rankLog);
            }

            CompositionComparison comcom = new CompositionComparison(
                composition, (List<Composition>)e.Result);

            //comcom.UpdateCompositionAppearance();
            comcom.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            comcom.Show();

            IsBusy = false;
            spreadSheetLog.StopProcessing();
        }
    }
}