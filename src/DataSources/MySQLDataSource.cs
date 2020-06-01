using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace PerformanceTracker.DataSources
{
    class MySQLDataSource : IDataSource
    {

        public MySqlConnection connection;

        public MySQLDataSource()
        {
            string server = "localhost";
            string database = "ow_stats";
            string uid = "root";
            string password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch(Exception e)
            {
                throw new Exception("Unable to connect to Database. Error: " + e.Message);
            }
        }
        public List<Game> ReadExistingGamesSource()
        {
            string query = "SELECT * FROM gamestats";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<Game> games = new List<Game>();
            var FirstGame = new Game();
            reader.Read();
            FirstGame.SR = int.Parse(reader.GetString("SR"));
            games.Add(FirstGame);

            while (reader.Read())
            {
                Game sglGame = new Game();
                sglGame.SR = int.Parse(reader.GetString("SR"));
                
                sglGame.Map = reader.GetString("Map");
                sglGame.Deaths = int.Parse(reader.GetString("Deaths"));
                sglGame.GameTime = TimeSpan.Parse(reader.GetString("Game_Length"));
                sglGame.PlayedOn = DateTime.Parse(reader.GetString("Played_On"));
                string[] HeroList = reader.GetString("Hero").Split(';');
                foreach (var HeroString in HeroList)
                {
                    var Hero = new Hero();
                    Hero.SetHero(HeroString.Trim());
                    sglGame.Heroes.Add(Hero);
                }

                games.Add(sglGame);
            }
            reader.Close();
            return games;
        }
        public void SaveGamesToDataSource(List<Game> games)
        {
            //TODO
        }
        public bool VerifySourceExists()
        {
            string query = "SELECT * FROM gamestats";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            //Has to be in a separate variable as reader.HasRows is false as soon as the connection is closed
            bool HasRows = reader.HasRows;
            reader.Close();
            return HasRows;
        }
    }
}
