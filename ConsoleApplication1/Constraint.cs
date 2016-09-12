using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Extractors
{
    class Constraint
    {
        public static string matchListValues (string text, List<string> listValues)
        {
            for(int i=0; i< listValues.Count; i++)
            {
                if (text.Contains(listValues[i])){
                    return listValues[i];
                }
            }
            return "";
        }

        public static string matchRegEx(string text,Regex regex)
        {
            //Regex adressRegex = new Regex(@"^([\w]+)@([\w]+)\.([\w]+)");
           
            Match OneMatch = regex.Match(text);
            if (OneMatch.Success)
            {
                return OneMatch.Groups[0].Value;
            }
            return "";
        }

        public static bool compareValues (DateTime valueToCompare, DateTime lowestValue, DateTime HighestValue)
        {
            if (DateTime.Compare(valueToCompare, lowestValue) < 0)
            {
                return false;
            }

            if (DateTime.Compare(valueToCompare, HighestValue) > 0)
            {
                return false;
            }
            return true;
        }

        public static bool compareValues(double valueToCompare, double lowestValue, double HighestValue)
        {
            if (valueToCompare- lowestValue < 0)
            {
                return false;
            }

            if (valueToCompare- HighestValue > 0)
            {
                return false;
            }
            return true;
        }
    }
}
