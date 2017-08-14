using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharePointBot.Services;

namespace SharePointBot.UnitTests.Services
{
    [TestClass]
    public class SharePointServiceTests
    {
        #region GetTenantUrlFromSiteCollectionUrl

        [TestMethod]
        public void SharePointService_GetTenantUrlFromSiteCollectionUrl_Match1()
        {
            var input = "https://myHost.sharepoint.com";

            var service = new SharePointService();

            var output = service.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.AreEqual("https://myHost.sharepoint.com", output);
        }

        [TestMethod]
        public void SharePointService_GetTenantUrlFromSiteCollectionUrl_Match2()
        {
            var input = "https://myHost.sharepoint.com/sites/AAA";

            var service = new SharePointService();

            var output = service.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.AreEqual("https://myHost.sharepoint.com", output);
        }

        [TestMethod]
        public void SharePointService_GetTenantUrlFromSiteCollectionUrl_Match3()
        {
            var input = "https://myHost.sharepoint.com/teams/BBB";

            var service = new SharePointService();

            var output = service.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.AreEqual("https://myHost.sharepoint.com", output);
        }

        [TestMethod]
        public void SharePointService_GetTenantUrlFromSiteCollectionUrl_NoMatch1()
        {
            var input = "https://myHost.sharepoint.com/sites/A/B";

            var service = new SharePointService();

            var output = service.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void SharePointService_GetTenantUrlFromSiteCollectionUrl_NoMatch2()
        {
            var input = "https://myHost.onmicrosoft.com/sites/A";

            var service = new SharePointService();

            var output = service.GetTenantUrlFromSiteCollectionUrl(input);

            Assert.IsNull(output);
        }

        #endregion
    }
}
