using Microsoft.Win32;
using Puma.Net;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testOcr
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            Nullable<bool> isSelected = fileDialog.ShowDialog();
            if(isSelected == true)
            {
                string filePath = fileDialog.FileName;
                txt1.Text = filePath;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string sourceFilePath = txt1.Text.Trim();
            if (!string.IsNullOrEmpty(sourceFilePath))
            {
                PumaPage inputFile = new PumaPage(sourceFilePath);
                inputFile.FileFormat = PumaFileFormat.TxtAscii;
                //inputFile.Language = PumaLanguage.French;
                inputFile.Language = PumaLanguage.English;
                string outputString = inputFile.RecognizeToString();
                txt_display.Text = outputString;
                inputFile.Dispose();
            }
            else MessageBox.Show("error");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            Nullable<bool> isSelected = fileDialog.ShowDialog();
            if(isSelected == true)
            {
                string destinationPath = fileDialog.FileName;
                txt2.Text = destinationPath;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string sourceFilePath = txt1.Text.Trim();
            string destinationFilePath = txt2.Text.Trim();
            if(!string.IsNullOrEmpty(destinationFilePath) & !string.IsNullOrEmpty(sourceFilePath))
            {
                PumaPage outputFile = new PumaPage(sourceFilePath);
                outputFile.FileFormat = PumaFileFormat.TxtAscii;
                //outputFile.Language = PumaLanguage.French;
                outputFile.Language = PumaLanguage.English;
                outputFile.RecognizeToFile(destinationFilePath);
                outputFile.Dispose();
                MessageBox.Show("writting succeed!");
            }
            else MessageBox.Show("error");
        }

        private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
