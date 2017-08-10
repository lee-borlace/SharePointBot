using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot.Model
{
    /// <summary>
    /// A "site" as referenced by the bot. In actual fact this is a SP web.
    /// </summary>
    [Serializable]
    public class BotSite
    {
        /// <summary>
        /// Friendly alias that user knows the site as. This may not be its actual name.
        /// </summary>
        public string Alias { get; set; }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}