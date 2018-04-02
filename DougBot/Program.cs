using Discord;
using Discord.WebSocket;
using Discord.Net.Providers.WS4Net;
using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using System.Reflection;

namespace DougBot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandService commands;

        public async Task Start()
        {
            // Create private objects client object
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                WebSocketProvider = WS4NetProvider.Instance
            });
            commands = new CommandService();

            // Set up logging and command handling
            client.Log += Log;
            client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            // Login and start client using token from file
            string token = GetToken();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Wait until program end
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd.ToLower().Equals("quit") || cmd.ToLower().Equals("exit"))
                    break;
            }
            await client.StopAsync();
            await client.LogoutAsync();
        }
        
        private async Task HandleCommand(SocketMessage messageParam)
        {
            // Make sure the message came from another user
            SocketUserMessage message = messageParam as SocketUserMessage;
            if (message == null)
                return;

            // A number to track the end of the prefix
            int argPos = 0;
            
            // If the user just pings the bot with no message body, ping them back
            if (message.Content.Substring(2).Equals(client.CurrentUser.Mention.Substring(3)))
            {
                await message.Channel.SendMessageAsync(message.Author.Mention);
                return;
            }

            // Check if the message has the proper prefix
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)))
                return;

            // Create a command context
            var context = new CommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private String GetToken()
        {
            String token;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("..\\..\\..\\..\\..\\token.txt");
                token = reader.ReadLine();
            }
            catch (IOException)
            {
                token = "";
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return token;
        }
    }
}
