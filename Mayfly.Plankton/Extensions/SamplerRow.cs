using Mayfly.Wild;

namespace Mayfly.Plankton
{
    public static class SamplerRowExtensions
    {
        public static PlanktonSamplerType GetSamplerType(this Survey.SamplerRow samplerRow) {
            if (samplerRow == null) return PlanktonSamplerType.None;
            if (samplerRow.IsTypeNull()) return PlanktonSamplerType.None;
            return (PlanktonSamplerType)samplerRow.Type;
        }
    }

    public enum PlanktonSamplerType
    {
        None,
        Filter,
        Bathometer
    }
}
