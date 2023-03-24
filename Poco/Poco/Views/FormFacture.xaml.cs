using Poco.Models;
using System;
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
using System.Windows.Shapes;

namespace Poco.Views
{
    /// <summary>
    /// Logique d'interaction pour FormFacture.xaml
    /// </summary>
    public partial class FormFacture : Window
    {
        private Facture _factureCourante;

        private Plat _platCourant;

        public FormFacture()
        {
            InitializeComponent();

            

            lstFacture.ItemsSource = _factureCourante.ListePlats;
            
        }

        //private decimal CalculerPrixPlat(TypePlat pPlat)
        //{
        //    decimal prix;
        //    switch (pViande)
        //    {
        //        case "Burritos":
        //            prix = 4M;
        //            break;
        //        case "Fajitas":
        //            prix = 3M;
        //            break;
        //        case "Nachos":
        //            prix = 2.5M;
        //            break;
        //        case "Tacos":
        //            prix = 2.75M;
        //            break;
        //        default:
        //            prix = 0M;
        //            break;

        //    }
        //    return prix;
        //}

        private decimal CalculerPrixViande(string pViande)
        {
            decimal prix;
            switch (pViande)
            {
                case "Boeuf":
                    prix = 2M;
                    break;
                case "Dinde":
                    prix = 2.5M;
                    break;
                case "Poisson":
                    prix = 2M;
                    break;
                case "Porc":
                    prix = 2.25M;
                    break;
                case "Poulet":
                    prix = 1.50M;
                    break;
                case "Végé":
                    prix = 1M;
                    break;
                default:
                    prix = 0M;
                    break;

            }
            return prix;
        }

        private void Button_Click_Plat(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;

            _platCourant.TPlat = (TypePlat)Enum.Parse(typeof(TypePlat), btn.Content.ToString());

        }

        private void Button_Click_Viande(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            _platCourant.ListeGarniture.Add(new Viande(btn.Content.ToString(),CalculerPrixViande(btn.Content.ToString())));
        }

        private void Button_Click_Garniture(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            _platCourant.ListeGarniture.Add(new Legume(btn.Content.ToString()));
        }
    }
}
