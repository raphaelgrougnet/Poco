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
using static System.Net.WebRequestMethods;

namespace Poco.Views
{
    /// <summary>
    /// Interaction logic for FormPoincon.xaml
    /// </summary>
    public partial class FormPoincon : Window
    {
        
        private GestionEmploye _gestionEmploye;

        private Employe _employeConnecter;

        public FormPoincon(GestionEmploye pGestionEmploye)
        {
            InitializeComponent();

            _gestionEmploye = pGestionEmploye;
            _employeConnecter = null;
            

            btnSortie.IsEnabled = false;
            btnEntree.IsEnabled = false;
            
        }

        public void LoginParAutreForm(string pCode)
        {
            txtCode1.Text = pCode.Substring(0, 1);
            txtCode2.Text = pCode.Substring(1, 1);
            txtCode3.Text = pCode.Substring(2, 1);
            txtCode4.Text = pCode.Substring(3, 1);
            _employeConnecter = ConnexionEmploye();
            lblNomEmploye.Content = _employeConnecter;
            AfficherListePoincon();
        }

        /// <summary>
        /// Gère IsEnable des boutons Sortie et Entrée selon le nombres poinçons dans chacune des listes 
        /// </summary>
        private void GestionBtnEntreeSortie()
        {
            if (lstEntree.Items.Count == lstSortie.Items.Count)
            {
                btnEntree.IsEnabled = true;
                btnSortie.IsEnabled = false;
            }
            if (lstEntree.Items.Count > lstSortie.Items.Count)
            {
                btnEntree.IsEnabled = false;
                btnSortie.IsEnabled = true;
            }
            if (lstEntree.Items.Count < lstSortie.Items.Count)
            {
                btnEntree.IsEnabled = true;
                btnSortie.IsEnabled = false;
            }
        }

        /// <summary>
        /// Afficer les listes de poinçon de l'employé connecté
        /// </summary>
        private void AfficherListePoincon()
        {
            if(_employeConnecter != null)
            {
                lstEntree.ItemsSource = _employeConnecter.MesPoincons.Where(p => p.TypePoincon == eTypePoincon.Entree);
                lstSortie.ItemsSource = _employeConnecter.MesPoincons.Where(p => p.TypePoincon == eTypePoincon.Sortie);
                lstEntree.Items.Refresh();
                lstSortie.Items.Refresh();

                GestionBtnEntreeSortie();
            }
            
        }

        /// <summary>
        /// Permet de clear les deux listes
        /// </summary>
        private void ClearListe()
        {
            lstEntree.ItemsSource = null;
            lstSortie.ItemsSource = null;
            lstEntree.Items.Refresh();
            lstSortie.Items.Refresh();
            lblNomEmploye.Content = "";
        }

        /// <summary>
        /// Permet la connexion d'un(e) employé(e)
        /// </summary>
        /// <returns></returns>
        private Employe ConnexionEmploye()
        {
            string code = txtCode1.Text + txtCode2.Text + txtCode3.Text + txtCode4.Text;

            if (_gestionEmploye.DictEmployesCodes.ContainsKey(code))
            {
                return _gestionEmploye.DictEmployesCodes[code];
            }

            MessageBox.Show($"Il n'y a pas d'employé avec ce code : {code}", "Connexion", MessageBoxButton.OK, MessageBoxImage.Information);
            txtCode1.Text = "";
            txtCode2.Text = "";
            txtCode3.Text = "";
            txtCode4.Text = "";
            
            return null;
        }

       


        private void Keypad_Click(object sender, RoutedEventArgs e)
        {
            if (txtCode1.Text == "")
            {
                txtCode1.Text = (sender as Button).Content.ToString();
            }
            else
            {
                if (txtCode2.Text == "")
                {
                    txtCode2.Text = (sender as Button).Content.ToString();
                }
                else
                {
                    if (txtCode3.Text == "")
                    {
                        txtCode3.Text = (sender as Button).Content.ToString();
                    }
                    else
                    {
                        if (txtCode4.Text == "")
                        {
                            txtCode4.Text = (sender as Button).Content.ToString();
                            _employeConnecter = ConnexionEmploye();
                            lblNomEmploye.Content = _employeConnecter;
                            AfficherListePoincon();

                        }

                    }
                }
            }

        }

        private void Keypad_Clear(object sender, RoutedEventArgs e)
        {
            if (txtCode4.Text != "")
            {
                txtCode4.Text = "";
                btnSortie.IsEnabled = false;
                btnEntree.IsEnabled = false;
                ClearListe();
                _employeConnecter = null;

            }
            else
            {
                if (txtCode3.Text != "")
                {
                    txtCode3.Text = "";
                }
                else
                {
                    if (txtCode2.Text != "")
                    {
                        txtCode2.Text = "";
                    }
                    else
                    {
                        if (txtCode1.Text != "")
                        {
                            txtCode1.Text = "";
                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        private void btnFermer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEntree_Click(object sender, RoutedEventArgs e)
        {
            if(_employeConnecter != null)
            {
                _employeConnecter.MesPoincons.Add(new Poincon(eTypePoincon.Entree));
                _gestionEmploye.ListeEmployesPresent.Add(_employeConnecter);
                AfficherListePoincon();
            }
        }

        private void btnSortie_Click(object sender, RoutedEventArgs e)
        {
            if(_employeConnecter != null)
            {
                _employeConnecter.MesPoincons.Add(new Poincon(eTypePoincon.Sortie));
                _gestionEmploye.ListeEmployesPresent.Remove(_employeConnecter);
                AfficherListePoincon();
            }
        }
    }
}
