using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Library
{
    public partial class ResearchEdit : Form
    {
        Library Library;

        public Library.BookRow row;

        public bool IsChanged { get; set; }



        public ResearchEdit(Library library)
        {
            InitializeComponent();

            Library = library;

            comboBoxPublisher.DataSource = Library.Publisher.Select(null, "Name Asc");
            comboBoxPublisher.SelectedIndex = -1;

            comboBoxWhere.DataSource = Library.Publisher.Select(null, "Where Asc");
            comboBoxWhere.SelectedIndex = -1;

            comboBoxExecutive.DataSource = Library.Author.Select(null, "Name Asc");
            comboBoxExecutive.SelectedIndex = -1;

            comboBoxApproved.DataSource = Library.Author.Select(null, "Name Asc");
            comboBoxApproved.SelectedIndex = -1;

            comboBoxRack.DataSource = Library.Rack.Select(null, "Name Asc");
            comboBoxRack.SelectedIndex = -1;

            textBoxKeywords.Separator = UserSettings.Separator;
            textBoxKeywords.Options = Library.Termin.WordColumn.GetStrings().ToArray();

            textBoxAuthor.Separator = UserSettings.Separator;
            textBoxAuthor.Options = Library.Author.NameColumn.GetStrings().ToArray();
        }

        public ResearchEdit(Library.BookRow bookRow)
            : this((Library)bookRow.Table.DataSet)
        {
            row = bookRow;
            UpdateValues();
        }


        public void SetFilename(string filename)
        {
            textBoxFile.Text = filename;
        }

        public void UpdateValues()
        {
            if (row.IsPubIDNull()) 
            {
                comboBoxWhere.SelectedIndex = -1;
                comboBoxPublisher.SelectedIndex = -1;
            }
            else
            {
                comboBoxWhere.Text = row.PublisherRow.Where;
                comboBoxPublisher.Text = row.PublisherRow.Name;
            }
            //comboBoxPublisher.SelectedItem = row.IsPubIDNull() ? null : row.PublisherRow;
            //comboBoxWhere.SelectedItem = row.IsWhereNull() ? null : row.;

            comboBoxApproved.SelectedItem = row.IsAppIDNull() ? null : row.AuthorRowByApproved;
            //comboBoxApproved.Text = row.IsAppIDNull() ? string.Empty : row.GetApprovedBy().Name;

            textBoxTitle.Text = row.IsTitleNull() ? string.Empty : row.Title;

            textBoxAuthor.Strings = row.GetAuthors();

            comboBoxExecutive.SelectedItem = row.IsExeIDNull() ? null : row.AuthorRowByExecutive;
            //comboBoxExecutive.Text = row.IsExeIDNull() ? string.Empty : row.GetExecutive().Name;

            if (row.IsWhenNull()) numericYear.Value = DateTime.Today.Year;
            else numericYear.Value = row.When;

            if (row.IsPagesNull()) numericPages.Value = 1;
            else numericPages.Value = row.Pages;

            textBoxKeywords.Strings = row.GetKeywords();
            textBoxSummary.Text = row.IsSummaryNull() ? string.Empty : row.Summary;

            if (row.IsQuantityNull()) numericQuantity.Value = 0;
            else numericQuantity.Value = row.Quantity;

            if (row.IsRackIDNull()) comboBoxRack.SelectedIndex = -1;
            else comboBoxRack.SelectedItem = row.RackRow;

            if (row.IsShelfNull()) numericShelf.Value = 0;
            else numericShelf.Value = row.Shelf;

            if (row.IsFileNull()) textBoxFile.Text = string.Empty;
            else textBoxFile.Text = row.File;
        }

        public void SaveValues()
        {
            row.Title = textBoxTitle.Text;

            if (string.IsNullOrWhiteSpace(comboBoxExecutive.Text))
                row.SetExeIDNull();
            else
            {
                if (comboBoxExecutive.SelectedIndex == -1)
                {
                    Library.AuthorRow authorRow =
                        Library.Author.FindByName(comboBoxExecutive.Text.Trim());

                    if (authorRow == null)
                    {
                        authorRow = Library.Author.AddAuthorRow(comboBoxExecutive.Text.Trim());
                    }

                    row.AuthorRowByExecutive = authorRow;
                }
                else row.AuthorRowByExecutive = (Library.AuthorRow)comboBoxExecutive.SelectedItem;
            }


            if (string.IsNullOrWhiteSpace(comboBoxApproved.Text))
                row.SetAppIDNull();
            else
            {
                if (comboBoxApproved.SelectedIndex == -1)
                {
                    Library.AuthorRow authorRow =
                        Library.Author.FindByName(comboBoxApproved.Text.Trim());

                    if (authorRow == null)
                    {
                        authorRow = Library.Author.AddAuthorRow(comboBoxApproved.Text.Trim());
                    }

                    row.AuthorRowByApproved = authorRow;
                }
                else row.AuthorRowByApproved = (Library.AuthorRow)comboBoxApproved.SelectedItem;
            }

            if (comboBoxApproved.SelectedIndex == -1) row.SetAppIDNull();
            else row.AuthorRowByApproved = (Library.AuthorRow)comboBoxApproved.SelectedItem;

            Library.BylineRow[] bylines = row.GetBylineRows();

            foreach (Library.BylineRow byline in bylines)
            {
                Library.Byline.RemoveBylineRow(byline);
            }

            foreach (string author in textBoxAuthor.Strings)
            {
                Library.AuthorRow authorRow =
                    Library.Author.FindByName(author.Trim());

                if (authorRow == null)
                {
                    authorRow = Library.Author.AddAuthorRow(author.Trim());
                }

                Library.BylineRow bylineRow =
                    Library.Byline.FindByAuthorIDBookID(authorRow.ID, row.ID);

                if (bylineRow == null)
                {
                    bylineRow = Library.Byline.AddBylineRow(authorRow, row, false);
                }
            }


            if (string.IsNullOrWhiteSpace(comboBoxPublisher.Text))
                row.SetPubIDNull();
            else
            {
                if (comboBoxPublisher.SelectedIndex == -1)
                {
                    Library.PublisherRow publisherRow =
                        Library.Publisher.FindByNameWhere(comboBoxPublisher.Text.Trim(), comboBoxWhere.Text);

                    if (publisherRow == null)
                    {
                        publisherRow = Library.Publisher.NewPublisherRow();
                        publisherRow.Name = comboBoxPublisher.Text;
                        publisherRow.Where = comboBoxWhere.Text;
                        Library.Publisher.AddPublisherRow(publisherRow);
                    }

                        row.PublisherRow = publisherRow;
                }
                else row.PublisherRow = (Library.PublisherRow)comboBoxPublisher.SelectedItem;
            }

            row.When = (int)numericYear.Value;
            row.Pages = (int)numericPages.Value;
            row.Quantity = (int)numericQuantity.Value;



            Library.KeywordsRow[] keys = row.GetKeywordsRows();

            foreach (Library.KeywordsRow key in keys)
            { 
                Library.Keywords.RemoveKeywordsRow(key);
            }

            foreach (string keyword in textBoxKeywords.Strings)
            {
                Library.TerminRow terminRow = Library.Termin.FindByWord(keyword.Trim().ToUpper());

                if (terminRow == null)
                {
                    terminRow = Library.Termin.AddTerminRow(keyword.Trim().ToUpper());
                }

                Library.Keywords.AddKeywordsRow(row, terminRow);               
            }

            // Rack

            if (string.IsNullOrWhiteSpace(comboBoxRack.Text))
                row.SetRackIDNull();
            else
            {
                if (comboBoxRack.SelectedIndex == -1)
                {
                    Library.RackRow rackRow =
                        Library.Rack.FindByName(comboBoxRack.Text.Trim());

                    if (rackRow == null)
                    {
                        rackRow = Library.Rack.NewRackRow();
                        rackRow.Name = comboBoxRack.Text;
                        Library.Rack.AddRackRow(rackRow);
                    }

                    row.RackRow = rackRow;
                }
                else row.RackRow = (Library.RackRow)comboBoxRack.SelectedItem;
            }


            row.Shelf = (int)numericShelf.Value;

            if (!string.IsNullOrWhiteSpace(textBoxFile.Text))
            {
                if (File.Exists(textBoxFile.Text))
                {
                    row.File = textBoxFile.Text;
                }
            }

            if (string.IsNullOrWhiteSpace(textBoxSummary.Text)) row.SetSummaryNull();
            else row.Summary = textBoxSummary.Text;

            row.Type = 2;
        }



        private void value_Changed(object sender, EventArgs e)
        {
            buttonOK.Enabled = 
                !string.IsNullOrEmpty(textBoxTitle.Text) &&
                !string.IsNullOrEmpty(comboBoxExecutive.Text);

            IsChanged = true;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openPDF.ShowDialog() == DialogResult.OK)
            {
                SetFilename(openPDF.FileName);
            }
        }

        private void textBoxFileName_DragDrop(object sender, DragEventArgs e)
        {
            SetFilename(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
        }

        private void textBoxFileName_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effect = filenames.Length == 1 && Path.GetExtension(filenames[0]) == ".pdf" ?
                    DragDropEffects.Link : DragDropEffects.None;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (row == null)
            {
                row = Library.Book.NewBookRow();
                Library.Book.AddBookRow(row);
            }

            SaveValues();

            DialogResult = DialogResult.OK;
            Close();

            //if (WorkRow == null)
            //{
            //    WorkRow = Archive.Work.NewWorkRow();
            //    string id = numericUpDownYear.Value.ToString() + "." + Archive.Work.Count.ToString("000");
            //    WorkRow.ID = id;
            //    Archive.Work.AddWorkRow(WorkRow);

            //    File.Move(textBoxFileName.Text, Path.Combine("Research", id + ".pdf"));
            //}
            //else
            //{

            //}

            //WorkRow.Title = textBoxTitle.Text;
            //WorkRow.Year = (int)numericUpDownYear.Value;

            //ResearchArchive.ExecutiveRow ex;

            //if (comboBoxExecutive.SelectedIndex == -1)
            //{ ex = Archive.Executive.AddExecutiveRow(comboBoxExecutive.Text); }
            //else { ex = comboBoxExecutive.SelectedItem as ResearchArchive.ExecutiveRow; }
            //WorkRow.ExecutiveRow = ex;

            //Archive.WriteXml("archive.xml");
            //DialogResult = DialogResult.OK;
            //Close();
        }

        private void textBoxFile_TextChanged(object sender, EventArgs e)
        {
            buttonRead.Enabled = File.Exists(textBoxFile.Text);
            value_Changed(sender, e);
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(textBoxFile.Text);
        }
    }
}
