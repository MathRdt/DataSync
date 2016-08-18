///<summary>
///ensemble de fonctions qui permettent d'extraire des méta-données d'un odt
///1. fonction qui transforme odt en chaine de caractère
///2. fonction qui cherche si un mot est dans le titre (ex : facture, ...)
///3. fonction qui cherche si les méta-données d'une liste sont dans le titre (ex : facture, bulletin, paye, ...)
///4. fonction qui cherche un mot dans le pdf
///5. fonction qui cherche si les méta-données d'une liste sont dans le pdf
///6. fonction qui cherche s'il y a un nom dans le pdf
///7. fonction qui cherche s'il y a un prénom dans le pdf
///8. fonction qui cherche s'il y a une méta-données précise dans le pdf (ex: code postal, ville, ...)
///9. fonction qui enlève les accents d'une chaîne de caractères
/// </summary>

using System;
using System.Text;
using AODL.Document.TextDocuments;
using AODL.Document.Content.Text;
using System.IO;
using System.Globalization;

namespace ConsoleApplication1
{
    class Extract_ODT
    {
        /// <summary>
        /// fonction qui transforme un odt en une chaine de caractère
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string OdtToString(TextDocument document)
        {
            var content = document.Content;
            string texte = " ";
            foreach (var item in content)
            {
                if (item is Paragraph)
                {
                    foreach (IText itemc in ((Paragraph)item).TextContent)
                    {
                        texte = texte + " " + itemc.Text;
                    }
                }
            }
            return texte;
        }
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
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NameToSearch"></param>
        /// <returns></returns>
        private static bool SearchWord(string path, string NameToSearch)
        {
            TextDocument doc = new TextDocument();
            doc.Load(path);
            string chaineOdt = OdtToString(doc);
            string chaineodt = chaineOdt.ToLower();//met le pdf en minuscule
            string chaineOdtSansAccent = RemoveAccent(chaineodt); //transforme le odt sans accent
            string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule
            string nameToSearchSansAccent = RemoveAccent(nametosearch); //transforme le nom à chercher sans accent

            int result = chaineOdtSansAccent.IndexOf(nameToSearchSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf

            if (result > 0 || result == 0) return true; //si oui retourne vrai
            else return false; //sinon retourne faux
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
            TextDocument doc = new TextDocument();
            doc.Load(path);
            string chaineOdt = OdtToString(doc);
            string chaineodt = chaineOdt.ToLower();//met le pdf en minuscule
            string chaineOdtSansAccent = RemoveAccent(chaineodt);//transforme le odt sans accent
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
                result = chaineOdtSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre
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
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NameToSearch"></param>
        /// <returns></returns>
        public static bool SearchTitle(string path, string NameToSearch)
        {
            FileInfo oFileInfo = new FileInfo(path);
            string name = oFileInfo.Name; //recupere nom fichier
            string Name = name.ToLower(); //met le titre en minuscule
            string nameSansAccent = RemoveAccent(Name);//transforme le nom du fichier sans accent
            string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule
            string nameToSearchSansAccent = RemoveAccent(nametosearch);//trnasforme le nom à chercher sans accent
            int result = nameSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre

            if (result > 0 || result == 0) return true; //si oui retourne vrai
            else return false; //sinon retourne faux
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

        /// <summary>
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string SearchName(string path)
        {
            TextDocument doc = new TextDocument();
            doc.Load(path);
            string chaineOdt = OdtToString(doc);
            string chaineodt = chaineOdt.ToLower();//met le pdf en minuscule
            string chaineOdtSansAccent = RemoveAccent(chaineodt);
            int result = chaineOdtSansAccent.IndexOf("nom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf

            string surname = " ";
            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chaineOdtSansAccent[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chaineOdtSansAccent[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chaineOdtSansAccent[result + k] == ' ' || chaineOdtSansAccent[result + k] == '\n' || chaineOdtSansAccent[result + k] == '\r' || chaineOdtSansAccent[result + k] == '\t') break; //trouve fin du nom
                }
                surname = chaineOdt.Substring(result + j, k - j + 1); //extrait le nom

            }
            else surname = "prenom non présent";

            return surname;
        }

        

        /// <summary>
        /// fonction qui cherche une méta-données en particulier
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        private static string SearchMetaData(string path, string metaData)
        {
            TextDocument doc = new TextDocument();
            doc.Load(path);
            string chaineOdt = OdtToString(doc);
            string chaineodt = chaineOdt.ToLower();//met le pdf en minuscule
            string chaineOdtSansAccent = RemoveAccent(chaineodt);
            string metadata = metaData.ToLower();
            string metaDataSansAccent = RemoveAccent(metadata);
            int result = chaineOdtSansAccent.IndexOf(metaDataSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = " ";

            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chaineOdtSansAccent[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chaineOdtSansAccent[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chaineOdtSansAccent[result + k] == ' ' || chaineOdtSansAccent[result + k] == '\n' || chaineOdtSansAccent[result + k] == '\r' || chaineOdtSansAccent[result + k] == '\t') break; //trouve fin du nom
                }
                name = chaineOdt.Substring(result + j, k - j + 1); //extrait le nom

            }
            else name = "nom non trouvé";

            return name;
        }
    }
}
