using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using System.Collections;
using System.Globalization;
using Meta.Numerics.Statistics;
using System.Windows.Forms;
using Mayfly.Controls;

namespace Mayfly.Wild
{
    public class Composition : List<Category>, IFormattable, IComparable<Composition>
    {
        public string Name { get; set; }

        public int SamplesCount { get; set; }

        public double Weight 
        {
            get { return _weight; }

            set
            {
                _weight = value;

                foreach (Category category in this)
                {
                    category.Index = value;
                }
            }
        }

        double _weight;



        public int NonEmptyCount
        {
            get
            {
                int result = 0;

                foreach (Category category in this)
                {
                    if (category.Quantity > 0) result++;
                }

                return result;
            }
        }


        public double TotalQuantity
        {
            get
            {
                double result = 0;

                foreach (Category category in this)
                {
                    result += category.Quantity;
                }

                return result;
            }
        }

        public double TotalMass
        {
            get
            {
                double result = 0;

                foreach (Category category in this)
                {
                    result += category.Mass;
                }

                return result;
            }
        }


        public double TotalAbundance
        {
            get
            {
                double result = 0;

                foreach (Category category in this)
                {
                    result += category.Abundance;
                }

                return result;
            }
        }

        public double TotalBiomass
        {
            get
            {
                double result = 0;

                foreach (Category category in this)
                {
                    result += category.Biomass;
                }

                return result;
            }
        }


        public Category MostDominant
        {
            get
            {
                if (this.Count == 0) return null;

                Category result = this[0];

                for (int i = 1; i < Count; i++)
                {
                    if (this[i].Dominance > result.Dominance)
                    {
                        result = this[i];
                    }
                }

                return result;
            }
        }

        public Category MostAbundant
        {
            get
            {
                if (this.Count == 0) return null;

                Category result = this[0];

                for (int i = 1; i < Count; i++)
                {
                    if (this[i].Abundance > result.Abundance)
                    {
                        result = this[i];
                    }
                }

                return result;
            }
        }

        public Category MostSampled
        {
            get
            {
                if (this.Count == 0) return null;

                Category result = this[0];

                for (int i = 1; i < Count; i++)
                {
                    if (this[i].Quantity > result.Quantity)
                    {
                        result = this[i];
                    }
                }

                return result;
            }
        }

        public Category MostAbundantByMass
        {
            get
            {
                if (this.Count == 0) return null;

                Category result = this[0];

                for (int i = 1; i < Count; i++)
                {
                    if (this[i].Mass > result.Mass)
                    {
                        result = this[i];
                    }
                }

                return result;
            }
        }

        public double Diversity
        {
            get
            {
                Sample sample = new Sample();

                foreach (Category category in this)
                {
                    sample.Add(category.Abundance);
                }

                return sample.Diversity();
            }
        }

        public double AdditionalDistributedMass { get; private set; }



        public Composition()
            : base()
        {
            Name = string.Empty;
        }

        public Composition(string name)
            : this()
        {
            Name = name;
        }

        public Composition(string name, int capacity)
            : base(capacity)
        {
            Name = name;
        }

        public Composition(Composition example)
        {
            foreach (Category category in example)
            {
                AddCategory(new Category(category.Name));
            }

            Name = example.Name;
        }



        public void AddCategory(Category item)
        {
            base.Add(item);
            item.Parent = this;
        }

        public void AddCategory(Category item, int j)
        {
            base.Insert(j, item);
            item.Parent = this;
        }

        public Category GetCategory(string name)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == name) return this[i];
            }

            return null;
        }

        public bool ContainsNamed(string categoryName)
        {
            return GetCategory(categoryName) != null;
        }


        public double AbundanceFractionStartingFrom(Category category)
        {
            double result = 0;
            bool reached = false;

            foreach (Category cat in this)
            {
                reached |= (cat == category);
                if (reached) result += cat.AbundanceFraction;
            }

            return result;
        }

        public double BiomassFractionStartingFrom(Category category)
        {
            double result = 0;
            bool reached = false;

            foreach (Category cat in this)
            {
                reached |= (cat == category);
                if (reached) result += cat.BiomassFraction;
            }

            return result;
        }

        public Category GetNonEmptyNext(Category category)
        {
            for (int i = this.IndexOf(category) + 1; i < this.Count; i++)
            {
                if (this[i].Quantity > 0)
                {
                    return this[i];
                }
            }

            return null;
        }

        public Category GetLast()
        {
            return this.Last((Category c) => c.Quantity > 0);
        }

        public int GetIndexOfLast()
        {
            return this.IndexOf(this.GetLast());
        }


        public Category[] GetDominants()
        {
            //this.Sort();

            List<Category> result = new List<Category>();

            double maxD = this.MostDominant.Dominance;
            double minD = maxD * 0.5;

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Dominance > minD)
                    result.Add(this[i]);
            }

            return result.ToArray();
        }



        #region IFormattable

        public virtual string ToString(string format, IFormatProvider provider)
        {
            return string.Format(Resources.Interface.Interface.ResourceManager.GetString("CompositionPresentation",
                (CultureInfo)provider), Name, Count);
        }

        public virtual string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public override string ToString()
        {
            return ToString(Resources.Interface.Interface.CompositionPresentation);
        }

        #endregion

        #region IComparable

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Composition objAsPart = obj as Composition;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public int SortByNameAscending(string name1, string name2)
        {
            return name1.CompareTo(name2);
        }

        public int CompareTo(Composition comparePart)
        {
            if (comparePart == null)
                return 1;

            else
                return this.Name.CompareTo(comparePart.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(Composition other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }

        #endregion


        public void ScaleUp(double total)
        {
            if (total > 0 && this.TotalMass == 0)
            {
                return;
            }

            if (total > this.TotalMass)
            {
                this.AdditionalDistributedMass = total - this.TotalMass;
                //double totalQ = 0;

                foreach (Category category in this)
                {
                    if (category.Quantity == 0) continue;

                    double w = category.Mass / (double)category.Quantity;
                    category.Mass = category.BiomassFraction * total;
                    category.Quantity = (int)(category.Mass / w);
                }

                double totalQ = this.TotalQuantity;

                foreach (Category category in this)
                {
                    if (category.Quantity == 0) continue;
                    category.Quantity = (int)(category.AbundanceFraction * totalQ);
                }
            }
        }

        public void SetLines(DataGridViewColumn gridColumn)
        {
            gridColumn.HeaderText = this.Name;
            gridColumn.DataGridView.Rows.Clear();

            for (int i = 0; i < this.Count; i++)
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(gridColumn.DataGridView);
                gridRow.Cells[gridColumn.Index].Value = this[i].Name;
                gridRow.Height = gridColumn.DataGridView.RowTemplate.Height;
                gridColumn.DataGridView.Rows.Add(gridRow);
            }
        }

        /// <summary>
        /// Find corresponding column and updates values in it according to row number
        /// </summary>
        /// <param name="sheet">SpreadSheet to update</param>
        /// <param name="vv">Value to display</param>
        public void UpdateValues(SpreadSheet sheet, DataGridViewColumn columnNames, ValueVariant vv)
        {
            UpdateValues(columnNames, sheet.GetColumn(this.Name), vv);
        }

        /// <summary>
        /// Find corresponding column and updates values in it according to string value in specified column
        /// </summary>
        /// <param name="columnNames">Column containing categories names</param>
        /// <param name="vv">Value to display</param>
        public void UpdateValues(DataGridViewColumn columnNames, ValueVariant vv)
        {
            SpreadSheet sheet = (SpreadSheet)columnNames.DataGridView;
            UpdateValues(columnNames, sheet.GetColumn(this.Name), vv);
        }
        
        public void UpdateValues(DataGridViewColumn columnNames, DataGridViewColumn col, ValueVariant vv)
        {
            SpreadSheet sheet = (SpreadSheet)columnNames.DataGridView;

            foreach (Category cat in this)
            {
                foreach (DataGridViewRow gridRow in sheet.Rows)
                {
                    if (gridRow.Cells[columnNames.Index].Value.Equals(cat.Name))
                    {
                        object value = cat.GetValue(vv);

                        if (Convert.ToDouble(value) == 0)
                        {
                            sheet[col.Index, gridRow.Index].Value = null;
                        }
                        else
                        {
                            sheet[col.Index, gridRow.Index].Value = value;
                        }

                        break;
                    }
                }
            }
        }

        public void SetFormats(params string[] formats)
        {
            if (formats.Length > 0) UnitAbundance = formats[0];
            if (formats.Length > 1) UnitBiomass = formats[1];

            if (formats.Length > 2) FormatSampleLength = formats[2];
            if (formats.Length > 3) FormatSampleMass = formats[3];

            if (formats.Length > 4) AbundanceFormat = formats[4];
            if (formats.Length > 5) AbundanceFractionFormat = formats[5];

            if (formats.Length > 6) MassFormat = formats[6];
            if (formats.Length > 7) BiomassFormat = formats[7];
            if (formats.Length > 8) BiomassFractionFormat = formats[8];

            if (formats.Length > 9) OccuranceFormat = formats[9];
            if (formats.Length > 10) DominanceFormat = formats[10];
        }

        public string UnitAbundance;
        public string UnitBiomass; 

        public string FormatSampleLength = "g";
        public string FormatSampleMass = "g";

        public string AbundanceFormat = "n2";
        public string AbundanceFractionFormat = "p1";

        public string MassFormat = "n2";
        public string BiomassFormat = "n2";
        public string BiomassFractionFormat = "p1";

        public string OccuranceFormat = "n2";
        public string DominanceFormat = "n2";
    }
}
