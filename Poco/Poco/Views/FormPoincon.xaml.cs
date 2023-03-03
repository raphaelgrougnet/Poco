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
    /// Interaction logic for FormPoincon.xaml
    /// </summary>
    public partial class FormPoincon : Window
    {
        
        private GestionEmploye _gestionEmploye;

        public FormPoincon(GestionEmploye pGestionEmploye)
        {
            InitializeComponent();

            _gestionEmploye = pGestionEmploye;

            btnDeconnexion.IsEnabled = false;
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
