using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WEB.Shop.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeWithStandardRegex(this string input)
        {
            var regex = new Regex("[^a-zA-Z0-9]");
            return regex.Replace(input, "").ToUpper();
        }

        public static bool StandardSearch(this string input, string match)
        {
            if (Regex.IsMatch(input, $@"\b{match}\b(?=\s)", RegexOptions.IgnoreCase)) return true;
            else return false;
        }
    }
}
