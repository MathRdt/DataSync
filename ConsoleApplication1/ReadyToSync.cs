using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// liste de booléens qui nous infome si un ficher est prêt à être synchronisé ou non
    /// </summary>
    public static class ReadyToSync
    {
        static List<bool> metaDataDone {get;set;}

        static ReadyToSync()
        {
            
            ReadyToSync.metaDataDone = new List<bool>();
            
            for (int i=0; i < 6; i++)
            {
                metaDataDone.Add(false);
            }
        }
        
        public static void record (int i)
        {
            metaDataDone[i] = true;
        }

        

        public static void display()
        {
            for (int i = 0; i < metaDataDone.Count; i++)
            {
                Console.WriteLine(ReadyToSync.metaDataDone[i]);
            }
        }

        public static bool isReadyToSync()
        {
            for(int i =0;i< metaDataDone.Count; i++)
            {
                if (metaDataDone[i] == false) return false;
            }
            return true;
        }
         
    }
}
