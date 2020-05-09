using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Apt
{
    public enum SurveyMethod { Surveyed, Estimated };
    class SurveyMethodParser
    {
        public static bool TryParse(string input, out SurveyMethod surveyMethod)
        {
            switch(input)
            {
                case "E": surveyMethod = SurveyMethod.Estimated; break;
                case "S": surveyMethod = SurveyMethod.Surveyed; break;
                default: surveyMethod = SurveyMethod.Estimated; return false;
            }
            return true;
        }
    }
}
