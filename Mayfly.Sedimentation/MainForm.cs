using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Software;
using System.Resources;

namespace Mayfly.Sedimentation
{
    public partial class MainForm : Form
    {
        private string filename;

        public string FileName
        {
            set
            {
                this.ResetText(value == null ? IO.GetNewFileCaption(UserSettings.Interface.Extension) : value, EntryAssemblyInfo.Title);
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        SedimentProject Data;

        SedimentProject.ProjectRow Project { get; set; }

        double AxisXRange;

        //public double Volume
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxVolume.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double G
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxGgiWeight.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double DutyHourly
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDutyHourly.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double DutySecondly
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDutySecondly.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Density
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDensity.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Weight
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxWeight.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Z
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxZ.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Duration
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDurationHours.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Temperature
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxTemperature.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double ChannelWidth
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxWidth.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Depth
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDepth.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Velocity
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxVelocity.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double WaterSpend
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxWaterSpend.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double dP
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxGgiDisperse.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Fi
        //{
        //    get
        //    {
        //        try { return (double)numericUpDownFi.Value; }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double b
        //{
        //    get
        //    {
        //        try { return (double)numericUpDownTurbulentWidth.Value; }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double Entrainment
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxEntrainment.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double dPNgavtGeneral
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDpNgavtGeneral.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        //public double dPNgavt
        //{
        //    get
        //    {
        //        try { return Convert.ToDouble(textBoxDpNgavt.Text); }
        //        catch { return double.NaN; }
        //    }
        //}

        public double Omega
        {
            get
            {
                try { return Convert.ToDouble(textBoxHydraulicSize.Text); }
                catch { return double.NaN; }
            }
        }

        public double LethalL
        {
            get
            {
                try { return Convert.ToDouble(textBoxLethalLength.Text); }
                catch { return double.NaN; }
            }
        }

        public double LethalLoS
        {
            get
            {
                try { return Convert.ToDouble(textBoxLethalLengthOfSquare.Text); }
                catch { return double.NaN; }
            }
        }

        public MineralComposition Composition { set; get; }

        /// <summary>
        /// List of stretches with own compositions of sedimented fractions
        /// </summary>
        public List<MineralComposition> Sedimentation { set; get; }



        public MainForm()
        {
            InitializeComponent();

            Data = new SedimentProject();

            ColumnGrainHydraulicSize.ValueType = typeof(double);
            ColumnGrainMax.ValueType = typeof(double);
            ColumnGrainMin.ValueType = typeof(double);
            ColumnGrainSeparate.ValueType = typeof(double);
            ColumnGrainSize.ValueType = typeof(double);
            ColumnGrainSpread.ValueType = typeof(double);
            ColumnGrainWeight.ValueType = typeof(double);

            ColumnSedWeight.ValueType = typeof(double);
            ColumnSedDensity.ValueType = typeof(double);
            ColumnSedVolume.ValueType = typeof(double);
            ColumnSedWidth.ValueType = typeof(double);

            columnStretch.ValueType =
                ColumnStretchDepth.ValueType =
                ColumnStretchRatio.ValueType =
                ColumnStretchSuspense.ValueType =
                ColumnStretchVelocity.ValueType = typeof(double);

            //UpdateInitials();

            Clear();
            chart1.Format();
            RefreshChart(chart1);
            RefreshChart(chart2);

            Composition = new MineralComposition();
            Sedimentation = new List<MineralComposition>();
        }

        public MainForm(string filename)
            : this()
        {
            LoadData(filename);

            UpdateInitials();
        }




        private void UpdateInitials()
        {
            textBoxPerformanceSecondly.Text = Project.PerformanceSecondly.ToString("N5");
            textBoxDuration.Text = Project.Duration.ToString("N3");
            textBoxWeight.Text = Project.Weight.ToString("N3");
            //textBoxWaterSpend.Text = Project.WaterSpend.ToString("N3");

            if (!Project.IsWidthNull())
                numericUpDownTurbulentWidth.Maximum = (decimal)Project.Width;

            UpdateGgi();
            UpdateNgavt();
        }



        private void UpdateNgavt()
        {
            foreach (DataGridViewRow gridRow in spreadSheetNgavt.Rows)
            {
                UpdateNgavt(gridRow.Index);
            }

            UpdateNgavtTotals();
        }

        private void UpdateNgavt(int rowIndex)
        {
            if (rowIndex == -1) return;
            if (spreadSheetNgavt[columnStretch.Index, rowIndex].Value == null) return;

            SedimentProject.SectionRow ngavtRow = Data.Section.FindByID(rowIndex);

            double l1 = rowIndex == 0 ? 0 : (double)spreadSheetNgavt[columnStretch.Index, rowIndex - 1].Value;
            double l2 = (double)spreadSheetNgavt[columnStretch.Index, rowIndex].Value;

            double v = spreadSheetNgavt[ColumnStretchVelocity.Index, rowIndex].Value == null ?
                Project.IsVelocityNull() ? 0 : Project.Velocity :
                (double)spreadSheetNgavt[ColumnStretchVelocity.Index, rowIndex].Value;
            double h = spreadSheetNgavt[ColumnStretchDepth.Index, rowIndex].Value == null ?
                Project.IsDepthNull() ? 0 : Project.Depth :
                (double)spreadSheetNgavt[ColumnStretchDepth.Index, rowIndex].Value;

            double ratio = ModelNgavt.GetDilution(v, h, Omega, l2 - l1);
            spreadSheetNgavt[ColumnStretchRatio.Index, rowIndex].Value = ratio;

            double p = rowIndex == 0 ? (Project.Load * UserSettings.ControlPart) :
                (double)spreadSheetNgavt[ColumnStretchSuspense.Index, rowIndex - 1].Value;

            spreadSheetNgavt[ColumnStretchSuspense.Index, rowIndex].Value = ratio * p;
        }

        private void UpdateNgavtTotals()
        {
            if (Project.IsVelocityNull()) return;
            if (Project.IsDepthNull()) return;

            double length = ModelNgavt.DistanceOfTargetDilution(
                Project.Velocity,
                Project.Depth, Omega, 
                (double)numericUpDownDisperse.Value / (Project.Load * UserSettings.ControlPart)
                );
            textBoxLength.Text = length.ToString("N3");

            UpdateChartNgavt();
        }



        private void UpdateGgi()
        {
            textBoxDuration.Text = Project.Duration.ToString("N2");
            textBoxGgiWeight.Text = Project.WeightFlushed.ToString("N3");
            textBoxGgiDisperse.Text = Project.Load.ToString("N3");

            UpdateGrain();
            UpdateWeights();

            //numericUpDownLethalDisperse_ValueChanged(this, null);
            //numericUpDownLethalSilt_ValueChanged(this, null);
        }

        private void UpdateGrain()
        {
            foreach (DataGridViewRow gridRow in spreadSheetGrain.Rows)
            {
                if (gridRow.IsNewRow) continue;

                UpdateGrain(gridRow);
            }
        }

        private void UpdateGrain(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnGrainSize.Index].Value == null)
            {
                if (gridRow.Cells[ColumnGrainMin.Index].Value == null) return;
                if (gridRow.Cells[ColumnGrainMax.Index].Value == null) return;

                MineralFraction fraction = new MineralFraction(
                    (double)gridRow.Cells[ColumnGrainMin.Index].Value,
                    (double)gridRow.Cells[ColumnGrainMax.Index].Value);

                Composition.Add(fraction);

                gridRow.HeaderCell.Value = fraction.ToString();
                spreadSheetGrain[ColumnGrainSize.Index, gridRow.Index].Value = fraction.GrainSize;
            }
            else
            {
                MineralFraction fraction = Composition.Find((double)gridRow.Cells[ColumnGrainSize.Index].Value);

                double separate = gridRow.Cells[ColumnGrainSeparate.Index].Value == null ? 0D :
                    (double)gridRow.Cells[ColumnGrainSeparate.Index].Value;

                fraction.Value = Project.WeightFlushed * separate;

                gridRow.Cells[ColumnGrainKT.Index].Value = Project.IsTemperatureNull() ? 1 :
                    Service.TemperatureCorrectionFactor(fraction.GrainSize, Project.Temperature);

                double w = this.Project.IsTemperatureNull() ?
                        Service.HydraulicSize(fraction.GrainSize) :
                        Service.HydraulicSize(fraction.GrainSize, Project.Temperature);

                gridRow.Cells[ColumnGrainHydraulicSize.Index].Value = w;
                w *= .001;

                double L = Project.Depth * Project.Velocity / w;
                gridRow.Cells[ColumnGrainSpread.Index].Value = L;

                string zoneTitle = (gridRow.Index + 1).ToString();
                zoneTitle += (fraction.Value == 0) ? string.Empty : ": " + L.ToString("N1");
                spreadSheetSedimentation.Rows[gridRow.Index].HeaderCell.Value = zoneTitle;

                gridRow.Cells[ColumnGrainWeight.Index].Value = fraction.Value;

                UpdateSedimentation(gridRow.Index);

                if (spreadSheetGrain.ContainsFocus)
                    UpdateWeights();
            }
        }

        private void UpdateSedimentation(int grainIndex)
        {
            double c = 0.0;

            double L1 = 0.0;
            if (grainIndex > 0 && spreadSheetGrain[ColumnGrainSeparate.Index, grainIndex - 1].Value != null)
            {
                L1 = (double)spreadSheetGrain[ColumnGrainSpread.Index, grainIndex - 1].Value;
            }
            double L2 = 0.0;
            if (spreadSheetGrain[ColumnGrainSeparate.Index, grainIndex].Value != null)
            {
                L2 = (double)spreadSheetGrain[ColumnGrainSpread.Index, grainIndex].Value;
            }

            if (L2 - L1 > 0)
            {
                spreadSheetSedimentation[ColumnSedLength.Index, grainIndex].Value = L2 - L1;
                spreadSheetSedimentation[ColumnSedSquare.Index, grainIndex].Value = (L2 - L1) * Project.Width;
            }
            else
            {
                spreadSheetSedimentation[ColumnSedLength.Index, grainIndex].Value = null;
                spreadSheetSedimentation[ColumnSedSquare.Index, grainIndex].Value = null;
            }

            for (int i = 0; i < spreadSheetSedimentation.RowCount; i++)
            {
                MineralComposition sedimentComposition = Sedimentation[i];

                int columnIndex = spreadSheetSedimentation.GetColumn("ColumnSeparate" + grainIndex).Index;

                if (i > grainIndex)
                {
                    spreadSheetSedimentation[columnIndex, i].Value = Mayfly.Constants.Null;
                }
                else
                {
                    double w = 0.0;

                    if (spreadSheetGrain[ColumnGrainSeparate.Index, grainIndex].Value != null)
                    {
                        w = (double)spreadSheetGrain[ColumnGrainWeight.Index, grainIndex].Value;
                    }

                    if (spreadSheetGrain[ColumnGrainSeparate.Index, grainIndex].Value != null &&
                        spreadSheetGrain[ColumnGrainSeparate.Index, i].Value != null)
                    {
                        double l = (double)spreadSheetGrain[ColumnGrainSpread.Index, grainIndex].Value;
                        double li = (double)spreadSheetSedimentation[ColumnSedLength.Index, i].Value;

                        double g = w * (li / l);// -c;
                        sedimentComposition[grainIndex].Value = g;
                        spreadSheetSedimentation[columnIndex, i].Value = sedimentComposition[grainIndex].Value;

                        c += g;
                    }
                    else
                    {
                        spreadSheetSedimentation[columnIndex, i].Value = 0.0;
                    }
                }

                if (Sedimentation.Count <= i) Sedimentation.Add(sedimentComposition);
            }
        }

        private void UpdateWeights()
        {
            double wIntegral = 0.0;

            for (int i = 0; i < spreadSheetSedimentation.RowCount; i++)
            {
                MineralComposition composition = Sedimentation[i];
                double s = composition.Total;

                spreadSheetSedimentation[ColumnSedWeight.Index, i].Value = s;

                wIntegral += s;
                spreadSheetSedimentation[ColumnSedWeightIntegral.Index, i].Value = wIntegral;

                double wTransit = Project.WeightFlushed - wIntegral;
                spreadSheetSedimentation[ColumnSedWeightTransit.Index, i].Value = wTransit;

                double mu = wTransit * 1000000.0 / (Project.WaterSpend * Project.Duration * 3600.0);
                spreadSheetSedimentation[ColumnSedSuspended.Index, i].Value = mu;

                spreadSheetSedimentation[ColumnSedDensity.Index, i].Value = composition.GetSedimentDensity();
                UpdateSilt(i);
            }
        }

        private void UpdateSilt(int grainIndex)
        {
            if (spreadSheetSedimentation[ColumnSedDensity.Index, grainIndex].Value == null) return;

            if (spreadSheetGrain[ColumnGrainSeparate.Index, grainIndex].Value == null)
            {
                spreadSheetSedimentation[ColumnSedDensity.Index, grainIndex].Value = null;
                spreadSheetSedimentation[ColumnSedVolume.Index, grainIndex].Value = null;
                spreadSheetSedimentation[ColumnSedWidth.Index, grainIndex].Value = null;
            }
            else
            {
                double square = (double)spreadSheetSedimentation[ColumnSedSquare.Index, grainIndex].Value;
                double weight = (double)spreadSheetSedimentation[ColumnSedWeight.Index, grainIndex].Value;
                double density = (double)spreadSheetSedimentation[ColumnSedDensity.Index, grainIndex].Value;

                double volume = weight / density;
                double width = 1000 * volume / square;

                spreadSheetSedimentation[ColumnSedDensity.Index, grainIndex].Value = density;
                spreadSheetSedimentation[ColumnSedVolume.Index, grainIndex].Value = volume;
                spreadSheetSedimentation[ColumnSedWidth.Index, grainIndex].Value = width;
            }
        }

        private void UpdateChart()
        {
            //chart1.ChartAreas[1].AxisY.IsLogarithmic = checkBox1.Checked;

            chart1.Series["LethalDisperse"].Points.Clear();
            chart1.Series["Disperse"].Points.Clear();
            chart1.Series["LethalWidth"].Points.Clear();
            chart1.Series["Width"].Points.Clear();

            Series susLeathalSeries = chart1.Series["LethalDisperse"];
            Series susSeries = chart1.Series["Disperse"];
            Series susLeathalWidth = chart1.Series["LethalWidth"];
            Series widthSeries = chart1.Series["Width"];

            double lMax = 0;
            double susMax = 0;
            double widMax = 0;

            double prevL = 0;

            chart1.ChartAreas[1].AxisX.CustomLabels.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetGrain.Rows)
            {
                if (gridRow.Cells[ColumnGrainSeparate.Index].Value == null) continue;

                double l = (double)gridRow.Cells[ColumnGrainSpread.Index].Value;

                lMax = Math.Max(lMax, l);

                //if (spreadSheetSedimentation[ColumnSedWeight.Index, gridRow.Index].Value != null)
                //{
                //    double weight = (double)spreadSheetSedimentation[ColumnSedWeight.Index, gridRow.Index].Value;
                //    weightSeries.Points.AddXY(l, weight);
                //}

                chart1.ChartAreas[1].AxisX.CustomLabels.Add(prevL, l, (gridRow.Index + 1).ToString(), 1, LabelMarkStyle.LineSideMark);

                double x = l;

                if (checkBoxSmooth.Checked)
                {
                    x = prevL + 0.5 * (l - prevL);
                }
                else
                {
                    x = prevL;
                }

                if (spreadSheetSedimentation[ColumnSedSuspended.Index, gridRow.Index].Value != null)
                {
                    double suspended = (double)spreadSheetSedimentation[ColumnSedSuspended.Index, gridRow.Index].Value;
                    susSeries.Points.AddXY(x, suspended);
                    susMax = Math.Max(susMax, suspended);
                }

                if (spreadSheetSedimentation[ColumnSedWidth.Index, gridRow.Index].Value != null)
                {
                    double width = (double)spreadSheetSedimentation[ColumnSedWidth.Index, gridRow.Index].Value;
                    widthSeries.Points.AddXY(x, width);
                    widMax = Math.Max(widMax, width);
                }

                prevL = l;
            }

            susLeathalSeries.Points.AddXY(0, numericUpDownLethalDisperse.Value);
            DataPoint cross = new DataPoint(LethalL, (double)numericUpDownLethalDisperse.Value);
            cross.MarkerStyle = MarkerStyle.Circle;
            cross.MarkerSize = 7;
            susLeathalSeries.Points.Add(cross);
            susLeathalSeries.Points.AddXY(lMax, numericUpDownLethalDisperse.Value);

            susLeathalWidth.Points.AddXY(0, numericUpDownLethalSilt.Value);
            DataPoint crossWidth = new DataPoint(LethalLoS, (double)numericUpDownLethalSilt.Value);
            crossWidth.MarkerStyle = MarkerStyle.Circle;
            crossWidth.MarkerSize = 7;
            susLeathalWidth.Points.Add(crossWidth);
            susLeathalWidth.Points.AddXY(lMax, numericUpDownLethalSilt.Value);

            double rest = Math.IEEERemainder(lMax, Mayfly.Service.GetAutoInterval(lMax));
            foreach (ChartArea chartArea in chart1.ChartAreas)
            {
                chartArea.AxisX.Maximum = (rest > 0) ? lMax + (lMax - rest) : lMax - rest;
            }

            double susRest = Math.IEEERemainder(susMax, Mayfly.Service.GetAutoInterval(susMax));
            chart1.ChartAreas[0].AxisY.Maximum = (susRest > 0) ? susMax + (susMax - susRest) : susMax - susRest;

            double widthRest = Math.IEEERemainder(widMax, Mayfly.Service.GetAutoInterval(widMax));
            chart1.ChartAreas[1].AxisY.Maximum = (widthRest > 0) ? widMax + (widMax - widthRest) : widMax - widthRest;
        }

        private void RefreshChart(Chart chart)
        {
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                double yRight = chartArea.AxisY.Maximum;

                double xInterval = Mayfly.Service.GetAutoInterval(AxisXRange);
                double yInterval = Mayfly.Service.GetAutoInterval(yRight);

                try
                {
                    double chartHeight = chartArea.AxisY.ValueToPixelPosition(0) - chartArea.AxisY.ValueToPixelPosition(yRight);
                    chartArea.AxisY.AdjustIntervals(chartHeight, yInterval);
                }
                catch
                {
                    chartArea.AxisY.AdjustIntervals(Height, yInterval);
                }

                try
                {
                    double chartWidth = chartArea.AxisX.ValueToPixelPosition(AxisXRange);
                    chartArea.AxisX.AdjustIntervals(chartWidth, xInterval);
                }
                catch
                {
                    chartArea.AxisX.AdjustIntervals(Width, xInterval);
                }
            }
        }

        private void UpdateChartNgavt()
        {
            chart2.Series["LethalDisperse"].Points.Clear();
            chart2.Series["Disperse"].Points.Clear();
            chart2.Series["DisperseUI"].Points.Clear();

            Series susLeathalSeries = chart2.Series["LethalDisperse"];
            Series susSeries = chart2.Series["Disperse"];
            Series susSeriesUI = chart2.Series["DisperseUI"];

            double lMax = Convert.ToDouble(textBoxLength.Text);
            double susMax = 0;

            foreach (DataGridViewRow gridRow in spreadSheetNgavt.Rows)
            {
                if (gridRow.Cells[columnStretch.Index].Value == null) continue;

                double l = (double)gridRow.Cells[columnStretch.Index].Value;

                if (spreadSheetNgavt[ColumnStretchSuspense.Index, gridRow.Index].Value != null)
                {
                    double suspended = (double)spreadSheetNgavt[ColumnStretchSuspense.Index, gridRow.Index].Value;
                    susSeriesUI.Points.AddXY(l, suspended);
                    susMax = Math.Max(susMax, suspended);
                }
            }


            for (double l = 0; l < lMax; l += lMax / 50)
            {
                double sus = Project.WeightFlushed * UserSettings.ControlPart
                    * ModelNgavt.GetDilution(Project.Velocity, Project.Depth, Omega, l);
                DataPoint dp = new DataPoint(l, sus);
                dp.MarkerStyle = MarkerStyle.None;
                dp.IsValueShownAsLabel = false;
                dp.Label = string.Empty;
                susSeries.Points.Add(dp);
                susMax = Math.Max(susMax, sus);
            }

            susLeathalSeries.Points.AddXY(0, numericUpDownDisperse.Value);
            susLeathalSeries.Points.AddXY(lMax, numericUpDownDisperse.Value);

            double rest = Math.IEEERemainder(lMax, Mayfly.Service.GetAutoInterval(lMax));
            foreach (ChartArea chartArea in chart2.ChartAreas)
            {
                chartArea.AxisX.Maximum = (rest > 0) ? lMax + (lMax - rest) : lMax - rest;
            }

            double susRest = Math.IEEERemainder(susMax, Mayfly.Service.GetAutoInterval(susMax));
            chart2.ChartAreas[0].AxisY.Maximum = (susRest > 0) ? susMax + (susMax - susRest) : susMax - susRest;


        }



        private void Save()
        {
            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void Save(string filename)
        {
            SaveData();
            Data.WriteXml(filename);
            FileName = filename;
        }

        private void SaveInitials()
        {
            if (Project == null) Project = Data.Project.NewProjectRow();

            if (string.IsNullOrWhiteSpace(textBoxVolume.Text)) Project.SetVolumeNull();
            else Project.Volume = Convert.ToDouble(textBoxVolume.Text);

            if (string.IsNullOrWhiteSpace(textBoxPerformance.Text)) Project.SetPerformanceNull();
            else Project.Performance = Convert.ToDouble(textBoxPerformance.Text);

            if (string.IsNullOrWhiteSpace(textBoxDensity.Text)) Project.SetDensityNull();
            else Project.Density = Convert.ToDouble(textBoxDensity.Text);

            if (string.IsNullOrWhiteSpace(textBoxWidth.Text)) Project.SetWidthNull();
            else Project.Width = Convert.ToDouble(textBoxWidth.Text);

            if (string.IsNullOrWhiteSpace(textBoxDepth.Text)) Project.SetDepthNull();
            else Project.Depth = Convert.ToDouble(textBoxDepth.Text);

            if (string.IsNullOrWhiteSpace(textBoxVelocity.Text)) Project.SetVelocityNull();
            else Project.Velocity = Convert.ToDouble(textBoxVelocity.Text);

            if (string.IsNullOrWhiteSpace(textBoxTemperature.Text)) Project.SetTemperatureNull();
            else Project.Temperature = Convert.ToDouble(textBoxTemperature.Text);

            if (Project.Table == null) Data.Project.AddProjectRow(Project);
        }

        private void SaveNgavt()
        {
            Project.TurbulentWidth = (double)numericUpDownTurbulentWidth.Value;

            Project.Fi = (double)numericUpDownFi.Value;

            Data.Section.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetNgavt.Rows)
            {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[columnStretch.Index].Value == null) continue;

                SedimentProject.SectionRow ngavtRow = Data.Section.NewSectionRow();
                ngavtRow.ProjectRow = Project;

                ngavtRow.Distance = (double)gridRow.Cells[columnStretch.Index].Value;

                if (gridRow.Cells[ColumnStretchDepth.Index].Value != null)
                    ngavtRow.Depth = (double)gridRow.Cells[ColumnStretchDepth.Index].Value;

                if (gridRow.Cells[ColumnStretchVelocity.Index].Value != null)
                    ngavtRow.Velocity = (double)gridRow.Cells[ColumnStretchVelocity.Index].Value;

                Data.Section.AddSectionRow(ngavtRow);
            }
        }

        private void SaveGgi()
        {
            if (string.IsNullOrWhiteSpace(textBoxZ.Text)) Project.SetZNull();
            else Project.Z = Convert.ToDouble(textBoxZ.Text);

            Data.Composition.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetGrain.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SedimentProject.CompositionRow compositionRow = Data.Composition.NewCompositionRow();
                compositionRow.ProjectRow = Project;

                compositionRow.FractionFrom = (double)gridRow.Cells[ColumnGrainMin.Index].Value;
                compositionRow.FractionTo = (double)gridRow.Cells[ColumnGrainMax.Index].Value;
                if (gridRow.Cells[ColumnGrainSeparate.Index].Value != null)
                    compositionRow.Separate = (double)gridRow.Cells[ColumnGrainSeparate.Index].Value;

                Data.Composition.AddCompositionRow(compositionRow);
            }
        }

        private void SaveData()
        {
            SaveInitials();
            SaveNgavt();
            SaveGgi();
        }



        public void LoadData(string filename)
        {
            Clear();

            Data = new SedimentProject();
            Data.ReadXml(filename);

            Project = Data.Project[0];

            if (!Project.IsVolumeNull()) textBoxVolume.Text = Project.Volume.ToString();
            if (!Project.IsPerformanceNull()) textBoxPerformance.Text = Project.Performance.ToString();
            //if (!prjRow.IsDurationNull()) textBoxDuration.Text = prjRow.Duration.ToString();
            if (!Project.IsDensityNull()) textBoxDensity.Text = Project.Density.ToString();
            if (!Project.IsZNull()) textBoxZ.Text = Project.Z.ToString();
            if (!Project.IsWidthNull()) textBoxWidth.Text = Project.Width.ToString();
            if (!Project.IsDepthNull()) textBoxDepth.Text = Project.Depth.ToString();
            if (!Project.IsVelocityNull()) textBoxVelocity.Text = Project.Velocity.ToString();
            if (!Project.IsTemperatureNull()) textBoxTemperature.Text = Project.Temperature.ToString();
            if (!Project.IsTurbulentWidthNull()) numericUpDownTurbulentWidth.Value = (decimal)Project.TurbulentWidth;
            if (!Project.IsFiNull()) numericUpDownFi.Value = (decimal)Project.Fi;
            UpdateInitials();

            FileName = filename;

            if (Project.GetCompositionRows().Length > 0)
            {
                Composition = new MineralComposition();
                Sedimentation = new List<MineralComposition>();

                foreach (SedimentProject.CompositionRow row in Project.GetCompositionRows())
                {
                    int insertedRowIndex = spreadSheetGrain.Rows.Add(row.FractionFrom, row.FractionTo);
                }

                for (int i = 0; i < Composition.Count; i++)
                {
                    Sedimentation.Add(Composition.Copy());
                }

                int index = 0;
                foreach (SedimentProject.CompositionRow row in Project.GetCompositionRows())
                {
                    if (!row.IsSeparateNull())
                    {
                        spreadSheetGrain[ColumnGrainSeparate.Index, index].Value = row.Separate;
                    }
                    index++;
                }

                UpdateGgi();

                tabPageGgi.Parent = tabControl;
                tabControl.SelectedTab = tabPageGgi;
            }

            if (Project.GetSectionRows().Length > 0)
            {
                foreach (SedimentProject.SectionRow ngavtRow in Project.GetSectionRows())
                {
                    int insertedRowIndex = spreadSheetNgavt.Rows.Add(ngavtRow.Distance);

                    if (!ngavtRow.IsDepthNull())
                        spreadSheetNgavt[ColumnStretchDepth.Index, insertedRowIndex].Value = ngavtRow.Depth;

                    if (!ngavtRow.IsVelocityNull())
                        spreadSheetNgavt[ColumnStretchVelocity.Index, insertedRowIndex].Value = ngavtRow.Velocity;
                }

                UpdateNgavt();

                tabPageNgavt.Parent = tabControl;
                tabControl.SelectedTab = tabPageNgavt;
            }
        }

        private void Clear()
        {
            spreadSheetGrain.Rows.Clear();
            spreadSheetSedimentation.Rows.Clear();
            spreadSheetSedimentation.ClearInsertedColumns();

            spreadSheetNgavt.Rows.Clear();

            chart1.Series["Disperse"].Points.Clear();
            chart1.Series["Width"].Points.Clear();
            chart1.Series["LethalDisperse"].Points.Clear();
            chart1.Series["LethalWidth"].Points.Clear();

            textBoxVolume.Text =
                textBoxPerformance.Text =
                textBoxDensity.Text =
                textBoxZ.Text =
                textBoxWidth.Text =
                textBoxDepth.Text =
                textBoxVelocity.Text =
                textBoxTemperature.Text = string.Empty;

            tabPageGgi.Parent = tabPageNgavt.Parent = null;
        }



        private double LethalLengthOfVolume(double lethalDisperse)
        {
            if (checkBoxSmooth.Checked)
            {
                double d1 = 0;
                double d2 = 0;
                double l1 = 0;
                double l2 = 0;
                double prevL = 0;

                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedSuspended.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double disperse = (double)gridRow.Cells[ColumnSedSuspended.Index].Value;
                    double stretch = (double)gridRow.Cells[ColumnSedLength.Index].Value;

                    if (disperse > lethalDisperse)
                    {
                        d1 = disperse;
                        l1 = prevL + 0.5 * stretch;
                    }
                    else if (disperse == lethalDisperse)
                    {
                        return prevL;
                    }
                    else if (disperse <= lethalDisperse)
                    {
                        d2 = disperse;
                        l2 = prevL + 0.5 * stretch;
                        break;
                    }

                    prevL += stretch;
                }

                double deltaL = l2 - l1;
                double deltaD = Math.Abs(d2 - d1);
                double x = (deltaL * (Math.Max(d1, d2) - lethalDisperse)) / deltaD;
                return l1 + x;
            }
            else
            {
                double lengthSum = 0;

                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedSuspended.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double suspense = (double)gridRow.Cells[ColumnSedSuspended.Index].Value;
                    if (suspense > lethalDisperse)
                    {
                        lengthSum += (double)gridRow.Cells[ColumnSedLength.Index].Value;
                    }
                }

                return lengthSum;
            }
        }

        private double LethalVolume(double lethalDisperse)
        {
            double squareSum = 0;

            if (checkBoxSmooth.Checked)
            {
                double d1 = 0;
                double d2 = 0;
                double l1 = 0;
                double l2 = 0;
                double prevL = 0;

                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedSuspended.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double disperse = (double)gridRow.Cells[ColumnSedSuspended.Index].Value;
                    double square = (double)gridRow.Cells[ColumnSedSquare.Index].Value;
                    double stretch = (double)gridRow.Cells[ColumnSedLength.Index].Value;

                    if (disperse > lethalDisperse)
                    {
                        d1 = disperse;
                        l1 = prevL + 0.5 * stretch;
                        squareSum += square;
                    }
                    else if (disperse == lethalDisperse)
                    {
                        squareSum += square;
                        return squareSum;
                    }
                    else if (disperse <= lethalDisperse)
                    {
                        d2 = disperse;
                        l2 = prevL + 0.5 * stretch;
                        break;
                    }

                    prevL += stretch;
                }

                double deltaL = l2 - l1;
                double deltaD = Math.Abs(d2 - d1);
                double x = (deltaL * (Math.Max(d1, d2) - lethalDisperse)) / deltaD;
                return (l1 + x) * Project.Width * Project.Depth;
            }
            else
            {
                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedSuspended.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double suspense = (double)gridRow.Cells[ColumnSedSuspended.Index].Value;
                    if (suspense > lethalDisperse)
                    {
                        squareSum += (double)gridRow.Cells[ColumnSedSquare.Index].Value;
                    }
                }

                squareSum *= Project.Depth;
            }

            return squareSum;
        }



        private double LethalLengthOfSquare(double lethalSilt)
        {
            double lengthSum = 0;

            if (checkBoxSmooth.Checked)
            {
                double width1 = 0;
                double width2 = 0;
                double l1 = 0;
                double l2 = 0;
                double prevL = 0;

                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedSuspended.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double silt = (double)gridRow.Cells[ColumnSedWidth.Index].Value;
                    double square = (double)gridRow.Cells[ColumnSedSquare.Index].Value;
                    double stretch = (double)gridRow.Cells[ColumnSedLength.Index].Value;

                    if (silt > lethalSilt)
                    {
                        width1 = silt;
                        l1 = prevL + 0.5 * stretch;
                        lengthSum += stretch;
                    }
                    else if (silt == lethalSilt)
                    {
                        lengthSum += stretch;
                        return lengthSum;
                    }
                    else if (silt <= lethalSilt)
                    {
                        width2 = silt;
                        l2 = prevL + 0.5 * stretch;
                        break;
                    }

                    prevL += stretch;
                }

                double deltaL = l2 - l1;
                double deltaW = Math.Abs(width2 - width1);
                double x = (deltaL * (Math.Max(width1, width2) - lethalSilt)) / deltaW;
                return l1 + x;
            }
            else
            {
                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedWidth.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double silt = (double)gridRow.Cells[ColumnSedWidth.Index].Value;
                    double stretch = (double)gridRow.Cells[ColumnSedLength.Index].Value;
                    if (silt > lethalSilt)
                    {
                        lengthSum += stretch;
                    }
                }
            }

            return lengthSum;
        }

        private double LethalSquare(double lethalSilt)
        {
            double squareSum = 0;

            if (checkBoxSmooth.Checked)
            {
                double width1 = 0;
                double width2 = 0;
                double l1 = 0;
                double l2 = 0;
                double prevL = 0;

                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedSuspended.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double silt = (double)gridRow.Cells[ColumnSedWidth.Index].Value;
                    double square = (double)gridRow.Cells[ColumnSedSquare.Index].Value;
                    double stretch = (double)gridRow.Cells[ColumnSedLength.Index].Value;

                    if (silt > lethalSilt)
                    {
                        width1 = silt;
                        l1 = prevL + 0.5 * stretch;
                        squareSum += square;
                    }
                    else if (silt == lethalSilt)
                    {
                        squareSum += square;
                        return squareSum;
                    }
                    else if (silt <= lethalSilt)
                    {
                        width2 = silt;
                        l2 = prevL + 0.5 * stretch;
                        break;
                    }

                    prevL += stretch;
                }

                double deltaL = l2 - l1;
                double deltaW = Math.Abs(width2 - width1);
                double x = (deltaL * (Math.Max(width1, width2) - lethalSilt)) / deltaW;
                return (l1 + x) * Project.Width;
            }
            else
            {
                foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
                {
                    if (gridRow.Cells[ColumnSedWidth.Index].Value == null) continue;
                    if (gridRow.Cells[ColumnSedSquare.Index].Value == null) continue;

                    double silt = (double)gridRow.Cells[ColumnSedWidth.Index].Value;
                    double square = (double)gridRow.Cells[ColumnSedSquare.Index].Value;
                    if (silt > lethalSilt)
                    {
                        squareSum += square;
                    }
                }
            }

            return squareSum;
        }






        //public Report GetReport()
        //{
        //    ResourceManager resources = new ResourceManager(typeof(MainForm));

        //    Report report = new Report("Расчет осаждения взвешенных частиц в " +
        //    "малом водотоке " +
        //    "методом ГГИ"
        //    );

        //    Report.Table table1 = new Report.Table(resources.GetString("labelWorks.Text"));

        //    table1.StartRow();
        //    table1.AddCellPrompt(resources.GetString("labelVolume.Text"),
        //        Project.IsVolumeNull() ? string.Empty : Project.Volume.ToString("N3"));
        //    table1.AddCellPrompt(resources.GetString("labelPerformance.Text"),
        //        Project.IsPerformanceNull() ? string.Empty : Project.Performance.ToString("N3"));
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddCellPrompt(resources.GetString("labelPerformanceSecondly.Text"),
        //        Project.IsPerformanceNull() ? string.Empty : Project.PerformanceSecondly.ToString("N3"));
        //    table1.AddCellPrompt(resources.GetString("labelDuration.Text"),
        //        Project.Duration.ToString("N3"));
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddCellPrompt(resources.GetString("labelDensity.Text"),
        //        Project.IsDensityNull() ? string.Empty : Project.Density.ToString("N3"));
        //    table1.AddCellPrompt(resources.GetString("labelWeight.Text"),
        //        Project.IsVolumeNull() || Project.IsDensityNull() ? string.Empty : Project.Weight.ToString("N3"));
        //    table1.EndRow();

        //    report.AddTable(table1);

        //    Report.Table table2 = new Report.Table(resources.GetString("labelChannel.Text"));

        //    table2.StartRow();
        //    table2.AddCellPrompt(resources.GetString("labelWidth.Text"),
        //        Project.IsWidthNull() ? string.Empty : Project.Width.ToString("N3"));
        //    table2.AddCellPrompt(resources.GetString("labelDepth.Text"),
        //        Project.IsDepthNull() ? string.Empty : Project.Depth.ToString("N3"));
        //    table2.AddCellPrompt(resources.GetString("labelVelocity.Text"),
        //        Project.IsVelocityNull() ? string.Empty : Project.Velocity.ToString("N3"));
        //    table2.EndRow();

        //    table2.StartRow();
        //    table2.AddCellPrompt(resources.GetString("labelWaterSpend.Text"),
        //        Project.IsWidthNull() || Project.IsDepthNull() || Project.IsVelocityNull() ?
        //        string.Empty : Project.WaterSpend.ToString("N3"), 2);

        //    table2.AddCellPrompt(resources.GetString("labelTemperature.Text"),
        //        Project.IsTemperatureNull() ? string.Empty : Project.Temperature.ToString("N3"));
        //    table2.EndRow();

        //    report.AddTable(table2);

        //    if (Project.GetCompositionRows().Length > 0) // Метод ГГИ
        //    {
        //        Report.Table table3 = new Report.Table(
        //            resources.GetString("tabPageGgiInitials.Text") + " для " +
        //            resources.GetString("tabPageGgi.Text"));

        //        table3.StartRow();
        //        table3.AddCellPrompt(resources.GetString("labelZ.Text"), Project.Z.ToString("N3"), 2);
        //        table3.EndRow();

        //        table3.StartRow();

        //        table3.AddCellPrompt(resources.GetString("labelGgiWeight.Text"), Project.WeightFlushed.ToString("N3"));
        //        table3.AddCellPrompt(resources.GetString("labelGgiDisperse.Text"), Project.Load.ToString("N3"));
        //        table3.EndRow();

        //        report.AddTable(table3);


        //        Report.Table table4 = new Report.Table(
        //            resources.GetString("tabPageGrains.Text"));

        //        table4.StartRow();
        //        table4.AddHeaderCell("Фракция, мм (расчетный размер)");
        //        table4.AddHeaderCell(resources.GetString("ColumnGrainKT.HeaderText"));
        //        table4.AddHeaderCell(resources.GetString("ColumnGrainHydraulicSize.HeaderText"));
        //        table4.AddHeaderCell(resources.GetString("ColumnGrainSeparate.HeaderText"));
        //        table4.AddHeaderCell(resources.GetString("ColumnGrainWeight.HeaderText"));
        //        table4.AddHeaderCell(resources.GetString("ColumnGrainSpread.HeaderText"));
        //        table4.EndRow();

        //        foreach (SedimentProject.CompositionRow fractionRow in Project.GetCompositionRows())
        //        {
        //            if (fractionRow.IsSeparateNull()) continue;

        //            MineralFraction fraction = fractionRow.Fraction;
        //            fraction.Value = fractionRow.IsSeparateNull() ? 0 : Project.WeightFlushed * fractionRow.Separate;

        //            table4.StartRow();
        //            table4.AddCell(string.Format("{0} ({1:G2})", fraction, fraction.GrainSize));
        //            table4.AddCellRight(Project.IsTemperatureNull() ? 1 :
        //                Service.TemperatureCorrectionFactor(fraction.GrainSize, Project.Temperature));
        //            table4.AddCellRight(fractionRow.HydraulicSize, "G3");

        //            //if (fractionRow.IsSeparateNull())
        //            //{
        //            //    table4.AddCell();
        //            //    table4.AddCell();
        //            //    table4.AddCell();
        //            //}
        //            //else
        //            //{
        //                table4.AddCellRight(fractionRow.Separate, "P2");
        //                table4.AddCellRight(fraction.Value, "N3");
        //                table4.AddCellRight(fractionRow.SedimentationLongitude, "N2");
        //            //}
        //            table4.EndRow();
        //        }

        //        report.AddTable(table4);

        //        //Report.Table table1 = new Report.Table();

        //        //table1.StartRow();
        //        //report.AddHeaderCell(resources.GetString("ColumnSedLength.HeaderText"));
        //        //report.AddHeaderCell(resources.GetString("ColumnSedSquare.HeaderText"));
        //        //report.AddHeaderCell(resources.GetString("ColumnSedWeight.HeaderText"));
        //        //table1.EndRow();

        //        report.AddSubtitle3(resources.GetString("tabPageSedimentation.Text"));
        //        spreadSheetSedimentation.AddToReport(report);

        //        //report.AddTable(table1);

        //    }

        //    return report;

        //}





        private void MainForm_Load(object sender, EventArgs e)
        {

        }



        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mayfly.IO.RunFile(Application.ExecutablePath, "-run");
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                //if (CheckAndSave() != DialogResult.Cancel)
                //{
                LoadData(UserSettings.Interface.OpenDialog.FileName);
                //}
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (FileName == null)
            {
                menuItemSaveAs_Click(sender, e);
            }
            else
            {
                Save(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void menuItemPrint_Click(object sender, EventArgs e)
        {
            Project.GetReport().Run();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                UpdateNgavt();
            }
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            about.SetPowered(Properties.Resources.sriif, Resources.Powered.SRIIF);
            about.ShowDialog();
        }



        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void valueChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                SaveInitials();
                UpdateInitials();
            }
        }



        private void buttonGgi_Click(object sender, EventArgs e)
        {
            tabPageGgi.Parent = tabControl;
            tabControl.SelectedTab = tabPageGgi;
        }

        private void ggiChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                SaveGgi();
                UpdateGgi();
            }
        }

        private void valueAndGrainChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                SaveInitials();
                UpdateInitials();
                ngavt_Changed(sender, e);
            }
        }

        private void textBoxH_TextChanged(object sender, EventArgs e)
        {
            valueAndGrainChanged(sender, e);
            numericUpDownLethalDisperse_ValueChanged(sender, e);
        }

        private void buttonSetDefaultGrains_Click(object sender, EventArgs e)
        {
            spreadSheetGrain.Rows.Clear();
            spreadSheetSedimentation.Rows.Clear();
            spreadSheetSedimentation.ClearInsertedColumns();

            ColumnGrainMin.Visible = ColumnGrainMax.Visible = false;

            Composition = new MineralComposition();
            Sedimentation = new List<MineralComposition>();

            spreadSheetGrain.Rows.Add(5.0, 10.0);
            spreadSheetGrain.Rows.Add(2.0, 5.0);
            spreadSheetGrain.Rows.Add(1.0, 2.0);
            spreadSheetGrain.Rows.Add(0.5, 1.0);
            spreadSheetGrain.Rows.Add(0.2, 0.5);
            spreadSheetGrain.Rows.Add(0.10, 0.20);
            spreadSheetGrain.Rows.Add(0.05, 0.10);
            spreadSheetGrain.Rows.Add(0.01, 0.05);
            spreadSheetGrain.Rows.Add(0.005, 0.01);
            spreadSheetGrain.Rows.Add(0.001, 0.005);

            for (int i = 0; i < Composition.Count; i++)
            {
                Sedimentation.Add(Composition.Copy());
            }

            UpdateGrain();
        }

        private void spreadSheetGrain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (!spreadSheetGrain.ContainsFocus) return;
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;
            if (spreadSheetGrain.Columns[e.ColumnIndex].ReadOnly) return;

            UpdateGrain(spreadSheetGrain.Rows[e.RowIndex]);

            textBoxTotalSeparate.Text = ColumnGrainSeparate.GetDoubles().Sum()
                .ToString(ColumnGrainSeparate.DefaultCellStyle.Format);
        }

        private void spreadSheetGrain_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (spreadSheetGrain.Rows[e.RowIndex].IsNewRow) return;

            MineralFraction fraction = new MineralFraction(
                (double)spreadSheetGrain[ColumnGrainMin.Index, e.RowIndex].Value,
                (double)spreadSheetGrain[ColumnGrainMax.Index, e.RowIndex].Value);

            Composition.Add(fraction);
            //Sedimentation.Add(new MineralComposition());

            spreadSheetGrain.Rows[e.RowIndex].HeaderCell.Value = fraction.ToString();
            spreadSheetGrain[ColumnGrainSize.Index, e.RowIndex].Value = fraction.GrainSize;

            DataGridViewColumn gridColumn = spreadSheetSedimentation.InsertColumn(
                "ColumnSeparate" + spreadSheetGrain.Rows[e.RowIndex].Index, spreadSheetSedimentation.ColumnCount - 7);
            gridColumn.HeaderText = fraction.ToString();
            gridColumn.Width = 75;
            gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridColumn.Visible = checkBoxShowSeparates.Checked;

            spreadSheetSedimentation.Rows.Add();
        }

        private void spreadSheetGrain_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //spreadSheetSedimentation.Columns.Remove("ColumnSeparate" + spreadSheetGrain.Rows[e.RowIndex].Index);
        }

        private void spreadSheetSedimentation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == ColumnSedDensity.Index)
            {
                UpdateSilt(e.RowIndex);
            }

            if (e.ColumnIndex == ColumnSedSquare.Index)
            {
                numericUpDownLethalDisperse_ValueChanged(sender, e);
                numericUpDownLethalSilt_ValueChanged(sender, e);
            }

            UpdateChart();
        }

        private void checkBoxShowSeparates_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn gridColumn in spreadSheetSedimentation.GetInsertedColumns())
            {
                gridColumn.Visible = checkBoxShowSeparates.Checked;
            }
        }

        private void numericUpDownLethalDisperse_ValueChanged(object sender, EventArgs e)
        {
            textBoxLethalLength.Text = LethalLengthOfVolume((double)numericUpDownLethalDisperse.Value).ToString("N3");

            if (!Project.IsDepthNull())
                textBoxLethalVolume.Text = LethalVolume((double)numericUpDownLethalDisperse.Value).ToString("N3");

            UpdateChart();
        }

        private void numericUpDownLethalSilt_ValueChanged(object sender, EventArgs e)
        {
            textBoxLethalLengthOfSquare.Text = LethalLengthOfSquare((double)numericUpDownLethalSilt.Value).ToString("N1");
            textBoxLethalSquare.Text = LethalSquare((double)numericUpDownLethalSilt.Value).ToString("N1");
            UpdateChart();
        }

        private void chart_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            RefreshChart((Chart)sender);
        }

        private void chart_SizeChanged(object sender, EventArgs e)
        {
            RefreshChart((Chart)sender);
        }

        private void chart1_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            AxisXRange = chart1.ChartAreas[0].CursorX.SelectionEnd - chart1.ChartAreas[1].CursorX.SelectionStart;
        }

        private void checkBoxSmooth_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Disperse"].ChartType =
                checkBoxSmooth.Checked ? SeriesChartType.Line : SeriesChartType.StepLine;
            chart1.Series["Width"].ChartType =
                checkBoxSmooth.Checked ? SeriesChartType.Line : SeriesChartType.StepLine;

            numericUpDownLethalDisperse_ValueChanged(sender, e);
            numericUpDownLethalSilt_ValueChanged(sender, e);
        }




        private void buttonNgavt_Click(object sender, EventArgs e)
        {
            tabPageNgavt.Parent =
                tabControl;

            tabControl.SelectedTab = tabPageNgavt;


        }

        private void ngavt_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                if (Project.IsVelocityNull()) return;
                if (Project.IsFiNull()) return;

                Project.Z = Service.SoilEntrainmentNomogram(Project.Velocity, Project.Obstruction, Project.Fi, SandSize.Fine);

                textBoxStreamObstruction.Text = Project.Obstruction.ToString("N3");
                textBoxEntrainment.Text = (100 * Project.Z).ToString("N1");

                textBoxDpNgavtGeneral.Text = Project.Load.ToString("N3");
                textBoxDpNgavt.Text = (Project.Load * UserSettings.ControlPart).ToString("N3");
                textBoxHydraulicSize.Text = Service.HydraulicSize(UserSettings.ControlSize).ToString("N3");

                UpdateNgavt();
            }
        }

        private void spreadSheetNgavt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateNgavt(e.RowIndex);
            if (e.RowIndex > -1) UpdateChartNgavt();
        }

        private void numericUpDownDisperse_ValueChanged(object sender, EventArgs e)
        {
            UpdateNgavtTotals();
        }
    }
}