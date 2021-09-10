using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Mathematics
{
    public partial class MathAdapter : Component, ISerializable
    {
        private IContainer components;

        private ContextMenuStrip contextCharting;
        private ToolStripMenuItem itemHistogram;
        private ToolStripMenuItem itemScatterplot;
        private ToolStripMenuItem itemCrossChart;

        private ContextMenuStrip contextStat;
        private ToolStripMenuItem itemDescriptive;
        private ToolStripSeparator toolStripSeparatorDependance;
        private ToolStripMenuItem itemAnova;
        private ToolStripMenuItem itemRegression;
        private ToolStripSeparator toolStripSeparatorSeparation;
        private ToolStripMenuItem itemScatterClustering;
        private ToolStripSeparator toolStripSeparatorService;

        private ContextMenuStrip contextTable;
        private ToolStripMenuItem itemPivot;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem itemSettings;
        private ToolStripMenuItem itemSampleDescriptive;
        private ToolStripMenuItem itemCalc;

        private ContextMenuStrip contextValue;
        private ToolStripMenuItem itemCopyAsSample;
        private ToolStripMenuItem itemGroup;
        private SpreadSheet sheet;

        public SpreadSheet Sheet
        {
            set
            {
                if (value == null)
                {
                    sheet = null;
                    return;
                }

                sheet = (SpreadSheet)value;

                sheet.AddTableMenu(contextTable);

                sheet.ItemSave.Click -= sheet.ItemSave_Click;
                sheet.ItemSave.Click += ItemSave_Click;

                sheet.ItemCopyTable.Click -= sheet.ItemCopyTable_Click;
                sheet.ItemCopyTable.Click += ItemCopyTable_Click;

                sheet.ItemReport.Click -= sheet.ItemReport_Click;
                sheet.ItemReport.Click += ItemReport_Click;

                sheet.AddCellMenu(contextValue).Opening += contextValue_Opening;

                if (sheet.Parent == null)
                {
                    sheet.ParentChanged += sheet_ParentChanged;
                }
                else
                {
                    HandleMathButtons();
                }

                sheet.EnabledChanged += sheet_EnabledChanged;
                sheet.SortCompare += sheet_SortCompare;
                sheet.SelectionChanged += sheet_SelectionChanged;
                sheet.CellMouseDoubleClick += sheet_CellMouseDoubleClick;
            }

            get
            {
                return sheet;
            }
        }

        private void ItemSave_Click1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        [Category("Mayfly Events")]
        public event EventHandler OperatingChanged;

        private void sheet_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.ValueType == typeof(Sample))
            {
                if (e.CellValue1 == null) return;
                if (e.CellValue2 == null) return;
                e.SortResult = ((SampleDisplay)e.CellValue1).CompareTo(((SampleDisplay)e.CellValue2));
                e.Handled = true;
            }
        }

        private void sheet_EnabledChanged(object sender, EventArgs e)
        {
            if (chartButton != null) chartButton.Enabled = sheet.Enabled;
            if (statButton != null) statButton.Enabled = sheet.Enabled;
        }

        private void sheet_SelectionChanged(object sender, EventArgs e)
        {
            SelectionContainsNumbers = false;

            foreach (DataGridViewCell gridCell in sheet.SelectedCells)
            {
                if (gridCell.Value is double)
                {
                    SelectionContainsNumbers = true;
                    break;
                }

                //if (gridCell.Value is Sample)
                //{
                //    itemSampleDescriptive.GetCurrentParent().Items.AddRange(
                //        (gridCell.Value as Sample).GetMenu().Items);
                //    break;
                //}
            }

            SelectionContainsEnoughNumbers = false;
            int numbersCount = 0;

            foreach (DataGridViewCell gridCell in sheet.SelectedCells)
            {
                if (gridCell.Value is Sample)
                {
                    numbersCount += ((Sample)gridCell.Value).Count;
                }

                if (gridCell.Value is double)
                {
                    numbersCount++;
                }

                if (numbersCount >= UserSettings.RequiredSampleSize)
                {
                    SelectionContainsEnoughNumbers = true;
                    break;
                }
            }

            //if (sheet.Display != null)
            //{
            if (sheet.SelectedNonEmptyCellsCount > 1)
            {
                Sample sample = GetSelectedNumbers();

                if (sample == null)
                {
                    sheet.UpdateStatus(string.Format(Resources.Interface.SelectionCommon,
                        sheet.SelectedNonEmptyCellsCount));
                    //sheet.Display.Default = string.Format(Resources.Interface.SelectionCommon,
                    //    sheet.SelectedNonEmptyCellsCount); 
                    //sheet.Display.SetStatus(string.Format(Resources.Interface.SelectionCommon,
                    //    sheet.SelectedNonEmptyCellsCount));
                }
                else
                {
                    string format = "N" + sample.Precision();

                    string sampleDescription = string.Format(Resources.Interface.SelectionDetails,
                        sample.Count, sample.Minimum.ToString(format),
                        sample.Maximum.ToString(format), sample.Mean.ToString(format), sample.Sum().ToString(format));

                    sheet.UpdateStatus(sample.Count < sheet.SelectedNonEmptyCellsCount ?
                        string.Format(Resources.Interface.SelectionTotal, sampleDescription, sheet.SelectedNonEmptyCellsCount)
                        : sampleDescription);
                    //sheet.Display.Default = sample.Count < sheet.SelectedNonEmptyCellsCount ?
                    //    string.Format(Resources.Interface.SelectionTotal, sampleDescription, sheet.SelectedNonEmptyCellsCount) 
                    //    : sampleDescription;
                    //sheet.Display.SetStatus(sample.Count < sheet.SelectedNonEmptyCellsCount ?
                    //    string.Format(Resources.Interface.SelectionTotal, sampleDescription, sheet.SelectedNonEmptyCellsCount) 
                    //    : sampleDescription);
                }

                //sheet.Display.ResetStatus();
            }
            else
            {
                if (OperatingSheet == null)
                    sheet.UpdateStatus();
                //sheet.Display.StopProcessing();
            }
        }
    

        private void sheet_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SpreadSheet hittedSheet = (SpreadSheet)sender;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (e.ColumnIndex > -1 &&
                        e.RowIndex > -1 &&
                        hittedSheet[e.ColumnIndex, e.RowIndex].Value is Sample)
                    {
                        SampleProperties properties = new SampleProperties((Sample)hittedSheet[e.ColumnIndex, e.RowIndex].Value);
                        properties.SetFriendlyDesktopLocation(hittedSheet[e.ColumnIndex, e.RowIndex]);
                        properties.Show();
                    }

                    break;
            }
        }

        [Browsable(false)]
        public SpreadSheet OperatingSheet { internal set; get; }

        //[Browsable(false)]
        //public Display DisplayToolBox { private set; get; }

        [Browsable(false)]
        public bool SelectionContainsNumbers { private set; get; }

        [Browsable(false)]
        public bool SelectionContainsEnoughNumbers { private set; get; }

        private void sheet_ParentChanged(object sender, EventArgs e)
        {
            HandleMathButtons();
        }

        private Button chartButton;

        private Button statButton;

        private Pivot pivotBox;

        private Semipivot semipivotBox;



        public MathAdapter()
        {
            InitializeComponent();

            SelectionContainsNumbers = false;

            // Table
            itemGroup.Click += itemGroup_Click;
            itemPivot.Click += itemPivot_Click;
            //itemSave.Click += itemSave_Click;

            contextTable.Opening += contextTable_Opening;

            // Cell
            itemCalc.Click += itemCalc_Click;
            itemSampleDescriptive.Click += itemSampleDescriptive_Click;
            itemCopyAsSample.Click += itemCopyAsSample_Click;

            // Charting
            itemHistogram.Click += itemHistogram_Click;
            itemCrossChart.Click += itemCrossChart_Click;
            itemScatterplot.Click += itemScatterplot_Click;

            // Statistics
            itemDescriptive.Click += itemDescriptive_Click;

            itemRegression.Click += itemRegression_Click;
            itemAnova.Click += itemAnova_Click;

            itemScatterClustering.Click += itemScatterClustering_Click;
            

            // Service
            itemSettings.Click += itemSettings_Click;

            contextStat.Opening += contextStat_Opening;
        }

        public MathAdapter(IContainer container) : this()
        {
            container.Add(this);    
        }

        protected MathAdapter(SerializationInfo info, StreamingContext context) : this()
        {
            Sheet = (SpreadSheet)info.GetValue("sheet", typeof(SpreadSheet));
        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MathAdapter));
            this.contextCharting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemHistogram = new System.Windows.Forms.ToolStripMenuItem();
            this.itemScatterplot = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCrossChart = new System.Windows.Forms.ToolStripMenuItem();
            this.contextStat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemDescriptive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorDependance = new System.Windows.Forms.ToolStripSeparator();
            this.itemAnova = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRegression = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSeparation = new System.Windows.Forms.ToolStripSeparator();
            this.itemScatterClustering = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorService = new System.Windows.Forms.ToolStripSeparator();
            this.itemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPivot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSampleDescriptive = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCopyAsSample = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCharting.SuspendLayout();
            this.contextStat.SuspendLayout();
            this.contextTable.SuspendLayout();
            this.contextValue.SuspendLayout();
            // 
            // contextCharting
            // 
            this.contextCharting.BackColor = System.Drawing.SystemColors.Window;
            this.contextCharting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemHistogram,
            this.itemScatterplot,
            this.itemCrossChart});
            this.contextCharting.Name = "MenuCharting";
            resources.ApplyResources(this.contextCharting, "contextCharting");
            // 
            // itemHistogram
            // 
            this.itemHistogram.Name = "itemHistogram";
            resources.ApplyResources(this.itemHistogram, "itemHistogram");
            // 
            // itemScatterplot
            // 
            this.itemScatterplot.Name = "itemScatterplot";
            resources.ApplyResources(this.itemScatterplot, "itemScatterplot");
            // 
            // itemCrossChart
            // 
            this.itemCrossChart.Name = "itemCrossChart";
            resources.ApplyResources(this.itemCrossChart, "itemCrossChart");
            // 
            // contextStat
            // 
            this.contextStat.BackColor = System.Drawing.SystemColors.Window;
            this.contextStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemDescriptive,
            this.toolStripSeparatorDependance,
            this.itemAnova,
            this.itemRegression,
            this.toolStripSeparatorSeparation,
            this.itemScatterClustering,
            this.toolStripSeparatorService,
            this.itemSettings});
            this.contextStat.Name = "StatMenu";
            resources.ApplyResources(this.contextStat, "contextStat");
            // 
            // itemDescriptive
            // 
            this.itemDescriptive.Name = "itemDescriptive";
            resources.ApplyResources(this.itemDescriptive, "itemDescriptive");
            // 
            // toolStripSeparatorDependance
            // 
            this.toolStripSeparatorDependance.Name = "toolStripSeparatorDependance";
            resources.ApplyResources(this.toolStripSeparatorDependance, "toolStripSeparatorDependance");
            // 
            // itemAnova
            // 
            resources.ApplyResources(this.itemAnova, "itemAnova");
            this.itemAnova.Name = "itemAnova";
            // 
            // itemRegression
            // 
            this.itemRegression.Name = "itemRegression";
            resources.ApplyResources(this.itemRegression, "itemRegression");
            // 
            // toolStripSeparatorSeparation
            // 
            this.toolStripSeparatorSeparation.Name = "toolStripSeparatorSeparation";
            resources.ApplyResources(this.toolStripSeparatorSeparation, "toolStripSeparatorSeparation");
            // 
            // itemScatterClustering
            // 
            this.itemScatterClustering.Name = "itemScatterClustering";
            resources.ApplyResources(this.itemScatterClustering, "itemScatterClustering");
            // 
            // toolStripSeparatorService
            // 
            this.toolStripSeparatorService.Name = "toolStripSeparatorService";
            resources.ApplyResources(this.toolStripSeparatorService, "toolStripSeparatorService");
            // 
            // itemSettings
            // 
            this.itemSettings.Name = "itemSettings";
            resources.ApplyResources(this.itemSettings, "itemSettings");
            // 
            // contextTable
            // 
            this.contextTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemGroup,
            this.itemPivot,
            this.toolStripSeparator3});
            this.contextTable.Name = "TableMenu";
            resources.ApplyResources(this.contextTable, "contextTable");
            // 
            // itemGroup
            // 
            this.itemGroup.Name = "itemGroup";
            resources.ApplyResources(this.itemGroup, "itemGroup");
            // 
            // itemPivot
            // 
            this.itemPivot.Name = "itemPivot";
            resources.ApplyResources(this.itemPivot, "itemPivot");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // contextValue
            // 
            this.contextValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemCalc,
            this.itemSampleDescriptive,
            this.itemCopyAsSample});
            this.contextValue.Name = "contextValue";
            resources.ApplyResources(this.contextValue, "contextValue");
            this.contextValue.Opening += new System.ComponentModel.CancelEventHandler(this.contextValue_Opening);
            // 
            // itemCalc
            // 
            this.itemCalc.Name = "itemCalc";
            resources.ApplyResources(this.itemCalc, "itemCalc");
            // 
            // itemSampleDescriptive
            // 
            this.itemSampleDescriptive.Name = "itemSampleDescriptive";
            resources.ApplyResources(this.itemSampleDescriptive, "itemSampleDescriptive");
            // 
            // itemCopyAsSample
            // 
            this.itemCopyAsSample.Name = "itemCopyAsSample";
            resources.ApplyResources(this.itemCopyAsSample, "itemCopyAsSample");
            this.contextCharting.ResumeLayout(false);
            this.contextStat.ResumeLayout(false);
            this.contextTable.ResumeLayout(false);
            this.contextValue.ResumeLayout(false);

        }



        #region Methods

        private void HandleMathButtons()
        {
            if (sheet == null) return;

            if (sheet.IsLog && License.AllowedFeaturesLevel == FeatureLevel.Insider)
            {
                sheet.HandleButton(true, ref chartButton, Properties.Resources.Charts, buttonCharts_Click);
                sheet.HandleButton(true, ref statButton, Properties.Resources.Stat, buttonStat_Click);
            }
        }

        //public void SaveToFile(string filename)
        //{
        //    StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.Default);

        //    switch (Path.GetExtension(filename))
        //    {
        //        case ".csv":
        //            streamWriter.Write(sheet.SeparatedValues(';'));
        //            break;
        //        case ".txt":
        //            streamWriter.Write(sheet.SeparatedValues('\t'));
        //            break;
        //        case ".prn":
        //            streamWriter.Write(sheet.PrintableFile());
        //            break;
        //    }

        //    streamWriter.Flush();
        //    streamWriter.Close();
        //}

        public Sample GetSelectedNumbers()
        {
            List<double> data = new List<double>();

            foreach (DataGridViewCell gridCell in sheet.SelectedCells)
            {
                if (!gridCell.Visible) continue;

                if (gridCell.Value is Sample)
                {
                    data.AddRange((Sample)gridCell.Value);
                }
                else
                {
                    if (!gridCell.Value.IsDoubleConvertible()) continue;
                    data.Add(gridCell.Value.ToDouble());
                }
            }

            if (data.Count >= UserSettings.RequiredSampleSize)
            {
                Sample sample = new Sample(data)
                {
                    Name = Resources.Interface.CustomValues
                };

                return sample;
            }
            else
            {
                return null;
            }
        }



        #region Charting

        public void HandleHistogram(ColumnSelection columnSelection)
        {
            List<double> residuals = new List<double>();
            List<Histogramma> histograms = new List<Histogramma>();

            foreach (Sample sample in columnSelection.ValuesColumn.GetSamples(columnSelection.Groupers, true))
            {
                Histogramma histogram = new Histogramma(sample.Name, sample, columnSelection.ValuesColumn.ValueType == typeof(DateTime));

                if (histogram.Data.Count < UserSettings.RequiredSampleSize)
                {
                    residuals.AddRange(histogram.Data);
                    continue;
                }

                if (Form.ModifierKeys.HasFlag(Keys.Shift))
                {
                    histogram.ShowOnChart();
                }
                else
                {
                    histograms.Add(histogram);
                }
            }

            if (histograms.Count > 0)
            {
                ChartForm chartForm = new ChartForm
                {
                    Text = columnSelection.ValuesColumn.HeaderText
                };
                chartForm.StatChart.AxisXTitle = columnSelection.ValuesColumn.HeaderText;
                chartForm.StatChart.AxisYTitle = Resources.Interface.DefaultYTitle;
                chartForm.StatChart.IsChronic = histograms.ElementAt(0).IsChronic;

                foreach (Histogramma histogram in histograms)
                {
                    //histogram.Updated += new HistogramEventHandler(chartForm.StatChart.Redraw);
                    chartForm.StatChart.AddSeries(histogram);
                }

                if (residuals.Count >= UserSettings.RequiredSampleSize)
                {
                    Histogramma residualHistogram = new Histogramma(Resources.Interface.Others,
                        residuals, columnSelection.ValuesColumn.ValueType == typeof(DateTime));

                    //residualHistogram.Updated += new HistogramEventHandler(chartForm.StatChart.Redraw);
                    chartForm.StatChart.AddSeries(residualHistogram);
                }

                chartForm.StatChart.DoPlot();
                chartForm.Show();
            }
        }

        public void HandleScatterplot(ScatterplotColumnSelection columnSelection)
        {
            if (columnSelection.Grouping == null)
            {
                Scatterplot scatterplot = new Scatterplot(columnSelection.Argument,
                    columnSelection.Function, columnSelection.Labels);
                scatterplot.Properties.ShowTrend = scatterplot.Properties.ShowCount =
                    scatterplot.Properties.ShowExplained = columnSelection.AutoTrend;
                scatterplot.ShowOnChart();
            }
            else
            {
                List<Scatterplot> scatterplots = new List<Scatterplot>();
                List<string> variants = columnSelection.Grouping.GetStrings(true);

                foreach (string variant in variants)
                {
                    Scatterplot scatterplot;

                    if (columnSelection.Labels == null)
                    {
                        scatterplot = Scatterplot.GetByGroup(columnSelection.Argument,
                            columnSelection.Function,
                            columnSelection.Grouping, variant);
                    }
                    else
                    {
                        scatterplot = Scatterplot.GetByGroup(columnSelection.Argument,
                            columnSelection.Function, columnSelection.Labels,
                            columnSelection.Grouping, variant);
                    }

                    scatterplot.Properties.ShowTrend = columnSelection.AutoTrend;

                    scatterplot.Properties.ShowCount =
                        scatterplot.Properties.ShowExplained = 
                        (columnSelection.AutoTrend && variants.Count < 4);

                    if (scatterplot.Calc.Data.Count < UserSettings.RequiredSampleSize) continue;

                    if (Form.ModifierKeys.HasFlag(Keys.Control) &&
                        scatterplot.Calc.Data.Count < UserSettings.RequiredSampleSize)
                    {
                        continue;
                    }

                    if (Form.ModifierKeys.HasFlag(Keys.Shift))
                    {
                        Plot statChart = scatterplot.ShowOnChart(Form.ModifierKeys.HasFlag(Keys.Alt));
                        statChart.AxisXTitle = columnSelection.Argument.HeaderText;
                        statChart.AxisYTitle = columnSelection.Function.HeaderText;
                        statChart.DoPlot();
                    }
                    else
                    {
                        scatterplots.Add(scatterplot);
                    }
                }

                if (scatterplots.Count > 0)
                {
                    Scatterplot.ShowOnChart(columnSelection.Argument.HeaderText, columnSelection.Function.HeaderText, scatterplots);
                }
            }
        }

        public void HandleScatterplotClustering(ScatterplotColumnSelection columnSelection)
        {
            Scatterplot scatterplot = new Scatterplot(columnSelection.Argument, columnSelection.Function, columnSelection.Labels);
            Plot statChart = scatterplot.ShowOnChart();
            statChart.FindForm().DesktopLocation = new Point(100, 100);
            statChart.LaunchSeparation(scatterplot);
            //statChart.LaunchSeparation(scatterplot, new Point(statChart.FindForm().DesktopLocation.X + statChart.FindForm().Width,
            //    statChart.FindForm().DesktopLocation.Y));
        }

        public void HandleCrossChart(ColumnSelection columnSelection)
        {
            //List<DataGridViewColumn> list = new List<DataGridViewColumn>();
            //list.Add(columnSelection.ValuesColumn);
            //list.AddRange(columnSelection.Groupers);
            //DataTable table = sheet.GetData(list.ToArray());
            //Evaluation evaluation = new Evaluation(table.Columns[columnSelection.ValuesColumn.Name]);
            //evaluation.ShowOnChart();
        }

        #endregion



        #region Statistics

        public void HandleDescriptive(ColumnSelection columnSelection)
        {
            List<Sample> samples = columnSelection.ValuesColumn.GetSamples(columnSelection.Groupers, false);

            foreach (Sample sample in samples)
            {
                if (sample.Count > 0)
                {
                    SampleProperties result = new SampleProperties(sample);
                    result.SetFriendlyDesktopLocation(columnSelection.ValuesColumn);
                    result.Show();
                }
            }
        }

        public void HandleRegression(RegressionSelection columnSelection)
        {
            List<BivariateSample> samples = Service.GetSamples(columnSelection.Argument, 
                columnSelection.Function, columnSelection.Groupers);

            if (columnSelection.Compare)
            {
                RegressionComparison regressionComparison = new RegressionComparison();
                regressionComparison.SetFriendlyDesktopLocation(columnSelection.Function);
                regressionComparison.Show();
                regressionComparison.Run(samples);
            }
            else
            {
                //List<Regression> regressions = new List<Regression>();

                foreach (BivariateSample bivariateSample in samples)
                {
                    Regression regression = Regression.GetRegression(bivariateSample);

                    if (regression == null) continue;

                    regression.Name = bivariateSample.X.Name;
                    regression.Data.X.Name = columnSelection.Argument.HeaderText;
                    regression.Data.Y.Name = columnSelection.Function.HeaderText;

                    RegressionProperties properties = new RegressionProperties(regression, true);
                    properties.SetFriendlyDesktopLocation(columnSelection.Function);
                    properties.Show();
                }
            }
        }

        //public void HandleAnova(AnovaSelection columnSelection)
        //{
        //    List<DataGridViewColumn> list = new List<DataGridViewColumn>();
        //    list.Add(columnSelection.Dependent);
        //    list.AddRange(columnSelection.Independents);
        //    DataTable table = sheet.GetData(list.ToArray());

        //    try
        //    {
        //        AnovaProperties properties = new AnovaProperties(table.Columns[columnSelection.Dependent.Name]);
        //        properties.Show();
        //        properties.ShowGraph();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Write(e);

        //        if (MessageBox.Show(
        //            Resources.Messages.AnovaUnable +
        //            e.Message, EntryAssemblyInfo.Title,
        //            MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
        //        {
        //            Evaluation evaluation = new Evaluation(table.Columns[columnSelection.Dependent.Name]);
        //            evaluation.ShowOnChart();
        //        }
        //    }
        //}

        #endregion



        #region Operating data

        public void InitiateOperating(DataGridViewColumn grouperColumn)
        {
            OperatingSheet = new SpreadSheet
            {
                Location = sheet.Location,
                Size = sheet.Size,
                Anchor = sheet.Anchor,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ReadOnly = true
            };
            OperatingSheet.DefaultCellStyle.Padding = new Padding(0);
            //OperatingSheet.ScrollBars = ScrollBars.Both;


            //OperatingSheet.AddCellMenu(contextValue).Opening += contextValue_Opening;
            //Operatingsheet.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //Operatingsheet.RowTemplate.MinimumHeight = 35;

            OperatingSheet.InsertColumn(grouperColumn);
            grouperColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            OperatingSheet.Display = sheet.Display;
            OperatingSheet.CellFormatting += Operatingsheet_CellFormatting;
            OperatingSheet.SortCompare += sheet_SortCompare;
            OperatingSheet.Disposed += Operatingsheet_Disposed;
            OperatingSheet.SelectionChanged += sheet_SelectionChanged;
            OperatingSheet.CellMouseDoubleClick += sheet_CellMouseDoubleClick;

            sheet.Parent.Controls.Add(OperatingSheet);
            OperatingSheet.BringToFront();
            Sheet.UpdateStatus("Data operating");

            //DisplayToolBox = new Display();
            //DisplayToolBox.ParameterChanged += DisplayToolBox_ParameterChanged;
            //DisplayToolBox.Height = OperatingSheet.Height;
            //DisplayToolBox.SnapToRight(sheet);
            //DisplayToolBox.Show(sheet.FindForm());

            OperatingSheet.FindForm().LocationChanged += operatingBounds_Changed;
            OperatingSheet.FindForm().ResizeEnd += operatingBounds_Changed;
            OperatingSheet.VisibleChanged += Operatingsheet_VisibleChanged;

            if (OperatingChanged != null)
            {
                OperatingChanged.Invoke(OperatingSheet, new EventArgs());
            }
        }

        private void Operatingsheet_VisibleChanged(object sender, EventArgs e)
        {
            
            //if (OperatingSheet.Visible)
            //{
            //    DisplayToolBox.Show();
            //    DisplayToolBox.BringToFront();
            //}
            //else
            //{
            //    DisplayToolBox.Hide();
            //}
        }

        private void Operatingsheet_Disposed(object sender, EventArgs e)
        {
            if (OperatingChanged != null)
            {
                OperatingChanged.Invoke(OperatingSheet, new EventArgs());
            }

            Sheet.UpdateStatus();
        }

        private void Operatingsheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { }

        private void operatingBounds_Changed(object sender, EventArgs e)
        {
            if (OperatingSheet != null)
            {
                //DisplayToolBox.SnapToRight(sheet);
            }
        }

        public void ClearOperating()
        {
            if (OperatingSheet == null)
                throw new Exception("Operating spreadsheet is missing");

            while (OperatingSheet.ColumnCount > 1)
                OperatingSheet.Columns.RemoveAt(1);

            OperatingSheet.Rows.Clear();

            if (OperatingChanged != null)
            {
                OperatingChanged.Invoke(OperatingSheet, new EventArgs());
            }
        }

        private void DisplayToolBox_ParameterChanged(object sender, EventArgs e)
        {
            //foreach (DataGridViewColumn gridColumn in OperatingSheet.GetInsertedColumns())
            //{
            //    gridColumn.DefaultCellStyle.Format = DisplayToolBox.Format;
            //}

            OperatingSheet.Refresh();
        }

        #endregion

        #endregion



        #region Interface

        void contextTable_Opening(object sender, CancelEventArgs e)
        {        }

        void contextStat_Opening(object sender, CancelEventArgs e)
        { }

        private void buttonCharts_Click(object sender, EventArgs e)
        {
            contextCharting.Show((Control)sender, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void buttonStat_Click(object sender, EventArgs e)
        {
            contextStat.Show((Control)sender, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        #region Table menu

        private void ItemCopyTable_Click(object sender, EventArgs e)
        {
            if (OperatingSheet != null && OperatingSheet.Visible)
            {
                Clipboard.SetText(OperatingSheet.SeparatedValues(Constants.Tab));
            }
            else
            {
                Clipboard.SetText(Sheet.SeparatedValues(Constants.Tab));
            }
        }

        private void ItemReport_Click(object sender, EventArgs e)
        {
            if (OperatingSheet != null && OperatingSheet.Visible)
            {
                OperatingSheet.Print(string.Empty).Run();
            }
            else
            {
                Sheet.Print(string.Empty).Run();
            }
        }

        private void ItemSave_Click(object sender, EventArgs e)
        {
            if (IO.InterfaceSheets.ExportDialog.ShowDialog(this.sheet.FindForm()) == DialogResult.OK)
            {
                if (OperatingSheet != null && OperatingSheet.Visible)
                {
                    OperatingSheet.SaveToFile(IO.InterfaceSheets.ExportDialog.FileName);
                }
                else
                {
                    Sheet.SaveToFile(IO.InterfaceSheets.ExportDialog.FileName);
                }
            }
        }



        private void itemGroup_Click(object sender, EventArgs e)
        {
            if (pivotBox != null && pivotBox.Visible) pivotBox.Close();

            if (semipivotBox == null || semipivotBox.IsDisposed)
            {
                // Check if some rows are hidden
                if (Sheet.VisibleRowCount < Sheet.RowCount)
                {
                    Notification.ShowNotification(
                        Resources.Interface.PivotCautionTitle, 
                        Resources.Interface.PivotCautionInstruction,
                        hiddenNotificationGroup_Clicked);
                }


                semipivotBox = new Semipivot(this);
                //semipivotBox.ParameterChanged += pivotBox_ParameterChanged;
            }

            semipivotBox.Show(sheet.FindForm());

            //Semipivot box = new Semipivot(this);
            //box.Show(sheet.FindForm());
        }

        void hiddenNotificationGroup_Clicked(object sender, EventArgs e)
        {
            if (semipivotBox != null && semipivotBox.Visible) semipivotBox.Close();
            Sheet.itemShowAll_Click(sender, e);
            itemGroup_Click(sender, e);
        }


        private void itemPivot_Click(object sender, EventArgs e)
        {
            if (semipivotBox != null && semipivotBox.Visible) semipivotBox.Close();

            if (pivotBox == null || pivotBox.IsDisposed)
            {
                // Check if some rows are hidden
                if (Sheet.VisibleRowCount < Sheet.RowCount)
                {
                    Notification.ShowNotification(
                        Resources.Interface.PivotCautionTitle, 
                        Resources.Interface.PivotCautionInstruction,
                        hiddenNotificationPivot_Clicked);
                }


                pivotBox = new Pivot(this);
                pivotBox.ParameterChanged += pivotBox_ParameterChanged;
            }

            pivotBox.Show(sheet.FindForm());
        }

        void hiddenNotificationPivot_Clicked(object sender, EventArgs e)
        {
            if (pivotBox != null && pivotBox.Visible) pivotBox.Close();
            Sheet.itemShowAll_Click(sender, e);
            itemPivot_Click(sender, e);
        }

        void pivotBox_ParameterChanged(object sender, EventArgs e)
        { }

        #endregion

        #region Statistics Menu

        private void itemDescriptive_Click(object sender, EventArgs e)
        {
            ColumnSelection columnSelection = new ColumnSelection(sheet);

            if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            {
                HandleDescriptive(columnSelection);
            }
        }

        private void itemAnova_Click(object sender, EventArgs e)
        {
            //// Single Factor Anova
            //// 1 - Columns are factor values


            //AnovaSelection columnSelection = new AnovaSelection(sheet);
            //if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            //{
            //    HandleAnova(columnSelection);
            //}
        }

        private void itemRegression_Click(object sender, EventArgs e)
        {
            RegressionSelection columnSelection = new RegressionSelection(sheet);
            if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            {
                HandleRegression(columnSelection);
            }
        }

        private void itemScatterClustering_Click(object sender, EventArgs e)
        {
            ScatterplotColumnSelection columnSelection = new ScatterplotColumnSelection(sheet);
            columnSelection.ForbidGroupers();
            columnSelection.ForbidAutoTrend();
            if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            {
                HandleScatterplotClustering(columnSelection);
            }
        }



        private void itemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        #endregion

        #region Chart Menu

        private void itemHistogram_Click(object sender, EventArgs e)
        {
            ColumnSelection columnSelection = new ColumnSelection(sheet);

            if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            {
                HandleHistogram(columnSelection);
            }
        }

        private void itemScatterplot_Click(object sender, EventArgs e)
        {
            ScatterplotColumnSelection columnSelection = new ScatterplotColumnSelection(sheet);
            if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            {
                HandleScatterplot(columnSelection);
            }
        }

        private void itemCrossChart_Click(object sender, EventArgs e)
        {
            ColumnSelection columnSelection = new ColumnSelection(sheet);
            if (columnSelection.ShowDialog(sheet.FindForm()) == DialogResult.OK)
            {
                HandleCrossChart(columnSelection);
            }
        }

        #endregion

        #region Cell menu

        private void contextValue_Opening(object sender, CancelEventArgs e)
        {
            itemSampleDescriptive.Enabled = 
                itemCopyAsSample.Enabled =
                SelectionContainsEnoughNumbers;
        }

        private void itemSampleDescriptive_Click(object sender, EventArgs e)
        {
            Sample selectedNumbers = GetSelectedNumbers();

            if (selectedNumbers != null)
            {
                SampleProperties properties = new SampleProperties(selectedNumbers);
                properties.SetFriendlyDesktopLocation(sheet.CurrentCell);
                properties.Show();
            }
        }

        private void itemCopyAsSample_Click(object sender, EventArgs e)
        {
            SampleDisplay selectedNumbers = new SampleDisplay(GetSelectedNumbers());

            //if (selectedNumbers.Count >= UserSettings.StrongSampleSize)
            //{                
                if (Control.ModifierKeys.HasFlag(Keys.Control))
                {
                    Clipboard.SetText(selectedNumbers.ToString("F"));
                }
                else
                {
                    Clipboard.SetText(selectedNumbers.ToString());
                }
            //}
        }

        private void itemCalc_Click(object sender, EventArgs e)
        {
            Mayfly.Service.RunCalculator(new string[] { sheet.CurrentCell.Value.ToString() });
        }

        #endregion

        #endregion



        [SecurityPermissionAttribute(SecurityAction.Demand, 
            Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            if (info == null)
                throw new System.ArgumentNullException("info");

            info.AddValue("sheet", sheet);
        }
    }
}