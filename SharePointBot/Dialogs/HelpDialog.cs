using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SharePointBot.Services.Interfaces;
using BotAuth.Models;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class HelpDialog : IDialog<object>
    {
        private LogInDialog _loginDialog;
        private SelectSiteDialog _selectSiteDialog;
        private GetSiteDialog _getSiteDialog;

        private IAuthenticationService _authenticationService;
        private ISharePointBotStateService _sharePointBotStateService;

        public HelpDialog(
            LogInDialog loginDialog,
            SelectSiteDialog selectSiteDialog,
            GetSiteDialog getSiteDialog,
            IAuthenticationService authenticationService,
            ISharePointBotStateService sharePointBotStateService)
        {
            _authenticationService = authenticationService;
            _sharePointBotStateService = sharePointBotStateService;
            _loginDialog = loginDialog;
            _selectSiteDialog = selectSiteDialog;
            _getSiteDialog = getSiteDialog;
        }

        public async Task StartAsync(IDialogContext context)
        {
            //PromptDialog.Choice(
            //  context,
            //  AfterChoiceSelected,
            //  await GetValidOptions(context),
            //  Constants.Responses.ICanHelpWith,
            //  Constants.Responses.DontUnderstand + " Please choose one of the options below.",
            //  attempts: Constants.Misc.DialogAttempts);

            var options = await GetValidOptions(context);

            var descriptions = new List<string>();
            int index = 1;
            foreach (var option in options)
            {
                descriptions.Add($"{index++}: {option}");
            }

            var choose = new PromptDialog.PromptChoice<string>(
                  await GetValidOptions(context),
                  Constants.Responses.ICanHelpWith,
                  Constants.Responses.DidntUnderstand + Constants.Responses.PleaseChooseAnOption,
                  Constants.Misc.DialogAttempts,
                  promptStyle: PromptStyle.Auto,
                  descriptions: descriptions
               );

            context.Call<string>(choose, AfterChoiceSelected);

        }

        /// <summary>
        /// Get valid options based on current state of bot.
        /// </summary>
        /// <returns></returns>
        protected async Task<IEnumerable<string>> GetValidOptions(IDialogContext context)
        {
            var options = new List<string>();

            // Can log in or out.
            var accessToken = await _authenticationService.GetAccessToken(context);
            if (accessToken != null)
            {
                options.Add(Constants.Choices.LogOut);

                // If logged in, can select site and find out the current site.
                options.Add(Constants.Choices.SelectSite);
                options.Add(Constants.Choices.GetCurrentSite);

                // If a site is selected, can select list.
                _sharePointBotStateService.BotContext = context;
                if (await _sharePointBotStateService.GetCurrentSite() != null)
                {
                    options.Add(Constants.Choices.SelectList);
                }
            }
            else
            {
                options.Add(Constants.Choices.LogIn);
            }

            return options;
        }

        private async Task AfterChoiceSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var selectedChoice = await result;

                switch (selectedChoice)
                {
                    case Constants.Choices.LogIn:
                        context.Call(_loginDialog, AfterCallDialog);
                        break;
                    case Constants.Choices.SelectSite:
                        context.Call(_selectSiteDialog, AfterCallDialog);
                        break;
                    case Constants.Choices.GetCurrentSite:
                        context.Call(_getSiteDialog, AfterCallDialog);
                        break;
                    case Constants.Choices.SelectList:
                        await context.PostAsync(Constants.Responses.NotImplemented);
                        context.Done<object>(null);
                        break;
                    default:
                        context.Done<object>(null);
                        break;

                }
            }
            catch (TooManyAttemptsException)
            {
                await this.StartAsync(context);
            }
        }

        private async Task AfterCallDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }
    }
}