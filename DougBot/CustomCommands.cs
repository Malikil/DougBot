using Discord.Commands;
using System.Threading.Tasks;
using System.Data.SQLite;
using System;

namespace DougBot
{
    public class CustomCommands : ModuleBase
    {
        [Command("ping"), Summary("Pings the bot.")]
        public async Task Ping()
        {
            await base.ReplyAsync("Pong!");
        }

        [Command("dodb"), Summary("Executes a command on the database")]
        public async Task DoDB([Remainder] string command)
        {
            DatabaseHandler db = DatabaseHandler.Instance();
            int rows = db.ExecuteNoQuery(command);
            await ReplyAsync(rows + " row(s) affected.");
        }

        [Command("adddb"), Summary("Inserts into the table")]
        public async Task AddDB(string name, string secret)
        {
            DatabaseHandler db = DatabaseHandler.Instance();
            db.AddEntry(name, secret);
            await ReplyAsync("Added to database");
        }

        [Command("getdb"), Summary("Gets everything in the database")]
        public async Task GetDB()
        {
            DatabaseHandler db = DatabaseHandler.Instance();
            SQLiteDataReader reader = db.GetData();
            string s = "```\n";
            while (reader.Read())
            {
                s += reader[0] + " | " + reader[1] + "\n";
            }
            s += "```";
            await ReplyAsync(s);
        }
    }
}
