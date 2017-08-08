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

            var match = Regex.Match(userToBot, Constants.UtteranceRegexes.LOGIN);
            if (match.Success)
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
                    //await authContext.PostAsync($"I'm a simple bot that doesn't do much, but I know your name is {json.Value<string>("displayName")} and your UPN is {json.Value<string>("userPrincipalName")}");

                    await authContext.PostAsync("Made the call OK.");

                }, message, CancellationToken.None);
            }
            else
            {
                context.Wait(MessageReceivedAsync);
            }
        }
    }
}