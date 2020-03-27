using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    static class Maps
    {
        public static List<string> AvailableMaps;
        public static void LoadMaps(string input)
        {
            AvailableMaps = new List<string>();
            foreach (var map in input.Split(','))
            {
                AvailableMaps.Add(map.Trim());
            }
        }

        public static void List()
        {
            foreach (var map in AvailableMaps)
                Console.WriteLine("* " + map);
        }
    }
}
