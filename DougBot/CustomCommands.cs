using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
