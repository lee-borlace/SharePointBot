using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading;
using Microsoft.Bot.Builder.Tests;
using Autofac;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Base;
using System.Collections.Generic;
using Moq;
using SharePointBot.Services.Interfaces;
using SharePointBot.Services;

namespace SharePointBot.UnitTests.Dialogs
{
    [TestClass]
    public class RootDialogTests : DialogTestBase
    {
        //[TestMethod]
        //public async Task ShouldReturnEcho()
        //{
        //    // Instantiate dialog to test
        //    IDialog<object> rootDialog = new RootDialog(new LogInDialog(new AuthenticationService(), new SharePointService()));

        //    // Create in-memory bot environment
        //    Func<IDialog<object>> MakeRoot = () => rootDialog;

        //    using (new FiberTestBase.ResolveMoqAssembly(rootDialog))
        //    {
        //        using (var container = Build(Options.MockConnectorFactory | Options.ScopedQueue, rootDialog))
        //        {
        //            // Create a message to send to bot
        //            var toBot = DialogTestBase.MakeTestMessage();
        //            toBot.From.Id = Guid.NewGuid().ToString();
        //            toBot.Text = "login";

        //            // Send message and check the answer.
        //            IMessageActivity toUser = await GetResponse(container, MakeRoot, toBot);

        //            Assert.AreEqual(Constants.Responses.LogIntoWhichSiteCollection, toUser.Text);
        //        }
        //    }
        //}



        [TestMethod]
        public async Task RootDialogTest1()
        {
            var authService = new Mock<IAuthenticationService>();
            var spBotStateService = new Mock<ISharePointBotStateService>();
            var spService = new Mock<ISharePointService>();

            using (new FiberTestBase.ResolveMoqAssembly(authService.Object, spBotStateService.Object, spService.Object))
            {
                using (var container = Build(Options.ResolveDialogFromContainer, authService.Object, spBotStateService.Object, spService.Object))
                {

                }
            }

        }

    }
}
