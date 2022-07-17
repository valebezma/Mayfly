using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Meta.Numerics;
using Mayfly.Extensions;
using System.Windows.Forms;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static Wild.Survey.IndividualRow[] GetIndividuals(this CardStack stack, TaxonomicIndex.TaxonRow spcRow, string field, object value)
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            foreach (Wild.Survey.IndividualRow indRow in stack.GetIndividualRows(spcRow))
            {
                if (indRow[field].Equals(value))
                {
                    result.Add(indRow);
                }
            }

            return result.ToArray();
        }

        public static Wild.Survey.IndividualRow[] GetIndividuals(this CardStack stack, TaxonomicIndex.TaxonRow spcRow, string[] field, object[] value)
        {
            List<Wild.Survey.IndividualRow[]> packs = new List<Wild.Survey.IndividualRow[]>();
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            for (int i = 0; i < field.Length; i++)
            {
                Wild.Survey.IndividualRow[] pack = stack.GetIndividuals(spcRow, field[i], value[i]);
                packs.Add(pack);

                foreach (Wild.Survey.IndividualRow indRow in pack)
                {
                    if (!result.Contains(indRow))
                    {
                        result.Add(indRow);
                    }
                }
            }

            for (int i = 0; i < result.Count; i++)// (Data.IndividualRow indRow in result)
            {
                foreach (Wild.Survey.IndividualRow[] pack in packs)
                {
                    if (!pack.Contains(result[i]))
                    {
                        result.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            return result.ToArray();
        }






        public static Samplers.SamplerRow[] GetSamplers(this CardStack stack)
        {
            List<Samplers.SamplerRow> result = new List<Samplers.SamplerRow>();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.IsSamplerNull()) continue;
                if (result.Contains(cardRow.SamplerRow)) continue;
                result.Add(cardRow.SamplerRow);
            }

            return result.ToArray();
        }

        public static string[] GetSamplersList(this CardStack stack)
        {
            List<string> result = new List<string>();

            foreach (Samplers.SamplerRow samplerRow in stack.GetSamplers())
            {
                string sampler = samplerRow.Sampler + " ";
                sampler += stack.SamplerClasses(samplerRow).Merge(", ");
                result.Add(sampler.Trim());
            }

            return result.ToArray();
        }

        public static FishSamplerType[] GetSamplerTypes(this CardStack stack)
        {
            List<FishSamplerType> result = new List<FishSamplerType>();

            foreach (Samplers.SamplerRow samplerRow in stack.GetSamplers())
            {
                FishSamplerType type = samplerRow.GetSamplerType();
                if (result.Contains(type)) continue;
                result.Add(type);
            }

            result.Sort();
            return result.ToArray();
        }

        public static FishSamplerTypeDisplay[] GetSamplerTypeDisplays(this CardStack stack)
        {
            List<FishSamplerTypeDisplay> result = new List<FishSamplerTypeDisplay>();

            foreach (FishSamplerType type in stack.GetSamplerTypes())
            {
                result.Add(new FishSamplerTypeDisplay(type));
            }

            return result.ToArray();
        }

        public static string[] Classes(this CardStack stack, FishSamplerType samplerType)
        {
            List<string> result = new List<string>();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;
                string gearClass = cardRow.GetGearClass();
                if (!result.Contains(gearClass)) result.Add(gearClass);
            }

            result.Sort();

            return result.ToArray();
        }

        public static string[] SamplerClasses(this CardStack stack, Samplers.SamplerRow samplerRow)
        {
            List<string> result = new List<string>();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.Sampler != samplerRow.ID) continue;
                string gearClass = cardRow.GetGearClass();
                if (gearClass == string.Empty) continue;
                if (result.Contains(gearClass)) continue;
                result.Add(gearClass);
            }

            result.Sort(new OmniSorter());

            return result.ToArray();
        }

        public static string[] SamplerClasses(this CardStack stack, FishSamplerType samplerType)
        {
            List<string> result = new List<string>();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;
                string _class = cardRow.GetGearClass();
                if (_class == string.Empty) continue;
                if (result.Contains(_class)) continue;
                result.Add(_class);
            }

            result.Sort(new OmniSorter());

            return result.ToArray();
        }


        public static double GetEffort(this CardStack stack)
        {
            double result = 0;

            FishSamplerType samplerType = FishSamplerType.None;

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (samplerType == FishSamplerType.None)
                {
                    samplerType = cardRow.GetGearType();
                }
                else
                {
                    if (cardRow.GetGearType() != samplerType)
                        return double.NaN;
                }

                result += cardRow.GetEffort();
            }

            return result;
        }

        public static double GetEffort(this CardStack stack, FishSamplerType samplerType)
        {
            return stack.GetEffort(samplerType, samplerType.GetDefaultExpression());
        }

        public static double GetEffort(this CardStack stack, FishSamplerType samplerType, EffortExpression expression)
        {
            double result = 0;

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;
                result += cardRow.GetEffort(expression);
                if (double.IsNaN(result)) return double.NaN;
            }
            return result;
        }


        public static double GetTotalAbundance(this CardStack stack)
        {
            double result = 0.0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageAbundance(speciesRow);
            }

            return result;
        }

        public static double GetAverageAbundance(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetAbundance();
            }

            return result / (double)stack.Count;
        }

        public static double GetAverageAbundance(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, EffortExpression variant)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetAbundance(variant);
            }

            return result / (double)stack.Count;
        }

        public static double GetTotalBiomass(this CardStack stack)
        {
            double result = 0.0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                result += stack.GetAverageBiomass(speciesRow);
            }

            return result;
        }

        public static double GetAverageBiomass(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetBiomass();
            }

            return result / (double)stack.Count;
        }

        public static double GetAverageBiomass(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, EffortExpression variant)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.GetBiomass(variant);
            }

            return result / (double)stack.Count;
        }


        public static CardStack GetStack(this CardStack stack, FishSamplerType samplerType)
        {
            CardStack result = new CardStack();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;

                result.Add(cardRow);
            }

            result.Name = samplerType.ToDisplay();

            return result;
        }

        public static CardStack GetStack(this CardStack stack, FishSamplerType samplerType, string gearClass)
        {
            CardStack result = new CardStack();
            foreach (Wild.Survey.CardRow cardRow in stack) {
                if (cardRow.GetGearType() != samplerType) continue;
                if (cardRow.GetGearClass() != gearClass) continue;
                result.Add(cardRow);
            }
            result.Name = gearClass;
            return result;
        }

        public static CardStack GetStack(this CardStack stack, FishSamplerType samplerType, EffortExpression effortVariant)
        {
            CardStack result = new CardStack();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;

                switch (effortVariant)
                {
                    case EffortExpression.Square:
                        if (double.IsNaN(cardRow.GetSquare())) { continue; }
                        break;

                    case EffortExpression.Volume:
                        if (double.IsNaN(cardRow.GetVolume())) { continue; }
                        break;

                    case EffortExpression.Standards:
                        if (double.IsNaN(cardRow.GetStandards())) { continue; }
                        break;
                }

                result.Add(cardRow);
            }

            result.Name = samplerType.ToDisplay() +
                " (" + new UnitEffort(effortVariant).Unit + ")" +
                " (" + Resources.Interface.AllDataCombined + ")";

            return result;
        }
        
        
        public static CardStack[] GetClassedStacks(this CardStack stack, FishSamplerType samplerType)
        {
            List<CardStack> result = new List<CardStack>();
            foreach (string mesh in stack.Classes(samplerType))
            {
                result.Add(stack.GetStack(samplerType, mesh));
            }
            return result.ToArray();
        }

        public static CardStack[] GetClassedStacks(this CardStack stack)
        {
            List<CardStack> result = new List<CardStack>();

            if (stack.Count > 0)
            {
                result.AddRange(stack.GetClassedStacks(stack[0].GetGearType()));
            }

            return result.ToArray();
        }
        

        public static EnvironmentMonitor GetMonitor(this CardStack stack)
        {
            EnvironmentMonitor result = new EnvironmentMonitor();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                if (cardRow.IsEnvironmentDescribed)
                {
                    result.Add(cardRow.EnvironmentState);
                }
            }

            // TODO: Caution if cards are from different places!

            return result;
        }

        public static TreatmentSuggestion GetTreatmentSuggestion(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, System.Data.DataColumn column)
        {
            if (column.Table != stack.Parent.Individual)
                throw new Exception("Column should be of Individuals table.");

            List<Wild.Survey.IndividualRow> untreated = new List<Wild.Survey.IndividualRow>(); // Untreated are specimen with sampe number and studying value

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue; // No length - skip
                if (individualRow.IsTallyNull()) continue; // No sample number - skip
                if (!individualRow.IsNull(column)) continue; // No studying value - skip
                untreated.Add(individualRow);
            }

            if (untreated.Count == 0) return null; // If there is no untreated individuals - end.

            List<Wild.Survey.IndividualRow> primary = new List<Wild.Survey.IndividualRow>(); // Primaries are specimen most urgent to be studied to suggest model nature
            List<Wild.Survey.IndividualRow[]> secondary = new List<Wild.Survey.IndividualRow[]>(); // 
            List<Wild.Survey.IndividualRow> ignore = new List<Wild.Survey.IndividualRow>();

            TreatmentSuggestion result = new TreatmentSuggestion();



            double min = stack.LengthMin(speciesRow);
            double max = stack.LengthMax(speciesRow);
            double interval = UserSettings.SizeInterval;

            int laps = 0;

            // Do until all unregs will be distributed by categories

            while (result.Registered < untreated.Count)
            {
                // Secondaries will be created in each round untill 
                // they will be enough numerous to satisfy Required level

                List<Wild.Survey.IndividualRow> sec = new List<Wild.Survey.IndividualRow>();

                // Along with length range

                for (double l = Mathematics.Service.GetStrate(min, interval).LeftEndpoint; l <= max; l += interval)
                {
                    Interval strate = Interval.FromEndpointAndWidth(l, interval);

                    foreach (Wild.Survey.IndividualRow individualRow in untreated)
                    {
                        // Skip if length is not appropriate

                        if (!strate.LeftClosedContains(individualRow.Length)) continue;


                        // Skip if individual is already in Primaries

                        if (primary.Contains(individualRow)) continue;

                        // If individual is already in one of the secondaries - skip it

                        foreach (Wild.Survey.IndividualRow[] _sec in secondary)
                        {
                            if (_sec.Contains(individualRow)) goto Next;
                        }

                        // Skip is individul is already in ignorables

                        if (ignore.Contains(individualRow)) continue;

                        // Checking model, 
                        // if there is such: checking number of points in it in current interval
                        // If no: just distributing

                        ContinuousBio grBio = stack.Parent.FindGrowthModel(speciesRow.Name);

                        if (grBio != null)
                        {
                            Meta.Numerics.Statistics.Sample lengths = grBio.CombinedData.Data.Y;

                            // First - count number of same strated points in model
                            // If they are zero - add individual to primary
                            // If they are 

                            int modelled = lengths.GetSabsample(strate).Count;

                            if (laps == 0 && modelled == 0) primary.Add(individualRow);
                            else if (laps < UserSettings.RequiredClassSize && modelled < UserSettings.RequiredClassSize) sec.Add(individualRow);
                            else ignore.Add(individualRow);
                        }
                        else
                        {
                            // If it is first lap - fill the Primaries
                            // If Laps is second to Required level - fill current Secondary
                            // Otherwise fill ignorables

                            if (laps == 0) primary.Add(individualRow);
                            else if (laps < UserSettings.RequiredClassSize) sec.Add(individualRow);
                            else ignore.Add(individualRow);
                        }

                        break;

                    Next:
                        continue;
                    }
                }

                if (sec.Count > 0) secondary.Add(sec.ToArray());

                laps++;

                if (result.Primaries.Length != primary.Count) result.Primaries = primary.ToArray();
                if (result.Ignorables.Length != ignore.Count) result.Ignorables = ignore.ToArray();
                result.Secondaries = secondary.ToArray();
            }


            result.SortSuggestions();

            return result;
        }
    }

    public class TreatmentSuggestion
    {
        public Wild.Survey.IndividualRow[] Primaries;

        public Wild.Survey.IndividualRow[][] Secondaries;

        public Wild.Survey.IndividualRow[] Ignorables;

        public int Registered
        {
            get
            {
                int result = 0;
                result += Primaries.Length;
                result += Ignorables.Length;
                for (int i = 0; i < Secondaries.Length; i++)
                {
                    result += Secondaries[i].Length;
                }
                return result;
            }
        }

        public TreatmentSuggestion()
        {
            Primaries = new Wild.Survey.IndividualRow[0];
            Ignorables = new Wild.Survey.IndividualRow[0];
            Secondaries = new Wild.Survey.IndividualRow[0][];
        }



        public void SortSuggestions()
        {
            List<Wild.Survey.IndividualRow> list = new List<Wild.Survey.IndividualRow>(this.Primaries);
            list.Sort(new IndividualRegSorter());
            this.Primaries = list.ToArray();

            for (int i = 0; i < Secondaries.Length; i++)
            {
                list = new List<Wild.Survey.IndividualRow>(this.Secondaries[i]);
                list.Sort(new IndividualRegSorter());
                this.Secondaries[i] = list.ToArray();
            }

            list = new List<Wild.Survey.IndividualRow>(this.Ignorables);
            list.Sort(new IndividualRegSorter());
            this.Ignorables = list.ToArray();

        }

        public static string[] GetTags(Wild.Survey.IndividualRow[] indRows)
        {
            List<string> regs = new List<string>();

            foreach (Wild.Survey.IndividualRow indRow in indRows)
            {
                regs.Add(indRow.Tally);
            }

            regs.Sort(new OmniSorter());

            return regs.ToArray();
        }

        public Wild.Survey.IndividualRow[] GetSuggested()
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();
            result.AddRange(Primaries);
            for (int i = 0; i < Secondaries.Length; i++)
            {
                result.AddRange(Secondaries[i]);
            }
            return result.ToArray();
        }

        public Wild.Survey.IndividualRow[] GetAll()
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();
            result.AddRange(this.GetSuggested());
            result.AddRange(Ignorables);
            return result.ToArray();
        }

        public string GetNote()
        {
            List<string> rec = new List<string>();

            if (this.Registered > 0)
            {
                rec.Add(string.Format(Resources.Reports.Sections.TreatmentSuggestion.Untreated,
                    this.Registered));

                if (this.Primaries.Length > 0)
                {
                    rec.Add(string.Format(Resources.Reports.Sections.TreatmentSuggestion.Pri,
                        TreatmentSuggestion.GetTags(this.Primaries).Merge(", ")));
                }

                if (this.Secondaries.Length > 0)
                {
                    rec.Add(Resources.Reports.Sections.TreatmentSuggestion.Sec_start);

                    for (int i = 0; i < this.Secondaries.Length; i++)
                    {
                        rec.Add(string.Format(Resources.Reports.Sections.TreatmentSuggestion.Sec, i + 2, TreatmentSuggestion.GetTags(this.Secondaries[i]).Merge(", ")));
                    }
                }

                if (this.Ignorables.Length > 0)
                {
                    rec.Add(string.Format(Resources.Reports.Sections.TreatmentSuggestion.Ign,
                        TreatmentSuggestion.GetTags(this.Ignorables).Merge(", ")));
                }

                return rec.Merge(". ");
            }

            return string.Empty;
        }
    }
}
