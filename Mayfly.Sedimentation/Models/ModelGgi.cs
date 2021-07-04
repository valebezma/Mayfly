using System;
using System.Collections.Generic;

namespace Mayfly.Sedimentation
{
    public class ModelGgi : ModelLoad
    {
        public double Depth { get; set; }

        //public double WaterWidth { get; set; }

        //public double Width { get; set; }

        public override string Reference
        {
            get
            {
                return "Добыча нерудных строительных материалов в водных объектах. Учет руслового " +
                    "процесса и рекомендации по проектированию и эксплуатации русловых карьеров. - СПБ.: "+
                    "Изд-во «Глобус», 2012. - 140 с.";
            }
        }



        public ModelGgi(SedimentProject.ProjectRow project) : base(project)
        {
            Depth = project.IsDepthNull() ? 1 : project.Depth;
            //WaterWidth = project.IsWidthNull() ? 1 : project.Width;
            //Width = project.IsWorkWidthNull() ? 1 : project.WorkWidth;
            Load = project.Load + (project.IsLoadNaturalNull() ? 0 : project.LoadNatural);

            foreach (SedimentProject.ZoneRow row in project.GetZoneRows())
            {
                ModelStretch stretch = new ModelStretch(row);
                stretch.ProcessSedimentation();
                Stretches.Add(stretch);
            }
        }



        public double GetLoad(double transitWeight)
        {
            return GetLoad(transitWeight, Project);
        }

        public static double GetLoad(double transitWeight, SedimentProject.ProjectRow project)
        {
            return transitWeight * 1000000.0 / (project.WaterSpend * project.Duration * 3600.0);
        }

        public override ModelStretch GetByLoad(double from, double to)
        {
            double l1 = GetDistanceOfLoad(from);
            double l2 = GetDistanceOfLoad(to);
            ModelStretch result = new ModelStretch(l2, l1, Project);
            result.ProcessSedimentation();

            return result;
        }
        
        public double GetDistanceOfLoad(double criticalLoad)
        {
            double result = 0;

            foreach (ModelStretch stretch in Stretches)
            {
                if (stretch.FinalAdditionalLoad > criticalLoad)
                {
                    result = stretch.End;
                }

                if (stretch.FinalAdditionalLoad < criticalLoad)
                {
                    break;
                }
            }

            return result;
        }

        public ModelStretch GetBySilt(double from, double to)
        {
            double l1 = GetDistanceOfSilt(from);
            double l2 = GetDistanceOfSilt(to);
            ModelStretch result = new ModelStretch(l2, l1, Project);
            result.ProcessSedimentation();

            return result;
        }
        
        public double GetDistanceOfSilt(double criticalSilt)
        {
            double result = 0;

            foreach (ModelStretch stretch in Stretches)
            {
                if (stretch.SedimentsMeanWidth > criticalSilt)
                {
                    result = stretch.End;
                }

                if (stretch.SedimentsMeanWidth < criticalSilt)
                {
                    break;
                }
            }

            return result;
        }
    }
}
