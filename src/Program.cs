using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class Program
    {
        private static List<Game> games;

        static void Main(string[] args)
        {
            string FileName = "SeasonData.csv";
            string FileNamePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + FileName;
            Maps.LoadMaps((string)ConfigurationManager.AppSettings["Map"]);
            games = new List<Game>();
            if (File.Exists(FileNamePath))
                ReadExistingFileIn();
            else
                PopulateGameData(); 
            Console.ReadKey();
        }

        static void ReadExistingFileIn()
        {
            // Use something like streamreader
        }

        static void PopulateGameData()
        {
            var FirstGame = new Game();
            Console.WriteLine("Please enter your earlist SR figure:");
            FirstGame.SR = int.Parse(Console.ReadLine());
            games.Add(FirstGame);
            
            // Start of a do while loop?
            var NextGame = new Game();
            Console.WriteLine("Please enter the map name:");
            Maps.List();
            string input = Console.ReadLine();
            NextGame.Map = input;
            Console.WriteLine("Please enter the number of deaths:");
            NextGame.Deaths = int.Parse(Console.ReadLine()); ;
            Console.WriteLine("Please enter the map name:");
            NextGame.Heroes.Add(new Hero());  // Waiting on the hero object

        }
    }
}
