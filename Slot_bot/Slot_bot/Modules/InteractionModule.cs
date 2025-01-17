﻿using Discord.Interactions;
using Discord;
using Slot_bot.Data.Interfaces;
using Slot_bot.Entities;
using Slot_bot.Services;

namespace Slot_bot.Modules
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        IUnitOfWork unitOfWork;
        SlotService appearanceService;
        public InteractionModule( IUnitOfWork unitOfWork, SlotService appearanceService)
        {
            this.unitOfWork = unitOfWork;   
            this.appearanceService = appearanceService;
        }

        [SlashCommand("start", "Start gambling!")]
        public async Task HandleStartCommand()
        {
            appearanceService.SpinSlot();
            var user =await  unitOfWork.UserRepository.IsUserExistAsync(Context.User.Id.ToString());
            if (user is null)
            {
                user = new User() { Id = Context.User.Id.ToString(), Username = Context.User.GlobalName, Balance = 500 };
                await unitOfWork.UserRepository.AddAsync(user);
            }
            var embed = new EmbedBuilder()
                    .WithTitle("Slots")
                    .WithColor(Color.Magenta)
                    .WithDescription(appearanceService.ToString())
                    .WithFooter($"Balance:  {user.Balance} \t Player - {user.Username}")
                    .Build();
            var componentBuilder = new ComponentBuilder()
                .WithButton("Spin", "spin", ButtonStyle.Success)
                .Build();
            
            await RespondAsync(embed: embed, components: componentBuilder,text:$"Lets gamble!");
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
