using Discord;
using System;

namespace DiscordBotSample.Helpers
{
    static class ResponseHelper
    {
        public static MessageComponent CreateDeleteButton()
        {
            try
            {
                var component = new ComponentBuilder()
                    .WithButton("Effacer", "delete")
                    .Build();
                return component;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Créer un ensemble de bouton "Try Again" / "Delete"
        /// </summary>
        /// <returns></returns>
        public static MessageComponent CreateTryAgainDeleteButtons(string initialInteractionId, bool enabledRetry = true)
        {
            try
            {
                var component = new ComponentBuilder()
                    .WithButton("Try Again !", initialInteractionId, disabled: !enabledRetry)
                    .WithButton("Annuler", "delete", ButtonStyle.Secondary)
                    .Build();
                return component;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Créer un ensemble de bouton "Confirmer" / "Annuler"
        /// </summary>
        /// <returns></returns>
        public static MessageComponent CreateConfirmCancelButtons(string initialInteractionId)
        {
            try
            {
                var component = new ComponentBuilder()
                    .WithButton("Confirmer", initialInteractionId + "-confirm")
                    .WithButton("Annuler", "delete", ButtonStyle.Secondary)
                    .Build();
                return component;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Créer un Embed pour les dons
        /// </summary>
        /// <returns></returns>
        public static Embed CreateEmbedResponse()
        {
            EmbedBuilder embedBuilder = new EmbedBuilder()
            {
                Title = "Title",
                Description = $"Une courte description",
                ThumbnailUrl = "https://cdn.pixabay.com/photo/2021/04/30/16/47/btc-logo-6219386_1280.png",
                Color = Color.Green,
                Fields = new()
                {
                    new EmbedFieldBuilder() { IsInline = true, Name = "Name 1", Value = "1" },//Inline permet d'aligner ou non jusqu'a 3 Fields sur la même ligne
                    new EmbedFieldBuilder() { IsInline = true, Name = "Name 2", Value = "2" },
                    new EmbedFieldBuilder() { IsInline = false, Name = "Name 3", Value = "3" },
                    new EmbedFieldBuilder() { IsInline = false, Name = "Name 4", Value = "4" },
                    new EmbedFieldBuilder() { IsInline = false, Name = "Name 5", Value = "5" },
                },
                Timestamp = DateTime.Now,
            };
            return embedBuilder.Build();
        }

        internal static MessageComponent CreateMenuReponseComponent()
        {
            SelectMenuBuilder menuBuilder = new SelectMenuBuilder()
            {
                Placeholder = "Liste déroulante",
                MinValues = 0,
                CustomId = "subscribe-update",
                IsDisabled = false,
                MaxValues = 4,  //MaxValues doit pas dépasser le nombre d'options du menu
                Options = new() {
                    new("Option 1", "1", isDefault: true),
                    new("Option 2", "2", isDefault: false),
                    new("Option 3", "3", isDefault: true),
                    new("Option 4", "4", isDefault: true),
                }
            };

            return new ComponentBuilder().WithSelectMenu(menuBuilder).Build();
        }

    }
}
