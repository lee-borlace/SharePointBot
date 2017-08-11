using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Services;
using SharePointBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot.AutofacModules
{
    /// <summary>
    /// Autofac module for services dependencies.
    /// </summary>
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SharePointBotStateService>().As<ISharePointBotStateService>();
            builder.RegisterType<SharePointService>().As<ISharePointService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
        }
    }
}