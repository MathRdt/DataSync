using System.Collections.Generic;

namespace ConsoleApplication1
{
    /// <summary>
    /// Classe famille, qui donne le nom de la famille ainsi que toutes les sous familles qui la composent
    /// </summary>
    public class Branche
    {
        public string name { get; set; }
        public List<string> societes { get; set; }

        public Branche()
        {
            name = "";
            societes = new List<string>();
        }

        public Branche(string branche)
        {
            name = branche;
            societes = new List<string>();
        }

        public Branche(string branche, string societeValue)
        {
            name = branche;
            societes = new List<string>();
            this.societes.Add(societeValue);
        }

        public void addSociete(string societeValue)
        {
            this.societes.Add(societeValue);
        }
    }
}
