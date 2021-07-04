using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Collections.Generic;

namespace Mayfly.Software.Locals
{
    public partial class Locals : Form
    {
        public Locals(string path)
        {
            InitializeComponent();

            textBoxRoot.Text = path;

            comboBoxCulture.Items.Add(new CultureInfo("ru"));
        }



        public void GetRes()
        {
            foreach (string resxFile in comboBoxFile.Items)
            {
                GetRes((string)resxFile); //System.IO.Path.Combine(Path, (string)resxFile));
            }
        }

        public void GetRes(string resxFile)
        {
            ResXResourceReader resxReader = new ResXResourceReader(resxFile);

            foreach (DictionaryEntry entry in resxReader)
            {
                if (!((string)entry.Key).EndsWith("Text")) continue;
                if (((string)entry.Value).Length < numericFrom.Value) continue;
                if (((string)entry.Value).Length > numericEnd.Value) continue;

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetValues);
                gridRow.Cells[ColumnFile.Index].Value = resxFile.Substring(textBoxRoot.Text.Length);
                gridRow.Cells[ColumnRes.Index].Value = entry.Key;
                gridRow.Cells[ColumnText.Index].Value = entry.Value;

                spreadSheetValues.Rows.Add(gridRow);

                gridRow.Tag = resxFile;
            }
        }

        public void GetLocTexts(CultureInfo culture)
        {
            string currentResFile = string.Empty;
            ResXResourceReader resxReader = new ResXResourceReader(Stream.Null);

            foreach (DataGridViewRow gridRow in spreadSheetValues.Rows)
            {
                try
                {
                    string resxFile = (string)gridRow.Tag;
                    if (currentResFile != resxFile.Replace(".resx", "." + culture.TwoLetterISOLanguageName + ".resx"))
                    {
                        currentResFile = resxFile.Replace(".resx", "." + culture.TwoLetterISOLanguageName + ".resx");
                        resxReader = new ResXResourceReader(currentResFile);
                    }

                    IDictionaryEnumerator locEntry = resxReader.GetEnumerator();
                    while (locEntry.MoveNext())
                    {
                        if (locEntry.Key.ToString() == spreadSheetValues[ColumnRes.Index, gridRow.Index].Value.ToString())
                        {
                            gridRow.Cells[ColumnLoc.Index].Value = locEntry.Value;
                            break;
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    gridRow.Cells[ColumnLoc.Index].ReadOnly = true;
                }
            }
        }

        public string[] GetResourceFileNames(string path)
        {
            List<string> result = new List<string>();

            foreach (string resxFile in Directory.GetFiles(path, "*.resx", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(resxFile).Count(c => c == '.') > 1) continue;

                int texts = 0;

                try
                {
                    ResXResourceReader resxReader = new ResXResourceReader(resxFile);

                    foreach (DictionaryEntry entry in resxReader)
                    {
                        if (((string)entry.Key).EndsWith(".Text"))
                        {
                            texts = 1;
                            break;
                        }
                    }
                }
                catch { continue; }

                if (texts == 0) continue;

                result.Add(resxFile);
            }

            return result.ToArray();
        }



        private void textBoxRoot_TextChanged(object sender, System.EventArgs e)
        {
            if (!Directory.Exists(textBoxRoot.Text)) return;
            comboBoxProject.Items.AddRange(Directory.GetDirectories(textBoxRoot.Text));
            comboBoxProject_SelectedIndexChanged(comboBoxProject, new System.EventArgs());
        }

        private void comboBoxProject_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboBoxFile.Items.Clear();
            if (comboBoxProject.SelectedIndex == -1) comboBoxFile.Items.AddRange(GetResourceFileNames(textBoxRoot.Text));
            else comboBoxFile.Items.AddRange(GetResourceFileNames(Path.Combine(textBoxRoot.Text, (string)comboBoxProject.SelectedItem)));
        }

        private void comboBoxCulture_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CultureInfo culture = (CultureInfo)comboBoxCulture.SelectedItem;

            if (culture == null)
            {
                ColumnLoc.HeaderText = "Loc not selected";
                ColumnLoc.ReadOnly = true;
            }
            else
            {
                ColumnLoc.ReadOnly = false;
                ColumnLoc.ResetFormatted(culture.DisplayName);
                GetLocTexts(culture);
            }
        }

        private void comboBoxProject_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void buttonLoad_Click(object sender, System.EventArgs e)
        {
            spreadSheetValues.Rows.Clear();

            if (comboBoxFile.SelectedIndex == -1) GetRes();
            else GetRes((string)comboBoxFile.SelectedItem);

            comboBoxCulture_SelectedIndexChanged(sender, e);
        }

        private void buttonPrint_Click(object sender, System.EventArgs e)
        {
            if (comboBoxProject.SelectedIndex == -1)
            {
                foreach (string path in comboBoxProject.Items)
                {
                    Report r = GetProjectSpellSheet(path);
                    if (r != null) r.Run();
                }
            }
            else
            {
                Report r = GetProjectSpellSheet((string)comboBoxProject.SelectedItem);
                if (r != null) r.Run();
            }
        }

        private Report GetProjectSpellSheet(string path)
        {
            if (!ContainsResFolder(path)) return null;

            Report report = new Report(string.Format("Project: {0}", path.Substring(textBoxRoot.Text.Length)));

            if (comboBoxFile.SelectedIndex == -1)
            {
                foreach (string resxFile in GetResourceFileNames(path))
                {
                    AddResx(report, resxFile);
                }
            }
            else
            {
                AddResx(report, (string)comboBoxFile.SelectedItem);
            }

            return report;
        }

        private bool ContainsResFolder(string path)
        {
            foreach (string resxFile in GetResourceFileNames(path))
            {
                if (ContainsRes(resxFile)) return true;
            }

            return false;
        }

        private void AddResx(Report report, string resxFile)
        {
            if (!ContainsRes(resxFile)) return;

            report.AddSubtitle3("Resource file: {0}", Path.GetFileNameWithoutExtension(resxFile));

            ResXResourceReader resxReader = new ResXResourceReader(resxFile);
            ResXResourceReader resxReaderLoc = new ResXResourceReader(Stream.Null);

            if (comboBoxCulture.SelectedIndex != -1)
            {
                try
                {
                    resxReaderLoc = new ResXResourceReader(
                        resxFile.Replace(".resx", "." +
                        ((CultureInfo)comboBoxCulture.SelectedItem).TwoLetterISOLanguageName + ".resx"));
                }
                catch { }
            }

            foreach (DictionaryEntry entry in resxReader)
            {
                if (!((string)entry.Key).EndsWith("Text")) continue;
                if (((string)entry.Value).Length < numericFrom.Value) continue;
                if (((string)entry.Value).Length > numericEnd.Value) continue;
                
                Report.Table table1 = new Report.Table("Entity: {0}", entry.Key);
                table1.StartRow();
                table1.AddCell(entry.Value);

                if (comboBoxCulture.SelectedIndex == -1)
                {
                    //table1.AddCell(Constants.Null);
                }
                else
                {
                    try
                    {
                        bool found = false;
                        IDictionaryEnumerator locEntry = resxReaderLoc.GetEnumerator();
                        while (locEntry.MoveNext())
                        {
                            if (locEntry.Key.ToString() == entry.Key.ToString())
                            {
                                table1.AddCell(locEntry.Value);
                                locEntry.Reset();
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                            table1.AddCell(Constants.Null);
                    }
                    catch
                    {
                        //table1.AddCell(Constants.Null);
                    }
                }

                table1.EndRow();

                report.AddTable(table1, "airy");
            }
        }

        private bool ContainsRes(string resxFile)
        {
            ResXResourceReader resxReader = new ResXResourceReader(resxFile);

            foreach (DictionaryEntry entry in resxReader)
            {
                if (!((string)entry.Key).EndsWith("Text")) continue;
                if (((string)entry.Value).Length < numericFrom.Value) continue;
                if (((string)entry.Value).Length > numericEnd.Value) continue;

                return true;
            }

            return false;
        }
    }
}
