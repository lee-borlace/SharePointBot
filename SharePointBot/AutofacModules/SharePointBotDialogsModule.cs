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
    public class SharePointBotDialogsModule : Module
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
                .Keyed<LogInDialog>(FiberModule.Key_DoNotSerialize)
                .AsSelf()
                .InstancePerDependency();

            builder
               .RegisterType<GetSiteDialog>()
               .Keyed<GetSiteDialog>(FiberModule.Key_DoNotSerialize)
               .AsSelf()
               .InstancePerDependency();

            builder
               .RegisterType<SelectSiteDialog>()
               .Keyed<SelectSiteDialog>(FiberModule.Key_DoNotSerialize)
               .AsSelf()
               .InstancePerDependency();
        }
    }
}