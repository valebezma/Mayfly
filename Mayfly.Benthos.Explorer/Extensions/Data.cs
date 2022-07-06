using Mayfly.Benthos;
using Mayfly.Benthos.Explorer;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Mayfly.Species;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class DataExtensions
    {
        public static string[] GetVariantsOf(this Data data, string field)
        {
            return data.GetVariantsOf(field, string.Empty);
        }

        public static string[] GetVariantsOf(this Data data, string field, string format)
        {
            List<string> result = new List<string>();

            foreach (Data.CardRow cardRow in data.Card)
            {
                object value = cardRow.Get(field);

                if (value == null) continue;

                string formatted = value.Format(format);

                if (!result.Contains(formatted))
                {
                    result.Add(formatted);
                }
            }

            result.Sort(new OmniSorter());

            return result.ToArray();
        }



        public static int GetCount(this IList<Data.IndividualRow> individualRows)
        {
            int result = 0;

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }

            return result;
        }

        public static double[] GetMass(this IList<Data.IndividualRow> individualRows)
        {
            Data data = (Data)individualRows[0].Table.DataSet;
            return data.Individual.MassColumn.GetDoubles(individualRows).ToArray();

            //List<double> result = new List<double>();

            //foreach (Data.IndividualRow individualRow in individualRows)
            //{
            //    if (individualRow.IsMassNull()) continue;
            //    result.Add(individualRow.Mass);
            //}

            //return result.ToArray();
        }

        public static double GetAverageMass(this IList<Data.IndividualRow> individualRows)
        {
            Sample mass = new Sample(GetMass(individualRows));
            return mass.Count > 0 ? mass.Mean : double.NaN;
            //double result = 0;
            //int count = 0;

            //foreach (Data.IndividualRow individualRow in individualRows)
            //{
            //    if (individualRow.IsMassNull()) continue;
            //    result += individualRow.Mass;
            //    count++;
            //}

            //if (count > 0)
            //{
            //    return result / (double)count;
            //}
            //else
            //{
            //    return double.NaN;
            //}
        }

        public static List<Data.IndividualRow> GetMeasuredRows(this IList<Data.IndividualRow> rows, string variable)
        {
            return rows.GetMeasuredRows(((Data)rows[0].Table.DataSet).Variable.FindByVarName(variable));
        }

        public static List<Data.IndividualRow> GetMeasuredRows(this IList<Data.IndividualRow> rows, Data.VariableRow variableRow)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in rows)
            {
                foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
                {
                    if (valueRow.VariableRow.Variable == variableRow.Variable && 
                        !valueRow.IsValueNull() && !result.Contains(individualRow))
                    {
                        result.Add(individualRow);
                        break;
                    }
                }
            }

            return result;
        }

        public static List<Data.IndividualRow> GetMeasuredRows(this IList<Data.IndividualRow> rows, DataColumn dataColumn)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in rows)
            {
                if (individualRow.IsNull(dataColumn)) continue;
                result.Add(individualRow);
            }

            return result;
        }
        
        public static void PopulateSpeciesMenus(this ToolStripMenuItem item, CardStack stack, EventHandler command)
        {
            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i].Tag != null)
                {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator))
            {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                ToolStripItem _item = new ToolStripMenuItem();
                _item.Tag = speciesRow;
                _item.Text = speciesRow.FullName;
                _item.Click += command;
                item.DropDownItems.Add(_item);
            }

        }
    }
}

