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
using Mayfly.Geographics;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        public Data data = new Data(Benthos.UserSettings.SpeciesIndex, Benthos.UserSettings.SamplersIndex);

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

                menuItemBackup.Enabled =
                    menuItemSaveSet.Enabled =
                    !empty;

                //menuSample.Enabled =
                //    menuInstant.Enabled = !empty;

                //foreach (ToolStripItem item in menuSurvey.DropDownItems)
                //{
                //    if (item == menuItemSurveyInput) continue;
                //    if (item == menuItemSpawning) continue;
                //    item.Enabled = !empty;
                //}

                menuItemExportBio.Enabled = !empty;

                //foreach (ToolStripItem item in menuSurvey.DropDownItems)
                //{
                //    if (item == menuItemSurveyInput) continue;
                //    item.Enabled = !empty;
                //}
            }
        }

        public bool IsChanged
        {
            get
            {
                return ChangedCards.Count > 0;
            }
        }

        bool busy;
        bool empty;
        bool isClosing = false;


        private void updateSummary()
        {
            FullStack = new CardStack(data);
            IsEmpty = data.Card.Count == 0;

            if (IsEmpty)
            {
                this.ResetText(DietExplorer ? Resources.Interface.DietTitle : EntryAssemblyInfo.Title);
                
                labelArtifacts.Visible = pictureBoxArtifacts.Visible = false;
                labelCardCountValue.Text = Constants.Null;

                updateQty(0);
                updateMass(0);

                data.RefreshBios();

                IsBusy = false;
            }
            else
            {
                UserSettings.Interface.SaveDialog.FileName = FullStack.FriendlyName;
                this.ResetText(FullStack.FriendlyName, DietExplorer ? Resources.Interface.DietTitle : EntryAssemblyInfo.Title);

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
                            loadCards(FullStack.GetStack(waterRow));
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
                            loadCards(FullStack.GetStack(samplerRow));
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
                        menuItem.Click += (sender, e) =>
                        {
                            loadCards(FullStack.GetStack("Investigator", investigator));
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


                FullStack.PopulateSpeciesMenu(menuItemIndividuals, indSpecies_Click, (spcRow) =>
                {
                    return FullStack.QuantityIndividual(spcRow);
                });

                FullStack.PopulateSpeciesMenu(menuItemLog, logSpecies_Click, (spcRow) =>
                {

                    return FullStack.GetLogRows(spcRow).Length;

                });

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
                labelWgtValue.Text = Wild.Service.GetFriendlyMass(w);
                statusMass.ResetFormatted(Wild.Service.GetFriendlyMass(w));
            }
        }

        private void updateArtifacts()
        {
            if (tabPageCard.Parent != null)
            {
                foreach (DataGridViewRow gridRow in spreadSheetCard.Rows)
                {
                    updateCardArtifacts(gridRow);
                }
            }

            if (tabPageSpc.Parent != null)
            {
                foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
                {
                    updateSpeciesArtifacts(gridRow);
                }
            }

            if (tabPageLog.Parent != null)
            {
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    updateLogArtifacts(gridRow);
                }
            }
        }

        private void applyBio()
        {
            //if (tabPageInd.Parent != null)
            //{
            //    spreadSheetInd_DisplayChanged(spreadSheetInd, new ScrollEventArgs(ScrollEventType.SmallDecrement, 0));
            //}
        }

        private void logSpecies_Click(object sender, EventArgs e)
        {
            loadLog((SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag);
        }

        private void indSpecies_Click(object sender, EventArgs e)
        {
            loadIndividuals((SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag);
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


        private void rememberChanged(Data.CardRow cardRow)
        {
            if (!changedCards.Contains(cardRow)) { changedCards.Add(cardRow); }
            menuItemSave.Enabled = IsChanged;
        }







        #region Species

        SpeciesKey.BaseRow baseSpc;



        private void loadTaxaList()
        {
            // Clear list
            comboBoxSpcTaxa.Items.Clear();
            menuItemSpcTaxa.DropDownItems.Clear();

            if (Benthos.UserSettings.SpeciesIndex == null) return;

            // Fill list
            foreach (SpeciesKey.BaseRow baseRow in Benthos.UserSettings.SpeciesIndex.Base)
            {
                comboBoxSpcTaxa.Items.Add(baseRow);

                ToolStripMenuItem item = new ToolStripMenuItem(baseRow.BaseName);
                item.Click += (sender, o) =>
                {
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

                        SpeciesKey.SpeciesRow speciesRow = (gridRow.Cells[columnSpcSpc.Index].Value as Data.SpeciesRow).KeyRecord;

                        if (speciesRow == null)
                        {
                            gridRow.Cells[gridColumn.Index].Value = null;
                        }
                        else
                        {
                            SpeciesKey.TaxaRow taxaRow = speciesRow.GetTaxon(baseRow);
                            if (taxaRow != null) gridRow.Cells[gridColumn.Index].Value = taxaRow.TaxonName;
                        }
                    }
                };
                menuItemSpcTaxa.DropDownItems.Add(item);
            }

            comboBoxSpcTaxa.Enabled = comboBoxSpcTaxa.Items.Count > 0;
            menuItemSpcTaxa.Enabled = menuItemSpcTaxa.DropDownItems.Count > 0;
        }

        private void loadSpc()
        {
            IsBusy = true;
            spreadSheetSpc.StartProcessing(baseSpc == null ? data.Species.Count + 1 : baseSpc.GetTaxaRows().Length + 1,
                Wild.Resources.Interface.Process.SpeciesProcessing);

            spreadSheetSpc.Rows.Clear();

            loaderSpc.RunWorkerAsync();
        }



        private SpeciesKey.SpeciesRow findSpeciesRow(DataGridViewRow gridRow)
        {
            //return baseSpc == null ? Benthos.UserSettings.SpeciesIndex.Species.FindByID((int)gridRow.Cells[columnSpcID.Index].Value) : null;
            return baseSpc == null ? (SpeciesKey.SpeciesRow)gridRow.Cells[columnSpcSpc.Index].Value : null;
        }

        private void updateSpeciesArtifacts(DataGridViewRow gridRow)
        {
            if (gridRow == null) return;

            if (!UserSettings.CheckConsistency) return;

            SpeciesConsistencyChecker artifact = findSpeciesRow(gridRow).CheckConsistency(FullStack);

            if (artifact.ArtifactsCount > 0)
            {
                ((TextAndImageCell)gridRow.Cells[columnSpcSpc.Index]).Image = ConsistencyChecker.GetImage(artifact.WorstCriticality);
                gridRow.Cells[columnSpcSpc.Index].ToolTipText = artifact.GetNotices(true).Merge(System.Environment.NewLine);
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnSpcSpc.Index]).Image = null;
                gridRow.Cells[columnSpcSpc.Index].ToolTipText = string.Empty;
            }

        }



        private SpeciesKey.SpeciesRow[] getSpeciesRows(IList rows)
        {
            spreadSheetLog.EndEdit();
            List<SpeciesKey.SpeciesRow> result = new List<SpeciesKey.SpeciesRow>();
            foreach (DataGridViewRow gridRow in rows)
            {
                if (gridRow.IsNewRow) continue;
                result.Add(findSpeciesRow(gridRow));
            }

            return result.ToArray();
        }

        #endregion



        #region Cards

        private List<Data.CardRow> changedCards = new List<Data.CardRow>();

        private void loadCards(CardStack stack)
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);
            spreadSheetCard.Rows.Clear();

            loaderCard.RunWorkerAsync(stack);
        }

        private void loadCards()
        {
            loadCards(FullStack);
        }

        private Data.CardRow findCardRow(DataGridViewRow gridRow)
        {
            if (gridRow == null) return null;

            return data.Card.FindByID((int)gridRow.Cells[columnCardID.Index].Value);
        }

        private void updateCardRow(DataGridViewRow gridRow)
        {
            Data.CardRow cardRow = findCardRow(gridRow);

            if (cardRow == null) return;

            setCardValue(cardRow, gridRow, columnCardInvestigator, "Investigator");
            setCardValue(cardRow, gridRow, columnCardLabel, "Label");
            setCardValue(cardRow, gridRow, columnCardWater, "Water");
            setCardValue(cardRow, gridRow, columnCardWhen, "When");
            setCardValue(cardRow, gridRow, columnCardWhere, "Where");

            setCardValue(cardRow, gridRow, columnCardWeather, "Weather");
            setCardValue(cardRow, gridRow, columnCardTempSurface, "Surface");

            setCardValue(cardRow, gridRow, columnCardSampler, "Sampler");
            setCardValue(cardRow, gridRow, columnCardMesh, "Mesh");
            setCardValue(cardRow, gridRow, columnCardSquare, "Square");

            setCardValue(cardRow, gridRow, columnCardDepth, "Depth");
            setCardValue(cardRow, gridRow, columnCardWealth, "Wealth");
            setCardValue(cardRow, gridRow, columnCardQuantity, "Quantity");
            setCardValue(cardRow, gridRow, columnCardMass, "Mass");
            setCardValue(cardRow, gridRow, columnCardAbundance, "Abundance");
            setCardValue(cardRow, gridRow, columnCardBiomass, "Biomass");
            setCardValue(cardRow, gridRow, columnCardDiversityA, "DiversityA");
            setCardValue(cardRow, gridRow, columnCardDiversityB, "DiversityB");

            setCardValue(cardRow, gridRow, columnCardComments, "Comments");

            foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
            {
                setCardValue(cardRow, gridRow, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }

            updateCardArtifacts(gridRow);
        }

        private void updateCardArtifacts(DataGridViewRow gridRow)
        {
            if (gridRow == null) return;

            if (!UserSettings.CheckConsistency) return;

            CardConsistencyChecker artifact = findCardRow(gridRow).CheckConsistency();

            if (artifact.SquareCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardSquare.Index]).Image = ConsistencyChecker.GetImage(artifact.SquareCriticality);
                gridRow.Cells[columnCardSquare.Index].ToolTipText = Resources.Artifact.Square;
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardSquare.Index]).Image = null;
                gridRow.Cells[columnCardSquare.Index].ToolTipText = string.Empty;
            }

            if (artifact.WhereCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardWhere.Index]).Image = ConsistencyChecker.GetImage(artifact.WhereCriticality);
                gridRow.Cells[columnCardWhere.Index].ToolTipText = Wild.Resources.Artifact.Where;
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardSquare.Index]).Image = null;
                gridRow.Cells[columnCardSquare.Index].ToolTipText = string.Empty;
            }


            if (artifact.LogArtifacts.Count > 0)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardWealth.Index]).Image = ConsistencyChecker.GetImage(ConsistencyChecker.GetWorst(artifact.LogWorstCriticality));
                gridRow.Cells[columnCardWealth.Index].ToolTipText = LogConsistencyChecker.GetCommonNotices(artifact.LogArtifacts).Merge();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardWealth.Index]).Image = null;
                gridRow.Cells[columnCardWealth.Index].ToolTipText = string.Empty;
            }

            if (artifact.IndividualArtifacts.Count > 0)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardQuantity.Index]).Image = ConsistencyChecker.GetImage(ConsistencyChecker.GetWorst(artifact.IndividualWorstCriticality));
                gridRow.Cells[columnCardQuantity.Index].ToolTipText = IndividualConsistencyChecker.GetCommonNotices(artifact.IndividualArtifacts).Merge();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardQuantity.Index]).Image = null;
                gridRow.Cells[columnCardQuantity.Index].ToolTipText = string.Empty;
            }
        }

        private void saveCardRow(DataGridViewRow gridRow)
        {
            Data.CardRow cardRow = findCardRow(gridRow);

            if (cardRow == null) return;

            object wpt = (object)gridRow.Cells[columnCardWhere.Name].Value;
            if (wpt == null) cardRow.SetWhereNull();
            else cardRow.Where = ((Waypoint)wpt).Protocol;

            object mesh = (object)gridRow.Cells[columnCardMesh.Name].Value;
            if (mesh == null) cardRow.SetMeshNull();
            else cardRow.Mesh = (int)mesh;

            //object square = (object)gridRow.Cells[columnCardSquare.Name].Value;
            //if (square == null) cardRow.SetSquareNull();
            //else cardRow.Square = (double)square;

            object depth = (object)gridRow.Cells[columnCardDepth.Name].Value;
            if (depth == null) cardRow.SetDepthNull();
            else cardRow.Depth = (double)depth;

            object comments = gridRow.Cells[columnCardComments.Name].Value;
            if (comments == null) cardRow.SetCommentsNull();
            else cardRow.Comments = (string)comments;

            // Additional factors
            foreach (DataGridViewColumn gridColumn in spreadSheetCard.GetInsertedColumns())
            {
                Data.FactorRow factorRow = data.Factor.FindByFactor(gridColumn.HeaderText);
                if (factorRow == null) continue;
                object factorValue = gridRow.Cells[gridColumn.Name].Value;

                if (factorValue == null)
                {
                    if (factorRow == null) continue;

                    Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null) continue;

                    factorValueRow.Delete();
                }
                else
                {
                    if (factorRow == null)
                    {
                        factorRow = data.Factor.AddFactorRow(gridColumn.HeaderText);
                    }

                    Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null)
                    {
                        data.FactorValue.AddFactorValueRow(cardRow, factorRow, (double)factorValue);
                    }
                    else
                    {
                        factorValueRow.Value = (double)factorValue;
                    }
                }
            }

            rememberChanged(cardRow);

            updateCardArtifacts(gridRow);

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (tabPageLog.Parent != null)
                {
                    DataGridViewRow gridLogRow = columnLogID.GetRow(logRow.ID);
                    if (gridLogRow != null) updateLogRow(gridLogRow);
                }

                foreach (Data.IndividualRow indRow in logRow.GetIndividualRows())
                {
                    if (tabPageInd.Parent != null)
                    {
                        DataGridViewRow gridIndRow = columnIndID.GetRow(indRow.ID);
                        if (gridIndRow != null) updateIndividualRow(gridIndRow);
                    }
                }
            }
        }

        private CardStack getCardStack(IList rows)
        {
            spreadSheetCard.EndEdit();

            CardStack stack = new CardStack();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (gridRow.IsNewRow) continue;

                stack.Add(findCardRow(gridRow));
            }

            return stack;
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
        }

        private delegate void ValueSetEventHandler(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);

        #endregion



        #region Log

        SpeciesKey.BaseRow baseLog;

        private void loadLog(Data.LogRow[] logRows)
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(
                baseLog == null ? logRows.Length : (FullStack.Count * (baseLog.GetTaxaRows().Length + 1)),
                Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();
            loaderLog.RunWorkerAsync(logRows);
        }

        private void loadLog()
        {
            loadLog(FullStack.GetLogRows());
            comboBoxLogTaxa.Enabled =
                label1.Enabled = true;
        }

        private void loadLog(SpeciesKey.SpeciesRow[] spcRows, CardStack stack)
        {
            List<Data.LogRow> logRows = new List<Data.LogRow>();

            foreach (SpeciesKey.SpeciesRow spcRow in spcRows)
            {
                logRows.AddRange(stack.GetLogRows(spcRow));
            }

            loadLog(logRows.ToArray());
            comboBoxLogTaxa.Enabled =
                label1.Enabled = false;
        }

        private void loadLog(SpeciesKey.SpeciesRow[] spcRows)
        {
            loadLog(spcRows, FullStack);
        }

        private void loadLog(SpeciesKey.SpeciesRow spcRow)
        {
            loadLog(new SpeciesKey.SpeciesRow[] { spcRow });
        }

        private Data.LogRow findLogRow(DataGridViewRow gridRow)
        {
            return baseLog == null ? data.Log.FindByID((int)gridRow.Cells[columnLogID.Index].Value) : null;
        }

        private void updateLogRow(DataGridViewRow gridRow)
        {
            Data.LogRow logRow = findLogRow(gridRow);

            gridRow.Cells[columnLogSpc.Index].Value = logRow.SpeciesRow;

            if (!logRow.IsQuantityNull())
            {
                gridRow.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
                gridRow.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
            }

            if (!logRow.IsMassNull())
            {
                gridRow.Cells[columnLogMass.Index].Value = logRow.Mass;
                gridRow.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();
            }

            setCardValue(logRow.CardRow, gridRow, spreadSheetLog.GetInsertedColumns());

            updateLogArtifacts(gridRow);
        }

        private void updateLogArtifacts(DataGridViewRow gridRow)
        {
            if (gridRow == null) return;

            if (!UserSettings.CheckConsistency) return;

            LogConsistencyChecker artifact = findLogRow(gridRow).CheckConsistency();

            if (artifact.SpeciesCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnLogSpc.Index]).Image = ConsistencyChecker.GetImage(artifact.SpeciesCriticality);
                gridRow.Cells[columnLogSpc.Index].ToolTipText = Wild.Resources.Artifact.LogSpecies;
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnLogSpc.Index]).Image = null;
                gridRow.Cells[columnLogSpc.Index].ToolTipText = string.Empty;
            }
        }

        private void saveLogRow(DataGridViewRow gridRow)
        {
            if (baseSpc != null) return;

            if (!UserSettings.CheckConsistency) return;

            Data.LogRow logRow = findLogRow(gridRow);

            if (logRow == null) return;

            object qty = gridRow.Cells[columnLogQuantity.Name].Value;
            if (qty == null) logRow.SetQuantityNull();
            else logRow.Quantity = (int)qty;

            object mass = gridRow.Cells[columnLogMass.Name].Value;
            if (mass == null) logRow.SetMassNull();
            else logRow.Mass = (double)mass;

            rememberChanged(logRow.CardRow);

            updateLogRow(gridRow);

            updateQty(FullStack.Quantity());
            updateMass(FullStack.Mass());

            updateLogArtifacts(gridRow);
            if (tabPageCard.Parent != null) updateCardArtifacts(columnCardID.GetRow(logRow.CardID));
        }

        private Data.LogRow[] getLogRows(IList rows)
        {
            spreadSheetLog.EndEdit();
            List<Data.LogRow> result = new List<Data.LogRow>();
            foreach (DataGridViewRow gridRow in rows)
            {
                if (gridRow.IsNewRow) continue;
                result.Add(findLogRow(gridRow));
            }

            return result.ToArray();
        }

        //private DataGridViewRow createTaxonLogRow(Data.CardRow cardRow, SpeciesKey.TaxaRow taxaRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();

        //    result.CreateCells(spreadSheetLog);

        //    result.Cells[columnLogID.Index].Value = cardRow.ID;

        //    int Q = 0;
        //    double W = 0.0;
        //    double A = 0.0;
        //    double B = 0.0;
        //    List<double> abundances = new List<double>();
        //    List<double> biomasses = new List<double>();

        //    foreach (Data.LogRow logRow in cardRow.GetLogRows())
        //    {
        //        if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

        //        if (!logRow.IsQuantityNull())
        //        {
        //            Q += logRow.Quantity;
        //            A += logRow.GetAbundance();
        //            abundances.Add(logRow.GetAbundance());
        //        }

        //        if (!logRow.IsMassNull())
        //        {
        //            W += logRow.Mass;
        //            B += logRow.GetBiomass();
        //            biomasses.Add(logRow.GetBiomass());
        //        }
        //    }

        //    setCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

        //    result.Cells[columnLogSpc.Index].Value = taxaRow.TaxonName;

        //    result.Cells[columnLogQuantity.Index].Value = Q;
        //    result.Cells[columnLogMass.Index].Value = W;
        //    result.Cells[columnLogAbundance.Index].Value = A;
        //    result.Cells[columnLogBiomass.Index].Value = B;
        //    result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
        //    result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

        //    return result;
        //}

        //private void logLoaderTaxa1111_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    // How to analize selectedLogSpcRows???

        //    for (int i = 0; i < selectesLogStack.Count; i++)
        //    {
        //        foreach (SpeciesKey.TaxaRow taxaRow in data.Species.Taxa(baseLog))
        //        {
        //            DataGridViewRow gridRow = createTaxonLogRow(selectesLogStack[i], taxaRow);

        //            if (gridRow == null) continue;

        //            if ((int)gridRow.Cells[columnLogQuantity.Index].Value == 0)
        //            {
        //                spreadSheetLog.SetHidden(gridRow);
        //            }
        //        }

        //    }
        //}

        #endregion



        #region Individuals

        SpeciesKey.SpeciesRow individualSpecies;
        ContinuousBio growthModel;
        ContinuousBio massModel;


        private void loadIndividuals(Data.IndividualRow[] indRows)
        {
            individualSpecies = null;

            IsBusy = true;
            spreadSheetInd.StartProcessing(indRows.Length, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            foreach (Data.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }
            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync(indRows);
        }

        private void loadIndividuals()
        {
            loadIndividuals(data.Individual.Rows.Cast<Data.IndividualRow>().ToArray());
        }

        private void loadIndividuals(CardStack stack)
        {
            loadIndividuals(stack.GetIndividualRows());
        }

        private void loadIndividuals(SpeciesKey.SpeciesRow[] spcRows)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (SpeciesKey.SpeciesRow spcRow in spcRows)
            {
                result.AddRange(FullStack.GetIndividualRows(spcRow));
            }

            loadIndividuals(result.ToArray());
        }

        private void loadIndividuals(SpeciesKey.SpeciesRow spcRow)
        {
            loadIndividuals(new SpeciesKey.SpeciesRow[] { spcRow });
            individualSpecies = spcRow;
            growthModel = data.FindGrowthModel(individualSpecies.Species);
            massModel = data.FindMassModel(individualSpecies.Species);
        }

        private void loadIndividuals(Data.LogRow[] logRows)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.LogRow logRow in logRows)
            {
                result.AddRange(logRow.GetIndividualRows());
            }

            loadIndividuals(result.ToArray());
        }



        private Data.IndividualRow findIndividualRow(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[columnIndID.Index].Value == null)
            {
                return null;
            }
            else
            {
                return data.Individual.FindByID((int)gridRow.Cells[columnIndID.Index].Value);
            }
        }

        private void updateIndividualRow(DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return;

            gridRow.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.SpeciesRow;
            gridRow.Cells[columnIndLength.Index].Value = individualRow.IsLengthNull() ? null : (object)individualRow.Length;
            gridRow.Cells[columnIndMass.Index].Value = individualRow.IsMassNull() ? null : (object)individualRow.Mass;
            gridRow.Cells[columnIndSex.Index].Value = individualRow.IsSexNull() ? null : (Sex)individualRow.Sex;
            gridRow.Cells[columnIndComments.Index].Value = individualRow.IsCommentsNull() ? null : individualRow.Comments;

            foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
            {
                gridRow.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.IsValueNull() ? null : (object)valueRow.Value;
            }

            updateIndividualArtifacts(gridRow);
        }

        private void updateIndividualArtifacts(DataGridViewRow gridRow)
        {
            if (gridRow == null) return;

            if (!UserSettings.CheckConsistency) return;

            IndividualConsistencyChecker artifact = findIndividualRow(gridRow).CheckConsistency();

            if (artifact.TallyCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnIndTally.Index]).Image = ConsistencyChecker.GetImage(artifact.TallyCriticality);
                gridRow.Cells[columnIndTally.Index].ToolTipText = artifact.Treated ? artifact.GetNoticeTallyMissing() : artifact.GetNoticeTallyOdd();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnIndTally.Index]).Image = null;
                gridRow.Cells[columnIndTally.Index].ToolTipText = string.Empty;
            }
        }

        private Data.IndividualRow saveIndividualRow(DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return null;

            object length = gridRow.Cells[columnIndLength.Name].Value;
            if (length == null) individualRow.SetLengthNull();
            else individualRow.Length = (double)length;

            object mass = gridRow.Cells[columnIndMass.Name].Value;
            if (mass == null) individualRow.SetMassNull();
            else individualRow.Mass = (double)mass;

            Sex sex = (Sex)gridRow.Cells[columnIndSex.Name].Value;
            if (sex == null) individualRow.SetSexNull();
            else individualRow.Sex = sex.Value;

            object comments = gridRow.Cells[columnIndComments.Name].Value;
            if (comments == null) individualRow.SetCommentsNull();
            else individualRow.Comments = (string)comments;

            #region Additional variables

            foreach (DataGridViewColumn gridColumn in spreadSheetInd.GetColumns("Var_"))
            {
                Data.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                object varValue = gridRow.Cells[gridColumn.Name].Value;

                if (varValue == null)
                {
                    if (variableRow == null) continue;

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) continue;

                    valueRow.Delete();
                }
                else
                {
                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null)
                    {
                        data.Value.AddValueRow(individualRow, variableRow, (double)varValue);
                    }
                    else
                    {
                        valueRow.Value = (double)varValue;
                    }
                }
            }

            #endregion

            rememberChanged(individualRow.LogRow.CardRow);

            updateIndividualRow(gridRow);
            if (tabPageLog.Parent != null) updateLogArtifacts(columnLogID.GetRow(individualRow.LogID));
            if (tabPageSpc.Parent != null) updateSpeciesArtifacts(columnSpcID.GetRow(individualRow.LogRow.SpcID));
            if (tabPageCard.Parent != null) updateCardArtifacts(columnCardID.GetRow(individualRow.LogRow.CardID));

            return individualRow;
        }


        private DataGridViewRow[] IndividualRows(Data.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                int id = (int)gridRow.Cells[columnIndID.Name].Value;
                Data.IndividualRow individualRow = data.Individual.FindByID(id);

                if (individualRow.LogRow.CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private void setIndividualMassTip(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[columnIndMass.Index].Value != null) return;

            Data.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return;

            if (individualRow.IsLengthNull()) return;

            double mass = double.NaN;

            if (individualSpecies == null)
            {
                mass = data.FindMassModel(individualRow.Species).GetValue(individualRow.Length);
            }
            else
            {
                mass = massModel.GetValue(individualRow.Length);
            }

            gridRow.Cells[columnIndMass.Index].SetNullValue(
                double.IsNaN(mass) ?
                Wild.Resources.Interface.Interface.SuggestionUnavailable :
                " " + mass.ToString(columnIndMass.DefaultCellStyle.Format) + " ");
        }

        private Data.IndividualRow[] getIndividuals(IList rows)
        {
            spreadSheetInd.EndEdit();
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.IndividualRow individualRow = findIndividualRow(gridRow);
                if (individualRow == null) continue;

                result.Add(individualRow);
            }

            return result.ToArray();
        }


        #endregion
    }
}
