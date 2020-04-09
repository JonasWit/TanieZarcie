using System.Linq;
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

        public static string NormalizeWithStandardRegex(this string input)
        {
            var regex = new Regex("[^AaĄąBbCcĆćDdEeĘęFfGgHhIiJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwYyZzŹźŻż0-9]");
            return regex.Replace(input, "").ToUpper();
        }

        public static bool ContainsAny(this string inputString, params string[] lookupStrings) => lookupStrings.Any(inputString.Contains);

        public static bool EqualsTrim(this string inputString, params string[] lookupStrings) => lookupStrings.Any(x => inputString.Trim().Equals(x.Trim()));

        public static string RemoveUnwantedStrings(this string input) => 
            input.Replace("&quot;", "")
            .Replace("&nbsp;","");

    }
}
