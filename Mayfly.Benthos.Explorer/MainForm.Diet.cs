using Mayfly.Benthos;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Wild;
using Mayfly.Wild.Controls;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;
using Mayfly.Geographics;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        private bool DietExplorer
        {
            get; 
            set;
        }

        public event CardRowSaveEventHandler CardRowSaved;

        private SpeciesKey SpeciesIndex
        { get; set; }

        List<Data.LogRow> selectedLogRows;

        List<Data.CardRow> ChangedCards = new List<Data.CardRow>();

        private ResourceManager localizer = new ResourceManager(typeof(MainForm));

        public void SetSpeciesIndex(string indexPath)
        {
            if (indexPath != null)
            {
                SpeciesIndex = new SpeciesKey();
                SpeciesIndex.Read(indexPath);

                speciesValidator.IndexPath =
                    speciesLog.IndexPath =
                    speciesInd.IndexPath =
                    indexPath;

                comboBoxLogTaxa.AddBaseList(SpeciesIndex);
                comboBoxSpcTaxa.AddBaseList(SpeciesIndex);

                menuItemSpcTaxa.AddBaseMenus(SpeciesIndex, baseItem_Click);
                menuItemBrief.AddBaseMenus(SpeciesIndex, briefBase_Click);
            }
            else
            {
                //throw new ArgumentNullException(nameof(indexPath),
                //    "Nutrients index is absent. It was moved or deleted since last successfull launch.");

                Log.Write(EventType.ExceptionThrown,
                    "Reference {0} is absent. It was moved or deleted since last successfull launch.", indexPath);
            }
        }

        private void baseItem_Click(object sender, EventArgs e)
        {
            SpeciesKey.BaseRow baseRow = ((ToolStripMenuItem)sender).Tag as SpeciesKey.BaseRow;

            DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(baseRow.BaseName,
                baseRow.BaseName, typeof(string), 0);

            foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
            {
                if (gridRow.Cells[columnSpcSpc.Index].Value == null)
                {
                    continue;
                }

                if (gridRow.Cells[columnSpcSpc.Index].Value as string ==
                    Species.Resources.Interface.UnidentifiedTitle)
                {
                    continue;
                }

                SpeciesKey.SpeciesRow spcRow = gridRow.Cells[columnSpcSpc.Index].Value as SpeciesKey.SpeciesRow;
                SpeciesKey.TaxaRow taxaRow = spcRow.GetTaxon(baseRow);
                gridRow.Cells[gridColumn.Index].Value = (taxaRow == null) ?
                    Species.Resources.Interface.Varia : taxaRow.TaxonName;
            }
        }

        private void briefBase_Click(object sender, EventArgs e)
        {
            SpeciesKey.BaseRow baseRow = (SpeciesKey.BaseRow)((ToolStripMenuItem)sender).Tag;
            Report report = new Report(Resources.Reports.Cenosis.Title);
            FullStack.AddBrief(report, baseRow);
            report.Run();
        }

        public void PerformDietExplorer(string title)
        {
            DietExplorer = true;

            this.Text = String.Format("{0} - {1}", title, Resources.Interface.DietTitle);

            columnCardSampler.Visible =
                columnCardSubstrate.Visible =
                columnCardMesh.Visible =
                columnCardDepth.Visible = false;

            columnCardSquare.HeaderText = Resources.Interface.DietSquare;
            columnCardAbundance.HeaderText = Resources.Interface.DietAbundance;
            columnCardBiomass.HeaderText = Resources.Interface.DietBiomass;
            columnSpcAbundance.HeaderText = Resources.Interface.DietTxAbundance;
            columnSpcBiomass.HeaderText = Resources.Interface.DietTxBiomass;
            columnLogAbundance.HeaderText = Resources.Interface.DietTxAbundance;
            columnLogBiomass.HeaderText = Resources.Interface.DietTxBiomass;
        }















        //private void speciesInd_Click(object sender, EventArgs e)
        //{
        //    SpeciesKey.SpeciesRow speciesRow = (SpeciesKey.SpeciesRow)((ToolStripMenuItem)sender).Tag;
        //    LoadIndLog(speciesRow);
        //}



        //private DialogResult CheckAndSave()
        //{
        //    if (IsChanged)
        //    {
        //        TaskDialogButton b = taskDialogSave.ShowDialog(this);

        //        if (b == tdbSaveAll)
        //        {
        //            SaveCards();
        //            return DialogResult.OK;
        //        }
        //        else if (b == tdbCancelClose)
        //        {
        //            return DialogResult.Cancel;
        //        }
        //    }

        //    return DialogResult.No;
        //}

        //private void SaveCards()
        //{
        //    IsBusy = true;
        //    spreadSheetCard.StartProcessing(ChangedCards.Count, Wild.Resources.Interface.Process.CardsSaving);
        //    dataSaver.RunWorkerAsync();
        //}

        //public void LoadCards(params string[] entries)
        //{
        //    IsBusy = true;
        //    processDisplay.StartProcessing(entries.Length, Wild.Resources.Interface.Process.CardsLoading);
        //    loaderData.RunWorkerAsync(entries);
        //}


        //#region Cards


        //private void loadCards(CardStack stack)
        //{
        //    IsBusy = true;
        //    spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);
        //    spreadSheetCard.Rows.Clear();

        //    loaderCard.RunWorkerAsync(stack);
        //}

        //private void loadCards()
        //{
        //    loadCards(FullStack);
        //}


        //private bool LoadCardAddt(SpreadSheet spreadSheet)
        //{
        //    SelectionValue selectionValue = new SelectionValue(spreadSheetCard);
        //    selectionValue.Picker.UserSelectedColumns = spreadSheet.GetInsertedColumns();

        //    if (selectionValue.ShowDialog(this) != DialogResult.OK) return false;

        //    bool newInserted = false;
        //    int i = spreadSheet.InsertedColumnCount;
        //    foreach (DataGridViewColumn gridColumn in spreadSheet.GetInsertedColumns())
        //    {
        //        if (gridColumn.Name.Contains("Var")) continue;
        //        if (selectionValue.Picker.IsSelected(gridColumn)) continue;
        //        spreadSheet.Columns.Remove(gridColumn);
        //        i--;
        //    }

        //    foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns)
        //    {
        //        if (spreadSheet.GetColumn(gridColumn.Name) == null)
        //        {
        //            spreadSheet.InsertColumn(gridColumn, i, gridColumn.Name.TrimStart("columnCard".ToCharArray()));
        //            newInserted = true;
        //            i++;
        //        }
        //    }

        //    return newInserted;
        //}

        //private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns)
        //{
        //    foreach (DataGridViewColumn gridColumn in gridColumns)
        //    {
        //        if (gridColumn.Name.StartsWith("Var_")) continue;
        //        SetCardValue(cardRow, gridRow, gridColumn);
        //    }
        //}

        //private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        //{
        //    SetCardValue(cardRow, gridRow, gridColumn, gridColumn.Name);
        //}

        //private void SetCardValue(Data.CardRow cardRow, DataGridViewRow gridRow, DataGridViewColumn gridColumn, string field)
        //{
        //    try
        //    {
        //        object s = cardRow.Get(field);

        //        if (s == null) {
        //            DataGridViewRow line = GetLine(cardRow);
        //            if (line != null) gridRow.Cells[gridColumn.Index].Value = line.Cells[field].Value;
        //            else gridRow.Cells[gridColumn.Index].Value = "Error";
        //        }
        //        else switch (field)
        //        {
        //            case "Abundance":
        //                // s in (1000ind./m2) or (1000ind./g)
        //                gridRow.Cells[gridColumn.Index].Value = (double)(s) * (DietExplorer ? 1000 : 1);
        //                break;
        //            case "Biomass":
        //                // s in (g/m2) or (g/g)
        //                gridRow.Cells[gridColumn.Index].Value = (double)(s) * (DietExplorer ? 10000 : 1);
        //                break;
        //            default:
        //                gridRow.Cells[gridColumn.Index].Value = s;
        //                break;
        //        }
        //    }
        //    catch (ArgumentException)
        //    {
        //        if (spreadSheetCard.Columns[field] == null) return;
        //        DataGridViewRow cardGridRow = columnCardID.GetRow(cardRow.ID, true);
        //        if (cardGridRow == null) return;
        //        object value = cardGridRow.Cells[field].Value;
        //        if (value == null) return;
        //        gridRow.Cells[gridColumn.Index].Value = value;
        //    }

        //    //if (Data.Card.Columns[field] != null)
        //    //{
        //    //    if (cardRow.IsNull(field))
        //    //    {
        //    //        gridRow.Cells[gridColumn.Index].Value = null;
        //    //    }
        //    //    else switch (field)
        //    //        {
        //    //            case "CrossSection":
        //    //                if (cardRow.IsWaterIDNull()) gridRow.Cells[gridColumn.Index].Value = null;
        //    //                else gridRow.Cells[gridColumn.Index].Value = Wild.Service.CrossSection((WaterType)cardRow.WaterRow.Type, cardRow.CrossSection);
        //    //                break;
        //    //            case "Bank":
        //    //                gridRow.Cells[gridColumn.Index].Value = Wild.Service.Bank(cardRow.Bank);
        //    //                break;
        //    //            case "Sampler":
        //    //                gridRow.Cells[gridColumn.Index].Value = Benthos.Service.Sampler(cardRow.Sampler).ShortName;
        //    //                gridRow.Cells[gridColumn.Index].ToolTipText = Benthos.Service.Sampler(cardRow.Sampler).Sampler;
        //    //                break;
        //    //            default:
        //    //                gridRow.Cells[gridColumn.Index].Value = cardRow[field];
        //    //                break;
        //    //        }
        //    //}
        //    //else if (Data.Factor.FindByFactor(field) != null)
        //    //{
        //    //    Data.FactorValueRow factorValueRow = Data.FactorValue.FindByCardIDFactorID(cardRow.ID, Data.Factor.FindByFactor(field).ID);
        //    //    gridRow.Cells[gridColumn.Index].Value = factorValueRow == null ? double.NaN : factorValueRow.Value;
        //    //}
        //    //else
        //    //{
        //    //    switch (field)
        //    //    {
        //    //        case "Investigator":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.Investigator;
        //    //            break;
        //    //        case "Water":
        //    //            if (cardRow.IsWaterIDNull() || cardRow.WaterRow.IsWaterNull()) gridRow.Cells[gridColumn.Index].Value = null;
        //    //            else gridRow.Cells[gridColumn.Index].Value = cardRow.WaterRow.Water;
        //    //            break;
        //    //        case "Substrate":
        //    //            if (cardRow.IsSubstrateNull()) gridRow.Cells[gridColumn.Index].Value = null;
        //    //            else gridRow.Cells[gridColumn.Index].Value = cardRow.SampleSubstrate.TypeName;
        //    //            break;
        //    //        case "Wealth":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.Wealth();
        //    //            break;
        //    //        case "Quantity":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.Quantity;
        //    //            break;
        //    //        case "Abundance":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.Abundance / (DietExplorer ? 1d : 1000d);
        //    //            break;
        //    //        case "Mass":
        //    //            gridRow.Cells[gridColumn.Index].Value = Data.Mass(cardRow);
        //    //            break;
        //    //        case "Biomass":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.Biomass * (DietExplorer ? 10000d : 1d);
        //    //            break;
        //    //        case "DiversityA":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.DiversityA();
        //    //            break;
        //    //        case "DiversityB":
        //    //            gridRow.Cells[gridColumn.Index].Value = cardRow.DiversityB();
        //    //            break;
        //    //        default: // A column with custom performed data
        //    //            if (spreadSheetCard.Columns[field] == null) break;
        //    //            DataGridViewRow cardGridRow = columnCardID.GetRow(cardRow.ID, true);
        //    //            if (cardGridRow == null) break;
        //    //            object value = cardGridRow.Cells[field].Value;
        //    //            if (value == null) break;
        //    //            gridRow.Cells[gridColumn.Index].Value = value;
        //    //            break;
        //    //    }
        //    //}
        //}

        //private delegate void ValueSetEventHandler(Data.CardRow cardRow, DataGridViewRow gridRow, IEnumerable<DataGridViewColumn> gridColumns);

        //#endregion


        //private void RememberChanged(Data.CardRow cardRow)
        //{
        //    if (!ChangedCards.Contains(cardRow)) { ChangedCards.Add(cardRow); }
        //    menuItemSave.Enabled = IsChanged;
        //}





        //private void LoadCardLog()
        //{
        //    IsBusy = true;
        //    spreadSheetCard.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.CardsProcessing);

        //    spreadSheetCard.Rows.Clear();

        //    foreach (Data.FactorRow factorRow in data.Factor)
        //    {
        //        DataGridViewColumn gridColumn = spreadSheetCard.InsertColumn(factorRow.Factor, factorRow.Factor, typeof(double), spreadSheetCard.ColumnCount - 1);
        //        gridColumn.Width = gridColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.ColumnHeader, true);
        //    }

        //    loaderCard.RunWorkerAsync();
        //}

        //private CardStack GetStack(IList rows)
        //{
        //    spreadSheetCard.EndEdit();
        //    CardStack stack = new CardStack();
        //    foreach (DataGridViewRow gridRow in rows)
        //    {
        //        if (gridRow.IsNewRow) continue;
        //        stack.Add(CardRow(gridRow));
        //    }

        //    return stack;
        //}



        //private Data.CardRow CardRow(DataGridViewRow gridRow)
        //{
        //    return CardRow(gridRow, columnCardID);
        //}

        //private Data.CardRow CardRow(DataGridViewRow gridRow, DataGridViewColumn gridColumn)
        //{
        //    int id = (int)gridRow.Cells[gridColumn.Index].Value;
        //    return data.Card.FindByID(id);
        //}

        //private DataGridViewRow GetLine(Data.CardRow cardRow)
        //{
        //    return columnCardID.GetRow(cardRow.ID, true, true);
        //}

        //private void SaveCardRow(DataGridViewRow gridRow)
        //{
        //    Data.CardRow cardRow = CardRow(gridRow);

        //    if (cardRow == null) return;

        //    object Label = gridRow.Cells[columnCardLabel.Name].Value;
        //    if (Label == null) cardRow.SetLabelNull();
        //    else cardRow.Label = (string)Label;

        //    object Mesh = (object)gridRow.Cells[columnCardMesh.Name].Value;
        //    if (Mesh == null) cardRow.SetMeshNull();
        //    else cardRow.Mesh = (int)(double)Mesh;

        //    object Square = (object)gridRow.Cells[columnCardSquare.Name].Value;
        //    if (Square == null) cardRow.SetSquareNull();
        //    else cardRow.Square = (double)Square;

        //    object Depth = (object)gridRow.Cells[columnCardDepth.Name].Value;
        //    if (Depth == null) cardRow.SetDepthNull();
        //    else cardRow.Depth = (double)Depth;

        //    object Comments = gridRow.Cells[columnCardComments.Name].Value;
        //    if (Comments == null) cardRow.SetCommentsNull();
        //    else cardRow.Comments = (string)Comments;

        //    // Additional factors
        //    foreach (DataGridViewColumn gridColumn in spreadSheetCard.GetInsertedColumns())
        //    {
        //        Data.FactorRow factorRow = data.Factor.FindByFactor(gridColumn.HeaderText);
        //        if (factorRow == null) continue;
        //        object factorValue = gridRow.Cells[gridColumn.Name].Value;

        //        if (factorValue == null)
        //        {
        //            if (factorRow == null) continue;

        //            Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

        //            if (factorValueRow == null) continue;

        //            factorValueRow.Delete();
        //        }
        //        else
        //        {
        //            if (factorRow == null)
        //            {
        //                factorRow = data.Factor.AddFactorRow(gridColumn.HeaderText);
        //            }

        //            Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(cardRow.ID, factorRow.ID);

        //            if (factorValueRow == null)
        //            {
        //                data.FactorValue.AddFactorValueRow(cardRow, factorRow, (double)factorValue);
        //            }
        //            else
        //            {
        //                factorValueRow.Value = (double)factorValue;
        //            }
        //        }
        //    }

        //    // Change info in IndGrid
        //    foreach (DataGridViewRow indGridRow in IndividualRows(cardRow))
        //    {
        //        SetCardValue(cardRow, indGridRow, spreadSheetInd.GetInsertedColumns());
        //    }

        //    RememberChanged(cardRow);
        //}

        //private DataGridViewRow UpdateCardRow(Data.CardRow cardRow)
        //{
        //    DataGridViewRow result = GetLine(cardRow);

        //    SetCardValue(cardRow, result, columnCardInvestigator, "Investigator");
        //    SetCardValue(cardRow, result, columnCardWater, "Water");
        //    SetCardValue(cardRow, result, columnCardLabel, "Label");
        //    SetCardValue(cardRow, result, columnCardWhen, "When");
        //    SetCardValue(cardRow, result, columnCardWhere, "Where");
        //    SetCardValue(cardRow, result, columnCardSampler, "Sampler");
        //    SetCardValue(cardRow, result, columnCardSubstrate, "Substrate");
        //    SetCardValue(cardRow, result, columnCardMesh, "Mesh");
        //    SetCardValue(cardRow, result, columnCardSquare, "Square");
        //    SetCardValue(cardRow, result, columnCardDepth, "Depth");
        //    SetCardValue(cardRow, result, ColumnCardCrossSection, "CrossSection");
        //    SetCardValue(cardRow, result, ColumnCardBank, "Bank");
        //    SetCardValue(cardRow, result, columnCardWealth, "Wealth");
        //    SetCardValue(cardRow, result, columnCardQuantity, "Quantity");
        //    SetCardValue(cardRow, result, columnCardAbundance, "Abundance");
        //    SetCardValue(cardRow, result, columnCardMass, "Mass");
        //    SetCardValue(cardRow, result, columnCardBiomass, "Biomass");
        //    SetCardValue(cardRow, result, columnCardDiversityA, "DiversityA");
        //    SetCardValue(cardRow, result, columnCardDiversityB, "DiversityB");
        //    SetCardValue(cardRow, result, columnCardComments, "Comments");

        //    foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
        //    {
        //        SetCardValue(cardRow, result, spreadSheetCard.GetColumn(factorValueRow.FactorRow.Factor));
        //    }

        //    return result;
        //}





        //SpeciesKey.BaseRow baseSpc;

        //SpeciesKey.TaxaRow[] taxaSpc;

        //SpeciesKey.SpeciesRow[] variaSpc;



        //private void baseItem_Click(object sender, EventArgs e)
        //{
        //    SpeciesKey.BaseRow baseRow = ((ToolStripMenuItem)sender).Tag as SpeciesKey.BaseRow;

        //    DataGridViewColumn gridColumn = spreadSheetSpc.InsertColumn(baseRow.BaseName,
        //        baseRow.BaseName, typeof(string), 0);

        //    foreach (DataGridViewRow gridRow in spreadSheetSpc.Rows)
        //    {
        //        if (gridRow.Cells[columnSpcSpc.Index].Value == null)
        //        {
        //            continue;
        //        }

        //        if (gridRow.Cells[columnSpcSpc.Index].Value as string ==
        //            Species.Resources.Interface.UnidentifiedTitle)
        //        {
        //            continue;
        //        }

        //        string species = gridRow.Cells[columnSpcSpc.Index].Value as string;

        //        SpeciesKey.TaxaRow taxaRow = SpeciesIndex.GetTaxon(species, baseRow);

        //        gridRow.Cells[gridColumn.Index].Value = (taxaRow == null) ?
        //            Species.Resources.Interface.Varia : taxaRow.TaxonName;
        //    }
        //}

        //private void LoadSpc()
        //{
        //    IsBusy = true;
        //    spreadSheetSpc.StartProcessing(
        //        baseSpc == null ? data.Species.Count : (taxaSpc.Length + 1),
        //        Wild.Resources.Interface.Process.SpeciesProcessing);
        //    spreadSheetSpc.Rows.Clear();

        //    columnSpcDiversityA.Visible = (baseSpc != null);
        //    columnSpcDiversityB.Visible = (baseSpc != null);

        //    loaderSpc.RunWorkerAsync();
        //}

        //private Data.SpeciesRow GetSpcRow(DataGridViewRow gridRow)
        //{
        //    int ID = (int)gridRow.Cells[columnSpcID.Index].Value;
        //    return data.Species.FindByID(ID);
        //}

        //private DataGridViewRow GetSpcRow(Data.SpeciesRow speciesRow)
        //{
        //    return columnSpcID.GetRow(speciesRow.ID, true, true);
        //}

        //private DataGridViewRow UpdateSpeciesRow(Data.SpeciesRow speciesRow)
        //{
        //    DataGridViewRow result = GetSpcRow(speciesRow);
        //    result.Cells[columnSpcSpc.Index].Value = speciesRow.Species;
        //    return result;
        //}





        //SpeciesKey.BaseRow baseLog;

        //SpeciesKey.TaxaRow[] taxaLog;

        //SpeciesKey.SpeciesRow[] variaLog;



        //private void LoadLog()
        //{
        //    IsBusy = true;
        //    spreadSheetLog.StartProcessing(data.Card.Count, Wild.Resources.Interface.Process.SpeciesProcessing);
        //    spreadSheetLog.Rows.Clear();

        //    columnLogDiversityA.Visible = (baseLog != null);
        //    columnLogDiversityB.Visible = (baseLog != null);

        //    loaderLog.RunWorkerAsync();
        //}

        //private Data.LogRow GetLogRow(DataGridViewRow gridRow)
        //{
        //    int ID = (int)gridRow.Cells[columnLogID.Index].Value;
        //    return data.Log.FindByID(ID);
        //}

        //private DataGridViewRow GetLogRow(Data.LogRow logRow)
        //{
        //    return columnLogID.GetRow(logRow.ID, true, true);
        //}

        //private DataGridViewRow[] GetCardLogRows(Data.CardRow cardRow)
        //{
        //    List<DataGridViewRow> result = new List<DataGridViewRow>();

        //    foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
        //    {
        //        if (GetLogRow(gridRow).CardRow == cardRow)
        //        {
        //            result.Add(gridRow);
        //        }
        //    }

        //    return result.ToArray();
        //}

        ////private DataGridViewRow LogRow(SpeciesKey.TaxaRow taxaRow)
        ////{
        ////    DataGridViewRow result = new DataGridViewRow();

        ////    result.CreateCells(spreadSheetSpc);

        ////    result.Cells[columnSpcID.Index].Value = taxaRow.ID;

        ////    double Q = 0.0;
        ////    double M = 0.0;

        ////    double A = 0.0;
        ////    double B = 0.0;
        ////    List<double> Abundances = new List<double>();
        ////    List<double> Biomasses = new List<double>();

        ////    foreach (Data.LogRow logRow in data.Log)
        ////    {
        ////        if (logRow.CardRow.IsSquareNull()) continue;

        ////        if (!taxaRow.DoesInclude(logRow.SpeciesRow.Species)) continue;

        ////        if (!logRow.IsQuantityNull())
        ////        {
        ////            Q += logRow.Quantity;
        ////            A += logRow.Abundance;
        ////            Abundances.Add(logRow.Abundance);
        ////        }

        ////        if (!logRow.IsMassNull())
        ////        {
        ////            M += logRow.Mass;
        ////            double b = logRow.Biomass * (DietExplorer ? 10000d : 1d);
        ////            B += b;
        ////            Biomasses.Add(b);
        ////        }
        ////    }

        ////    //A /= Data.Card.Count;
        ////    //B /= Data.Card.Count;
        ////    //A = Math.Round(A / data.Card.Count, 3);
        ////    //B = Math.Round(B / data.Card.Count, 3);

        ////    result.Cells[columnSpcSpc.Index].Value = taxaRow.TaxonName;

        ////    if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
        ////    if (Q > 0) result.Cells[columnSpcMass.Index].Value = M;

        ////    if (A > 0) result.Cells[columnSpcAbundance.Index].Value = A;
        ////    if (B > 0) result.Cells[columnSpcBiomass.Index].Value = B;

        ////    result.Cells[columnSpcOccurrence.Index].Value = FullStack.GetOccurrence(taxaRow.GetSpecies());

        ////    result.Cells[columnSpcDiversityA.Index].Value = new Sample(Abundances).Diversity();
        ////    result.Cells[columnSpcDiversityB.Index].Value = new Sample(Biomasses).Diversity();

        ////    return result;
        ////}

        ////private DataGridViewRow LogRowVaria()
        ////{
        ////    DataGridViewRow result = new DataGridViewRow();

        ////    result.CreateCells(spreadSheetSpc);

        ////    result.Cells[columnSpcID.Index].Value = 0;

        ////    double Q = 0.0;
        ////    double M = 0.0;
        ////    double A = 0.0;
        ////    double B = 0.0;
        ////    List<double> Abundances = new List<double>();
        ////    List<double> Biomasses = new List<double>();

        ////    foreach (Data.LogRow logRow in data.Log)
        ////    {
        ////        if (logRow.CardRow.IsSquareNull()) continue;

        ////        if (!variaSpc.Contains(SpeciesIndex.Species.FindBySpecies(logRow.SpeciesRow.Species))) continue;

        ////        if (!logRow.IsQuantityNull())
        ////        {
        ////            Q += logRow.Quantity;
        ////            A += logRow.Abundance;
        ////            Abundances.Add(logRow.Abundance);
        ////        }

        ////        if (!logRow.IsMassNull())
        ////        {
        ////            M += logRow.Mass;
        ////            double b = logRow.Biomass * (DietExplorer ? 10000d : 1d);
        ////            B += b;
        ////            Biomasses.Add(b);
        ////        }
        ////    }

        ////    //A = Math.Round(A / data.Card.Count, 3);
        ////    //B = Math.Round(B / data.Card.Count, 3);

        ////    result.Cells[columnSpcSpc.Index].Value = Species.Resources.Interface.Varia;

        ////    if (Q > 0) result.Cells[columnSpcQuantity.Index].Value = Q;
        ////    if (Q > 0) result.Cells[columnSpcMass.Index].Value = M;

        ////    if (A > 0) result.Cells[columnSpcAbundance.Index].Value = A;
        ////    if (B > 0) result.Cells[columnSpcBiomass.Index].Value = B;

        ////    result.Cells[columnSpcOccurrence.Index].Value = FullStack.GetOccurrence(variaSpc);

        ////    result.Cells[columnSpcDiversityA.Index].Value = new Sample(Abundances).Diversity();
        ////    result.Cells[columnSpcDiversityB.Index].Value = new Sample(Biomasses).Diversity();

        ////    return result;
        ////}



        //private DataGridViewRow LogRow(Data.CardRow cardRow, SpeciesKey.TaxaRow taxaRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();

        //    result.CreateCells(spreadSheetLog);

        //    result.Cells[columnLogID.Index].Value = cardRow.ID; // Why?

        //    double q = 0.0;
        //    double w = 0.0;

        //    List<double> abundances = new List<double>();
        //    List<double> biomasses = new List<double>();

        //    foreach (Data.LogRow logRow in cardRow.GetLogRows())
        //    {
        //        if (logRow.CardRow.IsSquareNull()) continue;

        //        if (!taxaRow.Includes(logRow.SpeciesRow.Species)) continue;

        //        if (!logRow.IsQuantityNull())
        //        {
        //            q += logRow.Quantity;
        //            abundances.Add(logRow.GetAbundance() * (DietExplorer ? 1000 : 1));
        //        }

        //        if (!logRow.IsMassNull())
        //        {
        //            w += logRow.Mass;
        //            biomasses.Add(logRow.GetBiomass() * (DietExplorer ? 10 : 1));
        //        }
        //    }

        //    SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

        //    result.Cells[columnLogSpc.Index].Value = taxaRow.TaxonName;

        //    result.Cells[columnLogQuantity.Index].Value = (int)q;
        //    result.Cells[columnLogMass.Index].Value = w;

        //    result.Cells[columnLogAbundance.Index].Value = abundances.Sum();
        //    result.Cells[columnLogBiomass.Index].Value = biomasses.Sum();

        //    result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
        //    result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

        //    return result;
        //}

        //private DataGridViewRow LogRowVaria(Data.CardRow cardRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();

        //    result.CreateCells(spreadSheetLog);

        //    result.Cells[columnLogID.Index].Value = cardRow.ID; // Why?

        //    double q = 0.0;
        //    double w = 0.0;

        //    List<double> abundances = new List<double>();
        //    List<double> biomasses = new List<double>();

        //    foreach (Data.LogRow logRow in cardRow.GetLogRows())
        //    {
        //        if (logRow.CardRow.IsSquareNull()) continue;

        //        if (!variaLog.Contains(SpeciesIndex.Species.FindBySpecies(logRow.SpeciesRow.Species))) continue;

        //        if (!logRow.IsQuantityNull())
        //        {
        //            q += logRow.Quantity;
        //            abundances.Add(logRow.GetAbundance() * (DietExplorer ? 1000 : 1));
        //        }

        //        if (!logRow.IsMassNull())
        //        {
        //            w += logRow.Mass;
        //            biomasses.Add(logRow.GetBiomass() * (DietExplorer ? 10 : 1));
        //        }
        //    }

        //    SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());

        //    result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.Varia;

        //    result.Cells[columnLogQuantity.Index].Value = (int)q;
        //    result.Cells[columnLogMass.Index].Value = w;

        //    result.Cells[columnLogAbundance.Index].Value = abundances.Sum();
        //    result.Cells[columnLogBiomass.Index].Value = biomasses.Sum();

        //    result.Cells[columnLogDiversityA.Index].Value = new Sample(abundances).Diversity();
        //    result.Cells[columnLogDiversityB.Index].Value = new Sample(biomasses).Diversity();

        //    return result;
        //}



        //private DataGridViewRow LogRow(Data.CardRow cardRow, Data.SpeciesRow speciesRow)
        //{
        //    Data.LogRow logRow = data.Log.FindByCardIDSpcID(cardRow.ID, speciesRow.ID);

        //    if (logRow == null)
        //    {
        //        DataGridViewRow result = new DataGridViewRow();
        //        result.CreateCells(spreadSheetLog);
        //        result.Cells[columnLogSpc.Index].Value = speciesRow.Species;
        //        result.Cells[columnLogID.Index].Value = cardRow.ID; // Why?
        //        result.Cells[columnLogAbundance.Index].Value = 0D;
        //        result.Cells[columnLogBiomass.Index].Value = 0D;
        //        SetCardValue(cardRow, result, spreadSheetLog.GetInsertedColumns());
        //        return result;
        //    }
        //    else
        //    {
        //        return UpdateLogRow(logRow);
        //    }
        //}

        //private DataGridViewRow UpdateLogRow(Data.LogRow logRow)
        //{
        //    DataGridViewRow result = GetLogRow(logRow);

        //    if (logRow.IsSpcIDNull())
        //    {
        //        result.Cells[columnLogSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
        //    }
        //    else
        //    {
        //        result.Cells[columnLogSpc.Index].Value = logRow.SpeciesRow.Species;
        //    }

        //    result.Cells[columnLogQuantity.Index].Value = logRow.Quantity;
        //    result.Cells[columnLogAbundance.Index].Value = logRow.GetAbundance() * (DietExplorer ? 1000 : 1);

        //    if (!logRow.IsMassNull()) result.Cells[columnLogMass.Index].Value = logRow.Mass;
        //    result.Cells[columnLogBiomass.Index].Value = logRow.GetBiomass() * (DietExplorer ? 10 : 1);

        //    SetCardValue(logRow.CardRow, result, spreadSheetLog.GetInsertedColumns());

        //    return result;
        //}

        ////private DataGridViewRow LogRow(Data.SpeciesRow speciesRow)
        ////{
        ////    DataGridViewRow result = new DataGridViewRow();

        ////    result.CreateCells(spreadSheetSpc);

        ////    result.Cells[columnSpcID.Index].Value = speciesRow.ID;

        ////    if (speciesRow.IsSpeciesNull())
        ////    {
        ////        result.Cells[columnSpcSpc.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
        ////    }
        ////    else
        ////    {
        ////        result.Cells[columnSpcSpc.Index].Value = speciesRow.Species;
        ////    }

        ////    result.Cells[columnSpcQuantity.Index].Value = Data.Quantity(speciesRow);
        ////    result.Cells[columnSpcMass.Index].Value = Data.Mass(speciesRow);
        ////    result.Cells[columnSpcAbundance.Index].Value = Data.Abundance(speciesRow);
        ////    result.Cells[columnSpcBiomass.Index].Value = Data.Biomass(speciesRow) * (DietExplorer ? 10000d : 1d);
        ////    result.Cells[columnSpcOccurrence.Index].Value = speciesRow.Occurrence();
        ////    result.Cells[columnSpcDominance.Index].Value = speciesRow.Dominance();

        ////    return result;
        ////}






        //SpeciesKey.SpeciesRow individualSpecies;

        //private void LoadIndLog()
        //{
        //    IsBusy = true;
        //    spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
        //    spreadSheetInd.Rows.Clear();
        //    foreach (Data.VariableRow variableRow in data.Variable)
        //    {
        //        spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
        //            variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
        //    }
        //    if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
        //    loaderInd.RunWorkerAsync();
        //}

        //private void LoadIndLog(SpeciesKey.SpeciesRow speciesRow)
        //{
        //    individualSpecies = speciesRow;

        //    IsBusy = true;
        //    spreadSheetInd.StartProcessing(FullStack.Quantity(speciesRow),
        //        Wild.Resources.Interface.Process.IndividualsProcessing);
        //    spreadSheetInd.Rows.Clear();

        //    foreach (Data.VariableRow variableRow in data.Variable)
        //    {
        //        spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
        //            variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
        //    }

        //    if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
        //    loaderInd.RunWorkerAsync(speciesRow);
        //}

        //private Data.IndividualRow[] GetIndividuals(IList rows)
        //{
        //    spreadSheetInd.EndEdit();
        //    List<Data.IndividualRow> result = new List<Data.IndividualRow>();

        //    foreach (DataGridViewRow gridRow in rows)
        //    {
        //        if (!gridRow.Visible) continue;
        //        if (gridRow.IsNewRow) continue;
        //        Data.IndividualRow individualRow = IndividualRow(gridRow);
        //        if (individualRow == null) continue;

        //        result.Add(individualRow);
        //    }

        //    return result.ToArray();
        //}


        //private DataGridViewRow GetLine(Data.IndividualRow individualRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();
        //    result.CreateCells(spreadSheetInd);
        //    result.Cells[columnIndID.Index].Value = individualRow.ID;
        //    UpdateIndividualRow(result, individualRow);
        //    //SetIndividualAgeTip(result, individualRow);
        //    //SetIndividualMassTip(result, individualRow);
        //    SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());
        //    return result;
        //}


        //private DataGridViewRow FindIndividualRow(Data.IndividualRow individualRow)
        //{
        //    return columnIndID.GetRow(individualRow.ID, true, true);
        //}

        //private Data.IndividualRow IndividualRow(DataGridViewRow gridRow)
        //{
        //    if (gridRow.Cells[columnIndID.Name].Value == null) { return null; }
        //    else
        //    {
        //        int ID = (int)gridRow.Cells[columnIndID.Name].Value;
        //        return data.Individual.FindByID(ID);
        //    }
        //}

        //private Data.IndividualRow SaveIndRow(DataGridViewRow gridRow)
        //{
        //    Data.IndividualRow individualRow = IndividualRow(gridRow);

        //    if (individualRow == null) return null;

        //    object length = gridRow.Cells[columnIndLength.Name].Value;
        //    if (length == null) individualRow.SetLengthNull();
        //    else individualRow.Length = (double)length;

        //    object mass = gridRow.Cells[columnIndMass.Name].Value;
        //    if (mass == null)
        //    {
        //        individualRow.SetMassNull();
        //        individualRow.LogRow.SetMassNull();
        //    }
        //    else
        //    {
        //        if (individualRow.LogRow.IsMassNull())
        //        {
        //            individualRow.LogRow.Mass = individualRow.LogRow.DetailedMass;
        //        }
        //        else
        //        {
        //            if (!individualRow.IsMassNull()) individualRow.LogRow.Mass -= individualRow.Mass;
        //            individualRow.LogRow.Mass += (double)mass;
        //        }

        //        UpdateLogRow(individualRow.LogRow);

        //        individualRow.Mass = (double)mass;
        //    }

        //    Sex sex = (Sex)gridRow.Cells[columnIndSex.Name].Value;
        //    if (sex == null) individualRow.SetSexNull();
        //    else individualRow.Sex = sex.Value;

        //    object instar = gridRow.Cells[columnIndInstar.Name].Value;
        //    if (instar == null) individualRow.SetInstarNull();
        //    else individualRow.Instar = (int)instar;

        //    Grade grade = (Grade)gridRow.Cells[columnIndGrade.Name].Value;
        //    if (grade == null) individualRow.SetGradeNull();
        //    else individualRow.Grade = grade.Value;

        //    object comments = gridRow.Cells[columnIndComments.Name].Value;
        //    if (comments == null) individualRow.SetCommentsNull();
        //    else individualRow.Comments = (string)comments;

        //    // Additional variables
        //    foreach (DataGridViewColumn gridColumn in spreadSheetInd.GetColumns("Var_"))
        //    {
        //        Data.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

        //        object varValue = gridRow.Cells[gridColumn.Name].Value;

        //        if (varValue == null)
        //        {
        //            if (variableRow == null) continue;

        //            Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

        //            if (valueRow == null) continue;

        //            valueRow.Delete();
        //        }
        //        else
        //        {
        //            if (variableRow == null)
        //            {
        //                variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
        //            }

        //            Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

        //            if (valueRow == null)
        //            {
        //                data.Value.AddValueRow(individualRow, variableRow, (double)varValue);
        //            }
        //            else
        //            {
        //                valueRow.Value = (double)varValue;
        //            }
        //        }
        //    }

        //    RememberChanged(individualRow.LogRow.CardRow);

        //    return individualRow;
        //}

        //private void UpdateIndividualRow(Data.IndividualRow individualRow)
        //{
        //    UpdateIndividualRow(FindIndividualRow(individualRow), individualRow);
        //}

        //private DataGridViewRow UpdateIndividualRow(DataGridViewRow result, Data.IndividualRow individualRow)
        //{
        //    result.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.IsSpcIDNull() ? null : individualRow.LogRow.SpeciesRow.Species;
        //    result.Cells[ColumnIndFrequency.Index].Value = individualRow.IsFrequencyNull() ? null : (object)individualRow.Frequency;
        //    result.Cells[columnIndLength.Index].Value = individualRow.IsLengthNull() ? null : (object)individualRow.Length;
        //    result.Cells[columnIndMass.Index].Value = individualRow.IsMassNull() ? null : (object)individualRow.Mass;
        //    result.Cells[columnIndSex.Index].Value = individualRow.IsSexNull() ? null : (Sex)individualRow.Sex;
        //    result.Cells[columnIndGrade.Index].Value = individualRow.IsGradeNull() ? null : (Grade)individualRow.Grade;
        //    result.Cells[columnIndInstar.Index].Value = individualRow.IsInstarNull() ? null : (object)individualRow.Instar;
        //    result.Cells[columnIndComments.Index].Value = individualRow.IsCommentsNull() ? null : individualRow.Comments;

        //    SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());

        //    foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
        //    {
        //        if (valueRow.IsValueNull()) continue;
        //        result.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
        //    }

        //    return result;
        //}

        //private DataGridViewRow[] IndividualRows(Data.CardRow cardRow)
        //{
        //    List<DataGridViewRow> result = new List<DataGridViewRow>();

        //    foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
        //    {
        //        if (IndividualRow(gridRow).LogRow.CardRow == cardRow)
        //        {
        //            result.Add(gridRow);
        //        }
        //    }

        //    return result.ToArray();
        //}

        //private DataGridViewRow[] IndividualRows(Data.LogRow logRow)
        //{
        //    List<DataGridViewRow> result = new List<DataGridViewRow>();

        //    foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
        //    {
        //        if (IndividualRow(gridRow).LogRow == logRow)
        //        {
        //            result.Add(gridRow);
        //        }
        //    }

        //    return result.ToArray();
        //}



        //bool showArtifacts;

        //private void ShowCardArtifacts(CardArtifact[] artifacts)
        //{
        //    spreadSheetArtifactCard.Rows.Clear();
        //    int artifactCount = 0;

        //    foreach (CardArtifact artifact in artifacts)
        //    {
        //        DataGridViewRow gridRow = new DataGridViewRow();
        //        gridRow.CreateCells(spreadSheetArtifactCard);

        //        gridRow.Cells[columnArtCardName.Index].Value = artifact.Card;
        //        gridRow.Cells[columnArtCardName.Index].ToolTipText = artifact.Card.Path;

        //        spreadSheetArtifactCard.Rows.Add(gridRow);

        //        if (artifact.SamplingSquareMissing)
        //        {
        //            ((TextAndImageCell)gridRow.Cells[columnArtCardSquareMissing.Index]).Image =
        //                Mathematics.Properties.Resources.None;
        //            gridRow.Cells[columnArtCardSquareMissing.Index].ToolTipText = Resources.Artifact.SampleSquare;
        //            artifactCount++;
        //        }
        //        else
        //        {
        //            ((TextAndImageCell)gridRow.Cells[columnArtCardSquareMissing.Index]).Image =
        //                Mathematics.Properties.Resources.Check;
        //            gridRow.Cells[columnArtCardSquareMissing.Index].Value = artifact.Card.Square;
        //        }
        //    }

        //    labelArtifactCard.Visible = pictureBoxArtifactCard.Visible = artifactCount == 0;
        //    spreadSheetArtifactCard.UpdateStatus(artifactCount);
        //}

        //private void ShowSpeciesArtifacts(SpeciesArtifact[] artifacts)
        //{
        //    spreadSheetArtifactSpecies.Rows.Clear();

        //    int total = 0;

        //    foreach (SpeciesArtifact artifact in artifacts)
        //    {
        //        total += artifact.GetFacts();

        //        DataGridViewRow gridRow = new DataGridViewRow();
        //        gridRow.CreateCells(spreadSheetArtifactSpecies);
        //        gridRow.Cells[columnArtifactSpecies.Index].Value = artifact.SpeciesRow.Species;
        //        gridRow.Cells[columnArtifactN.Index].Value = artifact.Quantity;
        //        spreadSheetArtifactSpecies.Rows.Add(gridRow);

        //        if (artifact.ReferenceMissing)
        //        {
        //            gridRow.Cells[columnArtifactValidName.Index].Value = null;
        //            ((TextAndImageCell)gridRow.Cells[columnArtifactValidName.Index]).Image =
        //                Mathematics.Properties.Resources.None;
        //            gridRow.Cells[columnArtifactValidName.Index].ToolTipText =
        //                Resources.Artifact.ReferenceNull;
        //        }
        //        else
        //        {
        //            gridRow.Cells[columnArtifactValidName.Index].Value =
        //                speciesValidator.Find(artifact.SpeciesRow.Species).FullName;
        //            ((TextAndImageCell)gridRow.Cells[columnArtifactValidName.Index]).Image =
        //                Pictogram.Check;
        //        }
        //    }

        //    spreadSheetArtifactSpecies.Sort(columnArtifactSpecies, ListSortDirection.Ascending);
        //    labelArtifactSpecies.Visible = pictureBoxArtifactSpecies.Visible = total == 0;
        //    spreadSheetArtifactSpecies.UpdateStatus(total);
        //}
    }
}
