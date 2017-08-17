using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using BotAuth.Models;
using System.Configuration;
using BotAuth.Dialogs;
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
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Linq;

namespace SharePointBot.Dialogs
{
    [LuisModel("7716c1d3-40ea-4f10-8397-956c37074e70", "41f72c548e2a42a1b5d900c9ccf2d4fe")]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        private LogInDialog _loginDialog;
        private SelectSiteDialog _selectSiteDialog;
        private GetSiteDialog _getSiteDialog;
        private IAuthenticationService _authenticationService;

        public RootDialog(
            LogInDialog loginDialog, 
            SelectSiteDialog selectSiteDialog, 
            GetSiteDialog getSiteDialog,
            IAuthenticationService authenticationService)
        {
            _loginDialog = loginDialog;
            _selectSiteDialog = selectSiteDialog;
            _getSiteDialog = getSiteDialog;
            _authenticationService = authenticationService;
        }


        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Constants.Responses.DontUnderstand);
            context.Wait(MessageReceived);
        }


        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(Constants.Responses.Greeting);
            context.Wait(MessageReceived);
        }

        [LuisIntent("LogIn")]
        public async Task LogIn(IDialogContext context, LuisResult result)
        {
            context.Call(_loginDialog, Callback);
        }

        [LuisIntent("LogOut")]
        public async Task LogOut(IDialogContext context, LuisResult result)
        {
            await _authenticationService.LogOut(context);
            context.Wait(MessageReceived);
        }

        [LuisIntent("GetCurrentSite")]
        public async Task GetCurrentSite(IDialogContext context, LuisResult result)
        {
            context.Call(_getSiteDialog, Callback);
        }

        [LuisIntent("SelectSite")]
        public async Task SelectSite(IDialogContext context, LuisResult result)
        {
            string siteTitleOrAlias = null;

            foreach (var entity in result.Entities.Where(Entity => Entity.Type == Constants.LuisEntityNames.SiteTitleOrAlias))
            {
                siteTitleOrAlias = entity.Entity;
            }

            _selectSiteDialog.SiteTitleOrAlias = siteTitleOrAlias;

            context.Call(_selectSiteDialog, Callback);
        }


        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}