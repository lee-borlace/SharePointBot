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
using Microsoft.Bot.Builder.Dialogs.Internals;
using SharePointBot.Utility;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class SelectSiteDialog : IDialog<BotSite>
    {
        /// <summary>
        /// Title or alias of site to select.
        /// </summary>
        public string SiteTitleOrAlias;

        protected IAuthenticationService _authenticationService;

        protected ISharePointService _sharePointService;

        private ISharePointBotStateService _sharePointBotStateService;

        private GetSiteDialog _getSiteDialog;

        protected LogInDialog _logInDialog;

        /// <summary>
        /// The resolved site.
        /// </summary>
        protected BotSite _site;

        public SelectSiteDialog(
            IAuthenticationService authenticationService,
            ISharePointService sharePointService,
            ISharePointBotStateService sharePointBotStateService,
            GetSiteDialog getSiteDialog,
            LogInDialog logInDialog)
        {
            _authenticationService = authenticationService;
            _sharePointService = sharePointService;
            _sharePointBotStateService = sharePointBotStateService;
            _logInDialog = logInDialog;
            _getSiteDialog = getSiteDialog;
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
                context.Call(_logInDialog, AfterLogOn);
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
            if (await result != null)
            {
                await SelectSite(context);
            }
            else
            {
                await context.PostAsync(Constants.Responses.LogInFailed);
                context.Done<BotSite>(null);
            }

        }


        /// <summary>
        /// Select site.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task SelectSite(IDialogContext context)
        {
            // If title / alias was specified when opening the dialog, check it and store it.
            if (!string.IsNullOrEmpty(SiteTitleOrAlias))
            {
                await GetSpecifiedSite(context);
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


        private async Task StoreAndFinish(IDialogContext context)
        {
            if (_site != null)
            {
                await StoreSiteInBotStateAndFinaliseDialog(context);
            }
            else
            {
                await context.PostAsync(Constants.Responses.CouldntFindSite);
                context.Done<BotSite>(null);
            }
        }



        /// <summary>
        /// Prompted for and received site name / alias. Proceed with checking.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task AfterGetSiteFromInput(IDialogContext ctx, IAwaitable<string> result)
        {
            SiteTitleOrAlias = await result;
            await GetSpecifiedSite(ctx);
            await StoreSiteInBotStateAndFinaliseDialog(ctx);
        }


        /// <summary>
        /// Try to get the specified site.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GetSpecifiedSite(IDialogContext context)
        {
            var sites = await _sharePointService.SearchForWeb(SiteTitleOrAlias, await _authenticationService.GetAccessToken(context), context);

            // Exactly one match.
            if (sites.Count == 1)
            {
                _site = sites[0];
                await StoreAndFinish(context);
            }
            // No match.
            else if (sites.Count == 0)
            {
                _site = null;
                await StoreAndFinish(context);
            }
            // Multiple matches - need to clarify further.
            else
            {
                var choose = new PromptDialog.PromptChoice<BotSite>(
                   sites,
                   Constants.Responses.ChooseSite,
                   Constants.Responses.DidntUnderstand + Constants.Responses.PleaseChooseAnOption,
                   Constants.Misc.DialogAttempts,
                   descriptions: sites.Select(s => $"{s.Title} ({UrlUtility.GetServerRelativeUrl(s.Url)})"),
                   promptStyle: PromptStyle.Auto
                );

                context.Call<BotSite>(choose, AfterChoiceSelected);
            }

        }

      

        private async Task AfterChoiceSelected(IDialogContext context, IAwaitable<BotSite> result)
        {
            _site = await result;
            await StoreAndFinish(context);
        }

        /// <summary>
        /// Store the selected site in bot state, return from dialog.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task StoreSiteInBotStateAndFinaliseDialog(IDialogContext context)
        {
            if (_site != null)
            {
                _sharePointBotStateService.BotContext = context;
                await _sharePointBotStateService.SetCurrentSite(_site);

                // Display the current site, then return from dialog.
                context.Call(_getSiteDialog, ReturnFromGetSiteDialog);
            }
            else
            {
                context.Done<BotSite>(null);
            }
        }

        /// <summary>
        /// Return from the get site dialog which is called at the end of this dialog.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task ReturnFromGetSiteDialog(IDialogContext context, IAwaitable<BotSite> result)
        {
            context.Done(_site);
        }
    }
}