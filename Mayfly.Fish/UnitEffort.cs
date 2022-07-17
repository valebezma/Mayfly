using System;
using System.Windows.Forms;

namespace Mayfly.Fish
{
    public class UnitEffort
    {
        public FishSamplerType GearType { set; get; }

        public EffortExpression Variant { set; get; }

        public string UnitDescription { set; get; }

        public string Unit {
            get {
                return Resources.Interface.EU.ResourceManager.GetString("Unit" +
                    Enum.GetName(typeof(EffortExpression), Variant));
            }
        }

        public double UnitCost {
            get {
                switch (Variant) {
                    case EffortExpression.Area:
                        return SquareUnitCost;

                    case EffortExpression.Volume:
                        return VolumeUnitCost;

                    case EffortExpression.Standards:
                        return GearType.GetEffortsUnitCost();

                    default: return 1;
                }
            }
        }



        public UnitEffort(EffortExpression variant) {
            Variant = variant;
        }

        public UnitEffort(FishSamplerType samplerType, EffortExpression variant) {
            GearType = samplerType;
            Variant = variant;
            UnitDescription = GetUnitDescription(GearType, Variant);
        }



        public static double SquareUnitCost { get { return 10000.0; } }

        public static double VolumeUnitCost { get { return 1000.0; } }

        public static string GetDefaultUnit(FishSamplerType samplerType) {
            return Resources.Interface.EU.ResourceManager.GetString("Unit" +
                Enum.GetName(typeof(EffortExpression), samplerType));
        }

        public static void SwitchUE(ComboBox comboBox, FishSamplerType samplerType) {
            int selectedIndex = comboBox.SelectedIndex;

            comboBox.Items.Clear();
            comboBox.Items.AddRange(samplerType.GetUnitEfforts());
            comboBox.Enabled = true;

            if (comboBox.Items.Count > selectedIndex) {
                comboBox.SelectedIndex = selectedIndex;
            } else {
                if (comboBox.Items.Count > 0) {
                    comboBox.SelectedIndex = 0;
                } else {
                    comboBox.Enabled = false;
                }
            }

            if (comboBox.SelectedIndex == -1) comboBox.SelectedIndex = comboBox.FindStringExact(samplerType.GetDefaultUnitEffort().UnitDescription);
        }

        private static string GetUnitDescription(FishSamplerType samplerType, EffortExpression variant) {
            switch (variant) {
                case EffortExpression.Standards:
                    switch (samplerType) {
                        case FishSamplerType.Hook:
                        case FishSamplerType.Trap:
                            return Resources.Interface.EU.NominalSpan;

                        case FishSamplerType.Gillnet:
                            return Resources.Interface.EU.NominalEfforts;

                        default: return null;
                    }

                case EffortExpression.Area:
                    switch (samplerType) {
                        case FishSamplerType.Dredge:
                        case FishSamplerType.Driftnet:
                        case FishSamplerType.FallingGear:
                        case FishSamplerType.LiftNet:
                        case FishSamplerType.Sein:
                        case FishSamplerType.SurroundingNet:
                        case FishSamplerType.Trawl:
                        case FishSamplerType.Electrofishing:
                            return Resources.Interface.EU.RealSquare;

                        case FishSamplerType.Gillnet:
                            return Resources.Interface.EU.NominalSquare;
                        default: return null;
                    }

                case EffortExpression.Volume:
                    switch (samplerType) {
                        case FishSamplerType.Dredge:
                        case FishSamplerType.Driftnet:
                        case FishSamplerType.FallingGear:
                        case FishSamplerType.LiftNet:
                        case FishSamplerType.Sein:
                        case FishSamplerType.SurroundingNet:
                        case FishSamplerType.Trawl:
                            return Resources.Interface.EU.RealVolume;

                        case FishSamplerType.Gillnet:
                            return Resources.Interface.EU.NominalVolume;

                        default: return null;
                    }

                default: return null;
            }
        }
    }
}
