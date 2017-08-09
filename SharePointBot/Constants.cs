using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharePointBot
{
    public static class Constants
    {
        public static class UtteranceRegexes
        {
            public const string Login = @"\s*(log|sign)\s*(in|on)\s*";
            public const string LogOut = @"\s*(log|sign)\s*(out|off)\s*";
            public const string SelectSite = @"\s*((go\s*to)|(select))\s+((web\s*)?site)|web\s*";
            public const string WhatIsCurrentSite = @"(what site am i on)|(what is the current list)";
            public const string WhatIsCurrentList = @"";
        }
      

        public static class Choices
        {
        }

        public static class Responses
        {
        }

        public static class StateKeys
        {

        }
    }
}