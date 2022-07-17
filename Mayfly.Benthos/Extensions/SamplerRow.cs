using Mayfly.Wild;

namespace Mayfly.Benthos
{
    public static class SamplerRowExtensions
    {
        public static BenthosSamplerType GetSamplerType(this Survey.SamplerRow samplerRow) {
            if (samplerRow == null) return BenthosSamplerType.None;
            if (samplerRow.IsTypeNull()) return BenthosSamplerType.None;
            return (BenthosSamplerType)samplerRow.Type;
        }
    }

    public enum BenthosSamplerType
    {
        None,
        Scraper,
        Grabber
    }
}