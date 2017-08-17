using BotAuth.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BotAuth;
using BotAuth.Models;

namespace SharePointBot.Dialogs
{
    /// <summary>
    /// Sub-class of AuthDialog to account for various peculiarities encountered.
    /// </summary>
    /// <seealso cref="BotAuth.Dialogs.AuthDialog" />
    public class SharePointBotAuthDialog : AuthDialog
    {
        public SharePointBotAuthDialog(IAuthProvider AuthProvider, AuthenticationOptions AuthOptions, string Prompt = "Please click to sign in: ") : base(AuthProvider, AuthOptions, Prompt)
        {

        }

        protected new Task PromptToLogin(IDialogContext context, IMessageActivity msg, string authenticationUrl)
        {
            Attachment plAttachment = null;
            SigninCard plCard;

            if (msg.ChannelId == "skypeforbusiness")
            {
                return context.PostAsync($@"<a href=""{authenticationUrl}"">Authentication Required</a>");
            }
            else if (msg.ChannelId == "msteams")
                plCard = new SigninCard(this.prompt, GetCardActions(authenticationUrl, "openUrl"));
            else
                plCard = new SigninCard(this.prompt, GetCardActions(authenticationUrl, "signin"));
            plAttachment = plCard.ToAttachment();

            IMessageActivity response = context.MakeMessage();
            response.Recipient = msg.From;
            response.Type = "message";

            response.Attachments = new List<Attachment>();
            response.Attachments.Add(plAttachment);

            return context.PostAsync(response);
        }

        private List<CardAction> GetCardActions(string authenticationUrl, string actionType)
        {
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction plButton = new CardAction()
            {
                Value = authenticationUrl,
                Type = actionType,
                Title = "Authentication Required"
            };
            cardButtons.Add(plButton);
            return cardButtons;
        }
    }
}