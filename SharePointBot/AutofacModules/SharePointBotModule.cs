using Autofac;
using Autofac.Core;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Luis;
using SharePointBot.Dialogs;
using SharePointBot.Services;
using SharePointBot.Services.Interfaces;
using System.Configuration;

namespace SharePointBot.AutofacModules
{
    /// <summary>
    /// Module for resolving various dialogs.
    /// </summary>
    public class SharePointBotModule : Module
    {
        private string _luisModelId;
        private string _luisSubscriptionKey;

        public SharePointBotModule(string luisModelId, string luisSubscriptionKey)
        {
            _luisModelId = luisModelId;
            _luisSubscriptionKey = luisSubscriptionKey;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new LuisModelAttribute(
                _luisModelId,
                _luisSubscriptionKey)).AsSelf().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<LuisService>().Keyed<ILuisService>(FiberModule.Key_DoNotSerialize).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerDependency();

            builder.RegisterType<SharePointBotStateService>()
                .Keyed<ISharePointBotStateService>(FiberModule.Key_DoNotSerialize)
                .As<ISharePointBotStateService>();

            builder.RegisterType<SharePointService>()
                .Keyed<ISharePointService>(FiberModule.Key_DoNotSerialize)
                .As<ISharePointService>().SingleInstance();

            builder.RegisterType<AuthenticationService>()
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
        }
    }
}