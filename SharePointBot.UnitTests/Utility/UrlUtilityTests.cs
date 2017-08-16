using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePointBot.Services;
using SharePointBot.Utility;

namespace SharePointBot.UnitTests.Utility
{
    [TestClass]
    public class UrlUtilityTests
    {
        #region GetTenantUrlFromSiteCollectionUrl

        [TestMethod]
        public void UrlUtility_GetTenantUrlFromSiteCollectionUrl_Match1()
        {
            var input = "https://myHost.sharepoint.com";

            var output = UrlUtility.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.AreEqual("https://myHost.sharepoint.com", output);
        }

        [TestMethod]
        public void UrlUtility_GetTenantUrlFromSiteCollectionUrl_Match2()
        {
            var input = "https://myHost.sharepoint.com/sites/AAA";

            var output = UrlUtility.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.AreEqual("https://myHost.sharepoint.com", output);
        }

        [TestMethod]
        public void UrlUtility_GetTenantUrlFromSiteCollectionUrl_Match3()
        {
            var input = "https://myHost.sharepoint.com/teams/BBB";

            var output = UrlUtility.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.AreEqual("https://myHost.sharepoint.com", output);
        }

        [TestMethod]
        public void UrlUtility_GetTenantUrlFromSiteCollectionUrl_NoMatch1()
        {
            var input = "https://myHost.sharepoint.com/sites/A/B";

            var output = UrlUtility.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void UrlUtility_GetTenantUrlFromSiteCollectionUrl_NoMatch2()
        {
            var input = "https://myHost.onmicrosoft.com/sites/A";

            var output = UrlUtility.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.IsNull(output);
        }

        #endregion

        #region GetServerRelativeUrl

        [TestMethod]
        public void UrlUtility_GetServerRelativeUrl_1()
        {
            var input = "https://myHost.sharepoint.com";

            var output = UrlUtility.GetServerRelativeUrl(input);

            Assert.AreEqual("/", output);
        }

        [TestMethod]
        public void UrlUtility_GetServerRelativeUrl_2()
        {
            var input = "https://myHost.sharepoint.com/";

            var output = UrlUtility.GetServerRelativeUrl(input);

            Assert.AreEqual("/", output);
        }

        [TestMethod]
        public void UrlUtility_GetServerRelativeUrl_3()
        {
            var input = "https://myHost.sharepoint.com/sites/a/b/c/d";

            var output = UrlUtility.GetServerRelativeUrl(input);

            Assert.AreEqual("/sites/a/b/c/d", output);
        }



        #endregion
    }
}
