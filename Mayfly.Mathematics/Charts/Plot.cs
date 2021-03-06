using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using RDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace Mayfly.Mathematics.Charts
{
    public partial class Plot : Chart
    {
        [Localizable(true)]
        public override string Text
        {
            get { return Properties.textBoxName.Text; }
            set { Properties.textBoxName.Text = value; }
        }

        #region New properties

        [Browsable(false)]
        public PlotProperties Properties
        {
            get
            {
                return properties;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Font), "Segoe UI 9.75pt")]
        public new Font Font
        {
            get
            {
                return Properties.fontDialogUniversalFont.Font;
            }

            set
            {
                Properties.fontDialogUniversalFont.Font = value;
                Properties.textBoxFont.Text = string.Format("{0}; {1}", value.Name, value.Size);
            }
        }

        [Category("Axes"), DefaultValue(typeof(DateTimeIntervalType), "DateTimeIntervalType.Days")]
        public DateTimeIntervalType TimeInterval
        {
            get
            {
                switch (Properties.comboBoxTimeInterval.SelectedIndex)
                {
                    case 0: return DateTimeIntervalType.Milliseconds;
                    case 1: return DateTimeIntervalType.Seconds;
                    case 2: return DateTimeIntervalType.Minutes;
                    case 3: return DateTimeIntervalType.Hours;
                    case 4: return DateTimeIntervalType.Days;
                    case 5: return DateTimeIntervalType.Weeks;
                    case 6: return DateTimeIntervalType.Months;
                    case 7: return DateTimeIntervalType.Years;
                    default: return DateTimeIntervalType.Months;
                }
            }

            set
            {
                switch (value)
                {
                    case DateTimeIntervalType.Milliseconds:
                        Properties.comboBoxTimeInterval.SelectedIndex = 0;
                        break;
                    case DateTimeIntervalType.Seconds:
                        Properties.comboBoxTimeInterval.SelectedIndex = 1;
                        break;
                    case DateTimeIntervalType.Minutes:
                        Properties.comboBoxTimeInterval.SelectedIndex = 2;
                        break;
                    case DateTimeIntervalType.Hours:
                        Properties.comboBoxTimeInterval.SelectedIndex = 3;
                        break;
                    case DateTimeIntervalType.Days:
                        Properties.comboBoxTimeInterval.SelectedIndex = 4;
                        break;
                    case DateTimeIntervalType.Weeks:
                        Properties.comboBoxTimeInterval.SelectedIndex = 5;
                        break;
                    case DateTimeIntervalType.Months:
                        Properties.comboBoxTimeInterval.SelectedIndex = 6;
                        break;
                    case DateTimeIntervalType.Years:
                        Properties.comboBoxTimeInterval.SelectedIndex = 7;
                        break;
                }
            }
        }

        [Category("Axes"), DefaultValue("Axis X"), Localizable(true)]
        public string AxisXTitle
        {
            get { return Properties.textBoxXTitle.Text; }
            set { Properties.textBoxXTitle.Text = value; }
        }

        [Category("Axes"), DefaultValue(""), Localizable(true)]
        public string AxisXFormat
        {
            get
            {
                return Properties.textBoxXFormat.Text;
            }

            set
            {
                Properties.textBoxXFormat.Text = value;
            }
        }

        [Category("Axes"), DefaultValue(0d)]
        public double AxisXMin
        {
            set
            {
                if (IsChronic)
                {
                    Properties.dateTimePickerXMin.Value = DateTime.FromOADate(value);
                }
                else
                {
                    Properties.numericUpDownXMin.Value = (decimal)value;
                }
            }

            get
            {
                if (IsChronic)
                {
                    return Properties.dateTimePickerXMin.Value.ToOADate();
                }
                else
                {
                    return (double)Properties.numericUpDownXMin.Value;
                }
            }
        }

        [Category("Axes"), DefaultValue(100d)]
        public double AxisXMax
        {
            set
            {
                if (IsChronic)
                {
                    Properties.dateTimePickerXMax.Value = DateTime.FromOADate(value);
                }
                else
                {
                    Properties.numericUpDownXMax.Value = (decimal)value;
                }
            }

            get
            {
                if (IsChronic)
                {
                    return Properties.dateTimePickerXMax.Value.ToOADate();
                }
                else
                {
                    return (double)Properties.numericUpDownXMax.Value;
                }
            }
        }

        [Category("Axes"), DefaultValue(10d)]
        public double AxisXInterval
        {
            set
            {
                if (value == 0) return;
                Properties.numericUpDownXInterval.Value = (decimal)value;
            }

            get { return (double)Properties.numericUpDownXInterval.Value; }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisXAutoMinimum
        {
            get
            {
                return Properties.checkBoxXMinAuto.Checked;
            }

            set
            {
                Properties.checkBoxXMinAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisXAutoMaximum
        {
            get
            {
                return Properties.checkBoxXMaxAuto.Checked;
            }

            set
            {
                Properties.checkBoxXMaxAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisXAutoInterval
        {
            get
            {
                return Properties.checkBoxXIntervalAuto.Checked;
            }
            set
            {
                Properties.checkBoxXIntervalAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(false)]
        public bool AxisXLogarithmic
        {
            get
            {
                return Properties.checkBoxXLog.Checked;
            }

            set
            {
                Properties.checkBoxXLog.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue("Axis Y"), Localizable(true)]
        public string AxisYTitle
        {
            get { return Properties.textBoxYTitle.Text; }
            set { Properties.textBoxYTitle.Text = value; }
        }

        [Category("Axes"), DefaultValue(""), Localizable(true)]
        public string AxisYFormat
        {
            get
            {
                return Properties.textBoxYFormat.Text;
            }

            set
            {
                Properties.textBoxYFormat.Text = value;
            }
        }

        [Category("Axes"), DefaultValue(0d)]
        public double AxisYMin
        {
            set { try { Properties.numericUpDownYMin.Value = (decimal)value; } catch { } }

            get { return (double)Properties.numericUpDownYMin.Value; }
        }

        [Category("Axes"), DefaultValue(100d)]
        public double AxisYMax
        {
            set { try { Properties.numericUpDownYMax.Value = (decimal)value; } catch { } }
            get { return (double)Properties.numericUpDownYMax.Value; }
        }

        [Category("Axes"), DefaultValue(10d)]
        public double AxisYInterval
        {
            set { Properties.numericUpDownYInterval.Value = (decimal)value; }

            get { return (double)Properties.numericUpDownYInterval.Value; }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisYAutoMinimum
        {
            get
            {
                return Properties.checkBoxYMinAuto.Checked;
            }

            set
            {
                Properties.checkBoxYMinAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisYAutoMaximum
        {
            get
            {
                return Properties.checkBoxYMaxAuto.Checked;
            }

            set
            {
                Properties.checkBoxYMaxAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisYAutoInterval
        {
            get
            {
                return Properties.checkBoxYIntervalAuto.Checked;
            }

            set
            {
                Properties.checkBoxYIntervalAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(false)]
        public bool AxisYAllowBreak
        {
            get { return Properties.checkBoxAllowBreak.Checked; }

            set { Properties.checkBoxAllowBreak.Checked = value; }
        }

        [Category("Axes"), DefaultValue(10)]
        public int AxisYScaleBreak
        {
            set { Properties.numericUpDownScaleBreak.Value = (decimal)value; }

            get { return (int)Properties.numericUpDownScaleBreak.Value; }
        }

        [Category("Axes"), DefaultValue(false)]
        public bool AxisYLogarithmic
        {
            get
            {
                return Properties.checkBoxYLog.Checked;
            }

            set
            {
                Properties.checkBoxYLog.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue("Axis Y 2"), Localizable(true)]
        public string AxisY2Title
        {
            get
            {
                return Properties.textBoxY2Title.Text;
            }

            set
            {
                Properties.textBoxY2Title.Text = value;
            }
        }

        [Category("Axes"), DefaultValue(""), Localizable(true)]
        public string AxisY2Format
        {
            get
            {
                return Properties.textBoxY2Format.Text;
            }

            set
            {
                Properties.textBoxY2Format.Text = value;
            }
        }

        [Category("Axes"), DefaultValue(0d)]
        public double AxisY2Min
        {
            set { try { Properties.numericUpDownY2Min.Value = (decimal)value; } catch { } }

            get { return (double)Properties.numericUpDownY2Min.Value; }
        }

        [Category("Axes"), DefaultValue(100d)]
        public double AxisY2Max
        {
            set { try { Properties.numericUpDownY2Max.Value = (decimal)value; } catch { } }

            get { return (double)Properties.numericUpDownY2Max.Value; }
        }

        [Category("Axes"), DefaultValue(10d)]
        public double AxisY2Interval
        {
            set { Properties.numericUpDownY2Interval.Value = (decimal)value; }

            get { return (double)Properties.numericUpDownY2Interval.Value; }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisY2AutoMinimum
        {
            get
            {
                return Properties.checkBoxY2MinAuto.Checked;
            }

            set
            {
                Properties.checkBoxY2MinAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisY2AutoMaximum
        {
            get
            {
                return Properties.checkBoxY2MaxAuto.Checked;
            }

            set
            {
                Properties.checkBoxY2MaxAuto.Checked = value;
            }
        }

        [Category("Axes"), DefaultValue(true)]
        public bool AxisY2AutoInterval
        {
            get
            {
                return Properties.checkBoxY2IntervalAuto.Checked;
            }

            set
            {
                Properties.checkBoxY2IntervalAuto.Checked = value;
            }
        }

        public bool ShowLegend
        {
            get
            {
                return Properties.checkBox1.CheckState == CheckState.Checked;
            }

            set
            {
                if (value) Properties.checkBox1.CheckState = CheckState.Checked;
                else Properties.checkBox1.CheckState = CheckState.Unchecked;
            }
        }

        public bool HasSecondaryYAxis
        {
            get
            {
                foreach (Series series in Series)
                {
                    if (series.YAxisType == AxisType.Secondary) return true;
                }
                return false;
            }
        }

        [DefaultValue(false)]
        public bool IsChronic
        {
            get
            {
                return isChronic;
            }

            set
            {
                isChronic = value;

                Properties.checkBoxXLog.Enabled = !IsChronic;

                Properties.dateTimePickerXMin.Visible =
                    Properties.comboBoxTimeInterval.Visible =
                    Properties.dateTimePickerXMax.Visible = IsChronic;

                Properties.numericUpDownXMin.Visible =
                    Properties.numericUpDownXInterval.Visible =
                    Properties.numericUpDownXMax.Visible = !IsChronic;
            }
        }

        #endregion

        #region Statistical issues

        [Browsable(false)]
        public List<Scatterplot> Scatterplots = new List<Scatterplot>();

        [Browsable(false)]
        public List<Functor> Functors = new List<Functor>();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Functor LastSelectedFunctor { get; set; }

        [Browsable(false)]
        public List<Histogramma> Histograms = new List<Histogramma>();

        //[Browsable(false)]
        //public List<Evaluation> Evaluations = new List<Evaluation>();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Scatterplot LastSelectedScatterplot { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Scatterplot> SelectedScatterplots = new List<Scatterplot>();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Histogramma LastSelectedHistogram { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Histogramma> SelectedHistograms = new List<Histogramma>();

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public List<Evaluation> SelectedEvaluations = new List<Evaluation>();

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private List<DataPoint> SelectedDataPoints = new List<DataPoint>();

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public System.Windows.Forms.DataVisualization.Charting.StripLine SelectedArgumentStripLine { get; set; }

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public System.Windows.Forms.DataVisualization.Charting.StripLine SelectedFunctionStripLine { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.DataVisualization.Charting.Cursor SelectionCursor { get; set; }

        #endregion

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool IsCursorArgumentDragging
        //{
        //    set;

        //    get;
        //}

        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool IsCursorFunctionDragging
        //{
        //    set;
        //    get;
        //}

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private double MostLeft
        {
            get
            {
                double minimum = double.MaxValue;

                foreach (Scatterplot sample in Scatterplots)
                {
                    if (sample.Calc == null) continue;
                    if (double.IsNaN(sample.Left)) continue;
                    minimum = Math.Min(minimum, sample.Left);
                }

                foreach (Functor functor in Functors)
                {
                    if (functor.ArgumentStripLine == null) continue;                    
                    minimum = Math.Min(minimum, functor.ArgumentStripLine.IntervalOffset);
                }

                foreach (Histogramma sample in Histograms)
                {
                    if (sample.Data == null) continue;
                    if (double.IsNaN(sample.Left)) continue;
                    minimum = Math.Min(minimum, sample.Left);
                }

                if (minimum != double.MaxValue)
                {
                    return minimum;
                }

                return 0;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private double MostRight
        {
            get
            {
                double maximum = double.MinValue;

                foreach (Scatterplot sample in Scatterplots)
                {
                    if (sample.Calc == null) continue;
                    if (double.IsNaN(sample.Right)) continue;
                    maximum = Math.Max(maximum, sample.Right);
                }

                foreach (Functor functor in Functors)
                {
                    if (functor.ArgumentStripLine == null) continue;
                    maximum = Math.Max(maximum, functor.ArgumentStripLine.IntervalOffset);
                }

                foreach (Histogramma sample in Histograms)
                {
                    if (sample.Data == null) continue;
                    if (double.IsNaN(sample.Right)) continue;
                    maximum = Math.Max(maximum, sample.Right);
                }

                if (maximum != double.MinValue)
                {
                    return maximum;
                }

                return 1;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private double MostBottom
        {
            get
            {
                double minimum = double.MaxValue;

                foreach (Histogramma sample in Histograms)
                {
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Secondary) continue;
                    minimum = Math.Max(minimum, 0);
                }

                foreach (Scatterplot sample in Scatterplots)
                {
                    if (sample.Calc == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Secondary) continue;
                    if (!double.IsNaN(sample.Bottom)) minimum = Math.Min(minimum, sample.Bottom);
                }

                foreach (Functor sample in Functors)
                {
                    if (sample.FunctionStripLine == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Secondary) continue;                    
                    minimum = Math.Min(minimum, sample.FunctionStripLine.IntervalOffset);
                }

                if (minimum != double.MaxValue)
                {
                    return minimum;
                }

                return 0;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private double MostBottom2
        {
            get
            {
                double minimum = double.MaxValue;

                foreach (Histogramma sample in Histograms)
                {
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Primary) continue;
                    minimum = Math.Max(minimum, 0);
                }

                foreach (Scatterplot sample in Scatterplots)
                {
                    if (sample.Calc == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Primary) continue;
                    if (!double.IsNaN(sample.Bottom)) minimum = Math.Min(minimum, sample.Bottom);
                }

                foreach (Functor sample in Functors)
                {
                    if (sample.FunctionStripLine == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Primary) continue;                    
                    minimum = Math.Min(minimum, sample.FunctionStripLine.IntervalOffset);
                }

                if (minimum != double.MaxValue)
                {
                    return minimum;
                }

                return 0;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private double MostTop
        {
            get
            {
                double maximum = double.MinValue;

                foreach (Histogramma sample in Histograms)
                {
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Secondary) continue;
                    maximum = Math.Max(maximum, sample.Top);
                }

                foreach (Scatterplot sample in Scatterplots)
                {
                    if (sample.Calc == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Secondary) continue;
                    if (!double.IsNaN(sample.Top)) maximum = Math.Max(maximum, sample.Top);
                }

                foreach (Functor sample in Functors)
                {
                    if (sample.FunctionStripLine == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Secondary) continue;
                    maximum = Math.Max(maximum, sample.FunctionStripLine.IntervalOffset);
                }

                if (maximum != double.MinValue)
                {
                    return maximum;
                }

                return 1;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private double MostTop2
        {
            get
            {
                double maximum = double.MinValue;

                foreach (Histogramma sample in Histograms)
                {
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Primary) continue;
                    maximum = Math.Max(maximum, sample.Top);
                }

                foreach (Scatterplot sample in Scatterplots)
                {
                    if (sample.Calc == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Primary) continue;
                    if (!double.IsNaN(sample.Top)) maximum = Math.Max(maximum, sample.Top);
                }

                foreach (Functor sample in Functors)
                {
                    if (sample.FunctionStripLine == null) continue;
                    if (sample.Series != null && sample.Series.YAxisType == AxisType.Primary) continue;
                    maximum = Math.Max(maximum, sample.FunctionStripLine.IntervalOffset);
                }

                if (maximum != double.MinValue)
                {
                    return maximum;
                }

                return 1;
            }
        }



        public event EventHandler Updated;

        public event EventHandler AxesUpdated;

        public event EventHandler CollectionChanged;

        public event EventHandler SelectionChanged;

        internal bool IsDistinguishingMode { get; set; }

        private PlotProperties properties;

        private bool isChronic;

        int colorIndex = 0;



        public Plot()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                if (ChartAreas.Count == 0)
                {
                    ChartAreas.Add(CreateChartArea("Common"));
                }
                else
                {
                    ChartAreas[0].Name = "Common";
                }
            }

            Series.Clear();

            SelectedScatterplots = new List<Scatterplot>();
            SelectedHistograms = new List<Histogramma>();

            properties = new PlotProperties(this);

            IsChronic = false;
            //IsCursorArgumentDragging = false;
        }

        protected override void DestroyHandle()
        {
            Clear();
            Properties.Close();

            base.DestroyHandle();
        }




        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            HitTestResult result = HitTest(e.X, e.Y);

            //contextScatterplotFindValue.Visible = toolStripSeparatorScatterplotFindValue.Visible =
            //    result.ChartElementType == ChartElementType.DataPoint |
            //    result.ChartElementType == ChartElementType.DataPointLabel;

            switch (result.ChartElementType)
            {
                case ChartElementType.Annotation:
                    object sample = GetSample((result.Object as CalloutAnnotation).Name);
                    if (sample == null) return;
                    if (sample is Scatterplot)
                    {
                        Series_MouseClick((sample as Scatterplot).Trend.Series, e);
                    }
                    break;
                case ChartElementType.DataPoint:
                    if (result.Series == null) break;
                    Series_MouseClick(result.Series, e);
                    DataPoint_MouseClick(result.Series.Points[result.PointIndex], e);
                    break;
                case ChartElementType.LegendItem:
                    Series_MouseClick(result.Series, e);
                    break;
                case ChartElementType.Gridlines:
                case ChartElementType.Axis:
                case ChartElementType.AxisTitle:
                case ChartElementType.PlottingArea:
                case ChartElementType.Nothing:
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        SelectedScatterplots.Clear();
                        SelectedHistograms.Clear();
                        SeriesClearSelection();
                        SelectedDataPoints.Clear();
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        contextChart.Show(this, e.Location);
                    }
                    break;
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            HitTestResult hitTest = HitTest(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                switch (hitTest.ChartElementType)
                {
                    case ChartElementType.Annotation:
                        object sample = GetSample((hitTest.Object as CalloutAnnotation).Name);
                        if (sample == null) return;

                        if (sample is Functor)
                        {
                            LastSelectedFunctor = sample as Functor;
                            contextFunctorProperties_Click(sample, e);
                        }

                        if (sample is Scatterplot)
                        {
                            SelectedScatterplots.Clear();
                            SelectedScatterplots.Add(sample as Scatterplot);
                            LastSelectedScatterplot = sample as Scatterplot;
                            SeriesShowSelected();
                            Annotation_MouseDoubleClick((sample as Scatterplot).Series);
                        }

                        if (sample is Histogramma)
                        {
                            SelectedHistograms.Clear();
                            SelectedHistograms.Add(sample as Histogramma);
                            LastSelectedHistogram = sample as Histogramma;
                            SeriesShowSelected();
                            Annotation_MouseDoubleClick((sample as Histogramma).Series);
                        }
                        break;

                    case ChartElementType.LegendItem:
                    case ChartElementType.DataPoint:
                        if (hitTest.Series == null) return;
                        if (GetSample(hitTest.Series) == null) return;
                        Series_MouseDoubleClick(hitTest.Series);
                        break;
                    case ChartElementType.Axis:
                    case ChartElementType.AxisTitle:
                        contextChartAxes_Click(this, e);
                        if (hitTest.Axis == ChartAreas[0].AxisX)
                            Properties.SetAxisXTitleEntering();
                        if (hitTest.Axis == ChartAreas[0].AxisY)
                            Properties.SetAxisYTitleEntering();
                        if (hitTest.Axis == ChartAreas[0].AxisY2)
                            Properties.SetAxisY2TitleEntering();
                        break;
                    case ChartElementType.PlottingArea:
                    case ChartElementType.Nothing:
                        contextChartProperties_Click(this, e);
                        break;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            HitTestResult result = HitTest(e.X, e.Y);
            switch (result.ChartElementType)
            {
                case ChartElementType.StripLines:
                    if (result.Object is StripLine)
                    {
                        LastSelectedFunctor = GetSample(result.Object as StripLine);

                        if (LastSelectedFunctor != null)
                        {
                            LastSelectedFunctor.IsCursorFunctionDragging =
                                LastSelectedFunctor.Properties.AllowCursors &&
                                LastSelectedFunctor.IsMouseAboveFunction(e);

                            LastSelectedFunctor.IsCursorArgumentDragging =
                                LastSelectedFunctor.Properties.AllowCursors &&
                                LastSelectedFunctor.IsMouseAboveArgument(e);
                        }
                    }
                    break;

                case ChartElementType.DataPoint:
                    if (result.Series == null) break;
                    object sample = GetSample(result.Series.Name);

                    if (sample is Scatterplot)
                    {
                        LastSelectedScatterplot = sample as Scatterplot;

                        if (LastSelectedScatterplot.Trend != null)
                        {
                            LastSelectedScatterplot.Trend.IsCursorFunctionDragging = LastSelectedScatterplot.Trend.Properties.AllowCursors &&
                                LastSelectedScatterplot.Trend.IsMouseAboveFunction(e);

                            LastSelectedScatterplot.Trend.IsCursorArgumentDragging = LastSelectedScatterplot.Trend.Properties.AllowCursors &&
                                LastSelectedScatterplot.Trend.IsMouseAboveArgument(e);
                        }
                    }

                    if (sample is Histogramma)
                    {
                        LastSelectedHistogram = sample as Histogramma;
                    }

                    break;
            }

            //IsCursorArgumentDragging = IsMouseAboveArgument(e);
            //IsCursorFunctionDragging = IsMouseAboveFunction(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (LastSelectedFunctor != null)
                LastSelectedFunctor.OnMouseMove(e);

            if (LastSelectedScatterplot != null && LastSelectedScatterplot.Trend != null)
                LastSelectedScatterplot.Trend.OnMouseMove(e);

            //if (IsCursorArgumentDragging)
            //{
            //    if (SelectedArgumentStripLine != null) {

            //        //SetCursorPixelPosition(SelectedArgumentStripLine, e.Location, TargetXInterval);
            //        SelectedArgumentStripLine.IntervalOffset = ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);

            //        if (LastSelectedFunctor != null && LastSelectedFunctor.Properties.AllowCursors) 
            //            LastSelectedFunctor.SetFunctionCursor();

            //        if (LastSelectedScatterplot != null && LastSelectedScatterplot.Trend != null) 
            //            LastSelectedScatterplot.Trend.SetFunctionCursor();
            //    }
            //}
            //else if (IsCursorFunctionDragging)
            //{
            //    if (SelectedFunctionStripLine != null) {

            //        //SetCursorPixelPosition(SelectedFunctionStripLine, e.Location, TargetYInterval);
            //        SelectedFunctionStripLine.IntervalOffset = ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);

            //        if (LastSelectedFunctor != null && LastSelectedFunctor.Properties.AllowCursors)
            //            LastSelectedFunctor.SetArgumentCursor();

            //        if (LastSelectedScatterplot != null && LastSelectedScatterplot.Trend != null) 
            //            LastSelectedScatterplot.Trend.SetArgumentCursor();
            //    }
            //}
            //else
            //{
            //    if (IsMouseAboveArgument(e)) {
            //        Cursor = Cursors.SizeWE;
            //    } else if (IsMouseAboveFunction(e)) {
            //        Cursor = Cursors.SizeNS;
            //    } else {
            //        Cursor = Cursors.Default;
            //    }
            //}

            if (e.Button == MouseButtons.Left && SelectionChanged != null)
            {
                SelectionChanged.Invoke(this, e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (LastSelectedFunctor != null)
            {
                LastSelectedFunctor.IsCursorArgumentDragging = false;
                LastSelectedFunctor.IsCursorFunctionDragging = false;
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                MoveSelection(e);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (this.FindForm() != null)
            {
                FindForm().FormClosing += FormClosing;
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Scatterplot sample in Scatterplots)
            {
                sample.Properties.Dispose();
            }

            foreach (Histogramma sample in Histograms)
            {
                sample.Properties.Dispose();
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    sample.Properties.Dispose();
            //}

            Dispose();

            (sender as Form).Dispose();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            UpdateAxes();
        }



        public Color GetNextColor(double opacity)
        {
            if (Series.Count == 0 || colorIndex == UserSettings.Pallette.Length)
            {
                colorIndex = 0;
            }

            Color result = Color.FromArgb((int)(255d * opacity), UserSettings.Pallette[colorIndex]);
            colorIndex++;
            return result;
        }

        public Color GetNextColor()
        {
            return GetNextColor(1);
        }


        public void AddSeries(Histogramma histogram)
        {
            if (IsChronic != histogram.IsChronic)
                throw new ArgumentException("New series has different argument type.");

            if (GetSample(histogram.Properties.HistogramName) != null)
                return;

            Histograms.Add(histogram);
            histogram.Container = this;
            LastSelectedHistogram = histogram;

            recalculateAxesProperties();

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(histogram, new EventArgs());
            }
        }

        public void AddSeries(Scatterplot scatterplot)
        {
            if (IsChronic != scatterplot.IsChronic)
                throw new ArgumentException("New series has different argument type.");

            if (GetSample(scatterplot.Properties.ScatterplotName) != null)
                return;

            Scatterplots.Add(scatterplot);
            scatterplot.Container = this;
            LastSelectedScatterplot = scatterplot;

            //contextScatterplotTrendCompare.Enabled = Scatterplots.Count > 1;

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(scatterplot, new EventArgs());
            }
        }

        public void AddSeries(Functor functor)
        {
            Functors.Add(functor);
            functor.Container = this;

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(functor, new EventArgs());
            }
        }

        //public void AddSeries(Evaluation evaluation)
        //{
        //    Evaluations.Add(evaluation);
        //    evaluation.Container = this;

        //    AxisXAutoInterval = false;
        //    AxisXInterval = 1;

        //    AxisXAutoMaximum = false;
        //    AxisXAutoMinimum = false;

        //    AxisXMax = evaluation.Count + 1;
        //    AxisXMin = 0;

        //    evaluation.BuildSeries();
        //    Series.Add(evaluation.DataSeries);
        //    Series.Add(evaluation.ErrorSeries);

        //    if (CollectionChanged != null)
        //    {
        //        CollectionChanged.Invoke(this, new EventArgs());
        //    }

        //    if (AxisYAutoMinimum) { UpdateYMin(); }
        //    if (AxisYAutoMaximum) { UpdateYMax(); }

        //    //ShowLegend = Evaluations.Count > 1;
        //}



        void recalculateAxesProperties()
        {
            if (IsChronic)
            {
                //if (AxisXAutoInterval)
                //{
                //        TimeInterval = Mayfly.Service.GetAutoIntervalType(AxisXMax - AxisXMin);
                //        AxisXFormat = Mayfly.Service.GetAutoFormat(TimeInterval);
                //        //AxisXFormat = Mayfly.Service.GetAutoFormat(DateTime.FromOADate(AxisXMin), DateTime.FromOADate(AxisXMax));

                //        switch (TimeInterval)
                //        {
                //            case DateTimeIntervalType.Days:
                //                DateTime dateTimeD = DateTime.FromOADate(AxisXMin);
                //                AxisXMin = Math.Floor(dateTimeD.ToOADate());

                //                dateTimeD = DateTime.FromOADate(AxisXMax);
                //                AxisXMax = Math.Ceiling(dateTimeD.ToOADate()) + ChartAreas[0].AxisX.IntervalOffset;
                //                break;
                //            case DateTimeIntervalType.Weeks:
                //                DateTime dateTimeW = DateTime.FromOADate(AxisXMin);
                //                dateTimeW = dateTimeW.AddDays(-7 + dateTimeW.DayOfWeek.CompareTo(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) + 1);
                //                AxisXMin = dateTimeW.ToOADate();

                //                dateTimeW = DateTime.FromOADate(AxisXMax);
                //                dateTimeW = dateTimeW.AddDays(dateTimeW.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 1);
                //                AxisXMax = dateTimeW.ToOADate() + ChartAreas[0].AxisX.IntervalOffset;
                //                break;
                //            case DateTimeIntervalType.Months:
                //                DateTime dateTimeM = DateTime.FromOADate(AxisXMax);
                //                dateTimeM = dateTimeM.AddDays(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTimeM.Year, dateTimeM.Month) - dateTimeM.Day);
                //                AxisXMax = dateTimeM.ToOADate();

                //                dateTimeM = DateTime.FromOADate(AxisXMin);
                //                dateTimeM = dateTimeM.AddDays(-dateTimeM.Day + 1);
                //                AxisXMin = dateTimeM.ToOADate();
                //                break;
                //            case DateTimeIntervalType.Years:
                //                DateTime dateTimeY = DateTime.FromOADate(AxisXMax);
                //                dateTimeY = dateTimeY.AddDays(CultureInfo.CurrentCulture.Calendar.GetDaysInYear(dateTimeY.Year) - dateTimeY.DayOfYear);
                //                AxisXMax = dateTimeY.ToOADate();

                //                dateTimeY = DateTime.FromOADate(AxisXMin);
                //                dateTimeY = dateTimeY.AddDays(-dateTimeY.DayOfYear + 1);
                //                AxisXMin = dateTimeY.ToOADate();
                //                break;
                //        }

                //        rest1 = Math.IEEERemainder(AxisXMin, 1 / Mayfly.Service.GetAutoInterval(TimeInterval));

                //        if (AxisXAutoMinimum && rest1 < 0)
                //        {
                //            AxisXMin = AxisXMin + Math.Abs(rest1) - 1 / Mayfly.Service.GetAutoInterval(TimeInterval);
                //        }

                //        rest2 = Math.IEEERemainder(AxisXMax, 1 / Mayfly.Service.GetAutoInterval(TimeInterval));

                //        if (AxisXAutoMaximum && rest2 > 0)
                //        {
                //            AxisXMax = AxisXMax + (1 / Mayfly.Service.GetAutoInterval(TimeInterval) - rest2);
                //        }
                //}
            }
            else
            {
                if (AxisXAutoMinimum)
                {
                    AxisXMin = Mayfly.Service.AdjustLeft(MostLeft, AxisXMax);
                }

                if (AxisXAutoMaximum)
                {
                    AxisXMax = Mayfly.Service.AdjustRight(AxisXMin, MostRight);
                }

                if (AxisXAutoInterval)
                {
                    AxisXInterval = Mayfly.Service.GetAutoInterval(AxisXMax - AxisXMin);
                }
            }

            foreach (Histogramma hist in Histograms)
            {
                hist.Distribute();
            }

            if (AxisYAutoMinimum)
            {
                AxisYMin = Mayfly.Service.AdjustLeft(MostBottom, AxisYMax);
            }

            if (AxisYAutoMaximum)
            {
                AxisYMax = Mayfly.Service.AdjustRight(AxisYMin, MostTop);
            }


            if (AxisY2AutoMinimum)
            {
                AxisY2Min = Mayfly.Service.AdjustLeft(MostBottom2, AxisY2AutoMaximum ? MostTop2 : AxisY2Max);
            }

            if (AxisY2AutoMaximum)
            {
                AxisY2Max = Mayfly.Service.AdjustRight(AxisY2Min, MostTop2);
            }



            if (AxisYAutoInterval)
            {
                if (AxisY2AutoInterval)
                {
                    AxisYInterval = Mayfly.Service.GetAutoInterval(AxisYMax - AxisYMin);
                }
                else
                {
                    // Set interval according to Secondary Axis
                    int intervals = (int) ((AxisY2Max - AxisY2Min) / AxisY2Interval);
                    AxisYInterval = Mayfly.Service.AdjustRight(0, (AxisYMax - AxisYMin) / intervals);
                    AxisYMax = AxisYMin + intervals * AxisYInterval;
                }
            }

            if (AxisY2AutoInterval)
            {
                int intervals = (int) ((AxisYMax - AxisYMin) / AxisYInterval);
                AxisY2Interval = Mayfly.Service.AdjustRight(0, (AxisY2Max - AxisY2Min) / intervals);
                AxisY2Max = AxisY2Min + intervals * AxisY2Interval;
            }
        }

        public void UpdateAxes()
        {
            recalculateAxesProperties();

            foreach (ChartArea chartArea in ChartAreas)
            {
                chartArea.AxisX.Minimum = AxisXMin;
                chartArea.AxisX.Maximum = AxisXMax;
                chartArea.AxisX.AdjustIntervals(Width * (chartArea.Position.Auto ? 1 :(chartArea.Position.Width / 100)), AxisXInterval);

                chartArea.AxisY.Minimum = AxisYMin;
                chartArea.AxisY.Maximum = AxisYMax;
                chartArea.AxisY.AdjustIntervals(Height, AxisYInterval);

                chartArea.AxisY2.Minimum = AxisY2Min;
                chartArea.AxisY2.Maximum = AxisY2Max;
                chartArea.AxisY2.AdjustIntervals(Height * (chartArea.Position.Auto ? 1 :(chartArea.Position.Height / 100)), AxisY2Interval);
            }

            if (AxesUpdated != null)
            {
                AxesUpdated.Invoke(this, new EventArgs());
            }
        }

        public void DoPlot()
        {
            if (FindForm() != null && FindForm().Controls.Count == 1)
            {
                FindForm().Text = Text;
            }

            #region Legends

            if (ShowLegend)
            {
                if (Legends.Count == 0)
                {
                    Legends.Add("Common");
                }
            }
            else
            {
                Legends.Clear();
            }

            foreach (Legend legend in Legends)
            {
                legend.Font = Font;
                legend.Docking = Series.Count > 10 ? Docking.Right : Docking.Bottom;
                legend.Alignment = Series.Count > 10 ? StringAlignment.Near : StringAlignment.Center;
            }

            #endregion

            if (SelectionCursor != null)
            {
                SelectionCursor.Interval = AxisXInterval / 100;
            }

            #region Appearance

            foreach (Title title in Titles)
            {
                Font titleFont = new Font(Font.FontFamily, Font.Size + 2F, FontStyle.Bold);
                title.Font = titleFont;
            }

            #region Axes

            UpdateAxes();

            foreach (ChartArea chartArea in ChartAreas)
            {
                // TODO: Place titles where they are needed
                chartArea.AxisX.Title = AxisXTitle;
                chartArea.AxisX.IsLogarithmic = AxisXLogarithmic;
                chartArea.AxisX.LabelStyle.Format = AxisXFormat;
                chartArea.AxisX.IntervalType = IsChronic ? TimeInterval : DateTimeIntervalType.Number;

                chartArea.AxisY.Title = AxisYTitle;
                chartArea.AxisY.IsLogarithmic = AxisYLogarithmic;
                chartArea.AxisY.LabelStyle.Format = AxisYFormat;

                chartArea.AxisY2.Title = AxisY2Title;
                chartArea.AxisY2.LabelStyle.Format = AxisY2Format;

                chartArea.AxisY.ScaleBreakStyle.Enabled = AxisYAllowBreak;
                chartArea.AxisY.ScaleBreakStyle.CollapsibleSpaceThreshold = AxisYScaleBreak;

                chartArea.AxisX.LabelStyle.Font =
                chartArea.AxisX.TitleFont =
                chartArea.AxisX2.LabelStyle.Font =
                chartArea.AxisX2.TitleFont =
                chartArea.AxisY.LabelStyle.Font =
                chartArea.AxisY.TitleFont =
                chartArea.AxisY2.LabelStyle.Font =
                chartArea.AxisY2.TitleFont =
                    this.Font;

                foreach (Axis axis in chartArea.Axes)
                {
                    foreach (StripLine line in axis.StripLines)
                    {
                        line.Font = this.Font;
                    }
                }

                chartArea.AxisY.ScaleBreakStyle.Enabled = AxisYAllowBreak;

                if (AxisYAllowBreak)
                {
                    chartArea.AxisY.ScaleBreakStyle.CollapsibleSpaceThreshold =
                    AxisYScaleBreak;
                }
            }

            #endregion

            #endregion

            #region Samples

            foreach (Histogramma sample in Histograms)
            {
                if (sample.Properties.DataPointColor == Color.OliveDrab) sample.Properties.DataPointColor = GetNextColor();

                sample.Update();
            }

            foreach (Scatterplot sample in Scatterplots)
            {
                if (sample.Properties.DataPointColor == Color.SeaGreen)
                {
                    sample.Properties.DataPointColor = GetNextColor();
                }

                if (sample.Properties.ShowTrend && sample.Properties.TrendColor == Color.Maroon)
                {
                    sample.Properties.TrendColor = sample.Properties.DataPointColor.Darker();
                }

                sample.Update();
            }

            foreach (Functor sample in Functors)
            {
                if (sample.Properties.TrendColor == Color.OrangeRed) sample.Properties.TrendColor = GetNextColor();

                sample.Update();
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    sample.Properties.DataPointColor = Series[sample.DataSeries.Name].Color;
            //    sample.Update(this, new EventArgs());
            //}

            #endregion



            foreach (ChartArea chartArea in ChartAreas)
            {
                chartArea.AxisY2.Enabled = this.HasSecondaryYAxis ? AxisEnabled.True : AxisEnabled.False;
            }

            if (Updated != null)
            {
                Updated.Invoke(this, new EventArgs());
            }
        }


        public object GetSample(string name)
        {
            foreach (Scatterplot sample in Scatterplots)
            {
                if (sample.Properties.ScatterplotName == name) return sample;
            }

            foreach (Histogramma sample in Histograms)
            {
                if (sample.Properties.HistogramName == name) return sample;
            }

            foreach (Functor sample in Functors)
            {
                if (sample.Properties.FunctionName == name) return sample;
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    if (sample.Properties.EvaluationName == name) return sample;
            //}

            return null;
        }

        public object GetSample(Series series)
        {
            foreach (Scatterplot sample in Scatterplots)
            {
                if (sample.Series == series) return sample;
                if (sample.PredictionBandUpper == series) return sample;
                if (sample.PredictionBandLower == series) return sample;
                if (sample.Trend != null && sample.Trend.Series == series) return sample;
                if (sample.ConfidenceBandUpper == series) return sample;
                if (sample.ConfidenceBandLower == series) return sample;
            }

            foreach (Histogramma sample in Histograms)
            {
                if (sample.Series == series) return sample;
                if (sample.Fit != null && sample.Fit.Series == series) return sample;
            }

            foreach (Functor sample in Functors)
            {
                if (sample.Series == series) return sample;
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    if (sample.DataSeries == series) return sample;
            //    if (sample.ErrorSeries == series) return sample;
            //}

            return null;
        }

        public Functor GetSample(StripLine strip)
        {
            foreach (Functor sample in Functors)
            {
                if (sample.ArgumentStripLine == strip) return sample;
                if (sample.FunctionStripLine == strip) return sample;
            }

            return null;
        }

        public void Clear()
        {
            Series.Clear();
            Scatterplots.Clear();
            Histograms.Clear();
            Functors.Clear();
            DoPlot();
        }

        public void Remove(string name)
        {
            Remove(name, true);
        }

        public void Remove(string name, bool removeChartArea)
        {
            object sample = GetSample(name);

            if (sample != null)
            {
                List<Series> toRemove = new List<Series>();

                if (sample is Scatterplot sc)
                {
                    toRemove.Add(sc.Series);
                    toRemove.Add(sc.PredictionBandLower);
                    toRemove.Add(sc.PredictionBandUpper);
                    toRemove.Add(sc.Outliers);
                    Scatterplots.Remove(sc);

                    if (sc.Trend != null)
                    {
                        toRemove.Add(sc.Trend.Series);
                        Functors.Remove(sc.Trend);
                    }
                }

                if (sample is Histogramma h)
                {
                    toRemove.Add(h.Series);
                    Histograms.Remove(h);

                    if (h.Fit != null)
                    {
                        toRemove.Add(h.Fit.Series);
                        Functors.Remove(h.Fit);
                    }
                }

                if (sample is Functor f)
                {
                    toRemove.Add(f.Series);
                    Functors.Remove(f);
                }

                foreach (Series series in toRemove)
                {
                    if (series != null && Series.Contains(series)) Series.Remove(series);
                }
            }

            if (removeChartArea)
            {
                ChartArea HostArea = ChartAreas.FindByName(name);

                if (HostArea != null)
                {
                    ChartAreas.Remove(HostArea);
                }
            }

            DoPlot();
        }

        public void GetCursor()
        {
            SelectionCursor = ChartAreas[0].CursorX;
            SelectionCursor.AxisType = AxisType.Primary;
            SelectionCursor.IsUserSelectionEnabled = true;
            SelectionCursor.LineColor = Color.Black;
            SelectionCursor.LineDashStyle = ChartDashStyle.Solid;
            SelectionCursor.SelectionColor = Color.Black.Lighter();
            SelectionCursor.Interval = AxisXInterval / 100;
        }

        private static ChartArea CreateChartArea(string chartAreaName)
        {
            ChartArea result = new ChartArea(chartAreaName);
            result.Format();
            return result;
        }

        private void SeriesSeparate()
        {
            SeriesSeparate(AxesSyncRule.Both);
        }

        private void SeriesSeparate(AxesSyncRule syncRule)
        {
            foreach (Scatterplot sample in Scatterplots)
            {
                sample.ChartArea = SeparateChartArea(sample.Properties.ScatterplotName);
            }

            foreach (Histogramma sample in Histograms)
            {
                sample.ChartArea = SeparateChartArea(sample.Properties.HistogramName);
            }

            //ShowLegend = false;
            ChartAreas.RemoveAt(0);

            DoPlot();
        }

        private ChartArea SeparateChartArea(string name)
        {
            ChartArea result = CreateChartArea(name);
            ChartAreas.Add(result);

            Title title = new Title(result.Name, Docking.Top);
            title.DockedToChartArea = result.Name;
            title.IsDockedInsideChartArea = false;
            title.Alignment = ContentAlignment.TopCenter;
            Titles.Add(title);

            foreach (CustomLabel customLabel in ChartAreas[0].AxisX.CustomLabels)
            {
                CustomLabel newCustomLabel = new CustomLabel();
                newCustomLabel.Text = customLabel.Text;
                newCustomLabel.LabelMark = customLabel.LabelMark;
                newCustomLabel.RowIndex = customLabel.RowIndex;
                newCustomLabel.FromPosition = customLabel.FromPosition;
                newCustomLabel.ToPosition = customLabel.ToPosition;
                result.AxisX.CustomLabels.Add(newCustomLabel);
            }

            return result;
        }

        private void SeriesMerge()
        {
            Titles.Clear();
            //ShowLegend = Scatterplots.Count > 1;

            ChartArea commonChartArea = CreateChartArea("Common");

            foreach (CustomLabel customLabel in ChartAreas[0].AxisX.CustomLabels)
            {
                CustomLabel newCustomLabel = new CustomLabel();
                newCustomLabel.Text = customLabel.Text;
                newCustomLabel.LabelMark = customLabel.LabelMark;
                newCustomLabel.RowIndex = customLabel.RowIndex;
                newCustomLabel.FromPosition = customLabel.FromPosition;
                newCustomLabel.ToPosition = customLabel.ToPosition;
                commonChartArea.AxisX.CustomLabels.Add(newCustomLabel);
            }

            ChartAreas.Add(commonChartArea);

            while (ChartAreas.Count > 1)
            {
                ChartAreas.RemoveAt(0);
            }

            foreach (Scatterplot sample in Scatterplots)
            {
                sample.ChartArea = commonChartArea;
            }

            foreach (Histogramma sample in Histograms)
            {
                sample.ChartArea = commonChartArea;
            }
        }

        private void SeriesShowSelected()
        {
            SeriesClearSelection();

            //foreach (Functor functor in Functors)
            //{
            //    if (IsDistinguishingMode)
            //    {
            //        functor.Series.BorderColor =
            //        functor.Series.SmartLabelStyle.CalloutLineColor =
            //        functor.Series.LabelForeColor = Constants.InfantColor;
            //    }
            //    else
            //    {
            //        functor.Series.SmartLabelStyle.CalloutLineColor =
            //        functor.Series.LabelForeColor = Color.Black;
            //        functor.Update(this, new EventArgs());
            //    }
            //}

            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (IsDistinguishingMode)
                {
                    scatterplot.Series.MarkerColor = scatterplot.Series.MarkerBorderColor = UserSettings.ColorSelected;
                    if (scatterplot.Properties.ShowTrend) scatterplot.Trend.Series.Color = UserSettings.ColorSelected.Darker();
                    scatterplot.Series.SmartLabelStyle.CalloutLineColor = scatterplot.Series.LabelForeColor = Color.Black;
                    scatterplot.Series.SmartLabelStyle.CalloutStyle = LabelCalloutStyle.Underlined;
                }
                else
                {
                    scatterplot.Series.MarkerColor = scatterplot.Series.MarkerBorderColor;
                    if (scatterplot.Properties.ShowTrend) scatterplot.Trend.Series.Color = scatterplot.Trend.Properties.TrendColor;
                    scatterplot.Series.SmartLabelStyle.CalloutLineColor = scatterplot.Series.LabelForeColor = Color.Black;
                }
            }

            foreach (Histogramma histogram in SelectedHistograms)
            {
                if (IsDistinguishingMode)
                {
                    histogram.Series.Color = 
                        histogram.Series.BorderColor = 
                        UserSettings.ColorSelected;
                }
                else
                {
                    histogram.Series.Color = histogram.Properties.DataPointColor;
                    histogram.Series.BorderColor = Color.Black;
                }
            }
        }

        private void SeriesClearSelection()
        {
            foreach (Functor functor in Functors)
            {
                functor.Series.MarkerColor = Color.Transparent;

                if (IsDistinguishingMode)
                {
                    functor.Series.BorderColor =
                    functor.Series.SmartLabelStyle.CalloutLineColor =
                    functor.Series.LabelForeColor = Constants.InfantColor;
                }
                else
                {
                    functor.Series.SmartLabelStyle.CalloutLineColor =
                    functor.Series.LabelForeColor = Color.Black;
                    functor.Update();
                }
            }

            foreach (Scatterplot sample in Scatterplots)
            {
                Scatterplot scatterplot = (Scatterplot)sample;

                scatterplot.Series.MarkerColor = Color.Transparent;

                if (IsDistinguishingMode)
                {
                    scatterplot.Series.MarkerBorderColor =
                    scatterplot.Series.SmartLabelStyle.CalloutLineColor =
                    scatterplot.Series.LabelForeColor = Constants.InfantColor;

                    if (scatterplot.Properties.ShowTrend)
                    {
                        scatterplot.Trend.Series.Color = Constants.InfantColor;
                    }
                }
                else
                {
                    scatterplot.Series.SmartLabelStyle.CalloutLineColor =
                    scatterplot.Series.LabelForeColor = Color.Black;
                    scatterplot.Update();
                }
            }

            foreach (Histogramma sample in Histograms)
            {
                Histogramma histogram = (Histogramma)sample;

                histogram.Series.BorderColor = Color.Black;

                if (IsDistinguishingMode)
                {
                    histogram.Series.Color =
                    histogram.Series.BorderColor =
                    histogram.Series.LabelForeColor = Constants.InfantColor;
                }
            }
        }

        //private bool IsMouseAboveArgument(MouseEventArgs e)
        //{
        //    foreach (Scatterplot sample in Scatterplots)
        //    {
        //        if (sample.Trend != null && sample.Trend.IsMouseAboveArgument(e)) return true;
        //    }

        //    foreach (Functor sample in Functors)
        //    {
        //        if (sample.IsMouseAboveArgument(e)) return true;
        //    }

        //    return false;
        //}

        //private bool IsMouseAboveFunction(MouseEventArgs e)
        //{
        //    foreach (Scatterplot sample in Scatterplots)
        //    {
        //        if (sample.Trend != null && sample.Trend.IsMouseAboveFunction(e)) return true;
        //    }

        //    foreach (Functor sample in Functors)
        //    {
        //        if (sample.IsMouseAboveFunction(e)) return true;
        //    }

        //    return false;
        //}

        public void LaunchSeparation(Scatterplot scatterplot)
        {
            //if (scatterplot.Separator == null)
            //{
            //    ScatterplotSeparation separator = new ScatterplotSeparation(this, scatterplot);
            //    separator.SetFriendlyDesktopLocation(FindForm(), FormLocation.NextToHost);
            //    separator.FormClosed += new FormClosedEventHandler(Separator_FormClosed);
            //    separator.Show(FindForm());
            //    separator.AutoSeparate();
            //}
            //else
            //{
            //    scatterplot.Separator.Show();
            //}
        }

        //private void Separator_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    ScatterplotSeparation separator = (ScatterplotSeparation)sender;

        //    if (separator.DialogResult == DialogResult.OK)
        //    {
        //        foreach (Scatterplot scatterplot in (separator.Subsamples))
        //        {
        //            scatterplot.Properties.DataPointSize = separator.Sample.Properties.DataPointSize;
        //        }

        //        Remove(separator.Sample.Properties.ScatterplotName, false);
        //    }
        //    else
        //    {
        //        foreach (Scatterplot scatterplot in (separator.Subsamples))
        //        {
        //            Series.Remove(scatterplot.Series);
        //        }
        //        separator.Sample.Separator = null;
        //    }

        //    Series.Remove(separator.Centroids);

        //    Update(sender, e);
        //}

        private void MoveSelection(PreviewKeyDownEventArgs e)
        {
            if (Scatterplots.Count > 0)
            {
                int currentIndex = Scatterplots.IndexOf(LastSelectedScatterplot);
                int newIndex = currentIndex;

                switch (e.KeyCode)
                {
                    case Keys.Down:
                        newIndex--;
                        break;

                    case Keys.Up:
                        newIndex++;
                        break;
                }

                if (newIndex < 0) if (e.Shift) return; else newIndex = Scatterplots.Count - 1;
                if (newIndex == Scatterplots.Count) if (e.Shift) return; else newIndex = 0;

                if (!e.Shift)
                {
                    SelectedScatterplots.Clear();
                }

                if (SelectedScatterplots.Contains(Scatterplots[newIndex]) && SelectedScatterplots.Count > 1)
                {
                    int prevIndex = 2 * currentIndex - newIndex;
                    if (prevIndex > 0 && prevIndex < Scatterplots.Count)
                    {
                        bool retracting = !SelectedScatterplots.Contains(Scatterplots[prevIndex]);
                        if (retracting) SelectedScatterplots.Remove(LastSelectedScatterplot);
                    }
                    else
                    {
                        SelectedScatterplots.Remove(LastSelectedScatterplot);
                    }
                }
                else
                {
                    if (!SelectedScatterplots.Contains(Scatterplots[newIndex]))
                    {
                        SelectedScatterplots.Add(Scatterplots[newIndex]);
                    }
                }

                LastSelectedScatterplot = Scatterplots[newIndex];
            }

            if (Histograms.Count > 0)
            {
                int currentIndex = Histograms.IndexOf(LastSelectedHistogram);
                int newIndex = currentIndex;

                switch (e.KeyCode)
                {
                    case Keys.Down:
                        newIndex--;
                        break;

                    case Keys.Up:
                        newIndex++;
                        break;
                }

                if (newIndex < 0) newIndex = Histograms.Count - 1;
                if (newIndex >= Histograms.Count) newIndex = 0;

                if (!e.Shift)
                {
                    SelectedHistograms.Clear();
                }

                if (SelectedHistograms.Contains(Histograms[newIndex]) && SelectedHistograms.Count > 1)
                {
                    int prevIndex = 2 * currentIndex - newIndex;
                    if (prevIndex > 0 && prevIndex < Scatterplots.Count)
                    {
                        bool retracting = !SelectedHistograms.Contains(Histograms[prevIndex]);
                        if (retracting) SelectedHistograms.Remove(LastSelectedHistogram);
                    }
                    else
                    {
                        SelectedHistograms.Remove(LastSelectedHistogram);
                    }
                }
                else
                {
                    if (!SelectedHistograms.Contains(Histograms[newIndex]))
                    {
                        SelectedHistograms.Add(Histograms[newIndex]);
                    }
                }

                LastSelectedScatterplot = Scatterplots[newIndex];
            }

            SeriesShowSelected();
        }

        public void ShowOnChart(bool modal, string title, double min, double interval)
        {
            Form result = new Form();
            result.Text = title;
            result.Controls.Add(this);
            this.Dock = DockStyle.Fill;

            AxisXMin = min;
            AxisXInterval = interval;

            DoPlot();

            if (modal)
            {
                result.ShowDialog();
            }
            else
            {
                result.Show();
            }
        }



        public BivariateSample GetDataPointValues(Series series)
        {
            BivariateSample result = new BivariateSample();
            
            foreach (DataPoint dp in series.Points)
            {
                if (dp.MarkerBorderColor == Color.Transparent) continue;

                result.Add(dp.XValue, dp.YValues[0]);
            }

            return result;
        }

        public string GetVector(double width, double height)
        {
            string svg = IO.GetTempFileName(".svg");

            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();

            engine.Evaluate(string.Format("options(OutDec= '{0}')", CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator));
            engine.Evaluate(string.Format("svg('{0}', width = {1} / 2.54, height = {2} / 2.54, pointsize = {3})",
                svg.Replace('\\', '/'), width, height, 8));
            engine.Evaluate("par(mfrow = c(1, 1))");
            engine.Evaluate("par(mar = c(3, 3, 0, 3) + 1.1, cex = 1)");
            engine.SetSymbol("xlims", engine.CreateNumericVector(new double[] { AxisXMin, AxisXMax }));
            engine.SetSymbol("ylims", engine.CreateNumericVector(new double[] { AxisYMin, AxisYMax }));
            engine.Evaluate(string.Format("plot(NULL, bty = 'n', xlab = '{0}', ylab = '{1}', xlim = xlims, ylim = ylims)", AxisXTitle, AxisYTitle));

            List<string> seriesNames = new List<string>();
            engine.Evaluate("cols = c()");
            engine.Evaluate("ltys = c()");
            engine.Evaluate("lwds = c()");
            engine.Evaluate("pchs = c()");
            engine.Evaluate("fills = c()");
            engine.Evaluate("borders = c()");

            AxisType type = AxisType.Primary;

            for (int i = 0; i < Series.Count; i++)
            {
                Series series = Series[i];

                Color col = series.MarkerBorderColor;
                if (col.A == 0) col = series.MarkerColor;
                if (col.A == 0) col = series.Color;
                //if (col.Name == "Transparent") col = UserSettings.DistinguishColorSelected;

                if (series.YAxisType == type)// && col.Name != "Transparent")
                {
                    BivariateSample data = GetDataPointValues(series);

                    if (data.Count > 0)
                    {
                        var xvalues = engine.CreateNumericVector(data.X);
                        var yvalues = engine.CreateNumericVector(data.Y);
                        engine.SetSymbol("xvalues", xvalues);
                        engine.SetSymbol("yvalues", yvalues);
                        engine.SetSymbol("colvalues", engine.CreateNumericVector(new double[] { ((double)col.R / 255d), ((double)col.G / 255d), ((double)col.B / 255d), ((double)col.A / 255d) }));
                        engine.Evaluate("col1 = rgb(colvalues[1], colvalues[2], colvalues[3], colvalues[4])");

                        if (series.IsVisibleInLegend)
                        {
                            seriesNames.Add(string.IsNullOrWhiteSpace(series.LegendText) ? series.Name : series.LegendText);
                            engine.Evaluate("cols = c(cols, col1)");
                        }

                        switch (series.ChartType)
                        {
                            case SeriesChartType.Column:
                                engine.Evaluate("points(yvalues ~ xvalues, type = 'h', lwd = 5, lend = 1, col = col1, border = " + (series.BorderDashStyle == ChartDashStyle.Solid ? "'black'" : "NA") + ")");
                                if (series.IsVisibleInLegend)
                                {
                                    engine.Evaluate("pchs = c(pchs, NA)");
                                    engine.Evaluate("ltys = c(ltys, NA)");
                                    engine.Evaluate("fills = c(fills, col1)");
                                    engine.Evaluate("borders = c(borders, " + (series.BorderDashStyle == ChartDashStyle.Solid ? "'black'" : "NA") + ")");
                                    engine.Evaluate("lwds = c(lwds, NA)");
                                }
                                break;

                            case SeriesChartType.Line:
                            case SeriesChartType.Area:
                                engine.Evaluate("lines(yvalues ~ xvalues, col = col1, lty = " + (series.BorderDashStyle == ChartDashStyle.Solid ? "1" : "2") + ")");
                                if (series.IsVisibleInLegend)
                                {
                                    engine.Evaluate("pchs = c(pchs, NA)");
                                    engine.Evaluate("ltys = c(ltys, " + (series.BorderDashStyle == ChartDashStyle.Solid ? "1" : "2") + ")");
                                    engine.Evaluate("fills = c(fills, NA)");
                                    engine.Evaluate("borders = c(borders, NA)");
                                    engine.Evaluate("lwds = c(lwds, 1)");
                                }
                                break;

                            case SeriesChartType.Point:
                                engine.Evaluate("points(yvalues ~ xvalues, pch = 1, col = col1)");
                                if (series.IsVisibleInLegend)
                                {
                                    engine.Evaluate("pchs = c(pchs, 1)");
                                    engine.Evaluate("ltys = c(ltys, NA)");
                                    engine.Evaluate("fills = c(fills, NA)");
                                    engine.Evaluate("borders = c(borders, NA)");
                                    engine.Evaluate("lwds = c(lwds, NA)");
                                }
                                break;
                        }
                    }
                }

                if (i == Series.Count - 1 && HasSecondaryYAxis && type == AxisType.Primary)
                {
                    type = AxisType.Secondary;
                    i = 0;

                    engine.Evaluate("par(new = TRUE)");
                    engine.SetSymbol("y2lims", engine.CreateNumericVector(new double[] { AxisY2Min, AxisY2Max }));
                    engine.Evaluate(string.Format("plot(NULL, xlab = '', ylab = '', xlim = xlims, ylim = y2lims, axes = F)"));
                    engine.Evaluate("axis(side = 4)");
                    engine.Evaluate(string.Format("mtext('{0}', side = 4, line = 3)", AxisY2Title));
                }
            }

            if (ShowLegend)
            {
                //var legs = engine.CreateCharacterVector(seriesNames);
                //engine.SetSymbol("legs", legs);
                //engine.Evaluate("legend(x = 'topleft', legend = legs, col = cols, pch = pchs, fill = fills, bty = 'n', border = borders, lwd = lwds, lty = ltys)");

                engine.Evaluate("legs = c()");
                foreach (string s in seriesNames) { engine.Evaluate(string.Format("legs = c(legs, '{0}')", s)); }
                engine.Evaluate("legend(x = 'topleft', legend = legs, col = cols, pch = pchs, fill = fills, bty = 'n', border = borders, lwd = lwds, lty = ltys)");
            }

            engine.Evaluate("dev.off()");
            return svg;
        }

        public Report GetPrintable(double width, double height)
        {
            ApplyPaletteColors();

            Report report = new Report(string.Empty);
            report.AddImage(GetVector(width, height), Text);
            report.EndBranded();
            return report;
        }

        public void Print(double width, double height)
        {
            GetPrintable(width, height).Run();
        }

        public void Print()
        {
            Print(15, 20);
        }



        #region Series logics

        private void Series_MouseClick(Series series, MouseEventArgs e)
        {
            if (series == null) return;
            if (GetSample(series) == null) return;

            if (GetSample(series) is Functor)
            {
                LastSelectedFunctor = GetSample(series) as Functor;
                SeriesShowSelected();
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        contextFunctor.Show(this, e.Location);
                        break;
                }
            }

            if (GetSample(series) is Scatterplot)
            {
                Scatterplot scatterplot = GetSample(series) as Scatterplot;
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    if (SelectedScatterplots.Contains(scatterplot))
                    {
                        SelectedScatterplots.Remove(scatterplot);
                    }
                    else
                    {
                        SelectedScatterplots.Add(scatterplot);
                    }
                }
                else
                {
                    SelectedScatterplots.Clear();
                    SelectedScatterplots.Add(scatterplot);
                }

                LastSelectedScatterplot = scatterplot;
                SeriesShowSelected();

                if (e.Button == MouseButtons.Right)
                {
                    contextScatterplot.Show(this, e.Location);
                }
            }

            if (GetSample(series) is Histogramma)
            {
                Histogramma histogram = GetSample(series) as Histogramma;
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (ModifierKeys.HasFlag(Keys.Control))
                        {
                            if (SelectedHistograms.Contains(histogram))
                            {
                                SelectedHistograms.Remove(histogram);
                            }
                            else
                            {
                                SelectedHistograms.Add(histogram);
                            }
                        }
                        else
                        {
                            SelectedHistograms.Clear();
                            SelectedHistograms.Add(histogram);
                        }
                        LastSelectedHistogram = histogram;
                        SeriesShowSelected();
                        break;
                    case MouseButtons.Right:
                        if (ModifierKeys.HasFlag(Keys.Control))
                        {
                            if (!SelectedHistograms.Contains(histogram))
                            {
                                SelectedHistograms.Add(histogram);
                            }
                        }
                        else
                        {
                            if (!SelectedHistograms.Contains(histogram))
                            {
                                SelectedHistograms.Clear();
                                SelectedHistograms.Add(histogram);
                            }
                        }
                        LastSelectedHistogram = histogram;
                        SeriesShowSelected();
                        contextHistogram.Show(this, e.Location);
                        break;
                }
            }

            //if (GetSample(series) is Evaluation)
            //{
            //    Evaluation evaluation = GetSample(series) as Evaluation;
            //    switch (e.Button)
            //    {
            //        case MouseButtons.Left:
            //            if (ModifierKeys.HasFlag(Keys.Control))
            //            {
            //                if (SelectedEvaluations.Contains(evaluation))
            //                {
            //                    SelectedEvaluations.Remove(evaluation);
            //                }
            //                else
            //                {
            //                    SelectedEvaluations.Add(evaluation);
            //                }
            //            }
            //            else
            //            {
            //                SelectedEvaluations.Clear();
            //                SelectedEvaluations.Add(evaluation);
            //            }
            //            SeriesShowSelected();
            //            break;
            //        case MouseButtons.Right:
            //            if (ModifierKeys.HasFlag(Keys.Control))
            //            {
            //                if (!SelectedEvaluations.Contains(evaluation))
            //                {
            //                    SelectedEvaluations.Add(evaluation);
            //                }
            //            }
            //            else
            //            {
            //                if (!SelectedEvaluations.Contains(evaluation))
            //                {
            //                    SelectedEvaluations.Clear();
            //                    SelectedEvaluations.Add(evaluation);
            //                }
            //            }
            //            SeriesShowSelected();
            //            contextEvaluation.Show(this, e.Location);
            //            break;
            //    }
            //}
        }

        private void Series_MouseDoubleClick(Series series)
        {
            object sample = GetSample(series);

            if (sample is Scatterplot)
            {
                if (series == ((Scatterplot)sample).Series)
                {
                    contextScatterplotProperties_Click(((Scatterplot)sample).Series, new EventArgs());
                }
                else if (series == ((Scatterplot)sample).Trend.Series)
                {
                    contextScatterplotAddTrend_Click(((Scatterplot)sample).Trend.Series, new EventArgs());
                }
            }

            if (sample is Histogramma)
            {
                contextHistogramProperties_Click(contextHistogramProperties, new EventArgs());
            }

            //if (sample is Evaluation)
            //{
            //    contextEvalProperties_Click(contextEvalProperties, new EventArgs());
            //}
        }

        private void Annotation_MouseDoubleClick(Series series)
        {
            object sample = GetSample(series);

            if (sample is Scatterplot)
            {
                contextScatterplotTrend_Click(sample, new EventArgs());
            }

            if (sample is Histogramma)
            {
                contextHistogramAddFit_Click(sample, new EventArgs());
            }
        }

        #endregion

        #region DataPoint logics

        private void DataPoint_MouseClick(DataPoint dataPoint, MouseEventArgs e)
        {
            if (dataPoint == null) return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        if (SelectedDataPoints.Contains(dataPoint))
                        {
                            SelectedDataPoints.Remove(dataPoint);
                        }
                        else
                        {
                            SelectedDataPoints.Add(dataPoint);
                        }
                    }
                    else
                    {
                        SelectedDataPoints.Clear();
                        SelectedDataPoints.Add(dataPoint);
                    }
                    break;
                case MouseButtons.Right:
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        if (!SelectedDataPoints.Contains(dataPoint))
                        {
                            SelectedDataPoints.Add(dataPoint);
                        }
                    }
                    else
                    {
                        if (!SelectedDataPoints.Contains(dataPoint))
                        {
                            SelectedDataPoints.Clear();
                            SelectedDataPoints.Add(dataPoint);
                        }
                    }
                    break;
            }
        }

        private void DataPoint_MouseDoubleClick(DataPoint dataPoint)
        {
            contextScatterplotFindValue_Click(dataPoint, new EventArgs());
        }

        #endregion

        #region Chart menu

        private void contextChart_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextChartSeparate.Enabled =
                (Scatterplots.Count > 1 && Scatterplots.Count < 25) ||
                (Histograms.Count > 1 && Histograms.Count < 25);
        }



        private void contextChartProperties_Click(object sender, EventArgs e)
        {
            Properties.tabControl.SelectedTab = Properties.tabPageChart;
            Properties.SetFriendlyDesktopLocation(contextChart);
            Properties.Show();
        }

        private void contextChartAxes_Click(object sender, EventArgs e)
        {
            Properties.tabControl.SelectedTab = Properties.tabPageAxisX;
            Properties.SetFriendlyDesktopLocation(contextChart);
            Properties.Show();
        }

        private void contextChartAdjustAxes_Click(object sender, EventArgs e)
        {
            AxisXAutoMinimum = true;
            AxisXAutoMaximum = true;
            AxisXAutoInterval = true;

            AxisYAutoMinimum = true;
            AxisYAutoMaximum = true;
            AxisYAutoInterval = true;

            DoPlot();
        }

        private void contextChartSeparate_CheckedChanged(object sender, EventArgs e)
        {
            if (contextChartSeparate.Checked)
            {
                SeriesSeparate();
            }
            else
            {
                SeriesMerge();
            }

            DoPlot();
        }




        private void contextSizeA4_Click(object sender, EventArgs e)
        {
            Print(15, 20);
        }

        private void contextSizeA5_Click(object sender, EventArgs e)
        {
            Print(15, 10);
        }

        private void contextSizeA6_Click(object sender, EventArgs e)
        {
            Print(10, 10);
        }

        #endregion

        private void contextFunctorProperties_Click(object sender, EventArgs e)
        {
            if (!LastSelectedFunctor.Properties.Visible)
            {
                LastSelectedFunctor.Properties.SetFriendlyDesktopLocation(contextFunctor);
                LastSelectedFunctor.Properties.Show();
            }
            else
            {
                LastSelectedFunctor.Properties.BringToFront();
            }
        }

        #region Scatterplot

        private void contextScatterplot_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextScatterplotDistinguish.Enabled = Scatterplots.Count > 1;

            bool trended = false;
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (scatterplot.Calc.IsRegressionOK)
                {
                    trended = true;
                    break;
                }
            }

            contextScatterplotTrend.Visible = License.AllowedFeaturesLevel == FeatureLevel.Insider;
            contextScatterplotTrend.Enabled = trended;

            bool hasColumn = false;
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (scatterplot.ColumnX != null && scatterplot.ColumnY != null)
                {
                    hasColumn = true;
                    break;
                }
            }

            toolStripSeparatorScatterplotFindValue.Visible = contextScatterplotFindValue.Visible = hasColumn;
        }

        private void contextScatterplotProperties_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (!scatterplot.Properties.Visible)
                {
                    scatterplot.Properties.SetFriendlyDesktopLocation(contextScatterplot);
                    scatterplot.Properties.Show();
                }
                else
                {
                    scatterplot.Properties.BringToFront();
                }
            }
        }

        private void contextScatterplotAddTrend_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                scatterplot.OpenTrendProperties();
            }
        }

        private void contextScatterplotDistinguish_CheckedChanged(object sender, EventArgs e)
        {
            IsDistinguishingMode = contextScatterplotDistinguish.Checked;
            SeriesShowSelected();
        }

        private void contextScatterplotTrend_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                scatterplot.OpenRegressionProperties();
            }
        }

        private void contextScatterplotFindValue_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (scatterplot.ColumnX == null) continue;
                if (scatterplot.ColumnY == null) continue;

                scatterplot.ColumnY.DataGridView.ClearSelection();
                scatterplot.ColumnY.DataGridView.FindForm().Show();

                foreach (DataPoint dataPoint in SelectedDataPoints)
                {
                    if (scatterplot.Series.Points.Contains(dataPoint))
                    {
                        DataGridViewRow gridRow = Service.GetRow(
                            new DataGridViewColumn[] { scatterplot.ColumnX, scatterplot.ColumnY },
                            new double[] { dataPoint.XValue, dataPoint.YValues[0] });

                        if (gridRow == null) continue;

                        gridRow.Selected = true;
                        gridRow.DataGridView.FirstDisplayedScrollingRowIndex = gridRow.Index;

                        gridRow.DataGridView.FindForm().BringToFront();
                        gridRow.DataGridView.FindForm().Focus();
                    }
                }
            }
        }

        #endregion

        #region Histogram

        private void contextHistogram_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool hasData = false;
            foreach (Histogramma histogram in SelectedHistograms)
            {
                if (histogram.Data != null)
                {
                    hasData = true;
                    break;
                }
            }

            contextHistogramAddFit.Enabled = hasData;
            contextHistogramDelete.Enabled = Scatterplots.Count > 1;
            contextHistogramDistinguish.Enabled = Scatterplots.Count > 1;
            contextHistogramApart.Enabled = Scatterplots.Count > 1;
            contextHistogramDescriptive.Visible = License.AllowedFeaturesLevel == FeatureLevel.Insider;
            contextHistogramUpdate.Visible = License.AllowedFeaturesLevel == FeatureLevel.Insider;
        }

        private void contextHistogramProperties_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                if (!histogram.Properties.Visible)
                {
                    histogram.Properties.SetFriendlyDesktopLocation(contextHistogram);
                    histogram.Properties.Show();
                }
                else
                {
                    histogram.Properties.BringToFront();
                }
            }
        }

        private void contextHistogramAddFit_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                if (histogram.Data != null && histogram.Properties.ShowFit)
                {
                    if (!histogram.Properties.Visible)
                    {
                        histogram.Properties.SetFriendlyDesktopLocation(contextHistogram);
                        histogram.Properties.Show();
                    }
                    else
                    {
                        histogram.Properties.BringToFront();
                    }

                    histogram.Properties.ShowFitTab();
                }
                else
                {
                    histogram.Properties.ShowFit = true;
                    histogram.Update();
                }
            }
        }

        private void contextHistogramDistinguish_CheckedChanged(object sender, EventArgs e)
        {
            IsDistinguishingMode = contextHistogramDistinguish.Checked;
            if (!IsDistinguishingMode) DoPlot();
            SeriesShowSelected();
        }

        private void contextHistogramDelete_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                Remove(histogram.Properties.HistogramName);
            }
        }

        private void contextHistogamCopyValues_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            foreach (Histogramma histogram in SelectedHistograms)
            {
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    result += ChartAreas[0].AxisX.Title + Constants.Return;
                }

                foreach (double d in histogram.Data)
                {
                    result += d.ToString(this.AxisXFormat) + Constants.Return;
                }
            }

            Clipboard.SetText(result.Trim(Constants.Return.ToCharArray()));
        }

        private void ContextHistogramCopyDatapoints_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            foreach (Histogramma histogram in SelectedHistograms)
            {
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    result += ChartAreas[0].AxisX.Title + Constants.Tab +
                        ChartAreas[0].AxisY.Title + Constants.Return;
                }

                double s = 0;

                for (int i = 0; i < histogram.Series.Points.Count; i++)
                {
                    s += histogram.Series.Points[i].YValues[0];
                }

                for (int i = 0; i < histogram.Series.Points.Count; i++)
                {
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        result += histogram.Series.Points[i].XValue.ToString(this.AxisXFormat) + Constants.Tab +
                            (histogram.Series.Points[i].YValues[0] / s).ToString("P1") + Constants.Return;
                    }
                    else
                    {
                        result += histogram.Series.Points[i].XValue.ToString(this.AxisXFormat) + Constants.Tab +
                            histogram.Series.Points[i].YValues[0].ToString(this.AxisYFormat) + Constants.Return;
                    }
                }

                //result += Constants.Return;
            }

            Clipboard.SetText(result);
        }

        private void contextHistogramDescriptive_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                SampleProperties properties = new SampleProperties(histogram.Data);
                properties.SetFriendlyDesktopLocation(contextHistogram);
                properties.Show();
            }
        }

        private void contextHistogramSplit_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                //HistogramSeparation Separator = new HistogramSeparation(histogram, this);
                //histogram.Properties.SetFriendlyDesktopLocation(FindForm(), FormLocation.NextToHost);
                //Separator.Show();
            }
        }

        private void contextHistogramApart_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                histogram.ShowOnChart(histogram.Properties.HistogramName);
            }
        }

        private void contextHistogramUpdate_Click(object sender, EventArgs e)
        {
            foreach (Histogramma histogram in SelectedHistograms)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Evaluation

        //private void contextEvaluation_Opening(object sender, CancelEventArgs e)
        //{
        //    contextEvalDelete.Enabled = Evaluations.Count > 1;
        //    contextEvalApart.Enabled = Evaluations.Count > 1;
        //}

        //private void contextEvalProperties_Click(object sender, EventArgs e)
        //{
        //    foreach (Evaluation evaluation in SelectedEvaluations)
        //    {
        //        if (!evaluation.Properties.Visible)
        //        {
        //            evaluation.Properties.SetFriendlyDesktopLocation(contextHistogram);
        //            evaluation.Properties.Show();
        //        }
        //        else
        //        {
        //            evaluation.Properties.BringToFront();
        //        }
        //    }
        //}

        //private void contextEvalDelete_Click(object sender, EventArgs e)
        //{
        //    foreach (Evaluation evaluation in SelectedEvaluations)
        //    {
        //        Remove(evaluation.Properties.EvaluationName);
        //    }
        //}

        //private void contextEvalPointDescriptive_Click(object sender, EventArgs e)
        //{
        //    foreach (Evaluation evaluation in Evaluations)
        //    {
        //        foreach (DataPoint dataPoint in SelectedDataPoints)
        //        {
        //            int index = evaluation.DataSeries.Points.IndexOf(dataPoint);

        //            if (index == -1) continue;

        //            SampleProperties properties = new SampleProperties(evaluation[index]);
        //            properties.SetFriendlyDesktopLocation(MousePosition);
        //            properties.Show();
        //        }
        //    }
        //}

        //private void contextEvalApart_Click(object sender, EventArgs e)
        //{
        //    foreach (Evaluation evaluation in SelectedEvaluations)
        //    {
        //        evaluation.ShowOnChart();
        //    }
        //}

        #endregion
    }

    public enum AxesSyncRule
    {
        Both,
        AxisX,
        AxisY,
        None
    }
}