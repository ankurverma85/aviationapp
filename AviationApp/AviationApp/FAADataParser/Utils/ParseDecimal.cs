using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Utils
{
    class ParseDecimal
    {
        public static bool TryParse(string input, out decimal output) => decimal.TryParse(input, out output);
    }
}
