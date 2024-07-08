using Discord.Interactions;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Slot_bot.Services
{
    public class PrefixHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IConfigurationRoot config;

        public PrefixHandler(DiscordSocketClient client, CommandService commands, IConfigurationRoot config)
        {
            this.client = client;
            this.commands = commands;
            this.config = config;
        }

        public async Task InitializeAsync()
        {
            client.MessageReceived += HandleCommandAsync;
        }

        public void AddModule<T>()
        {
            commands.AddModuleAsync<T>(null);
        }

        private async Task HandleCommandAsync(SocketMessage message)
        {
            var msg = message as SocketUserMessage;
            if (msg is null) return;

            int argPos = 0;

            if (!((msg.HasCharPrefix(config["prefix"][0], ref argPos)) || !msg.HasMentionPrefix(client.CurrentUser, ref argPos)) || message.Author.IsBot) return;

            var context = new SocketCommandContext(client, msg);
            await commands.ExecuteAsync(context: context, argPos: argPos, services: null);
        }
    }
}
