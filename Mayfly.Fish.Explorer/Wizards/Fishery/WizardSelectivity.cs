using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer.Survey;
using Mayfly.Mathematics.Charts;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mayfly.Fish.Explorer.Fishery
{
    public partial class WizardSelectivity : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        public Data.SpeciesRow SpeciesRow;

        public CompositionEqualizer CatchesComposition;

        LengthComposition Structure { set; get; }

        GillnetSelectivityModel Selectivity { set; get; }



        private WizardSelectivity()
        {
            InitializeComponent();
        }

        public WizardSelectivity(CardStack data, Data.SpeciesRow speciesRow)
            : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(SpeciesRow.KeyRecord.FullName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.FullName, SpeciesRow.KeyRecord.FullName);

            Log.Write(EventType.WizardStarted, "Selectivity wizard is started for {0}.", 
                speciesRow.Species);
        }



        private Report GetReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Selectivity.Title, 
                    gearWizard.SelectedSamplerType.ToDisplay(), SpeciesRow.KeyRecord.FullNameReport));
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            if (checkBoxLength.Checked)
            {
                report.AddParagraph(Resources.Reports.Selectivity.Paragraph1, SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

                report.AddTable(
                    CatchesComposition.SeparateCompositions.ToArray().GetTable( 
                        CompositionColumn.Abundance,
                        string.Format(Resources.Reports.Selectivity.Table1, SpeciesRow.KeyRecord.FullNameReport),
                        CatchesComposition.Name, Resources.Reports.Caption.GearClass)
                    );

                report.AddParagraph(Resources.Reports.Selectivity.Paragraph2, report.NextTableNumber);

                report.AddTable(
                    CatchesComposition.GetTable(
                        CompositionColumn.Abundance | CompositionColumn.AbundanceFraction,
                        string.Format(Resources.Reports.Selectivity.Table2, SpeciesRow.KeyRecord.FullNameReport),
                        CatchesComposition.Name)
                    );
            }

            if (checkBoxSelectivity.Checked)
            {
                report.AddParagraph(
                    string.Format(Resources.Reports.Selectivity.Paragraph3,
                    report.NextTableNumber - 2, report.NextTableNumber));

                Selectivity.AddReport1(report, string.Format(Resources.Reports.Selectivity.Table3,
                    SpeciesRow.KeyRecord.FullNameReport, gearWizard.SelectedSamplerType.ToDisplay()));

                report.AddParagraph(
                    string.Format(Resources.Reports.Selectivity.Paragraph4, 
                    report.NextTableNumber - 1, Selectivity.SelectionFactor, 
                    Selectivity.StandardDeviation,report.NextTableNumber));

                Selectivity.AddReport2(report, string.Format(Resources.Reports.Selectivity.Table4,
                    SpeciesRow.KeyRecord.FullNameReport));
            }

            if (checkBoxPopulation.Checked)
            {
                report.AddParagraph(
                    string.Format(Resources.Reports.Selectivity.Paragraph5,
                    report.NextTableNumber)
                    );

                Report.Table table5 = Structure.GetTable(
                    CompositionColumn.Abundance | CompositionColumn.AbundanceFraction, 
                    string.Format(Resources.Reports.Selectivity.Table5, SpeciesRow.KeyRecord.FullNameReport),
                    Structure.Name
                    );

                report.AddTable(table5);
            }

            return report;
        }




        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            gearWizard = new WizardGearSet(Data, SpeciesRow);
            gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            gearWizard.Replace(this);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            this.Replace(gearWizard);

            //spreadSheetComposition.ClearInsertedColumns();
            plotC.Clear();
            pageCatches.SetNavigation(false);
            backCompose.RunWorkerAsync();
        }

        private void backCompose_DoWork(object sender, DoWorkEventArgs e)
        {
            Structure = gearWizard.SelectedData.GetLengthCompositionFrame(SpeciesRow, UserSettings.SizeInterval);
            Structure.Name = Fish.Resources.Common.SizeUnits;

            CatchesComposition = new CompositionEqualizer(Structure);
            CatchesComposition = gearWizard.SelectedStacks.ToArray().GetWeightedComposition(
                gearWizard.WeightType, gearWizard.SelectedUnit.Variant, Structure, SpeciesRow);

            if (gearWizard.SelectedSamplerType.IsPassive())
            {
                List<LengthComposition> _catches = new List<LengthComposition>();
                List<double> _meshes = new List<double>();

                for (int i = 0; i < CatchesComposition.Dimension; i++)
                {
                    Composition comp = CatchesComposition.GetComposition(i);

                    if (comp is LengthComposition)
                    {
                        _catches.Add((LengthComposition)comp);
                        _meshes.Add(gearWizard.SelectedStacks[i][0].Mesh);
                    }
                }

                Selectivity = new GillnetSelectivityModel(_catches, _meshes);

                for (int i = 0; i < Structure.Count; i++)
                {
                    double a = CatchesComposition[i].Abundance;
                    double s = Selectivity.GetSelection(Structure[i].Size.Midpoint);
                    double w = (CatchesComposition[i].Quantity > 0) ?
                        (CatchesComposition[i].Abundance / CatchesComposition[i].Biomass) : 0.0;
                    Structure[i].Abundance = (s > .01) ? (a / s) : a;
                    Structure[i].Biomass = w * Structure[i].Abundance;
                }
            }
        }

        private void backCompose_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Structure.SetLines(columnComposition);

            plotC.Series.Clear();

            plotC.AxisXAutoInterval = false;
            plotC.AxisXInterval = Structure.Interval;
            plotC.AxisXAutoMinimum = false;
            plotC.AxisXMin = Structure.Minimum;
            plotC.AxisXAutoMaximum = false;
            plotC.AxisXMax = Structure.Ceiling;

            plotC.AxisYAutoMinimum = false;
            plotC.AxisYAutoMaximum = false;
            plotC.AxisYMin = 0.0;

            double max = 0;

            foreach (Composition comp in CatchesComposition.SeparateCompositions)
            {
                //spreadSheetComposition.InsertColumn(comp.Name, comp.Name,
                //    typeof(double), spreadSheetComposition.ColumnCount, 75, "N3").ReadOnly = true;
                //comp.UpdateValues(spreadSheetComposition, ValueVariant.Abundance);

                if (comp is LengthComposition)
                {
                    Series catches = new Series();
                    catches.Name = comp.Name;
                    catches.ChartType = SeriesChartType.Column;
                    foreach (SizeClass size in comp) {
                        catches.Points.AddXY(size.Size.Midpoint, size.Abundance);
                        max = Math.Max(max, size.Abundance);
                    }

                    plotC.Series.Add(catches);
                }
            }

            plotC.AxisYMax = max;
            plotC.Remaster();
            
            pageCatches.SetNavigation(true);
            pageCatches.AllowNext = (Selectivity != null);            
            labelWarn.Visible = pictureBoxWarn.Visible = (Selectivity == null);
        }

        private void pageCatches_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
        }

        private void pageCatches_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Selectivity != null) {

                plotS.AxisXMin = plotC.AxisXMin;
                plotS.AxisXMax = plotC.AxisXMax;
                plotS.AxisXInterval = Structure.Interval;

                plotS.Remaster();

                textBoxSF.Text = Selectivity.SelectionFactor.ToString("N3");
                textBoxSD.Text = Selectivity.StandardDeviation.ToString("N3");

                plotS.Clear();

                Functor ogive = new Functor(Resources.Interface.Interface.SelectivityOgive, Selectivity.GetSelection);
                plotS.AddSeries(ogive);
                ogive.Series.ChartType = SeriesChartType.Area;
                ogive.Series.BackHatchStyle = ChartHatchStyle.Percent90;


                for (int i = 0; i < Selectivity.Dimension; i++)
                {
                    int factor = i;
                    Functor sel = new Functor(Selectivity.Catch(factor).Name,
                        (l) => { return Selectivity.GetSelection(factor, l); });
                    plotS.AddSeries(sel);
                }

                plotS.Remaster();
            }
        }

        private void pageSelectivity_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            plotP.Series.Clear();

            plotP.AxisXInterval = Structure.Interval;
            plotP.AxisXMin = plotC.AxisXMin;
            plotP.AxisXMax = plotC.AxisXMax;

            double max = 0;

            Series catches = new Series();
            catches.Name = Resources.Interface.Interface.Catches;
            catches.ChartType = SeriesChartType.Column;
            foreach (SizeClass size in CatchesComposition) {
                catches.Points.AddXY(size.Size.Midpoint, size.AbundanceFraction);
                max = Math.Max(max, size.AbundanceFraction);
            }

            Series pop = new Series();
            pop.Name = Resources.Interface.Interface.CatchesCorrected;
            pop.ChartType = SeriesChartType.Column;
            foreach (SizeClass size in Structure) {
                pop.Points.AddXY(size.Size.Midpoint, size.AbundanceFraction);
                max = Math.Max(max, size.AbundanceFraction);
            }

            plotP.AxisYMax = max;
            plotP.RecalculateAxesProperties();
            plotP.RefreshAxes();
            plotP.Series.Add(catches);
            plotP.Series.Add(pop);

            plotP.Remaster();
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
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
            Log.Write(EventType.WizardEnded, "Selectivity wizard is finished for {0}, catch biomass ({1:N3}) is adjusted to {2:N3}.", 
                SpeciesRow.Species, CatchesComposition.TotalBiomass, Structure.TotalBiomass);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
