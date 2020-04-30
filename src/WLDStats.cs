using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.DesignerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class WLDStats
    {
        private int GamesPlayed = 0;
        private int GamesWon = 0;
        private int GamesLost = 0;
        private int GamesDrawn = 0;
        

        public void IncreaseWins()
        {
            GamesPlayed++;
            GamesWon++;
        }
        public void IncreaseLoss()
        {
            GamesPlayed++;
            GamesWon++;
        }
        public void IncreaseDraw()
        {
            GamesPlayed++;
            GamesWon++;
        }

        public int GetPlayed()
        {
            return GamesPlayed;
        }
        public int GetWins()
        {
            return GamesWon;
        }
        public int GetLoss()
        {
            return GamesLost;
        }
        public int GetDraw()
        {
            return GamesDrawn;
        }
    }
}
