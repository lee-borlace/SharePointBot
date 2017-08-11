using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBot.Services.Interfaces
{
    interface IAuthenticationService
    {
        AuthenticationOptions GetDefaultOffice365Options();
        Task ForwardToLoginDialog(IDialogContext context, object message, ResumeAfter<AuthResult> loginCallBack);
        Task LogOut(IDialogContext context);
    }
}
