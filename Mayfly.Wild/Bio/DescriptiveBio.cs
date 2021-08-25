﻿using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mayfly.Wild
{
    public class DescriptiveBio : Bio
    {        
        public Composition InternalData { get; private set; }

        public Composition ExternalData { get; private set; }

        public Composition CombinedData { get; private set; }



        public DescriptiveBio(Data data, Data.SpeciesRow speciesRow, DataColumn xColumn, DataColumn yColumn) :
            base(data, speciesRow, yColumn)
        {
            nameX = xColumn.Caption;
            RefreshInternal();
        }



        public override void RefreshInternal()
        {
            InternalData = new Composition(Species.Species);

            foreach (string group in Parent.Individual.Columns[nameX].GetStrings(true))
            {
                List<DataRow> rows = Parent.Individual.Columns[nameX].GetRows(group);

                if (rows.Count >= Mathematics.UserSettings.StrongSampleSize)
                {
                    Category category = InternalData.GetCategory(group);
                    if (category == null) category = new Category(group);
                    category.MassSample = Parent.Individual.Columns[nameY].GetSample(rows);
                }
            }
        }

        public void Involve(DescriptiveBio bio)
        {
            if (bio == null) return;

            if (ExternalData == null)
            {
                ExternalData = bio.InternalData;
            }
            else
            {
                ExternalData.Include(bio.InternalData);
            }

            foreach (string author in bio.Authors)
            {
                if (!Authors.Contains(author))
                {
                    Authors.Add(author);
                }
            }

            foreach (DateTime date in bio.Dates)
            {
                if (!Dates.Contains(date))
                {
                    Dates.Add(date);
                }
            }

            foreach (string place in bio.Places)
            {
                if (!Places.Contains(place))
                {
                    Places.Add(place);
                }
            }

            CombinedData = new Composition();
            CombinedData.Include(InternalData);
            CombinedData.Include(ExternalData);
        }

        public override void Involve(Bio bio)
        {
            if (bio is DescriptiveBio)
            {
                Involve((DescriptiveBio)bio);
            }
            else
            {
                throw new InvalidCastException("DescriptiveBio can involve only DescriptiveBio");
            }
        }

        public override double GetValue(object x)
        {
            Category category = CombinedData.GetCategory(x.ToString());
            if (category == null) return double.NaN;
            return category.MassSample.Mean;
        }
    }
}
