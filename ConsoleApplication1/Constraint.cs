using System;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Constraint
    {
        public string stringRegEX(string text, Regex regEx)
        {
           
            string[] splits;
            //Regex adressRegex = new Regex(@"^([\w]+)@([\w]+)\.([\w]+)$");
            Regex splitRegex = new Regex(@"\s");
            //([\w]+) ==> caractère alphanumérique apparaissant une fois ou plus 
            splits = splitRegex.Split(text); // retourne true ou false selon la vérification
            foreach(string split in splits)
            {
                //Console.WriteLine(split);
                if (regEx.IsMatch(split))
                {
                    return split;
                }
            }
            return "no mail found";
        }

        public string ValidCP(string text)
        {

            string[] splits;
            Regex adressRegex = new Regex(@"\d{5}");
            Regex splitRegex = new Regex(@"\s");
            //([\w]+) ==> caractère alphanumérique apparaissant une fois ou plus 
            splits = splitRegex.Split(text); // retourne true ou false selon la vérification
            foreach (string split in splits)
            {
                //Console.WriteLine(split);
                if (adressRegex.IsMatch(split))
                {
                    return split;
                }
            }
            return "no CP found";
        }

        public string matchRegEx(string text,Regex regex)
        {
            //Regex adressRegex = new Regex(@"^([\w]+)@([\w]+)\.([\w]+)");
           
            Match OneMatch = regex.Match(text);
            if (OneMatch.Success)
            {
                return OneMatch.Groups[0].Value;
            }
            return "";
        }

        public Boolean compareValues (DateTime valueToCompare, DateTime lowestValue, DateTime HighestValue)
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

        public Boolean compareValues(Double valueToCompare, Double lowestValue, Double HighestValue)
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
