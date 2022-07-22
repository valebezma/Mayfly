using System;
using System.Windows.Forms;
using static Mayfly.Wild.SettingsReader;
using Mayfly.Controls;

namespace Mayfly.Wild.Controls
{
    public partial class SettingsPageEquipment : SettingsPage, ISettingsPage
    {
        public SettingsPageEquipment() {

            InitializeComponent();

            columnMesh.ValueType = typeof(int);
            columnLength.ValueType = typeof(double);
            columnHeight.ValueType = typeof(double);

            //columnSampler.DataSource = SamplersIndex.GetPassives();
            columnSampler.DataSource = SamplersIndex.Sampler.Select();
            columnSampler.DisplayMember = "Name";
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
