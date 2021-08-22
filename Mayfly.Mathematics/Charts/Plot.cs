using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using RDotNet;

namespace Mayfly.Mathematics.Charts
{
    public partial class Plot : Chart
    {
        [Localizable(true)]
        public override string Text
        {
            get { return Properties.textBoxName.Text; }
            set
            {
                Properties.textBoxName.Text = value;
                Properties.ResetTitle();
            }
        }

        #region New properties

        [Browsable(false)]
        public ChartProperties Properties
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
            get
            {
                return Properties.textBoxXTitle.Text;
            }

            set
            {
                Properties.textBoxXTitle.Text = value;
                Properties.ResetTitle();
            }
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
            get
            {
                return Properties.textBoxYTitle.Text;
            }

            set
            {
                Properties.textBoxYTitle.Text = value;
                Properties.ResetTitle();
            }
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

        public event EventHandler Updated;

        public event EventHandler CollectionChanged;

        public event EventHandler SelectionChanged;

        public event EventHandler AppearanceValueChanged;

        public event EventHandler StructureValueChanged;

        internal bool IsDistinguishingMode { get; set; }

        private ChartProperties properties;

        private bool isChronic;



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

            properties = new ChartProperties(this);

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

            contextScatterplotFindValue.Visible = toolStripSeparator5.Visible =
                result.ChartElementType == ChartElementType.DataPoint |
                result.ChartElementType == ChartElementType.DataPointLabel;

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
                            Annotation_MouseDoubleClick((sample as Histogramma).DataSeries);
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

            RefreshAxes();
        }



        public void AddSeries(Scatterplot scatterplot)
        {
            if (IsChronic == scatterplot.IsChronic)
            {
                Scatterplots.Add(scatterplot);
                scatterplot.Container = this;
                LastSelectedScatterplot = scatterplot;

                //if (Series.FindByName[scatterplot.Series.Name] != null)
                //    Series.Remove(Series[scatterplot.Series.Name]);

                Series.Add(scatterplot.Series);

                if (CollectionChanged != null)
                {
                    CollectionChanged.Invoke(scatterplot, new ScatterplotEventArgs(scatterplot));
                }
            }

            contextScatterplotTrendCompare.Enabled = Scatterplots.Count > 1;

            //ShowLegend = Scatterplots.Count > 1;
        }

        public void AddSeries(Functor functor)
        {
            Functors.Add(functor);
            functor.Container = this;
            //LastSelectedFunctor = functor;
            Series.Add(functor.Series);
            //functor.BuildSeries();

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(functor, new EventArgs());
            }
        }

        public void AddSeries(Histogramma histogram)
        {
            if (IsChronic != histogram.IsChronic)
                throw new ArgumentException("New series has different argument type.");

            if (GetSample(histogram.Properties.HistogramName) != null)
                return;

            if (Histograms.Count == 0)
            {
                if (AxisXAutoMinimum) AxisXMin = histogram.Left;
                if (AxisXAutoMaximum) AxisXMax = histogram.Right;
                if (AxisXAutoInterval) AxisXInterval = Mayfly.Service.GetAutoInterval(histogram.Right - histogram.Left);
            }

            Histograms.Add(histogram);
            histogram.Container = this;
            LastSelectedHistogram = histogram;

            if (AxisXAutoMinimum) { UpdateXMin(); }
            if (AxisXAutoMaximum) { UpdateXMax(); }

            histogram.BuildChart(this);
            //histogram.ChartArea = "Common";
            Series.Add(histogram.DataSeries);

            if (AxisYAutoMinimum) { UpdateYMin(); }
            if (AxisYAutoMaximum) { UpdateYMax(); UpdateY2Max(); }

            //ShowLegend = Histograms.Count > 1;

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(histogram, new HistogramEventArgs(histogram));
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

        public void Remaster()
        {
            SetColorScheme();
            Rebuild(this, new EventArgs());
            Update(this, new EventArgs());
        }

        public void SetColorScheme()
        {
            ApplyPaletteColors();

            foreach (Functor sample in Functors)
            {
                sample.Properties.TrendColor = Series[sample.Series.Name].Color;
            }

            foreach (Scatterplot sample in Scatterplots)
            {
                if (sample.Properties.DataPointColor == Color.SeaGreen) sample.Properties.DataPointColor = sample.Series.Color;
                if (sample.Properties.ShowTrend && sample.Properties.TrendColor == Color.Maroon) sample.Properties.TrendColor = sample.Properties.DataPointColor.Darker();
                sample.Series.Color = Color.Transparent;
            }

            foreach (Histogramma sample in Histograms)
            {
                if (sample.Properties.DataPointColor == Color.OliveDrab) sample.Properties.DataPointColor = Series[sample.DataSeries.Name].Color;
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    sample.Properties.DataPointColor = Series[sample.DataSeries.Name].Color;
            //}
        }

        public void Rebuild(object sender, EventArgs e)
        {
            foreach (Scatterplot sample in Scatterplots)
            {
                sample.BuildSeries();

                if (sample.PredictionBandLower != null) sample.PredictionBandLower.Points.Clear();
                if (sample.PredictionBandUpper != null) sample.PredictionBandUpper.Points.Clear();
                if (sample.Trend != null) sample.Trend.Series.Points.Clear();
                if (sample.ConfidenceBandLower != null) sample.ConfidenceBandLower.Points.Clear();
                if (sample.ConfidenceBandUpper != null) sample.ConfidenceBandUpper.Points.Clear();
            }

            RecalculateAxesProperties();

            foreach (Functor sample in Functors)
            {
                sample.BuildSeries();
            }

            foreach (Histogramma sample in Histograms)
            {
                Rebuild(sample, new HistogramEventArgs(sample));
                sample.BuildChart();
                //if (AxisYAutoMaximum) UpdateYMax();
            }

            //foreach (Evaluation sample in Evaluations)
            //{

            //}


            if (Updated != null)
            {
                Updated.Invoke(sender, e);
            }

            if (StructureValueChanged != null)
            {
                StructureValueChanged.Invoke(sender, e);
            }
        }

        private void Rebuild(object sender, HistogramEventArgs e)
        {
            e.Sample.BuildChart(this);

            if (AxisYAutoMinimum) { UpdateYMin(); }
            if (AxisYAutoMaximum) { UpdateYMax(); UpdateY2Max(); }
            if (AxisYAutoInterval)
            {
                AxisYInterval = Mayfly.Service.GetAutoInterval(AxisYMax - AxisYMin);
            }

            if (AxisYAutoMaximum) { UpdateYMax(); UpdateY2Max(); }
        }

        public void Update(object sender, EventArgs e)
        {
            if (FindForm() != null)
            {
                if (FindForm().Controls.Count == 1)
                {
                    FindForm().Text = Text;
                }
            }

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

            if (SelectionCursor != null)
            {
                SelectionCursor.Interval = AxisXInterval / 100;
            }

            foreach (Scatterplot sample in Scatterplots)
            {
                sample.Update(this, new EventArgs());
            }

            foreach (Functor sample in Functors)
            {
                sample.Update(this, new EventArgs());
            }

            foreach (Histogramma sample in Histograms)
            {
                sample.Update(this, new EventArgs());
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    sample.Update(this, new EventArgs());
            //}

            foreach (Title title in Titles)
            {
                Font titleFont = new Font(Font.FontFamily, Font.Size + 2F, FontStyle.Bold);
                title.Font = titleFont;
            }

            foreach (ChartArea chartArea in ChartAreas)
            {
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

                // TODO: Place titles where they are needed
                chartArea.AxisX.Title = AxisXTitle;
                chartArea.AxisY.Title = AxisYTitle;
                chartArea.AxisY2.Title = AxisY2Title;

                chartArea.AxisY2.Enabled = string.IsNullOrEmpty(chartArea.AxisY2.Title) ?
                    AxisEnabled.False : AxisEnabled.True;

                chartArea.AxisX.LabelStyle.Format = AxisXFormat;
                chartArea.AxisY.LabelStyle.Format = AxisYFormat;
                chartArea.AxisY2.LabelStyle.Format = AxisY2Format;
            }

            ChartAreas[0].AxisY.ScaleBreakStyle.Enabled = AxisYAllowBreak;

            if (AxisYAllowBreak)
            {
                ChartAreas[0].AxisY.ScaleBreakStyle.CollapsibleSpaceThreshold =
                    AxisYScaleBreak;
            }

            if (Updated != null)
            {
                Updated.Invoke(this, new EventArgs());
            }

            if (AppearanceValueChanged != null)
            {
                AppearanceValueChanged.Invoke(this, new EventArgs());
            }
        }

        public void RefreshAxes()
        {
            foreach (ChartArea chartArea in ChartAreas)
            {
                chartArea.AxisX.Minimum = AxisXMin;
                chartArea.AxisX.Maximum = AxisXMax;
                chartArea.AxisX.IsLogarithmic = AxisXLogarithmic;
                chartArea.AxisX.LabelStyle.Format = AxisXFormat;

                chartArea.AxisY.Minimum = AxisYMin;
                chartArea.AxisY.Maximum = AxisYMax;
                chartArea.AxisY.IsLogarithmic = AxisYLogarithmic;
                chartArea.AxisY.LabelStyle.Format = AxisYFormat;

                chartArea.AxisY.ScaleBreakStyle.Enabled = AxisYAllowBreak;
                chartArea.AxisY.ScaleBreakStyle.CollapsibleSpaceThreshold = AxisYScaleBreak;

                chartArea.AxisY2.Minimum = AxisY2Min;
                chartArea.AxisY2.Maximum = AxisY2Max;
                chartArea.AxisY2.LabelStyle.Format = AxisY2Format;

                #region Axis X

                double w = this.Width;

                //if (this.Visible)
                //{
                //    w = chartArea.AxisX.ValueToPixelPosition(chartArea.AxisX.Maximum) -
                //        chartArea.AxisX.ValueToPixelPosition(chartArea.AxisX.Minimum);
                //}

                if (IsChronic)
                {
                    chartArea.AxisX.IntervalType = TimeInterval;
                    double round = Mayfly.Service.GetAutoInterval(TimeInterval);
                    chartArea.AxisX.AdjustIntervals(w, round);
                }
                else
                {
                    chartArea.AxisX.IntervalType = DateTimeIntervalType.Number;
                    chartArea.AxisX.AdjustIntervals(w, AxisXInterval);
                }

                #endregion

                double h = this.Height;

                //if (this.Visible)
                //{
                //    h = chartArea.AxisY.ValueToPixelPosition(AxisYMin) -
                //        chartArea.AxisY.ValueToPixelPosition(AxisYMax);
                //}

                chartArea.AxisY.AdjustIntervals(h, AxisYInterval);

                if (chartArea.AxisY2.Enabled == AxisEnabled.True)
                {
                    double intervals = (chartArea.AxisY.Maximum - chartArea.AxisY.Minimum) / chartArea.AxisY.Interval;
                    double interval2 = (chartArea.AxisY2.Maximum - chartArea.AxisY2.Minimum) / intervals;
                    //AxisY2Interval = (AxisY2Max - AxisY2Min) / intervals;
                    chartArea.AxisY2.AdjustIntervals(h, interval2);
                    chartArea.AxisY2.Maximum = chartArea.AxisY2.Minimum + intervals * interval2;
                }

            }
        }

        public void RecalculateAxesProperties()
        {
            if (AxisXAutoMinimum) { UpdateXMin(); }

            if (AxisXAutoMaximum) { UpdateXMax(); }

            if (AxisXAutoInterval)
            {
                double rest1 = 0.0;
                double rest2 = 0.0;

                if (IsChronic)
                {
                    TimeInterval = Mayfly.Service.GetAutoIntervalType(AxisXMax - AxisXMin);
                    AxisXFormat = Mayfly.Service.GetAutoFormat(TimeInterval);
                    //AxisXFormat = Mayfly.Service.GetAutoFormat(DateTime.FromOADate(AxisXMin), DateTime.FromOADate(AxisXMax));

                    switch (TimeInterval)
                    {
                        case DateTimeIntervalType.Days:
                            DateTime dateTimeD = DateTime.FromOADate(AxisXMin);
                            AxisXMin = Math.Floor(dateTimeD.ToOADate());

                            dateTimeD = DateTime.FromOADate(AxisXMax);
                            AxisXMax = Math.Ceiling(dateTimeD.ToOADate()) + ChartAreas[0].AxisX.IntervalOffset;
                            break;
                        case DateTimeIntervalType.Weeks:
                            DateTime dateTimeW = DateTime.FromOADate(AxisXMin);
                            dateTimeW = dateTimeW.AddDays(-7 + dateTimeW.DayOfWeek.CompareTo(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) + 1);
                            AxisXMin = dateTimeW.ToOADate();

                            dateTimeW = DateTime.FromOADate(AxisXMax);
                            dateTimeW = dateTimeW.AddDays(dateTimeW.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 1);
                            AxisXMax = dateTimeW.ToOADate() + ChartAreas[0].AxisX.IntervalOffset;
                            break;
                        case DateTimeIntervalType.Months:
                            DateTime dateTimeM = DateTime.FromOADate(AxisXMax);
                            dateTimeM = dateTimeM.AddDays(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTimeM.Year, dateTimeM.Month) - dateTimeM.Day);
                            AxisXMax = dateTimeM.ToOADate();

                            dateTimeM = DateTime.FromOADate(AxisXMin);
                            dateTimeM = dateTimeM.AddDays(-dateTimeM.Day + 1);
                            AxisXMin = dateTimeM.ToOADate();
                            break;
                        case DateTimeIntervalType.Years:
                            DateTime dateTimeY = DateTime.FromOADate(AxisXMax);
                            dateTimeY = dateTimeY.AddDays(CultureInfo.CurrentCulture.Calendar.GetDaysInYear(dateTimeY.Year) - dateTimeY.DayOfYear);
                            AxisXMax = dateTimeY.ToOADate();

                            dateTimeY = DateTime.FromOADate(AxisXMin);
                            dateTimeY = dateTimeY.AddDays(-dateTimeY.DayOfYear + 1);
                            AxisXMin = dateTimeY.ToOADate();
                            break;
                    }

                    rest1 = Math.IEEERemainder(AxisXMin, 1 / Mayfly.Service.GetAutoInterval(TimeInterval));

                    if (AxisXAutoMinimum && rest1 < 0)
                    {
                        AxisXMin = AxisXMin + Math.Abs(rest1) - 1 / Mayfly.Service.GetAutoInterval(TimeInterval);
                    }

                    rest2 = Math.IEEERemainder(AxisXMax, 1 / Mayfly.Service.GetAutoInterval(TimeInterval));

                    if (AxisXAutoMaximum && rest2 > 0)
                    {
                        AxisXMax = AxisXMax + (1 / Mayfly.Service.GetAutoInterval(TimeInterval) - rest2);
                    }
                }
                else
                {
                    AxisXInterval = Mayfly.Service.GetAutoInterval(AxisXMax - AxisXMin);
                    AxisXFormat = Mayfly.Service.GetAutoFormat(AxisXInterval);

                    rest1 = Math.IEEERemainder(AxisXMin, AxisXInterval);

                    if (AxisXAutoMinimum && rest1 < 0)
                    {
                        AxisXMin = AxisXMin + Math.Abs(rest1) - AxisXInterval;
                    }

                    rest2 = Math.IEEERemainder(AxisXMax, AxisXInterval);

                    if (AxisXAutoMaximum && rest2 > 0)
                    {
                        AxisXMax = AxisXMax + (AxisXInterval - rest2);
                    }
                }

                if (AxisXAutoMinimum && rest1 > 0)
                {
                    AxisXMin = AxisXMin - rest1;
                }

                if (AxisXAutoMaximum && rest2 < 0)
                {
                    AxisXMax = AxisXMax - rest2;
                }
            }

            if (AxisYAutoMinimum) { UpdateYMin(); }
            if (AxisYAutoMaximum) { UpdateYMax(); UpdateY2Max(); }
            if (AxisYAutoInterval)
            {
                double yint = AxisYMax - AxisYMin;
                if (yint == 0) yint = 1;
                AxisYInterval = Mayfly.Service.GetAutoInterval(yint);
                AxisYFormat = Mayfly.Service.GetAutoFormat(AxisYInterval);
            }

            //if (AxisY2AutoMinimum) { UpdateY2Min(); }
            if (AxisY2AutoMaximum) { UpdateY2Max(); }
            if (AxisY2AutoInterval)
            {
                double y2int = AxisY2Max - AxisY2Min;
                if (y2int == 0) y2int = 1;
                AxisY2Interval = Mayfly.Service.GetAutoInterval(y2int);
                AxisY2Format = Mayfly.Service.GetAutoFormat(AxisY2Interval);
            }

            RefreshAxes();
        }

        public void UpdateXMin()
        {
            double minimum = double.MaxValue;

            foreach (Scatterplot sample in Scatterplots)
            {
                minimum = Math.Min(minimum, sample.Left);

                if (sample.Properties.ShowTrend && sample.Trend != null && sample.Trend.ArgumentStripLine != null)
                {
                    minimum = Math.Min(minimum, sample.Trend.ArgumentStripLine.IntervalOffset);
                }
            }

            foreach (Histogramma sample in Histograms)
            {
                minimum = Math.Min(minimum, sample.Left);
            }

            if (minimum != double.MaxValue)
            {
                AxisXMin = minimum;
            }

            //double rest = 0.0;

            //if (IsChronic)
            //{
            //    switch (TimeInterval)
            //    {
            //        case DateTimeIntervalType.Days:
            //            DateTime dateTimeD = DateTime.FromOADate(AxisXMin);
            //            AxisXMin = Math.Floor(dateTimeD.ToOADate());
            //            return;
            //        case DateTimeIntervalType.Weeks:
            //            DateTime dateTimeW = DateTime.FromOADate(AxisXMin);
            //            dateTimeW = dateTimeW.AddDays(-7 + dateTimeW.DayOfWeek.CompareTo(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) + 1);
            //            AxisXMin = dateTimeW.ToOADate();
            //            return;
            //        case DateTimeIntervalType.Months:
            //            DateTime dateTimeM = DateTime.FromOADate(AxisXMin);
            //            dateTimeM = dateTimeM.AddDays(-dateTimeM.Day + 1);
            //            AxisXMin = dateTimeM.ToOADate();
            //            return;
            //        case DateTimeIntervalType.Years:
            //            DateTime dateTimeY = DateTime.FromOADate(AxisXMin);
            //            dateTimeY = dateTimeY.AddDays(-dateTimeY.DayOfYear + 1);
            //            AxisXMin = dateTimeY.ToOADate();
            //            return;
            //    }

            //    rest = Math.IEEERemainder(AxisXMin, 1 / Mayfly.Service.GetAutoInterval(TimeInterval));

            //    if (rest < 0)
            //    {
            //        AxisXMin = AxisXMin + Math.Abs(rest) - 1 / Mayfly.Service.GetAutoInterval(TimeInterval);
            //    }
            //}
            //else
            //{
            //    rest = Math.IEEERemainder(AxisXMin, AxisXInterval);

            //    if (rest < 0)
            //    {
            //        AxisXMin = AxisXMin + Math.Abs(rest) - AxisXInterval;
            //    }
            //}

            //if (rest > 0)
            //{
            //    AxisXMin = AxisXMin - rest;
            //}
        }

        public void UpdateXMax()
        {
            double maximum = double.MinValue;

            foreach (Scatterplot sample in Scatterplots)
            {
                maximum = Math.Max(maximum, sample.Right);

                if (sample.Properties.ShowTrend && sample.Trend != null && sample.Trend.ArgumentStripLine != null)
                {
                    maximum = Math.Max(maximum, sample.Trend.ArgumentStripLine.IntervalOffset);
                }
            }

            foreach (Histogramma sample in Histograms)
            {
                maximum = Math.Max(maximum, sample.Right);
            }

            if (maximum != double.MinValue)
            {
                AxisXMax = maximum;
            }
        }

        public void UpdateYMin()
        {
            double minimum = double.MaxValue;

            foreach (Scatterplot sample in Scatterplots)
            {
                minimum = Math.Min(minimum, sample.Bottom);

                if (sample.Trend != null && sample.Trend.FunctionStripLine != null)
                {
                    minimum = Math.Min(minimum, sample.Trend.FunctionStripLine.IntervalOffset);
                }
            }

            if (minimum != double.MaxValue)
            {
                AxisYMin = minimum;
            }

            if (AxisYAutoInterval && AxisYMax - AxisYMin > 0)
            {
                AxisYInterval = Mayfly.Service.GetAutoInterval(AxisYMax - AxisYMin);
            }

            double rest = Math.IEEERemainder(AxisYMin, AxisYInterval);

            if (rest < 0)
            {
                AxisYMin = AxisYMin + Math.Abs(rest) - AxisYInterval;
            }

            if (rest > 0)
            {
                AxisYMin = AxisYMin - rest;
            }
        }

        public void UpdateYMax()
        {
            double maximum = 0;

            foreach (Scatterplot sample in Scatterplots)
            {
                maximum = Math.Max(maximum, sample.Top);
            }

            foreach (Histogramma sample in Histograms)
            {
                maximum = Math.Max(maximum, sample.Top);
            }

            //foreach (Evaluation sample in Evaluations)
            //{
            //    maximum = Math.Max(maximum, sample.Top);
            //}

            //foreach (Series series in Series)
            //{
            //    if (series.Points == null)
            //    {
            //        maximum = 1;
            //        continue;
            //    }
            //    foreach (DataPoint dataPoint in series.Points)
            //    {
            //        maximum = Math.Max(maximum, dataPoint.YValues.Max());
            //    }
            //}

            //if (SelectedFunctionCursor != null)
            //{
            //    maximum = Math.Max(maximum, SelectedFunctionCursor.Position);
            //}

            AxisYMax = 1.1 * maximum;

            if (AxisYAutoInterval && AxisYMax - AxisYMin > 0)
            {
                AxisYInterval = Mayfly.Service.GetAutoInterval(AxisYMax - AxisYMin);
            }

            double rest = Math.IEEERemainder(AxisYMax, AxisYInterval);

            if (rest > 0)
            {
                AxisYMax = AxisYMax + (AxisYInterval - rest);
            }

            if (rest < 0)
            {
                AxisYMax = AxisYMax + Math.Abs(rest);
            }
        }

        public void UpdateY2Max()
        {
            double maximum = 0;

            foreach (Series series in Series)
            {
                if (series.YAxisType == AxisType.Secondary)
                {
                    if (series.Points == null)
                    {
                        maximum = 1;
                        continue;
                    }

                    foreach (DataPoint dataPoint in series.Points)
                    {
                        maximum = Math.Max(maximum, dataPoint.YValues.Max());
                    }
                }
            }

            AxisY2Max = maximum == 0 ? 1 : maximum;

            if (AxisY2AutoInterval && AxisY2Max - AxisY2Min > 0)
            {
                double g = Mayfly.Service.GetAutoInterval(AxisY2Max - AxisY2Min);
                if (g > 0) AxisY2Interval = g;
            }

            double rest = AxisY2Max % AxisY2Interval;

            if (rest > 0)
            {
                AxisY2Max = AxisY2Max + (AxisY2Interval - rest);
            }

            if (rest < 0)
            {
                AxisY2Max = AxisY2Max + Math.Abs(rest);
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

            foreach (Functor sample in Functors)
            {
                if (sample.Series == series) return sample;
            }

            foreach (Histogramma sample in Histograms)
            {
                if (sample.DataSeries == series) return sample;
                if (sample.FitSeries == series) return sample;
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
            while (Functors.Count > 0)
            {
                Functors[0].Dispose();
            }

            while (Scatterplots.Count > 0)
            {
                Scatterplots[0].Dispose();
            }

            while (Histograms.Count > 0)
            {
                Histograms[0].Dispose();
            }

            //while (Evaluations.Count > 0)
            //{
            //    Evaluations[0].Dispose();
            //}
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
                if (sample is Scatterplot)
                {
                    ((Scatterplot)sample).Dispose();
                }

                if (sample is Histogramma)
                {
                    ((Histogramma)sample).Dispose();
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

            Remaster();
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
                    scatterplot.Series.MarkerColor = scatterplot.Series.MarkerBorderColor = UserSettings.DistinguishColorSelected;
                    if (scatterplot.Properties.ShowTrend) scatterplot.Trend.Series.Color = UserSettings.DistinguishColorSelected.Darker();
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
                    histogram.DataSeries.Color = 
                        histogram.DataSeries.BorderColor = 
                        UserSettings.DistinguishColorSelected;
                }
                else
                {
                    histogram.DataSeries.Color = histogram.Properties.DataPointColor;
                    histogram.DataSeries.BorderColor = Color.Black;
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
                    functor.Update(this, new EventArgs());
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
                    scatterplot.Update(this, new EventArgs());
                }
            }

            foreach (Histogramma sample in Histograms)
            {
                Histogramma histogram = (Histogramma)sample;

                histogram.DataSeries.BorderColor = Color.Black;

                if (IsDistinguishingMode)
                {
                    histogram.DataSeries.Color =
                    histogram.DataSeries.BorderColor =
                    histogram.DataSeries.LabelForeColor = Constants.InfantColor;
                }
            }
        }

        public void CopyTo(Plot statChart)
        {
            statChart.Font = Font;
            statChart.IsChronic = IsChronic;
            statChart.TimeInterval = TimeInterval;
            statChart.Text = Text;
            statChart.AxisXTitle = AxisXTitle;
            statChart.AxisXMin = AxisXMin;
            statChart.AxisXMax = AxisXMax;
            statChart.AxisXInterval = AxisXInterval;
            statChart.AxisXAutoMinimum = AxisXAutoMinimum;
            statChart.AxisXAutoMaximum = AxisXAutoMaximum;
            statChart.AxisXAutoInterval = AxisXAutoInterval;
            statChart.AxisYTitle = AxisYTitle;
            statChart.AxisYMin = AxisYMin;
            statChart.AxisYMax = AxisYMax;
            statChart.AxisYInterval = AxisYInterval;
            statChart.AxisYAutoMinimum = AxisYAutoMinimum;
            statChart.AxisYAutoMaximum = AxisYAutoMaximum;
            statChart.AxisYAutoInterval = AxisYAutoInterval;
            statChart.AxisYAllowBreak = AxisYAllowBreak;
            statChart.AxisYScaleBreak = AxisYScaleBreak;
            statChart.AxisXLogarithmic = AxisXLogarithmic;
            statChart.AxisYLogarithmic = AxisYLogarithmic;
            statChart.ShowLegend = ShowLegend;
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

            Update(this, new EventArgs());

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
            engine.Evaluate(string.Format("plot(NULL, bty = 'n', xlab = '{0}', ylab = '{1}', xlim=c({2}, {3}), ylim=c({4}, {5}))",
                AxisXTitle, AxisYTitle, AxisXMin, AxisXMax, AxisYMin, AxisYMax)
                );

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

                Color col = series.Color;
                if (col.Name == "Transparent") col = series.MarkerBorderColor;

                if (series.YAxisType == type && series.Points.Count > 0 && col.Name != "Transparent")
                {
                    BivariateSample data = GetDataPointValues(series);

                    var xvalues = engine.CreateNumericVector(data.X);
                    var yvalues = engine.CreateNumericVector(data.Y);
                    engine.SetSymbol("xvalues", xvalues);
                    engine.SetSymbol("yvalues", yvalues);
                    engine.Evaluate(string.Format("col1 = rgb({0}, {1}, {2}, {3})", ((double)col.R / 255d), ((double)col.G / 255d), ((double)col.B / 255d), ((double)col.A / 255d)));

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
                            engine.Evaluate("lines(yvalues ~ xvalues, col = col1, lty = " + (series.BorderDashStyle == ChartDashStyle.Dash ? "2" : "1") + ")");
                            if (series.IsVisibleInLegend)
                            {
                                engine.Evaluate("pchs = c(pchs, NA)");
                                engine.Evaluate("ltys = c(ltys, " + (series.BorderDashStyle == ChartDashStyle.Dash ? "2" : "1") + ")");
                                engine.Evaluate("fills = c(fills, NA)");
                                engine.Evaluate("borders = c(borders, NA)");
                                engine.Evaluate("lwds = c(lwds, 2)");
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

                if (i == Series.Count - 1 && HasSecondaryYAxis && type == AxisType.Primary)
                {
                    type = AxisType.Secondary;
                    i = 0;

                    engine.Evaluate("par(new = TRUE)");
                    engine.Evaluate(string.Format("plot(NULL, xlab = '', ylab = '', xlim=c({0}, {1}), ylim=c({2}, {3}), axes = F)", AxisXMin, AxisXMax, AxisY2Min, AxisY2Max));
                    engine.Evaluate("axis(side = 4)");
                    engine.Evaluate(string.Format("mtext('{0}', side = 4, line = 3)", AxisY2Title));
                }
            }

            if (ShowLegend)
            {
                var names = engine.CreateCharacterVector(seriesNames);
                engine.SetSymbol("names", names);
                engine.Evaluate("legend(x = 'topright', legend = names, col = cols, pch = pchs, fill = fills, bty = 'n', border = borders, lwd = lwds)");
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



        public void OpenRegressionProperties(Scatterplot scatterplot)
        {
            if (!scatterplot.Calc.IsRegressionOK) return;

            RegressionProperties properties = new RegressionProperties(scatterplot.Calc.Regression, false);
            properties.SetFriendlyDesktopLocation(contextScatterplot);
            properties.Show(this);

            scatterplot.Updated += properties.ChangeRegression;
        }

        public void OpenTrendProperties(Scatterplot scatterplot)
        {
            if (scatterplot.Properties.ShowTrend)
            {
                if (!scatterplot.Properties.Visible)
                {
                    //scatterplot.Properties.SetFriendlyDesktopLocation(contextScatterplot);
                    scatterplot.Properties.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.NextToHost);
                    scatterplot.Properties.Show();
                }
                else
                {
                    scatterplot.Properties.BringToFront();
                }

                scatterplot.Properties.ShowTrendTab();
            }
            else
            {
                scatterplot.Properties.ShowTrend = true;
                scatterplot.Update(scatterplot, new EventArgs());
            }
        }

        public void OpenTrendProperties(Functor functor)
        {
            if (!functor.Properties.Visible)
            {
                functor.Properties.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.NextToHost);
                functor.Properties.Show();
            }
            else
            {
                functor.Properties.BringToFront();
            }
        }




        #region Interface

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
                    contextScatterplotProperties_Click(contextScatterplotProperties, new EventArgs());
                }
                else if (series == ((Scatterplot)sample).Trend.Series)
                {
                    contextScatterplotTrend_Click(contextScatterplotTrend, new EventArgs());
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
                contextScatterplotTrend_Click(contextScatterplotTrend, new EventArgs());
            }

            if (sample is Histogramma)
            {
                contextHistogramAddFit_Click(contextScatterplotTrend, new EventArgs());
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

            RecalculateAxesProperties();
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

            Update(sender, e);
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
            contextScatterplotDelete.Enabled = Scatterplots.Count > 1;
            contextScatterplotDistinguish.Enabled = Scatterplots.Count > 1;
            //contextScatterplotTrendCompare.Enabled = SelectedScatterplots.Count > 1;

            bool trended = false;
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (scatterplot.Calc.IsRegressionOK)
                {
                    trended = true;
                    break;
                }
            }

            contextScatterplotTrend.Enabled = trended;

            contextScatterplotDescriptiveX.Text = string.Format(Resources.Interface.DescriptiveTitle, AxisXTitle);
            contextScatterplotDescriptiveY.Text = string.Format(Resources.Interface.DescriptiveTitle, AxisYTitle);
            contextScatterplotMerge.Enabled = SelectedScatterplots.Count > 1;
            contextScatterplotSplit.Enabled = SelectedScatterplots.Count == 1;

            contextScatterplotApart.Enabled = Scatterplots.Count > 1;

            bool hasColumn = false;
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (scatterplot.ColumnX != null && scatterplot.ColumnY != null)
                {
                    hasColumn = true;
                    break;
                }
            }

            contextScatterplotFindValue.Enabled = hasColumn;
            contextScatterplotUpdate.Enabled = hasColumn;
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
                OpenTrendProperties(scatterplot);
            }
        }

        private void contextScatterplotDistinguish_CheckedChanged(object sender, EventArgs e)
        {
            IsDistinguishingMode = contextScatterplotDistinguish.Checked;
            SeriesShowSelected();
        }

        private void contextScatterplotDelete_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                Remove(scatterplot.Properties.ScatterplotName);
            }
        }

        private void contextScatterplotDescriptiveX_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                string result = string.Empty;
                foreach (Scatterplot scatterplot in SelectedScatterplots)
                {
                    result += new SampleDisplay(scatterplot.Calc.Data.X).ToString();
                    result += Constants.Break;
                }
                Clipboard.SetText(result.TrimEnd('\n'));
            }
            else
            {
                foreach (Scatterplot scatterplot in SelectedScatterplots)
                {
                    SampleProperties properties = new SampleProperties(scatterplot.Calc.Data.X);
                    properties.SetFriendlyDesktopLocation(contextScatterplot);
                    properties.Show();
                }
            }
        }

        private void contextScatterplotDescriptiveY_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                string result = string.Empty;
                foreach (Scatterplot scatterplot in SelectedScatterplots)
                {
                    result += new SampleDisplay(scatterplot.Calc.Data.Y).ToString();
                    result += Constants.Break;
                }
                Clipboard.SetText(result.TrimEnd('\n'));
            }
            else
            {
                foreach (Scatterplot scatterplot in SelectedScatterplots)
                {
                    SampleProperties properties = new SampleProperties(scatterplot.Calc.Data.Y);
                    properties.SetFriendlyDesktopLocation(contextScatterplot);
                    properties.Show();
                }
            }
        }

        private void contextScatterplotTrend_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                OpenRegressionProperties(scatterplot);
            }
        }

        private void contextScatterplotTrendCompare_Click(object sender, EventArgs e)
        {
            List<Regression> regressions = new List<Regression>();

            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                if (scatterplot.Calc.IsRegressionOK)
                {
                    regressions.Add(scatterplot.Calc.Regression);
                }
            }

            if (regressions.Count > 1)
            {
                RegressionComparison regressionComparison = new RegressionComparison(regressions.ToArray());
                regressionComparison.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.Centered);
                regressionComparison.Show();
            }
        }

        private void contextScatterplotMerge_Click(object sender, EventArgs e)
        {
            BivariateSample mergedBivariate = new BivariateSample(AxisXTitle, AxisYTitle);
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                for (int i = 0; i < scatterplot.Calc.Data.Count; i++)
                {
                    mergedBivariate.Add(scatterplot.Calc.Data.X.ElementAt(i), scatterplot.Calc.Data.Y.ElementAt(i));
                }
            }

            Scatterplot mergedScatterplot = new Scatterplot(mergedBivariate, Resources.Interface.MergedBivariate);
            mergedScatterplot.Properties.DataPointSize = 12;
            AddSeries(mergedScatterplot);
            SetColorScheme();
        }

        private void contextScatterplotSplit_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                LaunchSeparation(scatterplot);
            }
        }

        private void contextScatterplotHistogramX_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                Histogramma histogram = new Histogramma(AxisXTitle, scatterplot.Calc.Data.X, IsChronic);
                histogram.ShowOnChart(this);
            }
        }

        private void contextItemScatterplotApart_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                foreach (Scatterplot scatterplot in SelectedScatterplots)
                {
                    Plot statChart = scatterplot.Copy().ShowOnChart();
                    statChart.AxisXTitle = AxisXTitle;
                    statChart.AxisYTitle = AxisYTitle;
                    statChart.Update(sender, e);
                }
            }
            else
            {
                Scatterplot.ShowOnChart(this, SelectedScatterplots);
            }
        }

        private void contextScatterplotUpdate_Click(object sender, EventArgs e)
        {
            foreach (Scatterplot scatterplot in SelectedScatterplots)
            {
                scatterplot.GetData();

                if (scatterplot.Calc.Data != null)
                {
                    scatterplot.BuildSeries();
                }

                scatterplot.InvokeChanged();
            }

            RecalculateAxesProperties();
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
                    histogram.Update(sender, e);
                }
            }
        }

        private void contextHistogramDistinguish_CheckedChanged(object sender, EventArgs e)
        {
            IsDistinguishingMode = contextHistogramDistinguish.Checked;
            if (!IsDistinguishingMode) Update(sender, e);
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

                for (int i = 0; i < histogram.DataSeries.Points.Count; i++)
                {
                    s += histogram.DataSeries.Points[i].YValues[0];
                }

                for (int i = 0; i < histogram.DataSeries.Points.Count; i++)
                {
                    if (ModifierKeys.HasFlag(Keys.Control))
                    {
                        result += histogram.DataSeries.Points[i].XValue.ToString(this.AxisXFormat) + Constants.Tab +
                            (histogram.DataSeries.Points[i].YValues[0] / s).ToString("P1") + Constants.Return;
                    }
                    else
                    {
                        result += histogram.DataSeries.Points[i].XValue.ToString(this.AxisXFormat) + Constants.Tab +
                            histogram.DataSeries.Points[i].YValues[0].ToString(this.AxisYFormat) + Constants.Return;
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