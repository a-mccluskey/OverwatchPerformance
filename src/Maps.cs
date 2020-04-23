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
            // Now disused as of 14th April 2020
            AvailableMaps = new List<string>();
            foreach (var map in input.Split(','))
            {
                AvailableMaps.Add(map.Trim());
            }
        }

        public static void ExcludedMaps(string input)
        {
            AvailableMaps = new List<string>();
            // Assault AKA 2CP
            AvailableMaps.Add("Hanamura");
            AvailableMaps.Add("Horizon Lunar Colony");
            AvailableMaps.Add("Paris");
            AvailableMaps.Add("Temple of Anubis");
            AvailableMaps.Add("Volskaya Industries");
            // Escort AKA payload
            AvailableMaps.Add("Dorado");
            AvailableMaps.Add("Junkertown");
            AvailableMaps.Add("Rialto");
            AvailableMaps.Add("Route 66");
            AvailableMaps.Add("Watchpoint: Gibraltar");
            // Assault / Escort AKA Hybrid
            AvailableMaps.Add("Blizzard World");
            AvailableMaps.Add("Eichenwalde");
            AvailableMaps.Add("Hollywood");
            AvailableMaps.Add("King's Row");
            AvailableMaps.Add("Numbani");

            // Control AKA KOTH
            AvailableMaps.Add("Busan");
            AvailableMaps.Add("Ilios");
            AvailableMaps.Add("Lijiang Tower");
            AvailableMaps.Add("Nepal");
            AvailableMaps.Add("Oasis");

            foreach (var map in input.Split(','))
            {
                AvailableMaps.Remove(map.Trim());
            }
        }

        public static void List()
        {
            foreach (var map in AvailableMaps)
                Console.WriteLine("* " + map);
        }
    }
}
