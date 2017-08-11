using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SharePointBot.Services;
using SharePointBot.Model;
using Autofac;
using SharePointBot.AutofacModules;
using SharePointBot.Services.Interfaces;
using Microsoft.Bot.Builder.Internals.Fibers;
using BotAuth.Models;
using Microsoft.Bot.Connector;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class SelectSiteDialog : IDialog<BotSite>
    {
        /// <summary>
        /// Title or alias of site to select.
        /// </summary>
        protected string _siteTitleOrAlias;

        [NonSerialized]
        protected IAuthenticationService _authenticationService;

        [NonSerialized]
        protected ISharePointService _sharePointService;

        /// <summary>
        /// The resolved site.
        /// </summary>
        protected BotSite _site;

        public SelectSiteDialog(string siteTitleOrAlias, IAuthenticationService authenticationService, ISharePointService sharePointService)
        {
            SetField.NotNull(out _siteTitleOrAlias, nameof(_siteTitleOrAlias), siteTitleOrAlias);
            SetField.NotNull(out _authenticationService, nameof(_authenticationService), authenticationService);
            SetField.NotNull(out _sharePointService, nameof(_sharePointService), sharePointService);
        }

        /// <summary>
        /// Starting point of dialog.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task StartAsync(IDialogContext context)
        {
            // Make sure we have an access token before trying to select site.
            var accessToken = await _authenticationService.GetAccessToken(context);

            // No access token - redirect to login dialog first.
            if (accessToken == null)
            {
                await context.PostAsync(Constants.Responses.LogOnFirst);
                await _authenticationService.ForwardToLoginDialog(context, context.Activity as IMessageActivity, AfterLogOn);
            }
            else
            {
                await SelectSite(context);
            }
        }

        /// <summary>
        /// User has logged on. Continue with the dialog.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task AfterLogOn(IDialogContext context, IAwaitable<AuthResult> result)
        {
            await SelectSite(context);
        }


        /// <summary>
        /// Select site.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task SelectSite(IDialogContext context)
        {
            // If title / alias was specified when opening the dialog, check it and store it.
            if (!string.IsNullOrEmpty(_siteTitleOrAlias))
            {
                await GetSpecifiedSite(context);
                await StoreSiteInBotState(context);
            }
            // Otherwise prompt, then check and store.
            else
            {
                PromptDialog.Text(
                   context,
                   this.AfterGetSiteFromInput,
                   Constants.Responses.SelectWhichSite,
                   attempts: Constants.Misc.DialogAttempts
               );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task AfterGetSiteFromInput(IDialogContext ctx, IAwaitable<string> result)
        {
            _siteTitleOrAlias = await result;
            await GetSpecifiedSite(ctx);
            await StoreSiteInBotState(ctx);
        }


        /// <summary>
        /// Try to get the specified site.If it doesn't exist, trigger a dialog to narrow the source.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GetSpecifiedSite(IDialogContext context)
        {
            _site = new BotSite
            {
                Alias = _siteTitleOrAlias,
                Id = Guid.NewGuid(),
                Title = _siteTitleOrAlias,
                Url = "/sites/whatevs"
            };
        }

        /// <summary>
        /// Store the selected site in bot state, return from dialog.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task StoreSiteInBotState(IDialogContext context)
        {
            using (var scope = Conversation.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ISharePointBotStateService>(new NamedParameter(Constants.FieldNames.BotContext, context));
                await service.SetCurrentSite(_site);

                // Display the current site, then return from dialog.
                context.Call(scope.Resolve<GetSiteDialog>(), ReturnFromGetSiteDialog);
            }
        }

        private async Task ReturnFromGetSiteDialog(IDialogContext context, IAwaitable<BotSite> result)
        {
            context.Done(_site);
        }
    }
}