using System;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Waters
{
    public partial class CardTank : Form
    {
        public bool IsChanged { get; set; }

        private WatersKey Data { get; set; }

        public CardTank(WatersKey watersKey)
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

                if (!value.IsLengthNull()) textBoxLength.Text = value.Length.ToString();

                if (!value.IsAltitudeNull()) textBoxAltitude.Text = value.Altitude.ToString();

                if (!value.IsAreaNull()) textBoxArea.Text = value.Area.ToString();

                if (!value.IsAreaDeadNull()) textBoxAreaDead.Text = value.AreaDead.ToString();

                if (!value.IsVolumeNull()) textBoxVolume.Text = value.Volume.ToString();

                if (!value.IsVolumeDeadNull()) textBoxVolumeDead.Text = value.VolumeDead.ToString();

                if (!value.IsDepthMaxNull()) textBoxDepthMax.Text = value.DepthMax.ToString();

                if (!value.IsDepthMidNull()) textBoxDepthAve.Text = value.DepthMid.ToString();

                if (!value.IsFlowageNull()) textBoxFlowage.Text = value.Flowage.ToString();

                if (!value.IsCyclingNull()) textBoxCycling.Text = value.Cycling.ToString();

                if (!value.IsVegetationNull()) textBoxVegetation.Text = value.Vegetation.ToString();

                if (value.IsOutflowNull())
                {
                    outflowSelector.WaterObject = null;
                }
                else
                {
                    outflowSelector.WaterObject = ((WatersKey.WaterDataTable)value.Table).FindByID(value.Outflow);

                    SetOutflow();

                    if (!value.IsMouthToMouthNull()) textBoxMouthToMouth.Text = value.MouthToMouth.ToString();
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

            waterRow.Type = (int)WaterType.Tank;

            if (textBoxName.Text.IsAcceptable()) waterRow.Water = textBoxName.Text;
            else waterRow.SetWaterNull();

            if (textBoxBuilt.Text.IsDoubleConvertible()) waterRow.Built = (int)double.Parse(textBoxBuilt.Text);
            else waterRow.SetBuiltNull();

            if (comboBoxKind.SelectedIndex == -1) waterRow.SetKindNull();
            else waterRow.Kind = comboBoxKind.SelectedIndex;

            if (!outflowSelector.IsWaterSelected) waterRow.SetOutflowNull();
            else waterRow.Outflow = outflowSelector.WaterObject.ID;

            if (textBoxMouthToMouth.Text.IsDoubleConvertible()) waterRow.MouthToMouth = double.Parse(textBoxMouthToMouth.Text);
            else waterRow.SetMouthToMouthNull();

            if (textBoxLength.Text.IsDoubleConvertible()) waterRow.Length = double.Parse(textBoxLength.Text);
            else waterRow.SetLengthNull();

            if (textBoxAltitude.Text.IsDoubleConvertible()) waterRow.Altitude = double.Parse(textBoxAltitude.Text);
            else waterRow.SetAltitudeNull();

            if (textBoxArea.Text.IsDoubleConvertible()) waterRow.Area = double.Parse(textBoxArea.Text);
            else waterRow.SetAreaNull();

            if (textBoxAreaDead.Text.IsDoubleConvertible()) waterRow.AreaDead = double.Parse(textBoxAreaDead.Text);
            else waterRow.SetAreaDeadNull();

            if (textBoxVolume.Text.IsDoubleConvertible()) waterRow.Volume = double.Parse(textBoxVolume.Text);
            else waterRow.SetVolumeNull();

            if (textBoxVolumeDead.Text.IsDoubleConvertible()) waterRow.VolumeDead = double.Parse(textBoxVolumeDead.Text);
            else waterRow.SetVolumeDeadNull();

            if (textBoxDepthMax.Text.IsDoubleConvertible()) waterRow.DepthMax = double.Parse(textBoxDepthMax.Text);
            else waterRow.SetDepthMaxNull();

            if (textBoxDepthAve.Text.IsDoubleConvertible()) waterRow.DepthMid = double.Parse(textBoxDepthAve.Text);
            else waterRow.SetDepthMidNull();

            if (textBoxFlowage.Text.IsDoubleConvertible()) waterRow.Flowage = double.Parse(textBoxFlowage.Text);
            else waterRow.SetFlowageNull();

            if (textBoxCycling.Text.IsDoubleConvertible()) waterRow.Cycling = double.Parse(textBoxCycling.Text);
            else waterRow.SetCyclingNull();

            if (textBoxVegetation.Text.IsDoubleConvertible()) waterRow.Vegetation = (int)double.Parse(textBoxVegetation.Text);
            else waterRow.SetVegetationNull();
        }

        public void SetOutflow()
        {
            labelMouthToMouth.Visible = textBoxMouthToMouth.Visible =
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
