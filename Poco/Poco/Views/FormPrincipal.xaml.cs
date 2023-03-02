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
                }
                
                
            }
        }

        private void btnAjtEmploye_Click(object sender, RoutedEventArgs e)
        {
            FormGestionEmployes frm = new FormGestionEmployes(_gestionEmploye);
            frm.Show();
        }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnPoincon_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
