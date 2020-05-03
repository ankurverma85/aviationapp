using System;
using System.Collections.Generic;
using System.Reflection;

namespace AviationApp.FAADataParser.Utils
{
    class FAADataParserGeneric<T> where T : new()
    {
        public static bool TryParse(
            string input,
            int expectedInputLength,
            List<(int fieldBegin, int fieldLength, Type parserType, string propertyName, bool nullable)> fieldList,
            out T output
            )
        {
            output = new T();
            Type t = output.GetType();
            if (input.Length != expectedInputLength)
            {
                return false;
            }
            foreach (var field in fieldList)
            {
                PropertyInfo property = t.GetProperty(field.propertyName);
                string fieldSubstring = input.Substring(field.fieldBegin, field.fieldLength).Trim();
                if (fieldSubstring.Length == 0)
                {
                    if (field.nullable)
                    {
                        property.SetValue(output, null);
                    }
                    else
                    { return false; }
                }
                else if (field.parserType == typeof(string))
                {
                    property.SetValue(output, fieldSubstring);
                }
                else
                {
                    MethodInfo method = field.parserType.GetMethod("TryParse");
                    object[] parameters = new object[] { fieldSubstring, null };
                    object result = method.Invoke(null, parameters);
                    bool boolResult = (bool)result;
                    if (boolResult)
                    {
                        property.SetValue(output, parameters[1]);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
