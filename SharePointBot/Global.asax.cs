using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SharePointBot.AutofacModules;
using SharePointBot.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SharePointBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RegisterWebApiDependencies();
            RegisterBotDependencies();
        }


        /// <summary>
        /// Register global dependencies for Web API.
        /// </summary>
        private static void RegisterWebApiDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DialogModule());
            builder.RegisterModule(new SharePointBotModule());


#if DEBUG
#else  
            builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));

            builder.RegisterModule(new TableLoggerModule(
              CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString),
              Constants.Azure.TableNameActivityLogging));
#endif

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var config = GlobalConfiguration.Configuration;
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }



        /// <summary>
        /// Register specific dependencies for bot.
        /// </summary>
        private void RegisterBotDependencies()
        {
            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterModule(new SharePointBotModule());

                builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));


#if DEBUG
#else                
                // TODO : See issue #1 - this causes issues when commented in. Need to work out how to use Azure Storage.

                //builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));

                //var store = new TableBotDataStore(ConfigurationManager.AppSettings["StorageConnectionString"]);

                //builder.Register(c => store)
                //    .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                //    .AsSelf()
                //    .SingleInstance();
#endif
            });
        }
    }
}
