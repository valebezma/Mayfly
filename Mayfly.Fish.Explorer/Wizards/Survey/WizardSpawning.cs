using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using AeroWizard;
using Mayfly.Wild;
using Mayfly.Waters;
using Mayfly.Mathematics;
using System.Data;
using Mayfly;
using Meta.Numerics.Statistics;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Fish;
using Mayfly.Mathematics.Statistics;
using Mayfly.Mathematics.Charts;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardSpawning : Form
    {
        public CardStack Data { get; set; }

        EnvironmentMonitor Monitor { get; set; }



        private WizardSpawning() 
        {
            InitializeComponent();

            Log.Write(EventType.WizardStarted, "Spawning wizard is started.");

            ColumnEnvWhen.ValueType = typeof(DateTime);

            ColumnEnvLevel.ValueType =
                ColumnEnvTempAir.ValueType =
                ColumnEnvTempSurface.ValueType =
                ColumnEnvWind.ValueType = typeof(double);

            ColumnEnvPrecips.ValueType = typeof(Weather);    
        }

        public WizardSpawning(CardStack data) : this()
        {
            Data = data;

            Monitor = Data.GetMonitor();

            foreach (EnvironmentState state in Monitor)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetEnv);

                if (!state.StateOfWater.IsTemperatureSurfaceNull())
                    gridRow.Cells[ColumnEnvTempSurface.Index].Value = state.StateOfWater.TemperatureSurface;

                spreadSheetEnv.Rows.Add(gridRow);
            }

            // TODO: Caution if cards are from different places!
        }



        public Report GetReport()
        {
            Report report = new Report("");

            report.UseTableNumeration = true;

            if (checkBoxEnvironment.Checked)
            {

            }

            if (checkBoxFecundityPattern.Checked)
            {

            }

            report.EndBranded();

            return report;
        }



        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();
            e.Cancel = true;
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetReport();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageReport.SetNavigation(true);            
            ((Report)e.Result).Run();
            Log.Write(EventType.WizardEnded, "Spawning wizard is finished");
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }

        private void pageTable_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            chart1.Series["Water"].Points.Clear();
            chart1.Series["Air"].Points.Clear();
            chart1.Series["Wind"].Points.Clear();
            chart1.Series["Clouds"].Points.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetEnv.Rows)
            {
                if (gridRow.IsNewRow) continue;

                //DateTime dt = (DateTime)spreadSheetEnv[ColumnEnvWhen.Index, gridRow.Index].Value;

                //if (spreadSheetEnv[ColumnEnvTempAir.Index, gridRow.Index].Value != null)
                //    chart1.Series["Air"].Points.AddXY(dt.ToDouble(), cardRow.WeatherConditions.Temperature);

                //if (spreadSheetEnv[ColumnEnvWind.Index, gridRow.Index].Value != null)
                //    chart1.Series["Wind"].Points.AddXY(dt.ToDouble(), cardRow.WeatherConditions.WindRate);

                //if (spreadSheetEnv[ColumnEnvClouds.Index, gridRow.Index].Value != null)
                //    chart1.Series["Clouds"].Points.AddXY(dt.ToDouble(), cardRow.WeatherConditions.Cloudage);

                //if (spreadSheetEnv[ColumnEnvTempSurface.Index, gridRow.Index].Value != null)
                //    chart1.Series["Water"].Points.AddXY(dt.ToDouble(), cardRow.StateOfWater.TemperatureSurface);
            }
        }
    }
}
