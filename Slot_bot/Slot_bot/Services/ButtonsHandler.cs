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
        SlotAppearanceService appearanceService;
        public ButtonsHandler(IUnitOfWork unitOfWork, SlotAppearanceService appearanceService)
        {
            this.unitOfWork = unitOfWork;  
            this.appearanceService = appearanceService;
        }
        public async Task ButtonClickHandler(SocketMessageComponent component)
        {

            var user = await unitOfWork.UserRepository.IsUserExistAsync(component.User.Id.ToString());
            
            if (component.Data.CustomId == "spin")
            {

                user.Balance -= 5;
                appearanceService.SpinSlot();
                var embed = new EmbedBuilder()
                    .WithTitle("🎰🎰🎰🎰🎰🎰🎰🎰 Slots 🎰🎰🎰🎰🎰🎰🎰🎰")
                    .WithColor(Color.Green)
                    .WithDescription(appearanceService.ToString())
                    .WithFooter($"Balance - {user.Balance}  \t\t\t\t\t\t Player - {user.Username}         SlotBot v1.0.0")
                    .Build();

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
