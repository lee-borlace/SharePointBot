using Autofac;
using Autofac.Core;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Dialogs;

namespace SharePointBot.AutofacModules
{
    internal class RootDialogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RootDialog>().As<IDialog<object>>().InstancePerDependency();
        }
    }
}