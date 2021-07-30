using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer.Survey;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer.Fishery
{
    public partial class WizardExtrapolations : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        private WizardCenosisComposition compositionWizard;

        public double Area 
        {
            get
            {
                return (double)numericUpDownArea.Value * 10000;
            }

            set
            {
                if (numericUpDownArea.Maximum < (decimal)value)
                {
                    numericUpDownArea.Maximum = (decimal)value * 2;
                }
                numericUpDownArea.Value = (decimal)value / 10000;
            }
        }

        public double Depth 
        {
            get { return (double)numericUpDownDepth.Value; }
            set { numericUpDownDepth.Value = (decimal)value; }
        }

        public double Volume 
        {
            get
            {
                return (double)numericUpDownVolume.Value * 1000;
            }

            set
            {
                if (numericUpDownVolume.Maximum < (decimal)value)
                {
                    numericUpDownVolume.Maximum = (decimal)value * 2;
                }
                numericUpDownVolume.Value = (decimal)value / 1000;
            }
        }

        public List<Category> GamingStocks { get; set; }



        public WizardExtrapolations()
        {
            InitializeComponent();

            ColumnSpecies.ValueType =
                typeof(string);

            ColumnAbundance.ValueType =
                ColumnBiomass.ValueType =
                ColumnN.ValueType =
                ColumnB.ValueType =
                ColumnGamingN.ValueType =
                ColumnGamingB.ValueType =
                ColumnGamingLength.ValueType =
                typeof(double);

            ColumnGamingAge.ValueType = 
                typeof(Age);

            this.RestoreAllCheckStates();
        }

        public WizardExtrapolations(CardStack data)
            : this()
        {
            Data = data;
            Log.Write(EventType.WizardStarted, "Extrapolations wizard is started.");
        }


        
        private void UpdateVolume() 
        {
            Volume = Area * Depth;
            numericUpDownVolume.Value = (decimal)(Volume / 1000.0);
        }

        private void UpdateStocks() 
        {
            foreach (Category species in compositionWizard.NaturalComposition)
            {
                double s = (gearWizard.SelectedUnit.Variant == ExpressionVariant.Square ? Area : Volume) 
                    / gearWizard.SelectedUnit.UnitCost;

                species.Quantity = (int)(species.Abundance * s);
                species.Mass = (species.Biomass * s);
            }

            for (int i = 0 ; i < compositionWizard.NaturalComposition.Count ; i++)
            {
                spreadSheetStocks[ColumnN.Index, i].Value = compositionWizard.NaturalComposition[i].Quantity / 1000.0;
                spreadSheetStocks[ColumnB.Index, i].Value = compositionWizard.NaturalComposition[i].Mass / 1000.0;
            }
        }

        private void UpdateGamingStock(int i) 
        {
            Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(
                compositionWizard.NaturalComposition[i].Name);            

            if (spreadSheetStocks[ColumnGamingLength.Index, i].Value != null)
            {
                BackgroundWorker populationSizeComposer = new BackgroundWorker();
                populationSizeComposer.DoWork += new DoWorkEventHandler(populationSizeComposer_DoWork);
                populationSizeComposer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(populationComposer_RunWorkerCompleted);
                populationSizeComposer.RunWorkerAsync(speciesRow);
            }
            else if (spreadSheetStocks[ColumnGamingAge.Index, i].Value != null)
            {
                BackgroundWorker populationAgeComposer = new BackgroundWorker();
                populationAgeComposer.DoWork += new DoWorkEventHandler(populationAgeComposer_DoWork);
                populationAgeComposer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(populationComposer_RunWorkerCompleted);
                populationAgeComposer.RunWorkerAsync(speciesRow);
            }
            else
            {
                spreadSheetStocks[ColumnGamingN.Index, i].Value = null;
                spreadSheetStocks[ColumnGamingB.Index, i].Value = null;

                GamingStocks[i].Quantity = 0;
                GamingStocks[i].Mass = 0.0;
            }
        }



        private Report GetReport() 
        {
            Report report = new Report(Resources.Reports.Extrapolations.Title);

            gearWizard.SelectedData.AddCommon(report);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddSummarySection(report);
            }

            if (checkBoxCatches.Checked)
            {
                compositionWizard.CatchesComposition.AddSummarySection(Data.Parent, report,
                    gearWizard.AbundanceUnits, gearWizard.BiomassUnits);

                //compositionWizard.AddCatchesComposition(report);
            }

            if (checkBoxCenosis.Checked)
            {
                compositionWizard.AddSummarySection(report);
            }

            if (checkBoxStocks.Checked)
            {
                AddStock(report);
            }

            if (checkBoxCPUE.Checked)
            {
                foreach (Report.Appendix app in compositionWizard.GetCpueAppendices())
                {
                    report.AddAppendix(app);
                }
            }

            if (checkBoxAbundance.Checked)
            {
                compositionWizard.AddAbundanceAppendices(report);
            }

            return report;
        }

        private void AddStock(Report report) 
        {
            string waterSize = string.Empty;

            switch (gearWizard.SelectedUnit.Variant)
            {
                case ExpressionVariant.Efforts:
                    waterSize = string.Format(Resources.Reports.Extrapolation.Paragraph5_E, Volume,
                        gearWizard.SelectedUnit.Unit, gearWizard.SelectedUnit.UnitCost);
                    break;

                case ExpressionVariant.Volume:
                    waterSize = string.Format(Resources.Reports.Extrapolation.Paragraph5_V, Volume / 1000.0);
                    break;

                case ExpressionVariant.Square:
                    waterSize = string.Format(Resources.Reports.Extrapolation.Paragraph5_S, Area / 10000.0);
                    break;
            }

            report.AddParagraph(Resources.Reports.Extrapolations.Paragraph1,
                waterSize, report.NextTableNumber);

            Report.Table table1 = new Report.Table(Resources.Reports.Extrapolations.Table1);

            table1.StartRow();
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25, 2);
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnRegistered, 2);
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnGameTerm, 2, CellSpan.Rows);
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnGaming, 2);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnQty);
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnMss);
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnQty);
            table1.AddHeaderCell(Resources.Reports.Extrapolations.ColumnMss);
            table1.EndRow();

            for (int i = 0; i < compositionWizard.NaturalComposition.Count; i++)
            {
                Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(
                    compositionWizard.NaturalComposition[i].Name);

                table1.StartRow();
                table1.AddCell(speciesRow.ToShortHTML());
                table1.AddCellRight(compositionWizard.NaturalComposition[i].Quantity / 1000, ColumnN.DefaultCellStyle.Format);
                table1.AddCellRight(compositionWizard.NaturalComposition[i].Mass / 1000, ColumnB.DefaultCellStyle.Format);

                if (spreadSheetStocks[ColumnGamingLength.Index, i].Value != null)
                {
                    double sizeClass = (double)spreadSheetStocks[ColumnGamingLength.Index, i].Value;
                    table1.AddCellValue(sizeClass);
                }
                else if (spreadSheetStocks[ColumnGamingAge.Index, i].Value != null)
                {
                    Age age = (Age)spreadSheetStocks[ColumnGamingAge.Index, i].Value;
                    table1.AddCellValue(age.Group);
                }
                else
                {
                    table1.AddCell();
                }

                if (GamingStocks[i].Quantity == 0) table1.AddCell();
                else table1.AddCellRight((double)GamingStocks[i].Quantity / 1000, 
                    ColumnN.DefaultCellStyle.Format);

                if (GamingStocks[i].Mass == 0) table1.AddCell();
                else table1.AddCellRight(GamingStocks[i].Mass / 1000, 
                    ColumnB.DefaultCellStyle.Format);

                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
            table1.AddCellRight(compositionWizard.NaturalComposition.GetTotalQuantity() / 1000, ColumnN.DefaultCellStyle.Format);
            table1.AddCellRight(compositionWizard.NaturalComposition.GetTotalMass() / 1000, ColumnB.DefaultCellStyle.Format);
            table1.AddCell();
            table1.AddCellRight((double)GamingStocks.GetTotalQuantity() / 1000, ColumnN.DefaultCellStyle.Format);
            table1.AddCellRight(GamingStocks.GetTotalMass() / 1000, ColumnB.DefaultCellStyle.Format);
            table1.EndRow();
            report.AddTable(table1);
        }


                


        private void populationSizeComposer_DoWork(object sender, DoWorkEventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)e.Argument;
            e.Result = gearWizard.SelectedStacks.ToArray().GetWeightedComposition(
                gearWizard.WeightType, gearWizard.SelectedUnit.Variant, Data.GetLengthCompositionFrame(speciesRow, UserSettings.SizeInterval), speciesRow);
        }

        private void populationAgeComposer_DoWork(object sender, DoWorkEventArgs e)
        {
            Data.SpeciesRow speciesRow = (Data.SpeciesRow)e.Argument;
            e.Result = gearWizard.SelectedStacks.ToArray().GetWeightedComposition(
                gearWizard.WeightType, gearWizard.SelectedUnit.Variant, Data.GetAgeCompositionFrame(speciesRow), speciesRow);
        }

        private void populationComposer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error is ArgumentException)
                return;

            Composition populationComposition = (Composition)e.Result;
            Category stock = compositionWizard.NaturalComposition.GetCategory(populationComposition.Name);
            int i = compositionWizard.NaturalComposition.IndexOf(stock);

            if (populationComposition == null)
            {
                GamingStocks[i].Quantity = 0;
                GamingStocks[i].Mass = 0.0;
                
                spreadSheetStocks[ColumnGamingN.Index, i].Value = null;
                spreadSheetStocks[ColumnGamingB.Index, i].Value = null;
            }
            else
            {
                double n = stock.Quantity;
                double b = stock.Mass;

                double f_n = 1;
                double f_b = 1;

                if (spreadSheetStocks[ColumnGamingAge.Index, i].Value != null)
                {
                    Age age = (Age)spreadSheetStocks[ColumnGamingAge.Index, i].Value;
                    string categoryName = age.Group;
                    Category graduates = populationComposition.GetCategory(categoryName);

                    if (graduates == null)
                    {
                        if (age < new Age(populationComposition[0].Name))
                        {
                            f_n = f_b = 1;
                        }
                        else
                        {
                            f_n = f_b = 0;
                        }
                    }
                    else
                    {
                        f_n = populationComposition.AbundanceFractionStartingFrom(graduates);
                        f_b = populationComposition.BiomassFractionStartingFrom(graduates);
                    }
                }
                else if (spreadSheetStocks[ColumnGamingLength.Index, i].Value != null)
                {
                    double sizeClass = (double)spreadSheetStocks[ColumnGamingLength.Index, i].Value;
                    //string categoryName1 = sizeClass.ToString(Resources.Interface.SizeClassMask);
                    Category reached = populationComposition.GetCategory(sizeClass.ToString());

                    if (reached == null)
                    {
                        if (sizeClass * 10.0 < compositionWizard.NaturalComposition[i].LengthSample.Minimum)
                        {
                            f_n = f_b = 1;
                        }
                        else
                        {
                            f_n = f_b = 0;
                        }
                    }
                    else
                    {
                        f_n = populationComposition.AbundanceFractionStartingFrom(reached);
                        f_b = populationComposition.BiomassFractionStartingFrom(reached);
                    }
                }

                GamingStocks[i].Quantity = (int)(n * f_n);
                GamingStocks[i].Mass = b * f_b;

                if (GamingStocks[i].Quantity == 0) spreadSheetStocks[ColumnGamingN.Index, i].Value = null;
                else spreadSheetStocks[ColumnGamingN.Index, i].Value = GamingStocks[i].Quantity / 1000;

                if (GamingStocks[i].Mass == 0) spreadSheetStocks[ColumnGamingB.Index, i].Value = null;
                else spreadSheetStocks[ColumnGamingB.Index, i].Value = GamingStocks[i].Mass / 1000;
            }
        }
        



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            gearWizard = new WizardGearSet(Data);
            gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            gearWizard.Replace(this);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            ColumnAbundance.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnBiomass.ResetFormatted(gearWizard.SelectedUnit.Unit);
            numericUpDownDepth.Enabled = gearWizard.SelectedUnit.Variant != ExpressionVariant.Square;
            this.Replace(gearWizard);

            if (compositionWizard == null)
            {
                compositionWizard = new WizardCenosisComposition(Data);
                compositionWizard.Returned += compositionWizard_Returned;
                compositionWizard.Finished += compositionWizard_Finished;
            }

            compositionWizard.Replace(this);
            compositionWizard.Run(gearWizard, null);

            checkBoxCatches_CheckedChanged(sender, e);
        }

        private void compositionWizard_Returned(object sender, EventArgs e)
        {
            //this.Replace(compositionWizard);
            //gearWizard.Replace(this);
            gearWizard.Replace(compositionWizard);
        }

        private void compositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(compositionWizard);
            compositionWizard.NaturalComposition.SetLines(ColumnSpecies);

            GamingStocks = new List<Category>();

            foreach (Category species in compositionWizard.NaturalComposition)
            {
                GamingStocks.Add(new SpeciesSwarm(species.Name));
            }

            for (int i = 0; i < compositionWizard.NaturalComposition.Count; i++)
            {
                spreadSheetStocks[ColumnAbundance.Index, i].Value = compositionWizard.NaturalComposition[i].Abundance;
                spreadSheetStocks[ColumnBiomass.Index, i].Value = compositionWizard.NaturalComposition[i].Biomass;

                // Update remembered gaming limits

                if (spreadSheetStocks[ColumnGamingLength.Index, i].Value != null) continue;
                if (spreadSheetStocks[ColumnGamingAge.Index, i].Value != null) continue;

                double measure = Service.GetMeasure(compositionWizard.NaturalComposition[i].Name);

                if (double.IsNaN(measure))
                {
                    Age gamingAge = Service.GetGamingAge(compositionWizard.NaturalComposition[i].Name);

                    if (gamingAge != null)
                    {
                        spreadSheetStocks[ColumnGamingAge.Index, i].Value = gamingAge;
                    }
                }
                else
                {
                    spreadSheetStocks[ColumnGamingLength.Index, i].Value = measure;
                }
            }
        }



        private void pageWater_Initialize(object sender, WizardPageInitEventArgs e)
        {
            numericUpDownArea.Value = (decimal)(UserSettings.MemorizedWaterArea / 10000.0);
            numericUpDownDepth.Value = (decimal)UserSettings.MemorizedWaterDepth;
        }

        private void numericUpDownArea_ValueChanged(object sender, EventArgs e)
        {
            Area = 10000 * (double)numericUpDownArea.Value;
            UpdateVolume();
        }

        private void numericUpDownDepth_ValueChanged(object sender, EventArgs e)
        {
            Depth = (double)numericUpDownDepth.Value;
            UpdateVolume();
        }

        private void pageWater_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            UserSettings.MemorizedWaterArea = Area;
            UserSettings.MemorizedWaterDepth = Depth;

            UpdateStocks();
        }

        private void pageWater_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) compositionWizard.Replace(this);
        }




        private void spreadSheetStocks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == ColumnN.Index ||
                e.ColumnIndex == ColumnGamingLength.Index ||
                e.ColumnIndex == ColumnGamingAge.Index)
            {
                UpdateGamingStock(e.RowIndex);
            }
        }

        private void pageStocks_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetStocks.Rows)
            {
                string species = (string)gridRow.Cells[ColumnSpecies.Index].Value;

                if (gridRow.Cells[ColumnGamingLength.Index].Value != null)
                {
                    Service.SaveMeasure(species, (double)gridRow.Cells[ColumnGamingLength.Index].Value);
                }
                else if (gridRow.Cells[ColumnGamingAge.Index].Value != null)
                {
                    Service.SaveGamingAge(species, (Age)gridRow.Cells[ColumnGamingAge.Index].Value);
                }
            }
        }



        private void checkBoxCatches_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCPUE.Enabled = checkBoxCatches.Checked && gearWizard.IsMultipleClasses;
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
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
            ((Report)e.Result).Run();

            Log.Write(EventType.WizardEnded, "Extrapolations wizard is finished for with result {1:N2} t.",
                GamingStocks.GetTotalMass());
            pageReport.SetNavigation(true);
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
