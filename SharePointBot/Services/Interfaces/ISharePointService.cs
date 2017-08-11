using BotAuth.Models;
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
        Task<BotSite> GetWebByTitle(string title, AuthResult auth);
    }
}
