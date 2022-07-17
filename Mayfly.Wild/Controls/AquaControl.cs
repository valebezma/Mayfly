using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Wild.Controls
{
    public partial class AquaControl : UserControl
    {
        public event EventHandler Changed;

        [Browsable(false)]
        public AquaState AquaState { get; set; }

        public bool IsStateAvailable {
            get {
                if (AquaState == null) return false;
                if (AquaState.IsPhysicalsAvailable) return true;
                if (AquaState.IsChemicalsAvailable) return true;
                if (AquaState.IsOrganolepticsAvailable) return true;
                return false;
            }
        }




        public AquaControl() {
            InitializeComponent();

            AquaState = new AquaState();

            colorPicker1.AddWaterColors();
            colorPicker1.SelectedIndex = -1;
        }

        public AquaControl(string value1, string value2, string value3)
            : this() {
            AquaState = new AquaState(value1, value2, value3);
            UpdateValues();
        }



        public void UpdateValues() {
            if (AquaState.IsTemperatureBottomNull()) {
                numericTempBottom.Clear();
            } else {
                numericTempBottom.Value = AquaState.TemperatureBottom;
            }

            if (AquaState.IsTemperatureSurfaceNull()) {
                numericTempSurface.Clear();
            } else {
                numericTempSurface.Value = AquaState.TemperatureSurface;
            }

            if (AquaState.IsFlowRateNull()) {
                numericFlowRate.Clear();
            } else {
                numericFlowRate.Value = AquaState.FlowRate;
            }

            if (AquaState.IsLimpidityNull()) {
                numericLimpidity.Clear();
            } else {
                numericLimpidity.Value = AquaState.Limpidity;
            }

            if (AquaState.IsColourNull()) {
                colorPicker1.SelectedIndex = -1;
            } else {
                colorPicker1.SelectedIndex = AquaState.Colour;
            }

            if (AquaState.IsOdorNull()) {
                checkBoxOdor.CheckState = CheckState.Indeterminate;
            } else {
                checkBoxOdor.CheckState = (CheckState)AquaState.Odor;
            }

            if (AquaState.IsSewageNull()) {
                checkBoxSewage.CheckState = CheckState.Indeterminate;
            } else {
                checkBoxSewage.CheckState = (CheckState)AquaState.Sewage;
            }

            if (AquaState.IsFoamNull()) {
                checkBoxFoam.CheckState = CheckState.Indeterminate;
            } else {
                checkBoxFoam.CheckState = (CheckState)AquaState.Foam;
            }

            if (AquaState.IsTurbidityNull()) {
                checkBoxTurbidity.CheckState = CheckState.Indeterminate;
            } else {
                checkBoxTurbidity.CheckState = (CheckState)AquaState.Turbidity;
            }

            if (AquaState.IsConductivityNull()) {
                numericConductivity.Clear();
            } else {
                numericConductivity.Value = AquaState.Conductivity;
            }

            if (AquaState.IsDissolvedOxygenNull()) {
                numericDissolvedOxygen.Clear();
            } else {
                numericDissolvedOxygen.Value = AquaState.DissolvedOxygen;
            }

            if (AquaState.IsOxygenSaturationNull()) {
                numericOxygenSaturation.Clear();
            } else {
                numericOxygenSaturation.Value = AquaState.OxygenSaturation;
            }

            if (AquaState.IspHNull()) {
                numericpH.Clear();
            } else {
                numericpH.Value = AquaState.pH;
            }
        }

        public void Save() {

            AquaState.TemperatureBottom = numericTempBottom.IsSet ? numericTempBottom.Value : double.NaN;
            AquaState.TemperatureSurface = numericTempSurface.IsSet ? numericTempSurface.Value : double.NaN;
            AquaState.FlowRate = numericFlowRate.IsSet ? numericFlowRate.Value : double.NaN;
            AquaState.Limpidity = numericLimpidity.IsSet ? numericLimpidity.Value : double.NaN;
            AquaState.Colour = colorPicker1.SelectedIndex;

            AquaState.SetOrganoleptics((OrganolepticState)checkBoxOdor.CheckState,
                (OrganolepticState)checkBoxSewage.CheckState,
                (OrganolepticState)checkBoxFoam.CheckState,
                (OrganolepticState)checkBoxTurbidity.CheckState);

            AquaState.Conductivity = numericConductivity.IsSet ? numericConductivity.Value : double.NaN;
            AquaState.DissolvedOxygen = numericDissolvedOxygen.IsSet ? numericDissolvedOxygen.Value : double.NaN;
            AquaState.OxygenSaturation = numericOxygenSaturation.IsSet ? numericOxygenSaturation.Value : double.NaN;
            AquaState.pH = numericpH.IsSet ? numericpH.Value : double.NaN;
        }

        public void Clear() {

            numericTempBottom.Clear();
            numericTempSurface.Clear();
            numericFlowRate.Clear();
            numericLimpidity.Clear();
            colorPicker1.SelectedIndex = -1;
            checkBoxOdor.CheckState = CheckState.Indeterminate;
            checkBoxSewage.CheckState = CheckState.Indeterminate;
            checkBoxFoam.CheckState = CheckState.Indeterminate;
            checkBoxTurbidity.CheckState = CheckState.Indeterminate;
            numericConductivity.Clear();
            numericDissolvedOxygen.Clear();
            numericOxygenSaturation.Clear();
            numericpH.Clear();
        }



        private void value_Changed(object sender, EventArgs e) {
            if (Changed != null) Changed.Invoke(sender, e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e) {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void colorPicker1_KeyPress(object sender, KeyPressEventArgs e) {
            ((ComboBox)sender).HandleInput(e);
        }
    }
}
