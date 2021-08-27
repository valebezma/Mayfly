using Mayfly.Extensions;
using Mayfly.Wild;
using System.Collections.Generic;


namespace Mayfly.Benthos.Explorer
{
    public static partial class CardStackExtensions
    {
        public static Artefact[] GetArtefacts(this CardStack stack)
        {
            List<Artefact> result = new List<Artefact>();
            result.AddRange(stack.GetCardArtefacts());
            result.AddRange(stack.GetSpeciesArtefacts());
            return result.ToArray();
        }

        public static CardArtefact[] GetCardArtefacts(this CardStack stack)
        {
            List<CardArtefact> result = new List<CardArtefact>();

            foreach (Data.CardRow cardRow in stack)
            {
                CardArtefact artefact = new CardArtefact(cardRow);
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
                if (artefact.GetFacts() > 0) result.Add(artefact);
                artefact.Quantity = (int)stack.Quantity(speciesRow);
            }

            return result.ToArray();
        }
    }

    public class CardArtefact : Artefact
    {
        public Data.CardRow Card { get; private set; }

        public bool SamplingSquareMissing { get; private set; }



        public CardArtefact(Data.CardRow cardRow)
        {
            Card = cardRow;

            SamplingSquareMissing = cardRow.IsSquareNull();
        }



        public new int GetFacts()
        {
            return (this.SamplingSquareMissing ? 1 : 0);
        }

        public override string ToString()
        {
            return base.ToString(Card.ToString());
        }

        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (SamplingSquareMissing)
            {
                result.Add(Resources.Artefact.SampleSquare);
            }

            return result.ToArray();
        }
    }

    public class SpeciesArtefact : Artefact
    {
        public Data.SpeciesRow SpeciesRow { get; private set; }

        public bool ReferenceMissing { get; private set; }

        public int Quantity { get; set; }



        public SpeciesArtefact(Data.SpeciesRow speciesRow)
        {
            SpeciesRow = speciesRow;

            ReferenceMissing = !Benthos.UserSettings.SpeciesIndex.Contains(speciesRow.Species);
        }



        public new int GetFacts()
        {
            return (this.ReferenceMissing ? 1 : 0);
        }

        public override string[] GetNotices(bool includeChildren)
        {
            List<string> result = new List<string>();

            if (ReferenceMissing)
            {
                result.Add(Resources.Artefact.ReferenceNull);
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return base.ToString(SpeciesRow.Species);
        }
    }
}
