using SharePointBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SharePointBot.UnitTests.Mocks
{
    /// <summary>
    /// Mocked IAuthenticationService.
    /// </summary>
    /// <seealso cref="SharePointBot.Services.Interfaces.IAuthenticationService" />
    public class AuthenticationServiceMock : IAuthenticationService
    {
        /// <summary>
        /// Instead of forwarding to authentication prompt and then returning, just calls the callback with a fake access token.
        /// </summary>
        /// <param name="tenantUrl">The tenant URL.</param>
        /// <param name="context">The context.</param>
        /// <param name="message">The message.</param>
        /// <param name="loginCallBack">The login call back.</param>
        /// <returns></returns>
        public async Task ForwardToBotAuthLoginDialog(string tenantUrl, IDialogContext context, IMessageActivity message, ResumeAfter<AuthResult> loginCallBack)
        {
            await loginCallBack(context, null);
        }

    public Task<AuthResult> GetAccessToken(IDialogContext context)
        {
            throw new NotImplementedException();
        }

        public AuthenticationOptions GetDefaultOffice365Options()
        {
            throw new NotImplementedException();
        }

        public Task LogOut(IDialogContext context)
        {
            throw new NotImplementedException();
        }
    }
}
