using System.IO;
using System;
using System.Collections.Generic;
using Mayfly.Extensions;
using System.Windows.Forms;

namespace Mayfly.Wild.Exchange
{
    public partial class Permission
    {
        partial class GrantRow
        {
            public bool IsExpired
            {
                get
                {
                    return this.IsExpireNull() ? true :
                        this.Expire > DateTime.Now ? false : true;
                }
            }
        }

        public static GrantRow GetPermission(string filename, string password)
        {
            string encrypted = File.ReadAllText(filename);
            string decrypted = Mayfly.StringCipher.Decrypt(encrypted, password);
            Permission permission = new Permission();
            permission.ReadXml(new StringReader(decrypted));
            return permission.Grant[0];
        }

        public bool IsPermitted(string author, DateTime dt)
        {
            if (author == Mayfly.UserSettings.Username) return true;

            if (author == Mayfly.Resources.Interface.InvestigatorNotApproved) return true;

            foreach (Permission.GrantRow grantRow in this.Grant)
            {
                if (grantRow.Donor != author) continue;
                if (grantRow.Expire < dt) continue;

                return true;
            }

            return false;
        }

        public bool IsPermitted(string author)
        {
            return IsPermitted(author, DateTime.Today);
        }
    }
}
