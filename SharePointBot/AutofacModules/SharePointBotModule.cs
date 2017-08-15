using Autofac;
using Autofac.Core;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using SharePointBot.Dialogs;
using SharePointBot.Services;
using SharePointBot.Services.Interfaces;

namespace SharePointBot.AutofacModules
{
    /// <summary>
    /// Module for resolving various dialogs.
    /// </summary>
    public class SharePointBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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