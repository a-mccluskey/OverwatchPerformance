using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class GameCreator : Game
    {
        public void CreateGame()
        {
            Console.Clear();
            this.PlayedOn = DateTime.Now;
            do
            {
                Console.WriteLine("Please enter the map name:");
                Console.WriteLine(Maps.List());
                string input = Console.ReadLine();
                this.Map = Maps.ValidateMap(input);
            } while (this.Map == null);
            Console.WriteLine("Please enter the number of deaths:");
            this.Deaths = int.Parse(Console.ReadLine()); ;
            Console.WriteLine("Please enter the First Hero:");
            Hero firstHero = new Hero();
            firstHero.SetHero(Console.ReadLine());
            this.Heroes.Add(firstHero);
            Console.WriteLine("Please enter the game length");
            this.GameTime = TimeSpan.Parse("0:" + Console.ReadLine().Replace('.', ':'));
            Console.WriteLine("Please enter your SR at the end of this game");
            this.SR = int.Parse(Console.ReadLine());
        }
    }
}
