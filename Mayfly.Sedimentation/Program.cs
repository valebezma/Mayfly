using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Sedimentation
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Mayfly.Service.Application_ThreadException;

            Log.WriteAppStarted();

            //args = new string[] { @"C:\Users\valeb\Desktop\Check.mud" };
            //args = new string[] { @"C:\Users\valeb\Desktop\Check.mud", "-print" };
            args = new string[] { "-run" };

            switch (args.Length)
            {
                case 0:
                    Application.Run(new MainForm());
                    break;

                case 1:
                    switch (args[0])
                    {
                        case "-set":
                            Application.Run(new Settings());
                            break;

                        case "-run":
                            Application.Run(new WizardSed());
                            break;

                        default:
                            Application.Run(new MainForm(args[0]));
                            break;
                    }
                    break;

                default:
                    string filename = args[0];

                    switch (args[1])
                    {
                        case "-print":
                            SedimentProject Data = new SedimentProject();
                            Data.ReadXml(filename);
                            Data.Project[0].GetReport().Run();
                            break;

                        case "-run":
                            Application.Run(new WizardSed(filename));
                            break;
                    }
                    break;
            }

            Log.WriteAppEnded();
        }
    }
}
