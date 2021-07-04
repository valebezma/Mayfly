using System;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Waters
{
    public partial class CardStream : Form
    {
        public bool IsChanged { get; set; }

        private WatersKey Data { get; set; }

        public CardStream(WatersKey watersKey)
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

                if (!value.IsLengthNull()) textBoxLength.Text = value.Length.ToString();

                if (!value.IsWatershedNull()) textBoxWatershed.Text = value.Watershed.ToString();

                if (!value.IsConsumptionNull()) textBoxSpend.Text = value.Consumption.ToString();

                if (!value.IsVolumeNull()) textBoxVolume.Text = value.Volume.ToString();

                if (!value.IsSlopeNull()) textBoxSlope.Text = value.Slope.ToString();

                if (value.IsOutflowNull())
                {
                    outflowSelector.WaterObject = null;
                }
                else
                {
                    outflowSelector.WaterObject = ((WatersKey.WaterDataTable)value.Table).FindByID(value.Outflow);

                    if (!value.IsMouthCoastNull())
                    {
                        comboBoxBank.SelectedIndex = value.MouthCoast;
                    }

                    if (!value.IsMouthToMouthNull())
                    {
                        textBoxMouthToMouth.Text = value.MouthToMouth.ToString();
                    }

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

            WaterRow.Type = (int)WaterType.Stream;

            if (textBoxName.Text.IsAcceptable()) waterRow.Water = textBoxName.Text;
            else waterRow.SetWaterNull();

            if (!outflowSelector.IsWaterSelected)
            {
                waterRow.SetOutflowNull();
            }
            else
            {
                if (comboBoxBank.SelectedIndex == -1) waterRow.SetMouthCoastNull();
                else waterRow.MouthCoast = comboBoxBank.SelectedIndex;

                if (textBoxMouthToMouth.Text.IsDoubleConvertible()) waterRow.MouthToMouth = double.Parse(textBoxMouthToMouth.Text);
                else waterRow.SetMouthToMouthNull();

                WatersKey.WaterRow destinationOutflow = outflowSelector.WaterObject;
                waterRow.Outflow = destinationOutflow.ID;
            }

            if (textBoxLength.Text.IsDoubleConvertible()) waterRow.Length = double.Parse(textBoxLength.Text);
            else waterRow.SetLengthNull();

            if (textBoxWatershed.Text.IsDoubleConvertible()) waterRow.Watershed = double.Parse(textBoxWatershed.Text);
            else waterRow.SetWatershedNull();

            if (textBoxSpend.Text.IsDoubleConvertible()) waterRow.Consumption = double.Parse(textBoxSpend.Text);
            else waterRow.SetConsumptionNull();

            if (textBoxVolume.Text.IsDoubleConvertible()) waterRow.Volume = double.Parse(textBoxVolume.Text);
            else waterRow.SetVolumeNull();

            if (textBoxSlope.Text.IsDoubleConvertible()) waterRow.Slope = double.Parse(textBoxSlope.Text);
            else waterRow.SetSlopeNull();
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
    }
}
