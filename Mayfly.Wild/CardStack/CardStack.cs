using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using Mayfly.Species;
using Meta.Numerics;

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
                return FileSystem.GetCommonPath(this.GetFilenames());
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



        public Data.SpeciesRow[] GetSpeciesRows()
        {
            List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();

            foreach (Data.SpeciesRow speciesRow in Parent.Species.GetSorted())
            {
                //if (this.Quantity(speciesRow) == 0) continue;

                result.Add(speciesRow);
            }

            return result.ToArray();
        }

        public List<string> GetSpeciesList()
        {
            List<string> result = new List<string>();

            foreach (Data.LogRow logRow in this.GetLogRows())
            {
                if (!result.Contains(logRow.SpeciesRow.Species))
                {
                    result.Add(logRow.SpeciesRow.Species);
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

            foreach (Data.LogRow logRow in Parent.Log)
            {
                if (!this.Contains(logRow.CardRow)) continue;
                result.Add(logRow);
            }

            return result.ToArray();
        }

        public Data.LogRow[] GetLogRows(Data.SpeciesRow speciesRow)
        {
            List<Data.LogRow> result = new List<Data.LogRow>();

            foreach (Data.LogRow logRow in Parent.Log)
            {
                if (logRow.SpeciesRow.Species != speciesRow.Species) continue;
                if (!this.Contains(logRow.CardRow)) continue;
                result.Add(logRow);
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

        public Data.IndividualRow[] GetIndividualRows(Data.SpeciesRow speciesRow)
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



        public Data.SpeciesRow[] GetSpecies()
        {
            List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();

            if (Parent != null)
            {
                foreach (Data.SpeciesRow speciesRow in Parent.Species.GetSorted())
                {
                    if (this.Quantity(speciesRow) == 0) continue;
                    result.Add(speciesRow);
                }
            }

            return result.ToArray();
        }

        public int GetOccurrenceCases(SpeciesKey.SpeciesRow[] speciesRows)
        {
            int result = 0;

            foreach (Data.CardRow cardRow in this)
            {
                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    foreach (SpeciesKey.SpeciesRow speciesRow in speciesRows)
                    {
                        if (speciesRow.Name == logRow.SpeciesRow.Species)
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



        public Samplers.SamplerRow[] GetSamplers(Samplers index)
        {
            List<Samplers.SamplerRow> result = new List<Samplers.SamplerRow>();

            foreach (Data.CardRow cardRow in this)
            {
                if (cardRow.IsSamplerNull()) continue;
                if (result.Contains(cardRow.GetSamplerRow(index))) continue;
                result.Add(cardRow.GetSamplerRow(index));
            }

            return result.ToArray();
        }

        public string[] GetSamplersList(Samplers index)
        {
            List<string> result = new List<string>();

            foreach (Samplers.SamplerRow samplerRow in this.GetSamplers(index))
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



        public void AddCommon(Report report, string samplers, string[] prompts, string[] values, string notice)
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
            _values.Add(samplers);
            _values.AddRange(values);

            report.AddTable(Report.Table.GetLinedTable(_prompts.ToArray(), _values.ToArray()), "fill");
                        
            if (!string.IsNullOrWhiteSpace(notice)) {
                report.AddComment(notice);
            }
        }

        public void AddCommon(Report report, string samplers, string[] prompts, string[] values) => this.AddCommon(report, samplers, prompts, values, string.Empty);

        public void AddCommon(Report report, string samplers, string prompt, string value) => this.AddCommon(report, samplers, new string[] { prompt }, new string[] { value });

        public void AddCommon(Report report, string samplers, string notice) => this.AddCommon(report, samplers, new string[] { }, new string[] { }, notice);

        public void AddCommon(Report report, string samplers) => this.AddCommon(report, samplers, new string[] { }, new string[] { });
    }
}