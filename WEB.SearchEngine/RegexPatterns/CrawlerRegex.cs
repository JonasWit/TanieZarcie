using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WEB.SearchEngine.RegexPatterns
{
    public static class CrawlerRegex
    {
        public static string StandardNormalization = "[^a-zA-Z0-9]";

        public static Regex GetStandardNormalizationRegex()
        {
            return new Regex(StandardNormalization);
        
        }




    }
}
