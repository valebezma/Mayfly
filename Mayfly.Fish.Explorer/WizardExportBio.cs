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
        public Data Allowed { get; set; }



        private WizardExportBio()
        { 
            InitializeComponent();
        }

        public WizardExportBio(Data data)
            : this()
        {
            Allowed = data;
            //Allowed = new Data(Fish.UserSettings.SpeciesIndex);
            Log.Write(EventType.WizardStarted, "Bio export wizard is started.");
        }



        public Report GetReport()
        {
            Report report = new Report(Resources.Reports.Sections.Export.Title);

            Allowed.GetStack().AddCommon(report);

            report.UseTableNumeration = true;

            if (Allowed.GrowthModels.Count > 0)
            {
                AddGrowth(report);
            }

            if (Allowed.MassModels.Count > 0)
            {
                AddMass(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddGrowth(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Export.Subtitle1);

            report.AddParagraph(Resources.Reports.Sections.Export.Paragraph1,
                report.NextTableNumber);
            report.AddEquation(@"L = {L_∞} (1 - e^{-K (t - {t_0})})");

            Report.Table table = new Report.Table(Resources.Reports.Sections.Export.Table1);

            table.StartRow();
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .4);
            table.AddHeaderCell("RSE");
            table.AddHeaderCell("n");
            table.AddHeaderCell("L<sub>∞</sub>");
            table.AddHeaderCell("K");
            table.AddHeaderCell("t<sub>0</sub>");
            table.EndRow();

            foreach (var model in Allowed.GrowthModels)
            {
                if (!model.InternalData.IsRegressionOK) continue;

                Growth regression = (Growth)model.InternalData.Regression;

                table.StartRow();
                //table1.AddCell(model.InternalData.Name);
                table.AddCell(model.Species, regression.ResidualStandardError < .9);
                table.AddCellRight(regression.ResidualStandardError, ColumnGrowthRSE.DefaultCellStyle.Format);
                table.AddCellRight(model.InternalData.Data.Count, ColumnGrowthN.DefaultCellStyle.Format);
                table.AddCellRight(regression.Linf, ColumnGrowthL.DefaultCellStyle.Format, regression.ResidualStandardError < .9);
                table.AddCellRight(regression.K, ColumnGrowthK.DefaultCellStyle.Format, regression.ResidualStandardError < .9);
                table.AddCellRight(regression.T0, ColumnGrowthT.DefaultCellStyle.Format, regression.ResidualStandardError < .9);
                table.EndRow();
            }

            report.AddTable(table);

            report.AddComment(Resources.Reports.Sections.Export.Comment, true);
        }

        public void AddMass(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Export.Subtitle2);

            report.AddParagraph(Resources.Reports.Sections.Export.Paragraph2,
                report.NextTableNumber);
            report.AddEquation(@"W = {q}\times {L^{b}}");

            Report.Table table = new Report.Table(Resources.Reports.Sections.Export.Table2);

            table.StartRow();
            table.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .4);
            table.AddHeaderCell("r²");
            table.AddHeaderCell("n");
            table.AddHeaderCell("q");
            table.AddHeaderCell("b");
            table.EndRow();

            foreach (var model in Allowed.MassModels)
            {
                if (!model.InternalData.IsRegressionOK) continue;

                Power regression = (Power)model.InternalData.Regression;

                table.StartRow();
                //table1.AddCell(model.InternalData.Name);
                table.AddCell(model.Species, regression.RSquared > .9);
                table.AddCellRight(regression.RSquared, ColumnWeightR2.DefaultCellStyle.Format);
                table.AddCellRight(model.InternalData.Data.Count, ColumnWeightN.DefaultCellStyle.Format);
                table.AddCellRight(regression.Intercept, ColumnWeightQ.DefaultCellStyle.Format, regression.RSquared > .9);
                table.AddCellRight(regression.Slope, ColumnWeightB.DefaultCellStyle.Format, regression.RSquared > .9);
                table.EndRow();
            }

            report.AddTable(table);

            report.AddComment(Resources.Reports.Sections.Export.Comment, true);
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            spreadSheetSigns.Rows.Clear();

            int allowed = 0;

            foreach (string investigator in Allowed.GetStack().GetInvestigators())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSigns);

                gridRow.Cells[ColumnSignInvestigator.Index].Value = investigator;
                int investigatorCount = Allowed.GetStack().GetStack("Investigator", investigator).Count;
                gridRow.Cells[ColumnSignN.Index].Value = investigatorCount;
                if (investigator != Mayfly.Resources.Interface.InvestigatorNotApproved) allowed += investigatorCount;

                spreadSheetSigns.Rows.Add(gridRow);

                gridRow.DefaultCellStyle.ForeColor = investigator == Mayfly.Resources.Interface.InvestigatorNotApproved ?
                    Constants.InfantColor : spreadSheetSigns.DefaultCellStyle.ForeColor;
            }

            pageSigns.AllowNext = allowed > 0;
        }

        private void pageSigns_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageGrowth.SetNavigation(false);
            Cursor = Cursors.WaitCursor;
            calcGrowth.RunWorkerAsync();
        }

        private void modelCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Allowed = Allowed.GetBio();
        }

        private void modelCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (ContinuousBio bio in Allowed.GrowthModels)
            {
                if (!bio.InternalData.IsRegressionOK) continue;

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGrowth);

                gridRow.Cells[ColumnGrowthSpecies.Index].Value = bio.Species;
                gridRow.Cells[ColumnGrowthN.Index].Value = bio.InternalData.Data.Count;

                Growth regression = (Growth)bio.InternalData.Regression;

                gridRow.Cells[ColumnGrowthRSE.Index].Value = regression.ResidualStandardError;

                gridRow.DefaultCellStyle.ForeColor = regression.ResidualStandardError < .9 ?
                    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

                gridRow.Cells[ColumnGrowthL.Index].Value = regression.Linf;
                //gridRow.Cells[ColumnGrowthL.Index].Style.ForeColor = regression.IsSignificant(0) ?
                //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

                gridRow.Cells[ColumnGrowthK.Index].Value = regression.K;
                //gridRow.Cells[ColumnGrowthK.Index].Style.ForeColor = regression.IsSignificant(1) ?
                //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

                gridRow.Cells[ColumnGrowthT.Index].Value = regression.T0;
                //gridRow.Cells[ColumnGrowthT.Index].Style.ForeColor = regression.IsSignificant(2) ?
                //    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

                spreadSheetGrowth.Rows.Add(gridRow);
            }

            labelNoDataGrowth.Visible = spreadSheetGrowth.Rows.Count == 0;


            foreach (ContinuousBio bio in Allowed.MassModels)
            {
                if (!bio.InternalData.IsRegressionOK) continue;

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetWeight);

                gridRow.Cells[ColumnWeightSpecies.Index].Value = bio.Species;
                gridRow.Cells[ColumnWeightN.Index].Value = bio.InternalData.Data.Count;

                Power regression = (Power)bio.InternalData.Regression;

                gridRow.Cells[ColumnWeightR2.Index].Value = regression.RSquared;

                gridRow.DefaultCellStyle.ForeColor = regression.RSquared > .9 ?
                    spreadSheetGrowth.DefaultCellStyle.ForeColor : Constants.InfantColor;

                gridRow.Cells[ColumnWeightQ.Index].Value = regression.Intercept;
                //gridRow.Cells[ColumnWeightQ.Index].Style.ForeColor = regression.IsSignificant(0) ?
                //    spreadSheetWeight.DefaultCellStyle.ForeColor : Constants.InfantColor;

                gridRow.Cells[ColumnWeightB.Index].Value = regression.Slope;
                //gridRow.Cells[ColumnWeightB.Index].Style.ForeColor = regression.IsSignificant(1) ?
                //    spreadSheetWeight.DefaultCellStyle.ForeColor : Constants.InfantColor;

                spreadSheetWeight.Rows.Add(gridRow);
            }

            labelWait.Visible = false;
            Cursor = Cursors.Default;
            labelNoDataWeight.Visible = spreadSheetWeight.Rows.Count == 0;
            pageGrowth.SetNavigation(true);
        }

        private void contextGrowthChart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetGrowth.SelectedRows)
            {
                Data.SpeciesRow spc = (Data.SpeciesRow)gridRow.Cells[ColumnGrowthSpecies.Index].Value;

                Scatterplot scatter = new Scatterplot(Allowed.FindGrowthModel(spc.Species).InternalData);
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
                Data.SpeciesRow spc = (Data.SpeciesRow)gridRow.Cells[ColumnGrowthSpecies.Index].Value;

                Scatterplot scatter = new Scatterplot(Allowed.FindMassModel(spc.Species).InternalData);
                scatter.Properties.ShowTrend = true;
                scatter.Properties.SelectedApproximationType = TrendType.Growth;
                scatter.Properties.ShowPredictionBands = true;
                scatter.Properties.HighlightOutliers = true;
                scatter.Properties.ShowCount = true;
                scatter.ShowOnChart();
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
                else
                {
                    Close();
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