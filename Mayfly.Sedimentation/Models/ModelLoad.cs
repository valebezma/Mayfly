using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Sedimentation
{
    public class ModelLoad
    {
        public SedimentProject.ProjectRow Project { get; set; }

        public List<ModelStretch> Stretches { get; private set; }

        public double Load { get; internal set; }

        public virtual string Reference
        {
            get
            {
                return string.Empty;
            }
        }



        private ModelLoad()
        {
            Stretches = new List<ModelStretch>();
        }

        public ModelLoad(SedimentProject.ProjectRow prjRow) : this()
        {
            Project = prjRow;
        }



        public virtual ModelStretch GetByLoad(double from, double to)
        {
            double l1 = GetDistance(from);
            double l2 = GetDistance(to);
            ModelStretch result = new ModelStretch(l1, l2, Project);


            return result;
        }



        public double GetDistance(double criticalLoad)
        {
            if (criticalLoad >= Load) return 0;

            foreach (ModelStretch stretch in Stretches)
            {
                if (stretch.FinalFullLoad == criticalLoad)
                {
                    return stretch.End;
                }

                if (stretch.StartingFullLoad > criticalLoad && stretch.FinalFullLoad <= criticalLoad)
                {
                    return Service.GetX(stretch.Start, stretch.End, stretch.StartingFullLoad, stretch.FinalFullLoad, criticalLoad);
                }
            }

            return double.PositiveInfinity;
        }

        public double GetMaxLoad
        {
            get
            {
                double s = 0;
                foreach (ModelStretch stretch in Stretches)
                {
                    s = Math.Max(s, stretch.FinalAdditionalLoad);
                }
                return s;
            }
        }

        public double GetMaxSedimentWidth
        {
            get
            {
                double s = 0;
                foreach (ModelStretch stretch in Stretches)
                {
                    s = Math.Max(s, stretch.SedimentsMeanWidth);
                }
                return s;
            }
        }
    }
}