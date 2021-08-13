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

        private List<Data.LogRow> selectedLogRows;
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
                labelWgtValue.Text = Constants.Null;
                labelQtyValue.Text = Constants.Null;

                statusQuantity.ResetFormatted(Constants.Null);
                statusMass.ResetFormatted(Constants.Null);

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
                    listViewWaters.CreateItem(waterRow.ID.ToString(), waterRow.IsWaterNull() ? Waters.Resources.Interface.Unnamed : waterRow.Water, waterRow.Type - 1);
                }

                listViewSamplers.Items.Clear();
                foreach (Samplers.SamplerRow samplerRow in FullStack.GetSamplers())
                {
                    listViewSamplers.CreateItem(samplerRow.ID.ToString(), samplerRow.Sampler);
                }

                listViewInvestigators.Items.Clear();
                foreach (string investigator in FullStack.GetInvestigators())
                {
                    listViewInvestigators.CreateItem(investigator, investigator);
                }

                AllowedStack.PopulateSpeciesMenu(menuItemLoadIndividuals, speciesInd_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemGrowthCohorts, speciesGrowthCohorts_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemComposition, speciesComposition_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemMortalityCohorts, speciesMortalityCohorts_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemExtrapolation, speciesStockExtrapolation_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemVpa, speciesStockVpa_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemMSYR, speciesMSYR_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemMSY, speciesMSY_Click);
                AllowedStack.PopulateSpeciesMenu(menuItemPrediction, speciesPrediction_Click);

                if (!modelCalc.IsBusy && !isClosing)
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
