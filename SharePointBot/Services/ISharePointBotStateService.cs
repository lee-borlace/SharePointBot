using Microsoft.SharePoint.Client;
using SharePointBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointBot.Services
{
    interface ISharePointBotStateService
    {
        BotSite SetCurrentSite(BotSite site);

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
