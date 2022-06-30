using System.Collections.Generic;
using System;
using Mayfly.Extensions;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Mayfly.Species;

namespace Mayfly.Wild
{
    public abstract class Bio
    {
        public Data Parent { get; set; }

        //public Data.SpeciesRow Species { get; private set; }

        public string Species { get; private set; }

        public List<string> Authors { get; set; }

        public List<DateTime> Dates { get; set; }

        public List<string> Places { get; set; }

        public string Description
        {
            get
            {
                return string.Format(Resources.Interface.Interface.BioFormat, Dates.GetDatesDescription(), Places.Merge());
            }
        }

        internal string nameX;

        internal string nameY;



        public Bio(Data data, SpeciesKey.TaxonRow speciesRow, DataColumn yColumn)
        {
            if (yColumn.DataType != typeof(double))
                throw new FormatException();

            Parent = data;
            Species = speciesRow.Name;
            nameY = yColumn.Caption;

            RefreshMeta();
        }



        public void RefreshMeta()
        {
            Authors = new List<string>();
            Authors.AddRange(Parent.GetAuthors());

            Dates = new List<DateTime>();
            Dates.AddRange(Parent.GetDates());

            Places = new List<string>();
            Places.AddRange(Parent.GetPlaces());
        }

        public abstract double GetValue(object x);

        public abstract void RefreshInternal();

        public abstract void Involve(Bio bio, bool clearExisted);


        public static ContinuousBio Find(IEnumerable<ContinuousBio> bios, string species)
        {
            if (bios == null) return null;

            foreach (ContinuousBio bio in bios)
            {
                if (bio.Species == species)
                {
                    return bio;
                }
            }

            return null;
        }
    }
}
