using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class Common
    {

        public static void RowOfDashes()
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
        }

        public static void WriteTextInGreen(string input)
        {
            WriteTextInColour(input, ConsoleColor.Green);
        }

        public static void WriteTextInRed(string input)
        {
            WriteTextInColour(input, ConsoleColor.Red);
        }

        public static string DeathsPerTen(int deaths, TimeSpan GameLength)
        {
            if (deaths <= 0)
                return "0";
            return Math.Round((deaths / GameLength.TotalSeconds) * 600, 1).ToString();
        }

        public static double CalculateTotalDeathsPerTen(List<Game> allGames)
        {
            int TotalDeaths = 0;
            TimeSpan TotalGameTime = new TimeSpan();
            foreach (var _game in allGames)
            {
                TotalDeaths += _game.Deaths;
                TotalGameTime += _game.GameTime;
            }
            return Double.Parse(DeathsPerTen(TotalDeaths, TotalGameTime));
        }

        public static double CalculateAvgDeathsAllGames(List<Game> allGames)
        {
            int TotalDeaths = 0;
            foreach (var _game in allGames)
            {
                TotalDeaths += _game.Deaths;
            }
            return (double)TotalDeaths / (allGames.Count-2);
        }

        public static string HourMorningAfternoon(int hour)
        {
            if (hour == 0)
                return "Midnight"; //"12 AM";
            if (hour == 12)
                return "Midday"; //"12 PM";
            if (hour > 0 && hour < 12)
                return hour + " AM";
            if (hour > 12 && hour < 24)
                return (hour-12) + " PM";
            throw new Exception("Hour is not valid"); 
        }

        private static void WriteTextInColour(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ResetColor();
        }
    }
}
