using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Extractors
{
    public class GlobalExtract
    {
        /// <summary>
        /// permet de réaliser l'extraction suivant les différents modules que l'on a implanté
        /// </summary>
        /// <param name="extractType">type d'extraction à réaliser</param>
        /// <param name="file">le fichier sur lequel l'extraction va être réalisée</param>
        /// <param name="app"></param>
        /// <param name="family">permet de donner la famille du fichier si on la connait déjà</param>
        /// <returns></returns>
        public static void extract(string extractType, string file, string app, GlobalMetaDatas globalMetaDatas)
        {
            string text = "";

            switch (extractType)
            {
                case "OCR":
                    break;

                case "PDF":
                    text = Extract_PDF.PDFtoString(file);
                    break;



                case "Word":
                    string filePDF = Extract_Word.WordToPdf(file);
                    extract("PDF", filePDF, app, globalMetaDatas);
                    File.Delete(filePDF);
                    return;


                case "Text":
                    text = Extract_text.TxtToString(file);
                    break;
                case "Odt":
                    text = Extract_ODT.ODTtoString(file);
                    break;
                case "Calc":
                    text = Extract_ODS.ODStoString(file);
                    break;
                case "Manuel":
                    Form1 form1 = null;
                    try
                    {
                        form1 = new Form1();
                        form1.AllowDrop = false;
                        int extension = file.LastIndexOf(".");
                        string XMLfile = file;
                        XMLfile = XMLfile + ".metadata";
                        MetaDatas metadatas = globalMetaDatas.metadatas;
                        form1.fillForm(metadatas);
                        System.Windows.Forms.Application.Run(form1);
                    }
                    catch (Exception e)
                    {
                    }
                    globalMetaDatas.metadatas = form1.metadatasFromManual;
                    return;
            }

            string famille = globalMetaDatas.getFamille();
            string sousFamille = globalMetaDatas.getSousFamille();
            if (sousFamille == "")
            {
                List<Famille> listFamilles;
                Conf conf;
                conf = Conf.Charger(Program.confFile);
                listFamilles = conf.getListFamilles(app);
                List<string> familles = new List<string>(listFamilles.Count);

                for (int i = 0; i < listFamilles.Count; i++)
                {
                    familles.Add(listFamilles[i].name);
                }
                List<string> sousFamilles = new List<string>();

                if (famille == "")
                {

                    famille = SearchTitleInList(file, familles);
                    if (famille == "")
                    {
                        famille = SearchWordInList(file, familles);
                        if (famille != "") globalMetaDatas.metadatas.changeMetaData("fiducial:domainContainerFamille", famille, true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");

                    }
                    else globalMetaDatas.metadatas.changeMetaData("fiducial:domainContainerFamille", famille, true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");

                }
                for (int i = 0; i < listFamilles.Count; i++)
                {
                    if (listFamilles[i].name.Equals(famille))
                    {
                        sousFamilles = new List<string>(listFamilles[i].sousFamille.Count);
                        for (int j = 0; j < listFamilles[i].sousFamille.Count; j++)
                        {
                            sousFamilles.Add(listFamilles[i].sousFamille[j]);
                        }
                        break;
                    }
                }
                sousFamille = SearchTitleInList(file, sousFamilles);
                if (sousFamille == "")
                {
                    sousFamille = SearchWordInList(file, sousFamilles);
                    if (sousFamille != "") globalMetaDatas.metadatas.changeMetaData("fiducial:domainContainerSousFamille", sousFamille, true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");
                }
                else globalMetaDatas.metadatas.changeMetaData("fiducial:domainContainerSousFamille", sousFamille, true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");
            }

            foreach (MetaData metaData in globalMetaDatas.metadatas.Optional)
            {

                if (metaData.listValues.Equals(new List<string>()))
                {
                    if (metaData.keyWords.Equals(new List<string>()))
                    {
                        for (int i = 0; i < metaData.keyWords.Count; i++)
                        {
                            string result = SearchMetaDataInList(text, metaData.keyWords[i], metaData.listValues);
                            if (result != "")
                            {
                                if (metaData.valueType == "date")
                                {
                                    DateTime dt = Convert.ToDateTime(result);
                                    metaData.value = dt;
                                }
                                else if (metaData.valueType == "int")
                                {
                                    int value = Convert.ToInt32(result);
                                    Console.WriteLine(metaData.min.GetType() + " = " + metaData.min);
                                    if (metaData.min.ToString() != "" || metaData.max.ToString() != "")
                                    {
                                        if (metaData.min.ToString() != "" && metaData.max.ToString() != "")
                                        {
                                            if (Constraint.compareValues((double)value, (double)metaData.min, (double)metaData.max))
                                            {
                                                metaData.value = value;
                                            }
                                        }
                                        if (metaData.min.ToString() != "")
                                        {
                                            if (Constraint.compareValues((double)value, (double)metaData.min, Double.MaxValue))
                                            {
                                                metaData.value = value;
                                            }
                                        }

                                        if (Constraint.compareValues((double)value, Double.MinValue, (double)metaData.max))
                                        {
                                            metaData.value = value;
                                        }
                                    }
                                    else metaData.value = value;
                                }
                                else
                                {
                                    metaData.value = result;
                                }
                            }
                        }
                    }
                    else
                    {

                        string result = SearchWordInList(text, metaData.listValues);
                        if (result != "")
                        {
                            if (metaData.valueType == "date")
                            {
                                DateTime dt = Convert.ToDateTime(result);
                                metaData.value = dt;
                            }
                            else if (metaData.valueType == "int")
                            {
                                int value = Convert.ToInt32(result);
                                if (metaData.min.ToString() != "" || metaData.max.ToString() != "")
                                {
                                    if (metaData.min.ToString() != "" && metaData.max.ToString() != "")
                                    {
                                        if (Constraint.compareValues((double)value, (double)metaData.min, (double)metaData.max))
                                        {
                                            metaData.value = value;
                                        }
                                    }
                                    if (metaData.min.ToString() != "")
                                    {
                                        if (Constraint.compareValues((double)value, (double)metaData.min, Double.MaxValue))
                                        {
                                            metaData.value = value;
                                        }
                                    }

                                    if (Constraint.compareValues((double)value, Double.MinValue, (double)metaData.max))
                                    {
                                        metaData.value = value;
                                    }
                                }
                                else metaData.value = value;
                            }
                            else
                            {
                                metaData.value = result;
                            }
                        }
                    }
                }
                else if (metaData.regEx != "")
                {

                    Regex regex = new Regex(metaData.regEx);
                    Console.WriteLine("Helloo !:" + regex);
                    if (metaData.keyWords.Equals(new List<string>()))
                    {

                        for (int i = 0; i < metaData.keyWords.Count; i++)
                        {
                            string result = SearchMetaDataRegEx(text, metaData.keyWords[i], regex);
                            if (result != "")
                            {
                                if (metaData.valueType == "date")
                                {
                                    DateTime dt = Convert.ToDateTime(result);
                                    metaData.value = dt;
                                }
                                else if (metaData.valueType == "int")
                                {
                                    int value = Convert.ToInt32(result);
                                    if (metaData.min.ToString() != "" || metaData.max.ToString() != "")
                                    {
                                        if (metaData.min.ToString() != "" && metaData.max.ToString() != "")
                                        {
                                            if (Constraint.compareValues((double)value, (double)metaData.min, (double)metaData.max))
                                            {
                                                metaData.value = value;
                                            }
                                        }
                                        if (metaData.min.ToString() != "")
                                        {
                                            if (Constraint.compareValues((double)value, (double)metaData.min, Double.MaxValue))
                                            {
                                                metaData.value = value;
                                            }
                                        }

                                        if (Constraint.compareValues((double)value, Double.MinValue, (double)metaData.max))
                                        {
                                            metaData.value = value;
                                        }
                                    }
                                    else metaData.value = value;
                                }
                                else
                                {
                                    metaData.value = result;
                                }
                            }
                        }
                    }
                    else
                    {
                        string result = Constraint.matchRegEx(text, regex);
                        Console.WriteLine(text);
                        if (result != "")
                        {
                            if (metaData.valueType == "date")
                            {
                                DateTime dt = Convert.ToDateTime(result);
                                metaData.value = dt;
                            }
                            else if (metaData.valueType == "int")
                            {
                                int value = Convert.ToInt32(result);
                                if (metaData.min.ToString() != "" || metaData.max.ToString() != "")
                                {
                                    if (metaData.min.ToString() != "" && metaData.max.ToString() != "")
                                    {
                                        if (Constraint.compareValues((double)value, (double)metaData.min, (double)metaData.max))
                                        {
                                            metaData.value = value;
                                        }
                                    }
                                    if (metaData.min.ToString() != "")
                                    {
                                        if (Constraint.compareValues((double)value, (double)metaData.min, Double.MaxValue))
                                        {
                                            metaData.value = value;
                                        }
                                    }

                                    if (Constraint.compareValues((double)value, Double.MinValue, (double)metaData.max))
                                    {
                                        metaData.value = value;
                                    }
                                }
                                else metaData.value = value;

                            }
                            else
                            {
                                metaData.value = result;
                            }
                        }
                    }

                }
                else
                {
                    //Console.WriteLine("coucou");
                    if (!metaData.keyWords.Equals(new List<string>()))
                    {
                        for (int i = 0; i < metaData.keyWords.Count; i++)
                        {
                            string result = SearchMetaData(text, metaData.keyWords[i]);
                            if (result != "")
                            {
                                if (metaData.valueType == "date")
                                {
                                    DateTime dt = Convert.ToDateTime(result);
                                    metaData.value = dt;
                                }
                                else if (metaData.valueType == "int")
                                {
                                    int value = Convert.ToInt32(result);
                                    if (metaData.min.ToString() != "" || metaData.max.ToString() != "")
                                    {
                                        if (metaData.min.ToString() != "" && metaData.max.ToString() != "")
                                        {
                                            if (Constraint.compareValues((double)value, (double)metaData.min, (double)metaData.max))
                                            {
                                                metaData.value = value;
                                            }
                                        }
                                        if (metaData.min.ToString() != "")
                                        {
                                            if (Constraint.compareValues((double)value, (double)metaData.min, Double.MaxValue))
                                            {
                                                metaData.value = value;
                                            }
                                        }

                                        if (Constraint.compareValues((double)value, Double.MinValue, (double)metaData.max))
                                        {
                                            metaData.value = value;
                                        }
                                    }
                                    else metaData.value = value;
                                }
                                else
                                {
                                    metaData.value = result;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// fonction qui regarder si le mot que l'on cherche est dans le titre du fichier retourne true si le mot est dans le titre du pdf
        ///cherche le mot a partir de la lilste des méta-données que l'on connait
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MetaDataList"></param>
        /// <returns></returns>
        public static string SearchTitleInList(string path, List<string> MetaDataList)
        {
            FileInfo oFileInfo = new FileInfo(path);
            string name = oFileInfo.Name; //recupere nom fichier
            string Name = name.ToLower(); //met le titre en minuscule
            string nameSansAccent = RemoveAccent(Name);
            string nameToSearchSansAccent = "";
            int i = 0;
            int taille = MetaDataList.Count;
            string nametosearch = "";
            int result = 0;
            string resultTest = "";
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
        public static string SearchWordInList(string text, List<string> MetaDataList)
        {

            int i = 0;
            int taille = MetaDataList.Count;
            string nametosearch = "";
            string nameToSearchSansAccent = "";
            int result = 0;
            string resultTest = "";
            int cpt = 0;
            for (i = 0; i < taille; i++)
            {
                nametosearch = MetaDataList[i].ToLower();//met le mot qu'on cherche en minuscule
                nameToSearchSansAccent = RemoveAccent(nametosearch);
                result = text.IndexOf(nameToSearchSansAccent);//regarde si le mot qu'on cherche est sous chaine du titre
                if (result > 0 || result == 0)
                {
                    resultTest = nametosearch;
                    cpt++;
                    break;
                }
                else resultTest = "";
            }
            Console.WriteLine(resultTest);
            return resultTest;

        }

        /// <summary>
        /// fonction qui cherche une méta-données en particulier
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public static string SearchMetaData(string text, string keyWord)
        {
            string keyword = keyWord.ToLower();
            string keywordSansAccent = RemoveAccent(keyword);
            int result = text.IndexOf(keywordSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = "";

            if (result >= 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                int length = text.Length;

                for (i = 0; i < Math.Min(20, length - result); i++)
                {
                    if (text[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < Math.Min(20, length - (result + i)); j++)
                {
                    if (text[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < Math.Min(20, length - (result + i + j)); k++)
                {
                    if (text[result + k] == ' ' || text[result + k] == '\n' || text[result + k] == '\r' || text[result + k] == '\t') break; //trouve fin du nom
                }
                name = text.Substring(result + j, k - j + 1); //extrait le nom

            }
            Console.WriteLine(name);
            return name;
        }


        /// <summary>
        /// fonction qui cherche une méta-données en particulier
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public static string SearchMetaDataRegEx(string text, string keyWord, Regex regex)
        {
            string keyword = keyWord.ToLower();
            string keywordSansAccent = RemoveAccent(keyword);
            int result = text.IndexOf(keywordSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = "";
            Console.WriteLine("passage par metadataregex");
            if (result >= 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                int length = text.Length;
                for (i = 0; i < Math.Min(20, length - result); i++)
                {
                    if (text[result + i] == ':') break;
                }
                text = text.Substring(result + i + 1);
                name = Constraint.matchRegEx(text, regex);
            }
            Console.WriteLine(name);
            return name;
        }

        /// <summary>
        /// fonction qui cherche une méta-données en particulier
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public static string SearchMetaDataInList(string text, string keyWord, List<string> MetaDataList)
        {
            string keyword = keyWord.ToLower();
            string keywordSansAccent = RemoveAccent(keyword);
            int result = text.IndexOf(keywordSansAccent); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = "";

            if (result >= 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                int length = text.Length;
                for (i = 0; i < Math.Min(20, length - result); i++)
                {
                    if (text[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < Math.Min(20, length - (result + i)); j++)
                {
                    if (text[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < Math.Min(20, length - (result + i + j)); k++)
                {
                    if (text[result + k] == ' ' || text[result + k] == '\n' || text[result + k] == '\r' || text[result + k] == '\t') break; //trouve fin du nom
                }
                name = text.Substring(result + j, k - j + 1); //extrait le nom

                for (int m = 0; m < MetaDataList.Count; m++)
                {
                    string nametosearch = MetaDataList[m].ToLower();//met le mot qu'on cherche en minuscule
                    string nameToSearchSansAccent = RemoveAccent(nametosearch);

                    if (name == nameToSearchSansAccent)
                    {
                        Console.WriteLine(name);
                        return name;
                    }
                }
            }
            Console.WriteLine(name);
            return name;
        }

        /// <summary>
        /// fonction qui permet de transformer une chaine de caractères en lui enlevant tous ces accents
        /// </summary>
        /// <param name="stIn"></param>
        /// <returns></returns>
        public static string RemoveAccent(string stIn)
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

    }
}
