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
using Microsoft.Bot.Builder.Internals.Fibers;

namespace SharePointBot.UnitTests.Dialogs
{
    [TestClass]
    public class RootDialogTests : DialogTestBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RootDialog_Conversation_Login()
        {
            var authService = new Mock<IAuthenticationService>();
            var spBotStateService = new Mock<ISharePointBotStateService>();
            var spService = new Mock<ISharePointService>();

            using (new FiberTestBase.ResolveMoqAssembly(authService.Object, spBotStateService.Object, spService.Object))
            {
                // Create the container which will be used when testing this conversation.
                using (_container = Build(Options.ResolveDialogFromContainer, authService.Object, spBotStateService.Object, spService.Object))
                {
                    RegisterDependencies(_container, authService, spBotStateService, spService);

                    // Create common conversation ID to use for this conversation.
                    _conversationId = Guid.NewGuid();

                    await SendTextAndAssertResponse(
                        "login",
                        Constants.Responses.LogIntoWhichSiteCollection);

                    await SendTextAndAssertResponse(
                        "dasdsadasdsaddsa",
                        Constants.Responses.InvalidSiteCollectionUrl);

                    await SendTextAndAssertResponse(
                        "login",
                       Constants.Responses.LogIntoWhichSiteCollection);

                    //await SendTextAndAssertResponse(
                    //    "https://mytenant.sharepoint.com",
                    //    Constants.Responses.LogIntoWhichSiteCollection);
                }
            }

        }



        private void RegisterDependencies(
            IContainer container,
            Mock<IAuthenticationService> authService,
            Mock<ISharePointBotStateService> spBotStateService,
            Mock<ISharePointService> spService)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerDependency();

            builder.Register(c => spBotStateService.Object)
                .Keyed<ISharePointBotStateService>(FiberModule.Key_DoNotSerialize)
                .As<ISharePointBotStateService>();

            builder.Register(c => spService.Object)
                .Keyed<ISharePointService>(FiberModule.Key_DoNotSerialize)
                .As<ISharePointService>().SingleInstance();

            builder.Register(c => authService.Object)
                .Keyed<IAuthenticationService>(FiberModule.Key_DoNotSerialize)
                .As<IAuthenticationService>().SingleInstance();

            builder
                .RegisterType<LogInDialog>()
                .AsSelf()
                .InstancePerDependency();

            builder
               .RegisterType<GetSiteDialog>()
               .AsSelf()
               .InstancePerDependency();

            builder
               .RegisterType<SelectSiteDialog>()
               .AsSelf()
               .InstancePerDependency();


            builder.Update(container);
        }

    }
}
