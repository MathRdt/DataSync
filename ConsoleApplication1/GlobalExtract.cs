using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GlobalExtract
    {
        public static MetaDatas extract (string extractType, string file, string app,string family)
        {
            MetaDatas metaDataValues =new MetaDatas();
            List<Famille> listFamilles;
            Conf conf;
            conf = Conf.Charger(Program.confFile);
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

                        famille = Extract_PDF.SearchTitleInList(file,familles);
                        if (famille == "") {
                            famille = Extract_PDF.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues.changeMetaData("fiducial:domainContainerFamille", famille, true);
                    }
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
                    metaDataValues.changeMetaData("fiducial:domainContainerSousFamille", sousFamille, true);
                    break;



                case "Word":
                    string path = Extract_Word.WordToPdf(file);
                    metaDataValues = extract("PDF", path, app, family);
                    break;


                case "Text":
                    if (famille == "")
                    {

                        famille = Extract_text.SearchTitleInList(file, familles);
                        if (famille == "")
                        {
                            famille = Extract_text.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues.changeMetaData("fiducial:domainContainerFamille", famille, true);
                    }
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
                    sousFamille = Extract_text.SearchTitleInList(file, sousFamilles);
                    if (sousFamille == "")
                    {
                        sousFamille = Extract_text.SearchWordInList(file, sousFamilles);
                        if (sousFamille == "") break;
                    }
                    metaDataValues.changeMetaData("fiducial:domainContainerSousFamille", sousFamille, true);
                    break;


                case "Odt":
                    if (famille == "")
                    {
                        
                        famille = Extract_ODT.SearchTitleInList(file, familles);
                        if (famille == "")
                        {
                            famille = Extract_ODT.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues.changeMetaData("fiducial:domainContainerFamille", famille, true);
                    }
                    
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
                    metaDataValues.changeMetaData("fiducial:domainContainerSousFamille", sousFamille, true);
                    break;
                case "Calc":
                    if (famille == "")
                    {
                        
                        famille = Extract_ODS.SearchTitleInList(file, familles);
                        if (famille == "")
                        {
                            famille = Extract_ODS.SearchWordInList(file, familles);
                            if (famille == "") break;
                        }
                        metaDataValues.changeMetaData("fiducial:domainContainerFamille", famille, true);
                    }
                    

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
                    metaDataValues.changeMetaData("fiducial:domainContainerSousFamille", sousFamille, true);
                    break;
                    
                case "Manuel":
                    Form1 form1 = new Form1();
                    int extension = file.LastIndexOf(".");
                    string XMLfile = file;
                    XMLfile = XMLfile + ".metadata";
                    GlobalMetaDatas globalMetaDatas = GlobalMetaDatas.Charger(XMLfile);
                    MetaDatas metadatas = globalMetaDatas.metadatas;
                    form1.fillForm(metadatas);
                    System.Windows.Forms.Application.Run(form1);
                    metaDataValues = form1.metadatasFromManual;
                    break;

            }
            return metaDataValues;
        }
    }
}
