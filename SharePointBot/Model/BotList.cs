using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot.Model
{
    /// <summary>
    /// A "list" as referenced by the bot. In actual fact this is a SP web.
    /// </summary>
    public class BotList
    {
        /// <summary>
        /// Friendly alias that user knows the list as. This may not be its actual name.
        /// </summary>
        public string Alias { get; set; }
    }
}