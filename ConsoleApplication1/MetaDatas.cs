using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApplication1
{

   

    /// <summary>
    /// Strucuture d'une méta donnée
    /// le Type défini le type de la méta donnée
    /// la Valeur donne la valeur affectée à cette méta donnée
    /// </summary>
    [Serializable]
    public class MetaData
    {
        public string type { get; set; }
        public object value { get; set; }

        //[XmlIgnore]
        public List<string> keyWords { get; set; }

        //[XmlIgnore]
        public List<string> listValues { get; set; }

        //[XmlIgnore]
        public string regEx { get; set; }

        //[XmlIgnore]
        public string valueType { get; set; }

        //[XmlIgnore]
        public double min { get; set; }

        //[XmlIgnore]
        public double max { get; set; }

        public Boolean isSameType(String typeOfMetadata)
        {
            if (type.Equals(typeOfMetadata))
            {
                return true;
            }
            return false;
        }

    }


    /// <summary>
    /// donne la liste des méta données obtenues pour un fichier
    /// l'attribut Mandatory donne la liste des meta données obligatoires pour ce type de document
    /// l'attribut Optional donne la liste des meta données obligatoires pour ce type de document
    /// </summary>
    [Serializable()]
    public class MetaDatas
    {
        public List<MetaData> Mandatory { get; set; }
        public List<MetaData> Optional { get; set; }

        public MetaDatas()
        {
            this.Mandatory = new List<MetaData>();
            //this.changeMetaData("fiducial:domainContainerBranche", "", true);
            //this.changeMetaData("fiducial:domainContainerSociete", "", true);
            //this.changeMetaData("fiducial:domainContainerApplication", "", true);
            //this.changeMetaData("fiducial:domainContainerFamille", "", true);
            //this.changeMetaData("fiducial:domainContainerSousFamille", "", true);

            this.Optional = new List<MetaData>();
        }

        /// <summary>
        /// ajoute une nouvelle méta donnée à la liste des méta données déjà existante
        /// </summary>
        /// <param name="fileName">nom du fichier XML à modifier</param>
        /// <param name="typeMetaData"> le type de la nouvelle meta donnée </param>
        /// <param name="valueMetaData">la valeur affectée à cette nouvelle méta donnée</param>
        /// <param name="mandatory"> cette méta donnée est-elle obligatoire ou non</param>
        public void addMetaData(string typeMetaData, object valueMetaData, bool mandatory,List<string> listKeyWords, List<string> listValuesMetaData, string regex, double minValue, double maxValue, string valuetype)
        {
            if (mandatory == true)
                this.Mandatory.Add(new MetaData() { type = typeMetaData, value = valueMetaData, keyWords = listKeyWords, listValues = listValuesMetaData , max = maxValue, min = minValue, regEx = regex, valueType = valuetype });
                
            else
                this.Optional.Add(new MetaData() { type = typeMetaData, value = valueMetaData, keyWords = listKeyWords, listValues = listValuesMetaData, max = maxValue, min = minValue, regEx = regex, valueType = valuetype });
        }

        /// <summary>
        /// remplace la valeur d'une méta donnée déjà exsistante par une nouvelle valeur
        /// </summary>
        /// <param name="fileName">fichier XML à modifier</param>
        /// <param name="typeMetaData">la méta donnée à modifier</param>
        /// <param name="valueMetaData">la nouvelle valeur à prendre</param>
        /// <param name="mandatory">méta donnée obligatoire ou non</param>

        public bool changeMetaData(string typeMetaData, object valueMetaData, bool mandatory, List<string> listKeyWords, List<string> listValuesMetaData, string regex, double minValue, double maxValue, string valuetype)
        {
            //Console.WriteLine(valueMetaData.GetType().ToString());
            if (mandatory == true)
            {
                for (int i = 0; i < this.Mandatory.Count; i++)
                {
                    if (this.Mandatory[i].type == typeMetaData)
                    {
                        
                        if(valueMetaData != null && (string)valueMetaData != "") {
                            this.Mandatory[i].value = valueMetaData;
                            return true;
                        }
                        return false;

                    }
                }
            }
            else
            {
                for (int j = 0; j < this.Optional.Count; j++)
                {
                    if (this.Optional[j].type == typeMetaData)
                    {

                        if (valueMetaData != null && (string)valueMetaData != "")
                        {
                            this.Optional[j].value = valueMetaData;
                        }
                        return false;

                    }
                }
            }
            this.addMetaData(typeMetaData, valueMetaData, mandatory,listKeyWords,listValuesMetaData,regex,minValue,maxValue,valuetype);
            if (mandatory == true && (string)valueMetaData != "") return true;
            return false;
        }

        public string getBranche()
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerBranche")
                {
                    return (string)this.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getSociete()
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerSociete")
                {
                    return (string)this.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getApplication()
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerApplication")
                {
                    return (string)this.Mandatory[i].value;
                }
            }
            return null;
        }

        public string getFamille()
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerFamille")
                {
                    return (string)this.Mandatory[i].value;
                }
            }
            return null;
        }

        public void setFamille(string famille)
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerFamille")
                {
                    this.Mandatory[i].value = famille;
                }
            }
        }

        public string getSousFamille()
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerSousFamille")
                {
                    return (string)this.Mandatory[i].value;
                }
            }
            return null;
        }

        public void setSousFamille(string sousFamille)
        {
            for (int i = 0; i < this.Mandatory.Count; i++)
            {
                if (this.Mandatory[i].type == "fiducial:domainContainerSousFamille")
                {
                    this.Mandatory[i].value = sousFamille;
                }
            }
        }
    }
}
