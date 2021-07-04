using Mayfly.Extensions;
using Mayfly.Waters;
using System;
using System.Collections.Generic;
using System.IO;
using Mayfly.Fish;
using Mayfly.Geographics;
using System.Linq;
using Mayfly.Wild;
using Mayfly.Fish.Explorer;
using Mayfly.Benthos.Explorer;

namespace Mayfly.Prospect
{
    public class ComplexSurvey
    {
        public CardStack PlanktonCards { get; set; }

        public CardStack BenthosCards { get; set; }

        public CardStack FishCards { get; set; }

        public int CardsCount { get; private set; }

        public List<DateTime> Dates { get; private set; }

        public List<WatersKey.WaterRow> Waters { get; private set; }

        public List<string> Investigators { get; private set; }



        public ComplexSurvey()
        { }



        public void SetValues()
        {
            this.CardsCount = PlanktonCards.Count + BenthosCards.Count + FishCards.Count;

            this.Dates = new List<DateTime>();
            this.Dates.AddRange(this.PlanktonCards.GetDates());
            this.Dates.AddRange(this.BenthosCards.GetDates());
            this.Dates.AddRange(this.FishCards.GetDates());
            this.Dates.Sort();

            #region Authorities

            this.Investigators = new List<string>();

            foreach (string investigator in this.PlanktonCards.GetInvestigators())
            {
                if (!this.Investigators.Contains(investigator))
                {
                    this.Investigators.Add(investigator);
                }
            }

            foreach (string investigator in this.BenthosCards.GetInvestigators())
            {
                if (!this.Investigators.Contains(investigator))
                {
                    this.Investigators.Add(investigator);
                }
            }

            foreach (string investigator in this.FishCards.GetInvestigators())
            {
                if (!this.Investigators.Contains(investigator))
                {
                    this.Investigators.Add(investigator);
                }
            }

            #endregion

            #region Waters

            if (Wild.UserSettings.WatersIndex == null) {
                Wild.UserSettings.WatersIndex = new WatersKey();
            }

            this.Waters = new List<WatersKey.WaterRow>();

            foreach (Data.WaterRow waterRow in this.PlanktonCards.GetWaters())
            {
                WatersKey.WaterRow refRow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.ID);

                if (refRow == null) {
                    Wild.UserSettings.WatersIndex.Water.Import(waterRow);
                    refRow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.ID);
                }

                if (!this.Waters.Contains(refRow)) {
                    this.Waters.Add(refRow);
                }
            }

            foreach (Data.WaterRow waterRow in this.BenthosCards.GetWaters())
            {
                WatersKey.WaterRow refRow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.ID);

                if (refRow == null) {
                    Wild.UserSettings.WatersIndex.Water.Import(waterRow);
                    refRow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.ID);
                }

                if (!this.Waters.Contains(refRow)) {
                    this.Waters.Add(refRow);
                }
            }

            foreach (Data.WaterRow waterRow in this.FishCards.GetWaters())
            {
                WatersKey.WaterRow refRow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.ID);

                if (refRow == null) {
                    Wild.UserSettings.WatersIndex.Water.Import(waterRow);
                    refRow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.ID);
                }

                if (!this.Waters.Contains(refRow)) {
                    this.Waters.Add(refRow);
                }
            }

            #endregion
        }



        public Waypoint[] GetLocations()
        {
            List<Waypoint> result = new List<Waypoint>();

            foreach (Data.CardRow cardRow in this.PlanktonCards)
            {
                if (!cardRow.IsWhereNull())
                {
                    result.Add(cardRow.Position);
                }
            }

            foreach (Data.CardRow cardRow in this.BenthosCards)
            {
                if (!cardRow.IsWhereNull())
                {
                    result.Add(cardRow.Position);
                }
            }

            foreach (Data.CardRow cardRow in this.FishCards)
            {
                if (!cardRow.IsWhereNull())
                {
                    result.Add(cardRow.Position);
                }
            }

            result.Sort();

            return result.ToArray();
        }

        public double[] GetDepths()
        {
            List<double> result = new List<double>();

            foreach (Data.CardRow cardRow in this.PlanktonCards)
            {
                if (!cardRow.IsDepthNull())
                {
                    result.Add(cardRow.Depth);
                }
            }

            foreach (Data.CardRow cardRow in this.BenthosCards)
            {
                if (!cardRow.IsDepthNull())
                {
                    result.Add(cardRow.Depth);
                }
            }

            foreach (Data.CardRow cardRow in this.FishCards)
            {
                if (!cardRow.IsDepthNull())
                {
                    result.Add(cardRow.Depth);
                }
            }

            result.Sort();

            return result.ToArray();
        }

        public double[] GetFlowRates()
        {
            List<double> result = new List<double>();

            foreach (Data.CardRow cardRow in this.PlanktonCards)
            {
                if (!cardRow.StateOfWater.IsFlowRateNull())
                {
                    result.Add(cardRow.StateOfWater.FlowRate);
                }
            }

            foreach (Data.CardRow cardRow in this.BenthosCards)
            {
                if (!cardRow.StateOfWater.IsFlowRateNull())
                {
                    result.Add(cardRow.StateOfWater.FlowRate);
                }
            }

            foreach (Data.CardRow cardRow in this.FishCards)
            {
                if (!cardRow.StateOfWater.IsFlowRateNull())
                {
                    result.Add(cardRow.StateOfWater.FlowRate);
                }
            }

            result.Sort();

            return result.ToArray();
        }

        public string GetLocationDescription()
        {
            Waypoint[] locations = this.GetLocations();
            double radius = 0.0;
            Waypoint center = Waypoint.GetCenterOf(locations, ref radius);

            if (locations.Length == 0)
            {
                return Constants.Null;
            }
            else if (locations.Length == 1)
            {
                return locations[0].ToString();
            }
            else
            {
                return string.Format(Mayfly.Resources.Interface.LocationRadius, radius, center);
            }
        }

        public string GetLocationLink()
        {
            Waypoint[] locations = this.GetLocations();

            if (locations.Length == 0) {
                return Constants.Null;
            } else {
                return Waypoint.GetPlaceableLink(locations, "dms");
            }
        }

        public string GetDepthDescription()
        {
            double[] depths = this.GetDepths();

            if (depths.Length == 0)
            {
                return Constants.Null;
            }
            else if (depths.Length == 1)
            {
                return depths[0].ToString();
            }
            else
            {
                return string.Format("{0}-{1}", depths.Min(), depths.Max());
            }
        }

        public string GetFlowRateDescription()
        {
            double[] flowrates = this.GetFlowRates();

            if (flowrates.Length == 0)
            {
                return Constants.Null;
            }
            else if (flowrates.Length == 1)
            {
                return flowrates[0].ToString();
            }
            else
            {
                return string.Format("{0}-{1}", flowrates.Min(), flowrates.Max());
            }
        }


        public void AddEnvironmentReport(Report report)
        {
            report.AddSubtitle3(Resources.Reports.Brief.Environment);

            Report.Table table1 = new Report.Table();

            table1.StartRow();
            table1.AddCellPrompt(Resources.Reports.Brief.EnvironmentLocation, this.GetLocationLink(), 2);
            table1.EndRow();

            table1.StartRow();
            table1.AddCellPrompt(Resources.Reports.Brief.EnvironmentDepth, this.GetDepthDescription());
            table1.AddCellPrompt(Resources.Reports.Brief.EnvironmentFlowrate, this.GetFlowRateDescription());
            table1.EndRow();

            report.AddTable(table1);
        }

        public void AddPlanktonReport(Report report)
        {
            report.AddSubtitle3("Brief on plankton community survey");
        }

        public void AddBenthosReport(Report report, Species.SpeciesKey.BaseRow baseRow)
        {
            report.AddSubtitle3(Resources.Reports.Brief.BriefBenthos);
            BenthosCards.AddBrief(report, baseRow);            
        }

        public void AddFishReport(Report report, FishSamplerType gearType,
            Composition composition, string formatn, string formatb)
        {
            report.AddSubtitle3(Resources.Reports.Brief.BriefFish);
            Fish.Explorer.CardStackExtensions.AddCommon(FishCards, report);

            report.AddParagraph(Resources.Reports.Brief.BriefFish1, report.NextTableNumber);
            FishCards.AddDetailedSpeciesSamplesReport(report);

            if (composition.Count > 0)
            {
                report.AddParagraph(Resources.Reports.Brief.BriefFish2, gearType.ToDisplay(),
                    new UnitEffort(ExpressionVariant.Volume).Unit, report.NextTableNumber);

                composition.AddCompositionReport(report, FishCards, formatn, formatb);
            }
        }
    }

    public class ComplexSurveyRoot : ComplexSurvey
    {
        public Data PlanktonData { get; set; }

        public Data BenthosData { get; set; }

        public Data FishData { get; set; }

        public List<WaterComplexSurvey> WaterSurveys { get; private set; }



        public ComplexSurveyRoot()
        {
            DateTime lastUpdated = File.GetLastWriteTime(UserSettingPaths.LocalFishCopyPath);

            if ((DateTime.Now - lastUpdated).TotalDays > UserSettings.UpdateFrequency)
            {
                Service.UpdateLocalData();
            }

            LoadData();
        }



        public void LoadData()
        {
            this.PlanktonData = new Data();
            this.PlanktonData.Read(UserSettingPaths.LocalPlanktonCopyPath);
            //this.PlanktonData.InitializeBio();

            this.BenthosData = new Data();
            this.BenthosData.Read(UserSettingPaths.LocalBenthosCopyPath);
            this.BenthosData.InitializeBio();

            this.FishData = new Data();
            this.FishData.Read(UserSettingPaths.LocalFishCopyPath);
            this.FishData.InitializeBio();

            PlanktonCards = PlanktonData.GetStack();
            BenthosCards = BenthosData.GetStack();
            FishCards = FishData.GetStack();

            SetValues();

            Arrange();
        }

        private void Arrange()
        {
            this.WaterSurveys = new List<WaterComplexSurvey>();

            foreach (WatersKey.WaterRow waterRow in this.Waters)
            {
                WaterComplexSurvey survey = new WaterComplexSurvey
                {
                    Water = waterRow,
                    PlanktonCards = PlanktonData.GetCards(waterRow),
                    BenthosCards = BenthosData.GetCards(waterRow),
                    FishCards = FishData.GetCards(waterRow)
                };

                survey.SetValues();
                WaterSurveys.Add(survey);

                survey.AnnualSurveys = new List<AnnualWaterComplexSurvey>();

                foreach (int y in survey.Dates.GetYears())
                {
                    AnnualWaterComplexSurvey annual = new AnnualWaterComplexSurvey
                    {
                        Water = waterRow,
                        Year = y,
                        PlanktonCards = survey.PlanktonCards.GetStack(y),
                        BenthosCards = survey.BenthosCards.GetStack(y),
                        FishCards = survey.FishCards.GetStack(y)
                    };

                    annual.SetValues();
                    survey.AnnualSurveys.Add(annual);
                }
            }
        }

        public bool IsSurveyed(WatersKey.WaterRow waterRow)
        {
            return this.Waters.Contains(waterRow);
        }

        public WaterComplexSurvey GetSurvey(WatersKey.WaterRow waterRow)
        {
            foreach (WaterComplexSurvey survey in WaterSurveys)
            {
                if (survey.Water == waterRow)
                {
                    return survey;
                }
            }

            return null;
        }
    }

    public class WaterComplexSurvey : ComplexSurvey
    {
        public WatersKey.WaterRow Water;

        public List<AnnualWaterComplexSurvey> AnnualSurveys;
    }

    public class AnnualWaterComplexSurvey : WaterComplexSurvey
    {
        public int Year;
    }
}
