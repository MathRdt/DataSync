using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ExprReg
    {
        public string ValidMail(string text)
        {
           
            string[] splits;
            Regex adressRegex = new Regex(@"^([\w]+)@([\w]+)\.([\w]+)$");
            Regex splitRegex = new Regex(@"\s");
            //([\w]+) ==> caractère alphanumérique apparaissant une fois ou plus 
            splits = splitRegex.Split(text); // retourne true ou false selon la vérification
            foreach(string split in splits)
            {
                //Console.WriteLine(split);
                if (adressRegex.IsMatch(split))
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




    }
}
