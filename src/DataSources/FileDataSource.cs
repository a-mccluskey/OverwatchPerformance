using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class FileDataSource : IDataSource
    {
        public FileDataSource()
        {
        }
        public FileDataSource(string input)
        {
            this.FileName = input;
        }
        private string FileName;

        public bool VerifySourceExists()
        {
            return File.Exists(FileName);
        }

        public List<Game> ReadExistingGamesSource()
        {
            StreamReader input = new StreamReader(FileName);
            input.ReadLine(); //We ignore this first line - as it's the header
            string initalSR = input.ReadLine().Split(',')[0];
            var FirstGame = new Game();
            List<Game> games = new List<Game>();
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
                NextGame.GameTime = TimeSpan.Parse("0:" + currentLine[3].Trim());
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
            return games;
        }

        public List<Game> ReadExistingGamesSource(string input)
        {
            FileName = input;
            return ReadExistingGamesSource();
        }

        public void SaveGamesToDataSource(List<Game> games)
        {
            StreamWriter output = new StreamWriter(FileName);
            output.WriteLine("SR, Map, Deaths, Game Length, Played On, Hero");
            output.Flush();
            foreach (var game in games)
            {
                output.WriteLine($"{game}");
                output.Flush();
            }
            output.Close();
        }

    }
}
