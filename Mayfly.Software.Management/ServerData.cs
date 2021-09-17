using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.Data;

namespace Mayfly.Software.Management
{
    public abstract class ServerData
    {
        readonly static TaskDialogs.CredentialDialog dialog = new TaskDialogs.CredentialDialog()
        {
            Target = Server.Domain,
            Content = "Enter username and password of account accredited to publish updates.",
            MainInstruction = "Updates Server Sign In"
        };

        static Scheme schemeData;

        public static Scheme SchemeData
        {
            get
            {
                while (schemeData == null)
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        schemeData = ServerSoftware.GetScheme(dialog.Credentials);
                    }
                    else
                    {
                        return null;
                    }
                }

                return schemeData;
            }
        }

        public static void UpdateDatabase()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            foreach (string tableName in ServerSoftware.TablesOrder)
            {
                DataSet changes = SchemeData.GetChanges();

                if (changes == null) return;

                if (changes.Tables[tableName].Rows.Count > 0)
                {
                    adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + tableName.ToLowerInvariant(), ServerSoftware.Connection);
                    MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);

                    adapter.InsertCommand = cb.GetInsertCommand();

                    if (SchemeData.Tables[tableName].Columns["ID"] == null)
                    {
                        adapter.UpdateCommand = null;
                        adapter.DeleteCommand = null;
                    }
                    else
                    {
                        adapter.UpdateCommand = cb.GetUpdateCommand();
                        adapter.DeleteCommand = cb.GetDeleteCommand();
                    }

                    adapter.Update(SchemeData, tableName);

                }
            }

        }
    }
}
