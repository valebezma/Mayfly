using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardPopulation : Form
    {
        WizardComposition ageCompositionWizard;

        WizardComposition lengthCompositionWizard;

        WizardGearSet gearWizard;

        SpeciesComposition classedComposition;



        public CardStack Data { get; set; }

        public Data.SpeciesRow SpeciesRow { set; get; }

        public SpeciesSwarm Swarm { set; get; }

        public GillnetSelectivityModel SelectivityModel { set; get; }

        public LengthComposition LengthStructure { set; get; }

        public AgeComposition AgeStructure { set; get; }

        public Scatterplot GrowthModel { get; private set; }
        Scatterplot growthInternal;
        Scatterplot growthExternal;

        public Scatterplot WeightModel { get; private set; }
        Scatterplot weightInternal;
        Scatterplot weightExternal;
        
        Scatterplot weightGrowthInternal;
        Scatterplot weightGrowthExternal;

        public event EventHandler ModelsReturned;

        public event EventHandler ModelsCalculated;

        public event EventHandler MortalityReturned;

        public event EventHandler MortalityCalculated;

        public Scatterplot unused;

        public Scatterplot CatchCurve { get; private set; }

        public AgeGroup YoungestCaught
        {
            get
            {
                return (AgeGroup)comboBoxMortalityAge.SelectedItem;
            }
        }

        public double Z { get; set; }

        public double Fi { get; set; }

        public double S { get; set; }



        private WizardPopulation()
        {
            InitializeComponent();

            this.RestoreAllCheckStates();
        }

        public WizardPopulation(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(SpeciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            labelBasicInstruction.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            pageStart.SetNavigation(false);

            statChartAL.Clear();
            statChartLW.Clear();
            buttonAL.Enabled = buttonLW.Enabled = false;

            Log.Write(EventType.WizardStarted, "Stock composition wizard is started for {0}.",
                speciesRow.Species);

            structureCalculator.RunWorkerAsync();
        }


        public void RunModels()
        {
            pageStart.Suppress = true;
            pageBasic.Suppress = true;
            wizardExplorer.NextPage();
        }


        public void RunMortality()
        {
            //wizardExplorer.SelectedPage = pageAge;
        }

        public double GetWeight(Age age)
        {
            if (GrowthModel == null)
                throw new ArgumentNullException();

            if (!GrowthModel.IsRegressionOK)
                throw new ArgumentNullException();

            if (WeightModel == null)
                throw new ArgumentNullException();

            if (!WeightModel.IsRegressionOK)
                throw new ArgumentNullException();

            double l = GrowthModel.Regression.Predict(age.Years + .5);
            double w = WeightModel.Regression.Predict(l);
            return w;
        }


        public Report GetReport()
        {
            Report report = new Report(string.Format(Resources.Reports.Header.SummaryPopulation, SpeciesRow.KeyRecord.FullNameReport));

            Data.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxReportGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            if (checkBoxReportAge.Checked)
            {
                ageCompositionWizard.AppendPopulationSectionTo(report);
            }

            if (checkBoxReportAgeCPUE.Checked | checkBoxReportAgeKeys.Checked)
            {
                report.BreakPage(PageBreakOption.Odd);
                report.AddChapterTitle(Resources.Reports.Chapter.Appendices);
            }

            if (checkBoxReportAgeCPUE.Checked)
            {
                ageCompositionWizard.AppendCalculationSectionTo(report);
            }

            if (checkBoxReportAgeKeys.Checked)
            {
                ageCompositionWizard.AddAgeRecoveryRoutines(report);
            }

            if (true)
            {
                AddGrowth(report);
            }

            if (true)
            {
                AddSelectivity(report);
            }

            if (true)
            {
                AddMortality(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddSelectivity(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Selectivity.Title, gearWizard.SelectedSamplerType.ToDisplay(), SpeciesRow.KeyRecord.FullNameReport);

            if (true)
            {
                report.AddParagraph(Resources.Reports.Sections.Selectivity.Paragraph1, SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

                report.AddTable(
                    lengthCompositionWizard.CatchesComposition.SeparateCompositions.ToArray().GetTable( 
                        CompositionColumn.Abundance,
                        string.Format(Resources.Reports.Sections.Selectivity.Table1, SpeciesRow.KeyRecord.FullNameReport),
                         lengthCompositionWizard.CatchesComposition.Name, Resources.Reports.Caption.GearClass)
                    );

                report.AddParagraph(Resources.Reports.Sections.Selectivity.Paragraph2, report.NextTableNumber);

                report.AddTable(
                     lengthCompositionWizard.CatchesComposition.GetTable(
                        CompositionColumn.Abundance | CompositionColumn.AbundanceFraction,
                        string.Format(Resources.Reports.Sections.Selectivity.Table2, SpeciesRow.KeyRecord.FullNameReport),
                         lengthCompositionWizard.CatchesComposition.Name)
                    );
            }

            if (true)
            {
                report.AddParagraph(
                    string.Format(Resources.Reports.Sections.Selectivity.Paragraph3,
                    report.NextTableNumber - 2, report.NextTableNumber));

                SelectivityModel.AddReport1(report, string.Format(Resources.Reports.Sections.Selectivity.Table3,
                    SpeciesRow.KeyRecord.FullNameReport, gearWizard.SelectedSamplerType.ToDisplay()));

                report.AddParagraph(
                    string.Format(Resources.Reports.Sections.Selectivity.Paragraph4, 
                    report.NextTableNumber - 1, SelectivityModel.SelectionFactor,
                    SelectivityModel.StandardDeviation,report.NextTableNumber));

                SelectivityModel.AddReport2(report, string.Format(Resources.Reports.Sections.Selectivity.Table4,
                    SpeciesRow.KeyRecord.FullNameReport));
            }

            if (true)
            {
                report.AddParagraph(
                    string.Format(Resources.Reports.Sections.Selectivity.Paragraph5,
                    report.NextTableNumber)
                    );

                Report.Table table5 = LengthStructure.GetTable(
                    CompositionColumn.Abundance | CompositionColumn.AbundanceFraction, 
                    string.Format(Resources.Reports.Sections.Selectivity.Table5, SpeciesRow.KeyRecord.FullNameReport),
                    LengthStructure.Name
                    );

                report.AddTable(table5);
            }
        }

        public void AddMortality(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Mortality.Title, SpeciesRow.KeyRecord.FullNameReport);

            report.AddParagraph(Resources.Reports.Sections.Mortality.Paragraph1, CatchCurve.Regression, 
                (Age)CatchCurve.Left, (Age)CatchCurve.Right,
                SpeciesRow.KeyRecord.FullNameReport);
            report.AddEquation(CatchCurve.Regression.GetEquation("CPUE(%)", "t"));

            report.AddParagraph(Resources.Reports.Sections.Mortality.Paragraph2, Z);
            report.AddEquation(@"S = e^{-" + Z.ToString("N5") + "} = " + S.ToString("N5"));
            report.AddEquation(@"φ = 1 - " + S.ToString("N5") + " = " + Fi.ToString("N5"));
        }

        public void AddGrowth(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Growth.Title, SpeciesRow.KeyRecord.FullNameReport);

            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph1,
                SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

            Report.Table table = AgeStructure.GetTable(
                CompositionColumn.Quantity | CompositionColumn.LengthSample | CompositionColumn.MassSample, 
                string.Format(Resources.Reports.Sections.Growth.Table1, SpeciesRow.KeyRecord.FullNameReport), 
                AgeStructure.Name);

            report.AddTable(table);

            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph3, WeightModel.Regression);
            report.AddEquation(WeightModel.Regression.GetEquation("W", "L"));

            if (GrowthModel.IsRegressionOK) {
                report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph2, GrowthModel.Regression);
                report.AddEquation(GrowthModel.Regression.GetEquation("L", "t"));
            }
        }



        private void checkBoxLength_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxLengthAdjusted.Enabled =
                checkBoxLength.Checked;
        }

        private void checkBoxLengthAdjusted_CheckedChanged(object sender, EventArgs e)
        { }

        private void checkBoxAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAgeAdjusted.Enabled =
                checkBoxAge.Checked;
        }

        private void checkBoxAgeAdjusted_CheckedChanged(object sender, EventArgs e)
        { }

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageLength.Suppress =
                !checkBoxLength.Checked;

            pageSelectionSource.Suppress =
                pageSelection.Suppress =
                pageLengthAdjusted.Suppress =
                !checkBoxLengthAdjusted.Checked;

            pageAge.Suppress =
                !checkBoxAge.Checked;

            pageMortality.Suppress =
                pageAgeAdjusted.Suppress =
                !checkBoxAgeAdjusted.Checked;

            checkBoxReportLength.Enabled =
                checkBoxLength.Checked;

            checkBoxReportLengthAdjusted.Enabled =
                checkBoxLengthAdjusted.Checked;

            checkBoxReportAge.Enabled =
                checkBoxAge.Checked;

            checkBoxReportAgeAdjusted.Enabled =
                checkBoxAgeAdjusted.Checked;
        }



        private void structureCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Swarm = Data.GetSwarm(SpeciesRow);

            LengthStructure = Data.GetLengthCompositionFrame(SpeciesRow, UserSettings.SizeInterval);
            LengthStructure.Name = Fish.Resources.Common.SizeUnits;

            growthInternal = Data.Parent.GrowthModels.GetInternalScatterplot(SpeciesRow.Species);
            growthExternal = Data.Parent.GrowthModels.GetExternalScatterplot(SpeciesRow.Species);
            GrowthModel = Data.Parent.GrowthModels.GetCombinedScatterplot(SpeciesRow.Species);

            weightInternal = Data.Parent.MassModels.GetInternalScatterplot(SpeciesRow.Species);
            weightExternal = Data.Parent.MassModels.GetExternalScatterplot(SpeciesRow.Species);
            WeightModel = Data.Parent.MassModels.GetCombinedScatterplot(SpeciesRow.Species);

            Data.Parent.MassGrowthModels.Refresh(SpeciesRow.Species);
            weightGrowthInternal = Data.Parent.MassGrowthModels.GetInternalScatterplot(SpeciesRow.Species);
            weightGrowthExternal = Data.Parent.MassGrowthModels.GetExternalScatterplot(SpeciesRow.Species);

            try
            {
                AgeStructure = Data.GetAgeCompositionFrame(SpeciesRow);
                AgeStructure.Name = Wild.Resources.Reports.Caption.Age;
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void structureCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageStart.SetNavigation(true);

            labelBasicInstruction.ResetFormatted(Swarm);

            buttonL.ResetFormatted(new SampleDisplay(Swarm.LengthSample).ToString("s"));
            buttonW.ResetFormatted(new SampleDisplay(Swarm.MassSample).ToString("s"));

            checkBoxAge.Enabled = 
                checkBoxReportAge.Checked = 
                AgeStructure != null;

            #region Age to Length

            statChartAL.Visible = GrowthModel != null;
            buttonAL.Enabled = GrowthModel != null && GrowthModel.IsRegressionOK;

            statChartAL.Clear();

            if (growthExternal != null)
            {
                growthExternal = growthExternal.Copy();
                growthExternal.Series.Name =
                    growthExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SpeciesRow.Species);
                growthExternal.Properties.DataPointColor = Constants.InfantColor;
                statChartAL.AddSeries(growthExternal);
            }

            if (growthInternal != null)
            {
                growthInternal = growthInternal.Copy();
                growthInternal.Series.Name =
                    growthInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SpeciesRow.Species);
                growthInternal.Properties.DataPointColor = UserSettings.ModelColor;
                statChartAL.AddSeries(growthInternal);
            }

            if (GrowthModel != null)
            {
                GrowthModel = GrowthModel.Copy();
                GrowthModel.Properties.ShowTrend = true;
                GrowthModel.Properties.SelectedApproximationType = Data.Parent.GrowthModels.Nature;
                GrowthModel.Properties.DataPointColor = System.Drawing.Color.Transparent;
                GrowthModel.Properties.TrendColor = UserSettings.ModelColor.Darker();
                statChartAL.AddSeries(GrowthModel);
            }

            //statChartAL.Refresh();
            statChartAL.Update(this, new EventArgs());
            if (statChartAL.Scatterplots.Count > 0) statChartAL.Rebuild(this, new EventArgs());

            #endregion

            #region Length to Mass

            statChartLW.Visible = WeightModel != null;
            buttonLW.Enabled = WeightModel != null && WeightModel.IsRegressionOK;

            statChartLW.Clear();

            if (weightExternal != null)
            {
                weightExternal = weightExternal.Copy();
                weightExternal.Series.Name =
                    weightExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SpeciesRow.Species);
                weightExternal.Properties.DataPointColor = Constants.InfantColor;
                statChartLW.AddSeries(weightExternal);
            }

            if (weightInternal != null)
            {
                weightInternal = weightInternal.Copy();
                weightInternal.Series.Name =
                    weightInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SpeciesRow.Species);
                weightInternal.Properties.DataPointColor = UserSettings.ModelColor;
                statChartLW.AddSeries(weightInternal);
            }

            if (WeightModel != null)
            {
                WeightModel = WeightModel.Copy();
                WeightModel.Properties.ShowTrend = true;
                WeightModel.Properties.SelectedApproximationType = Data.Parent.MassModels.Nature;
                WeightModel.Properties.DataPointColor = System.Drawing.Color.Transparent;
                WeightModel.Properties.TrendColor = UserSettings.ModelColor.Darker();
                statChartLW.AddSeries(WeightModel);
            }

            //statChartLW.Refresh();
            statChartLW.Update(this, new EventArgs());
            if (statChartLW.Scatterplots.Count > 0) statChartLW.Rebuild(this, new EventArgs());

            pageModelAL.SetNavigation(true);

            #endregion

            #region Age to Mass

            plotAW.Visible = (GrowthModel != null && WeightModel != null);

            plotAW.Clear();

            if (weightGrowthExternal != null)
            {
                weightGrowthExternal.Series.Name =
                    weightGrowthExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SpeciesRow.Species);
                weightGrowthExternal.Properties.DataPointColor = Constants.InfantColor;
                plotAW.AddSeries(weightGrowthExternal);
            }

            if (weightGrowthInternal != null)
            {
                weightGrowthInternal = weightGrowthInternal.Copy();
                weightGrowthInternal.Series.Name =
                    weightGrowthInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SpeciesRow.Species);
                weightGrowthInternal.Properties.DataPointColor = UserSettings.ModelColor;
                plotAW.AddSeries(weightGrowthInternal);
            }

            if (GrowthModel != null && GrowthModel.IsRegressionOK &&
                WeightModel != null && WeightModel.IsRegressionOK)
            {
                string name = string.Format(Resources.Interface.Interface.MassGrowth, SpeciesRow.Species);

                Functor weightGrowth = new Functor(name,
                    (t) =>
                    {
                        double l = GrowthModel.Regression.Predict(t);
                        return WeightModel.Regression.Predict(l);
                    },
                    (w) =>
                    {
                        double l = WeightModel.Regression.PredictInversed(w);
                        return GrowthModel.Regression.PredictInversed(l);
                    }
                        );

                weightGrowth.Properties.TrendColor = UserSettings.ModelColor.Darker();
                plotAW.AddSeries(weightGrowth);
            }

            //plotAW.Refresh();
            plotAW.Update(this, new EventArgs());
            if (plotAW.Scatterplots.Count > 0) plotAW.Rebuild(this, new EventArgs());

            pageModelAW.SetNavigation(true);

            #endregion
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            SampleProperties lengthForm = new SampleProperties(Swarm.LengthSample);
            lengthForm.SetFriendlyDesktopLocation(buttonL);
            lengthForm.ShowDialog();
        }

        private void buttonW_Click(object sender, EventArgs e)
        {
            SampleProperties massForm = new SampleProperties(Swarm.MassSample);
            massForm.SetFriendlyDesktopLocation(buttonW);
            massForm.ShowDialog();
        }

        private void pageBasic_Commit(object sender, WizardPageConfirmEventArgs e)
        {
        }



        private void pageLW_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (ModelsReturned != null)
            {
                ModelsReturned.Invoke(sender, e);
            }
        }

        private void buttonAL_Click(object sender, EventArgs e)
        {
            statChartAL.OpenRegressionProperties(GrowthModel);
        }

        private void buttonLW_Click(object sender, EventArgs e)
        {
            statChartLW.OpenRegressionProperties(WeightModel);
        }

        private void pageAW_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (ModelsCalculated != null)
            {
                ModelsCalculated.Invoke(sender, e);
                e.Cancel = true;
            }

            if (Visible)
            {
                if (gearWizard == null)
                {
                    gearWizard = new WizardGearSet(Data, SpeciesRow);
                    gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
                    gearWizard.Returned += gearWizard_Returned;
                }

                gearWizard.Replace(this);
            }
        }



        private void gearWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageModelAW);
            this.Replace(gearWizard);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageCpue);
            this.Replace(gearWizard);

            columnSelectivityNpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            columnSelectivityBpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            labelNpueUnit.ResetFormatted(gearWizard.SelectedUnit.Unit);
            labelBpueUnit.ResetFormatted(gearWizard.SelectedUnit.Unit);

            classesCalculator.RunWorkerAsync();
        }



        private void pageCpue_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                gearWizard.Replace(this);
            }
        }

        private void classesCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            classedComposition = gearWizard.SelectedStacks.GetClassedComposition(SpeciesRow, gearWizard.SelectedSamplerType, gearWizard.SelectedUnit);
            Swarm.Abundance = classedComposition.TotalAbundance / classedComposition.Count;
            Swarm.Biomass = classedComposition.TotalBiomass / classedComposition.Count;
        }

        private void classesCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSelectivity.Rows.Clear();

            foreach (SpeciesSwarm species in classedComposition)
            {
                DataGridViewRow gridRow = new DataGridViewRow
                {
                    Height = spreadSheetSelectivity.RowTemplate.Height
                };
                gridRow.CreateCells(spreadSheetSelectivity);

                gridRow.Cells[columnSelectivityClass.Index].Value = species;
                gridRow.Cells[columnSelectivityL.Index].Value = new SampleDisplay(species.LengthSample);
                gridRow.Cells[columnSelectivityW.Index].Value = new SampleDisplay(species.MassSample);
                gridRow.Cells[columnSelectivityN.Index].Value = species.Quantity;
                gridRow.Cells[columnSelectivityNpue.Index].Value = species.Abundance;
                gridRow.Cells[columnSelectivityB.Index].Value = species.Mass;
                gridRow.Cells[columnSelectivityBpue.Index].Value = species.Biomass;
                gridRow.Cells[columnSelectivitySex.Index].Value = species.Sexes;

                spreadSheetSelectivity.Rows.Add(gridRow);
            }

            textBoxNpue.ResetFormatted(Swarm.Abundance);
            textBoxBpue.ResetFormatted(Swarm.Biomass);
        }

        private void pageCpue_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                if (checkBoxLength.Checked)
                {
                    if (lengthCompositionWizard == null)
                    {
                        lengthCompositionWizard = new WizardComposition(Data, LengthStructure, SpeciesRow, CompositionColumn.MassSample);
                        lengthCompositionWizard.Returned += lengthCompositionWizard_Returned;
                        lengthCompositionWizard.Finished += lengthCompositionWizard_Finished;
                    }

                    lengthCompositionWizard.Replace(this);
                    lengthCompositionWizard.Run(gearWizard);
                }
                //else
                //{
                //    wizardExplorer.EnsureSelected(pageAge);
                //}
            }
        }



        private void lengthCompositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageCpue);
            this.Replace(lengthCompositionWizard);
        }

        private void lengthCompositionWizard_Finished(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageLength);
            this.Replace(lengthCompositionWizard);

            double minx = double.MaxValue;
            double maxx = 0;
            double maxy = 0;

            foreach (SizeClass sizeClass in lengthCompositionWizard.CatchesComposition)
            {
                double af = sizeClass.AbundanceFraction;
                double bf = sizeClass.BiomassFraction;
                minx = Math.Min(minx, sizeClass.Size.LeftEndpoint);
                maxx = Math.Max(maxx, sizeClass.Size.RightEndpoint);
                maxy = Math.Max(maxy, Math.Max(af, bf));
            }

            plotLength.AxisXMin = minx;
            plotLength.AxisXMax = maxx;
            plotLength.AxisYMin = 0.0;
            plotLength.AxisYMax = maxy;

            plotLength.Remaster();

            comboBoxLengthSource.SelectedIndex = 0;
            comboBoxLengthSource_SelectedIndexChanged(sender, e);




            plotSelectionSource.Clear();

            plotSelectionSource.Series.Clear();

            plotSelectionSource.AxisXAutoInterval = false;
            plotSelectionSource.AxisXInterval = LengthStructure.Interval;
            plotSelectionSource.AxisXAutoMinimum = false;
            plotSelectionSource.AxisXMin = LengthStructure.Minimum;
            plotSelectionSource.AxisXAutoMaximum = false;
            plotSelectionSource.AxisXMax = LengthStructure.Ceiling;

            plotSelectionSource.AxisYAutoMinimum = false;
            plotSelectionSource.AxisYAutoMaximum = false;
            plotSelectionSource.AxisYMin = 0.0;

            double max = 0;

            foreach (Composition comp in lengthCompositionWizard.CatchesComposition.SeparateCompositions)
            {
                //spreadSheetComposition.InsertColumn(comp.Name, comp.Name,
                //    typeof(double), spreadSheetComposition.ColumnCount, 75, "N3").ReadOnly = true;
                //comp.UpdateValues(spreadSheetComposition, ValueVariant.Abundance);

                if (comp is LengthComposition)
                {
                    Series catches = new Series
                    {
                        Name = comp.Name,
                        ChartType = SeriesChartType.Column
                    };
                    foreach (SizeClass size in comp)
                    {
                        catches.Points.AddXY(size.Size.Midpoint, size.Abundance);
                        max = Math.Max(max, size.Abundance);
                    }

                    plotSelectionSource.Series.Add(catches);
                }
            }

            plotSelectionSource.AxisYMax = max;
            plotSelectionSource.Remaster();

            pageSelectionSource.SetNavigation(false);
            selectionCalculator.RunWorkerAsync();
        }

        private void pageLength_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                lengthCompositionWizard.Replace(this);
            }
        }

        private void comboBoxLengthSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            plotLength.Series.Clear();
            Series all = new Series() { ChartType = SeriesChartType.Column };
            plotLength.AxisY2Title = "";
            foreach (SizeClass sizeClass in lengthCompositionWizard.CatchesComposition)
            {
                all.Points.AddXY(sizeClass.Size.Midpoint, comboBoxLengthSource.SelectedIndex == 0 ? sizeClass.AbundanceFraction : sizeClass.BiomassFraction);
            }
            plotLength.Series.Add(all);
        }

        private void pageLength_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (!checkBoxLengthAdjusted.Checked)
            {
                pageLengthAdjusted_Commit(sender, e);
            }
        }

        private void pageSelectionSource_Initialize(object sender, WizardPageInitEventArgs e)
        {
            //if (!checkBoxLengthAdjust.Checked)
            //{
            //    while (wizardExplorer.SelectedPage != pageAge)
            //    {
            //        wizardExplorer.NextPage();
            //    }
            //}
        }


        private void selectionCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            if (gearWizard.SelectedSamplerType.IsPassive())
            {
                List<LengthComposition> catches = new List<LengthComposition>();
                List<double> meshes = new List<double>();

                for (int i = 0; i < lengthCompositionWizard.CatchesComposition.Dimension; i++)
                {
                    Composition comp = lengthCompositionWizard.CatchesComposition.GetComposition(i);

                    if (comp is LengthComposition composition)
                    {
                        catches.Add(composition);
                        meshes.Add(gearWizard.SelectedStacks[i].Mesh());
                    }
                }

                try
                {
                    SelectivityModel = new GillnetSelectivityModel(catches, meshes);

                    for (int i = 0; i < LengthStructure.Count; i++)
                    {
                        double a = lengthCompositionWizard.CatchesComposition[i].Abundance;
                        double s = SelectivityModel.GetSelection(LengthStructure[i].Size.Midpoint);
                        double w = (lengthCompositionWizard.CatchesComposition[i].Quantity > 0) ?
                            (lengthCompositionWizard.CatchesComposition[i].Abundance / lengthCompositionWizard.CatchesComposition[i].Biomass) : 0.0;
                        LengthStructure[i].Abundance = (s > .01) ? (a / s) : a;
                        LengthStructure[i].Biomass = w * LengthStructure[i].Abundance;
                    }
                }
                catch { }
            }

        }

        private void selectionCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelSelectionSourceWarn.Visible = pictureBoxSelectionSourceWarn.Visible = (SelectivityModel == null);

            if (SelectivityModel != null)
            {
                plotSelection.AxisXMin = LengthStructure.Minimum;
                plotSelection.AxisXMax = LengthStructure.Ceiling;
                plotSelection.AxisXInterval = LengthStructure.Interval;

                plotSelection.Remaster();

                textBoxSF.Text = SelectivityModel.SelectionFactor.ToString("N3");
                textBoxSD.Text = SelectivityModel.StandardDeviation.ToString("N3");

                plotSelection.Clear();

                Functor ogive = new Functor(Resources.Interface.Interface.SelectivityOgive, SelectivityModel.GetSelection);
                plotSelection.AddSeries(ogive);
                ogive.Series.ChartType = SeriesChartType.Area;
                ogive.Series.BackHatchStyle = ChartHatchStyle.Percent90;


                for (int i = 0; i < SelectivityModel.Dimension; i++)
                {
                    int factor = i;
                    Functor sel = new Functor(SelectivityModel.Catch(factor).Name,
                        (l) => { return SelectivityModel.GetSelection(factor, l); });
                    plotSelection.AddSeries(sel);
                }

                plotSelection.Remaster();


                plotLengthAdjusted.Series.Clear();

                plotLengthAdjusted.AxisXInterval = LengthStructure.Interval;
                plotLengthAdjusted.AxisXMin = plotSelection.AxisXMin;
                plotLengthAdjusted.AxisXMax = plotSelection.AxisXMax;

                double max = 0;

                Series catches = new Series
                {
                    Name = Resources.Interface.Interface.Catches,
                    ChartType = SeriesChartType.Column
                };
                foreach (SizeClass size in lengthCompositionWizard.CatchesComposition)
                {
                    catches.Points.AddXY(size.Size.Midpoint, size.AbundanceFraction);
                    max = Math.Max(max, size.AbundanceFraction);
                }

                Series pop = new Series
                {
                    Name = Resources.Interface.Interface.CatchesCorrected,
                    ChartType = SeriesChartType.Column
                };
                foreach (SizeClass size in LengthStructure)
                {
                    pop.Points.AddXY(size.Size.Midpoint, size.AbundanceFraction);
                    max = Math.Max(max, size.AbundanceFraction);
                }

                plotLengthAdjusted.AxisYMax = max;
                plotLengthAdjusted.RecalculateAxesProperties();
                plotLengthAdjusted.RefreshAxes();
                plotLengthAdjusted.Series.Add(catches);
                plotLengthAdjusted.Series.Add(pop);

                plotLengthAdjusted.Remaster();
                plotLengthAdjusted.Remaster();
            }

            pageSelectionSource.SetNavigation(true);
            pageLengthAdjusted.Suppress = 
                pageSelection.Suppress =
                SelectivityModel == null;
        }

        private void pageSelectionSource_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (pageLengthAdjusted.Suppress)
            {
                pageLengthAdjusted_Commit(sender, e);
            }
        }

        private void pageLengthAdjusted_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                if (checkBoxAge.Checked)
                {
                    if (ageCompositionWizard == null)
                    {
                        ageCompositionWizard = new WizardComposition(Data, AgeStructure, SpeciesRow, CompositionColumn.MassSample | CompositionColumn.LengthSample);
                        ageCompositionWizard.Returned += ageCompositionWizard_Returned;
                        ageCompositionWizard.Finished += ageCompositionWizard_Finished;
                    }

                    ageCompositionWizard.Replace(this);
                    ageCompositionWizard.Run(gearWizard);
                }
                //else
                //{
                //    wizardExplorer.EnsureSelected(pageReport);
                //}
            }
        }



        private void ageCompositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(checkBoxLengthAdjusted.Checked ? (pageLengthAdjusted.Suppress ? pageSelectionSource : pageLengthAdjusted) : pageLength);
            this.Replace(ageCompositionWizard);
        }

        private void ageCompositionWizard_Finished(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageAge);
            this.Replace(ageCompositionWizard);

            checkBoxAge_CheckedChanged(sender, e);

            double minx = double.MaxValue;
            double maxx = 0;
            double maxy = 0;

            foreach (AgeGroup ageGroup in ageCompositionWizard.CatchesComposition)
            {
                double af = ageGroup.AbundanceFraction;
                double bf = ageGroup.BiomassFraction;
                minx = Math.Min(minx, ageGroup.Age.Years);
                maxx = Math.Max(maxx, ageGroup.Age.Years);
                maxy = Math.Max(maxy, Math.Max(af, bf));
            }

            plotAge.AxisXMin = minx;
            plotAge.AxisXMax = maxx;
            plotAge.AxisYMin = 0.0;
            plotAge.AxisYMax = maxy;

            plotAge.Remaster();

            comboBoxAgeSource.SelectedIndex = 0;
            comboBoxAgeSource_SelectedIndexChanged(sender, e);

            comboBoxMortalityAge.DataSource = ageCompositionWizard.CatchesComposition;
            comboBoxMortalityAge.SelectedItem = ageCompositionWizard.CatchesComposition.MostAbundant;
        }

        private void pageAge_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                ageCompositionWizard.Replace(this);
            }
        }

        private void comboBoxAgeSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            plotAge.Series.Clear();

            Series all = new Series() { ChartType = SeriesChartType.Column };
            //Series all = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.Gainsboro };
            //Series juv = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.LawnGreen };
            //Series mal = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.DodgerBlue };
            //Series fem = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.Coral };

            plotAge.AxisY2Title = "";

            foreach (AgeGroup ageGroup in ageCompositionWizard.CatchesComposition)
            {
                double total = comboBoxAgeSource.SelectedIndex == 0 ? ageGroup.AbundanceFraction : ageGroup.BiomassFraction;

                //if (total == 0) continue;

                //if (ageGroup.Sexes[0].Quantity > 0) { juv.Points.AddXY(ageGroup.Age.Years + .5, comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes[0].Abundance : ageGroup.Sexes[0].Biomass); }
                //if (ageGroup.Sexes[1].Quantity > 0) { mal.Points.AddXY(ageGroup.Age.Years + .5, comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes[1].Abundance: ageGroup.Sexes[1].Biomass); }
                //if (ageGroup.Sexes[2].Quantity > 0) { fem.Points.AddXY(ageGroup.Age.Years + .5, comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes[2].Abundance: ageGroup.Sexes[2].Biomass); }
                //all.Points.AddXY(ageGroup.Age.Years + .5, total - (comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes.TotalAbundance: ageGroup.Sexes.TotalBiomass));
                all.Points.AddXY(ageGroup.Age.Years + .5, total);
            }

            plotAge.Series.Add(all);
            //plotT.Series.Add(juv);
            //plotT.Series.Add(mal);
            //plotT.Series.Add(fem);
        }

        private void pageAge_Commit(object sender, WizardPageConfirmEventArgs e)
        {        }



        private void pageMortality_Initialize(object sender, WizardPageInitEventArgs e)
        {
            // fires at backward move too!!!
            //if (!checkBoxAgeAdjusted.Checked)
            //{
            //    while (wizardExplorer.SelectedPage != pageReport)
            //    {
            //        wizardExplorer.NextPage();
            //    }
            //}
        }

        private void pageMortality_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (MortalityReturned != null)
            {
                MortalityReturned.Invoke(sender, e);
            }
        }

        private void comboBoxMortalityAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            Scatterplot[] res = ageCompositionWizard.CatchesComposition.GetCatchCurve(comboBoxMortalityAge.SelectedIndex);

            unused = res[0];
            CatchCurve = res[1];

            plotMortality.Clear();

            if (unused != null)
            {
                //unused.Properties.DataPointColor = Color.LightCoral;
                plotMortality.AddSeries(unused);
            }

            if (CatchCurve != null)
            {
                //Model.Properties.DataPointColor = Color.DarkRed;
                CatchCurve.Properties.ShowTrend = true;
                CatchCurve.Properties.SelectedApproximationType = TrendType.Exponential;
                plotMortality.AddSeries(CatchCurve);
            }

            plotMortality.Visible = unused != null && CatchCurve != null;

            plotMortality.ShowLegend = false;
            plotMortality.Remaster();

            if (CatchCurve.IsRegressionOK)
            {
                Z = -CatchCurve.Regression.Parameter(1);
                S = Math.Exp(-Z);
                Fi = 1 - S;

                textBoxZ.Text = Z.ToString("N4");
                textBoxFi.Text = Fi.ToString("N4");
                textBoxS.Text = S.ToString("N4");
            } else {
                textBoxZ.Text = 
                    textBoxFi.Text = 
                    textBoxS.Text = Constants.Null;
            }
        }

        private void buttonMortality_Click(object sender, EventArgs e)
        {
            plotMortality.OpenRegressionProperties(CatchCurve);
        }

        private void pageMortality_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (ModelsCalculated != null) {
                ModelsCalculated.Invoke(sender, e);
            }
        }



        private void pageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        {        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void checkBoxReportLength_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReportLengthAdjusted.Enabled = 
                checkBoxLengthAdjusted.Checked && checkBoxReportLength.Checked;
        }

        private void checkBoxReportAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReportAgeAdjusted.Enabled = 
                checkBoxAgeAdjusted.Checked && checkBoxReportAge.Checked;

            checkBoxReportAgeCPUE.Enabled = 
                checkBoxReportAgeKeys.Enabled = 
                checkBoxReportAge.Checked && (gearWizard == null || gearWizard.IsMultipleClasses);

        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetReport();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();

            pageReport.SetNavigation(true);

            Log.Write(EventType.WizardEnded, "Stock composition wizard is finished for {0}.", SpeciesRow.Species);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            if ((wizardExplorer.SelectedPage == pageModelLW |
                wizardExplorer.SelectedPage == pageModelAL |
                wizardExplorer.SelectedPage == pageModelAW) &&
                ModelsCalculated != null)
            {
                ModelsCalculated.Invoke(sender, e);
            }

            if ((wizardExplorer.SelectedPage == pageAgeAdjusted |
                wizardExplorer.SelectedPage == pageMortality) &&
                MortalityCalculated != null)
            {
                MortalityCalculated.Invoke(sender, e);
            }

            Close();
        }
    }
}
