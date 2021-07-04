using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Sedimentation.Models
{
    public class Model1986 : ModelLoad
    {
        public double HydraulicSizeCoefficient { get; private set; }

        public override string Reference
        {
            get
            {
                return "Временные указания по оценке повышения мутности при землечерпательных работах, проводимых для обеспечения транзитного судоходства на реках и учету ее влияния на качество воды и экологию гидробионтов. – М.: МРФ РСФСР, 1986";
            }
        }



        public Model1986(SedimentProject.ProjectRow project) :
            base(project)
        {
            Load = project.ControlLoad;
            HydraulicSizeCoefficient = PerformHydraulicSize(project.ControlHydraulicSize);

            foreach (SedimentProject.SectionRow row in project.GetSectionRows())
            {
                ModelStretch stretch = new ModelStretch(row);
                Stretches.Add(stretch);
            }

            double L = GetModelLongitude(this.Load);

            for (int i = 0; i < Stretches.Count; i++)
            {
                Stretches[i].Start = i == 0 ? 0 : Stretches[i - 1].End;

                double velocityCoefficient = PerformVelocity(Stretches[i].Velocity);
                double depthCoefficient = PerformDepth(Stretches[i].Depth);
                double dL = Stretches[i].Longitude * HydraulicSizeCoefficient * velocityCoefficient * depthCoefficient;

                Stretches[i].FinalAdditionalLoad = GetLoad(L + dL);

                L += dL;
            }
        }



        public static double GetModelLongitude(double load)
        {
            return 1;
        }

        public static double GetLoad(double modelLongitude)
        {
            return 1;
        }



        public static double PerformVelocity(double velocity)
        {
            return 1D / velocity;
        }

        public static double PerformDepth(double depth)
        {
            return 1;
        }

        public static double PerformHydraulicSize(double omega)
        {
            return 1;
        }
    }
}
