using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Mathematics.Statistics.Regression;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;


namespace Mayfly.Reader
{
    public partial class Data : IBioable
    {
        partial class WaterDataTable
        {
            public WaterRow Find(WaterRow Row)
            {
                foreach (WaterRow waterRow in Rows)
                {
                    if (waterRow.GetWaterFullName() == Row.GetWaterFullName() &&
                            Row.Type == waterRow.Type)
                    {
                        return waterRow;
                    }
                }
                return null;
            }
        }

        partial class SpeciesDataTable
        {
            public SpeciesKey.TaxaRow[] Taxa(SpeciesKey.BaseRow baseRow)
            {
                List<SpeciesKey.TaxaRow> result = new List<SpeciesKey.TaxaRow>();

                foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows())
                {
                    if (IsPresented(taxaRow))
                    {
                        result.Add(taxaRow);
                    }
                }

                return result.ToArray();
            }

            public bool IsPresented(SpeciesKey.TaxaRow taxaRow)
            {
                foreach (Data.SpeciesRow speciesRow in ((Data)DataSet).Species)
                {
                    if (taxaRow.DoesInclude(speciesRow.Species)) return true;
                }

                return false;
            }

            public SpeciesRow FindBySpecies(string value)
            {
                foreach (SpeciesRow speciesRow in Rows)
                {
                    if (speciesRow.IsNull(columnSpecies))
                    {
                        continue;
                    }

                    if (speciesRow.Species == value)
                    {
                        return speciesRow;
                    }
                }
                return null;
            }

            public SpeciesRow[] GetSorted()
            {
                return (SpeciesRow[])this.Select(null, "Species asc");
            }
        }

        partial class LogDataTable
        {
            public LogRow FindByCardIDSpcID(int cardID, int spcID)
            {
                foreach (LogRow logRow in Rows)
                {
                    if (logRow.IsSpcIDNull())
                    {
                        continue;
                    }

                    if (logRow.CardID == cardID && logRow.SpcID == spcID)
                    {
                        return logRow;
                    }
                }

                return null;
            }
        }

        partial class VariableDataTable
        {
            public void Fill()
            {
                foreach (string addtVar in UserSettings.AddtVariables)
                {
                    if (FindByVarName(addtVar) == null)
                    {
                        Rows.Add(null, addtVar);
                    }
                }
            }

            public VariableRow FindByVarName(string varName)
            {
                foreach (VariableRow variableRow in Rows)
                {
                    if (variableRow.Variable == varName)
                    {
                        return variableRow;
                    }
                }
                return null;
            }

            public VariableRow[] FindByLog(LogRow logRow)
            {
                List<VariableRow> result = new List<VariableRow>();

                foreach (IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    foreach (ValueRow valueRow in individualRow.GetValueRows())
                    {
                        if (!result.Contains(valueRow.VariableRow))
                        {
                            result.Add(valueRow.VariableRow);
                        }
                    }
                }

                return result.ToArray();
            }

            public string[] Names(VariableRow[] variableRows)
            {
                List<string> result = new List<string>();

                foreach (VariableRow variableRow in variableRows)
                {
                    result.Add(variableRow.Variable);
                }

                return result.ToArray();
            }
        }

        partial class CardRow
        {
            public string Investigator { get; internal set; }

            public string Path { get; set; }


            public void ImportLogTo(Data.CardRow dstCard)
            {
                Data srcData = (Data)this.Table.DataSet;
                Data dstData = (Data)dstCard.Table.DataSet;

                foreach (Data.VariableRow variableRow in srcData.Variable)
                {
                    if (dstData.Variable.FindByVarName(variableRow.Variable) == null)
                    {
                        Data.VariableRow newVariableRow = dstData.Variable.NewVariableRow();
                        newVariableRow.Variable = variableRow.Variable;
                        dstData.Variable.AddVariableRow(newVariableRow);
                    }
                }

                foreach (Data.LogRow srcLogRow in this.GetLogRows())
                {
                    Data.LogRow dstLogRow = dstData.Log.NewLogRow();
                    dstLogRow.CardRow = dstCard;

                    if (!srcLogRow.IsSpcIDNull())
                    {
                        Data.SpeciesRow dstSpeciesRow = dstData.Species.FindBySpecies(srcLogRow.SpeciesRow.Species);
                        if (dstSpeciesRow == null)
                        {
                            dstLogRow.SpeciesRow = dstData.Species.AddSpeciesRow(srcLogRow.SpeciesRow.Species);
                        }
                        else
                        {
                            if (dstData.Log.FindByCardIDSpcID(dstCard.ID, dstSpeciesRow.ID) == null)
                            {
                                dstLogRow.SpeciesRow = dstSpeciesRow;
                            }
                            else
                            {
                                dstLogRow = dstData.Log.FindByCardIDSpcID(dstCard.ID, dstSpeciesRow.ID);
                            }
                        }
                    }

                    if (!srcLogRow.IsQuantityNull())
                    {
                        if (dstLogRow.IsQuantityNull())
                        {
                            dstLogRow.Quantity = srcLogRow.Quantity;
                        }
                        else
                        {
                            dstLogRow.Quantity += srcLogRow.Quantity;
                        }
                    }

                    if (!srcLogRow.IsMassNull())
                    {
                        if (dstLogRow.IsMassNull())
                        {
                            dstLogRow.Mass = srcLogRow.Mass;
                        }
                        else
                        {
                            dstLogRow.Mass += srcLogRow.Mass;
                        }
                    }

                    if (!srcLogRow.IsIntervalNull())
                    {
                        if (dstLogRow.IsIntervalNull())
                        {
                            dstLogRow.Interval = srcLogRow.Interval;
                        }
                        else
                        {
                            //
                        }
                    }

                    if (!srcLogRow.IsCommentsNull())
                    {
                        if (dstLogRow.IsCommentsNull())
                        {
                            dstLogRow.Comments = srcLogRow.Comments;
                        }
                        else
                        {
                            dstLogRow.Comments += Constants.Return + srcLogRow.Comments;
                        }
                    }

                    try
                    {
                        dstData.Log.AddLogRow(dstLogRow);
                    }
                    catch { }

                    foreach (Data.IndividualRow individualRow in srcLogRow.GetIndividualRows())
                    {
                        Data.IndividualRow newIndividualRow = dstData.Individual.NewIndividualRow();
                        newIndividualRow.LogRow = dstLogRow;
                        if (!individualRow.IsLengthNull()) newIndividualRow.Length = individualRow.Length;
                        if (!individualRow.IsMassNull()) newIndividualRow.Mass = individualRow.Mass;
                        if (!individualRow.IsSexNull()) newIndividualRow.Sex = individualRow.Sex;
                        if (!individualRow.IsIdenticCountNull()) newIndividualRow.IdenticCount = individualRow.IdenticCount;
                        if (!individualRow.IsCommentsNull()) newIndividualRow.Comments = individualRow.Comments;
                        dstData.Individual.AddIndividualRow(newIndividualRow);

                        foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
                        {
                            Data.ValueRow newValueRow = dstData.Value.NewValueRow();
                            newValueRow.IndividualRow = newIndividualRow;
                            newValueRow.VariableRow = dstData.Variable.FindByVarName(valueRow.VariableRow.Variable);
                            newValueRow.Value = valueRow.Value;
                            dstData.Value.AddValueRow(newValueRow);
                        }
                    }
                }
            }

            public void AttachSign()
            {
                this.Sign = StringCipher.Encrypt(Mayfly.UserSettings.Username,
                    this.When.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
                this.Investigator = Mayfly.UserSettings.Username;
            }

            internal void RenewSign(DateTime newDateValue)
            {
                string owner = this.Investigator;

                if (owner == Mayfly.Resources.Interface.InvestigatorNotApproved)
                {
                    this.SetSignNull();
                    this.Investigator = null;
                    return;
                }
                else
                {
                    this.When = newDateValue;
                    this.Sign = StringCipher.Encrypt(owner,
                        this.When.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
                }
            }


            public Waypoint Position
            {
                get
                {
                    return this.IsWhereNull() ? null : new Waypoint(this.Where);
                }
            }


            public int Quantity
            {
                get
                {
                    int result = 0;

                    Data data = (Data)this.tableCard.DataSet;

                    foreach (LogRow logRow in this.GetLogRows())
                    {
                        if (logRow.IsQuantityNull())
                        {
                            logRow.Quantity = logRow.GetQuantity();
                        }

                        result += logRow.Quantity;
                    }

                    return result;
                }
            }

            public double Mass
            {
                get
                {
                    double result = 0;

                    foreach (LogRow logRow in this.GetLogRows())
                    {
                        if (logRow.IsMassNull()) return double.NaN;

                        result += logRow.Mass;
                    }

                    return result;
                }
            }
        }

        partial class LogRow
        {
            public int GetQuantity()
            {
                int result = 0;

                foreach (IndividualRow individualRow in this.GetIndividualRows())
                {
                    result += individualRow.IsIdenticCountNull() ? 1 : individualRow.IdenticCount;
                }

                return result;
            }

            public double GetMass()
            {
                int total = this.Quantity;

                int undefinedRecords = 0;
                int definedRecords = 0;

                bool fifthWheel = false;

                foreach (IndividualRow individualRow in this.GetIndividualRows())
                {
                    if (individualRow.IsIdenticCountNull())
                    {
                        undefinedRecords++;
                    }
                    else
                    {
                        definedRecords += individualRow.IdenticCount;
                    }
                }

                double undefinedCost = (double)(total - definedRecords) / (double)undefinedRecords;

                if (total > 0 && definedRecords + undefinedRecords == 0)
                    return double.NaN;

                if (undefinedCost > (int)undefinedCost)
                {
                    fifthWheel = true;
                    undefinedCost = (int)undefinedCost;
                }

                double result = 0;

                foreach (IndividualRow individualRow in this.GetIndividualRows())
                {
                    double quantity = individualRow.IsIdenticCountNull() ?
                        undefinedCost : individualRow.IdenticCount;

                    if (individualRow.IsIdenticCountNull() && fifthWheel)
                    {
                        quantity++;
                        fifthWheel = false;
                    }

                    if (individualRow.IsMassNull())
                    {
                        if (individualRow.IsLengthNull())
                        {
                            return double.NaN;
                        }

                        double meanWeight = ((Data)this.Table.DataSet).WeightModels.GetValue(this.SpeciesRow.Species, individualRow.Length);

                        if (double.IsNaN(meanWeight))
                        {
                            return double.NaN;
                        }
                        else
                        {
                            result += quantity * meanWeight;
                        }
                    }
                    else
                    {
                        result += quantity * individualRow.Mass;
                    }
                }

                return result;
            }
        }

        partial class SpeciesRow
        {
            public IndividualRow[] GetIndividualRows()
            {
                List<IndividualRow> result = new List<IndividualRow>();

                foreach (LogRow logRow in this.GetLogRows())
                {
                    result.AddRange(logRow.GetIndividualRows());
                }

                return result.ToArray();
            }


            public string FullName
            {
                get
                {
                    SpeciesKey.SpeciesRow refEntry = UserSettings.SpeciesIndex.Species.FindBySpecies(this.Species);
                    return refEntry == null ? this.Species : refEntry.GetFullName();
                }
            }

            public string ReportFullPresentation
            {
                get
                {
                    SpeciesKey.SpeciesRow refEntry = UserSettings.SpeciesIndex.Species.FindBySpecies(this.Species);
                    return refEntry == null ? this.Species : refEntry.ReportFullPresentation;
                }
            }


            public int Quantity
            {
                get
                {
                    int result = 0;

                    foreach (LogRow logRow in this.GetLogRows())
                    {
                        if (logRow.IsQuantityNull())
                        {
                            logRow.Quantity = logRow.GetQuantity();
                        }

                        result += logRow.Quantity;
                    }

                    return result;
                }
            }

            public double Mass
            {
                get
                {
                    double result = 0.0;

                    foreach (Data.LogRow logRow in this.GetLogRows())
                    {
                        double w = logRow.GetMass();

                        if (double.IsNaN(w))
                        {
                            return double.NaN;
                        }
                        else
                        {
                            result += w;
                        }
                    }

                    return result;
                }
            }


            public int GetQuantity(double lengthClass)
            {
                int result = 0;

                foreach (LogRow logRow in this.GetLogRows())
                {
                    foreach (IndividualRow individualRow in logRow.GetIndividualRows())
                    {
                        if (individualRow.IsLengthNull()) continue;
                        if (Service.GetStrate(individualRow.Length) != lengthClass) continue;
                        result++;
                    }
                }

                return result;
            }

            public double MassAverage()
            {
                double result = 0;
                int divider = 0;

                foreach (Data.IndividualRow individualRow in GetIndividualRows())
                {
                    if (individualRow.IsMassNull()) continue;
                    result += individualRow.Mass;
                    divider++;
                }

                if (divider > 0) // There are some weighted individuals of given length class
                {
                    return result / divider;
                }
                else
                {
                    return double.NaN;
                }
            }
        }

        partial class IndividualRow
        {
            public double RecoveredMass { get; set; }

            public string Species
            {
                get
                {
                    return this.LogRow.SpeciesRow.Species;
                }
            }

            public Data.IndividualRow CopyIndividualData(Data.LogRow dstLogRow)
            {
                Data dstData = (Data)dstLogRow.Table.DataSet;
                Data.IndividualRow newIndividualRow = dstData.Individual.NewIndividualRow();

                if (!this.IsCommentsNull())
                    newIndividualRow.Comments = this.Comments;

                if (!this.IsLengthNull())
                    newIndividualRow.Length = this.Length;

                if (!this.IsMassNull())
                    newIndividualRow.Mass = this.Mass;

                if (!this.IsSexNull())
                    newIndividualRow.Sex = this.Sex;

                newIndividualRow.LogRow = dstLogRow;
                dstData.Individual.AddIndividualRow(newIndividualRow);

                foreach (ValueRow valueRow in this.GetValueRows())
                {
                    ValueRow newValueRow = dstData.Value.NewValueRow();
                    newValueRow.IndividualRow = newIndividualRow;

                    if (dstData.Variable.FindByVarName(valueRow.VariableRow.Variable) == null)
                    {
                        VariableRow newVariableRow = dstData.Variable.NewVariableRow();
                        newVariableRow.Variable = valueRow.VariableRow.Variable;
                        dstData.Variable.AddVariableRow(newVariableRow);
                    }

                    newValueRow.VariableRow = dstData.Variable.FindByVarName(valueRow.VariableRow.Variable);
                    newValueRow.Value = valueRow.Value;
                    dstData.Value.AddValueRow(newValueRow);
                }

                return newIndividualRow;
            }
        }



        #region IO

        public string SuggestedName
        {
            get
            {
                return SuggestName(UserSettings.Interface.Extension);
            }
        }

        public string SuggestName(string extension)
        {
            if (Card.Count == 1)
            {
                CardRow cardRow = Card[0];

                string result = string.Empty;

                if (!cardRow.IsLabelNull())
                {
                    result += cardRow.Label + " ";
                }

                if (!cardRow.IsWhenNull())
                {
                    result += cardRow.When.ToString("yyyy-MM-dd") + " ";
                }

                foreach (FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
                {
                    result += string.Format(Constants.TwoWordsMask, factorValueRow.FactorRow.Factor, factorValueRow.Value) + " ";
                }

                if (!cardRow.IsWaterIDNull())
                {
                    result += cardRow.WaterRow.GetWaterFullName() + " ";
                }

                return result.Trim() + extension;
            }
            else
            {
                return string.Empty;
            }
        }

        public bool Read(string fileName)
        {
            try
            {
                base.ReadXml(fileName);

                this.Solitary.Path = fileName;

                try
                {
                    this.Solitary.Investigator = Mayfly.StringCipher.Decrypt(
                        this.Solitary.Sign, this.Solitary.When.ToString("s"));
                }
                catch
                {
                    this.Solitary.Investigator = Mayfly.Resources.Interface.InvestigatorNotApproved;
                }

                return Card.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public void SaveToFile(string fileName)
        {
            File.WriteAllText(fileName, GetXml());
        }

        public static Data FromClipboard()
        {
            Data data = new Data();

            data.ReadXml(new StringReader(Clipboard.GetText()));

            try
            {
                data.Solitary.Investigator = Mayfly.StringCipher.Decrypt(
                    data.Solitary.Sign, data.Solitary.When.ToString("s"));
            }
            catch
            {
                data.Solitary.Investigator = Mayfly.Resources.Interface.InvestigatorNotApproved;
            }

            return data;
        }

        #endregion



        public Data.CardRow Solitary
        {
            get
            {
                if (Card.Rows.Count == 0)
                {
                    CardRow result = Card.NewCardRow();
                    Card.AddCardRow(result);
                    return result;
                }
                else
                {
                    return Card[0];
                }
            }
        }

        public void ClearUseless()
        {
            for (int i = 0; i < Water.Count; i++)
            {
                if (Water[i].GetCardRows().Count() == 0)
                {
                    Water.RemoveWaterRow(Water[i]);
                    i--;
                }
            }

            for (int i = 0; i < Species.Count; i++)
            {
                if (Species[i].GetLogRows().Count() == 0)
                {
                    Species.RemoveSpeciesRow(Species[i]);
                    i--;
                }
            }

            for (int i = 0; i < Value.Count; i++)
            {
                if (Value[i].IndividualRow == null)
                {
                    Value.RemoveValueRow(Value[i]);
                    i--;
                }
            }

            for (int i = 0; i < Variable.Count; i++)
            {
                if (Variable[i].GetValueRows().Count() == 0)
                {
                    Variable.RemoveVariableRow(Variable[i]);
                    i--;
                }
            }
        }



        #region Check content

        private bool IsDataPresented(DataColumn dataColumn, IndividualRow[] individualRows)
        {
            foreach (IndividualRow individualRow in individualRows)
            {
                if (!individualRow.IsNull(dataColumn)) return true;
            }
            return false;
        }

        public static bool ContainsLog(string text)
        {
            try
            {
                Data data = new Data();
                data.ReadXml(new StringReader(text));
                return data.Log.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public static bool ContainsIndividuals(string text)
        {
            try
            {
                Data data = new Data();
                data.ReadXml(new StringReader(text));
                return data.Individual.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion



        public new Data Copy()
        {
            Data result = (Data)((DataSet)this).Copy();
            result.InitializeBio();
            result.RefreshBios();
            return result;
        }



        #region Bios

        public RegressiveBio WeightModels;

        public DataRow[] GetBioRows(string species)
        {
            return Species.FindBySpecies(species).GetIndividualRows();
        }

        public double GetIndividualValue(DataRow dataRow, string field)
        {
            IndividualRow individualRow = (IndividualRow)dataRow;

            if (this.Individual.Columns[field] != null)
            {
                return (double)individualRow[field];
            }
            else if (this.Variable.FindByVarName(field) != null)
            {
                return (double)this.Value.FindByIndIDVarID(individualRow.ID, this.Variable.FindByVarName(field).ID).Value;
            }
            else
            {
                return double.NaN;
            }
        }

        public void InitializeBio()
        {
            WeightModels = new RegressiveBio(
                this, Species.Select(), Individual.LengthColumn, Individual.MassColumn, RegressionType.Power);
            WeightModels.DisplayNameY = Resources.Common.MassUnits;
        }

        public void RefreshBios()
        {
            WeightModels.Refresh();
        }

        public IBio GetBio(string species, string x, string y)
        {
            return null;
        }

        public string[] GetAuthors()
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (string.IsNullOrWhiteSpace(cardRow.Investigator)) continue;
                string investigator = cardRow.Investigator;
                if (result.Contains(investigator)) continue;
                result.Add(investigator);
            }
            return result.ToArray();
        }

        public string[] GetPlaces()
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.IsWaterIDNull()) continue;
                string waterDescription = cardRow.WaterRow.GetWaterFullName();
                if (result.Contains(waterDescription)) continue;
                result.Add(waterDescription);
            }
            return result.ToArray();
        }

        public DateTime[] GetDates()
        {
            List<DateTime> result = new List<DateTime>();
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.IsWhenNull()) continue;
                if (result.Contains(cardRow.When.Date)) continue;
                result.Add(cardRow.When.Date);
            }
            result.Sort();
            return result.ToArray();
        }

        #endregion

        //#region Constructing report

        //public Report HTML(string title)
        //{
        //    if (Card.Count == 1)
        //    {
        //        return HTML(Card[0], title);
        //    }
        //    else
        //    {
        //        return HTML(Card.NewCardRow(), title);
        //    }
        //}

        //public Report HTML(CardRow cardRow, string title)
        //{
        //    if (title == null) title = string.Format(
        //        "<span class='pretitle'>{0}.</span> [<span style='float:right'>]</span>",
        //        FileSystem.GetFriendlyFiletypeName(UserSettings.Interface.Extension));
        //    Report report = new Report(title);
        //    AddHTML(report, cardRow, CardReport.IncludeIndividualProfiles);
        //    report.End("© {0} {1}", cardRow.When.Year, cardRow.Investigator);
        //    return report;
        //}

        //public void AddHTML(Report report, CardRow cardRow, CardReport cardReport)
        //{
        //    report.AddSubtitle(Wild.Resources.Interface.SampleNote);
        //    AddHTMLCard(report, cardRow);

        //    if (cardReport == CardReport.Note) return;

        //    LogRow[] logRows = cardRow.GetLogRows();

        //    if (logRows.Length == 0)
        //    {
        //        report.AddParagraph(Wild.Resources.Common.EmptySample);
        //    }
        //    else
        //    {
        //        report.BreakPage();
        //        report.AddSubtitle((string)new ResourceManager(typeof(Card)).GetObject("labelLog.Text"));
        //        AddHTMLLog(report, cardRow);

        //        if (cardReport == CardReport.IncludeSpeciesLog) return;

        //        List<IndividualRow> individualRows = new List<IndividualRow>();

        //        foreach (LogRow logRow in logRows)
        //        {
        //            individualRows.AddRange(logRow.GetIndividualRows());
        //        }

        //        if (individualRows.Count > 0)
        //        {
        //            if (UserSettings.BreakBeforeIndividuals)
        //            {
        //                report.BreakPage();
        //            }

        //            report.AddSubtitle(Wild.Resources.Interface.IndividualsCaption);

        //            foreach (LogRow logRow in logRows)
        //            {
        //                if (AddHTMLIndividuals(report, logRow) > 0 &&
        //                    UserSettings.BreakBetweenSpecies && logRows.Last() != logRow)
        //                {
        //                    report.BreakPage();
        //                }
        //            }

        //            if (cardReport == CardReport.IncludeIndividualsLog) return;

        //            // TODO: Implement individual reports when 
        //            // individual profiles will be implemented
        //        }
        //    }
        //}

        //public void AddHTMLCard(Report report, CardRow cardRow)
        //{
        //    ResourceManager resources = new ResourceManager(typeof(Card));

        //    #region Common

        //    Report.Table table1 = new Report.Table(Wild.Resources.Interface.Common);

        //    table1.StartRow();
        //    table1.AddCellPrompt(Wild.Resources.Common.Investigator,
        //        cardRow.Investigator, 2);
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddCellPrompt(resources.GetString("labelWater.Text"),
        //        cardRow.IsWaterIDNull() ? string.Empty : cardRow.WaterRow.GetWaterFullName(), 2);
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddCellPrompt(resources.GetString("labelLabel.Text"),
        //            cardRow.IsLabelNull() ? string.Empty : cardRow.Label);
        //    table1.AddCellPrompt(Wild.Resources.Common.When, cardRow.IsWhenNull() ? string.Empty :
        //            (cardRow.When.ToShortDateString() + " " + cardRow.When.ToString("HH:mm")));
        //    table1.EndRow();

        //    table1.StartRow();
        //    table1.AddCellPrompt(Wild.Resources.Common.Where,
        //        cardRow.IsWhereNull() ? string.Empty : cardRow.Position.GetPlaceableLink(), 2);
        //    table1.EndRow();
        //    report.AddTable(table1);

        //    #endregion

        //    #region Additional Factors

        //    if (cardRow.GetFactorValueRows().Length > 0)
        //    {
        //        Report.Table table2 = new Report.Table(resources.GetString("labelFactors.Text"));
        //        table2.AddTableHeader(new string[]{ Wild.Resources.Interface.Factor, 
        //                    Wild.Resources.Interface.FactorValue }, new double[] { .80 });

        //        foreach (FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
        //        {
        //            table2.StartRow();
        //            table2.AddCell(factorValueRow.FactorRow.Factor);
        //            table2.AddCellRight(factorValueRow.Value);
        //            table2.EndRow();
        //        }

        //        report.AddTable(table2);
        //    }

        //    #endregion

        //    #region Comments

        //    Report.Table table3 = new Report.Table(Wild.Resources.Interface.Comments);

        //    table3.StartRow();
        //    if (cardRow.IsCommentsNull())
        //    {
        //        table3.AddCell(string.Empty);
        //    }
        //    else
        //    {
        //        table3.AddCell(cardRow.Comments);
        //    }
        //    table3.EndRow();
        //    report.AddTable(table3);

        //    #endregion
        //}

        //public void AddHTMLLog(Report report, CardRow cardRow)
        //{
        //    LogRow[] LogRows = cardRow.GetLogRows();

        //    if (LogRows.Length == 0)
        //    {
        //        report.AddParagraph(Wild.Resources.Common.EmptySample);
        //        return;
        //    }

        //    ResourceManager resources = new ResourceManager(typeof(Card));

        //    Report.Table table1 = new Report.Table();
        //    table1.AddTableHeader(new string[]{
        //                    resources.GetString("ColumnSpecies.HeaderText"),
        //                    resources.GetString("ColumnQuantity.HeaderText"),
        //                    resources.GetString("ColumnMass.HeaderText") }, new double[] { .50 });

        //    double Quantity = 0;
        //    double Mass = 0;

        //    foreach (LogRow logRow in LogRows)
        //    {
        //        table1.StartRow();

        //        table1.StartCellOfClass("left", logRow.SpeciesRow.ReportFullPresentation);

        //        if (!logRow.IsCommentsNull())
        //        {
        //            report.Write("<br><span class='leftcomment'>{0}: {1}</span>",
        //                Wild.Resources.Interface.Comments, logRow.Comments);
        //        }

        //        table1.EndCell();

        //        if (logRow.IsQuantityNull())
        //        {
        //            table1.AddCell();
        //        }
        //        else
        //        {
        //            table1.AddCellRight(logRow.Quantity);
        //            Quantity += logRow.Quantity;
        //        }

        //        if (logRow.IsMassNull())
        //        {
        //            table1.AddCell();
        //        }
        //        else
        //        {
        //            table1.AddCellRight(logRow.Mass, Mayfly.Service.Mask(2));
        //            Mass += logRow.Mass;
        //        }
        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total);
        //    table1.AddCellRight(Quantity);
        //    table1.AddCellRight(Mass, Mayfly.Service.Mask(2));
        //    table1.EndRow();

        //    report.AddTable(table1);
        //}

        //public int AddHTMLIndividuals(Report report, LogRow logRow)
        //{
        //    Data.IndividualRow[] individualRows = logRow.GetIndividualRows();

        //    if (individualRows.Length > 0)
        //    {
        //        VariableRow[] variableRows = Variable.FindByLog(logRow);

        //        int massColumnIndex = 1;
        //        int columnCount = 2;

        //        AddHTMLIndividualsStart(report, logRow.SpeciesRow, individualRows, variableRows,
        //            ref massColumnIndex, ref columnCount);

        //        double mass = 0.0;
        //        int count = 0;

        //        foreach (IndividualRow individualRow in individualRows)
        //        {
        //            AddHTMLIndividuals(report, individualRow, individualRows, variableRows);

        //            if (!individualRow.IsMassNull())
        //            {
        //                mass += individualRow.Mass;
        //            }

        //            if (individualRow.IsIdenticCountNull())
        //            {
        //                count++;
        //            }
        //            else
        //            {
        //                count += individualRow.IdenticCount;
        //            }
        //        }

        //        AddHTMLIndividualsTotal(report, columnCount, count, mass, massColumnIndex);
        //    }

        //    return individualRows.Length;
        //}

        //public void AddHTMLIndividualsStart(Report report,
        //    Data.SpeciesRow speciesRow,
        //    IndividualRow[] individualRows,
        //    VariableRow[] variables,
        //    ref int massColumnIndex,
        //    ref int totalColumnCount)
        //{
        //    ResourceManager resources = new ResourceManager(typeof(Individuals));

        //    Report.Table table1 = new Report.Table(speciesRow.ReportFullPresentation);

        //    List<string> vars = new List<string>();
        //    vars.Add(Wild.Resources.Interface.IndNo);

        //    if (IsDataPresented(Individual.LengthColumn, individualRows))
        //    {
        //        vars.Add(resources.GetString("ColumnLength.HeaderText"));
        //    }

        //    massColumnIndex = vars.Count;
        //    vars.Add(resources.GetString("ColumnMass.HeaderText"));

        //    if (IsDataPresented(Individual.SexColumn, individualRows))
        //    {
        //        vars.Add(resources.GetString("ColumnSex.HeaderText"));
        //    }

        //    vars.AddRange(Variable.Names(variables));

        //    if (IsDataPresented(Individual.IdenticCountColumn, individualRows))
        //    {
        //        vars.Add(resources.GetString("ColumnIdenticCount.HeaderText"));
        //    }

        //    vars.Add(Wild.Resources.Interface.Comments);

        //    totalColumnCount = vars.Count;

        //    List<double> widths = new List<double>();
        //    widths.Add(.05);
        //    widths.Add(.10);
        //    for (int i = 2; i < vars.Count - 1; i++)
        //    {
        //        widths.Add(.10);
        //    }

        //    table1.AddTableHeader(vars.ToArray(), widths.ToArray());
        //}

        //public void AddHTMLIndividuals(Report report,
        //    IndividualRow individualRow,
        //    IndividualRow[] individualRows,
        //    VariableRow[] variables)
        //{
        //    table1.StartRow();

        //    //Report.AddCellRight(No);
        //    table1.AddCellRight(Math.Abs(individualRow.ID));

        //    if (IsDataPresented(Individual.LengthColumn, individualRows))
        //    {
        //        if (individualRow.IsLengthNull())
        //        {
        //            table1.AddCell();
        //        }
        //        else
        //        {
        //            table1.AddCellRight(individualRow.Length, Mayfly.Service.Mask(1));
        //        }
        //    }

        //    if (individualRow.IsMassNull())
        //    {
        //        table1.AddCell();
        //    }
        //    else
        //    {
        //        table1.AddCellRight(individualRow.Mass, Mayfly.Service.Mask(2));
        //    }


        //    if (IsDataPresented(Individual.SexColumn, individualRows))
        //    {
        //        if (individualRow.IsSexNull())
        //        {
        //            table1.AddCell();
        //        }
        //        else
        //        {
        //            table1.AddCellValue(new Sex(individualRow.Sex), "C");
        //        }
        //    }

        //    foreach (VariableRow variable in variables)
        //    {
        //        if (Value.FindByIndIDVarID(individualRow.ID, variable.ID) == null)
        //        {
        //            table1.AddCell();
        //        }
        //        else
        //        {
        //            table1.AddCellRight(Value.FindByIndIDVarID(individualRow.ID, variable.ID).Value, Mayfly.Service.Mask(2));
        //        }
        //    }


        //    if (IsDataPresented(Individual.IdenticCountColumn, individualRows))
        //    {
        //        if (individualRow.IsIdenticCountNull())
        //        {
        //            table1.AddCell();
        //        }
        //        else
        //        {
        //            table1.AddCellValue("× " + individualRow.IdenticCount);
        //        }
        //    }

        //    if (individualRow.IsCommentsNull())
        //    {
        //        table1.AddCell(string.Empty);
        //    }
        //    else
        //    {
        //        table1.AddCell(individualRow.Comments);
        //    }

        //    table1.EndRow();
        //}

        //public void AddHTMLIndividualsTotal(Report report,
        //    int varCount, int count, double mass, int massColumnIndex)
        //{
        //    table1.StartTableFooter();
        //    table1.StartRow();

        //    for (int i = 0; i < varCount; i++)
        //    {
        //        if (i == 0)
        //        {
        //            table1.AddCellRight(count, true);
        //        }
        //        else if (i == massColumnIndex)
        //        {
        //            table1.AddCellRight(mass, Mayfly.Service.Mask(2), true);
        //        }
        //        else
        //        {
        //            table1.AddCell();
        //        }
        //    }

        //    //Report.AddCell( Count.ToString(Wild.Resources.Interface.QuantityStatus));
        //    //Report.AddCell(string.Format("{0}<br>{1}",
        //    //    Count.ToString(Wild.Resources.Interface.QuantityStatus),
        //    //    Mass.ToString(Resources.Interface.MassStatus)));

        //    table1.EndRow();
        //    table1.EndTableFooter();
        //    report.AddTable(table1);
        //}

        //#endregion



        //#region Reporting

        //public static Report BlankCard
        //{
        //    get
        //    {
        //        ResourceManager resources = new ResourceManager(typeof(Card));
        //        Report report = new Report(string.Format(
        //            "<span class='pretitle'>{0}.</span> [<span style='float:right'>]</span>",
        //            FileSystem.GetFriendlyFiletypeName(UserSettings.Interface.Extension)), Report.cssBlank);

        //        #region Common upper part

        //        Report.Table table1 = new Report.Table(Wild.Resources.Interface.Common);

        //        table1.StartRow();
        //        table1.AddCellPrompt(Wild.Resources.Common.Investigator);
        //        table1.EndRow();

        //        table1.StartRow();
        //        table1.AddCellPrompt(Wild.Resources.Common.When);
        //        table1.EndRow();

        //        table1.StartRow();
        //        table1.AddCellPrompt(resources.GetString("labelWater.Text"));
        //        table1.EndRow();

        //        table1.StartRow();
        //        table1.AddCellPrompt(Wild.Resources.Common.Where);
        //        table1.EndRow();

        //        report.AddTable(table1);

        //        #endregion

        //        #region Comments

        //        Report.Table table2 = new Report.Table(Wild.Resources.Interface.Comments);
        //        table2.StartRow();
        //        table2.AddCell(string.Empty);
        //        table2.EndRow();
        //        report.AddTable(table2);

        //        #endregion

        //        return report;
        //    }
        //}

        //public static Report BlankLog
        //{
        //    get
        //    {
        //        ResourceManager resources = new ResourceManager(typeof(Card));
        //        Report report = new Report(resources.GetString("labelLog.Text"), Report.cssBlank);
        //        report.AddTable(Wild.Service.GetBlankLog(Resources.Common.MassUnits, 26));
        //        return report;
        //    }
        //}

        //public static Report BlankIndividuals
        //{
        //    get
        //    {
        //        ResourceManager resources = new ResourceManager(typeof(Individuals));
        //        Report report = new Mayfly.Report(Wild.Resources.Interface.IndividualsCaption, Report.cssBlank);
        //        Report.Table table1 = new Report.Table();
        //        table1.AddTableHeader(new string[] { Wild.Resources.Interface.IndNo,
        //                    resources.GetString("ColumnMass.HeaderText"),
        //                    string.Empty,
        //                    string.Empty,
        //                    string.Empty, 
        //                    string.Empty, 
        //                    string.Empty, 
        //                    Wild.Resources.Interface.Comments },
        //            new double[] { .05, 0, 0, 0, 0, 0, .45 });
        //        for (int i = 1; i < 26; i++)
        //        {
        //            table1.StartRow();
        //            table1.AddCell(i);
        //            table1.AddCell();
        //            table1.AddCell();
        //            table1.AddCell();
        //            table1.AddCell();
        //            table1.AddCell();
        //            table1.AddCell();
        //            table1.EndRow();
        //        }
        //        report.AddTable(table1);
        //        report.End();
        //        return report;
        //    }
        //}

        //#endregion
    }
}