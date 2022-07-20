using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mayfly.Fish.UserSettings;
using static Mayfly.UserSettings;
using static Mayfly.Wild.SettingsReader;
using Mayfly.Wild;

namespace Mayfly.Fish
{
    public partial class SettingsControlGears : UserControl, ISettingControl
    {
        public SettingsControlGears() {

            InitializeComponent();

            spreadSheetOpening.StringVariants = SamplersIndex.Virtue.FindByName("Opening").GetSamplers();
            columnOpeningGear.ValueType = typeof(string);
            columnOpeningValue.ValueType = typeof(double);
        }

        public void LoadSettings() {

            numericUpDownOpeningDefault.Value = 100 * (decimal)DefaultOpening;
            numericUpDownStdLength.Value = (decimal)GillnetStdLength;
            numericUpDownStdHeight.Value = (decimal)GillnetStdHeight;
            numericUpDownStdSoak.Value = GillnetStdExposure;

            spreadSheetOpening.Rows.Clear();

            foreach (Survey.SamplerRow samplerRow in SamplersIndex.Sampler) {
                loadOpening(samplerRow);
            }
        }

        public void SaveSettings() {

            DefaultOpening = .01 * (double)numericUpDownOpeningDefault.Value;

            ClearFolder(FeatureKey, nameof(Service.Opening));
            foreach (DataGridViewRow gridRow in spreadSheetOpening.Rows) {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[columnOpeningGear.Index].Value == null) continue;
                if (gridRow.Cells[columnOpeningValue.Index].Value == null) continue;
                if ((double)gridRow.Cells[columnOpeningValue.Index].Value == DefaultOpening) continue;
                Service.SaveOpening(((Survey.SamplerRow)gridRow.Tag).ID, (double)gridRow.Cells[columnOpeningValue.Index].Value);
            }

            GillnetStdLength = (double)numericUpDownStdLength.Value;
            GillnetStdHeight = (double)numericUpDownStdHeight.Value;
            GillnetStdExposure = (int)numericUpDownStdSoak.Value;
        }

        private void loadOpening(Survey.SamplerRow samplerRow) {

            double open = Service.DefaultOpening(samplerRow.ID);
            if (double.IsNaN(open)) return;
            if (open == Service.DefaultOpening()) return;

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetOpening);
            gridRow.Cells[columnOpeningGear.Index].Value = samplerRow;
            gridRow.Cells[columnOpeningValue.Index].Value = open;
            gridRow.Tag = samplerRow;
            spreadSheetOpening.Rows.Add(gridRow);
        }
    }
}
