using Mayfly.Wild;
using Mayfly.Waters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using Mayfly;
using System.Resources;
using Meta.Numerics;

namespace Mayfly.Fish
{
    public abstract class Service
    {
        public static string Opening;

        public static Samplers.SamplerRow Sampler(int samplerID)
        {
            return UserSettings.SamplersIndex.Sampler.FindByID(samplerID);
        }

        public static double DefaultOpening()
        {
            object result = UserSettings.GetValue(UserSettings.Path, nameof(Opening), 600);

            if (result == null)
            {
                return double.NaN;
            }
            else
            {
                return (double)(int)result / 100;
            }
        }

        public static double DefaultOpening(int samplerID)
        {
            object result = UserSettings.GetValue(UserSettings.Path, nameof(Opening), Sampler(samplerID).ShortName, 600);

            if (result == null)
            {
                return DefaultOpening();
            }
            else
            {
                return (double)(int)result / 100;
            }
        }

        public static void SaveOpening(int samplerID, double value)
        {
            UserSettings.SetValue(UserSettings.Path, nameof(Opening), Sampler(samplerID).ShortName, 
                (int)(value * 100));
        }

        internal static string Section(int p)
        {
            ResourceManager resources = new ResourceManager(typeof(Individual));

            switch (p)
            {
                case -1:
                    return Resources.Common.AllTract;
                case 0:
                    return resources.GetString("comboBoxSection.Items");
                default:
                    return resources.GetString("comboBoxSection.Items" + p);
            }
        }

        public static void SaveEquipment()
        {
            UserSettings.Equipment.WriteXml(System.IO.Path.Combine(Mayfly.IO.UserFolder, "equipment.ini"));
        }
    }
}
