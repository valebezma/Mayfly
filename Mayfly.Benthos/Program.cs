using System;
using System.Windows.Forms;
using Mayfly.Wild.Controls;

namespace Mayfly.Benthos
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

            Wild.SettingsReader.SetFeature("Benthos", ".bcd", Wild.MassDegree.Gramm, Wild.MassDegree.Milligramm);
            Mayfly.UserSettings.SetApp(Properties.Resources.logo, Properties.Resources.ibiw, Wild.Resources.Interface.Powered.IBIW);
            Log.WriteAppStarted();

            if (args.Length == 0)
            {
                Application.Run(new BenthosCard());
            }
            else
            {
                BenthosCard card = new BenthosCard(args[0]);

                //if (args.Length > 1)
                //{
                //    card.Logger.RunDefinition(args[1]);
                //}

                Application.Run(card);
            }

            Log.WriteAppEnded();
        }
    }
}
