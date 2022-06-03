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
using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using Mayfly.Mathematics.Charts;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Wild;
using Mayfly.Mathematics.Statistics;
using System.Drawing;
using Mayfly.Extensions;
using Meta.Numerics.Statistics;
using System;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Wild;
using Mayfly.Controls;
using System;
using System.Linq;
using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mayfly.Controls;
using System.Linq;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

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
                    menuInstant.Enabled =

                    spreadSheetInd.Enabled = !value;


                menuSample.Enabled =
                    menuInstant.Enabled =
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
                    menuInstant.Enabled = !allowedEmpty;

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

        bool isClosing = false;


        private void updateSummary()
        {
            FullStack = new CardStack(data.Card);
            AllowedStack = new CardStack(data.Card); //, true);

            IsEmpty = FullStack.Count == 0;
            IsAllowedEmpty = AllowedStack.Count == 0;

            if (IsEmpty)
            {
                this.ResetText(EntryAssemblyInfo.Title);

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
                        menuItem.Click += (sender, e) =>
                        {
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


                AllowedStack.PopulateSpeciesMenu(menuItemIndAll, indSpecies_Click, (spcRow) =>
                {

                    return AllowedStack.QuantityIndividual(spcRow);

                });
                AllowedStack.PopulateSpeciesMenu(menuItemIndSuggested, indSuggested_Click, (spcRow) =>
                {

                    TreatmentSuggestion sugg = AllowedStack.GetTreatmentSuggestion(spcRow, data.Individual.AgeColumn);
                    return (sugg == null) ? 0 : sugg.GetSuggested().Length;

                });

                AllowedStack.PopulateSpeciesMenu(menuItemLog, logSpecies_Click, (spcRow) =>
                {

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

            if (tabPageInd.Parent != null)
            {
                foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
                {
                    updateIndividualArtifacts(gridRow);
                }
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
                spreadSheetInd_DisplayChanged(spreadSheetInd, new ScrollEventArgs(ScrollEventType.SmallDecrement, 0));
            }
        }

        private void mainForm_Scroll(object sender, ScrollEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void logSpecies_Click(object sender, EventArgs e)
        {
            loadLog((SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag);
        }

        private void indSpecies_Click(object sender, EventArgs e)
        {
            loadIndividuals((SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag);
        }

        private void indSuggested_Click(object sender, EventArgs e)
        {
            SpeciesKey.SpeciesRow speciesRow = (SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag;
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


        private void rememberChanged(Data.CardRow cardRow)
        {
            if (!changedCards.Contains(cardRow)) { changedCards.Add(cardRow); }
            menuItemSave.Enabled = IsChanged;
        }



        #region Gear stats

        private CardStack EffortsData;

        private CardStack CatchesData;

        private FishSamplerType SelectedSamplerType
        {
            get; set;
            //{
            //    return Fish.Service.Sampler(listViewGears.GetID()).GetSamplerType();
            //}
        }

        private UnitEffort SelectedEffortUE { get; set; }



        private Series GetSinkingSchedule(FishSamplerType samplerType)
        {
            Series series = new Series();
            series.LegendText = samplerType.ToDisplay();
            series.BorderColor = Color.Black;

            series.ChartType = SeriesChartType.RangeBar;
            series.YValuesPerPoint = 2;
            series.SetCustomProperty("DrawSideBySide", "False");
            //series.SetCustomProperty("PointWidth", "2");

            foreach (Data.CardRow cardRow in FullStack.GetStack(samplerType))
            {
                if (cardRow.IsMeshNull())
                    continue;

                if (cardRow.IsWhenNull())
                    continue;

                int pointIndex = series.Points.AddXY(cardRow.Mesh, cardRow.When - (cardRow.IsSpanNull() ? TimeSpan.FromHours(2) : cardRow.Duration), cardRow.When);
                series.Points[pointIndex].Tag = cardRow;
                series.Points[pointIndex].ToolTip = cardRow.ToString();
            }

            return series;
        }

        //private void UpdateCatchTotals()
        //{
        //    double q = 0.0;
        //    double w = 0.0;

        //    for (int i = 0; i < spreadSheetCatches.Rows.Count; i++)
        //    {
        //        if (spreadSheetCatches[columnCatchSpc.Index, i].Value.ToString() == Mayfly.Resources.Interface.Total)
        //        {
        //            continue;
        //        }

        //        if (!spreadSheetCatches.IsHidden(i))
        //        {
        //            if (spreadSheetCatches[columnCatchN.Index, i].Value != null)
        //            {
        //                q += (double)spreadSheetCatches[columnCatchN.Index, i].Value;
        //            }

        //            if (spreadSheetCatches[columnCatchW.Index, i].Value != null)
        //            {
        //                w += (double)spreadSheetCatches[columnCatchW.Index, i].Value;
        //            }
        //        }
        //    }

        //    for (int i = 0; i < spreadSheetCatches.Rows.Count; i++)
        //    {
        //        if (spreadSheetCatches[columnCatchSpc.Index, i].Value.ToString() == Mayfly.Resources.Interface.Total)
        //        {
        //            spreadSheetCatches[columnCatchN.Index, i].Value = q;
        //            spreadSheetCatches[columnCatchW.Index, i].Value = w;
        //        }
        //    }
        //}

        #endregion



        #region Species stats

        SpeciesKey.SpeciesRow selectedStatSpc;

        FishSamplerType selectedTechSamplerType;

        Series sufficientLine;

        Histogramma histBio;
        Histogramma histSample;
        Histogramma histWeighted;
        Histogramma histRegistered;
        Histogramma histAged;

        DataQualificationWay selectedQualificationWay;

        ContinuousBio model;
        BivariateSample outliersData;

        Scatterplot ext;
        Scatterplot inter;
        Scatterplot combi;
        Scatterplot outliers;

        private void getFilteredList(DataGridViewColumn gridColumn)
        {
            spreadSheetInd.Rows.Clear();

            if (selectedStatSpc == null)
            {
                spreadSheetInd.EnsureFilter(gridColumn, null, loaderInd,
                    menuItemIndAllAll_Click);
            }
            else
            {
                spreadSheetInd.EnsureFilter(gridColumn, null, loaderInd,
                    (sender, e) => { loadIndividuals(selectedStatSpc); });
            }
        }



        private void clearSpcStats()
        {
            textBoxSpcLog.Text = Constants.Null;
            textBoxSpcL.Text = Constants.Null;
            textBoxSpcW.Text = Constants.Null;
            textBoxSpcA.Text = Constants.Null;
            textBoxSpcS.Text = Constants.Null;
            textBoxSpcM.Text = Constants.Null;
            textBoxSpcStrat.Text = Constants.Null;
            textBoxSpcWTotal.Text = Constants.Null;
            textBoxSpcWLog.Text = Constants.Null;
            textBoxSpcWStrat.Text = Constants.Null;
            textBoxSpsTotal.Text = Constants.Null;

            chartSpcStats.Series[0].Points.Clear();
        }

        private DataGridViewRow GetTechRow(CardStack stack)
        {
            string format = "{0} ({1})";

            DataGridViewRow result = new DataGridViewRow();
            result.CreateCells(spreadSheetTech);
            result.Cells[ColumnSpcTechClass.Index].Value = stack.Name;
            result.Cells[ColumnSpcTechOps.Index].Value = stack.Count;
            double quantity = (selectedStatSpc == null) ? stack.Quantity() : stack.Quantity(selectedStatSpc);

            if (quantity > 0)
            {
                result.Cells[ColumnSpcTechN.Index].Value = quantity;

                int totals = 0;
                int strats = 0;
                //int mixed = 0;
                int logged = 0;

                int totals_q = 0;
                int strats_q = 0;
                //int mixed_q = 0;
                int logged_q = 0;

                foreach (Data.CardRow cardRow in stack)
                {
                    int stratified = 0;
                    int individuals = 0;

                    if (selectedStatSpc == null)
                    {
                        bool all_totalled = true;

                        foreach (Data.LogRow logRow in cardRow.GetLogRows())
                        {
                            if (logRow.IsQuantityNull() || logRow.IsMassNull()) { all_totalled = false; break; }

                            if (!logRow.IsQuantityNull()) totals_q += logRow.Quantity;
                            stratified += logRow.QuantityStratified;
                            individuals += logRow.QuantityIndividuals;
                        }

                        if (all_totalled) totals++;
                    }
                    else
                    {
                        Data.LogRow logRow = data.Log.FindByCardIDSpcID(cardRow.ID, selectedStatSpc.ID);

                        if (logRow == null) continue;

                        if (!logRow.IsQuantityNull() && !logRow.IsMassNull()) totals++;
                        if (!logRow.IsQuantityNull()) totals_q += logRow.Quantity;

                        stratified = logRow.QuantityStratified;
                        individuals = logRow.QuantityIndividuals;
                    }

                    //if (stratified > 0 && individuals == 0) { strats++; strats_q += stratified; }
                    //if (stratified > 0 && individuals > 0) { mixed++; mixed_q += stratified + individuals; }
                    //if (stratified == 0 && individuals > 0) { logged++; logged_q += individuals; }

                    if (stratified > 0) { strats++; }
                    if (individuals > 0) { logged++; }

                    strats_q += stratified;
                    logged_q += individuals;
                }

                if (totals > 0) result.Cells[ColumnSpcTechTotals.Index].Value = string.Format(format, totals, totals_q);
                if (strats > 0) result.Cells[ColumnSpcTechStratified.Index].Value = string.Format(format, strats, strats_q);
                //if (mixed > 0) result.Cells[ColumnSpcTechMixed.Index].Value = string.Format(format, mixed, mixed_q);
                if (logged > 0) result.Cells[ColumnSpcTechLog.Index].Value = string.Format(format, logged, logged_q);
            }

            return result;
        }

        private void initializeSpeciesStatsPlot()
        {
            if (plotQualify.Series.Count > 1) return;

            plotQualify.Series.Clear();

            sufficientLine = new Series(Resources.Interface.EnoughStamp)
            {
                Color = Mathematics.UserSettings.ColorSelected,
                ChartType = SeriesChartType.Line,
                BorderDashStyle = ChartDashStyle.Dash
            };
            sufficientLine.Points.AddXY(0, UserSettings.RequiredClassSize);
            sufficientLine.Points.AddXY(1, UserSettings.RequiredClassSize);
            plotQualify.Series.Add(sufficientLine);

            plotQualify.AxisXInterval = Fish.UserSettings.DefaultStratifiedInterval;

            histBio = new Histogramma(Resources.Interface.StratesBio);
            histSample = new Histogramma(Resources.Interface.StratesSampled);
            histWeighted = new Histogramma(Resources.Interface.StratesWeighted);
            histRegistered = new Histogramma(Resources.Interface.StratesRegistered);
            histAged = new Histogramma(Resources.Interface.StratesAged);

            foreach (Histogramma hist in new Histogramma[] { histBio, histSample, histWeighted, histRegistered, histAged })
            {
                hist.Properties.Borders = false;
                hist.Properties.DataPointColor = plotQualify.GetNextColor(.5);
                hist.Properties.PointWidth = .5;
                plotQualify.AddSeries(hist);
            }

            ext = new Scatterplot(Resources.Interface.QualBio);
            ext.Properties.DataPointColor = Constants.InfantColor;
            plotQualify.AddSeries(ext);

            inter = new Scatterplot(Resources.Interface.QualOwn);
            inter.Properties.DataPointColor = Mathematics.UserSettings.ColorAccent;
            plotQualify.AddSeries(inter);
            inter.Updated += inter_Updated;

            combi = new Scatterplot(Resources.Interface.QualCombi);
            combi.Properties.ShowTrend = true;
            combi.Properties.ConfidenceLevel = .99999;
            combi.Properties.ShowPredictionBands = true;
            combi.Properties.HighlightOutliers = checkBoxQualOutliers.Checked;
            combi.Properties.DataPointColor = Color.Transparent;
            combi.Properties.TrendColor = Mathematics.UserSettings.ColorAccent;
            plotQualify.AddSeries(combi);
            combi.Updated += combi_Updated;

            //plotQualify.Remove(Resources.Interface.StratesSampled, false);
            //plotQualify.Remove(Resources.Interface.StratesWeighted, false);
            //plotQualify.Remove(Resources.Interface.StratesRegistered, false);
            //plotQualify.Remove(Resources.Interface.StratesAged, false);
        }

        private void resetQualPlotAxes(double from, double to, double top)
        {
            if (!plotQualify.AxisXAutoMinimum)
            {
                plotQualify.AxisXMin = Mayfly.Service.AdjustLeft(from, to);
            }

            if (!plotQualify.AxisXAutoMaximum)
            {
                plotQualify.AxisXMax = Mayfly.Service.AdjustRight(from, to);
            }

            if (!plotQualify.AxisYAutoMaximum)
            {
                plotQualify.AxisYMax = Mayfly.Service.AdjustRight(0, Math.Max(UserSettings.RequiredClassSize, top));
            }

            sufficientLine.Points[0].XValue = plotQualify.AxisXMin;
            sufficientLine.Points[1].XValue = plotQualify.AxisXMax;

            //LengthComposition lc = AllowedStack.GetStatisticComposition(selectedStatSpc, (s, i) => { return AllowedStack.Quantity(s, i); }, string.Empty);
            //plotQualify.AxisYMax = Mayfly.Service.AdjustRight(0, lc.MostSampled.Quantity);
            //plotQualify.AxisXMin = Mayfly.Service.AdjustLeft(lc[0].Size.LeftEndpoint, ((SizeClass)lc.GetLast()).Size.RightEndpoint);
            //plotQualify.AxisXMax = Mayfly.Service.AdjustRight(lc[0].Size.LeftEndpoint, ((SizeClass)lc.GetLast()).Size.RightEndpoint);
        }

        private void resetQualPlotAxes()
        {
            resetQualPlotAxes(
                Service.GetStrate(AllowedStack.LengthMin(selectedStatSpc)).LeftEndpoint,
                Service.GetStrate(AllowedStack.LengthMax(selectedStatSpc)).RightEndpoint,
                AllowedStack.GetLengthComposition(selectedStatSpc, plotQualify.AxisXInterval).MostSampled.Quantity);

        }

        private void inter_Updated(object sender, ScatterplotEventArgs e)
        {
            combi.Properties.TrendColor = inter.Properties.DataPointColor.Darker();
        }

        private void combi_Updated(object sender, ScatterplotEventArgs e)
        {
            outliersData = (combi.Calc != null && combi.Calc.IsRegressionOK) ? combi.Calc.Regression.GetOutliers(inter.Calc.Data, combi.Properties.ConfidenceLevel) : new BivariateSample();

            checkBoxQualOutliers.Enabled = buttonQualOutliers.Enabled =
                outliersData.Count > 0;

            checkBoxQualOutliers_CheckedChanged(sender, e);
        }

        #endregion



        #region Species

        SpeciesKey.BaseRow baseSpc;

        SpeciesKey.TaxaRow[] taxaSpc;



        private void loadTaxaList()
        {
            // Clear list
            comboBoxSpcTaxa.Items.Clear();
            menuItemSpcTaxa.DropDownItems.Clear();

            if (Fish.UserSettings.SpeciesIndex == null) return;

            // Fill list
            foreach (SpeciesKey.BaseRow baseRow in Fish.UserSettings.SpeciesIndex.Base)
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
            spreadSheetSpc.StartProcessing(baseSpc == null ? data.Species.Count : taxaSpc.Length,
                Wild.Resources.Interface.Process.SpeciesProcessing);

            spreadSheetSpc.Rows.Clear();

            loaderSpc.RunWorkerAsync();
        }



        private SpeciesKey.SpeciesRow findSpeciesRow(DataGridViewRow gridRow)
        {
            return baseSpc == null ? Fish.UserSettings.SpeciesIndex.Species.FindByID((int)gridRow.Cells[columnSpcID.Index].Value) : null;
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

        List<DataGridViewColumn> effortSource
        {
            get
            {
                return new List<DataGridViewColumn>
                {
                    columnCardExposure,
                    columnCardHeight,
                    columnCardHook,
                    columnCardLength,
                    columnCardOpening,
                    columnCardSquare,
                    columnCardSpan,
                    columnCardVelocity
                };
            }
        }



        private void loadCards(CardStack stack)
        {
            IsBusy = true;
            spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);
            spreadSheetCard.Rows.Clear();

            loaderCard.RunWorkerAsync(stack);
        }

        private void loadCards()
        {
            loadCards(AllowedStack);
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

            setCardValue(cardRow, gridRow, ColumnCardWeather, "Weather");
            setCardValue(cardRow, gridRow, ColumnCardTempSurface, "Surface");

            setCardValue(cardRow, gridRow, columnCardGear, "Gear");
            setCardValue(cardRow, gridRow, columnCardMesh, "Mesh");
            setCardValue(cardRow, gridRow, columnCardHook, "Hook");
            setCardValue(cardRow, gridRow, columnCardLength, "Length");
            setCardValue(cardRow, gridRow, columnCardOpening, "Opening");
            setCardValue(cardRow, gridRow, columnCardHeight, "Height");
            setCardValue(cardRow, gridRow, columnCardSquare, "Square");
            setCardValue(cardRow, gridRow, columnCardSpan, "Span");
            setCardValue(cardRow, gridRow, columnCardVelocity, "Velocity");
            setCardValue(cardRow, gridRow, columnCardExposure, "Exposure");

            //if (Wild.UserSettings.InstalledPermissions.IsPermitted(cardRow.Investigator))
            //{
            setCardValue(cardRow, gridRow, columnCardEffort, "Effort");
            setCardValue(cardRow, gridRow, columnCardDepth, "Depth");
            setCardValue(cardRow, gridRow, columnCardWealth, "Wealth");
            setCardValue(cardRow, gridRow, columnCardQuantity, "Quantity");
            setCardValue(cardRow, gridRow, columnCardMass, "Mass");
            setCardValue(cardRow, gridRow, columnCardAbundance, "Abundance");
            setCardValue(cardRow, gridRow, columnCardBiomass, "Biomass");
            setCardValue(cardRow, gridRow, columnCardDiversityA, "DiversityA");
            setCardValue(cardRow, gridRow, columnCardDiversityB, "DiversityB");
            //}

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

            if (artifact.EffortCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnCardEffort.Index]).Image = ConsistencyChecker.GetImage(artifact.EffortCriticality);
                gridRow.Cells[columnCardEffort.Index].ToolTipText = artifact.GetNotices(false).Merge();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnCardEffort.Index]).Image = null;
                gridRow.Cells[columnCardEffort.Index].ToolTipText = string.Empty;
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

            object hook = (object)gridRow.Cells[columnCardHook.Name].Value;
            if (hook == null) cardRow.SetHookNull();
            else cardRow.Hook = (int)hook;

            object length = (object)gridRow.Cells[columnCardLength.Name].Value;
            if (length == null) cardRow.SetLengthNull();
            else cardRow.Length = (double)length;

            object opening = (object)gridRow.Cells[columnCardOpening.Name].Value;
            if (opening == null) cardRow.SetOpeningNull();
            else cardRow.Opening = (double)opening;

            object height = gridRow.Cells[columnCardHeight.Name].Value;
            if (height == null) cardRow.SetHeightNull();
            else cardRow.Height = (double)height;

            object square = (object)gridRow.Cells[columnCardSquare.Name].Value;
            if (square == null) cardRow.SetSquareNull();
            else cardRow.Square = (double)square;

            object time = (object)gridRow.Cells[columnCardSpan.Name].Value;
            if (time == null) cardRow.SetSpanNull();
            else cardRow.Span = (int)((TimeSpan)time).TotalMinutes;

            object velocity = (object)gridRow.Cells[columnCardVelocity.Name].Value;
            if (velocity == null) cardRow.SetVelocityNull();
            else cardRow.Velocity = (double)velocity;

            object exposure = (object)gridRow.Cells[columnCardExposure.Name].Value;
            if (exposure == null) cardRow.SetExposureNull();
            else cardRow.Exposure = (double)exposure;

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

            switch (field)
            {
                case "Effort":
                    gridRow.Cells[gridColumn.Index].Style.Format = gridColumn.ExtendFormat(cardRow.GetGearType().GetDefaultUnitEffort().Unit);
                    break;
            }
        }

        private delegate void ValueSetEventHandler(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);

        #endregion



        #region Log




        private void loadLog(Data.LogRow[] logRows)
        {
            IsBusy = true;
            spreadSheetLog.StartProcessing(logRows.Length, Wild.Resources.Interface.Process.SpeciesProcessing);
            spreadSheetLog.Rows.Clear();
            loaderLog.RunWorkerAsync(logRows);
        }

        private void loadLog()
        {
            loadLog(data.Log.Rows.Cast<Data.LogRow>().ToArray());
        }

        private void loadLog(SpeciesKey.SpeciesRow[] spcRows, CardStack stack)
        {
            List<Data.LogRow> logRows = new List<Data.LogRow>();

            foreach (SpeciesKey.SpeciesRow spcRow in spcRows)
            {
                logRows.AddRange(stack.GetLogRows(spcRow));
            }

            loadLog(logRows.ToArray());
        }

        private void loadLog(SpeciesKey.SpeciesRow[] spcRows)
        {
            loadLog(spcRows, FullStack);
        }

        private void loadLog(SpeciesKey.SpeciesRow spcRows)
        {
            loadLog(new SpeciesKey.SpeciesRow[] { spcRows });
        }

        private void loadLog(CardStack stack)
        {
            loadLog(stack.GetSpecies(), stack);
        }



        private Data.LogRow findLogRow(DataGridViewRow gridRow)
        {
            return baseSpc == null ? data.Log.FindByID((int)gridRow.Cells[columnLogID.Index].Value) : null;
        }

        private void updateLogRow(DataGridViewRow gridRow)
        {
            Data.LogRow logRow = findLogRow(gridRow);

            gridRow.Cells[columnLogSpc.Index].Value = logRow.SpeciesRow;

            if (!logRow.IsQuantityNull())
            {
                gridRow.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
                gridRow.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance();
                gridRow.Cells[columnLogAbundance.Index].Style.Format = columnLogAbundance.ExtendFormat(logRow.CardRow.GetAbundanceUnits());
            }

            if (!logRow.IsMassNull())
            {
                gridRow.Cells[columnLogMass.Index].Value = logRow.Mass;
                gridRow.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass();
                gridRow.Cells[columnLogBiomass.Index].Style.Format = columnLogBiomass.ExtendFormat(logRow.CardRow.GetBiomassUnits());
            }

            setCardValue(logRow.CardRow, gridRow, spreadSheetLog.GetInsertedColumns());

            updateLogArtifacts(gridRow);
        }

        private void updateLogArtifacts(DataGridViewRow gridRow)
        {
            if (gridRow == null) return;

            if (!UserSettings.CheckConsistency) return;

            LogConsistencyChecker artifact = findLogRow(gridRow).CheckConsistency();

            if (artifact.OddMassCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnLogMass.Index]).Image = ConsistencyChecker.GetImage(artifact.OddMassCriticality);
                gridRow.Cells[columnLogMass.Index].ToolTipText = artifact.GetNoticeOddMass();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnLogMass.Index]).Image = null;
                gridRow.Cells[columnLogMass.Index].ToolTipText = string.Empty;

            }

            if (artifact.UnmeasurementsCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnLogQuantity.Index]).Image = ConsistencyChecker.GetImage(artifact.UnmeasurementsCriticality);
                gridRow.Cells[columnLogQuantity.Index].ToolTipText = artifact.GetNoticeUnmeasured();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnLogQuantity.Index]).Image = null;
                gridRow.Cells[columnLogQuantity.Index].ToolTipText = string.Empty;
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



        private DataGridViewRow createTaxonLogRow(Data.CardRow cardRow, SpeciesKey.TaxaRow taxaRow)
        {
            DataGridViewRow result = new DataGridViewRow();

            result.CreateCells(spreadSheetLog);

            result.Cells[columnLogID.Index].Value = cardRow.ID;

            int Q = 0;
            double W = 0.0;
            double A = 0.0;
            double B = 0.0;
            List<double> abundances = new List<double>();
            List<double> biomasses = new List<double>();

            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                if (double.IsNaN(cardRow.GetEffort())) continue;

                if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

                if (!logRow.IsQuantityNull())
                {
                    Q += logRow.Quantity;
                    A += logRow.GetAbundance();
                    abundances.Add(logRow.GetAbundance());
                }

                if (!logRow.IsMassNull())
                {
                    W += logRow.Mass;
                    B += logRow.GetBiomass();
                    biomasses.Add(logRow.GetBiomass());
                }
            }

            setCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

            result.Cells[columnLogSpc.Index].Value = taxaRow.TaxonName;

            result.Cells[columnLogQuantity.Index].Value = Q;
            result.Cells[columnLogMass.Index].Value = W;
            result.Cells[columnLogAbundance.Index].Value = A;
            result.Cells[columnLogBiomass.Index].Value = B;
            result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
            result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

            return result;
        }

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
            gridRow.Cells[columnIndSomaticMass.Index].Value = individualRow.IsSomaticMassNull() ? null : (object)individualRow.SomaticMass;
            gridRow.Cells[columnIndCondition.Index].Value = double.IsNaN(individualRow.GetCondition()) ? null : (object)individualRow.GetCondition();
            gridRow.Cells[columnIndConditionSoma.Index].Value = double.IsNaN(individualRow.GetConditionSomatic()) ? null : (object)individualRow.GetConditionSomatic();
            gridRow.Cells[columnIndTally.Index].Value = individualRow.IsTallyNull() ? null : individualRow.Tally;

            if (individualRow.IsAgeNull())
            {
                gridRow.Cells[columnIndAge.Index].Value = null;
                gridRow.Cells[columnIndGeneration.Index].Value = null;
            }
            else
            {
                Age age = (Age)individualRow.Age;
                gridRow.Cells[columnIndAge.Index].Value = age;
                Wild.Service.HandleAgeInput(gridRow.Cells[columnIndAge.Index], columnIndAge.DefaultCellStyle);
                if (!individualRow.IsAgeNull()) gridRow.Cells[columnIndGeneration.Index].Value = individualRow.Generation;
            }

            gridRow.Cells[columnIndSex.Index].Value = individualRow.IsSexNull() ? null : (Sex)individualRow.Sex;
            gridRow.Cells[columnIndMaturity.Index].Value = individualRow.IsMaturityNull()
                ? null
                : individualRow.IsIntermatureNull()
                    ? new Maturity(individualRow.Maturity)
                    : new Maturity(individualRow.Maturity, individualRow.Intermature);


            gridRow.Cells[columnIndGonadMass.Index].Value = individualRow.IsGonadMassNull() ? null : (object)individualRow.GonadMass;
            gridRow.Cells[columnIndGonadSampleMass.Index].Value = individualRow.IsGonadSampleMassNull() ? null : (object)individualRow.GonadSampleMass;
            gridRow.Cells[columnIndGonadSample.Index].Value = individualRow.IsGonadSampleNull() ? null : (object)individualRow.GonadSample;
            gridRow.Cells[columnIndEggSize.Index].Value = individualRow.IsEggSizeNull() ? null : (object)individualRow.EggSize;
            gridRow.Cells[columnIndFecundityAbs.Index].Value = double.IsNaN(individualRow.GetAbsoluteFecundity()) ? null : (object)(individualRow.GetAbsoluteFecundity() / 1000.0);
            gridRow.Cells[columnIndFecundityRelative.Index].Value = double.IsNaN(individualRow.GetRelativeFecundity()) ? null : (object)individualRow.GetRelativeFecundity();
            gridRow.Cells[columnIndFecundityRelativeSoma.Index].Value = double.IsNaN(individualRow.GetRelativeFecunditySomatic()) ? null : (object)individualRow.GetRelativeFecunditySomatic();
            gridRow.Cells[columnIndGonadIndex.Index].Value = double.IsNaN(individualRow.GetGonadIndex()) ? null : (object)individualRow.GetGonadIndex();
            gridRow.Cells[columnIndGonadIndexSoma.Index].Value = double.IsNaN(individualRow.GetGonadIndexSomatic()) ? null : (object)individualRow.GetGonadIndexSomatic();

            
            gridRow.Cells[columnIndFat.Index].Value = individualRow.IsFatnessNull() ? null : (object)individualRow.Fatness;

            if (individualRow.IsConsumedMassNull())
            {
                gridRow.Cells[columnIndConsumed.Index].Value = null;
                gridRow.Cells[columnIndConsumptionIndex.Index].Value = null;
            }
            else
            {
                gridRow.Cells[columnIndConsumed.Index].Value = individualRow.ConsumedMass;
                gridRow.Cells[columnIndConsumptionIndex.Index].Value = individualRow.GetConsumptionIndex();
            }

            int dietCount = individualRow.GetDietItemCount();
            gridRow.Cells[columnIndDietItems.Index].Value = dietCount == -1 ? null : (object)dietCount;

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

            if (artifact.UnweightedDietItemsCriticality > ArtifactCriticality.Normal)
            {
                ((TextAndImageCell)gridRow.Cells[columnIndConsumed.Index]).Image = ConsistencyChecker.GetImage(artifact.UnweightedDietItemsCriticality);
                gridRow.Cells[columnIndConsumed.Index].ToolTipText = artifact.GetNoticeUnweightedDiet();
            }
            else
            {
                ((TextAndImageCell)gridRow.Cells[columnIndConsumed.Index]).Image = null;
                gridRow.Cells[columnIndConsumed.Index].ToolTipText = string.Empty;
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

            object gmass = gridRow.Cells[columnIndSomaticMass.Name].Value;
            if (gmass == null) individualRow.SetSomaticMassNull();
            else individualRow.SomaticMass = (double)gmass;

            object tally = gridRow.Cells[columnIndTally.Name].Value;
            if (tally == null) individualRow.SetTallyNull();
            else individualRow.Tally = (string)tally;

            Age age = (Age)gridRow.Cells[columnIndAge.Name].Value;
            if (age == null) individualRow.SetAgeNull();
            else individualRow.Age = age.Value;

            if ((tabPageSpcStats.Parent != null) && // If stats are loaded
                (selectedStatSpc.Species == individualRow.Species)) // and selected species is currently editing
            {
                strates_Changed(this, new EventArgs());
            }

            Sex sex = (Sex)gridRow.Cells[columnIndSex.Name].Value;
            if (sex == null) individualRow.SetSexNull();
            else individualRow.Sex = sex.Value;

            object maturity = gridRow.Cells[columnIndMaturity.Name].Value;
            if (maturity == null)
            {
                individualRow.SetMaturityNull();
                individualRow.SetIntermatureNull();
            }
            else
            {
                individualRow.Maturity = ((Maturity)maturity).Value;
                individualRow.Intermature = ((Maturity)maturity).IsIntermediate;
            }

            #region Fecundity

            object gonadMass = gridRow.Cells[columnIndGonadMass.Name].Value;
            if (gonadMass == null) individualRow.SetGonadMassNull();
            else individualRow.GonadMass = (double)gonadMass;

            object gonadSampleMass = gridRow.Cells[columnIndGonadSampleMass.Name].Value;
            if (gonadSampleMass == null) individualRow.SetGonadSampleMassNull();
            else individualRow.GonadSampleMass = (double)gonadSampleMass;

            object gonadSample = gridRow.Cells[columnIndGonadSample.Name].Value;
            if (gonadSample == null) individualRow.SetGonadSampleNull();
            else individualRow.GonadSample = (int)gonadSample;

            object eggSize = gridRow.Cells[columnIndEggSize.Name].Value;
            if (eggSize == null) individualRow.SetEggSizeNull();
            else individualRow.EggSize = (double)eggSize;

            #endregion

            #region Diet

            object fat = gridRow.Cells[columnIndFat.Name].Value;
            if (fat == null) individualRow.SetFatnessNull();
            else individualRow.Fatness = (int)fat;

            #endregion

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



        private DataGridViewRow[] IndividualRows(Data.StratifiedRow stratifiedRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            for (int i = 0; i < stratifiedRow.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetInd);
                gridRow.ReadOnly = true;
                gridRow.DefaultCellStyle.ForeColor = Constants.InfantColor; // Color.DarkGray;

                //gridRow.Cells[columnIndID.Index].Value;

                gridRow.Cells[columnIndSpecies.Index].Value = stratifiedRow.LogRow.SpeciesRow;
                gridRow.Cells[columnIndLength.Index].Value = stratifiedRow.SizeClass.Midpoint;
                gridRow.Cells[columnIndMass.Index].Value = data.FindMassModel(stratifiedRow.LogRow.SpeciesRow.Species).GetValue(stratifiedRow.SizeClass.Midpoint);
                gridRow.Cells[columnIndComments.Index].Value = Resources.Interface.SimulatedIndividual;
                Age age = (Age)data.FindGrowthModel(stratifiedRow.LogRow.SpeciesRow.Species).GetValue(stratifiedRow.SizeClass.Midpoint, true);

                gridRow.Cells[columnIndAge.Index].Value = age;
                Wild.Service.HandleAgeInput(gridRow.Cells[columnIndAge.Index], columnIndAge.DefaultCellStyle);

                setCardValue(stratifiedRow.LogRow.CardRow, gridRow, spreadSheetInd.GetInsertedColumns());

                result.Add(gridRow);
            }

            return result.ToArray();
        }

        private DataGridViewRow[] IndividualRows(Data.LogRow logRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
            {
                result.AddRange(IndividualRows(stratifiedRow));
            }

            return result.ToArray();
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

        private void setIndividualAgeTip(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[columnIndAge.Index].Value != null) return;

            //if (gridRow.Cells[columnIndAge.Index].Style.NullValue != null) return;

            Data.IndividualRow individualRow = findIndividualRow(gridRow);

            if (individualRow == null) return;

            if (individualRow.IsLengthNull()) return;

            double ageValue = double.NaN;

            if (individualSpecies == null)
            {
                ageValue = data.FindGrowthModel(individualRow.Species).GetValue(individualRow.Length, true);
            }
            else
            {
                ageValue = growthModel.GetValue(individualRow.Length, true);
            }

            Age age = new Age(ageValue);

            gridRow.Cells[columnIndAge.Index].SetNullValue(
                double.IsNaN(ageValue) ?
                Wild.Resources.Interface.Interface.SuggestionUnavailable : " " + age.ToString() + " ");
            //Wild.Service.HandleAgeInput(gridRow.Cells[columnIndAge.Index], columnIndAge.DefaultCellStyle);

            gridRow.Cells[columnIndGeneration.Index].SetNullValue(
                double.IsNaN(ageValue) ?
                Wild.Resources.Interface.Interface.SuggestionUnavailable :
                individualRow.LogRow.CardRow.When.AddYears(-age.Years).Year.ToString());

            // Set generation value
            if (individualRow.IsAgeNull()) { gridRow.Cells[columnIndGeneration.Index].Value = null; }
            else { gridRow.Cells[columnIndGeneration.Index].Value = individualRow.Generation; }
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

            foreach (DataGridViewRow gridRow in rows) {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.IndividualRow individualRow = findIndividualRow(gridRow);
                if (individualRow == null) continue;

                result.Add(individualRow);
            }

            return result.ToArray();
        }

        private void SimulateStratifiedSamples()
        {
            IsBusy = true;

            spreadSheetInd.StartProcessing(individualSpecies == null ?
                data.Stratified.Quantity : AllowedStack.QuantityStratified(individualSpecies),
                Wild.Resources.Interface.Process.StratifiedProcessing);

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderIndSimulated.RunWorkerAsync(individualSpecies);
        }

        private void ClearSimulated()
        {
            for (int i = 0; i < spreadSheetInd.Rows.Count; i++)
            {
                if (spreadSheetInd[columnIndID.Index, i].Value == null)
                {
                    spreadSheetInd.Rows.RemoveAt(i);
                    i--;
                }
            }

            spreadSheetInd.UpdateStatus();
        }


        #endregion



        #region Startified samples

        public bool stratifiedShown;

        private void loadStratifiedSamples(Data.LogRow[] logRows)
        {
            IsBusy = true;
            spreadSheetStratified.StartProcessing(data.Stratified.Quantity, Wild.Resources.Interface.Process.StratifiedProcessing);
            spreadSheetStratified.Rows.Clear();
            loaderStratified.RunWorkerAsync(logRows);
        }

        private void loadStratifiedSamples(CardStack stack)
        {
            loadStratifiedSamples(stack.GetLogRows());
        }

        private void loadStratifiedSamples()
        {
            loadStratifiedSamples(FullStack);
        }

        private void loadStratifiedSamples(SpeciesKey.SpeciesRow[] spcRows, CardStack stack)
        {
            List<Data.LogRow> logRows = new List<Data.LogRow>();

            foreach (SpeciesKey.SpeciesRow spcRow in spcRows)
            {
                logRows.AddRange(stack.GetLogRows(spcRow));
            }

            loadStratifiedSamples(logRows.ToArray());
        }

        private void loadStratifiedSamples(SpeciesKey.SpeciesRow[] spcRows)
        {
            loadStratifiedSamples(spcRows, FullStack);
        }



        private Data.LogRow findLogRowStratified(DataGridViewRow gridRow)
        {
            return data.Log.FindByID((int)gridRow.Cells[columnStratifiedID.Index].Value);
        }



        private Data.LogRow[] getLogRowsStratified(IList rows)
        {
            spreadSheetStratified.EndEdit();
            List<Data.LogRow> result = new List<Data.LogRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.LogRow logRow = findLogRowStratified(gridRow);
                if (logRow == null) continue;

                result.Add(logRow);
            }

            return result.ToArray();
        }



        private void showStratified()
        {
            if (!stratifiedShown)
            {
                stratifiedSample.Visible = true;
                spreadSheetStratified.Height -= (stratifiedSample.Height + stratifiedSample.Margin.Top);
                stratifiedShown = true;
            }
        }

        private void hideStratified()
        {
            if (stratifiedShown)
            {
                stratifiedSample.Visible = false;
                spreadSheetStratified.Height += (stratifiedSample.Height + stratifiedSample.Margin.Top);
                stratifiedShown = false;
                stratifiedSample.Reset();
            }
        }

        #endregion
    }

    public enum DataQualificationWay
    {
        None = -1,
        WeightOfLength = 0,
        AgeOfLength = 1
    }
}
