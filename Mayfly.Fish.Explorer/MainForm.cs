﻿using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer.Fishery;
using Mayfly.Fish.Explorer.Observations;
using Mayfly.Fish.Explorer.Survey;
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

namespace Mayfly.Fish.Explorer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Licensing.SetTabsAvailability("Fishery Scientist",
                tabPageSpcLW, tabPageSpcAL);

            Licensing.SetMenuAvailability("Fishery Scientist",
                menuItemExportSpec,
                menuItemImportSpec, 
                toolStripSeparator11,
                menuItemGearStats,
                menuItemSpcStats,
                menuItemArtefactsSearch, 
                toolStripSeparator2,
                menuItemComCom,
                menuSurvey,
                menuFishery,
                menuItemCardCatches,
                menuItemCardEfforts,
                toolStripSeparator14,
                menuItemSpcTotals,
                menuItemSpcCpues,
                menuItemSpcStrates,
                menuItemSpcModels,
                toolStripSeparator13,
                menuModels,
                menuItemMortality,
                menuItemSelectivity,
                menuItemVpa,
                menuItemGrowth);

            Licensing.SetControlsAvailability("Fishery Scientist",
                buttonCardDetails,
                buttonSpcDetails);


            Fish.UserSettings.Interface.OpenDialog.Multiselect = true;

            listViewWaters.Shine();
            listViewSamplers.Shine();
            listViewInvestigators.Shine();

            spreadSheetArtefactCard.UpdateStatus();
            spreadSheetArtefactSpecies.UpdateStatus();
            spreadSheetArtefactInd.UpdateStatus();

            spreadSheetCard.UpdateStatus();
            spreadSheetSpc.UpdateStatus();
            spreadSheetLog.UpdateStatus();
            spreadSheetInd.UpdateStatus();
            spreadSheetStratified.UpdateStatus();

            selectedLogRows = new List<Data.LogRow>();

            tabPageGearStats.Parent = null;

            //tabPageCardSchedule.Parent = null;

            columnCardWater.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardWhere.ValueType = typeof(Waypoint);
            ColumnCardWeather.ValueType = typeof(Weather);
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

            columnSpcSpc.ValueType = typeof(string);
            columnSpcValid.ValueType = typeof(string);
            columnSpcQuantity.ValueType = typeof(double);
            columnSpcMass.ValueType = typeof(double);
            columnSpcOccurrence.ValueType = typeof(double);

            LoadTaxaList();

            tabPageSpc.Parent = null;
            tabPageSpcStats.Parent = null;

            columnLogSpc.ValueType = typeof(string);
            columnLogQuantity.ValueType = typeof(int);
            columnLogMass.ValueType = typeof(double);
            columnLogAbundance.ValueType = typeof(double);
            columnLogBiomass.ValueType = typeof(double);
            columnLogDiversityA.ValueType = typeof(double);
            columnLogDiversityB.ValueType = typeof(double);

            tabPageLog.Parent = null;

            columnIndSpecies.ValueType = typeof(string);
            columnIndLength.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndSomaticMass.ValueType = typeof(double);
            columnIndCondition.ValueType = typeof(double);
            columnIndConditionSoma.ValueType = typeof(double);
            columnIndRegID.ValueType = typeof(string);
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

            columnStratifiedSpc.ValueType = typeof(string);
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

            columnArtefactIndRegID.ValueType = typeof(string);
            columnArtefactIndDietUnw.ValueType = typeof(int);

            tabPageArtefacts.Parent = null;

            chartSchedule.Format();

            plotStrates.Series["Limiter"].LegendText = Resources.Interface.Interface.EnoughStamp;
            plotStrates.Series["Limiter"].Points[0].YValues[0] = UserSettings.RequiredClassSize;
            plotStrates.Series["Limiter"].Points[1].YValues[0] = UserSettings.RequiredClassSize;

            plotLW.AxisXInterval = UserSettings.SizeInterval;
            plotLW.Series["Limiter"].LegendText = Resources.Interface.Interface.EnoughStamp;
            plotLW.Series["Limiter"].Points[0].YValues[0] = UserSettings.RequiredClassSize;
            plotLW.Series["Limiter"].Points[1].YValues[0] = UserSettings.RequiredClassSize;

            plotAL.Series["Limiter"].LegendText = Resources.Interface.Interface.EnoughStamp;
            plotAL.Series["Limiter"].Points[0].YValues[0] = UserSettings.RequiredClassSize;
            plotAL.Series["Limiter"].Points[1].YValues[0] = UserSettings.RequiredClassSize;

            data = new Data();
            FullStack = new CardStack(); //.ConvertFrom(data);
            AllowedStack = new CardStack();

            if (Licensing.Verify("Fishery Scientist"))
            {
                data.InitializeBio();
                data.MassModels.VisualConfirmation =
                    data.GrowthModels.VisualConfirmation =
                    UserSettings.VisualConfirmation;

                if (UserSettings.AutoLoadBio)
                {
                    processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.SpecLoading);
                    LoadCards(UserSettings.Bios);
                }
            }

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

            UpdateSummary();
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateSummary();
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

        //private void cardFile_Changed(object sender, FileSystemEventArgs e)
        //{
        //    //if (true)
        //    //{
        //    //    Data.CardRow interestCard = null;

        //    //    foreach (Data.CardRow cardRow in Data.Card)
        //    //    {
        //    //        if (cardRow.Path == null) continue;

        //    //        if (cardRow.Path == e.FullPath) {
        //    //            interestCard = cardRow;
        //    //        }
        //    //    }

        //    //    if (interestCard != null)
        //    //    {
        //    //        Data data = new Data();

        //    //        if (data.Read(e.FullPath))
        //    //        {
        //    //            if (data.Card.Count == 0)
        //    //                Console.WriteLine(string.Format("File is empty: {0}.", e.FullPath));
        //    //            else
        //    //            {
        //    //                Data.CardRow importedCardRow = data.ImportTo(Data)[0];

        //    //                if (tabPageCard.Parent != null)
        //    //                {
        //    //                    UpdateCardRow(importedCardRow, columnCardID.GetRow(interestCard.ID));
        //    //                }

        //    //                if (tabPageInd.Parent != null)
        //    //                {
        //    //                    //UpdateIndividualRow(, );
        //    //                }
        //    //            }
                        
        //    //            Data.Card.RemoveCardRow(interestCard);
        //    //        }
        //    //    }
                
        //    //}
        //}


        private void tab_Changed(object sender, EventArgs e)
        {
            menuCards.Visible = (tabControl.SelectedTab == tabPageCard);
            menuSpc.Visible = (tabControl.SelectedTab == tabPageSpc);
            menuLog.Visible = (tabControl.SelectedTab == tabPageLog);
            menuIndividuals.Visible = (tabControl.SelectedTab == tabPageInd);
            stratifiedSamplesToolStripMenuItem.Visible = (tabControl.SelectedTab == tabPageStratified);
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }


        //DateTime started = DateTime.Now;
        //TimeSpan ts = TimeSpan.Zero;

        private void dataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            //started = DateTime.Now;
            
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

                    data.RefreshBios();

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
                            Log.Write(string.Format("File is empty: {0}.", filenames[i]));
                        else
                        {
                            foreach (Data.CardRow cardRow in _data.Card)
                            {
                                cardRow.SamplerPresentation = cardRow.IsSamplerNull() ? Constants.Null : cardRow.GetSamplerSign();
                            }

                            Data.CardRow[] cardRows = _data.CopyTo(data);
                        }
                    }
                }

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }
        }

        private void dataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //ts = DateTime.Now - started;
            //MessageBox.Show(string.Format("Loaded in {0}.", ts));

            spreadSheetCard.StopProcessing();
            UpdateSummary();
        }


        private void modelCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Licensing.Verify("Fishery Scientist"))
            {
                e.Cancel = true;
                return;
            }

            int i = 0;
            foreach (Data.SpeciesRow speciesRow in data.Species.GetSorted())
            {
                data.GrowthModels.Refresh(speciesRow.Species);
                data.MassModels.Refresh(speciesRow.Species);

                i++;
                ((BackgroundWorker)sender).ReportProgress(i);
            }
        }

        private void modelCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                double q = FullStack.Quantity();
                double w = FullStack.Mass();

                statusQuantity.ResetFormatted(Wild.Service.GetFriendlyQuantity((int)q));
                statusMass.ResetFormatted(Wild.Service.GetFriendlyMass(w * 1000));

                //if (tabPageSpcModels.Parent != null) {
                species_Changed(spreadSheetSpcStats, new EventArgs());
                //}

                if (tabPageInd.Parent != null)
                {
                    processDisplay.StartProcessing(spreadSheetInd.RowCount, Wild.Resources.Interface.Process.SpecApply);
                    specTipper.RunWorkerAsync();
                }

                processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.ArtefactsProcessing);
                artefactFinder.RunWorkerAsync();
            }
            else
            {
                processDisplay.StopProcessing();
                IsBusy = false;
            }
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
                    //if (_data != null) // If data is null AND its file contain some info?
                    //{ 
                    _data.WriteToFile(cardRow.Path);
                    //}
                }

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
                    Data _data = cardRow.SingleCardDataset();
                    string filename = FileSystem.SuggestName(fbdBackup.SelectedPath, _data.GetSuggestedName());
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
            WizardExport2 exportWizard = new WizardExport2(data);
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
            menuItemLoadStratified.Enabled = data.Stratified.Count > 0;
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

            spreadSheetSpcStats.Rows.Clear();
            foreach (Data.SpeciesRow speciesRow in AllowedStack.GetSpecies())
            {
                spreadSheetSpcStats.Rows.Add(speciesRow);
            }
            spreadSheetSpcStats.Rows.Add(Mayfly.Resources.Interface.Total);


            comboBoxCpueSampler.DataSource = AllowedStack.GetSamplerTypeDisplays();
            comboBoxCpueSampler.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxCpueSampler.DataSource)[0];
            comboBoxCpueSampler_SelectedIndexChanged(comboBoxCpueSampler, e);

            comboBoxTechSampler.DataSource = AllowedStack.GetSamplerTypeDisplays();
            comboBoxTechSampler.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxCpueSampler.DataSource)[0];

            comboBoxStrateGearType.DataSource = AllowedStack.GetSamplerTypeDisplays();
            comboBoxStrateGearType.SelectedItem = ((FishSamplerTypeDisplay[])comboBoxStrateGearType.DataSource)[0];

            ClearSpcStats();
            species_Changed(spreadSheetSpcStats, e);
        }

        private void menuItemArtefacts_Click(object sender, EventArgs e)
        {
            showArtefacts = true;
            processDisplay.StartProcessing(100, Wild.Resources.Interface.Process.ArtefactsProcessing);
            artefactFinder.RunWorkerAsync();
        }

        //private void menuItemStratifiedControl_Click(object sender, EventArgs e)
        //{
        //    StratifiedControl sampleControl = new StratifiedControl(data);
        //    sampleControl.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
        //    sampleControl.Show();
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

        private void menuItemIndAll_Click(object sender, EventArgs e)
        {
            LoadIndLog();
        }

        private void menuItemLoadStratified_Click(object sender, EventArgs e)
        {
            tabPageStratified.Parent = tabControl;
            tabControl.SelectedTab = tabPageStratified;

            LoadStratifiedSamples();
        }

        #endregion

        #region Survey

        private void menuItemSurveyInput_Click(object sender, EventArgs e)
        {
            WizardOb wizOb = new WizardOb();
            wizOb.Explorer = this;
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

        private void menuItemGearClass_Click(object sender, EventArgs e)
        {
            WizardGearClass selectivityWizard = new WizardGearClass(AllowedStack);
            selectivityWizard.Show();
        }

        private void menuItemCommunity_Click(object sender, EventArgs e)
        {
            WizardCommunity wizard = new WizardCommunity(AllowedStack);
            wizard.Show();
        }



        private void speciesGrowth_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardGrowth wizard = new WizardGrowth(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesGrowthCohorts_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardGrowthCohorts wizard = new WizardGrowthCohorts(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesComposition_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardStockComposition wizard = new WizardStockComposition(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesMortality_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardMortality wizard = new WizardMortality(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesMortalityCohorts_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardMortalityCohorts wizard = new WizardMortalityCohorts(AllowedStack, speciesRow);
            wizard.Show();
        }

        private void speciesSelectivity_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            WizardSelectivity wizard = new WizardSelectivity(AllowedStack, speciesRow);
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
                labelCards.ForeColor = labelCardsValue.ForeColor = Color.LightGray;
            }
        }

        private void cards_DragLeave(object sender, EventArgs e)
        {
            labelCards.ForeColor = labelCardsValue.ForeColor = SystemColors.ControlText;
        }
        

        private void labelRateValue_SizeChanged(object sender, EventArgs e)
        {
            //labelRateValue.CenterWith(chartRate);

            //labelRate.Left =
            //    labelRateValue.Left + 10;
        }

        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardWater, listViewWaters.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        private void listViewSamplers_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardGear, listViewSamplers.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e)
        {
            spreadSheetCard.EnsureFilter(columnCardInvestigator, listViewInvestigators.FocusedItem.Text, loaderCard, menuItemLoadCards_Click);
        }  

        #endregion        

        #region Artefacts

        private void spreadSheetArtefactSpecies_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //switch (e.ColumnIndex)
            //{
            //    case 4: // Weight runouts
            //    case 5: // Age runouts

            //        if (spreadSheetInd.RowCount > 0)
            //        {
            //            menuItemIndInputControl_Click(sender, e);

            //            if (e.ColumnIndex == columnArtefactWeight.Index)
            //            {
            //                inputControl.SetParameters(
            //                    spreadSheetArtefactSpecies[columnArtefactSpecies.Index, e.RowIndex].Value.ToString(),
            //                    columnIndLength, columnIndMass);
            //            }

            //            if (e.ColumnIndex == columnArtefactAge.Index)
            //            {
            //                inputControl.SetParameters(
            //                    spreadSheetArtefactSpecies[columnArtefactSpecies.Index, e.RowIndex].Value.ToString(),
            //                    columnIndLength, columnIndAge);
            //            }
            //        }
            //        break;
            //}
        }

        private void contextArtCardOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetArtefactCard.SelectedRows)
            {
                Data.CardRow cardRow = (Data.CardRow)gridRow.Cells[columnArtCardName.Index].Value;
                FileSystem.RunFile(cardRow.Path);
            }
        }

        private void contextArtSpecies_Opening(object sender, CancelEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetArtefactSpecies.SelectedRows)
            {
                string species = (string)gridRow.Cells[columnArtefactSpecies.Index].Value;
                Data.SpeciesRow speciesRow = data.Species.FindBySpecies(species);

                contextArtSpeciesFindInd.Enabled = AllowedStack.QuantityIndividual(speciesRow) > 0;
                contextArtSpeciesFindStratified.Enabled = AllowedStack.QuantityStratified(speciesRow) > 0;
            }
        }

        private void contextArtSpeciesModels_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetArtefactSpecies.SelectedRows)
            {
                string species = (string)gridRow.Cells[columnArtefactSpecies.Index].Value;
                Data.SpeciesRow speciesRow = data.Species.FindBySpecies(species);

                //if (tabPageSpcModels.Parent == tabControlSpc) {
                    tabControlSpc.SelectedTab = tabPageSpcAL;
                //} else {
                //    menuItemSpcModels_Click(sender, e);
                //}

                spreadSheetSpcStats.ClearSelection();
                for (int i = 0; i < spreadSheetSpcStats.RowCount; i++)
                {
                    if (spreadSheetSpcStats[ColumnSpcStat.Index, i].Value == speciesRow)
                    {
                        spreadSheetSpcStats.Rows[i].Selected = true;
                        break;
                    }
                }

                //sortableSpecies.SelectedItems.Clear();
                //sortableSpecies.FindItemWithText(speciesRow.Species).Selected = true;
            }
        }

        private void contextArtSpeciesFindIndividuals_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetArtefactSpecies.SelectedRows)
            {
                string species = (string)gridRow.Cells[columnArtefactSpecies.Index].Value;
                Data.SpeciesRow speciesRow = data.Species.FindBySpecies(species);
                LoadIndLog(speciesRow);
            }
        }

        private void contextArtSpeciesFindStratified_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetArtefactSpecies.SelectedRows)
            {
                string species = (string)gridRow.Cells[columnArtefactSpecies.Index].Value;
                spreadSheetStratified.EnsureFilter(columnStratifiedSpc, species, loaderStratified, menuItemLoadStratified_Click);
            }
        }

        private void artefactFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Licensing.Verify("Fishery Scientist"))
            {
                e.Cancel = true;
                return;
            }

            e.Result = AllowedStack.GetArtefacts(true);
        }

        private void artefactFinder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                int count = 0;

                foreach (Artefact artefact in (Artefact[])e.Result)
                {
                    count += artefact.GetFacts();
                }

                labelArtefacts.Visible = pictureBoxArtefacts.Visible = count > 0;

                if (count > 0)
                {
                    if (showArtefacts)
                    {
                        List<CardArtefact> cardArtefacts = new List<CardArtefact>();
                        List<SpeciesArtefact> spcArtefacts = new List<SpeciesArtefact>();
                        List<IndividualArtefact> indArtefacts = new List<IndividualArtefact>();

                        foreach (Artefact artefact in (Artefact[])e.Result)
                        {
                            if (artefact is CardArtefact)
                                cardArtefacts.Add((CardArtefact)artefact);

                            if (artefact is SpeciesArtefact)
                                spcArtefacts.Add((SpeciesArtefact)artefact);

                            if (artefact is IndividualArtefact)
                                indArtefacts.Add((IndividualArtefact)artefact);
                        }

                        tabPageArtefacts.Parent = tabControl;
                        tabControl.SelectedTab = tabPageArtefacts;

                        if (cardArtefacts.Count == 0)
                        {
                            tabPageArtefactCards.Parent = null;
                        }
                        else
                        {
                            tabPageArtefactCards.Parent = tabControlArtefacts;
                            ShowCardArtefacts(cardArtefacts.ToArray());
                        }

                        if (spcArtefacts.Count == 0)
                        {
                            tabPageArtefactSpecies.Parent = null;
                        }
                        else
                        {
                            tabPageArtefactSpecies.Parent = tabControlArtefacts;
                            ShowSpeciesArtefacts(spcArtefacts.ToArray());
                        }

                        if (indArtefacts.Count == 0)
                        {
                            tabPageArtefactInds.Parent = null;
                        }
                        else
                        {
                            tabPageArtefactInds.Parent = tabControlArtefacts;
                            ShowIndividualArtefacts(indArtefacts.ToArray());
                        }

                        showArtefacts = false;
                    }
                    else
                    {
                        Notification.ShowNotification(
                            Wild.Resources.Interface.Messages.ArtefactsNotification,
                            Wild.Resources.Interface.Messages.ArtefactsNotificationInstruction,
                            menuItemArtefacts_Click);
                    }
                }
                else
                {
                    if (showArtefacts)
                    {
                        Notification.ShowNotification(Wild.Resources.Interface.Messages.ArtefactsNoneNotification,
                            Wild.Resources.Interface.Messages.ArtefactsNoneNotificationInstruction);
                        showArtefacts = false;
                    }
                }
            }

            IsBusy = false;
            processDisplay.StopProcessing();
        }

        #endregion

        #region Cards

        private void menuItemCardFindEmpty_Click(object sender, EventArgs e)
        {
            spreadSheetCard.ClearSelection();
            foreach (DataGridViewRow gridRow in spreadSheetCard.Rows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                gridRow.Selected = cardRow.GetLogRows().Length == 0;
            }
        }

        private void menuItemCardFindSpaced_Click(object sender, EventArgs e)
        {
            spreadSheetCard.ClearSelection();
            foreach (DataGridViewRow gridRow in spreadSheetCard.Rows)
            {
                Data.CardRow cardRow = CardRow(gridRow);

                int logQuantity = 0;
                int individualsQuantity = 0;
                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    logQuantity += logRow.Quantity;
                    individualsQuantity += logRow.DetailedQuantity;
                }

                gridRow.Selected = logQuantity != individualsQuantity;
            }
        }

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Stratified | CardReportLevel.Individuals).Run();
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

            for (int i = 0; i < data.Card.Count; i++)
            {
                result.Add(GetLine(data.Card[i]));
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
        }



        private void spreadSheetCard_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void spreadSheetCard_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetCard.ContainsFocus)
            {
                SaveCardRow(spreadSheetCard.Rows[e.RowIndex]);

                if (effortSource.Contains(spreadSheetCard.Columns[e.ColumnIndex]))
                {
                    spreadSheetCard[columnCardEffort.Index, e.RowIndex].Value =
                        CardRow(spreadSheetCard.Rows[e.RowIndex]).GetEffort();
                }
            }
            
            if (spreadSheetCard.Columns[e.ColumnIndex].ValueType == typeof(Waypoint))
            {
                SaveCardRow(spreadSheetCard.Rows[e.RowIndex]);
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
                Samplers.SamplerRow samplerRow = cardRow.GetSamplerRow();
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

        }

        private void contextCardOpen_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                FileSystem.RunFile(cardRow.Path);
            }
        }

        private void contextCardExplore_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();

            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows)
            {
                Data.CardRow cardRow = CardRow(gridRow);
                if (!filenames.Contains("\"" + cardRow.Path + "\"")) filenames.Add("\"" + cardRow.Path + "\"");
            }

            FileSystem.RunFile(Application.ExecutablePath, filenames.ToArray());
            //MainForm newMain = new MainForm(filenames.ToArray());
            //newMain.Show();
        }

        private void contextCardOpenFolder_Click(object sender, EventArgs e)
        {
            List<string> directories = new List<string>();

            foreach (DataGridViewRow gridRow in spreadSheetCard.SelectedRows) {
                if (spreadSheetCard.IsHidden(gridRow)) continue;
                Data.CardRow cardRow = CardRow(gridRow);
                string dir = Path.GetDirectoryName(cardRow.Path);
                if (!directories.Contains(dir)) {
                    directories.Add(dir);
                }
            }

            foreach (string dir in directories) {
                FileSystem.RunFile(dir);
            }
        }

        private void contextCardPrintNotes_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e)
        {
            GetStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Stratified | CardReportLevel.Individuals).Run();
        }

        #endregion

        #region Gear stats

        private void gear_Changed(object sender, EventArgs e)
        {
            if (spreadSheetGearStats.SelectedRows.Count == 0 ||
                !(spreadSheetGearStats[ColumnGearStats.Index, spreadSheetGearStats.SelectedRows[0].Index].Value is FishSamplerTypeDisplay))
            {
                SelectedSamplerType = FishSamplerType.None;
            }
            else
            {
                SelectedSamplerType = ((FishSamplerTypeDisplay)spreadSheetGearStats[ColumnGearStats.Index, spreadSheetGearStats.SelectedRows[0].Index].Value).Type;
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
                    if (htResult.Series.Points[htResult.PointIndex].Tag is Data.CardRow)
                    {
                        Data.CardRow cardRow = (Data.CardRow)htResult.Series.Points[htResult.PointIndex].Tag;
                        FileSystem.RunFile(cardRow.Path);
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
                    gridRow.Cells[columnCatchSpc.Index].Value = speciesRow.Species;
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
                Report report = new Report(Resources.Reports.GearStats.Title);
                AllowedStack.AddCommon(report);
                AllowedStack.Sort();
                AllowedStack.AddGearStatsReport(report);
                report.EndBranded();
                report.Run();
            }
            else
            {
                Report report = new Report(string.Format(Resources.Reports.GearStats.Title_1, SelectedSamplerType.ToDisplay()));
                CardStack gearStack = AllowedStack.GetStack(SelectedSamplerType);
                gearStack.AddCommon(report);
                gearStack.Sort();
                gearStack.AddGearStatsReport(report, SelectedSamplerType, SelectedEffortUE);
                report.EndBranded();
                report.Run();
            }
        }

        #endregion

        #region Species

        private void menuItemSpcValidate_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (Mayfly.Species.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    speciesValidator.IndexPath = Mayfly.Species.UserSettings.Interface.OpenDialog.FileName;
                }
            }
            else
            {
                speciesValidator.IndexPath = Fish.UserSettings.SpeciesIndexPath;
            }

            IsBusy = true;
            spreadSheetSpc.StartProcessing(data.Species.Count, Wild.Resources.Interface.Process.SpeciesProcessing);

            columnSpcValid.Visible = true;

            loaderSpcList.RunWorkerAsync();
        }

        private void menuItemSpcTotals_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            tabControlSpc.SelectedTab = tabPageSpcTotals;
        }

        private void menuItemSpcCpues_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            tabControlSpc.SelectedTab = tabPageSpcCpue;
        }

        private void menuItemSpcStrates_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            tabControlSpc.SelectedTab = tabPageSpcStrates;
        }

        private void menuItemSpcModels_Click(object sender, EventArgs e)
        {
            menuItemSpcStats_Click(sender, e);
            tabControlSpc.SelectedTab = tabPageSpcAL;
        }



        private void SpcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (baseSpc == null)
            {
                Composition speciesComposition = AllowedStack.GetCommunityComposition();
                for (int i = 0; i < speciesComposition.Count; i++)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();

                    gridRow.CreateCells(spreadSheetSpc);
                    gridRow.Cells[columnSpcSpc.Index].Value = speciesComposition[i].Name;
                    gridRow.Cells[columnSpcQuantity.Index].Value = speciesComposition[i].Quantity;
                    gridRow.Cells[columnSpcMass.Index].Value = speciesComposition[i].Mass;
                    gridRow.Cells[columnSpcOccurrence.Index].Value = speciesComposition[i].Occurrence;
                    result.Add(gridRow);

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }
            else
            {
                for (int i = 0; i < taxaSpc.Length; i++)
                {
                    result.Add(GetLine(taxaSpc[i]));
                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }

            e.Result = result.ToArray();
        }

        private void SpcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSpc.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
            spreadSheetSpc.UpdateStatus();
        }



        private void spcListLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
            {
                if (gridRow.Cells[columnSpcSpc.Index].Value == null)
                {
                    continue;
                }

                if (gridRow.Cells[columnSpcSpc.Index].Value as string == Species.Resources.Interface.UnidentifiedTitle)
                {
                    continue;
                }

                SpeciesKey.SpeciesRow speciesRow = speciesValidator.Find((string)gridRow.Cells[columnSpcSpc.Index].Value);

                if (speciesRow == null)
                {
                    gridRow.Cells[columnSpcValid.Index].Value = null;
                }
                else
                {
                    gridRow.Cells[columnSpcValid.Index].Value = speciesRow.FullName;
                }
            }
        }

        private void spcListLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
        }
        


        private void comboBoxSpcTaxa_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseSpc = comboBoxSpcTaxa.SelectedItem as SpeciesKey.BaseRow;
            taxaSpc = baseSpc == null ? null : data.Species.Taxa(baseSpc);
            LoadSpc();

            menuItemSpcValidate.Enabled =
                menuItemSpcTaxa.Enabled =
                baseSpc == null;
        }

        private void BaseItem_Click(object sender, EventArgs e)
        {
            SpeciesKey.BaseRow baseRow = ((ToolStripMenuItem)sender).Tag as SpeciesKey.BaseRow;
            DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(baseRow.Base,
                baseRow.Base, typeof(string), 0);

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

                SpeciesKey.SpeciesRow speciesRow = Fish.UserSettings.SpeciesIndex.Species.FindBySpecies(
                    gridRow.Cells[columnSpcSpc.Index].Value as string);

                if (speciesRow == null)
                {
                    gridRow.Cells[gridColumn.Index].Value = null;
                }
                else
                {
                    SpeciesKey.TaxaRow taxaRow = speciesRow.GetTaxon(baseRow);
                    if (taxaRow != null) gridRow.Cells[gridColumn.Index].Value = taxaRow.Taxon;
                }
            }
        }

        #endregion
        
        #region Species stats

        private void species_Changed(object sender, EventArgs e)
        {
            // Make a selection
            if (spreadSheetSpcStats.SelectedRows.Count == 0 ||
                !(spreadSheetSpcStats[ColumnSpcStat.Index, spreadSheetSpcStats.SelectedRows[0].Index].Value is Data.SpeciesRow))
            {
                SelectedStatSpc = null;
            }
            else
            {
                SelectedStatSpc = (Data.SpeciesRow)spreadSheetSpcStats[ColumnSpcStat.Index, spreadSheetSpcStats.SelectedRows[0].Index].Value;
            }

            // Totals
            totals_Changed(sender, e);

            // Cpues
            cpue_Changed(sender, e);

            // Techniques
            tech_Changed(sender, e);

            // Strates
            labelStratesSelectSpecies.Visible = SelectedStatSpc == null;
            plotStrates.Visible = label25.Enabled = label26.Enabled =
                SelectedStatSpc != null;

            if (SelectedStatSpc != null)
            {
                plotStrates.AxisXMin = Service.GetStrate(AllowedStack.LengthMin(SelectedStatSpc)).LeftEndpoint;
                plotStrates.AxisXMax = Service.GetStrate(AllowedStack.LengthMax(SelectedStatSpc)).RightEndpoint;
                plotStrates.AxisYMax = Math.Max(UserSettings.RequiredClassSize, 
                    AllowedStack.GetLengthComposition(SelectedStatSpc, UserSettings.SizeInterval).MostSampled.Quantity);
            }

            strates_Changed(sender, e);

            // Models
            models_Changed(sender, e);
        }

        #region Totals

        private void totals_Changed(object sender, EventArgs e)
        {
            string valueMask = "{0} ({1:P0})";

            if (SelectedStatSpc == null)
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
                double total = AllowedStack.Quantity(SelectedStatSpc);

                double n_str = AllowedStack.QuantityStratified(SelectedStatSpc);
                double n_ind = AllowedStack.QuantityIndividual(SelectedStatSpc);
                double n2 = AllowedStack.Measured(SelectedStatSpc);
                double n3 = AllowedStack.Weighted(SelectedStatSpc);
                double n35 = AllowedStack.Registered(SelectedStatSpc);
                double n4 = AllowedStack.Aged(SelectedStatSpc);
                double n5 = AllowedStack.Sexed(SelectedStatSpc);
                double n6 = AllowedStack.Matured(SelectedStatSpc);

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

                double totalMass = AllowedStack.Mass(SelectedStatSpc);

                if (totalMass > 0)
                {
                    textBoxSpcWTotal.Text = totalMass.ToString("N3");
                    textBoxSpcWLog.Text = AllowedStack.MassIndividual(SelectedStatSpc).ToString("N3");

                    double stratifiedMass = AllowedStack.MassStratified(SelectedStatSpc);

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

                DataPoint speciesPoint = GetSpeciesDataPoint(SelectedStatSpc, false);
                chartSpcStats.Series[0].Points.Add(speciesPoint);

                DataPoint spacePoint = new DataPoint();
                spacePoint.YValues[0] = AllowedStack.QuantitySampled() - AllowedStack.QuantitySampled(SelectedStatSpc);
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
            spreadSheetInd.EnsureFilter(columnIndSpecies, listViewInvestigators.FocusedItem.Text,
                loaderInd, menuItemIndAll_Click);
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            GetFilteredList(columnIndLength);
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            GetFilteredList(columnIndMass);
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            GetFilteredList(columnIndAge);
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            GetFilteredList(columnIndSex);
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {
            GetFilteredList(columnIndMaturity);
        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedStatSpc == null)
            {
                if (data.Stratified.Count == 0) return;
                menuItemLoadStratified_Click(sender, e);
            }
            else
            {
                if (AllowedStack.QuantityStratified(SelectedStatSpc) == 0) return;

                spreadSheetStratified.EnsureFilter(columnStratifiedSpc,
                    SelectedStatSpc.Species, loaderStratified, menuItemLoadStratified_Click);
            }
        }

        #endregion

        #region Cpue

        private void comboBoxCpueSampler_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCpueSamplerType = comboBoxCpueSampler.SelectedValue == null ? 
                FishSamplerType.None : ((FishSamplerTypeDisplay)comboBoxCpueSampler.SelectedValue).Type;

            comboBoxCpueUE.Enabled = SelectedCpueSamplerType != FishSamplerType.None;
            comboBoxCpueUE_SelectedIndexChanged(sender, e);

            if (SelectedCpueSamplerType == FishSamplerType.None) return;
            UnitEffort.SwitchUE(comboBoxCpueUE, SelectedCpueSamplerType);
        }

        private void comboBoxCpueUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCpueUE = (UnitEffort)comboBoxCpueUE.SelectedItem;

            string effortUnit = "?";

            if (SelectedCpueUE != null) {
                effortUnit = SelectedCpueUE.Unit;
            }

            columnCpueEffort.ResetFormatted(effortUnit);
            columnCpueNpue.ResetFormatted(effortUnit);
            columnCpueBpue.ResetFormatted(effortUnit);
            labelNpue.ResetFormatted(effortUnit);
            labelBpue.ResetFormatted(effortUnit);

            cpue_Changed(sender, e);
        }

        private void cpue_Changed(object sender, EventArgs e)
        {
            spreadSheetCpue.Rows.Clear();

            textBoxNpue.Text = string.Empty;
            textBoxBpue.Text = string.Empty;

            if (SelectedCpueSamplerType == FishSamplerType.None) return;
            if (SelectedCpueUE == null) return;

            CpueData = AllowedStack.GetStack(SelectedCpueSamplerType, SelectedCpueUE.Variant);

            string[] meshValues = CpueData.Classes(SelectedCpueSamplerType);
            Npue = 0.0;
            Bpue = 0.0;

            if (meshValues.Length == 0)
            {
                spreadSheetCpue.Rows.Add(GetCpueRow(CpueData));
            }
            else
            {
                foreach (string meshValue in meshValues)
                {
                    CardStack meshData = AllowedStack.GetStack(SelectedCpueSamplerType, meshValue);
                    spreadSheetCpue.Rows.Add(GetCpueRow(meshData));
                }

                Npue /= meshValues.Length;
                Bpue /= meshValues.Length;
            }

            labelCpueNoData.Visible = spreadSheetCpue.RowCount == 0;

            textBoxNpue.Text = Npue.ToString(columnCpueNpue.DefaultCellStyle.Format);
            textBoxBpue.Text = Bpue.ToString(columnCpueBpue.DefaultCellStyle.Format);
        }

        #endregion

        #region Techniques

        private void tech_Changed(object sender, EventArgs e)
        {
            SelectedTechSamplerType = comboBoxTechSampler.SelectedValue == null ?
                FishSamplerType.None : ((FishSamplerTypeDisplay)comboBoxTechSampler.SelectedValue).Type;

            spreadSheetTech.Rows.Clear();

            if (SelectedTechSamplerType == FishSamplerType.None) return;

            TechData = FullStack.GetStack(SelectedTechSamplerType);
            
            string[] meshValues = TechData.Classes(SelectedTechSamplerType);

            if (meshValues.Length == 0)
            {
                spreadSheetTech.Rows.Add(GetTechRow(TechData));
            }
            else
            {
                foreach (string meshValue in meshValues)
                {
                    CardStack meshData = FullStack.GetStack(SelectedTechSamplerType, meshValue);
                    spreadSheetTech.Rows.Add(GetTechRow(meshData));
                }
            }

            labelTechNoData.Visible = spreadSheetTech.RowCount == 0;
        }

        #endregion

        #region Strates

        private void checkBoxStrateUseGearType_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxStrateGearType.Enabled = 
                checkBoxStrateUseClass.Enabled = 
                checkBoxStrateUseGearType.Checked;
            
            strates_Changed(sender, e);
        }

        private void comboBoxStrateGearType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStrateGearClass.DataSource = AllowedStack.Classes(((FishSamplerTypeDisplay)comboBoxStrateGearType.SelectedValue).Type);

            if (((int[])comboBoxStrateGearClass.DataSource).Length == 0)
            {
                strates_Changed(sender, e);
            }
        }

        private void checkBoxStrateUseClass_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxStrateGearClass.Enabled = checkBoxStrateUseClass.Checked;
            strates_Changed(sender, e);
        }

        private void checkBoxStrateUseClass_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBoxStrateUseClass.Enabled)
            {
                checkBoxStrateUseClass.Checked = false;
            }
        }

        private void comboBoxStrateGearClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            strates_Changed(comboBoxStrateGearClass, e);
        }

        private void strates_Changed(object sender, EventArgs e)
        {
            plotStrates.Clear();

            if (SelectedStatSpc == null) return;

            CardStack countStack = AllowedStack;

            if (checkBoxStrateUseGearType.Checked)
            {
                FishSamplerType samplerType = ((FishSamplerTypeDisplay)comboBoxStrateGearType.SelectedValue).Type;

                if (checkBoxStrateUseClass.Checked)
                {
                    countStack = FullStack.GetStack(samplerType, comboBoxStrateGearClass.SelectedValue.ToString());
                }
                else
                {
                    countStack = FullStack.GetStack(samplerType);
                }
            }

            LengthComposition sample = countStack.GetStatisticComposition(SelectedStatSpc, (s, i) => { return countStack.Quantity(s, i); }, Resources.Interface.Interface.StratesSampled);
            LengthComposition weighted = countStack.GetStatisticComposition(SelectedStatSpc, (s, i) => { return countStack.Weighted(s, i); }, Resources.Interface.Interface.StratesWeighted);
            LengthComposition registered = countStack.GetStatisticComposition(SelectedStatSpc, (s, i) => { return countStack.Registered(s, i); }, Resources.Interface.Interface.StratesRegistered);
            LengthComposition aged = countStack.GetStatisticComposition(SelectedStatSpc, (s, i) => { return countStack.Aged(s, i); }, Resources.Interface.Interface.StratesAged);

            if (sample.TotalQuantity > 0) plotStrates.AddSeries(sample.GetHistogram());
            if (weighted.TotalQuantity > 0)  plotStrates.AddSeries(weighted.GetHistogram());
            if (registered.TotalQuantity > 0) plotStrates.AddSeries(registered.GetHistogram());
            if (aged.TotalQuantity > 0) plotStrates.AddSeries(aged.GetHistogram());
            plotStrates.Remaster();
        }

        private void plotStrates_CollectionChanged(object sender, EventArgs e)
        {
            Color startColor = Color.FromArgb(150, Color.Lavender);

            foreach (Histogramma hist in plotStrates.Histograms)
            {
                hist.DataSeries.SetCustomProperty("DrawSideBySide", "False");//.ChartType = SeriesChartType.StackedColumn;
                hist.Properties.Borders = false;
                hist.DataSeries.Color = startColor;
                startColor = startColor.Darker();
            }
        }

        #endregion

        #region Models

        private void models_Changed(object sender, EventArgs e)
        {
            if (!Licensing.Verify("Fishery Scientist")) return;

            labelAL.Enabled = labelALInstruction.Enabled = SelectedStatSpc != null;
            labelLW.Enabled = labelLWInstruction.Enabled = SelectedStatSpc != null;

            if (SelectedStatSpc == null)
            {
                return;
            }

            spreadSheetSpcStats.Enabled = false;

            Cursor = plotAL.Cursor = plotLW.Cursor = Cursors.WaitCursor;
            plotAL.Clear();
            plotLW.Clear(); // That bitch is killing my models

            if (calcModel.IsBusy) { calcModel.CancelAsync(); }
            else { calcModel.RunWorkerAsync(SelectedStatSpc); }
        }

        private void calcModel_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null)
            {
                //e.Cancel = true; 

                growthInternal = growthExternal = GrowthModel =
                    massInternal = massExternal = MassModel =
                    null;

                return;
            }

            Data.SpeciesRow speciesRow = (Data.SpeciesRow)e.Argument;

            //data.GrowthModels.Refresh(speciesRow.Species);
            growthInternal = AllowedStack.Parent.GrowthModels.GetInternalScatterplot(speciesRow.Species);
            growthExternal = AllowedStack.Parent.GrowthModels.GetExternalScatterplot(speciesRow.Species);
            GrowthModel = AllowedStack.Parent.GrowthModels.GetCombinedScatterplot(speciesRow.Species);

            //data.MassModels.Refresh(speciesRow.Species);
            massInternal = AllowedStack.Parent.MassModels.GetInternalScatterplot(speciesRow.Species);
            massExternal = AllowedStack.Parent.MassModels.GetExternalScatterplot(speciesRow.Species);
            MassModel = AllowedStack.Parent.MassModels.GetCombinedScatterplot(speciesRow.Species);
        }

        private void calcModel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) 
            {
                growthInternal = growthExternal = GrowthModel = 
                    massInternal = massExternal = MassModel =
                    null;

                if (SelectedStatSpc != null)
                {
                    calcModel.RunWorkerAsync(SelectedStatSpc);
                }

                return;
            }

            // Check if species changed.
            //if (SelectedStatSpc.Species != )

            labelNoDataAL.Visible = GrowthModel == null;
            plotAL.Visible = GrowthModel != null;
            //plotAL.Clear();

            if (growthExternal != null)
            {
                growthExternal = growthExternal.Copy();
                growthExternal.Series.Name =
                    growthExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SelectedStatSpc.Species);
                growthExternal.Properties.DataPointColor = Constants.InfantColor;
                plotAL.AddSeries(growthExternal);
            }

            if (growthInternal != null)
            {
                growthInternal = growthInternal.Copy();
                growthInternal.Series.Name =
                    growthInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SelectedStatSpc.Species);
                growthInternal.Properties.DataPointColor = UserSettings.ModelColor;
                plotAL.AddSeries(growthInternal);
            }

            if (GrowthModel != null)
            {
                plotAL.AddSeries(GrowthModel.Data.X.GetHistogram());

                GrowthModel = GrowthModel.Copy();
                GrowthModel.Properties.ShowTrend = true;
                GrowthModel.Properties.SelectedApproximationType = AllowedStack.Parent.GrowthModels.Nature;
                GrowthModel.Properties.DataPointColor = System.Drawing.Color.Transparent;
                GrowthModel.Properties.TrendColor = UserSettings.ModelColor.Darker();
                GrowthModel.Properties.ShowPredictionBands = true;
                GrowthModel.Properties.HighlightRunouts = true;
                //GrowthModel.Properties.AllowCursors = true;
                plotAL.AddSeries(GrowthModel);
            }

            //statChartAL.Remaster();
            plotAL.Update(this, new EventArgs());
            if (plotAL.Scatterplots.Count > 0) plotAL.Rebuild(this, new EventArgs());


            labelNoDataLW.Visible = MassModel == null;
            plotLW.Visible = MassModel != null;

            //plotLW.Clear();
            //while (plotLW.Scatterplots.Count > 0) {
            //    plotLW.Scatterplots[0].Dispose();
            //}

            if (massExternal != null)
            {
                massExternal = massExternal.Copy();
                massExternal.Series.Name =
                    massExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SelectedStatSpc.Species);
                massExternal.Properties.DataPointColor = Constants.InfantColor;
                plotLW.AddSeries(massExternal);
            }

            if (massInternal != null)
            {
                massInternal = massInternal.Copy();
                massInternal.Series.Name =
                    massInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SelectedStatSpc.Species);
                massInternal.Properties.DataPointColor = UserSettings.ModelColor;
                plotLW.AddSeries(massInternal);
            }

            if (MassModel != null)
            {
                plotLW.AddSeries(MassModel.Data.X.GetHistogram());
                //plotLW.AxisXMin = Service.GetStrate(FullStack.LengthMin(SelectedStatSpc)).LeftEndpoint;
                //plotLW.AxisXMax = Service.GetStrate(FullStack.LengthMax(SelectedStatSpc)).RightEndpoint;
                //plotLW.AxisY2Max = Math.Max(UserSettings.RequiredClassSize,
                //    FullStack.GetStatisticComposition(SelectedStatSpc, (s, i) => { return FullStack.Weighted(s, i); }, Resources.Interface.StratesWeighted).MostSampled.Quantity);

                MassModel = MassModel.Copy();
                MassModel.Properties.ShowTrend = true;
                MassModel.Properties.SelectedApproximationType = AllowedStack.Parent.MassModels.Nature;
                MassModel.Properties.DataPointColor = System.Drawing.Color.Transparent;
                //weightInternal.Properties.DataPointColor = UserSettings.ModelColor.Darker();
                MassModel.Properties.ShowPredictionBands = true;
                MassModel.Properties.HighlightRunouts = true;
                //WeightModel.Properties.AllowCursors = true;
                plotLW.AddSeries(MassModel);
            }

            //plotWeight.Remaster();
            plotLW.Update(this, new EventArgs());
            if (plotLW.Scatterplots.Count > 0) plotLW.Rebuild(this, new EventArgs());

            //buttonOutliersAL.Enabled = 
            //    GrowthModel != null && GrowthModel.IsRegressionOK && GrowthModel.Regression.GetRunouts().Count > 0;

            //buttonOutliersLW.Enabled = 
            //    WeightModel != null && WeightModel.IsRegressionOK && WeightModel.Regression.GetRunouts().Count > 0;

            //bool growthOK = (GrowthModel != null && GrowthModel.IsRegressionOK && !growthRunouts);
            //bool weightOK = (WeightModel != null && WeightModel.IsRegressionOK && !weigthRunouts);

            //pictureBoxInput.Visible = labelInputGood.Visible = growthOK && weightOK;

            Cursor = plotAL.Cursor = plotLW.Cursor = Cursors.Default;
            spreadSheetSpcStats.Enabled = true;
        }

        private void plotAL_CollectionChanged(object sender, EventArgs e)
        {
            if (sender is Histogramma)
            {
                Color startColor = Color.Gray.Lighter();
                double max = 0;

                foreach (Histogramma hist in plotAL.Histograms)
                {
                    //hist.DataSeries.ChartType = SeriesChartType.Bar;
                    hist.DataSeries.YAxisType = AxisType.Secondary;
                    hist.DataSeries.SetCustomProperty("DrawSideBySide", "False");
                    hist.Properties.Borders = false;
                    hist.Properties.DataPointColor = startColor;
                    startColor = startColor.Darker();

                    max = Math.Max(max, hist.Top);
                }

                plotAL.AxisY2Max = Math.Max(UserSettings.RequiredClassSize, max);
            }
        }

        private void plotLW_CollectionChanged(object sender, EventArgs e)
        {
            if (sender is Histogramma)
            {
                Color startColor = Color.Gray.Lighter(); // Color.Gainsboro.Lighter();
                double max = 0;

                foreach (Histogramma hist in plotLW.Histograms)
                {
                    hist.DataSeries.YAxisType = AxisType.Secondary;
                    hist.DataSeries.SetCustomProperty("DrawSideBySide", "False");//.ChartType = SeriesChartType.StackedColumn;
                    hist.Properties.DataPointColor = startColor;
                    hist.Properties.Borders = false;
                    startColor = startColor.Darker();

                    max = Math.Max(max, hist.Top);
                }

                plotLW.AxisY2Max = Math.Max(UserSettings.RequiredClassSize, max);
            }
        }

        private void buttonOutliersLW_Click(object sender, EventArgs e)
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();

            BackgroundWorker outlierLW = new BackgroundWorker();
            outlierLW.DoWork += outlierLW_DoWork;
            outlierLW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(indLoader_RunWorkerCompleted);
            outlierLW.RunWorkerAsync();
        }

        private void buttonOutliersAL_Click(object sender, EventArgs e)
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();

            BackgroundWorker outlierAL = new BackgroundWorker();
            outlierAL.DoWork += outlierAL_DoWork;
            outlierAL.RunWorkerCompleted += new RunWorkerCompletedEventHandler(indLoader_RunWorkerCompleted);
            outlierAL.RunWorkerAsync();
        }

        private void outlierAL_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

            if (GrowthModel != null)
            {
                //BivariateSample growthRun = GrowthModel.Regression.GetRunouts();
                //if (growthRun != null)
                //{
                //    foreach (XY pair in growthRun)
                //    {
                //        indRows.AddRange(data.GetIndividuals(SelectedStatSpc,
                //            new string[] { "Age", "Length" },
                //            new object[] { pair.X, pair.Y }));
                //    }
                //}
            }

            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (Data.IndividualRow indRow in indRows)
            {
                result.Add(GetLine(indRow));
            }

            e.Result = result.ToArray();
        }

        private void outlierLW_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

            if (MassModel != null)
            {
                //BivariateSample weightRun = WeightModel.Regression.GetRunouts();
                //if (weightRun != null)
                //{
                //    foreach (XY pair in weightRun)
                //    {
                //        indRows.AddRange(data.GetIndividuals(SelectedStatSpc,
                //            new string[] { "Length", "Mass" },
                //            new object[] { pair.X, pair.Y }));
                //    }
                //}
            }

            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (Data.IndividualRow indRow in indRows)
            {
                result.Add(GetLine(indRow));
            }

            e.Result = result.ToArray();
        }

        #endregion

        private void buttonSpeciesStatsReport_Click(object sender, EventArgs e)
        {
            SpeciesStatsLevel lvl = SpeciesStatsLevel.Totals | SpeciesStatsLevel.Detailed | SpeciesStatsLevel.TreatmentSuggestion | SpeciesStatsLevel.SurveySuggestion;
            
            if (SelectedStatSpc == null)
            {
                Report report = new Report(Resources.Reports.SpeciesStats.Title);
                AllowedStack.AddCommon(report);
                AllowedStack.Sort();
                AllowedStack.AddSpeciesStatsReport(report, lvl);
                report.EndBranded();
                report.Run();                

                //Report report1 = new Report(string.Format(Resources.Reports.SpeciesStats.Header4, Service.Localize(data.Individual.AgeColumn.Caption)));
                //AllowedStack.AddSpeciesSurveySuggestionReport(report1);
                //report1.EndBranded();
                //report1.Run();
            }
            else
            {
                Report report = new Report(string.Format(Resources.Reports.SpeciesStats.Title_1, SelectedStatSpc.ToHTML()));
                AllowedStack.AddCommon(report, SelectedStatSpc);
                AllowedStack.Sort();
                AllowedStack.AddSpeciesStatsReport(report, SelectedStatSpc, lvl);
                report.EndBranded();
                report.Run();
            }


        }

        #endregion

        #region Individuals

        private void menuItemIndividuals_DropDownOpening(object sender, EventArgs e)
        {
            menuItemDietExplorer.Enabled = false;

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                Data.IndividualRow indRow = IndividualRow(gridRow);
                if (!indRow.IsDietPresented()) continue;
                menuItemDietExplorer.Enabled = true;
                break;
            }

            menuItemIndSimulate.Enabled = data.Stratified.Count > 0;
            menuItemIndClearSimulated.Enabled = (data.Stratified.Count > 0 && spreadSheetInd.ContainsEmptyCells(columnIndID));
        }

        private void menuItemIndPrintLog_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetInd.Rows).GetReport(CardReportLevel.Individuals).Run();
        }

        private void menuItemIndPrint_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetInd.Rows).GetReport(CardReportLevel.Profile).Run();
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
            spreadSheetInd.UpdateStatus();
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



        private void specTipper_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Licensing.Verify("Fishery Scientist")) return;

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (UserSettings.AgeSuggest) SetIndividualAgeTip(gridRow);
                if (UserSettings.MassSuggest) SetIndividualMassTip(gridRow);
            }
        }

        private void specTipper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

            Data.IndividualRow editedIndividualRow = IndividualRow(spreadSheetInd.Rows[e.RowIndex]);

            // If values was typed in
            if (spreadSheetInd.ContainsFocus)
            {
                // Save all new values
                editedIndividualRow = SaveIndRow(spreadSheetInd.Rows[e.RowIndex]);

                // If mass OR age was changed
                if (e.ColumnIndex == columnIndMass.Index || e.ColumnIndex == columnIndAge.Index)
                {
                    // Recalculate if needed
                    if (specUpdater.IsBusy) { specUpdater.CancelAsync(); }
                    else { specUpdater.RunWorkerAsync(editedIndividualRow.LogRow.SpeciesRow); }
                }

                // If age was changed
                if (e.ColumnIndex == columnIndAge.Index)
                {
                    // Reset formatting
                    Wild.Service.HandleAgeInput(spreadSheetInd[columnIndAge.Index, e.RowIndex]);

                    // Set generation value
                    if (editedIndividualRow.IsAgeNull()) { spreadSheetInd[columnIndGeneration.Index, e.RowIndex].Value = null; }
                    else { spreadSheetInd[columnIndGeneration.Index, e.RowIndex].Value = editedIndividualRow.Generation; }
                }

                // If mass was changed
                if (e.ColumnIndex == columnIndMass.Index)
                {
                    // Reset Total mass status text
                    statusMass.ResetFormatted(AllowedStack.Mass());
                }

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

        private void specUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Licensing.Verify("Fishery Scientist")) return;

            Data.SpeciesRow specisRow = (Data.SpeciesRow)e.Argument;
            e.Result = specisRow;

            if (UserSettings.MassSuggest)
            {
                data.MassModels.Refresh(specisRow.Species);

                foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
                {
                    if (IndividualRow(gridRow).LogRow.SpeciesRow != specisRow) continue;
                    SetIndividualMassTip(gridRow);
                }
            }

            if (UserSettings.AgeSuggest)
            {
                data.GrowthModels.Refresh(specisRow.Species);

                foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
                {
                    if (IndividualRow(gridRow).LogRow.SpeciesRow != specisRow) continue;
                    SetIndividualAgeTip(gridRow);
                }
            }
        }

        private void specUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) {
                specUpdater.RunWorkerAsync(e.Result);
                return;
            }
        }


        private void spreadSheetInd_SelectionChanged(object sender, EventArgs e)
        {
            selectedLogRows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                if (individualRow == null) continue;

                if (!selectedLogRows.Contains(individualRow.LogRow))
                {
                    selectedLogRows.Add(individualRow.LogRow);
                }
            }
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
                Data.IndividualRow indRow = IndividualRow(gridRow);
                if (!indRow.IsDietPresented()) continue;
                contextIndExploreDiet.Enabled = true;
                break;
            }

            printIndividualsLogToolStripMenuItem.Enabled = spreadSheetInd.SelectedRows.Count > 1;
        }

        private void contextIndProfile_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                if (individualRow == null) continue;

                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(gridRow);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.Show();
            }
        }

        private void fecundityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                if (individualRow == null) continue;

                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(gridRow);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowFecundity();
                individual.Show();
            }
        }

        private void growthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                if (individualRow == null) continue;

                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(gridRow);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowGrowth();
                individual.Show();
            }
        }

        private void contextIndTrophics_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                if (individualRow == null) continue;

                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(gridRow);
                individual.ValueChanged += new EventHandler(individual_ValueChanged);
                individual.FormClosing += individual_FormClosing;
                individual.ShowTrophics();
                individual.Show();
            }
        }

        private void parasitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

                if (individualRow == null) continue;

                Individual individual = new Individual(individualRow);
                individual.SetFriendlyDesktopLocation(gridRow);
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
                RememberChanged(profile.IndividualRow.LogRow.CardRow);
                UpdateIndividualRow(profile.IndividualRow);
            }
        }

        private void contextIndOpen_Click(object sender, EventArgs e)
        {
            spreadSheetInd.EndEdit();
            
            foreach (Data.LogRow logRow in selectedLogRows)
            {
                Mayfly.FileSystem.RunFile(logRow.CardRow.Path,
                    new object[] { logRow.SpeciesRow.Species });

                // TODO: select row in a log
            }
        }

        private void contextIndOpenFolder_Click(object sender, EventArgs e)
        {
            List<string> directories = new List<string>();

            foreach (Data.LogRow logRow in selectedLogRows)
            {
                string dir = Path.GetDirectoryName(logRow.CardRow.Path);
                if (!directories.Contains(dir))
                {
                    directories.Add(dir);
                }
            }

            foreach (string dir in directories)
            {
                FileSystem.RunFile(dir);
            }
        }

        private void individual_ValueChanged(object sender, EventArgs e)
        {
            UpdateIndividualRow(((Individual)sender).IndividualRow);
        }

        private void printIndividualsLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetInd.SelectedRows).GetReport(CardReportLevel.Individuals).Run();
        }

        private void contextIndPrint_Click(object sender, EventArgs e)
        {
            GetIndividuals(spreadSheetInd.SelectedRows).GetReport(CardReportLevel.Profile).Run();
        }

        private void contextIndExploreDiet_Click(object sender, EventArgs e)
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(spreadSheetInd.SelectedRows.Count, Resources.Interface.Process.DietCompiling);
            dietCompiler.RunWorkerAsync(spreadSheetInd.SelectedRows);
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
            spreadSheetInd.StartProcessing(spreadSheetInd.VisibleRowCount, Resources.Interface.Process.DietCompiling);
            dietCompiler.RunWorkerAsync(spreadSheetInd.GetVisibleRows());
        }

        private void dietCompiler_DoWork(object sender, DoWorkEventArgs e)
        {
            Data result = new Data();

            int index = 0;

            foreach (DataGridViewRow gridRow in (IEnumerable)e.Argument)
            {
                Data.IndividualRow individualRow = IndividualRow(gridRow);

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
            dietExplorer.CardRowSaved += DietExplorer_CardRowSaved;
            dietExplorer.Sync(this);
            this.WindowState = FormWindowState.Minimized;
            dietExplorer.Show();
            dietExplorer.PerformDietExplorer(AllowedStack.FriendlyName);
        }

        private void DietExplorer_CardRowSaved(object sender, CardRowSaveEvent e)
        {
            Data.IntestineRow intestineRow = data.Intestine.FindByID(e.Row.ID);
            Data consumed = e.Row.SingleCardDataset();
            // TODO: Clear consumed from Comments, date, water and other stuff
            intestineRow.Consumed = consumed.GetXml().Replace(System.Environment.NewLine, " ");
            double w = intestineRow.IndividualRow.GetConsumed().GetStack().Mass();
            if (!double.IsNaN(w)) intestineRow.IndividualRow.ConsumedMass = w;
            UpdateIndividualRow(intestineRow.IndividualRow);
            RememberChanged(intestineRow.IndividualRow.LogRow.CardRow);
        }

        #endregion

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



        private void contextLog_Opening(object sender, CancelEventArgs e)
        {
            contextLogOpen.Enabled = selectedLogRows.Count > 0;
        }

        private void contextLogOpen_Click(object sender, EventArgs e)
        {
            foreach (Data.LogRow logRow in selectedLogRows)
            {
                Mayfly.FileSystem.RunFile(logRow.CardRow.Path,
                    new object[] { logRow.SpeciesRow.Species });
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

                if (baseSpc == null) {
                    composition = stack.GetCommunityComposition();
                } else {
                    composition = stack.GetTaxonomicComposition(baseSpc);
                }

                composition.Name = variant;
                composition.Weight = stack.GetEffort();
                result.Add(composition);
            }

            e.Result = result;
        }

        private void comparerLog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Composition composition;

            if (baseSpc == null) {
                composition = AllowedStack.GetCommunityCompositionFrame();
            } else {
                composition = AllowedStack.GetTaxonomicCompositionFrame(baseSpc);
            }

            CompositionComparison comcom = new CompositionComparison(
                composition, (List<Composition>)e.Result);

            comcom.UpdateCompositionAppearance();
            comcom.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            comcom.Show();

            IsBusy = false;
            spreadSheetLog.StopProcessing();
        }



        private void LogLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            if (baseLog == null)
            {
                for (int i = 0; i < data.Card.Count; i++)
                {
                    foreach (Data.SpeciesRow speciesRow in data.Species)
                    {
                        DataGridViewRow gridRow = GetLine(data.Card[i], speciesRow);

                        result.Add(gridRow);

                        if (gridRow.Cells[columnLogQuantity.Index].Value != null &&
                            (int)gridRow.Cells[columnLogQuantity.Index].Value == 0)
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
                        DataGridViewRow gridRow = GetLine(data.Card[i], taxaRow);

                        result.Add(gridRow);

                        if ((int)gridRow.Cells[columnLogQuantity.Index].Value == 0)
                        {
                            spreadSheetLog.SetHidden(gridRow);
                        }
                    }

                    (sender as BackgroundWorker).ReportProgress(i + 1);
                }
            }

            e.Result = result.ToArray();
        }

        private void LogLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetLog.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetLog.StopProcessing();
            spreadSheetLog.UpdateStatus();
        }

        private void LogExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(SetCardValue);
                gridRow.DataGridView.Invoke(valueSetter,
                    new object[] { CardRow(gridRow, columnLogID), gridRow, spreadSheetLog.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void spreadSheetLog_SelectionChanged(object sender, EventArgs e)
        {
            selectedLogRows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                Data.LogRow logRow = LogRow(gridRow);
                if (logRow != null) selectedLogRows.Add(logRow);
            }
        }
        
        private void comboBoxLogTaxa_SelectedIndexChanged(object sender, EventArgs e)
        {
            baseLog = comboBoxLogTaxa.SelectedItem as SpeciesKey.BaseRow;
            taxaLog = baseLog == null ? null : data.Species.Taxa(baseLog);
            LoadLog();
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

        #region Stratified

        private void printStratifiedSamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetStratified(spreadSheetStratified.Rows).GetReport(CardReportLevel.Stratified).Run();
        }

        private void printStratifiedSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetStratified(spreadSheetStratified.SelectedRows).GetReport(CardReportLevel.Stratified).Run();
        }

        private void stratifiedLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            int totalCount = 0;

            for (int i = 0; i < data.Log.Rows.Count; i++)
            {
                int count = data.Log[i].QuantityStratified;

                if (count > 0)
                {
                    result.Add(StratifiedSampleRow(data.Log[i]));
                }

                totalCount += count;

                (sender as BackgroundWorker).ReportProgress(totalCount);
            }

            e.Result = result.ToArray();
        }

        private void stratifiedLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetStratified.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetStratified.StopProcessing();
            spreadSheetStratified.UpdateStatus((int)spreadSheetStratified.Sum(columnStratifiedCount));
        }

        private void stratifiedExtender_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetStratified.Rows)
            {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(SetCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { LogRowStratified(gridRow).CardRow, gridRow, spreadSheetStratified.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void spreadSheetStratified_SelectionChanged(object sender, EventArgs e)
        {
            selectedLogRows.Clear();

            if (spreadSheetStratified.SelectedRows.Count > 0)
            {
                double min = double.MaxValue;
                double max = double.MinValue;

                double interval = 0.1;

                foreach (Data.LogRow logRow in GetStratified(spreadSheetStratified.SelectedRows))
                {
                    selectedLogRows.Add(logRow);

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
                        foreach (Data.StratifiedRow stratifiedRow in LogRowStratified(gridRow).GetStratifiedRows())
                        {
                            if (sizeGroup.LengthInterval.LeftClosedContains(stratifiedRow.SizeClass.Midpoint))
                                countSum += stratifiedRow.Count;
                        }
                    }

                    sizeGroup.Count = countSum;
                }

                ShowStratified();

                stratifiedSample.PerformLayout();
            }
            else
            {
                HideStratified();
            }
        }

        private void spreadSheetStratified_Filtered(object sender, EventArgs e)
        {
            spreadSheetStratified.UpdateStatus((int)spreadSheetStratified.Sum(columnStratifiedCount));
        }

        private void buttonSelectStratified_Click(object sender, EventArgs e)
        {
            if (LoadCardAddt(spreadSheetStratified))
            {
                IsBusy = true;
                processDisplay.StartProcessing(spreadSheetStratified.RowCount, Wild.Resources.Interface.Process.ExtStrat);
                loaderStratifiedExtended.RunWorkerAsync();
            }
        }

        #endregion
    }
}