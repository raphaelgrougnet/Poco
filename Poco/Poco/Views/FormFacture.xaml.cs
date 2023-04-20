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
            InitialiserPlat();
            _factureCourante = _gestionFacture.CreerFacture();
            lstFacture.ItemsSource = _factureCourante.ListePlats;
            lblNoFacture.DataContext = _factureCourante;
            lblTotalFacture.DataContext = null;
            lblTotalFacture.DataContext = _factureCourante;
            lstFacture.Items.Refresh();

            

        }

        private void InitialiserPlat()
        {
            spGarniture.IsEnabled = false;
            spExtras.IsEnabled = false;
            spViandes.IsEnabled = false;
            spPlats.IsEnabled = true;

            btnPayer.IsEnabled = false;
            btnAjouter.IsEnabled = false;
            btnRetirer.IsEnabled = false;

            lstFacture.SelectedItem = null;

            foreach ((TypeLegume legume, int qte) in FormPrincipal.DictGarnitureQuantite)
            {

                string nomElem = legume.ToString();
                Border bordure = this.FindName(nomElem) as Border;
                if (qte <= 0)
                {
                    bordure.IsEnabled = false;
                }
                else
                {
                    bordure.IsEnabled = true;
                }

            }
        }

        private void MiseAJourPrix()
        {
            _factureCourante.SousTotal = _factureCourante.CalculerSousTotal();
            _factureCourante.PrixTotal = _factureCourante.CalculerPrixTotal();
            
            lblTotalFacture.DataContext = null;
            

            lblTotalFacture.DataContext = _factureCourante;
        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            if (lstFacture.SelectedItem != null)
            {
                if (_platCourant != null)
                {
                    MessageBox.Show("Veuillez finir le plat en cours.");
                    lstFacture.SelectedItem = null;
                    btnRetirer.IsEnabled = false;
                    return;
                }

                Plat plat = lstFacture.SelectedItem as Plat;
                foreach (Garniture garniture in plat.ListeGarniture)
                {
                    FormPrincipal.DictGarnitureQuantite[Enum.Parse<TypeLegume>(garniture.Nom)]++;
                    
                }
                _factureCourante.ListePlats.Remove(plat);
                lstFacture.Items.Refresh();
                btnRetirer.IsEnabled = false;
                InitialiserPlat();
                if (_factureCourante.ListePlats.Count == 0)
                {
                    btnPayer.IsEnabled = false;
                }

                MiseAJourPrix();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un plat à retirer.");
            }

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderQteGarnitureAjout(_platCourant))
            {
                lstFacture.Items.Refresh();
                InitialiserPlat();
                _platCourant = null;
                //DeselectionnerToogleButton();
                if (_factureCourante.ListePlats.Count == 0)
                    btnPayer.IsEnabled = false;
                else
                    btnPayer.IsEnabled = true;

                MiseAJourPrix();
            }
            
        }

        private bool ValiderQteGarnitureAjout(Plat pPlat)
        {
            string legumesManquants = "";
            foreach (Garniture garniture in pPlat.ListeGarniture)
            {
                if (FormPrincipal.DictGarnitureQuantite[Enum.Parse<TypeLegume>(garniture.Nom)] <= 0)
                {
                    legumesManquants += " " + garniture.Nom;
                }
            }
            if (legumesManquants != "")
            {
                MessageBox.Show($"Impossible de compléter la commande car il n'y a plus de{legumesManquants}.\nVeuillez vérifier le stock ou modifier le plat {pPlat.Nom} {pPlat.ViandeP.Nom}.", "Erreur de quantitée", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;

            }
            
            foreach (Garniture garniture in pPlat.ListeGarniture)
            {
                FormPrincipal.DictGarnitureQuantite[Enum.Parse<TypeLegume>(garniture.Nom)]--;
            }
            
            return true;
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
            MiseAJourPrix();
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
            spExtras.IsEnabled = true;
            if (v.Nom == "Vege")
            {
                btnExtraViande.IsEnabled = false;
            }
            else
            {
                btnExtraViande.IsEnabled = true;
            }

            MiseAJourPrix();




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



            MiseAJourPrix();


        }


        private void btnPayer_Click(object sender, RoutedEventArgs e)
        {
            if (_platCourant == null && _factureCourante.ListePlats.Count > 0)
            {
                _gestionFacture.ListeFactures.Add(_factureCourante);
                MessageBox.Show("Facture payée | Total : " + "$" + _factureCourante.PrixTotal.ToString("n2") + "\nEmployé : " + _gestionEmploye.EmployeActif.Prenom + " " + _gestionEmploye.EmployeActif.Nom);
                //_gestionFacture.SauvegarderFactures("Factures.csv");
                MiseAJourPrix();
                InitialiserVente();
            }
            else
            {
                MessageBox.Show("Veuillez terminer votre commande");
            }

        }

        
        

        private void btnAccueil_Click(object sender, MouseButtonEventArgs e)
        {
            if (_factureCourante.ListePlats.Count > 0)
                MessageBox.Show("Veuillez terminer la facture en cours avant de quitter.", "Facture en cours", MessageBoxButton.OK);
            else
                Close();
        }

        private void lstFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFacture.SelectedItem != null)
            {
                btnRetirer.IsEnabled = true;
            }
        }

        private void ButtonClick_Extras(object sender, MouseButtonEventArgs e)
        {
            Border btn = sender as Border;
            Extra extra = new Extra(Enum.Parse<Extra.TypeExtra>(btn.DataContext.ToString()));

            foreach (Extra ex in _platCourant.ListeExtras)
            {

                if (ex.Nom == extra.Nom)
                {
                    _platCourant.RetirerExtra(ex);
                    lstFacture.Items.Refresh();
                    MiseAJourPrix();

                    return;
                }

            }

            _platCourant.AjouterExtra(extra);
            lstFacture.Items.Refresh();



            MiseAJourPrix();
        }
    }
}
