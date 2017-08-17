using Autofac;
using Autofac.Core;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Luis;
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

            builder.Register(c => new LuisModelAttribute("7716c1d3-40ea-4f10-8397-956c37074e70", "41f72c548e2a42a1b5d900c9ccf2d4fe")).AsSelf().AsImplementedInterfaces().SingleInstance();
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