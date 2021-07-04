using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using System.Globalization;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public class Cohort : AgeComposition
    {
        public int Birth { get; set; }

        //public double[] Survivors { get; internal set; }
        //public AgeGroup[] Survivors { get; internal set; }
        public AgeComposition Survivors { get; internal set; }

        public double[] F { get; internal set; }


        public Cohort(int birth, AgeComposition composition)
            : base(string.Format(Resources.Interface.Interface.CohortPresentation, birth),
            composition.Youngest, composition.Oldest)
        {
            Birth = birth;
            F = new double[Count];
            //Survivors = new double[Count];
            Survivors = new AgeComposition(
                string.Format("Survivors of {0} generation", birth),
                composition.Youngest, composition.Oldest);
        }



        public int GetCatchYear(int i)
        {
            return Birth + this[i].Age.Years;
        }

        public int GetYear(Age age)
        {
            return Birth + age.Years;
        }

        public double GetFisheryMortality(int i)
        {
            return this.F[i];
        }

        public Cohort GetNextGeneration()
        {
            Cohort result = new Cohort(Birth + 1, this);
            return result;
        }

        public AgeGroup GetInitialState()
        {
            if (this.Count < 1) return null;

            if (this.Survivors[0].Quantity > 0)
            {
                return this.Survivors[0];
            }
            else
            {
                try
                {
                    BivariateSample data = new BivariateSample();
                    for (int i = 0; i < this.Count; i++) {
                        if (this.Survivors[i].Quantity == 0) continue;
                        double x = (this[i]).Age;
                        data.Add(x, this.Survivors[i].Quantity);
                    }

                    Exponent exp = new Exponent(data);
                    double n = exp.Predict(0.5);                    
                    return new AgeGroup((Age)0) { Quantity = (int)n };
                }
                catch
                {
                    return null;
                }
            }
        }

        public Report.Table GetTable()
        {
            Report.Table table1 = new Report.Table(Resources.Reports.VPA.Table3, this.Name);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Growth.Column1, .2);
            table1.AddHeaderCell(Resources.Reports.VPA.Column2);
            table1.AddHeaderCell(Resources.Reports.VPA.Column3);
            table1.AddHeaderCell(Resources.Reports.VPA.Column4);
            table1.AddHeaderCell(Resources.Reports.VPA.Column5);
            table1.EndRow();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Quantity == 0) continue;

                table1.StartRow();
                table1.AddCellValue(this[i].Name);
                table1.AddCellValue(this.GetCatchYear(i));
                table1.AddCellRight(this[i].Quantity, "N0");
                table1.AddCellRight(this.F[i], "N4");
                table1.AddCellRight(this.Survivors[i].Quantity, "N0");
                table1.EndRow();
            }

            return table1;
        }
    }
}
