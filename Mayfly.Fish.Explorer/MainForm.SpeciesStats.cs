using Mayfly.Mathematics.Charts;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Wild;
using Mayfly.Mathematics.Statistics;
using System.Drawing;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        Data.SpeciesRow selectedStatSpc;

        FishSamplerType selectedTechSamplerType;

        Histogramma histSample;
        Histogramma histWeighted;
        Histogramma histRegistered;
        Histogramma histAged;

        DataQualificationWay selectedQualificationWay;

        ContinuousBio model;

        Scatterplot ext;
        Scatterplot inter;
        Scatterplot combi;

        private void GetFilteredList(DataGridViewColumn gridColumn)
        {
            if (selectedStatSpc == null)
            {
                spreadSheetInd.EnsureFilter(gridColumn, null, loaderInd,
                    menuItemIndAll_Click);
            }
            else
            {
                spreadSheetInd.EnsureFilter(new DataGridViewColumn[] { columnIndSpecies, gridColumn },
                    new string[] { selectedStatSpc.Species, null }, loaderInd,
                    menuItemIndAll_Click);
            }
        }
        


        private void ClearSpcStats()
        {
            textBoxSpcLog.Text = Constants.Null;
            textBoxSpcL.Text = Constants.Null;
            textBoxSpcW.Text = Constants.Null;
            textBoxSpcA.Text = Constants.Null;
            textBoxSpcS.Text = Constants.Null;
            textBoxSpcM.Text = Constants.Null;
            textBoxSpcStrat.Text = Constants.Null;
            textBoxSpcWTotal.Text = Constants.Null;
            textBoxSpcWLog.Text = Constants.Null;
            textBoxSpcWStrat.Text = Constants.Null;
            textBoxSpsTotal.Text = Constants.Null;
            chartSpcStats.Series[0].Points.Clear();
        }

        private DataPoint GetSpeciesDataPoint(Data.SpeciesRow speciesRow, bool useMass)
        {
            DataPoint dataPoint = new DataPoint();
            double quantity = AllowedStack.Quantity(speciesRow);
            dataPoint.SetCustomProperty("Species", speciesRow.Species);
            if (useMass)
            {
                double mass = AllowedStack.Mass(speciesRow);
                dataPoint.YValues[0] = mass;
            }
            else
            {
                dataPoint.YValues[0] = quantity;
            }
            dataPoint.LegendText = speciesRow.Species;

            return dataPoint;
        }

        private DataGridViewRow GetTechRow(CardStack stack)
        {
            string format = "{0} ({1})";

            DataGridViewRow result = new DataGridViewRow();
            result.CreateCells(spreadSheetTech);
            result.Cells[ColumnSpcTechClass.Index].Value = stack.Name;
            result.Cells[ColumnSpcTechOps.Index].Value = stack.Count;
            double quantity = (selectedStatSpc == null) ? stack.Quantity() : stack.Quantity(selectedStatSpc);

            if (quantity > 0)
            {
                result.Cells[ColumnSpcTechN.Index].Value = quantity;

                int totals = 0;
                int strats = 0;
                //int mixed = 0;
                int logged = 0;

                int totals_q = 0;
                int strats_q = 0;
                //int mixed_q = 0;
                int logged_q = 0;

                foreach (Data.CardRow cardRow in stack)
                {
                    int stratified = 0;
                    int individuals = 0;

                    if (selectedStatSpc == null)
                    {
                        bool all_totalled = true;

                        foreach (Data.LogRow logRow in cardRow.GetLogRows())
                        {
                            if (logRow.IsQuantityNull() || logRow.IsMassNull()) { all_totalled = false; break; }

                            if (!logRow.IsQuantityNull()) totals_q += logRow.Quantity;
                            stratified += logRow.QuantityStratified;
                            individuals += logRow.QuantityIndividuals;
                        }

                        if (all_totalled) totals++;
                    }
                    else
                    {
                        Data.LogRow logRow = data.Log.FindByCardIDSpcID(cardRow.ID, selectedStatSpc.ID);

                        if (logRow == null) continue;

                        if (!logRow.IsQuantityNull() && !logRow.IsMassNull()) totals++;
                        if (!logRow.IsQuantityNull()) totals_q += logRow.Quantity; 

                        stratified = logRow.QuantityStratified;
                        individuals = logRow.QuantityIndividuals;
                    }

                    //if (stratified > 0 && individuals == 0) { strats++; strats_q += stratified; }
                    //if (stratified > 0 && individuals > 0) { mixed++; mixed_q += stratified + individuals; }
                    //if (stratified == 0 && individuals > 0) { logged++; logged_q += individuals; }

                    if (stratified > 0) { strats++; }
                    if (individuals > 0) { logged++; }
                    
                    strats_q += stratified;
                    logged_q += individuals;
                }

                if (totals > 0) result.Cells[ColumnSpcTechTotals.Index].Value = string.Format(format, totals, totals_q);
                if (strats > 0) result.Cells[ColumnSpcTechStratified.Index].Value = string.Format(format, strats, strats_q);
                //if (mixed > 0) result.Cells[ColumnSpcTechMixed.Index].Value = string.Format(format, mixed, mixed_q);
                if (logged > 0) result.Cells[ColumnSpcTechLog.Index].Value = string.Format(format, logged, logged_q);
            }

            return result;
        }

        private void initializeSpeciesStatsPlot()
        {
            //plotQualify.Remove(Resources.Interface.Interface.StratesSampled, false);
            //plotQualify.Remove(Resources.Interface.Interface.StratesWeighted, false);
            //plotQualify.Remove(Resources.Interface.Interface.StratesRegistered, false);
            //plotQualify.Remove(Resources.Interface.Interface.StratesAged, false);

            plotQualify.AxisXInterval = Fish.UserSettings.DefaultStratifiedInterval;

            histSample = new Histogramma(Resources.Interface.Interface.StratesSampled);
            histWeighted = new Histogramma(Resources.Interface.Interface.StratesWeighted);
            histRegistered = new Histogramma(Resources.Interface.Interface.StratesRegistered);
            histAged = new Histogramma(Resources.Interface.Interface.StratesAged);

            Color startColor = Color.FromArgb(150, Color.Lavender);

            foreach (Histogramma hist in new Histogramma[] { histSample, histWeighted, histRegistered, histAged })
            {
                hist.Properties.Borders = false;
                hist.Properties.DataPointColor = startColor;
                hist.Properties.PointWidth = .5;
                startColor = startColor.Darker();
            }

            foreach (Histogramma hist in new Histogramma[] { histSample, histWeighted, histRegistered, histAged })
            {
                plotQualify.AddSeries(hist);
                //if (hist.Series != null) hist.Series.SetCustomProperty("DrawSideBySide", "False");
            }

            //plotQualify.Remove("Bio", false);
            //plotQualify.Remove("Own data", false);
            //plotQualify.Remove("Model", false);

            ext = new Scatterplot("Bio");
            ext.Properties.DataPointColor = Constants.InfantColor;
            plotQualify.AddSeries(ext);

            inter = new Scatterplot("Own data");
            inter.Properties.DataPointColor = Constants.MotiveColor;
            plotQualify.AddSeries(inter);
            inter.Updated += inter_Updated;

            combi = new Scatterplot("Model");
            combi.Properties.ShowTrend = true;
            combi.Properties.ConfidenceLevel = .99;
            combi.Properties.ShowPredictionBands = true;
            combi.Properties.HighlightOutliers = checkBoxQualOutliers.Checked;
            combi.Properties.DataPointColor = Color.Transparent;
            combi.Properties.TrendColor = Constants.MotiveColor;
            plotQualify.AddSeries(combi);
            combi.Updated += combi_Updated;
        }

        private void inter_Updated(object sender, ScatterplotEventArgs e)
        {
            combi.Properties.TrendColor = inter.Properties.DataPointColor.Darker();
        }

        private void combi_Updated(object sender, ScatterplotEventArgs e)
        {
            checkBoxQualOutliers.Checked = combi.Properties.HighlightOutliers;
        }
    }

    public enum DataQualificationWay
    {
        None = -1,
        WeightOfLength = 0,
        AgeOfLength = 1
    }
}
