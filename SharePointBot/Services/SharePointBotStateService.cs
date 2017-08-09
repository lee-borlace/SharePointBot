using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharePointBot.Model;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace SharePointBot.Services
{
    /// <summary>
    /// Provides some wrapping around state.
    /// </summary>
    public class SharePointBotStateService : ISharePointBotStateService
    {
        IBotContext _botContext;

        public SharePointBotStateService(IBotContext botContext)
        {
            _botContext = botContext;
        }

        public async BotSite SetCurrentSite(BotSite site)
        {
            throw new NotImplementedException();
        }

        public BotList CurrentList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<BotSite> FavouriteSites { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<BotList> FavouriteLists { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}