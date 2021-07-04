using Mayfly.Mathematics;
using Mayfly.Mathematics.Charts;
using System;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Mathematics.Statistics;
using System.Resources;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class ModelConfirm : Form
    {
        public Scatterplot Model { set; get; }



        public ModelConfirm(Scatterplot scatterplot)
        {
            InitializeComponent();

            statChart1.AxisXTitle = scatterplot.Data.X.Name;
            statChart1.AxisYTitle = scatterplot.Data.Y.Name;

            statChart1.AxisXAutoMinimum = false;
            statChart1.AxisYAutoMinimum = false;
            statChart1.AxisXAutoInterval = true;
            statChart1.AxisYAutoInterval = true;

            Model = scatterplot.Copy();
            Model.Properties.ShowTrend = true;
            Model.Properties.SelectedApproximationType = 
                scatterplot.Regression == null ? TrendType.Linear :
                scatterplot.Regression.Type;
            Model.Properties.ShowPredictionBands = true;

            statChart1.AddSeries(Model);
            statChart1.SetColorScheme();
            statChart1.AxisXMin = 0;
            statChart1.AxisYMin = 0;

            statChart1.Remaster();

            label1.ResetFormatted(statChart1.AxisXTitle, statChart1.AxisYTitle, scatterplot.Name);
            labelInstruction.ResetFormatted(scatterplot.Name, statChart1.AxisYTitle, statChart1.AxisXTitle);
            label2.ResetFormatted(scatterplot.Name, statChart1.AxisYTitle);

            Model.Updated += Scatterplot_Updated;
        }

        private void Scatterplot_Updated(object sender, ScatterplotEventArgs e)
        {
            buttonOK.Enabled = (Model.Properties.ShowTrend && Model.IsRegressionOK);
        }

        private void ModelConfirm_Load(object sender, EventArgs e)
        {
            statChart1.Update(statChart1.Properties, e);
            this.BringToFront();  
        }

        private void buttonTrend_Click(object sender, EventArgs e)
        {
            statChart1.OpenTrendProperties(Model);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Model = Model.Copy();
        }
    }
}