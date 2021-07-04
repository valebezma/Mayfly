using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Sedimentation
{
    public class ModelNgavt : ModelLoad
    {
        public override string Reference
        {
            get
            {
                return "Баула В. А. Упрощенный метод определения границ зон мутности при работе земснарядов. В сб. научн. трудов НГАВТ «Развитие внутренних водных путей Сибири и Саха (Якутии)». – Изд-во: НГАВТ, Новосибирск, 1994";
            }
        }



        public ModelNgavt(SedimentProject.ProjectRow project) :
            base(project)
        {
            Load = project.ControlLoad;

            foreach (SedimentProject.SectionRow row in project.GetSectionRows())
            {
                ModelStretch stretch = new ModelStretch(row);
                Stretches.Add(stretch);
            }

            for (int i = 0; i < Stretches.Count; i++)
            {
                Stretches[i].Start = i == 0 ? 0 : Stretches[i - 1].End;

                double ratio = GetDilution(Stretches[i].Velocity,
                    Stretches[i].Depth, project.ControlHydraulicSize, Stretches[i].Longitude);

                double p = (i == 0 ? project.ControlLoad : Stretches[i - 1].FinalAdditionalLoad);

                Stretches[i].FinalAdditionalLoad = ratio * p;
            }
        }



        public static double GetDilution(double v, double h, double omega, double dl)
        {
            return Math.Exp(-.0023 * dl * Math.Log(Math.Pow(1.7 / v, 1.7)) * Math.Log(Math.Pow(12.5 / h, .5)) * Math.Log(omega / .18));
        }

        public static double DistanceOfTargetDilution(double v, double h, double omega, double targetRatio)
        {
            // 1 : Math.Exp(-.0023 * dl * Math.Log(Math.Pow(1.7 / v, 1.7)) * Math.Log(Math.Pow(12.5 / h, .5)) * Math.Log(omega / .18)) = targetRatio;
            // 2 : -.0023 * dl * Math.Log(Math.Pow(1.7 / v, 1.7)) * Math.Log(Math.Pow(12.5 / h, .5)) * Math.Log(omega / .18) = Math.Log(targetRatio);
            // 3 : dl = Math.Log(targetRatio) / (-.0023 * Math.Log(Math.Pow(1.7 / v, 1.7)) * Math.Log(Math.Pow(12.5 / h, .5)) * Math.Log(omega / .18));

            return Math.Log(targetRatio) / (-.0023 * Math.Log(Math.Pow(1.7 / v, 1.7)) * Math.Log(Math.Pow(12.5 / h, .5)) * Math.Log(omega / .18));
        }
    }
}
