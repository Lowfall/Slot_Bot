using Discord;
using Discord.WebSocket;
using Slot_bot.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Slot_bot.Services
{
    public class ButtonsHandler
    {
        IUnitOfWork unitOfWork;
        SlotService appearanceService;
        public ButtonsHandler(IUnitOfWork unitOfWork, SlotService appearanceService)
        {
            this.unitOfWork = unitOfWork;  
            this.appearanceService = appearanceService;
        }
        public async Task ButtonClickHandler(SocketMessageComponent component)
        {

            var user = await unitOfWork.UserRepository.IsUserExistAsync(component.User.Id.ToString());
            
            if (component.Data.CustomId == "spin")
            {

                
                appearanceService.SpinSlot();
                var result = appearanceService.IsWinningCombination(-5);
                user.Balance += result;
                Embed embed;
                if (result < 0)
                {
                    embed = new EmbedBuilder()
                    .WithTitle("Slots")
                    .WithColor(Color.Red)
                    .WithDescription(appearanceService.ToString() + $"\nYou lose!")
                    .WithFooter($"Balance:  {user.Balance} \t Player - {user.Username}")
                    .Build();
                }
                else
                {
                    embed = new EmbedBuilder()
                    .WithTitle("Slots")
                    .WithColor(Color.Green)
                    .WithDescription(appearanceService.ToString() + $"\nYou win {result} $")
                    .WithFooter($"Balance:  {user.Balance} \t Player - {user.Username}")
                    .Build();
                }
                var componentBuilder = new ComponentBuilder()
                    .WithButton("Spin", "spin", ButtonStyle.Success)
                    .Build();

                await component.UpdateAsync(msg =>
                {
                    msg.Embed = embed;
                    msg.Components = componentBuilder;
                });
                await unitOfWork.UserRepository.UpdateAsync(user);
            }
        }
    }
}
