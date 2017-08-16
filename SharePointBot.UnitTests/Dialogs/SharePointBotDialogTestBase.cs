using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Tests;
using Microsoft.Bot.Connector;
using Moq;
using SharePointBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBot.UnitTests.Dialogs
{
    /// <summary>
    /// Base class for dialog tests for this bot.
    /// </summary>
    /// <seealso cref="Microsoft.Bot.Builder.Tests.DialogTestBase" />
    public class SharePointBotDialogTestBase : DialogTestBase
    {
        // TODO : Put this back in if I ever work out how to call a supplied delegate parameter from mocked call!
        //protected Mock<IAuthenticationService> GetMockedAuthenticationService()
        //{
        //    var mockedService = new Mock<IAuthenticationService>();

        //    mockedService.Setup(s => s.ForwardToBotAuthLoginDialog(
        //        It.IsAny<string>(),
        //        It.IsAny<IDialogContext>(),
        //        It.IsAny<IMessageActivity>(),
        //        It.IsAny<ResumeAfter<AuthResult>>()
        //        )).Callback(() => {  });

        //    return mockedService;
        //}


    }
}
