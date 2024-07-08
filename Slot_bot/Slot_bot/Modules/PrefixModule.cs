using Discord.Commands;
using Discord;

namespace Slot_bot.Modules
{
    public class PrefixModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task HandlePingCommand()
        {
            await Context.Message.ReplyAsync("!PING - " + Context.Message.Author.GlobalName);
        }
    }
}
