using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharePointBot.UnitTests
{
    [TestClass]
    public class RegexTests
    {
        #region SiteCollectionUrl

        #region Match

        [TestMethod] 
        public void SPOSiteCollectionUrl_Match1()
        {
            var input = "https://myHost.sharepoint.com";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var tenantUrl = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(tenantUrl));
            Assert.AreEqual("https://myHost.sharepoint.com", tenantUrl);
        }

        [TestMethod]
        public void SPOSiteCollectionUrl_Match2()
        {
            var input = "https://myHost.sharepoint.com/sites/SiteColl1";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var tenantUrl = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(tenantUrl));
            Assert.AreEqual("https://myHost.sharepoint.com", tenantUrl);
        }

        [TestMethod]
        public void SPOSiteCollectionUrl_Match3()
        {
            var input = "   https://myHost.sharepoint.com/sites/SiteColl1  ";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteCollectionUrl = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteCollectionUrl));
            Assert.AreEqual("https://myHost.sharepoint.com", siteCollectionUrl);
        }


        [TestMethod]
        public void SPOSiteCollectionUrl_Match4()
        {
            var input = "   https://myHost.sharepoint.com/teams/SiteColl1  ";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var tenantUrl = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(tenantUrl));
            Assert.AreEqual("https://myHost.sharepoint.com", tenantUrl);
        }

        [TestMethod]
        public void SPOSiteCollectionUrl_Match5()
        {
            var input = "   https://myHost.sharepoint.com/whatever/SiteColl1  ";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var tenantUrl = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(tenantUrl));
            Assert.AreEqual("https://myHost.sharepoint.com", tenantUrl);
        }

        [TestMethod]
        public void SPOSiteCollectionUrl_Match6()
        {
            var input = "   https://awesomesite.sharepoint.com/whatever/SiteColl2  ";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var tenantUrl = match.Groups[Constants.RegexGroupNames.TenantUrl].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(tenantUrl));
            Assert.AreEqual("https://awesomesite.sharepoint.com", tenantUrl);
        }

        #endregion

        #region No match



        [TestMethod]
        public void SPOSiteCollectionUrl_NoHttps_NoMatch1()
        {
            var input = "http://mySiteCollection.sharepoint.com";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SPOSiteCollectionUrl_NotSharePointDotCom_NoMatch2()
        {
            var input = "https://mySiteCollection.onmicrosoft.com";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SPOSiteCollectionUrl_SubsiteSpecified_NoMatch3()
        {
            var input = "https://mySiteCollection.sharepoint.com/sites/siteA/siteB";
            var pattern = Constants.RegexMisc.SiteCollectionUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsFalse(result);
        }

        #endregion

        #endregion

    }
}
