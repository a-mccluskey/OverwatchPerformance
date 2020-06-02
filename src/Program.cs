using PerformanceTracker.DataSources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    class Program
    {
        private static List<Game> games;

        private static readonly string DirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\";

        static void Main(string[] args)
        {
            if (args.Contains("-help"))
                ApplicationHelp();

            //Setup the file to read in
            IDataSource DataSource = null;
            string FileName = DirectoryPath;
            try
            {
                if (args.Length == 0)
                {
                    FileName += "SeasonData.csv"; //Default Filename
                    DataSource = new FileDataSource(FileName);
                }
                else
                {
                    if (args.Contains("-db"))
                        DataSource = new MySQLDataSource();
                    else
                    {
                        FileName += args[0];
                        DataSource = new FileDataSource(FileName);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: "+e.Message+ " Unable To continue");
                Console.WriteLine("\nPress any key to close");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            // Removed due to the map rotation changing mid season 21
            //Maps.LoadMaps((string)ConfigurationManager.AppSettings["Map"]);
            Maps.ExcludedMaps((string)ConfigurationManager.AppSettings["ExcludedMaps"]);
            games = new List<Game>();
            
            //Check that the data source is readable, and that it has some data in it
            if (DataSource.VerifySourceExists())
                games = DataSource.ReadExistingGamesSource();
            else
                PopulateGameData();

            ConsoleKey keyPressed;
            do
            {
                Console.WriteLine("Please choose from the following options:\n");
                Console.WriteLine("(A) to Add a new game\n(B) to Backup the game data\n(C) to Compress all backups\n(D) to Display the current stats\n" +
                    "(O) to display an Overview\n(S) to Save the games details\n(X) to eXit without saving");
                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.X)
                    Environment.Exit(0);
                if (keyPressed == ConsoleKey.D)
                    DisplayGameData();
                if (keyPressed == ConsoleKey.O)
                    GamesOverview();
                if (keyPressed == ConsoleKey.A)
                    AddNewGame();
                if (keyPressed == ConsoleKey.B)
                    ArchiveGameData();
                if (keyPressed == ConsoleKey.C)
                    ArchiveToZipFile();
            } while (keyPressed != ConsoleKey.S);

            DataSource.SaveGamesToDataSource(games);
            
            Console.WriteLine("File Saved.\nPress Any key to exit");
            Console.ReadKey();
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
                GameCreator NextGame = new GameCreator();
                NextGame.CreateGame();
                games.Add(NextGame);

                Console.WriteLine("Press X to exit, or any key to add a new game detail");
                exitCheck = Console.ReadKey().Key;
            } while (exitCheck != ConsoleKey.X);
        }

        static void DisplayGameData()
        {
            Console.Clear();
            Common.RowOfDashes();
            Console.WriteLine("|  SR  | Diff |         Map         | Game Length | Deaths | Deaths per 10 | Date & Time Played on |");
            Common.RowOfDashes();

            Console.Write($"| {games[0].SR} |");
            Console.Write(" ---- |         ----        |");//Diff & Map
            Console.Write("     ----    |  ----  |");//GL & Deaths
            Console.Write("     ----      |           ----        |");//DP10 & TPlayed
            Console.WriteLine();

            double AvgDeathsPerTenAllGames = Common.CalculateTotalDeathsPerTen(games);
            double AvgDeathsAllGames = Common.CalculateAvgDeathsAllGames(games);

            for (int i = 1; i < games.Count; i++)
            {
                Console.Write($"| {games[i].SR} |");
                int diffBetweenGames = games[i].SR - games[i - 1].SR;
                if (diffBetweenGames == 0)
                    Console.Write(" ---- ");
                string colorText = diffBetweenGames.ToString() + " ";
                colorText = colorText.PadLeft(6);
                if (diffBetweenGames > 0)
                    Common.WriteTextInGreen(colorText);
                if (diffBetweenGames < 0)
                    Common.WriteTextInRed(colorText);
                Console.Write($"|{games[i].Map.PadLeft(21)}|");
                Console.Write(games[i].GameTime.ToString("mm':'ss' '").PadLeft(13) + "|");
                if (games[i].Deaths > AvgDeathsAllGames)
                {
                    Common.WriteTextInRed((games[i].Deaths.ToString() + " ").PadLeft(8));
                }
                else
                {
                    Common.WriteTextInGreen((games[i].Deaths.ToString() + " ").PadLeft(8));
                }
                Console.Write("|");

                double DeathPerTenCurGame = double.Parse(Common.DeathsPerTen(games[i].Deaths, games[i].GameTime));
                if (DeathPerTenCurGame > AvgDeathsPerTenAllGames)
                {
                    Common.WriteTextInRed(DeathPerTenCurGame.ToString().PadLeft(15));
                }
                else
                {
                    Common.WriteTextInGreen(DeathPerTenCurGame.ToString().PadLeft(15));
                }
                Console.Write("|");
                Console.WriteLine($" {games[i].PlayedOn.ToString().PadLeft(21)} |");
                if(i%25==0)
                {
                    Console.WriteLine("Press Any Key to Display the next page");
                    Console.ReadKey();
                    Common.RowOfDashes();
                    Console.WriteLine("|  SR  | Diff |         Map         | Game Length | Deaths | Deaths per 10 | Date & Time Played on |");
                    Common.RowOfDashes();
                }
            }
            Common.RowOfDashes();
            Console.WriteLine();
        }

        static void AddNewGame()
        {
            GameCreator NewGame = new GameCreator();
            NewGame.CreateGame();
            games.Add(NewGame);
        }

        static void GamesOverview()
        {
            Console.Clear();
            
            int SR_Difference = games[games.Count-1].SR - games[0].SR;
            int winCount = 0;
            int lossCount = 0;
            int drawCount = 0;

            Dictionary<object, WLDStats> DayStats = new Dictionary<object, WLDStats>();
            Dictionary<string, WLDStats> MapStats = new Dictionary<string, WLDStats>();
            Dictionary<int, WLDStats> HourStats = new Dictionary<int, WLDStats>();


            foreach (var Day in Enum.GetValues(typeof(DayOfWeek)))
            {
                DayStats.Add(Day, new WLDStats());
            }
            foreach (var _map in Maps.AvailableMaps)
            {
                MapStats.Add(_map, new WLDStats());
            }
            for (int hour=0; hour<=23; hour++)
            {
                HourStats.Add(hour, new WLDStats());
            }

            for (int i=1; i<games.Count;i++)
            {
                if (games[i].SR == games[i - 1].SR)
                {
                    drawCount++;
                    DayStats[games[i].PlayedOn.DayOfWeek].IncreaseDraw();
                    MapStats[games[i].Map].IncreaseDraw();
                    HourStats[games[i].PlayedOn.Hour].IncreaseDraw();
                }
                if (games[i].SR > games[i - 1].SR)
                {
                    winCount++;
                    DayStats[games[i].PlayedOn.DayOfWeek].IncreaseWins();
                    MapStats[games[i].Map].IncreaseWins();
                    HourStats[games[i].PlayedOn.Hour].IncreaseWins();
                }
                if (games[i].SR < games[i - 1].SR)
                {
                    lossCount++;
                    DayStats[games[i].PlayedOn.DayOfWeek].IncreaseLoss();
                    MapStats[games[i].Map].IncreaseLoss();
                    HourStats[games[i].PlayedOn.Hour].IncreaseLoss();
                }
            }
            double winRate = ((double)winCount / (winCount + lossCount)*100);
            winRate = Math.Round(winRate, 1);

            var orderedDaysByTotalWins = DayStats.OrderByDescending(_dayOfWeek => _dayOfWeek.Value.GetWins()).ToList();
            var orderedDaysByWinRate = DayStats.OrderByDescending(_dayOfWeek => _dayOfWeek.Value.GetWinRate()).ToList();

            var orderedMapsByTotalWins = MapStats.OrderByDescending(_map => _map.Value.GetWins()).ToList();
            var orderedMapsByWinRate = MapStats.OrderByDescending(_map => _map.Value.GetWinRate()).ToList();

            var orderedHoursByWinRate = HourStats.OrderByDescending(_hour => _hour.Value.GetWinRate()).ToList();

            Common.RowOfDashes();
            Console.WriteLine($"Wins: {winCount} Losses: {lossCount} Draws: {drawCount}");
            Console.WriteLine($"Win Rate: {winRate.ToString()}");
            Console.WriteLine($"Total SR change this season: {SR_Difference}");

            Common.RowOfDashes();
            Console.WriteLine($"Best Day for wins is {orderedDaysByTotalWins[0].Key} With {orderedDaysByTotalWins[0].Value.GetWins()} Wins");
            Console.WriteLine($"Best Day for win rate is {orderedDaysByWinRate[0].Key} With {orderedDaysByWinRate[0].Value.GetWinRate()}%");
            Console.WriteLine($"Worst Day for win rate is {orderedDaysByWinRate.Last().Key} at {orderedDaysByWinRate.Last().Value.GetWinRate()}%");

            Common.RowOfDashes();
            Console.WriteLine($"Best Map for wins is {orderedMapsByTotalWins[0].Key} With {orderedMapsByTotalWins[0].Value.GetWins()} Wins at {orderedMapsByTotalWins[0].Value.GetWinRate()}%");
            Console.WriteLine($"Best Map for win rate is {orderedMapsByWinRate[0].Key} At {orderedMapsByWinRate[0].Value.GetWinRate()}% for a total of {orderedMapsByWinRate[0].Value.GetWins()} Wins");
            
            Common.RowOfDashes();
            Console.WriteLine($"Best Time of day is {orderedHoursByWinRate[0].Key} At {orderedHoursByWinRate[0].Value.GetWinRate()}%");
            Console.WriteLine($"2nd Best Time of day is {orderedHoursByWinRate[1].Key} At {orderedHoursByWinRate[1].Value.GetWinRate()}%");
            Console.WriteLine($"3rd Best Time of day is {orderedHoursByWinRate[2].Key} At {orderedHoursByWinRate[2].Value.GetWinRate()}%");
            Common.RowOfDashes();
            Console.WriteLine("Hourly breakdown:");

            foreach (var _hour in orderedHoursByWinRate)
            {
                Console.WriteLine($"| {_hour.Key} | {_hour.Value.GetWinRate()}% |");
            }
        }

        static void ApplicationHelp()
        {
            Console.WriteLine("Overwatch Performance tracker:\n To use a MySQL database connection use -db.");
            //Console.WriteLine(" To use an alternative file use -f=FILENAME");
            Console.WriteLine(" To display this help -help");
            Environment.Exit(1);
        }

        static void ArchiveGameData()
        {
            string archiveFileName = "stats_" + DateTime.Now.ToString("ddmmyy")+".csv";
            var backup = new FileDataSource(DirectoryPath+archiveFileName);
            backup.SaveGamesToDataSource(games);
            Console.Clear();
            Console.WriteLine("Backup saved to: " + DirectoryPath + archiveFileName);
        }

        static void ArchiveToZipFile()
        {

        }
    }
}
