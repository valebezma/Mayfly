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
    public partial class ListPolygons : Form
    {
        public event LocationEventHandler PolygonSelected;

        public Polygon[] Values { set; get; }

        //public Polygon Value { set; get; }

        public ListPolygons()
        {
            InitializeComponent();

            buttonOK.Visible = false;
        }

        public ListPolygons(string fileName)
            : this(new string[] { fileName })
        { }

        public ListPolygons(string[] fileNames) 
            : this(Service.GetPolygons(fileNames))
        { }

        public ListPolygons(Polygon[] polygons)
        {
            InitializeComponent();

            foreach (Polygon polygon in polygons)
            {
                PlacePolygon(polygon);
            }

            GridWpt_SelectionChanged(GridWpt, new EventArgs());
        }

        private void PlacePolygon(Polygon polygon)
        {
            int rowindex = GridWpt.Rows.Add();

            GridWpt[ColumnPolygon.Index, rowindex].Value = polygon;

            if (polygon.IsNameNull) GridWpt[ColumnName.Index, rowindex].Value = Mayfly.Resources.Interface.NoTitle;
            else GridWpt[ColumnName.Index, rowindex].Value = polygon.Name;

            if (!polygon.IsDescriptionNull) GridWpt[ColumnName.Index, rowindex].ToolTipText = polygon.Description;

            GridWpt[ColumnPerimeter.Index, rowindex].Value = polygon.Perimeter;
            GridWpt[ColumnArea.Index, rowindex].Value = polygon.Area / 10000d;
            GridWpt[ColumnWidth.Index, rowindex].Value = polygon.Width;
        }

        private void GridWpt_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                List<Polygon> result = new List<Polygon>();
                foreach (DataGridViewRow gridRow in GridWpt.SelectedRows)
                {
                    result.Add((Polygon)gridRow.Cells[ColumnPolygon.Index].Value);
                }
                Values = result.ToArray();
                //Value = (Polygon)GridWpt.CurrentRow.Cells[ColumnPolygon.Index].Value;
                buttonOK.Enabled = true;
            }
            catch
            {
                Values = null;
                buttonOK.Enabled = false;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (IO.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Polygon polygon in Service.GetPolygons(IO.InterfaceLocation.OpenDialog.FileNames))
                {
                    PlacePolygon(polygon);
                }
                GridWpt_SelectionChanged(GridWpt, new EventArgs());
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
            PolygonSelected.Invoke(sender, new LocationEventArgs(Values));
            Close();
        }
    }
}
