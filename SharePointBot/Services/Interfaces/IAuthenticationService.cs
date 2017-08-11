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

        Task ForwardToLoginDialog(IDialogContext context, IMessageActivity message, ResumeAfter<AuthResult> loginCallBack);

        Task LogOut(IDialogContext context);

        Task<AuthResult> GetAccessToken(IDialogContext context);
    }
}
