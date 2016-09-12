using System;

namespace Extractors
{
    [Serializable]
    public class Aspect
    {
        public string name;
        public MetaDatas metadatas { get; set; }

        public Aspect()
        {
            name = "";
            metadatas = new MetaDatas();
        }
    }
}
