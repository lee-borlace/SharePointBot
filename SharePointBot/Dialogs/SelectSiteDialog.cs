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
    public class SelectSiteDialog : AutofacDialog, IDialog<object>
    {
        public SelectSiteDialog(ILifetimeScope scope) : base(scope) { }

        public async Task StartAsync(IDialogContext context)
        {
            using (var scope = _dialogScope.BeginLifetimeScope())
            {
                var service = scope.Resolve<SharePointBotStateService.Factory>().Invoke(context);

                await service.SetCurrentSite(
                    new BotSite
                    {
                        Alias = "health and fitness",
                        Id = Guid.NewGuid(),
                        Title = "My h&f site",
                        Url = "/sites/whatevs"
                    }
                );
            }

            await context.PostAsync("Site selected.");

            context.Done("All done!");
        }
    }
}