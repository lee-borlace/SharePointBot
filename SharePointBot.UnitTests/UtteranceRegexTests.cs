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
        public void LogIn_Match1()
        {
            var input = "log in";
            var pattern = Constants.UtteranceRegexes.LOGIN;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogIn_Match2()
        {
            var input = "login";
            var pattern = Constants.UtteranceRegexes.LOGIN;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogIn_Match3()
        {
            var input = "login please";
            var pattern = Constants.UtteranceRegexes.LOGIN;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogIn_Match4()
        {
            var input = "I'd like to login please";
            var pattern = Constants.UtteranceRegexes.LOGIN;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogIn_Match5()
        {
            var input = "sign   in";
            var pattern = Constants.UtteranceRegexes.LOGIN;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogIn_Match6()
        {
            var input = "signin";
            var pattern = Constants.UtteranceRegexes.LOGIN;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        #endregion

        #region Log out

        const string CATEGORY_LOG_OUT = "Log out";

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void LogOut_Match1()
        {
            var input = "log out";
            var pattern = Constants.UtteranceRegexes.LOGOUT;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void LogOut_Match2()
        {
            var input = "logout";
            var pattern = Constants.UtteranceRegexes.LOGOUT;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogOut_Match3()
        {
            var input = "logout please";
            var pattern = Constants.UtteranceRegexes.LOGOUT;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_IN)]
        public void LogOut_Match4()
        {
            var input = "I'd like to logout please";
            var pattern = Constants.UtteranceRegexes.LOGOUT;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void LogOut_Match5()
        {
            var input = "sign out";
            var pattern = Constants.UtteranceRegexes.LOGOUT;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void LogOut_Match6()
        {
            var input = "signout";
            var pattern = Constants.UtteranceRegexes.LOGOUT;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }


        #endregion

        #region Select site

        const string CATEGORY_SELECT_SITE = "Select site";

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void SelectSite_Match1()
        {
            var input = "select site";
            var pattern = Constants.UtteranceRegexes.SELECT_SITE;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void SelectSite_Match2()
        {
            var input = "select website";
            var pattern = Constants.UtteranceRegexes.SELECT_SITE;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }


        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void SelectSite_Match3()
        {
            var input = "select web site";
            var pattern = Constants.UtteranceRegexes.SELECT_SITE;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void SelectSite_Match4()
        {
            var input = "go to site";
            var pattern = Constants.UtteranceRegexes.SELECT_SITE;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory(CATEGORY_LOG_OUT)]
        public void SelectSite_Match5()
        {
            var input = "goto   site";
            var pattern = Constants.UtteranceRegexes.SELECT_SITE;

            var result = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);

            Assert.IsTrue(result);
        }

        #endregion
    }
}
