﻿using System;
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
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs.Internals;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private LogInDialog _loginDialog;

        public RootDialog(LogInDialog loginDialog)
        {
            _loginDialog = loginDialog;
        }


        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            var activity = await result as Activity;

            using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
            {
                var userToBot = activity.Text.ToLowerInvariant();

                var foundMatch = false;

                // Log in.
                var match = Regex.Match(userToBot, Constants.UtteranceRegexes.Login);
                if (match.Success)
                {
                    foundMatch = true;
                    context.Call(_loginDialog, LoginCallBack);
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
        }
    }
}