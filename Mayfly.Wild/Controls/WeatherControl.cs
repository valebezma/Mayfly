using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Wild.Controls
{
    public partial class WeatherControl : UserControl
    {
        public event EventHandler Changed;

        public WeatherState Weather { get; set; }

        public bool IsWeatherAvailable {
            get {
                if (Weather == null) return false;
                return Weather.IsAvailable;
            }
        }



        public WeatherControl() {
            InitializeComponent();
            Weather = new WeatherState();

            trackBarCloudage.Value = 0;

            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime) {
                comboBoxEvent.DataSource = UserSettings.WeatherIndex.Event.Select(null, "ID desc");
                comboBoxDiscretion.DataSource = UserSettings.WeatherIndex.Discretion.Select(null, "ID desc");
                comboBoxEvent.SelectedIndex = -1;
                comboBoxEvent_SelectedIndexChanged(comboBoxEvent, new EventArgs());
            }
        }

        public void UpdateValues() {
            if (double.IsNaN(Weather.Humidity)) {
                numericHumidity.Clear();
            } else {
                numericHumidity.Value = Weather.Humidity;
            }

            if (double.IsNaN(Weather.Temperature)) {
                numericTemperature.Clear();
            } else {
                numericTemperature.Value = Weather.Temperature;
            }

            if (double.IsNaN(Weather.Pressure)) {
                numericBaro.Clear();
            } else {
                numericBaro.Value = Weather.Pressure;
            }

            if (double.IsNaN(Weather.WindRate)) {
                numericWindRate.Clear();
            } else {
                numericWindRate.Value = Weather.WindRate;
            }

            if (double.IsNaN(Weather.WindDirection)) {
                numericWindDirection.Clear();
            } else {
                numericWindDirection.Value = Weather.WindDirection;
            }

            if (double.IsNaN(Weather.Cloudage)) {
                checkBoxCloudage.Checked = false;
            } else {
                checkBoxCloudage.Checked = true;
                trackBarCloudage.Value = (int)Weather.Cloudage;
            }

            if (Weather.IsEventNull()) {
                comboBoxEvent.SelectedIndex = -1;
            } else {
                comboBoxEvent.SelectedItem = UserSettings.WeatherIndex.Event.FindByID(Weather.Event);
            }

            if (Weather.IsDegreeNull()) {
                comboBoxDegree.SelectedIndex = -1;
            } else {
                comboBoxDegree.SelectedItem = UserSettings.WeatherIndex.Degree.FindByID(Weather.Degree);
            }

            if (Weather.IsDiscretionNull()) {
                comboBoxDiscretion.SelectedIndex = -1;
            } else {
                comboBoxDiscretion.SelectedItem = UserSettings.WeatherIndex.Discretion.FindByID(Weather.Discretion);
            }

            if (Weather.IsAdditionalEventNull()) {
                comboBoxAdditionalEvent.SelectedIndex = -1;
            } else {
                comboBoxAdditionalEvent.SelectedItem = UserSettings.WeatherIndex.Event.FindByID(Weather.AdditionalEvent);
            }
        }

        public void Save() {

            Weather.Humidity = numericHumidity.IsSet ? numericHumidity.Value : double.NaN;
            Weather.Temperature = numericTemperature.IsSet ? numericTemperature.Value : double.NaN;
            Weather.Pressure = numericBaro.IsSet ? numericBaro.Value : double.NaN;
            Weather.WindRate = numericWindRate.IsSet ? numericWindRate.Value : double.NaN;
            Weather.WindDirection = numericWindDirection.IsSet ? numericWindDirection.Value : double.NaN;
            Weather.Cloudage = checkBoxCloudage.Checked ? trackBarCloudage.Value : double.NaN;

            Weather.Event = comboBoxEvent.SelectedIndex == -1 ? 0 : ((WeatherEvents.EventRow)comboBoxEvent.SelectedItem).ID;
            Weather.Degree = comboBoxDegree.SelectedIndex == -1 ? 0 : ((WeatherEvents.DegreeRow)comboBoxDegree.SelectedItem).ID;
            Weather.Discretion = comboBoxDiscretion.SelectedIndex == -1 ? 0 : ((WeatherEvents.DiscretionRow)comboBoxDiscretion.SelectedItem).ID;
            Weather.AdditionalEvent = comboBoxAdditionalEvent.SelectedIndex == -1 ? 0 : ((WeatherEvents.EventRow)comboBoxAdditionalEvent.SelectedItem).ID;
        }

        public void Clear() {
            numericHumidity.Clear();
            numericTemperature.Clear();
            numericBaro.Clear();
            numericWindRate.Clear();
            numericWindDirection.Clear();
            checkBoxCloudage.Checked = false;
            trackBarCloudage.Value = 0;
            comboBoxEvent.SelectedIndex = -1;
        }



        private void value_Changed(object sender, EventArgs e) {
            if (Changed != null) Changed.Invoke(sender, e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e) {
            ((Control)sender).HandleInput(e, typeof(double));
        }



        private void trackBarClouds_Scroll(object sender, EventArgs e) {
            toolTipAttention.ToolTipTitle = checkBoxCloudage.Text;
            toolTipAttention.Show(Service.CloudageName(trackBarCloudage.Value),
                trackBarCloudage, 0, trackBarCloudage.Height, 1500);
            value_Changed(sender, e);
        }

        private void checkBoxCloudage_CheckedChanged(object sender, EventArgs e) {
            trackBarCloudage.Enabled = checkBoxCloudage.Checked;
            value_Changed(sender, e);
        }

        private void comboBoxEvent_SelectedIndexChanged(object sender, EventArgs e) {
            WeatherEvents.EventRow selectedEventRow = (WeatherEvents.EventRow)comboBoxEvent.SelectedValue;

            if (selectedEventRow == null) {
                comboBoxDegree.Enabled =
                    comboBoxDiscretion.Enabled =
                    comboBoxAdditionalEvent.Enabled = false;
            } else {
                comboBoxDegree.Enabled = selectedEventRow.IsDegreeAvailable;
                comboBoxDegree.DataSource = selectedEventRow.AvailableDegrees;
                comboBoxDegree.SelectedIndex = -1;

                comboBoxDiscretion.Enabled = selectedEventRow.IsDiscretionAvailable;
                comboBoxDiscretion.SelectedIndex = -1;

                comboBoxAdditionalEvent.Enabled = selectedEventRow.IsAdditionalEventAvailable;
                comboBoxAdditionalEvent.DataSource = selectedEventRow.AvailableAdditionalEvents;
                comboBoxAdditionalEvent.SelectedIndex = -1;
            }

            value_Changed(sender, e);
        }

        private void comboBoxEventDegree_EnabledChanged(object sender, EventArgs e) {
            labelEventDegree.Enabled = comboBoxDegree.Enabled;
        }

        private void comboBoxEventDiscretion_EnabledChanged(object sender, EventArgs e) {
            labelEventDiscretion.Enabled = comboBoxDiscretion.Enabled;
        }

        private void comboBoxAdditionalEvent_EnabledChanged(object sender, EventArgs e) {
            labelAdditionalEvent.Enabled = comboBoxAdditionalEvent.Enabled;
        }

        private void numericWindRate_TextChanged(object sender, EventArgs e) {
            value_Changed(sender, e);
            numericWindDirection.Enabled = numericWindRate.IsSet;
        }

        private void numericWindDirection_EnabledChanged(object sender, EventArgs e) {
            labelWindDirection.Enabled = numericWindRate.Enabled;
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e) {
            ((ComboBox)sender).HandleInput(e);
        }
    }
}
