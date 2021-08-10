using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Device.Location;
using System.Drawing;
using System.Windows.Forms;

namespace Mayfly.Geographics
{
    public partial class WaypointControl : UserControl
    {
        public event EventHandler Changed;

        public event LocationDataEventHandler LocationImported;

        Waypoint waypoint;

        public Waypoint Waypoint
        {
            get
            {
                return waypoint;
            }

            set
            {
                waypoint = value;
                UpdateValues();
            }
        }

        string dateTimeFormat;

        [DefaultValue("dd.MM.yyyy (ddd) HH:mm")]
        public string DateTimeFormat
        {
            get { return dateTimeFormat; }

            set
            {
                dateTimeFormat = value;
                dateTimePickerDate.CustomFormat = value;
            }
        }

        string coordinateFormat;

        [DefaultValue("dms")]
        public string CoordinateFormat
        {
            get { return coordinateFormat; }

            set
            {
                coordinateFormat = value;

                maskedTextBoxLatitude.Mask = Coordinate.GetMask(coordinateFormat);
                if (maskedTextBoxLatitude.Text.IsAcceptable())
                {
                    //Coordinate latitude = new Coordinate(maskedTextBoxLatitude.Text, false, labelLat.Text == Mayfly.Geographics.Formats.dirS, coordinateFormat);
                    maskedTextBoxLatitude.Text = Waypoint.Latitude.ToMaskedText(coordinateFormat);
                }

                maskedTextBoxLongitude.Mask = Coordinate.GetMask(coordinateFormat);
                if (maskedTextBoxLongitude.Text.IsAcceptable())
                {
                    //Coordinate longitude = new Coordinate(maskedTextBoxLongitude.Text, true, labelLng.Text == Mayfly.Geographics.Formats.dirW, coordinateFormat);
                    maskedTextBoxLongitude.Text = Waypoint.Longitude.ToMaskedText(coordinateFormat);
                }
            }
        }

        public bool ReadOnly
        {
            get { return maskedTextBoxLatitude.ReadOnly; }
            set { UI.SetControlClickability(!value, this.Controls); }
        }



        public WaypointControl()
        {
            InitializeComponent();

            dateTimePickerDate.Value = DateTime.Now.AddSeconds(-DateTime.Now.Second);

            Waypoint = new Waypoint(0, 0);
            DateTimeFormat = "dd.MM.yyyy (ddd) HH:mm";
            CoordinateFormat = UserSettings.FormatCoordinate;
        }



        public void UpdateValues()
        {
            if (Waypoint == null)
            {
                Clear();
            }
            else
            {
                if (Waypoint.IsTimeMarkNull)
                {
                    dateTimePickerDate.Value = DateTime.Now;
                }
                else
                {
                    SetDateTime(Waypoint.TimeMark);
                }

                if (Waypoint.IsEmpty)
                {
                    maskedTextBoxLatitude.Text = string.Empty;
                    maskedTextBoxLongitude.Text = string.Empty;
                    textBoxAltitude.Text = string.Empty;
                }
                else
                {
                    if (Waypoint.IsLatitudeNull)
                    {
                        maskedTextBoxLatitude.Text = string.Empty;
                    }
                    else
                    {
                        maskedTextBoxLatitude.Text = Waypoint.Latitude.ToMaskedText(CoordinateFormat);
                        labelLat.Text = Waypoint.Latitude.Cardinal;
                    }

                    if (Waypoint.IsLongitudeNull)
                    {
                        maskedTextBoxLongitude.Text = string.Empty;
                    }
                    else
                    {
                        maskedTextBoxLongitude.Text = Waypoint.Longitude.ToMaskedText(CoordinateFormat);
                        labelLng.Text = Waypoint.Longitude.Cardinal;
                    }

                    if (Waypoint.IsAltitudeNull)
                    {
                        textBoxAltitude.Text = string.Empty;
                    }
                    else
                    {
                        textBoxAltitude.Text = Waypoint.Altitude.ToString();
                    }
                }
            }
        }

        public void SetDateTime(DateTime dt)
        {
            dateTimePickerDate.Value = dt;
        }

        public void Save()
        {
            if (Waypoint == null) Waypoint = new Waypoint(0, 0);

            if (maskedTextBoxLatitude.Text.IsAcceptable()) { Waypoint.Latitude = new Coordinate(maskedTextBoxLatitude.Text, false, CoordinateFormat, labelLat.Text == "S"); }
            else { Waypoint.Latitude.Degrees = 0; }

            if (maskedTextBoxLongitude.Text.IsAcceptable()) { Waypoint.Longitude = new Coordinate(maskedTextBoxLongitude.Text, true, CoordinateFormat, labelLng.Text == "W"); }
            else { Waypoint.Longitude.Degrees = 0; }

            if (textBoxAltitude.Text.IsDoubleConvertible()) { Waypoint.Altitude = double.Parse(textBoxAltitude.Text); }
            else { Waypoint.Altitude = double.NaN; }

            Waypoint.TimeMark = dateTimePickerDate.Value;
        }

        public void Clear()
        {
            dateTimePickerDate.Value = DateTime.Today;
            maskedTextBoxLatitude.Text = string.Empty;
            maskedTextBoxLongitude.Text = string.Empty;
            textBoxAltitude.Text = string.Empty;
        }

        public void SelectGPS(string[] filenames)
        {
            locationData_Selected(filenames);
        }

        public void GetLocation()
        {
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

            if (watcher.TryStart(false, TimeSpan.FromMilliseconds(5000)))
            {
                var coord = watcher.Position.Location;

                if (!coord.IsUnknown)
                {
                    Log.Write(coord.ToString());
                    Waypoint = new Waypoint(coord.Latitude, coord.Longitude, DateTime.Now) { Altitude = coord.Altitude };
                }
            }
        }



        private void value_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                Save();
                if (Changed != null) Changed.Invoke(sender, e);
            }
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }



        private void labelDirection_Click(object sender, EventArgs e)
        {
            (sender as Label).Text = Coordinate.ReverseCardinal((sender as Label).Text);

            value_Changed(sender, e);
        }

        private void locationData_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (IO.MaskedNames((string[])e.Data.GetData(DataFormats.FileDrop),
                    IO.InterfaceLocation.OpenExtensions).Length > 0)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                if (Waypoint.Parse((string)e.Data.GetData(DataFormats.StringFormat)) != null)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void locationData_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = IO.MaskedNames((string[])e.Data.GetData(DataFormats.FileDrop),
                    IO.InterfaceLocation.OpenExtensions);
                locationData_Selected(fileNames);
            }
            else if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                Waypoint = Waypoint.Parse((string)e.Data.GetData(DataFormats.StringFormat));
            }
        }

        private void locationData_Selected(string[] filenames)
        {
            if (LocationImported != null)
            {
                LocationImported.Invoke(this, new LocationDataEventArgs(filenames));
            }
            else
            {
                ListWaypoints waypoints = new ListWaypoints(filenames);
                waypoints.SetFriendlyDesktopLocation(this, Point.Empty);
                waypoints.WaypointSelected += new LocationEventHandler(waypoints_WaypointSelected);
                waypoints.Show(this);
            }
        }

        private void waypoints_WaypointSelected(object sender, LocationEventArgs e)
        {
            Waypoint = (Waypoint)e.LocationObject;
        }

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerDate.Value = dateTimePickerDate.Value.AddSeconds(
                -dateTimePickerDate.Value.Second);
            value_Changed(sender, e);
        }

        private void contextItemOpen_Click(object sender, EventArgs e)
        {
            if (IO.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                SelectGPS(Mayfly.IO.InterfaceLocation.OpenDialog.FileNames);
            }
        }

        private void contextItemGet_Click(object sender, EventArgs e)
        {
            GetLocation();
        }

        private void degreesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoordinateFormat = "d";
            UserSettings.FormatCoordinate = CoordinateFormat;
        }

        private void degreesAndMinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoordinateFormat = "dm";
            UserSettings.FormatCoordinate = CoordinateFormat;
        }

        private void degreesMinutesAndSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoordinateFormat = "dms";
            UserSettings.FormatCoordinate = CoordinateFormat;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // Need to loose focus
            if (maskedTextBoxLatitude.Focused || maskedTextBoxLongitude.Focused) textBoxAltitude.Focus();
        }
    }

    public class LocationDataEventArgs : EventArgs
    {
        public string[] Filenames { get; set; }

        public LocationDataEventArgs(string[] filenames)
        {
            Filenames = filenames;
        }
    }

    public delegate void LocationDataEventHandler(object sender, LocationDataEventArgs e);
}
