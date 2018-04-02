using Discord;
using Discord.WebSocket;
using Discord.Net.Providers.WS4Net;
using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;

namespace DougBot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            // Create the client object, set up logging and receiving messages
            DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                WebSocketProvider = WS4NetProvider.Instance
            });
            client.Log += Log;
            client.MessageReceived += Message;

            // Login and start client using token from file
            string token = GetToken();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Wait until program end
            while (true)
            {
                Console.Write(" > ");
                string cmd = Console.ReadLine();
                if (cmd.ToLower().Equals("quit") || cmd.ToLower().Equals("exit"))
                    break;
            }
            await client.StopAsync();
            await client.LogoutAsync();
        }

        private async Task Message(SocketMessage message)
        {
            if (message.Content.Equals("!ping"))
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
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
