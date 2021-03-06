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
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static IndividualConsistencyChecker CheckConsistency(this Wild.Survey.IndividualRow individualRow)
        {
            return new IndividualConsistencyChecker(individualRow);
        }

        public static LogConsistencyChecker CheckConsistency(this Wild.Survey.LogRow logRow)
        {
            return new LogConsistencyChecker(logRow);
        }

        public static CardConsistencyChecker CheckConsistency(this Wild.Survey.CardRow cardRow)
        {
            return new CardConsistencyChecker(cardRow);
        }

        public static SpeciesConsistencyChecker CheckConsistency(this TaxonomicIndex.TaxonRow speciesRow, CardStack stack)
        {
            return new SpeciesConsistencyChecker(speciesRow, stack);
        }


        public static ConsistencyChecker[] CheckConsistency(this CardStack stack)
        {
            List<ConsistencyChecker> result = new List<ConsistencyChecker>();

            foreach (Wild.Survey.CardRow cardRow in stack)
            {
                CardConsistencyChecker ccc = cardRow.CheckConsistency();

                if (ccc.ArtifactsCount > 0)
                {
                    result.Add(ccc);
                }
            }

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                SpeciesConsistencyChecker scc = speciesRow.CheckConsistency(stack);

                if (scc.ArtifactsCount > 0)
                {
                    result.Add(scc);
                }
            }

            return result.ToArray();
        }
    }

    public class IndividualConsistencyChecker : ConsistencyChecker
    {
        public Wild.Survey.IndividualRow IndividualRow { get; set; }

        public bool HasTally { get; set; }

        public bool Treated { get; set; }

        public int UnweightedDietItems { get; set; }

        public ArtifactCriticality TallyCriticality
        {
            get
            {
                if (this.HasTally && !this.Treated)
                {
                    return ArtifactCriticality.Bad;
                }
                else if (!this.HasTally && this.Treated)
                {
                    return ArtifactCriticality.Allowed;
                }
                else
                {
                    return ArtifactCriticality.Normal;
                }
            }
        }

        public ArtifactCriticality UnweightedDietItemsCriticality
        {
            get
            {
                if (this.UnweightedDietItems > 0)
                {
                    return ArtifactCriticality.Bad;
                }
                else
                {
                    return ArtifactCriticality.Normal;
                }
            }
        }



        public IndividualConsistencyChecker(Wild.Survey.IndividualRow individualRow)
        {
            IndividualRow = individualRow;
            HasTally = !individualRow.IsTallyNull();
            Treated = false;
            Treated |= !individualRow.IsAgeNull();
            Treated |= individualRow.ContainsSex;
            Treated |= individualRow.ContainsTrophics;
            Treated |= individualRow.ContainsParasites;
        }



        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            switch (TallyCriticality)
            {
                case ArtifactCriticality.Bad:
                    result.Add(GetNoticeTallyOdd());
                    break;

                case ArtifactCriticality.Allowed:
                    result.Add(GetNoticeTallyMissing());
                    break;
            }

            if (UnweightedDietItemsCriticality > ArtifactCriticality.Normal)
            {
                result.Add(GetNoticeUnweightedDiet());
            }

            return result.ToArray();
        }

        public override string ToString() => base.ToString(IndividualRow.GetDescription());

        public string GetNoticeTallyOdd()
        {
            return string.Format(Wild.Resources.Artifact.IndividualTallyOdd, IndividualRow.Tally);
        }

        public string GetNoticeTallyMissing()
        {
            return Wild.Resources.Artifact.IndividualTallyMissing;
        }

        public string GetNoticeUnweightedDiet()
        {
            return string.Format(Resources.Artifact.IndividualUnweightedDiet, UnweightedDietItems);
        }

        public static string[] GetCommonNotices(IEnumerable<IndividualConsistencyChecker> artifacts)
        {
            List<string> result = new List<string>();

            int regMissed = 0;
            int dietNotExplored = 0;

            if (artifacts != null)
            {
                foreach (IndividualConsistencyChecker indArtifact in artifacts)
                {
                    if (indArtifact.TallyCriticality > ArtifactCriticality.Normal)
                    {
                        regMissed++;
                    }

                    if (indArtifact.UnweightedDietItemsCriticality > ArtifactCriticality.Normal)
                    {
                        dietNotExplored++;
                    }
                }
            }

            if (regMissed > 0) result.Add(string.Format(Wild.Resources.Artifact.IndividualTallies, regMissed));
            if (dietNotExplored > 0) result.Add(string.Format(Resources.Artifact.IndividualUnweightedDiets, dietNotExplored));

            return result.ToArray();
        }

        public static string[] GetAllNotices(IEnumerable<IndividualConsistencyChecker> artifacts)
        {
            List<string> result = new List<string>();

            if (artifacts != null)
            {
                foreach (IndividualConsistencyChecker artifact in artifacts)
                {
                    if (artifact.ArtifactsCount > 0) result.Add(artifact.ToString());
                }
            }

            return result.ToArray();
        }
    }

    public class LogConsistencyChecker : ConsistencyChecker
    {
        public Wild.Survey.LogRow LogRow { get; set; }

        public double Mass { get; set; }

        public double DetailedMass { get; set; }

        public int LengthMissing { get; set; }

        public List<IndividualConsistencyChecker> IndividualArtifacts { get; set; }

        public ArtifactCriticality UnmeasurementsCriticality
        {
            get
            {
                if (this.LengthMissing > 0)
                {
                    return ArtifactCriticality.Bad;
                }
                else
                {
                    return ArtifactCriticality.Normal;
                }
            }
        }

        public ArtifactCriticality OddMassCriticality
        {
            get
            {
                if (Mass == DetailedMass) // If Total equlas Sampled - Good
                {
                    return ArtifactCriticality.Normal;
                }
                else if (Mass > DetailedMass) // If Total is more than Sampled - OK
                {                    
                    return ArtifactCriticality.Allowed;
                }
                else if ((DetailedMass / Mass) <= (1 + Mathematics.UserSettings.DefaultAlpha)) // If Sampled more than Total around 5% or less - it is calculation artifact - Fine
                {                    
                    return ArtifactCriticality.Bad;
                }
                else // If Sampled significantly more than Total - it is weird and should be checked
                {                    
                    return ArtifactCriticality.Critical;
                }
            }
        }

        public ArtifactCriticality IndividualWorstCriticality
        {
            get
            {
                ArtifactCriticality result = ArtifactCriticality.Normal;

                foreach (IndividualConsistencyChecker artifact in IndividualArtifacts)
                {
                    result = GetWorst(result, artifact.UnweightedDietItemsCriticality, artifact.TallyCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality WorstCriticality
        {
            get
            {
                return GetWorst(OddMassCriticality, UnmeasurementsCriticality, IndividualWorstCriticality);
            }
        }



        public LogConsistencyChecker(Wild.Survey.LogRow logRow)
        {
            if (logRow == null) return;

            LogRow = logRow;

            LengthMissing = logRow.QuantitySampled() - logRow.Measured() - logRow.QuantityStratified();
            Mass = LogRow.IsMassNull() ? 0 : Math.Round(logRow.Mass, 3);
            DetailedMass = Math.Round(logRow.MassStratified() + logRow.MassIndividual(), 3);

            List<IndividualConsistencyChecker> result = new List<IndividualConsistencyChecker>();
            foreach (Wild.Survey.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                IndividualConsistencyChecker indArtifact = individualRow.CheckConsistency();
                if (indArtifact.ArtifactsCount > 0) result.Add(indArtifact);
            }
            IndividualArtifacts = result;
        }



        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (UnmeasurementsCriticality > ArtifactCriticality.Normal)
            {
                result.Add(GetNoticeUnmeasured());
            }

            if (OddMassCriticality == ArtifactCriticality.Critical)
            {
                result.Add(GetNoticeOddMass());
            }

            if (includeChildren)
            {
                result.AddRange(IndividualConsistencyChecker.GetCommonNotices(IndividualArtifacts));
            }

            return result.ToArray();
        }

        public string GetNoticeOddMass()
        {
            switch (OddMassCriticality)
            {
                case ArtifactCriticality.Allowed:
                    return string.Format(Wild.Resources.Artifact.LogMassMissing, Mass, DetailedMass);

                case ArtifactCriticality.Bad:
                case ArtifactCriticality.Critical:
                    return string.Format(Wild.Resources.Artifact.LogMassOdd, Mass, Mass == 0 ? 1d : (DetailedMass / Mass) - 1d, DetailedMass);

                default:
                    return string.Empty;
            }
        }

        public string GetNoticeUnmeasured()
        {
            return string.Format(Wild.Resources.Artifact.LogLength, LengthMissing);
        }

        public override string ToString() => base.ToString(LogRow.GetDescription());

        public static string[] GetCommonNotices(IEnumerable<LogConsistencyChecker> artifacts)
        {
            List<string> result = new List<string>();

            int lengthMissing = 0;
            int oddMass = 0;

            foreach (LogConsistencyChecker logArtifact in artifacts)
            {
                if (logArtifact.UnmeasurementsCriticality > ArtifactCriticality.Normal)
                {
                    lengthMissing++;
                }

                if (logArtifact.OddMassCriticality > ArtifactCriticality.Normal)
                {
                    oddMass++;
                }
            }

            if (lengthMissing > 0) result.Add(string.Format(Wild.Resources.Artifact.LogLengths, lengthMissing));
            if (oddMass > 0) result.Add(string.Format(Wild.Resources.Artifact.LogMassOdds, oddMass));
            
            return result.ToArray();
        }

        public static string[] GetAllNotices(IEnumerable<LogConsistencyChecker> artifacts)
        {
            List<string> result = new List<string>();

            foreach (LogConsistencyChecker artifact in artifacts)
            {
                if (artifact.ArtifactsCount > 0) result.Add(artifact.ToString());
            }
            
            return result.ToArray();
        }
    }

    public class CardConsistencyChecker : ConsistencyChecker
    {
        public Wild.Survey.CardRow CardRow { get; set; }

        public bool EffortMissing { get; set; }

        public List<LogConsistencyChecker> LogArtifacts { get; set; }

        public List<IndividualConsistencyChecker> IndividualArtifacts
        {
            get
            {
                List<IndividualConsistencyChecker> result = new List<IndividualConsistencyChecker>();
                foreach (LogConsistencyChecker logArtifact in LogArtifacts)
                {
                    result.AddRange(logArtifact.IndividualArtifacts);
                }
                return result;
            }
        }

        public ArtifactCriticality EffortCriticality
        {
            get
            {
                if (this.EffortMissing)
                {
                    return ArtifactCriticality.Bad;
                }
                else
                {
                    return ArtifactCriticality.Normal;
                }
            }
        }

        public ArtifactCriticality LogWorstCriticality
        {
            get
            {
                ArtifactCriticality result = ArtifactCriticality.Normal;

                foreach (LogConsistencyChecker artifact in LogArtifacts)
                {
                    result = GetWorst(result, artifact.OddMassCriticality, artifact.UnmeasurementsCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality IndividualWorstCriticality
        {
            get
            {
                ArtifactCriticality result = ArtifactCriticality.Normal;

                foreach (IndividualConsistencyChecker artifact in IndividualArtifacts)
                {
                    result = GetWorst(result, artifact.TallyCriticality, artifact.UnweightedDietItemsCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality WorstCriticality
        {
            get
            {
                return ConsistencyChecker.GetWorst(EffortCriticality, LogWorstCriticality, IndividualWorstCriticality);
            }
        }



        public CardConsistencyChecker(Wild.Survey.CardRow cardRow)
        {
            CardRow = cardRow;
            EffortMissing = double.IsNaN(cardRow.GetEffort());

            List<LogConsistencyChecker> logArtifacts = new List<LogConsistencyChecker>();
            foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
            {
                LogConsistencyChecker logArtifact = logRow.CheckConsistency();
                if (logArtifact.ArtifactsCount > 0)
                {
                    logArtifacts.Add(logArtifact);
                }
            }
            LogArtifacts = logArtifacts;
        }

        public void Filterate(ArtifactCriticality criticality)
        {
            for (int i = 0; i < LogArtifacts.Count; i++)
            {
                for (int j = 0; j < LogArtifacts[i].IndividualArtifacts.Count; j++)
                {
                    if (LogArtifacts[i].IndividualArtifacts[j].TallyCriticality < criticality &&
                        LogArtifacts[i].IndividualArtifacts[j].UnweightedDietItemsCriticality < criticality)
                    {
                        LogArtifacts[i].IndividualArtifacts.RemoveAt(j);
                        j--;
                    }
                }

                if (LogArtifacts[i].WorstCriticality < criticality)
                {
                    LogArtifacts.RemoveAt(i);
                    i--;
                }
            }
        }


        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (EffortCriticality > ArtifactCriticality.Normal)
            {
                result.Add(Resources.Artifact.CardEffort);
            }

            if (includeChildren)
            {
                result.AddRange(LogConsistencyChecker.GetCommonNotices(LogArtifacts));
                result.AddRange(IndividualConsistencyChecker.GetCommonNotices(IndividualArtifacts));
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(CardRow.FriendlyPath);
        }
    }

    public class SpeciesFeatureConsistencyChecker : ConsistencyChecker
    {
        public string FeatureName { get; set; }

        public int UnmeasuredCount { get; set; }

        public bool HasRegression { get; set; }

        public int DeviationsCount { get { return Outliers == null ? 0 : Outliers.Count; } }

        public BivariateSample Outliers { get; set; }

        public ArtifactCriticality Criticality
        {
            get
            {
                if (this.HasRegression) // If sample is enough to build regression
                {
                    if (this.UnmeasuredCount == 0) // If there are no missing values
                    {
                        if (this.DeviationsCount == 0) // And there are no outliers at all
                        {
                            return ArtifactCriticality.Normal;
                        }
                        else // If there are outliers
                        {
                            return ArtifactCriticality.Bad;
                        }
                    }
                    else // If there are some missing values
                    {
                        if (this.DeviationsCount == 0) // If there are no outliers
                        {
                            return ArtifactCriticality.Allowed;
                        }
                        else // Or there are outliers
                        {
                            return ArtifactCriticality.Critical;
                        }
                    }
                }
                else // If sample is too small
                {
                    if (this.UnmeasuredCount == 0) // If there are no missing values
                    {
                        return ArtifactCriticality.Normal;
                    }
                    else // If there are some missing values
                    {
                        return ArtifactCriticality.Critical;
                    }
                }
            }
        }



        public SpeciesFeatureConsistencyChecker(string featureName)
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
                        result.Add(string.Format(Wild.Resources.Artifact.ValueHasOutliers, FeatureName, DeviationsCount));
                    }
                }
                else
                {
                    if (DeviationsCount == 0)
                    {
                        result.Add(string.Format(Wild.Resources.Artifact.ValueIsRecoverable, FeatureName, UnmeasuredCount));
                    }
                    else
                    {
                        result.Add(string.Format(Wild.Resources.Artifact.ValueIsRecoverableButHasOutliers, FeatureName, UnmeasuredCount, DeviationsCount));
                    }
                }
            }
            else
            {
                if (UnmeasuredCount != 0)
                {
                    result.Add(string.Format(Wild.Resources.Artifact.ValueIsCritical, FeatureName, UnmeasuredCount));
                }
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(FeatureName);
        }

        public static SpeciesFeatureConsistencyChecker Empty
        {
            get
            {
                return new SpeciesFeatureConsistencyChecker(string.Empty);
            }
        }
    }

    public class SpeciesConsistencyChecker : ConsistencyChecker
    {
        public TaxonomicIndex.TaxonRow TaxonRow { get; set; }

        public SpeciesFeatureConsistencyChecker MassInspector { get; set; }

        public SpeciesFeatureConsistencyChecker AgeInspector { get; set; }

        public List<LogConsistencyChecker> LogArtifacts { get; set; }

        public List<IndividualConsistencyChecker> IndividualArtifacts 
        {
            get
            {
                List<IndividualConsistencyChecker> result = new List<IndividualConsistencyChecker>();
                foreach (LogConsistencyChecker logArtifact in LogArtifacts)
                {
                    result.AddRange(logArtifact.IndividualArtifacts);
                }
                return result;
            }
        }

        public ArtifactCriticality LogWorstCriticality
        {
            get
            {
                ArtifactCriticality result = ArtifactCriticality.Normal;

                foreach (LogConsistencyChecker artifact in LogArtifacts)
                {
                    result = GetWorst(result, artifact.OddMassCriticality, artifact.UnmeasurementsCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality IndividualWorstCriticality
        {
            get
            {
                ArtifactCriticality result = ArtifactCriticality.Normal;

                foreach (IndividualConsistencyChecker artifact in IndividualArtifacts)
                {
                    result = GetWorst(result, artifact.UnweightedDietItemsCriticality, artifact.TallyCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality WorstCriticality
        {
            get
            {
                return GetWorst(AgeInspector.Criticality, MassInspector.Criticality, LogWorstCriticality, IndividualWorstCriticality);
            }
        }



        public SpeciesConsistencyChecker(TaxonomicIndex.TaxonRow speciesRow, CardStack stack)
        {
            TaxonRow = speciesRow;

            if (speciesRow == null) return;

            int sampled = stack.QuantitySampled(speciesRow);

            AgeInspector = new SpeciesFeatureConsistencyChecker(Wild.Resources.Reports.Caption.Age);
            AgeInspector.UnmeasuredCount = sampled - stack.Treated(TaxonRow, stack.Parent.Individual.AgeColumn);
            var gm = stack.Parent.FindGrowthModel(speciesRow.Name);
            if (gm != null) AgeInspector.HasRegression = gm.CombinedData.IsRegressionOK;
            if (gm != null && gm.CombinedData.IsRegressionOK) AgeInspector.Outliers = gm.CombinedData.Regression.GetOutliers(gm.InternalData.Data, .99999);

            MassInspector = new SpeciesFeatureConsistencyChecker(Wild.Resources.Reports.Caption.Mass);
            MassInspector.UnmeasuredCount = sampled - stack.Treated(TaxonRow, stack.Parent.Individual.MassColumn);
            var mm = stack.Parent.FindMassModel(speciesRow.Name);
            if (mm != null) MassInspector.HasRegression = mm != null && mm.CombinedData.IsRegressionOK;
            if (mm != null && mm.CombinedData.IsRegressionOK) MassInspector.Outliers = mm.CombinedData.Regression.GetOutliers(mm.InternalData.Data, .99999);

            List<LogConsistencyChecker> result = new List<LogConsistencyChecker>();
            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                LogConsistencyChecker logArtifact = logRow.CheckConsistency();
                if (logArtifact.ArtifactsCount > 0) result.Add(logArtifact);
            }
            LogArtifacts = result;
        }




        public void Filterate(ArtifactCriticality criticality)
        {
            for (int i = 0; i < LogArtifacts.Count; i++)
            {
                for (int j = 0; j < LogArtifacts[i].IndividualArtifacts.Count; j++)
                {
                    if (LogArtifacts[i].IndividualArtifacts[j].TallyCriticality < criticality &&
                        LogArtifacts[i].IndividualArtifacts[j].UnweightedDietItemsCriticality < criticality)
                    {
                        LogArtifacts[i].IndividualArtifacts.RemoveAt(j);
                        j--;
                    }
                }

                if (LogArtifacts[i].WorstCriticality < criticality)
                {
                    LogArtifacts.RemoveAt(i);
                    i--;
                }
            }

            if (AgeInspector.Criticality < criticality)
            {
                AgeInspector = SpeciesFeatureConsistencyChecker.Empty;
            }

            if (MassInspector.Criticality < criticality)
            {
                MassInspector = SpeciesFeatureConsistencyChecker.Empty;
            }
        }

        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();
            result.AddRange(AgeInspector.GetNotices());
            result.AddRange(MassInspector.GetNotices());

            if (includeChildren)
            {
                result.AddRange(LogConsistencyChecker.GetCommonNotices(LogArtifacts));
                result.AddRange(IndividualConsistencyChecker.GetCommonNotices(IndividualArtifacts));
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(TaxonRow.CommonName);
        }
    }
}
