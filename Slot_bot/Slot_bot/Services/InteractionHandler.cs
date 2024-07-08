using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace Slot_bot.Services
{
    public class InteractionHandler
    {
        private readonly DiscordSocketClient client;
        private readonly InteractionService commands;
        private readonly IServiceProvider serviceProvider;

        public InteractionHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider serviceProvider)
        {
            this.client = client;
            this.commands = commands;
            this.serviceProvider = serviceProvider;
        }
         
        public async Task InitializeAsync()
        {
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

            client.InteractionCreated += HandleInteraction;
        }

        private async Task HandleInteraction(SocketInteraction interaction)
        {
            try
            {
                var context =new SocketInteractionContext(client,interaction);
                await commands.ExecuteCommandAsync(context, serviceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
