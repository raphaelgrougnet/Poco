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

        public FormPrincipal()
        {
            InitializeComponent();
            List<Employe> lstEmployes = Utils.ChargerListeEmployes("Files/Employes.csv");
            List<Facture> lstFactures = Utils.ChargerListeFacture("Files/Factures.csv");
            //string path = GestionEmploye.PATH_FILES + "Employes.csv";
            //lstStringEmployes = Utils.ChargerDonnees("Employes.csv");
            _gestionEmploye = new GestionEmploye();
            _gestionFacture = new GestionFacture(lstFactures);

            //foreach (string[] lstEmploye in lstStringEmployes)
            //{

            //    string code = lstEmploye[0];
            //    string nom = lstEmploye[1];
            //    string prenom = lstEmploye[2];
            //    DateTime dob = DateTime.Parse(lstEmploye[3], cultureinfo);
            foreach (Employe employe in lstEmployes)
            {
                string validation = _gestionEmploye.ValiderEmploye(employe.Code, employe.Nom, employe.Prenom, employe.DateNaissance);

                if (validation == "")
                {
                    
                    _gestionEmploye.AjouterEmploye(employe);
                    if (_gestionEmploye.DictEmployesCodes.ContainsKey(employe.Code) == false)
                    {
                        _gestionEmploye.DictEmployesCodes.Add(employe.Code, employe);
                    }

                }
            }
            


            //}


            lstEmployesPresents.ItemsSource = _gestionEmploye.ListeEmployesPresent;

        }

        private void btnAjtEmploye_Click(object sender, RoutedEventArgs e)
        {
            FormGestionEmployes frm = new FormGestionEmployes(_gestionEmploye);
            frm.ShowDialog();
            lstEmployesPresents.Items.Refresh();

        }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void btnPoincon_Click(object sender, RoutedEventArgs e)
        {
            
            FormPoincon frp = new FormPoincon(_gestionEmploye);
            frp.ShowDialog();
            lstEmployesPresents.Items.Refresh();
        }

        private void EnregistrerDonnes()
        {
            string donneesEmployes = "Code;Nom;Prenom;DOB\n";
            foreach (Employe employe in _gestionEmploye.ListeEmployes)
            {
                donneesEmployes += String.Format($"{employe.Code};{employe.Nom};{employe.Prenom};{employe.DateNaissance.ToString("dd-MM-yyyy")}\n");
            }
            donneesEmployes.TrimEnd();
            string path = "Files/Employes.csv";
            Utils.EnregistrerDonnees(path, donneesEmployes, false);
            path = "Files/Factures.csv";
            string donneesFactures = "NoFacture;SousTotalFacture;TotalFacture\n";
            
            foreach (Facture facture in _gestionFacture.ListeFactures)
            {
                
                 donneesFactures += String.Format($"{facture.NoFacture};{facture.Date.ToString("dd-MM-yyyy")};{facture.SousTotal};{facture.PrixTotal}\n");

                
            }
            donneesFactures.TrimEnd();
            Utils.EnregistrerDonnees(path, donneesFactures, false);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (MessageBoxResult.Yes ==  MessageBox.Show("Voulez-vous quitter l'application ?", "Fermeture de l'application", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
                EnregistrerDonnes();
                MessageBox.Show("Enregistrement terminé", "Fermeture de l'application", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                e.Cancel = true;
            
            
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
        

        /// <summary>
        /// Click de la souris sur un boutton du keypad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            string code = txtCode1.Text + txtCode2.Text + txtCode3.Text + txtCode4.Text;
                            ValiderCode(code);
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
    }
}
