﻿using Poco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for FormInventaire.xaml
    /// </summary>
    public partial class FormInventaire : Window
    {
        private Dictionary<Garniture, int> _inventaire;

        public FormInventaire(Dictionary<Garniture, int> pInventaire)
        {
            InitializeComponent();
            _inventaire = pInventaire;
            InitialiserForm();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);
        }

        private void InitialiserForm()
        {
            txtQuantite.Text = "0";
            GestionBtnAjouter();
            AfficherListeGarniture();

        }

        private void AfficherListeGarniture()
        {
            lstGarniture.Items.Clear();

            foreach (KeyValuePair<Garniture, int> garniture in _inventaire)
            {
                lstGarniture.Items.Add(garniture.Key.Nom + " - " + garniture.Value);
            }
        }

        private void GestionBtnAjouter()
        {
            int quantite;

            if (txtQuantite.Text == "")
                quantite = 0;
            else
             quantite = int.Parse(txtQuantite.Text);

            if (lstGarniture.SelectedItem != null)
            {
                if (quantite == 0)
                {
                    btnAjouter.IsHitTestVisible = false;
                }
                else
                {
                    btnAjouter.IsHitTestVisible = true;
                }

            }
            else
            {
                btnAjouter.IsHitTestVisible = false;
            }

            AfficherBtnAjouter();

        }

        private void AfficherBtnAjouter()
        {
            if (!btnAjouter.IsHitTestVisible)
            {
                btnAjouter.BorderBrush = new SolidColorBrush(Color.FromRgb(42,42,42));
                btnAjouter.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                (btnAjouter.Child as Label).Foreground = new SolidColorBrush(Color.FromRgb(132, 131, 131));

                brdQuantite.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                brdQuantite.BorderBrush = new SolidColorBrush(Color.FromRgb(42, 42, 42));
                txtQuantite.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                txtQuantite.Foreground = new SolidColorBrush(Color.FromRgb(132, 131, 131));
            }
            else
            {
                btnAjouter.BorderBrush = new SolidColorBrush(Color.FromRgb(196, 87, 24));
                btnAjouter.Background = new SolidColorBrush(Color.FromRgb(253, 201, 3));
                (btnAjouter.Child as Label).Foreground = new SolidColorBrush(Colors.Black);

                brdQuantite.Background = new SolidColorBrush(Color.FromRgb(254, 225, 111));
                brdQuantite.BorderBrush = new SolidColorBrush(Color.FromRgb(196, 87, 24));
                txtQuantite.Background = new SolidColorBrush(Color.FromRgb(254, 225, 111));
                txtQuantite.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void lstGarniture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GestionBtnAjouter();
        }

        private void txtQuantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            GestionBtnAjouter();
        }

        private void btnFermer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
