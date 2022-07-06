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

            UI.SetMenuAvailability(License.AllowedFeaturesLevel >= FeatureLevel.Advanced,
                menuItemImportBio,
                menuItemExportBio,
                toolStripSeparator10,
                menuInstant
                );

            UI.SetMenuAvailability(License.AllowedFeaturesLevel == FeatureLevel.Insider,
                menuSurvey,
                menuModels,
                menuCohort);

            //UI.SetControlsAvailability(License.AllowedFeaturesLevel >= FeatureLevel.Advanced,
            //    labelQualify,
            //    buttonQualOutliers,
            //    checkBoxQualOutliers,
            //    comboBoxQualValue);

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

            columnCardWater.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardWhere.ValueType = typeof(Waypoint);
            columnCardWeather.ValueType = typeof(WeatherState);
            columnCardTempSurface.ValueType = typeof(double);
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

            columnSpcSpc.ValueType = typeof(TaxonomicIndex.TaxonRow);
            columnSpcQuantity.ValueType = typeof(double);
            columnSpcMass.ValueType = typeof(double);
            columnSpcOccurrence.ValueType = typeof(double);

            tabPageSpc.Parent = null;

            tabPageSpcStats.Parent = null;

            columnLogSpc.ValueType = typeof(TaxonomicIndex.TaxonRow);
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

            menuStrip.SetMenuIcons();

            chartSchedule.Format();

            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced)
            {
                plotQualify.SetBioAcceptable(LoadCards);
                spreadSheetInd.SetBioAcceptable(LoadCards);
                spreadSheetSpc.SetBioAcceptable(LoadCards);
                spreadSheetLog.SetBioAcceptable(LoadCards);
            }

            data = new Data(Fish.UserSettings.SpeciesIndex, Fish.UserSettings.SamplersIndex);
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

            if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-obs":
                        menuItemSurveyInput_Click(this, new EventArgs());
                        this.WindowState = FormWindowState.Minimized;
                        return;
                }
            }

            this.Load += (o, e) => { LoadCards(args.GetOperableFilenames(Fish.UserSettings.Interface.Extension)); };
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
            menuStratified.Visible = (tabControl.SelectedTab == tabPageStratified);
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
                    if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced)
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
                }
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
            //if (License.AllowedFeaturesLevel < FeatureLevel.Advanced)
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

            while (changedCards.Count > 0)
            {
                Data.CardRow cardRow = changedCards[0];

                index++;
                dataSaver.ReportProgress(index);

                if (cardRow.Path != null)
                {
                    Data _data = cardRow.SingleCardDataset();
                    _data.WriteToFile(cardRow.Path);
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
            foreach (FishSamplerTypeDisplay samplerType in FullStack.GetSamplerTypeDisplays()) {
                spreadSheetGearStats.Rows.Add(samplerType);
            }
            spreadSheetGearStats.Rows.Add(Mayfly.Resources.Interface.Total);

            dateTimePickerCatches.MinDate =
                dateTimePickerEffort.MinDate =
                FullStack.EarliestEvent;

            dateTimePickerCatches.MaxDate =
                dateTimePickerEffort.MaxDate =
                FullStack.LatestEvent;

            spreadSheetCatches.Rows.Clear();
            spreadSheetEfforts.Rows.Clear();
        }

        private void menuItemSpcStats_Click(object sender, EventArgs e)
        {
            tabPageSpcStats.Parent = tabControl;
            tabControl.SelectedTab = tabPageSpcStats;

            initializeSpeciesStatsPlot();

            clearSpcStats();

            spreadSheetSpcStats.Rows.Clear();
            foreach (TaxonomicIndex.TaxonRow speciesRow in FullStack.GetSpecies())
            {
                spreadSheetSpcStats.Rows.Add(speciesRow.ID, speciesRow);
            }
            spreadSheetSpcStats.Rows.Add(null, Mayfly.Resources.Interface.Total);

            comboBoxTechSampler.DataSource = FullStack.GetSamplerTypeDisplays();
            comboBoxTechSampler.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxTechSampler.DataSource)[0];

            comboBoxQualType.DataSource = FullStack.GetSamplerTypeDisplays();
            comboBoxQualType.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxQualType.DataSource)[0];

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

            foreach (TaxonomicIndex.TaxonRow spcRow in FullStack.GetSpecies())
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
            WizardSpawning spawnWizard = new WizardSpawning(FullStack);
            spawnWizard.Show();
        }

        #endregion

        #region Fishery

        private void menuItemCenosis_Click(object sender, EventArgs e)
        {
            WizardCenosis wizard = new WizardCenosis(FullStack);
            wizard.Show();
        }

        private void speciesComposition_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardPopulation wizard = new WizardPopulation(FullStack, speciesRow);
            wizard.Show();
        }




        private void speciesGrowthCohorts_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardGrowthCohorts wizard = new WizardGrowthCohorts(FullStack, speciesRow);
            wizard.Show();
        }

        private void speciesMortalityCohorts_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardMortalityCohorts wizard = new WizardMortalityCohorts(FullStack, speciesRow);
            wizard.Show();
        }

        private void menuItemExtrapolations_Click(object sender, EventArgs e)
        {
            WizardExtrapolations wizard = new WizardExtrapolations(FullStack);
            wizard.Show();
        }

        private void speciesStockExtrapolation_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardExtrapolation wizard = new WizardExtrapolation(FullStack, speciesRow);
            wizard.Show();
        }

        private void speciesStockVpa_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardVirtualPopulation wizard = new WizardVirtualPopulation(FullStack, speciesRow);
            wizard.Show();
        }

        private void speciesMSYR_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardMSYR wizard = new WizardMSYR(FullStack, speciesRow);
            wizard.Show();
        }

        private void meniItemMSYUnspecified_Click(object sender, EventArgs e)
        {
            WizardMSY wizard = new WizardMSY();
            wizard.Show();
        }

        private void speciesMSY_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardMSY wizard = new WizardMSY(FullStack, speciesRow);
            wizard.Show();
        }

        private void meniItemPredictionUnspecified_Click(object sender, EventArgs e)
        {
            WizardPrediction wizard = new WizardPrediction();
            wizard.Show();
        }

        private void speciesPrediction_Click(object sender, EventArgs e)
        {
            TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag;
            WizardPrediction wizard = new WizardPrediction(FullStack, speciesRow);
            wizard.Show();
        }



        private void menuItemStockCumulative_Click(object sender, EventArgs e)
        {
            //WizardStock stockExplorer = new WizardStock((ToolStripMenuItem)sender, Data, StockExplorerVariant.CumulativeStock);
            //stockExplorer.Show();
        }

        private void menuItemTac_Click(object sender, EventArgs e)
        {
            WizardTac tacWizard = new WizardTac(FullStack);
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
            List<string> ext = new List<string>();
            ext.Add(Fish.UserSettings.Interface.Extension);
            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced) ext.Add(Wild.UserSettings.InterfaceBio.Extension);
            LoadCards(e.GetOperableFilenames(ext.ToArray()));
        }

        private void cards_DragEnter(object sender, DragEventArgs e)
        {
            List<string> ext = new List<string>();
            ext.Add(Fish.UserSettings.Interface.Extension);
            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced) ext.Add(Wild.UserSettings.InterfaceBio.Extension);
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

            comboBoxCatchesMesh.DataSource = FullStack.Classes(SelectedSamplerType);
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

            string[] meshValues = FullStack.Classes(SelectedSamplerType);

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
                    CatchesData = FullStack.GetStack(dateTimePickerCatches.Value);
                }
                else
                {
                    CatchesData = FullStack;
                }
            }
            else
            {
                if (checkBoxCatchesDaily.Checked)
                {
                    if (checkBoxCatchesMesh.Checked)
                    {
                        CatchesData = FullStack.GetStack(SelectedSamplerType, comboBoxCatchesMesh.SelectedValue.ToString())
                            .GetStack(dateTimePickerCatches.Value);
                    }
                    else
                    {
                        CatchesData = FullStack.GetStack(SelectedSamplerType)
                            .GetStack(dateTimePickerCatches.Value);
                    }
                }
                else
                {
                    if (checkBoxCatchesMesh.Checked)
                    {
                        CatchesData = FullStack.GetStack(SelectedSamplerType, comboBoxCatchesMesh.SelectedValue.ToString());
                    }
                    else
                    {
                        CatchesData = FullStack.GetStack(SelectedSamplerType);
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

            foreach (TaxonomicIndex.TaxonRow speciesRow in CatchesData.GetSpecies())
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
                FullStack.AddCommon(report);
                FullStack.Sort();
                FullStack.AddGearStatsReport(report);
                report.EndBranded();
                report.Run();
            }
            else
            {
                Report report = new Report(string.Format(Resources.Reports.Header.StatsGearType, SelectedSamplerType.ToDisplay()));
                CardStack gearStack = FullStack.GetStack(SelectedSamplerType);
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
            // Make a selection
            if (spreadSheetSpcStats.SelectedRows.Count == 0 ||
                !(spreadSheetSpcStats[ColumnSpcStat.Index, spreadSheetSpcStats.SelectedRows[0].Index].Value is TaxonomicIndex.TaxonRow row))
            {
                selectedStatSpc = null;
            }
            else
            {
                selectedStatSpc = row;
                plotQualify.ResetFormatted(selectedStatSpc.ToString("s"));

                resetQualPlotAxes(
                    Service.GetStrate(FullStack.LengthMin(selectedStatSpc)).LeftEndpoint,
                    Service.GetStrate(FullStack.LengthMax(selectedStatSpc)).RightEndpoint,
                    FullStack.GetLengthComposition(selectedStatSpc, UserSettings.SizeInterval).MostSampled.Quantity);
            }

            // Totals
            totals_Changed(sender, e);

            // Techniques
            tech_Changed(sender, e);

            // Strates
            labelQualNotSelected.Visible = selectedStatSpc == null;
            plotQualify.Visible = selectedStatSpc != null;

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

            SampleSizeDescriptor d = selectedStatSpc == null ? FullStack.GetDescriptor() : FullStack.GetDescriptor(selectedStatSpc);

            textBoxSpsTotal.Text = d.Quantity > 0 ? d.Quantity.ToString() : textBoxSpsTotal.Text = Constants.Null;

            textBoxSpcStrat.Text = d.QuantityStratified > 0 ? string.Format(valueMask, d.QuantityStratified, (double)d.QuantityStratified / (double)d.Quantity) : Constants.Null;
            textBoxSpcLog.Text = d.QuantityIndividual > 0 ? string.Format(valueMask, d.QuantityIndividual, (double)d.QuantityIndividual / (double)d.Quantity) : Constants.Null;

            textBoxSpcL.Text = d.Measured > 0 ? string.Format(valueMask, d.Measured, (double)d.Measured / (double)d.Quantity) : Constants.Null;
            textBoxSpcW.Text = d.Weighted > 0 ? string.Format(valueMask, d.Weighted, (double)d.Weighted / (double)d.Quantity) : Constants.Null;
            textBoxSpcTally.Text = d.Tallied > 0 ? string.Format(valueMask, d.Tallied, (double)d.Tallied / (double)d.Quantity) : Constants.Null;
            textBoxSpcA.Text = d.Aged > 0 ? string.Format(valueMask, d.Aged, (double)d.Aged / (double)d.Quantity) : Constants.Null;
            textBoxSpcS.Text = d.Sexed > 0 ? string.Format(valueMask, d.Sexed, (double)d.Sexed / (double)d.Quantity) : Constants.Null;
            textBoxSpcM.Text = d.Matured > 0 ? string.Format(valueMask, d.Matured, (double)d.Matured / (double)d.Quantity) : Constants.Null;

            textBoxSpcWTotal.Text = d.Mass > 0 ? d.Mass.ToString("N3") : Constants.Null;
            textBoxSpcWLog.Text = selectedStatSpc == null ? Constants.Null : (d.MassIndividual.ToString("N3"));
            textBoxSpcWStrat.Text = selectedStatSpc == null ? Constants.Null : (d.MassStratified > 0 ? d.MassStratified.ToString("N3") : Constants.Null);

            buttonSpcStrat.Enabled = d.Quantity > 0;
            buttonSpcLog.Enabled = d.QuantityIndividual > 0;
            buttonSpcL.Enabled = d.Measured > 0;
            buttonSpcW.Enabled = d.Weighted > 0;
            buttonSpcTally.Enabled = d.Tallied > 0;
            buttonSpcA.Enabled = d.Aged > 0;
            buttonSpcS.Enabled = d.Sexed > 0;
            buttonSpcM.Enabled = d.Matured > 0;

            if (selectedStatSpc == null)
            {
                chartSpcStats.Series[0].Points.Clear();
                chartSpcStats.Legends[0].Enabled = true;
                foreach (TaxonomicIndex.TaxonRow speciesRow in FullStack.GetSpecies())
                {
                    DataPoint dp = new DataPoint();
                    dp.YValues[0] = FullStack.Quantity(speciesRow);
                    dp.LegendText = speciesRow.ToString(ColumnSpcStat.DefaultCellStyle.Format);
                    chartSpcStats.Series[0].Points.Add(dp);
                }

                chartSpcStats.Series[0].Sort(PointSortOrder.Descending);

                Color startColor = Mathematics.UserSettings.ColorAccent;

                foreach (DataPoint dp in chartSpcStats.Series[0].Points)
                {
                    dp.Color = startColor;
                    startColor = startColor.Lighter();
                }
            }
            else
            {
                chartSpcStats.Legends[0].Enabled = false;
                chartSpcStats.Series[0].AnimateChart(d.Quantity, FullStack.Quantity(), Mathematics.UserSettings.ColorAccent);
            }
        }

        private void buttonSpcStrat_Click(object sender, EventArgs e)
        {
            if (selectedStatSpc == null)
            {
                if (data.Stratified.Count == 0) return;
                menuItemLoadStratified_Click(sender, e);
            }
            else
            {
                if (FullStack.QuantityStratified(selectedStatSpc) == 0) return;

                spreadSheetStratified.EnsureFilter(columnStratifiedSpc,
                    selectedStatSpc.Name, loaderStratified, menuItemLoadStratified_Click);
            }
        }

        private void buttonSpcLog_Click(object sender, EventArgs e)
        {
            loadIndividuals(selectedStatSpc);
        }

        private void buttonSpcL_Click(object sender, EventArgs e)
        {
            getFilteredList(columnIndLength);
        }

        private void buttonSpcW_Click(object sender, EventArgs e)
        {
            getFilteredList(columnIndMass);
        }

        private void buttonSpcTally_Click(object sender, EventArgs e)
        {
            getFilteredList(columnIndTally);
        }

        private void buttonSpcA_Click(object sender, EventArgs e)
        {
            getFilteredList(columnIndAge);
        }

        private void buttonSpcS_Click(object sender, EventArgs e)
        {
            getFilteredList(columnIndSex);
        }

        private void buttonSpcM_Click(object sender, EventArgs e)
        {
            getFilteredList(columnIndMaturity);
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
            comboBoxQualClass.DataSource = FullStack.Classes(((FishSamplerTypeDisplay)comboBoxQualType.SelectedValue).Type);

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

            CardStack countStack = FullStack;

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
                (s, i) => { return countStack.Tallied(s, i); }, Resources.Interface.StratesRegistered).GetHistogramSample();

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
                    model = data.FindMassModel(selectedStatSpc.Name);
                    break;

                case DataQualificationWay.AgeOfLength:
                    model = data.FindGrowthModel(selectedStatSpc.Name);
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
            spreadSheetInd.StartProcessing(Wild.Resources.Interface.Process.LoadInd);
            spreadSheetInd.Rows.Clear();

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();

            List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

            foreach (var pair in outliersData)
            {
                indRows.AddRange(FullStack.GetIndividuals(selectedStatSpc,
                    (selectedQualificationWay == 0 ? new string[] { "Length", "Mass" } : new string[] { "Age", "Length" }),
                    new object[] { pair.X, pair.Y }));
            }

            loadIndividuals(indRows.ToArray());

            //BackgroundWorker outlierEditor = new BackgroundWorker();
            //outlierEditor.DoWork += outlierEditor_DoWork;
            ////outlierEditor.RunWorkerCompleted += new RunWorkerCompletedEventHandler(indLoader_RunWorkerCompleted);
            //outlierEditor.RunWorkerAsync();
        }

        //private void outlierEditor_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

        //    foreach (var pair in outliersData)
        //    {
        //        indRows.AddRange(FullStack.GetIndividuals(selectedStatSpc,
        //            (selectedQualificationWay == 0 ? new string[] { "Length", "Mass" } : new string[] { "Age", "Length" }),
        //            new object[] { pair.X, pair.Y }));
        //    }

        //    loadIndividuals(indRows.ToArray());
        //}

        #endregion

        private void buttonSpeciesStatsReport_Click(object sender, EventArgs e)
        {
            buttonSpeciesStatsReport.Enabled = false;

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork; 
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
            Cursor = Cursors.AppStarting;
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Report report = (Report)e.Result;
            report.Run();

            buttonSpeciesStatsReport.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            SpeciesStatsLevel lvl = SpeciesStatsLevel.Totals | SpeciesStatsLevel.Detailed | SpeciesStatsLevel.TreatmentSuggestion | SpeciesStatsLevel.SurveySuggestion;

            Report report = new Report(selectedStatSpc == null ?
                Resources.Reports.Sections.SpeciesStats.Title :
                string.Format(Resources.Reports.Sections.SpeciesStats.TitleSpecies, selectedStatSpc.FullNameReport));
            FullStack.Sort();

            if (selectedStatSpc == null)
            {
                FullStack.AddCommon(report);
                FullStack.AddSpeciesStatsReport(report, lvl, ExpressionVariant.Efforts);
                report.EndBranded();
            }
            else
            {
                FullStack.AddCommon(report, selectedStatSpc);
                FullStack.AddSpeciesStatsReport(report, selectedStatSpc, lvl, ExpressionVariant.Efforts);
            }

            report.EndBranded();
            e.Result = report;
        }

        #endregion




        #region Cards

        private void menuItemCardMeteo_CheckedChanged(object sender, EventArgs e)
        {
            columnCardWeather.Visible =
            columnCardTempSurface.Visible =
                menuItemCardMeteo.Checked;
        }

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

            Composition composition;

            if (rank == null)
            {
                composition = FullStack.GetBasicCenosisComposition();
            }
            else
            {
                composition = FullStack.GetBasicTaxonomicComposition(Fish.UserSettings.SpeciesIndex, rank);
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

                gridRow.Cells[columnSpcOccurrence.Index].Value = composition[i].Occurrence;

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

            tabPageSpc.Parent = tabControl;
            tabControl.SelectedTab = tabPageSpc;
        }



        private void comboBoxSpcTaxon_SelectedIndexChanged(object sender, EventArgs e)
        {
            rank = comboBoxSpcTaxon.SelectedItem as TaxonomicRank;

            menuItemSpcTaxon.Enabled = rank == null;

            if (rank != null)
            {
                spreadSheetSpc.ClearInsertedColumns();
            }

            columnSpcSpc.HeaderText = rank == null ? Wild.Resources.Reports.Caption.Species : rank.ToString();

            loadSpc();
        }

        private void contextSpecies_Opening(object sender, CancelEventArgs e)
        {
            contextSpecies.Enabled = rank == null;

            bool hasStratified = false;
            bool hasSampled = false;

            foreach (TaxonomicIndex.TaxonRow spcRow in getSpeciesRows(spreadSheetSpc.SelectedRows))
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
                    new object[] { logRow.DefinitionRow.Taxon });
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

            columnLogDiversityA.Visible = (rank != null);
            columnLogDiversityB.Visible = (rank != null);

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
                processDisplay.StartProcessing(Wild.Resources.Interface.Process.ExtLog);
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
            foreach (DataGridViewRow gridRow in (DataGridViewRow[])e.Argument)
            {
                if (UserSettings.SuggestAge) setIndividualAgeTip(gridRow);
                if (UserSettings.SuggestMass) setIndividualMassTip(gridRow);
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
                Data.LogRow[] logRows = FullStack.GetLogRows(individualSpecies);

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
                    else { bioUpdater.RunWorkerAsync(editedIndividualRow.LogRow.DefinitionRow); }
                }

                // If mass was changed
                if (e.ColumnIndex == columnIndMass.Index)
                {
                    // Reset Total mass status text
                    statusMass.ResetFormatted(FullStack.Mass());
                    labelWgtValue.Text = Wild.Service.GetFriendlyMass(FullStack.Mass() * 1000);
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
            Data.DefinitionRow speciesRow = (Data.DefinitionRow)e.Argument;
            data.FindMassModel(speciesRow.Taxon).RefreshInternal();
            data.FindGrowthModel(speciesRow.Taxon).RefreshInternal();
            e.Result = speciesRow;
        }

        private void bioUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                bioUpdater.RunWorkerAsync(e.Result);
                return;
            }
            else if (selectedStatSpc == (TaxonomicIndex.TaxonRow)e.Result)
            {
                qualCalc.RunWorkerAsync((TaxonomicIndex.TaxonRow)e.Result);
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
                FullStack.QuantityStratified(Fish.UserSettings.SpeciesIndex.FindByName(speciesNames[0])) > 0;

            if (pictureBoxStratified.Visible)
            {
                toolTipAttention.SetToolTip(pictureBoxStratified,
                    string.Format((new ResourceManager(typeof(MainForm))).GetString(
                    "pictureBoxStratified.ToolTip"), speciesNames[0]));
            }
        }

        private void spreadSheetInd_DisplayChanged(object sender, EventArgs e)
        {
            if (!bioTipper.IsBusy)
            {
                DataGridViewRow[] gridRows = spreadSheetInd.GetDisplayedRows().ToArray();
                processDisplay.StartProcessing(Wild.Resources.Interface.Process.BioApply);
                bioTipper.RunWorkerAsync(gridRows);
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
            //        new object[] { logRow.DefinitionRow.Name });

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
            spreadSheetInd.StartProcessing(Resources.Process.DietCompiling);
            dietCompiler.RunWorkerAsync(spreadSheetInd.SelectedRows);
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
            spreadSheetInd.StartProcessing(Resources.Process.DietCompiling);
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
            dietExplorer.PerformDietExplorer(FullStack.FriendlyName);
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

        private void menuItemStratifiedPrint_Click(object sender, EventArgs e)
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
                    gridRow.Cells[columnStratifiedSpc.Index].Value = logRows[i].DefinitionRow;
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
                spreadSheetStratified.StartProcessing(Wild.Resources.Interface.Process.ExtStrat);
                loaderStratifiedExtended.RunWorkerAsync();
            }
        }

        #endregion
    }
}