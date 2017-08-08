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
            public const string LOGIN = @"\s*(log|sign)\s*(in|on)\s*";
            public const string LOGOUT = @"\s*(log|sign)\s*(out|off)\s*";
            public const string SELECT_SITE = @"\s*((go\s*to)|(select))\s+((web\s*)?site)|web\s*";
            public const string WHAT_IS_CURRENT_SITE = @"(what site am i on)|(what is the current list)";
            public const string WHAT_IS_CURRENT_LIST = @"";
        }
      

        public static class Choices
        {
        }

        public static class Responses
        {
        }
    }
}