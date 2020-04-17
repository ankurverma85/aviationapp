using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;

namespace AviationApp.FAADataParser
{
    public class Cycle
    {
        [PrimaryKey, AutoIncrement]
        public int CycleID { get; set; }
        public DateTime StartDate { get; set; }
        public bool FixesParsed { get; set; }
        public static (bool found, DateTime cycle, bool fiftySixDay)? GetCurrentCycle()
        {
            DateTime now = DateTime.UtcNow.Date;
            IOrderedEnumerable<(DateTime, bool)> lowerCycles = releaseDates.Where(n => n.Item1 <= now).OrderBy(n => (now - n.Item1));
            (DateTime cycle, bool fiftySixDay) = lowerCycles.FirstOrDefault();
            bool found = lowerCycles.Count() > 0;
            return (found, cycle, fiftySixDay);
        }
        private static readonly List<(DateTime, bool)> releaseDates = new List<(DateTime, bool)>
        {
            (new DateTime(2020, 01, 02), false), (new DateTime (2020, 01, 30), true),
            (new DateTime(2020, 02, 27), false), (new DateTime (2020, 03, 26), true),
            (new DateTime(2020, 04, 23), false), (new DateTime (2020, 05, 21), true),
            (new DateTime(2020, 06, 18), false), (new DateTime (2020, 07, 16), true),
            (new DateTime(2020, 08, 13), false), (new DateTime (2020, 09, 10), true),
            (new DateTime(2020, 10, 08), false), (new DateTime (2020, 11, 05), true),
            (new DateTime(2020, 12, 03), false), (new DateTime (2020, 12, 31), true)
        };
    }
}
