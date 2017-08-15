using Microsoft.Bot.Builder.Dialogs;
using Microsoft.SharePoint.Client;
using SharePointBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBot.Services.Interfaces
{
    public interface ISharePointBotStateService
    {
        IBotContext BotContext { get; set; }

        /// <summary>
        /// Set currently-selected site for current user in current conversation in current channel.
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        Task SetCurrentSite(BotSite site);

        /// <summary>
        /// Get currently-selected site for current user in current conversation in current channel.
        /// </summary>
        /// <returns></returns>
        Task<BotSite> GetCurrentSite();

        /// <summary>
        /// Currently-selected list for user.
        /// </summary>
        BotList CurrentList { get; set; }

        /// <summary>
        /// Sites that user has added to their list of favourite sites.
        /// </summary>
        IEnumerable<BotSite> FavouriteSites { get; set; }

        /// <summary>
        /// Lists that user has added to their list of favourite lists.
        /// </summary>
        IEnumerable<BotList> FavouriteLists { get; set; }

    }
}
