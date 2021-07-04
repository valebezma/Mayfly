using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Mayfly.Benthos
{
    public partial class MassRecovery : Form
    {
        #region Properties

        public Scatterplot ApprovedModel { get; set; }

        public Data Data { get; set; }

        public SpeciesKey Species { get; set; }

        public List<Data.SpeciesRow> SelectedSpecies
        {
            get
            {
                List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();
                foreach (ListViewItem item in listViewSpecies.SelectedItems)
                {
                    result.Add(Data.Species.FindBySpecies(item.Name));                    
                }
                return result;
            }
        }

        public string SelectedVariable
        {
            get
            {
                return listViewVars.SelectedItems[0].Name;
            }
        }

        public string OriginalSpecies { set; get; }

        public List<string> OriginalVars { set; get; }

        #endregion

        public MassRecovery()
        {
            InitializeComponent();

            listViewSpecies.Shine();
            listViewVars.Shine();

            OriginalVars = new List<string>();

            Data = new Data();
            Data.Card.AddCardRow(Data.Card.NewCardRow());
            Species = new SpeciesKey();

            statChart1.AxisYTitle = Resources.Interface.MassAxis;
        }

        #region Cards Loader

        List<string> CardsToLoad = new List<string>();

        private void CardsDragDrop(object sender, DragEventArgs e)
        {
            string[] droppedNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            string[] fileNames = FileSystem.MaskedNames(droppedNames, UserSettings.Extension);
            CardsToLoad.AddRange(fileNames);

            listViewSpecies.Enabled = listViewVars.Enabled =
                label1.Enabled = label2.Enabled =
                checkBoxCombine.Enabled = checkBoxOnlyFit.Enabled =  false;
            label4.Visible = false;
            progressBar1.Visible = true;
            progressBar1.Maximum = fileNames.Length;

            cardLoader.RunWorkerAsync();
        }

        private void CardsDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void cardLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            foreach (string fileName in CardsToLoad)
            {
                Data Card = new Data();
                Card.Read(fileName);
                Card.SingleCardRow.ImportLogTo(Data.SingleCardRow);
                i++;
                ((BackgroundWorker)sender).ReportProgress(i);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void cardLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewSpecies.Enabled = listViewVars.Enabled =
                label1.Enabled = label2.Enabled =
                checkBoxCombine.Enabled = checkBoxOnlyFit.Enabled = true;
            progressBar1.Visible = false;
            ShowSpecies();
            ShowVars();
        }

        private void ShowSpecies()
        {
            listViewSpecies.Items.Clear();

            foreach (Data.SpeciesRow speciesRow in Data.Species)
            {
                Add(speciesRow);
            }

            listViewSpecies.Sorting = SortOrder.Ascending;
            listViewSpecies.Sort();

            if (OriginalSpecies != null)
            {
                listViewSpecies.SelectedItems.Clear();
                ListViewItem originalCopy = listViewSpecies.FindItemWithText(OriginalSpecies);
                if (originalCopy != null)
                {
                    originalCopy.Selected = true;
                    listViewSpecies.EnsureVisible(originalCopy.Index);
                }
            }
        }

        public void Add(Data.SpeciesRow speciesRow)
        {
            if (listViewSpecies.FindItemWithText(speciesRow.Species) != null) return;

            ListViewItem item = new ListViewItem();
            item.Name = speciesRow.Species;
            item.Text = speciesRow.Species;
            listViewSpecies.Items.Add(item);
        }

        private void ShowVars()
        {
            listViewVars.Items.Clear();            

            foreach (Data.VariableRow variableRow in Data.Variable)
            {
                if (checkBoxOnlyFit.Checked)
                {
                    if (OriginalVars.Contains(variableRow.Variable))
                    {
                        Add(variableRow);
                    }
                }
                else
                {
                    Add(variableRow);
                }
            }

            listViewVars.SelectedItems.Clear();
            if (listViewVars.Items.Count > 0)
            {
                listViewVars.Items[0].Selected = true;
            }
        }

        public void Add(Data.VariableRow varRow)
        {
            if (listViewVars.FindItemWithText(varRow.Variable) != null) return;

            ListViewItem item = new ListViewItem();
            item.Name = varRow.Variable;
            item.Text = varRow.Variable;
            listViewVars.Items.Add(item);
        }

        #endregion

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            wizardPageData.AllowNext = 
                listViewSpecies.SelectedItems.Count > 0 &&
                listViewVars.SelectedItems.Count > 0;
        }

        private void checkBoxOnlyFit_CheckedChanged(object sender, EventArgs e)
        {
            ShowVars();
        }

        private void wizardPageData_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            statChart1.Clear();
            statChart1.AxisXTitle = SelectedVariable;
            Data.VariableRow SelectedVar = Data.Variable.FindByVarName(SelectedVariable);

            if (checkBoxCombine.Checked)
            {
                BivariateSample sample = new BivariateSample();

                foreach (Data.IndividualRow individualRow in Data.Individual)
                {
                    if (!SelectedSpecies.Contains(individualRow.LogRow.SpeciesRow)) continue;
                    if (individualRow.IsMassNull()) continue;
                    if (Data.Value.FindByIndIDVarID(individualRow.ID, SelectedVar.ID) == null) continue;
                    sample.Add(Data.Value.FindByIndIDVarID(individualRow.ID, SelectedVar.ID).Value, individualRow.Mass);
                }

                Scatterplot scatterplot = new Scatterplot(sample, "Combined series");
                scatterplot.Properties.ShowTrend = true;
                scatterplot.Properties.SelectedApproximationType = Mathematics.Statistics.Regression.RegressionType.Power;
                statChart1.AddSeries(scatterplot);
            }
            else foreach (Data.SpeciesRow speciesRow in SelectedSpecies)
            {
                BivariateSample sample = new BivariateSample();

                foreach (Data.IndividualRow individualRow in Data.Individual)
                {
                    if (individualRow.LogRow.SpeciesRow != speciesRow) continue;
                    if (individualRow.IsMassNull()) continue;
                    if (Data.Value.FindByIndIDVarID(individualRow.ID, SelectedVar.ID) == null) continue;

                    sample.Add(Data.Value.FindByIndIDVarID(individualRow.ID, SelectedVar.ID).Value, individualRow.Mass);
                }

                Scatterplot scatterplot = new Scatterplot(sample, speciesRow.Species);
                scatterplot.Properties.ShowTrend = true;
                scatterplot.Properties.SelectedApproximationType = Mathematics.Statistics.Regression.RegressionType.Power;
                statChart1.AddSeries(scatterplot);
            }

            statChart1.SetColorScheme();
            statChart1.RecalculateAxesProperties();
            statChart1.RefreshAxes();
            statChart1.Update(statChart1, new EventArgs());
        }

        private void wizardPageChart_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            ApprovedModel = statChart1.LastSelectedScatterplot;
        }
    }
}
