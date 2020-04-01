﻿using System.Text.RegularExpressions;

namespace WEB.SearchEngine.RegexPatterns
{
    public enum MatchDireciton
    { 
        Equals = 0,
        InputContainsMatch = 1,
        MatchContainsInput = 2,
    }

    public static class CrawlerRegex
    {
        public const string StandardNormalization = @"[^a-zA-Z0-9]";
        public const string Metacharactes = @"[\t|\n|\r\|(&quot;)]";

        public static Regex GetStandardNormalizationRegex() => new Regex(StandardNormalization);

        public static bool StandardMatch(string input, string match, MatchDireciton matchDireciton)
        {
            var normalizedInput = CrawlerRegex.GetStandardNormalizationRegex().Replace(input, "").ToUpper();
            var normalizedMatch = CrawlerRegex.GetStandardNormalizationRegex().Replace(match, "").ToUpper();

            switch (matchDireciton)
            {
                case MatchDireciton.Equals:
                    if (normalizedInput.Equals(normalizedMatch)) return true;
                    else return false; 
                case MatchDireciton.InputContainsMatch:
                    if (normalizedInput.Contains(normalizedMatch)) return true;
                    else return false;
                case MatchDireciton.MatchContainsInput:
                    if (normalizedMatch.Contains(normalizedInput)) return true;
                    else return false;
                default:
                    return false;
            }
        }

        public static string RemoveMetacharacters(this string input) => Regex.Replace(input, Metacharactes, "");

        public static string NormalizeWithStandardRegex(this string input) => GetStandardNormalizationRegex().Replace(input, "").ToUpper();
    }
}
