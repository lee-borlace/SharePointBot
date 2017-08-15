using Autofac;
using Autofac.Core;
using Microsoft.Bot.Builder.Dialogs;
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
            builder.RegisterType<GetSiteDialog>().AsSelf();
            builder.RegisterType<SelectSiteDialog>().AsSelf();

            builder.RegisterType<SharePointBotStateService>().As<ISharePointBotStateService>();
            builder.RegisterType<SharePointService>().As<ISharePointService>().SingleInstance();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();

            builder.RegisterType<LogInDialog>().AsSelf();
            builder.Register((c, p) => new LogInDialog(c.Resolve<IAuthenticationService>(), c.Resolve<ISharePointService>())).AsSelf().InstancePerDependency();
        }
    }
}