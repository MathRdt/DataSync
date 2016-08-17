///<summary>
///ensemble de fonctions qui permettent d'extraire des méta-données d'un pdf
///1. fonction qui transforme pdf en chaine de caractère
///2. fonction qui cherche si un mot est dans le titre (ex : facture, ...)
///3. fonction qui cherche si les méta-données d'une liste sont dans le titre (ex : facture, bulletin, paye, ...)
///4. fonction qui cherche un mot dans le pdf
///5. fonction qui cherche si les méta-données d'une liste sont dans le pdf
///6. fonction qui cherche s'il y a un nom dans le pdf
///7. fonction qui cherche s'il y a un prénom dans le pdf
///8. fonction qui cherche s'il y a une méta-données précise dans le pdf (ex: code postal, ville, ...)
///9. fonction qui enlève les accents d'une chaine de caractères
/// </summary>


using System;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System.IO;
using System.Globalization;
using System.Text;

namespace ConsoleApplication1
{
    class Extract_PDF
    {
        /// <summary>
        /// //fonction qui extrait un pdf en chaine de caractère
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ExtractTextFromPdf(string path)
        {
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(path);
                PDFTextStripper stripper = new PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
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
            string nameSansAccent = RemoveAccent(Name);
            string nameToSearchSansAccent = " ";
            int i = 0;
            int taille = MetaDataList.Length;
            string nametosearch = " ";
            int result = 0;
            string resultTest = " ";
            int cpt = 0;
            for (i = 0; i < taille; i++)
            {
                nametosearch = MetaDataList[i].ToLower();//met le mot qu'on cherche en minuscule
                nameToSearchSansAccent = RemoveAccent(nametosearch);
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
        /// fonction qui va regarder si le mot qu'on cherche est dans le pdf retourne true si le mot est dans le pdf
        /// cherche le mot a partir de la lilste des méta-données que l'on connait
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MetaDataList"></param>
        /// <returns></returns>
        public static string SearchWordInList(string path, string[] MetaDataList)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            string chainePdfSansAccent = RemoveAccent(chainepdf);
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
                nameToSearchSansAccent = RemoveAccent(nametosearch);
                result = chainePdfSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre
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
        /// fonction qui regarder si le mot que l'on cherche est dans le titre du fichier retourne true si le mot est dans le titre du pdf
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NameToSearch"></param>
        /// <returns></returns>
        private static bool SearchTitle(string path, string NameToSearch)
        {
            FileInfo oFileInfo = new FileInfo(path);
            string name = oFileInfo.Name; //recupere nom fichier
            string Name = name.ToLower(); //met le titre en minuscule
            string nameSansAccent = RemoveAccent(Name);
            string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule
            string nameToSearchSansAccent = RemoveAccent(nametosearch);
            int result = nameSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre

            if (result > 0 || result == 0 ) return true; //si oui retourne vrai
            else return false; //sinon retourne faux
        }

        /// <summary>
        /// fonction qui va regarder si le mot qu'on cherche est dans le pdf retourne true si le mot est dans le pdf
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NameToSearch"></param>
        /// <returns></returns>
        private static bool SearchWord(string path, string NameToSearch)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            string chainePdfSansAccent = RemoveAccent(chainepdf);
            string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule
            string nameToSearchSansAccent = RemoveAccent(nametosearch);
            int result = chainePdfSansAccent.IndexOf(nameToSearchSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            
            if (result > 0 || result == 0) return true; //si oui retourne vrai
            else return false; //sinon retourne faux
        }

        /// <summary>
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string SearchName(string path)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            string chainePdfSansAccent = RemoveAccent(chainepdf);
            int result = chainePdfSansAccent.IndexOf("nom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = " ";

            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chainePdfSansAccent[result + i] == ':') break;
                }
                int j = 0;
                for (j = i+1; j < 20; j++)
                {
                    if (chainePdfSansAccent[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j+1; k < 20; k++)
                {
                    if (chainePdfSansAccent[result + k] == ' ' || chainePdfSansAccent[result + k] == '\n' || chainePdfSansAccent[result + k] == '\r' || chainePdfSansAccent[result + k] == '\t') break; //trouve fin du nom
                }
                name = chainepdf.Substring(result + j, k-j+1); //extrait le nom

            }
            else name = "nom non trouvé";

            return name;
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
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            string chainePdfSansAccent = RemoveAccent(chainepdf);
            string metadata = metaData.ToLower();
            string metaDataSansAccent = RemoveAccent(metadata); 
            int result = chainePdfSansAccent.IndexOf(metaDataSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = " ";

            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chainePdfSansAccent[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chainePdfSansAccent[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chainePdfSansAccent[result + k] == ' ' || chainePdfSansAccent[result + k] == '\n' || chainePdfSansAccent[result + k] == '\r' || chainePdfSansAccent[result + k] == '\t') break; //trouve fin du nom
                }
                name = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

            }
            else name = "nom non trouvé";

            return name;
        }

        /// <summary>
        /// fonction qui cherche le prénom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string SearchSurname(string path)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            string chainePdfSansAccent = RemoveAccent(chainepdf);
            int result = chainePdfSansAccent.IndexOf("prenom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string surname = " ";
            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chainePdfSansAccent[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chainePdfSansAccent[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chainePdfSansAccent[result + k] == ' ' || chainePdfSansAccent[result + k] == '\n' || chainePdfSansAccent[result + k] == '\r' || chainePdfSansAccent[result + k] == '\t') break; //trouve fin du nom
                }
                surname = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

            }
            else surname = "prenom non présent";

            return surname;
        }

        static void Main(string[] args)
        {
            string path = @"C:\Users\adminprojetindus\Documents\projet indus\fichier\testPdf.pdf";
            string aChercheTitre = "tést";

            //sotcke pdf dans une chaine de caractère
            string pdf = ExtractTextFromPdf(path);
            Console.WriteLine("pdf : \n" + pdf);

            //regarde si le mot à cherche est dans le titre du pdf
            bool isTitle = SearchTitle(path,aChercheTitre);
            Console.WriteLine("si true c'est que c'est dans le titre : " + isTitle);

            //regarde si le momt à chercher est dans le pdf
            string aChercheText = "FacTUre";
            bool isPdf = SearchWord(path, aChercheText);
            Console.WriteLine("trouvé dans texte : si true oui " + isPdf);

            //cherche si un nom est dans le document
            string name = SearchName(path);
            Console.WriteLine(name);

            //cherche si un prénom est dans le document
            string surname = SearchSurname(path);
            Console.WriteLine(surname);

            //test cherche si le titre fait partie d'une liste
            string[] listNom = { "toto", "titi","dfg", "tést" };
            string test = SearchTitleInList(path, listNom);
            Console.Write("test liste titre : " + test + "\n");

            //test cherche si un mot du pdf fait partie d'une liste
            string[] listNomPdf = { "PreNom", "titi", "gh","gh"};
            string testPdf = SearchWordInList(path, listNomPdf);
            Console.Write("test liste dans texte: " + testPdf + "\n");

            //test cherche méta-données
            string metaData = "postal";
            string retour = SearchMetaData(path, metaData);
            Console.WriteLine("code postal : " + retour + "\n");

        }
    }
}
