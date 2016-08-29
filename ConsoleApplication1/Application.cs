using System.Collections.Generic;
using System.Web;

namespace ConsoleApplication1
{
    public class Application
    {
        public string user;
        public string password;
        public List<FolderName> folders;
        public List<TypeInfos> typeInfos;

        public Application()
        {
            user = "";
            password = "";
        }

        public Application(string user, string password, List<FolderName> folders, List<TypeInfos> typeInfos)
        {
            this.user = user;
            this.password = password;
            this.folders = folders;
            this.typeInfos = typeInfos;
        }
    }
}