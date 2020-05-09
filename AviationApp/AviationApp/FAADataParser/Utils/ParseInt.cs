using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Utils
{
    class ParseInt
    {
        public static bool TryParse(string input, out int val) => int.TryParse(input, out val);
    }
}
