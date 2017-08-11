using BotAuth;
using BotAuth.AADv2;
using BotAuth.Dialogs;
using BotAuth.Models;
using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SharePointBot.Helpers
{
    public class AuthenticationHelper
    {
        public static AuthenticationOptions GetDefaultOffice365Otions()
        {
            return new AuthenticationOptions()
            {
                Authority = ConfigurationManager.AppSettings["aad:Authority"],
                ClientId = ConfigurationManager.AppSettings["MicrosoftAppId"],
                ClientSecret = ConfigurationManager.AppSettings["MicrosoftAppPassword"],
                Scopes = new string[] { "User.Read" }
            };
        }

        public static async Task ForwardToLoginDialog(IDialogContext context, object message, ResumeAfter<AuthResult> loginCallBack)
        {
            var options = AuthenticationHelper.GetDefaultOffice365Otions();
            options.RedirectUrl = ConfigurationManager.AppSettings["aad:Callback"];
            await context.Forward(new AuthDialog(new MSALAuthProvider(), options), loginCallBack, message, CancellationToken.None);
        }

        public static async Task LogOut(IDialogContext context)
        {
            // We will store the conversation reference in the callback URL. When Office 365 logs out it will hit the LogOut endpoint and pass
            // that reference. That event signifies that log out has completed, and will prompt a message from the bot to the user to indicate that fact.
            var conversationRef = context.Activity.ToConversationReference();

            var options = AuthenticationHelper.GetDefaultOffice365Otions();
            options.RedirectUrl = $"{ConfigurationManager.AppSettings["PostLogoutUrl"]}?conversationRef={UrlToken.Encode(conversationRef)}";

            await new MSALAuthProvider().Logout(options, context);
        }

    }
}