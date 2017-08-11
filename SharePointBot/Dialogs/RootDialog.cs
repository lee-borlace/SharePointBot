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
using Microsoft.Bot.Builder.ConnectorEx;
using Autofac;
using SharePointBot.AutofacModules;
using SharePointBot.Model;
using SharePointBot.Services.Interfaces;

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
            using (var scope = Conversation.Container.BeginLifetimeScope())
            {

                var message = await result;
                var activity = await result as Activity;
                var userToBot = activity.Text.ToLowerInvariant();

                var foundMatch = false;

                // Log in.
                var match = Regex.Match(userToBot, Constants.UtteranceRegexes.Login);
                if (match.Success)
                {
                    foundMatch = true;
                    await scope.Resolve<IAuthenticationService>().ForwardToLoginDialog(context, message, LoginCallBack);
                }

                // Log out.
                match = Regex.Match(userToBot, Constants.UtteranceRegexes.LogOut);
                if (match.Success)
                {
                    foundMatch = true;
                    await scope.Resolve<IAuthenticationService>().LogOut(context);
                }

                // Select site.
                match = Regex.Match(userToBot, Constants.UtteranceRegexes.SelectSite);
                if (match.Success)
                {
                    foundMatch = true;

                    var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

                    context.Call(scope.Resolve<SelectSiteDialog>(new NamedParameter(Constants.FieldNames.SiteTitleOrAlias, siteTitleOrAlias)), ReturnFromDialog);
                }

                // What is current site.
                match = Regex.Match(userToBot, Constants.UtteranceRegexes.WhatIsCurrentSite);
                if (match.Success)
                {
                    foundMatch = true;
                    context.Call(scope.Resolve<GetSiteDialog>(), ReturnFromDialog);
                }

                if (!foundMatch)
                {
                    context.Wait(MessageReceivedAsync);
                }
            }
        }

        private async Task ReturnFromDialog(IDialogContext context, IAwaitable<BotSite> result)
        {
            var dialogResult = await result;
            context.Wait(MessageReceivedAsync);
        }


        private async Task LoginCallBack(IDialogContext authContext, IAwaitable<AuthResult> authResult)
        {
            var authResultAwaited = await authResult;
            var json = await new HttpClient().GetWithAuthAsync(authResultAwaited.AccessToken, "https://graph.microsoft.com/beta/sites/lee79.sharepoint.com:/sites/dev:/lists");
            await authContext.PostAsync("Made the call OK.");
        }
    }
}