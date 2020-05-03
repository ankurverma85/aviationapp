using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace AviationApp.FAADataParser.Utils
{
    class ParsePhone
    {
        public static bool TryParse(string input, out string phone)
        {
            Match match = _800Number.Match(input);
            if (match.Success)
            {
                input = "800-" + match.Groups["Group1"] + "-" + match.Groups["Group2"];
            }
            match = _1800Number.Match(input);
            if (match.Success)
            {
                input = "1-800-" + match.Groups["Group1"] + "-" + match.Groups["Group2"];
            }
            try
            {
                PhoneNumber phoneNumber = phoneNumberUtil.Parse(input, "US");
                phone = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
            }
            catch (NumberParseException)
            {
                phone = null;
                return false;
            }
            return true;
        }
        private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

        private static readonly Regex _800Number = new Regex(@"\b8-(?<Group1>\d{3})-(?<Group2>\d{4})");
        private static readonly Regex _1800Number = new Regex(@"\b1-(?<Group1>\d{3})-(?<Group2>\d{4})");
    }
}
