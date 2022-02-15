using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using DiscordBotSample.Helpers;

namespace DiscordBotSample.Services
{
    public class ComponentInteractionService : InteractionModuleBase<SocketInteractionContext>
    {
        //ComponentInteraction avec le même id que le SlashCommand, bien différencier les deux.
        [ComponentInteraction("donate", ignoreGroupNames: true, runMode: RunMode.Async)]
        public async Task DonateButton()
        {
            Embed embed = ResponseHelper.CreateEmbedResponse();
            try
            {
                await Context.Interaction.User.SendMessageAsync(embed: embed);
                _ = Context.Channel.DeleteMessageAsync(Context.Interaction.Id);
            }
            catch
            {
                Console.WriteLine($"DM fail with {Context.Interaction.User} - {Context.Interaction.User.Id}");
            }
        }

        //Répond à l'intéraction delete définit sur les boutons pour supprimer le message
        [ComponentInteraction("delete", ignoreGroupNames: true, runMode: RunMode.Async)]
        public async Task Delete()
        {
            await (Context.Interaction as SocketMessageComponent).Message.DeleteAsync();
        }


        #region Private Methods


        #endregion Private Methods
    }
}
