using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace SharePointBot.UnitTests
{
    [TestClass]
    public class UtteranceRegexTests
    {
        #region Log in

        const string CATEGORY_LOG_IN = "Log in";

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogIn_Match1()
        {
            var input = "log in";
            var pattern = Constants.UtteranceRegexes.Login;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogIn_Match2()
        {
            var input = "login";
            var pattern = Constants.UtteranceRegexes.Login;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogIn_Match3()
        {
            var input = "login please";
            var pattern = Constants.UtteranceRegexes.Login;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogIn_Match4()
        {
            var input = "I'd like to login please";
            var pattern = Constants.UtteranceRegexes.Login;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogIn_Match5()
        {
            var input = "sign   in";
            var pattern = Constants.UtteranceRegexes.Login;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogIn_Match6()
        {
            var input = "signin";
            var pattern = Constants.UtteranceRegexes.Login;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        #endregion

        #region Log out

        const string CATEGORY_LOG_OUT = "Log out";

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_LogOut_Match1()
        {
            var input = "log out";
            var pattern = Constants.UtteranceRegexes.LogOut;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_LogOut_Match2()
        {
            var input = "logout";
            var pattern = Constants.UtteranceRegexes.LogOut;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogOut_Match3()
        {
            var input = "logout please";
            var pattern = Constants.UtteranceRegexes.LogOut;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void UtteranceRegex_LogOut_Match4()
        {
            var input = "I'd like to logout please";
            var pattern = Constants.UtteranceRegexes.LogOut;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_LogOut_Match5()
        {
            var input = "sign out";
            var pattern = Constants.UtteranceRegexes.LogOut;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_LogOut_Match6()
        {
            var input = "signout";
            var pattern = Constants.UtteranceRegexes.LogOut;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }


        #endregion

        #region Select site - no site specified

        const string CATEGORY_SELECT_SITE_NO_SITE_SPECIFIED = "Select site - no site specified";

        [TestMethod]
        [TestCategory(CATEGORY_SELECT_SITE_NO_SITE_SPECIFIED)]
        public void UtteranceRegex_SelectSite_NoSiteSpecified_Match1()
        {
            var input = "select site";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_SELECT_SITE_NO_SITE_SPECIFIED)]
        public void UtteranceRegex_SelectSite_NoSiteSpecified_Match2()
        {
            var input = "select website";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }


        [TestMethod]
        [TestCategory(CATEGORY_SELECT_SITE_NO_SITE_SPECIFIED)]
        public void UtteranceRegex_SelectSite_NoSiteSpecified_Match3()
        {
            var input = "select web site";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_SELECT_SITE_NO_SITE_SPECIFIED)]
        public void UtteranceRegex_SelectSite_NoSiteSpecified_Match4()
        {
            var input = "go to site";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_SELECT_SITE_NO_SITE_SPECIFIED)]
        public void UtteranceRegex_SelectSite_NoSiteSpecified_Match5()
        {
            var input = "goto   site";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        #endregion

        #region Select site - site specified

        const string CATEGORY_SELECT_SITE_SITE_SPECIFIED = "Select site - site specified";

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_SelectSite_SiteSpecified_Match1()
        {
            var input = "select site abcd";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteTitleOrAlias));
            Assert.AreEqual("abcd", siteTitleOrAlias);

            
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_SelectSite_SiteSpecified_Match2()
        {
            var input = "select website    health and fitness";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteTitleOrAlias));
            Assert.AreEqual("health and fitness", siteTitleOrAlias);
        }


        /// <summary>
        /// TODO - get this working. Doesn't cope with quote in website name.
        /// </summary>
        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_SelectSite_SiteSpecified_Match3()
        {
            var input = "   select  web  site crazy jack's crazy website";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteTitleOrAlias));
            Assert.AreEqual("crazy jack's crazy website", siteTitleOrAlias);
        }


        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_SelectSite_SiteSpecified_Match4()
        {
            var input = "   select  website website";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteTitleOrAlias));
            Assert.AreEqual("website", siteTitleOrAlias);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_SelectSite_SiteSpecified_Match5()
        {
            var input = "go to site aaa bbb ccc";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteTitleOrAlias));
            Assert.AreEqual("aaa bbb ccc", siteTitleOrAlias);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void UtteranceRegex_SelectSite_SiteSpecified_Match6()
        {
            var input = "goto   site zzz 111";
            var pattern = Constants.UtteranceRegexes.SelectSite;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            var siteTitleOrAlias = match.Groups[Constants.RegexGroupNames.SiteTitleOrAlias].Value;

            Assert.IsTrue(match.Success);
            Assert.IsFalse(string.IsNullOrEmpty(siteTitleOrAlias));
            Assert.AreEqual("zzz 111", siteTitleOrAlias);
        }

        #endregion
    }
}
