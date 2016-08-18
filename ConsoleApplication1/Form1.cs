//fonction module manuel afin de récupérer les méta-données manquantes

using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        public List<Famille> listFamilles;
        public MetaDatas metadatasFromManual=new MetaDatas();
       
        public void fillForm (MetaDatas metadatas)
        {
            metadatasFromManual = metadatas;
           
            InitializeComponent();
            int i = 0;
            
            int length = metadatas.Mandatory.Count;
            if((string)metadatasFromManual.Mandatory[0].value == "" || (string)metadatasFromManual.Mandatory[1].value == "" || (string)metadatasFromManual.Mandatory[2].value == "")
            {
                Console.WriteLine("votre fichier doit être placé dans un dossier Fidusync/Branche/Société/Application");
                return;
            }

            //branche, société, application ne peuvent pas être modifié par l'utilisateur
            //affichage de ces méta-données
            labelValueBranche.Text = (string)metadatasFromManual.Mandatory[0].value;
            labelValueSociete.Text = (string)metadatasFromManual.Mandatory[1].value;
            labelValueApplication.Text = (string)metadatasFromManual.Mandatory[2].value;
            labelValueAuteur.Text = (string)metadatasFromManual.Mandatory[2].value;
            
            Conf conf = Conf.Charger(Program.confFile);
            listFamilles = conf.getListFamilles((string)metadatasFromManual.Mandatory[2].value);

            if ((string)metadatasFromManual.Mandatory[3].value != "")
            {
                //si la famille a été trouvé suppression de la liste déroulante
                comboBoxFamille.Dispose();
                //création d'un label pour afficher le résultat
                Label textbox45 = new Label();
                textbox45.Left = 115;
                textbox45.Top = 155;
                textbox45.Width = 120;
                textbox45.Height = 30;
                this.Controls.Add(textbox45);
                //ce label correspond à la méta-données famille qui est la 4eme de la liste
                textbox45.Text = (string)metadatasFromManual.Mandatory[3].value;
            }
            else
            {
                //si la famille n'a pas été trouvé proposition des famille sous forme de menu déroulant
                comboBoxFamille.Items.Clear();


                
                for ( i = 0; i < listFamilles.Count; i++)
                {
                    comboBoxFamille.Items.Add(listFamilles[i].name);
                }
            }


            //si la sous famille n'existe pas l'utilisateur choisi une sous-famille parmi la liste des sous-familles existantes 
            if ((string)metadatasFromManual.Mandatory[4].value != "")
            {
                //si la sous-famille est connue suppression de la liste déroulante
                comboBoxSousFamille.Dispose();
                //création d'un label pour afficher le résultat
                Label textbox45 = new Label();
                textbox45.Left = 115;
                textbox45.Top = 187;
                textbox45.Width = 120;
                textbox45.Height = 30;
                this.Controls.Add(textbox45);
                //si l'on connait la sous-famille elle est la 5eme string de la liste
                textbox45.Text = (string)metadatasFromManual.Mandatory[4].value;

            }
            else
            {
                //si la famille n'a pas été trouvé proposition des famille sous forme de menu déroulant
                comboBoxSousFamille.Items.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void labelSociete_Click(object sender, EventArgs e)
        {

        }

        private void labelFamille_Click(object sender, EventArgs e)
        {

        }

        private void labelBranche_Click(object sender, EventArgs e)
        {

        }

        private void labelSousFamille_Click(object sender, EventArgs e)
        {

        }

        private void labelNomClient_Click(object sender, EventArgs e)
        {

        }

        private void labelPrenomClient_Click(object sender, EventArgs e)
        {

        }

        //si l'utilisateur clique sur le bouton annuler la fenêtre est fermée
        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        //si l'utilisateur clique sur le bouton ok affichage d'une fenêtre demandant si l'utilisateur est sûr de vouloir quitter
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //si oui la fenêtre se ferme
            //sinon on revient sur la page
            if (MessageBox.Show("Etes-vous sûr de vouloir quitter ?", "Quitter",
         MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                    if ((string)metadatasFromManual.Mandatory[4].value == "")
                    {
                        if ((string)metadatasFromManual.Mandatory[3].value == "")
                        {
                            metadatasFromManual.changeMetaData("fiducial:domainContainerFamille", comboBoxFamille.Text, true);
                        }
                    metadatasFromManual.changeMetaData("fiducial:domainContainerSousFamille", comboBoxSousFamille.Text, true);
                    }
                metadatasFromManual.changeMetaData("cm:title", textBoxTitre.Text, false);
                metadatasFromManual.changeMetaData("cm:description", textBoxDescription.Text, false);


                for (int i = 0; i < metadatasFromManual.Mandatory.Count; i++)
                {
                    Console.WriteLine("metadonnee from manual " + i + " : " + metadatasFromManual.Mandatory[i].value);
                }

                Close();
            }
        }

        private void textBoxNomClient_TextChanged(object sender, EventArgs e)
        {
            

        }

        //lorsque l'utilisateur clique sur la croix rouge lui demande s'il est sûr de vouloir quitter
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Etes-vous certain de vouloir quitter ?", "Quitter", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        //affiche la sous famille correspondant à la famille sélectionnée lorsque l'utilisateur clique sur le bouton ok à coté de famille
        private void buttonokTest_Click(object sender, EventArgs e)
        {
            string[] SousFamilleFacture = { "Facture client", "Facture interne" };
            string[] SousFamillePaye = { "Paye type 1", "Paye type 2" };

            //en fonction de la famille choisi va s'afficher le menu correspondant des sous familles
            if (comboBoxFamille.Text == "Facture")
            {
                comboBoxSousFamille.Items.Clear();
                for (int i = 0; i < SousFamilleFacture.Length; i++)
                {
                    comboBoxSousFamille.Items.Add(SousFamilleFacture[i]);
                }
            }
            else if (comboBoxFamille.Text == "Paye")
            {
                comboBoxSousFamille.Items.Clear();
                for (int i = 0; i < SousFamillePaye.Length; i++)
                {
                    comboBoxSousFamille.Items.Add(SousFamillePaye[i]);
                }
            }
        }

        private void comboBoxSousFamille_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void okSousFamille_Click(object sender, EventArgs e)
        {

        }

        private void labelValueAuteur_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxFamille_SelectedIndexChanged(object sender, EventArgs e)
        {
            for(int i = 0; i < listFamilles.Count; i++) {
                if (listFamilles[i].name.Equals(comboBoxFamille.Text))
                {
                    comboBoxSousFamille.Items.Clear();
                    for (int j = 0; j < listFamilles[i].sousFamille.Count; j++)
                    {
                        comboBoxSousFamille.Items.Add(listFamilles[i].sousFamille[j]);
                    }
                    break;
                }
            }
        }

        private void labelValueBranche_Click(object sender, EventArgs e)
        {

        }

        private void textBoxTitre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
