using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot
{
    public class Constants
    {
        public static class UtteranceRegexes
        {
            public const string Login = @"\s*(log|sign)\s*(in|on)\s*";
            public const string LogOut = @"\s*(log|sign)\s*(out|off)\s*";
            public const string SelectSite = @"^\s*((go\s*to)|(select))\s+(((web\s*)?site)|web)(\s*(?<siteTitleOrAlias>.+))?\s*$";
            public const string WhatIsCurrentSite = @"(what site am i on)|(what is the current site)";
            public const string WhatIsCurrentList = @"";
        }
      

        public static class Choices
        {
        }

        public static class Responses
        {
            public static string SelectWhichSite = "What's the title or alias of the site you want to select?";
            public static string LogOnFirst = "You'll need to log on first.";
        }

        public static class StateKeys
        {
            public const string CurrentSite = "SPBot_CurrentSite";
        }

        /// <summary>
        /// Used for reflection when needed.
        /// </summary>
        public static class FieldNames
        {
            public const string BotContext = "botContext";
            public const string SiteTitleOrAlias = "siteTitleOrAlias";
        }

        public static class RegexGroupNames
        {
            public const string SiteTitleOrAlias = "siteTitleOrAlias";
        }

        public static class Misc
        {
            public const int DialogAttempts = 3;
        }


        public static class GraphApiUrls
        {
            public const string RootSite = "https://graph.microsoft.com/v1.0/sites/root";
        }

        public static class RestApi
        {
            public const string SiteName = "displayName";
            public const string SiteUrl = "webUrl";
        }

    }
}