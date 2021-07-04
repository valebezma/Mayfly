using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Fish
{
    public class UnitEffort
    {
        public FishSamplerType GearType { set; get; }

        public ExpressionVariant Variant { set; get; }

        public string UnitDescription { set; get; }

        public string Unit
        {
            get
            {
                return Resources.Interface.EU.ResourceManager.GetString("Unit" +
                    Enum.GetName(typeof(ExpressionVariant), Variant));
            }
        }

        public double UnitCost
        {
            get
            {
                switch (Variant)
                {
                    case ExpressionVariant.Square:
                        return SquareUnitCost;

                    case ExpressionVariant.Volume:
                        return VolumeUnitCost;

                    case ExpressionVariant.Efforts:
                        return GearType.GetEffortsUnitCost();

                    default: return 1;
                }
            }
        }



        public UnitEffort(ExpressionVariant variant)
        {
            Variant = variant;
        }

        public UnitEffort(FishSamplerType samplerType, ExpressionVariant variant)
        {
            GearType = samplerType;
            Variant = variant;
            UnitDescription = GetUnitDescription(GearType, Variant);
        }



        public static double SquareUnitCost { get { return 10000.0; } }

        public static double VolumeUnitCost { get { return 1000.0; } }

        public static string GetDefaultUnit(FishSamplerType samplerType)
        {
            return Resources.Interface.EU.ResourceManager.GetString("Unit" +
                Enum.GetName(typeof(ExpressionVariant), samplerType));
        }

        public static void SwitchUE(ComboBox comboBox, FishSamplerType samplerType)
        {
            int selectedIndex = comboBox.SelectedIndex;

            comboBox.Items.Clear();
            comboBox.Items.AddRange(samplerType.GetUnitEfforts());
            comboBox.Enabled = true;

            if (comboBox.Items.Count > selectedIndex)
            {
                comboBox.SelectedIndex = selectedIndex;
            }
            else
            {
                if (comboBox.Items.Count > 0)
                {
                    comboBox.SelectedIndex = 0;
                }
                else
                {
                    comboBox.Enabled = false;
                }
            }

            comboBox.SelectedIndex = comboBox.FindStringExact(samplerType.GetDefaultUnitEffort().UnitDescription);
        }

        private static string GetUnitDescription(FishSamplerType samplerType, ExpressionVariant variant)
        {
            switch (variant)
            {
                case ExpressionVariant.Efforts:
                    switch (samplerType)
                    {
                        case FishSamplerType.Hook:
                        case FishSamplerType.Trap:
                            return Fish.Resources.Interface.EU.NominalSpan;

                        case FishSamplerType.Gillnet:
                            return Fish.Resources.Interface.EU.NominalEfforts;

                        default: return null;
                    }

                case ExpressionVariant.Square:
                    switch (samplerType)
                    {
                        case FishSamplerType.Dredge:
                        case FishSamplerType.Driftnet:
                        case FishSamplerType.FallingGear:
                        case FishSamplerType.LiftNet:
                        case FishSamplerType.Sein:
                        case FishSamplerType.SurroundingNet:
                        case FishSamplerType.Trawl:
                        case FishSamplerType.Electrofishing:
                            return Fish.Resources.Interface.EU.RealSquare;

                        case FishSamplerType.Gillnet:
                            return Fish.Resources.Interface.EU.NominalSquare;
                        default: return null;
                    }

                case ExpressionVariant.Volume:
                    switch (samplerType)
                    {
                        case FishSamplerType.Dredge:
                        case FishSamplerType.Driftnet:
                        case FishSamplerType.FallingGear:
                        case FishSamplerType.LiftNet:
                        case FishSamplerType.Sein:
                        case FishSamplerType.SurroundingNet:
                        case FishSamplerType.Trawl:
                            return Fish.Resources.Interface.EU.RealVolume;

                        case FishSamplerType.Gillnet:
                            return Fish.Resources.Interface.EU.NominalVolume;

                        default: return null;
                    }

                default: return null;
            }
        }
    }

    public enum ExpressionVariant
    {
        Square,
        Volume,
        Efforts
    }
}
