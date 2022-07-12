using Mayfly.Extensions;
using Mayfly.Software;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
//using Permission = Mayfly.Wild.Exchange.Permission;

using OfficeOpenXml;
using Mayfly.Wild;
using Mayfly.Fish.Explorer;

namespace Mayfly.ManualLicenser
{
    class Program
    {
        static void Main(string[] args)
        {
        //Console.WriteLine("Continue to grant license to {0}.", UserSettings.Username);
        Enter:
            Console.Write("Enter admin pass: ");
            string pass = string.Empty;

            ConsoleKey i = Console.ReadKey(true).Key;
            Console.Write("*");
            while (i != ConsoleKey.Enter)
            {
                pass += i;
                i = Console.ReadKey(true).Key;
                Console.Write("*");
            }

            if (pass != "SHAKETHATASS")
            {
                Console.WriteLine();
                Console.WriteLine("Wrong password");
                Log.Write(EventType.Maintenance, "User tried to add license manually but typed wrong password: {0}", pass);
                goto Enter;
                //return;
            }
            else
            {
                Console.Write("*********");
                Console.Write("\r\nHello, Valentine. Let's do things.");
            }



        Start:

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===== SELECT PROCEDURE =====");
            Console.WriteLine();
            Console.WriteLine("Available procedures are:");
            Console.WriteLine("\t1 - Install license");
            Console.WriteLine("\t2 - Reset investigator signs");
            //Console.WriteLine("\t3 - Get author permission");
            Console.WriteLine("\t4 - Convert .msps to .bio");
            Console.WriteLine("\t5 - Hack .bio reference");
            Console.WriteLine("\t6 - Convert stat sheet to .fcd cards");
            Console.WriteLine("\t7 - Reset tons to kg in .fcd cards");
            Console.WriteLine("\t8 - Rearrange fishing stats .fcd cards");
            Console.WriteLine("\t9 - Convert excel workbook to plankton cards (.pcd)");
            Console.WriteLine();
            Console.Write("Type procedure number to continue: ");

            string feature = Console.ReadLine();

            switch (feature)
            {
                //case "1":
                //    InstallLicense();
                //    break;

                case "2":
                    ResetSign();
                    break;

                //case "3":
                //    GetPermission();
                //    break;

                case "4":
                    CovertBio();
                    break;

                //case "5":
                //    HackBio();
                //    break;

                case "6":
                    //ConvertFishStats();
                    break;

                case "7":
                    ResetMass();
                    break;

                case "8":
                    RearrangeStatCards();
                    break;

                case "9":
                    ConvertPlankton();
                    break;
            }

            goto Start;
        }

        static void ResetSign()
        {
            Console.WriteLine();
            Console.WriteLine("===== SIGN RESET =====");

        Start:

            Console.WriteLine();
            Console.Write("Enter cards path: ");
            string source = Console.ReadLine().Trim(new char[] { '"' });

            //Console.Write("Enter path to save resigned cards: ");
            //string destination = Console.ReadLine();

            //Console.Write("Enter file extension to update signs in (.bcd, .fcd): .");
            //string ext = "." + Console.ReadLine();

            Console.Write("Enter new author sign: ");
            string sign = Console.ReadLine();

            string[] filenames = Directory.GetFiles(source, "*.*", System.IO.SearchOption.AllDirectories);

            Console.Write("\r\nSign will be updated in {0} data cards themselves. New one is \"{1}\". Continue (y)? ", filenames.Length, sign);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine("Sign resetting started:");
                Console.WriteLine();

                foreach (string filename in filenames)
                {
                    Wild.Survey data = new Wild.Survey();
                    data.Read(filename);
                    if (data.Solitary.IsWhenNull()) data.Solitary.When = new DateTime(2000, 01, 01);
                    data.Solitary.Sign = StringCipher.Encrypt(sign, data.Solitary.When.ToString("s"));
                    Console.WriteLine("Saving " + filename.Substring(source.Length) + " with sign resetted.");
                    data.WriteToFile(filename);
                }
            }

            //Console.WriteLine();
            //Console.WriteLine();

            Console.Write("\r\nDone. Exit procedure (y)? ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return;
            }

            goto Start;
        }

        //static void GetPermission()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("===== GET PERMISSION =====");

        //Start:
        //    Console.WriteLine();
        //    Console.Write("Type donor name: ");
        //    string donor = Console.ReadLine();

        //Date:
        //    Console.Write("Type expiration date: ");
        //    string date = Console.ReadLine();

        //    DateTime exp = DateTime.Today;
        //    try
        //    {
        //        exp = Convert.ToDateTime(date);
        //        //Console.WriteLine("Expairation date will be set on {0:G}", exp);
        //    }
        //    catch
        //    {
        //        Console.WriteLine("\r\nCan't recognize date from '{0}'. Retry.", date);
        //        goto Date;
        //    }

        //    string name = "man";
        //    string source = "qwertyuiopasdfghjklzxcvbnm";
        //    Random r = new Random();

        //    while (name.Length < 15)
        //    {
        //        //serial += Console.ReadKey().KeyChar;
        //        name += source[r.Next(source.Length)];
        //    }

        //    Console.Write("\r\nParameters of permission are:\r\n\tDonor: {0}\r\n\tExpiration date: {1:G}\r\n\r\nConfirm paramaters and select permission desination:\r\n    F - file\r\n    R - registry\r\n\r\nDestination: ",
        //        donor, exp);

        //    switch (Console.ReadKey().Key)
        //    {
        //        case ConsoleKey.F:

        //            Console.WriteLine();
        //            Console.Write("Type recepient name: ");
        //            string recepient = Console.ReadLine();

        //            Permission permission = new Permission();
        //            permission.Grant.AddGrantRow(donor, recepient, exp);
        //            string data = permission.GetXml();
        //            string encryptedData = StringCipher.Encrypt(data, "1111");
        //            File.WriteAllText(name + ".perm", encryptedData);

        //            break;
        //        case ConsoleKey.R:

        //            UserSetting.SetValue(UserSettingPaths.KeyGeneral, new string[] { "Permissions", name },
        //                "Donor", StringCipher.Encrypt(donor, name));
        //            UserSetting.SetValue(UserSettingPaths.KeyGeneral, new string[] { "Permissions", name },
        //                "Expire", StringCipher.Encrypt(exp.ToString("s", CultureInfo.InvariantCulture), name));

        //            Console.WriteLine("\r\nPermission manually installed.");
        //            Log.Write(EventType.Maintenance, "Permission from {0} manually installed with name {1}.", donor, name);
        //            break;
        //    }

        //    Console.Write("\r\nDone. Exit procedure (y)? ");

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {
        //        return;
        //    }

        //    goto Start;
        //}

        static void CovertBio()
        {
            Console.WriteLine();
            Console.WriteLine("===== BIO CONVERT =====");

        Start:

            Console.WriteLine();
            Console.Write("Enter .msps file path: ");
            string source = Console.ReadLine().Trim(new char[] { '"' });

            if (Path.GetExtension(source) != ".msps")
            {
                Console.WriteLine("Could not recognize format.");
                goto Start;
            }

            string newfile = source.Replace(".msps", ".bio");

            Console.Write("\r\n\"{0}\" will be converted to \"{1}\". Continue (y)? ", source, newfile);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Wild.Survey bio = new Wild.Survey();
                string contents = StringCipher.Decrypt(File.ReadAllText(source), "BIOREFERENCE");
                bio.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
                File.WriteAllText(newfile, StringCipher.Encrypt(bio.GetXml(), "bio"));
            }

            //Console.WriteLine();
            //Console.WriteLine();

            Console.Write("\r\nDone. Exit procedure (y)? ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return;
            }

            goto Start;
        }

        //static void HackBio()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("===== HACK BIO =====");

        //Start:

        //    Console.WriteLine();
        //    Console.Write("Enter .bio file path: ");
        //    string source = Console.ReadLine().Trim(new char[] { '"' });

        //    if (Path.GetExtension(source) != ".bio")
        //    {
        //        Console.WriteLine("Could not recognize format.");
        //        goto Start;
        //    }

        //    string destination = source.Replace(".bio", string.Empty);

        //    Console.Write("\r\n\"{0}\" will be disassemled to \"{1}\". Continue (y)? ", source, destination);

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {
        //        Console.Write("Enter extensions for future cards: ");
        //        string ext = Console.ReadLine().Trim(new char[] { '"' });

        //        Data bio = new Data();
        //        string contents = StringCipher.Decrypt(File.ReadAllText(source), "bio");
        //        bio.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));

        //        Directory.CreateDirectory(destination);

        //        foreach (Data.CardRow cardRow in bio.Card)
        //        {
        //            Data data = cardRow.SingleCardDataset();
        //            string filename = IO.SuggestName(destination, cardRow.GetSuggestedName(ext));
        //            data.WriteToFile(Path.Combine(destination, filename));

        //            Console.Write("\r\nCard {0} is written", filename);
        //        }
        //    }

        //    //Console.WriteLine();
        //    //Console.WriteLine();

        //    Console.Write("\r\nDone. Exit procedure (y)? ");

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {
        //        return;
        //    }

        //    goto Start;
        //}

        //static void ConvertFishStats()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("===== FISH STAT CONVERT =====");

        //Start:

        //    Console.WriteLine();
        //    Console.Write("Enter .csv file path: ");
        //    string source = Console.ReadLine().Trim(new char[] { '"' });

        //    if (Path.GetExtension(source) != ".csv")
        //    {
        //        Console.WriteLine("Could not recognize format.");
        //        goto Start;
        //    }

        //    string destination = source.Replace(".csv", "");
        //    System.IO.Directory.CreateDirectory(destination);

        //    Console.WriteLine();
        //    Console.Write("Enter water type (1 - stream, 2 - lake, 3 - res.): ");
        //    int watertype = Convert.ToInt32(Console.ReadLine().Trim(new char[] { '"' }));

        //    //Console.WriteLine();
        //    Console.Write("Enter water name (e. g. \"Камское\"): ");
        //    string watername = Console.ReadLine().Trim(new char[] { '"' });

        //    //Console.WriteLine();
        //    Console.Write("Enter year: ");
        //    int year = Convert.ToInt32(Console.ReadLine().Trim(new char[] { '"' }));


        //    Console.Write("\r\n\"{0}\" will be converted to fish cards and placed to {1}. Continue (y)? ", source, destination);

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {


        //        string dataString = new StreamReader(source).ReadToEnd();
        //        DataTable dt = new DataTable();
        //        string[] lines = dataString.Split('\n');
        //        string[] headers = lines[0].Trim('\r').Split(';');

        //        foreach (string header in headers)
        //        {
        //            dt.Columns.Add(header);
        //        }

        //        for (int i = 1; i < lines.Length; i++)
        //        {
        //            DataRow headRow = dt.NewRow();
        //            string[] pastedCells = lines[i].Trim('\r').Split(';');
        //            headRow.ItemArray = pastedCells;
        //            dt.Rows.Add(headRow);
        //        }

        //        Console.WriteLine("There are {0} rows.", dt.Rows.Count);


        //        List<Spc> pairs = new List<Spc>
        //        {
        //            new Spc("лещ", "Abramis brama"),
        //            new Spc("щука", "Esox lucius"),
        //            new Spc("судак", "Stizostedion lucioperca"),
        //            new Spc("налим", "Lota lota"),
        //            new Spc("жерех", "Aspus aspius"),
        //            new Spc("язь", "Leuciscus idus"),
        //            new Spc("плотва", "Rutilua rutilus"),
        //            new Spc("чехонь", "Pelecus cultratus"),
        //            new Spc("синец", "Abramis ballerus"),
        //            new Spc("густера", "Blicca bjoerkna"),
        //            new Spc("окунь", "Perca fluviatilis"),
        //            new Spc("уклея", "Alburnus alburnus"),
        //            new Spc("тюлька", "Clupeonella cultriventris caspia"),
        //            new Spc("карась", "Carassius auratus"),
        //            new Spc("голавль", "Leuciscus cephalus")
        //        };


        //        string subject = string.Empty;
        //        int area = 0;

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //        Row:

        //            if (!string.IsNullOrWhiteSpace(dr[0].ToString()))
        //            {
        //                try
        //                {
        //                    string subj = dr[0].ToString();
        //                    Console.Write("Check subject name: ");
        //                    SendKeys.SendWait(subj);
        //                    subject = Console.ReadLine();

        //                    Console.Write("Enter area, ha: ");
        //                    if (subj.Contains("га"))
        //                    {
        //                        int haind = subj.IndexOf("га");
        //                        string before = subj.Substring(0, haind).Trim();
        //                        int numind = before.LastIndexOf(" ");
        //                        string areapart = before.Substring(numind).Trim();
        //                        SendKeys.SendWait(areapart);
        //                    }
        //                    else
        //                    {
        //                        SendKeys.SendWait(subj);
        //                    }

        //                    area = Convert.ToInt32(Console.ReadLine());
        //                }
        //                catch { goto Row; }
        //            }

        //            Data data = new Data();

        //            Data.CardRow cr = data.Card.NewCardRow();
        //            cr.WaterRow = data.Water.AddWaterRow(watertype, watername);

        //            try {
        //                cr.When = new DateTime(year, Convert.ToDateTime("01." + dr["месяц"] + ".01").Month, 15);
        //            } catch { continue; }

        //            cr.Sign = StringCipher.Encrypt(subject, cr.When.ToString("s"));
        //            //cr.Investigator = Mayfly.UserSettings.Username;
        //            //cr.Sampler = 710;
        //            cr.ExactArea = 10000d * area;
        //            cr.Comments = string.Format("According to {0}", source);
        //            data.Card.AddCardRow(cr);

        //            foreach (Spc spc in pairs)
        //            {
        //                if (!dt.Columns.Contains(spc.rus)) continue;

        //                if (!string.IsNullOrWhiteSpace(dr[spc.rus].ToString()))
        //                {
        //                    Data.DefinitionRow spcRow = data.Species.AddSpeciesRow(spc.lat);
        //                    Data.LogRow lr = data.Log.NewLogRow();
        //                    lr.CardRow = cr;
        //                    lr.SpeciesRow = spcRow;
        //                    lr.Mass = 1000 * Convert.ToDouble(dr[spc.rus]);

        //                    data.Log.AddLogRow(lr);
        //                }
        //            }

        //            data.WriteToFile(Path.Combine(destination, string.Format("{0} {1:yyyy-MM} {2}.fcd",
        //                subject, data.Solitary.When, watername)));
        //        }
        //    }

        //    //Console.WriteLine();
        //    //Console.WriteLine();

        //    Console.Write("\r\nDone. Exit procedure (y)? ");

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {
        //        return;
        //    }

        //    goto Start;
        //}

        static void RearrangeStatCards()
        {
            Console.WriteLine();
            Console.WriteLine("===== FISH STAT ARRANGE =====");

        Start:

            Console.WriteLine();
            Console.Write("Enter source path: ");
            string source = Console.ReadLine().Trim(new char[] { '"' });

            string destination = source + " (Rearranged)";
            Directory.CreateDirectory(destination);

            Wild.Survey data = new Wild.Survey();

            foreach (string file in Directory.GetFiles(source, "*.fcd", SearchOption.AllDirectories))
            {
                Wild.Survey data1 = new Wild.Survey();
                data1.Read(file);
                data1.CopyTo(data);
            }

            foreach (Wild.Survey.WaterRow waterRow in data.Water)
            {
                string waterpath = Path.Combine(destination, waterRow.Water);
                Directory.CreateDirectory(waterpath);
                Console.WriteLine("Folder for water {0} created.", waterRow.Water);

                CardStack w_stack = data.GetStack().GetStack("Water", waterRow.Water);

                foreach (int y in w_stack.GetYears())
                {
                    string yearpath = Path.Combine(waterpath, y.ToString());
                    Directory.CreateDirectory(yearpath);
                    Console.WriteLine("Folder for year {0} created.", y);

                    CardStack y_stack = w_stack.GetStack(y);

                    foreach (string invest in y_stack.GetInvestigators())
                    {
                        string investpath = Path.Combine(yearpath, invest);
                        Directory.CreateDirectory(investpath);
                        Console.WriteLine("Folder for subject {0} created.", invest);

                        foreach (Wild.Survey.CardRow cardRow in y_stack)
                        {
                            if (cardRow.Investigator != invest) continue;

                            cardRow.SingleCardDataset().WriteToFile(Path.Combine(investpath,
                                string.Format("{0:yyyy-MM} {1} {2}.fcd", cardRow.When, cardRow.WaterRow.Presentation, invest)));
                        }
                    }
                }
            }

            Console.Write("\r\nDone. Exit procedure (y)? ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return;
            }

            goto Start;
        }

        //static void ConvertPlankton1()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("===== CONVERT PLANKTON =====");

        //Start:

        //    Console.WriteLine();
        //    Console.Write("Enter .xlsx file path: ");
        //    string source = Console.ReadLine().Trim(new char[] { '"' });

        //    if (Path.GetExtension(source) != ".xlsx")
        //    {
        //        Console.WriteLine("Could not recognize format.");
        //        goto Start;
        //    }

        //    string destination = source.Replace(".xlsx", string.Empty);

        //    Console.Write("\r\n\"{0}\" will be converted to path \"{1}\". Continue (y)? ", source, destination);

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {
        //        Directory.CreateDirectory(destination);

        //        FileInfo existingFile = new FileInfo(source);

        //        using (ExcelPackage package = new ExcelPackage(existingFile))
        //        {
        //            Console.Write("\r\nIs LOMO microscope used (y)? ");

        //            bool lomo = (Console.ReadKey().Key == ConsoleKey.Y);

        //            Console.WriteLine();
        //            Console.WriteLine("\r\nAvailable worksheets are:");

        //            foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
        //            {
        //                Console.WriteLine("\t" + sheet.Name);
        //            }

        //            Console.Write("\r\nConvert all worksheets (y)? ");

        //            if (Console.ReadKey().Key == ConsoleKey.Y)
        //            {
        //                foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
        //                {
        //                    try
        //                    {
        //                        ConvertPlankton(sheet, destination, lomo);
        //                    }
        //                    catch
        //                    {
        //                        Console.WriteLine("Error executing conversion sheet {0}.", sheet.Name);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Console.Write("\r\nConvert worksheets serially (y)? ");

        //                if (Console.ReadKey().Key == ConsoleKey.Y)
        //                {
        //                    foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
        //                    {
        //                        Console.Write("\r\nConvert worksheet {0} (y)? ", sheet.Name);

        //                        if (Console.ReadKey().Key == ConsoleKey.Y)
        //                        {
        //                            ConvertPlankton(sheet, destination, lomo);
        //                        }
        //                    }
        //                }
        //                else
        //                {

        //                SelectSheet:

        //                    Console.WriteLine();
        //                    Console.Write("Type sheet name to convert it to .pcd file: ");

        //                    string feature = Console.ReadLine();

        //                    ConvertPlankton(package.Workbook.Worksheets[feature], destination, lomo);

        //                    Console.Write("\r\nDone. Convert another sheet (y)? ");

        //                    if (Console.ReadKey().Key == ConsoleKey.Y)
        //                    {
        //                        goto SelectSheet;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    Console.Write("\r\nDone. Exit procedure (y)? ");

        //    if (Console.ReadKey().Key == ConsoleKey.Y)
        //    {
        //        return;
        //    }

        //    goto Start;
        //}

        //static void ConvertPlankton1(ExcelWorksheet sheet, string destination, bool lomo)
        //{
        //    bool present = false;

        //    for (int row = 1; row < 320; row++)
        //    {
        //        for (int col = 40; col < 65; col++)
        //        {
        //            if (sheet.Cells[row + 1, col + 1].Value != null && sheet.Cells[row + 1, col + 1].Value.ToString() == "мм")
        //            // We parked at species size block
        //            {
        //                //int lines = -1;
        //                //bool formula = true;

        //                //for (int l = row + 2; formula; l++)
        //                //{
        //                //    formula = !string.IsNullOrEmpty(sheet.Cells[l, col + 1].Formula);
        //                //    lines++;
        //                //}

        //                //int Qty = Convert.ToInt32(sheet.Cells[row + 2 + lines, col + 2].Value);

        //                //if (Qty > 0)
        //                //{
        //                present = true;
        //                break;
        //                //}
        //            }
        //        }
        //    }

        //    if (present)
        //    {
        //        Plankton.Data data = new Plankton.Data();

        //        data.Solitary.Label = Convert.ToString(sheet.Cells[2, 2].Value);
        //        data.Solitary.When = (sheet.Cells[9, 2].Value is DateTime) ? Convert.ToDateTime(sheet.Cells[9, 2].Value) :
        //            DateTime.FromOADate(Convert.ToDouble(sheet.Cells[9, 2].Value));
        //        data.Solitary.When = data.Solitary.When.AddHours(12);

        //        string l1 = (string)sheet.Cells[1, 2].Value;
        //        if (!string.IsNullOrWhiteSpace(l1)) data.Solitary.Comments = l1;

        //        string l2 = (string)sheet.Cells[2, 4].Value;
        //        if (!string.IsNullOrWhiteSpace(l2)) data.Solitary.Comments = l2;

        //        data.Solitary.WaterRow = data.Water.AddWaterRow(3, ((string)sheet.Cells[3, 2].Value).TrimEnd("вдхр".ToCharArray()).Trim());

        //        if (sheet.Cells[8, 2].Value != null)
        //        {
        //            data.Solitary.Volume = Convert.ToDouble(sheet.Cells[8, 2].Value);

        //            if (sheet.Cells[8, 2].Formula.Contains("0.125^2"))
        //            {
        //                data.Solitary.Sampler = 1;
        //            }
        //            else if (sheet.Cells[8, 2].Formula.Contains("0.06^2"))
        //            {
        //                data.Solitary.Sampler = 5;
        //            }
        //            else
        //            {
        //                data.Solitary.Sampler = 6;
        //            }
        //        }

        //        if (sheet.Cells[5, 2].Value != null) data.Solitary.Depth = Convert.ToDouble(sheet.Cells[5, 2].Value);

        //        if (sheet.Cells[6, 2].Value != null) data.Solitary.StateOfWater.FlowRate = Convert.ToDouble(sheet.Cells[6, 2].Value);

        //        if (sheet.Cells[4, 2].Value != null)
        //        {
        //            string sectside = Convert.ToString(sheet.Cells[4, 2].Value);

        //            switch (sectside)
        //            {
        //                case "левый":
        //                    data.Solitary.CrossSection = 0;
        //                    data.Solitary.Bank = 0;
        //                    break;

        //                case "правый":
        //                    data.Solitary.CrossSection = 0;
        //                    data.Solitary.Bank = 1;
        //                    break;

        //                case "центр":
        //                case "русло":
        //                    data.Solitary.CrossSection = 4;
        //                    break;
        //            }
        //        }
        //        Console.WriteLine("\r\nData recognized: \r\nWater: {0}\r\nLabel: {1}\r\nDate: {2}\r\nSampler: {3}\r\nVolume: {4} l\r\n",
        //            data.Solitary.WaterRow.Presentation,
        //            data.Solitary.Label,
        //            data.Solitary.When,
        //            data.Solitary.SamplerRow.Name,
        //            data.Solitary.Volume * 1000);


        //        Console.WriteLine("{0,-30}{1,5}{2,10}\r\n", "Species", "Qty", "Subsample");

        //        for (int row = 1; row < 320; row++)
        //        {
        //            for (int col = 40; col < 65; col++)
        //            {
        //                if (sheet.Cells[row + 1, col + 1].Value != null && sheet.Cells[row + 1, col + 1].Value.ToString() == "мм")
        //                // We parked at species size block
        //                {
        //                    // Initiate block
        //                    string Species = sheet.Cells[row, col].Value.ToString();

        //                    while (Species.Contains("  "))
        //                    {
        //                        Species = Species.Replace("  ", " ");
        //                    }

        //                    if (Species.Contains("("))
        //                    {
        //                        Species = Species.Substring(0, Species.IndexOf('('));
        //                    }

        //                    Species = Species.Trim();

        //                    int lines = -1;
        //                    bool formula = true;

        //                    for (int l = row + 2; formula; l++)
        //                    {
        //                        formula = !string.IsNullOrEmpty(sheet.Cells[l, col + 1].Formula);
        //                        lines++;
        //                    }

        //                    int Qty = Convert.ToInt32(sheet.Cells[row + 2 + lines, col + 2].Value);

        //                    if (Qty > 0)
        //                    {
        //                        // Initiate species and log record

        //                        Plankton.Data.DefinitionRow spcRow = data.Species.AddSpeciesRow(Species);

        //                        Plankton.Data.LogRow logRow = data.Log.NewLogRow();
        //                        logRow.CardRow = data.Solitary;
        //                        logRow.DefinitionRow = spcRow;
        //                        logRow.Quantity = Qty;

        //                        // Set dilution

        //                        for (int r = 16; r <= 274; r++)
        //                        {
        //                            if (sheet.Cells[r, 1].Value != null && sheet.Cells[r, 1].Value.ToString().Contains(Species))
        //                            {
        //                                double dilution = Convert.ToDouble(sheet.Cells[r, 2].Value);
        //                                if (dilution > 0 && dilution < 1) logRow.Subsample = dilution;
        //                                break;
        //                            }
        //                        }

        //                        data.Log.AddLogRow(logRow);

        //                        Console.WriteLine("{0,-30}{1,5}{2,10:N3}",
        //                            spcRow.Species,
        //                            logRow.Quantity,
        //                            (logRow.IsSubsampleNull() ? 1 : logRow.Subsample));

        //                        // Place individual measurements

        //                        double convert = 1; // inverse size units in 1 mm
        //                        if (sheet.Cells[row + 1, col].Value.ToString() == "х4") convert = lomo ? convert = (1d / 40d) : (1d / 61d);
        //                        else if (sheet.Cells[row + 1, col].Value.ToString() == "х0,8") convert = 1d / 12d;

        //                        for (int r = row + 2; r <= row + 2 + lines; r++)
        //                        {
        //                            int size = Convert.ToInt32(sheet.Cells[r, col].Value);
        //                            if (size == 0) continue;

        //                            int q = Convert.ToInt32(sheet.Cells[r, col + 2].Value);
        //                            if (q == 0) continue;

        //                            Plankton.Data.IndividualRow indRow = data.Individual.NewIndividualRow();

        //                            if (q > 1) indRow.Frequency = q;
        //                            indRow.Length = Math.Round(size * convert, 3);
        //                            indRow.LogRow = logRow;

        //                            data.Individual.AddIndividualRow(indRow);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (data.Solitary.Quantity > 0)
        //        {
        //            string filepath = Path.Combine(destination, data.SuggestedName);
        //            data.WriteToFile(filepath);
        //            Console.WriteLine("\r\nCard saved to {0}", filepath);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("\r\nCard is not found on worksheet {0}", sheet.Name);
        //    }
        //}

        static void ConvertPlankton()
        {
            Console.WriteLine();
            Console.WriteLine("===== CONVERT PLANKTON =====");

        Start:

            Console.WriteLine();
            Console.Write("Enter path: ");
            string source = Console.ReadLine().Trim(new char[] { '"' });
            string destination = source.TrimEnd('\\') + " (Converted)";

            Console.Write("\r\nAll Excel workbooks from \"{0}\" will be converted to plankton cards and stored to \"{1}\". Continue (y)? ", source, destination);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Directory.CreateDirectory(destination);

                string[] filenames = Directory.GetFiles(source, "*.xlsx");
                Console.Write("\r\nIs LOMO microscope used (y)? ");
                bool lomo = (Console.ReadKey().Key == ConsoleKey.Y);

                foreach (string filename in filenames)
                {
                    FileInfo existingFile = new FileInfo(filename);

                    try
                    {
                        ExcelPackage package = new ExcelPackage(existingFile);

                        foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
                        {
                            Wild.Survey data = convertPlankton(sheet, destination, lomo);

                            if (data != null && data.Solitary.Quantity > 0)
                            {
                                string filepath = Path.Combine(destination, 
                                    data.Solitary.When.Year.ToString(), 
                                    data.Solitary.WaterRow.Presentation, 
                                    data.Solitary.GetSuggestedName()
                                    );
                                Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                                data.WriteToFile(filepath);
                                Console.WriteLine("\r\nCard saved to {0}", filepath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            Console.Write("\r\nDone. Exit procedure (y)? ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return;
            }

            goto Start;
        }

        static Wild.Survey convertPlankton(ExcelWorksheet sheet, string destination, bool lomo)
        {
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            bool present = false;

            for (int row = 1; row < 320; row++)
            {
                for (int col = 40; col < 65; col++)
                {
                    if (sheet.Cells[row + 1, col + 1].Value != null && sheet.Cells[row + 1, col + 1].Value.ToString() == "мм")
                    // We parked at species size block
                    {
                        int lines = -1;
                        bool formula = true;

                        for (int l = row + 2; formula; l++)
                        {
                            formula = !string.IsNullOrEmpty(sheet.Cells[l, col + 1].Formula);
                            lines++;
                        }

                        // In case there is proportion calculation line in a block
                        if (sheet.Cells[row + 3 + lines, col + 2].Value != null || sheet.Cells[row + 3 + lines, col + 3].Value != null)
                        {
                            lines++;
                        }

                        int Qty = Convert.ToInt32(sheet.Cells[row + 2 + lines, col + 2].Value);

                        if (Qty > 0)
                        {
                            present = true;
                            break;
                        }
                    }
                }
            }

            if (present)
            {
                Wild.Survey data = new Wild.Survey();

                data.Solitary.Label = Convert.ToString(sheet.Cells[2, 2].Value);
                data.Solitary.When = (sheet.Cells[9, 2].Value is DateTime) ? Convert.ToDateTime(sheet.Cells[9, 2].Value) :
                    DateTime.FromOADate(Convert.ToDouble(sheet.Cells[9, 2].Value));
                data.Solitary.When = data.Solitary.When.AddHours(12);

                string l1 = (string)sheet.Cells[1, 2].Value;
                if (!string.IsNullOrWhiteSpace(l1)) data.Solitary.Comments = l1;

                string l2 = (string)sheet.Cells[2, 4].Value;
                if (!string.IsNullOrWhiteSpace(l2)) data.Solitary.Comments = l2;

                data.Solitary.WaterRow = data.Water.AddWaterRow(3, ((string)sheet.Cells[3, 2].Value).TrimEnd("вдхр".ToCharArray()).Trim());

                if (sheet.Cells[8, 2].Value != null)
                {
                    data.Solitary.Volume = Convert.ToDouble(sheet.Cells[8, 2].Value);

                    if (sheet.Cells[8, 2].Formula.Contains("0.125^2"))
                    {
                        data.Solitary.Sampler = 1;
                    }
                    else if (sheet.Cells[8, 2].Formula.Contains("0.06^2"))
                    {
                        data.Solitary.Sampler = 5;
                    }
                    else
                    {
                        data.Solitary.Sampler = 6;
                    }
                }

                if (sheet.Cells[5, 2].Value != null) data.Solitary.Depth = Convert.ToDouble(sheet.Cells[5, 2].Value);

                if (sheet.Cells[6, 2].Value != null) data.Solitary.StateOfWater.FlowRate = Convert.ToDouble(sheet.Cells[6, 2].Value);

                if (sheet.Cells[4, 2].Value != null)
                {
                    string sectside = Convert.ToString(sheet.Cells[4, 2].Value);

                    switch (sectside)
                    {
                        case "левый":
                            data.Solitary.CrossSection = 0;
                            data.Solitary.Bank = 0;
                            break;

                        case "правый":
                            data.Solitary.CrossSection = 0;
                            data.Solitary.Bank = 1;
                            break;

                        case "центр":
                        case "русло":
                            data.Solitary.CrossSection = 4;
                            break;
                    }
                }
                Console.WriteLine("\r\nData recognized: \r\nWater: {0}\r\nLabel: {1}\r\nDate: {2}\r\nSampler: {3}\r\nVolume: {4} l\r\n",
                    data.Solitary.WaterRow.Presentation,
                    data.Solitary.Label,
                    data.Solitary.When,
                    data.Solitary.SamplerShortPresentation,
                    data.Solitary.Volume * 1000);


                Console.WriteLine("{0,-30}{1,5}{2,10}\r\n", "Species", "Qty", "Subsample");

                for (int row = 1; row < 320; row++)
                {
                    for (int col = 40; col < 65; col++)
                    {
                        if (sheet.Cells[row + 1, col + 1].Value != null && sheet.Cells[row + 1, col + 1].Value.ToString() == "мм")
                        // We parked at species size block
                        {
                            // Initiate block
                            string Species = sheet.Cells[row, col].Value.ToString();

                            while (Species.Contains("  "))
                            {
                                Species = Species.Replace("  ", " ");
                            }

                            if (Species.Contains("("))
                            {
                                Species = Species.Substring(0, Species.IndexOf('('));
                            }

                            Species = Species.Trim();

                            int lines = -1;
                            bool formula = true;

                            for (int l = row + 2; formula; l++)
                            {
                                formula = !string.IsNullOrEmpty(sheet.Cells[l, col + 1].Formula); // Is formula typed in "мм" column
                                lines++;
                            }

                            // In case there is proportion calculation line in a block
                            if (sheet.Cells[row + 3 + lines, col + 2].Value != null || sheet.Cells[row + 3 + lines, col + 3].Value != null)
                            {
                                lines++;
                            }

                            int Qty = Convert.ToInt32(sheet.Cells[row + 2 + lines, col + 2].Value);

                            if (Qty > 0)
                            {
                                // Initiate species and log record

                                Wild.Survey.DefinitionRow spcRow = data.Definition.AddDefinitionRow(91, Species);

                                Wild.Survey.LogRow logRow = data.Log.NewLogRow();
                                logRow.CardRow = data.Solitary;
                                logRow.DefinitionRow = spcRow;
                                logRow.Quantity = Qty;

                                // Set dilution

                                for (int r = 16; r <= 274; r++)
                                {
                                    if (sheet.Cells[r, 1].Value != null && sheet.Cells[r, 1].Value.ToString().Contains(Species))
                                    {
                                        double dilution = Convert.ToDouble(sheet.Cells[r, 2].Value);
                                        if (dilution > 0 && dilution < 1) logRow.Subsample = dilution;
                                        break;
                                    }
                                }

                                data.Log.AddLogRow(logRow);

                                Console.WriteLine("{0,-30}{1,5}{2,10:N3}",
                                    spcRow.Taxon,
                                    logRow.Quantity,
                                    (logRow.IsSubsampleNull() ? 1 : logRow.Subsample));

                                // Place individual measurements

                                double convert = (sheet.Cells[row + 1, col].Value.ToString() == "х4") ? (lomo ? (1d / 40d) : (1d / 61d)) : 
                                    ((sheet.Cells[row + 1, col].Value.ToString() == "х0,8") ? 1d / 12d : 1);

                                for (int r = row + 2; r <= row + 2 + lines; r++)
                                {
                                    int size = Convert.ToInt32(sheet.Cells[r, col].Value);
                                    if (size == 0) continue;

                                    int q = Convert.ToInt32(sheet.Cells[r, col + 2].Value);
                                    if (q == 0) continue;

                                    Wild.Survey.IndividualRow indRow = data.Individual.NewIndividualRow();

                                    if (q > 1) indRow.Frequency = q;
                                    indRow.Length = Math.Round(size * convert, 3);
                                    indRow.LogRow = logRow;

                                    data.Individual.AddIndividualRow(indRow);
                                }
                            }
                        }
                    }
                }

                return data;
            }
            else
            {
                Console.WriteLine("\r\nCard is not found on worksheet \"{0}\"", sheet.Name);
                return null;
            }
        }

        public class Spc
        {
            public string rus;
            public string lat;

            public Spc(string s, string l)
            {
                rus = s;
                lat = l;
            }
        }

        static void ResetMass()
        {
            Console.WriteLine();
            Console.WriteLine("===== MASS RESET =====");

        Start:

            Console.WriteLine();
            Console.Write("Enter cards path: ");
            string source = Console.ReadLine().Trim(new char[] { '"' });

            string[] filenames = Directory.GetFiles(source, "*.*", System.IO.SearchOption.AllDirectories);

            Console.Write("\r\nMass will be updated in {0} data cards. Continue (y)? ", filenames.Length);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.WriteLine("Mass resetting started:");
                Console.WriteLine();

                foreach (string filename in filenames)
                {
                    Wild.Survey fdata = new Wild.Survey();
                    fdata.Read(filename);
                    foreach (Wild.Survey.LogRow logrow in fdata.Log)
                    {
                        logrow.Mass *= 1000;
                    }

                    Console.WriteLine("Saving " + filename.Substring(source.Length) + " with sign resetted.");
                    fdata.WriteToFile(filename);
                }
            }

            //Console.WriteLine();
            //Console.WriteLine();

            Console.Write("\r\nDone. Exit procedure (y)? ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return;
            }

            goto Start;
        }
    }
}
