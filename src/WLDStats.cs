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
        private int winRatePercent = 01;
        

        public void IncreaseWins()
        {
            GamesPlayed++;
            GamesWon++;
            winRatePercent = (100* GamesWon) / (GamesWon+GamesLost);
        }
        public void IncreaseLoss()
        {
            GamesPlayed++;
            GamesLost++;
            winRatePercent = (100*GamesWon) / (GamesWon + GamesLost);
        }
        public void IncreaseDraw()
        {
            GamesPlayed++;
            GamesDrawn++;
            //the Winrate will be the same if we drawez 
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
        public int GetWinRate()
        {
            return winRatePercent;
        }
    }
}
