using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Functions;
using System.Drawing;
using Functor = Mayfly.Mathematics.Charts.Functor;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardMSYR : Form
    {
        public CardStack Data { get; set; }

        public Data.SpeciesRow SpeciesRow;

        WizardPopulation growthWizard;

        WizardMortality mortalityWizard;

        YieldPerRecruitModel model;



        private WizardMSYR()
        {
            InitializeComponent();
        }

        public WizardMSYR(CardStack data, Data.SpeciesRow speciesRow)
            : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            Age ga = Service.GetGamingAge(SpeciesRow.Species);
            if (ga != null) textBoxTr.Value = ga;

            Log.Write(EventType.WizardStarted, "MSY/R wizard is started for {0}.",
                speciesRow.Species);
        }



        public Report GetReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Sections.MSYR.Title,
                    SpeciesRow.KeyRecord.FullNameReport));

            report.UseTableNumeration = true;

            if (checkBoxGrowth.Checked)
            {
                report.AddSectionTitle(
                    string.Format(Resources.Reports.Sections.Growth.Title,
                    SpeciesRow.KeyRecord.FullNameReport));
                growthWizard.AddGrowth(report);
                growthWizard.AddMass(report);
            }

            if (checkBoxYR.Checked)
            {
                report.AddSectionTitle(Resources.Reports.Sections.MSYR.Title1);

                report.AddParagraph(
                    string.Format(Resources.Reports.Sections.MSYR.Paragraph1, model.Tr, model.Tc, model.M));

                report.AddEquation(@"{B/R} = exp({-M} \times (T_c - T_r)) \times W_{inf.} \times \Big[ \frac{1}{Z} - \frac{3S}{Z + K} + \frac{3S^2}{Z + 2K} - \frac{S^3}{Z+3K} \Big]");
                report.AddEquation(@"{S} = exp({-K} \times {T_c - t_0})");
                report.AddEquation(@"{Y/R} = F \times {B/R}");

                report.AddParagraph(
                    string.Format(Resources.Reports.Sections.MSYR.Paragraph3,
                    model.MaximumSustainableYieldPerRecruit,
                    model.FMSY,
                    model.OptimalBiomassPerRecruit,
                    model.OptimalBiomassPerRecruit / model.VirginBiomassPerRecruit,
                    model.VirginBiomassPerRecruit));
            }

            report.EndBranded();

            return report;
        }



        private void buttonGrowth_Click(object sender, EventArgs e)
        {
            growthWizard = new WizardPopulation(Data, SpeciesRow);
            growthWizard.Calculated += growthWizard_ModelConfirmed;
            growthWizard.Replace(this);
        }

        private void growthWizard_ModelConfirmed(object sender, EventArgs e)
        {
            this.Replace(growthWizard);

            model = new YieldPerRecruitModel(
                (Power)growthWizard.WeightModel.Regression,
                (Growth)growthWizard.GrowthModel.Regression);

            textBoxW.Value = model.W;
            textBoxK.Value = model.K;
            textBoxT0.Value = model.T0;

            growthWizard.Close();
        }

        private void growth_Changed(object sender, EventArgs e)
        {
            pageGrowth.AllowNext = 
                !double.IsNaN(textBoxK.Value) &&
                !double.IsNaN(textBoxW.Value) &&
                !double.IsNaN(textBoxT0.Value);

            pictureBoxWarn.Visible = labelWarn.Visible = !model.IsOK;
        }


        private void buttonMortality_Click(object sender, EventArgs e)
        {
            mortalityWizard = new WizardMortality(Data, SpeciesRow);
            mortalityWizard.Calculated += mortalityWizard_ModelConfirmed;
            mortalityWizard.Replace(this);
        }

        private void mortalityWizard_ModelConfirmed(object sender, EventArgs e)
        {
            this.Replace(mortalityWizard);

            if (mortalityWizard.YoungestCaught != null)
            {
                textBoxTc.Value = model.Tc = mortalityWizard.YoungestCaught.Age;
            }

            mortalityWizard.Close();
        }

        private void ages_Changed(object sender, EventArgs e)
        {
            pageAges.AllowNext =
                !double.IsNaN(textBoxTc.Value) &&
                !double.IsNaN(textBoxTr.Value);
        }

        private void pageAges_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            model.Tr = textBoxTr.Value;
            model.Tc = textBoxTc.Value;
            model.M = (double)numericUpDownM.Value;
            model.Run();

            pictureBoxWarn.Visible = labelWarn.Visible = !model.IsOK;
            pageChart.AllowNext = model.IsOK;

            if (!double.IsNaN(model.FMSY))
            {
                //plotYR.AxisXMax = model.FMSY * 2;
                plotYR.AxisYMax = model.MaximumSustainableYieldPerRecruit;
                plotYR.AxisY2Max = model.VirginBiomassPerRecruit;

                plotYR.Series.Clear();

                Functor yr = new Functor("Y/R", model.GetYieldPerRecruit);
                Functor br = new Functor("B/R", model.GetBiomassPerRecruit);

                plotYR.AddSeries(yr);
                plotYR.AddSeries(br);
                br.Series.YAxisType = AxisType.Secondary;

                plotYR.Remaster();

                plotYR.ChartAreas[0].AxisX.StripLines.Clear();
                plotYR.ChartAreas[0].AxisY.StripLines.Clear();
                plotYR.ChartAreas[0].AxisY2.StripLines.Clear();

                if (!double.IsNaN(model.FMSY))
                {
                    yr.AxisX.AddStripLine(model.FMSY, string.Format("F(MSY) ({0:N3})", model.FMSY));

                    yr.AxisY.AddStripLine(model.MaximumSustainableYieldPerRecruit, 
                        string.Format("MSY/R ({0:N0})",
                        model.MaximumSustainableYieldPerRecruit),
                        yr.Properties.TrendColor);

                    br.AxisY.AddStripLine(model.OptimalBiomassPerRecruit,
                        string.Format("Optimum B/R ({0:N0}, or {1:P1})",
                        model.OptimalBiomassPerRecruit, 
                        model.OptimalBiomassPerRecruit / model.VirginBiomassPerRecruit), 
                        br.Properties.TrendColor);

                    //plotYR.ChartAreas[0].AxisX.CustomLabels.Clear();

                    //CustomLabel overfishing = new CustomLabel();
                    //overfishing.FromPosition = model.FMSY;
                    //overfishing.ToPosition = plotYR.AxisXMax;
                    //overfishing.Text = "Growth overfishing";
                    //overfishing.RowIndex = 1;
                    //overfishing.LabelMark = LabelMarkStyle.LineSideMark;
                    //plotYR.ChartAreas[0].AxisX.CustomLabels.Add(overfishing);
                }

            }
            else
            {
                plotYR.Series.Clear();
            }
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
            pageReport.SetNavigation(true);
            ((Report)e.Result).Run();
            Log.Write(EventType.WizardEnded, "MSY/R wizard is finished. Species: {0}, MSY/R = {1:N3}.",
                SpeciesRow.Species, model.MaximumSustainableYieldPerRecruit);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
