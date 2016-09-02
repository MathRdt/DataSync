using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    /// <summary>
    /// structure qui permet la création du XML pour chaque fichier
    /// </summary>
    [Serializable]
    public class GlobalMetaDatas
    {
        public string typename;
        public List<string> aspects;
        public MetaDatas metadatas;


        public GlobalMetaDatas()
        {
            typename = "";
            aspects = new List<string>();
            metadatas = new MetaDatas();
        }

        /// <summary>
        /// création d'un fichier XML
        /// </summary>
        /// <param name="fileName">le nom et l'emplacement de ce fichier XML</param>
        public static void createXML(string fileName)
        {
            GlobalMetaDatas xmlMetadatas = new GlobalMetaDatas();
            xmlMetadatas.Enregistrer(fileName);
        }


        /// <summary>
        /// Enregistre l'état courant de la classe dans un fichier au format XML.
        /// </summary>
        /// <param name="chemin">chemin pour enregistrer le fichier XML (jusqu'au nom du fichier .xml)</param>
        
        public void Enregistrer(string chemin)
        {
            //if (!File.Exists(chemin))
            //{
            //    throw new NoPathFoundException();
            //}
            XmlSerializer serializer = new XmlSerializer(typeof(GlobalMetaDatas));
            StreamWriter writer = new StreamWriter(chemin);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// déserialise un fichier XML, re donne le fichier XML sous forme de classes C#
        /// </summary>
        /// <param name="chemin">chemin du fichier XML à déserialiser</param>
        /// <returns>retourne la liste des méta données du fichier observé</returns>
        public static GlobalMetaDatas Charger(string chemin)
        {
            if (!File.Exists(chemin))
            {
                throw new NoPathFoundException();
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(GlobalMetaDatas));
            StreamReader reader = new StreamReader(chemin);
            GlobalMetaDatas md = (GlobalMetaDatas)deserializer.Deserialize(reader);
            reader.Close();

            return md;
        }

        /// <summary>
        /// à partir du fichier de configuration, ajoute les aspects et méta données dans le fichier XML
        /// si un aspect existe déjà, il ne sera pas recopié
        /// si une méta donnée existe déjà et quelle a une valeur par défaut dans le fichier de configuration, elle prendra la valeur du fichier de conf
        /// </summary>
        /// <param name="aspectsToAdd">Liste des aspects extraits du fichier de configuration </param>
        public void addAspects(List<Aspect> aspectsToAdd)
        {
            int k = 0;
            bool isMandatoryMetaDataCompleted;
            if(aspectsToAdd != null) {
                for (int i = 0; i < aspectsToAdd.Count; i++)
                {
                    if (!this.aspects.Contains(aspectsToAdd[i].name))
                    {
                        this.aspects.Add("P:"+aspectsToAdd[i].name);
                    }
                    for(int j=0; j< aspectsToAdd[i].metadatas.Mandatory.Count;j++)
                    {
                        isMandatoryMetaDataCompleted = this.metadatas.changeMetaData(aspectsToAdd[i].metadatas.Mandatory[j].type, aspectsToAdd[i].metadatas.Mandatory[j].value, true, aspectsToAdd[i].metadatas.Mandatory[j].keyWords, aspectsToAdd[i].metadatas.Mandatory[j].listValues, aspectsToAdd[i].metadatas.Mandatory[j].regEx, aspectsToAdd[i].metadatas.Mandatory[j].min, aspectsToAdd[i].metadatas.Mandatory[j].max, aspectsToAdd[i].metadatas.Mandatory[j].valueType);
                        //aspectsToAdd[i].metadatas.Mandatory[j].listValues;
                        if (isMandatoryMetaDataCompleted) ReadyToSync.record(k);
                        k++;
                    }
                    for (int j = 0; j < aspectsToAdd[i].metadatas.Optional.Count; j++)
                    {
                        this.metadatas.changeMetaData(aspectsToAdd[i].metadatas.Optional[j].type, aspectsToAdd[i].metadatas.Optional[j].value, false, aspectsToAdd[i].metadatas.Optional[j].keyWords, aspectsToAdd[i].metadatas.Optional[j].listValues, aspectsToAdd[i].metadatas.Optional[j].regEx, aspectsToAdd[i].metadatas.Optional[j].min, aspectsToAdd[i].metadatas.Optional[j].max, aspectsToAdd[i].metadatas.Optional[j].valueType);
                    }
                }
            }
        }

        public Boolean isPresent(Aspect aspectToAdd)
        {
            for (int i = 0; i < aspects.Count; i++) {
                if (this.aspects[i].Equals(aspectToAdd.name))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ajoute ou modifie des méta données suivant les infos fournies par le fichier de configuration
        /// </summary>
        /// <param name="conf">chemin menant jusqu'au fichier de conf</param>
        /// <param name="documentType">type du document que l'on observe</param>
        public void getMetaDatasFromConf(Conf conf, string documentType)
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < conf.applications.Count; i++)
            {
                for (j = 0; j < conf.applications[i].typeInfos.Count; j++)
                {
                    if (conf.applications[i].typeInfos[j].typename == documentType)
                    {
                        this.addAspects(conf.applications[i].typeInfos[j].aspects);
                        break;
                    }
                }
                if (conf.applications[i].typeInfos[j].typename == documentType)
                {
                    break;
                }

            }
        }

        /// <summary>
        /// </summary>
        /// <returns>true si le fichier est prêt à être sychronisé, false sinon</returns>
        public bool isComplete()
        {
            if (this.metadatas.Mandatory.Count == 0) return false;
            for(int i=0; i< this.metadatas.Mandatory.Count; i++)
            {
                if ((string)this.metadatas.Mandatory[i].value == "") return false;
                else ReadyToSync.record(i);
            }
            return true;
        }

        public string getAppFromConf(Conf conf, string path )
        {
            string app = "";
            for(int i =0; i < conf.applications.Count; i++)
            {
                for (int j = 0; j < conf.applications[i].folders.Count; j++)
                {
                    if (conf.applications[i].folders[j].name == path)
                    {
                        string[]pathString =  Extract_Path.conversionRemotePath(conf.applications[i].folders[j].remotePath);
                        //for(int k = 0; k < pathString.Length; k++)
                        //{
                        //    Console.WriteLine(pathString[k]);
                        //}
                        string[] stringMetaDatas = new string[4];
                        stringMetaDatas[0] = pathString[pathString.Length - 4];
                        stringMetaDatas[1] = pathString[pathString.Length - 3];
                        stringMetaDatas[2] = pathString[pathString.Length - 2];
                        
                        this.metadatas.changeMetaData("fiducial:domainContainerBranche", stringMetaDatas[0], true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");
                        this.metadatas.changeMetaData("fiducial:domainContainerSociete", stringMetaDatas[1], true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");
                        this.metadatas.changeMetaData("fiducial:domainContainerApplication", stringMetaDatas[2], true, new List<string>(), new List<string>(), "", Double.MinValue, Double.MaxValue, "string");
                        return stringMetaDatas[2];
                    }
                }
            }
            return app;
        }

        public string getBranche()
        {
            for (int i = 0; i < this.metadatas.Mandatory.Count; i++)
            {
                if (this.metadatas.Mandatory[i].type == "fiducial:domainContainerBranche")
                {
                    return (string)this.metadatas.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getSociete()
        {
            for (int i = 0; i < this.metadatas.Mandatory.Count; i++)
            {
                if (this.metadatas.Mandatory[i].type == "fiducial:domainContainerSociete")
                {
                    return (string)this.metadatas.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getApp()
        {
            for (int i=0; i < this.metadatas.Mandatory.Count; i++)
            {
                if (this.metadatas.Mandatory[i].type == "fiducial:domainContainerApplication")
                {
                    return (string)this.metadatas.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getFamille()
        {
            for (int i=0; i < this.metadatas.Mandatory.Count; i++)
            {
                if (this.metadatas.Mandatory[i].type == "fiducial:domainContainerFamille")
                {
                    return (string)this.metadatas.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getSousFamille()
        {
            for (int i = 0; i < this.metadatas.Mandatory.Count; i++)
            {
                if (this.metadatas.Mandatory[i].type == "fiducial:domainContainerSousFamille")
                {
                    return (string)this.metadatas.Mandatory[i].value;
                }
            }
            return null;
        }



        public void DetermineType(Conf conf, string path)
        {
            string app = getAppFromConf(conf, path);
            
            List<Famille> familles = conf.getListFamilles(app);
            for(int i=0; i < familles.Count; i++)
            {
                Console.WriteLine(familles[i].name);
                for(int j=0; j < familles[i].sousFamille.Count; j++)
                {
                    Console.WriteLine(familles[i].sousFamille[j]);
                }
            }


            for (int i =0; i < this.metadatas.Mandatory.Count; i++)
            {
                if(this.metadatas.Mandatory[i].type == "fiducial:domainContainerFamille")
                {
                    if((string)this.metadatas.Mandatory[i].value == "" || (string)this.metadatas.Mandatory[i+1].value == "")
                    {
                        this.typename = "cmis:document";
                    }
                    for (int j =0; j< familles.Count; j++)
                    {
                        if (familles[j].name == (string) this.metadatas.Mandatory[i].value)
                        {
                            
                            for (int k=0; k< familles[j].sousFamille.Count; k++)
                            {
                                if(familles[j].sousFamille[k] == (string)this.metadatas.Mandatory[i + 1].value)
                                {
                                    this.typename = this.getType(conf, app,(string) this.metadatas.Mandatory[i].value, (string) this.metadatas.Mandatory[i + 1].value);
                                }
                            }
                        }
                    }
                }
            }
        }

        public string getType (Conf Conf,string app, string famille, string sousFamille)
        {
            string type = "";
            for (int i=0; i < Conf.applications.Count; i++)
            {
                if (Conf.applications[i].user == app)
                {
                    for(int j=0; j< Conf.applications[i].typeInfos.Count; j++)
                    {
                        for(int k=0; k < Conf.applications[i].typeInfos[j].aspects.Count; k++)
                        {
                            if(Conf.applications[i].typeInfos[j].aspects[k].name == "fiducial:domainContainer")
                            {
                                for(int m=0;m< Conf.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory.Count; m++)
                                {
                                    if (Conf.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[m].type == "fiducial:domainContainerFamille")
                                    {
                                        if ((string) Conf.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[m].value == famille)
                                        {
                                            if ((string)Conf.applications[i].typeInfos[j].aspects[k].metadatas.Mandatory[m + 1].value == sousFamille)
                                            {
                                                type = Conf.applications[i].typeInfos[j].typename;
                                                
                                                return type;
                                            }
                                            else break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            type = "cmis:document";
            
            return type;
        }

    }

    

    




    [Serializable()]
    public class NoPathFoundException : System.Exception
    {
        public NoPathFoundException() : base() { }
        public NoPathFoundException(string message) : base(message) { }
        public NoPathFoundException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected NoPathFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }

}
