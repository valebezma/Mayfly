using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer;
using Mayfly.Mathematics.Charts;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardVirtualPopulation : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard
        {
            get;
            set;
        }

        //public CardStack Allowed { get; set; }

        //public FishingGearType SelectedSamplerType { get; set; }

        public Data.SpeciesRow SpeciesRow;

        public AgeComposition Structure { get; internal set; }

        public CardStack[] AnnualStacks { get; internal set; }

        public Composition[] AnnualCompositions { get; internal set; }

        public VirtualPopulation Population 
        {
            get; internal set;
        }

        public List<AgeComposition> Survivors { get; internal set; }

        public Cohort ModelCohort { get; set; }

        public event EventHandler Calculated;

        public event EventHandler Returned;



        private WizardVirtualPopulation()
        {
            InitializeComponent();

            this.RestoreAllCheckStates();
        }

        public WizardVirtualPopulation(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            Log.Write(EventType.WizardStarted, "VPA wizard is started for {0}.", 
                speciesRow.Species);
        }



        public void Run()
        {
            pageStart.Suppress = true;
            wizardExplorer.NextPage();

            //gearWizard = new WizardGearSet(Data);
            //gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            //gearWizard.Replace(this);
        }

        public Report GetReport()
        {
            Report report = new Report(
                string.Format(Resources.Reports.Sections.VPA.Title,
                SpeciesRow.KeyRecord.FullNameReport));
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxCatchHistory.Checked)
            {
                report.AddSectionTitle(Resources.Reports.Sections.VPA.Header1);
                AddHistory(report);
            }

            //if (checkBoxMortality.Checked)
            //{
            //    report.AddSectionTitle(Resources.Reports.Sections.VPA.Header2);
            //}

            if (checkBoxVpa.Checked)
            {
                report.AddSectionTitle(Resources.Reports.Sections.VPA.Header3);
                AddVpa(report);
            }

            if (checkBoxAppVpa.Checked)
            {
                report.BreakPage();

                report.AddHeader(Resources.Reports.Sections.VPA.TitleAppendices);

                foreach (Cohort coh in Population)
                {
                    if (coh.TotalQuantity == 0) continue;

                    report.AddTable(coh.GetTable());
                }
            }

            report.EndBranded();

            return report;
        }

        public void AddHistory(Report report)
        {
            // Year-based catches
            report.AddParagraph(Resources.Reports.Sections.VPA.Paragraph1,
                SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

            report.AddAppendix(
                AnnualCompositions.GetTable(
                    CompositionColumn.Quantity,
                    string.Format(Resources.Reports.Sections.VPA.Table1, SpeciesRow.KeyRecord.FullNameReport, AnnualCompositions[0].Name, AnnualCompositions.Last().Name),
                    Resources.Reports.Sections.Growth.Column1, Resources.Reports.Sections.VPA.Column2)
                    );

            //// Cohort-arranged catches
            //report.AddParagraph(Resources.Reports.Sections.VPA.Paragraph2,
            //    SpeciesRow.GetReportFullPresentation(), report.NextTableNumber, report.NextTableNumber - 1));

            //Report.Table table1 = new Report.Table(Resources.Reports.Sections.VPA.Table2,
            //    SpeciesRow.GetReportFullPresentation()));
            //Population.ToArray().AddCompositionTable(report, Resources.Reports.Sections.Growth.Column1,
            //    Resources.Reports.Sections.GrowthCohorts.Column1, ValueVariant.Quantity, "N0");
        }

        public void AddVpa(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.VPA.Paragraph3,
                Population.NaturalMortality, ModelCohort.Name, report.NextTableNumber);

            report.AddTable(ModelCohort.GetTable());

            report.AddComment(string.Format(Resources.Reports.Sections.VPA.NoticeF, numericUpDownF.Value), true);

            // Overall totals

            report.AddParagraph(Resources.Reports.Sections.VPA.Paragraph4,
                report.NextTableNumber);

            report.AddTable(
                Survivors.GetTable(
                    CompositionColumn.Quantity,
                    string.Format(Resources.Reports.Sections.VPA.Table4, SpeciesRow.KeyRecord.FullNameReport),
                    Resources.Reports.Sections.Growth.Column1, 
                    Resources.Reports.Sections.VPA.Column2)
                );

            report.AddParagraph(Resources.Reports.Sections.VPA.Paragraph5,
                SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

            report.AddTable(
                Survivors.GetTable(
                    CompositionColumn.Mass,
                    string.Format(Resources.Reports.Sections.VPA.Table5, SpeciesRow.KeyRecord.FullNameReport), 
                    Resources.Reports.Sections.Growth.Column1, 
                    Resources.Reports.Sections.VPA.Column2)
                );
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            gearWizard = new WizardGearSet(Data, SpeciesRow);
            gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            gearWizard.Returned += gearWizard_Returned;
            gearWizard.Replace(this);
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            if (Returned != null)
            {
                Returned.Invoke(sender, e);
            }
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            this.Replace(gearWizard);

            plotYield.Clear();
            spreadSheetCatches.Rows.Clear();
            spreadSheetCohorts.Rows.Clear();

            pageYield.SetNavigation(false);
            calcAnnuals.RunWorkerAsync();
        }

        private void calcAnnuals_DoWork(object sender, DoWorkEventArgs e)
        {
            // 1 - Frame for age compositions
            Structure = gearWizard.GearData.GetAgeCompositionFrame(SpeciesRow);

            // 1.1 - Stacks by years
            List<CardStack> annualStacks = new List<CardStack>();
            int[] years = gearWizard.SelectedData.GetYears();
            foreach (int year in years)
            {
                CardStack annualStack = gearWizard.SelectedData.GetStack(year);
                annualStacks.Add(annualStack);
            }
            AnnualStacks = annualStacks.ToArray();
        }

        private void calcAnnuals_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error is AgeArgumentException)
            {
                pageYield.SetNavigation(true);
                pageYield.AllowNext = false;
                return;
            }

            Structure.SetLines(ColumnCatAge);
            DataGridViewRow totalRow1 = new DataGridViewRow();
            totalRow1.CreateCells(spreadSheetCatches);
            totalRow1.Cells[ColumnCatAge.Index].Value = Mayfly.Resources.Interface.Total;
            spreadSheetCatches.Rows.Add(totalRow1);

            Structure.SetLines(ColumnCohAge);
            Structure.SetLines(ColumnVpaAge);
            Structure.SetLines(ColumnResultAge);

            Structure.SetLines(ColumnStockAge);
            DataGridViewRow totalRow2 = new DataGridViewRow();
            totalRow2.CreateCells(spreadSheetStock);
            totalRow2.Cells[ColumnStockAge.Index].Value = Mayfly.Resources.Interface.Total;
            spreadSheetStock.Rows.Add(totalRow2);

            BivariateSample yield = new BivariateSample();
            BivariateSample sample = new BivariateSample();

            foreach (CardStack annualStack in AnnualStacks)
            {
                double d = Service.GetCatchDate(int.Parse(annualStack.Name)).ToOADate();
                double m = annualStack.Mass(SpeciesRow) / 1000;
                double s = annualStack.MassSampled(SpeciesRow) / 1000;

                yield.Add(d, m);
                if (s > 0) sample.Add(d, s);
            }

            Scatterplot yieldF = new Scatterplot(yield, "Yield");
            yieldF.Series.ChartType = SeriesChartType.Line;
            yieldF.IsChronic = true;

            Scatterplot yieldS = new Scatterplot(sample, "Sample");
            yieldS.Series.ChartType = SeriesChartType.Line;
            yieldS.IsChronic = true;

            plotYield.AddSeries(yieldF);
            plotYield.AddSeries(yieldS);

            plotYield.Remaster();
            plotYield.RefreshAxes();
            //plotYield.RefreshAxes();

            pageYield.SetNavigation(true);
            pageYield.AllowNext = AnnualStacks.Length > 1;
        }


        private void contextItemAnnualExplore_Click(object sender, EventArgs e)
        {
            int n =  0;

            MainForm explorer = new MainForm(AnnualStacks[n]);
            explorer.Show();
        }

        private void pageYield_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
        }

        private void pageYield_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            spreadSheetCatches.ClearInsertedColumns();
            spreadSheetCohorts.ClearInsertedColumns();

            comboBoxCohortModel.Enabled = false;

            pageCatches.SetNavigation(false);
            calcCatches.RunWorkerAsync();
        }


        private void calcCatches_DoWork(object sender, DoWorkEventArgs e)
        {
            AnnualCompositions = gearWizard.GearData.GetAnnualAgeCompositions(
                SpeciesRow, gearWizard.SelectedSamplerType, AnnualStacks, gearWizard.WeightType, gearWizard.SelectedUnit.Variant).ToArray();
            Population = new VirtualPopulation(AnnualCompositions);
        }

        private void calcCatches_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int min = DateTime.Today.Year;
            int max = 0;

            contextAnnuals.Items.Clear();
            
            for (int i = 0; i < AnnualCompositions.Length; i++) // for year of surveying from first to last
            {
                Composition annualComposition = AnnualCompositions[i];
            
                DataGridViewColumn annualCol = spreadSheetCatches.InsertColumn(annualComposition.Name, typeof(double), "N1");
                annualCol.SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int j = 0; j < annualComposition.Count; j++)
                {
                    Category category = annualComposition[j];
                    if (category.Quantity == 0) spreadSheetCatches[annualCol.Index, j].Value = null;
                    else spreadSheetCatches[annualCol.Index, j].Value = ((double)category.Quantity / 1000); // In thousands
                }

                spreadSheetCatches[i + 1, Structure.Count].Value = (annualComposition.TotalQuantity) / 1000;

                min = Math.Min(min, int.Parse(annualComposition.Name));
                max = Math.Max(max, int.Parse(annualComposition.Name));

                DataGridViewColumn stockCol = spreadSheetStock.InsertColumn(annualComposition.Name, typeof(double), "N1");
                stockCol.SortMode = DataGridViewColumnSortMode.NotSortable;

                ToolStripMenuItem item = new ToolStripMenuItem(annualComposition.Name)
                {
                    Text = string.Format(Wild.Resources.Interface.Interface.AnnualSurveyItem, annualComposition.Name),
                    Tag = AnnualStacks.First(c => c.Name == annualComposition.Name)
                };
                item.Click += annual_Click;
                contextAnnuals.Items.Add(item);
            }

            buttonAnnual.Enabled = AnnualCompositions.Length > 0;

            statChartVpa.AxisXMin = new DateTime(min, 1, 1).ToOADate();
            statChartVpa.AxisXMax = new DateTime(max + 1, 1, 1).ToOADate();
            statChartVpa.RefreshAxes();

            //foreach (Cohort cohort in Population)
            //{
            //    DataGridViewColumn cohColumn = spreadSheetCohorts.InsertColumn(cohort.Name, typeof(double), "N1");
            //    cohColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

            //    for (int i = 0; i < cohort.Count; i++)
            //    {
            //        Category category = cohort[i];

            //        if (category.Quantity == 0) spreadSheetCohorts[cohColumn.Index, i].Value = null;
            //        else spreadSheetCohorts[cohColumn.Index, i].Value = ((double)category.Quantity / 1000); // In thousands

            //        if (cohort.GetCatchYear(i) > DateTime.Today.Year)
            //            spreadSheetCohorts[cohColumn.Index, i].Value = Constants.Null;
            //    }
            //}

            comboBoxCohortModel.Items.Clear();
            comboBoxCohortModel.Items.AddRange(Population.ToArray());
            comboBoxCohortModel.Enabled = Population.Count > 0;

            if (comboBoxCohortModel.Enabled)
            {
                comboBoxCohortModel.SelectedItem = Population.GetAutoModel(
                     max, ((AgeGroup)AnnualCompositions.Last().GetLast()).Age);

                comboBoxCohort.DataSource = Population;
                comboBoxCohortChart.DataSource = Population;
            }

            //pageCatches.SetNavigation(true);
            //pageCatches.AllowNext = AnnualCompositions.Length > 0;
            //labelNoData.Visible = AnnualCompositions.Length == 0;

            //double m = Service.GetNaturalMortality(SpeciesRow.Species);
            //if (!double.IsNaN(m)) numericUpDownM.Value = (decimal)m;

            //double f = Service.GetFishingMortality(SpeciesRow.Species);
            //if (!double.IsNaN(f)) numericUpDownF.Value = (decimal)f;

            ////pageCohorts.SetNavigation(true);
            //pageCohorts.AllowNext = Population.Count > 0;
        }

        private void buttonAnnual_Click(object sender, EventArgs e)
        {
            contextAnnuals.Show(buttonAnnual, (Point)buttonAnnual.Size, ToolStripDropDownDirection.AboveRight);
        }

        private void annual_Click(object sender, EventArgs e)
        {
            CardStack annualStack = (CardStack)((ToolStripMenuItem)sender).Tag;
            WizardPopulation wizard = new WizardPopulation(annualStack, SpeciesRow);
            //wizard.SetGearSet(gearWizard);
            wizard.Show();
        }

        private void comboBoxCohortModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelCohort = (Cohort)comboBoxCohortModel.SelectedItem;

            // Highlight cells in catches sheet
            for (int i = 1; i <= spreadSheetCatches.InsertedColumnCount; i++)
            {
                for (int j = 0; j < ModelCohort.Count; j++)
                {
                    // i - year; j - age
                    int year = int.Parse(AnnualCompositions[i - 1].Name);
                    int age = ModelCohort[j].Age.Years;
                    spreadSheetCatches[i, j].Style.ForeColor = (year - age == ModelCohort.Birth) ?
                        Color.Red : spreadSheetCohorts.DefaultCellStyle.ForeColor;
                }
            }


            // Highlight column in cohorts sheet
            foreach (DataGridViewColumn gridColumn in spreadSheetCohorts.GetInsertedColumns())
            {
                gridColumn.DefaultCellStyle.ForeColor = spreadSheetCohorts.DefaultCellStyle.ForeColor;
            }

            DataGridViewColumn cohortColumn = spreadSheetCohorts.GetColumn(ModelCohort.Name);
            if (cohortColumn != null) cohortColumn.DefaultCellStyle.ForeColor = Color.Red;

            pictureBoxWarn.Visible = labelWarn.Visible = (ModelCohort.Count > AnnualCompositions.Length);           

            // Perform VPA
            vpa_Changed(sender, e);
        }

        private void spreadSheetCatches_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!spreadSheetCatches.ContainsFocus) return;

            for (int i = 0; i < AnnualCompositions.Length; i++) // for year of surveying from first to last
            {
                Composition annualComposition = AnnualCompositions[i];
                DataGridViewColumn annualCol = spreadSheetCatches.GetColumn(annualComposition.Name);

                if (annualCol.Index != e.ColumnIndex) continue;

                for (int j = 0; j < annualComposition.Count; j++)
                {
                    if (e.RowIndex != j) continue;

                    Category category = annualComposition[j];

                    if (spreadSheetCatches[annualCol.Index, j].Value == null)
                    {
                        category.Quantity = 0;
                        category.Mass = 0;
                    }
                    else
                    {
                        double avgMass = category.Mass / (double)category.Quantity;
                        category.Quantity = (int)((double)spreadSheetCatches[annualCol.Index, j].Value * 1000); // Thousands to singles
                        category.Mass = category.Quantity * avgMass;
                    }

                    break;
                }

                spreadSheetCatches[e.ColumnIndex, Structure.Count].Value = (annualComposition.TotalQuantity) / 1000;

                break;
            }
        }

        private void pageCatches_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            // Insert new quantity values to Annual Compositions


            foreach (Cohort cohort in Population)
            {
                DataGridViewColumn cohColumn = spreadSheetCohorts.InsertColumn(cohort.Name, typeof(double), "N1");
                cohColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < cohort.Count; i++)
                {
                    Category category = cohort[i];

                    if (category.Quantity == 0) spreadSheetCohorts[cohColumn.Index, i].Value = null;
                    else spreadSheetCohorts[cohColumn.Index, i].Value = ((double)category.Quantity / 1000); // In thousands

                    if (cohort.GetCatchYear(i) > DateTime.Today.Year)
                        spreadSheetCohorts[cohColumn.Index, i].Value = Constants.Null;
                }
            }

            //comboBoxCohortModel.Items.Clear();
            //comboBoxCohortModel.Items.AddRange(Population.ToArray());
            //comboBoxCohortModel.Enabled = Population.Count > 0;

            //if (comboBoxCohortModel.Enabled)
            //{
            //    comboBoxCohortModel.SelectedItem = Population.GetAutoModel(
            //         max, ((AgeGroup)AnnualCompositions.Last().GetLast()).Age);

            //    comboBoxCohort.DataSource = Population;
            //    comboBoxCohortChart.DataSource = Population;
            //}

            pageCatches.SetNavigation(true);
            pageCatches.AllowNext = AnnualCompositions.Length > 0;
            labelNoData.Visible = AnnualCompositions.Length == 0;

            double m = Service.GetNaturalMortality(SpeciesRow.Species);
            if (!double.IsNaN(m)) numericUpDownM.Value = (decimal)m;

            double f = Service.GetFishingMortality(SpeciesRow.Species);
            if (!double.IsNaN(f)) numericUpDownF.Value = (decimal)f;

            //pageCohorts.SetNavigation(true);
            pageCohorts.AllowNext = Population.Count > 0;
        }


        private void vpa_Changed(object sender, EventArgs e)
        {
            Population.SetParameters(0.0, (double)numericUpDownF.Maximum);
            Population.NaturalMortality = (double)numericUpDownM.Value;
            Population.TerminalFisheryMortality = (double)numericUpDownF.Value;
            Population.Run(ModelCohort);

            statChartVpa.AxisYMax = Population.GetMaximumSurvivals() / 1000;
            statChartVpa.RecalculateAxesProperties();

            for (int i = 0; i < ModelCohort.Count; i++)
            {
                spreadSheetVpa[ColumnVpaYear.Index, i].Value = ModelCohort.GetCatchYear(i);

                if (ModelCohort.GetCatchYear(i) > DateTime.Today.Year)
                {
                    spreadSheetVpa[ColumnVpaCatch.Index, i].Value = Constants.Null;
                    spreadSheetVpa[ColumnVpaF.Index, i].Value = Constants.Null;
                    spreadSheetVpa[ColumnVpaN.Index, i].Value = Constants.Null;
                }
                else
                {
                    if (ModelCohort[i].Quantity > 0)
                    {
                        spreadSheetVpa[ColumnVpaCatch.Index, i].Value = (double)ModelCohort[i].Quantity / 1000;
                        spreadSheetVpa[ColumnVpaF.Index, i].Value = ModelCohort.GetFisheryMortality(i);
                        spreadSheetVpa[ColumnVpaN.Index, i].Value = (double)ModelCohort.Survivors[i].Quantity / 1000;
                    }
                    else
                    {
                        spreadSheetVpa[ColumnVpaCatch.Index, i].Value = null;
                        spreadSheetVpa[ColumnVpaF.Index, i].Value = null;
                        spreadSheetVpa[ColumnVpaN.Index, i].Value = null;
                    }
                }

                spreadSheetVpa.Rows[i].DefaultCellStyle.ForeColor = ModelCohort[i].Quantity > 0 ?
                    spreadSheetVpa.DefaultCellStyle.ForeColor : Constants.InfantColor;
            }
        }

        private void pageVpa_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            Service.SaveNaturalMortality(SpeciesRow.Species, (double)numericUpDownM.Value);
            Service.SaveFishingMortality(SpeciesRow.Species, (double)numericUpDownF.Value);

            Survivors = new List<AgeComposition>();

            for (int i = 0; i < AnnualCompositions.Length; i++) // for year of surveying from first to last
            {
                AgeComposition ag = Population.GetComposition(int.Parse(AnnualCompositions[i].Name));

                for (int j = 0; j < ag.Count; j++) // for age from youngest to oldest
                {
                    if (ag[j].Quantity == 0) spreadSheetStock[i + 1, j].Value = null;
                    else spreadSheetStock[i + 1, j].Value = (double)ag[j].Quantity / 1000; // In thousands
                }

                Survivors.Add(ag);
                spreadSheetStock[i + 1, Structure.Count].Value = (ag.TotalQuantity) / 1000;
            }
        }


        private void comboBoxCohort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cohort cohort = (Cohort)comboBoxCohort.SelectedItem;

            for (int i = 0; i < cohort.Count; i++)
            {
                spreadSheetResults[ColumnResultYear.Index, i].Value = cohort.GetCatchYear(i);

                if (cohort.GetCatchYear(i) > DateTime.Today.Year)
                {
                    spreadSheetResults[ColumnResultC.Index, i].Value = Constants.Null;
                    spreadSheetResults[ColumnResultF.Index, i].Value = Constants.Null;
                    spreadSheetResults[ColumnResultN.Index, i].Value = Constants.Null;
                }
                else
                {
                    if (cohort[i].Quantity > 0)
                    {
                        spreadSheetResults[ColumnResultC.Index, i].Value = (double)cohort[i].Quantity / 1000;
                        spreadSheetResults[ColumnResultF.Index, i].Value = cohort.GetFisheryMortality(i);
                        spreadSheetResults[ColumnResultN.Index, i].Value = (double)cohort.Survivors[i].Quantity / 1000;
                    }
                    else
                    {
                        spreadSheetResults[ColumnResultC.Index, i].Value = null;
                        spreadSheetResults[ColumnResultF.Index, i].Value = null;
                        spreadSheetResults[ColumnResultN.Index, i].Value = null;
                    }
                }

                spreadSheetResults.Rows[i].DefaultCellStyle.ForeColor = cohort[i].Quantity > 0 ?
                    spreadSheetResults.DefaultCellStyle.ForeColor : Constants.InfantColor;
            }
        } 

        private void comboBoxCohortChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cohort cohort = (Cohort)comboBoxCohortChart.SelectedItem;

            statChartVpa.Clear();

            Scatterplot catches = cohort.GetHistory(statChartVpa.IsChronic);
            catches.Properties.ScatterplotName = "Catch";
            catches.Series.ChartType = SeriesChartType.Line;
            statChartVpa.AddSeries(catches);
            catches.Update(this, e);

            Scatterplot size = cohort.GetSurvivorsHistory(statChartVpa.IsChronic);
            size.Properties.ScatterplotName = "Size";
            size.Series.ChartType = SeriesChartType.Line;
            statChartVpa.AddSeries(size);
            size.Update(this, e);

            statChartVpa.Remaster();
        }

        private void pageChart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Calculated != null)
            {
                e.Cancel = true;
                Calculated.Invoke(sender, e);
            }
        }


        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }


        private void update_Click(object sender, EventArgs e)
        {

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
            Log.Write(EventType.WizardEnded, "VPA wizard is finished for {0}. Last years survivors quantity is calculated {1}.",
                SpeciesRow.Species, Survivors.Last().TotalQuantity);
            if (!UserSettings.KeepWizard) Close();
        }


        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
