using System;
using System.Data.SQLite;
using System.IO;

namespace DougBot
{
    public class DatabaseHandler
    {
        private DatabaseHandler()
        {
            Connect();
        }
        
        private SQLiteConnection connection = null;

        private static DatabaseHandler _instance = null;
        public static DatabaseHandler Instance()
        {
            if (_instance == null)
                _instance = new DatabaseHandler();
            return _instance;
        }

        public int ExecuteNoQuery(string commandText)
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteNonQuery();
        }

        public int AddEntry(string user, string secret)
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Example VALUES ('{user}', '{secret}');";
            return command.ExecuteNonQuery();
        }

        public SQLiteDataReader GetData()
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Example;";
            return command.ExecuteReader();
        }

        public void Connect()
        {
            if (connection == null)
            {
                if (!File.Exists("DougBase.sqlite"))
                    SQLiteConnection.CreateFile("DougBase.sqlite");

                connection = new SQLiteConnection("Data Source=DougBase.sqlite; Version=3");
                connection.Open();
                CreateTables();
            }
        }

        public void Disconnect()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        private void CreateTables()
        {
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Example (" +
                "user VARCHAR(20), " +
                "secret VARCHAR(10));";
            command.ExecuteNonQuery();
        }
    }
}
