using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SharePointBot.Services;
using SharePointBot.Model;

namespace SharePointBot.Dialogs
{
    public class GetSiteDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            var service = new SharePointBotStateService(context);

            var currentSite = await service.GetCurrentSite();

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