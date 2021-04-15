using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.DataObject
{
    public class TimeWayMetroClass
    {
        public int minutes { get; set; }
        public string Comment { get; set; }

        public static List<TimeWayMetroClass> GetTimewayMetro()
        {
            List<TimeWayMetroClass> TWM = new List<TimeWayMetroClass>()
            {
                new TimeWayMetroClass(){minutes=5, Comment="5 минут"},
                new TimeWayMetroClass(){minutes=10, Comment="10 минут"},
                new TimeWayMetroClass(){minutes=15, Comment="15 минут"},
                new TimeWayMetroClass(){minutes=30, Comment="30 минут"},
                new TimeWayMetroClass(){minutes=0, Comment="Более 30 мин"},

            };
            return TWM;
        }
     

    }
}
