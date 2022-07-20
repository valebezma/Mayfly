using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using static Mayfly.Fish.UserSettings;

namespace Mayfly.Fish
{
    public static class SamplerRowExtensions
    {
        public static Survey.SamplerRow[] GetPassives(this Survey samplers) {
            List<Survey.SamplerRow> result = new List<Survey.SamplerRow>();

            foreach (Survey.SamplerRow samplerRow in samplers.Sampler.Rows) {
                if (samplerRow.IsPassive()) {
                    result.Add(samplerRow);
                }
            }

            return result.ToArray();
        }


        public static FishSamplerType GetSamplerType(this Survey.SamplerRow samplerRow) {
            if (samplerRow == null) return FishSamplerType.None;
            if (samplerRow.IsTypeNull()) return FishSamplerType.None;
            return (FishSamplerType)samplerRow.Type;
        }

        public static bool IsPassive(this Survey.SamplerRow samplerRow) {
            return samplerRow.GetSamplerType().IsPassive();
        }

        public static bool IsMesh(this Survey.SamplerRow samplerRow) {
            return samplerRow.GetSamplerType().IsMesh();
        }


        public static bool IsPassive(this FishSamplerType type) {
            switch (type) {
                case FishSamplerType.Dredge:
                case FishSamplerType.Driftnet:
                case FishSamplerType.FallingGear:
                case FishSamplerType.LiftNet:
                case FishSamplerType.Sein:
                case FishSamplerType.SurroundingNet:
                case FishSamplerType.Trawl:
                case FishSamplerType.None:
                    return false;

                default:
                    return true;
            }
        }

        public static bool IsMesh(this FishSamplerType type) {
            switch (type) {
                case FishSamplerType.Dredge:
                case FishSamplerType.Driftnet:
                case FishSamplerType.FallingGear:
                case FishSamplerType.Gillnet:
                case FishSamplerType.LiftNet:
                case FishSamplerType.Sein:
                case FishSamplerType.SurroundingNet:
                case FishSamplerType.Trawl:
                    return true;

                default:
                    return false;
            }
        }

        public static double GetEffortsUnitCost(this FishSamplerType type) {
            switch (type) {
                case FishSamplerType.Gillnet:
                    return GillnetStdHeight * Math.PI * Math.Pow(GillnetStdLength / 2, 2) * (GillnetStdExposure / 24);

                default:
                    return 1;
            }
        }

        public static double GetEffortStdScore(this FishSamplerType type) {
            switch (type) {
                case FishSamplerType.Trap:
                case FishSamplerType.Hook:
                    return 24;

                case FishSamplerType.Gillnet:
                    return GillnetStdHeight * GillnetStdLength * GillnetStdExposure;

                default:
                    return 1;
            }
        }

        public static UnitEffort[] GetUnitEfforts(this FishSamplerType type) {
            List<UnitEffort> result = new List<UnitEffort>();

            foreach (EffortExpression variant in Enum.GetValues(typeof(EffortExpression))) {
                UnitEffort ue = new UnitEffort(type, variant);
                if (ue.UnitDescription != null) result.Add(ue);
            }

            return result.ToArray();
        }

        public static UnitEffort GetDefaultUnitEffort(this FishSamplerType type) {
            return new UnitEffort(type, type.GetDefaultExpression());
        }

        public static EffortExpression GetDefaultExpression(this FishSamplerType type) {
            switch (type) {
                case FishSamplerType.SurroundingNet:
                case FishSamplerType.Trawl:
                    return EffortExpression.Volume;

                case FishSamplerType.Gillnet:
                case FishSamplerType.Hook:
                case FishSamplerType.Trap:
                    return EffortExpression.Standards;

                default:
                    return EffortExpression.Area;
            }
        }

        public static string ToDisplay(this FishSamplerType type) {
            if (type == FishSamplerType.None) return string.Empty;
            return SettingsReader.SamplersIndex.SamplerType.FindByID((int)type).Name.GetLocalizedValue();
        }
    }

    public class FishSamplerTypeDisplay
    {
        public FishSamplerType Type;

        public FishSamplerTypeDisplay(FishSamplerType type) {
            Type = type;
        }

        public override string ToString() {
            return Type.ToDisplay();
        }
    }

    [Flags]
    public enum FishSamplerType
    {
        None,
        SurroundingNet,
        Sein,
        Trawl,
        Dredge,
        LiftNet,
        FallingGear,
        Gillnet,
        Driftnet,
        Trap,
        Hook,
        Electrofishing,
    }
}
