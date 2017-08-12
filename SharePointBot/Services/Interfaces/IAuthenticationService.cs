using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBot.Services.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationOptions GetDefaultOffice365Options();

        /// <summary>
        /// Forwards to BotAuth login dialog.
        /// </summary>
        /// <param name="siteCollectionUrl">The site collection URL.</param>
        /// <param name="context">The context.</param>
        /// <param name="message">The message.</param>
        /// <param name="loginCallBack">The login call back.</param>
        /// <returns></returns>
        Task ForwardToBotAuthLoginDialog(string siteCollectionUrl, IDialogContext context, IMessageActivity message, ResumeAfter<AuthResult> loginCallBack);

        Task LogOut(IDialogContext context);

        Task<AuthResult> GetAccessToken(IDialogContext context);
    }
}
