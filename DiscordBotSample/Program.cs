using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBotSample
{
    class Program
    {
        string botToken = ConfigurationManager.AppSettings["botToken"];
        InteractionService _interactionService;
        ServiceProvider services;

        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            services = ConfigureServices(new ServiceCollection());
            var _client = services.GetRequiredService<DiscordSocketClient>();

            // setup logging
            _client.Log += LogAsync;

            //Start bot
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();

            _client.Ready += async () =>
            {
                //Enregistre les commandes et interactions

                _interactionService = services.GetRequiredService<InteractionService>();
                await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), services);
#if DEBUG
                await _interactionService.RegisterCommandsToGuildAsync(0); //Remplacer 0 par l'id du serveur discord servant d'env de dev
#else
                await _interactionService.RegisterCommandsGloballyAsync();
#endif
                _client.InteractionCreated += async interaction => {
                    var scope = services.CreateScope();
                    var ctx = new SocketInteractionContext(_client, interaction);
                    await _interactionService.ExecuteCommandAsync(ctx, scope.ServiceProvider);
                };
            };

            //Rich Presence
            await _client.SetActivityAsync(new Game("My Game")); //Définit un statut custom au bot

            await Task.Delay(-1);
        }

        public Task LogAsync(LogMessage log)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}    {log.Message}");
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices(IServiceCollection services)
        {
            var config = new DiscordSocketConfig
            {
                MessageCacheSize = 0,
                LogLevel = LogSeverity.Info,
                GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMessages |
                GatewayIntents.DirectMessages,
                ConnectionTimeout = 7000
            };
            var client = new DiscordSocketClient(config);
            var interactionService = new InteractionService(client.Rest, new InteractionServiceConfig() { DefaultRunMode = RunMode.Async, LogLevel = LogSeverity.Verbose, AutoServiceScopes = true});

            return services.AddSingleton(client)
                .AddSingleton(interactionService)
                .BuildServiceProvider();
        }


    }
}
