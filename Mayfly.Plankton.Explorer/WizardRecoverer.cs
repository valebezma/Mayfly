using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics.Regression;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Mayfly.Wild;
using IndividualRow = Mayfly.Wild.Data.IndividualRow;
using LogRow = Mayfly.Wild.Data.LogRow;
using SpeciesRow = Mayfly.Wild.Data.SpeciesRow;
using VariableRow = Mayfly.Wild.Data.VariableRow;

namespace Mayfly.Plankton.Explorer
{
    public partial class WizardRecoverer : Form
    {
        #region Properties

        public Data BadData;

        public Data NaturalData;

        public int TotalUnweighted;

        public double TotalRate;

        public int TotalRecoverable;

        public Report Protocol;

        public event EventHandler DataRecovered;

        public bool ShowProtocol
        {
            get { return checkBoxReport.Checked; }
            set { checkBoxReport.Checked = value; }
        }

        List<string> CardsToLoad = new List<string>();

        DataColumn[] CategorialVariables;

        #endregion



        public WizardRecoverer(Data data)
        {
            InitializeComponent();

            columnPriSpecies.ValueType = typeof(string);
            columnPriLength.ValueType = typeof(int);
            columnPriRaw.ValueType = typeof(int);
            columnPriRecoverable.ValueType = typeof(int);
            columnPriRecoverableP.ValueType = typeof(double);
            columnPriQuality.ValueType = typeof(double);

            openNatural.Filter = FileSystem.FilterFromExt(
                Plankton.UserSettings.Interface.Extension, 
                Wild.UserSettings.InterfaceBio.Extension);

            //checkBoxUseMemberedAssociation.Checked = UserSettings.MassRecoveryRestoreAssociation;
            checkBoxUseRawMass.Checked = UserSettings.MassRecoveryUseRaw;
            checkBoxReport.Checked = UserSettings.MassRecoveryProtocol;
            //RequiredExplaination = UserSettings.MassRecoveryRequiredStrength;

            BadData = data;
            NaturalData = new Data();
            CategorialVariables = new DataColumn[] { 
                NaturalData.Individual.GradeColumn 
            };

            FillBad();
        }



        #region Methods

        private void FillBad()
        {
            spreadSheetInit.Rows.Clear();
            TotalUnweighted = 0;

            foreach (SpeciesRow speciesRow in BadData.SpeciesForWeightRecovery())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetInit);
                gridRow.Cells[columnInitSpecies.Index].Value = speciesRow.Species;
                gridRow.Cells[columnInitCount.Index].Value = speciesRow.Quantity;

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

            foreach (SpeciesRow speciesRow in NaturalData.Species)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetExtra);
                gridRow.Cells[columnExtraSpecies.Index].Value = speciesRow.Species;
                gridRow.Cells[columnExtraCount.Index].Value = speciesRow.Quantity;
                gridRow.Cells[columnExtraRaw.Index].Value = speciesRow.MassAverage();
                spreadSheetExtra.Rows.Add(gridRow);
            }

            spreadSheetExtra.ClearInsertedColumns();

            DataGridViewColumn lengthColumn = spreadSheetExtra.InsertColumn(
                NaturalData.Individual.LengthColumn.ColumnName,
                Explorer.Service.Localize(NaturalData.Individual.LengthColumn.ColumnName),
                typeof(int));

            foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
            {
                SpeciesRow speciesRow = NaturalData.Species.FindBySpecies(
                    (string)gridRow.Cells[columnExtraSpecies.Index].Value);
                if (speciesRow == null) continue;
                DataGridViewCell gridCell = gridRow.Cells[lengthColumn.Index];

                List<IndividualRow> okRows = speciesRow.GetIndividualRows().GetMeasuredRows(NaturalData.Individual.LengthColumn);

                if (okRows.Count > 0) gridCell.Value = okRows.GetCount();
            }

            foreach (DataColumn dataColumn in CategorialVariables)
            {
                DataGridViewColumn gridColumn = spreadSheetExtra.InsertColumn(
                    dataColumn.ColumnName, Explorer.Service.Localize(dataColumn.ColumnName),
                    typeof(int));

                foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
                {
                    SpeciesRow speciesRow = NaturalData.Species.FindBySpecies(
                        (string)gridRow.Cells[columnExtraSpecies.Index].Value);
                    if (speciesRow == null) continue;
                    DataGridViewCell gridCell = gridRow.Cells[gridColumn.Index];

                    List<IndividualRow> okRows = speciesRow.GetIndividualRows().GetMeasuredRows(dataColumn);

                    if (okRows.Count > 0) gridCell.Value = okRows.GetCount();
                }
            }

            foreach (VariableRow variableRow in NaturalData.Variable)
            {
                DataGridViewColumn gridColumn = spreadSheetExtra.InsertIconColumn(variableRow.Variable,
                    variableRow.Variable, typeof(int));

                foreach (DataGridViewRow gridRow in spreadSheetExtra.Rows)
                {
                    SpeciesRow speciesRow = NaturalData.Species.FindBySpecies(
                        (string)gridRow.Cells[columnExtraSpecies.Index].Value);
                    if (speciesRow == null) continue;
                    PowerRegression model = NaturalData.SearchMassModel(variableRow, speciesRow.GetIndividualRows());
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
                gridRowCoo.Cells[columnAsscSpecies.Index].Value = species;
                spreadSheetAssociation.Rows.Add(gridRowCoo);

                List<SpeciesRow> associates = new List<SpeciesRow>();

                SpeciesRow conSpecies = NaturalData.Species.FindBySpecies(species);

                if (conSpecies != null)
                {
                    associates.Add(conSpecies);
                }

                //if (checkBoxUseMemberedAssociation.Checked)
                //{
                    foreach (string associate in Service.GetAssociates(species))
                    {
                        conSpecies = NaturalData.Species.FindBySpecies(associate);

                        if (conSpecies != null)
                        {
                            associates.Add(conSpecies);
                        }
                    }
                //}

                if (associates.Count == 0)
                {
                    SpeciesRow conGenus = NaturalData.Species.FindBySpecies(Species.SpeciesKey.Genus(species) + " sp.");

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
                SpeciesRow[] speciesRows = (SpeciesRow[])gridCell.Tag;
                if (speciesRows.Length == 0)
                {
                    gridCell.Value = null;
                    return;
                }

                string pool = string.Empty;
                string toolTip = string.Empty;

                foreach (SpeciesRow speciesRow in speciesRows)
                {
                    pool += string.Format("{0} ({1}); ", speciesRow.Species,
                        NaturalData.Quantity(speciesRow));

                    toolTip += string.Format("{0} ({1})\r\n", speciesRow.Species,
                        NaturalData.Quantity(speciesRow));
                }

                gridCell.Value = pool.Substring(0, pool.Length - 2);
                gridCell.ToolTipText = toolTip.Substring(0, toolTip.Length - 2);
            }
        }

        private SpeciesRow[] Associates(SpeciesRow speciesRow)
        {
            foreach (DataGridViewRow gridRow in spreadSheetAssociation.Rows)
            {
                if (((string)gridRow.Cells[columnAsscSpecies.Index].Value) == speciesRow.Species)
                {
                    if (gridRow.Cells[columnAsscAssociate.Index].Tag == null) return null;
                    return (SpeciesRow[])gridRow.Cells[columnAsscAssociate.Index].Tag;
                }
            }

            return null;
        }

        private void RememberAssociation()
        {
            foreach (DataGridViewRow gridRow in spreadSheetAssociation.Rows)
            {
                SpeciesRow speciesRow = BadData.Species.FindBySpecies((string)gridRow.Cells[columnAsscSpecies.Index].Value);
                if (speciesRow == null) continue;
                Service.SaveAssociates(speciesRow, Associates(speciesRow));
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

            foreach (VariableRow variableRow in NaturalData.Variable)
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

        private void Prepare()
        {
            TotalRecoverable = 0;
            TotalRate = 0;

            foreach (DataGridViewRow gridRow in spreadSheetPriority.Rows)
            {
                // Point species
                SpeciesRow speciesRow = BadData.Species.FindBySpecies((string)gridRow.Cells[columnInitSpecies.Index].Value);
                if (speciesRow == null) continue;

                // Select associates
                SpeciesRow[] associates = Associates(speciesRow);
                List<string> assc = new List<string>();

                if (associates == null) continue;

                // Define all corresponding individuals
                List<IndividualRow> weightedIndividuals = new List<IndividualRow>();
                foreach (SpeciesRow associate in associates)
                {
                    weightedIndividuals.AddRange(associate.GetIndividualRows());
                    assc.Add(associate.Species);
                }
                
                double ascRate = 0;

                if (associates.Length == 1 && associates[0].Species == speciesRow.Species)
                {
                    ascRate = 1;
                }
                else
                {
                    ascRate = .9 / associates.Length;
                }

                double recoveryRate = 0;

                // Define all unweighted individuals
                List<IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

                // Define total unweighted
                int unweighted = speciesRow.UnweightedIndividuals() + speciesRow.AbstractIndividuals();
                int recoverable = 0;

                foreach (DataGridViewColumn gridColumn in spreadSheetPriority.GetUserOrderedColumns())
                {
                    #region If column is LengthColumn

                    if (gridColumn == columnPriLength)
                    {
                        TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

                        List<IndividualRow> okRows = unweightedIndividuals.GetUnweightedAndMeasuredIndividualRows();
                        int okCount = okRows.GetCount();

                        if (okCount == 0) goto Drop;

                        Regression model = NaturalData.WeightModels.GetInternalScatterplot(assc.ToArray()).Regression;

                        if (model == null) goto Drop;

                        recoverable += okCount;

                        gridCell.Tag =  model;
                        gridCell.Value = okCount;
                        //gridCell.Image = model.RSquare >= RequiredExplaination && model.Fit.GoodnessOfFit.IsPassed(TestDirection.Right) ?
                        //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
                        double r = model.Determination * model.Fit.GoodnessOfFit.Probability;
                        gridCell.ToolTipText = string.Format(Wild.Resources.Interface.QualityTip, r);
                        recoveryRate += r * okCount;

                        foreach (IndividualRow individualRow in okRows)
                        {
                            unweightedIndividuals.Remove(individualRow);
                        }

                        continue;

                    Drop:
                        gridCell.Tag = null;
                        gridCell.Value = null;
                        gridCell.Image = null;
                        gridCell.ToolTipText = string.Empty;
                    }

                    #endregion

                    #region If column is raw mass column - all are recoverable

                    else if (gridColumn == columnPriRaw && columnPriRaw.Visible)
                    {
                        TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

                        int weighted = 0;// NaturalData.Weighted(speciesRow);

                        foreach (SpeciesRow associate in associates)
                        {
                            weighted += NaturalData.Weighted(associate);
                        }

                        int okCount = unweightedIndividuals.GetCount() + speciesRow.AbstractIndividuals();

                        if (weighted > 0 && okCount > 0)
                        {
                            List<IndividualRow> okRows = new List<IndividualRow>();

                            foreach (SpeciesRow associate in associates)
                            {
                                okRows.AddRange(associate.GetWeightedIndividualRows());
                            }

                            recoverable += okCount;

                            gridCell.Tag = okRows;
                            //gridCell.Value = string.Format("{0} ({1:P1})", okCount,
                            //    (double)okCount / (double)unweighted);
                            gridCell.Value = okCount;

                            try
                            {
                                double r = Math.Max(.1, 1 - new Sample(okRows.GetMass()).GetVariation());
                                //gridCell.Image = r >= RequiredExplaination ?
                                //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
                                gridCell.ToolTipText = string.Format(Wild.Resources.Interface.QualityTip, r);
                                recoveryRate += r * okCount;
                            }
                            catch
                            {
                                gridCell.Image = null;
                                gridCell.ToolTipText = Wild.Resources.Interface.QualityTipBad;
                                recoveryRate += .1 * okCount;
                            }

                            unweightedIndividuals.Clear();
                        }
                        else
                        {
                            gridCell.Tag = null;
                            gridCell.Image = null;
                            gridCell.Value = null;
                            gridCell.ToolTipText = string.Empty;
                        }
                    }

                    #endregion

                    // Else If column not inserted in runtime - it doesn't fit
                    else if (!spreadSheetPriority.GetInsertedColumns().Contains(gridColumn))
                    {
                        continue;
                    }

                    #region If column presents variableRow

                    VariableRow variableRow = NaturalData.Variable.FindByVarName(gridColumn.Name);

                    if (variableRow != null)
                    {
                        TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

                        PowerRegression model = NaturalData.SearchMassModel(variableRow, weightedIndividuals.ToArray());

                        if (model == null) goto SkipVariable;

                        List<IndividualRow> okRows = unweightedIndividuals.GetMeasuredRows(variableRow);

                        if (okRows.Count == 0) goto SkipVariable;

                        int okCount = okRows.GetCount();
                        recoverable += okCount;

                        gridCell.Tag = model;
                        //gridCell.Tag = okRows;
                        //gridCell.Value = string.Format("{0} ({1:P1})", okCount, (double)okCount / (double)unweighted);
                        gridCell.Value = okCount;
                        //gridCell.Image = model.RSquare >= RequiredExplaination && model.Fit.GoodnessOfFit.IsPassed(TestDirection.Right) ?
                        //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
                        double r = model.Determination * model.Fit.GoodnessOfFit.Probability;
                        gridCell.ToolTipText = string.Format(Wild.Resources.Interface.QualityTip, r);
                        recoveryRate += r * okCount;

                        foreach (IndividualRow individualRow in okRows)
                        {
                            unweightedIndividuals.Remove(individualRow);
                        }

                        continue;

                    SkipVariable:

                        gridCell.Tag = null;
                        gridCell.Value = null;
                        gridCell.Image = null;
                        gridCell.ToolTipText = string.Empty;
                    }

                    #endregion

                    #region If column presents dataColumn

                    DataColumn dataColumn = BadData.Individual.Columns[gridColumn.Name];

                    if (dataColumn != null)
                    {
                        TextAndImageCell gridCell = (TextAndImageCell)gridRow.Cells[gridColumn.Index];

                        List<IndividualRow> naturalRows = new List<IndividualRow>();

                        foreach (SpeciesRow associate in associates)
                        {
                            naturalRows.AddRange(associate.GetIndividualRows()
                                .GetMeasuredRows(NaturalData.Individual.Columns[dataColumn.ColumnName]));
                        }

                        List<IndividualRow> okRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows,
                            unweightedIndividuals.GetMeasuredRows(dataColumn));

                        if (okRows.Count > 0)
                        {
                            int okCount = okRows.GetCount();
                            recoverable += okCount;

                            gridCell.Tag = naturalRows;
                            //gridCell.Value = string.Format("{0} ({1:P1})", okCount, (double)okCount / (double)unweighted);
                            gridCell.Value = okCount;

                            try
                            {
                                double r = 1;
                                //double r1 = 1 - new Sample(naturalRows.GetMass()).CoefficientOfVariation;

                                foreach (object variant in dataColumn.GetValues(okRows, true))
                                {
                                    List<DataRow> variantRows = naturalRows.GetRows(NaturalData.Individual.Columns[
                                        dataColumn.ColumnName], variant.ToString());
                                    double r2 = Math.Max(.1, 1 - new Sample(NaturalData.Individual.
                                        MassColumn.GetDoubles(variantRows)).GetVariation());
                                    r *= r2;
                                }

                                //gridCell.Image = r >= RequiredExplaination ?
                                //    Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
                                gridCell.ToolTipText = string.Format(Wild.Resources.Interface.QualityTip, r);
                                recoveryRate += r * okCount;
                            }
                            catch
                            {
                                gridCell.Image = null;
                                gridCell.ToolTipText = Wild.Resources.Interface.QualityTipBad;
                                recoveryRate += .1 * okCount;
                            }

                            foreach (IndividualRow individualRow in okRows)
                            {
                                unweightedIndividuals.Remove(individualRow);
                            }
                        }
                        else
                        {
                            gridCell.Value = null;
                            gridCell.Image = null;
                            gridCell.ToolTipText = string.Empty;
                        }
                    }

                    #endregion
                }

                TotalRecoverable += recoverable;
                TotalRate += ascRate * recoveryRate;
                recoveryRate /= recoverable;

                gridRow.Cells[columnPriRecoverable.Index].Value = recoverable;
                gridRow.Cells[columnPriRecoverableP.Index].Value = (double)recoverable / (double)unweighted;
                gridRow.Cells[columnPriQuality.Index].Value = recoveryRate;
                //((TextAndImageCell)gridRow.Cells[columnPriQuality.Index]).Image =
                //    recoveryRate >= RequiredExplaination ? Mathematics.Properties.Resources.Check :
                //    Mathematics.Properties.Resources.None; ;
            }

            TotalRate /= TotalRecoverable;

            textBoxTotalRecoverable.Text = string.Format("{0} ({1:P1})",
                TotalRecoverable, (double)TotalRecoverable / (double)TotalUnweighted);
            textBoxRate.Text = TotalRate.ToString("P1");

            wizardPagePriority.AllowNext = TotalRecoverable > 0;
        }

        private void Run()
        {
            Report report = new Report(Wild.Resources.MassRecover.Header);

            // Description of NaturalData
            report.AddSubtitle(Wild.Resources.MassRecover.Section1Title);
            NaturalData.AddCommon(report);

            #region Association

            report.AddSubtitle(Wild.Resources.MassRecover.Section2Title);

            // table of Associates
            Report.Table table1 = new Report.Table(Wild.Resources.MassRecover.TableAscHeader);
            table1.StartHeader(.35, 0, .35, 0);
            table1.AddHeaderCell(
                Wild.Resources.MassRecover.ColDiet,
                Wild.Resources.MassRecover.ColFragments, 
                Wild.Resources.MassRecover.ColAsc,
                Wild.Resources.MassRecover.ColSample);
            table1.EndHeader();

            foreach (SpeciesRow speciesRow in BadData.Species.GetSorted())
            {
                table1.StartRow();
                table1.AddCell(speciesRow.Species);
                table1.AddCellRight(speciesRow.Quantity);

                SpeciesRow[] associates = Associates(speciesRow);

                if (associates == null)
                {
                    table1.AddCell(Constants.Null);
                    table1.AddCell();
                }
                else
                {
                    int associatedSample = 0;
                    List<string> associateNames = new List<string>();
                    foreach (SpeciesRow associate in associates)
                    {
                        associatedSample += associate.Quantity;
                        associateNames.Add(associate.Species);
                    }
                    table1.AddCell(associateNames.Merge("<br>"));
                    table1.AddCellRight(associatedSample);
                }
                table1.EndRow();
            }

            report.AddTable(table1);

            #endregion

            report.AddSubtitle(Wild.Resources.MassRecover.Section3Title);

            foreach (DataGridViewRow gridRow in spreadSheetPriority.Rows)
            {
                SpeciesRow speciesRow = BadData.Species.FindBySpecies((string)gridRow.Cells[columnInitSpecies.Index].Value);
                if (speciesRow == null) continue;

                report.AddSubtitle3(speciesRow.Species);

                List<IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

                int unweighted = speciesRow.Unweighted();
                int recovered = 0;

                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecPar1,
                    unweighted, (double)unweighted / (double)TotalUnweighted);

                List<string> associateNames = new List<string>();
                SpeciesRow[] associates = Associates(speciesRow);
                if (associates == null)
                {
                    report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParNoAsc);
                }
                else
                {
                    foreach (SpeciesRow associate in associates)
                    {
                        associateNames.Add(associate.Species);
                    }
                    report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParAsc, 
                        associateNames.Merge(", "));

                    foreach (DataGridViewColumn gridColumn in spreadSheetPriority.GetUserOrderedColumns())
                    {
                        DataGridViewCell gridCell = gridRow.Cells[gridColumn.Index];

                        if (gridCell.Tag == null) continue;

                        #region Length

                        if (gridColumn == columnPriLength)
                        {
                            Regression model = (PowerRegression)gridCell.Tag;

                            List<IndividualRow> okRows = new List<IndividualRow>();

                            foreach (IndividualRow individualRow in unweightedIndividuals)
                            {
                                if (individualRow.IsLengthNull()) continue;
                                individualRow.Mass = Math.Round(model.Predict(individualRow.Length), 5);
                                okRows.Add(individualRow);
                            }

                            if (okRows.Count > 0)
                            {
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParLength,
                                    okRows.GetCount(), (double)okRows.GetCount() / (double)unweighted);
                                report.AddEquation(model.GetEquation("W", "L"), ",");
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParLength2,
                                    model.Determination, model.Fit.GoodnessOfFit.Probability);

                                Report.Table table2 = new Report.Table(Wild.Resources.MassRecover.TableLenHeader);
                                table2.StartHeader(.35, 0, 0, 0, 0);
                                table2.AddHeaderCell(
                                    Wild.Resources.MassRecover.ColPredator,
                                    Wild.Resources.Common.LengthUnits,
                                    Wild.Resources.MassRecover.ColRecMass,
                                    Wild.Resources.MassRecover.ColIdenticFor,
                                    Wild.Resources.MassRecover.ColTotalMass);
                                table2.EndHeader();

                                double totalMass = 0;
                                int totalCount = 0;
                                string cardLabel = string.Empty;

                                foreach (IndividualRow individualRow in okRows)
                                {
                                    int count = individualRow.IsIdenticCountNull() ? 1 : individualRow.IdenticCount;

                                    table2.StartRow();

                                    if ((individualRow.LogRow.CardRow.IsCommentsNull() && cardLabel == string.Empty) ||
                                        individualRow.LogRow.CardRow.Comments == cardLabel)
                                    {
                                        table2.AddCellValue(Constants.RepeatedValue);
                                    }
                                    else
                                    {
                                        cardLabel = individualRow.LogRow.CardRow.IsCommentsNull() ?
                                            string.Empty : individualRow.LogRow.CardRow.Comments;
                                        table2.AddCell(cardLabel);
                                    }

                                    table2.AddCellRight(individualRow.Length, Mayfly.Service.Mask(1));
                                    table2.AddCellRight(individualRow.Mass, Mayfly.Service.Mask(5));
                                    table2.AddCellRight(count == 1 ? string.Empty : string.Format("× {0}", count));
                                    table2.AddCellRight(individualRow.Mass * count, Mayfly.Service.Mask(5));
                                    table2.EndRow();

                                    recovered += count;
                                    totalMass += individualRow.Mass * count;
                                    totalCount += count;
                                }

                                table2.StartFooter();
                                table2.AddCell();
                                table2.AddCell();
                                table2.AddCell();
                                table2.AddCellRight(totalCount);
                                table2.AddCellRight(totalMass, Mayfly.Service.Mask(2));
                                table2.EndFooter();
                                report.AddTable(table2);
                            }

                            foreach (IndividualRow individualRow in okRows)
                            {
                                unweightedIndividuals.Remove(individualRow);
                            }
                        }

                        #endregion

                        #region Raw mass

                        if (gridColumn == columnPriRaw && columnPriRaw.Visible)
                        {
                            List<IndividualRow> naturalRows = (List<IndividualRow>)gridCell.Tag;
                            double avgMass = Math.Round(naturalRows.GetAverageMass(), 2);

                            if (unweightedIndividuals.Count > 0)
                            {
                                recovered += unweightedIndividuals.GetCount();
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParRaw,
                                    unweightedIndividuals.GetCount(),
                                    (double)unweightedIndividuals.GetCount() / (double)unweighted, 
                                    avgMass, unweightedIndividuals.GetCount(), avgMass,
                                    unweightedIndividuals.GetCount() * avgMass);
                            }

                            foreach (IndividualRow individualRow in unweightedIndividuals)
                            {
                                individualRow.Mass = avgMass;
                            }

                            int logCount = 0;
                            int logQuantity = 0;

                            foreach (LogRow logRow in speciesRow.GetLogRows())
                            {
                                if (logRow.GetIndividualRows().Length > 0) continue;
                                logRow.Mass = logRow.Quantity * avgMass;
                                logCount++;
                                logQuantity += logRow.Quantity;
                                recovered += logRow.Quantity;
                            }

                            if (logCount > 0)
                            {
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParAbstract,
                                    logCount, logQuantity, logQuantity, avgMass, logQuantity * avgMass);
                            }
                        }

                        #endregion

                        if (!spreadSheetPriority.GetInsertedColumns().Contains(gridColumn)) continue;

                        #region Variable

                        VariableRow variableRow = BadData.Variable.FindByVarName(gridColumn.Name);

                        if (variableRow != null)
                        {
                            List<IndividualRow> okRows = unweightedIndividuals.GetMeasuredRows(variableRow);

                            Regression model = (Regression)gridCell.Tag;
                            BadData.ApplyMassRecoveryModel(okRows, variableRow, model);

                            if (okRows.Count > 0)
                            {
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParVar,
                                    okRows.GetCount(), (double)okRows.GetCount() / (double)unweighted, variableRow.Variable);
                                report.AddEquation(model.GetEquation("W", "X"), ",");
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParVar2, variableRow.Variable,
                                    model.Determination, model.Fit.GoodnessOfFit.Probability);

                                Report.Table table2 = new Report.Table(Wild.Resources.MassRecover.TableRecHeader, variableRow.Variable);
                                table2.StartHeader(.35, 0, 0, 0, 0);
                                table2.AddHeaderCell(
                                    Wild.Resources.MassRecover.ColPredator,
                                    variableRow.Variable, 
                                    Wild.Resources.MassRecover.ColRecMass,
                                    Wild.Resources.MassRecover.ColIdenticFor,
                                    Wild.Resources.MassRecover.ColTotalMass);
                                table2.EndHeader();

                                double totalMass = 0;
                                int totalCount = 0;
                                string predator = string.Empty;

                                foreach (IndividualRow individualRow in okRows)
                                {
                                    int count = individualRow.IsIdenticCountNull() ? 1 : individualRow.IdenticCount;

                                    table2.StartRow();
                                    if (individualRow.LogRow.CardRow.Comments == predator) table2.AddCellValue(Constants.RepeatedValue);
                                    else { predator = individualRow.LogRow.CardRow.Comments; table2.AddCell(predator); }
                                    table2.AddCellRight(BadData.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID).Value);
                                    table2.AddCellRight(individualRow.Mass, Mayfly.Service.Mask(5));
                                    table2.AddCellRight(count == 1 ? string.Empty : string.Format("× {0}", count));
                                    table2.AddCellRight(individualRow.Mass * count, Mayfly.Service.Mask(5));
                                    table2.EndRow();

                                    totalMass += individualRow.Mass * count;
                                    totalCount += count;
                                    recovered += count;
                                }

                                table2.StartFooter();
                                table2.AddCell();
                                table2.AddCell();
                                table2.AddCell();
                                table2.AddCellRight(totalCount);
                                table2.AddCellRight(totalMass, Mayfly.Service.Mask(2));
                                table2.EndFooter();
                                report.AddTable(table2);
                            }

                            foreach (IndividualRow individualRow in okRows)
                            {
                                unweightedIndividuals.Remove(individualRow);
                            }
                        }

                        #endregion

                        #region Category

                        DataColumn dataColumn = BadData.Individual.Columns[gridColumn.Name];

                        if (dataColumn != null)
                        {
                            List<IndividualRow> naturalRows = (List<IndividualRow>)gridCell.Tag;

                            List<IndividualRow> okRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows,
                                unweightedIndividuals.GetMeasuredRows(dataColumn));

                            BadData.ApplyMassRecoveryWithModelData(okRows, dataColumn, naturalRows);

                            if (okRows.Count > 0)
                            {
                                report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParCategory,
                                    okRows.GetCount(), (double)okRows.GetCount() / (double)unweighted,
                                    Service.Localize(dataColumn.ColumnName));

                                Report.Table table2 = new Report.Table(Wild.Resources.MassRecover.TableRecHeader, Service.Localize(dataColumn.ColumnName));

                                table2.StartHeader(.35, 0, 0, 0, 0);
                                table2.AddHeaderCell(
                                    Wild.Resources.MassRecover.ColPredator,
                                    Service.Localize(dataColumn.ColumnName),
                                    Wild.Resources.MassRecover.ColMeanMass,
                                    Wild.Resources.MassRecover.ColIdenticFor,
                                    Wild.Resources.MassRecover.ColTotalMass);
                                table2.EndHeader();

                                double totalMass = 0;
                                int totalCount = 0;
                                string predator = string.Empty;

                                foreach (IndividualRow individualRow in okRows)
                                {
                                    int count = individualRow.IsIdenticCountNull() ? 1 : individualRow.IdenticCount;

                                    table2.StartRow();
                                    if (individualRow.LogRow.CardRow.Comments == predator) table2.AddCellValue(Constants.RepeatedValue);
                                    else { predator = individualRow.LogRow.CardRow.Comments; table2.AddCell(predator); }
                                    table2.AddCellRight(individualRow[dataColumn.ColumnName]);
                                    table2.AddCellRight(individualRow.Mass, Mayfly.Service.Mask(5));
                                    table2.AddCellRight(count == 1 ? string.Empty : string.Format("× {0}", count));
                                    table2.AddCellRight(individualRow.Mass * count, Mayfly.Service.Mask(5));
                                    table2.EndRow();

                                    totalMass += individualRow.Mass * count;
                                    totalCount += count;
                                    recovered += count;
                                }

                                table2.StartFooter();
                                table2.AddCell();
                                table2.AddCell();
                                table2.AddCell();
                                table2.AddCellRight(totalCount);
                                table2.AddCellRight(totalMass, Mayfly.Service.Mask(2));
                                table2.EndFooter();
                                report.AddTable(table2);
                            }

                            foreach (IndividualRow individualRow in okRows)
                            {
                                unweightedIndividuals.Remove(individualRow);
                            }
                        }

                        #endregion
                    }

                    report.AddParagraphClassValue(Wild.Resources.MassRecover.RecParTotal,
                        recovered, (double)recovered / (double)unweighted);
                }
            }

            //BadData.RestoreMass();

            report.EndSign();

            Protocol = report;
        }

        #endregion



        #region Interface logics

        private void wizardControlRecoverer_SelectedPageChanged(object sender, EventArgs e)
        {
            checkBoxReport.Visible = wizardControlRecoverer.SelectedPage.IsFinishPage;
        }

        private void wizardControlRecoverer_Finished(object sender, EventArgs e)
        {
            wizardPageTotals.AllowNext = false;
            Cursor = Cursors.WaitCursor;
            spreadSheetPriority.Sort(columnPriSpecies, ListSortDirection.Ascending);
            backgroundRecoverer.RunWorkerAsync();
        }

        private void backgroundRecoverer_DoWork(object sender, DoWorkEventArgs e)
        {
            Run();
        }

        private void backgroundRecoverer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DataRecovered != null)
            {
                DataRecovered.Invoke(this, e);
            }

            wizardPageTotals.AllowNext = true;
            Cursor = Cursors.Default;

            Close();
        }

        private void wizardControlRecoverer_Cancelling(object sender, EventArgs e)
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
                CardsToLoad.AddRange(FileSystem.MaskedNames(openNatural.FileNames,
                    new string[] { Plankton.UserSettings.Interface.Extension, Wild.UserSettings.InterfaceBio.Extension }));
                LoadCards();
            }
        }

        private void loaderCard_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            //int I = CardsToLoad.Count;
            foreach (string fileName in CardsToLoad)
            {
                Data data = new Data();
                data.Read(fileName);
                data.ImportTo(NaturalData);

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
                buttonAddNatural.Text = Wild.Resources.Interface.MoreExtraData;
                labelLoaded.ResetFormatted(NaturalData.Species.Count, BadData.InCommon(NaturalData).Length);
                NaturalData.InitializeBio();
            }

            FillExtra();
            Associate();
        }

        private void cards_DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            string[] fileNames = FileSystem.MaskedNames(droppedNames, Plankton.UserSettings.Interface.Extension);
            CardsToLoad.AddRange(fileNames);
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
                string variable = gridCell.OwningColumn.Name;
                Scatterplot scatter = new Scatterplot(regr.Data, species);
                scatter.Properties.ShowTrend = true;
                scatter.Properties.SelectedApproximationType = RegressionType.Power;
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

        private void taxaSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            SpeciesSelector taxaSelector = (SpeciesSelector)sender;
            foreach (DataGridViewCell gridCell in spreadSheetAssociation.SelectedCells)
            {
                if (gridCell.OwningColumn == columnAsscAssociate)
                {
                    gridCell.Tag = taxaSelector.SelectedSpecies;
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
            SpeciesSelector taxaSelector = new SpeciesSelector(
                //spreadSheetCoordination.CurrentCell,
                NaturalData);
            taxaSelector.FormClosing += taxaSelector_FormClosing;

            List<SpeciesRow> tagged = new List<SpeciesRow>();
            foreach (DataGridViewCell gridCell in spreadSheetAssociation.SelectedCells)
            {
                if (gridCell.OwningColumn == columnAsscAssociate)
                {
                    if (spreadSheetAssociation.CurrentCell.Tag is SpeciesRow[])
                        tagged.AddRange((SpeciesRow[])spreadSheetAssociation.CurrentCell.Tag);
                }
            }
            taxaSelector.SelectedSpecies = tagged.ToArray();
            taxaSelector.ShowDialog(this);

        }

        private void buttonResetCoo_Click(object sender, EventArgs e)
        {
            Associate();
        }

        private void wizardPageAssociation_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            RememberAssociation();
            InitiatePriority();
            Prepare();
        }

        private void spreadSheetPriority_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Prepare();
        }

        private void spreadSheetPriority_DoubleClick(object sender, EventArgs e)
        {
            if (spreadSheetPriority.CurrentCellAddress.X > 1)
            {
                regression_Click(spreadSheetPriority.CurrentCell, columnPriSpecies.Index);
            }
        }

        private void wizardPageTotals_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            UserSettings.MassRecoveryProtocol = checkBoxReport.Checked;
        }

        #endregion
    }
}