using Mayfly.Extensions;
using Mayfly.Geographics;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace Mayfly.Wild.Controls
{
    public partial class WeatherControl : UserControl
    {
        public event EventHandler Changed;

        public WeatherState Weather { get; set; }

        public bool IsWeatherAvailable
        {
            get
            {
                if (Weather == null) return false;
                return Weather.IsAvailable;
            }
        }



        public WeatherControl()
        {
            InitializeComponent();
            Weather = new WeatherState();

            trackBarCloudage.Value = 0;

            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                comboBoxEvent.DataSource = UserSettings.WeatherIndex.Event.Select(null, "ID desc");
                comboBoxDiscretion.DataSource = UserSettings.WeatherIndex.Discretion.Select(null, "ID desc");
                comboBoxEvent.SelectedIndex = -1;
                comboBoxEvent_SelectedIndexChanged(comboBoxEvent, new EventArgs());
            }
        }

        public void UpdateValues()
        {
            if (double.IsNaN(Weather.Humidity))
            {
                textBoxHumidity.Text = string.Empty;
            }
            else
            {
                textBoxHumidity.Text = Weather.Humidity.ToString();
            }

            if (double.IsNaN(Weather.Temperature))
            {
                textBoxTemperature.Text = string.Empty;
            }
            else
            {
                textBoxTemperature.Text = Weather.Temperature.ToString();
            }

            if (double.IsNaN(Weather.Pressure))
            {
                textBoxBaro.Text = string.Empty;
            }
            else
            {
                textBoxBaro.Text = Weather.Pressure.ToString();
            }

            if (double.IsNaN(Weather.WindRate))
            {
                textBoxWindRate.Text = string.Empty;
            }
            else
            {
                textBoxWindRate.Text = Weather.WindRate.ToString();
            }

            if (double.IsNaN(Weather.WindDirection))
            {
                textBoxWindDirection.Text = string.Empty;
            }
            else
            {
                textBoxWindDirection.Text = Weather.WindDirection.ToString();
            }

            if (double.IsNaN(Weather.Cloudage))
            {
                checkBoxCloudage.Checked = false;
            }
            else
            {
                checkBoxCloudage.Checked = true;
                trackBarCloudage.Value = (int)Weather.Cloudage;
            }

            if (Weather.IsEventNull())
            {
                comboBoxEvent.SelectedIndex = -1;
            }
            else
            {
                comboBoxEvent.SelectedItem = UserSettings.WeatherIndex.Event.FindByID(Weather.Event);
            }

            if (Weather.IsDegreeNull())
            {
                comboBoxDegree.SelectedIndex = -1;
            }
            else
            {
                comboBoxDegree.SelectedItem = UserSettings.WeatherIndex.Degree.FindByID(Weather.Degree);
            }

            if (Weather.IsDiscretionNull())
            {
                comboBoxDiscretion.SelectedIndex = -1;
            }
            else
            {
                comboBoxDiscretion.SelectedItem = UserSettings.WeatherIndex.Discretion.FindByID(Weather.Discretion);
            }

            if (Weather.IsAdditionalEventNull())
            {
                comboBoxAdditionalEvent.SelectedIndex = -1;
            }
            else
            {
                comboBoxAdditionalEvent.SelectedItem = UserSettings.WeatherIndex.Event.FindByID(Weather.AdditionalEvent);
            }
        }

        public void Save()
        {
            if (textBoxHumidity.Text.IsDoubleConvertible())
            {
                Weather.Humidity = double.Parse(textBoxHumidity.Text);
            }
            else
            {
                Weather.Humidity = double.NaN;
            }

            if (textBoxTemperature.Text.IsDoubleConvertible())
            {
                Weather.Temperature = double.Parse(textBoxTemperature.Text);
            }
            else
            {
                Weather.Temperature = double.NaN;
            }

            if (textBoxBaro.Text.IsDoubleConvertible())
            {
                Weather.Pressure = double.Parse(textBoxBaro.Text);
            }
            else
            {
                Weather.Pressure = double.NaN;
            }

            if (textBoxWindRate.Text.IsDoubleConvertible())
            {
                Weather.WindRate = double.Parse(textBoxWindRate.Text);
            }
            else
            {
                Weather.WindRate = double.NaN;
            }

            if (textBoxWindDirection.Text.IsDoubleConvertible())
            {
                Weather.WindDirection = (int)double.Parse(textBoxWindDirection.Text);
            }
            else
            {
                Weather.WindDirection = double.NaN;
            }

            if (checkBoxCloudage.Checked)
            {
                Weather.Cloudage = (double)trackBarCloudage.Value;
            }
            else
            {
                Weather.Cloudage = double.NaN;
            }

            Weather.Event = comboBoxEvent.SelectedIndex == -1 ? 0 : ((WeatherEvents.EventRow)comboBoxEvent.SelectedItem).ID;
            Weather.Degree = comboBoxDegree.SelectedIndex == -1 ? 0 : ((WeatherEvents.DegreeRow)comboBoxDegree.SelectedItem).ID;
            Weather.Discretion = comboBoxDiscretion.SelectedIndex == -1 ? 0 : ((WeatherEvents.DiscretionRow)comboBoxDiscretion.SelectedItem).ID;
            Weather.AdditionalEvent = comboBoxAdditionalEvent.SelectedIndex == -1 ? 0 : ((WeatherEvents.EventRow)comboBoxAdditionalEvent.SelectedItem).ID;
        }

        public void Clear()
        {
            textBoxHumidity.Text = string.Empty;
            textBoxTemperature.Text = string.Empty;
            textBoxBaro.Text = string.Empty;
            textBoxWindRate.Text = string.Empty;
            textBoxWindDirection.Text = string.Empty;
            checkBoxCloudage.Checked = false;
            trackBarCloudage.Value = 0;
            comboBoxEvent.SelectedIndex = -1;
        }

        
        
        private void value_Changed(object sender, EventArgs e)
        {
            if (Changed != null) Changed.Invoke(sender, e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }



        private void trackBarClouds_Scroll(object sender, EventArgs e)
        {
            toolTipAttention.ToolTipTitle = checkBoxCloudage.Text;
            toolTipAttention.Show(Service.CloudageName(trackBarCloudage.Value),
                trackBarCloudage, 0, trackBarCloudage.Height, 1500);
            value_Changed(sender, e);
        }

        private void checkBoxCloudage_CheckedChanged(object sender, EventArgs e)
        {
            trackBarCloudage.Enabled = checkBoxCloudage.Checked;
            value_Changed(sender, e);
        }

        private void comboBoxEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            WeatherEvents.EventRow selectedEventRow = (WeatherEvents.EventRow)comboBoxEvent.SelectedValue;

            if (selectedEventRow == null)
            {
                comboBoxDegree.Enabled = 
                    comboBoxDiscretion.Enabled = 
                    comboBoxAdditionalEvent.Enabled = false;
            }
            else
            {
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

        private void comboBoxEventDegree_EnabledChanged(object sender, EventArgs e)
        {
            labelEventDegree.Enabled = comboBoxDegree.Enabled;
        }

        private void comboBoxEventDiscretion_EnabledChanged(object sender, EventArgs e)
        {
            labelEventDiscretion.Enabled = comboBoxDiscretion.Enabled;
        }

        private void comboBoxAdditionalEvent_EnabledChanged(object sender, EventArgs e)
        {
            labelAdditionalEvent.Enabled = comboBoxAdditionalEvent.Enabled;
        }

        private void textBoxWindRate_TextChanged(object sender, EventArgs e)
        {
            value_Changed(sender, e);
            textBoxWindDirection.Enabled = textBoxWindRate.Text.IsDoubleConvertible();
        }

        private void textBoxWindDirection_EnabledChanged(object sender, EventArgs e)
        {
            labelWindDirection.Enabled = textBoxWindRate.Enabled;
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }
    }
}
