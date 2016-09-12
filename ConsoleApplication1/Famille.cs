using System.Collections.Generic;

namespace Extractors
{
    /// <summary>
    /// Classe famille, qui donne le nom de la famille ainsi que toutes les sous familles qui la composent
    /// </summary>
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
