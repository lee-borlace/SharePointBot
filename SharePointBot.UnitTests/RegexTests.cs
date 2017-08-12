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
        [TestMethod]
        public void SPOTenantUrl_Match1()
        {
            var input = "https://myTenant.sharepoint.com";
            var pattern = Constants.RegexMisc.SPOTenantUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SPOTenantUrl_Match2()
        {
            var input = "https://bob-24.sharepoint.com";
            var pattern = Constants.RegexMisc.SPOTenantUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SPOTenantUrl_NoMatch1()
        {
            var input = "http://myTenant.sharepoint.com";
            var pattern = Constants.RegexMisc.SPOTenantUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SPOTenantUrl_NoMatch2()
        {
            var input = "https://myTenant.onmicrosoft.com";
            var pattern = Constants.RegexMisc.SPOTenantUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SPOTenantUrl_NoMatch3()
        {
            var input = "https://myTenant.sharepoint.com/sites/HealthAndFitness";
            var pattern = Constants.RegexMisc.SPOTenantUrl;
            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsFalse(result);
        }
    }
}
