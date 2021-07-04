﻿using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Resources;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        public Data data = new Data();

        public CardStack FullStack { get; private set; }

        public CardStack AllowedStack { get; private set; }

        public bool IsBusy
        {
            get
            {
                return busy;
            }

            set
            {
                tabControl.AllowDrop =
                    tabPageInfo.AllowDrop =
                    spreadSheetInd.Enabled =

                    menuFile.Enabled =
                    menuSample.Enabled =
                    menuSurvey.Enabled =
                    menuFishery.Enabled =

                    spreadSheetInd.Enabled = !value;


                menuSample.Enabled =
                    menuFishery.Enabled =
                    (!IsAllowedEmpty && !value);


                foreach (Control control in tabPageInfo.Controls)
                {
                    control.Enabled = !value;
                }

                busy = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return empty;
            }

            set
            {
                empty = value;

                menuItemBackup.Enabled =
                    menuItemSaveSet.Enabled =
                    !empty;

                foreach (ToolStripItem item in menuSurvey.DropDownItems)
                {
                    if (item == menuItemSurveyInput) continue;
                    if (item == menuItemSpawning) continue;
                    item.Enabled = !allowedempty;
                }
            }
        }

        public bool IsAllowedEmpty
        {
            get
            {
                return allowedempty;
            }

            set
            {
                allowedempty = value;

                menuSample.Enabled =
                    menuFishery.Enabled = !allowedempty;

                menuItemMSYR.Enabled = !allowedempty;

                menuItemExportSpec.Enabled = !allowedempty;

                foreach (ToolStripItem item in menuSurvey.DropDownItems)
                {
                    if (item == menuItemSurveyInput) continue;
                    item.Enabled = !allowedempty;
                }
            }
        }

        public bool IsChanged
        {
            get
            {
                return ChangedCards.Count > 0;
            }
        }



        List<Data.LogRow> selectedLogRows;

        bool busy;

        bool empty;

        bool allowedempty;

        List<Data.CardRow> ChangedCards = new List<Data.CardRow>();

        bool IsClosing = false;


        private void UpdateSummary()
        {
            FullStack = new CardStack(data.Card);
            AllowedStack = new CardStack(data.Card); //, true);

            IsEmpty = FullStack.Count == 0;
            IsAllowedEmpty = AllowedStack.Count == 0;

            if (IsEmpty)
            {
                this.Text = EntryAssemblyInfo.Title;
                labelArtefacts.Visible = pictureBoxArtefacts.Visible = false;

                labelDatesValue.Text = Constants.Null;
                labelCardsValue.Text = Constants.Null;
                labelCards.Text = string.Empty;

                statusQuantity.ResetFormatted(Constants.Null);
                statusMass.ResetFormatted(Constants.Null);

                IsBusy = false;
            }
            else
            {                
                UserSettings.Interface.SaveDialog.FileName = FullStack.FriendlyName;
                this.ResetText(FullStack.FriendlyName, EntryAssemblyInfo.Title);

                Log.Write("{0} cards are under consideration (common path: {1}).",
                    data.Card.Count, FileSystem.GetCommonPath(FullStack.GetFilenames()));

                spreadSheetCard.ClearInsertedColumns();

                foreach (Data.FactorRow factorRow in data.Factor)
                {
                    DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                    gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
                }

                #region Update general values

                labelCardsValue.Text = data.Card.Count.ToString();
                string formatted = data.Card.Count.ToCorrectString(new ResourceManager(typeof(MainForm)).GetString("labelCards.Text"));
                labelCards.Text = formatted.Substring(formatted.IndexOf(' ') + 1);


                labelDatesValue.Text = data.GetDates().GetDatesDescription();

                //if (FullStack.GetDates().Count() > 1)
                //{
                //    labelDatesValue.ResetFormatted(FullStack.EarliestEvent, FullStack.LatestEvent);
                //}
                //else if (FullStack.GetDates().Count() == 1)
                //{
                //    labelDatesValue.Text = FullStack.EarliestEvent.ToString("D");
                //}
                //else
                //{
                //    labelDatesValue.Text = string.Empty;
                //}

                listViewWaters.Items.Clear();
                foreach (Data.WaterRow waterRow in data.Water)
                {
                    if (waterRow.IsWaterNull())
                    {
                        listViewWaters.CreateItem(waterRow.ID.ToString(), Waters.Resources.Interface.Unnamed, waterRow.Type - 1);
                    }
                    else
                    {
                        listViewWaters.CreateItem(waterRow.ID.ToString(), waterRow.Water, waterRow.Type - 1);
                    }
                }

                listViewSamplers.Items.Clear();
                foreach (Samplers.SamplerRow samplerRow in FullStack.GetSamplers())
                {
                    listViewSamplers.CreateItem(samplerRow.ID.ToString(), samplerRow.Sampler);
                }

                #endregion

                #region Authorities

                listViewInvestigators.Items.Clear();

                foreach (string investigator in FullStack.GetInvestigators()) {
                    listViewInvestigators.CreateItem(investigator, investigator);
                }

                //Wild.UserSettings.InstalledPermissions.CheckAuthorization(
                //    FullStack.GetInvestigators());

                #endregion

                menuItemLoadIndividuals.AddSpeciesMenus(AllowedStack, speciesInd_Click);
                menuItemGrowthCrossSection.AddSpeciesMenus(AllowedStack, speciesGrowth_Click);
                menuItemGrowthCohorts.AddSpeciesMenus(AllowedStack, speciesGrowthCohorts_Click);
                menuItemComposition.AddSpeciesMenus(AllowedStack, speciesComposition_Click);
                menuItemMortalitySustainable.AddSpeciesMenus(AllowedStack, speciesMortality_Click);
                menuItemMortalityCohorts.AddSpeciesMenus(AllowedStack, speciesMortalityCohorts_Click);
                menuItemSelectivity.AddSpeciesMenus(AllowedStack, speciesSelectivity_Click);
                menuItemExtrapolation.AddSpeciesMenus(AllowedStack, speciesStockExtrapolation_Click);
                menuItemVpa.AddSpeciesMenus(AllowedStack, speciesStockVpa_Click);
                menuItemMSYR.AddSpeciesMenus(AllowedStack, speciesMSYR_Click);
                menuItemMSY.AddSpeciesMenus(AllowedStack, speciesMSY_Click);
                menuItemPrediction.AddSpeciesMenus(AllowedStack, speciesPrediction_Click);

                if (!modelCalc.IsBusy && !IsClosing)
                {
                    IsBusy = true;
                    processDisplay.StartProcessing(data.Species.Count, Wild.Resources.Interface.Interface.ModelCalc);
                    modelCalc.RunWorkerAsync();
                }
            }

            statusBio.Visible = data.BioLoaded;
        }

        private void speciesInd_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                tabPageInd.Parent = tabControl;
                tabControl.SelectedTab = tabPageInd;
                spreadSheetInd.Rows.Clear();
                Data.IndividualRow[] indRows =
                    ModifierKeys.HasFlag(Keys.Shift) ?
                    AllowedStack.GetTreatmentSuggestion(speciesRow, data.Individual.AgeColumn).GetAll() :
                    AllowedStack.GetTreatmentSuggestion(speciesRow, data.Individual.AgeColumn).GetSuggested();
                List<DataGridViewRow> result = new List<DataGridViewRow>();
                for (int i = 0; i < indRows.Length; i++) {
                    result.Add(GetLine(indRows[i]));
                }
                spreadSheetInd.Rows.AddRange(result.ToArray());
                spreadSheetInd.UpdateStatus();
            } else {
                LoadIndLog(speciesRow);
            }
        }



        private DialogResult CheckAndSave()
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSave.ShowDialog(this);

                if (b == tdbSaveAll)
                {
                    SaveCards();
                    return DialogResult.OK;
                }
                else if (b == tdbCancelClose)
                {
                    return DialogResult.Cancel;
                }
            }

            return DialogResult.No;
        }

        private void SaveCards()
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(ChangedCards.Count, Wild.Resources.Interface.Process.CardsSaving);
            dataSaver.RunWorkerAsync();
        }  


        public void LoadCards(params string[] entries)
        {
            IsBusy = true;
            processDisplay.StartProcessing(entries.Length, Wild.Resources.Interface.Process.CardsLoading);            
            loaderData.RunWorkerAsync(entries);
        }
                

        private bool LoadCardAddt(SpreadSheet spreadSheet)
        {
            SelectionValue selectionValue = new SelectionValue(spreadSheetCard);
            selectionValue.Picker.UserSelectedColumns = spreadSheet.GetInsertedColumns();

            if (selectionValue.ShowDialog(this) != DialogResult.OK) return false;

            bool newInserted = false;
            int i = spreadSheet.InsertedColumnCount;
            foreach (DataGridViewColumn gridColumn in spreadSheet.GetInsertedColumns())
            {
                if (gridColumn.Name.Contains("Var")) continue;
                if (selectionValue.Picker.IsSelected(gridColumn)) continue;
                spreadSheet.Columns.Remove(gridColumn);
                i--;
            }

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
            {
                if (spreadSheet.GetColumn(gridColumn.Name) == null)
                {
                    spreadSheet.InsertColumn(gridColumn, i, gridColumn.Name.TrimStart("columnCard".ToCharArray())).ReadOnly = true;
                    newInserted = true;
                    i++;
                }
            }

            return newInserted;
        }

        private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns)
        {
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                if (gridColumn.Name.StartsWith("Var_")) continue;
                SetCardValue(cardRow, gridRow, gridColumn);
            }
        }

        private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        {
            SetCardValue(cardRow, gridRow, gridColumn, gridColumn.Name);
        }

        private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn, string field)
        {
            gridRow.Cells[gridColumn.Index].Value = cardRow.Get(field);

            switch (field)
            {
                case "Effort":
                    gridRow.Cells[gridColumn.Index].Style.Format = gridColumn.ExtendFormat(cardRow.GetGearType().GetDefaultUnitEffort().Unit);
                    break;
            }
        }

        private delegate void ValueSetEventHandler(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);


        private void RememberChanged(Data.CardRow cardRow)
        {
            if (!ChangedCards.Contains(cardRow)) { ChangedCards.Add(cardRow); }
            menuItemSave.Enabled = IsChanged;
        }
    }
}