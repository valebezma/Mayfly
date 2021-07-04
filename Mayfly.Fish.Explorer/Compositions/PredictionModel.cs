using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Meta.Numerics;
using Meta.Numerics.Functions;
using Meta.Numerics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public class Regression
    {
        public VirtualCohort VirtualCohort { get; set; }

        public int ModelYear { get { return VirtualCohort.Birth; } }



        public Regression(VirtualCohort virtualCohort)
        {
            VirtualCohort = virtualCohort;
        }

        public Regression(VirtualPopulation population)
            : this(population.GetPseudoCohort())
        { }



        /// <summary>
        /// Predicts the next season state of specified cohort with conditions of no changes in effort 
        /// </summary>
        /// <param name="cohort">Cohort in its current state</param>
        /// <returns>Predicted state of cohort</returns>
        public Cohort GetPredicted(Cohort cohort)
        {
            return GetPredicted(cohort, 1.0);
        }

        /// <summary>
        /// Predicts the next season state of specified cohort with specified changes in effort
        /// </summary>
        /// <param name="cohort">Cohort in its current state</param>
        /// <param name="impact">Changes in effort (as fraction of current)</param>
        /// <returns>Predicted state of cohort</returns>
        public Cohort GetPredicted(Cohort cohort, double impact)
        {
            Cohort result = cohort.GetNextGeneration();

            // Calculating first age group

            double m = VirtualCohort.NaturalMortality;
            double n = VirtualCohort.Recruitment;
            double w = VirtualCohort.Masses[0];
            double f = cohort.F[0] * impact;
            double z = f + m;
            double c = n * (1 - Math.Exp(-z)) * (f / z);

            result.Survivors[0].Quantity = (int)n;
            result.Survivors[0].Mass = n * w;

            result.F[0] = f;

            result[0].Quantity = (int)c;
            result[0].Mass = c * w;

            // Setting 1 to oldest values

            for (int i = 1; i < cohort.Count; i++)
            {
                f = cohort.F[i] * impact;
                z = f + m;

                result.F[i] = f;

                double t = cohort[i].Age.Years;
                double dt = t - cohort[i - 1].Age.Years;

                n = cohort.Survivors[i - 1].Quantity * Math.Exp(-(cohort.F[i - 1] + m) * dt);

                //double d = (i == (cohort.Count - 1) ? n : (n - n * Math.Exp(-z * dt)));
                //c = d * (f / z);
                c = n * (1 - Math.Exp(-z * dt)) * (f / z);

                w = VirtualCohort.Masses[i];

                result.Survivors[i].Quantity = (int)n;
                result.Survivors[i].Mass = n * w;

                result[i].Quantity = (int)c;
                result[i].Mass = c * w;
            }

            return result;
        }

        /// <summary>
        /// Builds pairs of values date-yeild for specified period
        /// </summary>
        /// <param name="lastYear">Date to which prediction will be processed</param>
        /// <returns></returns>
        public BivariateSample Predict(DateTime lastYear)
        {
            return Predict(lastYear, 1.0);
        }

        /// <summary>
        /// Builds pairs of values date-yeild for specified period, also change in fishing effort is in prospect
        /// </summary>
        /// <param name="lastYear">Date to which prediction will be processed</param>
        /// <param name="impact">Changes in fishing effort in first year</param>
        /// <returns></returns>
        public BivariateSample Predict(DateTime lastYear, double impact)
        {
            BivariateSample changes = new BivariateSample();

            double y = this.VirtualCohort.TotalMass / 1000000.0;
            changes.Add(Service.GetCatchDate(this.VirtualCohort.Birth).ToOADate(), y);

            Cohort changed = this.GetPredicted(this.VirtualCohort, impact);

            while (changed.Birth <= lastYear.Year)
            {
                y = changed.TotalMass / 1000000.0;
                changes.Add(Service.GetCatchDate(changed.Birth).ToOADate(), y);

                changed = this.GetPredicted(changed);
            }

            return changes;
        }
    }
}
