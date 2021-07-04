using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Mayfly.Extensions;
using Meta.Numerics.Functions;
using Meta.Numerics;
using Meta.Numerics.Analysis;
using Meta.Numerics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public class VirtualPopulation : List<Cohort>
    {
        Interval solutionInterval;

        public double NaturalMortality { get; set; }

        public double TerminalFisheryMortality { get; set; }



        public VirtualPopulation(List<Cohort> cohs)
        {
            this.AddRange(cohs);
        }

        public VirtualPopulation(Composition[] compositions)
            : this(compositions.GetCohorts())
        {

        }



        public void SetParameters(double _start, double _end)
        {
            solutionInterval = Interval.FromEndpoints(_start, _end);
        }

        public Cohort GetAutoModel(int lastYear, Age oldest)
        {
            return this.GetCohort(lastYear - oldest.Years);
        }



        public void Run(Cohort model)
        {
            Analize(model);

            foreach (Cohort catches in this)
            {
                if (catches.Birth < model.Birth)
                {
                    Analize(catches);
                }
                else if (catches.Birth > model.Birth)
                {
                    double f = model.F[catches.GetIndexOfLast()];
                    Analize(catches, f);
                }
            }
        }

        public void Analize(Cohort coh)
        {
            Analize(coh, TerminalFisheryMortality);
        }

        public void Analize(Cohort coh, double terminal_f)
        {
            double f = terminal_f;
            double m = this.NaturalMortality;
            double z = m + f;
            double n = 0;
            bool first = true;

            for (int t = coh.Count - 1; t >= 0; t--)
            {
                if (coh[t].Quantity == 0)
                {
                    continue;
                }

                double c = coh[t].Quantity;

                if (first)
                {
                    first = false;
                    if (f == 0)
                    {
                        f = double.NaN;
                        n = double.NaN;
                    }
                    else
                    {
                        n = c / ((f / z) * (1 - Math.Exp(-z)));
                    }
                }
                else
                {
                    try
                    {
                        AgeGroup older = (AgeGroup)coh.GetNonEmptyNext(coh[t]);
                        double dt = older.Age - coh[t].Age;

                        f = FunctionMath.FindZero(
                            (double x) => (n * Math.Exp((x + m) * dt)) * ((x / (x + m)) * (1 - Math.Exp(-(x + m) * dt))) - c,
                             solutionInterval);

                        //f = Mathematics.Service.FindRoot(
                        //    //(double x) => (x / (x + m)) * (Math.Exp((x + m) - dt) - 1) - ((double)c_y / n),
                        //    (double x) => (n * Math.Exp((x + m) * dt)) * ((x / (x + m)) * (1 - Math.Exp(-(x + m) * dt))) - c,
                        //    start, end, h);

                        z = m + f;
                        n = n / Math.Exp(-z * dt);
                    }
                    catch
                    {
                        f = double.NaN;
                        n = double.NaN;
                    }
                }

                //coh.Survivors[t] = (int)Math.Round(n);
                coh.Survivors[t].Quantity = (int)n;
                coh.Survivors[t].Mass = ((int)n) * (coh[t].Mass / coh[t].Quantity);
                coh.F[t] = f;
            }
        }

        public AgeComposition GetComposition(int year)
        {
            AgeComposition result = new AgeComposition(year.ToString(), this[0].Youngest, this[0].Oldest);

            for (int j = 0; j < this[0].Count; j++)
            {
                Cohort c = this.GetCohort(year - result[j].Age.Years);

                if (c == null)
                {
                    continue;
                }
                else
                {
                    //if (double.IsNaN(c.Survivors[j])) continue;

                    result[j] = c.Survivors[j];
                    //AgeGroup ag = new AgeGroup(c[j].Age);
                    //ag.Quantity = (int)c.Survivors[j];
                    //ag.Mass = ag.Quantity * c[j].MassSample.Mean;
                    //result[j] = ag; // c[j];
                }
            }

            return result;
        }

        public VirtualCohort GetPseudoCohort()
        {
            if (this.Count == 0) return null;

            VirtualCohort result = new VirtualCohort(this[0]);
            result.Birth = this.Last().Birth + 1;

            for (int i = 0 ; i < this[0].Count; i++)
            {
                Sample q = new Sample();
                Sample m = new Sample();
                Sample f = new Sample();
                
                foreach (Cohort coh in this)
                {
                    if (coh[i].Quantity > 0)
                        q.Add(coh[i].Quantity);
                    
                    if (coh[i].Mass > 0)
                        m.Add(coh[i].Mass); 
                    
                    if (coh.GetFisheryMortality(i) > 0)
                        f.Add(coh.GetFisheryMortality(i));

                    result[i].MassSample.Add(coh[i].MassSample);
                }
                
                if (q.Count > 0) result[i].Quantity = (int)q.Mean;
                if (m.Count > 0) result[i].Mass = m.Mean;
                if (f.Count > 0) result.F[i] = f.Mean;

                q = new Sample();
                m = new Sample();
                
                foreach (Cohort coh in this)
                {
                    if (coh.Survivors[i].Quantity > 0)
                        q.Add(coh.Survivors[i].Quantity);
                    
                    if (coh.Survivors[i].Mass > 0)
                        m.Add(coh.Survivors[i].Mass); 
                    
                    if (coh.GetFisheryMortality(i) > 0)
                        f.Add(coh.GetFisheryMortality(i));
                }
                
                if (q.Count > 0) result.Survivors[i].Quantity = (int)q.Mean;
                if (m.Count > 0) result.Survivors[i].Mass = m.Mean;
            }

            return result;
        }



        public double GetMaximumSurvivals()
        {
            double max = 0;

            foreach (Cohort coh in this)
            {
                for (int i = 0; i < coh.Count; i++)
                {
                    double n = coh.Survivors[i].Quantity;
                    //if (double.IsNaN(n)) continue;
                    max = Math.Max(max, n);
                }
            }

            return max;
        }
    }
}
