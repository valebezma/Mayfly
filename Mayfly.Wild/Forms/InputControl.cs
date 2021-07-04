using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Mayfly.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Mayfly.Wild
{
    public partial class InputControl : Form
    {
        SpreadSheet SpreadSheet;

        DataGridViewColumn SpeciesColumn;

        string Species;

        Scatterplot Scatterplot;

        public DataGridViewColumn Argument
        {
            set;
            get;
        }

        public DataGridViewColumn Function
        {
            get;
            set;
        }



        public InputControl(SpreadSheet spreadSheet,
            DataGridViewColumn speciesColumn,
            DataGridViewColumn[] xColumns,
            DataGridViewColumn[] yColumns)
        {
            InitializeComponent();

            Species = string.Empty;
            SpreadSheet = spreadSheet;
            SpreadSheet.Filtered += SelectedIndexChanged;

            SpeciesColumn = speciesColumn;

            comboBoxSpecies.DataSource = SpeciesColumn.GetStrings(true);
            comboBoxArgument.DataSource = xColumns;
            comboBoxFunction.DataSource = yColumns;
            comboBoxFunction.SelectedIndex = 1;
        }



        public void SetParameters(string species, DataGridViewColumn xColumn, DataGridViewColumn yColumn)
        {
            comboBoxArgument.SelectedItem = xColumn;
            comboBoxFunction.SelectedItem = yColumn;

            if (SpreadSheet.IsFiltering) return;
            //if (backgroundChartBuilder.IsBusy) return;

            comboBoxSpecies.SelectedIndex = comboBoxSpecies.FindStringExact(species);
        }



        private void backgroundChartBuilder_DoWork(object sender, DoWorkEventArgs e)
        {
            Scatterplot = new Scatterplot(Argument, Function, Species);

            if (Scatterplot.Data.Count < Mathematics.UserSettings.StrongSampleSize) return;

            Scatterplot.Properties.ShowTrend = true;
            Scatterplot.Properties.SelectedApproximationType = Mathematics.Statistics.TrendType.Power;
            Scatterplot.Properties.ShowPredictionBands = true;
            Scatterplot.Properties.HighlightRunouts = true;
        }

        private void backgroundChartBuilder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatChart.Clear();

            labelLack.Visible = Scatterplot == null;

            if (Scatterplot != null)
            {
                StatChart.AddSeries(Scatterplot);

                StatChart.AxisXTitle = Argument.HeaderText;
                StatChart.AxisYTitle = Function.HeaderText;
                StatChart.RecalculateAxesProperties();
            }

            StatChart.Remaster();

            Cursor = Cursors.Default;

            BringToFront();
            Focus();
        }



        private void comboBoxSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpreadSheet.OpenFilter(SpeciesColumn, comboBoxSpecies.SelectedValue as string, true);
            SpreadSheet.Filter.Apply();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSpecies.SelectedIndex == -1) return;
            if (SpreadSheet.IsFiltering) return;
            if (SpreadSheet.RowCount == SpreadSheet.VisibleRowCount) return;

            StatChart.Remove(Species);
            Species = comboBoxSpecies.SelectedValue as string;

            if (comboBoxArgument.SelectedIndex == -1) return;

            Argument = comboBoxArgument.SelectedItem as DataGridViewColumn;

            if (comboBoxFunction.SelectedIndex == -1) return;

            Function = comboBoxFunction.SelectedItem as DataGridViewColumn;

            if (backgroundChartBuilder.IsBusy) return;

            Cursor = Cursors.AppStarting;
            backgroundChartBuilder.RunWorkerAsync();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
