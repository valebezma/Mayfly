using Mayfly.Waters;
using Mayfly.Wild;
using System;
using System.Resources;
using System.Windows.Forms;

namespace Mayfly.Plankton
{
    public abstract class Service
    {
        public static Survey.SamplerRow Sampler(int id)
        {
            return ReaderSettings.SamplersIndex.Sampler.FindByID(id);
        }

        public static double GetStrate(double length)
        {
            return Math.Floor(length);
        }
    }
}
