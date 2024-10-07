using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Deserializer.Helper
{
    public static class LinkCleaner
    {
        public static string CleanLink(string input)
        {
            if (input==null)
            {
                return string.Empty;
            }
            string cleaned = input.Trim().Replace("\r", "").Replace("\n", "").Replace("\"", "");


            string pattern = @"(http|https)://[^\s]+";
            Match match = Regex.Match(cleaned, pattern);

            if (match.Success)
            {
                return match.Value;
            }

            return string.Empty;
        }
    }
}
