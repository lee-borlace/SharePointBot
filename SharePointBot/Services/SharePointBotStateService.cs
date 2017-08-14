using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharePointBot.Model;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using SharePointBot.Services.Interfaces;

namespace SharePointBot.Services
{
    /// <summary>
    /// Provides some wrapping around state.
    /// </summary>
    public class SharePointBotStateService : ISharePointBotStateService
    {
        IBotContext _botContext;
        IActivity _activity;
        IBotState _botState;
        StateClient _stateClient;

        public SharePointBotStateService(IBotContext botContext)
        {
            _botContext = botContext;
            _activity = botContext.Activity;
            _stateClient = _activity.GetStateClient();
            _botState = _stateClient.BotState;
        }

        /// <summary>
        /// Get currently-selected site for current user in current conversation in current channel.
        /// </summary>
        /// <returns></returns>
        public async Task<BotSite> GetCurrentSite()
        {
            // TODO - neither of these commented-out options work - the getter returns null. Why?

            //var botData = await GetPrivateConversationDataAsync();
            //return botData.GetProperty<BotSite>(Constants.StateKeys.CurrentSite);

            //var botData = await GeUserDataAsync();
            //return botData.GetProperty<BotSite>(Constants.StateKeys.CurrentSite);

            BotSite retrieved = null;
            _botContext.PrivateConversationData.TryGetValue<BotSite>(Constants.StateKeys.CurrentSite, out retrieved);
            return retrieved;
        }

        /// <summary>
        /// Set currently-selected site for current user in current conversation in current channel.
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public async Task SetCurrentSite(BotSite site)
        {
            // TODO - neither of these commented-out options work - the getter returns null. Why?

            //var botData = await GetPrivateConversationDataAsync();
            //botData.SetProperty<BotSite>(Constants.StateKeys.CurrentSite, site);
            //await SetPrivateConversationDataAsync(botData);

            //var botData = await GeUserDataAsync();
            //botData.SetProperty<BotSite>(Constants.StateKeys.CurrentSite, site);
            //await SetuserDataAsync(botData);

            _botContext.PrivateConversationData.SetValue<BotSite>(Constants.StateKeys.CurrentSite, site);
        }

        /// <summary>
        /// Get private conversation data based on current activity.
        /// </summary>
        /// <returns></returns>
        private async Task<BotData> GetPrivateConversationDataAsync()
        {
            return await _botState.GetPrivateConversationDataAsync(_activity.ChannelId, _activity.Conversation.Id, _activity.From.Id);
        }

        /// <summary>
        /// Set private conversation data based on current activity.
        /// </summary>
        /// <returns></returns>
        private async Task SetPrivateConversationDataAsync(BotData botdata)
        {
            await _botState.SetPrivateConversationDataAsync(_activity.ChannelId, _activity.Conversation.Id, _activity.From.Id, botdata);
        }


        /// <summary>
        /// Get user data based on current activity.
        /// </summary>
        /// <returns></returns>
        private async Task<BotData> GeUserDataAsync()
        {
            return await _botState.GetUserDataAsync(_activity.ChannelId, _activity.From.Id);
        }


        /// <summary>
        /// Set user data based on current activity.
        /// </summary>
        /// <returns></returns>
        private async Task SetuserDataAsync(BotData botdata)
        {
            await _botState.SetUserDataAsync(_activity.ChannelId, _activity.From.Id, botdata);
        }

        public BotList CurrentList { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public IEnumerable<BotSite> FavouriteSites { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public IEnumerable<BotList> FavouriteLists { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

    }
}