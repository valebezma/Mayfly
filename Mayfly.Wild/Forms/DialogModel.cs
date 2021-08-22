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



        public ModelConfirm(BivariatePredictiveModel model)
        {
            InitializeComponent();

            Model = new Scatterplot(model);

            statChart1.AxisXTitle = Model.Calc.Data.X.Name;
            statChart1.AxisYTitle = Model.Calc.Data.Y.Name;

            statChart1.AxisXAutoMinimum = false;
            statChart1.AxisYAutoMinimum = false;
            statChart1.AxisXAutoInterval = true;
            statChart1.AxisYAutoInterval = true;

            Model.Properties.ShowTrend = true;
            Model.Properties.SelectedApproximationType = 
                model.Regression == null ? TrendType.Linear :
                model.Regression.Type;
            Model.Properties.ShowPredictionBands = true;

            statChart1.AddSeries(Model);
            statChart1.SetColorScheme();
            statChart1.AxisXMin = 0;
            statChart1.AxisYMin = 0;

            statChart1.Remaster();

            label1.ResetFormatted(statChart1.AxisXTitle, statChart1.AxisYTitle, model.Name);
            labelInstruction.ResetFormatted(model.Name, statChart1.AxisYTitle, statChart1.AxisXTitle);
            label2.ResetFormatted(model.Name, statChart1.AxisYTitle);

            Model.Updated += Scatterplot_Updated;
        }

        private void Scatterplot_Updated(object sender, ScatterplotEventArgs e)
        {
            buttonOK.Enabled = (Model.Properties.ShowTrend && Model.Calc.IsRegressionOK);
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
        { }
    }
}