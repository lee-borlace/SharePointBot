
using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using SharePointBot.Services.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class LogInDialog : IDialog<AuthResult>
    {
        protected IAuthenticationService _authenticationService;
        protected ISharePointService _sharePointService;

        protected string _lastSiteCollectionUrl;

        public LogInDialog(IAuthenticationService authenticationService, ISharePointService sharePointService)
        {
            SetField.NotNull(out _authenticationService, nameof(_authenticationService), authenticationService);
            SetField.NotNull(out _sharePointService, nameof(_sharePointService), sharePointService);
        }

        public async Task StartAsync(IDialogContext context)
        {
            // Build up prompt depending on whether previous site collection URL is recorded in state.
            string prompt = Constants.Responses.LogIntoWhichSiteCollection;
            _lastSiteCollectionUrl = null;
            var lastSiteCollectionUrlPresent = context.PrivateConversationData.TryGetValue<string>(Constants.StateKeys.LastLoggedInSiteCollectionUrl, out _lastSiteCollectionUrl);

            if (lastSiteCollectionUrlPresent)
            {
                prompt += string.Format(Constants.Responses.LastSiteCollection, _lastSiteCollectionUrl);
            }

            PromptDialog.Text(
                context,
                this.AfterGetSiteCollectionUrl,
                prompt,
                attempts: Constants.Misc.DialogAttempts
            );
        }

        /// <summary>
        /// User has specified site collection.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private async Task AfterGetSiteCollectionUrl(IDialogContext context, IAwaitable<string> result)
        {
            var userResponse = await result;

            var valid = false;

            string siteCollectionUrl = string.Empty;

            // User typed "last"
            if (Regex.IsMatch(userResponse, Constants.UtteranceRegexes.LastSiteCollectionUrl))
            {
                // Last URL is present - use it.
                if (!string.IsNullOrEmpty(_lastSiteCollectionUrl))
                {
                    valid = true;
                    siteCollectionUrl = _lastSiteCollectionUrl;
                }
            }
            // User didn't type "last".
            else
            {
                if (Regex.IsMatch(userResponse, Constants.RegexMisc.SiteCollectionUrl, RegexOptions.IgnoreCase))
                {
                    valid = true;
                    siteCollectionUrl = userResponse;
                }
            }

            if (valid)
            {
                context.PrivateConversationData.SetValue<string>(Constants.StateKeys.LastLoggedInSiteCollectionUrl, siteCollectionUrl);

                var tenantUrl = _sharePointService.GetTenantUrlFromSiteCollectionUrl(siteCollectionUrl);
                context.PrivateConversationData.SetValue<string>(Constants.StateKeys.LastLoggedInTenantUrl, tenantUrl);

                await _authenticationService.ForwardToBotAuthLoginDialog(tenantUrl, context, context.Activity as IMessageActivity, AfterLogOn);
            }
            else
            {
                // TODO : Don't just quit here, instead allow X number of retries.
                await context.PostAsync(Constants.Responses.InvalidSiteCollectionUrl);
                context.Done<AuthResult>(null);
            }
        }

        private async Task AfterLogOn(IDialogContext context, IAwaitable<AuthResult> result)
        {
            context.Done<AuthResult>(await result);
        }
    }
}