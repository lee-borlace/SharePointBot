using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BotAuth.Models;
using System.Configuration;
using BotAuth.Dialogs;
using BotAuth.AADv2;
using System.Net.Http;
using BotAuth;
using System.Threading;
using System.Text.RegularExpressions;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            var activity = await result as Activity;
            var userToBot = activity.Text.ToLowerInvariant();

            var foundMatch = false;

            // Log in.
            var match = Regex.Match(userToBot, Constants.UtteranceRegexes.LOGIN);
            if (match.Success)
            {
                foundMatch = true;
                await Login(context, message);
            }

            // Log out.
            match = Regex.Match(userToBot, Constants.UtteranceRegexes.LOGOUT);
            if (match.Success)
            {
                foundMatch = true;
                await LogOut(context);
            }

            if (!foundMatch)
            {
                context.Wait(MessageReceivedAsync);
            }
        }

        private static async Task Login(IDialogContext context, object message)
        {
            // Initialize AuthenticationOptions and forward to AuthDialog for token
            AuthenticationOptions options = new AuthenticationOptions()
            {
                Authority = ConfigurationManager.AppSettings["aad:Authority"],
                ClientId = ConfigurationManager.AppSettings["MicrosoftAppId"],
                ClientSecret = ConfigurationManager.AppSettings["MicrosoftAppPassword"],
                Scopes = new string[] { "User.Read" },
                RedirectUrl = ConfigurationManager.AppSettings["aad:Callback"]
            };

            await context.Forward(new AuthDialog(new MSALAuthProvider(), options), async (IDialogContext authContext, IAwaitable<AuthResult> authResult) =>
            {
                var authResultAwaited = await authResult;

                // Use token to call into service
                var json = await new HttpClient().GetWithAuthAsync(authResultAwaited.AccessToken, "https://graph.microsoft.com/beta/sites/lee79.sharepoint.com:/sites/dev:/lists");
                await authContext.PostAsync("Made the call OK.");

            }, message, CancellationToken.None);
        }

        private static async Task LogOut(IDialogContext context)
        {
            AuthenticationOptions options = new AuthenticationOptions()
            {
                Authority = ConfigurationManager.AppSettings["aad:Authority"],
                ClientId = ConfigurationManager.AppSettings["MicrosoftAppId"],
                ClientSecret = ConfigurationManager.AppSettings["MicrosoftAppPassword"],
                Scopes = new string[] { "User.Read" },
                RedirectUrl = ConfigurationManager.AppSettings["PostLogoutUrl"]
            };

            await new MSALAuthProvider().Logout(options, context);
        }
    }
}