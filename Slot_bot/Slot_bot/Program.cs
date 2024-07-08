using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Slot_bot.Data;
using Pomelo.EntityFrameworkCore.MySql;
using Slot_bot.Data.Interfaces;
using Slot_bot.Data.UnitOfWork;
using Slot_bot.Modules;
using Slot_bot.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Program { 
    class Program
    {
        public static Task Main() => new Program().MainAsync();

        public async Task MainAsync() {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "appsettings.json"))
                .Build();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context,services) => {
                    var connectionString =configuration.GetConnectionString("MySqlConnection");
                    services.AddSingleton(configuration);
                    services.AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig()
                    {
                        GatewayIntents = GatewayIntents.All,
                        AlwaysDownloadUsers = true
                    }));
                    services.AddDbContext<ApplicationDbContext>(options =>
                     {
                         options.UseMySql(connectionString,ServerVersion.Create(new Version(8, 0, 0), ServerType.MySql));
                     });
                    services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
                    services.AddSingleton<InteractionHandler>();
                    services.AddSingleton(x => new CommandService());
                    services.AddSingleton<PrefixHandler>();
                    services.AddSingleton<IUnitOfWork,UnitOfWork>();
                })
                .Build();

            await RunAsync(host);
        }

        private async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            var slashCommands = serviceProvider.GetRequiredService<InteractionService>();
            var client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            var configuration = serviceProvider.GetRequiredService<IConfigurationRoot>();

            await serviceProvider.GetRequiredService<InteractionHandler>().InitializeAsync();

            var prefixCommands = serviceProvider.GetRequiredService<PrefixHandler>();
            prefixCommands.AddModule<PrefixModule>();
            await prefixCommands.InitializeAsync();


            client.Log += async (LogMessage message) => Console.WriteLine(message);
            slashCommands.Log += async (LogMessage message) => Console.WriteLine(message);

            client.Ready += async () =>
            {
                Console.WriteLine("Bot ready!");
                await slashCommands.RegisterCommandsToGuildAsync(UInt64.Parse(configuration["testGuild"]), true);
            };

            await client.LoginAsync(TokenType.Bot, configuration.GetSection("BotConfiguration")["BotToken"]);
            await client.StartAsync();

            await Task.Delay(-1);
        }
    }
}