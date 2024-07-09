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

        [SlashCommand("start", "Start gambling!")]
        public async Task HandleStartCommand()
        {
            var user =await  unitOfWork.UserRepository.IsUserExistAsync(Context.User.Id.ToString());
            if (user is not null)
            {
                await RespondAsync($"You have already start gambling!");
            }
            else
            {
                 await unitOfWork.UserRepository.AddAsync(new User() { Id = Context.User.Id.ToString(), Username = Context.User.GlobalName, Balance = 500 });
                await RespondAsync($"Hello {Context.User.GlobalName} my name is SlotBot i give you 500$ lets gamble!");
            }
        }

        [SlashCommand("balance","Check your balance")]
        public async Task HandleBalanceCommand()
        {
            var user = await unitOfWork.UserRepository.IsUserExistAsync(Context.User.Id.ToString());
            if (user is not null)
            {
                await RespondAsync($"You have {user.Balance}$");
            }
            else
            {
                await RespondAsync($"You are not a member!");
            }
        }
    }
}
