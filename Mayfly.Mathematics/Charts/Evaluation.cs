using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Extensions;
using Meta.Numerics.Statistics;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;


namespace Mayfly.Mathematics.Charts
{
    public class Evaluation : List<Sample>
    {
        #region Properties

        public Plot Container { set; get; }

        public EvaluationProperties Properties { get; set; }

        public Series DataSeries { get; set; }

        public Series ErrorSeries { get; set; }

        public double Top { get; set; }

        public ChartArea ChartArea
        {
            get
            {
                ChartArea result = Container.ChartAreas.FindByName(DataSeries.ChartArea);
                if (result == null) result = Container.ChartAreas[0];
                return result;
            }

            set
            {
                DataSeries.ChartArea = value.Name;
                if (ErrorSeries != null) ErrorSeries.ChartArea = value.Name;
            }
        }

        private DataTable Table;

        private DataColumn ValueColumn;

        public event EvaluationEventHandler ValueChanged;

        public event EvaluationEventHandler StructureChanged;

        #endregion

        #region Construction and destruction

        public Evaluation(string name)
        {
            Properties = new EvaluationProperties(this);
            Properties.ValueChanged += Properties_ValueChanged;
            Properties.StructureChanged += Properties_StructureChanged;
            Properties.EvaluationName = name;
        }

        public Evaluation(DataColumn valueColumn)
        {
            Properties = new EvaluationProperties(this);

            Properties.ValueChanged += Properties_ValueChanged;
            Properties.StructureChanged += Properties_StructureChanged;
            Properties.EvaluationName = valueColumn.Caption;

            Table = valueColumn.Table;
            ValueColumn = valueColumn;
            AddRange(ValueColumn.GetSamples());
        }

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~Evaluation()
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
                        if (!Container.IsDisposed && DataSeries != null)
                        {
                            Container.Series.Remove(DataSeries);
                            DataSeries.Dispose();

                            if (ErrorSeries != null)
                            {
                                Container.Series.Remove(ErrorSeries);
                                ErrorSeries.Dispose();
                            }
                        }

                        Container.Evaluations.Remove(this);
                    }

                    Properties.Dispose();

                    Properties = null;
                }

                disposed = true;
            }
        }

        #endregion

        #region Methods

        public void Update(object sender, EventArgs e)
        {
            if (Container == null) return;

            DataSeries.Name = DataSeries.LegendText =
                 Properties.EvaluationName;

            if (!Container.IsDistinguishingMode)
            {
                foreach (DataPoint dataPoint in DataSeries.Points)
                {
                    dataPoint.Color = Properties.DataPointColor;
                }

                if (Properties.Borders)
                {
                    DataSeries.BorderWidth = 1;
                    DataSeries.BorderColor = Color.Black;
                    DataSeries.BorderDashStyle = ChartDashStyle.Solid;
                }
                else
                {
                    DataSeries.BorderWidth = 0;
                    DataSeries.BorderDashStyle = ChartDashStyle.NotSet;
                }
            }

            DataSeries.SetCustomProperty("PointWidth",
                Properties.PointWidth.ToString("0.00",
                CultureInfo.InvariantCulture));


            if (ErrorSeries != null)
            {
                //ErrorSeries.SetCustomProperty("PointWidth", "1");
                //ErrorSeries.SetCustomProperty("PointWidth",
                //    (Properties.PointWidth / 4).ToString("0.00",
                //    CultureInfo.InvariantCulture));

                ErrorSeries.Color = DataSeries.BorderColor;

                for (int i = 0; i < Count; i++)
                {
                    UpdateDataPoint(i);
                }
            }
        }

        public Plot ShowOnChart()
        {
            return ShowOnChart(false);
        }

        public void ShowOnChart(Plot statChart)
        {
            statChart.AddSeries(this);
        }

        public Plot ShowOnChart(bool modal)
        {
            ChartForm result = new ChartForm(string.Empty, ValueColumn == null ? "Value" : ValueColumn.Caption);

            result.StatChart.AddSeries(this);
            result.StatChart.Remaster();

            ValueChanged += new EvaluationEventHandler(result.StatChart.Update);
            ValueChanged += new EvaluationEventHandler(result.StatChart.Rebuild);

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

        public void BuildSeries()
        {
            Top = 0;

            if (DataSeries == null)
            {
                DataSeries = new Series(Properties.EvaluationName);
            }
            else
            {
                DataSeries.Points.Clear();
            }

            if (ErrorSeries == null)
            {
                ErrorSeries = new Series(string.Format(Resources.Interface.ErrorTitle, Properties.EvaluationName));
                ErrorSeries.ChartType = SeriesChartType.ErrorBar;
                ErrorSeries.YValuesPerPoint = 3;
                ErrorSeries.IsVisibleInLegend = false;
                ErrorSeries.SetCustomProperty("MinPixelPointWidth", "5");
                ErrorSeries.SetCustomProperty("MaxPixelPointWidth", "10");
            }
            else
            {
                ErrorSeries.Points.Clear();
            }

            for (int i = 0; i < Count; i++)
            {
                DataPoint dataPoint = new DataPoint(i + 1, this[i].Mean);
                dataPoint.ToolTip = this[i].Name;
                dataPoint.AxisLabel = this[i].Name;
                DataSeries.Points.Add(dataPoint);

                Top = Math.Max(Top, this[i].Mean);
            }

            if (Table == null) return;

            int totalVariants = 1;

            foreach (DataColumn groupColumn in Table.Columns)
            {
                if (groupColumn == ValueColumn) continue;

                List<string> labels = groupColumn.GetStrings(true);

                int currentLevelLabelCount = 0;
                int times = 0;

                while (currentLevelLabelCount < labels.Count * totalVariants)
                {
                    CustomLabel customLabel = new CustomLabel();
                    customLabel.Text = labels[currentLevelLabelCount - labels.Count * times];
                    customLabel.LabelMark = System.Windows.Forms.DataVisualization.Charting.LabelMarkStyle.LineSideMark;
                    customLabel.RowIndex = Table.Columns.Count - 1 - Table.Columns.IndexOf(groupColumn);
                    customLabel.FromPosition = currentLevelLabelCount * (Count / labels.Count / (double)totalVariants) + 0.5;
                    customLabel.ToPosition = (currentLevelLabelCount + 1) * (Count / labels.Count / (double)totalVariants) + 0.5;
                    ChartArea.AxisX.CustomLabels.Add(customLabel);

                    currentLevelLabelCount++;
                    if (currentLevelLabelCount - labels.Count * times == labels.Count) times++;
                }

                totalVariants *= labels.Count;
            }
        }

        private void UpdateDataPoint(int index)
        {
            Sample sample = this[index];

            DataPoint errorBar;

            if (ErrorSeries.Points.Count <= index)
            {
                errorBar = new DataPoint(ErrorSeries);
                errorBar.YValues[0] = sample.Mean;
                ErrorSeries.Points.Add(errorBar);
            }
            else
            {
                errorBar = ErrorSeries.Points[index];
            }

            errorBar.XValue = TrueX(index);

            switch (Properties.Appearance)
            {
                case ErrorBarType.StandardError:
                    if (sample.Count > 2)
                    {
                        errorBar.YValues[1] = sample.Mean - sample.PopulationMean.Uncertainty;
                        errorBar.YValues[2] = sample.Mean + sample.PopulationMean.Uncertainty;
                    }
                    break;
                case ErrorBarType.ConfidenceInterval:
                    if (sample.Count > 2)
                    {
                        double me = sample.MarginOfError(Properties.ConfidenceLevel);
                        errorBar.YValues[1] = sample.Mean - me;
                        errorBar.YValues[2] = sample.Mean + me;
                    }
                    break;
                case ErrorBarType.ExtemalValues:
                    if (sample.Count > 0)
                    {
                        errorBar.YValues[1] = sample.Minimum;
                        errorBar.YValues[2] = sample.Maximum;
                    }
                    break;
            }
        }

        private void Properties_ValueChanged(object sender, EvaluationEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(sender, new EvaluationEventArgs(this));
            }
        }

        private void Properties_StructureChanged(object sender, EvaluationEventArgs e)
        {
            if (StructureChanged != null)
            {
                StructureChanged.Invoke(sender, new EvaluationEventArgs(this));
            }
        }

        private double TrueX(int index)
        {
            double position = index + .5 + (1 - Properties.PointWidth) / 2;
            double separate = Properties.PointWidth / (double)Container.Evaluations.Count;
            int evalIndex = Container.Evaluations.IndexOf(this);
            return position + (evalIndex + .5) * separate;
        }

        #endregion
    }
}
