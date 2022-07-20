using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using Mayfly.Wild;
using System.Windows.Forms;
using static Mayfly.Fish.UserSettings;
using static Mayfly.UserSettings;
using static Mayfly.Wild.SettingsReader;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsControlEquipment : UserControl, ISettingControl
    {
        public SettingsControlEquipment() {

            InitializeComponent();

            columnMesh.ValueType = typeof(int);
            columnLength.ValueType = typeof(double);
            columnHeight.ValueType = typeof(double);

            //columnSampler.DataSource = SamplersIndex.GetPassives();
            columnSampler.DataSource = SamplersIndex.Sampler.Select();
            columnSampler.DisplayMember = "Sampler";
            columnSampler.ValueMember = "ShortName";
        }

        public void LoadSettings() {

            spreadSheetGears.Rows.Clear();

            foreach (Survey.EquipmentRow unitRow in SettingsReader.Equipment.Equipment) {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGears);
                gridRow.Cells[columnSampler.Index].Value = unitRow.SamplerRow;
                gridRow.Cells[columnMesh.Index].Value = unitRow.GetVirtue("Mesh");
                gridRow.Cells[columnLength.Index].Value = unitRow.GetVirtue("Length");
                gridRow.Cells[columnHeight.Index].Value = unitRow.GetVirtue("Height");
                spreadSheetGears.Rows.Add(gridRow);
            }
        }

        public void SaveSettings() {

            Equipment = new Survey();
            foreach (DataGridViewRow gridRow in spreadSheetGears.Rows) {
                if (gridRow.IsNewRow) continue;
                if (gridRow.Cells[columnSampler.Index].Value == null) continue;

                Survey.SamplerRow selectedSamplerRow = (Survey.SamplerRow)gridRow.Cells[columnSampler.Index].Value;
                Survey.SamplerRow samplerRow = selectedSamplerRow.CopyTo(Equipment);

                Survey.EquipmentRow equipmentRow = Equipment.Equipment.AddEquipmentRow(samplerRow);

                foreach (Survey.VirtueRow virtueRow in Equipment.Virtue) {
                    if (spreadSheetGears.Columns["column" + virtueRow.Name] != null && gridRow.Cells["column" + virtueRow.Name].Value is double value) {
                        Equipment.EquipmentVirtue.AddEquipmentVirtueRow(equipmentRow, virtueRow, value);
                    }
                }
            }
            Wild.Service.SaveEquipment();
        }

        private void buttonClear_Click(object sender, EventArgs e) {

            spreadSheetGears.Rows.Clear();
        }
    }
}
