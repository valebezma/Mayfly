using System.Data;
using System.Drawing;
using System.Collections.Generic;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class Samplers
    {
        partial class SamplerDataTable
        {
            public bool Check(SamplerRow newGear)
            {
                foreach (SamplerRow samplerRow in this.Rows)
                {
                    if (samplerRow.ID == newGear.ID)
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool Check(DataRow newGear)
            {
                foreach (SamplerRow samplerRow in this.Rows)
                {
                    if (samplerRow.ID == (int)newGear["ID"])
                    {
                        return true;
                    }
                }
                return false;
            }

            public void Import(DataRow newGear)
            {
                Rows.Add(newGear["ID"], newGear["Sampler"]);
            }

            public SamplerRow FindBySampler(string sampler)
            {
                foreach (SamplerRow samplerRow in this.Rows)
                {
                    if (samplerRow.Sampler == sampler)
                    {
                        return samplerRow;
                    }
                }

                return null;
            }

            public SamplerRow FindByCode(string code)
            {
                foreach (SamplerRow samplerRow in this.Rows)
                {
                    if (samplerRow.ShortName == code)
                    {
                        return samplerRow;
                    }
                }

                return null;
            }
        }

        partial class SamplerRow
        {
            public string Sampler
            {
                get
                {
                    return this.IsNameNull() ? Constants.Null : this.Name.GetLocalizedValue();
                }
            }

            public string OperationDisplay
            {
                get
                {
                    return IsTypeNull() ? Constants.Null : TypeRow.Display.GetLocalizedValue();
                }
            }

            public override string ToString()
            {
                return this.Sampler;
            }
        }
    }
}
