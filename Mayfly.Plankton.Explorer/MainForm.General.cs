using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mayfly.Plankton.Explorer
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
                menuItemCenosis.Enabled = 
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
                menuItemCenosis.Enabled =
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

        private SpeciesKey SpeciesIndex
        { get; set; }



        List<Data.LogRow> selectedLogRows;

        bool busy;

        bool empty;

        List<string> ChangedCards = new List<string>();

        bool IsClosing = false;



        private ResourceManager localizer = new ResourceManager(typeof(MainForm));



        private void UpdateSummary()
        {
            FullStack = new CardStack(data);

            IsEmpty = data.Card.Count == 0;

            if (IsEmpty)
            {
                Text = EntryAssemblyInfo.Title;
                labelArtefacts.Visible = pictureBoxArtefacts.Visible = false;

                statusQuantity.ResetFormatted(0);
                statusMass.ResetFormatted(0);

                labelSample.ResetFormatted(0);

                IsBusy = false;
            }
            else
            {
                string friendly = FileSystem.GetFriendlyCommonName(data.GetFilenames());

                if (string.IsNullOrWhiteSpace(friendly))
                {
                    this.Text = EntryAssemblyInfo.Title;
                }
                else
                {
                    this.Text = String.Format("{0} - {1}", friendly, EntryAssemblyInfo.Title);
                    UserSettings.Interface.SaveDialog.FileName = friendly;
                }

                spreadSheetCard.ClearInsertedColumns();

                foreach (Data.FactorRow factorRow in data.Factor)
                {
                    DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                    gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
                }

                #region Update general values

                labelCardsNumber.Text = data.Card.Count.ToString();

                if (FullStack.GetDates().Count() > 1)
                {
                    labelDateStart.Text = FullStack.EarliestEvent.ToLongDateString();
                    labelDateEnd.Text = FullStack.LatestEvent.ToLongDateString();
                }
                else if (FullStack.GetDates().Count() == 1)
                {
                    labelDateStart.Text = FullStack.EarliestEvent.ToLongDateString();
                    labelDateEnd.Text = string.Empty;
                }
                else
                {
                    labelDateStart.Text = string.Empty;
                    labelDateEnd.Text = string.Empty;
                }

                labelSample.Text = FullStack.Quantity().ToString();

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
                foreach (Samplers.SamplerRow samplerRow in FullStack.GetSamplers(Plankton.UserSettings.SamplersIndex))
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

                //if (!modelCalc.IsBusy)
                //{
                //    IsBusy = true;
                //    processDisplay.StartProcessing(data.Species.Count, Wild.Resources.Interface.Interface.ModelCalc);
                //    modelCalc.RunWorkerAsync();
                //}
            }
        }

        private void speciesInd_Click(object sender, EventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)((ToolStripMenuItem)sender).Tag;
            LoadIndLog(speciesRow);
        }

        private void briefBase_Click(object sender, EventArgs e)
        {
            //SpeciesKey.BaseRow baseRow = (SpeciesKey.BaseRow)((ToolStripMenuItem)sender).Tag;
            //Report report = new Report(Resources.Reports.Cenosis.Title);
            //FullStack.AddBrief(report, baseRow);
            //report.Run();
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
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsSaving);

            dataSaver.RunWorkerAsync();
        }

        private void SaveCards(BackgroundWorker worker, DoWorkEventArgs e)
        {
            int index = 0;
            foreach (Data.CardRow cardRow in data.Card)
            {
                if (ChangedCards.Contains(cardRow.Path))
                {
                    SaveCard(cardRow);
                }

                index++;
                worker.ReportProgress(index);
            }
        }

        private void SaveCard(Data.CardRow cardRow)
        {
            if (cardRow.Path == null) return;

            if (ChangedCards.Contains(cardRow.Path))
            {
                Data data = cardRow.SingleCardDataset();
                if (data != null)
                {
                    data.WriteToFile(cardRow.Path);
                    ChangedCards.Remove(cardRow.Path);
                }
            }
        }


        
        public void LoadCards(string[] entries)
        {
            IsBusy = true;
            string[] filenames = FileSystem.MaskedNames(entries, Plankton.UserSettings.Interface.Extension);
            spreadSheetCard.StartProcessing(filenames.Length, Wild.Resources.Interface.Process.CardsLoading);
            loaderData.RunWorkerAsync(filenames);
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

                switch (field)
                {
                    case "Abundance":
                        gridRow.Cells[gridColumn.Index].Value = (double)(s) / (DietExplorer ? 1d : 1000d);
                        break;
                    case "Biomass":
                        gridRow.Cells[gridColumn.Index].Value = (double)(s) * (DietExplorer ? 10000d : 1d);
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

        private void UpdateSpeciesChart(Chart chart, bool showLegend)
        {
            if (chart.Series.Count != 1)
                throw new ArgumentException("Chart should contain single data series");

            chart.Series[0].Points.Clear();
            chart.Legends.Clear();

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                chart.Series[0].Points.Add(SpeciesDataPoint(speciesRow));
            }

            chart.Series[0].Sort(PointSortOrder.Descending);

            while (chart.Series[0].Points.Count > 6)
            {
                chart.Series[0].Points.RemoveAt(6);
            }

            chart.Series[0].Sort(PointSortOrder.Ascending);

            if (showLegend)
            {
                chart.Legends.Add(new Legend());
            }
        }

        private DataPoint SpeciesDataPoint(Data.SpeciesRow speciesRow)
        {
            DataPoint dataPoint = new DataPoint();
            double quantity = speciesRow.TotalQuantity;
            dataPoint.SetCustomProperty("Species", speciesRow.Species);

            dataPoint.YValues[0] = quantity;
            dataPoint.ToolTip = string.Format("{0}\n{1}", speciesRow.Species,
                quantity.ToString((string)localizer.GetObject("StatusQuantity.Text")));

            dataPoint.AxisLabel =
                //dataPoint.Label =
                speciesRow.Species;

            return dataPoint;
        }

        public void SetSpeciesIndex(string indexPath)
        {
            if (indexPath != null)
            {
                SpeciesIndex = new SpeciesKey();
                SpeciesIndex.ReadXml(indexPath);

                speciesValidator.IndexPath =
                    speciesLog.IndexPath =
                    speciesInd.IndexPath =
                    indexPath;

                comboBoxLog.AddBaseList(SpeciesIndex);
                comboBoxSpc.AddBaseList(SpeciesIndex);

                menuItemSpc.AddBaseMenus(SpeciesIndex, baseItem_Click);
                //menuItemBrief.AddBaseMenus(SpeciesIndex, briefBase_Click);
            }
            else
            {
                Log.Write(EventType.ExceptionThrown, "Reference {0} is absent. It was moved or deleted since last successfull launch.", indexPath);
            }
        }
        
        private void RememberChanged(Data.CardRow cardRow)
        {
            if (cardRow.Path == null) return;

            if (!ChangedCards.Contains(cardRow.Path))
            {
                ChangedCards.Add(cardRow.Path);
            }

            menuItemSave.Enabled = IsChanged;
        }

        public void PerformDietExplorer(string trophicsIndex)
        {
            DietExplorer = true;

            Text = Resources.Interface.DietTitle;

            SpeciesIndex = new SpeciesKey();
            SpeciesIndex.ReadXml(trophicsIndex);
            tabPageArtefacts.Parent = tabControl;
            speciesValidator.IndexPath = trophicsIndex;
            tabPageArtefacts.Parent = null;

            columnCardSampler.Visible =
                columnCardMesh.Visible =
                columnCardDepth.Visible = false;

            columnCardVolume.HeaderText = Resources.Interface.DietVolume;
            columnCardVolume.DefaultCellStyle.Format = Mayfly.Service.Mask(0);

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

            LoadTaxaList();

            menuItemLoadCards_Click(this, new EventArgs());
        }
    }
}
