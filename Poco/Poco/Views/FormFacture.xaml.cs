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

        private GestionFacture _gestionFacture = new GestionFacture(Utils.ChargerListeFacture(GestionEmploye.PATH_FILES + "Factures.csv"));

        


        public FormFacture()
        {
            InitializeComponent();


            
            InitialiserVente();
        }

        private void InitialiserVente()
        {
            spGarniture.IsEnabled = false;
            spViandes.IsEnabled = false;
            _factureCourante = _gestionFacture.CreerFacture();
            lstFacture.ItemsSource = _factureCourante.ListePlats;
            lblNoFacture.DataContext = _factureCourante;
        }


        

        private void Button_Click_Plat(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;

            _platCourant.TPlat = (TypePlat)Enum.Parse(typeof(TypePlat), btn.Content.ToString());

        }

        private void Button_Click_Viande(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            _platCourant.ListeGarniture.Add(new Viande(btn.Content.ToString()));
        }

        private void Button_Click_Garniture(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            _platCourant.ListeGarniture.Add(new Legume(btn.Content.ToString()));
        }

        private void btnPayer_Click(object sender, RoutedEventArgs e)
        {
            InitialiserVente();
        }
    }
}
