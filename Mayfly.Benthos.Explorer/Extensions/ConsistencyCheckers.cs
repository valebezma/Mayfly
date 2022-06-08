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
using Mayfly.Extensions;
using Mayfly.Species;

namespace Mayfly.Benthos.Explorer
{
    public static partial class CardStackExtensions
    {
        public static IndividualConsistencyChecker CheckConsistency(this Data.IndividualRow individualRow)
        {
            return new IndividualConsistencyChecker(individualRow);
        }

        public static LogConsistencyChecker CheckConsistency(this Data.LogRow logRow)
        {
            return new LogConsistencyChecker(logRow);
        }

        public static CardConsistencyChecker CheckConsistency(this Data.CardRow cardRow)
        {
            return new CardConsistencyChecker(cardRow);
        }

        public static SpeciesConsistencyChecker CheckConsistency(this SpeciesKey.SpeciesRow speciesRow, CardStack stack)
        {
            return new SpeciesConsistencyChecker(speciesRow, stack);
        }


        public static ConsistencyChecker[] CheckBenthosConsistency(this CardStack stack)
        {
            List<ConsistencyChecker> result = new List<ConsistencyChecker>();

            foreach (Data.CardRow cardRow in stack)
            {
                CardConsistencyChecker ccc = cardRow.CheckConsistency();

                if (ccc.ArtifactsCount > 0)
                {
                    result.Add(ccc);
                }
            }

            foreach (SpeciesKey.SpeciesRow speciesRow in stack.GetSpecies())
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
        public Data.IndividualRow IndividualRow { get; set; }

        public bool HasTally { get; set; }

        public bool Treated { get; set; }

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



        public IndividualConsistencyChecker(Data.IndividualRow individualRow)
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

        public static string[] GetCommonNotices(IEnumerable<IndividualConsistencyChecker> artifacts)
        {
            List<string> result = new List<string>();

            int regMissed = 0;

            if (artifacts != null)
            {
                foreach (IndividualConsistencyChecker indArtifact in artifacts)
                {
                    if (indArtifact.TallyCriticality > ArtifactCriticality.Normal)
                    {
                        regMissed++;
                    }
                }
            }

            if (regMissed > 0) result.Add(string.Format(Wild.Resources.Artifact.IndividualTallies, regMissed));

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
        public Data.LogRow LogRow { get; set; }

        public bool SpeciesMissing { get; set; }

        public List<IndividualConsistencyChecker> IndividualArtifacts { get; set; }

        public ArtifactCriticality SpeciesCriticality
        {
            get
            {
                if (this.SpeciesMissing)
                {
                    return ArtifactCriticality.Allowed;
                }
                else
                {
                    return ArtifactCriticality.Normal;
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
                    result = GetWorst(result, artifact.TallyCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality WorstCriticality
        {
            get
            {
                return GetWorst(SpeciesCriticality, IndividualWorstCriticality);
            }
        }



        public LogConsistencyChecker(Data.LogRow logRow)
        {
            if (logRow == null) return;

            LogRow = logRow;

            SpeciesMissing = LogRow.SpeciesRow.KeyRecord == null;

            List<IndividualConsistencyChecker> result = new List<IndividualConsistencyChecker>();
            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                IndividualConsistencyChecker indArtifact = individualRow.CheckConsistency();
                if (indArtifact.ArtifactsCount > 0) result.Add(indArtifact);
            }

            IndividualArtifacts = result;
        }



        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (SpeciesCriticality > ArtifactCriticality.Normal)
            {
                result.Add(Wild.Resources.Artifact.LogSpecies);
            }

            if (includeChildren)
            {
                result.AddRange(IndividualConsistencyChecker.GetCommonNotices(IndividualArtifacts));
            }

            return result.ToArray();
        }

        public override string ToString() => base.ToString(LogRow.GetDescription());

        public static string[] GetCommonNotices(IEnumerable<LogConsistencyChecker> artifacts)
        {
            List<string> result = new List<string>();

            if (artifacts != null)
            {
                int speciesMissing = 0;

                foreach (LogConsistencyChecker logArtifact in artifacts)
                {
                    if (logArtifact.SpeciesCriticality > ArtifactCriticality.Normal)
                    {
                        speciesMissing++;
                    }
                }

                if (speciesMissing > 0) result.Add(string.Format(Wild.Resources.Artifact.LogSpecia, speciesMissing));
            }

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
        public Data.CardRow CardRow { get; set; }

        public bool SquareMissing { get; set; }

        public bool WhereMissing { get; set; }

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

        public ArtifactCriticality SquareCriticality
        {
            get
            {
                if (this.SquareMissing)
                {
                    return ArtifactCriticality.Allowed;
                }
                else
                {
                    return ArtifactCriticality.Normal;
                }
            }
        }

        public ArtifactCriticality WhereCriticality
        {
            get
            {
                if (this.WhereMissing)
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
                    result = GetWorst(result, artifact.SpeciesCriticality);
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
                    result = GetWorst(result, artifact.TallyCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality WorstCriticality
        {
            get
            {
                return ConsistencyChecker.GetWorst(SquareCriticality, WhereCriticality, LogWorstCriticality, IndividualWorstCriticality);
            }
        }



        public CardConsistencyChecker(Data.CardRow cardRow)
        {
            CardRow = cardRow;
            SquareMissing = cardRow.IsSquareNull();
            WhereMissing = cardRow.IsWhereNull();

            List<LogConsistencyChecker> logArtifacts = new List<LogConsistencyChecker>();
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
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
                    if (LogArtifacts[i].IndividualArtifacts[j].TallyCriticality < criticality)
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

            if (SquareCriticality > ArtifactCriticality.Normal)
            {
                result.Add(Resources.Artifact.Square);
            }

            if (WhereCriticality > ArtifactCriticality.Normal)
            {
                result.Add(Wild.Resources.Artifact.Where);
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

    public class SpeciesConsistencyChecker : ConsistencyChecker
    {
        public SpeciesKey.SpeciesRow SpeciesRow { get; set; }

        public bool MissingInReference { get; set; }

        public List<LogConsistencyChecker> LogArtifacts { get; set; }

        public List<IndividualConsistencyChecker> IndividualArtifacts 
        {
            get
            {
                List<IndividualConsistencyChecker> result = new List<IndividualConsistencyChecker>();
                
                if (LogArtifacts != null)
                {
                    foreach (LogConsistencyChecker logArtifact in LogArtifacts)
                    {
                        result.AddRange(logArtifact.IndividualArtifacts);
                    }
                }

                return result;
            }
        }

        public ArtifactCriticality MissingCriticality
        {
            get
            {
                if (this.MissingInReference)
                {
                    return ArtifactCriticality.Allowed;
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
                    result = GetWorst(result, artifact.SpeciesCriticality);
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
                    result = GetWorst(result, artifact.TallyCriticality);
                }

                return result;
            }
        }

        public ArtifactCriticality WorstCriticality
        {
            get
            {
                return GetWorst(MissingCriticality, LogWorstCriticality, IndividualWorstCriticality);
            }
        }



        public SpeciesConsistencyChecker(SpeciesKey.SpeciesRow speciesRow, CardStack stack)
        {
            SpeciesRow = speciesRow;

            if (speciesRow == null) return;

            MissingInReference = SpeciesRow.RowState == DataRowState.Detached;

            List<LogConsistencyChecker> result = new List<LogConsistencyChecker>();

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
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
                    if (LogArtifacts[i].IndividualArtifacts[j].TallyCriticality < criticality)
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

            if (MissingCriticality > ArtifactCriticality.Normal)
            {
                result.Add(Wild.Resources.Artifact.LogSpecies);
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
            return base.ToString(SpeciesRow.ShortName);
        }
    }
}
