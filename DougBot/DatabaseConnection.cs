using MySql.Data.MySqlClient;

namespace DougBot
{
    public class DatabaseConnection
    {
        private DatabaseConnection() { }
        
        public MySqlConnection Connection { get; private set; } = null;

        private static DatabaseConnection _instance = null;
        public static DatabaseConnection Instance()
        {
            if (_instance == null)
                _instance = new DatabaseConnection();
            return _instance;
        }

        public bool IsConnected()
        {
            return (Connection != null && Connection.State == System.Data.ConnectionState.Open);
        }

        public bool UseDatabase(string databaseName)
        {
            if (!IsConnected())
            {
                string constring = "Server=localhost;";
                Connection = new MySqlConnection(constring);
                Connection.Open();
            }
            
        }

        public void ExecuteCommand(string mySqlCommand)
        {
            using (MySqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = mySqlCommand;
                command.ExecuteNonQuery();
            }
        }

        public void QueryDatabase(string mySqlCommand)
        {
            using (MySqlCommand command = Connection.CreateCommand())
            {
                command.CommandText = mySqlCommand;
            }
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
