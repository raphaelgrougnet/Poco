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
    /// Logique d'interaction pour FormFacture.xaml
    /// </summary>
    public partial class FormFacture : Window
    {
        public FormFacture()
        {
            InitializeComponent();
            Plat plat = new Plat(TypePlat.Nachos);
            plat.AjouterGarniture(new Legume("Mais"));
            plat.AjouterGarniture(new Legume("Oignon"));
            plat.AjouterGarniture(new Legume("Poivron"));
            plat.AjouterGarniture(new Legume("Tomate"));

            Facture f = new Facture(1, DateTime.Today, new List<Plat>() { plat, plat }, 25.0m, 25.3m);
            
            
            lstFacture.ItemsSource = f.ListePlats;
            
        }
    }
}
