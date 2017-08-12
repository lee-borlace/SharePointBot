
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

        protected string _lastSiteCollectionUrl;

        public LogInDialog(IAuthenticationService authenticationService)
        {
            SetField.NotNull(out _authenticationService, nameof(_authenticationService), authenticationService);
        }

        public async Task StartAsync(IDialogContext context)
        {
            string prompt = Constants.Responses.LogIntoWhichTenant;
            _lastSiteCollectionUrl = null;
            var lastSiteCollectionUrlPresent = context.PrivateConversationData.TryGetValue<string>(Constants.StateKeys.LastLoggedInTenantUrl, out _lastSiteCollectionUrl);

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

            // User typed "last"
            if (Regex.IsMatch(userResponse, Constants.UtteranceRegexes.LastSiteCollectionUrl))
            {
                // Last URL is present - use it.
                if (!string.IsNullOrEmpty(_lastSiteCollectionUrl))
                {
                    context.PrivateConversationData.SetValue<string>(Constants.StateKeys.LastLoggedInTenantUrl, _lastSiteCollectionUrl);
                    await _authenticationService.ForwardToBotAuthLoginDialog(_lastSiteCollectionUrl, context, context.Activity as IMessageActivity, AfterLogOn);
                }
                // Last URL is not present - "last" isn't a valid response.
                else
                {
                    // TODO : Don't just quit here, instead allow X number of retries.
                    await context.PostAsync(Constants.Responses.InvalidTenantURL);
                    context.Done<AuthResult>(null);
                }
            }
            // User didn't type "last".
            else
            {
                if (Regex.IsMatch(userResponse, Constants.RegexMisc.SPOTenantUrl, RegexOptions.IgnoreCase))
                {
                    context.PrivateConversationData.SetValue<string>(Constants.StateKeys.LastLoggedInTenantUrl, userResponse);
                    await _authenticationService.ForwardToBotAuthLoginDialog(userResponse, context, context.Activity as IMessageActivity, AfterLogOn);
                }
                else
                {
                    // TODO : Don't just quit here, instead allow X number of retries.
                    await context.PostAsync(Constants.Responses.InvalidTenantURL);
                    context.Done<AuthResult>(null);
                }
            }
        }

        private async Task AfterLogOn(IDialogContext context, IAwaitable<AuthResult> result)
        {
            context.Done<AuthResult>(await result);
        }
    }
}