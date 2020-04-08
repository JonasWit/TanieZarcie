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
            var regex = new Regex("[^AaĄąBbCcĆćDdEeĘęFfGgHhIiJjKkLlŁłMmNnŃńOoÓóPpRrSsŚśTtUuWwYyZzŹźŻż0-9]");
            return regex.Replace(input, "").ToUpper();
        }

        public static bool StandardSearch(this string input, string match)
        {
            if (input.ToUpper().Contains(match.ToUpper().Trim())) return true;
            else return false;

            //if (Regex.IsMatch(input, $@"\b{match}\b(?=\s|,)", RegexOptions.IgnoreCase)) return true;
            //else return false;
        }
    }
}
