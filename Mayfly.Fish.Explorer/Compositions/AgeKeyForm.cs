using Mayfly.Wild;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public partial class AgeKeyForm : Form
    {
        public AgeKey Key;

        private DataGridViewRow TreatedRow { get; set; }
        private DataGridViewRow UntreatedRow { get; set; }
        private DataGridViewRow TotalRow { get; set; }

        private DataGridViewRow MeanMassRow { get; set; }
        private DataGridViewRow TotalMassRow { get; set; }



        private AgeKeyForm() 
        {
            InitializeComponent();

            TreatedRow = new DataGridViewRow();
            TreatedRow.CreateCells(spreadSheet, Resources.Interface.Interface.AgeSampled);
            TreatedRow.Cells[columnSizeClass.Index].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;           
            spreadSheet.Rows.Add(TreatedRow);

            UntreatedRow = new DataGridViewRow();
            UntreatedRow.CreateCells(spreadSheet, Resources.Interface.Interface.AgeAdd);
            UntreatedRow.Cells[columnSizeClass.Index].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;            
            spreadSheet.Rows.Add(UntreatedRow);

            TotalRow = new DataGridViewRow();
            TotalRow.CreateCells(spreadSheet, Resources.Interface.Interface.AgeGeneral);
            TotalRow.Cells[columnSizeClass.Index].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;            
            spreadSheet.Rows.Add(TotalRow);

            MeanMassRow = new DataGridViewRow();
            MeanMassRow.CreateCells(spreadSheet, Resources.Interface.Interface.AvgMass);
            MeanMassRow.Cells[columnSizeClass.Index].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            MeanMassRow.DefaultCellStyle.Format = "S0";
            spreadSheet.Rows.Add(MeanMassRow);

            TotalMassRow = new DataGridViewRow();
            TotalMassRow.CreateCells(spreadSheet, Resources.Interface.Interface.TotalMass);
            TotalMassRow.Cells[columnSizeClass.Index].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TotalMassRow.DefaultCellStyle.Format = "N3";            
            spreadSheet.Rows.Add(TotalMassRow);
        }

        public AgeKeyForm(AgeKey key) : this()
        {
            this.Key = key;

            TreatedRow.Cells[columnAge.Index].Value = key.treatedTtl.TotalQuantity;
            UntreatedRow.Cells[columnLength.Index].Value = key.untreatedTtl.TotalQuantity;

            //int rc = spreadSheet.RowCount;

            // Adding rows for length classes
            //foreach (Category sizeClass in Key.treated)
            for (int i = 0; i < key.treatedTtl.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheet);
                gridRow.Cells[columnSizeClass.Index].Value = key.treatedTtl[i].Name;
                spreadSheet.Rows.Insert(i, gridRow);
            }

            // Inserting aged and non-aged quantities
            for (int i = 0; i < key.treatedTtl.Count; i++)
            {
                if (key.treatedTtl[i].Quantity > 0)
                {
                    spreadSheet[columnAge.Index, i].Value = key.treatedTtl[i].Quantity;
                }

                if (key.untreatedTtl[i].Quantity > 0)
                {
                    spreadSheet[columnLength.Index, i].Value = key.untreatedTtl[i].Quantity;
                }
            }

            // Inserting columns for age classes
            for (int j = 0; j < key.Count; j++)
            {
                DataGridViewColumn gridColumn = spreadSheet.InsertColumn(key[j].Name, j + 1);//, typeof(double));
                gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < key.treatedTtl.Count; i++)
                {
                    int t = key.treated[j, i].Quantity;
                    int o = key.obtained[j, i].Quantity;

                    if (t > 0 && o == 0)
                    {
                        spreadSheet[gridColumn.Index, i].Value = t;
                    }
                    else if (t == 0 && o > 0)
                    {
                        spreadSheet[gridColumn.Index, i].Style.ForeColor = Constants.InfantColor;
                        spreadSheet[gridColumn.Index, i].Value = o;
                    }
                    else if (t > 0 && o > 0)
                    {
                        spreadSheet[gridColumn.Index, i].Value = string.Format("{0} + {1}", t, o);
                    }
                }

                // Total rows
                if (key.measured[j].Quantity != 0)
                    TreatedRow.Cells[gridColumn.Index].Value = key.measured[j].Quantity;

                if (key[j].Quantity - key.measured[j].Quantity > 0)
                    UntreatedRow.Cells[gridColumn.Index].Value = key[j].Quantity - key.measured[j].Quantity;

                if (key[j].Quantity != 0)
                    TotalRow.Cells[gridColumn.Index].Value = key[j].Quantity;

                if (key[j].MassSample != null)
                    MeanMassRow.Cells[gridColumn.Index].Value = new SampleDisplay(key[j].MassSample);

                if (key[j].Mass != 0)
                    TotalMassRow.Cells[gridColumn.Index].Value = key[j].Mass;
            }

            //contextHideEmpty.Checked = true;

            plotT.Series.Clear();
            plotT.Text = string.Format(Resources.Reports.Sections.ALK.Title, key.Name);

            Series hist1 = new Series();
            hist1.ChartType = SeriesChartType.StackedColumn;
            hist1.Name = Resources.Interface.Interface.AgeSampled;

            Series hist2 = new Series();
            hist2.ChartType = SeriesChartType.StackedColumn;
            hist2.Name = Resources.Interface.Interface.AgeAdd;

            double minx = double.MaxValue;
            double maxx = 0;
            double maxy = 0;

            for (int j = 0; j < key.Count; j++)
            {
                Category ageT = key.measured[j];
                AgeGroup ageR = key[j];

                hist1.Points.AddXY(ageR.Age.Years + .5, ageT.Quantity);
                hist2.Points.AddXY(ageR.Age.Years + .5, ageR.Quantity - ageT.Quantity);

                minx = Math.Min(minx, ageR.Age.Years);
                maxx = Math.Max(maxx, ageR.Age.Years);
                maxy = Math.Max(maxy, ageR.Quantity);
            }

            plotT.Series.Add(hist1);
            plotT.Series.Add(hist2);

            plotT.AxisXMin = minx;
            plotT.AxisXMax = maxx;
            plotT.AxisYMin = 0.0;
            plotT.AxisYMax = maxy;

            plotT.Remaster();

            tabControl1.SelectedTab = pageChart;
        }



        private void ToolStripMenuItemHideEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (contextHideEmpty.Checked)
            {
                for (int i = 3; i < spreadSheet.ColumnCount; i++)
                {
                    if (spreadSheet[i, TotalRow.Index].Value == null)
                        spreadSheet.Columns[i].Visible = false;
                    else break; 
                }

                for (int i = spreadSheet.ColumnCount - 1; i > 2; i--)
                {
                    if (spreadSheet[i, TotalRow.Index].Value == null)
                        spreadSheet.Columns[i].Visible = false; 
                    else break;
                }

                for (int i = 5; i < spreadSheet.RowCount; i++)
                {
                    if (spreadSheet[columnAge.Index, i].Value == null &&
                        spreadSheet[columnLength.Index, i].Value == null)
                        spreadSheet.Rows[i].Visible = false;
                    else break;
                }

                for (int i = spreadSheet.RowCount - 1; i > 4; i--)
                {
                    if (spreadSheet[columnAge.Index, i].Value == null &&
                        spreadSheet[columnLength.Index, i].Value == null)
                        spreadSheet.Rows[i].Visible = false;
                    else break;
                }
            }
            else
            {
                foreach (DataGridViewRow gridRow in spreadSheet.Rows) 
                {
                    spreadSheet.SetVisible(gridRow);
                }

                foreach (DataGridViewColumn gridColumn in spreadSheet.Columns) 
                {
                    gridColumn.Visible = true;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
