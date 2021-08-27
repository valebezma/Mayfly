using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using System.Data;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Statistics;
using Mayfly.Mathematics.Charts;
using Mayfly.Fish;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static IndividualArtefact GetFacts(this Data.IndividualRow individualRow)
        {
            return new IndividualArtefact(individualRow);
        }

        public static LogArtefact GetFacts(this Data.LogRow logRow)
        {
            return new LogArtefact(logRow);
        }

        public static CardArtefact GetFacts(this Data.CardRow cardRow)
        {
            return new CardArtefact(cardRow);
        }

        public static SpeciesArtefact GetFacts(this Data.SpeciesRow speciesRow, CardStack stack)
        {
            return new SpeciesArtefact(speciesRow, stack);
        }


        public static Artefact[] GetArtefacts(this CardStack stack)
        {
            List<Artefact> result = new List<Artefact>();

            foreach (Data.CardRow cardRow in stack)
            {
                CardArtefact artefact = cardRow.GetFacts();
                if (artefact.FactsCount > 0)
                {
                    result.Add(artefact);
                    //result.AddRange(artefact.LogArtefacts);
                    //result.AddRange(artefact.IndividualArtefacts);
                }
            }

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                SpeciesArtefact artefact = speciesRow.GetFacts(stack);
                if (artefact.FactsCount > 0)
                {
                    result.Add(artefact);
                    //if (artefact.MassArtefact.Criticality != ArtefactCriticality.Normal) result.Add(artefact.MassArtefact);
                    //if (artefact.AgeArtefact.Criticality != ArtefactCriticality.Normal) result.Add(artefact.AgeArtefact);
                }
            }

            return result.ToArray();
        }
    }

    public class IndividualArtefact : Artefact
    {
        public Data.IndividualRow IndividualRow { get; set; }

        public bool HasRegID { get; set; }

        public bool Treated { get; set; }

        public int UnweightedDietItems { get; set; }

        public ArtefactCriticality RegIDCriticality
        {
            get
            {
                if (this.HasRegID && !this.Treated)
                {
                    return ArtefactCriticality.NotCritical;
                }
                else if (!this.HasRegID && this.Treated)
                {
                    return ArtefactCriticality.Allowed;
                }
                else
                {
                    return ArtefactCriticality.Normal;
                }
            }
        }

        public ArtefactCriticality UnweightedDietItemsCriticality
        {
            get
            {
                if (this.UnweightedDietItems > 0)
                {
                    return ArtefactCriticality.NotCritical;
                }
                else
                {
                    return ArtefactCriticality.Normal;
                }
            }
        }



        public IndividualArtefact(Data.IndividualRow individualRow)
        {
            IndividualRow = individualRow;
            HasRegID = !individualRow.IsRegIDNull();
            Treated = false;
            Treated |= !individualRow.IsAgeNull();
            Treated |= individualRow.ContainsSex;
            Treated |= individualRow.ContainsTrophics;
            Treated |= individualRow.ContainsParasites;
        }



        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            switch (RegIDCriticality)
            {
                case ArtefactCriticality.NotCritical:
                    result.Add(string.Format(Resources.Artefact.IndividualRegID, IndividualRow.RegID));
                    break;

                case ArtefactCriticality.Allowed:
                    result.Add(Resources.Artefact.IndividualRegID_1);
                    break;
            }

            if (UnweightedDietItemsCriticality > ArtefactCriticality.Normal)
            {
                result.Add(string.Format(Resources.Artefact.IndividualUnweightedDiet, UnweightedDietItems));
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(IndividualRow.GetDescription());
        }

        public static string[] GetNotices(IEnumerable<IndividualArtefact> artefacts)
        {
            List<string> result = new List<string>();

            int regMissed = 0;
            int dietNotExplored = 0;

            if (artefacts != null)
            {
                foreach (IndividualArtefact indArtefact in artefacts)
                {
                    if (indArtefact.RegIDCriticality > ArtefactCriticality.Normal)
                    {
                        regMissed++;
                    }

                    if (indArtefact.UnweightedDietItemsCriticality > ArtefactCriticality.Normal)
                    {
                        dietNotExplored++;
                    }
                }
            }

            if (regMissed > 0) result.Add(string.Format(Resources.Artefact.IndividualsRegID, regMissed));
            if (dietNotExplored > 0) result.Add(string.Format(Resources.Artefact.IndividualsUnweightedDiet, dietNotExplored));

            return result.ToArray();
        }
    }

    public class LogArtefact : Artefact
    {
        public Data.LogRow LogRow { get; set; }

        public double Mass { get; set; }

        public double DetailedMass { get; set; }

        public int LengthMissing { get; set; }

        public IndividualArtefact[] IndividualArtefacts { get; set; }

        public ArtefactCriticality UnmeasurementsCriticality
        {
            get
            {
                if (this.LengthMissing > 0)
                {
                    return ArtefactCriticality.NotCritical;
                }
                else
                {
                    return ArtefactCriticality.Normal;
                }
            }
        }

        public ArtefactCriticality OddMassCriticality
        {
            get
            {
                if (Mass == DetailedMass) // If Total equlas Sampled - Good
                {
                    return ArtefactCriticality.Normal;
                }
                else if (Mass > DetailedMass) // If Total is more than Sampled - OK
                {                    
                    return ArtefactCriticality.Allowed;
                }
                else if ((DetailedMass / Mass) <= (1 - Mathematics.UserSettings.DefaultAlpha)) // If Sampled more than Total around 5% or less - it is calculation artefact - Fine
                {                    
                    return ArtefactCriticality.NotCritical;
                }
                else // If Sampled significantly more than Total - it is weird and should be checked
                {                    
                    return ArtefactCriticality.Critical;
                }
            }
        }

        public ArtefactCriticality WorstCriticality
        {
            get
            {
                return GetWorst(OddMassCriticality, UnmeasurementsCriticality);
            }
        }



        public LogArtefact(Data.LogRow logRow)
        {
            if (logRow == null) return;

            LogRow = logRow;

            LengthMissing = logRow.QuantitySampled() - logRow.Measured() - logRow.QuantityStratified();
            Mass = LogRow.IsMassNull() ? 0 : Math.Round(logRow.Mass, 3);
            DetailedMass = Math.Round(logRow.MassStratified() + logRow.MassIndividual(), 3);

            List<IndividualArtefact> result = new List<IndividualArtefact>();
            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                IndividualArtefact indArtefact = individualRow.GetFacts();
                if (indArtefact.FactsCount > 0) result.Add(indArtefact);
            }
            IndividualArtefacts = result.ToArray();
        }



        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (UnmeasurementsCriticality > ArtefactCriticality.Normal)
            {
                result.Add(GetNoticeUnmeasured());
            }

            if (OddMassCriticality == ArtefactCriticality.Critical)
            {
                result.Add(GetNoticeOddMass());
            }

            if (includeChildren)
            {
                result.AddRange(IndividualArtefact.GetNotices(IndividualArtefacts));
            }

            return result.ToArray();
        }

        public string GetNoticeOddMass()
        {
            return string.Format(Resources.Artefact.LogMassOdd, Mass, (DetailedMass / Mass), DetailedMass);
        }

        public string GetNoticeUnmeasured()
        {
            return string.Format(Resources.Artefact.LogLength, LengthMissing);
        }

        public override string ToString()
        {
            return base.ToString(LogRow.SpeciesRow.KeyRecord.ShortName);
        }

        public static string[] GetNotices(IEnumerable<LogArtefact> artefacts)
        {
            List<string> result = new List<string>();

            int lengthMissing = 0;
            int oddMass = 0;

            foreach (LogArtefact logArtefact in artefacts)
            {
                if (logArtefact.UnmeasurementsCriticality > ArtefactCriticality.Normal)
                {
                    lengthMissing++;
                }

                if (logArtefact.OddMassCriticality > ArtefactCriticality.Normal)
                {
                    oddMass++;
                }
            }

            if (lengthMissing > 0) result.Add(string.Format(Resources.Artefact.LogLengths, lengthMissing));
            if (oddMass > 0) result.Add(string.Format(Resources.Artefact.LogMassOdds, oddMass));
            
            return result.ToArray();
        }
    }

    public class CardArtefact : Artefact
    {
        public Data.CardRow Card { get; set; }

        public bool EffortMissing { get; set; }

        public LogArtefact[] LogArtefacts { get; set; }

        public IndividualArtefact[] IndividualArtefacts
        {
            get
            {
                List<IndividualArtefact> result = new List<IndividualArtefact>();
                foreach (LogArtefact logArtefact in LogArtefacts)
                {
                    result.AddRange(logArtefact.IndividualArtefacts);
                }
                return result.ToArray();
            }
        }

        public ArtefactCriticality EffortCriticality
        {
            get
            {
                if (this.EffortMissing)
                {
                    return ArtefactCriticality.NotCritical;
                }
                else
                {
                    return ArtefactCriticality.Normal;
                }
            }
        }

        public ArtefactCriticality LogWorstCriticality
        {
            get
            {
                ArtefactCriticality result = ArtefactCriticality.Normal;

                foreach (LogArtefact artefact in LogArtefacts)
                {
                    result = GetWorst(result, artefact.OddMassCriticality, artefact.UnmeasurementsCriticality);
                }

                return result;
            }
        }

        public ArtefactCriticality IndividualWorstCriticality
        {
            get
            {
                ArtefactCriticality result = ArtefactCriticality.Normal;

                foreach (IndividualArtefact artefact in IndividualArtefacts)
                {
                    result = GetWorst(result, artefact.RegIDCriticality, artefact.UnweightedDietItemsCriticality);
                }

                return result;
            }
        }



        public CardArtefact(Data.CardRow cardRow)
        {
            Card = cardRow;
            EffortMissing = double.IsNaN(cardRow.GetEffort());

            List<LogArtefact> logArtefacts = new List<LogArtefact>();
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                LogArtefact logArtefact = logRow.GetFacts();
                if (logArtefact.FactsCount > 0)
                {
                    logArtefacts.Add(logArtefact);
                }
            }
            LogArtefacts = logArtefacts.ToArray();
        }


        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (EffortCriticality > ArtefactCriticality.Normal)
            {
                result.Add(Resources.Artefact.CardEffort);
            }

            if (includeChildren)
            {
                result.AddRange(LogArtefact.GetNotices(LogArtefacts));
                result.AddRange(IndividualArtefact.GetNotices(IndividualArtefacts));
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(Card.FriendlyPath);
        }
    }

    public class SpeciesFeatureArtefact : Artefact
    {
        public string FeatureName { get; set; }

        public int UnmeasuredCount { get; set; }

        public bool HasRegression { get; set; }

        public int DeviationsCount { get { return Outliers == null ? 0 : Outliers.Count; } }

        public BivariateSample Outliers { get; set; }

        public ArtefactCriticality Criticality
        {
            get
            {
                if (this.HasRegression) // If sample is enough to build regression
                {
                    if (this.UnmeasuredCount == 0) // If there are no missing values
                    {
                        if (this.DeviationsCount == 0) // And there are no outliers at all
                        {
                            return ArtefactCriticality.Normal;
                        }
                        else // If there are outliers
                        {
                            return ArtefactCriticality.NotCritical;
                        }
                    }
                    else // If there are some missing values
                    {
                        if (this.DeviationsCount == 0) // If there are no outliers
                        {
                            return ArtefactCriticality.Allowed;
                        }
                        else // Or there are outliers
                        {
                            return ArtefactCriticality.Critical;
                        }
                    }
                }
                else // If sample is too small
                {
                    if (this.UnmeasuredCount == 0) // If there are no missing values
                    {
                        return ArtefactCriticality.Normal;
                    }
                    else // If there are some missing values
                    {
                        return ArtefactCriticality.Critical;
                    }
                }
            }
        }



        public SpeciesFeatureArtefact(string featureName)
        {
            FeatureName = featureName;
        }


        
        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (HasRegression)
            {
                if (UnmeasuredCount == 0)
                {
                    if (DeviationsCount != 0)
                    {
                        result.Add(string.Format( Resources.Artefact.ValueHasOutliers, FeatureName, DeviationsCount));
                    }
                }
                else
                {
                    if (DeviationsCount == 0)
                    {
                        result.Add(string.Format(Resources.Artefact.ValueIsRecoverable, FeatureName, UnmeasuredCount));
                    }
                    else
                    {
                        result.Add(string.Format( Resources.Artefact.ValueIsRecoverableButHasOutliers, FeatureName, UnmeasuredCount, DeviationsCount));
                    }
                }
            }
            else
            {
                if (UnmeasuredCount != 0)
                {
                    result.Add(string.Format( Resources.Artefact.ValueIsCritical, FeatureName, UnmeasuredCount));
                }
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(FeatureName);
        }
    }

    public class SpeciesArtefact : Artefact
    {
        public Data.SpeciesRow SpeciesRow { get; set; }

        public SpeciesFeatureArtefact MassArtefact { get; set; }

        public SpeciesFeatureArtefact AgeArtefact { get; set; }

        public LogArtefact[] LogArtefacts { get; set; }

        public IndividualArtefact[] IndividualArtefacts 
        {
            get
            {
                List<IndividualArtefact> result = new List<IndividualArtefact>();
                foreach (LogArtefact logArtefact in LogArtefacts)
                {
                    result.AddRange(logArtefact.IndividualArtefacts);
                }
                return result.ToArray();
            }
        }

        public ArtefactCriticality Criticality
        {
            get
            {
                return Artefact.GetWorst(AgeArtefact.Criticality, MassArtefact.Criticality);
            }
        }



        public SpeciesArtefact(Data.SpeciesRow speciesRow, CardStack stack)
        {
            SpeciesRow = speciesRow;

            int sampled = stack.QuantitySampled(speciesRow);

            AgeArtefact = new SpeciesFeatureArtefact(Wild.Resources.Reports.Caption.Age);
            AgeArtefact.UnmeasuredCount = sampled - stack.Treated(SpeciesRow, stack.Parent.Individual.AgeColumn);
            var gm = stack.Parent.FindGrowthModel(speciesRow.Species);
            AgeArtefact.HasRegression = gm.CombinedData.IsRegressionOK;
            if (gm.CombinedData.IsRegressionOK) AgeArtefact.Outliers = gm.CombinedData.Regression.GetOutliers(.99999);

            MassArtefact = new SpeciesFeatureArtefact(Wild.Resources.Reports.Caption.Mass);
            MassArtefact.UnmeasuredCount = sampled - stack.Treated(SpeciesRow, stack.Parent.Individual.MassColumn);
            var mm = stack.Parent.FindMassModel(speciesRow.Species);
            MassArtefact.HasRegression = mm != null && mm.CombinedData.IsRegressionOK;
            if (mm != null && mm.CombinedData.IsRegressionOK) MassArtefact.Outliers = mm.CombinedData.Regression.GetOutliers(.99999);

            List<LogArtefact> result = new List<LogArtefact>();
            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                LogArtefact logArtefact = logRow.GetFacts();
                if (logArtefact.FactsCount > 0) result.Add(logArtefact);
            }
            LogArtefacts = result.ToArray();
        }




        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();
            result.AddRange(AgeArtefact.GetNotices());
            result.AddRange(MassArtefact.GetNotices());

            if (includeChildren)
            {
                result.AddRange(LogArtefact.GetNotices(LogArtefacts));
                result.AddRange(IndividualArtefact.GetNotices(IndividualArtefacts));
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(SpeciesRow.KeyRecord.ShortName);
        }
    }
}
