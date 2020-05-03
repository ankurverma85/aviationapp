using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Utils
{
    class ParseDate
    {
        public static bool TryParse(string val, out DateTime dateTime)
        {
            return DateTime.TryParseExact(val, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out dateTime);
        }
    }
}
