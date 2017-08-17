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
            /// <summary>
            /// Any general URL.
            /// </summary>
            public const string GeneralUrl = @"https ?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";

            /// <summary>
            /// A SPO site collection URL, with capture group for tenant URL portion.
            /// </summary>
            public const string SiteCollectionUrl = @"^\s*(?<tenantUrl>https:\/\/[-a-zA-Z0-9@:%._\+~#=]+\.sharepoint\.com)(\/[a-zA-Z0-9]+\/[-a-zA-Z0-9@:%._\+~#=]+)?\s*$";

            /// <summary>
            /// SPO tenant URL.
            /// </summary>
            public const string SPOTenantUrl = @"^\s*https:\/\/[-a-zA-Z0-9@:%._\+~#=]+\.sharepoint\.com\s*$";

            /// <summary>
            /// URL on any sub-site, with capture group for the server-relative part.
            /// </summary>
            public const string AnySubSiteUrl = @"^\s*https:\/\/[-a-zA-Z0-9@:%._\+~#=]+\.sharepoint\.com(?<serverRelativeUrl>\/\S*)?\s*$";

            /// <summary>
            /// HTML anchor tag, with capture group for href.
            /// </summary>
            public const string AnchorTag = @"^\s*<a\s+href=('|"")(?<href>" + GeneralUrl + @")('|"").*>.+<\s*/a\s*>$";
        }


        public static class Choices
        {
        }

        public static class Responses
        {
            public static string LogIntoWhichSiteCollection = "What's the URL of the site collection you want to log into, e.g. https://tenantName.sharepoint.com/sites/siteCollection";
            public static string LastSiteCollection = " To log into the last site collection you used ({0}), you can say 'last'.";
            public static string SelectWhichSite = "What's the title or alias of the site you want to select?";
            public static string LogOnFirst = "You'll need to log on first.";
            public static string InvalidSiteCollectionUrl = "That didn't look like a valid site collection URL e.g. https://tenantName.sharepoint.com/sites/siteCollection. You're not logged in yet.";
            public static string LogInFailed = "Sorry, I couldn't log you in.";
            public static string CouldntFindSite = "Sorry, I couldn't find that site.";
            public static string LoggedIn = "You're all logged in.";
            public static string ChooseSite = "I found more than one matching site. Which one do you want to select?";
            public static string DidntUnderstand = "Hmm I'm sorry, I didn't quite understand that.";
            public static string PleaseChooseAnOption = " Please choose one of the options below.";
        }

        public static class StateKeys
        {
            public const string LastLoggedInTenantUrl = "SPBot_LoggedInTenantUrl";
            public const string LastLoggedInSiteCollectionUrl = "SPBot_LoggedInSiteCollectionUrl";
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
            public const string TenantUrl = "tenantUrl";
            public const string ServerRelativeUrl = "serverRelativeUrl";
            public const string Href = "href";
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


        public static class Azure
        {
            public const string TableNameActivityLogging = "SharePointBotActivityLogging";
        }


    }
}