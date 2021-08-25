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
using System.Drawing;

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

        public LengthComposition LengthStructure { set; get; }

        public GillnetSelectivityModel SelectivityModel { set; get; }

        public AgeComposition AgeStructure { set; get; }

        public ExponentialMortalityModel TotalMortalityModel { set; get; }

        public ContinuousBio GrowthModel { get; private set; }

        public ContinuousBio WeightModel { get; private set; }

        public event EventHandler ModelsReturned;

        public event EventHandler ModelsCalculated;

        public event EventHandler MortalityReturned;

        public event EventHandler MortalityCalculated;



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

            plotLW.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            plotAL.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            plotAW.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            plotLengthAdjusted.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            plotAgeAdjusted.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            pageStart.SetNavigation(false);

            plotAL.Clear();
            plotLW.Clear();
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

            if (!GrowthModel.CombinedData.IsRegressionOK)
                throw new ArgumentNullException();

            if (WeightModel == null)
                throw new ArgumentNullException();

            if (!WeightModel.CombinedData.IsRegressionOK)
                throw new ArgumentNullException();

            double l = GrowthModel.CombinedData.Regression.Predict(age.Years + .5);
            double w = WeightModel.CombinedData.Regression.Predict(l);
            return w;
        }


        public Report GetReport()
        {
            Report report = new Report(string.Format(Resources.Reports.Header.SummaryPopulation, SpeciesRow.KeyRecord.FullNameReport));

            Data.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxReportBasic.Checked)
            {
                if (checkBoxReportGears.Checked)
                {
                    gearWizard.AddEffortSection(report);
                }

                AppendBasicSectionTo(report);
            }

            if (checkBoxReportLength.Checked)
            {
                lengthCompositionWizard.AppendCategorialCatchesSectionTo(report);

                if (checkBoxReportLengthAdjusted.Checked)
                {
                    AddSelectivity(report);
                }
                else
                {
                    report.AddParagraph(Resources.Reports.Sections.Population.Paragraph1_1, report.NextFigureNumber);
                    report.AddImage(plotLength.GetVector(17, 7), plotLength.Text);
                }
            }

            if (checkBoxReportAge.Checked)
            {
                ageCompositionWizard.AppendCategorialCatchesSectionTo(report);

                if (checkBoxReportAgeAdjusted.Checked)
                {
                    AddMortality(report);
                }
                else
                {
                    report.AddParagraph(Resources.Reports.Sections.Population.Paragraph1_1, report.NextFigureNumber);
                    report.AddImage(plotAge.GetVector(17, 7), plotAge.Text);
                }
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

            report.EndBranded();

            return report;
        }

        public void AppendBasicSectionTo(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Growth.Title, SpeciesRow.KeyRecord.FullNameReport);

            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph1,
                SpeciesRow.KeyRecord.FullNameReport,
                Swarm.LengthSample.Minimum, Swarm.LengthSample.Maximum, Swarm.LengthSample.Mean,
                Swarm.MassSample.Minimum, Swarm.MassSample.Maximum, Swarm.MassSample.Mean);


            // LW model
            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph2, report.NextFigureNumber);
            report.AddEquation(WeightModel.CombinedData.Regression.GetEquation("W", "L"));
            report.AddImage(plotLW.GetVector(17, 10), plotLW.Text);

            if (GrowthModel.CombinedData.IsRegressionOK)
            {

                // AL model
                report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph3,
                    report.NextFigureNumber);
                report.AddEquation(GrowthModel.CombinedData.Regression.GetEquation("L", "t"));
                report.AddImage(plotAL.GetVector(17, 10), plotAL.Text);

                // AW model
                report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph4,
                    report.NextFigureNumber);
                report.AddImage(plotAW.GetVector(17, 10), plotAW.Text);
            }
            else
            {
                report.AddParagraphClass("warning", Resources.Reports.Sections.Growth.Paragraph5);
            }

            // CPUE
            report.AddParagraph(Resources.Reports.Sections.Growth.Paragraph6,
                SpeciesRow.KeyRecord.ShortName, Swarm.Abundance, gearWizard.SelectedUnit.Unit,
                Swarm.Biomass, gearWizard.SelectedSamplerType.ToDisplay(), report.NextTableNumber);
            Report.Table table = classedComposition.GetStandardCatchesTable(
                string.Format(Resources.Reports.Sections.Growth.Table,
                SpeciesRow.KeyRecord.ShortName, gearWizard.SelectedSamplerType.ToDisplay()),
                Resources.Reports.Caption.GearClass);
            report.AddTable(table);
        }

        public void AddSelectivity(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Selectivity.Title,
                gearWizard.SelectedSamplerType.ToDisplay());

            report.AddParagraph(Resources.Reports.Sections.Selectivity.Paragraph1,
                SpeciesRow.KeyRecord.ShortName, report.NextFigureNumber);
            report.AddImage(plotSelectionSource.GetVector(17, 7), plotSelectionSource.Text);

            if (SelectivityModel == null)
            {
                report.AddParagraphClass("warning", Resources.Reports.Sections.Selectivity.Paragraph6);
            }
            else
            {
                report.AddParagraph(Resources.Reports.Sections.Selectivity.Paragraph3, report.NextFigureNumber, report.NextFigureNumber);
                report.AddImage(plotSelection.GetVector(17, 7), plotSelection.Text);
                report.AddTable(SelectivityModel.GetReportTable(
                    string.Format(Resources.Reports.Sections.Selectivity.Table3, SpeciesRow.KeyRecord.FullNameReport, gearWizard.SelectedSamplerType.ToDisplay())));

                report.AddParagraph(Resources.Reports.Sections.Selectivity.Paragraph5, report.NextFigureNumber);
                report.AddImage(plotLengthAdjusted.GetVector(17, 7), plotLengthAdjusted.Text);
            }
        }

        public void AddMortality(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Mortality.Title, SpeciesRow.KeyRecord.FullNameReport);

            report.AddParagraph(Resources.Reports.Sections.Mortality.Paragraph1,
                TotalMortalityModel.YoungestCaught.Age, TotalMortalityModel.OldestCaught.Age,
                SpeciesRow.KeyRecord.ShortName);
            report.AddEquation(TotalMortalityModel.Exploited.Regression.GetEquation("NPUE", "t"));
            report.AddImage(plotMortality.GetVector(17, 7), plotMortality.Text);

            report.AddParagraph(Resources.Reports.Sections.Mortality.Paragraph2, TotalMortalityModel.Z, report.NextFigureNumber);
            report.AddEquation(@"S = e^{-" + TotalMortalityModel.Z.ToString("N5") + "} = " + TotalMortalityModel.S.ToString("N5"));
            report.AddEquation(@"φ = 1 - " + TotalMortalityModel.S.ToString("N5") + " = " + TotalMortalityModel.Fi.ToString("N5"));

            report.AddParagraph(Resources.Reports.Sections.Mortality.Paragraph3,
                SpeciesRow.KeyRecord.ShortName, report.NextTableNumber, report.NextFigureNumber);

            report.AddTable(
                new Composition[] { ageCompositionWizard.CatchesComposition, AgeStructure }.GetTable(
                    CompositionColumn.Abundance | CompositionColumn.AbundanceFraction | CompositionColumn.Biomass | CompositionColumn.BiomassFraction,
                plotAgeAdjusted.Text, Wild.Resources.Reports.Caption.AgeUnit, string.Empty));
            report.AddImage(plotAgeAdjusted.GetVector(17, 7), plotAgeAdjusted.Text);
        }



        private void checkBoxLength_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxLengthAdjusted.Enabled =
                checkBoxLength.Checked;
        }

        private void checkBoxAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAgeAdjusted.Enabled =
                checkBoxAge.Checked;
        }

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

            GrowthModel = Data.Parent.FindGrowthModel(SpeciesRow.Species);
            WeightModel = Data.Parent.FindMassModel(SpeciesRow.Species);

            LengthStructure = Data.GetLengthCompositionFrame(SpeciesRow, UserSettings.SizeInterval);
            LengthStructure.Name = Fish.Resources.Common.SizeUnits;

            try
            {
                AgeStructure = Data.GetAgeCompositionFrame(SpeciesRow);
                AgeStructure.Name = Wild.Resources.Reports.Caption.AgeUnit;
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

            plotAL.Visible = GrowthModel != null;
            buttonAL.Enabled = GrowthModel != null && GrowthModel.CombinedData.IsRegressionOK;

            plotAL.Clear();

            if (GrowthModel != null)
            {
                if (GrowthModel.ExternalData != null)
                {
                    Scatterplot ext = new Scatterplot(GrowthModel.ExternalData);
                    ext.Properties.ScatterplotName = Resources.Interface.BioReference;
                    ext.Properties.DataPointColor = Constants.InfantColor;
                    plotAL.AddSeries(ext);
                }

                if (GrowthModel.InternalData != null)
                {
                    Scatterplot inter = new Scatterplot(GrowthModel.InternalData);
                    inter.Properties.ScatterplotName = Resources.Interface.BioLoaded;
                    inter.Properties.DataPointColor = Constants.MainAccent;
                    plotAL.AddSeries(inter);
                }

                if (GrowthModel.CombinedData != null)
                {
                    Scatterplot combi = new Scatterplot(GrowthModel.CombinedData);
                    combi.Properties.ScatterplotName = Resources.Interface.BioCombined;
                    combi.Properties.ShowTrend =
                        combi.Properties.ShowConfidenceBands =
                        combi.Properties.ShowPredictionBands = true;
                    combi.Properties.ShowTrend = true;
                    combi.Properties.SelectedApproximationType = GrowthModel.Nature;
                    combi.Properties.DataPointColor = Color.Transparent;
                    combi.Properties.TrendColor = Constants.MainAccent.Darker();
                    plotAL.AddSeries(combi);
                }
            }

            //statChartAL.Refresh();
            plotAL.DoPlot();
            if (plotAL.Scatterplots.Count > 0) plotAL.DoPlot();

            #endregion

            #region Length to Mass

            plotLW.Visible = WeightModel != null;
            buttonLW.Enabled = WeightModel != null && WeightModel.CombinedData.IsRegressionOK;

            plotLW.Clear();

            if (WeightModel != null)
            {
                if (WeightModel.ExternalData != null)
                {
                    Scatterplot ext = new Scatterplot(WeightModel.ExternalData);
                    ext.Properties.ScatterplotName = Resources.Interface.BioReference;
                    ext.Properties.DataPointColor = Constants.InfantColor;
                    plotLW.AddSeries(ext);
                }

                if (WeightModel.InternalData != null)
                {
                    Scatterplot inter = new Scatterplot(WeightModel.InternalData);
                    inter.Properties.ScatterplotName = Resources.Interface.BioLoaded;
                    inter.Properties.DataPointColor = Constants.MainAccent;
                    plotLW.AddSeries(inter);
                }

                if (WeightModel.CombinedData != null)
                {
                    Scatterplot combi = new Scatterplot(WeightModel.CombinedData);
                    combi.Properties.ScatterplotName = Resources.Interface.BioCombined;
                    combi.Properties.ShowTrend =
                        combi.Properties.ShowConfidenceBands =
                        combi.Properties.ShowPredictionBands = true;

                    combi.Properties.SelectedApproximationType = WeightModel.Nature;
                    combi.Properties.DataPointColor = Color.Transparent;
                    combi.Properties.TrendColor = Constants.MainAccent.Darker();
                    plotLW.AddSeries(combi);
                }
            }

            plotLW.DoPlot();
            if (plotLW.Scatterplots.Count > 0) plotLW.DoPlot();

            pageModelAL.SetNavigation(true);

            #endregion

            #region Age to Mass

            plotAW.Visible = (GrowthModel != null && WeightModel != null);

            plotAW.Clear();

            ContinuousBio cb = new ContinuousBio(Data.Parent, SpeciesRow,
                Data.Parent.Individual.AgeColumn, Data.Parent.Individual.MassColumn, TrendType.Linear);

            if (cb != null)
            {
                Scatterplot sc = new Scatterplot(cb.InternalData);
                sc.Properties.DataPointColor = Constants.MainAccent;
                plotAW.AddSeries(sc);

                //MassGrowthModels.DisplayNameY = Resources.Reports.Caption.Mass;
                //MassGrowthModels.DisplayNameX = Resources.Reports.Caption.Age;

                //if (weightGrowthExternal != null)
                //{
                //    weightGrowthExternal.Series.Name =
                //        weightGrowthExternal.Properties.ScatterplotName =
                //        Resources.Interface.BioReference;
                //    weightGrowthExternal.Properties.DataPointColor = Constants.InfantColor;
                //    plotAW.AddSeries(weightGrowthExternal);
                //}

                //if (weightGrowthInternal != null)
                //{
                //    weightGrowthInternal = weightGrowthInternal.Copy();
                //    weightGrowthInternal.Series.Name =
                //        weightGrowthInternal.Properties.ScatterplotName =
                //        Resources.Interface.BioLoaded;
                //    weightGrowthInternal.Properties.DataPointColor = Constants.MotiveColor;
                //    plotAW.AddSeries(weightGrowthInternal);
                //}
            }

            if (GrowthModel != null && GrowthModel.CombinedData.IsRegressionOK &&
                WeightModel != null && WeightModel.CombinedData.IsRegressionOK)
            {
                Functor aw = new Functor(string.Format(Resources.Interface.MassGrowth, SpeciesRow.Species), (t) =>
                {
                    double l = GrowthModel.CombinedData.Regression.Predict(t);
                    return WeightModel.CombinedData.Regression.Predict(l);
                },
                (w) =>
                {
                    double l = WeightModel.CombinedData.Regression.PredictInversed(w);
                    return GrowthModel.CombinedData.Regression.PredictInversed(l);
                });

                aw.Properties.TrendColor = Constants.MainAccent.Darker();

                plotAW.AddSeries(aw);
            }
            
            plotAW.DoPlot();

            pageModelAW.SetNavigation(true);

            #endregion
        }

        private void plotLW_Updated(object sender, EventArgs e)
        {
            plotAW.AxisYMin = plotLW.AxisYMin;
            plotAW.AxisYMax = plotLW.AxisYMax;
            plotAW.AxisYFormat = plotLW.AxisYFormat;
            plotAW.DoPlot();
        }

        private void plotAL_Updated(object sender, EventArgs e)
        {
            plotAW.AxisXMin = plotAL.AxisXMin;
            plotAW.AxisXMax = plotAL.AxisXMax;
            plotAW.AxisXFormat = plotAL.AxisXFormat;
            plotAW.DoPlot();
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



        private void pageLW_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (ModelsReturned != null)
            {
                ModelsReturned.Invoke(sender, e);
            }
        }

        private void buttonAL_Click(object sender, EventArgs e)
        {
            plotAL.OpenRegressionProperties((Scatterplot)plotAL.GetSample(Resources.Interface.BioCombined));
        }

        private void buttonLW_Click(object sender, EventArgs e)
        {
            plotLW.OpenRegressionProperties((Scatterplot)plotLW.GetSample(Resources.Interface.BioCombined));
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
            plotSelection.ResetFormatted(gearWizard.SelectedSamplerType.ToDisplay());

            plotLengthAdjusted.AxisYTitle =
                plotMortality.AxisYTitle =
                plotAgeAdjusted.AxisYTitle =
                columnSelectivityNpue.HeaderText;

            if (AgeStructure != null) AgeStructure.Unit = gearWizard.SelectedUnit.Unit;

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
            classedComposition = gearWizard.SelectedStacks.GetClassedComposition(SpeciesRow,
                gearWizard.SelectedSamplerType, gearWizard.SelectedUnit);

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
                else
                {
                    pageLengthAdjusted_Commit(sender, e);
                }
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

            plotLength.DoPlot();

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
                        ChartType = SeriesChartType.Line
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
            plotSelectionSource.DoPlot();

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
            plotLength.AxisYTitle = comboBoxLengthSource.SelectedIndex == 0 ? "NPUE, %" : "BPUE, %";
            plotLength.Series.Clear();
            Series all = new Series() { ChartType = SeriesChartType.Column };
            foreach (SizeClass sizeClass in lengthCompositionWizard.CatchesComposition)
            {
                all.Points.AddXY(sizeClass.Size.Midpoint, comboBoxLengthSource.SelectedIndex == 0 ? sizeClass.AbundanceFraction : sizeClass.BiomassFraction);
            }
            plotLength.Series.Add(all);
            plotLength.DoPlot();
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

                plotSelection.DoPlot();

                textBoxSF.Text = SelectivityModel.SelectionFactor.ToString("N3");
                textBoxSD.Text = SelectivityModel.StandardDeviation.ToString("N3");

                plotSelection.Clear();

                for (int i = 0; i < SelectivityModel.Dimension; i++)
                {
                    int factor = i;
                    Functor sel = new Functor(SelectivityModel.Catch(factor).Name,
                        (l) => { return SelectivityModel.GetSelection(factor, l); });
                    plotSelection.AddSeries(sel);
                }

                Functor ogive = new Functor(Resources.Interface.SelectivityOgive, SelectivityModel.GetSelection);
                plotSelection.AddSeries(ogive);
                //ogive.Series.ChartType = SeriesChartType.Line;
                ogive.Series.BorderWidth = 2 * ogive.Series.BorderWidth;
                plotSelection.DoPlot();

                plotLengthAdjusted.Series.Clear();
                plotLengthAdjusted.AxisXInterval = LengthStructure.Interval;
                plotLengthAdjusted.AxisXMin = plotSelection.AxisXMin;
                plotLengthAdjusted.AxisXMax = plotSelection.AxisXMax;

                double max = 0;

                Series catches = new Series
                {
                    Name = Resources.Interface.Catches,
                    ChartType = SeriesChartType.Line

                };
                foreach (SizeClass size in lengthCompositionWizard.CatchesComposition)
                {
                    catches.Points.AddXY(size.Size.Midpoint, size.Abundance);
                    max = Math.Max(max, size.Abundance);
                }

                Series pop = new Series
                {
                    Name = Resources.Interface.CatchesAdjusted,
                    ChartType = SeriesChartType.Line
                };
                foreach (SizeClass size in LengthStructure)
                {
                    pop.Points.AddXY(size.Size.Midpoint, size.Abundance);
                    max = Math.Max(max, size.Abundance);
                }

                plotLengthAdjusted.AxisYMax = max;
                plotLengthAdjusted.Series.Add(catches);
                plotLengthAdjusted.Series.Add(pop);
                plotLengthAdjusted.DoPlot();
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

            plotAge.DoPlot();

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
            plotAge.AxisYTitle = comboBoxAgeSource.SelectedIndex == 0 ? "NPUE, %" : "BPUE, %";
            plotAge.Series.Clear();

            Series all = new Series() { ChartType = SeriesChartType.Column };
            //Series all = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.Gainsboro };
            //Series juv = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.LawnGreen };
            //Series mal = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.DodgerBlue };
            //Series fem = new Series() { ChartType = SeriesChartType.Column, BorderColor = System.Drawing.Color.Black, Color = System.Drawing.Color.Coral };

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
            plotAge.DoPlot();
            //plotT.Series.Add(juv);
            //plotT.Series.Add(mal);
            //plotT.Series.Add(fem);
        }

        private void pageAge_Commit(object sender, WizardPageConfirmEventArgs e)
        { }



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
            TotalMortalityModel = ExponentialMortalityModel.FromComposition(ageCompositionWizard.CatchesComposition, comboBoxMortalityAge.SelectedIndex);

            plotMortality.Clear();

            if (TotalMortalityModel != null)
            {
                if (TotalMortalityModel.Unexploited != null)
                {
                    Scatterplot unexp = new Scatterplot(TotalMortalityModel.Unexploited);
                    plotMortality.AddSeries(unexp);
                }

                Scatterplot exp = new Scatterplot(TotalMortalityModel.Exploited);
                exp.Properties.SelectedApproximationType = exp.Calc.TrendType;
                exp.Properties.ShowTrend = true;
                plotMortality.AddSeries(exp);
                plotMortality.ShowLegend = false;
                plotMortality.DoPlot();

                textBoxZ.Text = TotalMortalityModel.Z.ToString("N4");
                textBoxFi.Text = TotalMortalityModel.Fi.ToString("N4");
                textBoxS.Text = TotalMortalityModel.S.ToString("N4");
            }
            else
            {
                textBoxZ.Text =
                    textBoxFi.Text =
                    textBoxS.Text = Constants.Null;
            }

            plotMortality.Visible = TotalMortalityModel != null && TotalMortalityModel.Unexploited != null;

            buttonMortality.Enabled =
                TotalMortalityModel != null;

            pictureMortalityWarn.Visible =
                labelMortalityWarn.Visible =
                pageAgeAdjusted.Suppress =
                TotalMortalityModel == null;
        }

        private void buttonMortality_Click(object sender, EventArgs e)
        {
            RegressionProperties properties = new RegressionProperties(TotalMortalityModel.Exploited.Regression, false);
            properties.SetFriendlyDesktopLocation(this);
            properties.Show(this);
        }

        private void pageMortality_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (ModelsCalculated != null)
            {
                ModelsCalculated.Invoke(sender, e);
            }

            if (TotalMortalityModel != null)
            {
                for (int i = 0; i < AgeStructure.Count; i++)
                {
                    double a = TotalMortalityModel.Exploited.Regression.Predict(AgeStructure[i].Age.Value);
                    double w = ageCompositionWizard.CatchesComposition[i].MassSample.Count > 0 ? ageCompositionWizard.CatchesComposition[i].MassSample.Mean :
                        (ageCompositionWizard.CatchesComposition[i].Quantity > 0 ? (ageCompositionWizard.CatchesComposition[i].Abundance / ageCompositionWizard.CatchesComposition[i].Biomass) : 0);
                    AgeStructure[i].Abundance = a;
                    AgeStructure[i].Biomass = w * a;
                }

                plotAgeAdjusted.Series.Clear();

                plotAgeAdjusted.AxisXInterval = plotAge.AxisXInterval;
                plotAgeAdjusted.AxisXMin = plotAge.AxisXMin;
                plotAgeAdjusted.AxisXMax = plotAge.AxisXMax;

                double max = 0;

                Series catches = new Series
                {
                    Name = Resources.Interface.Catches,
                    ChartType = SeriesChartType.Line
                };
                foreach (AgeGroup group in ageCompositionWizard.CatchesComposition)
                {
                    catches.Points.AddXY(group.Age.Value, group.Abundance);
                    max = Math.Max(max, group.Abundance);
                }

                Series pop = new Series
                {
                    Name = Resources.Interface.CatchesAdjusted,
                    ChartType = SeriesChartType.Line
                };
                foreach (AgeGroup group in AgeStructure)
                {
                    pop.Points.AddXY(group.Age.Value, group.Abundance);
                    max = Math.Max(max, group.Abundance);
                }

                plotAgeAdjusted.AxisYMax = max;
                plotAgeAdjusted.Series.Add(catches);
                plotAgeAdjusted.Series.Add(pop);
                plotAgeAdjusted.DoPlot();
            }
        }



        private void pageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        { }

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
            foreach (Plot plot in new Plot[] {
                plotLength, plotSelectionSource, plotSelection,
                plotLengthAdjusted, plotAge, plotMortality, plotAgeAdjusted })
            {
                plot.DoPlot();
            }

            if (ageCompositionWizard != null) ageCompositionWizard.CatchesComposition.Name = "Age composition of catches";
            if (AgeStructure != null) AgeStructure.Name = "Adjusted age composition";

            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();
            e.Cancel = true;
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetReport();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();
            pageReport.SetNavigation(true);
            Log.Write(EventType.WizardEnded, "Population wizard is finished for {0}.", SpeciesRow.Species);
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
