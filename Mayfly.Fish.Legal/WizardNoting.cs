﻿using Mayfly.Extensions;
using Mayfly.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using Mayfly.Controls;

namespace Mayfly.Fish.Legal
{
    public partial class WizardNoting : Form
    {
        LegalPapers.LicenseRow License;

        LegalPapers Paper 
        {
            get 
            {
                return (LegalPapers)License.Table.DataSet;
            }
        }

        Data Data;

        public LegalPapers.LegalNoteRow CatchNote { get; private set; }

        public LegalPapers.LegalNoteRow ReleaseNote { get; private set; }

        public LegalPapers.LegalNoteRow UtilizationNote { get; private set; }

        public LegalPapers.LegalNoteRow TransportNote { get; private set; }

        private decimal midmass = decimal.MinusOne;



        public WizardNoting()
        {
            InitializeComponent();
            listViewCards.Shine();

            speciesLogger.IndexPath = Fish.UserSettings.SpeciesIndexPath;

            Data = new Data();
            Data.InitializeBio();
            Data.WeightModels.VisualConfirmation =
                Data.GrowthModels.VisualConfirmation =
                false;

            ColumnQuantity.ValueType = typeof(int);
            ColumnMass.ValueType = typeof(decimal);

            Fish.UserSettings.Interface.OpenDialog.Multiselect = true;
            checkBoxUseWater.Checked = UserSettings.UseWaterAsNotingOffice;
        }

        public WizardNoting(LegalPapers.LicenseRow license) : this()
        {
            License = license;

            textBoxCompiler.Text = License.ExecutiveFormal;
        }

        //public WizardNoting(LegalPapers.LegalNoteRow legalNoteRow)
        //    : this(legalNoteRow.LicenseRow)
        //{
        //    if ((LegalNoteType)legalNoteRow.Content == LegalNoteType.Catch)
        //    {
        //        CatchNote = legalNoteRow;
        //        SecondNote = ((LegalPapers.LegalNoteDataTable)legalNoteRow.Table).FindByID(legalNoteRow.Second);
        //    }
        //    else
        //    {
        //        CatchNote = legalNoteRow.CorrespondingCatchNote;
        //        SecondNote = legalNoteRow;
        //    }

        //    // Have to supress files page

        //    pageData.Suppress = true;

        //    LoadValues();
        //}



        //internal void LoadValues()
        //{
        //    // Fill the fields

        //    textBoxBystander1.Text = CatchNote.IsBystander1Null() ? string.Empty : CatchNote.Bystander1;
        //    textBoxBystander2.Text = CatchNote.IsBystander2Null() ? string.Empty : CatchNote.Bystander2;
        //    textBoxPlace.Text = CatchNote.IsPlaceNull() ? string.Empty : CatchNote.Place;
        //    textBoxGear.Text = CatchNote.IsGearNull() ? string.Empty : CatchNote.Gear;
        //    textBoxWater.Text = CatchNote.IsWaterNull() ? string.Empty : CatchNote.Water;
        //    checkBoxUseWater.Checked = textBoxWater.Text == textBoxPlace.Text;

        //    // Fill the catch

        //    foreach (LegalPapers.CatchRow catchRow in CatchNote.GetCatchRows())
        //    {
        //        DataGridViewRow gridRow = new DataGridViewRow();
        //        gridRow.CreateCells(spreadSheetCatches);

        //        gridRow.Cells[ColumnSpecies.Index].Value = catchRow.SpeciesRow.Species;

        //        LegalPapers.QuoteRow quoteRow = License.GetQuoteRow(catchRow.SpeciesRow.Species);
        //        if (quoteRow != null)  {
        //            gridRow.Cells[ColumnExploration.Index].Value = ((quoteRow.CaughtMass() - catchRow.Mass) / quoteRow.Mass);
        //        }

        //        gridRow.Cells[ColumnQuantity.Index].Value = catchRow.Quantity;
        //        spreadSheetCatches.Rows.Add(gridRow);
        //        gridRow.Cells[ColumnMass.Index].Value = catchRow.Mass;
        //    }

        //    dateTimePicker1.Value = CatchNote.Date;

        //    switch ((LegalNoteType)SecondNote.Content)
        //    {
        //        case LegalNoteType.Release:
        //            radioButtonRelease.Checked = true;
        //            break;

        //        case LegalNoteType.Utilization:
        //            radioButtonUtilization.Checked = true;
        //            radioButtonDispose.Checked = SecondNote.Utilization == 0;
        //            radioButtonFood.Checked = SecondNote.Utilization == 1;
        //            radioButtonExpedition.Checked = SecondNote.Utilization == 2;
        //            textBoxDispose.Text = SecondNote.IsTechnicNull() ? string.Empty : SecondNote.Technic;
        //            break;

        //        case LegalNoteType.Transport:
        //            radioButtonTransport.Checked = true;
        //            break;
        //    }

        //    if ((LegalNoteType)SecondNote.Content == LegalNoteType.Transport ||
        //        ((LegalNoteType)SecondNote.Content == LegalNoteType.Utilization && SecondNote.Utilization == 2))
        //    {
        //        textBoxExpeditor.Text = SecondNote.IsExpeditorNull() ? string.Empty : SecondNote.Expeditor;
        //        textBoxExpeditorRequisites.Text = SecondNote.IsExpeditorRequisitesNull() ? string.Empty : SecondNote.ExpeditorRequisites;
        //        textBoxRoute.Text = SecondNote.IsRouteNull() ? string.Empty : SecondNote.Route;
        //        textBoxDestination.Text = SecondNote.IsDestinationNull() ? string.Empty : SecondNote.Destination;
        //        textBoxAddressee.Text = SecondNote.IsAddresseeNull() ? string.Empty : SecondNote.Addressee;
        //        textBoxConservant.Text = SecondNote.IsConservantNull() ? string.Empty : SecondNote.Conservant;
        //        textBoxDish.Text = SecondNote.IsDishNull() ? string.Empty : SecondNote.Dish;
        //    }
        //}
        
        internal void LoadCards(string[] entries)
        {
            string[] filenames = FileSystem.MaskedNames(entries, Fish.UserSettings.Interface.Extension);
            pageData.SetNavigation(false);
            loaderData.RunWorkerAsync(filenames);
        }

        private void UpdateCards()
        {
            listViewCards.Items.Clear();

            foreach (Data.CardRow cardRow in Data.Card)
            {
                ListViewItem li = listViewCards.CreateItem(
                    cardRow.Path, Path.GetFileNameWithoutExtension(cardRow.Path));

                li.UpdateItem(new object[] { 
                    cardRow.GetSamplerSign(),
                    cardRow.When.ToString("F")  });
            }
        }

        private void UpdateOverCatch()
        {
            pageCatch.AllowNext = true;

            foreach (DataGridViewRow gridRow in spreadSheetCatches.Rows)
            {
                if (gridRow.IsNewRow) continue;

                decimal mass = (decimal)gridRow.Cells[ColumnMass.Index].Value;

                LegalPapers.QuoteRow quoteRow = License.GetQuoteRow(
                    (string)gridRow.Cells[ColumnSpecies.Index].Value);

                if (quoteRow == null) continue;

                pageCatch.AllowNext &= quoteRow.RemainMass() >= mass;
            }
        }

        private void SaveValues()
        {
            if (CatchNote == null)
            {
                CatchNote = Paper.LegalNote.NewLegalNoteRow();
                CatchNote.Content = (int)LegalNoteType.Catch;
                CatchNote.No = Paper.LegalNote.GetNextNoteNumber(LegalNoteType.Catch);
                CatchNote.LicenseRow = License;
                Paper.LegalNote.AddLegalNoteRow(CatchNote);
            }

            CatchNote.Date = dateTimePicker1.Value;

            CatchNote.Bystander1 = textBoxBystander1.Text;
            CatchNote.Bystander2 = textBoxBystander2.Text;
            CatchNote.Place = textBoxPlace.Text;
            CatchNote.Gear = textBoxGear.Text;
            CatchNote.Water = textBoxWater.Text;

            if (SecondNote == null)
            {
                SecondNote = Paper.LegalNote.NewLegalNoteRow();
                SecondNote.LicenseRow = License;
                Paper.LegalNote.AddLegalNoteRow(SecondNote);

                CatchNote.Second = SecondNote.ID;
                SecondNote.Second = CatchNote.ID;
            }

            SecondNote.Date = dateTimePicker1.Value;

            SecondNote.Bystander1 = textBoxBystander1.Text;
            SecondNote.Bystander2 = textBoxBystander2.Text;
            SecondNote.Place = textBoxPlace.Text;
            SecondNote.Gear = textBoxGear.Text;
            SecondNote.Water = textBoxWater.Text;

            if (radioButtonRelease.Checked)
            {
                SecondNote.Content = (int)LegalNoteType.Release;
            }

            if (radioButtonUtilization.Checked)
            {
                SecondNote.Content = (int)LegalNoteType.Utilization;
            }

            if (radioButtonTransport.Checked)
            {
                SecondNote.Content = (int)LegalNoteType.Transport;
            }

            if (SecondNote != null)
            {
                SecondNote.No = Paper.LegalNote.GetNextNoteNumber((LegalNoteType)SecondNote.Content);
            }



            if (radioButtonUtilization.Checked)
            {
                SecondNote.Utilization =
                    radioButtonDispose.Checked ? 0 :
                    radioButtonFood.Checked ? 1 :
                    radioButtonExpedition.Checked ? 2 : -1;

                SecondNote.Technic = textBoxDispose.Text;
            }

            if (radioButtonTransport.Checked || radioButtonExpedition.Checked)
            {
                SecondNote.Expeditor = textBoxExpeditor.Text;
                SecondNote.ExpeditorRequisites = textBoxExpeditorRequisites.Text;
                SecondNote.Route = textBoxRoute.Text;
                SecondNote.Destination = textBoxDestination.Text;
                SecondNote.Addressee = textBoxAddressee.Text;
                SecondNote.Conservant = textBoxConservant.Text;
                SecondNote.Dish = textBoxDish.Text;
            }

            // Catch. Clear and create new rows

            while (CatchNote.GetCatchRows().Length > 0)
            {
                Paper.Catch.RemoveCatchRow(CatchNote.GetCatchRows()[0]);
            }

            foreach (DataGridViewRow gridRow in spreadSheetCatches.Rows)
            {
                if (gridRow.IsNewRow) continue;

                string species = (string)gridRow.Cells[ColumnSpecies.Index].Value;
                LegalPapers.SpeciesRow speciesRow = Paper.Species.FindBySpecies(species);

                if (speciesRow == null)
                {
                    Species.SpeciesKey.SpeciesRow refSpeciesRow =
                        Fish.UserSettings.SpeciesIndex.Species.FindBySpecies(species);
                    speciesRow = Paper.Species.AddSpeciesRow(species, refSpeciesRow.Local);
                }

                LegalPapers.CatchRow catchRow = Paper.Catch.NewCatchRow();

                catchRow.LegalNoteRow = CatchNote;
                catchRow.SpeciesRow = speciesRow;
                catchRow.Quantity = (int)gridRow.Cells[ColumnQuantity.Index].Value;
                catchRow.Mass = (decimal)gridRow.Cells[ColumnMass.Index].Value;

                Paper.Catch.AddCatchRow(catchRow);
            }
        }



        private void wizardControl1_Finished(object sender, EventArgs e)
        {
            SaveValues();

            DialogResult = DialogResult.OK;
            //Close();
        }

        private void wizardControl1_Cancelling(object sender, EventArgs e)
        {
            Close();
        }


        
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (Fish.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                LoadCards(Fish.UserSettings.Interface.OpenDialog.FileNames);
            }
        }

        private void listViewCards_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void listViewCards_DragDrop(object sender, DragEventArgs e)
        {
            LoadCards((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void dataLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //processDisplay.SetProgress(e.ProgressPercentage);
        }

        private void dataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] filenames = (string[])e.Argument;

            for (int i = 0; i < filenames.Length; i++)
            {
                if (Data.IsLoaded(filenames[i])) continue;

                Data data = new Data();

                if (data.Read(filenames[i]))
                {
                    Data.CardRow[] cardRows = data.ImportTo(Data);
                }

                //(sender as BackgroundWorker).ReportProgress(i + 1);
            }
        }

        private void dataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageData.SetNavigation(true);
            pageData.AllowNext = Data.GetStack().QuantityCaught() > 0;

            foreach (Data.SpeciesRow speciesRow in Data.Species.GetSorted())
            {
                //Data.GrowthModels.Refresh(speciesRow.Species);
                Data.WeightModels.Refresh(speciesRow.Species);
            }

            UpdateCards();
        }

        private void pageData_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            Fish.Explorer.CardStack catches = Data.GetStack();
            if (catches.GetInvestigators().Length > 1) textBoxBystander1.Text = catches.GetInvestigators()[1];
            if (catches.GetInvestigators().Length > 2) textBoxBystander2.Text = catches.GetInvestigators()[2];

            textBoxGear.Text = catches.GetSamplersList().Merge();
            textBoxWater.Text = catches.GetWaterNames().Merge();

            spreadSheetCatches.Rows.Clear();

            foreach (Data.SpeciesRow speciesRow in catches.GetSpeciesCaught())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetCatches);

                gridRow.Cells[ColumnSpecies.Index].Value = speciesRow.Species;

                LegalPapers.QuoteRow quoteRow = License.GetQuoteRow(
                    (string)gridRow.Cells[ColumnSpecies.Index].Value);

                if (quoteRow != null)
                {
                    gridRow.Cells[ColumnExploration.Index].Value = quoteRow.CaughtMass() / quoteRow.Mass;
                }

                gridRow.Cells[ColumnQuantity.Index].Value = catches.QuantityCaught(speciesRow);
                spreadSheetCatches.Rows.Add(gridRow);

                double w = catches.MassCaught(speciesRow);

                // First - round the mass for further papers to be easier
                if (UserSettings.RoundCatch != 0)
                {
                    gridRow.Cells[ColumnMass.Index].Value = Convert.ToDecimal(Math.Round(w, (int)(3 - Math.Log10(UserSettings.RoundCatch))));
                }

                //gridRow.Cells[ColumnMass.Index].Value = double.IsNaN(w) ? 0 : Convert.ToDecimal(w);
            }

            dateTimePicker1.Value = catches.GetDates().Last();
        }



        private void wizardPagePersons_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            UserSettings.UseWaterAsNotingOffice = checkBoxUseWater.Checked;
        }

        private void textBoxBystander1_TextChanged(object sender, EventArgs e)
        {
            textBoxBystander2.ReadOnly = (!textBoxBystander1.Text.IsAcceptable());
            labelBystander2.Enabled = (textBoxBystander1.Text.IsAcceptable());
        }

        private void checkBoxUseWater_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlace.ReadOnly = checkBoxUseWater.Checked;

            if (checkBoxUseWater.Checked)
            {
                textBoxPlace.Text = textBoxWater.Text;
            }
        }

        private void textBoxWater_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxUseWater.Checked)
            {
                textBoxPlace.Text = textBoxWater.Text;
            }
        }



        private void spreadSheetCatches_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void spreadSheetCatches_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            midmass = decimal.MinusOne;

            if (spreadSheetCatches[ColumnMass.Index, e.RowIndex].Value != null &&
                spreadSheetCatches[ColumnQuantity.Index, e.RowIndex].Value != null)
            {
                decimal mass = (decimal)spreadSheetCatches[ColumnMass.Index, e.RowIndex].Value;
                int qty = (int)spreadSheetCatches[ColumnQuantity.Index, e.RowIndex].Value;
                if (mass != 0 && qty != 0) midmass = mass / (decimal)qty;
            }
        }

        private void spreadSheetCatches_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridViewRow gridRow = spreadSheetCatches.Rows[e.RowIndex];

            LegalPapers.QuoteRow quoteRow = License.GetQuoteRow(
                (string)gridRow.Cells[ColumnSpecies.Index].Value);

            if (e.ColumnIndex == ColumnQuantity.Index)
            {
                if (gridRow.Cells[ColumnQuantity.Index].Value != null)
                {
                    int qty = (int)gridRow.Cells[ColumnQuantity.Index].Value;

                    if (UserSettings.BindCatch && midmass != decimal.MinusOne && spreadSheetCatches.CurrentCell.OwningColumn == ColumnQuantity)
                    {
                        // User changes quantity, so we have to cut mass                    
                        gridRow.Cells[ColumnMass.Index].Value = qty * midmass;
                    }
                }
                else
                {
                    midmass = decimal.MinusOne;
                }
            }

            if (e.ColumnIndex == ColumnMass.Index) // 
            {
                if (gridRow.Cells[ColumnMass.Index].Value != null)
                {
                    decimal mass = (decimal)gridRow.Cells[ColumnMass.Index].Value;

                    if (UserSettings.BindCatch && midmass != decimal.MinusOne && spreadSheetCatches.CurrentCell.OwningColumn == ColumnMass)
                    {
                        // User changes mass, so we have to cut quantity
                        gridRow.Cells[ColumnQuantity.Index].Value = (int)(mass / midmass);
                    }

                    if (quoteRow == null)
                    {
                        ((TextAndImageCell)gridRow.Cells[ColumnMass.Index]).Image =
                            Mayfly.Properties.Resources.NoneRed;
                    }
                    else
                    {
                        gridRow.Cells[ColumnFraction.Index].Value = mass / quoteRow.Mass;

                        ((TextAndImageCell)gridRow.Cells[ColumnMass.Index]).Image =
                            (quoteRow.RemainMass() + (CatchNote == null ? 0 : CatchNote.Mass) >= mass) ?
                            Mayfly.Properties.Resources.Check :
                            Mayfly.Properties.Resources.NoneRed;

                        if (UserSettings.PreventOvercatch)
                        {
                            UpdateOverCatch();
                        }
                    }
                }
                else
                {
                    midmass = decimal.MinusOne;
                }
            }

            bool containNulls = false;

            foreach (DataGridViewRow catchRow in spreadSheetCatches.Rows)
            {
                if (gridRow.Cells[ColumnMass.Index].Value == null) { containNulls = true; break; }
                if ((decimal)gridRow.Cells[ColumnMass.Index].Value == 0) { containNulls = true; break; }
            }

            pageCatch.AllowNext = !containNulls;
        }

        private void spreadSheetCatches_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (UserSettings.PreventOvercatch)
            {
                UpdateOverCatch();
            }
        }

        private void speciesLogger_DuplicateDetected(object sender, DuplicateFoundEventArgs e)
        {
            int q = 0;
            decimal w = 0;

            if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null)
            {
                q += (int)e.EditedRow.Cells[ColumnQuantity.Name].Value;
            }

            if (e.EditedRow.Cells[ColumnMass.Index].Value != null)
            {
                w += (decimal)e.EditedRow.Cells[ColumnMass.Name].Value;
            }

            if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null)
            {
                q += (int)e.DuplicateRow.Cells[ColumnQuantity.Name].Value;
            }

            if (e.DuplicateRow.Cells[ColumnMass.Index].Value != null)
            {
                w += (decimal)e.DuplicateRow.Cells[ColumnMass.Name].Value;
            }

            if (q > 0)
            {
                e.EditedRow.Cells[ColumnQuantity.Index].Value = q;
            }

            if (w > 0)
            {
                e.EditedRow.Cells[ColumnMass.Index].Value = w;
            }

            spreadSheetCatches.Rows.Remove(e.DuplicateRow);
        }

        private void speciesLogger_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            LegalPapers.QuoteRow quoteRow = License.GetQuoteRow((string)e.Row.Cells[ColumnSpecies.Index].Value);

            if (quoteRow != null)
            {
                e.Row.Cells[ColumnExploration.Index].Value = quoteRow.CaughtMass() / quoteRow.Mass;
                spreadSheetCatches_CellValueChanged(sender, new DataGridViewCellEventArgs(ColumnMass.Index, e.Row.Index));
            }
        }



        private void contextCatchDelete_Click(object sender, EventArgs e)
        {
            while (spreadSheetCatches.SelectedRows.Count > 0)
            {
                spreadSheetCatches.Rows.Remove(spreadSheetCatches.SelectedRows[0]);
            }
        }


        private void pageUtilization_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            textBoxDispose.Text = UserSettings.Utilization;
        }       

        private void radioButtonUtilization_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDispose.Visible = radioButtonDispose.Checked;

            pageUtilization.AllowNext = (radioButtonDispose.Checked || radioButtonFood.Checked || radioButtonExpedition.Checked);

            //pageUtilization.IsFinishPage = !(radioButtonTransport.Checked ||
            //    (radioButtonUtilization.Checked && radioButtonExpedition.Checked));

            //pageExpedition.IsFinishPage = !pageNote.IsFinishPage;
        }

        private void pageUtilization_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (radioButtonDispose.Checked) UserSettings.UtilizationVariant = "Dispose";
            if (radioButtonFood.Checked) UserSettings.UtilizationVariant = "Food";
            if (radioButtonExpedition.Checked) UserSettings.UtilizationVariant = "Expedition";
        }


        private void wizardPageExpedition_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            label22.Visible =
                label23.Visible =
                textBoxConservant.Visible =
                textBoxDish.Visible =
                radioButtonTransport.Checked;

            textBoxExpeditorRequisites.Enabled =
                label26.Enabled = radioButtonExpedition.Checked;

            if (CatchNote == null)
            {
                textBoxExpeditor.Text = UserSettings.ProxyFace;

                if (radioButtonExpedition.Checked)
                {
                    textBoxExpeditorRequisites.Text = UserSettings.ProxyDuty;
                }

                textBoxRoute.Text = UserSettings.TransportationRoute;
                textBoxDestination.Text = UserSettings.TransportationAddress;
                textBoxAddressee.Text = UserSettings.TransportationOrg;

                if (radioButtonTransport.Checked)
                {
                    textBoxConservant.Text = UserSettings.TransportationConservation;
                    textBoxDish.Text = UserSettings.TransportationDish;
                }
            }
        }

        private void checkBoxUseOrg_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAddressee.ReadOnly = 
                textBoxDestination.ReadOnly =
                checkBoxUseOrgAsDestination.Checked;

            if (checkBoxUseOrgAsDestination.Checked)
            {
                textBoxDestination.Text = License.Address;
                textBoxAddressee.Text = License.Holder;
            }
        }

        private void checkBoxUseCompiler_CheckedChanged(object sender, EventArgs e)
        {
            textBoxExpeditor.ReadOnly = 
                checkBoxUseCompilerAsExpeditor.Checked;

            if (checkBoxUseCompilerAsExpeditor.Checked)
            {
                textBoxExpeditor.Text = textBoxCompiler.Text;
            }
        }

        private void textBoxPost_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxUseOrgAsDestination.Checked)
            {
                textBoxExpeditor.Text = textBoxCompiler.Text;
            }
        }

        private void wizardPageExpedition_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            UserSettings.ProxyFace = textBoxExpeditor.Text;

            if (radioButtonExpedition.Checked) {
                UserSettings.ProxyDuty = textBoxExpeditorRequisites.Text;
            }

            UserSettings.TransportationRoute = textBoxRoute.Text;
            UserSettings.TransportationAddress = textBoxDestination.Text;
            UserSettings.TransportationOrg = textBoxAddressee.Text;

            UserSettings.TransportationConservation = textBoxConservant.Text;
            UserSettings.TransportationDish = textBoxDish.Text;
        }
    }
}
