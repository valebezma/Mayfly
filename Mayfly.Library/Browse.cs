using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Mayfly.Extensions;

namespace Mayfly.Library
{
    public partial class Browse : Form
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

        ResearchArchive archive;

        ListViewColumnSorter lvwColumnSorter;




        public Browse(string filename)
        {
            InitializeComponent();

            SetWindowTheme(listView1.Handle, "explorer", null);
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;

            archive = new ResearchArchive();

            try
            {
                archive.ReadXml(filename);
                //Text = Path.GetFileNameWithoutExtension(filename);

                LoadArchive();
            }
            catch
            {

            }
        }




        private void LoadArchive()
        {
            listView1.Items.Clear();

            foreach (ResearchArchive.WorkRow workRow in archive.Work)
            {
                listView1.Items.Add(GetItem(workRow));
            }

            label2.Text = string.Format("Записей: {0}", listView1.Items.Count);
        }

        private void LoadArchive(string text)
        {
            listView1.Items.Clear();

            foreach (ResearchArchive.WorkRow workRow in archive.Work)
            {
                if (workRow.ID.ToLower().Contains(text.ToLower()) ||
                    workRow.Title.ToLower().Contains(text.ToLower()) ||
                    workRow.Year.ToString().Contains(text.ToLower()) ||
                    workRow.ExecutiveRow.Name.ToLower().Contains(text.ToLower()))
                {
                    listView1.Items.Add(GetItem(workRow));
                }
            }

            label2.Text = string.Format("Записей: {0}", listView1.Items.Count);
        }

        private ListViewItem GetItem(ResearchArchive.WorkRow workRow)
        {
            ListViewItem item = new ListViewItem();
            item.Name = workRow.ID;
            item.Text = workRow.Title;
            item.SubItems.AddRange(new string[] { 
                        //workRow.Title, 
                        workRow.Year.ToString(), workRow.ExecutiveRow.Name });

            return item;
        }

        private ResearchArchive.WorkRow GetResearch(ListViewItem item)
        {
            return archive.Work.FindByID(item.Name);
        }




        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            menuItemRun_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadArchive(textBoxSearch.Text);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effect = filenames.Length == 1 && Path.GetExtension(filenames[0]) == ".pdf" ?
                    DragDropEffects.Link : DragDropEffects.None;
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            ResearchEdit addForm = new ResearchEdit(archive);
            textBoxSearch.Text = string.Empty;
            addForm.SetFilename(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);

            if (addForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadArchive();
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
        }



        private void menuItemRun_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                string filename = Path.Combine("Research", item.Name + ".pdf");

                if (File.Exists(filename))
                {
                    var p = new Process();
                    p.StartInfo.FileName = filename;
                    p.Start();
                }
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                ResearchCard card = new ResearchCard(GetResearch(item));
                card.SetFriendlyDesktopLocation(listView1);
                card.Show();
            }
        }

        private void menuItemEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                ResearchEdit card = new ResearchEdit(GetResearch(item));
                card.SetFriendlyDesktopLocation(listView1);
                card.Show();
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            string copyline = string.Empty;

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                copyline += GetResearch(item).GetReference() + Environment.NewLine;
            }

            Clipboard.SetText(copyline);
        }
    }
}
