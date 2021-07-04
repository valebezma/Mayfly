using System.Data;
using Mayfly.Wild;

namespace Mayfly.Fish
{
    public partial class Equipment
    {
        partial class UnitsDataTable
        {
            public UnitsRow FindDuplicate(UnitsRow unitRow)
            {
                foreach (UnitsRow _unitRow in this)
                {
                    if (unitRow.SamplerID == _unitRow.SamplerID &&
                        ((unitRow.IsMeshNull() && _unitRow.IsMeshNull()) || (unitRow.Mesh == _unitRow.Mesh)) &&
                        ((unitRow.IsHookNull() && _unitRow.IsHookNull()) || (unitRow.Hook == _unitRow.Hook)) &&
                        ((unitRow.IsLengthNull() && _unitRow.IsLengthNull()) || (unitRow.Length == _unitRow.Length)) &&
                        ((unitRow.IsHeightNull() && _unitRow.IsHeightNull()) || (unitRow.Height == _unitRow.Height)) /*&&
                        ((unitRow.IsOpeningNull() && _unitRow.IsOpeningNull()) || (unitRow.Opening == _unitRow.Opening))*/)
                        return _unitRow;
                }

                return null;
            }
        }

        partial class UnitsRow
        {
            public override string ToString()
            {
                Samplers.SamplerRow samplerRow = UserSettings.SamplersIndex.Sampler.FindByID(this.SamplerID);
                return samplerRow.Sampler + (
                    (this.IsMeshNull() ? string.Empty : this.Mesh.ToString(" ◊ 0")) +
                    (this.IsHookNull() ? string.Empty : this.Hook.ToString(" J 0")) +
                    (this.IsLengthNull() ? string.Empty : this.Length.ToString(" (0")) +
                    (this.IsHeightNull() ? ")" : this.Height.ToString(" x 0.0)")));
            }
        }
    }
}
