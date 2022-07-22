using Mayfly.Species;
using Mayfly.Wild;
using System.Collections.Generic;

namespace Mayfly.Benthos.Explorer
{
    public static partial class CardStackExtensions
    {
        public static ConsistencyChecker[] CheckConsistency(this CardStack stack) {
            List<ConsistencyChecker> result = new List<ConsistencyChecker>();
            result.AddRange(stack.GetCardArtifacts());
            result.AddRange(stack.GetSpeciesArtifacts());
            return result.ToArray();
        }

        public static CardArtifact[] GetCardArtifacts(this CardStack stack) {
            List<CardArtifact> result = new List<CardArtifact>();

            foreach (Wild.Survey.CardRow cardRow in stack) {
                CardArtifact artifact = new CardArtifact(cardRow);
                if (artifact.GetFacts() > 0) result.Add(artifact);
            }

            return result.ToArray();
        }

        public static SpeciesArtifact[] GetSpeciesArtifacts(this CardStack stack) {
            List<SpeciesArtifact> result = new List<SpeciesArtifact>();

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies()) {
                SpeciesArtifact artifact = new SpeciesArtifact(speciesRow);
                if (artifact.GetFacts() > 0) result.Add(artifact);
                artifact.Quantity = (int)stack.Quantity(speciesRow);
            }

            return result.ToArray();
        }
    }

    public class CardArtifact : ConsistencyChecker
    {
        public Wild.Survey.CardRow Card { get; private set; }

        public bool SamplingSquareMissing { get; private set; }



        public CardArtifact(Wild.Survey.CardRow cardRow) {
            Card = cardRow;

            SamplingSquareMissing = double.IsNaN(cardRow.GetArea());
        }



        public int GetFacts() {
            return (this.SamplingSquareMissing ? 1 : 0);
        }

        public override string ToString() {
            return base.ToString(Card.ToString());
        }

        public override string[] GetNotices(bool includeChildren) {
            List<string> result = new List<string>();

            if (SamplingSquareMissing) {
                result.Add(Resources.Artifact.Square);
            }

            return result.ToArray();
        }
    }

    public class SpeciesArtifact : ConsistencyChecker
    {
        public TaxonomicIndex.TaxonRow TaxonRow { get; private set; }

        public bool ReferenceMissing { get; private set; }

        public int Quantity { get; set; }



        public SpeciesArtifact(TaxonomicIndex.TaxonRow speciesRow) {
            TaxonRow = speciesRow;

            ReferenceMissing = !ReaderSettings.TaxonomicIndex.Contains(speciesRow.Name);
        }



        public int GetFacts() {
            return (this.ReferenceMissing ? 1 : 0);
        }

        public override string[] GetNotices(bool includeChildren) {
            List<string> result = new List<string>();

            if (ReferenceMissing) {
                result.Add(Resources.Artifact.Species);
            }

            return result.ToArray();
        }

        public override string ToString() {
            return base.ToString(TaxonRow.Name);
        }
    }
}
