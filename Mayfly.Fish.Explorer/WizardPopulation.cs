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
        private WizardComposition ageCompositionWizard;

        private WizardComposition lengthCompositionWizard;

        private WizardGearSet gearWizard;

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

        public event EventHandler Returned;

        public event EventHandler Calculated;



        private WizardPopulation()
        {
            InitializeComponent();

            this.RestoreAllCheckStates();
        }

        public WizardPopulation(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            labelBasic.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            pageStart.SetNavigation(false);

            statChartAL.Clear();
            statChartLW.Clear();
            buttonAL.Enabled = buttonLW.Enabled = false;

            Log.Write(EventType.WizardStarted, "Stock composition wizard is started for {0}.",
                speciesRow.Species);

            structureCalculator.RunWorkerAsync();
        }


        public void Run()
        {
            pageStart.Suppress = true;
            pageBasic.Suppress = true;
            wizardExplorer.NextPage();
        }

        public double GetWeight(Age age)
        {
            return 2;
        }


        public Report GetReport()
        {
            Report report = new Report(string.Format(Resources.Reports.Header.SummaryPopulation, SpeciesRow.KeyRecord.FullNameReport));

            Data.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            if (checkBoxAge.Checked)
            {
                ageCompositionWizard.AppendPopulationSectionTo(report);
            }

            if (checkBoxAppT.Checked | checkBoxAppKeys.Checked)
            {
                report.BreakPage(PageBreakOption.Odd);
                report.AddChapterTitle(Resources.Reports.Chapter.Appendices);
            }

            if (checkBoxAppT.Checked)
            {
                ageCompositionWizard.AppendCalculationSectionTo(report);
            }

            if (checkBoxAppKeys.Checked)
            {
                ageCompositionWizard.AddAgeRecoveryRoutines(report);
            }

            report.EndBranded();

            return report;
        }

        public void SetGearSet(WizardGearSet _gearWizard)
        {
            gearWizard = _gearWizard;
        }

        private Report GetSelectivityReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Sections.Selectivity.Title, 
                    gearWizard.SelectedSamplerType.ToDisplay(), SpeciesRow.KeyRecord.FullNameReport));
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

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

            return report;
        }

        public void AddData(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph1,
                SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

            Report.Table table = AgeStructure.GetTable(
                CompositionColumn.Quantity | CompositionColumn.LengthSample | CompositionColumn.MassSample, 
                string.Format(Resources.Reports.Sections.Growth.Table1, SpeciesRow.KeyRecord.FullNameReport), 
                AgeStructure.Name);

            report.AddTable(table);
        }

        public void AddGrowth(Report report)
        {
            if (GrowthModel.IsRegressionOK) {
                report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph2, GrowthModel.Regression);
                report.AddEquation(GrowthModel.Regression.GetEquation("L", "t"));
            }
        }

        public void AddMass(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph3, WeightModel.Regression);
            report.AddEquation(WeightModel.Regression.GetEquation("W", "L"));
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
            //pageBasic.SetNavigation(AgeStructure != null);

            labelBasic.ResetFormatted(Swarm);

            buttonL.ResetFormatted(new SampleDisplay(Swarm.LengthSample).ToString("s"));
            buttonW.ResetFormatted(new SampleDisplay(Swarm.MassSample).ToString("s"));

            pageAge.Suppress = AgeStructure == null;
            checkBoxAge.Checked = AgeStructure != null;


            #region Age to Length

            statChartAL.Visible = GrowthModel != null;
            buttonAL.Enabled = checkBoxAL.Enabled = GrowthModel != null && GrowthModel.IsRegressionOK;

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
            buttonLW.Enabled = checkBoxLW.Enabled = WeightModel != null && WeightModel.IsRegressionOK;

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

            pageAL.SetNavigation(true);

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

            pageAW.SetNavigation(true);

            #endregion
        }

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
        }

        private void PageBasic_Commit(object sender, WizardPageConfirmEventArgs e)
        {
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

        private void buttonAL_Click(object sender, EventArgs e)
        {
            statChartAL.OpenRegressionProperties(GrowthModel);
        }

        private void buttonLW_Click(object sender, EventArgs e)
        {
            statChartLW.OpenRegressionProperties(WeightModel);
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageBasic);
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

        private void PageCpue_Rollback(object sender, WizardPageConfirmEventArgs e)
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
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.Height = spreadSheetSelectivity.RowTemplate.Height;
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

        private void PageCpue_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
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

            plotC.Clear();

            plotC.Series.Clear();

            plotC.AxisXAutoInterval = false;
            plotC.AxisXInterval = LengthStructure.Interval;
            plotC.AxisXAutoMinimum = false;
            plotC.AxisXMin = LengthStructure.Minimum;
            plotC.AxisXAutoMaximum = false;
            plotC.AxisXMax = LengthStructure.Ceiling;

            plotC.AxisYAutoMinimum = false;
            plotC.AxisYAutoMaximum = false;
            plotC.AxisYMin = 0.0;

            double max = 0;

            foreach (Composition comp in lengthCompositionWizard.CatchesComposition.SeparateCompositions)
            {
                //spreadSheetComposition.InsertColumn(comp.Name, comp.Name,
                //    typeof(double), spreadSheetComposition.ColumnCount, 75, "N3").ReadOnly = true;
                //comp.UpdateValues(spreadSheetComposition, ValueVariant.Abundance);

                if (comp is LengthComposition)
                {
                    Series catches = new Series();
                    catches.Name = comp.Name;
                    catches.ChartType = SeriesChartType.Column;
                    foreach (SizeClass size in comp)
                    {
                        catches.Points.AddXY(size.Size.Midpoint, size.Abundance);
                        max = Math.Max(max, size.Abundance);
                    }

                    plotC.Series.Add(catches);
                }
            }

            plotC.AxisYMax = max;
            plotC.Remaster();

            pageLength.AllowNext = false;
            selectionCalculator.RunWorkerAsync();
        }

        private void PageLength_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                lengthCompositionWizard.Replace(this);
            }
        }

        private void PageLength_Commit(object sender, WizardPageConfirmEventArgs e)
        {

        }

        private void SelectionCalculator_DoWork(object sender, DoWorkEventArgs e)
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

        private void SelectionCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageLength.AllowNext = SelectivityModel != null;
            labelWarn.Visible = pictureBoxWarn.Visible = (SelectivityModel == null);

            if (SelectivityModel != null)
            {
                plotS.AxisXMin = LengthStructure.Minimum;
                plotS.AxisXMax = LengthStructure.Ceiling;
                plotS.AxisXInterval = LengthStructure.Interval;

                plotS.Remaster();

                textBoxSF.Text = SelectivityModel.SelectionFactor.ToString("N3");
                textBoxSD.Text = SelectivityModel.StandardDeviation.ToString("N3");

                plotS.Clear();

                Functor ogive = new Functor(Resources.Interface.Interface.SelectivityOgive, SelectivityModel.GetSelection);
                plotS.AddSeries(ogive);
                ogive.Series.ChartType = SeriesChartType.Area;
                ogive.Series.BackHatchStyle = ChartHatchStyle.Percent90;


                for (int i = 0; i < SelectivityModel.Dimension; i++)
                {
                    int factor = i;
                    Functor sel = new Functor(SelectivityModel.Catch(factor).Name,
                        (l) => { return SelectivityModel.GetSelection(factor, l); });
                    plotS.AddSeries(sel);
                }

                plotS.Remaster();


                plotP.Series.Clear();

                plotP.AxisXInterval = LengthStructure.Interval;
                plotP.AxisXMin = plotS.AxisXMin;
                plotP.AxisXMax = plotS.AxisXMax;

                double max = 0;

                Series catches = new Series();
                catches.Name = Resources.Interface.Interface.Catches;
                catches.ChartType = SeriesChartType.Column;
                foreach (SizeClass size in lengthCompositionWizard.CatchesComposition)
                {
                    catches.Points.AddXY(size.Size.Midpoint, size.AbundanceFraction);
                    max = Math.Max(max, size.AbundanceFraction);
                }

                Series pop = new Series();
                pop.Name = Resources.Interface.Interface.CatchesCorrected;
                pop.ChartType = SeriesChartType.Column;
                foreach (SizeClass size in LengthStructure)
                {
                    pop.Points.AddXY(size.Size.Midpoint, size.AbundanceFraction);
                    max = Math.Max(max, size.AbundanceFraction);
                }

                plotP.AxisYMax = max;
                plotP.RecalculateAxesProperties();
                plotP.RefreshAxes();
                plotP.Series.Add(catches);
                plotP.Series.Add(pop);

                plotP.Remaster();
                plotP.Remaster();
            }
        }


        private void PageSelection_Commit(object sender, WizardPageConfirmEventArgs e)
        {
        }


        private void pageLengthPopulation_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                if (AgeStructure == null) return;

                if (ageCompositionWizard == null)
                {
                    ageCompositionWizard = new WizardComposition(Data, AgeStructure, SpeciesRow, CompositionColumn.MassSample | CompositionColumn.LengthSample);
                    ageCompositionWizard.Returned += AgeCompositionWizard_Returned;
                    ageCompositionWizard.Finished += ageCompositionWizard_Finished;
                }

                ageCompositionWizard.Replace(this);
                ageCompositionWizard.Run(gearWizard);
            }
        }

        private void AgeCompositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageLengthPopulation);
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

            plotT.AxisXMin = minx;
            plotT.AxisXMax = maxx;
            plotT.AxisYMin = 0.0;
            plotT.AxisYMax = maxy;

            plotT.Remaster();

            comboBoxParameter.SelectedIndex = 1;
            //comboBoxParameter_SelectedIndexChanged(sender, e);
        }

        private void comboBoxParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            plotT.Series.Clear();

            Series all = new Series() { ChartType = SeriesChartType.Column };
            //Series all = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.Gainsboro };
            //Series juv = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.LawnGreen };
            //Series mal = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.DodgerBlue };
            //Series fem = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.Coral };

            plotT.AxisY2Title = "";

            foreach (AgeGroup ageGroup in ageCompositionWizard.CatchesComposition)
            {
                double total = comboBoxParameter.SelectedIndex == 0 ? ageGroup.AbundanceFraction : ageGroup.BiomassFraction;

                //if (total == 0) continue;

                //if (ageGroup.Sexes[0].Quantity > 0) { juv.Points.AddXY(ageGroup.Age.Years + .5, comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes[0].Abundance : ageGroup.Sexes[0].Biomass); }
                //if (ageGroup.Sexes[1].Quantity > 0) { mal.Points.AddXY(ageGroup.Age.Years + .5, comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes[1].Abundance: ageGroup.Sexes[1].Biomass); }
                //if (ageGroup.Sexes[2].Quantity > 0) { fem.Points.AddXY(ageGroup.Age.Years + .5, comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes[2].Abundance: ageGroup.Sexes[2].Biomass); }
                //all.Points.AddXY(ageGroup.Age.Years + .5, total - (comboBoxParameter.SelectedIndex == 0 ? ageGroup.Sexes.TotalAbundance: ageGroup.Sexes.TotalBiomass));
                all.Points.AddXY(ageGroup.Age.Years + .5, total);
            }

            plotT.Series.Add(all);
            //plotT.Series.Add(juv);
            //plotT.Series.Add(mal);
            //plotT.Series.Add(fem);
        }

        private void pageAge_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                ageCompositionWizard.Replace(this);
            }
        }

        private void PageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            //if (AgeStructure != null)
            //{
            //if (this.Visible) ageCompositionWizard.Replace(this);
            //}
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void checkBoxAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppT.Enabled = checkBoxAge.Checked && (gearWizard == null || gearWizard.IsMultipleClasses);
            checkBoxAppKeys.Enabled = checkBoxAge.Checked && (gearWizard == null || gearWizard.IsMultipleClasses);
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
            if ((wizardExplorer.SelectedPage == pageLW |
                wizardExplorer.SelectedPage == pageAL |
                wizardExplorer.SelectedPage == pageAW) &&
                Calculated != null)
            {
                Calculated.Invoke(sender, e);
            }

            Close();
        }

        private void ButtonL_Click(object sender, EventArgs e)
        {
            SampleProperties lengthForm = new SampleProperties(Swarm.LengthSample);
            lengthForm.SetFriendlyDesktopLocation(buttonL);
            lengthForm.ShowDialog();
        }

        private void ButtonW_Click(object sender, EventArgs e)
        {
            SampleProperties massForm = new SampleProperties(Swarm.MassSample);
            massForm.SetFriendlyDesktopLocation(buttonW);
            massForm.ShowDialog();
        }

        private void PageAW_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Calculated != null)
            {
                Calculated.Invoke(sender, e);
                e.Cancel = true;
            }
        }

        private void PageLW_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Returned != null)
            {
                Returned.Invoke(sender, e);
            }
        }
    }
}
