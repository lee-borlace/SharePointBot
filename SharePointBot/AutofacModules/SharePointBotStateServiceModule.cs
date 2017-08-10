using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot.AutofacModules
{
    /// <summary>
    /// Autofac module for SharePointBotStateService dependencies.
    /// </summary>
    public class SharePointBotStateServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SharePointBotStateService>();
        }
    }
}