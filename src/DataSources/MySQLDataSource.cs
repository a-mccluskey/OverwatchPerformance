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
    class MySQLDataSource /*: IDataSource*/
    {

        public MySqlConnection connection;

        public void init()
        {
            string server = "localhost";
            string database = "ow_stats";
            string uid = "root";
            string password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }
        public List<Game> ReadExistingGamesSource()
        {
            init();
            string query = "SELECT * FROM gamestats";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<object> srs = new List<object>();
            while (reader.Read())
            {
                srs.Add(reader.GetValue(2));
            }
            return null;
        }
        public void SaveGamesToDataSource(List<Game> games)
        {
            //TODO
        }
    }
}
