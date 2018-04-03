namespace DougBot
{
    public class DatabaseHandler
    {
        private readonly DatabaseConnection db;

        public DatabaseHandler()
        {
            db = DatabaseConnection.Instance();
            if (!db.IsConnected())
                db.DatabaseName = "DougBase";

            if (db.IsConnected())
            {

            }
        }
    }
}
