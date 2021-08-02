using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public static class CohortExtensions
    {
        internal static Cohort GetCohort(this List<Cohort> cohorts, int birth)
        {
            foreach (Cohort cohort in cohorts)
            {
                if (cohort.Birth == birth)
                    return cohort;
            }

            return null;
        }

        internal static Cohort GetCohort(this List<Cohort> cohorts, int birth, ref AgeComposition frame)
        {
            Cohort result = cohorts.GetCohort(birth);

            if (result == null)
            {
                result = new Cohort(birth, frame);
                cohorts.Add(result);
            }

            return result;
        }

        public static List<Cohort> GetCohorts(this Composition[] annualCompositions)
        {
            Composition example = annualCompositions[0];
            AgeComposition ac = new AgeComposition(string.Empty,
                ((AgeGroup)example[0]).Age, ((AgeGroup)example.Last()).Age);
            return annualCompositions.GetCohorts(ac);
        }

        public static List<Cohort> GetCohorts(this Composition[] annualCompositions, AgeComposition example)
        {
            List<Cohort> result = new List<Cohort>();
            int youngest = example.Youngest.Years;

            foreach (Composition cross in annualCompositions)
            {
                int year = int.Parse(cross.Name);

                // Distribute to cohorts
                foreach (AgeGroup ageGroup in cross)
                {
                    int birth = year - ageGroup.Age.Years;
                    result.GetCohort(birth, ref example)[ageGroup.Age.Years - youngest] = ageGroup;
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].TotalQuantity == 0)
                {
                    result.RemoveAt(i);
                    i--;
                }
            }

            result.Sort();
            return result;
        }
    }
}
