using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Fish.Legal
{
    public partial class MainForm : Form
    {
        string path { get { return Path.Combine(FileSystem.AppFolder, "legal.xml"); } }



        public MainForm()
        {
            InitializeComponent();

            ColumnSpecies.ValueType = 
                typeof(string);

            ColumnQuantity.ValueType = 
                typeof(int);

            ColumnMass.ValueType = 
                ColumnFraction.ValueType =
                typeof(decimal);


            listViewLicenses.Shine();
            listViewNotes.Shine();

            if (File.Exists(path))
            {
                Paper.ReadXml(path);
            }

            UpdateLicenses();
        }



        private void UpdateLicenses()
        {
            foreach (LegalPapers.LicenseRow licRow in Paper.License)
            {
                listViewLicenses.CreateItem(licRow.ID.ToString(), licRow.No);
            }
        }

        private void UpdateNotes(LegalPapers.LicenseRow licRow)
        {
            listViewNotes.Items.Clear();

            foreach (LegalPapers.LegalNoteRow noteRow in licRow.GetLegalNoteRows())
            {
                CreateNoteItem(noteRow);
            }
        }

        private void CreateNoteItem(LegalPapers.LegalNoteRow noteRow)
        {
            ListViewItem li = listViewNotes.CreateItem(
                noteRow.ID.ToString(),
                noteRow.No.ToString());

            li.UpdateItem(new object[]
            {
                noteRow.Date.ToShortDateString(),
                noteRow.LicenseRow.Description
            });

            li.Group = listViewNotes.Groups[noteRow.Content];
        }

        private void UpdateCatches()
        {
            spreadSheetCatches.Rows.Clear();

        }

        private void UpdateCatches(LegalPapers.LicenseRow licRow)
        {
            spreadSheetCatches.Rows.Clear();

            foreach (LegalPapers.QuoteRow quoteRow in licRow.GetQuoteRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetCatches);

                gridRow.Cells[ColumnSpecies.Index].Value = quoteRow.SpeciesRow.Local;
                gridRow.Cells[ColumnQuantity.Index].Value = quoteRow.CaughtQuantity;
                gridRow.Cells[ColumnMass.Index].Value = quoteRow.CaughtMass;
                gridRow.Cells[ColumnFraction.Index].Value = quoteRow.CaughtMass / quoteRow.Mass;

                spreadSheetCatches.Rows.Add(gridRow);
            }

        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Paper.License.Count > 0)
                Paper.WriteXml(Path.Combine(FileSystem.AppFolder, "legal.xml"));
        }

        private void buttonAddLicense_Click(object sender, EventArgs e)
        {
            FisheryLicense addLicense = new FisheryLicense(Paper);
            addLicense.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            if (addLicense.ShowDialog(this) == DialogResult.OK)
            {
                UpdateLicenses();
            }
        }

        private void listViewLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewLicenses.SelectedIndices.Count == 0)
            {
                listViewNotes.Items.Clear();
                spreadSheetCatches.Rows.Clear();
            }
            else
            {
                UpdateNotes(Paper.License.FindByID(listViewLicenses.GetID()));
                UpdateCatches(Paper.License.FindByID(listViewLicenses.GetID()));
            }

            buttonAddNote.Enabled = listViewLicenses.SelectedIndices.Count > 0;
        }

        private void listViewLicenses_ItemActivate(object sender, EventArgs e)
        {
            FisheryLicense addLicense = new FisheryLicense(Paper.License.FindByID(listViewLicenses.GetID()));
            addLicense.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            if (addLicense.ShowDialog(this) == DialogResult.OK)
            {
                UpdateLicenses();
            }
        }

        private void listViewNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewNotes.SelectedIndices.Count == 0) // display catch of selected license or
            {

            }
            else // display catch by all selected notes
            {

            }
        }

        private void contextNotePrint_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewNotes.SelectedItems)
            {
                Report report = Paper.LegalNote.FindByID(li.GetID()).GetLegalNoteReport();
                report.Run(this);
            }
        }

        private void buttonAddNote_Click(object sender, EventArgs e)
        {
            WizardLegal addNotes = new WizardLegal(Paper.License.FindByID(listViewLicenses.GetID()));
            addNotes.FormClosed += addNotes_FormClosed;
            addNotes.Show(this);
        }

        void addNotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            WizardLegal addNotes = (WizardLegal)sender;

            if (addNotes.DialogResult == DialogResult.OK)
            {
                CreateNoteItem(addNotes.CatchNote);
                CreateNoteItem(addNotes.SecondNote);
            }
        }
    }
}
