using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Mayfly.UserSettings;

namespace Mayfly.Wild
{
    public partial class Explorer : Form
    {
        public Survey data = new Survey();

        public CardStack FullStack { get; private set; }

        public bool IsBusy {
            get {
                return busy;
            }

            set {
                tabControl.AllowDrop =
                    tabPageInfo.AllowDrop =
                    spreadSheetInd.Enabled =
                    menuFile.Enabled =
                    menuSample.Enabled =
                    spreadSheetInd.Enabled = !value;


                menuSample.Enabled =
                    (!IsEmpty && !value);


                foreach (Control control in tabPageInfo.Controls) {
                    control.Enabled = !value;
                }

                busy = value;
            }
        }

        public bool IsEmpty {
            get {
                return empty;
            }

            set {
                empty = value;

                menuItemBackup.Enabled =
                    menuItemSaveSet.Enabled =
                    !empty;

                menuSample.Enabled =
                    !empty;

                menuItemExportBio.Enabled = !empty;
            }
        }

        public bool IsChanged {
            get {
                return changedCards.Count > 0;
            }
        }

        bool busy;
        bool empty;
        bool isClosing = false;


        private void updateSummary() {
            FullStack = new CardStack(data.Card);

            IsEmpty = FullStack.Count == 0;

            if (IsEmpty) {
                this.ResetText(EntryAssemblyInfo.Title);

                labelArtifacts.Visible = pictureBoxArtifacts.Visible = false;
                labelCardCountValue.Text = Constants.Null;

                updateQty(0);
                updateMass(0);

                data.RefreshBios();

                IsBusy = false;
            } else {
                ReaderSettings.Interface.SaveDialog.FileName = FullStack.FriendlyName;
                this.ResetText(FullStack.FriendlyName, EntryAssemblyInfo.Title);

                Log.Write("{0} cards are under consideration (common path: {1}).",
                    data.Card.Count, IO.GetCommonPath(FullStack.GetFilenames()));

                spreadSheetCard.ClearInsertedColumns();

                foreach (Survey.FactorRow factorRow in data.Factor) {
                    DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
                    gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
                }

                labelCardCountValue.Text = data.Card.Count.ToString();

                listViewDates.Items.Clear();
                foreach (DateTime[] bunch in data.GetDates().GetDatesBunches()) {
                    ListViewItem li = listViewDates.CreateItem(bunch.GetDatesRangeDescription());
                    li.Tag = bunch;
                }

                listViewWaters.Items.Clear();
                foreach (Survey.WaterRow waterRow in data.Water) {
                    var li = listViewWaters.CreateItem(waterRow.ID.ToString(), waterRow.IsWaterNull() ? Waters.Resources.Interface.Unnamed : waterRow.Water, waterRow.Type - 1);
                }

                bool mono = true;

                menuItemCardWater.Visible = data.Water.Count > 1;
                if (data.Water.Count > 1) {
                    mono = false;
                    menuItemCardWater.DropDownItems.Clear();
                    foreach (Survey.WaterRow waterRow in data.Water) {
                        var menuItem = new ToolStripMenuItem(waterRow.IsWaterNull() ? Waters.Resources.Interface.Unnamed : waterRow.Water);
                        menuItem.Click += (sender, e) => {
                            loadCards(FullStack.GetStack(waterRow));
                        };
                        menuItemCardWater.DropDownItems.Add(menuItem);
                    }
                }

                listViewSamplers.Items.Clear();
                foreach (Survey.SamplerRow samplerRow in FullStack.GetSamplers()) {
                    listViewSamplers.CreateItem(samplerRow.ID.ToString(), samplerRow.Name);
                }

                menuItemCardGear.Visible = FullStack.GetSamplers().Length > 1;
                if (FullStack.GetSamplers().Length > 1) {
                    mono = false;
                    menuItemCardGear.DropDownItems.Clear();
                    foreach (Survey.SamplerRow samplerRow in FullStack.GetSamplers()) {
                        var menuItem = new ToolStripMenuItem(samplerRow.Name);
                        menuItem.Click += (sender, e) => {
                            loadCards(FullStack.GetStack(samplerRow));
                        };
                        menuItemCardGear.DropDownItems.Add(menuItem);
                    }
                }

                listViewInvestigators.Items.Clear();
                foreach (string investigator in FullStack.GetInvestigators()) {
                    listViewInvestigators.CreateItem(investigator, investigator);
                }

                menuItemCardInvestigator.Visible = FullStack.GetInvestigators().Length > 1;
                if (FullStack.GetInvestigators().Length > 1) {
                    mono = false;
                    menuItemCardInvestigator.DropDownItems.Clear();
                    foreach (string investigator in FullStack.GetInvestigators()) {
                        var menuItem = new ToolStripMenuItem(investigator);
                        menuItem.Click += (sender, e) => {
                            loadCards(FullStack.GetStack("Investigator", investigator));
                        };
                        menuItemCardInvestigator.DropDownItems.Add(menuItem);
                    }
                }

                toolStripSeparator25.Visible = menuItemCardAll.Visible = !mono;

                if (mono) {
                    menuItemCards.Click += menuItemCards_Click;
                    menuItemCardAll.Click -= menuItemCards_Click;
                } else {
                    menuItemCards.Click -= menuItemCards_Click;
                    menuItemCardAll.Click += menuItemCards_Click;
                }


                FullStack.PopulateSpeciesMenu(menuItemIndAllAll, (sender, args) => {
                    loadIndividuals((TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag);
                }, (spcRow) => {
                    return FullStack.QuantityIndividual(spcRow);
                });

                FullStack.PopulateSpeciesMenu(menuItemLog, (sender, args) => {
                    loadLog((TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag);
                }, (spcRow) => {
                    return FullStack.GetLogRows(spcRow).Length;
                });

                updateQty(FullStack.Quantity());

                if (!modelCalc.IsBusy && !isClosing) {
                    IsBusy = true;
                    processDisplay.StartProcessing(Resources.Interface.Interface.ModelCalc);
                    modelCalc.RunWorkerAsync();
                }
            }

            statusBio.Visible = data.IsBioLoaded;
        }

        private void updateQty(double q) {

            if (q == 0) {
                labelQtyValue.Text = Constants.Null;
                statusQuantity.ResetFormatted(Constants.Null);
            } else {
                labelQtyValue.Text = Service.GetFriendlyQuantity((int)q);
                statusQuantity.ResetFormatted(Service.GetFriendlyQuantity((int)q));
            }
        }

        private void updateMass(Mass w) {

            if (w == 0) {
                labelWgtValue.Text = Constants.Null;
                statusMass.ResetFormatted(Constants.Null);
            } else {
                labelWgtValue.Text = w.ToString("a1");
                statusMass.ResetFormatted(w.ToString("a1"));
            }
        }

        private void updateArtifacts() {
            if (tabPageCard.Parent != null) {
                foreach (DataGridViewRow gridRow in spreadSheetCard.Rows) {
                    updateCardArtifacts(gridRow);
                }
            }

            if (tabPageLog.Parent != null) {
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows) {
                    updateLogArtifacts(gridRow);
                }
            }

            if (tabPageInd.Parent != null) {
                foreach (DataGridViewRow gridRow in spreadSheetInd.Rows) {
                    updateIndividualArtifacts(gridRow);
                }
            }

            if (tabPageComposition.Parent != null) {
                foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows) {
                    updateSpeciesArtifacts(gridRow);
                }
            }
        }

        private void applyBio() {

            if (tabPageInd.Parent != null) {
                spreadSheetInd_DisplayChanged(spreadSheetInd, new ScrollEventArgs(ScrollEventType.SmallDecrement, 0));
            }
        }


        public DialogResult CheckAndSave() {
            if (IsChanged) {
                TaskDialogButton b = taskDialogSave.ShowDialog(this);

                if (b == tdbSaveAll) {
                    SaveCards();
                    return DialogResult.OK;
                } else if (b == tdbCancelClose) {
                    return DialogResult.Cancel;
                }
            }

            return DialogResult.No;
        }

        public void SaveCards() {
            IsBusy = true;
            spreadSheetCard.StartProcessing(Resources.Interface.Process.DataSaving);
            dataSaver.RunWorkerAsync();
        }

        public void LoadCards(params string[] entries) {
            IsBusy = true;
            processDisplay.StartProcessing(Resources.Interface.Process.DataLoading);
            loaderData.RunWorkerAsync(entries);
        }

        private void rememberChanged(Survey.CardRow cardRow) {
            if (!changedCards.Contains(cardRow)) { changedCards.Add(cardRow); }
            menuItemSave.Enabled = IsChanged;
        }

        #region Species

        TaxonomicRank rank;

        private void loadTaxonList() {
            comboBoxSpcTaxon.DataSource = TaxonomicRank.MajorRanks;
            comboBoxSpcTaxon.SelectedIndex = -1;
            //comboBoxLogTaxon.DataSource = TaxonomicRank.MajorRanks;
            //comboBoxLogTaxon.SelectedIndex = -1;

            foreach (TaxonomicRank rank in TaxonomicRank.MajorRanks) {
                ToolStripMenuItem item = new ToolStripMenuItem(rank.ToString());
                item.Click += (sender, e) => {
                    DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(rank.ToString(),
                        rank.ToString(), typeof(string), 0);

                    foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows) {
                        if (gridRow.Cells[columnSpcSpc.Index].Value == null) {
                            continue;
                        }

                        if (gridRow.Cells[columnSpcSpc.Index].Value as string ==
                            Species.Resources.Interface.UnidentifiedTitle) {
                            continue;
                        }

                        TaxonomicIndex.TaxonRow spcRow = gridRow.Cells[columnSpcSpc.Index].Value as TaxonomicIndex.TaxonRow;
                        TaxonomicIndex.TaxonRow taxonRow = spcRow.GetParentTaxon(rank);
                        gridRow.Cells[gridColumn.Index].Value = (taxonRow == null) ?
                            Species.Resources.Interface.Varia : taxonRow.CommonName;
                    }
                };
                menuItemSpcTaxon.DropDownItems.Add(item);
            }

            // Clear list
            menuItemSpcTaxon.DropDownItems.Clear();

            if (ReaderSettings.TaxonomicIndex == null) return;

            // Fill list
            foreach (TaxonomicRank rank in TaxonomicRank.MajorRanks) {
                ToolStripMenuItem item = new ToolStripMenuItem(rank.ToString());
                item.Click += (sender, o) => {
                    DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(rank.ToString(),
                        rank.ToString(), typeof(string), 0, 200);

                    foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows) {
                        if (gridRow.Cells[columnSpcSpc.Index].Value == null) {
                            continue;
                        }

                        if (gridRow.Cells[columnSpcSpc.Index].Value as string ==
                            Species.Resources.Interface.UnidentifiedTitle) {
                            continue;
                        }

                        TaxonomicIndex.TaxonRow speciesRow = (TaxonomicIndex.TaxonRow)gridRow.Cells[columnSpcSpc.Index].Value;

                        if (speciesRow == null) {
                            gridRow.Cells[gridColumn.Index].Value = null;
                        } else {
                            TaxonomicIndex.TaxonRow taxonRow = speciesRow.GetParentTaxon(rank);
                            if (taxonRow != null) gridRow.Cells[gridColumn.Index].Value = taxonRow.CommonName;
                        }
                    }
                };
                menuItemSpcTaxon.DropDownItems.Add(item);
            }

            menuItemSpcTaxon.Enabled = menuItemSpcTaxon.DropDownItems.Count > 0;
        }

        private void loadSpc() {
            IsBusy = true;
            spreadSheetSpc.StartProcessing(Resources.Interface.Process.LoadSpc);
            spreadSheetSpc.Rows.Clear();
            loaderSpc.RunWorkerAsync();
        }

        private TaxonomicIndex.TaxonRow findSpeciesRow(DataGridViewRow gridRow) {
            return rank == null ? ReaderSettings.TaxonomicIndex.Taxon.FindByID((int)gridRow.Cells[columnSpcID.Index].Value) : null;
        }

        private void updateSpeciesArtifacts(DataGridViewRow gridRow) {
            if (gridRow == null) return;

            if (!ExplorerSettings.CheckConsistency) return;

            SpeciesConsistencyChecker artifact = findSpeciesRow(gridRow).CheckConsistency(FullStack);

            if (artifact.ArtifactsCount > 0) {
                ((TextAndImageCell)gridRow.Cells[columnSpcSpc.Index]).Image = ConsistencyChecker.GetImage(artifact.WorstCriticality);
                gridRow.Cells[columnSpcSpc.Index].ToolTipText = artifact.GetNotices(true).Merge(System.Environment.NewLine);
            } else {
                ((TextAndImageCell)gridRow.Cells[columnSpcSpc.Index]).Image = null;
                gridRow.Cells[columnSpcSpc.Index].ToolTipText = string.Empty;
            }

        }

        private TaxonomicIndex.TaxonRow[] getSpeciesRows(IList rows) {
            spreadSheetLog.EndEdit();
            List<TaxonomicIndex.TaxonRow> result = new List<TaxonomicIndex.TaxonRow>();
            foreach (DataGridViewRow gridRow in rows) {
                if (gridRow.IsNewRow) continue;
                result.Add(findSpeciesRow(gridRow));
            }

            return result.ToArray();
        }

        #endregion

        #region Cards

        private List<Survey.CardRow> changedCards = new List<Survey.CardRow>();



        private void loadCards(CardStack stack) {
            IsBusy = true;
            spreadSheetCard.StartProcessing(Resources.Interface.Process.LoadCard);
            spreadSheetCard.Rows.Clear();

            loaderCard.RunWorkerAsync(stack);
        }

        private void loadCards() {
            loadCards(FullStack);
        }



        private Survey.CardRow findCardRow(DataGridViewRow gridRow) {
            if (gridRow == null) return null;

            return data.Card.FindByID((int)gridRow.Cells[columnCardID.Index].Value);
        }

        private void updateCardRow(DataGridViewRow gridRow) {
            Survey.CardRow cardRow = findCardRow(gridRow);

            if (cardRow == null) return;

            setCardValue(cardRow, gridRow, columnCardInvestigator, "Investigator");
            setCardValue(cardRow, gridRow, columnCardLabel, "Label");
            setCardValue(cardRow, gridRow, columnCardWater, "Water");
            setCardValue(cardRow, gridRow, columnCardWhen, "When");
            setCardValue(cardRow, gridRow, columnCardWhere, "Where");

            setCardValue(cardRow, gridRow, columnCardWeather, "Weather");
            setCardValue(cardRow, gridRow, columnCardTempSurface, "Surface");

            setCardValue(cardRow, gridRow, columnCardSampler, "Sampler");

            setCardValue(cardRow, gridRow, columnCardEffort, "Effort");
            setCardValue(cardRow, gridRow, columnCardDepth, "Depth");
            setCardValue(cardRow, gridRow, columnCardWealth, "Wealth");
            setCardValue(cardRow, gridRow, columnCardQuantity, "Quantity");
            setCardValue(cardRow, gridRow, columnCardMass, "Mass");
            setCardValue(cardRow, gridRow, columnCardAbundance, "Abundance");
            setCardValue(cardRow, gridRow, columnCardBiomass, "Biomass");
            setCardValue(cardRow, gridRow, columnCardDiversityA, "DiversityA");
            setCardValue(cardRow, gridRow, columnCardDiversityB, "DiversityB");

            setCardValue(cardRow, gridRow, columnCardComments, "Comments");

            foreach (Survey.FactorValueRow factorValueRow in cardRow.GetFactorValueRows()) {
                setCardValue(cardRow, gridRow, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
            }

            updateCardArtifacts(gridRow);
        }

        private void updateCardArtifacts(DataGridViewRow gridRow) {

            if (gridRow == null) return;

            if (!ExplorerSettings.CheckConsistency) return;

            //CardConsistencyChecker artifact = findCardRow(gridRow).CheckConsistency();

            //if (artifact.EffortCriticality > ArtifactCriticality.Normal) {
            //    ((TextAndImageCell)gridRow.Cells[columnCardEffort.Index]).Image = ConsistencyChecker.GetImage(artifact.EffortCriticality);
            //    gridRow.Cells[columnCardEffort.Index].ToolTipText = artifact.GetNotices(false).Merge();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnCardEffort.Index]).Image = null;
            //    gridRow.Cells[columnCardEffort.Index].ToolTipText = string.Empty;
            //}


            //if (artifact.LogArtifacts.Count > 0) {
            //    ((TextAndImageCell)gridRow.Cells[columnCardWealth.Index]).Image = ConsistencyChecker.GetImage(ConsistencyChecker.GetWorst(artifact.LogWorstCriticality));
            //    gridRow.Cells[columnCardWealth.Index].ToolTipText = LogConsistencyChecker.GetCommonNotices(artifact.LogArtifacts).Merge();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnCardWealth.Index]).Image = null;
            //    gridRow.Cells[columnCardWealth.Index].ToolTipText = string.Empty;
            //}

            //if (artifact.IndividualArtifacts.Count > 0) {
            //    ((TextAndImageCell)gridRow.Cells[columnCardQuantity.Index]).Image = ConsistencyChecker.GetImage(ConsistencyChecker.GetWorst(artifact.IndividualWorstCriticality));
            //    gridRow.Cells[columnCardQuantity.Index].ToolTipText = IndividualConsistencyChecker.GetCommonNotices(artifact.IndividualArtifacts).Merge();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnCardQuantity.Index]).Image = null;
            //    gridRow.Cells[columnCardQuantity.Index].ToolTipText = string.Empty;
            //}
        }

        private void saveCardRow(DataGridViewRow gridRow) {

            Survey.CardRow cardRow = findCardRow(gridRow);

            if (cardRow == null) return;

            object wpt = (object)gridRow.Cells[columnCardWhere.Name].Value;
            if (wpt == null) cardRow.SetWhereNull();
            else cardRow.Where = ((Waypoint)wpt).Protocol;

            object depth = (object)gridRow.Cells[columnCardDepth.Name].Value;
            if (depth == null) cardRow.SetDepthNull();
            else cardRow.Depth = (double)depth;

            object comments = gridRow.Cells[columnCardComments.Name].Value;
            if (comments == null) cardRow.SetCommentsNull();
            else cardRow.Comments = (string)comments;

            // Additional factors
            foreach (DataGridViewColumn gridColumn in spreadSheetCard.GetInsertedColumns()) {
                Survey.FactorRow factorRow = data.Factor.FindByFactor(gridColumn.HeaderText);
                if (factorRow == null) continue;
                object factorValue = gridRow.Cells[gridColumn.Name].Value;

                if (factorValue == null) {
                    if (factorRow == null) continue;

                    Survey.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null) continue;

                    factorValueRow.Delete();
                } else {
                    if (factorRow == null) {
                        factorRow = data.Factor.AddFactorRow(gridColumn.HeaderText);
                    }

                    Survey.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

                    if (factorValueRow == null) {
                        data.FactorValue.AddFactorValueRow(cardRow, factorRow, (double)factorValue);
                    } else {
                        factorValueRow.Value = (double)factorValue;
                    }
                }
            }

            rememberChanged(cardRow);

            updateCardArtifacts(gridRow);

            foreach (Survey.LogRow logRow in cardRow.GetLogRows()) {
                if (tabPageLog.Parent != null) {
                    DataGridViewRow gridLogRow = columnLogID.GetRow(logRow.ID);
                    if (gridLogRow != null) updateLogRow(gridLogRow);
                }

                foreach (Survey.IndividualRow indRow in logRow.GetIndividualRows()) {
                    if (tabPageInd.Parent != null) {
                        DataGridViewRow gridIndRow = columnIndID.GetRow(indRow.ID);
                        if (gridIndRow != null) updateIndividualRow(gridIndRow);
                    }
                }
            }
        }



        private CardStack getCardStack(IList rows) {
            spreadSheetCard.EndEdit();

            CardStack stack = new CardStack();

            foreach (DataGridViewRow gridRow in rows) {
                if (gridRow.IsNewRow) continue;

                stack.Add(findCardRow(gridRow));
            }

            return stack;
        }


        private bool loadCardAddt(SpreadSheet spreadSheet) {
            SelectionValue selectionValue = new SelectionValue(spreadSheetCard);
            selectionValue.Picker.UserSelectedColumns = spreadSheet.GetInsertedColumns();

            if (selectionValue.ShowDialog(this) != DialogResult.OK) return false;

            bool newInserted = false;
            int i = spreadSheet.InsertedColumnCount;
            foreach (DataGridViewColumn gridColumn in spreadSheet.GetInsertedColumns()) {
                if (gridColumn.Name.Contains("Var")) continue;
                if (selectionValue.Picker.IsSelected(gridColumn)) continue;
                spreadSheet.Columns.Remove(gridColumn);
                i--;
            }

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns) {
                if (spreadSheet.GetColumn(gridColumn.Name) == null) {
                    spreadSheet.InsertColumn(gridColumn, i, gridColumn.Name.TrimStart("columnCard".ToCharArray())).ReadOnly = true;
                    newInserted = true;
                    i++;
                }
            }

            return newInserted;
        }

        private void setCardValue(Survey.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns) {
            foreach (DataGridViewColumn gridColumn in gridColumns) {
                if (gridColumn.Name.StartsWith("Var_")) continue;
                setCardValue(cardRow, gridRow, gridColumn);
            }
        }

        private void setCardValue(Survey.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn) {
            setCardValue(cardRow, gridRow, gridColumn, gridColumn.Name);
        }

        private void setCardValue(Survey.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn, string field) {
            gridRow.Cells[gridColumn.Index].Value = cardRow.Get(field);

            switch (field) {
                case "Effort":
                    gridRow.Cells[gridColumn.Index].Style.Format = gridColumn.ExtendFormat(cardRow.GetGearType().GetDefaultUnitEffort().Unit);
                    break;
            }
        }

        private delegate void ValueSetEventHandler(Survey.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);

        #endregion



        #region Log

        TaxonomicRank baseLog;

        private void loadLog(Survey.LogRow[] logRows) {
            IsBusy = true;
            spreadSheetLog.StartProcessing(Resources.Interface.Process.LoadLog);
            spreadSheetLog.Rows.Clear();
            loaderLog.RunWorkerAsync(logRows);
        }

        private void loadLog() {

            loadLog(data.Log.Rows.Cast<Survey.LogRow>().ToArray());
        }

        private void loadLog(TaxonomicIndex.TaxonRow[] spcRows, CardStack stack) {
            List<Survey.LogRow> logRows = new List<Survey.LogRow>();

            foreach (TaxonomicIndex.TaxonRow spcRow in spcRows) {
                logRows.AddRange(stack.GetLogRows(spcRow));
            }

            loadLog(logRows.ToArray());
        }

        private void loadLog(TaxonomicIndex.TaxonRow[] spcRows) {
            loadLog(spcRows, FullStack);
        }

        private void loadLog(TaxonomicIndex.TaxonRow spcRows) {
            loadLog(new TaxonomicIndex.TaxonRow[] { spcRows });
        }

        private void loadLog(CardStack stack) {
            loadLog(stack.GetSpecies(), stack);
        }

        private Survey.LogRow findLogRow(DataGridViewRow gridRow) {
            return rank == null ? data.Log.FindByID((int)gridRow.Cells[columnLogID.Index].Value) : null;
        }

        private void updateLogRow(DataGridViewRow gridRow) {
            Survey.LogRow logRow = findLogRow(gridRow);

            gridRow.Cells[columnLogSpc.Index].Value = logRow.DefinitionRow;

            if (!logRow.IsQuantityNull()) {
                gridRow.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
                gridRow.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
                gridRow.Cells[columnLogAbundance.Index].Style.Format = columnLogAbundance.ExtendFormat(logRow.CardRow.GetAbundanceUnits());
            }

            if (!logRow.IsMassNull()) {
                gridRow.Cells[columnLogMass.Index].Value = logRow.Mass;
                gridRow.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();
                gridRow.Cells[columnLogBiomass.Index].Style.Format = columnLogBiomass.ExtendFormat(logRow.CardRow.GetBiomassUnits());
            }

            setCardValue(logRow.CardRow, gridRow, spreadSheetLog.GetInsertedColumns());

            updateLogArtifacts(gridRow);
        }

        private void updateLogArtifacts(DataGridViewRow gridRow) {

            if (gridRow == null) return;

            if (!ExplorerSettings.CheckConsistency) return;

            //LogConsistencyChecker artifact = findLogRow(gridRow).CheckConsistency();

            //if (artifact.OddMassCriticality > ArtifactCriticality.Normal) {
            //    ((TextAndImageCell)gridRow.Cells[columnLogMass.Index]).Image = ConsistencyChecker.GetImage(artifact.OddMassCriticality);
            //    gridRow.Cells[columnLogMass.Index].ToolTipText = artifact.GetNoticeOddMass();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnLogMass.Index]).Image = null;
            //    gridRow.Cells[columnLogMass.Index].ToolTipText = string.Empty;

            //}

            //if (artifact.UnmeasurementsCriticality > ArtifactCriticality.Normal) {
            //    ((TextAndImageCell)gridRow.Cells[columnLogQuantity.Index]).Image = ConsistencyChecker.GetImage(artifact.UnmeasurementsCriticality);
            //    gridRow.Cells[columnLogQuantity.Index].ToolTipText = artifact.GetNoticeUnmeasured();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnLogQuantity.Index]).Image = null;
            //    gridRow.Cells[columnLogQuantity.Index].ToolTipText = string.Empty;
            //}
        }

        private void saveLogRow(DataGridViewRow gridRow) {

            if (rank != null) return;

            if (!ExplorerSettings.CheckConsistency) return;

            Survey.LogRow logRow = findLogRow(gridRow);

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

        private Survey.LogRow[] getLogRows(IList rows) {

            spreadSheetLog.EndEdit();
            List<Survey.LogRow> result = new List<Survey.LogRow>();
            foreach (DataGridViewRow gridRow in rows) {
                if (gridRow.IsNewRow) continue;
                result.Add(findLogRow(gridRow));
            }

            return result.ToArray();
        }

        #endregion



        #region Individuals

        TaxonomicIndex.TaxonRow individualSpecies;
        ContinuousBio growthModel;
        ContinuousBio massModel;


        private void loadIndividuals(Survey.IndividualRow[] indRows) {
            individualSpecies = null;

            IsBusy = true;
            spreadSheetInd.StartProcessing(Resources.Interface.Process.LoadInd);
            spreadSheetInd.Rows.Clear();

            foreach (Survey.VariableRow variableRow in data.Variable) {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }
            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync(indRows);
        }

        private void loadIndividuals() {

            loadIndividuals(data.Individual.Rows.Cast<Survey.IndividualRow>().ToArray());
        }

        private void loadIndividuals(CardStack stack) {

            loadIndividuals(stack.GetIndividualRows());
        }

        private void loadIndividuals(TaxonomicIndex.TaxonRow[] spcRows) {

            List<Survey.IndividualRow> result = new List<Survey.IndividualRow>();

            foreach (TaxonomicIndex.TaxonRow spcRow in spcRows) {
                result.AddRange(FullStack.GetIndividualRows(spcRow));
            }

            loadIndividuals(result.ToArray());
        }

        private void loadIndividuals(TaxonomicIndex.TaxonRow spcRow) {

            loadIndividuals(new TaxonomicIndex.TaxonRow[] { spcRow });
            individualSpecies = spcRow;
            growthModel = data.FindGrowthModel(individualSpecies.Name);
            massModel = data.FindMassModel(individualSpecies.Name);
        }

        private void loadIndividuals(Survey.LogRow[] logRows) {

            List<Survey.IndividualRow> result = new List<Survey.IndividualRow>();

            foreach (Survey.LogRow logRow in logRows) {
                result.AddRange(logRow.GetIndividualRows());
            }

            loadIndividuals(result.ToArray());
        }



        private Survey.IndividualRow findIndividualRow(DataGridViewRow gridRow) {

            if (gridRow.Cells[columnIndID.Index].Value == null) {
                return null;
            } else {
                return data.Individual.FindByID((int)gridRow.Cells[columnIndID.Index].Value);
            }
        }

        private void updateIndividualRow(DataGridViewRow gridRow) {

            Survey.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return;

            gridRow.Cells[columnIndTaxon.Index].Value = individualRow.LogRow.DefinitionRow;
            gridRow.Cells[columnIndLength.Index].Value = individualRow.IsLengthNull() ? null : (object)individualRow.Length;
            gridRow.Cells[columnIndMass.Index].Value = individualRow.IsMassNull() ? null : (object)individualRow.Mass;
            gridRow.Cells[columnIndTally.Index].Value = individualRow.IsTallyNull() ? null : individualRow.Tally;

            gridRow.Cells[columnIndComments.Index].Value = individualRow.IsCommentsNull() ? null : individualRow.Comments;

            foreach (Survey.ValueRow valueRow in individualRow.GetValueRows()) {
                gridRow.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.IsValueNull() ? null : (object)valueRow.Value;
            }

            updateIndividualArtifacts(gridRow);
        }

        private void updateIndividualArtifacts(DataGridViewRow gridRow) {

            if (gridRow == null) return;

            if (!ExplorerSettings.CheckConsistency) return;

            //IndividualConsistencyChecker artifact = findIndividualRow(gridRow).CheckConsistency();

            //if (artifact.TallyCriticality > ArtifactCriticality.Normal) {
            //    ((TextAndImageCell)gridRow.Cells[columnIndTally.Index]).Image = ConsistencyChecker.GetImage(artifact.TallyCriticality);
            //    gridRow.Cells[columnIndTally.Index].ToolTipText = artifact.Treated ? artifact.GetNoticeTallyMissing() : artifact.GetNoticeTallyOdd();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnIndTally.Index]).Image = null;
            //    gridRow.Cells[columnIndTally.Index].ToolTipText = string.Empty;
            //}

            //if (artifact.UnweightedDietItemsCriticality > ArtifactCriticality.Normal) {
            //    ((TextAndImageCell)gridRow.Cells[columnIndConsumed.Index]).Image = ConsistencyChecker.GetImage(artifact.UnweightedDietItemsCriticality);
            //    gridRow.Cells[columnIndConsumed.Index].ToolTipText = artifact.GetNoticeUnweightedDiet();
            //} else {
            //    ((TextAndImageCell)gridRow.Cells[columnIndConsumed.Index]).Image = null;
            //    gridRow.Cells[columnIndConsumed.Index].ToolTipText = string.Empty;
            //}
        }

        private Survey.IndividualRow saveIndividualRow(DataGridViewRow gridRow) {

            Survey.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return null;

            object length = gridRow.Cells[columnIndLength.Name].Value;
            if (length == null) individualRow.SetLengthNull();
            else individualRow.Length = (double)length;

            object mass = gridRow.Cells[columnIndMass.Name].Value;
            if (mass == null) individualRow.SetMassNull();
            else individualRow.Mass = (double)mass;

            object tally = gridRow.Cells[columnIndTally.Name].Value;
            if (tally == null) individualRow.SetTallyNull();
            else individualRow.Tally = (string)tally;

            object comments = gridRow.Cells[columnIndComments.Name].Value;
            if (comments == null) individualRow.SetCommentsNull();
            else individualRow.Comments = (string)comments;

            #region Additional variables

            foreach (DataGridViewColumn gridColumn in spreadSheetInd.GetColumns("Var_")) {
                Survey.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                object varValue = gridRow.Cells[gridColumn.Name].Value;

                if (varValue == null) {
                    if (variableRow == null) continue;

                    Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) continue;

                    valueRow.Delete();
                } else {
                    if (variableRow == null) {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) {
                        data.Value.AddValueRow(individualRow, variableRow, (double)varValue);
                    } else {
                        valueRow.Value = (double)varValue;
                    }
                }
            }

            #endregion

            rememberChanged(individualRow.LogRow.CardRow);

            updateIndividualRow(gridRow);
            if (tabPageLog.Parent != null) updateLogArtifacts(columnLogID.GetRow(individualRow.LogID));
            if (tabPageComposition.Parent != null) updateSpeciesArtifacts(columnSpcID.GetRow(individualRow.LogRow.DefID));
            if (tabPageCard.Parent != null) updateCardArtifacts(columnCardID.GetRow(individualRow.LogRow.CardID));

            return individualRow;
        }

        private DataGridViewRow[] IndividualRows(Survey.CardRow cardRow) {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows) {
                int id = (int)gridRow.Cells[columnIndID.Name].Value;
                Survey.IndividualRow individualRow = data.Individual.FindByID(id);

                if (individualRow.LogRow.CardRow == cardRow) {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private void setIndividualMassTip(DataGridViewRow gridRow) {
            if (gridRow.Cells[columnIndMass.Index].Value != null) return;

            Survey.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return;

            if (individualRow.IsLengthNull()) return;

            double mass = double.NaN;

            if (individualSpecies == null) {
                mass = data.FindMassModel(individualRow.Species).GetValue(individualRow.Length);
            } else {
                mass = massModel.GetValue(individualRow.Length);
            }

            gridRow.Cells[columnIndMass.Index].SetNullValue(
                double.IsNaN(mass) ?
                Resources.Interface.Interface.SuggestionUnavailable :
                " " + mass.ToString(columnIndMass.DefaultCellStyle.Format) + " ");
        }

        private Survey.IndividualRow[] getIndividuals(IList rows) {

            spreadSheetInd.EndEdit();
            List<Survey.IndividualRow> result = new List<Survey.IndividualRow>();

            foreach (DataGridViewRow gridRow in rows) {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Survey.IndividualRow individualRow = findIndividualRow(gridRow);
                if (individualRow == null) continue;

                result.Add(individualRow);
            }

            return result.ToArray();
        }


        #endregion


        public Explorer() {

            InitializeComponent();

            UI.SetMenuAvailability(License.AllowedFeaturesLevel >= FeatureLevel.Advanced,
                menuItemImportBio,
                menuItemExportBio,
                toolStripSeparator10
                );

            ReaderSettings.Interface.OpenDialog.Multiselect = true;

            listViewWaters.Shine();
            listViewSamplers.Shine();
            listViewInvestigators.Shine();
            listViewDates.Shine();

            spreadSheetCard.UpdateStatus();
            spreadSheetSpc.UpdateStatus();
            spreadSheetLog.UpdateStatus();
            spreadSheetInd.UpdateStatus();

            columnCardWater.ValueType = typeof(string);
            columnCardWhen.ValueType = typeof(DateTime);
            columnCardWhere.ValueType = typeof(Waypoint);
            columnCardWeather.ValueType = typeof(WeatherState);
            columnCardTempSurface.ValueType = typeof(double);
            columnCardSampler.ValueType = typeof(string);
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

            columnLogSpc.ValueType = typeof(TaxonomicIndex.TaxonRow);
            columnLogQuantity.ValueType = typeof(int);
            columnLogMass.ValueType = typeof(double);
            columnLogAbundance.ValueType = typeof(double);
            columnLogBiomass.ValueType = typeof(double);
            columnLogDiversityA.ValueType = typeof(double);
            columnLogDiversityB.ValueType = typeof(double);

            tabPageLog.Parent = null;

            columnIndTaxon.ValueType = typeof(TaxonomicIndex.TaxonRow);
            columnIndLength.ValueType = typeof(double);
            columnIndMass.ValueType = typeof(double);
            columnIndTally.ValueType = typeof(string);

            columnIndComments.ValueType = typeof(string);

            tabPageInd.Parent = null;

            columnSpcSpc.ValueType = typeof(TaxonomicIndex.TaxonRow);
            columnSpcQuantity.ValueType = typeof(double);
            columnSpcMass.ValueType = typeof(Mass);
            columnSpcOccurrence.ValueType = typeof(double);

            tabPageComposition.Parent = null;

            menuStrip.SetMenuIcons();

            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced) {

                spreadSheetInd.SetBioAcceptable(LoadCards);
                spreadSheetSpc.SetBioAcceptable(LoadCards);
                spreadSheetLog.SetBioAcceptable(LoadCards);
            }

            data = new Survey();
            FullStack = new CardStack();

            loadTaxonList();

            IsEmpty = true;
        }

        public Explorer(string[] args)
            : this() {
            if (args.Length == 0) {
                return;
            }

            Load += (o, e) => { LoadCards(args.GetOperableFilenames(ReaderSettings.Interface.Extension)); };
        }

        public Explorer(CardStack stack)
            : this() {
            foreach (Survey.CardRow cardRow in stack) {
                cardRow.SingleCardDataset().CopyTo(data);
            }

            updateSummary();
        }



        private void mainForm_Load(object sender, EventArgs e) {
            updateSummary();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e) {
            isClosing = true;
            switch (CheckAndSave()) {
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


        private void tab_Changed(object sender, EventArgs e) {

            menuCards.Visible = (tabControl.SelectedTab == tabPageCard);
            menuSpc.Visible = (tabControl.SelectedTab == tabPageComposition);
            menuIndividuals.Visible = (tabControl.SelectedTab == tabPageInd);
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e) {

            ((ComboBox)sender).HandleInput(e);
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e) {

            processDisplay.SetProgress(e.ProgressPercentage);
        }



        private void dataLoader_DoWork(object sender, DoWorkEventArgs e) {

            string[] filenames = (string[])e.Argument;

            for (int i = 0; i < filenames.Length; i++) {
                if (Path.GetExtension(filenames[i]) == UserSettings.InterfaceBio.Extension) {
                    if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced) {
                        processDisplay.SetStatus(Resources.Interface.Process.BioLoading);

                        if (data.IsBioLoaded) {
                            TaskDialogButton tdb = taskDialogBio.ShowDialog();

                            if (tdb != tdbSpecCancel) {
                                data.ImportBio(filenames[i], tdb == tdbSpecClear);
                            }
                        } else {
                            data.ImportBio(filenames[i]);
                        }

                        processDisplay.ResetStatus();
                    }
                } else if (Path.GetExtension(filenames[i]) == ReaderSettings.Interface.Extension) {
                    if (data.IsLoaded(filenames[i])) continue;

                    Survey _data = new Survey();

                    if (_data.Read(filenames[i])) {
                        if (_data.Card.Count == 0) {
                            Log.Write(string.Format("File is empty: {0}.", filenames[i]));
                        } else {
                            _data.CopyTo(data);
                        }
                    }
                }

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }
        }

        private void dataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            spreadSheetCard.StopProcessing();
            updateSummary();
        }


        private void modelCalc_DoWork(object sender, DoWorkEventArgs e) {

            //if (License.AllowedFeaturesLevel < FeatureLevel.Advanced)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            data.RefreshBios();
        }

        private void modelCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            if (!e.Cancelled) {
                processDisplay.StartProcessing(Resources.Interface.Process.ArtifactsProcessing);
            } else {
                processDisplay.StopProcessing();
                IsBusy = false;
            }

            artifactFinder.RunWorkerAsync();

            updateMass(FullStack.Mass());
        }



        private void artifactFinder_DoWork(object sender, DoWorkEventArgs e) {

            if (!ExplorerSettings.CheckConsistency) {
                e.Cancel = true;
                return;
            }

            e.Result = FullStack.CheckConsistency();
        }

        private void artifactFinder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            if (!e.Cancelled) {
                ConsistencyChecker[] artifacts = (ConsistencyChecker[])e.Result;

                labelArtifacts.Visible = pictureBoxArtifacts.Visible = artifacts.Length > 0;

                if (artifacts.Length > 0) {
                    Notification.ShowNotification(
                        Resources.Interface.Messages.ArtifactsNotification,
                        Resources.Interface.Messages.ArtifactsNotificationInstruction);
                } else {
                    Notification.ShowNotification(Resources.Interface.Messages.ArtifactsNoneNotification,
                        Resources.Interface.Messages.ArtifactsNoneNotificationInstruction);
                }

                updateArtifacts();
            }

            IsBusy = false;
            processDisplay.StopProcessing();
        }



        private void dataSaver_DoWork(object sender, DoWorkEventArgs e) {

            int index = 0;

            while (changedCards.Count > 0) {
                Survey.CardRow cardRow = changedCards[0];

                index++;
                dataSaver.ReportProgress(index);

                if (cardRow.Path != null) {
                    Survey _data = cardRow.SingleCardDataset();
                    _data.WriteToFile(cardRow.Path);
                }

                changedCards.RemoveAt(0);
            }
        }

        private void dataSaver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            IsBusy = false;
            spreadSheetCard.StopProcessing();
            updateSummary();
            menuItemSave.Enabled = IsChanged;
            if (isClosing) { Close(); }
        }



        private void extender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            IsBusy = false;
            processDisplay.StopProcessing();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            ((Report)e.Result).Run();
            IsBusy = false;
            processDisplay.StopProcessing();
        }


        #region Menus

        #region File

        private void menuItemAddData_Click(object sender, EventArgs e) {

            if (ReaderSettings.Interface.OpenDialog.ShowDialog(this) == DialogResult.OK) {
                LoadCards(ReaderSettings.Interface.OpenDialog.FileNames);
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e) {

            SaveCards();
        }

        private void menuItemBackup_Click(object sender, EventArgs e) {

            if (fbdBackup.ShowDialog(this) == DialogResult.OK) {
                if (ModifierKeys.HasFlag(Keys.Shift)) {
                    data.WriteToFile(Path.Combine(fbdBackup.SelectedPath, "backup.xml"));
                } else foreach (Survey.CardRow cardRow in data.Card) {
                        ExplorerSettings.Interface.FolderPath = fbdBackup.SelectedPath;
                        string filename = ExplorerSettings.Interface.SuggestName(cardRow.GetSuggestedName());
                        Survey _data = cardRow.SingleCardDataset();
                        _data.WriteToFile(Path.Combine(fbdBackup.SelectedPath, filename));
                    }
            }
        }

        private void menuItemSaveSet_Click(object sender, EventArgs e) {

            if (ReaderSettings.Interface.SaveDialog.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllLines(ReaderSettings.Interface.SaveDialog.FileName, FullStack.GetFilenames());
            }
        }

        private void menuItemExportBio_Click(object sender, EventArgs e) {

            WizardExportBio exportWizard = new WizardExportBio(data);
            exportWizard.Show();
        }

        private void menuItemImportBio_Click(object sender, EventArgs e) {

            if (UserSettings.InterfaceBio.OpenDialog.ShowDialog(this) == DialogResult.OK) {
                LoadCards(UserSettings.InterfaceBio.OpenDialog.FileName);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e) {

            Close();
        }

        #endregion

        #region Sample

        private void menuItemSample_DropDownOpening(object sender, EventArgs e) {

        }

        private void menuItemCards_Click(object sender, EventArgs e) {
            loadCards();
        }

        private void menuItemSpc_Click(object sender, EventArgs e) {
            loadSpc();
        }

        private void menuItemLog_Click(object sender, EventArgs e) {
            loadLog();
        }

        private void menuItemIndAllAll_Click(object sender, EventArgs e) {
            loadIndividuals();
        }

        #endregion

        #region Service

        private void menuItemSettings_Click(object sender, EventArgs e) {

            Mayfly.UserSettings.Settings.ShowDialog();
        }

        private void menuItemLicenses_Click(object sender, EventArgs e) {


        }

        private void menuItemAbout_Click(object sender, EventArgs e) {

            About about = new About(AppBanner);
            about.SetPowered(SupportLogo, SupportText);
            about.ShowDialog();
        }

        #endregion

        #endregion



        #region Main page

        private void cards_DragDrop(object sender, DragEventArgs e) {
            cards_DragLeave(sender, e);
            List<string> ext = new List<string>();
            ext.Add(ReaderSettings.Interface.Extension);
            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced) ext.Add(UserSettings.InterfaceBio.Extension);
            LoadCards(e.GetOperableFilenames(ext.ToArray()));
        }

        private void cards_DragEnter(object sender, DragEventArgs e) {
            List<string> ext = new List<string>();
            ext.Add(ReaderSettings.Interface.Extension);
            if (License.AllowedFeaturesLevel >= FeatureLevel.Advanced) ext.Add(UserSettings.InterfaceBio.Extension);
            if (e.GetOperableFilenames(ext.ToArray()).Length > 0) {
                e.Effect = DragDropEffects.All;

                foreach (Control ctrl in tabPageInfo.Controls) {
                    ctrl.ForeColor = Color.LightGray;
                }
            }
        }

        private void cards_DragLeave(object sender, EventArgs e) {
            foreach (Control ctrl in tabPageInfo.Controls) {
                ctrl.ForeColor = SystemColors.ControlText;
            }
        }


        private void listViewWaters_ItemActivate(object sender, EventArgs e) {
            loadCards(FullStack.GetStack(data.Water.FindByID(listViewWaters.FocusedItem.GetID())));
        }

        private void listViewSamplers_ItemActivate(object sender, EventArgs e) {
            loadCards(FullStack.GetStack("SamplerID", listViewSamplers.FocusedItem.GetID()));
        }

        private void listViewInvestigators_ItemActivate(object sender, EventArgs e) {
            loadCards(FullStack.GetStack("Investigator", listViewInvestigators.FocusedItem.Text));
        }

        private void listViewDates_ItemActivate(object sender, EventArgs e) {
            DateTime[] dt = (DateTime[])listViewDates.FocusedItem.Tag;
            spreadSheetCard.EnsureFilter(columnCardWhen, dt[0].ToOADate(), dt[dt.Length - 1].ToOADate() + 1, loaderCard, menuItemCards_Click);
        }

        #endregion 



        #region Cards

        private void menuItemCardMeteo_CheckedChanged(object sender, EventArgs e) {
            columnCardWeather.Visible =
            columnCardTempSurface.Visible =
                menuItemCardMeteo.Checked;
        }

        private void menuItemCardPrintNotes_Click(object sender, EventArgs e) {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void menuItemCardPrintFull_Click(object sender, EventArgs e) {
            getCardStack(spreadSheetCard.Rows).GetCardReport(CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Stratified | CardReportLevel.Individuals).Run();
        }



        private void cardLoader_DoWork(object sender, DoWorkEventArgs e) {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            CardStack stack = (CardStack)e.Argument;

            for (int i = 0; i < stack.Count; i++) {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetCard);
                setCardValue(stack[i], gridRow, columnCardID, "ID");
                updateCardRow(gridRow);
                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void cardLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            spreadSheetCard.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetCard.StopProcessing();
            spreadSheetCard.UpdateStatus();

            tabPageCard.Parent = tabControl;
            tabControl.SelectedTab = tabPageCard;

            updateArtifacts();
        }

        private void spreadSheetCard_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetCard.ContainsFocus) {
                saveCardRow(spreadSheetCard.Rows[e.RowIndex]);

                //if (effortSource.Contains(spreadSheetCard.Columns[e.ColumnIndex])) {
                //    spreadSheetCard[columnCardEffort.Index, e.RowIndex].Value =
                //        findCardRow(spreadSheetCard.Rows[e.RowIndex]).GetEffort();
                //}
            }

            if (spreadSheetCard.Columns[e.ColumnIndex].ValueType == typeof(Waypoint)) {
                saveCardRow(spreadSheetCard.Rows[e.RowIndex]);
            }
        }

        private void spreadSheetCard_RowEnter(object sender, DataGridViewCellEventArgs e) {

            DataGridViewRow gridRow = spreadSheetCard.Rows[e.RowIndex];
            int id = (int)gridRow.Cells[columnCardID.Name].Value;
            Survey.CardRow cardRow = data.Card.FindByID(id);

            // TODO: Set ReadOnly to Columns that does not impact effort

            //if (cardRow.IsEqpIDNull()) { 

            //} else {
            //    Survey.SamplerRow samplerRow = cardRow.SamplerRow;
            //    if (samplerRow.IsEffortFormulaNull()) {

            //        foreach (DataGridViewColumn gridColumn in new DataGridViewColumn[] { }) {
            //            gridRow.Cells[gridColumn.Index].ReadOnly = true;
            //        }
            //    } else {
            //    }
            //}
        }



        private void contextCard_Opening(object sender, CancelEventArgs e) {
            CardStack stack = getCardStack(spreadSheetCard.SelectedRows);
            contextCardLog.Enabled = stack.GetLogRows().Length > 0;
            contextCardIndividuals.Enabled = stack.QuantitySampled() > 0;
        }

        private void contextCardOpen_Click(object sender, EventArgs e) {
            foreach (string path in getCardStack(spreadSheetCard.SelectedRows).GetFilenames()) {
                IO.RunFile(path);
            }
        }

        private void contextCardOpenFolder_Click(object sender, EventArgs e) {
            List<string> directories = new List<string>();

            foreach (string path in getCardStack(spreadSheetCard.SelectedRows).GetFilenames()) {
                string dir = Path.GetDirectoryName(path);

                if (!directories.Contains(dir)) {
                    directories.Add(dir);
                }
            }

            foreach (string dir in directories) {
                IO.RunFile(dir);
            }
        }

        private void contextCardSaveSet_Click(object sender, EventArgs e) {
            if (ReaderSettings.Interface.SaveDialog.ShowDialog(this) == DialogResult.OK) {
                File.WriteAllLines(ReaderSettings.Interface.SaveDialog.FileName, getCardStack(spreadSheetCard.SelectedRows).GetFilenames());
            }
        }

        private void contextCardLog_Click(object sender, EventArgs e) {
            loadLog(getCardStack(spreadSheetCard.SelectedRows));
        }

        private void contextCardIndividuals_Click(object sender, EventArgs e) {
            loadIndividuals(getCardStack(spreadSheetCard.SelectedRows));
        }

        private void contextCardPrintNotes_Click(object sender, EventArgs e) {
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note).Run();
        }

        private void contextCardPrintFull_Click(object sender, EventArgs e) {
            getCardStack(spreadSheetCard.SelectedRows).GetCardReport(CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Stratified | CardReportLevel.Individuals).Run();
        }

        #endregion




        #region Log

        private void spreadSheetLog_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex != columnLogQuantity.Index && e.ColumnIndex != columnLogMass.Index) return;

            if (spreadSheetLog.ContainsFocus) {
                saveLogRow(spreadSheetLog.Rows[e.RowIndex]);
            }
        }

        private void spreadSheetLog_SelectionChanged(object sender, EventArgs e) { }

        private void contextLog_Opening(object sender, CancelEventArgs e) {
            contextLogOpen.Enabled = spreadSheetLog.SelectedRows.Count > 0;

            bool hasSampled = false;

            foreach (Survey.LogRow logRow in getLogRows(spreadSheetLog.SelectedRows)) {
                hasSampled |= logRow.QuantityIndividual() > 0;
            }

            contextLogIndividuals.Enabled = hasSampled;
        }

        private void contextLogOpen_Click(object sender, EventArgs e) {
            foreach (Survey.LogRow logRow in getLogRows(spreadSheetLog.SelectedRows)) {
                IO.RunFile(logRow.CardRow.Path,
                    new object[] { logRow.DefinitionRow.Taxon });
            }
        }

        private void contextLogIndividuals_Click(object sender, EventArgs e) {
            loadIndividuals(getLogRows(spreadSheetLog.SelectedRows));
        }

        private void logLoader_DoWork(object sender, DoWorkEventArgs e) {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Survey.LogRow[] logRows = (Survey.LogRow[])e.Argument;

            for (int i = 0; i < logRows.Length; i++) {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetLog);
                gridRow.Cells[columnLogID.Index].Value = logRows[i].ID;
                updateLogRow(gridRow);
                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void logLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
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

        private void logExtender_DoWork(object sender, DoWorkEventArgs e) {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows) {
                Survey.LogRow logRow = findLogRow(gridRow);
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
                gridRow.DataGridView.Invoke(valueSetter,
                    new object[] { logRow.CardRow, gridRow, spreadSheetLog.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }

        private void buttonSelectLog_Click(object sender, EventArgs e) {
            if (loadCardAddt(spreadSheetLog)) {
                IsBusy = true;
                processDisplay.StartProcessing(Resources.Interface.Process.ExtLog);
                loaderLogExtended.RunWorkerAsync();
            }
        }

        #endregion




        #region Individuals

        private void menuItemIndividuals_DropDownOpening(object sender, EventArgs e) {

        }

        private void menuItemIndPrintLog_Click(object sender, EventArgs e) {
            getIndividuals(spreadSheetInd.Rows).GetReport(CardReportLevel.Individuals).Run();
        }

        private void menuItemIndPrint_Click(object sender, EventArgs e) {
            getIndividuals(spreadSheetInd.Rows).GetReport(CardReportLevel.Profile).Run();
        }

        private void menuItemIndExtra_CheckedChanged(object sender, EventArgs e) {

        }



        private void indLoader_DoWork(object sender, DoWorkEventArgs e) {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Survey.IndividualRow[] indRows = (Survey.IndividualRow[])e.Argument;

            for (int i = 0; i < indRows.Length; i++) {
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

        private void indLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            spreadSheetInd.Rows.AddRange(e.Result as DataGridViewRow[]);
            spreadSheetInd.UpdateStatus();
            IsBusy = false;
            spreadSheetInd.StopProcessing();

            tabPageInd.Parent = tabControl;
            tabControl.SelectedTab = tabPageInd;

            applyBio();
            updateArtifacts();
        }

        private void indExtender_DoWork(object sender, DoWorkEventArgs e) {
            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows) {
                ValueSetEventHandler valueSetter = new ValueSetEventHandler(setCardValue);
                gridRow.DataGridView.Invoke(valueSetter, new object[] { findIndividualRow(gridRow).LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns() });
                ((BackgroundWorker)sender).ReportProgress(gridRow.Index + 1);
            }
        }



        private void bioTipper_DoWork(object sender, DoWorkEventArgs e) {

            foreach (DataGridViewRow gridRow in (DataGridViewRow[])e.Argument) {
                if (UserSettings.SuggestAge) setIndividualAgeTip(gridRow);
                if (UserSettings.SuggestMass) setIndividualMassTip(gridRow);
            }
        }

        private void bioTipper_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            spreadSheetInd.StopProcessing();
        }


        private void spreadSheetInd_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (spreadSheetInd.Columns[e.ColumnIndex].ReadOnly) return;

            Survey.IndividualRow editedIndividualRow;

            // If values was typed in
            if (spreadSheetInd.ContainsFocus) {
                // Save all new values
                editedIndividualRow = saveIndividualRow(spreadSheetInd.Rows[e.RowIndex]);

                // If mass OR age was changed
                if (e.ColumnIndex == columnIndLength.Index || e.ColumnIndex == columnIndMass.Index || e.ColumnIndex == columnIndAge.Index) {
                    // Recalculate if needed
                    if (bioUpdater.IsBusy) { bioUpdater.CancelAsync(); } else { bioUpdater.RunWorkerAsync(editedIndividualRow.LogRow.DefinitionRow); }
                }

                // If mass was changed
                if (e.ColumnIndex == columnIndMass.Index) {
                    // Reset Total mass status text
                    statusMass.ResetFormatted(FullStack.Mass());
                    labelWgtValue.Text = Service.GetFriendlyMass(FullStack.Mass() * 1000);
                }

            } else {
                editedIndividualRow = findIndividualRow(spreadSheetInd.Rows[e.RowIndex]);
            }
        }

        private void bioUpdater_DoWork(object sender, DoWorkEventArgs e) {
            Survey.DefinitionRow definitionRow = (Survey.DefinitionRow)e.Argument;
            data.FindMassModel(definitionRow.Taxon).RefreshInternal();
            data.FindGrowthModel(definitionRow.Taxon).RefreshInternal();
            e.Result = speciesRow;
        }

        private void bioUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                bioUpdater.RunWorkerAsync(e.Result);
                return;
            }

            applyBio();
        }


        private void spreadSheetInd_SelectionChanged(object sender, EventArgs e) {

        }

        private void spreadSheetInd_Filtered(object sender, EventArgs e) {

        }

        private void spreadSheetInd_DisplayChanged(object sender, EventArgs e) {
            if (!bioTipper.IsBusy) {
                DataGridViewRow[] gridRows = spreadSheetInd.GetDisplayedRows().ToArray();
                processDisplay.StartProcessing(Resources.Interface.Process.BioApply);
                bioTipper.RunWorkerAsync(gridRows);
            }
        }


        private void contextInd_Opening(object sender, CancelEventArgs e) {

            printIndividualsLogToolStripMenuItem.Enabled = spreadSheetInd.SelectedRows.Count > 1;
        }

        private void contextIndCard_Click(object sender, EventArgs e) {
            if (spreadSheetCard.RowCount > 0) {
                selectCorrespondingCardRows();
                tabControl.SelectedTab = tabPageCard;
            } else {
                loaderCard.RunWorkerCompleted += selectCorrespondingCardRows;
                menuItemCards_Click(sender, e);
            }
        }

        private void selectCorrespondingCardRows() {

            spreadSheetCard.ClearSelection();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows) {
                if (!gridRow.Visible) continue;

                Survey.IndividualRow individualRow = findIndividualRow(gridRow);
                DataGridViewRow corrRow = columnCardID.GetRow(individualRow.LogRow.CardID);

                if (corrRow == null) continue;
                corrRow.Selected = true;
                spreadSheetCard.FirstDisplayedScrollingRowIndex = corrRow.Index;
            }
        }

        private void selectCorrespondingCardRows(object sender, RunWorkerCompletedEventArgs e) {

            selectCorrespondingCardRows();
            loaderLog.RunWorkerCompleted -= selectCorrespondingLogRows;
        }

        private void contextIndLog_Click(object sender, EventArgs e) {

            if (spreadSheetLog.RowCount > 0) {
                selectCorrespondingLogRows();
                tabControl.SelectedTab = tabPageLog;
            } else {
                loaderLog.RunWorkerCompleted += selectCorrespondingLogRows;
                menuItemLog_Click(sender, e);
            }
        }

        private void selectCorrespondingLogRows() {

            spreadSheetLog.ClearSelection();

            foreach (DataGridViewRow gridRow in spreadSheetInd.SelectedRows) {
                if (!gridRow.Visible) continue;

                Survey.IndividualRow individualRow = findIndividualRow(gridRow);
                DataGridViewRow corrRow = columnLogID.GetRow(individualRow.LogID);

                if (corrRow == null) continue;
                corrRow.Selected = true;
                spreadSheetLog.FirstDisplayedScrollingRowIndex = corrRow.Index;
            }
        }

        private void selectCorrespondingLogRows(object sender, RunWorkerCompletedEventArgs e) {

            selectCorrespondingLogRows();
            loaderLog.RunWorkerCompleted -= selectCorrespondingLogRows;
        }

        private void printIndividualsLogToolStripMenuItem_Click(object sender, EventArgs e) {

            getIndividuals(spreadSheetInd.SelectedRows).GetReport(CardReportLevel.Individuals).Run();
        }

        private void contextIndPrint_Click(object sender, EventArgs e) {

            getIndividuals(spreadSheetInd.SelectedRows).GetReport(CardReportLevel.Profile).Run();
        }

        private void buttonSelectInd_Click(object sender, EventArgs e) {

            if (loadCardAddt(spreadSheetInd)) {
                IsBusy = true;
                spreadSheetInd.StartProcessing(Resources.Interface.Process.ExtInd);
                loaderIndExtended.RunWorkerAsync();
            }
        }

        #endregion




        #region Species

        private void spcLoader_DoWork(object sender, DoWorkEventArgs e) {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            Composition composition;

            if (rank == null) {
                composition = FullStack.GetBasicCenosisComposition();
            } else {
                composition = FullStack.GetBasicTaxonomicComposition(ReaderSettings.TaxonomicIndex, rank);
            }

            processDisplay.SetProgressMaximum(composition.Count);

            for (int i = 0; i < composition.Count; i++) {
                if (composition[i].Quantity == 0) continue;

                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetSpc);

                if (composition is TaxonomicComposition tc) {
                    gridRow.Cells[columnSpcSpc.Index].Value = tc[i].DataRow;
                    gridRow.Cells[columnSpcID.Index].Value = tc[i].DataRow?.ID;

                    if (tc[i].DataRow == null) gridRow.Cells[columnSpcSpc.Index].Value = tc[i].Name;
                } else if (composition is SpeciesComposition sc) {
                    gridRow.Cells[columnSpcSpc.Index].Value = sc[i].TaxonRow;
                    gridRow.Cells[columnSpcID.Index].Value = sc[i].TaxonRow?.ID;
                }

                gridRow.Cells[columnSpcQuantity.Index].Value = composition[i].Quantity;
                gridRow.Cells[columnSpcMass.Index].Value = composition[i].Mass;

                gridRow.Cells[columnSpcOccurrence.Index].Value = composition[i].Occurrence;

                result.Add(gridRow);

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            e.Result = result.ToArray();
        }

        private void spcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            spreadSheetSpc.Rows.AddRange(e.Result as DataGridViewRow[]);
            IsBusy = false;
            spreadSheetSpc.StopProcessing();
            spreadSheetSpc.UpdateStatus();

            tabPageComposition.Parent = tabControl;
            tabControl.SelectedTab = tabPageComposition;
        }



        private void comboBoxSpcTaxon_SelectedIndexChanged(object sender, EventArgs e) {
            rank = comboBoxSpcTaxon.SelectedItem as TaxonomicRank;

            menuItemSpcTaxon.Enabled = rank == null;

            if (rank != null) {
                spreadSheetSpc.ClearInsertedColumns();
            }

            columnSpcSpc.HeaderText = rank == null ? Resources.Reports.Caption.Species : rank.ToString();

            loadSpc();
        }

        private void contextSpecies_Opening(object sender, CancelEventArgs e) {
            contextSpecies.Enabled = rank == null;

            bool hasSampled = false;

            foreach (TaxonomicIndex.TaxonRow spcRow in getSpeciesRows(spreadSheetSpc.SelectedRows)) {
                hasSampled |= FullStack.QuantityIndividual(spcRow) > 0;
            }

            contextSpcIndividuals.Enabled = hasSampled;
        }

        private void contextSpcLog_Click(object sender, EventArgs e) {
            loadLog(getSpeciesRows(spreadSheetSpc.SelectedRows));
        }

        private void contextSpcIndividuals_Click(object sender, EventArgs e) {
            loadIndividuals(getSpeciesRows(spreadSheetSpc.SelectedRows));
        }

        #endregion
    }
}