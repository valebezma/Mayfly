using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public class ContinuousBio : IFormattable, IBio
    {
        public Data Parent { get; set; }

        public List<string> Authors { get; set; }

        public List<DateTime> Dates { get; set; }

        public List<string> Places { get; set; }

        public string Description
        {
            get
            {
                return string.Format(Resources.Interface.Interface.BioFormat, Dates.GetDatesDescription(DateTime.Now), Places.Merge());
            }
        }



        public bool VisualConfirmation { get; set; }



        //public DataColumn XSource { get; set; }

        //public DataColumn YSource { get; set; }

        string nameX;

        string nameY;

        string displayNameX;

        string displayNameY;

        public string DisplayNameX
        {
            get { return displayNameX; }
            set
            {
                displayNameX = value;

                foreach (Scatterplot scatter in InternalScatterplots)
                {
                    scatter.Data.X.Name = value;
                }

            }
        }

        public string DisplayNameY
        {
            get { return displayNameY; }
            set
            {
                displayNameY = value;

                foreach (Scatterplot scatter in InternalScatterplots)
                {
                    scatter.Data.Y.Name = value;
                }

            }
        }


        public List<Scatterplot> InternalScatterplots { get; private set; }

        public List<Scatterplot> ExternalScatterplots { get; private set; }

        public List<Scatterplot> CombinedScatterplots { get; private set; }

        public TrendType Nature { get; set; }

        //public Func<double, double> SizeClassConverter;



        public ContinuousBio(Data data, DataRow[] speciesRows, DataColumn xColumn, DataColumn yColumn, TrendType type)
        {
            if (xColumn.DataType != typeof(double))
                throw new FormatException();

            if (yColumn.DataType != typeof(double))
                throw new FormatException();

            Parent = data;
            VisualConfirmation = true;
            Nature = type;

            //XSource = xColumn;
            nameX = xColumn.Caption;

            //YSource = yColumn;
            nameY = yColumn.Caption;

            InternalScatterplots = new List<Scatterplot>();
            ExternalScatterplots = new List<Scatterplot>();
            CombinedScatterplots = new List<Scatterplot>();

            RefreshMeta();

            foreach (DataRow speciesRow in speciesRows)
            {
                Refresh(speciesRow["Species"].ToString());
            }
        }

        private void RefreshMeta()
        {
            Authors = new List<string>();
            Authors.AddRange(Parent.GetAuthors());

            Dates = new List<DateTime>();
            Dates.AddRange(Parent.GetDates());

            Places = new List<string>();
            Places.AddRange(Parent.GetPlaces());
        }



        public bool IsAvailable(string name)
        {
            Scatterplot scatter = GetCombinedScatterplot(name);
            return scatter == null ? false : scatter.Data.Count >= Mayfly.Mathematics.UserSettings.StrongSampleSize;
        }

        public string[] GetSpecies()
        {
            List<string> result = new List<string>();

            foreach (Scatterplot scatter in InternalScatterplots)
            {
                result.Add(scatter.Name);
            }

            foreach (Scatterplot scatter in ExternalScatterplots)
            {
                if (!result.Contains(scatter.Name))
                    result.Add(scatter.Name);
            }

            return result.ToArray();
        }

        public Scatterplot GetInternalScatterplot(string name)
        {
            return GetScatterplot(name, InternalScatterplots);
        }

        public Scatterplot GetExternalScatterplot(string name)
        {
            return GetScatterplot(name, ExternalScatterplots);
        }

        public Scatterplot GetCombinedScatterplot(string name)
        {
            return GetScatterplot(name, CombinedScatterplots);
        }

        public Scatterplot GetInternalScatterplot(string[] names)
        {
            return GetScatterplot(names, InternalScatterplots);
        }

        public Scatterplot GetExternalScatterplot(string[] names)
        {
            return GetScatterplot(names, ExternalScatterplots);
        }

        public Scatterplot GetCombinedScatterplot(string[] names)
        {
            return GetScatterplot(names, CombinedScatterplots);
        }

        public Scatterplot GetScatterplot(string name, List<Scatterplot> scatterplots)
        {
            foreach (Scatterplot scatterplot in scatterplots)
            {
                if (scatterplot.Name == name)
                {
                    return scatterplot;
                }
            }

            return null;
        }

        public Scatterplot GetScatterplot(string[] names, List<Scatterplot> scatterplots)
        {
            BivariateSample biSample = new BivariateSample();

            foreach (Scatterplot scatterplot in scatterplots)
            {
                if (names.Contains(scatterplot.Name))
                {
                    for (int i = 0; i < scatterplot.Data.Count; i++)
                    {
                        biSample.Add(
                            scatterplot.Data.X.ElementAt(i),
                            scatterplot.Data.Y.ElementAt(i));
                    }
                }
            }

            return new Scatterplot(biSample, names.Merge());
        }

        public Regression GetInternalRegression(string name)
        {
            Scatterplot scatter = GetInternalScatterplot(name);

            if (scatter == null) return null;

            return scatter.Regression;
        }

        public BivariateSample GetInternalBivariate(string name)
        {
            Regression regression = GetInternalRegression(name);

            if (regression == null) return null;

            return regression.Data;
        }

        public void ReplaceInternalScatterplot(string name, Scatterplot model)
        {
            Scatterplot scatter = GetInternalScatterplot(name);

            if (scatter != null) InternalScatterplots.Remove(scatter);

            InternalScatterplots.Add(model);

            Recombine(name);
        }



        public double GetValue(string name, double x, int decimals)
        {
            return Math.Round(GetValue(name, x), decimals);
        }

        private delegate DialogResult getConfirmation(Form form);

        private static DialogResult GetConfirmation(Form form)
        {
            if (Application.OpenForms[0].InvokeRequired) // Detect if we're on background
            {
                return (DialogResult)Application.OpenForms[0].Invoke(
                    new getConfirmation(GetConfirmation), new object[] { form });
            }
            else
            {
                return form.ShowDialog();
            }
        }

        bool Denied = false;

        public double GetValue(string name, double x, bool inversed)
        {
            if (!Licensing.Verify("Bios")) return double.NaN;

            if (this.Denied) return double.NaN;

            Scatterplot scatter = GetCombinedScatterplot(name);

            if (scatter == null || !scatter.IsRegressionOK) // There is no confirmed scatterplot for given species
            {
                // Create new BivariateSample
                BivariateSample bivariateSample = BuildInternalBivariate(name);

                // If BivariateSample is too small - return NaN
                if (bivariateSample == null) return double.NaN;

                scatter = new Scatterplot(bivariateSample, name);

                if (VisualConfirmation)
                {
                    scatter.CalculateApproximation(Nature);
                    ModelConfirm confirm = new ModelConfirm(scatter);//, XName, YName, name);

                    if (GetConfirmation(confirm) == DialogResult.OK)
                    {
                        ReplaceInternalScatterplot(name, confirm.Model);
                        return inversed ? confirm.Model.Regression.PredictInversed(x) : confirm.Model.Regression.Predict(x);
                    }
                    else
                    {
                        Denied = true;
                        return double.NaN;
                    }
                }
                else
                {
                    if (scatter.Regression == null) return double.NaN;
                    ReplaceInternalScatterplot(name, scatter);
                    return inversed ? scatter.Regression.PredictInversed(x) : scatter.Regression.Predict(x);
                }
            }
            else // There is confirmed scatterplot for given species
            {
                return inversed ? scatter.Regression.PredictInversed(x) : scatter.Regression.Predict(x);
            }
        }

        public double GetValue(string name, double x)
        {
            return GetValue(name, x, false);
        }

        public double GetValue(string name, object x)
        {
            return GetValue(name, Convert.ToDouble(x));
        }



        public void Refresh()
        {
            int i = 0;

            while (i < this.InternalScatterplots.Count)
            {
                if (!Refresh(InternalScatterplots[i].Name))
                    i--;
                i++;
            }
            //foreach (Scatterplot scatter in this.InternalScatterplots)
            //{
            //    Refresh(scatter.Name);
            //}
        }

        public bool Refresh(string name)
        {
            // Handling regressions

            BivariateSample biSample = BuildInternalBivariate(name);

            if (biSample == null)
            {
                // If sample is small - remove it from regressions

                Scatterplot scatter = GetInternalScatterplot(name);
                if (scatter != null)
                {
                    InternalScatterplots.Remove(scatter);
                    return false;
                }
            }
            else
            {
                // Else - replace it

                Scatterplot scatter = new Scatterplot(biSample, name);
                scatter.CalculateApproximation(Nature);

                //if (scatter.IsRegressionOK)
                //{
                ReplaceInternalScatterplot(name, scatter);
                //}
            }

            return true;
        }

        public BivariateSample BuildInternalBivariate(string name)
        {
            BivariateSample biSample = new BivariateSample();

            foreach (DataRow individualRow in Parent.GetBioRows(name))
            {
                double x = Parent.GetIndividualValue(individualRow, nameX);
                double y = Parent.GetIndividualValue(individualRow, nameY);

                if (double.IsNaN(x)) continue;
                if (double.IsNaN(y)) continue;

                biSample.Add(x, y);
            }

            biSample.X.Name = DisplayNameX;
            biSample.Y.Name = DisplayNameY;

            if (biSample.Count < Mathematics.UserSettings.StrongSampleSize)
            {
                return null;
            }

            return biSample;
        }



        public void Involve(ContinuousBio external, bool clear)
        {
            if (clear)
            {
                ExternalScatterplots.Clear();

                RefreshMeta();

                foreach (Scatterplot scatter in external.InternalScatterplots)
                {
                    scatter.CalculateApproximation(Nature);
                    ExternalScatterplots.Add(scatter);
                }
            }
            else
            {
                foreach (Scatterplot scatter in external.InternalScatterplots)
                {
                    Scatterplot ext1 = GetExternalScatterplot(scatter.Name);

                    if (ext1 == null)
                    {
                        scatter.CalculateApproximation(Nature);
                        ExternalScatterplots.Add(scatter);
                    }
                    else
                    {
                        ExternalScatterplots.Remove(ext1);
                        ExternalScatterplots.Add(Combine(ext1, scatter));
                    }
                }
            }

            foreach (string author in external.Authors)
            {
                if (!Authors.Contains(author))
                {
                    Authors.Add(author);
                }
            }

            foreach (DateTime date in external.Dates)
            {
                if (!Dates.Contains(date))
                {
                    Dates.Add(date);
                }
            }

            foreach (string place in external.Places)
            {
                if (!Places.Contains(place))
                {
                    Places.Add(place);
                }
            }

            // Combining scatterplots

            Recombine();
        }

        public void Recombine()
        {
            //CombinedScatterplots.Clear();

            foreach (string name in GetSpecies())
            {
                Recombine(name);
            }
        }

        public void Recombine(string name)
        {
            // 1 - Remove existing combined model
            CombinedScatterplots.Remove(GetCombinedScatterplot(name));

            // 2 - Refresh combined
            Scatterplot combined = Combine(GetInternalScatterplot(name), GetExternalScatterplot(name));
            if (combined == null) return;
            CombinedScatterplots.Add(combined);
        }

        public Scatterplot Combine(Scatterplot original, Scatterplot external1)
        {
            if (original == null) return external1;
            //if (original.Regression == null) return external1;

            if (external1 == null) return original;
            //if (external1.Regression == null) return original;

            // If internal is confirmed non Nature???

            BivariateSample biSample = MetaExtensions.GetCombinedBivariate(
                new BivariateSample[] { original.Data, external1.Data });
            biSample.X.Name = Resources.Reports.Caption.Length;
            biSample.Y.Name = nameY;
            Scatterplot result = new Scatterplot(biSample, original.Name);

            result.CalculateApproximation(original.IsRegressionOK ? original.Regression.Type : Nature);

            return result;
        }



        public override string ToString()
        {
            return ToString(string.Empty);
        }

        public string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0}: {1} internal + {2} external models",
                nameY,
                InternalScatterplots.Count,
                ExternalScatterplots.Count);
        }
    }
}