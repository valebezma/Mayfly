using System;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Waters
{
    public partial class CardLake : Form
    {
        public bool IsChanged { get; set; }

        private WatersKey Data { get; set; }



        public CardLake(WatersKey watersKey)
        {
            InitializeComponent();

            Data = watersKey;

            outflowSelector.CreateList();
            outflowSelector.Index = watersKey;

            IsChanged = false;
        }



        private WatersKey.WaterRow waterRow;

        public WatersKey.WaterRow WaterRow
        {
            get
            {
                return waterRow;
            }

            set
            {
                waterRow = value;

                Text = Resources.Interface.EditMode;

                if (!value.IsWaterNull()) textBoxName.Text = value.Water;

                if (!value.IsKindNull()) comboBoxKind.SelectedIndex = value.Kind;

                if (!value.IsAltitudeNull()) textBoxAltitude.Text = value.Altitude.ToString();

                if (!value.IsWatershedNull()) textBoxWatershed.Text = value.Watershed.ToString();

                if (!value.IsAreaNull()) textBoxArea.Text = value.Area.ToString();

                if (!value.IsVolumeNull()) textBoxVolume.Text = value.Volume.ToString();

                if (!value.IsDepthMaxNull()) textBoxDepthMax.Text = value.DepthMax.ToString();

                if (!value.IsDepthMidNull()) textBoxDepthAve.Text = value.DepthMid.ToString();

                if (!value.IsVegetationNull()) textBoxVegetation.Text = value.Vegetation.ToString();

                if (value.IsOutflowNull()) { outflowSelector.WaterObject = null; }
                else
                {
                    outflowSelector.WaterObject = ((WatersKey.WaterDataTable)value.Table).FindByID(value.Outflow);

                    if (!value.IsMouthCoastNull()) comboBoxBank.SelectedIndex = value.MouthCoast;

                    if (!value.IsMouthToMouthNull()) textBoxMouthToMouth.Text = value.MouthToMouth.ToString();

                    SetOutflow();
                }

                IsChanged = false;
            }
        }

        private void SaveRow()
        {
            if (waterRow == null)
            {
                waterRow = Data.Water.NewWaterRow();
                Data.Water.AddWaterRow(waterRow);
            }

            waterRow.Type = (int)WaterType.Lake;

            if (textBoxName.Text.IsAcceptable())
            {
                waterRow.Water = textBoxName.Text;
            }
            else
            {
                waterRow.SetWaterNull();
            }

            if (comboBoxKind.SelectedIndex == -1)
            {
                waterRow.SetKindNull();
            }
            else
            {
                waterRow.Kind = comboBoxKind.SelectedIndex;

                if (!outflowSelector.IsWaterSelected) waterRow.SetOutflowNull();
                else
                {
                    WatersKey.WaterRow destinationOutflow = outflowSelector.WaterObject;
                    waterRow.Outflow = destinationOutflow.ID;

                    if (comboBoxBank.SelectedIndex == -1) waterRow.SetMouthCoastNull();
                    else waterRow.MouthCoast = comboBoxBank.SelectedIndex;

                    if (textBoxMouthToMouth.Text.IsDoubleConvertible()) waterRow.MouthToMouth = double.Parse(textBoxMouthToMouth.Text);
                    else waterRow.SetMouthToMouthNull();
                }
            }

            if (textBoxAltitude.Text.IsDoubleConvertible()) waterRow.Altitude = double.Parse(textBoxAltitude.Text);
            else waterRow.SetAltitudeNull();

            if (textBoxWatershed.Text.IsDoubleConvertible()) waterRow.Watershed = double.Parse(textBoxWatershed.Text);
            else waterRow.SetWatershedNull();

            if (textBoxArea.Text.IsDoubleConvertible()) waterRow.Area = double.Parse(textBoxArea.Text);
            else waterRow.SetAreaNull();

            if (textBoxVolume.Text.IsDoubleConvertible()) waterRow.Volume = double.Parse(textBoxVolume.Text);
            else waterRow.SetVolumeNull();

            if (textBoxDepthMax.Text.IsDoubleConvertible()) waterRow.DepthMax = double.Parse(textBoxDepthMax.Text);
            else waterRow.SetDepthMaxNull();

            if (textBoxDepthAve.Text.IsDoubleConvertible()) waterRow.DepthMid = double.Parse(textBoxDepthAve.Text);
            else waterRow.SetDepthMidNull();

            if (textBoxVegetation.Text.IsDoubleConvertible()) waterRow.Vegetation = (int)double.Parse(textBoxVegetation.Text);
            else waterRow.SetVegetationNull();
        }

        public void SetOutflow()
        {
            labelMouthToMouth.Visible = textBoxMouthToMouth.Visible =
                labelBank.Visible = comboBoxBank.Visible =
                outflowSelector.IsWaterSelected;
        }

        private void elementChanged(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        private void waterSelectorOutflow_WaterChanged(object sender, EventArgs e)
        {
            IsChanged = true;
            SetOutflow();
        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(string));
        }

        private void comboBoxOutflow_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveRow();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            IsChanged = false;
            Close();
        }

        private void comboBoxKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelOutflow.Visible = outflowSelector.Visible = (comboBoxKind.SelectedIndex == 0);
            IsChanged = true;
        }
    }
}
