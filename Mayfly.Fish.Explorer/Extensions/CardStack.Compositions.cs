using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.Data;
using Meta.Numerics.Statistics;
using Mayfly.Fish.Explorer;
using System.Linq;
using Meta.Numerics;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        //public static SpeciesComposition GetCenosisComposition(this CardStack stack)
        //{
        //    SpeciesComposition result = stack.GetBasicCenosisComposition();

        //    foreach (SpeciesSwarm category in result)
        //    {
        //        category.Abundance = stack.GetAverageAbundance(category.SpeciesRow);
        //        category.Biomass = stack.GetAverageBiomass(category.SpeciesRow);
        //    }

        //    return result;
        //}

        public static SpeciesComposition GetCenosisComposition(this CardStack stack, ExpressionVariant variant)
        {
            SpeciesComposition result = stack.GetBasicCenosisComposition();

            foreach (SpeciesSwarm category in result)
            {
                category.Abundance = stack.GetAverageAbundance(category.SpeciesRow, variant);
                category.Biomass = stack.GetAverageBiomass(category.SpeciesRow, variant);
            }

            return result;
        }

        public static SpeciesComposition GetCenosisComposition(this CardStack stack, SpeciesComposition example)
        {
            SpeciesComposition result = example.GetEmptyCopy();

            for (int i = 0; i < example.Count; i++)
            {
                result.AddCategory(stack.GetSwarm(result[0].SpeciesRow));
                result.RemoveAt(0);
            }

            result.SamplesCount = stack.Count;

            return result;
        }



        public static Composition GetComposition(this CardStack stack, Data.SpeciesRow speciesRow, Composition example)
        {
            if (example is LengthComposition)
            {
                return stack.GetLengthComposition(speciesRow, 
                    ((LengthComposition)example).Interval,
                    ((LengthComposition)example).Minimum,
                    ((LengthComposition)example).Maximum);
            }
            else if (example is AgeComposition)
            {
                return stack.GetAgeComposition(speciesRow,
                    ((AgeComposition)example).Youngest,
                    ((AgeComposition)example).Oldest);
            }
            else
            {
                return new Composition(example);
            }
        }



        public static LengthComposition GetLengthCompositionFrame(this CardStack stack, Data.SpeciesRow speciesRow, double interval)
        {
            return new LengthComposition(speciesRow.Species,
                stack.LengthMin(speciesRow), stack.LengthMax(speciesRow),
                interval);
        }

        public static LengthComposition GetLengthComposition(this CardStack stack, Data.SpeciesRow speciesRow, double interval)
        {
            return stack.GetLengthComposition(speciesRow, 
                interval,
                stack.LengthMin(speciesRow),
                stack.LengthMax(speciesRow));
        }

        public static LengthComposition GetLengthComposition(this CardStack stack, Data.SpeciesRow speciesRow, 
            double interval, double min, double max)
        {
            LengthComposition result = stack.GetStatisticComposition(speciesRow, interval, min, max, (s, i) => { return stack.Quantity(s, i); }, speciesRow.Species);

            foreach (SizeClass group in result)
            {
                //group.Quantity = stack.Quantity(speciesRow, group.Size);
                //if (group.Quantity == 0) continue;
                group.MassSample = stack.MassSample(speciesRow, group.Size);
                double w = group.MassSample.Count > 0 ? group.MassSample.Mean :
                   stack.Parent.FindMassModel(speciesRow.Species).GetValue(group.Size.Midpoint);
                group.Mass = group.Quantity > 0 ? group.Quantity * w / 1000.0 : 0;
                group.SetSexualComposition(
                    stack.Quantity(speciesRow, group.Size, Sex.Juvenile),
                    stack.Quantity(speciesRow, group.Size, Sex.Male),
                    stack.Quantity(speciesRow, group.Size, Sex.Female));
            }

            //if (max < min)
            //    throw new AgeArgumentException("Wrong length limits");

            //LengthComposition result = new LengthComposition(speciesRow.Species, 
            //    min, max, interval);

            //foreach (SizeClass group in result)
            //{
            //    group.Quantity = stack.Quantity(speciesRow, group.Size);
            //    if (group.Quantity == 0) continue;
            //    group.MassSample = stack.MassSample(speciesRow, group.Size);
            //    double w = group.MassSample.Count > 0 ? group.MassSample.Mean :
            //        Parent.WeightModels.GetValue(speciesRow.Species, group.Size.Midpoint);
            //    group.Mass = group.Quantity > 0 ? group.Quantity * w / 1000.0 : 0;
            //    group.SetSexualComposition(
            //        stack.Quantity(speciesRow, group.Size, Sex.Juvenile),
            //        stack.Quantity(speciesRow, group.Size, Sex.Male), 
            //        stack.Quantity(speciesRow, group.Size, Sex.Female));
            //}

            return result;
        }

        public static LengthComposition GetStatisticComposition(this CardStack stack, Data.SpeciesRow speciesRow, 
            double interval, double min, double max, 
            Func<Data.SpeciesRow, Interval, int> counter, string name)
        {
            return LengthComposition.Get(interval, min, max, (size) => { return counter.Invoke(speciesRow, size); }, name);

            //if (max < min)
            //    throw new AgeArgumentException("Wrong length limits");

            //LengthComposition result = new LengthComposition(name, 
            //    min, max, interval);

            //foreach (SizeClass group in result)
            //{
            //    group.Quantity = counter.Invoke();
            //}

            //return result;
        }

        public static LengthComposition GetStatisticComposition(this CardStack stack, Data.SpeciesRow speciesRow, 
            double interval, 
            Func<Data.SpeciesRow, Interval, int> counter, string name)
        {
            return stack.GetStatisticComposition(speciesRow, 
                interval,
                stack.LengthMin(speciesRow),
                stack.LengthMax(speciesRow), counter, name);
        }

        public static LengthComposition GetStatisticComposition(this CardStack stack, Data.SpeciesRow speciesRow,  
            Func<Data.SpeciesRow, Interval, int> counter, string name)
        {
            return stack.GetStatisticComposition(speciesRow, 
                UserSettings.SizeInterval,
                stack.LengthMin(speciesRow),
                stack.LengthMax(speciesRow), counter, name);
        }
         


        public static AgeComposition GetAgeCompositionFrame(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            return new AgeComposition(speciesRow.Species,
                stack.AgeMin(speciesRow), stack.AgeMax(speciesRow));
        }

        public static AgeComposition GetAgeComposition(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            return stack.GetAgeComposition(speciesRow,
                stack.AgeMin(speciesRow, true),
                stack.AgeMax(speciesRow, true));
        }

        public static AgeKey GetAgeComposition(this CardStack stack, Data.SpeciesRow speciesRow, Age start, Age end)
        {
            if (end <= start)
                throw new AgeArgumentException("Wrong age limits");

            AgeKey result = new AgeKey(stack.Name, start, end, 
                stack.LengthMin(speciesRow), stack.LengthMax(speciesRow), UserSettings.SizeInterval);

            result.Fill(stack, speciesRow);
            result.Weight = stack.GetEffort();

            foreach (AgeGroup group in result)
            {
                group.SetSexualComposition(
                    stack.Quantity(speciesRow, Sex.Juvenile, group.Age),
                    stack.Quantity(speciesRow, Sex.Male, group.Age),
                    stack.Quantity(speciesRow, Sex.Female, group.Age));
                group.LengthSample = stack.LengthSample(speciesRow, group.Age);
                group.MassSample = stack.MassSample(speciesRow, group.Age);
            }

            return result;
        }

        public static AgeComposition GetSampleAgeComposition(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            AgeComposition result = new AgeComposition(speciesRow.Species,
                stack.AgeMin(speciesRow, false), stack.AgeMax(speciesRow, false));

            foreach (AgeGroup group in result)
            {
                group.Quantity = stack.Quantity(speciesRow, group.Age);
                if (group.Quantity == 0) continue;
                group.MassSample = stack.MassSample(speciesRow, group.Age);
                group.Mass = group.Quantity > 0 ? group.Quantity * group.MassSample.Mean / 1000.0 : 0;
                group.SetSexualComposition(
                    stack.Quantity(speciesRow, Sex.Juvenile, group.Age),
                    stack.Quantity(speciesRow, Sex.Male, group.Age),
                    stack.Quantity(speciesRow, Sex.Female, group.Age));
                group.LengthSample = stack.LengthSample(speciesRow, group.Age);
            }

            return result;
        }

        public static List<Composition> GetAnnualAgeCompositions(this CardStack stack, Data.SpeciesRow speciesRow,
            FishSamplerType samplerType, GearWeightType weightType, ExpressionVariant variant)
        {
            List<CardStack> annualStacks = new List<CardStack>();

            foreach (int year in stack.GetYears())
            {
                CardStack annualSurvey = (CardStack)stack.GetStack(year);
                annualStacks.Add(annualSurvey);
            }

            return stack.GetAnnualAgeCompositions(speciesRow, samplerType, annualStacks.ToArray(), weightType, variant);
        }

        public static List<Composition> GetAnnualAgeCompositions(this CardStack stack, Data.SpeciesRow speciesRow,
            FishSamplerType samplerType, CardStack[] annualStacks, GearWeightType weightType, ExpressionVariant variant)
        {
            List<Composition> result = new List<Composition>();

            AgeComposition example = stack.GetAgeCompositionFrame(speciesRow);

            foreach (CardStack annualSurvey in annualStacks)
            {
                Composition cross;

                if (samplerType == FishSamplerType.None)
                {
                    cross = annualSurvey.GetAgeComposition(speciesRow, example.Youngest, example.Oldest);
                }
                else
                {
                    CardStack[] stacks = annualSurvey.GetClassedStacks(samplerType);
                    cross = stacks.GetWeightedComposition(weightType, variant, example, speciesRow, annualSurvey.Mass(speciesRow));
                }

                if (cross.TotalQuantity == 0) continue;

                cross.Name = annualSurvey.Name;
                result.Add(cross);
            }

            result.Sort();
            return result;
        }

        public static List<Cohort> GetCohorts(this CardStack stack, Data.SpeciesRow speciesRow, FishSamplerType samplerType, GearWeightType weightType, ExpressionVariant variant)
        {
            Composition[] annualCompositions = stack.GetAnnualAgeCompositions(speciesRow, samplerType, weightType, variant).ToArray();
            return annualCompositions.GetCohorts();
        }




        public static Composition GetSexualCompositionFrame(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            Composition result = new Composition(speciesRow.Species);

            foreach (Sex sex in new Sex[] { Sex.Juvenile, Sex.Male, Sex.Female })
            {
                Category category = new Category(sex.ToString());
                result.AddCategory(category);
            }

            return result;
        }

        public static Composition GetSexualComposition(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            Composition result = new Composition(speciesRow.Species);

            foreach (Sex sex in new Sex[] { Sex.Juvenile, Sex.Male, Sex.Female })
            {
                int q = stack.Quantity(speciesRow, sex);

                Category group = new Category(sex.ToString());
                group.Quantity = q;
                group.LengthSample = stack.LengthSample(speciesRow, sex);
                group.MassSample = stack.MassSample(speciesRow, sex);
                double w = group.MassSample.Count > 0 ? group.MassSample.Mean :
                    double.NaN;
                group.Mass = group.Quantity > 0 ? group.Quantity * w / 1000.0 : 0;
                result.AddCategory(group);
            }

            return result;
        }


        //public static SpeciesComposition GetClassedComposition(this CardStack[] classedStacks, Data.SpeciesRow speciesRow, FishSamplerType samplerType, ExpressionVariant variant)
        //{
        //    SpeciesComposition result = new SpeciesComposition();

        //    if (classedStacks.Length > 1)
        //    {
        //        foreach (string mesh in classes)
        //        {
        //            CardStack meshData = stack.GetStack(samplerType, mesh);

        //            double q = meshData.Quantity(speciesRow);

        //            if (q == 0) continue;

        //            SpeciesSwarm swarm = meshData.GetSwarm(speciesRow);
        //            swarm.Abundance = meshData.GetAverageAbundance(speciesRow);
        //            swarm.Biomass = meshData.GetAverageBiomass(speciesRow);
        //            swarm.Name = mesh;

        //            result.AddCategory(swarm);
        //        }
        //    }
        //    else
        //    {
        //        SpeciesSwarm swarm = samplerData.GetSwarm(speciesRow);
        //        swarm.Abundance = samplerData.GetAverageAbundance(speciesRow);
        //        swarm.Biomass = samplerData.GetAverageBiomass(speciesRow);

        //        result.AddCategory(swarm);
        //    }

        //    result.Unit = new UnitEffort(samplerType, variant).Unit;
        //    return result;
        //}


        public static SpeciesComposition GetClassedComposition(this IEnumerable<CardStack> classedStacks, Data.SpeciesRow speciesRow, FishSamplerType samplerType, UnitEffort ue)
        {
            SpeciesComposition result = new SpeciesComposition();

            if (classedStacks.Count() > 1)
            {
                foreach (CardStack meshData in classedStacks)
                {
                    //double q = meshData.Quantity(speciesRow);

                    //if (q == 0) continue;

                    SpeciesSwarm swarm = meshData.GetSwarm(speciesRow);
                    swarm.Index = meshData.GetEffort(samplerType, ue.Variant);
                    //swarm.Abundance = meshData.GetAverageAbundance(speciesRow);
                    //swarm.Biomass = meshData.GetAverageBiomass(speciesRow);
                    swarm.Name = meshData.Name;

                    result.AddCategory(swarm);
                }
            }
            else
            {
                SpeciesSwarm swarm = classedStacks.First().GetSwarm(speciesRow);
                swarm.Index = classedStacks.First().GetEffort(samplerType, ue.Variant);
                swarm.Name = classedStacks.First().Name;

                result.AddCategory(swarm);
            }

            result.Unit = ue.Unit;
            return result;
        }



        public static CompositionEqualizer GetWeightedComposition(this CardStack[] classedStacks,
            GearWeightType weight, ExpressionVariant variant, Composition example, Data.SpeciesRow speciesRow)
        {
            CompositionEqualizer result = new CompositionEqualizer(example);

            foreach (CardStack classedStack in classedStacks)
            {
                Composition classComposition =
                    classedStack.GetSpecies().Contains(speciesRow) ?
                    classedStack.GetComposition(speciesRow, example) :
                    new Composition(example);
                classComposition.Name = classedStack.Name;
                classComposition.Weight =
                    (weight.HasFlag(GearWeightType.Effort) ? classedStack.GetEffort(classedStack.GetSamplers()[0].GetSamplerType(), variant) : 1) /
                    (weight.HasFlag(GearWeightType.SpatialRatio) ? Service.GetGearSpatialValue(classedStack.GetSamplers()[0].GetSamplerType(), classedStack.Name) : 1);

                if (double.IsNaN(classComposition.Weight)) continue;

                classComposition.ScaleUp(classedStack.Mass(speciesRow));
                result.AddComposition(classComposition);
            }

            result.GetWeighted();

            return result;
        }

        public static CompositionEqualizer GetWeightedComposition(this CardStack[] classedStacks,
            GearWeightType weight, ExpressionVariant variant, Composition example, Data.SpeciesRow speciesRow, double totalMass)
        {
            CompositionEqualizer result = classedStacks.GetWeightedComposition(weight, variant, example,
                speciesRow);
            result.ScaleUp(totalMass);
            return result;
        }



        public static CompositionEqualizer GetWeightedComposition(this CardStack[] classedStacks,
            GearWeightType weight, ExpressionVariant variant, SpeciesComposition example)
        {
            CompositionEqualizer result = new CompositionEqualizer(example);

            foreach (CardStack classedStack in classedStacks)
            {
                Composition classComposition = GetCenosisComposition(classedStack, example);
                classComposition.Name = classedStack.Name;
                classComposition.Weight =
                    (weight.HasFlag(GearWeightType.Effort) ? classedStack.GetEffort(classedStack.GetSamplers()[0].GetSamplerType(), variant) : 1) /
                    (weight.HasFlag(GearWeightType.SpatialRatio) ? Service.GetGearSpatialValue(classedStack.GetSamplers()[0].GetSamplerType(), classedStack.Name) : 1);

                result.AddComposition(classComposition);
            }

            result.GetWeighted();

            return result;
        }

        public static Category[] GetWeightedCatches(this CardStack[] classedStacks,
            GearWeightType weight, FishSamplerType st, ExpressionVariant ev, Data.SpeciesRow speciesRow)
        {
            List<Category> result = new List<Category>();

            foreach (CardStack classedStack in classedStacks)
            {
                Category cat = new Category(classedStack.Name);
                cat.Quantity = classedStack.Quantity(speciesRow);
                cat.Mass = classedStack.Mass(speciesRow);
                cat.Index =
                    (weight.HasFlag(GearWeightType.Effort) ? classedStack.GetEffort(st, ev) : 1) /
                    (weight.HasFlag(GearWeightType.SpatialRatio) ? Service.GetGearSpatialValue(classedStack.GetSamplers()[0].GetSamplerType(), classedStack.Name) : 1);
                result.Add(cat);
            }

            return result.ToArray();
        }
    }

    [Flags]
    public enum GearWeightType
    {
        None,
        Effort,
        SpatialRatio
    }
}
