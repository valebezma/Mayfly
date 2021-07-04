using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer;
using Mayfly.Fish;
using System.Resources;
using Mayfly.Geographics;
using Mayfly.Wild;
using Mayfly.Species;

namespace Mayfly.Prospect
{
    public partial class WizardSurveyBrief : Form
    {
        public ComplexSurvey Survey;

        public Composition CatchesComposition;

        SpeciesKey.BaseRow benthosBase;



        public WizardSurveyBrief(ComplexSurvey survey)
        {
            InitializeComponent();

            listViewSpecies.Shine();

            Survey = survey;

            pageWater.Suppress = !(Survey is WaterComplexSurvey);

            if (Survey is WaterComplexSurvey)
            {
                UpdateWater(((WaterComplexSurvey)Survey).Water);
            }

            UpdateEnvironment();

            pagePlankton.Suppress = Survey.PlanktonCards.Count == 0;
            buttonExplorePlankton.Enabled = Survey.PlanktonCards.Count > 0;

            pageBenthos.Suppress = Survey.BenthosCards.Count == 0;
            buttonExploreBenthos.Enabled = Survey.BenthosCards.Count > 0;

            pageSpeciesList.Suppress = 
                pageSpeciesComposition.Suppress =
                Survey.FishCards.IsEmpty;

            buttonExploreFish.Enabled = !Survey.FishCards.IsEmpty;

            UpdateBenthos();

            UpdateFish();
        }



        private void UpdateWater(Waters.WatersKey.WaterRow waterRow)
        {
            pageWater.ResetFormatted(waterRow.FullName);
            labelWaterInstruction.ResetFormatted(waterRow.FullName, waterRow.Description, waterRow.GetPath());

            panelStream.Visible = waterRow.WaterType == Waters.WaterType.Stream;

            if (!waterRow.IsLengthNull()) textBoxLength.Text = waterRow.Length.ToString();
            if (!waterRow.IsWatershedNull()) textBoxWatershed.Text = waterRow.Watershed.ToString();
            if (!waterRow.IsConsumptionNull()) textBoxSpend.Text = waterRow.Consumption.ToString();
            if (!waterRow.IsVolumeNull()) textBoxVolume.Text = waterRow.Volume.ToString();
            if (!waterRow.IsSlopeNull()) textBoxSlope.Text = waterRow.Slope.ToString();

            panelLake.Visible = waterRow.WaterType == Waters.WaterType.Lake;

            if (!waterRow.IsAltitudeNull()) textBoxAltitude.Text = waterRow.Altitude.ToString();
            if (!waterRow.IsWatershedNull()) textBoxWatershed.Text = waterRow.Watershed.ToString();
            if (!waterRow.IsAreaNull()) textBoxArea.Text = waterRow.Area.ToString();
            if (!waterRow.IsVolumeNull()) textBoxVolume.Text = waterRow.Volume.ToString();
            if (!waterRow.IsDepthMaxNull()) textBoxDepthMax.Text = waterRow.DepthMax.ToString();
            if (!waterRow.IsDepthMidNull()) textBoxDepthAve.Text = waterRow.DepthMid.ToString();

            panelTank.Visible = waterRow.WaterType == Waters.WaterType.Tank;

            if (!waterRow.IsLengthNull()) textBoxLength.Text = waterRow.Length.ToString();
            if (!waterRow.IsAltitudeNull()) textBoxAltitude.Text = waterRow.Altitude.ToString();
            if (!waterRow.IsAreaNull()) textBoxArea.Text = waterRow.Area.ToString();
            if (!waterRow.IsAreaDeadNull()) textBoxAreaDead.Text = waterRow.AreaDead.ToString();
            if (!waterRow.IsVolumeNull()) textBoxVolume.Text = waterRow.Volume.ToString();
            if (!waterRow.IsVolumeDeadNull()) textBoxVolumeDead.Text = waterRow.VolumeDead.ToString();
            if (!waterRow.IsDepthMaxNull()) textBoxDepthMax.Text = waterRow.DepthMax.ToString();
            if (!waterRow.IsDepthMidNull()) textBoxDepthAve.Text = waterRow.DepthMid.ToString();
            if (!waterRow.IsFlowageNull()) textBoxFlowage.Text = waterRow.Flowage.ToString();
            if (!waterRow.IsCyclingNull()) textBoxCycling.Text = waterRow.Cycling.ToString();
        }



        private void UpdateEnvironment()
        {
            textBoxLocations.Text = Survey.GetLocationDescription();
            textBoxFlowRates.Text = Survey.GetFlowRateDescription();
            textBoxDepths.Text = Survey.GetDepthDescription();
        }


        private void UpdateBenthos()
        {
            comboBoxBenBase.AddBaseList(Benthos.UserSettings.SpeciesIndex);

            textBoxBenN.Text = Survey.BenthosCards.GetTotalAbundance().ToString("N0");
            textBoxBenB.Text = Survey.BenthosCards.GetTotalBiomass().ToString("N2");
        }



        private void UpdateFish()
        {
            listViewSpecies.Items.Clear();

            if (!Survey.FishCards.IsEmpty)
            {
                foreach (Data.SpeciesRow speciesRow in Survey.FishCards.GetSpecies())
                {
                    ListViewItem item = listViewSpecies.CreateItem(speciesRow.Species,
                        speciesRow.GetFullName());
                    int q = Survey.FishCards.Quantity(speciesRow);
                    item.SubItems.Add(q > 0 ? q.ToString() : Constants.Null);
                }

                listViewSpecies.Sort();

                comboBoxGearType.DataSource = Survey.FishCards.GetSamplerTypeDisplays();
                comboBoxGearType_SelectedIndexChanged(comboBoxGearType, new EventArgs());
            }

            ColumnCatchesAbundance.ResetFormatted(new UnitEffort(ExpressionVariant.Volume).Unit);
            ColumnCatchesBiomass.ResetFormatted(new UnitEffort(ExpressionVariant.Volume).Unit);
        }

        private Composition CalculateQuickComposition(FishSamplerType samplerType)
        {
            CardStack typeStack = Survey.FishCards.GetStack(samplerType);
            string[] classes = typeStack.Classes(samplerType);

            if (classes.Length == 0)
            {
                return typeStack.GetCommunityComposition();
            }
            else
            {
                // Create frame
                SpeciesComposition example = typeStack.GetCommunityCompositionFrame();
                CompositionEqualizer result = new CompositionEqualizer(example);
                
                foreach (string mesh in classes)
                {
                    CardStack meshStack = typeStack.GetStack(samplerType, mesh);
                    Composition classComposition = meshStack.GetCommunityComposition(
                        example);
                    classComposition.Weight = meshStack.GetEffort(samplerType, ExpressionVariant.Volume);
                    result.AddComposition(classComposition);
                }

                // Calculate weighted by effort composition
                result.GetWeighted();

                return result;
            }
        }

        public Report GetReport()
        {
            Report report = new Report(Resources.Reports.Brief.Title);

            report.UseTableNumeration = true;

            if (Survey is WaterComplexSurvey)
            {
                ((WaterComplexSurvey)Survey).Water.AddWaterBlock(report);
                ((WaterComplexSurvey)Survey).Water.AddWatershedBlock(report);
            }

            Survey.AddEnvironmentReport(report);

            if (Survey.PlanktonCards.Count > 0)
            {
                Survey.AddPlanktonReport(report);
            }

            if (Survey.BenthosCards.Count > 0)
            {
                Survey.AddBenthosReport(report, benthosBase);
            }

            if (Survey.FishCards.Count > 0)
            {
                Survey.AddFishReport(report, 
                    ((FishSamplerTypeDisplay)comboBoxGearType.SelectedValue).Type,
                    CatchesComposition, 
                    ColumnCatchesAbundance.DefaultCellStyle.Format,
                    ColumnCatchesBiomass.DefaultCellStyle.Format);
            }

            report.EndBranded();

            return report;
        }



        private void pageBenthos_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            comboBoxBenBase.SelectedItem = Benthos.UserSettings.SpeciesIndex.Base.FindByID(UserSettings.SelectedBenthosBase);
        }

        private void comboBoxBenBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Composition cmp;

            if (comboBoxBenBase.SelectedItem is SpeciesKey.BaseRow)
            {
                benthosBase = (SpeciesKey.BaseRow)comboBoxBenBase.SelectedItem;
                cmp = Survey.BenthosCards.GetTaxonomicComposition(benthosBase);                
            }
            else
            {
                cmp = Survey.BenthosCards.GetCommunityComposition();
            }

            cmp.SetLines(columnBenTaxa);
            cmp.UpdateValues(columnBenTaxa, columnBenA, ValueVariant.Abundance);
            cmp.UpdateValues(columnBenTaxa, columnBenAP, ValueVariant.AbundanceFraction);
            cmp.UpdateValues(columnBenTaxa, columnBenB, ValueVariant.Biomass);
            cmp.UpdateValues(columnBenTaxa, columnBenBP, ValueVariant.BiomassFraction);
        }

        private void pageBenthos_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (benthosBase == null) UserSettings.SelectedBenthosBase = -1;
            else UserSettings.SelectedBenthosBase = benthosBase.ID;
        }



        private void comboBoxGearType_SelectedIndexChanged(object sender, EventArgs e)
        {
            spreadSheetCatches.Rows.Clear();

            if (comboBoxGearType.SelectedValue == null) return;

            comboBoxGearType.Enabled = false;
            pageSpeciesComposition.SetNavigation(false);
            FishSamplerType type = ((FishSamplerTypeDisplay)comboBoxGearType.SelectedValue).Type;
            ColumnCatchesAbundance.ResetFormatted(type.GetDefaultUnitEffort().Unit);
            ColumnCatchesBiomass.ResetFormatted(type.GetDefaultUnitEffort().Unit);
            if (!backComposer.IsBusy) backComposer.RunWorkerAsync(type);
        }

        private void backComposer_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = CalculateQuickComposition((FishSamplerType)e.Argument);
        }

        private void backComposer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CatchesComposition = (Composition)e.Result;

            CatchesComposition.SetLines(ColumnCatchesSpecies);

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                spreadSheetCatches[ColumnCatchesAbundance.Index, i].Value = CatchesComposition[i].Abundance;
                spreadSheetCatches[ColumnCatchesAbundanceP.Index, i].Value = CatchesComposition[i].AbundanceFraction;
                spreadSheetCatches[ColumnCatchesBiomass.Index, i].Value = CatchesComposition[i].Biomass;
                spreadSheetCatches[ColumnCatchesBiomassP.Index, i].Value = CatchesComposition[i].BiomassFraction;
            }

            spreadSheetCatches.Sort(ColumnCatchesAbundanceP, ListSortDirection.Descending);
            pageSpeciesComposition.SetNavigation(true);
            comboBoxGearType.Enabled = true;
        }

        private void wizardControl1_Cancelling(object sender, EventArgs e)
        {
            Close();
        }



        private void buttonReport_Click(object sender, EventArgs e)
        {
            GetReport().Run();
        }

        private void buttonExplorePlankton_Click(object sender, EventArgs e)
        {
            //Mayfly.Plankton.Explorer.MainForm explorer = new Plankton.Explorer.MainForm(PlanktonData);
            //explorer.Show();
        }

        private void buttonExploreBenthos_Click(object sender, EventArgs e)
        {
            //Mayfly.Benthos.Explorer.MainForm explorer = new Benthos.Explorer.MainForm(BenthosData);
            //explorer.Show();
        }

        private void buttonExploreFish_Click(object sender, EventArgs e)
        {
            Mayfly.Fish.Explorer.MainForm explorer = new Fish.Explorer.MainForm(Survey.FishCards);
            explorer.Show();
        }
    }
}
