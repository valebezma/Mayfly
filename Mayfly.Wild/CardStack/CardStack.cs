using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using Mayfly.Species;
using Meta.Numerics;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public partial class CardStack : List<Survey.CardRow>
    {
        public Survey Parent {
            get {
                return this.Count == 0 ? null : (Survey)this[0].Table.DataSet;
            }
        }

        public string CommonPath {
            get {
                return IO.GetCommonPath(this.GetFilenames());
            }
        }

        public string FriendlyName {
            get {
                string result = System.IO.Path.GetFileName(this.CommonPath);

                if (string.IsNullOrWhiteSpace(result)) {
                    result = global::Mayfly.Resources.Interface.VariousSources;
                }

                return result;
            }
        }

        public string Name { get; set; }

        public bool IsEmpty {
            get {
                return Parent == null;// GetLogRows().Length == 0;
            }
        }

        public int SpeciesWealth {
            get {
                return GetSpecies().Length;
            }
        }



        public CardStack() { }

        public CardStack(IEnumerable<Survey.CardRow> cardRows)
            : this() {

            this.AddRange(cardRows);
            this.Name = string.Format("{0}; {1}.",
                this.GetWaterNames().Merge(),
                this.GetDates().GetDatesDescription());
        }

        public CardStack(Survey data)
            : this(data.Card) { }


        public string ToShortDescription() {

            return string.Format("{0}; {1}; {2}.",
                this.GetInvestigators().Merge(),
                this.GetWaterNames().Merge(),
                this.GetDates().GetDatesDescription()
                );
        }

        public string[] GetFilenames() {
            List<string> filenames = new List<string>();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.Path == null) continue;
                if (!filenames.Contains(cardRow.Path))
                    filenames.Add(cardRow.Path);
            }

            return filenames.ToArray();
        }



        public void Merge(CardStack stack) {
            foreach (Survey.CardRow cardRow in stack) {
                if (!this.Contains(cardRow)) {
                    this.Add(cardRow);
                }
            }
        }



        public List<string> GetSpeciesList() {
            List<string> result = new List<string>();

            foreach (Survey.LogRow logRow in this.GetLogRows()) {
                if (!result.Contains(logRow.DefinitionRow.Taxon)) {
                    result.Add(logRow.DefinitionRow.Taxon);
                }
            }

            return result;
        }

        public List<string> GetSpeciesList(CardStack stackToCommon) {
            List<string> list1 = this.GetSpeciesList();
            List<string> list2 = stackToCommon.GetSpeciesList();

            List<string> result = new List<string>();

            foreach (string species in list1) {
                if (list2.Contains(species)) {
                    result.Add(species);
                }
            }

            return result;
        }

        public Survey.LogRow[] GetLogRows() {
            List<Survey.LogRow> result = new List<Survey.LogRow>();

            if (Parent != null) {
                foreach (Survey.LogRow logRow in Parent.Log) {
                    if (!this.Contains(logRow.CardRow)) continue;
                    result.Add(logRow);
                }
            }

            return result.ToArray();
        }

        public Survey.LogRow[] GetLogRows(TaxonomicIndex.TaxonRow speciesRow) {
            List<Survey.LogRow> result = new List<Survey.LogRow>();

            if (Parent != null && speciesRow != null) {
                foreach (Survey.LogRow logRow in Parent.Log) {
                    if (!speciesRow.Validate(logRow.DefinitionRow.Taxon)) continue;
                    if (!this.Contains(logRow.CardRow)) continue;
                    result.Add(logRow);
                }
            }

            return result.ToArray();
        }

        public Survey.IndividualRow[] GetIndividualRows() {
            List<Survey.IndividualRow> result = new List<Survey.IndividualRow>();

            foreach (Survey.LogRow logRow in this.GetLogRows()) {
                result.AddRange(logRow.GetIndividualRows());
            }

            return result.ToArray();
        }

        public Survey.IndividualRow[] GetIndividualRows(TaxonomicIndex.TaxonRow speciesRow) {
            List<Survey.IndividualRow> result = new List<Survey.IndividualRow>();

            foreach (Survey.LogRow logRow in this.GetLogRows(speciesRow)) {
                result.AddRange(logRow.GetIndividualRows());
            }

            return result.ToArray();
        }



        public Survey.WaterRow[] GetWaters() {
            List<Survey.WaterRow> result = new List<Survey.WaterRow>();
            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWaterIDNull()) continue;
                if (result.Contains(cardRow.WaterRow)) continue;
                result.Add(cardRow.WaterRow);
            }
            return result.ToArray();
        }

        public string[] GetWaterNames() {
            List<string> result = new List<string>();
            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWaterIDNull()) continue;
                string waterDescription = cardRow.WaterRow.Presentation;
                if (result.Contains(waterDescription)) continue;
                result.Add(waterDescription);
            }
            return result.ToArray();
        }


        public string[] GetInvestigators() {
            List<string> result = new List<string>();
            foreach (Survey.CardRow cardRow in this) {
                string investigator = cardRow.Investigator;
                if (result.Contains(investigator)) continue;
                result.Add(investigator);
            }
            return result.ToArray();
        }


        public DateTime[] GetDates() {
            List<DateTime> result = new List<DateTime>();
            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWhenNull()) continue;
                if (result.Contains(cardRow.When.Date)) continue;
                result.Add(cardRow.When.Date);
            }
            result.Sort();
            return result.ToArray();
        }

        public DateTime EarliestEvent {
            get {
                DateTime result = DateTime.Now;

                foreach (Survey.CardRow cardRow in this) {
                    if (cardRow.WhenStarted < result) result = cardRow.WhenStarted;
                }

                return result;
            }
        }

        public DateTime LatestEvent {
            get {
                DateTime result = DateTime.FromOADate(0.0);

                foreach (Survey.CardRow cardRow in this) {
                    if (cardRow.When > result) result = cardRow.When;
                }

                return result;
            }
        }

        public Waypoint[] GetLocations() {
            List<Waypoint> result = new List<Waypoint>();

            foreach (Survey.CardRow cardRow in this) {
                if (!(cardRow.Position == null)) {
                    result.Add(cardRow.Position);
                }
            }

            return result.ToArray();
        }

        public int[] GetYears() {
            return GetDates().GetYears();
        }



        public static CardStack ConvertFrom(Survey data) {
            return new CardStack(data.Card);
        }

        public CardStack GetStack(DateTime day) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWhenNull()) continue;
                if (cardRow.When.Date != day.Date) continue;

                result.Add(cardRow);
            }

            result.Name = day.ToShortDateString();

            return result;
        }

        public CardStack GetStack(DateTime from, DateTime to) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWhenNull()) continue;
                if (cardRow.When.Date < from) continue;
                if (cardRow.When.Date > to) continue;
                result.Add(cardRow);
            }

            return result;
        }

        public CardStack GetStack(int year) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWhenNull()) continue;
                if (cardRow.When.Year != year) continue;

                result.Add(cardRow);
            }

            result.Name = year.ToString();

            return result;
        }

        public CardStack GetStack(Survey.SamplerRow samplerRow) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsEqpIDNull()) continue;
                if (cardRow.SamplerRow != samplerRow) continue;

                result.Add(cardRow);
            }

            return result;
        }

        public CardStack GetStack(Survey.WaterRow waterRow) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsWaterIDNull()) continue;
                if (cardRow.WaterRow != waterRow) continue;

                result.Add(cardRow);
            }

            return result;
        }

        public CardStack GetStack(string field, object value) {
            return GetStack(field, value, string.Empty);
        }

        public CardStack GetStack(string field, object value, string format) {
            CardStack result = new CardStack();

            foreach (Survey.CardRow cardRow in this) {
                string valueFormatted = value.Format(format);
                string cardFormatted = cardRow.GetValue(field).Format(format);

                if (object.Equals(cardFormatted, valueFormatted))
                    result.Add(cardRow);
            }

            return result;
        }


        public TaxonomicIndex.TaxonRow[] GetSpecies() {
            return GetSpecies(0);
        }

        public TaxonomicIndex.TaxonRow[] GetSpecies(int minimalSample) {
            List<TaxonomicIndex.TaxonRow> result = new List<TaxonomicIndex.TaxonRow>();

            if (Parent != null) {
                foreach (Survey.DefinitionRow spcRow in Parent.Definition) {
                    TaxonomicIndex.TaxonRow currentRecord = spcRow.KeyRecord;

                    //if (currentRecord.RowState == System.Data.DataRowState.Detached) {
                    //    //TaxonomicIndex.TaxonRow newSpcRow = ReaderSettings.TaxonomicIndex.Taxon.NewTaxonRow(spcRow.Rank, spcRow.Taxon);
                    //    //currentRecord = newSpcRow;
                    //    ReaderSettings.TaxonomicIndex.Taxon.AddTaxonRow(currentRecord);
                    //}

                    if (minimalSample > 0 && Quantity(currentRecord) < minimalSample) continue;

                    if (!result.Contains(currentRecord)) {
                        result.Add(currentRecord);
                    }
                }

                result.Sort();
            }

            return result.ToArray();
        }

        public int GetOccurrenceCases(TaxonomicIndex.TaxonRow[] speciesRows) {
            int result = 0;

            foreach (Survey.CardRow cardRow in this) {
                foreach (Survey.LogRow logRow in cardRow.GetLogRows()) {
                    foreach (TaxonomicIndex.TaxonRow speciesRow in speciesRows) {
                        if (speciesRow.Validate(logRow.DefinitionRow.Taxon)) {
                            result++;
                            goto Next;
                        }
                    }
                }

            Next:
                continue;
            }

            return result;
        }



        public Survey.SamplerRow[] GetSamplers() {
            List<Survey.SamplerRow> result = new List<Survey.SamplerRow>();

            foreach (Survey.CardRow cardRow in this) {
                if (cardRow.IsEqpIDNull()) continue;
                if (cardRow.SamplerRow == null) continue;
                if (result.Contains(cardRow.SamplerRow)) continue;
                result.Add(cardRow.SamplerRow);
            }

            return result.ToArray();
        }

        public string[] GetSamplersList() {
            List<string> result = new List<string>();

            foreach (Survey.SamplerRow samplerRow in GetSamplers()) {
                result.Add(samplerRow.Name);
            }

            return result.ToArray();
        }

        public TaxonomicIndex GetSpeciesKey() {

            TaxonomicIndex speciesKey = new TaxonomicIndex();

            foreach (Survey.DefinitionRow dataSpcRow in getDefinitionRows()) {
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

        public Survey.DefinitionRow[] GetUnweightedSpecies() {

            List<Survey.DefinitionRow> result = new List<Survey.DefinitionRow>();

            foreach (Survey.LogRow logRow in Log) {
                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow)) {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public Survey.DefinitionRow[] GetSpeciesWithUnweightedIndividuals() {

            List<Survey.DefinitionRow> result = new List<Survey.DefinitionRow>();

            foreach (Survey.IndividualRow individualRow in Individual) {
                if (!individualRow.IsMassNull()) continue;
                if (!result.Contains(individualRow.LogRow.DefinitionRow)) {
                    result.Add(individualRow.LogRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }

        public Survey.DefinitionRow[] GetSpeciesForWeightRecovery() {

            List<DefinitionRow> result = new List<DefinitionRow>();

            foreach (LogRow logRow in Log) {

                if (!logRow.IsMassNull()) continue;
                if (!result.Contains(logRow.DefinitionRow)) {
                    result.Add(logRow.DefinitionRow);
                }
            }

            return result.ToArray();
        }


        public override string ToString() {
            return string.Format("{0}: {1} cards", Name, Count);
        }

        public string ToString(string format) {
            return string.Format("{0}: {1} cards", Name, Count);

        }

        public string ToString(string format, IFormatProvider provider) {
            return string.Format("{0}: {1} cards", Name, Count);
        }



        public void AddCommon(Report report, string[] prompts, string[] values, string[] notices) {
            List<string> _prompts = new List<string>();
            List<string> _values = new List<string>();

            _prompts.Add(Resources.Reports.Prompt.Name);
            _prompts.Add(Resources.Reports.Prompt.Signatures);
            _prompts.Add(Resources.Reports.Prompt.Waters);
            _prompts.Add(Resources.Reports.Prompt.Dates);
            _prompts.Add(Resources.Reports.Prompt.Samplers);
            _prompts.AddRange(prompts);

            _values.Add(this.FriendlyName);
            _values.Add(this.GetInvestigators().Merge());
            _values.Add(this.GetWaterNames().Merge());
            _values.Add(this.GetDates().GetDatesDescription());
            _values.Add(this.GetSamplersList().Merge());
            _values.AddRange(values);

            Report.Table t = Report.Table.GetLinedTable(_prompts.ToArray(), _values.ToArray(), notices);
            t.MarkNotices = false;
            report.AddTable(t, "fill");
        }

        public void AddCommon(Report report, string[] prompts, string[] values) => this.AddCommon(report, prompts, values, new string[] { });

        public void AddCommon(Report report, string prompt, string value) => this.AddCommon(report, new string[] { prompt }, new string[] { value });

        public void AddCommon(Report report, string[] notices) => this.AddCommon(report, new string[] { }, new string[] { }, notices);

        public void AddCommon(Report report) => this.AddCommon(report, new string[] { }, new string[] { });

        public Report GetCardReport(CardReportLevel level) {

            Report report = new Report(string.Empty);

            bool first = true;
            foreach (Survey.CardRow cardRow in this) {
                if (first) { first = false; } else { report.BreakPage(ReaderSettings.OddCardStart ? PageBreakOption.Odd : PageBreakOption.None); }
                report.AddHeader(cardRow.FriendlyPath);
                cardRow.AddReport(report, level);
            }

            report.End();

            return report;
        }


        public void PopulateSpeciesMenu(ToolStripMenuItem item, EventHandler command, Func<TaxonomicIndex.TaxonRow, int> resultsCounter) {
            for (int i = 0; i < item.DropDownItems.Count; i++) {
                if (item.DropDownItems[i].Tag != null) {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator)) {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            int added = 0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in this.GetSpecies()) {
                int s = resultsCounter.Invoke(speciesRow);

                if (s != 0) {
                    ToolStripItem _item = new ToolStripMenuItem {
                        Tag = speciesRow
                    };
                    string txt = speciesRow.ToString("s");
                    _item.Text = s == -1 ? txt : string.Format("{0} ({1})", txt, s);
                    _item.Click += command;
                    item.DropDownItems.Add(_item);
                    added++;
                }
            }

            if (added == 0) {
                item.Enabled = false;
            }

            // If no item added - remove separator, main item and set parent enabled to false;

        }

        public void PopulateSpeciesMenu(ToolStripMenuItem item, EventHandler command) {
            PopulateSpeciesMenu(item, command, (s) => { return -1; });
        }
    }
}