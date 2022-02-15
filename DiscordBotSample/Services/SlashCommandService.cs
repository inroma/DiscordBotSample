using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using DiscordBotSample.Helpers;

namespace DiscordBotSample.Services
{
    public class SlashCommandService : InteractionModuleBase<SocketInteractionContext>
    {

        [SlashCommand("donate", "Pour soutenir le développement du bot", true, runMode: RunMode.Async)]
        public async Task SendDonationEmbedResponse()
        {
            //Création d'une réponse avec mise en page (composant Discord)
            Embed embed = ResponseHelper.CreateEmbedResponse();
            try
            {
                //Notifie dans le channel du serveur que la réponse est envoyé en MP + envoi en MP
                if (Context.Channel is SocketGuildChannel)
                {
                    await Context.User.SendMessageAsync(embed: embed);
                    _ = Context.Interaction.RespondAsync("Réponse envoyé en DM", components: ResponseHelper.CreateDeleteButton());
                }
                else
                    await Context.Interaction.RespondAsync("", embed: embed); //Les embeds vont dans la partie embed

            }
            catch (Exception)
            {
                MessageComponent component = ResponseHelper.CreateTryAgainDeleteButtons("donate");
                _ = Context.Interaction.RespondAsync("Impossible d'envoyer un DM (DM ouverts ?)", components: component);
            }
        }


        /// <summary>
        /// Delete all alerts for the server
        /// </summary>
        /// <returns></returns>
        [SlashCommand("delete-all", "Supprime toutes les alertes du serveur", true, runMode: RunMode.Async)]
        public async Task ShowDialogDeleteAllAlerts()
        {
            if ((Context.Channel is IPrivateChannel) || (Context.User as IGuildUser).GuildPermissions.Administrator)
            {
                try
                {
                    var buttons = ResponseHelper.CreateConfirmCancelButtons("delete-all");
                    await Context.Interaction.RespondAsync($"Voulez-vous vraiment supprimer toutes les alertes ?", components: buttons); //Les boutons et menu déroulant sont dans la partie Components de la réponse
                }
                catch (Exception e)
                {
                    _ = Context.Interaction.RespondAsync($"Une erreur est survenue. \n {e.Message}", ephemeral: true);
                }
            }
        }


        /// <summary>
        /// Delete all alerts
        /// </summary>
        /// <returns></returns>
        [ComponentInteraction("delete-all-confirm", true, runMode: RunMode.Async)]
        public async Task DeleteAllAlerts()
        {
            _ = Context.Interaction.DeferAsync();   //Notifie Discord que l'interaction est reçu (permet un traitement de +3 secondes avant réponse)
            try
            {
                await Context.Interaction.FollowupAsync($"Alertes supprimées"); //FollowUp permet de répondre au message qui a trigger cette interaction (donc une réponse du bot avec un bouton/menu déroulant)
            }
            catch (Exception e)
            {
                _ = Context.Interaction.FollowupAsync($"Une erreur est survenue. \n {e.Message}", ephemeral: true);
            }
        }
    }
}
