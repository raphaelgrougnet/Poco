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
    public partial class MainWindow : Window
    {
        public GestionEmploye _gestionEmploye;
        

        public MainWindow()
        {
            InitializeComponent();
            List<string[]> lstStringEmployes = new List<string[]>();
            string path = GestionEmploye.PATH_FILES + "Employes.csv";
            lstStringEmployes = Utils.ChargerDonnees(path);
            _gestionEmploye = new GestionEmploye();


            foreach (string[] lstEmploye in lstStringEmployes)
            {
                
                string code = lstEmploye[0];
                string nom = lstEmploye[1];
                string prenom = lstEmploye[2];
                DateTime dob = DateTime.Parse(lstEmploye[3]);

                string validation = _gestionEmploye.ValiderEmploye(code, nom, prenom, dob);

                if (validation == "")
                {
                    Employe e = new Employe(code, nom, prenom, dob);
                    _gestionEmploye.AjouterEmploye(e);
                    if (_gestionEmploye.DictEmployesCodes.ContainsKey(code) == false)
                    {
                        _gestionEmploye.DictEmployesCodes.Add(code, e);
                    }
                    
                }
                
                
            }

            
            lstEmployesPresents.ItemsSource = _gestionEmploye.ListeEmployesPresent;

        }

        private void btnAjtEmploye_Click(object sender, RoutedEventArgs e)
        {
            FormGestionEmployes frm = new FormGestionEmployes(_gestionEmploye);
            frm.ShowDialog();
            
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            string donneesEmployes = "Code;Nom;Prenom;DOB\n";
            foreach (Employe employe in _gestionEmploye.ListeEmployes)
            {
                donneesEmployes += String.Format($"{employe.Code};{employe.Nom};{employe.Prenom};{employe.DateNaissance}\n");
            }
            donneesEmployes.TrimEnd();
            string path = GestionEmploye.PATH_FILES + "Employes.csv";
            Utils.EnregistrerDonneesCrush(path, donneesEmployes);
        }


        private void ValiderCode(string pCode)
        {
            if (_gestionEmploye.DictEmployesCodes.ContainsKey(pCode))
            {
                txtErreur.Text = "";
                MessageBox.Show($"Bienvenue {_gestionEmploye.DictEmployesCodes[pCode]}");
                
                txtCode1.Text = "";
                txtCode2.Text = "";
                txtCode3.Text = "";
                txtCode4.Text = "";
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
