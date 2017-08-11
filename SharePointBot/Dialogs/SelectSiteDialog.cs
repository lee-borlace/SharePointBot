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
            if (!string.IsNullOrEmpty(_siteTitleOrAlias))
            {
                await StoreSelectedSiteInBotState(context);
            }
            else
            {
                PromptDialog.Text(
                   context,
                   this.AfterSiteSpecified,
                   Constants.Responses.SelectWhichSite,
                   attempts: 3
               );
            }

            await context.PostAsync($"Site {_siteTitleOrAlias} selected.");

            context.Done("All done!");
        }

        private async Task AfterSiteSpecified(IDialogContext context, IAwaitable<string> result)
        {
            _siteTitleOrAlias = await result;
            await CheckSpecifiedSite(context);
        }

        /// <summary>
        /// Check whether specified site matches the title of a SharePoint site or the alias of a previously-favourited site.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Task CheckSpecifiedSite(IDialogContext context)
        {
            throw new NotImplementedException();
        }

        private async Task StoreSelectedSiteInBotState(IDialogContext context)
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
        }
    }
}