using Discord;
using Discord.Interactions;
using System.Threading.Tasks;
using DiscordBotSample.Models;

namespace DiscordBotSample.Services
{
    [Group("subscribe", "Abonnement pour un utilisateur ou un serveur")]
    public class SubscribeCommandService : InteractionModuleBase<SocketInteractionContext>
    {

        #region SlashCommands

        /// <summary>
        /// Create an Alert
        /// </summary>
        /// <param name="tradingPairs"></param>
        /// <param name="ut"></param>
        /// <returns></returns>
        [SlashCommand("user", "Vous abonne pour une en MP", runMode: RunMode.Async)]
        public async Task SubscribeUser([Summary(description: "La pair de trading à surveiller")] string tradingPair,
                                        [Summary(description: "L'unité de temps à surveiller")] BaseModel.UnitTime ut) //Les Enums permettent une suggestion auto des réponses dans Discord
        {
            await Context.Interaction.RespondAsync("Aucune alerte enregistrée");
        }


        /// <summary>
        /// Create an Alert on a Discord Server
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="tradingPair"></param>
        /// <param name="ut"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [SlashCommand("server", "Abonne un channel de votre serveur", runMode: RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SubscribeGuildChannel(
            [Summary(description: "Le channel où envoyer les alertes")] ITextChannel channel, //Permet l'autocomplétion avec les channels du serveur
            [Summary(description: "La pair de trading à surveiller")] string tradingPair,
            [Summary(description: "L'unité de temps à surveiller")] BaseModel.UnitTime ut,
            [Summary(description: "Un groupe à ping en cas d'alerte (Optionnel)")] IRole role = null) //Permet l'autocomplétion avec les rôles du serveur, parametre optionnel le rend optionnel aussi dans la commande
        {
            await Context.Interaction.RespondAsync("Aucune alerte enregistrée");
        }

        #endregion SlashCommands


        #region ComponentInteraction

        [ComponentInteraction("subscribe-update", true, runMode: RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task UpdateAlerts(string[] selectedValues)
        {
            if (Context.Channel is IPrivateChannel)
                //Réponse en MP
                await Context.User.SendMessageAsync("updated");
            else
                //Réponse dans un channel de serveur
                await Context.Interaction.RespondAsync("updated");
        }

        #endregion ComponentInteraction

    }
}
