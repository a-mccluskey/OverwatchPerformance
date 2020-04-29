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

        private static void WriteTextInColour(string input, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ResetColor();
        }
    }
}
