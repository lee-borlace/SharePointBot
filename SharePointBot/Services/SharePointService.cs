using BotAuth;
using BotAuth.Models;
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
        public async Task<BotSite> GetWebByTitle(string title, AuthResult auth)
        {
            var json = await new HttpClient().GetWithAuthAsync(auth.AccessToken, Constants.GraphApiUrls.RootSite);

            return new BotSite
            {
                Alias = string.Empty,
                Id = Guid.Empty,
                Title = json.Value<string>(Constants.RestApi.SiteName),
                Url = json.Value<string>(Constants.RestApi.SiteUrl)
            };
        }
    }
}