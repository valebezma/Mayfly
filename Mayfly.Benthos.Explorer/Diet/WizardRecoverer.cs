using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using Mayfly.Wild;
using Mayfly.Species;
using static Mayfly.Wild.SettingsReader;

namespace Mayfly.Benthos.Explorer
{
    public partial class WizardRecoverer : Form
    {
        public Wild.Survey BadData;

        public Wild.Survey NaturalData;

        public int TotalUnweighted;

        public int TotalRecoverable;

        public Report Report;

        public event EventHandler DataRecovered;

        List<string> CardsToLoad = new List<string>();

        readonly DataColumn[] CategorialVariables;

        public List<Wild.Survey.IndividualRow> RecoveredIndividualRows = new List<Wild.Survey.IndividualRow>();



        private WizardRecoverer()
        {
            InitializeComponent();

            columnPriSpecies.ValueType = typeof(string);
            columnPriLength.ValueType = typeof(int);
            columnPriRaw.ValueType = typeof(int);
            columnPriRecoverable.ValueType = typeof(int);
            columnPriRecoverableP.ValueType = typeof(double);

            openNatural.Filter = IO.FilterFromExt(
                 Interface.Extension, 
                 Wild.UserSettings.InterfaceBio.Extension);

            checkBoxUseRawMass.Checked = UserSettings.MassRecoveryUseRaw;
        }

        public WizardRecoverer(Wild.Survey data) : this()
        {
            BadData = data;
            NaturalData = new Wild.Survey();
            CategorialVariables = new DataColumn[] {
                NaturalData.Individual.InstarColumn
            };

            FillBad();
        }



        private void FillBad()
        {
            spreadSheetInit.Rows.Clear();
            TotalUnweighted = 0;

            foreach (Wild.Survey.DefinitionRow speciesRow in BadData.GetUnweightedSpecies())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetInit);
                gridRow.Cells[columnInitSpecies.Index].Value = speciesRow.Taxon;
                gridRow.Cells[columnInitCount.Index].Value = speciesRow.TotalQuantity;

                int u = speciesRow.UnweightedIndividuals();
                if (u > 0) gridRow.Cells[columnInitUnweighted.Index].Value = u;
                int a = speciesRow.AbstractIndividuals();
                if (a > 0) gridRow.Cells[columnInitAbstract.Index].Value = a;
                int t = speciesRow.Unweighted();
                if (t > 0) gridRow.Cells[columnInitUnweightedTotal.Index].Value = t;

                TotalUnweighted += t;
                spreadSheetInit.Rows.Add(gridRow);
            }

            spreadSheetInit.Sort(columnInitUnweightedTotal, ListSortDirection.Descending);
        }

        private void LoadCards()
        {
            labelLoading.Visible = progressBar1.Visible = true;
            progressBar1.Maximum = CardsToLoad.Count;
            buttonAddNatural.Visible = false;

            loaderCard.RunWorkerAsync();
        }

        private void FillExtra()
        {
            spreadSheetExtra.Rows.Clear();

            foreach (Wild.Survey.DefinitionRow speciesRow in NaturalData.Definition)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetExtra);
                gridRow.Cells[columnExtraSpecies.Index].Value = speciesRow.Taxon;
                gridRow.Cells[columnExtraCount.Index].Value = speciesRow.TotalQuantity;
                gridRow.Cells[columnExtraRaw.Index].Value = speciesRow.GetAverageMass();
                spreadSheetExtra.Rows.Add(gridRow);
            }

            spreadSheetExtra.ClearInsertedColumns();

            DataGridViewColumn lengthColumn = spreadSheetExtra.InsertColumn(
                NaturalData.Individual.LengthColumn.ColumnName,
                Explorer.Service.Localize(NaturalData.Individual.LengthColumn.ColumnName),
                typeof(int));

            foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
            {
                Wild.Survey.DefinitionRow speciesRow = NaturalData.Definition.FindByName(
                    (string)gridRow.Cells[columnExtraSpecies.Index].Value);
                if (speciesRow == null) continue;
                DataGridViewCell gridCell = gridRow.Cells[lengthColumn.Index];

                List<Wild.Survey.IndividualRow> okRows = speciesRow.GetIndividualRows().GetMeasuredRows(NaturalData.Individual.LengthColumn);

                if (okRows.Count > 0) gridCell.Value = okRows.GetCount();
            }

            foreach (DataColumn dataColumn in CategorialVariables)
            {
                DataGridViewColumn gridColumn = spreadSheetExtra.InsertColumn(
                    dataColumn.ColumnName, Explorer.Service.Localize(dataColumn.ColumnName),
                    typeof(int));

                foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
                {
                    Wild.Survey.DefinitionRow speciesRow = NaturalData.Definition.FindByName(
                        (string)gridRow.Cells[columnExtraSpecies.Index].Value);
                    if (speciesRow == null) continue;
                    DataGridViewCell gridCell = gridRow.Cells[gridColumn.Index];

                    List<Wild.Survey.IndividualRow> okRows = speciesRow.GetIndividualRows().GetMeasuredRows(dataColumn);

                    if (okRows.Count > 0) gridCell.Value = okRows.GetCount();
                }
            }

            foreach (Wild.Survey.VariableRow variableRow in NaturalData.Variable)
            {
                DataGridViewColumn gridColumn = spreadSheetExtra.InsertIconColumn(variableRow.Variable,
                    variableRow.Variable, typeof(int));

                foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
                {
                    Wild.Survey.DefinitionRow speciesRow = NaturalData.Definition.FindByName(
                        (string)gridRow.Cells[columnExtraSpecies.Index].Value);
                    if (speciesRow == null) continue;
                    Power model = NaturalData.SearchMassModel(variableRow, speciesRow.GetIndividualRows());
                    TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

                    if (model == null) continue;
                    gridCell.Tag = model;
                }
            }

            spreadSheetExtra.Sort(columnExtraCount, ListSortDirection.Descending);

            EstimateExtra();
        }

        private void EstimateExtra()
        {
            foreach (DataGridViewColumn gridColumn in spreadSheetExtra.GetInsertedColumns())
            {
                foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
                {
                    if (NaturalData.Variable.FindByVarName(gridColumn.Name) == null) continue;

                    TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];
                    if (gridCell.Tag == null) continue;
                    Regression model = (Regression)gridCell.Tag;
                    if (model == null) continue;
                    gridCell.Value = model.Data.Count;
                    //gridCell.Image = model.RSquare >= RequiredExplaination && model.Fit.GoodnessOfFit.IsPassed(TestDirection.Right) ?
                    //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
                }
            }
        }

        private void Associate()
        {
            spreadSheetAssociation.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetInit.GetVisibleRows())
            {
                DataGridViewRow gridRowCoo = new DataGridViewRow();
                gridRowCoo.CreateCells(spreadSheetAssociation);
                string species = (string)gridRow.Cells[columnInitSpecies.Index].Value;
                int q = BadData.Definition.FindByName(species).TotalQuantity;
                gridRowCoo.Cells[columnAsscSpecies.Index].Value = string.Format("{0} ({1})", species, q);
                gridRowCoo.Cells[columnAsscSpecies.Index].Tag = species;
                spreadSheetAssociation.Rows.Add(gridRowCoo);

                List<Wild.Survey.DefinitionRow> associates = new List<Wild.Survey.DefinitionRow>();

                Wild.Survey.DefinitionRow conSpecies = NaturalData.Definition.FindByName(species);

                if (conSpecies != null) {
                    associates.Add(conSpecies);
                }

                //if (checkBoxUseMemberedAssociation.Checked)
                //{
                    foreach (string associate in Service.GetAssociates(species))
                    {
                        conSpecies = NaturalData.Definition.FindByName(associate);

                        if (conSpecies != null)
                        {
                            associates.Add(conSpecies);
                        }
                    }
                //}

                if (associates.Count == 0)
                {
                    Wild.Survey.DefinitionRow conGenus = NaturalData.Definition.FindByName(Species.TaxonomicIndex.Genus(species) + " sp.");

                    if (conGenus != null)
                    {
                        associates.Add(conGenus);
                    }
                }

                gridRowCoo.Cells[columnAsscAssociate.Index].Tag =
                    associates.Count == 0 ? null : associates.ToArray();

                UpdateAssociates(gridRowCoo.Cells[columnAsscAssociate.Index]);
            }
        }

        private void UpdateAssociates(DataGridViewCell gridCell)
        {
            if (gridCell.Tag == null)
            {
                gridCell.Value = null;
            }
            else
            {
                TaxonomicIndex.TaxonRow[] speciesRows = (TaxonomicIndex.TaxonRow[])gridCell.Tag;
                if (speciesRows.Length == 0)
                {
                    gridCell.Value = null;
                    return;
                }

                string pool = string.Empty;
                string toolTip = string.Empty;

                foreach (TaxonomicIndex.TaxonRow speciesRow in speciesRows)
                {
                    pool += string.Format("{0} ({1}); ", speciesRow.Name,
                        NaturalData.GetStack().Quantity(speciesRow));

                    toolTip += string.Format("{0} ({1})\r\n", speciesRow.Name,
                        NaturalData.GetStack().Quantity(speciesRow));
                }

                gridCell.Value = pool.Substring(0, pool.Length - 2);
                gridCell.ToolTipText = toolTip.Substring(0, toolTip.Length - 2);
            }
        }

        private TaxonomicIndex.TaxonRow[] Associates(TaxonomicIndex.TaxonRow speciesRow)
        {
            foreach (DataGridViewRow gridRow in spreadSheetAssociation.Rows)
            {
                if (((string)gridRow.Cells[columnAsscSpecies.Index].Tag) == speciesRow.Name)
                {
                    if (gridRow.Cells[columnAsscAssociate.Index].Tag == null) return null;
                    return (TaxonomicIndex.TaxonRow[])gridRow.Cells[columnAsscAssociate.Index].Tag;
                }
            }

            return null;
        }

        private void RememberAssociation()
        {
            foreach (DataGridViewRow gridRow in spreadSheetAssociation.Rows)
            {
                Wild.Survey.DefinitionRow speciesRow = BadData.Definition.FindByName((string)gridRow.Cells[columnAsscSpecies.Index].Tag);
                if (speciesRow == null) continue;
                Service.SaveAssociates(speciesRow, Associates(speciesRow.KeyRecord));
            }
        }

        private void InitiatePriority()
        {
            spreadSheetPriority.ClearInsertedColumns();
            spreadSheetPriority.Rows.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetInit.GetVisibleRows())
            {
                DataGridViewRow gridRowPri = new DataGridViewRow();
                gridRowPri.CreateCells(spreadSheetPriority);
                gridRowPri.Cells[columnPriSpecies.Index].Value = gridRow.Cells[columnInitSpecies.Index].Value;
                //gridRowPri.Cells[columnPriUnweighted.Index].Value = gridRow.Cells[columnInitUnweightedTotal.Index].Value;

                spreadSheetPriority.Rows.Add(gridRowPri);
            }

            foreach (Wild.Survey.VariableRow variableRow in NaturalData.Variable)
            {
                if (BadData.Variable.FindByVarName(variableRow.Variable) == null) continue;
                DataGridViewColumn gridColumn = spreadSheetPriority.InsertIconColumn(variableRow.Variable,
                    variableRow.Variable, typeof(int));
            }

            foreach (DataColumn dataColumn in CategorialVariables)
            {
                DataGridViewColumn gridColumn = spreadSheetPriority.InsertIconColumn(
                    dataColumn.ColumnName, Explorer.Service.Localize(dataColumn.ColumnName),
                    typeof(int));
            }

            columnPriRaw.DisplayIndex = spreadSheetPriority.ColumnCount - 1;
        }

        //private void Prepare()
        //{
        //    TotalRecoverable = 0;
        //    //TotalRate = 0;

        //    foreach (DataGridViewRow gridRow in spreadSheetPriority.Rows)
        //    {
        //        // Point species
        //        //SpeciesKey.TaxonRow speciesRow = BadData.Definition.FindByName((string)gridRow.Cells[columnInitSpecies.Index].Value);
        //        SpeciesKey.TaxonRow speciesRow = Benthos.UserSettings.SpeciesIndex.Definition.FindByName((string)gridRow.Cells[columnInitSpecies.Index].Value);
        //        if (speciesRow == null) continue;

        //        // Select associates
        //        SpeciesKey.TaxonRow[] associates = Associates(speciesRow);

        //        if (associates == null) continue;

        //        // Define all corresponding individuals
        //        List<Data.IndividualRow> weightedIndividuals = new List<Data.IndividualRow>();
        //        foreach (SpeciesKey.TaxonRow associate in associates)
        //        {
        //            weightedIndividuals.AddRange(associate.GetIndividualRows());
        //        }
                
        //        double ascRate = 0;

        //        if (associates.Length == 1 && associates[0].Species == speciesRow.Species)
        //        {
        //            ascRate = 1;
        //        }
        //        else
        //        {
        //            ascRate = .9 / associates.Length;
        //        }

        //        //double recoveryRate = 0;

        //        // Define all unweighted individuals
        //        List<Data.IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

        //        // Define total unweighted
        //        int unweighted = speciesRow.UnweightedIndividuals() + speciesRow.AbstractIndividuals();
        //        int recoverable = 0;

        //        foreach (DataGridViewColumn gridColumn in spreadSheetPriority.GetUserOrderedColumns())
        //        {
        //            //#region If column is LengthColumn

        //            //if (gridColumn == columnPriLength)
        //            //{
        //            //    TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

        //            //    int measured = 0;

        //            //    foreach (Data.DefinitionRow associate in associates)
        //            //    {
        //            //        measured += NaturalData.GetStack().Measured(associate);
        //            //    }

        //            //    List<Data.IndividualRow> okRows = unweightedIndividuals.GetUnweightedAndMeasuredIndividualRows();
        //            //    int okCount = okRows.GetCount();

        //            //    if (measured == 0 || okCount == 0) goto Drop;

        //            //    List<Data.IndividualRow> natureRows = new List<Data.IndividualRow>();

        //            //    foreach (Data.DefinitionRow associate in associates)
        //            //    {
        //            //        natureRows.AddRange(associate.GetIndividualRows().GetWeightedAndMeasuredIndividualRows());
        //            //    }

        //            //    BivariateSample sample = NaturalData.GetBivariate(natureRows.ToArray(),
        //            //        NaturalData.Individual.LengthColumn, NaturalData.Individual.MassColumn);

        //            //    if (sample.Count < Mayfly.Mathematics.UserSettings.StrongSampleSize) goto Drop;

        //            //    Power model = new Power(sample);

        //            //    if (model == null) goto Drop;

        //            //    recoverable += okCount;

        //            //    gridCell.Tag = model;
        //            //    //gridCell.Value = string.Format("{0} ({1:P1})", okCount,
        //            //    //    (double)okCount / (double)unweighted);
        //            //    gridCell.Value = okCount;
        //            //    gridCell.Image = model.RSquare >= RequiredExplaination && model.Fit.GoodnessOfFit.IsPassed(TestDirection.Right) ?
        //            //        Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
        //            //    double r = model.RSquare * model.Fit.GoodnessOfFit.P(TestDirection.Left);
        //            //    gridCell.ToolTipText = string.Format(Wild.Resources.Interface.Interface.QualityTip, r);
        //            //    recoveryRate += r * okCount;

        //            //    foreach (Data.IndividualRow individualRow in okRows)
        //            //    {
        //            //        unweightedIndividuals.Remove(individualRow);
        //            //    }

        //            //    continue;

        //            //Drop:
        //            //    gridCell.Tag = null;
        //            //    gridCell.Value = null;
        //            //    gridCell.Image = null;
        //            //    gridCell.ToolTipText = string.Empty;
        //            //}

        //            //#endregion

        //            #region If column is raw mass column - all are recoverable

        //            //else
        //            if (gridColumn == columnPriRaw && columnPriRaw.Visible)
        //            {
        //                TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

        //                int weighted = 0;// NaturalData.Weighted(speciesRow);

        //                foreach (SpeciesKey.TaxonRow associate in associates)
        //                {
        //                    weighted += NaturalData.GetStack().Weighted(associate);
        //                }

        //                int okCount = unweightedIndividuals.GetCount() + speciesRow.AbstractIndividuals();

        //                if (weighted > 0 && okCount > 0)
        //                {
        //                    List<Data.IndividualRow> okRows = new List<Data.IndividualRow>();

        //                    foreach (Data.DefinitionRow associate in associates)
        //                    {
        //                        okRows.AddRange(associate.GetWeightedIndividualRows());
        //                    }

        //                    recoverable += okCount;

        //                    gridCell.Tag = okRows;
        //                    //gridCell.Value = string.Format("{0} ({1:P1})", okCount,
        //                    //    (double)okCount / (double)unweighted);
        //                    gridCell.Value = okCount;

        //                    //try
        //                    //{
        //                    //    double r = Math.Max(.1, 1 - new Sample(okRows.GetMass()).StandardDeviation);
        //                    //    //gridCell.Image = r >= RequiredExplaination ?
        //                    //    //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
        //                    //    gridCell.ToolTipText = string.Format(Wild.Resources.Interface.Interface.QualityTip, r);
        //                    //    //recoveryRate += r * okCount;
        //                    //}
        //                    //catch
        //                    //{
        //                    //    gridCell.Image = null;
        //                    //    gridCell.ToolTipText = Wild.Resources.Interface.Interface.QualityTipBad;
        //                    //    //recoveryRate += .1 * okCount;
        //                    //}

        //                    unweightedIndividuals.Clear();
        //                }
        //                else
        //                {
        //                    gridCell.Tag = null;
        //                    gridCell.Image = null;
        //                    gridCell.Value = null;
        //                    gridCell.ToolTipText = string.Empty;
        //                }
        //            }

        //            #endregion

        //            // Else If column not inserted in runtime - it doesn't fit
        //            else if (!spreadSheetPriority.GetInsertedColumns().Contains(gridColumn))
        //            {
        //                continue;
        //            }

        //            #region If column presents variableRow

        //            Data.VariableRow variableRow = NaturalData.Variable.FindByVarName(gridColumn.Name);

        //            if (variableRow != null)
        //            {
        //                TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

        //                Power model = NaturalData.SearchMassModel(variableRow, weightedIndividuals.ToArray());

        //                if (model == null) goto SkipVariable;

        //                List<Data.IndividualRow> okRows = unweightedIndividuals.GetMeasuredRows(variableRow);

        //                if (okRows.Count == 0) goto SkipVariable;

        //                int okCount = okRows.GetCount();
        //                recoverable += okCount;

        //                gridCell.Tag = model;
        //                //gridCell.Tag = okRows;
        //                //gridCell.Value = string.Format("{0} ({1:P1})", okCount, (double)okCount / (double)unweighted);
        //                gridCell.Value = okCount;
        //                //gridCell.Image = model.RSquare >= RequiredExplaination && model.Fit.GoodnessOfFit.IsPassed(TestDirection.Right) ?
        //                //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
        //                //double r = model.Determination * model.Fit.GoodnessOfFit.Probability;
        //                //gridCell.ToolTipText = string.Format(Wild.Resources.Interface.Interface.QualityTip, r);
        //                //recoveryRate += r * okCount;

        //                foreach (Data.IndividualRow individualRow in okRows)
        //                {
        //                    unweightedIndividuals.Remove(individualRow);
        //                }

        //                continue;

        //            SkipVariable:

        //                gridCell.Tag = null;
        //                gridCell.Value = null;
        //                gridCell.Image = null;
        //                gridCell.ToolTipText = string.Empty;
        //            }

        //            #endregion

        //            #region If column presents dataColumn

        //            DataColumn dataColumn = BadData.Individual.Columns[gridColumn.Name];

        //            if (dataColumn != null)
        //            {
        //                TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

        //                List<Data.IndividualRow> naturalRows = new List<Data.IndividualRow>();

        //                foreach (Data.DefinitionRow associate in associates)
        //                {
        //                    naturalRows.AddRange(associate.GetIndividualRows()
        //                        .GetMeasuredRows(NaturalData.Individual.Columns[dataColumn.ColumnName]));
        //                }

        //                List<Data.IndividualRow> okRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows,
        //                    unweightedIndividuals.GetMeasuredRows(dataColumn));

        //                if (okRows.Count > 0)
        //                {
        //                    int okCount = okRows.GetCount();
        //                    recoverable += okCount;

        //                    gridCell.Tag = naturalRows;
        //                    //gridCell.Value = string.Format("{0} ({1:P1})", okCount, (double)okCount / (double)unweighted);
        //                    gridCell.Value = okCount;

        //                    try
        //                    {
        //                        //double r = 1;
        //                        //double r1 = 1 - new Sample(naturalRows.GetMass()).CoefficientOfVariation;

        //                        foreach (object variant in dataColumn.GetValues(okRows, true))
        //                        {
        //                            List<DataRow> variantRows = naturalRows.GetRows(NaturalData.Individual.Columns[
        //                                dataColumn.ColumnName], variant.ToString());
        //                            //double r2 = Math.Max(.1, 1 - new Sample(NaturalData.Individual.
        //                            //    MassColumn.GetDoubles(variantRows)).GetVariation());
        //                            //r *= r2;
        //                        }

        //                        //gridCell.Image = r >= RequiredExplaination ?
        //                        //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
        //                        //gridCell.ToolTipText = string.Format(Wild.Resources.Interface.Interface.QualityTip, r);
        //                        //recoveryRate += r * okCount;
        //                    }
        //                    catch
        //                    {
        //                        gridCell.Image = null;
        //                        gridCell.ToolTipText = Wild.Resources.Interface.Interface.QualityTipBad;
        //                        //recoveryRate += .1 * okCount;
        //                    }

        //                    foreach (Data.IndividualRow individualRow in okRows)
        //                    {
        //                        unweightedIndividuals.Remove(individualRow);
        //                    }
        //                }
        //                else
        //                {
        //                    gridCell.Value = null;
        //                    gridCell.Image = null;
        //                    gridCell.ToolTipText = string.Empty;
        //                }
        //            }

        //            #endregion
        //        }

        //        TotalRecoverable += recoverable;
        //        //TotalRate += ascRate * recoveryRate;
        //        //recoveryRate /= recoverable;

        //        gridRow.Cells[columnPriRecoverable.Index].Value = recoverable;
        //        gridRow.Cells[columnPriRecoverableP.Index].Value = (double)recoverable / (double)unweighted;
        //        //gridRow.Cells[columnPriQuality.Index].Value = recoveryRate;
        //        //((TextAndImageCell)gridRow.Cells[columnPriQuality.Index]).Image =
        //        //    recoveryRate >= RequiredExplaination ? Mathematics.Properties.Resources.Check :
        //        //    Mathematics.Properties.Resources.None; ;
        //    }

        //    //TotalRate /= TotalRecoverable;

        //    textBoxTotalRecoverable.Text = string.Format("{0} ({1:P1})",
        //        TotalRecoverable, (double)TotalRecoverable / (double)TotalUnweighted);
        //    //textBoxRate.Text = TotalRate.ToString("P1");

        //    wizardPagePriority.AllowNext = TotalRecoverable > 0;
        //}




        private void wizardControlRecoverer_SelectedPageChanged(object sender, EventArgs e)
        { }

        private void wizardControlRecoverer_Cancelling(object sender, CancelEventArgs e)
        {
            Close();
        }

        private void wizardPageStart_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //UserSettings.MassRecoveryRequiredStrength = RequiredExplaination;
            //UserSettings.MassRecoveryRestoreAssociation = checkBoxUseMemberedAssociation.Checked;
        }

        private void buttonAddNatural_Click(object sender, EventArgs e)
        {
            if (openNatural.ShowDialog(this) == DialogResult.OK)
            {
                CardsToLoad.AddRange(IO.MaskedNames(openNatural.FileNames,
                    new string[] { SettingsReader.Interface.Extension, Wild.UserSettings.InterfaceBio.Extension }));
                LoadCards();
            }
        }

        private void loaderCard_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            //int I = CardsToLoad.Count;
            foreach (string filename in CardsToLoad)
            {
                Wild.Survey data = new Wild.Survey();
                switch (Path.GetExtension(filename))
                {
                    case ".bcd":
                        data.Read(filename);
                        break;

                    case ".bio":
                        string contents = StringCipher.Decrypt(File.ReadAllText(filename), "BIOREFERENCE");
                        Log.Write("Bio {0} is loaded.", Path.GetFileNameWithoutExtension(filename));
                        data.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
                        break;
                }

                data.CopyTo(NaturalData);


                //if (data.Card.Count > 1)
                //{
                //    int j = 0;
                //    int J = data.Card.Count;
                //    foreach (CardRow cardRow in data.Card)
                //    {
                //        cardRow.ImportTo(NaturalData.SingleCardRow);
                //        j++;
                //        ((BackgroundWorker)sender).ReportProgress((int)(100 * (double)j / (double)J));
                //    }
                //}
                //else
                //{
                //    foreach (CardRow cardRow in data.Card)
                //    {
                //        cardRow.ImportLogTo(NaturalData.SingleCardRow);
                //    }
                //}
                i++;
                //((BackgroundWorker)sender).ReportProgress((int)(100 * (double)i / (double)I));
                ((BackgroundWorker)sender).ReportProgress(i);
            }
        }

        private void loaderCard_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void loaderCard_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CardsToLoad = new List<string>();

            progressBar1.Value = 0;
            labelLoading.Visible = progressBar1.Visible = false;
            buttonAddNatural.Visible = true;

            pictureBoxCheck.Visible = labelLoaded.Visible =
                wizardPageExtraLoading.AllowNext =
                NaturalData.Card.Count > 0;

            if (NaturalData.Card.Count > 0)
            {
                buttonAddNatural.Text = Wild.Resources.Interface.Interface.MoreExtraData;
                labelLoaded.ResetFormatted(NaturalData.Definition.Count,
                    BadData.GetStack().GetSpeciesList(NaturalData.GetStack()).Count);
            }

            FillExtra();
            Associate();
        }

        private void cards_DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            string[] filenames = IO.MaskedNames(droppedNames, new string[] { Interface.Extension, Wild.UserSettings.InterfaceBio.Extension });
            CardsToLoad.AddRange(filenames);
            LoadCards();
        }

        private void cards_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void wizardPageExtraLoading_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        { }

        private void numericUpDownReqRsq_ValueChanged(object sender, EventArgs e)
        {
            EstimateExtra();
        }

        private void checkBoxUseRawMass_CheckedChanged(object sender, EventArgs e)
        {
            columnExtraRaw.Visible =
            columnPriRaw.Visible = checkBoxUseRawMass.Checked;
        }

        private void spreadSheetExtra_DoubleClick(object sender, EventArgs e)
        {
            if (spreadSheetExtra.CurrentCellAddress.X > 1)
            {
                regression_Click(spreadSheetExtra.CurrentCell, columnExtraSpecies.Index);
            }
        }

        private static void regression_Click(DataGridViewCell gridCell, int speciesColumn)
        {
            Regression regr = (Regression)gridCell.Tag;

            if (regr != null)
            {
                string species = (string)gridCell.DataGridView[speciesColumn, gridCell.RowIndex].Value;
                //string variable = gridCell.OwningColumn.Name;
                Scatterplot scatter = new Scatterplot(regr.Data, species);
                scatter.Properties.ShowTrend = true;
                scatter.Properties.SelectedApproximationType = TrendType.Power;
                scatter.Properties.ShowPredictionBands = true;
                scatter.Properties.ShowExplained = true;

                scatter.ShowOnChart();
            }
        }

        private void wizardPageExtraSpecies_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            UserSettings.MassRecoveryUseRaw = checkBoxUseRawMass.Checked;
        }

        private void spreadSheetCoordination_DoubleClick(object sender, EventArgs e)
        {
            if (spreadSheetAssociation.CurrentCellAddress.X == columnAsscAssociate.Index)
            {
                RunCoordination();
            }
        }

        private void taxonSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            SpeciesSelector taxonSelector = (SpeciesSelector)sender;
            foreach (DataGridViewCell gridCell in spreadSheetAssociation.SelectedCells)
            {
                if (gridCell.OwningColumn == columnAsscAssociate)
                {
                    gridCell.Tag = taxonSelector.SelectedSpecies;
                    UpdateAssociates(gridCell);
                }
            }
        }

        private void buttonAssiciate_Click(object sender, EventArgs e)
        {
            RunCoordination();
        }

        private void RunCoordination()
        {
            SpeciesSelector taxonSelector = new SpeciesSelector(NaturalData);
            taxonSelector.FormClosing += taxonSelector_FormClosing;

            List<Wild.Survey.DefinitionRow> tagged = new List<Wild.Survey.DefinitionRow>();
            foreach (DataGridViewCell gridCell in spreadSheetAssociation.SelectedCells)
            {
                if (gridCell.OwningColumn == columnAsscAssociate)
                {
                    if (spreadSheetAssociation.CurrentCell.Tag is Wild.Survey.DefinitionRow[] v)
                    {
                        tagged.AddRange(v);
                    }
                }
            }
            taxonSelector.SelectedSpecies = tagged.ToArray();
            taxonSelector.ShowDialog(this);

        }

        private void buttonResetCoo_Click(object sender, EventArgs e)
        {
            Associate();
        }

        private void wizardPageAssociation_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            RememberAssociation();
            InitiatePriority();
            //Prepare();
        }

        private void spreadSheetPriority_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //Prepare();
        }

        private void spreadSheetPriority_DoubleClick(object sender, EventArgs e)
        {
            if (spreadSheetPriority.CurrentCellAddress.X > 1)
            {
                regression_Click(spreadSheetPriority.CurrentCell, columnPriSpecies.Index);
            }
        }

        private void WizardPagePriority_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            wizardPageTotals.AllowNext = false;
            Cursor = Cursors.WaitCursor;
            spreadSheetPriority.Sort(columnPriSpecies, ListSortDirection.Ascending);
            backgroundRecoverer.RunWorkerAsync();
        }

        private void backgroundRecoverer_DoWork(object sender, DoWorkEventArgs e)
        {
            RecoveredIndividualRows.Clear();
            Report = new Report(Wild.Resources.Interface.MassRecover.Header);

            Report.AddSectionTitle(Wild.Resources.Interface.MassRecover.Section1Title);
            NaturalData.GetStack().AddCommon(Report);

            #region Association

            Report.AddSectionTitle(Wild.Resources.Interface.MassRecover.Section2Title);

            //// table of Associates
            //Report.Table table1 = new Report.Table(Wild.Resources.Interface.MassRecover.TableAscHeader);
            //table1.StartHeader(.35, 0, .35, 0);
            //table1.AddHeaderCell(
            //    Wild.Resources.Interface.MassRecover.ColDiet,
            //    Wild.Resources.Interface.MassRecover.ColFragments,
            //    Wild.Resources.Interface.MassRecover.ColAsc,
            //    Wild.Resources.Interface.MassRecover.ColSample);
            //table1.EndHeader();

            //foreach (SpeciesKey.TaxonRow speciesRow in BadData.Species.GetPhylogeneticallySorted(Benthos.UserSettings.SpeciesIndex))
            //{
            //    table1.StartRow();
            //    table1.AddCell(speciesRow.Species);
            //    table1.AddCellRight(BadData.GetStack().Quantity(speciesRow));

            //    SpeciesKey.TaxonRow[] associates = Associates(speciesRow);

            //    if (associates == null)
            //    {
            //        table1.AddCell(Constants.Null);
            //        table1.AddCell();
            //    }
            //    else
            //    {
            //        int associatedSample = 0;
            //        List<string> associateNames = new List<string>();
            //        foreach (SpeciesKey.TaxonRow associate in associates)
            //        {
            //            associatedSample += associate.TotalQuantity;
            //            associateNames.Add(associate.Species);
            //        }
            //        table1.AddCell(associateNames.Merge("<br>"));
            //        table1.AddCellRight(associatedSample);
            //    }
            //    table1.EndRow();
            //}

            //Report.AddTable(table1);

            #endregion

            Report.AddSectionTitle(Wild.Resources.Interface.MassRecover.Section3Title);

            //foreach (DataGridViewRow gridRow in spreadSheetPriority.Rows)
            //{
            //    Data.DefinitionRow speciesRow = BadData.Definition.FindByName((string)gridRow.Cells[columnInitSpecies.Index].Value);
            //    if (speciesRow == null) continue;

            //    Report.AddSectionTitle(speciesRow.Species);

            //    List<Data.IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

            //    int unweighted = speciesRow.Unweighted();
            //    int recovered = 0;

            //    Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecPar1,
            //        unweighted, (double)unweighted / (double)TotalUnweighted);

            //    List<string> associateNames = new List<string>();
            //    Data.DefinitionRow[] associates = Associates(speciesRow);
            //    if (associates == null)
            //    {
            //        Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParNoAsc);
            //    }
            //    else
            //    {
            //        foreach (Data.DefinitionRow associate in associates)
            //        {
            //            associateNames.Add(associate.Species);
            //        }
            //        Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParAsc,
            //            associateNames.Merge(", "));

            //        foreach (DataGridViewColumn gridColumn in spreadSheetPriority.GetUserOrderedColumns())
            //        {
            //            DataGridViewCell gridCell = gridRow.Cells[gridColumn.Index];

            //            if (gridCell.Tag == null) continue;

            //            #region Length

            //            if (gridColumn == columnPriLength)
            //            {
            //                Regression model = (Power)gridCell.Tag;

            //                List<Data.IndividualRow> okRows = new List<Data.IndividualRow>();

            //                foreach (Data.IndividualRow individualRow in unweightedIndividuals)
            //                {
            //                    if (individualRow.IsLengthNull()) continue;
            //                    individualRow.Mass = Math.Round(model.Predict(individualRow.Length), 2);
            //                    RecoveredIndividualRows.Add(individualRow);
            //                    okRows.Add(individualRow);
            //                }

            //                if (okRows.Count > 0)
            //                {
            //                    Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParLength,
            //                        okRows.GetCount(), (double)okRows.GetCount() / (double)unweighted);
            //                    Report.AddEquation(model.GetEquation("W", "L"), ",");
            //                    //report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParLength2,
            //                    //    model.Determination, model.Fit.GoodnessOfFit.Probability);

            //                    Report.Table table2 = new Report.Table(Wild.Resources.Interface.MassRecover.TableLenHeader);
            //                    table2.StartHeader(.35, 0, 0, 0, 0);
            //                    table2.AddHeaderCell(
            //                        Wild.Resources.Interface.MassRecover.ColPredator,
            //                        Wild.Resources.Reports.Caption.LengthUnit,
            //                        Wild.Resources.Interface.MassRecover.ColRecMass,
            //                        Wild.Resources.Interface.MassRecover.ColIdenticFor,
            //                        Wild.Resources.Interface.MassRecover.ColTotalMass);
            //                    table2.EndHeader();

            //                    double totalMass = 0;
            //                    int totalCount = 0;
            //                    string predator = string.Empty;

            //                    foreach (Data.IndividualRow individualRow in okRows)
            //                    {
            //                        int count = individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;

            //                        table2.StartRow();
            //                        if (individualRow.LogRow.CardRow.Comments == predator) table2.AddCellValue(Constants.RepeatedValue);
            //                        else { predator = individualRow.LogRow.CardRow.Comments; table2.AddCell(predator); }
            //                        table2.AddCellRight(individualRow.Length, 1);
            //                        table2.AddCellRight(individualRow.Mass, 5);
            //                        table2.AddCellRight(count == 1 ? string.Empty : string.Format("× {0}", count));
            //                        table2.AddCellRight(individualRow.Mass * count, 5);
            //                        table2.EndRow();

            //                        recovered += count;
            //                        totalMass += individualRow.Mass * count;
            //                        totalCount += count;
            //                    }

            //                    table2.StartFooter();
            //                    table2.AddCell();
            //                    table2.AddCell();
            //                    table2.AddCell();
            //                    table2.AddCellRight(totalCount);
            //                    table2.AddCellRight(totalMass, 2);
            //                    table2.EndFooter();

            //                    Report.AddTable(table2);
            //                }

            //                foreach (Data.IndividualRow individualRow in okRows)
            //                {
            //                    unweightedIndividuals.Remove(individualRow);
            //                }
            //            }

            //            #endregion

            //            #region Raw mass

            //            if (gridColumn == columnPriRaw && columnPriRaw.Visible)
            //            {
            //                List<Data.IndividualRow> naturalRows = (List<Data.IndividualRow>)gridCell.Tag;
            //                double avgMass = Math.Round(naturalRows.GetAverageMass(), 2);

            //                if (unweightedIndividuals.Count > 0)
            //                {
            //                    recovered += unweightedIndividuals.GetCount();
            //                    Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParRaw,
            //                        unweightedIndividuals.GetCount(),
            //                        (double)unweightedIndividuals.GetCount() / (double)unweighted,
            //                        avgMass, unweightedIndividuals.GetCount(), avgMass,
            //                        unweightedIndividuals.GetCount() * avgMass);
            //                }

            //                foreach (Data.IndividualRow individualRow in unweightedIndividuals)
            //                {
            //                    individualRow.Mass = avgMass;
            //                    RecoveredIndividualRows.Add(individualRow);
            //                }

            //                int logCount = 0;
            //                int logQuantity = 0;

            //                foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            //                {
            //                    if (logRow.GetIndividualRows().Length > 0) continue;
            //                    logRow.Mass = logRow.Quantity * avgMass;
            //                    logCount++;
            //                    logQuantity += logRow.Quantity;
            //                    recovered += logRow.Quantity;
            //                }

            //                if (logCount > 0)
            //                {
            //                    Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParAbstract,
            //                        logCount, logQuantity, logQuantity, avgMass, logQuantity * avgMass);
            //                }
            //            }

            //            #endregion

            //            if (!spreadSheetPriority.GetInsertedColumns().Contains(gridColumn)) continue;

            //            #region Variable

            //            Data.VariableRow variableRow = BadData.Variable.FindByVarName(gridColumn.Name);

            //            if (variableRow != null)
            //            {
            //                List<Data.IndividualRow> okRows = unweightedIndividuals.GetMeasuredRows(variableRow);

            //                Regression model = (Regression)gridCell.Tag;
            //                BadData.ApplyMassRecoveryModel(okRows, variableRow, model);
            //                RecoveredIndividualRows.AddRange(okRows);

            //                if (okRows.Count > 0)
            //                {
            //                    Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParVar,
            //                        okRows.GetCount(), (double)okRows.GetCount() / (double)unweighted, variableRow.Variable);
            //                    Report.AddEquation(model.GetEquation("W", "X"), ",");
            //                    //report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParVar2, variableRow.Variable,
            //                    //    model.Determination, model.Fit.GoodnessOfFit.Probability);

            //                    Report.Table table3 = new Report.Table(Wild.Resources.Interface.MassRecover.TableRecHeader, variableRow.Variable);
            //                    table3.StartHeader(.35, 0, 0, 0, 0);
            //                    table3.AddHeaderCell(
            //                        Wild.Resources.Interface.MassRecover.ColPredator,
            //                        variableRow.Variable,
            //                        Wild.Resources.Interface.MassRecover.ColRecMass,
            //                        Wild.Resources.Interface.MassRecover.ColIdenticFor,
            //                        Wild.Resources.Interface.MassRecover.ColTotalMass);
            //                    table3.EndHeader();

            //                    double totalMass = 0;
            //                    int totalCount = 0;
            //                    string predator = string.Empty;

            //                    foreach (Data.IndividualRow individualRow in okRows)
            //                    {
            //                        int count = individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;

            //                        table3.StartRow();
            //                        if (individualRow.LogRow.CardRow.Comments == predator) table3.AddCellValue(Constants.RepeatedValue);
            //                        else { predator = individualRow.LogRow.CardRow.Comments; table3.AddCell(predator); }
            //                        table3.AddCellRight(BadData.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID).Value);
            //                        table3.AddCellRight(individualRow.Mass, 5);
            //                        table3.AddCellRight(count == 1 ? string.Empty : string.Format("× {0}", count));
            //                        table3.AddCellRight(individualRow.Mass * count, 2);
            //                        table3.EndRow();

            //                        totalMass += individualRow.Mass * count;
            //                        totalCount += count;
            //                        recovered += count;
            //                    }

            //                    table3.StartFooter();
            //                    table3.AddCell();
            //                    table3.AddCell();
            //                    table3.AddCell();
            //                    table3.AddCellRight(totalCount);
            //                    table3.AddCellRight(totalMass, 2);
            //                    table3.EndFooter();
            //                    Report.AddTable(table3);
            //                }

            //                foreach (Data.IndividualRow individualRow in okRows)
            //                {
            //                    unweightedIndividuals.Remove(individualRow);
            //                }
            //            }

            //            #endregion

            //            #region Category

            //            DataColumn dataColumn = BadData.Individual.Columns[gridColumn.Name];

            //            if (dataColumn != null)
            //            {
            //                List<Data.IndividualRow> naturalRows = (List<Data.IndividualRow>)gridCell.Tag;

            //                List<Data.IndividualRow> okRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows,
            //                    unweightedIndividuals.GetMeasuredRows(dataColumn));

            //                BadData.ApplyMassRecoveryWithModelData(okRows, dataColumn, naturalRows);
            //                RecoveredIndividualRows.AddRange(okRows);

            //                if (okRows.Count > 0)
            //                {
            //                    Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParCategory,
            //                        okRows.GetCount(), (double)okRows.GetCount() / (double)unweighted,
            //                        Service.Localize(dataColumn.ColumnName));

            //                    Report.Table table4 = new Report.Table(Wild.Resources.Interface.MassRecover.TableRecHeader, Service.Localize(dataColumn.ColumnName));
            //                    table4.StartHeader(.35, 0, 0, 0, 0);
            //                    table4.AddHeaderCell(
            //                        Wild.Resources.Interface.MassRecover.ColPredator,
            //                        Service.Localize(dataColumn.ColumnName),
            //                        Wild.Resources.Interface.MassRecover.ColMeanMass,
            //                        Wild.Resources.Interface.MassRecover.ColIdenticFor,
            //                        Wild.Resources.Interface.MassRecover.ColTotalMass);
            //                    table4.EndHeader();

            //                    double totalMass = 0;
            //                    int totalCount = 0;
            //                    string predator = string.Empty;

            //                    foreach (Data.IndividualRow individualRow in okRows)
            //                    {
            //                        int count = individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;

            //                        table4.StartRow();
            //                        if (individualRow.LogRow.CardRow.Comments == predator) table4.AddCellValue(Constants.RepeatedValue);
            //                        else { predator = individualRow.LogRow.CardRow.Comments; table4.AddCell(predator); }
            //                        table4.AddCellRight(individualRow[dataColumn.ColumnName]);
            //                        table4.AddCellRight(individualRow.Mass, 5);
            //                        table4.AddCellRight(count == 1 ? string.Empty : string.Format("× {0}", count));
            //                        table4.AddCellRight(individualRow.Mass * count, 5);
            //                        table4.EndRow();

            //                        totalMass += individualRow.Mass * count;
            //                        totalCount += count;
            //                        recovered += count;
            //                    }

            //                    table4.StartFooter();
            //                    table4.AddCell();
            //                    table4.AddCell();
            //                    table4.AddCell();
            //                    table4.AddCellRight(totalCount);
            //                    table4.AddCellRight(totalMass, 2);
            //                    table4.EndFooter();
            //                    Report.AddTable(table4);
            //                }

            //                foreach (Data.IndividualRow individualRow in okRows)
            //                {
            //                    unweightedIndividuals.Remove(individualRow);
            //                }
            //            }

            //            #endregion
            //        }

            //        Report.AddParagraphClassValue(Wild.Resources.Interface.MassRecover.RecParTotal,
            //            recovered, (double)recovered / (double)unweighted);
            //    }
            //}

            //BadData.RestoreMass();

            Report.EndBranded();
        }

        //private void backgroundRecoverer_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    foreach (DataGridViewRow gridRow in spreadSheetPriority.Rows)
        //    {
        //        Data.DefinitionRow speciesRow = BadData.Definition.FindByName((string)gridRow.Cells[columnInitSpecies.Index].Value);

        //        if (speciesRow == null) continue;

        //        List<Data.IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

        //        if (Associates(speciesRow) != null)
        //        {
        //            foreach (DataGridViewColumn gridColumn in spreadSheetPriority.GetUserOrderedColumns())
        //            {
        //                DataGridViewCell gridCell = gridRow.Cells[gridColumn.Index];

        //                if (gridCell.Tag == null) continue;

        //                if (gridColumn == columnPriLength)
        //                {
        //                    Regression model = (Power)gridCell.Tag;

        //                    foreach (Data.IndividualRow individualRow in unweightedIndividuals)
        //                    {
        //                        if (individualRow.IsLengthNull()) continue;
        //                        individualRow.Mass = Math.Round(model.Predict(individualRow.Length), 5);
        //                    }
        //                }

        //                if (gridColumn == columnPriRaw && columnPriRaw.Visible)
        //                {
        //                    List<Data.IndividualRow> naturalRows = (List<Data.IndividualRow>)gridCell.Tag;
        //                    double avgMass = Math.Round(naturalRows.GetAverageMass(), 2);
        //                    foreach (Data.IndividualRow individualRow in unweightedIndividuals) { individualRow.Mass = avgMass; }
        //                }

        //                if (!spreadSheetPriority.GetInsertedColumns().Contains(gridColumn)) continue;

        //                Data.VariableRow variableRow = BadData.Variable.FindByVarName(gridColumn.Name);

        //                if (variableRow != null)
        //                {
        //                    List<Data.IndividualRow> okRows = unweightedIndividuals.GetMeasuredRows(variableRow);
        //                    Regression model = (Regression)gridCell.Tag;
        //                    BadData.ApplyMassRecoveryModel(okRows, variableRow, model);
        //                }

        //                DataColumn dataColumn = BadData.Individual.Columns[gridColumn.Name];

        //                if (dataColumn != null)
        //                {
        //                    List<Data.IndividualRow> naturalRows = (List<Data.IndividualRow>)gridCell.Tag;
        //                    List<Data.IndividualRow> okRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows, unweightedIndividuals.GetMeasuredRows(dataColumn));
        //                    BadData.ApplyMassRecoveryWithModelData(okRows, dataColumn, naturalRows);
        //                }
        //            }
        //        }
        //    }

        //    BadData.RestoreMass();
        //}

        private void backgroundRecoverer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DataRecovered != null) { DataRecovered.Invoke(this, e); }
            wizardPageTotals.AllowNext = true;
            Cursor = Cursors.Default;
        }

        private void wizardPageTotals_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            Report.Run();
        }
    }
}