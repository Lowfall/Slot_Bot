using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Slot_bot.Entities
{
    public class Bot
    {
        private DiscordSocketClient client;
        private string token;
        private  ILogger<Bot> logger;
        private IServiceProvider serviceProvider;
        public Bot(ILogger<Bot> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            client = new DiscordSocketClient();
            this.serviceProvider = serviceProvider;
            this.token = configuration.GetSection("BotConfiguration")["BotToken"];
            this.logger = logger;
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
