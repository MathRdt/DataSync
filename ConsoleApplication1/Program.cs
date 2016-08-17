using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication1
{

    class Program
    {
        [STAThreadAttribute]
        private static void Main()
        {
        System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            string UserEntry = "";
            do
            {
                int num = 0;
                bool IsOk = false;
                do
                {
                    Console.WriteLine("choisissez un test :");
                    Console.WriteLine("1- Test de création d'XML");
                    Console.WriteLine("2- Test de chemin");
                    Console.WriteLine("3- Test de parsing de PDF");
                    Console.WriteLine("4- Test de Lecture de fichier de conf");
                    Console.WriteLine("5- Test creation XML + lecture du chemin + extraction fichier de conf");
                    Console.WriteLine("6- Test windows form");
                    Console.WriteLine("7- Test windows form + chemin + extraction meta données fichier de conf ");
                    Console.WriteLine("8- Test creation XML + lecture du chemin + extraction fichier de conf");
                    UserEntry = Console.ReadLine();
                    IsOk = int.TryParse(UserEntry, out num);
                } while (!IsOk && UserEntry != "q" && num > 0 && num < 6);


                string chemin = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\app1\famille1\sousfamille1\";
                string cheminBis = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\app1\recette\paie\";
                string partialPath = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\app1\";
                GlobalMetaDatas globalmetadatas = new GlobalMetaDatas();
                string[] stringPath;
                string[] stringMetaDatas;
                string confFile = "confUpdate.xml";
                confFile = chemin + confFile;

                Conf conf;


                switch (num)
                {
                    case 1:
                        string file1 = "creationXML.xml";
                        file1 = chemin + file1;

                        if (!File.Exists(file1)) GlobalMetaDatas.createXML(file1);
                        else Console.WriteLine("le fichier " + file1 + " existe déjà");
                        break;

                    case 2:
                        string file2 = "testChemin.XML";
                        file2 = chemin + file2;
                        stringPath = ExtractPath.conversion_path_xml(chemin);
                        stringMetaDatas = ExtractPath.getMetaData(stringPath);
                        for (int i = 0; i < stringMetaDatas.Length; i++)
                        {
                            Console.WriteLine("metadonnee " + i + " : " + stringMetaDatas[i]);
                        }

                        break;

                    case 3:
                        string file3 = "testPourPdf.pdf";
                        file3 = cheminBis + file3;
                        string aChercheTitre = "pdf";

                        //sotcke pdf dans une chaine de caractère
                        string pdf = Extract_PDF.ExtractTextFromPdf(file3);
                        Console.WriteLine("pdf : \n" + pdf);

                        //regarde si le mot à cherche est dans le titre du pdf
                        bool isTitle = Extract_PDF.SearchTitle(file3, aChercheTitre);
                        Console.WriteLine("si true c'est que c'est dans le titre : " + isTitle);

                        //regarde si le momt à chercher est dans le pdf
                        string aChercheText = "FacTUre";
                        bool isPdf = Extract_PDF.SearchWord(file3, aChercheText);
                        Console.WriteLine("trouvé dans texte : si true oui " + isPdf);

                        //cherche si un nom est dans le document
                        string name = Extract_PDF.SearchName(file3);
                        Console.WriteLine(name);

                        //cherche si un prénom est dans le document
                        string surname = Extract_PDF.SearchSurname(file3);
                        Console.WriteLine(surname);

                        //test cherche si le titre fait partie d'une liste
                        string[] listNom = { "toto", "titi", "pdf", "nom" };
                        //bool test = Extract_PDF.SearchTitleInList(file3, listNom);
                        //Console.Write("test liste : " + test + "\n");

                        //test cherche si un mot du pdf fait partie d'une liste
                        string[] listNomPdf = { "toto", "titi", "pdf", "nom" };
                        //bool testPdf = Extract_PDF.SearchWordInList(file3, listNomPdf);
                        //Console.Write("test liste : " + testPdf + "\n");

                        //test cherche méta-données
                        string metaDataa = "postal";
                        string retour = Extract_PDF.SearchMetaData(file3, metaDataa);
                        Console.WriteLine("code postal : " + retour + "\n");
                        break;

                    case 4:
                        
                        string file4 = "metadatasFromXML.xml";
                        file4 = chemin + file4;
                        try
                        {
                            if (!File.Exists(file4))
                            {

                                GlobalMetaDatas.createXML(file4);
                            }
                            
                        
                            conf = Conf.Charger(confFile);
                            globalmetadatas = GlobalMetaDatas.Charger(file4);

                            globalmetadatas.getMetaDatasFromConf(conf, "fiducial_recette:type_paie");
                            globalmetadatas.Enregistrer(file4);
                        }
                        catch (NoPathFoundException e)
                        {
                            Console.WriteLine("{0}", e);
                        }
                        break;

                    case 5:
                        string file5 = "testPourPdf.pdf";

                        int extension = file5.LastIndexOf(".");
                        string XMLfile5 = file5.Remove(extension);
                        XMLfile5 = XMLfile5 + ".xml";

                        Console.WriteLine(XMLfile5);
                        file5 = cheminBis + file5;
                        XMLfile5 = cheminBis + XMLfile5;

                        

                        try
                        {
                            if (!File.Exists(XMLfile5))
                            {
                                GlobalMetaDatas.createXML(XMLfile5);
                                Console.WriteLine("fichier " + XMLfile5 + " a été créé");
                            }
                            else Console.WriteLine("fichier " + XMLfile5 + " était deja present");

                            globalmetadatas = GlobalMetaDatas.Charger(XMLfile5);

                            Console.WriteLine("debut extraction chemin");
                            stringPath = ExtractPath.conversion_path_xml(cheminBis);
                            stringMetaDatas = ExtractPath.getMetaData(stringPath);
                            for (int i = 0; i < stringMetaDatas.Length; i++)
                            {
                                Console.WriteLine("metadonnee " + i + " : " + stringMetaDatas[i]);
                            }

                            MetaDatas metaDatas = globalmetadatas.metadatas;
                            ExtractPath.fillPathMetaDatas(stringMetaDatas, metaDatas);

                            Console.WriteLine("debut extraction fichier de conf");

                            globalmetadatas.typename = "fiducial_" + metaDatas.Mandatory[3].value + ":type_" + metaDatas.Mandatory[4].value;

                            conf = Conf.Charger(confFile);
                            globalmetadatas.getMetaDatasFromConf(conf, globalmetadatas.typename);
                            globalmetadatas.Enregistrer(XMLfile5);

                            string fileMimeType = MimeSniffer.getMimeFromFile(file5);
                            Console.WriteLine(fileMimeType);
                            string[] extractors = conf.extractorsByType(fileMimeType);

                            for (int i=0; i< extractors.Length; i++)
                            {
                                Console.WriteLine(extractors[i]);
                            }

                        }
                        catch (NoPathFoundException e)
                        {
                            Console.WriteLine("{0}", e);
                        }
                        break;

                    default:
                        Console.WriteLine("Default case");
                    break;

                    case 6:
                        string file6 = "testChemin.XML";
                        file6 = chemin + file6;
                        stringPath = ExtractPath.conversion_path_xml(file6);
                       
                        stringMetaDatas = ExtractPath.getMetaData(stringPath);

                        //string[] stringMetadaDatasOnlyUntilApp = new string[3];
                        //stringMetadaDatasOnlyUntilApp[0] = stringMetaDatas[0];
                        //stringMetadaDatasOnlyUntilApp[1] = stringMetaDatas[1];
                        //stringMetadaDatasOnlyUntilApp[2] = stringMetaDatas[2];

                        conf = Conf.Charger(confFile);
                        List<Famille> listFamilles = conf.getListFamilles("App1");

                        Form1 form1 = new Form1() ;
                        form1.listFamilles = listFamilles;
                        //form1.fillForm(stringMetadaDatasOnlyUntilApp);




                        System.Windows.Forms.Application.Run(form1);
                        


                        break;

                    case 7:
                        string file7 = "testChemin.XML";
                        file7 = chemin + file7;
                        stringPath = ExtractPath.conversion_path_xml(file7);

                        stringMetaDatas = ExtractPath.getMetaData(stringPath);
                        Form1 form2 = new Form1();
                        //form2.fillForm(stringMetaDatas);
                        System.Windows.Forms.Application.Run(form2);
                        //for(int i = 0; i< form2.metadatasFromManual.Length; i++)
                        //{
                        //    Console.WriteLine(form2.metadatasFromManual[i]);
                        //}

                        break;

                    case 8:
                       
                        string file8 = "testPourPdf.pdf";

                        int extension8 = file8.LastIndexOf(".");
                        string XMLfile8 = file8.Remove(extension8);
                        XMLfile8 = XMLfile8 + ".xml";

                        //Console.WriteLine(XMLfile8);
                        file8 = partialPath + file8;
                        XMLfile8 = partialPath + XMLfile8;



                        try
                        {
                            if (!File.Exists(XMLfile8))
                            {
                                GlobalMetaDatas.createXML(XMLfile8);
                                globalmetadatas = GlobalMetaDatas.Charger(XMLfile8);
                                //Console.WriteLine("fichier " + XMLfile8 + " a été créé");
                            }
                            else
                            {
                                globalmetadatas = GlobalMetaDatas.Charger(XMLfile8);

                                if (globalmetadatas.isXMLComplete())
                                {
                                    Console.WriteLine("DOCUMENT OK POUR SYNCHRO APRES XML");
                                    break;
                                }
                            }

                            for (int i = 0; i < globalmetadatas.metadatas.Mandatory.Count; i++)
                            {
                                Console.WriteLine(globalmetadatas.metadatas.Mandatory[i].type);
                                Console.WriteLine(globalmetadatas.metadatas.Mandatory[i].value);
                            }

                            Console.WriteLine("debut extraction chemin");
                            stringPath = ExtractPath.conversion_path_xml(file8);
                            stringMetaDatas = ExtractPath.getMetaData(stringPath);
                            //for (int i = 0; i < stringMetaDatas.Length; i++)
                            //{
                            //    Console.WriteLine("metadonnee " + i + " : " + stringMetaDatas[i]);
                            //}
                            ExtractPath.fillPathMetaDatas(stringMetaDatas, globalmetadatas.metadatas);
                            globalmetadatas.Enregistrer(XMLfile8);
                            if (ReadyToSync.isReadyToSync())
                            {
                                Console.WriteLine("DOCUMENT OK POUR SYNCHRO APRES CHEMIN");
                                break;
                            }
                            Console.WriteLine("debut extraction fichier de conf");
                            conf = Conf.Charger(confFile);
                            if ((string)globalmetadatas.metadatas.Mandatory[3].value == "" || (string)globalmetadatas.metadatas.Mandatory[4].value == "")
                            {

                                string fileMimeType = MimeSniffer.getMimeFromFile(file8);
                                string[] extractors = conf.extractorsByType(fileMimeType);
                                for (int i = 0; i < extractors.Length; i++)
                                {
                                    MetaDatas tempMetaDatas;
                                    tempMetaDatas = GlobalExtract.extract(extractors[i], file8, (string)globalmetadatas.metadatas.Mandatory[2].value, "");
                                    for (int j = 0; j < tempMetaDatas.Mandatory.Count; j++)
                                    {
                                        globalmetadatas.metadatas.changeMetaData(tempMetaDatas.Mandatory[j].type, tempMetaDatas.Mandatory[j].value, true);
                                    }
                                    for (int j = 0; j < tempMetaDatas.Optional.Count; j++)
                                    {
                                        globalmetadatas.metadatas.changeMetaData(tempMetaDatas.Optional[j].type, tempMetaDatas.Optional[j].value, false);
                                    }
                                    globalmetadatas.Enregistrer(XMLfile8);
                                }
                                if ((string)globalmetadatas.metadatas.Mandatory[3].value == "" || (string)globalmetadatas.metadatas.Mandatory[4].value == "")
                                {
                                    Console.WriteLine("FICHIER IMPOSSIBLE A SYNCHRO");
                                    break;
                                }
                                else
                                {
                                    globalmetadatas.typename = "fiducial_" + globalmetadatas.metadatas.Mandatory[3].value + ":type_" + globalmetadatas.metadatas.Mandatory[4].value;
                                    Console.WriteLine("type de fichier à synchro " + globalmetadatas.typename);
                                }
                            }
                            else globalmetadatas.typename = "fiducial_" + globalmetadatas.metadatas.Mandatory[3].value + ":type_" + globalmetadatas.metadatas.Mandatory[4].value;
                            globalmetadatas.getMetaDatasFromConf(conf, globalmetadatas.typename);
                            globalmetadatas.Enregistrer(XMLfile8);


                        }
                        catch (NoPathFoundException e)
                        {
                            Console.WriteLine("{0}", e);
                        }
                        break;
                }
                do
                {
                    Console.WriteLine("voulez vous essayer un autre test ?");
                    Console.WriteLine("o - oui");
                    Console.WriteLine("n - non");
                    UserEntry = Console.ReadLine();
                } while (UserEntry != "n" && UserEntry != "o");

            } while (UserEntry != "n");
        }
    }

}
