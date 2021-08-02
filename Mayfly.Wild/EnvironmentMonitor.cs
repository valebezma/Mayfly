using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Geographics;

namespace Mayfly.Wild
{
    public class EnvironmentMonitor : List<EnvironmentState>
    {
        public Waypoint Where;



        public EnvironmentMonitor()
        {
            Where = new Waypoint(0, 0, DateTime.Now);
        }



        public void Add(WeatherState w, AquaState a)
        {
            this.Add(new EnvironmentState(this.Where, w, a));
        }
    }

    public class EnvironmentState
    {
        public Waypoint Where;

        public DateTime When { get { return Where.TimeMark; } }

        public WeatherState StateOfWeather;

        public AquaState StateOfWater;



        public EnvironmentState(Waypoint where, WeatherState stateOfWeather, AquaState stateOfWater)
        {
            Where = where;
            StateOfWeather = stateOfWeather;
            StateOfWater = stateOfWater;
        }
    }

}
