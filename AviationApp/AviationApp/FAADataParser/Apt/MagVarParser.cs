using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AviationApp.FAADataParser.Apt
{
    class MagVarParser
    {
        public static bool TryParse(string input, out int var)
        {
            var = 0;
            Match match = varRegex.Match(input);
            if (match.Success)
            {
                _ = int.TryParse(match.Groups["Amount"].Value, out var);
                if(match.Groups["Direction"].Value == "E")
                {
                    var = -var;
                }
                return true;
            }
            return false;
        }
        private static Regex varRegex = new Regex(@"\b(?<Amount>\d{2})(?<Direction>[EW])\b");
    }
}
