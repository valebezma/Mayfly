using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.TaskDialogs;
using System.IO;
using Mayfly.Software;

namespace Mayfly.Library
{
    public partial class Catalogue : Form
    {
        private string fileName;

        public string FileName
        {
            set
            {
                this.ResetText(value == null ? FileSystem.GetNewFileCaption(UserSettings.Interface.Extension) : value, EntryAssemblyInfo.Title);
                fileName = value;
            }

            get
            {
                return fileName;
            }
        }

        public bool IsChanged { get; set; }

        private bool close = false;



        public Catalogue()
        {
            InitializeComponent();

            FileName = null;
            
            listViewBooks.Shine();
            listViewBooks.MakeSortable();
            listViewSeries.Shine();
            listViewSeries.MakeSortable();
            listViewIssues.Shine();
            listViewIssues.MakeSortable();
            listViewResearches.Shine();
            listViewResearches.MakeSortable();

            labelBooks.UpdateStatus(0);
            tabBook.ResetFormatted(0);

            labelSeries.UpdateStatus(0);
            tabSeries.ResetFormatted(0);
            labelIssues.UpdateStatus(0);

            labelResearches.UpdateStatus(0);            
            tabResearches.ResetFormatted(0);

            statusLabelCount.ResetFormatted(0);

            //this.Expand(150);
        }

        public Catalogue(string filename) : this()
        {
            LoadData(filename);
        }


        
        delegate void SeriesListViewItemEventHandler(Library.SeriesRow seriesRow);

        public void UpdateBookItem(Library.BookRow bookRow, ListViewItem li)
        {
            li.SubItems.Clear();
            li.Name = bookRow.ID.ToString();
            li.Text = bookRow.Title;
            li.SubItems.Add(bookRow.When.ToString());
            li.SubItems.Add(bookRow.PublisherRow.Where.ToString());
            li.SubItems.Add(bookRow.PublisherRow.Name.ToString());
            li.SubItems.Add(bookRow.Pages.ToString());
            li.SubItems.Add(bookRow.Quantity.ToString());
        }

        public void UpdateResearchItem(Library.BookRow bookRow, ListViewItem li)
        {
            li.SubItems.Clear();
            li.Name = bookRow.ID.ToString();
            li.Text = bookRow.Title;
            li.SubItems.Add(bookRow.When.ToString());
            li.SubItems.Add(bookRow.AuthorRowByExecutive.Name);
            li.SubItems.Add(bookRow.GetKeywords().Merge(UserSettings.Separator + " "));
            li.SubItems.Add(bookRow.GetAuthors().Merge(UserSettings.Separator + " "));
        }

        public void UpdateSeriesItem(Library.SeriesRow seriesRow, ListViewItem li)
        {
            li.Name = seriesRow.ID.ToString();
            li.Text = seriesRow.Title;
        }

        public void UpdateIssueItem(Library.BookRow bookRow, ListViewItem li)
        {
            li.SubItems.Clear();
            li.Name = bookRow.ID.ToString();
            li.Text = bookRow.When.ToString();
            li.SubItems.Add(bookRow.IsIssueNull() ? string.Empty : bookRow.Issue);
            li.SubItems.Add(bookRow.IsTitleNull() ? string.Empty : bookRow.Title);
        }



        private DialogResult CheckAndSave()
        {
            DialogResult result = DialogResult.None;

            if (IsChanged)
            {
                TaskDialogButton tdbPressed = taskDialogSaveChanges.ShowDialog(this);

                if (tdbPressed == tdbSave)
                {
                    menuItemSave_Click(menuItemSave, new EventArgs());
                    result = DialogResult.OK;
                }
                else if (tdbPressed == tdbDiscard)
                {
                    IsChanged = false;
                    result = DialogResult.No;
                }
                else if (tdbPressed == tdbCancelClose)
                {
                    result = DialogResult.Cancel;
                }
            }

            return result;
        }

        private void Clear()
        {
            FileName = null;

            listViewBooks.Items.Clear();
            listViewSeries.Items.Clear();
            listViewIssues.Items.Clear();

            Data = new Library();
        }

        public void LoadData(string fileName)
        {
            Clear();

            //ResearchArchive ra = new ResearchArchive();
            //ra.ReadXml(fileName);
            //Data = ra.GetLibrary(System.IO.Path.GetDirectoryName(fileName));

            Data = new Library();
            Data.ReadXml(fileName);
            
            LoadData();
            FileName = fileName;

            IsChanged = false;
        }

        private void LoadData()
        {
            foreach (ListView lv in new ListView[] {
                listViewBooks, listViewSeries, listViewIssues, listViewResearches })
            {
                lv.Items.Clear();
                lv.Groups.Clear();
            }

            processDisplay.StartProcessing(Data.Book.Count + Data.Series.Count, Resources.Interface.ProgressOpen);
            backLoader.RunWorkerAsync();
        }

        private void Write(string fileName)
        {
            Data.WriteXml(fileName);
            FileName = fileName;

            menuFile.Enabled = false;

            processDisplay.StartProcessing(Data.Book.Count, Resources.Interface.ProgressFiles0);
            backFileMover.RunWorkerAsync();
        }

        private void UpdateResearchesGroups(int groupMode)
        {
            listViewResearches.Groups.Clear();

            switch (groupMode)
            {
                case 0: // Year
                    foreach (int y in Data.Book.WhenColumn.SelectDistinctInteger())
                    {
                        listViewResearches.CreateGroup(y.ToString());
                    }
                    break;

                case 1: // City
                    foreach (string city in Data.Publisher.WhereColumn.GetStrings(true))
                    {
                        listViewResearches.CreateGroup(city);
                    }
                    break;

                case 2: // Publisher
                    foreach (Library.PublisherRow pubRow in Data.Publisher.Select(null, "Name Asc"))
                    {
                        listViewResearches.CreateGroup(pubRow.Name);
                    }
                    break;

                case 3: // Executive
                    foreach (Library.AuthorRow authRow in Data.Author.Select(null, "Name Asc"))
                    {
                        if (authRow.GetBookRowsByExecutive().Length == 0) continue;
                        listViewResearches.CreateGroup(authRow.Name);
                    }
                    break;

                case 4: // Rack
                    foreach (Library.RackRow rackRow in Data.Rack.Select(null, "Name Asc"))
                    {
                        listViewResearches.CreateGroup(rackRow.Name, string.Format("{0} ({1})", rackRow.Name, rackRow.Location));
                    }
                    break;
            }

            listViewResearches.Shine();
        }

        private void UpdateResearchesItemGroups(int groupMode)
        {
            foreach (ListViewItem li in listViewResearches.Items)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());

                switch (groupMode)
                {
                    case 0:
                        li.Group = bookRow.IsWhenNull() ? null : listViewResearches.GetGroup(bookRow.When.ToString());
                        break;

                    case 1:
                        li.Group = bookRow.IsPubIDNull() ? null : listViewResearches.GetGroup(bookRow.PublisherRow.Where);
                        break;

                    case 2:
                        li.Group = bookRow.IsPubIDNull() ? null : listViewResearches.GetGroup(bookRow.PublisherRow.Name);
                        break;

                    case 3:
                        li.Group = bookRow.IsExeIDNull() ? null : listViewResearches.GetGroup(bookRow.AuthorRowByExecutive.Name);
                        break;

                    case 4:
                        li.Group = bookRow.IsRackIDNull() ? null : listViewResearches.GetGroup(bookRow.RackRow.Name);
                        break;
                }
            }
        }

        private void UpdateBooksGroups(int groupMode)
        {
            listViewBooks.Groups.Clear();

            switch (groupMode)
            {
                case 0: // Year
                    foreach (int y in Data.Book.WhenColumn.SelectDistinctInteger())
                    {
                        listViewBooks.CreateGroup(y.ToString());
                    }
                    break;

                case 1: // City
                    foreach (string city in Data.Publisher.WhereColumn.GetStrings(true))
                    {
                        listViewBooks.CreateGroup(city);
                    }
                    break;

                case 2: // Publisher
                    foreach (Library.PublisherRow pubRow in Data.Publisher.Select(null, "Name Asc"))
                    {
                        listViewBooks.CreateGroup(pubRow.Name);
                    }
                    break;

                case 3: // Shelf
                    int[] nos = Data.Book.ShelfColumn.SelectDistinctInteger();                    
                    foreach (Library.RackRow rackRow in Data.Rack.Select(null, "Name Asc"))
                    {
                        foreach (int no in nos)
                        {
                            listViewBooks.CreateGroup(string.Format("{0}+{1}", rackRow.Name, no), string.Format(Resources.Interface.GroupShelf, rackRow.Name, no));
                        }

                        listViewBooks.CreateGroup(string.Format("{0}+{1}", rackRow.Name, 0), string.Format(Resources.Interface.GroupNoShelf, rackRow.Name));
                    }
                    break;

                case 4: // Rack
                    foreach (Library.RackRow rackRow in Data.Rack.Select(null, "Name Asc"))
                    {
                        listViewBooks.CreateGroup(rackRow.Name, string.Format("{0} ({1})", rackRow.Name, rackRow.Location));
                    }
                    break;

                case 5:
                    foreach (char c in "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЭЮЯABCDEFGHIJKLMNOPQRSTUVWXYZ")
                    {
                        listViewBooks.CreateGroup(string.Format("LET_{0}", c), string.Format("{0}", c));
                    }
                    break;
            }

            listViewBooks.Shine();
        }

        private void UpdateBooksItemGroups(int groupMode)
        {
            foreach (ListViewItem li in listViewBooks.Items)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());

                switch (groupMode)
                {
                    case 0:
                        li.Group = bookRow.IsWhenNull() ? null : listViewBooks.GetGroup(bookRow.When.ToString());
                        break;

                    case 1:
                        li.Group = bookRow.IsPubIDNull() ? null : listViewBooks.GetGroup(bookRow.PublisherRow.Where);
                        break;

                    case 2:
                        li.Group = bookRow.IsPubIDNull() ? null : listViewBooks.GetGroup(bookRow.PublisherRow.Name);
                        break;

                    case 3:
                        li.Group = bookRow.IsRackIDNull() ? null : listViewBooks.GetGroup(string.Format("{0}+{1}", bookRow.RackRow.Name, bookRow.IsShelfNull() ? 0 : bookRow.Shelf));
                        break;

                    case 4:
                        li.Group = bookRow.IsRackIDNull() ? null : listViewBooks.GetGroup(bookRow.RackRow.Name);
                        break;

                    case 5:
                        li.Group = bookRow.IsTitleNull() ? null : listViewBooks.GetGroup(("LET_" + bookRow.Title.GetInitial()).ToUpper());
                        break;
                }
            }
        }

        private void UpdateSeriesGroups(int groupMode)
        {
            listViewSeries.Groups.Clear();

            switch (groupMode)
            {
                //case 0: // City
                //    foreach (string city in Data.Publisher.WhereColumn.GetStrings(true))
                //    {
                //        listViewSeries.CreateGroup(city);
                //    }
                //    break;

                //case 1: // Publisher
                //    foreach (Library.PublisherRow pubRow in Data.Publisher.Select(null, "Name Asc"))
                //    {
                //        listViewSeries.CreateGroup(pubRow.Name);
                //    }
                //    break;

                case 0:
                    foreach (char c in "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЭЮЯABCDEFGHIJKLMNOPQRSTUVWXYZ")
                    {
                        listViewSeries.CreateGroup(string.Format("LET_{0}", c), string.Format("{0}", c));
                    }
                    break;
            }

            listViewSeries.Shine();
        }

        private void UpdateSeriesItemGroups(int groupMode)
        {
            foreach (ListViewItem li in listViewSeries.Items)
            {
                Library.SeriesRow seriesRow = Data.Series.FindByID(li.GetID());

                switch (groupMode)
                {
                    //case 0:
                    //    li.Group = seriesRow.IsPubIDNull() ? null : listViewSeries.GetGroup(seriesRow.PublisherRow.Where);
                    //    break;

                    //case 1:
                    //    li.Group = seriesRow.IsPubIDNull() ? null : listViewSeries.GetGroup(seriesRow.PublisherRow.Name);
                    //    break;

                    case 0:

                        li.Group = listViewSeries.GetGroup(("LET_" + seriesRow.Title.GetInitial()).ToUpper());
                        break;
                }
            }
        }

        private void UpdateIssuesGroups(int groupMode)
        {
            listViewIssues.Groups.Clear();

            switch (groupMode)
            {
                case 0: // Year
                    foreach (int y in Data.Book.WhenColumn.SelectDistinctInteger())
                    {
                        listViewIssues.CreateGroup(y.ToString());
                    }
                    break;

                case 1: // Shelf
                    int[] nos = Data.Book.ShelfColumn.SelectDistinctInteger();                    
                    foreach (Library.RackRow rackRow in Data.Rack.Select(null, "Name Asc"))
                    {
                        foreach (int no in nos)
                        {
                            listViewIssues.CreateGroup(string.Format("{0}+{1}", rackRow.Name, no), string.Format(Resources.Interface.GroupShelf, rackRow.Name, no));
                        }

                        listViewIssues.CreateGroup(string.Format("{0}+{1}", rackRow.Name, 0), string.Format(Resources.Interface.GroupNoShelf, rackRow.Name));
                    }
                    break;

                case 2: // Rack
                    foreach (Library.RackRow rackRow in Data.Rack.Select(null, "Name Asc"))
                    {
                        listViewIssues.CreateGroup(rackRow.Name, string.Format("{0} ({1})", rackRow.Name, rackRow.Location));
                    }
                    break;

                case 3:
                    foreach (char c in "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЫЭЮЯABCDEFGHIJKLMNOPQRSTUVWXYZ")
                    {
                        listViewIssues.CreateGroup(string.Format("LET_{0}", c), string.Format("{0}", c));
                    }
                    break;
            }

            listViewIssues.Shine();
        }

        private void UpdateIssuesItemGroups(int groupMode)
        {
            foreach (ListViewItem li in listViewIssues.Items)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());

                switch (groupMode)
                {
                    case 0:
                        li.Group = bookRow.IsWhenNull() ? null : listViewIssues.GetGroup(bookRow.When.ToString());
                        break;

                    case 1:
                        li.Group = bookRow.IsRackIDNull() ? null : listViewIssues.GetGroup(string.Format("{0}+{1}", bookRow.RackRow.Name, bookRow.IsShelfNull() ? 0 : bookRow.Shelf));
                        break;

                    case 2:
                        li.Group = bookRow.IsRackIDNull() ? null : listViewIssues.GetGroup(bookRow.RackRow.Name);
                        break;

                    case 3:
                        li.Group = bookRow.IsTitleNull() ? null : listViewIssues.GetGroup(("LET_" + bookRow.Title.GetInitial()).ToUpper());
                        break;
                }
            }
        }




        private void backLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ListViewItem> books = new List<ListViewItem>();
            List<ListViewItem> researches = new List<ListViewItem>();
            List<ListViewItem> series = new List<ListViewItem>();

            for (int i = 0; i < Data.Book.Rows.Count; i++)
            {
                Library.BookRow bookRow = Data.Book[i];

                if (e.Argument is string)
                {
                    string pattern = (string)e.Argument;

                    pattern = pattern.ToUpper();

                    // Skip if no meta with this pattern inside

                    if (!bookRow.IsTitleNull() && bookRow.Title.ToUpper().Contains(pattern)) goto Next;
                    if (!bookRow.IsWhenNull() && bookRow.When.ToString().Contains(pattern)) goto Next;
                    if (!bookRow.IsPubIDNull() && bookRow.PublisherRow.Where.ToString().Contains(pattern)) goto Next;
                    if (!bookRow.IsExeIDNull() && bookRow.AuthorRowByExecutive.Name.ToUpper().Contains(pattern)) goto Next;
                    if (!bookRow.IsAppIDNull() && bookRow.AuthorRowByApproved.Name.ToUpper().Contains(pattern)) goto Next;
                    foreach (string keyword in bookRow.GetKeywords()) { if (keyword.ToUpper().Contains(pattern)) goto Next; }
                    foreach (string author in bookRow.GetAuthors()) { if (author.ToUpper().Contains(pattern)) goto Next; }

                    continue;
                }

            Next:
                
                switch (bookRow.Type)
                {
                    case 1:
                        ListViewItem li1 = new ListViewItem();
                        UpdateBookItem(bookRow, li1);
                        books.Add(li1);
                        break;

                    case 2:
                        ListViewItem li2 = new ListViewItem();
                        UpdateResearchItem(bookRow, li2);
                        researches.Add(li2);
                        break;
                }

                (sender as BackgroundWorker).ReportProgress(i + 1);
            }

            for (int i = 0; i < Data.Series.Rows.Count; i++)
            {
                Library.SeriesRow seriesRow = Data.Series[i];
                ListViewItem li3 = new ListViewItem();
                UpdateSeriesItem(seriesRow, li3);
                series.Add(li3);

                (sender as BackgroundWorker).ReportProgress(Data.Book.Rows.Count + i + 1);
            }

            e.Result = new ListViewItem[][] { books.ToArray(), researches.ToArray(), series.ToArray() };
        }

        private void backLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetStatus(statusProcess, string.Format(Resources.Interface.ProgressLoading, e.ProgressPercentage, statusLoading.Maximum));
            processDisplay.SetProgress(e.ProgressPercentage);
        }

        private void backLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;

            ListViewItem[][] results = (ListViewItem[][])e.Result;

            listViewBooks.Items.AddRange(results[0]);
            labelBooks.UpdateStatus(listViewBooks.Items.Count);
            tabBook.ResetFormatted(listViewBooks.Items.Count);
            UpdateBooksGroups(comboBoxBookGroup.SelectedIndex);
            UpdateBooksItemGroups(comboBoxBookGroup.SelectedIndex);

            listViewResearches.Items.AddRange(results[1]);
            labelResearches.UpdateStatus(listViewResearches.Items.Count);
            tabResearches.ResetFormatted(listViewResearches.Items.Count);
            UpdateResearchesGroups(comboBoxResearchesGroup.SelectedIndex);
            UpdateResearchesItemGroups(comboBoxResearchesGroup.SelectedIndex);

            listViewSeries.Items.AddRange(results[2]);
            labelSeries.UpdateStatus(listViewSeries.Items.Count);
            tabSeries.ResetFormatted(listViewSeries.Items.Count);
            UpdateSeriesGroups(comboBoxSeriesGroup.SelectedIndex);
            UpdateSeriesItemGroups(comboBoxSeriesGroup.SelectedIndex);

            statusLabelCount.ResetFormatted(Data.ItemsCount);

            processDisplay.StopProcessing();
            IsChanged = false;
        }

        private void Catalogue_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (CheckAndSave() == DialogResult.Cancel || backFileMover.IsBusy);

            close = true;
        }

        private void backFileMover_DoWork(object sender, DoWorkEventArgs e)
        {
            //foreach (Library.BookRow row in Data.Book)
            for (int i = 0; i < Data.Book.Rows.Count; i++)
            {
                //VerifyFile(row);

                Library.BookRow row = Data.Book[i];

                (sender as BackgroundWorker).ReportProgress(i + 1);

                // Check if Path contains full filename without extension - then OK
                // it means file was previously added and prcessed

                // If not then:
                // 1. Copy file to [[filename] + \\ + ID.ToString("000000000") + [original extension]]
                // 2. Process title page

                if (row.IsFileNull()) return;

                // Compile standard filename
                string standardPath = Path.Combine(new string[] { 
                    Path.GetDirectoryName(FileName),
                    Path.GetFileNameWithoutExtension(fileName), 
                    row.When.ToString(),
                    row.ID.ToString("D5") + Path.GetExtension(row.File)});

                if (File.Exists(standardPath)) return;

                // If there is no file by standard path

                // If there is no directory by standard path
                // Create such directory
                if (!Directory.Exists(Path.GetDirectoryName(standardPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(standardPath));
                }

                // Copy specified file to standard location
                File.Move(row.File, standardPath);

                // Change path in datatable
                row.File = standardPath;
            }
        }

        private void backFileMover_ProgressChanged(object sender, ProgressChangedEventArgs e) 
        {
            processDisplay.SetStatus(statusProcess, string.Format(Resources.Interface.ProgressFiles, e.ProgressPercentage, statusLoading.Maximum));
            processDisplay.SetProgress(e.ProgressPercentage);
        }

        private void backFileMover_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            processDisplay.StopProcessing();
            Data.WriteXml(fileName);
            IsChanged = false;
            menuFile.Enabled = true;

            if (close) Close();
        }

        #region Menu

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                Clear();
                IsChanged = false;
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (UserSettings.Interface.OpenDialog.FileName == FileName)
                { }
                else
                {
                    if (CheckAndSave() != DialogResult.Cancel)
                    {
                        LoadData(UserSettings.Interface.OpenDialog.FileName);
                    }
                }
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (FileName == null)
            {
                menuItemSaveAs_Click(sender, e);
            }
            else
            {
                Write(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Write(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }

        #endregion

        private void searchBox1_SearchTermChanged(object sender, EventArgs e)
        {
            foreach (ListView lv in new ListView[] {
                listViewBooks, listViewSeries, listViewIssues, listViewResearches })
            {
                lv.Items.Clear();
                lv.Groups.Clear();
            }

            processDisplay.StartProcessing(Data.Book.Count + Data.Series.Count, Resources.Interface.ProgressSearch);

            if (backLoader.IsBusy) {
                backLoader.CancelAsync();
            }

            backLoader.RunWorkerAsync(searchBox1.Text);
        }

        #region Books

        private void buttonBookAdd_Click(object sender, EventArgs e)
        {
            BookEdit edit = new BookEdit(Data);
            edit.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            edit.FormClosing += bookAdd_FormClosing;
            edit.Show(this);
        }

        private void bookAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((BookEdit)sender).DialogResult == DialogResult.OK)
            {
                IsChanged = true;
                ListViewItem li = new ListViewItem();
                UpdateBookItem(((BookEdit)sender).row, li);

                labelBooks.UpdateStatus(Data.BooksCount);
                statusLabelCount.ResetFormatted(Data.ItemsCount);

                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    buttonBookAdd_Click(sender, e);
                }
            }
        }

        private void listViewBooks_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewBooks.SelectedItems)
            {
                BookEdit edit = new BookEdit(Data.Book.FindByID(li.GetID()));
                edit.SetFriendlyDesktopLocation(li);
                edit.FormClosing += bookEdit_FormClosing;
                edit.Show(this);
            }
        }

        private void bookEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((BookEdit)sender).DialogResult == DialogResult.OK)
            {
                IsChanged |= ((BookEdit)sender).IsChanged;
                
                Library.BookRow row = ((BookEdit)sender).row;

                ListViewItem li = listViewBooks.GetItem(row.ID.ToString());
                li.SubItems.Clear();
                li.Name = row.ID.ToString();
                li.Text = row.Title;
                li.SubItems.Add(row.When.ToString());
                li.SubItems.Add(row.PublisherRow.Where.ToString());
                li.SubItems.Add(row.PublisherRow.Name.ToString());
                li.SubItems.Add(row.Pages.ToString());

                labelBooks.UpdateStatus(Data.BooksCount);
                statusLabelCount.ResetFormatted(Data.ItemsCount);
            }
        }

        private void contextBook_Opening(object sender, CancelEventArgs e)
        {
            contextBookEdit.Enabled = listViewBooks.SelectedItems.Count > 0;

            contextBookRead.Enabled = false;
            foreach (ListViewItem li in listViewBooks.SelectedItems)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());

                if (!bookRow.IsFileNull())
                {
                    contextBookRead.Enabled = true;
                    break;
                }
            }
        }

        private void contextBookRead_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewBooks.SelectedItems)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());
                if (bookRow.IsFileNull()) continue;
                FileSystem.RunFile(bookRow.File);
            }
        }

        private void comboBoxBookGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBooksGroups(comboBoxBookGroup.SelectedIndex);
            UpdateBooksItemGroups(comboBoxBookGroup.SelectedIndex);
        }

        #endregion

        #region Series

        private void listViewSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewIssues.Items.Clear();

            foreach (ListViewItem li in listViewSeries.SelectedItems)
            {
                Library.SeriesRow seriesRow = Data.Series.FindByID(li.GetID());

                foreach (Library.BookRow bookRow in seriesRow.GetBookRows())
                {
                    ListViewItem lii = new ListViewItem(bookRow.ID.ToString());
                    UpdateIssueItem(bookRow, lii);
                    listViewIssues.Items.Add(lii);
                }
            }

            labelIssues.UpdateStatus(listViewIssues.Items.Count);
            comboBoxIssueGroup_SelectedIndexChanged(comboBoxIssueGroup, e);
        }

        private void comboBoxSeriesGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSeriesGroups(comboBoxSeriesGroup.SelectedIndex);
            UpdateSeriesItemGroups(comboBoxSeriesGroup.SelectedIndex);
        }

        private void comboBoxIssueGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateIssuesGroups(comboBoxIssueGroup.SelectedIndex);
            UpdateIssuesItemGroups(comboBoxIssueGroup.SelectedIndex);

        }

        #endregion

        #region Researches

        private void buttonResearchAdd_Click(object sender, EventArgs e)
        {
            ResearchEdit edit = new ResearchEdit(Data);
            edit.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            edit.FormClosing += researchAdd_FormClosing;
            edit.Show(this);
        }

        private void researchAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((ResearchEdit)sender).DialogResult == DialogResult.OK)
            {
                IsChanged = true;
                ListViewItem li = new ListViewItem();
                UpdateBookItem(((ResearchEdit)sender).row, li);

                labelResearches.UpdateStatus(Data.ResearchesCount);
                statusLabelCount.ResetFormatted(Data.ItemsCount);

                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    buttonResearchAdd_Click(sender, e);
                }
            }
        }

        private void listViewResearches_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewResearches.SelectedItems)
            {
                ResearchEdit edit = new ResearchEdit(Data.Book.FindByID(li.GetID()));
                edit.SetFriendlyDesktopLocation(li);
                edit.FormClosing += researchEdit_FormClosing;
                edit.Show();
            }
        }

        private void researchEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((ResearchEdit)sender).DialogResult == DialogResult.OK)
            {
                IsChanged |= ((ResearchEdit)sender).IsChanged;

                Library.BookRow row = ((ResearchEdit)sender).row;                

                ListViewItem li = listViewResearches.GetItem(row.ID.ToString());
                li.SubItems.Clear();
                li.Name = row.ID.ToString();
                li.Text = row.Title;
                li.SubItems.Add(row.When.ToString());
                li.SubItems.Add(row.AuthorRowByExecutive.Name);

                UpdateResearchItem(row, li);
                UpdateResearchesItemGroups(comboBoxResearchesGroup.SelectedIndex);

                labelBooks.UpdateStatus(Data.ResearchesCount);
                statusLabelCount.ResetFormatted(Data.ItemsCount);
            }
        }

        private void contextResearch_Opening(object sender, CancelEventArgs e)
        {
            contextResearchEdit.Enabled = listViewResearches.SelectedItems.Count > 0;

            contextResearchRead.Enabled = false;
            foreach (ListViewItem li in listViewResearches.SelectedItems)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());

                if (!bookRow.IsFileNull())
                {
                    contextResearchRead.Enabled = true;
                    break;
                }
            }
        }

        private void contextResearchRead_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewResearches.SelectedItems)
            {
                Library.BookRow bookRow = Data.Book.FindByID(li.GetID());
                if (bookRow.IsFileNull()) continue;
                FileSystem.RunFile(bookRow.File);
            }
        }

        private void comboBoxResearchesGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateResearchesGroups(comboBoxResearchesGroup.SelectedIndex);
            UpdateResearchesItemGroups(comboBoxResearchesGroup.SelectedIndex);
        }

        #endregion

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            settings.ShowDialog();
        }

        private void menuItemLicenses_Click(object sender, EventArgs e)
        {
            //Features lics = new Features();
            //lics.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            //lics.ShowDialog(this);
        }
    }
}
