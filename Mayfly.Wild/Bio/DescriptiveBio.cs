using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mayfly.Wild
{
    public class DescriptiveBio : IBio
    {
        public IBioable Parent;



        public DataColumn XSource;

        public DataColumn YSource;

        public string XName { get; set; }

        public string YName { get; set; }


        
        public List<Composition> InternalDescriptives { get; private set; }

        public List<Composition> ExternalDescriptives { get; private set; }

        public List<Composition> CombinedDescriptives { get; private set; }



        public DescriptiveBio(IBioable data, DataRow[] speciesRows, 
            DataColumn xColumn, DataColumn yColumn)
        {
            if (yColumn.DataType != typeof(double))
                throw new FormatException();

            Parent = data;

            XSource = xColumn;
            XName = xColumn.Caption;

            YSource = yColumn;
            YName = yColumn.Caption;

            InternalDescriptives = new List<Composition>();
            ExternalDescriptives = new List<Composition>();
            CombinedDescriptives = new List<Composition>();

            foreach (DataRow speciesRow in speciesRows)
            {
                Refresh(speciesRow["Species"].ToString());
            }
        }



        public bool IsAvailable(string name)
        {
            Composition composition = GetCombinedComposition(name);
            return composition != null;
        }

        public string[] GetSpecies()
        {
            List<string> result = new List<string>();

            foreach (Composition composition in InternalDescriptives)
            {
                result.Add(composition.Name);
            }

            foreach (Composition composition in ExternalDescriptives)
            {
                if (!result.Contains(composition.Name))
                    result.Add(composition.Name);
            }

            return result.ToArray();
        }

        public Composition GetInternalComposition(string name)
        {
            foreach (Composition composition in InternalDescriptives)
            {
                if (composition.Name == name)
                {
                    return composition;
                }
            }

            return null;
        }

        public Composition GetExternalComposition(string name)
        {
            foreach (Composition composition in ExternalDescriptives)
            {
                if (composition.Name == name)
                {
                    return composition;
                }
            }

            return null;
        }

        public Composition GetCombinedComposition(string name)
        {
            foreach (Composition composition in CombinedDescriptives)
            {
                if (composition.Name == name)
                {
                    return composition;
                }
            }

            return null;
        }



        public double GetValue(string name, object x)
        {
            Composition composition = GetCombinedComposition(name);
            if (composition == null) return double.NaN;
            Category category = composition.GetCategory(x.ToString());
            if (category == null) return double.NaN;

            return category.MassSample.Mean;
        }

        public void Refresh()
        {
            foreach (Composition composition in this.InternalDescriptives)
            {
                Refresh(composition.Name);
            }
        }

        public bool Refresh(string name)
        {
            Composition composition = GetInternalComposition(name);

            if (composition == null) composition = new Composition(name);

            foreach (string group in XSource.GetStrings(true))
            {
                List<DataRow> rows = Parent.GetBioRows(name).GetRows(XSource, group);

                if (rows.Count >= Mathematics.UserSettings.StrongSampleSize)
                {
                    Category category = composition.GetCategory(group);
                    if (category == null) category = new Category(name);
                    category.MassSample = XSource.GetSample(rows);
                }
            }

            return true;
        }

        public void Involve(DescriptiveBio external, bool clear)
        {
            if (clear)
            {
                ExternalDescriptives.Clear();

                foreach (Composition composition in external.InternalDescriptives)
                {
                    ExternalDescriptives.Add(composition);
                }
            }
            else
            {
                foreach (Composition composition in external.InternalDescriptives)
                {
                    Composition ext1 = GetExternalComposition(composition.Name);

                    if (ext1 == null)
                    {
                        ExternalDescriptives.Add(composition);
                    }
                    else
                    {
                        ExternalDescriptives.Remove(ext1);
                        ExternalDescriptives.Add(Combine(ext1, composition));
                    }
                }
            }

            Recombine();
        }

        public void Recombine()
        {
            CombinedDescriptives.Clear();

            foreach (string name in GetSpecies())
            {
                CombinedDescriptives.Add(Combine(
                    GetInternalComposition(name),
                    GetExternalComposition(name)));
            }
        }

        public Composition Combine(Composition original, Composition external)
        {
            if (original == null) return external;

            if (external == null) return original;

            Composition result = new Composition(original.Name);

            foreach (Category category in original)
            {
                result.AddCategory(category);
            }

            foreach (Category category in external)
            {
                if (result.ContainsNamed(category.Name))
                {
                    // cope samples
                }
                else
                {
                    result.AddCategory(category);
                }
            }

            return result;
        }
    }
}
