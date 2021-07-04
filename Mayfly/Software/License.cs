using System;
using System.Collections.Generic;

namespace Mayfly.Software
{
    partial class License
    {
        partial class UserLicenseDataTable
        {
            public UserLicenseRow[] FindByFeature(string feature)
            {
                List<UserLicenseRow> result = new List<License.UserLicenseRow>();

                foreach (UserLicenseRow licRow in this.Rows)
                {
                    if (licRow.Feature == feature)
                        result.Add(licRow);
                }

                return result.ToArray();
            }
        }

        partial class UserLicenseRow
        {
            public bool IsValid
            {
                get
                {
                    return this.Expires >= DateTime.Today.AddDays(1);
                }
            }
        }
    }
}
