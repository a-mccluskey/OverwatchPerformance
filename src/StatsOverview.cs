using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class StatsOverview
    {
        //All the stats the need to be usable via the interface.
        public int SR_Difference; //The Total SR change betwen the first game and the last game
        public int winCount; 
        public int lossCount;
        public int drawCount;

        public double winRate; //The win rate as a percentage

        public List<KeyValuePair<object, WLDStats>> orderedDaysByTotalWins;
        public List<KeyValuePair<object, WLDStats>> orderedDaysByWinRate;

        public List<KeyValuePair<string, WLDStats>> orderedMapsByTotalWins;
        public List<KeyValuePair<string, WLDStats>> orderedMapsByWinRate;

        public List<KeyValuePair<int, WLDStats>> orderedHoursByWinRate;

        public List<KeyValuePair<object, WLDStats>> orderedHeroByTotalWins;
        public List<KeyValuePair<object, WLDStats>> orderedHeroByWinRate;

        StatsOverview()
        { }

        public StatsOverview(List<Game> games) 
        {
            SR_Difference = games[games.Count - 1].SR - games[0].SR;
            winCount = 0;
            lossCount = 0;
            drawCount = 0;

            //Dictionaries to contain the stats for a specific metric
            Dictionary<object, WLDStats> DayStats = new Dictionary<object, WLDStats>();
            Dictionary<string, WLDStats> MapStats = new Dictionary<string, WLDStats>();
            Dictionary<int, WLDStats> HourStats = new Dictionary<int, WLDStats>();
            Dictionary<object, WLDStats> HeroStats = new Dictionary<object, WLDStats>();

            //Initalise the dictionaries
            foreach (var Day in Enum.GetValues(typeof(DayOfWeek)))
            {
                DayStats.Add(Day, new WLDStats());
            }
            foreach (var _map in Maps.AvailableMaps)
            {
                MapStats.Add(_map, new WLDStats());
            }
            for (int hour = 0; hour <= 23; hour++)
            {
                HourStats.Add(hour, new WLDStats());
            }
            foreach (var hero in Enum.GetValues(typeof(SupportHero)))
            {
                HeroStats.Add(hero, new WLDStats());
            }

            //Go through all the games and get W/L/D counts for a particular metric
            //Eg. 10W 6L 1D for a Tuesday
            for (int i = 1; i < games.Count; i++)
            {
                if (games[i].SR == games[i - 1].SR)
                {
                    drawCount++;
                    DayStats[games[i].PlayedOn.DayOfWeek].IncreaseDraw();
                    MapStats[games[i].Map].IncreaseDraw();
                    HourStats[games[i].PlayedOn.Hour].IncreaseDraw();
                    foreach (var hero in games[i].Heroes)
                    {
                        HeroStats[hero.hero].IncreaseDraw();
                    }
                }
                if (games[i].SR > games[i - 1].SR)
                {
                    winCount++;
                    DayStats[games[i].PlayedOn.DayOfWeek].IncreaseWins();
                    MapStats[games[i].Map].IncreaseWins();
                    HourStats[games[i].PlayedOn.Hour].IncreaseWins();
                    foreach (var hero in games[i].Heroes)
                    {
                        HeroStats[hero.hero].IncreaseWins();
                    }
                }
                if (games[i].SR < games[i - 1].SR)
                {
                    lossCount++;
                    DayStats[games[i].PlayedOn.DayOfWeek].IncreaseLoss();
                    MapStats[games[i].Map].IncreaseLoss();
                    HourStats[games[i].PlayedOn.Hour].IncreaseLoss();
                    foreach(var hero in games[i].Heroes)
                    {
                        HeroStats[hero.hero].IncreaseLoss();
                    }
                }
            }

            //calulate the win rate percentage
            //I've decided that a draw doesn't count as a 1W 1L 1D gives a 33% win rate
            winRate = ((double)winCount / (winCount + lossCount) * 100);
            winRate = Math.Round(winRate, 1);

            //Orders the specific metrics by total wins & by win percentage
            //As if 10 times more games are played on a Tuesday it would give a false sense that you get more wins on a tuesday
            orderedDaysByTotalWins = DayStats.OrderByDescending(_dayOfWeek => _dayOfWeek.Value.GetWins()).ToList();
            orderedDaysByWinRate = DayStats.OrderByDescending(_dayOfWeek => _dayOfWeek.Value.GetWinRate()).ToList();

            orderedMapsByTotalWins = MapStats.OrderByDescending(_map => _map.Value.GetWins()).ToList();
            orderedMapsByWinRate = MapStats.OrderByDescending(_map => _map.Value.GetWinRate()).ToList();

            orderedHoursByWinRate = HourStats.OrderByDescending(_hour => _hour.Value.GetWinRate()).ToList();

            orderedHeroByTotalWins = HeroStats.OrderByDescending(_hero => _hero.Value.GetWins()).ToList();
            orderedHeroByWinRate = HeroStats.OrderByDescending(_hero => _hero.Value.GetWinRate()).ToList();
        }
    }
}
