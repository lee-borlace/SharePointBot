using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SharePointBot.Services;
using SharePointBot.Model;
using Autofac;
using Microsoft.Bot.Builder.Internals.Fibers;
using SharePointBot.AutofacModules;

namespace SharePointBot.Dialogs
{
    [Serializable]
    public class GetSiteDialog : AutofacDialog, IDialog<object>
    {
        public GetSiteDialog(ILifetimeScope scope) : base(scope) { }

        public async Task StartAsync(IDialogContext context)
        {
            BotSite currentSite = null;

            using (var scope = SharePointBotStateServiceModule.BeginLifetimeScope(this._dialogScope, context))
            {
                var service = scope.Resolve<ISharePointBotStateService>();
                currentSite = await service.GetCurrentSite();
            }

            if (currentSite != null)
            {
                var siteNameToDisplay = !string.IsNullOrEmpty(currentSite.Alias) ? currentSite.Alias : currentSite.Title;

                await context.PostAsync($"You are on '{siteNameToDisplay}' ({currentSite.Url}).");
            }
            else
            {
                await context.PostAsync("You haven't selected a site.");
            }

            context.Done("All done!");
        }
    }
}