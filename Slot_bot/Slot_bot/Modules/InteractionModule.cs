using Discord.Interactions;
using Discord;
using Slot_bot.Data;
using Slot_bot.Data.Interfaces;
using Slot_bot.Entities;
using Slot_bot.Data.UnitOfWork;

namespace Slot_bot.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        ApplicationDbContext dbContext;
        IUnitOfWork unitOfWork;
        public InteractionModule(ApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            this.dbContext = dbContext;
            this.unitOfWork = unitOfWork;   

        }

        [SlashCommand("ping","Recieve a ping message!")]
        public async Task HandlePingCommand()
        {
            await RespondAsync("PING!");
        }

        [SlashCommand("start", "Start gambling!")]
        public async Task HandleStartCommand()
        {
            if (await unitOfWork.UserRepository.IsUserExistAsync(Context.User.Id))
            {
                await RespondAsync($"You have already start gambling!");
            }
            else
            {
                 await unitOfWork.UserRepository.AddAsync(new User() { Id = Context.User.Id, Username = Context.User.GlobalName, ScoreAmount = 500 });
                await RespondAsync($"Hello {Context.User.GlobalName} my name is SlotBot i give you 500$ lets gamble!");
            }
        }


    }
}
