using System;
using System.Threading.Tasks;
using BaclavaSharp.Framework;
using DSharpPlus;

namespace BaclavaSharp
{
    public class Program
    {
        public static DiscordClient Client;
        private static CommandHandler _handler;

        private static void Main()
        {
            MainAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Loading baclava...");
            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = Environment.GetEnvironmentVariable("TOKEN"),
                TokenType = TokenType.Bot
            });
            
            _handler = new CommandHandler();
            _handler.RegisterAll();

            Client.MessageCreated += async e =>
            {
                if (!e.Message.Content.StartsWith(">>")) return;
                var cmdName = e.Message.Content.Substring(2).Split(' ')[0];
                var command = _handler.FindByAlias(cmdName);
                if (command != null) await command.Execute(e);
            };

            await Client.ConnectAsync();
            Console.WriteLine("Finished loading.");
            await Task.Delay(-1);
        }
    }
}