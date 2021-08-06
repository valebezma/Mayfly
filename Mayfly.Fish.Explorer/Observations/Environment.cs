using Mayfly.Wild;
using Mayfly.Geographics;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    public partial class Environment : Form
    {
        Surveys.TimelineRow TimelineRow;

        public bool IsChanged { get; set; }



        public Environment()
        {
            InitializeComponent();
        }

        public Environment(Surveys.TimelineRow timelineRow) : this()
        {
            TimelineRow = timelineRow;

            Text = String.Format("{0} - {1}", TimelineRow.GetShortDescription(), Text);

            if (TimelineRow.IsWaterTemperatureNull())
            {
                textBoxTempSurface.Text = string.Empty;
            }
            else
            {
                textBoxTempSurface.Text = TimelineRow.WaterTemperature.ToString();
            }

            if (TimelineRow.IsWeatherNull())
            {
                weatherControl1.Clear();
            }
            else
            {
                weatherControl1.Weather = TimelineRow.WeatherConditions;
                weatherControl1.UpdateValues();
            }
        }



        public void Save()
        {
            #region Water conditions

            if (textBoxTempSurface.Text.IsDoubleConvertible())
            {
                TimelineRow.WaterTemperature = double.Parse(textBoxTempSurface.Text);
            }
            else
            {
                TimelineRow.SetWaterTemperatureNull();
            }

            #endregion

            weatherControl1.Save();

            if (weatherControl1.Weather.IsAvailable)
            {
                TimelineRow.Weather = weatherControl1.Weather.Protocol;
            }
        }




        private void value_Changed(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                Save();
            }

            Close();
        }
    }
}
