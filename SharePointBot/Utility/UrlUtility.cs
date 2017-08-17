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

        /// <summary>
        /// Given a full URL, get the server-relative part.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetServerRelativeUrl(string url)
        {
            string retVal = null;

            var match = Regex.Match(url, Constants.RegexMisc.AnySubSiteUrl, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);

            if (match.Success)
            {
                retVal = match.Groups[Constants.RegexGroupNames.ServerRelativeUrl].Value;

                // Account for the root with nothing after it.
                if (string.IsNullOrEmpty(retVal))
                {
                    retVal = "/";
                }
            }

            return retVal;
        }

        /// <summary>
        /// Given an anchor tag, extract the href attribute.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Href attribute if found in valid anchor tag, otherwise return entire input</returns>
        public static string ExtractHrefFromAnchorTag(string input)
        {
            string retVal = input;

            var match = Regex.Match(input, Constants.RegexMisc.AnchorTag, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);

            if (match.Success)
            {
                retVal = match.Groups[Constants.RegexGroupNames.Href].Value;
            }

            return retVal;
        }

    }
}