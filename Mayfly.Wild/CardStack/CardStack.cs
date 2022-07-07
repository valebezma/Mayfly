﻿using Mayfly.Extensions;
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
    public partial class CardStack : List<Data.CardRow>
    {
        public Data Parent 
        {
            get
            {
                return this.Count == 0 ? null : (Data)this[0].Table.DataSet;
            }
        }

        public string CommonPath 
        {
            get
            {
                return IO.GetCommonPath(this.GetFilenames());
            }
        }

        public string FriendlyName 
        {
            get
            {
                string result = System.IO.Path.GetFileName(this.CommonPath);

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = Mayfly.Resources.Interface.VariousSources;
                }

                return result;
            }
        }

        public string Name { get; set; }

        public bool IsEmpty 
        {
            get
            {
                return Parent == null;// GetLogRows().Length == 0;
            }
        }

        public int SpeciesWealth 
        {
            get
            {
                return GetSpecies().Length;
            }
        }



        public CardStack() { }

        public CardStack(IEnumerable<Data.CardRow> cardRows)
            : this()
        {
            //this.Parent = ;
            this.AddRange(cardRows);
            this.Name = string.Format("{0}; {1}.",
                this.GetWaterNames().Merge(),
                this.GetDates().GetDatesDescription());
        }

        public CardStack(Data data)
            : this(data.Card)
        { }


        public string ToShortDescription()
        {
            return string.Format("{0}; {1}; {2}.",
                this.GetInvestigators().Merge(),
                this.GetWaterNames().Merge(),
                this.GetDates().GetDatesDescription()
                );
        }

        public string[] GetFilenames()
        {
            List<string> filenames = new List<string>();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.Path == null) continue;
                if (!filenames.Contains(cardRow.Path))
                    filenames.Add(cardRow.Path);
            }

            return filenames.ToArray();
        }



        public void Merge(CardStack stack)
        {
            foreach (Data.CardRow cardRow in stack)
            {
                if (!this.Contains(cardRow))
                {
                    this.Add(cardRow);
                }
            }
        }



        public List<string> GetSpeciesList()
        {
            List<string> result = new List<string>();

            foreach (Data.LogRow logRow in this.GetLogRows())
            {
                if (!result.Contains(logRow.DefinitionRow.Taxon))
                {
                    result.Add(logRow.DefinitionRow.Taxon);
                }
            }

            return result;
        }

        public List<string> GetSpeciesList(CardStack stackToCommon)
        {
            List<string> list1 = this.GetSpeciesList();
            List<string> list2 = stackToCommon.GetSpeciesList();

            List<string> result = new List<string>();

            foreach (string species in list1)
            {
                if (list2.Contains(species))
                {
                    result.Add(species);
                }
            }

            return result;
        }

        public Data.LogRow[] GetLogRows()
        {
            List<Data.LogRow> result = new List<Data.LogRow>();

            if (Parent != null)
            {
                foreach (Data.LogRow logRow in Parent.Log)
                {
                    if (!this.Contains(logRow.CardRow)) continue;
                    result.Add(logRow);
                }
            }

            return result.ToArray();
        }

        public Data.LogRow[] GetLogRows(TaxonomicIndex.TaxonRow speciesRow)
        {
            List<Data.LogRow> result = new List<Data.LogRow>();

            if (Parent != null && speciesRow != null)
            {
                foreach (Data.LogRow logRow in Parent.Log)
                {
                    if (!speciesRow.Validate(logRow.DefinitionRow.Taxon)) continue;
                    if (!this.Contains(logRow.CardRow)) continue;
                    result.Add(logRow);
                }
            }

            return result.ToArray();
        }

        public Data.IndividualRow[] GetIndividualRows()
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.LogRow logRow in this.GetLogRows())
            {
                result.AddRange(logRow.GetIndividualRows());
            }

            return result.ToArray();
        }

        public Data.IndividualRow[] GetIndividualRows(TaxonomicIndex.TaxonRow speciesRow)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                result.AddRange(logRow.GetIndividualRows());
            }

            return result.ToArray();
        }
        

        
        public Data.WaterRow[] GetWaters()
        {
            List<Data.WaterRow> result = new List<Data.WaterRow>();
            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWaterIDNull()) continue;
                if (result.Contains(cardRow.WaterRow)) continue;
                result.Add(cardRow.WaterRow);
            }
            return result.ToArray();
        }

        public string[] GetWaterNames()
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWaterIDNull()) continue;
                string waterDescription = cardRow.WaterRow.Presentation;
                if (result.Contains(waterDescription)) continue;
                result.Add(waterDescription);
            }
            return result.ToArray();
        }


        public string[] GetInvestigators()
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in this)
            {
                string investigator = cardRow.Investigator;
                if (result.Contains(investigator)) continue;
                result.Add(investigator);
            }
            return result.ToArray();
        }


        public DateTime[] GetDates()
        {
            List<DateTime> result = new List<DateTime>();
            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWhenNull()) continue;
                if (result.Contains(cardRow.When.Date)) continue;
                result.Add(cardRow.When.Date);
            }
            result.Sort();
            return result.ToArray();
        }

        public DateTime EarliestEvent
        {
            get
            {
                DateTime result = DateTime.Now;

                foreach (Data.CardRow cardRow in this)
                {
                    if (cardRow.WhenStarted < result) result = cardRow.WhenStarted;
                }

                return result;
            }
        }

        public DateTime LatestEvent
        {
            get
            {
                DateTime result = DateTime.FromOADate(0.0);

                foreach (Data.CardRow cardRow in this)
                {
                    if (cardRow.When > result) result = cardRow.When;
                }

                return result;
            }
        }

        public Waypoint[] GetLocations()
        {
            List<Waypoint> result = new List<Waypoint>();

            foreach (Data.CardRow cardRow in this)
            {
                if (!(cardRow.Position == null))
                {
                    result.Add(cardRow.Position);
                }
            }

            return result.ToArray();
        }

        public int[] GetYears()
        {
            return GetDates().GetYears();
        }

                      

        public static CardStack ConvertFrom(Data data)
        {
            return new CardStack(data.Card);
        }

        public CardStack GetStack(DateTime day)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWhenNull()) continue;
                if (cardRow.When.Date != day.Date) continue;

                result.Add(cardRow);
            }

            result.Name = day.ToShortDateString();

            return result;
        }

        public CardStack GetStack(DateTime from, DateTime to)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWhenNull()) continue;
                if (cardRow.When.Date < from) continue;
                if (cardRow.When.Date > to) continue;
                result.Add(cardRow);
            }

            return result;
        }

        public CardStack GetStack(int year)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWhenNull()) continue;
                if (cardRow.When.Year != year) continue;

                result.Add(cardRow);
            }

            result.Name = year.ToString();

            return result;
        }

        public CardStack GetStack(Samplers.SamplerRow samplerRow)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsSamplerNull()) continue;
                if (cardRow.Sampler != samplerRow.ID) continue;

                result.Add(cardRow);
            }

            return result;
        }

        public CardStack GetStack(Data.WaterRow waterRow)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsWaterIDNull()) continue;
                if (cardRow.WaterRow != waterRow) continue;

                result.Add(cardRow);
            }

            return result;
        }

        public CardStack GetStack(string field, object value)
        {
            return GetStack(field, value, string.Empty);
        }

        public CardStack GetStack(string field, object value, string format)
        {
            CardStack result = new CardStack();

            foreach (Data.CardRow cardRow in this)
            {
                string valueFormatted = value.Format(format);
                string cardFormatted = cardRow.GetValue(field).Format(format);

                if (object.Equals(cardFormatted, valueFormatted))
                    result.Add(cardRow);
            }

            return result;
        }


        public TaxonomicIndex.TaxonRow[] GetSpecies()
        {
            return GetSpecies(0);
        }

        public TaxonomicIndex.TaxonRow[] GetSpecies(int minimalSample)
        {
            List<TaxonomicIndex.TaxonRow> result = new List<TaxonomicIndex.TaxonRow>();

            if (Parent != null)
            {
                foreach (Data.DefinitionRow spcRow in Parent.Definition)
                {
                    TaxonomicIndex.TaxonRow currentRecord = spcRow.KeyRecord;

                    if (currentRecord == null)
                    {
                        TaxonomicIndex.TaxonRow newSpcRow = ReaderSettings.TaxonomicIndex.Taxon.NewTaxonRow(spcRow.Rank, spcRow.Taxon);
                        currentRecord = newSpcRow;
                    }

                    if (minimalSample > 0 && Quantity(currentRecord) < minimalSample) continue;

                    if (!result.Contains(currentRecord))
                    {
                        result.Add(currentRecord);
                    }
                }

                result.Sort();
            }

            return result.ToArray();
        }

        public int GetOccurrenceCases(TaxonomicIndex.TaxonRow[] speciesRows)
        {
            int result = 0;

            foreach (Data.CardRow cardRow in this)
            {
                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    foreach (TaxonomicIndex.TaxonRow speciesRow in speciesRows)
                    {
                        if (speciesRow.Validate(logRow.DefinitionRow.Taxon))
                        {
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



        public Samplers.SamplerRow[] GetSamplers()
        {
            List<Samplers.SamplerRow> result = new List<Samplers.SamplerRow>();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsSamplerNull()) continue;
                if (cardRow.SamplerRow == null) continue;
                if (result.Contains(cardRow.SamplerRow)) continue;
                result.Add(cardRow.SamplerRow);
            }

            return result.ToArray();
        }

        public string[] GetSamplersList()
        {
            List<string> result = new List<string>();

            foreach (Samplers.SamplerRow samplerRow in this.GetSamplers())
            {
                result.Add(samplerRow.Sampler);
            }

            return result.ToArray();
        }              

        

        public override string ToString()
        {
            return string.Format("{0}: {1} cards", Name, Count);
        }

        public string ToString(string format)
        {
            return string.Format("{0}: {1} cards", Name, Count);

        }

        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0}: {1} cards", Name, Count);
        }



        public void AddCommon(Report report, string[] prompts, string[] values, string[] notices)
        {
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


        public void PopulateSpeciesMenu(ToolStripMenuItem item, EventHandler command, Func<TaxonomicIndex.TaxonRow, int> resultsCounter)
        {
            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i].Tag != null)
                {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator))
            {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            int added = 0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in this.GetSpecies())
            {
                int s = resultsCounter.Invoke(speciesRow);

                if (s != 0)
                {
                    ToolStripItem _item = new ToolStripMenuItem
                    {
                        Tag = speciesRow
                    };
                    string txt = speciesRow.ToString("s");
                    _item.Text = s == -1 ? txt : string.Format("{0} ({1})", txt, s);
                    _item.Click += command;
                    item.DropDownItems.Add(_item);
                    added++;
                }
            }

            if (added == 0)
            {
                item.Enabled = false;
            }

            // If no item added - remove separator, main item and set parent enabled to false;

        }

        public void PopulateSpeciesMenu(ToolStripMenuItem item, EventHandler command)
        {
            PopulateSpeciesMenu(item, command, (s) => { return -1; });
        }
    }
}