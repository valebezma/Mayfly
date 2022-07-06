using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Species;
using Mayfly.Waters;
using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Resources;

namespace Mayfly.Wild
{
    public partial class Data
    {
        internal TaxonomicIndex key;

        private readonly Samplers samplers;

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



        public Data(TaxonomicIndex _key, Samplers _samplers) : this()
        {
            key = _key;
            samplers = _samplers;
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

            for (int i = 0; i < Definition.Count; i++)
            {
                if (Definition[i].GetLogRows().Length == 0)
                {
                    Definition.RemoveDefinitionRow(Definition[i]);
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

        public bool Read(string filename)
        {
            try
            {
                this.SetAttributable();
                ReadXml(filename);

                //foreach (SpeciesRow spcRow in Species)
                //{
                //    Definition.AddDefinitionRow(spcRow.ID, 91, spcRow.Species);

                //    foreach (LogRow logRow in spcRow.GetLogRows())
                //    {
                //        logRow.DefID = spcRow.ID;
                //        logRow.SetSpcIDNull();
                //    }
                //}

                //Species.Clear();

                foreach (CardRow cardRow in this.Card)
                {
                    cardRow.Path = filename;
                }

                return Card.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public void WriteToFile(string filename)
        {
            //XmlTextWriter xmlWriter = new XmlTextWriter(filename, Encoding.Unicode);
            //xmlWriter.IndentChar = ' ';
            //xmlWriter.Indentation = 4;
            //xmlWriter.WriteStartElement("meta");
            //Version libVersion = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
            //xmlWriter.WriteAttributeString("version", libVersion.ToString());
            //xmlWriter.WriteAttributeString("saved", DateTime.UtcNow.ToString("s"));
            //xmlWriter.WriteEndElement();
            //this.WriteXml(xmlWriter);
            //xmlWriter.Close();

            ////WriteXml(filename);

            this.SetAttributable();
            File.WriteAllText(filename, GetXml());
        }

        public static Data FromClipboard()
        {
            Data data = new Data();
            data.ReadXml(new StringReader(Clipboard.GetText()));
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

        public bool IsLoaded(string filename)
        {
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.Path == null) continue;
                if (cardRow.Path == filename) return true;
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
            result.SetAttributable();
            result.key = this.key;
            result.RefreshBios();
            return result;
        }

        public CardRow[] CopyTo(Data extData)
        {
            return this.CopyTo(extData, true);
        }

        public CardRow[] CopyTo(Data extData, bool inheritPath)
        {
            List<CardRow> result = new List<CardRow>();

            foreach (FactorRow factorRow in this.Factor)
            {
                if (extData.Factor.FindByFactor(factorRow.Factor) == null)
                {
                    FactorRow newFactorRow = extData.Factor.NewFactorRow();
                    newFactorRow.Factor = factorRow.Factor;
                    extData.Factor.AddFactorRow(newFactorRow);
                }
            }

            foreach (CardRow cardRow in this.Card)
            {
                CardRow newCardRow = cardRow.CopyTo(extData);

                result.Add(newCardRow);

                try { newCardRow.ID = cardRow.ID; }
                catch { }

                if (cardRow.Path != null && inheritPath)
                {
                    newCardRow.Path = cardRow.Path;
                }
            }

            return result.ToArray();
        }


        public static TaxonomicIndex GetSpeciesKey(DefinitionRow[] dataSpcRows)
        {
            TaxonomicIndex speciesKey = new TaxonomicIndex();

            if (dataSpcRows.Length == 0) return speciesKey;

            Data data = (Data)dataSpcRows[0].Table.DataSet;

            foreach (Data.DefinitionRow dataSpcRow in dataSpcRows)
            {
                TaxonomicIndex.TaxonRow speciesRow = speciesKey.Taxon.NewTaxonRow(dataSpcRow.Rank, dataSpcRow.Taxon);
                TaxonomicIndex.TaxonRow equivalentRow = dataSpcRow.KeyRecord;

                if (equivalentRow != null)
                {
                    if (!equivalentRow.IsReferenceNull()) speciesRow.Reference = equivalentRow.Reference;
                    if (!equivalentRow.IsLocalNull()) speciesRow.Local = equivalentRow.Local;
                }

                //var logRows = dataSpcRow.GetLogRows();

                //if (logRows.Length > 0)
                //{
                //    speciesRow.Description = "Species collected: ";

                //    foreach (Data.LogRow logRow in logRows)
                //    {
                //        if (!logRow.CardRow.IsWaterIDNull())
                //        {
                //            speciesRow.Description += string.Format("at {0} ", logRow.CardRow.WaterRow.Presentation);
                //        }

                //        if (!logRow.CardRow.IsWhenNull())
                //        {
                //            speciesRow.Description += logRow.CardRow.When.ToString("d");
                //        }

                //        Sample lengths = data.Individual.LengthColumn.GetSample(logRow.GetIndividualRows());
                //        Sample masses = data.Individual.MassColumn.GetSample(logRow.GetIndividualRows());
                //        List<object> sexes = data.Individual.SexColumn.GetValues(logRow.GetIndividualRows(), true);

                //        if (lengths.Count + masses.Count + sexes.Count > 0)
                //        {
                //            speciesRow.Description += ": ";

                //            if (lengths.Count > 0)
                //            {
                //                speciesRow.Description += string.Format("L = {0} mm", lengths.ToString("E"));
                //            }

                //            if (masses.Count > 0)
                //            {
                //                speciesRow.Description += ", ";
                //                speciesRow.Description += string.Format("W = {0} mg", masses.ToString("E"));
                //            }

                //            if (sexes.Count > 0)
                //            {
                //                speciesRow.Description += ", ";
                //                speciesRow.Description += "sexes are ";
                //                foreach (object sex in sexes)
                //                {
                //                    speciesRow.Description += new Sex((int)sex).ToString("C") + ", ";
                //                }
                //            }
                //        }

                //        speciesRow.Description += "; ";
                //    }

                //    speciesRow.Description = speciesRow.Description.TrimEnd("; ".ToCharArray()) + ".";
                //}

                speciesKey.Taxon.AddTaxonRow(speciesRow);

                //if (Species.UserSettings.VisualConfirmationOnAutoUpdate)
                //{
                //    AddAutoSpecies editSpecies = new AddAutoSpecies(speciesRow);

                //    if (editSpecies.ShowDialog() == DialogResult.OK)
                //    {
                //        speciesKey.Species.AddSpeciesRow(speciesRow);

                //        //foreach (SpeciesKey.TaxonRow taxonRow in editSpecies.SelectedTaxon)
                //        //{
                //        //    speciesKey.Species.AddSpeciesRow(speciesRow);
                //        //    dest.Rep.AddRepRow(taxonRow, destRow);
                //        //}
                //    }
                //}
                //else
                //{
                //    speciesKey.Species.AddSpeciesRow(speciesRow);
                //}
            }

            return speciesKey;
        }

        public TaxonomicIndex GetSpeciesKey()
        {
            return GetSpeciesKey((DefinitionRow[])Definition.Select());
        }

        /// <summary>
        /// Creates report containing Card
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Report GetReport()
        {
            return Solitary.GetReport();
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

        partial class DefinitionDataTable
        {
            public DefinitionRow FindByName(string value)
            {
                foreach (DefinitionRow speciesRow in Rows)
                {
                    if (speciesRow.IsNull(columnTaxon))
                    {
                        continue;
                    }

                    if (speciesRow.Taxon == value)
                    {
                        return speciesRow;
                    }
                }
                return null;
            }
        }

        partial class LogDataTable
        {
            public LogRow FindByCardIDDefID(int cardID, int defID)
            {
                foreach (LogRow logRow in Rows)
                {
                    if (logRow.IsDefIDNull())
                    {
                        continue;
                    }

                    if (logRow.CardID == cardID && logRow.DefID == defID)
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
            string investigator;

            private string path;

            public string Investigator
            {
                get
                {
                    if (investigator == null)
                    {
                        try
                        {
                            string author = StringCipher.Decrypt(this.Sign, this.When.ToString("s"));
                            investigator = string.IsNullOrWhiteSpace(author) ? Mayfly.Resources.Interface.InvestigatorNotApproved : author;
                        }
                        catch
                        {
                            investigator = Mayfly.Resources.Interface.InvestigatorNotApproved;
                        }
                    }

                    return investigator;
                }
            }

            public Samplers.SamplerRow SamplerRow
            {
                get
                {
                    return this.IsSamplerNull() ? null : (((Data)tableCard.DataSet).samplers?.Sampler.FindByID(this.Sampler));
                }
            }

            public string SamplerDetails
            {
                get
                {
                    return IsSamplerNull() ? string.Empty : (
                        SamplerRow.IsEffortFormulaNull() ? string.Empty : (
                        SamplerRow.EffortFormula.Contains("M") ? (IsMeshNull() ? string.Empty : Mesh.ToString("◊ 0")) : (
                        SamplerRow.EffortFormula.Contains("J") ? (IsHookNull() ? string.Empty : Hook.ToString("ʔ 0")) : When.ToString("yyyy MMMM")
                        )
                        )
                        );
                }
            }

            public string SamplerShortPresentation
            {
                get
                {
                    return IsSamplerNull() ? Constants.Null : string.Format("{0} {1}", SamplerRow.ShortName, SamplerDetails).TrimEnd();
                }
            }

            public string SamplerFullPresentation
            {
                get
                {
                    return IsSamplerNull() ? Constants.Null : string.Format("{0} {1}", SamplerRow.Sampler, SamplerDetails).TrimEnd();
                }
            }

            public string Path
            {
                get { return path; }

                set
                {
                    path = value;
                    tableCard.CommonPath = IO.GetCommonPath(((Data)this.tableCard.DataSet).GetFilenames());
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
                        //return string.Format("<a href='{0}'>{1}</a>",
                        //    this.Path,
                        //    (tableCard.CommonPath == null || tableCard.CommonPath == this.Path) ? System.IO.Path.GetFileNameWithoutExtension(this.Path) :
                        //    System.IO.Path.GetFileNameWithoutExtension(this.Path.Substring(tableCard.CommonPath.Length + 1))
                        //    );
                    }
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
                        //    else return double.NaN; // It is artifact
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



            /// <summary>
            /// Get suggested name for file with specified extension
            /// </summary>
            public string GetSuggestedName(string extension)
            {
                string result = string.Empty;

                if (!this.IsWhenNull())
                {
                    result += this.When.ToString("yyyy-MM-dd") + " ";
                }

                if (!this.IsLabelNull())
                {
                    result += this.Label + " ";
                }

                if (!this.IsSamplerNull())
                {
                    result += SamplerShortPresentation + " ";
                }

                if (!this.IsWaterIDNull())
                {
                    result += this.WaterRow.Presentation + " ";
                }

                foreach (char s in System.IO.Path.GetInvalidFileNameChars())
                {
                    if (result.Contains(s))
                    {
                        result = result.Replace(s, ' ');
                    }
                }

                return result.Trim() + (string.IsNullOrWhiteSpace(extension) ? string.Empty : extension);
            }

            public void AttachSign()
            {
                this.Sign = StringCipher.Encrypt(Mayfly.UserSettings.Username, this.When.ToString("s"));
            }

            public void RenewSign(DateTime newDateValue)
            {
                string owner = this.Investigator;

                if (owner == Mayfly.Resources.Interface.InvestigatorNotApproved)
                {
                    SetSignNull();
                    investigator = null;
                    return;
                }
                else
                {
                    When = newDateValue;
                    Sign = StringCipher.Encrypt(owner, When.ToString("s"));
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
                CardRow newCardRow = extData.Card.NewCardRow();

                if (!this.IsWaterIDNull())
                {
                    WaterRow waterRow = extData.Water.Find(this.WaterRow);

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

                extData.Card.Rows.Add(newCardRow);

                CopyLogTo(newCardRow);

                foreach (FactorValueRow factorValueRow in this.GetFactorValueRows())
                {
                    if (extData.Factor.FindByFactor(factorValueRow.FactorRow.Factor) == null)
                    {
                        FactorRow newFactorRow = extData.Factor.NewFactorRow();
                        newFactorRow.Factor = factorValueRow.FactorRow.Factor;
                        extData.Factor.AddFactorRow(newFactorRow);
                    }

                    FactorValueRow newFactorValueRow = extData.FactorValue.NewFactorValueRow();
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

                foreach (VariableRow variableRow in srcData.Variable)
                {
                    if (dstData.Variable.FindByVarName(variableRow.Variable) == null)
                    {
                        VariableRow newVariableRow = dstData.Variable.NewVariableRow();
                        newVariableRow.Variable = variableRow.Variable;
                        dstData.Variable.AddVariableRow(newVariableRow);
                    }
                }

                foreach (LogRow logRow in this.GetLogRows())
                {
                    LogRow dstLogRow = dstData.Log.NewLogRow();
                    dstLogRow.CardRow = dstCard;

                    if (!logRow.IsDefIDNull())
                    {
                        DefinitionRow dstDefRow = dstData.Definition.FindByName(logRow.DefinitionRow.Taxon);
                        if (dstDefRow == null)
                        {
                            dstLogRow.DefinitionRow = dstData.Definition.AddDefinitionRow(logRow.DefinitionRow.Rank, logRow.DefinitionRow.Taxon);
                            dstData.Log.AddLogRow(dstLogRow);
                        }
                        else
                        {
                            if (dstData.Log.FindByCardIDDefID(dstCard.ID, dstDefRow.ID) == null)
                            {
                                dstLogRow.DefinitionRow = dstDefRow;
                                dstData.Log.AddLogRow(dstLogRow);
                            }
                            else
                            {
                                dstLogRow = dstData.Log.FindByCardIDDefID(dstCard.ID, dstDefRow.ID);
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

            public void AddReport(Report report, CardReportLevel level) { }

            /// <summary>
            /// Creates report containing Card with specified detalization level
            /// </summary>
            /// <param name="cardRow"></param>
            /// <returns></returns>
            public Report GetReport(CardReportLevel level)
            {
                Report report = new Report(FriendlyPath);
                AddReport(report, level);
                report.End(When.Year, Investigator);
                return report;
            }

            /// <summary>
            /// Creates report containing Card
            /// </summary>
            /// <param name="cardRow"></param>
            /// <returns></returns>
            public Report GetReport()
            {
                return GetReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Stratified | CardReportLevel.Individuals);
            }

            public Report.Table GetLogReport(string massCaption, string logTitle)
            {
                return GetLogRows().GetSpeciesLogReportTable(massCaption, logTitle);
            }

            public Report.Table GetLogReport(string massCaption)
            {
                return GetLogReport(massCaption, string.Empty);
            }

            public Report.Table GetLogReport()
            {
                return GetLogReport(string.Empty, string.Empty);
            }



            public int CompareTo(CardRow cardRow)
            {
                return DateTime.Compare(this.When, cardRow.When);
            }

            public int CompareTo(object o)
            {
                if (o is CardRow row)
                {
                    return this.CompareTo(row);
                }

                if (o is DateTime time) return DateTime.Compare(this.When, time);
                return 0;
            }



            public override string ToString() => ToString(string.Empty);

            public virtual string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

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
                            When, When, WaterRow == null ? Resources.Interface.Interface.WaterUnknown : WaterRow.Presentation,
                            Investigator, SamplerFullPresentation);
                }
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

                            double meanWeight = ((Data)this.Table.DataSet).FindMassModel(this.DefinitionRow.Taxon).GetValue(individualRow.Length);

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
                        double meanWeight = ((Data)this.Table.DataSet).FindMassModel(this.DefinitionRow.Taxon).GetValue(stratifiedRow.SizeClass.Midpoint);

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



            /// <summary>
            /// Adds Individuals log and Stratified sample with given titles into Report
            /// </summary>
            /// <param name="logRow"></param>
            /// <param name="report"></param>
            /// <param name="logtitle"></param>
            /// <param name="stratifiedtitle"></param>
            public void AddReport(Report report, CardReportLevel level, string logtitle, string stratifiedtitle)
            {
                (new Data.LogRow[] { this }).AddReport(report, level, logtitle, stratifiedtitle);
            }

            /// <summary>
            /// Creates report containing Individuals log and Stratified sample
            /// </summary>
            /// <param name="logRow"></param>
            /// <returns></returns>
            public Report GetReport(CardReportLevel level)
            {
                Report report = new Report(string.Format("Treatment Log for {0}", DefinitionRow.Taxon));
                AddReport(report, level, 
                    string.Format(Resources.Interface.Interface.IndLog, string.Empty),
                    string.Format(Resources.Reports.Header.StratifiedSample, string.Empty));
                if (CardRow.IsWhenNull()) report.End();
                else report.End(CardRow.When.Year, CardRow.Investigator);

                return report;
            }
        }

        partial class DefinitionRow : IFormattable
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

            public TaxonomicIndex.TaxonRow KeyRecord
            {
                get
                {
                    TaxonomicIndex.TaxonRow spcRow = ((Data)Table.DataSet).key?.FindByName(this.Taxon);
                    if (spcRow == null) return null;
                    if (spcRow.MajorSynonym != null) spcRow = spcRow.MajorSynonym;
                    return spcRow;
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

            public string ToString(string format, IFormatProvider formatProvider)
            {
                return Taxon;
            }

            public string ToString(string format)
            {
                return ToString(format, CultureInfo.CurrentCulture);
            }

            public override string ToString()
            {
                return ToString(string.Empty);
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
                    return this.LogRow.DefinitionRow.Taxon;
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



            public void AddReport(Report report) { }

            /// <summary>
            /// Creates report containing Individual profile
            /// </summary>
            /// <param name="indRow"></param>
            /// <returns></returns>
            public Report GetReport()
            {
                Report report = new Report(Wild.Resources.Reports.Header.IndividualProfile);
                AddReport(report);
                report.End(LogRow.CardRow.When.Year, LogRow.CardRow.Investigator);
                return report;
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
                    if (x.IsDefIDNull()) return 1;
                    if (y.IsDefIDNull()) return -1;
                    return string.Compare(x.DefinitionRow.Taxon, y.DefinitionRow.Taxon);

                case LogOrder.ByMass:
                    if (x.IsMassNull()) return 1;
                    if (y.IsMassNull()) return -1;
                    return (int)(1000 * y.Mass - 1000 * x.Mass);

                case LogOrder.ByQuantity:
                    if (x.IsQuantityNull()) return 1;
                    if (y.IsQuantityNull()) return -1;
                    return y.Quantity - x.Quantity;

                case LogOrder.Philogenetically:
                    if (x.IsDefIDNull()) return 0;
                    if (x.DefinitionRow.KeyRecord == null) return 0;
                    if (y.IsDefIDNull()) return 0;
                    if (y.DefinitionRow.KeyRecord == null) return 0;
                    return x.DefinitionRow.KeyRecord.CompareTo(y.DefinitionRow.KeyRecord);

            }

            return 0;
        }
    }

    public delegate void CardRowSaveEventHandler(object sender, CardRowSaveEvent e);

    public class CardRowSaveEvent : global::System.EventArgs
    {
        private readonly Data.CardRow eventRow;

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