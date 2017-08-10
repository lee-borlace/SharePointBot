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
    public class SelectSiteDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Site selected.");

            var service = new SharePointBotStateService(context);
            await service.SetCurrentSite(
                new BotSite {
                    Alias = "health and fitness",
                    Id = Guid.NewGuid(),
                    Title = "My h&f site",
                    Url = "/sites/whatevs"
                }
            );

            context.Done("All done!");
        }
    }
}