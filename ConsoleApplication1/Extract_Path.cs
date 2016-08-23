using System;
using System.IO;

namespace ConsoleApplication1
{
    class ExtractPath
    {
        public static string getPath(string path)
        {
            if (!Directory.Exists(path))
                throw new NoPathFoundException();
            string filePath = Path.GetFullPath(path);
            return filePath;
        }

        ///<summary>
        ///fonction qui récupère chaque composante du chemin
        /// </summary>
        public static string[] conversion_path_xml(string filePath)
        {
            string[] words = filePath.Split('\\'); //permet de séparer les répertoires et les stocke dans un tableau
            return words;
        }

        ///<summary>
        ///fonction qui permet de récupérer l'indice à partir duquel les méta-données nous intéressent (branche, société, ...)
        /// </summary>
        public static int getNbMetaData(string[] path) //paramètre : tableau contenant les répertoires donné par conversion_path_xml
        {
            int comptMetaData = 0;
            int j = 0;
            int taille = path.Length; //donne la taille du tableau contenant les répertoires
            string[] pathbis = new string[taille];
            for (j = 0; j < taille; j++)
            {
                pathbis[j] = path[j].ToLower(); //réécrit la chaine de caractère en minuscule
                if (pathbis[j] != "cmissync") comptMetaData++; //compte le nb de répertoire qu'il y a jusqu'à celui correspndant à CmisSync
                else break;
            }

            return comptMetaData + 1; //permet de récupérer l'élément juste après CmisSync
        }

        ///<summary>
        ///renvoi un tableau qui contient les méta-données qui nous intéressent (branche, société, ...)
        /// </summary>
        public static string[] getMetaData(string[] path)
        {
            int nbMeta = getNbMetaData(path); //récupération de l'induce de début des méta-données
            int taille = path.Length;
            string[] metaData = new string[taille - nbMeta]; //chaine de la taille des méta-données
            int i = 0;
            for (i = nbMeta; i < taille; i++)
            {
                metaData[i - nbMeta] = path[i]; //remplissage méta-données
            }
            return metaData;
        }

        /// <summary>
        /// convertit les strings de meta données en MetaDatas
        /// </summary>
        /// <param name="stringMetaDatas">string à convertir</param>
        /// <param name="metadatas">méta données qui vont être complétées par cette fonction</param>
        public static void fillPathMetaDatas(string[] stringMetaDatas, MetaDatas metadatas)
        {

            if (stringMetaDatas.Length < 4)
            {
                throw new NoPathFoundException();
            }
            metadatas.changeMetaData("fiducial:domainContainerBranche", stringMetaDatas[0],true);
            metadatas.changeMetaData("fiducial:domainContainerSociete", stringMetaDatas[1], true);
            metadatas.changeMetaData("fiducial:domainContainerApplication", stringMetaDatas[2], true);
            ReadyToSync.record(0);
            ReadyToSync.record(1);
            ReadyToSync.record(2);

            if (stringMetaDatas.Length >= 5)
            {
                metadatas.changeMetaData("fiducial:domainContainerFamille", stringMetaDatas[3], true);
                ReadyToSync.record(3);
                if (stringMetaDatas.Length >= 6) { 
                    metadatas.changeMetaData("fiducial:domainContainerSousFamille", stringMetaDatas[4], true);
                    ReadyToSync.record(4);
                }
                else
                    metadatas.changeMetaData("fiducial:domainContainerSousFamille", "", true);

            }
            else
            {
                metadatas.changeMetaData("fiducial:domainContainerFamille", "", true);
                metadatas.changeMetaData("fiducial:domainContainerSousFamille", "", true);
            }
            metadatas.changeMetaData("fiducial:domainContainerNom", stringMetaDatas[stringMetaDatas.Length - 1], true);

        }
    }
}
