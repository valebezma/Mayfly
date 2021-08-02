using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using System.Windows.Forms;
using Meta.Numerics;
using Meta.Numerics.Statistics;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public class AgeKey : AgeComposition
    {
        internal double LengthInterval;

        internal AgeComposition measured;

        internal LengthComposition untreatedTtl;
        internal LengthComposition treatedTtl;

        internal Category[,] treated;
        internal Category[,] obtained;

        internal double Minimum;
        internal double Maximum;



        public AgeKey(string name, Age start, Age end, double min, double max, double lengthInterval)
            : base(name, start, end)
        {
            measured = new AgeComposition("Measured", start, end);

            LengthInterval = lengthInterval;
            Minimum = min;
            Maximum = max;

            // Create and fill length compositions
            untreatedTtl = new LengthComposition("Untreated", min, max, lengthInterval);
            treatedTtl = new LengthComposition("Treated", min, max, lengthInterval);

            // Create matrices
            treated = new Category[Count, treatedTtl.Count];
            obtained = new Category[Count, treatedTtl.Count];

            for (int ac = 0; ac < Count; ac++)
            {
                for (int lc = 0; lc < treatedTtl.Count; lc++)
                {
                    treated[ac, lc] = new Category(string.Format("Treated {0} {1}", this[ac].Name, treatedTtl[lc].Name));
                    obtained[ac, lc] = new Category(string.Format("Obtained {0} {1}", this[ac].Name, treatedTtl[lc].Name));
                }
            }
        }



        public void Fill(CardStack stack, Data.SpeciesRow speciesRow)
        {
            #region Prepare data

            // Set Untreated totals column
            foreach (SizeClass group in untreatedTtl)
            {
                group.Quantity = (stack.Quantity(speciesRow, group.Size) - stack.Aged(speciesRow, group.Size));
            }

            // Set treated totals column
            foreach (SizeClass group in treatedTtl)
            {
                group.Quantity = stack.Aged(speciesRow, group.Size);
            }

            int ac = 0;

            // Set treated matrix
            foreach (AgeGroup age in this)
            {
                int lc = 0;

                //for (Interval size = Service.GetStrate(Minimum); 
                //    size.LeftEndpoint <= Maximum; size.Shift())
                for (double l = Service.GetStrate(Minimum).LeftEndpoint;
                    l <= Maximum; l += LengthInterval)
                {
                    Interval size = Interval.FromEndpointAndWidth(l, LengthInterval);
                    
                    treated[ac, lc].Quantity = stack.Quantity(speciesRow, size, age.Age);

                    if (treated[ac, lc].Quantity > 0)
                    {
                        treated[ac, lc].MassSample = stack.MassSample(speciesRow, size, age.Age);
                        double w = treated[ac, lc].MassSample.Count > 0 ? treated[ac, lc].MassSample.Mean :
                            stack.Parent.MassModels.GetValue(speciesRow.Species, treatedTtl[lc].Size.Midpoint);
                        treated[ac, lc].Mass = treated[ac, lc].Quantity * w / 1000.0;
                    }
                    else
                    {
                        treated[ac, lc].Mass = 0.0;
                    }

                    lc++;
                }

                ac++;
            }

            #endregion

            // Calculate untreated numbers matrix
            ac = 0;

            foreach (AgeGroup age in this)
            {
                int lc = 0;

                for (double l = Service.GetStrate(Minimum).LeftEndpoint;
                    l <= Maximum; l += LengthInterval)
                {
                    Interval size = Interval.FromEndpointAndWidth(l, LengthInterval);

                    int t = treated[ac, lc].Quantity;
                    int U = untreatedTtl[lc].Quantity;
                    int T = treatedTtl[lc].Quantity;

                    if (U > 0) // If there are untreated individuals if size class
                    {
                        // Issue of method priority:
                        // if regression should be applied first (in that case total of recovered will fit with total of sample)
                        // or simple proportion (in that case total won't fit exactly)

                        double p = 0;

                        switch (UserSettings.SelectedAgeLengthKeyType)
                        {
                            case AgeLengthKeyType.Raw:
                                // If there are treated individuals - get obtained by raw proportion
                                if (T > 0) p = (double)t / (double)T;
                                break;

                            case AgeLengthKeyType.Smooth:
                                // If there is model - get all untreated as obtained if obtained age conforms
                                if (stack.Parent.GrowthModels.IsAvailable(speciesRow.Species))
                                {
                                    double rec = stack.Parent.GrowthModels.GetValue(speciesRow.Species, size.Midpoint, true);

                                    if (Math.Floor(rec) == age.Age.Years)
                                    {
                                        p = 1;
                                    }
                                }
                                else
                                {
                                    Log.Write(new ArgumentException("Using smooth age-length key without growth model on " + speciesRow.Species));
                                }
                                break;
                        }

                        if (p > 0)
                        {
                            obtained[ac, lc].Quantity = (int)((double)U * p);
                            double mw = stack.Parent.MassModels.GetValue(speciesRow.Species, size.Midpoint);
                            obtained[ac, lc].Mass = obtained[ac, lc].Quantity * mw / 1000.0;
                        }
                    }

                    lc++;
                }

                ac++;
            }

            Summarize();
        }

        private void Summarize()
        {
            for (int ac = 0; ac < Count; ac++)
            {
                double untreatedQ = 0;
                double untreatedW = 0;

                measured[ac].Quantity = 0;
                measured[ac].Mass = 0;

                for (int lc = 0; lc < treatedTtl.Count; lc++)
                {
                    untreatedQ += obtained[ac, lc].Quantity;
                    untreatedW += obtained[ac, lc].Mass;

                    measured[ac].Quantity += treated[ac, lc].Quantity;
                    measured[ac].Mass += treated[ac, lc].Mass;
                }

                this[ac].Quantity = measured[ac].Quantity + (int)Math.Round(untreatedQ);
                this[ac].Mass = measured[ac].Mass + untreatedW;

                this[ac].Index = Weight;
            }

            IsRecovered = TotalQuantity > measured.TotalQuantity;
        }

        public void Split(int ac, double measure)
        {
            treated = Split(treated, ac, measure);
            obtained = Split(obtained, ac, measure);

            string less = string.Format("{0} (<{1})", this[ac].Age.Group, measure);
            string more = string.Format("{0} (>{1})", this[ac].Age.Group, measure);

            this[ac].Name = less;
            AddCategory(new AgeGroup(more) { Age = this[ac].Age, MassSample = this[ac].MassSample }, ac + 1);

            Sample sampleLess = new Sample();
            Sample sampleMore = new Sample();

            foreach (double d in this[ac].LengthSample)
            {
                if (d < measure) { sampleLess.Add(d); }
                else { sampleMore.Add(d); }
            }

            this[ac].LengthSample = sampleLess;
            this[ac + 1].LengthSample = sampleMore;


            measured[ac].Name = less;
            measured.AddCategory(new AgeGroup(more) { Age = this[ac].Age }, ac + 1);

            Summarize();
        }

        private Category[,] Split(Category[,] array, int r, double measure)
        {
            // r - position of new layer

            Category[,] result = new Category[array.GetLength(0) + 1, array.GetLength(1)];

            for (int lc = 0; lc < array.GetLength(1); lc++)
            {
                if (this[r].LengthSample.Count == 0)
                {
                    // I/O distribution variant
                    result[r, lc] = (treatedTtl[lc].Size.Midpoint < measure) ? array[r, lc] : new Category(string.Format("{0} <{1}", array[r, lc].Name, measure));
                    result[r + 1, lc] = (treatedTtl[lc].Size.Midpoint >= measure) ? array[r, lc] : new Category(string.Format("{0} >{1}", array[r, lc].Name, measure));
                }
                else
                {
                    // Distribution based on length sample
                    double left = this[r].LengthSample.LeftProbability(measure);
                    double w = (this[r].Mass / (double)this[r].Quantity);
                    int leftQ = (int)(array[r, lc].Quantity * left);

                    result[r, lc] = new Category(string.Format("{0} <{1}", array[r, lc].Name, measure))
                    {
                        Quantity = leftQ,
                        Mass = leftQ * w
                    };

                    result[r + 1, lc] = new Category(string.Format("{0} >{1}", array[r, lc].Name, measure))
                    {
                        Quantity = array[r, lc].Quantity - leftQ,
                        Mass = array[r, lc].Mass - (leftQ * w)
                    };
                }

                // following new array
                for (int ac = 0; ac < result.GetLength(0); ac++)
                {
                    if (ac < r)
                    {
                        result[ac, lc] = array[ac, lc]; // copy to exact the same address
                    }

                    if (ac > (r + 1)) // copy to next address
                    {
                        result[ac, lc] = array[ac - 1, lc];
                    }
                }
            }

            return result;
        }



        public Report.Table GetReport()
        {
            Report.Table table1 = new Report.Table(Resources.Reports.AgeLengthKey.Title, Name);

            table1.StartRow();
            table1.AddHeaderCell(Fish.Resources.Common.SizeUnits, .15, 2);
            //table1.AddHeaderCell(Resources.Reports.AgeLengthKey.Sample, 2);
            table1.AddHeaderCell(Resources.Reports.AgeLengthKey.Treated, 2, CellSpan.Rows);
            table1.AddHeaderCell(Resources.Reports.AgeLengthKey.Untreated, 2, CellSpan.Rows);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Age, Count);
            table1.EndRow();

            table1.StartRow();
            for (int ac = 0; ac < Count; ac++) { table1.AddHeaderCell(this[ac].Name); }
            table1.EndRow();

            // Quantity

            table1.StartRow();
            table1.AddCellValue(Wild.Resources.Reports.Caption.Quantity, Count + 3, CellSpan.Columns);
            table1.EndRow();

            for (int lc = 0; lc < untreatedTtl.Count; lc++)
            {
                //if (treated[lc].Quantity + untreated[lc].Quantity == 0)
                //{
                //    continue;
                //}

                table1.StartRow();
                table1.AddCellValue(treatedTtl[lc].Name);
                //table1.AddCellValue(this[lc].Quantity);
                if (treatedTtl[lc].Quantity > 0) table1.AddCellRight(treatedTtl[lc].Quantity); else table1.AddCell();
                if (untreatedTtl[lc].Quantity > 0) table1.AddCellRight(untreatedTtl[lc].Quantity); else table1.AddCell();

                for (int ac = 0; ac < Count; ac++)
                {
                    int t = treated[ac, lc].Quantity;
                    int o = obtained[ac, lc].Quantity;

                    if ((t + o) > 0)
                    {
                        if (o > 0) table1.AddCellValue(string.Format("{0} (+{1})", t, o));
                        else table1.AddCellValue(t);
                    }
                    else
                    {
                        table1.AddCell();
                    }
                }

                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Resources.Reports.AgeLengthKey.Total, 3, CellSpan.Columns);
            for (int ac = 0; ac < measured.Count; ac++)
            {
                if (measured[ac].Quantity > 0) table1.AddCellRight(measured[ac].Quantity);
                else table1.AddCell();
            }
            table1.EndRow();

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
            //table1.AddCellRight(this.TotalQuantity);
            table1.AddCellRight(treatedTtl.TotalQuantity);
            table1.AddCellRight(untreatedTtl.TotalQuantity);
            for (int ac = 0; ac < Count; ac++)
            {
                if (this[ac].Quantity > 0) table1.AddCellRight(this[ac].Quantity);
                else table1.AddCell();
            }
            table1.EndRow();

            // Mass

            table1.StartRow();
            table1.AddCellValue(Resources.Reports.Common.MassUnits, Count + 3, CellSpan.Columns);
            table1.EndRow();

            for (int lc = 0; lc < untreatedTtl.Count; lc++)
            {
                //if (treated[lc].Quantity + untreated[lc].Quantity == 0)
                //{
                //    continue;
                //}

                table1.StartRow();
                table1.AddCellValue(treatedTtl[lc].Name);
                table1.AddCell();
                table1.AddCell();

                for (int ac = 0; ac < Count; ac++)
                {
                    double t = treated[ac, lc].Mass;
                    double o = obtained[ac, lc].Mass;

                    if ((t + o) > 0)
                    {
                        if (o > 0) table1.AddCellValue(string.Format("{0:N3} (+{1:N3})", t, o));
                        else table1.AddCellRight(t, "N3");
                    }
                    else
                    {
                        table1.AddCell();
                    }
                }

                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Resources.Reports.AgeLengthKey.Total, 3, CellSpan.Columns);
            for (int ac = 0; ac < measured.Count; ac++)
            {
                if (measured[ac].Mass > 0) table1.AddCellRight(measured[ac].Mass, "N3");
                else table1.AddCell();
            }
            table1.EndRow();

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
            table1.AddCellRight(treatedTtl.TotalQuantity);
            table1.AddCellRight(untreatedTtl.TotalQuantity);

            for (int ac = 0; ac < Count; ac++)
            {
                if (this[ac].Mass > 0) table1.AddCellRight(this[ac].Mass, "N3");
                else table1.AddCell();
            }
            table1.EndRow();

            return table1;
        }

        public DialogResult ShowDialog()
        {
            AgeKeyForm form = new AgeKeyForm(this);
            return form.ShowDialog();
        }
    }

    public enum AgeLengthKeyType
    {
        Raw,
        Smooth
    }
}
