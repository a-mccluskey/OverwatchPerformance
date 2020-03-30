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
            StreamWriter output = new StreamWriter(FileNamePath);
            output.WriteLine("SR, Map, Deaths, Game Length, Played On, Hero");
            output.Flush();
            foreach(var game in games)
            {
                output.WriteLine($"{game}");
            }
            output.Close();
            Console.WriteLine("Press Any key to exit");
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
            ConsoleKey exitCheck;

            do
            {
                Console.Clear();
                var NextGame = new Game();
                NextGame.PlayedOn = DateTime.Now;
                Console.WriteLine("Please enter the map name:");
                Maps.List();
                string input = Console.ReadLine();
                NextGame.Map = input;
                Console.WriteLine("Please enter the number of deaths:");
                NextGame.Deaths = int.Parse(Console.ReadLine()); ;
                Console.WriteLine("Please enter the First Hero:");
                Hero firstHero = new Hero();
                firstHero.SetHero(Console.ReadLine());
                NextGame.Heroes.Add(firstHero);
                Console.WriteLine("Please enter the game length");
                NextGame.GameTime = TimeSpan.Parse("0:" + Console.ReadLine().Replace('.', ':'));
                Console.WriteLine("Please enter your SR at the end of this game");
                NextGame.SR = int.Parse(Console.ReadLine());
                games.Add(NextGame);
                Console.WriteLine("Press X to exit, or any key to add a new game detail");
                exitCheck = Console.ReadKey().Key;
            } while (exitCheck != ConsoleKey.X);
        }
    }
}
