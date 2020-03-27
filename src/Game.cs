using System;
using System.Collections.Generic;
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
                    throw new Exception("Not a valid map for this season - check the app.config contains the correct maps");
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

        public List<Hero> Heroes;

    }
}