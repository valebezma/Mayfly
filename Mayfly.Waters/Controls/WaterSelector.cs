using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using Mayfly.Extensions;

namespace Mayfly.Waters.Controls
{
    public partial class WaterSelector : TextBox
    {
        private WatersKey index;

        [Localizable(false)]
        public WatersKey Index 
        {
            get 
            {
                return index;
            }

            set 
            {
                if (value == null)
                {
                    index = new WatersKey();
                }
                else
                {
                    index = value;
                }
            }
        }

        private WatersKey.WaterRow waterRow;

        public WatersKey.WaterRow WaterObject
        {
            get
            {
                return waterRow;
            }

            set
            {
                if (value == null)
                {
                    this.Text = String.Empty;
                }
                else
                {
                    this.Text = value.FullName;
                }

                waterRow = value;

                if (WaterSelected != null)
                {
                    WaterSelected.Invoke(this, new WaterEventArgs(value));
                }
            }
        }

        private bool AllowSuggest 
        {
            get;
            set;
        }

        public bool IsWaterSelected
        {
            get
            {
                return WaterObject != null;
            }
        }

        public event WaterEventHandler WaterSelected;
        


        public WaterSelector()
        {
            InitializeComponent();

            AllowSuggest = true;
        }



        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Down:
                    if (listWaters.Visible && listWaters.Items.Count > 0)
                    {
                        listWaters.Focus();
                        listWaters.Items[0].Selected = true;
                    }
                    break;
                case Keys.Up:
                    if (listWaters.Visible && listWaters.Items.Count > 0)
                    {
                        listWaters.Focus();
                        listWaters.Items[listWaters.Items.Count - 1].Selected = true;
                        listWaters.Items[listWaters.Items.Count - 1].Focused = true;
                        listWaters.Items[listWaters.Items.Count - 1].EnsureVisible();
                    }
                    break;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (this.FindForm().ActiveControl == listWaters)
            {
                //
            }
            else
            {
                listWaters.Visible = false;
            }

            if (Index == null) return;

            if (WaterObject == null)
            {
                WatersKey.WaterRow[] probWaters = Index.GetWatersNamed(this.Text);
                if (probWaters.Length == 1)
                {
                    AllowSuggest = false;
                    WaterObject = probWaters[0];
                    AllowSuggest = true;
                }
                else if (probWaters.Length == 0)
                {
                    WaterObject = GetWaterFromInput();
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            waterRow = null;

            if (this.ContainsFocus && AllowSuggest && this.Text.Length > 1)
            {
                AllowSuggest = false;
                listLoader.RunWorkerAsync(this.Text);
            }
        }



        private void SetListPosition()
        {
            Point pointToClient = this.FindForm().PointToClient(this.Parent.PointToScreen(this.Location));
            listWaters.Location = new Point(pointToClient.X, pointToClient.Y + this.Height + 1);
        }

        public void CreateList()
        {
            this.FindForm().Controls.Add(listWaters);
            listWaters.TabIndex = this.TabIndex;
            listWaters.BringToFront();
            listWaters.Shine();
        }

        private WatersKey.WaterRow GetWaterFromInput()
        {
            return GetWaterFromInput(this.Text);
        }

        private WatersKey.WaterRow GetWaterFromInput(string text)
        {
            if (text == string.Empty) return null;

            WatersKey.WaterRow result = Index.Water.NewWaterRow();

            string[] words = text.Split(' ');

            List<string> worked = new List<string>();

            foreach (string word in words)
            {
                if (Resources.Interface.StreamMarkers.ToUpper().Contains(word.ToUpper()))
                {
                    result.Type = 1;
                    worked.Add(word);
                    break;
                }

                if (Resources.Interface.LakeMarkers.ToUpper().Contains(word.ToUpper()))
                {
                    result.Type = 2;
                    worked.Add(word);
                    break;
                }

                if (Resources.Interface.TankMarkers.ToUpper().Contains(word.ToUpper()))
                {
                    result.Type = 3;
                    worked.Add(word);
                    break;
                }
            }

            string waterName = string.Empty;

            foreach (string word in words)
            {
                if (worked.Contains(word)) continue;

                waterName += " " + word;
            }

            result.Water = waterName.TrimStart();

            return result;
        }

        public ListViewItem[] WatersItems(string searchTerm)
        {
            List<ListViewItem> result = new List<ListViewItem>();

            foreach (WatersKey.WaterRow waterRow in Index.GetWatersNameContaining(searchTerm))
            {
                ListViewItem item = new ListViewItem();
                item.Text = waterRow.FullName;
                item.Tag = waterRow;
                item.ToolTipText = waterRow.Description;
                result.Add(item);
            }

            return result.ToArray();
        }



        private void listLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = WatersItems((string)e.Argument);
        }

        private void listLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListViewItem[] result = e.Result as ListViewItem[];
            if (result.Length > 0)
            {
                foreach (ListViewItem item in listWaters.Items)
                {
                    if (!result.Contains(item))
                    {
                        listWaters.Items.Remove(item);
                    }
                }

                foreach (ListViewItem item in result)
                {
                    if (!listWaters.Items.Contains(item))
                    {
                        listWaters.Items.Add(item);
                    }
                }

                if (!listWaters.Visible)
                {
                    SetListPosition();
                    listWaters.Visible = true;
                }

                int AvailableHeight = this.FindForm().ClientSize.Height - this.Bottom - 51;
                listWaters.Height = Math.Min(AvailableHeight, 2 + listWaters.Items.Count * 17);

                listWaters.Width = this.Width;
                if (result.Length > 4)
                {
                    listWaters.Columns[0].Width = listWaters.Width - 2 -
                         SystemInformation.VerticalScrollBarWidth;
                }
                else
                {
                    listWaters.Columns[0].Width = listWaters.Width - 2;
                }

            }
            else
            {
                listViewWaters_Leave(listWaters, new EventArgs());
            }
            AllowSuggest = true;
        }



        private void listViewWaters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                listWaters.Visible = false;
            }
        }

        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            if (listWaters.SelectedItems.Count > 0)
            {
                WaterObject = listWaters.SelectedItems[0].Tag as WatersKey.WaterRow;
                listWaters.Visible = false;
            }
        }

        private void listViewWaters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listWaters.SelectedItems.Count > 0)
            {
                if (listWaters.SelectedItems[0].Tag is WatersKey.WaterRow)
                {
                    WatersKey.WaterRow waterRow = listWaters.SelectedItems[0].Tag as WatersKey.WaterRow;
                    AllowSuggest = false;
                    WaterObject = waterRow;
                    AllowSuggest = true;
                }
            }
        }

        private void listViewWaters_VisibleChanged(object sender, EventArgs e)
        {
            if (!listWaters.Visible)
            {
                AllowSuggest = (Index != null);
                if (this.FindForm().ActiveControl != this) 
                {
                    //this.Focus();
                }
            }
        }

        private void listViewWaters_Leave(object sender, EventArgs e)
        {
            listWaters.Visible = false;
        }
    }

    public class WaterEventArgs : EventArgs
    {
        public WatersKey.WaterRow WaterRow;

        public WaterEventArgs(WatersKey.WaterRow waterRow)
        {
            WaterRow = waterRow;
        }
    }

    public delegate void WaterEventHandler(object sender, WaterEventArgs e);
}
