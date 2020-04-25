using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class Game
    {
        private int _SR;
        public int SR
        {
            get => _SR;
            set
            {
                if (value > 0 || value < 5000)
                    _SR = value;
                else
                    throw new Exception("SR must be between 0 and 5000");
            }
        }
        private int _Deaths;

        public int Deaths
        {
            get => _Deaths;
            set
            {
                if (value >= 0)
                    _Deaths = value;
                else
                    throw new Exception("Deaths must not be negative"); 
            }
        }

        private string _map;

        public string Map
        {
            get => _map;
            set
            { 
                if (Maps.AvailableMaps.Contains(value))
                    _map = value;
                else
                    throw new Exception("Not a valid map");
            }
        }

        private int _TotalHealing;

        public int TotalHealing
        {
            get => _TotalHealing;
            set
            {
                if (value >= 0)
                    _TotalHealing = value;
                else
                    throw new Exception("Total healing must be positive");
            }
        }

        private TimeSpan _GameTime;
        
        public TimeSpan GameTime
        {
            get => _GameTime;
            set
            {
                if (value.TotalMinutes < 45 && value.TotalSeconds > 60)
                    _GameTime = value;
                else
                    throw new Exception("Game must be at least 60 seconds but less than 45 minutes");
            }
        }

        public DateTime PlayedOn;

        public List<Hero> Heroes = new List<Hero>();

        private string HeroesToString()
        {
            string output = "";
            foreach (var hero in this.Heroes)
                output += hero.hero+ ";";
            if (output.Length>2)
                output = output.Remove(output.Length-1);
            return output;
        }

        public override string ToString()
        {
            return $"{this.SR}, {this.Map}, {this.Deaths}, {this.GameTime.ToString("mm':'ss")}, {this.PlayedOn}, {this.HeroesToString()}";
        }
    }
}