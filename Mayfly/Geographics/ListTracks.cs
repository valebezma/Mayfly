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

namespace Mayfly.Geographics
{
    public partial class ListTracks : Form
    {
        public event LocationEventHandler TrackSelected;

        public Track[] Values { set; get; }

        public Track Value { set; get; }

        public ListTracks()
        {
            InitializeComponent();
            buttonOK.Visible = false;
        }

        public ListTracks(string fileName) : this(new string[] { fileName })
        { }

        public ListTracks(string[] fileNames) : this(Service.GetTracks(fileNames))
        { }

        public ListTracks(Track[] tracks)
        {
            InitializeComponent();

            foreach (Track track in tracks)
            {
                PlaceTrack(track);
            }

            GridWpt_SelectionChanged(GridWpt, new EventArgs());
        }

        private void PlaceTrack(Track track)
        {
            int rowindex = GridWpt.Rows.Add();

            GridWpt[ColumnTrack.Index, rowindex].Value = track;

            if (track.IsNameNull) GridWpt[ColumnName.Index, rowindex].Value = Mayfly.Resources.Interface.NoTitle;
            else GridWpt[ColumnName.Index, rowindex].Value = track.Name;

            if (!track.IsDescriptionNull) GridWpt[ColumnName.Index, rowindex].ToolTipText = track.Description;

            GridWpt[ColumnLength.Index, rowindex].Value = track.Length;
            GridWpt[ColumnDuration.Index, rowindex].Value = track.Duration;
        }

        private void GridWpt_SelectionChanged(object sender, EventArgs e)
        {
            List<Track> result = new List<Geographics.Track>();

            foreach (DataGridViewRow gridRow in GridWpt.SelectedRows)
            {
                result.Add((Track)gridRow.Cells[ColumnTrack.Index].Value);
            }

            Values = result.ToArray();

            if (Values.Length > 0)
            {
                Value = Values[0];
                buttonOK.Enabled = true;
            }
            else
            {
                Value = null;
                buttonOK.Enabled = false;
            }

            //try
            //{
            //    Value = (Track)GridWpt.CurrentRow.Cells[ColumnTrack.Index].Value;
            //    buttonOK.Enabled = true;
            //}
            //catch
            //{
            //    Value = null;
            //    buttonOK.Enabled = false;
            //}
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Track track in Service.GetTracks(FileSystem.InterfaceLocation.OpenDialog.FileNames))
                {
                    PlaceTrack(track);
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
            TrackSelected.Invoke(sender, new LocationEventArgs(Values));
            Close();
        }
    }
}
