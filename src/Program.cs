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
            
            // Removed due to the map rotation changing mid season 21
            //Maps.LoadMaps((string)ConfigurationManager.AppSettings["Map"]);
            Maps.ExcludedMaps((string)ConfigurationManager.AppSettings["ExcludedMaps"]);
            games = new List<Game>();
            if (File.Exists(FileNamePath))
                ReadExistingFileIn(FileNamePath);
            else
                PopulateGameData();

            ConsoleKey exitCheck;
            do
            {
                // TODO
                // Add more games in? 
                // Display stats for the current list of games?

                Console.WriteLine("Press S to save the games details, X to quit without saving");
                exitCheck = Console.ReadKey().Key;
                if (exitCheck == ConsoleKey.X)
                    Environment.Exit(0);
            } while (exitCheck != ConsoleKey.S) ;

            StreamWriter output = new StreamWriter(FileNamePath);
            output.WriteLine("SR, Map, Deaths, Game Length, Played On, Hero");
            output.Flush();
            foreach(var game in games)
            {
                output.WriteLine($"{game}");
                output.Flush();
            }
            output.Close();
            Console.WriteLine("File Saved.\nPress Any key to exit");
            Console.ReadKey();
        }

        static void ReadExistingFileIn(string FileNamePath)
        {
            StreamReader input = new StreamReader(FileNamePath);
            input.ReadLine(); //We ignore this first line - as it's the header
            string initalSR = input.ReadLine().Split(',')[0];
            var FirstGame = new Game();
            FirstGame.SR = int.Parse(initalSR);
            games.Add(FirstGame);
            Game NextGame;
            while (!input.EndOfStream)
            {
                NextGame = new Game();
                string[] currentLine = input.ReadLine().Split(',');
                NextGame.SR = int.Parse(currentLine[0]);
                NextGame.Map = currentLine[1].Trim();
                NextGame.Deaths = int.Parse(currentLine[2]);
                NextGame.GameTime = TimeSpan.Parse("0:"+currentLine[3].Trim());
                NextGame.PlayedOn = DateTime.Parse(currentLine[4]);
                string[] HeroList = currentLine[5].Split(';');
                foreach (var HeroString in HeroList)
                {
                    var Hero = new Hero();
                    Hero.SetHero(HeroString.Trim());
                    NextGame.Heroes.Add(Hero);
                }
                games.Add(NextGame);
            }
            input.Close();
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
                do
                {
                    Console.WriteLine("Please enter the map name:");
                    Console.WriteLine(Maps.List());
                    string input = Console.ReadLine();
                    NextGame.Map = Maps.ValidateMap(input);
                } while (NextGame.Map == null);
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
