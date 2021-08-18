using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;


namespace Mayfly.Mathematics.Charts
{
    public partial class HistogramSeparation : Form
    {
        #region Properties

        private Plot ParentChart { get; set; }

        public Histogramma CombinedHistogram { get; set; }

        private Scatterplot CurrentOverallData { get; set; }

        private Scatterplot CurrentSelectedData { get; set; }

        private int GroupNumber { get; set; }

        public bool IsCalculating { get; set; }

        public List<double> InitialFrequences
        {
            get
            {
                List<double> result = new List<double>();
                for (int i = 0; i < CombinedHistogram.DataSeries.Points.Count; i++)
                {
                    result.Add(CombinedHistogram.DataSeries.Points[i].YValues[0]);
                }
                return result;
            }
        }

        private List<double> NextFrequences { get; set; }

        private List<double> CurrentFrequences { get; set; }

        private List<System.Windows.Forms.DataVisualization.Charting.Series> Distributions { get; set; }

        private System.Windows.Forms.DataVisualization.Charting.Series Combined { get; set; }

        public double Total
        {
            get
            {
                double result = 0;
                for (int i = 0; i < GridLog.RowCount; i++)
                {
                    if (GridLog.Rows[i].Cells[ColumnN.Index].Value is double)
                    {
                        result += (double)GridLog.Rows[i].Cells[ColumnN.Index].Value;
                    }
                }
                return result;
            }
        }

        private double CurrentMean
        {
            get
            {
                return (double)GridLog.Rows[GridLog.RowCount - 1].Cells[ColumnMean.Index].Value;
            }
        }

        private double CurrentDeviation
        {
            get
            {
                return (double)GridLog.Rows[GridLog.RowCount - 1].Cells[ColumnSD.Index].Value;
            }
        }

        public event HistogramEventHandler CalculationComleted;

        #endregion

        #region Constructors

        public HistogramSeparation(Histogramma histogram, Plot statChart)
        {
            InitializeComponent();

            Distributions = new List<Series>();

            ParentChart = statChart;
            ParentChart.AppearanceValueChanged += new EventHandler(Properties_ValueChanged);

            CombinedHistogram = histogram;

            CombinedHistogram.ValueChanged += new HistogramEventHandler(CombinedHistogram_ValueChanged);
            CombinedHistogram.StructureChanged += new HistogramEventHandler(CombinedHistogram_ValueChanged);

            statChartPlot.ShowLegend = false;
            statChartPlot.AxisXMin = ParentChart.AxisXMin;
            statChartPlot.AxisXMax = ParentChart.AxisXMax;
            statChartPlot.AxisYTitle = string.Format("Δ ln({0})", ParentChart.AxisYTitle);
            statChartPlot.GetCursor();

            ParentChart.Remaster();
            AutoSeparate();
        }

        #endregion

        #region Methods

        private void AutoSeparate()
        {
            IsCalculating = true;

            Restart();

            double PrevTotal = 0;

            while (Total > PrevTotal)
            {
                PrevTotal = Total;

                statChartPlot.Remove(GroupNumber.ToString(Resources.Interface.SelectedPointsMask));
                GroupNumber++;
                CurrentFrequences = NextFrequences;

                BuildOverAllPlot(CurrentFrequences);
            }

            buttonRestart.Enabled = GroupNumber > 0;
            Finish();
        }

        private void Restart()
        {
            GridLog.Height = this.Height - 300;
            GridLog.Location = new Point(25, 200);
            GridLog.Rows.Clear();

            labelInstruction.Visible =
                statChartPlot.Visible = true;
            ClearDistributions();

            GroupNumber = 0;
            CurrentFrequences = InitialFrequences;
            BuildOverAllPlot(InitialFrequences);
        }

        private void ClearDistributions()
        {
            while (Distributions.Count > 0)
            {
                ParentChart.Series.Remove(Distributions[0]);
                Distributions.RemoveAt(0);
            }

            ParentChart.Series.Remove(Combined);
        }

        private void Finish()
        {
            if (IsCalculating)
            {
                GridLog.Height = this.Height - 125;
                GridLog.Location = new Point(25, 25);
                labelInstruction.Visible =
                    statChartPlot.Visible = false;
            }
            IsCalculating = false;
        }

        private void BuildOverAllPlot(List<double> RestFrequences)
        {
            statChartPlot.Clear();

            BivariateSample OverAllSample = new BivariateSample();

            for (int i = 1; i < RestFrequences.Count; i++)
            {
                double ClassBottom = CombinedHistogram.DataSeries.Points[i].XValue - ParentChart.AxisXInterval / 2;

                if (RestFrequences[i] > 1 && RestFrequences[i - 1] > 1)
                {
                    OverAllSample.Add(ClassBottom,
                        Math.Log(RestFrequences[i], Math.E) -
                        Math.Log(RestFrequences[i - 1], Math.E));
                }
            }

            if (OverAllSample.Count > 2)
            {
                CurrentOverallData = (Scatterplot)statChartPlot.GetSample(Resources.Interface.BhattacharyaPlot);

                if (CurrentOverallData == null)
                {
                    CurrentOverallData = new Scatterplot(OverAllSample, Resources.Interface.BhattacharyaPlot);
                    CurrentOverallData.Series.IsVisibleInLegend = false;
                    statChartPlot.AddSeries(CurrentOverallData);
                }
                else
                {
                    CurrentOverallData.Data = OverAllSample;
                    CurrentOverallData.BuildSeries();
                }

                CurrentOverallData.Series.MarkerStyle = MarkerStyle.Circle;
                CurrentOverallData.Properties.DataPointSize = 3;
                CurrentOverallData.Properties.DataPointColor = CombinedHistogram.Properties.DataPointColor;
                SetSelection();
                BuildPlotBhattacharya();
            }
            else
            {
                buttonNext.Enabled = false;
                Finish();
            }

            statChartPlot.AxisXMin = ParentChart.AxisXMin;
            statChartPlot.AxisXMax = ParentChart.AxisXMax;
            double Interval = Math.Ceiling(Math.Max(Math.Abs(statChartPlot.AxisYMin), Math.Abs(statChartPlot.AxisYMax)));
            statChartPlot.AxisYMin = -Interval;
            statChartPlot.AxisYMax = Interval;
            statChartPlot.AxisYInterval = 1;
            statChartPlot.AxisYFormat = "0";
            statChartPlot.Remaster();
        }

        private void SetSelection()
        {
            int i = 0;

            while (i < CurrentOverallData.Series.Points.Count - 1)
            {
                if (CurrentOverallData.Series.Points[i].YValues[0] > CurrentOverallData.Series.Points[i + 1].YValues[0])
                {
                    statChartPlot.SelectionCursor.SelectionStart = CurrentOverallData.Series.Points[i].XValue;
                    break;
                }
                else
                {
                    i++;
                }
            }

            while (i < CurrentOverallData.Series.Points.Count - 1)
            {
                if (CurrentOverallData.Series.Points[i].YValues[0] > CurrentOverallData.Series.Points[i + 1].YValues[0])
                {
                    statChartPlot.SelectionCursor.SelectionEnd = CurrentOverallData.Series.Points[i + 1].XValue;
                    i++;
                }
                else
                {
                    break;
                }
            }
        }

        private void BuildPlotBhattacharya()
        {
            List<DataPoint> SelectedPonts = new List<DataPoint>();

            for (int i = 0; i < CurrentOverallData.Series.Points.Count; i++)
            {
                if (CurrentOverallData.Data.X.ElementAt(i) >= statChartPlot.SelectionCursor.SelectionStart &&
                    CurrentOverallData.Data.X.ElementAt(i) <= statChartPlot.SelectionCursor.SelectionEnd)
                {
                    SelectedPonts.Add(CurrentOverallData.Series.Points[i]);
                }
            }

            BuildPlot(SelectedPonts.ToArray());
        }

        private void BuildPlotCaddy()
        {
            List<DataPoint> SelectedPonts = new List<DataPoint>();

            int First = 0;
            int Second = 0;
            int Third = 0;
            double LowestB = double.MaxValue;
            for (int i = 0; i < CurrentOverallData.Series.Points.Count; i++)
            {
                BivariateSample BS = new BivariateSample();
                BS.Add(CurrentOverallData.Data.X.ElementAt(i),
                    CurrentOverallData.Data.Y.ElementAt(i));

                for (int j = 0; j < CurrentOverallData.Series.Points.Count; j++)
                {
                    if (j != i)
                    {
                        BS.Add(CurrentOverallData.Data.X.ElementAt(j),
                           CurrentOverallData.Data.Y.ElementAt(j));

                        for (int k = 0; k < CurrentOverallData.Series.Points.Count; k++)
                        {
                            if (k != i && k != j)
                            {
                                BS.Add(CurrentOverallData.Data.X.ElementAt(k),
                                    CurrentOverallData.Data.Y.ElementAt(k));

                                double R = new Linear(BS).Slope;
                                if (R < LowestB)
                                {
                                    First = i;
                                    Second = j;
                                    Third = k;
                                    LowestB = R;
                                }
                            }
                        }
                    }
                }
            }

            SelectedPonts.Add(CurrentOverallData.Series.Points[First]);
            SelectedPonts.Add(CurrentOverallData.Series.Points[Second]);
            SelectedPonts.Add(CurrentOverallData.Series.Points[Third]);

            BuildPlot(SelectedPonts.ToArray());
        }

        private void BuildPlot(DataPoint[] dataPoints)
        {
            BivariateSample bivariateSample = new BivariateSample();

            foreach (DataPoint dataPoint in dataPoints)
            {
                bivariateSample.Add(dataPoint.XValue, dataPoint.YValues[0]);
            }

            if (bivariateSample.Count > 1)
            {
                CurrentSelectedData = (Scatterplot)statChartPlot.GetSample(GroupNumber.ToString(Resources.Interface.SelectedPointsMask));

                if (CurrentSelectedData == null)
                {
                    CurrentSelectedData = new Scatterplot(bivariateSample, GroupNumber.ToString(Resources.Interface.SelectedPointsMask));
                    CurrentSelectedData.Series.IsVisibleInLegend = false;
                    statChartPlot.AddSeries(CurrentSelectedData);
                    CurrentSelectedData.Properties.ShowTrend = true;
                    CurrentSelectedData.Properties.SelectedApproximationType = TrendType.Linear;
                }
                else
                {
                    CurrentSelectedData.Data = bivariateSample;
                    CurrentSelectedData.BuildSeries();
                }

                CurrentSelectedData.Series.MarkerStyle = MarkerStyle.Circle;
                CurrentSelectedData.Properties.DataPointSize = 5;
                CurrentSelectedData.Properties.DataPointColor = CurrentOverallData.Properties.DataPointColor.Darker();
                //CurrentSelectedData.Trend.Properties.TrendColor = CurrentOverallData.Properties.DataPointColor.Darker();
            }

            statChartPlot.AxisYInterval = 1;
            statChartPlot.AxisYFormat = "0";
            statChartPlot.Remaster();

            CalculateCurrentDistribution();
        }

        private void CalculateCurrentDistribution()
        {
            IsCalculating = true;

            if (CurrentSelectedData == null)
            {
                ParentChart.Remove(GroupNumber.ToString(Resources.Interface.DistributionMask));
                return;
            }

            if (CurrentSelectedData.Trend == null)
            {
                ParentChart.Remove(GroupNumber.ToString(Resources.Interface.DistributionMask));
                return;
            }

            Linear line = (Linear)CurrentSelectedData.Regression;

            buttonNext.Enabled = line.Slope < 0;

            CurrentSelectedData.Trend.Series.IsVisibleInLegend = false;

            if (line.Slope < 0)
            {
                List<double> SelectedFrequences = new List<double>();

                GridLog.ClearSelection();
                if (GridLog.RowCount < GroupNumber + 1)
                {
                    GridLog.Rows[GridLog.Rows.Add()].Selected = true;
                }

                double Mean = -line.Intercept / line.Slope;
                double StdDev = Math.Sqrt(-ParentChart.AxisXInterval / line.Slope);
                double Count = double.NaN;
                double SeparationIndex = double.NaN;

                if (GroupNumber > 0)
                {
                    SeparationIndex =
                        (Mean - (double)GridLog.Rows[GroupNumber - 1].Cells[ColumnMean.Index].Value) /
                        ((StdDev + (double)GridLog.Rows[GroupNumber - 1].Cells[ColumnSD.Index].Value) / 2);
                }

                buttonNext.Enabled = (GroupNumber == 0 || SeparationIndex >= 2);

                if (GroupNumber == 0 || SeparationIndex >= 2)
                {

                    List<double> dLN = new List<double>();
                    List<double> LN = new List<double>();

                    NextFrequences = new List<double>();

                    for (int i = 0; i < CurrentFrequences.Count; i++)
                    {
                        double ClassBottom = CombinedHistogram.DataSeries.Points[i].XValue - ParentChart.AxisXInterval / 2;

                        dLN.Add(line.Intercept +
                            line.Slope * ClassBottom);

                        if (i == 0 || Math.Exp(LN[i - 1] + dLN[i]) == 0)
                        {
                            LN.Add(Math.Log(CurrentFrequences[i], Math.E));
                            SelectedFrequences.Add(CurrentFrequences[i]);
                            NextFrequences.Add(0);
                        }
                        else
                        {
                            if (CombinedHistogram.DataSeries.Points[i].XValue < Mean)
                            {
                                LN.Add(Math.Log(CurrentFrequences[i], Math.E));
                                SelectedFrequences.Add(CurrentFrequences[i]);
                                NextFrequences.Add(0);
                            }
                            else
                            {
                                if (CurrentFrequences[i] < Math.Exp(LN[i - 1] + dLN[i]))
                                {
                                    LN.Add(Math.Log(CurrentFrequences[i], Math.E));
                                    SelectedFrequences.Add(CurrentFrequences[i]);
                                    NextFrequences.Add(0);
                                }
                                else
                                {
                                    LN.Add(LN[i - 1] + dLN[i]);
                                    double CalculatedFrequency = Math.Exp(LN[i]);
                                    SelectedFrequences.Add(CalculatedFrequency);
                                    NextFrequences.Add(CurrentFrequences[i] - CalculatedFrequency);
                                }
                            }
                        }
                    }

                    Count = SelectedFrequences.Sum();
                }
                else
                {
                    ParentChart.Remove(GroupNumber.ToString(Resources.Interface.DistributionMask));
                }

                GridLog.Rows[GroupNumber].Cells[ColumnGroup.Index].Value = GroupNumber.ToString(Resources.Interface.DistributionMask);
                GridLog.Rows[GroupNumber].Cells[ColumnMean.Index].Value = Mean;
                GridLog.Rows[GroupNumber].Cells[ColumnSD.Index].Value = StdDev;
                GridLog.Rows[GroupNumber].Cells[ColumnN.Index].Value = Count;
                GridLog.Rows[GroupNumber].Cells[ColumnSI.Index].Value = SeparationIndex;

                if (buttonNext.Enabled)
                {
                    #region Build or rebuild current distribution

                    Series DistrSeries = ParentChart.Series.FindByName(GroupNumber.ToString(Resources.Interface.DistributionMask));

                    if (DistrSeries == null)
                    {
                        DistrSeries = new Series(GroupNumber.ToString(Resources.Interface.DistributionMask));
                        DistrSeries.ChartType = SeriesChartType.Line;
                        DistrSeries.Color = Color.Black;
                        DistrSeries.BorderWidth = 1;
                        ParentChart.Series.Add(DistrSeries);
                        Distributions.Add(DistrSeries);
                    }
                    else
                    {
                        DistrSeries.Points.Clear();
                    }

                    NormalDistribution distribution = new NormalDistribution(Mean, StdDev);

                    for (double i = ParentChart.AxisXMin - ParentChart.AxisXInterval;
                        i <= ParentChart.AxisXMax;
                        i += (ParentChart.AxisXInterval / 100))
                    {
                        DataPoint _Point = new DataPoint();
                        _Point.XValue = i;
                        double Y = ParentChart.AxisXInterval * Count * distribution.ProbabilityDensity(i);
                        _Point.YValues[0] = Y;
                        DistrSeries.Points.Add(_Point);
                    }

                    #endregion
                }

            }
            else
            {
                ParentChart.Remove(GroupNumber.ToString(Resources.Interface.DistributionMask));
                if (GridLog.RowCount == GroupNumber + 1) GridLog.Rows.RemoveAt(GroupNumber);
            }

            RebuildCombinedDistribution();
        }

        private void RebuildCombinedDistribution()
        {
            Combined = ParentChart.Series.FindByName(Resources.Interface.CombinedDistribution);

            if (Combined == null)
            {
                Combined = new Series(Resources.Interface.CombinedDistribution);
                Combined.ChartType = SeriesChartType.Line;
                Combined.Color = Color.Red;
                Combined.BorderWidth = 2;
                ParentChart.Series.Add(Combined);
            }
            else
            {
                Combined.Points.Clear();
            }

            List<double> Counts = new List<double>();
            List<NormalDistribution> Groups = new List<NormalDistribution>();

            for (int i = 0; i < GridLog.RowCount; i++)
            {
                Counts.Add((double)GridLog.Rows[i].Cells[ColumnN.Index].Value);
                Groups.Add(new NormalDistribution((double)GridLog.Rows[i].Cells[ColumnMean.Index].Value,
                    (double)GridLog.Rows[i].Cells[ColumnSD.Index].Value));
            }

            for (double i = ParentChart.AxisXMin - ParentChart.AxisXInterval;
                i <= ParentChart.AxisXMax;
                i += (ParentChart.AxisXInterval / 100))
            {
                DataPoint _Point = new DataPoint();
                _Point.XValue = i;
                double Y = 0;
                for (int j = 0; j < Groups.Count; j++)
                {
                    Y += ParentChart.AxisXInterval * Counts[j] * Groups[j].ProbabilityDensity(i);
                }
                _Point.YValues[0] = Y;
                Combined.Points.Add(_Point);
            }
        }

        #endregion

        #region Interface logics

        private void HistogramSeparation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                ClearDistributions();
            }
        }

        private void Properties_ValueChanged(object sender, EventArgs e)
        {
            AutoSeparate();
        }

        private void CombinedHistogram_ValueChanged(object sender, HistogramEventArgs e)
        {
            Restart();
        }

        private void statChartPlot_SelectionChanged(object sender, EventArgs e)
        {
            if (GroupNumber == 0)
            {
                CurrentFrequences = InitialFrequences;
                BuildPlotBhattacharya();
            }
            else
            {
                BuildPlotBhattacharya();
            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                AutoSeparate();
            }
            else
            {
                Restart();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            statChartPlot.Remove(GroupNumber.ToString(Resources.Interface.SelectedPointsMask));
            GroupNumber++;
            CurrentFrequences = NextFrequences;
            BuildOverAllPlot(CurrentFrequences);
            buttonRestart.Enabled = GroupNumber > 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CalculationComleted != null)
            {
                CalculationComleted.Invoke(this, new HistogramEventArgs(CombinedHistogram));
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        private void buttonAdjust_Click(object sender, EventArgs e)
        {
            //IsCalculating = true;
        }
    }
}
