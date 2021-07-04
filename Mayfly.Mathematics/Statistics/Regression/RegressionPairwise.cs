using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Statistics
{
    public partial class RegressionPairwise : Form
    {
        List<Linear> Regressions;

        public RegressionPairwise()
        {
            InitializeComponent();

            Regressions = new List<Linear>();
            spreadSheetMatrix.Rows.Clear();
        }



        public void Add(Linear regression)
        {
            Regressions.Add(regression);

            DataGridViewColumn newColumn = spreadSheetMatrix.InsertColumn(regression.Name, typeof(double), "N3");

            DataGridViewRow newRow = spreadSheetMatrix.Rows[spreadSheetMatrix.Rows.Add()];
            newRow.HeaderCell.Value = regression.Name;
        }

        private void UpdateMatrix(int p)
        {
            for (int i = 0; i < Regressions.Count; i++)
            {
                for (int j = 0; j < Regressions.Count; j++)
                {
                    if (i == j)
                    {
                        spreadSheetMatrix[i, j].Value = Constants.Null;
                    }
                    else if (i > j)
                    {
                        spreadSheetMatrix[i, j].Value = null;
                    }
                    else
                    {
                        TestResult q = p == 0 ? 
                            Regressions[i].GetTukeySlopeStatistic(Regressions[j])
                            : Regressions[i].GetTukeyElevationStatistic(Regressions[j]);

                        spreadSheetMatrix[i, j].Value = q.Statistic;
                    }
                }
            }
        }



        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMatrix(comboBoxType.SelectedIndex);
        }
    }
}
