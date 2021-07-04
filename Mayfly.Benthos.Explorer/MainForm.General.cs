using Mayfly.Benthos;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Wild;
using Mayfly.Wild.Controls;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        public Data data = new Data();

        public CardStack FullStack { get; private set; }

        public bool IsBusy
        {
            get
            {
                return busy;
            }

            set
            {
                menuItemCommunity.Enabled = 
                    !value;

                tabControl.AllowDrop =
                    spreadSheetInd.Enabled = 
                    !value;

                foreach (Control control in tabPageInfo.Controls)
                {
                    control.Enabled = !value;
                }

                foreach (Control control in new List<Control>{ 
                    spreadSheetCard, //menuItemCards,
                    spreadSheetSpc, //menuItemSpc,
                    spreadSheetLog, 
                    spreadSheetInd, 
                    buttonSelectLog, 
                    buttonSpcFull,
                    buttonSelectInd })
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

                menuItemSaveSet.Enabled =
                menuItemSample.Enabled =
                menuItemCommunity.Enabled =
                    !value;
            }
        }

        public bool IsChanged
        {
            get
            {
                return ChangedCards.Count > 0;
            }
        }


        private bool DietExplorer
        { get; set; }

        public event CardRowSaveEventHandler CardRowSaved;

        private SpeciesKey SpeciesIndex
        { get; set; }



        List<Data.LogRow> selectedLogRows;

        bool busy;

        bool empty;

        List<Data.CardRow> ChangedCards = new List<Data.CardRow>();

        bool IsClosing = false;



        private ResourceManager localizer = new ResourceManager(typeof(MainForm));



        private void UpdateSummary()
        {
            FullStack = new CardStack(data);

            IsEmpty = data.Card.Count == 0;


            if (IsEmpty)
            {
                this.Text = DietExplorer ? Resources.Interface.DietTitle : EntryAssemblyInfo.Title;
                labelArtefacts.Visible = pictureBoxArtefacts.Visible = false;

                labelDatesValue.Text = Constants.Null;
                labelCardsValue.Text = Constants.Null;
                labelCards.Text = string.Empty;

                statusQuantity.ResetFormatted(0);
                statusMass.ResetFormatted(0);

                IsBusy = false;
            }
            else
            {
                UserSettings.Interface.SaveDialog.FileName = FullStack.FriendlyName;
                this.ResetText(FullStack.FriendlyName, DietExplorer ? Resources.Interface.DietTitle : EntryAssemblyInfo.Title);

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

                if (FullStack.GetDates().Count() > 1)
                {
                    labelDatesValue.ResetFormatted(FullStack.EarliestEvent, FullStack.LatestEvent);
                }
                else if (FullStack.GetDates().Count() == 1)
                {
                    labelDatesValue.Text = FullStack.EarliestEvent.ToString("D");
                }
                else
                {
                    labelDatesValue.Text = string.Empty;
                }

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
                    listViewSamplers.CreateItem(samplerRow.ID.ToString(), samplerRow.ShortName);
                }

                #endregion

                #region Authorities

                listViewInvestigators.Items.Clear();

                foreach (string investigator in FullStack.GetInvestigators())
                {
                    listViewInvestigators.CreateItem(investigator, investigator);
                }

                #endregion

                menuItemLoadIndividuals.AddSpeciesMenus(FullStack, speciesInd_Click);

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
            LoadIndLog(speciesRow);
        }

        private void briefBase_Click(object sender, EventArgs e)
        {
            Species.SpeciesKey.BaseRow baseRow = (Species.SpeciesKey.BaseRow)((ToolStripMenuItem)sender).Tag;
            Report report = new Report(Resources.Reports.Community.Title);
            FullStack.AddBrief(report, baseRow);
            report.Run();
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
            spreadSheetCard.StartProcessing(entries.Length, Wild.Resources.Interface.Process.CardsLoading);
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
                    spreadSheet.InsertColumn(gridColumn, i, gridColumn.Name.TrimStart("columnCard".ToCharArray()));
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
            try
            {
                object s = cardRow.Get(field);

                if (s == null) {
                    DataGridViewRow line = GetLine(cardRow);
                    if (line != null) gridRow.Cells[gridColumn.Index].Value = line.Cells[field].Value;
                    else gridRow.Cells[gridColumn.Index].Value = "Error";
                }
                else switch (field)
                {
                    case "Abundance":
                        // s in (1000ind./m2) or (1000ind./g)
                        gridRow.Cells[gridColumn.Index].Value = (double)(s) * (DietExplorer ? 1000 : 1);
                        break;
                    case "Biomass":
                        // s in (g/m2) or (g/g)
                        gridRow.Cells[gridColumn.Index].Value = (double)(s) * (DietExplorer ? 10000 : 1);
                        break;
                    default:
                        gridRow.Cells[gridColumn.Index].Value = s;
                        break;
                }
            }
            catch (ArgumentException)
            {
                if (spreadSheetCard.Columns[field] == null) return;
                DataGridViewRow cardGridRow = columnCardID.GetRow(cardRow.ID, true);
                if (cardGridRow == null) return;
                object value = cardGridRow.Cells[field].Value;
                if (value == null) return;
                gridRow.Cells[gridColumn.Index].Value = value;
            }

            //if (Data.Card.Columns[field] != null)
            //{
            //    if (cardRow.IsNull(field))
            //    {
            //        gridRow.Cells[gridColumn.Index].Value = null;
            //    }
            //    else switch (field)
            //        {
            //            case "CrossSection":
            //                if (cardRow.IsWaterIDNull()) gridRow.Cells[gridColumn.Index].Value = null;
            //                else gridRow.Cells[gridColumn.Index].Value = Wild.Service.CrossSection((WaterType)cardRow.WaterRow.Type, cardRow.CrossSection);
            //                break;
            //            case "Bank":
            //                gridRow.Cells[gridColumn.Index].Value = Wild.Service.Bank(cardRow.Bank);
            //                break;
            //            case "Sampler":
            //                gridRow.Cells[gridColumn.Index].Value = Benthos.Service.Sampler(cardRow.Sampler).ShortName;
            //                gridRow.Cells[gridColumn.Index].ToolTipText = Benthos.Service.Sampler(cardRow.Sampler).Sampler;
            //                break;
            //            default:
            //                gridRow.Cells[gridColumn.Index].Value = cardRow[field];
            //                break;
            //        }
            //}
            //else if (Data.Factor.FindByFactor(field) != null)
            //{
            //    Data.FactorValueRow factorValueRow = Data.FactorValue.FindByCardIDFactorID(cardRow.ID, Data.Factor.FindByFactor(field).ID);
            //    gridRow.Cells[gridColumn.Index].Value = factorValueRow == null ? double.NaN : factorValueRow.Value;
            //}
            //else
            //{
            //    switch (field)
            //    {
            //        case "Investigator":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.Investigator;
            //            break;
            //        case "Water":
            //            if (cardRow.IsWaterIDNull() || cardRow.WaterRow.IsWaterNull()) gridRow.Cells[gridColumn.Index].Value = null;
            //            else gridRow.Cells[gridColumn.Index].Value = cardRow.WaterRow.Water;
            //            break;
            //        case "Substrate":
            //            if (cardRow.IsSubstrateNull()) gridRow.Cells[gridColumn.Index].Value = null;
            //            else gridRow.Cells[gridColumn.Index].Value = cardRow.SampleSubstrate.TypeName;
            //            break;
            //        case "Wealth":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.Wealth();
            //            break;
            //        case "Quantity":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.Quantity;
            //            break;
            //        case "Abundance":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.Abundance / (DietExplorer ? 1d : 1000d);
            //            break;
            //        case "Mass":
            //            gridRow.Cells[gridColumn.Index].Value = Data.Mass(cardRow);
            //            break;
            //        case "Biomass":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.Biomass * (DietExplorer ? 10000d : 1d);
            //            break;
            //        case "DiversityA":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.DiversityA();
            //            break;
            //        case "DiversityB":
            //            gridRow.Cells[gridColumn.Index].Value = cardRow.DiversityB();
            //            break;
            //        default: // A column with custom performed data
            //            if (spreadSheetCard.Columns[field] == null) break;
            //            DataGridViewRow cardGridRow = columnCardID.GetRow(cardRow.ID, true);
            //            if (cardGridRow == null) break;
            //            object value = cardGridRow.Cells[field].Value;
            //            if (value == null) break;
            //            gridRow.Cells[gridColumn.Index].Value = value;
            //            break;
            //    }
            //}
        }

        private delegate void ValueSetEventHandler(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);


        private void RememberChanged(Data.CardRow cardRow)
        {
            if (!ChangedCards.Contains(cardRow)) { ChangedCards.Add(cardRow); }
            menuItemSave.Enabled = IsChanged;
        }


        public void SetSpeciesIndex(string indexPath)
        {
            if (indexPath != null)
            {
                SpeciesIndex = new SpeciesKey();
                SpeciesIndex.Read(indexPath);

                speciesValidator.IndexPath =
                    speciesLog.IndexPath =
                    speciesInd.IndexPath =
                    indexPath;

                comboBoxLogTaxa.AddBaseList(SpeciesIndex);
                comboBoxSpcTaxa.AddBaseList(SpeciesIndex);

                menuItemSpcTaxa.AddBaseMenus(SpeciesIndex, baseItem_Click);
                menuItemBrief.AddBaseMenus(SpeciesIndex, briefBase_Click);
            }
            else
            {
                //throw new ArgumentNullException(nameof(indexPath),
                //    "Nutrients reference is absent. It was moved or deleted since last successfull launch.");

                Log.Write(EventType.ExceptionThrown,
                    "Reference {0} is absent. It was moved or deleted since last successfull launch.", indexPath);
            }
        }

        public void PerformDietExplorer(string title)
        {
            DietExplorer = true;

            this.Text = String.Format("{0} - {1}", title, Resources.Interface.DietTitle);

            tabPageArtefacts.Parent = tabControl;
            tabPageArtefacts.Parent = null;

            columnCardSampler.Visible =
                columnCardSubstrate.Visible =
                columnCardMesh.Visible =
                columnCardDepth.Visible = false;

            columnCardSquare.HeaderText = Resources.Interface.DietSquare;
            columnCardSquare.DefaultCellStyle.Format = Mayfly.Service.Mask(0);

            columnCardAbundance.HeaderText = Resources.Interface.DietAbundance;

            string dietFormat = "N2";

            columnCardBiomass.HeaderText = Resources.Interface.DietBiomass;
            columnCardBiomass.DefaultCellStyle.Format = dietFormat;

            columnSpcAbundance.HeaderText = Resources.Interface.DietTxAbundance;
            columnSpcAbundance.DefaultCellStyle.Format = Mayfly.Service.Mask(3);

            columnSpcBiomass.HeaderText = Resources.Interface.DietTxBiomass;
            columnSpcBiomass.DefaultCellStyle.Format = dietFormat;

            columnLogAbundance.HeaderText = Resources.Interface.DietTxAbundance;
            columnLogAbundance.DefaultCellStyle.Format = Mayfly.Service.Mask(3);

            columnLogBiomass.HeaderText = Resources.Interface.DietTxBiomass;
            columnLogBiomass.DefaultCellStyle.Format = dietFormat;

            //menuItemLoadCards_Click(this, new EventArgs());
        }
    }
}
