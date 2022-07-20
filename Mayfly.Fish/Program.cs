using System;
using System.Windows.Forms;

namespace Mayfly.Fish
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

            Wild.SettingsReader.SetFeature("Fish", ".fcd", Wild.MassDegree.Kilogramm, Wild.MassDegree.Gramm);
            Mayfly.UserSettings.SetApp(Properties.Resources.logo, Properties.Resources.sriif, Wild.Resources.Interface.Powered.SRIIF);
            Log.WriteAppStarted();

            if (args.Length == 0)
            {
                Application.Run(new FishCard());
            }
            else
            {
                FishCard card = new FishCard(args[0]);

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