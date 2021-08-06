using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Mayfly.Fish;
using Mayfly.Extensions;
using System.Globalization;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardTac : Form
    {
        CardStack Data;

        public FishSamplerType SelectedSamplerType { get; set; }

        public UnitEffort SelectedUnit { get; set; }

        public Data.SpeciesRow SelectedSpecies { get; set; }



        private WizardTac()
        {
            InitializeComponent();

            ColumnYear.ValueType = typeof(int);
            ColumnCatch.ValueType = typeof(double);
            ColumnEffort.ValueType = typeof(double);
            ColumnCpue.ValueType = typeof(double);

            wizardPageEffort.Suppress = true;
        }

        public WizardTac(CardStack data) : this()
        {
            wizardPageEffort.Suppress = false;

            Data = data;

            comboBoxGear.DataSource = Data.GetSamplerTypeDisplays();

            textBoxFishery.Text = Data.GetWaterNames().Merge();

            comboBoxSpecies.DataSource = data.GetSpecies();

            textBoxSources.Enabled = false;
            userGetter.RunWorkerAsync();

            buttonLoad.Enabled = false;
        }



        private void LoadCombiFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename, Encoding.Default);

            comboBoxSpecies.Text = lines[2];
            textBoxFishery.Text = lines[11];
            textBoxSources.Text = lines[17];

            spreadSheetData.Rows.Clear();

            for (int i = 23; i < lines.Length; i++)
            {
                string[] pastesCells = lines[i].TrimEnd(System.Environment.NewLine.ToCharArray()).Split('\t');

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetData);
                gridRow.Cells[ColumnYear.Index].Value = Convert.ToInt32(pastesCells[0]);
                gridRow.Cells[ColumnCatch.Index].Value = Convert.ToDouble(pastesCells[3], CultureInfo.InvariantCulture);
                gridRow.Cells[ColumnEffort.Index].Value = Convert.ToDouble(pastesCells[2], CultureInfo.InvariantCulture);
                RecalcCpue(gridRow);
                spreadSheetData.Rows.Add(gridRow);
            }
        }

        private void RecalcCpue(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnCatch.Index].Value == null ||
                gridRow.Cells[ColumnEffort.Index].Value == null)
            {
                gridRow.Cells[ColumnCpue.Index].Value = null;
            }
            else
            {
                double cpue = (double)gridRow.Cells[ColumnCatch.Index].Value /
                    (double)gridRow.Cells[ColumnEffort.Index].Value;

                gridRow.Cells[ColumnCpue.Index].Value = cpue;
            }
        }

        private void comboBoxGear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSamplerType = ((FishSamplerTypeDisplay)comboBoxGear.SelectedItem).Type;
            UnitEffort.SwitchUE(comboBoxUE, SelectedSamplerType);
        }

        private void comboBoxUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUE.SelectedIndex != -1)
            {
                SelectedUnit = (UnitEffort)comboBoxUE.SelectedItem;
            }

            wizardPageEffort.AllowNext = (comboBoxUE.SelectedIndex != -1);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialogData.ShowDialog() == DialogResult.OK)
            {
                LoadCombiFile(openFileDialogData.FileName);
            }
        }

        private void wizardControlTac_Cancelling(object sender, CancelEventArgs e)
        {
            Close();
        }

        private void comboBoxSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSpecies = (Data.SpeciesRow)comboBoxSpecies.SelectedItem;
        }

        private void spreadSheetData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;

            RecalcCpue(spreadSheetData.Rows[e.RowIndex]);
        }

        private void userGetter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Data.GetInvestigators().Merge();
        }

        private void userGetter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBoxSources.Enabled = true;
            textBoxSources.Text = (string)e.Result;
        }

        private void wizardPageTitle_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (Data == null) return;

            foreach (int year in Data.GetYears())
            {
                CardStack yearlyData = Data.GetStack(year);

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetData);
                gridRow.Cells[ColumnYear.Index].Value = year;
                gridRow.Cells[ColumnCatch.Index].Value = yearlyData.Mass(SelectedSpecies);
                gridRow.Cells[ColumnEffort.Index].Value = yearlyData.GetEffort(
                    SelectedSamplerType, SelectedUnit.Variant);
                RecalcCpue(gridRow);
                spreadSheetData.Rows.Add(gridRow);
            }
        }
    }
}
