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
            public const string LastSiteCollectionUrl = @"^\s*last\s*$";
        }
      
        public static class RegexMisc
        {
            public const string Url = @"https ?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
            public const string SPOTenantUrl = @"^\s*https:\/\/[-a-zA-Z0-9@:%._\+~#=]+\.sharepoint\.com\s*$";
        }


        public static class Choices
        {
        }

        public static class Responses
        {
            public static string LogIntoWhichTenant = "What's the full URL of the SPO tenant you want to log into? This should be the root tenant name without any site collection paths, i.e. https://tenantName.sharepoint.com";
            public static string LastSiteCollection = " To log into the last site collection you used ({0}), you can say 'last'.";
            public static string SelectWhichSite = "What's the title or alias of the site you want to select?";
            public static string LogOnFirst = "You'll need to log on first.";
            public static string InvalidTenantURL = "That didn't look like a valid tenant URL e.g. https://tenantName.sharepoint.com. You're not logged in yet.";
            public static string LogInFailed = "Sorry, I couldn't log you in.";
        }

        public static class StateKeys
        {
            public const string LastLoggedInTenantUrl = "SPBot_LoggedInTenantUrl";
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