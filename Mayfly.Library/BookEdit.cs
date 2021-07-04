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
    public partial class BookEdit : Form
    {
        Library Library;

        public Library.BookRow row;

        public bool IsChanged { get; set; }



        public BookEdit(Library library)
        {
            InitializeComponent();

            Library = library;

            // UDK

            // BBK

            // ISBN - if select - open that book and increase Qty
            textBoxAuthor.Separator = UserSettings.Separator;
            textBoxAuthor.Options = Library.Author.NameColumn.GetStrings().ToArray();

            comboBoxPublisher.DataSource = Library.Publisher.Select(null, "Name Asc");
            comboBoxPublisher.SelectedIndex = -1;

            comboBoxWhere.DataSource = Library.Publisher.Select(null, "Where Asc");
            comboBoxWhere.SelectedIndex = -1;

            comboBoxRack.DataSource = Library.Rack.Select(null, "Name Asc");
            comboBoxRack.SelectedIndex = -1;
        }

        public BookEdit(Library.BookRow bookRow)
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
            if (row.IsUDKNull()) comboBoxUDK.SelectedIndex = -1;
            else comboBoxUDK.Text = row.UDK;

            if (row.IsBBKNull()) comboBoxBBK.SelectedIndex = -1;
            else comboBoxBBK.Text = row.BBK;

            if (row.IsISBNNull()) maskedISBN.Text = string.Empty;
            else maskedISBN.Text = row.ISBN;

            if (row.IsTitleNull()) textBoxTitle.Text = string.Empty;
            else textBoxTitle.Text = row.Title;

            textBoxAuthor.Strings = row.GetAuthors();


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


            if (row.IsWhenNull()) numericYear.Value = DateTime.Today.Year;
            else numericYear.Value = row.When;

            if (row.IsPagesNull()) numericPages.Value = 0;
            else numericPages.Value = row.Pages;



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
            if (!string.IsNullOrWhiteSpace(comboBoxUDK.Text)) 
                row.UDK = comboBoxUDK.Text;

            if (!string.IsNullOrWhiteSpace(comboBoxBBK.Text)) 
                row.BBK = comboBoxBBK.Text;

            if (!string.IsNullOrWhiteSpace(maskedISBN.Text))
                row.ISBN = maskedISBN.Text;

            row.Title = textBoxTitle.Text;

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

            if (comboBoxPublisher.SelectedIndex == -1)
            {
                Library.PublisherRow publisherRow = Library.Publisher.NewPublisherRow();
                publisherRow.Name = comboBoxPublisher.Text;
                publisherRow.Where = comboBoxWhere.Text;
                Library.Publisher.AddPublisherRow(publisherRow);
                row.PublisherRow = publisherRow;
            }
            else
            {
                row.PublisherRow = (Library.PublisherRow)comboBoxPublisher.SelectedItem;
            }

            row.When = (int)numericYear.Value;

            row.Pages = (int)numericPages.Value;

            row.Quantity = (int)numericQuantity.Value;

            // Rack
            if (comboBoxRack.SelectedIndex == -1)
            {
                Library.RackRow rackRow = Library.Rack.NewRackRow();
                rackRow.Name = comboBoxRack.Text;
                Library.Rack.AddRackRow(rackRow);
                row.RackRow = rackRow;
            }
            else
            {
                row.RackRow = (Library.RackRow)comboBoxRack.SelectedItem;
            }

            row.Shelf = (int)numericShelf.Value;

            if (!string.IsNullOrWhiteSpace(textBoxFile.Text))
            {
                if (File.Exists(textBoxFile.Text))
                {
                    row.File = textBoxFile.Text;
                }
            }

            row.Type = 1;
        }

        

        private void value_Changed(object sender, EventArgs e)
        {
            buttonOK.Enabled = 
                !string.IsNullOrEmpty(textBoxTitle.Text) &&
                !string.IsNullOrEmpty(textBoxAuthor.Text);

            IsChanged = true;
        }

        private void comboBoxPublisher_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxWhere.SelectedItem = comboBoxPublisher.SelectedItem;
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
        }

        private void BookEdit_FormClosing(object sender, FormClosingEventArgs e)
        { }
    }
}
