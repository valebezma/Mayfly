using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Mayfly.Mathematics.Charts;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;
using System.Globalization;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
{
    public class CompositionEqualizer : Composition, IFormattable
    {
        public List<Composition> SeparateCompositions { get; set; }

        public int Dimension
        {
            get { return SeparateCompositions.Count; }
        }



        public CompositionEqualizer()
        {
            SeparateCompositions = new List<Composition>();
        }

        public CompositionEqualizer(string name)
            : this()
        {
            Name = name;
        }

        public CompositionEqualizer(Composition example)
            : this(example.Name)
        {
            foreach (Category category in example)
            {
                AddCategory(category.GetEmptyCopy());
            }
        }



        public void AddComposition(Composition composition)
        {
            this.SeparateCompositions.Add(composition);
        }

        /// <summary>
        /// Split array of age keys and resulting composition
        /// </summary>
        /// <param name="j">Index of age row</param>
        /// <param name="measure">Length used for separation</param>
        public void Split(int j, double measure)
        {
            string less = string.Format("{0} (<{1})", this[j].Name, measure);
            string more = string.Format("{0} (>{1})", this[j].Name, measure);

            foreach (Composition compos in this.SeparateCompositions)
            {
                if (compos is AgeKey)
                {
                    ((AgeKey)compos).Split(j, measure);
                }
                else
                {
                    compos.Split(j, less, more);
                }
            }

            this.Split(j, less, more);
            this.GetWeighted();
        }

        public void GetWeighted()
        {
            this.SamplesCount = 0;

            foreach (Composition composition in SeparateCompositions)
            {
                this.SamplesCount += composition.SamplesCount;
            }

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Quantity = 0;
                this[i].Mass = 0;
                this[i].Abundance = 0;
                this[i].Biomass = 0;

                this[i].SamplesCount = 0;

                this[i].MassSample = new Sample();
                this[i].LengthSample = new Sample();

                this[i].Sexes = new Composition();

                Category j = new Category();
                Category m = new Category();
                Category f = new Category();

                foreach (Composition composition in SeparateCompositions)
                {
                    this[i].Quantity += composition[i].Quantity;
                    this[i].Mass += composition[i].Mass;
                    this[i].Abundance += composition[i].Abundance;
                    this[i].Biomass += composition[i].Biomass;

                    this[i].SamplesCount += composition[i].SamplesCount;

                    this[i].MassSample.Add(composition[i].MassSample);
                    this[i].LengthSample.Add(composition[i].LengthSample);

                    if (composition[i].Sexes != null)
                    {
                        j.Quantity += composition[i].Sexes[0].Quantity;
                        j.Mass += composition[i].Sexes[0].Mass;
                        j.Abundance += composition[i].Sexes[0].Abundance;
                        j.Biomass += composition[i].Sexes[0].Biomass;

                        m.Quantity += composition[i].Sexes[1].Quantity;
                        m.Mass += composition[i].Sexes[1].Mass;
                        m.Abundance += composition[i].Sexes[1].Abundance;
                        m.Biomass += composition[i].Sexes[1].Biomass;

                        f.Quantity += composition[i].Sexes[2].Quantity;
                        f.Abundance += composition[i].Sexes[2].Abundance;
                        f.Mass += composition[i].Sexes[2].Mass;
                        f.Biomass += composition[i].Sexes[2].Biomass;
                    }
                }

                this[i].SetSexualComposition(j, m, f);
            }

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Abundance /= this.Dimension;
                this[i].Biomass /= this.Dimension;
                foreach (Category cat in this[i].Sexes)
                {
                    cat.Abundance /= this.Dimension;
                    cat.Biomass /= this.Dimension;
                }
            }
        }

        public Category this[int i, int j]
        {
            get
            {
                return this.SeparateCompositions[i][j];
            }

            //set
            //{
            //    this.SeparateCompositions[i][j] = value;
            //}
        }

        public Composition GetComposition(int i)
        {
            return SeparateCompositions[i];
        }

        public DialogResult ShowDialog()
        {
            List<Histogramma> result = new List<Histogramma>();

            foreach (Composition comp in SeparateCompositions)
            {
                if (comp is AgeComposition) {
                    result.Add(((AgeComposition)comp).GetHistogram());
                }

                if (comp is LengthComposition) {
                    result.Add(((LengthComposition)comp).GetHistogram());
                }
            }

            if (result.Count == 0) return DialogResult.Abort;

            ChartForm form = new ChartForm();
            foreach (Histogramma hist in result) {
                form.StatChart.AddSeries(hist);
            }
            form.StatChart.Remaster();
            return form.ShowDialog();
        }




        public Report.Table GetAppendix(CompositionColumn valueVariant, string appName, string separatesHeader)
        {
            return SeparateCompositions.GetTable(valueVariant, appName, this.Name, separatesHeader);
        }


        #region IFormattable

        public override string ToString()
        {
            return base.ToString(Resources.Interface.Interface.CompositionEqPresentation);
        }

        public override string ToString(string format, IFormatProvider provider)
        {
            return string.Format(Resources.Interface.Interface.ResourceManager.GetString("CompositionEqPresentation",
                (CultureInfo)provider), Name, Count);
        }

        public override string ToString(string format)
        {
            return string.Format(format, Name, Dimension, Count);
        }

        #endregion
    }
}
