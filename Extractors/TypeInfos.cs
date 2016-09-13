using System.Collections.Generic;

namespace Extractors
{
    public class TypeInfos
    {
        public string typename;
        public List<Aspect> aspects;


        public TypeInfos()
        {
            typename = "";
            aspects = new List<Aspect>();
        }

        public TypeInfos(string typename)
        {
            this.typename = typename;
            aspects = new List<Aspect>();
        }
    }
}