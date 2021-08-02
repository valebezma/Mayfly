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
using Mayfly.Controls;

namespace Mayfly.Wild
{
    public partial class CompositionComparison : Form
    {
        public List<Composition> Compositions { get; set; }

        public Composition CompositionOne { get; set; }

        public Composition CompositionTwo { get; set; }



        private CompositionComparison()
        {
            InitializeComponent();
            listViewIndex.Shine();
            Compositions = new List<Composition>();
            tabPageMatrix.Parent = null;
        }

        public CompositionComparison(Composition frameList)
            : this()
        {
            foreach (DataGridViewColumn columnSpecies in new DataGridViewColumn[] { 
                ColumnSpeciesA, columnSpeciesB, columnSpeciesO, columnSpeciesD })
            {
                frameList.SetLines(columnSpecies);
                columnSpecies.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public CompositionComparison(Composition frameList, List<Composition> lists)
            : this(frameList)
        {
            Compositions.AddRange(lists);

            UpdateCompositionAppearance();
        }



        public void AddComposition(Composition composition)
        {
            Compositions.Add(composition);

            UpdateCompositionAppearance();
        }

        public void UpdateCompositionAppearance()
        {
            UpdateCompositionAppearance(ColumnSpeciesA, CompositionColumn.Abundance, "N1");
            UpdateCompositionAppearance(columnSpeciesB, CompositionColumn.Biomass, "N3");
            UpdateCompositionAppearance(columnSpeciesO, CompositionColumn.Occurrence, "P1");
            UpdateCompositionAppearance(columnSpeciesD, CompositionColumn.Dominance, "N3");
        }

        private void UpdateCompositionAppearance(DataGridViewColumn columnSpecies,
            CompositionColumn variant, string format)
        {
            SpreadSheet spreadSheet = (SpreadSheet)columnSpecies.DataGridView;

            spreadSheet.ClearInsertedColumns();

            foreach (Composition composition in Compositions)
            {
                spreadSheet.InsertColumn(composition.Name, typeof(double), format);
                composition.UpdateValues(columnSpecies, variant);

                //DataGridViewColumn gridColumn = spreadSheet.InsertColumn(composition.Name);
                //composition.UpdateValues(spreadSheet, variant);


                //foreach (Category cat in composition)
                //{
                //    foreach (DataGridViewRow gridRow in spreadSheet.Rows)
                //    {
                //        if (gridRow.Cells[columnSpecies.Index].Value.Equals(cat.Name))
                //        {
                //            object value = cat.GetValue(variant);

                //            if (Convert.ToDouble(value) == 0)
                //            {
                //            }
                //            else
                //            {
                //                spreadSheet[gridColumn.Index, gridRow.Index].Value = value;
                //            }
                //        }
                //        else
                //        {
                //            spreadSheet[gridColumn.Index, gridRow.Index].Value = null;
                //        }
                //    }
                //}
            }
        }



        private void menuItemSimilarity_Click(object sender, EventArgs e)
        {
            tabPageMatrix.Parent = tabControl1;
            tabControl1.SelectedTab = tabPageMatrix;

            spreadSheetMatrix.Rows.Clear();

            foreach (Composition composition in Compositions)
            {
                DataGridViewColumn newColumn = spreadSheetMatrix.InsertColumn(composition.Name, typeof(double), "N3");

                DataGridViewRow newRow = spreadSheetMatrix.Rows[spreadSheetMatrix.Rows.Add()];
                newRow.HeaderCell.Value = composition.Name;
            }

            labelCombinationsCount.UpdateStatus((int)(Compositions.Count * Compositions.Count - Compositions.Count * .5));

            listViewIndex_SelectedIndexChanged(listViewIndex, e);

        }

        private void listViewIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewIndex.SelectedIndices.Count == 0)
            {
                for (int i = 0; i < Compositions.Count; i++)
                {
                    for (int j = 0; j < Compositions.Count; j++)
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
                            spreadSheetMatrix[i, j].Style.BackColor = (j % 2 == 0) ? 
                                spreadSheetMatrix.DefaultCellStyle.BackColor : 
                                ((DataGridView)spreadSheetMatrix).AlternatingRowsDefaultCellStyle.BackColor;

                            spreadSheetMatrix[i, j].Value = double.NaN;
                        }
                    }
                }
            }
            else
            {
                listViewIndex.Enabled = false;
                spreadSheetMatrix.StartProcessing(
                    Compositions.Count * Compositions.Count - Compositions.Count / 2,
                    Resources.Interface.Process.SimCalc);

                backMatch.RunWorkerAsync(listViewIndex.SelectedIndices[0]);
            }
        }

        private void backMatch_DoWork(object sender, DoWorkEventArgs e)
        {
            double[,] result = new double[Compositions.Count, Compositions.Count];

            foreach (Composition column in Compositions)
            {
                int colNo = Compositions.IndexOf(column);

                foreach (Composition row in Compositions)
                {
                    int rowNo = Compositions.IndexOf(row);

                    if (colNo < rowNo)
                    {
                        CompositionMatch sim = new CompositionMatch(column, row);

                        switch ((int)e.Argument)
                        {
                            case 0:
                                result[colNo, rowNo] = sim.Czekanowski1900_Sorensen1948;
                                break;
                            case 1:
                                result[colNo, rowNo] = sim.Jaccard1901;
                                break;
                            case 2:
                                result[colNo, rowNo] = sim.Szymkiewicz1926_Simpson1943;
                                break;
                            case 3:
                                result[colNo, rowNo] = sim.Kulczynski1927_A;
                                break;
                            case 4:
                                result[colNo, rowNo] = sim.Kulczynski1927_B;
                                break;
                            case 5:
                                result[colNo, rowNo] = sim.BraunBlanquet1932;
                                break;
                            case 6:
                                result[colNo, rowNo] = sim.Ochiai1957_Barkman1958;
                                break;
                            case 7:
                                result[colNo, rowNo] = sim.SokalSneath1963;
                                break;


                            case 8:
                                result[colNo, rowNo] = sim.Czekanowcki1911;
                                break;
                            case 9:
                                result[colNo, rowNo] = sim.Shorygin1939;
                                break;
                            case 10:
                                result[colNo, rowNo] = sim.Weinstein1976;
                                break;



                            case 11:
                                result[colNo, rowNo] = sim.Pianka1973;
                                break;
                            case 12:
                                result[colNo, rowNo] = sim.Morisita1959;
                                break;
                            case 13:
                                result[colNo, rowNo] = sim.Horn1966_MorisitaSimplified;
                                break;
                            case 14:
                                result[colNo, rowNo] = sim.Horn1966;
                                break;
                            case 15:
                                result[colNo, rowNo] = sim.Hurlbert1978;
                                break;


                            default:
                                result[colNo, rowNo] = double.NaN;
                                break;
                        }

                        ((BackgroundWorker)sender).ReportProgress(colNo * Compositions.Count + rowNo);
                    }
                }
            }

            e.Result = result;
        }

        private void backMatch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double[,] result = (double[,])e.Result;

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
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
                        spreadSheetMatrix[i, j].Value = result[i, j];

                        Color defaultColor = (j % 2 == 0) ?
                                spreadSheetMatrix.DefaultCellStyle.BackColor :
                                ((DataGridView)spreadSheetMatrix).AlternatingRowsDefaultCellStyle.BackColor;

                        spreadSheetMatrix[i, j].Style.BackColor = 
                            double.IsNaN(result[i, j]) || double.IsInfinity(result[i, j]) || double.IsNegativeInfinity(result[i, j]) ?
                            defaultColor : Mayfly.Service.Map(result[i, j]);
                    }
                }
            }

            listViewIndex.Enabled = true;
            spreadSheetMatrix.StopProcessing();

        }

        private void backMatch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetProgress(e.ProgressPercentage);
        }
    }
}
