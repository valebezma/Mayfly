using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using Mayfly.Extensions;

namespace Mayfly.Geographics
{
    public partial class ListWaypoints : Form
    {
        public event LocationEventHandler WaypointSelected;

        private event EventHandler settingsRequired;

        public event EventHandler SettingsRequired
        {
            add
            {
                settingsRequired += value;
                buttonSettings.Visible = true;
            }

            remove
            {
                settingsRequired -= value;
                buttonSettings.Visible = false;
            }
        }

        public Waypoint Value { set; get; }

        public Waypoint[] SelectedWaypoints { set; get; }

        public int Count { get { return GridWpt.RowCount; } }



        public ListWaypoints()
        {
            InitializeComponent();

            buttonOK.Visible = false;
        }

        public ListWaypoints(string fileName)
            : this(new string[] { fileName })
        { }

        public ListWaypoints(string[] fileNames)
            : this(Service.GetWaypoints(fileNames))
        { }

        public ListWaypoints(Waypoint[] points)
        {
            InitializeComponent();

            foreach (Waypoint waypoint in points)
            {
                PlaceWaypoint(waypoint);
            }

            //GridWpt_SelectionChanged(GridWpt, new EventArgs());
        }



        private void PlaceWaypoint(Waypoint wayPoint)
        {
            int rowindex = GridWpt.Rows.Add();

            GridWpt[ColumnWPT.Index, rowindex].Value = wayPoint;

            if (wayPoint.IsNameNull) GridWpt[ColumnName.Index, rowindex].Value = Mayfly.Resources.Interface.NoTitle;
            else GridWpt[ColumnName.Index, rowindex].Value = wayPoint.Name;

            if (!wayPoint.IsDescriptionNull) GridWpt[ColumnName.Index, rowindex].ToolTipText = wayPoint.Description;

            if (!wayPoint.IsLatitudeNull)
            {
                GridWpt[ColumnLatT.Index, rowindex].Value = wayPoint.Latitude;
            }

            if (!wayPoint.IsLongitudeNull)
            {
                GridWpt[ColumnLngT.Index, rowindex].Value = wayPoint.Longitude;
            }

            if (!wayPoint.IsAltitudeNull) GridWpt[ColumnAlt.Index, rowindex].Value = wayPoint.Altitude;
            if (!wayPoint.IsTimeMarkNull) GridWpt[ColumnTime.Index, rowindex].Value = wayPoint.TimeMark;

            //GridWpt.CurrentCell = GridWpt[ColumnWPT.Index, rowindex];

            Value = wayPoint;
        }

        private void GridWpt_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Value = (Waypoint)GridWpt.CurrentRow.Cells[ColumnWPT.Index].Value;
                List<Waypoint> wpts = new List<Waypoint>();
                foreach (DataGridViewRow gridRow in GridWpt.SelectedRows)
                {
                    if (gridRow.IsNewRow) continue;
                    wpts.Add((Waypoint)gridRow.Cells[ColumnWPT.Index].Value);
                }
                SelectedWaypoints = wpts.ToArray();
                buttonOK.Enabled = true;
            }
            catch
            {
                Value = null;
                SelectedWaypoints = null;
                buttonOK.Enabled = false;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Waypoint WPT in Service.GetWaypoints(FileSystem.InterfaceLocation.OpenDialog.FileNames))
                {
                    PlaceWaypoint(WPT);
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
            WaypointSelected.Invoke(this, new LocationEventArgs(Value));
            Close();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (settingsRequired != null)
                settingsRequired.Invoke(sender, e);
            //Settings settings = new Settings();

            //if (settings.ShowDialog() == DialogResult.OK)
            //{
            //    GridWpt.Update();
            //    GridWpt.Refresh();
            //}
        }

        private void contextWaypoints_Opening(object sender, CancelEventArgs e)
        {
            contextItemGetTrack.Enabled = contextItemGetPoly.Enabled =
                GridWpt.SelectedRows.Count > 1;
        }

        private void contextItemGetTrack_Click(object sender, EventArgs e)
        {
            Track track = new Track(this.SelectedWaypoints.Merge("→", "Name"), this.SelectedWaypoints);
            ListTracks tracks = new ListTracks(new Track[] { track });
            tracks.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            tracks.TrackSelected += combined_Selected;
            tracks.Show(this);
        }

        private void contextItemGetPoly_Click(object sender, EventArgs e)
        {
            Polygon poly = new Polygon(this.SelectedWaypoints.Merge("→", "Name"), this.SelectedWaypoints);
            ListPolygons polygons = new ListPolygons(new Polygon[] { poly });
            polygons.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            polygons.PolygonSelected += combined_Selected;
            polygons.Show(this);
        }

        void combined_Selected(object sender, LocationEventArgs e)
        {
            WaypointSelected.Invoke(sender, e);
            Close();            
        }
    }
}
