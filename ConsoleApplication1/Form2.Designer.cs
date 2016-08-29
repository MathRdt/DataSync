namespace ConsoleApplication1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelBranche = new System.Windows.Forms.Label();
            this.labelSociete = new System.Windows.Forms.Label();
            this.comboBoxBranche = new System.Windows.Forms.ComboBox();
            this.comboBoxSociete = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBranche
            // 
            this.labelBranche.AutoSize = true;
            this.labelBranche.Location = new System.Drawing.Point(34, 39);
            this.labelBranche.Name = "labelBranche";
            this.labelBranche.Size = new System.Drawing.Size(53, 13);
            this.labelBranche.TabIndex = 0;
            this.labelBranche.Text = "Branche :";
            // 
            // labelSociete
            // 
            this.labelSociete.AutoSize = true;
            this.labelSociete.Location = new System.Drawing.Point(265, 39);
            this.labelSociete.Name = "labelSociete";
            this.labelSociete.Size = new System.Drawing.Size(49, 13);
            this.labelSociete.TabIndex = 1;
            this.labelSociete.Text = "Société :";
            this.labelSociete.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBoxBranche
            // 
            this.comboBoxBranche.FormattingEnabled = true;
            this.comboBoxBranche.Location = new System.Drawing.Point(37, 74);
            this.comboBoxBranche.Name = "comboBoxBranche";
            this.comboBoxBranche.Size = new System.Drawing.Size(141, 21);
            this.comboBoxBranche.TabIndex = 2;
            // 
            // comboBoxSociete
            // 
            this.comboBoxSociete.FormattingEnabled = true;
            this.comboBoxSociete.Location = new System.Drawing.Point(268, 74);
            this.comboBoxSociete.Name = "comboBoxSociete";
            this.comboBoxSociete.Size = new System.Drawing.Size(141, 21);
            this.comboBoxSociete.TabIndex = 3;
            this.comboBoxSociete.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(92, 155);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(124, 30);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(247, 155);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(124, 30);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 256);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxSociete);
            this.Controls.Add(this.comboBoxBranche);
            this.Controls.Add(this.labelSociete);
            this.Controls.Add(this.labelBranche);
            this.Name = "Form2";
            this.Text = "Configuration installation";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBranche;
        private System.Windows.Forms.Label labelSociete;
        private System.Windows.Forms.ComboBox comboBoxBranche;
        private System.Windows.Forms.ComboBox comboBoxSociete;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}