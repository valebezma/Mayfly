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
        public static Artefact[] GetArtefacts(this CardStack stack, bool individuals)
        {
            List<Artefact> result = new List<Artefact>();
            result.AddRange(stack.GetCardArtefacts());
            result.AddRange(stack.GetSpeciesArtefacts());
            if (individuals) result.AddRange(stack.GetIndividualArtefacts());
            return result.ToArray();
        }

        public static CardArtefact[] GetCardArtefacts(this CardStack stack)
        {
            List<CardArtefact> result = new List<CardArtefact>();

            foreach (Data.CardRow cardRow in stack)
            {
                CardArtefact artefact = new CardArtefact(cardRow);
                artefact.EffortMissing = double.IsNaN(cardRow.GetEffort());
                double totalMass = 0;
                double sampleMass = 0;

                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    double sampled = stack.MassStratified(logRow) + stack.MassIndividual(logRow);
                    sampleMass += sampled;

                    if (logRow.IsMassNull() || double.IsNaN(logRow.Mass)) {
                        totalMass += sampled;
                    } else {
                        totalMass += logRow.Mass;
                    }
                }

                sampleMass = Math.Round(sampleMass, 3);
                totalMass = Math.Round(totalMass, 3);

                artefact.Mass = totalMass;
                artefact.UnsampledMass = totalMass - sampleMass;

                if (artefact.GetFacts() > 0) result.Add(artefact);
            }

            return result.ToArray();
        }

        public static SpeciesArtefact[] GetSpeciesArtefacts(this CardStack stack)
        {
            List<SpeciesArtefact> result = new List<SpeciesArtefact>();

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                SpeciesArtefact artefact = new SpeciesArtefact(speciesRow);

                int sampled = stack.QuantitySampled(speciesRow);
                artefact.LengthMissing = sampled - stack.Measured(speciesRow) - stack.QuantityStratified(speciesRow);


                artefact.AgeArtefact = new SpeciesFeatureArtefact(Wild.Resources.Reports.Caption.Age);
                artefact.AgeArtefact.UnmeasuredCount = sampled - stack.Treated(artefact.SpeciesRow, stack.Parent.Individual.AgeColumn);
                var gm = stack.Parent.FindGrowthModel(speciesRow.Species);
                artefact.AgeArtefact.HasRegression = gm.CombinedData.IsRegressionOK;
                if (gm.CombinedData.IsRegressionOK) artefact.AgeArtefact.Outliers = gm.CombinedData.Regression.GetOutliers(.99999);

                artefact.MassArtefact = new SpeciesFeatureArtefact(Wild.Resources.Reports.Caption.Mass);
                artefact.MassArtefact.UnmeasuredCount = sampled - stack.Treated(artefact.SpeciesRow, stack.Parent.Individual.MassColumn);
                var mm = stack.Parent.FindMassModel(speciesRow.Species);
                artefact.MassArtefact.HasRegression = mm.CombinedData.IsRegressionOK;
                if (mm.CombinedData.IsRegressionOK) artefact.MassArtefact.Outliers = mm.CombinedData.Regression.GetOutliers(.99999);

                artefact.IndividualArtefacts = stack.GetIndividualArtefacts(artefact.SpeciesRow);

                if (artefact.GetFacts() > 0) result.Add(artefact);
            }

            return result.ToArray();
        }

        public static IndividualArtefact[] GetIndividualArtefacts(this CardStack stack)
        {
            List<IndividualArtefact> result = new List<IndividualArtefact>();

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                result.AddRange(stack.GetIndividualArtefacts(speciesRow));
            }

            return result.ToArray();
        }

        public static IndividualArtefact[] GetIndividualArtefacts(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            List<IndividualArtefact> result = new List<IndividualArtefact>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                IndividualArtefact indArtefact = new IndividualArtefact(individualRow);

                indArtefact.HasRegID = !individualRow.IsRegIDNull();
                indArtefact.Treated = false;
                indArtefact.Treated |= !individualRow.IsAgeNull();
                indArtefact.Treated |= individualRow.ContainsSex;
                indArtefact.Treated |= individualRow.ContainsTrophics;
                indArtefact.Treated |= individualRow.ContainsParasites;

                if (indArtefact.GetFacts() > 0) result.Add(indArtefact);
            }

            return result.ToArray();
        }
    }

    public class CardArtefact : Artefact
    {
        public Data.CardRow Card { get; set; }

        /// <summary>
        /// Total card mass.
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Difference between total card mass and sample mass.
        /// Should be 0 (all are sampled) or positive, e. g. total mass should be more or equal than sample mass).
        /// </summary>
        public double UnsampledMass { get; set; }

        /// <summary>
        /// Effort is unable to estimate with specified information
        /// </summary>
        public bool EffortMissing { get; set; }

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

        public ArtefactCriticality OddMassCriticality
        {
            get
            {
                if (this.UnsampledMass == 0)
                {
                    return ArtefactCriticality.Normal;
                }
                else if (this.UnsampledMass > 0)
                {
                    // If Total is more than Sampled - OK
                    return ArtefactCriticality.Allowed;
                }
                else if (-this.UnsampledMass <= Mayfly.Mathematics.UserSettings.DefaultAlpha * this.Mass)
                {
                    // If Sampled more than Total around 1% or less - it is calculation artefact - OK
                    return ArtefactCriticality.NotCritical;
                }
                else
                {
                    // If Sampled significantly more than Total - it is weird and should be checked
                    return ArtefactCriticality.Critical;
                }
            }
        }



        public CardArtefact(Data.CardRow cardRow)
        {
            Card = cardRow;
        }



        public new int GetFacts()
        {
            return (this.EffortMissing ? 1 : 0) + (this.UnsampledMass < 0 ? 1 : 0);
        }

        public override string ToString()
        {
            if (EffortCriticality == ArtefactCriticality.Critical || OddMassCriticality == ArtefactCriticality.Critical)
            {
                //string result = "[" + Card.ShortPath + "]: ";
                string result = "<span class = 'hl'>" + Card.FriendlyPath + ": </span>";

                if (EffortCriticality == ArtefactCriticality.Critical)
                {
                    result += Resources.Artefact.CardEffort + "; ";
                }

                if (OddMassCriticality == ArtefactCriticality.Critical)
                {
                    result += string.Format(Resources.Artefact.CardMassOdd, -this.UnsampledMass, -this.UnsampledMass / this.Mass, this.Mass);
                }

                return result.TrimEnd("; ".ToCharArray()) + ". ";
            }
            else return string.Empty;
        }
    }

    public class SpeciesArtefact : Artefact
    {
        public Data.SpeciesRow SpeciesRow { get; set; }

        public int LengthMissing { get; set; }

        public SpeciesFeatureArtefact MassArtefact { get; set; }

        public SpeciesFeatureArtefact AgeArtefact { get; set; }

        public IndividualArtefact[] IndividualArtefacts { get; set; }

        public ArtefactCriticality Criticality
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



        public SpeciesArtefact(Data.SpeciesRow speciesRow)
        {
            SpeciesRow = speciesRow;
        }



        public new int GetFacts()
        {
            int indCount = 0;

            foreach (IndividualArtefact indArtefact in IndividualArtefacts)
            {
                indCount += indArtefact.GetFacts();
            }

            return //(stack.IndividualsMissing > 0 ? 1 : 0) +
                (this.LengthMissing > 0 ? 1 : 0) +
                (this.AgeArtefact.DeviationsCount > 0 ? 1 : 0) +
                (this.MassArtefact.DeviationsCount > 0 ? 1 : 0) + indCount;
        }

        public override string ToString()
        {
            string result = "<span class = 'hl'>" + SpeciesRow.KeyRecord.ShortName + ": </span>";
            //string result = "[" + SpeciesRow.GetReportFullPresentation() + "]: ";

            //if (IndividualsMissing > 0)
            //{
            //    result += string.Format( Resources.Artefact.Specimen,
            //         SpeciesRow.Species, Quantity,
            //         QuantityIndividuals, QuantityStratified, IndividualsMissing) + "; ";
            //}

            if (LengthMissing > 0)
            {
                result += string.Format(Resources.Artefact.Length, LengthMissing) + "; ";
            }

            // Features artefacts

            result += AgeArtefact.ToString();
            result += MassArtefact.ToString();
            
            // Individual artefacts

            List<IndividualArtefact> regedButNotTreated = new List<IndividualArtefact>();
            List<IndividualArtefact> treatedButNotReged = new List<IndividualArtefact>();
            List<IndividualArtefact> dietNotExplored = new List<IndividualArtefact>();

            foreach (IndividualArtefact indArtefact in IndividualArtefacts)
            {
                if (indArtefact.HasRegID && !indArtefact.Treated)
                {
                    regedButNotTreated.Add(indArtefact);
                }
                else if (!indArtefact.HasRegID && indArtefact.Treated)
                {
                    treatedButNotReged.Add(indArtefact);
                }

                if (indArtefact.UnweightedDietItems > 0)
                {
                    dietNotExplored.Add(indArtefact);
                }
            }

            if (regedButNotTreated.Count > 0) result += string.Format(Resources.Artefact.IndividualsRegID, regedButNotTreated.Count);
            if (treatedButNotReged.Count > 0) result += string.Format(Resources.Artefact.IndividualsTreat, treatedButNotReged.Count);
            if (dietNotExplored.Count > 0) result += string.Format(Resources.Artefact.IndividualsUnweightedDiet, dietNotExplored.Count);

            return result.TrimEnd("; .".ToCharArray()) + ". ";
        }
    }

    public class SpeciesFeatureArtefact
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



        public override string ToString()
        {
            string result = string.Empty;

            if (HasRegression)
            {
                if (UnmeasuredCount == 0)
                {
                    if (DeviationsCount != 0)
                    {
                        result += string.Format( Resources.Artefact.ValueHasRunouts, FeatureName, DeviationsCount) + "; ";
                    }
                }
                else
                {
                    if (DeviationsCount == 0)
                    {
                        result += string.Format(Resources.Artefact.ValueIsRecoverable, FeatureName, UnmeasuredCount) + "; ";
                    }
                    else
                    {
                        result += string.Format( Resources.Artefact.ValueIsRecoverableButHasRunouts, FeatureName, UnmeasuredCount, DeviationsCount) + "; ";
                    }
                }
            }
            else
            {
                if (UnmeasuredCount != 0)
                {
                    result += string.Format( Resources.Artefact.ValueIsCritical, FeatureName, UnmeasuredCount) + "; ";
                }
            }

            if (result != string.Empty) return result = result.TrimEnd("; ".ToCharArray()) + ". ";
            return result;
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
        }



        public new int GetFacts()
        {
            int result = 0;
            if (this.HasRegID && !this.Treated) result += 1;
            //if (!stack.HasRegID && stack.Treated) result += 1;
            if (this.UnweightedDietItems > 0) result += 1;
            return result;
        }

        public override string ToString()
        {
            string result = IndividualRow.GetDescription() + ": ";

            if (HasRegID && !Treated)
            {
                result += string.Format( Resources.Artefact.IndividualRegID, IndividualRow.RegID) + "; ";
            }
            else if (!HasRegID && Treated)
            {
                result +=  Resources.Artefact.IndividualTreat + "; ";
            }

            if (UnweightedDietItems > 0)
            {
                result += string.Format( Resources.Artefact.IndividualUnweightedDiet, UnweightedDietItems) + "; ";
            }

            return result.TrimEnd("; ".ToCharArray()) + ". ";
        }
    }
}
