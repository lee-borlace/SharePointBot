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
        public static readonly object LifetimeScopeTag = typeof(SharePointBotStateServiceModule);

        /// <summary>
        /// Build up a lifetime scope. The context parameter is used when instantiating related items.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ILifetimeScope BeginLifetimeScope(ILifetimeScope scope, IBotContext context)
        {
            // Use context for IBotContext constructor parameters.
            var inner = scope.BeginLifetimeScope(LifetimeScopeTag);
            inner.Resolve<IBotContext>(TypedParameter.From(context));
            return inner;
        }



        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SharePointBotStateService>().As<ISharePointBotStateService>();
        }
    }
}