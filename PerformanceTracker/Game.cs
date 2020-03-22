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

        private int _TotalHealing;

        public List<Hero> Heroes;

    }
}
