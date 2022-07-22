using System;
using System.Globalization;

namespace Mayfly.Wild
{
    public class WeatherState : IFormattable
    {
        public double Humidity { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double WindRate { get; set; }

        public double WindDirection { get; set; }

        public double Cloudage { get; set; }

        public int Event { get; set; }

        public int Degree { get; set; }

        public int Discretion { get; set; }

        public int AdditionalEvent { get; set; }

        public bool IsAvailable {
            get {
                if (!double.IsNaN(Humidity)) return true;
                if (!double.IsNaN(Temperature)) return true;
                if (!double.IsNaN(Pressure)) return true;
                if (!double.IsNaN(Cloudage)) return true;
                if (!double.IsNaN(WindRate)) return true;
                if (!double.IsNaN(WindDirection)) return true;
                if (Event != 0) return true;

                return false;
            }
        }



        public WeatherState() {
            Humidity = double.NaN;
            Temperature = double.NaN;
            Pressure = double.NaN;
            WindRate = double.NaN;
            WindDirection = double.NaN;
            Cloudage = double.NaN;

            Event = -1;
            Degree = -1;
            Discretion = -1;
            AdditionalEvent = -1;
        }

        public WeatherState(string value) : this() {
            string[] parameters = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters) {
                string[] fields = parameter.Split(new char[] { ':' });

                switch (fields[0]) {
                    case "HUM":
                        Humidity = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "TMP":
                        Temperature = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "PRS":
                        Pressure = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "CLD":
                        Cloudage = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "WNR":
                        WindRate = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "WND":
                        WindDirection = fields[1] == "-" ? double.NaN : Convert.ToDouble(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "EVT":
                        Event = fields[1] == "-" ? 0 : Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "DEG":
                        Degree = fields[1] == "-" ? 0 : Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "DCR":
                        Discretion = fields[1] == "-" ? 0 : Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                    case "ADE":
                        AdditionalEvent = fields[1] == "-" ? 0 : Convert.ToInt32(fields[1], CultureInfo.InvariantCulture);
                        break;
                }
            }
        }



        public bool IsHumidityNull() { return double.IsNaN(Humidity); }

        public bool IsTemperatureNull() { return double.IsNaN(Temperature); }

        public bool IsPressureNull() { return double.IsNaN(Pressure); }

        public bool IsWindRateNull() { return double.IsNaN(WindRate); }

        public bool IsWindDirectionNull() { return double.IsNaN(WindDirection); }

        public bool IsCloudageNull() { return double.IsNaN(Cloudage); }

        public bool IsEventNull() { return Event == 0; }

        public bool IsDegreeNull() { return Degree == 0; }

        public bool IsDiscretionNull() { return Discretion == 0; }

        public bool IsAdditionalEventNull() { return AdditionalEvent == 0; }



        public Report.Table GetReport() {

            Report.Table table1 = new Report.Table(Resources.Reports.Header.Weather);

            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Wild.Controls.WeatherControl));

            table1.StartRow();
            if (this.IsHumidityNull()) {
                table1.AddCellPrompt(resources.GetString("labelHumidity.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelHumidity.Text"), this.Humidity, 2);
            }
            if (this.IsTemperatureNull()) {
                table1.AddCellPrompt(resources.GetString("labelTemperatureAir.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelTemperatureAir.Text"), this.Temperature, 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsPressureNull()) {
                table1.AddCellPrompt(resources.GetString("labelAirPressure.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelAirPressure.Text"), this.Pressure, 2);
            }
            if (this.IsCloudageNull()) {
                table1.AddCellPrompt(resources.GetString("checkBoxCloudage.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("checkBoxCloudage.Text"), this.Cloudage + " / 8", 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsWindRateNull()) {
                table1.AddCellPrompt(resources.GetString("labelWindRate.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelWindRate.Text"), this.WindRate, 2);
            }
            if (this.IsWindDirectionNull()) {
                table1.AddCellPrompt(resources.GetString("labelWindDirection.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelWindDirection.Text"), this.WindDirection, 2);
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsEventNull()) {
                table1.AddCellPrompt(resources.GetString("labelEvent.Text"), Resources.Interface.Interface.NoPrecip, 4);
            } else {
                if (this.IsAdditionalEventNull()) {
                    table1.AddCellPrompt(resources.GetString("labelEvent.Text"),
                        UserSettings.WeatherIndex.Event.FindByID(this.Event).Display, 4);
                } else {
                    table1.AddCellPrompt(resources.GetString("labelEvent.Text"),
                        UserSettings.WeatherIndex.Event.FindByID(this.Event).Display + " + " +
                        UserSettings.WeatherIndex.Event.FindByID(this.AdditionalEvent).Display, 4);
                }
            }
            table1.EndRow();

            table1.StartRow();
            if (this.IsDegreeNull()) {
                table1.AddCellPrompt(resources.GetString("labelEventDegree.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelEventDegree.Text"),
                    UserSettings.WeatherIndex.Degree.FindByID(this.Degree).Display, 2);
            }
            if (this.IsDiscretionNull()) {
                table1.AddCellPrompt(resources.GetString("labelEventDiscretion.Text"), Constants.Null, 2);
            } else {
                table1.AddCellPrompt(resources.GetString("labelEventDiscretion.Text"),
                    UserSettings.WeatherIndex.Discretion.FindByID(this.Discretion).Display, 2);
            }
            table1.EndRow();

            return table1;
        }



        public string ToString(string format, IFormatProvider provider) {

            string result = string.Empty;

            switch (format) {
                case "f":
                    result += "HUM:" + (double.IsNaN(Humidity) ? "-" : Humidity.ToString("N1", provider)) + ";";
                    result += "TMP:" + (double.IsNaN(Temperature) ? "-" : Temperature.ToString("N1", provider)) + ";";
                    result += "PRS:" + (double.IsNaN(Pressure) ? "-" : Pressure.ToString("N1", provider)) + ";";
                    result += "CLD:" + (double.IsNaN(Cloudage) ? "-" : Cloudage.ToString("N0", provider)) + ";";
                    result += "WNR:" + (double.IsNaN(WindRate) ? "-" : WindRate.ToString("N1", provider)) + ";";
                    result += "WND:" + (double.IsNaN(WindDirection) ? "-" : WindDirection.ToString("N0", provider)) + ";";
                    result += "EVT:" + (IsEventNull() ? "-" : Event.ToString()) + ";";
                    result += "DEG:" + (IsDegreeNull() ? "-" : Degree.ToString()) + ";";
                    result += "DCR:" + (IsDiscretionNull() ? "-" : Discretion.ToString()) + ";";
                    result += "ADE:" + (IsAdditionalEventNull() ? "-" : AdditionalEvent.ToString()) + ";";
                    return result;

                default:
                    if (!double.IsNaN(Temperature)) result += string.Format("{0:0.0}°C;", Temperature);

                    //if (!this.IsTemperatureSurfaceNull()) result += string.Format(" ({0}°C)", this.TemperatureSurface);
                    //if (!this.IsAirPressureNull()) result += string.Format(" {0} кПа", this.AirPressure);
                    //if (!this.IsWindRateNull()) result += string.Format(" ветер {0} м/с", this.WindRate);
                    //if (!this.IsCloudageNull()) result += string.Format(" обл. {0}", this.Cloudage);


                    if (Degree != 0) result += UserSettings.WeatherIndex.Degree.FindByID(Degree).Display;

                    if (Event != 0) result += " " + UserSettings.WeatherIndex.Event.FindByID(Event).Display.ToLower();
                    else result += Resources.Interface.Interface.NoPrecip;

                    if (Discretion != 0) result += " " + UserSettings.WeatherIndex.Discretion.FindByID(Discretion).Display.ToLower();
                    if (AdditionalEvent != 0) result += " + " + UserSettings.WeatherIndex.Event.FindByID(AdditionalEvent).Display.ToLower();

                    return result;
            }
        }

        public string ToString(string format) {
            return ToString(format, CultureInfo.InvariantCulture);
        }

        public override string ToString() {
            return ToString(string.Empty);
        }
    }
}
