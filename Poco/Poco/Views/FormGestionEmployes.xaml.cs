using Poco.Models;
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
using System.Windows.Shapes;

namespace Poco.Views
{
    /// <summary>
    /// Logique d'interaction pour FormGestionEmployes.xaml
    /// </summary>
    public partial class FormGestionEmployes : Window
    {

        private GestionEmploye _gestionEmploye;

        public FormGestionEmployes(GestionEmploye pGestionEmploye)
        {
            InitializeComponent();

            lstEmployes.ItemsSource = pGestionEmploye.ListeEmployes;
            _gestionEmploye = pGestionEmploye;
            btnSupprimer.IsEnabled = false;
            btnAjouter.IsEnabled = true;
        }

        private void InitialiserChamps()
        {
            txtNom.Text = "";
            txtPrenom.Text = "";
            dateDOB.SelectedDate = null;

            txtNom.IsEnabled = true;
            txtPrenom.IsEnabled = true;
            dateDOB.IsEnabled = true;

            btnAjouter.Content = "Ajouter";
            btnSupprimer.IsEnabled = false;
            borderSupprimer.IsEnabled = false;

            txtCode1.Text = "";
            txtCode2.Text = "";
            txtCode3.Text = "";
            txtCode4.Text = "";

            btn0.IsEnabled = true;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            btn3.IsEnabled = true;
            btn4.IsEnabled = true;
            btn5.IsEnabled = true;
            btn6.IsEnabled = true;
            btn7.IsEnabled = true;
            btn8.IsEnabled = true;
            btn9.IsEnabled = true;
            btnC.IsEnabled = true;

            lstEmployes.SelectedIndex = -1;
            lstEmployes.Items.Refresh();
        }

        private void SelectionEmploye(Employe emp)
        {

            txtNom.Text = emp.Nom;
            txtPrenom.Text = emp.Prenom;
            dateDOB.SelectedDate = emp.DateNaissance;

            txtNom.IsEnabled = false;
            txtPrenom.IsEnabled = false;
            dateDOB.IsEnabled = false;
            btnAjouter.Content = "Nouveau";
            btnSupprimer.IsEnabled = true;
            borderSupprimer.IsEnabled = true;

            txtCode1.Text = emp.Code[0].ToString();
            txtCode2.Text = emp.Code[1].ToString();
            txtCode3.Text = emp.Code[2].ToString();
            txtCode4.Text = emp.Code[3].ToString();

            btn0.IsEnabled = false;
            btn1.IsEnabled = false;
            btn2.IsEnabled = false;
            btn3.IsEnabled = false;
            btn4.IsEnabled = false;
            btn5.IsEnabled = false;
            btn6.IsEnabled = false;
            btn7.IsEnabled = false;
            btn8.IsEnabled = false;
            btn9.IsEnabled = false;
            btnC.IsEnabled = false;
        }

        
        /// <summary>
        /// Click de la souris sur un employé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border control = sender as Border;
            Employe employe = (Employe) control.DataContext;
            SelectionEmploye(employe);

            
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

        
        private void btnAjouter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lstEmployes.SelectedIndex == -1)
            {
                DateTime dateSelec = new DateTime(0);
                if (dateDOB.SelectedDate is not null)
                {
                    dateSelec = dateDOB.SelectedDate.Value;
                }


                string code = txtCode1.Text + txtCode2.Text + txtCode3.Text + txtCode4.Text;
                string message = _gestionEmploye.ValiderEmploye(code, txtNom.Text, txtPrenom.Text, dateSelec);
                if (message != "")
                {
                    MessageBox.Show(message, "Ajouter un employé", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                    Employe newE = new Employe(code, txtNom.Text, txtPrenom.Text, dateDOB.SelectedDate.Value);
                    _gestionEmploye.AjouterEmploye(newE);
                    InitialiserChamps();
                }
            }
            else
            {
                InitialiserChamps();
            }
        }

        private void btnSupprimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TODO : FAIRE VALIDER A L'UTILISATEUR LA SUPPRESSION
            if (lstEmployes.SelectedItem is not null)
            {
                _gestionEmploye.SupprimerEmploye((Employe)lstEmployes.SelectedItem);

            }
            InitialiserChamps();
        }

        private void btnFermer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
