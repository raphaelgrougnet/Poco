using Poco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        private GestionFacture _gestionFacture;

        


        public FormFacture()
        {
            InitializeComponent();

            _gestionFacture = new GestionFacture(Utils.ChargerListeFacture(GestionEmploye.PATH_FILES + "Factures.csv"));

            InitialiserVente();



        }

        private void InitialiserVente()
        {
            spGarniture.IsEnabled = false;
            spViandes.IsEnabled = false;
            _factureCourante = _gestionFacture.CreerFacture();
            lstFacture.ItemsSource = _factureCourante.ListePlats;
            lblNoFacture.DataContext = _factureCourante;
            lstFacture.Items.Refresh();
        }

        private void DeselectionnerToogleButton()
        {
            btnAvocat.IsChecked = false;
            btnJalapeno.IsChecked = false;
            btnMais.IsChecked = false;
            btnOignon.IsChecked = false;
            btnOignonF.IsChecked = false;
            btnOlive.IsChecked = false;
            btnPoivron.IsChecked = false;
            btnRiz.IsChecked = false;
            btnSalade.IsChecked = false;
            btnTomate.IsChecked = false;
            
        }

        

        private void ButtonClick_Plat(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;
            TypePlat typeP = Utils.ParseEnum<TypePlat>(btn.Content.ToString());

            _platCourant = new Plat(typeP);
            spViandes.IsEnabled = true;
            spPlats.IsEnabled = false;

        }

        private void ButtonClick_Viande(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            Viande v = new Viande(btn.Content.ToString());
                
            _platCourant.AjouterGarniture(v);
            spViandes.IsEnabled = false;
            spGarniture.IsEnabled = true;
            btnAjouter.IsEnabled = true;
                
                
            
                    
            
        }

        private void Button_Checked_Garniture(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;
            _platCourant.AjouterGarniture(new Legume(btn.Content.ToString()));
            

        }

        private void Button_Unchecked_Garniture(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = sender as ToggleButton;
            _platCourant.RetirerGarniture(new Legume(btn.Content.ToString()));
        }

        private void btnPayer_Click(object sender, RoutedEventArgs e)
        {
            InitialiserVente();
        }


        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            _factureCourante.ListePlats.Add(_platCourant);
            lstFacture.Items.Refresh();
            spGarniture.IsEnabled = false;
            spViandes.IsEnabled = false;
            spPlats.IsEnabled = true;
            btnAjouter.IsEnabled = false;
            _platCourant = null;
            DeselectionnerToogleButton();
        }

        
    }
}
