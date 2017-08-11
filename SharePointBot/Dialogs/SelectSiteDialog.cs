using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SharePointBot.Services;
using SharePointBot.Model;
using Autofac;
using SharePointBot.AutofacModules;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class SelectSiteDialog : IDialog<object>
    {
        protected string _siteTitleOrAlias;

        public SelectSiteDialog(string siteTitleOrAlias)
        {
            _siteTitleOrAlias = siteTitleOrAlias;
        }

        public async Task StartAsync(IDialogContext context)
        {
            using (var scope = Conversation.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ISharePointBotStateService>(new NamedParameter(Constants.FieldNames.BotContext, context));

                await service.SetCurrentSite(
                    new BotSite
                    {
                        Alias = _siteTitleOrAlias,
                        Id = Guid.NewGuid(),
                        Title = _siteTitleOrAlias,
                        Url = "/sites/whatevs"
                    }
                );
            }

            await context.PostAsync($"Site {_siteTitleOrAlias} selected.");

            context.Done("All done!");
        }
    }
}