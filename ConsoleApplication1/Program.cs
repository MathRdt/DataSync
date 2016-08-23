using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication1
{
    
    class Program
    {

        public static string chemin = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\app1\famille1\sousfamille1\";
        public static string cheminBis = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\app1\recette\paie\";
        public static string partialPath = @"C:\Users\projetindus\Documents\projetindus\CmisSync\branche1\societe1\app1\";
        public static string confFile = @"C:\Users\adminprojetindus\AppData\Roaming\cmissync\confUpdate.xml";

        /// <summary>
        /// foncion principale qui prends un fichier en entrée et en extrait les méta données dans un fichier XML, nous informe si le fichier est prêt à être synchronisé ou non 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool ExtractMetaDatas (string file)
        {
            int extension = file.LastIndexOf(".");
            string XMLfile = file + ".metadata";
            GlobalMetaDatas globalmetadatas = new GlobalMetaDatas();
            Conf conf;
            try
            {
                if (!File.Exists(XMLfile))
                {
                    GlobalMetaDatas.createXML(XMLfile);
                    globalmetadatas = GlobalMetaDatas.Charger(XMLfile);
                }
                else
                {
                    globalmetadatas = GlobalMetaDatas.Charger(XMLfile);

                    if (globalmetadatas.isComplete())
                    {
                        return true;
                    }
                }

                // Debut extraction chemin
                string[] stringPath = ExtractPath.conversion_path_xml(file);
                string[] stringMetaDatas = ExtractPath.getMetaData(stringPath);
                
                ExtractPath.fillPathMetaDatas(stringMetaDatas, globalmetadatas.metadatas);
                
                if (ReadyToSync.isReadyToSync())
                {
                    globalmetadatas.typename = "D:fiducial_" + globalmetadatas.metadatas.Mandatory[3].value + ":type_" + globalmetadatas.metadatas.Mandatory[4].value;
                    globalmetadatas.Enregistrer(XMLfile);
                    return true;
                }
                globalmetadatas.Enregistrer(XMLfile);
                // Debut extraction meta données fichier de conf
                conf = Conf.Charger(Program.confFile);
                if ((string)globalmetadatas.metadatas.Mandatory[3].value == "" || (string)globalmetadatas.metadatas.Mandatory[4].value == "")
                {
                    string fileMimeType = MimeTypes.GetContentType(file);
                    string[] extractors = conf.extractorsByType(fileMimeType);
                    for (int i = 0; i < extractors.Length; i++)
                    {
                        MetaDatas tempMetaDatas;
                        tempMetaDatas = GlobalExtract.extract(extractors[i], file, (string)globalmetadatas.metadatas.Mandatory[2].value, (string)globalmetadatas.metadatas.Mandatory[3].value);
                        for (int j = 0; j < tempMetaDatas.Mandatory.Count; j++)
                        {
                            globalmetadatas.metadatas.changeMetaData(tempMetaDatas.Mandatory[j].type, tempMetaDatas.Mandatory[j].value, true);
                        }
                        for (int j = 0; j < tempMetaDatas.Optional.Count; j++)
                        {
                            globalmetadatas.metadatas.changeMetaData(tempMetaDatas.Optional[j].type, tempMetaDatas.Optional[j].value, false);
                        }
                        if (globalmetadatas.isComplete() && !extractors[i].Equals("Manuel") )
                        {
                            GlobalExtract.extract("Manuel", file, (string)globalmetadatas.metadatas.Mandatory[2].value, (string)globalmetadatas.metadatas.Mandatory[3].value);
                            break;
                        }
                    }

                    globalmetadatas.Enregistrer(XMLfile);
                    
                    if (!globalmetadatas.isComplete())
                    {
                        return false;
                    }
                    else
                    {
                        globalmetadatas.typename = "D:fiducial_" + globalmetadatas.metadatas.Mandatory[3].value + ":type_" + globalmetadatas.metadatas.Mandatory[4].value;
                    }
                }
                else globalmetadatas.typename = "D:fiducial_" + globalmetadatas.metadatas.Mandatory[3].value + ":type_" + globalmetadatas.metadatas.Mandatory[4].value;
                globalmetadatas.getMetaDatasFromConf(conf, globalmetadatas.typename);
                globalmetadatas.Enregistrer(XMLfile);
                return true;

            }
            catch (NoPathFoundException e)
            {
                Console.WriteLine("{0}", e);
                return false;
            }
        }


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


                
                GlobalMetaDatas globalmetadatas = new GlobalMetaDatas();
                string[] stringPath;
                string[] stringMetaDatas;
                
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
                        System.Windows.Forms.Application.Run(form2);

                        break;

                    case 8:
                        string file = @"C:\Users\adminprojetindus\CmisSync\Branche\Societe\App1\recette\test.txt";
                        Program.ExtractMetaDatas(file);
                        break;
                    case 9:
                        string file9 = partialPath + "recette\\testDocx.docx.odt";
                        Program.ExtractMetaDatas(file9);
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
