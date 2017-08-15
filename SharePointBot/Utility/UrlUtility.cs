using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SharePointBot.Utility
{
    public class UrlUtility
    {
        /// <summary>
        /// Gets the tenant URL from site collection URL.
        /// </summary>
        /// <param name="siteCollectionUrl">The site collection URL.</param>
        /// <returns></returns>
        public static string GetTenantUrlFromSiteCollectionUrl(string siteCollectionUrl)
        {
            string retVal = null;

            var match = Regex.Match(siteCollectionUrl, Constants.RegexMisc.SiteCollectionUrl, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);

            if (match.Success)
            {
                retVal = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;
            }

            return retVal;
        }
    }
}