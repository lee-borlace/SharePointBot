using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using SharePointBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBot.Services.Interfaces
{
    /// <summary>
    /// SharePoint-related service functionality.
    /// </summary>
    public interface ISharePointService
    {
        /// <summary>
        /// Get web by title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="auth"></param>
        /// <returns>A BotSite representing the web if it exists, otherwise null.</returns>
        Task<BotSite> GetWebByTitle(string title, AuthResult auth, IBotContext context);

        /// <summary>
        /// Gets the tenant URL from site collection URL.
        /// </summary>
        /// <param name="siteCollectionUrl">The site collection URL.</param>
        /// <returns></returns>
        string GetTenantUrlFromSiteCollectionUrl(string siteCollectionUrl);
    }
}
