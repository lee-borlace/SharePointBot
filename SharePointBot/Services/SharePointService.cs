using BotAuth;
using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.SharePoint.Client;
using SharePointBot.Model;
using SharePointBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SharePointBot.Services
{
    [Serializable]
    public class SharePointService : ISharePointService
    {
        /// <summary>
        /// Get web by title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="accessToken"></param>
        /// <returns>A BotSite representing the web if it exists, otherwise null.</returns>
        public async Task<BotSite> GetWebByTitle(string title, AuthResult auth, IBotContext context)
        {
            // We need to know the resource ID. This *should be* stored in bot state from when user logged in.
            string lastSiteCollectionUrl = null;
            if(!context.PrivateConversationData.TryGetValue<string>(Constants.StateKeys.LastLoggedInTenantUrl, out lastSiteCollectionUrl))
            {
                throw new InvalidOperationException("Could not find ");
            }

            using (var clientContext = new ClientContext(lastSiteCollectionUrl))
            {
                clientContext.ExecutingWebRequest += (object sender, WebRequestEventArgs e) =>
                {
                    e.WebRequestExecutor.RequestHeaders["Authorization"] = "Bearer " + auth.AccessToken;
                };

                List tasksList = clientContext.Web.Lists.GetByTitle("Reusable Content");
                var listItems = tasksList.GetItems(CamlQuery.CreateAllItemsQuery());

                clientContext.Load(listItems);
                clientContext.ExecuteQuery();

                foreach (ListItem item in listItems)
                {

                }

                //return new BotSite
                //{
                //    Alias = string.Empty,
                //    Id = Guid.Empty,
                //    Title = json.Value<string>(Constants.RestApi.SiteName),
                //    Url = json.Value<string>(Constants.RestApi.SiteUrl)
                //};

                return new BotSite
                {
                    Alias = string.Empty,
                    Id = Guid.Empty,
                    Title = "uuuuu",
                    Url = "u2u2u2u2u2u2u2u2u2"
                };
            }
        }
    }
}