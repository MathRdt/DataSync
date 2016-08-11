using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Famille
    {
        public string name { get; set; }
        public List<string> sousFamille { get; set; }

        public Famille()
        {
            name = "";
            sousFamille = new List<string>();
        }

        public Famille(string famille)
        {
            name = famille;
            sousFamille = new List<string>();
        }

        public Famille(string famille, string sousFamilleValue)
        {
            name = famille;
            sousFamille = new List<string>();
            this.sousFamille.Add(sousFamilleValue);
        }

        public void addSousFamille(string sousFamilleValue)
        {
            this.sousFamille.Add(sousFamilleValue);
        }
    }
}
