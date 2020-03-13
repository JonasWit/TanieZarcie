using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WEB.SearchEngine.Enums;

namespace WEB.SearchEngine.Extensions
{
    public static class StringExtensions
    {
        public static bool MatchWithRegex(this string input, string toMatch, string regexString, MatchDirection matchDirection)
        {
            var regex = new Regex(regexString);

            var normalizedInput = regex.Replace(input, "").ToUpper();
            var normalizedOutput = regex.Replace(toMatch, "").ToUpper();

            switch (matchDirection)
            {
                case MatchDirection.InputContainsOutput:
                    if (normalizedInput.Contains(normalizedOutput))
                    {
                        return true;
                    }
                    break;
                case MatchDirection.OutputContainsInput:
                    if (normalizedOutput.Contains(normalizedInput))
                    {
                        return true;
                    }
                    break;
                case MatchDirection.Equals:
                    if (normalizedInput == normalizedOutput)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }

        public static bool ContainsAny(this string inputString, params string[] lookupStrings)
        {
            return lookupStrings.Any(inputString.Contains);
        }

    }
}
