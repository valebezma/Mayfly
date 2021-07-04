using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Software
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Mayfly.Service.Application_ThreadException;
            //Mayfly.Service.ResetUICulture();

            //arguments = new string[] { "-checkup", "Complex Fishery Desktop" };

            if (arguments.Length == 0)
            {
                Application.Run(new WizardInstall());
            }
            else
            {
                switch (arguments[0].Trim())
                {
                    case "-checkup":
                        Update.CheckUpdates(arguments[1].Trim());
                        break;

                    case "-update":
                        Update.DoUpdates(arguments[1].Trim());
                        break;

                    case "-remove":
                        Application.Run(new WizardUninstall(arguments[1].Trim()));
                        break;
                }
            }
        }
    }
}
