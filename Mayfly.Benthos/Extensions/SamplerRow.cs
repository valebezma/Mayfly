using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;

namespace Mayfly.Benthos
{
    public static class SamplerRowExtensions
    {
        public static BenthosSamplerType GetSamplerType(this Samplers.SamplerRow samplerRow)
        {
            if (samplerRow == null) return BenthosSamplerType.None;
            if (samplerRow.IsTypeNull()) return BenthosSamplerType.None;
            return (BenthosSamplerType)samplerRow.Type;
        }
    }

    public enum BenthosSamplerType
    {
        None,
        Manual,
        Scraper,
        Grabber
    }
}