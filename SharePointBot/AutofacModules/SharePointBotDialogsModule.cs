using Autofac;
using Autofac.Core;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Dialogs;

namespace SharePointBot.AutofacModules
{
    /// <summary>
    /// Module for resolving various dialogs.
    /// </summary>
    internal class SharePointBotDialogsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerDependency();
            builder.RegisterType<GetSiteDialog>().AsSelf();
            builder.RegisterType<SelectSiteDialog>().AsSelf();
            builder.RegisterType<LogInDialog>().AsSelf();
        }
    }
}