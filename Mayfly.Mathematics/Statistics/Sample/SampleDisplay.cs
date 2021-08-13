using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meta.Numerics.Statistics;
using System.Globalization;
using Mayfly.Extensions;
using System.Resources;
using System.Windows.Forms;

namespace Mayfly.Mathematics.Statistics
{
    public class SampleDisplay : IFormattable, IComparable
    {
        Sample Sample { get; set; }

        private SampleDisplay()
        {

        }

        public SampleDisplay(Sample sample) 
            : this()
        {
            Sample = sample;
        }



        #region IFormatable interface

        public override string ToString()
        {
            return this.ToString("X" + Sample.Precision());
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (Sample == null) return string.Empty;

            if (format == null)
            {
                format = string.Empty;
            }

            if (format == string.Empty)
            {
                format = "x";
            }

            format = format.ToLower().Trim();

            if (Sample.Count == 0)
            {
                return Resources.Interface.EmptySample;
            }
            else
            {
                string numformat = Sample.Precision().ToString();

                int j = -1;
                for (int i = 0; i < format.Length; i++)
                {
                    if ("1234567890".Contains(format[i])) j = i; 
                }

                if (j > -1) numformat = format.Substring(j);

                numformat = "n" + numformat;


                switch (format.Substring(0, j < 0 ? format.Length : j))
                {
                    case "min": // Minimum
                        return Sample.Minimum.ToString(provider);

                    case "max": // Maximum
                        return Sample.Maximum.ToString(provider);

                    case "sd": // Standard deviation
                        return Sample.Count > 2 ? Service.PresentError(Sample.StandardDeviation, numformat) : double.NaN.ToString();

                    case "sdc": // Standard deviation (corrected)
                        return Sample.Count > 2 ? Service.PresentError(Sample.CorrectedStandardDeviation, numformat) : double.NaN.ToString();

                    case "se": // Standard error
                        return Sample.Count > 2 ? Service.PresentError(Sample.PopulationMean.Uncertainty, numformat) : double.NaN.ToString();

                    case "se%": // Relative standard error
                        return Sample.Count > 2 ? Sample.PopulationMean.RelativeUncertainty.ToString("P1") : double.NaN.ToString();

                    //case "ME": // Margins of error
                    //    return Sample.Count > 2 ? Service.PresentError(
                    //        Sample.pop .MarginOfError(UserSettings.DefaultConfidenceLevel))
                    //            : double.NaN.ToString();

                    //case "ME%":
                    //    return Sample.Count > 2 ? (
                    //        Sample.MarginOfError(UserSettings.DefaultConfidenceLevel) /
                    //            Sample.Mean).ToString("P1") : double.NaN.ToString();

                    default: // Other variants are single letter with numerical value

                        // define format of numeric values
                        // f. e., F1 becomes N1
                        //string numberformat = "N" + (
                        //    format.Length == 1 ? Sample.Precision().ToString() : format.Substring(1)
                        //    );

                        switch (format[0])
                        {
                            case 'n': // Sample size
                                return Sample.Count.ToString(provider);

                            case 'a': // All values
                                return Sample.Merge(numformat, provider);

                            default:
                                switch (format[0])
                                {
                                    case 't': // Total
                                        return Sample.Sum().ToString(numformat, provider);

                                    case 'm': // Mean
                                        return Sample.Mean.ToString(numformat, provider);

                                    default:
                                        if (Sample.Count == 1)
                                        {
                                            return Sample.Mean.ToString(numformat, provider);
                                        }
                                        else
                                        {
                                            switch (format[0])
                                            {
                                                case 'e': // Extremal values
                                                    return Sample.Minimum.ToString(numformat, provider) +
                                                        " — " + Sample.Maximum.ToString(numformat, provider);

                                                case 'q': // Mean + extremal values
                                                    return Sample.Mean.ToString(numformat, provider) + Environment.NewLine +
                                                        this.ToString(numformat.Replace('N', 'E'));

                                                case 's': // Mean and standard error
                                                    return Sample.Mean.ToString(numformat, provider) + (
                                                        Sample.Count == 2 ? string.Empty : " ± " +
                                                        Service.PresentError(
                                                            Sample.PopulationMean.Uncertainty, provider)
                                                        );

                                                //case 'C': // Mean and confidence interval
                                                //    return Sample.Mean.ToString(numberformat, provider) + (
                                                //        Sample.Count == 2 ? string.Empty : " ± " +
                                                //        Service.PresentError(
                                                //            Sample.MarginOfError(UserSettings.DefaultConfidenceLevel), provider)
                                                //        );

                                                case 'g': // Mean and standard error + extremal values
                                                    return
                                                        Sample.Mean.ToString(numformat, provider) + (
                                                        Sample.Count == 2 ? string.Empty : " ± " +
                                                        Service.PresentError(Sample.PopulationMean.Uncertainty, provider)
                                                        ) +
                                                        Environment.NewLine +
                                                        Sample.Minimum.ToString(numformat, provider) +
                                                        " — " + Sample.Maximum.ToString(numformat, provider);

                                                default: // By default gets Mean, stadard error and Sample size
                                                    return Sample.Mean.ToString(numformat, provider) + (
                                                        Sample.Count == 2 ? string.Empty : " ± " + Service.PresentError(Sample.PopulationMean.Uncertainty, provider)
                                                        + " (" + Sample.Count + ")");

                                            }
                                        }
                                }
                        }
                }
            }
        }

        #endregion

        #region IComparable interface

        public int CompareTo(SampleDisplay other)
        {
            return Sample.Mean.CompareTo(other.Sample.Mean);
        }

        public int CompareTo(object obj)
        {
            if (obj is SampleDisplay) return this.CompareTo((SampleDisplay)obj);
            else if (obj is double) return Sample.Mean.CompareTo((double)obj);
            else throw new NotImplementedException();
        }

        #endregion


        public string Values()
        {
            string result = string.Empty;

            foreach (double value in Sample)
            {
                result += value + Constants.Return;
            }

            return result;
        }

        public string ToMathML()
        {
            if (Sample.Count == 1)
            {
                return Sample.ElementAt(0).ToString(Sample.MeanFormat());
            }
            else if (Sample.Count == 2)
            {
                return Sample.Mean.ToString(Sample.MeanFormat());
            }
            else
            {
                return @"<math display='block'>
<mrow><mfrac><mrow><mn>" + Sample.Mean.ToString(Sample.MeanFormat()) + @"</mn><mo>&#x00B1;</mo><mn>" +
                         Service.PresentError(Sample.PopulationMean.Uncertainty) + @"</mn></mrow>
<mrow><mn>" + Sample.Minimum.ToString(Sample.MeanFormat()) + @"</mn><mo>&#x2212;</mo><mn>" +
            Sample.Maximum.ToString(Sample.MeanFormat()) + @"</mn></mrow>
</mfrac></mrow></math>";
            }
        }

        public string ToLongString()
        {
            ResourceManager resources = new ResourceManager(typeof(Mathematics.Statistics.SampleProperties));

            System.IO.StringWriter result = new System.IO.StringWriter();

            result.WriteLine(Sample.Name);
            result.WriteLine();
            if (Sample.Count > 0)
            {
                result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelSample.Count.Text"), Sample.Count);
                result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelMinimum.Text"), Sample.Minimum);
                result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelMaximum.Text"), Sample.Maximum);
                result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelRange.Text"), Sample.Maximum - Sample.Minimum);
                result.WriteLine("{0, -22}{1, 9:" + Sample.MeanFormat() + "}", resources.GetString("labelMean.Text"), Sample.Mean);
                if (Sample.Count > 2)
                {
                    result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelVariance.Text"), Mathematics.Service.PresentError(Sample.Variance));
                    result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelStdDeviation.Text"), Mathematics.Service.PresentError(Sample.StandardDeviation));
                    result.WriteLine("{0, -22}{1, 9}", resources.GetString("labelStdError.Text"), Mathematics.Service.PresentError(Sample.PopulationMean.Uncertainty));
                }
            }
            else
            {
                result.WriteLine(Mayfly.Resources.Interface.EmptyValue);
            }

            return result.ToString();
        }

        public Report GetReport()
        {
            // TODO: implement Report

            Report report = new Report("Sample");

            return report;
        }




        public Button GetButton()
        {
            Button button = new Button();
            button.Text = string.Format("{0} ({1})", Sample.Name, Sample.Count);
            button.AutoSize = true;
            button.Click += (o, e) => {
                SampleProperties result =
                    new SampleProperties(Sample);
                result.SetFriendlyDesktopLocation(button);
                result.Show();
            };
            button.FlatStyle = FlatStyle.System;
            return button;
        }

        private void SampleButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Sample sample = (Sample)button.Tag;
        }

        public ContextMenuStrip GetMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem itemProperties = new ToolStripMenuItem();
            itemProperties.Text = "Properties";
            itemProperties.Click += (o, e) => {
                SampleProperties properties =
                    new SampleProperties(Sample);
                properties.SetFriendlyDesktopLocation(Form.MousePosition);
                properties.Show();
            };

            ToolStripMenuItem itemCopyPresentation = new ToolStripMenuItem();
            itemProperties.Text = "Copy presentation";
            itemProperties.Click += (o, e) => {
                Clipboard.SetText(Sample.ToString());
            };

            ToolStripMenuItem itemCopyValues = new ToolStripMenuItem();
            itemProperties.Text = "Copy values";
            itemProperties.Click += (o, e) => {
                Clipboard.SetText(this.Values());
            };

            ToolStripMenuItem itemReport = new ToolStripMenuItem();
            itemProperties.Text = "Print...";
            itemProperties.Click += (o, e) => {
                this.GetReport().Run();
            };

            menu.Items.AddRange(new ToolStripMenuItem[] {
                itemProperties,
                itemCopyPresentation,
                itemCopyValues,
                itemReport });

            return menu;
        }
    }
}
