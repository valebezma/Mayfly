using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Species;

namespace Mayfly.Wild
{
    public class ContinuousBio : Bio
    {
        public bool VisualConfirmation { get; set; }

        public BivariatePredictiveModel InternalData { get; private set; }

        public BivariatePredictiveModel ExternalData { get; private set; }

        public BivariatePredictiveModel CombinedData { get; private set; }

        public TrendType Nature { get; set; }

        //public BivariateSample Outliers;

        internal string displayNameX;

        internal string displayNameY;

        public string DisplayNameX
        {
            get { return displayNameX; }
            set
            {
                displayNameX = value;
                InternalData.Data.X.Name = value;
            }
        }

        public string DisplayNameY
        {
            get { return displayNameY; }
            set
            {
                displayNameY = value;
                InternalData.Data.Y.Name = value;
            }
        }



        public ContinuousBio(Survey data, TaxonomicIndex.TaxonRow speciesRow, DataColumn xColumn, DataColumn yColumn, TrendType type) :
            base(data, speciesRow, yColumn)
        {
            if (yColumn.DataType != typeof(double))
                throw new FormatException();

            VisualConfirmation = true;
            Nature = type;
            nameX = xColumn.Caption;
            //Outliers = new BivariateSample();

            RefreshInternal();
        }



        public void RefreshCombined()
        {
            BivariateSample sample = new BivariateSample();
            if (InternalData != null) sample.Add(InternalData.Data);
            if (ExternalData != null) sample.Add(ExternalData.Data);

            CombinedData = new BivariatePredictiveModel(sample, Nature);
            //if (CombinedData.IsRegressionOK) Outliers = CombinedData.Regression.GetOutliers(InternalData.Data, .99999);
        }

        public override void RefreshInternal()
        {
            BivariateSample biSample = new BivariateSample();

            foreach (Survey.IndividualRow individualRow in Parent.Definition.FindByName(Species).GetIndividualRows())
            {
                double x = Parent.GetIndividualValue(individualRow, nameX);
                double y = Parent.GetIndividualValue(individualRow, nameY);

                if (double.IsNaN(x)) continue;
                if (double.IsNaN(y)) continue;

                biSample.Add(x, y);
            }

            biSample.X.Name = DisplayNameX;
            biSample.Y.Name = DisplayNameY;

            InternalData = new BivariatePredictiveModel(biSample, Nature);

            RefreshCombined();
        }

        public void Involve(ContinuousBio bio, bool clearExisted)
        {
            if (bio == null) return;

            if (ExternalData == null)
            {
                ExternalData = bio.InternalData;
            }
            else
            {
                if (clearExisted)
                {
                    ExternalData.Data.Clear();
                }

                ExternalData.Data.Add(bio.InternalData.Data);
                ExternalData.Calculate();
            }

            if (clearExisted)
            {
                Authors.Clear();
                Dates.Clear();
                Places.Clear();
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

            RefreshCombined();
        }

        public override void Involve(Bio bio, bool clearExisted)
        {
            if (bio is ContinuousBio)
            {
                Involve((ContinuousBio)bio, clearExisted);
            }
            else
            {
                throw new InvalidCastException("ContinuousBio can involve only ContinuousBio");
            }
        }

        public void Reverse()
        {
            BivariatePredictiveModel t = ExternalData;
            ExternalData = InternalData;
            InternalData = t;
        }

        public double GetValue(double x, bool inversed)
        {
            //if (this.Denied) return double.NaN;

            //if (CombinedData == null || !CombinedData.IsRegressionOK) // There is no confirmed model for given species
            //{
            //    // Create new BivariateSample
            //    BivariateSample bivariateSample = BuildInternalBivariate(name);

            //    // If BivariateSample is too small - return NaN
            //    if (bivariateSample == null) return double.NaN;

            //    scatter = new BivariatePredictiveModel(bivariateSample, name);

            //    if (VisualConfirmation)
            //    {
            //        scatter.Calculate(Nature);
            //        ModelConfirm confirm = new ModelConfirm(scatter);//, XName, YName, name);

            //        if (GetConfirmation(confirm) == DialogResult.OK)
            //        {
            //            ReplaceInternalModel(name, confirm.Model);
            //            return inversed ? confirm.Model.Regression.PredictInversed(x) : confirm.Model.Regression.Predict(x);
            //        }
            //        else
            //        {
            //            Denied = true;
            //            return double.NaN;
            //        }
            //    }
            //    else
            //    {
            //        if (scatter.Regression == null) return double.NaN;
            //        ReplaceInternalModel(name, scatter);
            //        return inversed ? scatter.Regression.PredictInversed(x) : scatter.Regression.Predict(x);
            //    }
            //}
            //else // There is confirmed model for given species
            //{

            
                return CombinedData.IsRegressionOK ? (inversed ? CombinedData.Regression.PredictInversed(x) : CombinedData.Regression.Predict(x)) : double.NaN;
            //}
        }

        public double GetValue(double x)
        {
            return GetValue(x, false);
        }

        public override double GetValue(object x)
        {
            return GetValue(Convert.ToDouble(x));
        }
    }
}