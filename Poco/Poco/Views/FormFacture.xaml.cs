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

        private GestionEmploye _gestionEmploye;




        public FormFacture(GestionFacture gf, GestionEmploye ge)
        {
            InitializeComponent();

            _gestionFacture = gf;
            _gestionEmploye = ge;

            InitialiserVente();



        }

        private void InitialiserVente()
        {
            spGarniture.IsEnabled = false;
            spViandes.IsEnabled = false;
            spPlats.IsEnabled = true;
            _factureCourante = _gestionFacture.CreerFacture();
            lstFacture.ItemsSource = _factureCourante.ListePlats;
            lblNoFacture.DataContext = _factureCourante;
            lstFacture.Items.Refresh();
            btnAjouter.IsEnabled = false;
            btnRetirer.IsEnabled = false;
            btnPayer.IsEnabled = false;
        }

        private void InitialiserPlat()
        {
            spGarniture.IsEnabled = false;
            spViandes.IsEnabled = false;
            btnAjouter.IsEnabled = false;
            btnRetirer.IsEnabled = false;
            spPlats.IsEnabled = true;
        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            if (lstFacture.SelectedItem != null)
            {
                Plat plat = lstFacture.SelectedItem as Plat;
                _factureCourante.ListePlats.Remove(plat);
                lstFacture.Items.Refresh();
                btnRetirer.IsEnabled = false;
                InitialiserPlat();
                if (_factureCourante.ListePlats.Count == 0)
                {
                    btnPayer.IsEnabled = false;
                }


            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un plat à retirer");
            }

        }



        private void ButtonClick_Plat(object sender, RoutedEventArgs e)
        {
            
            Border btn = sender as Border;
            TypePlat typeP = Utils.ParseEnum<TypePlat>(btn.DataContext.ToString());

            _platCourant = new Plat(typeP);
            spViandes.IsEnabled = true;
            spPlats.IsEnabled = false;

            _factureCourante.ListePlats.Add(_platCourant);
            lstFacture.Items.Refresh();

        }

        private void ButtonClick_Viande(object sender, RoutedEventArgs e)
        {
            Border btn = sender as Border;
            
            Viande v = new Viande(btn.DataContext.ToString());
                
            _platCourant.AjouterGarniture(v);
            
            lstFacture.Items.Refresh();
            spViandes.IsEnabled = false;
            spGarniture.IsEnabled = true;
            btnAjouter.IsEnabled = true;
                
                
            
                    
            
        }

        private void ButtonClick_Garniture(object sender, RoutedEventArgs e)
        {
            Border btn = sender as Border;
            Legume l = new Legume(btn.DataContext.ToString());

            foreach (Garniture garniture in _platCourant.ListeGarniture)
            {
                
                if (garniture.Nom == l.Nom)
                {
                    _platCourant.RetirerGarniture(garniture);
                    lstFacture.Items.Refresh();
                    return;
                }
                
            }
            
            _platCourant.AjouterGarniture(l);
            lstFacture.Items.Refresh();






        }


        private void btnPayer_Click(object sender, RoutedEventArgs e)
        {
            if (_platCourant == null && _factureCourante.ListePlats.Count > 0)
            {
                _gestionFacture.ListeFactures.Add(_factureCourante);
                MessageBox.Show("Facture payée | Total : " + _factureCourante.PrixTotal.ToString("C2") + "\nEmployé : " + _gestionEmploye.EmployeActif.Prenom + " " + _gestionEmploye.EmployeActif.Nom);
                //_gestionFacture.SauvegarderFactures("Factures.csv");
                InitialiserVente();
            }
            else
            {
                MessageBox.Show("Veuillez terminer votre commande");
            }

        }


        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            lstFacture.Items.Refresh();
            spGarniture.IsEnabled = false;
            spViandes.IsEnabled = false;
            spPlats.IsEnabled = true;
            btnAjouter.IsEnabled = false;
            _platCourant = null;
            //DeselectionnerToogleButton();
            if (_factureCourante.ListePlats.Count == 0)
                btnPayer.IsEnabled = false;
            else
                btnPayer.IsEnabled = true;
        }

        private void btnAccueil_Click(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void lstFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFacture.SelectedItem != null)
            {
                btnRetirer.IsEnabled = true;
            }
        }
    }
}
