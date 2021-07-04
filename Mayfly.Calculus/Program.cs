using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Calculus
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
            Application.ThreadException += Service.Application_ThreadException;
            
            Log.WriteAppStarted();

            if (!Licensing.VerifyApp("Statistics")) return;

            if (args.Length == 0)
            {
                Application.Run(new MainForm());
            }
            else
            {
                MainForm card = new MainForm();

                if (args.Length > 0)
                {
                    card.LoadData(args[0]);
                }

                Application.Run(card);
            }
        }
    }
}
