using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Extensions;
using Meta.Numerics;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace Mayfly.Mathematics.Charts
{
    public class Scatterplot : ISerializable, IDisposable
    {
        //public string Name { get { return Properties.ScatterplotName; } }
        public string Name { get; private set; }

        public Plot Container { set; get; }

        public DataGridViewColumn ColumnX { get; set; }

        public DataGridViewColumn ColumnY { get; set; }

        public List<DataGridViewColumn> ColumnsLabels { get; set; }

        public Regression Regression { get; set; }

        public bool IsRegressionOK { get { return Regression != null && Regression.Fit != null; } }

        public BivariateSample Data { get; set; }

        public List<List<string>> Labels { get; set; }

        public Series Series { get; set; }

        public Series DataRange { get; set; }

        public Functor Trend { get; set; }

        public Series TrendRange { get; set; }

        public bool IsChronic { get; set; }

        public ScatterplotProperties Properties { get; set; }

        public double Left { get; set; }

        public double Right { get; set; }

        public double Top { get; set; }

        public double Bottom { get; set; }

        public ChartArea ChartArea
        {
            get
            {
                ChartArea result = Container.ChartAreas.FindByName(Series.ChartArea);
                if (result == null) result = Container.ChartAreas[0];
                return result;
            }

            set
            {
                Series.ChartArea = value.Name;
                if (DataRange != null) DataRange.ChartArea = value.Name;
                if (Trend != null) Trend.ChartArea = value;
                //if (TrendSeries != null) TrendSeries.ChartArea = value.Name;
                if (TrendRange != null) TrendRange.ChartArea = value.Name;
            }
        }

        public CalloutAnnotation TrendAnnotation { get; set; }

        public ScatterplotSeparation Separator { get; set; }

        public event ScatterplotEventHandler ValueChanged;

        public event ScatterplotEventHandler Updated;

        private bool disposed;

        private static readonly int splineStep = 100;

        private TrendType model = TrendType.Auto;

        

        public Scatterplot(DataGridViewColumn xColumn, DataGridViewColumn yColumn)
        {
            ColumnX = xColumn;
            ColumnX.DataGridView.CellValueChanged += new DataGridViewCellEventHandler(CellValueChanged);
            ColumnY = yColumn;
            Labels = new List<List<string>>();
            Data = new BivariateSample(ColumnX.HeaderText, ColumnY.HeaderText);
            Properties = new ScatterplotProperties(this);
            this.Name = Properties.ScatterplotName = yColumn.HeaderText;
            GetData();
            BuildSeries();
        }

        public Scatterplot(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> labelColumns)
        {
            ColumnX = xColumn;
            ColumnX.DataGridView.CellValueChanged += new DataGridViewCellEventHandler(CellValueChanged);

            ColumnY = yColumn;

            ColumnsLabels = labelColumns;

            Labels = new List<List<string>>();
            Data = new BivariateSample(xColumn.HeaderText, yColumn.HeaderText);

            GetData();

            Properties = new ScatterplotProperties(this);
            this.Name = Properties.ScatterplotName = yColumn.HeaderText;

            BuildSeries();
        }

        public Scatterplot(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            string name)
        {
            ColumnX = xColumn;
            ColumnX.DataGridView.CellValueChanged += new DataGridViewCellEventHandler(CellValueChanged);
            ColumnY = yColumn;
            ColumnY.DataGridView.CellValueChanged += new DataGridViewCellEventHandler(CellValueChanged);
            Labels = new List<List<string>>();
            Data = new BivariateSample(xColumn.HeaderText, yColumn.HeaderText);
            GetData();
            Properties = new ScatterplotProperties(this);
            BuildSeries();
            this.Name = Properties.ScatterplotName = name;
        }

        public Scatterplot(BivariateSample bivariateSample, string name, List<List<string>> labels)
        {
            Labels = labels;
            Data = bivariateSample;
            Properties = new ScatterplotProperties(this);
            this.Name = Properties.ScatterplotName = name;
            BuildSeries();
        }

        public Scatterplot(BivariateSample bivariateSample, string name)
        {
            Labels = new List<List<string>>();
            Data = bivariateSample;
            Properties = new ScatterplotProperties(this);
            this.Name = Properties.ScatterplotName = name;
            BuildSeries();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~Scatterplot()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Container != null)
                    {
                        if (!Container.IsDisposed && Series != null)
                        {
                            Container.Series.Remove(Series);
                            Series.Dispose();

                            if (DataRange != null)
                            {
                                Container.Series.Remove(DataRange);
                                DataRange.Dispose();
                            }

                            if (Trend != null)
                            {
                                Container.Series.Remove(Trend.Series);
                                Trend.Dispose();
                            }

                            if (TrendRange != null)
                            {
                                Container.Series.Remove(TrendRange);
                                TrendRange.Dispose();
                            }

                            if (TrendAnnotation != null)
                            {
                                Container.Annotations.Remove(TrendAnnotation);
                                TrendAnnotation.Dispose();
                            }
                        }

                        Container.Scatterplots.Remove(this);
                    }

                    Properties.Dispose();
                    Properties = null;

                    if (ColumnX != null)
                    {
                        if (ColumnX.DataGridView != null) ColumnX.DataGridView.CellValueChanged -= CellValueChanged;
                        ColumnX = null;
                    }

                    ColumnY = null;
                    Labels = null;
                    Data = null;
                }

                disposed = true;
            }
        }


        
        public Scatterplot Copy ()
        {
            Scatterplot result = new Scatterplot(Data.Copy(), Properties.ScatterplotName);

            if (this.Properties.ShowTrend)
            {
                result.CalculateApproximation(this.Regression.Type);
            }

            return result;
        }

        public void Update(object sender, EventArgs e)
        {
            if (Container == null) return;

            this.Name = Properties.ScatterplotName;
            Series.Name = Properties.ScatterplotName;

            if (!Container.IsDistinguishingMode)
            {
                Series.MarkerBorderColor = Properties.DataPointColor;
            }

            Series.MarkerSize = Properties.DataPointSize;
            Series.MarkerBorderWidth = Properties.DataPointBorderWidth;

            if (Properties.ShowTrend)
            {
                if (model == TrendType.Auto || model != Properties.SelectedApproximationType)
                {
                    CalculateApproximation(Properties.SelectedApproximationType);
                }

                if (Regression == null)
                {
                    Properties.ShowTrend = false;
                    Properties.SelectedApproximationType = TrendType.Auto;
                    return;
                }

                model = Properties.SelectedApproximationType;

                this.Regression.Name = Properties.TrendName;

                if (Properties.SelectedApproximationType == TrendType.Auto)
                {
                    Properties.SelectedApproximationType = Regression.Type;
                }

                if (Trend == null)
                {
                    Trend = new Functor(Properties.TrendName, Regression.Predict, Regression.PredictInversed);
                    Container.AddSeries(Trend);
                }
                else
                {
                    Trend.Function = Regression.Predict;
                    Trend.FunctionInverse = Regression.PredictInversed;
                }

                Trend.BuildSeries(Series.YAxisType);

                Trend.Properties.FunctionName = Properties.TrendName;
                Trend.Properties.TrendWidth = Properties.TrendWidth;
                Trend.Properties.TrendColor = Properties.TrendColor;
                Trend.Properties.AllowCursors = Properties.AllowCursors;

                Trend.Update(sender, e);
            }
            else
            {
                if (Trend != null)
                {
                    Container.Series.Remove(Trend.Series);
                    Trend = null;
                }
            }

            if (Properties.ShowConfidenceBands)
            {
                BuildTrendBands();

                if (Container.Series.FindByName(TrendRange.Name) == null)
                {
                    Container.Series.Add(TrendRange);
                }

                TrendRange.BorderColor = Properties.DataPointColor;
                TrendRange.BorderWidth = (int)Math.Ceiling(Properties.TrendWidth / 2M);
            }
            else
            {
                if (TrendRange != null)
                {
                    Container.Series.Remove(TrendRange);
                    TrendRange = null;
                }
            }

            if (Properties.ShowPredictionBands)
            {
                BuildDataBands();

                if (Container.Series.FindByName(DataRange.Name) == null)
                {
                    Container.Series.Add(DataRange);
                }

                DataRange.BorderColor = Properties.DataPointColor;
                DataRange.BorderWidth = (int)Math.Ceiling(Properties.TrendWidth / 2M);
                DataRange.BackHatchStyle = Properties.HighlightRunouts ? ChartHatchStyle.Percent05 : ChartHatchStyle.None;
                DataRange.BackSecondaryColor = Properties.DataPointColor;

                if (Properties.HighlightRunouts) // Colorize runouts
                {
                    //BivariateSample runouts = Regression.GetRunouts(Properties.ConfidenceLevel);

                    //foreach (DataPoint dp in Series.Points)
                    //{
                    //    if (runouts != null && runouts.Contains(dp.XValue, dp.YValues[0]))
                    //    {
                    //        dp.MarkerColor = dp.MarkerBorderColor = Color.Red;
                    //    }
                    //    else
                    //    {
                    //        dp.MarkerColor = dp.MarkerBorderColor = Properties.DataPointColor;
                    //    }
                    //}
                }
                else
                {
                    //foreach (DataPoint dp in Series.Points)
                    //{
                    //    dp.MarkerColor = dp.MarkerBorderColor = Properties.DataPointColor;
                    //}
                }
            }
            else
            {
                if (DataRange != null)
                {
                    Container.Series.Remove(DataRange);
                    DataRange = null;
                }
            }

            if (Properties.ShowAnnotation)
            {
                if (TrendAnnotation == null)
                {
                    TrendAnnotation = new CalloutAnnotation
                    {
                        BackColor = Container.ChartAreas[0].BackColor,
                        Name = Properties.ScatterplotName,
                        //TrendAnnotation.IsSizeAlwaysRelative = false;
                        CalloutStyle = CalloutStyle.Rectangle,
                        Alignment = ContentAlignment.MiddleCenter,
                        AllowMoving = true,
                        AxisX = Container.ChartAreas[0].AxisX,
                        AxisY = Container.ChartAreas[0].AxisY,
                        X = Left + 3 * (Right - Left) / 4
                    };
                    TrendAnnotation.Y = Trend.Predict(TrendAnnotation.X);
                    Container.Annotations.Add(TrendAnnotation);
                }

                TrendAnnotation.Font = Container.Font;
                TrendAnnotation.Visible = true;
                TrendAnnotation.Text = Properties.TrendName;

                if (Properties.ShowCount)
                {
                    TrendAnnotation.Text += Constants.Break + "N = " + Data.Count;
                }

                if (Properties.ShowExplained)
                {
                    //TrendAnnotation.Text += Constants.Break + "R² = " + Regression.Determination.ToString("G4");
                }
            }
            else
            {
                if (TrendAnnotation != null)
                {
                    TrendAnnotation.Visible = false;
                }
            }

            if (Updated != null)
            {
                Updated.Invoke(this, new ScatterplotEventArgs(this));
            }
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ColumnX == null) return;
            if (ColumnY == null) return;

            if (e.ColumnIndex == ColumnX.Index || e.ColumnIndex == ColumnY.Index)
            {
                GetData();

                if (Data != null)
                {
                    BuildSeries();
                }

                if (Properties.ShowTrend)
                {
                    CalculateApproximation(Properties.SelectedApproximationType);

                    Trend.BuildSeries(Series.YAxisType);

                    if (Properties.ShowConfidenceBands)
                    {
                        BuildTrendBands();
                    }

                    if (Properties.ShowPredictionBands)
                    {
                        BuildDataBands();
                    }
                }

                InvokeChanged();
            }
        }

        public void InvokeChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, new ScatterplotEventArgs(this));
            }

            Update(this, new EventArgs());
        }

        public void BuildSeries()
        {
            if (Series == null)
            {
                Series = new Series(Properties.ScatterplotName)
                {
                    ChartType = SeriesChartType.Point,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerColor = System.Drawing.Color.Transparent
                };
            }
            else
            {
                Series.Points.Clear();
            }

            for (int i = 0; i < Data.Count; i++)
            {
                DataPoint dataPoint = new DataPoint(Data.X.ElementAt(i), Data.Y.ElementAt(i));

                //if (IsChronic)
                //{
                //    //dataPoint.ToolTip = Properties.ScatterplotName + Constants.Return +
                //    //    DateTime.FromOADate(dataPoint.XValue).ToString(ChartArea.AxisX.LabelStyle.Format) + Constants.Return +
                //    //    dataPoint.YValues[0];
                //}
                //else
                //{

                if (Container == null)
                {
                    dataPoint.ToolTip = Properties.ScatterplotName + Constants.Return +
                        dataPoint.XValue + Constants.Return + dataPoint.YValues[0];
                }
                else
                {
                    dataPoint.ToolTip = Properties.ScatterplotName + Constants.Return +
                        (IsChronic ? (DateTime.FromOADate(dataPoint.XValue)).ToString(Container.AxisXFormat) : dataPoint.XValue.ToString(Container.AxisXFormat)) + Constants.Return +
                        dataPoint.YValues[0].ToString(Container.AxisYFormat);
                }
                //}

                Series.Points.Add(dataPoint);

                if (Labels.Count > 0)
                {
                    if (Labels[i].Count > 0)
                    {
                        foreach (string label in Labels[i])
                        {
                            if (label.IsAcceptable())
                            {
                                dataPoint.Label += label + Constants.Break;
                            }
                        }
                    }
                }
            }

            if (Data.Count > 0)
            {
                Left = Data.X.Minimum;
                Bottom = Data.Y.Minimum;
                Top = Data.Y.Maximum;
                Right = Data.X.Maximum;
            }
        }

        public void BuildTrendBands()
        {
            BuildTrendBands(Container.AxisXMin, Container.AxisXMax,
                Container.AxisYMin, Container.AxisYMax);
        }

        public void BuildTrendBands(double xMin, double xMax, double yMin, double yMax)
        {
            if (this.IsRegressionOK)
            {
                if (TrendRange == null)
                {
                    TrendRange = new Series(string.Format(Resources.Interface.ConfidenceBands, Properties.ScatterplotName))
                    {
                        IsVisibleInLegend = false,
                        ChartType = SeriesChartType.SplineRange,
                        BorderDashStyle = ChartDashStyle.Dash,
                        Color = Color.Transparent,
                        YValuesPerPoint = 2
                    };
                }
                else
                {
                    TrendRange.Points.Clear();
                }

                double xInterval = (xMax - xMin) / splineStep;
                double yInterval = (yMax - yMin) / splineStep;

                for (double x = xMin - xInterval; x <= xMax + xInterval; x += xInterval)
                {
                    //UncertainValue s = Regression.GetConfidenceInterval(x, Properties.ConfidenceLevel);

                    //double lowerY = s.Value - s.Uncertainty;
                    //if (double.IsInfinity(lowerY)) continue;
                    //if (double.IsNaN(lowerY)) continue;

                    //double upperY = s.Value + s.Uncertainty;
                    //if (double.IsInfinity(upperY)) continue;
                    //if (double.IsNaN(upperY)) continue;

                    //DataPoint dataPoint = new DataPoint(TrendRange);
                    //dataPoint.XValue = x;
                    //dataPoint.YValues[0] = lowerY;
                    //dataPoint.YValues[1] = upperY;

                    //TrendRange.Points.Add(dataPoint);
                }

                if (TrendRange != null) TrendRange.YAxisType = Series.YAxisType;
            }
            else
            {
                if (TrendRange != null) TrendRange.Points.Clear();
            }
        }

        public void BuildDataBands()
        {
            BuildDataBands(Container.AxisXMin, Container.AxisXMax, 
                Container.AxisYMin, Container.AxisYMax);
        }

        public void BuildDataBands(double xMin, double xMax, double yMin, double yMax)
        {
            if (this.IsRegressionOK)
            {
                if (DataRange == null)
                {
                    DataRange = new Series(string.Format(Resources.Interface.PredictionBands, Properties.ScatterplotName))
                    {
                        ChartType = SeriesChartType.SplineRange,
                        BorderDashStyle = ChartDashStyle.Dash,
                        IsVisibleInLegend = false,
                        Color = Color.Transparent,
                        YValuesPerPoint = 2
                    };
                }
                else
                {
                    DataRange.Points.Clear();
                }

                double xInterval = (xMax - xMin) / splineStep;
                double yInterval = (yMax - yMin) / splineStep;

                for (double x = xMin - xInterval; x <= xMax + xInterval; x += xInterval)
                {
                    //UncertainValue s = Regression.GetPredictionInterval(x, Properties.ConfidenceLevel);

                    //double lowerY = s.Value - s.Uncertainty;
                    //if (double.IsInfinity(lowerY)) continue;
                    //if (double.IsNaN(lowerY)) continue;

                    //double upperY = s.Value + s.Uncertainty;
                    //if (double.IsInfinity(upperY)) continue;
                    //if (double.IsNaN(upperY)) continue;

                    //DataPoint dataPoint = new DataPoint(DataRange);
                    //dataPoint.XValue = x;
                    //dataPoint.YValues[0] = lowerY;
                    //dataPoint.YValues[1] = upperY;

                    //DataRange.Points.Add(dataPoint);
                }

                if (DataRange != null) DataRange.YAxisType = Series.YAxisType;
            }
            else
            {
                if (DataRange != null) DataRange.Points.Clear();
            }
        }

        public Plot ShowOnChart()
        {
            return ShowOnChart(false);
        }

        public Plot ShowOnChart(bool modal)
        {
            ChartForm result = new ChartForm(Data.X.Name ?? "X", Data.Y.Name ?? "Y");
            if (ColumnX != null && ColumnY != null)
            {
                result = new ChartForm(ColumnX.HeaderText, ColumnY.HeaderText);
                result.StatChart.IsChronic = ColumnX.ValueType == typeof(DateTime);
            }
            result.Text = Properties.ScatterplotName;
            result.StatChart.AddSeries(this);
            result.StatChart.Remaster();

            if (modal)
            {
                result.ShowDialog();
            }
            else
            {
                result.Show();
            }

            return result.StatChart;
        }

        public static void ShowOnChart(Plot chartProperties, IEnumerable<Scatterplot> scatterplots)
        {
            ChartForm chartForm = new ChartForm();
            chartProperties.CopyTo(chartForm.StatChart);

            foreach (Scatterplot scatterplot in scatterplots)
            {
                chartForm.StatChart.AddSeries(scatterplot);
            }

            chartForm.StatChart.Remaster();
            chartForm.Show();
        }

        public static void ShowOnChart(string xTitle, string yTitle, IEnumerable<Scatterplot> scatterplots)
        {
            ChartForm chartForm = new ChartForm(xTitle, yTitle);
            chartForm.StatChart.Text = string.Format(Resources.Interface.ScatterplotTitle, xTitle, yTitle);
            chartForm.StatChart.IsChronic = scatterplots.ElementAt(0).IsChronic;

            foreach (Scatterplot scatterplot in scatterplots)
            {
                chartForm.StatChart.AddSeries(scatterplot);
            }

            chartForm.StatChart.Remaster();
            chartForm.Show();
        }

        public void GetData()
        {
            GetData(ColumnX, ColumnY, ColumnsLabels);
        }

        private void GetData(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> labelColumns)
        {
            Data.Clear();
            Labels = new List<List<string>>();
            IsChronic = xColumn.ValueType == typeof(DateTime);

            foreach (DataGridViewRow gridRow in xColumn.DataGridView.Rows)
            {
                if (!gridRow.Visible) continue;

                if (!gridRow.Cells[xColumn.Index].Value.IsDoubleConvertible() ||
                    !gridRow.Cells[yColumn.Index].Value.IsDoubleConvertible()) continue;

                double x = gridRow.Cells[xColumn.Index].Value.ToDouble();
                if (IsChronic) x = gridRow.Cells[xColumn.Index].FormattedValue.ToDouble();

                double y = gridRow.Cells[yColumn.Index].Value.ToDouble();
                if (yColumn.ValueType == typeof(DateTime)) y = gridRow.Cells[yColumn.Index].FormattedValue.ToDouble();
                
                Data.Add(x, y);

                if (labelColumns != null)
                {
                    List<string> currentValuesLabels = new List<string>();
                    foreach (DataGridViewColumn labelColumn in labelColumns)
                    {
                        if (gridRow.Cells[labelColumn.Index].Value == null)
                        {
                            currentValuesLabels.Add(string.Empty);
                        }
                        else
                        {
                            currentValuesLabels.Add(gridRow.Cells[labelColumn.Index].FormattedValue.ToString());
                        }
                    }
                    Labels.Add(currentValuesLabels);
                }
            }
        }

        public void CalculateApproximation(TrendType type)
        {
            Regression = Regression.GetRegression(Data, type);
        }

        public static Scatterplot GetByGroup(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            DataGridViewColumn groupColumn, string group)
        {
            BivariateSample bivariateSample = new BivariateSample(xColumn.HeaderText, yColumn.HeaderText);

            foreach (DataGridViewRow gridRow in xColumn.DataGridView.Rows)
            {
                if (!gridRow.Visible) continue;

                if (!gridRow.Cells[xColumn.Index].Value.IsDoubleConvertible() ||
                    !gridRow.Cells[yColumn.Index].Value.IsDoubleConvertible()) continue;

                if (group == Mayfly.Resources.Interface.EmptyValue)
                {
                    if (gridRow.Cells[groupColumn.Index].Value != null) continue;
                }
                else
                {
                    if (gridRow.Cells[groupColumn.Index].FormattedValue.ToString() != group) continue;
                }

                bivariateSample.Add(gridRow.Cells[xColumn.Index].Value.ToDouble(),
                    gridRow.Cells[yColumn.Index].Value.ToDouble());
            }

            Scatterplot scatterplot = new Scatterplot(bivariateSample, group)
            {
                ColumnX = xColumn,
                ColumnY = yColumn,
                IsChronic = xColumn.ValueType == typeof(DateTime)
            };
            scatterplot.Properties.ScatterplotName = group;
            return scatterplot;
        }

        public static Scatterplot GetByGroup(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> labelColumns, DataGridViewColumn groupColumn, string group)
        {
            BivariateSample result = new BivariateSample(xColumn.HeaderText, yColumn.HeaderText);
            List<List<string>> labels = new List<List<string>>();
            foreach (DataGridViewRow gridRow in xColumn.DataGridView.Rows)
            {
                if (!gridRow.Visible) continue;

                if (!gridRow.Cells[xColumn.Index].Value.IsDoubleConvertible() ||
                    !gridRow.Cells[yColumn.Index].Value.IsDoubleConvertible()) continue;

                if (group == Mayfly.Resources.Interface.EmptyValue)
                {
                    if (gridRow.Cells[groupColumn.Index].Value != null) continue;
                }
                else
                {
                    if (gridRow.Cells[groupColumn.Index].FormattedValue.ToString() != group) continue;
                }

                result.Add(gridRow.Cells[xColumn.Index].Value.ToDouble(),
                    gridRow.Cells[yColumn.Index].Value.ToDouble());

                List<string> currentValueLabels = new List<string>();
                foreach (DataGridViewColumn labelColumn in labelColumns)
                {
                    if (gridRow.Cells[labelColumn.Index].Value == null)
                    {
                        currentValueLabels.Add(string.Empty);
                    }
                    else
                    {
                        currentValueLabels.Add(gridRow.Cells[labelColumn.Index].FormattedValue.ToString());
                    }
                }
                labels.Add(currentValueLabels);
            }

            Scatterplot scatterplot = new Scatterplot(result, group, labels)
            {
                ColumnX = xColumn,
                ColumnY = yColumn,
                IsChronic = xColumn.ValueType == typeof(DateTime)
            };
            scatterplot.Properties.ScatterplotName = group;
            return scatterplot;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ColumnX", ColumnX);
            info.AddValue("ColumnY", ColumnY);
            info.AddValue("Data", Data);
            info.AddValue("Series", Series);
            info.AddValue("DataRange", DataRange);
            info.AddValue("Trend", Trend);
            info.AddValue("TrendRange", TrendRange);
            info.AddValue("IsChronic", IsChronic);
            info.AddValue("Properties", Properties);
            info.AddValue("Left", Left);
            info.AddValue("Right", Right);
            info.AddValue("Top", Top);
            info.AddValue("Bottom", Bottom);
            info.AddValue("TrendAnnotation", TrendAnnotation);
            info.AddValue("Regression", Regression);
        }
    }
}
