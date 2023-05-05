using Poco.Views;
using System;
using System.Collections.Generic;
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
using Poco.Models;

namespace Poco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FormPrincipal : Window
    {
        public GestionEmploye _gestionEmploye;
        public GestionFacture _gestionFacture;
        public static System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("fr-FR");

        public static Dictionary<TypeLegume, int> DictGarnitureQuantite = new Dictionary<TypeLegume, int>();

        public FormPrincipal()
        {
            InitializeComponent();

            _gestionEmploye = new GestionEmploye();
            _gestionFacture = new GestionFacture(new List<Facture>());

            DictGarnitureQuantite = Utils.ChargerDonnees(_gestionEmploye, _gestionFacture);

            foreach (Employe employe in _gestionEmploye.ListeEmployes)
            {
                string validation = _gestionEmploye.ValiderEmploye(employe.Code, employe.Nom, employe.Prenom, employe.DateNaissance);

                if (validation == "")
                {

                    
                    if (_gestionEmploye.DictEmployesCodes.ContainsKey(employe.Code) == false)
                    {
                        _gestionEmploye.DictEmployesCodes.Add(employe.Code, employe);
                    }

                }
            }

            lstEmployesPresents.ItemsSource = _gestionEmploye.ListeEmployesPresent;
            

        }

        private void btnPoincon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormPoincon frp = new FormPoincon(_gestionEmploye);
                frp.ShowDialog();
                lstEmployesPresents.Items.Refresh();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur s'est produite lors de l'ouverture de l'interface Poincon, veuillez reporter cette erreur à l'administrateur de l'application : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void btnAjtEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FormGestionEmployes frm = new FormGestionEmployes(_gestionEmploye);
                frm.ShowDialog();
                lstEmployesPresents.Items.Refresh();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur s'est produite lors de l'ouverture de l'interface Ajout Employé, veuillez reporter cette erreur à l'administrateur de l'application : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void btnInventaire_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                FormInventaire form = new FormInventaire(DictGarnitureQuantite);
                form.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur s'est produite lors de l'ouverture de l'interface Inventaire, veuillez reporter cette erreur à l'administrateur de l'application : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void btnAide_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Keypad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string contenuBtn = (sender as Button).Content.ToString();
                if (txtCode1.Text == "")
                {
                    txtCode1.Text = contenuBtn;
                }
                else
                {
                    if (txtCode2.Text == "")
                    {
                        txtCode2.Text = contenuBtn;
                    }
                    else
                    {
                        if (txtCode3.Text == "")
                        {
                            txtCode3.Text = contenuBtn;
                        }
                        else
                        {
                            if (txtCode4.Text == "")
                            {
                                txtCode4.Text = contenuBtn;
                                string code = txtCode1.Text + txtCode2.Text + txtCode3.Text + txtCode4.Text;
                                ValiderCode(code);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur s'est produite lors de la saisie d'un code, veuillez reporter cette erreur à l'administrateur de l'application : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void Keypad_Clear(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCode4.Text != "")
                {
                    txtCode4.Text = "";
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

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur s'est produite lors de l'effacement du code, veuillez reporter cette erreur à l'administrateur de l'application : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Voulez-vous quitter l'application ?", "Fermeture de l'application", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (_gestionEmploye.ListeEmployesPresent.Count > 0)
                    {
                        if (MessageBoxResult.Yes == MessageBox.Show("Vous devez poinçonner les employés encore présents.\nVoulez-vous les poinçonner ?", "Fermeture de l'application", MessageBoxButton.YesNo, MessageBoxImage.Question))
                        {
                            foreach (Employe emp in _gestionEmploye.ListeEmployesPresent)
                            {
                                emp.MesPoincons.Add(new Poincon(eTypePoincon.Sortie));

                            }
                        }
                        else
                            e.Cancel = true;
                    }
                    Utils.EnregistrerDonnees(_gestionEmploye, _gestionFacture, DictGarnitureQuantite);
                    MessageBox.Show("Enregistrement terminé", "Fermeture de l'application", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    e.Cancel = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur s'est produite lors de la fermeture de l'application, veuillez reporter cette erreur à l'administrateur de l'application : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        private void ValiderCode(string pCode)
        {
            if (_gestionEmploye.DictEmployesCodes.ContainsKey(pCode))
            {
                if (!_gestionEmploye.ListeEmployesPresent.Contains(_gestionEmploye.DictEmployesCodes[pCode]))
                {
                    MessageBoxResult resultat = MessageBox.Show($"L'employé {_gestionEmploye.DictEmployesCodes[pCode]} n'est pas poinçonné.\nVoulez-vous le poinçonner?", "Connexion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultat == MessageBoxResult.Yes)
                    {
                        FormPoincon frp = new FormPoincon(_gestionEmploye);
                        frp.LoginParAutreForm(pCode);
                        frp.ShowDialog();
                        
                        lstEmployesPresents.Items.Refresh();
                    }
                    txtCode1.Text = "";
                    txtCode2.Text = "";
                    txtCode3.Text = "";
                    txtCode4.Text = "";
                }
                else
                {
                    txtErreur.Text = "";
                    
                    txtCode1.Text = "";
                    txtCode2.Text = "";
                    txtCode3.Text = "";
                    txtCode4.Text = "";

                    FormFacture frf = new FormFacture(_gestionFacture, _gestionEmploye);
                    _gestionEmploye.EmployeActif = _gestionEmploye.DictEmployesCodes[pCode];
                    frf.ShowDialog();

                }
                
            }
            else
            {
                
                txtErreur.Text = "Code invalide, aucun employé trouvé.";
                txtCode1.Text = "";
                txtCode2.Text = "";
                txtCode3.Text = "";
                txtCode4.Text = "";
            }
        }
        
    }
}
