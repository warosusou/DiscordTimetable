using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTimetable
{
    static class TimeSchedule
    {
        public const int ClassInterval = 100;
        public static IReadOnlyCollection<DateTime> StartingTimes = new List<DateTime> 
        {
            new DateTime(1,1,1,9,0,0),
            new DateTime(1,1,1,10,50,0),
            new DateTime(1,1,1,13,10,0),
            new DateTime(1,1,1,15,0,0),
            new DateTime(1,1,1,16,50,0),
            new DateTime(1,1,1,18,40,0),
            new DateTime(1,1,1,20,30,0)
        };
    }
}
