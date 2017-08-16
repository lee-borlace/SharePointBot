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
        /// Search for web. This will return a list of possible matches as it uses search for this purpose.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="auth">The authentication.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A BotSite representing the web if it exists, otherwise null.
        /// </returns>
        Task<List<BotSite>> SearchForWeb(string title, AuthResult auth, IBotContext context);
    }
}
