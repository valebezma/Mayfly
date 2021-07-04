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

            //arguments = new string[] { "-remove", "Fishery Desktop" };

            if (arguments.Length == 0)
            {
                Application.Run(new WizardInstall());
            }
            else
            {
                switch (arguments[0].Trim())
                {
                    case "-checkup":
                        Update.RunUpdates(arguments[1].Trim(), false);
                        break;

                    case "-update":
                        Update.RunUpdates(arguments[1].Trim(), true);
                        break;

                    case "-remove":
                        Application.Run(new WizardUninstall(arguments[1].Trim()));
                        break;
                }
            }
        }
    }
}
