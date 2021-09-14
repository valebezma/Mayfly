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
    public abstract class Service
    {
        public static Commit[] GetCommits(string pathRepository, string pathProject)
        {
            Repository repo = new Repository(pathRepository);
            List<Commit> result = new List<Commit>();

            foreach (var commit in repo.Commits)
            {
                foreach (var t in commit.Tree)
                {
                    if (t.Path.Contains(pathProject) && !result.Contains(commit))
                    {
                        result.Add(commit);
                    }
                }
            }

            return result.ToArray();
        }

        //public static string RandomString(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    var random = new Random();
        //    return new string(Enumerable.Repeat(chars, length)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //}

        //public static string GetFriendlyString(string sn, int groupSize, char separator)
        //{
        //    string result = string.Empty;

        //    int counter = 0;
        //    for (int i = 0; i < sn.Length; i++ )
        //    {
        //        result += sn[i];
        //        counter++;
        //        if (i < sn.Length - 1 && counter == groupSize)
        //        {
        //            result += separator;
        //            counter = 0;
        //        }
        //    }

        //    return result.ToUpperInvariant();
        //}

        //public static void CombineOldSchemes()
        //{
        //    Scheme en = new Scheme(); // Software.Service.GetScheme(Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile));
        //    Scheme ru = new Scheme(); // Software.Service.GetScheme(Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile, new CultureInfo("ru")));

        //    en.ReadXml("scheme//en.xml");
        //    ru.ReadXml("scheme//ru.xml");

        //    foreach (Scheme.FileRow fileRow in en.File)
        //    {
        //        if (fileRow.IsShortcutTipNull()) continue;
        //        if (ru.File.FindByID(fileRow.ID) == null) continue;
        //        if (ru.File.FindByID(fileRow.ID).IsShortcutTipNull()) continue;
        //        fileRow.ShortcutTip += Environment.NewLine + "ru: " + ru.File.FindByID(fileRow.ID).ShortcutTip;
        //    }

        //    foreach (Scheme.FileTypeRow filetypeRow in en.FileType)
        //    {
        //        if (filetypeRow.IsFriendlyNameNull()) continue;
        //        if (ru.FileType.FindByExtension(filetypeRow.Extension) == null) continue;
        //        if (ru.FileType.FindByExtension(filetypeRow.Extension).IsFriendlyNameNull()) continue;
        //        filetypeRow.FriendlyName += Environment.NewLine + "ru: " + ru.FileType.FindByExtension(filetypeRow.Extension).FriendlyName;
        //    }

        //    foreach (Scheme.VersionRow versionRow in en.Version)
        //    {
        //        if (versionRow.IsChangesNull()) continue;
        //        if (ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version) == null) continue;
        //        if (ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version).IsChangesNull()) continue;
        //        versionRow.Changes += Environment.NewLine + "ru: " + ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version).Changes;
        //    }

        //    en.WriteXml(Path.Combine(Application.StartupPath, "scheme", "combined.xml"));
        //}

        //public void UpdateDatabaseTable(string tableName)
        //{
        //    adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + tableName, connection);
        //    MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
        //    adapter.InsertCommand = cb.GetInsertCommand();
        //    if (SchemeData.Tables[tableName].Columns["ID"] == null)
        //    {
        //        adapter.UpdateCommand = null;
        //        adapter.DeleteCommand = null;
        //    }
        //    else
        //    {
        //        adapter.UpdateCommand = cb.GetUpdateCommand();
        //        adapter.DeleteCommand = cb.GetDeleteCommand();
        //    }
        //    adapter.Update(SchemeData.Tables[tableName]);
        //}

        //public void GetData()
        //{
        //    foreach (System.Data.DataTable dt in SchemeData.Tables)
        //    {
        //        adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + dt.TableName, connection);
        //        adapter.Fill(SchemeData, dt.TableName);
        //    }
        //}

        //public void Update()
        //{
        //    foreach (System.Data.DataTable dt in SchemeData.Tables)
        //    {
        //        adapter.Update(SchemeData, dt.TableName);
        //    }
        //}
    }
}
