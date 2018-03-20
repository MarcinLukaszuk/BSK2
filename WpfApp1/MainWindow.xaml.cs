using BSK2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _seed = seed.Text;
            string _polynominal = polynominal.Text;
            LFSR lfsr = new LFSR(_seed, _polynominal);
            string text = "";

            int tmp = int.Parse(inputfile.Text);
            for (int i = 0; i < tmp; i++)
            {
                text += lfsr.wyswietlLFSR();
                lfsr.GetNext();

            }
            File.WriteAllText(outputfile.Text, text);
        }

        static void CompareArrays(string filepath1, string filepath2)
        {
            var array1 = File.ReadAllBytes(filepath1).ToList();
            var array2 = File.ReadAllBytes(filepath2).ToList();


            for (int i = 0; i < array1.Count; i++)
            {
                if (array1[i] != array2[i])
                {
                    Debug.WriteLine("nie to samo");
                }
            }
            Debug.WriteLine("to samo");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string _seed = seed.Text;
            string _polynominal = polynominal.Text;
            string _inputfile = inputfile.Text;
            string _outputfile = outputfile.Text;
            SynchronousStreamCipher SSC = new SynchronousStreamCipher(_seed, _polynominal);
            SSC.Encrypt(_inputfile, _outputfile);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string _seed = seed.Text;
            string _polynominal = polynominal.Text;
            string _inputfile = inputfile.Text;
            string _outputfile = outputfile.Text;
            AutokeyStreamCipher SSC = new AutokeyStreamCipher(_seed, _polynominal);
            SSC.Encrypt(_inputfile, _outputfile);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string _seed = seed.Text;
            string _polynominal = polynominal.Text;
            string _inputfile = inputfile.Text;
            string _outputfile = outputfile.Text;
            AutokeyStreamCipher SSC = new AutokeyStreamCipher(_seed, _polynominal);
            SSC.Decrypt(_inputfile, _outputfile);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string _inputfile = inputfile.Text;
            string _outputfile = outputfile.Text;
            CompareArrays(_inputfile, _outputfile);
        }
    }
}
