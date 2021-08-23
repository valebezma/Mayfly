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
using RDotNet;
using System.IO;

namespace Mayfly.Mathematics.Charts
{
    public class Scatterplot : ISerializable, IDisposable
    {
        public BivariatePredictiveModel Calc { get; set; }



        public DataGridViewColumn ColumnX { get; set; }

        public DataGridViewColumn ColumnY { get; set; }

        public List<DataGridViewColumn> ColumnsLabels { get; set; }

        public List<List<string>> Labels { get; set; }



        public Plot Container { set; get; }

        public ScatterplotProperties Properties { get; set; }

        public Series Series { get; set; }

        public Series PredictionBandUpper { get; set; }

        public Series PredictionBandLower { get; set; }

        public Functor Trend { get; set; }

        public Series ConfidenceBandUpper { get; set; }

        public Series ConfidenceBandLower { get; set; }

        public CalloutAnnotation TrendAnnotation { get; set; }

        //public ScatterplotSeparation Separator { get; set; }

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
                if (PredictionBandUpper != null) PredictionBandUpper.ChartArea = value.Name;
                if (PredictionBandLower != null) PredictionBandLower.ChartArea = value.Name;
                if (Trend != null) Trend.ChartArea = value;
                if (ConfidenceBandUpper != null) PredictionBandUpper.ChartArea = value.Name;
                if (ConfidenceBandLower != null) PredictionBandLower.ChartArea = value.Name;
            }
        }

        public bool TransposeCharting { get; set; }

        public double Left { get { return TransposeCharting ? Calc.Data.Y.Minimum : Calc.Data.X.Minimum;  } }

        public double Right { get { return TransposeCharting ? Calc.Data.Y.Maximum : Calc.Data.X.Maximum; } }

        public double Top { get { return TransposeCharting ? Calc.Data.X.Maximum : Calc.Data.Y.Maximum; } }

        public double Bottom { get { return TransposeCharting ? Calc.Data.X.Minimum : Calc.Data.Y.Minimum; } }

        public bool IsChronic { get; set; }



        private bool disposed;



        public event ScatterplotEventHandler ValueChanged;

        public event ScatterplotEventHandler Updated;

        

        public Scatterplot(DataGridViewColumn xColumn, DataGridViewColumn yColumn)
        {
            TransposeCharting = false;
            ColumnX = xColumn;
            ColumnX.DataGridView.CellValueChanged += new DataGridViewCellEventHandler(CellValueChanged);
            ColumnY = yColumn;
            Labels = new List<List<string>>();
            Calc = new BivariatePredictiveModel(new BivariateSample(ColumnX.HeaderText, ColumnY.HeaderText), TrendType.Auto) { Name = yColumn.HeaderText };
            Properties = new ScatterplotProperties(this);
            GetData();
            BuildSeries();
        }

        public Scatterplot(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> labelColumns) : this(xColumn, yColumn)
        {
            ColumnsLabels = labelColumns;
        }

        public Scatterplot(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            string name) : this(xColumn, yColumn)
        {
            Calc.Name = Properties.ScatterplotName = name;
        }

        public Scatterplot(BivariatePredictiveModel calc)
        {
            TransposeCharting = false;
            Calc = calc;
            Properties = new ScatterplotProperties(this);
            BuildSeries();
        }

        public Scatterplot(BivariateSample bivariateSample, string name, List<List<string>> labels) : 
            this(new BivariatePredictiveModel(bivariateSample, TrendType.Auto) { Name = name })
        {
            Labels = labels;
            BuildSeries();
        }

        public Scatterplot(BivariateSample bivariateSample, string name) :
            this(bivariateSample, name, new List<List<string>>()) { }

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
            if (disposed) return;

            if (disposing)
            {
                if (Container != null)
                {
                    if (!Container.IsDisposed && Series != null)
                    {
                        Container.Series.Remove(Series);
                        Series.Dispose();

                        if (PredictionBandUpper != null)
                        {
                            Container.Series.Remove(PredictionBandUpper);
                            PredictionBandUpper.Dispose();
                        }

                        if (PredictionBandLower != null)
                        {
                            Container.Series.Remove(PredictionBandLower);
                            PredictionBandLower.Dispose();
                        }

                        if (Trend != null)
                        {
                            Container.Series.Remove(Trend.Series);
                            Trend.Dispose();
                        }

                        if (ConfidenceBandUpper != null)
                        {
                            Container.Series.Remove(ConfidenceBandUpper);
                            ConfidenceBandUpper.Dispose();
                        }

                        if (ConfidenceBandLower != null)
                        {
                            Container.Series.Remove(ConfidenceBandLower);
                            ConfidenceBandLower.Dispose();
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
                Calc = null;
            }

            disposed = true;
        }


        
        public Scatterplot Copy()
        {
            Scatterplot result = new Scatterplot(Calc.Data, Properties.ScatterplotName);
            return result;
        }

        public void Update(object sender, EventArgs e)
        {
            if (Container == null) return;

            Calc.Name = Series.Name = Properties.ScatterplotName;
            Series.MarkerBorderColor = Container.IsDistinguishingMode ? Constants.InfantColor : Properties.DataPointColor;
            Series.MarkerSize = Properties.DataPointSize;
            Series.MarkerBorderWidth = Properties.DataPointBorderWidth;

            if (Properties.ShowTrend)
            {
                if (Calc.TrendType == TrendType.Auto || Calc.TrendType != Properties.SelectedApproximationType)
                {
                    Calc.Calculate(Properties.SelectedApproximationType);
                }

                if (!Calc.IsRegressionOK)
                {
                    Properties.ShowTrend = false;
                    Properties.SelectedApproximationType = TrendType.Auto;
                    return;
                }

                Calc.Regression.Name = Properties.TrendName;

                if (Properties.SelectedApproximationType == TrendType.Auto)
                {
                    Properties.SelectedApproximationType = Calc.Regression.Type;
                }

                if (Trend == null)
                {
                    Trend = new Functor(Properties.TrendName, Calc.Regression.Predict, Calc.Regression.PredictInversed);
                    Container.AddSeries(Trend);
                }
                else
                {
                    Trend.Properties.FunctionName = Properties.TrendName;
                    Trend.Function = Calc.Regression.Predict;
                    Trend.FunctionInverse = Calc.Regression.PredictInversed;
                }
                
                Trend.TransposeCharting = this.TransposeCharting;

                Trend.BuildSeries(Series.YAxisType);

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
                BuildConfidenceBands();

                if (Container.Series.FindByName(ConfidenceBandUpper.Name) == null)
                {
                    Container.Series.Add(ConfidenceBandUpper);
                    Container.Series.Add(ConfidenceBandLower);
                }

                foreach (Series band in new Series[] { ConfidenceBandUpper, ConfidenceBandLower })
                {
                    band.BorderColor = band.Color = Properties.TrendColor;
                    band.BorderWidth = (int)Math.Ceiling(Properties.TrendWidth / 2M);
                    band.YAxisType = Series.YAxisType;
                }
            }
            else
            {
                if (ConfidenceBandUpper != null)
                {
                    Container.Series.Remove(ConfidenceBandUpper);
                    ConfidenceBandUpper = null;
                }

                if (ConfidenceBandLower != null)
                {
                    Container.Series.Remove(ConfidenceBandLower);
                    ConfidenceBandLower = null;
                }
            }

            if (Properties.ShowPredictionBands)
            {
                BuildPredictionBands();

                if (Container.Series.FindByName(PredictionBandUpper.Name) == null)
                {
                    Container.Series.Add(PredictionBandUpper);
                    Container.Series.Add(PredictionBandLower);
                }

                foreach (Series band in new Series[] { PredictionBandUpper, PredictionBandLower })
                {
                    band.BorderColor = band.Color = Properties.TrendColor;
                    band.BorderWidth = (int)Math.Ceiling(Properties.TrendWidth / 2M);
                    band.YAxisType = Series.YAxisType;
                }

                foreach (DataPoint dp in Series.Points)
                {
                    dp.MarkerColor = dp.MarkerBorderColor = (
                        Properties.HighlightOutliers && Calc.Regression.outliers != null &&
                        Calc.Regression.outliers.Contains(
                            TransposeCharting ? dp.YValues[0] : dp.XValue,
                            TransposeCharting ? dp.XValue : dp.YValues[0])) ? UserSettings.DistinguishColorSelected : Properties.DataPointColor;
                }
            }
            else
            {
                if (PredictionBandUpper != null)
                {
                    Container.Series.Remove(PredictionBandUpper);
                    PredictionBandUpper = null;
                }

                if (PredictionBandLower != null)
                {
                    Container.Series.Remove(PredictionBandLower);
                    PredictionBandLower = null;
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
                    TrendAnnotation.Text += Constants.Break + "N = " + Calc.Data.Count;
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

                Update(sender, e);

                if (Calc.Data != null)
                {
                    BuildSeries();
                }

                if (Properties.ShowTrend)
                {
                    Calc.Calculate(Properties.SelectedApproximationType);

                    Trend.BuildSeries(Series.YAxisType);

                    if (Properties.ShowConfidenceBands)
                    {
                        BuildConfidenceBands();
                    }

                    if (Properties.ShowPredictionBands)
                    {
                        BuildPredictionBands();
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

            for (int i = 0; i < Calc.Data.Count; i++)
            {
                DataPoint dataPoint = TransposeCharting ?
                    new DataPoint(Calc.Data.Y.ElementAt(i), Calc.Data.X.ElementAt(i)) :
                    new DataPoint(Calc.Data.X.ElementAt(i), Calc.Data.Y.ElementAt(i));

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

                if (Labels != null && Labels.Count > 0)
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
        }

        //private void AddBandsTo(Series series, Statistics.IntervalType type)
        //{
        //    double xMin = Container.AxisXMin;
        //    double xMax = Container.AxisXMax;
        //    double xInterval = (xMax - xMin) / 100;

        //    List<double> xvalues = new List<double>();
        //    for (double x = xMin - xInterval; x <= xMax + xInterval; x += xInterval)
        //    {
        //        xvalues.Add(x);
        //    }

        //    Interval[] predictions = Regression.GetInterval(xvalues.ToArray(), Properties.ConfidenceLevel, type);

        //    series.Points.Clear();

        //    for (int i = 0; i < predictions.Length; i++)
        //    {
        //        double lowerY = predictions[i].LeftEndpoint;
        //        if (double.IsInfinity(lowerY)) continue;
        //        if (double.IsNaN(lowerY)) continue;

        //        double upperY = predictions[i].RightEndpoint;
        //        if (double.IsInfinity(upperY)) continue;
        //        if (double.IsNaN(upperY)) continue;

        //        series.Points.Add(new DataPoint(series)
        //        {
        //            XValue = xvalues[i],
        //            YValues = new double[] { lowerY, upperY },
        //        });

        //        //series.Points.Add(new DataPoint(series)
        //        //{
        //        //    XValue = TransposeCharting ? lowerY : xvalues[i],
        //        //    YValues = TransposeCharting ? new double[] { xvalues[i] } : new double[] { lowerY, upperY },
        //        //});
        //    }
        //}

        private void AddBandsTo(Series upperBand, Series lowerBand, Statistics.IntervalType type)
        {
            double xMin = TransposeCharting ? (upperBand.YAxisType == AxisType.Primary ? Container.AxisYMin : Container.AxisY2Min) : Container.AxisXMin;
            double xMax = TransposeCharting ? (upperBand.YAxisType == AxisType.Primary ? Container.AxisYMax : Container.AxisY2Max) : Container.AxisXMax;
            double xInterval = (xMax - xMin) / 100;

            List<double> xvalues = new List<double>();
            for (double x = xMin/* - xInterval*/; x <= xMax/* + xInterval*/; x += xInterval)
            {
                xvalues.Add(x);
            }

            Interval[] predictions = Calc.Regression.GetInterval(xvalues.ToArray(), Properties.ConfidenceLevel, type);

            upperBand.Points.Clear();
            lowerBand.Points.Clear();

            for (int i = 0; i < predictions.Length; i++)
            {
                double lowerY = predictions[i].LeftEndpoint;
                if (double.IsInfinity(lowerY)) continue;
                if (double.IsNaN(lowerY)) continue;

                double upperY = predictions[i].RightEndpoint;
                if (double.IsInfinity(upperY)) continue;
                if (double.IsNaN(upperY)) continue;

                upperBand.Points.Add(TransposeCharting ? new DataPoint(upperY, xvalues[i]) : new DataPoint(xvalues[i], upperY));
                lowerBand.Points.Add(TransposeCharting ? new DataPoint(lowerY, xvalues[i]) : new DataPoint(xvalues[i], lowerY));
            }
        }



        public void BuildConfidenceBands()
        {
            if (ConfidenceBandUpper == null)
            {
                ConfidenceBandUpper = new Series(string.Format(Resources.Interface.ConfidenceBands, Properties.ConfidenceLevel, Properties.ScatterplotName))
                {
                    ChartType = SeriesChartType.Line,
                    BorderDashStyle = ChartDashStyle.Dash,
                    IsVisibleInLegend = true
                };
            }
            else
            {
                ConfidenceBandUpper.Name = string.Format(Resources.Interface.ConfidenceBands, Properties.ConfidenceLevel, Properties.ScatterplotName);
                ConfidenceBandUpper.Points.Clear();
            }

            if (ConfidenceBandLower == null)
            {
                ConfidenceBandLower = new Series(string.Format(Resources.Interface.ConfidenceBands + " (lower)", Properties.ConfidenceLevel, Properties.ScatterplotName))
                {
                    ChartType = SeriesChartType.Line,
                    BorderDashStyle = ChartDashStyle.Dash,
                    IsVisibleInLegend = false
                };
            }
            else
            {
                ConfidenceBandLower.Points.Clear();
            }

            ConfidenceBandUpper.YAxisType =
                ConfidenceBandLower.YAxisType = 
                Series.YAxisType;

            if (Calc.IsRegressionOK)
            {
                AddBandsTo(ConfidenceBandUpper, ConfidenceBandLower, Statistics.IntervalType.Confidence);
            }
            else
            {
                if (ConfidenceBandUpper != null) ConfidenceBandUpper.Points.Clear();
            }
        }



        public void BuildPredictionBands()
        {
            if (PredictionBandUpper == null)
            {
                PredictionBandUpper = new Series(string.Format(Resources.Interface.PredictionBands, Properties.ConfidenceLevel, Properties.ScatterplotName))
                {
                    ChartType = SeriesChartType.Line,
                    BorderDashStyle = ChartDashStyle.DashDot,
                    IsVisibleInLegend = true
                };
            }
            else
            {
                PredictionBandUpper.Name = string.Format(Resources.Interface.PredictionBands, Properties.ConfidenceLevel, Properties.ScatterplotName);
                PredictionBandUpper.Points.Clear();
            }

            if (PredictionBandLower == null)
            {
                PredictionBandLower = new Series(string.Format(Resources.Interface.PredictionBands + " (lower)", Properties.ConfidenceLevel, Properties.ScatterplotName))
                {
                    ChartType = SeriesChartType.Line,
                    BorderDashStyle = ChartDashStyle.DashDot,
                    IsVisibleInLegend = false
                };
            }
            else
            {
                PredictionBandLower.Points.Clear();
            }

            PredictionBandUpper.YAxisType = 
                PredictionBandLower.YAxisType = 
                Series.YAxisType;

            if (Calc.IsRegressionOK)
            {
                AddBandsTo(PredictionBandUpper, PredictionBandLower, Statistics.IntervalType.Prediction);
            }
            else
            {
                if (PredictionBandUpper != null) PredictionBandUpper.Points.Clear();
            }
        }




        public Plot ShowOnChart()
        {
            return ShowOnChart(false);
        }

        public Plot ShowOnChart(bool modal)
        {
            ChartForm result = new ChartForm(Calc.Data.X.Name ?? "X", Calc.Data.Y.Name ?? "Y");
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

        //public void AddPowerPlot(Report report, string caption)
        //{
        //    REngine.SetEnvironmentVariables();
        //    REngine engine = REngine.GetInstance();
        //    string svg = IO.GetTempFileName();

        //    var drawPowerChart = engine.Evaluate(File.ReadAllText(@"interface\reports\scripts\power.R")).AsFunction();

        //    var xvalues = engine.CreateNumericVector(Data.X);
        //    var yvalues = engine.CreateNumericVector(Data.Y);
        //    var xlabel = engine.CreateCharacter(Data.X.Name);
        //    var ylabel = engine.CreateCharacter(Data.Y.Name);
        //    var width = engine.CreateNumeric(16);
        //    var height = engine.CreateNumeric(16);
        //    var detailed = engine.CreateLogical(false);
        //    var path = engine.CreateCharacter(svg);

        //    drawPowerChart.Invoke(new SymbolicExpression[] { xvalues, yvalues, xlabel, ylabel, width, height, path, detailed });

        //    foreach (string line in File.ReadAllLines(svg))
        //    {
        //        report.WriteLine(line);
        //    }

        //    report.AddParagraphClass("picturecaption", caption);
        //}









        public void GetData()
        {
            GetData(ColumnX, ColumnY, ColumnsLabels);
        }

        private void GetData(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> labelColumns)
        {
            Calc.Data.Clear();
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

                Calc.Data.Add(x, y);

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
            info.AddValue("Calc", Calc);
            info.AddValue("Series", Series);
            info.AddValue("PredictionBandUpper", PredictionBandUpper);
            info.AddValue("PredictionBandLower", PredictionBandLower);
            info.AddValue("Trend", Trend);
            info.AddValue("ConfidenceBandUpper", ConfidenceBandUpper);
            info.AddValue("ConfidenceBandLower", ConfidenceBandLower);
            info.AddValue("IsChronic", IsChronic);
            info.AddValue("Properties", Properties);
            info.AddValue("Left", Left);
            info.AddValue("Right", Right);
            info.AddValue("Top", Top);
            info.AddValue("Bottom", Bottom);
            info.AddValue("TrendAnnotation", TrendAnnotation);
        }
    }
}
