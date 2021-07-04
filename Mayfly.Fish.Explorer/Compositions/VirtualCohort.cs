using Meta.Numerics;
using Meta.Numerics.Analysis;
using System;

namespace Mayfly.Fish.Explorer
{
    public class VirtualCohort : Cohort
    {
        public double NaturalMortality { get { return (m); } set { m = value; } }

        public double Recruitment
        {
            get { return (r); }
            set
            {
                r = value; 
                SetState(this, 1.0);
            }
        }

        public double[] Masses { get; internal set; }

        double r;
        double m;



        public VirtualCohort(Cohort cohort) 
            : base(cohort.Birth, cohort)
        {
            this.Masses = new double[Count];

            for (int i = 0; i < Count; i++) {
                this.Masses[i] = this[i].Mass / this[i].Quantity;
            }
        }


        private void SetState(Cohort result, double impact)
        {
            double prev_t = 0;
            double prev_n = 0;
            double prev_z = 0;

            for (int i = 0; i < this.Count; i++)
            {
                double f = this.F[i] * impact;
                double z = f + m;

                result.F[i] = f;

                double t = this[i].Age;
                double dt = t - prev_t;
                prev_t = t;

                double w = this.Masses[i];

                double n = (i == 0 ? r : (prev_n * Math.Exp(-prev_z * dt)));
                prev_n = n;
                prev_z = z;

                result.Survivors[i].Quantity = (int)n;
                result.Survivors[i].Mass = n * w;
            }

            for (int i = 0; i < result.Count; i++)
            {
                double f = this.F[i] * impact;
                double z = f + m;
                double t = this[i].Age;
                double w = this.Masses[i];

                double n = result.Survivors[i].Quantity;
                double next_n = (i == (result.Count - 1) ? 0 : result.Survivors[i + 1].Quantity);
                double c = (n - next_n) * (f / z);

                result[i].Quantity = (int)c;
                result[i].Mass = c * w;
            }
        }
        
        /// <summary>
        /// Suggest the cohort state with specified changes in effort
        /// </summary>
        /// <param name="impact">Changes in effort (as fraction of current)</param>
        /// <returns>Suggested state of virtual cohort</returns>
        public Cohort GetAlternateState(double impact)
        {
            Cohort result = new Cohort(this.Birth, this);
            SetState(result, impact);
            return result;
        }

        public double Z(int i)
        {
            return F[i] + NaturalMortality;
        }
        
        /// <summary>
        /// Calculates the MSY position for specified cohort
        /// </summary>
        /// <param name="cohort">Cohort with specified catches and F values</param>
        /// <returns>X - maximal allowed change to effort, Y - MSY</returns>
        public XY GetMaximumSustainableYeild()
        {
            try
            {
                Extremum e = FunctionMath.FindMaximum(
                    (x) => { return GetAlternateState(x).TotalMass; },
                    Interval.FromEndpoints(0, 10));
                return new XY(e.Location, e.Value);
                //return new XY(double.NaN, double.NaN);
            }
            catch
            {
                return new XY(double.NaN, double.NaN);
            }
        }
    }
}
