using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Extract_text
    {
        /// <summary>
        /// fonction qui permet de transformer une chaine de caractères en lui enlevant tous ces accents
        /// </summary>
        /// <param name="stIn"></param>
        /// <returns></returns>
        static string RemoveAccent(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// fonction qui va regarder si le mot qu'on cherche est dans le pdf retourne true si le mot est dans le pdf
        /// cherche le mot a partir de la lilste des méta-données que l'on connait
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MetaDataList"></param>
        /// <returns></returns>
        public static string SearchWordInList(string path, string[] MetaDataList)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException(path + " not found");
            string chaineTxt = File.ReadAllText(path);
            chaineTxt = chaineTxt.ToLower();//met le pdf en minuscule
            string chaineTxtSansAccent = RemoveAccent(chaineTxt);//transforme le odt sans accent
            int i = 0;
            int taille = MetaDataList.Length;
            string nametosearch = " ";
            string nameToSearchSansAccent = " ";
            string resultR = " ";
            int result = 0;
            int cpt = 0;
            for (i = 0; i < taille; i++)
            {
                nametosearch = MetaDataList[i].ToLower();//met le mot qu'on cherche en minuscule
                nameToSearchSansAccent = RemoveAccent(nametosearch);//transforme le mot à chercher sans accent
                result = chaineTxtSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre
                if (result > 0 || result == 0)
                {
                    resultR = nametosearch;
                    cpt++;
                    break;
                }
                else resultR = "";
            }
            return resultR;

        }

        /// <summary>
        /// fonction qui regarder si le mot que l'on cherche est dans le titre du fichier retourne true si le mot est dans le titre du pdf
        ///cherche le mot a partir de la lilste des méta-données que l'on connait
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MetaDataList"></param>
        /// <returns></returns>
        public static string SearchTitleInList(string path, string[] MetaDataList)
        {
            FileInfo oFileInfo = new FileInfo(path);
            string name = oFileInfo.Name; //recupere nom fichier
            string Name = name.ToLower(); //met le titre en minuscule
            string nameSansAccent = RemoveAccent(Name);//transforme le nom du fichier sans accent

            int i = 0;
            int taille = MetaDataList.Length;
            string nametosearch = " ";
            string nameToSearchSansAccent = " ";
            int result = 0;
            string resultTest = " ";
            int cpt = 0;
            for (i = 0; i < taille; i++)
            {
                nametosearch = MetaDataList[i].ToLower();//met le mot qu'on cherche en minuscule
                nameToSearchSansAccent = RemoveAccent(nametosearch);//transforme le nom à chercher sans accent
                result = nameSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre
                if (result > 0 || result == 0)
                {
                    resultTest = nametosearch;
                    cpt++;
                    break;
                }
                else resultTest = "";
            }
            return resultTest;
        }

    }
}
