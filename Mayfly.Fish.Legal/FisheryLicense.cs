using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mayfly.Fish.Legal
{
    public partial class FisheryLicense : Form
    {
        LegalPapers Paper;

        LegalPapers.LicenseRow LicenseRow { get; set; }

        private string filename;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        public bool IsChanged { get; set; }



        public FisheryLicense()
        {
            InitializeComponent();
            listViewNotes.Shine();
            listViewNotes.MakeSortable();

            FileName = null;

            speciesLogger.IndexPath = Fish.UserSettings.SpeciesIndexPath;

            ColumnSpecies.ValueType = 
                typeof(string);

            ColumnQuantity.ValueType =
                typeof(int);

            ColumnMass.ValueType =
                ColumnFraction.ValueType =
                typeof(decimal);

            ColumnQuote.ValueType = typeof(double);
            ColumnRest.ValueType = typeof(double);

            dateUntil.MaxDate = DateTime.Today;

            Paper = new LegalPapers();
            
            textBoxExecutiveName.Text = Mayfly.UserSettings.Username;

            IsChanged = false;

            StatusLog.ResetFormatted(0);
            statusLicense.Default = StatusLog.Text;
            StatusMass.ResetFormatted(0);
            StatusCount.ResetFormatted(0);
        }

        public FisheryLicense(string filename) : this()
        {
            LoadValues(filename);
        }



        private void LoadValues(string filename)
        {
            FileName = filename;

            Paper = new LegalPapers();
            Paper.ReadXml(filename);
            LicenseRow = Paper.License[0];

            maskedBlank.Text = LicenseRow.Blank;
            maskedNo.Text = LicenseRow.No;
            dateIssued.Value = LicenseRow.Issued;
            textBoxExecutiveName.Text = LicenseRow.Executive;
            textBoxPost.Text = LicenseRow.ExecutivePost;

            textBoxProgram.Text = LicenseRow.Program;
            textBoxHolder.Text = LicenseRow.Holder;
            textBoxAddress.Text = LicenseRow.Address;
            //textBoxProgExecutive.Text = LicenseRow.ProgramExecutive;

            foreach (LegalPapers.QuoteRow quoteRow in LicenseRow.GetQuoteRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetQuotes);

                gridRow.Cells[ColumnSpecies.Index].Value = quoteRow.SpeciesRow.Species;
                gridRow.Cells[ColumnQuote.Index].Value = Convert.ToDouble(quoteRow.Mass);

                spreadSheetQuotes.Rows.Add(gridRow);
            }

            listViewNotes.Items.Clear();

            foreach (LegalPapers.LegalNoteRow noteRow in LicenseRow.GetLegalNoteRows())
            {
                CreateNoteItem(noteRow);
            }

            UpdateCatches();

            IsChanged = false;

            if (buttonAddNote.Enabled)
            {
                tabControl1.SelectedTab = pageNotes;
            }
        }

        private void SaveValues()
        {
            // Save License requisites

            if (LicenseRow == null)
            {
                LicenseRow = Paper.License.NewLicenseRow();
            }


            LicenseRow.Blank = maskedBlank.Text;
            LicenseRow.No = maskedNo.Text;
            LicenseRow.Issued = dateIssued.Value.Date;
            LicenseRow.Executive = textBoxExecutiveName.Text;
            LicenseRow.ExecutivePost = textBoxPost.Text;

            LicenseRow.Program = textBoxProgram.Text;
            LicenseRow.Holder = textBoxHolder.Text;
            LicenseRow.Address = textBoxAddress.Text;

            if (Paper.License.FindByID(LicenseRow.ID) == null)
                Paper.License.AddLicenseRow(LicenseRow);

            // Save quotes

            List<string> quotedSpecies = new List<string>();

            foreach (DataGridViewRow gridRow in spreadSheetQuotes.Rows)
            {
                if (gridRow.IsNewRow) continue;

                string species = (string)gridRow.Cells[ColumnSpecies.Index].Value;
                LegalPapers.SpeciesRow speciesRow = Paper.Species.FindBySpecies(species);
                if (speciesRow == null)
                {
                    Species.SpeciesKey.SpeciesRow refSpecies = speciesLogger.Find(species);
                    speciesRow = Paper.Species.AddSpeciesRow(species, refSpecies == null ? species : refSpecies.Name);
                }

                LegalPapers.QuoteRow quoteRow = LicenseRow.GetQuoteRow(species);

                if (quoteRow == null)
                {

                    Paper.Quote.AddQuoteRow(LicenseRow, speciesRow,
                        Convert.ToDecimal(gridRow.Cells[ColumnQuote.Index].Value));
                }
                else
                {
                    quoteRow.Mass = Convert.ToDecimal(gridRow.Cells[ColumnQuote.Index].Value);
                }

                quotedSpecies.Add(species);
            }

            // Delete all others

            for (int i = 0; i < LicenseRow.GetQuoteRows().Length; i++)
            {
                if (!quotedSpecies.Contains(LicenseRow.GetQuoteRows()[i].SpeciesRow.Species))
                {
                    Paper.Quote.RemoveQuoteRow(LicenseRow.GetQuoteRows()[i]);
                    i--;
                }
            }
        }

        private void Save(string filename)
        {
            SaveValues();
            Paper.WriteXml(filename);
            statusLicense.Message(Wild.Resources.Interface.Messages.Saved);
            FileName = filename;
            IsChanged = false;
        }

        private DialogResult CheckAndSave()
        {
            DialogResult result = DialogResult.None;

            if (IsChanged)
            {
                TaskDialogButton tdbPressed = taskDialogSaveChanges.ShowDialog(this);

                if (tdbPressed == tdbSave)
                {
                    menuItemSave_Click(ToolStripMenuItemSave, new EventArgs());
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

        private ListViewItem CreateNoteItem(LegalPapers.LegalNoteRow noteRow)
        {
            ListViewItem li = listViewNotes.CreateItem(
                noteRow.ID.ToString(),
                noteRow.No.ToString()); //Requisites.ToString());

            UpdateItem(li, noteRow);

            return li;
        }

        private void UpdateItem(ListViewItem li, LegalPapers.LegalNoteRow noteRow)
        {
            li.Text = noteRow.No.ToString();

            li.UpdateItem(new object[]
            {
                noteRow.Date.ToShortDateString(),
                noteRow.Water,
                noteRow.Mass
            });

            li.Group = listViewNotes.Groups[noteRow.Content];
        }

        private void UpdateItem(ListViewItem li)
        {
            UpdateItem(li, Paper.LegalNote.FindByID(li.GetID()));
        }

        private void UpdateCatches()
        {
            spreadSheetCatches.Rows.Clear();

            decimal totalMass = 0;
            int totalQuantity = 0;

            decimal totalQuote = 0;

            if (LicenseRow == null) return;

            foreach (LegalPapers.QuoteRow quoteRow in LicenseRow.GetQuoteRows())
            {
                int q = quoteRow.CaughtQuantity(dateStarted.Value.Date, dateUntil.Value.Date);
                decimal w = quoteRow.CaughtMass(dateStarted.Value.Date, dateUntil.Value.Date);
                decimal w_cum = quoteRow.CaughtMass(quoteRow.LicenseRow.Issued, dateUntil.Value.Date);

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetCatches);
                gridRow.Cells[ColumnSpecies.Index].Value = quoteRow.SpeciesRow;
                gridRow.Cells[ColumnQuote1.Index].Value = quoteRow.Mass;
                if (q > 0) gridRow.Cells[ColumnQuantity.Index].Value = q;
                if (w > 0) gridRow.Cells[ColumnMass.Index].Value = w;
                if (w_cum > 0) gridRow.Cells[ColumnCumulate.Index].Value = w_cum;
                if (w > 0) gridRow.Cells[ColumnFraction.Index].Value = w_cum / quoteRow.Mass;
                gridRow.Cells[ColumnRest.Index].Value = quoteRow.Mass - w_cum;
                spreadSheetCatches.Rows.Add(gridRow);

                totalQuote += quoteRow.Mass;
                totalMass += w;
                totalQuantity += q;

                ((TextAndImageCell)gridRow.Cells[ColumnRest.Index]).Image =
                    quoteRow.Mass >= w ?
                    Mayfly.Resources.Icons.Check :
                    Mayfly.Resources.Icons.NoneRed;
            }


            StatusLog.ResetFormatted(totalQuote == 0 ? 0 : totalMass / totalQuote);
            statusLicense.Default = StatusLog.Text;

            StatusMass.ResetFormatted(totalMass);
            StatusCount.ResetFormatted(totalQuantity);
        }

        private Report GetCatchReport()
        {
            Report report = new Report(string.Format("Отчет о вылове по разрешению {0}", LicenseRow.No));

            List<string> waters = new List<string>();

            foreach (LegalPapers.LegalNoteRow noteRow in LicenseRow.GetLegalNoteRows())
            {
                if (noteRow.IsWaterNull()) continue;
                if (waters.Contains(noteRow.Water)) continue;
                waters.Add(noteRow.Water);
            }

            report.AddTable(Report.Table.GetLinedTable(
                new string[] { 
                    "Пользователь",
                    "Ответственный за лов",
                    "Реквизиты разрешения",
                    "Водные объекты",
                    "Период лова" },
                new string[] { 
                    LicenseRow.Holder,
                    LicenseRow.Executive,
                    LicenseRow.Requisites, 
                    waters.Merge(),
                    string.Format("{0:d} - {1:d}", dateStarted.Value.Date, dateUntil.Value.Date) }
                    ), "fill");

            double totalMass1 = 0;
            double totalMass2 = 0;
            int totalQuantity = 0;
            double totalQuote = 0;

            Report.Table table1 = new Report.Table("Структура вылова (добычи)");

            table1.StartRow();
            table1.AddHeaderCell("Вид", .25);
            table1.AddHeaderCell("Вылов, экз.");
            table1.AddHeaderCell("Вылов, кг");
            table1.AddHeaderCell(string.Format("Вылов нарастающим итогом с {0:d}, кг", LicenseRow.Issued));
            table1.AddHeaderCell("Квота, кг");
            table1.AddHeaderCell(string.Format("Освоение на {0:d}, %", dateUntil.Value.Date));
            table1.AddHeaderCell(string.Format("Остаток на {0:d}, кг", dateUntil.Value.Date));
            table1.EndRow();

            foreach (LegalPapers.QuoteRow quoteRow in LicenseRow.GetQuoteRows())
            {
                int q = quoteRow.CaughtQuantity(dateStarted.Value.Date, dateUntil.Value.Date);

                if (q > 0)
                {
                    double w1 = (double)quoteRow.CaughtMass(dateStarted.Value.Date, dateUntil.Value.Date);
                    double w2 = (double)quoteRow.CaughtMass(LicenseRow.Issued, dateUntil.Value.Date);

                    table1.StartRow();
                    table1.AddCell(quoteRow.SpeciesRow);
                    table1.AddCellRight(q == 0 ? Constants.Null : q.ToString());
                    table1.AddCellRight(w1, ColumnMass.DefaultCellStyle.Format);
                    table1.AddCellRight(w2, ColumnCumulate.DefaultCellStyle.Format);
                    table1.AddCellRight(quoteRow.Mass, ColumnQuote1.DefaultCellStyle.Format);
                    table1.AddCellRight(w2 / (double)quoteRow.Mass, "P1");
                    table1.AddCellRight((double)quoteRow.Mass - w2, ColumnRest.DefaultCellStyle.Format);
                    table1.EndRow();

                    totalQuote += (double)quoteRow.Mass;

                    totalMass1 += w1;
                    totalMass2 += w2;
                    totalQuantity += q;
                }
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
            table1.AddCellRight(totalQuantity > 0 ? totalQuantity.ToString() : Constants.Null);
            table1.AddCellRight(totalMass1, ColumnMass.DefaultCellStyle.Format);
            table1.AddCellRight(totalMass2, ColumnCumulate.DefaultCellStyle.Format);
            table1.AddCellRight(Constants.Null);
            table1.AddCellRight(totalMass2 / totalQuote, "P1");
            table1.AddCellRight(totalQuote - totalMass2, ColumnRest.DefaultCellStyle.Format);
            table1.EndRow();


            report.AddTable(table1);

            report.EndBranded();

            return report;
        }




        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (UserSettings.Interface.OpenDialog.FileName == FileName)
                {
                    statusLicense.Message(Wild.Resources.Interface.Messages.AlreadyOpened);
                }
                else
                {
                    if (CheckAndSave() != DialogResult.Cancel)
                    {
                        LoadValues(UserSettings.Interface.OpenDialog.FileName);
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
                Save(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveValues();

            UserSettings.Interface.SaveDialog.FileName = LicenseRow.No + UserSettings.Interface.Extension;

            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            about.SetPowered(Properties.Resources.far, "Подготовлено в соответствии с законодательными актами Федерального агентства по рыболовству");
            about.ShowDialog();
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {            
            Settings settings = new Settings();
            settings.ShowDialog();
        }


        
        private void dateIssued_ValueChanged(object sender, EventArgs e)
        {
            dateStarted.MinDate = dateIssued.Value;
            buttonClear_Click(sender, e);
        }

        private void text_TextChanged(object sender, EventArgs e)
        {
            IsChanged = true;

            buttonAddNote.Enabled = 
                maskedBlank.Text.IsAcceptable() &&
                maskedNo.Text.IsAcceptable() &&
                textBoxExecutiveName.Text.IsAcceptable() &&
                textBoxProgram.Text.IsAcceptable() &&
                    textBoxHolder.Text.IsAcceptable() &&
                    textBoxAddress.Text.IsAcceptable();// &&
                    //textBoxProgExecutive.Text.IsAcceptable();

            pictureExecutive.Visible = !textBoxExecutiveName.Text.IsAcceptable();
            pictureBlank.Visible = !maskedBlank.Text.IsAcceptable();
            pictureNo.Visible = !maskedNo.Text.IsAcceptable();
            pictureProgram.Visible = !textBoxProgram.Text.IsAcceptable();
            pictureHolder.Visible = !textBoxHolder.Text.IsAcceptable();
            pictureAddress.Visible = !textBoxAddress.Text.IsAcceptable();




        }



        private void listViewNotes_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (IO.MaskedNames((string[])e.Data.GetData(DataFormats.FileDrop),
                    Fish.UserSettings.Interface.Extension).Length > 0)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void listViewNotes_DragDrop(object sender, DragEventArgs e)
        {
            SaveValues();

            WizardNotes addNotes = new WizardNotes(LicenseRow);
            addNotes.LoadCards(IO.MaskedNames((string[])e.Data.GetData(DataFormats.FileDrop), Fish.UserSettings.Interface.Extension));
            addNotes.FormClosed += addNotes_FormClosed;
            addNotes.Show(this);
        }
        
        private void buttonAddNote_Click(object sender, EventArgs e)
        {
            SaveValues();

            WizardNotes addNotes = new WizardNotes(LicenseRow);
            addNotes.FormClosed += addNotes_FormClosed;
            addNotes.Show(this);
        }

        private void addNotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            WizardNotes addNotes = (WizardNotes)sender;

            listViewNotes.SelectedItems.Clear();

            if (addNotes.DialogResult == DialogResult.OK)
            {
                IsChanged = true;

                CreateNoteItem(addNotes.CatchNote).Selected = true;
                CreateNoteItem(addNotes.SecondNote).Selected = true;

                UpdateCatches();
            }
        }



        private void dateStarted_ValueChanged(object sender, EventArgs e)
        {
            dateUntil.MinDate = dateStarted.Value;
            UpdateCatches();
        }

        private void dateUntil_ValueChanged(object sender, EventArgs e)
        {
            UpdateCatches();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            dateStarted.Value = dateIssued.Value;
            DateTime yearend = new DateTime(dateIssued.Value.Year, 12, 31);
            dateUntil.Value = (DateTime.Today < yearend) ? DateTime.Today : yearend;
        }

        private void spreadSheetQuotes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
        }

        private void contextNotePrint_Click(object sender, EventArgs e)
        {
            if (listViewNotes.SelectedItems.Count == 0) return;

            //LegalNote legalNote = new LegalNote();

            foreach (ListViewItem li in listViewNotes.SelectedItems)
            {
                Paper.LegalNote.FindByID(li.GetID()).GetLegalNoteReport().Run(); //.AddLegalNoteReport(legalNote);

                //if (listViewNotes.SelectedItems.IndexOf(li) < listViewNotes.SelectedItems.Count - 1) 
                //    legalNote.BreakPage(PageBreakOption.Odd);
            }

            //legalNote.Run();
        }

        private void contextNoteEdit_Click(object sender, EventArgs e)
        {
            SaveValues();

            foreach (ListViewItem li in listViewNotes.SelectedItems)
            {
                WizardNotes editNote = new WizardNotes(Paper.LegalNote.FindByID(li.GetID()));
                editNote.FormClosed +=  editNote_FormClosed;
                editNote.Show(this);
            }

        }

        private void editNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            WizardNotes editNote = (WizardNotes)sender;
            
            if (editNote.DialogResult == DialogResult.OK)
            {
                IsChanged = true;
             
                Paper.ResetNumbers(LegalNoteType.Catch);
                Paper.ResetNumbers((LegalNoteType)editNote.SecondNote.Content);

                foreach (ListViewItem li in listViewNotes.Items)
                {
                    UpdateItem(li);
                }

                listViewNotes.Sort();

                //UpdateItem(listViewNotes.GetItem(editNote.CatchNote.ID.ToString()));
                //UpdateItem(listViewNotes.GetItem(editNote.SecondNote.ID.ToString()));

                UpdateCatches();
            }
        }

        private void contextNoteDelete_Click(object sender, EventArgs e)
        {
            if (taskDialogDelete.ShowDialog() == tdbDeleteYes)
            {
                foreach (ListViewItem li in listViewNotes.SelectedItems)
                {
                    Paper.LegalNote.Rows.Remove(Paper.LegalNote.FindByID(li.GetID()));
                    li.Remove();
                }

                UpdateCatches();
                IsChanged = true;
            }
        }

        private void FisheryLicense_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (CheckAndSave() == DialogResult.Cancel);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            GetCatchReport().Run();
        }

        private void buttonSendReport_Click(object sender, EventArgs e)
        {
            string subject = string.Format("Отчет о вылове по разрешению {0} ({1:d} — {2:d}) от {3}",
                LicenseRow.No,
                dateStarted.Value.Date, dateUntil.Value.Date,
                LicenseRow.Executive);

            string body = string.Format("За период с {0:d} по {1:d} включительно по разрешению {2} было выловлено:\r\n\r\n",
                dateStarted.Value.Date, dateUntil.Value.Date, LicenseRow.Requisites);

            decimal totalMass1 = 0;
            decimal totalMass2 = 0;
            int totalQuantity = 0;
            decimal totalQuote = 0;

            body += String.Format("{0,-20}{1,15}{2,15}{3,15}{4,15}\r\n", "Вид", "Количество,", "Масса,", "Масса нараст.", "Освоение");
            body += String.Format("{0,-20}{1,15}{2,15}{3,15}{4,15}\r\n\r\n", "", "экз.", "кг", "итогом, кг", "квоты, %");

            foreach (LegalPapers.QuoteRow quoteRow in LicenseRow.GetQuoteRows())
            {
                int q = quoteRow.CaughtQuantity(dateStarted.Value.Date, dateUntil.Value.Date);

                if (q > 0)
                {
                    decimal w1 = quoteRow.CaughtMass(dateStarted.Value.Date, dateUntil.Value.Date);
                    decimal w2 = quoteRow.CaughtMass(LicenseRow.Issued, dateUntil.Value.Date);

                    body += String.Format("{0,-20:s}{1,15}{2,15:n1}{3,15:n1}{4,15:p1}\r\n",
                        quoteRow.SpeciesRow, q,  w1, w2, (w2 / quoteRow.Mass));

                    totalQuote += quoteRow.Mass;

                    totalMass1 += w1;
                    totalMass2 += w2;
                    totalQuantity += q;
                }
            }


            body += String.Format("\n{0,-20}{1,15}{2,15}{3,15}{4,15}\r\n\r\n",
                "Итого",
                totalQuantity,
                totalMass1.ToString("N1"),
                totalMass2.ToString("N1"),
                (totalMass2 / totalQuote).ToString("P1")
                );

            body += "Файл разрешения с актами вылова прилагается.";

            try
            {
                Server.SendEmail(string.Empty, subject, body, FileName);
            }
            catch
            {
                Clipboard.SetText(subject + Environment.NewLine + body);
                throw new Exception("Произошла ошибка отправки e-mail. Текст письма скопирован в буфер обмена.");
            }

            //Report report = new Report();

            //report.AddParagraph("За период с {0:d} по {1:d} включительно по разрешению {2} было выловлено:",
            //    calendarCatch.SelectionStart.Date, calendarCatch.SelectionEnd.Date, LicenseRow.Requisites));

            //Report.Table table1 = new Report.Table();
            //report.StartLog(new string[] { "Вид", "Количество, экз.", "Масса, кг", "Освоение квоты, %" });

            ////decimal totalMass = 0;
            ////int totalQuantity = 0;

            ////decimal totalQuote = 0;

            //foreach (LegalPapers.QuoteRow quoteRow in LicenseRow.GetQuoteRows())
            //{
            //    int q = quoteRow.CaughtQuantity(calendarCatch.SelectionStart.Date, calendarCatch.SelectionEnd.Date);

            //    if (q > 0)
            //    {
            //        decimal w = quoteRow.CaughtMass(calendarCatch.SelectionStart.Date, calendarCatch.SelectionEnd.Date);

            //        body += string.Format("{0}\t\t\t{1}\t{2:N2}\t{3:P1}\r\n",
            //            quoteRow.SpeciesRow.Name,
            //            q,
            //            w,
            //            w / quoteRow.Mass);

            //        table1.StartRow();
            //        table1.AddCell(quoteRow.SpeciesRow.Name);
            //        table1.AddCellRight(q.ToDouble(), ColumnQuantity.DefaultCellStyle.Format);
            //        table1.AddCellRight(w.ToDouble(), ColumnMass.DefaultCellStyle.Format);
            //        table1.AddCellRight((w / quoteRow.Mass).ToDouble(), ColumnFraction.DefaultCellStyle.Format);
            //        table1.EndRow();

            //        totalQuote += quoteRow.Mass;

            //        totalMass += w;
            //        totalQuantity += q;
            //    }
            //}

            //table1.StartRow();
            //table1.AddCell("Итого");
            //table1.AddCellRight(totalQuantity.ToDouble(), ColumnQuantity.DefaultCellStyle.Format);
            //table1.AddCellRight(totalMass.ToDouble(), ColumnMass.DefaultCellStyle.Format);
            //table1.AddCellRight((totalMass / totalQuote).ToDouble(), ColumnFraction.DefaultCellStyle.Format);
            //table1.EndRow();

            //report.AddParagraph("Файл разрешения с актами вылова прилагается.");

            //new Mail().ComposeMail(new string[] { }, subject,
            //    report.ToString(),
            //    new string[] { FileName });
        }
    }
}
