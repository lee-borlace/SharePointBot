using BotAuth;
using BotAuth.AADv1;
using BotAuth.AADv2;
using BotAuth.Dialogs;
using BotAuth.Models;
using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SharePointBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace SharePointBot.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationOptions GetDefaultOffice365Options()
        {
            return new AuthenticationOptions()
            {
                Authority = "https://login.microsoftonline.com/common",
                ClientId = ConfigurationManager.AppSettings["MicrosoftAppId"],
                ClientSecret = ConfigurationManager.AppSettings["MicrosoftAppPassword"],
                Scopes = new string[] { "User.Read", "Sites.Read.All", "Sites.ReadWrite.All" },
            };
        }

        /// <summary>
        /// Forwards to BotAuth login dialog.
        /// </summary>
        /// <param name="tenantUrl">The tenant URL.</param>
        /// <param name="context">The context.</param>
        /// <param name="message">The message.</param>
        /// <param name="loginCallBack">The login call back.</param>
        /// <returns></returns>
        public async Task ForwardToBotAuthLoginDialog(string tenantUrl, IDialogContext context, IMessageActivity message, ResumeAfter<AuthResult> loginCallBack)
        {
            var options = GetDefaultOffice365Options();
            options.RedirectUrl = ConfigurationManager.AppSettings["aad:Callback"];
            options.ResourceId = tenantUrl;

            await context.Forward(new AuthDialog(new ADALAuthProvider(), options), loginCallBack, message, CancellationToken.None);
        }

        public async Task LogOut(IDialogContext context)
        {
            // We will store the conversation reference in the callback URL. When Office 365 logs out it will hit the LogOut endpoint and pass
            // that reference. That event signifies that log out has completed, and will prompt a message from the bot to the user to indicate that fact.
            var conversationRef = context.Activity.ToConversationReference();

            var options = GetDefaultOffice365Options();
            options.RedirectUrl = $"{ConfigurationManager.AppSettings["PostLogoutUrl"]}?conversationRef={UrlToken.Encode(conversationRef)}";

            // We need to know the resource ID. This *should be* stored in bot state from when user logged in.
            string lastSiteCollectionUrl = null;
            context.UserData.TryGetValue<string>(Constants.StateKeys.LastLoggedInSiteCollectionUrl, out lastSiteCollectionUrl);
            options.ResourceId = lastSiteCollectionUrl;


            await new ADALAuthProvider().Logout(options, context);
            
        }

        public async Task<AuthResult> GetAccessToken(IDialogContext context)
        {
            var options = GetDefaultOffice365Options();
            options.RedirectUrl = ConfigurationManager.AppSettings["aad:Callback"];

            // We need to know the resource ID. This should be stored in bot state from when user logged in.
            string lastTenantUrl = null;
            context.UserData.TryGetValue<string>(Constants.StateKeys.LastLoggedInTenantUrl, out lastTenantUrl);
            options.ResourceId = lastTenantUrl;

            return await new ADALAuthProvider().GetAccessToken(options, context);
        }
    }
}