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
using SharePointBot.UnitTests.Mocks;
using SharePointBot.UnitTests.Dialogs;

namespace SharePointBot.UnitTests.Conversations
{
    [TestClass]
    public class RootDialogTests : SharePointBotDialogTestBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Conversation_LoginLogoutSequence()
        {
            var authService = new AuthenticationServiceMock(true);
            var spBotStateServiceMock = new Mock<ISharePointBotStateService>();
            var spServiceMock = new Mock<ISharePointService>();

            using (new FiberTestBase.ResolveMoqAssembly(authService, spBotStateServiceMock.Object, spServiceMock.Object))
            {
                // Create the container which will be used when testing this conversation.
                using (_container = Build(Options.ResolveDialogFromContainer, authService, spBotStateServiceMock.Object, spServiceMock.Object))
                {
                    RegisterDependencies(_container, authService, spBotStateServiceMock, spServiceMock);

                    // Start new conversation with new user.
                    _conversationId = Guid.NewGuid();
                    _user = Guid.NewGuid().ToString();

                    // Ensure root dialog is captured.
                    _makeRoot = () => _container.Resolve<RootDialog>();

                    await SendTextAndAssertResponse(
                        "login",
                        Constants.Responses.LogIntoWhichSiteCollection);

                    // "Last" isn't a valid response at this point.
                    await SendTextAndAssertResponse(
                        "last",
                        Constants.Responses.InvalidSiteCollectionUrl);

                    await SendTextAndAssertResponse(
                        "login",
                       Constants.Responses.LogIntoWhichSiteCollection);

                    const string SiteCollectionUrl = "https://mytenant.sharepoint.com/sites/mysitecollection";

                    await SendTextAndAssertResponse(
                        SiteCollectionUrl,
                        Constants.Responses.LoggedIn);

                    await SendMessageNoResponse("logout");

                    // Last site collection stored in bot state, so prompt that user can re-use that one.
                    await SendTextAndAssertResponse(
                        "login",
                        Constants.Responses.LogIntoWhichSiteCollection + string.Format(Constants.Responses.LastSiteCollection, SiteCollectionUrl));

                    // Start new conversation with new user.
                    _conversationId = Guid.NewGuid();
                    _user = Guid.NewGuid().ToString();

                    // New user so last site collection won't be stored in bot state and bot won't prompt for prior site collection.
                    await SendTextAndAssertResponse(
                       "login",
                       Constants.Responses.LogIntoWhichSiteCollection);
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Conversation_LoginLogoutSequence_SimulateSkype()
        {
            var authService = new AuthenticationServiceMock(true);
            var spBotStateServiceMock = new Mock<ISharePointBotStateService>();
            var spServiceMock = new Mock<ISharePointService>();

            using (new FiberTestBase.ResolveMoqAssembly(authService, spBotStateServiceMock.Object, spServiceMock.Object))
            {
                // Create the container which will be used when testing this conversation.
                using (_container = Build(Options.ResolveDialogFromContainer, authService, spBotStateServiceMock.Object, spServiceMock.Object))
                {
                    RegisterDependencies(_container, authService, spBotStateServiceMock, spServiceMock);

                    // Start new conversation with new user.
                    _conversationId = Guid.NewGuid();
                    _user = Guid.NewGuid().ToString();

                    // Ensure root dialog is captured.
                    _makeRoot = () => _container.Resolve<RootDialog>();

                    await SendTextAndAssertResponse(
                        "login",
                        Constants.Responses.LogIntoWhichSiteCollection);

                    // "Last" isn't a valid response at this point.
                    await SendTextAndAssertResponse(
                        "last",
                        Constants.Responses.InvalidSiteCollectionUrl);

                    await SendTextAndAssertResponse(
                        "login",
                       Constants.Responses.LogIntoWhichSiteCollection);

                    const string SiteCollectionUrl = @"https://mytenant.sharepoint.com/sites/mysitecollection";
                    string SiteCollectionUrlSkypeified = $@"<a href=""{SiteCollectionUrl}"">{SiteCollectionUrl}</a>";

                    await SendTextAndAssertResponse(
                        SiteCollectionUrlSkypeified,
                        Constants.Responses.LoggedIn);

                    await SendMessageNoResponse("logout");

                    // Last site collection stored in bot state, so prompt that user can re-use that one.
                    await SendTextAndAssertResponse(
                        "login",
                        Constants.Responses.LogIntoWhichSiteCollection + string.Format(Constants.Responses.LastSiteCollection, SiteCollectionUrl));

                    // Start new conversation with new user.
                    _conversationId = Guid.NewGuid();
                    _user = Guid.NewGuid().ToString();

                    // New user so last site collection won't be stored in bot state and bot won't prompt for prior site collection.
                    await SendTextAndAssertResponse(
                       "login",
                       Constants.Responses.LogIntoWhichSiteCollection);
                }
            }

        }



        private void RegisterDependencies(
            IContainer container,
            IAuthenticationService authService,
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

            builder.Register(c => authService)
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
