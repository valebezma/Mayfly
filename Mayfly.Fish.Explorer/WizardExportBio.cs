using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using System.IO;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardExportBio : Form
    {
        public CardStack Data { get; set; }

        public Data Allowed { get; set; }



        private WizardExportBio()
        { 
            InitializeComponent();
        }

        public WizardExportBio(Data data)
            : this()
        {
            Data = data.GetStack();
            Allowed = new Data();
            Log.Write(EventType.WizardStarted, "Bio export wizard is started.");
        }



        public Report GetReport()
        {
            Report report = new Report(Resources.Reports.Sections.Export.Title);
            
            //report.UseTableNumeration = true;

            //Allowed.GetStack().AddCommon(report);

            //if (Allowed.GrowthModels.InternalData.Count > 0)
            //{
            //    AddGrowth(report);
            //}

            //if (Allowed.MassModels.InternalData.Count > 0)
            //{
            //    AddMass(report);
            //}

            report.EndBranded();

            return report;
        }

        public void AddGrowth(Report report)
        {
            //report.AddParagraph(Resources.Reports.Sections.Export.Paragraph1,
            //    report.NextTableNumber);
            //report.AddEquation(@"L = {L_∞} (1 - e^{-K (t - {t_0})})");

            //Report.Table table1 = new Report.Table(Resources.Reports.Sections.Export.Table1);

            //table1.StartRow();
            //table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .4);
            //table1.AddHeaderCell("R²");
            //table1.AddHeaderCell("N");
            //table1.AddHeaderCell("L<sub>∞</sub>");
            //table1.AddHeaderCell("K");
            //table1.AddHeaderCell("t<sub>0</sub>");
            //table1.EndRow();

            //foreach (Scatterplot scatter in Allowed.GrowthModels.InternalData)
            //{
            //    if (!scatter.IsRegressionOK) continue;

            //    Growth regression = (Growth)scatter.Regression;

            //    table1.StartRow();
            //    table1.AddCell(scatter.Name);
            //    //table1.AddCell(Fish.UserSettings.SpeciesIndex.Species.FindBySpecies(scatter.Name).GetReportFullPresentation(),
            //    //    regression.IsSignificant());
            //    //table1.AddCellRight(scatter.Regression.Determination, ColumnGrowthR2.DefaultCellStyle.Format);
            //    //table1.AddCellRight(scatter.Data.Count, ColumnGrowthN.DefaultCellStyle.Format);
            //    //table1.AddCellRight(regression.L, ColumnGrowthL.DefaultCellStyle.Format, regression.IsSignificant(0));
            //    //table1.AddCellRight(regression.K, ColumnGrowthK.DefaultCellStyle.Format, regression.IsSignificant(1));
            //    //table1.AddCellRight(regression.t0, ColumnGrowthT.DefaultCellStyle.Format, regression.IsSignificant(2));
            //    table1.EndRow();
            //}

            //report.AddTable(table1);

            //report.AddComment(Resources.Reports.Sections.Export.Comment, true);
        }

        public void AddMass(Report report)
        {
            //report.AddParagraph(Resources.Reports.Sections.Export.Paragraph2,
            //    report.NextTableNumber);
            //report.AddEquation(@"W = {q}\times {L^{b}}");

            //Report.Table table1 = new Report.Table(Resources.Reports.Sections.Export.Table2);

            //table1.StartRow();
            //table1.AddHeaderCell(Wild.Resources.Reports.Common.Species, .4);
            //table1.AddHeaderCell("R²");
            //table1.AddHeaderCell("N");
            //table1.AddHeaderCell("q");
            //table1.AddHeaderCell("b");
            //table1.EndRow();

            //foreach (Scatterplot scatter in Allowed.WeightModels.InternalScatterplots)
            //{
            //    if (!scatter.IsRegressionOK) continue;

            //    Power regression = (Power)scatter.Regression;

            //    table1.StartRow();
            //    table1.AddCell(scatter.Name);
            //    //table1.AddCell(Fish.UserSettings.SpeciesIndex.Species.FindBySpecies(scatter.Name).GetReportFullPresentation(),
            //    //    regression.IsSignificant());
            //    table1.AddCellRight(scatter.Regression.Determination, ColumnWeightR2.DefaultCellStyle.Format);
            //    table1.AddCellRight(scatter.Data.Count, ColumnWeightN.DefaultCellStyle.Format);
            //    table1.AddCellRight(regression.a, ColumnWeightQ.DefaultCellStyle.Format, regression.IsSignificant(0));
            //    table1.AddCellRight(regression.b, ColumnWeightB.DefaultCellStyle.Format, regression.IsSignificant(1));
            //    table1.EndRow();
            //}

            //report.AddTable(table1);

            //report.AddComment(Resources.Reports.Sections.Export.Comment, true);
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            spreadSheetSigns.Rows.Clear();

            foreach (string investigator in Data.GetInvestigators())
            {
                CardStack investigatorStack = Data.GetStack("Investigator", investigator);

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSigns);

                gridRow.Cells[ColumnSignInvestigator.Index].Value = investigator;
                gridRow.Cells[ColumnSignN.Index].Value = investigatorStack.Count;

                spreadSheetSigns.Rows.Add(gridRow);

                gridRow.DefaultCellStyle.ForeColor = investigator == Mayfly.Resources.Interface.InvestigatorNotApproved ?
                    Constants.InfantColor : spreadSheetSigns.DefaultCellStyle.ForeColor;
            }

            Allowed = new Data();

            foreach (string investigator in Data.GetInvestigators())
            {
                if (investigator == Mayfly.Resources.Interface.InvestigatorNotApproved) continue;

                foreach (Data.CardRow cardRow in Data.GetStack("Investigator", investigator))
                {
                    cardRow.CopyTo(Allowed);
                }
            }

            pageSigns.AllowNext = Allowed.Card.Count > 0;
        }

        private void pageSigns_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageWeight.SetNavigation(false);
            modelCalculator.RunWorkerAsync();
        }

        private void modelCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Allowed.InitializeBio();
        }

        private void modelCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //foreach (Scatterplot scatter in Allowed.GrowthModels.InternalData)
            //{
            //    if (!scatter.IsRegressionOK) continue;

            //    DataGridViewRow gridRow = new DataGridViewRow();
            //    gridRow.CreateCells(spreadSheetGrowth);

            //    gridRow.Cells[ColumnGrowthSpecies.Index].Value = scatter.Name;
            //    //gridRow.Cells[ColumnGrowthR2.Index].Value = scatter.Regression.Determination;
            //    gridRow.Cells[ColumnGrowthN.Index].Value = scatter.Data.Count;

            //    Growth regression = (Growth)scatter.Regression;

            //    //gridRow.DefaultCellStyle.ForeColor = regression.IsSignificant() ? 
            //    //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    gridRow.Cells[ColumnGrowthL.Index].Value = regression.Linf;
            //    //gridRow.Cells[ColumnGrowthL.Index].Style.ForeColor = regression.IsSignificant(0) ?
            //    //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    gridRow.Cells[ColumnGrowthK.Index].Value = regression.K;
            //    //gridRow.Cells[ColumnGrowthK.Index].Style.ForeColor = regression.IsSignificant(1) ?
            //    //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    gridRow.Cells[ColumnGrowthT.Index].Value = regression.T0;
            //    //gridRow.Cells[ColumnGrowthT.Index].Style.ForeColor = regression.IsSignificant(2) ?
            //    //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    spreadSheetGrowth.Rows.Add(gridRow);
            //}

            //labelNoDataGrowth.Visible = Allowed.GrowthModels.InternalData.Count == 0;


            //foreach (Scatterplot scatter in Allowed.MassModels.InternalData)
            //{
            //    if (!scatter.IsRegressionOK) continue;

            //    DataGridViewRow gridRow = new DataGridViewRow();
            //    gridRow.CreateCells(spreadSheetWeight);

            //    gridRow.Cells[ColumnWeightSpecies.Index].Value = scatter.Name;
            //    //gridRow.Cells[ColumnWeightR2.Index].Value = scatter.Regression.Determination;
            //    gridRow.Cells[ColumnWeightN.Index].Value = scatter.Data.Count;

            //    Power regression = (Power)scatter.Regression;

            //    //gridRow.DefaultCellStyle.ForeColor = regression.IsSignificant() ?
            //    //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    gridRow.Cells[ColumnWeightQ.Index].Value = regression.Intercept;
            //    //gridRow.Cells[ColumnWeightQ.Index].Style.ForeColor = regression.IsSignificant(0) ?
            //    //    spreadSheetWeight.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    gridRow.Cells[ColumnWeightB.Index].Value = regression.Slope;
            //    //gridRow.Cells[ColumnWeightB.Index].Style.ForeColor = regression.IsSignificant(1) ?
            //    //    spreadSheetWeight.DefaultCellStyle.ForeColor : Constants.InfantColor;

            //    spreadSheetWeight.Rows.Add(gridRow);
            //}

            //labelNoDataWeight.Visible = Allowed.MassModels.InternalData.Count == 0;

            //pageWeight.SetNavigation(true);
        }

        private void pageWeight_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            backSpecExporter.RunWorkerAsync();
        }

        private void backSpecExporter_DoWork(object sender, DoWorkEventArgs e)
        {
            Allowed = Allowed.GetBio();
        }

        private void backSpecExporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Notification.ShowNotification(Resources.Interface.Interface.SpecFailed,
                    e.Error.Message);
            }
            else
            {
                pageReport.SetNavigation(true);
            }
        }

        private void contextGrowthChart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetGrowth.SelectedRows)
            {
                string spc = (string)gridRow.Cells[ColumnGrowthSpecies.Index].Value;

                Scatterplot scatter = new Scatterplot(Allowed.FindGrowthModel(spc).InternalData);
                scatter.Properties.ShowTrend = true;
                scatter.Properties.SelectedApproximationType = TrendType.Growth;
                scatter.Properties.ShowPredictionBands = true;
                scatter.Properties.HighlightOutliers = true;
                scatter.Properties.ShowCount = true;
                scatter.ShowOnChart();
            }
        }

        private void contextWeightChart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetWeight.SelectedRows)
            {
                string spc = (string)gridRow.Cells[ColumnGrowthSpecies.Index].Value;

                //Scatterplot scatter = Allowed.MassModels.GetInternalScatterplot(spc).Copy();
                //scatter.Properties.ShowTrend = true;
                //scatter.Properties.SelectedApproximationType = TrendType.Power;
                //scatter.Properties.ShowPredictionBands = true;
                //scatter.Properties.HighlightOutliers = true;
                //scatter.Properties.ShowCount = true;
                //scatter.ShowOnChart();
            }
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
            Wild.UserSettings.InterfaceBio.SaveDialog.FileName = IO.SuggestName(
                Wild.UserSettings.InterfaceBio.SaveDialog.InitialDirectory,
                IO.GetFriendlyCommonName(Allowed.GetFilenames())
                );

            if (Wild.UserSettings.InterfaceBio.SaveDialog.ShowDialog(this) == DialogResult.OK)
            {
                Allowed.ExportBio(Wild.UserSettings.InterfaceBio.SaveDialog.FileName);

                Log.Write("Bios are exported to {0}", Wild.UserSettings.InterfaceBio.SaveDialog.FileName);

                if (checkBoxReport.Checked)
                {
                    reporter.RunWorkerAsync();
                }
            }

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
            Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}