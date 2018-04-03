using Discord.Commands;
using System.Threading.Tasks;

namespace DougBot
{
    public class CustomCommands : ModuleBase
    {
        [Command("ping"), Summary("Pings the bot.")]
        public async Task Ping()
        {
            await base.ReplyAsync("Pong!");
        }

        [Command("dodb"), Summary("Executes code on the database")]
        public async Task DoDb([Remainder]string arg)
        {
            new DatabaseHandler();
        }
    }
}
