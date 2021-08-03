using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Windows.Forms.DataVisualization.Charting;
using System.Resources;

namespace Mayfly.Sedimentation
{
    public partial class WizardSed : Form
    {
        public SedimentProject Data;
        SedimentProject.ProjectRow Project { get; set; }
        public ModelLoad Model { get; set; }
        public string FileName { get; set; }

        double maxDistance, selectedDistanceFrom, selectedDistanceTo;
        bool nomogramUsedForZ;



        public WizardSed()
        {
            InitializeComponent();

            Data = new SedimentProject();
            Project = Data.Project.NewProjectRow();
            Data.Project.AddProjectRow(Project);

            ColumnSectionDistance.ValueType =
                ColumnSectionDepth.ValueType =
                ColumnSectionRatio.ValueType =
                ColumnSectionLoad.ValueType =
                ColumnSectionVelocity.ValueType = typeof(double);

            ColumnGrainMax.ValueType = typeof(double);
            ColumnGrainMin.ValueType = typeof(double);
            ColumnGrainSeparate.ValueType = typeof(double);
            ColumnGrainWeight.ValueType = typeof(double);

            ColumnZoneSize.ValueType = typeof(double);
            ColumnZoneKT.ValueType = typeof(double);
            ColumnZoneHydraulicSize.ValueType = typeof(double);
            ColumnZoneLongitude.ValueType = typeof(double);

            ColumnSedFrom.ValueType = typeof(double);
            ColumnSedTo.ValueType = typeof(double);
            ColumnSedLength.ValueType = typeof(double);
            ColumnSedSquare.ValueType = typeof(double);
            ColumnSedWeight.ValueType = typeof(double);
            ColumnSedWeightIntegral.ValueType = typeof(double);
            ColumnSedWeightTransit.ValueType = typeof(double);
            ColumnSedLoad.ValueType = typeof(double);
            ColumnSedDensity.ValueType = typeof(double);
            ColumnSedVolume.ValueType = typeof(double);
            ColumnSedWidth.ValueType = typeof(double);

            ColumnLoadFrom.ValueType = typeof(double);
            ColumnLoadTo.ValueType = typeof(double);
            ColumnLoadLongitude.ValueType = typeof(double);
            ColumnLoadSquare.ValueType = typeof(double);
            ColumnLoadVolume.ValueType = typeof(double);
            ColumnLoadWaterVolume.ValueType = typeof(double);

            ColumnSiltFrom.ValueType = typeof(double);
            ColumnSiltTo.ValueType = typeof(double);
            ColumnSiltLongitude.ValueType = typeof(double);
            ColumnSiltSquare.ValueType = typeof(double);

            textBoxD.Text = UserSettings.ControlSize.ToString();
            textBoxControlPart.Text = UserSettings.ControlPart.ToString();
            CheckBoxNgavt_CheckedChanged(checkBoxNgavt, new EventArgs());
        }

        public WizardSed(string filename)
            : this()
        {
            FileName = filename;
            Load += WizardSed_Load;
        }

        private void WizardSed_Load(object sender, EventArgs e)
        {
            LoadData(FileName);
        }

        public void LoadData(string fileName)
        {
            Clear();

            FileName = fileName;

            Data = new SedimentProject();
            Data.ReadXml(fileName);

            Project = Data.Project[0];

            LoadTitle();

            wizardControl1.NextPage();

            LoadWorks();

            if (!double.IsNaN(Project.Weight))
            {
                wizardControl1.NextPage();
            }

            LoadNaturals();

            if (!double.IsNaN(Project.WaterSpend))
            {
                wizardControl1.NextPage();
            }

            LoadEntrainment();

            if (!double.IsNaN(Project.WeightFlushed))
            {
                wizardControl1.NextPage();
            }

            LoadControlParticles();

            LoadSections();

            LoadComposition();

            LoadLoad();

            LoadSilt();
        }

        private void Clear()
        {
            spreadSheetGrain.Rows.Clear();
            spreadSheetSedimentation.Rows.Clear();
            spreadSheetSedimentation.ClearInsertedColumns();

            textBoxVolume.Text =
                textBoxPerformance.Text =
                textBoxDensity.Text =

                textBoxWidth.Text =
                textBoxDepth.Text =
                textBoxVelocity.Text =
                textBoxTemperature.Text =

                textBoxZ.Text =

                string.Empty;
        }

        private void Save(string fileName)
        {
            Data.WriteXml(fileName);
            FileName = fileName;
        }



        private void LoadTitle()
        {
            if (!Project.IsTitleNull()) textBoxTitle.Text = Project.Title;
            if (!Project.IsWaterNull()) textBoxWater.Text = Project.Water;
            if (!Project.IsStretchNull()) textBoxStretch.Text = Project.Stretch;
            if (!Project.IsWorkTypeNull()) textBoxWorkType.Text = Project.WorkType;
            if (!Project.IsStartNull()) dateStart.Value = Project.Start;
            if (!Project.IsEndNull()) dateEnd.Value = Project.End;
        }

        private void SaveTitle()
        {
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text)) Project.SetTitleNull(); else Project.Title = textBoxTitle.Text;
            if (string.IsNullOrWhiteSpace(textBoxWater.Text)) Project.SetWaterNull(); else Project.Water = textBoxWater.Text;
            if (string.IsNullOrWhiteSpace(textBoxStretch.Text)) Project.SetStretchNull(); else Project.Stretch = textBoxStretch.Text;
            if (string.IsNullOrWhiteSpace(textBoxWorkType.Text)) Project.SetWorkTypeNull(); else Project.WorkType = textBoxWorkType.Text;

            Project.Start = dateStart.Value.Date;

            Project.End = dateEnd.Value.Date;
        }



        private void LoadWorks()
        {
            if (!Project.IsVolumeNull()) textBoxVolume.Text = Project.Volume.ToString();
            if (!Project.IsPerformanceNull()) textBoxPerformance.Text = Project.Performance.ToString();
            if (!Project.IsDensityNull()) textBoxDensity.Text = Project.Density.ToString();
            if (!Project.IsWorkWidthNull()) textBoxWorkWidth.Text = Project.WorkWidth.ToString();
            if (!Project.IsWorkLongitudeNull()) textBoxWorkLongitude.Text = Project.WorkLongitude.ToString();
            if (!Project.IsLeftGapNull()) textBoxLeftGap.Text = Project.LeftGap.ToString();
            if (!Project.IsLateralFlowNull()) { checkBoxLateralFlow.Checked = true; numericLateralFlow.Text = Project.LateralFlow.ToString(); }
            UpdateWorks();
        }

        private void SaveWorks()
        {
            if (string.IsNullOrWhiteSpace(textBoxVolume.Text)) Project.SetVolumeNull(); else Project.Volume = Convert.ToDouble(textBoxVolume.Text);
            if (string.IsNullOrWhiteSpace(textBoxPerformance.Text)) Project.SetPerformanceNull(); else Project.Performance = Convert.ToDouble(textBoxPerformance.Text);
            if (string.IsNullOrWhiteSpace(textBoxDensity.Text)) Project.SetDensityNull(); else Project.Density = Convert.ToDouble(textBoxDensity.Text);
            if (string.IsNullOrWhiteSpace(textBoxWorkWidth.Text)) Project.SetWorkWidthNull(); else Project.WorkWidth = Convert.ToDouble(textBoxWorkWidth.Text);
            if (string.IsNullOrWhiteSpace(textBoxWorkLongitude.Text)) Project.SetWorkLongitudeNull(); else Project.WorkLongitude = Convert.ToDouble(textBoxWorkLongitude.Text);
            if (string.IsNullOrWhiteSpace(textBoxLeftGap.Text)) Project.SetLeftGapNull(); else Project.LeftGap = Convert.ToDouble(textBoxLeftGap.Text);
            if (checkBoxLateralFlow.Checked) Project.LateralFlow = Convert.ToDouble(numericLateralFlow.Value); else Project.SetLateralFlowNull();
        }

        private void UpdateWorks()
        {
            try
            {
                textBoxWorkSquare.Text = Project.WorkSquare.ToString("N3");
                textBoxPerformanceSecondly.Text = Project.PerformanceSecondly.ToString("N5");
                textBoxDuration.Text = Project.Duration.ToString("N3");
                textBoxWeight.Text = Project.Weight.ToString("N3");
                textBoxRightGap.Text = Project.RightGap.ToString("N3");
                textBoxWaterSpend.Text = Project.WaterSpend.ToString("N3");

                UpdateEntrainment();
            }
            catch { }
        }



        private void LoadNaturals()
        {
            if (!Project.IsWidthNull()) textBoxWidth.Text = Project.Width.ToString();
            if (!Project.IsDepthNull()) textBoxDepth.Text = Project.Depth.ToString();
            if (!Project.IsVelocityNull()) textBoxVelocity.Text = Project.Velocity.ToString();
            if (!Project.IsTemperatureNull()) textBoxTemperature.Text = Project.Temperature.ToString();
            if (!Project.IsLoadNaturalNull()) textBoxLoadNatural.Text = Project.LoadNatural.ToString();
            UpdateNaturals();
        }

        private void SaveNaturals()
        {
            if (string.IsNullOrWhiteSpace(textBoxWidth.Text)) Project.SetWidthNull();
            else Project.Width = Convert.ToDouble(textBoxWidth.Text);

            if (string.IsNullOrWhiteSpace(textBoxDepth.Text)) Project.SetDepthNull();
            else Project.Depth = Convert.ToDouble(textBoxDepth.Text);

            if (string.IsNullOrWhiteSpace(textBoxVelocity.Text)) Project.SetVelocityNull();
            else Project.Velocity = Convert.ToDouble(textBoxVelocity.Text);

            if (string.IsNullOrWhiteSpace(textBoxTemperature.Text)) Project.SetTemperatureNull();
            else Project.Temperature = Convert.ToDouble(textBoxTemperature.Text);

            if (string.IsNullOrWhiteSpace(textBoxLoadNatural.Text)) Project.SetLoadNaturalNull();
            else Project.LoadNatural = Convert.ToDouble(textBoxLoadNatural.Text);
        }

        private void UpdateNaturals()
        {
            textBoxWaterSpend.Text = Project.WaterSpend.ToString("N3");
        }



        private void LoadEntrainment()
        {
            if (!Project.IsZNull()) textBoxZ.Text = Project.Z.ToString();
            UpdateEntrainment();
        }

        private void SaveEntrainment()
        {
            if (string.IsNullOrWhiteSpace(textBoxZ.Text)) Project.SetZNull();
            else Project.Z = Convert.ToDouble(textBoxZ.Text);
        }

        private void UpdateEntrainment()
        {
            textBoxGgiFlushed.Text = Project.WeightFlushed.ToString("N3");
            textBoxGgiLoad.Text = Project.Load.ToString("N3");
        }



        private void LoadControlParticles()
        {
            if (!Project.IsdNull()) textBoxD.Text = Project.d.ToString();
            if (!Project.IsControlPartNull()) textBoxControlPart.Text = Project.ControlPart.ToString();
            UpdateControlParticles();
        }

        private void SaveControlParticles()
        {
            if (string.IsNullOrWhiteSpace(textBoxD.Text)) Project.SetdNull();
            else Project.d = Convert.ToDouble(textBoxD.Text);

            if (string.IsNullOrWhiteSpace(textBoxControlPart.Text)) Project.SetControlPartNull();
            else Project.ControlPart = .01 * Convert.ToDouble(textBoxControlPart.Text);
        }

        private void UpdateControlParticles()
        {
            textBoxHydraulicSize.Text = Project.ControlHydraulicSize.ToString("N3");
            textBoxControlLoad.Text = Project.ControlLoad.ToString("N3");
        }



        private void LoadSections()
        {
            foreach (SedimentProject.SectionRow row in Project.GetSectionRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSections);

                gridRow.Cells[ColumnSectionDistance.Index].Value = row.Distance;

                if (!row.IsVelocityNull())
                    gridRow.Cells[ColumnSectionVelocity.Index].Value = row.Velocity;

                if (!row.IsDepthNull())
                    gridRow.Cells[ColumnSectionDepth.Index].Value = row.Depth;

                spreadSheetSections.Rows.Add(gridRow);
            }
        }

        private void SaveSections()
        {
            Data.Section.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetSections.Rows)
            {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[ColumnSectionDistance.Index].Value == null) continue;

                SedimentProject.SectionRow sectionRow = Data.Section.NewSectionRow();
                sectionRow.ProjectRow = Project;

                sectionRow.Distance = (double)gridRow.Cells[ColumnSectionDistance.Index].Value;

                if (gridRow.Cells[ColumnSectionDepth.Index].Value != null)
                    sectionRow.Depth = (double)gridRow.Cells[ColumnSectionDepth.Index].Value;

                if (gridRow.Cells[ColumnSectionVelocity.Index].Value != null)
                    sectionRow.Velocity = (double)gridRow.Cells[ColumnSectionVelocity.Index].Value;

                Data.Section.AddSectionRow(sectionRow);
            }

            Model = new ModelNgavt(Project);
        }

        private void SetSectionLines(double interval)
        {
            buttonSetSections.Enabled = false;
            pageNgavtLoad.SetNavigation(false);
            calcNgavtModel.RunWorkerAsync(interval);
        }



        private void LoadComposition()
        {
            spreadSheetGrain.Rows.Clear();

            foreach (SedimentProject.CompositionRow row in Project.GetCompositionRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGrain);

                gridRow.Cells[ColumnGrainMin.Index].Value = row.FractionFrom;
                gridRow.Cells[ColumnGrainMax.Index].Value = row.FractionTo;
                if (!row.IsSeparateNull()) { gridRow.Cells[ColumnGrainSeparate.Index].Value = 100.0 * row.Separate; }
                gridRow.Cells[ColumnGrainWeight.Index].Value = row.Weight;

                spreadSheetGrain.Rows.Add(gridRow);
            }

            textBoxTotalSeparate.Text = (ColumnGrainSeparate.GetDoubles().Sum())
                .ToString(ColumnGrainSeparate.DefaultCellStyle.Format);
        }

        private void SaveComposition()
        {
            Data.Composition.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetGrain.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SedimentProject.CompositionRow compositionRow = Data.Composition.NewCompositionRow();
                compositionRow.ProjectRow = Project;

                compositionRow.FractionFrom = (double)gridRow.Cells[ColumnGrainMin.Index].Value;
                compositionRow.FractionTo = (double)gridRow.Cells[ColumnGrainMax.Index].Value;

                if (gridRow.Cells[ColumnGrainSeparate.Index].Value != null)
                    compositionRow.Separate = .01 * (double)gridRow.Cells[ColumnGrainSeparate.Index].Value;

                Data.Composition.AddCompositionRow(compositionRow);
            }
        }



        private void UpdateZones()
        {
            spreadSheetZone.Rows.Clear();

            foreach (SedimentProject.CompositionRow row in Project.GetCompositionRows())
            {
                MineralFraction frac = row.Fraction;

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetZone);

                gridRow.HeaderCell.Value = frac.ToString();

                gridRow.Cells[ColumnZoneSize.Index].Value = frac.GrainSize;
                gridRow.Cells[ColumnZoneKT.Index].Value = Project.IsTemperatureNull() ? 1 :
                    Service.TemperatureCorrectionFactor(frac.GrainSize, Project.Temperature);
                gridRow.Cells[ColumnZoneHydraulicSize.Index].Value = row.HydraulicSize;
                gridRow.Cells[ColumnZoneLongitude.Index].Value = row.SedimentationLongitude;

                spreadSheetZone.Rows.Add(gridRow);
            }
        }



        private void LoadSedimentation()
        {
            foreach (SedimentProject.ZoneRow row in Project.GetZoneRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSedimentation);

                gridRow.Cells[ColumnSedFrom.Index].Value = row.From;
                gridRow.Cells[ColumnSedTo.Index].Value = row.To;
            }
        }

        private void UpdateSedimentation()
        {
            spreadSheetSedimentation.ClearInsertedColumns();

            foreach (SedimentProject.CompositionRow row in Project.GetCompositionRows())
            {
                MineralFraction frac = row.Fraction;

                DataGridViewColumn gridColumn = spreadSheetSedimentation.InsertColumn(
                    frac.GrainSize.ToString(),
                    spreadSheetSedimentation.ColumnCount - 7
                    );

                gridColumn.DefaultCellStyle.Format = ColumnGrainWeight.DefaultCellStyle.Format;
                gridColumn.HeaderText = frac.ToString();
                gridColumn.Width = 75;
                gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                gridColumn.Visible = checkBoxShowSeparates.Checked;
            }

            spreadSheetSedimentation.Rows.Clear();

            foreach (SedimentProject.ZoneRow stretch in Project.GetZoneRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSedimentation);
                gridRow.Cells[ColumnSedFrom.Index].Value = stretch.From;
                spreadSheetSedimentation.Rows.Add(gridRow);
                gridRow.Cells[ColumnSedTo.Index].Value = stretch.To;
            }

            //foreach (SedimentationStretch stretch in GgiSedimentationModel.Stretches)
            //{
            //    DataGridViewRow gridRow = new DataGridViewRow();
            //    gridRow.CreateCells(spreadSheetSedimentation);
            //    gridRow.HeaderCell.Value = stretch.Name;
            //    gridRow.Cells[ColumnSedLength.Index].Value = stretch.Longitude;
            //    gridRow.Cells[ColumnSedSquare.Index].Value = stretch.Square;
            //    spreadSheetSedimentation.Rows.Add(gridRow);

            //    foreach (Fraction frac in stretch.Sediments)
            //    {
            //        if (frac.Value > 0) gridRow.Cells[frac.GrainSize.ToString()].Value = frac.Value;
            //        else gridRow.Cells[frac.GrainSize.ToString()].Value = null;
            //    }

            //    if (stretch.Weight > 0) gridRow.Cells[ColumnSedWeight.Index].Value = stretch.Weight;
            //    if (stretch.WeightCumulate > 0) gridRow.Cells[ColumnSedWeightIntegral.Index].Value = stretch.WeightCumulate;
            //    if (stretch.WeightLeft > 0) gridRow.Cells[ColumnSedWeightTransit.Index].Value = stretch.WeightLeft;
            //    if (stretch.Load > 0) gridRow.Cells[ColumnSedLoad.Index].Value = stretch.Load;
            //    if (stretch.Density > 0) gridRow.Cells[ColumnSedDensity.Index].Value = stretch.Density;
            //    if (stretch.Volume > 0) gridRow.Cells[ColumnSedVolume.Index].Value = stretch.Volume;
            //    if (stretch.Width > 0) gridRow.Cells[ColumnSedWidth.Index].Value = stretch.Width;
            //}
        }

        private void SaveSedimentation()
        {
            Data.Zone.Rows.Clear();

            //GgiSedimentationModel = new SedimentationGgiModel(Project.Depth, Project.Width);

            foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SedimentProject.ZoneRow zoneRow = Data.Zone.NewZoneRow();
                zoneRow.ProjectRow = Project;

                if (gridRow.Cells[ColumnSedFrom.Index].Value != null)
                    zoneRow.From = (double)gridRow.Cells[ColumnSedFrom.Index].Value;

                if (gridRow.Cells[ColumnSedTo.Index].Value != null)
                    zoneRow.To = (double)gridRow.Cells[ColumnSedTo.Index].Value;

                Data.Zone.AddZoneRow(zoneRow);

                //if (gridRow.Tag != null)
                //    GgiSedimentationModel.Stretches.Add((SedimentationStretch)gridRow.Tag);
            }

            Model = new ModelGgi(Project);
        }

        private void SetSedimentationLines(double interval)
        {
            buttonSetZones.Enabled = false;
            pageSediments.SetNavigation(false);
            //int iterations = (int)(maxDistance / interval) + 1;

            calcGgiModel.RunWorkerAsync(interval);
        }



        private void LoadLoad()
        {
            spreadSheetLoad.Rows.Clear();

            foreach (SedimentProject.CriticalLoadRow loadRow in Project.GetCriticalLoadRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetLoad);
                if (!loadRow.IsFromNull()) gridRow.Cells[ColumnLoadFrom.Index].Value = loadRow.From;
                spreadSheetLoad.Rows.Add(gridRow);
                if (!loadRow.IsToNull()) gridRow.Cells[ColumnLoadTo.Index].Value = loadRow.To;
            }
        }

        private void SaveLoad()
        {
            Data.CriticalLoad.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetLoad.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SedimentProject.CriticalLoadRow loadRow = Data.CriticalLoad.NewCriticalLoadRow();
                loadRow.ProjectRow = Project;

                if (gridRow.Cells[ColumnLoadFrom.Index].Value != null)
                    loadRow.From = (double)gridRow.Cells[ColumnLoadFrom.Index].Value;

                if (gridRow.Cells[ColumnLoadTo.Index].Value != null)
                    loadRow.To = (double)gridRow.Cells[ColumnLoadTo.Index].Value;

                Data.CriticalLoad.AddCriticalLoadRow(loadRow);
            }
        }

        private void SetLoadLine(double from, double to)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLoad);

            gridRow.Cells[ColumnLoadFrom.Index].Value = from;
            gridRow.Cells[ColumnLoadTo.Index].Value = to;

            //ModelStretch stretch =
            //    (Model != null ? Model.GetByLoad(from, to) :
            //     Model != null ? Model.GetByLoad(from, to) :
            //     new ModelStretch(0, 10000, Project));
            ModelStretch stretch = ((ModelGgi)Model).GetByLoad(from, to);

            gridRow.Cells[ColumnLoadLongitude.Index].Value = stretch.Longitude;
            gridRow.Cells[ColumnLoadSquare.Index].Value = stretch.Square;
            gridRow.Cells[ColumnLoadVolume.Index].Value = stretch.WaterVolume;
            gridRow.Cells[ColumnLoadWaterVolume.Index].Value = stretch.TransitWaterVolume;

            spreadSheetLoad.Rows.Add(gridRow);
        }



        private void LoadSilt()
        {
            spreadSheetSilt.Rows.Clear();

            foreach (SedimentProject.CriticalSiltRow siltRow in Project.GetCriticalSiltRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSilt);
                if (!siltRow.IsFromNull()) gridRow.Cells[ColumnSiltFrom.Index].Value = siltRow.From;
                spreadSheetSilt.Rows.Add(gridRow);
                if (!siltRow.IsToNull()) gridRow.Cells[ColumnSiltTo.Index].Value = siltRow.To;
            }
        }

        private void SaveSilt()
        {
            Data.CriticalSilt.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetSilt.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SedimentProject.CriticalSiltRow siltRow = Data.CriticalSilt.NewCriticalSiltRow();
                siltRow.ProjectRow = Project;

                if (gridRow.Cells[ColumnSiltFrom.Index].Value != null)
                    siltRow.From = (double)gridRow.Cells[ColumnSiltFrom.Index].Value;

                if (gridRow.Cells[ColumnSiltTo.Index].Value != null)
                    siltRow.To = (double)gridRow.Cells[ColumnSiltTo.Index].Value;

                Data.CriticalSilt.AddCriticalSiltRow(siltRow);
            }
        }

        private void SetSiltLine(double from, double to)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetSilt);

            gridRow.Cells[ColumnSiltFrom.Index].Value = from;
            gridRow.Cells[ColumnSiltTo.Index].Value = to;

            ModelStretch stretch = ((ModelGgi)Model).GetBySilt(from, to);

            gridRow.Cells[ColumnSiltLongitude.Index].Value = stretch.Longitude;
            gridRow.Cells[ColumnSiltSquare.Index].Value = stretch.Square;

            spreadSheetSilt.Rows.Add(gridRow);
        }



        //private void UpdateLoadChart()
        //{
        //    chartLoad.Series["Load"].Points.Clear();
        //    chartLoad.Series["Load"].Points.AddXY(0, Model.GetMaxLoad);

        //    if (Model != null)
        //    {
        //        foreach (ModelStretch stretch in Model.Stretches)
        //        {
        //            chartLoad.Series["Load"].Points.AddXY(stretch.End, stretch.FinalFullLoad);
        //        }

        //        Series series2 = chartLoad.Series["Critical"];
        //        series2.Points.Clear();

        //        chartLoad.ChartAreas[0].AxisX.CustomLabels.Clear();

        //        foreach (SedimentProject.CriticalLoadRow loadRow in Project.GetCriticalLoadRows())
        //        {
        //            double start = Model.GetDistance(loadRow.From);
        //            double end = Model.GetDistance(loadRow.To);

        //            if (start + end > 0 && !double.IsPositiveInfinity(start + end))
        //            {
        //                chartLoad.ChartAreas[0].AxisX.CustomLabels.Add(end, start,
        //                    string.Format("{0:N0}—{1:N0} г/м³", loadRow.From, loadRow.To), 1, LabelMarkStyle.LineSideMark);

        //                DataPoint cross1 = new DataPoint(start, loadRow.From);
        //                //cross1.Label = string.Format("{0:N2} г/м³\r\n{1:N1} м", loadRow.From, start);
        //                series2.Points.Add(cross1);

        //                DataPoint cross2 = new DataPoint(end, loadRow.To);
        //                //cross2.Label = string.Format("{0:N2} г/м³\r\n{1:N1} м", loadRow.To, end);
        //                series2.Points.Add(cross2);
        //            }
        //        }
        //    }
        //    else if (Model != null)
        //    {
        //        foreach (ModelStretch stretch in Model.Stretches)
        //        {
        //            chartLoad.Series["Load"].Points.AddXY(stretch.End, stretch.FinalFullLoad);
        //        }

        //        Series series2 = chartLoad.Series["Critical"];
        //        series2.Points.Clear();

        //        chartLoad.ChartAreas[0].AxisX.CustomLabels.Clear();

        //        foreach (SedimentProject.CriticalLoadRow loadRow in Project.GetCriticalLoadRows())
        //        {
        //            double start = Model.GetDistance(loadRow.From);
        //            double end = Model.GetDistance(loadRow.To);

        //            if (start + end > 0 && !double.IsPositiveInfinity(start + end))
        //            {
        //                chartLoad.ChartAreas[0].AxisX.CustomLabels.Add(end, start,
        //                    string.Format("{0:N0}—{1:N0} г/м³", loadRow.From, loadRow.To), 1, LabelMarkStyle.LineSideMark);

        //                DataPoint cross1 = new DataPoint(start, loadRow.From);
        //                //cross1.Label = string.Format("{0:N2} г/м³\r\n{1:N1} м", loadRow.From, start);
        //                series2.Points.Add(cross1);

        //                DataPoint cross2 = new DataPoint(end, loadRow.To);
        //                //cross2.Label = string.Format("{0:N2} г/м³\r\n{1:N1} м", loadRow.To, end);
        //                series2.Points.Add(cross2);
        //            }
        //        }
        //    }

        //    //Service.RefreshAxes(chartLoad);
        //    Service.RefreshAxes(chartLoad, selectedDistanceTo - selectedDistanceFrom);
        //}

        //private void UpdateSiltChart()
        //{
        //    Series series1 = chartSilt.Series["Silt"];

        //    series1.Points.Clear();

        //    double siltMax = 0;

        //    foreach (ModelStretch stretch in Model.Stretches)
        //    {
        //        series1.Points.AddXY(stretch.Start, stretch.SedimentsMeanWidth);
        //        //series1.Points.AddXY(stretch.Midpoint, stretch.SedimentsMeanWidth);
        //        series1.Points.AddXY(stretch.End, stretch.SedimentsMeanWidth);
        //        siltMax = Math.Max(siltMax, stretch.SedimentsMeanWidth);
        //    }

        //    double siltRest = Math.IEEERemainder(siltMax, Mayfly.Service.GetAutoInterval(siltMax));
        //    chartSilt.ChartAreas[0].AxisY.Maximum = (siltRest > 0) ?
        //        (siltMax - siltRest) + Mayfly.Service.GetAutoInterval(siltMax - siltRest) : siltMax - siltRest;

        //    Series series2 = chartSilt.Series["Critical"];
        //    series2.Points.Clear();

        //    chartSilt.ChartAreas[0].AxisX.CustomLabels.Clear();

        //    foreach (SedimentProject.CriticalSiltRow siltRow in Project.GetCriticalSiltRows())
        //    {
        //        double start = ((ModelGgi)Model).GetDistanceOfSilt(siltRow.From);
        //        double end = ((ModelGgi)Model).GetDistanceOfSilt(siltRow.To);

        //        if (start + end > 0)
        //        {
        //            chartSilt.ChartAreas[0].AxisX.CustomLabels.Add(end, start,
        //                string.Format("{0:N0}—{1:N0} мм", siltRow.From, siltRow.To), 1, LabelMarkStyle.LineSideMark);

        //            DataPoint cross1 = new DataPoint(start, siltRow.From);
        //            //cross1.Label = string.Format("{0:N2} мм\r\n{1:N1} м", siltRow.From, start);
        //            series2.Points.Add(cross1);

        //            DataPoint cross2 = new DataPoint(end, siltRow.To);
        //            //cross2.Label = string.Format("{0:N2} мм\r\n{1:N1} м", siltRow.To, end);
        //            series2.Points.Add(cross2);
        //        }
        //    }

        //    Service.RefreshAxes(chartSilt, selectedDistanceTo - selectedDistanceFrom);
        //}



        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                LoadData(UserSettings.Interface.OpenDialog.FileName);
            }
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }



        private void pageTitle_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveTitle();
        }



        private void workValue_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                SaveWorks();
                UpdateWorks();

                wizardPageWorkZone.AllowNext = Project.RightGap >= 0;
            }
        }



        private void naturalValue_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                SaveNaturals();
                UpdateNaturals();
            }
        }



        private void entrainmentValue_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                nomogramUsedForZ = false;

                SaveEntrainment();
                UpdateEntrainment();
            }
        }



        private void buttonGetZ_Click(object sender, EventArgs e)
        {
            GetZ getZ = new GetZ(Project);

            if (getZ.ShowDialog() == DialogResult.OK)
            {
                Project.TurbulentWidth = getZ.TurbulentWidth;
                Project.Fi = getZ.Fi;

                textBoxZ.Text = getZ.Z.ToString("N1");

                nomogramUsedForZ = true;

                SaveEntrainment();
                UpdateEntrainment();
            }

            //textBoxZ.Text = (3.2).ToString();
        }

        private void CheckBoxNgavt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNgavt.Checked)
            {
                pageNgavtLoad.NextPage = pageLoad;
                pageLoadChart.NextPage = pageReport;
            }
            else
            {
                pageEntrainment.NextPage = pageComposition;
            }
        }

        private void PageEntrainment_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {        }




        private void pageNgavtParameters_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            SaveControlParticles();
            UpdateControlParticles();
        }

        private void pageNgavtParameters_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //chartLoad.ChartAreas[0].AxisY.Maximum = Model.GetMaxLoad;
        }

        private void controlParticles_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                SaveControlParticles();
                UpdateControlParticles();
            }
        }



        private void spreadSheetSections_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (spreadSheetSections[ColumnSectionDistance.Index, e.RowIndex].Value == null) return;

            double l1 = e.RowIndex == 0 ? 0 : (double)spreadSheetSections[ColumnSectionDistance.Index, e.RowIndex - 1].Value;
            double l2 = (double)spreadSheetSections[ColumnSectionDistance.Index, e.RowIndex].Value;

            double v = spreadSheetSections[ColumnSectionVelocity.Index, e.RowIndex].Value == null ?
                Project.IsVelocityNull() ? 0 : Project.Velocity :
                (double)spreadSheetSections[ColumnSectionVelocity.Index, e.RowIndex].Value;

            double h = spreadSheetSections[ColumnSectionDepth.Index, e.RowIndex].Value == null ?
                Project.IsDepthNull() ? 0 : Project.Depth :
                (double)spreadSheetSections[ColumnSectionDepth.Index, e.RowIndex].Value;

            double ratio = ModelNgavt.GetDilution(v, h, Project.ControlHydraulicSize, l2 - l1);

            spreadSheetSections[ColumnSectionRatio.Index, e.RowIndex].Value = -(1 - ratio);

            double p = e.RowIndex == 0 ? Project.ControlLoad :
                (double)spreadSheetSections[ColumnSectionLoad.Index, e.RowIndex - 1].Value;

            spreadSheetSections[ColumnSectionLoad.Index, e.RowIndex].Value = ratio * p;
        }

        private void buttonSetSections_Click(object sender, EventArgs e)
        {
            contextSections.Show(buttonSetSections, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void contextSec1_Click(object sender, EventArgs e)
        {
            SetSectionLines(1);
        }

        private void contextSec10_Click(object sender, EventArgs e)
        {
            SetSectionLines(10);
        }

        private void contextSec50_Click(object sender, EventArgs e)
        {
            SetSectionLines(50);
        }

        private void contextSec100_Click(object sender, EventArgs e)
        {
            SetSectionLines(100);
        }

        private void contextSec500_Click(object sender, EventArgs e)
        {
            SetSectionLines(500);
        }

        private void contextSec1000_Click(object sender, EventArgs e)
        {
            SetSectionLines(1000);
        }

        private void calcNgavtModel_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            double interval = (double)e.Argument;
            double generalLength = interval;

            double load = Project.ControlLoad;

            while (load <= Project.ControlLoad && load > .01)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSections);

                gridRow.Cells[ColumnSectionDistance.Index].Value = generalLength;
                double ratio = ModelNgavt.GetDilution(
                    Project.Velocity,
                    Project.Depth,
                    Project.ControlHydraulicSize, interval);
                gridRow.Cells[ColumnSectionRatio.Index].Value = -(1 - ratio);
                gridRow.Cells[ColumnSectionLoad.Index].Value = ratio * load;

                load *= ratio;

                result.Add(gridRow);

                generalLength += interval;
            }

            e.Result = result.ToArray();
        }

        private void calcNgavtModel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                spreadSheetSections.Rows.Clear();
                spreadSheetSections.Rows.AddRange((DataGridViewRow[])e.Result);
            }

            pageNgavtLoad.SetNavigation(true);
            buttonSetSections.Enabled = true;
        }

        private void pageNgavtLoad_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveSections();
            //UpdateLoadChart();
        }



        private void buttonSetDefaultGrains_Click(object sender, EventArgs e)
        {
            spreadSheetGrain.Rows.Clear();

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
        }

        private void spreadSheetGrain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex != ColumnGrainSeparate.Index) return;

            double separate = 0;

            if (spreadSheetGrain[ColumnGrainSeparate.Index, e.RowIndex].Value != null)
            {
                separate = .01 * (double)spreadSheetGrain[ColumnGrainSeparate.Index, e.RowIndex].Value;
            }

            if (separate > 0) spreadSheetGrain[ColumnGrainWeight.Index, e.RowIndex].Value = Project.WeightFlushed * separate;
            else spreadSheetGrain[ColumnGrainWeight.Index, e.RowIndex].Value = null;

            textBoxTotalSeparate.Text = (ColumnGrainSeparate.GetDoubles().Sum())
                .ToString(ColumnGrainSeparate.DefaultCellStyle.Format);
        }

        private void pageComposition_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveComposition();

            //maxDistance = Project.GetMaximalDistance();
            //chartLoad.ChartAreas[0].AxisX.Maximum = maxDistance;
            //chartSilt.ChartAreas[0].AxisX.Maximum = maxDistance;

            //chartLoad.ChartAreas[0].AxisY.Maximum = Project.Load;

            UpdateZones();
            UpdateSedimentation();
        }



        private void pageZone_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
        }


        private void spreadSheetSedimentation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex != ColumnSedFrom.Index && e.ColumnIndex != ColumnSedTo.Index) return;

            DataGridViewRow gridRow = spreadSheetSedimentation.Rows[e.RowIndex];

            if (gridRow.Cells[ColumnSedTo.Index].Value == null) return;

            if (spreadSheetSedimentation.Focused &&
                e.ColumnIndex == ColumnSedTo.Index &&
                spreadSheetSedimentation.Rows[e.RowIndex + 1].IsNewRow)
            {
                spreadSheetSedimentation[ColumnSedFrom.Index, e.RowIndex + 1].Value =
                    (double)gridRow.Cells[ColumnSedTo.Index].Value;
            }

            if (gridRow.Cells[ColumnSedFrom.Index].Value == null) return;

            ModelStretch stretch = new ModelStretch(
                (double)gridRow.Cells[ColumnSedFrom.Index].Value,
                (double)gridRow.Cells[ColumnSedTo.Index].Value,
                Project);

            stretch.ProcessSedimentation();

            gridRow.Tag = stretch;

            BackgroundWorker stretchProcessor = new BackgroundWorker();
            stretchProcessor.DoWork += stretchProcessor_DoWork;
            stretchProcessor.RunWorkerCompleted += stretchProcessor_RunWorkerCompleted;

            stretchProcessor.RunWorkerAsync(stretch);
        }

        private void stretchProcessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ModelStretch stretch = (ModelStretch)e.Result;

            foreach (DataGridViewRow gridRow in spreadSheetSedimentation.Rows)
            {
                if (gridRow.Tag != stretch) continue;

                gridRow.Cells[ColumnSedLength.Index].Value = stretch.Longitude;
                gridRow.Cells[ColumnSedSquare.Index].Value = stretch.Square;

                foreach (MineralFraction frac in stretch.Sediments)
                {
                    if (frac.Value > 0) gridRow.Cells[frac.GrainSize.ToString()].Value = frac.Value;
                    else gridRow.Cells[frac.GrainSize.ToString()].Value = null;
                }

                if (stretch.Weight > 0) gridRow.Cells[ColumnSedWeight.Index].Value = stretch.Weight;
                else gridRow.Cells[ColumnSedWeight.Index].Value = null;

                //if (stretch.WeightCumulate > 0) gridRow.Cells[ColumnSedWeightIntegral.Index].Value = stretch.WeightCumulate;
                //else gridRow.Cells[ColumnSedWeightIntegral.Index].Value = null;

                if (stretch.TransitWeight > 0) gridRow.Cells[ColumnSedWeightTransit.Index].Value = stretch.TransitWeight;
                else gridRow.Cells[ColumnSedWeightTransit.Index].Value = null;

                if (stretch.FinalAdditionalLoad > 0) gridRow.Cells[ColumnSedLoad.Index].Value = stretch.FinalAdditionalLoad;
                else gridRow.Cells[ColumnSedLoad.Index].Value = null;

                if (stretch.SedimentsDensity > 0) gridRow.Cells[ColumnSedDensity.Index].Value = stretch.SedimentsDensity;
                else gridRow.Cells[ColumnSedDensity.Index].Value = null;

                if (stretch.SedimentsVolume > 0) gridRow.Cells[ColumnSedVolume.Index].Value = stretch.SedimentsVolume;
                else gridRow.Cells[ColumnSedVolume.Index].Value = null;

                if (stretch.SedimentsMeanWidth > 0) gridRow.Cells[ColumnSedWidth.Index].Value = stretch.SedimentsMeanWidth;
                else gridRow.Cells[ColumnSedWidth.Index].Value = null;

                break;
            }
        }

        private void stretchProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            ModelStretch stretch = (ModelStretch)e.Argument;
            stretch.ProcessSedimentation();
            e.Result = stretch;
        }

        private void buttonSetZones_Click(object sender, EventArgs e)
        {
            contextZones.Show(buttonSetZones, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void contextDefaultZones_Click(object sender, EventArgs e)
        {
            double generalLength = 0;

            spreadSheetSedimentation.Rows.Clear();

            foreach (SedimentProject.CompositionRow row in Project.GetCompositionRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSedimentation);
                gridRow.Cells[ColumnSedFrom.Index].Value = generalLength;
                spreadSheetSedimentation.Rows.Add(gridRow);
                gridRow.Cells[ColumnSedTo.Index].Value = row.SedimentationLongitude;

                generalLength = row.SedimentationLongitude;
            }
        }

        private void ДмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSedimentationLines(.1);
        }

        private void context1_Click(object sender, EventArgs e)
        {
            SetSedimentationLines(1);
        }

        private void context50_Click(object sender, EventArgs e)
        {
            SetSedimentationLines(50);
        }

        private void context100_Click(object sender, EventArgs e)
        {
            SetSedimentationLines(100);
        }

        private void context500_Click(object sender, EventArgs e)
        {
            SetSedimentationLines(500);
        }

        private void context1000_Click(object sender, EventArgs e)
        {
            SetSedimentationLines(1000);
        }

        private void calcGgiModel_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            double interval = (double)e.Argument;
            double generalLength = 0;
            int it = 0;

            while (true)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSedimentation);

                gridRow.Cells[ColumnSedFrom.Index].Value = generalLength;
                gridRow.Cells[ColumnSedTo.Index].Value = generalLength + interval;

                ModelStretch stretch = new ModelStretch(
                    generalLength, generalLength + interval, Project);

                stretch.ProcessSedimentation();

                gridRow.Tag = stretch;

                gridRow.Cells[ColumnSedLength.Index].Value = stretch.Longitude;
                gridRow.Cells[ColumnSedSquare.Index].Value = stretch.Square;

                foreach (MineralFraction frac in stretch.Sediments)
                {
                    DataGridViewColumn gridColumn = spreadSheetSedimentation.GetColumn(frac.GrainSize.ToString());
                    if (frac.Value > 0) gridRow.Cells[gridColumn.Index].Value = frac.Value;
                }

                if (stretch.Weight > 0) gridRow.Cells[ColumnSedWeight.Index].Value = stretch.Weight;
                if (stretch.TransitWeight > 0) gridRow.Cells[ColumnSedWeightTransit.Index].Value = stretch.TransitWeight;
                if (stretch.FinalAdditionalLoad > 0) gridRow.Cells[ColumnSedLoad.Index].Value = stretch.FinalAdditionalLoad;
                if (stretch.SedimentsDensity > 0) gridRow.Cells[ColumnSedDensity.Index].Value = stretch.SedimentsDensity;
                if (stretch.SedimentsVolume > 0) gridRow.Cells[ColumnSedVolume.Index].Value = stretch.SedimentsVolume;
                if (stretch.SedimentsMeanWidth > 0) gridRow.Cells[ColumnSedWidth.Index].Value = stretch.SedimentsMeanWidth;

                result.Add(gridRow);

                generalLength += interval;

                it++;
                calcGgiModel.ReportProgress(it);

                if (UserSettings.CriticalSediment > stretch.SedimentsMeanWidth && UserSettings.CriticalLoad > stretch.FinalAdditionalLoad) break;
            }

            e.Result = result.ToArray();
        }

        private void calcGgiModel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //int i = e.ProgressPercentage;
        }

        private void calcGgiModel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                spreadSheetSedimentation.Rows.Clear();
                spreadSheetSedimentation.Rows.AddRange((DataGridViewRow[])e.Result);
            }

            pageSediments.SetNavigation(true);
            buttonSetZones.Enabled = true;
        }

        private void checkBoxShowSeparates_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn gridColumn in spreadSheetSedimentation.GetInsertedColumns())
            {
                gridColumn.Visible = checkBoxShowSeparates.Checked;
            }
        }

        private void pageSediments_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveSedimentation();
        }




        private void buttonSetLoad_Click(object sender, EventArgs e)
        {
            spreadSheetLoad.Rows.Clear();

            if (Model.GetMaxLoad >= UserSettings.CriticalLoad) SetLoadLine(UserSettings.CriticalLoad, Model.GetMaxLoad);
            if (Model.GetMaxLoad > (.2 * UserSettings.CriticalLoad)) SetLoadLine(.2 * UserSettings.CriticalLoad, UserSettings.CriticalLoad);
            if (Model.GetMaxLoad > 0 && Model.GetMaxLoad <= (.2 * UserSettings.CriticalLoad)) SetLoadLine(0D, .2 * UserSettings.CriticalLoad);
        }

        private void spreadSheetLoad_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex != ColumnLoadFrom.Index && e.ColumnIndex != ColumnLoadTo.Index) return;

            DataGridViewRow gridRow = spreadSheetLoad.Rows[e.RowIndex];

            if (gridRow.Cells[ColumnLoadFrom.Index].Value == null) return;
            if (gridRow.Cells[ColumnLoadTo.Index].Value == null) return;

            double loadFrom = (double)gridRow.Cells[ColumnLoadFrom.Index].Value;
            double loadTo = (double)gridRow.Cells[ColumnLoadTo.Index].Value;

            if (Model != null)
            {
                ModelStretch stretch = Model.GetByLoad(loadFrom, loadTo);

                gridRow.Cells[ColumnLoadLongitude.Index].Value = stretch.Longitude;
                gridRow.Cells[ColumnLoadSquare.Index].Value = stretch.Square;
                gridRow.Cells[ColumnLoadVolume.Index].Value = stretch.WaterVolume;
                gridRow.Cells[ColumnLoadWaterVolume.Index].Value = stretch.TransitWaterVolume;
            }
            else if (Model != null)
            {
                ModelStretch stretch = Model.GetByLoad(loadFrom, loadTo);

                gridRow.Cells[ColumnLoadLongitude.Index].Value = stretch.Longitude;
                gridRow.Cells[ColumnLoadSquare.Index].Value = stretch.Square;
                gridRow.Cells[ColumnLoadVolume.Index].Value = stretch.WaterVolume;
                gridRow.Cells[ColumnLoadWaterVolume.Index].Value = stretch.TransitWaterVolume;
            }
            else
            {
                gridRow.Cells[ColumnLoadLongitude.Index].Value = null;
                gridRow.Cells[ColumnLoadSquare.Index].Value = null;
                gridRow.Cells[ColumnLoadVolume.Index].Value = null;
                gridRow.Cells[ColumnLoadWaterVolume.Index].Value = null;
            }
        }

        private void pageLoad_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveLoad();
            //UpdateLoadChart();
        }



        private void buttonSetSilt_Click(object sender, EventArgs e)
        {
            spreadSheetSilt.Rows.Clear();

            if (Model.GetMaxSedimentWidth >= UserSettings.CriticalSediment) SetSiltLine(UserSettings.CriticalSediment, Model.GetMaxSedimentWidth);
            if (Model.GetMaxSedimentWidth > (.2 * UserSettings.CriticalSediment)) SetSiltLine(.2 * UserSettings.CriticalSediment, UserSettings.CriticalSediment);
            if (Model.GetMaxSedimentWidth > 0 && Model.GetMaxSedimentWidth <= (.2 * UserSettings.CriticalSediment)) SetSiltLine(0D, .2 * UserSettings.CriticalSediment);
        }

        private void spreadSheetSilt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex != ColumnSiltFrom.Index && e.ColumnIndex != ColumnSiltTo.Index) return;

            DataGridViewRow gridRow = spreadSheetSilt.Rows[e.RowIndex];

            if (gridRow.Cells[ColumnSiltFrom.Index].Value == null) return;
            if (gridRow.Cells[ColumnSiltTo.Index].Value == null) return;

            double siltFrom = (double)gridRow.Cells[ColumnSiltFrom.Index].Value;
            double siltTo = (double)gridRow.Cells[ColumnSiltTo.Index].Value;

            if (Model == null)
            {
                gridRow.Cells[ColumnSiltSquare.Index].Value = null;
            }
            else
            {
                ModelStretch stretch = ((ModelGgi)Model).GetBySilt(siltFrom, siltTo);

                gridRow.Cells[ColumnSiltLongitude.Index].Value = stretch.Longitude;
                gridRow.Cells[ColumnSiltSquare.Index].Value = stretch.Square;
            }
        }

        private void pageSilt_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveSilt();
            //UpdateSiltChart();
        }



        private void chart_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            Service.RefreshAxes((Chart)sender, selectedDistanceTo - selectedDistanceFrom);
        }

        private void chart_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            selectedDistanceFrom = ((Chart)sender).ChartAreas[0].CursorX.SelectionStart;
            selectedDistanceTo = ((Chart)sender).ChartAreas[0].CursorX.SelectionEnd;
        }

        private void chart_SizeChanged(object sender, EventArgs e)
        {
            Service.RefreshAxes((Chart)sender, selectedDistanceTo - selectedDistanceFrom);
        }

        private void CheckBoxLateralFlow_CheckedChanged(object sender, EventArgs e)
        {
            numericLateralFlow.Enabled = checkBoxLateralFlow.Checked;
            workValue_Changed(sender, e);
        }

        private void PageChannel_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {

        }

        private void WizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {

        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Project.GetReport(Model,
                checkBoxMethods.Checked, checkBoxImpact.Checked, checkBoxCriticals.Checked).Run();
        }

        private void pageReport_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (FileName == null)
            {
                if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
                {
                    Save(UserSettings.Interface.SaveDialog.FileName);
                    Close();
                }
            }
            else
            {
                Save(FileName);
                Close();
            }

            e.Cancel = true;
        }

        private void wizardControl1_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}