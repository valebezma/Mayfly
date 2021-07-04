using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly;
using Mayfly.Statistics;
using Mayfly.Species;
using Meta.Numerics.Statistics;

namespace Mayfly.Fish
{
    public partial class MassRecovery : Form
    {
        #region Properties

        public Regression ApprovedModel { get; set; }

        public Benthos.Data Data { get; set; }

        public _Species Species { get; set; }

        public Benthos.Data.SpeciesRow[] SelectedSpecies
        {
            get
            {
                List<Benthos.Data.SpeciesRow> result = new List<Benthos.Data.SpeciesRow>();
                foreach (object Item in listBoxSpecies.SelectedItems)
                {
                    if (Item is DataRowView)
                    {
                        result.Add((Benthos.Data.SpeciesRow)(((DataRowView)Item).Row));
                    }
                }
                return result.ToArray();
            }
        }

        public Benthos.Data.VariableRow SelectedVariable
        {
            get
            {
                return (Benthos.Data.VariableRow)((DataRowView)listBoxVars.SelectedItem).Row;
            }
        }

        public List<string> OriginalSpecies { set; get; }

        #endregion

        #region Constructors

        public MassRecovery()
        {
            InitializeComponent();

            Mayfly.Service.Shine(listBoxSpecies.Handle);
            Mayfly.Service.Shine(listBoxVars.Handle);

            Data = new Benthos.Data();
            Species = new _Species();

            OriginalSpecies = new List<string>();

            statChart1.Properties.TitleY = Benthos.Resources.Interface.MassAxis;
        }

        #endregion

        #region Cards Loader

        List<string> CardToLoad = new List<string>();

        private void CardsDragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            CardToLoad.AddRange(Mayfly.Service.MaskedNames(fileNames, Benthos.AppProperties.Extension));
            label4.Visible = false;
            LoadCards();
        }

        private void CardsDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        public void LoadCards()
        {
            if (CardToLoad.Count > 0)
            {
                BackgroundWorker CardLoader = new BackgroundWorker();
                CardLoader.DoWork += CardLoader_DoWork;
                CardLoader.RunWorkerCompleted += CardLoader_RunWorkerCompleted;
                CardLoader.RunWorkerAsync(CardToLoad[0]);
            }
        }

        private void CardLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            Attach((string)e.Argument);
        }

        private void CardLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBoxSpecies.DataSource = Data.Species;
            listBoxSpecies.DisplayMember = "Species";

            listBoxVars.DataSource = Data.Variable;
            listBoxVars.DisplayMember = "Variable";

            CardToLoad.RemoveAt(0);

            if (CardToLoad.Count == 0)
            {
                if (OriginalSpecies.Count > 0)
                {
                    listBoxSpecies.ClearSelected();
                    foreach (string _OriginalSpecies in OriginalSpecies)
                    {
                        listBoxSpecies.SelectedIndices.Add(
                            listBoxSpecies.FindString(_OriginalSpecies));
                    }
                }
            }
            else
            {
                BackgroundWorker CardLoader = new BackgroundWorker();
                CardLoader.DoWork += CardLoader_DoWork;
                CardLoader.RunWorkerCompleted += CardLoader_RunWorkerCompleted;
                CardLoader.RunWorkerAsync(CardToLoad[0]);
            }
        }

        private void Attach(string filename)
        {
            Benthos.Data Card = new Benthos.Data();
            Card.Read(filename);

            foreach (Benthos.Data.SpeciesRow CurrentSpecies in Card.Species.Rows)
            {
                if (!Species.Species.Check(CurrentSpecies))
                {
                    Species.Species.Import(CurrentSpecies);
                }
            }

            Data.Species.Fill(Species.Species);
            Card.MergeLogWith(Data);
        }

        #endregion

        private void listBoxSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            wizardPageData.AllowNext = listBoxSpecies.SelectedItems.Count > 0 && listBoxVars.SelectedItems.Count > 0;
        }

        private void wizardPageData_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            statChart1.ClearSeries();
            statChart1.Properties.TitleX = SelectedVariable.Variable;
            foreach (Benthos.Data.SpeciesRow _Species in SelectedSpecies)
            {
                BivariateSample Sample = new BivariateSample();

                foreach (Benthos.Data.IndividualRow _IndRow in Data.Individual)
                {
                    if (_IndRow.LogRow.SpeciesRow != _Species) continue;
                    if (_IndRow.IsMassNull()) continue;
                    if (Data.Value.FindByIndIDVarID(_IndRow.ID, SelectedVariable.ID) == null) continue;
                   
                    Sample.Add(Data.Value.FindByIndIDVarID(_IndRow.ID, SelectedVariable.ID).Value, _IndRow.Mass);
                }

                Regression _Regression = new Regression(Sample, _Species.Species);
                //_Regression.CalculateProxy(RelationKind.Power);
                _Regression.Properties.ShowTrend = true;
                statChart1.AddSeries(_Regression);
            }

            statChart1.SetColorScheme();

            foreach (Benthos.Data.SpeciesRow _Species in SelectedSpecies)
            {
                //statChart1.SetSeriesAppearance(statChart1.FindRegression(_Species.Species));
            }       
        }

        private void wizardPageChart_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //ApprovedModel = statChart1.FindRegression(statChart1.SelectedSeries.Name);
        }
    }
}
