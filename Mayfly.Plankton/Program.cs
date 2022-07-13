﻿using System;
using System.Windows.Forms;

namespace Mayfly.Plankton
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

            Wild.ReaderSettings.SetFeature("Plankton", ".pcd");
            Log.WriteAppStarted();

            if (args.Length == 0)
            {
                Application.Run(new PlanktonCard());
            }
            else
            {
                PlanktonCard card = new  PlanktonCard(args[0]);

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
