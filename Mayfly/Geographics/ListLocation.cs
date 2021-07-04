using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Geographics
{
    public delegate void LocationEventHandler(object sender, LocationEventArgs e);

    public partial class ListLocation : Form
    {
        public event LocationEventHandler LocationSelected;

        public Waypoint[] Waypoints;

        public Track[] Tracks;

        public Polygon[] Polygons;

        public ListLocation(string[] filenames)
        {
            InitializeComponent();
            listViewLocationType.Shine();

            listViewLocationType.Items[0].SubItems[1].Text = Mayfly.Constants.Null;
            listViewLocationType.Items[1].SubItems[1].Text = Mayfly.Constants.Null;
            listViewLocationType.Items[2].SubItems[1].Text = Mayfly.Constants.Null;

            Cursor = Cursors.AppStarting;
            backgroundLocationLoader.RunWorkerAsync(filenames);
        }

        private void listViewLocationType_ItemActivate(object sender, EventArgs e)
        {
            if (!buttonSelect.Enabled) return;

            Hide();

            switch (listViewLocationType.SelectedIndices[0])
            {
                case 0:
                    ListWaypoints waypoints = new ListWaypoints(Waypoints);
                    waypoints.SetFriendlyDesktopLocation(this, FormLocation.Centered);
                    if (LocationSelected != null) waypoints.WaypointSelected += LocationSelected;
                    waypoints.ShowDialog(this);
                    break;
                case 1:
                    ListTracks tracks = new ListTracks(Tracks);
                    tracks.SetFriendlyDesktopLocation(this, FormLocation.Centered);
                    if (LocationSelected != null) tracks.TrackSelected += LocationSelected;
                    tracks.ShowDialog(this);
                    break;
                case 2:
                    ListPolygons polygons = new ListPolygons(Polygons);
                    polygons.SetFriendlyDesktopLocation(this, FormLocation.Centered);
                    if (LocationSelected != null) polygons.PolygonSelected += LocationSelected;
                    polygons.ShowDialog(this);
                    break;
            }
            
            Close();
        }

        private void listViewLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSelect.Enabled = listViewLocationType.SelectedItems.Count > 0 &&
                listViewLocationType.SelectedItems[0].SubItems[1].Text !=  Mayfly.Constants.Null;
        }

        private void backgroundLocationLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;

            Waypoints = Service.GetWaypoints(filenames);
            Tracks = Service.GetTracks(filenames);
            Polygons = Service.GetPolygons(filenames);
        }

        private void backgroundLocationLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewLocationType.Items[0].SubItems[1].Text = Waypoints.Length > 0 ? Waypoints.Length.ToString() : Mayfly.Constants.Null;
            listViewLocationType.Items[1].SubItems[1].Text = Tracks.Length > 0 ? Tracks.Length.ToString() : Mayfly.Constants.Null;
            listViewLocationType.Items[2].SubItems[1].Text = Polygons.Length > 0 ? Polygons.Length.ToString() : Mayfly.Constants.Null;

            Cursor = Cursors.Default;
            listViewLocationType_SelectedIndexChanged(sender, e);
        }

        private void ListLocation_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundLocationLoader.CancelAsync();
        }
    }

    public class LocationEventArgs : EventArgs
    {
        public object[] LocationObjects
        {
            get;
            set;
        }

        public object LocationObject
        {
            get;
            set;
        }

        public LocationEventArgs(Waypoint point)
        {
            this.LocationObject = point;
        }

        public LocationEventArgs(Track track)
        {
            this.LocationObject = track;
        }

        public LocationEventArgs(Polygon polygon)
        {
            this.LocationObject = polygon;
        }

        public LocationEventArgs(Track[] tracks)
        {
            this.LocationObjects = tracks;
            if (tracks.Length > 0) this.LocationObject = tracks[0];
        }

        public LocationEventArgs(Polygon[] polygons)
        {
            this.LocationObjects = polygons;
            if (polygons.Length > 0) this.LocationObject = polygons[0];
        }
    }
}
