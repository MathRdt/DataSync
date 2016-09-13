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
using System.Text.RegularExpressions;

namespace Extractors
{
    public class Extract_PDF
    {
        /// <summary>
        /// //fonction qui extrait un pdf en chaine de caractère
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// 
        public static string ExtractTextFromPdf(string path)
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


        public static string PDFtoString(string file)
        {
            string chainePdf = ExtractTextFromPdf(file); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            return GlobalExtract.RemoveAccent(chainepdf);

        }

        

        ///// <summary>
        ///// fonction qui regarder si le mot que l'on cherche est dans le titre du fichier retourne true si le mot est dans le titre du pdf
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="NameToSearch"></param>
        ///// <returns></returns>
        //public static bool SearchTitle(string path, string NameToSearch)
        //{
        //    FileInfo oFileInfo = new FileInfo(path);
        //    string name = oFileInfo.Name; //recupere nom fichier
        //    string Name = name.ToLower(); //met le titre en minuscule
        //    string nameSansAccent = RemoveAccent(Name);
        //    string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule
        //    string nameToSearchSansAccent = RemoveAccent(nametosearch);
        //    int result = nameSansAccent.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre

        //    if (result > 0 || result == 0 ) return true; //si oui retourne vrai
        //    else return false; //sinon retourne faux
        //}

        ///// <summary>
        ///// fonction qui va regarder si le mot qu'on cherche est dans le pdf retourne true si le mot est dans le pdf
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="NameToSearch"></param>
        ///// <returns></returns>
        //public static bool SearchWord(string path, string NameToSearch)
        //{
        //    string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
        //    string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
        //    string chainePdfSansAccent = RemoveAccent(chainepdf);
        //    string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule
        //    string nameToSearchSansAccent = RemoveAccent(nametosearch);
        //    int result = chainePdfSansAccent.IndexOf(nameToSearchSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            
        //    if (result > 0 || result == 0) return true; //si oui retourne vrai
        //    else return false; //sinon retourne faux
        //}

        ///// <summary>
        ///// fonction qui cherche le nom dans le document
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string SearchName(string path)
        //{
        //    string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
        //    string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
        //    string chainePdfSansAccent = RemoveAccent(chainepdf);
        //    int result = chainePdfSansAccent.IndexOf("nom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf
        //    string name = " ";

        //    if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
        //    {
        //        int i = 0;
        //        for (i = 0; i < 20; i++)
        //        {
        //            if (chainePdfSansAccent[result + i] == ':') break;
        //        }
        //        int j = 0;
        //        for (j = i+1; j < 20; j++)
        //        {
        //            if (chainePdfSansAccent[result + j] != ' ') break; //trouve le début du nom
        //        }
        //        int k = 0;
        //        for (k = j+1; k < 20; k++)
        //        {
        //            if (chainePdfSansAccent[result + k] == ' ' || chainePdfSansAccent[result + k] == '\n' || chainePdfSansAccent[result + k] == '\r' || chainePdfSansAccent[result + k] == '\t') break; //trouve fin du nom
        //        }
        //        name = chainepdf.Substring(result + j, k-j+1); //extrait le nom

        //    }
        //    else name = "nom non trouvé";

        //    return name;
        //}

       

        


        ///// <summary>
        ///// fonction qui cherche le prénom dans le document
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static string SearchSurname(string path)
        //{
        //    string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
        //    string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
        //    string chainePdfSansAccent = RemoveAccent(chainepdf);
        //    int result = chainePdfSansAccent.IndexOf("prenom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf
        //    string surname = " ";
        //    if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
        //    {
        //        int i = 0;
        //        for (i = 0; i < 20; i++)
        //        {
        //            if (chainePdfSansAccent[result + i] == ':') break;
        //        }
        //        int j = 0;
        //        for (j = i + 1; j < 20; j++)
        //        {
        //            if (chainePdfSansAccent[result + j] != ' ') break; //trouve le début du nom
        //        }
        //        int k = 0;
        //        for (k = j + 1; k < 20; k++)
        //        {
        //            if (chainePdfSansAccent[result + k] == ' ' || chainePdfSansAccent[result + k] == '\n' || chainePdfSansAccent[result + k] == '\r' || chainePdfSansAccent[result + k] == '\t') break; //trouve fin du nom
        //        }
        //        surname = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

        //    }
        //    else surname = "prenom non présent";

        //    return surname;
        //}

        
    }
}
