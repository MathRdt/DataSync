namespace ConsoleApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.labelBranche = new System.Windows.Forms.Label();
            this.labelSociete = new System.Windows.Forms.Label();
            this.labelFamille = new System.Windows.Forms.Label();
            this.labelSousFamille = new System.Windows.Forms.Label();
            this.labelNomClient = new System.Windows.Forms.Label();
            this.labelPrenomClient = new System.Windows.Forms.Label();
            this.labelValueBranche = new System.Windows.Forms.Label();
            this.labelValueSociete = new System.Windows.Forms.Label();
            this.textBoxNomClient = new System.Windows.Forms.TextBox();
            this.textBoxPrenomClient = new System.Windows.Forms.TextBox();
            this.comboBoxFamille = new System.Windows.Forms.ComboBox();
            this.comboBoxSousFamille = new System.Windows.Forms.ComboBox();
            this.buttonAnnuler = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelApplication = new System.Windows.Forms.Label();
            this.labelValueApplication = new System.Windows.Forms.Label();
            this.buttonokTest = new System.Windows.Forms.Button();
            this.labelTitre = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelAuteur = new System.Windows.Forms.Label();
            this.textBoxTitre = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelValueAuteur = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Liste des méta-données :";
            // 
            // labelBranche
            // 
            this.labelBranche.AllowDrop = true;
            this.labelBranche.AutoSize = true;
            this.labelBranche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBranche.Location = new System.Drawing.Point(21, 59);
            this.labelBranche.Name = "labelBranche";
            this.labelBranche.Size = new System.Drawing.Size(53, 13);
            this.labelBranche.TabIndex = 1;
            this.labelBranche.Text = "Branche :";
            this.labelBranche.Click += new System.EventHandler(this.labelBranche_Click);
            // 
            // labelSociete
            // 
            this.labelSociete.AutoSize = true;
            this.labelSociete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSociete.Location = new System.Drawing.Point(21, 91);
            this.labelSociete.Name = "labelSociete";
            this.labelSociete.Size = new System.Drawing.Size(49, 13);
            this.labelSociete.TabIndex = 2;
            this.labelSociete.Text = "Société :";
            this.labelSociete.Click += new System.EventHandler(this.labelSociete_Click);
            // 
            // labelFamille
            // 
            this.labelFamille.AutoSize = true;
            this.labelFamille.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFamille.Location = new System.Drawing.Point(21, 155);
            this.labelFamille.Name = "labelFamille";
            this.labelFamille.Size = new System.Drawing.Size(45, 13);
            this.labelFamille.TabIndex = 3;
            this.labelFamille.Text = "Famille :";
            this.labelFamille.Click += new System.EventHandler(this.labelFamille_Click);
            // 
            // labelSousFamille
            // 
            this.labelSousFamille.AutoSize = true;
            this.labelSousFamille.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSousFamille.Location = new System.Drawing.Point(21, 187);
            this.labelSousFamille.Name = "labelSousFamille";
            this.labelSousFamille.Size = new System.Drawing.Size(69, 13);
            this.labelSousFamille.TabIndex = 4;
            this.labelSousFamille.Text = "Sous-famille :";
            this.labelSousFamille.Click += new System.EventHandler(this.labelSousFamille_Click);
            // 
            // labelNomClient
            // 
            this.labelNomClient.AutoSize = true;
            this.labelNomClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNomClient.Location = new System.Drawing.Point(21, 315);
            this.labelNomClient.Name = "labelNomClient";
            this.labelNomClient.Size = new System.Drawing.Size(63, 13);
            this.labelNomClient.TabIndex = 5;
            this.labelNomClient.Text = "Nom client :";
            this.labelNomClient.Click += new System.EventHandler(this.labelNomClient_Click);
            // 
            // labelPrenomClient
            // 
            this.labelPrenomClient.AutoSize = true;
            this.labelPrenomClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrenomClient.Location = new System.Drawing.Point(21, 347);
            this.labelPrenomClient.Name = "labelPrenomClient";
            this.labelPrenomClient.Size = new System.Drawing.Size(77, 13);
            this.labelPrenomClient.TabIndex = 6;
            this.labelPrenomClient.Text = "Prénom client :";
            this.labelPrenomClient.Click += new System.EventHandler(this.labelPrenomClient_Click);
            // 
            // labelValueBranche
            // 
            this.labelValueBranche.AutoSize = true;
            this.labelValueBranche.Location = new System.Drawing.Point(114, 59);
            this.labelValueBranche.Name = "labelValueBranche";
            this.labelValueBranche.Size = new System.Drawing.Size(77, 13);
            this.labelValueBranche.TabIndex = 7;
            this.labelValueBranche.Text = "Value Branche";
            // 
            // labelValueSociete
            // 
            this.labelValueSociete.AutoSize = true;
            this.labelValueSociete.Location = new System.Drawing.Point(114, 91);
            this.labelValueSociete.Name = "labelValueSociete";
            this.labelValueSociete.Size = new System.Drawing.Size(73, 13);
            this.labelValueSociete.TabIndex = 8;
            this.labelValueSociete.Text = "Value Société";
            // 
            // textBoxNomClient
            // 
            this.textBoxNomClient.Location = new System.Drawing.Point(114, 308);
            this.textBoxNomClient.Name = "textBoxNomClient";
            this.textBoxNomClient.Size = new System.Drawing.Size(100, 20);
            this.textBoxNomClient.TabIndex = 9;
            this.textBoxNomClient.TextChanged += new System.EventHandler(this.textBoxNomClient_TextChanged);
            // 
            // textBoxPrenomClient
            // 
            this.textBoxPrenomClient.Location = new System.Drawing.Point(114, 340);
            this.textBoxPrenomClient.Name = "textBoxPrenomClient";
            this.textBoxPrenomClient.Size = new System.Drawing.Size(100, 20);
            this.textBoxPrenomClient.TabIndex = 10;
            // 
            // comboBoxFamille
            // 
            this.comboBoxFamille.FormattingEnabled = true;
            this.comboBoxFamille.Location = new System.Drawing.Point(114, 147);
            this.comboBoxFamille.Name = "comboBoxFamille";
            this.comboBoxFamille.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFamille.TabIndex = 11;
            // 
            // comboBoxSousFamille
            // 
            this.comboBoxSousFamille.FormattingEnabled = true;
            this.comboBoxSousFamille.Location = new System.Drawing.Point(114, 179);
            this.comboBoxSousFamille.Name = "comboBoxSousFamille";
            this.comboBoxSousFamille.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSousFamille.TabIndex = 12;
            this.comboBoxSousFamille.SelectedIndexChanged += new System.EventHandler(this.comboBoxSousFamille_SelectedIndexChanged);
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.Location = new System.Drawing.Point(108, 392);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(75, 23);
            this.buttonAnnuler.TabIndex = 13;
            this.buttonAnnuler.Text = "Annuler";
            this.buttonAnnuler.UseVisualStyleBackColor = true;
            this.buttonAnnuler.Click += new System.EventHandler(this.buttonAnnuler_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(358, 392);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelApplication
            // 
            this.labelApplication.AutoSize = true;
            this.labelApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplication.Location = new System.Drawing.Point(21, 123);
            this.labelApplication.Name = "labelApplication";
            this.labelApplication.Size = new System.Drawing.Size(65, 13);
            this.labelApplication.TabIndex = 15;
            this.labelApplication.Text = "Application :";
            // 
            // labelValueApplication
            // 
            this.labelValueApplication.AutoSize = true;
            this.labelValueApplication.Location = new System.Drawing.Point(114, 123);
            this.labelValueApplication.Name = "labelValueApplication";
            this.labelValueApplication.Size = new System.Drawing.Size(89, 13);
            this.labelValueApplication.TabIndex = 16;
            this.labelValueApplication.Text = "Value Application";
            // 
            // buttonokTest
            // 
            this.buttonokTest.Location = new System.Drawing.Point(271, 139);
            this.buttonokTest.Name = "buttonokTest";
            this.buttonokTest.Size = new System.Drawing.Size(36, 29);
            this.buttonokTest.TabIndex = 17;
            this.buttonokTest.Text = "ok";
            this.buttonokTest.UseVisualStyleBackColor = true;
            this.buttonokTest.Click += new System.EventHandler(this.buttonokTest_Click);
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitre.Location = new System.Drawing.Point(21, 219);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(34, 13);
            this.labelTitre.TabIndex = 18;
            this.labelTitre.Text = "Titre :";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(21, 251);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(66, 13);
            this.labelDescription.TabIndex = 19;
            this.labelDescription.Text = "Description :";
            // 
            // labelAuteur
            // 
            this.labelAuteur.AutoSize = true;
            this.labelAuteur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuteur.Location = new System.Drawing.Point(21, 283);
            this.labelAuteur.Name = "labelAuteur";
            this.labelAuteur.Size = new System.Drawing.Size(44, 13);
            this.labelAuteur.TabIndex = 20;
            this.labelAuteur.Text = "Auteur :";
            // 
            // textBoxTitre
            // 
            this.textBoxTitre.Location = new System.Drawing.Point(114, 212);
            this.textBoxTitre.Name = "textBoxTitre";
            this.textBoxTitre.Size = new System.Drawing.Size(100, 20);
            this.textBoxTitre.TabIndex = 21;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(114, 244);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(100, 20);
            this.textBoxDescription.TabIndex = 22;
            // 
            // labelValueAuteur
            // 
            this.labelValueAuteur.AutoSize = true;
            this.labelValueAuteur.Location = new System.Drawing.Point(114, 283);
            this.labelValueAuteur.Name = "labelValueAuteur";
            this.labelValueAuteur.Size = new System.Drawing.Size(132, 13);
            this.labelValueAuteur.TabIndex = 23;
            this.labelValueAuteur.Text = "Value Auteur = Application";
            this.labelValueAuteur.Click += new System.EventHandler(this.labelValueAuteur_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 424);
            this.Controls.Add(this.labelValueAuteur);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.textBoxTitre);
            this.Controls.Add(this.labelAuteur);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelTitre);
            this.Controls.Add(this.buttonokTest);
            this.Controls.Add(this.labelValueApplication);
            this.Controls.Add(this.labelApplication);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonAnnuler);
            this.Controls.Add(this.comboBoxSousFamille);
            this.Controls.Add(this.comboBoxFamille);
            this.Controls.Add(this.textBoxPrenomClient);
            this.Controls.Add(this.textBoxNomClient);
            this.Controls.Add(this.labelValueSociete);
            this.Controls.Add(this.labelValueBranche);
            this.Controls.Add(this.labelPrenomClient);
            this.Controls.Add(this.labelNomClient);
            this.Controls.Add(this.labelSousFamille);
            this.Controls.Add(this.labelFamille);
            this.Controls.Add(this.labelSociete);
            this.Controls.Add(this.labelBranche);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Méta-données";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBranche;
        private System.Windows.Forms.Label labelSociete;
        private System.Windows.Forms.Label labelFamille;
        private System.Windows.Forms.Label labelSousFamille;
        private System.Windows.Forms.Label labelNomClient;
        private System.Windows.Forms.Label labelPrenomClient;
        private System.Windows.Forms.Label labelValueBranche;
        private System.Windows.Forms.Label labelValueSociete;
        private System.Windows.Forms.TextBox textBoxNomClient;
        private System.Windows.Forms.TextBox textBoxPrenomClient;
        private System.Windows.Forms.ComboBox comboBoxFamille;
        private System.Windows.Forms.ComboBox comboBoxSousFamille;
        private System.Windows.Forms.Button buttonAnnuler;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelApplication;
        private System.Windows.Forms.Label labelValueApplication;
        private System.Windows.Forms.Button buttonokTest;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelAuteur;
        private System.Windows.Forms.TextBox textBoxTitre;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelValueAuteur;
    }
}

