/// <summary>
/// fonction qui permet de transformer un word en pdf
/// remarque : pour ensuite extraire les méta-données, il suffit d'utiliser les foonctions de Extracteur_pdf
/// </summary>

using System;
using Spire.Doc;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;


namespace ConsoleApplication1
{
    class Extract_Word
    {
       /// <summary>
       /// fonction qui convertit un document word en pdf
       /// </summary>
       /// <param name="path"></param>

        public static string WordToPdf(string path)
        {
            Spire.Doc.Document doc = new Spire.Doc.Document();
            doc.LoadFromFile(path);//charge le document word
            int taille = path.Length;
            string path2 = path;
            path2 = path.Replace("docx", "PDF"); //créer nouveau chemin pour le pdf avec le même nom de fichier que le word
            
            doc.SaveToFile(path2, FileFormat.PDF); //exporte le word à l'emplacement qu'indique path2

            PDDocument document = null;
            try
            {
                document = PDDocument.load(path2);
                PDFTextStripper stripper = new PDFTextStripper();
                return stripper.getText(document);
            }
            finally
            {
                if (doc != null)
                {
                    document.close();
                }
            }
        }

        

        static void Main(string[] args)
        {
            string path = @"C:\Users\adminprojetindus\Documents\projet indus\testDocx.docx";
            string result = WordToPdf(path);
            //utiliser ensuite les fonctions de Extracteur_pdf.WriteLine("resultat :" + result);

            Console.WriteLine(result);

            Console.WriteLine("Hello Word");
            
        }
    }
}
