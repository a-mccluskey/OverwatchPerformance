using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    static class Maps
    {
        public static List<string> AvailableMaps;

        public static List<string> SeasonMaps;

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
            AvailableMaps.Add("Havana");
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

            SeasonMaps = new List<string>(AvailableMaps);
            foreach (var map in input.Split(','))
            {
                SeasonMaps.Remove(map.Trim());
            }
        }

        public static string List()
        {
            string output = "";
            for (int i = 0; i<SeasonMaps.Count; i++)
                output+=$"{i} {SeasonMaps[i]}\n";
            return output;
        }
        
        public static string ValidateMap(string input)
        {
            int index;
            if (int.TryParse(input, out index))
                return SeasonMaps[index];
            switch(input.ToLower())
            {
                case "hanamura":
                    return "Hanamura";
                case "horizon lunar colony":
                case "horizon":
                    return "Horizon Lunar Colony";
                case "paris":
                    return "Paris";
                case "temple of anubis":
                case "temple":
                case "anubis":
                    return "Temple of Anubis";
                case "volskaya industries":
                case "volskaya":
                    return "Volskaya Industries";

                case "dorado":
                    return "Dorado";
                case "junkertown":
                case "junk":
                case "junk town":
                    return "Junkertown";
                case "rialto":
                    return "Rialto";
                case "route 66":
                case "route":
                case "66":
                    return "Route 66";
                case "havana":
                    return "Havana";
                case "watchpoint: gibraltar":
                case "watchpoint: gibralter":
                case "watchpoint gibraltar":
                case "watchpoint":
                case "gibraltar":
                case "gibralter":
                case "gibralta":
                    return "Watchpoint: Gibraltar";

                case "blizzard world":
                case "blizzard":
                case "theme park":
                    return "Blizzard World";
                case "eichenwalde":
                case "castle":
                    return "Eichenwalde";
                case "hollywood":
                case "holywood":
                case "holly wood":
                    return "Hollywood";
                case "king's row":
                case "kings row":
                case "row":
                    return "King's Row";
                case "numbani":
                    return "Numbani";

                case "busan":
                case "korea":
                    return "Busan";
                case "ilios":
                case "ilio":
                case "illios":
                    return "Ilios";
                case "lijiang tower":
                case "lijiang":
                case "tower":
                    return "Lijiang Tower";
                case "nepal":
                    return "Nepal";
                case "oasis":
                    return "Oasis";
            }

            // if we don't know which was tried then just return null
            return null;
        }
    }
}
