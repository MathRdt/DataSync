using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public class Conf
    {
        public string url;
        public string repository;
        public List<Application> applications;
        public List<TypeExtracteurs> typesExtracteurs;

        public Conf()
        {
            url = "";
            repository = "";
            applications = new List<Application>();
        }

        public Conf(string url, string repository, List<Application> applications, List<TypeInfos> mandatoryMetadatas, List<TypeExtracteurs> typesExtracteurs)
        {
            this.url = url;
            this.repository = repository;
            this.applications = applications;
            this.typesExtracteurs = typesExtracteurs;
        }

        public Conf(string url, string repository)
        {
            this.url = url;
            this.repository = repository;
            this.applications = new List<Application>();
            this.typesExtracteurs = new List<TypeExtracteurs>();
        }



        /// <summary>
        /// Enregistre l'état courant de la classe dans un fichier au format XML.
        /// </summary>
        /// <param name="chemin">chemin pour enregistrer le fichier XML (jusqu'au nom du fichier .xml)</param>
        /// <example> 
        /// Mise en oeuvre : 
        /// <code> 
        ///   Metadatas metadatas1 = new Metadatas();
        ///   ...
        ///   metadatas1.Enregistrer("C:\\tmp\\test.xml");
        /// </code> 
        /// </example>
        public void Enregistrer(string chemin)
        {
            //if (!File.Exists(chemin))
            //{
            //    throw new NoPathFoundException();
            //}
            XmlSerializer serializer = new XmlSerializer(typeof(Conf));
            StreamWriter writer = new StreamWriter(chemin);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// déserialise un fichier XML, re donne le fichier XML sous forme de classes C#
        /// </summary>
        /// <param name="chemin">chemin du fichier XML à déserialiser</param>
        /// <returns>retourne la liste des méta données du fichier observé</returns>
        public static Conf Charger(string chemin)
        {
            if (!File.Exists(chemin))
            {
                throw new NoPathFoundException();
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(Conf));
            StreamReader reader = new StreamReader(chemin);
            Conf conf = (Conf)deserializer.Deserialize(reader);
            reader.Close();

            return conf;
        }
        
        public void changeMetaData(string fileName, string typeMetaData, object valueMetaData, Boolean mandatory)
        {
            int i = 0;

            GlobalMetaDatas globalmetadatas = GlobalMetaDatas.Charger(fileName);
            

            if (mandatory == true)
            {
                for (i = 0; i < globalmetadatas.metadatas.Mandatory.Count; i++)
                {
                    if (globalmetadatas.metadatas.Mandatory[i].type == typeMetaData)
                    {
                        globalmetadatas.metadatas.Mandatory[i].value = valueMetaData;
                        break;
                    }
                }
            }

            else
            {
                for (i = 0; i < globalmetadatas.metadatas.Optional.Count; i++)
                {
                    if (globalmetadatas.metadatas.Optional[i].type == typeMetaData)
                    {
                        globalmetadatas.metadatas.Optional[i].value = valueMetaData;
                        break;
                    }
                }
            }
            globalmetadatas.Enregistrer(fileName);
        }



        public string[] extractorsByType (string mimeType)
        {
            string[] extractors = null;
            for (int i = 0; i < this.typesExtracteurs.Count; i++)
            {
                if( this.typesExtracteurs[i].name == mimeType)
                {
                    extractors = new string[typesExtracteurs[i].extracteurs.Count];
                    for (int j = 0; j < typesExtracteurs[i].extracteurs.Count; j++)
                    {
                        extractors[j] = typesExtracteurs[i].extracteurs[j].ToString();
                    }
                    break;
                }
            }
            if(extractors == null)
            {
                extractors = new string[1];
                extractors[0] = "Manuel";
            }
            return extractors;
        }

        public List<Famille> getListFamilles(string appli)
        {
            int i = 0;
            int j = 0;
            while (!this.applications[i].user.Equals(appli))
            {
                i++;
            }

            List<Famille> listFamilles = new List<Famille>();
            Famille famille = new Famille();

            for (j = 0; j < this.applications[i].typeInfos.Count; j++)
            {
                for (int k = 0; k < this.applications[i].typeInfos[j].aspects.Count; k++)
                {
                    if (this.applications[i].typeInfos[j].aspects[k].name == "fiducial:domainContainer")
                    {
                        for (int p = 0; p < this.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory.Count; p++)
                        {
                            string familleValue;
                            string sousFamilleValue;
                            if (this.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[p].type.Equals("fiducial:domainContainerFamille"))
                            {
                                
                                familleValue = (string)this.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[p].value;
                                for (int q = 0; q < this.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory.Count - p; q++)
                                {
                                    if (this.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[p + q].type.Equals("fiducial:domainContainerSousFamille"))
                                    {
                                        sousFamilleValue = (string)this.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[p+q].value;
                                        if (famille.name.Equals(familleValue))
                                        {
                                            famille.addSousFamille(sousFamilleValue);
                                        }
                                        else {
                                            
                                            famille = new Famille(familleValue, sousFamilleValue);
                                            listFamilles.Add(famille);

                                        }
                                        
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                } 
            }
            if (listFamilles.Count ==0 ||!famille.name.Equals(listFamilles[listFamilles.Count - 1].name))
            {
                listFamilles.Add(famille);
            }
            return listFamilles;
        }


    }
}
