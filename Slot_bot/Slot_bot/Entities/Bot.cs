using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Slot_bot.Entities
{
    public class Bot
    {
        DiscordSocketClient client;
        string token;
        ILogger<Bot> logger;

        public Bot(ILogger<Bot> logger, IConfiguration configuration)
        {
            client = new DiscordSocketClient();
            this.token = configuration.GetSection("BotConfiguration")["BotToken"];
            this.logger = logger;
            client.Log += Log;
        }

        private async Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
        }

        public async Task RunAsync()
        {
            logger.Log(LogLevel.Information, "Starting discord bot");
            client.LoginAsync(TokenType.Bot, token).Wait();
            await client.StartAsync();
        }

        public async Task StopAsync()
        {
            logger.Log(LogLevel.Information, "Bot stopped");
            await client.StopAsync();
        }
    }
}
