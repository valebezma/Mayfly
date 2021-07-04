using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Waters
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

            //args = new string[] { @"C:\Users\valebezma\AppData\Roaming\Mayfly\WatersDefaultList.wtr" };

            if (args.Length == 0)
            {
                Application.Run(new MainForm());
            }
            else
            {
                if (args.Length == 1)
                {
                    Application.Run(new MainForm(args[0]));
                }
            }

            Log.WriteAppEnded();
        }
    }
}