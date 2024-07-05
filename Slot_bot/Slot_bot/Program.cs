using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Slot_bot.Configuration;
using Slot_bot.Data;
using Slot_bot.Data.Interfaces;
using Slot_bot.Data.UnitOfWork;
using Slot_bot.Entities;


namespace Program { 
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = DependencyInjection.ConfigureApp();

            await provider.GetService<Bot>().RunAsync();

            Console.ReadKey();
        }
    }
}