using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Utils
{
    class ParseBool
    {
        public static bool TryParse(string input, out bool output)
        {
            switch(input)
            {
                case "Y": output = true; break;
                case "N": output = false; break;
                default: output = false; return false;
            }
            return true;
        }
    }
}
