using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Mayfly.Library
{
    public partial class Add : Form
    {
        ResearchArchive Archive;



        public Add(ResearchArchive archive)
        {
            InitializeComponent();
            Archive = archive;
            comboBoxExecutive.DataSource = Archive.Executive.Select();
        }


        public void SetFilename(string filename)
        {
            textBoxFileName.Text = filename;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            buttonAdd.Enabled = !string.IsNullOrEmpty(textBoxTitle.Text) &&
                !string.IsNullOrEmpty(comboBoxExecutive.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = numericUpDownYear.Value.ToString() + "." + Archive.Work.Count.ToString("000");
            File.Move(textBoxFileName.Text, id + ".pdf");

            ResearchArchive.ExecutiveRow ex;

            if (comboBoxExecutive.SelectedIndex == -1) 
            { ex = Archive.Executive.AddExecutiveRow(comboBoxExecutive.Text); }
            else { ex = comboBoxExecutive.SelectedItem as ResearchArchive.ExecutiveRow; }

            ResearchArchive.WorkRow workRow = Archive.Work.NewWorkRow();

            workRow.ID = id;
            workRow.Title = textBoxTitle.Text;
            workRow.Year = (int)numericUpDownYear.Value;
            workRow.ExecutiveRow = ex;

            Archive.Work.AddWorkRow(workRow);             

            Archive.WriteXml("archive.xml");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SetFilename(openFileDialog1.FileName);
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
    }
}
