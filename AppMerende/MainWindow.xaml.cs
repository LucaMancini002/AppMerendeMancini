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

namespace AppMerende
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
 
        }

        private void CaricaMerende()
        {
            List<Merenda> merendeDisponibili= new List<Merenda>();
            string line;
            StreamReader sr = new StreamReader("Merende.csv");
            sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    Merenda m = new Merenda();
                    string[] camp1 = line.Split(';');
                    m.Nome = camp1[0];
                    m.Prezzo = Convert.ToDouble(camp1[1]);
                    merendeDisponibili.Add(m);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    break;
                }           
            }

            Dispatcher.Invoke(() => lst_Merende.ItemsSource = merendeDisponibili);
        }

        private void btn_Visualizza_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(CaricaMerende);
        }

        private void btn_Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            Merenda ms = (Merenda)lst_Merende.SelectedItem;
            lst_Scelte.Items.Add(ms);
            btn_Rimuovi.IsEnabled = true;
            btn_Stampa.IsEnabled = false;
        }

        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            lst_Scelte.Items.Clear();
        }

        private void Btn_Rimuovi_Click(object sender, RoutedEventArgs e)
        {
            Merenda ms = (Merenda)lst_Scelte.SelectedItem;
            lst_Scelte.Items.Remove(ms);
        }

        private void Btn_Calcola_Click(object sender, RoutedEventArgs e)
        {
            double prezzo = 0;

            for(int i=0; i<lst_Scelte.Items.Count; i++)
            {
                Merenda ms = (Merenda)lst_Scelte.Items[i];

                prezzo += ms.Prezzo;
            }

            txt_Spesa.Text = Convert.ToString(prezzo) + "€";

            btn_Stampa.IsEnabled = true;
        }

        private void Btn_Stampa_Click(object sender, RoutedEventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"ListaMerende.txt"))
            {
                file.Write($"{txt_Classe.Text} con totale spesa {txt_Spesa.Text}, per le seguenti merende:");

                foreach(Merenda ms in lst_Scelte.Items)
                {
                    file.Write("\n");
                    file.Write($"{ms}");
                }
                
            }

            Process.Start(@"ListaMerende.txt");
        }
    }
}
