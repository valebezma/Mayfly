using Mayfly.Controls;
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
        public Data data = new Data(Fish.UserSettings.SpeciesIndex, Fish.UserSettings.SamplersIndex);

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
                    item.Enabled = !allowedEmpty;
                }
            }
        }

        public bool IsAllowedEmpty
        {
            get
            {
                return allowedEmpty;
            }

            set
            {
                allowedEmpty = value;

                menuSample.Enabled =
                    menuFishery.Enabled = !allowedEmpty;

                menuItemMSYR.Enabled = !allowedEmpty;

                menuItemExportSpec.Enabled = !allowedEmpty;

                foreach (ToolStripItem item in menuSurvey.DropDownItems)
                {
                    if (item == menuItemSurveyInput) continue;
                    item.Enabled = !allowedEmpty;
                }
            }
        }

        public bool IsChanged
        {
            get
            {
                return changedCards.Count > 0;
            }
        }

        bool busy;
        bool empty;
        bool allowedEmpty;
        private List<Data.CardRow> changedCards = new List<Data.CardRow>();

        bool isClosing = false;


        private void updateSummary()
        {
            FullStack = new CardStack(data.Card);
            AllowedStack = new CardStack(data.Card); //, true);

            IsEmpty = FullStack.Count == 0;
            IsAllowedEmpty = AllowedStack.Count == 0;

            if (IsEmpty)
            {
                Text = EntryAssemblyInfo.Title;

                labelArtefacts.Visible = pictureBoxArtefacts.Visible = false;
                labelCardCountValue.Text = Constants.Null;

                updateQty(0);
                updateMass(0);

                data.RefreshBios();

                IsBusy = false;
            }
            else
            {
                UserSettings.Interface.SaveDialog.FileName = FullStack.FriendlyName;
                this.ResetText(FullStack.FriendlyName, EntryAssemblyInfo.Title);

                Log.Write("{0} cards are under consideration (common path: {1}).",
                    data.Card.Count, IO.GetCommonPath(FullStack.GetFilenames()));

                spreadSheetCard.ClearInsertedColumns();

                foreach (Data.FactorRow factorRow in data.Factor)
                {
                    DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                    gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
                }

                labelCardCountValue.Text = data.Card.Count.ToString();

                listViewDates.Items.Clear();
                foreach (DateTime[] bunch in data.GetDates().GetDatesBunches())
                {
                    ListViewItem li = listViewDates.CreateItem(bunch.GetDatesRangeDescription());
                    li.Tag = bunch;
                }

                listViewWaters.Items.Clear();
                foreach (Data.WaterRow waterRow in data.Water)
                {
                    var li = listViewWaters.CreateItem(waterRow.ID.ToString(), waterRow.IsWaterNull() ? Waters.Resources.Interface.Unnamed : waterRow.Water, waterRow.Type - 1);
                }

                bool mono = true;

                menuItemCardWater.Visible = data.Water.Count > 1;
                if (data.Water.Count > 1)
                {
                    mono = false;
                    menuItemCardWater.DropDownItems.Clear();
                    foreach (Data.WaterRow waterRow in data.Water)
                    {
                        var menuItem = new ToolStripMenuItem(waterRow.IsWaterNull() ? Waters.Resources.Interface.Unnamed : waterRow.Water);
                        menuItem.Click += (sender, e) =>
                        {
                            loadCards(AllowedStack.GetStack(waterRow));
                        };
                        menuItemCardWater.DropDownItems.Add(menuItem);
                    }
                }

                listViewSamplers.Items.Clear();
                foreach (Samplers.SamplerRow samplerRow in FullStack.GetSamplers())
                {
                    listViewSamplers.CreateItem(samplerRow.ID.ToString(), samplerRow.Sampler);
                }

                menuItemCardGear.Visible = FullStack.GetSamplers().Length > 1;
                if (FullStack.GetSamplers().Length > 1)
                {
                    mono = false;
                    menuItemCardGear.DropDownItems.Clear();
                    foreach (Samplers.SamplerRow samplerRow in FullStack.GetSamplers())
                    {
                        var menuItem = new ToolStripMenuItem(samplerRow.Sampler);
                        menuItem.Click += (sender, e) =>
                        {
                            loadCards(AllowedStack.GetStack(samplerRow));
                        };
                        menuItemCardGear.DropDownItems.Add(menuItem);
                    }
                }

                listViewInvestigators.Items.Clear();
                foreach (string investigator in FullStack.GetInvestigators())
                {
                    listViewInvestigators.CreateItem(investigator, investigator);
                }

                menuItemCardInvestigator.Visible = FullStack.GetInvestigators().Length > 1;
                if (FullStack.GetInvestigators().Length > 1)
                {
                    mono = false;
                    menuItemCardInvestigator.DropDownItems.Clear();
                    foreach (string investigator in FullStack.GetInvestigators())
                    {
                        var menuItem = new ToolStripMenuItem(investigator);
                        menuItem.Click += (sender, e) => {
                            loadCards(AllowedStack.GetStack("Investigator", investigator));
                        };
                        menuItemCardInvestigator.DropDownItems.Add(menuItem);
                    }
                }

                toolStripSeparator25.Visible = menuItemCardAll.Visible = !mono;

                if (mono)
                {
                    menuItemCards.Click += menuItemCards_Click;
                    menuItemCardAll.Click -= menuItemCards_Click;
                }
                else
                {
                    menuItemCards.Click -= menuItemCards_Click;
                    menuItemCardAll.Click += menuItemCards_Click;
                }


                AllowedStack.PopulateSpeciesMenu(menuItemIndAll, indSpecies_Click, (spcRow) => {

                    return AllowedStack.QuantityIndividual(spcRow);

                });
                AllowedStack.PopulateSpeciesMenu(menuItemIndSuggested, indSuggested_Click, (spcRow) => {

                    TreatmentSuggestion sugg = AllowedStack.GetTreatmentSuggestion(spcRow, data.Individual.AgeColumn);
                    return (sugg == null) ? 0 : sugg.GetSuggested().Length;

                });

                AllowedStack.PopulateSpeciesMenu(menuItemLog, logSpecies_Click, (spcRow) => {

                    return AllowedStack.GetLogRows(spcRow).Length;

                });

                AllowedStack.PopulateSpeciesMenu(menuItemGrowthCohorts, speciesGrowthCohorts_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemComposition, speciesComposition_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemMortalityCohorts, speciesMortalityCohorts_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemExtrapolation, speciesStockExtrapolation_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemVpa, speciesStockVpa_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemMSYR, speciesMSYR_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemMSY, speciesMSY_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemPrediction, speciesPrediction_Click);

                updateQty(FullStack.Quantity());

                if (!modelCalc.IsBusy && !isClosing)
                {
                    IsBusy = true;
                    processDisplay.StartProcessing(data.Species.Count, Wild.Resources.Interface.Interface.ModelCalc);
                    modelCalc.RunWorkerAsync();
                }
            }

            statusBio.Visible = data.IsBioLoaded;
        }

        private void updateQty(double q)
        {
            if (q == 0)
            {
                labelQtyValue.Text = Constants.Null;
                statusQuantity.ResetFormatted(Constants.Null);
            }
            else
            {
                labelQtyValue.Text = Wild.Service.GetFriendlyQuantity((int)q);
                statusQuantity.ResetFormatted(Wild.Service.GetFriendlyQuantity((int)q));
            }
        }

        private void updateMass(double w)
        {
            if (w == 0)
            {
                labelWgtValue.Text = Constants.Null;
                statusMass.ResetFormatted(Constants.Null);
            }
            else
            {
                labelWgtValue.Text = Wild.Service.GetFriendlyMass(w * 1000);
                statusMass.ResetFormatted(Wild.Service.GetFriendlyMass(w * 1000));
            }
        }

        private void applyBio()
        {
            if (tabPageSpcStats.Parent != null)
            {
                species_Changed(spreadSheetSpcStats, new EventArgs());
            }

            if (tabPageInd.Parent != null)
            {
                processDisplay.StartProcessing(spreadSheetInd.RowCount, Wild.Resources.Interface.Process.SpecApply);
                specTipper.RunWorkerAsync();
            }
        }

        private void logSpecies_Click(object sender, EventArgs e)
        {
            loadLog((Data.SpeciesRow)((ToolStripMenuItem)sender).Tag);
        }

        private void indSpecies_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;

            individualSpecies = speciesRow;

            IsBusy = true;
            spreadSheetInd.StartProcessing(AllowedStack.QuantityIndividual(speciesRow),
                Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            foreach (Data.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync(speciesRow);
        }

        private void indSuggested_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            TreatmentSuggestion sugg = AllowedStack.GetTreatmentSuggestion(speciesRow, data.Individual.AgeColumn);
            if (sugg != null) loadIndividuals(ModifierKeys.HasFlag(Keys.Shift) ? sugg.GetAll() : sugg.GetSuggested());
        }



        public DialogResult CheckAndSave()
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

        public void SaveCards()
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(changedCards.Count, Wild.Resources.Interface.Process.CardsSaving);
            dataSaver.RunWorkerAsync();
        }  


        public void LoadCards(params string[] entries)
        {
            IsBusy = true;
            processDisplay.StartProcessing(entries.Length, Wild.Resources.Interface.Process.CardsLoading);            
            loaderData.RunWorkerAsync(entries);
        }
                

        private bool loadCardAddt(SpreadSheet spreadSheet)
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

        private void setCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns)
        {
            foreach (DataGridViewColumn gridColumn in gridColumns)
            {
                if (gridColumn.Name.StartsWith("Var_")) continue;
                setCardValue(cardRow, gridRow, gridColumn);
            }
        }

        private void setCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        {
            setCardValue(cardRow, gridRow, gridColumn, gridColumn.Name);
        }

        private void setCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn, string field)
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


        private void rememberChanged(Data.CardRow cardRow)
        {
            if (!changedCards.Contains(cardRow)) { changedCards.Add(cardRow); }
            menuItemSave.Enabled = IsChanged;
        }
    }
}
