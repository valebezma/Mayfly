using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Wild.Controls
{
    public partial class AquaControl : UserControl
    {
        public event EventHandler Changed;

        [Browsable(false)]
        public AquaState AquaState { get; set; }

        public bool IsStateAvailable
        {
            get
            {
                if (AquaState == null) return false;
                if (AquaState.IsPhysicalsAvailable) return true;
                if (AquaState.IsChemicalsAvailable) return true;
                if (AquaState.IsOrganolepticsAvailable) return true;
                return false;
            }
        }




        public AquaControl()
        {
            InitializeComponent();

            AquaState = new AquaState();

            colorPicker1.AddWaterColors();
            colorPicker1.SelectedIndex = -1;
        }

        public AquaControl(string value1, string value2, string value3)
            : this()
        {
            AquaState = new AquaState(value1, value2, value3);
            UpdateValues();
        }



        public void UpdateValues()
        {
            if (AquaState.IsTemperatureBottomNull())
            {
                textBoxTempBottom.Text = string.Empty;
            }
            else
            {
                textBoxTempBottom.Text = AquaState.TemperatureBottom.ToString();
            }

            if (AquaState.IsTemperatureSurfaceNull())
            {
                textBoxTempSurface.Text = string.Empty;
            }
            else
            {
                textBoxTempSurface.Text = AquaState.TemperatureSurface.ToString();
            }

            if (AquaState.IsFlowRateNull())
            {
                textBoxFlowRate.Text = string.Empty;
            }
            else
            {
                textBoxFlowRate.Text = AquaState.FlowRate.ToString();
            }

            if (AquaState.IsLimpidityNull())
            {
                textBoxLimpidity.Text = string.Empty;
            }
            else
            {
                textBoxLimpidity.Text = AquaState.Limpidity.ToString();
            }

            if (AquaState.IsColourNull())
            {
                colorPicker1.SelectedIndex = -1;
            }
            else
            {
                colorPicker1.SelectedIndex = AquaState.Colour;
            }

            if (AquaState.IsOdorNull())
            {
                checkBoxOdor.CheckState = CheckState.Indeterminate;
            }
            else
            {
                checkBoxOdor.CheckState = (CheckState)AquaState.Odor;
            }

            if (AquaState.IsSewageNull())
            {
                checkBoxSewage.CheckState = CheckState.Indeterminate;
            }
            else
            {
                checkBoxSewage.CheckState = (CheckState)AquaState.Sewage;
            }

            if (AquaState.IsFoamNull())
            {
                checkBoxFoam.CheckState = CheckState.Indeterminate;
            }
            else
            {
                checkBoxFoam.CheckState = (CheckState)AquaState.Foam;
            }

            if (AquaState.IsTurbidityNull())
            {
                checkBoxTurbidity.CheckState = CheckState.Indeterminate;
            }
            else
            {
                checkBoxTurbidity.CheckState = (CheckState)AquaState.Turbidity;
            }

            if (AquaState.IsConductivityNull())
            {
                textBoxConductivity.Text = string.Empty;
            }
            else
            {
                textBoxConductivity.Text = AquaState.Conductivity.ToString();
            }

            if (AquaState.IsDissolvedOxygenNull())
            {
                textBoxDissolvedOxygen.Text = string.Empty;
            }
            else
            {
                textBoxDissolvedOxygen.Text = AquaState.DissolvedOxygen.ToString();
            }

            if (AquaState.IsOxygenSaturationNull())
            {
                textBoxOxygenSaturation.Text = string.Empty;
            }
            else
            {
                textBoxOxygenSaturation.Text = AquaState.OxygenSaturation.ToString();
            }

            if (AquaState.IspHNull())
            {
                textBoxpH.Text = string.Empty;
            }
            else
            {
                textBoxpH.Text = AquaState.pH.ToString();
            }
        }

        public void Save()
        {
            if (textBoxTempBottom.Text.IsDoubleConvertible())
            {
                AquaState.TemperatureBottom = double.Parse(textBoxTempBottom.Text);
            }

            if (textBoxTempSurface.Text.IsDoubleConvertible())
            {
                AquaState.TemperatureSurface = double.Parse(textBoxTempSurface.Text);
            }

            if (textBoxFlowRate.Text.IsDoubleConvertible())
            {
                AquaState.FlowRate = double.Parse(textBoxFlowRate.Text);
            }

            if (textBoxLimpidity.Text.IsDoubleConvertible())
            {
                AquaState.Limpidity = double.Parse(textBoxLimpidity.Text);
            }

            if (colorPicker1.SelectedIndex > -1)
            {
                AquaState.Colour = colorPicker1.SelectedIndex;
            }



            //switch (checkBoxOdor.CheckState)
            //{
            //    case CheckState.Checked:
            //        AquaState.Odor = OrganolepticState.Present;
            //        break;
            //    case CheckState.Indeterminate:
            //        AquaState.Odor = OrganolepticState.None;
            //        break;
            //    case CheckState.Unchecked:
            //        AquaState.Odor = OrganolepticState.Absent;
            //        break;
            //}

            AquaState.SetOrganoleptics((OrganolepticState)checkBoxOdor.CheckState,
                (OrganolepticState)checkBoxSewage.CheckState,
                (OrganolepticState)checkBoxFoam.CheckState,
                (OrganolepticState)checkBoxTurbidity.CheckState);

            //if (checkBoxOdor.CheckState == CheckState.Indeterminate)
            //    AquaState.Odor = OrganolepticState.None;
            //else
            //{
            //    AquaState.Odor = checkBoxOdor.Checked ? OrganolepticState.Present : OrganolepticState.Absent;
            //}

            //if (checkBoxSewage.CheckState == CheckState.Indeterminate)
            //    AquaState.Sewage = OrganolepticState.None;
            //else
            //{
            //    AquaState.Sewage = checkBoxSewage.Checked ? OrganolepticState.Present : OrganolepticState.Absent;
            //}

            //if (checkBoxFoam.CheckState == CheckState.Indeterminate)
            //    AquaState.Foam = OrganolepticState.None;
            //else
            //{
            //    AquaState.Foam = checkBoxFoam.Checked ? OrganolepticState.Present : OrganolepticState.Absent;
            //}

            //if (checkBoxTurbidity.CheckState == CheckState.Indeterminate)
            //    AquaState.Turbidity = OrganolepticState.None;
            //else
            //{
            //    AquaState.Turbidity = checkBoxTurbidity.Checked ? OrganolepticState.Present : OrganolepticState.Absent;
            //}

            if (textBoxConductivity.Text.IsDoubleConvertible())
            {
                AquaState.Conductivity = double.Parse(textBoxConductivity.Text);
            }

            if (textBoxDissolvedOxygen.Text.IsDoubleConvertible())
            {
                AquaState.DissolvedOxygen = double.Parse(textBoxDissolvedOxygen.Text);
            }

            if (textBoxOxygenSaturation.Text.IsDoubleConvertible())
            {
                AquaState.OxygenSaturation = double.Parse(textBoxOxygenSaturation.Text);
            }

            if (textBoxpH.Text.IsDoubleConvertible())
            {
                AquaState.pH = double.Parse(textBoxpH.Text);
            }
        }

        public void Clear()
        {
            textBoxTempBottom.Text = string.Empty;
            textBoxTempSurface.Text = string.Empty;
            textBoxFlowRate.Text = string.Empty;
            textBoxLimpidity.Text = string.Empty;
            colorPicker1.SelectedIndex = -1;
            checkBoxOdor.CheckState = CheckState.Indeterminate;
            checkBoxSewage.CheckState = CheckState.Indeterminate;
            checkBoxFoam.CheckState = CheckState.Indeterminate;
            checkBoxTurbidity.CheckState = CheckState.Indeterminate;
            textBoxConductivity.Text = string.Empty;
            textBoxDissolvedOxygen.Text = string.Empty;
            textBoxOxygenSaturation.Text = string.Empty;
            textBoxpH.Text = string.Empty;
        }



        private void value_Changed(object sender, EventArgs e)
        {
            if (Changed != null) Changed.Invoke(sender, e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void colorPicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }
    }
}
