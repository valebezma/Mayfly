using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Species;
using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Waters;
using System.Resources;
using System.Diagnostics;
using System.Xml;
using System.Text;
using System.Reflection;

namespace Mayfly.Wild
{
    public partial class Data
    {
        private SpeciesKey key;

        public CardRow Solitary
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



        public Data(SpeciesKey _key) : this()
        {
            key = _key;
        }



        /// <summary>
        /// Get suggested name for file with extension
        /// </summary>
        public string GetSuggestedName(string extension)
        {
            return GetSuggestedName(extension, string.Empty);
        }

        /// <summary>
        /// Get suggested name for file with extension and gear code
        /// </summary>
        public string GetSuggestedName(string extension, string gearCode)
        {
            string result = string.Empty;

            if (Card.Count == 1)
            {
                if (!Solitary.IsWhenNull())
                {
                    result += Solitary.When.ToString("yyyy-MM-dd") + " ";
                }

                if (!Solitary.IsLabelNull())
                {
                    result += Solitary.Label + " ";
                }

                if (!string.IsNullOrWhiteSpace(gearCode))
                {
                    result += gearCode + " ";
                }

                if (!Solitary.IsWaterIDNull())
                {
                    result += Solitary.WaterRow.Presentation + " ";
                }

                foreach (char s in Path.GetInvalidFileNameChars())
                {
                    if (result.Contains(s))
                    {
                        result = result.Replace(s, ' ');
                    }
                }

                return result.Trim() + (string.IsNullOrWhiteSpace(extension) ? string.Empty : extension);
            }
            else
            {
                return result;
            }
        }



        public void ClearUseless()
        {
            for (int i = 0; i < Water.Count; i++)
            {
                if (Water[i].GetCardRows().Length == 0)
                {
                    Water.RemoveWaterRow(Water[i]);
                    i--;
                }
            }

            for (int i = 0; i < Species.Count; i++)
            {
                if (Species[i].GetLogRows().Length == 0)
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

            for (int i = 0; i < Intestine.Count; i++)
            {
                if (Intestine[i].IndividualRow == null)
                {
                    Intestine.RemoveIntestineRow(Intestine[i]);
                    i--;
                }
            }

            for (int i = 0; i < Organ.Count; i++)
            {
                if (Organ[i].IndividualRow == null)
                {
                    Organ.RemoveOrganRow(Organ[i]);
                    i--;
                }
            }
        }

        public bool Read(string fileName)
        {
            try
            {
                foreach (DataTable dt in Tables)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dc.ColumnMapping = MappingType.Attribute;
                    }
                }

                ReadXml(fileName);


                foreach (CardRow cardRow in this.Card)
                {
                    cardRow.Path = fileName;

                    try
                    {
                        string author = StringCipher.Decrypt(cardRow.Sign, cardRow.When.ToString("s"));
                        cardRow.Investigator = string.IsNullOrWhiteSpace(author) ? Mayfly.Resources.Interface.InvestigatorNotApproved : author;
                    }
                    catch
                    {
                        cardRow.Investigator = Mayfly.Resources.Interface.InvestigatorNotApproved;
                    }
                }

                return Card.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public void WriteToFile(string fileName)
        {
            foreach (DataTable dt in Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    dc.ColumnMapping = MappingType.Attribute;
                }
            }

            //XmlTextWriter xmlWriter = new XmlTextWriter(fileName, Encoding.Unicode);
            //xmlWriter.IndentChar = ' ';
            //xmlWriter.Indentation = 4;
            //xmlWriter.WriteStartElement("meta");
            //Version libVersion = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
            //xmlWriter.WriteAttributeString("version", libVersion.ToString());
            //xmlWriter.WriteAttributeString("saved", DateTime.UtcNow.ToString("s"));
            //xmlWriter.WriteEndElement();
            //this.WriteXml(xmlWriter);
            //xmlWriter.Close();

            ////WriteXml(fileName);

            File.WriteAllText(fileName, GetXml());
        }

        public static Data FromClipboard()
        {
            Data data = new Data();

            data.ReadXml(new StringReader(Clipboard.GetText()));

            try
            {
                data.Solitary.Investigator = StringCipher.Decrypt(data.Solitary.Sign, data.Solitary.When.ToString("s"));
            }
            catch
            {
                data.Solitary.Investigator = Mayfly.Resources.Interface.InvestigatorNotApproved;
            }

            return data;
        }

        public string[] GetFilenames()
        {
            List<string> filenames = new List<string>();

            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.Path == null) continue;
                if (!filenames.Contains(cardRow.Path))
                    filenames.Add(cardRow.Path);
            }

            return filenames.ToArray();
        }



        public CardStack GetStack()
        {
            return new CardStack(this);
        }

        public CardStack GetNotIncluded(CardStack stack)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this.Card)
            {
                if (stack.Contains(cardRow)) continue;
                result.Add(cardRow);
            }

            return result;
        }



        public bool IsDataPresented(DataColumn dataColumn, IndividualRow[] individualRows)
        {
            foreach (IndividualRow individualRow in individualRows)
            {
                if (!individualRow.IsNull(dataColumn)) return true;
            }
            return false;
        }

        public bool IsLoaded(string fileName)
        {
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.Path == null) continue;
                if (cardRow.Path == fileName) return true;
            }

            return false;
        }

        public static bool ContainsLog(string xml)
        {
            try
            {
                Data data = new Data();
                data.ReadXml(new StringReader(xml));
                return data.Log.Count > 0;
            }
            catch { return false; }
        }

        public static bool ContainsIndividuals(string xml)
        {
            try
            {
                Data data = new Data();
                data.ReadXml(new StringReader(xml));
                return data.Individual.Count > 0;
            }
            catch
            {
                return false;
            }
        }


        public new Data Copy()
        {
            Data result = (Data)((DataSet)this).Copy();
            result.InitializeBio();
            result.RefreshBios();
            return result;
        }

        public CardRow[] CopyTo(Data extData)
        {
            return this.CopyTo(extData, true);
        }

        public CardRow[] CopyTo(Data extData, bool inheritPath)
        {
            List<Data.CardRow> result = new List<Data.CardRow>();

            foreach (Data.FactorRow factorRow in this.Factor)
            {
                if (extData.Factor.FindByFactor(factorRow.Factor) == null)
                {
                    Data.FactorRow newFactorRow = extData.Factor.NewFactorRow();
                    newFactorRow.Factor = factorRow.Factor;
                    extData.Factor.AddFactorRow(newFactorRow);
                }
            }

            foreach (Data.CardRow cardRow in this.Card)
            {
                Data.CardRow newCardRow = cardRow.CopyTo(extData);

                result.Add(newCardRow);

                try { newCardRow.ID = cardRow.ID; }
                catch { }

                if (cardRow.Path != null && inheritPath)
                {
                    newCardRow.Path = cardRow.Path;
                    newCardRow.SamplerPresentation = cardRow.SamplerPresentation;
                }
            }

            return result.ToArray();
        }


        partial class IntestineDataTable
        {
            public IntestineRow FindByID(int id)
            {
                foreach (IntestineRow intestineRow in Rows)
                {
                    if (intestineRow.ID == id)
                    {
                        return intestineRow;
                    }
                }

                return null;
            }
        }

        partial class CardDataTable
        {
            public string CommonPath { get; set; }
        }

        partial class WaterDataTable
        {
            public WaterRow Find(WaterRow row)
            {
                foreach (WaterRow waterRow in Rows)
                {
                    if (waterRow.Presentation == row.Presentation)
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
                    foreach (Data.SpeciesRow speciesRow in ((Data)DataSet).Species)
                    {
                        if (taxaRow.Includes(speciesRow.Species))
                        {
                            result.Add(taxaRow);
                            break;
                        }
                    }
                }

                return result.ToArray();
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

            public SpeciesRow[] GetPhylogeneticallySorted(SpeciesKey key)
            {
                List<SpeciesRow> result = new List<SpeciesRow>();
                result.AddRange((SpeciesRow[])this.Select());
                result.Sort(new SpeciesSorter(key));
                return result.ToArray();
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

        partial class StratifiedDataTable
        {
            public int Quantity
            {
                get
                {
                    int result = 0;

                    foreach (StratifiedRow stratifiedRow in Rows)
                    {
                        result += stratifiedRow.Count;
                    }

                    return result;
                }
            }
        }

        partial class VariableDataTable
        {
            public void Fill(string[] vars)
            {
                foreach (string addtVar in vars)
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

            public VariableRow[] FindByIndividuals(IndividualRow[] individualRows)
            {
                List<VariableRow> result = new List<VariableRow>();

                foreach (IndividualRow individualRow in individualRows)
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

            public VariableRow[] FindByLog(LogRow logRow)
            {
                return FindByIndividuals(logRow.GetIndividualRows());
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



        partial class WaterRow
        {
            public string Presentation
            {
                get
                {
                    WaterType type = this.IsTypeNull() ? WaterType.None : (WaterType)this.Type;

                    if (this.IsWaterNull())
                    {
                        switch (type)
                        {
                            case WaterType.Stream:
                                return String.Format(Waters.Resources.Interface.TitleStream, Waters.Resources.Interface.Unnamed);

                            case WaterType.Lake:
                                return String.Format(Waters.Resources.Interface.TitleLake, Waters.Resources.Interface.Unnamed);

                            case WaterType.Tank:
                                return String.Format(Waters.Resources.Interface.TitleTank, Waters.Resources.Interface.Unnamed);

                            default:
                                return string.Empty;
                        }
                    }
                    else
                    {
                        switch (type)
                        {
                            case WaterType.Stream:
                                return String.Format(Waters.Resources.Interface.TitleStream, this.Water);

                            case WaterType.Lake:
                                return String.Format(Waters.Resources.Interface.TitleLake, this.Water);

                            case WaterType.Tank:
                                return String.Format(Waters.Resources.Interface.TitleTank, this.Water);

                            default:
                                return this.Water;
                        }
                    }
                }
            }
        }

        partial class CardRow : IComparable, IFormattable
        {
            public string Investigator { get; set; }

            private string path;

            public string SamplerPresentation { get; set; }

            public string Path
            {
                get { return path; }

                set
                {
                    path = value;
                    tableCard.CommonPath = FileSystem.GetCommonPath(((Data)this.tableCard.DataSet).GetFilenames());
                }
            }

            public string FriendlyPath
            {
                get
                {
                    if (this.Path == null)
                    {
                        return this.ToString();
                    }
                    else
                    {
                        return (tableCard.CommonPath == null || tableCard.CommonPath == this.Path) ? System.IO.Path.GetFileNameWithoutExtension(this.Path) :
                            System.IO.Path.GetFileNameWithoutExtension(this.Path.Substring(tableCard.CommonPath.Length + 1));
                    }
                }
            }



            public void AttachSign()
            {
                this.Sign = StringCipher.Encrypt(Mayfly.UserSettings.Username,
                    this.When.ToString("s"));
                this.Investigator = Mayfly.UserSettings.Username;
            }

            public void RenewSign(DateTime newDateValue)
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
                        this.When.ToString("s"));
                }
            }


            public Waypoint Position
            {
                get
                {
                    Waypoint result = this.IsWhereNull() ? new Waypoint(0, 0) : new Waypoint(this.Where);

                    if (this.IsWhenNull()) this.When = DateTime.Now;

                    result.TimeMark = this.When;
                    result.Name = string.Format("Sampled {0} by {1}", this.When, this.Investigator);

                    return result;
                }
            }


            public Samplers.SamplerRow GetSamplerRow(Samplers index)
            {
                return index.Sampler.FindByID(this.Sampler);
            }

            public TimeSpan Duration
            {
                get
                {
                    return TimeSpan.FromMinutes(this.Span);
                }
            }

            public DateTime WhenStarted
            {
                get
                {
                    return (!this.IsSpanNull()) ? this.When - this.Duration : this.When;
                }
            }


            public override string ToString() => ToString(string.Empty);

            public virtual string ToString(string format) => ToString(format, System.Globalization.CultureInfo.CurrentCulture);

            public virtual string ToString(string format, IFormatProvider provider)
            {
                switch (format)
                {
                    case "F":
                    case "f":
                        return this.Path;

                    case "S":
                    case "s":
                        return this.FriendlyPath;

                    default:
                        return string.Format(Resources.Interface.Interface.SampleFormat,
                            When,
                            When,
                            WaterRow == null ? Resources.Interface.Interface.WaterUnknown : WaterRow.Presentation,
                            Investigator,
                            string.IsNullOrEmpty(SamplerPresentation) ? format : SamplerPresentation);
                }
            }


            public bool IsEnvironmentDescribed
            {
                get
                {
                    if (!this.IsWeatherNull()) return true;
                    if (!this.IsPhysicalsNull()) return true;
                    if (!this.IsChemicalsNull()) return true;
                    if (!this.IsOrganolepticsNull()) return true;
                    return false;
                }
            }

            public AquaState StateOfWater
            {
                get
                {
                    return new AquaState(
                        this.IsPhysicalsNull() ? string.Empty : this.Physicals,
                        this.IsChemicalsNull() ? string.Empty : this.Chemicals,
                        this.IsOrganolepticsNull() ? string.Empty : this.Organoleptics);
                }
            }

            public WeatherState WeatherConditions
            {
                get
                {
                    return this.IsWeatherNull() ? null : new WeatherState(this.Weather);
                }
            }

            public EnvironmentState EnvironmentState
            {
                get
                {
                    return new EnvironmentState(this.Position, WeatherConditions, StateOfWater);
                }
            }


            public int Quantity
            {
                get
                {
                    int result = 0;

                    //Data data = (Data)this.tableCard.DataSet;

                    foreach (Data.LogRow logRow in this.GetLogRows())
                    {
                        if (logRow.IsQuantityNull()) continue;
                        //{
                        //    if (logRow.GetIndividualRows().Length == 0) continue; // It is just notice of species presence
                        //    else return double.NaN; // It is artefact
                        //}

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

                    foreach (Data.LogRow logRow in this.GetLogRows())
                    {
                        if (logRow.IsMassNull()) return double.NaN;

                        result += logRow.Mass;
                    }

                    return result;
                }
            }

            public int Wealth
            {
                get
                {
                    return this.GetLogRows().Length;
                }
            }


            public object GetValue(string field)
            {
                Data data = ((Data)this.Table.DataSet);

                if (data.Card.Columns[field] != null)
                {
                    if (this.IsNull(field))
                    {
                        return null;
                    }
                    else switch (field)
                        {
                            case "Where":
                                if (this.IsWhereNull()) return null;
                                else return this.Position;

                            case "CrossSection":
                                if (this.IsWaterIDNull()) return null;
                                else return Service.CrossSection((WaterType)this.WaterRow.Type, this.CrossSection);

                            case "Bank":
                                return Service.Bank(this.Bank);

                            case "Span":
                                if (this.IsSpanNull()) return null;
                                else return this.Duration;

                            case "Weather":
                                return this.WeatherConditions;

                            default:
                                return this[field];
                        }
                }
                else if (data.Factor.FindByFactor(field) != null)
                {
                    Data.FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(
                        this.ID, data.Factor.FindByFactor(field).ID);
                    return factorValueRow == null ? double.NaN : factorValueRow.Value;
                }
                else
                {
                    switch (field)
                    {
                        case "Investigator":
                            return this.Investigator;

                        case "Water":
                            if (this.IsWaterIDNull() || this.WaterRow.IsWaterNull()) return null;
                            else return this.WaterRow.Water;

                        case "Wealth":
                            return this.Wealth;

                        case "Quantity":
                            return this.Quantity;

                        case "Mass":
                            return this.Mass;

                        case "Surface":
                            if (this.StateOfWater.IsTemperatureSurfaceNull()) return null;
                            else return this.StateOfWater.TemperatureSurface;
                    }
                }

                throw new ArgumentNullException(field, string.Format("Field '{0}' is not found in dataset.", field));
            }

            public Data SingleCardDataset()
            {
                Data data = new Data();
                this.CopyTo(data);
                data.ClearUseless();
                return data;
            }

            public LogRow[] GetLogRows(LogOrder logOrder)
            {
                List<LogRow> result = new List<LogRow>();
                result.AddRange(this.GetLogRows());
                result.Sort(new LogRowSorter(logOrder));
                return result.ToArray();
            }

            public CardRow CopyTo(Data extData)
            {
                Data.CardRow newCardRow = extData.Card.NewCardRow();

                if (!this.IsWaterIDNull())
                {
                    Data.WaterRow waterRow = extData.Water.Find(this.WaterRow);

                    if (waterRow == null)
                    {
                        waterRow = extData.Water.NewWaterRow();
                        waterRow.Type = this.WaterRow.Type;
                        if (!this.WaterRow.IsWaterNull()) waterRow.Water = this.WaterRow.Water;
                        extData.Water.AddWaterRow(waterRow);
                    }

                    newCardRow.WaterRow = waterRow;
                }

                foreach (DataColumn dataColumn in this.tableCard.Columns)
                {
                    if (this.IsNull(dataColumn)) continue;
                    if (dataColumn == this.tableCard.IDColumn) continue;
                    if (dataColumn == this.tableCard.WaterIDColumn) continue;
                    newCardRow[dataColumn.ColumnName] = this[dataColumn.ColumnName];
                }

                newCardRow.Investigator = this.Investigator;

                extData.Card.Rows.Add(newCardRow);

                this.CopyLogTo(newCardRow);

                foreach (Data.FactorValueRow factorValueRow in this.GetFactorValueRows())
                {
                    if (extData.Factor.FindByFactor(factorValueRow.FactorRow.Factor) == null)
                    {
                        Data.FactorRow newFactorRow = extData.Factor.NewFactorRow();
                        newFactorRow.Factor = factorValueRow.FactorRow.Factor;
                        extData.Factor.AddFactorRow(newFactorRow);
                    }

                    Data.FactorValueRow newFactorValueRow = extData.FactorValue.NewFactorValueRow();
                    newFactorValueRow.CardRow = newCardRow;
                    newFactorValueRow.FactorRow = extData.Factor.FindByFactor(factorValueRow.FactorRow.Factor);
                    newFactorValueRow.Value = factorValueRow.Value;
                    extData.FactorValue.AddFactorValueRow(newFactorValueRow);
                }

                return newCardRow;
            }

            public void CopyLogTo(CardRow dstCard)
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

                foreach (Data.LogRow logRow in this.GetLogRows())
                {
                    Data.LogRow dstLogRow = dstData.Log.NewLogRow();
                    dstLogRow.CardRow = dstCard;

                    if (!logRow.IsSpcIDNull())
                    {
                        Data.SpeciesRow dstSpeciesRow = dstData.Species.FindBySpecies(logRow.SpeciesRow.Species);
                        if (dstSpeciesRow == null)
                        {
                            dstLogRow.SpeciesRow = dstData.Species.AddSpeciesRow(logRow.SpeciesRow.Species);
                            dstData.Log.AddLogRow(dstLogRow);
                        }
                        else
                        {
                            if (dstData.Log.FindByCardIDSpcID(dstCard.ID, dstSpeciesRow.ID) == null)
                            {
                                dstLogRow.SpeciesRow = dstSpeciesRow;
                                dstData.Log.AddLogRow(dstLogRow);
                            }
                            else
                            {
                                dstLogRow = dstData.Log.FindByCardIDSpcID(dstCard.ID, dstSpeciesRow.ID);
                            }
                        }
                    }

                    if (!logRow.IsQuantityNull())
                    {
                        if (dstLogRow.IsQuantityNull())
                        {
                            dstLogRow.Quantity = logRow.Quantity;
                        }
                        else
                        {
                            dstLogRow.Quantity += logRow.Quantity;
                        }
                    }

                    if (!logRow.IsMassNull())
                    {
                        if (dstLogRow.IsMassNull())
                        {
                            dstLogRow.Mass = logRow.Mass;
                        }
                        else
                        {
                            dstLogRow.Mass += logRow.Mass;
                        }
                    }

                    if (!logRow.IsIntervalNull())
                    {
                        if (dstLogRow.IsIntervalNull())
                        {
                            dstLogRow.Interval = logRow.Interval;
                        }
                        else
                        {
                            //
                        }
                    }

                    if (!logRow.IsCommentsNull())
                    {
                        if (dstLogRow.IsCommentsNull())
                        {
                            dstLogRow.Comments = logRow.Comments;
                        }
                        else
                        {
                            dstLogRow.Comments += Constants.Return + logRow.Comments;
                        }
                    }

                    //dstData.Log.AddLogRow(dstLogRow);

                    foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                    {
                        individualRow.CopyTo(dstLogRow);
                    }

                    foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                    {
                        Data.StratifiedRow newStratifiedRow = dstData.Stratified.NewStratifiedRow();
                        newStratifiedRow.LogRow = dstLogRow;
                        newStratifiedRow.Count = stratifiedRow.Count;
                        newStratifiedRow.Class = stratifiedRow.Class;
                        dstData.Stratified.AddStratifiedRow(newStratifiedRow);
                    }
                }
            }


            public int CompareTo(CardRow cardRow)
            {
                return DateTime.Compare(this.When, cardRow.When);
            }

            public int CompareTo(object o)
            {
                if (o is CardRow) return this.CompareTo((CardRow)o);
                if (o is DateTime) return DateTime.Compare(this.When, (DateTime)o);
                return 0;
            }


            public Report.Table GetLogReport(string massCaption, string logTitle)
            {
                return this.GetLogRows().GetSpeciesLogReportTable(((Data)Table.DataSet).key, massCaption, logTitle);
            }

            public Report.Table GetLogReport(string massCaption)
            {
                return GetLogReport(massCaption, string.Empty);
            }

            public Report.Table GetLogReport()
            {
                return GetLogReport(string.Empty, string.Empty);
            }
        }

        partial class LogRow
        {
            public Interval MinStrate
            {
                get
                {
                    Interval result = Meta.Numerics.Interval.FromEndpoints(double.MaxValue, double.MaxValue);
                    int i = 0;

                    foreach (Data.StratifiedRow stratifiedRow in this.GetStratifiedRows())
                    {
                        if (result.LeftEndpoint > stratifiedRow.Class)
                        {
                            result = stratifiedRow.SizeClass;
                            i++;
                        }
                    }

                    if (i > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return Meta.Numerics.Interval.FromEndpoints(0, 0);
                    }
                }
            }

            public Interval MaxStrate
            {
                get
                {
                    Interval result = Meta.Numerics.Interval.FromEndpoints(double.MinValue, double.MinValue);
                    int i = 0;

                    foreach (Data.StratifiedRow stratifiedRow in this.GetStratifiedRows())
                    {
                        if (result.RightEndpoint < stratifiedRow.SizeClass.RightEndpoint)
                        {
                            result = stratifiedRow.SizeClass;
                            i++;
                        }

                    }

                    if (i > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return Meta.Numerics.Interval.FromEndpoints(0, 0);
                    }
                }
            }

            public int QuantityIndividuals
            {
                get
                {
                    int result = 0;

                    foreach (IndividualRow individualRow in this.GetIndividualRows())
                    {
                        result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
                    }

                    return result;
                }
            }

            public int QuantityStratified
            {
                get
                {
                    int result = 0;

                    foreach (StratifiedRow stratifiedRow in this.GetStratifiedRows())
                    {
                        result += stratifiedRow.Count;
                    }

                    return result;
                }
            }

            public int DetailedQuantity => this.QuantityIndividuals + this.QuantityStratified;

            /// <summary>
            /// 
            /// </summary>
            /// <returns>Mass if individuals in grams</returns>
            public double MassIndividuals
            {
                get
                {
                    double result = 0;

                    foreach (IndividualRow individualRow in this.GetIndividualRows())
                    {
                        if (individualRow.IsMassNull())
                        {
                            if (individualRow.IsLengthNull())
                            {
                                return double.NaN;
                            }

                            double meanWeight = ((Data)this.Table.DataSet).MassModels.GetValue(this.SpeciesRow.Species, individualRow.Length);

                            if (double.IsNaN(meanWeight))
                            {
                                return double.NaN;
                            }
                            else
                            {
                                result += (individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency) * meanWeight;
                            }
                        }
                        else
                        {
                            result += (individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency) * individualRow.Mass;
                        }
                    }

                    return result;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns>Mass of stratified sample in grams</returns>
            public double MassStratified
            {
                get
                {
                    double result = 0;

                    foreach (StratifiedRow stratifiedRow in this.GetStratifiedRows())
                    {
                        double meanWeight = ((Data)this.Table.DataSet).MassModels.GetValue(this.SpeciesRow.Species, stratifiedRow.SizeClass.Midpoint);

                        if (double.IsNaN(meanWeight))
                        {
                            return double.NaN;
                        }
                        else
                        {
                            result += meanWeight * stratifiedRow.Count;
                        }
                    }

                    return result;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns>Returns mass of sample in grams</returns>
            public double DetailedMass => this.MassIndividuals + this.MassStratified;
        }

        partial class SpeciesRow
        {
            public int TotalQuantity
            {
                get
                {
                    int result = 0;

                    foreach (LogRow logRow in this.GetLogRows())
                    {
                        result += logRow.DetailedQuantity;
                    }

                    return result;
                }
            }

            public SpeciesKey.SpeciesRow KeyRecord
            {
                get
                {
                    return ((Data)Table.DataSet).key.Species.FindBySpecies(this.Species);
                }
            }



            public IndividualRow[] GetIndividualRows()
            {
                List<IndividualRow> result = new List<IndividualRow>();

                foreach (LogRow logRow in this.GetLogRows())
                {
                    result.AddRange(logRow.GetIndividualRows());
                }

                return result.ToArray();
            }

            public override string ToString()
            {
                return this.IsSpeciesNull() ? Mayfly.Species.Resources.Interface.UnidentifiedTitle : this.Species;
            }
        }

        partial class IndividualRow
        {
            public int Generation
            {
                get
                {
                    return this.LogRow.CardRow.When.AddYears(-((Age)this.Age).Years).Year;
                }
            }

            public string Species
            {
                get
                {
                    return this.LogRow.SpeciesRow.Species;
                }
            }

            public bool ContainsParasites
            {
                get
                {
                    if (this.GetOrganRows().Length > 0) return true;
                    return false;
                }
            }

            public bool ContainsTrophics
            {
                get
                {
                    if (!this.IsFatnessNull()) return true;
                    if (this.GetIntestineRows().Length > 0) return true;
                    return false;
                }
            }

            public bool ContainsSex
            {
                get
                {
                    if (!this.IsGonadMassNull()) return true;
                    if (!this.IsGonadSampleMassNull()) return true;
                    if (!this.IsGonadSampleNull()) return true;
                    if (!this.IsEggSizeNull()) return true;
                    return false;
                }
            }

            public bool ContainsExtended
            {
                get
                {
                    return !this.IsSomaticMassNull() ||
                        this.ContainsSex ||
                        this.ContainsTrophics ||
                        this.ContainsParasites;
                }
            }

            public IndividualRow CopyTo(LogRow dstLogRow)
            {
                Data dstData = (Data)dstLogRow.Table.DataSet;
                Data.IndividualRow newIndRow = dstData.Individual.NewIndividualRow();
                newIndRow.LogRow = dstLogRow;

                foreach (DataColumn col in this.Table.Columns)
                {
                    if (col == this.tableIndividual.Columns["ID"]) continue;
                    if (col == this.tableIndividual.Columns["LogID"]) continue;
                    if (this.IsNull(col)) continue;

                    newIndRow[col.ColumnName] = this[col];
                }

                dstData.Individual.AddIndividualRow(newIndRow);

                this.CopyTrophicsTo(newIndRow);
                this.CopyParasitesTo(newIndRow);

                foreach (ValueRow valueRow in this.GetValueRows())
                {
                    ValueRow newValueRow = dstData.Value.NewValueRow();
                    newValueRow.IndividualRow = newIndRow;

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

                return newIndRow;
            }

            public void CopyTrophicsTo(IndividualRow dstRow)
            {
                Data srcData = (Data)this.tableIndividual.DataSet;
                Data dstData = (Data)dstRow.Table.DataSet;

                foreach (IntestineRow Row in this.GetIntestineRows())
                {
                    IntestineRow NewRow = dstData.Intestine.NewIntestineRow();
                    NewRow.IndividualRow = dstRow;

                    foreach (DataColumn Col in new DataColumn[] {
                                srcData.Intestine.FermentationColumn,
                                srcData.Intestine.FullnessColumn,
                                srcData.Intestine.SectionColumn,
                                srcData.Intestine.ConsumedColumn })
                    {
                        if (Row.IsNull(Col)) continue;
                        NewRow[Col.ColumnName] = Row[Col.ColumnName];
                    }

                    dstData.Intestine.AddIntestineRow(NewRow);
                }
            }

            public void CopyParasitesTo(IndividualRow dstRow)
            {
                Data srcData = (Data)this.tableIndividual.DataSet;
                Data dstData = (Data)dstRow.Table.DataSet;

                foreach (OrganRow Row in this.GetOrganRows())
                {
                    OrganRow NewRow = dstData.Organ.NewOrganRow();
                    NewRow.IndividualRow = dstRow;

                    foreach (DataColumn Col in new DataColumn[] {
                                srcData.Organ.OrganColumn,
                                srcData.Organ.InfectionColumn })
                    {
                        if (Row.IsNull(Col)) continue;
                        NewRow[Col.ColumnName] = Row[Col.ColumnName];
                    }

                    dstData.Organ.AddOrganRow(NewRow);
                }
            }

            public double GetAddtValue(string var)
            {
                foreach (Data.ValueRow valueRow in this.GetValueRows())
                {
                    if (valueRow.VariableRow.Variable == var)
                        return valueRow.Value;
                }

                return double.NaN;
            }

            public void SetAddtValue(string var, double value)
            {
                Data data = (Data)this.Table.DataSet;

                if (double.IsNaN(value))
                {
                    Data.VariableRow CurrentVarRow = data.Variable.FindByVarName(var);
                    if (CurrentVarRow == null) return;
                    Data.ValueRow ValueRow = data.Value.FindByIndIDVarID(this.ID, CurrentVarRow.ID);
                    if (ValueRow == null) return;
                    ValueRow.Delete();
                }
                else
                {
                    Data.VariableRow variableRow = data.Variable.FindByVarName(var);
                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(var);
                    }
                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(this.ID, variableRow.ID);
                    if (valueRow == null)
                    {
                        data.Value.AddValueRow(this, variableRow, value);
                    }
                    else
                    {
                        valueRow.Value = value;
                    }
                }
            }

            public void SetAddtValueNull(string var)
            {
                this.SetAddtValue(var, double.NaN);
            }
        }

        partial class StratifiedRow
        {
            public Interval SizeClass
            {
                get
                {
                    return Interval.FromEndpointAndWidth(this.Class, this.LogRow.Interval - .001);
                }
            }
        }
    }

    public class LogRowSorter : IComparer<Data.LogRow>
    {
        readonly LogOrder logOrder;

        public LogRowSorter(LogOrder _logOrder)
        {
            logOrder = _logOrder;
        }

        public int Compare(Data.LogRow x, Data.LogRow y)
        {
            switch (logOrder)
            {
                case LogOrder.Alphabetically:
                    if (x.IsSpcIDNull()) return 1;
                    if (y.IsSpcIDNull()) return -1;
                    return string.Compare(x.SpeciesRow.Species, y.SpeciesRow.Species);

                case LogOrder.ByMass:
                    if (x.IsMassNull()) return 1;
                    if (y.IsMassNull()) return -1;
                    return (int)(1000 * y.Mass - 1000 * x.Mass);

                case LogOrder.ByQuantity:
                    if (x.IsQuantityNull()) return 1;
                    if (y.IsQuantityNull()) return -1;
                    return y.Quantity - x.Quantity;
            }

            return 0;
        }
    }

    public delegate void CardRowSaveEventHandler(object sender, CardRowSaveEvent e);

    public class CardRowSaveEvent : global::System.EventArgs
    {
        private Data.CardRow eventRow;

        public CardRowSaveEvent(Data.CardRow row)
        {
            this.eventRow = row;
        }

        public Data.CardRow Row
        {
            get
            {
                return this.eventRow;
            }
        }
    }

}