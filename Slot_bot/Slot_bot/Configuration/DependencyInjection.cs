using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slot_bot.Data.Interfaces;
using Slot_bot.Data.UnitOfWork;
using Slot_bot.Data;
using Slot_bot.Entities;
using Microsoft.Extensions.Logging;

namespace Slot_bot.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceProvider ConfigureApp()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "appsettings.json"), optional: false, reloadOnChange: true)
            .Build();

            var services = new ServiceCollection();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationDbContext>();
            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<Bot>();

            var provider = services.BuildServiceProvider();

            return provider;
        }
    }
}
