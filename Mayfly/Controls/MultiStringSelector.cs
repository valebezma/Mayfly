using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.ComponentModel;
using System.Drawing;

namespace Mayfly.Controls
{
    public partial class MultiStringSelector : TextBox
    {
        [Category("Behaviour"), DefaultValue(";")]
        public string Separator { get; set; }

        [Category("Behaviour"), Browsable(false)]
        public string[] Strings
        {
            get
            {
                return this.Text.Split(Separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }

            set
            {
                this.Text = value.Merge(this.Separator + " ");
            }
        }

        [Category("Behaviour"), Browsable(false)]
        public string LastEntry
        {
            get
            {
                return this.Text.Contains(this.Separator) ? this.Text.Substring(this.Text.LastIndexOf(this.Separator) + 1).Trim() : this.Text.Trim();
            }
        }

        private int currentEntryStart
        {
            get
            {
                if (!this.Text.Contains(this.Separator)) return 0;

                int indexOfSepBeforeSelection = this.Text.Substring(0, this.SelectionStart).LastIndexOf(this.Separator) + 1;
                return indexOfSepBeforeSelection == -1 ? 0 : indexOfSepBeforeSelection;
            }
        }

        private int currentEntryEnd {
            get 
            {
                if (!this.Text.Contains(this.Separator)) return this.Text.TrimEnd().Length;

                int selectionEnd = this.SelectionStart + this.SelectionLength;
                int indexOfSepAfterSelection = selectionEnd + 1 + this.Text.Substring(selectionEnd).IndexOf(this.Separator);
                return indexOfSepAfterSelection == -1 ? (this.Text.TrimEnd().Length - 1) : indexOfSepAfterSelection; 
            }
        }

        [Category("Behaviour"), Browsable(false)]
        public string CurrentEntry
        {
            get
            {
                return this.Text.Substring(currentEntryStart, currentEntryEnd - currentEntryStart).Trim();
            }
        }

        [Category("Behaviour"), Browsable(false)]
        public string[] Options
        {
            get;
            set;
        }

        //[Category("Behaviour"), DefaultValue(true)]
        //public bool CheckDuplicates
        //{
        //    get;
        //    set;
        //}

        public event EventHandler EntrySelected;

        private BackgroundWorker listLoader;

        private ListView listOptions;

        private ColumnHeader columnHeader1;

        private string valueBeforeEditing = string.Empty;



        public MultiStringSelector() : base()
        {
            this.Separator = Constants.ElementSeparator;

            listLoader = new BackgroundWorker();
            listLoader.WorkerSupportsCancellation = true;
            listLoader.DoWork += new DoWorkEventHandler(listLoader_DoWork);
            listLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(listLoader_RunWorkerCompleted);
            
            
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            
            // 
            // listSpc
            // 
            listOptions = new ListView();
            this.listOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1});
            this.listOptions.FullRowSelect = true;
            this.listOptions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listOptions.HideSelection = false;
            this.listOptions.MultiSelect = false;
            this.listOptions.Name = "listSpc";
            this.listOptions.ShowItemToolTips = true;
            this.listOptions.UseCompatibleStateImageBehavior = false;
            this.listOptions.View = System.Windows.Forms.View.Details;

            this.listOptions.ItemActivate += new System.EventHandler(this.listOptions_ItemActivate);
            this.listOptions.SelectedIndexChanged += new System.EventHandler(this.listOptions_SelectedIndexChanged);
            this.listOptions.VisibleChanged += new System.EventHandler(this.listOptions_VisibleChanged);
            this.listOptions.Enter += new System.EventHandler(this.listOptions_Enter);
            this.listOptions.Leave += new System.EventHandler(this.listOptions_Leave);
            this.listOptions.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.listOptions_PreviewKeyDown);
            
        }



        public ListViewItem[] SuggestedItems(string pattern)
        {
            List<ListViewItem> result = new List<ListViewItem>();

            foreach (string value in this.Options)
            {
                if (!value.ToUpper().Contains(pattern.ToUpper())) continue;

                ListViewItem item = new ListViewItem();
                item.Text = value;
                result.Add(item);
            }

            result.Sort(new SearchResultSorter(pattern));

            return result.ToArray();
        }

        public void CreateList()
        {
            if (listOptions.Parent == null)
            {
                this.Parent.Controls.Add(listOptions);
                listOptions.Visible = false;
                listOptions.BringToFront();
                listOptions.Shine();
            }
        }

        private void ResetListPosition()
        {
            if (this.Parent == null) return;

            Point p = this.GetPositionInForm();
            p.Offset(0, this.Height + 3);
            listOptions.Location = p;
            //listSpc.Visible = new Rectangle(this.FindForm().PointToClient(
            //    this.Parent.PointToScreen(this.Location)), this.Size).Contains(p);
        }

        private void RunSelected(string entered)
        {
            if (entered != valueBeforeEditing && EntrySelected != null)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    EntrySelected.Invoke(this, new EventArgs());
                }));
            }
        }



        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.Down:
                    if (listOptions.Visible && listOptions.Items.Count > 0)
                    {
                        listOptions.Focus();
                        listOptions.Items[0].Selected = true;
                        //listSpc.Items[0].Focused = true;
                        //listSpc.Items[0].EnsureVisible();
                    }
                    break;
                case Keys.Up:
                    if (listOptions.Visible && listOptions.Items.Count > 0)
                    {
                        listOptions.Focus();
                        listOptions.Items[listOptions.Items.Count - 1].Selected = true;
                        listOptions.Items[listOptions.Items.Count - 1].Focused = true;
                        listOptions.Items[listOptions.Items.Count - 1].EnsureVisible();
                    }
                    break;
                default:
                    base.OnPreviewKeyDown(e);
                    break;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            
            if (this.FindForm().ActiveControl != listOptions)
            {
                listOptions.Visible = false;

                if ((string)this.CurrentEntry != valueBeforeEditing)
                {
                    RunSelected(this.Text);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!this.Focused) return;

            if (!listLoader.IsBusy)
            {
                if (CurrentEntry.IsAcceptable())
                {
                    listLoader.RunWorkerAsync(CurrentEntry);
                }
                else
                {
                    listOptions_Leave(listOptions, new EventArgs());
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResetListPosition();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            CreateList();
        }



        #region List logics

        private void listLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SuggestedItems((string)e.Argument);
        }

        private void listLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                listOptions_Leave(listOptions, new EventArgs());
                return;
            }

            ListViewItem[] result = e.Result as ListViewItem[];

            if (result.Length > 0)
            {
                foreach (ListViewItem item in listOptions.Items)
                {
                    if (!result.Contains(item))
                    {
                        listOptions.Items.Remove(item);
                    }
                }

                foreach (ListViewItem item in result)
                {
                    if (!listOptions.Items.Contains(item))
                    {
                        listOptions.Items.Add(item);
                    }
                }

                if (!listOptions.Visible)
                {
                    ResetListPosition();
                    listOptions.Visible = true;
                    listOptions.BringToFront();
                }

                listOptions.Width = this.Width;
                if (result.Length > 4)
                {
                    listOptions.Columns[0].Width = listOptions.Width - 2 -
                         SystemInformation.VerticalScrollBarWidth;
                }
                else
                {
                    listOptions.Columns[0].Width = listOptions.Width - 2;
                }
            }
            else
            {
                listOptions_Leave(listOptions, e);
            }
            //AllowSuggest = true;
        }

        private void listOptions_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                listOptions.Visible = false;
            }

            if (e.KeyCode == Keys.Escape)
            {
                listOptions.Visible = false;
                this.Text = valueBeforeEditing;
            }
        }

        private void listOptions_ItemActivate(object sender, EventArgs e)
        {
            if (listOptions.SelectedItems.Count == 0) return;

            string selectedValue = listOptions.SelectedItems[0].Text;

            if (/*this.CheckDuplicates &&*/ Strings.Contains(selectedValue))
            {
                // Select entered value in TextBox
                this.Select(this.Text.IndexOf(selectedValue), selectedValue.Length);
                return;
            }
            else
            {
                // Insert new string
                string before = this.Text.Substring(0, currentEntryStart);
                this.Text = before +
                    /*(string.IsNullOrEmpty(before) ? string.Empty : Separator) + */
                    selectedValue + 
                    this.Text.Substring(currentEntryEnd, this.Text.Length - currentEntryEnd) + 
                    this.Separator +
                    " ";
                this.Select(this.Text.Length - 1, 0);

                listOptions_Leave(listOptions, e);
            }
        }

        private void listOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listSpc.SelectedItems.Count > 0)
            //{
            //    if (listSpc.SelectedItems[0].Tag is SpeciesKey.SpeciesRow)
            //    {
            //        SpeciesKey.SpeciesRow speciesRow = listSpc.SelectedItems[0].Tag as SpeciesKey.SpeciesRow;

            //        AllowSuggest = false;
            //        if (InsertFullNames)
            //        {
            //            Grid.CurrentCell.Value = speciesRow.GetFullName();
            //        }
            //        else
            //        {
            //            Grid.CurrentCell.Value = speciesRow.Species;
            //        }

            //        AllowSuggest = true;
            //    }
            //    else
            //    {
            //        Grid.CurrentCell.Value = listSpc.SelectedItems[0].Text; 
            //    }
            //}
        }

        private void listOptions_VisibleChanged(object sender, EventArgs e)
        {
            if (listOptions.Visible)
            {
            }
            else
            {
                //AllowSuggest = true;
                //if (this.FindForm().ActiveControl == this) { }
                //else
                //{
                //    this.Focus();
                //}

                this.Focus();
            }
        }

        private void listOptions_Enter(object sender, EventArgs e)
        {
            //if (Grid.CurrentCell.Value == null)
            //{
            //    handEntered = string.Empty;
            //}
            //else
            //{
            //    handEntered = Grid.CurrentCell.Value.ToString();
            //}

            valueBeforeEditing = this.Text;
        }

        private void listOptions_Leave(object sender, EventArgs e)
        {
            listOptions.Visible = false;
        }

        #endregion
    }
}
