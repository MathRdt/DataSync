﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
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
                        this.aspects.Add(aspectsToAdd[i].name);
                    }
                    for(int j=0; j< aspectsToAdd[i].metadatas.Mandatory.Count;j++)
                    {
                        isMandatoryMetaDataCompleted =this.metadatas.changeMetaData(aspectsToAdd[i].metadatas.Mandatory[j].type, aspectsToAdd[i].metadatas.Mandatory[j].value, true);
                        if (isMandatoryMetaDataCompleted) ReadyToSync.record(k);
                        k++;
                    }
                    for (int j = 0; j < aspectsToAdd[i].metadatas.Optional.Count; j++)
                    {
                        this.metadatas.changeMetaData(aspectsToAdd[i].metadatas.Optional[j].type, aspectsToAdd[i].metadatas.Optional[j].value, false);
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

        public void getMetaDatasFromConf(Conf conf, string documentType)
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < conf.applications.Count; i++)
            {
                for (j = 0; i < conf.applications[i].typeInfos.Count; j++)
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
