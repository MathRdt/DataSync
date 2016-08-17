using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GlobalExtract
    {
        public string[] extract (string extractType, string file, string app,string family)
        {
            string[] metaDataValues =null;
            string chemin = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\appli1\famille1\sousfamille1\";
            string confFile = "confUpdate.xml";
            confFile = chemin + confFile;
            List<Famille> listFamilles;
            Conf conf;
            conf = Conf.Charger(confFile);
            listFamilles = conf.getListFamilles(app);
            string[] familles = new string[listFamilles.Count];

            for (int i = 0; i < listFamilles.Count; i++)
            {
                familles[i] = listFamilles[i].name;
            }
            string famille = family;


            string[] sousFamilles = null;
            string sousFamille;
            switch (extractType)
            {
                case "OCR":
                    break;

                case "PDF":
                    if (famille == "") {
                        metaDataValues = new string[2];
                        famille = Extract_PDF.SearchTitleInList(file,familles);
                        if (famille == "") {
                            famille = Extract_PDF.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues[0] = famille;
                    }
                    else metaDataValues = new string[1];

                    for (int i = 0; i < listFamilles.Count; i++)
                    {
                        if (listFamilles[i].name.Equals(famille))
                        {
                            sousFamilles = new string[listFamilles[i].sousFamille.Count];
                            for (int j = 0; j < listFamilles[i].sousFamille.Count; j++)
                            {
                                sousFamilles[j] = listFamilles[i].sousFamille[j];
                            }
                            break;
                        }
                    }
                    sousFamille = Extract_PDF.SearchTitleInList(file, sousFamilles);
                    if (sousFamille == "")
                    {
                        sousFamille = Extract_PDF.SearchWordInList(file, sousFamilles);
                        if (sousFamille == "") break;
                    }
                    metaDataValues[metaDataValues.Length -1] = sousFamille;
                    break;



                case "Word":
                    string path = Extract_Word.WordToPdf(file);
                    metaDataValues = extract("PDF", path, app, family);
                    break;
                case "Text":
                    break;
                case "Odt":
                    if (famille == "")
                    {
                        metaDataValues = new string[2];
                        famille = Extract_ODT.SearchTitleInList(file, familles);
                        if (famille == "")
                        {
                            famille = Extract_ODT.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues[0] = famille;
                    }
                    else metaDataValues = new string[1];

                    for (int i = 0; i < listFamilles.Count; i++)
                    {
                        if (listFamilles[i].name.Equals(famille))
                        {
                            sousFamilles = new string[listFamilles[i].sousFamille.Count];
                            for (int j = 0; j < listFamilles[i].sousFamille.Count; j++)
                            {
                                sousFamilles[j] = listFamilles[i].sousFamille[j];
                            }
                            break;
                        }
                    }
                    sousFamille = Extract_ODT.SearchTitleInList(file, sousFamilles);
                    if (sousFamille == "")
                    {
                        sousFamille = Extract_ODT.SearchWordInList(file, sousFamilles);
                        if (sousFamille == "") break;
                    }
                    metaDataValues[metaDataValues.Length - 1] = sousFamille;
                    break;
                case "Calc":
                    
                case "Excel":
                    if (famille == "")
                    {
                        metaDataValues = new string[2];
                        famille = Extract_ODS.SearchTitleInList(file, familles);
                        if (famille == "")
                        {
                            famille = Extract_ODS.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues[0] = famille;
                    }
                    else metaDataValues = new string[1];

                    for (int i = 0; i < listFamilles.Count; i++)
                    {
                        if (listFamilles[i].name.Equals(famille))
                        {
                            sousFamilles = new string[listFamilles[i].sousFamille.Count];
                            for (int j = 0; j < listFamilles[i].sousFamille.Count; j++)
                            {
                                sousFamilles[j] = listFamilles[i].sousFamille[j];
                            }
                            break;
                        }
                    }
                    sousFamille = Extract_ODS.SearchTitleInList(file, sousFamilles);
                    if (sousFamille == "")
                    {
                        sousFamille = Extract_ODS.SearchWordInList(file, sousFamilles);
                        if (sousFamille == "") break;
                    }
                    metaDataValues[metaDataValues.Length - 1] = sousFamille;
                    break;
                    
                case "Manuel":
                    break;

            }
            return metaDataValues;
        }
    }
}
