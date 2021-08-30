using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Mathematics.Charts;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using Mayfly.Wild.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            UI.SetMenuAvailability(Licensing.AllowedFeaturesLevel >= FeatureLevel.Advanced,

                // Bio import/export
                //menuItemExportSpec,
                menuItemImportSpec,
                //toolStripSeparator11,

                // All stats
                menuItemGearStats,
                menuItemSpcStats,

                // Assessment
                menuInstant

                // From cards to gear stats
                //menuItemCardCatches,
                //menuItemCardEfforts,
                //toolStripSeparator2,

                //// From species to species stats
                //menuItemSpcTotals,
                //menuItemSpcStrates,
                //toolStripSeparator13
                );

            UI.SetMenuAvailability(Licensing.AllowedFeaturesLevel >= FeatureLevel.Insider,
                // Fast input
                menuSurvey,
                // Models
                menuModels,
                // Cohort analysis
                menuCohort);

            //UI.SetControlsAvailability(Licensing.AllowedFeaturesLevel >= FeatureLevel.Advanced,
            //    buttonCardDetails,
            //    buttonSpcDetails);


            Fish.UserSettings.Interface.OpenDialog.Multiselect = true;

            listViewWaters.Shine();
            listViewSamplers.Shine();
            listViewInvestigators.Shine();
            listViewDates.Shine();

            spreadSheetCard.UpdateStatus();
            spreadSheetSpc.UpdateStatus();
            spreadSheetLog.UpdateStatus();
            spreadSheetInd.UpdateStatus();
            spreadSheetStratified.UpdateStatus();

            tabPageGearStats.Parent = null;

            //tabPageCardSchedule.Parent = null;

            columnCardWater.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardWhere.ValueType = typeof(Waypoint);
            ColumnCardWeather.ValueType = typeof(WeatherState);
            ColumnCardTempSurface.ValueType = typeof(double);
            columnCardGear.ValueType = typeof(string);
            columnCardMesh.ValueType = typeof(int);
            columnCardHook.ValueType = typeof(double);
            columnCardLength.ValueType = typeof(double);
            columnCardOpening.ValueType = typeof(double);
            columnCardHeight.ValueType = typeof(double);
            columnCardSquare.ValueType = typeof(double);
            columnCardSpan.ValueType = typeof(TimeSpan);
            columnCardVelocity.ValueType = typeof(double);
            columnCardExposure.ValueType = typeof(double);
            columnCardEffort.ValueType = typeof(double);
            columnCardDepth.ValueType = typeof(double);
            columnCardWealth.ValueType = typeof(double);
            columnCardQuantity.ValueType = typeof(double);
            columnCardMass.ValueType = typeof(double);
            columnCardAbundance.ValueType = typeof(double);
            columnCardBiomass.ValueType = typeof(double);
            columnCardDiversityA.ValueType = typeof(double);
            columnCardDiversityB.ValueType = typeof(double);
            columnCardComments.ValueType = typeof(string);

            tabPageCard.Parent = null;
            tabPageGearStats.Parent = null;

            //columnSpcSpc.ValueType = typeof(string);
            //columnSpcValid.ValueType = typeof(string);
            columnSpcQuantity.ValueType = typeof(double);
            columnSpcMass.ValueType = typeof(double);
            columnSpcOccurrence.ValueType = typeof(double);

            loadTaxaList();

            tabPageSpc.Parent = null;
            tabPageSpcStats.Parent = null;

            //columnLogSpc.ValueType = typeof(string);
            columnLogQuantity.ValueType = typeof(int);
            columnLogMass.ValueType = typeof(double);
            columnLogAbundance.ValueType = typeof(double);
            columnLogBiomass.ValueType = typeof(double);
            columnLogDiversityA.ValueType = typeof(double);
            columnLogDiversityB.ValueType = typeof(double);

            tabPageLog.Parent = null;

            //columnIndSpecies.ValueType = typeof(string);
            columnIndLength.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndSomaticMass.ValueType = typeof(double);
            columnIndCondition.ValueType = typeof(double);
            columnIndConditionSoma.ValueType = typeof(double);
            columnIndTally.ValueType = typeof(string);
            columnIndAge.ValueType = typeof(Age);
            columnIndGeneration.ValueType = typeof(int);
            columnIndSex.ValueType = typeof(Sex);
            columnIndMaturity.ValueType = typeof(Maturity);

            columnIndGonadMass.ValueType = typeof(double);
            columnIndGonadSampleMass.ValueType = typeof(double);
            columnIndGonadSample.ValueType = typeof(int);
            columnIndEggSize.ValueType = typeof(double);
            columnIndFecundityAbs.ValueType = typeof(double);
            columnIndFecundityRelative.ValueType = typeof(double);
            columnIndFecundityRelativeSoma.ValueType = typeof(double);
            columnIndGonadIndex.ValueType = typeof(double);
            columnIndGonadIndexSoma.ValueType = typeof(double);

            columnIndFat.ValueType = typeof(int);
            columnIndConsumed.ValueType = typeof(double);
            columnIndConsumptionIndex.ValueType = typeof(double);
            columnIndDietItems.ValueType = typeof(int);

            columnIndComments.ValueType = typeof(string);

            tabPageInd.Parent = null;

            //columnStratifiedSpc.ValueType = typeof(string);
            columnStratifiedCount.ValueType = typeof(int);
            columnSpcMass.ValueType = typeof(double);
            columnStratifiedFrom.ValueType = typeof(double);
            columnStratifiedTo.ValueType = typeof(double);

            foreach (SpreadSheet spreadSheet in new SpreadSheet[] { spreadSheetInd, spreadSheetStratified })
            {
                foreach (DataGridViewColumn gridColumn in new DataGridViewColumn[] { columnCardGear, columnCardMesh })
                {
                    spreadSheet.InsertColumn(gridColumn, spreadSheet.InsertedColumnCount, gridColumn.Name.TrimStart("columnCard".ToCharArray()));
                }
            }

            tabPageStratified.Parent = null;

            chartSchedule.Format();

            plotQualify.SetBioAcceptable(LoadCards);
            spreadSheetInd.SetBioAcceptable(LoadCards);
            spreadSheetSpc.SetBioAcceptable(LoadCards);
            spreadSheetLog.SetBioAcceptable(LoadCards);

            data = new Data(Fish.UserSettings.SpeciesIndex, Fish.UserSettings.SamplersIndex);
            FullStack = new CardStack(); //.ConvertFrom(data);
            AllowedStack = new CardStack();

            //// Will do nothing
            //if (Licensing.Verify("Fishery Scientist"))
            //{
            //    data.InitializeBio();

            //    foreach (ContinuousBio bio in data.MassModels)
            //    {
            //        bio.VisualConfirmation = UserSettings.VisualConfirmation; 
            //    }
            //    foreach (ContinuousBio bio in data.GrowthModels)
            //    {
            //        bio.VisualConfirmation = UserSettings.VisualConfirmation; 
            //    }

            //    if (UserSettings.AutoLoadBio)
            //    {
            //        processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.SpecLoading);
            //        LoadCards(UserSettings.Bios);
            //    }
            //}

            IsEmpty = true;
            IsAllowedEmpty = true;
        }

        public MainForm(string[] args)
            : this()
        {
            if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-obs":
                        menuItemSurveyInput_Click(this, new EventArgs());
                        this.WindowState = FormWindowState.Minimized;
                        break;

                    default:
                        this.Load += (o, e) => { LoadCards(args.GetOperableFilenames(Fish.UserSettings.Interface.Extension)); };
                        break;
                }
            }
            else
            {
                this.Load += (o, e) => { LoadCards(args.GetOperableFilenames(Fish.UserSettings.Interface.Extension)); };
            }
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
            menuSpc.Visible = (tabControl.SelectedTab == tabPageSpc);
            menuIndividuals.Visible = (tabControl.SelectedTab == tabPageInd);
            stratifiedSamplesToolStripMenuItem.Visible = (tabControl.SelectedTab == tabPageStratified);
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

                    processDisplay.ResetStatus();
                }
                //else if (Path.GetExtension(filenames[i]) == Wild.UserSettings.InterfacePermission.Extension)
                //{
                //    Permissions perm = new Permissions(filenames[i]);
                //    perm.SetFriendlyDesktopLocation(this, FormLocation.Centered);
                //    perm.ShowDialog(this);
                //}
                else if (Path.GetExtension(filenames[i]) == Fish.UserSettings.Interface.Extension)
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
            //if (Licensing.AllowedFeaturesLevel < FeatureLevel.Advanced)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            data.RefreshBios();
        }

        private void modelCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.ArtifactsProcessing);
                artefactFinder.RunWorkerAsync();
            }
            else
            {
                processDisplay.StopProcessing();
                IsBusy = false;
            }

            updateMass(FullStack.Mass());
        }



        private void artefactFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (Licensing.AllowedFeaturesLevel < FeatureLevel.Advanced)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            e.Result = AllowedStack.CheckConsistency();
        }

        private void artefactFinder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            }

            updateArtifacts();

            IsBusy = false;
            processDisplay.StopProcessing();
        }



        private void dataSaver_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;

            while (changedCards.Count > 0)
            {
                Data.CardRow cardRow = changedCards[0];

                index++;
                dataSaver.ReportProgress(index);

                if (cardRow.Path != null)
                {
                    Data _data = cardRow.SingleCardDataset();
                    //if (_data != null) // If data is null AND its file contain some info?
                    //{ 
                    _data.WriteToFile(cardRow.Path);
                    //}
                }

                changedCards.RemoveAt(0);
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

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();
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
                File.WriteAllLines(UserSettings.Interface.SaveDialog.FileName, FullStack.GetFilenames());
            }
        }

        private void menuItemExportBio_Click(object sender, EventArgs e)
        {
            WizardExportBio exportWizard = new WizardExportBio(data);
            exportWizard.Show();
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

        #region Sample

        private void menuItemSample_DropDownOpening(object sender, EventArgs e)
        {
            menuItemStratified.Enabled = data.Stratified.Count > 0;
        }

        private void menuItemGearStats_Click(object sender, EventArgs e)
        {
            tabPageGearStats.Parent = tabControl;
            tabControl.SelectedTab = tabPageGearStats;

            spreadSheetGearStats.Rows.Clear();
            foreach (FishSamplerTypeDisplay samplerType in AllowedStack.GetSamplerTypeDisplays()) {
                spreadSheetGearStats.Rows.Add(samplerType);
            }
            spreadSheetGearStats.Rows.Add(Mayfly.Resources.Interface.Total);

            dateTimePickerCatches.MinDate =
                dateTimePickerEffort.MinDate =
                AllowedStack.EarliestEvent;

            dateTimePickerCatches.MaxDate =
                dateTimePickerEffort.MaxDate =
                AllowedStack.LatestEvent;

            spreadSheetCatches.Rows.Clear();
            spreadSheetEfforts.Rows.Clear();
        }

        private void menuItemSpcStats_Click(object sender, EventArgs e)
        {
            tabPageSpcStats.Parent = tabControl;
            tabControl.SelectedTab = tabPageSpcStats;

            initializeSpeciesStatsPlot();

            spreadSheetSpcStats.Rows.Clear();
            foreach (Data.SpeciesRow speciesRow in AllowedStack.GetSpecies())
            {
                spreadSheetSpcStats.Rows.Add(speciesRow.ID, speciesRow);
            }
            spreadSheetSpcStats.Rows.Add(Mayfly.Resources.Interface.Total);

            comboBoxTechSampler.DataSource = AllowedStack.GetSamplerTypeDisplays();
            comboBoxTechSampler.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxTechSampler.DataSource)[0];

            comboBoxQualType.DataSource = AllowedStack.GetSamplerTypeDisplays();
            comboBoxQualType.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxQualType.DataSource)[0];

            clearSpcStats();
            species_Changed(spreadSheetSpcStats, e);
        }

        private void menuItemCards_Click(object sender, EventArgs e)
        {
            loadCards();
        }

        private void menuItemSpc_Click(object sender, EventArgs e)
        {
            loadSpc();
        }

        private void menuItemLog_Click(object sender, EventArgs e)
        {
            loadLog();
        }

        private void menuItemIndAllAll_Click(object sender, EventArgs e)
        {
            loadIndividuals();
        }

        private void menuItemIndSuggestedAll_Click(object sender, EventArgs e)
        {
            List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

            foreach (Data.SpeciesRow spcRow in FullStack.GetSpecies())
            {
                TreatmentSuggestion sugg = FullStack.GetTreatmentSuggestion(spcRow, data.Individual.AgeColumn);
                if (sugg != null) indRows.AddRange(sugg.GetSuggested());
            }
            
            loadIndividuals(indRows.ToArray());
        }

        private void menuItemLoadStratified_Click(object sender, EventArgs e)
        {
            loadStratifiedSamples();
        }

        #endregion

        #region Survey

        private void menuItemSurveyInput_Click(object sender, EventArgs e)
        {
            WizardOb wizOb = new WizardOb
            {
                Explorer = this
            };
            wizOb.Survey.Anamnesis = this.data;
            wizOb.Show();
        }

        private void menuItemSpawning_Click(object sender, EventArgs e)
        {
            WizardSpawning spawnWizard = new WizardSpawning(AllowedStack);
            spawnWizard.Show();
        }

        #endregion

        #region Fishery

        private void menuItemCenosis_Click(object sender, EventArgs e)
        {
            WizardCenosis wizard = new WizardCenosis(AllowedStack);
            wizard.Show();
        }

        private void speciesComposition_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardPopulation wizard = new WizardPopulation(AllowedStack, speciesRow);
            wizard.Show();
        }




        private void speciesGrowthCohorts_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardGrowthCohorts wizard = new WizardGrowthCohorts(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesMortalityCohorts_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardMortalityCohorts wizard = new WizardMortalityCohorts(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void menuItemExtrapolations_Click(object sender, EventArgs e)
        {
            WizardExtrapolations wizard = new WizardExtrapolations(AllowedStack);
            wizard.Show();
        }

        private void speciesStockExtrapolation_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardExtrapolation wizard = new WizardExtrapolation(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesStockVpa_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardVirtualPopulation wizard = new WizardVirtualPopulation(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesMSYR_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardMSYR wizard = new WizardMSYR(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void meniItemMSYUnspecified_Click(object sender, EventArgs e)
        {
            WizardMSY wizard = new WizardMSY();
            wizard.Show();
        }

        private void speciesMSY_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardMSY wizard = new WizardMSY(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void meniItemPredictionUnspecified_Click(object sender, EventArgs e)
        {
            WizardPrediction wizard = new WizardPrediction();
            wizard.Show();
        }

        private void speciesPrediction_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardPrediction wizard = new WizardPrediction(AllowedStack, speciesRow);
            wizard.Show();
        }



        private void menuItemStockCumulative_Click(object sender, EventArgs e)
        {
            //WizardStock stockExplorer = new WizardStock((ToolStripMenuItem)sender, Data, StockExplorerVariant.CumulativeStock);
            //stockExplorer.Show();
        }

        private void menuItemTac_Click(object sender, EventArgs e)
        {
            WizardTac tacWizard = new WizardTac(AllowedStack);
            tacWizard.Show();
        }

        #endregion

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
            about.SetPowered(Fish.Properties.Resources.sriif, Wild.Resources.Interface.Powered.SRIIF);
            about.ShowDialog();
        }

        #endregion

        #endregion



        #region Main page

        private void cards_DragDrop(object sender, DragEventArgs e)
        {
            cards_DragLeave(sender, e);
            LoadCards(e.GetOperableFilenames(Fish.UserSettings.Interface.Extension));
        }

        private void cards_DragEnter(object sender, DragEventArgs e)
        {
            if (e.GetOperableFilenames(Fish.UserSettings.Interface.Extension).Length > 0)
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


        private void labelRateValue_SizeChanged(object sender, EventArgs e)
        {
            //labelRateValue.CenterWith(chartRate);

            //labelRate.Left =
            //    labelRateValue.Left + 10;
        }

        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            loadCards(AllowedStack.GetStack(data.Water.FindByID(listViewWaters.FocusedItem.GetID())));
        }

        private void listViewSamplers_ItemActivate(object sender, EventArgs e)
        {
            loadCards(AllowedStack.GetStack("SamplerID", listViewSamplers.FocusedItem.GetID()));
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e)
        {
            loadCards(AllowedStack.GetStack("Investigator", listViewInvestigators.FocusedItem.Text));
        }

        private void listViewDates_ItemActivate(object sender, EventArgs e)
        {
            DateTime[] dt = (DateTime[])listViewDates.FocusedItem.Tag;
            spreadSheetCard.EnsureFilter(columnCardWhen, dt[0].ToOADate(), dt[dt.Length - 1].ToOADate() + 1, loaderCard, menuItemCards_Click);
        }

        #endregion    




        #region Gear stats

        private void gear_Changed(object sender, EventArgs e)
        {
            if (spreadSheetGearStats.SelectedRows.Count == 0 ||
                !(spreadSheetGearStats[ColumnGearStats.Index, spreadSheetGearStats.SelectedRows[0].Index].Value is FishSamplerTypeDisplay display))
            {
                SelectedSamplerType = FishSamplerType.None;
            }
            else
            {
                SelectedSamplerType = display.Type;
            }

            comboBoxCatchesMesh.DataSource = AllowedStack.Classes(SelectedSamplerType);
            checkBoxCatchesMesh.Enabled = SelectedSamplerType.IsMesh(); // != FishingGearType.None;

            catches_Changed(sender, e);
            effort_Changed(sender, e);
            schedule_Changed(sender, e);
        }

        #region Efforts

        private void checkBoxSamplerStatDaily_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEffort.Enabled = checkBoxEffortDaily.Checked;
            effort_Changed(sender, e);
        }

        private void effort_Changed(object sender, EventArgs e)
        {
            spreadSheetEfforts.Rows.Clear();

            if (SelectedSamplerType == FishSamplerType.None)
            {
                comboBoxEffortUE.Enabled = false;
            }
            else
            {
                comboBoxEffortUE.Enabled = true;
                UnitEffort.SwitchUE(comboBoxEffortUE, SelectedSamplerType);
            }
        }

        private void comboBoxStatEffortEU_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedEffortUE = (UnitEffort)comboBoxEffortUE.SelectedItem;
            columnEffort.ResetFormatted(SelectedEffortUE.Unit);

            if (SelectedSamplerType == FishSamplerType.None) return;

            EffortsData = FullStack.GetStack(SelectedSamplerType);

            if (checkBoxEffortDaily.Checked)
            {
                EffortsData = EffortsData.GetStack(dateTimePickerEffort.Value);
            }

            spreadSheetEfforts.Rows.Clear();

            BackgroundWorker effortCalc = new BackgroundWorker();
            effortCalc.DoWork += effortCalc_DoWork;
            effortCalc.RunWorkerCompleted += effortCalc_RunWorkerCompleted;
            effortCalc.RunWorkerAsync();
        }

        private void effortCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            string[] meshValues = AllowedStack.Classes(SelectedSamplerType);

            if (meshValues.Length == 0)
            {
                DataGridViewRow singleRow = new DataGridViewRow();
                singleRow.CreateCells(spreadSheetEfforts);

                double reps = EffortsData.Count;
                double effort = EffortsData.GetEffort(SelectedSamplerType, SelectedEffortUE.Variant);

                singleRow.Cells[columnEffortMesh.Index].Value = Mayfly.Resources.Interface.Total;
                singleRow.Cells[columnEffortReps.Index].Value = reps;
                singleRow.Cells[columnEffort.Index].Value = effort;
                singleRow.Cells[columnEffortEO.Index].Value = effort / reps;
                result.Add(singleRow);
            }
            else
            {
                double repsSum = 0;
                double effortSum = 0;

                foreach (string meshValue in meshValues)
                {
                    CardStack meshData = EffortsData.GetStack(SelectedSamplerType, meshValue);

                    if (meshData.Count > 0)
                    {
                        DataGridViewRow gridRow = new DataGridViewRow();
                        gridRow.CreateCells(spreadSheetEfforts);
                        gridRow.Cells[columnEffortMesh.Index].Value = meshValue;
                        gridRow.Cells[columnEffortReps.Index].Value = meshData.Count;
                        double effort = meshData.GetEffort(SelectedSamplerType, SelectedEffortUE.Variant);
                        gridRow.Cells[columnEffort.Index].Value = effort;
                        if (effort > 0)
                        {
                            gridRow.Cells[columnEffortEO.Index].Value = effort / meshData.Count;
                        }
                        result.Add(gridRow);

                        effortSum += effort;
                        repsSum += meshData.Count;
                    }
                }

                if (repsSum > 0)
                {
                    DataGridViewRow totalRow = new DataGridViewRow();
                    totalRow.CreateCells(spreadSheetEfforts);
                    totalRow.Cells[columnEffortMesh.Index].Value = Mayfly.Resources.Interface.Total;
                    totalRow.Cells[columnEffortReps.Index].Value = repsSum;
                    totalRow.Cells[columnEffort.Index].Value = effortSum;
                    totalRow.Cells[columnEffortEO.Index].Value = effortSum / repsSum;
                    result.Add(totalRow);
                }
            }

            e.Result = result.ToArray();
        }

        private void effortCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetEfforts.Rows.Clear();

            foreach (DataGridViewRow gridRow in (DataGridViewRow[])e.Result)
            {
                spreadSheetEfforts.Rows.Add(gridRow);
            }

            labelEffortsNoData.Visible = spreadSheetEfforts.RowCount == 0;
        }

        //private void buttonEffortsPrint_Click(object sender, EventArgs e)
        //{
        //    processDisplay.StartProcessing(100, Resources.Process.EffortCalculation);
        //    IsBusy = true;
        //    reporterEffort.RunWorkerAsync();
        //}

        //private void reporterEffort_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Report report = new Report(Resources.Reports.Reports.Efforts);

        //    report.End();
        //    e.Result = report;
        //}

        #endregion

        #region Schedule

        private void schedule_Changed(object sender, EventArgs e)
        {
            //tabPageCardSchedule.Parent = SelectedSamplerType.IsPassive() ? tabControlCard : null;
            chartSchedule.Series.Clear();
            chartSchedule.ChartAreas[0].AxisY.Minimum = ((FullStack.EarliestEvent - TimeSpan.FromHours(1)).Date).ToOADate();
            chartSchedule.ChartAreas[0].AxisY.Maximum = (FullStack.LatestEvent + TimeSpan.FromDays(1)).ToOADate();

            chartSchedule_AxisViewChanged(sender, e: new ViewEventArgs(chartSchedule.ChartAreas[0].AxisY, chartSchedule.ChartAreas[0].AxisY.Minimum));

            if (SelectedSamplerType == FishSamplerType.None)
            {
                foreach (FishSamplerType samplerType in FullStack.GetSamplerTypes())
                {
                    chartSchedule.Series.Add(GetSinkingSchedule(samplerType));
                }
            }
            else
            {
                chartSchedule.Series.Add(GetSinkingSchedule(SelectedSamplerType));
            }

            chartSchedule.ApplyPaletteColors();

            foreach (var series in chartSchedule.Series)
            {
                foreach (var point in series.Points)
                {
                    point.Color = Color.FromArgb(128, point.Color);
                }
            }
        }

        private void chartSchedule_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HitTestResult htResult = chartSchedule.HitTest(e.X, e.Y);

            switch (htResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    if (htResult.Series.Points[htResult.PointIndex].Tag is Data.CardRow cardRow)
                    {
                        IO.RunFile(cardRow.Path);
                        //string cardName = Data.GetStack().GetInfoRow(cardRow).FileName;
                        //Fish.Service.RunCard(cardName);
                        //spreadSheetCard.EnsureFilter(columnCardID, cardRow.ID, loaderCard, menuItemLoadCards_Click);
                    }
                    break;
            }
        }

        private void chartSchedule_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (e.Axis == chartSchedule.ChartAreas[0].AxisY)
            {
                double range = double.IsNaN(e.NewSize) ?
                    chartSchedule.ChartAreas[0].AxisY.Maximum - chartSchedule.ChartAreas[0].AxisY.Minimum :
                    e.NewSize;

                chartSchedule.ChartAreas[0].AxisY.IntervalType =
                    chartSchedule.ChartAreas[0].AxisY.LabelStyle.IntervalType =
                    chartSchedule.ChartAreas[0].AxisY.MajorGrid.IntervalType =
                    Mayfly.Service.GetAutoIntervalType(range);
                chartSchedule.ChartAreas[0].AxisY.LabelStyle.Format = Mayfly.Service.GetAutoFormat(chartSchedule.ChartAreas[0].AxisY.IntervalType);
                //chartSchedule.ChartAreas[0].AxisY.AdjustIntervals(chartSchedule.Width, Mayfly.Service.GetAutoInterval(range));
            }
        }

        #endregion

        #region Catches

        private void catches_Changed(object sender, EventArgs e)
        {
            comboBoxCatchesMesh.Enabled = checkBoxCatchesMesh.Checked;
            dateTimePickerCatches.Enabled = checkBoxCatchesDaily.Checked;

            if (SelectedSamplerType == FishSamplerType.None)
            {
                checkBoxCatchesMesh.Checked = false;

                if (checkBoxCatchesDaily.Checked)
                {
                    CatchesData = AllowedStack.GetStack(dateTimePickerCatches.Value);
                }
                else
                {
                    CatchesData = AllowedStack;
                }
            }
            else
            {
                if (checkBoxCatchesDaily.Checked)
                {
                    if (checkBoxCatchesMesh.Checked)
                    {
                        CatchesData = AllowedStack.GetStack(SelectedSamplerType, comboBoxCatchesMesh.SelectedValue.ToString())
                            .GetStack(dateTimePickerCatches.Value);
                    }
                    else
                    {
                        CatchesData = AllowedStack.GetStack(SelectedSamplerType)
                            .GetStack(dateTimePickerCatches.Value);
                    }
                }
                else
                {
                    if (checkBoxCatchesMesh.Checked)
                    {
                        CatchesData = AllowedStack.GetStack(SelectedSamplerType, comboBoxCatchesMesh.SelectedValue.ToString());
                    }
                    else
                    {
                        CatchesData = AllowedStack.GetStack(SelectedSamplerType);
                    }
                }
            }

            spreadSheetCatches.Rows.Clear();

            BackgroundWorker catchesCalc = new BackgroundWorker();
            catchesCalc.DoWork += catchesCalc_DoWork;
            catchesCalc.RunWorkerCompleted += catchesCalc_RunWorkerCompleted;
            catchesCalc.RunWorkerAsync();
        }

        private void catchesCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            double Q = 0D;

            foreach (Data.SpeciesRow speciesRow in CatchesData.GetSpeciesCaught())
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                double q = CatchesData.Quantity(speciesRow);

                if (q > 0)
                {
                    Q += q;

                    double w = CatchesData.Mass(speciesRow);

                    gridRow.CreateCells(spreadSheetCatches);
                    gridRow.Cells[columnCatchSpc.Index].Value = speciesRow;
                    gridRow.Cells[columnCatchN.Index].Value = q;
                    gridRow.Cells[columnCatchW.Index].Value = w;

                    result.Add(gridRow);
                }
            }

            if (Q > 0)
            {
                DataGridViewRow totalGridRow = new DataGridViewRow();
                totalGridRow.CreateCells(spreadSheetCatches);
                totalGridRow.Cells[columnCatchSpc.Index].Value = Mayfly.Resources.Interface.Total;

                result.Add(totalGridRow);
            }

            e.Result = result.ToArray();
        }

        private void catchesCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetCatches.Rows.Clear();

            foreach (DataGridViewRow gridRow in (DataGridViewRow[])e.Result)
            {
                spreadSheetCatches.Rows.Add(gridRow);
            }

            labelCatchesNoData.Visible = spreadSheetCatches.RowCount == 0;
            spreadSheetCatches.Sort(columnCatchN, ListSortDirection.Descending);


            // Recalculating catch totals

            double q = 0.0;
            double w = 0.0;

            for (int i = 0; i < spreadSheetCatches.Rows.Count; i++)
            {
                if (spreadSheetCatches[columnCatchSpc.Index, i].Value.ToString() == Mayfly.Resources.Interface.Total)
                {
                    continue;
                }

                if (!spreadSheetCatches.IsHidden(i))
                {
                    if (spreadSheetCatches[columnCatchN.Index, i].Value != null)
                    {
                        q += (double)spreadSheetCatches[columnCatchN.Index, i].Value;
                    }

                    if (spreadSheetCatches[columnCatchW.Index, i].Value != null)
                    {
                        w += (double)spreadSheetCatches[columnCatchW.Index, i].Value;
                    }
                }
            }

            for (int i = 0; i < spreadSheetCatches.Rows.Count; i++)
            {
                if (spreadSheetCatches[columnCatchSpc.Index, i].Value.ToString() == Mayfly.Resources.Interface.Total)
                {
                    spreadSheetCatches[columnCatchN.Index, i].Value = q;
                    spreadSheetCatches[columnCatchW.Index, i].Value = w;
                }
            }
        }

        //private void buttonCatchesPrint_Click(object sender, EventArgs e)
        //{
        //    processDisplay.StartProcessing(100, Resources.Process.CatchesProcessing);
        //    IsBusy = true;
        //    reporterCatch.RunWorkerAsync();
        //}

        //private void reporterCatch_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    //CardStack data = SelectedCatchesData(CatchesData);
        //    Report report = new Report(Resources.Reports.Reports.Catches);
        //    CatchesData.AddCommon(report);
        //    CatchesData.AddCatches(report);
        //    report.End();

        //    e.Result = report;
        //}

        #endregion

        private void buttonGearStatsReport_Click(object sender, EventArgs e)
        {
            if (SelectedSamplerType == FishSamplerType.None)
            {
                Report report = new Report(Resources.Reports.Header.StatsGear);
                AllowedStack.AddCommon(report);
                AllowedStack.Sort();
                AllowedStack.AddGearStatsReport(report);
                report.EndBranded();
                report.Run();
            }
            else
            {
                Report report = new Report(string.Format(Resources.Reports.Header.StatsGearType, SelectedSamplerType.ToDisplay()));
                CardStack gearStack = AllowedStack.GetStack(SelectedSamplerType);
                gearStack.AddCommon(report);
                gearStack.Sort();
                gearStack.AddGearStatsReport(report, SelectedSamplerType, SelectedEffortUE);
                report.EndBranded();
                report.Run();
            }
        }

        #endregion




        #region Species stats

        private void species_Changed(object sender, EventArgs e)
        {
            plotQualify.plotCounter = 0;

            // Make a selection
            if (spreadSheetSpcStats.SelectedRows.Count == 0 ||
                !(spreadSheetSpcStats[ColumnSpcStat.Index, spreadSheetSpcStats.SelectedRows[0].Index].Value is Data.SpeciesRow row))
            {
                selectedStatSpc = null;
            }
            else
            {
                selectedStatSpc = row;
                plotQualify.ResetFormatted(selectedStatSpc.KeyRecord.ShortName);

                resetQualPlotAxes(
                    Service.GetStrate(AllowedStack.LengthMin(selectedStatSpc)).LeftEndpoint,
                    Service.GetStrate(AllowedStack.LengthMax(selectedStatSpc)).RightEndpoint,
                    AllowedStack.GetLengthComposition(selectedStatSpc, UserSettings.SizeInterval).MostSampled.Quantity);
            }

            // Totals
            totals_Changed(sender, e);

            // Techniques
            tech_Changed(sender, e);

            // Strates
            labelQualNotSelected.Visible = selectedStatSpc == null;

            UI.SetControlClickability(selectedStatSpc != null,
                labelQualTitle,
                labelQualDescription,
                labelQualify,
                comboBoxQualValue,
                labelQualSubset,
                checkBoxQualType,
                //comboBoxQualType,
                checkBoxQualClass,
                //comboBoxQualClass,
                plotQualify);


            strates_Changed(sender, e);

            // Models
            models_Changed(sender, e);
        }

        #region Totals

        private void totals_Changed(object sender, EventArgs e)
        {
            string valueMask = "{0} ({1:P0})";

            if (selectedStatSpc == null)
            {
                double total = AllowedStack.Quantity();

                double n1 = data.Individual.Count;
                double n2 = AllowedStack.Measured();
                double n3 = AllowedStack.Weighted();
                double n35 = AllowedStack.Registered();
                double n4 = AllowedStack.Aged();
                double n5 = AllowedStack.Sexed();
                double n6 = AllowedStack.Matured();
                double n7 = data.Stratified.Quantity;
                double n8 = AllowedStack.MassSampled();

                if (total > 0) textBoxSpsTotal.Text = total.ToString();
                else textBoxSpsTotal.Text = Constants.Null;

                if (n1 > 0) textBoxSpcLog.Text = string.Format(valueMask, n1, n1 / total);
                else textBoxSpcLog.Text = Constants.Null;

                if (n2 > 0) textBoxSpcL.Text = string.Format(valueMask, n2, n2 / total);
                else textBoxSpcL.Text = Constants.Null;

                if (n3 > 0) textBoxSpcW.Text = string.Format(valueMask, n3, n3 / total);
                else textBoxSpcW.Text = Constants.Null;

                if (n35 > 0) textBoxSpcReg.Text = string.Format(valueMask, n35, n35 / total);
                else textBoxSpcReg.Text = Constants.Null;

                if (n4 > 0) textBoxSpcA.Text = string.Format(valueMask, n4, n4 / total);
                else textBoxSpcA.Text = Constants.Null;

                if (n5 > 0) textBoxSpcS.Text = string.Format(valueMask, n5, n5 / total);
                else textBoxSpcS.Text = Constants.Null;

                if (n6 > 0) textBoxSpcM.Text = string.Format(valueMask, n6, n6 / total);
                else textBoxSpcM.Text = Constants.Null;

                if (n7 > 0) textBoxSpcStrat.Text = string.Format(valueMask, n7, n7 / total);
                else textBoxSpcStrat.Text = Constants.Null;

                if (n8 > 0) textBoxSpcWTotal.Text = n8.ToString("N3");
                else textBoxSpcWTotal.Text = Constants.Null;

                textBoxSpcWLog.Text = Constants.Null;
                textBoxSpcWStrat.Text = Constants.Null;

                chartSpcStats.Series[0].Points.Clear();
                chartSpcStats.Legends[0].Enabled = true;

                foreach (Data.SpeciesRow speciesRow in data.Species)
                {
                    chartSpcStats.Series[0].Points.Add(GetSpeciesDataPoint(speciesRow, false));
                }

                chartSpcStats.Series[0].Sort(PointSortOrder.Descending);
            }
            else
            {
                double total = AllowedStack.Quantity(selectedStatSpc);

                double n_str = AllowedStack.QuantityStratified(selectedStatSpc);
                double n_ind = AllowedStack.QuantityIndividual(selectedStatSpc);
                double n2 = AllowedStack.Measured(selectedStatSpc);
                double n3 = AllowedStack.Weighted(selectedStatSpc);
                double n35 = AllowedStack.Registered(selectedStatSpc);
                double n4 = AllowedStack.Aged(selectedStatSpc);
                double n5 = AllowedStack.Sexed(selectedStatSpc);
                double n6 = AllowedStack.Matured(selectedStatSpc);

                if (n_str > 0) textBoxSpcStrat.Text = string.Format(valueMask, n_str, n_str / total);
                else textBoxSpcStrat.Text = Constants.Null;

                if (n_ind > 0) textBoxSpcLog.Text = string.Format(valueMask, n_ind, n_ind / total);
                else textBoxSpcL.Text = Constants.Null;

                if (n2 > 0) textBoxSpcL.Text = string.Format(valueMask, n2, n2 / total);
                else textBoxSpcL.Text = Constants.Null;

                if (n3 > 0) textBoxSpcW.Text = string.Format(valueMask, n3, n3 / total);
                else textBoxSpcW.Text = Constants.Null;

                if (n35 > 0) textBoxSpcReg.Text = string.Format(valueMask, n35, n35 / total);
                else textBoxSpcReg.Text = Constants.Null;

                if (n4 > 0) textBoxSpcA.Text = string.Format(valueMask, n4, n4 / total);
                else textBoxSpcA.Text = Constants.Null;

                if (n5 > 0) textBoxSpcS.Text = string.Format(valueMask, n5, n5 / total);
                else textBoxSpcS.Text = Constants.Null;

                if (n6 > 0) textBoxSpcM.Text = string.Format(valueMask, n6, n6 / total);
                else textBoxSpcM.Text = Constants.Null;

                double totalMass = AllowedStack.Mass(selectedStatSpc);

                if (totalMass > 0)
                {
                    textBoxSpcWTotal.Text = totalMass.ToString("N3");
                    textBoxSpcWLog.Text = AllowedStack.MassIndividual(selectedStatSpc).ToString("N3");

                    double stratifiedMass = AllowedStack.MassStratified(selectedStatSpc);

                    if (stratifiedMass > 0) {
                        textBoxSpcWStrat.Text = stratifiedMass.ToString("N3");
                    } else {
                        textBoxSpcWStrat.Text = Constants.Null;
                    }
                }
                else
                {
                    textBoxSpcWTotal.Text = Constants.Null;
                    textBoxSpcWLog.Text = Constants.Null;
                    textBoxSpcWStrat.Text = Constants.Null;
                }

                if (total > 0) textBoxSpsTotal.Text = total.ToString();
                else textBoxSpsTotal.Text = Constants.Null;

                chartSpcStats.Series[0].Points.Clear();
                chartSpcStats.Legends[0].Enabled = false;

                DataPoint speciesPoint = GetSpeciesDataPoint(selectedStatSpc, false);
                chartSpcStats.Series[0].Points.Add(speciesPoint);

                DataPoint spacePoint = new DataPoint();
                spacePoint.YValues[0] = AllowedStack.QuantitySampled() - AllowedStack.QuantitySampled(selectedStatSpc);
                spacePoint.Color = speciesPoint.Color.GetDesaturatedColor(.1);
                chartSpcStats.Series[0].Points.Add(spacePoint);
            }
        }

        //private void chartSpcStats_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    HitTestResult hitTest = chartRate.HitTest(e.X, e.Y, false);

        //    if (e.Button == MouseButtons.Left)
        //    {
        //        switch (hitTest.ChartElementType)
        //        {
        //            case ChartElementType.DataPoint:
        //            case ChartElementType.LegendItem:
        //                if (hitTest.Series == null) return;

        //                SelectedStatSpc = data.Species.FindBySpecies(hitTest.Series.Points[hitTest.PointIndex].GetCustomProperty("Species"));

        //                if (SelectedStatSpc != null)
        //                {
        //                    spreadSheetInd.EnsureFilter(columnIndSpecies, SelectedStatSpc.Species,
        //                        loaderInd, menuItemIndAll_Click);
        //                }

        //                break;
        //        }
        //    }
        //}

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            loadIndividuals(selectedStatSpc);
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            getFilteredList(columnIndLength);
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            getFilteredList(columnIndMass);
        }

        private void label35_DoubleClick(object sender, EventArgs e)
        {
            getFilteredList(columnIndTally);
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            getFilteredList(columnIndAge);
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            getFilteredList(columnIndSex);
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {
            getFilteredList(columnIndMaturity);
        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            if (selectedStatSpc == null)
            {
                if (data.Stratified.Count == 0) return;
                menuItemLoadStratified_Click(sender, e);
            }
            else
            {
                if (AllowedStack.QuantityStratified(selectedStatSpc) == 0) return;

                spreadSheetStratified.EnsureFilter(columnStratifiedSpc,
                    selectedStatSpc.Species, loaderStratified, menuItemLoadStratified_Click);
            }
        }

        #endregion

        #region Techniques

        private void tech_Changed(object sender, EventArgs e)
        {
            selectedTechSamplerType = comboBoxTechSampler.SelectedValue == null ?
                FishSamplerType.None : ((FishSamplerTypeDisplay)comboBoxTechSampler.SelectedValue).Type;

            spreadSheetTech.Rows.Clear();

            if (selectedTechSamplerType == FishSamplerType.None) return;

            CardStack techStack = FullStack.GetStack(selectedTechSamplerType);

            string[] meshValues = techStack.Classes(selectedTechSamplerType);

            if (meshValues.Length == 0)
            {
                spreadSheetTech.Rows.Add(GetTechRow(techStack));
            }
            else
            {
                foreach (string meshValue in meshValues)
                {
                    CardStack meshData = FullStack.GetStack(selectedTechSamplerType, meshValue);
                    spreadSheetTech.Rows.Add(GetTechRow(meshData));
                }
            }

            labelTechNoData.Visible = spreadSheetTech.RowCount == 0;
        }

        #endregion

        #region Strates

        private void checkBoxStrateUseGearType_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxQualType.Enabled =
                checkBoxQualClass.Enabled =
                checkBoxQualType.Checked;

            strates_Changed(sender, e);
        }

        private void comboBoxStrateGearType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxQualClass.DataSource = AllowedStack.Classes(((FishSamplerTypeDisplay)comboBoxQualType.SelectedValue).Type);

            if (((string[])comboBoxQualClass.DataSource).Length == 0)
            {
                strates_Changed(sender, e);
            }
        }

        private void checkBoxStrateUseClass_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxQualClass.Enabled = checkBoxQualClass.Checked;
            strates_Changed(sender, e);
        }

        private void checkBoxStrateUseClass_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBoxQualClass.Enabled)
            {
                checkBoxQualClass.Checked = false;
            }
        }

        private void strates_Changed(object sender, EventArgs e)
        {
            if (selectedStatSpc == null) return;

            CardStack countStack = AllowedStack;

            if (checkBoxQualType.Checked)
            {
                FishSamplerType samplerType = ((FishSamplerTypeDisplay)comboBoxQualType.SelectedValue).Type;

                if (checkBoxQualClass.Checked && comboBoxQualClass.SelectedValue != null)
                {
                    countStack = FullStack.GetStack(samplerType, comboBoxQualClass.SelectedValue.ToString());
                }
                else
                {
                    countStack = FullStack.GetStack(samplerType);
                }
            }

            histSample.Data =
                countStack.GetStatisticComposition(selectedStatSpc,
                (s, i) => { return countStack.Quantity(s, i); }, Resources.Interface.StratesSampled).GetHistogramSample();

            histWeighted.Data =
                countStack.GetStatisticComposition(selectedStatSpc,
                (s, i) => { return countStack.Weighted(s, i); }, Resources.Interface.StratesWeighted).GetHistogramSample();

            histRegistered.Data =
                countStack.GetStatisticComposition(selectedStatSpc,
                (s, i) => { return countStack.Registered(s, i); }, Resources.Interface.StratesRegistered).GetHistogramSample();

            histAged.Data =
                countStack.GetStatisticComposition(selectedStatSpc,
                (s, i) => { return countStack.Aged(s, i); }, Resources.Interface.StratesAged).GetHistogramSample();

            plotQualify.DoPlot();

            foreach (Histogramma hist in new Histogramma[] { histSample, histWeighted, histRegistered, histAged })
            {
                if (hist.Series != null) hist.Series.SetCustomProperty("DrawSideBySide", "False");
            }
        }

        #endregion

        #region Models

        private void models_Changed(object sender, EventArgs e)
        {
            //if (Licensing.AllowedFeaturesLevel < FeatureLevel.Advanced) return;

            plotQualify.Visible = selectedStatSpc != null;

            if (selectedStatSpc == null) return;

            selectedQualificationWay = (DataQualificationWay)comboBoxQualValue.SelectedIndex;

            spreadSheetSpcStats.Enabled =
                comboBoxQualValue.Enabled =
                checkBoxQualOutliers.Enabled =
                false;

            plotQualify.AllowDrop = selectedQualificationWay != DataQualificationWay.None;

            Cursor = plotQualify.Cursor = Cursors.WaitCursor;

            plotQualify.AxisY2Title =
                selectedQualificationWay == DataQualificationWay.WeightOfLength ?
                Wild.Resources.Reports.Caption.MassUnit : Wild.Resources.Reports.Caption.AgeUnit;

            if (qualCalc.IsBusy) { qualCalc.CancelAsync(); }
            else { qualCalc.RunWorkerAsync(); }
        }

        private void qualCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (selectedQualificationWay)
            {
                case DataQualificationWay.WeightOfLength:
                    model = data.FindMassModel(selectedStatSpc.Species);
                    break;

                case DataQualificationWay.AgeOfLength:
                    model = data.FindGrowthModel(selectedStatSpc.Species);
                    break;

                default:
                    model = null;
                    return;
            }
        }

        private void qualCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (selectedStatSpc != null)
                {
                    qualCalc.RunWorkerAsync(comboBoxQualValue.SelectedIndex);
                }

                return;
            }

            if (model != null)
            {
                if (model.ExternalData == null)
                {
                    ext.Calc = null;
                    histBio.Data = null;
                    resetQualPlotAxes();
                }
                else
                {
                    ext.TransposeCharting = selectedQualificationWay == DataQualificationWay.AgeOfLength;
                    ext.Calc = model.ExternalData;
                    if (ext.Series != null) ext.Series.YAxisType = AxisType.Secondary;

                    double from = selectedQualificationWay == DataQualificationWay.AgeOfLength ? model.ExternalData.Data.Y.Minimum : model.ExternalData.Data.X.Minimum;
                    double to = selectedQualificationWay == DataQualificationWay.AgeOfLength ? model.ExternalData.Data.Y.Maximum : model.ExternalData.Data.X.Maximum;

                    LengthComposition bioComposition = LengthComposition.Get(plotQualify.AxisXInterval, from, to,
                            (size) => { return (selectedQualificationWay == DataQualificationWay.AgeOfLength ? model.ExternalData.Data.Y : model.ExternalData.Data.X).GetSabsample(size).Count; },
                            Resources.Interface.StratesBio);

                    if (bioComposition.Count > 0)
                    {
                        histBio.Data = bioComposition.GetHistogramSample();
                        resetQualPlotAxes(
                            Math.Min(plotQualify.AxisXMin, bioComposition[0].Size.LeftEndpoint),
                            Math.Max(plotQualify.AxisXMax, ((SizeClass)bioComposition.GetLast()).Size.RightEndpoint),
                            Math.Max(plotQualify.AxisYMax, bioComposition.MostSampled.Quantity));
                    }
                    else
                    {
                        histBio.Data = null;
                        resetQualPlotAxes();
                    }
                }

                if (model.InternalData == null)
                {
                    inter.Calc = null;
                }
                else
                {
                    inter.TransposeCharting = selectedQualificationWay == DataQualificationWay.AgeOfLength;
                    inter.Calc = model.InternalData;
                    if (inter.Series != null) inter.Series.YAxisType = AxisType.Secondary;
                }

                if (model.CombinedData == null)
                {
                    combi.Calc = null;
                }
                else
                {
                    combi.TransposeCharting = selectedQualificationWay == DataQualificationWay.AgeOfLength;
                    combi.Calc = model.CombinedData;
                    if (combi.Series != null) combi.Series.YAxisType = AxisType.Secondary;
                    combi.Properties.SelectedApproximationType = model.Nature;
                    combi.Properties.ShowTrend =
                        combi.Properties.ShowPredictionBands = true;
                }
            }
            else
            {
                ext.Calc = null;
                inter.Calc = null;
                combi.Calc = null;
                histBio.Data = null;
                resetQualPlotAxes();
            }

            plotQualify.DoPlot();

            if (histBio.Series != null) histBio.Series.SetCustomProperty("DrawSideBySide", "False");

            //foreach (System.Windows.Forms.DataVisualization.Charting.Series ser in plotQualify.Series)
            //{

            //}

            Cursor = plotQualify.Cursor = Cursors.Default;
            spreadSheetSpcStats.Enabled =
                comboBoxQualValue.Enabled =
                true;
        }

        private void plotQualify_AxesUpdated(object sender, EventArgs e)
        {
            plotQualify.AxesUpdated -= plotQualify_AxesUpdated;

            if (!plotQualify.AxisYAutoMaximum)
            {
                int max = 0;
                foreach (Histogramma h in plotQualify.Histograms)
                {
                    max = Math.Max(max, h.Top);
                }

                plotQualify.AxisYMax = Mayfly.Service.AdjustRight(0, Math.Max(UserSettings.RequiredClassSize, max));
                plotQualify.ChartAreas[0].AxisY.Maximum = plotQualify.AxisYMax;
                plotQualify.UpdateAxes();
            }

            plotQualify.AxesUpdated += plotQualify_AxesUpdated;
        }

        private void checkBoxQualOutliers_CheckedChanged(object sender, EventArgs e)
        {
            combi.Updated -= combi_Updated;
            plotQualify.AxesUpdated -= plotQualify_AxesUpdated;

            //outliers = plotQualify.GetSample("Out") as Scatterplot;

            if (checkBoxQualOutliers.Checked)
            {
                if (outliers == null)
                {
                    outliers = new Scatterplot(outliersData, string.Format(Mathematics.Resources.Interface.Outliers, combi.Properties.ConfidenceLevel, combi.Properties.ScatterplotName));
                    outliers.Properties.DataPointColor = Mathematics.UserSettings.ColorSelected;
                    plotQualify.AddSeries(outliers);
                }
                else
                {
                    outliers.Properties.ScatterplotName = string.Format(Mathematics.Resources.Interface.Outliers, combi.Properties.ConfidenceLevel, combi.Properties.ScatterplotName);
                    outliers.Calc.Data = outliersData;
                }

                outliers.Update();
                //plotQualify.DoPlot();
                outliers.Series.YAxisType = combi.Series.YAxisType;
            }
            else
            {
                if (outliers != null)
                {
                    plotQualify.Remove(outliers.Properties.ScatterplotName);
                    outliers = null;
                }
            }

            combi.Updated += combi_Updated;
            plotQualify.AxesUpdated += plotQualify_AxesUpdated;
        }

        private void buttonQualOutliers_Click(object sender, EventArgs e)
        {
            if (selectedQualificationWay == DataQualificationWay.None) return;

            IsBusy = true;
            spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();

            BackgroundWorker outlierEditor = new BackgroundWorker();
            outlierEditor.DoWork += outlierEditor_DoWork;
            outlierEditor.RunWorkerCompleted += new RunWorkerCompletedEventHandler(indLoader_RunWorkerCompleted);
            outlierEditor.RunWorkerAsync();
        }

        private void outlierEditor_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

            foreach (var pair in outliersData)
            {
                indRows.AddRange(FullStack.GetIndividuals(selectedStatSpc,
                    (selectedQualificationWay == 0 ? new string[] { "Length", "Mass" } : new string[] { "Age", "Length" }),
                    new object[] { pair.X, pair.Y }));
            }

            loadIndividuals(indRows.ToArray());
        }

        #endregion

        private void buttonSpeciesStatsReport_Click(object sender, EventArgs e)
        {
            SpeciesStatsLevel lvl = SpeciesStatsLevel.Totals | SpeciesStatsLevel.Detailed | SpeciesStatsLevel.TreatmentSuggestion | SpeciesStatsLevel.SurveySuggestion;

            if (selectedStatSpc == null)
            {
                Report report = new Report(Resources.Reports.Sections.SpeciesStats.Title);
                AllowedStack.AddCommon(report);
                AllowedStack.Sort();
                AllowedStack.AddSpeciesStatsReport(report, lvl, ExpressionVariant.Efforts);
                report.EndBranded();
                report.Run();
            }
            else
            {
                Report report = new Report(string.Format(Resources.Reports.Sections.SpeciesStats.TitleSpecies, selectedStatSpc.KeyRecord.FullNameReport));
                AllowedStack.AddCommon(report, selectedStatSpc);
                AllowedStack.Sort();
                AllowedStack.AddSpeciesStatsReport(report, selectedStatSpc, lvl, ExpressionVariant.Efforts);
                report.EndBranded();
                report.Run();
            }
        }

        #endregion




        #region Cards

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Stratified | CardReportLevel.Individuals).Run();
        }

        private void menuItemCardCatches_Click(object sender, EventArgs e)
        {
            menuItemGearStats_Click(sender, e);
            tabControlCard.SelectedTab = tabPageCardCatches;
        }

        private void menuItemCardEfforts_Click(object sender, EventArgs e)
        {
            menuItemGearStats_Click(sender, e);
            tabControlCard.SelectedTab = tabPageCardEfforts;
        }

        private void menuItemCardMeteo_CheckedChanged(object sender, EventArgs e)
        {
            ColumnCardWeather.Visible =
            ColumnCardTempSurface.Visible =
                menuItemCardMeteo.Checked;
        }



        private void cardLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            CardStack stack = (CardStack)e.Argument;

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

                if (effortSource.Contains(spreadSheetCard.Columns[e.ColumnIndex]))
                {
                    spreadSheetCard[columnCardEffort.Index, e.RowIndex].Value =
                        findCardRow(spreadSheetCard.Rows[e.RowIndex]).GetEffort();
                }
            }

            if (spreadSheetCard.Columns[e.ColumnIndex].ValueType == typeof(Waypoint))
            {
                saveCardRow(spreadSheetCard.Rows[e.RowIndex]);
            }
        }

        private void spreadSheetCard_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow gridRow = spreadSheetCard.Rows[e.RowIndex];
            int id = (int)gridRow.Cells[columnCardID.Name].Value;
            Data.CardRow cardRow = data.Card.FindByID(id);

            if (cardRow.IsSamplerNull())
            { }
            else
            {
                Samplers.SamplerRow samplerRow = cardRow.SamplerRow;
                if (samplerRow.IsEffortFormulaNull())
                {
                    foreach (DataGridViewColumn gridColumn in new DataGridViewColumn[] { columnCardMesh,
                        columnCardHook, columnCardLength, columnCardOpening,
                        columnCardHeight, columnCardSquare, columnCardSpan,
                        columnCardVelocity, columnCardExposure })
                    {
                        gridRow.Cells[gridColumn.Index].ReadOnly = true;
                    }
                }
                else
                {
                    if (!samplerRow.EffortFormula.Contains("M"))
                    {
                        gridRow.Cells[columnCardMesh.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("J"))
                    {
                        gridRow.Cells[columnCardHook.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("L"))
                    {
                        gridRow.Cells[columnCardLength.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("O"))
                    {
                        gridRow.Cells[columnCardOpening.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("H"))
                    {
                        gridRow.Cells[columnCardHeight.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("S"))
                    {
                        gridRow.Cells[columnCardSquare.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("T"))
                    {
                        gridRow.Cells[columnCardSpan.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("V"))
                    {
                        gridRow.Cells[columnCardVelocity.Name].ReadOnly = true;
                    }

                    if (!samplerRow.EffortFormula.Contains("E"))
                    {
                        gridRow.Cells[columnCardExposure.Name].ReadOnly = true;
                    }
                }
            }
        }



        private void contextCard_Opening(object sender, CancelEventArgs e)
        {
            CardStack stack = getCardStack(spreadSheetCard.SelectedRows);
            contextCardLog.Enabled = stack.GetLogRows().Length > 0;
            contextCardIndividuals.Enabled = stack.QuantitySampled() > 0;
            contextCardStratified.Enabled = stack.Quantity() - stack.QuantitySampled() > 0;
        }

        private void contextCardOpen_Click(object sender, EventArgs e)
        {
            foreach (string path in getCardStack(spreadSheetCard.SelectedRows).GetFilenames())
            {
                IO.RunFile(path);
            }
        }

        private void contextCardOpenFolder_Click(object sender, EventArgs e)
        {
            List<string> directories = new List<string>();

            foreach (string path in getCardStack(spreadSheetCard.SelectedRows).GetFilenames())
            {
                string dir = Path.GetDirectoryName(path);

                if (!directories.Contains(dir))
                {
                    directories.Add(dir);
                }
            }

            foreach (string dir in directories)
            {
                IO.RunFile(dir);
            }
        }

        private void contextCardSaveSet_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.SaveDialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllLines(UserSettings.Interface.SaveDialog.FileName, getCardStack(spreadSheetCard.SelectedRows).GetFilenames());
            }
        }

        private void contextCardLog_Click(object sender, EventArgs e)
        {
            loadLog(getCardStack(spreadSheetCard.SelectedRows));
        }

        private void contextCardIndividuals_Click(object sender, EventArgs e)
        {
            loadIndividuals(getCardStack(spreadSheetCard.SelectedRows));
        }

        private void contextCardStratified_Click(object sender, EventArgs e)
        {
            loadStratifiedSamples(getCardStack(spreadSheetCard.SelectedRows));
        }

        private void contextCardPrintNotes_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e)
        {
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Stratified | CardReportLevel.Individuals).Run();
        }

        #endregion




        #region Species

        private void menuItemSpcTotals_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            tabControlSpc.SelectedTab = tabPageSpcTotals;
        }

        private void menuItemSpcStrates_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            tabControlSpc.SelectedTab = tabPageSpcQualify;
        }



        private void spcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (baseSpc == null)
            {
                SpeciesComposition speciesComposition = AllowedStack.GetBasicCenosisComposition();
                for (int i = 0; i < speciesComposition.Count; i++)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetSpc);
                    gridRow.Cells[columnSpcID.Index].Value = speciesComposition[i].SpeciesRow.ID;
                    gridRow.Cells[columnSpcSpc.Index].Value = speciesComposition[i].SpeciesRow;
                    gridRow.Cells[columnSpcQuantity.Index].Value = speciesComposition[i].Quantity;
                    gridRow.Cells[columnSpcMass.Index].Value = speciesComposition[i].Mass;
                    gridRow.Cells[columnSpcOccurrence.Index].Value = speciesComposition[i].Occurrence;
                    result.Add(gridRow);

                    updateSpeciesArtifacts(gridRow);

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }
            else
            {
                for (int i = 0; i < taxaSpc.Length; i++)
                {

                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetSpc);
                    gridRow.Cells[columnSpcID.Index].Value = taxaSpc[i].ID;

                    double Q = 0.0;
                    double W = 0.0;

                    foreach (Data.LogRow logRow in data.Log)
                    {
                        if (!taxaSpc[i].Includes(logRow.SpeciesRow.Species)) continue;

                        if (!logRow.IsQuantityNull())
                        {
                            Q += logRow.Quantity;
                        }

                        if (!logRow.IsMassNull())
                        {
                            W += logRow.Mass;
                        }
                    }

                    gridRow.Cells[columnSpcSpc.Index].Value = taxaSpc[i].TaxonName;
                    if (Q > 0) gridRow.Cells[columnSpcQuantity.Index].Value = Q;
                    if (W > 0) gridRow.Cells[columnSpcMass.Index].Value = W;
                    gridRow.Cells[columnSpcOccurrence.Index].Value = taxaSpc[i].Occurrence(data);

                    result.Add(gridRow);

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }

            e.Result = result.ToArray();
        }

        private void spcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSpc.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
            spreadSheetSpc.UpdateStatus();

            tabPageSpc.Parent = tabControl;
            tabControl.SelectedTab = tabPageSpc;

            updateArtifacts();
        }



        private void comboBoxSpcTaxa_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseSpc = comboBoxSpcTaxa.SelectedItem as SpeciesKey.BaseRow;
            taxaSpc = baseSpc == null ? null : data.Species.Taxa(baseSpc);
            loadSpc();

            menuItemSpcTaxa.Enabled = baseSpc == null;

            if (baseSpc != null)
            {
                spreadSheetSpc.ClearInsertedColumns();
            }

            columnSpcSpc.HeaderText = baseSpc == null ? Wild.Resources.Reports.Caption.Species : baseSpc.BaseName;
        }

        private void contextSpecies_Opening(object sender, CancelEventArgs e)
        {
            contextSpecies.Enabled = baseSpc == null;

            bool hasStratified = false;
            bool hasSampled = false;

            foreach (Data.SpeciesRow spcRow in getSpeciesRows(spreadSheetSpc.SelectedRows))
            {
                hasStratified |= FullStack.QuantityStratified(spcRow) > 0;
                hasSampled |= FullStack.QuantityIndividual(spcRow) > 0;
            }

            contextSpcIndividuals.Enabled = hasSampled;
            contextSpcStratified.Enabled = hasStratified;
        }

        private void contextSpcQualify_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            ColumnSpcStatsID.GetRow(getSpeciesRows(spreadSheetSpc.SelectedRows)[0].ID).Selected = true;
            tabControlSpc.SelectedTab = tabPageSpcQualify;
        }

        private void contextSpcLog_Click(object sender, EventArgs e)
        {
            loadLog(getSpeciesRows(spreadSheetSpc.SelectedRows));
        }

        private void contextSpcIndividuals_Click(object sender, EventArgs e)
        {
            loadIndividuals(getSpeciesRows(spreadSheetSpc.SelectedRows));
        }

        private void contextSpcStratified_Click(object sender, EventArgs e)
        {
            loadStratifiedSamples(getSpeciesRows(spreadSheetSpc.SelectedRows));
        }

        #endregion




        #region Log

        private void spreadSheetLog_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex != columnLogQuantity.Index && e.ColumnIndex != columnLogMass.Index) return;

            if (spreadSheetLog.ContainsFocus)
            {
                saveLogRow(spreadSheetLog.Rows[e.RowIndex]);
            }
        }

        private void spreadSheetLog_SelectionChanged(object sender, EventArgs e)
        { }

        private void contextLog_Opening(object sender, CancelEventArgs e)
        {
            contextLogOpen.Enabled = spreadSheetLog.SelectedRows.Count > 0;

            bool hasStratified = false;
            bool hasSampled = false;

            foreach (Data.LogRow logRow in getLogRows(spreadSheetLog.SelectedRows))
            {
                hasStratified |= logRow.QuantityStratified > 0;
                hasSampled |= logRow.QuantityIndividual() > 0;
            }

            contextLogStratified.Enabled = hasStratified;
            contextLogIndividuals.Enabled = hasSampled;
        }

        private void contextLogOpen_Click(object sender, EventArgs e)
        {
            foreach (Data.LogRow logRow in getLogRows(spreadSheetLog.SelectedRows))
            {
                IO.RunFile(logRow.CardRow.Path,
                    new object[] { logRow.SpeciesRow.Species });
            }
        }

        private void contextLogIndividuals_Click(object sender, EventArgs e)
        {
            loadIndividuals(getLogRows(spreadSheetLog.SelectedRows));
        }

        private void contextLogStratified_Click(object sender, EventArgs e)
        {
            loadStratifiedSamples(getLogRows(spreadSheetLog.SelectedRows));
        }

        private void logLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Data.LogRow[] logRows = (Data.LogRow[])e.Argument;

            for (int i = 0; i < logRows.Length; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetLog);
                gridRow.Cells[columnLogID.Index].Value = logRows[i].ID;
                updateLogRow(gridRow);
                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
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

            columnLogDiversityA.Visible = (baseSpc != null);
            columnLogDiversityB.Visible = (baseSpc != null);

            updateArtifacts();
        }

        private void logExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                Data.LogRow logRow = findLogRow(gridRow);
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
                gridRow.DataGridView.Invoke(valueSetter,
                    new object[] { logRow.CardRow, gridRow, spreadSheetLog.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }

        private void buttonSelectLog_Click(object sender, EventArgs e)
        {
            if (loadCardAddt(spreadSheetLog))
            {
                IsBusy = true;
                processDisplay.StartProcessing(spreadSheetLog.RowCount,
                    Wild.Resources.Interface.Process.ExtSpc);
                loaderLogExtended.RunWorkerAsync();
            }
        }

        #endregion




        #region Individuals

        private void menuItemIndividuals_DropDownOpening(object sender, EventArgs e)
        {
            menuItemDietExplorer.Enabled = false;

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                Data.IndividualRow indRow = findIndividualRow(gridRow);
                if (!indRow.IsDietPresented()) continue;
                menuItemDietExplorer.Enabled = true;
                break;
            }

            menuItemIndSimulate.Enabled = data.Stratified.Count > 0;
            menuItemIndClearSimulated.Enabled = (data.Stratified.Count > 0 && spreadSheetInd.ContainsEmptyCells(columnIndID));
        }

        private void menuItemIndPrintLog_Click(object sender, EventArgs e)
        {
            getIndividuals(spreadSheetInd.Rows).GetReport(CardReportLevel.Individuals).Run();
        }

        private void menuItemIndPrint_Click(object sender, EventArgs e)
        {
            getIndividuals(spreadSheetInd.Rows).GetReport(CardReportLevel.Profile).Run();
        }

        private void menuItemIndExtra_CheckedChanged(object sender, EventArgs e)
        {
            columnIndCondition.Visible =
                columnIndConditionSoma.Visible =

                menuItemIndExtra.Checked;

            columnIndSomaticMass.Visible =
               menuItemIndExtra.Checked || menuItemIndFecundity.Checked || menuItemIndDiet.Checked;
        }

        private void menuItemIndFecundity_CheckedChanged(object sender, EventArgs e)
        {
            columnIndGonadMass.Visible =
               columnIndGonadSampleMass.Visible =
               columnIndGonadSample.Visible =
               columnIndEggSize.Visible =

               columnIndFecundityAbs.Visible =
               columnIndFecundityRelative.Visible =
               columnIndFecundityRelativeSoma.Visible =
               columnIndGonadIndex.Visible =
               columnIndGonadIndexSoma.Visible =

               menuItemIndFecundity.Checked;

            columnIndSomaticMass.Visible =
               menuItemIndExtra.Checked || menuItemIndFecundity.Checked || menuItemIndDiet.Checked;
        }

        private void menuItemIndDiet_CheckedChanged(object sender, EventArgs e)
        {
            columnIndSomaticMass.Visible =
                columnIndFat.Visible =
                columnIndConsumed.Visible =
                columnIndConsumptionIndex.Visible =
                columnIndDietItems.Visible =
                menuItemDietExplorer.Enabled =
                menuItemIndDiet.Checked;

            columnIndSomaticMass.Visible =
               menuItemIndExtra.Checked || menuItemIndFecundity.Checked || menuItemIndDiet.Checked;
        }

        private void menuItemIndSimulate_Click(object sender, EventArgs e)
        {
            // Check wheather or not simulated individuals are already loaded
            if (spreadSheetInd.ContainsEmptyCells(columnIndID))
            {
                if (taskDialogClearSimulations.ShowDialog() == tdbClearSimulation)
                {
                    ClearSimulated();
                    SimulateStratifiedSamples();
                }
            }
            else
            {
                SimulateStratifiedSamples();
            }
        }

        private void menuItemIndClearSimulated_Click(object sender, EventArgs e)
        {
            ClearSimulated();
        }



        private void indLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Data.IndividualRow[] indRows = (Data.IndividualRow[])e.Argument;

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
        }

        private void indExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { findIndividualRow(gridRow).LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void bioTipper_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Licensing.AllowedFeaturesLevel < FeatureLevel.Advanced) return;

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (UserSettings.AgeSuggest) setIndividualAgeTip(gridRow);
                if (UserSettings.MassSuggest) setIndividualMassTip(gridRow);
            }
        }

        private void bioTipper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetInd.StopProcessing();
        }



        private void stratifiedSimulator_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            int totalCount = 0;

            if (individualSpecies == null)
            {
                for (int i = 0; i < data.Log.Rows.Count; i++)
                {
                    int count = data.Log[i].QuantityStratified;

                    if (count > 0)
                    {
                        result.AddRange(IndividualRows(data.Log[i]));
                    }

                    totalCount += count;

                    (sender as BackgroundWorker).ReportProgress(totalCount);
                }
            }
            else
            {
                Data.LogRow[] logRows = individualSpecies.GetLogRows();

                for (int i = 0; i < logRows.Length; i++)
                {
                    int count = logRows[i].QuantityStratified;

                    if (count > 0)
                    {
                        result.AddRange(IndividualRows(logRows[i]));
                    }

                    totalCount += count;

                    (sender as BackgroundWorker).ReportProgress(totalCount);
                }
            }

            e.Result = result.ToArray();
        }

        private void stratifiedSimulator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetInd.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetInd.StopProcessing();
            spreadSheetInd.UpdateStatus();

            if ((e.Result as DataGridViewRow[]).Length == 0) return;

            spreadSheetInd.FirstDisplayedScrollingRowIndex = (e.Result as DataGridViewRow[])[0].Index;
        }



        private void spreadSheetInd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetInd.Columns[e.ColumnIndex].ReadOnly) return;

            Data.IndividualRow editedIndividualRow;

            // If values was typed in
            if (spreadSheetInd.ContainsFocus)
            {
                // Save all new values
                editedIndividualRow = saveIndividualRow(spreadSheetInd.Rows[e.RowIndex]);

                // If mass OR age was changed
                if (e.ColumnIndex == columnIndLength.Index || e.ColumnIndex == columnIndMass.Index || e.ColumnIndex == columnIndAge.Index)
                {
                    // Recalculate if needed
                    if (bioUpdater.IsBusy) { bioUpdater.CancelAsync(); }
                    else { bioUpdater.RunWorkerAsync(editedIndividualRow.LogRow.SpeciesRow); }
                }

                // If mass was changed
                if (e.ColumnIndex == columnIndMass.Index)
                {
                    // Reset Total mass status text
                    statusMass.ResetFormatted(AllowedStack.Mass());
                    labelWgtValue.Text = Wild.Service.GetFriendlyMass(AllowedStack.Mass() * 1000);
                }

            }
            else
            {
                editedIndividualRow = findIndividualRow(spreadSheetInd.Rows[e.RowIndex]);
            }

            // Further - autocalculations

            // Fecundity recalc
            if (e.ColumnIndex == columnIndGonadMass.Index ||
                e.ColumnIndex == columnIndGonadSample.Index ||
                e.ColumnIndex == columnIndGonadSampleMass.Index ||
                e.ColumnIndex == columnIndMass.Index ||
                e.ColumnIndex == columnIndSomaticMass.Index)
            {
                if (double.IsNaN(editedIndividualRow.GetAbsoluteFecundity()))
                {
                    spreadSheetInd[columnIndFecundityAbs.Index, e.RowIndex].Value = null;
                }
                else
                {
                    spreadSheetInd[columnIndFecundityAbs.Index, e.RowIndex].Value = editedIndividualRow.GetAbsoluteFecundity() / 1000.0;
                }

                if (double.IsNaN(editedIndividualRow.GetRelativeFecundity()))
                {
                    spreadSheetInd[columnIndFecundityRelative.Index, e.RowIndex].Value = null;
                }
                else
                {
                    spreadSheetInd[columnIndFecundityRelative.Index, e.RowIndex].Value = editedIndividualRow.GetRelativeFecundity();
                }

                if (double.IsNaN(editedIndividualRow.GetRelativeFecunditySomatic()))
                {
                    spreadSheetInd[columnIndFecundityRelativeSoma.Index, e.RowIndex].Value = null;
                }
                else
                {
                    spreadSheetInd[columnIndFecundityRelativeSoma.Index, e.RowIndex].Value = editedIndividualRow.GetRelativeFecunditySomatic();
                }
            }

            // Gonad index recalc
            if (e.ColumnIndex == columnIndSomaticMass.Index ||
                e.ColumnIndex == columnIndGonadMass.Index)
            {
                if (double.IsNaN(editedIndividualRow.GetGonadIndex()))
                {
                    spreadSheetInd[columnIndGonadIndex.Index, e.RowIndex].Value = null;
                }
                else
                {
                    spreadSheetInd[columnIndGonadIndex.Index, e.RowIndex].Value = editedIndividualRow.GetGonadIndex();
                }
            }

            // Gonad index somatic recalc
            if (e.ColumnIndex == columnIndSomaticMass.Index ||
                e.ColumnIndex == columnIndGonadMass.Index)
            {
                if (double.IsNaN(editedIndividualRow.GetGonadIndexSomatic()))
                {
                    spreadSheetInd[columnIndGonadIndexSoma.Index, e.RowIndex].Value = null;
                }
                else
                {
                    spreadSheetInd[columnIndGonadIndexSoma.Index, e.RowIndex].Value = editedIndividualRow.GetGonadIndexSomatic();
                }
            }

            // Consumption index
            if (e.ColumnIndex == columnIndSomaticMass.Index ||
                e.ColumnIndex == columnIndConsumed.Index)
            {
                if (double.IsNaN(editedIndividualRow.GetConsumptionIndex()))
                {
                    spreadSheetInd[columnIndConsumptionIndex.Index, e.RowIndex].Value = null;
                }
                else
                {
                    spreadSheetInd[columnIndConsumptionIndex.Index, e.RowIndex].Value = editedIndividualRow.GetConsumptionIndex();
                }
            }


        }

        private void bioUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Licensing.AllowedFeaturesLevel < FeatureLevel.Advanced) return;

            Data.SpeciesRow speciesRow = (Data.SpeciesRow)e.Argument;
            e.Result = speciesRow;

            data.FindMassModel(speciesRow.Species).RefreshInternal();
            data.FindGrowthModel(speciesRow.Species).RefreshInternal();
        }

        private void bioUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                bioUpdater.RunWorkerAsync(e.Result);
                return;
            }
            else if (selectedStatSpc == (Data.SpeciesRow)e.Result)
            {
                qualCalc.RunWorkerAsync((Data.SpeciesRow)e.Result);
            }

            applyBio();
        }


        private void spreadSheetInd_SelectionChanged(object sender, EventArgs e)
        {
            //selectedLogRows.Clear();

            //foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            //{
            //    Data.IndividualRow individualRow = findIndividualRow(gridRow);

            //    if (individualRow == null) continue;

            //    if (!selectedLogRows.Contains(individualRow.LogRow))
            //    {
            //        selectedLogRows.Add(individualRow.LogRow);
            //    }
            //}
        }

        private void spreadSheetInd_Filtered(object sender, EventArgs e)
        {
            List<string> speciesNames = columnIndSpecies.GetStrings(true);

            pictureBoxStratified.Visible = speciesNames.Count == 1 &&
                AllowedStack.QuantityStratified(data.Species.FindBySpecies(speciesNames[0])) > 0;

            if (pictureBoxStratified.Visible)
            {
                toolTipAttention.SetToolTip(pictureBoxStratified,
                    string.Format((new ResourceManager(typeof(MainForm))).GetString(
                    "pictureBoxStratified.ToolTip"), speciesNames[0]));
            }
        }


        private void contextInd_Opening(object sender, CancelEventArgs e)
        {
            contextIndExploreDiet.Enabled = false;

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow indRow = findIndividualRow(gridRow);
                if (!indRow.IsDietPresented()) continue;
                contextIndExploreDiet.Enabled = true;
                break;
            }

            printIndividualsLogToolStripMenuItem.Enabled = spreadSheetInd.SelectedRows.Count > 1;
        }

        private void contextIndProfile_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (Data.IndividualRow individualRow in getIndividuals(spreadSheetInd.SelectedRows))
            {
                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(spreadSheetInd);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.Show();

            }
        }

        private void fecundityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (Data.IndividualRow individualRow in getIndividuals(spreadSheetInd.SelectedRows))
            {
                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(spreadSheetInd);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowFecundity();
                individual.Show();
            }
        }

        private void growthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (Data.IndividualRow individualRow in getIndividuals(spreadSheetInd.SelectedRows))
            {
                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(spreadSheetInd);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowGrowth();
                individual.Show();
            }
        }

        private void contextIndTrophics_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (Data.IndividualRow individualRow in getIndividuals(spreadSheetInd.SelectedRows))
            {
                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(spreadSheetInd);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowTrophics();
                individual.Show();
            }
        }

        private void parasitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (Data.IndividualRow individualRow in getIndividuals(spreadSheetInd.SelectedRows))
            {
                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(spreadSheetInd);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowParasites();
                individual.Show();
            }
        }

        private void individual_FormClosing(object sender, FormClosingEventArgs e)
        {
            Individual profile = (Individual)sender;
            if (profile.DialogResult == DialogResult.OK)
            {
                rememberChanged(profile.IndividualRow.LogRow.CardRow);
                updateIndividualRow(columnIndID.GetRow(profile.IndividualRow.ID));
            }
        }

        private void contextIndOpen_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();
            
            //foreach (Data.LogRow logRow in selectedLogRows)
            //{
            //    Mayfly.IO.RunFile(logRow.CardRow.Path,
            //        new object[] { logRow.SpeciesRow.Species });

            //    // TODO: select row in a log
            //}
        }

        private void individual_ValueChanged(object sender, EventArgs e)
        {
            updateIndividualRow(columnIndID.GetRow(((Individual)sender).IndividualRow.ID));
        }

        private void printIndividualsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getIndividuals(spreadSheetInd.SelectedRows).GetReport(CardReportLevel.Individuals).Run();
        }

        private void contextIndPrint_Click(object sender, EventArgs e)
        {
            getIndividuals(spreadSheetInd.SelectedRows).GetReport(CardReportLevel.Profile).Run();
        }

        private void contextIndExploreDiet_Click(object sender, EventArgs e)
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(spreadSheetInd.SelectedRows.Count, Resources.Process.DietCompiling);
            dietCompiler.RunWorkerAsync(spreadSheetInd.SelectedRows);
        }

        private void buttonSelectInd_Click(object sender, EventArgs e)
        {
            if (loadCardAddt(spreadSheetInd))
            {
                IsBusy = true;
                processDisplay.StartProcessing(spreadSheetInd.RowCount,
                    Wild.Resources.Interface.Process.ExtInd);
                loaderIndExtended.RunWorkerAsync();
            }
        }

        private void pictureBoxStratified_DoubleClick(object sender, EventArgs e)
        {
            tabPageStratified.Parent = tabControl;
            tabControl.SelectedTab = tabPageStratified;
            List<string> speciesNames = columnIndSpecies.GetStrings(true);

            spreadSheetStratified.EnsureFilter(columnStratifiedSpc, speciesNames[0],
                loaderStratified, menuItemLoadStratified_Click);
        }

        #region Diet

        private void menuItemDiet_DropDownOpening(object sender, EventArgs e)
        {
            //menuItemDietRecover.Enabled = ;
        }

        private void menuItemDietExplorer_Click(object sender, EventArgs e)
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(spreadSheetInd.VisibleRowCount, Resources.Process.DietCompiling);
            dietCompiler.RunWorkerAsync(spreadSheetInd.GetVisibleRows());
        }

        private void dietCompiler_DoWork(object sender, DoWorkEventArgs e)
        {
            Data result = new Data();

            int index = 0;

            foreach (DataGridViewRow gridRow in (IEnumerable)e.Argument)
            {
                Data.IndividualRow individualRow = findIndividualRow(gridRow);

                // If pooled - correct ecological values, if NOT - editable
                Data indData = individualRow.GetConsumed(!ModifierKeys.HasFlag(Keys.Control));
                indData.CopyTo(result);

                //foreach (Data.IntestineRow intestineRow in individualRow.GetIntestineRows())
                //{
                //    Data intData = intestineRow.GetConsumed();
                //    if (intData == null)
                //        continue;
                //    intData.CopyTo(result)[0].ID = intestineRow.ID;
                //}

                index++;
                ((BackgroundWorker)sender).ReportProgress(index);
            }

            e.Result = result;
        }

        private void dietCompiler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            processDisplay.StopProcessing();

            Benthos.Explorer.MainForm dietExplorer = new Benthos.Explorer.MainForm((Data)e.Result);
            dietExplorer.SetSpeciesIndex(Fish.UserSettings.DietIndexPath);
            dietExplorer.CardRowSaved += dietExplorer_CardRowSaved;
            dietExplorer.Sync(this);
            this.WindowState = FormWindowState.Minimized;
            dietExplorer.Show();
            dietExplorer.PerformDietExplorer(AllowedStack.FriendlyName);
        }

        private void dietExplorer_CardRowSaved(object sender, CardRowSaveEvent e)
        {
            Data.IntestineRow intestineRow = data.Intestine.FindByID(e.Row.ID);
            Data consumed = e.Row.SingleCardDataset();
            // TODO: Clear consumed from Comments, date, water and other stuff
            intestineRow.Consumed = consumed.GetXml().Replace(System.Environment.NewLine, " ");
            double w = intestineRow.IndividualRow.GetConsumed().GetStack().Mass();
            if (!double.IsNaN(w)) intestineRow.IndividualRow.ConsumedMass = w;
            updateIndividualRow(columnIndID.GetRow(intestineRow.IndividualRow.ID));
            rememberChanged(intestineRow.IndividualRow.LogRow.CardRow);
        }

        #endregion

        #endregion




        #region Stratified

        private void printStratifiedSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getLogRowsStratified(spreadSheetStratified.Rows).GetReport(CardReportLevel.Stratified).Run();
        }

        private void printStratifiedSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getLogRowsStratified(spreadSheetStratified.SelectedRows).GetReport(CardReportLevel.Stratified).Run();
        }

        private void stratifiedLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Data.LogRow[] logRows = (Data.LogRow[])e.Argument;

            for (int i = 0; i < logRows.Length; i++)
            {
                if (logRows[i].QuantityStratified > 0)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetStratified);
                    gridRow.Cells[columnStratifiedID.Index].Value = logRows[i].ID;
                    gridRow.Cells[columnStratifiedSpc.Index].Value = logRows[i].SpeciesRow;
                    gridRow.Cells[columnStratifiedCount.Index].Value = logRows[i].QuantityStratified;
                    gridRow.Cells[columnStratifiedMass.Index].Value = logRows[i].MassStratified / 1000;
                    gridRow.Cells[columnStratifiedInterval.Index].Value = logRows[i].Interval;
                    gridRow.Cells[columnStratifiedFrom.Index].Value = logRows[i].MinStrate.LeftEndpoint;
                    gridRow.Cells[columnStratifiedTo.Index].Value = logRows[i].MaxStrate.LeftEndpoint;

                    foreach (DataGridViewColumn gridColumn in spreadSheetStratified.GetInsertedColumns())
                    {
                        setCardValue(logRows[i].CardRow, gridRow, gridColumn);
                    }

                    result.Add(gridRow);
                }
                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void stratifiedLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetStratified.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetStratified.StopProcessing();
            spreadSheetStratified.UpdateStatus((int)spreadSheetStratified.Sum(columnStratifiedCount));

            tabPageStratified.Parent = tabControl;
            tabControl.SelectedTab = tabPageStratified;

            //updateArtifacts();
        }

        private void stratifiedExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetStratified.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { findLogRowStratified(gridRow).CardRow, gridRow, spreadSheetStratified.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void spreadSheetStratified_SelectionChanged(object sender, EventArgs e)
        {
            if (spreadSheetStratified.SelectedRows.Count > 0)
            {
                double min = double.MaxValue;
                double max = double.MinValue;

                double interval = 0.1;

                foreach (Data.LogRow logRow in getLogRowsStratified(spreadSheetStratified.SelectedRows))
                {
                    if (logRow.QuantityStratified > 0)
                    {
                        interval = Math.Max(interval, logRow.Interval);
                        min = Math.Min(min, logRow.MinStrate.LeftEndpoint);
                        max = Math.Max(max, logRow.MaxStrate.LeftEndpoint);
                    }
                }

                stratifiedSample.Interval = interval;
                stratifiedSample.Reset(min, max, true);

                foreach (SizeGroup sizeGroup in stratifiedSample.SizeGroups)
                {
                    int countSum = 0;

                    foreach (DataGridViewRow gridRow in spreadSheetStratified.SelectedRows)
                    {
                        foreach (Data.StratifiedRow stratifiedRow in findLogRowStratified(gridRow).GetStratifiedRows())
                        {
                            if (sizeGroup.LengthInterval.LeftClosedContains(stratifiedRow.SizeClass.Midpoint))
                                countSum += stratifiedRow.Count;
                        }
                    }

                    sizeGroup.Count = countSum;
                }

                showStratified();

                stratifiedSample.PerformLayout();
            }
            else
            {
                hideStratified();
            }
        }

        private void spreadSheetStratified_Filtered(object sender, EventArgs e)
        {
            spreadSheetStratified.UpdateStatus((int)spreadSheetStratified.Sum(columnStratifiedCount));
        }

        private void buttonSelectStratified_Click(object sender, EventArgs e)
        {
            if (loadCardAddt(spreadSheetStratified))
            {
                IsBusy = true;
                processDisplay.StartProcessing(spreadSheetStratified.RowCount, Wild.Resources.Interface.Process.ExtStrat);
                loaderStratifiedExtended.RunWorkerAsync();
            }
        }

        #endregion
    }
}