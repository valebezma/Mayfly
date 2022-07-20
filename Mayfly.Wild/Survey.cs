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
using System.Resources;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public partial class Survey
    {
        public CardRow Solitary {
            get {
                if (Card.Rows.Count == 0) {
                    CardRow result = Card.NewCardRow();
                    Card.AddCardRow(result);
                    return result;
                } else {
                    return Card[0];
                }
            }
        }



        public void ClearUseless() {
            for (int i = 0; i < Water.Count; i++) {
                if (Water[i].GetCardRows().Length == 0) {
                    Water.RemoveWaterRow(Water[i]);
                    i--;
                }
            }

            for (int i = 0; i < Definition.Count; i++) {
                if (Definition[i].GetLogRows().Length == 0) {
                    Definition.RemoveDefinitionRow(Definition[i]);
                    i--;
                }
            }

            for (int i = 0; i < Value.Count; i++) {
                if (Value[i].IndividualRow == null) {
                    Value.RemoveValueRow(Value[i]);
                    i--;
                }
            }

            for (int i = 0; i < Variable.Count; i++) {
                if (Variable[i].GetValueRows().Count() == 0) {
                    Variable.RemoveVariableRow(Variable[i]);
                    i--;
                }
            }

            for (int i = 0; i < Intestine.Count; i++) {
                if (Intestine[i].IndividualRow == null) {
                    Intestine.RemoveIntestineRow(Intestine[i]);
                    i--;
                }
            }

            for (int i = 0; i < Organ.Count; i++) {
                if (Organ[i].IndividualRow == null) {
                    Organ.RemoveOrganRow(Organ[i]);
                    i--;
                }
            }
        }

        public bool Read(string filename) {
            try {
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

                foreach (CardRow cardRow in this.Card) {
                    cardRow.Path = filename;
                }

                return Card.Count > 0;
            } catch {
                return false;
            }
        }

        public void WriteToFile(string filename) {
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

        public static Survey FromClipboard() {
            Survey data = new Survey();
            data.ReadXml(new StringReader(Clipboard.GetText()));
            return data;
        }

        public string[] GetFilenames() {
            List<string> filenames = new List<string>();

            foreach (Survey.CardRow cardRow in this.Card) {
                if (cardRow.Path == null) continue;
                if (!filenames.Contains(cardRow.Path))
                    filenames.Add(cardRow.Path);
            }

            return filenames.ToArray();
        }



        public CardStack GetStack() {
            return new CardStack(this);
        }

        public CardStack GetNotIncluded(CardStack stack) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this.Card) {
                if (stack.Contains(cardRow)) continue;
                result.Add(cardRow);
            }

            return result;
        }



        public bool IsDataPresented(DataColumn dataColumn, IndividualRow[] individualRows) {
            foreach (IndividualRow individualRow in individualRows) {
                if (!individualRow.IsNull(dataColumn)) return true;
            }
            return false;
        }

        public bool IsLoaded(string filename) {
            foreach (Survey.CardRow cardRow in this.Card) {
                if (cardRow.Path == null) continue;
                if (cardRow.Path == filename) return true;
            }

            return false;
        }

        public static bool ContainsLog(string xml) {
            try {
                Survey data = new Survey();
                data.ReadXml(new StringReader(xml));
                return data.Log.Count > 0;
            } catch { return false; }
        }

        public static bool ContainsIndividuals(string xml) {
            try {
                Survey data = new Survey();
                data.ReadXml(new StringReader(xml));
                return data.Individual.Count > 0;
            } catch {
                return false;
            }
        }


        public new Survey Copy() {
            Survey result = (Survey)((DataSet)this).Copy();
            result.SetAttributable();
            result.RefreshBios();
            return result;
        }

        public CardRow[] CopyTo(Survey extData) {
            return this.CopyTo(extData, true);
        }

        public CardRow[] CopyTo(Survey extData, bool inheritPath) {
            List<CardRow> result = new List<CardRow>();

            foreach (FactorRow factorRow in this.Factor) {
                if (extData.Factor.FindByFactor(factorRow.Factor) == null) {
                    FactorRow newFactorRow = extData.Factor.NewFactorRow();
                    newFactorRow.Factor = factorRow.Factor;
                    extData.Factor.AddFactorRow(newFactorRow);
                }
            }

            foreach (CardRow cardRow in this.Card) {
                CardRow newCardRow = cardRow.CopyTo(extData);

                result.Add(newCardRow);

                try { newCardRow.ID = cardRow.ID; } catch { }

                if (cardRow.Path != null && inheritPath) {
                    newCardRow.Path = cardRow.Path;
                }
            }

            return result.ToArray();
        }


        public static TaxonomicIndex GetSpeciesKey(DefinitionRow[] dataSpcRows) {
            TaxonomicIndex speciesKey = new TaxonomicIndex();

            if (dataSpcRows.Length == 0) return speciesKey;

            //Survey data = (Survey)dataSpcRows[0].Table.DataSet;

            foreach (Survey.DefinitionRow dataSpcRow in dataSpcRows) {
                TaxonomicIndex.TaxonRow speciesRow = speciesKey.Taxon.NewTaxonRow(dataSpcRow.Rank, dataSpcRow.Taxon);
                TaxonomicIndex.TaxonRow equivalentRow = dataSpcRow.KeyRecord;

                if (equivalentRow != null) {
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

        public TaxonomicIndex GetSpeciesKey() {
            return GetSpeciesKey((DefinitionRow[])Definition.Select());
        }

        public DefinitionRow[] GetUnweightedSpecies() {

            List<Survey.DefinitionRow> result = new List<Survey.DefinitionRow>();

            foreach (Survey.LogRow logRow in Log) {
                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow)) {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public DefinitionRow[] GetSpeciesWithUnweightedIndividuals() {

            List<Survey.DefinitionRow> result = new List<Survey.DefinitionRow>();

            foreach (Survey.IndividualRow individualRow in Individual) {
                if (!individualRow.IsMassNull()) continue;
                if (!result.Contains(individualRow.LogRow.DefinitionRow)) {
                    result.Add(individualRow.LogRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public DefinitionRow[] GetSpeciesForWeightRecovery() {

            List<DefinitionRow> result = new List<DefinitionRow>();

            foreach (LogRow logRow in Log) {

                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow)) {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }


        partial class IntestineDataTable
        {
            public IntestineRow FindByID(int id) {
                foreach (IntestineRow intestineRow in Rows) {
                    if (intestineRow.ID == id) {
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
            public WaterRow Find(WaterRow row) {
                foreach (WaterRow waterRow in Rows) {
                    if (waterRow.Presentation == row.Presentation) {
                        return waterRow;
                    }
                }
                return null;
            }
        }

        partial class DefinitionDataTable
        {
            public DefinitionRow FindByName(string value) {
                foreach (DefinitionRow speciesRow in Rows) {
                    if (speciesRow.IsNull(columnTaxon)) {
                        continue;
                    }

                    if (speciesRow.Taxon == value) {
                        return speciesRow;
                    }
                }
                return null;
            }

            public DefinitionRow AddDefinitionRow(int rank, string name) {
                return AddDefinitionRow(rank, name, null);
            }

            public DefinitionRow AddDefinitionRow(string name) {
                return AddDefinitionRow(TaxonomicRank.Species, name);
            }
        }

        partial class LogDataTable
        {
            public LogRow FindByCardIDDefID(int cardID, int defID) {
                foreach (LogRow logRow in Rows) {
                    if (logRow.IsDefIDNull()) {
                        continue;
                    }

                    if (logRow.CardID == cardID && logRow.DefID == defID) {
                        return logRow;
                    }
                }

                return null;
            }
        }

        partial class StratifiedDataTable
        {
            public int Quantity {
                get {
                    int result = 0;

                    foreach (StratifiedRow stratifiedRow in Rows) {
                        result += stratifiedRow.Count;
                    }

                    return result;
                }
            }
        }

        partial class VariableDataTable
        {
            public void Fill(string[] vars) {
                foreach (string addtVar in vars) {
                    if (FindByVarName(addtVar) == null) {
                        Rows.Add(null, addtVar);
                    }
                }
            }

            public VariableRow FindByVarName(string varName) {
                foreach (VariableRow variableRow in Rows) {
                    if (variableRow.Variable == varName) {
                        return variableRow;
                    }
                }
                return null;
            }

            public VariableRow[] FindByIndividuals(IndividualRow[] individualRows) {
                List<VariableRow> result = new List<VariableRow>();

                foreach (IndividualRow individualRow in individualRows) {
                    foreach (ValueRow valueRow in individualRow.GetValueRows()) {
                        if (!result.Contains(valueRow.VariableRow)) {
                            result.Add(valueRow.VariableRow);
                        }
                    }
                }

                return result.ToArray();
            }

            public VariableRow[] FindByLog(LogRow logRow) {
                return FindByIndividuals(logRow.GetIndividualRows());
            }

            public string[] Names(VariableRow[] variableRows) {
                List<string> result = new List<string>();

                foreach (VariableRow variableRow in variableRows) {
                    result.Add(variableRow.Variable);
                }

                return result.ToArray();
            }
        }

        partial class SamplerDataTable
        {
            public SamplerRow FindByName(string name) {
                foreach (SamplerRow sampleRow in Rows) {
                    if (sampleRow.Name == name) {
                        return sampleRow;
                    }
                }
                return null;
            }
        }

        partial class VirtueDataTable
        {
            public VirtueRow FindByName(string name) {
                foreach (VirtueRow virtueRow in Rows) {
                    if (virtueRow.Name == name) {
                        return virtueRow;
                    }
                }
                return null;
            }
        }

        partial class EquipmentDataTable
        {
            public EquipmentRow FindDuplicate(EquipmentRow eqpRow) {
                foreach (EquipmentRow row in this) {
                    if (row == eqpRow) return row;
                }
                return null;
            }
        }



        partial class WaterRow
        {
            public string Presentation {
                get {
                    WaterType type = this.IsTypeNull() ? WaterType.None : (WaterType)this.Type;

                    if (this.IsWaterNull()) {
                        switch (type) {
                            case WaterType.Stream:
                                return String.Format(Waters.Resources.Interface.TitleStream, Waters.Resources.Interface.Unnamed);

                            case WaterType.Lake:
                                return String.Format(Waters.Resources.Interface.TitleLake, Waters.Resources.Interface.Unnamed);

                            case WaterType.Tank:
                                return String.Format(Waters.Resources.Interface.TitleTank, Waters.Resources.Interface.Unnamed);

                            default:
                                return string.Empty;
                        }
                    } else {
                        switch (type) {
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

            public string Investigator {
                get {
                    if (investigator == null) {
                        try {
                            string author = StringCipher.Decrypt(this.Sign, this.When.ToString("s"));
                            investigator = string.IsNullOrWhiteSpace(author) ? global::Mayfly.Resources.Interface.InvestigatorNotApproved : author;
                        } catch {
                            investigator = global::Mayfly.Resources.Interface.InvestigatorNotApproved;
                        }
                    }

                    return investigator;
                }
            }

            public SamplerRow SamplerRow {
                get {
                    return IsEqpIDNull() ? null : this.EquipmentRow.SamplerRow;
                }
            }

            public string Path {
                get { return path; }

                set {
                    path = value;
                    tableCard.CommonPath = IO.GetCommonPath(((Survey)this.tableCard.DataSet).GetFilenames());
                }
            }

            public string FriendlyPath {
                get {
                    if (this.Path == null) {
                        return this.ToString();
                    } else {
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

            public Waypoint Position {
                get {
                    Waypoint result = this.IsWhereNull() ? new Waypoint(0, 0) : new Waypoint(this.Where);

                    if (this.IsWhenNull()) this.When = DateTime.Now;

                    result.TimeMark = this.When;
                    result.Name = string.Format("Sampled {0} by {1}", this.When, this.Investigator);

                    return result;
                }
            }


            public int Portions {
                get {
                    if (EquipmentRow.EffortType != EffortType.Portion)
                        return -1;

                    if (IsEffortNull())
                        return -1;

                    return (int)Effort;
                }
            }

            public double Exposure {
                get {
                    if (EquipmentRow.EffortType != EffortType.Exposure)
                        return double.NaN;

                    if (IsEffortNull())
                        return double.NaN;

                    return Effort;
                }
            }

            public TimeSpan Duration {
                get {
                    //if (IsEqpIDNull())
                    //    return TimeSpan.Zero;

                    //if (EquipmentRow.EffortType != EffortType.Exposition)
                    //    return TimeSpan.Zero;

                    if (IsEffortNull())
                        return TimeSpan.Zero;

                    return TimeSpan.FromMinutes(Effort);
                }
            }

            public DateTime WhenStarted {
                get {
                    return (!IsEffortNull()) ? When - Duration : When;
                }
            }


            public bool IsEnvironmentDescribed {
                get {
                    if (!this.IsWeatherNull()) return true;
                    if (!this.IsPhysicalsNull()) return true;
                    if (!this.IsChemicalsNull()) return true;
                    if (!this.IsOrganolepticsNull()) return true;
                    return false;
                }
            }

            public AquaState StateOfWater {
                get {
                    return new AquaState(
                        this.IsPhysicalsNull() ? string.Empty : this.Physicals,
                        this.IsChemicalsNull() ? string.Empty : this.Chemicals,
                        this.IsOrganolepticsNull() ? string.Empty : this.Organoleptics);
                }
            }

            public WeatherState WeatherConditions {
                get {
                    return this.IsWeatherNull() ? null : new WeatherState(this.Weather);
                }
            }

            public EnvironmentState EnvironmentState {
                get {
                    return new EnvironmentState(this.Position, WeatherConditions, StateOfWater);
                }
            }

            public int Quantity {
                get {
                    int result = 0;

                    //Data data = (Data)this.tableCard.DataSet;

                    foreach (Survey.LogRow logRow in this.GetLogRows()) {
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

            public double Mass {
                get {
                    double result = 0;

                    foreach (Survey.LogRow logRow in this.GetLogRows()) {
                        if (logRow.IsMassNull()) return double.NaN;

                        result += logRow.Mass;
                    }

                    return result;
                }
            }

            public int Wealth {
                get {
                    return this.GetLogRows().Length;
                }
            }



            /// <summary>
            /// Get suggested name for file with specified extension
            /// </summary>
            public string GetSuggestedName() {

                string result = string.Empty;

                if (!IsWhenNull()) {
                    result += When.ToString("yyyy-MM-dd") + " ";
                }

                if (!IsLabelNull()) {
                    result += Label + " ";
                }

                if (!IsEqpIDNull()) {
                    result += EquipmentRow.ShortPresentation + " ";
                }

                if (!IsWaterIDNull()) {
                    result += WaterRow.Presentation + " ";
                }

                foreach (char s in System.IO.Path.GetInvalidFileNameChars()) {
                    if (result.Contains(s)) {
                        result = result.Replace(s, ' ');
                    }
                }

                return result.Trim();
            }

            public void AttachSign() {
                this.Sign = StringCipher.Encrypt(global::Mayfly.UserSettings.Username, this.When.ToString("s"));
            }

            public void RenewSign() {
                string owner = this.Investigator;

                if (owner == global::Mayfly.Resources.Interface.InvestigatorNotApproved) {
                    SetSignNull();
                    investigator = null;
                    return;
                } else {
                    Sign = StringCipher.Encrypt(owner, When.ToString("s"));
                }
            }

            public object GetValue(string field) {
                Survey data = (Survey)tableCard.DataSet;

                if (data.Card.Columns[field] != null) {
                    if (IsNull(field)) {
                        return null;
                    } else switch (field) {
                            case "Where":
                                if (IsWhereNull()) return null;
                                else return Position;

                            case "CrossSection":
                                if (IsWaterIDNull()) return null;
                                else return Service.CrossSection((WaterType)WaterRow.Type, CrossSection);

                            case "Bank":
                                return Service.Bank(Bank);

                            case "Span":
                                if (IsEffortNull()) return null;
                                else return Duration;

                            case "Weather":
                                return WeatherConditions;

                            default:
                                return this[field];
                        }
                } else if (data.Factor.FindByFactor(field) != null) {
                    FactorValueRow factorValueRow = data.FactorValue.FindByCardIDFactorID(
                        ID, data.Factor.FindByFactor(field).ID);
                    return factorValueRow == null ? double.NaN : factorValueRow.Value;
                } else {
                    switch (field) {
                        case "Investigator":
                            return Investigator;

                        case "Water":
                            if (IsWaterIDNull() || WaterRow.IsWaterNull()) return null;
                            else return WaterRow.Water;

                        case "Wealth":
                            return Wealth;

                        case "Quantity":
                            return Quantity;

                        case "Mass":
                            return Mass;

                        case "Surface":
                            if (StateOfWater.IsTemperatureSurfaceNull()) return null;
                            else return StateOfWater.TemperatureSurface;
                    }
                }

                throw new ArgumentNullException(field, string.Format("Field '{0}' is not found in dataset.", field));
            }

            public Survey SingleCardDataset() {
                Survey data = new Survey();
                this.CopyTo(data);
                data.ClearUseless();
                return data;
            }

            public LogRow[] GetLogRows(LogSortOrder logOrder) {
                List<LogRow> result = new List<LogRow>();
                result.AddRange(this.GetLogRows());
                result.Sort(new LogRowSorter(logOrder));
                return result.ToArray();
            }

            public CardRow CopyTo(Survey extData) {
                CardRow newCardRow = extData.Card.NewCardRow();

                if (!this.IsWaterIDNull()) {
                    WaterRow waterRow = extData.Water.Find(this.WaterRow);

                    if (waterRow == null) {
                        waterRow = extData.Water.NewWaterRow();
                        waterRow.Type = this.WaterRow.Type;
                        if (!this.WaterRow.IsWaterNull()) waterRow.Water = this.WaterRow.Water;
                        extData.Water.AddWaterRow(waterRow);
                    }

                    newCardRow.WaterRow = waterRow;
                }

                foreach (DataColumn dataColumn in this.tableCard.Columns) {
                    if (this.IsNull(dataColumn)) continue;
                    if (dataColumn == this.tableCard.IDColumn) continue;
                    if (dataColumn == this.tableCard.WaterIDColumn) continue;
                    newCardRow[dataColumn.ColumnName] = this[dataColumn.ColumnName];
                }

                extData.Card.Rows.Add(newCardRow);

                CopyLogTo(newCardRow);

                foreach (FactorValueRow factorValueRow in this.GetFactorValueRows()) {
                    if (extData.Factor.FindByFactor(factorValueRow.FactorRow.Factor) == null) {
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

            public void CopyLogTo(CardRow dstCard) {
                Survey srcData = (Survey)this.Table.DataSet;
                Survey dstData = (Survey)dstCard.Table.DataSet;

                foreach (VariableRow variableRow in srcData.Variable) {
                    if (dstData.Variable.FindByVarName(variableRow.Variable) == null) {
                        VariableRow newVariableRow = dstData.Variable.NewVariableRow();
                        newVariableRow.Variable = variableRow.Variable;
                        dstData.Variable.AddVariableRow(newVariableRow);
                    }
                }

                foreach (LogRow logRow in this.GetLogRows()) {
                    LogRow dstLogRow = dstData.Log.NewLogRow();
                    dstLogRow.CardRow = dstCard;

                    if (!logRow.IsDefIDNull()) {
                        DefinitionRow dstDefRow = dstData.Definition.FindByName(logRow.DefinitionRow.Taxon);
                        if (dstDefRow == null) {
                            dstLogRow.DefinitionRow = dstData.Definition.AddDefinitionRow(logRow.DefinitionRow.Rank, logRow.DefinitionRow.Taxon);
                            dstData.Log.AddLogRow(dstLogRow);
                        } else {
                            if (dstData.Log.FindByCardIDDefID(dstCard.ID, dstDefRow.ID) == null) {
                                dstLogRow.DefinitionRow = dstDefRow;
                                dstData.Log.AddLogRow(dstLogRow);
                            } else {
                                dstLogRow = dstData.Log.FindByCardIDDefID(dstCard.ID, dstDefRow.ID);
                            }
                        }
                    }

                    if (!logRow.IsQuantityNull()) {
                        if (dstLogRow.IsQuantityNull()) {
                            dstLogRow.Quantity = logRow.Quantity;
                        } else {
                            dstLogRow.Quantity += logRow.Quantity;
                        }
                    }

                    if (!logRow.IsMassNull()) {
                        if (dstLogRow.IsMassNull()) {
                            dstLogRow.Mass = logRow.Mass;
                        } else {
                            dstLogRow.Mass += logRow.Mass;
                        }
                    }

                    if (!logRow.IsIntervalNull()) {
                        if (dstLogRow.IsIntervalNull()) {
                            dstLogRow.Interval = logRow.Interval;
                        } else {
                            //
                        }
                    }

                    if (!logRow.IsCommentsNull()) {
                        if (dstLogRow.IsCommentsNull()) {
                            dstLogRow.Comments = logRow.Comments;
                        } else {
                            dstLogRow.Comments += Constants.Return + logRow.Comments;
                        }
                    }

                    //dstData.Log.AddLogRow(dstLogRow);

                    foreach (Survey.IndividualRow individualRow in logRow.GetIndividualRows()) {
                        individualRow.CopyTo(dstLogRow);
                    }

                    foreach (Survey.StratifiedRow stratifiedRow in logRow.GetStratifiedRows()) {
                        Survey.StratifiedRow newStratifiedRow = dstData.Stratified.NewStratifiedRow();
                        newStratifiedRow.LogRow = dstLogRow;
                        newStratifiedRow.Count = stratifiedRow.Count;
                        newStratifiedRow.Class = stratifiedRow.Class;
                        dstData.Stratified.AddStratifiedRow(newStratifiedRow);
                    }
                }
            }

            public void AddReport(Report report, CardReportLevel level) {

                ResourceManager resources = new ResourceManager(typeof(Card));

                if (level.HasFlag(CardReportLevel.Note)) {

                    report.AddSectionTitle(Wild.Resources.Reports.Header.SampleNote);

                    #region Common

                    Report.Table table1 = new Report.Table(Resources.Reports.Header.Common);

                    table1.StartRow();
                    table1.AddCellPrompt(Wild.Resources.Reports.Card.Investigator, Investigator, 2);
                    table1.EndRow();

                    table1.StartRow();
                    table1.AddCellPrompt(resources.GetString("labelWater.Text"),
                        IsWaterIDNull() ? Constants.Null : WaterRow.Presentation, 2);
                    table1.EndRow();

                    table1.StartRow();
                    table1.AddCellPrompt(resources.GetString("labelLabel.Text"),
                            IsLabelNull() ? Constants.Null : Label);
                    table1.AddCellPrompt(Resources.Reports.Card.When, IsWhenNull() ? Constants.Null :
                            (When.ToShortDateString() + " " + When.ToString("HH:mm")));
                    table1.EndRow();

                    table1.StartRow();
                    table1.AddCellPrompt(Resources.Reports.Card.Where, Position.GetHTMLReference(UI.FormatCoordinate), 2);
                    table1.EndRow();
                    report.AddTable(table1);

                    //if (IsDepthNull())
                    //{
                    //    table1.AddCellPrompt(resources.GetString("labelDepth.Text"));
                    //}
                    //else
                    //{
                    //    table1.AddCellPrompt(resources.GetString("labelDepth.Text"), Depth);
                    //}

                    #endregion

                    #region Sampler

                    Report.Table table2 = new Report.Table(resources.GetString("labelMethod.Text"));

                    if (!IsEqpIDNull()) {

                        table2.StartRow();
                        table2.AddCellPrompt(resources.GetString("labelSampler.Text"), SamplerRow, 2);
                        table2.EndRow();

                        int t = 0;

                        foreach (SamplerVirtueRow svr in SamplerRow.GetSamplerVirtueRows()) {

                            if (t == 0) {
                                table2.StartRow();
                            }

                            EquipmentVirtueRow evr = ((Survey)tableCard.DataSet).EquipmentVirtue.FindByEqpIDVrtID(EqpID, svr.VrtID);
                            table2.AddCellPrompt(resources.GetString("label" + svr.VirtueRow.Name + ".Text"),
                                evr == null ? Constants.Null : evr.Value.ToString());
                            t++;

                            if (t == 2) {
                                table2.EndRow();
                                t = 0;
                            }
                        }


                        //table2.StartRow();
                        //table2.AddCellPrompt(resources.GetString("labelMesh.Text"), IsMeshNull() ? Constants.Null : Mesh.ToString());
                        //table2.AddCellPrompt(resources.GetString("labelHook.Text"), IsHookNull() ? Constants.Null : Hook.ToString());
                        //table2.EndRow();

                        //table2.StartRow();
                        //table2.AddCellPrompt(resources.GetString("labelLength.Text"), IsLengthNull() ? Constants.Null : Length.ToString());
                        //table2.AddCellPrompt(resources.GetString("labelOpening.Text"), IsOpeningNull() ? Constants.Null : Opening.ToString());
                        //table2.EndRow();

                        //table2.StartRow();
                        //table2.AddCellPrompt(resources.GetString("labelHeight.Text"), IsHeightNull() ? Constants.Null : Height.ToString());
                        //table2.AddCellPrompt(resources.GetString("labelSquare.Text"), IsSquareNull() ? Constants.Null : Square.ToString());
                        //table2.EndRow();

                        report.AddTable(table2);

                        //Report.Table table3 = new Report.Table(resources.GetString("labelEffort.Text"));

                        //table3.StartRow();
                        //if (IsSpanNull()) {
                        //    table3.AddCellPrompt(resources.GetString("labelOperation.Text"));
                        //    table3.AddCellPrompt(Resources.Reports.Card.Duration);
                        //} else {
                        //    table3.AddCellPrompt(resources.GetString("labelOperation.Text"), (WhenStarted.ToShortDateString() + " " + WhenStarted.ToString("HH:mm")));
                        //    table3.AddCellPrompt(Resources.Reports.Card.Duration, string.Format("{0:N0}:{1:mm}", Duration.TotalHours, Duration));
                        //}
                        //table3.EndRow();

                        //table3.StartRow();
                        //table3.AddCellPrompt(resources.GetString("labelVelocity.Text"),
                        //    IsVelocityNull() ? Constants.Null : Velocity.ToString());
                        //table3.AddCellPrompt(resources.GetString("labelExposure.Text"),
                        //    IsExposureNull() ? Constants.Null : Exposure.ToString());
                        //table3.EndRow();

                        //table3.StartRow();
                        //table3.AddCellPrompt(resources.GetString("labelArea.Text"),
                        //    GetSquare().ToString("N3"));
                        //table3.AddCellPrompt(resources.GetString("labelVolume.Text"),
                        //    GetVolume().ToString("N3"));
                        //table3.EndRow();

                        //table3.StartRow();
                        //table3.AddCellPrompt(resources.GetString("labelEfforts.Text"),
                        //    GetEffort().ToString("N3"), 2);
                        //table3.EndRow();

                        //report.AddTable(table3);
                    }

                    #endregion

                    #region Environment

                    if (IsEnvironmentDescribed) {
                        report.AddTable(StateOfWater.GetReport());
                        report.AddTable(WeatherConditions.GetReport());
                    }

                    #endregion

                    #region Additional Factors

                    if (GetFactorValueRows().Length > 0) {
                        Report.Table table5 = new Report.Table(resources.GetString("labelFactors.Text"));

                        table5.AddHeader(new string[]{ Wild.Resources.Reports.Caption.Factor,
                            Wild.Resources.Reports.Caption.FactorValue }, new double[] { .80 });

                        foreach (Wild.Survey.FactorValueRow FVR in GetFactorValueRows()) {
                            table5.StartRow();
                            table5.AddCell(FVR.FactorRow.Factor);
                            table5.AddCellRight(FVR.Value);
                            table5.EndRow();
                        }

                        report.AddTable(table5);
                    }

                    #endregion

                    #region Comments

                    Report.Table table4 = new Report.Table(Wild.Resources.Reports.Card.Comments);
                    table4.StartRow();
                    table4.AddCell(IsCommentsNull() ? Constants.Null : Comments);
                    table4.EndRow();
                    report.AddTable(table4);

                    #endregion
                }

                if (level.HasFlag(CardReportLevel.Log)) {

                    LogRow[] LogRows = GetLogRows();

                    report.AddSectionTitle(resources.GetString("tabPageLog.Text"));

                    if (LogRows.Length == 0) {
                        report.AddParagraph(Resources.Common.EmptySample);
                    } else {
                        //report.BreakPage();
                        report.AddTable(GetLogReport(resources.GetString("ColumnMass.HeaderText"), string.Empty));
                    }
                }

                List<IndividualRow> individualRows = new List<IndividualRow>();
                foreach (LogRow logRow in GetLogRows()) {
                    individualRows.AddRange(logRow.GetIndividualRows());
                }

                List<StratifiedRow> stratifiedRows = new List<StratifiedRow>();
                foreach (LogRow logRow in GetLogRows()) {
                    stratifiedRows.AddRange(logRow.GetStratifiedRows());
                }

                if ((individualRows.Count + stratifiedRows.Count) > 0 && (level.HasFlag(CardReportLevel.Individuals) || level.HasFlag(CardReportLevel.Stratified))) {
                    if (SettingsReader.BreakBeforeIndividuals) { report.BreakPage(); }
                    report.AddSectionTitle(Resources.Reports.Header.IndividualsLog);
                    foreach (LogRow logRow in GetLogRows()) {
                        string speciesPresentation = logRow.DefinitionRow.KeyRecord.FullNameReport;
                        logRow.AddReport(report, level, speciesPresentation, string.Format(Wild.Resources.Reports.Header.StratifiedSample, speciesPresentation));
                        if (SettingsReader.BreakBetweenSpecies && GetLogRows().Last() != logRow) { report.BreakPage(); }
                    }
                }

                if (individualRows.Count > 0 && level.HasFlag(CardReportLevel.Profile)) {
                    if (SettingsReader.BreakBeforeIndividuals) { report.BreakPage(); }
                    individualRows.ToArray().AddReport(report, CardReportLevel.Profile, string.Empty);
                }
            }

            /// <summary>
            /// Creates report containing Card with specified detalization level
            /// </summary>
            /// <param name="cardRow"></param>
            /// <returns></returns>
            public Report GetReport(CardReportLevel level) {
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
            public Report GetReport() {
                return GetReport(CardReportLevel.Note | CardReportLevel.Log | CardReportLevel.Stratified | CardReportLevel.Individuals);
            }

            public Report.Table GetLogReport(string massCaption, string logTitle) {
                return GetLogRows().GetSpeciesLogReportTable(massCaption, logTitle);
            }

            public Report.Table GetLogReport(string massCaption) {
                return GetLogReport(massCaption, string.Empty);
            }

            public Report.Table GetLogReport() {
                return GetLogReport(string.Empty, string.Empty);
            }



            public int CompareTo(CardRow cardRow) {
                return DateTime.Compare(this.When, cardRow.When);
            }

            public int CompareTo(object o) {
                if (o is CardRow row) {
                    return this.CompareTo(row);
                }

                if (o is DateTime time) return DateTime.Compare(this.When, time);
                return 0;
            }



            public override string ToString() => ToString(string.Empty);

            public virtual string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

            public virtual string ToString(string format, IFormatProvider provider) {
                switch (format.ToLowerInvariant()) {
                    case "f":
                        return Path;

                    case "s":
                        return FriendlyPath;

                    default:
                        return string.Format(Resources.Interface.Interface.SampleFormat,
                            When, When, (IsWaterIDNull() ? Resources.Interface.Interface.WaterUnknown : WaterRow.Presentation),
                            Investigator, (IsEqpIDNull() ? string.Empty : EquipmentRow.ShortPresentation));
                }
            }
        }

        partial class LogRow
        {
            public Interval MinStrate {
                get {
                    Interval result = Meta.Numerics.Interval.FromEndpoints(double.MaxValue, double.MaxValue);
                    int i = 0;

                    foreach (Survey.StratifiedRow stratifiedRow in this.GetStratifiedRows()) {
                        if (result.LeftEndpoint > stratifiedRow.Class) {
                            result = stratifiedRow.SizeClass;
                            i++;
                        }
                    }

                    if (i > 0) {
                        return result;
                    } else {
                        return Meta.Numerics.Interval.FromEndpoints(0, 0);
                    }
                }
            }

            public Interval MaxStrate {
                get {
                    Interval result = Meta.Numerics.Interval.FromEndpoints(double.MinValue, double.MinValue);
                    int i = 0;

                    foreach (Survey.StratifiedRow stratifiedRow in this.GetStratifiedRows()) {
                        if (result.RightEndpoint < stratifiedRow.SizeClass.RightEndpoint) {
                            result = stratifiedRow.SizeClass;
                            i++;
                        }

                    }

                    if (i > 0) {
                        return result;
                    } else {
                        return Meta.Numerics.Interval.FromEndpoints(0, 0);
                    }
                }
            }

            public int QuantityIndividuals {
                get {
                    int result = 0;

                    foreach (IndividualRow individualRow in this.GetIndividualRows()) {
                        result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
                    }

                    return result;
                }
            }

            public int QuantityStratified {
                get {
                    int result = 0;

                    foreach (StratifiedRow stratifiedRow in this.GetStratifiedRows()) {
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
            public double MassIndividuals {
                get {
                    double result = 0;

                    foreach (IndividualRow individualRow in this.GetIndividualRows()) {
                        if (individualRow.IsMassNull()) {
                            if (individualRow.IsLengthNull()) {
                                return double.NaN;
                            }

                            double meanWeight = ((Survey)this.Table.DataSet).FindMassModel(this.DefinitionRow.Taxon).GetValue(individualRow.Length);

                            if (double.IsNaN(meanWeight)) {
                                return double.NaN;
                            } else {
                                result += (individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency) * meanWeight;
                            }
                        } else {
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
            public double MassStratified {
                get {
                    double result = 0;

                    foreach (StratifiedRow stratifiedRow in this.GetStratifiedRows()) {
                        double meanWeight = ((Survey)this.Table.DataSet).FindMassModel(this.DefinitionRow.Taxon).GetValue(stratifiedRow.SizeClass.Midpoint);

                        if (double.IsNaN(meanWeight)) {
                            return double.NaN;
                        } else {
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
            public void AddReport(Report report, CardReportLevel level, string logtitle, string stratifiedtitle) {
                (new Survey.LogRow[] { this }).AddReport(report, level, logtitle, stratifiedtitle);
            }

            /// <summary>
            /// Creates report containing Individuals log and Stratified sample
            /// </summary>
            /// <param name="logRow"></param>
            /// <returns></returns>
            public Report GetReport(CardReportLevel level) {
                Report report = new Report(string.Format("Treatment Log for {0}", DefinitionRow.Taxon));
                AddReport(report, level,
                    string.Format(Resources.Interface.Interface.IndLog, string.Empty),
                    string.Format(Resources.Reports.Header.StratifiedSample, string.Empty));
                if (CardRow.IsWhenNull()) report.End();
                else report.End(CardRow.When.Year, CardRow.Investigator);

                return report;
            }
        }

        partial class DefinitionRow : IFormattable//, IConvertible
        {
            public int TotalQuantity {
                get {
                    int result = 0;

                    foreach (LogRow logRow in this.GetLogRows()) {
                        result += logRow.DetailedQuantity;
                    }

                    return result;
                }
            }

            public TaxonomicIndex.TaxonRow KeyRecord {
                get {
                    TaxonomicIndex.TaxonRow spcRow = SettingsReader.TaxonomicIndex?.FindByName(this.Taxon);
                    if (spcRow == null) return TaxonomicIndex.GetFakeTaxon(this.Rank, this.Taxon);
                    if (spcRow.MajorSynonym != null) spcRow = spcRow.MajorSynonym;
                    return spcRow;
                }
            }

            public double GetAverageMass() {

                double result = 0;
                int divider = 0;

                foreach (Survey.IndividualRow individualRow in GetIndividualRows()) {
                    if (individualRow.IsMassNull()) continue;
                    result += individualRow.Mass;
                    divider++;
                }

                if (divider > 0) // There are some weighted individuals of given length class
                {
                    return result / divider;
                } else {
                    return double.NaN;
                }
            }



            public IndividualRow[] GetIndividualRows() {
                List<IndividualRow> result = new List<IndividualRow>();

                foreach (LogRow logRow in this.GetLogRows()) {
                    result.AddRange(logRow.GetIndividualRows());
                }

                return result.ToArray();
            }



            public string ToString(string format, IFormatProvider formatProvider) {
                return Taxon;
            }

            public string ToString(string format) {
                return ToString(format, CultureInfo.CurrentCulture);
            }

            public override string ToString() {
                return ToString(string.Empty);
            }
        }

        partial class IndividualRow
        {
            public int Generation {
                get {
                    return this.LogRow.CardRow.When.AddYears(-((Age)this.Age).Years).Year;
                }
            }

            public string Species {
                get {
                    return this.LogRow.DefinitionRow.Taxon;
                }
            }

            public bool ContainsParasites {
                get {
                    if (this.GetOrganRows().Length > 0) return true;
                    return false;
                }
            }

            public bool ContainsTrophics {
                get {
                    if (!this.IsFatnessNull()) return true;
                    if (this.GetIntestineRows().Length > 0) return true;
                    return false;
                }
            }

            public bool ContainsSex {
                get {
                    if (!this.IsGonadMassNull()) return true;
                    if (!this.IsGonadSampleMassNull()) return true;
                    if (!this.IsGonadSampleNull()) return true;
                    if (!this.IsEggSizeNull()) return true;
                    return false;
                }
            }

            public bool ContainsExtended {
                get {
                    return !this.IsSomaticMassNull() ||
                        this.ContainsSex ||
                        this.ContainsTrophics ||
                        this.ContainsParasites;
                }
            }

            public IndividualRow CopyTo(LogRow dstLogRow) {
                Survey dstData = (Survey)dstLogRow.Table.DataSet;
                Survey.IndividualRow newIndRow = dstData.Individual.NewIndividualRow();
                newIndRow.LogRow = dstLogRow;

                foreach (DataColumn col in this.Table.Columns) {
                    if (col == this.tableIndividual.Columns["ID"]) continue;
                    if (col == this.tableIndividual.Columns["LogID"]) continue;
                    if (this.IsNull(col)) continue;

                    newIndRow[col.ColumnName] = this[col];
                }

                dstData.Individual.AddIndividualRow(newIndRow);

                this.CopyTrophicsTo(newIndRow);
                this.CopyParasitesTo(newIndRow);

                foreach (ValueRow valueRow in this.GetValueRows()) {
                    ValueRow newValueRow = dstData.Value.NewValueRow();
                    newValueRow.IndividualRow = newIndRow;

                    if (dstData.Variable.FindByVarName(valueRow.VariableRow.Variable) == null) {
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

            public void CopyTrophicsTo(IndividualRow dstRow) {
                Survey srcData = (Survey)this.tableIndividual.DataSet;
                Survey dstData = (Survey)dstRow.Table.DataSet;

                foreach (IntestineRow Row in this.GetIntestineRows()) {
                    IntestineRow NewRow = dstData.Intestine.NewIntestineRow();
                    NewRow.IndividualRow = dstRow;

                    foreach (DataColumn Col in new DataColumn[] {
                                srcData.Intestine.FermentationColumn,
                                srcData.Intestine.FullnessColumn,
                                srcData.Intestine.SectionColumn,
                                srcData.Intestine.ConsumedColumn }) {
                        if (Row.IsNull(Col)) continue;
                        NewRow[Col.ColumnName] = Row[Col.ColumnName];
                    }

                    dstData.Intestine.AddIntestineRow(NewRow);
                }
            }

            public void CopyParasitesTo(IndividualRow dstRow) {
                Survey srcData = (Survey)this.tableIndividual.DataSet;
                Survey dstData = (Survey)dstRow.Table.DataSet;

                foreach (OrganRow Row in this.GetOrganRows()) {
                    OrganRow NewRow = dstData.Organ.NewOrganRow();
                    NewRow.IndividualRow = dstRow;

                    foreach (DataColumn Col in new DataColumn[] {
                                srcData.Organ.OrganColumn,
                                srcData.Organ.InfectionColumn }) {
                        if (Row.IsNull(Col)) continue;
                        NewRow[Col.ColumnName] = Row[Col.ColumnName];
                    }

                    dstData.Organ.AddOrganRow(NewRow);
                }
            }

            public double GetAddtValue(string var) {
                foreach (Survey.ValueRow valueRow in this.GetValueRows()) {
                    if (valueRow.VariableRow.Variable == var)
                        return valueRow.Value;
                }

                return double.NaN;
            }

            public void SetAddtValue(string var, double value) {
                Survey data = (Survey)this.Table.DataSet;

                if (double.IsNaN(value)) {
                    Survey.VariableRow CurrentVarRow = data.Variable.FindByVarName(var);
                    if (CurrentVarRow == null) return;
                    Survey.ValueRow ValueRow = data.Value.FindByIndIDVarID(this.ID, CurrentVarRow.ID);
                    if (ValueRow == null) return;
                    ValueRow.Delete();
                } else {
                    Survey.VariableRow variableRow = data.Variable.FindByVarName(var);
                    if (variableRow == null) {
                        variableRow = data.Variable.AddVariableRow(var);
                    }
                    Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(this.ID, variableRow.ID);
                    if (valueRow == null) {
                        data.Value.AddValueRow(this, variableRow, value);
                    } else {
                        valueRow.Value = value;
                    }
                }
            }

            public void SetAddtValueNull(string var) {
                this.SetAddtValue(var, double.NaN);
            }



            public void AddReport(Report report) { }

            /// <summary>
            /// Creates report containing Individual profile
            /// </summary>
            /// <param name="indRow"></param>
            /// <returns></returns>
            public Report GetReport() {
                Report report = new Report(Wild.Resources.Reports.Header.IndividualProfile);
                AddReport(report);
                report.End(LogRow.CardRow.When.Year, LogRow.CardRow.Investigator);
                return report;
            }
        }

        partial class StratifiedRow
        {
            public Interval SizeClass {
                get {
                    return Interval.FromEndpointAndWidth(this.Class, this.LogRow.Interval - .001);
                }
            }
        }

        partial class EquipmentRow : IComparable, IFormattable
        {
            public string VirtueDescription {
                get {
                    VirtueRow virtueRow = SamplerRow.ClassVirtue;
                    string result = virtueRow == null ? String.Empty : string.Format("{0} {1}", virtueRow.Notation,
                        ((Survey)tableEquipment.DataSet).EquipmentVirtue.FindByEqpIDVrtID(ID, virtueRow.ID).Value);

                    result += " (";
                    foreach (EquipmentVirtueRow row in GetEquipmentVirtueRows()) {
                        if (row.VirtueRow == virtueRow) continue;
                        result += string.Format("{0} {1} ", row.VirtueRow.Notation, row.Value);
                    }
                    result = result.Trim() + ")";

                    return result;
                }
            }

            public string ShortPresentation {
                get {
                    return string.Format("{0} {1}", SamplerRow.ShortName, VirtueDescription);
                }
            }

            public string FullPresentation {
                get {
                    return string.Format("{0} {1}", SamplerRow.Name, VirtueDescription);
                }
            }

            public EffortType EffortType {
                get {
                    return SamplerRow.IsEffortTypeNull() ? EffortType.Portion : (EffortType)SamplerRow.EffortType;
                }
            }

            public void SetVirtue(string virtue, double value) {
                foreach (EquipmentVirtueRow row in GetEquipmentVirtueRows()) {
                    if (row.VirtueRow.Name == virtue) row.Value = value;
                }
            }

            public double GetVirtue(string virtue) {
                foreach (EquipmentVirtueRow row in GetEquipmentVirtueRows()) {
                    if (row.VirtueRow.Name == virtue) return row.Value;
                }
                return double.NaN;
            }


            public int CompareTo(EquipmentRow other) {
                return this.ToString().CompareTo(other.ToString());
            }

            public static bool operator ==(EquipmentRow a, EquipmentRow b) {
                // If both are null, or both are same instance, return true.
                if (Object.ReferenceEquals(a, b)) {
                    return true;
                }

                // If one is null, but not both, return false.
                if ((a is null) || (b is null)) {
                    return false;
                }

                // Return true if the fields match:
                return a.CompareTo(b) == 0;
            }

            public static bool operator !=(EquipmentRow a, EquipmentRow b) {
                return !(a == b);
            }

            int IComparable.CompareTo(object obj) {
                return Compare(this, (EquipmentRow)obj);
            }

            public static int Compare(EquipmentRow value1, EquipmentRow value2) {
                return value1.CompareTo(value2);
            }

            public override bool Equals(System.Object obj) {
                // If parameter is null return false.
                if (obj == null) {
                    return false;
                }

                // If parameter cannot be cast return false.
                if (!(obj is EquipmentRow p)) {
                    return false;
                }

                // Return true if the fields match:
                return this.CompareTo(p) == 0;
            }

            public bool Equals(EquipmentRow p) {
                // If parameter is null return false:
                if (p is null) {
                    return false;
                }

                // Return true if the fields match:
                return this.CompareTo(p) == 0;
            }

            public override int GetHashCode() {
                return this.ToString().GetHashCode();
            }


            public string ToString(string format, IFormatProvider formatProvider) {
                if (string.IsNullOrEmpty(format)) format = string.Empty;

                switch (format.ToLowerInvariant()) {

                    case "s":
                        return ShortPresentation;

                    case "f":
                        return FullPresentation;

                    default:
                        return SamplerRow.Name;
                }
            }

            public string ToString(string format) {
                return ToString(format, CultureInfo.CurrentCulture);
            }

            public override string ToString() {
                return ToString("f");
            }

        }

        partial class SamplerRow : IFormattable
        {
            public string TypeName {
                get { return this.SamplerTypeRow.Name.GetLocalizedValue(); }
            }

            public VirtueRow ClassVirtue {
                get {
                    foreach (SamplerVirtueRow samplerVirtueRow in GetSamplerVirtueRows()) {
                        if (!samplerVirtueRow.IsClassNull() && samplerVirtueRow.Class)
                            return samplerVirtueRow.VirtueRow;
                    }
                    return null;
                }
            }

            public VirtueRow GetVirtueRow(string virtue) {

                foreach (SamplerVirtueRow samplerVirtueRow in GetSamplerVirtueRows()) {
                    if (samplerVirtueRow.VirtueRow.Name == virtue)
                        return samplerVirtueRow.VirtueRow;
                }
                return null;
            }

            public VirtueRow[] GetVirtueRows() {

                List<VirtueRow> result = new List<VirtueRow>();
                foreach (SamplerVirtueRow samplerVirtueRow in GetSamplerVirtueRows()) {
                    result.Add(samplerVirtueRow.VirtueRow);
                }
                return result.ToArray();
            }

            public VirtueRow[] GetEffortVirtueRows() {

                List<VirtueRow> result = new List<VirtueRow>();
                foreach (SamplerVirtueRow samplerVirtueRow in GetSamplerVirtueRows()) {
                    if (samplerVirtueRow.Effort) result.Add(samplerVirtueRow.VirtueRow);
                }
                return result.ToArray();
            }

            public string[] GetVirtues() {

                List<string> result = new List<string>();
                foreach (VirtueRow virtueRow in GetVirtueRows()) {
                    result.Add(virtueRow.Name);
                }
                return result.ToArray();
            }

            public bool HasVirtue(string virtue) {

                foreach (SamplerVirtueRow samplerVirtueRow in GetSamplerVirtueRows()) {
                    if (samplerVirtueRow.VirtueRow.Name == virtue) return true;
                }

                return false;
            }

            public SamplerRow CopyTo(Survey data) {

                SamplerRow result = data.Sampler.NewSamplerRow();
                result.ID = this.ID;
                result.Type = this.Type;
                result.Name = this.Name.GetLocalizedValue();
                if (!this.IsShortNameNull()) result.ShortName = this.ShortName;
                if (!this.IsEffortTypeNull()) result.EffortType = this.EffortType;
                data.Sampler.AddSamplerRow(result);

                foreach (VirtueRow virtueRow in GetVirtueRows()) {
                    VirtueRow nvr = data.Virtue.FindByName(virtueRow.Name);

                    if (nvr == null) {

                        nvr = data.Virtue.AddVirtueRow(virtueRow.Name, virtueRow.Notation);
                    }

                    foreach (SamplerVirtueRow samplerVirtueRow in virtueRow.GetSamplerVirtueRows()) {

                        SamplerVirtueRow nsvr = data.SamplerVirtue.FindBySmpIDVrtID(result.ID, nvr.ID);

                        if (nsvr == null) {
                            nsvr = data.SamplerVirtue.NewSamplerVirtueRow();
                            nsvr.SamplerRow = result;
                            nsvr.VirtueRow = nvr;
                            data.SamplerVirtue.AddSamplerVirtueRow(nsvr);
                        }

                        if (!samplerVirtueRow.IsClassNull()) nsvr.Class = samplerVirtueRow.Class;
                    }
                }

                return result;
            }


            public override string ToString() {
                return ToString(string.Empty);
            }

            public string ToString(string format, IFormatProvider formatProvider) {
                if (string.IsNullOrEmpty(format)) format = string.Empty;

                switch (format.ToLowerInvariant()) {

                    case "s":
                        return IsShortNameNull() ? ToString(String.Empty) : ShortName;

                    default:
                        return Name.GetLocalizedValue((CultureInfo)formatProvider);
                }
            }

            public string ToString(string format) {
                return ToString(format, CultureInfo.CurrentCulture);
            }
        }

        partial class VirtueRow
        {
            public SamplerRow[] GetSamplerRows() {
                List<SamplerRow> result = new List<SamplerRow>();

                foreach (SamplerVirtueRow samplerVirtueRow in GetSamplerVirtueRows()) {
                    result.Add(samplerVirtueRow.SamplerRow);
                }


                return result.ToArray();

            }

            public string[] GetSamplers() {

                List<string> result = new List<string>();
                foreach (SamplerRow samplerRow in GetSamplerRows()) {
                    result.Add(samplerRow.Name);
                }
                return result.ToArray();

            }
        }
    }

    public class LogRowSorter : IComparer<Survey.LogRow>
    {
        readonly LogSortOrder logOrder;

        public LogRowSorter(LogSortOrder _logOrder) {
            logOrder = _logOrder;
        }

        public int Compare(Survey.LogRow x, Survey.LogRow y) {
            switch (logOrder) {
                case LogSortOrder.Alphabetically:
                    if (x.IsDefIDNull()) return 1;
                    if (y.IsDefIDNull()) return -1;
                    return string.Compare(x.DefinitionRow.Taxon, y.DefinitionRow.Taxon);

                case LogSortOrder.ByMass:
                    if (x.IsMassNull()) return 1;
                    if (y.IsMassNull()) return -1;
                    return (int)(1000 * y.Mass - 1000 * x.Mass);

                case LogSortOrder.ByQuantity:
                    if (x.IsQuantityNull()) return 1;
                    if (y.IsQuantityNull()) return -1;
                    return y.Quantity - x.Quantity;

                case LogSortOrder.Philogenetically:
                    if (x.IsDefIDNull()) return 0;
                    if (x.DefinitionRow.KeyRecord == null) return 0;
                    if (y.IsDefIDNull()) return 0;
                    if (y.DefinitionRow.KeyRecord == null) return 0;
                    return x.DefinitionRow.KeyRecord.CompareTo(y.DefinitionRow.KeyRecord);

            }

            return 0;
        }
    }

    public delegate void CardEventHandler(object sender, CardEventArgs e);

    public class CardEventArgs : EventArgs
    {
        private readonly Survey.CardRow eventRow;

        public CardEventArgs(Survey.CardRow row) {
            this.eventRow = row;
        }

        public Survey.CardRow Row {
            get {
                return this.eventRow;
            }
        }
    }

    public delegate void EquipmentEventHandler(object sender, EquipmentEventArgs e);

    public class EquipmentEventArgs : EventArgs
    {
        private readonly Survey.EquipmentRow eventRow;

        public EquipmentEventArgs(Survey.EquipmentRow row) {
            this.eventRow = row;
        }

        public Survey.EquipmentRow Row {
            get {
                return this.eventRow;
            }
        }
    }
}

public enum EffortType
{
    Portion,    // Size of environment captured by single portion is known from its virtues
    Exposure,   // Size of environment harvested by sampler is defined by its virtues and harvesting exposure
    Exposition  // Size of environment harvested by sampler is estimated by its virtues and soaktime
}

public enum EffortExpression
{
    Area,
    Volume,
    Standards
}
